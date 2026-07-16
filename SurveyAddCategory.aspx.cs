using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SurveyAddCategory : System.Web.UI.Page
{
    SqlConnection mycon = new SqlConnection(SqlHelper.mainConnectionString);
    Comman objComman = new Comman();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Convert.ToString(Session["username"]) != "")
        {
            if (!IsPostBack)
            {
                LoadYear();
                FillDropdown();
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //  FillFormName();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }


    }

    public void LoadYear()
    {
        DataTable dtYear = Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, "", "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
    }
    public DataTable Generate_Financial_Year()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("Type");
        DataRow dr;
        int stYr = DateTime.Today.Month < 4 ? DateTime.Today.Year : DateTime.Today.Year + 1;
        for (int i = stYr; i > 2023; i--)
        {
            dr = dt.NewRow();
            dr[0] = (i - 1).ToString();
            dr[1] = (i - 1).ToString() + "-" + (i).ToString();
            dt.Rows.Add(dr);
        }
        dt.AcceptChanges();
        return dt;
    }
    public void FillFormName()
    {
        //DataTable dt = new DataTable();
        //dt = objBLL.Exec_Procedure("GetTenFormEvalTables");
        //foreach (DataRow row in dt.Rows)
        //{
        //    if (row["TableName"].ToString().Contains('_'))
        //        row["TableName"] = "Table" + row["TableName"].ToString().Substring(row["TableName"].ToString().IndexOf('_'));
        //    else
        //        row["TableName"] = "MainTable";
        //}
        //dt.AcceptChanges();
        //GV_AllTenFormEvalTable.DataSource = dt;
        //GV_AllTenFormEvalTable.DataBind();
    }
    protected void ddlLearning_SelectedIndexChanged(object sender, EventArgs e)
    {
        int FormLevel = Int32.Parse(ddlLevel.SelectedValue.ToString());
        if (FormLevel == 1)
        {
            LoadOutComeSpicify();
        }
        FillFormName(FormLevel);
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["FinYear"].ToString() == ddlYear.SelectedItem.Text)
        {
            LnkFormNameSave.Visible = true;
        }
        else
        {
            LnkFormNameSave.Visible = false;
        }
        GVFormName.DataSource = null;
        GVFormName.DataBind();
        ddlLevel.SelectedIndex = 0;
    }
    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        int FormLevel = Int32.Parse(ddlLevel.SelectedValue.ToString());
        LnkFormNameSave.Text = "<span class=\"glyphicon glyphicon-floppy-disk\"></span> Save";
        txtFormName.Text = "";
        HFCopyFormNameId.Value = "0";
        Label2.Text = "";
        d1.Visible = false;
        d2.Visible = false;
        if (FormLevel == 1)
        {
            d1.Visible = true;
            d2.Visible = true;
            LoadOutCome();
            Label2.Text = "Specific Training Name :";

        }
        if (FormLevel == 2)
        {
            Label2.Text = "Training Outcome :";

            d1.Visible = false;
            d2.Visible = true;

            Filllearning();
        }
        if (FormLevel == 3)
        {


            d1.Visible = false;
            d2.Visible = false;
            txtFormName.Text = ddlLevel.SelectedItem.Text;
        }
        FillFormName(FormLevel);
    }

    protected void ddlTraingOutcome_SelectedIndexChanged(object sender, EventArgs e)
    {
        int FormLevel = Int32.Parse(ddlLevel.SelectedValue.ToString());
        if (FormLevel == 1)
        {
            txtFormName.Text = ddlTraingOutcome.SelectedItem.Text;
        }
        if (FormLevel == 2)
        {
            txtFormName.Text = ddlTraingOutcome.SelectedItem.Text;
        }

        FillFormName(FormLevel);
    }

    public void Filllearning()
    {


        string conditions = "  ISNULL(TrainingStatus,0)=1 ";
        objComman.BindDLL("mstlearning", "learningID,dbo.TitleCase(upper(learningName)) as learningName ", conditions, "learningName", "asc", ddlTraingOutcome, "learningName", "learningID", "--Select--");



    }
    public void LoadOutCome()
    {
        string conditions = "  ActiveStatus=1";

        objComman.BindDLL("mstOutcome", "OutcomeID,OutcomeName ", conditions, "OutcomeName", "asc", ddlLearning, "OutcomeName", "OutcomeID", "--Select--");

        ddlLearning.SelectedIndex = 0;


    }
    public void LoadOutComeSpicify()
    {
        string conditions = " ";

        objComman.BindDLL("mstOutcomeSpecific", "sOutcomeID,sOutcomeName ", "OutcomeID=" + ddlLearning.SelectedValue + " and ActiveStatus=1", "sOutcomeID", "asc", ddlTraingOutcome, "sOutcomeName", "sOutcomeID", "--Select--");

        ddlTraingOutcome.SelectedIndex = 0;


    }
    private void FillDropdown()
    {
        //  DataTable dt = objBLL.Exec_CommonProc("USP_TableCountCompare");//Exec_CommonProc("USP_TableCountCompare");
        DataTable dt1 = Exec_Procedure("USP_GetLevel");
        ddlLevel.DataSource = dt1;
        ddlLevel.DataValueField = "id";
        ddlLevel.DataTextField = "Value";
        ddlLevel.DataBind();
        ddlLevel.Items.Insert(0, new System.Web.UI.WebControls.ListItem("---Select Level---", "0"));

        //ddlsurveytype.DataSource = dt;
        //ddlsurveytype.DataValueField = "id";
        //ddlsurveytype.DataTextField = "Value";
        //ddlsurveytype.DataBind();
        //ddlsurveytype.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" ---Select--- ", "0"));

    }
    public DataTable Exec_Procedure(string ProcedureName)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
                    {

                    };
            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, ProcedureName, paramvT);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch (Exception)
        {

        }
        return dtcombo;
    }



    protected void ddlDataBound(object sender, EventArgs e)
    {
        DropDownList list = sender as DropDownList;
        if (list != null)
            list.Items.Insert(0, new ListItem("------Select-------", "0"));

    }

    public void FillFormName(int FormLevel)
    {
        DataTable dt = new DataTable();
        //int FormLevel;
        int T1 = 0;
        int T2 = 0;
        if (FormLevel == 1)
        {
            if (ddlTraingOutcome.SelectedIndex > 0)
            {
                T1 = Convert.ToInt32(ddlTraingOutcome.SelectedValue);
            }
            if (ddlLearning.SelectedIndex > 0)
            {
                T2 = Convert.ToInt32(ddlLearning.SelectedValue);
            }
        }
        if (FormLevel == 2)
        {
            if (ddlTraingOutcome.SelectedIndex > 0)
            {
                T1 = Convert.ToInt32(ddlTraingOutcome.SelectedValue);
            }
        }

        if (FormLevel != 0 || FormLevel != -1)
        {
            //dt = objBLL.Get_DataFor1Filter()
            dt = GetFormTableDetails(FormLevel, T1, T2);
        }
        else
        {
            //dt = objBLL.Select_All_Data("MSTForm", "FormLevel,FormID,FormName", "IsDeleted = 0 and FormLevel = " + FormLevel + " ", "", "");
        }

        GVFormName.DataSource = dt;
        GVFormName.DataBind();

    }

    public DataTable GetFormTableDetails(int FormLevel, int T1, int T2)
    {
        DataTable dtBSL = new DataTable();
        dtBSL = null;
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                         new SqlParameter("@FormLevel",FormLevel),
                          new SqlParameter("@T1",T1),
                           new SqlParameter("@T2",T2),
                             new SqlParameter("@fyear",ddlYear.SelectedItem.Text ),
                    };
            DataTable ds = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Form_Table_Deatils2024", paramvT);
            dtBSL = ds;
        }
        catch
        { DataTable ds = new DataTable(); ds = null; return ds; }
        return dtBSL;
    }
    public int FormNameInsertUpdate(int FormID, int Level, string FormName, string sTran_Type, int UserID, string tablename, string Flag, int Toutcome, int Tsoutcome)
    {

        string Dateof = txtDate.Text;
        string[] b = Dateof.Split('/');

        string FcDate = b[2] + '-' + b[1] + '-' + b[0];

        SqlCommand dbSqlCommand;
        using (dbSqlCommand = new SqlCommand())
            dbSqlCommand.Connection = mycon;
        if (mycon.State == ConnectionState.Closed)
            mycon.Open();
        dbSqlCommand.CommandType = CommandType.StoredProcedure;
        dbSqlCommand.CommandText = "USP_FormNameInsertUpdate2024";
        dbSqlCommand.Parameters.Add("@FormID", SqlDbType.Int).Value = FormID;
        dbSqlCommand.Parameters.Add("@Level", SqlDbType.VarChar).Value = Level;
        dbSqlCommand.Parameters.Add("@FormName", SqlDbType.NVarChar).Value = FormName;
        dbSqlCommand.Parameters.Add("@Tran_Type", SqlDbType.VarChar).Value = sTran_Type;
        dbSqlCommand.Parameters.Add("@UserID", SqlDbType.VarChar).Value = UserID;
        dbSqlCommand.Parameters.Add("@Tablename", SqlDbType.VarChar).Value = tablename;
        dbSqlCommand.Parameters.Add("@Filter2", SqlDbType.VarChar).Value = Flag;
        dbSqlCommand.Parameters.Add("@LastDate", SqlDbType.Date).Value = Convert.ToDateTime(FcDate);
        dbSqlCommand.Parameters.Add("@Toutcome", SqlDbType.Int).Value = Toutcome;
        dbSqlCommand.Parameters.Add("@Tsoutcome", SqlDbType.Int).Value = Tsoutcome;
        dbSqlCommand.Parameters.Add("@Eyear", SqlDbType.VarChar).Value = Convert.ToString(Session["FinYear"]);
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
    protected void LnkFormNameSave_Click(object sender, EventArgs e)
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
        string table = string.Empty;
        int FormID = 0, Level;
        string FormName;

        int UserID = Session["UserID"] == null ? 0 : Convert.ToInt32(Session["UserID"]);

        Level = Int32.Parse(ddlLevel.SelectedValue);
        FormName = txtFormName.Text;
        int Toutcome = 0, Tsoutcome = 0;


        FormID = Int32.Parse(HFCopyFormNameId.Value);
        Level = Int32.Parse(ddlLevel.SelectedValue);


        if (Level == 1)
        {
            Toutcome = Int32.Parse(ddlTraingOutcome.SelectedValue);
            Tsoutcome = Int32.Parse(ddlLearning.SelectedValue);



        }
        if (Level == 2)
        {
            Toutcome = Int32.Parse(ddlTraingOutcome.SelectedValue);
        }

        int status = 0;
        if (UserID != 0)
        {

            table = hdnfdtableid.Value.ToString();
            if (LnkFormNameSave.Text == "<span class=\"glyphicon glyphicon-floppy-disk\"></span> Save")
            {
                status = FormNameInsertUpdate(FormID, Level, FormName, "I", UserID, table, "M", Toutcome, Tsoutcome);
            }
            else if (LnkFormNameSave.Text == "<span class=\"glyphicon glyphicon-trash\"></span> Delete")
            {
                FormID = Int32.Parse(HFFormNameID.Value);
                status = FormNameInsertUpdate(FormID, Level, FormName, "D", UserID, table, "M", Toutcome, Tsoutcome);
            }
            else
            {
                FormID = Int32.Parse(HFFormNameID.Value);
                status = FormNameInsertUpdate(FormID, Level, FormName, "U", UserID, table, "M", Toutcome, Tsoutcome);
            }


        }
        else
        {
            Response.Redirect("Login.aspx");
        }
        if (status == 1)
        {
            showMessages("Added successfully");
            FillFormName(Level);
        }
        else if (status == 2)
        {
            showMessages("Updated successfully");
            FillFormName(Level);
        }
        else if (status == 3)
        {
            showMessages(" Survey Name is Already Present in this Project at this Level ");
            FillFormName(Level);
        }
        else if (status == 4)
        {
            showMessages(" Dleted successfully ");
            FillFormName(Level);
        }

        else
        {
            showMessages("Something went wrong ! ");
            FillFormName(Level);
        }
        LnkFormNameSave.Text = "<span class=\"glyphicon glyphicon-floppy-disk\"></span> Save";
        //  ddlLevel.SelectedIndex = -1;
        ddlsurveytype.SelectedIndex = -1;
        txtFormName.Text = "";
        txttablename.Text = "";
        lbltablename.Text = "";
        ddlsurveytype.Enabled = true;
        txttablename.Enabled = true;
        lbltablename.Enabled = true;
    }
    protected void EditFormName_Click(object sender, EventArgs e)
    {
        LinkButton Edit_Question = sender as LinkButton;
        GridViewRow row = Edit_Question.NamingContainer as GridViewRow;
        int index = row.RowIndex;
        LnkFormNameSave.Text = "<span class=\"glyphicon glyphicon-floppy-disk\"></span> Update";
        HFFormNameID.Value = GVFormName.DataKeys[index].Values["FormID"].ToString();
        txtFormName.Text = GVFormName.DataKeys[index].Values["FormName"].ToString();
        //txtDate.Text = GVFormName.DataKeys[index].Values["LastDate"].ToString();
        ddlLevel.SelectedValue = GVFormName.DataKeys[index].Values["FormLevel"].ToString();
        ddlLearning_SelectedIndexChanged(ddlLevel, null);
        if (Convert.ToInt32(ddlLevel.SelectedValue) == 1)
        {
            ddlLearning.SelectedValue = GVFormName.DataKeys[index].Values["StaffTraingOutcome"].ToString();
            ddlLearning_SelectedIndexChanged(ddlLearning, null);
            ddlTraingOutcome.SelectedValue = GVFormName.DataKeys[index].Values["TrainingOutcome"].ToString();
        }
        if (Convert.ToInt32(ddlLevel.SelectedValue) == 2)
        {
            ddlTraingOutcome.SelectedValue = GVFormName.DataKeys[index].Values["TrainingOutcome"].ToString();
        }
        //string TableName = GVFormName.DataKeys[index].Values["FormEvaluationTableName"].ToString();
        //if (TableName.Contains('_'))
        //{
        //    hdnfdtablename.Value = TableName;
        //    lbltablename.Text = "Table_" + hdnfdtablename.Value.Substring(hdnfdtablename.Value.IndexOf('_'));
        //    ddlsurveytype.SelectedIndex = 1;
        //}
        //else
        //{
        //    lbltablename.Text = TableName;
        //    ddlsurveytype.SelectedIndex = 2;
        //}
        ddlsurveytype.Enabled = false;
        lbltablename.Enabled = false;
        txttablename.Enabled = false;
    }
    protected void DeleteFormName_Click(object sender, EventArgs e)
    {
        LinkButton Edit_Question = sender as LinkButton;
        GridViewRow row = Edit_Question.NamingContainer as GridViewRow;
        int index = row.RowIndex;
        LnkFormNameSave.Text = "<span class=\"glyphicon glyphicon-trash\"></span> Delete";
        HFFormNameID.Value = GVFormName.DataKeys[index].Values["FormID"].ToString();
        txtFormName.Text = GVFormName.DataKeys[index].Values["FormName"].ToString();
        ddlLevel.SelectedValue = GVFormName.DataKeys[index].Values["FormLevel"].ToString();
        string TableName = GVFormName.DataKeys[index].Values["FormEvaluationTableName"].ToString();
        if (TableName.Contains('_'))
        {
            hdnfdtablename.Value = TableName;
            lbltablename.Text = "Table_" + hdnfdtablename.Value.Substring(hdnfdtablename.Value.IndexOf('_'));
            ddlsurveytype.SelectedIndex = 1;
        }
        else
        {
            lbltablename.Text = TableName;
            ddlsurveytype.SelectedIndex = 2;
        }
        ddlsurveytype.Enabled = false;
        lbltablename.Enabled = false;
        txttablename.Enabled = false;
    }
    private void showMessages(string messages)
    {
        lbl_messages.Text = "";
        lbl_messages.Text = messages;
        ModalAlert.Show();
    }
    protected void CopyFormName_Click(object sender, EventArgs e)
    {
        LinkButton CopyFormName = sender as LinkButton;
        GridViewRow row = CopyFormName.NamingContainer as GridViewRow;
        int index = row.RowIndex;
        txtNewformName.Text = GVFormName.DataKeys[index].Values["FormName"].ToString() + "-New";
        HFCopyFormNameId.Value = GVFormName.DataKeys[index].Values["FormID"].ToString();
        MPPreview.Show();
        // FormLevel,FormID,FormName
    }
    protected void LnkBtnSaveNew_Click(object sender, EventArgs e)
    {
        int status;

        int FormID, Level, Toutcome = 0, Tsoutcome = 0;
        string FormName;

        FormName = txtNewformName.Text;
        FormID = Int32.Parse(HFCopyFormNameId.Value);
        Level = Int32.Parse(ddlLevel.SelectedValue);



        /// status = objBLL.CopyFormNameInsert(FormID, Level, FormName, "I");

        //if (status == 1)
        //{
        //    showMessages("Added successfully");
        //    FillFormName(Convert.ToInt32(ddlLevel.SelectedValue));
        //}
        //else if (status == 3)
        //{
        //    showMessages("Survey Name is Already Present in this Project at this Level ");
        //    FillFormName(Convert.ToInt32(ddlLevel.SelectedValue));
        //}
        //else
        //{
        //    showMessages("Something went wrong ! Try Again ");
        //    FillFormName(Convert.ToInt32(ddlLevel.SelectedValue));
        //}

    }

    protected void lnkbtnselecttable_Click(object sender, EventArgs e)
    {
        LinkButton SelectTable = sender as LinkButton;
        GridViewRow row = SelectTable.NamingContainer as GridViewRow;
        int index = row.RowIndex;
        string ID = GV_AllTenFormEvalTable.DataKeys[index].Values["ID"].ToString();
        string TableID = GV_AllTenFormEvalTable.DataKeys[index].Values["Tablename"].ToString();
        lbltablename.Text = TableID;
        hdnfdtableid.Value = ID;
    }

    //public bool IsEnglish(string inputstring)
    //{
    //    Regex regex = new Regex(@"[A-Za-z0-9 .,-=+(){}\[\]\\]");
    //    MatchCollection matches = regex.Matches(inputstring);

    //    if (matches.Count.Equals(inputstring.Length))
    //        return true;
    //    else
    //        return false;
    //}


}