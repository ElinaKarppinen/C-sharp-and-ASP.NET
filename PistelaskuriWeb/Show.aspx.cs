using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Show : System.Web.UI.Page
{
    private string conStr = WebConfigurationManager.ConnectionStrings["DatabaseConnectionString1"].ConnectionString;
    int bisSijoitus = 0, sijoitusPisteet = 0, rypSijoitus = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            RadioButtonListShowType.SelectedIndex = 0;
            RadioButtonShowResults.SelectedIndex = 0;
            TextBoxLkmAll.Text = "0";
            TextBoxLkmSex.Text = "0";
            FillDogList();
        }
    }
    protected void ButtonAddShow_Click(object sender, EventArgs e)
    {
        if (DropDownListShow_Dog.SelectedIndex < 0)
            Response.Write("<script language='javascript'>alert('Käy lisäämässä koira Koira-sivulta');</script>");
        else
        {
            if (TextBoxShowPVM.Text == "")
                Response.Write("<script language='javascript'>alert('Anna päivämäärä.');</script>");
            else
            {
                if (TextBoxShowPlace.Text == "")
                    Response.Write("<script language='javascript'>alert('Anna tapahtumapaikka.');</script>");
                else
                {
                    DateTime dDate;
                    if (!(DateTime.TryParse(TextBoxShowPVM.Text, out dDate)))
                    {
                        Response.Write("<script language='javascript'>alert('Anna päivämäärä.');</script>");
                    }
                    else
                    {
                        SqlConnection con = new SqlConnection(conStr);
                        SqlCommand cmd = new SqlCommand("Insert into Show(DogVirName,Date,Place,Type,Placing,Points) values(@virName,@date,@place,@type,@placing,@points)", con);
                        cmd.Parameters.AddWithValue("@virName", DropDownListShow_Dog.SelectedValue);
                        cmd.Parameters.AddWithValue("@date", dDate);
                        cmd.Parameters.AddWithValue("@place", TextBoxShowPlace.Text);
                        cmd.Parameters.AddWithValue("@type", RadioButtonListShowType.SelectedValue);
                        string placing = "";
                        if (RadioButtonShowResults.SelectedIndex != 5)
                        {
                            placing = RadioButtonShowResults.SelectedValue;
                            if (DropDownListRYP.SelectedIndex != 0)
                                placing = placing + " " + DropDownListRYP.SelectedValue;
                            if (DropDownListBIS.SelectedIndex != 0)
                                placing = placing + " " + DropDownListBIS.SelectedValue;
                        }
                        else
                            placing = TextBoxMuuShow.Text;
                        cmd.Parameters.AddWithValue("@placing", placing);

                        getShowPoints();
                        int points = 0;
                        points = GetExtraShowPoints();
                        points = points + sijoitusPisteet + rypSijoitus + bisSijoitus;
                        cmd.Parameters.AddWithValue("@points", points);
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
                            TextBoxAddShowResult.Text = "" + points;
                            countPoints();
                        }
                    }
                }
            }
        }
    }

    public void getShowPoints()
    {
        bisSijoitus = 0;

        if (RadioButtonListShowType.SelectedIndex == 0)
        { //KV
            if (RadioButtonShowResults.SelectedIndex == 0)
            { //ROP
                sijoitusPisteet = 20;

                if (DropDownListRYP.SelectedValue == "RYP1")
                {
                    rypSijoitus = 20;
                    if (DropDownListBIS.SelectedValue == "BIS1")
                        bisSijoitus = 20;
                    else if (DropDownListBIS.SelectedValue == "BIS2")
                        bisSijoitus = 17;
                    else if (DropDownListBIS.SelectedValue == "BIS3")
                        bisSijoitus = 14;
                    else if (DropDownListBIS.SelectedValue == "BIS4")
                        bisSijoitus = 11;
                    else
                        bisSijoitus = 0;
                }
                else if (DropDownListRYP.SelectedValue == "RYP2")
                    rypSijoitus = 17;
                else if (DropDownListRYP.SelectedValue == "RYP3")
                    rypSijoitus = 14;
                else if (DropDownListRYP.SelectedValue == "RYP4")
                    rypSijoitus = 11;
                else
                    rypSijoitus = 0;
            }
            else if (RadioButtonShowResults.SelectedIndex == 1)
                //VSP
                sijoitusPisteet = 15;
            else if (RadioButtonShowResults.SelectedIndex == 2)
                //PU/PN2
                sijoitusPisteet = 10;
            else if (RadioButtonShowResults.SelectedIndex == 3)
                //PU/PN3
                sijoitusPisteet = 7;
            else if (RadioButtonShowResults.SelectedIndex == 4)
                //PU/PN4
                sijoitusPisteet = 4;
            else
                sijoitusPisteet = 0;
        }
        else //other shows
        {
            if (RadioButtonListShowType.SelectedIndex == 0)
            { //ROP
                sijoitusPisteet = 12;

                if (DropDownListRYP.SelectedValue == "RYP1")
                    rypSijoitus = 10;
                else if (DropDownListRYP.SelectedValue == "RYP2")
                    rypSijoitus = 8;
                else if (DropDownListRYP.SelectedValue == "RYP3")
                    rypSijoitus = 6;
                else if (DropDownListRYP.SelectedValue == "RYP4")
                    rypSijoitus = 4;
                else
                    rypSijoitus = 0;
            }
            else if (RadioButtonShowResults.SelectedIndex == 1)
                //VSP
                sijoitusPisteet = 8;
            else if (RadioButtonShowResults.SelectedIndex == 2)
                sijoitusPisteet = 6;
            else if (RadioButtonShowResults.SelectedIndex == 3)
                sijoitusPisteet = 4;
            else if (RadioButtonShowResults.SelectedIndex == 4)
                sijoitusPisteet = 2;
            else
                sijoitusPisteet = 0;
        }
    }

    public int GetExtraShowPoints()
    {
        int dogLkm = 0, extraPoints = 0, sexLkm = 0;
        if (int.TryParse(TextBoxLkmAll.Text, out dogLkm) && dogLkm >= 0)
        {
            if (int.TryParse(TextBoxLkmSex.Text, out sexLkm) && sexLkm >= 0)
            {
                if (RadioButtonShowResults.SelectedIndex == 0)
                {
                    extraPoints = dogLkm / 5;
                    //1. piste jokaista 5:ttä ilmoitettua koiraa kohti
                }
                else if (RadioButtonShowResults.SelectedIndex > 0 && RadioButtonShowResults.SelectedIndex < 5)
                {
                    extraPoints = sexLkm / 5;
                    //1. piste jokaista 5:ttä ilmoitettua saman sukupuolen koiraa kohti
                }
                else
                    extraPoints = 0;
            }
            else
                Response.Write("<script language='javascript'>alert('Koirien määrän pitää olla kokonaisluku!');</script>");
        }
        else
            Response.Write("<script language='javascript'>alert('Koirien määrän pitää olla kokonaisluku!');</script>");
        return extraPoints;
    }

    protected void RadioButtonShowResults_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonShowResults.SelectedIndex == 0)
        {
            //ROP
            DropDownListRYP.Enabled = true;
            DropDownListBIS.Enabled = true;
        }
        else if (RadioButtonShowResults.SelectedIndex == 1)
        {
            setBISRYP_disabled();
        }
        else if (RadioButtonShowResults.SelectedIndex == 2)
        {
            setBISRYP_disabled();
        }
        else if (RadioButtonShowResults.SelectedIndex == 3)
        {
            setBISRYP_disabled();
        }
        else if (RadioButtonShowResults.SelectedIndex == 4)
        {
            setBISRYP_disabled();
        }
        else if (RadioButtonShowResults.SelectedIndex == 5)
        {
            setBISRYP_disabled();
        }
    }

    protected void setBISRYP_disabled()
    {
        DropDownListBIS.SelectedIndex = 0;
        DropDownListBIS.Enabled = false;
        DropDownListRYP.SelectedIndex = 0;
        DropDownListRYP.Enabled = false;
    }

    protected void DropDownListRYP_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListRYP.SelectedIndex == 1)
        {
            DropDownListBIS.Enabled = true;
        }
        else
        {
            DropDownListBIS.SelectedIndex = 0;
            DropDownListBIS.Enabled = false;
        }
    }

    protected void FillDogList()
    {
        SqlConnection con = new SqlConnection(conStr);
        SqlCommand cmd = new SqlCommand("Select VirName, KutsName from Dog", con);
        SqlDataReader reader;
        DropDownListShow_Dog.Items.Clear();
        try
        {
            con.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListItem newItem = new ListItem();
                newItem.Text = reader["VirName"].ToString() + " \"" + reader["KutsName"].ToString() + "\"";
                newItem.Value = reader["VirName"].ToString();
                DropDownListShow_Dog.Items.Add(newItem);
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
    protected void countPoints()
    {
        int point = 0;
        SqlConnection con = new SqlConnection(conStr);
        SqlCommand cmd = new SqlCommand("Select top 8 Points from Show where DogVirName=@name", con);
        cmd.Parameters.AddWithValue("@name", DropDownListShow_Dog.SelectedValue);
        SqlDataReader reader;
        try
        {
            con.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                point = point + int.Parse(reader["Points"].ToString());
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
        cmd = new SqlCommand("Update Dog Set FullpointsShow=@point where VirName=@name", con);
        cmd.Parameters.AddWithValue("@name", DropDownListShow_Dog.SelectedValue);
        cmd.Parameters.AddWithValue("@point", point);
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception er)
        {
            Response.Write("<script language='javascript'>alert('Näyttelytulosten pisteiden päivitysvirhe!');</script>" + er.ToString());
        }
        finally
        {
            con.Close();
        }
    }
}