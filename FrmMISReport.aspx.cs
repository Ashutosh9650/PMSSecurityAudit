using ClosedXML.Excel;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmMISReport : System.Web.UI.Page
{
    string conditions = "";
    Comman objComman = new Comman();
    clsMain objMain = new clsMain();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {

                //string FilePath = "D:\\WRD\\GridViewExport (36).xls";


                //string file = FilePath;
                //StreamReader sr;
                //FileInfo fi = new FileInfo(file);
                //string input = "";
                //if (File.Exists(file))
                //{
                //    sr = File.OpenText(file);
                //    input += Server.HtmlEncode(sr.ReadToEnd());
                //    sr.Close();
                //}
                //input = input.Replace("&lt;", string.Empty);
                //input = input.Replace("tr&gt;", string.Empty);
                //input = input.Replace("td&gt;", string.Empty);

                //input = input.Replace("\t\t\t", string.Empty);
                //string[] data = input.Split('\n');

                //for (int d = 0; d < data.Length; d++)
                //{

                //    int le = data[d].Length;
                //    string[] xyz = data[d].Split(' ', '/');



                //}

                LoadYear();
                LoadUserLeavel();


            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
        }
    }
    #region ****************** Fill Function *******************
    public void FillCBState()
    {
        conditions = "";
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
            string strQry1 = "sELECT distinct mst2District.DistrictCode as DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist  ";
            strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions + "  and  Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName  ";
            dtDistrict = objMain.LoadData(strQry1);
        }
        else
        {
            string strQry = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where " + conditions + "  order by DistrictName   ";
            dtDistrict = objMain.LoadData(strQry);
        }
        chkDistrict.DataSource = dtDistrict;
        chkDistrict.DataTextField = "DistrictName";
        chkDistrict.DataValueField = "DistrictCode";
        chkDistrict.DataBind();
        if (Session["user_level_Role"].ToString() == "2")
        {
            foreach (ListItem item in chkDistrict.Items)
            {
                item.Selected = true;
            }
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

                dr = dtYear.NewRow();
                dr["Type"] = GivenYear.ToString() + "-" + Convert.ToString((GivenYear + 1));
                dr["ID"] = y;
                dtYear.Rows.Add(dr);

                //get last  two digits (eg: 10 from 2010);


            }

        }
        dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


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
            string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State  where   " + conditions + "  order by StateName   ";
            DataTable dtState = objMain.LoadData(strQry1);
            ChkState.DataSource = dtState;
            ChkState.DataTextField = "StateName";
            ChkState.DataValueField = "StateCode";
            ChkState.DataBind();
            foreach (ListItem item in ChkState.Items)
            {
                item.Selected = true;
            }
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
            conditions = "UserName='" + Session["username"].ToString() + "' ";
            string strQry1 = "  SELECT distinct mst2District.DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode  where   " + conditions + " and Fyear='" + ddlYear.SelectedItem.Text + "'  order by DistrictName   ";
            DataTable dtDistrict = objMain.LoadData(strQry1);
            chkDistrict.DataSource = dtDistrict;
            chkDistrict.DataTextField = "DistrictName";
            chkDistrict.DataValueField = "DistrictCode";
            chkDistrict.DataBind();
            if (Session["user_level_Role"].ToString() == "2")
            {
                foreach (ListItem item in chkDistrict.Items)
                {
                    item.Selected = true;
                }
            }
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
            string strQry1 = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where " + conditions + "  order by DistrictName   ";
            DataTable dtDistrict = objMain.LoadData(strQry1);
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
            foreach (ListItem item in chkDistrict.Items)
            {
                item.Selected = true;
            }
        }
    }
    #endregion
    #region ********** Button Click Events ********************



    protected void LnkProcess_OnClick(object sender, EventArgs e)
    {
        if (ddlMonth.SelectedIndex > 0)
        {
            if (ddlMonth.SelectedIndex > 0)
            {
                ViewState["1"] = 7;

                LoadPlanReportProcess1(1);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Week ')</script>", false);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Month ')</script>", false);
        }
    }

    protected void LnkProcess1_OnClick(object sender, EventArgs e)
    {
        if (ddlMonth.SelectedIndex > 0)
        {
            if (ddlWeek.SelectedIndex > 0)
            {
                ViewState["1"] = 7;

                LoadPlanReportProcess1(1);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Week ')</script>", false);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Month ')</script>", false);
        }
    }
    public void LoadPlanReportProcess(int Flag)
    {
        string conditions = "";
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




        string condition = string.Empty;
        string ConNe = string.Empty;
        string Con1 = string.Empty;
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "     mst2District.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
            ConNe += "     mst3Block.Fyear = '" + ddlYear.SelectedItem.Text + "' ";


        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and mst2District.StateCode in(" + ddlStatecode + ") ";
            ConNe += "   and   mst3Block.StateCode in(" + ddlStatecode + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst2District.DistrictCode in(" + ddlDistrict + ") ";
            ConNe += " and mst3Block.DistrictCode in(" + ddlDistrict + ") ";
        }

        if (ddlBlock.Length > 0)
        {

            ConNe += " and mst3Block.BlockCode in(" + ddlBlock + ") ";


        }

        int Myear = Convert.ToInt32(ddlYear.SelectedValue);
        if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        {
            Myear = Myear + 1;
        }

        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Fyear",Myear),
              new SqlParameter("@mMonth",ddlMonth.SelectedValue),
             new SqlParameter("@WeekType",ddlWeek.SelectedValue),
        };
        DataSet dt = null;


        dt = GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptperfomancereort", cmdParameters);

        if (dt.Tables[0].Rows.Count > 0)
        {
            ViewState["SAC"] = dt;
            MultipuExeclProcess();
        }




    }


    public void LoadPlanReportProcess1(int Flag)
    {
        string conditions = "";
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




        string condition = string.Empty;
        string ConNe = string.Empty;
        string Con1 = string.Empty;
        if (ddlYear.SelectedIndex > 0)
        {
            condition += "  where   mst2District.Fyear = '" + ddlYear.SelectedItem.Text + "' ";


        }
        if (ddlStatecode.Length > 0)
        {
            condition += " and mst2District.StateCode in(" + ddlStatecode + ") ";


        }
        if (ddlDistrict.Length > 0)
        {
            condition += " and mst2District.DistrictCode in(" + ddlDistrict + ") ";

        }



        DataSet dt = null;


        SqlParameter[] cmdParameters = new SqlParameter[]
            {

              new SqlParameter("@M",ddlMonth.SelectedValue),
             new SqlParameter("@W",ddlWeek.SelectedValue),

               new SqlParameter("@Con",condition),
            };


        dt = GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptWeekReportSummary", cmdParameters);

        if (dt.Tables[0].Rows.Count > 0)
        {
            ViewState["SAC"] = dt;
            MultipuExeclProcess1();
        }




    }

    public void MultipuExeclProcess1()
    {
        DataSet dtMain1 = ViewState["SAC"] as DataSet;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\WeeklyPMSAutomation.xlsx");
        var ws = wb.Worksheet(1);
        var ws1 = wb.Worksheet(2);
        var ws2 = wb.Worksheet(3);
        var ws3 = wb.Worksheet(4);

        DataTable dt = dtMain1.Tables[0];
        dt.Columns.Remove("RowNo");
        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("RowNo");
        ws.Cell(2, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 1;
        string str = "A2:O" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        DataTable dt1 = dtMain1.Tables[1];

        dt1.Columns.Remove("RowNo");

        ws1.Cell(2, 1).InsertData(dt1.Rows);
        Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 1;
        string str1 = "A2:E" + ii1;
        ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        DataTable dt2 = dtMain1.Tables[2];

        dt2.Columns.Remove("RowNo");
        ws2.Cell(2, 1).InsertData(dt2.Rows);
        Int32 ii2 = Convert.ToInt32(dt2.Rows.Count) + 1;
        string str2 = "A2:E" + ii2;
        ws2.Range(str2).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws2.Range(str2).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws2.Range(str2).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws2.Range(str2).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        DataTable dt3 = dtMain1.Tables[3];

        dt3.Columns.Remove("RowNo");
        ws3.Cell(2, 1).InsertData(dt3.Rows);
        Int32 ii3 = Convert.ToInt32(dt3.Rows.Count) + 1;
        string str3 = "A2:E" + ii2;
        ws3.Range(str3).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws3.Range(str3).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws3.Range(str3).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws3.Range(str3).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);



        filepath = StartupPath + "\\WeeklyUpdateReport " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
    public static DataSet GetDataSet(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParameters)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(connString);
        SqlCommand cmd = new SqlCommand();

        try
        {
            PrepareCommand(cmd, conn, cmdType, cmdText, cmdParameters);
            da.SelectCommand = new SqlCommand();
            da.SelectCommand = cmd;
            da.Fill(ds);
            return ds;
        }
        catch
        {
            throw;
        }
        finally
        {
            conn.Close();
        }
    }

    private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParameters)
    {
        if (conn.State != ConnectionState.Open)
            conn.Open();
        cmd.Connection = conn;
        cmd.CommandTimeout = 0;
        cmd.CommandType = cmdType;
        cmd.CommandText = cmdText;

        if (cmdParameters != null)
        {
            foreach (SqlParameter param in cmdParameters)
            {
                cmd.Parameters.Add(param);
            }
        }
    }

    public void MultipuExeclProcess()
    {
        DataSet dtMain1 = ViewState["SAC"] as DataSet;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\WeeklyEnrolmentReportformat.xlsx");
        var ws = wb.Worksheet(1);
        var ws1 = wb.Worksheet(2);
        var ws2 = wb.Worksheet(3);
        var ws3 = wb.Worksheet(4);
        var ws4 = wb.Worksheet(5);
        DataTable dt = dtMain1.Tables[0];
        dt.Columns.Remove("RowNo");
        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("RowNo");
        ws.Cell(4, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 3;
        string str = "A4:G" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        DataTable dt1 = dtMain1.Tables[1];

        dt1.Columns.Remove("RowNo");

        ws1.Cell(4, 1).InsertData(dt1.Rows);
        Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 3;
        string str1 = "A4:G" + ii1;
        ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        DataTable dt2 = dtMain1.Tables[2];

        dt2.Columns.Remove("RowNo");
        ws2.Cell(4, 1).InsertData(dt2.Rows);
        Int32 ii2 = Convert.ToInt32(dt2.Rows.Count) + 3;
        string str2 = "A4:G" + ii2;
        ws2.Range(str2).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws2.Range(str2).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws2.Range(str2).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws2.Range(str2).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        DataTable dt3 = dtMain1.Tables[3];

        dt3.Columns.Remove("RowNo");
        ws3.Cell(4, 1).InsertData(dt3.Rows);
        Int32 ii3 = Convert.ToInt32(dt3.Rows.Count) + 3;
        string str3 = "A4:G" + ii2;
        ws3.Range(str3).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws3.Range(str3).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws3.Range(str3).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws3.Range(str3).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        DataTable dt4 = dtMain1.Tables[4];

        dt4.Columns.Remove("RowNo");
        ws3.Cell(22, 1).InsertData(dt4.Rows);


        DataTable dt5 = dtMain1.Tables[5];
        dt5.Columns.Remove("RowNo");
        ws1.Cell(23, 1).InsertData(dt5.Rows);

        DataTable dt6 = dtMain1.Tables[6];
        dt6.Columns.Remove("RowNo");
        ws2.Cell(23, 1).InsertData(dt6.Rows);

        DataTable dt7 = dtMain1.Tables[7];
        dt7.Columns.Remove("RowNo");
        ws3.Cell(41, 1).InsertData(dt7.Rows);

        DataTable dt8 = dtMain1.Tables[8];
        ws4.Cell(4, 1).InsertData(dt8.Rows);

        //ws1.Cell(4, 1).InsertData(dt1.Rows);

        //Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 3;
        //string str1 = "A4:AL" + ii1;

        //ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        //ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        //ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        //ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        //DataTable dt2 = dtMain1.Tables[2];
        //dt2.Columns.Remove("rowno");
        //ws3.Cell(3, 1).InsertData(dt2.Rows);


        //Int32 ii11 = Convert.ToInt32(dt2.Rows.Count) + 2;
        //string str11 = "A3:O" + ii11;

        //ws3.Range(str11).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        //ws3.Range(str11).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        //ws3.Range(str11).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        //ws3.Range(str11).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
        //DataTable dt3 = dtMain1.Tables[3];
        //ws3.Cell(2, 2).Value = "Week (" + dt3.Rows[0]["Week1"].ToString() + " to  " + dt3.Rows[0]["Cumulative1"].ToString() + ")";
        //ws3.Cell(2, 3).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative1"].ToString() + ")";
        //ws3.Cell(2, 4).Value = "Week (" + dt3.Rows[0]["Week2"].ToString() + " to  " + dt3.Rows[0]["Cumulative2"].ToString() + ")";
        //ws3.Cell(2, 5).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative2"].ToString() + ")";
        //ws3.Cell(2, 6).Value = "Week (" + dt3.Rows[0]["Week3"].ToString() + " to  " + dt3.Rows[0]["Cumulative3"].ToString() + ")";
        //ws3.Cell(2, 7).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative3"].ToString() + ")";
        //ws3.Cell(2, 8).Value = "Week (" + dt3.Rows[0]["Week4"].ToString() + " to  " + dt3.Rows[0]["Cumulative4"].ToString() + ")";
        //ws3.Cell(2, 9).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative4"].ToString() + ")";
        //ws3.Cell(2, 10).Value = "Week (" + dt3.Rows[0]["Week5"].ToString() + " to  " + dt3.Rows[0]["Cumulative5"].ToString() + ")";
        //ws3.Cell(2, 11).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative5"].ToString() + ")";
        //ws3.Cell(2, 12).Value = "Week (" + dt3.Rows[0]["Week6"].ToString() + " to  " + dt3.Rows[0]["Cumulative6"].ToString() + ")";
        //ws3.Cell(2, 13).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative6"].ToString() + ")";
        //ws3.Cell(2, 14).Value = "Week (" + dt3.Rows[0]["Week7"].ToString() + " to  " + dt3.Rows[0]["Cumulative7"].ToString() + ")";
        //ws3.Cell(2, 15).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative7"].ToString() + ")";

        filepath = StartupPath + "\\WeeklyUpdateReport " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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

    protected void LnkMIS_OnClick(object sender, EventArgs e)
    {
        if (ddlMonth.SelectedIndex > 0)
        {

            ViewState["1"] = 7;

            btnExport_Click(LinkButton5, null);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Month ')</script>", false);
        }
    }
    protected void LnkKMI_OnClick(object sender, EventArgs e)
    {
        if (ddlMonth.SelectedIndex > 0)
        {

            ViewState["1"] = 1222;

            btnKM_Click(LinkButton5, null);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Month ')</script>", false);
        }
    }

    public void btnKM_Click(object sender, EventArgs e)
    {

        conditions = "";
        string conditions1 = "";
        string ddlDistrict = "";

        string ddlStatecode = "";
        string ddlStatecodeName = "";

        string ddlDistrictName = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";
                ddlStatecodeName += "'" + item.Text + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
            ddlStatecodeName = ddlStatecodeName.Substring(0, ddlStatecodeName.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";

                ddlDistrictName += "'" + item.Text + "'" + ",";
            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
            ddlDistrictName = ddlDistrictName.Substring(0, ddlDistrictName.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "    and mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }

        if (ddlStatecode.Length > 0)
        {
            conditions += " and mst5Village.StateCode in(" + ddlStatecode + ") ";
            conditions1 += " and StateName in(" + ddlStatecodeName + ") ";

        }


        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
            conditions1 += " and DistrictName in(" + ddlDistrictName + ") ";

        }

        int Myear = Convert.ToInt32(ddlYear.SelectedValue);
        if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        {
            Myear = Myear + 1;
        }

        DataTable dt = null;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2022)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),
             new SqlParameter("@Con1",conditions1),
               new SqlParameter("@month",ddlMonth.SelectedValue),
                 new SqlParameter("@Year",Myear),

        };

            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptKMISummaryReport2022]", cmdParameters);
        }
        else
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
       {
            new SqlParameter("@Con",conditions),
             new SqlParameter("@Con1",conditions1),
               new SqlParameter("@month",ddlMonth.SelectedValue),
                 new SqlParameter("@Year",Myear),

       };

            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptKMISummaryReport]", cmdParameters);
        }
        if (dt.Rows.Count > 0)
        {
            dt.Columns.Remove("rowno");
            ViewState["SAC"] = dt;
            MultipuExeclTrack();
        }

    }
    public void MultipuExeclTrack()
    {


        DataTable dtMain1 = ViewState["SAC"] as DataTable;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\KMI.xlsx");
        var ws = wb.Worksheet(1);
        //var ws1 = wb.Worksheet(2);
        //var ws3 = wb.Worksheet(3);
        DataTable dt = dtMain1.Copy();
        //dt.Columns.Remove("rownNO");
        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("rownNO");
        ws.Cell(2, 4).Value = "April-till " + ddlMonth.SelectedItem.Text + " Achiev";
        ws.Cell(2, 8).Value = "April-till " + ddlMonth.SelectedItem.Text + " Achiev";
        ws.Cell(2, 12).Value = "April-till " + ddlMonth.SelectedItem.Text + " Achiev";
        ws.Cell(2, 16).Value = "April-till " + ddlMonth.SelectedItem.Text + " Achiev";

        ws.Cell(3, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 2;
        string str = "A2:Q" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
        //ws1.Cell(4, 1).InsertData(dt1.Rows);




        filepath = StartupPath + "\\KMI " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
    public void btnExport_Click(object sender, EventArgs e)
    {
        XLWorkbook wb = new XLWorkbook();
        try
        {
            DataSet ds = new DataSet();
            conditions = "where 1 = 1 ";
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
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "    and MFyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "    and mMonth = '" + ddlMonth.SelectedValue + "' ";

            }
            if (ddlStatecode.Length > 0)
            {
                conditions += " and StateCode in(" + ddlStatecode + ") ";

            }


            if (ddlDistrict.Length > 0)
            {
                conditions += " and DistrictCode in(" + ddlDistrict + ") ";

            }


            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),


        };
            string SMonth = "";
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                SMonth = "Data Range Date (CM): 26th Dec - 25th Jan";
            }
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 2)
            {
                SMonth = "Data Range Date (CM): 26th Jan - 25th Feb";
            }
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                SMonth = "Data Range Date (CM): 26th Feb - 31th Mar";
            }
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
            {
                SMonth = "Data Range Date (CM): 1th Apr - 25th Apr";
            }
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 5)
            {
                SMonth = "Data Range Date (CM): 26th Apr - 25th May";
            }
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 6)
            {
                SMonth = "Data Range Date (CM): 26th May - 25th Jun";
            }
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 7)
            {
                SMonth = "Data Range Date (CM): 26th Jun - 25th Jul";
            }

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 8)
            {
                SMonth = "Data Range Date (CM): 26th Jul - 25th Aug";
            }
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 9)
            {
                SMonth = "Data Range Date (CM): 26th Aug - 25th Sep";
            }
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 10)
            {
                SMonth = "Data Range Date (CM): 26th Sep - 25th Oct";
            }
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 11)
            {
                SMonth = "Data Range Date (CM): 26th Oct - 25th Nov";
            }
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 12)
            {
                SMonth = "Data Range Date (CM): 26th Nov - 25th Desc";
            }
            ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Report_Mis_Excel_MultipleSheet2020]", cmdParameters);
            // ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Report_Mis_Excel_MultipleSheet");
            ds.Tables[6].Columns.Remove("Rr");
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                for (int x = 0; x < ds.Tables[i].Rows.Count; x++)
                {

                    if (ds.Tables[i].Rows[x][1].ToString().Trim() == "ZGround_Total")
                    {
                        ds.Tables[i].Rows[x][1] = "Ground_Total";
                    }
                    //if (Convert.ToString(dt.Rows[x][0]).Contains("_Total"))
                    //{

                    //    for (int y = 0; y < dt.Columns.Count; y++)
                    //    {
                    //        ws.Cell(x, y).Style.Font.Bold = true;
                    //    }
                    //}
                }
                // ds.Tables[i].Columns["ZGround_Total"].ColumnName = "Ground_Total";
                string sheetname = Convert.ToString(ds.Tables[i].Rows[0]["SheetName"]);
                if (ds.Tables[i].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[i];
                    try
                    {
                        dt.Columns.RemoveAt(0);
                    }
                    catch
                    {

                        throw;
                    }
                    var ws = wb.Worksheets.Add(dt, sheetname);

                    if (sheetname == "District Wise Enrollment")
                    {
                        var blueRow = ws.Row(1);
                        blueRow.InsertRowsAbove(5);
                        ws.Range(1, 1, 5, ds.Tables[i].Columns.Count).Style.Fill.BackgroundColor = XLColor.White;
                        ws.Range(3, 1, 5, ds.Tables[i].Columns.Count).Style.Font.Bold = true;
                        ws.Cell(1, 1).Value = "PMS Data";
                        ws.Cell(1, 2).Value = "Date";
                        ws.Cell(1, 3).Value = DateTime.Now.ToString("dd/MM/yyyy");
                        ws.Cell(1, 5).Value = SMonth;
                        if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
                        {
                            ws.Cell(1, 11).Value = "Data Range Date (YTD): 1st April - 31th " + ddlMonth.SelectedItem.Text + "";
                        }
                        else
                        {
                            ws.Cell(1, 11).Value = "Data Range Date (YTD): 1st April - 25th " + ddlMonth.SelectedItem.Text + "";
                        }
                        ws.Cell(1, 17).Value = "Source: PMS, MIS";
                        ws.Cell(1, 22).Value = "Under Achievement";
                        ws.Cell(1, 22).Style.Fill.BackgroundColor = XLColor.Yellow;
                        ws.Cell(1, 26).Value = "Over Achievement";
                        ws.Cell(1, 26).Style.Fill.BackgroundColor = XLColor.Red;
                        ws.Cell(1, 29).Value = "Within +10% and -10% Range";
                        ws.Cell(1, 29).Style.Fill.BackgroundColor = XLColor.GreenRyb;
                        ws.Cell(1, 1).Style.Font.Bold = true;
                        ws.Cell(1, 2).Style.Font.Bold = true;
                        ws.Cell(1, 5).Style.Font.Bold = true;
                        ws.Cell(1, 11).Style.Font.Bold = true;
                        ws.Cell(1, 17).Style.Font.Bold = true;
                        ws.Cell(2, 1).Value = "Pplan - Participants Planned, Pach - Participants Achieved, Dplan - Days Planned, Dach - Days Achieved, Mplan - Mandays Planned, Mach - Mandays Achieved";
                        ws.Cell(4, 3).Value = "6 Years Girls Targets";
                        ws.Cell(4, 4).Value = "7-14 Yrs OOSG Targets";
                        ws.Cell(4, 5).Value = "7-14 Yrs OOSB Targets";
                        ws.Cell(4, 3).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell(4, 4).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell(4, 5).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("F4:K4").Merge();
                        ws.Range("F4:K4").Value = "GSS";
                        ws.Range("F4:K4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("F4:K4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("L4:Q4").Merge();
                        ws.Range("L4:Q4").Value = "MM";
                        ws.Range("L4:Q4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("L4:Q4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("R4:AP4").Merge();
                        ws.Range("R4:AP4").Value = "Staff Training - Enrollment and SMC";
                        ws.Range("R4:AP4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("R4:AP4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("AQ4:BO4").Merge();
                        ws.Range("AQ4:BO4").Value = "Staff Training - CMM";
                        ws.Range("AQ4:BO4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("AQ4:BO4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("BP4:CN4").Merge();
                        ws.Range("BP4:CN4").Value = "TB Training - Enrollment";
                        ws.Range("BP4:CN4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("BP4:CN4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Cell(5, 3).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell(5, 4).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Cell(5, 5).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("F5:H5").Merge();
                        ws.Range("F5:H5").Value = "CM";
                        ws.Range("F5:H5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("F5:H5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("I5:K5").Merge();
                        ws.Range("I5:K5").Value = "YTD";
                        ws.Range("I5:K5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("I5:K5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("L5:N5").Merge();
                        ws.Range("L5:N5").Value = "CM";
                        ws.Range("L5:N5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("L5:N5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("O5:Q5").Merge();
                        ws.Range("O5:Q5").Value = "YTD";
                        ws.Range("O5:Q5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("O5:Q5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("R5:AC5").Merge();
                        ws.Range("R5:AC5").Value = "CM";
                        ws.Range("R5:AC5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("R5:AC5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("AD5:AP5").Merge();
                        ws.Range("AD5:AP5").Value = "YTD";
                        ws.Range("AD5:AP5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("AD5:AP5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("AQ5:BB5").Merge();
                        ws.Range("AQ5:BB5").Value = "CM";
                        ws.Range("AQ5:BB5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("AQ5:BB5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("BC5:BO5").Merge();
                        ws.Range("BC5:BO5").Value = "YTD";
                        ws.Range("BC5:BO5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("BC5:BO5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("BP5:CA5").Merge();
                        ws.Range("BP5:CA5").Value = "CM";
                        ws.Range("BP5:CA5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("BP5:CA5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("CB5:CN5").Merge();
                        ws.Range("CB5:CN5").Value = "YTD";
                        ws.Range("CB5:CN5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("CB5:CN5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        //for (int x = 7; x < dt.Rows.Count+7; x++)
                        //{
                        //    if (Convert.ToString(dt.Rows[x][0]).Contains("_Total"))
                        //    {
                        //        ws.Cell(x,  dt.Columns.Count).Style.Font.Bold = true;


                        //    }
                        //}

                        for (int x = 7; x < dt.Rows.Count + 7; x++)
                        {
                            int[] arcols = { 8, 11, 14, 17, 20, 23, 26, 29, 32, 35, 38, 42, 45, 48, 51, 54, 57, 60, 63, 67, 70, 73, 76, 79, 82, 85, 88, 92 };
                            for (int y = 0; y < arcols.Length; y++)
                            {
                                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                                {
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) == 0)
                                {
                                    ws.Cell(x, arcols[y]).Value = "NA";
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) < 90)
                                {
                                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 90 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) <= 110)
                                {
                                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.GreenRyb;
                                }

                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 110 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) < 9997)
                                {
                                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 9997)
                                {
                                    ws.Cell(x, arcols[y]).Value = "0";
                                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
                                }
                            }
                        }
                        ws.Range(6, 1, ds.Tables[i].Rows.Count, ds.Tables[i].Columns.Count);
                        // ws.SheetView.Freeze(2, 2);
                        //  ws.AutoFilter.Enabled = true;
                        //ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                    }
                    if (sheetname == "Phase 3 Activities")
                    {
                        var blueRow = ws.Row(1);
                        blueRow.InsertRowsAbove(5);
                        ws.Range(1, 1, 5, ds.Tables[i].Columns.Count).Style.Fill.BackgroundColor = XLColor.White;
                        ws.Range(3, 1, 5, ds.Tables[i].Columns.Count).Style.Font.Bold = true;
                        ws.Cell(1, 1).Value = "PMS Data";
                        ws.Cell(1, 2).Value = "Date";
                        ws.Cell(1, 3).Value = DateTime.Now.ToString("dd/MM/yyyy");
                        ws.Cell(1, 8).Value = SMonth;
                        if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
                        {
                            ws.Cell(1, 14).Value = "Data Range Date (YTD): 1st April - 31th " + ddlMonth.SelectedItem.Text + "";
                        }
                        else
                        {
                            ws.Cell(1, 14).Value = "Data Range Date (YTD): 1st April - 25th " + ddlMonth.SelectedItem.Text + "";
                        }
                        ws.Cell(1, 21).Value = "Source: PMS, MIS";
                        ws.Cell(1, 24).Value = "Under Achievement";
                        ws.Cell(1, 25).Value = "Over Achievement";
                        ws.Cell(1, 26).Value = "Within +10% and -10% Range";
                        ws.Cell(1, 24).Style.Fill.BackgroundColor = XLColor.Yellow;
                        ws.Cell(1, 25).Style.Fill.BackgroundColor = XLColor.Red;

                        ws.Cell(1, 26).Style.Fill.BackgroundColor = XLColor.GreenRyb;
                        ws.Cell(1, 24).Style.Font.Bold = true;
                        ws.Cell(1, 2).Style.Font.Bold = true;
                        ws.Cell(1, 8).Style.Font.Bold = true;
                        ws.Cell(1, 14).Style.Font.Bold = true;
                        ws.Cell(1, 21).Style.Font.Bold = true;
                        ws.Cell(2, 1).Value = "Pplan - Participants Planned, Pach - Participants Achieved, Dplan - Days Planned, Dach - Days Achieved, Mplan - Mandays Planned, Mach - Mandays Achieved";
                        ws.Range("C4:AA4").Merge();
                        ws.Range("C4:AA4").Value = "TB PRI Meeting/Training";
                        ws.Range("C4:AA4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("C4:AA4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("C5:N5").Merge();
                        ws.Range("C5:N5").Value = "CM";
                        ws.Range("C5:N5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("C5:N5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("O5:AA5").Merge();
                        ws.Range("O5:AA5").Value = "YTD";
                        ws.Range("O5:AA5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("O5:AA5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        for (int x = 7; x < dt.Rows.Count + 7; x++)
                        {
                            int[] arcols = { 5, 8, 11, 14, 17, 20, 23, 27 };
                            for (int y = 0; y < arcols.Length; y++)
                            {
                                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "NA")
                                {
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) == 0)
                                {
                                    ws.Cell(x, arcols[y]).Value = "NA";
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) < 90)
                                {
                                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 90 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) <= 110)
                                {
                                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.GreenRyb;
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 110 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) < 9997)
                                {
                                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 9997)
                                {
                                    ws.Cell(x, arcols[y]).Value = "0";
                                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
                                }
                            }
                        }
                        ws.Range(6, 1, ds.Tables[i].Rows.Count, ds.Tables[i].Columns.Count);
                        // ws.SheetView.Freeze(2, 2);
                        //ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                    }
                    if (sheetname == "District Wise Retention")
                    {
                        var blueRow = ws.Row(1);
                        blueRow.InsertRowsAbove(5);
                        ws.Range(1, 1, 5, ds.Tables[i].Columns.Count).Style.Fill.BackgroundColor = XLColor.White;
                        ws.Range(3, 1, 5, ds.Tables[i].Columns.Count).Style.Font.Bold = true;
                        ws.Cell(1, 1).Value = "PMS Data";
                        ws.Cell(1, 2).Value = "Date";
                        ws.Cell(1, 3).Value = DateTime.Now.ToString("dd/MM/yyyy");
                        ws.Cell(1, 5).Value = SMonth;
                        if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
                        {
                            ws.Cell(1, 11).Value = "Data Range Date (YTD): 1st April - 31th " + ddlMonth.SelectedItem.Text + "";
                        }
                        else
                        {
                            ws.Cell(1, 11).Value = "Data Range Date (YTD): 1st April - 25th " + ddlMonth.SelectedItem.Text + "";
                        }
                        ws.Cell(1, 18).Value = "Source: PMS, MIS";
                        ws.Cell(1, 22).Value = "Under Achievement";
                        ws.Cell(1, 26).Value = "Over Achievement";
                        ws.Cell(1, 29).Value = "Within +10% and -10% Range";
                        ws.Cell(1, 22).Style.Fill.BackgroundColor = XLColor.Yellow;
                        ws.Cell(1, 26).Style.Fill.BackgroundColor = XLColor.Red;

                        ws.Cell(1, 29).Style.Fill.BackgroundColor = XLColor.GreenRyb;
                        ws.Cell(1, 1).Style.Font.Bold = true;
                        ws.Cell(1, 2).Style.Font.Bold = true;
                        ws.Cell(1, 5).Style.Font.Bold = true;
                        ws.Cell(1, 11).Style.Font.Bold = true;
                        ws.Cell(1, 18).Style.Font.Bold = true;
                        ws.Cell(2, 1).Value = "Pplan - Participants Planned, Pach - Participants Achieved, Dplan - Days Planned, Dach - Days Achieved, Mplan - Mandays Planned, Mach - Mandays Achieved";
                        ws.Range("C4:I4").Merge();
                        ws.Range("C4:I4").Value = "Retention Collaterals";
                        ws.Range("C4:I4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("C4:I4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("J4:AH4").Merge();
                        ws.Range("J4:AH4").Value = "Staff Training on Soft Skills";
                        ws.Range("J4:AH4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("J4:AH4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("AI4:BG4").Merge();
                        ws.Range("AI4:BG4").Value = "Staff Training on Balsabha & LSE";
                        ws.Range("AI4:BG4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("AI4:BG4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("BH4:CF4").Merge();
                        ws.Range("BH4:CF4").Value = "TB Training Balsabha & LSE";
                        ws.Range("BH4:CF4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("BH4:CF4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("CG4:DE4").Merge();
                        ws.Range("CG4:DE4").Value = "TB Training (VE)";
                        ws.Range("CG4:DE4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("CG4:DE4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("C5:E5").Merge();
                        ws.Range("C5:E5").Value = "CM";
                        ws.Range("C5:E5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("C5:E5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("F5:I5").Merge();
                        ws.Range("F5:I5").Value = "YTD";
                        ws.Range("F5:I5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("F5:I5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("J5:U5").Merge();
                        ws.Range("J5:U5").Value = "CM";
                        ws.Range("J5:U5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("J5:U5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("V5:AH5").Merge();
                        ws.Range("V5:AH5").Value = "YTD";
                        ws.Range("V5:AH5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("V5:AH5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("AI5:AT5").Merge();
                        ws.Range("AI5:AT5").Value = "CM";
                        ws.Range("AI5:AT5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("AI5:AT5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("AU5:BG5").Merge();
                        ws.Range("AU5:BG5").Value = "YTD";
                        ws.Range("AU5:BG5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("AU5:BG5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("BH5:BS5").Merge();
                        ws.Range("BH5:BS5").Value = "CM";
                        ws.Range("BH5:BS5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("BH5:BS5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("BT5:CF5").Merge();
                        ws.Range("BT5:CF5").Value = "YTD";
                        ws.Range("BT5:CF5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("BT5:CF5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("CG5:CR5").Merge();
                        ws.Range("CG5:CR5").Value = "CM";
                        ws.Range("CG5:CR5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("CG5:CR5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("CS5:DE5").Merge();
                        ws.Range("CS5:DE5").Value = "YTD";
                        ws.Range("CS5:DE5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("CS5:DE5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        for (int x = 7; x < dt.Rows.Count + 7; x++)
                        {
                            //int[] arcols = { 5, 9, 12, 15, 18, 21, 24, 27,30,34,37,40,43,46,49,52,55,59,62,65,68,71,74,77,80,84,87,90,96,99,102,105,109 };
                            int[] arcols = { 5, 9, 12, 15, 18, 21, 24, 27, 30, 34, 37, 40, 43, 46, 49, 52, 55, 62, 65, 68, 74, 77, 80, 87, 90, 93, 99, 102, 105 };
                            for (int y = 0; y < arcols.Length; y++)
                            {
                                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "NA")
                                {
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) == 0)
                                {
                                    ws.Cell(x, arcols[y]).Value = "NA";
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) < 90)
                                {
                                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 90 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) <= 110)
                                {
                                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.GreenRyb;
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 110 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) < 9997)
                                {
                                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 9997)
                                {
                                    ws.Cell(x, arcols[y]).Value = "0";
                                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
                                }
                            }
                        }
                        ws.Range(6, 1, ds.Tables[i].Rows.Count, ds.Tables[i].Columns.Count);
                        // ws.SheetView.Freeze(2, 2);
                        //ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                    }
                    if (sheetname == "L&D Training")
                    {
                        var blueRow = ws.Row(1);
                        blueRow.InsertRowsAbove(5);
                        ws.Range(1, 1, 5, ds.Tables[i].Columns.Count).Style.Fill.BackgroundColor = XLColor.White;
                        ws.Range(3, 1, 5, ds.Tables[i].Columns.Count).Style.Font.Bold = true;
                        ws.Cell(1, 1).Value = "PMS Data";
                        ws.Cell(1, 2).Value = "Date";
                        ws.Cell(1, 3).Value = DateTime.Now.ToString("dd/MM/yyyy");
                        ws.Cell(1, 5).Value = SMonth;
                        if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
                        {
                            ws.Cell(1, 11).Value = "Data Range Date (YTD): 1st April - 31th " + ddlMonth.SelectedItem.Text + "";
                        }
                        else
                        {
                            ws.Cell(1, 11).Value = "Data Range Date (YTD): 1st April - 25th " + ddlMonth.SelectedItem.Text + "";
                        }
                        ws.Cell(1, 18).Value = "Source: PMS, MIS";
                        ws.Cell(1, 22).Value = "Under Achievement";
                        ws.Cell(1, 26).Value = "Over Achievement";
                        ws.Cell(1, 29).Value = "Within +10% and -10% Range";

                        ws.Cell(1, 22).Style.Fill.BackgroundColor = XLColor.Yellow;
                        ws.Cell(1, 26).Style.Fill.BackgroundColor = XLColor.Red;

                        ws.Cell(1, 29).Style.Fill.BackgroundColor = XLColor.GreenRyb;

                        ws.Cell(1, 1).Style.Font.Bold = true;
                        ws.Cell(1, 2).Style.Font.Bold = true;
                        ws.Cell(1, 5).Style.Font.Bold = true;
                        ws.Cell(1, 11).Style.Font.Bold = true;
                        ws.Cell(1, 18).Style.Font.Bold = true;
                        ws.Cell(2, 1).Value = "Pplan - Participants Planned, Pach - Participants Achieved, Dplan - Days Planned, Dach - Days Achieved, Mplan - Mandays Planned, Mach - Mandays Achieved";
                        ws.Range("C4:H4").Merge();
                        ws.Range("C4:H4").Value = "PoSH (Unbudgeted)";
                        ws.Range("C4:H4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("C4:H4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("I4:AG4").Merge();
                        ws.Range("I4:AG4").Value = "L& D Training - Functional Training - Finance";
                        ws.Range("I4:AG4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("I4:AG4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("AH4:BF4").Merge();
                        ws.Range("AH4:BF4").Value = "L& D Training - Functional Training - IT";
                        ws.Range("AH4:BF4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("AH4:BF4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("BG4:BS4").Merge();
                        ws.Range("BG4:BS4").Value = "L & D Training - District Induction (Booked in HO)";
                        ws.Range("BG4:BS4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("BG4:BS4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("BT4:CF4").Merge();
                        ws.Range("BT4:CF4").Value = "L & D Training -HO Induction";
                        ws.Range("BT4:CF4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("BT4:CF4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("CG4:CS4").Merge();
                        ws.Range("CG4:CS4").Value = "L & D Training - Train the Trainer (Booked in HO)";
                        ws.Range("CG4:CS4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("CG4:CS4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("C5:E5").Merge();
                        ws.Range("C5:E5").Value = "CM";
                        ws.Range("C5:E5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("C5:E5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("F5:H5").Merge();
                        ws.Range("F5:H5").Value = "YTD";
                        ws.Range("F5:H5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("F5:H5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("I5:T5").Merge();
                        ws.Range("I5:T5").Value = "CM";
                        ws.Range("I5:T5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("I5:T5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("U5:AG5").Merge();
                        ws.Range("U5:AG5").Value = "YTD";
                        ws.Range("U5:AG5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("U5:AG5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("AH5:AS5").Merge();
                        ws.Range("AH5:AS5").Value = "CM";
                        ws.Range("AH5:AS5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("AH5:AS5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("AT5:BF5").Merge();
                        ws.Range("AT5:BF5").Value = "YTD";
                        ws.Range("AT5:BF5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("AT5:BF5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("BG5:BL5").Merge();
                        ws.Range("BG5:BL5").Value = "CM";
                        ws.Range("BG5:BL5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("BG5:BL5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("BM5:BS5").Merge();
                        ws.Range("BM5:BS5").Value = "YTD";
                        ws.Range("BM5:BS5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("BM5:BS5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("BT5:BY5").Merge();
                        ws.Range("BT5:BY5").Value = "CM";
                        ws.Range("BT5:BY5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("BT5:BY5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("BZ5:CF5").Merge();
                        ws.Range("BZ5:CF5").Value = "YTD";
                        ws.Range("BZ5:CF5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("BZ5:CF5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("CG5:CL5").Merge();
                        ws.Range("CG5:CL5").Value = "CM";
                        ws.Range("CG5:CL5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("CG5:CL5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("CM5:CS5").Merge();
                        ws.Range("CM5:CS5").Value = "YTD";
                        ws.Range("CM5:CS5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("CM5:CS5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        for (int x = 7; x < dt.Rows.Count + 7; x++)
                        {
                            // int[] arcols = { 5, 8, 11, 14, 17, 20, 23, 26, 29, 33, 36, 39, 42, 45, 48, 51, 54, 58, 61, 64, 67, 71, 74, 77, 80, 84, 87, 90, 93, 97 };
                            int[] arcols = { 5, 8, 11, 14, 17, 20, 23, 26, 29, 36, 39, 42, 48, 51, 54, 61, 67, 74, 80, 87, 93 };
                            for (int y = 0; y < arcols.Length; y++)
                            {
                                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "NA")
                                {
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) == 0)
                                {
                                    ws.Cell(x, arcols[y]).Value = "NA";
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) < 90)
                                {
                                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 90 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) <= 110)
                                {
                                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.GreenRyb;
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 110 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) < 9997)
                                {
                                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 9997)
                                {
                                    ws.Cell(x, arcols[y]).Value = "0";
                                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
                                }
                            }
                        }
                        ws.Range(6, 1, ds.Tables[i].Rows.Count, ds.Tables[i].Columns.Count);
                        /// ws.SheetView.Freeze(2, 2);
                        //ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                    }
                    if (sheetname == "Impact Assessment Cost")
                    {
                        var blueRow = ws.Row(1);
                        blueRow.InsertRowsAbove(5);
                        ws.Range(1, 1, 5, ds.Tables[i].Columns.Count).Style.Fill.BackgroundColor = XLColor.White;
                        ws.Range(3, 1, 5, ds.Tables[i].Columns.Count).Style.Font.Bold = true;
                        ws.Cell(1, 1).Value = "PMS Data";
                        ws.Cell(1, 2).Value = "Date";
                        ws.Cell(1, 3).Value = DateTime.Now.ToString("dd/MM/yyyy");
                        ws.Cell(1, 9).Value = SMonth;
                        if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
                        {
                            ws.Cell(1, 15).Value = "Data Range Date (YTD): 1st April - 31th " + ddlMonth.SelectedItem.Text + " ";
                        }
                        else
                        {
                            ws.Cell(1, 15).Value = "Data Range Date (YTD): 1st April - 25th " + ddlMonth.SelectedItem.Text + " ";
                        }
                        ws.Cell(1, 24).Value = "Source: PMS, MIS";
                        ws.Cell(1, 26).Value = "Under Achievement";
                        ws.Cell(1, 28).Value = "Over Achievement";
                        ws.Cell(1, 33).Value = "Within +10% and -10% Range";

                        ws.Cell(1, 26).Style.Fill.BackgroundColor = XLColor.Yellow;
                        ws.Cell(1, 28).Style.Fill.BackgroundColor = XLColor.Red;

                        ws.Cell(1, 33).Style.Fill.BackgroundColor = XLColor.GreenRyb;

                        ws.Cell(1, 1).Style.Font.Bold = true;
                        ws.Cell(1, 2).Style.Font.Bold = true;
                        ws.Cell(1, 9).Style.Font.Bold = true;
                        ws.Cell(1, 15).Style.Font.Bold = true;
                        ws.Cell(1, 24).Style.Font.Bold = true;
                        ws.Cell(2, 1).Value = "Pplan - Participants Planned, Pach - Participants Achieved, Dplan - Days Planned, Dach - Days Achieved, Mplan - Mandays Planned, Mach - Mandays Achieved";
                        ws.Range("C4:AA4").Merge();
                        ws.Range("C4:AA4").Value = "PMS Training";
                        ws.Range("C4:AA4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("C4:AA4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("AB4:AZ4").Merge();
                        ws.Range("AB4:AZ4").Value = "Staff Training on CV";
                        ws.Range("AB4:AZ4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("AB4:AZ4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("BA4:BG4").Merge();
                        ws.Range("BA4:BG4").Value = "PMS Mobiles + Survey CTO Mobile";
                        ws.Range("BA4:BG4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("BA4:BG4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("C5:N5").Merge();
                        ws.Range("C5:N5").Value = "CM";
                        ws.Range("C5:N5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("C5:N5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("O5:AA5").Merge();
                        ws.Range("O5:AA5").Value = "YTD";
                        ws.Range("O5:AA5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("O5:AA5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("AB5:AM5").Merge();
                        ws.Range("AB5:AM5").Value = "CM";
                        ws.Range("AB5:AM5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("AB5:AM5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("AN5:AZ5").Merge();
                        ws.Range("AN5:AZ5").Value = "YTD";
                        ws.Range("AN5:AZ5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("AN5:AZ5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("BA5:BC5").Merge();
                        ws.Range("BA5:BC5").Value = "CM";
                        ws.Range("BA5:BC5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("BA5:BC5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("BD5:BG5").Merge();
                        ws.Range("BD5:BG5").Value = "YTD";
                        ws.Range("BD5:BG5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("BD5:BG5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        for (int x = 7; x < dt.Rows.Count + 7; x++)
                        {
                            // int[] arcols = { 5, 8, 11, 14, 17, 20, 23, 27,30,33,36,39,42,45,48,52,55,59 };
                            int[] arcols = { 5, 8, 11, 14, 17, 20, 23, 27, 30, 33, 36, 42, 45, 48 };
                            for (int y = 0; y < arcols.Length; y++)
                            {
                                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "NA")
                                {
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) == 0)
                                {
                                    ws.Cell(x, arcols[y]).Value = "NA";
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) < 90)
                                {
                                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 90 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) <= 110)
                                {
                                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.GreenRyb;
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 110 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) < 9997)
                                {
                                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 9997)
                                {
                                    ws.Cell(x, arcols[y]).Value = "0";
                                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
                                }
                            }
                        }
                        ws.Range(6, 1, ds.Tables[i].Rows.Count, ds.Tables[i].Columns.Count);
                        //  ws.SheetView.Freeze(2, 2);
                        //ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                    }
                    if (sheetname == "District Wise Learning")
                    {
                        var blueRow = ws.Row(1);
                        blueRow.InsertRowsAbove(5);
                        ws.Range(1, 1, 5, ds.Tables[i].Columns.Count).Style.Fill.BackgroundColor = XLColor.White;
                        ws.Range(3, 1, 5, ds.Tables[i].Columns.Count).Style.Font.Bold = true;
                        ws.Cell(1, 1).Value = "PMS Data";
                        ws.Cell(1, 2).Value = "Date";
                        ws.Cell(1, 3).Value = DateTime.Now.ToString("dd/MM/yyyy");
                        ws.Cell(1, 9).Value = SMonth;
                        if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
                        {
                            ws.Cell(1, 15).Value = "Data Range Date (YTD): 1st April - 31th " + ddlMonth.SelectedItem.Text + "";
                        }
                        else
                        {
                            ws.Cell(1, 15).Value = "Data Range Date (YTD): 1st April - 25th " + ddlMonth.SelectedItem.Text + "";
                        }
                        ws.Cell(1, 24).Value = "Source: PMS, MIS";
                        ws.Cell(1, 26).Value = "Under Achievement";
                        ws.Cell(1, 28).Value = "Over Achievement";
                        ws.Cell(1, 33).Value = "Within +10% and -10% Range";

                        ws.Cell(1, 26).Style.Fill.BackgroundColor = XLColor.Yellow;
                        ws.Cell(1, 28).Style.Fill.BackgroundColor = XLColor.Red;

                        ws.Cell(1, 33).Style.Fill.BackgroundColor = XLColor.GreenRyb;


                        ws.Cell(1, 1).Style.Font.Bold = true;
                        ws.Cell(1, 2).Style.Font.Bold = true;
                        ws.Cell(1, 9).Style.Font.Bold = true;
                        ws.Cell(1, 15).Style.Font.Bold = true;
                        ws.Cell(1, 24).Style.Font.Bold = true;
                        ws.Cell(2, 1).Value = "Pplan - Participants Planned, Pach - Participants Achieved, Dplan - Days Planned, Dach - Days Achieved, Mplan - Mandays Planned, Mach - Mandays Achieved";
                        ws.Range("C4:U4").Merge();
                        ws.Range("C4:U4").Value = "Core Group Training";
                        ws.Range("C4:U4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("C4:U4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("V4:AN4").Merge();
                        ws.Range("V4:AN4").Value = "Master Trainer Training (GKP)";
                        ws.Range("V4:AN4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("V4:AN4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("AO4:BM4").Merge();
                        ws.Range("AO4:BM4").Value = "L0 Staff Training";
                        ws.Range("AO4:BM4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("AO4:BM4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("BN4:CL4").Merge();
                        ws.Range("BN4:CL4").Value = "L1 Staff Training";
                        ws.Range("BN4:CL4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("BN4:CL4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("CN4:DK4").Merge();
                        ws.Range("CN4:DK4").Value = "L2 Staff Training";
                        ws.Range("CN4:DK4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("CN4:DK4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("DL4:EJ4").Merge();
                        ws.Range("DL4:EJ4").Value = "L3 Staff Training";
                        ws.Range("DL4:EJ4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("DL4:EJ4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("EK4:FC4").Merge();
                        ws.Range("EK4:FC4").Value = "Learning Baseline Staff Training";
                        ws.Range("EK4:FC4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("EK4:FC4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("FD4:FV4").Merge();
                        ws.Range("FD4:FV4").Value = "TB Training - L0";
                        ws.Range("FD4:FV4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("FD4:FV4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("FW4:GU4").Merge();
                        ws.Range("FW4:GU4").Value = "TB Training - L1";
                        ws.Range("FW4:GU4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("FW4:GU4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("GV4:HT4").Merge();
                        ws.Range("GV4:HT4").Value = "TB Training - L2";
                        ws.Range("GV4:HT4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("GV4:HT4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("HU4:IS4").Merge();
                        ws.Range("HU4:IS4").Value = "TB Training - L3";
                        ws.Range("HU4:IS4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("HU4:IS4").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("C5:K5").Merge();
                        ws.Range("C5:K5").Value = "CM";
                        ws.Range("C5:K5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("C5:K5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("L5:U5").Merge();
                        ws.Range("L5:U5").Value = "YTD";
                        ws.Range("L5:U5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("L5:U5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("V5:AD5").Merge();
                        ws.Range("V5:AD5").Value = "CM";
                        ws.Range("V5:AD5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("V5:AD5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("AE5:AN5").Merge();
                        ws.Range("AE5:AN5").Value = "YTD";
                        ws.Range("AE5:AN5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("AE5:AN5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("AO5:AZ5").Merge();
                        ws.Range("AO5:AZ5").Value = "CM";
                        ws.Range("AO5:AZ5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("AO5:AZ5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("BA5:BM5").Merge();
                        ws.Range("BA5:BM5").Value = "YTD";
                        ws.Range("BA5:BM5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("BA5:BM5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("BN5:BY5").Merge();
                        ws.Range("BN5:BY5").Value = "CM";
                        ws.Range("BN5:BY5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("BN5:BY5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("BZ5:CL5").Merge();
                        ws.Range("BZ5:CL5").Value = "YTD";
                        ws.Range("BZ5:CL5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("BZ5:CL5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("CM5:CX5").Merge();
                        ws.Range("CM5:CX5").Value = "CM";
                        ws.Range("CM5:CX5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("CM5:CX5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("CY5:DK5").Merge();
                        ws.Range("CY5:DK5").Value = "YTD";
                        ws.Range("CY5:DK5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("CY5:DK5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("DL5:DW5").Merge();
                        ws.Range("DL5:DW5").Value = "CM";
                        ws.Range("DL5:DW5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("DL5:DW5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("DX5:EJ5").Merge();
                        ws.Range("DX5:EJ5").Value = "YTD";
                        ws.Range("DX5:EJ5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("DX5:EJ5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("EK5:ES5").Merge();
                        ws.Range("EK5:ES5").Value = "CM";
                        ws.Range("EK5:ES5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("EK5:ES5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("ET5:FC5").Merge();
                        ws.Range("ET5:FC5").Value = "YTD";
                        ws.Range("ET5:FC5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("ET5:FC5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("FD5:FL5").Merge();
                        ws.Range("FD5:FL5").Value = "CM";
                        ws.Range("FD5:FL5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("FD5:FL5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("FM5:FV5").Merge();
                        ws.Range("FM5:FV5").Value = "YTD";
                        ws.Range("FM5:FV5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("FM5:FV5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("FW5:GH5").Merge();
                        ws.Range("FW5:GH5").Value = "CM";
                        ws.Range("FW5:GH5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("FW5:GH5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("GI5:GU5").Merge();
                        ws.Range("GI5:GU5").Value = "YTD";
                        ws.Range("GI5:GU5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("GI5:GU5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("GV5:HG5").Merge();
                        ws.Range("GV5:HG5").Value = "CM";
                        ws.Range("GV5:HG5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("GV5:HG5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("HH5:HT5").Merge();
                        ws.Range("HH5:HT5").Value = "YTD";
                        ws.Range("HH5:HT5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("HH5:TH5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("HU5:IF5").Merge();
                        ws.Range("HU5:IF5").Value = "CM";
                        ws.Range("HU5:IF5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("HU5:IF5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("IG5:IS5").Merge();
                        ws.Range("IG5:IS5").Value = "YTD";
                        ws.Range("IG5:IS5").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("IG5:IS5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        for (int x = 7; x < dt.Rows.Count + 7; x++)
                        {
                            int[] arcols = { 5, 8, 11, 14, 17, 20, 24, 27, 33, 36, 43, 46, 49, 55, 58, 61, 68, 71, 74, 80, 83, 68, 93, 96, 99, 105, 108, 111, 118, 121, 124, 130, 133, 136, 143, 146, 152, 155, 162, 165, 171, 174, 181, 184, 187, 193, 196, 199, 206, 209, 212, 218, 221, 224, 231, 234, 237, 243, 246, 249 };
                            //int[] arcols = { 5, 8, 11, 14, 17, 21, 24, 27,30,33,36,40,43,46,49,52,55,58,61,65,68,71,74,77,80,83,86,90,93,96,99,102,105,108,111,115,118,121,124,127,130,133,136,140,143,146,149,152,155,159,162,165,168,171,174,178,181,184,187,190,193,196,199,203,206,209,212,215,218,221,224,228,231,234,237,240,243,246,249,253 };
                            for (int y = 0; y < arcols.Length; y++)
                            {
                                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "NA")
                                {
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) == 0)
                                {
                                    ws.Cell(x, arcols[y]).Value = "NA";
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) < 90)
                                {
                                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 90 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) <= 110)
                                {
                                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.GreenRyb;
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 110 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) < 9997)
                                {
                                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                                }
                                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 9997)
                                {
                                    ws.Cell(x, arcols[y]).Value = "0";
                                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
                                }
                            }
                        }
                        ws.Range(6, 1, ds.Tables[i].Rows.Count, ds.Tables[i].Columns.Count);
                        //ws.SheetView.Freeze(2, 2);
                        //ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                    }
                    if (sheetname == "AGP Activities")
                    {

                        var blueRow = ws.Row(1);
                        blueRow.InsertRowsAbove(3);
                        ws.Range(1, 1, 3, ds.Tables[i].Columns.Count).Style.Fill.BackgroundColor = XLColor.White;
                        ws.Range("A2:A3").Merge();
                        ws.Range("A2:A3").Value = "";
                        ws.Range("A2:A3").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("A2:A3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        ws.Range("B2:M2").Merge();
                        ws.Range("B2:M2").Value = "AGP";
                        ws.Range("B2:M2").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("B2:M2").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("B3:G3").Merge();
                        ws.Range("B3:G3").Value = "CM";
                        ws.Range("B3:G3").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("B3:G3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        ws.Range("H3:M3").Merge();
                        ws.Range("H3:M3").Value = "YTD";
                        ws.Range("H3:M3").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        ws.Range("H3:M3").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


                        //for (int x = 5; x < dt.Rows.Count + 5; x++)
                        //{
                        //    // int[] arcols = { 5, 8, 11, 14, 17, 20, 23, 27,30,33,36,39,42,45,48,52,55,59 };
                        //    int[] arcols = { 4, 7, 10, 14 };
                        //    for (int y = 0; y < arcols.Length; y++)
                        //    {
                        //        if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) < 90)
                        //        {
                        //            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
                        //        }
                        //        else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 90 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) <= 110)
                        //        {
                        //            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.GreenRyb;
                        //        }
                        //        else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 110)
                        //        {
                        //            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                        //        }
                        //    }
                        //}


                        ws.Range(4, 1, ds.Tables[i].Rows.Count, ds.Tables[i].Columns.Count);
                        //ws.SheetView.Freeze(2, 2);
                        //ws.Tables.FirstOrDefault().ShowAutoFilter = false;
                    }

                }



            }
            string filename = "MIS_Report" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + ".xlsx";


            Export_TO_Excel(wb, filename);
        }
        catch
        {

            throw;
        }
    }

    protected void btnCSV_Click(object sender, EventArgs e)
    {

    }
    #endregion
    #region *************OnSelectedIndexChanged Evets*************************
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
            }
            foreach (ListItem item in ChkState.Items)
            {

                item.Selected = true;

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
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
    }
    #endregion
    #region *********** Grid view Events ********************
    public void GV_DynamicGrid_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }
    #endregion

    #region ********************************** Export Execel ***********************************
    public void Export_TO_Excel(XLWorkbook wb, string filename)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                //Response.End();
                Response.SuppressContent = true;
                ApplicationInstance.CompleteRequest();
            }
        }
        catch
        {
        }
    }
    #endregion
}
