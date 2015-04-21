using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Dog : System.Web.UI.Page
{
    private string conStr = WebConfigurationManager.ConnectionStrings["DatabaseConnectionString1"].ConnectionString;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            ButtonEditDog.Enabled = false;
            FillList();
        }
    }
    protected void ButtonAddDog_Click(object sender, EventArgs e)
    {
        if (TextBoxVirName.Text == "")
            Response.Write("<script language='javascript'>alert('Anna koiran virallinen nimi.');</script>");
        else
        {
            if (TextBoxKutsName.Text == "")
                Response.Write("<script language='javascript'>alert('Anna koiran kutsumanimi.');</script>");
            else
            {
                SqlConnection con = new SqlConnection(conStr);
                SqlCommand cmd = new SqlCommand("Insert into Dog(VirName,KutsName,Sex,Basepoints,FullpointsShow,FullpointsTest) values(@virName,@kutsName,@sex,0,0,0)", con);
                cmd.Parameters.AddWithValue("@virName", TextBoxVirName.Text);
                cmd.Parameters.AddWithValue("@kutsName", TextBoxKutsName.Text);
                cmd.Parameters.AddWithValue("@sex", DropDownListSukup.SelectedValue);
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
                    TextBoxVirName.Text = "";
                    TextBoxKutsName.Text = "";
                    DropDownListSukup.SelectedIndex = 0;
                    FillList();
                }
            }
        }
    }

    protected void ButtonEditDog_Click(object sender, EventArgs e)
    {
        if (TextBoxVirName.Text == "")
            Response.Write("<script language='javascript'>alert('Anna koiran virallinen nimi.');</script>");
        else
        {
            if (TextBoxKutsName.Text == "")
                Response.Write("<script language='javascript'>alert('Anna koiran kutsumanimi.');</script>");
            else
            {
                SqlConnection con = new SqlConnection(conStr);
                SqlCommand cmd = new SqlCommand("Update Dog Set VirName=@virName, KutsName=@kutsName, Sex=@sex where VirName=@oldName", con);
                cmd.Parameters.AddWithValue("@oldName", ListBoxDogs.SelectedValue);
                cmd.Parameters.AddWithValue("@virName", TextBoxVirName.Text);
                cmd.Parameters.AddWithValue("@kutsName", TextBoxKutsName.Text);
                cmd.Parameters.AddWithValue("@sex", DropDownListSukup.SelectedValue);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception er)
                {
                    Response.Write("<script language='javascript'>alert('Päivitys ei onnistunut!');</script>" + er.ToString());
                }
                finally
                {
                    con.Close();
                    TextBoxVirName.Text = "";
                    TextBoxKutsName.Text = "";
                    DropDownListSukup.SelectedIndex = 0;
                    FillList();
                }
            }
        }
    }
    protected void FillList()
    {
        SqlConnection con = new SqlConnection(conStr);
        SqlCommand cmd = new SqlCommand("Select VirName, KutsName from Dog", con);
        SqlDataReader reader;
        ListBoxDogs.Items.Clear();
        try
        {
            con.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListItem newItem = new ListItem();
                newItem.Text = reader["VirName"].ToString() + " \"" + reader["KutsName"].ToString() + "\"";
                newItem.Value = reader["VirName"].ToString();
                ListBoxDogs.Items.Add(newItem);
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
    protected void ListBoxDogs_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ListBoxDogs.SelectedIndex < 0)
            ButtonEditDog.Enabled = false;
        else
            ButtonEditDog.Enabled = true;
    }
}