using ClosedXML.Excel;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class SurveyReport : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {




                if (!IsPostBack)
                {

                    FillDropdown();

                }
                // btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");LinkButton8
            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }

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
    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        int FormLevl = Int32.Parse(ddlLevel.SelectedValue.ToString());

        FillFormNameNew(FormLevl);

    }
    public void FillFormNameNew(int FormLevel)
    {
        string conditions = "";
        string conditions4 = "";
        string dist = "";
        if (Session["user_level_Role"].ToString() == "1")
        {


        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions4 = "  UserName='" + Session["username"].ToString() + "' ";

            string strQry1 = "       sELECT distinct mst2District.DistrictCode as DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist  ";
            strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions4 + "  and  Fyear='" + Session["FinYear"].ToString() + "' order by DistrictName  ";
            DataTable dtDistrict = objMain.LoadData(strQry1);

            if (dtDistrict.Rows.Count > 0)
            {
                foreach (DataRow dr in dtDistrict.Rows)
                {
                    dist += "'" + dr["DistrictCode"] + "'" + ",";
                }
            }

            if (dist.Length > 0)
            {
                dist = dist.Substring(0, dist.LastIndexOf(","));
            }
            conditions = " and mst2District.DistrictCode in(" + dist.ToString() + ")  ";
        }
        else
        {
            conditions = " and mst2District.DistrictCode in(" + Session["DistrictCode"].ToString() + ")  ";


        }
        string UserID = Session["UserID"].ToString();
        DataTable dt = new DataTable();
        //int FormLevel;
        if (FormLevel == 0 || FormLevel == -1)
        {
            //  dt = objBLL.Select_All_Data("MSTForm", "FormID,FormName", "IsDeleted = 0", "", "");
        }
        else
        {
            dt = Get_DataFor3Filter("USP_GetSurveyChange2023", conditions, FormLevel.ToString(), "");
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
    protected void Lnkpf_OnClick(object sender, EventArgs e)
    {
        string con = "";

        string conditions = "";
        string conditions4 = "";
        string dist = "";
        if (Session["user_level_Role"].ToString() == "1")
        {


        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions4 = "  UserName='" + Session["username"].ToString() + "' ";

            string strQry1 = "       sELECT distinct mst2District.DistrictCode as DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist  ";
            strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions4 + "  and  Fyear='" + Session["FinYear"].ToString() + "' order by DistrictName  ";
            DataTable dtDistrict = objMain.LoadData(strQry1);

            if (dtDistrict.Rows.Count > 0)
            {
                foreach (DataRow dr in dtDistrict.Rows)
                {
                    dist += "'" + dr["DistrictCode"] + "'" + ",";
                }
            }

            if (dist.Length > 0)
            {
                dist = dist.Substring(0, dist.LastIndexOf(","));
            }
            con += " and mst2District.DistrictCode in(" + dist.ToString() + ")  ";
        }
        else
        {
            con += " and mst2District.DistrictCode in(" + Session["DistrictCode"].ToString() + ")  ";


        }

        //if (ddlLevel.SelectedIndex>0)
        //{
        //    con += " and AssessmentFor =" + ddlLevel.SelectedValue + "";
        //}
        //if (ddlForm.SelectedIndex > 0)
        //{
        //    con = con+ " and FormEvaluation.FormID =" + ddlForm.SelectedValue + "";
        //}
        DataTable dtHeader = Get_DataFor("rptSurveySummary2023", con);
        if (dtHeader.Rows.Count > 0)
        {
            GVChildTarget.DataSource = dtHeader;
            GVChildTarget.DataBind();
            Session["Summary"] = dtHeader;
        }

    }
    public DataTable Get_DataFor(string ProcedureName, string Filter1)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                            new SqlParameter("@Con",Filter1),


                    };
            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, ProcedureName, paramvT);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch (Exception)
        {

        }
        return dtcombo;
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        DataTable dt = Session["Summary"] as DataTable;

        ExporttoExcel(GVChildTarget, dt, "Summary");

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


    protected void LnkDetails_OnClick(object sender, EventArgs e)
    {
        if (ddlForm.SelectedIndex > 0)
        {
            DataTable dtHeader = Get_DataFor2FilterReport("rptSurvey", ddlForm.SelectedValue.ToString(), "1");
            GVChildTarget.DataSource = null;
            GVChildTarget.DataBind();
            Session["dtHeader1"] = dtHeader;
            ExportReportDetails();
            // exportTABLE_COMPLETE(dtHeader);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Showalert", "alert('Please select Survey');", true);
        }

    }
    protected void LnkDetails_OnClick1(object sender, EventArgs e)
    {
        if (ddlForm.SelectedIndex > 0)
        {
            DataTable dtHeader = Get_DataFor2FilterReport("rptSurverEMpScoreNew", ddlForm.SelectedValue.ToString(), "1");
            Session["dtHeader"] = dtHeader;
            GVChildTarget.DataSource = null;
            GVChildTarget.DataBind();
            ExportReport();
            // exportTABLE_COMPLETESchor(dtHeader);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Showalert", "alert('Please select Survey');", true);
        }

    }
    public void ExportReportDetails()
    {

        DataTable dtMain = Session["dtHeader1"] as DataTable;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\Assessment.xlsx");
        var ws = wb.Worksheet(1);

        for (int x = 0; x < dtMain.Columns.Count; x++)
        {

            ws.Cell(1, x + 1).Value = dtMain.Columns[x].ColumnName;
        }


        //dt1.Columns.Remove("rownNO");
        ws.Cell(2, 1).InsertData(dtMain.Rows);
        Int32 ii = Convert.ToInt32(dtMain.Rows.Count) + 2;
        string str = "A1:AT" + ii;

        //ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        //ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        //ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        //ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        filepath = StartupPath + "\\AssessmentDetails" + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
    public void ExportReport()
    {

        DataTable dtMain = Session["dtHeader"] as DataTable;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\Assessment.xlsx");
        var ws = wb.Worksheet(1);

        dtMain.Columns.Add("Total_Question");
        dtMain.Columns.Add("Total_Answer");

        DataTable dtSurveyQTotal = Get_DataFor2FilterReport("rptSurveyQTotal", ddlForm.SelectedValue.ToString(), "1");
        int FScore = 0;
        if (dtSurveyQTotal.Rows.Count > 0)
        {
            FScore = Convert.ToInt32(dtSurveyQTotal.Rows[0]["Score"]);
        }
        int dd = 0;
        for (int j = 0; j < dtMain.Rows.Count; j++)
        {
            for (int i = 7; i < dtMain.Columns.Count - 1; i++)
            {
                if (dtMain.Columns[i].ColumnName == "Total_Question")
                {

                }
                else
                {
                    dd = dd + Convert.ToInt32(dtMain.Rows[j][i].ToString());
                    //if (dtMain.Columns[i].ColumnName == "Total_Answer")
                    //{
                    //    dtMain.Rows[j].SetField("Total_Answer", Convert.ToString(dd));
                    //    dd = 0;
                    //}
                }

            }
            dtMain.Rows[j]["Total_Answer"] = dd.ToString();
            dd = 0;
            dtMain.AcceptChanges();
        }
        for (int hh = 0; hh < dtMain.Rows.Count; hh++)
        {
            dtMain.Rows[hh]["Total_Question"] = FScore.ToString();
        }
        for (int x = 0; x < dtMain.Columns.Count; x++)
        {

            ws.Cell(1, x + 1).Value = dtMain.Columns[x].ColumnName;
        }


        //dt1.Columns.Remove("rownNO");
        ws.Cell(2, 1).InsertData(dtMain.Rows);
        Int32 ii = Convert.ToInt32(dtMain.Rows.Count) + 2;
        string str = "A1:AT" + ii;

        //ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        //ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        //ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        //ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        filepath = StartupPath + "\\ResponseRawDetail " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
    private void exportTABLE_COMPLETESchor(DataTable dt)
    {
        DataTable dtExp_Data = new DataTable();

        dtExp_Data = dt;
        String name = "Emloyee wise Report form " + ddlForm.SelectedItem.ToString() + "_" + DateTime.Now.ToString() + ".xls";
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

        //sbb.Append("<tr style='backcolor=red'>");
        //for (int k = 0; k < dtHeader.Rows.Count; k++)
        //{
        //    sbb.Append("<td align=center style=\"font-weight:bold;BACKGROUND-COLOR: #dcdcdc;FONT-SIZE: 11pt;\"><b>");
        //    sbb.Append(dtHeader.Rows[k][0]);
        //    sbb.Append("</b></td>");
        //}
        //sbb.Append("</tr>");
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

                sbb.Append("<td align=Left style='FONT-SIZE: 10pt'>" + firstString + "</td>");
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
        catch
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

        //sbb.Append("<tr style='backcolor=red'>");
        //for (int k = 0; k < dtHeader.Rows.Count; k++)
        //{
        //    sbb.Append("<td align=center style=\"font-weight:bold;BACKGROUND-COLOR: #dcdcdc;FONT-SIZE: 11pt;\"><b>");
        //    sbb.Append(dtHeader.Rows[k][0]);
        //    sbb.Append("</b></td>");
        //}
        //sbb.Append("</tr>");
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
}