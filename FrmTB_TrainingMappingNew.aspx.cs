using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmTB_TrainingMappingNew : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    string conditions = "", conditions1 = "";
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    public bool edit_status = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                LoadYear();
                LoadUserLeavel();


                ViewState["1"] = "ss";
                UserLevelFilter();
            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }

        }

    }
    public void UserLevelFilter()
    {
        string strQry = "";
        string Cond = "Module='Enroll'";
        strQry = "Select * from MstUserRight  where " + Cond + " and Role_Id=" + Session["user_level"].ToString() + "   ";
        DataTable dtRole = objMain.LoadData(strQry);
        if (dtRole.Rows.Count > 0)
        {
            vADD = Convert.ToBoolean(dtRole.Rows[0]["AddStatus"].ToString());
            vVerify = Convert.ToBoolean(dtRole.Rows[0]["verify_Status"].ToString());
            vDelete = Convert.ToBoolean(dtRole.Rows[0]["Delete_status"].ToString());
            edit_status = Convert.ToBoolean(dtRole.Rows[0]["edit_status"].ToString());
            ViewState["vADD"] = vADD;
            ViewState["vVerify"] = vVerify;
            ViewState["vDelete"] = vDelete;
            ViewState["edit_status"] = edit_status;
        }
    }
    public DataTable CreateDataTable()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYear.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYear;
    }

    public void LoadYear()
    {
        DateTime GivenDate = DateTime.Now;
        int GivenYear = GivenDate.Year;
        int m = GivenDate.Month;


        //ddlYear.Items.Add("--Select--","0");
        int y = GivenDate.Year;


        DateTime GivenDate1 = DateTime.Now;
        int GivenYear1 = GivenDate1.Year;
        DataTable dtYear = CreateDataTable();
        DataRow dr;
        if (ddlYear.SelectedIndex < 0)
        {

            string mYear1 = GivenYear1.ToString();
            for (int j = 0; j < 1; j++)
            {

                dr = dtYear.NewRow();
                dr["Type"] = GivenYear.ToString() + "-" + Convert.ToString((GivenYear + 1));
                dr["ID"] = y;
                dtYear.Rows.Add(dr);

                //get last  two digits (eg: 10 from 2010);


            }

        }
        dtYear = Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, "", "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


    }
    public DataTable Generate_Financial_Year()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("Type");
        DataRow dr;
        int stYr = DateTime.Today.Month < 4 ? DateTime.Today.Year + 1 : DateTime.Today.Year + 1;
        for (int i = stYr; i > 2016; i--)
        {
            dr = dt.NewRow();
            dr[0] = (i - 1).ToString();
            dr[1] = (i - 1).ToString() + "-" + (i).ToString();
            dt.Rows.Add(dr);
        }
        dt.AcceptChanges();
        return dt;
    }

    public void AlllStateCode()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {
            SqlParameter[] par1 = new SqlParameter[]
               {
                      new SqlParameter("@user_level_Role",  Convert.ToString(Session["user_level_Role"])),
                      new SqlParameter("@UserName", "" ),
                    new SqlParameter("@StateCode",  ""),
                       new SqlParameter("@Year",  ddlYear.SelectedValue),
               };
            DataTable dtAllState = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptLoadAllState", par1);
            objComman.BindDLLDatatable("mst1State", dtAllState, "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

        }
        else if (Session["user_level_Role"].ToString() == "2")
        {

            SqlParameter[] par1 = new SqlParameter[]
               {
                      new SqlParameter("@user_level_Role",  Convert.ToString(Session["user_level_Role"])),
                      new SqlParameter("@UserName", Convert.ToString(Session["username"]) ),
                    new SqlParameter("@StateCode",  ""),
                       new SqlParameter("@Year",  ddlYear.SelectedValue),
               };
            DataTable dtAllState = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptLoadAllState", par1);
            objComman.BindDLLDatatable("mst1State", dtAllState, "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

        }
        else
        {
            SqlParameter[] par1 = new SqlParameter[]
                  {
                      new SqlParameter("@user_level_Role",  Convert.ToString(Session["user_level_Role"])),
                      new SqlParameter("@UserName", Convert.ToString(Session["username"]) ),
                    new SqlParameter("@StateCode", Convert.ToString(Session["StateCode"]) ),
                       new SqlParameter("@Year",  ddlYear.SelectedValue),
                  };
            DataTable dtAllState = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptLoadAllState", par1);
            objComman.BindDLLDatatable("mst1State", dtAllState, "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");


        }

    }
    public void LoadUserLeavel()
    {
        conditions = "";
        AlllStateCode();
        if (Session["user_level_Role"].ToString() == "1")
        {
            //  objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            ddlState.SelectedIndex = 1;
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            ddlState.SelectedIndex = 1;
            ddlState.Enabled = false;
            ddlDistrict.Enabled = false;
        }


        if (Session["user_level_Role"].ToString() == "1")
        {
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "";
            conditions = "StateCode ='" + ddlState.SelectedValue + "'  and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");
            ddlDistrict.SelectedIndex = 0;
        }

        else
        {
            conditions = "";
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and Fyear= '" + ddlYear.SelectedItem.Text + "' ";
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");
            string strQry;
            strQry = "Select * from mst2District where   DistrictCode in(" + Session["DistrictCode"].ToString() + ")";
            DataTable dtcountCheck = objMain.LoadData(strQry);
            if (dtcountCheck.Rows.Count > 0)
            {
                if (dtcountCheck.Rows.Count == 1)
                {
                    ddlYear.Enabled = false;
                }
                else
                {
                    ddlYear.Enabled = true;
                }
            }
            else
            {
                ddlYear.Enabled = true;
            }
            ddlDistrict.SelectedIndex = 1;

        }





    }
    public void FillCBState()
    {
        conditions = "";
        objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");



    }
    public void FillCBDist()
    {

        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "StateCode ='" + ddlState.SelectedValue + "' and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "StateCode ='" + ddlState.SelectedValue + "'  and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else
        {
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";

        }
        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");



    }
    public void LoadData()
    {
        string strQry = "";
        conditions = "";
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + " and m.Fyear='" + Convert.ToString(ddlYear.SelectedItem.Text) + "'";
        }
        if (ddlTraining.SelectedIndex > 0)
        {
            conditions = conditions + " and m.TrainingType='" + Convert.ToString(ddlTraining.SelectedValue) + "'";

        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions = conditions + " and DistrictCode='" + Convert.ToString(ddlDistrict.SelectedValue) + "'";

        }

        if (ddlOutcomeFilter.SelectedIndex > 0)
        {
            if (ddlTraining.SelectedValue == "T")
            {
                conditions = conditions + " and l.learningID='" + Convert.ToString(ddlOutcomeFilter.SelectedValue) + "'";
            }
            else
            {
                conditions = conditions + " and l.OutcomeID='" + Convert.ToString(ddlOutcomeFilter.SelectedValue) + "'";
            }

        }
        if (ddlTraining.SelectedValue == "T")
        {
            SqlParameter[] p1 = new SqlParameter[]
            {

               new SqlParameter("@Con",  conditions),
                 new SqlParameter("@Flag",  3),
                   new SqlParameter("@StateCode",  ddlState.SelectedValue),
                     new SqlParameter("@DistrinctCode",  ddlDistrict.SelectedValue),
                      new SqlParameter("@Fyear",  ddlYear.SelectedValue),
            };
            DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_GET_Tbl_PhaseMapping2021", p1);
            if (dt1.Rows.Count > 0)
            {

                gvnroll.DataSource = dt1;
                gvnroll.DataBind();
                gvnroll.Columns[1].Visible = false;
                gvnroll.Columns[2].Visible = false;
                gvnroll.Columns[3].Visible = true;
                Session["ExcelExport"] = dt1;
            }
            else
            {
                SqlParameter[] p = new SqlParameter[]
            {

               new SqlParameter("@Con",  conditions),
                 new SqlParameter("@Flag",  2),
                   new SqlParameter("@StateCode",  ddlState.SelectedValue),
                     new SqlParameter("@DistrinctCode",  ddlDistrict.SelectedValue),
                      new SqlParameter("@Fyear",  ddlYear.SelectedValue),
            };
                dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_GET_Tbl_PhaseMapping2021", p);
                gvnroll.DataSource = dt1;
                gvnroll.DataBind();
                gvnroll.Columns[1].Visible = false;
                gvnroll.Columns[2].Visible = false;
                gvnroll.Columns[3].Visible = true;
                Session["ExcelExport"] = dt1;
            }
        }
        else
        {
            SqlParameter[] p1 = new SqlParameter[]
            {

               new SqlParameter("@Con",  conditions),
                 new SqlParameter("@Flag",  5),
                   new SqlParameter("@StateCode",  ddlState.SelectedValue),
                     new SqlParameter("@DistrinctCode",  ddlDistrict.SelectedValue),
                      new SqlParameter("@Fyear",  ddlYear.SelectedValue),
            };
            DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_GET_Tbl_PhaseMapping2021", p1);
            if (dt1.Rows.Count > 0)
            {
                gvnroll.DataSource = dt1;
                gvnroll.DataBind();
                gvnroll.Columns[1].Visible = true;
                gvnroll.Columns[2].Visible = true;
                gvnroll.Columns[3].Visible = false;
                Session["ExcelExport"] = dt1;
            }
            else
            {
                SqlParameter[] p = new SqlParameter[]
            {

               new SqlParameter("@Con",  conditions),
                 new SqlParameter("@Flag",  4),
                   new SqlParameter("@StateCode",  ddlState.SelectedValue),
                     new SqlParameter("@DistrinctCode",  ddlDistrict.SelectedValue),
                      new SqlParameter("@Fyear",  ddlYear.SelectedValue),
            };
                dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_GET_Tbl_PhaseMapping2021", p);
                gvnroll.DataSource = dt1;
                gvnroll.DataBind();
                gvnroll.Columns[1].Visible = true;
                gvnroll.Columns[2].Visible = true;
                gvnroll.Columns[3].Visible = false;
                Session["ExcelExport"] = dt1;
            }
        }

    }

    #region ****** On Selected Index Changed Event
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        AlllStateCode();
        if (ddlYear.SelectedIndex > 0)
        {
            ddlState.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlDistrict, null);
            if (Session["user_level_Role"].ToString() == "3" || Session["user_level_Role"].ToString() == "4")
            {
                ddlDistrict.SelectedIndex = 1;
            }
        }
        else
        {
            ddlState.SelectedIndex = 0;
            ddlDistrict.Items.Clear();

        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
    }
    protected void ddlDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvnroll.DataSource = null;
        gvnroll.DataBind();
    }
    protected void ddlTraining_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTraining.SelectedValue == "T")
        {
            conditions = " ActiveStatus=1 and ISNULL(TrainingStatus,0)=1 ";
            objComman.BindDLL("mstlearning", "learningID,dbo.TitleCase(upper(learningName)) as learningName ", conditions, "learningName", "asc", ddlOutcomeFilter, "learningName", "learningID", "--ALL--");
        }
        else
        {
            conditions = " ActiveStatus=1 ";
            objComman.BindDLL("mstOutcome", "OutcomeID,OutcomeName", conditions, "OutcomeName", "asc", ddlOutcomeFilter, "OutcomeName", "OutcomeID", "--ALL--");
        }
    }
    #endregion
    protected void gvnroll_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow.CssClass = "gridnewheadercss";
            TableCell HeaderCell = new TableCell();
            if (ddlTraining.SelectedValue == "S")
            {

                HeaderCell.Text = "Training Outcome";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.RowSpan = 2;
                HeaderCell.ColumnSpan = 2;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);

            }
            else
            {
                HeaderCell = new TableCell();
                HeaderCell.Text = "Training Outcome";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.RowSpan = 2;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);
            }
            HeaderCell = new TableCell();
            HeaderCell.Text = "No. of Training Days ";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 7;
            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 7;
            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);
            GridView HeaderGrid1 = (GridView)sender;
            GridViewRow HeaderGridRow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow1.CssClass = "gridnewheadercss";
            TableCell HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Phase 1";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow1.Cells.Add(HeaderCell1);
            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Phase 2";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 2;
            HeaderCell1.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow1.Cells.Add(HeaderCell1);
            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Phase 3";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;


            HeaderCell1.ColumnSpan = 2;
            HeaderCell1.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow1.Cells.Add(HeaderCell1);
            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Phase 4";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 2;
            HeaderCell1.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow1.Cells.Add(HeaderCell1);
            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 2;
            HeaderCell1.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow1.Cells.Add(HeaderCell1);
            gvnroll.Controls[0].Controls.AddAt(0, HeaderGridRow);
            gvnroll.Controls[0].Controls.AddAt(1, HeaderGridRow1);
        }
    }
    protected void gvnroll_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnActiveDeactive = (HiddenField)e.Row.FindControl("hdnActiveDeactive");

            LinkButton btnActiveDeactive = (LinkButton)e.Row.FindControl("btnActiveDeactive");
            if (hdnActiveDeactive.Value == "True")
            {
                btnActiveDeactive.Text = "Active";
            }
            else
            {
                btnActiveDeactive.Text = "de-Active";
            }
        }
    }
    #region ****** Button Click Event *****
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select District')</script>", false);
            gvnroll.DataSource = null;
            gvnroll.DataBind();
            return;
        }
        if (ddlTraining.SelectedIndex > 0)
        {
            LoadData();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Training Type.')</script>", false);
            gvnroll.DataSource = null;
            gvnroll.DataBind();
            return;
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
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
            string Flag = "";
            string UniqueID = "";
            int Ret = 0;
            for (int i = 0; i < gvnroll.Rows.Count; i++)
            {

                Label lblTB_GUID = (Label)gvnroll.Rows[i].FindControl("lblTB_GUID");
                if (lblTB_GUID.Text != "" && lblTB_GUID.Text != null)
                {

                    Flag = "U";
                    UniqueID = Convert.ToString(lblTB_GUID.Text);
                }
                else
                {
                    Flag = "I";

                    UniqueID = objComman.Generate_RandomStringAnu(8);
                }
                Label lblStateCode = (Label)gvnroll.Rows[i].FindControl("lblStateCode");
                Label lblDistrictCode = (Label)gvnroll.Rows[i].FindControl("lblDistrictCode");
                Label LearningID = (Label)gvnroll.Rows[i].FindControl("lblLearningID");
                Label OutcomeID = (Label)gvnroll.Rows[i].FindControl("lblOutComeID");
                TextBox TxtN_P1_Y1 = (TextBox)gvnroll.Rows[i].FindControl("TxtN_P1_Y1");
                //TextBox TxtN_P1_Y2 = (TextBox)gvnroll.Rows[i].FindControl("TxtN_P1_Y2");
                //TextBox TxtN_P1_Y3 = (TextBox)gvnroll.Rows[i].FindControl("TxtN_P1_Y3");
                //TextBox TxtN_P2_Y1 = (TextBox)gvnroll.Rows[i].FindControl("TxtN_P2_Y1");
                //TextBox TxtN_P2_Y2 = (TextBox)gvnroll.Rows[i].FindControl("TxtN_P2_Y2");
                //TextBox TxtN_P2_Y3 = (TextBox)gvnroll.Rows[i].FindControl("TxtN_P2_Y3");
                //TextBox TxtN_P3_Y1 = (TextBox)gvnroll.Rows[i].FindControl("TxtN_P3_Y1");
                //TextBox TxtN_P3_Y2 = (TextBox)gvnroll.Rows[i].FindControl("TxtN_P3_Y2");
                //TextBox TxtN_P3_Y3 = (TextBox)gvnroll.Rows[i].FindControl("TxtN_P3_Y3");

                //TextBox TxtN_P4_Y2 = (TextBox)gvnroll.Rows[i].FindControl("TxtN_P4_Y2");
                //TextBox TxtN_P4_Y3 = (TextBox)gvnroll.Rows[i].FindControl("TxtN_P4_Y3");

                Ret = Insert_Update_TB_Staff_Taining(UniqueID, ddlState.SelectedValue, ddlDistrict.SelectedValue, LearningID, OutcomeID, TxtN_P1_Y1, Flag);

                Ret++;
            }
            if (Ret > 0)
            {
                LoadData();
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
            }
        }
        catch
        {

            throw;
        }

    }
    protected void btnSaveNew_Click(object sender, EventArgs e)
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
        int ret = 0;
        if (ddlTraining.SelectedIndex > 0)
        {
            if (ddlTraining.SelectedValue == "T")
            {
                if (txtTrainingOutCome.Text != "")
                {
                    ret = Insert_Update_TB_Taining(objComman.Generate_RandomStringAnu(8), "0", txtTrainingOutCome, ddlTraining.SelectedValue);
                }

            }
            else
            {
                if (txtTrainingOutCome.Text != "" && ddlOutCome.SelectedIndex > 0)
                {
                    ret = Insert_Update_TB_Taining(objComman.Generate_RandomStringAnu(8), Convert.ToString(ddlOutCome.SelectedValue), txtTrainingOutCome, ddlTraining.SelectedValue);
                }

            }
            if (ret > 0)
            {
                LoadData();
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);

            }

        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlTraining.SelectedValue == "T")
            {
                divoutcome.Visible = false;
            }
            else
            {
                divoutcome.Visible = true;
                conditions = "";
                objComman.BindDLL("mstOutcome", "OutcomeID,OutcomeName", conditions, "OutcomeName", "asc", ddlOutCome, "OutcomeName", "OutcomeID", "--Select--");
            }
            MpexdrDistrict.Show();
        }
        catch
        {

            throw;
        }
    }
    protected void btnActiveDeactive_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        int i = Convert.ToInt32(row.RowIndex);
        Label lblLearningID = (Label)gvnroll.Rows[i].FindControl("lblLearningID");
        HiddenField hdnActiveDeactive = (HiddenField)gvnroll.Rows[i].FindControl("hdnActiveDeactive");
        int ret = 0;
        if (ddlTraining.SelectedIndex > 0)
        {

            ret = Insert_Update_TB_Active_Deactive(Convert.ToString(lblLearningID.Text), Convert.ToBoolean(hdnActiveDeactive.Value), ddlTraining.SelectedValue);



            if (ret > 0)
            {
                LoadData();
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);

            }

        }
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        DataTable dt = null;
        Int32 Flag = 0;
        if (ddlTraining.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select  Training')</script>", false);

            return;
        }
        if (ddlTraining.SelectedValue == "T")
        {
            Flag = 7;
        }
        else
        {
            Flag = 8;
        }
        SqlParameter[] p = new SqlParameter[]
            {

               new SqlParameter("@Con",  conditions),
                 new SqlParameter("@Flag", Flag),
                   new SqlParameter("@StateCode",  ddlState.SelectedValue),
                     new SqlParameter("@DistrinctCode",  ddlDistrict.SelectedValue),
                      new SqlParameter("@Fyear",  ddlYear.SelectedItem.Text),
            };
        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_GET_Tbl_PhaseMapping2021", p);

        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2024)
        {
            ExportExcel2024((ddlTraining.SelectedItem.Text).Replace(" ", "") + "_", dt);
        }
        else
        {
            ExportExcel((ddlTraining.SelectedItem.Text).Replace(" ", "") + "_", dt);
        }

    }

    private void ExportExcel2024(string FIleName, DataTable dt)
    {
        try
        {

            if (dt.Rows.Count > 0)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                string Fullfilename = "" + FIleName + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");
                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                HttpContext.Current.Response.Write("<table  width='80%'>");
                if (ddlTraining.SelectedValue == "T")
                {
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='17' ' style='text-align:Center;border:.2pt solid windowtext;'>TB Training </td>");
                    HttpContext.Current.Response.Write("</tr>");
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td  rowspan='2' style='text-align:Center;border:.2pt solid windowtext;'> Training Outcome  </td>");
                    HttpContext.Current.Response.Write("<td colspan='16' style='text-align:Center;border:.2pt solid windowtext;'> No. of Trainings  </td>");
                    HttpContext.Current.Response.Write("</tr>");
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='4' style='text-align:Center;border:.2pt solid windowtext;'> MP  </td>");
                    HttpContext.Current.Response.Write("<td colspan='4' style='text-align:Center;border:.2pt solid windowtext;'> Rajasthan  </td>");
                    HttpContext.Current.Response.Write("<td colspan='2' style='text-align:Center;border:.2pt solid windowtext;'>UP ZONE-1  </td>");
                    HttpContext.Current.Response.Write("<td colspan='2' style='text-align:Center;border:.2pt solid windowtext;'>UP ZONE-2  </td>");
                    HttpContext.Current.Response.Write("<td colspan='3' style='text-align:Center;border:.2pt solid windowtext;'>UP ZONE-3  </td>");
                    HttpContext.Current.Response.Write("<td colspan='9' style='text-align:Center;border:.2pt solid windowtext;'>Bihar  </td>");
                    HttpContext.Current.Response.Write("<td colspan='9' style='text-align:Center;border:.2pt solid windowtext;'>UP MAITRI  </td>");

                    int columnscount = dt.Columns.Count;

                    HttpContext.Current.Response.Write("<tr>");
                    for (int j = 0; j < columnscount; j++)
                    {      //write in new column

                        HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'>" + dt.Columns[j].ColumnName + "</td>");
                    }
                    HttpContext.Current.Response.Write("</tr>");
                    //HttpContext.Current.Response.Write("</tr>");
                    //HttpContext.Current.Response.Write("<tr>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Trianing outcome  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y1  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y2  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y3  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y4  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y5  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y6  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y7  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y8  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y9  </td>");
                    //HttpContext.Current.Response.Write("</tr>");
                }
                else
                {
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='30' ' style='text-align:Center;border:.2pt solid windowtext;'>Staff Training </td>");
                    HttpContext.Current.Response.Write("</tr>");
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='2' rowspan='2' style='text-align:Center;border:.2pt solid windowtext;'> Training Outcome  </td>");
                    HttpContext.Current.Response.Write("<td colspan='16' style='text-align:Center;border:.2pt solid windowtext;'> No. of Training  </td>");
                    HttpContext.Current.Response.Write("</tr>");
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='4' style='text-align:Center;border:.2pt solid windowtext;'> MP  </td>");
                    HttpContext.Current.Response.Write("<td colspan='4' style='text-align:Center;border:.2pt solid windowtext;'> Rajasthan  </td>");
                    HttpContext.Current.Response.Write("<td colspan='2' style='text-align:Center;border:.2pt solid windowtext;'>UP ZONE-1  </td>");
                    HttpContext.Current.Response.Write("<td colspan='2' style='text-align:Center;border:.2pt solid windowtext;'>UP ZONE-2  </td>");
                    HttpContext.Current.Response.Write("<td colspan='3' style='text-align:Center;border:.2pt solid windowtext;'>UP ZONE-3  </td>");
                    HttpContext.Current.Response.Write("<td colspan='9' style='text-align:Center;border:.2pt solid windowtext;'>Bihar  </td>");
                    HttpContext.Current.Response.Write("<td colspan='9' style='text-align:Center;border:.2pt solid windowtext;'>UP MAITRI  </td>");


                    HttpContext.Current.Response.Write("</tr>");
                    HttpContext.Current.Response.Write("<tr>");
                    int columnscount = dt.Columns.Count;


                    for (int j = 0; j < columnscount; j++)
                    {      //write in new column

                        HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'>" + dt.Columns[j].ColumnName + "</td>");
                    }
                    HttpContext.Current.Response.Write("</tr>");

                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Trianing outcome  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Specific Trianing Name  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y1  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y2  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y3  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y4  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y5  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y6  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y7  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y8  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y9  </td>");
                    //HttpContext.Current.Response.Write("</tr>");
                }
                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";




                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    HttpContext.Current.Response.Write("<tr>");
                    //HttpContext.Current.Response.Write("<td >Direct</td>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {

                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");

                    }
                }
                HttpContext.Current.Response.Write("</tr>");

                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    HttpContext.Current.Response.Write("<tr>");
                //    if (ddlTraining.SelectedValue == "S")
                //    {

                //        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["OutcomeName"] + "</td>");
                //    }

                //    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TrainingOutcome"] + "</td>");
                //    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["N_P1_Y1"] + "</td>");

                //    HttpContext.Current.Response.Write("</tr>");
                //}



                HttpContext.Current.Response.Write("</table>");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }
        catch
        {

            throw;
        }


    }
    #endregion
    #region   Insert Update
    public int Insert_Update_TB_Taining(string TB_GUID, string OutComeID, TextBox txtTrainingOutCome, string Flag)
    {
        SqlConnection dbSqlconnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            dbSqlconnection.Open();
            using (SqlCommand dbSqlCommand = (SqlCommand)dbSqlconnection.CreateCommand())
            {
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = "Insert_Update_OutCome_TrainingOutCome";
                dbSqlCommand.Parameters.AddWithValue("@TB_GUID", TB_GUID);
                dbSqlCommand.Parameters.AddWithValue("@TrainingOutcome", txtTrainingOutCome.Text);
                dbSqlCommand.Parameters.AddWithValue("@OutComeID", OutComeID);
                dbSqlCommand.Parameters.AddWithValue("@Fyear", Convert.ToString(ddlYear.SelectedItem.Text));
                dbSqlCommand.Parameters.AddWithValue("@CreatedBy", Convert.ToString(Session["username"]));
                dbSqlCommand.Parameters.AddWithValue("@Type", Flag);
                SqlParameter ReturnAffectedRows = new SqlParameter("@RowAffected", System.Data.SqlDbType.Int);
                ReturnAffectedRows.Direction = ParameterDirection.Output;
                dbSqlCommand.Parameters.Add(ReturnAffectedRows);
                dbSqlCommand.ExecuteNonQuery();
                int _returnRow = Convert.ToInt32(ReturnAffectedRows.Value);
                return _returnRow;
            }
        }
        catch (SqlException exp)
        {
            throw exp;
        }
        finally
        {
            dbSqlconnection.Dispose();
        }
    }
    public int Insert_Update_TB_Active_Deactive(string OutComeID, bool Status, string Flag)
    {
        SqlConnection dbSqlconnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            dbSqlconnection.Open();
            using (SqlCommand dbSqlCommand = (SqlCommand)dbSqlconnection.CreateCommand())
            {
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = "Insert_Update_Active_Deactive";
                dbSqlCommand.Parameters.AddWithValue("@OutComeID", OutComeID);
                dbSqlCommand.Parameters.AddWithValue("@Status", Status == true ? 0 : 1);
                dbSqlCommand.Parameters.AddWithValue("@Type", Flag);
                SqlParameter ReturnAffectedRows = new SqlParameter("@RowAffected", System.Data.SqlDbType.Int);
                ReturnAffectedRows.Direction = ParameterDirection.Output;
                dbSqlCommand.Parameters.Add(ReturnAffectedRows);
                dbSqlCommand.ExecuteNonQuery();
                int _returnRow = Convert.ToInt32(ReturnAffectedRows.Value);
                return _returnRow;
            }
        }
        catch (SqlException exp)
        {
            throw exp;
        }
        finally
        {
            dbSqlconnection.Dispose();
        }
    }
    public int Insert_Update_TB_Staff_Taining(string TB_GUID, string lblStateCode, string lblDistrictCode, Label lblLearningID, Label lblOutComeID, TextBox TxtN_P1_Y1, string Flag)
    {
        SqlConnection dbSqlconnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            dbSqlconnection.Open();
            using (SqlCommand dbSqlCommand = (SqlCommand)dbSqlconnection.CreateCommand())
            {
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = "Insert_Update_TBTraining_StaffTraining";
                dbSqlCommand.Parameters.AddWithValue("@TB_GUID", TB_GUID);
                dbSqlCommand.Parameters.AddWithValue("@StateCode", Convert.ToString(lblStateCode));
                dbSqlCommand.Parameters.AddWithValue("@DistrictCode", Convert.ToString(lblDistrictCode));
                dbSqlCommand.Parameters.AddWithValue("@LearningID", Convert.ToInt32(lblLearningID.Text));
                dbSqlCommand.Parameters.AddWithValue("@OutComeID", lblOutComeID.Text == "" ? 0 : Convert.ToInt32(lblOutComeID.Text));
                dbSqlCommand.Parameters.AddWithValue("@Fyear", Convert.ToString(ddlYear.SelectedItem.Text));
                dbSqlCommand.Parameters.AddWithValue("@N_P1_Y1", TxtN_P1_Y1.Text == "" ? 0 : Convert.ToInt32(TxtN_P1_Y1.Text));
                dbSqlCommand.Parameters.AddWithValue("@N_P1_Y2", "0");
                dbSqlCommand.Parameters.AddWithValue("@N_P1_Y3", "0");
                dbSqlCommand.Parameters.AddWithValue("@N_P2_Y1", "0");
                dbSqlCommand.Parameters.AddWithValue("@N_P2_Y2", "0");
                dbSqlCommand.Parameters.AddWithValue("@N_P2_Y3", "0");
                dbSqlCommand.Parameters.AddWithValue("@N_P3_Y1", "0");
                dbSqlCommand.Parameters.AddWithValue("@N_P3_Y2", "0");
                dbSqlCommand.Parameters.AddWithValue("@N_P3_Y3", "0");


                dbSqlCommand.Parameters.AddWithValue("@TrainingType", ddlTraining.SelectedValue);
                dbSqlCommand.Parameters.AddWithValue("@CreatedBy", Convert.ToString(Session["username"]));
                dbSqlCommand.Parameters.AddWithValue("@Flag", Flag);
                dbSqlCommand.Parameters.AddWithValue("@N_P4_Y2", "0");
                dbSqlCommand.Parameters.AddWithValue("@N_P4_Y3", "0");


                SqlParameter ReturnAffectedRows = new SqlParameter("@RowAffected", System.Data.SqlDbType.Int);
                ReturnAffectedRows.Direction = ParameterDirection.Output;
                dbSqlCommand.Parameters.Add(ReturnAffectedRows);
                dbSqlCommand.ExecuteNonQuery();
                int _returnRow = Convert.ToInt32(ReturnAffectedRows.Value);
                return _returnRow;
            }
        }
        catch (SqlException exp)
        {
            throw exp;
        }
        finally
        {
            dbSqlconnection.Dispose();
        }
    }
    #endregion

    #region *************** Export Excel
    private void ExportExcel(string FIleName, DataTable dt)
    {
        try
        {

            if (dt.Rows.Count > 0)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                string Fullfilename = "" + FIleName + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");
                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                HttpContext.Current.Response.Write("<table  width='80%'>");
                if (ddlTraining.SelectedValue == "T")
                {
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='17' ' style='text-align:Center;border:.2pt solid windowtext;'>TB Training </td>");
                    HttpContext.Current.Response.Write("</tr>");
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td  rowspan='2' style='text-align:Center;border:.2pt solid windowtext;'> Training Outcome  </td>");
                    HttpContext.Current.Response.Write("<td colspan='16' style='text-align:Center;border:.2pt solid windowtext;'> No. of Trainings  </td>");
                    HttpContext.Current.Response.Write("</tr>");
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='7' style='text-align:Center;border:.2pt solid windowtext;'> MP  </td>");
                    HttpContext.Current.Response.Write("<td colspan='6' style='text-align:Center;border:.2pt solid windowtext;'> Rajasthan  </td>");
                    HttpContext.Current.Response.Write("<td colspan='8' style='text-align:Center;border:.2pt solid windowtext;'>UP  </td>");
                    int columnscount = dt.Columns.Count;

                    HttpContext.Current.Response.Write("<tr>");
                    for (int j = 0; j < columnscount; j++)
                    {      //write in new column

                        HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'>" + dt.Columns[j].ColumnName + "</td>");
                    }
                    HttpContext.Current.Response.Write("</tr>");
                    //HttpContext.Current.Response.Write("</tr>");
                    //HttpContext.Current.Response.Write("<tr>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Trianing outcome  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y1  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y2  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y3  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y4  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y5  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y6  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y7  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y8  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y9  </td>");
                    //HttpContext.Current.Response.Write("</tr>");
                }
                else
                {
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='18' ' style='text-align:Center;border:.2pt solid windowtext;'>Staff Training </td>");
                    HttpContext.Current.Response.Write("</tr>");
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='2' rowspan='2' style='text-align:Center;border:.2pt solid windowtext;'> Training Outcome  </td>");
                    HttpContext.Current.Response.Write("<td colspan='16' style='text-align:Center;border:.2pt solid windowtext;'> No. of Training  </td>");
                    HttpContext.Current.Response.Write("</tr>");
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='7' style='text-align:Center;border:.2pt solid windowtext;'> MP  </td>");
                    HttpContext.Current.Response.Write("<td colspan='5' style='text-align:Center;border:.2pt solid windowtext;'> Rajasthan  </td>");
                    HttpContext.Current.Response.Write("<td colspan='7' style='text-align:Center;border:.2pt solid windowtext;'>UP  </td>");
                    HttpContext.Current.Response.Write("<td colspan='17' style='text-align:Center;border:.2pt solid windowtext;'>Bihar  </td>");
                    HttpContext.Current.Response.Write("</tr>");
                    HttpContext.Current.Response.Write("<tr>");
                    int columnscount = dt.Columns.Count;


                    for (int j = 0; j < columnscount; j++)
                    {      //write in new column

                        HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'>" + dt.Columns[j].ColumnName + "</td>");
                    }
                    HttpContext.Current.Response.Write("</tr>");

                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Trianing outcome  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Specific Trianing Name  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y1  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y2  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y3  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y4  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y5  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y6  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y7  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y8  </td>");
                    //HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'> Y9  </td>");
                    //HttpContext.Current.Response.Write("</tr>");
                }
                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";




                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    HttpContext.Current.Response.Write("<tr>");
                    //HttpContext.Current.Response.Write("<td >Direct</td>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {

                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");

                    }
                }
                HttpContext.Current.Response.Write("</tr>");

                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    HttpContext.Current.Response.Write("<tr>");
                //    if (ddlTraining.SelectedValue == "S")
                //    {

                //        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["OutcomeName"] + "</td>");
                //    }

                //    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TrainingOutcome"] + "</td>");
                //    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["N_P1_Y1"] + "</td>");

                //    HttpContext.Current.Response.Write("</tr>");
                //}



                HttpContext.Current.Response.Write("</table>");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }
        catch
        {

            throw;
        }


    }

    #endregion
}