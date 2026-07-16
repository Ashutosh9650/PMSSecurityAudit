using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.InteropServices;

using Microsoft.Reporting.WebForms;
using System.IO;
using System.Drawing;
public partial class frmUserEntryReports : System.Web.UI.Page
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
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            ddlState.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlDistrict, null);
            ddlDistrict.SelectedIndex = 1;
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
        DGV_Report.DataSource = null;
        DGV_Report.DataBind();

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
        DGV_Report.DataSource = null;
        DGV_Report.DataBind();
        DGV_Report.Visible = false;

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

    protected void PMS_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = true;
        ViewState["1"] = 51;
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

        getreport(1);

    }
    protected void School_Click(object sender, EventArgs e)
    {
        
        DGV_Report.Visible = true;

        ViewState["1"] = 52;
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
        getreport(2);
    }
    public void getreport(Int32 Flag)
    {
        conditions = "";

        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + "  where  mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlState.SelectedIndex > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode = '" + ddlState.SelectedValue + "' ";
        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";
        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode = '" + ddlBlock.SelectedValue + "' ";
        }
        SqlParameter[] cmdParameters = new SqlParameter[]
		{
			new SqlParameter("@Cond", conditions),
            	new SqlParameter("@Flag", Flag),
			
		};
        DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptVillageReportCard]", cmdParameters);
        lblTotalCount.Text = dataTable.Rows.Count.ToString();
        ViewState["dt"] = dataTable;
        if (dataTable.Rows.Count > 0)
        {

            DGV_Report.DataSource = dataTable;
            DGV_Report.DataBind();
            Session["MobileReport"] = dataTable;

            return;
        }
        DGV_Report.DataSource = null;
        DGV_Report.DataBind();
    }
    protected void btnEnroll_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 4;
        ClearGrid();
        DGV_Report.DataSource = null;
        DGV_Report.DataBind();
        DGV_Report.Visible = false;
        GvReport.Visible = true;
        GV_DynamicGrid2.Visible = false;
        gvD2d.Visible = false;
        gvnroll.Visible = false;
        GvReport.DataSource = null;
        GvReport.DataBind();
        gvD2d.DataSource = null;
        gvUserReport.Visible = false;
        gvD2d.DataBind();
        gvUserReport.DataSource = null;
        gvUserReport.DataBind();
        LoadReportEnrollment();
    }
    protected void btnUser_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 3;
        ClearGrid();
        DGV_Report.DataSource = null;
        DGV_Report.DataBind();
        DGV_Report.Visible = false;
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
        DGV_Report.DataSource = null;
        DGV_Report.DataBind();
        DGV_Report.Visible = false;
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
        DGV_Report.DataSource = null;
        DGV_Report.DataBind();
        DGV_Report.Visible = false;
        gvD2d.Visible = true;
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
        DGV_Report.DataSource = null;
        DGV_Report.DataBind();
        DGV_Report.Visible = false;
        gvD2d.Visible = true;
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
        DGV_Report.DataSource = null;
        DGV_Report.DataBind();
        DGV_Report.Visible = false;
        GvReport.Visible = false;
        GV_DynamicGrid.Visible = false;
        GV_DynamicGrid1.Visible = false;
        GV_DynamicGrid2.Visible = false;
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
      

        if (ViewState["1"].ToString() == "3")
        {
            DataTable dt = (DataTable)Session["D2dUserDet"];
            ExporttoExcel(gvUserReport, dt, "Door to Door Date-wise");
        }
        if (ViewState["1"].ToString() == "1")
        {
            DataTable dt = (DataTable)Session["D2dUser"];
            ExporttoExcel(GvReport, dt, "Door to Door Summary");
        }
       
        if (ViewState["1"].ToString() == "4")
        {
            DataTable dt = (DataTable)Session["EnrollSummary"];
            ExporttoExcel(GvReport, dt, "Enrollment Summary");
        }
        if (ViewState["1"].ToString() == "5")
        {
           
            DataTable dt = (DataTable)Session["ENrollDetail"];
            ExporttoExcel(gvUserReport, dt, "Enrollment Date-wise");
        }
        if (ViewState["1"].ToString() == "51")
        {
           
            DataTable dt = (DataTable)Session["MobileReport"];
            ExporttoExcel(gvUserReport, dt, "VillageCard");
        }
         if (ViewState["1"].ToString() == "52")
        {
           
            DataTable dt = (DataTable)Session["MobileReport"];
            ExporttoExcel(gvUserReport, dt, "SchoolCard");
        }
       

    }
    protected void btnMainReport_Click(object sender, EventArgs e)
    {
        
            
       
    }
    protected void LnkMasterData_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 7;
        ClearGrid();
        DGV_Report.DataSource = null;
        DGV_Report.DataBind();
        DGV_Report.Visible = false;
        gvnroll.Visible = false;
        gvD2d.Visible = false;
        GV_DynamicGrid1.Visible = false;
        GV_DynamicGrid2.Visible = false;
        GvReport.Visible = false;
        gvUserReport.Visible = false;
        LoadMasterData(0);
        GV_DynamicGrid.Visible = true;
       
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
            conditions += "  mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
           
        }
        if (ddlState.SelectedIndex > 0)
        {
            conditions += "  and mst5Village.StateCode = '" + ddlState.SelectedValue + "' ";
            conditionsAll += "  mst2District.StateCode = '" + ddlState.SelectedValue + "' ";
        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " and mst5Village.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";
            conditionsAll += "  and (mst2District.DistrictCode = '" + ddlDistrict.SelectedValue + "' or mst2District.DistrictCode in(select DistrictCode from mst2District where OldDistrictCode='" + ddlDistrict.SelectedValue + "') )  ";

        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions += " and mst5Village.BlockCode = '" + ddlBlock.SelectedValue + "' ";
            conditionsAll += " and  (mst3Block.BlockCode = '" + ddlBlock.SelectedValue + "' or mst3Block.BlockCode in(select BlockCode from mst3Block where OldBlockCode='" + ddlBlock.SelectedValue + "') )  ";

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
                GvReport.DataSource = dt;
                GvReport.DataBind();
                Session["EnrollSummary"] = dt;
                lblTotalCount.Text = (dt.Rows.Count).ToString();
            }
            else
            {
                GvReport.DataSource = null;
                Session["EnrollSummary"] = null;
                GvReport.DataBind();
            }
        }


        if (ViewState["1"].ToString() == "6")
        {

            if (ddlUser.SelectedIndex > 0)
            {
                conditionsCr += " and tblEnrolment.CreateBy =  '" + ddlUser.SelectedValue + "' ";

            }
            if (txtDate.Text != "" && txtTodate.Text=="")
            {
                DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                conditionsCr += " and tblEnrolment.Createdate >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
            }
            if (txtTodate.Text != "" && txtDate.Text=="")
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
                gvnroll.DataSource = dt;
                gvnroll.DataBind();

                Session["Enroll"] = dt;
                lblTotalCount.Text = (dt.Rows.Count).ToString();
            }
            else
            {
                gvnroll.DataSource = null;
                gvnroll.DataBind();
                Session["Enroll"] = null;
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
                gvUserReport.Visible = true;
                gvUserReport.DataSource = dt;
                gvUserReport.DataBind();
                Session["ENrollDetail"] = dt;
                lblTotalCount.Text = (dt.Rows.Count).ToString();
            }
            else
            {
                Session["ENrollDetail"] = null;
                gvUserReport.DataSource = null;
                gvUserReport.DataBind();
            }
        }
    }
    public void LoadMasterData(int Flag)
    {
        string condition = string.Empty;
        if (ddlState.SelectedIndex > 0)
        {
            conditions += " where V.StateCode = '" + ddlState.SelectedValue + "' ";
            
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
        DataTable dt = objMain.LoadMasterData(conditions,Flag);
        Session["D2dUser"] = dt;

        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        GV_DynamicGrid1.DataSource = null;
        GV_DynamicGrid1.DataBind();
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();

        if (ViewState["1"].ToString() == "7")
        {
            GV_DynamicGrid.DataSource = dt;
            GV_DynamicGrid.DataBind();
            lblTotalCount.Text = (dt.Rows.Count).ToString();
        }
        else if (ViewState["1"].ToString() == "8")
        {
            GV_DynamicGrid1.DataSource = dt;
            GV_DynamicGrid1.DataBind();
            lblTotalCount.Text = (dt.Rows.Count).ToString();
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
        if (ddlState.SelectedIndex > 0)
        {
            conditions += "  mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlState.SelectedIndex > 0)
        {
            conditions += " and mst5Village.StateCode = '" + ddlState.SelectedValue + "' ";
            conditionsAll += "   mst2District.StateCode = '" + ddlState.SelectedValue + "' ";
        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " and mst5Village.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";
            conditionsAll += "  and (mst2District.DistrictCode = '" + ddlDistrict.SelectedValue + "' or mst2District.DistrictCode in(select DistrictCode from mst2District where OldDistrictCode='" + ddlDistrict.SelectedValue + "') )  ";
        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions += " and mst5Village.BlockCode = '" + ddlBlock.SelectedValue + "' ";
            conditionsAll += " and  (mst3Block.BlockCode = '" + ddlBlock.SelectedValue + "' or mst3Block.BlockCode in(select BlockCode from mst3Block where OldBlockCode='" + ddlBlock.SelectedValue + "') )  ";
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
                Session["D2dUser"] = dt;
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
            if (txtDate.Text != "" && txtTodate.Text=="")
            {
                DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                conditionsCr += " and tblDTD.Createdate >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
            }
            if (txtTodate.Text != "" && txtDate.Text=="")
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
               
                gvD2d.DataSource = dt;
                gvD2d.DataBind();
                Session["D2d"] = dt;
                lblTotalCount.Text = (dt.Rows.Count).ToString();
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

                gvD2d.DataSource = dt;
                gvD2d.DataBind();
                Session["OutD2d"] = dt;
                lblTotalCount.Text = (dt.Rows.Count).ToString();
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
                Session["D2dUserDet"] = dt;
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
            conditions = "StateCode ='" + ddlState.SelectedValue + "' ";
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
        objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "desc", ddlState, "StateName", "StateCode", "--Select--");



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
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode ='" + Session["DistrictCode"].ToString() + "'  and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else
        {
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";


        }


        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");



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
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'";
        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--All--");



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
    private void ExporttoExcel(GridView Gv, DataTable table,string FileName)
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
    public static void ToCSV(DataTable dtDataTable, string strFilePath,GridView Gv)
    {
        StreamWriter sw = new StreamWriter(strFilePath, false);
        //headers  



        int columnscount = Gv.HeaderRow.Cells.Count;


        for (int j = 0; j < columnscount; j++)
        {      //write in new column
         
            sw.Write(Gv.HeaderRow.Cells[j].Text);
            sw.Write(",");
        }
        //for (int i = 0; i < dtDataTable.Columns.Count; i++)
        //{
        //    sw.Write(dtDataTable.Columns[i]);
        //    if (i < dtDataTable.Columns.Count - 1)
        //    {
        //        sw.Write(",");
        //    }
        //}
        sw.Write(sw.NewLine);
        foreach (DataRow dr in dtDataTable.Rows)
        {
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                if (!Convert.IsDBNull(dr[i]))
                {
                    string value = dr[i].ToString();
                    if (value.Contains(','))
                    {
                        value = String.Format("\"{0}\"", value);
                        sw.Write(value);
                    }
                    else
                    {
                        sw.Write(dr[i].ToString());
                    }
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
        if (Session["D2d"] != null)
        {
            DataTable dt = Session["D2d"] as DataTable;
            gvD2d.DataSource = dt;
            gvD2d.DataBind();
        }

    }
    protected void gvnroll_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvnroll.PageIndex = e.NewPageIndex;
        if (Session["Enroll"] != null)
        {
            DataTable Dt = Session["Enroll"] as DataTable;
            gvnroll.DataSource = Dt;
            gvnroll.DataBind();
        }
    }
    protected void GV_DynamicGrid_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_DynamicGrid.PageIndex = e.NewPageIndex;
        if (Session["D2dUser"] != null)
        {
            DataTable Dt = Session["D2dUser"] as DataTable;
            GV_DynamicGrid.DataSource = Dt;
            GV_DynamicGrid.DataBind();
        }
    }
    protected void GV_DynamicGrid1_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_DynamicGrid1.PageIndex = e.NewPageIndex;
        if (Session["D2dUser"] != null)
        {
            DataTable Dt = Session["D2dUser"] as DataTable;
            GV_DynamicGrid1.DataSource = Dt;
            GV_DynamicGrid1.DataBind();
        }
    }
    protected void GV_DynamicGrid2_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_DynamicGrid2.PageIndex = e.NewPageIndex;
        if (Session["D2dUser"] != null)
        {
            DataTable Dt = Session["D2dUser"] as DataTable;
            GV_DynamicGrid2.DataSource = Dt;
            GV_DynamicGrid2.DataBind();
        }
    }
#region Abhimanyu

    protected void btnCSV_Click(object sender, EventArgs e)
    {
        if (ViewState["1"].ToString() == "2")
        {
            DataTable dt = (DataTable)Session["D2d"];
            ExporttoCSV(gvD2d, dt);
        }

        if (ViewState["1"].ToString() == "3")
        {
            DataTable dt = (DataTable)Session["D2dUserDet"];
            ExporttoCSV(gvUserReport, dt);
        }
        if (ViewState["1"].ToString() == "1")
        {
            DataTable dt = (DataTable)Session["D2dUser"];
            ExporttoCSV(GvReport, dt);
        }
        if (ViewState["1"].ToString() == "7")
        {
            DataTable dt = (DataTable)Session["D2dUser"];
            ExporttoCSV(GV_DynamicGrid, dt);
        }
        if (ViewState["1"].ToString() == "6")
        {
            DataTable dt = (DataTable)Session["Enroll"];
            ExporttoCSV(gvnroll, dt);
        }
        if (ViewState["1"].ToString() == "4")
        {
            DataTable dt = (DataTable)Session["EnrollSummary"];
            ExporttoCSV(GvReport, dt);
        }
        if (ViewState["1"].ToString() == "5")
        {
           
            DataTable dt = (DataTable)Session["ENrollDetail"];
            ExporttoCSV(gvUserReport, dt);
        }
        if (ViewState["1"].ToString() == "8")
        {
            DataTable dt = (DataTable)Session["D2dUser"];
            ExporttoCSV(GV_DynamicGrid1, dt);
        }
        if (ViewState["1"].ToString() == "9")
        {
            DataTable dt = (DataTable)Session["D2dUser"];
            ExporttoCSV(GV_DynamicGrid2, dt);
        }

        if (ViewState["1"].ToString() == "10")
        {
            DataTable dt = (DataTable)Session["OutD2d"];
            ExporttoCSV(gvD2d, dt);
        }
    }


    private void ExporttoCSV(GridView Gv, DataTable table)
    {
        if (table != null)
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
        
    }

#endregion
}