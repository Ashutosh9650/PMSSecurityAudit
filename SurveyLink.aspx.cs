using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SurveyLink : System.Web.UI.Page
{
    SqlConnection mycon = new SqlConnection(SqlHelper.mainConnectionString);
    public static string STRPRINTCONTENT;

    static string prevPage = String.Empty;

    static int EssionFormID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Convert.ToString(Session["username"]) != "")
        {
            if (!IsPostBack)
            {


                FillDropdown();
                Session["dtParticiparticipate"] = null;


            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }

    }
    private void FillDropdown()
    {
        DataTable dt1 = Exec_Procedure("USP_GetLevel");
        ddlLevel.DataSource = dt1;
        ddlLevel.DataValueField = "id";
        ddlLevel.DataTextField = "Value";
        ddlLevel.DataBind();
        ddlLevel.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" --Select Level-- ", "0"));


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
    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        int FormLevl = Int32.Parse(ddlLevel.SelectedValue.ToString());

        FillFormNameNew(FormLevl);

    }

    protected void LnkImport_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        if (ddlForm.SelectedIndex > 0)
        {
            DataTable dtHeader = Get_DataFor2FilterReport("rptSurvey", ddlForm.SelectedValue.ToString(), "1");
            exportTABLE_COMPLETE(dtHeader);


        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Showalert", "alert('Please select Survey');", true);
        }

        //  MPEFormName1.Show();

    }

    public DataTable Get_DataFor2FilterReport(string ProcedureName, string Filter1, string Filter2)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                            new SqlParameter("@FromID",Filter1),
                             new SqlParameter("@Flag",Filter2),

                    };
            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, ProcedureName, paramvT);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch (Exception)
        {

        }
        return dtcombo;
    }
    private void exportTABLE_COMPLETE(DataTable dt)
    {
        DataTable dtExp_Data = new DataTable();
        DataTable dtHeader = new DataTable();
        dtHeader = Get_DataFor2FilterReport("rptSurvey", ddlForm.SelectedValue.ToString(), "2");
        dtExp_Data = dt;
        String name = "Survey form " + ddlForm.SelectedItem.ToString() + "_" + DateTime.Now.ToString() + ".xls";
        HttpResponse response = HttpContext.Current.Response;
        response.Clear();
        response.Charset = "";
        response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = System.Text.Encoding.Unicode;
        Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
        response.AddHeader("Content-Disposition", "attachment;filename=\"" + name + "\"");
        System.Text.StringBuilder sbb = new System.Text.StringBuilder();

        sbb.Append("<html>");
        sbb.Append("<Table  border=1>");

        sbb.Append("<tr style='backcolor=red'>");
        for (int k = 0; k < dtHeader.Rows.Count; k++)
        {
            sbb.Append("<td align=center style=\"font-weight:bold;BACKGROUND-COLOR: #dcdcdc;FONT-SIZE: 11pt;\"><b>");
            sbb.Append(dtHeader.Rows[k][0]);
            sbb.Append("</b></td>");
        }
        sbb.Append("</tr>");
        sbb.Append("<tr style='backcolor=red'>");
        for (int i = 0; i < dtExp_Data.Columns.Count; i++)
        {
            sbb.Append("<td align=center style=\"font-weight:bold;BACKGROUND-COLOR: #dcdcdc;FONT-SIZE: 11pt;\"><b>");
            sbb.Append(dtExp_Data.Columns[i].ColumnName);
            sbb.Append("</b></td>");
        }
        sbb.Append("</tr>");

        for (int i = 0; i < dtExp_Data.Rows.Count; i++)
        {
            sbb.Append("<tr style='backcolor=red'>");
            for (int j = 0; j < dtExp_Data.Columns.Count; j++)
            {
                string CellValueFirstTD = dtExp_Data.Rows[i][j].ToString();
                string[] tokens = CellValueFirstTD.Split(',');
                string firstString = tokens[0];
                string last = firstString.Substring(firstString.LastIndexOf(',') + 1);

                if (firstString.Contains(".jpg") || firstString.Contains(".png") || firstString.Contains(".jpeg") || firstString.Contains(".gif"))
                {
                    string http = "https://testpms.educategirls.ngo/SurveyAns/" + firstString + "";
                    // string http = "http://survey.microwarecomp.com/Documents/Docs/";
                    // sbb.Append("<td align=Left style='FONT-SIZE: 10pt'>" + http + "" + firstString + " </td>");

                    sbb.Append("<td align=Left style='FONT-SIZE: 10pt'><img width='7%' height='5%'  src='" + http + "'    alt=''/> </td>");
                }
                else
                {
                    sbb.Append("<td align=Left style='FONT-SIZE: 10pt'>" + firstString + "</td>");
                }
                //sbb.Append("<td align=Left style='FONT-SIZE: 10pt'>" + dtExp_Data.Rows[i][j].ToString() + "</td>");
            }
            sbb.Append("</tr>");
        }
        sbb.Append("</Table>");
        sbb.Append("</html>");

        response.Write(sbb);
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
        //response.End();

    }
    public void FillFormNameNew(int FormLevel)
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
            dt = Get_DataFor3Filter("USP_GetSurveyOnAgencyAndLevelFormLinkChange", "", FormLevel.ToString(), "");
            //dt = objBLL.Select_All_Data("MSTForm", "FormID,FormName", "IsDeleted = 0 and FormLevel = " + FormLevel  + " ", "", "");

        }

        ddlForm.DataSource = dt;
        ddlForm.DataTextField = "FormName";
        ddlForm.DataValueField = "FormID";
        ddlForm.DataBind();
        ddlForm.Items.Insert(0, new System.Web.UI.WebControls.ListItem("------Select-------", "0"));



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
    protected void ddlForm_SelectedIndexChanged(object sender, EventArgs e)
    {
        int FormLevl = Int32.Parse(ddlForm.SelectedValue.ToString());

        BindGvQuestion(FormLevl);
    }
    protected void gvnroll_OnRowCommand(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Label lblUniqueChildCode = (Label)e.Row.FindControl("lblUniqueChildCode");

            Image lbtn = (Image)e.Row.FindControl("imgMKSG");

            Label lblQuestionType = (Label)e.Row.FindControl("lblQuestionType");
            Label lblQuestion = (Label)e.Row.FindControl("lblQuestion");
            if (lblQuestionType.Text == "2")
            {
                lbtn.Visible = true;
                lblQuestion.Visible = false;

                lbtn.ImageUrl = ResolveUrl("~/Survey/" + lblQuestion.Text);
            }
            else
            {
                lbtn.Visible = false;
                lblQuestion.Visible = true;
            }
        }



    }

    protected void BindGvQuestion(int FormID)
    {

        DataTable dtQuestion = new DataTable();
        DataTable dtFormLinked = new DataTable();

        //dtQuestion = objBLL.Select_All_Data("MSTFormQuestion", "QuestionID,QuestionNo,Question,QuestionFieldName,QestionTypeID,Sequence,Flag,IsQuestionMandatory,MaxLenght,MaskValidation", "IsDeleted = 0 and FormID = " + FormID + " ", "Sequence", "");
        dtQuestion = Get_DataFor1Filter("USP_GetMSTFormQuestionOnForm1Link", FormID.ToString());

        GvQuestion.Visible = true;
        GvQuestion.DataSource = dtQuestion;
        GvQuestion.DataBind();

        if (dtQuestion.Rows.Count > 0)
        {
            lnkUplnkDown();
        }
        //dtFormLinked = objBLL.Select_All_Data("formProject", "ProjectID", "FormID = " + FormID + " ", "", "");
        dtFormLinked = Get_DataFor1Filter("USP_GetformProjectOnForm", FormID.ToString());


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


        //GvQuestion.DataSource = dt;
        //GvQuestion.DataBind();
        lnkUplnkDown();
        BindGvQuestion(Convert.ToInt32(ddlForm.SelectedValue));

    }
    protected void ChangePreferenceDown(object sender, EventArgs e)
    {
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


        //GvQuestion.DataSource = dt;
        //GvQuestion.DataBind();
        lnkUplnkDown();
        BindGvQuestion(Convert.ToInt32(ddlForm.SelectedValue));
    }
    protected void update_Question_Click(object sender, EventArgs e)
    {
        LinkButton Edit_Question = sender as LinkButton;
        GridViewRow row = Edit_Question.NamingContainer as GridViewRow;
        int index = row.RowIndex;
        string Flag = GvQuestion.DataKeys[index].Values["Flag"].ToString();
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
            DataTable ds = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "USP_UpdatePreferencetraiing", paramvT);
            dtBSL = ds;
        }
        catch (Exception)
        { }
        return dtBSL;

    }
    public DataTable UpdatePreferenceNew(int QuetionID, int Sequence, int FormID)
    {
        DataTable dtBSL = new DataTable();
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                         new SqlParameter("@QuetionID ",QuetionID),
                         new SqlParameter("@Sequence ",Sequence),

                         new SqlParameter("@FormID ",FormID),
                    };
            DataTable ds = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "USP_UpdatePreferencetraiingNew", paramvT);
            dtBSL = ds;
        }
        catch (Exception)
        { }
        return dtBSL;

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int editsqunce = Convert.ToInt32(txtEditSequence.Text);
        DataTable dt = new DataTable();
        dt = UpdatePreferenceNew(Convert.ToInt32(hdnNrmlquestionid.Value), editsqunce, Int32.Parse(ddlForm.SelectedValue));

        //int editsqunce = Convert.ToInt32(txtEditSequence.Text);
        //int status;
        //status = objBLL.UpdateSequenceInQuestion(Convert.ToInt32(ddlnrmlform.SelectedValue), Convert.ToInt32(hdnNrmlquestionid.Value), Convert.ToInt32(hdnQuestionbankid.Value), editsqunce);

        if (dt.Rows.Count > 0)
        {
            showMessages("Sequence Updated On Survey successfully");
        }

        //FillGridDataForQuestion();
        lblDependQuest.Text = "";
        txtprsntSequence.Text = "";
        txtEditSequence.Text = "";
        BindGvQuestion(Convert.ToInt32(ddlForm.SelectedValue));


    }
    private void showMessages(string messages)
    {
        lbl_messages.Text = "";
        lbl_messages.Text = messages;
        ModalAlert.Show();
    }

    protected void Edit_Question_Click(object sender, EventArgs e)
    {
        LinkButton Edit_Question = sender as LinkButton;
        GridViewRow row = Edit_Question.NamingContainer as GridViewRow;
        int index = row.RowIndex;
        divquestion.Visible = true;
        DivPrsntSquence.Visible = true;
        DivEditSqunce.Visible = true;

        lblDependQuest.Text = GvQuestion.DataKeys[index].Values["QuestionNo"].ToString() + ' ' + '-' + ' ' + GvQuestion.DataKeys[index].Values["Question"].ToString();
        txtprsntSequence.Text = GvQuestion.DataKeys[index].Values["Sequence"].ToString();

        hdnNrmlquestionid.Value = GvQuestion.DataKeys[index].Values["QuestionID"].ToString();


        MPEFormName.Show();
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
    public void FillFormName(int FormLevel)
    {
        DataTable dt = new DataTable();
        //int FormLevel;
        if (FormLevel != 0 || FormLevel != -1)
        {
            //dt = objBLL.Get_DataFor1Filter()
            dt = GetFormTableDetails(FormLevel, Convert.ToString(Session["UserID"]));
        }
        else
        {
            //dt = objBLL.Select_All_Data("MSTForm", "FormLevel,FormID,FormName", "IsDeleted = 0 and FormLevel = " + FormLevel + " ", "", "");
        }

        //GVFormName.DataSource = dt;
        //GVFormName.DataBind();

    }
    public DataTable GetFormTableDetails(int FormLevel, string userid)
    {
        DataTable dtBSL = new DataTable();
        dtBSL = null;
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                         new SqlParameter("@FormLevel",FormLevel),
                          new SqlParameter("@Userid",userid),
                    };
            DataTable ds = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Form_Table_Deatilslink", paramvT);
            dtBSL = ds;
        }
        catch
        { DataTable ds = new DataTable(); ds = null; return ds; }
        return dtBSL;
    }
    //protected void btnParticipate_Click(object sender, EventArgs e)
    //{
    //    DataTable dtParticiparticipate = null;
    //   if (Session["dtParticiparticipate"]!=null)
    //      {
    //        dtParticiparticipate = ((DataTable)Session["dtParticiparticipate"]);

    //    }
    //   else
    //    {
    //        dtParticiparticipate = CreateDataDate();
    //    }
    //   if (txtParticipate.Text!="")
    //    {
    //        string[] words = txtParticipate.Text.Trim().Split(',');
    //        foreach (var word in words)
    //        {
    //            if (word.Length > 3)
    //            {
    //                DataRow[] drmain = dtParticiparticipate.Select("ParticiparticipateName='" + word.Trim() + "'");
    //                if (drmain.Length > 0)
    //                {

    //                }
    //                else
    //                {
    //                    DataRow dr;
    //                    dr = dtParticiparticipate.NewRow();
    //                    dr["ParticiparticipateName"] = word.Trim();
    //                    dr["FormID"] = string.Empty;
    //                    dtParticiparticipate.Rows.Add(dr);
    //                }
    //            }
    //        }
    //    }
    //    Session["dtParticiparticipate"] = dtParticiparticipate;
    //    GridView1.DataSource = dtParticiparticipate;
    //    GridView1.DataBind();
    //    MPEFormName1.Show();
    //}

    //protected void Delete_Question_Click(object sender, EventArgs e)
    //{
    //    //MPEFormName.Show();

    //    LinkButton Edit_Question = sender as LinkButton;
    //    GridViewRow row = Edit_Question.NamingContainer as GridViewRow;
    //    int index = row.RowIndex;


    //   string QuestionID = (GridView1.DataKeys[index].Values["ParticiparticipateName"].ToString());
    //    DataTable dtParticiparticipate = null;

    //  dtParticiparticipate = ((DataTable)Session["dtParticiparticipate"]);
    //    dtParticiparticipate.Rows.Remove(dtParticiparticipate.Rows[index]);

    //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Delete Sucessfully')</script>", false);

    //    Session["dtParticiparticipate"] = dtParticiparticipate;
    //    GridView1.DataSource = dtParticiparticipate;
    //    GridView1.DataBind();
    //    MPEFormName1.Show();
    //}
    //public DataTable CreateDataDate()
    //{

    //    DataTable dtParticiparticipate = new DataTable();


    //    dtParticiparticipate.Columns.Add(new DataColumn("FormID", System.Type.GetType("System.String")));
    //    dtParticiparticipate.Columns.Add(new DataColumn("ParticiparticipateName", System.Type.GetType("System.String")));
    //    Session["dtParticiparticipate"]= dtParticiparticipate;
    //    return dtParticiparticipate;
    //}
}