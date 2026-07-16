using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frm_Change_PasswordAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["username"] != null)
            {

                fillrole();
                //fillemployee();
            }
        }
    }
    public void fillrole()
    {

        string cond = "Role_Level not in(1)";


        if (Session["user_level"].ToString() == "79")
        {
            cond = "Role_Level=24  ";

        }








        DataTable dtrole = Select_All_Data("mstuserrole", "*", cond, "Role_id", "");
        if (dtrole.Rows.Count > 0)
        {
            ddlRole.DataSource = dtrole;
            ddlRole.DataTextField = "Role";
            ddlRole.DataValueField = "Role_Level";
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }



    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        FillGridNew();
    }
    public void FillGridNew()
    {
        try
        {
            string conditions = "where 1=1";

            if (ddlType.SelectedIndex > 0)
            {
                if (Convert.ToInt32(ddlType.SelectedValue) == 1)
                {
                    conditions = conditions + " and UserName like '" + txtSearchUser.Text + "%'";
                }
                if (Convert.ToInt32(ddlType.SelectedValue) == 2)
                {
                    conditions = conditions + " and FristName like '" + txtSearchUser.Text + "%'";
                }
            }
            if (ddlRole.SelectedIndex > 0)
            {
                conditions = conditions + " and UserLevel = " + ddlRole.SelectedValue + " ";
            }
            DataTable dtstate = Select_All_DataNew("MstUser", "UserName,FristName + ' (' + UserName +')' as [Name]", conditions, "FristName", "");
            if (dtstate.Rows.Count > 0)
            {
                ddlemployee.DataSource = dtstate;
                ddlemployee.DataTextField = "Name";
                ddlemployee.DataValueField = "UserName";
                ddlemployee.DataBind();
                ddlemployee.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
            }
        }
        catch (Exception)
        {

            throw;
        }

    }
    public DataTable Select_All_DataNew(string TableName, string TFieldName, string Condition, string OrderbyCondition, string Sortcondition)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            string WConditions = Condition.Length > 0 ? "  " + Condition : "";
            string OrderbyvalueMem = OrderbyCondition.Length > 0 ? " order by " + OrderbyCondition + "  " : "";
            string sortbycondi = Sortcondition.Length > 0 ? "" + Sortcondition : "";
            string FieldName = TFieldName.Length > 0 ? TFieldName : "";
            SqlParameter[] paramv = new SqlParameter[]
                    {
                            new SqlParameter("@TableName",TableName),
                            new SqlParameter("@Condition",WConditions),
                            new SqlParameter("@OrderbyvalueMem",OrderbyvalueMem),
                            new SqlParameter("@sortbycondi",sortbycondi),
                            new SqlParameter("@FieldName",FieldName),

                    };

            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Select_AllTableData", paramv);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch
        {
            //string mmsg = ex.Message; showMessages(mmsg);
            //showMessages("(SelectAllData)  " + mmsg);
        }
        return dtcombo;
    }

    public void fillemployee()
    {


        string Cond = " 1=1 and  UserName <> ''";
        DataTable dtstate = Select_All_Data("MstUser", "UserName,FristName + ' (' + UserName +')' as [Name]", Cond, "FristName", "");
        if (dtstate.Rows.Count > 0)
        {
            ddlemployee.DataSource = dtstate;
            ddlemployee.DataTextField = "Name";
            ddlemployee.DataValueField = "UserName";
            ddlemployee.DataBind();
            ddlemployee.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
    }
    public DataTable Select_All_Data(string TableName, string TFieldName, string Condition, string OrderbyCondition, string Sortcondition)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            string WConditions = Condition.Length > 0 ? " where " + Condition : "";
            string OrderbyvalueMem = OrderbyCondition.Length > 0 ? " order by " + OrderbyCondition + "  " : "";
            string sortbycondi = Sortcondition.Length > 0 ? "" + Sortcondition : "";
            string FieldName = TFieldName.Length > 0 ? TFieldName : "";
            SqlParameter[] paramv = new SqlParameter[]
                    {
                            new SqlParameter("@TableName",TableName),
                            new SqlParameter("@Condition",WConditions),
                            new SqlParameter("@OrderbyvalueMem",OrderbyvalueMem),
                            new SqlParameter("@sortbycondi",sortbycondi),
                            new SqlParameter("@FieldName",FieldName),

                    };

            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Select_AllTableData", paramv);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch
        {
            //string mmsg = ex.Message; showMessages(mmsg);
            //showMessages("(SelectAllData)  " + mmsg);
        }
        return dtcombo;
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
    // Session["username"]
    protected void btn_Save_Click(object sender, EventArgs e)
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

        if (ddlemployee.SelectedIndex <= 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select User Name')</script>", false);
            return;
        }
        Change_Pwd("Sp_Change_User_PasswordNew2023");

    }

    public void Change_Pwd(string porcName)
    {
        try
        {
            if (Session["username"] != null)
            {
                if (Txtpasswordnew.Text.Trim() != "")
                {
                    //string OldPassWord=Password.CreatePasswordHash("").ToString();
                    string NewPassWord = Password.CreatePasswordHash(Txtpasswordnew.Text.Trim()).ToString();
                    SqlParameter[] pr = new SqlParameter[] {
                       new SqlParameter("@UserID",ddlemployee.SelectedValue),

                       new SqlParameter("@NewPwd",NewPassWord),

                          new SqlParameter("@ActivemodifyBy",Session["username"].ToString()),
            };
                    int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, porcName, pr);
                    if (result > 0)
                    {
                        Clear_All();
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Password changed successfully')</script>", false);


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
        }

    }

    public void Clear_All()
    {

        TxtPasswordconfirm.Text = "";
        Txtpasswordnew.Text = "";

    }

}