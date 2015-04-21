using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Test : System.Web.UI.Page
{
    private string conStr = WebConfigurationManager.ConnectionStrings["DatabaseConnectionString1"].ConnectionString;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FillDogList();
            RadioButtonListLaji.SelectedIndex = 0;
            RadioButtonListLuokka.SelectedIndex = 1;
            RadioButtonListLuokka.Items[0].Enabled = false;
            RadioButtonListTulos.SelectedIndex = 0;
            RadioButtonListSijoitus.SelectedIndex = 0;
            TextBoxKoePisteet.Text = "0";
            CheckBoxRT_TP.Enabled = false;
        }
    }

    protected void FillDogList()
    {
        SqlConnection con = new SqlConnection(conStr);
        SqlCommand cmd = new SqlCommand("Select VirName, KutsName from Dog", con);
        SqlDataReader reader;
        DropDownListTest_Dog.Items.Clear();
        try
        {
            con.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListItem newItem = new ListItem();
                newItem.Text = reader["VirName"].ToString() + " \"" + reader["KutsName"].ToString() + "\"";
                newItem.Value = reader["VirName"].ToString();
                DropDownListTest_Dog.Items.Add(newItem);
            }
        }
        catch (Exception er)
        {
            Response.Write("<script language='javascript'>alert('Some Error!');</script>" + er.ToString());
        }
        finally
        {
            con.Close();
        }
    }
    protected void enableAll()
    {
        RadioButtonListLuokka.Enabled = true;
        RadioButtonListLuokka.Items[0].Enabled = true;
        RadioButtonListLuokka.Items[1].Enabled = true;
        RadioButtonListLuokka.Items[4].Enabled = true;
        RadioButtonListTulos.Items[1].Enabled = true;
        RadioButtonListTulos.Items[2].Enabled = true;
        if (RadioButtonListLuokka.SelectedIndex < 0)
            RadioButtonListLuokka.SelectedIndex = 1;
        CheckBoxRT_TP.Enabled = true;
    }
    protected void RadioButtonListLaji_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonListLaji.SelectedIndex == 0)
        {
            //toko
            enableAll();
            RadioButtonListLuokka.Items[0].Enabled = false;
            if (RadioButtonListLuokka.SelectedIndex == 0)
                RadioButtonListLuokka.SelectedIndex = 1;
            CheckBoxRT_TP.Enabled = false;
        }
        else if (RadioButtonListLaji.SelectedIndex == 1)
        {
            //rt
            enableAll();
            RadioButtonListLuokka.Items[0].Enabled = false;
            if (RadioButtonListLuokka.SelectedIndex == 0)
                RadioButtonListLuokka.SelectedIndex = 1;
            RadioButtonListTulos.Items[1].Enabled = false;
            RadioButtonListTulos.Items[2].Enabled = false;
            if (RadioButtonListTulos.SelectedIndex == 1 || RadioButtonListTulos.SelectedIndex == 2)
            {
                RadioButtonListTulos.SelectedIndex = 0;
            }

        }
        else if (RadioButtonListLaji.SelectedIndex == 2)
        {
            //mejä
            enableAll();
            RadioButtonListLuokka.Items[0].Enabled = false;
            RadioButtonListLuokka.Items[1].Enabled = false;
            RadioButtonListLuokka.Items[4].Enabled = false;
            if (RadioButtonListLuokka.SelectedIndex == 0 || RadioButtonListLuokka.SelectedIndex == 1 
                || RadioButtonListLuokka.SelectedIndex == 4)
                RadioButtonListLuokka.SelectedIndex = 2;
            CheckBoxRT_TP.Enabled = false;

        }
        else if (RadioButtonListLaji.SelectedIndex == 3)
        {
            //bh
            enableAll();
            RadioButtonListLuokka.Enabled = false;
            RadioButtonListLuokka.SelectedIndex = -1;
            if (RadioButtonListLuokka.SelectedIndex == 0)
                RadioButtonListLuokka.SelectedIndex = 1;
            RadioButtonListTulos.Items[1].Enabled = false;
            RadioButtonListTulos.Items[2].Enabled = false;
            if (RadioButtonListTulos.SelectedIndex == 1 || RadioButtonListTulos.SelectedIndex == 2)
            {
                RadioButtonListTulos.SelectedIndex = 0;
            }
            CheckBoxRT_TP.Enabled = false;

        }
        else if (RadioButtonListLaji.SelectedIndex == 4)
        {
            //rotu-toko
            enableAll();
            CheckBoxRT_TP.Enabled = false;

        }
    }
    protected void ButtonAddTest_Click(object sender, EventArgs e)
    {
        int points = 0, sPoint = 0;
        if (DropDownListTest_Dog.SelectedIndex < 0)
            Response.Write("<script language='javascript'>alert('Käy lisäämässä koira Koira-sivulta');</script>");
        else
        {
            if (TextBoxTestPVM.Text == "")
                Response.Write("<script language='javascript'>alert('Anna päivämäärä.');</script>");
            else
            {
                if (TextBoxTestPlace.Text == "")
                    Response.Write("<script language='javascript'>alert('Anna tapahtumapaikka.');</script>");
                else
                {
                    DateTime dDate;
                    if (!(DateTime.TryParse(TextBoxTestPVM.Text, out dDate)))
                    {
                        Response.Write("<script language='javascript'>alert('Anna päivämäärä.');</script>");
                    }
                    else
                    {
                        SqlConnection con = new SqlConnection(conStr);
                        SqlCommand cmd = new SqlCommand("Insert into Test(DogVirName,Date,Place,Type,TestResult,TestPoints,TestSija,KisaPoints) values(@virName,@date,@place,@type,@result,@points,@sija,@skisa)", con);
                        cmd.Parameters.AddWithValue("@virName", DropDownListTest_Dog.SelectedValue);
                        cmd.Parameters.AddWithValue("@date", dDate);
                        cmd.Parameters.AddWithValue("@place", TextBoxTestPlace.Text);
                        cmd.Parameters.AddWithValue("@type", RadioButtonListLaji.SelectedValue);
                        cmd.Parameters.AddWithValue("@result", RadioButtonListLuokka.SelectedValue + " " + RadioButtonListTulos.SelectedValue);
                        int koepiste;
                        if (int.TryParse(TextBoxKoePisteet.Text, out koepiste))
                            cmd.Parameters.AddWithValue("@points", TextBoxKoePisteet.Text);
                        else
                            cmd.Parameters.AddWithValue("@points", 0);
                        if (RadioButtonListSijoitus.SelectedIndex == 5)
                            cmd.Parameters.AddWithValue("@sija", TextBoxMuuSijoitus.Text);
                        else
                            cmd.Parameters.AddWithValue("@sija", RadioButtonListSijoitus.SelectedValue);

                        points = getTestPoints();
                        sPoint = stayPoints(DropDownListTest_Dog.SelectedValue);
                        cmd.Parameters.AddWithValue("@skisa", points);
                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception er)
                        {
                            Response.Write("<script language='javascript'>alert('Lisäys ei onnistunut!');</script>" + er.ToString());
                        }
                        finally
                        {
                            con.Close();
                            TextBoxAddTestResult.Text = "" + (points + sPoint);
                            countPoints();
                        }
                    }
                }
            }
        }
    }
    protected int getTestPoints()
    {
        int points = 0, pojo = 0;
        if (RadioButtonListLaji.SelectedIndex == 3)
        {
            //BH
            if (RadioButtonListTulos.SelectedIndex == 0)
                points = 25;
            else
                points = 0;
        }
        else
        {
            if (RadioButtonListLuokka.SelectedIndex == 0)
            {
                //Mölli
                points = 2; //osallistuminen

                if (RadioButtonListTulos.SelectedIndex == 0)
                    points = points + 10;
                else if (RadioButtonListTulos.SelectedIndex == 1)
                    points = points + 7;
                else if (RadioButtonListTulos.SelectedIndex == 2)
                    points = points + 4;
                else
                    points = 0; //Hylätystä tuloksesta ei osallistumispisteitä

                if (RadioButtonListSijoitus.SelectedIndex == 0)
                    points = points + 6;
                else if (RadioButtonListSijoitus.SelectedIndex == 1)
                    points = points + 5;
                else if (RadioButtonListSijoitus.SelectedIndex == 2)
                    points = points + 4;
                else if (RadioButtonListSijoitus.SelectedIndex == 3)
                    points = points + 3;
                else if (RadioButtonListSijoitus.SelectedIndex == 4)
                    points = points + 2;
                else if (RadioButtonListSijoitus.SelectedIndex == 5)
                {
                    if (int.TryParse(TextBoxMuuSijoitus.Text, out pojo))
                    {
                        if (pojo == 6)
                            points = points + 1;
                    }
                }
            }
            else if (RadioButtonListLuokka.SelectedIndex == 1)
            {
                //ALO
                points = 2; //osallistuminen

                if (RadioButtonListTulos.SelectedIndex == 0)
                    points = points + 15;
                else if (RadioButtonListTulos.SelectedIndex == 1)
                    points = points + 10;
                else if (RadioButtonListTulos.SelectedIndex == 2)
                    points = points + 5;
                else
                    points = 0; //Hylätystä tuloksesta ei osallistumispisteitä

                if (RadioButtonListTulos.SelectedIndex != 3 && RadioButtonListSijoitus.SelectedIndex == 0)
                    points = points + 10;
                else if (RadioButtonListTulos.SelectedIndex != 3 && RadioButtonListSijoitus.SelectedIndex == 1)
                    points = points + 8;
                else if (RadioButtonListTulos.SelectedIndex != 3 && RadioButtonListSijoitus.SelectedIndex == 2)
                    points = points + 6;
                else if (RadioButtonListTulos.SelectedIndex != 3 && RadioButtonListSijoitus.SelectedIndex == 3)
                    points = points + 4;
                else if (RadioButtonListTulos.SelectedIndex != 3 && RadioButtonListSijoitus.SelectedIndex == 4)
                    points = points + 2;
            }
            else if (RadioButtonListLuokka.SelectedIndex == 2)
            {
                //AVO
                points = 4; //osallistuminen
                if (RadioButtonListTulos.SelectedIndex == 0)
                    points = points + 20;
                else if (RadioButtonListTulos.SelectedIndex == 1)
                    points = points + 15;
                else if (RadioButtonListTulos.SelectedIndex == 2)
                    points = points + 10;
                else
                    points = 0; //Hylätystä tuloksesta ei osallistumispisteitä

                if (RadioButtonListTulos.SelectedIndex != 3 && RadioButtonListSijoitus.SelectedIndex == 0)
                    points = points + 15;
                else if (RadioButtonListTulos.SelectedIndex != 3 && RadioButtonListSijoitus.SelectedIndex == 1)
                    points = points + 13;
                else if (RadioButtonListTulos.SelectedIndex != 3 && RadioButtonListSijoitus.SelectedIndex == 2)
                    points = points + 11;
                else if (RadioButtonListTulos.SelectedIndex != 3 && RadioButtonListSijoitus.SelectedIndex == 3)
                    points = points + 9;
                else if (RadioButtonListTulos.SelectedIndex != 3 && RadioButtonListSijoitus.SelectedIndex == 4)
                    points = points + 7;
            }
            else if (RadioButtonListLuokka.SelectedIndex == 3)
            {
                //VOI
                points = 6; //osallistuminen
                if (RadioButtonListTulos.SelectedIndex == 0)
                    points = points + 25;
                else if (RadioButtonListTulos.SelectedIndex == 1)
                    points = points + 20;
                else if (RadioButtonListTulos.SelectedIndex == 2)
                    points = points + 15;
                else
                    points = 0; //Hylätystä tuloksesta ei osallistumispisteitä

                if (RadioButtonListTulos.SelectedIndex != 3 && RadioButtonListSijoitus.SelectedIndex == 0)
                    points = points + 20;
                else if (RadioButtonListTulos.SelectedIndex != 3 && RadioButtonListSijoitus.SelectedIndex == 1)
                    points = points + 18;
                else if (RadioButtonListTulos.SelectedIndex != 3 && RadioButtonListSijoitus.SelectedIndex == 2)
                    points = points + 16;
                else if (RadioButtonListTulos.SelectedIndex != 3 && RadioButtonListSijoitus.SelectedIndex == 3)
                    points = points + 14;
                else if (RadioButtonListTulos.SelectedIndex != 3 && RadioButtonListSijoitus.SelectedIndex == 4)
                    points = points + 12;
            }
            else if (RadioButtonListLuokka.SelectedIndex == 4)
            {
                //EVL/MES
                points = 8; //osallistuminen
                if (RadioButtonListTulos.SelectedIndex == 0)
                    points = points + 30;
                else if (RadioButtonListTulos.SelectedIndex == 1)
                    points = points + 25;
                else if (RadioButtonListTulos.SelectedIndex == 2)
                    points = points + 20;
                else
                    points = 0; //Hylätystä tuloksesta ei osallistumispisteitä

                if (RadioButtonListTulos.SelectedIndex != 3 && RadioButtonListSijoitus.SelectedIndex == 0)
                    points = points + 25;
                else if (RadioButtonListTulos.SelectedIndex != 3 && RadioButtonListSijoitus.SelectedIndex == 1)
                    points = points + 23;
                else if (RadioButtonListTulos.SelectedIndex != 3 && RadioButtonListSijoitus.SelectedIndex == 2)
                    points = points + 21;
                else if (RadioButtonListTulos.SelectedIndex != 3 && RadioButtonListSijoitus.SelectedIndex == 3)
                    points = points + 19;
                else if (RadioButtonListTulos.SelectedIndex != 3 && RadioButtonListSijoitus.SelectedIndex == 4)
                    points = points + 17;
            }

            //Lisäpisteet
            //RT tuomaripalkinto
            if (CheckBoxRT_TP.Checked == true && RadioButtonListLaji.SelectedIndex == 1)
                points = points + 10;
            //arvokisa
            if (DropDownListArvokilpailu.SelectedIndex == 1)
                points = points + 15;
            else if (DropDownListArvokilpailu.SelectedIndex == 2)
                points = points + 20;
            else if (DropDownListArvokilpailu.SelectedIndex == 3)
                points = points + 25;
            else if (DropDownListArvokilpailu.SelectedIndex == 4)
                points = points + 30;
            else if (DropDownListArvokilpailu.SelectedIndex == 5)
                points = points + 35;
        }
        return points;
    }
    protected int stayPoints(string dog)
    {
        int basePoints = 0, points = 0; //points on pisteet jotka on tallennettu aikaisemmin. basePoints on pisteet jotka nyt lisätään.
        SqlConnection con = new SqlConnection(conStr);
        SqlCommand cmd = new SqlCommand("Select Basepoints from Dog Where VirName=@dog", con);
        cmd.Parameters.AddWithValue("@dog", dog);
        SqlDataReader reader;
        try
        {
            con.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int.TryParse(reader["Basepoints"].ToString(), out points);                
            }
        }
        catch (Exception er)
        {
            Response.Write("<script language='javascript'>alert('Koiran luku ei onnistunut. stayPoints!');</script>" + er.ToString());
        }
        finally
        {
            con.Close();
        }

        //luokkanousu
        if (DropDownListLuokanVaihto.SelectedIndex == 1)
            basePoints = basePoints + 15;
        else if (DropDownListLuokanVaihto.SelectedIndex == 2)
            basePoints = basePoints + 20;
        else if (DropDownListLuokanVaihto.SelectedIndex == 3)
            basePoints = basePoints + 25;
        //valioituminen
        if (CheckBoxValio.Checked == true)
            basePoints = basePoints + 50;
        //koulutustunnus
        if (DropDownListKoulari.SelectedIndex == 1)
            basePoints = basePoints + 10;
        else if (DropDownListKoulari.SelectedIndex == 2)
            basePoints = basePoints + 20;
        else if (DropDownListKoulari.SelectedIndex == 3)
            basePoints = basePoints + 30;
        else if (DropDownListKoulari.SelectedIndex == 4)
            basePoints = basePoints + 40;

        cmd = new SqlCommand("Update Dog Set Basepoints = @basePoints, FullpointsTest=FullpointsTest+@basePoints Where Virname=@dog", con);
        cmd.Parameters.AddWithValue("@dog", dog);
        cmd.Parameters.AddWithValue("@basePoints", (basePoints + points));
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception er)
        {
            Response.Write("<script language='javascript'>alert('Basepoints päivitys ei onnistunut!');</script>" + er.ToString());
        }
        finally
        {
            con.Close();
        }
        return basePoints; //palautetaan tästä kokeesta ansaitut pysyvät pisteet
    }
    protected void countPoints()
    {
        int point = 0;
        SqlConnection con = new SqlConnection(conStr);
        SqlCommand cmd = new SqlCommand("Select top 5 KisaPoints from Test where DogVirName=@name", con);
        cmd.Parameters.AddWithValue("@name", DropDownListTest_Dog.SelectedValue);
        SqlDataReader reader;
        try
            {
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    point = point + int.Parse(reader["KisaPoints"].ToString());
                }
            }
        catch (Exception er)
        {
            Response.Write("<script language='javascript'>alert('Koetulosten lukuvirhe - countPoints!');</script>" + er.ToString());
        }
        finally
        {
            con.Close();
        }
        SqlCommand cmd2 = new SqlCommand("Update Dog Set FullpointsTest= @point where VirName=@name", con);
        cmd2.Parameters.AddWithValue("@name", DropDownListTest_Dog.SelectedValue);
        cmd2.Parameters.AddWithValue("@point", point);
        try
        {
            con.Open();
            cmd2.ExecuteNonQuery();
        }
        catch (Exception er)
        {
            Response.Write("<script language='javascript'>alert('Koetulosten pisteiden päivitysvirhe!');</script>" + er.ToString());
        }
        finally
        {
            con.Close();
        }
    }
}