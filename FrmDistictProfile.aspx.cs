using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class FrmDistictProfile : System.Web.UI.Page
{

     clsMain objMain = new clsMain();

     Comman objComman = new Comman();
     string labelmainheading = "";
     string conditions = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                ViewState["Button"] = "AA";
                LoadYear();
                LoadUserLeavel();
                //FillCBState();
                //ddlstate.SelectedIndex = 2;
                //ddlState_SelectedIndexChanged(sender, e);
                return;
            }
            base.Response.Redirect("Login.aspx", false);
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
                       new SqlParameter("@Year",  ddlYear.SelectedValue),
               };
            DataTable dtAllState = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptLoadAllState", par1);
            objComman.BindDLLDatatable("mst1State", dtAllState, "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlstate, "StateName", "StateCode", "--Select--");

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
            objComman.BindDLLDatatable("mst1State", dtAllState, "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlstate, "StateName", "StateCode", "--Select--");

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
            objComman.BindDLLDatatable("mst1State", dtAllState, "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlstate, "StateName", "StateCode", "--Select--");


        }

    }
    public void LoadUserLeavel()
    {
        AlllStateCode();
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlstate, "StateName", "StateCode", "--Select--");
            ddlstate.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
           // conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";

            //conditions = "UserName='" + Convert.ToString(Session["username"]) + "' ";
            //string strQry1 = "  SELECT distinct mst1State.StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode inner join mst1State on mst1State.StateCode=mst2District.StateCode where   " + conditions + "  order by StateName   ";
            //DataTable dtState = objMain.LoadData(strQry1);

            //     objComman.BindDLLMasterTable("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", dtState, "", "StateName", "", ddlstate, "StateName", "StateCode", "Select");

            ddlstate.SelectedIndex = 1;
            ddlstate.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else
        {
            //conditions = "StateCode='" + Convert.ToString(Session["StateCode"]) + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlstate, "StateName", "StateCode", "--Select--");

            ddlstate.SelectedIndex = 1;
            ddlstate.Enabled = false;
            ddlDistrict.Enabled = false;
        }


        if (Convert.ToString(Session["user_level_Role"]) == "1")
        {
        }
        else if (Convert.ToString(Session["user_level_Role"]) == "2")
        {
            conditions = "";
            conditions = "UserName='" + Convert.ToString(Session["username"]) + "' ";
            string strQry1 = "  SELECT distinct mst2District.DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode  where   " + conditions + " and Fyear='" + ddlYear.SelectedItem.Text + "'  order by DistrictName   ";


            conditions = "StateCode ='" + ddlstate.SelectedValue + "' and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";

            DataTable dtDistrict = objMain.LoadData(strQry1);
            objComman.BindDLLMasterTable("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", dtDistrict, "", "DistrictName", "", ddlDistrict, "DistrictName", "DistrictCode", "Select");

           // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

            ddlDistrict.SelectedIndex = 0;

            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }

        else
        {
            conditions = "";
            conditions = "StateCode ='" + ddlstate.SelectedValue + "' and DistrictCode in(" + Convert.ToString(Session["DistrictCode"]) + ") and Fyear= '" + ddlYear.SelectedItem.Text + "' ";
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

            string strQry;
            strQry = "Select * from mst2District where   DistrictCode in(" + Convert.ToString(Session["DistrictCode"]) + ")";
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
         
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
            ddlDistrict.SelectedIndex = 1;
            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

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
        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


    }
    public void FillCBState()
    {
        conditions = "";
        objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlstate, "StateName", "StateCode", "--Select--");
    }

    public void FillCBBock()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'";
        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--All--");
    }

    public void FillCBDist()
    {
        DataTable dtDistrict = null;

        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "StateCode in('" + ddlstate.SelectedValue + "') and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = " mst2District.StateCode in('" + ddlstate.SelectedValue + "') and UserName='" + Session["username"].ToString() + "' ";
        }
        else
        {
            conditions = "StateCode  in('" + ddlstate.SelectedValue + "')  and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";


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
            strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions + "  and  Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName  ";
            dtDistrict = objMain.LoadData(strQry1);
        }
        else
        {
            string strQry = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where " + conditions + "  order by DistrictName   ";
            dtDistrict = objMain.LoadData(strQry);
        }

        // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");
        objComman.BindDLLMasterTable("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", dtDistrict, "", "DistrictName", "", ddlDistrict, "DistrictName", "DistrictCode", "Select");

       

        //conditions = "";
        //conditions = "StateCode ='" + ddlstate.SelectedValue + "' and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        //objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--All--");



    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            AlllStateCode();
            ddlstate.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlDistrict, null);

            if (Session["user_level_Role"].ToString() == "3" || Session["user_level_Role"].ToString() == "4")
            {
                ddlDistrict_SelectedIndexChanged(ddlDistrict, null);

                ddlDistrict.SelectedIndex = 1;

            }
             //ddlDistrict_SelectedIndexChanged(ddlDistrict, null);

             //ddlDistrict.SelectedIndex = 1;
          
        }
        else
        {
            ddlstate.SelectedIndex = 0;
            ddlDistrict.Items.Clear();
            ddlBlock.Items.Clear();
          
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["Button"] = " ";
        FillCBDist();
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["Button"] = " ";
        FillCBBock();
    }

    public void getreport()
    {
        conditions = "";

        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + "  where  mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlstate.SelectedIndex > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode = '" + ddlstate.SelectedValue + "' ";
        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";
        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode = '" + ddlBlock.SelectedValue + "' ";
        }
        SqlParameter[] cmdParameters = new SqlParameter[]
		{
			new SqlParameter("@Cond", conditions),
			new SqlParameter("@flag", ddlType.SelectedValue)
		};
        DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptDistProfile]", cmdParameters);
        lblTotalCount.Text = dataTable.Rows.Count.ToString();
        ViewState["dt"] = dataTable;
        if (dataTable.Rows.Count > 0)
        {
            DGV_Report.DataSource = dataTable;
            DGV_Report.DataBind();
            return;
        }
        DGV_Report.DataSource = null;
        DGV_Report.DataBind();
    }

    protected void PMSBaseline_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = true;
        rptD2D.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView1.Visible = false;
        ViewState["Button"] = "Baseline";
        btnexcel.Visible = true;
        if (ddlType.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, base.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Type')</script>", false);
        }
        labelmainheading = "Baseline Level Report";
    }

    protected void PMS_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = true;
        rptD2D.Visible = false;
        ViewState["Button"] = "PMS";
        btnexcel.Visible = true;
        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView1.Visible = false;
        if (ddlType.SelectedIndex > 0)
        {
            getreport();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, base.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Type')</script>", false);
        }
        labelmainheading = "Learning Level Report";
    }

    protected void Report2_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        rptD2D.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView1.Visible = true;
        btnexcel.Visible = true;
        ViewState["Button"] = "Report2";
        labelmainheading = "";
    }

    protected void excel(DataTable dtexcel)
    {
        string rptnm = "EG_Report_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".xls";
        StringBuilder sbb = new StringBuilder();
        ExportToExcel exportToExcel = new ExportToExcel();
        exportToExcel.ExporttoExcel(dtexcel, sbb, rptnm);
    }

    public void Export_To_Excel(object sender, EventArgs e)
    {
        DataTable table = ViewState["dt"] as DataTable;
        ExporttoExcel(DGV_Report, table, "DistictProfile");
    }

    private void ExporttoExcel(GridView Gv, DataTable table, string FileName)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">");
        string str = FileName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + str + " ");
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("windows-1250");
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
        int count = Gv.HeaderRow.Cells.Count;
        for (int i = 0; i < count; i++)
        {
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(Gv.HeaderRow.Cells[i].Text);
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");
        }
        HttpContext.Current.Response.Write("</TR>");
        foreach (DataRow dataRow in table.Rows)
        {
            HttpContext.Current.Response.Write("<TR>");
            for (int j = 0; j < table.Columns.Count; j++)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write(dataRow[j].ToString());
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");
        }
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }

 
    private void ExporttoExcel(GridView Gv, DataTable table)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("windows-1250");
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
        int count = Gv.HeaderRow.Cells.Count;
        for (int i = 0; i < count; i++)
        {
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(Gv.HeaderRow.Cells[i].Text);
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");
        }
        HttpContext.Current.Response.Write("</TR>");
        foreach (DataRow dataRow in table.Rows)
        {
            HttpContext.Current.Response.Write("<TR>");
            for (int j = 0; j < table.Columns.Count; j++)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write(dataRow[j].ToString());
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