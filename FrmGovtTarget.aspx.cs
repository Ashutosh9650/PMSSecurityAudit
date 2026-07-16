using Ionic.Zip;
using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
public partial class FrmGovtTarget : System.Web.UI.Page
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
            FillMonth();
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
    public DataTable CreateDataTable()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYear.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYear;
    }
    public void FillMonth()
    {
        DataTable dtYear = CreateDataTable();
        DataRow dr;
        dr = dtYear.NewRow();
        dr["Type"] = "Jan";
        dr["ID"] = 01;
        dtYear.Rows.Add(dr);
        dr = dtYear.NewRow();
        dr["Type"] = "Feb";
        dr["ID"] = 02;
        dtYear.Rows.Add(dr);
        dr = dtYear.NewRow();
        dr["Type"] = "Mar";
        dr["ID"] = 03;
        dtYear.Rows.Add(dr);
        dr = dtYear.NewRow();
        dr["Type"] = "Apr";
        dr["ID"] = 04;
        dtYear.Rows.Add(dr);

        dr = dtYear.NewRow();
        dr["Type"] = "May";
        dr["ID"] = 05;
        dtYear.Rows.Add(dr);

        dr = dtYear.NewRow();
        dr["Type"] = "Jun";
        dr["ID"] = 06;
        dtYear.Rows.Add(dr);

        dr = dtYear.NewRow();
        dr["Type"] = "Jul";
        dr["ID"] = 07;
        dtYear.Rows.Add(dr);

        dr = dtYear.NewRow();
        dr["Type"] = "Aug";
        dr["ID"] = 08;
        dtYear.Rows.Add(dr);

        dr = dtYear.NewRow();
        dr["Type"] = "Sep";
        dr["ID"] = 09;
        dtYear.Rows.Add(dr);

        dr = dtYear.NewRow();
        dr["Type"] = "Oct";
        dr["ID"] = 10;
        dtYear.Rows.Add(dr);

        dr = dtYear.NewRow();
        dr["Type"] = "Nov";
        dr["ID"] = 11;
        dtYear.Rows.Add(dr);

        dr = dtYear.NewRow();
        dr["Type"] = "Dec";
        dr["ID"] = 12;
        dtYear.Rows.Add(dr);


        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlMonth, "Type", "ID", "Select");

    }

    public void FillCBDist()
    {

        conditions = "";


        conditions = "StateCode ='" + ddlState.SelectedValue + "' and mst2District.FYear ='" + Session["FinYear"].ToString() + "'";


        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");



    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (ddlType.SelectedIndex > 0)
        {
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Type ')</script>", false);
            return;
        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            GenerateExcelData();
        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            GenerateExcelData2();
        }


    }
    protected void btnImport1_Click(object sender, EventArgs e)
    {
        GenerateExcelData1();
    }
    protected void btnCSV_Click(object sender, EventArgs e)
    {
    }
    public DataTable ReadCsvFile()
    {

        DataTable dtCsv = new DataTable();
        string Fulltext;
        if (FileUpload1.HasFile)
        {


            string sDirectory = Server.MapPath(Comman.GetImagePath("MouPath"));

            string excelPath = Server.MapPath(Comman.GetImagePath("MouPath") + "/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(excelPath);

            using (StreamReader sr = new StreamReader(excelPath))
            {
                while (!sr.EndOfStream)
                {
                    Fulltext = sr.ReadToEnd().ToString(); //read full file text  
                    string[] rows = Fulltext.Split('\n'); //split full file text into rows  
                    for (int i = 0; i < rows.Count() - 1; i++)
                    {
                        string[] rowValues = rows[i].Split(','); //split each row with comma to get individual values  
                        {
                            if (i == 0)
                            {
                                for (int j = 0; j < rowValues.Count(); j++)
                                {
                                    dtCsv.Columns.Add(rowValues[j].Replace("\r", "").Trim()); ; //add headers  
                                    if (rowValues[j] == "#Govt meeting conducted\r")
                                    {
                                        string gg = rowValues[j];
                                    }
                                }
                            }
                            else
                            {
                                DataRow dr = dtCsv.NewRow();
                                for (int k = 0; k < rowValues.Count(); k++)
                                {
                                    dr[k] = rowValues[k].ToString();
                                }
                                dtCsv.Rows.Add(dr); //add other rows  
                            }
                        }
                    }
                }
            }
        }
        return dtCsv;
    }

    private void GenerateExcelData2()
    {
        DataTable dtExcelData = new DataTable();

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
            if (ddlMonth.SelectedIndex > 0)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Month ')</script>", false);
                return;
            }
            //string sDirectory = Server.MapPath(Comman.GetImagePath("MouPath");

            //string excelPath = Server.MapPath(Comman.GetImagePath("MouPath") + "/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
            //FileUpload1.SaveAs(excelPath);

            //string conString = string.Empty;
            //string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            //switch (extension)
            //{
            //    case ".xls": //Excel 97-03
            //        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
            //        break;
            //    case ".xlsx": //Excel 07 or higher
            //        conString = ConfigurationManager.ConnectionStrings["Exl07Con"].ConnectionString;
            //        break;

            //}
            //conString = string.Format(conString, excelPath);
            //using (OleDbConnection excel_con = new OleDbConnection(conString))
            //{
            //    excel_con.Open();
            //    string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();

            //    //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.
            //    //dtExcelData.Columns.AddRange(new DataColumn[3] { new DataColumn("Id", typeof(int)),
            //    //new DataColumn("Name", typeof(string)),
            //    //new DataColumn("Salary", typeof(decimal)) });

            //    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
            //    {
            //        oda.Fill(dtExcelData);
            //    }
            //    excel_con.Close();


            //}

            DataTable dt = new DataTable();
            dt = ReadCsvFile();

            string str = "";




            dt.Columns.Add("Myear", System.Type.GetType("System.Int32"));

            dt.Columns.Add("Mmonth", System.Type.GetType("System.Int32"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Myear"] = 2024;
                dt.Rows[i]["Mmonth"] = ddlMonth.SelectedValue;
            }
            Boolean hhh = BulkCopyTbTrainingDeatilsTrakerNew(dt);

            if (hhh == true)
            {
                lbl_messages.Text = "Data Import Success " + dt.Rows.Count + " Record";
                ModalAlert.Show();
            }
            //DataSet RowAffected = new DataSet();
            //RowAffected = SP_Check_District_Excel_ImportCheck();



            //if (RowAffected.Tables[0].Rows.Count > 0)
            //{
            //    btnApprove.Visible = false;
            //    ExporttoExcel(RowAffected.Tables[0]);
            //}
            //else
            //{
            //    btnApprove.Visible = true;
            //    lbl_messages.Text = "Data Import Success..";
            //    ModalAlert.Show();
            //}


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
    private void GenerateExcelData1()
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
            if (ddlMonth.SelectedIndex > 0)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Month ')</script>", false);
                return;
            }
            string sDirectory = Server.MapPath(Comman.GetImagePath("MouPath"));

            bool res = false;

            string FilePath = sDirectory + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + FileUpload1.FileName;
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

            OleDbConnection con = null;
            con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=Excel 8.0;Persist Security Info=False;");
            con.Open();
            //DataTable dt2 = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,null);

            /*int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                cb.Items.Add(row["TABLE_NAME"].ToString());
                i++;
            }*/

            OleDbCommand ExcelCommand = new OleDbCommand(@"SELECT * FROM [Sheet1$]", con);
            OleDbDataAdapter ExcelAdapter = new OleDbDataAdapter(ExcelCommand);
            DataSet ExcelDataSet = new DataSet();
            ExcelAdapter.Fill(ExcelDataSet);
            con.Close();

            //oledbConn.Open();
            //OleDbCommand cmd = new OleDbCommand(); ;

            //DataSet ds = new DataSet();

            //// string Q = "SELECT Sno,StateName,StateCode,DistrictName,DistrictCode,BlockName,BlockCode,EGBlock,EGBlockCode,GramPanchyat,GP_CODE,ClusterName,ClusterCode,VillageName,VillageCode,SchoolName,GOVTDISECODE,DISECODE,Operational_NON_Operational,Management,SchoolType  FROM [JHALAWAR DATA$]";
            //string Q = "SELECT * FROM [Sheet1$]";
            //OleDbDataAdapter oleda = new OleDbDataAdapter(Q, oledbConn);
            //oleda.Fill(ds);
            DataTable dt = new DataTable();

            dt = ExcelDataSet.Tables[0];


            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + dt.Rows.Count + "')</script>", false);

            string str = "";



            dt.Columns.Add("Myear", System.Type.GetType("System.Int32"));

            dt.Columns.Add("Mmonth", System.Type.GetType("System.Int32"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Myear"] = 2021;
                dt.Rows[i]["Mmonth"] = ddlMonth.SelectedValue;
            }
            Boolean hhh = BulkCopyTbTrainingDeatilsTraker(dt);

            if (hhh == true)
            {
                lbl_messages.Text = "Data Import Success..";
                ModalAlert.Show();
            }
            //DataSet RowAffected = new DataSet();
            //RowAffected = SP_Check_District_Excel_ImportCheck();



            //if (RowAffected.Tables[0].Rows.Count > 0)
            //{
            //    btnApprove.Visible = false;
            //    ExporttoExcel(RowAffected.Tables[0]);
            //}
            //else
            //{
            //    btnApprove.Visible = true;
            //    lbl_messages.Text = "Data Import Success..";
            //    ModalAlert.Show();
            //}


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



            Boolean hhh = BulkCopyTbTrainingDeatils(dt);

            DataSet RowAffected = new DataSet();
            RowAffected = SP_Check_District_Excel_ImportCheck();



            if (RowAffected.Tables[0].Rows.Count > 0)
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
            // SqlBulkCopyColumnMapping mapping02 = new SqlBulkCopyColumnMapping("VillageCode", "VillageCode");
            //SqlBulkCopyColumnMapping mapping03 = new SqlBulkCopyColumnMapping("DistrictName", "DistrictName");
            //SqlBulkCopyColumnMapping mapping04 = new SqlBulkCopyColumnMapping("BlockName", "BlockName");
            //SqlBulkCopyColumnMapping mapping05 = new SqlBulkCopyColumnMapping("VillageName", "VillageName");
            //SqlBulkCopyColumnMapping mapping06 = new SqlBulkCopyColumnMapping("VillageCode", "VillageCode");


            SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlHelper.mainConnectionString);
            bulkCopy.BatchSize = 5000;
            bulkCopy.BulkCopyTimeout = 10000;
            bulkCopy.ColumnMappings.Add(mapping01);
            //  bulkCopy.ColumnMappings.Add(mapping02);
            //bulkCopy.ColumnMappings.Add(mapping03);
            //bulkCopy.ColumnMappings.Add(mapping04);
            //bulkCopy.ColumnMappings.Add(mapping05);
            //bulkCopy.ColumnMappings.Add(mapping06);

            bulkCopy.DestinationTableName = "tblDTDGovTargetTemp";
            bulkCopy.NotifyAfter = 5000;
            bulkCopy.WriteToServer(dt);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public Boolean BulkCopyTbTrainingDeatilsTrakerNew(DataTable dt)
    {
        try
        {

            SqlBulkCopyColumnMapping mapping01 = new SqlBulkCopyColumnMapping("State", "State");
            SqlBulkCopyColumnMapping mapping02 = new SqlBulkCopyColumnMapping("District", "District");
            SqlBulkCopyColumnMapping mapping03 = new SqlBulkCopyColumnMapping("Village Name", "Block");
            SqlBulkCopyColumnMapping mapping04 = new SqlBulkCopyColumnMapping("Village Name", "Village Name");
            SqlBulkCopyColumnMapping mapping05 = new SqlBulkCopyColumnMapping("Village Code", "Village Code");
            SqlBulkCopyColumnMapping mapping06 = new SqlBulkCopyColumnMapping("Myear", "Myear");
            SqlBulkCopyColumnMapping mapping07 = new SqlBulkCopyColumnMapping("Mmonth", "Mmonth");
            SqlBulkCopyColumnMapping mapping08 = new SqlBulkCopyColumnMapping("KGBV Enrolment Target", "KGBV Enrolment Target");

            SqlBulkCopyColumnMapping mapping09 = new SqlBulkCopyColumnMapping("#Enrolment form Submitted to KGBV School", "KGBVEnrolment form Submitted to KGBV School");
            SqlBulkCopyColumnMapping mapping10 = new SqlBulkCopyColumnMapping("#KGBV Enrolled Girls", "KGBV Enrolled Girls");
            SqlBulkCopyColumnMapping mapping11 = new SqlBulkCopyColumnMapping("#No.of Camps - Pragati/AGP", "Community1Peoples having Discussion on Phone Calls");
            SqlBulkCopyColumnMapping mapping12 = new SqlBulkCopyColumnMapping("# Unique Beneficiary in CBL Camp from Attendance - Pragati/AGP", "Community1Parents Contacted");
            SqlBulkCopyColumnMapping mapping13 = new SqlBulkCopyColumnMapping("School Readiness Kit - # Schools", "Community1Teachers Contacted");
            SqlBulkCopyColumnMapping mapping14 = new SqlBulkCopyColumnMapping("#Govt meeting conducted", "CommunityParents Contacted");


            SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlHelper.mainConnectionString);
            bulkCopy.BatchSize = 5000;
            bulkCopy.BulkCopyTimeout = 10000;
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
            bulkCopy.ColumnMappings.Add(mapping12);
            bulkCopy.ColumnMappings.Add(mapping13);
            bulkCopy.ColumnMappings.Add(mapping14);

            bulkCopy.DestinationTableName = "tblGovtDataForEG";
            bulkCopy.NotifyAfter = 5000000;
            bulkCopy.WriteToServer(dt);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public Boolean BulkCopyTbTrainingDeatilsTraker(DataTable dt)
    {
        try
        {

            SqlBulkCopyColumnMapping mapping01 = new SqlBulkCopyColumnMapping("State", "State");
            SqlBulkCopyColumnMapping mapping02 = new SqlBulkCopyColumnMapping("District", "District");
            SqlBulkCopyColumnMapping mapping03 = new SqlBulkCopyColumnMapping("Village Name", "Block");
            SqlBulkCopyColumnMapping mapping04 = new SqlBulkCopyColumnMapping("Village Name", "Village Name");
            SqlBulkCopyColumnMapping mapping05 = new SqlBulkCopyColumnMapping("Village Code", "Village Code");
            SqlBulkCopyColumnMapping mapping06 = new SqlBulkCopyColumnMapping("Myear", "Myear");
            SqlBulkCopyColumnMapping mapping07 = new SqlBulkCopyColumnMapping("Mmonth", "Mmonth");
            SqlBulkCopyColumnMapping mapping08 = new SqlBulkCopyColumnMapping("KGBV Enrolment Target", "KGBV Enrolment Target");

            SqlBulkCopyColumnMapping mapping09 = new SqlBulkCopyColumnMapping("#Enrolment form Submitted to KGBV School", "KGBVEnrolment form Submitted to KGBV School");
            SqlBulkCopyColumnMapping mapping10 = new SqlBulkCopyColumnMapping("#KGBV Enrolled Girls", "KGBV Enrolled Girls");
            SqlBulkCopyColumnMapping mapping11 = new SqlBulkCopyColumnMapping("#Peoples having Discussion on Phone Calls", "#No.of Camps - Pragati/AGP");
            SqlBulkCopyColumnMapping mapping12 = new SqlBulkCopyColumnMapping("#Parents Contacted", "# Unique Beneficiary in CBL Camp from Attendance - Pragati/AGP");
            SqlBulkCopyColumnMapping mapping13 = new SqlBulkCopyColumnMapping("#Teachers Contacted", "School Readiness Kit - # Schools");
            SqlBulkCopyColumnMapping mapping14 = new SqlBulkCopyColumnMapping("#Anganwari Workers Contacted", "# Govt meeting conducted");
            //SqlBulkCopyColumnMapping mapping24 = new SqlBulkCopyColumnMapping("#ANMs Contacted", "Community1ANMs Contacted");
            //SqlBulkCopyColumnMapping mapping15 = new SqlBulkCopyColumnMapping("#Village Influencers/PRI/SMC Members Contacted", "Community1Village Influencers/PRI/SMC Members Contacted");
            //SqlBulkCopyColumnMapping mapping16 = new SqlBulkCopyColumnMapping("Parents Made Aware of SSS", "Community1Parents Made Aware of SSS");
            //SqlBulkCopyColumnMapping mapping17 = new SqlBulkCopyColumnMapping("#Peoples having Discussion on Phone Calls1", "CommunityPeoples having Discussion on Phone Calls");
            //SqlBulkCopyColumnMapping mapping18 = new SqlBulkCopyColumnMapping("#Parents Contacted1", "CommunityParents Contacted");
            //SqlBulkCopyColumnMapping mapping19 = new SqlBulkCopyColumnMapping("#Teachers Contacted1", "CommunityTeachers Contacted");
            //SqlBulkCopyColumnMapping mapping20 = new SqlBulkCopyColumnMapping("#Anganwari Workers Contacted1", "CommunityAnganwari Workers Contacted");
            //SqlBulkCopyColumnMapping mapping21 = new SqlBulkCopyColumnMapping("#ANMs Contacted1", "CommunityANMs Contacted");
            //SqlBulkCopyColumnMapping mapping22 = new SqlBulkCopyColumnMapping("#Village Influencers/PRI/SMC Members Contacted1", "CommunityVillage Influencers/PRI/SMC Members Contacted");
            //SqlBulkCopyColumnMapping mapping23 = new SqlBulkCopyColumnMapping("Parents Made Aware of SSS1", "CommunityParents Made Aware of SSS");
            //SqlBulkCopyColumnMapping mapping25 = new SqlBulkCopyColumnMapping("Total Benefitted Household", "RashanTotal Benefitted Household");
            //SqlBulkCopyColumnMapping mapping26 = new SqlBulkCopyColumnMapping("#Schools with SMILE Activity", "LearningSchools with SMILE Activity");
            //SqlBulkCopyColumnMapping mapping27 = new SqlBulkCopyColumnMapping("#Parents Contacted on Phone Calls", "LearningParents Contacted on Phone Calls");
            //SqlBulkCopyColumnMapping mapping28 = new SqlBulkCopyColumnMapping("#Parents Support on Phone Calls", "LearningParents Support on Phone Calls");

            //SqlBulkCopyColumnMapping mapping29 = new SqlBulkCopyColumnMapping("#Girls Contacted", "LearningGirls Contacted");
            //SqlBulkCopyColumnMapping mapping30 = new SqlBulkCopyColumnMapping("#Boys Contacted", "LearningBoys Contacted");
            //SqlBulkCopyColumnMapping mapping31 = new SqlBulkCopyColumnMapping("#Children Contacted", "LearningChildren Contacted");
            //SqlBulkCopyColumnMapping mapping32 = new SqlBulkCopyColumnMapping("#Teachers Contacted2", "LearningTeachers Contacted");
            //SqlBulkCopyColumnMapping mapping33 = new SqlBulkCopyColumnMapping("#Teachers Supported", "LearningTeachers Supported");
            //SqlBulkCopyColumnMapping mapping34 = new SqlBulkCopyColumnMapping("#Number of Mentors (FC/TB) Supported in Digilap", "LearningNumber of Mentors (FC/TB) Supported in Digilap ");
            SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlHelper.mainConnectionString);
            bulkCopy.BatchSize = 5000;
            bulkCopy.BulkCopyTimeout = 10000;
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
            bulkCopy.ColumnMappings.Add(mapping12);
            bulkCopy.ColumnMappings.Add(mapping13);
            bulkCopy.ColumnMappings.Add(mapping14);
            // bulkCopy.ColumnMappings.Add(mapping15);
            // bulkCopy.ColumnMappings.Add(mapping16);
            // bulkCopy.ColumnMappings.Add(mapping17);
            // bulkCopy.ColumnMappings.Add(mapping18);
            // bulkCopy.ColumnMappings.Add(mapping19);
            // bulkCopy.ColumnMappings.Add(mapping20);
            // bulkCopy.ColumnMappings.Add(mapping21);
            // bulkCopy.ColumnMappings.Add(mapping22);
            // bulkCopy.ColumnMappings.Add(mapping23);
            // bulkCopy.ColumnMappings.Add(mapping24);
            // bulkCopy.ColumnMappings.Add(mapping25);
            // bulkCopy.ColumnMappings.Add(mapping26);
            // bulkCopy.ColumnMappings.Add(mapping27);
            // bulkCopy.ColumnMappings.Add(mapping28);
            // bulkCopy.ColumnMappings.Add(mapping29);
            // bulkCopy.ColumnMappings.Add(mapping30);
            // bulkCopy.ColumnMappings.Add(mapping31);
            // bulkCopy.ColumnMappings.Add(mapping32);
            // bulkCopy.ColumnMappings.Add(mapping33);
            //bulkCopy.ColumnMappings.Add(mapping34);
            bulkCopy.DestinationTableName = "tblGovtDataForEG";
            bulkCopy.NotifyAfter = 5000000;
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
            sqlcmd.CommandText = "GovUploadDateChech";
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
            sqlcmd.CommandText = "GovUploadDate";
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


    protected void LnktTrackerExport_Click(object sender, EventArgs e)
    {
        if (ddlMonth.SelectedIndex > 0)
        {
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Month ')</script>", false);
            return;
        }

        DateTime GivenDate = DateTime.Now;
        int GivenYear = GivenDate.Year;
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Year", "2021"),
         new SqlParameter("@month",ddlMonth.SelectedValue),

        };
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptLoadTrackerData]", cmdParameters);
        if (dt.Rows.Count > 0)
        {
            GenerateExcelNew2021(dt, "TrackerData");
        }
    }

    protected void LnktTrackerExpor6t_Click(object sender, EventArgs e)
    {
        if (ddlMonth.SelectedIndex > 0)
        {
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Month ')</script>", false);
            return;
        }

        DateTime GivenDate = DateTime.Now;
        int GivenYear = GivenDate.Year;
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Year", "2022"),
         new SqlParameter("@month",ddlMonth.SelectedValue),

        };
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptLoadTrackerData]", cmdParameters);
        if (dt.Rows.Count > 0)
        {
            ExportToCSVFile(dt, "TrackerData");
        }
    }

    private void GenerateExcelNewSUmmary(DataTable dt, string FIleName)
    {
        try
        {



            string Fullfilename = "" + FIleName + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";
            if (dt.Rows.Count > 0)
            {

                //sw.Clear();
                //sw.ClearContent();
                //sw.ClearHeaders();
                //sw.Buffer = true;
                //sw.ContentType = "application/ms-excel";
                //sw.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                string fileName = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + Fullfilename + "");

                StreamWriter sw = new StreamWriter(fileName, false);
                sw.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");

                //sw.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");
                //sw.Charset = "utf-8";
                //sw.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                sw.Write("<table  >");

                sw.Write("<tr>");
                sw.Write("<td colspan='35' ' style='text-align:Center;border:.2pt solid windowtext;'>Annual Plan District Summary </td>");
                sw.Write("</tr>");

                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

                sw.Write("<tr style='font-width:bold;'>");
                int columnscount = dt.Columns.Count;

                for (int j = 0; j < columnscount; j++)
                {
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> " + dt.Columns[j].ColumnName + "</th>");
                }

                sw.Write("</tr>");

                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    sw.Write("<tr>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");
                    }
                }



                sw.Write("</tr>");
                sw.Write("</table>");

                sw.Close();
                //HttpContext.Current.Response.Flush();
                //HttpContext.Current.Response.End();
                FileStream fs = null;//, fs2=null;
                try
                {
                    string path1 = Fullfilename;
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
            }
        }
        catch
        {

            throw;
        }


    }
    private void GenerateExcelNew2021(DataTable dt, string FIleName)
    {
        try
        {






            if (dt.Rows.Count > 0)
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
                HttpContext.Current.Response.Write("<td colspan='34'  style='text-align:Center;border:.2pt solid windowtext;'></td>");

                HttpContext.Current.Response.Write("</tr>");

                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                //HttpContext.Current.Response.Write("<th class='header' rowspan='3'  style='" + HeaderStyle + "  width:2%;'>Master</th>");
                //HttpContext.Current.Response.Write("<th class='header'  rowspan='3' style='" + HeaderStyle + "  width:2%;'>Planned Villages</th>");
                //HttpContext.Current.Response.Write("<th class='header'  rowspan='3' style='" + HeaderStyle + "  width:2%;'> TB Leading Session having Smart Phone (Plan)</th>");

                //  HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Camp Start Status</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='7' style='" + HeaderStyle + "  width:2%;'> Master</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> KGBV Enrolment</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='7' style='" + HeaderStyle + "  width:2%;'>Community Calling- Phase-1</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='7' style='" + HeaderStyle + "  width:2%;'>Community Calling- Phase-2</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> Rashan Distribution Data</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='9' style='" + HeaderStyle + "  width:2%;'>E Learning</th>");

                HttpContext.Current.Response.Write("</tr>");
                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";

                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                int columnscount = dt.Columns.Count;

                for (int j = 0; j < columnscount; j++)
                {
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> " + dt.Columns[j].ColumnName + "</th>");
                }

                HttpContext.Current.Response.Write("</tr>");





                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    HttpContext.Current.Response.Write("<tr>");


                    for (int c = 0; c < dt.Columns.Count; c++)
                    {

                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");

                    }

                }
                #region Row1



                #endregion


                HttpContext.Current.Response.Write("</tr>");


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
    protected void LnkExport_Click(object sender, EventArgs e)
    {

        conditions += "    and mst5Village.Fyear = '" + Session["FinYear"].ToString() + "' ";


        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " and mst5Village.DistrictCode ='" + ddlDistrict.SelectedValue + "' ";

        }
        DateTime GivenDate = DateTime.Now;
        int GivenYear = GivenDate.Year;
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Condition",conditions),
         new SqlParameter("@Fyear",GivenYear),

        };
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptGovTargetD2dDetials]", cmdParameters);
        if (dt.Rows.Count > 0)
        {
            ExportToCSVFile(dt, "EnrollmentTargetRawData");
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
        string filePath = Server.MapPath(Comman.GetImagePath("ExportPath") + "/GovtTarget_Formate.xlsx");
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
    }
    protected void btnNewImport1_Click(object sender, EventArgs e)
    {
        string filePath = Server.MapPath(Comman.GetImagePath("ExportPath") + "/GovernmentData.csv");
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
    }

    protected void btnNewImport2_Click(object sender, EventArgs e)
    {
        string filePath = Server.MapPath(Comman.GetImagePath("ExportPath") + "/GovernmentData.xlsx");
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