using ClosedXML.Excel;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class frmChangeCluster2025 : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();

    string conditions = string.Empty, Flag = string.Empty;
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                LoadYear();
                LoadUserLeavel();
                GVCluster.DataSource = null;
                GVCluster.DataBind();
                ValdateUserLavel();
                LoadClass();
                LoadClass2025();
                //mstSchoolClass
                /// mstSchoolLevel
                ddlYear.Enabled = false;

            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }

            ImageButton9.Attributes.Add("onclick", "javascript:return " + "confirm('Do you really want to create “Village Name” as cluster? ')");

        }

    }
    protected void btnNewImport1_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlYear.SelectedValue) != 2026)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Fyear 2026 ')</script>", false);
            return;
        }
        if (FileUpload1.FileName.Length > 0)
        {

        }
        else
        {
            return;
        }

        // need to pass relative path after deploying on server
        string path = System.IO.Path.GetFullPath(Server.MapPath(FileUpload1.FileName));
        /* connection string  to work with excel file. HDR=Yes - indicates 
           that the first row contains columnnames, not data. HDR=No - indicates 
           the opposite. "IMEX=1;" tells the driver to always read "intermixed" 
           (numbers, dates, strings etc) data columns as text. 
        Note that this option might affect excel sheet write access negative. */
        string sDirectory = Server.MapPath(Comman.GetImagePath("MouPath"));

        string FilePath = sDirectory + FileUpload1.FileName;
        FileUpload1.PostedFile.SaveAs(FilePath);
        ViewState["FileName"] = FileUpload1.FileName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");


        // Required for .NET Framework

        DataSet ds;

        using (var stream = File.Open(FilePath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true   // 👈 First row as header for ALL sheets
                    }
                });
            }
        }
        int Flag = 0;
        foreach (DataTable dt11 in ds.Tables)
        {
            string sheetName = dt11.TableName;
            if (sheetName == "School Update")
            {
                Flag = Flag + 1;
            }
            else if (sheetName == "Village Update")
            {
                Flag = Flag + 1;
            }
            else if (sheetName == "Code Book")
            {
                Flag = Flag + 1;
            }

            else if (sheetName == "Define Cluster")
            {
                Flag = Flag + 1;
            }
            else if (sheetName == "Assign Cluster to Village")
            {
                Flag = Flag + 1;
            }
            else if (sheetName == "Validation")
            {
                Flag = Flag + 1;
            }
            else
            {
                Flag = 0;
                break;



            }


        }
        if (Flag != 6)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Valid Template ')</script>", false);
            return;
        }
        clsMain Objcls = new clsMain();

        DataTable dt = ds.Tables[0];
        DataTable dt1 = ds.Tables[1];
        DataTable dt3 = ds.Tables[3];
        DataTable dt2 = ds.Tables[4];

        dt.Columns.Add("CreateBy", System.Type.GetType("System.String"));
        dt1.Columns.Add("CreateBy", System.Type.GetType("System.String"));
        dt2.Columns.Add("CreateBy", System.Type.GetType("System.String"));
        dt3.Columns.Add("CreateBy", System.Type.GetType("System.String"));
        foreach (DataRow dr in dt.Rows)
        {
            dr["CreateBy"] = Session["username"].ToString();
        }
        foreach (DataRow dr in dt1.Rows)
        {

            dr["CreateBy"] = Session["username"].ToString();
        }
        foreach (DataRow dr in dt2.Rows)
        {

            dr["CreateBy"] = Session["username"].ToString();
        }
        foreach (DataRow dr in dt3.Rows)
        {

            dr["CreateBy"] = Session["username"].ToString();
        }
        BulkCopySchool(dt);

        BulkCopyVillage(dt1);

        BulkCopyClusterVillage(dt2);
        BulkCopyCluster(dt3);
        SqlParameter[] cmdParameters = new SqlParameter[]
       {
            new SqlParameter("@CreateBy", Session["username"].ToString()),
             new SqlParameter("@YearID", ddlYear.SelectedItem.Text),

       };
        DataSet dtcheck = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptMatersheetErro]", cmdParameters);
        if (dtcheck.Tables[0].Rows.Count > 0 || dtcheck.Tables[1].Rows.Count > 0 || dtcheck.Tables[2].Rows.Count > 0 || dtcheck.Tables[3].Rows.Count > 0)
        {
            MultipuExeclTrackError(dtcheck);
        }
        else
        {
            string MDG = "School and Village Data Saved sucessfully";
            SqlParameter[] cmdParameters1 = new SqlParameter[]
     {
                    new SqlParameter("@CreateBy", Session["username"].ToString()),
                     new SqlParameter("@YearID", ddlYear.SelectedItem.Text),
                      new SqlParameter("@Dist", ddlDistrict.SelectedValue),
     };
            DataSet dtcheck1 = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptUploadData]", cmdParameters1);
            if (dtcheck1.Tables[0].Rows.Count > 0)
            {
                MDG = MDG + "  Cluster Add in GIS moduel";
            }

            if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
            {



                int icount = 0;
                SqlParameter[] cmdParameters15 = new SqlParameter[]
               {
            new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
            new SqlParameter("@approveStataus", "0"),
            new SqlParameter("@Remark", ""),
             new SqlParameter("@UserName", Convert.ToString(Session["username"])),
               new SqlParameter("@Flag", "1"),



               };
                icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatefMasterFinalApproveSave", cmdParameters15);


                LockIapproval();
            }
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + MDG + "')</script>", false);
        }



    }
    public Boolean BulkCopySchool(DataTable dt)
    {
        try
        {

            SqlBulkCopyColumnMapping mapping01 = new SqlBulkCopyColumnMapping("DISTRICT NAME", "DISTRICT NAME");
            SqlBulkCopyColumnMapping mapping02 = new SqlBulkCopyColumnMapping("BLOCK NAME", "BLOCK NAME");
            SqlBulkCopyColumnMapping mapping03 = new SqlBulkCopyColumnMapping("CLUSTER NAME", "CLUSTER NAME");
            SqlBulkCopyColumnMapping mapping04 = new SqlBulkCopyColumnMapping("VILLAGE NAME", "VILLAGE NAME");
            SqlBulkCopyColumnMapping mapping05 = new SqlBulkCopyColumnMapping("VILLAGE CODE", "VILLAGE CODE");
            SqlBulkCopyColumnMapping mapping06 = new SqlBulkCopyColumnMapping("SCHOOL NAME", "SCHOOL NAME");
            SqlBulkCopyColumnMapping mapping07 = new SqlBulkCopyColumnMapping("DISE CODE", "DISE CODE");
            //SqlBulkCopyColumnMapping mapping08 = new SqlBulkCopyColumnMapping("Village Operational", "Village Operational");
            SqlBulkCopyColumnMapping mapping09 = new SqlBulkCopyColumnMapping("WORKING STATUS", "WORKING STATUS");
            SqlBulkCopyColumnMapping mapping10 = new SqlBulkCopyColumnMapping("SCHOOL LEVEL", "SCHOOL LEVEL");
            SqlBulkCopyColumnMapping mapping11 = new SqlBulkCopyColumnMapping("SCHOOL TYPE", "SCHOOL TYPE");
            SqlBulkCopyColumnMapping mapping12 = new SqlBulkCopyColumnMapping("GKP SCHOOL", "GKP SCHOOL");
            SqlBulkCopyColumnMapping mapping13 = new SqlBulkCopyColumnMapping("GKP SCHOOL LEVEL", "GKP SCHOOL LEVEL");
            SqlBulkCopyColumnMapping mapping14 = new SqlBulkCopyColumnMapping("GKP++ SCHOOLS", "GKP++ SCHOOLS");
            SqlBulkCopyColumnMapping mapping15 = new SqlBulkCopyColumnMapping("BALSABHA SCHOOL", "BALSABHA SCHOOL");
            SqlBulkCopyColumnMapping mapping16 = new SqlBulkCopyColumnMapping("CLASS", "CLASS");
            SqlBulkCopyColumnMapping mapping17 = new SqlBulkCopyColumnMapping("SCHOOL CAMPUS", "SCHOOL CAMPUS");
            SqlBulkCopyColumnMapping mapping18 = new SqlBulkCopyColumnMapping("TEACHER NAME", "TEACHER NAME");

            SqlBulkCopyColumnMapping mapping19 = new SqlBulkCopyColumnMapping("TEACHER MOBILE NUMBER", "TEACHER MOBILE NUMBER");
            SqlBulkCopyColumnMapping mapping20 = new SqlBulkCopyColumnMapping("TEACHER DESIGNATION", "TEACHER DESIGNATION");
            SqlBulkCopyColumnMapping mapping21 = new SqlBulkCopyColumnMapping("SCHOOL LOCATION", "SCHOOL LOCATION");
            SqlBulkCopyColumnMapping mapping22 = new SqlBulkCopyColumnMapping("CreateBy", "CreateBy");
            SqlBulkCopyColumnMapping mapping23 = new SqlBulkCopyColumnMapping("KGBV LSE SCHOOLS", "KGBV LSE SCHOOLS");
            SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlHelper.mainConnectionString);
            bulkCopy.BatchSize = 5000;
            bulkCopy.BulkCopyTimeout = 5;
            bulkCopy.ColumnMappings.Add(mapping01);
            bulkCopy.ColumnMappings.Add(mapping02);
            bulkCopy.ColumnMappings.Add(mapping03);
            bulkCopy.ColumnMappings.Add(mapping04);
            bulkCopy.ColumnMappings.Add(mapping05);
            bulkCopy.ColumnMappings.Add(mapping06);
            bulkCopy.ColumnMappings.Add(mapping07);
            //bulkCopy.ColumnMappings.Add(mapping08);
            bulkCopy.ColumnMappings.Add(mapping09);
            bulkCopy.ColumnMappings.Add(mapping10);
            bulkCopy.ColumnMappings.Add(mapping11);
            bulkCopy.ColumnMappings.Add(mapping12);
            bulkCopy.ColumnMappings.Add(mapping13);
            bulkCopy.ColumnMappings.Add(mapping14);
            bulkCopy.ColumnMappings.Add(mapping15);
            bulkCopy.ColumnMappings.Add(mapping16);
            bulkCopy.ColumnMappings.Add(mapping17);
            bulkCopy.ColumnMappings.Add(mapping18);
            bulkCopy.ColumnMappings.Add(mapping19);
            bulkCopy.ColumnMappings.Add(mapping20);
            bulkCopy.ColumnMappings.Add(mapping21);
            bulkCopy.ColumnMappings.Add(mapping22);
            bulkCopy.ColumnMappings.Add(mapping23);
            bulkCopy.DestinationTableName = "MasterSchoolBulkUpdate";
            bulkCopy.NotifyAfter = 200;
            bulkCopy.WriteToServer(dt);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public Boolean BulkCopyVillage(DataTable dt)
    {
        try
        {

            SqlBulkCopyColumnMapping mapping01 = new SqlBulkCopyColumnMapping("DISTRICT NAME", "DISTRICT NAME");
            SqlBulkCopyColumnMapping mapping02 = new SqlBulkCopyColumnMapping("BLOCK NAME", "BLOCK NAME");
            SqlBulkCopyColumnMapping mapping03 = new SqlBulkCopyColumnMapping("CLUSTER NAME", "CLUSTER NAME");
            SqlBulkCopyColumnMapping mapping04 = new SqlBulkCopyColumnMapping("VILLAGE NAME", "VILLAGE NAME");
            SqlBulkCopyColumnMapping mapping05 = new SqlBulkCopyColumnMapping("VILLAGE CODE", "VILLAGE CODE");
            SqlBulkCopyColumnMapping mapping06 = new SqlBulkCopyColumnMapping("VILLAGE OPERATIONAL STATUS", "VILLAGE OPERATIONAL STATUS");
            SqlBulkCopyColumnMapping mapping07 = new SqlBulkCopyColumnMapping("VILLAGE GEOGRAPHY", "VILLAGE GEOGRAPHY");
            //SqlBulkCopyColumnMapping mapping08 = new SqlBulkCopyColumnMapping("Village Operational", "Village Operational");
            //SqlBulkCopyColumnMapping mapping09 = new SqlBulkCopyColumnMapping("AGP VILLAGE FLAG", "AGP VILLAGE FLAG");
            SqlBulkCopyColumnMapping mapping10 = new SqlBulkCopyColumnMapping("PANCHAYAT SAMITI", "PANCHAYAT SAMITI");

            SqlBulkCopyColumnMapping mapping22 = new SqlBulkCopyColumnMapping("CreateBy", "CreateBy");
            SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlHelper.mainConnectionString);
            bulkCopy.BatchSize = 5000;
            bulkCopy.BulkCopyTimeout = 5;
            bulkCopy.ColumnMappings.Add(mapping01);
            bulkCopy.ColumnMappings.Add(mapping02);
            bulkCopy.ColumnMappings.Add(mapping03);
            bulkCopy.ColumnMappings.Add(mapping04);
            bulkCopy.ColumnMappings.Add(mapping05);
            bulkCopy.ColumnMappings.Add(mapping06);
            bulkCopy.ColumnMappings.Add(mapping07);
            //bulkCopy.ColumnMappings.Add(mapping08);
            //bulkCopy.ColumnMappings.Add(mapping09);
            bulkCopy.ColumnMappings.Add(mapping10);
            bulkCopy.ColumnMappings.Add(mapping22);
            bulkCopy.DestinationTableName = "MasterSchoolBulkVillage";
            bulkCopy.NotifyAfter = 200;
            bulkCopy.WriteToServer(dt);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public Boolean BulkCopyClusterVillage(DataTable dt)
    {
        try
        {

            SqlBulkCopyColumnMapping mapping01 = new SqlBulkCopyColumnMapping("DISTRICT NAME", "DISTRICT NAME");
            SqlBulkCopyColumnMapping mapping02 = new SqlBulkCopyColumnMapping("BLOCK NAME", "BLOCK NAME");
            SqlBulkCopyColumnMapping mapping04 = new SqlBulkCopyColumnMapping("VILLAGE NAME", "VILLAGE NAME");
            SqlBulkCopyColumnMapping mapping05 = new SqlBulkCopyColumnMapping("VILLAGE CODE", "VILLAGE CODE");
            SqlBulkCopyColumnMapping mapping06 = new SqlBulkCopyColumnMapping("PANCHAYAT NAME", "PANCHAYAT NAME");
            SqlBulkCopyColumnMapping mapping07 = new SqlBulkCopyColumnMapping("PANCHAYAT CODE", "PANCHAYAT CODE");
            //SqlBulkCopyColumnMapping mapping08 = new SqlBulkCopyColumnMapping("Village Operational", "Village Operational");
            SqlBulkCopyColumnMapping mapping09 = new SqlBulkCopyColumnMapping("VILLAGE STATUS", "VILLAGE STATUS");
            SqlBulkCopyColumnMapping mapping10 = new SqlBulkCopyColumnMapping("CLUSTER NAME", "CLUSTER NAME");
            SqlBulkCopyColumnMapping mapping03 = new SqlBulkCopyColumnMapping("CLUSTER CODE", "CLUSTER CODE");
            SqlBulkCopyColumnMapping mapping22 = new SqlBulkCopyColumnMapping("CreateBy", "CreateBy");
            SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlHelper.mainConnectionString);
            bulkCopy.BatchSize = 5000;
            bulkCopy.BulkCopyTimeout = 5;
            bulkCopy.ColumnMappings.Add(mapping01);
            bulkCopy.ColumnMappings.Add(mapping02);
            bulkCopy.ColumnMappings.Add(mapping03);
            bulkCopy.ColumnMappings.Add(mapping04);
            bulkCopy.ColumnMappings.Add(mapping05);
            bulkCopy.ColumnMappings.Add(mapping06);
            bulkCopy.ColumnMappings.Add(mapping07);
            //bulkCopy.ColumnMappings.Add(mapping08);
            bulkCopy.ColumnMappings.Add(mapping09);
            bulkCopy.ColumnMappings.Add(mapping10);
            bulkCopy.ColumnMappings.Add(mapping22);
            bulkCopy.DestinationTableName = "MasterAssignClusterVillage";
            bulkCopy.NotifyAfter = 200;
            bulkCopy.WriteToServer(dt);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public Boolean BulkCopyCluster(DataTable dt)
    {
        try
        {


            SqlBulkCopyColumnMapping mapping04 = new SqlBulkCopyColumnMapping("VILLAGE NAME", "VILLAGE NAME");
            SqlBulkCopyColumnMapping mapping05 = new SqlBulkCopyColumnMapping("VILLAGE CODE", "VILLAGE CODE");

            SqlBulkCopyColumnMapping mapping22 = new SqlBulkCopyColumnMapping("CreateBy", "CreateBy");
            SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlHelper.mainConnectionString);
            bulkCopy.BatchSize = 5000;
            bulkCopy.BulkCopyTimeout = 5;

            bulkCopy.ColumnMappings.Add(mapping04);
            bulkCopy.ColumnMappings.Add(mapping05);

            bulkCopy.ColumnMappings.Add(mapping22);
            bulkCopy.DestinationTableName = "MasterDefineCluster";
            bulkCopy.NotifyAfter = 200;
            bulkCopy.WriteToServer(dt);
            return true;
        }
        catch
        {
            return false;
        }
    }
    protected void btnNewImport_Click(object sender, EventArgs e)
    {

        if (ddlDistrict.SelectedIndex <= 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select District ')</script>", false);
            return;
        }
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
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions += " and v.Blockcode = '" + ddlBlock.SelectedValue + "' ";

        }
        DataSet dt = LoadMasterImport(conditions);

        MultipuExeclTrack(dt);
    }
    public DataSet LoadMasterImport(string Frist)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con", Frist),

        };
        return SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptInportExcel2025]", cmdParameters);
    }
    public void MultipuExeclTrack(DataSet dtMain)
    {
        try
        {
            string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
            string filepath = "";
            XLWorkbook wb = new XLWorkbook();
            wb = new XLWorkbook(StartupPath + "\\SchoolMaster.xlsx");
            var ws = wb.Worksheet(1);
            var ws1 = wb.Worksheet(2);
            var ws42 = wb.Worksheet(4);
            var ws4 = wb.Worksheet(5);

            //var ws1 = wb.Worksheet(2);
            //var ws3 = wb.Worksheet(3);

            //dt.Columns.Remove("rownNO");
            //DataTable dt1 = dtMain1.Tables[1];

            //dt1.Columns.Remove("rownNO");
            DataTable dt = dtMain.Tables[0];
            DataTable dt1 = dtMain.Tables[1];
            DataTable dt2 = dtMain.Tables[2];

            DataTable dt3 = dtMain.Tables[3];
            ws.Cell(2, 1).InsertData(dt.Rows);
            Int32 ii = Convert.ToInt32(dt.Rows.Count) + 1;
            string str = "A2:U" + ii;
            ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);



            ws1.Cell(2, 1).InsertData(dt1.Rows);
            Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 1;
            string str1 = "A2:H" + ii1;
            ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


            ws4.Cell(2, 1).InsertData(dt2.Rows);
            Int32 ii11 = Convert.ToInt32(dt2.Rows.Count) + 1;
            string str2 = "A2:I" + ii11;
            ws4.Range(str2).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws4.Range(str2).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws4.Range(str2).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws4.Range(str2).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


            ws42.Cell(2, 1).InsertData(dt3.Rows);
            Int32 ii114 = Convert.ToInt32(dt3.Rows.Count) + 1;
            string str24 = "A2:B" + ii114;
            ws42.Range(str24).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws42.Range(str24).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws42.Range(str24).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws42.Range(str24).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);






            filepath = StartupPath + "\\Village_and_School_Data_Update_Sheet" + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
        catch
        {
            throw;
        }

    }



    public void MultipuExeclTrackError(DataSet dtMain)
    {
        try
        {
            string StartupPath = Server.MapPath(Comman.GetImagePath("TabletImagePath"));
            string filepath = "";
            XLWorkbook wb = new XLWorkbook();
            wb = new XLWorkbook(StartupPath + "\\SchoolMasterError.xlsx");
            var ws = wb.Worksheet(1);
            var ws1 = wb.Worksheet(2);
            var ws2 = wb.Worksheet(3);
            var ws3 = wb.Worksheet(4);
            //var ws1 = wb.Worksheet(2);
            //var ws3 = wb.Worksheet(3);

            //dt.Columns.Remove("rownNO");
            //DataTable dt1 = dtMain1.Tables[1];

            //dt1.Columns.Remove("rownNO");
            DataTable dt = dtMain.Tables[0];
            DataTable dt1 = dtMain.Tables[1];
            DataTable dt2 = dtMain.Tables[2];
            DataTable dt3 = dtMain.Tables[3];
            ws.Cell(2, 1).InsertData(dt.Rows);
            Int32 ii = Convert.ToInt32(dt.Rows.Count) + 1;
            string str = "A2:V" + ii;
            ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);



            ws1.Cell(2, 1).InsertData(dt1.Rows);
            Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 1;
            string str1 = "A2:H" + ii1;
            ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

            ws2.Cell(2, 1).InsertData(dt2.Rows);
            Int32 ii11 = Convert.ToInt32(dt2.Rows.Count) + 1;
            string str11 = "A2:C" + ii11;
            ws2.Range(str11).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws2.Range(str11).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws2.Range(str11).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws2.Range(str11).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);




            ws3.Cell(2, 1).InsertData(dt3.Rows);
            Int32 ii111 = Convert.ToInt32(dt3.Rows.Count) + 1;
            string str111 = "A2:J" + ii111;
            ws3.Range(str111).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws3.Range(str111).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws3.Range(str111).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws3.Range(str111).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);



            filepath = StartupPath + "\\SchoolMasterError" + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
        catch
        {
            throw;
        }

    }

    public void LoadClass2025()
    {

        SqlParameter[] par = new SqlParameter[]
        {
              new SqlParameter("@Con",  ""),

         };
        DataSet DT = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptClassLookup2025", par);
        Session["dtClassNew"] = DT;
    }
    public void LoadClass()
    {

        SqlParameter[] par = new SqlParameter[]
        {
              new SqlParameter("@Con",  ""),

         };
        DataSet DT = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptClassLookup", par);
        Session["dtClass"] = DT;
    }
    public void ValdateUserLavel()
    {

        string strQry = "";
        string Cond = "Module='Cluster and  School Change Tool' ";
        strQry = "Select * from MstUserRight  where " + Cond + " and Role_Id=" + Session["user_level"].ToString() + "   ";


        DataTable dtRole = objMain.LoadData(strQry);

        if (dtRole.Rows.Count > 0)
        {
            vADD = Convert.ToBoolean(dtRole.Rows[0]["AddStatus"].ToString());
            vVerify = Convert.ToBoolean(dtRole.Rows[0]["verify_Status"].ToString());
            vDelete = Convert.ToBoolean(dtRole.Rows[0]["Delete_status"].ToString());
            ViewState["vADD"] = vADD;
            ViewState["vVerify"] = vVerify;
            ViewState["vDelete"] = vDelete;
        }
        if (vDelete == true)
        {

            btnDelete.Visible = false;
        }
        else
        {

            btnDelete.Visible = false;
        }

        if (vADD == true)
        {
            btnAdd.Enabled = true;
            btnsave.Enabled = true;
            //lblMain.Text = "School Information Campaign";
        }
        else
        {
            btnAdd.Enabled = false;
            btnsave.Enabled = false;
        }

        if (vVerify == true)
        {

            btnsave.Enabled = true;


        }
        if (vVerify == true || vADD == true)
        {
            btnsave.Enabled = true;

        }
        else
        {
            btnsave.Enabled = false;

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

        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");



        ddlYear.SelectedIndex = 1;



    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            ddlState.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlDistrict, null);
            ddlBlock.Items.Clear();
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

    public void Locking()
    {
        if (ddlYear.SelectedIndex > 0)
        {

            btnsave.Enabled = true;
            // LinkButton1.Enabled = true;
            LinkButton2.Enabled = true;
            string strQry;

            strQry = "Select * from mstModuleLocking  where [FromName]='Cluster' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "' ";


            string Year = ddlYear.SelectedItem.Text;
            string[] Year1 = Year.Split('-');



            DateTime date1;
            DateTime date2;
            DataTable dtModel = objMain.LoadData(strQry);
            if (dtModel.Rows.Count > 0)
            {


                date1 = Convert.ToDateTime(dtModel.Rows[0]["lockdate"].ToString());
                date2 = DateTime.Now.Date;





                if (date2 > date1)
                {



                    btnsave.Enabled = false;
                    //   LinkButton1.Enabled = false;
                    LinkButton2.Enabled = false;



                }
            }


        }
    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {

        if (ddlType.SelectedIndex <= 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Type')</script>", false);
            return;
        }
        if (ddlBlock.SelectedIndex <= 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Block')</script>", false);
            return;
        }
        FillGrid();
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
    protected void btnSaveClick_Click(object sender, EventArgs e)
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
        ImageButton9.Attributes.Add("onclick", "javascript:return " + "confirm('Do you really want to create “Village Name” as cluster? ')");

        if (ddlCLusterVillage.SelectedIndex > 0)
        {
            string EGVillagecode = "";
            string strQry = "Select EGVillagecode from mst5Village  where  VillageCode='" + ddlCLusterVillage.SelectedValue.ToString() + "'  ";


            DataTable dtEGVillagecode = objMain.LoadData(strQry);
            if (dtEGVillagecode.Rows.Count > 0)
            {
                EGVillagecode = dtEGVillagecode.Rows[0]["EGVillagecode"].ToString();
            }
            int icount = 0;


            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@ClusterCode", ddlDeleteCluster.SelectedValue),
               new SqlParameter("@Villagecode", ddlCLusterVillage.SelectedValue),
                 new SqlParameter("@Flag", "V"),



        };
            icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "UpdateCluster", cmdParameters);







            SqlParameter[] cmdParameters1 = new SqlParameter[]
        {
            new SqlParameter("@StateCode", ddlState.SelectedValue),
               new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
                  new SqlParameter("@BlockCode", ddlBlock.SelectedValue),
                       new SqlParameter("@ClusterCode", ddlCLusterVillage.SelectedValue),
                     new SqlParameter("@ClusterName", ddlCLusterVillage.SelectedItem.Text),
                               new SqlParameter("@fYear", ddlYear.SelectedItem.Text),
                 new SqlParameter("@EGClusterCode", EGVillagecode),



        };
            icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "UpdateCluster", cmdParameters1);



            if (icount > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                FillGrid();
            }
        }
    }

    protected void btnDeleteClick_Click(object sender, EventArgs e)
    {
        if (ddlDeleteCluster.SelectedIndex > 0)
        {
            string StudentTSInsertQuery = "";
            int Icount = 0;


            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@ClusterCode", ddlDeleteCluster.SelectedValue),
               new SqlParameter("@Villagecode", ""),
                 new SqlParameter("@Flag", "B"),



            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "UpdateCluster", cmdParameters);


            int icount = DeleteCLuster(ddlDeleteCluster.SelectedValue.ToString());

            if (icount > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Delete sucessfully')</script>", false);
                FillGrid();
            }
        }
    }

    public int DeleteCLuster(string ClusterCode)
    {
        int Icount = 0;
        try
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@ClusterCode", ClusterCode),
               new SqlParameter("@UserName", Session["username"].ToString()),




            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "deletecluster", cmdParameters);
        }
        catch
        {
            throw;
        }
        return Icount;
    }

    public int Update_SchoolWorkingStatus(string SchoolCode, int WorkingStatus, int MangmentType, int GKP, int GKPLevel, int SchoolType, int BalType, int SchoolCampus, string TeacherName, string TeacherContactNo, string txtTeacherdesignation, string ClassID, string ClassIDName)
    {
        SqlConnection dbSqlconnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            dbSqlconnection.Open();
            using (SqlCommand dbSqlCommand = (SqlCommand)dbSqlconnection.CreateCommand())
            {
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = "[Update_School_WorkingStatusNew2023]";
                dbSqlCommand.Parameters.AddWithValue("@SchoolCode", SchoolCode);
                dbSqlCommand.Parameters.AddWithValue("@WorkingStatus", WorkingStatus);
                dbSqlCommand.Parameters.AddWithValue("@MangmentType", MangmentType);
                dbSqlCommand.Parameters.AddWithValue("@GKP", GKP);
                dbSqlCommand.Parameters.AddWithValue("@GKPLevel", GKPLevel);
                dbSqlCommand.Parameters.AddWithValue("@SchoolType", SchoolType);
                dbSqlCommand.Parameters.AddWithValue("@BalType", BalType);
                dbSqlCommand.Parameters.AddWithValue("@SchoolCampus", SchoolCampus);
                dbSqlCommand.Parameters.AddWithValue("@TeacherName", TeacherName);
                dbSqlCommand.Parameters.AddWithValue("@TeacherContactNo", TeacherContactNo);
                dbSqlCommand.Parameters.AddWithValue("@Teacherdesignation", txtTeacherdesignation);
                dbSqlCommand.Parameters.AddWithValue("@ClassID", ClassID);
                dbSqlCommand.Parameters.AddWithValue("@ClassIDName", ClassIDName);
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
            throw;
        }
        finally
        {
            dbSqlconnection.Dispose();
        }
    }
    public int Update_SchoolWorkingStatus2025(string SchoolCode, int WorkingStatus, int MangmentType, int GKP, int GKPLevel, int SchoolType, int BalType, int SchoolCampus, string TeacherName, string TeacherContactNo, string txtTeacherdesignation, string ClassID, string ClassIDName, string GKPPlus, string LSG, string DonorID, string DonorIDName)
    {
        SqlConnection dbSqlconnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            dbSqlconnection.Open();
            using (SqlCommand dbSqlCommand = (SqlCommand)dbSqlconnection.CreateCommand())
            {
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = "[Update_School_WorkingStatusNew2026]";
                dbSqlCommand.Parameters.AddWithValue("@SchoolCode", SchoolCode);
                dbSqlCommand.Parameters.AddWithValue("@WorkingStatus", WorkingStatus);
                dbSqlCommand.Parameters.AddWithValue("@MangmentType", MangmentType);
                dbSqlCommand.Parameters.AddWithValue("@GKP", GKP);
                dbSqlCommand.Parameters.AddWithValue("@GKPLevel", GKPLevel);
                dbSqlCommand.Parameters.AddWithValue("@SchoolType", SchoolType);
                dbSqlCommand.Parameters.AddWithValue("@BalType", BalType);
                dbSqlCommand.Parameters.AddWithValue("@SchoolCampus", SchoolCampus);
                dbSqlCommand.Parameters.AddWithValue("@TeacherName", TeacherName);
                dbSqlCommand.Parameters.AddWithValue("@TeacherContactNo", TeacherContactNo);
                dbSqlCommand.Parameters.AddWithValue("@Teacherdesignation", txtTeacherdesignation);
                dbSqlCommand.Parameters.AddWithValue("@ClassID", ClassID);
                dbSqlCommand.Parameters.AddWithValue("@ClassIDName", ClassIDName);
                dbSqlCommand.Parameters.AddWithValue("@GKPPlus", GKPPlus);
                dbSqlCommand.Parameters.AddWithValue("@LSG", LSG);
                dbSqlCommand.Parameters.AddWithValue("@DonorID", DonorID);
                dbSqlCommand.Parameters.AddWithValue("@DonorIDName", DonorIDName);

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
            throw;
        }
        finally
        {
            dbSqlconnection.Dispose();
        }
    }

    public int Update_VillageCluster(string VillageCode, string ClusterCode, string VillageGeography, string VillageOperational, string CBlVillage, string FunctionalStatus, string AGPStatus, string TempID, string PanchayatSamiti)
    {
        SqlConnection dbSqlconnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            dbSqlconnection.Open();
            using (SqlCommand dbSqlCommand = (SqlCommand)dbSqlconnection.CreateCommand())
            {
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = "[Update_Village_Cluster2024]";
                dbSqlCommand.Parameters.AddWithValue("@VillageCode", VillageCode);
                dbSqlCommand.Parameters.AddWithValue("@ClusterCode", ClusterCode);
                dbSqlCommand.Parameters.AddWithValue("@VillageGeography", VillageGeography);
                dbSqlCommand.Parameters.AddWithValue("@CBlVillage", CBlVillage);
                dbSqlCommand.Parameters.AddWithValue("@FunctionalStatus", FunctionalStatus);
                dbSqlCommand.Parameters.AddWithValue("@VillageGeographyOperational", VillageOperational);
                dbSqlCommand.Parameters.AddWithValue("@AGPStatus", AGPStatus);
                dbSqlCommand.Parameters.AddWithValue("@tempID", TempID);
                dbSqlCommand.Parameters.AddWithValue("@dist", ddlDistrict.SelectedValue);
                dbSqlCommand.Parameters.AddWithValue("@UserID", Session["username"].ToString());
                dbSqlCommand.Parameters.AddWithValue("@PanchayatSamiti", PanchayatSamiti);
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
            throw;
        }
        finally
        {
            dbSqlconnection.Dispose();
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        if (Session["GridViewData"] != null)
        {
            UpdateData();
            int ret = 0;
            DataTable Dt = Session["GridViewData"] as DataTable;

            // DataRow[] dr = Dt.Select(Cond);
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(ddlType.SelectedValue) == 2)
                {
                    string SchoolCode = Dt.Rows[i]["SchoolCode"].ToString();
                    Int32 WorkingStatus = Convert.ToInt32(Dt.Rows[i]["WorkingStatus"].ToString());
                    Int32 Management = Convert.ToInt32(Dt.Rows[i]["Management"].ToString());
                    Int32 GKP = Convert.ToInt32(Dt.Rows[i]["GKP"].ToString()); ;
                    Int32 GKPLevel = Convert.ToInt32(Dt.Rows[i]["GKPLevel"].ToString());
                    Int32 SchoolType = Convert.ToInt32(Dt.Rows[i]["SchoolType"].ToString());
                    Int32 BAlVal = Convert.ToInt32(Dt.Rows[i]["BAlVal"].ToString());
                    Int32 SchoolCampus = Convert.ToInt32(Dt.Rows[i]["SchoolCampus"].ToString());

                    string TeacherName = Convert.ToString(Dt.Rows[i]["TeacherName"].ToString());
                    string TeacherContactNo = Convert.ToString(Dt.Rows[i]["TeacherContactNo"].ToString());
                    string Teacherdesignation = Convert.ToString(Dt.Rows[i]["Teacherdesignation"].ToString());
                    string ClassID = Convert.ToString(Dt.Rows[i]["ClassID"].ToString());
                    string ClassIDName = Convert.ToString(Dt.Rows[i]["ClassIDName"].ToString());
                    string FunctionalStatus = Convert.ToString(Dt.Rows[i]["FunctionalStatus"].ToString());
                    string GKPPlus = Convert.ToString(Dt.Rows[i]["GKPPlus"].ToString());
                    string LSG = Convert.ToString(Dt.Rows[i]["LSG"].ToString());
                    string DonorID = Convert.ToString(Dt.Rows[i]["DonorID"].ToString());
                    string DonorIDName = Convert.ToString(Dt.Rows[i]["School Donor Name"].ToString());

                    if (SchoolCampus == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select School campus')</script>", false);
                        return;
                    }
                    if (FunctionalStatus == "9")
                    {
                        if (WorkingStatus == 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Working Status')</script>", false);
                            return;
                        }
                        if (SchoolType == 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select School Level')</script>", false);
                            return;
                        }
                        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
                        {
                            ret = Update_SchoolWorkingStatus2025(SchoolCode, WorkingStatus, Management, GKP, GKPLevel, SchoolType, BAlVal, SchoolCampus, TeacherName, TeacherContactNo, Teacherdesignation, ClassID, ClassIDName, GKPPlus, LSG, DonorID, DonorIDName);

                        }
                        else
                        {
                            ret = Update_SchoolWorkingStatus(SchoolCode, WorkingStatus, Management, GKP, GKPLevel, SchoolType, BAlVal, SchoolCampus, TeacherName, TeacherContactNo, Teacherdesignation, ClassID, ClassIDName);

                        }
                    }

                    if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
                    {

                        int icount = 0;
                        SqlParameter[] cmdParameters = new SqlParameter[]
                       {
            new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
            new SqlParameter("@approveStataus", "0"),
            new SqlParameter("@Remark", ""),
             new SqlParameter("@UserName", Convert.ToString(Session["username"])),
               new SqlParameter("@Flag", "1"),



                       };
                        icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatefMasterFinalApproveSave", cmdParameters);
                        LockIapproval();
                    }
                }
                if (Convert.ToInt32(ddlType.SelectedValue) == 1 || Convert.ToInt32(ddlType.SelectedValue) == 3)
                {

                    string VillageCode = Dt.Rows[i]["TempVillageCode"].ToString();
                    string ClusterCode = Dt.Rows[i]["ClusterCode"].ToString();
                    string VillageGeography = Dt.Rows[i]["VillageGeography"].ToString();
                    string VillageOperational = Dt.Rows[i]["VillageGeographyOperational"].ToString();

                    string CBlVillage = Dt.Rows[i]["CBlVillage"].ToString();
                    string FunctionalStatus = Dt.Rows[i]["FunctionalStatus"].ToString();
                    string AGPStatus = Dt.Rows[i]["AGPStatus"].ToString();
                    string TeacherContactNo = Dt.Rows[i]["TeacherContactNo"].ToString();
                    string PanchayatSamiti = Dt.Rows[i]["PanchayatSamiti"].ToString();
                    if (FunctionalStatus == "9")
                    {
                        if (VillageOperational == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Village Operational Status')</script>", false);
                            return;
                        }

                        ret = Update_VillageCluster(VillageCode, ClusterCode, VillageGeography, VillageOperational, CBlVillage, FunctionalStatus, AGPStatus, TeacherContactNo, PanchayatSamiti);

                        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
                        {

                            int icount = 0;
                            SqlParameter[] cmdParameters = new SqlParameter[]
                           {
            new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
            new SqlParameter("@approveStataus", "0"),
            new SqlParameter("@Remark", ""),
             new SqlParameter("@UserName", Convert.ToString(Session["username"])),
               new SqlParameter("@Flag", "1"),



                           };
                            icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatefMasterFinalApproveSave", cmdParameters);
                            LockIapproval();
                        }
                    }
                }



            }

            if (ret > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
            }
        }
    }

    private int Update_AnnualExamStatus(string str, string UID, string p)
    {
        int iReturnValue = 0;
        try
        {
            iReturnValue = objComman.Update_AnnualExamStatus(str, UID, Flag);
        }
        catch (Exception exp)
        {

        }
        return iReturnValue;
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
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //   objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            AlllStateCode();
            ddlState.SelectedIndex = 0;
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
            //conditions = "";
            //conditions = "StateCode ='" + ddlState.SelectedValue + "' and Fyear= '" + ddlYear.SelectedItem.Text + "' ";
            //objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

            //ddlDistrict.SelectedIndex = 0;

            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");
            //ddlState_SelectedIndexChanged(ddlState, null);
        }

        else
        {


            conditions = "";
            //conditions = "StateCode ='" + ddlState.SelectedValue + "'  and Fyear= '2019-2020' ";

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
                    ddlYear.Enabled = false;
                }
            }
            else
            {
                ddlYear.Enabled = false;
            }
            ddlDistrict.SelectedIndex = 1;
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }





    }
    protected void btnAddCluster(object sender, EventArgs e)
    {
        if (ddlBlock.SelectedIndex <= 0)
        {

            this.ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Block')</script>", false);
            return;
        }
        conditions = "";
        if (ddlState.SelectedIndex > 0)
        {
            conditions = "  mst5Village.StateCode='" + ddlState.SelectedValue + "'";

        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode='" + ddlDistrict.SelectedValue + "'";

        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode='" + ddlBlock.SelectedValue + "'";
        }

        if (ddlVillage.SelectedIndex > 0)
        {
            conditions = conditions + " and mst5Village.VillageCode='" + ddlVillage.SelectedValue + "'";
        }
        conditions = conditions + " and (mstCluster.ClusterCode is null ) ";

        objComman.BindDLL("mst5Village left  join mstCluster on mstCluster.ClusterCode=mst5Village.VillageCode  ", "VillageCode,VillageName ", conditions, "VillageName", "asc", ddlCLusterVillage, "VillageName", "VillageCode", "--Select--");
        ModalPopupExtender1.Show();
    }

    protected void btnDeleteCluster(object sender, EventArgs e)
    {
        if (ddlBlock.SelectedIndex <= 0)
        {

            this.ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Block')</script>", false);
            return;
        }
        conditions = "";
        if (ddlState.SelectedIndex > 0)
        {
            conditions = "  mst5Village.StateCode='" + ddlState.SelectedValue + "'";

        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode='" + ddlDistrict.SelectedValue + "'";

        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode='" + ddlBlock.SelectedValue + "'";
        }

        if (ddlVillage.SelectedIndex > 0)
        {
            conditions = conditions + " and mst5Village.VillageCode='" + ddlVillage.SelectedValue + "'";
        }
        //  conditions = conditions + " and (mstCluster.ClusterCode is null or mstCluster.ClusterCode='' or mstCluster.ClusterCode=mst5Village.VillageCode) ";

        objComman.BindDLL("mstCluster   left  join mst5Village on mst5Village.ClusterCode=mstCluster.ClusterCode ", "mstCluster.ClusterCode as ClusterCode ,mstCluster.ClusterName as ClusterName ", conditions, "ClusterName", "asc", ddlDeleteCluster, "ClusterName", "ClusterCode", "--Select--");
        ModalPopupExtender2.Show();
    }
    public void FillGrid()
    {
        try
        {
            conditions = "";
            string conditionsCLuster = "";
            if (ddlState.SelectedIndex > 0)
            {
                conditions = " where V.StateCode='" + ddlState.SelectedValue + "'";
                conditionsCLuster = " where D.StateCode='" + ddlState.SelectedValue + "'";
            }
            if (ddlDistrict.SelectedIndex > 0)
            {
                conditions = conditions + " and V.DistrictCode='" + ddlDistrict.SelectedValue + "'";
                conditionsCLuster = conditionsCLuster + " and mstCluster.DistrictCode='" + ddlDistrict.SelectedValue + "'";
            }

            if (ddlBlock.SelectedIndex > 0)
            {
                conditions = conditions + " and V.BlockCode='" + ddlBlock.SelectedValue + "'";
            }
            if (ddlPanchayat.SelectedIndex > 1)
            {
                conditions = conditions + " and V.PanchayatCode='" + ddlPanchayat.SelectedValue + "'";
            }
            if (ddlVillage.SelectedIndex > 0)
            {
                conditions = conditions + " and V.VillageCode='" + ddlVillage.SelectedValue + "'";
            }

            if (Convert.ToInt32(ddlType.SelectedValue) == 1 || Convert.ToInt32(ddlType.SelectedValue) == 3)
            {
                SqlParameter[] par1 = new SqlParameter[]
                {
                      new SqlParameter("@Condition",  conditionsCLuster),
                      new SqlParameter("@Flag", 4 ),
                };
                DataTable DTcluster = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptReportClusterChange", par1);
                Session["DTcluster"] = DTcluster;
            }

            SqlParameter[] par = new SqlParameter[]
            {
              new SqlParameter("@Condition",  conditions),
              new SqlParameter("@Flag",  ddlType.SelectedValue),

             };
            DataTable DT = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptReportClusterChange", par);
            Session["GridViewData"] = DT;
            GVCluster.Visible = true;
            if (DT.Rows.Count > 0)
            {
                GVCluster.DataSource = DT;
                GVCluster.DataBind();
            }
            else
            {
                GVCluster.DataSource = null;
                GVCluster.DataBind();

            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 2)
            {
                GVCluster.Columns[1].Visible = false;

                GVCluster.Columns[9].Visible = true;
                GVCluster.Columns[10].Visible = true;
                GVCluster.Columns[11].Visible = true;
                GVCluster.Columns[12].Visible = true;
                GVCluster.Columns[13].Visible = false;
                GVCluster.Columns[14].Visible = false;
                GVCluster.Columns[15].Visible = false;
                GVCluster.Columns[16].Visible = true;
                GVCluster.Columns[17].Visible = true;
                GVCluster.Columns[18].Visible = true;
                GVCluster.Columns[19].Visible = true;
                GVCluster.Columns[20].Visible = false;
                GVCluster.Columns[21].Visible = false;
                GVCluster.Columns[22].Visible = false;
                if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
                {
                    GVCluster.Columns[23].Visible = true;
                    GVCluster.Columns[24].Visible = true;
                    GVCluster.Columns[27].Visible = true;
                }
                else
                {
                    GVCluster.Columns[23].Visible = false;
                    GVCluster.Columns[24].Visible = false;
                    GVCluster.Columns[27].Visible = false;
                }

                GVCluster.Columns[25].Visible = true;
                GVCluster.Columns[26].Visible = true;
                ///GVCluster.Columns[27].Visible = true;

                GVCluster.Columns[28].Visible = true;

                GVCluster.Columns[29].Visible = true;
                GVCluster.Columns[30].Visible = true;
                LinkButton1.Visible = false;
                LinkButton2.Visible = false;
                GVCluster.Columns[31].Visible = false;
                GVCluster.Width = Unit.Percentage(140);
                // GVCluster.Height = 800;

            }
            else
            {
                GVCluster.Columns[1].Visible = false;
                GVCluster.Columns[9].Visible = false;
                GVCluster.Columns[10].Visible = false;
                GVCluster.Columns[11].Visible = false;
                GVCluster.Columns[12].Visible = false;
                GVCluster.Columns[13].Visible = true;
                GVCluster.Columns[14].Visible = true;
                GVCluster.Columns[15].Visible = true;
                GVCluster.Columns[16].Visible = false;
                GVCluster.Columns[17].Visible = false;
                GVCluster.Columns[18].Visible = false;
                GVCluster.Columns[19].Visible = false;
                GVCluster.Columns[20].Visible = false;
                GVCluster.Columns[21].Visible = false;
                GVCluster.Columns[22].Visible = true;
                GVCluster.Columns[23].Visible = false;
                GVCluster.Columns[24].Visible = false;
                GVCluster.Columns[25].Visible = false;
                GVCluster.Columns[26].Visible = false;
                GVCluster.Columns[27].Visible = false;
                GVCluster.Columns[28].Visible = false;
                GVCluster.Columns[29].Visible = false;
                GVCluster.Columns[30].Visible = false;
                GVCluster.Columns[31].Visible = true;
                // LinkButton1.Visible = true;
                if (btnsave.Visible == true)
                {
                    LinkButton2.Visible = true;
                }
                else
                {
                    LinkButton2.Visible = false;
                }
                GVCluster.Width = Unit.Percentage(100);
            }

        }
        catch (Exception)
        {

            throw;
        }

    }
    #region Fill Master Data
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
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and  mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        //else
        //{
        //    conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCodeNew"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";


        //}
        else
        {
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";


        }
        if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "";
            conditions = " mst2District.StateCode ='" + ddlState.SelectedValue + "' and UserName='" + Session["username"].ToString() + "' ";
            string strQry1 = "       sELECT distinct mst2District.DistrictCode as DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist  ";
            strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions + "  and  Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName  ";
            DataTable dtDistrict = objMain.LoadData(strQry1);

            objComman.BindDLLDatatable("mst2District", dtDistrict, "DistrictCode, dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "Desc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

        }
        else
        {
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        }

    }
    public void FillCBBock()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        if (Session["user_level"].ToString() == "19")
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 and BlockCode in(" + Session["DistrictCodeNew"].ToString() + ")";
        }
        else
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");



    }
    public void FillCVillage()
    {
        conditions = "";
        ////conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        ////objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--Select--");

        if (Convert.ToString(ddlPanchayat.SelectedValue) == "1")
        {
            conditions = "mst5Village.DistrictCode ='" + ddlDistrict.SelectedValue + "'  and mst5Village.BlockCode ='" + ddlBlock.SelectedValue + "'  ";

        }
        else
        {
            conditions = "mst5Village.DistrictCode ='" + ddlDistrict.SelectedValue + "'  and mst5Village.BlockCode ='" + ddlBlock.SelectedValue + "' and  mst5Village.PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";

        }

        string strQry = "  SELECT mst5Village.VillageCode, dbo.TitleCase(upper((mst5Village.VillageName))) + ' (' + dbo.TitleCase(upper(mst5Village.EGVillageCode)) +')'   as VillageName FROM mst5Village INNER JOIN mstPanchayat ON mst5Village.PanchayatCode = mstPanchayat.PanchayatCode where " + conditions + "  order by VillageName   ";
        DataTable dtVillage = objMain.LoadData(strQry);

        objComman.BindDLLMasterTableVillage("mst5Village", "VillageName,VillageCode", dtVillage, conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "Select");


    }


    ////public void FillSchool()
    ////{
    ////    conditions = "";
    ////    if (ddlBlock.SelectedIndex > 0)
    ////    {
    ////        conditions = "BlockCode ='" + ddlBlock.SelectedValue + "' ";
    ////    }
    ////    if (ddlVillage.SelectedIndex > 0)
    ////    {
    ////        conditions = "VillageCode ='" + ddlVillage.SelectedValue + "' ";
    ////    }

    ////    objComman.BindDLL("mstSchool", "SchoolCode,Name", conditions, "Name", "asc", ddlSchool, "Name", "SchoolCode", "Select");

    ////}

    #endregion

    #region   SelectedIndexChanged Methods
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
        LockIapproval();
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillage();
    }

    //protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (Convert.ToInt32(ddlType.SelectedValue) == 1)
    //    {
    //        lblShool.Visible = false;
    //        ddlSchool.Visible = false;
    //    }
    //    else
    //    {
    //        lblShool.Visible = true;
    //        ddlSchool.Visible = true;
    //    }
    //}
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        Locking();
        FillCBBock();
        LockIapproval();
    }

    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        // FillCVillage();
        FillCBCluster();
        //  FillSchool();
        Locking();
        LockIapproval();
    }
    public void LockIapproval()
    {
        if (Convert.ToString(Session["user_level"]) == "39" || Convert.ToString(Session["user_level"]) == "136" || Convert.ToString(Session["user_level"]) == "145" || Convert.ToString(Session["user_level"]) == "145")
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
       {

              new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
               new SqlParameter("@Flag",Convert.ToString(Session["user_level"])),

       };
            DataTable dtSchool = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadMasterApprovalStatus]", cmdParameters);
            if (dtSchool.Rows.Count > 0)
            {

                if (dtSchool.Rows[0]["ApproveSatus"].ToString() == "0" && dtSchool.Rows[0]["AdminLock"].ToString() == "0")
                {
                    btnSubmit.Enabled = true;
                    btnSubmit.Visible = true;
                    btnSubmit.Text = "Submit to DOL";
                    Button2.Visible = true;
                    divUp.Visible = true;

                }
                else if (dtSchool.Rows[0]["ApproveSatus"].ToString() == "0" && dtSchool.Rows[0]["AdminLock"].ToString() == "99")
                {
                    btnSubmit.Enabled = true;
                    btnSubmit.Visible = true;
                    btnSubmit.Text = "Submit to SIS";
                    Button2.Visible = true;
                    divUp.Visible = true;

                }
                else if (dtSchool.Rows[0]["ApproveSatus"].ToString() == "1" && dtSchool.Rows[0]["AdminLock"].ToString() == "0")
                {
                    btnSubmit.Enabled = false;
                    btnSubmit.Visible = true;
                    btnsave.Visible = false;
                    Button2.Visible = false;
                    btnSubmit.Text = "Submitted to DOL for Approval";
                    divUp.Visible = false;

                }
                else if (dtSchool.Rows[0]["ApproveSatus"].ToString() == "1" && dtSchool.Rows[0]["AdminLock"].ToString() == "99")
                {
                    btnSubmit.Enabled = false;
                    btnSubmit.Visible = true;
                    btnsave.Visible = false;
                    Button2.Visible = false;
                    btnSubmit.Text = "Submitted to SIS for Approval";
                    divUp.Visible = false;

                }
                else if (dtSchool.Rows[0]["ApproveSatus"].ToString() == "2" && dtSchool.Rows[0]["AdminLock"].ToString() == "99")
                {
                    btnSubmit.Enabled = false;
                    btnSubmit.Visible = true;
                    btnsave.Visible = false;
                    Button2.Visible = false;
                    btnSubmit.Text = "Submitted to SIS for Approval";
                    divUp.Visible = false;

                }
                else if (dtSchool.Rows[0]["ApproveSatus"].ToString() == "2" && dtSchool.Rows[0]["AdminLock"].ToString() == "0")
                {
                    btnSubmit.Enabled = false;
                    btnSubmit.Visible = true;
                    btnsave.Visible = false;
                    Button2.Visible = false;
                    btnSubmit.Text = "Approved by DOL";
                    divUp.Visible = false;
                    btnReject.Visible = false;

                }
                else if (dtSchool.Rows[0]["ApproveSatus"].ToString() == "4" && dtSchool.Rows[0]["AdminLock"].ToString() == "99")
                {
                    btnSubmit.Enabled = false;
                    btnSubmit.Visible = true;
                    btnsave.Visible = false;
                    Button2.Visible = false;
                    btnSubmit.Text = "Master Data Lock";
                    divUp.Visible = false;

                }
                else if (dtSchool.Rows[0]["ApproveSatus"].ToString() == "2")
                {
                    btnSubmit.Enabled = false;
                    btnSubmit.Visible = true;
                    btnsave.Visible = false;
                    Button2.Visible = false;
                    btnSubmit.Text = "Approved by DOL";
                    divUp.Visible = false;
                    btnReject.Visible = false;
                }
                else
                {
                    btnSubmit.Enabled = true;
                    btnSubmit.Visible = true;
                    btnSubmit.Text = "Submit to DOL";
                    Button2.Visible = true;
                    divUp.Visible = true;

                }
            }
            else
            {
                btnSubmit.Enabled = false;
                btnSubmit.Visible = false;
                btnSubmit.Text = "Submit to DOL";
                Button2.Visible = true;
                divUp.Visible = true;
            }

        }
        else if (Convert.ToString(Session["user_level"]) == "59")
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
      {

                      new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
                       new SqlParameter("@Flag",Convert.ToString(Session["user_level"])),

      };
            DataTable dtSchool = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadMasterApprovalStatus]", cmdParameters);
            if (dtSchool.Rows.Count > 0)
            {
                if (dtSchool.Rows[0]["ApproveSatus"].ToString() == "1" && dtSchool.Rows[0]["AdminLock"].ToString() == "99")
                {
                    btnSubmit.Enabled = true;
                    btnSubmit.Visible = true;
                    btnsave.Visible = false;
                    Button2.Visible = false;
                    // btnSubmit.Text = "Submitted to SIS for Approval";
                    btnSubmit.Text = "Approval";
                    divUp.Visible = false;
                    btnReject.Visible = false;
                }
                if (dtSchool.Rows[0]["ApproveSatus"].ToString() == "4" && dtSchool.Rows[0]["AdminLock"].ToString() == "99")
                {
                    btnSubmit.Enabled = false;
                    btnSubmit.Visible = true;
                    btnsave.Visible = false;
                    Button2.Visible = false;
                    btnSubmit.Text = "Master Data Lock";
                    divUp.Visible = false;
                    btnReject.Visible = false;
                }
            }
            else
            {
                btnSubmit.Enabled = false;
                btnSubmit.Visible = false;
                btnsave.Visible = false;
                Button2.Visible = false;
                btnSubmit.Text = "Master Data Lock";
                divUp.Visible = false;
                btnReject.Visible = false;
            }
        }
        else if (Convert.ToString(Session["user_level"]) == "91")
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
      {

              new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
               new SqlParameter("@Flag",Convert.ToString(Session["user_level"])),

      };
            DataTable dtSchool = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadMasterApprovalStatus]", cmdParameters);
            if (dtSchool.Rows.Count > 0)
            {
                if (dtSchool.Rows[0]["ApproveSatus"].ToString() == "1" && dtSchool.Rows[0]["AdminLock"].ToString() == "0")
                {
                    btnSubmit.Enabled = true;
                    btnSubmit.Visible = true;
                    btnsave.Visible = false;
                    Button2.Visible = false;
                    // btnSubmit.Text = "Submitted to DOL for Approval";
                    btnSubmit.Text = "Approval";
                    divUp.Visible = false;
                    btnReject.Visible = true;
                }
                else if (dtSchool.Rows[0]["ApproveSatus"].ToString() == "4" && dtSchool.Rows[0]["AdminLock"].ToString() == "99")
                {
                    btnSubmit.Enabled = false;
                    btnSubmit.Visible = true;
                    btnsave.Visible = false;
                    Button2.Visible = false;
                    btnSubmit.Text = "Master Data Lock";
                    divUp.Visible = false;

                }
                else if (dtSchool.Rows[0]["ApproveSatus"].ToString() == "2" && dtSchool.Rows[0]["AdminLock"].ToString() == "0")
                {
                    btnSubmit.Enabled = false;
                    btnSubmit.Visible = true;
                    btnsave.Visible = false;
                    Button2.Visible = false;
                    btnSubmit.Text = "Approved by DOL";
                    divUp.Visible = false;
                    btnReject.Visible = false;
                }
                else
                {
                    btnSubmit.Enabled = true;
                    btnSubmit.Visible = false;
                    btnsave.Visible = false;
                    btnReject.Visible = false;
                }
            }
            else
            {
                btnSubmit.Enabled = true;
                btnSubmit.Visible = false;
                btnsave.Visible = false;
                btnReject.Visible = false;
            }
        }
        else
        {
            btnSubmit.Visible = false;
            btnsave.Visible = false;
            divUp.Visible = false;
        }
        if (Convert.ToString(Session["username"]) == "PMSAdmin" || Convert.ToString(Session["username"]) == "EGE7557" || Convert.ToString(Session["username"]) == "SuperAdmin")
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
             {

                      new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
                       new SqlParameter("@Flag",Convert.ToString(Session["user_level"])),

             };
            DataTable dtSchool = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadMasterApprovalStatus]", cmdParameters);
            if (dtSchool.Rows.Count > 0)
            {
                if (dtSchool.Rows[0]["ApproveSatus"].ToString() == "2" || dtSchool.Rows[0]["ApproveSatus"].ToString() == "1" || dtSchool.Rows[0]["ApproveSatus"].ToString() == "4")
                {
                    LinkButton3.Visible = true;
                }
                else
                {
                    LinkButton3.Visible = false;
                }
                btnsave.Visible = false;
            }
            else
            {
                LinkButton3.Visible = false;
                btnsave.Visible = true;
            }
        }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        ModalPopupExtender3.Show();
    }
    protected void btnSubmitted_Click(object sender, EventArgs e)
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
        if (Convert.ToString(Session["username"]) != "")
        {
        }
        else
        {
            Response.Redirect("Login.aspx", false);
        }
        if (ddlDistrict.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select District')</script>", false);

            return;

        }
        int approveStataus = 0;
        int Flag = 0;
        if (Convert.ToString(Session["user_level"]) == "39" || Convert.ToString(Session["user_level"]) == "136" || Convert.ToString(Session["user_level"]) == "145")
        {
            SqlParameter[] cmdParameters1 = new SqlParameter[]
             {

                      new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
                       new SqlParameter("@Flag",Convert.ToString(Session["user_level"])),

             };
            DataTable dtSchool = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadMasterApprovalStatus]", cmdParameters1);
            if (dtSchool.Rows.Count > 0)
            {
                if (dtSchool.Rows[0]["AdminLock"].ToString() == "99")
                {
                    Flag = 1;
                }
            }
            approveStataus = 1;
        }
        if (Convert.ToString(Session["user_level"]) == "91")
        {
            approveStataus = 2;
        }
        if (Convert.ToString(Session["user_level"]) == "59")
        {
            approveStataus = 4;
        }
        int icount = 0;
        SqlParameter[] cmdParameters = new SqlParameter[]
       {
            new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
            new SqlParameter("@approveStataus", approveStataus),
            new SqlParameter("@Remark", ""),
             new SqlParameter("@UserName", Convert.ToString(Session["username"])),
               new SqlParameter("@Flag", "1"),



       };
        icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatefMasterFinalApprove", cmdParameters);




        if (icount > 0)
        {
            if (Convert.ToString(Session["user_level"]) == "39" || Convert.ToString(Session["user_level"]) == "136" || Convert.ToString(Session["user_level"]) == "145")
            {
                if (Flag == 1)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Data Successfully Submitted to SIS!!')</script>", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Data Successfully Submitted to DOL!!')</script>", false);

                }
            }
            if (Convert.ToString(Session["user_level"]) == "91")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Approved by DOL!!')</script>", false);

            }
            if (Convert.ToString(Session["user_level"]) == "59")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Master Data successfully Lock!')</script>", false);
            }

            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);

        }


    }

    protected void btnLock_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {
        }
        else
        {
            Response.Redirect("Login.aspx", false);
        }
        if (ddlDistrict.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select District')</script>", false);

            return;

        }
        int approveStataus = 99;

        int icount = 0;
        SqlParameter[] cmdParameters = new SqlParameter[]
       {
            new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
            new SqlParameter("@approveStataus", approveStataus),
            new SqlParameter("@Remark", ""),
             new SqlParameter("@UserName", Convert.ToString(Session["username"])),
               new SqlParameter("@Flag", "1"),



       };
        icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatefMasterFinalApproveAdmin", cmdParameters);




        if (icount > 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Data Successfully Unlock!!')</script>", false);


            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);

        }


    }


    protected void btnsaveReject_Click(object sender, EventArgs e)
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
        if (Convert.ToString(Session["username"]) != "")
        {
        }
        else
        {
            Response.Redirect("Login.aspx", false);
        }
        if (ddlDistrict.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select District')</script>", false);

            return;

        }
        int approveStataus = 0;
        if (Convert.ToString(Session["user_level"]) == "39" || Convert.ToString(Session["user_level"]) == "136" || Convert.ToString(Session["user_level"]) == "145")
        {
            approveStataus = 1;
        }
        if (Convert.ToString(Session["user_level"]) == "91")
        {
            approveStataus = 3;
        }
        if (Convert.ToString(Session["user_level"]) == "59")
        {
            approveStataus = 3;
        }
        int icount = 0;
        SqlParameter[] cmdParameters = new SqlParameter[]
       {
            new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
            new SqlParameter("@approveStataus", approveStataus),
            new SqlParameter("@Remark", txtRemark.Text),
             new SqlParameter("@UserName", Convert.ToString(Session["username"])),
               new SqlParameter("@Flag", "2"),



       };
        icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatefMasterFinalApprove", cmdParameters);




        if (icount > 0)
        {
            if (Convert.ToString(Session["user_level"]) == "39" || Convert.ToString(Session["user_level"]) == "136" || Convert.ToString(Session["user_level"]) == "145")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Data Successfully Submitted to DOL!!')</script>", false);

            }
            if (Convert.ToString(Session["user_level"]) == "91")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reject Successfully!!')</script>", false);

            }
            if (Convert.ToString(Session["user_level"]) == "59")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Congratulations your Annual Plan has been successfully approved!')</script>", false);
            }

            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);

        }


    }

    public void FillCBCluster()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "'";
        objComman.BindDLLSelectAll("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "Select");



    }

    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillSchool();
    }

    #endregion

    protected void GV_Cluster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        UpdateData();
        GVCluster.PageIndex = e.NewPageIndex;
        if (Session["GridViewData"] != null)
        {
            DataTable dt = Session["GridViewData"] as DataTable;
            GVCluster.DataSource = dt;
            GVCluster.DataBind();
        }


    }
    public void UpdateData()
    {

        DataTable dt = (DataTable)Session["GridViewData"];

        for (int i = 0; i < GVCluster.Rows.Count; i++)
        {
            if (Convert.ToInt32(ddlType.SelectedValue) == 2)
            {


                DropDownList ddlWorkingStatus = (DropDownList)GVCluster.Rows[i].FindControl("ddlWorkingStatus");
                DropDownList ddlManagement = (DropDownList)GVCluster.Rows[i].FindControl("ddlManagement");
                Label lblDISECode = (Label)GVCluster.Rows[i].FindControl("lblDISECode");
                DropDownList ddlGKP = (DropDownList)GVCluster.Rows[i].FindControl("ddlGKP");
                DropDownList ddlGKPLevel = (DropDownList)GVCluster.Rows[i].FindControl("ddlGKPLevel");
                DropDownList ddlSchoolType = (DropDownList)GVCluster.Rows[i].FindControl("ddlSchoolType");
                DropDownList ddlBalsabha = (DropDownList)GVCluster.Rows[i].FindControl("ddlBalsabha");
                DropDownList ddlSchoolCampus = (DropDownList)GVCluster.Rows[i].FindControl("ddlSchoolCampus");

                TextBox txtTeacher = (TextBox)GVCluster.Rows[i].FindControl("txtTeacher");
                TextBox txtTeacherMobile = (TextBox)GVCluster.Rows[i].FindControl("txtTeacherMobile");
                TextBox txtTeacherdesignation = (TextBox)GVCluster.Rows[i].FindControl("txtTeacherdesignation");
                ListBox ddlClass = (ListBox)GVCluster.Rows[i].FindControl("ddlClass");
                DropDownList ddlMainNew = (DropDownList)GVCluster.Rows[i].FindControl("ddlMainNew");
                ListBox ddlClassDo = (ListBox)GVCluster.Rows[i].FindControl("ddlClassDo");

                DropDownList ddlGKPPlus = (DropDownList)GVCluster.Rows[i].FindControl("ddlGKPPlus");
                DropDownList ddlKGG = (DropDownList)GVCluster.Rows[i].FindControl("ddlKGG");
                DataRow[] dr = dt.Select("DISECode='" + Convert.ToString(lblDISECode.Text) + "'");
                if (dr.Length > 0)
                {

                    dr[0]["WorkingStatus"] = ddlWorkingStatus.SelectedValue;
                    dr[0]["Management"] = ddlManagement.SelectedValue;
                    dr[0]["GKP"] = ddlGKP.SelectedValue;
                    dr[0]["GKPLevel"] = ddlGKPLevel.SelectedValue;
                    dr[0]["SchoolType"] = ddlSchoolType.SelectedValue;
                    dr[0]["BAlVal"] = ddlBalsabha.SelectedValue;

                    dr[0]["SchoolCampus"] = ddlSchoolCampus.SelectedValue;
                    dr[0]["TeacherName"] = txtTeacher.Text;
                    dr[0]["TeacherContactNo"] = txtTeacherMobile.Text;
                    dr[0]["Teacherdesignation"] = txtTeacherdesignation.Text;

                    string ClassCOde = "";
                    string ClassName = "";
                    foreach (System.Web.UI.WebControls.ListItem item in ddlClass.Items)
                    {
                        if (item.Selected)
                        {
                            ClassCOde += "" + item.Value + "" + ",";
                            ClassName += "" + item.Text + "" + ";";
                        }
                    }
                    if (ClassCOde.Length > 0)
                    {
                        ClassCOde = ClassCOde.Substring(0, ClassCOde.LastIndexOf(","));
                        ClassName = ClassName.Substring(0, ClassName.LastIndexOf(";"));
                    }


                    string ClassCOde1 = "";
                    string ClassName1 = "";
                    foreach (System.Web.UI.WebControls.ListItem item in ddlClassDo.Items)
                    {
                        if (item.Selected)
                        {
                            ClassCOde1 += "" + item.Value + "" + ",";
                            ClassName1 += "" + item.Text + "" + ";";
                        }
                    }
                    if (ClassCOde1.Length > 0)
                    {
                        ClassCOde1 = ClassCOde1.Substring(0, ClassCOde1.LastIndexOf(","));
                        ClassName1 = ClassName1.Substring(0, ClassName1.LastIndexOf(";"));
                    }
                    dr[0]["FunctionalStatus"] = "1";
                    if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
                    {
                        dr[0]["ClassID"] = ddlClass.SelectedValue;
                        dr[0]["GKPPlus"] = ddlGKPPlus.SelectedValue;
                        dr[0]["LSG"] = ddlKGG.SelectedValue;
                        dr[0]["DonorID"] = ClassCOde1;
                        dr[0]["School Donor Name"] = ClassName1;
                    }
                    else
                    {


                        dr[0]["ClassID"] = ClassCOde;
                        dr[0]["ClassIDName"] = ClassName;
                    }
                    dr[0]["FunctionalStatus"] = "9";
                }

            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 1 || Convert.ToInt32(ddlType.SelectedValue) == 3)
            {



                DropDownList ddlClusterCode = (DropDownList)GVCluster.Rows[i].FindControl("ddlClusterCode");
                DropDownList ddlVillageGeography = (DropDownList)GVCluster.Rows[i].FindControl("ddlVillageGeography");
                DropDownList ddlVillageOperational = (DropDownList)GVCluster.Rows[i].FindControl("ddlVillageOperational");
                DropDownList ddlCblVillage = (DropDownList)GVCluster.Rows[i].FindControl("ddlCblVillage");
                DropDownList ddlFunctionalStatus = (DropDownList)GVCluster.Rows[i].FindControl("ddlFunctionalStatus");
                DropDownList ddlAGP = (DropDownList)GVCluster.Rows[i].FindControl("ddlAGP");
                Label lblTempID = (Label)GVCluster.Rows[i].FindControl("lblTempID");
                TextBox txtPanchayatSamiti = (TextBox)GVCluster.Rows[i].FindControl("txtPanchayatSamiti");




                Label lblVillageCode = (Label)GVCluster.Rows[i].FindControl("lblTempVillageCode");

                DataRow[] dr = dt.Select("TempVillageCode='" + Convert.ToString(lblVillageCode.Text) + "'");
                if (dr.Length > 0)
                {

                    dr[0]["ClusterCode"] = ddlClusterCode.SelectedValue;
                    dr[0]["VillageGeography"] = ddlVillageGeography.SelectedValue;
                    dr[0]["VillageGeographyOperational"] = ddlVillageOperational.SelectedValue;


                    dr[0]["CBlVillage"] = ddlCblVillage.SelectedValue;
                    dr[0]["FunctionalStatus"] = ddlFunctionalStatus.SelectedValue;
                    dr[0]["AGPStatus"] = ddlAGP.SelectedValue;
                    dr[0]["TeacherContactNo"] = lblTempID.Text;
                    dr[0]["PanchayatSamiti"] = txtPanchayatSamiti.Text;

                    dr[0]["FunctionalStatus"] = "9";

                }

            }



        }
        Session["GridViewData"] = dt;

    }
    protected void ddlClusterCode_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;
        Label lblTempID = (Label)row1.FindControl("lblTempID");
        Label lblClusterCode = (Label)row1.FindControl("lblTempClusterCode");
        string strQry = "Select clustercode from tblAnualPlanClusterWiseDetail where   clustercode='" + lblClusterCode.Text.ToString() + "'";
        //DataTable dtcountCheck = objMain.LoadData(strQry);
        //if (dtcountCheck.Rows.Count > 0)
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('annual plan entry done if u change cluster annaul plan deleted')</script>", false);


        //}
        //else
        //{
        //    lblTempID.Text = "1";
        //}
        lblTempID.Text = "1";
    }
    protected void ddlVillageOperational_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        DropDownList ddlVillageOperational = (DropDownList)row1.FindControl("ddlVillageOperational");
        DropDownList ddlCblVillage = (DropDownList)row1.FindControl("ddlCblVillage");
        DropDownList ddlFunctionalStatus = (DropDownList)row1.FindControl("ddlFunctionalStatus");


        Label lblTempVillageCode = (Label)row1.FindControl("lblTempVillageCode");
        if (ddlVillageOperational.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlVillageOperational.SelectedValue) == 2)
            {
                string strQry = "Select * from mstschool  where  WorkingStatus=1  and Villagecode='" + lblTempVillageCode.Text.ToString() + "'  ";


                DataTable dtEGVillagecode = objMain.LoadData(strQry);
                if (dtEGVillagecode.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Mark Schools as Non-Operational School')</script>", false);
                    ddlVillageOperational.SelectedValue = "1";
                }

                //if (Convert.ToInt32(ddlCblVillage.SelectedValue) == 1)
                //{
                //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Mark  Non-CBL Village')</script>", false);
                //    ddlVillageOperational.SelectedValue = "1";
                //}
                //if (Convert.ToInt32(ddlFunctionalStatus.SelectedValue) == 1)
                //{
                //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Mark  Non Functional Village')</script>", false);
                //    ddlVillageOperational.SelectedValue = "1";
                //}
            }

        }
        else
        {
            ddlVillageOperational.SelectedValue = "1";
        }

    }


    protected void ddlFunctionalStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        DropDownList ddlVillageOperational = (DropDownList)row1.FindControl("ddlVillageOperational");
        DropDownList ddlFunctionalStatus = (DropDownList)row1.FindControl("ddlFunctionalStatus");


        if (ddlFunctionalStatus.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlVillageOperational.SelectedValue) == 2)
            {
                if (Convert.ToInt32(ddlFunctionalStatus.SelectedValue) == 1)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please mark village as Operational Village')</script>", false);
                    ddlFunctionalStatus.SelectedValue = "2";
                }

            }

        }
        else
        {
            ddlFunctionalStatus.SelectedValue = "1";
        }

    }
    protected void ddlBal1_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;
        DropDownList ddlGKP = (DropDownList)row1.FindControl("ddlGKP");
        DropDownList ddlKGG = (DropDownList)row1.FindControl("ddlKGG");
        DropDownList ddlManagement = (DropDownList)row1.FindControl("ddlManagement");
        if (Convert.ToInt32(ddlManagement.SelectedValue) == 2 || Convert.ToInt32(ddlManagement.SelectedValue) == 10 || Convert.ToInt32(ddlManagement.SelectedValue) == 3 || Convert.ToInt32(ddlManagement.SelectedValue) == 4)
        {
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select UPS and Secondary government schools')</script>", false);
            ddlKGG.SelectedValue = "0";
            return;
        }



    }
    protected void ddlBal_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;
        DropDownList ddlGKP = (DropDownList)row1.FindControl("ddlGKP");
        DropDownList ddlBalsabha = (DropDownList)row1.FindControl("ddlBalsabha");
        DropDownList ddlManagement = (DropDownList)row1.FindControl("ddlManagement");
        if (Convert.ToInt32(ddlManagement.SelectedValue) == 2 || Convert.ToInt32(ddlManagement.SelectedValue) == 10 || Convert.ToInt32(ddlManagement.SelectedValue) == 3 || Convert.ToInt32(ddlManagement.SelectedValue) == 4)
        {
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select UPS and Secondary government schools')</script>", false);
            ddlBalsabha.SelectedValue = "0";
            return;
        }

        //if (Convert.ToInt32(ddlManagement.SelectedValue) == 2)
        //{

        //}
        //else
        //{
        //    if (Convert.ToInt32(ddlBalsabha.SelectedValue) == 1)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select UPS  schools')</script>", false);
        //        ddlBalsabha.SelectedValue = "0";
        //        return;

        //    }

        //}

    }
    protected void ddlGKP1_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;
        DropDownList ddlGKP = (DropDownList)row1.FindControl("ddlGKP");
        DropDownList ddlGKPLevel = (DropDownList)row1.FindControl("ddlGKPLevel");
        DropDownList ddlManagement = (DropDownList)row1.FindControl("ddlManagement");
        if (Convert.ToInt32(ddlGKP.SelectedValue) == 3)
        {
            if (Convert.ToInt32(ddlManagement.SelectedValue) == 2 || Convert.ToInt32(ddlManagement.SelectedValue) == 3 || Convert.ToInt32(ddlManagement.SelectedValue) == 4)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select UPS and Secondary government schools')</script>", false);
                ddlGKP.SelectedValue = "0";
                return;
            }
        }

    }
    protected void ddlGKP_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;
        DropDownList ddlGKP = (DropDownList)row1.FindControl("ddlGKP");
        DropDownList ddlGKPLevel = (DropDownList)row1.FindControl("ddlGKPLevel");
        DropDownList ddlManagement = (DropDownList)row1.FindControl("ddlManagement");
        if (Convert.ToInt32(ddlGKP.SelectedValue) == 2)
        {
            if (ddlGKPLevel.SelectedIndex > 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Remove GKP Level')</script>", false);
                ddlGKP.SelectedValue = "1";
                return;
            }
        }
        if (Convert.ToInt32(ddlGKP.SelectedValue) == 1)
        {
            ddlGKPLevel.Enabled = true;
        }
        else
        {
            ddlGKPLevel.Enabled = false;
        }
        if (Convert.ToInt32(ddlGKP.SelectedValue) == 3)
        {
            if (Convert.ToInt32(ddlManagement.SelectedValue) == 2 || Convert.ToInt32(ddlManagement.SelectedValue) == 3 || Convert.ToInt32(ddlManagement.SelectedValue) == 4)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select UPS and Secondary government schools')</script>", false);
                ddlGKP.SelectedValue = "0";
                return;
            }
        }
        if (Convert.ToInt32(ddlGKP.SelectedValue) == 2)
        {
            if (Convert.ToInt32(ddlManagement.SelectedValue) == 1 || Convert.ToInt32(ddlManagement.SelectedValue) == 2 || Convert.ToInt32(ddlManagement.SelectedValue) == 3 || Convert.ToInt32(ddlManagement.SelectedValue) == 4)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select UPS and Secondary government schools')</script>", false);
                ddlGKP.SelectedValue = "0";
                return;
            }
        }
    }
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        DropDownList ddlManagement = (DropDownList)row1.FindControl("ddlManagement");
        DropDownList ddlGKP = (DropDownList)row1.FindControl("ddlGKP");
        DropDownList ddlBalsabha = (DropDownList)row1.FindControl("ddlBalsabha");

        DropDownList ddlWorkingStatus = (DropDownList)row1.FindControl("ddlWorkingStatus");
        ListBox ddlClass = (ListBox)row1.FindControl("ddlClass");
        Label lblClassID = (Label)row1.FindControl("lblClassID");

        DataSet dtclass = Session["dtClass"] as DataSet;
        ddlClass.Enabled = true;
        Label lblManagement = (Label)row1.FindControl("lblManagement");
        Label lblWorkingStatus = (Label)row1.FindControl("lblWorkingStatus");
        string[] meeting = lblClassID.Text.Split(',');
        DropDownList ddlKGG = (DropDownList)row1.FindControl("ddlKGG");
        DropDownList ddlGKPPlus = (DropDownList)row1.FindControl("ddlGKPPlus");
        if ((Convert.ToInt32(ddlManagement.SelectedValue) == 2 || Convert.ToInt32(ddlManagement.SelectedValue) == 3 || Convert.ToInt32(ddlManagement.SelectedValue) == 4 || Convert.ToInt32(ddlManagement.SelectedValue) == 10) && ddlWorkingStatus.SelectedValue == "1")
        {
            ddlBalsabha.Enabled = true;
            ddlKGG.Enabled = true;
        }
        else
        {
            ddlBalsabha.Enabled = false;
            ddlKGG.Enabled = false;
        }
        if (Convert.ToInt32(ddlManagement.SelectedValue) == 2)
        {

        }
        else
        {
            if (ddlBalsabha.SelectedIndex > 0)
            {

                if (Convert.ToInt32(ddlBalsabha.SelectedValue) == 1)
                {

                    ddlBalsabha.SelectedValue = "0";
                }

            }
            if (ddlKGG.SelectedIndex > 0)
            {

                if (Convert.ToInt32(ddlKGG.SelectedValue) == 1)
                {

                    ddlKGG.SelectedValue = "0";
                }

            }
        }
        if (Convert.ToInt32(ddlManagement.SelectedValue) == 2 || Convert.ToInt32(ddlManagement.SelectedValue) == 3 || Convert.ToInt32(ddlManagement.SelectedValue) == 4)
        {
        }
        else
        {
            if (ddlBalsabha.SelectedIndex > 0)
            {

                if (Convert.ToInt32(ddlBalsabha.SelectedValue) == 3)
                {
                    ddlBalsabha.SelectedValue = "0";
                }
            }

        }
        if (Convert.ToInt32(ddlGKP.SelectedValue) == 3 || Convert.ToInt32(ddlGKPPlus.SelectedValue) == 1)
        {
            if (Convert.ToInt32(ddlManagement.SelectedValue) == 2 || Convert.ToInt32(ddlManagement.SelectedValue) == 3 || Convert.ToInt32(ddlManagement.SelectedValue) == 4)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select UPS and Secondary government schools')</script>", false);
                ddlGKP.SelectedValue = "0";
                return;
            }
        }
        if (Convert.ToInt32(ddlGKP.SelectedValue) == 2)
        {
            if (Convert.ToInt32(ddlManagement.SelectedValue) == 1 || Convert.ToInt32(ddlManagement.SelectedValue) == 2 || Convert.ToInt32(ddlManagement.SelectedValue) == 3 || Convert.ToInt32(ddlManagement.SelectedValue) == 4)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select UPS and Secondary government schools')</script>", false);
                ddlGKP.SelectedValue = "0";
                return;
            }
        }
        if (Convert.ToInt32(ddlManagement.SelectedValue) == 1)
        {
            ddlClass.DataTextField = "Description";
            ddlClass.DataValueField = "LookupCode";
            ddlClass.DataSource = dtclass.Tables[0];
            ddlClass.DataBind();


        }
        else if (Convert.ToInt32(ddlManagement.SelectedValue) == 2)
        {
            ddlClass.DataTextField = "Description";
            ddlClass.DataValueField = "LookupCode";
            ddlClass.DataSource = dtclass.Tables[1];
            ddlClass.DataBind();

        }
        else if (Convert.ToInt32(ddlManagement.SelectedValue) == 3)
        {
            ddlClass.DataTextField = "Description";
            ddlClass.DataValueField = "LookupCode";
            ddlClass.DataSource = dtclass.Tables[2];
            ddlClass.DataBind();

        }
        else if (Convert.ToInt32(ddlManagement.SelectedValue) == 4)
        {
            ddlClass.DataTextField = "Description";
            ddlClass.DataValueField = "LookupCode";
            ddlClass.DataSource = dtclass.Tables[3];
            ddlClass.DataBind();

        }
        else if (Convert.ToInt32(ddlManagement.SelectedValue) == 10)
        {
            ddlClass.DataTextField = "Description";
            ddlClass.DataValueField = "LookupCode";
            ddlClass.DataSource = dtclass.Tables[4];
            ddlClass.DataBind();

        }
        else if (Convert.ToInt32(ddlManagement.SelectedValue) == 6)
        {
            ddlClass.DataTextField = "Description";
            ddlClass.DataValueField = "LookupCode";
            ddlClass.DataSource = dtclass.Tables[5];
            ddlClass.DataBind();

        }
        else if (Convert.ToInt32(ddlManagement.SelectedValue) == 7)
        {
            ddlClass.DataTextField = "Description";
            ddlClass.DataValueField = "LookupCode";
            ddlClass.DataSource = dtclass.Tables[6];
            ddlClass.DataBind();

        }
        else
        {
            ddlClass.Enabled = false;
            ddlClass.DataSource = null;
            ddlClass.DataBind();
        }


        //if (Convert.ToInt32(ddlManagement.SelectedValue) == Convert.ToInt32(lblManagement.Text))
        //{
        //    if (lblClassID.Text.Length > 0)
        //    {
        //        foreach (string s in meeting)
        //        {
        //            foreach (System.Web.UI.WebControls.ListItem item in ddlClass.Items)
        //            {
        //                if (item.Value == s)
        //                {
        //                    item.Selected = true;

        //                }
        //            }
        //        }
        //    }
        //}
    }

    protected void ddlWorkingStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        DropDownList ddlWorkingStatus = (DropDownList)row1.FindControl("ddlWorkingStatus");

        DropDownList ddlGKP = (DropDownList)row1.FindControl("ddlGKP");

        DropDownList ddlGKPLevel = (DropDownList)row1.FindControl("ddlGKPLevel");
        DropDownList ddlBalsabha = (DropDownList)row1.FindControl("ddlBalsabha");


        DropDownList ddlGKPPlus = (DropDownList)row1.FindControl("ddlGKPPlus");
        DropDownList ddlKGG = (DropDownList)row1.FindControl("ddlKGG");

        Label lblTempVillageCode = (Label)row1.FindControl("lblTempVillageCode");
        if (ddlWorkingStatus.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 2 || Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 3 || Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 4 || Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 5)
            {
                string strQry = "Select * from mst5Village  where  VillageGeographyOperational=1  and Villagecode='" + lblTempVillageCode.Text.ToString() + "'  ";


                //DataTable dtEGVillagecode = objMain.LoadData(strQry);
                //if (dtEGVillagecode.Rows.Count > 0)
                //{

                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please update VillageOperational')</script>", false);
                //    ddlWorkingStatus.SelectedValue = "1";
                //    return;
                //}
                //if (Convert.ToInt32(ddlBalsabha.SelectedValue) == 1)
                //{

                //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Mark School As Non-Balsabha School')</script>", false);
                //    ddlWorkingStatus.SelectedValue = "1";
                //    return;
                //}
                //if (Convert.ToInt32(ddlGKP.SelectedValue) == 1)
                //{

                //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Mark school as Non GKP School')</script>", false);
                //    ddlWorkingStatus.SelectedValue = "1";
                //    return;
                //}

                ddlGKP.Enabled = false;
                ddlGKPLevel.Enabled = false;
                ddlGKPLevel.Enabled = false;
                ddlBalsabha.Enabled = false;
                ddlGKPPlus.Enabled = false;
                ddlKGG.Enabled = false;
                //ddlGKP.SelectedIndex = 0;
                //ddlGKPLevel.SelectedIndex = 0;
                //ddlGKPLevel.SelectedIndex = 0;
            }
            else
            {
                string strQry = "Select * from mst5Village  where  VillageGeographyOperational=1  and Villagecode='" + lblTempVillageCode.Text.ToString() + "'  ";


                DataTable dtEGVillagecode = objMain.LoadData(strQry);
                if (dtEGVillagecode.Rows.Count > 0)
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please update VillageOperational')</script>", false);
                    ddlWorkingStatus.SelectedValue = "2";
                    return;
                }
                Label lblManagement = (Label)row1.FindControl("lblManagement");
                DropDownList ddlManagement = (DropDownList)row1.FindControl("ddlManagement");
                if ((Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 2 || (Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 5)) && Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 1)
                {

                    ddlKGG.Enabled = true;
                    ddlBalsabha.Enabled = true;
                }
                else
                {
                    ddlBalsabha.Enabled = false;
                    ddlKGG.Enabled = false;
                }
                ddlGKPLevel.Enabled = true;
                ddlGKPPlus.Enabled = true;
                ddlGKP.Enabled = true;
                ddlGKPLevel.Enabled = true;
            }

        }
        else
        {
            ddlGKP.Enabled = true;
            ddlGKPLevel.Enabled = true;
            ddlGKPLevel.Enabled = true;
            ddlWorkingStatus.SelectedValue = "1";
        }

    }
    public bool BindDLLDatatable(string dtname, DataTable dt, string fieldname, string Condition, string orberbyfield, string orderby, DropDownList ddl, string textData, string valData, string ZeroIndex)
    {
        bool status = false;
        string conditions = Condition == "" ? "" : " where " + Condition;
        string orberbyfields = orberbyfield == "" ? "" : " order by " + orberbyfield;
        string orderbys = orderby == "" ? "" : orderby;



        if (dt.Rows.Count > 0)
        {
            ddl.DataTextField = textData;
            ddl.DataValueField = valData;

            ddl.DataSource = dt;
            ddl.DataBind();
            status = true;
        }
        return status;

    }

    protected void GV_luster_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (Convert.ToInt32(ddlType.SelectedValue) == 2)
            {
                Label lblWorkingStatus = (Label)e.Row.FindControl("lblWorkingStatus");
                Label lblManagement = (Label)e.Row.FindControl("lblManagement");
                DropDownList ddlWorkingStatus = (DropDownList)e.Row.FindControl("ddlWorkingStatus");
                DropDownList ddlManagement = (DropDownList)e.Row.FindControl("ddlManagement");

                Label lblGKP = (Label)e.Row.FindControl("lblGKP");
                Label lblGKPLevel = (Label)e.Row.FindControl("lblGKPLevel");
                Label lblSchoolType = (Label)e.Row.FindControl("lblSchoolType");
                Label lblBAlVal = (Label)e.Row.FindControl("lblBAlVal");

                Label lblSchoolCampus = (Label)e.Row.FindControl("lblSchoolCampus");
                Label lblFunctionalStatus = (Label)e.Row.FindControl("lblFunctionalStatus");


                DropDownList ddlGKP = (DropDownList)e.Row.FindControl("ddlGKP");
                DropDownList ddlGKPLevel = (DropDownList)e.Row.FindControl("ddlGKPLevel");
                DropDownList ddlSchoolType = (DropDownList)e.Row.FindControl("ddlSchoolType");
                DropDownList ddlBalsabha = (DropDownList)e.Row.FindControl("ddlBalsabha");
                DropDownList ddlSchoolCampus = (DropDownList)e.Row.FindControl("ddlSchoolCampus");

                DropDownList ddlGKPPlus = (DropDownList)e.Row.FindControl("ddlGKPPlus");

                DropDownList ddlKGG = (DropDownList)e.Row.FindControl("ddlKGG");

                ListBox ddlClass = (ListBox)e.Row.FindControl("ddlClass");
                DropDownList ddlClassNew = (DropDownList)e.Row.FindControl("ddlMainNew");
                ListBox ddlClassDo = (ListBox)e.Row.FindControl("ddlClassDo");

                Label lblClassID = (Label)e.Row.FindControl("lblClassID");
                Label lblLSG = (Label)e.Row.FindControl("lblLSG");
                Label lblGKPPlus = (Label)e.Row.FindControl("lblGKPPlus");
                DataSet dtclassNew = Session["dtClassNew"] as DataSet;
                Label lblDonorID = (Label)e.Row.FindControl("lblDonorID");

                BindDLLDatatable("mst5Village", dtclassNew.Tables[0], "ClassID, ClassName", conditions, "ClassName", "asc", ddlClassNew, "ClassName", "ClassID", "--Select--");

                ddlClassNew.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
                ListBox chkDonor = (ListBox)e.Row.FindControl("lstDonor");

                ddlClassDo.DataTextField = "DonorName";
                ddlClassDo.DataValueField = "DID";
                ddlClassDo.DataSource = dtclassNew.Tables[1];
                ddlClassDo.DataBind();



                if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
                {
                    ddlClassNew.Visible = true;
                    ddlClass.Visible = false;
                    ddlGKP.Items.FindByValue("3").Enabled = false;
                    ddlBalsabha.Items.FindByValue("3").Enabled = false;
                    ddlGKPPlus.SelectedValue = lblGKPPlus.Text;
                    ddlKGG.SelectedValue = lblLSG.Text;
                }
                else
                {

                    ddlClassNew.Visible = false;
                    ddlClass.Visible = true;
                    ddlBalsabha.Items.FindByValue("3").Enabled = true;
                    ddlGKP.Items.FindByValue("3").Enabled = true;
                }



                if ((lblManagement.Text == "2" || lblManagement.Text == "4" || lblManagement.Text == "3" || lblManagement.Text == "10") && lblWorkingStatus.Text == "1")
                {
                    ddlBalsabha.Enabled = true;
                    ddlKGG.Enabled = true;
                }
                else
                {
                    ddlBalsabha.Enabled = false;
                    ddlKGG.Enabled = false;
                }
                if (lblGKP.Text == "1")
                {
                    ddlGKPLevel.Enabled = true;
                }
                else
                {
                    ddlGKPLevel.Enabled = false;
                }
                if (lblWorkingStatus.Text == "1")
                {

                    ddlGKP.Enabled = true;
                    ddlGKPPlus.Enabled = true;


                }
                else
                {

                    ddlGKP.Enabled = false;
                    ddlGKPPlus.Enabled = false;

                }
                ddlGKP.SelectedValue = lblGKP.Text;
                ddlGKPLevel.SelectedValue = lblGKPLevel.Text;
                ddlSchoolType.SelectedValue = lblSchoolType.Text;
                ddlBalsabha.SelectedValue = lblBAlVal.Text;
                ddlWorkingStatus.SelectedValue = lblWorkingStatus.Text;
                ddlManagement.SelectedValue = lblManagement.Text;
                ddlSchoolCampus.SelectedValue = lblSchoolCampus.Text;
                lblFunctionalStatus.Text = "0";
                DataSet dtclass = Session["dtClass"] as DataSet;

                ddlClass.Enabled = true;
                string[] meeting = lblClassID.Text.Split(',');

                string[] meeting1 = lblDonorID.Text.Split(',');
                foreach (string s in meeting1)
                {
                    foreach (System.Web.UI.WebControls.ListItem item in ddlClassDo.Items)
                    {
                        if (item.Value == s)
                        {
                            item.Selected = true;

                        }
                    }
                }
                if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
                {
                    if (lblClassID.Text != "")
                    {
                        if (lblClassID.Text != "0")
                        {
                            ddlClassNew.SelectedValue = lblClassID.Text;
                        }
                        else
                        {
                            ddlClassNew.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        ddlClassNew.SelectedIndex = 0;
                    }
                }
                else
                {

                    if (lblManagement.Text == "1")
                    {
                        ddlClass.DataTextField = "Description";
                        ddlClass.DataValueField = "LookupCode";
                        ddlClass.DataSource = dtclass.Tables[0];
                        ddlClass.DataBind();

                        //foreach (System.Web.UI.WebControls.ListItem item in ddlClass.Items)
                        //{

                        //        item.Selected = true;

                        //}



                        foreach (string s in meeting)
                        {
                            foreach (System.Web.UI.WebControls.ListItem item in ddlClass.Items)
                            {
                                if (item.Value == s)
                                {
                                    item.Selected = true;

                                }
                            }
                        }

                    }
                    else if (lblManagement.Text == "2")
                    {
                        ddlClass.DataTextField = "Description";
                        ddlClass.DataValueField = "LookupCode";
                        ddlClass.DataSource = dtclass.Tables[1];
                        ddlClass.DataBind();

                        foreach (string s in meeting)
                        {
                            foreach (System.Web.UI.WebControls.ListItem item in ddlClass.Items)
                            {
                                if (item.Value == s)
                                {
                                    item.Selected = true;

                                }
                            }
                        }
                    }
                    else if (lblManagement.Text == "3")
                    {
                        ddlClass.DataTextField = "Description";
                        ddlClass.DataValueField = "LookupCode";
                        ddlClass.DataSource = dtclass.Tables[2];
                        ddlClass.DataBind();

                        foreach (string s in meeting)
                        {
                            foreach (System.Web.UI.WebControls.ListItem item in ddlClass.Items)
                            {
                                if (item.Value == s)
                                {
                                    item.Selected = true;

                                }
                            }
                        }

                    }
                    else if (lblManagement.Text == "4")
                    {
                        ddlClass.DataTextField = "Description";
                        ddlClass.DataValueField = "LookupCode";
                        ddlClass.DataSource = dtclass.Tables[3];
                        ddlClass.DataBind();

                        foreach (string s in meeting)
                        {
                            foreach (System.Web.UI.WebControls.ListItem item in ddlClass.Items)
                            {
                                if (item.Value == s)
                                {
                                    item.Selected = true;

                                }
                            }
                        }
                    }
                    else if (lblManagement.Text == "10")
                    {
                        ddlClass.DataTextField = "Description";
                        ddlClass.DataValueField = "LookupCode";
                        ddlClass.DataSource = dtclass.Tables[4];
                        ddlClass.DataBind();

                        foreach (string s in meeting)
                        {
                            foreach (System.Web.UI.WebControls.ListItem item in ddlClass.Items)
                            {
                                if (item.Value == s)
                                {
                                    item.Selected = true;

                                }
                            }
                        }
                    }
                    else if (lblManagement.Text == "6")
                    {
                        ddlClass.DataTextField = "Description";
                        ddlClass.DataValueField = "LookupCode";
                        ddlClass.DataSource = dtclass.Tables[5];
                        ddlClass.DataBind();

                        foreach (string s in meeting)
                        {
                            foreach (System.Web.UI.WebControls.ListItem item in ddlClass.Items)
                            {
                                if (item.Value == s)
                                {
                                    item.Selected = true;

                                }
                            }
                        }
                    }
                    else if (lblManagement.Text == "7")
                    {
                        ddlClass.DataTextField = "Description";
                        ddlClass.DataValueField = "LookupCode";
                        ddlClass.DataSource = dtclass.Tables[6];
                        ddlClass.DataBind();

                        foreach (string s in meeting)
                        {
                            foreach (System.Web.UI.WebControls.ListItem item in ddlClass.Items)
                            {
                                if (item.Value == s)
                                {
                                    item.Selected = true;

                                }
                            }
                        }
                    }
                    else
                    {
                        ddlClass.Enabled = false;
                        ddlClass.DataSource = null;
                        ddlClass.DataBind();
                    }
                }
            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 1 || Convert.ToInt32(ddlType.SelectedValue) == 3)
            {
                Label lblBlockCode = (Label)e.Row.FindControl("lblTempBlockCode");
                Label lblClusterCode = (Label)e.Row.FindControl("lblTempClusterCode");
                Label lblVillageCode = (Label)e.Row.FindControl("lblTempVillageCode");

                Label lblVillageGeography = (Label)e.Row.FindControl("lblVillageGeography");
                Label lblVillageGeographyOperational = (Label)e.Row.FindControl("lblVillageGeographyOperational");

                Label lblCBlVillage = (Label)e.Row.FindControl("lblCBlVillage");
                Label lblFunctionalStatus = (Label)e.Row.FindControl("lblFunctionalStatus");
                Label lblAGPStatus = (Label)e.Row.FindControl("lblAGPStatus");

                DropDownList ddlClusterCode = (DropDownList)e.Row.FindControl("ddlClusterCode");
                DropDownList ddlVillageGeography = (DropDownList)e.Row.FindControl("ddlVillageGeography");
                DropDownList ddlVillageOperational = (DropDownList)e.Row.FindControl("ddlVillageOperational");




                DropDownList ddlCblVillage = (DropDownList)e.Row.FindControl("ddlCblVillage");
                DropDownList ddlFunctionalStatus = (DropDownList)e.Row.FindControl("ddlFunctionalStatus");
                DropDownList ddlAGP = (DropDownList)e.Row.FindControl("ddlAGP");


                DataTable dt = Session["DTcluster"] as DataTable;
                DataTable dtAddCluster = dt.Clone();
                DataRow drNew;
                DataRow[] dr = dt.Select("BlockCode='" + lblBlockCode.Text + "'");
                if (dr.Length > 0)
                {
                    foreach (DataRow row in dr)
                    {
                        //  DtOutDoor.Rows.Remove(row);
                        drNew = dtAddCluster.NewRow();
                        drNew["ClusterCode"] = row["ClusterCode"];
                        drNew["ClusterName"] = row["ClusterName"];

                        dtAddCluster.Rows.Add(drNew);
                    }
                }

                objComman.BindDLLDatatable("mst5Village", dtAddCluster, "ClusterCode, ClusterName", conditions, "ClusterName", "asc", ddlClusterCode, "ClusterName", "ClusterCode", "--Select--");
                dtAddCluster = null;
                if (lblClusterCode.Text.Length > 1)
                {

                    ddlClusterCode.SelectedValue = lblClusterCode.Text;
                }
                if (lblVillageCode.Text == lblClusterCode.Text)
                {
                    ddlClusterCode.Enabled = false;
                }
                ddlVillageGeography.SelectedValue = lblVillageGeography.Text;
                ddlVillageOperational.SelectedValue = lblVillageGeographyOperational.Text;
                ddlCblVillage.SelectedValue = lblCBlVillage.Text;
                ddlFunctionalStatus.SelectedValue = lblFunctionalStatus.Text;
                ddlAGP.SelectedValue = lblAGPStatus.Text;
                lblFunctionalStatus.Text = "0";
            }

            //ImgBut1.Enabled = false;
            //ImgAcc1.Enabled = false;

            //ImageButton lnk = e.Row.FindControl("ImgAccExcel") as ImageButton;
            //AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
            //trigger.ControlID = lnk.UniqueID;
            //trigger.EventName = "Click";
            //ml121.Triggers.Add(trigger);
            //mainpnl121.Triggers.Add(trigger);

        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
        }
    }
}

