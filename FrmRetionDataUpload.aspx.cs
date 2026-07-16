using Ionic.Zip;
using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;


public partial class FrmRetionDataUpload : System.Web.UI.Page
{
    Comman obj = new Comman();
    clsMain Objcls = new clsMain();
    Comman objComman = new Comman();

    string conditions = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            AlllStateCode();
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
                            new SqlParameter("@Year","2024"),
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
                             new SqlParameter("@Year","2024"),
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
                       new SqlParameter("@Year","2024"),
                  };
            DataTable dtAllState = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptLoadAllState", par1);
            objComman.BindDLLDatatable("mst1State", dtAllState, "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");


        }

        ddlState.SelectedIndex = 2;
        ddlState_SelectedIndexChanged(ddlState, null);
    }
    public void FillCBState()
    {
        conditions = "";
        objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");

        ddlState.SelectedIndex = 2;
        ddlState_SelectedIndexChanged(ddlState, null);

    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
    }
    public void FillCBDist()
    {

        conditions = "";


        conditions = "StateCode ='" + ddlState.SelectedValue + "' and mst2District.FYear ='2025-2026'";


        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");



    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        GenerateExcelData();
    }
    protected void btnCSV_Click(object sender, EventArgs e)
    {
    }
    private void GenerateExcelData()
    {
        OleDbConnection oledbConn = new OleDbConnection();
        try
        {
            // need to pass relative path after deploying on server
            string path = System.IO.Path.GetFullPath(Server.MapPath(FileUpload1.FileName));
            /* connection string  to work with excel file. HDR=Yes - indicates 
               that the first row contains columnnames, not data. HDR=No - indicates 
               the opposite. "IMEX=1;" tells the driver to always read "intermixed" 
               (numbers, dates, strings etc) data columns as text. 
            Note that this option might affect excel sheet write access negative. */
            string sDirectory = Server.MapPath(Comman.GetImagePath("MouPath"));

            bool res = false;
            string FilePath = sDirectory + FileUpload1.FileName;
            FileUpload1.PostedFile.SaveAs(FilePath);
            ViewState["FileName"] = FileUpload1.FileName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");

            // instance a memory stream and pass the

            if (Path.GetExtension(path) == ".xls")
            {

                oledbConn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=Excel 4.0;Persist Security Info=False;");
            }
            else if (Path.GetExtension(path) == ".xlsx")
            {

                oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=Excel 8.0;Persist Security Info=False;");
            }
            else
            {

            }

            oledbConn.Open();
            OleDbCommand cmd = new OleDbCommand(); ;

            DataSet ds = new DataSet();

            // string Q = "SELECT Sno,StateName,StateCode,DistrictName,DistrictCode,BlockName,BlockCode,EGBlock,EGBlockCode,GramPanchyat,GP_CODE,ClusterName,ClusterCode,VillageName,VillageCode,SchoolName,GOVTDISECODE,DISECODE,Operational_NON_Operational,Management,SchoolType  FROM [JHALAWAR DATA$]";
            string Q = "SELECT * FROM [Sheet1$]";
            OleDbDataAdapter oleda = new OleDbDataAdapter(Q, oledbConn);
            oleda.Fill(ds);


            DataTable dt = ds.Tables[0];


            string str = "";

            str = " tblRetaionDataTemp ";

            Boolean hhh = BulkCopyTbTrainingDeatils(dt);

            DataSet RowAffected = new DataSet();
            RowAffected = SP_Check_District_Excel_ImportCheck();



            if (RowAffected.Tables[0].Rows.Count > 3)
            {
                btnApprove.Visible = false;
                ExporttoExcel(RowAffected.Tables[0]);
            }
            else
            {
                btnApprove.Visible = true;
                lbl_messages.Text = "Data Import Success..";
                ModalAlert.Show();
            }


        }
        // need to catch possible exceptions
        catch (Exception ex)
        {
            lbl_messages.Text = ex.ToString();
            ModalAlert.Show();

        }
        finally
        {
            oledbConn.Close();
        }
    }
    public Boolean BulkCopyTbTrainingDeatils(DataTable dt)
    {
        try
        {

            SqlBulkCopyColumnMapping mapping01 = new SqlBulkCopyColumnMapping("UniqueID", "UniqueID");
            SqlBulkCopyColumnMapping mapping02 = new SqlBulkCopyColumnMapping("ActiveStatus", "ActiveStatus");
            //SqlBulkCopyColumnMapping mapping03 = new SqlBulkCopyColumnMapping("DistrictName", "DistrictName");
            //SqlBulkCopyColumnMapping mapping04 = new SqlBulkCopyColumnMapping("BlockName", "BlockName");
            //SqlBulkCopyColumnMapping mapping05 = new SqlBulkCopyColumnMapping("VillageName", "VillageName");
            //SqlBulkCopyColumnMapping mapping06 = new SqlBulkCopyColumnMapping("VillageCode", "VillageCode");


            SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlHelper.mainConnectionString);
            bulkCopy.BatchSize = 5000;
            bulkCopy.BulkCopyTimeout = 10000;
            bulkCopy.ColumnMappings.Add(mapping01);
            bulkCopy.ColumnMappings.Add(mapping02);
            //bulkCopy.ColumnMappings.Add(mapping03);
            //bulkCopy.ColumnMappings.Add(mapping04);
            //bulkCopy.ColumnMappings.Add(mapping05);
            //bulkCopy.ColumnMappings.Add(mapping06);

            bulkCopy.DestinationTableName = "tblRetaionDataTemp";
            bulkCopy.NotifyAfter = 5000;
            bulkCopy.WriteToServer(dt);
            return true;
        }
        catch
        {
            return false;
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

            throw e;
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

            throw e;
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

            DataSet RowAffected = new DataSet();
            RowAffected = SP_Check_District_Excel_Import();



            if (RowAffected.Tables[0].Rows.Count > 0)
            {
                ExporttoExcel(RowAffected.Tables[0]);
            }
            else
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
    public DataSet SP_Check_District_Excel_ImportCheck()
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
            sqlcmd.CommandText = "GovUploadDateChechRetention";
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

            throw e;
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
            sqlcmd.CommandText = "RetaionUploadDate";
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

            throw e;
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

        conditions += "     mst5Village.Fyear = '2025-2026' ";

        if (ddlState.SelectedIndex > 0)
        {
            conditions += " and mst5Village.Statecode ='" + ddlState.SelectedValue + "' ";

        }

        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " and mst5Village.DistrictCode ='" + ddlDistrict.SelectedValue + "' ";

        }
        DateTime GivenDate = DateTime.Now;
        int GivenYear = GivenDate.Year;
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion",conditions),


        };
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportRetentionDeatilsNew]", cmdParameters);
        if (dt.Rows.Count > 0)
        {
            ExportToCSVFile(dt, "RetentionTargetRawData");
        }
    }
    private void ExportToCSVFile(DataTable dtTable, string filePath)
    {
        if (dtTable != null)
        {
            StringBuilder sbldr = new StringBuilder();
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

            //str.Write(sbldr.ToString());
            //Response.ContentType = "Application/x-msexcel";
            //Response.AddHeader("content-disposition", "attachment;filename=test.csv");
            //Response.Write(sbldr.ToString());
            //Response.End();
        }
    }
    protected void btnNewImport_Click(object sender, EventArgs e)
    {
        string filePath = Server.MapPath(Comman.GetImagePath("ExportPath") + "/" + "Retention_Formate.xlsx");
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
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