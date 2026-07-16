using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class FrmAddMasterCommon : System.Web.UI.Page
{
    SqlConnection mycon = new SqlConnection(SqlHelper.mainConnectionString);
    static string prevPage = String.Empty;
    static int EssionFormID = 0;
    static string EssionFormIDName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.UrlReferrer != null)
            {
                string[] strArr = null;
                prevPage = Request.UrlReferrer.ToString();
                char[] splitchar = { '/' };
                strArr = prevPage.Split(splitchar);
                int lengthOfArr = strArr.Length;
                string PageComingFrom = strArr[lengthOfArr - 1].ToString();
                if (PageComingFrom == "SurveyQuestion.aspx" || PageComingFrom == "Define_Questions")
                {
                    if (Session["FormID"] != null)
                    {
                        FillFormName();
                        EssionFormID = Int32.Parse(Session["FormID"].ToString());
                        DDLFormName.SelectedValue = EssionFormID.ToString();

                        GoBackToQuesionForm.Visible = true;
                        EssionFormIDName = Session["FormIDName"].ToString();
                        lblheading.Text = "Creating Choices for " + EssionFormIDName;
                        FillFlagMaster(EssionFormID);
                    }
                    else
                    {
                        EssionFormID = 0;
                        DDLFormName.SelectedIndex = -1;
                    }
                }
                else
                {
                    GoBackToQuesionForm.Visible = false;
                    EssionFormID = 0;
                    EssionFormIDName = "";
                    lblheading.Text = "";
                }

            }

            if (Session["FormID"] != null)
            {
                FillFormName();
                EssionFormID = Int32.Parse(Session["FormID"].ToString());
                DDLFormName.SelectedValue = EssionFormID.ToString();

                GoBackToQuesionForm.Visible = true;
                EssionFormIDName = Session["FormIDName"].ToString();
                lblheading.Text = "Creating Choices for " + EssionFormIDName;
                FillFlagMaster(EssionFormID);
            }
            else
            {
                EssionFormID = 0;
                DDLFormName.SelectedIndex = -1;
            }
            FillFlagMaster(Int32.Parse(Session["FormID"].ToString()));
        }
    }
    public void FillFlagMaster(int FormID)
    {
        DataTable dt = new DataTable();
        // (string TableName, string TFieldName, string Condition, string OrderbyCondition, string Sortcondition, string database)       
        dt = Select_All_Data("MSTCommon", "UID,ID,Value", "IsDeleted = 0 and FormID>0 and mYear='" + Convert.ToString(Session["FinYear"]) + "' and Sequence = 0 ", "UID desc", "");
        GVFlagMaster.DataSource = dt;
        GVFlagMaster.DataBind();
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
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                            new SqlParameter("@TableName",TableName),
                            new SqlParameter("@Condition",WConditions),
                            new SqlParameter("@OrderbyvalueMem",OrderbyvalueMem),
                            new SqlParameter("@sortbycondi",sortbycondi),
                            new SqlParameter("@FieldName",FieldName)
                    };


            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Select_AllTableData", paramvT);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch (Exception)
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
    protected void Define_Options_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["FormID"]) != "")
        { }
        else
        {
            Response.Redirect("Login.aspx");
        }
        LinkButton Define_Options = sender as LinkButton;
        GridViewRow row = Define_Options.NamingContainer as GridViewRow;
        int index = row.RowIndex;

        int Flag = Convert.ToInt32(GVFlagMaster.DataKeys[index].Values["ID"].ToString());
        string Flagname = GVFlagMaster.DataKeys[index].Values["Value"].ToString();
        lblHeadingTwo.Text = "Options For : " + Flagname;
        HFFlagValue.Value = Flag.ToString();
        FillFlagMasterValue(Flag, EssionFormID);

        int UID = Int32.Parse(GVFlagMaster.DataKeys[index].Values["UID"].ToString());
        clsMain objMain = new clsMain();
        DataTable dtTb = objMain.LoadData(" select * from Tbl_Training_Ques where QuestionID in (SELECT QuestionID FROM [MSTFormQuestion] where QuestionDate>='2026-04-01' and FlagSqn=" + UID + " )  and FormID in( select Tarining_ID from tbl_training_question where createdate>='2026-04-01')");

        if (dtTb.Rows.Count > 0)
        {
            showMessages("You can not  Edit because Flag link in  training ");
            GVFlagMasterValue.Columns[3].Visible = false;
            GVFlagMasterValue.Columns[4].Visible = false;
        }
        else
        {
            GVFlagMasterValue.Columns[3].Visible = true;
            GVFlagMasterValue.Columns[4].Visible = true;
        }


    }

    public void FillFlagMasterValue(int Flag, int FormID)
    {
        DataTable dt = new DataTable();
        // (string TableName, string TFieldName, string Condition, string OrderbyCondition, string Sortcondition, string database)       
        dt = Select_All_Data("MSTCommon", "UID,ID,Value,Score", "IsDeleted = 0 and FormID>0 and Flag = " + Flag + " ", "ID", "");

        if (dt.Rows.Count > 0)
        {

            GVFlagMasterValue.DataSource = dt;
            GVFlagMasterValue.DataBind();
        }
        else
        {
            GVFlagMasterValue.DataSource = Lang_Temp_Table();
            GVFlagMasterValue.DataBind();
        }

    }

    public DataTable Lang_Temp_Table()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("UID");
        dt.Columns.Add("ID");
        dt.Columns.Add("Value");
        dt.Columns.Add("Score");
        DataRow dr = dt.NewRow();
        dr["UID"] = "0";
        dt.Rows.Add(dr);
        dt.AcceptChanges();
        return dt;
    }

    public int MasterOptionInsert(int optionidId, string OptionValue, int formid, string sTran_Type)
    {
        SqlCommand dbSqlCommand;
        using (dbSqlCommand = new SqlCommand())
            dbSqlCommand.Connection = mycon;
        if (mycon.State == ConnectionState.Closed)
            mycon.Open();
        dbSqlCommand.CommandType = CommandType.StoredProcedure;
        dbSqlCommand.CommandText = "USP_MasterOptionInsert20242025";
        dbSqlCommand.Parameters.Add("@UID", SqlDbType.Int).Value = optionidId;
        dbSqlCommand.Parameters.Add("@OptionValue", SqlDbType.NVarChar).Value = OptionValue;
        dbSqlCommand.Parameters.Add("@formid", SqlDbType.VarChar).Value = formid;

        dbSqlCommand.Parameters.Add("@Tran_Type", SqlDbType.VarChar).Value = sTran_Type;

        dbSqlCommand.Parameters.Add("@mYear", SqlDbType.VarChar).Value = Convert.ToString(Session["FinYear"]);
        dbSqlCommand.Parameters.Add("@UserName", SqlDbType.VarChar).Value = Convert.ToString(Session["UserName"]);
        System.Data.SqlClient.SqlParameter pRowsAffected = new SqlParameter("@output", System.Data.SqlDbType.Int);
        pRowsAffected.Direction = System.Data.ParameterDirection.Output;
        dbSqlCommand.Parameters.Add(pRowsAffected);
        try
        {
            dbSqlCommand.ExecuteNonQuery();
        }
        catch
        {
            return -1;
        }
        return Convert.ToInt32(pRowsAffected.Value);
    }



    protected void Save_Category_click(object sender, EventArgs e)
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
        if (Convert.ToString(Session["FormID"]) != "")
        { }
        else
        {
            Response.Redirect("Login.aspx");
        }
        LinkButton Save_Category = sender as LinkButton;
        GridViewRow row = Save_Category.NamingContainer as GridViewRow;
        int index = row.RowIndex;

        TextBox txtOptionValue = GVFlagMaster.FooterRow.FindControl("txtOptionValue") as TextBox;

        int formid, UID = 0;

        formid = EssionFormID;

        if (txtOptionValue.Text.Trim() != "")
        {

            int status = MasterOptionInsert(UID, txtOptionValue.Text, formid, "I");

            if (status == 1)
            {
                showMessages("Added successfully");
                FillFlagMaster(EssionFormID);
            }
            else if (status == 2)
            {
                showMessages("Updated successfully");
                FillFlagMaster(EssionFormID);
            }
            else
            {
                showMessages("Some thing went wrong ! Try Again ");
                FillFlagMaster(EssionFormID);
            }
        }

    }
    public int MasterOptionValueInsert(int UID, string OptionValue, int flag, int formid, string sTran_Type, int Score)
    {
        SqlCommand dbSqlCommand;
        using (dbSqlCommand = new SqlCommand())
            dbSqlCommand.Connection = mycon;
        if (mycon.State == ConnectionState.Closed)
            mycon.Open();
        dbSqlCommand.CommandType = CommandType.StoredProcedure;
        dbSqlCommand.CommandText = "USP_MasterOptionValueInsertNew20242025";
        dbSqlCommand.Parameters.Add("@UID", SqlDbType.Int).Value = UID;
        dbSqlCommand.Parameters.Add("@OptionValue", SqlDbType.NVarChar).Value = OptionValue;

        dbSqlCommand.Parameters.Add("@flag", SqlDbType.Int).Value = flag;
        dbSqlCommand.Parameters.Add("@formid", SqlDbType.VarChar).Value = formid;

        dbSqlCommand.Parameters.Add("@Tran_Type", SqlDbType.VarChar).Value = sTran_Type;
        dbSqlCommand.Parameters.Add("@Score", SqlDbType.Int).Value = Score;
        dbSqlCommand.Parameters.Add("@mYear", SqlDbType.VarChar).Value = Convert.ToString(Session["FinYear"]);
        dbSqlCommand.Parameters.Add("@UserName", SqlDbType.VarChar).Value = Convert.ToString(Session["UserName"]);
        System.Data.SqlClient.SqlParameter pRowsAffected = new SqlParameter("@output", System.Data.SqlDbType.Int);
        pRowsAffected.Direction = System.Data.ParameterDirection.Output;
        dbSqlCommand.Parameters.Add(pRowsAffected);
        try
        {
            dbSqlCommand.ExecuteNonQuery();
        }
        catch
        {
            return -1;
        }
        return Convert.ToInt32(pRowsAffected.Value);
    }
    protected void Save_Options_click(object sender, EventArgs e)
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
        if (Convert.ToString(Session["FormID"]) != "")
        { }
        else
        {
            Response.Redirect("Login.aspx");
        }
        //LinkButton Save_Options = sender as LinkButton;
        //GridViewRow row = Save_Options.NamingContainer as GridViewRow;
        //int index = row.RowIndex;
        int UID = 0, flag, Score = 0, formid = 0;

        string FlagMasterValue = txtFlagMasterValue.Text;



        if (txtScore.Text.Trim() != "")
        {
            Score = Convert.ToInt32(txtScore.Text);
        }
        //    if (txtFlagMasterValue.Text.Trim() == "")
        //{
        //    showMessages("Enter Value");
        //    return;
        //}
        //else
        //{
        //    showMessages("The ',' Will be conisederd as Delimiter an the value will go in next Line");
        //}

        formid = EssionFormID;
        flag = Int32.Parse(HFFlagValue.Value);

        int status = MasterOptionValueInsert(UID, FlagMasterValue, flag, formid, "I", Score);

        if (status == 1)
        {
            showMessages("Added successfully");
            FillFlagMasterValue(flag, formid);
            txtFlagMasterValue.Text = "";
            txtScore.Text = "";
        }
        else if (status == 2)
        {
            showMessages("Updated successfully");
            FillFlagMasterValue(flag, formid);
            txtFlagMasterValue.Text = "";
            txtScore.Text = "";
        }
        else if (status == 3)
        {
            showMessages("Already Exit");
            FillFlagMasterValue(flag, formid);
            txtFlagMasterValue.Text = "";
            txtScore.Text = "";
        }
        else
        {
            showMessages("Some thing went wrong ! Try Again ");
            FillFlagMasterValue(flag, formid);
            txtFlagMasterValue.Text = "";
            txtScore.Text = "";
        }


    }



    protected void LbFlag_Click(object sender, EventArgs e)
    {
        MPEFormName.Show();
        btnFormName.Text = "Save";
        txtFlagName.Text = "";
    }

    protected void ddlDataBound(object sender, EventArgs e)
    {
        DropDownList list = sender as DropDownList;
        if (list != null)
            list.Items.Insert(0, new ListItem("------Select-------", "0"));

    }

    private void showMessages(string messages)
    {
        lbl_messages.Text = "";
        lbl_messages.Text = messages;
        ModalAlert.Show();
    }




    protected void btnFormName_Click(object sender, EventArgs e)
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
        if (Convert.ToString(Session["FormID"]) != "")
        { }
        else
        {
            Response.Redirect("Login.aspx");
        }
        int status = 0, formid, UID = 0;

        formid = Int32.Parse(DDLFormName.SelectedValue.ToString());

        if (btnFormName.Text.Trim() == "Save")
        {

            status = MasterOptionInsert(UID, txtFlagName.Text, formid, "I");
        }
        else if (btnFormName.Text.Trim() == "Update")
        {
            UID = Int32.Parse(HFFormId.Value);
            status = MasterOptionInsert(UID, txtFlagName.Text, formid, "U");
        }

        if (status == 1)
        {
            showMessages("Added successfully");
            FillFlagMaster(EssionFormID);
        }
        else if (status == 2)
        {
            showMessages("Updated successfully");
            FillFlagMaster(EssionFormID);
        }
        else if (status == 3)
        {
            showMessages("Flag Name is already present for this Form");
            FillFlagMaster(EssionFormID);
        }
        else
        {
            showMessages("Some thing went wrong ! Try Again ");
            FillFlagMaster(EssionFormID);
        }
    }

    protected void EditCategory_Click(object sender, EventArgs e)
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
        if (Convert.ToString(Session["FormID"]) != "")
        { }
        else
        {
            Response.Redirect("Login.aspx");
        }
        LinkButton EditCategory = sender as LinkButton;
        GridViewRow row = EditCategory.NamingContainer as GridViewRow;
        int index = row.RowIndex;

        int UID = Int32.Parse(GVFlagMaster.DataKeys[index].Values["UID"].ToString());
        clsMain objMain = new clsMain();
        DataTable dtTb = objMain.LoadData(" select * from Tbl_Training_Ques where QuestionID in (SELECT QuestionID FROM [MSTFormQuestion] where QuestionDate>='2026-04-01' and FlagSqn=" + UID + " )  and FormID in( select Tarining_ID from tbl_training_question where createdate>='2026-04-01')");

        if (dtTb.Rows.Count > 0)
        {
            showMessages("You can not  Edit because Flag link in training ");
        }
        else
        {
            txtFlagName.Text = GVFlagMaster.DataKeys[index].Values["Value"].ToString();
            HFFormId.Value = GVFlagMaster.DataKeys[index].Values["UID"].ToString();
            btnFormName.Text = "Update";
            MPEFormName.Show();
        }
    }

    protected void BtnFlagOption_Click(object sender, EventArgs e)
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
        if (Convert.ToString(Session["FormID"]) != "")
        { }
        else
        {
            Response.Redirect("Login.aspx");
        }
        int UID = 0, flag, formid = 0, Score = 0;

        if (txtEditScore.Text.Trim() != "")
        {
            Score = Convert.ToInt32(txtEditScore.Text);
        }

        if (txtFlagOption.Text.Trim() == "")
        {
            showMessages("Enter Value");
            return;
        }

        formid = EssionFormID;
        flag = Int32.Parse(HFFlagValue.Value);
        UID = Int32.Parse(HFFlagOptionValueUID.Value);
        int status = MasterOptionValueInsert(UID, txtFlagOption.Text, flag, formid, "U", Score);

        if (status == 1)
        {
            showMessages("Added successfully");
            FillFlagMasterValue(flag, formid);
        }
        else if (status == 2)
        {
            showMessages("Updated successfully");
            FillFlagMasterValue(flag, formid);
        }
        else if (status == 3)
        {
            showMessages("Already Exit");
            FillFlagMasterValue(flag, formid);
        }
        else
        {
            showMessages("Some thing went wrong ! Try Again ");
            FillFlagMasterValue(flag, formid);
        }

    }

    protected void EditOptionValue_Click(object sender, EventArgs e)
    {
        LinkButton EditOptionValue = sender as LinkButton;
        GridViewRow row = EditOptionValue.NamingContainer as GridViewRow;
        int index = row.RowIndex;

        HFFlagOptionValueUID.Value = GVFlagMasterValue.DataKeys[index].Values["UID"].ToString();
        txtFlagOption.Text = GVFlagMasterValue.DataKeys[index].Values["Value"].ToString();
        txtEditScore.Text = GVFlagMasterValue.DataKeys[index].Values["Score"].ToString();
        //GVFlagMasterValue
        MPFFlagOption.Show();

    }

    protected void DeleteFlags_Click(object sender, EventArgs e)
    {
        LinkButton EditCategory = sender as LinkButton;
        GridViewRow row = EditCategory.NamingContainer as GridViewRow;
        int index = row.RowIndex;
        int status;
        clsMain objMain = new clsMain();

        int UID = Int32.Parse(GVFlagMaster.DataKeys[index].Values["UID"].ToString());
        DataTable dtTb = objMain.LoadData(" SELECT * FROM [MSTFormQuestion] where QuestionDate>='2026-04-01' and FlagSqn=" + UID + "  ");

        if (dtTb.Rows.Count > 0)
        {
            showMessages("You can not  Deleted because Flag link in Question ");
        }
        else
        {
            status = MasterOptionInsert(UID, "", 0, "D");

            if (status == 3)
            {
                showMessages("Deleted successfully");
                FillFlagMaster(EssionFormID);
            }
            else if (status == 4)
            {
                showMessages("You can not  Deleted because Flag link in Question");
                FillFlagMaster(EssionFormID);
            }
            else
            {
                showMessages("Error !!");
            }
        }


    }
    protected void DeleteOption_Click(object sender, EventArgs e)
    {
        LinkButton EditOptionValue = sender as LinkButton;
        GridViewRow row = EditOptionValue.NamingContainer as GridViewRow;
        int formid = EssionFormID;
        int flag = Int32.Parse(HFFlagValue.Value);
        int index = row.RowIndex;
        int status;

        int UID = Int32.Parse(GVFlagMasterValue.DataKeys[index].Values["UID"].ToString());
        status = MasterOptionInsert(UID, "", 0, "D");
        clsMain objMain = new clsMain();
        DataTable dtTb = objMain.LoadData(" select * from Tbl_Training_Ques where QuestionID in (SELECT QuestionID FROM [MSTFormQuestion] where QuestionDate>='2026-04-01' and FlagSqn=" + UID + " )  and FormID in( select Tarining_ID from tbl_training_question where createdate>='2026-04-01')");

        if (dtTb.Rows.Count > 0)
        {
            showMessages("You can not  Deleted because Flag link in training ");
        }
        else
        {
            if (status == 3)
            {
                showMessages("Deleted successfully");
                FillFlagMasterValue(flag, formid);
            }
            else
            {
                showMessages("Error !!");
            }
        }

    }


    public void FillFormName()
    {
        DataTable dt = new DataTable();

        dt = Get_DataFor3Filter("USP_GetSurveyOnAgencyAndLevel", "0", Session["FormLevel"].ToString(), "0");


        DDLFormName.DataSource = dt;
        DDLFormName.DataTextField = "FormName";
        DDLFormName.DataValueField = "FormID";
        DDLFormName.DataBind();
        DDLFormName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("------Select-------", "0"));




    }
    public DataTable Get_DataFor3Filter(string ProcedureName, string Filter1, string Filter2, string Filter3)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                            new SqlParameter("@Filter1",Filter1),
                            new SqlParameter("@Filter2",Filter2),
                            new SqlParameter("@Filter3",Filter3),


                    };
            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, ProcedureName, paramvT);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch (Exception)
        {

        }
        return dtcombo;
    }
}