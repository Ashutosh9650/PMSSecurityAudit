using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frm_Change_Password : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["username"] != null)
            {

                Txtuser.Text = Convert.ToString(Session["username"]);
                Txtpassword.Text = "";

                // Disable menu
                SiteNewMaster master = (SiteNewMaster)this.Master;
                if (master != null && (Session["ForcePassword"] != null && Convert.ToInt16(Session["ForcePassword"]) == 1))
                {
                    master.DisableAllMenus();
                }


            }
        }
    }

    // Session["username"]
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (Session["username"] != null)
        {
            Change_Pwd("Sp_Change_User_Password");
        }
        else
        {
            Change_Pwd("Sp_Change_User_Password");
        }
    }
    public bool InterventionSql_Injection(string RVal)
    {
        SqlInjection objAudit = new SqlInjection();
        bool injection = false;


        injection = objAudit.CheckInputBool(RVal);

        return injection;

    }
    public static List<Control> GetAllControls(List<Control> controls, Type t, Control parent /* can be Page */)
    {
        foreach (Control c in parent.Controls)
        {
            if (c.GetType() == t)
                controls.Add(c);
            if (c.HasControls())
                controls = GetAllControls(controls, t, c);
        }
        return controls;
    }
    public string SetTextBoxFocusSelect(Page page)
    {
        string ALlTestBoxValue = "";
        List<Control> list = new List<Control>();
        list = GetAllControls(list, typeof(TextBox), page);
        foreach (Control ctl in list)
        {
            if (ctl.GetType() == typeof(TextBox))
            {
                ((TextBox)ctl).Attributes.Add("onfocus", "this.select()");
                string TempVari = ((TextBox)ctl).Text;
                if (TempVari.Length > 0)
                {
                    ALlTestBoxValue += TempVari + "  ";
                }
            }
        }
        return ALlTestBoxValue;
    }
    public void Change_Pwd(string porcName)
    {
        string RVal = SetTextBoxFocusSelect(this.Page);

        if (InterventionSql_Injection(RVal))
        {
            ScriptManager.RegisterStartupScript(
                Page,
                GetType(),
                "Message",
                "alert('Spurious input detected. Data rejected');",
                true);
            return;
        }

        try
        {
            if (Session["username"] == null)
            {
                Response.Redirect("Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (string.IsNullOrWhiteSpace(Txtpassword.Text))
            {
                ScriptManager.RegisterStartupScript(
                    Page,
                    GetType(),
                    "Message",
                    "alert('Please enter old password');",
                    true);
                return;
            }

            if (string.IsNullOrWhiteSpace(Txtpasswordnew.Text))
            {
                ScriptManager.RegisterStartupScript(
                    Page,
                    GetType(),
                    "Message",
                    "alert('Please enter new password');",
                    true);
                return;
            }

            string userName = Session["username"].ToString();

            // Get user details
            SqlParameter[] loginParam =
            {
            new SqlParameter("@UserName", userName)
        };

            DataSet ds = SqlHelper.GetDataSet(
                SqlHelper.mainConnectionString,
                CommandType.StoredProcedure,
                "Login_CheckUser",
                loginParam);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(
                    Page,
                    GetType(),
                    "Message",
                    "alert('User not found');",
                    true);
                return;
            }

            string storedPassword = ds.Tables[0].Rows[0]["Password"].ToString();

            // Verify old password
            bool validPassword = Password.VerifyPassword(
                Txtpassword.Text.Trim(),
                storedPassword);

            if (!validPassword)
            {
                ScriptManager.RegisterStartupScript(
                    Page,
                    GetType(),
                    "Message",
                    "alert('Old password is incorrect');",
                    true);
                return;
            }

            // Create hash for new password
            string newPasswordHash = Password.CreatePasswordHash(Txtpasswordnew.Text.Trim());

            SqlParameter[] pr =
            {
            new SqlParameter("@userid", userName),
            new SqlParameter("@NewPwd", newPasswordHash)
        };

            SqlHelper.ExecuteNonQuery(
                SqlHelper.mainConnectionString,
                CommandType.StoredProcedure,
                porcName,
                pr);

            Clear_All();

            Session.Clear();
            Session.Abandon();

            Response.Redirect("Login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {

            System.Diagnostics.Trace.TraceError(ex.ToString());

            ScriptManager.RegisterStartupScript(
                Page,
                GetType(),
                "Message",
                "alert('Unable to change password. Please try again.');",
                true);

        }
    }
    public void Change_PwdOld(string porcName)
    {
        string RVal = SetTextBoxFocusSelect(this.Page);
        if (!InterventionSql_Injection(RVal))
        {
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Spurious input detected. Data rejected')</script>", false);

            return;
        }
        try
        {
            if (Session["username"] != null)
            {
                if (Txtpasswordnew.Text.Trim() != "" && Txtpassword.Text.Trim() != "")
                {
                    string OldPassWord = Password.CreatePasswordHash(Txtpassword.Text.Trim()).ToString();
                    string NewPassWord = Password.CreatePasswordHash(Txtpasswordnew.Text.Trim()).ToString();
                    SqlParameter[] pr = new SqlParameter[] {
                       new SqlParameter("@UserID",Session["username"].ToString()),
                       new SqlParameter("@oldpwd",OldPassWord),
                       new SqlParameter("@NewPwd",NewPassWord),
                    };
                    //int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, porcName, pr);
                    //if (result > 0)
                    int rows = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, porcName, pr);

                    if (rows >= 0)
                    {
                        Clear_All();

                        Session["ForcePassword"] = 0;

                        Session.Clear();
                        Session.Abandon();

                        ScriptManager.RegisterStartupScript(
                            this,
                            GetType(),
                            "PwdChanged",
                            "alert('Password changed successfully. Please login again.');window.location='Login.aspx';",
                            true);
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Password not changed')</script>", false);

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Password')</script>", false);

                }
            }
            else
            {

            }
        }
        catch
        {
            throw;
        }

    }

    public void Clear_All()
    {
        Txtpassword.Text = "";
        TxtPasswordconfirm.Text = "";
        Txtpasswordnew.Text = "";

    }

}