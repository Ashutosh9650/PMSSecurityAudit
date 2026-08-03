using Ionic.Zip;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmStafftrainingReport : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                LoadYear();
                LoadUserLeavel();
                ViewState["1"] = "ss";
                //if (Convert.ToString(Session["username"]) == "PMSAdmin")
                //{
                //    LinkButton3.Visible = true;
                //}
                //else
                //{
                //    LinkButton3.Visible = false;
                //}
                LinkButton3.Visible = false;
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }

        }

    }

    protected void LnkTeamBalikaTraining_OnClick(object sender, EventArgs e)
    {

        LoadMasterDataTeamBalikaTraining(3);

    }

    protected void LnkTeamBalikaTrainingAtt_OnClick(object sender, EventArgs e)
    {

        LoadMasterDataTeamBalikaTrainingAtt(3);

    }
    public void LoadMasterDataTeamBalikaTrainingAtt(int Flag)
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
        //foreach (ListItem item in chkBlock.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlBlock += "'" + item.Value + "'" + ",";


        //    }
        //}

        //if (ddlBlock.Length > 0)
        //{
        //    ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        //}



        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }



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
           new SqlParameter("@Con", conditions),
        };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptMasterTeamBailkTrainingattendence]", parm);

        dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Internal Trainer Name", "TB Name");

        ViewState["D2dUser"] = dt;


        if (dt.Rows.Count > 0)
        {
            ExportToCSVFile(dt, "Team Balika Attendance ");

        }


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
        //foreach (ListItem item in chkBlock.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlBlock += "'" + item.Value + "'" + ",";


        //    }
        //}

        //if (ddlBlock.Length > 0)
        //{
        //    ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        //}



        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }



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

        dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "TB Name", "Internal Trainer Name", "External Trainer Name", "External Trainer Email", "External Trainer Contact No.");

        ViewState["D2dUser"] = dt;


        if (dt.Rows.Count > 0)
        {
            ExportToCSVFile(dt, "Team Balika Training");

        }


    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        AlllStateCode();
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




        }
        else
        {
            foreach (ListItem item in ChkState.Items)
            {

                item.Selected = false;

            }
            chkDistrict.Items.Clear();

        }
    }

    public void LoadYear()
    {
        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (ViewState["1"].ToString() == "216")
        {
            DataTable dt = (DataTable)ViewState["dt"];
            Int32 iff = dt.Rows.Count;
            ExporttoExcel(GV_DynamicGrid, dt, " StafftrainingReport");
        }
        if (ViewState["1"].ToString() == "217")
        {
            DataTable dt = (DataTable)ViewState["dt"];
            ExporttoExcel(GV_DynamicGrid, dt, " StafftrainingSchedularReport");
        }
        if (ViewState["1"].ToString() == "227")
        {
            DataTable dt = (DataTable)ViewState["dt"];
            ExporttoExcelNew(GV_DynamicGrid, dt, " StaffTrainingSummary");
        }

    }



    protected void btnCSV_Click(object sender, EventArgs e)
    {

        if (ViewState["1"].ToString() == "216")
        {
            DataTable dt = (DataTable)Session["SIP"];
            //ExporttoExcel(gvD2d, dt, "D2D");
            ExportToCSVFile(dt, " StafftrainingReport");
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

    private void ExporttoExcelNew(GridView Gv, DataTable table, string FileName)
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
            HttpContext.Current.Response.Write("<TR>");
            for (int J = 0; J < 1; J++)
            {
                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 1)
                {
                    #region
                    for (int c = 0; c < table.Columns.Count; c++)
                    {
                        if (c == 0 || c == 1 || c == 2)
                        {
                            if (c == 2)
                            {
                                HttpContext.Current.Response.Write("<Td>");
                                HttpContext.Current.Response.Write("Total");
                                HttpContext.Current.Response.Write("</Td>");


                            }
                            else
                            {
                                HttpContext.Current.Response.Write("<Td>");
                                HttpContext.Current.Response.Write("");
                                HttpContext.Current.Response.Write("</Td>");
                            }
                        }
                        else
                        {
                            string Col = "[" + table.Columns[c].ColumnName + "]";
                            int sum = 0;
                            if (Convert.ToString(table.Rows[J][table.Columns[c].ColumnName]) == "")
                            {
                            }
                            else
                            {
                                sum = Convert.ToInt32(table.Compute("SUM(" + Col + ")", string.Empty));
                            }

                            HttpContext.Current.Response.Write("<Td>");
                            HttpContext.Current.Response.Write(sum);
                            HttpContext.Current.Response.Write("</Td>");

                        }
                    }
                    #endregion
                }

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 2)
                {
                    #region
                    for (int c = 0; c < table.Columns.Count; c++)
                    {
                        if (c == 0 || c == 1 || c == 2 || c == 3)
                        {
                            if (c == 3)
                            {
                                HttpContext.Current.Response.Write("<Td>");
                                HttpContext.Current.Response.Write("Total");
                                HttpContext.Current.Response.Write("</Td>");


                            }
                            else
                            {
                                HttpContext.Current.Response.Write("<Td>");
                                HttpContext.Current.Response.Write("");
                                HttpContext.Current.Response.Write("</Td>");
                            }
                        }
                        else
                        {
                            string Col = "[" + table.Columns[c].ColumnName + "]";
                            int sum = 0;
                            if (Convert.ToString(table.Rows[J][table.Columns[c].ColumnName]) == "")
                            {
                            }
                            else
                            {
                                sum = Convert.ToInt32(table.Compute("SUM(" + Col + ")", string.Empty));
                            }

                            HttpContext.Current.Response.Write("<Td>");
                            HttpContext.Current.Response.Write(sum);
                            HttpContext.Current.Response.Write("</Td>");

                        }
                    }
                    #endregion
                }
                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 3)
                {
                    #region
                    for (int c = 0; c < table.Columns.Count; c++)
                    {
                        if (c == 0 || c == 1 || c == 2 || c == 3 || c == 4)
                        {
                            if (c == 4)
                            {
                                HttpContext.Current.Response.Write("<Td>");
                                HttpContext.Current.Response.Write("Total");
                                HttpContext.Current.Response.Write("</Td>");


                            }
                            else
                            {
                                HttpContext.Current.Response.Write("<Td>");
                                HttpContext.Current.Response.Write("");
                                HttpContext.Current.Response.Write("</Td>");
                            }
                        }
                        else
                        {
                            string Col = "[" + table.Columns[c].ColumnName + "]";
                            int sum = 0;
                            if (Convert.ToString(table.Rows[J][table.Columns[c].ColumnName]) == "")
                            {
                            }
                            else
                            {
                                sum = Convert.ToInt32(table.Compute("SUM(" + Col + ")", string.Empty));
                            }

                            HttpContext.Current.Response.Write("<Td>");
                            HttpContext.Current.Response.Write(sum);
                            HttpContext.Current.Response.Write("</Td>");

                        }
                    }
                    #endregion
                }
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
    public DataTable CreateDataTable()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYear.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYear;
    }
    public void AlllStateCode()
    {

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
        AlllStateCode();
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {
            //DataTable dtState = objMain.LoadData(" SELECT StateCode,  dbo.TitleCase(upper(StateName)) as  StateName FROM [mst1State] union select  StateCode,  dbo.TitleCase(upper(StateName))  +' ('+ 'Spine' +')'  as  StateName  from [mstSpineState] order by Statecode  ");

            ////string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State   order by StateName   ";
            ////DataTable dtState = objMain.LoadData(strQry1);
            //ChkState.DataSource = dtState;
            //ChkState.DataTextField = "StateName";
            //ChkState.DataValueField = "StateCode";
            //ChkState.DataBind();

            ChkState.Enabled = true;
            chkDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            //DataTable dtState = objMain.LoadData(" SELECT StateCode,  dbo.TitleCase(upper(StateName)) as  StateName FROM [mst1State] union select  StateCode,  dbo.TitleCase(upper(StateName))  +' ('+ 'Spine' +')'  as  StateName  from [mstSpineState] order by Statecode  ");

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
            //DataTable dtState = objMain.LoadData(" SELECT StateCode,  dbo.TitleCase(upper(StateName)) as  StateName FROM [mst1State] where   " + conditions + " union select  StateCode,  dbo.TitleCase(upper(StateName))  +' ('+ 'Spine' +')'  as  StateName  from [mstSpineState] order by Statecode  ");

            ////objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            ////string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State  where   " + conditions + "  order by StateName   ";
            ////DataTable dtState = objMain.LoadData(strQry1);
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
            ddlState_SelectedIndexChanged(ddlState, null);
            //  conditions = "StateCode in(" + ddlState + ") and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
            //conditions = "UserName='" + Session["username"].ToString() + "' ";
            //string strQry1 = "  SELECT distinct mst2District.DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode  where   " + conditions + "  order by DistrictName   ";


            //// string strQry1 = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where " + conditions + "  order by DistrictName   ";


            //DataTable dtDistrict = objMain.LoadData(strQry1);
            //// objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            //chkDistrict.DataSource = dtDistrict;
            //chkDistrict.DataTextField = "DistrictName";
            //chkDistrict.DataValueField = "DistrictCode";
            //chkDistrict.DataBind();


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

            ddlState_SelectedIndexChanged(ddlState, null);
            //conditions = "StateCode in(" + ddlState + ") and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and Fyear= '" + ddlYear.SelectedItem.Text + "' ";
            ////  objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");
            //string strQry1 = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where " + conditions + "  order by DistrictName   ";
            //DataTable dtDistrict = objMain.LoadData(strQry1);
            //// objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            //chkDistrict.DataSource = dtDistrict;
            //chkDistrict.DataTextField = "DistrictName";
            //chkDistrict.DataValueField = "DistrictCode";
            //chkDistrict.DataBind();
            string strQry;



            //ddlDistrict.SelectedIndex = 1;
            foreach (ListItem item in chkDistrict.Items)
            {

                item.Selected = true;

            }

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

        string conditions1 = "StateCode in(" + ddlState + ") ";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "StateCode in(" + ddlState + ")  and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = " StateCode in(" + ddlState + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";
        }
        else
        {
            conditions = "StateCode in(" + ddlState + ") and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'  ";


        }
        DataTable dtTb = objMain.LoadData(" SELECT DistrictCode,  dbo.TitleCase(upper(DistrictName)) as  DistrictName FROM [mst2District] where  " + conditions + "  union select  DistrictCode,  dbo.TitleCase(upper(DistrictName))  +' ('+ 'Spine' +')'  as  DistrictName from mstSpineDistrict  where  " + conditions1 + "   order by DistrictName ");



        // objComman.BindDLLMasterTableVillage("mst2District", "DistrictCode,DistrictName", dtTb, conditions, "DistrictName", "asc", ddlDistrictSearch, "DistrictName", "DistrictCode", "Select");



        // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        chkDistrict.DataSource = dtTb;
        chkDistrict.DataTextField = "DistrictName";
        chkDistrict.DataValueField = "DistrictCode";
        chkDistrict.DataBind();



    }




    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
    }






    protected void AnnaualFCReport_Click(object sender, EventArgs e)
    {

        GV_DynamicGrid.Visible = true;

        ViewState["1"] = "216";
        LinkButton1.Visible = true;


        AnnaualFCReport(3);


    }
    protected void AnnaualddrtAtt_Click(object sender, EventArgs e)
    {

        GV_DynamicGrid.Visible = true;

        ViewState["1"] = "216";
        LinkButton1.Visible = true;


        AnnaualFCReportAtt(3);


    }
    protected void AnnaualFCReportNew_Click(object sender, EventArgs e)
    {


        GV_DynamicGrid.Visible = true;

        ViewState["1"] = "217";
        LinkButton1.Visible = true;


        AnnaualFCReportss(3);



    }

    protected void Annaualddrt_Click(object sender, EventArgs e)
    {
        if (ddlGrouping.SelectedIndex > 0)
        {
            GV_DynamicGrid.Visible = true;

            ViewState["1"] = "227";
            LinkButton1.Visible = true;


            AnnaualFCReportSummary(3);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Group ')</script>", false);
        }


    }

    public void AnnaualFCReportSummary(Int32 Flag)
    {
        conditions = "";


        string ddlDistrict = "";
        string ddlDistrictName = "";
        string StateName = "";
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
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                StateName += "'" + item.Text + "'" + ",";


            }
        }

        if (StateName.Length > 0)
        {
            StateName = StateName.Substring(0, StateName.LastIndexOf(","));
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
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrictName += "'" + item.Text + "'" + ",";


            }
        }

        if (ddlDistrictName.Length > 0)
        {
            ddlDistrictName = ddlDistrictName.Substring(0, ddlDistrictName.LastIndexOf(","));
        }



        conditions = "   ";
        conditions = " where 1 =1 ";
        string conditions1 = "  ";
        //if (Convert.ToInt32(ddlYear.SelectedValue) == 2018)
        //{
        //    if (ddlStatecode.Length > 0)
        //    {
        //        conditions = conditions + "  and  D.StateCode in(" + ddlStatecode + ") ";
        //    }
        //    //if (ddlDistrict.Length > 0)
        //    //{
        //    //    conditions = conditions + " and D.DistrictCode in(" + ddlDistrict + ") ";
        //    //}
        //}
        //if (Convert.ToInt32(ddlYear.SelectedValue) == 2019)
        //{

        //    if (ddlStatecode.Length > 0)
        //    {
        //        conditions = conditions + "  and  D.StateCode in(" + ddlStatecode + ") ";
        //    }
        //    //if (ddlDistrict.Length > 0)
        //    //{
        //    //    conditions = conditions + " and D.DistrictCode in(" + ddlDistrict + ") ";
        //    //}
        //}


        //if (ddlStatecode.Length > 0)
        //{
        //    if (ddlDistrict.Length > 0)
        //    {
        //        conditions = conditions + "  and D.StateCode in(8,9,23) ";
        //    }
        //    else
        //    {
        //        conditions = conditions + "  and D.StateCode in(" + ddlStatecode + ") ";
        //    }
        //}

        //  conditions = conditions + "  and D.StateCode in(8,9,23,999) ";
        //if (ddlDistrict.Length > 0)
        //{
        //    conditions = conditions + " and D.DistrictCode in(" + ddlDistrict + ") ";
        //}

        //if (ddlStatecode.Length > 0)
        //{
        //    if (ddlDistrict.Length > 0)
        //    {
        //        conditions1 = conditions1 + "  and  D.StateCode in(8,9,23)";
        //    }
        //    else
        //    {
        //        conditions1 = conditions1 + "  and  D.StateCode in(" + ddlStatecode + ") ";
        //    }
        //}
        conditions = conditions + "  and D.StateCode in(8,9,23,999) ";
        conditions1 = conditions1 + "  and  D.StateCode in(8,9,23,999)";
        //if (ddlDistrict.Length > 0)
        //{
        //    conditions1 = conditions1 + " and D.DistrictCode in(" + ddlDistrict + ") ";
        //}
        if (txtDate.Text != "" && txtTodate.Text == "")
        {
            DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
            conditions += " and tblStaffTrainingSchedue.[FromDate] >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
            conditions1 += " andt blStaffTrainingSchedue.[FromDate] >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
        }
        if (txtTodate.Text != "" && txtDate.Text == "")
        {
            DateTime Todate = Convert.ToDateTime(txtTodate.Text);
            conditions += " and tblStaffTrainingSchedue.[FromDate] <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";
            conditions1 += " and tblStaffTrainingSchedue.[FromDate] <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";

        }
        if (txtDate.Text != "" && txtTodate.Text != "")
        {
            DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
            DateTime Todate = Convert.ToDateTime(txtTodate.Text);
            conditions += " and tblStaffTrainingSchedue.[FromDate] >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' and tblStaffTrainingSchedue.[FromDate] <=  '" + Todate.ToString("yyyy-MM-dd") + "'";
            conditions += " and tblStaffTrainingSchedue.[FromDate] >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' and tblStaffTrainingSchedue.[FromDate] <=  '" + Todate.ToString("yyyy-MM-dd") + "'";

        }
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "    And tblStaffTrainingSchedue.FromDate >= '" + Year1[0] + "-04-01' and tblStaffTrainingSchedue.ToDate<='" + Year1[1] + "-03-31'";
            conditions1 += "    And tblStaffTrainingSchedue.FromDate >= '" + Year1[0] + "-04-01' and tblStaffTrainingSchedue.ToDate<='" + Year1[1] + "-03-31'";

        }
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),
            new SqlParameter("@Con1",conditions1),
         new SqlParameter("@Flag",ddlGrouping.SelectedValue),


        };
        DataTable dataTable = null;
        DataTable Dtnew = null;

        dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptStaffTopicSummary]", cmdParameters);

        if (ddlDistrict.Length > 0)
        {
            string expression = "[District Name] in(" + ddlDistrictName + ") ";
            var filteredDataRows = dataTable.Select(expression);

            var filteredDataTable = new DataTable();

            if (filteredDataRows.Length != 0)
                filteredDataTable = filteredDataRows.CopyToDataTable();

            Dtnew = filteredDataTable.Copy();
        }
        else if (ddlStatecode.Length > 0)
        {
            string expression = " [State Name] in(" + StateName + ") ";
            var filteredDataRows = dataTable.Select(expression);

            var filteredDataTable = new DataTable();

            if (filteredDataRows.Length != 0)
                filteredDataTable = filteredDataRows.CopyToDataTable();

            Dtnew = filteredDataTable.Copy();
        }
        else
        {
            Dtnew = dataTable.Copy();
        }
        ViewState["dt"] = Dtnew;

        if (Dtnew.Rows.Count > 0)
        {

            GV_DynamicGrid.DataSource = Dtnew;
            GV_DynamicGrid.DataBind();


        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }


    }



    public void AnnaualFCReport(Int32 Flag)
    {
        conditions = "";


        string ddlDistrict = "";
        string ddlDistrictName = "";
        string StateName = "";
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
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                StateName += "'" + item.Text + "'" + ",";


            }
        }

        if (StateName.Length > 0)
        {
            StateName = StateName.Substring(0, StateName.LastIndexOf(","));
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
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrictName += "'" + item.Text + "'" + ",";


            }
        }

        if (ddlDistrictName.Length > 0)
        {
            ddlDistrictName = ddlDistrictName.Substring(0, ddlDistrictName.LastIndexOf(","));
        }


        string conditions2 = "";
        conditions = "   ";
        conditions = " where 1 =1 ";
        conditions2 = " where 1 =1 ";

        string conditions1 = "  ";
        string conditions3 = "  ";
        //if (Convert.ToInt32(ddlYear.SelectedValue) == 2018)
        //{
        //    if (ddlStatecode.Length > 0)
        //    {
        //        conditions = conditions + "  and  D.StateCode in(" + ddlStatecode + ") ";
        //    }
        //    //if (ddlDistrict.Length > 0)
        //    //{
        //    //    conditions = conditions + " and D.DistrictCode in(" + ddlDistrict + ") ";
        //    //}
        //}
        //if (Convert.ToInt32(ddlYear.SelectedValue) == 2019)
        //{

        //    if (ddlStatecode.Length > 0)
        //    {
        //        conditions = conditions + "  and  D.StateCode in(" + ddlStatecode + ") ";
        //    }
        //    //if (ddlDistrict.Length > 0)
        //    //{
        //    //    conditions = conditions + " and D.DistrictCode in(" + ddlDistrict + ") ";
        //    //}
        //}


        //if (ddlStatecode.Length > 0)
        //{
        //    if (ddlDistrict.Length > 0)
        //    {
        //        conditions = conditions + "  and D.StateCode in(8,9,23) ";
        //    }
        //    else
        //    {
        //        conditions = conditions + "  and D.StateCode in(" + ddlStatecode + ") ";
        //    }
        //}

        conditions = conditions + "  and D.StateCode in('8','9','23','999','9A','9B','9C','9D') ";
        //if (ddlDistrict.Length > 0)
        //{
        //    conditions = conditions + " and D.DistrictCode in(" + ddlDistrict + ") ";
        //}

        //if (ddlStatecode.Length > 0)
        //{
        //    if (ddlDistrict.Length > 0)
        //    {
        //        conditions1 = conditions1 + "  and  D.StateCode in(8,9,23)";
        //    }
        //    else
        //    {
        //        conditions1 = conditions1 + "  and  D.StateCode in(" + ddlStatecode + ") ";
        //    }
        //}
        conditions1 = conditions1 + "  and  D.StateCode in('8','9','23','999','9A','9B','9C','9D')";
        //if (ddlDistrict.Length > 0)
        //{
        //    conditions1 = conditions1 + " and D.DistrictCode in(" + ddlDistrict + ") ";
        //}
        if (txtDate.Text != "" && txtTodate.Text == "")
        {
            DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
            conditions += " and tblStaffTrainingSchedue.[FromDate] >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
            conditions1 += " and tblStaffTrainingSchedue.[FromDate] >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";

            conditions2 += " and tblStaffScheduling.[FromDate] >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
            conditions3 += " andt tblStaffScheduling.[FromDate] >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
        }
        if (txtTodate.Text != "" && txtDate.Text == "")
        {
            DateTime Todate = Convert.ToDateTime(txtTodate.Text);
            conditions += " and tblStaffTrainingSchedue.[FromDate] <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";
            conditions1 += " and tblStaffTrainingSchedue.[FromDate] <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";
            conditions2 += " and tblStaffScheduling.[FromDate] <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";
            conditions3 += " and tblStaffScheduling.[FromDate] <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";

        }
        if (txtDate.Text != "" && txtTodate.Text != "")
        {
            DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
            DateTime Todate = Convert.ToDateTime(txtTodate.Text);
            conditions += " and tblStaffTrainingSchedue.[FromDate] >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' and tblStaffTrainingSchedue.[FromDate] <=  '" + Todate.ToString("yyyy-MM-dd") + "'";
            conditions += " and tblStaffTrainingSchedue.[FromDate] >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' and tblStaffTrainingSchedue.[FromDate] <=  '" + Todate.ToString("yyyy-MM-dd") + "'";

            conditions2 += " and tblStaffScheduling.[FromDate] >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' and tblStaffScheduling.[FromDate] <=  '" + Todate.ToString("yyyy-MM-dd") + "'";
            conditions3 += " and tblStaffScheduling.[FromDate] >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' and tblStaffScheduling.[FromDate] <=  '" + Todate.ToString("yyyy-MM-dd") + "'";

        }
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "    And tblStaffTrainingSchedue.FromDate >= '" + Year1[0] + "-04-01' and tblStaffTrainingSchedue.ToDate<='" + Year1[1] + "-03-31'";
            conditions1 += "    And tblStaffTrainingSchedue.FromDate >= '" + Year1[0] + "-04-01' and tblStaffTrainingSchedue.ToDate<='" + Year1[1] + "-03-31'";
            conditions2 += "    And tblStaffScheduling.FromDate >= '" + Year1[0] + "-04-01' and tblStaffScheduling.ToDate<='" + Year1[1] + "-03-31'";
            conditions3 += "    And tblStaffScheduling.FromDate >= '" + Year1[0] + "-04-01' and tblStaffScheduling.ToDate<='" + Year1[1] + "-03-31'";
        }
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),
         new SqlParameter("@Con1",conditions1),
                  new SqlParameter("@Con2",conditions2),
                           new SqlParameter("@Con3",conditions3),
            new SqlParameter("@Fyear",ddlYear.SelectedValue),


        };
        DataTable dataTable = null;
        DataTable Dtnew = null;

        dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptStaffTrainingReport2026]", cmdParameters);

        dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Emp Name", "Contact No", "Email", "Internal Trainer Name", "External Trainer Name",
        "External Trainer Email", "External Trainer Contact No.", "Employee Name", "ContactNo", "Trainers Name", "Username");

        if (ddlDistrict.Length > 0)
        {
            string expression = "[Emp DistrictName] in(" + ddlDistrictName + ") ";
            var filteredDataRows = dataTable.Select(expression);

            var filteredDataTable = new DataTable();

            if (filteredDataRows.Length != 0)
                filteredDataTable = filteredDataRows.CopyToDataTable();

            Dtnew = filteredDataTable.Copy();
        }
        else if (ddlStatecode.Length > 0)
        {
            string expression = "[Emp State Name] in(" + StateName + ") ";
            var filteredDataRows = dataTable.Select(expression);

            var filteredDataTable = new DataTable();

            if (filteredDataRows.Length != 0)
                filteredDataTable = filteredDataRows.CopyToDataTable();

            Dtnew = filteredDataTable.Copy();
        }
        else
        {
            Dtnew = dataTable.Copy();
        }
        ViewState["dt"] = Dtnew;

        if (Dtnew.Rows.Count > 0)
        {
            if (Dtnew.Rows.Count > 1000)
            {

                ExportToCSVFile(Dtnew, " Stafftraining Report");
            }
            else
            {
                GV_DynamicGrid.DataSource = Dtnew;
                GV_DynamicGrid.DataBind();
            }

            return;
        }
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
    }


    public void AnnaualFCReportAtt(Int32 Flag)
    {
        conditions = "";


        string ddlDistrict = "";
        string ddlDistrictName = "";
        string StateName = "";
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
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                StateName += "'" + item.Text + "'" + ",";


            }
        }

        if (StateName.Length > 0)
        {
            StateName = StateName.Substring(0, StateName.LastIndexOf(","));
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
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrictName += "'" + item.Text + "'" + ",";


            }
        }

        if (ddlDistrictName.Length > 0)
        {
            ddlDistrictName = ddlDistrictName.Substring(0, ddlDistrictName.LastIndexOf(","));
        }


        string conditions2 = "";
        conditions = "   ";
        conditions = " where 1 =1 ";
        conditions2 = " where 1 =1 ";

        string conditions1 = "  ";
        string conditions3 = "  ";
        //if (Convert.ToInt32(ddlYear.SelectedValue) == 2018)
        //{
        //    if (ddlStatecode.Length > 0)
        //    {
        //        conditions = conditions + "  and  D.StateCode in(" + ddlStatecode + ") ";
        //    }
        //    //if (ddlDistrict.Length > 0)
        //    //{
        //    //    conditions = conditions + " and D.DistrictCode in(" + ddlDistrict + ") ";
        //    //}
        //}
        //if (Convert.ToInt32(ddlYear.SelectedValue) == 2019)
        //{

        //    if (ddlStatecode.Length > 0)
        //    {
        //        conditions = conditions + "  and  D.StateCode in(" + ddlStatecode + ") ";
        //    }
        //    //if (ddlDistrict.Length > 0)
        //    //{
        //    //    conditions = conditions + " and D.DistrictCode in(" + ddlDistrict + ") ";
        //    //}
        //}


        //if (ddlStatecode.Length > 0)
        //{
        //    if (ddlDistrict.Length > 0)
        //    {
        //        conditions = conditions + "  and D.StateCode in(8,9,23) ";
        //    }
        //    else
        //    {
        //        conditions = conditions + "  and D.StateCode in(" + ddlStatecode + ") ";
        //    }
        //}

        conditions = conditions + "  and D.StateCode in('8','9','23','999','9A','9B','9C','9D') ";
        //if (ddlDistrict.Length > 0)
        //{
        //    conditions = conditions + " and D.DistrictCode in(" + ddlDistrict + ") ";
        //}

        //if (ddlStatecode.Length > 0)
        //{
        //    if (ddlDistrict.Length > 0)
        //    {
        //        conditions1 = conditions1 + "  and  D.StateCode in(8,9,23)";
        //    }
        //    else
        //    {
        //        conditions1 = conditions1 + "  and  D.StateCode in(" + ddlStatecode + ") ";
        //    }
        //}
        conditions1 = conditions1 + "  and  D.StateCode in('8','9','23','999','9A','9B','9C','9D')";
        //if (ddlDistrict.Length > 0)
        //{
        //    conditions1 = conditions1 + " and D.DistrictCode in(" + ddlDistrict + ") ";
        //}
        if (txtDate.Text != "" && txtTodate.Text == "")
        {
            DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
            conditions += " and tblStaffTrainingSchedue.[FromDate] >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
            conditions1 += " and tblStaffTrainingSchedue.[FromDate] >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";

            conditions2 += " and tblStaffScheduling.[FromDate] >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
            conditions3 += " andt tblStaffScheduling.[FromDate] >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
        }
        if (txtTodate.Text != "" && txtDate.Text == "")
        {
            DateTime Todate = Convert.ToDateTime(txtTodate.Text);
            conditions += " and tblStaffTrainingSchedue.[FromDate] <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";
            conditions1 += " and tblStaffTrainingSchedue.[FromDate] <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";
            conditions2 += " and tblStaffScheduling.[FromDate] <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";
            conditions3 += " and tblStaffScheduling.[FromDate] <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";

        }
        if (txtDate.Text != "" && txtTodate.Text != "")
        {
            DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
            DateTime Todate = Convert.ToDateTime(txtTodate.Text);
            conditions += " and tblStaffTrainingSchedue.[FromDate] >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' and tblStaffTrainingSchedue.[FromDate] <=  '" + Todate.ToString("yyyy-MM-dd") + "'";
            conditions += " and tblStaffTrainingSchedue.[FromDate] >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' and tblStaffTrainingSchedue.[FromDate] <=  '" + Todate.ToString("yyyy-MM-dd") + "'";

            conditions2 += " and tblStaffScheduling.[FromDate] >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' and tblStaffScheduling.[FromDate] <=  '" + Todate.ToString("yyyy-MM-dd") + "'";
            conditions3 += " and tblStaffScheduling.[FromDate] >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' and tblStaffScheduling.[FromDate] <=  '" + Todate.ToString("yyyy-MM-dd") + "'";

        }
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "    And tblStaffTrainingSchedue.FromDate >= '" + Year1[0] + "-04-01' and tblStaffTrainingSchedue.ToDate<='" + Year1[1] + "-03-31'";
            conditions1 += "    And tblStaffTrainingSchedue.FromDate >= '" + Year1[0] + "-04-01' and tblStaffTrainingSchedue.ToDate<='" + Year1[1] + "-03-31'";
            conditions2 += "    And tblStaffScheduling.FromDate >= '" + Year1[0] + "-04-01' and tblStaffScheduling.ToDate<='" + Year1[1] + "-03-31'";
            conditions3 += "    And tblStaffScheduling.FromDate >= '" + Year1[0] + "-04-01' and tblStaffScheduling.ToDate<='" + Year1[1] + "-03-31'";
        }
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
         //   new SqlParameter("@Con",conditions),
         //new SqlParameter("@Con1",conditions1),
                  new SqlParameter("@Con2",conditions2),
                           new SqlParameter("@Con3",conditions3),
            new SqlParameter("@Fyear",ddlYear.SelectedValue),


        };
        DataTable dataTable = null;
        DataTable Dtnew = null;

        dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptStaffTrainingReport2026Att]", cmdParameters);

        dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Emp Name", "Contact No", "Email", "Internal Trainer Name");

        if (ddlDistrict.Length > 0)
        {
            string expression = "[Emp DistrictName] in(" + ddlDistrictName + ") ";
            var filteredDataRows = dataTable.Select(expression);

            var filteredDataTable = new DataTable();

            if (filteredDataRows.Length != 0)
                filteredDataTable = filteredDataRows.CopyToDataTable();

            Dtnew = filteredDataTable.Copy();
        }
        else if (ddlStatecode.Length > 0)
        {
            string expression = "[Emp State Name] in(" + StateName + ") ";
            var filteredDataRows = dataTable.Select(expression);

            var filteredDataTable = new DataTable();

            if (filteredDataRows.Length != 0)
                filteredDataTable = filteredDataRows.CopyToDataTable();

            Dtnew = filteredDataTable.Copy();
        }
        else
        {
            Dtnew = dataTable.Copy();
        }
        ViewState["dt"] = Dtnew;

        if (Dtnew.Rows.Count > 0)
        {
            if (Dtnew.Rows.Count > 0)
            {

                ExportToCSVFile(Dtnew, " Staff Training Attendance");
            }
            else
            {
                GV_DynamicGrid.DataSource = Dtnew;
                GV_DynamicGrid.DataBind();
            }

            return;
        }
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();


    }



    public void AnnaualFCReportss(Int32 Flag)
    {
        conditions = "";


        string ddlDistrict = "";

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



        conditions = "   ";
        conditions = " where 1 =1 ";
        string conditions1 = "  ";
        conditions = conditions + "  and mst2District.StateCode in('8','9','23','999','9A','9B','9C','9D') ";
        //if (ddlDistrict.Length > 0)
        //{
        //    conditions = conditions + " and D.DistrictCode in(" + ddlDistrict + ") ";
        //}

        //if (ddlStatecode.Length > 0)
        //{
        //    if (ddlDistrict.Length > 0)
        //    {
        //        conditions1 = conditions1 + "  and  D.StateCode in(8,9,23)";
        //    }
        //    else
        //    {
        //        conditions1 = conditions1 + "  and  D.StateCode in(" + ddlStatecode + ") ";
        //    }
        //}
        conditions1 = conditions1 + "  and  mst2District.StateCode in('8','9','23','999','9A','9B','9C','9D')";


        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst2District.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst2District.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + "  and  mst2District.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst2District.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (txtDate.Text != "" && txtTodate.Text == "")
        {
            DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
            conditions += " and tblStaffScheduling.[FromDate] >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
            conditions1 += " andt tblStaffScheduling.[FromDate] >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
        }
        if (txtTodate.Text != "" && txtDate.Text == "")
        {
            DateTime Todate = Convert.ToDateTime(txtTodate.Text);
            conditions += " and tblStaffScheduling.[FromDate] <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";
            conditions1 += " and tblStaffScheduling.[FromDate] <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";

        }
        if (txtDate.Text != "" && txtTodate.Text != "")
        {
            DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
            DateTime Todate = Convert.ToDateTime(txtTodate.Text);
            conditions += " and tblStaffScheduling.[FromDate] >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' and tblStaffScheduling.[FromDate] <=  '" + Todate.ToString("yyyy-MM-dd") + "'";
            conditions += " and tblStaffScheduling.[FromDate] >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' and tblStaffScheduling.[FromDate] <=  '" + Todate.ToString("yyyy-MM-dd") + "'";

        }
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "    And tblStaffScheduling.FromDate >= '" + Year1[0] + "-04-01' and tblStaffScheduling.ToDate<='" + Year1[1] + "-03-31'";
            conditions1 += "    And tblStaffScheduling.FromDate >= '" + Year1[0] + "-04-01' and tblStaffScheduling.ToDate<='" + Year1[1] + "-03-31'";

        }
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),



        };
        DataTable dataTable = null;


        dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptStaffTrainingScheduling2026]", cmdParameters);

        ViewState["dt"] = dataTable;
        if (dataTable.Rows.Count > 0)
        {
            if (dataTable.Rows.Count > 1000)
            {

                ExportToCSVFile(dataTable, " StafftrainingSchedularReport");
            }
            else
            {
                GV_DynamicGrid.DataSource = dataTable;
                GV_DynamicGrid.DataBind();
            }

            return;
        }
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
    }
}