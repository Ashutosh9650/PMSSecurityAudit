using ClosedXML.Excel;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;

public partial class FrmMasterImport : System.Web.UI.Page
{
    Comman obj = new Comman();
    clsMain Objcls = new clsMain();
    Comman objComman = new Comman();

    string conditions = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadYear();
            FillCBState();

        }
    }
    public void FillCBState()
    {
        conditions = "";
        objComman.BindDLL("[PMS].[dbo].mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");

        ddlState.SelectedIndex = 2;
        ddlState_SelectedIndexChanged(ddlState, null);

    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlState_SelectedIndexChanged(ddlDistrict, null);
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
    }
    public void FillCBDist()
    {

        conditions = "";


        conditions = "StateCode ='" + ddlState.SelectedValue + "' and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
        {
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        }
        else
        {

            objComman.BindDLL("[PMS].[dbo].mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");
        }


    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        //GenerateExcelData();
    }
    protected void btnCSV_Click(object sender, EventArgs e)
    {
    }
    public Boolean BulkCopyTempDistProfile(DataTable dt)
    {
        try
        {


            SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlHelper.mainConnectionString);
            bulkCopy.BatchSize = 100;
            bulkCopy.BulkCopyTimeout = 5;

            bulkCopy.DestinationTableName = "MasterDataUpload";
            bulkCopy.NotifyAfter = 200;
            bulkCopy.WriteToServer(dt);
            return true;
        }
        catch
        {
            return false;
        }
    }




    public Boolean BulkCopySchoolVillage(DataTable dt)
    {
        try
        {


            SqlBulkCopyColumnMapping mapping01 = new SqlBulkCopyColumnMapping("StateCode", "StateCode");
            SqlBulkCopyColumnMapping mapping02 = new SqlBulkCopyColumnMapping("DistrictCode", "DistrictCode");
            SqlBulkCopyColumnMapping mapping03 = new SqlBulkCopyColumnMapping("EGBlockCode", "BlockCode");
            SqlBulkCopyColumnMapping mapping04 = new SqlBulkCopyColumnMapping("BlockCode", "MainBlockCode");
            SqlBulkCopyColumnMapping mapping05 = new SqlBulkCopyColumnMapping("BlockName", "MainBlockName");
            SqlBulkCopyColumnMapping mapping06 = new SqlBulkCopyColumnMapping("ClusterCode", "ClusterCode");
            SqlBulkCopyColumnMapping mapping08 = new SqlBulkCopyColumnMapping("GP_CODE", "PanchayatCode");
            SqlBulkCopyColumnMapping mapping09 = new SqlBulkCopyColumnMapping("VillageCode", "VillageCode");
            SqlBulkCopyColumnMapping mapping10 = new SqlBulkCopyColumnMapping("VillageName", "VillageName");
            SqlBulkCopyColumnMapping mapping11 = new SqlBulkCopyColumnMapping("AdminDistrictCode", "AdminDistrictCode");
            SqlBulkCopyColumnMapping mapping12 = new SqlBulkCopyColumnMapping("AdminDistrictName", "AdminDistrictName");
            SqlBulkCopyColumnMapping mapping13 = new SqlBulkCopyColumnMapping("VillageCode", "EGVillageCode");
            SqlBulkCopyColumnMapping mapping14 = new SqlBulkCopyColumnMapping("MergeVillageCOde", "MergeVillageCOde");
            SqlBulkCopyColumnMapping mapping15 = new SqlBulkCopyColumnMapping("Admin State Name", "EG State Name");
            SqlBulkCopyColumnMapping mapping16 = new SqlBulkCopyColumnMapping("Admin State Code", "EGState Code");

            SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlHelper.mainConnectionString);
            bulkCopy.BatchSize = 20000;
            bulkCopy.BulkCopyTimeout = 0;
            bulkCopy.ColumnMappings.Add(mapping01);
            bulkCopy.ColumnMappings.Add(mapping02);
            bulkCopy.ColumnMappings.Add(mapping03);
            bulkCopy.ColumnMappings.Add(mapping04);
            bulkCopy.ColumnMappings.Add(mapping05);
            bulkCopy.ColumnMappings.Add(mapping06);
            bulkCopy.ColumnMappings.Add(mapping08);
            bulkCopy.ColumnMappings.Add(mapping09);
            bulkCopy.ColumnMappings.Add(mapping10);
            bulkCopy.ColumnMappings.Add(mapping11);
            bulkCopy.ColumnMappings.Add(mapping12);
            bulkCopy.ColumnMappings.Add(mapping13);
            bulkCopy.ColumnMappings.Add(mapping14);
            bulkCopy.ColumnMappings.Add(mapping15);
            bulkCopy.ColumnMappings.Add(mapping16);
            bulkCopy.DestinationTableName = "T_mstvillage";
            bulkCopy.NotifyAfter = 200;
            bulkCopy.WriteToServer(dt);
            return true;
        }
        catch
        {
            return false;
        }

    }
    public Boolean BulkCopySchoolPanchat(DataTable dt)
    {
        try
        {
            //      
            SqlBulkCopyColumnMapping mapping01 = new SqlBulkCopyColumnMapping("StateCode", "StateCode");
            SqlBulkCopyColumnMapping mapping02 = new SqlBulkCopyColumnMapping("DistrictCode", "DistrictCode");
            SqlBulkCopyColumnMapping mapping03 = new SqlBulkCopyColumnMapping("EGBlockCode", "BlockCode");
            SqlBulkCopyColumnMapping mapping04 = new SqlBulkCopyColumnMapping("GP_CODE", "PanchayatCode");
            SqlBulkCopyColumnMapping mapping05 = new SqlBulkCopyColumnMapping("GramPanchyat", "PanchayatName");
            SqlBulkCopyColumnMapping mapping06 = new SqlBulkCopyColumnMapping("GP_CODE", "EGPanchayatCode");

            SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlHelper.mainConnectionString);
            bulkCopy.BatchSize = 5000;
            bulkCopy.BulkCopyTimeout = 0;
            bulkCopy.ColumnMappings.Add(mapping01);
            bulkCopy.ColumnMappings.Add(mapping02);
            bulkCopy.ColumnMappings.Add(mapping03);
            bulkCopy.ColumnMappings.Add(mapping04);
            bulkCopy.ColumnMappings.Add(mapping05);
            bulkCopy.ColumnMappings.Add(mapping06);

            bulkCopy.DestinationTableName = "T_mstPanchayat";
            bulkCopy.NotifyAfter = 200;
            bulkCopy.WriteToServer(dt);
            return true;
        }
        catch
        {
            return false;
        }

    }
    public Boolean BulkCopySchool(DataTable dt)
    {
        try
        {

            SqlBulkCopyColumnMapping mapping01 = new SqlBulkCopyColumnMapping("VillageCode", "VillageCode");
            SqlBulkCopyColumnMapping mapping02 = new SqlBulkCopyColumnMapping("DISECODE", "SchoolCode");
            SqlBulkCopyColumnMapping mapping03 = new SqlBulkCopyColumnMapping("DISECODE", "DISECode");
            SqlBulkCopyColumnMapping mapping04 = new SqlBulkCopyColumnMapping("DISECODE", "SchoolCodeID");
            SqlBulkCopyColumnMapping mapping05 = new SqlBulkCopyColumnMapping("SchoolName", "Name");
            SqlBulkCopyColumnMapping mapping06 = new SqlBulkCopyColumnMapping("SchoolType", "SchoolLevel");
            SqlBulkCopyColumnMapping mapping07 = new SqlBulkCopyColumnMapping("SchoolType", "SchoolLevel1");
            SqlBulkCopyColumnMapping mapping08 = new SqlBulkCopyColumnMapping("SchoolType", "SchoolLevel2");
            SqlBulkCopyColumnMapping mapping09 = new SqlBulkCopyColumnMapping("GOVTDISECODE", "Govt_DiseCode");
            SqlBulkCopyColumnMapping mapping10 = new SqlBulkCopyColumnMapping("Management", "ManagementType");
            SqlBulkCopyColumnMapping mapping11 = new SqlBulkCopyColumnMapping("Operational", "WorkingStatus");
            SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlHelper.mainConnectionString);
            bulkCopy.BatchSize = 10000;
            bulkCopy.BulkCopyTimeout = 0;
            bulkCopy.ColumnMappings.Add(mapping01);
            bulkCopy.ColumnMappings.Add(mapping02);
            bulkCopy.ColumnMappings.Add(mapping03);
            bulkCopy.ColumnMappings.Add(mapping04);
            bulkCopy.ColumnMappings.Add(mapping05);
            bulkCopy.ColumnMappings.Add(mapping06);
            bulkCopy.ColumnMappings.Add(mapping07);
            bulkCopy.ColumnMappings.Add(mapping08);
            bulkCopy.ColumnMappings.Add(mapping09);
            bulkCopy.ColumnMappings.Add(mapping10);
            bulkCopy.ColumnMappings.Add(mapping11);

            bulkCopy.DestinationTableName = "T_mstSchool";
            bulkCopy.NotifyAfter = 200;
            bulkCopy.WriteToServer(dt);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public DataTable LoadMaster()
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@gg",""),

        };
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptLoadMasterData]", cmdParameters);
        return dt;
    }
    public DataSet SP_Check_District_Excel_Import()
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[SP_Check_District_Excel_Import]";
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet SP_Check_District_Excel_Import_IN_Maintable()
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandTimeout = 0;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "SP_Check_District_Excel_Import_MainTable";
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet rptUinqueGenerate()
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandTimeout = 0;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "rptUinqueGenerate";
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlYear.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Year')</script>", false);

                return;

            }
            DataSet RowAffected2 = new DataSet();
            RowAffected2 = obj.rptUpdateUniqueCode();

            DataSet RowAffected3 = new DataSet();
            int icount = rptUinqueGenerateSave();


            DataSet RowAffected1 = new DataSet();
            RowAffected1 = SP_Check_District_Excel_Import_IN_Maintable();


            if (RowAffected1 != null)
            {
                lbl_messages.Text = "Data Import Success..";
                ModalAlert.Show();
            }
        }
        catch (Exception ex)
        {
            lbl_messages.Text = ex.ToString();
            ModalAlert.Show();

        }
        finally
        {

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

        DataTable dt = null;
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
                if (m > 3)
                {
                    dr = dtYear.NewRow();
                    dr["Type"] = GivenYear.ToString() + "-" + Convert.ToString((GivenYear + 1));
                    dr["ID"] = y;
                    dtYear.Rows.Add(dr);
                    dr = dtYear.NewRow();
                    dr["Type"] = GivenYear - 1 + "-" + Convert.ToString((GivenYear - 1 + 1));
                    dr["ID"] = y - 1;
                    dtYear.Rows.Add(dr);
                    //get last  two digits (eg: 10 from 2010);

                }
                else
                {

                    Int32 m7 = y + 1;
                    dr = dtYear.NewRow();
                    dr["Type"] = Convert.ToString((y)) + "-" + m7.ToString();
                    //y = y - 1;
                    dr["ID"] = y;
                    dtYear.Rows.Add(dr);
                    dr = dtYear.NewRow();
                    dr["Type"] = Convert.ToString((y - 1)) + "-" + y.ToString();
                    //y = y - 1;
                    dr["ID"] = y - 1;

                    dtYear.Rows.Add(dr);


                }

            }

        }
        //DataTable dtYear = objComman.Generate_Financial_Year();

        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;


    }
    public int rptUinqueGenerateSave()
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
            {

            new SqlParameter("@Fyear", "2026-2027"),

        };
        int icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptUinqueGenerate", cmdParameters);

        return icount;
    }

    public void MultipuExeclProcess(DataTable table)
    {

        string StartupPath = Server.MapPath(Comman.GetImagePath("MouSinglePath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\ErrorFile.xlsx");
        var ws = wb.Worksheet(1);



        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("RowNo");
        ws.Cell(3, 1).InsertData(table.Rows);
        Int32 ii = Convert.ToInt32(table.Rows.Count) + 1;
        string str = "A3:Q" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);






        filepath = StartupPath + "\\ErrorFile " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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

    private void ExporttoExcel(DataTable table)
    {


        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        string Fullfilename = "" + "ErrorReport" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";

        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + " ");

        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'><TR> <TD colspan='13' style='font-size:13.0pt; text-align:center; color:blue; font-family:Calibri;' ><B>" + ViewState["FileName"] + "</B><TD></TR> <TR>");
        //am getting my grid's column headers
        int columnscount = table.Columns.Count;


        foreach (DataColumn dc in table.Columns)
        {      //write in new column
            HttpContext.Current.Response.Write("<Td>");
            //Get column headers  and make it as bold in excel columns
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(dc.ColumnName);
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



    private void ExporttoExcelDist(DataTable table)
    {


        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        string Fullfilename = "" + "DistProfile" + ".xls";

        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + " ");

        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
           "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
           "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");        //am getting my grid's column headers
        int columnscount = table.Columns.Count;


        foreach (DataColumn dc in table.Columns)
        {      //write in new column
            HttpContext.Current.Response.Write("<Td>");
            //Get column headers  and make it as bold in excel columns
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(dc.ColumnName);
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


    protected void LnkExport_Click(object sender, EventArgs e)
    {
        string filePath = Server.MapPath(Comman.GetImagePath("ExportPath") + "/GovtTarget_Formate.xlsx");
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();

    }
    protected void btnNewImport_Click(object sender, EventArgs e)
    {
        conditions = "";
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "  v.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlState.SelectedIndex > 0)
        {
            conditions += " and  v.StateCode = '" + ddlState.SelectedValue + "' ";

        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " and v.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";

        }
        DataTable dt = Objcls.LoadMasterImport(conditions);

        MultipuExeclTrack(dt);
    }

    protected void btnNewImport1_Click(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select District')</script>", false);

        }

        int icount = SaveDataInsertUpdate();
        if (icount > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Delete Successfully')</script>", false);

        }
    }
    public int SaveDataInsertUpdate()
    {
        int Icount = 0;
        try
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@Dist", ddlDistrict.SelectedValue),


            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteMainmaster", cmdParameters);
        }
        catch
        {

        }
        return Icount;
    }

    public void MultipuExeclTrack(DataTable dt)
    {

        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\DistUpload.xlsx");
        var ws = wb.Worksheet(1);

        //var ws1 = wb.Worksheet(2);
        //var ws3 = wb.Worksheet(3);

        //dt.Columns.Remove("rownNO");
        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("rownNO");

        ws.Cell(2, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 1;
        string str = "A2:Y" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);






        filepath = StartupPath + "\\DistUpload" + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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

    public string INSERT_ImportDataSingle(DataTable dt, string strSP_Name, string strParentTable_Name, string Flag)
    {
        string getresult = "";
        string R_Import = string.Empty;
        string strtemptblmstGroupChk = "IF OBJECT_ID('tempdb.#temp_" + strParentTable_Name + "') IS NOT NULL DROP TABLE #temp_" + strParentTable_Name + "";
        string strtemptblmstGroup = string.Empty;
        SqlConnection ConStr = new SqlConnection();
        ConStr = new SqlConnection(SqlHelper.mainConnectionString);
        if (strParentTable_Name == "T_mstSchool")
        {
            strtemptblmstGroup = "";
            strtemptblmstGroup += " SELECT WorkingStatus,ManagementType,[VillageCode],[SchoolCode],[SchoolCodeID],[DISECode],[DISECode1],[DISECode2],[Name],[Name1],[Name2],[SchoolLevel],[SchoolLevel1],[SchoolLevel2],[SchoolCodeTemp],OldSchoolUniqueCode,OldVillageUniqueCode ";


            strtemptblmstGroup += " INTO #temp_" + strParentTable_Name + " FROM " + strParentTable_Name + " ";
            strtemptblmstGroup += " where DISECode is null ";
            // ConStr = new SqlConnection("Data Source=EducateGirls.db.3975866.hostedresource.com;Initial Catalog=EducateGirls;User Id=educategirls;Password=mw2Master1EG0!");

        }

        if (strParentTable_Name == "T_mstVillage")
        {
            strtemptblmstGroup = "";
            strtemptblmstGroup += " SELECT  [StateCode],[DistrictCode] ,[BlockCode] ,[MainBlockCode],[MainBlockName],[ClusterCode],[GP_CODE],[VillageCode],[VillageName],OldUniqueCode ";


            strtemptblmstGroup += " INTO #temp_" + strParentTable_Name + " FROM " + strParentTable_Name + " ";
            strtemptblmstGroup += " where VillageCode is null ";
            // ConStr = new SqlConnection("Data Source=EducateGirls.db.3975866.hostedresource.com;Initial Catalog=EducateGirls;User Id=educategirls;Password=mw2Master1EG0!");

        }


        getresult = objComman.INSERT_ImportDataSingleSP(dt, strSP_Name, strParentTable_Name, strtemptblmstGroupChk, strtemptblmstGroup, Flag, ConStr);
        return getresult;
    }
}