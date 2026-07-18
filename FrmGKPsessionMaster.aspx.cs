using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class FrmGKPsessionMaster : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    string conditions = string.Empty, Flag = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadYear();
            if (Convert.ToString(Session["username"]) != "")
            {

                GridBindGKPMaster();

            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }

        }
    }

    public void LoadYear()
    {
        DataTable dtYear = objComman.Generate_Financial_Year();
        ddlYear.SelectedIndex = 1;
    }


    protected void btnAddSession_click(object sender, EventArgs e)
    {
        if (ddlMastertype.SelectedValue == "1")
        {
            HdnGKIPAID.Value = "";
            HdnGKIPID.Value = "";
            Clear();
            btnSave.Text = "Save";

            ModalAddGKPSession.Show();
        }
        else
        {
            Clear1();
            ModalAddGKPAssesment.Show();
            BtnGPKA.Text = "Save";
            HdnGKIPAID.Value = "";
            HdnGKIPID.Value = "";

        }
    }
    protected void btnAddAssessment_click(object sender, EventArgs e)
    {
        Clear1();
        ModalAddGKPAssesment.Show();
        BtnGPKA.Text = "Save";
    }

    public void Clear()
    {
        ddlSchoolGKP.SelectedValue = "0";
        ddlGKPLevel.SelectedValue = "0";
        txtMainSession.Text = "";
        TxtBaselineSession.Text = "";
        TxtRevisionSession.Text = "";
        TxtRemedial.Text = "";
        TxtRecapSession.Text = "";
        TxtEndline.Text = "";

        btnSave.Text = "Save";

    }
    public void Clear1()
    {
        DDlSubject.SelectedValue = "0";
        TxtGKPAssessmentQuestions.Text = "";
        TxtGKPMicroskillQuestion.Text = "";
        TxtMaxScoreAssessment.Text = "";
        TxtMaxScoreMicroskill.Text = "";
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
        ModalAddGKPSession.Show();
    }
    protected void btnGPKA_Clear_Click(object sender, EventArgs e)
    {
        Clear1();
        BtnGPKA.Text = "Save";
        ModalAddGKPAssesment.Show();
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        GridBindGKPMaster();
    }
    protected void ddlMastertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridBindGKPMaster();
    }
    protected void GKP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.DataItem != null)
            {

                DropDownList ddlFl = (DropDownList)e.Row.FindControl("ddlSchoolGKP");
                // DropDownList ddlGKPLevel = (DropDownList)e.Row.FindControl("ddlGKPLevel");
                //ddlFl.DataTextField = "Dip";
                //ddlFl.DataValueField = "Dip";
                //ddlFl.DataSource = RetrieveSubCategories();
                //ddlFl.DataBind();
                DataRowView dr = e.Row.DataItem as DataRowView;
                ddlFl.SelectedValue = dr["GKPLevelID"].ToString();
                /// ddlGKPLevel.Items.FindByText(dr["GKPLevel"].ToString()).Selected = true;
                //ddlGKPLevel.SelectedValue = dr["GKPLevel"].ToString();
            }
        }
    }


    protected void GKP1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.DataItem != null)
            {

                DropDownList DDlSubject = (DropDownList)e.Row.FindControl("DDlSubject");

                //ddlFl.DataValueField = "Dip";
                //ddlFl.DataSource = RetrieveSubCategories();
                //ddlFl.DataBind();
                DataRowView dr = e.Row.DataItem as DataRowView;
                DDlSubject.SelectedValue = dr["SubjectID"].ToString();
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //string UserName = Session["username"].ToString();
            int GKIPID = 0; int GKPLevelID = 0; int MainSession = 0; int BaselineSession = 0;
            int RevisionSession = 0; int Remedial = 0; int RecapSession = 0; int RecapSession1 = 0; int Endline = 0;
            string GKPLevel = "", Flag = "";
            string KG = "";
            if (Convert.ToInt32(ddlGKPLevel.SelectedValue) == 0)
            {
                KG = "Level 0";
            }
            if (Convert.ToInt32(ddlGKPLevel.SelectedValue) == 1)
            {
                KG = "Level 1";
            }
            if (Convert.ToInt32(ddlGKPLevel.SelectedValue) == 2)
            {
                KG = "Level 2";
            }
            if (Convert.ToInt32(ddlGKPLevel.SelectedValue) == 3)
            {
                KG = "Level 3";
            }
            if (HdnGKIPID.Value == "")
            {
                GKIPID = 0;
            }
            else
            {
                GKIPID = Convert.ToInt32(HdnGKIPID.Value);
            }

            if (GKIPID != 0)
            {

                Flag = "U";

                int Result = 0;
                GKPLevelID = Convert.ToInt32(ddlSchoolGKP.SelectedValue);
                GKPLevel = Convert.ToString(ddlGKPLevel.SelectedItem.Text);
                MainSession = Convert.ToInt32(txtMainSession.Text);
                BaselineSession = Convert.ToInt32(TxtBaselineSession.Text);
                RevisionSession = Convert.ToInt32(TxtRevisionSession.Text);
                Remedial = Convert.ToInt32(TxtRemedial.Text);
                RecapSession = Convert.ToInt32(TxtRecapSession.Text);
                RecapSession1 = Convert.ToInt32(TxtRecapSession1.Text);
                Endline = Convert.ToInt32(TxtEndline.Text);

                Result = InsertUpdateGKPMaster(GKIPID, GKPLevelID, GKPLevel, MainSession, BaselineSession, RevisionSession,
                       Remedial, RecapSession, Endline, Flag, RecapSession1, KG);
                if (Result > 0)
                {
                    Clear();
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Sucessfully')</script>", false);
                    ddlMastertype.SelectedValue = "1";
                    GridBindGKPMaster();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Unsuccesfull')</script>", false);
                }



            }
            else // Insert
            {
                Flag = "I";

                GKPLevelID = Convert.ToInt32(ddlSchoolGKP.SelectedValue);
                GKPLevel = Convert.ToString(ddlGKPLevel.SelectedItem.Text);
                MainSession = Convert.ToInt32(txtMainSession.Text);
                BaselineSession = Convert.ToInt32(TxtBaselineSession.Text);
                RevisionSession = Convert.ToInt32(TxtRevisionSession.Text);
                Remedial = Convert.ToInt32(TxtRemedial.Text);
                RecapSession = Convert.ToInt32(TxtRecapSession.Text);
                Endline = Convert.ToInt32(TxtEndline.Text);
                RecapSession1 = Convert.ToInt32(TxtRecapSession1.Text);
                int Result = 0;
                Result = InsertUpdateGKPMaster(GKIPID, GKPLevelID, GKPLevel, MainSession, BaselineSession, RevisionSession,
                       Remedial, RecapSession, Endline, Flag, RecapSession1, KG);


                if (Result > 0)
                {
                    Clear();
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                    ddlMastertype.SelectedValue = "1";
                    GridBindGKPMaster();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Unsuccesfull')</script>", false);
                }
            }


        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public int InsertUpdateGKPMaster(int GKIPID, int GKPLevelID, string GKPLevel, int MainSession, int BaselineSession, int RevisionSession, int Remedial, int RecapSession, int Endline, string Flag, int RecapSession1, string GKPLevelEn)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
                    new SqlParameter("@GKIPID", GKIPID),
                    new SqlParameter("@GKPLevelID", GKPLevelID),
                    new SqlParameter("@GKPLevel", GKPLevel),
                    new SqlParameter("@MainSession", MainSession),
                    new SqlParameter("@BaselineSession", BaselineSession),
                    new SqlParameter("@RevisionSession", RevisionSession),
                    new SqlParameter("@Remedial", Remedial),
                    new SqlParameter("@RecapSession", RecapSession),
                         new SqlParameter("@RecapSession1", RecapSession1),
                    new SqlParameter("@Endline", Endline),
                       new SqlParameter("@GKPLevelEn", GKPLevelEn),

                    new SqlParameter("@Flag", Flag)

    };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateGKPMasterData", cmdParameters);
    }


    protected void BtnGPKA_Click(object sender, EventArgs e)
    {
        try
        {

            int GKIPAID = 0; int SubjectID = 0;

            string GKPAssessmentQuestions = "", GKPMicroskillQuestion = "", MaxScoreAssessment = "", MaxScoreMicroskill = "", Flag = "";

            if (HdnGKIPAID.Value == "")
            {
                GKIPAID = 0;
            }
            else
            {
                GKIPAID = Convert.ToInt32(HdnGKIPAID.Value);
            }

            if (GKIPAID != 0)
            {


                Flag = "U";

                int Result = 0;

                SubjectID = Convert.ToInt32(DDlSubject.SelectedValue);
                GKPAssessmentQuestions = Convert.ToString(TxtGKPAssessmentQuestions.Text);
                GKPMicroskillQuestion = Convert.ToString(TxtGKPMicroskillQuestion.Text);
                MaxScoreAssessment = Convert.ToString(TxtMaxScoreAssessment.Text);
                MaxScoreMicroskill = Convert.ToString(TxtMaxScoreMicroskill.Text);

                Result = InsertUpdateGKPAssesmentMaster(GKIPAID, SubjectID, GKPAssessmentQuestions, GKPMicroskillQuestion, MaxScoreAssessment, MaxScoreMicroskill, Flag);
                if (Result > 0)
                {
                    Clear1();
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Sucessfully')</script>", false);
                    ddlMastertype.SelectedValue = "2";
                    GridBindGKPMaster();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Unsuccesfull')</script>", false);
                }



            }

            else // Insert
            {
                Flag = "I";

                SubjectID = Convert.ToInt32(DDlSubject.SelectedValue);
                GKPAssessmentQuestions = Convert.ToString(TxtGKPAssessmentQuestions.Text);
                GKPMicroskillQuestion = Convert.ToString(TxtGKPMicroskillQuestion.Text);
                MaxScoreAssessment = Convert.ToString(TxtMaxScoreAssessment.Text);
                MaxScoreMicroskill = Convert.ToString(TxtMaxScoreMicroskill.Text);

                int Result = 0;
                Result = InsertUpdateGKPAssesmentMaster(GKIPAID, SubjectID, GKPAssessmentQuestions, GKPMicroskillQuestion, MaxScoreAssessment, MaxScoreMicroskill, Flag);

                if (Result > 0)
                {
                    Clear1();
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                    ddlMastertype.SelectedValue = "2";
                    GridBindGKPMaster();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Unsuccesfull')</script>", false);
                }
            }



        }
        catch (Exception ex)
        {
            {
                throw;
            }

        }
    }

    public int InsertUpdateGKPAssesmentMaster(int GKIPAID, int SubjectID, string GKPAssessmentQuestions, string GKPMicroskillQuestion, string MaxScoreAssessment, string MaxScoreMicroskill, string Flag)
    {

        SqlParameter[] cmdParameters = new SqlParameter[]
        {
                    new SqlParameter("@GKIPAID", GKIPAID),
                    new SqlParameter("@SubjectID", SubjectID),
                    new SqlParameter("@GKPAssessmentQuestions", GKPAssessmentQuestions),
                    new SqlParameter("@GKPMicroskillQuestion", GKPMicroskillQuestion),
                    new SqlParameter("@MaxScoreAssessment", MaxScoreAssessment),
                    new SqlParameter("@MaxScoreMicroskill", MaxScoreMicroskill),
                    new SqlParameter("@Flag", Flag)

    };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateGKPAssesmentMasterData", cmdParameters);

    }
    public DataTable GridBindGKPSession()
    {
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GetBindGKPSession");
        return dt;
    }
    public DataTable GridBindGKPAssesment()
    {
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GetBindGKPAssesment");
        return dt;
    }
    public void GridBindGKPMaster()
    {
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        if (ddlMastertype.SelectedValue == "1")
        {
            dt1 = GridBindGKPSession();
            Session["dtGridGKPSession"] = dt1;
            GridGKPSession.DataSource = (DataTable)dt1;
            GridGKPSession.DataBind();
            GridGKPAssessment.Visible = false;
            GridGKPSession.Visible = true;
        }
        else if (ddlMastertype.SelectedValue == "2")
        {
            dt2 = GridBindGKPAssesment();
            Session["dtGridGKPAssessment"] = dt2;
            GridGKPAssessment.DataSource = (DataTable)dt2;
            GridGKPAssessment.DataBind();
            GridGKPSession.Visible = false;
            GridGKPAssessment.Visible = true;
        }
    }

    protected void GridGKPSessionMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridGKPSession.DataSource = (DataTable)Session["dtGridGKPSession"];
            GridGKPSession.PageIndex = e.NewPageIndex;
            GridGKPSession.DataBind();
        }
        catch
        {
            throw;
        }
    }
    protected void GridGKPAssessmentMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridGKPAssessment.DataSource = (DataTable)Session["dtGridGKPAssessment"];
            GridGKPAssessment.PageIndex = e.NewPageIndex;
            GridGKPAssessment.DataBind();
        }
        catch
        {
            throw;
        }
    }


    protected void GridGKPSession_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName.Equals("EditData"))
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                HdnGKIPID.Value = this.GridGKPSession.DataKeys[index]["GKIPID"].ToString();
                ddlSchoolGKP.SelectedValue = this.GridGKPSession.DataKeys[index]["GKPLevelID"].ToString();

                ddlGKPLevel.SelectedValue = (this.GridGKPSession.DataKeys[index]["SchoolL"].ToString());

                txtMainSession.Text = this.GridGKPSession.DataKeys[index]["MainSession"].ToString();
                TxtBaselineSession.Text = this.GridGKPSession.DataKeys[index]["BaselineSession"].ToString();
                TxtRevisionSession.Text = this.GridGKPSession.DataKeys[index]["RevisionSession"].ToString();
                TxtRemedial.Text = this.GridGKPSession.DataKeys[index]["Remedial"].ToString();
                TxtRecapSession.Text = this.GridGKPSession.DataKeys[index]["RecapSession"].ToString();
                TxtEndline.Text = this.GridGKPSession.DataKeys[index]["Endline"].ToString();

                TxtRecapSession1.Text = this.GridGKPSession.DataKeys[index]["RemedialL1"].ToString();
                btnSave.Text = "Update";
                ModalAddGKPSession.Show();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        if (e.CommandName.Equals("DelefffteData"))
        {
            try
            {
                int GKPID = 0;
                int index = Convert.ToInt32(e.CommandArgument);
                HdnGKIPID.Value = GridGKPSession.DataKeys[index]["GKIPID"].ToString();
                int GKPID1 = Convert.ToInt32(HdnGKIPID.Value);
                string Flag = "D";
                // int Result = 0;
                int Result = InsertUpdateGKPMaster(GKPID1, 0, "", 0, 0, 0, 0, 0, 0, Flag, 0, "");

                if (Result > 0)
                {
                    if (Flag == "D")
                    {
                        Clear();
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('GKP Master Data Deleted sucessfully')</script>", false);
                        GridBindGKPMaster();
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }

    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        ImageButton bt = (ImageButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;


        string UniqueChildCode = (gvr.FindControl("lblCUniqueChildCode") as Label).Text;
        string Flag = "D";

        int Result = InsertUpdateGKPMaster(Convert.ToInt32(UniqueChildCode), 0, "", 0, 0, 0, 0, 0, 0, Flag, 0, "");


        if (Result > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('GKP Master Data Deleted sucessfully')</script>", false);
            GridBindGKPMaster();
        }


    }
    protected void btn_Delete_Click1(object sender, EventArgs e)
    {
        ImageButton bt = (ImageButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;


        string UniqueChildCode = (gvr.FindControl("lblCUniqueChildCode") as Label).Text;
        string Flag = "D";
        int Result = InsertUpdateGKPAssesmentMaster(Convert.ToInt32(UniqueChildCode), 0, "", "", "", "", Flag);




        if (Result > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('GKP Master Data Deleted sucessfully')</script>", false);
            GridBindGKPMaster();
        }


    }
    protected void GridGKPAssessment_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName.Equals("EditData"))
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                HdnGKIPAID.Value = this.GridGKPAssessment.DataKeys[index]["GKIPAID"].ToString();
                DDlSubject.SelectedValue = this.GridGKPAssessment.DataKeys[index]["SubjectID"].ToString();
                TxtGKPAssessmentQuestions.Text = this.GridGKPAssessment.DataKeys[index]["GKPAssessmentQuestions"].ToString();
                TxtGKPMicroskillQuestion.Text = this.GridGKPAssessment.DataKeys[index]["GKPMicroskillQuestion"].ToString();
                TxtMaxScoreAssessment.Text = this.GridGKPAssessment.DataKeys[index]["MaxScoreAssessment"].ToString();
                TxtMaxScoreMicroskill.Text = this.GridGKPAssessment.DataKeys[index]["MaxScoreMicroskill"].ToString();

                BtnGPKA.Text = "Update";
                ModalAddGKPAssesment.Show();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        if (e.CommandName.Equals("DelefffteData1"))
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                HdnGKIPAID.Value = GridGKPAssessment.DataKeys[index]["GKIPAID"].ToString();
                int GKIPAID = Convert.ToInt32(HdnGKIPAID.Value);
                string Flag = "D";
                string UserName = Session["username"].ToString();
                int Result = InsertUpdateGKPAssesmentMaster(GKIPAID, 0, "", "", "", "", Flag);

                if (Result > 0)
                {
                    if (Flag == "D")
                    {
                        Clear();
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('GKP Master Data Deleted sucessfully')</script>", false);
                        GridBindGKPMaster();
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }



    //protected void btn_Delete_Click(object sender, EventArgs e)
    //{
    //    ImageButton bt = (ImageButton)sender;

    //    GridViewRow gvr = (GridViewRow)bt.NamingContainer;

    //    string UniqueChildCode = (gvr.FindControl("Label2") as Label).Text;

    //    HdnGKIPID.Value = UniqueChildCode;
    //    int CampID = Convert.ToInt32(HdnGKIPID.Value);
    //    string operation = "D";
    //    string UserName = Session["username"].ToString();
    //    int Result = InsertUpdateLearningCampMaster(CampID, 0, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", UserName, operation, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

    //    if (Result > 0)
    //    {
    //        if (operation == "D")
    //        {
    //            Clear();
    //            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('GKP Session Master Data Deleted sucessfully')</script>", false);
    //            GridBindGKPMaster();
    //        }
    //    }


    //}
    //protected void btn_Delete_Click1(object sender, EventArgs e)
    //{
    //    //GKIPAID
    //    ImageButton bt = (ImageButton)sender;

    //    GridViewRow gvr = (GridViewRow)bt.NamingContainer;

    //    string UniqueChildCode = (gvr.FindControl("Label2") as Label).Text;

    //    HdnGKIPID.Value = UniqueChildCode;
    //    int CampID = Convert.ToInt32(HdnGKIPID.Value);
    //    string operation = "D";
    //    string UserName = Session["username"].ToString();
    //    int Result = InsertUpdateLearningCampMaster(CampID, 0, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", UserName, operation, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

    //    if (Result > 0)
    //    {
    //        if (operation == "D")
    //        {
    //            Clear1();
    //            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('GKP Session Master Data Deleted sucessfully')</script>", false);
    //            GridBindGKPMaster();
    //        }
    //    }

    //}

    protected void btnReprot_Click(object sender, EventArgs e)
    {

        DataSet dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GetBindGKPSessionreport");
        if (ddlMastertype.SelectedValue == "1")
        {
            ExporttoExcel(dt.Tables[0], "GKPSession");
        }
        else
        {
            ExporttoExcel(dt.Tables[1], "GKPAssessment");
        }
        // ExporttoExcel(dt, "LearningCampMaster");



    }
    public void ExportToExcelNew(DataTable dt, string FileName)
    {
        Response.ClearContent();
        Response.Buffer = true;
        string Fullfilename = "" + FileName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";

        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", Fullfilename));
        Response.ContentType = "application/ms-excel";
        Response.ContentEncoding = System.Text.Encoding.Unicode;
        Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
        DataTable dt1 = dt;
        string str = string.Empty;
        foreach (DataColumn dtcol in dt1.Columns)
        {
            Response.Write(str + dtcol.ColumnName);
            str = "\t";
        }
        Response.Write("\n");
        foreach (DataRow dr in dt1.Rows)
        {
            str = "";
            for (int j = 0; j < dt1.Columns.Count; j++)
            {
                Response.Write(str + Convert.ToString(dr[j]));
                str = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }
    private void ExporttoExcel(DataTable table, string FileName)
    {
        try
        {


            if (table != null)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                string Fullfilename = "" + FileName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";

                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + " ");

                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                //sets font
                HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
                HttpContext.Current.Response.Write("<BR><BR><BR>");
                //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
                HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
                  "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
                  "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
                //am getting my grid's column headers
                int columnscount = table.Columns.Count;


                for (int j = 0; j < columnscount; j++)
                {      //write in new column
                    HttpContext.Current.Response.Write("<Td>");
                    //Get column headers  and make it as bold in excel columns
                    HttpContext.Current.Response.Write("<B>");
                    HttpContext.Current.Response.Write(table.Columns[j]);
                    HttpContext.Current.Response.Write("</B>");
                    HttpContext.Current.Response.Write("</Td>");
                }
                HttpContext.Current.Response.Write("</TR>");
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                foreach (DataRow row in table.Rows)
                {//write in new row
                    HttpContext.Current.Response.Write("<TR>");
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        HttpContext.Current.Response.Write("<Td>");
                        HttpContext.Current.Response.Write(row[i].ToString());
                        HttpContext.Current.Response.Write("</Td>");
                    }

                    HttpContext.Current.Response.Write("</TR>");
                }
                HttpContext.Current.Response.Write("</Table>");
                HttpContext.Current.Response.Write("</font>");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }
        catch
        {

            throw;
        }
    }
}