using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class ForcePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["username"] != null)
            {

                Txtuser.Text = Convert.ToString(Session["username"]);
                Txtpassword.Text = "";

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
                    int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, porcName, pr);
                    if (result > 0)
                    {
                        string sql = @"
                                        IF EXISTS (SELECT 1 FROM Tbl_ForcePasswrod WHERE UserName = @UserName)
                                        BEGIN
                                            UPDATE Tbl_ForcePasswrod
                                            SET ForcePasswrod = @ForcePasswrod,
                                                LastPasswordChanged = GETDATE()
                                            WHERE UserName = @UserName
                                        END
                                        ELSE
                                        BEGIN
                                            INSERT INTO Tbl_ForcePasswrod
                                            (
                                                ForcePasswrod,
                                                LastPasswordChanged,
                                                UserName
                                            )
                                            VALUES
                                            (
                                                @ForcePasswrod,
                                                GETDATE(),
                                                @UserName
                                            )
                                        END";

                        SqlParameter[] param =
                        {
                            new SqlParameter("@ForcePasswrod", SqlDbType.Int) { Value = 0 },
                            new SqlParameter("@UserName", SqlDbType.VarChar, 50)
                            {
                                Value = Session["username"].ToString()
                            }
                        };

                        SqlHelper.ExecuteNonQuery(
                            SqlHelper.mainConnectionString,
                            CommandType.Text,
                            sql,
                            param);

                        Clear_All();

                        Session.Clear();
                        Session.Abandon();

                        ScriptManager.RegisterStartupScript(
                            Page,
                            GetType(),
                            "Message",
                            "alert('Password changed successfully. Please login again.');window.location='login.aspx';",
                            true);
                    }


                    //if (result > 0)
                    //{
                    //    Clear_All();
                    //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Password changed successfully')</script>", false);


                    //}
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


    protected void btncheckEncryption_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtText.Text))
        {
            encryptedstring.Text = "Please enter text.";
            return;
        }

        if (string.IsNullOrWhiteSpace(txtUniqueChildCode.Text))
        {
            encryptedstring.Text = "Please enter Unique Child Code.";
            return;
        }

        // Get secret key from database
        string textKey = AESEncryption.GetKey(txtUniqueChildCode.Text.Trim());

        if (string.IsNullOrEmpty(textKey))
        {
            encryptedstring.Text = "Secret Key not found.";
            return;
        }

        // Encrypt
        string encryptedText = AESEncryption.Encrypt(
            txtText.Text.Trim(),
            textKey);

        encryptedstring.Text = encryptedText;

        // Decrypt
        string originalText = AESEncryption.Decrypt(
            encryptedText,
            textKey);

        encryptedstring.Text += "<br/><br/>Decrypted : " + originalText;
    }
}