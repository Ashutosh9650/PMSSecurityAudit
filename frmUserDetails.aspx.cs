using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class frmUserDetails : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    string conditions = "";
    string flag = "";
    Password objPass = new Password();
    public DataTable dtUserDeatils;
    DataTableMaskingHelper dataTableMaskingHelper = new DataTableMaskingHelper();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblTotalCount.Text = "";
        if (Convert.ToString(Session["username"]) != "")
        {

            if (!IsPostBack)
            {
                LoadYear();
                LoadUserLeavel();
                ViewState["1"] = "ss";

                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        else
        {
            Response.Redirect("Login.aspx", false);
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
    public void LoadUser()
    {
        conditions = "";

        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlState = "";

        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlState += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlState.Length > 0)
        {
            ddlState = ddlState.Substring(0, ddlState.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }

        conditions = " UserLevel in(24,19) ";

        if (ddlState.Length > 0)
        {
            conditions += "and StateCode in(" + ddlState + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions += " and BlockCode in(" + ddlBlock + ")  ";

        }





        objComman.BindDLL("[PMS].[dbo].MstUser", "UserId as UserId,[FristName]+' ('+ UserName +')' as [UserName] ", conditions, "UserName", "", ddlUser, "UserName", "UserId", "Select");
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
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
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
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
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
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
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
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
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
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
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
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
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
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
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
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
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
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
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
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
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
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
    }
    protected void btnLearningBaselineIO_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 15;
        ClearGrid();
        gvnroll.Visible = false;
        gvD2d.Visible = false;
        GV_DynamicGrid1.Visible = false;
        GV_DynamicGrid2.Visible = false;
        GvReport.Visible = false;
        gvUserReport.Visible = false;
        getreportIO(2);
        GV_DynamicGrid.Visible = true;
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
    }
    protected void btnLearningBaselineIOEnd_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 555;
        ClearGrid();
        gvnroll.Visible = false;
        gvD2d.Visible = false;
        GV_DynamicGrid1.Visible = false;
        GV_DynamicGrid2.Visible = false;
        GvReport.Visible = false;
        gvUserReport.Visible = false;
        getreportIO(5);
        GV_DynamicGrid.Visible = true;
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
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
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
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
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
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
        LoadMasterData(9);
        GV_DynamicGrid2.Visible = true;
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
        lnkCSV.Visible = true;
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
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
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
        RetentionIndividual(2);
        GV_DynamicGrid1.Visible = true;
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
    }
    protected void btnReo_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 611;
        ClearGrid();
        gvnroll.Visible = false;
        gvD2d.Visible = false;
        GvReport.Visible = false;
        gvUserReport.Visible = false;
        GV_DynamicGrid.Visible = false;
        GV_DynamicGrid2.Visible = false;
        ReEnrollment(1);
        GV_DynamicGrid1.Visible = true;
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
    }
    protected void btnReoe_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 612;
        ClearGrid();
        gvnroll.Visible = false;
        gvD2d.Visible = false;
        GvReport.Visible = false;
        gvUserReport.Visible = false;
        GV_DynamicGrid.Visible = false;
        GV_DynamicGrid2.Visible = false;
        ReEnrollment(1);
        GV_DynamicGrid1.Visible = true;
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
    }
    public void ReEnrollment(int Flag)
    {

        string condition = string.Empty;
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlState = "";

        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlState += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlState.Length > 0)
        {
            ddlState = ddlState.Substring(0, ddlState.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlState.Length > 0)
        {
            conditions += "and mst5Village.StateCode in(" + ddlState + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions += " and mst5Village.BlockCode in(" + ddlBlock + ")  ";

        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ")  ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }




        DataTable dt = objMain.ReenrollmentData(conditions, Flag.ToString());
        ViewState["Reenrollment"] = dt;

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
    protected void Retention_Click(object sender, EventArgs e)
    {
        conditions = "";
        gvRetaion.Visible = true;
        ViewState["1"] = 320;
        ClearGrid();
        gvnroll.Visible = false;
        gvD2d.Visible = false;
        GvReport.Visible = false;
        gvUserReport.Visible = false;
        GV_DynamicGrid.Visible = false;
        GV_DynamicGrid2.Visible = false;

        GV_DynamicGrid1.Visible = true;

        LinkButton1.Visible = true;
        string subject = "";

        ViewState["Button"] = "LoadRetion";

        if (ddlYear.SelectedIndex > 0)
        {
            conditions += " where  mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlState = "";

        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlState += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlState.Length > 0)
        {
            ddlState = ddlState.Substring(0, ddlState.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlState.Length > 0)
        {
            conditions += "and mst5Village.StateCode in(" + ddlState + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions += " and mst5Village.BlockCode in(" + ddlBlock + ")  ";

        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ")  ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }




        DataTable dt = objMain.rptRetention(conditions);
        gvRetaion.DataSource = dt;
        gvRetaion.DataBind();
    }
    protected void gvRetaion_RowCreated(object sender, GridViewRowEventArgs e)
    {

        //        Baseline


        #region Basline


        if (e.Row.RowType == DataControlRowType.Header)
        {


            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow.CssClass = "gridnewheadercss";
            TableCell HeaderCell;

            HeaderCell = new TableCell();
            HeaderCell.Text = "Dist Profile";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 10;
            HeaderCell.ColumnSpan = 10;
            HeaderCell.CssClass = "gridnewheadercss";
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "Enrolment Boys ";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 9;

            HeaderCell.CssClass = "gridnewheadercss";
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "Enrolment Girl ";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 9;

            HeaderCell.CssClass = "gridnewheadercss";
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Appear in Final Exam Boys";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 9;

            HeaderCell.CssClass = "gridnewheadercss";
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "Appear in Final Exam Girls";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 9;

            HeaderCell.CssClass = "gridnewheadercss";
            HeaderGridRow.Cells.Add(HeaderCell);



            HeaderCell = new TableCell();
            HeaderCell.Text = "No. of Newly Enrolled Girls";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 9;

            HeaderCell.CssClass = "gridnewheadercss";
            HeaderGridRow.Cells.Add(HeaderCell);




            HeaderCell = new TableCell();
            HeaderCell.Text = "No. of Girls appeared in Final Exam from Newly enrolled girls";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 9;

            HeaderCell.CssClass = "gridnewheadercss";
            HeaderGridRow.Cells.Add(HeaderCell);
            ////HeaderCell = new TableCell();
            ////HeaderCell.Text = "ANALYSIS";
            ////HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            ////if (ddlDistrict.SelectedIndex <= 0)
            ////{
            ////    HeaderCell.ColumnSpan = 3;
            ////}
            ////if (ddlDistrict.SelectedIndex > 0)
            ////{
            ////    HeaderCell.ColumnSpan = 3;
            ////}
            ////  HeaderCell.ColumnSpan = 3;
            //HeaderCell.CssClass = "gridnewheadercss";
            //HeaderGridRow.Cells.Add(HeaderCell);


            //
            gvRetaion.Controls[0].Controls.AddAt(0, HeaderGridRow);




        }
        #endregion
    }
    public void LoadRetion()
    {



        if (gvRetaion.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=EG_Retention_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvRetaion.AllowPaging = false;
                Retention_Click(LinkButton1, null);
                gvRetaion.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvRetaion.HeaderRow.Cells)
                {
                    cell.BackColor = gvRetaion.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvRetaion.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvRetaion.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvRetaion.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvRetaion.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //if (Page != null)
        //{
        //    Page.VerifyRenderingInServerForm(this);
        //}

        /* Verifies that the control is rendered */
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (ViewState["1"].ToString() == "320")
        {
            LoadRetion();
        }
        if (ViewState["1"].ToString() == "2")
        {
            DataTable dt = (DataTable)ViewState["D2dAllData"];
            ExporttoExcel(gvD2d, dt, "D2DRawData");
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
            ExporttoExcel(GV_DynamicGrid, dt, "LocationMaster");
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
            ExportToCSVFile(dt, "UserMaster");
        }

        if (ViewState["1"].ToString() == "10")
        {
            DataTable dt = (DataTable)ViewState["OutD2d"];
            ExporttoExcel(gvD2d, dt, "OutOfDoorToDoor");
        }

        if (ViewState["1"].ToString() == "11")
        {
            DataTable dt = (DataTable)ViewState["VillageLatLog"];
            ExporttoExcel(gvvillageschoolgrid, dt, "VillageTaggingReport");
        }

        if (ViewState["1"].ToString() == "15")
        {

            DataTable dt = (DataTable)Session["ExReport"];
            ExporttoExcel(GvReport, dt, "EmployeeExceptions");
        }
        if (ViewState["1"].ToString() == "555")
        {
            DataTable dt = (DataTable)ViewState["LearningBaseline"];
            ExporttoExcel(GV_DynamicGrid, dt, "EndLind");
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

    public void getreportIO(Int32 Flag)
    {
        conditions = "";
        string subject = "";

        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }


        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }


        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "  where v.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions += "and  v.StateCode in(" + ddlStatecode + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and v.DistrictCode in(" + ddlDistrict + ") ";

        }
        if (ddlBlock.Length > 0)
        {
            conditions += " and v.BlockCode in(" + ddlBlock + ") ";

        }

        //if (ddlsubject.SelectedIndex > 0)
        //{
        //    subject = ddlsubject.SelectedItem.Text;
        //}
        SqlParameter[] parm = new SqlParameter[]
            {
       new SqlParameter("@condition",  conditions),
       new SqlParameter("@subject",  subject),
        new SqlParameter("@flag",Flag),

                 };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Get_CLT_Report_DataIO]", parm);
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
            if (dt.Rows.Count > 2000)
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

    public void getreport2()
    {
        conditions = "";
        string subject = ddlYear.SelectedValue;

        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }


        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }


        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "  where v.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions += "and  v.StateCode in(" + ddlStatecode + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and v.DistrictCode in(" + ddlDistrict + ") ";

        }
        if (ddlBlock.Length > 0)
        {
            conditions += " and v.BlockCode in(" + ddlBlock + ") ";

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

        dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Student Name", "Class");

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

    public void LoadReportEnrollment()
    {

        conditions = "";
        string conditionsCr = "";
        string conditionsmo = "";
        string conditionsDe = "";
        string conditionsAll = "";
        lblTotalCount.Text = "";


        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }

        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        lblTotalCount.Text = "";
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions += "  and mst5Village.StateCode in(" + ddlStatecode + ") ";
            conditionsAll += "  StateCode in(" + ddlStatecode + ")";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst5Village.DistrictCode in( " + ddlDistrict + ") ";
            conditionsAll += "  and DistCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            if (Convert.ToInt32(rblBlockType.SelectedValue) == 1)
            {
                conditions += " and mst5Village.BlockCode in(" + ddlBlock + ") ";
                conditionsAll += " and  BlockCode in(" + ddlBlock + ") ";
            }
            if (Convert.ToInt32(rblBlockType.SelectedValue) == 2)
            {
                conditions += " and mst5Village.MainBlockCode in(" + ddlBlock + ") ";
                conditionsAll += " and  BlockCode in(" + ddlBlock + ") ";
            }
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.Villagecode in( " + ddlVillage + ") ";

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

            DataTable dt = objMain.ReportEnrollDeatilsNew(FristCon);
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

    public void LoadSIP(int Flag)
    {
        string condition = string.Empty;



        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        lblTotalCount.Text = "";


        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "    where V.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and V.StateCode in(" + ddlStatecode + ") ";

        }

        if (ddlDistrict.Length > 0)
        {
            conditions += " and V.DistrictCode in(" + ddlDistrict + ") ";

        }

        if (ddlBlock.Length > 0)
        {
            if (Convert.ToInt32(rblBlockType.SelectedValue) == 1)
            {
                conditions += " and V.BlockCode in(" + ddlBlock + ") ";

            }
            if (Convert.ToInt32(rblBlockType.SelectedValue) == 2)
            {
                conditions += " and V.MainBlockCode in(" + ddlBlock + ") ";

            }
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and V.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and V.VillageCode in(" + ddlVillage + ") ";
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
        conditions = "";
        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        lblTotalCount.Text = "";


        string condition = string.Empty;
        if (Flag == 9)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "   and  mst2District.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }

            if (ddlStatecode.Length > 0)
            {
                conditions += "  and  V.StateCode in(" + ddlStatecode + ") ";

            }
        }
        else
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "    where V.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (ddlStatecode.Length > 0)
            {
                conditions += " and V.StateCode in(" + ddlStatecode + ") ";

            }
        }

        if (ddlDistrict.Length > 0)
        {
            conditions += " and V.DistrictCode in(" + ddlDistrict + ") ";

        }

        if (ddlBlock.Length > 0)
        {
            if (Convert.ToInt32(rblBlockType.SelectedValue) == 1)
            {
                conditions += " and V.BlockCode in(" + ddlBlock + ") ";

            }
            if (Convert.ToInt32(rblBlockType.SelectedValue) == 2)
            {
                conditions += " and V.MainBlockCode in(" + ddlBlock + ") ";

            }
        }
        //if (ddlPhan.Length > 0)
        //{
        //    conditions += " and V.PanchayatCode in(" + ddlPhan + ") ";
        //}
        //if (ddlVillage.Length > 0)
        //{
        //    conditions += " and V.VillageCode in(" + ddlVillage + ") ";
        //}

        if (Flag == 3)
        {
            string Year = ddlYear.SelectedItem.Text;
            string[] Year1 = Year.Split('-');
            if (ddlYear.SelectedItem.Text == "2016-2017")
            {
                if (ddlYear.SelectedIndex > 0)
                {
                    conditions += "    And FromDate <= '" + Year1[1] + "-03-31'";
                }
            }
            else
            {
                if (ddlYear.SelectedIndex > 0)
                {
                    conditions += "    And FromDate >= '" + Year1[0] + "-04-01' and ToDate<='" + Year1[1] + "-03-31'";


                }
            }
        }
        DataTable dt = objMain.LoadMasterDataNew(conditions, Flag);

        if (Flag == 1)
            dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "TB Name", "Father Name", "Mother Name", "Date of Birth", "Contact No", "Alternate Mobile Number", "Name of Employee");
        else if (Flag == 0)
            dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Teacher Name", "Teacher Mobile Number", "School Donor Name");
        else if (Flag == 3)
            dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "TBName");
        else if (Flag == 9)
            dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Employee Name", "Email", "Contact No");

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
        else if (ViewState["1"].ToString() == "9")
        {
            lblTotalCount.Text = (dt.Rows.Count).ToString();

            btnCSV_Click(LinkButton8, null);


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
        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        lblTotalCount.Text = "";
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions += "  and mst5Village.StateCode in(" + ddlStatecode + ") ";
            conditionsAll += "  StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst5Village.DistrictCode in( " + ddlDistrict + ") ";
            conditionsAll += "  and DistCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            if (Convert.ToInt32(rblBlockType.SelectedValue) == 1)
            {
                conditions += " and mst5Village.BlockCode in(" + ddlBlock + ") ";
                conditionsAll += " and  BlockCode in(" + ddlBlock + ") ";
            }
            if (Convert.ToInt32(rblBlockType.SelectedValue) == 2)
            {
                conditions += " and mst5Village.MainBlockCode in(" + ddlBlock + ") ";
                conditionsAll += " and  BlockCode in(" + ddlBlock + ") ";
            }
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }

        //if (ViewState["1"].ToString() == "1")
        //{
        //    GvReport.Visible = true;
        //    if (ddlUser.SelectedIndex > 0)
        //    {
        //        conditionsCr += " and CreateBy =  '" + ddlUser.SelectedValue + "' ";
        //        conditionsmo += " and ModifyBy = '" + ddlUser.SelectedValue + "' ";
        //        conditionsDe += " and DeleteBy = '" + ddlUser.SelectedValue + "' ";
        //    }
        //    if (txtDate.Text != "")
        //    {
        //        DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
        //        conditionsCr += " and Createdate >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
        //        conditionsmo += " and ModifyDate >= '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
        //        conditionsDe += " and DeletedDate >= '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
        //    }
        //    if (txtTodate.Text != "")
        //    {
        //        DateTime Todate = Convert.ToDateTime(txtTodate.Text);
        //        conditionsCr += " and Createdate <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";
        //        conditionsmo += " and ModifyDate <= '" + Todate.ToString("yyyy-MM-dd") + "' ";
        //        conditionsDe += " and DeletedDate <= '" + Todate.ToString("yyyy-MM-dd") + "' ";
        //    }
        //    if (txtDate.Text != "" && txtTodate.Text != "")
        //    {
        //        DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
        //        DateTime Todate = Convert.ToDateTime(txtTodate.Text);
        //        conditionsCr += " and Createdate BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
        //        conditionsmo += " and ModifyDate BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "'  and '" + Todate.ToString("yyyy-MM-dd") + "' ";
        //        conditionsDe += " and DeletedDate BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "'  and '" + Todate.ToString("yyyy-MM-dd") + "' ";
        //    }
        //    if (ddlUser.SelectedIndex > 0)
        //    {
        //        conditionsAll += " and UserName1 =  '" + ddlUser.SelectedValue + "' ";

        //    }
        //    string FristCon = conditions + conditionsCr;
        //    string Second = conditions + conditionsmo;
        //    string Third = conditions + conditionsDe;
        //    DataTable dt = objMain.Report(FristCon, Second, Third, conditionsAll);
        //    if (dt.Rows.Count > 0)
        //    {
        //        GvReport.DataSource = dt;
        //        GvReport.DataBind();
        //        ViewState["D2dUser"] = dt;
        //        lblTotalCount.Text = (dt.Rows.Count).ToString();
        //    }
        //    else
        //    {
        //        GvReport.DataSource = null;
        //        GvReport.DataBind();
        //        lblTotalCount.Text = "";
        //    }
        //}


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

            DataTable dt = objMain.ReportD2dAllReport(FristCon, 1);

            if (dt.Rows.Count > 0)
            {
                dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Mauhalla", "House", "ChildName", "FathersName", "Mother Name", "Grandfather Name", "Contact Number", "Date of Birth", "Guardian Name", "Surveyor Name", "Latitude", "Longitude", "Age");

                gvD2d.Visible = true;
                ViewState["D2dAllData"] = dt;
                lblTotalCount.Text = (dt.Rows.Count).ToString();
                if (dt.Rows.Count > 10000)
                {
                    ExportToCSVFile(dt, "D2DRawData");
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

            DataTable dt = objMain.ReportD2dAllReport(FristCon, 2);
            if (dt.Rows.Count > 0)
            {
                dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Mauhalla", "House", "ChildName", "FathersName", "Mother Name", "Grandfather Name", "Contact Number", "Date of Birth", "Age");

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



        //////if (ViewState["1"].ToString() == "3")
        //////{

        //////    if (ddlUser.SelectedIndex > 0)
        //////    {
        //////        conditionsCr += " and CreateBy =  '" + ddlUser.SelectedValue + "' ";

        //////    }
        //////    if (txtDate.Text != "")
        //////    {
        //////        DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
        //////        conditionsCr += " and Createdate >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
        //////    }
        //////    if (txtTodate.Text != "")
        //////    {
        //////        DateTime Todate = Convert.ToDateTime(txtTodate.Text);
        //////        conditionsCr += " and Createdate <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";

        //////    }
        //////    if (txtDate.Text != "" && txtTodate.Text != "")
        //////    {
        //////        DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
        //////        DateTime Todate = Convert.ToDateTime(txtTodate.Text);
        //////        conditionsCr += " and Createdate BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
        //////    }

        //////    string FristCon = conditions + conditionsCr;

        //////    DataTable dt = objMain.ReportUserEntery(FristCon);
        //////    if (dt.Rows.Count > 0)
        //////    {
        //////        gvUserReport.Visible = true;
        //////        gvUserReport.DataSource = dt;
        //////        gvUserReport.DataBind();
        //////        ViewState["D2dUserDet"] = dt;
        //////        lblTotalCount.Text = (dt.Rows.Count).ToString();
        //////    }
        //////    else
        //////    {
        //////        gvUserReport.DataSource = null;
        //////        gvUserReport.DataBind();
        //////        lblTotalCount.Text = "";
        //////    }
        //////}
    }
    public void LoadIneligable(int Flag)
    {
        string condition = string.Empty;
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        lblTotalCount.Text = "";
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions += "  and mst5Village.StateCode in(" + ddlStatecode + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst5Village.DistrictCode in( " + ddlDistrict + ") ";

        }
        if (ddlBlock.Length > 0)
        {
            if (Convert.ToInt32(rblBlockType.SelectedValue) == 1)
            {
                conditions += " and mst5Village.BlockCode in(" + ddlBlock + ") ";

            }
            if (Convert.ToInt32(rblBlockType.SelectedValue) == 2)
            {
                conditions += " and mst5Village.MainBlockCode in(" + ddlBlock + ") ";

            }
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
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
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "  and I_Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        DataTable dt = objMain.ReportD2dAllReport(conditions, 3);

        dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "HHNo", "ChildName", "FatherName", "Age", "Migration_Place", "Permanent_Add");

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
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (ddlDistrict.SelectedIndex > 0)
        //    {
        //        GvReport.Columns[2].Visible = true;
        //    }
        //    else
        //    {
        //        GvReport.Columns[2].Visible = false;
        //    }
        //}
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

            string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State   order by StateName   ";
            DataTable dtState = objMain.LoadData(strQry1);
            ChkState.DataSource = dtState;
            ChkState.DataTextField = "StateName";
            ChkState.DataValueField = "StateCode";
            ChkState.DataBind();

            ChkState.Enabled = true;
            chkDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "UserName='" + Session["username"].ToString() + "' ";
            string strQry1 = "  SELECT distinct mst1State.StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode inner join mst1State on mst1State.StateCode=mst2District.StateCode where   " + conditions + "  order by StateName   ";
            DataTable dtState = objMain.LoadData(strQry1);
            ChkState.DataSource = dtState;
            ChkState.DataTextField = "StateName";
            ChkState.DataValueField = "StateCode";
            ChkState.DataBind();
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            foreach (ListItem item in ChkState.Items)
            {

                item.Selected = true;

            }

            ChkState.Enabled = true;
            chkDistrict.Enabled = true;
        }
        else
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State  where   " + conditions + "  order by StateName   ";
            DataTable dtState = objMain.LoadData(strQry1);
            ChkState.DataSource = dtState;
            ChkState.DataTextField = "StateName";
            ChkState.DataValueField = "StateCode";
            ChkState.DataBind();
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            foreach (ListItem item in ChkState.Items)
            {

                item.Selected = true;

            }
            // ChkState.SelectedIndex = 1;
            ChkState.Enabled = false;
            chkDistrict.Enabled = false;
        }


        if (Session["user_level_Role"].ToString() == "1")
        {
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            string ddlState = "";

            foreach (ListItem item in ChkState.Items)
            {
                if (item.Selected)
                {

                    ddlState += "'" + item.Value + "'" + ",";


                }
            }
            conditions = "";
            //  conditions = "StateCode in(" + ddlState + ") and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
            conditions = "UserName='" + Session["username"].ToString() + "' ";
            string strQry1 = "  SELECT distinct mst2District.DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode  where   " + conditions + " and Fyear='" + ddlYear.SelectedItem.Text + "'  order by DistrictName   ";


            // string strQry1 = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where " + conditions + "  order by DistrictName   ";


            DataTable dtDistrict = objMain.LoadData(strQry1);
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            chkDistrict.DataSource = dtDistrict;
            chkDistrict.DataTextField = "DistrictName";
            chkDistrict.DataValueField = "DistrictCode";
            chkDistrict.DataBind();


            foreach (ListItem item in chkDistrict.Items)
            {

                item.Selected = true;
                break;
            }
            //ddlDistrict.SelectedIndex = 0;

            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }

        else
        {

            string ddlState = "";

            foreach (ListItem item in ChkState.Items)
            {
                if (item.Selected)
                {

                    ddlState += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlState.Length > 0)
            {
                ddlState = ddlState.Substring(0, ddlState.LastIndexOf(","));
            }
            conditions = "";
            conditions = "StateCode in(" + ddlState + ") and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and Fyear= '" + ddlYear.SelectedItem.Text + "' ";
            //  objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");
            string strQry1 = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where " + conditions + "  order by DistrictName   ";
            DataTable dtDistrict = objMain.LoadData(strQry1);
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            chkDistrict.DataSource = dtDistrict;
            chkDistrict.DataTextField = "DistrictName";
            chkDistrict.DataValueField = "DistrictCode";
            chkDistrict.DataBind();
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


            //ddlDistrict.SelectedIndex = 1;
            foreach (ListItem item in chkDistrict.Items)
            {

                item.Selected = true;

            }
            ddlDistrict_SelectedIndexChanged(chkDistrict, null);
            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }





    }
    public void FillCBState()
    {
        conditions = "";
        // objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");


        //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
        string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State   order by StateName   ";
        DataTable dtState = objMain.LoadData(strQry1);
        ChkState.DataSource = dtState;
        ChkState.DataTextField = "StateName";
        ChkState.DataValueField = "StateCode";
        ChkState.DataBind();

    }
    public void FillCBDist()
    {
        string ddlState = "";
        DataTable dtDistrict = null;
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlState += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlState.Length > 0)
        {
            ddlState = ddlState.Substring(0, ddlState.LastIndexOf(","));
        }
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "StateCode in(" + ddlState + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = " mst2District.StateCode in(" + ddlState + ") and UserName='" + Session["username"].ToString() + "' ";
        }
        else
        {
            conditions = "StateCode  in(" + ddlState + ") and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";


        }
        if (Session["user_level_Role"].ToString() == "2")
        {
            //if (ddlYear.SelectedValue.ToString() == "2016")
            //{

            //    string strQry1 = "  SELECT distinct mst2District.DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where EGDistrictCode in(     SELECT distinct mst2District.EGDistrictCode  FROM MstusermultipleDist     where   " + conditions + " )  and Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName   ";
            //    dtDistrict = objMain.LoadData(strQry1);
            //}

            //if (ddlYear.SelectedValue.ToString() == "2017")
            //{

            //    string strQry1 = "  SELECT distinct mst2District.DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where EGDistrictCode in(     SELECT distinct mst2District.EGDistrictCode  FROM MstusermultipleDist     where   " + conditions + " )  and Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName   ";
            //    dtDistrict = objMain.LoadData(strQry1);
            //}
            //if (ddlYear.SelectedValue.ToString() == "2018")
            //{

            //    string strQry1 = "  SELECT distinct mst2District.DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist   inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode  where   " + conditions + " and Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName   ";
            //    dtDistrict = objMain.LoadData(strQry1);
            //}


            string strQry1 = "       sELECT distinct mst2District.DistrictCode as DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist  ";
            strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions + "  and  Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName ";
            dtDistrict = objMain.LoadData(strQry1);
        }
        else
        {
            string strQry = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where " + conditions + "  order by DistrictName   ";
            dtDistrict = objMain.LoadData(strQry);
        }

        // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        chkDistrict.DataSource = dtDistrict;
        chkDistrict.DataTextField = "DistrictName";
        chkDistrict.DataValueField = "DistrictCode";
        chkDistrict.DataBind();


        ddlPanchayat.Items.Clear();
        chkVillage.Items.Clear();
    }


    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            if (Session["user_level_Role"].ToString() == "3" || Session["user_level_Role"].ToString() == "4")
            {

            }
            else
            {
                foreach (ListItem item in ChkState.Items)
                {

                    item.Selected = false;

                }
            }

            if (Session["user_level_Role"].ToString() == "2")
            {

                conditions = "UserName='" + Session["username"].ToString() + "' ";
                string strQry1 = "  SELECT distinct mst1State.StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode inner join mst1State on mst1State.StateCode=mst2District.StateCode where   " + conditions + "  order by StateName   ";
                DataTable dtState = objMain.LoadData(strQry1);
                ChkState.DataSource = dtState;
                ChkState.DataTextField = "StateName";
                ChkState.DataValueField = "StateCode";
                ChkState.DataBind();
                foreach (ListItem item in ChkState.Items)
                {

                    item.Selected = true;

                }
            }

            ddlState_SelectedIndexChanged(chkDistrict, null);
            if (Session["user_level_Role"].ToString() == "3" || Session["user_level_Role"].ToString() == "4")
            {
                if (chkDistrict.Items.Count > 0)
                {
                    foreach (ListItem item in chkDistrict.Items)
                    {

                        item.Selected = true;

                    }
                }
            }

            ddlDistrict_SelectedIndexChanged(chkDistrict, null);

            ddlPanchayat.Items.Clear();
            chkVillage.Items.Clear();
        }
        else
        {
            foreach (ListItem item in ChkState.Items)
            {

                item.Selected = false;

            }
            chkDistrict.Items.Clear();
            chkBlock.Items.Clear();
            ddlPanchayat.Items.Clear();
            chkVillage.Items.Clear();
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBBock();
        LoadUser();
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBCluster();
        LoadUser();
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillage();
    }
    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadReport();
    }
    protected void rblBlockType_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlPanchayat.Items.Clear();
        chkVillage.Items.Clear();
        ddlDistrict_SelectedIndexChanged(chkBlock, null);
    }
    public void FillCBBock()
    {
        conditions = "";
        string ddlDistrict = "";

        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }

        if (Session["user_level_Role"].ToString() == "2")
        {
            if (ddlDistrict.Length > 0)
            {
            }
            else
            {
                if (chkDistrict.Items.Count > 0)
                {
                    foreach (ListItem item in chkDistrict.Items)
                    {
                        ddlDistrict += "'" + item.Value + "'" + ",";
                        item.Selected = true;
                        break;
                    }
                    if (ddlDistrict.Length > 0)
                    {
                        ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
                    }
                }
            }


        }
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "DistrictCode in(" + ddlDistrict + ") ";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "DistrictCode in(" + ddlDistrict + ")  ";
        }
        else if (Session["user_level_Role"].ToString() == "4")
        {
            conditions = "DistrictCode in(" + ddlDistrict + ")   and BlockCode in( " + Session["BlockCode"].ToString() + " ) and FYear ='" + ddlYear.SelectedItem.Text + "' ";
        }
        else
        {
            conditions = "DistrictCode in(" + ddlDistrict + ")  ";
        }
        //     objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");
        if (Convert.ToInt32(rblBlockType.SelectedValue) == 1)
        {
            string strQry = "  SELECT BlockCode, dbo.TitleCase(upper(BlockName))  as BlockName FROM mst3Block where " + conditions + "  order by BlockName   ";
            DataTable dtDistrict = objMain.LoadData(strQry);
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            chkBlock.DataSource = dtDistrict;
            chkBlock.DataTextField = "BlockName";
            chkBlock.DataValueField = "BlockCode";
            chkBlock.DataBind();
        }
        if (Convert.ToInt32(rblBlockType.SelectedValue) == 2)
        {
            string strQry = "  SELECT distinct MainBlockCode as BlockCode, dbo.TitleCase(upper(MainBlockName))  as BlockName FROM mst5Village where " + conditions + "  order by BlockName   ";
            DataTable dtDistrict = objMain.LoadData(strQry);
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            chkBlock.DataSource = dtDistrict;
            chkBlock.DataTextField = "BlockName";
            chkBlock.DataValueField = "BlockCode";
            chkBlock.DataBind();
        }


        ddlPanchayat.Items.Clear();
        chkVillage.Items.Clear();

    }
    public void FillCBCluster()
    {

        string ddlBlock = "";
        string ddlDistrict = "";

        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }
        conditions = "";
        DataTable dtDistrict = null;
        if (Convert.ToInt32(rblBlockType.SelectedValue) == 1)
        {
            conditions = "DistrictCode in(" + ddlDistrict + ")  and BlockCode in(" + ddlBlock + ")";
            string strQry = "  SELECT PanchayatCode, dbo.TitleCase(upper(PanchayatName))  as PanchayatName FROM mstPanchayat where " + conditions + "  order by PanchayatName   ";
            dtDistrict = objMain.LoadData(strQry);
        }
        if (Convert.ToInt32(rblBlockType.SelectedValue) == 2)
        {
            conditions = "mst5Village.DistrictCode in(" + ddlDistrict + ")  and mst5Village.MainBlockCode in(" + ddlBlock + ")";
            string strQry = "  SELECT distinct mst5Village.PanchayatCode as PanchayatCode, dbo.TitleCase(upper(PanchayatName))  as PanchayatName from mst5Village   inner join mstPanchayat on mstPanchayat.PanchayatCode=mst5Village.PanchayatCode and mst5Village.BlockCode=mstPanchayat.BlockCode where " + conditions + "  order by PanchayatName   ";
            dtDistrict = objMain.LoadData(strQry);
        }



        // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        ddlPanchayat.DataSource = dtDistrict;
        ddlPanchayat.DataTextField = "PanchayatName";
        ddlPanchayat.DataValueField = "PanchayatCode";
        ddlPanchayat.DataBind();

        // objComman.BindDLL("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "--All--");


        chkVillage.Items.Clear();

    }
    public void FillCVillage()
    {
        conditions = "";
        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";

        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        conditions = "";
        if (Convert.ToInt32(rblBlockType.SelectedValue) == 1)
        {
            conditions = "DistrictCode in(" + ddlDistrict + ")  and BlockCode in(" + ddlBlock + ") and  PanchayatCode in(" + ddlPhan + ")";
        }
        if (Convert.ToInt32(rblBlockType.SelectedValue) == 2)
        {
            conditions = "DistrictCode in(" + ddlDistrict + ")  and MainBlockCode in(" + ddlBlock + ") and  PanchayatCode in(" + ddlPhan + ")";

        }
        //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        //objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--All-");

        string strQry = "  SELECT VillageCode, dbo.TitleCase(upper(VillageName))  as VillageName FROM mst5Village where " + conditions + "  order by VillageName   ";
        DataTable dtDistrict = objMain.LoadData(strQry);

        chkVillage.DataSource = dtDistrict;
        chkVillage.DataTextField = "VillageName";
        chkVillage.DataValueField = "VillageCode";
        chkVillage.DataBind();


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
                string includeSubFolders = "File";
                string path1 = Fullfilename;
                string foldername = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + path1 + "");
                string datafolder = path1.Substring(0, path1.Length - 4);
                //  string[] file = Directory.GetFiles(foldername);

                string fullPath = Request.MapPath("~/DataBackup/" + datafolder + "" + ".zip");
                using (ZipFile zip = new ZipFile())
                {

                    //zip.AddFile(foldername);
                    //string zipName = String.Format("{0}.zip", datafolder);
                    //zip.AddSelectedFiles("*.*", foldername);
                    //zip.Save(Server.MapPath(Comman.GetImagePath("DataBackupPath")) +"/" ) + zipName);

                    zip.AddFile(foldername, "");
                    //    zip.AddFiles(file, foldername);
                    zip.Save(Server.MapPath(Comman.GetImagePath("DataBackupPath")) + "/" + datafolder + "" + ".zip");
                }



                HttpResponse Response = HttpContext.Current.Response; Response.Clear(); Response.ClearHeaders(); Response.Charset = "UTF-8";
                fs = File.Open(fullPath, FileMode.Open);
                byte[] bytBytes = new byte[(fs.Length)];
                fs.Read(bytBytes, 0, (int)fs.Length);
                fs.Close();
                Response.AddHeader("Content-disposition", "attachment; filename=" + datafolder + "" + ".zip");
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
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.End();
            }

            catch (System.Exception ex)
            {
                //  Server.Transfer("default.aspx", false);
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
            ExportToCSVFile(dt, "D2DRawData");
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

            ExportToCSVFile(dt, "LocationMaster");
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
            ExportToCSVFile(dt, "LocationMaster");
        }
        if (ViewState["1"].ToString() == "10")
        {
            DataTable dt = (DataTable)ViewState["OutD2d"];
            //  ExporttoExcel(gvD2d, dt, "OutOfDoorToDoor");

            ExportToCSVFile(dt, "OutOfDoorToDoor");
        }

        if (ViewState["1"].ToString() == "11")
        {
            DataTable dt = (DataTable)ViewState["VillageLatLog"];
            //  ExporttoExcel(gvvillageschoolgrid, dt, "VillageProfile");

            ExportToCSVFile(dt, "VillageTaggingReport");
        }

        if (ViewState["1"].ToString() == "15")
        {
            DataTable dt = (DataTable)Session["ExReport"];
            //  ExporttoExcel(gvvillageschoolgrid, dt, "VillageProfile");

            ExportToCSVFile(dt, "EmployeeExceptions");
        }
        if (ViewState["1"].ToString() == "555")
        {
            DataTable dt = (DataTable)ViewState["LearningBaseline"];
            // ExporttoExcel(GV_DynamicGrid, dt, "LearningBaseline");

            ExportToCSVFile(dt, "EndLine");
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

            ExportToCSVFile(dt, "EnrDailyStatus");
        }
        if (ViewState["1"].ToString() == "218")
        {
            DataTable dt = (DataTable)ViewState["RetentionIndividual"];
            // ExporttoExcel(gvvillageschoolgrid, dt, "SIC");

            ExportToCSVFile(dt, "RetentionIndividual");
        }
        if (ViewState["1"].ToString() == "611")
        {
            DataTable dt = (DataTable)ViewState["Reenrollment"];
            // ExporttoExcel(gvvillageschoolgrid, dt, "SIC");

            ExportToCSVFile(dt, "Rerollment");
        }

        if (ViewState["1"].ToString() == "612")
        {
            DataTable dt = (DataTable)ViewState["Reenrollment"];
            // ExporttoExcel(gvvillageschoolgrid, dt, "SIC");

            ExportToCSVFile(dt, "Rerollment");
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

    #region---------------------------------------------

    protected void village_Profile_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 11;
        LinkButton1.Visible = false;
        lnkCSV.Visible = true;
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
        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlState = "";

        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlState += "'" + item.Value + "'" + ",";


            }
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlState.Length > 0)
        {
            ddlState = ddlState.Substring(0, ddlState.LastIndexOf(","));
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "Where mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlState.Length > 0)
        {
            conditions += "and mst5Village.StateCode in(" + ddlState + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions += " and mst5Village.BlockCode in(" + ddlBlock + ")  ";

        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ")  ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }

        SqlParameter[] parm = new SqlParameter[]
            {
       new SqlParameter("@Condition",  conditions),


                 };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptVillageLatLog", parm);
        ViewState["VillageLatLog"] = dt;
        lblTotalCount.Text = dt.Rows.Count.ToString();
        if (dt.Rows.Count > 0)
        {
            btnCSV_Click(LinkButton6, null);
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


    public void RetentionIndividual(int Flag)
    {

        string condition = string.Empty;
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += " and  mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlState = "";

        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlState += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlState.Length > 0)
        {
            ddlState = ddlState.Substring(0, ddlState.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlState.Length > 0)
        {
            conditions += "and mst5Village.StateCode in(" + ddlState + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions += " and mst5Village.BlockCode in(" + ddlBlock + ")  ";

        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ")  ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
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
    public void ReportMobileActivityStatus(int Flag)
    {
        string condition = string.Empty;

        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlState = "";

        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlState += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlState.Length > 0)
        {
            ddlState = ddlState.Substring(0, ddlState.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += " mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlState.Length > 0)
        {
            conditions += "and mst5Village.StateCode in(" + ddlState + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions += " and mst5Village.BlockCode in(" + ddlBlock + ")  ";

        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ")  ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }

        //if (ddlYear.SelectedIndex > 0)
        //{
        //    conditions += "   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        //}



        DataTable dt = objMain.ReportMobileActivityStatus(conditions, ddlYear.SelectedValue.ToString());

        if (Convert.ToInt32(ddlYear.SelectedValue) == 2017 || Convert.ToInt32(ddlYear.SelectedValue) == 2018)
            dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "House", "ChildName", "Father Name", "CurrentAge");
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2019)
            dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Mauhalla", "House", "Child Name", "Father Name", "DOB", "SamgraID/GovtID", "Age");
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2020)
            dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Mauhalla", "House", "Child Name", "Father Name", "DOB", "SamgraID/GovtID", "Mobile No", "Age", "Latitude", "Longitude", "Class");
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2021)
            dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Mauhalla", "House", "Child Name", "Father Name", "DOB", "SamgraID/GovtID", "Mobile No", "Alternate Mobile Number", "Alternate mobile Owner Name", "Latitude", "Longitude", "Class");
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2022)
            dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Mauhalla", "House", "Child Name", "Father Name", "DOB", "SamgraID/GovtID", "Mobile No", "Alternate Mobile Number", "Alternate mobile Owner Name", "Age");


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


    protected void BtnShow_OnClick(object sender, EventArgs e)
    {
        MapId.Visible = true;
        GvReport.Visible = false;

        LinkButton1.Visible = true;
        lnkCSV.Visible = false;
        if (ddlUser.SelectedIndex <= 0)
        {
            LitChrtDistWise.Text = "";
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select User')</script>", false);
            return;
        }

        LoadReport(3);
        //GenerateExcelNewStringBuld();
        // DataTable dt = Session["ExReport"] as DataTable;
        DataTable dt = Session["MobileUser"] as DataTable;
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {

                BuildScript(dt);
            }
            else
            {
                LitChrtDistWise.Text = "";
            }
        }
    }
    protected void btnVisit_Click(object sender, EventArgs e)
    {
        LoadReport(2);
        MapId.Visible = false;
        GvReport.Visible = false;
        LinkButton1.Visible = false;
        lnkCSV.Visible = false;
        DataTable dt = Session["MobileUser"] as DataTable;
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                LitChrtDistWise.Text = TestCount(dt, "mapcanv");
            }
            else
            {
                LitChrtDistWise.Text = "";
            }
        }
        else
        {
            LitChrtDistWise.Text = "";
        }
    }
    protected void btnEmployee_Click(object sender, EventArgs e)
    {
        MapId.Visible = false;
        GvReport.Visible = true;
        LoadReport(1);

        LinkButton1.Visible = true;
        lnkCSV.Visible = false;
        // DataTable dt = Session["ExReport"] as DataTable;
        DataTable dt1 = Session["MobileUser"] as DataTable;
        if (dt1 != null)
        {
            GenerateExcelNewStringBuldNew();
            DataTable dt = Session["ExReport"] as DataTable;
            //  DataTable dt = Session["MobileUser"] as DataTable;
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ViewState["1"] = "15";
                    GvReport.DataSource = dt;
                    GvReport.DataBind();
                }
                else
                {
                    GvReport.DataSource = null;
                    GvReport.DataBind();
                }
            }
            else
            {
                LitChrtDistWise.Text = "";
            }
        }
    }
    public void LoadReport(Int32 Flag)
    {

        conditions = "";
        string conditions1 = "";

        string condition = string.Empty;
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += " where  mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlState = "";

        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlState += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlState.Length > 0)
        {
            ddlState = ddlState.Substring(0, ddlState.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlState.Length > 0)
        {
            conditions += "and mst5Village.StateCode in(" + ddlState + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions += " and mst5Village.BlockCode in(" + ddlBlock + ")  ";

        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ")  ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }

        if (ddlUser.SelectedIndex > 0)
        {
            conditions += " and Tbl_User_Login.UserID in('" + ddlUser.SelectedValue + "') ";
        }








        if (txtDate.Text != "")
        {
            DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
            conditions1 = " and Date >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
        }
        if (txtTodate.Text != "")
        {
            DateTime Todate = Convert.ToDateTime(txtTodate.Text);
            conditions1 = " and Date <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";

        }
        if (txtDate.Text != "" && txtTodate.Text != "")
        {
            DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
            DateTime Todate = Convert.ToDateTime(txtTodate.Text);
            conditions1 = " and Date BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
        }
        string mainCon = conditions + conditions1;
        DataTable dt = new DataTable();
        if (Flag == 1)
        {
            dt = objMain.tblReportGraph("", "", "", mainCon);
        }
        if (Flag == 2)
        {
            dt = objMain.rptTblUserLoginGraphCount("", "", "", mainCon);
        }
        if (Flag == 3)
        {
            dt = objMain.rptTblUserLoginMapLong("", "", "", mainCon);
        }

        if (dt.Rows.Count > 0)
        {

            Session["MobileUser"] = dt;


        }
        else
        {
            Session["MobileUser"] = null;
        }



    }

    private void GenerateExcelNewStringBuld()
    {



        DataTable dt = Session["MobileUser"] as DataTable;

        double distance = 0;


        if (dt.Rows.Count > 0)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {


                Int32 dHours = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Hours;
                Int32 dMins = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Minutes;

                TimeSpan dayJob = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"])).Subtract(Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"]));

                String villageGeoData = dt.Rows[i]["Village_GeoLocation"].ToString();

                if (villageGeoData != null && villageGeoData != "" && villageGeoData != "[]")
                {
                    if (dt.Rows[i]["StarttimeLocation"].ToString() == "" || villageGeoData.Length < 100 || dt.Rows[i]["StarttimeLocation"].ToString() == null || dt.Rows[i]["StarttimeLocation"].ToString() == "Unsupported browser" || dt.Rows[i]["StarttimeLocation"].ToString() == "User denied" || dt.Rows[i]["StarttimeLocation"].ToString() == "Location unavailable" || dt.Rows[i]["StarttimeLocation"].ToString() == "Request timed out" || dt.Rows[i]["StarttimeLocation"].ToString() == "Unknown error" || dt.Rows[i]["StarttimeLocation"].ToString() == "GPS turned off")
                    {
                        dt.Rows[i]["TotalHours"] = "0";
                    }
                    else
                    {

                        GeoUtils.GeoPointChecker geoChecker = new GeoUtils.GeoPointChecker(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData);
                        //if (geoChecker.isValid() == false && dt.Rows[i]["StarttimeLocation"].ToString().Length > 4 && dt.Rows[i]["EndtimeLocation"].ToString().ToString().Length > 4)

                        if (dt.Rows[i]["StarttimeLocation"].ToString().Length > 4 && dt.Rows[i]["EndtimeLocation"].ToString().ToString().Length > 4)
                        {
                            distance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData), 2);
                            if (distance >= 4)
                            {
                                dt.Rows[i]["TotalHours"] = distance;
                            }
                            else
                            {
                                dt.Rows[i]["TotalHours"] = distance;

                            }
                        }
                        else
                        {
                            dt.Rows[i]["TotalHours"] = "0";
                        }

                    }

                }


            }

        }
        DataTable dt2 = CreateDataTableUserWise();
        if (dt.Rows.Count > 0)
        {
            String[] arColoumn = { "FristName" };
            DataTable dtUser = dt.DefaultView.ToTable(true, arColoumn);

            DataRow[] dr = null;
            DataRow Item1;
            for (int i = 0; i < dtUser.Rows.Count; i++)
            {
                dr = dt.Select("FristName='" + dtUser.Rows[i]["FristName"].ToString() + "' and TotalHours>=4");
                if (dr.Length > 0)
                {

                    Item1 = dt2.NewRow();
                    Item1["Name"] = dtUser.Rows[i]["FristName"].ToString();
                    Item1["NumberExceptions"] = dr.Length.ToString();
                    dt2.Rows.Add(Item1);




                }
            }
        }

        Session["ExReport"] = dt2;





    }


    private void GenerateExcelNewStringBuldNew()
    {



        DataTable dt = Session["MobileUser"] as DataTable;

        double distance = 0;


        if (dt.Rows.Count > 0)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {


                Int32 dHours = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Hours;
                Int32 dMins = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Minutes;

                TimeSpan dayJob = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"])).Subtract(Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"]));

                String villageGeoData = dt.Rows[i]["Village_GeoLocation"].ToString();

                if (villageGeoData != null && villageGeoData != "" && villageGeoData != "[]")
                {
                    if (dt.Rows[i]["StarttimeLocation"].ToString() == "" || villageGeoData.Length < 100 || dt.Rows[i]["StarttimeLocation"].ToString() == null || dt.Rows[i]["StarttimeLocation"].ToString() == "Unsupported browser" || dt.Rows[i]["StarttimeLocation"].ToString() == "User denied" || dt.Rows[i]["StarttimeLocation"].ToString() == "Location unavailable" || dt.Rows[i]["StarttimeLocation"].ToString() == "Request timed out" || dt.Rows[i]["StarttimeLocation"].ToString() == "Unknown error" || dt.Rows[i]["StarttimeLocation"].ToString() == "GPS turned off")
                    {
                        dt.Rows[i]["TotalHours"] = "0";
                    }
                    else
                    {

                        GeoUtils.GeoPointChecker geoChecker = new GeoUtils.GeoPointChecker(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData);
                        //if (geoChecker.isValid() == false && dt.Rows[i]["StarttimeLocation"].ToString().Length > 4 && dt.Rows[i]["EndtimeLocation"].ToString().ToString().Length > 4)

                        if (dt.Rows[i]["StarttimeLocation"].ToString().Length > 4 && dt.Rows[i]["EndtimeLocation"].ToString().ToString().Length > 4)
                        {
                            distance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData), 2);
                            if (distance >= 4)
                            {
                                dt.Rows[i]["TotalHours"] = distance;
                            }
                            else
                            {
                                dt.Rows[i]["TotalHours"] = distance;

                            }
                        }
                        else
                        {
                            dt.Rows[i]["TotalHours"] = "0";
                        }

                    }

                }


            }

        }
        DataTable dt2 = CreateDataTableUserWiseNew();
        if (dt.Rows.Count > 0)
        {
            String[] arColoumn = { "VillageName" };
            DataTable dtUser = dt.DefaultView.ToTable(true, arColoumn);

            DataRow[] dr = null;
            DataRow Item1;
            for (int i = 0; i < dtUser.Rows.Count; i++)
            {
                dr = dt.Select("VillageName='" + dtUser.Rows[i]["VillageName"].ToString() + "' and TotalHours>=4");
                if (dr.Length > 0)
                {

                    Item1 = dt2.NewRow();
                    Item1["DistrictName"] = dr[0]["DistrictName"].ToString();
                    Item1["BlockName"] = dr[0]["BlockName"].ToString();
                    Item1["PanchayatName"] = dr[0]["PanchayatName"].ToString();
                    Item1["Village_GeoLocation"] = dr[0]["Village_GeoLocation"].ToString();
                    Item1["NumberExceptions"] = dr.Length.ToString();
                    dt2.Rows.Add(Item1);




                }
            }
        }

        Session["ExReport"] = dt2;





    }

    public DataTable CreateDataTableUserWiseNew()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("DistrictName", System.Type.GetType("System.String"));
        dtYear.Columns.Add("BlockName", System.Type.GetType("System.String"));
        dtYear.Columns.Add("PanchayatName", System.Type.GetType("System.String"));
        dtYear.Columns.Add("Village_GeoLocation", System.Type.GetType("System.String"));

        dtYear.Columns.Add("NumberExceptions", System.Type.GetType("System.Int32"));
        return dtYear;
    }


    public string TestCount(DataTable dt, string divID)
    {

        //StringBuilder sb1 = new StringBuilder();
        //sb1.Append("<script type='text/javascript'>$(document).ready(function () { var  chart = Highcharts.chart('" + divID + "', {");
        //sb1.Append("title: {text: 'Chart.update'},");
        //sb1.Append("subtitle: {text: 'Plain'},");
        //sb1.Append("xAxis: {");
        //sb1.Append("categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']");

        //sb1.Append("},");
        //sb1.Append("series: [{");
        //sb1.Append("type: 'column',");
        //sb1.Append("colorByPoint: false,");
        //sb1.Append("data: [29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4],");
        //sb1.Append("showInLegend: false");
        //sb1.Append("}]}); });</script>");

        StringBuilder sb = new StringBuilder();
        sb.Append("<script type='text/javascript'>$(document).ready(function () { var  chart = Highcharts.chart('" + divID + "', { chart: {type: 'column'},");
        sb.Append("title: {text: 'Total Employee  Visit Count'},");
        sb.Append("subtitle: {text: ''},");

        sb.Append(" xAxis: {");
        sb.Append(" type: 'category',");
        sb.Append("labels: {");

        sb.Append(" style: {");
        sb.Append("  fontSize: '10px',");
        sb.Append("fontFamily: 'Verdana, sans-serif'");
        sb.Append(" }   }   },");
        sb.Append("yAxis: {");
        sb.Append("min: 0,");
        sb.Append("title: {");
        sb.Append(" text: 'Village Visit Count'");
        sb.Append(" }   },");
        sb.Append(" legend: {");
        sb.Append(" enabled: false");
        sb.Append(" },");
        sb.Append(" tooltip: {");
        sb.Append("pointFormat: 'Village Count: <b>{point.y:.1f} </b>'");
        sb.Append(" },");
        sb.Append(" series: [{");
        sb.Append("name: 'Population',");
        sb.Append("data: [");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (i == dt.Rows.Count - 1)
            {
                sb.Append("['" + dt.Rows[i]["FristName"].ToString().Trim() + "'," + dt.Rows[i]["VillageCount"] + "]");
            }
            else
            {
                sb.Append("['" + dt.Rows[i]["FristName"].ToString().Trim() + "'," + dt.Rows[i]["VillageCount"] + "],");
            }
        }
        sb.Append("],");

        sb.Append("dataLabels: {");

        sb.Append("rotation: -90,");


        sb.Append("format: '{point.y:.1f}',");
        sb.Append("y: 10, ");
        sb.Append(" style: {");
        sb.Append("  fontSize: '13px',");
        sb.Append("fontFamily: 'Verdana, sans-serif'");
        sb.Append("}    }");

        sb.Append(" }]");
        sb.Append("});});</script>");

        //sb.Append("yAxis: { title: { text: 'Total percent market share'}},");
        //sb.Append("tooltip: {");
        ////sb.Append("headerFormat: '<span >{categories.Name}</span><br>',");
        ////sb.Append("pointFormat: '<span >jai ho</span>: <b>{point.NumberExceptions}</b><br/>'");
        ////sb.Append("},");
        //sb.Append("Population in 2017: <b>{point.series} millions</b>");
        //sb.Append("},");
        //sb.Append("xAxis: {");
        //sb.Append("categories:[ ");
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    if (i == dt.Rows.Count-1)
        //    {
        //        sb.Append("'" + dt.Rows[i]["Name"].ToString().Trim() + "'");
        //    }
        //    else
        //    {
        //        sb.Append("'" + dt.Rows[i]["Name"].ToString().Trim() + "',");
        //    }
        //}

        //sb.Append("]},");

        //sb.Append("series: [{");
        //sb.Append("type: 'column',");
        //sb.Append("colorByPoint: false,");
        //sb.Append("data:[");
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    if (i == dt.Rows.Count-1)
        //    {
        //        sb.Append("" + dt.Rows[i]["NumberExceptions"] + "");
        //    }
        //    else
        //    {
        //        sb.Append("" + dt.Rows[i]["NumberExceptions"] + ",");
        //    }
        //}

        //sb.Append("],showInLegend: false");
        //sb.Append("}]}); });</script>");

        return sb.ToString();
    }
    public string Test(DataTable dt, string divID)
    {



        StringBuilder sb = new StringBuilder();
        sb.Append("<script type='text/javascript'>$(document).ready(function () { var  chart = Highcharts.chart('" + divID + "', { chart: {type: 'column'},");
        sb.Append("title: {text: 'Total Number of Exceptions'},");
        sb.Append("subtitle: {text: ''},");
        sb.Append(" xAxis: {");
        sb.Append(" type: 'category',");
        sb.Append("labels: {");
        sb.Append(" style: {");
        sb.Append("  fontSize: '10px',");
        sb.Append("fontFamily: 'Verdana, sans-serif'");
        sb.Append(" }   }   },");
        sb.Append("yAxis: {");
        sb.Append("min: 0,");
        sb.Append("title: {");
        sb.Append(" text: 'Number of Exceptions'");
        sb.Append(" }   },");
        sb.Append(" legend: {");
        sb.Append(" enabled: false");
        sb.Append(" },");
        sb.Append(" tooltip: {");
        sb.Append("pointFormat: 'Exceptions: <b>{point.y:.1f} </b>'");
        sb.Append(" },");
        sb.Append(" series: [{");
        sb.Append("name: 'Population',");
        sb.Append("data: [");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (i == dt.Rows.Count - 1)
            {
                sb.Append("['" + dt.Rows[i]["Name"].ToString().Trim() + "'," + dt.Rows[i]["NumberExceptions"] + "]");
            }
            else
            {
                sb.Append("['" + dt.Rows[i]["Name"].ToString().Trim() + "'," + dt.Rows[i]["NumberExceptions"] + "],");
            }
        }
        sb.Append("],");
        sb.Append("dataLabels: {");
        sb.Append("rotation: -90,");
        sb.Append("format: '{point.y:.1f}',");
        sb.Append("y: 10, ");
        sb.Append(" style: {");
        sb.Append("  fontSize: '13px',");
        sb.Append("fontFamily: 'Verdana, sans-serif'");
        sb.Append("}    }");
        sb.Append(" }]");
        sb.Append("});});</script>");


        return sb.ToString();
    }

    public DataTable CreateDataTableUserWise()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("Name", System.Type.GetType("System.String"));

        dtYear.Columns.Add("NumberExceptions", System.Type.GetType("System.Int32"));
        return dtYear;
    }

    private void BuildScript(DataTable tbl)
    {

        string LatLong = "";
        string LatName = "";
        foreach (DataRow dr in tbl.Rows)
        {
            string villeo = dr["dd"].ToString();
            string Name = dr["VillageName"].ToString();
            string[] a = villeo.Split(',');
            LatLong += a[0].Trim() + "," + a[1].Trim() + "#";
            LatName += Name + "#";
        }
        LatLong = LatLong.Substring(0, LatLong.LastIndexOf("#"));
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode = item.Value;
                break;

            }
        }




        StringBuilder sb1 = new StringBuilder();
        //sb1.Append("<script type='text/javascript'>$(document).ready(function () { var  chart = Highcharts.chart('" + divID + "', {");
        sb1.Append(" <script type='text/javascript'>");
        sb1.Append("$(document).ready(function () {(function () {");
        if (ddlStatecode == "8")
        {
            sb1.Append("var options = { zoom: 6, center: new google.maps.LatLng(26.386948928, 72.97668457), mapTypeId: google.maps.MapTypeId.TERRAIN, mapTypeControl: false };");
        }
        else
        {
            sb1.Append("var options = { zoom: 6, center: new google.maps.LatLng(23.473324, 77.947998), mapTypeId: google.maps.MapTypeId.TERRAIN, mapTypeControl: false };");
        }
        sb1.Append("var map = new google.maps.Map(document.getElementById('mapcanv'), options);");

        sb1.Append("var latlongs = '" + LatLong + "';");

        sb1.Append("var Names = '" + LatName + "';");
        sb1.Append("var arr = latlongs.split('#');");
        sb1.Append("var ltngNames = Names.split('#');");
        sb1.Append("for (var i = 0; i < arr.length - 1; i++) {");
        sb1.Append("var ltlnval = arr[i];");
        sb1.Append("var ltng = ltlnval.split(',');");
        sb1.Append("var marker = new google.maps.Marker({");
        sb1.Append("position: new google.maps.LatLng(ltng[0].trim(), ltng[1].trim()),map: map,title: 'Click Me '});");
        sb1.Append("(function (marker, i) {google.maps.event.addListener(marker, 'click', function () {");
        sb1.Append("infowindow = new google.maps.InfoWindow({content: ltngNames[i]});infowindow.open(map, marker);});})(marker, i);}})();});");
        sb1.Append("</script>");


        LitChrtDistWise.Text = sb1.ToString();
    }

    #endregion
}