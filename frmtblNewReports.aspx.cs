using ClosedXML.Excel;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class frmtblNewReports : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    GeoUtils objGeo = new GeoUtils();
    public HttpContext Contx;
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


                ViewState["1"] = "ss";
                // LoadData();
                // FillUser();
                //LoadReport();
                ddlType.SelectedIndex = 1;
                LoadYear();
                LoadUserLeavel();

                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                LinkButton2.Visible = true;
                if (Convert.ToString(Session["username"]) == "PMSAdmin" || Convert.ToString(Session["username"]) == "SuperAdmin")
                {
                    kkk.Visible = true;
                }
                else
                {
                    kkk.Visible = false;
                }

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
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


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
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadUser();
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


        }
        else
        {
            foreach (ListItem item in ChkState.Items)
            {

                item.Selected = false;

            }
            chkDistrict.Items.Clear();
            chkBlock.Items.Clear();

        }
    }
    public DataTable CreateDataTable()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYear.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYear;
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillCBDist();
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


            foreach (ListItem item in chkDistrict.Items)
            {

                item.Selected = true;
                break;
            }
            ddlDistrict_SelectedIndexChanged(chkDistrict, null);
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
    //public void LoadUserLeavel()
    //{
    //    conditions = "";
    //    if (Session["user_level_Role"].ToString() == "1")
    //    {
    //        //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
    //        objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
    //        ddlState.Enabled = true;
    //        ddlDistrict.Enabled = true;
    //    }
    //    else if (Session["user_level_Role"].ToString() == "2")
    //    {
    //        conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
    //        objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

    //        ddlState.SelectedIndex = 1;
    //        ddlState.Enabled = true;
    //        ddlDistrict.Enabled = true;
    //    }
    //    else
    //    {
    //        conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
    //        objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

    //        ddlState.SelectedIndex = 1;
    //        ddlState.Enabled = false;
    //        ddlDistrict.Enabled = false;
    //    }


    //    if (Session["user_level_Role"].ToString() == "1")
    //    {
    //    }
    //    else if (Session["user_level_Role"].ToString() == "2")
    //    {
    //        conditions = "";
    //        conditions = "StateCode ='" + ddlState.SelectedValue + "'  and Fyear= '2017-2018'  ";
    //        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

    //        ddlDistrict.SelectedIndex = 0;

    //        //ddlDistrict.SelectedIndex = 1;
    //        //conditions = "";
    //        //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
    //        //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

    //    }

    //    else
    //    {
    //        conditions = "";
    //        conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and Fyear= '2017-2018' ";
    //        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");
    //        string strQry;

    //        ddlDistrict.SelectedIndex = 1;
    //        ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
    //        //ddlDistrict.SelectedIndex = 1;
    //        //conditions = "";
    //        //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
    //        //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

    //    }





    //}

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillCBBock();
        LoadUser();
    }


    public void ClearGrid()
    {

        gvD2d.DataSource = null;
        gvD2d.DataBind();


    }
    protected void btnSerach1_Click(object sender, EventArgs e)
    {
        LoadReportBOFCReport();
    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        //ViewState["1"] = 1;
        //ClearGrid();

        //gvD2d.Visible = false;
        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            gvD2d.Visible = true;
            gvd2dBo.Visible = false;
            if (Convert.ToInt32(ddlLearningCamp.SelectedValue) == 1)
            {
                LoadReportCamp();
            }
            else
            {
                LoadReport();
            }

        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            gvD2d.Visible = false;
            gvd2dBo.Visible = true;
            LoadReportBO();
        }
        //GenerateExcel();
    }

    protected void btnSerach3_Click(object sender, EventArgs e)
    {
        //ViewState["1"] = 1;
        //ClearGrid();

        //gvD2d.Visible = false;
        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            gvD2d.Visible = true;
            gvd2dBo.Visible = false;
            if (Convert.ToInt32(ddlLearningCamp.SelectedValue) == 1)
            {
                LoadReportAdmin();
            }
            else
            {
                LoadReportAdmin();
            }

        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            gvD2d.Visible = false;
            gvd2dBo.Visible = true;
            LoadReportBOAdmin();
        }
    }


    protected void btnImport_Click(object sender, EventArgs e)
    {
        DataTable dt = Session["MobileUser"] as DataTable;
        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            if (dt != null)
            {
                GenerateExcelNewStringBuld();
                // GenerateExcelNewStringBuldQithoutColour();
            }
        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            if (dt != null)
            {
                GenerateExcelNewStringBuldBO();
            }
        }
    }
    private void GenerateExcelNewStringBuldQithoutColour()
    {
        string abc1 = "";
        string abc2 = "";

        //HttpContext.Current.Response.Clear();
        //HttpContext.Current.Response.ClearContent();
        //HttpContext.Current.Response.ClearHeaders();
        //HttpContext.Current.Response.Buffer = true;
        //HttpContext.Current.Response.ContentType = "application/ms-excel";
        //HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        //string Fullfilename = "" + "Employeetracking" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";

        //sw.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");
        string Fullfilename1 = "" + "FCLoginLogoutReport" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";
        string fileName = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + Fullfilename1 + "");
        StreamWriter sw = new StreamWriter(fileName, false);
        sw.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        sw.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");


        DataTable dt = Session["MobileUser"] as DataTable;

        sw.Write("<table style='border:.5pt solid windowtext;'>");

        sw.Write("<tr>");
        sw.Write("         <td colspan='7' style='text-align:center;'><h1>Timesheet report</h1></td>");
        sw.Write("   </tr>");


        //DateTime FromDate = ConvertToEGDateTime(txtDate.Text);
        //DateTime ToDate = ConvertToEGDateTime(txtTodate.Text);



        //TimeSpan spanTime = (ToDate - FromDate);

        //Int32 totDays = spanTime.Days;




        if (dt.Rows.Count > 0)
        {

            dt.Columns.Add("Startdistance", System.Type.GetType("System.String"));
            dt.Columns.Add("Enddistance", System.Type.GetType("System.String"));
            //retStr += "    <tr>";
            //retStr += "         <td style='border:.5pt solid windowtext;font-weight:700;'>To Date</td>";
            //retStr += "         <td style='border:.5pt solid windowtext;'>" + ToDate.ToShortDateString() + "</td>";
            //retStr += "         <td style='border:.5pt solid windowtext;font-weight:700;'></td>";
            //retStr += "         <td style='border:.5pt solid windowtext;'></td>";
            //retStr += "    </tr>";

            sw.Write("    <tr>");
            sw.Write("         <td style='border:.5pt solid windowtext;font-weight:700;'>Generated On</td>");
            sw.Write("         <td style='border:.5pt solid windowtext;'>" + DateTime.Now.ToString() + "</td>");
            sw.Write("         <td style='border:.5pt solid windowtext;font-weight:700;'></td>");
            sw.Write("         <td style='border:.5pt solid windowtext;'></td>");
            sw.Write("    </tr>");


        }

        String HeaderStyle = "border:.5pt solid windowtext; font-weight:700;";
        sw.Write("    <tr style='font-width:bold;'>");
        sw.Write("         <td style='" + HeaderStyle + "'>#</td>");
        //   sw.Write("         <td style='" + HeaderStyle + "'>TimeSheet ID</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Employee ID</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Employee</td>");

        sw.Write("         <td style='" + HeaderStyle + "'>Type</td>");

        sw.Write("         <td style='" + HeaderStyle + "'>Date</td>");
        //    sw.Write("         <td style='" + HeaderStyle + "'>Employee District</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>District</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Block</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Grampanchayat</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Village ID</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Village</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Entry Date/Time</td>");
        //   sw.Write("         <td style='" + HeaderStyle + "'>Start Entry Time</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Start Entry Location</td>");
        //sw.Write("         <td style='" + HeaderStyle + "'>Start Time</td>");
        //  sw.Write("         <td style='" + HeaderStyle + "'>Start Time Change Reason</td>");
        //sw.Write("         <td style='" + HeaderStyle + "'>End Entry Time</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>End Entry Location</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>End Entry Time</td>");
        //    sw.Write("         <td style='" + HeaderStyle + "'>End Time Change Reason</td>");
        //     sw.Write("         <td style='" + HeaderStyle + "'>Mode</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Hours</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Start Distance</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>END Distance</td>");

        sw.Write("         <td style='" + HeaderStyle + "'>User District</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>User Block</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Cluster Name</td>");

        //sw.Write("         <td style='" + HeaderStyle + "'>Version No</td>");
        sw.Write("    </tr>");



        String DataStyle = "border:.5pt solid windowtext;";
        String DateTimeStyle = "mso-number-format:\"mm/dd/yyyy hh:mm AM/PM \";";
        String DateStyle = "mso-number-format:\"mm/dd/yyyy \";";
        String TimeStyle = "mso-number-format:\"hh:mm AM/PM \";";
        String InvalidGeoLocationStype = "background-color:red;";
        String ValidGeoLocationStype = "background-color:#99FF66;";

        var i = 0;
        double distance = 0;
        double distance1 = 0;
        double Enddistance = 0;
        for (i = 0; i < dt.Rows.Count; i++)
        {

            var RowStyle = DataStyle;
            var RowStyle1 = DataStyle;

            Int32 dHours = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Hours;
            Int32 dMins = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Minutes;

            TimeSpan dayJob = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"])).Subtract(Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"]));

            String villageGeoData = dt.Rows[i]["Village_GeoLocation"].ToString();
            abc1 = null;
            abc2 = null;
            if (villageGeoData != null && villageGeoData != "" && villageGeoData != "[]")
            {
                if (dt.Rows[i]["StarttimeLocation"].ToString() == "" || villageGeoData.Length < 100 || dt.Rows[i]["StarttimeLocation"].ToString() == null || dt.Rows[i]["StarttimeLocation"].ToString() == "Unsupported browser" || dt.Rows[i]["StarttimeLocation"].ToString() == "User denied" || dt.Rows[i]["StarttimeLocation"].ToString() == "Location unavailable" || dt.Rows[i]["StarttimeLocation"].ToString() == "Request timed out" || dt.Rows[i]["StarttimeLocation"].ToString() == "Unknown error" || dt.Rows[i]["StarttimeLocation"].ToString() == "GPS turned off")
                {

                }
                else
                {

                    GeoUtils.GeoPointChecker geoChecker = new GeoUtils.GeoPointChecker(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData);
                    //if (geoChecker.isValid() == false && dt.Rows[i]["StarttimeLocation"].ToString().Length > 4 && dt.Rows[i]["EndtimeLocation"].ToString().ToString().Length > 4)

                    if (dt.Rows[i]["StarttimeLocation"].ToString().Length > 4 && dt.Rows[i]["EndtimeLocation"].ToString().ToString().Length > 4)
                    {

                        if (geoChecker.isValid() == false)
                        {
                            distance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData), 2);
                            //double ddd = distance - distance1;

                            if (distance > 4)
                            {
                                RowStyle += InvalidGeoLocationStype;
                                abc1 = InvalidGeoLocationStype;
                            }
                            else
                            {
                                RowStyle += ValidGeoLocationStype;
                                abc1 = ValidGeoLocationStype;
                            }
                        }
                        else
                        {
                            //string GUID = (GeoUtils.GeoPointChecker.abcdNew(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData));
                            //distance1 = Math.Round(GeoUtils.GeoPointChecker.abcd(GUID, villageGeoData), 2);

                            distance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData), 2);
                            //double ddd = distance - distance1;

                            //if (ddd > 4)
                            //{
                            //    RowStyle += InvalidGeoLocationStype;
                            //    abc1 = InvalidGeoLocationStype;
                            //}
                            //else
                            //{
                            //    RowStyle += ValidGeoLocationStype;
                            //    abc1 = ValidGeoLocationStype;
                            //}
                            RowStyle += ValidGeoLocationStype;
                            abc1 = ValidGeoLocationStype;
                        }
                    }
                    else
                    {

                        RowStyle += DataStyle;
                        abc1 = DataStyle;
                    }

                    GeoUtils.GeoPointChecker geoChecker1 = new GeoUtils.GeoPointChecker(dt.Rows[i]["EndtimeLocation"].ToString(), villageGeoData);
                    if (dt.Rows[i]["StarttimeLocation"].ToString().Length > 4 && dt.Rows[i]["EndtimeLocation"].ToString().ToString().Length > 4)
                    {
                        Enddistance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["EndtimeLocation"].ToString(), villageGeoData), 2);

                        abc2 = ValidGeoLocationStype;
                    }
                    else
                    {


                        abc2 = InvalidGeoLocationStype;
                    }
                }
            }


            sw.Write("<tr>");
            sw.Write("<td style='" + RowStyle1 + "'>" + (i + 1) + "</td>");
            //retStr += "<td style='" + RowStyle + "'>" + mData.TimeSheet_ID + "</td>");
            sw.Write("<td style='" + RowStyle1 + "'>" + dt.Rows[i]["UserName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle1 + "'>" + dt.Rows[i]["FristName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle1 + "'>" + dt.Rows[i]["Role"].ToString() + "</td>");

            sw.Write("<td style='" + RowStyle1 + DateStyle + "'>" + Convert.ToDateTime(dt.Rows[i]["Date"].ToString()).ToString("dd/MM/yyy") + "</td>");
            //    sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["EmployeeDistrictName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle1 + "'>" + dt.Rows[i]["DistrictName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle1 + "'>" + dt.Rows[i]["BlockName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle1 + "'>" + dt.Rows[i]["PanchayatName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle1 + "'>" + dt.Rows[i]["VillageCode"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle1 + "'>" + dt.Rows[i]["VillageName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle1 + "'>" + dt.Rows[i]["TimeSheet_StartTime"].ToString() + "</td>");
            //   sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_StartTime"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle1 + "'>" + dt.Rows[i]["StarttimeLocation"].ToString() + "</td>");


            if (abc1 == "background-color:red);")
            {
                //sw.Write("<td style='" + RowStyle + "'>" + Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"].ToString()) + "</td>");

            }
            else if (abc1 == ValidGeoLocationStype)
            {
                if (dt.Rows[i]["EndtimeLocation"].ToString() == "GPS turned off")
                {
                    sw.Write("<td style='" + RowStyle1 + "'>" + "" + "</td>");

                }
                else
                {
                    sw.Write("<td style='" + RowStyle1 + "'>" + dt.Rows[i]["EndtimeLocation"].ToString() + "</td>");
                }

            }

            else
            {
                sw.Write("<td style='" + RowStyle1 + "'>" + dt.Rows[i]["EndtimeLocation"].ToString() + "</td>");
            }
            //retStr += "<td style='" + RowStyle + "'>" + mData.TimeSheet_EndLocation + "</td>";
            sw.Write("<td style='" + RowStyle1 + TimeStyle + "'>" + Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"].ToString()).ToShortTimeString() + "</td>");
            // sw.Write("<td style='" + RowStyle + "'>" + mData.TimeSheet_EndTimeReasonText + "</td>";
            //sw.Write("<td style='" + RowStyle + "'>" + mData.TimeSheet_Mode + "</td>";
            //retStr += "<td style='" + DataStyle + "'>" + dHours.ToString() + ":" + dMins.ToString() + "</td>";
            sw.Write("<td style='" + RowStyle1 + "mso-number-format:\"[hh]:mm\";'>" + dayJob.Hours + ":" + dayJob.Minutes + "</td>");
            dt.Rows[i]["TotalHours"] = dayJob.Hours + ":" + dayJob.Minutes;
            //if (abc1 == "background-color:red;")
            //if (abc1 == "background-color:#99FF66")


            if (abc1 == "background-color:#99FF66;")
            {
                sw.Write("<td style='" + RowStyle1 + "'>" + distance + "KM</td>");
                dt.Rows[i]["Startdistance"] = distance;
            }
            else if (abc1 == "background-color:red;")
            {
                sw.Write("<td style='" + RowStyle1 + "'>" + distance + "KM</td>");
                //sw.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");
                dt.Rows[i]["Startdistance"] = distance;
            }
            else if (abc1 == "border:.5pt solid windowtext;")
            {
                //sw.Write("<td style='" + RowStyle + "'>" + distance + "KM</td>");
                sw.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");

            }
            else
            {
                sw.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");
            }

            if (abc2 == "background-color:#99FF66;")
            {
                sw.Write("<td style='" + RowStyle1 + "'>" + Enddistance + "KM</td>");
                dt.Rows[i]["Enddistance"] = distance;
            }
            else if (abc2 == "background-color:red;")
            {
                //sw.Write("<td style='" + RowStyle + "'>" + "NA" + "KM</td>");
                sw.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");

            }
            else if (abc2 == "border:.5pt solid windowtext;")
            {
                //sw.Write("<td style='" + RowStyle + "'>" + distance + "KM</td>");
                sw.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");

            }
            else
            {
                sw.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");
            }
            //    sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["VersionCodeNo"].ToString() + "</td>");
            distance = 0;

            Enddistance = 0;
            sw.Write("<td style='" + RowStyle1 + "'>" + dt.Rows[i]["UserDist"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle1 + "'>" + dt.Rows[i]["UserBlock"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle1 + "'>" + dt.Rows[i]["ClusterName"].ToString() + "</td>");
            sw.Write("</tr>");


        }

        DataStyle += "";

        sw.Write("</table>");

        sw.Close();



        FileStream fs = null;//, fs2=null;
        try
        {
            string path1 = Fullfilename1;
            string foldername = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + path1 + "");
            string datafolder = path1.Substring(0, path1.Length - 4);
            //  string[] file = Directory.GetFiles(foldername);
            string path = foldername;
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
            Response.AddHeader("Content-disposition", "attachment; filename=" + datafolder + "" + ".zip");
            Response.ContentType = "application/octet-stream";
            Response.BinaryWrite(bytBytes);

            dt.Columns.Remove("Village_GeoLocation");
            ExportToCSVFile(dt, "FCLoginLogoutReport");



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
        //flushExcel(totDays.ToString());

        //var lq=dataList.Select(fld=>fld.TimeSheet_EndTime.

    }
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
    private void GenerateExcelNewStringBuld()
    {
        string abc1 = "";
        string abc2 = "";
        //HttpContext.Current.Response.Clear();
        //HttpContext.Current.Response.ClearContent();
        //HttpContext.Current.Response.ClearHeaders();
        //HttpContext.Current.Response.Buffer = true;
        //HttpContext.Current.Response.ContentType = "application/ms-excel";
        //HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        //string Fullfilename = "" + "Employeetracking" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";

        //sw.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");
        string Fullfilename1 = "" + "FCLoginLogoutReport" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";
        string fileName = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + Fullfilename1 + "");
        StreamWriter sw = new StreamWriter(fileName, false);
        sw.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        sw.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");


        DataTable dt = Session["MobileUser"] as DataTable;

        sw.Write("<table style='border:.5pt solid windowtext;'>");

        sw.Write("<tr>");
        sw.Write("         <td colspan='7' style='text-align:center;'><h1>Timesheet report</h1></td>");
        sw.Write("   </tr>");


        //DateTime FromDate = ConvertToEGDateTime(txtDate.Text);
        //DateTime ToDate = ConvertToEGDateTime(txtTodate.Text);



        //TimeSpan spanTime = (ToDate - FromDate);

        //Int32 totDays = spanTime.Days;




        if (dt.Rows.Count > 0)
        {



            //retStr += "    <tr>";
            //retStr += "         <td style='border:.5pt solid windowtext;font-weight:700;'>To Date</td>";
            //retStr += "         <td style='border:.5pt solid windowtext;'>" + ToDate.ToShortDateString() + "</td>";
            //retStr += "         <td style='border:.5pt solid windowtext;font-weight:700;'></td>";
            //retStr += "         <td style='border:.5pt solid windowtext;'></td>";
            //retStr += "    </tr>";

            sw.Write("    <tr>");
            sw.Write("         <td style='border:.5pt solid windowtext;font-weight:700;'>Generated On</td>");
            sw.Write("         <td style='border:.5pt solid windowtext;'>" + DateTime.Now.ToString() + "</td>");
            sw.Write("         <td style='border:.5pt solid windowtext;font-weight:700;'></td>");
            sw.Write("         <td style='border:.5pt solid windowtext;'></td>");
            sw.Write("    </tr>");


        }

        String HeaderStyle = "border:.5pt solid windowtext; font-weight:700;background:#D9D9D9;";
        sw.Write("    <tr style='font-width:bold;'>");
        sw.Write("         <td style='" + HeaderStyle + "'>#</td>");
        //   sw.Write("         <td style='" + HeaderStyle + "'>TimeSheet ID</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Employee ID</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Employee</td>");

        sw.Write("         <td style='" + HeaderStyle + "'>Type</td>");

        sw.Write("         <td style='" + HeaderStyle + "'>Date</td>");
        //    sw.Write("         <td style='" + HeaderStyle + "'>Employee District</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>District</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Block</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Grampanchayat</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Village ID</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Village</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Entry Date/Time</td>");
        //   sw.Write("         <td style='" + HeaderStyle + "'>Start Entry Time</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Start Entry Location</td>");
        //sw.Write("         <td style='" + HeaderStyle + "'>Start Time</td>");
        //  sw.Write("         <td style='" + HeaderStyle + "'>Start Time Change Reason</td>");
        //sw.Write("         <td style='" + HeaderStyle + "'>End Entry Time</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>End Entry Location</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>End Entry Time</td>");
        //    sw.Write("         <td style='" + HeaderStyle + "'>End Time Change Reason</td>");
        //     sw.Write("         <td style='" + HeaderStyle + "'>Mode</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Hours</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Start Distance</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>END Distance</td>");

        sw.Write("         <td style='" + HeaderStyle + "'>User District</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>User Block</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Cluster Name</td>");

        //sw.Write("         <td style='" + HeaderStyle + "'>Version No</td>");
        sw.Write("    </tr>");



        String DataStyle = "border:.5pt solid windowtext;";
        String DateTimeStyle = "mso-number-format:\"mm/dd/yyyy hh:mm AM/PM \";";
        String DateStyle = "mso-number-format:\"mm/dd/yyyy \";";
        String TimeStyle = "mso-number-format:\"hh:mm AM/PM \";";
        String InvalidGeoLocationStype = "background-color:red;";
        String ValidGeoLocationStype = "background-color:#99FF66;";

        var i = 0;
        double distance = 0;
        double distance1 = 0;
        double Enddistance = 0;
        for (i = 0; i < dt.Rows.Count; i++)
        {

            var RowStyle = DataStyle;

            Int32 dHours = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Hours;
            Int32 dMins = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Minutes;

            TimeSpan dayJob = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"])).Subtract(Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"]));

            String villageGeoData = dt.Rows[i]["Village_GeoLocation"].ToString();
            abc1 = null;
            abc2 = null;
            if (villageGeoData != null && villageGeoData != "" && villageGeoData != "[]")
            {
                if (dt.Rows[i]["StarttimeLocation"].ToString() == "" || villageGeoData.Length < 100 || dt.Rows[i]["StarttimeLocation"].ToString() == null || dt.Rows[i]["StarttimeLocation"].ToString() == "Unsupported browser" || dt.Rows[i]["StarttimeLocation"].ToString() == "User denied" || dt.Rows[i]["StarttimeLocation"].ToString() == "Location unavailable" || dt.Rows[i]["StarttimeLocation"].ToString() == "Request timed out" || dt.Rows[i]["StarttimeLocation"].ToString() == "Unknown error" || dt.Rows[i]["StarttimeLocation"].ToString() == "GPS turned off")
                {

                }
                else
                {

                    GeoUtils.GeoPointChecker geoChecker = new GeoUtils.GeoPointChecker(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData);
                    //if (geoChecker.isValid() == false && dt.Rows[i]["StarttimeLocation"].ToString().Length > 4 && dt.Rows[i]["EndtimeLocation"].ToString().ToString().Length > 4)

                    if (dt.Rows[i]["StarttimeLocation"].ToString().Length > 4 && dt.Rows[i]["EndtimeLocation"].ToString().ToString().Length > 4)
                    {

                        if (geoChecker.isValid() == false)
                        {
                            distance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData), 2);
                            //double ddd = distance - distance1;

                            if (distance > 4)
                            {
                                RowStyle += InvalidGeoLocationStype;
                                abc1 = InvalidGeoLocationStype;
                            }
                            else
                            {
                                RowStyle += ValidGeoLocationStype;
                                abc1 = ValidGeoLocationStype;
                            }
                        }
                        else
                        {
                            //string GUID = (GeoUtils.GeoPointChecker.abcdNew(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData));
                            //distance1 = Math.Round(GeoUtils.GeoPointChecker.abcd(GUID, villageGeoData), 2);

                            distance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData), 2);
                            //double ddd = distance - distance1;

                            //if (ddd > 4)
                            //{
                            //    RowStyle += InvalidGeoLocationStype;
                            //    abc1 = InvalidGeoLocationStype;
                            //}
                            //else
                            //{
                            //    RowStyle += ValidGeoLocationStype;
                            //    abc1 = ValidGeoLocationStype;
                            //}
                            RowStyle += ValidGeoLocationStype;
                            abc1 = ValidGeoLocationStype;
                        }
                    }
                    else
                    {

                        RowStyle += DataStyle;
                        abc1 = DataStyle;
                    }

                    GeoUtils.GeoPointChecker geoChecker1 = new GeoUtils.GeoPointChecker(dt.Rows[i]["EndtimeLocation"].ToString(), villageGeoData);
                    if (dt.Rows[i]["StarttimeLocation"].ToString().Length > 4 && dt.Rows[i]["EndtimeLocation"].ToString().ToString().Length > 4)
                    {
                        Enddistance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["EndtimeLocation"].ToString(), villageGeoData), 2);

                        abc2 = ValidGeoLocationStype;
                    }
                    else
                    {


                        abc2 = InvalidGeoLocationStype;
                    }
                }
            }


            sw.Write("<tr>");
            sw.Write("<td style='" + RowStyle + "'>" + (i + 1) + "</td>");
            //retStr += "<td style='" + RowStyle + "'>" + mData.TimeSheet_ID + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["UserName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["FristName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["Role"].ToString() + "</td>");

            sw.Write("<td style='" + RowStyle + DateStyle + "'>" + Convert.ToDateTime(dt.Rows[i]["Date"].ToString()).ToString("dd/MM/yyy") + "</td>");
            //    sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["EmployeeDistrictName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["DistrictName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["BlockName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["PanchayatName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["VillageCode"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["VillageName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_StartTime"].ToString() + "</td>");
            //   sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_StartTime"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["StarttimeLocation"].ToString() + "</td>");


            if (abc1 == "background-color:red);")
            {
                //sw.Write("<td style='" + RowStyle + "'>" + Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"].ToString()) + "</td>");

            }
            else if (abc1 == ValidGeoLocationStype)
            {
                if (dt.Rows[i]["EndtimeLocation"].ToString() == "GPS turned off")
                {
                    sw.Write("<td style='" + RowStyle + "'>" + "" + "</td>");

                }
                else
                {
                    sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["EndtimeLocation"].ToString() + "</td>");
                }

            }

            else
            {
                sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["EndtimeLocation"].ToString() + "</td>");
            }
            //retStr += "<td style='" + RowStyle + "'>" + mData.TimeSheet_EndLocation + "</td>";
            sw.Write("<td style='" + RowStyle + TimeStyle + "'>" + Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"].ToString()).ToShortTimeString() + "</td>");
            // sw.Write("<td style='" + RowStyle + "'>" + mData.TimeSheet_EndTimeReasonText + "</td>";
            //sw.Write("<td style='" + RowStyle + "'>" + mData.TimeSheet_Mode + "</td>";
            //retStr += "<td style='" + DataStyle + "'>" + dHours.ToString() + ":" + dMins.ToString() + "</td>";
            sw.Write("<td style='" + RowStyle + "mso-number-format:\"[hh]:mm\";'>" + dayJob.Hours + ":" + dayJob.Minutes + "</td>");
            //if (abc1 == "background-color:red;")
            //if (abc1 == "background-color:#99FF66")
            if (abc1 == "background-color:#99FF66;")
            {
                sw.Write("<td style='" + RowStyle + "'>" + distance + "KM</td>");

            }
            else if (abc1 == "background-color:red;")
            {
                sw.Write("<td style='" + RowStyle + "'>" + distance + "KM</td>");
                //sw.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");

            }
            else if (abc1 == "border:.5pt solid windowtext;")
            {
                //sw.Write("<td style='" + RowStyle + "'>" + distance + "KM</td>");
                sw.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");

            }
            else
            {
                sw.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");
            }

            if (abc2 == "background-color:#99FF66;")
            {
                sw.Write("<td style='" + RowStyle + "'>" + Enddistance + "KM</td>");

            }
            else if (abc2 == "background-color:red;")
            {
                //sw.Write("<td style='" + RowStyle + "'>" + "NA" + "KM</td>");
                sw.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");

            }
            else if (abc2 == "border:.5pt solid windowtext;")
            {
                //sw.Write("<td style='" + RowStyle + "'>" + distance + "KM</td>");
                sw.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");

            }
            else
            {
                sw.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");
            }
            //    sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["VersionCodeNo"].ToString() + "</td>");
            distance = 0;

            Enddistance = 0;
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["UserDist"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["UserBlock"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["ClusterName"].ToString() + "</td>");
            sw.Write("</tr>");


        }

        DataStyle += "background-color:yellow;";

        sw.Write("</table>");

        sw.Close();



        FileStream fs = null;//, fs2=null;
        try
        {
            string path1 = Fullfilename1;
            string foldername = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + path1 + "");
            string datafolder = path1.Substring(0, path1.Length - 4);
            //  string[] file = Directory.GetFiles(foldername);
            string path = foldername;
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
        //flushExcel(totDays.ToString());

        //var lq=dataList.Select(fld=>fld.TimeSheet_EndTime.

    }

    private void GenerateExcelNew()
    {
        string abc1 = "";
        string abc2 = "";
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        string Fullfilename = "" + "Employeetracking" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";

        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");


        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        // Int32 EmpID = Convert.ToInt32(Contx.Request["empid"]);

        DataTable dt = Session["MobileUser"] as DataTable;

        HttpContext.Current.Response.Write("<table style='border:.5pt solid windowtext;'>");

        HttpContext.Current.Response.Write("<tr>");
        HttpContext.Current.Response.Write("         <td colspan='7' style='text-align:center;'><h1>Timesheet report</h1></td>");
        HttpContext.Current.Response.Write("   </tr>");


        //DateTime FromDate = ConvertToEGDateTime(txtDate.Text);
        //DateTime ToDate = ConvertToEGDateTime(txtTodate.Text);



        //TimeSpan spanTime = (ToDate - FromDate);

        //Int32 totDays = spanTime.Days;




        if (dt.Rows.Count > 0)
        {



            //retStr += "    <tr>";
            //retStr += "         <td style='border:.5pt solid windowtext;font-weight:700;'>To Date</td>";
            //retStr += "         <td style='border:.5pt solid windowtext;'>" + ToDate.ToShortDateString() + "</td>";
            //retStr += "         <td style='border:.5pt solid windowtext;font-weight:700;'></td>";
            //retStr += "         <td style='border:.5pt solid windowtext;'></td>";
            //retStr += "    </tr>";

            HttpContext.Current.Response.Write("    <tr>");
            HttpContext.Current.Response.Write("         <td style='border:.5pt solid windowtext;font-weight:700;'>Generated On</td>");
            HttpContext.Current.Response.Write("         <td style='border:.5pt solid windowtext;'>" + DateTime.Now.ToString() + "</td>");
            HttpContext.Current.Response.Write("         <td style='border:.5pt solid windowtext;font-weight:700;'></td>");
            HttpContext.Current.Response.Write("         <td style='border:.5pt solid windowtext;'></td>");
            HttpContext.Current.Response.Write("    </tr>");


        }

        String HeaderStyle = "border:.5pt solid windowtext; font-weight:700;background:#D9D9D9;";
        HttpContext.Current.Response.Write("    <tr style='font-width:bold;'>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>#</td>");
        //   HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>TimeSheet ID</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Employee ID</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Employee</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Type</td>");

        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Date</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>District</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Block</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Grampanchayat</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Village ID</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Village</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Entry Date/Time</td>");
        //   HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Start Entry Time</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Start Entry Location</td>");
        //HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Start Time</td>");
        //  HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Start Time Change Reason</td>");
        //HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>End Entry Time</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>End Entry Location</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>End Entry Time</td>");
        //    HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>End Time Change Reason</td>");
        //     HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Mode</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Hours</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Start Distance</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>END Distance</td>");
        //HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Version No</td>");
        HttpContext.Current.Response.Write("    </tr>");



        String DataStyle = "border:.5pt solid windowtext;";
        String DateTimeStyle = "mso-number-format:\"mm/dd/yyyy hh:mm AM/PM \";";
        String DateStyle = "mso-number-format:\"mm/dd/yyyy \";";
        String TimeStyle = "mso-number-format:\"hh:mm AM/PM \";";
        String InvalidGeoLocationStype = "background-color:red;";
        String ValidGeoLocationStype = "background-color:#99FF66;";

        var i = 0;
        double distance = 0;
        double Enddistance = 0;
        for (i = 0; i < dt.Rows.Count; i++)
        {

            var RowStyle = DataStyle;

            Int32 dHours = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Hours;
            Int32 dMins = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Minutes;

            TimeSpan dayJob = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"])).Subtract(Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"]));

            String villageGeoData = dt.Rows[i]["Village_GeoLocation"].ToString();
            abc1 = null;
            abc2 = null;
            if (villageGeoData != null && villageGeoData != "" && villageGeoData != "[]")
            {
                if (dt.Rows[i]["StarttimeLocation"].ToString() == "" || villageGeoData.Length < 100 || dt.Rows[i]["StarttimeLocation"].ToString() == null || dt.Rows[i]["StarttimeLocation"].ToString() == "Unsupported browser" || dt.Rows[i]["StarttimeLocation"].ToString() == "User denied" || dt.Rows[i]["StarttimeLocation"].ToString() == "Location unavailable" || dt.Rows[i]["StarttimeLocation"].ToString() == "Request timed out" || dt.Rows[i]["StarttimeLocation"].ToString() == "Unknown error" || dt.Rows[i]["StarttimeLocation"].ToString() == "GPS turned off")
                {

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
                            RowStyle += InvalidGeoLocationStype;
                            abc1 = InvalidGeoLocationStype;
                        }
                        else
                        {
                            RowStyle += ValidGeoLocationStype;
                            abc1 = ValidGeoLocationStype;
                        }
                    }
                    else
                    {

                        RowStyle += DataStyle;
                        abc1 = DataStyle;
                    }

                    GeoUtils.GeoPointChecker geoChecker1 = new GeoUtils.GeoPointChecker(dt.Rows[i]["EndtimeLocation"].ToString(), villageGeoData);
                    if (dt.Rows[i]["StarttimeLocation"].ToString().Length > 4 && dt.Rows[i]["EndtimeLocation"].ToString().ToString().Length > 4)
                    {
                        Enddistance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["EndtimeLocation"].ToString(), villageGeoData), 2);

                        abc2 = ValidGeoLocationStype;
                    }
                    else
                    {


                        abc2 = InvalidGeoLocationStype;
                    }
                }
            }


            HttpContext.Current.Response.Write("<tr>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + (i + 1) + "</td>");
            //retStr += "<td style='" + RowStyle + "'>" + mData.TimeSheet_ID + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["UserName"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["FristName"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["Role"].ToString() + "</td>");

            HttpContext.Current.Response.Write("<td style='" + RowStyle + DateStyle + "'>" + Convert.ToDateTime(dt.Rows[i]["Date"].ToString()).ToString("dd/MM/yyy") + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["DistrictName"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["BlockName"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["PanchayatName"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["VillageCode"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["VillageName"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_StartTime"].ToString() + "</td>");
            //   HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_StartTime"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["StarttimeLocation"].ToString() + "</td>");
            //HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_EndTime"].ToString() + "</td>");
            //HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + mData.TimeSheet_StartTimeReasonText + "</td>");
            //HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_EndTime"].ToString() + "</td>");
            if (abc1 == "background-color:red);")
            {
                //HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"].ToString()) + "</td>");

            }
            else if (abc1 == ValidGeoLocationStype)
            {
                if (dt.Rows[i]["EndtimeLocation"].ToString() == "GPS turned off")
                {
                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");

                }
                else
                {
                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["EndtimeLocation"].ToString() + "</td>");
                }

            }

            else
            {
                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["EndtimeLocation"].ToString() + "</td>");
            }
            //retStr += "<td style='" + RowStyle + "'>" + mData.TimeSheet_EndLocation + "</td>";
            HttpContext.Current.Response.Write("<td style='" + RowStyle + TimeStyle + "'>" + Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"].ToString()).ToShortTimeString() + "</td>");
            // HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + mData.TimeSheet_EndTimeReasonText + "</td>";
            //HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + mData.TimeSheet_Mode + "</td>";
            //retStr += "<td style='" + DataStyle + "'>" + dHours.ToString() + ":" + dMins.ToString() + "</td>";
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "mso-number-format:\"[hh]:mm\";'>" + dayJob.Hours + ":" + dayJob.Minutes + "</td>");
            //if (abc1 == "background-color:red;")
            //if (abc1 == "background-color:#99FF66")
            if (abc1 == "background-color:#99FF66;")
            {
                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + distance + "KM</td>");

            }
            else if (abc1 == "background-color:red;")
            {
                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + distance + "KM</td>");
                //HttpContext.Current.Response.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");

            }
            else if (abc1 == "border:.5pt solid windowtext;")
            {
                //HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + distance + "KM</td>");
                HttpContext.Current.Response.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");

            }
            else
            {
                HttpContext.Current.Response.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");
            }

            if (abc2 == "background-color:#99FF66;")
            {
                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + Enddistance + "KM</td>");

            }
            else if (abc2 == "background-color:red;")
            {
                //HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "NA" + "KM</td>");
                HttpContext.Current.Response.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");

            }
            else if (abc2 == "border:.5pt solid windowtext;")
            {
                //HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + distance + "KM</td>");
                HttpContext.Current.Response.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");

            }
            else
            {
                HttpContext.Current.Response.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");
            }
            //    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["VersionCodeNo"].ToString() + "</td>");
            distance = 0;

            Enddistance = 0;
            HttpContext.Current.Response.Write("</tr>");

        }

        DataStyle += "background-color:yellow;";

        HttpContext.Current.Response.Write("</table>");
        string Fullfilename1 = "" + "Latlong" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";
        string fileName = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + Fullfilename + "");
        //HttpContext.Current.Response.AddHeader("Content-Disposition", "inline; filename=" + Fullfilename1);
        //HttpContext.Current.Response.AddHeader("Content-Type", "application/octet-stream");
        ////HttpContext.Current.Response.Flush();
        //HttpContext.Current.Response.End();

        Response.TransmitFile(fileName);
        // Response.WriteFile(fileName);



        //FileStream fs = null;//, fs2=null;
        //try
        //{
        //    string path1 = Fullfilename;
        //    string foldername = Server.MapPath(Comman.GetImagePath("DataBackupPath") +"/" + path1 + "");
        //    string datafolder = path1.Substring(0, path1.Length - 4);
        //    //  string[] file = Directory.GetFiles(foldername);

        //    string fullPath = Request.MapPath("~/DataBackup/" + datafolder + "" + ".zip");
        //    using (ZipFile zip = new ZipFile())
        //    {
        //        zip.AddFile(foldername, "");
        //        //    zip.AddFiles(file, foldername);
        //        zip.Save(Server.MapPath(Comman.GetImagePath("DataBackupPath")) +"/" + datafolder + "" + ".zip"));
        //    }



        //    HttpResponse Response = HttpContext.Current.Response; Response.Clear(); Response.ClearHeaders(); Response.Charset = "UTF-8";
        //    fs = File.Open(fullPath, FileMode.Open);
        //    byte[] bytBytes = new byte[(fs.Length)];
        //    fs.Read(bytBytes, 0, (int)fs.Length);
        //    fs.Close();
        //    Response.AddHeader("Content-disposition", "attachment; filename=" + fullPath);
        //    Response.ContentType = "application/octet-stream";
        //    Response.BinaryWrite(bytBytes);






        //    if (File.Exists(path))
        //    {
        //        System.IO.File.Delete(path);
        //    }
        //    if (File.Exists(fullPath))
        //    {
        //        System.IO.File.Delete(fullPath);
        //    }

        //    Response.Flush();
        //    HttpContext.Current.ApplicationInstance.CompleteRequest();
        //    Response.End();
        //}

        //catch (System.Exception ex)
        //{
        //    //  Server.Transfer("default.aspx", false);
        //    Response.Clear();

        //    //string mmsg = ex.Message;
        //    //showEXPMessages("(crateZip)  " + mmsg); //showMessages(mmsg);
        //}
        //finally
        //{
        //    fs.Dispose();
        //    Response.Clear();

        //}
        //flushExcel(totDays.ToString());

        //var lq=dataList.Select(fld=>fld.TimeSheet_EndTime.

    }

    public void LoadReport()
    {

        conditions = "";
        string conditions1 = "where 1=1 ";
        lblTotalCount.Text = "";
        conditions = "where 1=1  ";
        string ddlDistrict = "";

        string ddlState = "";
        string ddlBlock = "";

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

        //  if (ddlYear.SelectedIndex > 0)
        //{
        //    conditions += " and mst5Village.Fyear= '" + ddlYear.SelectedItem.Text + "' ";
        //}
        if (Session["FinYear"].ToString() == ddlYear.SelectedItem.Text)
        {
            if (ddlState.Length > 0)
            {
                conditions += "  and StateCode in( " + ddlState + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and DistrictCode in(" + ddlDistrict + ") ";

            }
            if (ddlBlock.Length > 0)
            {
                conditions += " and BlockCode in(" + ddlBlock + ") ";

            }

            if (ddlUser.SelectedIndex > 0)
            {
                conditions += " and mstuser.UserId =  '" + ddlUser.SelectedValue + "' ";

            }

        }

        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2025)
        {

            if (ddlState.Length > 0)
            {
                conditions += "  and StateCode in( " + ddlState + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and DistrictCode in(" + ddlDistrict + ") ";

            }
            if (ddlBlock.Length > 0)
            {
                conditions += " and BlockCode in(" + ddlBlock + ") ";

            }

            if (ddlUser.SelectedIndex > 0)
            {
                conditions += " and mstuser2026.UserId =  '" + ddlUser.SelectedValue + "' ";

            }
        }

        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2024)
        {

            if (ddlState.Length > 0)
            {
                conditions += "  and StateCode in( " + ddlState + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and DistrictCode in(" + ddlDistrict + ") ";

            }
            if (ddlBlock.Length > 0)
            {
                conditions += " and BlockCode in(" + ddlBlock + ") ";

            }

            if (ddlUser.SelectedIndex > 0)
            {
                conditions += " and mstuser2025.UserId =  '" + ddlUser.SelectedValue + "' ";

            }
        }




        if (txtDate.Text != "" && txtTodate.Text != "")
        {
            if (Convert.ToDateTime(txtDate.Text) == Convert.ToDateTime(txtTodate.Text))
            {


                if (Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd") == Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd"))
                {


                    DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                    DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                    conditions1 += " and year(Date)=" + DateTime.Now.Year + " and  month(Date)=" + DateTime.Now.Month + " and  day(Date)=" + DateTime.Now.Day + "";
                }
                else
                {
                    DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                    DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                    string[] multiArray = txtDate.Text.Split(new Char[] { '/' });
                    string[] multiArray1 = txtTodate.Text.Split(new Char[] { '/' });

                    string Fdate = Fromdate.Year + "" + multiArray[1] + "" + multiArray[0];
                    string Tdate = Todate.Year + "" + multiArray1[1] + "" + multiArray1[0];
                    conditions1 += " and (Year([Date])*10000)+(Month([Date])*100+Day([Date])) Between '" + Fdate + "' and '" + Tdate + "'";
                }
            }
            else
            {
                DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                string[] multiArray = txtDate.Text.Split(new Char[] { '/' });
                string[] multiArray1 = txtTodate.Text.Split(new Char[] { '/' });

                string Fdate = Fromdate.Year + "" + multiArray[1] + "" + multiArray[0];
                string Tdate = Todate.Year + "" + multiArray1[1] + "" + multiArray1[0];
                conditions1 += " and (Year([Date])*10000)+(Month([Date])*100+Day([Date])) Between '" + Fdate + "' and '" + Tdate + "'";

                // conditions1 += " and Date BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
            }
        }
        //    else
        //    {
        //        DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
        //        DateTime Todate = Convert.ToDateTime(txtTodate.Text);
        //        conditions1 += " and Date BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
        //    }
        //}

        if (ddlYear.SelectedIndex > 0)
        {
            string Year = ddlYear.SelectedItem.Text;
            string[] Year1 = Year.Split('-');
            conditions1 += "    And Date >= '" + Year1[0] + "-04-01' and Date<='" + Year1[1] + "-03-31'";


        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions1 += " and mst5Village.Fyear= '" + ddlYear.SelectedItem.Text + "' ";
        }
        string mainCon = conditions + conditions1;
        DataTable dt = null;
        //DataTable dt = objMain.tblReport("", "", "", mainCon);
        if (Session["FinYear"].ToString() == ddlYear.SelectedItem.Text)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {

                new SqlParameter("@condtion", conditions),
            new SqlParameter("@condtion1", conditions1),


            };

            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTblUserLoginNew2023]", cmdParameters);
        }

        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2025)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
               {

                new SqlParameter("@condtion", conditions),
            new SqlParameter("@condtion1", conditions1),


               };

            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTblUserLoginNew20242025]", cmdParameters);


        }
        else
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
           {

                new SqlParameter("@condtion", conditions),
            new SqlParameter("@condtion1", conditions1),


           };

            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTblUserLoginNew2023Back]", cmdParameters);

        }
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows.Count > 300)
            {
                Session["MobileUser"] = dt;
                lblTotalCount.Text = (dt.Rows.Count).ToString();
                btnCSV_Click(lnkCSV, null);


            }
            else
            {
                Session["MobileUser"] = dt;
                gvD2d.DataSource = dt;

                gvD2d.DataBind();


                lblTotalCount.Text = (dt.Rows.Count).ToString();
                //  LinkButton1.Visible = true;
                lnkCSV.Visible = true;
            }
        }
        else
        {
            //LinkButton1.Visible = false;
            lnkCSV.Visible = false;
            gvD2d.DataSource = null;
            gvD2d.DataBind();
            lblTotalCount.Text = "";
        }



    }

    public void LoadReportCamp()
    {

        conditions = "";
        string conditions1 = "where 1=1 ";
        lblTotalCount.Text = "";
        conditions = "where 1=1  ";
        string ddlDistrict = "";

        string ddlState = "";
        string ddlBlock = "";

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

        //  if (ddlYear.SelectedIndex > 0)
        //{
        //    conditions += " and mst5Village.Fyear= '" + ddlYear.SelectedItem.Text + "' ";
        //}
        if (Session["FinYear"].ToString() == ddlYear.SelectedItem.Text)
        {
            if (ddlState.Length > 0)
            {
                conditions += "  and StateCode in( " + ddlState + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and DistrictCode in(" + ddlDistrict + ") ";

            }
            if (ddlBlock.Length > 0)
            {
                conditions += " and BlockCode in(" + ddlBlock + ") ";

            }

            if (ddlUser.SelectedIndex > 0)
            {
                conditions += " and Tbl_User_Login.UserId =  '" + ddlUser.SelectedValue + "' ";

            }

        }
        else
        {

            if (ddlDistrict.Length > 0)
            {
                conditions += " and TempEGDIst in(" + ddlDistrict + ") ";

            }
            if (ddlBlock.Length > 0)
            {
                conditions += " and TempEGBlock in(" + ddlBlock + ") ";

            }

            if (ddlUser.SelectedIndex > 0)
            {
                conditions += " and Tbl_User_Login.UserId =  '" + ddlUser.SelectedValue + "' ";

            }
        }




        //if (txtDate.Text != "")
        //{
        //    DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
        //    conditions1= " and Date >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
        //}
        //if (txtTodate.Text != "")
        //{
        //    DateTime Todate = Convert.ToDateTime(txtTodate.Text);
        //    conditions1= " and Date <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";

        //}
        if (txtDate.Text != "" && txtTodate.Text != "")
        {
            DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
            DateTime Todate = Convert.ToDateTime(txtTodate.Text);
            conditions1 += " and Date BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
        }

        if (ddlYear.SelectedIndex > 0)
        {
            string Year = ddlYear.SelectedItem.Text;
            string[] Year1 = Year.Split('-');
            conditions1 += "    And Date >= '" + Year1[0] + "-04-01' and Date<='" + Year1[1] + "-03-31'";


        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions1 += " and mst5Village.Fyear= '" + ddlYear.SelectedItem.Text + "' ";
        }
        string mainCon = conditions + conditions1;
        //DataTable dt = objMain.tblReport("", "", "", mainCon);

        SqlParameter[] cmdParameters = new SqlParameter[]
            {

                new SqlParameter("@condtion", conditions),
            new SqlParameter("@condtion1", conditions1),


            };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTblUserLoginComp2023]", cmdParameters);

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows.Count > 1000)
            {
                Session["MobileUser"] = dt;
                lblTotalCount.Text = (dt.Rows.Count).ToString();
                btnImport_Click(lnkCSV, null);

            }
            else
            {
                Session["MobileUser"] = dt;
                gvD2d.DataSource = dt;
                gvD2d.DataBind();
                //  LinkButton1.Visible = true;
                lnkCSV.Visible = true;
                lblTotalCount.Text = (dt.Rows.Count).ToString();
            }
        }
        else
        {
            gvD2d.DataSource = null;
            gvD2d.DataBind();
            lblTotalCount.Text = "";
            //LinkButton1.Visible = false;
            lnkCSV.Visible = false;
        }



    }



    public void LoadReportAdmin()
    {

        conditions = "";
        string conditions1 = "where 1=1 ";
        lblTotalCount.Text = "";
        conditions = "where 1=1  ";
        string ddlDistrict = "";

        string ddlState = "";
        string ddlBlock = "";

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

        //  if (ddlYear.SelectedIndex > 0)
        //{
        //    conditions += " and mst5Village.Fyear= '" + ddlYear.SelectedItem.Text + "' ";
        //}
        if (Session["FinYear"].ToString() == ddlYear.SelectedItem.Text)
        {
            if (ddlState.Length > 0)
            {
                conditions += "  and StateCode in( " + ddlState + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and DistrictCode in(" + ddlDistrict + ") ";

            }
            if (ddlBlock.Length > 0)
            {
                conditions += " and BlockCode in(" + ddlBlock + ") ";

            }

            if (ddlUser.SelectedIndex > 0)
            {
                conditions += " and Tbl_User_Login.UserId =  '" + ddlUser.SelectedValue + "' ";

            }

        }
        else
        {

            if (ddlDistrict.Length > 0)
            {
                conditions += " and TempEGDIst in(" + ddlDistrict + ") ";

            }
            if (ddlBlock.Length > 0)
            {
                conditions += " and TempEGBlock in(" + ddlBlock + ") ";

            }

            if (ddlUser.SelectedIndex > 0)
            {
                conditions += " and Tbl_User_LoginOld.UserId =  '" + ddlUser.SelectedValue + "' ";

            }
        }




        //if (txtDate.Text != "")
        //{
        //    DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
        //    conditions1= " and Date >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
        //}
        //if (txtTodate.Text != "")
        //{
        //    DateTime Todate = Convert.ToDateTime(txtTodate.Text);
        //    conditions1= " and Date <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";

        //}
        if (txtDate.Text != "" && txtTodate.Text != "")
        {
            DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
            DateTime Todate = Convert.ToDateTime(txtTodate.Text);
            conditions1 += " and Date BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
        }

        if (ddlYear.SelectedIndex > 0)
        {
            string Year = ddlYear.SelectedItem.Text;
            string[] Year1 = Year.Split('-');
            conditions1 += "    And Date >= '" + Year1[0] + "-04-01' and Date<='" + Year1[1] + "-03-31'";


        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions1 += " and mst5Village.Fyear= '" + ddlYear.SelectedItem.Text + "' ";
        }
        string mainCon = conditions + conditions1;
        DataTable dt = null;
        //DataTable dt = objMain.tblReport("", "", "", mainCon);
        if (Session["FinYear"].ToString() == ddlYear.SelectedItem.Text)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {

                new SqlParameter("@condtion", conditions),
            new SqlParameter("@condtion1", conditions1),


            };

            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTblUserLogin]", cmdParameters);
        }
        else
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
           {

                new SqlParameter("@condtion", conditions),
            new SqlParameter("@condtion1", conditions1),


           };

            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTblUserLogin2023]", cmdParameters);

        }
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows.Count > 0)
            {
                Session["MobileUser"] = dt;
                GenerateExcelNewStringBuldQithoutColour();
                lblTotalCount.Text = (dt.Rows.Count).ToString();

            }
            else
            {
                Session["MobileUser"] = dt;
                gvD2d.DataSource = dt;

                gvD2d.DataBind();

                lblTotalCount.Text = (dt.Rows.Count).ToString();
            }
        }
        else
        {
            gvD2d.DataSource = null;
            gvD2d.DataBind();
            lblTotalCount.Text = "";
        }



    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTimeSheet_StartTime = (Label)e.Row.FindControl("lblTimeSheet_StartTime");
            Label lblTimeSheet_EndTime = (Label)e.Row.FindControl("lblTimeSheet_EndTime");
            Label lblHours = (Label)e.Row.FindControl("lblHours");
            Label lblStarttimeLocation = (Label)e.Row.FindControl("lblStarttimeLocation");
            Label lblEndtimeLocation = (Label)e.Row.FindControl("lblEndtimeLocation");
            Label lblVillage_GeoLocation = (Label)e.Row.FindControl("lblVillage_GeoLocation");
            Int32 dHours = (Convert.ToDateTime(lblTimeSheet_EndTime.Text) - Convert.ToDateTime(lblTimeSheet_StartTime.Text)).Hours;
            Int32 dMins = (Convert.ToDateTime(lblTimeSheet_EndTime.Text) - Convert.ToDateTime(lblTimeSheet_StartTime.Text)).Minutes;

            Label L1 = (Label)e.Row.FindControl("L1");
            Label L2 = (Label)e.Row.FindControl("L2");
            Label L3 = (Label)e.Row.FindControl("L3");
            Label L4 = (Label)e.Row.FindControl("L4");

            string L = L1 + "," + L2;
            string E = L3 + "," + L4;




            string retStr = dHours.ToString() + ":" + dMins.ToString();
            lblHours.Text = retStr;

            DateTime fromTime = Convert.ToDateTime(lblTimeSheet_StartTime.Text);
            DateTime toTime = Convert.ToDateTime(lblTimeSheet_EndTime.Text);
            TimeSpan fromH = TimeSpan.FromHours(fromTime.Hour);
            TimeSpan toH = TimeSpan.FromHours(toTime.Hour);
            TimeSpan hourTotalSpan = toH.Subtract(fromH);

            //string s1 = "24.445,72.82";
            //e.Row.Cells[8].Text = "<a href='javascript:Page.ShowLocation(\"" + s1 + "\"," + lblVillage_GeoLocation.Text + ")'>" + s1 + "</a>";

            e.Row.Cells[8].Text = "<a href='javascript:Page.ShowLocation(\"" + lblStarttimeLocation.Text + "\"," + lblVillage_GeoLocation.Text + ")'>" + lblStarttimeLocation.Text + "</a>";
            e.Row.Cells[10].Text = "<a href='javascript:Page.ShowLocation(\"" + lblEndtimeLocation.Text + "\"," + lblVillage_GeoLocation.Text + ")'>" + lblEndtimeLocation.Text + "</a>";


        }
    }


    protected void GvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {

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


    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {

        //  objComman.BindDLL("MstUser", "UserName,[UserName]+' ('+ FristName +')' as UserName1 ", conditions, "UserName1", "asc", ddlUser, "UserName1", "UserName", "--Select--");

        LoadUser();

    }

    public void LoadUser()
    {
        conditions = "";
        string ddlDistrict = "";

        string ddlState = "";
        string ddlBlock = "";

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
        if (Session["FinYear"].ToString() == ddlYear.SelectedItem.Text)
        {

            if (Convert.ToInt32(ddlType.SelectedValue) == 1)
            {
                conditions = " UserLevel=24 ";
            }
            else
            {
                conditions = " UserLevel<>24 ";
            }
            if (ddlState.Length > 0)
            {
                conditions += "  and StateCode in( " + ddlState + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and DistrictCode in(" + ddlDistrict + ") ";

            }
            if (ddlBlock.Length > 0)
            {
                conditions += " and BlockCode in(" + ddlBlock + ") ";

            }



        }

        else if (ddlYear.SelectedItem.Text == "2025-2026")
        {
            if (Convert.ToInt32(ddlType.SelectedValue) == 1)
            {
                conditions = " UserLevel=24 ";
            }
            else
            {
                conditions = " UserLevel<>24 ";
            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and TempEGDIst in(" + ddlDistrict + ") ";

            }
            if (ddlBlock.Length > 0)
            {
                conditions += " and TempEGBlock in(" + ddlBlock + ") ";

            }

            if (ddlUser.SelectedIndex > 0)
            {
                conditions += " and Tbl_User_Login.UserId =  '" + ddlUser.SelectedValue + "' ";

            }
        }
        else if (ddlYear.SelectedItem.Text == "2024-2025")
        {
            if (Convert.ToInt32(ddlType.SelectedValue) == 1)
            {
                conditions = " UserLevel=24 ";
            }
            else
            {
                conditions = " UserLevel<>24 ";
            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and TempEGDIst in(" + ddlDistrict + ") ";

            }
            if (ddlBlock.Length > 0)
            {
                conditions += " and TempEGBlock in(" + ddlBlock + ") ";

            }

            if (ddlUser.SelectedIndex > 0)
            {
                conditions += " and Tbl_User_Login.UserId =  '" + ddlUser.SelectedValue + "' ";

            }
        }
        else if (ddlYear.SelectedItem.Text == "2023-2024")
        {
            if (Convert.ToInt32(ddlType.SelectedValue) == 1)
            {
                conditions = " UserLevel=24 ";
            }
            else
            {
                conditions = " UserLevel<>24 ";
            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and DistrictCode in(" + ddlDistrict + ") ";

            }
            if (ddlBlock.Length > 0)
            {
                conditions += " and BlockCode in(" + ddlBlock + ") ";

            }

            if (ddlUser.SelectedIndex > 0)
            {
                conditions += " and Tbl_User_Login.UserId =  '" + ddlUser.SelectedValue + "' ";

            }
        }

        if (ddlYear.SelectedItem.Text == "2023-2024")
        {
            objComman.BindDLL("MstUser2024", "UserId as UserId,[FristName]+' ('+ UserName +')' as [UserName] ", conditions, "", "", ddlUser, "UserName", "UserId", "Select");

        }
        else

        {
            objComman.BindDLL("MstUser", "UserId as UserId,[FristName]+' ('+ UserName +')' as [UserName] ", conditions, "", "", ddlUser, "UserName", "UserId", "Select");

        }
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
            conditions = "DistrictCode in(" + ddlDistrict + ")   and BlockCode in( " + Session["BlockCode"].ToString() + " ) and FYear ='" + Session["FinYear"].ToString() + "' ";
        }
        else
        {
            conditions = "DistrictCode in(" + ddlDistrict + ")  ";
        }
        //     objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        string strQry = "  SELECT BlockCode, dbo.TitleCase(upper(BlockName))  as BlockName FROM mst3Block where " + conditions + "  order by BlockName   ";
        DataTable dtDistrict = objMain.LoadData(strQry);
        // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        chkBlock.DataSource = dtDistrict;
        chkBlock.DataTextField = "BlockName";
        chkBlock.DataValueField = "BlockCode";
        chkBlock.DataBind();


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

        }


    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    private void ExporttoExcel(GridView Gv, DataTable table)
    {


        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");

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
        if (Session["MobileUser"] != null)
        {
            DataTable dt = Session["MobileUser"] as DataTable;
            gvD2d.DataSource = dt;
            gvD2d.DataBind();
        }


        foreach (GridViewRow Itemst in gvD2d.Rows)
        {
            #region SaveData
            Label EGBlock = Itemst.FindControl("EGBlock") as Label;
            Label lblTimeSheet_StartTime = (Label)Itemst.FindControl("lblTimeSheet_StartTime");
            Label lblTimeSheet_EndTime = (Label)Itemst.FindControl("lblTimeSheet_EndTime");
            Label lblHours = (Label)Itemst.FindControl("lblHours");
            Label lblStarttimeLocation = (Label)Itemst.FindControl("lblStarttimeLocation");
            Label lblEndtimeLocation = (Label)Itemst.FindControl("lblEndtimeLocation");
            Label lblVillage_GeoLocation = (Label)Itemst.FindControl("lblVillage_GeoLocation");
            Int32 dHours = (Convert.ToDateTime(lblTimeSheet_EndTime.Text) - Convert.ToDateTime(lblTimeSheet_StartTime.Text)).Hours;
            Int32 dMins = (Convert.ToDateTime(lblTimeSheet_EndTime.Text) - Convert.ToDateTime(lblTimeSheet_StartTime.Text)).Minutes;

            Label L1 = (Label)Itemst.FindControl("L1");
            Label L2 = (Label)Itemst.FindControl("L2");
            Label L3 = (Label)Itemst.FindControl("L3");
            Label L4 = (Label)Itemst.FindControl("L4");

            string L = L1 + "," + L2;
            string E = L3 + "," + L4;




            string retStr = dHours.ToString() + ":" + dMins.ToString();
            lblHours.Text = retStr;

            DateTime fromTime = Convert.ToDateTime(lblTimeSheet_StartTime.Text);
            DateTime toTime = Convert.ToDateTime(lblTimeSheet_EndTime.Text);
            TimeSpan fromH = TimeSpan.FromHours(fromTime.Hour);
            TimeSpan toH = TimeSpan.FromHours(toTime.Hour);
            TimeSpan hourTotalSpan = toH.Subtract(fromH);

            //string s1 = "24.445,72.82";
            //e.Row.Cells[8].Text = "<a href='javascript:Page.ShowLocation(\"" + s1 + "\"," + lblVillage_GeoLocation.Text + ")'>" + s1 + "</a>";

            Itemst.Cells[8].Text = "<a href='javascript:Page.ShowLocation(\"" + lblStarttimeLocation.Text + "\"," + lblVillage_GeoLocation.Text + ")'>" + lblStarttimeLocation.Text + "</a>";
            Itemst.Cells[10].Text = "<a href='javascript:Page.ShowLocation(\"" + lblEndtimeLocation.Text + "\"," + lblVillage_GeoLocation.Text + ")'>" + lblEndtimeLocation.Text + "</a>";


            #endregion
        }


    }
    protected void gvnroll_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GV_DynamicGrid_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }


    #region Abhimanyu

    public void UpdateLatLong()
    {
        double distance = 0;
        double Enddistance = 0;
        string villagecode = string.Empty;
        string VillageLocation = string.Empty;
        DataTable dt = new DataTable();
        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[GetVillageLatLong1]");
        if (dt.Rows.Count > 0)
        {
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    distance = Math.Round(GeoUtils.GeoPointChecker.abcdnew2(dt.Rows[i]["VillageCode"].ToString(), dt.Rows[i]["Village_GeoLocation"].ToString()), 2);
            //}


        }
    }

    protected void btnCSV_Click(object sender, EventArgs e)
    {
        //UpdateLatLong();
        //if (ViewState["1"].ToString() == "2")
        //{
        DataTable dt = (DataTable)Session["MobileUser"];
        string color = string.Empty;
        var i = 0;
        double distance = 0;
        double Enddistance = 0;
        string abc1 = "";
        string abc2 = "";
        String DataStyle = "border:.5pt solid windowtext;";
        String InvalidGeoLocationStype = "Red";
        String ValidGeoLocationStype = "Green";

        DataTable dt1 = new DataTable();
        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            #region Add Datatable


            dt1.Columns.Add("#");
            dt1.Columns.Add("Employee ID");
            dt1.Columns.Add("Employee");
            dt1.Columns.Add("Type");
            dt1.Columns.Add("Date");
            dt1.Columns.Add("District");
            dt1.Columns.Add("Block");

            dt1.Columns.Add("Grampanchayat");
            dt1.Columns.Add("Village ID");
            dt1.Columns.Add("Village");
            dt1.Columns.Add("Entry Date/Time");
            dt1.Columns.Add("Start Entry Location");
            dt1.Columns.Add("End Entry Location");

            dt1.Columns.Add("End Entry Time");
            dt1.Columns.Add("Hours");
            dt1.Columns.Add("Start Distance");
            dt1.Columns.Add("END Distance");
            dt1.Columns.Add("User District");
            dt1.Columns.Add("User Block");
            dt1.Columns.Add("Cluster Name");
            dt1.Columns.Add("Color");
            if (dt.Rows.Count > 0)
            {
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    var RowStyle = DataStyle;
                    color = "";
                    Int32 dHours = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Hours;
                    Int32 dMins = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Minutes;

                    TimeSpan dayJob = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"])).Subtract(Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"]));

                    String villageGeoData = dt.Rows[i]["Village_GeoLocation"].ToString();
                    abc1 = null;
                    abc2 = null;


                    if (villageGeoData != null && villageGeoData != "" && villageGeoData != "[]")
                    {

                        if (dt.Rows[i]["StarttimeLocation"].ToString() == "" || villageGeoData.Length < 100 || dt.Rows[i]["StarttimeLocation"].ToString() == null || dt.Rows[i]["StarttimeLocation"].ToString() == "Unsupported browser" || dt.Rows[i]["StarttimeLocation"].ToString() == "User denied" || dt.Rows[i]["StarttimeLocation"].ToString() == "Location unavailable" || dt.Rows[i]["StarttimeLocation"].ToString() == "Request timed out" || dt.Rows[i]["StarttimeLocation"].ToString() == "Unknown error" || dt.Rows[i]["StarttimeLocation"].ToString() == "GPS turned off")
                        {
                        }
                        else
                        {

                            //GeoUtils.GeoPointChecker geoChecker = new GeoUtils.GeoPointChecker(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData);

                            if (dt.Rows[i]["StarttimeLocation"].ToString().Length > 4 && dt.Rows[i]["EndtimeLocation"].ToString().ToString().Length > 4)
                            {

                                //if (geoChecker.isValid() == false)
                                //{
                                //distance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData), 2);
                                distance = Math.Round(Convert.ToDouble(dt.Rows[i]["Distance1"].ToString()), 2);

                                if (distance > 4)
                                {
                                    color = "Red";
                                    abc1 = InvalidGeoLocationStype;
                                }
                                else
                                {
                                    if (distance > 0)
                                    {
                                        color = "Green";
                                        abc1 = ValidGeoLocationStype;
                                    }
                                    else
                                    {
                                        color = "Red";
                                        abc1 = InvalidGeoLocationStype;
                                    }
                                }
                                //}
                                //else
                                //{
                                //    color = "Green";
                                //    distance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData), 2);
                                //    abc1 = ValidGeoLocationStype;
                                //}
                            }
                            else
                            {
                                RowStyle += DataStyle;
                                abc1 = DataStyle;
                                color = "";
                            }

                            //GeoUtils.GeoPointChecker geoChecker1 = new GeoUtils.GeoPointChecker(dt.Rows[i]["EndtimeLocation"].ToString(), villageGeoData);
                            if (dt.Rows[i]["StarttimeLocation"].ToString().Length > 4 && dt.Rows[i]["EndtimeLocation"].ToString().ToString().Length > 4)
                            {
                                Enddistance = Math.Round(Convert.ToDouble(dt.Rows[i]["Distance2"].ToString()), 2);
                                abc2 = ValidGeoLocationStype;
                            }
                            else
                            {
                                abc2 = InvalidGeoLocationStype;
                            }
                        }
                    }


                    DataRow _dr = dt1.NewRow();
                    _dr["#"] = (i + 1);
                    _dr["Employee ID"] = dt.Rows[i]["UserName"].ToString();
                    _dr["Employee"] = dt.Rows[i]["FristName"].ToString();
                    _dr["Type"] = dt.Rows[i]["Role"].ToString();
                    _dr["Date"] = Convert.ToDateTime(dt.Rows[i]["Date"].ToString()).ToString("dd/MM/yyy");
                    _dr["District"] = dt.Rows[i]["DistrictName"].ToString();
                    _dr["Block"] = dt.Rows[i]["BlockName"].ToString();
                    _dr["Grampanchayat"] = dt.Rows[i]["PanchayatName"].ToString();
                    _dr["Village ID"] = dt.Rows[i]["VillageCode"].ToString();
                    _dr["Village"] = dt.Rows[i]["VillageName"].ToString();
                    _dr["Entry Date/Time"] = dt.Rows[i]["TimeSheet_StartTime"].ToString();
                    _dr["Start Entry Location"] = dt.Rows[i]["StarttimeLocation"].ToString();


                    if (abc1 == "red);")
                    {

                    }
                    else if (abc1 == "Green")
                    {
                        if (dt.Rows[i]["EndtimeLocation"].ToString() == "GPS turned off")
                        {
                            _dr["End Entry Location"] = "";

                        }
                        else
                        {
                            _dr["End Entry Location"] = dt.Rows[i]["EndtimeLocation"].ToString();
                        }

                    }

                    else
                    {
                        _dr["End Entry Location"] = dt.Rows[i]["EndtimeLocation"].ToString();
                    }
                    _dr["End Entry Time"] = dt.Rows[i]["TimeSheet_EndTime"].ToString();
                    _dr["Hours"] = dayJob.Hours + ":" + dayJob.Minutes;

                    if (abc1 == "Green")
                    {
                        _dr["Start Distance"] = distance + "KM";

                    }
                    else if (abc1 == "Red")
                    {
                        _dr["Start Distance"] = distance + "KM";

                    }
                    else if (abc1 == "border:.5pt solid windowtext;")
                    {
                        _dr["Start Distance"] = "NA";

                    }
                    else
                    {
                        _dr["Start Distance"] = "NA";
                    }

                    if (abc2 == "Green")
                    {
                        _dr["END Distance"] = Enddistance + "KM";
                    }
                    else if (abc2 == "Red")
                    {
                        _dr["END Distance"] = "NA";
                    }
                    else if (abc2 == "border:.5pt solid windowtext;")
                    {
                        _dr["END Distance"] = "NA";
                    }
                    else
                    {
                        _dr["END Distance"] = "NA";
                    }

                    _dr["User District"] = dt.Rows[i]["UserDist"].ToString();
                    _dr["User Block"] = dt.Rows[i]["UserBlock"].ToString();
                    _dr["Cluster Name"] = dt.Rows[i]["ClusterName"].ToString();
                    _dr["Color"] = color;
                    dt1.Rows.Add(_dr);
                }
            }

            #endregion
        }
        else
        {
            #region Add Datatable


            dt1.Columns.Add("#");
            dt1.Columns.Add("Employee ID");
            dt1.Columns.Add("Employee");
            dt1.Columns.Add("Type");
            dt1.Columns.Add("Date");
            dt1.Columns.Add("District");
            dt1.Columns.Add("Block");

            dt1.Columns.Add("Grampanchayat");
            dt1.Columns.Add("Cluster Name");
            dt1.Columns.Add("Cluster Code");
            dt1.Columns.Add("Village ID");
            dt1.Columns.Add("Village");
            dt1.Columns.Add("Entry Date/Time");
            dt1.Columns.Add("Start Entry Location");
            dt1.Columns.Add("End Entry Location");

            dt1.Columns.Add("End Entry Time");
            dt1.Columns.Add("Hours");
            dt1.Columns.Add("Start Distance");
            dt1.Columns.Add("END Distance");
            dt1.Columns.Add("User District");
            dt1.Columns.Add("Color");
            if (dt.Rows.Count > 0)
            {
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    var RowStyle = DataStyle;
                    color = "";
                    Int32 dHours = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Hours;
                    Int32 dMins = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Minutes;

                    TimeSpan dayJob = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"])).Subtract(Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"]));


                    String villageGeoData = dt.Rows[i]["Village_GeoLocation"].ToString();
                    abc1 = null;
                    abc2 = null;


                    if (villageGeoData != null && villageGeoData != "" && villageGeoData != "[]")
                    {

                        if (dt.Rows[i]["StarttimeLocation"].ToString() == "" || villageGeoData.Length < 100 || dt.Rows[i]["StarttimeLocation"].ToString() == null || dt.Rows[i]["StarttimeLocation"].ToString() == "Unsupported browser" || dt.Rows[i]["StarttimeLocation"].ToString() == "User denied" || dt.Rows[i]["StarttimeLocation"].ToString() == "Location unavailable" || dt.Rows[i]["StarttimeLocation"].ToString() == "Request timed out" || dt.Rows[i]["StarttimeLocation"].ToString() == "Unknown error" || dt.Rows[i]["StarttimeLocation"].ToString() == "GPS turned off")
                        {

                        }
                        else
                        {

                            //GeoUtils.GeoPointChecker geoChecker = new GeoUtils.GeoPointChecker(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData);

                            if (dt.Rows[i]["StarttimeLocation"].ToString().Length > 4 && dt.Rows[i]["EndtimeLocation"].ToString().ToString().Length > 4)
                            {

                                //if (geoChecker.isValid() == false)
                                //{
                                distance = Math.Round(Convert.ToDouble(dt.Rows[i]["Distance1"].ToString()), 2);
                                //double ddd = distance - distance1;

                                if (distance > 4)
                                {
                                    color = "Red";
                                    abc1 = InvalidGeoLocationStype;
                                }
                                else
                                {
                                    color = "Green";
                                    abc1 = ValidGeoLocationStype;
                                }
                                //}
                                //else
                                //{
                                //    color = "Green";
                                //    distance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData), 2);
                                //    abc1 = ValidGeoLocationStype;
                                //}
                            }
                            else
                            {
                                RowStyle += DataStyle;
                                abc1 = DataStyle;
                                color = "";
                            }

                            //GeoUtils.GeoPointChecker geoChecker1 = new GeoUtils.GeoPointChecker(dt.Rows[i]["EndtimeLocation"].ToString(), villageGeoData);
                            if (dt.Rows[i]["StarttimeLocation"].ToString().Length > 4 && dt.Rows[i]["EndtimeLocation"].ToString().ToString().Length > 4)
                            {
                                Enddistance = Math.Round(Convert.ToDouble(dt.Rows[i]["Distance2"].ToString()), 2);
                                abc2 = ValidGeoLocationStype;
                            }
                            else
                            {
                                abc2 = InvalidGeoLocationStype;
                            }
                        }
                    }


                    DataRow _dr = dt1.NewRow();
                    _dr["#"] = (i + 1);
                    _dr["Employee ID"] = dt.Rows[i]["UserName"].ToString();
                    _dr["Employee"] = dt.Rows[i]["FristName"].ToString();
                    _dr["Type"] = dt.Rows[i]["Role"].ToString();
                    _dr["Date"] = Convert.ToDateTime(dt.Rows[i]["Date"].ToString()).ToString("dd/MM/yyy");
                    _dr["District"] = dt.Rows[i]["DistrictName"].ToString();
                    _dr["Block"] = dt.Rows[i]["BlockName"].ToString();
                    _dr["Grampanchayat"] = dt.Rows[i]["PanchayatName"].ToString();
                    _dr["Cluster Name"] = dt.Rows[i]["ClusterName"].ToString();
                    _dr["Cluster Code"] = dt.Rows[i]["ClusterCode"].ToString();
                    _dr["Village ID"] = dt.Rows[i]["VillageCode"].ToString();
                    _dr["Village"] = dt.Rows[i]["VillageName"].ToString();
                    _dr["Entry Date/Time"] = dt.Rows[i]["TimeSheet_StartTime"].ToString();
                    _dr["Start Entry Location"] = dt.Rows[i]["StarttimeLocation"].ToString();


                    if (abc1 == "red);")
                    {

                    }
                    else if (abc1 == "Green")
                    {
                        if (dt.Rows[i]["EndtimeLocation"].ToString() == "GPS turned off")
                        {
                            _dr["End Entry Location"] = "";

                        }
                        else
                        {
                            _dr["End Entry Location"] = dt.Rows[i]["EndtimeLocation"].ToString();
                        }

                    }

                    else
                    {
                        _dr["End Entry Location"] = dt.Rows[i]["EndtimeLocation"].ToString();
                    }
                    _dr["End Entry Time"] = dt.Rows[i]["TimeSheet_EndTime"].ToString();
                    _dr["Hours"] = dayJob.Hours + ":" + dayJob.Minutes;

                    if (abc1 == "Green")
                    {
                        _dr["Start Distance"] = distance + "KM";

                    }
                    else if (abc1 == "Red")
                    {
                        _dr["Start Distance"] = distance + "KM";

                    }
                    else if (abc1 == "border:.5pt solid windowtext;")
                    {
                        _dr["Start Distance"] = "NA";

                    }
                    else
                    {
                        _dr["Start Distance"] = "NA";
                    }

                    if (abc2 == "Green")
                    {
                        _dr["END Distance"] = Enddistance + "KM";
                    }
                    else if (abc2 == "Red")
                    {
                        _dr["END Distance"] = "NA";
                    }
                    else if (abc2 == "border:.5pt solid windowtext;")
                    {
                        _dr["END Distance"] = "NA";
                    }
                    else
                    {
                        _dr["END Distance"] = "NA";
                    }
                    distance = 0;

                    Enddistance = 0;
                    _dr["User District"] = dt.Rows[i]["DistName"].ToString();
                    _dr["Color"] = color;
                    dt1.Rows.Add(_dr);
                }
            }
            #endregion
        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            ExporttoCSV(gvD2d, dt1, "FCLoginLogoutReport");
        }
        else
        {
            ExporttoCSV(gvD2d, dt1, "BOLoginLogoutReport");
        }
        //}
    }
    private void ExporttoCSV(GridView Gv, DataTable table, string filename1)
    {
        string filePath = filename1;

        var dataTable = table;
        StringBuilder sbldr = new StringBuilder();
        List<string> columnNames = new List<string>();
        List<string> rows = new List<string>();


        if (dataTable.Columns.Count != 0)
        {
            foreach (DataColumn col in dataTable.Columns)
            {
                sbldr.Append(col.ColumnName + ',');
            }
            sbldr.Append("\r\n");
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (DataColumn column in dataTable.Columns)
                {

                    if (column.ColumnName == "Start Entry Location" || column.ColumnName == "End Entry Location")
                    {
                        sbldr.Append(Convert.ToString(row[column]).Replace(",", "- ").Replace("\r", "").Replace("\n", "") + ',');
                    }
                    else
                    {
                        sbldr.Append(Convert.ToString(row[column]).Replace(",", "- ").Replace("\r", "").Replace("\n", "") + ',');
                    }
                }
                sbldr.Append("\r\n");

            }
        }
        //    foreach (DataColumn col in dataTable.Columns)
        //{
        //    builder.Append(col.ColumnName + ',');
        //}
        //builder.Append("\r\n");
        //foreach (DataRow row in dataTable.Rows)
        //{
        //    List<string> currentRow = new List<string>();

        //    foreach (DataColumn column in dataTable.Columns)
        //    {
        //        if (column.ColumnName == "Start Entry Location" || column.ColumnName == "End Entry Location")
        //        {
        //            builder.Append(row[column].ToString().Replace(",", "-"));
        //        }
        //        else
        //        {
        //            builder.Append(row[column].ToString().Replace("\r", "").Replace("\n", ""));
        //        }




        //    }
        //    builder.Append("\r\n");
        //    // rows.Add(string.Join(",", currentRow.ToArray()));

        //}



        //   builder.Append(string.Join("\n", rows.ToArray()));

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

    #endregion
    private DateTime ConvertToEGDateTime(string EGDateTime)
    {


        char[] sep = new char[] { '/' };

        string[] ogDateArray = EGDateTime.Split(sep);

        DateTime ReturnValue = new DateTime(Convert.ToInt32(ogDateArray[2]), Convert.ToInt32(ogDateArray[1]), Convert.ToInt32(ogDateArray[0]));



        return ReturnValue;
    }
    //  private void GenerateExcel()
    //  {
    //      string abc1 = "";

    //      HttpContext.Current.Response.Clear();
    //      HttpContext.Current.Response.ClearContent();
    //      HttpContext.Current.Response.ClearHeaders();
    //      HttpContext.Current.Response.Buffer = true;
    //      HttpContext.Current.Response.ContentType = "application/ms-excel";
    //      HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
    //      HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");
    //      string Fullfilename = "" + "Employeetracking" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


    //      HttpContext.Current.Response.Charset = "utf-8";
    //      HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
    //     // Int32 EmpID = Convert.ToInt32(Contx.Request["empid"]);

    //      DataTable dt = Session["MobileUser"] as DataTable;

    //      HttpContext.Current.Response.Write( "<table style='border:.5pt solid windowtext;'>");

    //     HttpContext.Current.Response.Write("<tr>");
    //     HttpContext.Current.Response.Write("         <td colspan='7' style='text-align:center;'><h1>Timesheet report</h1></td>");
    //     HttpContext.Current.Response.Write("   </tr>");


    //      //DateTime FromDate = ConvertToEGDateTime(txtDate.Text);
    //      //DateTime ToDate = ConvertToEGDateTime(txtTodate.Text);



    //      //TimeSpan spanTime = (ToDate - FromDate);

    //      //Int32 totDays = spanTime.Days;




    //      if (dt.Rows.Count > 0)
    //      {



    //          //retStr += "    <tr>";
    //          //retStr += "         <td style='border:.5pt solid windowtext;font-weight:700;'>To Date</td>";
    //          //retStr += "         <td style='border:.5pt solid windowtext;'>" + ToDate.ToShortDateString() + "</td>";
    //          //retStr += "         <td style='border:.5pt solid windowtext;font-weight:700;'></td>";
    //          //retStr += "         <td style='border:.5pt solid windowtext;'></td>";
    //          //retStr += "    </tr>";

    //         HttpContext.Current.Response.Write("    <tr>");
    //         HttpContext.Current.Response.Write("         <td style='border:.5pt solid windowtext;font-weight:700;'>Generated On</td>");
    //         HttpContext.Current.Response.Write("         <td style='border:.5pt solid windowtext;'>" + DateTime.Now.ToString() + "</td>");
    //         HttpContext.Current.Response.Write("         <td style='border:.5pt solid windowtext;font-weight:700;'></td>");
    //         HttpContext.Current.Response.Write("         <td style='border:.5pt solid windowtext;'></td>");
    //         HttpContext.Current.Response.Write("    </tr>");


    //      }

    //      String HeaderStyle = "border:.5pt solid windowtext; font-weight:700;background:#D9D9D9;";
    //     HttpContext.Current.Response.Write("    <tr style='font-width:bold;'>");
    //     HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>#</td>");
    //  //   HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>TimeSheet ID</td>");
    //     HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Employee ID</td>");
    //     HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Employee</td>");
    //     HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Type</td>");
    //     HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Entry Date/Time</td>");
    //     HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Date</td>");
    //     HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>District</td>");
    //     HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Block</td>");
    //     HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Grampanchayat</td>");
    //     HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Village ID</td>");
    //     HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Village</td>");
    //     HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Start Entry Time</td>");
    //     HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Start Entry Location</td>");
    //     HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Start Time</td>");
    //   //  HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Start Time Change Reason</td>");
    //     HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>End Entry Time</td>");
    //     HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>End Entry Location</td>");
    //     HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>End Time</td>");
    // //    HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>End Time Change Reason</td>");
    ////     HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Mode</td>");
    //     HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Hours</td>");
    //     HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Start Distance</td>");
    //     HttpContext.Current.Response.Write("    </tr>");



    //      String DataStyle = "border:.5pt solid windowtext;";
    //      String DateTimeStyle = "mso-number-format:\"mm/dd/yyyy hh:mm AM/PM \";";
    //      String DateStyle = "mso-number-format:\"mm/dd/yyyy \";";
    //      String TimeStyle = "mso-number-format:\"hh:mm AM/PM \";";
    //      String InvalidGeoLocationStype = "background-color:red;";
    //      String ValidGeoLocationStype = "background-color:#99FF66;";

    //      var i = 0;
    //      double distance = 0;
    //      for (i = 0; i < dt.Rows.Count; i++)
    //      {

    //          var RowStyle = DataStyle;

    //          Int32 dHours = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Hours;
    //          Int32 dMins = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Minutes;

    //          TimeSpan dayJob = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"])).Subtract(Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"]));

    //          String villageGeoData = dt.Rows[i]["Village_GeoLocation"].ToString();
    //          abc1 = null;
    //          if (villageGeoData != null && villageGeoData != "" && villageGeoData != "[]")
    //          {
    //              if (dt.Rows[i]["StarttimeLocation"].ToString() == "" || dt.Rows[i]["StarttimeLocation"].ToString() == null || dt.Rows[i]["StarttimeLocation"].ToString() == "Unsupported browser" || dt.Rows[i]["StarttimeLocation"].ToString() == "User denied" || dt.Rows[i]["StarttimeLocation"].ToString() == "Location unavailable" || dt.Rows[i]["StarttimeLocation"].ToString() == "Request timed out" || dt.Rows[i]["StarttimeLocation"].ToString() == "Unknown error" || dt.Rows[i]["StarttimeLocation"].ToString() == "GPS turned off")
    //              {

    //              }
    //              else
    //              {

    //                  GeoUtils.GeoPointChecker geoChecker = new GeoUtils.GeoPointChecker(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData);
    //                  if (geoChecker.isValid() == false)
    //                  {
    //                      distance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData), 2);
    //                      RowStyle += InvalidGeoLocationStype;
    //                      abc1 = InvalidGeoLocationStype;
    //                  }
    //                  else
    //                  {
    //                      RowStyle += ValidGeoLocationStype;
    //                      abc1 = ValidGeoLocationStype;

    //                  }
    //              }
    //          }


    //         HttpContext.Current.Response.Write("<tr>");
    //         HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + (i + 1) + "</td>");
    //          //retStr += "<td style='" + RowStyle + "'>" + mData.TimeSheet_ID + "</td>");
    //         HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["UserName"].ToString() + "</td>");
    //         HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["FristName"].ToString() + "</td>");
    //         HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["Role"].ToString() + "</td>");
    //         HttpContext.Current.Response.Write("<td style='" + RowStyle + DateTimeStyle + "'>" + Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"].ToString()).ToString("dd/MM/yyyy hh:mm tt") + "</td>");
    //         HttpContext.Current.Response.Write("<td style='" + RowStyle + DateStyle + "'>" + Convert.ToDateTime(dt.Rows[i]["Date"].ToString()).ToString("dd/MM/yyy") + "</td>");
    //         HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["DistrictName"].ToString() + "</td>");
    //         HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["BlockName"].ToString() + "</td>");
    //         HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["PanchayatName"].ToString() + "</td>");
    //         HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["VillageCode"].ToString() + "</td>");
    //         HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["VillageName"].ToString() + "</td>");
    //         HttpContext.Current.Response.Write("<td style='" + RowStyle + DateTimeStyle + "'>" + Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"].ToString()).ToString("dd/MM/yyyy hh:mm tt") + "</td>");
    //         HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["StarttimeLocation"].ToString() + "</td>");
    //         HttpContext.Current.Response.Write("<td style='" + RowStyle + TimeStyle + "'>" + Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"].ToString()).ToShortTimeString() + "</td>");
    //         //HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + mData.TimeSheet_StartTimeReasonText + "</td>");
    //         HttpContext.Current.Response.Write("<td style='" + RowStyle + DateTimeStyle + "'>" + Convert.ToDateTime(Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"].ToString())).ToString("dd/MM/yyyy hh:mm tt") + "</td>");
    //          if (abc1 == "background-color:red);")
    //          {
    //              HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"].ToString()) + "</td>");

    //          }
    //          else if (abc1 == ValidGeoLocationStype)
    //          {
    //              if (dt.Rows[i]["EndtimeLocation"].ToString()== "GPS turned off")
    //              {
    //                 HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");

    //              }
    //              else
    //              {
    //                  HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["EndtimeLocation"].ToString() + "</td>");
    //              }

    //          }

    //          else
    //          {
    //             HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["EndtimeLocation"].ToString() + "</td>");
    //          }
    //          //retStr += "<td style='" + RowStyle + "'>" + mData.TimeSheet_EndLocation + "</td>";
    //         HttpContext.Current.Response.Write("<td style='" + RowStyle + TimeStyle + "'>" + Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"].ToString()).ToShortTimeString() + "</td>");
    //        // HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + mData.TimeSheet_EndTimeReasonText + "</td>";
    //         //HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + mData.TimeSheet_Mode + "</td>";
    //          //retStr += "<td style='" + DataStyle + "'>" + dHours.ToString() + ":" + dMins.ToString() + "</td>";
    //         HttpContext.Current.Response.Write("<td style='" + RowStyle + "mso-number-format:\"[hh]:mm\";'>" + dayJob.Hours + ":" + dayJob.Minutes + "</td>");
    //          if (abc1 == "background-color:red;")
    //          {
    //             HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + distance + "KM</td>");

    //          }
    //          else if (abc1 == "background-color:#99FF66")
    //          {
    //             HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "NA" + "</td>");

    //          }
    //          else
    //          {
    //             HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "NA" + "</td>");
    //          }
    //          distance = 0;
    //          HttpContext.Current.Response.Write("</tr>");

    //      }

    //       DataStyle += "background-color:yellow;";

    //       HttpContext.Current.Response.Write("</table>");
    //       HttpContext.Current.Response.Flush();
    //       HttpContext.Current.Response.End();


    //      //flushExcel(totDays.ToString());

    //      //var lq=dataList.Select(fld=>fld.TimeSheet_EndTime.

    //  }


    //private void GenerateExcel()
    //{
    //    string abc1 = "";

    //    HttpContext.Current.Response.Clear();
    //    HttpContext.Current.Response.ClearContent();
    //    HttpContext.Current.Response.ClearHeaders();
    //    HttpContext.Current.Response.Buffer = true;
    //    HttpContext.Current.Response.ContentType = "application/ms-excel";
    //    HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
    //    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");
    //    string Fullfilename = "" + "Employeetracking" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


    //    HttpContext.Current.Response.Charset = "utf-8";
    //    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
    //    // Int32 EmpID = Convert.ToInt32(Contx.Request["empid"]);

    //    DataTable dt = Session["MobileUser"] as DataTable;

    //    HttpContext.Current.Response.Write("<table style='border:.5pt solid windowtext;'>");

    //    HttpContext.Current.Response.Write("<tr>");
    //    HttpContext.Current.Response.Write("         <td colspan='7' style='text-align:center;'><h1>Timesheet report</h1></td>");
    //    HttpContext.Current.Response.Write("   </tr>");


    //    //DateTime FromDate = ConvertToEGDateTime(txtDate.Text);
    //    //DateTime ToDate = ConvertToEGDateTime(txtTodate.Text);



    //    //TimeSpan spanTime = (ToDate - FromDate);

    //    //Int32 totDays = spanTime.Days;




    //    if (dt.Rows.Count > 0)
    //    {



    //        //retStr += "    <tr>";
    //        //retStr += "         <td style='border:.5pt solid windowtext;font-weight:700;'>To Date</td>";
    //        //retStr += "         <td style='border:.5pt solid windowtext;'>" + ToDate.ToShortDateString() + "</td>";
    //        //retStr += "         <td style='border:.5pt solid windowtext;font-weight:700;'></td>";
    //        //retStr += "         <td style='border:.5pt solid windowtext;'></td>";
    //        //retStr += "    </tr>";

    //        HttpContext.Current.Response.Write("    <tr>");
    //        HttpContext.Current.Response.Write("         <td style='border:.5pt solid windowtext;font-weight:700;'>Generated On</td>");
    //        HttpContext.Current.Response.Write("         <td style='border:.5pt solid windowtext;'>" + DateTime.Now.ToString() + "</td>");
    //        HttpContext.Current.Response.Write("         <td style='border:.5pt solid windowtext;font-weight:700;'></td>");
    //        HttpContext.Current.Response.Write("         <td style='border:.5pt solid windowtext;'></td>");
    //        HttpContext.Current.Response.Write("    </tr>");


    //    }

    //    String HeaderStyle = "border:.5pt solid windowtext; font-weight:700;background:#D9D9D9;";
    //    HttpContext.Current.Response.Write("    <tr style='font-width:bold;'>");
    //    HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>#</td>");
    //    //   HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>TimeSheet ID</td>");
    //    HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Employee ID</td>");
    //    HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Employee</td>");
    //    HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Type</td>");
    //    HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Entry Date/Time</td>");
    //    HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Date</td>");
    //    HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>District</td>");
    //    HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Block</td>");
    //    HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Grampanchayat</td>");
    //    HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Village ID</td>");
    //    HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Village</td>");
    //    HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Start Entry Time</td>");
    //    HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Start Entry Location</td>");
    //    HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Start Time</td>");
    //    //  HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Start Time Change Reason</td>");
    //    HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>End Entry Time</td>");
    //    HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>End Entry Location</td>");
    //    HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>End Time</td>");
    //    //    HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>End Time Change Reason</td>");
    //    //     HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Mode</td>");
    //    HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Hours</td>");
    //    HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Start Distance</td>");
    //    HttpContext.Current.Response.Write("    </tr>");



    //    String DataStyle = "border:.5pt solid windowtext;";
    //    String DateTimeStyle = "mso-number-format:\"mm/dd/yyyy hh:mm AM/PM \";";
    //    String DateStyle = "mso-number-format:\"mm/dd/yyyy \";";
    //    String TimeStyle = "mso-number-format:\"hh:mm AM/PM \";";
    //    String InvalidGeoLocationStype = "background-color:red;";
    //    String ValidGeoLocationStype = "background-color:#99FF66;";

    //    var i = 0;
    //    double distance = 0;
    //    for (i = 0; i < dt.Rows.Count; i++)
    //    {

    //        var RowStyle = DataStyle;

    //        Int32 dHours = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Hours;
    //        Int32 dMins = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Minutes;

    //        TimeSpan dayJob = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"])).Subtract(Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"]));

    //        String villageGeoData = dt.Rows[i]["Village_GeoLocation"].ToString();
    //        abc1 = null;
    //        if (villageGeoData != null && villageGeoData != "" && villageGeoData != "[]")
    //        {
    //            if (dt.Rows[i]["StarttimeLocation"].ToString() == "" || dt.Rows[i]["StarttimeLocation"].ToString() == null || dt.Rows[i]["StarttimeLocation"].ToString() == "Unsupported browser" || dt.Rows[i]["StarttimeLocation"].ToString() == "User denied" || dt.Rows[i]["StarttimeLocation"].ToString() == "Location unavailable" || dt.Rows[i]["StarttimeLocation"].ToString() == "Request timed out" || dt.Rows[i]["StarttimeLocation"].ToString() == "Unknown error" || dt.Rows[i]["StarttimeLocation"].ToString() == "GPS turned off")
    //            {

    //            }
    //            else
    //            {
    //                if (villageGeoData.Length > 25)
    //                {

    //                    GeoUtils.GeoPointChecker geoChecker = new GeoUtils.GeoPointChecker(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData);
    //                    if (geoChecker.isValid() == false)
    //                    {
    //                        if (dt.Rows[i]["StarttimeLocation"].ToString().Length > 10)
    //                        {
    //                            if (distance <= 4)
    //                            {
    //                                //distance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData), 2);
    //                                //RowStyle += InvalidGeoLocationStype;
    //                                //abc1 = InvalidGeoLocationStype;
    //                                distance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData), 2);
    //                                RowStyle += ValidGeoLocationStype;
    //                                abc1 = ValidGeoLocationStype;
    //                            }
    //                            else
    //                            {
    //                                distance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData), 2);
    //                                RowStyle += InvalidGeoLocationStype;
    //                                abc1 = InvalidGeoLocationStype;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            //RowStyle += ValidGeoLocationStype;
    //                            //abc1 = ValidGeoLocationStype;
    //                            RowStyle += InvalidGeoLocationStype;
    //                            abc1 = InvalidGeoLocationStype;

    //                        }
    //                    }
    //                    else
    //                    {
    //                        RowStyle += ValidGeoLocationStype;
    //                        abc1 = ValidGeoLocationStype;

    //                    }
    //                }
    //                else
    //                {
    //                    RowStyle += ValidGeoLocationStype;
    //                    abc1 = ValidGeoLocationStype;

    //                }
    //            }
    //        }


    //        HttpContext.Current.Response.Write("<tr>");
    //        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + (i + 1) + "</td>");
    //        //retStr += "<td style='" + RowStyle + "'>" + mData.TimeSheet_ID + "</td>");
    //        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["UserName"].ToString() + "</td>");
    //        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["FristName"].ToString() + "</td>");
    //        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["Role"].ToString() + "</td>");
    //        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_StartTime"].ToString() + "</td>");
    //        HttpContext.Current.Response.Write("<td style='" + RowStyle + DateStyle + "'>" + Convert.ToDateTime(dt.Rows[i]["Date"].ToString()).ToString("dd/MM/yyy") + "</td>");
    //        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["DistrictName"].ToString() + "</td>");
    //        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["BlockName"].ToString() + "</td>");
    //        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["PanchayatName"].ToString() + "</td>");
    //        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["VillageCode"].ToString() + "</td>");
    //        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["VillageName"].ToString() + "</td>");
    //        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_StartTime"].ToString() + "</td>");
    //        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["StarttimeLocation"].ToString() + "</td>");
    //        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_EndTime"].ToString() + "</td>");
    //        //HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + mData.TimeSheet_StartTimeReasonText + "</td>");
    //        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_EndTime"].ToString() + "</td>");
    //        if (abc1 == "background-color:red);")
    //        {
    //            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_EndTime"].ToString() + "</td>");

    //        }
    //        else if (abc1 == ValidGeoLocationStype)
    //        {
    //            if (dt.Rows[i]["EndtimeLocation"].ToString() == "GPS turned off")
    //            {
    //                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");

    //            }
    //            else
    //            {
    //                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["EndtimeLocation"].ToString() + "</td>");
    //            }

    //        }

    //        else
    //        {
    //            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["EndtimeLocation"].ToString() + "</td>");
    //        }
    //        //retStr += "<td style='" + RowStyle + "'>" + mData.TimeSheet_EndLocation + "</td>";
    //        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_EndTime"].ToString() + "</td>");
    //        // HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + mData.TimeSheet_EndTimeReasonText + "</td>";
    //        //HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + mData.TimeSheet_Mode + "</td>";
    //        //retStr += "<td style='" + DataStyle + "'>" + dHours.ToString() + ":" + dMins.ToString() + "</td>";
    //        HttpContext.Current.Response.Write("<td style='" + RowStyle + "mso-number-format:\"[hh]:mm\";'>" + dayJob.Hours + ":" + dayJob.Minutes + "</td>");
    //        if (abc1 == "background-color:red;")
    //        {
    //            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + distance + "KM</td>");

    //        }
    //        else if (abc1 == "background-color:#99FF66")
    //        {
    //            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "NA" + "</td>");

    //        }
    //        else
    //        {
    //            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "NA" + "</td>");
    //        }
    //        distance = 0;
    //        HttpContext.Current.Response.Write("</tr>");

    //    }

    //    DataStyle += "background-color:yellow;";

    //    HttpContext.Current.Response.Write("</table>");
    //    HttpContext.Current.Response.Flush();
    //    HttpContext.Current.Response.End();


    //    //flushExcel(totDays.ToString());

    //    //var lq=dataList.Select(fld=>fld.TimeSheet_EndTime.

    //}


    private void GenerateExcel()
    {
        string abc1 = "";

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");
        string Fullfilename = "" + "LoginLogoutReport" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        // Int32 EmpID = Convert.ToInt32(Contx.Request["empid"]);

        DataTable dt = Session["MobileUser"] as DataTable;

        HttpContext.Current.Response.Write("<table style='border:.5pt solid windowtext;'>");

        HttpContext.Current.Response.Write("<tr>");
        HttpContext.Current.Response.Write("         <td colspan='7' style='text-align:center;'><h1>Timesheet report</h1></td>");
        HttpContext.Current.Response.Write("   </tr>");


        //DateTime FromDate = ConvertToEGDateTime(txtDate.Text);
        //DateTime ToDate = ConvertToEGDateTime(txtTodate.Text);



        //TimeSpan spanTime = (ToDate - FromDate);

        //Int32 totDays = spanTime.Days;




        if (dt.Rows.Count > 0)
        {



            //retStr += "    <tr>";
            //retStr += "         <td style='border:.5pt solid windowtext;font-weight:700;'>To Date</td>";
            //retStr += "         <td style='border:.5pt solid windowtext;'>" + ToDate.ToShortDateString() + "</td>";
            //retStr += "         <td style='border:.5pt solid windowtext;font-weight:700;'></td>";
            //retStr += "         <td style='border:.5pt solid windowtext;'></td>";
            //retStr += "    </tr>";

            HttpContext.Current.Response.Write("    <tr>");
            HttpContext.Current.Response.Write("         <td style='border:.5pt solid windowtext;font-weight:700;'>Generated On</td>");
            HttpContext.Current.Response.Write("         <td style='border:.5pt solid windowtext;'>" + DateTime.Now.ToString() + "</td>");
            HttpContext.Current.Response.Write("         <td style='border:.5pt solid windowtext;font-weight:700;'></td>");
            HttpContext.Current.Response.Write("         <td style='border:.5pt solid windowtext;'></td>");
            HttpContext.Current.Response.Write("    </tr>");


        }

        String HeaderStyle = "border:.5pt solid windowtext; font-weight:700;background:#D9D9D9;";
        HttpContext.Current.Response.Write("    <tr style='font-width:bold;'>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>#</td>");
        //   HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>TimeSheet ID</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Employee ID</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Employee</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Type</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Entry Date/Time</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Date</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>District</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Block</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Grampanchayat</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Village ID</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Village</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Start Entry Time</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Start Entry Location</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Start Time</td>");
        //  HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Start Time Change Reason</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>End Entry Time</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>End Entry Location</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>End Time</td>");
        //    HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>End Time Change Reason</td>");
        //     HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Mode</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Hours</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Start Distance</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Version No</td>");
        HttpContext.Current.Response.Write("    </tr>");



        String DataStyle = "border:.5pt solid windowtext;";
        String DateTimeStyle = "mso-number-format:\"mm/dd/yyyy hh:mm AM/PM \";";
        String DateStyle = "mso-number-format:\"mm/dd/yyyy \";";
        String TimeStyle = "mso-number-format:\"hh:mm AM/PM \";";
        String InvalidGeoLocationStype = "background-color:red;";
        String ValidGeoLocationStype = "background-color:#99FF66;";
        string str = "";
        var i = 0;
        double distance = 0;
        for (i = 0; i < dt.Rows.Count; i++)
        {
            str = "";
            var RowStyle = DataStyle;

            Int32 dHours = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Hours;
            Int32 dMins = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Minutes;

            TimeSpan dayJob = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"])).Subtract(Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"]));

            String villageGeoData = dt.Rows[i]["Village_GeoLocation"].ToString();
            abc1 = null;
            if (villageGeoData != null && villageGeoData != "" && villageGeoData != "[]")
            {
                if (dt.Rows[i]["StarttimeLocation"].ToString() == "" || dt.Rows[i]["StarttimeLocation"].ToString() == null || dt.Rows[i]["StarttimeLocation"].ToString() == "Unsupported browser" || dt.Rows[i]["StarttimeLocation"].ToString() == "User denied" || dt.Rows[i]["StarttimeLocation"].ToString() == "Location unavailable" || dt.Rows[i]["StarttimeLocation"].ToString() == "Request timed out" || dt.Rows[i]["StarttimeLocation"].ToString() == "Unknown error" || dt.Rows[i]["StarttimeLocation"].ToString() == "GPS turned off")
                {

                }
                else
                {
                    if (villageGeoData.Length > 200)
                    {

                        GeoUtils.GeoPointChecker geoChecker = new GeoUtils.GeoPointChecker(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData);
                        if (geoChecker.isValid() == false)
                        {
                            if (dt.Rows[i]["StarttimeLocation"].ToString().Length > 10)
                            {
                                if (distance <= 4)
                                {
                                    //distance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData), 2);
                                    //RowStyle += InvalidGeoLocationStype;
                                    //abc1 = InvalidGeoLocationStype;
                                    distance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData), 2);
                                    RowStyle += ValidGeoLocationStype;
                                    abc1 = ValidGeoLocationStype;
                                    str = "1";
                                }
                                else
                                {
                                    distance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData), 2);
                                    RowStyle += InvalidGeoLocationStype;
                                    abc1 = InvalidGeoLocationStype;
                                }
                            }
                            else
                            {
                                //RowStyle += ValidGeoLocationStype;
                                //abc1 = ValidGeoLocationStype;
                                RowStyle += InvalidGeoLocationStype;
                                abc1 = InvalidGeoLocationStype;

                            }
                        }
                        else
                        {
                            RowStyle += ValidGeoLocationStype;
                            abc1 = ValidGeoLocationStype;

                        }
                    }
                    else
                    {
                        RowStyle += ValidGeoLocationStype;
                        abc1 = ValidGeoLocationStype;

                    }
                }
            }


            HttpContext.Current.Response.Write("<tr>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + (i + 1) + "</td>");
            //retStr += "<td style='" + RowStyle + "'>" + mData.TimeSheet_ID + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["UserName"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["FristName"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["Role"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_StartTime"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + DateStyle + "'>" + Convert.ToDateTime(dt.Rows[i]["Date"].ToString()).ToString("dd/MM/yyy") + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["DistrictName"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["BlockName"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["PanchayatName"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["VillageCode"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["VillageName"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_StartTime"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["StarttimeLocation"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_EndTime"].ToString() + "</td>");
            //HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + mData.TimeSheet_StartTimeReasonText + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_EndTime"].ToString() + "</td>");
            if (abc1 == "background-color:red);")
            {
                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_EndTime"].ToString() + "</td>");

            }
            else if (abc1 == ValidGeoLocationStype)
            {
                if (dt.Rows[i]["EndtimeLocation"].ToString() == "GPS turned off")
                {
                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");

                }
                else
                {
                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["EndtimeLocation"].ToString() + "</td>");
                }

            }

            else
            {
                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["EndtimeLocation"].ToString() + "</td>");
            }
            //retStr += "<td style='" + RowStyle + "'>" + mData.TimeSheet_EndLocation + "</td>";
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_EndTime"].ToString() + "</td>");
            // HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + mData.TimeSheet_EndTimeReasonText + "</td>";
            //HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + mData.TimeSheet_Mode + "</td>";
            //retStr += "<td style='" + DataStyle + "'>" + dHours.ToString() + ":" + dMins.ToString() + "</td>";
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "mso-number-format:\"[hh]:mm\";'>" + dayJob.Hours + ":" + dayJob.Minutes + "</td>");
            if (abc1 == "background-color:red;")
            {
                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + distance + "KM</td>");

            }
            if (str == "1")
            {
                HttpContext.Current.Response.Write("<td style='" + "background-color:red;" + "'>" + distance + "KM</td>");

            }

            else if (abc1 == "background-color:#99FF66")
            {
                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "NA" + "</td>");

            }
            else
            {
                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "NA" + "</td>");
            }
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["VersionCodeNo"].ToString() + "</td>");
            distance = 0;
            HttpContext.Current.Response.Write("</tr>");

        }

        DataStyle += "background-color:yellow;";

        HttpContext.Current.Response.Write("</table>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();


        //flushExcel(totDays.ToString());

        //var lq=dataList.Select(fld=>fld.TimeSheet_EndTime.

    }



    private void flushExcel(string str)
    {
        Contx.Response.Write(str);
        Contx.Response.Flush();

    }


    public void LoadReportBOAdmin()
    {

        conditions = "";
        string conditions1 = "where 1=1";
        lblTotalCount.Text = "";
        conditions = "where 1=1  ";
        string ddlDistrict = "";

        string ddlState = "";
        string ddlBlock = "";

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

        //if (ddlYear.SelectedIndex > 0)
        //{
        //    conditions += " and mst5Village.Fyear= '" + ddlYear.SelectedItem.Text + "' ";
        //}
        //if (ddlState.Length > 0)
        //{
        //    conditions += "  and mst5Village.StateCode in( " + ddlState + ") ";

        //}
        //if (ddlDistrict.Length > 0)
        //{
        //    conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";

        //}
        //if (ddlBlock.Length > 0)
        //{
        //    conditions += " and mst5Village.BlockCode in(" + ddlBlock + ") ";

        //}

        //if (ddlUser.SelectedIndex > 0)
        //{
        //    conditions += " and Tbl_User_LoginBO.UserId =  '" + ddlUser.SelectedValue + "' ";

        //}






        //    if (txtDate.Text != "")
        //    {
        //        DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
        //        conditions1= " and Date >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
        //    }
        //    if (txtTodate.Text != "")
        //    {
        //        DateTime Todate = Convert.ToDateTime(txtTodate.Text);
        //        conditions1= " and Date <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";

        //    }
        //    if (txtDate.Text != "" && txtTodate.Text != "")
        //    {
        //        DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
        //        DateTime Todate = Convert.ToDateTime(txtTodate.Text);
        //        conditions1= " and Date BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
        //    }

        //    if (ddlYear.SelectedIndex > 0)
        //    {
        //        string Year = ddlYear.SelectedItem.Text;
        //        string[] Year1 = Year.Split('-');
        //        conditions1 += "    And Date >= '" + Year1[0] + "-04-01' and Date<='" + Year1[1] + "-03-31'";


        //    }
        //    string mainCon = conditions + conditions1;
        //    DataTable dt = objMain.tblReportBO("", "", "", mainCon);

        if (Session["FinYear"].ToString() == ddlYear.SelectedItem.Text)
        {
            if (ddlState.Length > 0)
            {
                conditions += "  and StateCode in( " + ddlState + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and (DistrictCode in(" + ddlDistrict + ") or BaseDist  in(" + ddlDistrict + "))  ";

            }
            if (ddlBlock.Length > 0)
            {
                conditions += " and BlockCode in(" + ddlBlock + ") ";

            }
            if (ddlType.SelectedIndex > 0)
            {
                if (Convert.ToInt32(ddlType.SelectedValue) == 1)
                {
                    if (ddlUser.SelectedIndex > 0)
                    {
                        conditions += " and Tbl_User_Login.UserId =  '" + ddlUser.SelectedValue + "' ";

                    }
                }
                else
                {
                    if (ddlUser.SelectedIndex > 0)
                    {
                        conditions += " and Tbl_User_LoginBo.UserId =  '" + ddlUser.SelectedValue + "' ";

                    }
                }

            }

        }
        else
        {

            if (ddlDistrict.Length > 0)
            {

                conditions += " and (TempEGDIst in(" + ddlDistrict + ") or tempBaseDist  in(" + ddlDistrict + "))  ";
            }
            if (ddlBlock.Length > 0)
            {
                conditions += " and TempEGBlock in(" + ddlBlock + ") ";

            }

            if (ddlUser.SelectedIndex > 0)
            {
                conditions += " and Tbl_User_Login.UserId =  '" + ddlUser.SelectedValue + "' ";

            }
        }




        //if (txtDate.Text != "")
        //{
        //    DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
        //    conditions1= " and Date >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
        //}
        //if (txtTodate.Text != "")
        //{
        //    DateTime Todate = Convert.ToDateTime(txtTodate.Text);
        //    conditions1= " and Date <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";

        //}
        if (txtDate.Text != "" && txtTodate.Text != "")
        {
            DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
            DateTime Todate = Convert.ToDateTime(txtTodate.Text);
            conditions1 += " and Date BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
        }

        if (ddlYear.SelectedIndex > 0)
        {
            string Year = ddlYear.SelectedItem.Text;
            string[] Year1 = Year.Split('-');
            conditions1 += "    And Date >= '" + Year1[0] + "-04-01' and Date<='" + Year1[1] + "-03-31'";


        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions1 += " and mst5Village.Fyear= '" + ddlYear.SelectedItem.Text + "' ";
        }
        string mainCon = conditions + conditions1;
        //DataTable dt = objMain.tblReport("", "", "", mainCon);

        SqlParameter[] cmdParameters = new SqlParameter[]
            {

                new SqlParameter("@condtion", conditions),
            new SqlParameter("@condtion1", conditions1),


            };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTblUserLoginBO]", cmdParameters);

        if (dt.Rows.Count > 0)
        {
            Session["MobileUser"] = dt;
            GenerateExcelNewStringBuldBOTest();
            //gvd2dBo.DataSource = dt;
            //gvd2dBo.DataBind();

            lblTotalCount.Text = (dt.Rows.Count).ToString();
        }
        else
        {
            gvd2dBo.DataSource = null;
            gvd2dBo.DataBind();
            lblTotalCount.Text = "";
        }



    }


    public void LoadReportBO()
    {

        conditions = "";
        string conditions1 = "where 1=1";
        lblTotalCount.Text = "";
        conditions = "where 1=1  ";
        string ddlDistrict = "";

        string ddlState = "";
        string ddlBlock = "";

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

        //if (ddlYear.SelectedIndex > 0)
        //{
        //    conditions += " and mst5Village.Fyear= '" + ddlYear.SelectedItem.Text + "' ";
        //}
        //if (ddlState.Length > 0)
        //{
        //    conditions += "  and mst5Village.StateCode in( " + ddlState + ") ";

        //}
        //if (ddlDistrict.Length > 0)
        //{
        //    conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";

        //}
        //if (ddlBlock.Length > 0)
        //{
        //    conditions += " and mst5Village.BlockCode in(" + ddlBlock + ") ";

        //}

        //if (ddlUser.SelectedIndex > 0)
        //{
        //    conditions += " and Tbl_User_LoginBO.UserId =  '" + ddlUser.SelectedValue + "' ";

        //}






        //    if (txtDate.Text != "")
        //    {
        //        DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
        //        conditions1= " and Date >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
        //    }
        //    if (txtTodate.Text != "")
        //    {
        //        DateTime Todate = Convert.ToDateTime(txtTodate.Text);
        //        conditions1= " and Date <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";

        //    }
        //    if (txtDate.Text != "" && txtTodate.Text != "")
        //    {
        //        DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
        //        DateTime Todate = Convert.ToDateTime(txtTodate.Text);
        //        conditions1= " and Date BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
        //    }

        //    if (ddlYear.SelectedIndex > 0)
        //    {
        //        string Year = ddlYear.SelectedItem.Text;
        //        string[] Year1 = Year.Split('-');
        //        conditions1 += "    And Date >= '" + Year1[0] + "-04-01' and Date<='" + Year1[1] + "-03-31'";


        //    }
        //    string mainCon = conditions + conditions1;
        //    DataTable dt = objMain.tblReportBO("", "", "", mainCon);

        if (Session["FinYear"].ToString() == ddlYear.SelectedItem.Text)
        {
            if (ddlState.Length > 0)
            {
                conditions += "  and StateCode in( " + ddlState + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and (DistrictCode in(" + ddlDistrict + ") or BaseDist  in(" + ddlDistrict + "))  ";

            }
            if (ddlBlock.Length > 0)
            {
                conditions += " and BlockCode in(" + ddlBlock + ") ";

            }
            if (ddlType.SelectedIndex > 0)
            {
                if (Convert.ToInt32(ddlType.SelectedValue) == 1)
                {
                    if (ddlUser.SelectedIndex > 0)
                    {
                        conditions += " and mstuser.UserId =  '" + ddlUser.SelectedValue + "' ";

                    }
                }
                else
                {
                    if (ddlUser.SelectedIndex > 0)
                    {
                        conditions += " and mstuser.UserId =  '" + ddlUser.SelectedValue + "' ";

                    }
                }

            }

        }

        else if (ddlYear.SelectedItem.Text == "2025-2026")
        {
            if (ddlState.Length > 0)
            {
                conditions += "  and StateCode in( " + ddlState + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and (DistrictCode in(" + ddlDistrict + ") or BaseDist  in(" + ddlDistrict + "))  ";

            }
            if (ddlBlock.Length > 0)
            {
                conditions += " and BlockCode in(" + ddlBlock + ") ";

            }

            if (ddlUser.SelectedIndex > 0)
            {
                conditions += " and mstuser2026.UserId =  '" + ddlUser.SelectedValue + "' ";

            }
        }
        else if (ddlYear.SelectedItem.Text == "2023-2024")
        {
            if (ddlState.Length > 0)
            {
                conditions += "  and StateCode in( " + ddlState + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and (DistrictCode in(" + ddlDistrict + ") or BaseDist  in(" + ddlDistrict + "))  ";

            }
            if (ddlBlock.Length > 0)
            {
                conditions += " and BlockCode in(" + ddlBlock + ") ";

            }

            if (ddlUser.SelectedIndex > 0)
            {
                conditions += " and mstuser2024.UserId =  '" + ddlUser.SelectedValue + "' ";

            }
        }
        else
        {

            if (ddlState.Length > 0)
            {
                conditions += "  and StateCode in( " + ddlState + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and (DistrictCode in(" + ddlDistrict + ") or BaseDist  in(" + ddlDistrict + "))  ";

            }
            if (ddlBlock.Length > 0)
            {
                conditions += " and BlockCode in(" + ddlBlock + ") ";

            }

            if (ddlUser.SelectedIndex > 0)
            {
                conditions += " and mstuser2025.UserId =  '" + ddlUser.SelectedValue + "' ";

            }
        }




        //if (txtDate.Text != "")
        //{
        //    DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
        //    conditions1= " and Date >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
        //}
        //if (txtTodate.Text != "")
        //{
        //    DateTime Todate = Convert.ToDateTime(txtTodate.Text);
        //    conditions1= " and Date <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";

        //}
        //if (txtDate.Text != "" && txtTodate.Text != "")
        //{
        //    DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
        //    DateTime Todate = Convert.ToDateTime(txtTodate.Text);
        //    conditions1 += " and Date BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
        //}
        if (txtDate.Text != "" && txtTodate.Text != "")
        {
            if (Convert.ToDateTime(txtDate.Text) == Convert.ToDateTime(txtTodate.Text))
            {


                if (Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd") == Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd"))
                {


                    DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                    DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                    conditions1 += " and year(Date)=" + DateTime.Now.Year + " and  month(Date)=" + DateTime.Now.Month + " and  day(Date)=" + DateTime.Now.Day + "";
                }
                else
                {
                    DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                    DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                    string[] multiArray = txtDate.Text.Split(new Char[] { '/' });
                    string[] multiArray1 = txtTodate.Text.Split(new Char[] { '/' });

                    string Fdate = Fromdate.Year + "" + multiArray[1] + "" + multiArray[0];
                    string Tdate = Todate.Year + "" + multiArray1[1] + "" + multiArray1[0];
                    conditions1 += " and (Year([Date])*10000)+(Month([Date])*100+Day([Date])) Between '" + Fdate + "' and '" + Tdate + "'";
                }
            }
            else
            {
                DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                string[] multiArray = txtDate.Text.Split(new Char[] { '/' });
                string[] multiArray1 = txtTodate.Text.Split(new Char[] { '/' });

                string Fdate = Fromdate.Year + "" + multiArray[1] + "" + multiArray[0];
                string Tdate = Todate.Year + "" + multiArray1[1] + "" + multiArray1[0];
                conditions1 += " and (Year([Date])*10000)+(Month([Date])*100+Day([Date])) Between '" + Fdate + "' and '" + Tdate + "'";

                // conditions1 += " and Date BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
            }
        }
        if (ddlYear.SelectedIndex > 0)
        {
            string Year = ddlYear.SelectedItem.Text;
            string[] Year1 = Year.Split('-');
            conditions1 += "    And Date >= '" + Year1[0] + "-04-01' and Date<='" + Year1[1] + "-03-31'";


        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions1 += " and mst5Village.Fyear= '" + ddlYear.SelectedItem.Text + "' ";
        }
        string mainCon = conditions + conditions1;
        //DataTable dt = objMain.tblReport("", "", "", mainCon);
        DataTable dt = null;
        if (Session["FinYear"].ToString() == ddlYear.SelectedItem.Text)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
           {

                new SqlParameter("@condtion", conditions),
            new SqlParameter("@condtion1", conditions1),


           };
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTblUserLoginBONew2023]", cmdParameters);

        }
        else if (ddlYear.SelectedItem.Text == "2025-2026")
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
           {

                new SqlParameter("@condtion", conditions),
            new SqlParameter("@condtion1", conditions1),


           };
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTblUserLoginBONew20252026New]", cmdParameters);
        }
        else if (ddlYear.SelectedItem.Text == "2023-2024")
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
           {

                new SqlParameter("@condtion", conditions),
            new SqlParameter("@condtion1", conditions1),


           };
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTblUserLoginBONew2023Back2024]", cmdParameters);
        }
        else
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {

                new SqlParameter("@condtion", conditions),
            new SqlParameter("@condtion1", conditions1),


            };
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTblUserLoginBONew2023Back]", cmdParameters);
        }
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows.Count > 300)
            {
                gvd2dBo.DataSource = null;
                gvd2dBo.DataBind();
                Session["MobileUser"] = dt;
                lblTotalCount.Text = (dt.Rows.Count).ToString();
                //   LinkButton1.Visible = true;
                lnkCSV.Visible = true;
                btnCSV_Click(lnkCSV, null);
            }
            else
            {
                gvd2dBo.DataSource = dt;
                gvd2dBo.DataBind();
                Session["MobileUser"] = dt;
                lblTotalCount.Text = (dt.Rows.Count).ToString();
                // LinkButton1.Visible = true;
                lnkCSV.Visible = true;
            }
        }
        else
        {
            Session["MobileUser"] = null;
            gvd2dBo.DataSource = null;
            gvd2dBo.DataBind();
            lblTotalCount.Text = "";
            //  LinkButton1.Visible = false;
            lnkCSV.Visible = false;
        }



    }


    public void LoadReportBOFCReport()
    {

        string conditions5 = "where 1=1 ";
        string conditions6 = "where 1=1";
        lblTotalCount.Text = "";
        conditions = "where 1=1  ";
        string ddlDistrict = "";

        string ddlState = "";
        string ddlBlock = "";

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

        //if (ddlYear.SelectedIndex > 0)
        //{
        //    conditions += " and mst5Village.Fyear= '" + ddlYear.SelectedItem.Text + "' ";
        //}
        //if (ddlState.Length > 0)
        //{
        //    conditions += "  and mst5Village.StateCode in( " + ddlState + ") ";

        //}
        //if (ddlDistrict.Length > 0)
        //{
        //    conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";

        //}
        //if (ddlBlock.Length > 0)
        //{
        //    conditions += " and mst5Village.BlockCode in(" + ddlBlock + ") ";

        //}

        //if (ddlUser.SelectedIndex > 0)
        //{
        //    conditions += " and Tbl_User_LoginBO.UserId =  '" + ddlUser.SelectedValue + "' ";

        //}






        //    if (txtDate.Text != "")
        //    {
        //        DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
        //        conditions1= " and Date >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
        //    }
        //    if (txtTodate.Text != "")
        //    {
        //        DateTime Todate = Convert.ToDateTime(txtTodate.Text);
        //        conditions1= " and Date <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";

        //    }
        //    if (txtDate.Text != "" && txtTodate.Text != "")
        //    {
        //        DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
        //        DateTime Todate = Convert.ToDateTime(txtTodate.Text);
        //        conditions1= " and Date BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
        //    }

        //    if (ddlYear.SelectedIndex > 0)
        //    {
        //        string Year = ddlYear.SelectedItem.Text;
        //        string[] Year1 = Year.Split('-');
        //        conditions1 += "    And Date >= '" + Year1[0] + "-04-01' and Date<='" + Year1[1] + "-03-31'";


        //    }
        //    string mainCon = conditions + conditions1;
        //    DataTable dt = objMain.tblReportBO("", "", "", mainCon);

        if (Session["FinYear"].ToString() == ddlYear.SelectedItem.Text)
        {
            if (ddlState.Length > 0)
            {
                conditions5 += "  and StateCode in( " + ddlState + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions5 += " and (DistrictCode in(" + ddlDistrict + ") or BaseDist  in(" + ddlDistrict + "))  ";

            }
            if (ddlBlock.Length > 0)
            {
                conditions5 += " and BlockCode in(" + ddlBlock + ") ";

            }
            if (ddlType.SelectedIndex > 0)
            {
                if (Convert.ToInt32(ddlType.SelectedValue) == 1)
                {
                    //if (ddlUser.SelectedIndex > 0)
                    //{
                    //    conditions5 += " and  mstuser.UserID =  '" + ddlUser.SelectedValue + "' ";

                    //}
                }
                else
                {
                    //if (ddlUser.SelectedIndex > 0)
                    //{
                    //    conditions5 += " and mstuser.UserID  =  '" + ddlUser.SelectedValue + "' ";

                    //}
                }

            }

        }
        else
        {

            if (ddlDistrict.Length > 0)
            {

                conditions5 += " and (TempEGDIst in(" + ddlDistrict + ") or tempBaseDist  in(" + ddlDistrict + "))  ";
            }
            if (ddlBlock.Length > 0)
            {
                conditions5 += " and TempEGBlock in(" + ddlBlock + ") ";

            }

            //if (ddlUser.SelectedIndex > 0)
            //{
            //    conditions5 += " and mstuser.UserID  =  '" + ddlUser.SelectedValue + "' ";

            //}
        }




        //if (txtDate.Text != "")
        //{
        //    DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
        //    conditions1= " and Date >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
        //}
        //if (txtTodate.Text != "")
        //{
        //    DateTime Todate = Convert.ToDateTime(txtTodate.Text);
        //    conditions1= " and Date <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";

        //}
        if (txtDate.Text != "" && txtTodate.Text != "")
        {
            DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
            DateTime Todate = Convert.ToDateTime(txtTodate.Text);
            conditions6 += " and Date BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
        }

        if (ddlYear.SelectedIndex > 0)
        {
            string Year = ddlYear.SelectedItem.Text;
            string[] Year1 = Year.Split('-');
            conditions6 += "    And Date >= '" + Year1[0] + "-04-01' and Date<='" + Year1[1] + "-03-31'";


        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions6 += " and mst5Village.Fyear= '" + ddlYear.SelectedItem.Text + "' ";
        }
        string mainCon = conditions5 + conditions6;
        //DataTable dt = objMain.tblReport("", "", "", mainCon);



        conditions = "";
        string conditions1 = "where 1=1 ";
        lblTotalCount.Text = "";
        conditions = "where 1=1  ";
        string ddlDistrict1 = "";

        string ddlState1 = "";
        string ddlBlock1 = "";

        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlState1 += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlState1.Length > 0)
        {
            ddlState1 = ddlState1.Substring(0, ddlState1.LastIndexOf(","));
        }

        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict1 += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict1.Length > 0)
        {
            ddlDistrict1 = ddlDistrict1.Substring(0, ddlDistrict1.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock1 += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock1.Length > 0)
        {
            ddlBlock1 = ddlBlock1.Substring(0, ddlBlock1.LastIndexOf(","));
        }

        //  if (ddlYear.SelectedIndex > 0)
        //{
        //    conditions += " and mst5Village.Fyear= '" + ddlYear.SelectedItem.Text + "' ";
        //}
        if (Session["FinYear"].ToString() == ddlYear.SelectedItem.Text)
        {
            if (ddlState.Length > 0)
            {
                conditions += "  and StateCode in( " + ddlState1 + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and DistrictCode in(" + ddlDistrict1 + ") ";

            }
            if (ddlBlock.Length > 0)
            {
                conditions += " and BlockCode in(" + ddlBlock1 + ") ";

            }

            if (ddlUser.SelectedIndex > 0)
            {
                //  conditions += " and mstuser.UserID  =  '" + ddlUser.SelectedValue + "' ";

            }

        }
        else
        {
            if (ddlState.Length > 0)
            {
                conditions += "  and StateCode in( " + ddlState1 + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and DistrictCode in(" + ddlDistrict1 + ") ";

            }
            if (ddlBlock.Length > 0)
            {
                conditions += " and BlockCode in(" + ddlBlock1 + ") ";

            }

            //if (ddlUser.SelectedIndex > 0)
            //{
            //    conditions += " and mstuser.UserID  =  '" + ddlUser.SelectedValue + "' ";

            //}
        }




        //if (txtDate.Text != "")
        //{
        //    DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
        //    conditions1= " and Date >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
        //}
        //if (txtTodate.Text != "")
        //{
        //    DateTime Todate = Convert.ToDateTime(txtTodate.Text);
        //    conditions1= " and Date <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";

        //}
        if (txtDate.Text != "" && txtTodate.Text != "")
        {
            DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
            DateTime Todate = Convert.ToDateTime(txtTodate.Text);
            conditions1 += " and Date BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
        }

        if (ddlYear.SelectedIndex > 0)
        {
            string Year = ddlYear.SelectedItem.Text;
            string[] Year1 = Year.Split('-');
            conditions1 += "    And Date >= '" + Year1[0] + "-04-01' and Date<='" + Year1[1] + "-03-31'";


        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions1 += " and mst5Village.Fyear= '" + ddlYear.SelectedItem.Text + "' ";
        }
        string mainCon1 = conditions + conditions1;

        SqlParameter[] cmdParameters = new SqlParameter[]
            {

                new SqlParameter("@conditions5", conditions5),
            new SqlParameter("@conditions6", conditions6),

               new SqlParameter("@condtion", conditions),
            new SqlParameter("@condtion1", conditions1),




            };
        DataSet dt = null;
        if (Session["FinYear"].ToString() == ddlYear.SelectedItem.Text)
        {
            dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptFCBoReport]", cmdParameters);
        }
        else
        {
            dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptFCBoReport2024]", cmdParameters);
        }

        if (dt.Tables[0].Rows.Count > 0)
        {
            ViewState["SAC"] = dt;
            MultipuExeclFC();
        }
        else
        {
            gvd2dBo.DataSource = null;
            gvd2dBo.DataBind();
            lblTotalCount.Text = "";
        }



    }
    public void MultipuExeclFC()
    {
        DataSet dt4 = ViewState["SAC"] as DataSet;
        DataTable dt = dt4.Tables[0];
        DataTable dt1 = dt4.Tables[1];
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\FCVisitReport.xlsx");
        var ws = wb.Worksheet(2);

        var ws1 = wb.Worksheet(3);
        // ws1.Cell(2, 1).InsertData(dt.Rows);
        ws1.Cell(1, 1).Value = "StateCode";
        for (int i = 1; i < dt1.Columns.Count; i++)
        {
            ws1.Cell(1, i + 1).Value = dt1.Columns[i].ColumnName;

        }

        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("RowNo");
        ws.Cell(2, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 1;
        string str = "A2:R" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
        string m = "I";
        if (dt1.Columns.Count == 5)
        {
            m = "E";
        }
        if (dt1.Columns.Count == 6)
        {
            m = "F";
        }
        if (dt1.Columns.Count == 7)
        {
            m = "G";
        }
        if (dt1.Columns.Count == 8)
        {
            m = "H";
        }
        if (dt1.Columns.Count == 9)
        {
            m = "I";
        }
        if (dt1.Columns.Count == 10)
        {
            m = "J";
        }

        if (dt1.Columns.Count == 11)
        {
            m = "K";
        }
        else if (dt1.Columns.Count == 12)
        {
            m = "L";
        }
        else if (dt1.Columns.Count == 13)
        {
            m = "M";
        }
        else if (dt1.Columns.Count == 14)
        {
            m = "N";
        }
        else if (dt1.Columns.Count == 15)
        {
            m = "O";
        }
        else if (dt1.Columns.Count == 16)
        {
            m = "P";
        }
        else if (dt1.Columns.Count == 17)
        {
            m = "Q";
        }
        else if (dt1.Columns.Count == 18)
        {
            m = "R";
        }
        else if (dt1.Columns.Count == 19)
        {
            m = "S";
        }
        else if (dt1.Columns.Count == 20)
        {
            m = "T";
        }
        else if (dt1.Columns.Count == 21)
        {
            m = "U";
        }
        else if (dt1.Columns.Count == 22)
        {
            m = "V";
        }
        else if (dt1.Columns.Count == 23)
        {
            m = "W";
        }
        else if (dt1.Columns.Count == 24)
        {
            m = "X";
        }
        else if (dt1.Columns.Count == 25)
        {
            m = "Y";
        }
        else if (dt1.Columns.Count == 26)
        {
            m = "Z";
        }
        else if (dt1.Columns.Count == 27)
        {
            m = "AA";
        }
        else if (dt1.Columns.Count == 27)
        {
            m = "AB";
        }
        ws1.Cell(2, 1).InsertData(dt1.Rows);
        Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 1;
        string str2 = "A1:" + m + ii1;
        ws1.Range(str2).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws1.Range(str2).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws1.Range(str2).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws1.Range(str2).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
        filepath = StartupPath + "\\FCVisitReport" + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
        wb.SaveAs(filepath);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
        Response.WriteFile(filepath);

        Response.End();
        if (File.Exists(filepath))
        {
            System.IO.File.Delete(filepath);
        }

    }

    protected void GridView1Bo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTimeSheet_StartTime = (Label)e.Row.FindControl("lblTimeSheet_StartTime");
            Label lblTimeSheet_EndTime = (Label)e.Row.FindControl("lblTimeSheet_EndTime");
            Label lblHours = (Label)e.Row.FindControl("lblHours");
            Label lblStarttimeLocation = (Label)e.Row.FindControl("lblStarttimeLocation");
            Label lblEndtimeLocation = (Label)e.Row.FindControl("lblEndtimeLocation");
            Label lblVillage_GeoLocation = (Label)e.Row.FindControl("lblVillage_GeoLocation");
            Int32 dHours = (Convert.ToDateTime(lblTimeSheet_EndTime.Text) - Convert.ToDateTime(lblTimeSheet_StartTime.Text)).Hours;
            Int32 dMins = (Convert.ToDateTime(lblTimeSheet_EndTime.Text) - Convert.ToDateTime(lblTimeSheet_StartTime.Text)).Minutes;

            Label L1 = (Label)e.Row.FindControl("L1");
            Label L2 = (Label)e.Row.FindControl("L2");
            Label L3 = (Label)e.Row.FindControl("L3");
            Label L4 = (Label)e.Row.FindControl("L4");

            string L = L1 + "," + L2;
            string E = L3 + "," + L4;




            string retStr = dHours.ToString() + ":" + dMins.ToString();
            lblHours.Text = retStr;

            DateTime fromTime = Convert.ToDateTime(lblTimeSheet_StartTime.Text);
            DateTime toTime = Convert.ToDateTime(lblTimeSheet_EndTime.Text);
            TimeSpan fromH = TimeSpan.FromHours(fromTime.Hour);
            TimeSpan toH = TimeSpan.FromHours(toTime.Hour);
            TimeSpan hourTotalSpan = toH.Subtract(fromH);
            e.Row.Cells[8].Text = "<a href='javascript:Page.ShowLocation(\"" + lblStarttimeLocation.Text + "\"," + lblVillage_GeoLocation.Text + ")'>" + lblStarttimeLocation.Text + "</a>";
            e.Row.Cells[10].Text = "<a href='javascript:Page.ShowLocation(\"" + lblEndtimeLocation.Text + "\"," + lblVillage_GeoLocation.Text + ")'>" + lblEndtimeLocation.Text + "</a>";


        }
    }
    protected void gvD2dBo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvd2dBo.PageIndex = e.NewPageIndex;
        if (Session["D2d"] != null)
        {
            DataTable dt = Session["D2d"] as DataTable;
            gvd2dBo.DataSource = dt;
            gvd2dBo.DataBind();
        }


    }
    private void GenerateExcelNewStringBuldBO()
    {
        string abc1 = "";
        string abc2 = "";

        //sw.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");
        string Fullfilename1 = "" + "DistrictLoginLogoutReport" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";
        string fileName = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + Fullfilename1 + "");
        StreamWriter sw = new StreamWriter(fileName, false);
        sw.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        sw.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");


        DataTable dt = Session["MobileUser"] as DataTable;

        sw.Write("<table style='border:.5pt solid windowtext;'>");

        sw.Write("<tr>");
        sw.Write("         <td colspan='7' style='text-align:center;'><h1>Timesheet report</h1></td>");
        sw.Write("   </tr>");


        //DateTime FromDate = ConvertToEGDateTime(txtDate.Text);
        //DateTime ToDate = ConvertToEGDateTime(txtTodate.Text);



        //TimeSpan spanTime = (ToDate - FromDate);

        //Int32 totDays = spanTime.Days;




        if (dt.Rows.Count > 0)
        {



            //retStr += "    <tr>";
            //retStr += "         <td style='border:.5pt solid windowtext;font-weight:700;'>To Date</td>";
            //retStr += "         <td style='border:.5pt solid windowtext;'>" + ToDate.ToShortDateString() + "</td>";
            //retStr += "         <td style='border:.5pt solid windowtext;font-weight:700;'></td>";
            //retStr += "         <td style='border:.5pt solid windowtext;'></td>";
            //retStr += "    </tr>";

            sw.Write("    <tr>");
            sw.Write("         <td style='border:.5pt solid windowtext;font-weight:700;'>Generated On</td>");
            sw.Write("         <td style='border:.5pt solid windowtext;'>" + DateTime.Now.ToString() + "</td>");
            sw.Write("         <td style='border:.5pt solid windowtext;font-weight:700;'></td>");
            sw.Write("         <td style='border:.5pt solid windowtext;'></td>");
            sw.Write("    </tr>");


        }

        String HeaderStyle = "border:.5pt solid windowtext; font-weight:700;background:#D9D9D9;";
        sw.Write("    <tr style='font-width:bold;'>");
        sw.Write("         <td style='" + HeaderStyle + "'>#</td>");
        //   sw.Write("         <td style='" + HeaderStyle + "'>TimeSheet ID</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Employee ID</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Employee</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Type</td>");

        sw.Write("         <td style='" + HeaderStyle + "'>Date</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>District</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Block</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Grampanchayat</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Cluster Name</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Cluster Code</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Village ID</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Village</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Entry Date/Time</td>");
        //   sw.Write("         <td style='" + HeaderStyle + "'>Start Entry Time</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Start Entry Location</td>");
        //sw.Write("         <td style='" + HeaderStyle + "'>Start Time</td>");
        //  sw.Write("         <td style='" + HeaderStyle + "'>Start Time Change Reason</td>");
        //sw.Write("         <td style='" + HeaderStyle + "'>End Entry Time</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>End Entry Location</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>End Entry Time</td>");
        //    sw.Write("         <td style='" + HeaderStyle + "'>End Time Change Reason</td>");
        //     sw.Write("         <td style='" + HeaderStyle + "'>Mode</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Hours</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Start Distance</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>END Distance</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>User  District</td>");
        //sw.Write("         <td style='" + HeaderStyle + "'>Version No</td>");
        sw.Write("    </tr>");



        String DataStyle = "border:.5pt solid windowtext;";
        String DateTimeStyle = "mso-number-format:\"mm/dd/yyyy hh:mm AM/PM \";";
        String DateStyle = "mso-number-format:\"mm/dd/yyyy \";";
        String TimeStyle = "mso-number-format:\"hh:mm AM/PM \";";
        String InvalidGeoLocationStype = "background-color:red;";
        String ValidGeoLocationStype = "background-color:#99FF66;";

        var i = 0;
        double distance = 0;
        double Enddistance = 0;
        for (i = 0; i < dt.Rows.Count; i++)
        {

            var RowStyle = DataStyle;

            Int32 dHours = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Hours;
            Int32 dMins = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Minutes;

            TimeSpan dayJob = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"])).Subtract(Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"]));

            String villageGeoData = dt.Rows[i]["Village_GeoLocation"].ToString();
            abc1 = null;
            abc2 = null;
            if (villageGeoData != null && villageGeoData != "" && villageGeoData != "[]")
            {
                if (dt.Rows[i]["StarttimeLocation"].ToString() == "" || villageGeoData.Length < 100 || dt.Rows[i]["StarttimeLocation"].ToString() == null || dt.Rows[i]["StarttimeLocation"].ToString() == "Unsupported browser" || dt.Rows[i]["StarttimeLocation"].ToString() == "User denied" || dt.Rows[i]["StarttimeLocation"].ToString() == "Location unavailable" || dt.Rows[i]["StarttimeLocation"].ToString() == "Request timed out" || dt.Rows[i]["StarttimeLocation"].ToString() == "Unknown error" || dt.Rows[i]["StarttimeLocation"].ToString() == "GPS turned off")
                {

                }
                else
                {
                    if (dt.Rows[i]["UserName"].ToString() == "EGE3662")
                    {
                        string fff = "";
                    }
                    GeoUtils.GeoPointChecker geoChecker = new GeoUtils.GeoPointChecker(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData);
                    //if (geoChecker.isValid() == false && dt.Rows[i]["StarttimeLocation"].ToString().Length > 4 && dt.Rows[i]["EndtimeLocation"].ToString().ToString().Length > 4)

                    if (dt.Rows[i]["StarttimeLocation"].ToString().Length > 4 && dt.Rows[i]["EndtimeLocation"].ToString().ToString().Length > 4)
                    {

                        if (geoChecker.isValid() == false)
                        {
                            distance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData), 2);
                            //double ddd = distance - distance1;

                            if (distance >= 4)
                            {
                                RowStyle += InvalidGeoLocationStype;
                                abc1 = InvalidGeoLocationStype;
                            }
                            else
                            {
                                RowStyle += ValidGeoLocationStype;
                                abc1 = ValidGeoLocationStype;
                            }
                        }
                        else
                        {
                            distance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData), 2);

                            RowStyle += ValidGeoLocationStype;
                            abc1 = ValidGeoLocationStype;

                        }
                    }
                    else
                    {

                        RowStyle += DataStyle;
                        abc1 = DataStyle;
                    }

                    GeoUtils.GeoPointChecker geoChecker1 = new GeoUtils.GeoPointChecker(dt.Rows[i]["EndtimeLocation"].ToString(), villageGeoData);
                    if (dt.Rows[i]["StarttimeLocation"].ToString().Length > 4 && dt.Rows[i]["EndtimeLocation"].ToString().ToString().Length > 4)
                    {
                        Enddistance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["EndtimeLocation"].ToString(), villageGeoData), 2);

                        abc2 = ValidGeoLocationStype;
                    }
                    else
                    {


                        abc2 = InvalidGeoLocationStype;
                    }
                }
            }


            sw.Write("<tr>");
            sw.Write("<td style='" + RowStyle + "'>" + (i + 1) + "</td>");
            //retStr += "<td style='" + RowStyle + "'>" + mData.TimeSheet_ID + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["UserName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["FristName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["Role"].ToString() + "</td>");

            sw.Write("<td style='" + RowStyle + DateStyle + "'>" + Convert.ToDateTime(dt.Rows[i]["Date"].ToString()).ToString("dd/MM/yyy") + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["DistrictName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["BlockName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["PanchayatName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["ClusterName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["ClusterCode"].ToString() + "</td>");

            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["VillageCode"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["VillageName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_StartTime"].ToString() + "</td>");
            //   sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_StartTime"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["StarttimeLocation"].ToString() + "</td>");
            //sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_EndTime"].ToString() + "</td>");
            //sw.Write("<td style='" + RowStyle + "'>" + mData.TimeSheet_StartTimeReasonText + "</td>");
            //sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_EndTime"].ToString() + "</td>");
            if (abc1 == "background-color:red);")
            {
                //sw.Write("<td style='" + RowStyle + "'>" + Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"].ToString()) + "</td>");

            }
            else if (abc1 == ValidGeoLocationStype)
            {
                if (dt.Rows[i]["EndtimeLocation"].ToString() == "GPS turned off")
                {
                    sw.Write("<td style='" + RowStyle + "'>" + "" + "</td>");

                }
                else
                {
                    sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["EndtimeLocation"].ToString() + "</td>");
                }

            }

            else
            {
                sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["EndtimeLocation"].ToString() + "</td>");
            }
            //retStr += "<td style='" + RowStyle + "'>" + mData.TimeSheet_EndLocation + "</td>";
            sw.Write("<td style='" + RowStyle + TimeStyle + "'>" + Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"].ToString()).ToShortTimeString() + "</td>");
            // sw.Write("<td style='" + RowStyle + "'>" + mData.TimeSheet_EndTimeReasonText + "</td>";
            //sw.Write("<td style='" + RowStyle + "'>" + mData.TimeSheet_Mode + "</td>";
            //retStr += "<td style='" + DataStyle + "'>" + dHours.ToString() + ":" + dMins.ToString() + "</td>";
            sw.Write("<td style='" + RowStyle + "mso-number-format:\"[hh]:mm\";'>" + dayJob.Hours + ":" + dayJob.Minutes + "</td>");
            //if (abc1 == "background-color:red;")
            //if (abc1 == "background-color:#99FF66")
            if (abc1 == "background-color:#99FF66;")
            {
                sw.Write("<td style='" + RowStyle + "'>" + distance + "KM</td>");

            }
            else if (abc1 == "background-color:red;")
            {
                sw.Write("<td style='" + RowStyle + "'>" + distance + "KM</td>");
                //sw.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");

            }
            else if (abc1 == "border:.5pt solid windowtext;")
            {
                //sw.Write("<td style='" + RowStyle + "'>" + distance + "KM</td>");
                sw.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");

            }
            else
            {
                sw.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");
            }

            if (abc2 == "background-color:#99FF66;")
            {
                sw.Write("<td style='" + RowStyle + "'>" + Enddistance + "KM</td>");

            }
            else if (abc2 == "background-color:red;")
            {
                //sw.Write("<td style='" + RowStyle + "'>" + "NA" + "KM</td>");
                sw.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");

            }
            else if (abc2 == "border:.5pt solid windowtext;")
            {
                //sw.Write("<td style='" + RowStyle + "'>" + distance + "KM</td>");
                sw.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");

            }
            else
            {
                sw.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");
            }
            //    sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["VersionCodeNo"].ToString() + "</td>");
            distance = 0;

            Enddistance = 0;
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["DistName"].ToString() + "</td>");
            sw.Write("</tr>");

        }

        DataStyle += "background-color:yellow;";

        sw.Write("</table>");

        sw.Close();



        FileStream fs = null;//, fs2=null;
        try
        {
            string path1 = Fullfilename1;
            string foldername = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + path1 + "");
            string datafolder = path1.Substring(0, path1.Length - 4);
            //  string[] file = Directory.GetFiles(foldername);
            string path = foldername;
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
        //flushExcel(totDays.ToString());

        //var lq=dataList.Select(fld=>fld.TimeSheet_EndTime.

    }

    private void GenerateExcelNewStringBuldBOTest()
    {
        string abc1 = "";
        string abc2 = "";
        //HttpContext.Current.Response.Clear();
        //HttpContext.Current.Response.ClearContent();
        //HttpContext.Current.Response.ClearHeaders();
        //HttpContext.Current.Response.Buffer = true;
        //HttpContext.Current.Response.ContentType = "application/ms-excel";
        //HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        //string Fullfilename = "" + "Employeetracking" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";

        //sw.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");
        string Fullfilename1 = "" + "DistrictLoginLogoutReport" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";
        string fileName = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + Fullfilename1 + "");
        StreamWriter sw = new StreamWriter(fileName, false);
        sw.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        sw.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");


        DataTable dt = Session["MobileUser"] as DataTable;

        sw.Write("<table style='border:.5pt solid windowtext;'>");

        sw.Write("<tr>");
        sw.Write("         <td colspan='7' style='text-align:center;'><h1>Timesheet report</h1></td>");
        sw.Write("   </tr>");


        //DateTime FromDate = ConvertToEGDateTime(txtDate.Text);
        //DateTime ToDate = ConvertToEGDateTime(txtTodate.Text);



        //TimeSpan spanTime = (ToDate - FromDate);

        //Int32 totDays = spanTime.Days;




        if (dt.Rows.Count > 0)
        {



            //retStr += "    <tr>";
            //retStr += "         <td style='border:.5pt solid windowtext;font-weight:700;'>To Date</td>";
            //retStr += "         <td style='border:.5pt solid windowtext;'>" + ToDate.ToShortDateString() + "</td>";
            //retStr += "         <td style='border:.5pt solid windowtext;font-weight:700;'></td>";
            //retStr += "         <td style='border:.5pt solid windowtext;'></td>";
            //retStr += "    </tr>";

            sw.Write("    <tr>");
            sw.Write("         <td style='border:.5pt solid windowtext;font-weight:700;'>Generated On</td>");
            sw.Write("         <td style='border:.5pt solid windowtext;'>" + DateTime.Now.ToString() + "</td>");
            sw.Write("         <td style='border:.5pt solid windowtext;font-weight:700;'></td>");
            sw.Write("         <td style='border:.5pt solid windowtext;'></td>");
            sw.Write("    </tr>");


        }

        String HeaderStyle = "border:.5pt solid windowtext; font-weight:700;";
        sw.Write("    <tr style='font-width:bold;'>");
        sw.Write("         <td style='" + HeaderStyle + "'>#</td>");
        //   sw.Write("         <td style='" + HeaderStyle + "'>TimeSheet ID</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Employee ID</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Employee</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Type</td>");

        sw.Write("         <td style='" + HeaderStyle + "'>Date</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>District</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Block</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Grampanchayat</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Cluster Name</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Cluster Code</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Village ID</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Village</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Entry Date/Time</td>");
        //   sw.Write("         <td style='" + HeaderStyle + "'>Start Entry Time</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Start Entry Location</td>");
        //sw.Write("         <td style='" + HeaderStyle + "'>Start Time</td>");
        //  sw.Write("         <td style='" + HeaderStyle + "'>Start Time Change Reason</td>");
        //sw.Write("         <td style='" + HeaderStyle + "'>End Entry Time</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>End Entry Location</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>End Entry Time</td>");
        //    sw.Write("         <td style='" + HeaderStyle + "'>End Time Change Reason</td>");
        //     sw.Write("         <td style='" + HeaderStyle + "'>Mode</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Hours</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>Start Distance</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>END Distance</td>");
        sw.Write("         <td style='" + HeaderStyle + "'>User  District</td>");
        //sw.Write("         <td style='" + HeaderStyle + "'>Version No</td>");
        sw.Write("    </tr>");



        String DataStyle = "border:.5pt solid windowtext;";
        String DateTimeStyle = "mso-number-format:\"mm/dd/yyyy hh:mm AM/PM \";";
        String DateStyle = "mso-number-format:\"mm/dd/yyyy \";";
        String TimeStyle = "mso-number-format:\"hh:mm AM/PM \";";
        String InvalidGeoLocationStype = "";
        String ValidGeoLocationStype = "";

        var i = 0;
        double distance = 0;
        double Enddistance = 0;
        for (i = 0; i < dt.Rows.Count; i++)
        {

            var RowStyle = DataStyle;

            Int32 dHours = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Hours;
            Int32 dMins = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Minutes;

            TimeSpan dayJob = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"])).Subtract(Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"]));

            String villageGeoData = dt.Rows[i]["Village_GeoLocation"].ToString();
            abc1 = null;
            abc2 = null;
            if (villageGeoData != null && villageGeoData != "" && villageGeoData != "[]")
            {
                if (dt.Rows[i]["StarttimeLocation"].ToString() == "" || villageGeoData.Length < 100 || dt.Rows[i]["StarttimeLocation"].ToString() == null || dt.Rows[i]["StarttimeLocation"].ToString() == "Unsupported browser" || dt.Rows[i]["StarttimeLocation"].ToString() == "User denied" || dt.Rows[i]["StarttimeLocation"].ToString() == "Location unavailable" || dt.Rows[i]["StarttimeLocation"].ToString() == "Request timed out" || dt.Rows[i]["StarttimeLocation"].ToString() == "Unknown error" || dt.Rows[i]["StarttimeLocation"].ToString() == "GPS turned off")
                {

                }
                else
                {
                    if (dt.Rows[i]["UserName"].ToString() == "EGE3662")
                    {
                        string fff = "";
                    }
                    GeoUtils.GeoPointChecker geoChecker = new GeoUtils.GeoPointChecker(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData);
                    //if (geoChecker.isValid() == false && dt.Rows[i]["StarttimeLocation"].ToString().Length > 4 && dt.Rows[i]["EndtimeLocation"].ToString().ToString().Length > 4)

                    if (dt.Rows[i]["StarttimeLocation"].ToString().Length > 4 && dt.Rows[i]["EndtimeLocation"].ToString().ToString().Length > 4)
                    {

                        if (geoChecker.isValid() == false)
                        {
                            distance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData), 2);
                            //double ddd = distance - distance1;

                            if (distance >= 4)
                            {
                                RowStyle += InvalidGeoLocationStype;
                                abc1 = InvalidGeoLocationStype;
                            }
                            else
                            {
                                RowStyle += ValidGeoLocationStype;
                                abc1 = ValidGeoLocationStype;
                            }
                        }
                        else
                        {
                            distance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData), 2);

                            RowStyle += ValidGeoLocationStype;
                            abc1 = ValidGeoLocationStype;

                        }
                    }
                    else
                    {

                        RowStyle += DataStyle;
                        abc1 = DataStyle;
                    }

                    GeoUtils.GeoPointChecker geoChecker1 = new GeoUtils.GeoPointChecker(dt.Rows[i]["EndtimeLocation"].ToString(), villageGeoData);
                    if (dt.Rows[i]["StarttimeLocation"].ToString().Length > 4 && dt.Rows[i]["EndtimeLocation"].ToString().ToString().Length > 4)
                    {
                        Enddistance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["EndtimeLocation"].ToString(), villageGeoData), 2);

                        abc2 = ValidGeoLocationStype;
                    }
                    else
                    {


                        abc2 = InvalidGeoLocationStype;
                    }
                }
            }


            sw.Write("<tr>");
            sw.Write("<td style='" + RowStyle + "'>" + (i + 1) + "</td>");
            //retStr += "<td style='" + RowStyle + "'>" + mData.TimeSheet_ID + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["UserName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["FristName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["Role"].ToString() + "</td>");

            sw.Write("<td style='" + RowStyle + DateStyle + "'>" + Convert.ToDateTime(dt.Rows[i]["Date"].ToString()).ToString("dd/MM/yyy") + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["DistrictName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["BlockName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["PanchayatName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["ClusterName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["ClusterCode"].ToString() + "</td>");

            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["VillageCode"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["VillageName"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_StartTime"].ToString() + "</td>");
            //   sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_StartTime"].ToString() + "</td>");
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["StarttimeLocation"].ToString() + "</td>");
            //sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_EndTime"].ToString() + "</td>");
            //sw.Write("<td style='" + RowStyle + "'>" + mData.TimeSheet_StartTimeReasonText + "</td>");
            //sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_EndTime"].ToString() + "</td>");
            if (abc1 == "background-color:red);")
            {
                //sw.Write("<td style='" + RowStyle + "'>" + Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"].ToString()) + "</td>");

            }
            else if (abc1 == ValidGeoLocationStype)
            {
                if (dt.Rows[i]["EndtimeLocation"].ToString() == "GPS turned off")
                {
                    sw.Write("<td style='" + RowStyle + "'>" + "" + "</td>");

                }
                else
                {
                    sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["EndtimeLocation"].ToString() + "</td>");
                }

            }

            else
            {
                sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["EndtimeLocation"].ToString() + "</td>");
            }
            //retStr += "<td style='" + RowStyle + "'>" + mData.TimeSheet_EndLocation + "</td>";
            sw.Write("<td style='" + RowStyle + TimeStyle + "'>" + Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"].ToString()).ToShortTimeString() + "</td>");
            // sw.Write("<td style='" + RowStyle + "'>" + mData.TimeSheet_EndTimeReasonText + "</td>";
            //sw.Write("<td style='" + RowStyle + "'>" + mData.TimeSheet_Mode + "</td>";
            //retStr += "<td style='" + DataStyle + "'>" + dHours.ToString() + ":" + dMins.ToString() + "</td>";
            sw.Write("<td style='" + RowStyle + "mso-number-format:\"[hh]:mm\";'>" + dayJob.Hours + ":" + dayJob.Minutes + "</td>");

            dt.Rows[i]["TotalHours"] = dayJob.Hours + ":" + dayJob.Minutes;
            //if (abc1 == "background-color:red;")
            //if (abc1 == "background-color:#99FF66")
            if (abc1 == "background-color:#99FF66;")
            {
                sw.Write("<td style='" + RowStyle + "'>" + distance + "KM</td>");

            }
            else if (abc1 == "background-color:red;")
            {
                sw.Write("<td style='" + RowStyle + "'>" + distance + "KM</td>");
                //sw.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");

            }
            else if (abc1 == "border:.5pt solid windowtext;")
            {
                //sw.Write("<td style='" + RowStyle + "'>" + distance + "KM</td>");
                sw.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");

            }
            else
            {
                sw.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");
            }

            if (abc2 == "background-color:#99FF66;")
            {
                sw.Write("<td style='" + RowStyle + "'>" + Enddistance + "KM</td>");

            }
            else if (abc2 == "background-color:red;")
            {
                //sw.Write("<td style='" + RowStyle + "'>" + "NA" + "KM</td>");
                sw.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");

            }
            else if (abc2 == "border:.5pt solid windowtext;")
            {
                //sw.Write("<td style='" + RowStyle + "'>" + distance + "KM</td>");
                sw.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");

            }
            else
            {
                sw.Write("<td style='" + DataStyle + "'>" + "NA" + "</td>");
            }
            //    sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["VersionCodeNo"].ToString() + "</td>");
            distance = 0;

            Enddistance = 0;
            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["DistName"].ToString() + "</td>");
            sw.Write("</tr>");

        }

        DataStyle += "background-color:yellow;";

        sw.Write("</table>");

        sw.Close();

        dt.Columns.Remove("Village_GeoLocation");
        ExportToCSVFile(dt, "BOLoginLogoutReport");

        FileStream fs = null;//, fs2=null;
        try
        {
            string path1 = Fullfilename1;
            string foldername = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + path1 + "");
            string datafolder = path1.Substring(0, path1.Length - 4);
            //  string[] file = Directory.GetFiles(foldername);
            string path = foldername;
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
        //flushExcel(totDays.ToString());

        //var lq=dataList.Select(fld=>fld.TimeSheet_EndTime.

    }

}