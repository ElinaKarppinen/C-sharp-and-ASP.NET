using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Results : System.Web.UI.Page
{
    private string conStr = WebConfigurationManager.ConnectionStrings["DatabaseConnectionString1"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
        }
    }
    protected void ButtonGetResults_Click(object sender, EventArgs e)
    {
        if (DropDownListChooseType.SelectedIndex == 0)
        {
            //Koetulokset
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand("Select * from Test order by DogVirName", con);
            SqlDataReader reader;
            ListBoxResults.Items.Clear();
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ListItem newItem = new ListItem();
                    DateTime date;
                    DateTime.TryParse(reader["Date"].ToString(), out date);
                    
                    if (reader["Type"].ToString() != "BH" || reader["Type"].ToString() != "Rally-Toko")
                    {
                        newItem.Text = reader["DogVirName"].ToString() + ". " + reader["Type"].ToString() + " " + date.ToShortDateString() + " " 
                            + reader["Place"].ToString() + ". " + reader["TestResult"].ToString() + " " + reader["TestPoints"].ToString()
                            + " pistettä, sijoitus:" + reader["TestSija"].ToString() + ". " + reader["KisaPoints"].ToString()
                            + " pistettä ansaittu Harrastusdoggi kisaan.";
                        newItem.Value = reader["Id"].ToString();
                        ListBoxResults.Items.Add(newItem);
                    }
                    else
                    {
                        string testClass;
                        if (reader["TestResult"].ToString() == "1")
                            testClass = "Hyväksytty";
                        else
                            testClass = "Hylätty";
                        newItem.Text = reader["DogVirName"].ToString() + ". " + reader["Type"].ToString() + " " + date.ToShortDateString()
                            + " " + reader["Place"].ToString() + ". " + testClass + " " + reader["TestPoints"].ToString()
                            + " pistettä, sijoitus:" + reader["TestSija"].ToString() + ". " + reader["KisaPoints"].ToString()
                            + " pistettä ansaittu Harrastusdoggi kisaan.";
                        newItem.Value = reader["Id"].ToString();
                        ListBoxResults.Items.Add(newItem);
                    }
                }
            }
            catch (Exception er)
            {
                Response.Write("<script language='javascript'>alert('Koetulosten lukuvirhe!');</script>" + er.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        else if (DropDownListChooseType.SelectedIndex == 1)
        {
            //Näyttelytulokset
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand("Select * from Show order by DogVirName", con);
            SqlDataReader reader;
            ListBoxResults.Items.Clear();
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ListItem newItem = new ListItem();
                    DateTime date;
                    DateTime.TryParse(reader["Date"].ToString(), out date);
                    newItem.Text = reader["DogVirName"].ToString() + ". " + date.ToShortDateString() + " " + reader["Type"].ToString() + " " + reader["Place"].ToString() + ". "
                          + reader["Placing"].ToString() + ". " + reader["Points"].ToString()
                          + " pistettä ansaittu Harrastusdoggi kisaan.";
                    newItem.Value = reader["Id"].ToString();
                    ListBoxResults.Items.Add(newItem);
                }
            }
            catch (Exception er)
            {
                Response.Write("<script language='javascript'>alert('Näyttelytulosten lukuvirhe!');</script>" + er.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        else if (DropDownListChooseType.SelectedIndex == 2)
        {
            //harrastusdoggikisa
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand("Select * from Dog order by FullpointsTest desc", con);
            SqlDataReader reader;
            ListBoxResults.Items.Clear();
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ListItem newItem = new ListItem();
                    newItem.Text = reader["VirName"].ToString() + " \"" + reader["KutsName"].ToString() + "\": " + reader["FullpointsTest"].ToString();
                    ListBoxResults.Items.Add(newItem);                    
                }
            }
            catch (Exception er)
            {
                Response.Write("<script language='javascript'>alert('Koetulosten lukuvirhe!');</script>" + er.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        else if (DropDownListChooseType.SelectedIndex == 3)
        {
            //kultadoggikisa
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand("Select * from Dog order by FullpointsShow desc", con);
            SqlDataReader reader;
            ListBoxResults.Items.Clear();
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ListItem newItem = new ListItem();
                    newItem.Text = reader["VirName"].ToString() + " \"" + reader["KutsName"].ToString() + "\": " + reader["FullpointsShow"].ToString();
                    ListBoxResults.Items.Add(newItem); 
                }
            }
            catch (Exception er)
            {
                Response.Write("<script language='javascript'>alert('Näyttelytulosten lukuvirhe!');</script>" + er.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        else
        {
            Response.Write("<script language='javascript'>alert('Tämä ei pitäisi ikinä tulostua.');</script>");
        }
    }    
}