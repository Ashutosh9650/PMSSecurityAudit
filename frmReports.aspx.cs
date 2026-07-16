using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class frmReports : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    string conditions = "";
    string flag = "";
    Password objPass = new Password();
    public DataTable dtUserDeatils;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblTotalCount.Text = "";
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                LoadYear();
                LoadUserLeavel();
                ViewState["1"] = "ss";
                FillUser();
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
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
        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


    }
    public void FillUser()
    {
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions = "StaffID  <>'' and DistrictCode ='" + ddlDistrict.SelectedValue + "'";
            objComman.BindDLL("MstUser", "UserName,[UserName]+' ('+ FristName +')' as UserName1 ", conditions, "UserName1", "asc", ddlUser, "UserName1", "UserName", "--Select--");
        }
        else
        {
            conditions = "StaffID  <>'' and  DistrictCode ='" + ddlDistrict.SelectedValue + "' ";
            objComman.BindDLL("MstUser", "UserName,[UserName]+' ('+ FristName +')' as UserName1 ", conditions, "UserName1", "asc", ddlUser, "UserName1", "UserName", "--Select--");
        }



    }
    public void ClearGrid()
    {
        GvReport.DataSource = null;
        GvReport.DataBind();

        gvUserReport.DataSource = null;
        gvUserReport.DataBind();
        gvD2d.DataSource = null;
        gvD2d.DataBind();
        gvnroll.DataSource = null;
        gvnroll.DataBind();
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        GV_DynamicGrid1.DataSource = null;
        GV_DynamicGrid1.DataBind();
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();

    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 1;
        ClearGrid();
        GvReport.Visible = true;
        gvD2d.Visible = false;
        gvnroll.Visible = false;
        GV_DynamicGrid.Visible = false;
        GV_DynamicGrid1.Visible = false;
        GV_DynamicGrid2.Visible = false;
        GvReport.DataSource = null;
        GvReport.DataBind();
        gvD2d.DataSource = null;
        gvUserReport.Visible = false;
        gvD2d.DataBind();
        gvUserReport.DataSource = null;
        gvUserReport.DataBind();
        LoadReport();
    }
    protected void btnEnroll_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 4;
        ClearGrid();
        GvReport.Visible = true;
        GV_DynamicGrid2.Visible = false;
        gvD2d.Visible = false;
        gvnroll.Visible = false;
        GvReport.DataSource = null;
        GvReport.DataBind();
        gvD2d.DataSource = null;
        gvUserReport.Visible = false;
        gvvillageschoolgrid.Visible = false;
        gvD2d.DataBind();
        gvUserReport.DataSource = null;
        gvUserReport.DataBind();
        LoadReportEnrollment();
    }
    protected void btnUser_Click(object sender, EventArgs e)
    {
        gvvillageschoolgrid.Visible = false;
        ViewState["1"] = 3;
        ClearGrid();
        GV_DynamicGrid.Visible = false;
        GV_DynamicGrid1.Visible = false;
        GV_DynamicGrid2.Visible = false;
        GvReport.Visible = false;
        gvD2d.Visible = false;
        gvnroll.Visible = false;
        gvUserReport.Visible = true;
        GvReport.DataSource = null;
        GvReport.DataBind();
        gvD2d.DataSource = null;
        gvD2d.DataBind();
        gvUserReport.DataSource = null;
        gvUserReport.DataBind();
        LoadReport();
    }
    protected void btnUserDeatils_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 5;
        ClearGrid();
        gvvillageschoolgrid.Visible = false;
        GV_DynamicGrid.Visible = false;
        GV_DynamicGrid1.Visible = false;
        GV_DynamicGrid2.Visible = false;
        GvReport.Visible = false;
        gvD2d.Visible = false;
        gvnroll.Visible = false;
        gvUserReport.Visible = true;
        GvReport.DataSource = null;
        GvReport.DataBind();
        gvD2d.DataSource = null;
        gvD2d.DataBind();
        gvUserReport.DataSource = null;
        gvUserReport.DataBind();
        LoadReportEnrollment();
    }
    protected void btnD2d_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 2;
        ClearGrid();
        gvD2d.Visible = true;
        gvvillageschoolgrid.Visible = false;
        GV_DynamicGrid.Visible = false;
        GV_DynamicGrid1.Visible = false;
        GV_DynamicGrid2.Visible = false;
        GvReport.Visible = false;
        gvUserReport.Visible = false;
        gvnroll.Visible = false;
        GvReport.DataSource = null;
        GvReport.DataBind();
        gvD2d.DataSource = null;
        gvD2d.DataBind();
        //LoadReport();
        gvUserReport.DataSource = null;
        gvUserReport.DataBind();
        LoadReport();
    }


    protected void btnOuterD2d_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 10;
        ClearGrid();
        gvD2d.Visible = true;
        gvvillageschoolgrid.Visible = false;
        GV_DynamicGrid.Visible = false;
        GV_DynamicGrid1.Visible = false;
        GV_DynamicGrid2.Visible = false;
        GvReport.Visible = false;
        gvUserReport.Visible = false;
        gvnroll.Visible = false;
        GvReport.DataSource = null;
        GvReport.DataBind();
        gvD2d.DataSource = null;
        gvD2d.DataBind();
        //LoadReport();
        gvUserReport.DataSource = null;
        gvUserReport.DataBind();

        LoadReport();
    }



    protected void btnEnrolllment_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 6;
        ClearGrid(); ;
        gvD2d.Visible = false;
        GvReport.Visible = false;
        GV_DynamicGrid.Visible = false;
        GV_DynamicGrid1.Visible = false;
        GV_DynamicGrid2.Visible = false;
        gvvillageschoolgrid.Visible = false;
        gvUserReport.Visible = false;
        gvnroll.Visible = true;
        GvReport.DataSource = null;
        GvReport.DataBind();
        gvD2d.DataSource = null;
        gvD2d.DataBind();
        //LoadReport();
        gvUserReport.DataSource = null;
        gvUserReport.DataBind();
        LoadReportEnrollment();
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (ViewState["1"].ToString() == "2")
        {
            DataTable dt = (DataTable)ViewState["D2dAllData"];
            ExporttoExcel(gvD2d, dt, "D2D");
        }

        if (ViewState["1"].ToString() == "3")
        {
            DataTable dt = (DataTable)ViewState["D2dUserDet"];
            ExporttoExcel(gvUserReport, dt, "");
        }
        if (ViewState["1"].ToString() == "1")
        {
            DataTable dt = (DataTable)ViewState["D2dUser"];
            ExporttoExcel(GvReport, dt, "");
        }
        if (ViewState["1"].ToString() == "7")
        {
            DataTable dt = (DataTable)ViewState["D2dUser"];
            ExporttoExcel(GV_DynamicGrid, dt, "MasterData");
        }
        if (ViewState["1"].ToString() == "6")
        {
            DataTable dt = (DataTable)ViewState["Enroll"];
            ExporttoExcel(gvnroll, dt, "Enrolllment");
        }
        if (ViewState["1"].ToString() == "4")
        {
            DataTable dt = (DataTable)ViewState["EnrollSummary"];
            ExporttoExcel(GvReport, dt, "");
        }
        if (ViewState["1"].ToString() == "5")
        {

            DataTable dt = (DataTable)ViewState["ENrollDetail"];
            ExporttoExcel(gvUserReport, dt, "");
        }
        if (ViewState["1"].ToString() == "8")
        {
            DataTable dt = (DataTable)ViewState["D2dUser"];
            ExporttoExcel(GV_DynamicGrid1, dt, "TeamBalika");
        }
        if (ViewState["1"].ToString() == "9")
        {
            DataTable dt = (DataTable)ViewState["D2dUser"];
            ExporttoExcel(GV_DynamicGrid2, dt, "UserMaster");
        }

        if (ViewState["1"].ToString() == "10")
        {
            DataTable dt = (DataTable)ViewState["OutD2d"];
            ExporttoExcel(gvD2d, dt, "OutOfDoorToDoor");
        }

        if (ViewState["1"].ToString() == "11")
        {
            DataTable dt = (DataTable)ViewState["villageschool"];
            ExporttoExcel(gvvillageschoolgrid, dt, "VillageProfile");
        }

        if (ViewState["1"].ToString() == "15")
        {
            DataTable dt = (DataTable)ViewState["LearningBaseline"];
            ExporttoExcel(GV_DynamicGrid, dt, "LearningBaseline");
        }
        if (ViewState["1"].ToString() == "16")
        {
            DataTable dt = (DataTable)ViewState["D2dUser"];
            ExporttoExcel(GV_DynamicGrid, dt, "InEligible");
        }
        if (ViewState["1"].ToString() == "48")
        {
            DataTable dt = (DataTable)ViewState["D2dUser"];
            ExporttoExcel(GV_DynamicGrid, dt, "TBTraining");
        }

        if (ViewState["1"].ToString() == "18")
        {
            DataTable dt = (DataTable)ViewState["villageschool"];
            ExporttoExcel(gvvillageschoolgrid, dt, "SIC");
        }
        if (ViewState["1"].ToString() == "28")
        {
            DataTable dt = (DataTable)ViewState["SIPDetails"];
            ExporttoExcel(gvvillageschoolgrid, dt, "SIP");
        }

    }
    protected void btnMainReport_Click(object sender, EventArgs e)
    {



    }
    protected void LnkMasterData_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 7;
        ClearGrid();
        gvnroll.Visible = false;
        gvD2d.Visible = false;
        GV_DynamicGrid1.Visible = false;
        GV_DynamicGrid2.Visible = false;
        GvReport.Visible = false;
        gvUserReport.Visible = false;
        LoadMasterData(0);
        GV_DynamicGrid.Visible = true;

    }
    protected void LnkMobileDataReport_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 116;
        ClearGrid();
        gvnroll.Visible = false;
        gvD2d.Visible = false;
        GV_DynamicGrid1.Visible = false;
        GV_DynamicGrid2.Visible = false;
        GvReport.Visible = false;
        gvUserReport.Visible = false;
        ReportMobileActivityStatus(0);
        GV_DynamicGrid.Visible = true;

    }
    protected void btnSipdetail_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 28;
        ClearGrid();
        gvnroll.Visible = false;
        gvD2d.Visible = false;
        GV_DynamicGrid1.Visible = false;
        GV_DynamicGrid2.Visible = false;
        GvReport.Visible = false;
        gvUserReport.Visible = false;
        LoadSIP(28);
        GV_DynamicGrid.Visible = true;

    }
    protected void btnInEligible_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 16;
        ClearGrid();
        gvnroll.Visible = false;
        gvD2d.Visible = false;
        GV_DynamicGrid1.Visible = false;
        GV_DynamicGrid2.Visible = false;
        GvReport.Visible = false;
        gvUserReport.Visible = false;
        LoadIneligable(0);
        GV_DynamicGrid.Visible = true;
    }

    protected void btnLearningBaseline_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 15;
        ClearGrid();
        gvnroll.Visible = false;
        gvD2d.Visible = false;
        GV_DynamicGrid1.Visible = false;
        GV_DynamicGrid2.Visible = false;
        GvReport.Visible = false;
        gvUserReport.Visible = false;
        getreport2();
        GV_DynamicGrid.Visible = true;

    }
    public void getreport2()
    {
        conditions = "";
        string subject = "";
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "  where v.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlState.SelectedIndex > 0)
        {
            conditions += "and  v.StateCode = '" + ddlState.SelectedValue + "' ";

        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " and v.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";

        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions += " and v.BlockCode = '" + ddlBlock.SelectedValue + "' ";

        }

        //if (ddlsubject.SelectedIndex > 0)
        //{
        //    subject = ddlsubject.SelectedItem.Text;
        //}
        SqlParameter[] parm = new SqlParameter[]
            {
       new SqlParameter("@condition",  conditions),
       new SqlParameter("@subject",  subject),
        new SqlParameter("@flag",2),

                 };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Get_CLT_Report_Data]", parm);
        ViewState["dt"] = dt;
        lblTotalCount.Text = dt.Rows.Count.ToString();
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        GV_DynamicGrid1.DataSource = null;
        GV_DynamicGrid1.DataBind();
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();

        if (dt.Rows.Count > 0)
        {

            ViewState["LearningBaseline"] = dt;
            if (dt.Rows.Count > 5000)
            {
                btnCSV_Click(LinkButton8, null);
            }
            else
            {
                GV_DynamicGrid.DataSource = dt;
                GV_DynamicGrid.DataBind();
            }



        }

        else
        {
            ViewState["LearningBaseline"] = null;
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();

        }

    }
    protected void LnkTeamBalika_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 8;
        ClearGrid();
        gvnroll.Visible = false;
        gvD2d.Visible = false;
        GvReport.Visible = false;
        gvUserReport.Visible = false;
        GV_DynamicGrid.Visible = false;
        GV_DynamicGrid2.Visible = false;
        LoadMasterData(1);
        GV_DynamicGrid1.Visible = true;

    }
    protected void btnRetention_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 218;
        ClearGrid();
        gvnroll.Visible = false;
        gvD2d.Visible = false;
        GvReport.Visible = false;
        gvUserReport.Visible = false;
        GV_DynamicGrid.Visible = false;
        GV_DynamicGrid2.Visible = false;
        RetentionIndividual(1);
        GV_DynamicGrid1.Visible = true;

    }
    protected void LnkTeamBalikaTraining_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 48;
        ClearGrid();
        gvnroll.Visible = false;
        gvD2d.Visible = false;
        GvReport.Visible = false;
        gvUserReport.Visible = false;
        GV_DynamicGrid.Visible = true;
        GV_DynamicGrid2.Visible = false;
        LoadMasterData(3);
        GV_DynamicGrid1.Visible = false;

    }
    protected void LnkUserRole_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 9;
        ClearGrid();
        gvnroll.Visible = false;
        gvD2d.Visible = false;
        GV_DynamicGrid1.Visible = false;
        GV_DynamicGrid.Visible = false;
        GvReport.Visible = false;
        gvUserReport.Visible = false;
        gvvillageschoolgrid.Visible = false;
        LoadMasterData(2);
        GV_DynamicGrid2.Visible = true;
    }

    public void LoadReportEnrollment()
    {

        conditions = "";
        string conditionsCr = "";
        string conditionsmo = "";
        string conditionsDe = "";
        string conditionsAll = "";
        lblTotalCount.Text = "";
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlState.SelectedIndex > 0)
        {
            conditions += " and mst5Village.StateCode = '" + ddlState.SelectedValue + "' ";
            conditionsAll += "  StateCode = '" + ddlState.SelectedValue + "' ";
        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " and mst5Village.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";
            conditionsAll += "  and DistCode = '" + ddlDistrict.SelectedValue + "' ";
        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions += " and mst5Village.BlockCode = '" + ddlBlock.SelectedValue + "' ";
            conditionsAll += " and  BlockCode = '" + ddlBlock.SelectedValue + "' ";
        }
        if (ddlPanchayat.SelectedIndex > 0)
        {
            conditions += " and mst5Village.PanchayatCode = '" + ddlPanchayat.SelectedValue + "' ";
        }
        if (ddlVillage.SelectedIndex > 0)
        {
            conditions += " and mst5Village.VillageCode = '" + ddlVillage.SelectedValue + "' ";
        }

        if (ViewState["1"].ToString() == "4")
        {
            GvReport.Visible = true;
            if (ddlUser.SelectedIndex > 0)
            {
                conditionsCr += " and CreateBy =  '" + ddlUser.SelectedValue + "' ";
                conditionsmo += " and ModifyBy = '" + ddlUser.SelectedValue + "' ";
                //conditionsDe += " and DeleteBy = '" + ddlUser.SelectedValue + "' ";
            }
            if (txtDate.Text != "")
            {
                DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                conditionsCr += " and Createdate >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
                conditionsmo += " and ModifyDate >= '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
                //conditionsDe += " and DeletedDate >= '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
            }
            if (txtTodate.Text != "")
            {
                DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                conditionsCr += " and Createdate <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";
                conditionsmo += " and ModifyDate <= '" + Todate.ToString("yyyy-MM-dd") + "' ";
                //conditionsDe += " and DeletedDate <= '" + Todate.ToString("yyyy-MM-dd") + "' ";
            }
            if (txtDate.Text != "" && txtTodate.Text != "")
            {
                DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                conditionsCr += " and Createdate BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
                conditionsmo += " and ModifyDate BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "'  and '" + Todate.ToString("yyyy-MM-dd") + "' ";
                //conditionsDe += " and DeletedDate BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "'  and '" + Todate.ToString("yyyy-MM-dd") + "' ";
            }
            if (ddlUser.SelectedIndex > 0)
            {
                conditionsAll += " and UserName1 =  '" + ddlUser.SelectedValue + "' ";

            }
            string FristCon = conditions + conditionsCr;
            string Second = conditions + conditionsmo;
            string Third = conditions + conditionsDe;
            DataTable dt = objMain.ReportEnrollment(FristCon, Second, Third, conditionsAll);
            if (dt.Rows.Count > 0)
            {
                ViewState["EnrollSummary"] = dt;
                lblTotalCount.Text = (dt.Rows.Count).ToString();
                if (dt.Rows.Count > 3000)
                {
                    btnCSV_Click(LinkButton8, null);
                }
                else
                {
                    GvReport.DataSource = dt;
                    GvReport.DataBind();
                }

            }
            else
            {
                GvReport.DataSource = null;
                ViewState["EnrollSummary"] = null;
                GvReport.DataBind();
            }
        }


        if (ViewState["1"].ToString() == "6")
        {

            if (ddlUser.SelectedIndex > 0)
            {
                conditionsCr += " and tblEnrolment.CreateBy =  '" + ddlUser.SelectedValue + "' ";

            }
            if (txtDate.Text != "" && txtTodate.Text == "")
            {
                DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                conditionsCr += " and tblEnrolment.Createdate >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
            }
            if (txtTodate.Text != "" && txtDate.Text == "")
            {
                DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                conditionsCr += " and tblEnrolment.Createdate <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";

            }
            if (txtDate.Text != "" && txtTodate.Text != "")
            {
                DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                conditionsCr += " and tblEnrolment.Createdate BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
            }

            string FristCon = conditions + conditionsCr;

            DataTable dt = objMain.ReportEnrollDeatils(FristCon);
            if (dt.Rows.Count > 0)
            {
                gvnroll.Visible = true;
                ViewState["Enroll"] = dt;
                lblTotalCount.Text = (dt.Rows.Count).ToString();
                if (dt.Rows.Count > 4000)
                {
                    btnCSV_Click(LinkButton4, null);
                }
                else
                {
                    gvnroll.DataSource = dt;
                    gvnroll.DataBind();
                }


            }
            else
            {
                gvnroll.DataSource = null;
                gvnroll.DataBind();
                ViewState["Enroll"] = null;
            }
        }





        if (ViewState["1"].ToString() == "5")
        {

            if (ddlUser.SelectedIndex > 0)
            {
                conditionsCr += " and CreateBy =  '" + ddlUser.SelectedValue + "' ";

            }
            if (txtDate.Text != "")
            {
                DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                conditionsCr += " and Createdate >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
            }
            if (txtTodate.Text != "")
            {
                DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                conditionsCr += " and Createdate <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";

            }
            if (txtDate.Text != "" && txtTodate.Text != "")
            {
                DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                conditionsCr += " and Createdate BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
            }

            string FristCon = conditions + conditionsCr;

            DataTable dt = objMain.ReportEnrollUserWiseEntry(FristCon);
            if (dt.Rows.Count > 0)
            {
                ViewState["ENrollDetail"] = dt;
                lblTotalCount.Text = (dt.Rows.Count).ToString();

                if (dt.Rows.Count > 3000)
                {
                    btnCSV_Click(LinkButton8, null);
                }
                else
                {
                    gvUserReport.Visible = true;
                    gvUserReport.DataSource = dt;
                    gvUserReport.DataBind();
                }
            }
            else
            {
                ViewState["ENrollDetail"] = null;
                gvUserReport.DataSource = null;
                gvUserReport.DataBind();
            }
        }
    }
    public void ReportMobileActivityStatus(int Flag)
    {
        string condition = string.Empty;
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlState.SelectedIndex > 0)
        {
            conditions += " and mst5Village.StateCode = '" + ddlState.SelectedValue + "' ";

        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " and mst5Village.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";

        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions += " and mst5Village.BlockCode = '" + ddlBlock.SelectedValue + "' ";

        }
        if (ddlPanchayat.SelectedIndex > 0)
        {
            conditions += " and mst5Village.PanchayatCode = '" + ddlPanchayat.SelectedValue + "' ";
        }
        if (ddlVillage.SelectedIndex > 0)
        {
            conditions += " and mst5Village.VillageCode = '" + ddlVillage.SelectedValue + "' ";
        }
        if (ddlUser.SelectedIndex > 0)
        {
            conditions += " and tblDTD.CreateBy =  '" + ddlUser.SelectedValue + "' ";

        }


        DataTable dt = objMain.ReportMobileActivityStatus(conditions, ddlYear.SelectedValue.ToString());
        ViewState["ReportMobileActivityStatus"] = dt;

        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        GV_DynamicGrid1.DataSource = null;
        GV_DynamicGrid1.DataBind();
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();

        if (ViewState["1"].ToString() == "116")
        {
            lblTotalCount.Text = (dt.Rows.Count).ToString();
            if (dt.Rows.Count > 300)
            {
                btnCSV_Click(LinkButton5, null);
            }
            else
            {
                GV_DynamicGrid.DataSource = dt;
                GV_DynamicGrid.DataBind();
            }

        }


    }
    public void RetentionIndividual(int Flag)
    {
        string condition = string.Empty;
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += " and  mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlState.SelectedIndex > 0)
        {
            conditions += " and mst5Village.StateCode = '" + ddlState.SelectedValue + "' ";

        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " and mst5Village.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";

        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions += " and mst5Village.BlockCode = '" + ddlBlock.SelectedValue + "' ";

        }
        if (ddlPanchayat.SelectedIndex > 0)
        {
            conditions += " and mst5Village.PanchayatCode = '" + ddlPanchayat.SelectedValue + "' ";
        }
        if (ddlVillage.SelectedIndex > 0)
        {
            conditions += " and mst5Village.VillageCode = '" + ddlVillage.SelectedValue + "' ";
        }

        DataTable dt = objMain.rptRetentionIndividual(conditions);
        ViewState["RetentionIndividual"] = dt;

        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        GV_DynamicGrid1.DataSource = null;
        GV_DynamicGrid1.DataBind();
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();


        lblTotalCount.Text = (dt.Rows.Count).ToString();
        if (dt.Rows.Count > 500)
        {
            btnCSV_Click(LinkButton8, null);
        }
        else
        {
            GV_DynamicGrid.Visible = true;
            GV_DynamicGrid.DataSource = dt;
            GV_DynamicGrid.DataBind();
        }





    }
    public void LoadSIP(int Flag)
    {
        string condition = string.Empty;

        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "    where V.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlState.SelectedIndex > 0)
        {
            conditions += " and V.StateCode = '" + ddlState.SelectedValue + "' ";

        }


        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " and V.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";

        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions += " and V.BlockCode = '" + ddlBlock.SelectedValue + "' ";

        }
        if (ddlPanchayat.SelectedIndex > 0)
        {
            conditions += " and V.PanchayatCode = '" + ddlPanchayat.SelectedValue + "' ";
        }
        if (ddlVillage.SelectedIndex > 0)
        {
            conditions += " and V.VillageCode = '" + ddlVillage.SelectedValue + "' ";
        }
        DataTable dt = objMain.LoadSIPData(conditions, Flag);
        ViewState["SIPDetails"] = dt;

        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        GV_DynamicGrid1.DataSource = null;
        GV_DynamicGrid1.DataBind();
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();

        if (ViewState["1"].ToString() == "28")
        {
            lblTotalCount.Text = (dt.Rows.Count).ToString();
            if (dt.Rows.Count > 1500)
            {
                btnCSV_Click(LinkButton8, null);
            }
            else
            {
                GV_DynamicGrid.DataSource = dt;
                GV_DynamicGrid.DataBind();
            }

        }



    }
    public void LoadMasterData(int Flag)
    {
        string condition = string.Empty;
        if (Flag == 2)
        {
            if (ddlState.SelectedIndex > 0)
            {
                conditions += " Where V.StateCode = '" + ddlState.SelectedValue + "' ";

            }
        }
        else
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "    where V.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (ddlState.SelectedIndex > 0)
            {
                conditions += " and V.StateCode = '" + ddlState.SelectedValue + "' ";

            }
        }

        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " and V.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";

        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions += " and V.BlockCode = '" + ddlBlock.SelectedValue + "' ";

        }
        if (ddlPanchayat.SelectedIndex > 0)
        {
            conditions += " and V.PanchayatCode = '" + ddlPanchayat.SelectedValue + "' ";
        }
        if (ddlVillage.SelectedIndex > 0)
        {
            conditions += " and V.VillageCode = '" + ddlVillage.SelectedValue + "' ";
        }
        DataTable dt = objMain.LoadMasterData(conditions, Flag);
        ViewState["D2dUser"] = dt;

        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        GV_DynamicGrid1.DataSource = null;
        GV_DynamicGrid1.DataBind();
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();

        if (ViewState["1"].ToString() == "7")
        {
            lblTotalCount.Text = (dt.Rows.Count).ToString();
            if (dt.Rows.Count > 1500)
            {
                btnCSV_Click(LinkButton8, null);
            }
            else
            {
                GV_DynamicGrid.DataSource = dt;
                GV_DynamicGrid.DataBind();
            }

        }
        else if (ViewState["1"].ToString() == "8")
        {
            lblTotalCount.Text = (dt.Rows.Count).ToString();
            if (dt.Rows.Count > 1500)
            {
                btnCSV_Click(LinkButton8, null);
            }
            else
            {
                GV_DynamicGrid1.DataSource = dt;
                GV_DynamicGrid1.DataBind();
            }

        }
        if (ViewState["1"].ToString() == "48")
        {
            lblTotalCount.Text = (dt.Rows.Count).ToString();
            if (dt.Rows.Count > 1500)
            {
                btnCSV_Click(LinkButton8, null);
            }
            else
            {
                GV_DynamicGrid.DataSource = dt;
                GV_DynamicGrid.DataBind();
            }

        }
        else
        {
            GV_DynamicGrid2.DataSource = dt;
            GV_DynamicGrid2.DataBind();
            lblTotalCount.Text = (dt.Rows.Count).ToString();
        }


    }
    public void LoadReport()
    {

        conditions = "";
        string conditionsCr = "";
        string conditionsmo = "";
        string conditionsDe = "";
        string conditionsAll = "";
        lblTotalCount.Text = "";
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlState.SelectedIndex > 0)
        {
            conditions += "  and mst5Village.StateCode = '" + ddlState.SelectedValue + "' ";
            conditionsAll += "  StateCode = '" + ddlState.SelectedValue + "' ";
        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " and mst5Village.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";
            conditionsAll += "  and DistCode = '" + ddlDistrict.SelectedValue + "' ";
        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions += " and mst5Village.BlockCode = '" + ddlBlock.SelectedValue + "' ";
            conditionsAll += " and  BlockCode = '" + ddlBlock.SelectedValue + "' ";
        }
        if (ddlPanchayat.SelectedIndex > 0)
        {
            conditions += " and mst5Village.PanchayatCode = '" + ddlPanchayat.SelectedValue + "' ";
        }
        if (ddlVillage.SelectedIndex > 0)
        {
            conditions += " and mst5Village.VillageCode = '" + ddlVillage.SelectedValue + "' ";
        }

        if (ViewState["1"].ToString() == "1")
        {
            GvReport.Visible = true;
            if (ddlUser.SelectedIndex > 0)
            {
                conditionsCr += " and CreateBy =  '" + ddlUser.SelectedValue + "' ";
                conditionsmo += " and ModifyBy = '" + ddlUser.SelectedValue + "' ";
                conditionsDe += " and DeleteBy = '" + ddlUser.SelectedValue + "' ";
            }
            if (txtDate.Text != "")
            {
                DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                conditionsCr += " and Createdate >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
                conditionsmo += " and ModifyDate >= '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
                conditionsDe += " and DeletedDate >= '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
            }
            if (txtTodate.Text != "")
            {
                DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                conditionsCr += " and Createdate <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";
                conditionsmo += " and ModifyDate <= '" + Todate.ToString("yyyy-MM-dd") + "' ";
                conditionsDe += " and DeletedDate <= '" + Todate.ToString("yyyy-MM-dd") + "' ";
            }
            if (txtDate.Text != "" && txtTodate.Text != "")
            {
                DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                conditionsCr += " and Createdate BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
                conditionsmo += " and ModifyDate BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "'  and '" + Todate.ToString("yyyy-MM-dd") + "' ";
                conditionsDe += " and DeletedDate BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "'  and '" + Todate.ToString("yyyy-MM-dd") + "' ";
            }
            if (ddlUser.SelectedIndex > 0)
            {
                conditionsAll += " and UserName1 =  '" + ddlUser.SelectedValue + "' ";

            }
            string FristCon = conditions + conditionsCr;
            string Second = conditions + conditionsmo;
            string Third = conditions + conditionsDe;
            DataTable dt = objMain.Report(FristCon, Second, Third, conditionsAll);
            if (dt.Rows.Count > 0)
            {
                GvReport.DataSource = dt;
                GvReport.DataBind();
                ViewState["D2dUser"] = dt;
                lblTotalCount.Text = (dt.Rows.Count).ToString();
            }
            else
            {
                GvReport.DataSource = null;
                GvReport.DataBind();
                lblTotalCount.Text = "";
            }
        }


        if (ViewState["1"].ToString() == "2")
        {

            if (ddlUser.SelectedIndex > 0)
            {
                conditionsCr += " and tblDTD.CreateBy =  '" + ddlUser.SelectedValue + "' ";

            }
            if (txtDate.Text != "" && txtTodate.Text == "")
            {
                DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                conditionsCr += " and tblDTD.Createdate >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
            }
            if (txtTodate.Text != "" && txtDate.Text == "")
            {
                DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                conditionsCr += " and tblDTD.Createdate <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";

            }
            if (txtDate.Text != "" && txtTodate.Text != "")
            {
                DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                conditionsCr += " and tblDTD.Createdate BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
            }

            string FristCon = conditions + conditionsCr;

            DataTable dt = objMain.ReportD2d(FristCon);
            if (dt.Rows.Count > 0)
            {
                gvD2d.Visible = true;
                ViewState["D2dAllData"] = dt;
                lblTotalCount.Text = (dt.Rows.Count).ToString();
                if (dt.Rows.Count > 10000)
                {
                    btnCSV_Click(LinkButton5, null);
                }
                else
                {

                    gvD2d.DataSource = dt;
                    gvD2d.DataBind();
                }



            }
            else
            {
                gvD2d.DataSource = null;
                gvD2d.DataBind();
                lblTotalCount.Text = "";
            }
        }


        if (ViewState["1"].ToString() == "10")
        {

            if (ddlUser.SelectedIndex > 0)
            {
                conditionsCr += " and tblDTD.CreateBy =  '" + ddlUser.SelectedValue + "' ";

            }
            if (txtDate.Text != "" && txtTodate.Text == "")
            {
                DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                conditionsCr += " and tblDTD.Createdate >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
            }
            if (txtTodate.Text != "" && txtDate.Text == "")
            {
                DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                conditionsCr += " and tblDTD.Createdate <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";

            }
            if (txtDate.Text != "" && txtTodate.Text != "")
            {
                DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                conditionsCr += " and tblDTD.Createdate BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
            }

            string FristCon = conditions + conditionsCr;

            DataTable dt = objMain.OutD2d(FristCon);
            if (dt.Rows.Count > 0)
            {
                gvD2d.Visible = true;
                ViewState["OutD2d"] = dt;
                lblTotalCount.Text = (dt.Rows.Count).ToString();
                if (dt.Rows.Count > 10000)
                {
                    btnCSV_Click(LinkButton5, null);
                }
                else
                {
                    gvD2d.DataSource = dt;
                    gvD2d.DataBind();
                }

            }
            else
            {
                gvD2d.DataSource = null;
                gvD2d.DataBind();
                lblTotalCount.Text = "";
            }
        }



        if (ViewState["1"].ToString() == "3")
        {

            if (ddlUser.SelectedIndex > 0)
            {
                conditionsCr += " and CreateBy =  '" + ddlUser.SelectedValue + "' ";

            }
            if (txtDate.Text != "")
            {
                DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                conditionsCr += " and Createdate >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
            }
            if (txtTodate.Text != "")
            {
                DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                conditionsCr += " and Createdate <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";

            }
            if (txtDate.Text != "" && txtTodate.Text != "")
            {
                DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                conditionsCr += " and Createdate BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
            }

            string FristCon = conditions + conditionsCr;

            DataTable dt = objMain.ReportUserEntery(FristCon);
            if (dt.Rows.Count > 0)
            {
                gvUserReport.Visible = true;
                gvUserReport.DataSource = dt;
                gvUserReport.DataBind();
                ViewState["D2dUserDet"] = dt;
                lblTotalCount.Text = (dt.Rows.Count).ToString();
            }
            else
            {
                gvUserReport.DataSource = null;
                gvUserReport.DataBind();
                lblTotalCount.Text = "";
            }
        }
    }
    public void LoadIneligable(int Flag)
    {
        string condition = string.Empty;
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlState.SelectedIndex > 0)
        {
            conditions += " and mst5Village.StateCode = '" + ddlState.SelectedValue + "' ";

        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " and mst5Village.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";

        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions += " and mst5Village.BlockCode = '" + ddlBlock.SelectedValue + "' ";

        }
        if (ddlPanchayat.SelectedIndex > 0)
        {
            conditions += " and mst5Village.PanchayatCode = '" + ddlPanchayat.SelectedValue + "' ";
        }
        if (ddlVillage.SelectedIndex > 0)
        {
            conditions += " and mst5Village.VillageCode = '" + ddlVillage.SelectedValue + "' ";
        }
        if (ddlUser.SelectedIndex > 0)
        {
            conditions += " and tblDTD.CreateBy =  '" + ddlUser.SelectedValue + "' ";

        }
        if (txtDate.Text != "" && txtTodate.Text == "")
        {
            DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
            conditions += " and tblDTD.Createdate >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
        }
        if (txtTodate.Text != "" && txtDate.Text == "")
        {
            DateTime Todate = Convert.ToDateTime(txtTodate.Text);
            conditions += " and tblDTD.Createdate <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";

        }
        if (txtDate.Text != "" && txtTodate.Text != "")
        {
            DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
            DateTime Todate = Convert.ToDateTime(txtTodate.Text);
            conditions += " and tblDTD.Createdate BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
        }

        DataTable dt = objMain.LoadDTDInEligible(conditions);
        ViewState["D2dUser"] = dt;

        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        GV_DynamicGrid1.DataSource = null;
        GV_DynamicGrid1.DataBind();
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();

        if (ViewState["1"].ToString() == "16")
        {
            lblTotalCount.Text = (dt.Rows.Count).ToString();
            if (dt.Rows.Count > 5000)
            {
                btnCSV_Click(LinkButton5, null);
            }
            else
            {
                GV_DynamicGrid.DataSource = dt;
                GV_DynamicGrid.DataBind();
            }

        }


    }
    protected void GvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ddlDistrict.SelectedIndex > 0)
            {
                GvReport.Columns[2].Visible = true;
            }
            else
            {
                GvReport.Columns[2].Visible = false;
            }
        }
    }
    protected void Dgv_LeftGrid_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {


            GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell2 = new TableCell();


            HeaderCell2 = new TableCell();
            HeaderCell2.Text = "Original";
            HeaderCell2.ControlStyle.Font.Bold = true;
            HeaderCell2.ControlStyle.Font.Size = 15;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
            HeaderCell2.ColumnSpan = 9;
            HeaderRow.Cells.Add(HeaderCell2);

            HeaderCell2 = new TableCell();
            HeaderCell2.Text = "Verify";
            HeaderCell2.ControlStyle.Font.Bold = true;
            HeaderCell2.HorizontalAlign = HorizontalAlign.Left;
            HeaderCell2.ControlStyle.Font.Size = 15;
            HeaderCell2.ColumnSpan = 9;
            HeaderRow.Cells.Add(HeaderCell2);


            gvD2d.Controls[0].Controls.AddAt(0, HeaderRow);
        }
    }
    public void LoadUserLeavel()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            ddlState.SelectedIndex = 1;
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

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
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

            ddlDistrict.SelectedIndex = 0;

            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

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
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

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
            conditions = "StateCode ='" + ddlState.SelectedValue + "'   and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else
        {
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";


        }


        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");



    }


    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            ddlState.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlDistrict, null);
            if (Session["user_level_Role"].ToString() == "3" || Session["user_level_Role"].ToString() == "4")
            {
                if (ddlDistrict.Items.Count > 0)
                {
                    ddlDistrict.SelectedIndex = 1;
                }
            }
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);

            ddlPanchayat.Items.Clear();
            ddlVillage.Items.Clear();
        }
        else
        {
            ddlState.SelectedIndex = 0;
            ddlDistrict.Items.Clear();
            ddlBlock.Items.Clear();
            ddlPanchayat.Items.Clear();
            ddlVillage.Items.Clear();
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBBock();
        FillUser();
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBCluster();
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillage();
    }
    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadReport();
    }
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    public void FillCBBock()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        else if (Session["user_level_Role"].ToString() == "4")
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 and BlockCode in( " + Session["BlockCode"].ToString() + " ) and FYear ='" + ddlYear.SelectedItem.Text + "' ";
        }
        else
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");



    }
    public void FillCBCluster()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "'";
        objComman.BindDLL("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "--All--");



    }
    public void FillCVillage()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--All-");



    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    private void ExporttoExcel(GridView Gv, DataTable table, string FileName)
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
            int columnscount = Gv.HeaderRow.Cells.Count;


            for (int j = 0; j < columnscount; j++)
            {      //write in new column
                HttpContext.Current.Response.Write("<Td>");
                //Get column headers  and make it as bold in excel columns
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(Gv.HeaderRow.Cells[j].Text);
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");
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
    //protected void ExportToExcel(GridView GvReport)
    //{

    //    Response.Clear();
    //    Response.Buffer = true;
    //    Response.ClearContent();
    //    Response.ClearHeaders();
    //    Response.Charset = "";
    //    string FileName = "D2DReport" + DateTime.Now + ".xls";
    //    StringWriter strwritter = new StringWriter();
    //    HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
    //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //    Response.ContentType = "application/vnd.ms-excel";
    //    Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);

    //    GvReport.GridLines = GridLines.Both;  
    //    GvReport.HeaderStyle.Font.Bold = true;  
    //    GvReport.RenderControl(htmltextwrtter);
    //    Response.Write(strwritter.ToString());
    //    Response.End();
    //    Response.Clear();
    //    Response.Buffer = true;
    //    Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
    //    Response.Charset = "";
    //    Response.ContentType = "application/vnd.ms-excel";
    //    using (StringWriter sw = new StringWriter())
    //    {
    //        HtmlTextWriter hw = new HtmlTextWriter(sw);

    //        //To Export all pages
    //        GvReport.AllowPaging = false;
    //         GvReport.HeaderRow.BackColor = Color.White;
    //        foreach (TableCell cell in GvReport.HeaderRow.Cells)
    //        {
    //            cell.BackColor = GvReport.HeaderStyle.BackColor;
    //        }
    //        foreach (GridViewRow row in GvReport.Rows)
    //        {
    //            row.BackColor = Color.White;
    //            foreach (TableCell cell in row.Cells)
    //            {
    //                if (row.RowIndex % 2 == 0)
    //                {
    //                    cell.BackColor = GvReport.AlternatingRowStyle.BackColor;
    //                }
    //                else
    //                {
    //                    cell.BackColor = GvReport.RowStyle.BackColor;
    //                }
    //                cell.CssClass = "textmode";
    //            }
    //        }

    //        GvReport.RenderControl(hw);

    //        //style to format numbers to string
    //        string style = @"<style> .textmode { } </style>";
    //        Response.Write(style);
    //        Response.Output.Write(sw.ToString());
    //        Response.Flush();
    //        Response.End();
    //}


    private void ExportToCSVFile(DataTable dtTable, string filePath)
    {
        StringBuilder sbldr = new StringBuilder();
        if (dtTable != null)
        {
            if (dtTable.Columns.Count != 0)
            {
                foreach (DataColumn col in dtTable.Columns)
                {
                    sbldr.Append(col.ColumnName + ',');
                }
                sbldr.Append("\r\n");
                foreach (DataRow row in dtTable.Rows)
                {
                    foreach (DataColumn column in dtTable.Columns)
                    {

                        sbldr.Append(Convert.ToString(row[column]).Replace(",", "  ").Replace("\r", "").Replace("\n", "") + ',');
                    }
                    sbldr.Append("\r\n");

                }
            }
            string sFileDir = Server.MapPath(Comman.GetImagePath("DataBackupPath")); ;
            string Fullfilename = "" + filePath + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".csv";
            string path = sFileDir + Fullfilename;
            File.WriteAllText(path, sbldr.ToString());

            FileStream fs = null;//, fs2=null;
            try
            {
                string path1 = Fullfilename;
                string foldername = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + path1 + "");
                string datafolder = path1.Substring(0, path1.Length - 4);
                //  string[] file = Directory.GetFiles(foldername);

                string fullPath = Request.MapPath("~/DataBackup/" + datafolder + "" + ".zip");
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(foldername, "");
                    //    zip.AddFiles(file, foldername);
                    zip.Save(Server.MapPath(Comman.GetImagePath("DataBackupPath")) + "/" + datafolder + "" + ".zip");
                }



                HttpResponse Response = HttpContext.Current.Response; Response.Clear(); Response.ClearHeaders(); Response.Charset = "UTF-8";
                fs = File.Open(fullPath, FileMode.Open);
                byte[] bytBytes = new byte[(fs.Length)];
                fs.Read(bytBytes, 0, (int)fs.Length);
                fs.Close();
                Response.AddHeader("Content-disposition", "attachment; filename=" + fullPath);
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(bytBytes);






                if (File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                if (File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                Response.Flush();
                Response.End();
            }

            catch (System.Exception ex)
            {
                Response.Clear();

                //string mmsg = ex.Message;
                //showEXPMessages("(crateZip)  " + mmsg); //showMessages(mmsg);
            }
            finally
            {
                fs.Dispose();
                Response.Clear();

            }
        }
        //str.Write(sbldr.ToString());
        //Response.ContentType = "Application/x-msexcel";
        //Response.AddHeader("content-disposition", "attachment;filename=test.csv");
        //Response.Write(sbldr.ToString());
        //Response.End();
    }


    private void ExporttoCSV(GridView Gv, DataTable table)
    {
        var dataTable = table;
        StringBuilder builder = new StringBuilder();
        List<string> columnNames = new List<string>();
        List<string> rows = new List<string>();

        foreach (DataColumn column in dataTable.Columns)
        {
            columnNames.Add(column.ColumnName);
        }

        builder.Append(string.Join(",", columnNames.ToArray())).Append("\n");

        foreach (DataRow row in dataTable.Rows)
        {
            List<string> currentRow = new List<string>();

            foreach (DataColumn column in dataTable.Columns)
            {
                object item = row[column];
                if (item == "82102077-0490")
                {
                    string str = "";
                }
                currentRow.Add(item.ToString());
            }

            rows.Add(string.Join(",", currentRow.ToArray()));
        }

        builder.Append(string.Join("\n", rows.ToArray()));

        Response.Clear();
        Response.ContentType = "text/csv";
        Response.AddHeader("Content-Disposition", "attachment;filename=Reports.csv");
        Response.Write(builder.ToString());
        Response.End();


    }
    public static void ToCSV(DataTable dtDataTable, string strFilePath, GridView Gv)
    {
        StreamWriter sw = new StreamWriter(strFilePath, false);
        //headers  



        int columnscount = Gv.HeaderRow.Cells.Count;


        for (int j = 0; j < columnscount; j++)
        {      //write in new column

            sw.Write(Gv.HeaderRow.Cells[j].Text);
            sw.Write(",");
        }

        sw.Write(sw.NewLine);
        foreach (DataRow dr in dtDataTable.Rows)
        {
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                if (!Convert.IsDBNull(dr[i]))
                {
                    string value = dr[i].ToString();
                    if (value == "82102077-0490")
                    {
                        string str = "";
                    }

                    sw.Write(dr[i].ToString());
                    //if (value.Contains("\""))
                    //{
                    //    value = value.Replace("\"", "\"\"");
                    //}

                    //else if (value.Contains(","))
                    //{
                    //    value = String.Format("\"{0}\"", value);
                    //}
                    //else if (value.Contains("."))
                    //{
                    //    value = String.Format("\"{0}\"", value);
                    //}
                    //else if (value.Contains(System.Environment.NewLine))
                    //{
                    //    value = String.Format("\"{0}\"", value);
                    //}
                    //else if (value.Contains(','))
                    //{
                    //    value = String.Format("\"{0}\"", value);
                    //    sw.Write(value);
                    //}
                    //else
                    //{
                    //    sw.Write(dr[i].ToString());
                    //}
                }
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
        }
        sw.Close();
    }
    protected void gvD2d_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvD2d.PageIndex = e.NewPageIndex;
        if (ViewState["D2dAllData"] != null)
        {
            DataTable dt = ViewState["D2dAllData"] as DataTable;
            gvD2d.DataSource = dt;
            gvD2d.DataBind();
        }

    }
    protected void gvnroll_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvnroll.PageIndex = e.NewPageIndex;
        if (ViewState["Enroll"] != null)
        {
            DataTable Dt = ViewState["Enroll"] as DataTable;
            gvnroll.DataSource = Dt;
            gvnroll.DataBind();
        }
    }
    protected void GV_DynamicGrid_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_DynamicGrid.PageIndex = e.NewPageIndex;
        if (ViewState["D2dUser"] != null)
        {
            DataTable Dt = ViewState["D2dUser"] as DataTable;
            GV_DynamicGrid.DataSource = Dt;
            GV_DynamicGrid.DataBind();
        }
    }


    protected void GV_DynamicGrid1_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_DynamicGrid1.PageIndex = e.NewPageIndex;
        if (ViewState["D2dUser"] != null)
        {
            DataTable Dt = ViewState["D2dUser"] as DataTable;
            GV_DynamicGrid1.DataSource = Dt;
            GV_DynamicGrid1.DataBind();
        }
    }
    protected void GV_DynamicGrid2_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_DynamicGrid2.PageIndex = e.NewPageIndex;
        if (ViewState["D2dUser"] != null)
        {
            DataTable Dt = ViewState["D2dUser"] as DataTable;
            GV_DynamicGrid2.DataSource = Dt;
            GV_DynamicGrid2.DataBind();
        }
    }
    #region Abhimanyu

    protected void btnCSV_Click(object sender, EventArgs e)
    {
        //if (ViewState["1"].ToString() == "2")
        //{
        //    DataTable dt = (DataTable)ViewState["D2dAllData"];
        //    //ExporttoCSV(gvD2d, dt);

        //    ExportToCSVFile(dt);
        //}
        if (ViewState["1"].ToString() == "2")
        {
            DataTable dt = (DataTable)ViewState["D2dAllData"];
            //ExporttoExcel(gvD2d, dt, "D2D");
            ExportToCSVFile(dt, "D2d");
        }

        if (ViewState["1"].ToString() == "3")
        {
            DataTable dt = (DataTable)ViewState["D2dUserDet"];
            //  ExporttoExcel(gvUserReport, dt, "");
            ExportToCSVFile(dt, "");
        }
        if (ViewState["1"].ToString() == "1")
        {
            DataTable dt = (DataTable)ViewState["D2dUser"];
            //ExporttoExcel(GvReport, dt, "");
            ExportToCSVFile(dt, "");
        }
        if (ViewState["1"].ToString() == "7")
        {
            DataTable dt = (DataTable)ViewState["D2dUser"];
            //   ExporttoExcel(GV_DynamicGrid, dt, "MasterData");

            ExportToCSVFile(dt, "");
        }
        if (ViewState["1"].ToString() == "6")
        {
            DataTable dt = (DataTable)ViewState["Enroll"];
            //   ExporttoExcel(gvnroll, dt, "Enrolllment");

            ExportToCSVFile(dt, "Enrolllment");
        }
        if (ViewState["1"].ToString() == "4")
        {
            DataTable dt = (DataTable)ViewState["EnrollSummary"];
            //  ExporttoExcel(GvReport, dt, "");

            ExportToCSVFile(dt, "");
        }
        if (ViewState["1"].ToString() == "5")
        {

            DataTable dt = (DataTable)ViewState["ENrollDetail"];
            // ExporttoExcel(gvUserReport, dt, "");

            ExportToCSVFile(dt, "");
        }
        if (ViewState["1"].ToString() == "8")
        {
            DataTable dt = (DataTable)ViewState["D2dUser"];
            // ExporttoExcel(GV_DynamicGrid1, dt, "TeamBalika");

            ExportToCSVFile(dt, "TeamBalika");
        }

        if (ViewState["1"].ToString() == "48")
        {
            DataTable dt = (DataTable)ViewState["D2dUser"];
            // ExporttoExcel(GV_DynamicGrid1, dt, "TeamBalika");

            ExportToCSVFile(dt, "TBTraining");
        }
        if (ViewState["1"].ToString() == "9")
        {
            DataTable dt = (DataTable)ViewState["D2dUser"];
            // ExporttoExcel(GV_DynamicGrid2, dt, "UserMaster");

            ExportToCSVFile(dt, "UserMaster");
        }
        if (ViewState["1"].ToString() == "7")
        {
            DataTable dt = (DataTable)ViewState["D2dUser"];
            //ExporttoCSV(GV_DynamicGrid, dt);
            ExportToCSVFile(dt, "Master");
        }
        if (ViewState["1"].ToString() == "10")
        {
            DataTable dt = (DataTable)ViewState["OutD2d"];
            //  ExporttoExcel(gvD2d, dt, "OutOfDoorToDoor");

            ExportToCSVFile(dt, "OutOfDoorToDoor");
        }

        if (ViewState["1"].ToString() == "11")
        {
            DataTable dt = (DataTable)ViewState["villageschool"];
            //  ExporttoExcel(gvvillageschoolgrid, dt, "VillageProfile");

            ExportToCSVFile(dt, "VillageProfile");
        }

        if (ViewState["1"].ToString() == "15")
        {
            DataTable dt = (DataTable)ViewState["LearningBaseline"];
            // ExporttoExcel(GV_DynamicGrid, dt, "LearningBaseline");

            ExportToCSVFile(dt, "LearningBaseline");
        }
        if (ViewState["1"].ToString() == "16")
        {
            DataTable dt = (DataTable)ViewState["D2dUser"];
            // ExporttoExcel(GV_DynamicGrid, dt, "InEligible");

            ExportToCSVFile(dt, "InEligible");
        }

        if (ViewState["1"].ToString() == "18")
        {
            DataTable dt = (DataTable)ViewState["villageschool"];
            // ExporttoExcel(gvvillageschoolgrid, dt, "SIC");

            ExportToCSVFile(dt, "SIC");
        }
        if (ViewState["1"].ToString() == "28")
        {
            DataTable dt = (DataTable)ViewState["SIPDetails"];
            // ExporttoExcel(gvvillageschoolgrid, dt, "SIC");

            ExportToCSVFile(dt, "SIP");
        }
        if (ViewState["1"].ToString() == "116")
        {
            DataTable dt = (DataTable)ViewState["ReportMobileActivityStatus"];
            // ExporttoExcel(gvvillageschoolgrid, dt, "SIC");

            ExportToCSVFile(dt, "dailystatus");
        }
        if (ViewState["1"].ToString() == "218")
        {
            DataTable dt = (DataTable)ViewState["RetentionIndividual"];
            // ExporttoExcel(gvvillageschoolgrid, dt, "SIC");

            ExportToCSVFile(dt, "RetentionIndividual");
        }

        //if (ViewState["1"].ToString() == "3")
        //{
        //    DataTable dt = (DataTable)ViewState["D2dUserDet"];
        //    ExporttoCSV(gvUserReport, dt);
        //}
        //if (ViewState["1"].ToString() == "1")
        //{
        //    DataTable dt = (DataTable)ViewState["D2dUser"];
        //    ExporttoCSV(GvReport, dt);
        //}
        //if (ViewState["1"].ToString() == "7")
        //{
        //    DataTable dt = (DataTable)ViewState["D2dUser"];
        //    ExporttoCSV(GV_DynamicGrid, dt);
        //}
        //if (ViewState["1"].ToString() == "6")
        //{
        //    DataTable dt = (DataTable)ViewState["Enroll"];
        //    ExporttoCSV(gvnroll, dt);
        //}
        //if (ViewState["1"].ToString() == "4")
        //{
        //    DataTable dt = (DataTable)ViewState["EnrollSummary"];
        //    ExporttoCSV(GvReport, dt);
        //}
        //if (ViewState["1"].ToString() == "5")
        //{

        //    DataTable dt = (DataTable)ViewState["ENrollDetail"];
        //    ExporttoCSV(gvUserReport, dt);
        //}
        //if (ViewState["1"].ToString() == "8")
        //{
        //    DataTable dt = (DataTable)ViewState["D2dUser"];
        //    ExporttoCSV(GV_DynamicGrid1, dt);
        //}
        //if (ViewState["1"].ToString() == "9")
        //{
        //    DataTable dt = (DataTable)ViewState["D2dUser"];
        //    ExporttoCSV(GV_DynamicGrid2, dt);
        //}

        //if (ViewState["1"].ToString() == "10")
        //{
        //    DataTable dt = (DataTable)ViewState["OutD2d"];
        //    ExporttoCSV(gvD2d, dt);
        //}

        //if (ViewState["1"].ToString() == "11")
        //{
        //    DataTable dt = (DataTable)ViewState["villageschool"];
        //    ExporttoCSV(gvvillageschoolgrid, dt);
        //}
        //if (ViewState["1"].ToString() == "15")
        //{
        //    DataTable dt = (DataTable)ViewState["LearningBaseline"];
        //    ExporttoCSV(GV_DynamicGrid1, dt);
        //}
        //if (ViewState["1"].ToString() == "18")
        //{
        //    DataTable dt = (DataTable)ViewState["villageschool"];
        //    ExporttoExcel(gvvillageschoolgrid, dt,"SIC");
        //}
        //if (ViewState["1"].ToString() == "16")
        //{
        //    DataTable dt = (DataTable)ViewState["D2dUser"];
        //    ExporttoExcel(GV_DynamicGrid, dt,"InEligible");
        //}

    }




    #endregion

    #region--------------Chhavi-------------------------------

    protected void village_Profile_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 11;
        getreport(3);
    }

    protected void SIC_Data_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 18;
        getreport(4);
    }

    public void getreport(int flag)
    {

        string conditions = "";
        gvnroll.Visible = false;
        gvD2d.Visible = false;
        GV_DynamicGrid1.Visible = false;
        GV_DynamicGrid.Visible = false;
        GvReport.Visible = false;
        gvUserReport.Visible = false;
        GV_DynamicGrid2.Visible = false;
        gvvillageschoolgrid.Visible = true;
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "Where v.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlState.SelectedIndex > 0)
        {
            conditions += "and v.StateCode = '" + ddlState.SelectedValue + "' ";

        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " and v.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";
        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions += " and v.BlockCode = '" + ddlBlock.SelectedValue + "' ";

        }
        if (ddlPanchayat.SelectedIndex > 0)
        {
            conditions += " and v.PanchayatCode = '" + ddlPanchayat.SelectedValue + "' ";
        }
        if (ddlVillage.SelectedIndex > 0)
        {
            conditions += " and v.VillageCode = '" + ddlVillage.SelectedValue + "' ";
        }

        SqlParameter[] parm = new SqlParameter[]
            {
       new SqlParameter("@condition",  conditions),
       new SqlParameter("@subject",  "0"),
        new SqlParameter("@flag",flag),

                 };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_CLT_Report_Data", parm);
        ViewState["villageschool"] = dt;
        lblTotalCount.Text = dt.Rows.Count.ToString();
        if (dt.Rows.Count > 0)
        {

            gvvillageschoolgrid.DataSource = dt;
            gvvillageschoolgrid.DataBind();
        }

        else
        {
            gvvillageschoolgrid.DataSource = null;
            gvvillageschoolgrid.DataBind();
        }
    }

    protected void gvvillageschoolgrid_pageindexchanging(object sender, GridViewPageEventArgs e)
    {
        gvvillageschoolgrid.PageIndex = e.NewPageIndex;
        if (ViewState["villageschool"] != null)
        {
            DataTable Dt = ViewState["villageschool"] as DataTable;
            gvvillageschoolgrid.DataSource = Dt;
            gvvillageschoolgrid.DataBind();
        }
    }
    #endregion
}