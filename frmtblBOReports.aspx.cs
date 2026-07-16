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

public partial class frmtblBOReports : System.Web.UI.Page
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
                LoadYear();
                LoadUserLeavel();
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        //ViewState["1"] = 1;
        //ClearGrid();

        //gvD2d.Visible = false;
        LoadReport();
        //GenerateExcel();
    }


    protected void btnImport_Click(object sender, EventArgs e)
    {
        DataTable dt = Session["MobileUser"] as DataTable;
        if (dt != null)
        {
            GenerateExcelNewStringBuld();
        }

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
        string Fullfilename1 = "" + "EmployeeTrackingBOReport" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";
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
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");
        string Fullfilename = "" + "Employeetracking" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


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
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();


        //flushExcel(totDays.ToString());

        //var lq=dataList.Select(fld=>fld.TimeSheet_EndTime.

    }

    public void LoadReport()
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

            if (ddlUser.SelectedIndex > 0)
            {
                conditions += " and Tbl_User_LoginBO.UserId =  '" + ddlUser.SelectedValue + "' ";

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
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTblUserLoginBO]", cmdParameters);

        if (dt.Rows.Count > 0)
        {
            gvD2d.DataSource = dt;
            gvD2d.DataBind();
            Session["MobileUser"] = dt;
            lblTotalCount.Text = (dt.Rows.Count).ToString();
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


        if (ddlState.Length > 0)
        {
            conditions += "   StateCode in( " + ddlState + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and DistrictCode in(" + ddlDistrict + ") ";

        }
        if (ddlBlock.Length > 0)
        {
            conditions += " and BlockCode in(" + ddlBlock + ") ";

        }



        objComman.BindDLL("MstUser", "UserId as UserId,[FristName]+' ('+ UserName +')' as [UserName] ", conditions, "", "", ddlUser, "UserName", "UserId", "Select");
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
        if (Session["D2d"] != null)
        {
            DataTable dt = Session["D2d"] as DataTable;
            gvD2d.DataSource = dt;
            gvD2d.DataBind();
        }

    }
    protected void gvnroll_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GV_DynamicGrid_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }


    #region Abhimanyu

    protected void btnCSV_Click(object sender, EventArgs e)
    {
        if (ViewState["1"].ToString() == "2")
        {
            DataTable dt = (DataTable)Session["D2d"];
            ExporttoCSV(gvD2d, dt);
        }



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
        string Fullfilename = "" + "Employeetracking" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


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
}