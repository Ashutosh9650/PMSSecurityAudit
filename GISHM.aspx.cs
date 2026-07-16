using ClosedXML.Excel;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
public partial class GISHM : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    string conditions = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {




            if (Convert.ToString(Session["username"]) != "")
            {
                string userlevelrole = Convert.ToString(Session["user_level_Role"]);
                if (userlevelrole == "1")
                {
                    ddlYear.Enabled = true;
                    ddlState.Enabled = true;
                    ddlDistrict.Enabled = true;
                    ddlBlock.Enabled = true;
                }
                else if (userlevelrole == "4")
                {
                    ddlYear.Enabled = false;
                    ddlState.Enabled = false;
                    ddlDistrict.Enabled = false;
                    ddlBlock.Enabled = false;
                }
                else
                {
                    ddlYear.Enabled = false;
                    ddlState.Enabled = false;
                    ddlDistrict.Enabled = true;
                    ddlBlock.Enabled = true;
                }


            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }

        }
    }


    [WebMethod(EnableSession = true)]
    public static string Get_MapDetails(string ValidID, string ValidID1, string ValidID2, string ValidID3, string ValidID4, string ValidID5, string ValidID6, string ValidID7)
    {

        //string strFlag = "";
        //string s = "";
        //if (ValidID.Length > 6)
        //{
        //    s = ValidID;
        //    string[] subs = s.Split('#');
        //    strFlag = subs[0];
        //}
        //else
        //{
        //    strFlag = ValidID;
        //}
        //string LanguageID = Convert.ToString(HttpContext.Current.Session["SessLangID"]);
        //if (LanguageID != "")
        //{ }
        //else { LanguageID = "1"; }
        clsMain objMain1 = new clsMain();

        if (ValidID6 == "1")
        {
            objMain1.ReportDownload("Quality Enrolment", "Heat Map", Convert.ToString(HttpContext.Current.Session["username"]));
        }
        else if (ValidID6 == "2")
        {
            objMain1.ReportDownload("FC Run Rate", "Heat Map", Convert.ToString(HttpContext.Current.Session["username"]));
        }
        else if (ValidID6 == "3")
        {
            objMain1.ReportDownload("Quality of D2D Contact", "Heat Map", Convert.ToString(HttpContext.Current.Session["username"]));
        }
        else if (ValidID6 == "4")
        {
            objMain1.ReportDownload("Enrolment CV Error Rate", "Heat Map", Convert.ToString(HttpContext.Current.Session["username"]));
        }
        else if (ValidID6 == "5")
        {
            objMain1.ReportDownload("D2D Contact", "Heat Map", Convert.ToString(HttpContext.Current.Session["username"]));
        }
        else if (ValidID6 == "6")
        {
            objMain1.ReportDownload("RTE", "Heat Map", Convert.ToString(HttpContext.Current.Session["username"]));
        }
        else if (ValidID6 == "8")
        {
            objMain1.ReportDownload("RTE with Document", "Heat Map", Convert.ToString(HttpContext.Current.Session["username"]));
        }
        else if (ValidID6 == "9")
        {
            objMain1.ReportDownload("RTE without Document", "Heat Map", Convert.ToString(HttpContext.Current.Session["username"]));
        }
        else if (ValidID6 == "7")
        {
            objMain1.ReportDownload("GKP Average Attendance per Session", "Heat Map", Convert.ToString(HttpContext.Current.Session["username"]));
        }
        else
        {

        }


        SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@FYear",ValidID),
              new SqlParameter("@StateID",ValidID1),
               new SqlParameter("@DistrictID",ValidID2),
                new SqlParameter("@BlockID",ValidID3),
                 new SqlParameter("@ClusterID",ValidID4),
                  new SqlParameter("@Groupby",ValidID5),
                   new SqlParameter("@Report",ValidID6),
                     new SqlParameter("@YearID",ValidID7)

        };

        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_HeatMap2024", p);
        //int count = dt.Rows.Count;
        DataTable dtc = dt.Copy();
        if (dtc.Columns.Contains("DistrictCode"))
        { dtc.Columns.Remove("DistrictCode"); }
        if (dtc.Columns.Contains("BlockCode"))
        { dtc.Columns.Remove("BlockCode"); }
        if (dtc.Columns.Contains("Villagecode"))
        { dtc.Columns.Remove("Villagecode"); }
        if (dtc.Columns.Contains("ClusterCode"))
        { dtc.Columns.Remove("ClusterCode"); }
        if (dtc.Columns.Contains("SchoolCode"))
        { dtc.Columns.Remove("SchoolCode"); }
        if (dtc.Columns.Contains("latlong"))
        { dtc.Columns.Remove("latlong"); }

        HttpContext.Current.Session["tblLocDetails1"] = dtc;
        StringBuilder sb = new StringBuilder();
        // sb.Append("");
        sb.Append("<div class='col-lg-12 col-md-12 col-sm-12 col-xs-12'>");
        sb.Append("<table class='table table-striped table-bordered filtered-table' id='tblLocDetails'>");
        sb.Append("<thead><tr>");
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            if (dt.Columns[i].ColumnName.ToLower() == "blockcode")
            { }
            else if (dt.Columns[i].ColumnName.ToLower() == "clustercode")
            { }
            else if (dt.Columns[i].ColumnName.ToLower() == "schoolcode")
            { }
            else if (dt.Columns[i].ColumnName.ToLower() == "districtcode")
            { }
            else if (dt.Columns[i].ColumnName.ToLower() == "villagecode")
            { }
            else
            {
                sb.Append("<th class='common-header'>" + dt.Columns[i].ColumnName + "</th>");
            }
        }
        sb.Append("</tr></thead><tbody>");
        for (int r = 0; r < dt.Rows.Count; r++)
        {
            string loc = "";
            sb.Append("<tr>");
            for (int c = 0; c < dt.Columns.Count; c++)
            {
                if (c == 0)
                {
                    //sb.Append("<td style=' style='word-wrap: break-word'>" + dt.Rows[r][c] + "</td>");
                    loc = Convert.ToString(dt.Rows[r][c]);
                }
                else
                {
                    //if (dt.Columns[c].ColumnName.ToLower() == "blockcode")

                    //{
                    //    loc = Convert.ToString(dt.Rows[r][c]);

                    //}
                    if (dt.Columns[c].ColumnName.ToLower() == "block")
                    {
                        //string lin = "onclick=Go_to_Location('" + loc + "','')";
                        string lin = "onclick=showloader();getmap('blockclick','" + loc + "');ZoomToLatLong();hideloader();";
                        sb.Append("<td class='common-cell'> <a href='javascript:void(0);' " + lin + ">" + Convert.ToString(dt.Rows[r][c]).Trim() + "</a></td>");
                    }
                    else if (dt.Columns[c].ColumnName.ToLower() == "cluster")
                    {
                        string lin = "onclick=showloader();getmap('clusterclick','" + loc + "');ZoomToLatLong();hideloader();";
                        sb.Append("<td class='common-cell'> <a href='javascript:void(0);' " + lin + ">" + Convert.ToString(dt.Rows[r][c]).Trim() + "</a></td>");
                    }
                    else if (dt.Columns[c].ColumnName.ToLower() == "school")
                    {
                        string lin = "onclick=Go_to_Location('" + loc + "','')";
                        sb.Append("<td class='common-cell'> <a href='javascript:void(0);' " + lin + ">" + Convert.ToString(dt.Rows[r][c]).Trim() + "</a></td>");
                    }
                    else if (dt.Columns[c].ColumnName.ToLower() == "district")
                    {
                        string lin = "onclick=Go_to_Location('" + loc + "','')";
                        sb.Append("<td class='common-cell'> <a href='javascript:void(0);' " + lin + ">" + Convert.ToString(dt.Rows[r][c]).Trim() + "</a></td>");
                    }
                    else if (dt.Columns[c].ColumnName.ToLower() == "village")
                    {
                        string lin = "onclick=showloader();getmap('villageclick','" + loc + "');ZoomToLatLong();hideloader();";
                        sb.Append("<td class='common-cell'> <a href='javascript:void(0);' " + lin + ">" + Convert.ToString(dt.Rows[r][c]).Trim() + "</a></td>");
                    }
                    else
                    {
                        sb.Append("<td class='common-cell'>" + dt.Rows[r][c] + "</td>");
                    }

                }
            }
            sb.Append("</tr>");
        }
        sb.Append("</tbody></table>");
        sb.Append("</div>");
        string str = sb.ToString();
        return sb.ToString();


    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Extract_All_To_Export();
    }
    public void Extract_All_To_Export()
    {
        //XLWorkbook wb = new XLWorkbook();
        //DataTable dtClw = new DataTable();
        //string AppName = "HeatMap_Report";//ddl_Rerservior.SelectedItem.Text;// TypeName = ddl_DataType.SelectedItem.Text;
        //string filename = AppName + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss");

        DataTable dt = new DataTable();
        dt = (HttpContext.Current.Session["tblLocDetails1"] as DataTable);
        ExeclHeatMap(dt);
        //var ws = wb.Worksheets.Add(dt, "HeatMap_Report");
        //  ws.Tables.FirstOrDefault().ShowAutoFilter = false;
        //var NewRows = ws.Row(1);
        //NewRows.InsertRowsAbove(1);

        //ws.Range("A1:F1").Merge();
        //ws.Range("A1:F1").Value = "HeatMap Report";
        //ws.Range("A1:F1").Style.Font.SetFontSize(12);
        //ws.Range("A1:F1").Style.Font.SetBold();
        //ws.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        //Export_TO_Excel(wb, filename);

    }
    public void ExeclHeatMap(DataTable dtMain1)
    {

        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        if (Convert.ToInt32(ddlReportType.SelectedValue) == 1)
        {
            wb = new XLWorkbook(StartupPath + "\\HeatMap.xlsx");
        }
        if (Convert.ToInt32(ddlReportType.SelectedValue) == 2)
        {
            wb = new XLWorkbook(StartupPath + "\\HeatMapFC.xlsx");
        }
        if (Convert.ToInt32(ddlReportType.SelectedValue) == 3)
        {
            wb = new XLWorkbook(StartupPath + "\\HeatMapD2d.xlsx");
        }
        if (Convert.ToInt32(ddlReportType.SelectedValue) == 4)
        {
            wb = new XLWorkbook(StartupPath + "\\HeatMapEnroll.xlsx");
        }
        if (Convert.ToInt32(ddlReportType.SelectedValue) == 5)
        {
            wb = new XLWorkbook(StartupPath + "\\HeatMapD2dContact.xlsx");
        }
        if (Convert.ToInt32(ddlReportType.SelectedValue) == 6)
        {
            wb = new XLWorkbook(StartupPath + "\\HeatMapRT.xlsx");
        }
        if (Convert.ToInt32(ddlReportType.SelectedValue) == 8)
        {
            wb = new XLWorkbook(StartupPath + "\\HeatMapRTWithDoc.xlsx");
        }
        if (Convert.ToInt32(ddlReportType.SelectedValue) == 9)
        {
            wb = new XLWorkbook(StartupPath + "\\HeatMapRTWithLow.xlsx");
        }
        if (Convert.ToInt32(ddlReportType.SelectedValue) == 7)
        {
            wb = new XLWorkbook(StartupPath + "\\HeatMapGKP.xlsx");
        }
        var ws = wb.Worksheet(1);
        DataTable dt = dtMain1;

        //HeatMapFC
        //HeatMapD2d
        //HeatMapEnroll
        //HeatMapD2dContact
        //HeatMapRT
        //HeatMapRTWithDoc
        //HeatMapRTWithLow
        //HeatMapGKP
        for (int x = 0; x < dt.Columns.Count; x++)
        {

            ws.Cell(2, x + 1).Value = dt.Columns[x].ColumnName;
        }
        ws.Cell(3, 1).InsertData(dt.Rows);
        Int32 ii54 = Convert.ToInt32(dt.Rows.Count) + 3;
        string str55 = "";
        if (Convert.ToInt32(ddlReportType.SelectedValue) == 1)
        {
            str55 = "A2:F" + ii54;
        }
        else
        //if (Convert.ToInt32(ddlReportType.SelectedValue) == 2 || Convert.ToInt32(ddlReportType.SelectedValue) == 3 || Convert.ToInt32(ddlReportType.SelectedValue) == 4 || Convert.ToInt32(ddlReportType.SelectedValue) == 5 )
        {
            str55 = "A2:E" + ii54;
        }
        ws.Range(str55).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str55).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str55).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str55).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        filepath = StartupPath + "\\" + ddlReportType.SelectedItem.Text + "" + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
    protected void Export_TO_Excel(XLWorkbook wb, string filename)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
        catch
        {
            throw;
        }

    }

    [WebMethod]
    public static string GetGeoJson(string url)
    {
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
        using (var client = new WebClient())
        {
            client.Encoding = System.Text.Encoding.UTF8;
            return client.DownloadString(url);
        }
    }
}