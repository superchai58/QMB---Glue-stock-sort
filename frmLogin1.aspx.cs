using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmLogin1 : System.Web.UI.Page
{
    ConnectDBSMT oConSMT = new ConnectDBSMT();
    protected void Page_Load(object sender, EventArgs e)
    {}

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //--Check UserRight--
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "Select Top 1 Username, PassWord From UserDetail Where Username = @Username AND PassWord = @PassWord";
        cmd.Parameters.Add(new SqlParameter("@Username", txtID.Text.Trim()));
        cmd.Parameters.Add(new SqlParameter("@PassWord", txtPass.Text.Trim()));
        cmd.CommandTimeout = 180;

        DataTable dtCheckLogin = oConSMT.Query(cmd);
        if (dtCheckLogin.Rows.Count > 0)
        {
            //--Open main form--
            Response.Redirect("frmMain1.aspx?UID=" + txtID.Text.Trim() + "");
        }
        else
        {
            txtID.Text = "";
            txtPass.Text = "";

            ClientScript.RegisterStartupScript(this.GetType(), "ERROR!", "alert('Your ID or Password is incorrect.');", true);
            txtID.Focus();
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        //--Clear data--
        txtID.Text = "";
        txtPass.Text = "";
        txtID.Focus();
    }
}