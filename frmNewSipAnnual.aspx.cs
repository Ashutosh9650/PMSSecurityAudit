using Ionic.Zip;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class frmSipAnnual : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();

    string conditions = "";
    string flag = "";
    Password objPass = new Password();
    public DataTable dtUserDeatils;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                LoadYear();
                LoadUserLeavel();
                ViewState["1"] = "ss";

            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }

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

    public void LoadYear()
    {
        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (ViewState["1"].ToString() == "2")
        {
            DataTable dt = (DataTable)Session["SIP"];
            ExporttoExcel(GV_DynamicGrid, dt, "SchoolRaw");
        }
        if (ViewState["1"].ToString() == "3")
        {
            DataTable dt = (DataTable)Session["SIP"];
            ExporttoExcel(GV_DynamicGrid, dt, "VillageRaw");
        }
        if (ViewState["1"].ToString() == "5")
        {
            DataTable dt = (DataTable)Session["D2DTarget"];
            ExporttoExcel(GV_DynamicGrid, dt, "D2DTarget");
        }

        if (ViewState["1"].ToString() == "4")
        {
            DataTable dt = (DataTable)Session["D2DAnual"];
            ExporttoExcel(gvD2d, dt, "D2D");
        }
        if (ViewState["1"].ToString() == "10")
        {
            DataTable dt = (DataTable)Session["TargetSummary"];
            //ExporttoExcel(gvD2d, dt, "D2D");
            ExporttoExcel(GV_DynamicGrid, dt, "TargetSummary");
        }
        if (ViewState["1"].ToString() == "11")
        {
            DataTable dt = (DataTable)Session["TargetD"];
            //ExporttoExcel(gvD2d, dt, "D2D");
            ExporttoExcel(GV_DynamicGrid, dt, "Detial");
        }
        if (ViewState["1"].ToString() == "223")
        {
            DataTable dt = (DataTable)Session["TargetD2dDropOut"];
            //ExporttoExcel(gvD2d, dt, "D2D");
            //   ExporttoExcel(gvD2dTatget, dt, "TartgetNeverenroll");
            GenerateExcel(dt, "TartgetNeverenroll", "NEVERENROLL");
        }
        if (ViewState["1"].ToString() == "224")
        {
            DataTable dt = (DataTable)Session["TargetD2dDropOut"];
            //ExporttoExcel(gvD2d, dt, "D2D");
            GenerateExcelDropOut(dt, "TartgetDropOut", "DROPOUT");
            //ExporttoExcel(gvD2dTatget, dt, "TartgetDropOut");
        }

        if (ViewState["1"].ToString() == "20")
        {
            LoadExecel();
        }
    }
    protected void btnCSV_Click(object sender, EventArgs e)
    {

        if (ViewState["1"].ToString() == "2")
        {
            DataTable dt = (DataTable)Session["SIP"];
            //ExporttoExcel(gvD2d, dt, "D2D");
            ExportToCSVFile(dt, "SchoolRaw");
        }
        if (ViewState["1"].ToString() == "3")
        {
            DataTable dt = (DataTable)Session["SIP"];
            //ExporttoExcel(gvD2d, dt, "D2D");
            ExportToCSVFile(dt, "VillageRaw");
        }
        if (ViewState["1"].ToString() == "5")
        {
            DataTable dt = (DataTable)Session["D2DTarget"];
            //ExporttoExcel(gvD2d, dt, "D2D");
            ExportToCSVFile(dt, "D2DTarget");
        }

        if (ViewState["1"].ToString() == "4")
        {
            DataTable dt = (DataTable)Session["D2DAnual"];
            //ExporttoExcel(gvD2d, dt, "D2D");
            ExportToCSVFile(dt, "D2D");
        }
        if (ViewState["1"].ToString() == "10")
        {
            DataTable dt = (DataTable)Session["TargetSummary"];
            //ExporttoExcel(gvD2d, dt, "D2D");
            ExportToCSVFile(dt, "TargetSummary");
        }
        if (ViewState["1"].ToString() == "11")
        {
            DataTable dt = (DataTable)Session["TargetD"];
            //ExporttoExcel(gvD2d, dt, "D2D");
            ExportToCSVFile(dt, "Detial");
        }
        if (ViewState["1"].ToString() == "223")
        {
            DataTable dt = (DataTable)Session["TargetD2dDropOut"];
            //ExporttoExcel(gvD2d, dt, "D2D");
            ExportToCSVFile(dt, "TartgetNeverenroll");

        }
        if (ViewState["1"].ToString() == "224")
        {
            DataTable dt = (DataTable)Session["TargetD2dDropOut"];
            ExportToCSVFile(dt, "TartgetDropOut");

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
            conditions = "StateCode  in(" + ddlState + ") and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";


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
            strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions + "  and  Fyear='" + ddlYear.SelectedItem.Text + "'  order by DistrictName  ";
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


        ddlPanchayat.Items.Clear();
        chkVillage.Items.Clear();
    }





    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkDistrict.Items.Clear();
        chkBlock.Items.Clear();
        FillCBDist();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBBock();
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBCluster();
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillage();
    }

    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSchool();
    }
    public void FillSchool()
    {
        conditions = "";
        // conditions = "VillageCode ='" + ddlVillage.SelectedValue + "' ";
        // objComman.BindDLL("mstSchool", "SchoolCode,Name", conditions, "Name", "asc", ddlSchool, "Name", "SchoolCode", "Select");


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


        ddlPanchayat.Items.Clear();
        chkVillage.Items.Clear();
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
    protected void rblBlockType_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlPanchayat.Items.Clear();
        chkVillage.Items.Clear();
        ddlDistrict_SelectedIndexChanged(chkBlock, null);
    }

    public void LoadData()
    {
        string strQry = "";
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

        conditions = "";

        conditions = "  mst5Village.Fyear='" + ddlYear.SelectedItem.Text + "'";

        if (ddlStatecode.Length > 0)
        {
            conditions += " and mst5Village.StateCode in(" + ddlStatecode + ")";
        }
        //if (ViewState["1"].ToString() == "5")
        //{

        //    if (ddlDistrict.Length > 0)
        //    {
        //        strQry = "  SELECT OldDistrictCode from mst2District where DistrictCode in(" + ddlDistrict.ToString() + ")";
        //        DataTable dt = objMain.LoadData(strQry);
        //        conditions = conditions + " and mst5Village.DistrictCode='" +dt.Rows[0]["OldDistrictCode"].ToString() + "'";
        //    }
        //}
        //else
        //{
        if (ddlDistrict.Length > 0)
        {

            conditions = conditions + "and mst5Village.DistrictCode in(" + ddlDistrict.ToString() + ")";

        }
        // }
        if (ddlBlock.Length > 0)
        {

            conditions = conditions + "and mst5Village.BlockCode in(" + ddlBlock.ToString() + ")";

        }



        if (ddlPhan.Length > 0)
        {
            conditions = conditions + " and mst5Village.PanchayatCode in(" + ddlPhan.ToString() + ") ";
        }
        if (ddlVillage.Length > 0)
        {

            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }

        //if (ddlSchool.SelectedIndex > 0)
        //{
        //    conditions = conditions + " and s.SchoolCode='" + ddlSchool.SelectedValue.ToString() + "'";
        //}

        //if ( ddlVillage.SelectedIndex > 0)
        //{
        //    conditions = conditions + "and mst5Village.VillageCode='" + ddlVillage.SelectedValue.ToString() + "'";
        //}

        if (ViewState["1"].ToString() == "2")
        {
            SqlParameter[] parm = new SqlParameter[]
            {
             new SqlParameter("@Condition",  conditions),


            };
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnuanalSipData]", parm);
            if (dt.Rows.Count > 0)
            {
                Session["SIP"] = dt;
                GV_DynamicGrid.DataSource = dt;
                GV_DynamicGrid.DataBind();
                lblTotalCount.Text = (dt.Rows.Count).ToString();
            }

            else
            {
                Session["SIP"] = null;
                GV_DynamicGrid.DataSource = null;
                GV_DynamicGrid.DataBind();
            }
        }

        if (ViewState["1"].ToString() == "3")
        {
            SqlParameter[] parm = new SqlParameter[]
            {
             new SqlParameter("@Condition",  conditions),
              new SqlParameter("@Fyear",  ddlYear.SelectedValue),


            };
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptVillageRawData]", parm);
            if (dt.Rows.Count > 0)
            {
                Session["SIP"] = dt;
                lblTotalCount.Text = (dt.Rows.Count).ToString();
                if (dt.Rows.Count > 5000)
                {
                    btnCSV_Click(LinkButton3, null);
                }
                else
                {
                    GV_DynamicGrid.DataSource = dt;
                    GV_DynamicGrid.DataBind();
                }

            }

            else
            {
                Session["SIP"] = null;
                GV_DynamicGrid.DataSource = null;
                GV_DynamicGrid.DataBind();
            }
        }

        if (ViewState["1"].ToString() == "11")
        {
            SqlParameter[] parm = new SqlParameter[]
            {
             new SqlParameter("@Condition",  conditions),
              new SqlParameter("@Fyear",  ddlYear.SelectedValue),


            };
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTargetD2dDetials]", parm);
            if (dt.Rows.Count > 0)
            {
                Session["TargetD"] = dt;
                lblTotalCount.Text = (dt.Rows.Count).ToString();

                btnCSV_Click(LinkButton3, null);


            }


        }
        if (ViewState["1"].ToString() == "5")
        {


            SqlParameter[] parm = new SqlParameter[]
            {
             new SqlParameter("@Condition",  conditions),
                 new SqlParameter("@Fyear", ddlYear.SelectedValue),


            };
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTargetD2d]", parm);
            if (dt.Rows.Count > 0)
            {
                lblTotalCount.Text = (dt.Rows.Count).ToString();
                Session["D2DTarget"] = dt;
                if (dt.Rows.Count > 5000)
                {
                    btnCSV_Click(LinkButton3, null);
                }
                else
                {
                    GV_DynamicGrid.DataSource = dt;
                    GV_DynamicGrid.DataBind();

                }
            }

            else
            {
                Session["D2DTarget"] = null;
                GV_DynamicGrid.DataSource = null;
                GV_DynamicGrid.DataBind();
            }
        }
        if (ViewState["1"].ToString() == "4")
        {
            DataTable dt = objMain.ReportD2dENrollmentStatusNew(conditions);
            if (dt.Rows.Count > 0)
            {
                lblTotalCount.Text = (dt.Rows.Count).ToString();
                Session["D2DAnual"] = dt;
                if (dt.Rows.Count > 5000)
                {
                    btnCSV_Click(LinkButton3, null);
                }
                else
                {
                    gvD2d.DataSource = dt;
                    gvD2d.DataBind();
                }

            }

            else
            {
                Session["D2DAnual"] = null;
                gvD2d.DataSource = null;
                gvD2d.DataBind();
            }
        }
    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        gvD2d.DataSource = null;
        gvD2dTatget.Visible = false;
        gvD2d.DataBind();
        GV_DynamicGrid.Visible = true;
        gvD2d.Visible = false;
        lnkTarget.Visible = false;
        gvD2dTargetDropOut.Visible = false;
        ViewState["1"] = 2;
        LoadData();
    }
    protected void lnkTarget_Click(object sender, EventArgs e)
    {

        ViewState["1"] = 11;
        LoadData();
    }

    protected void btnVillageRaw_Click(object sender, EventArgs e)
    {
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        gvD2d.DataSource = null;
        lnkTarget.Visible = false;
        gvD2dTatget.Visible = false;
        gvD2d.DataBind();
        GV_DynamicGrid.Visible = true;
        gvD2dTargetDropOut.Visible = false;
        gvD2d.Visible = false;
        ViewState["1"] = 3;
        LoadData();
    }
    protected void btnD2d_Click(object sender, EventArgs e)
    {
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        gvD2d.DataSource = null;
        gvD2d.DataBind();
        GV_DynamicGrid.Visible = false;
        gvD2dTatget.Visible = false;
        lnkTarget.Visible = false;
        gvD2d.Visible = true;
        gvD2dTargetDropOut.Visible = false;
        ViewState["1"] = 4;
        LoadData();
    }
    protected void btnD2dTarget_Click(object sender, EventArgs e)
    {
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        gvD2d.DataSource = null;
        gvD2d.DataBind();
        GV_DynamicGrid.Visible = true;
        gvD2dTatget.Visible = false;
        gvD2d.Visible = false;
        ViewState["1"] = 5;
        lnkTarget.Visible = false;
        LoadData();
    }

    protected void btnD2dTargetSummary_Click(object sender, EventArgs e)
    {
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        gvD2dTatget.Visible = false;
        gvD2d.Visible = false;
        gvD2d.DataSource = null;
        gvD2d.DataBind();
        GV_DynamicGrid.Visible = true;
        gvD2d.Visible = false;
        ViewState["1"] = 10;
        lnkTarget.Visible = true;
        LoadDataLoadDataTarget(1);
    }
    protected void btnD2dTargetSummaryYear_Click(object sender, EventArgs e)
    {
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        gvD2d.DataSource = null;
        gvD2d.DataBind();
        GV_DynamicGrid.Visible = true;
        gvD2d.Visible = false;
        ViewState["1"] = 20;
        gvD2dTatget.Visible = false;
        gvD2d.Visible = false;
        lnkTarget.Visible = true;
        LoadDataLoadDataTarget(2);
    }

    protected void btnD2dTargetEnrolled_Click(object sender, EventArgs e)
    {
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        gvD2d.DataSource = null;
        gvD2d.DataBind();
        GV_DynamicGrid.Visible = false;
        gvD2d.Visible = false;
        ViewState["1"] = 223;
        lnkTarget.Visible = false;
        gvD2dTatget.Visible = true;
        gvD2d.Visible = false;
        string ddlVillage = "";

        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "" + "',";


            }
        }

        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlVillage.Length > 10)
        {
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Village')</script>", false);

            return;
        }
        LoadDataLoadDataTargetDroupOut(1);
    }
    protected void btnD2dTargetDrop_Click(object sender, EventArgs e)
    {
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        gvD2d.DataSource = null;
        gvD2d.DataBind();
        GV_DynamicGrid.Visible = false;
        gvD2d.Visible = false;
        ViewState["1"] = 224;
        lnkTarget.Visible = false;
        gvD2dTatget.Visible = true;
        gvD2d.Visible = false;
        string ddlVillage = "";

        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "" + "',";


            }
        }

        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlVillage.Length > 10)
        {
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Village')</script>", false);

            return;
        }
        LoadDataLoadDataTargetDroupOut(2);
    }
    public void LoadDataLoadDataTargetDroupOut(int type)
    {
        string strQry = "";
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

        conditions = "";

        conditions = "  mst5Village.Fyear='" + ddlYear.SelectedItem.Text + "'";

        if (ddlStatecode.Length > 0)
        {
            conditions += " and mst5Village.StateCode in(" + ddlStatecode + ")";
        }

        if (ddlDistrict.Length > 0)
        {

            conditions = conditions + "and mst5Village.DistrictCode in(" + ddlDistrict.ToString() + ")";

        }



        if (ddlBlock.Length > 0)
        {
            if (Convert.ToInt32(rblBlockType.SelectedValue) == 1)
            {
                conditions = conditions + "and mst5Village.BlockCode in(" + ddlBlock.ToString() + ")";

            }
            if (Convert.ToInt32(rblBlockType.SelectedValue) == 2)
            {
                conditions = conditions + "and mst5Village.MainBlockCode in(" + ddlBlock.ToString() + ")";
            }
        }

        if (ddlPhan.Length > 0)
        {
            conditions = conditions + " and mst5Village.PanchayatCode in(" + ddlPhan.ToString() + ") ";
        }
        if (ddlVillage.Length > 0)
        {

            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }
        if (ddlGender.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlGender.SelectedValue) == 2)
            {
                conditions += " and tbldtd.Gender =2";
            }
            if (Convert.ToInt32(ddlGender.SelectedValue) == 1)
            {
                conditions += " and tbldtd.Gender =1";
            }
        }

        if (type == 2)
        {
            gvD2dTatget.Visible = false;
            gvD2dTargetDropOut.Visible = true;
        }
        else
        {
            gvD2dTargetDropOut.Visible = false;
            gvD2dTatget.Visible = true;
        }
        SqlParameter[] parm = new SqlParameter[]
            {
             new SqlParameter("@Condition",  conditions),
                 new SqlParameter("@Fyear", ddlYear.SelectedValue),
         new SqlParameter("@Flag", type),


            };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTargetD2dDropOut]", parm);

        if (dt.Rows.Count > 0)
        {

            if (type == 2)
            {
                Session["TargetD2dDropOut"] = dt;
                gvD2dTargetDropOut.DataSource = dt;
                gvD2dTargetDropOut.DataBind();
                lblTotalCount.Text = (dt.Rows.Count).ToString();
            }
            else
            {
                Session["TargetD2dDropOut"] = dt;
                gvD2dTatget.DataSource = dt;
                gvD2dTatget.DataBind();
                lblTotalCount.Text = (dt.Rows.Count).ToString();
            }
        }
        else
        {
            Session["TargetSummary"] = null;
            gvD2dTatget.DataSource = null;
            gvD2dTatget.DataBind();
            gvD2dTargetDropOut.DataSource = null;
            gvD2dTargetDropOut.DataBind();
        }


    }

    private void GenerateExcel(DataTable dt, string FIleName, string Report)
    {
        try
        {



            string ddlVillage = "";

            foreach (ListItem item in chkVillage.Items)
            {
                if (item.Selected)
                {

                    ddlVillage += "'" + item.Value + "" + "',";


                }
            }

            if (ddlVillage.Length > 0)
            {
                ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
            }

            string strQry = "select DistrictName,BlockName,PanchayatName,ClusterName,Villagename from mst5Village inner join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode inner join mst3Block on mst3Block.BlockCode=mst5Village.BlockCode inner join mstPanchayat on mstPanchayat.PanchayatCode=mst5Village.PanchayatCode left join mstCluster on mstCluster.ClusterCode=mst5Village.ClusterCode where villagecode=" + ddlVillage + "  ";


            DataTable dtMaster = objMain.LoadData(strQry);

            DataRow[] result = dt.Select("Age >= 5 AND Age <=14");
            DataRow[] result1 = dt.Select("Age >= 7 AND Age <=14");
            string Gernder = "";
            if (Convert.ToInt32(ddlGender.SelectedValue) == 0)
            {
                Gernder = "All";
            }
            if (Convert.ToInt32(ddlGender.SelectedValue) == 2)
            {
                Gernder = ddlGender.SelectedItem.Text;
            }
            if (Convert.ToInt32(ddlGender.SelectedValue) == 1)
            {
                Gernder = ddlGender.SelectedItem.Text;
            }

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            string Fullfilename = "" + FIleName + "_" + Gernder + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");

            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            HttpContext.Current.Response.Write("<table  >");
            HttpContext.Current.Response.Write("<tr>");


            HttpContext.Current.Response.Write("<td colspan='17' rowspan='2' style='text-align:center;font:bold;border:.5pt solid windowtext;'>" + Report + " - " + Gernder + "</td>");
            //HttpContext.Current.Response.Write("<td colspan='13'> </td>");
            HttpContext.Current.Response.Write("<td colspan='4' rowspan='4' style='text-align:center;font:bold;text-align: right'></td>");

            HttpContext.Current.Response.Write("</tr>");
            HttpContext.Current.Response.Write("<tr>");
            HttpContext.Current.Response.Write("</tr>");
            HttpContext.Current.Response.Write("<tr>");
            //HttpContext.Current.Response.Write("<td style='border:.1pt solid windowtext; font-weight:700; font-size:9pt;'></td>");
            HttpContext.Current.Response.Write("<td colspan='2' ' style='text-align:left;font:bold;border:.5pt solid windowtext;'>District:    " + dtMaster.Rows[0]["DistrictName"].ToString() + "</td>");

            HttpContext.Current.Response.Write("<td  colspan='2' style=' border:.3pt solid windowtext; text-align:left;font:bold;'>Block:    " + dtMaster.Rows[0]["BlockName"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td  colspan='3' style=' border:.3pt solid windowtext; text-align:left;font:bold;'> Cluster:    " + dtMaster.Rows[0]["ClusterName"].ToString() + "</td>");

            HttpContext.Current.Response.Write("<td colspan='5' style=' border:.3pt solid windowtext; text-align:left;font:bold; '>Panchayat: " + dtMaster.Rows[0]["PanchayatName"].ToString() + "</td>");

            HttpContext.Current.Response.Write("<td colspan='5' style=' border:.3pt solid windowtext; text-align:left;font:bold;'> Village: " + dtMaster.Rows[0]["VillageName"].ToString() + "</td>");

            HttpContext.Current.Response.Write("</tr>");


            HttpContext.Current.Response.Write("<tr>");
            //HttpContext.Current.Response.Write("<td style='border:.1pt solid windowtext; font-weight:700; font-size:9pt;'></td>");
            HttpContext.Current.Response.Write("<td colspan='2' ' style='text-align:left;font:bold;border:.5pt solid windowtext;'>TB/FC:   </td>");

            HttpContext.Current.Response.Write("<td  colspan='4' style=' border:.3pt solid windowtext; text-align:left;font:bold;'>   Name:  </td>");
            HttpContext.Current.Response.Write("<td  colspan='4' style=' border:.3pt solid windowtext; text-align:center;font:bold;'> " + Gernder + "Target(5-14) :" + result.Length + "   </td>");

            HttpContext.Current.Response.Write("<td  colspan='4' style=' border:.3pt solid windowtext; text-align:left;font:bold;'> " + Gernder + "Target(7-14) :" + result1.Length + "   </td>");
            HttpContext.Current.Response.Write("<td  colspan='3' style=' border:.3pt solid windowtext; text-align:left;font:bold;'>   Date:  </td>");
            HttpContext.Current.Response.Write("</tr>");
            HttpContext.Current.Response.Write("<tr>");

            HttpContext.Current.Response.Write("</tr>");




            String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
            String HeaderStyle1 = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
            HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
            // HttpContext.Current.Response.Write("<td></td>");
            Int32 iCount = 16;


            for (int Index = 0; Index <= iCount; Index++)
            {
                var firstCell = gvD2dTatget.HeaderRow.Cells[Index];
                if (firstCell.Text != "C" && firstCell.Text != "F" && firstCell.Text != "Form -6" && firstCell.Text != "E" && firstCell.Text != "I")
                {
                    HttpContext.Current.Response.Write("<th class='header' rowspan='2' style='" + HeaderStyle + "  width:2%;'>" + firstCell.Text + "</th>");
                }
                else if (firstCell.Text == "C")
                {
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Status</th>");
                    Index = Index + 2;
                }
                else if (firstCell.Text == "E")
                {
                    HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'>Final Status</th>");
                    Index = Index + 1;
                }
            }
            HttpContext.Current.Response.Write("</tr><tr style='font-width:bold;'>");
            for (int Index = 0; Index <= iCount; Index++)
            {

                var firstCell = gvD2dTatget.HeaderRow.Cells[Index];
                if (firstCell.Text == "C" || firstCell.Text == "F" || firstCell.Text == "Form -6" || firstCell.Text == "I" || firstCell.Text == "E")
                {
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>" + firstCell.Text + "</th>");
                }

            }

            //for (int Index = 0; Index <= iCount; Index++)
            //{

            //    var firstCell = gvD2dTatget.HeaderRow.Cells[Index];
            //    if (firstCell.Text == "C")
            //    {
            //        HttpContext.Current.Response.Write("<th class='header' rowspan='1' colspan='3'  style='" + HeaderStyle + "  width:2%;'> status</th>");
            //    }
            //    else
            //    {

            //        HttpContext.Current.Response.Write("<th class='header' rowspan='2' style='" + HeaderStyle + "  width:2%;'>" + firstCell.Text + "</th>");
            //    }

            //}
            //HttpContext.Current.Response.Write("</tr>");
            //HttpContext.Current.Response.Write("<tr>");
            //HttpContext.Current.Response.Write("<td style='border:.1pt solid windowtext; font-weight:700; font-size:9pt;'></td>");
            //HttpContext.Current.Response.Write("<td colspan='1' style='text-align:center;font:bold;'></td>");
            //HttpContext.Current.Response.Write("</tr>");
            String DataStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";
            String DataGrey = "border:.1pt dotted windowtext; background:#dddddd; font-weight:100; font-size:9pt;";
            String dataBl = "border:.1pt dotted windowtext; font-weight:700; font-size:9pt;";
            int intMonth = DateTime.Now.Month;
            int intYear = DateTime.Now.Year;

            int i = 0; String day = "";

            for (i = 0; i < dt.Rows.Count; i++)
            {
                var RowStyle = DataStyle;

                HttpContext.Current.Response.Write("<tr>");
                //HttpContext.Current.Response.Write("<td >Direct</td>");
                for (int c = 0; c < dt.Columns.Count; c++)
                {

                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c].ToString() + "</td>");

                }

                HttpContext.Current.Response.Write("</tr>");
            }
            DataStyle = "border:.3pt solid windowtext; font-size:9pt;";

            HttpContext.Current.Response.Write("</tr>");


            //  DataStyle += "background-color:yellow;";


            HttpContext.Current.Response.Write("</table>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();

        }
        catch
        {

            throw;
        }


    }


    private void GenerateExcelDropOut(DataTable dt, string FIleName, string Report)
    {
        try
        {



            string ddlVillage = "";

            foreach (ListItem item in chkVillage.Items)
            {
                if (item.Selected)
                {

                    ddlVillage += "'" + item.Value + "" + "',";


                }
            }

            if (ddlVillage.Length > 0)
            {
                ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
            }

            string strQry = "select DistrictName,BlockName,PanchayatName,ClusterName,Villagename from mst5Village inner join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode inner join mst3Block on mst3Block.BlockCode=mst5Village.BlockCode inner join mstPanchayat on mstPanchayat.PanchayatCode=mst5Village.PanchayatCode left join mstCluster on mstCluster.ClusterCode=mst5Village.ClusterCode where villagecode=" + ddlVillage + "  ";


            DataTable dtMaster = objMain.LoadData(strQry);

            DataRow[] result = dt.Select("Age >= 5 AND Age <=14");
            DataRow[] result1 = dt.Select("Age >= 7 AND Age <=14");
            string Gernder = "";
            if (Convert.ToInt32(ddlGender.SelectedValue) == 0)
            {
                Gernder = "All";
            }
            if (Convert.ToInt32(ddlGender.SelectedValue) == 2)
            {
                Gernder = ddlGender.SelectedItem.Text;
            }
            if (Convert.ToInt32(ddlGender.SelectedValue) == 1)
            {
                Gernder = ddlGender.SelectedItem.Text;
            }

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            string Fullfilename = "" + FIleName + "_" + Gernder + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");

            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            HttpContext.Current.Response.Write("<table  >");
            HttpContext.Current.Response.Write("<tr>");


            HttpContext.Current.Response.Write("<td colspan='17' rowspan='2' style='text-align:center;font:bold;border:.5pt solid windowtext;'>" + Report + " - " + Gernder + "</td>");
            //HttpContext.Current.Response.Write("<td colspan='13'> </td>");
            HttpContext.Current.Response.Write("<td colspan='4' rowspan='4' style='text-align:center;font:bold;text-align: right'></td>");

            HttpContext.Current.Response.Write("</tr>");
            HttpContext.Current.Response.Write("<tr>");
            HttpContext.Current.Response.Write("</tr>");
            HttpContext.Current.Response.Write("<tr>");
            //HttpContext.Current.Response.Write("<td style='border:.1pt solid windowtext; font-weight:700; font-size:9pt;'></td>");
            HttpContext.Current.Response.Write("<td colspan='2' ' style='text-align:left;font:bold;border:.5pt solid windowtext;'>District:    " + dtMaster.Rows[0]["DistrictName"].ToString() + "</td>");

            HttpContext.Current.Response.Write("<td  colspan='2' style=' border:.3pt solid windowtext; text-align:left;font:bold;'>Block:    " + dtMaster.Rows[0]["BlockName"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td  colspan='3' style=' border:.3pt solid windowtext; text-align:left;font:bold;'> Cluster:    " + dtMaster.Rows[0]["ClusterName"].ToString() + "</td>");

            HttpContext.Current.Response.Write("<td colspan='5' style=' border:.3pt solid windowtext; text-align:left;font:bold; '>Panchayat: " + dtMaster.Rows[0]["PanchayatName"].ToString() + "</td>");

            HttpContext.Current.Response.Write("<td colspan='5' style=' border:.3pt solid windowtext; text-align:left;font:bold;'> Village: " + dtMaster.Rows[0]["VillageName"].ToString() + "</td>");

            HttpContext.Current.Response.Write("</tr>");


            HttpContext.Current.Response.Write("<tr>");
            //HttpContext.Current.Response.Write("<td style='border:.1pt solid windowtext; font-weight:700; font-size:9pt;'></td>");
            HttpContext.Current.Response.Write("<td colspan='2' ' style='text-align:left;font:bold;border:.5pt solid windowtext;'>TB/FC:   </td>");

            HttpContext.Current.Response.Write("<td  colspan='4' style=' border:.3pt solid windowtext; text-align:left;font:bold;'>   Name:  </td>");
            HttpContext.Current.Response.Write("<td  colspan='4' style=' border:.3pt solid windowtext; text-align:center;font:bold;'> " + Gernder + "Target(5-14) :" + result.Length + "   </td>");

            HttpContext.Current.Response.Write("<td  colspan='4' style=' border:.3pt solid windowtext; text-align:left;font:bold;'> " + Gernder + "Target(7-14) :" + result1.Length + "   </td>");
            HttpContext.Current.Response.Write("<td  colspan='3' style=' border:.3pt solid windowtext; text-align:left;font:bold;'>   Date:  </td>");
            HttpContext.Current.Response.Write("</tr>");
            HttpContext.Current.Response.Write("<tr>");

            HttpContext.Current.Response.Write("</tr>");




            String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
            String HeaderStyle1 = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
            HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
            // HttpContext.Current.Response.Write("<td></td>");
            Int32 iCount = 18;


            for (int Index = 0; Index <= iCount; Index++)
            {
                var firstCell = gvD2dTargetDropOut.HeaderRow.Cells[Index];
                if (firstCell.Text != "C" && firstCell.Text != "F" && firstCell.Text != "Form -6" && firstCell.Text != "E" && firstCell.Text != "I")
                {
                    HttpContext.Current.Response.Write("<th class='header' rowspan='2' style='" + HeaderStyle + "  width:2%;'>" + firstCell.Text + "</th>");
                }
                else if (firstCell.Text == "C")
                {
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Status</th>");
                    Index = Index + 2;
                }
                else if (firstCell.Text == "E")
                {
                    HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'>Final Status</th>");
                    Index = Index + 1;
                }
            }
            HttpContext.Current.Response.Write("</tr><tr style='font-width:bold;'>");
            for (int Index = 0; Index <= iCount; Index++)
            {

                var firstCell = gvD2dTargetDropOut.HeaderRow.Cells[Index];
                if (firstCell.Text == "C" || firstCell.Text == "F" || firstCell.Text == "Form -6" || firstCell.Text == "I" || firstCell.Text == "E")
                {
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>" + firstCell.Text + "</th>");
                }

            }

            //for (int Index = 0; Index <= iCount; Index++)
            //{

            //    var firstCell = gvD2dTargetDropOut.HeaderRow.Cells[Index];
            //    if (firstCell.Text == "C")
            //    {
            //        HttpContext.Current.Response.Write("<th class='header' rowspan='1' colspan='3'  style='" + HeaderStyle + "  width:2%;'> status</th>");
            //    }
            //    else
            //    {

            //        HttpContext.Current.Response.Write("<th class='header' rowspan='2' style='" + HeaderStyle + "  width:2%;'>" + firstCell.Text + "</th>");
            //    }

            //}
            //HttpContext.Current.Response.Write("</tr>");
            //HttpContext.Current.Response.Write("<tr>");
            //HttpContext.Current.Response.Write("<td style='border:.1pt solid windowtext; font-weight:700; font-size:9pt;'></td>");
            //HttpContext.Current.Response.Write("<td colspan='1' style='text-align:center;font:bold;'></td>");
            //HttpContext.Current.Response.Write("</tr>");
            String DataStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";
            String DataGrey = "border:.1pt dotted windowtext; background:#dddddd; font-weight:100; font-size:9pt;";
            String dataBl = "border:.1pt dotted windowtext; font-weight:700; font-size:9pt;";
            int intMonth = DateTime.Now.Month;
            int intYear = DateTime.Now.Year;

            int i = 0; String day = "";

            for (i = 0; i < dt.Rows.Count; i++)
            {
                var RowStyle = DataStyle;

                HttpContext.Current.Response.Write("<tr>");
                //HttpContext.Current.Response.Write("<td >Direct</td>");
                for (int c = 0; c < dt.Columns.Count; c++)
                {

                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c].ToString() + "</td>");

                }

                HttpContext.Current.Response.Write("</tr>");
            }
            DataStyle = "border:.3pt solid windowtext; font-size:9pt;";

            HttpContext.Current.Response.Write("</tr>");


            //  DataStyle += "background-color:yellow;";


            HttpContext.Current.Response.Write("</table>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();

        }
        catch
        {

            throw;
        }


    }
    public void LoadDataLoadDataTarget(int type)
    {
        string strQry = "";
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

        conditions = "";

        conditions = "  mst5Village.Fyear='" + ddlYear.SelectedItem.Text + "'";

        if (ddlStatecode.Length > 0)
        {
            conditions += " and mst5Village.StateCode in(" + ddlStatecode + ")";
        }

        if (ddlDistrict.Length > 0)
        {

            conditions = conditions + "and mst5Village.DistrictCode in(" + ddlDistrict.ToString() + ")";

        }



        if (ddlBlock.Length > 0)
        {
            if (Convert.ToInt32(rblBlockType.SelectedValue) == 1)
            {
                conditions = conditions + "and mst5Village.BlockCode in(" + ddlBlock.ToString() + ")";

            }
            if (Convert.ToInt32(rblBlockType.SelectedValue) == 2)
            {
                conditions = conditions + "and mst5Village.MainBlockCode in(" + ddlBlock.ToString() + ")";
            }
        }

        if (ddlPhan.Length > 0)
        {
            conditions = conditions + " and mst5Village.PanchayatCode in(" + ddlPhan.ToString() + ") ";
        }
        if (ddlVillage.Length > 0)
        {

            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }
        if (ddlGender.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlGender.SelectedValue) == 2)
            {
                conditions += " and tblTargetSummaryAllYearNew.Lookupcode =48";
            }
            if (Convert.ToInt32(ddlGender.SelectedValue) == 1)
            {
                conditions += " and tblTargetSummaryAllYearNew.Lookupcode =47";
            }
        }
        string conGroupby = "", conSelect = "", conJoin = "";

        if (ddlDistrict == "")
        {
            conSelect = " mst2District.DistrictName ,mst2District.EGDistrictCode as DistrictCode ";
            conGroupby = " mst2District.DistrictName ,mst2District.EGDistrictCode ";
            conJoin = " inner join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode ";
        }
        if (ddlDistrict.Length > 0)
        {
            conSelect = "mst2District.DistrictName ,mst2District.EGDistrictCode as DistrictCode,  mst3Block.BlockName ,mst3Block.EGBlockCode as BlockCode ";
            conGroupby = "mst2District.DistrictName ,mst2District.EGDistrictCode ,mst3Block.BlockName ,mst3Block.EGBlockCode";
            conJoin = " inner join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode inner join mst3Block on mst3Block.BlockCode=mst5Village.BlockCode  ";
        }
        if (ddlBlock.Length > 0)
        {
            conSelect = "mst2District.DistrictName ,mst2District.EGDistrictCode as DistrictCode,  mst3Block.BlockName ,mst3Block.EGBlockCode as BlockCode ,mstPanchayat.PanchayatName,mstPanchayat.EGPanchayatCode as PanchayatCode ";
            conGroupby = "mst2District.DistrictName ,mst2District.EGDistrictCode ,mst3Block.BlockName ,mst3Block.EGBlockCode,mstPanchayat.PanchayatName,mstPanchayat.EGPanchayatCode";
            conJoin = " inner join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode inner join mst3Block on mst3Block.BlockCode=mst5Village.BlockCode  inner join mstPanchayat on mstPanchayat.PanchayatCode=mst5Village.PanchayatCode and mstPanchayat.BlockCode=mst5Village.BlockCode ";

        }
        if (ddlPhan.Length > 0)
        {
            conSelect = "mst2District.DistrictName ,mst2District.EGDistrictCode as DistrictCode,  mst3Block.BlockName ,mst3Block.EGBlockCode as BlockCode ,mstPanchayat.PanchayatName,mstPanchayat.EGPanchayatCode as PanchayatCode,mst5Village.VillageName  ,mst5Village.EGVillagecode as Villagecode ";
            conGroupby = "mst2District.DistrictName ,mst2District.EGDistrictCode ,mst3Block.BlockName ,mst3Block.EGBlockCode,mstPanchayat.PanchayatName,mstPanchayat.EGPanchayatCode ,mst5Village.VillageName,mst5Village.EGVillagecode ";
            conJoin = " inner join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode inner join mst3Block on mst3Block.BlockCode=mst5Village.BlockCode  inner join mstPanchayat on mstPanchayat.PanchayatCode=mst5Village.PanchayatCode and mstPanchayat.BlockCode=mst5Village.BlockCode ";

        }

        SqlParameter[] parm = new SqlParameter[]
        {
             new SqlParameter("@conSelect",  conSelect),
           new SqlParameter("@conGroupby",  conGroupby),
               new SqlParameter("@conWhere",  conditions),
                    new SqlParameter("@conJoin",  conJoin),


        };
        DataTable dt = null;
        if (type == 1)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTargetSummaryReport]", parm);
        }
        else
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTargetSummaryReportYearWiseNew]", parm);
        }

        if (dt.Rows.Count > 0)
        {
            Session["TargetSummary"] = dt;
            GV_DynamicGrid.DataSource = dt;
            GV_DynamicGrid.DataBind();
            lblTotalCount.Text = (dt.Rows.Count).ToString();
        }

        else
        {
            Session["TargetSummary"] = null;
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }


    }
    protected void btnMain_Click(object sender, EventArgs e)
    {


    }
    protected void GV_DynamicGrid_RowCreated(object sender, GridViewRowEventArgs e)
    {

        //        Baseline
        if (ViewState["1"].ToString() == "20")
        {

            #region Basline

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

            if (e.Row.RowType == DataControlRowType.Header)
            {


                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                HeaderGridRow.CssClass = "gridnewheadercss";
                TableCell HeaderCell;

                HeaderCell = new TableCell();
                HeaderCell.Text = "Dist Profile";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                if (ddlDistrict == "")
                {
                    HeaderCell.ColumnSpan = 2;
                }
                if (ddlDistrict.Length > 0)
                {
                    HeaderCell.ColumnSpan = 4;
                }
                if (ddlBlock.Length > 0)
                {
                    HeaderCell.ColumnSpan = 6;
                }
                if (ddlPhan.Length > 0)
                {
                    HeaderCell.ColumnSpan = 8;
                }
                HeaderCell.CssClass = "gridnewheadercss";
                HeaderGridRow.Cells.Add(HeaderCell);


                HeaderCell = new TableCell();
                HeaderCell.Text = "2016-2017 ";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                if (ddlDistrict == "")
                {
                    HeaderCell.ColumnSpan = 7;
                }
                if (ddlDistrict.Length > 0)
                {
                    HeaderCell.ColumnSpan = 7;
                }
                HeaderCell.CssClass = "gridnewheadercss";
                HeaderGridRow.Cells.Add(HeaderCell);


                HeaderCell = new TableCell();
                HeaderCell.Text = "2017-2018 ";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                if (ddlDistrict == "")
                {
                    HeaderCell.ColumnSpan = 11;
                }
                if (ddlDistrict.Length > 0)
                {
                    HeaderCell.ColumnSpan = 11;
                }
                if (ddlBlock.Length > 0)
                {
                    HeaderCell.ColumnSpan = 13;
                }
                if (ddlPhan.Length > 0)
                {
                    HeaderCell.ColumnSpan = 14;
                }
                HeaderCell.CssClass = "gridnewheadercss";
                HeaderGridRow.Cells.Add(HeaderCell);




                HeaderCell = new TableCell();
                HeaderCell.Text = "2018-2019 ";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                if (ddlDistrict == "")
                {
                    HeaderCell.ColumnSpan = 15;
                }
                if (ddlDistrict.Length > 0)
                {
                    HeaderCell.ColumnSpan = 18;
                }
                HeaderGridRow.Cells.Add(HeaderCell);

                //
                GV_DynamicGrid.Controls[0].Controls.AddAt(0, HeaderGridRow);




            }
            #endregion
        }
    }
    public void LoadExecel()
    {



        if (GV_DynamicGrid.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=EG_Report_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GV_DynamicGrid.AllowPaging = false;


                LoadDataLoadDataTarget(2);
                GV_DynamicGrid.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GV_DynamicGrid.HeaderRow.Cells)
                {
                    cell.BackColor = GV_DynamicGrid.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GV_DynamicGrid.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GV_DynamicGrid.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GV_DynamicGrid.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GV_DynamicGrid.RenderControl(hw);

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
}