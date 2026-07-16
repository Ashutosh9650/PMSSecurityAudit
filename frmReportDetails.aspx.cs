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
public partial class frmReportDetails : System.Web.UI.Page
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
        if (Convert.ToString(Session["username"]) != "")
        {

            if (!IsPostBack)
            {
                LoadYear();
                LoadUserLeavel();
                ViewState["1"] = "ss";
                // FillUser();

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
    public void FillUser()
    {
        string ddlDistrict = "";
        conditions = "";
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
        if (ddlDistrict.Length > 0)
        {
            conditions = " DistrictCode in(" + ddlDistrict + ")";
            objComman.BindDLL("MstUser", "UserName,[UserName]+' ('+ FristName +')' as UserName1 ", conditions, "UserName1", "asc", ddlUser, "UserName1", "UserName", "--Select--");
            conditions = "";
        }
        else
        {

            objComman.BindDLL("MstUser", "UserName,[UserName]+' ('+ FristName +')' as UserName1 ", "", "UserName1", "asc", ddlUser, "UserName1", "UserName", "--Select--");
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
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
        gvEnrollSummary.Visible = false;
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
        gvEnrollSummary.Visible = false;
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
        gvEnrollSummary.Visible = false;
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
        gvEnrollSummary.Visible = false;
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
        gvEnrollSummary.Visible = false;
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
        gvEnrollSummary.Visible = false;
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
        gvEnrollSummary.Visible = false;
    }

    protected void btnEnrolllmentAgp_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 534;
        ClearGrid(); ;
        gvD2d.Visible = false;
        GvReport.Visible = false;
        GV_DynamicGrid.Visible = false;
        GV_DynamicGrid1.Visible = false;
        GV_DynamicGrid2.Visible = false;
        gvvillageschoolgrid.Visible = false;
        gvUserReport.Visible = false;
        gvnroll.Visible = false;
        GvReport.DataSource = null;
        GvReport.DataBind();
        gvD2d.DataSource = null;
        gvD2d.DataBind();
        //LoadReport();
        gvUserReport.DataSource = null;
        gvUserReport.DataBind();
        LoadReportEnrollmentAgp();
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
        gvEnrollSummary.Visible = false;
    }

    protected void btnEnrolllmentDelete_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 2226;
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
        LoadReportEnrollmentDelete();
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
        gvEnrollSummary.Visible = false;
    }


    protected void btnEnrolllmentDuplicate_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 2227;
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
        LoadReportEnrollmentDuplicate();
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
        gvEnrollSummary.Visible = false;
    }
    protected void btnEnrolllmenSummary_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 2228;
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
        LoadReportEnrollmentSummary();
        gvRetaion.Visible = false;
        LinkButton1.Visible = true;
        gvEnrollSummary.Visible = true;
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
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2024)
        {
            LoadMasterData(12);
        }
        else
        {
            LoadMasterData(0);
        }

        GV_DynamicGrid.Visible = true;
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
        gvEnrollSummary.Visible = false;
    }
    protected void LnkGovt_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 777;
        ClearGrid();
        gvnroll.Visible = false;
        gvD2d.Visible = false;
        GV_DynamicGrid1.Visible = false;
        GV_DynamicGrid2.Visible = false;
        GvReport.Visible = false;
        gvUserReport.Visible = false;
        LoadMasterDataGovt(0);
        GV_DynamicGrid.Visible = true;
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
        gvEnrollSummary.Visible = false;
    }

    protected void btnGKP_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 7777;
        ClearGrid();
        gvnroll.Visible = false;
        gvD2d.Visible = false;
        GV_DynamicGrid1.Visible = false;
        GV_DynamicGrid2.Visible = false;
        GvReport.Visible = false;
        gvUserReport.Visible = false;
        LoadGKPDeatils(0);
        GV_DynamicGrid.Visible = true;
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
        gvEnrollSummary.Visible = false;
    }


    public void LoadMasterDataGovt(int Flag)
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
        if (Flag == 2)
        {
            if (ddlStatecode.Length > 0)
            {
                conditions += " Where mst2District.StateCode in(" + ddlStatecode + ") ";

            }
        }
        else
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "    where mst2District.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (ddlStatecode.Length > 0)
            {
                conditions += " and mst2District.StateCode in(" + ddlStatecode + ") ";

            }
        }

        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst2District.DistrictCode in(" + ddlDistrict + ") ";

        }




        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con", conditions),



        };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptGovtRelations]", cmdParameters);

        ViewState["D2dUser"] = dt;

        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        GV_DynamicGrid1.DataSource = null;
        GV_DynamicGrid1.DataBind();
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();



        if (dt.Rows.Count > 1500)
        {
            btnCSV_Click(LinkButton8, null);
        }
        else
        {
            lblTotalCount.Text = (dt.Rows.Count).ToString();
            GV_DynamicGrid.DataSource = dt;
            GV_DynamicGrid.DataBind();
        }




    }
    public void LoadGKPDeatils(int Flag)
    {
        conditions = "";



        lblTotalCount.Text = "";


        string condition = string.Empty;
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "  where  Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }



        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con", conditions),



        };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptGKPMater]", cmdParameters);

        ViewState["D2dUser"] = dt;

        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        GV_DynamicGrid1.DataSource = null;
        GV_DynamicGrid1.DataBind();
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();



        if (dt.Rows.Count > 1500)
        {
            btnCSV_Click(LinkButton8, null);
        }
        else
        {
            lblTotalCount.Text = (dt.Rows.Count).ToString();
            GV_DynamicGrid.DataSource = dt;
            GV_DynamicGrid.DataBind();
        }




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

    protected void LnkInfluencerDetail_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 555;
        ClearGrid();
        gvnroll.Visible = false;
        gvD2d.Visible = false;
        GvReport.Visible = false;
        gvUserReport.Visible = false;
        GV_DynamicGrid.Visible = false;
        GV_DynamicGrid2.Visible = false;
        LoadInuData(1);
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
        LoadMasterDataTeamBalikaTraining(3);
        GV_DynamicGrid1.Visible = false;
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
    }

    public void LoadMasterDataTeamBalikaTraining(int Flag)
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
        if (Flag == 2)
        {
            if (ddlStatecode.Length > 0)
            {
                conditions += " Where V.StateCode in(" + ddlStatecode + ") ";

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
        if (ddlPhan.Length > 0)
        {
            conditions += " and V.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and V.VillageCode in(" + ddlVillage + ") ";
        }
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

                    conditions += " and (Year([FromDate])*10000)+(Month([FromDate])*100+Day([FromDate])) Between  '" + Year1[0] + "0401'  and '" + Year1[1] + "0331'";
                    // conditions += "    And FromDate >= '" + Year1[0] + "-04-01' and ToDate<='" + Year1[1] + "-03-31'";


                }
            }
        }

        SqlParameter[] parm = new SqlParameter[]
            {

            new SqlParameter("@Condition", conditions),
            new SqlParameter("@Flag", Flag),
                new SqlParameter("@Year", ddlYear.SelectedValue)

                 };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptMasterTeamBailkTraining2026]", parm);


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
            if (dt.Rows.Count > 100)
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
            if (dt.Rows.Count > 100)
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
            if (dt.Rows.Count > 1)
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
    }
    protected void LnkUserRole4_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 139;
        ClearGrid();
        gvnroll.Visible = false;
        gvD2d.Visible = false;
        GV_DynamicGrid1.Visible = false;
        GV_DynamicGrid.Visible = false;
        GvReport.Visible = false;
        gvUserReport.Visible = false;
        gvvillageschoolgrid.Visible = false;
        LoadMasterDataSafty(2);
        GV_DynamicGrid2.Visible = true;
        gvRetaion.Visible = false;
        LinkButton1.Visible = false;
    }

    public void LoadMasterDataSafty(int Flag)
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


        conditions = "where 1=1 ";
        if (ddlStatecode.Length > 0)
        {
            conditions += " and MstUser.StateCode in(" + ddlStatecode + ") ";

        }


        if (ddlDistrict.Length > 0)
        {
            conditions += " and MstUser.DistrictCode in(" + ddlDistrict + ") ";

        }


        if (ddlBlock.Length > 0)
        {
            conditions += " and MstUser.BlockCode in(" + ddlBlock + ") ";

        }

        SqlParameter[] cmdParameters = new SqlParameter[]
    {
            new SqlParameter("@con", conditions),

    };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptSafetySecurity]", cmdParameters);

        ViewState["D2dUser"] = dt;

        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        GV_DynamicGrid1.DataSource = null;
        GV_DynamicGrid1.DataBind();
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();
        GV_DynamicGrid.Visible = true;
        lblTotalCount.Text = (dt.Rows.Count).ToString();
        if (dt.Rows.Count > 0)
        {
            GV_DynamicGrid.DataSource = dt;
            GV_DynamicGrid.DataBind();

        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }







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

    protected void LnkMobileDataReport15_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 776;
        ClearGrid();
        gvnroll.Visible = false;
        gvD2d.Visible = false;
        GV_DynamicGrid1.Visible = false;
        GV_DynamicGrid2.Visible = false;
        GvReport.Visible = false;
        gvUserReport.Visible = false;
        ReportMobileActivityStatus15to18(0);
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


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", conditions)   ,
            new SqlParameter("@MYear", ddlYear.SelectedValue)
        };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptRetentionNew]", cmdParameters);

        //DataTable dt = objMain.rptRetention(conditions);
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
            ExporttoExcel(gvnroll, dt, "Enrollment");
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
        if (ViewState["1"].ToString() == "2228")
        {

            GenerateExcelNewEnroll("EnrollmentSummary");
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
    public void LoadReportEnrollmentAgp()
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



        string FristCon = conditions;

        SqlParameter[] cmdParameters = new SqlParameter[]
    {
            new SqlParameter("@condtion", FristCon),
            new SqlParameter("@Fyear", ddlYear.SelectedValue)
    };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportEnrollDeatilsNewAGP]", cmdParameters);

        //DataTable dt = objMain.ReportEnrollDeatilsNew(FristCon);
        if (dt.Rows.Count > 0)
        {
            GV_DynamicGrid.Visible = true;
            ViewState["Enroll"] = dt;
            lblTotalCount.Text = (dt.Rows.Count).ToString();
            if (dt.Rows.Count > 1000)
            {
                btnCSV_Click(LinkButton4, null);
            }
            else
            {
                GV_DynamicGrid.DataSource = dt;
                GV_DynamicGrid.DataBind();
            }



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

            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", FristCon),
            new SqlParameter("@Fyear", ddlYear.SelectedValue)
        };
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportEnrollDeatilsNew2020]", cmdParameters);

            //DataTable dt = objMain.ReportEnrollDeatilsNew(FristCon);
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



    public void LoadReportEnrollmentDelete()
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

        //  DataTable dt = objMain.ReportEnrollDeatilsNew(FristCon);
        SqlParameter[] cmdParameters = new SqlParameter[]
    {
            new SqlParameter("@condtion", FristCon)
    };

        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportEnrollDeatilsNewdelate]", cmdParameters);
        if (dt.Rows.Count > 0)
        {
            GV_DynamicGrid.Visible = true;
            ViewState["Enroll123"] = dt;
            lblTotalCount.Text = (dt.Rows.Count).ToString();
            if (dt.Rows.Count > 4000)
            {
                btnCSV_Click(LinkButton4, null);
            }
            else
            {
                GV_DynamicGrid.DataSource = dt;
                GV_DynamicGrid.DataBind();
            }


        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
            ViewState["Enroll123"] = null;
        }





    }

    public void LoadReportEnrollmentDuplicate()
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

        //  DataTable dt = objMain.ReportEnrollDeatilsNew(FristCon);
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", FristCon)
        };

        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptEnrollmetDuplicateRecord]", cmdParameters);
        if (dt.Rows.Count > 0)
        {
            dt.Columns.Remove("Tempschoolcode");
            dt.Columns.Remove("TempSerial");
            dt.Columns.Remove("tempclass");
            dt.Columns.Remove("class2");
            dt.Columns.Remove("Schoolcode");
            dt.Columns.Remove("duplicateRecCount");

            GV_DynamicGrid.Visible = true;
            ViewState["Enroll123"] = dt;
            lblTotalCount.Text = (dt.Rows.Count).ToString();
            if (dt.Rows.Count > 4000)
            {
                btnCSV_Click(LinkButton4, null);
            }
            else
            {
                GV_DynamicGrid.DataSource = dt;
                GV_DynamicGrid.DataBind();
            }


        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
            ViewState["Enroll123"] = null;
        }





    }


    public void LoadReportEnrollmentSummary()
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

        //  DataTable dt = objMain.ReportEnrollDeatilsNew(FristCon);
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@schoolCode", FristCon),
            new SqlParameter("@Year", ddlYear.SelectedValue),
            new SqlParameter("@Flag", "1"),
        };

        DataSet dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rpEnrollmentSummaryNew]", cmdParameters);
        if (dt.Tables[0].Rows.Count > 0)
        {


            gvEnrollSummary.Visible = true;
            Session["Enroll123"] = dt;
            lblTotalCount.Text = (dt.Tables[0].Rows.Count).ToString();
            gvEnrollSummary.DataSource = dt.Tables[0];
            gvEnrollSummary.DataBind();

        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
            Session["Enroll123"] = null;
        }





    }
    private void GenerateExcelNewEnroll(string FIleName)
    {
        try
        {



            DataSet dt = Session["Enroll123"] as DataSet;
            if (dt.Tables[0].Rows.Count > 0)
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
                HttpContext.Current.Response.Write("<table  >");
                HttpContext.Current.Response.Write("<tr>");
                HttpContext.Current.Response.Write("<td colspan='25' ' style='text-align:left;border:.2pt solid windowtext;'></td>");

                HttpContext.Current.Response.Write("</tr>");
                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                //  HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  width:2%;'>Block</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'> </th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'> By Age</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='8' style='" + HeaderStyle + "  width:2%;'> By Class</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>Flag</th>");
                HttpContext.Current.Response.Write("</tr>");

                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<th class='header' colspan='10' style='" + HeaderStyle + "  width:2%;'> </th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> Total Enrollment</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='6' style='" + HeaderStyle + "  width:2%;'> 	Male</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='6' style='" + HeaderStyle + "  width:2%;'> Female</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> 	Male (Age 5-14)</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> Male (Age 7-14)</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> 	Female (Age 5-14)</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> Female (Age 7-14)</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> D2D</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> OOD2D</th>");

                HttpContext.Current.Response.Write("</tr>");



                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");

                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	DISTRICT	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	DISTRICT CODE	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	BLOCK	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	BLOCK CODE	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	GRAMPANCHAYAT	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	GRAMPANCHAYAT CODE	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	CLUSTER	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	CLUSTER CODE	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	VILLAGE	</th>");


                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	VILLAGE CODE	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	All Class	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Class 1-8	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	3 TO 4	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	5	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	6	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	7 TO 14	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	15 & Above	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	TOTAL	</th>");


                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	3 TO 4	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	5	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	6	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	7 TO 14	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	15 & Above	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	TOTAL	</th>");

                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Class 1	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Class 2-8	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Class 1	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Class 2-8	</th>");

                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Class 1	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Class 2-8	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Class 1	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Class 2-8	</th>");

                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	AE	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	DO	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	NE	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	OOD2D	</th>");




                HttpContext.Current.Response.Write("</tr>");

                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";
                String ToallRowStyle = "border:.2pt solid windowtext; font-weight:100; font-size:11pt;rowspan=2;border:.2pt solid windowtext;";

                String RowStyeYellow = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#FFFF00;";
                String RowStyeRed = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#FF0000;";
                String RowStyeGreen = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#008000;";





                for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                {




                    HttpContext.Current.Response.Write("<tr>");
                    //HttpContext.Current.Response.Write("<td >Direct</td>");
                    for (int c = 0; c < dt.Tables[0].Columns.Count; c++)
                    {


                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Tables[0].Rows[i][c] + "</td>");


                    }
                    #region Row1



                    #endregion


                    HttpContext.Current.Response.Write("</tr>");


                }
                //HttpContext.Current.Response.Write("<tr>");
                //HttpContext.Current.Response.Write("</tr>");
                HttpContext.Current.Response.Write("<tr>");
                for (int i = 0; i < dt.Tables[1].Rows.Count; i++)
                {
                    for (int c = 0; c < dt.Tables[0].Columns.Count; c++)
                    {
                        if (c == 7)
                        {
                            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                        }
                        else
                        {
                            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Tables[1].Rows[i][c] + "</td>");
                        }
                    }
                }
                HttpContext.Current.Response.Write("</tr>");
                //HttpContext.Current.Response.Write("<tr>");
                //HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
                //HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
                //HttpContext.Current.Response.Write("</tr>");


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

    public void LoadInuData(int Flag)
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
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "     V.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }

        if (ddlDistrict.Length > 0)
        {
            conditions += " and V.DistrictCode in(" + ddlDistrict + ") ";

        }

        if (ddlBlock.Length > 0)
        {

            conditions += " and V.BlockCode in(" + ddlBlock + ") ";


        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and V.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and V.VillageCode in(" + ddlVillage + ") ";
        }
        //if (Flag == 3)
        //{
        //    string Year = ddlYear.SelectedItem.Text;
        //    string[] Year1 = Year.Split('-');
        //    if (ddlYear.SelectedItem.Text == "2016-2017")
        //    {
        //        if (ddlYear.SelectedIndex > 0)
        //        {
        //            conditions += "    And FromDate <= '" + Year1[1] + "-03-31'";
        //        }
        //    }
        //    else
        //    {
        //        if (ddlYear.SelectedIndex > 0)
        //        {
        //            conditions += "    And FromDate >= '" + Year1[0] + "-04-01' and ToDate<='" + Year1[1] + "-03-31'";


        //        }
        //    }
        //}
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con", conditions)

        };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptInfluencerProfile]", cmdParameters);

        ViewState["D2dUser"] = dt;

        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        GV_DynamicGrid1.DataSource = null;
        GV_DynamicGrid1.DataBind();
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();
        GV_DynamicGrid.Visible = true;

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
            if (Convert.ToString(Session["username"]) == "PMSAdmin" || Convert.ToString(Session["username"]) == "SuperAdmin" || Convert.ToString(Session["username"]) == "EGE7557")
            {
                Flag = 10;
            }
            if (ddlStatecode.Length > 0)
            {
                conditions += " and V.StateCode in(" + ddlStatecode + ") ";

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
        if (ddlPhan.Length > 0)
        {
            conditions += " and V.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and V.VillageCode in(" + ddlVillage + ") ";
        }
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
        DataTable dt = null;
        if (Flag == 1)
        {
            string Year = ddlYear.SelectedItem.Text;
            string[] Year1 = Year.Split('-');
            string fDate = "And FromDate >= '" + Year1[0] + "-04-01' and ToDate<= '" + Year1[1] + "-03-31'";
            dt = LoadMasterDataNewTr(conditions, Flag, fDate);
        }
        else

        {
            if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
            {
                dt = LoadMasterDataNew2026(conditions, Flag);
            }
            else if (Convert.ToInt32(ddlYear.SelectedValue) == 2024 | Convert.ToInt32(ddlYear.SelectedValue) == 2025)
            {
                dt = objMain.LoadMasterDataNew(conditions, Flag);
            }
            // dt = objMain.LoadMasterDataNew(conditions, Flag);
        }
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
            if (dt.Rows.Count > 0)
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

    public DataTable LoadMasterDataNew2026(string condition, int Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Condition", condition),
            new SqlParameter("@Flag", Flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Report_Master_DataNew2026]", cmdParameters);
    }
    public DataTable LoadMasterDataNewTr(string condition, int Flag, string fromdate)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Condition", condition),
            new SqlParameter("@Flag", Flag),
             new SqlParameter("@Fdate", fromdate),

        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Report_Master_TeamBail]", cmdParameters);
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
            //  int icount = objMain.InsertReportAudittrail("Report Details", "Door to Door", Convert.ToString(Session["username"]));
            if (dt.Rows.Count > 0)
            {

                gvD2d.Visible = true;
                ViewState["D2dAllData"] = dt;
                lblTotalCount.Text = (dt.Rows.Count).ToString();
                if (dt.Rows.Count > 10000)
                {

                    ExportToCSVFile(dt, "D2DRawData");
                    //  btnCSV_Click(LinkButton5, null);
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
            ChkState.DataSource = dtAllState;
            ChkState.DataTextField = "StateName";
            ChkState.DataValueField = "StateCode";
            ChkState.DataBind();
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
            ChkState.DataSource = dtAllState;
            ChkState.DataTextField = "StateName";
            ChkState.DataValueField = "StateCode";
            ChkState.DataBind();
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
            ChkState.DataSource = dtAllState;
            ChkState.DataTextField = "StateName";
            ChkState.DataValueField = "StateCode";
            ChkState.DataBind();

            foreach (ListItem item in ChkState.Items)
            {

                item.Selected = true;

            }

        }

    }
    public void LoadUserLeavel()
    {
        conditions = "";
        AlllStateCode();
        if (Session["user_level_Role"].ToString() == "1")
        {

            //string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State   order by StateName   ";
            //DataTable dtState = objMain.LoadData(strQry1);
            //ChkState.DataSource = dtState;
            //ChkState.DataTextField = "StateName";
            //ChkState.DataValueField = "StateCode";
            //ChkState.DataBind();

            ChkState.Enabled = true;
            chkDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            //conditions = "UserName='" + Session["username"].ToString() + "' ";
            //string strQry1 = "  SELECT distinct mst1State.StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode inner join mst1State on mst1State.StateCode=mst2District.StateCode where   " + conditions + "  order by StateName   ";
            //DataTable dtState = objMain.LoadData(strQry1);
            //ChkState.DataSource = dtState;
            //ChkState.DataTextField = "StateName";
            //ChkState.DataValueField = "StateCode";
            //ChkState.DataBind();
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
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            ////objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            //string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State  where   " + conditions + "  order by StateName   ";
            //DataTable dtState = objMain.LoadData(strQry1);
            //ChkState.DataSource = dtState;
            //ChkState.DataTextField = "StateName";
            //ChkState.DataValueField = "StateCode";
            //ChkState.DataBind();
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

            if (Session["user_level_Role"].ToString() == "2")
            {
                foreach (ListItem item in chkDistrict.Items)
                {

                    item.Selected = true;

                }
                ddlDistrict_SelectedIndexChanged(ddlState, null);
            }
            //foreach (ListItem item in chkDistrict.Items)
            //{

            //    item.Selected = true;
            //    break;
            //}
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
            conditions = " DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";


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
            strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions + "  and  Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName  ";
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

        if (Session["user_level_Role"].ToString() == "2")
        {
            foreach (ListItem item in chkDistrict.Items)
            {

                item.Selected = true;

            }
        }
        ddlPanchayat.Items.Clear();
        chkVillage.Items.Clear();
    }


    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            AlllStateCode();
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

                //conditions = "UserName='" + Session["username"].ToString() + "' ";
                //string strQry1 = "  SELECT distinct mst1State.StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode inner join mst1State on mst1State.StateCode=mst2District.StateCode where   " + conditions + "  order by StateName   ";
                //DataTable dtState = objMain.LoadData(strQry1);
                //ChkState.DataSource = dtState;
                //ChkState.DataTextField = "StateName";
                //ChkState.DataValueField = "StateCode";
                //ChkState.DataBind();
            }
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            foreach (ListItem item in ChkState.Items)
            {

                item.Selected = true;

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
        ddlPanchayat.Items.Clear();
        chkVillage.Items.Clear();
        chkDistrict.Items.Clear();
        chkBlock.Items.Clear();
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


        if (Session["user_level_Role"].ToString() == "4")
        {
            if (chkBlock.Items.Count > 0)
            {
                foreach (ListItem item in chkBlock.Items)
                {

                    item.Selected = true;

                }
            }
            chkBlock.Enabled = false;
            ddlBlock_SelectedIndexChanged(ddlDistrict, null);
        }
        else
        {
            ddlPanchayat.Items.Clear();
            chkVillage.Items.Clear();
        }
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

            ExportToCSVFile(dt, "Enrollment");
        }
        if (ViewState["1"].ToString() == "534")
        {
            DataTable dt = (DataTable)ViewState["Enroll"];
            //   ExporttoExcel(gvnroll, dt, "Enrolllment");

            ExportToCSVFile(dt, "EnrollmentAGP");
        }

        if (ViewState["1"].ToString() == "2226")
        {
            DataTable dt = (DataTable)ViewState["Enroll123"];
            //   ExporttoExcel(gvnroll, dt, "Enrolllment");

            ExportToCSVFile(dt, "EnrollmentDelete");
        }
        if (ViewState["1"].ToString() == "2227")
        {
            DataTable dt = (DataTable)ViewState["Enroll123"];
            //   ExporttoExcel(gvnroll, dt, "Enrolllment");

            ExportToCSVFile(dt, "EnrollmentDuplicate");
        }

        if (ViewState["1"].ToString() == "2228")
        {
            DataTable dt = (DataTable)ViewState["Enroll123"];
            //   ExporttoExcel(gvnroll, dt, "Enrolllment");

            ExportToCSVFile(dt, "EnrollmentSummary");
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
        if (ViewState["1"].ToString() == "555")
        {
            DataTable dt = (DataTable)ViewState["D2dUser"];
            // ExporttoExcel(GV_DynamicGrid1, dt, "TeamBalika");

            ExportToCSVFile(dt, "InfluencerDetail");
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
        if (ViewState["1"].ToString() == "139")
        {
            DataTable dt = (DataTable)ViewState["D2dUser"];
            // ExporttoExcel(GV_DynamicGrid2, dt, "UserMaster");

            ExportToCSVFile(dt, "SafetySecurity");
        }


        if (ViewState["1"].ToString() == "7")
        {
            DataTable dt = (DataTable)ViewState["D2dUser"];
            //ExporttoCSV(GV_DynamicGrid, dt);
            ExportToCSVFile(dt, "LocationMaster");
        }

        if (ViewState["1"].ToString() == "777")
        {
            DataTable dt = (DataTable)ViewState["D2dUser"];
            //ExporttoCSV(GV_DynamicGrid, dt);
            ExportToCSVFile(dt, "GovtLiasionreport");
        }
        if (ViewState["1"].ToString() == "7777")
        {
            DataTable dt = (DataTable)ViewState["D2dUser"];
            //ExporttoCSV(GV_DynamicGrid, dt);
            ExportToCSVFile(dt, "GKPMaster");
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

            ExportToCSVFile(dt, "LearningAssessment");
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
        if (ViewState["1"].ToString() == "776")
        {
            DataTable dt = (DataTable)ViewState["ReportMobileActivityStatus"];
            // ExporttoExcel(gvvillageschoolgrid, dt, "SIC");

            ExportToCSVFile(dt, "EnrDailyStatus(15to18)");
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
            conditions += "Where v.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlState.Length > 0)
        {
            conditions += "and v.StateCode in(" + ddlState + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and v.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions += " and v.BlockCode in(" + ddlBlock + ")  ";

        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and v.PanchayatCode in(" + ddlPhan + ")  ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and v.VillageCode =in(" + ddlVillage + ") ";
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
            if (dt.Rows.Count > 1000)
            {

                btnCSV_Click(Button4, null);
            }
            else
            {
                gvvillageschoolgrid.DataSource = dt;
                gvvillageschoolgrid.DataBind();
            }
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




        //  DataTable dt = objMain.rptRetentionIndividual(conditions);

        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),


                        new SqlParameter("@Myear",ddlYear.SelectedValue),


        };
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptRetentionIndividualNewwithCV", cmdParameters);

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
        //string condition = string.Empty;

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
        conditions = "";
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += " mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlState.Length > 0)
        {
            conditions += " and mst5Village.StateCode in(" + ddlState + ") ";

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

    public void ReportMobileActivityStatus15to18(int Flag)
    {
        //string condition = string.Empty;

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
        conditions = "";
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += " mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlState.Length > 0)
        {
            conditions += " and mst5Village.StateCode in(" + ddlState + ") ";

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

        SqlParameter[] cmdParameters = new SqlParameter[]
    {
                    new SqlParameter("@condtion", conditions),

                    new SqlParameter("@Year", ddlYear.SelectedValue),

    };

        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "ReportMobileActivityStatus15to18", cmdParameters);


        ViewState["ReportMobileActivityStatus"] = dt;

        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        GV_DynamicGrid1.DataSource = null;
        GV_DynamicGrid1.DataBind();
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();


        lblTotalCount.Text = (dt.Rows.Count).ToString();
        if (dt.Rows.Count > 0)
        {
            btnCSV_Click(LinkButton5, null);
        }
        else
        {
            GV_DynamicGrid.DataSource = dt;
            GV_DynamicGrid.DataBind();
        }



    }


    protected void gvReportNew_RowCreated(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.Header)
        {


            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow.CssClass = "gridnewheadercss";
            TableCell HeaderCell;

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;


            HeaderCell.ColumnSpan = 12;
            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);

            //  HeaderCell.ColumnSpan = 5;




            HeaderCell = new TableCell();
            HeaderCell.Text = "By Age";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 12;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);



            HeaderCell = new TableCell();
            HeaderCell.Text = "By Class";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 8;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);



            HeaderCell = new TableCell();
            HeaderCell.Text = "Flag";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 4;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);


            gvEnrollSummary.Controls[0].Controls.AddAt(0, HeaderGridRow);








            GridView HeaderGrid1 = (GridView)sender;
            GridViewRow HeaderGridRow1 = new GridViewRow(1, 1, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow1.CssClass = "gridnewheadercss";
            TableCell HeaderCell1;

            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;


            HeaderCell1.ColumnSpan = 10;

            //  HeaderCell1.ColumnSpan = 5;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Total Enrollment";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 2;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Male";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 6;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Female";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 6;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Male (Age 5-14)";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 2;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Male (Age 7-14)";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 2;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Female (Age 5-14)";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 2;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Female (Age 7-14)";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 2;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "D2D";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;



            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "OOD2D";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 1;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            gvEnrollSummary.Controls[0].Controls.AddAt(1, HeaderGridRow1);


        }
    }
    #endregion

    protected void txtdatefrom_TextChanged(object sender, EventArgs e)
    {
        if (txtTodate.Text != "" && txtDate.Text != "")
        {
            DateTime startDate = Convert.ToDateTime(txtDate.Text);
            DateTime endDate = Convert.ToDateTime(txtTodate.Text);
            if (endDate >= startDate)
            { }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Selection')</script>", false);
                txtTodate.Text = "";
                txtDate.Text = "";
                return;
            }
        }
    }
    protected void txtTodate_TextChanged(object sender, EventArgs e)
    {
        if (txtTodate.Text != "" && txtDate.Text != "")
        {
            DateTime startDate = Convert.ToDateTime(txtDate.Text);
            DateTime endDate = Convert.ToDateTime(txtTodate.Text);
            if (endDate >= startDate)
            { }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Selection')</script>", false);
                txtTodate.Text = "";
                txtDate.Text = "";
                return;
            }
        }
    }
}