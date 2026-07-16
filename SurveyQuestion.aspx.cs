using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SurveyQuestion : System.Web.UI.Page
{
    SqlConnection mycon = new SqlConnection(SqlHelper.mainConnectionString);
    public static string STRPRINTCONTENT;
    clsMain objMain = new clsMain();
    static string prevPage = String.Empty;
    Comman objComman = new Comman();
    static int EssionFormID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {
            if (!IsPostBack)
            {
                LoadYear();
                FillDropdown();
                FillFormName(0);
                Get_mskvalidation();

                ChnageOfDependentdQues(0);

                divmask.Attributes.Add("style", "display:none;");
                divMaxLenght.Attributes.Add("style", "display:none;");
                txtMaxLenght.Text = "";

                txtquestionno.Enabled = false;
                txtSeq.Enabled = false;
                txtquestion.Enabled = false;
                ddlAnswerTypeID.Enabled = false;
                ddlQuestionType.Enabled = false;
                btnSave.Visible = false;
                btnNew.Visible = false;
                ddlLevel.Focus();
                if (Request.UrlReferrer != null)
                {
                    string[] strArr = null;
                    prevPage = Request.UrlReferrer.ToString();
                    char[] splitchar = { '/' };
                    strArr = prevPage.Split(splitchar);
                    int lengthOfArr = strArr.Length;
                    string PageComingFrom = strArr[lengthOfArr - 1].ToString();
                    if (PageComingFrom == "FrmAddMasterCommon.aspx")
                    {
                        if (Session["FormID"] != null)
                        {



                            ddlLevel.SelectedValue = Session["FormLevel"].ToString();
                            FillFormName(Int32.Parse(Session["FormLevel"].ToString()));
                            EssionFormID = Int32.Parse(Session["FormID"].ToString());

                            ddlQuestionForm.SelectedValue = EssionFormID.ToString();
                            ddlForm.SelectedValue = EssionFormID.ToString();
                            BindGvQuestion(EssionFormID);
                            FillFlagDropDown(EssionFormID);
                            ddlAnswerTypeID.SelectedValue = Session["AnswerTuypeID"].ToString();
                            txtquestionno.Text = Session["SeQuestionNoValue"].ToString();
                            txtquestion.Text = Session["SeQuestionValue"].ToString();
                            txtSeq.Text = Session["SeDisplaySequenceValue"].ToString();
                            FillFlagCategory(EssionFormID);
                            ddlcat.SelectedValue = Session["cat"].ToString();
                            btnSave.Text = "Save";

                            txtquestionno.Enabled = true;
                            txtSeq.Enabled = true;
                            txtquestion.Enabled = true;
                            ddlAnswerTypeID.Enabled = true;
                            ddlQuestionType.Enabled = true;
                            btnSave.Visible = true;
                            btnNew.Visible = true;
                            ddlQuestionType.SelectedValue = Session["QID"].ToString();
                            if (Convert.ToInt32(ddlQuestionType.SelectedValue) == 2)
                            {
                                Q2.Visible = true;
                                Q1.Visible = true;
                                imgMKS.ImageUrl = ResolveUrl("~/Survey/" + clsMain.ImageID);
                            }
                            else
                            {
                                Q2.Visible = false;
                                Q1.Visible = true;
                            }
                            FillFlagDropDownGrop(Convert.ToInt32(ddlForm.SelectedValue));
                        }
                        else
                        {
                            EssionFormID = 0;
                            ddlQuestionForm.SelectedIndex = -1;
                            ddlForm.SelectedIndex = -1;
                        }
                        ddlFlag.Focus();
                    }
                    else
                    {
                        EssionFormID = 0;
                    }
                }


            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }

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
    public void LoadYear()
    {
        DataTable dtYear = Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, "", "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["FinYear"].ToString() == ddlYear.SelectedItem.Text)
        {
            pnlQ.Enabled = true;
            lnkCategory.Enabled = true;
        }
        else
        {
            pnlQ.Enabled = false;
            lnkCategory.Enabled = false;
        }
        GvQuestion.DataSource = null;
        GvQuestion.DataBind();
        ddlLevel.SelectedIndex = 0;
        ddlForm.SelectedIndex = 0;
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlcLevel.SelectedIndex = 0;

        if (ddlLevel.SelectedIndex > 0)
        {
            ddlcLevel.SelectedValue = ddlLevel.SelectedValue;
        }
        if (ddlForm.SelectedIndex > 0)
        {
            ddlcForm.SelectedValue = ddlForm.SelectedValue;
            ddlcLevel_SelectedIndexChanged(ddlcLevel, null);
            ddlcForm_SelectedIndexChanged(ddlcForm, null);
        }



        MPE_Entry.Show();
    }
    public DataTable Exec_ProcedureNew(string ProcedureName, int Flag)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                            new SqlParameter("@Flag", Flag),
                    };
            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, ProcedureName, paramvT);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch (Exception)
        {

        }
        return dtcombo;
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

    private void FillDropdown()
    {
        DataTable dt1 = Exec_Procedure("USP_GetLevel");
        ddlLevel.DataSource = dt1;
        ddlLevel.DataValueField = "id";
        ddlLevel.DataTextField = "Value";
        ddlLevel.DataBind();
        ddlLevel.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" --Select Level-- ", "0"));

        ddlcLevel.DataSource = dt1;
        ddlcLevel.DataValueField = "id";
        ddlcLevel.DataTextField = "Value";
        ddlcLevel.DataBind();
        ddlcLevel.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" --Select Level-- ", "0"));

        DataTable dt2 = Exec_Procedure("USP_GetChildQuestionOptions");
        ddlAnswerTypeID.DataSource = dt2;
        ddlAnswerTypeID.DataValueField = "id";
        ddlAnswerTypeID.DataTextField = "Value";
        ddlAnswerTypeID.DataBind();
        ddlAnswerTypeID.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" Select Question Type ", "0"));
    }

    protected void ddlDataBound(object sender, EventArgs e)
    {
        DropDownList list = sender as DropDownList;
        if (list != null)
            list.Items.Insert(0, new ListItem("------Select-------", "0"));

    }
    public void FillFormNamecat(int FormLevel)
    {
        string UserID = Session["UserID"].ToString();
        DataTable dt = new DataTable();
        //int FormLevel;
        if (FormLevel == 0 || FormLevel == -1)
        {
            //  dt = objBLL.Select_All_Data("MSTForm", "FormID,FormName", "IsDeleted = 0", "", "");
        }
        else
        {
            dt = Get_DataFor3Filter("USP_GetSurveyOnAgencyAndLevel2024", "", FormLevel.ToString(), ddlYear.SelectedItem.Text);
            //dt = objBLL.Select_All_Data("MSTForm", "FormID,FormName", "IsDeleted = 0 and FormLevel = " + FormLevel  + " ", "", "");
            PVLocatDT(dt);
        }

        ddlcForm.DataSource = dt;
        ddlcForm.DataTextField = "FormName";
        ddlcForm.DataValueField = "FormID";
        ddlcForm.DataBind();
        ddlcForm.Items.Insert(0, new System.Web.UI.WebControls.ListItem("------Select-------", "0"));



    }
    public void FillFormName(int FormLevel)
    {
        string UserID = Session["UserID"].ToString();
        DataTable dt = new DataTable();
        //int FormLevel;
        if (FormLevel == 0 || FormLevel == -1)
        {
            //  dt = objBLL.Select_All_Data("MSTForm", "FormID,FormName", "IsDeleted = 0", "", "");
        }
        else
        {
            dt = Get_DataFor3Filter("USP_GetSurveyOnAgencyAndLevel2024", "", FormLevel.ToString(), ddlYear.SelectedItem.Text);
            //dt = objBLL.Select_All_Data("MSTForm", "FormID,FormName", "IsDeleted = 0 and FormLevel = " + FormLevel  + " ", "", "");
            PVLocatDT(dt);
        }

        ddlForm.DataSource = dt;
        ddlForm.DataTextField = "FormName";
        ddlForm.DataValueField = "FormID";
        ddlForm.DataBind();
        ddlForm.Items.Insert(0, new System.Web.UI.WebControls.ListItem("------Select-------", "0"));




        ddlQuestionForm.DataSource = dt;
        ddlQuestionForm.DataTextField = "FormName";
        ddlQuestionForm.DataValueField = "FormID";
        ddlQuestionForm.DataBind();
        ddlQuestionForm.Items.Insert(0, new System.Web.UI.WebControls.ListItem("------Select-------", "0"));

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
    public DataTable PVLocatDT(DataTable dt)
    {
        DataTable dtData = new DataTable();
        try
        {

            DataColumn col;
            DataRow newRow;
            for (int I = 0; I < dt.Columns.Count - 1; I++)
            {
                newRow = dtData.NewRow();
                for (int J = 0; J < dt.Rows.Count; J++)
                {
                    col = new DataColumn(dt.Rows[J][I].ToString(), Type.GetType("System.String"));
                    dtData.Columns.Add(col);
                }
            }

            for (int I = 1; I <= dt.Columns.Count - 1; I++)
            {
                newRow = dtData.NewRow();
                for (int J = 0; J < dt.Rows.Count; J++)
                {
                    newRow[J] = dt.Rows[J][I].ToString();
                }
                dtData.Rows.Add(newRow);
            }
        }
        catch { }
        return dtData;
    }
    public void FillFlagCategory(int FormID)
    {

        DataTable dt = new DataTable();
        dt = Select_All_Data("mstQuestionCategory", "CategoryID,CategoryName", "cDeleteFlag = 1  and FormID='" + ddlForm.SelectedValue + "' ", "CategoryID", "");

        ddlcat.DataSource = dt;
        ddlcat.DataTextField = "CategoryName";
        ddlcat.DataValueField = "CategoryID";
        ddlcat.DataBind();
        ddlcat.Items.Insert(0, new System.Web.UI.WebControls.ListItem("------Select-------", "0"));


        ddlMainCategory.DataSource = dt;
        ddlMainCategory.DataTextField = "CategoryName";
        ddlMainCategory.DataValueField = "CategoryID";
        ddlMainCategory.DataBind();
        ddlMainCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("------Select-------", "0"));

    }
    public void FillFlagDropDown(int FormID)
    {
        DataTable dt = new DataTable();
        dt = Select_All_Data("MSTCommon", "UID,ID,Value", "IsDeleted = 0 and Flag = 0 and FormID >0 and mYear='" + Convert.ToString(Session["FinYear"]) + "' ", " uid desc", "");

        ddlFlag.DataSource = dt;
        ddlFlag.DataTextField = "Value";
        ddlFlag.DataValueField = "UID";
        ddlFlag.DataBind();
        ddlFlag.Items.Insert(0, new System.Web.UI.WebControls.ListItem("------Select-------", "0"));

    }
    public void FillFlagDropDownGrop(int FormID)
    {
        DataTable dt = new DataTable();
        dt = Select_All_Data("MSTFormQuestion", "QuestionID,Question", " QestionTypeID=9 and FormID=" + FormID + " ", "QuestionID", "");

        ddlGroup.DataSource = dt;
        ddlGroup.DataTextField = "Question";
        ddlGroup.DataValueField = "QuestionID";
        ddlGroup.DataBind();
        ddlGroup.Items.Insert(0, new System.Web.UI.WebControls.ListItem("------Select-------", "0"));

    }



    public int FormChildQuestionBankInsertUpdate(int Questionchild, int QuestionID, int FormID, string QuestionNo, string Question, int Sequence, int QestionTypeID, int Flag, bool IsQuestionMandatory, int? maxlength, bool IsChild
    , int maskvalidation, string sTran_Type)
    {
        SqlCommand dbSqlCommand;
        using (dbSqlCommand = new SqlCommand())
            dbSqlCommand.Connection = mycon;
        if (mycon.State == ConnectionState.Closed)
            mycon.Open();
        dbSqlCommand.CommandType = CommandType.StoredProcedure;
        dbSqlCommand.CommandText = "USP_FormQuestionInsertUpdateChildQuestion";
        dbSqlCommand.Parameters.Add("@Questionchild", SqlDbType.Int).Value = Questionchild;
        dbSqlCommand.Parameters.Add("@QuestionID", SqlDbType.Int).Value = QuestionID;
        dbSqlCommand.Parameters.Add("@FormID", SqlDbType.Int).Value = FormID;
        dbSqlCommand.Parameters.Add("@QuestionNo", SqlDbType.NVarChar).Value = QuestionNo;
        dbSqlCommand.Parameters.Add("@Question", SqlDbType.NVarChar).Value = Question;
        dbSqlCommand.Parameters.Add("@Sequence", SqlDbType.VarChar).Value = Sequence;
        dbSqlCommand.Parameters.Add("@QestionTypeID", SqlDbType.Int).Value = QestionTypeID;
        dbSqlCommand.Parameters.Add("@MaxLenght", SqlDbType.Int).Value = maxlength;
        dbSqlCommand.Parameters.Add("@Flag", SqlDbType.Int).Value = Flag;
        dbSqlCommand.Parameters.Add("@IsQuestionMandatory", SqlDbType.Bit).Value = IsQuestionMandatory;
        dbSqlCommand.Parameters.Add("@maskvalidation", SqlDbType.Int).Value = maskvalidation;
        dbSqlCommand.Parameters.Add("@Tran_Type", SqlDbType.VarChar).Value = sTran_Type;
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
    protected void btnSave_Click(object sender, EventArgs e)
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
        int FormID, QuestionID = 0, QestionTypeID, Sequence, Flag, maskvalidation = 0, GroupID = 0, QType = 0;
        string QuestionNo, Question, QuestionImage = "";
        int? maxlength = null;
        bool IsQuestionMandatory = false;
        int cat = 0;


        if (ddlMainCategory.SelectedIndex <= 0)
        {
            showMessages("Please enter Category");
            return;
        }
        else
        {
            cat = Convert.ToInt32(ddlMainCategory.SelectedValue);
        }
        if (ddlQuestionType.SelectedIndex <= 0)
        {

        }

        if (txtquestion.Text == "")
        {
            showMessages("Select Question Category");
            return;
        }
        if (Convert.ToInt32(ddlQuestionType.SelectedValue) == 1)
        {


        }
        else
        {
            if (clsMain.ImageID.Length > 0)
            {

            }
            else
            {
                showMessages("Please Upload Image");
                return;
            }
        }


        FormID = Int32.Parse(ddlQuestionForm.SelectedValue);
        QuestionNo = txtquestionno.Text.Trim();
        if (Convert.ToInt32(ddlQuestionType.SelectedValue) == 2)
        {
            QuestionImage = clsMain.ImageID;

        }

        Question = txtquestion.Text.Trim();
        QType = Convert.ToInt32(ddlQuestionType.SelectedValue);
        Sequence = Int32.Parse(txtSeq.Text);
        QestionTypeID = Int32.Parse(ddlAnswerTypeID.SelectedValue);
        GroupID = Int32.Parse(ddlGroup.SelectedValue);

        if (QestionTypeID == 1 || QestionTypeID == 2)
        {
            if (ddlAnswerTypeID.SelectedValue == "0")
            {
                ddlMaskValidation.Visible = false;
            }

            else
            {
                ddlMaskValidation.Visible = true;
            }
        }


        if (QestionTypeID == 4 || QestionTypeID == 5 || QestionTypeID == 10)
        {
            if (ddlFlag.SelectedValue == "0")
            {
                showMessages("Select Option Source");
                return;
            }
        }

        if (ddlMaskValidation.SelectedIndex != -1)
        {
            maskvalidation = Int32.Parse(ddlMaskValidation.SelectedValue);
        }

        Flag = Int32.Parse(ddlFlag.SelectedValue);
        if (txtMaxLenght.Text.Trim() != "")
        {
            maxlength = Int32.Parse(txtMaxLenght.Text);
        }

        if (chkMandatory.Checked == true)
        {
            IsQuestionMandatory = true;
        }
        else
        {
            IsQuestionMandatory = false;
        }
        int status = 0;
        if (btnSave.Text == "Save")
        {
            status = FormQuestionInsertUpdate(QuestionID, FormID, QuestionNo, Question, Sequence, QestionTypeID, Flag, IsQuestionMandatory, maxlength, maskvalidation, "I", GroupID, QType, QuestionImage, cat);
            btnSave.Text = "Save";
            btnNew_Click(btnNew, null);
            BindGvQuestion(FormID);


        }
        if (btnSave.Text == "Save Child Question")
        {
            QuestionID = Convert.ToInt32(hdnparentid.Value);
            status = FormChildQuestionBankInsertUpdate(0, QuestionID, FormID, QuestionNo, Question, Sequence, QestionTypeID, Flag, IsQuestionMandatory, maxlength, false, maskvalidation, "I");
            BindGVQuestionchild(Convert.ToString(ddlParentQuestion.SelectedValue), Convert.ToString(ddlForm.SelectedValue));
            FillFlagDropDownGrop(FormID);
            btnSave.Text = "Save Child Question";
        }
        if (btnSave.Text == "Update Child Question")
        {
            QuestionID = Convert.ToInt32(hdnparentid.Value);
            int OrignalChildQuestionID = Convert.ToInt32(hdnOrignalChildQuestionID.Value);

            status = FormChildQuestionBankInsertUpdate(OrignalChildQuestionID, QuestionID, FormID, QuestionNo, Question, Sequence, QestionTypeID, Flag, IsQuestionMandatory, maxlength, false, maskvalidation, "U");
            btnSave.Text = "Save Child Question";
            FillFlagDropDownGrop(FormID);
            btnNew_Click(btnNew, null);
        }


        else if (btnSave.Text == "Update")
        {

            string QuestionAns = "";

            if (Convert.ToString(Session["UID"]) == Convert.ToString(ddlFlag.SelectedValue))
            {
                QuestionAns = Convert.ToString(Session["QuestionAns"]);
            }
            QuestionID = Int32.Parse(HFQuestionID.Value);
            status = FormQuestionUpdate(QuestionID, FormID, QuestionNo, Question, Sequence, QestionTypeID, Flag, IsQuestionMandatory, maxlength, maskvalidation, "U", GroupID, QType, QuestionAns, QuestionImage);
            btnSave.Text = "Save";
            BindGvQuestion(FormID);
            btnNew_Click(btnNew, null);

        }

        if (status == 1)
        {
            showMessages("Added successfully");
        }
        else if (status == 2)
        {
            showMessages("Updated successfully");
        }
        else if (status == 3)
        {
            showMessages(" This Question is already present ");
        }
        else if (status == 4)
        {
            showMessages("The Sequence number is already present");
        }
        else
        {
            showMessages("Some thing went wrong ! Try Again ");
        }
        btnSave.Enabled = false;
        ClearField();
    }
    protected void BindGvQuestionCat(int FormID)
    {

        DataTable dtQuestion = new DataTable();
        DataTable dtFormLinked = new DataTable();

        //dtQuestion = objBLL.Select_All_Data("MSTFormQuestion", "QuestionID,QuestionNo,Question,QuestionFieldName,QestionTypeID,Sequence,Flag,IsQuestionMandatory,MaxLenght,MaskValidation", "IsDeleted = 0 and FormID = " + FormID + " ", "Sequence", "");
        dtQuestion = Get_DataFor3Filter("USP_GetMSTFormQuestionOnForm12024cate", FormID.ToString(), ddlYear.SelectedItem.Text, ddlcat.SelectedValue);

        GvQuestion.Visible = true;
        GvQuestion.DataSource = dtQuestion;
        GvQuestion.DataBind();

        if (dtQuestion.Rows.Count > 0)
        {
            lnkUplnkDown();
        }
        //dtFormLinked = objBLL.Select_All_Data("formProject", "ProjectID", "FormID = " + FormID + " ", "", "");
        dtFormLinked = Get_DataFor1Filter("USP_GetformProjectOnForm", FormID.ToString());

        if (dtFormLinked.Rows.Count > 0)
        {
            GvQuestion.Enabled = false;
        }
        else
        {
            GvQuestion.Enabled = true;
        }
        ClearField();
    }


    protected void BindGvQuestion(int FormID)
    {

        DataTable dtQuestion = new DataTable();
        DataTable dtFormLinked = new DataTable();
        if (ddlcat.SelectedIndex > 0)
        {
            dtQuestion = Get_DataFor3Filter("USP_GetMSTFormQuestionOnForm12024cate", FormID.ToString(), ddlYear.SelectedItem.Text, ddlcat.SelectedValue);


        }
        else
        {
            dtQuestion = Get_DataFor3Filter("USP_GetMSTFormQuestionOnForm12024", FormID.ToString(), ddlYear.SelectedItem.Text, "");

        }

        //dtQuestion = objBLL.Select_All_Data("MSTFormQuestion", "QuestionID,QuestionNo,Question,QuestionFieldName,QestionTypeID,Sequence,Flag,IsQuestionMandatory,MaxLenght,MaskValidation", "IsDeleted = 0 and FormID = " + FormID + " ", "Sequence", "");

        GvQuestion.Visible = true;
        GvQuestion.DataSource = dtQuestion;
        GvQuestion.DataBind();

        if (dtQuestion.Rows.Count > 0)
        {
            lnkUplnkDown();
        }
        //dtFormLinked = objBLL.Select_All_Data("formProject", "ProjectID", "FormID = " + FormID + " ", "", "");
        dtFormLinked = Get_DataFor1Filter("USP_GetformProjectOnForm", FormID.ToString());

        if (dtFormLinked.Rows.Count > 0)
        {
            GvQuestion.Enabled = false;
        }
        else
        {
            GvQuestion.Enabled = true;
        }
        ClearField();
    }
    public DataTable Get_DataFor1Filter(string ProcedureName, string Filter1)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                            new SqlParameter("@Filter1",Filter1),
                    };
            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, ProcedureName, paramvT);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch (Exception)
        {

        }
        return dtcombo;
    }
    protected void lnkbtn_Click(object sender, EventArgs e)
    {
        Session["FormLevel"] = ddlLevel.SelectedValue;
        Session["FormID"] = ddlForm.SelectedValue;
        Session["FormIDName"] = ddlForm.SelectedItem.Text;
        Session["SeQuestionNoValue"] = txtquestionno.Text;
        Session["SeQuestionValue"] = txtquestion.Text;
        Session["SeDisplaySequenceValue"] = txtSeq.Text;
        Session["AnswerTuypeID"] = ddlAnswerTypeID.SelectedValue.ToString();
        Session["QID"] = ddlQuestionType.SelectedValue.ToString();
        Session["cat"] = ddlcat.SelectedValue.ToString();
        Response.Redirect("FrmAddMasterCommon.aspx");
    }

    protected void ddlparentQuest_Change(object sender, EventArgs e)
    {

    }
    protected void ddlQuestionType_Change(object sender, EventArgs e)
    {
        imgMKS.ImageUrl = null;
        clsMain.ImageID = "";
        Q1.Visible = false;
        Q2.Visible = false;
        if (Convert.ToInt32(ddlQuestionType.SelectedValue) == 1)
        {
            Q1.Visible = true;
            Q2.Visible = false;

        }
        if (Convert.ToInt32(ddlQuestionType.SelectedValue) == 2)
        {
            Q1.Visible = true;
            Q2.Visible = true;
        }
        txtquestion.Focus();
    }
    protected void Submit(object sender, EventArgs e)
    {
        imgMKS.ImageUrl = ResolveUrl("~/Survey/" + clsMain.ImageID);
    }
    protected void ddlAnswerTypeID_Change(object sender, EventArgs e)
    {
        //int iSelectedValue = int.Parse(ddlAnswerTypeID.SelectedValue);
        //ChnageOfddlAnswerTypeID(iSelectedValue);
        imgMKS.ImageUrl = ResolveUrl("~/Survey/" + clsMain.ImageID);
        ddlGroup.SelectedIndex = 0;

        int iSelectedValue = int.Parse(ddlAnswerTypeID.SelectedValue);

        if (iSelectedValue == 1)
        {
            Get_mskvalidationNew(iSelectedValue);
            Text();
            ddlMaskValidation.Focus();
        }
        if (iSelectedValue == 2)
        {
            Get_mskvalidationNew(iSelectedValue);
            Numeric();
            ddlMaskValidation.Focus();

        }
        if (iSelectedValue == 3)
        {
            Get_mskvalidationNew(iSelectedValue);
            Date();
            chkMandatory.Focus();
        }
        if (iSelectedValue == 4)
        {
            SingleChoice();
            ddlFlag.Focus();
        }
        if (iSelectedValue == 5)
        {
            MultipleChoice();
            ddlFlag.Focus();
        }

        if (iSelectedValue > 5)
        {
            if (iSelectedValue == 9)
            {
                Date();
                div1Grop.Attributes.Add("style", "display:none;");
                chkMandatory.Focus();
            }
            else if (iSelectedValue == 10)
            {
                MultipleChoice();
                ddlFlag.Focus();
            }
            else
            {
                AfterImage();
                chkMandatory.Focus();
            }

        }



    }
    public void Get_mskvalidationNew(int iSelectedValue)
    {
        DataTable dt = new DataTable();
        dt = Exec_ProcedureNew("USP_GETmastValidationFlag", iSelectedValue);
        ddlMaskValidation.DataSource = dt;
        ddlMaskValidation.DataTextField = "Value";
        ddlMaskValidation.DataValueField = "ID";
        ddlMaskValidation.DataBind();
        ddlMaskValidation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("----Select-----", "0"));
    }
    public void ChnageOfddlAnswerTypeID(int iSelectedValue)
    {

        if (iSelectedValue == 7)
        {
            lblddlFlag.Visible = false;
            ddlFlag.Visible = false;
            lnkbtn.Visible = false;
            lblchkMandatory.Visible = false;
            chkMandatory.Visible = false;
        }
        else
        {

            lblddlFlag.Visible = true;
            ddlFlag.Visible = true;
            lnkbtn.Visible = true;
            lblchkMandatory.Visible = true;
            chkMandatory.Visible = true;


            if (iSelectedValue > 0 && iSelectedValue < 4)
            {
                divMaster.Attributes.Add("style", "display:none;");
                divredirect.Attributes.Add("style", "display:none;");
            }
            else
            {
                if (iSelectedValue == 6)
                {
                    divMaster.Attributes.Add("style", "display:none;");
                    divredirect.Attributes.Add("style", "display:none;");
                }
                else
                {
                    divMaster.Attributes.Add("style", "display:block;");
                    divredirect.Attributes.Add("style", "display:block;");
                }
            }


            if (iSelectedValue == 1)
            {
                divMaxLenght.Attributes.Add("style", "display:block;");
                txtMaxLenght.Text = "50";
            }
            else if (iSelectedValue == 2)
            {
                divMaxLenght.Attributes.Add("style", "display:block;");
                txtMaxLenght.Text = "7";
            }
            else
            {
                divMaxLenght.Attributes.Add("style", "display:none;");
                txtMaxLenght.Text = "";
            }




        }





    }

    protected void ddlForm_SelectedIndexChanged(object sender, EventArgs e)
    {
        int FormID = Int32.Parse(ddlForm.SelectedValue);
        ddlQuestionForm.SelectedValue = FormID.ToString();
        btnSave.Visible = true;
        btnNew.Visible = true;
        btnSave.Text = "Save";
        FillFlagCategory(FormID);
        BindGvQuestion(FormID);
        FillFlagDropDown(FormID);

        clsMain.TestID = FormID.ToString();

        Session["FormID"] = FormID.ToString();
        FillFlagDropDownGrop(FormID);
        ddlcat.Focus();
    }
    protected void ddlcat_SelectedIndexChanged(object sender, EventArgs e)
    {
        int FormID = Int32.Parse(ddlForm.SelectedValue);

        BindGvQuestion(FormID);
        LinkButton1.TabIndex = 4;
        LinkButton1.Focus();


    }

    private void showMessages(string messages)
    {
        lbl_messages.Text = "";
        lbl_messages.Text = messages;
        ModalAlert.Show();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        { }
        else
        {
            Response.Redirect("Login.aspx");
        }
        ClearField();
        FillFlagDropDownGrop(Convert.ToInt32(ddlForm.SelectedValue));
    }
    protected void gvnroll_OnRowCommand(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Label lblUniqueChildCode = (Label)e.Row.FindControl("lblUniqueChildCode");

            Image lbtn = (Image)e.Row.FindControl("imgMKSG");

            Label lblQuestionType = (Label)e.Row.FindControl("lblQuestionType");
            Label lblQuestion = (Label)e.Row.FindControl("lblQuestion");
            Label ImageUpload = (Label)e.Row.FindControl("lblImageUpload");
            LinkButton Edit_Question = (LinkButton)e.Row.FindControl("Edit_Question");
            ImageButton Delete_Question = (ImageButton)e.Row.FindControl("Delete_Question");
            if (Session["FinYear"].ToString() == ddlYear.SelectedItem.Text)
            {
                Delete_Question.Enabled = true;
                Edit_Question.Enabled = true;
            }
            else
            {
                Delete_Question.Enabled = false;
                Edit_Question.Enabled = false;
            }

            if (lblQuestionType.Text == "2")
            {
                lbtn.Visible = true;
                lblQuestion.Visible = true;

                lbtn.ImageUrl = ResolveUrl("~/Survey/" + ImageUpload.Text);
            }
            else
            {
                lbtn.Visible = false;
                lblQuestion.Visible = true;
            }
        }



    }

    public void ClearField()
    {
        divMaxLenght.Attributes.Add("style", "display:none;");
        divmask.Attributes.Add("style", "display:none;");

        txtMaxLenght.Text = "";
        HFQuestionID.Value = "";
        txtquestionno.Text = "";
        txtquestion.Text = "";
        //txtSeq.Text = "";
        ddlAnswerTypeID.SelectedIndex = -1;
        ddlFlag.SelectedIndex = -1;
        ddlMaskValidation.SelectedIndex = -1;
        chkMandatory.Checked = false;
        Q2.Visible = false;
        Q1.Visible = true;
        ddlQuestionType.SelectedIndex = 0;
        //btnSave.Text = "Save";
    }
    public DataTable Get_DataFor2Filter(string ProcedureName, string Filter1, string Filter2)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                            new SqlParameter("@Filter1",Filter1),
                            new SqlParameter("@Filter2",Filter2),
                    };
            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, ProcedureName, paramvT);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch (Exception)
        {

        }
        return dtcombo;
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        btnSave.Enabled = true;
        ClearField();
        int FormID = Int32.Parse(ddlQuestionForm.SelectedValue);

        //if (btnSave.Text == "Save")
        //{
        DataTable Dt = new DataTable();

        DataTable dt2 = Exec_Procedure("USP_GetChildQuestionOptions");
        //DataRow[] row1 = dt2.Select("ID = '7'");
        ddlAnswerTypeID.Items.Clear();
        ddlAnswerTypeID.DataSource = dt2;
        ddlAnswerTypeID.DataValueField = "id";
        ddlAnswerTypeID.DataTextField = "Value";
        ddlAnswerTypeID.DataBind();
        ddlAnswerTypeID.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" Select Question Type ", "0"));

        Dt = Get_DataFor1Filter("USP_GetQuestions", FormID.ToString());
        //Dt = objBLL.Select_All_Data("MSTFormQuestion", " CASE WHEN (max([Sequence]) = '' or max([Sequence]) is null) THEN 1 ELSE max([Sequence] +1) end as sequence", "formId = " + FormID + " ", "", "");

        if (Dt.Rows.Count > 0)
        {
            //txtquestionno.Text = Dt.Rows[0]["QuestionNo"].ToString();
            txtSeq.Text = Dt.Rows[0]["Sequence"].ToString();
        }
        BindGvQuestion(Int32.Parse(ddlForm.SelectedValue));
        GvQuestionChild.DataSource = null;
        GvQuestionChild.DataBind();
        GvQuestionChild.Visible = false;
        txtquestionno.Enabled = true;
        txtSeq.Enabled = false;
        txtquestion.Enabled = true;
        ddlAnswerTypeID.Enabled = true;
        ddlQuestionType.Enabled = true;
        btnSave.Visible = true;
        btnNew.Visible = true;
        divparentchildquestion.Visible = false;
        btnSave.Text = "Save";
        btnSave.Enabled = true;
        ddlGroup.SelectedIndex = 0;
        //}
        //else if (btnSave.Text == "Save Child Question")
        //{
        //    int FormIDforquestion = Int32.Parse(ddlForm.SelectedValue);
        //    ddlQuestionForm.SelectedValue = FormIDforquestion.ToString();
        //    GvQuestionChild.DataSource = null;
        //    GvQuestionChild.DataBind();
        //    GvQuestion.Visible = true;
        //    GvQuestionChild.Visible = false;
        //    BindGvQuestion(FormIDforquestion);

        //    DataTable Dt = objBLL.Get_DataFor1Filter("USP_GetQuestions", FormID.ToString());

        //    if (Dt.Rows.Count > 0)
        //    {
        //        //txtquestionno.Text = Dt.Rows[0]["QuestionNo"].ToString();
        //        txtSeq.Text = Dt.Rows[0]["Sequence"].ToString();
        //    }


        //    txtquestionno.Enabled = true;
        //    txtSeq.Enabled = false;
        //    txtquestion.Enabled = true;
        //    ddlAnswerTypeID.Enabled = true;
        //    btnSave.Visible = true;
        //    btnNew.Visible = true;
        //    divparentchildquestion.Visible = false;
        //}
        ddlMainCategory.Focus();
    }
    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        int FormLevl = Int32.Parse(ddlLevel.SelectedValue.ToString());

        FillFormName(FormLevl);
        GvQuestion.DataSource = null;
        GvQuestion.DataBind();
        ddlForm.Focus();
    }
    protected void ddlcLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        int FormLevl = Int32.Parse(ddlcLevel.SelectedValue.ToString());

        FillFormNamecat(FormLevl);
        MPE_Entry.Show();
    }

    protected void Edit_Question_Click(object sender, EventArgs e)
    {
        LinkButton Edit_Question = sender as LinkButton;
        GridViewRow row = Edit_Question.NamingContainer as GridViewRow;
        int index = row.RowIndex;


        txtquestionno.Enabled = true;
        txtSeq.Enabled = false;
        txtquestion.Enabled = true;
        ddlAnswerTypeID.Enabled = true;
        ddlQuestionType.Enabled = true;
        btnSave.Visible = true;
        btnNew.Visible = true;

        Session["QuestionAns"] = GvQuestion.DataKeys[index].Values["QuestionAns"].ToString();
        Session["UID"] = GvQuestion.DataKeys[index].Values["UID"].ToString();

        HFQuestionID.Value = GvQuestion.DataKeys[index].Values["QuestionID"].ToString();
        txtquestionno.Text = GvQuestion.DataKeys[index].Values["QuestionNo"].ToString();
        ddlQuestionType.SelectedValue = GvQuestion.DataKeys[index].Values["QuestionType"].ToString();

        ddlMainCategory.SelectedValue = GvQuestion.DataKeys[index].Values["QCategoryID"].ToString();
        string strQry = "Select * from Tbl_Training_Ques inner join tbl_training_question on tbl_training_question.Tarining_ID=Tbl_Training_Ques.FormID where Createdate>='2026-04-01' and QuestionID=" + HFQuestionID.Value + "   ";
        //  string strQry = "Select * from Tbl_Training_Ques  where QuestionID=" + HFQuestionID.Value + "   ";
        clsMain obm = new clsMain();




        DataTable dtRole = obm.LoadData(strQry);
        if (dtRole.Rows.Count > 0)
        {
            showMessages("You can not  Edit because Question link in training");
            return;
        }
        if (Convert.ToInt32(ddlQuestionType.SelectedValue) == 2)
        {
            Q1.Visible = true;
            Q2.Visible = true;
            txtquestion.Text = "";
            clsMain.ImageID = GvQuestion.DataKeys[index].Values["ImageUpload"].ToString();
            imgMKS.ImageUrl = ResolveUrl("~/Survey/" + clsMain.ImageID);
        }
        else
        {
            Q1.Visible = true;
            Q2.Visible = false;
            clsMain.ImageID = "";

        }
        txtquestion.Text = GvQuestion.DataKeys[index].Values["Question"].ToString();
        txtSeq.Text = GvQuestion.DataKeys[index].Values["Sequence"].ToString();
        ddlAnswerTypeID.SelectedValue = GvQuestion.DataKeys[index].Values["QestionTypeID"].ToString();
        ddlAnswerTypeID_Change(ddlAnswerTypeID, null);
        //ddlFlag

        //   ChnageOfddlAnswerTypeID(Int32.Parse(GvQuestion.DataKeys[index].Values["QestionTypeID"].ToString()));

        txtMaxLenght.Text = GvQuestion.DataKeys[index].Values["MaxLenght"].ToString();

        ddlMaskValidation.SelectedValue = GvQuestion.DataKeys[index].Values["MaskValidation"].ToString();

        try
        {
            string flagVaue = GvQuestion.DataKeys[index].Values["UID"].ToString();
            ddlFlag.SelectedValue = GvQuestion.DataKeys[index].Values["UID"].ToString();
            ddlGroup.SelectedValue = GvQuestion.DataKeys[index].Values["GroupID"].ToString();
        }
        catch
        {
            ddlFlag.SelectedIndex = -1;
            ddlGroup.SelectedIndex = -1;
        }

        string CheckValue = GvQuestion.DataKeys[index].Values["IsQuestionMandatory"].ToString();

        if (CheckValue == "True")
        {
            chkMandatory.Checked = true;
        }
        else
        {
            chkMandatory.Checked = false;
        }
        btnSave.Enabled = true;
        btnSave.Text = "Update";
    }

    protected void Delete_Question_Click(object sender, EventArgs e)
    {
        //MPEFormName.Show();
        if (Convert.ToString(Session["username"]) != "")
        { }
        else
        {
            Response.Redirect("Login.aspx");
        }
        ImageButton Edit_Question = sender as ImageButton;
        GridViewRow row = Edit_Question.NamingContainer as GridViewRow;
        int index = row.RowIndex;

        int QuestionID, status, FormID;
        QuestionID = Int32.Parse(GvQuestion.DataKeys[index].Values["QuestionID"].ToString());
        string strQry = "Select * from Tbl_Training_Ques inner join tbl_training_question on tbl_training_question.Tarining_ID=Tbl_Training_Ques.FormID where Createdate>='2026-04-01' and QuestionID=" + QuestionID + "   ";
        clsMain obm = new clsMain();

        DataTable dtRole = obm.LoadData(strQry);
        if (dtRole.Rows.Count > 0)
        {
            showMessages("You can not  Deleted because Question link in training");
            return;
        }
        status = FormQuestionInsertUpdate(QuestionID, 0, "", "", 0, 0, 0, false, 0, 0, "D", 0, 0, "", 0);
        if (status > 0)
        {
            showMessages("Record Deleted");
        }
        FormID = Int32.Parse(ddlForm.SelectedValue);
        ddlQuestionForm.SelectedValue = FormID.ToString();

        BindGvQuestion(FormID);
        FillFlagDropDown(FormID);

    }
    public int FormQuestionInsertUpdate(int QuestionID, int FormID, string QuestionNo, string Question, int Sequence, int QestionTypeID, int Flag, bool IsQuestionMandatory, int? maxlength, int maskvalidation, string sTran_Type, int GroupID, int QType, string QuestionImage, int CategoryID)
    {
        SqlCommand dbSqlCommand;
        using (dbSqlCommand = new SqlCommand())
            dbSqlCommand.Connection = mycon;
        if (mycon.State == ConnectionState.Closed)
            mycon.Open();
        dbSqlCommand.CommandType = CommandType.StoredProcedure;
        dbSqlCommand.CommandText = "USP_FormQuestionInsertUpdate2024";

        dbSqlCommand.Parameters.Add("@QuestionID", SqlDbType.Int).Value = QuestionID;
        dbSqlCommand.Parameters.Add("@FormID", SqlDbType.Int).Value = FormID;
        dbSqlCommand.Parameters.Add("@QuestionNo", SqlDbType.VarChar).Value = QuestionNo;
        dbSqlCommand.Parameters.Add("@Question", SqlDbType.NVarChar).Value = Question;
        //dbSqlCommand.Parameters.Add("@Question", SqlDbType.VarChar).Value = Question;
        dbSqlCommand.Parameters.Add("@Sequence", SqlDbType.VarChar).Value = Sequence;

        dbSqlCommand.Parameters.Add("@QestionTypeID", SqlDbType.Int).Value = QestionTypeID;
        dbSqlCommand.Parameters.Add("@MaxLenght", SqlDbType.Int).Value = maxlength;


        dbSqlCommand.Parameters.Add("@Flag", SqlDbType.Int).Value = Flag;


        dbSqlCommand.Parameters.Add("@IsQuestionMandatory", SqlDbType.Bit).Value = IsQuestionMandatory;
        dbSqlCommand.Parameters.Add("@maskvalidation", SqlDbType.Int).Value = maskvalidation;
        dbSqlCommand.Parameters.Add("@GroupID", SqlDbType.VarChar).Value = GroupID;
        dbSqlCommand.Parameters.Add("@QType", SqlDbType.Int).Value = QType;

        dbSqlCommand.Parameters.Add("@ImageUpload", SqlDbType.VarChar).Value = QuestionImage;
        dbSqlCommand.Parameters.Add("@CategoryID", SqlDbType.Int).Value = CategoryID;


        dbSqlCommand.Parameters.Add("@Tran_Type", SqlDbType.VarChar).Value = sTran_Type;

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
    public int FormQuestionUpdate(int QuestionID, int FormID, string QuestionNo, string Question, int Sequence, int QestionTypeID, int Flag, bool IsQuestionMandatory, int? maxlength, int maskvalidation, string sTran_Type, int GroupID, int QType, string QuestionAns, string QuestionImage)
    {
        SqlCommand dbSqlCommand;
        using (dbSqlCommand = new SqlCommand())
            dbSqlCommand.Connection = mycon;
        if (mycon.State == ConnectionState.Closed)
            mycon.Open();
        dbSqlCommand.CommandType = CommandType.StoredProcedure;
        dbSqlCommand.CommandText = "USP_FormQuestionUpdate";

        dbSqlCommand.Parameters.Add("@QuestionID", SqlDbType.Int).Value = QuestionID;
        dbSqlCommand.Parameters.Add("@FormID", SqlDbType.Int).Value = FormID;
        dbSqlCommand.Parameters.Add("@QuestionNo", SqlDbType.VarChar).Value = QuestionNo;
        dbSqlCommand.Parameters.Add("@Question", SqlDbType.NVarChar).Value = Question;
        //dbSqlCommand.Parameters.Add("@Question", SqlDbType.VarChar).Value = Question;
        dbSqlCommand.Parameters.Add("@Sequence", SqlDbType.VarChar).Value = Sequence;

        dbSqlCommand.Parameters.Add("@QestionTypeID", SqlDbType.Int).Value = QestionTypeID;
        dbSqlCommand.Parameters.Add("@MaxLenght", SqlDbType.Int).Value = maxlength;


        dbSqlCommand.Parameters.Add("@Flag", SqlDbType.Int).Value = Flag;


        dbSqlCommand.Parameters.Add("@IsQuestionMandatory", SqlDbType.Bit).Value = IsQuestionMandatory;
        dbSqlCommand.Parameters.Add("@maskvalidation", SqlDbType.Int).Value = maskvalidation;
        dbSqlCommand.Parameters.Add("@GroupID", SqlDbType.VarChar).Value = GroupID;
        dbSqlCommand.Parameters.Add("@QType", SqlDbType.Int).Value = QType;
        dbSqlCommand.Parameters.Add("@QuestionAns", SqlDbType.VarChar).Value = @QuestionAns;
        dbSqlCommand.Parameters.Add("@ImageUpload", SqlDbType.VarChar).Value = QuestionImage;

        dbSqlCommand.Parameters.Add("@Tran_Type", SqlDbType.VarChar).Value = sTran_Type;

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

    public void fillQuestionsNw()
    {
        //hdnconditions.Value = Condition.ToString();
        DataTable dt = Get_DataFor3Filter("USP_GetQuestionInDiffrentLanguage", "0", ddlForm.SelectedValue, "0");
        Session["Ism"] = dt;
        //DataListQuestion.DataSource = dt;
        //DataListQuestion.DataBind();

        StringBuilder sb = new StringBuilder();
        string Type, Questionid, Length;
        DataTable dtTempMSCommon = new DataTable();
        DataTable dtMSCommon = Get_DataFor2Filter("USP_GetoptionsforWebSurvey", "4", ddlForm.SelectedValue);

        sb.Append("<div class='container'> <form class='form-horizontal'><table id='WebSurveyTable' class='table table-bordered' width='100%'> ");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Questionid = dt.Rows[i]["QuestionId"].ToString();
            Length = dt.Rows[i]["MaxLenght"].ToString();
            Type = gettypeofQuestion(dt.Rows[i]["QestionTypeID"].ToString(), dt.Rows[i]["Flag"].ToString(), dtMSCommon, dtTempMSCommon, Questionid, Length, Convert.ToInt32(dt.Rows[i]["MaskValidation"].ToString()), dt.Rows[i]["Value"].ToString());
            if (dt.Rows[i]["QestionTypeID"].ToString() == "9")
            {
                sb.Append(" <tr class='header' style='background-color:yellow; font-size:15px; font-weight:bold;'><td style='width:100px;'><span> " + dt.Rows[i]["QuestionNo"].ToString() + " </span></td><td colspan = '2'>" + dt.Rows[i]["Question"].ToString().Replace("'", "") + " </td></tr>");
            }
            else
            {

                sb.Append("<tr class=" + Questionid + " style='background-color:#ffdfba'><td style='width:2%'><span> " + dt.Rows[i]["QuestionNo"].ToString() + " </span></td><td width='48%'>" + dt.Rows[i]["Question"].ToString().Replace("'", "") + " </td>");
                sb.Append("<td width='50%'> " + Type + " </td></tr>");
            }
        }
        sb.Append("</table></form></div>");
        Literal1.Text = sb.ToString();
        //StringBuilder sb2 = new StringBuilder();
        //sb2.Append("<input type='button' name='Submit' class='btn btn-primary px-5' value='Submit' onclick='savedata()'/>");
        //Savebutton.Text = sb2.ToString();
        //14300/14301/14302
    }

    public string gettypeofQuestion(string Qtype, string flag, DataTable dtcommon, DataTable dttempcommon, string Questionid, string Length, int MaskValidation, string value)
    {
        //string Ntextboxhtml = "<input type='text' class='form-control' maxlength='" + Length + "' onchange='return checkDec(this);' Style='margin-top: 5px' id='" + Questionid + "' name='Numeric' placeholder='Numeric Value'>";
        string Dtextboxhtml = "<input type='date' class='form-control'  Style='margin-top: 5px' id='" + Questionid + "' name='Date' placeholder='dd/MM/yyyy'>";
        string Timetextboxhtml = "<input type='text' class='form-control' Style='margin-top: 5px' id='" + Questionid + "' name='Time' placeholder='hh:mm:ss'>";
        string Imagehtml = "<span class='glyphicon glyphicon-picture' Style='margin-top: 5px; font-size:18px'></span>";
        string FingerPrnthtml = "<img src='images/fingerprint-2-512.png' alt='Finger Print' Style='height:25px; width:25px;'/> ";
        string Imgtextboxhtml = "<input type='file' class='form-control' Style='margin-top: 5px;'  onchange='Imageuploaddata(" + Questionid + "," + "lbl" + Questionid + ")'  id='" + Questionid + "' name='fname'><asp:HiddenField runat='server' id='" + "lbl" + Questionid + "'  Value=''  /> ";
        //string Imgtextboxhtml = "<input type='file' class='form-control' Style='margin-top: 5px'  id='File' name='fname'> ";

        string Ntextboxpercentage = "<input type='text' class='form-control' maxlength='" + Length + "' onchange='FN14351(" + Questionid + ")' Style='margin-top: 5px' id='" + Questionid + "' name='Numeric' placeholder='Numeric Value'>";


        if (Qtype == "1")
        {
            string Stextboxhtml = "";

            if (MaskValidation == 0)
            {
                Stextboxhtml = "<input type='text' class='form-control' maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='Text Box' >";

            }

            if (MaskValidation == 1)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onchange='return validateFristNumeric(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }
            if (MaskValidation == 2)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onchange='return NotOnlyNumeric(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }
            if (MaskValidation == 3)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onkeyup='return validateOnlyText(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }
            if (MaskValidation == 4)
            {
                Stextboxhtml = "<input type='text'  class='form-control'  onchange='return CheckMobile(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }
            if (MaskValidation == 5)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onchange='return ValidateEmail(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }
            if (MaskValidation == 6)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onkeyup='return checkDec(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }
            if (MaskValidation == 7)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onkeyup='return alphanumeric(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }

            Qtype = Stextboxhtml;
        }
        else if (Qtype == "2")
        {
            string Stextboxhtml = "";

            if (MaskValidation == 0)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onkeyup='return isNumberKey(this,event);' maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='Numeric Value' >";

            }

            if (MaskValidation == 1)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onchange='return validateFristNumeric(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }
            if (MaskValidation == 2)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onchange='return NotOnlyNumeric(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }
            if (MaskValidation == 3)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onkeyup='return validateOnlyText(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }
            if (MaskValidation == 4)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onchange='return CheckMobile(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }
            if (MaskValidation == 5)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onchange='return ValidateEmail(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }
            if (MaskValidation == 6)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onkeyup='return checkDec(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }
            if (MaskValidation == 7)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onkeyup='return alphanumeric(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }

            Qtype = Stextboxhtml;
            //DataTable dt = Select_All_Data("tblQuestionMapping", "*", "ParentQuestionId = " + Questionid + "", "", "");
            ////--------- if skiplogic apply
            //if (dt.Rows.Count > 0)
            //{
            //    Qtype = Ntextboxpercentage;
            //}
            //else
            //{
            //    Qtype = Ntextboxhtml;
            //}

        }

        else if (Qtype == "6")
        {
            Qtype = Imgtextboxhtml;
        }
        else if (Qtype == "Finger Print")
        {
            Qtype = FingerPrnthtml;
        }
        else if (Qtype == "3")
        {
            Qtype = Dtextboxhtml;
            if (MaskValidation == 8)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "", "<SCRIPT LANGUAGE='javascript'>NotallowFeatureDate(" + Questionid + ")</script>", false);
            }
            if (MaskValidation == 9)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "", "<SCRIPT LANGUAGE='javascript'>NotallowPastDate(" + Questionid + ")</script>", false);
            }

        }
        else if (Qtype == "8")
        {
            Qtype = Timetextboxhtml;
        }
        else if (Qtype == "4")
        {
            Qtype = "";
            DataRow[] dr2 = dtcommon.Select("Flag = " + flag);
            if (dr2.Length > 0)
                dttempcommon = dr2.CopyToDataTable();


            DataTable dt = Select_All_Data("tblQuestionMapping", "*", "ParentQuestionId = " + Questionid + "", "", "");
            //--------- if skiplogic apply
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dttempcommon.Rows.Count; i++)
                {
                    Qtype = Qtype + "<input type='radio' onchange='SetLogic(" + dttempcommon.Rows[i]["ID"] + "," + Questionid + ")' value='" + dttempcommon.Rows[i]["ID"] + "' name='" + Questionid + "'>" + dttempcommon.Rows[i]["Value"].ToString().Replace("'", "") + "<br />";
                }
            }
            else
            {
                for (int i = 0; i < dttempcommon.Rows.Count; i++)
                {
                    Qtype = Qtype + "<input type='radio' value='" + dttempcommon.Rows[i]["ID"] + "' name='" + Questionid + "'>" + dttempcommon.Rows[i]["Value"].ToString().Replace("'", "") + "<br />";
                }
            }


        }
        else if (Qtype == "5")
        {
            Qtype = "";
            DataRow[] dr2 = dtcommon.Select("Flag = " + flag);
            if (dr2.Length > 0)
                dttempcommon = dr2.CopyToDataTable();

            for (int i = 0; i < dttempcommon.Rows.Count; i++)
            {
                Qtype = Qtype + "<input type='checkbox' value='" + dttempcommon.Rows[i]["ID"] + "' name='" + Questionid + "'>" + dttempcommon.Rows[i]["Value"].ToString().Replace("'", "") + "<br />";
            }
        }
        else if (Qtype == "10")
        {
            Qtype = "";
            DataRow[] dr2 = dtcommon.Select("Flag = " + flag);
            if (dr2.Length > 0)
                dttempcommon = dr2.CopyToDataTable();
            Qtype = Qtype + "<select  class='form-control' id='" + Questionid + "' Name='Dropdown'>";
            for (int i = 0; i < dttempcommon.Rows.Count; i++)
            {
                if (i == 0)
                {
                    Qtype = Qtype + "<option type='checkbox'  value=" + i + ">--Select --</option>";
                    Qtype = Qtype + "<option type='checkbox' value=" + dttempcommon.Rows[i]["ID"] + ">" + dttempcommon.Rows[i]["Value"] + "</option>";
                }
                else
                {
                    Qtype = Qtype + "<option type='checkbox'  value=" + dttempcommon.Rows[i]["ID"] + ">" + dttempcommon.Rows[i]["Value"] + "</option>";
                }
            }
            Qtype = Qtype + "</select>";
        }
        return Qtype;
    }

    protected void lnkPreview_Click(object sender, EventArgs e)
    {

        fillQuestionsNw();
        StringBuilder html = new StringBuilder();
        DataTable dtQuestion = new DataTable();
        DataTable dtQuestionMappingDB = new DataTable();
        DataTable dtTemp = new DataTable();
        DataTable dtQuestTemp = new DataTable();
        DataTable dtQuestAll = new DataTable();

        DataTable dtMSCommon = new DataTable();

        DataTable dtTempMSCommon = new DataTable();

        //dtMSCommon = objBLL.Select_All_Data("MSTCommon", " * ", "LanguageID=1", "", "");
        dtMSCommon = Exec_Procedure("USP_GetMSTCommonFor1");
        string Type = "";


        //dtQuestion = objBLL.Select_All_Data("MSTFormQuestion inner join (select * from MSTCommon where Flag = 6 and IsDeleted = 0 and LanguageID=1) as tblQuestionType ON MSTFormQuestion.QestionTypeID = tblQuestionType.ID", " MSTFormQuestion.*,tblQuestionType.Value as QType ", " MSTFormQuestion.IsDeleted = 0 and MSTFormQuestion.FormID = " + ddlForm.SelectedValue + "  ", "Sequence", "");
        dtQuestion = Get_DataFor1Filter("USP_GetMSTFormQuestionOnForm", ddlForm.SelectedValue);
        //dtQuestionMappingDB = objBLL.Select_All_Data("tblQuestionMapping", " * ", "", "", "");
        dtQuestionMappingDB = Exec_Procedure("USP_GettblQuestionMapping");
        dtTemp = dtQuestionMappingDB.Clone();
        dtQuestTemp = dtQuestion.Clone();

        //dtQuestAll = objBLL.Select_All_Data("MSTFormQuestion inner join (select * from MSTCommon where Flag = 6 and IsDeleted = 0 and LanguageID=1) as tblQuestionType ON MSTFormQuestion.QestionTypeID = tblQuestionType.ID", " MSTFormQuestion.*,tblQuestionType.Value as QType ", " MSTFormQuestion.IsDeleted = 0 and MSTFormQuestion.FormID = " + ddlForm.SelectedValue + "  ", "Sequence", "");
        dtQuestAll = Get_DataFor1Filter("USP_GetMSTFormQuestionOnForm", ddlForm.SelectedValue);

        html.Append("<div class='panel-body' > <form class='form-horizontal'><table width='100%'> ");
        html.Append(" <tr><td width='15%'><b> " + "QuestionNo" + " </b></td><td width='70%'><b> " + "Question" + "</b> </td><td width='35%'><b> " + "Type" + "</b></td></tr>");
        for (int i = 0; i < dtQuestion.Rows.Count; i++)
        {

            //DataRow[] dr = dtQuestionMappingDB.Select("ParentQuestionId = " + dtQuestion.Rows[i]["QuestionID"]);
            //if (dr.Length > 0)
            //          dtTemp = dr.CopyToDataTable();
            //for (int j = 0; j < dtTemp.Rows.Count; j++)
            //{
            //    DataRow[] dr2 = dtQuestAll.Select("QuestionId = " + dtTemp.Rows[j]["DependentQuestionId"]);
            //    if (dr2.Length > 0)
            //        dtQuestTemp = dr2.CopyToDataTable();
            //    Type = gettypeofQuestion(dtQuestTemp.Rows[0]["QType"].ToString(), dtQuestion.Rows[i]["Flag"].ToString(), dtMSCommon, dtTempMSCommon);
            //    html.Append(" <div class='form-group' ><label for='inputEmail' class='control-label col-xs-8' style='margin-top: 5px;text-align: left;'> " + dtQuestTemp.Rows[0]["Question"] + " </label><div class='col-xs-4'> " + Type + " </div></div> ");

            //}
            Type = gettypeofQuestion(dtQuestion.Rows[i]["QType"].ToString(), dtQuestion.Rows[i]["Flag"].ToString(), dtMSCommon, dtTempMSCommon);

            html.Append(" <tr><td width='15%'><b> " + dtQuestion.Rows[i]["QuestionNo"] + " </b></td><td width='70%'> " + dtQuestion.Rows[i]["Question"] + " </td><td width='35%'> " + Type + " </td></tr>");


            //html.Append(" <div class='form-group'><div class='col-xs-1'> " + dtQuestion.Rows[i]["QuestionNo"] + " </div><div class='control-label col-xs-8' style='margin-top: 5px;text-align: left;'> " + dtQuestion.Rows[i]["Question"] + " </div><div class='col-xs-3' style='text-align: left;'> " + Type + " </div></div>");

        }

        html.Append("</table></form></div>");

        // dialog.InnerHtml = html.ToString();

        MPPreview.Show();
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
        catch
        {
            //string mmsg = ex.Message; showMessages(mmsg);
            //showMessages("(SelectAllData)  " + mmsg);
        }
        return dtcombo;
    }

    public string gettypeofQuestion(string Qtype, string flag, DataTable dtcommon, DataTable dttempcommon)
    {
        string Ntextboxhtml = "<input type='text' class='form-control' Style='margin-top: 5px' name='fname' placeholder='Numeric Value'>";
        string Stextboxhtml = "<input type='text' class='form-control' Style='margin-top: 5px' name='fname' placeholder='Text Value'>";
        string Dtextboxhtml = "<input type='text' class='form-control' Style='margin-top: 5px' name='fname' placeholder='Date'>";
        string Tmtextboxhtml = "<input type='text' class='form-control' Style='margin-top: 5px' name='fname' placeholder='Time'>";
        string Htextboxhtml = "<input type='text' class='form-control' Style='margin-top: 5px' name='fname' placeholder='Header'>";
        string Imgtextboxhtml = "<input type='text' class='form-control' Style='margin-top: 5px' name='fname' placeholder='Image'>";
        //"<span class='glyphicon glyphicon-picture'></span><lable Style='margin-top: 5px'>Image</lable>";
        string OtherText = "<input type='text' class='form-control' Style='margin-top: 5px' name='fname' placeholder='Other'>";


        if (Qtype == "Text")
        {
            Qtype = Stextboxhtml;
        }
        else if (Qtype == "Numeric")
        {
            Qtype = Ntextboxhtml;
        }
        else if (Qtype == "Date")
        {
            Qtype = Dtextboxhtml;
        }
        else if (Qtype == "Time")
        {
            Qtype = Tmtextboxhtml;
        }
        else if (Qtype == "Header")
        {
            Qtype = Htextboxhtml;
        }
        else if (Qtype == "Image")
        {
            Qtype = Imgtextboxhtml;
        }
        else if (Qtype == "Single Choice")
        {
            Qtype = "";
            DataRow[] dr2 = dtcommon.Select("Flag = " + flag);
            if (dr2.Length > 0)
                dttempcommon = dr2.CopyToDataTable();
            for (int i = 0; i < dttempcommon.Rows.Count; i++)
            {
                Qtype = Qtype + "<label class='radio'><input type='radio' name='optradio'>" + dttempcommon.Rows[i]["Value"] + "</label>";
            }

        }
        else if (Qtype == "Multiple Choice")
        {
            Qtype = "";
            DataRow[] dr2 = dtcommon.Select("Flag = " + flag);
            if (dr2.Length > 0)
                dttempcommon = dr2.CopyToDataTable();
            for (int i = 0; i < dttempcommon.Rows.Count; i++)
            {
                Qtype = Qtype + "<label class='checkbox'><input type='checkbox' value=''>" + dttempcommon.Rows[i]["Value"] + "</label>";
            }
        }
        else if (Qtype == "Dropdown")
        {
            Qtype = "";
            DataRow[] dr2 = dtcommon.Select("Flag = " + flag);
            if (dr2.Length > 0)
                dttempcommon = dr2.CopyToDataTable();
            Qtype = Qtype + "<select  class='form-control'>";
            for (int i = 0; i < dttempcommon.Rows.Count; i++)
            {
                Qtype = Qtype + "<option type='checkbox' value=" + dttempcommon.Rows[i]["Value"] + ">" + dttempcommon.Rows[i]["Value"] + "</option>";
            }
            Qtype = Qtype + "</select>";
        }
        else
        {
            Qtype = OtherText;
        }

        return Qtype;
    }

    protected void ChangePreferenceUP(object sender, EventArgs e)
    {

        LinkButton lnkUp = sender as LinkButton;
        GridViewRow row = lnkUp.NamingContainer as GridViewRow;
        int index = row.RowIndex;
        int QuetionID, Sequence, QuetionIDPrefrence, SequencePrefrence;

        QuetionID = Int32.Parse(GvQuestion.DataKeys[index].Values["QuestionID"].ToString());
        Sequence = Int32.Parse(GvQuestion.DataKeys[index].Values["Sequence"].ToString());

        QuetionIDPrefrence = Int32.Parse(GvQuestion.DataKeys[index - 1].Values["QuestionID"].ToString());
        SequencePrefrence = Int32.Parse(GvQuestion.DataKeys[index - 1].Values["Sequence"].ToString());

        DataTable dt = new DataTable();
        dt = UpdatePreference(QuetionID, Sequence, QuetionIDPrefrence, SequencePrefrence, Int32.Parse(ddlForm.SelectedValue));


        GvQuestion.DataSource = dt;
        GvQuestion.DataBind();
        lnkUplnkDown();
        BindGvQuestion(Convert.ToInt32(ddlForm.SelectedValue));

    }
    public DataTable UpdatePreference(int QuetionID, int Sequence, int QuetionIDPrefrence, int SequencePrefrence, int FormID)
    {
        DataTable dtBSL = new DataTable();
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                         new SqlParameter("@QuetionID ",QuetionID),
                         new SqlParameter("@Sequence ",Sequence),
                         new SqlParameter("@QuetionIDPrefrence ",QuetionIDPrefrence),
                         new SqlParameter("@SequencePrefrence ",SequencePrefrence),
                         new SqlParameter("@FormID ",FormID),
                    };
            DataTable ds = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "USP_UpdatePreference", paramvT);
            dtBSL = ds;
        }
        catch (Exception)
        { }
        return dtBSL;

    }

    protected void ChangePreferenceDown(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        { }
        else
        {
            Response.Redirect("Login.aspx");
        }
        LinkButton lnkDown = sender as LinkButton;
        GridViewRow row = lnkDown.NamingContainer as GridViewRow;
        int index = row.RowIndex;
        int QuetionID, Sequence, QuetionIDPrefrence, SequencePrefrence;

        QuetionID = Int32.Parse(GvQuestion.DataKeys[index].Values["QuestionID"].ToString());
        Sequence = Int32.Parse(GvQuestion.DataKeys[index].Values["Sequence"].ToString());

        QuetionIDPrefrence = Int32.Parse(GvQuestion.DataKeys[index + 1].Values["QuestionID"].ToString());
        SequencePrefrence = Int32.Parse(GvQuestion.DataKeys[index + 1].Values["Sequence"].ToString());

        DataTable dt = new DataTable();
        dt = UpdatePreference(QuetionID, Sequence, QuetionIDPrefrence, SequencePrefrence, Int32.Parse(ddlForm.SelectedValue));


        GvQuestion.DataSource = dt;
        GvQuestion.DataBind();
        lnkUplnkDown();
        BindGvQuestion(Convert.ToInt32(ddlForm.SelectedValue));
    }

    public void lnkUplnkDown()
    {
        LinkButton lnkUp = (GvQuestion.Rows[0].FindControl("lnkUp") as LinkButton);
        LinkButton lnkDown = (GvQuestion.Rows[GvQuestion.Rows.Count - 1].FindControl("lnkDown") as LinkButton);
        lnkUp.Enabled = false;
        lnkUp.CssClass = "buttonDisable";
        lnkDown.Enabled = false;
        lnkDown.CssClass = "buttonDisable";
    }

    protected void chkIsdepQues_Click(object sender, EventArgs e)
    {
        if (chkIsdepQues.Checked == true)
        {

            ChnageOfDependentdQues(1);
        }
        else
        {
            ChnageOfDependentdQues(0);
        }

    }

    public void ChnageOfDependentdQues(int ChkChangeVAlue)
    {
        if (ChkChangeVAlue == 1)
        {
            DependentQuestion.Attributes.Add("style", "display:block;");
            DependentQuestion.Attributes.Add("style", "display:block;");
        }
        else
        {
            DependentQuestion.Attributes.Add("style", "display:none;");
            DependentQuestion.Attributes.Add("style", "display:none;");
        }
    }

    protected void lnkbtnChild_Click(object sender, EventArgs e)
    {
        ClearField();
        int FormID = Int32.Parse(ddlForm.SelectedValue);
        DataTable dt2 = Exec_Procedure("USP_GetChildQuestionOptions");
        //DataRow[] row1 = dt2.Select("ID = '7'");
        foreach (DataRow row in dt2.Rows)
        {
            if (row["ID"].ToString().Trim().Contains("7"))
            {
                dt2.Rows.Remove(row);
                dt2.AcceptChanges();
                break;
            }
        }

        ddlAnswerTypeID.DataSource = dt2;
        ddlAnswerTypeID.DataValueField = "id";
        ddlAnswerTypeID.DataTextField = "Value";
        ddlAnswerTypeID.DataBind();
        ddlAnswerTypeID.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" Select Question Type ", "0"));

        btnSave.Text = "Save Child Question";
        if (btnSave.Text == "Save Child Question")
        {


            txtquestionno.Enabled = true;
            txtSeq.Enabled = false;
            txtquestion.Enabled = true;
            ddlAnswerTypeID.Enabled = true;
            ddlQuestionType.Enabled = true;
            btnSave.Visible = true;
            btnNew.Visible = true;
        }

        DataTable dt = new DataTable();

        dt = Get_DataFor1Filter("usp_getParentquestionForChild", Convert.ToString(FormID));
        PVLocatDT(dt);
        ddlParentQuestion.DataSource = dt;
        ddlParentQuestion.DataTextField = "Value";
        ddlParentQuestion.DataValueField = "ID";
        ddlParentQuestion.DataBind();
        ddlParentQuestion.Items.Insert(0, new System.Web.UI.WebControls.ListItem("----Select-----", "0"));




        modelQuestion.Show();


    }

    protected void btnQuestionchild_Click(object sender, EventArgs e)
    {
        divparentchildquestion.Visible = true;
        divparentdisplay.Visible = true;
        hdnparentid.Value = ddlParentQuestion.SelectedValue;


        DataTable DtMaxSequenceOfChildQuestion = new DataTable();


        DtMaxSequenceOfChildQuestion = Get_DataFor2Filter("USP_MaxSequenceOfChildQuestion", ddlForm.SelectedValue.ToString(), hdnparentid.Value.ToString());
        // ChildQuestionSection.Attributes.Add("style", "display:block;");
        if (DtMaxSequenceOfChildQuestion.Rows.Count > 0)
        {
            txtSeq.Text = DtMaxSequenceOfChildQuestion.Rows[0]["Sequence"].ToString();
        }

        BindGVQuestionchild(Convert.ToString(ddlParentQuestion.SelectedValue), Convert.ToString(ddlForm.SelectedValue));
    }

    public void BindGVQuestionchild(string ddlParentQuestionid, string Formid)
    {
        GvQuestion.DataSource = null;
        GvQuestion.DataBind();

        GvQuestion.Visible = false;
        GvQuestionChild.Visible = true;

        DataTable dtQuestion = new DataTable();
        dtQuestion = Get_DataFor2Filter("usp_GetChildQuestionList1", ddlParentQuestionid, Formid);

        lblParentQuestion.Text = ddlParentQuestion.SelectedItem.ToString();

        GvQuestionChild.DataSource = dtQuestion;
        GvQuestionChild.DataBind();
    }

    protected void lnkAddChildQuestion_Click(object sender, EventArgs e)
    {
        btnSave.Enabled = true;
        DataTable DtMaxSequenceOfChildQuestion = new DataTable();
        DtMaxSequenceOfChildQuestion = Get_DataFor2Filter("USP_MaxSequenceOfChildQuestion", ddlForm.SelectedValue.ToString(), hdnparentid.Value.ToString());
        if (DtMaxSequenceOfChildQuestion.Rows.Count > 0)
        {
            txtSeq.Text = DtMaxSequenceOfChildQuestion.Rows[0]["Sequence"].ToString();
        }
    }
    protected void Upload(object sender, EventArgs e)
    {
        string TBCode = ddlForm.SelectedValue;
        string Fullfilename = "";
        if (FileuploadAttach.PostedFile != null && FileuploadAttach.PostedFile.FileName != "")
        {
            string ext = System.IO.Path.GetExtension(FileuploadAttach.PostedFile.FileName).ToLower();
            if (FileuploadAttach.PostedFile.ContentLength < 102400)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Image size must be less than 100kb')</script>", false);
                return;
            }
            if (ext != ".jpeg" && ext != ".jpg" && ext != ".png" && ext != ".gif")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Images')</script>", false);
                return;
            }
            string exten = Path.GetExtension(FileuploadAttach.PostedFile.FileName);
            Fullfilename = "" + TBCode + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;
        }

        string sFileDir = Server.MapPath(Comman.GetImagePath("SurveyPath") + "/");

        if (FileuploadAttach.PostedFile != null && FileuploadAttach.PostedFile.FileName != "")
        {
            string exten = Path.GetExtension(FileuploadAttach.PostedFile.FileName);
            // string Imagefile1 = "LeaveDoc" + "_" + Convert.ToString(Session["EMP_ID"]) + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;

            //create directory

            if (Directory.Exists(sFileDir)) { }
            else { System.IO.Directory.CreateDirectory(sFileDir); }

            //======update the file =====\\

            if (System.IO.File.Exists(sFileDir + "\\" + Fullfilename))
            {
                try { System.IO.File.Delete(sFileDir + "\\" + Fullfilename); }
                catch
                {


                }
            }
            FileuploadAttach.PostedFile.SaveAs(sFileDir + Fullfilename);

        }

        Session["Fullfilename"] = Fullfilename;
        if (Convert.ToString(Session["Fullfilename"]) != "")
        {
            //string sFileDir = Server.MapPath(Comman.GetImagePath("ImgPage") + dtmstM.Rows[0]["ImagePath"].ToString().Trim() + "");
            //string sFileDir = Request.PhysicalApplicationPath + "images\\";
            string imagename = Convert.ToString(Session["Fullfilename"]);

            imgMKS.ImageUrl = ResolveUrl("~/Survey/" + imagename);
        }
        else
        {


            imgMKS.ImageUrl = null;
        }
    }
    protected void Edit_ChildQuestion_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        { }
        else
        {
            Response.Redirect("Login.aspx");
        }
        LinkButton Edit_ChildQuestion = sender as LinkButton;
        GridViewRow row = Edit_ChildQuestion.NamingContainer as GridViewRow;
        int index = row.RowIndex;

        txtquestionno.Enabled = true;
        txtSeq.Enabled = false;
        txtquestion.Enabled = true;
        ddlAnswerTypeID.Enabled = true;
        ddlQuestionType.Enabled = true;
        btnSave.Visible = true;
        btnNew.Visible = true;

        hdnOrignalChildQuestionID.Value = GvQuestionChild.DataKeys[index].Values["ChildQuestionID"].ToString();
        txtquestionno.Text = GvQuestionChild.DataKeys[index].Values["QuestionNo"].ToString();
        txtquestion.Text = GvQuestionChild.DataKeys[index].Values["Question"].ToString();
        txtSeq.Text = GvQuestionChild.DataKeys[index].Values["Sequence"].ToString();
        ddlAnswerTypeID.SelectedValue = GvQuestionChild.DataKeys[index].Values["QestionTypeID"].ToString();
        string MaskValidationValue = GvQuestionChild.DataKeys[index].Values["MaskValidation"].ToString();



        if (MaskValidationValue.Trim() != "")
        {
            divmask.Attributes.Add("style", "display:block;");
            ddlMaskValidation.SelectedValue = GvQuestionChild.DataKeys[index].Values["MaskValidation"].ToString();
        }
        else
        {
            divmask.Attributes.Add("style", "display:none;");
            ddlMaskValidation.SelectedIndex = -1;
        }


        //ddlFlag

        ChnageOfddlAnswerTypeID(Int32.Parse(GvQuestionChild.DataKeys[index].Values["QestionTypeID"].ToString()));
        txtMaxLenght.Text = GvQuestionChild.DataKeys[index].Values["MaxLenght"].ToString();

        try
        {
            string flagVaue = GvQuestionChild.DataKeys[index].Values["UID"].ToString();
            ddlFlag.SelectedValue = GvQuestionChild.DataKeys[index].Values["UID"].ToString();
        }
        catch
        {
            ddlFlag.SelectedIndex = -1;
        }



        string CheckValue = GvQuestionChild.DataKeys[index].Values["IsQuestionMandatory"].ToString();

        if (CheckValue == "True")
        {
            chkMandatory.Checked = true;
        }
        else
        {
            chkMandatory.Checked = false;
        }
        btnSave.Enabled = true;
        btnSave.Text = "Update Child Question";


    }

    protected void Delete_ChildQuestion_Click(object sender, EventArgs e)
    {
        //MPEFormName.Show();
        if (Convert.ToString(Session["username"]) != "")
        { }
        else
        {
            Response.Redirect("Login.aspx");
        }
        ImageButton Edit_ChildQuestion = sender as ImageButton;
        GridViewRow row = Edit_ChildQuestion.NamingContainer as GridViewRow;
        int index = row.RowIndex;
        int status = 0, OrignalChildQuestionID, FormID;
        OrignalChildQuestionID = Int32.Parse(GvQuestionChild.DataKeys[index].Values["OrignalChildQuestionID"].ToString());
        // status = objBLL.FormChildQuestionBankInsertUpdate(OrignalChildQuestionID, 0, 0, "", "", 0, 0, 0, false, 0, false, "D");
        if (status == 3)
        {
            showMessages("Deleted successfully");
        }

        FormID = Int32.Parse(ddlForm.SelectedValue);
        ddlQuestionForm.SelectedValue = FormID.ToString();

        BindGVQuestionchild(Convert.ToString(ddlParentQuestion.SelectedValue), Convert.ToString(ddlForm.SelectedValue));
        //FillFlagDropDown(FormID);
    }

    protected void ChangePreferenceUP1(object sender, EventArgs e)
    {

        LinkButton lnkUp = sender as LinkButton;
        GridViewRow row = lnkUp.NamingContainer as GridViewRow;
        int index = row.RowIndex;
        int QuetionID, Sequence, QuetionIDPrefrence, SequencePrefrence, parentquestionid;


        parentquestionid = Int32.Parse(GvQuestionChild.DataKeys[index].Values["OrignalQuestionID"].ToString());
        QuetionID = Int32.Parse(GvQuestionChild.DataKeys[index].Values["OrignalChildQuestionID"].ToString());
        Sequence = Int32.Parse(GvQuestionChild.DataKeys[index].Values["Sequence"].ToString());
        QuetionIDPrefrence = Int32.Parse(GvQuestionChild.DataKeys[index - 1].Values["OrignalChildQuestionID"].ToString());
        SequencePrefrence = Int32.Parse(GvQuestionChild.DataKeys[index - 1].Values["Sequence"].ToString());
        DataTable dt = new DataTable();
        //  dt = objBLL.UpdatePreferenceChildQuestionBank(parentquestionid, QuetionID, Sequence, QuetionIDPrefrence, SequencePrefrence, Int32.Parse(ddlForm.SelectedValue));
        GvQuestionChild.DataSource = dt;
        GvQuestionChild.DataBind();
        lnkUplnkDown1();

    }

    protected void ChangePreferenceDown1(object sender, EventArgs e)
    {

        LinkButton lnkDown = sender as LinkButton;
        GridViewRow row = lnkDown.NamingContainer as GridViewRow;
        int index = row.RowIndex;
        int QuetionID, Sequence, QuetionIDPrefrence, SequencePrefrence, parentquestionid;
        parentquestionid = Int32.Parse(GvQuestionChild.DataKeys[index].Values["OrignalQuestionID"].ToString());
        QuetionID = Int32.Parse(GvQuestionChild.DataKeys[index].Values["OrignalChildQuestionID"].ToString());
        Sequence = Int32.Parse(GvQuestionChild.DataKeys[index].Values["Sequence"].ToString());
        QuetionIDPrefrence = Int32.Parse(GvQuestionChild.DataKeys[index + 1].Values["OrignalChildQuestionID"].ToString());
        SequencePrefrence = Int32.Parse(GvQuestionChild.DataKeys[index + 1].Values["Sequence"].ToString());
        DataTable dt = new DataTable();
        //dt = objBLL.UpdatePreferenceChildQuestionBank(parentquestionid, QuetionID, Sequence, QuetionIDPrefrence, SequencePrefrence, Int32.Parse(ddlForm.SelectedValue));
        GvQuestionChild.DataSource = dt;
        GvQuestionChild.DataBind();
        lnkUplnkDown1();
    }

    public void lnkUplnkDown1()
    {
        LinkButton lnkUpChild = (GvQuestionChild.Rows[0].FindControl("lnkUpChild") as LinkButton);
        LinkButton lnkDownChild = (GvQuestionChild.Rows[GvQuestionChild.Rows.Count - 1].FindControl("lnkDownChild") as LinkButton);
        lnkUpChild.Enabled = false;
        lnkUpChild.CssClass = "buttonDisable";
        lnkDownChild.Enabled = false;
        lnkDownChild.CssClass = "buttonDisable";
    }

    public void Get_mskvalidation()
    {
        DataTable dt = new DataTable();
        dt = Exec_Procedure("USP_GETmastValidation");
        ddlMaskValidation.DataSource = dt;
        ddlMaskValidation.DataTextField = "Value";
        ddlMaskValidation.DataValueField = "ID";
        ddlMaskValidation.DataBind();
        ddlMaskValidation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("----Select-----", "0"));
    }


    #region AnswerTypeChange

    public void Text()
    {

        ddlFlag.Attributes.Add("style", "display:block;");
        divmask.Attributes.Add("style", "display:block;");
        divMaster.Attributes.Add("style", "display:none;");
        divredirect.Attributes.Add("style", "display:none;");
        divMaxLenght.Attributes.Add("style", "display:block;");
        txtMaxLenght.Text = "50";
        div1Grop.Attributes.Add("style", "display:block;");
    }

    public void Numeric()
    {

        ddlFlag.Attributes.Add("style", "display:block;");
        divMaster.Attributes.Add("style", "display:none;");
        divredirect.Attributes.Add("style", "display:none;");
        divmask.Attributes.Add("style", "display:block;");
        divMaxLenght.Attributes.Add("style", "display:block;");
        txtMaxLenght.Text = "7";
        div1Grop.Attributes.Add("style", "display:block;");
    }
    public void Date()
    {

        ddlFlag.Attributes.Add("style", "display:block;");
        divMaster.Attributes.Add("style", "display:none;");
        divredirect.Attributes.Add("style", "display:none;");
        divmask.Attributes.Add("style", "display:none;");
        divMaxLenght.Attributes.Add("style", "display:none;");
        div1Grop.Attributes.Add("style", "display:block;");


    }
    public void SingleChoice()
    {
        divMaster.Attributes.Add("style", "display:block;");
        divredirect.Attributes.Add("style", "display:block;");
        divmask.Attributes.Add("style", "display:none;");
        divMaxLenght.Attributes.Add("style", "display:none;");
        div1Grop.Attributes.Add("style", "display:block;");
    }
    public void MultipleChoice()
    {
        divMaster.Attributes.Add("style", "display:block;");
        divredirect.Attributes.Add("style", "display:block;");
        divmask.Attributes.Add("style", "display:none;");
        divMaxLenght.Attributes.Add("style", "display:none;");
        div1Grop.Attributes.Add("style", "display:block;");
    }
    public void AfterImage()
    {
        divMaster.Attributes.Add("style", "display:none;");
        divredirect.Attributes.Add("style", "display:none;");
        divmask.Attributes.Add("style", "display:none;");
        divMaxLenght.Attributes.Add("style", "display:none;");
        div1Grop.Attributes.Add("style", "display:block;");

    }
    void Popup(bool isDisplay)
    {
        MPEFormName.Show();
    }
    protected void update_Question_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        { }
        else
        {
            Response.Redirect("Login.aspx");
        }
        LinkButton Edit_Question = sender as LinkButton;
        GridViewRow row = Edit_Question.NamingContainer as GridViewRow;
        int index = row.RowIndex;
        string Flag = GvQuestion.DataKeys[index].Values["Flag"].ToString();

        string QuestionID = GvQuestion.DataKeys[index].Values["QuestionID"].ToString();
        string QuestionAns = GvQuestion.DataKeys[index].Values["QuestionAns"].ToString();
        string strQry = "Select * from Tbl_Training_Ques inner join tbl_training_question on tbl_training_question.Tarining_ID=Tbl_Training_Ques.FormID where Createdate>='2026-04-01' and QuestionID=" + QuestionID + "   ";
        // string strQry = "Select * from Tbl_Training_Ques  where QuestionID=" + QuestionID + "   ";
        clsMain obm = new clsMain();

        DataTable dtRole = obm.LoadData(strQry);
        if (dtRole.Rows.Count > 0)
        {
            Button2.Visible = false;
        }
        else
        {
            if (Session["FinYear"].ToString() == ddlYear.SelectedItem.Text)
            {
                Button2.Visible = true;
            }
            else
            {
                Button2.Visible = false;
            }
        }

        Session["questionbankid"] = Flag;
        Session["FlagQuestionID"] = QuestionID;

        lblFormNamerr.Text = GvQuestion.DataKeys[index].Values["Question"].ToString();
        BindOptionData();
        int icount = 0;
        if (QuestionAns.Length > 0)
        {
            string[] words = QuestionAns.Trim().Split(',');
            foreach (var word in words)
            {
                foreach (GridViewRow row1 in GVOptions.Rows)
                {

                    Int32 ID = Convert.ToInt32(GVOptions.DataKeys[row1.RowIndex]["ID"].ToString());

                    CheckBox Chkbox = ((CheckBox)row1.FindControl("chkFormName"));
                    if (Convert.ToInt32(word) == ID)
                    {
                        Chkbox.Checked = true;
                        icount = icount + 1;
                    }


                }
            }
            if (Convert.ToInt32(GVOptions.Rows.Count) == icount)
            {
                CheckBox chkHeader = (CheckBox)GVOptions.HeaderRow.FindControl("chkHeader");
                chkHeader.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow row1 in GVOptions.Rows)
            {


                CheckBox Chkbox = ((CheckBox)row1.FindControl("chkFormName"));
                Chkbox.Checked = true;

            }
            CheckBox chkHeader = (CheckBox)GVOptions.HeaderRow.FindControl("chkHeader");
            chkHeader.Checked = true;
        }



        Popup(true);

        //txtMaxLenght.Text = GvQuestion.DataKeys[index].Values["MaxLenght"].ToString();



    }
    protected void GVOptions_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {

            Popup(true);
            GVOptions.EditIndex = -1;
            BindOptionData();
        }
        catch
        { }
    }
    protected void GVOptions_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {

            //Finding the controls from Gridview for the row which is going to update
            //
            string Flag = GVOptions.DataKeys[e.RowIndex].Values["Flag"].ToString();
            string UId = GVOptions.DataKeys[e.RowIndex].Values["UId"].ToString();
            TextBox Option = GVOptions.Rows[e.RowIndex].FindControl("txtOptions") as TextBox;
            TextBox txtScore = GVOptions.Rows[e.RowIndex].FindControl("txtScore") as TextBox;

            int UID = 0, flag, Score = 0, formid = 0;
            if (txtScore.Text.Trim() != "")
            {
                Score = Convert.ToInt32(txtScore.Text);
            }

            if (Option.Text.Trim() == "")
            {
                showMessages("Enter Value");
                return;
            }


            flag = Int32.Parse(Flag);
            UID = Int32.Parse(UId);
            int status = MasterOptionValueInsert(UID, Option.Text, flag, Convert.ToInt32(ddlForm.SelectedValue), "U", Score);

            if (status > 0)
            {
                showMessages("Save successfully");

            }

            //string  Opt = Option.Text.Trim();
            //lblMsg.Text = "Option Updated Successfully !!";
            DataTable dt = new DataTable();
            //dt = objBLL.Update_Options(UId, Option.Text.Trim());
            //if (dt.Rows.Count > 0)
            //{
            //    if (dt.Rows[0]["mStatus"].ToString() == "1")
            //    {
            //        lblMsg.Text = "Option Updated Successfully !!";
            //        btnClose.Text = "Close";
            //    }
            //    else
            //    {
            //        lblMsg.Text = "Option Not Updated !!";
            //    }
            //}
            //else
            //{
            //    lblMsg.Text = "Please try again !!";
            //}
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            GVOptions.EditIndex = -1;
            //Call ShowData method for displaying updated data  
            BindOptionData();
            Popup(true);
        }
        catch
        { }
    }
    public int MasterOptionValueInsert(int UID, string OptionValue, int flag, int formid, string sTran_Type, int Score)
    {
        SqlCommand dbSqlCommand;
        using (dbSqlCommand = new SqlCommand())
            dbSqlCommand.Connection = mycon;
        if (mycon.State == ConnectionState.Closed)
            mycon.Open();
        dbSqlCommand.CommandType = CommandType.StoredProcedure;
        dbSqlCommand.CommandText = "USP_MasterOptionValueInsert";
        dbSqlCommand.Parameters.Add("@UID", SqlDbType.Int).Value = UID;
        dbSqlCommand.Parameters.Add("@OptionValue", SqlDbType.NVarChar).Value = OptionValue;

        dbSqlCommand.Parameters.Add("@flag", SqlDbType.Int).Value = flag;
        dbSqlCommand.Parameters.Add("@formid", SqlDbType.VarChar).Value = formid;

        dbSqlCommand.Parameters.Add("@Tran_Type", SqlDbType.VarChar).Value = sTran_Type;
        dbSqlCommand.Parameters.Add("@Score", SqlDbType.Int).Value = Score;
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
    protected void BindOptionData()
    {
        try
        {
            DataTable dtOption = new DataTable();
            string tablename = "";
            if (ddlYear.SelectedItem.Text == "2026-2027")
            {
                tablename = "MSTCommon";
            }

            if (ddlYear.SelectedItem.Text == "2025-2026")
            {
                tablename = "MSTCommon2025";
            }

            if (ddlYear.SelectedItem.Text == "2024-2025")
            {
                tablename = "MSTCommon2024";
            }

            if (ddlYear.SelectedItem.Text == "2023-2024")
            {
                tablename = "MSTCommon2023";
            }

            dtOption = Select_All_Data(tablename, "UID,ID,Value,Score,Flag", "IsDeleted = 0 and FormID>0 and Flag = " + Session["questionbankid"] + " ", "ID", "");





            GVOptions.DataSource = dtOption;
            GVOptions.DataBind();

        }
        catch
        {

        }
    }
    protected void GVOptions_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {

            Popup(true);
            //NewEditIndex property used to determine the index of the row being edited. 
            GVOptions.EditIndex = e.NewEditIndex;
            BindOptionData();
        }
        catch
        { }
    }
    protected void btnParticipate_Click(object sender, EventArgs e)
    {
        string QuestionID = Convert.ToString(Session["FlagQuestionID"]);
        string FInalValues = "";
        foreach (GridViewRow row in GVOptions.Rows)
        {

            Int32 ID = Convert.ToInt32(GVOptions.DataKeys[row.RowIndex]["ID"].ToString());

            CheckBox Chkbox = ((CheckBox)row.FindControl("chkFormName"));
            if (Chkbox.Checked == true)
            {
                FInalValues += "" + ID + "" + ",";

            }


        }
        if (FInalValues.Length > 0)
        {
            FInalValues = FInalValues.Substring(0, FInalValues.LastIndexOf(","));
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
    new SqlParameter(
        "@QuestionAns",
        FInalValues
    ),

    new SqlParameter(
        "@QuestionID",
        Convert.ToInt32(QuestionID)
    )
            };

            int icount = SqlHelper.ExecuteNonQuery(
                       SqlHelper.mainConnectionString,
                       CommandType.StoredProcedure,
                       "USP_Update_MSTFormQuestion",
                       cmdParameters
                   );

            if (icount > 0)
            {
                showMessages("Save successfully");

            }
            int FormID = Int32.Parse(ddlForm.SelectedValue);
            BindGvQuestion(FormID);
        }
        else
        {
            showMessages("Please select checkbox");
            Popup(true);
        }

    }
    #endregion
    protected void btnsaveCAT_Click(object sender, EventArgs e)
    {




        if (ddlcLevel.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Training OutCome')</script>", false);
            MPE_Entry.Show();
            return;
        }
        if (ddlcLevel.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Training OutCome')</script>", false);
            MPE_Entry.Show();
            return;
        }


        if (txtcat.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Other')</script>", false);
            MPE_Entry.Show();
            return;
        }

        string strQry = "Select  * from mstQuestionCategory where CategoryName='" + txtcat.Text + "' and FormID=" + ddlcForm.SelectedValue + " and cDeleteFlag=1  ";
        // string strQry = "Select * from Tbl_Training_Ques  where QuestionID=" + QuestionID + "   ";
        clsMain obm = new clsMain();

        DataTable dtRole = obm.LoadData(strQry);
        if (dtRole.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('CategoryName already exists')</script>", false);
            MPE_Entry.Show();
            return;
        }


        int Tarining_ID = TrainingQuestionInsertUpdate(0, Convert.ToInt32(ddlcLevel.SelectedValue), Convert.ToInt32(ddlcForm.SelectedValue), txtcat.Text, Convert.ToString(Session["username"]), "1");


        if (Tarining_ID > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
            MPE_Entry.Show();
            Loadcat(Convert.ToInt32(ddlcForm.SelectedValue));
            FillFlagCategory(0);
        }


    }
    protected void ddlcForm_SelectedIndexChanged(object sender, EventArgs e)
    {

        Loadcat(Convert.ToInt32(ddlcForm.SelectedValue));
        MPE_Entry.Show();
    }
    public void Loadcat(int FormLevel)
    {
        DataTable dt = GetFormTableDetailscat(FormLevel);
        GvEntry.DataSource = dt;
        GvEntry.DataBind();
    }
    public DataTable GetFormTableDetailscat(int FormLevel)
    {
        DataTable dtBSL = new DataTable();
        dtBSL = null;
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                         new SqlParameter("@FormLevel",FormLevel),

                    };
            DataTable ds = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Form_Table_Category", paramvT);
            dtBSL = ds;
        }
        catch
        { DataTable ds = new DataTable(); ds = null; return ds; }
        return dtBSL;
    }
    protected void btncDelete_Click(object sender, EventArgs e)
    {
        LinkButton ddlLabTest1 = (LinkButton)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        Label lblCat = (Label)row1.FindControl("lblCat");

        string strQry = "Select  * from MSTFormQuestion where QCategoryID=" + lblCat.Text + "   ";
        // string strQry = "Select * from Tbl_Training_Ques  where QuestionID=" + QuestionID + "   ";
        clsMain obm = new clsMain();

        DataTable dtRole = obm.LoadData(strQry);
        if (dtRole.Rows.Count > 0)
        {
            showMessages("You can not  Deleted because Question link in training");
            MPE_Entry.Show();
            return;
        }
        else
        {

            int Tarining_ID = TrainingQuestionInsertUpdate(Convert.ToInt32(lblCat.Text), Convert.ToInt32(ddlcLevel.SelectedValue), Convert.ToInt32(ddlcForm.SelectedValue), txtcat.Text, Convert.ToString(Session["username"]), "2");
            MPE_Entry.Show();
            ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);
            Loadcat(Convert.ToInt32(ddlcForm.SelectedValue));
            FillFlagCategory(0);
        }
        //btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
        //if (ViewState["Tarining_ID"] != null)
        //{
        //    int res1 = DeleteTBTraingAssment(ViewState["Tarining_ID"].ToString(), Session["username"].ToString());



        //    if (res1 > 0)
        //    {

        //        btnAdd_Click(btnAdd, null);
        //        GVMainBind();
        //        ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

        //    }

        //}


    }

    public int TrainingQuestionInsertUpdate(int CategoryID, int AssemnentType, int FormID, string CategoryName, string EntryBy, string Flag)
    {
        SqlCommand dbSqlCommand;
        using (dbSqlCommand = new SqlCommand())
            dbSqlCommand.Connection = mycon;
        if (mycon.State == ConnectionState.Closed)
            mycon.Open();
        dbSqlCommand.CommandType = CommandType.StoredProcedure;
        dbSqlCommand.CommandText = "InsertUpdateQuestionCategory";
        dbSqlCommand.Parameters.Add("@CategoryID", SqlDbType.Int).Value = @CategoryID;
        dbSqlCommand.Parameters.Add("@AssemnentType", SqlDbType.Int).Value = AssemnentType;
        dbSqlCommand.Parameters.Add("@FormID", SqlDbType.Int).Value = FormID;

        dbSqlCommand.Parameters.Add("@CategoryName", SqlDbType.NVarChar).Value = CategoryName;
        dbSqlCommand.Parameters.Add("@CreateBy", SqlDbType.VarChar).Value = EntryBy;
        dbSqlCommand.Parameters.Add("@Flag", SqlDbType.VarChar).Value = Flag;

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
    protected void lnkbtn1_Click(object sender, EventArgs e)
    {

        if (ddlForm.SelectedIndex > 0)
        {
            MPEFormNameQ.Show();
            DataTable dt = new DataTable();
            dt = Select_All_Data("MSTCommon", "UID,ID,Value", "IsDeleted = 0 and Flag = 0 and FormID >0 and mYear='" + Convert.ToString(Session["FinYear"]) + "' ", " uid desc", "");
            GVANs.DataSource = dt;
            GVANs.DataBind();
        }
    }
    protected void EditOptionValue_Click(object sender, EventArgs e)
    {
        LinkButton EditOptionValue = sender as LinkButton;
        GridViewRow row = EditOptionValue.NamingContainer as GridViewRow;
        int index = row.RowIndex;

        string v = GVANs.DataKeys[index].Values["UID"].ToString();
        ddlFlag.SelectedValue = v.ToString();

    }


}