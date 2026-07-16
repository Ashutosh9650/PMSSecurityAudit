using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmPhaseMapping : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    string conditions = "";
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    public bool edit_status = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                LoadYear();
                LoadUserLeavel();
                ViewState["1"] = "ss";
                UserLevelFilter();
            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }

        }

    }

    public void UserLevelFilter()
    {
        string strQry = "";
        string Cond = "Module='Enroll'";
        strQry = "Select * from MstUserRight  where " + Cond + " and Role_Id=" + Session["user_level"].ToString() + "   ";
        DataTable dtRole = objMain.LoadData(strQry);
        if (dtRole.Rows.Count > 0)
        {
            vADD = Convert.ToBoolean(dtRole.Rows[0]["AddStatus"].ToString());
            vVerify = Convert.ToBoolean(dtRole.Rows[0]["verify_Status"].ToString());
            vDelete = Convert.ToBoolean(dtRole.Rows[0]["Delete_status"].ToString());
            edit_status = Convert.ToBoolean(dtRole.Rows[0]["edit_status"].ToString());
            ViewState["vADD"] = vADD;
            ViewState["vVerify"] = vVerify;
            ViewState["vDelete"] = vDelete;
            ViewState["edit_status"] = edit_status;
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
        dtYear = Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, "", "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


    }
    public DataTable Generate_Financial_Year()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("Type");
        DataRow dr;
        int stYr = DateTime.Today.Month < 4 ? DateTime.Today.Year + 1 : DateTime.Today.Year + 1;
        for (int i = stYr; i > 2016; i--)
        {
            dr = dt.NewRow();
            dr[0] = (i - 1).ToString();
            dr[1] = (i - 1).ToString() + "-" + (i).ToString();
            dt.Rows.Add(dr);
        }
        dt.AcceptChanges();
        return dt;
    }
    public void LoadUserLeavel()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            ddlState.SelectedIndex = 1;
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            ddlState.SelectedIndex = 1;
            ddlState.Enabled = false;
            ddlDistrict.Enabled = false;
        }


        if (Session["user_level_Role"].ToString() == "1")
        {
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "";
            conditions = "StateCode ='" + ddlState.SelectedValue + "'  and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--All--");

            ddlDistrict.SelectedIndex = 0;

            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }

        else
        {
            conditions = "";
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and Fyear= '" + ddlYear.SelectedItem.Text + "' ";
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--ALL--");
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
            ddlDistrict.SelectedIndex = 1;

        }





    }
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
            conditions = "StateCode ='" + ddlState.SelectedValue + "'  and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else
        {
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";


        }


        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "All");



    }
    public void LoadData()
    {
        string strQry = "";
        conditions = "";
        if (ddlState.SelectedIndex > 0)
        {
            conditions = " and  a.StateCode='" + ddlState.SelectedValue.ToString() + "'";
        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions = conditions + " and a.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "'";
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + " and a.Fyear='" + Convert.ToString(ddlYear.SelectedItem.Text) + "'";
        }


        SqlParameter[] p = new SqlParameter[]
            {

               new SqlParameter("@Con",  conditions),
                 new SqlParameter("@Flag",  1),
            };


        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_GET_Tbl_PhaseMapping", p);



        if (dt.Rows.Count > 0)
        {
            gvnroll.DataSource = dt;
            gvnroll.DataBind();
            Session["ExcelExport"] = dt;
        }
        else
        {
            gvnroll.DataSource = null;
            gvnroll.DataBind();
        }
    }

    protected void gvnroll_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPhase = (Label)e.Row.FindControl("lblPhase");
            DropDownList ddlPhase = (DropDownList)e.Row.FindControl("ddlPhase");

            if (lblPhase.Text != "")
            {
                ddlPhase.SelectedValue = lblPhase.Text;
            }
        }
    }
    #region ****** On Selected Index Changed Event
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            ddlState.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlDistrict, null);
            if (Session["user_level_Role"].ToString() == "3" || Session["user_level_Role"].ToString() == "4")
            {
                ddlDistrict.SelectedIndex = 1;
            }
        }
        else
        {
            ddlState.SelectedIndex = 0;
            ddlDistrict.Items.Clear();

        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
    }
    #endregion
    #region ****** Button Click Event *****
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        LoadData();


    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string Flag = "";
        string UniqueID = "";
        int Ret = 0;
        for (int i = 0; i < gvnroll.Rows.Count; i++)
        {

            Label lblPM_GUID = (Label)gvnroll.Rows[i].FindControl("lblPM_GUID");
            if (lblPM_GUID.Text != "" && lblPM_GUID.Text != null)
            {

                Flag = "U";
                UniqueID = Convert.ToString(lblPM_GUID.Text);
            }
            else
            {
                Flag = "I";

                UniqueID = objComman.Generate_RandomStringAnu(8);
            }
            Label lblStateCode = (Label)gvnroll.Rows[i].FindControl("lblStateCode");
            Label lblDistrictCode = (Label)gvnroll.Rows[i].FindControl("lblDistrictCode");
            TextBox TxtRegion = (TextBox)gvnroll.Rows[i].FindControl("TxtRegion");
            DropDownList ddlPhase = (DropDownList)gvnroll.Rows[i].FindControl("ddlPhase");
            TextBox TxtProgramYear = (TextBox)gvnroll.Rows[i].FindControl("TxtProgramYear");
            TextBox TxtOperationalYear = (TextBox)gvnroll.Rows[i].FindControl("TxtOperationalYear");

            Ret = Insert_Update_Phase(UniqueID, lblStateCode, lblDistrictCode, TxtRegion, ddlPhase, TxtProgramYear, TxtOperationalYear, Flag);
            Ret++;
        }
        if (Ret > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
        }
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        ExportExcel("PhaseMapping_", Session["ExcelExport"] as DataTable);

    }
    public int Insert_Update_Phase(string PM_GUID, Label lblStateCode, Label lblDistrictCode, TextBox TxtRegion, DropDownList ddlPhase, TextBox TxtProgramYear, TextBox TxtOperationalYear, string Flag)
    {
        SqlConnection dbSqlconnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            dbSqlconnection.Open();
            using (SqlCommand dbSqlCommand = (SqlCommand)dbSqlconnection.CreateCommand())
            {
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = "[Insert_Update_Phase_Mapping]";
                dbSqlCommand.Parameters.AddWithValue("@PM_GUID", PM_GUID);
                dbSqlCommand.Parameters.AddWithValue("@StateCode", lblStateCode.Text == "" ? "" : Convert.ToString(lblStateCode.Text));
                dbSqlCommand.Parameters.AddWithValue("@DistrictCode", lblDistrictCode.Text == "" ? "" : Convert.ToString(lblDistrictCode.Text));
                dbSqlCommand.Parameters.AddWithValue("@Finacial_Year", ddlYear.SelectedIndex > 0 ? ddlYear.SelectedItem.Text : "");
                dbSqlCommand.Parameters.AddWithValue("@Region", TxtRegion.Text == "" ? "" : Convert.ToString(TxtRegion.Text));
                dbSqlCommand.Parameters.AddWithValue("@Phase", ddlPhase.SelectedIndex > 0 ? ddlPhase.SelectedValue : "0");
                dbSqlCommand.Parameters.AddWithValue("@Program_Year", TxtProgramYear.Text == "" ? 0 : Convert.ToInt32(TxtProgramYear.Text));
                dbSqlCommand.Parameters.AddWithValue("@Operational_Year", TxtOperationalYear.Text == "" ? 0 : Convert.ToInt32(TxtOperationalYear.Text));
                dbSqlCommand.Parameters.AddWithValue("@CreatedBy", Convert.ToString(Session["username"]));
                dbSqlCommand.Parameters.AddWithValue("@Flag", Flag);
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
            throw exp;
        }
        finally
        {
            dbSqlconnection.Dispose();
        }
    }
    #endregion
    #region *************** Export Excel
    private void ExportExcel(string FIleName, DataTable dt)
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
                HttpContext.Current.Response.Write("<table  width='80%'>");
                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";
                HttpContext.Current.Response.Write("<tr>");
                HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'>State Name</td>");
                HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'>District Name</td>");
                HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'>Region</td>");
                HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'>Phase</td>");
                HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'>Program Year</td>");
                HttpContext.Current.Response.Write("<td style='text-align:Center;border:.2pt solid windowtext;'>Operational Year</td>");
                HttpContext.Current.Response.Write("</tr>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["StateName"] + "</td>");
                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["DistrictName"] + "</td>");
                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["Region"] + "</td>");
                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["Phase"] + "</td>");
                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["Program_Year"] + "</td>");
                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["Operational_Year"] + "</td>");
                    HttpContext.Current.Response.Write("</tr>");
                }
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

    #endregion
}