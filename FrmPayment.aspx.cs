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
public partial class FrmPayment : System.Web.UI.Page
{
    clsMain objMain = new clsMain();

    Comman objComman = new Comman();
    string labelmainheading = "";
    string conditions = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Convert.ToString(Session["username"]) != "")
                {
                    ViewState["Button"] = "AA";
                    LoadYear();
                    LoadUserLeavel();
                    return;
                }
                base.Response.Redirect("Login.aspx", false);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    public void LoadYear()
    {
        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");
        ddlYear.SelectedIndex = 1;
    }
    public void FillCBState()
    {
        string conditions = "";
        string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State   order by StateName   ";
        DataTable dtState = objMain.LoadData(strQry1);
        ChkState.DataSource = dtState;
        ChkState.DataTextField = "StateName";
        ChkState.DataValueField = "StateCode";
        ChkState.DataBind();

    }



    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            AlllStateCode();
            string conditions = "";
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
                //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
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

                ddlDistrict_SelectedIndexChanged(chkDistrict, null);
            }
            else
            {
                foreach (ListItem item in ChkState.Items)
                {

                    item.Selected = false;

                }
                chkDistrict.Items.Clear();
                chkBlock.Items.Clear();
            }
        }
        catch (Exception)
        {

            throw;
        }
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
    public void FillCBDist()
    {
        string conditions = "";
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
           
            string strQry1 = "       sELECT distinct mst2District.DistrictCode as DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist  ";
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

    public void FillCBBock()
    {
        string conditions = "";
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

        string strQry = "  SELECT BlockCode, dbo.TitleCase(upper(BlockName))  as BlockName FROM mst3Block where " + conditions + "  order by BlockName   ";
        DataTable dtDistrict = objMain.LoadData(strQry);
        // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        chkBlock.DataSource = dtDistrict;
        chkBlock.DataTextField = "BlockName";
        chkBlock.DataValueField = "BlockCode";
        chkBlock.DataBind();

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
        }

    }

    protected void PMS_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlFMonth.SelectedIndex > 0 && ddlToMonth.SelectedIndex > 0)
            {
                if (Convert.ToInt32(ddlToMonth.SelectedValue) == DateTime.Now.Month)
                {
                    ScriptManager.RegisterStartupScript(Page, base.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('You can not download the report since the month is not completed!!')</script>", false);
                    return;
                }
                if (Convert.ToInt32(ddlFMonth.SelectedValue) == DateTime.Now.Month)
                {
                    ScriptManager.RegisterStartupScript(Page, base.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('You can not download the report since the month is not completed!!')</script>", false);
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, base.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select From & To month')</script>", false);
                }
            }

            if (ddlFMonth.SelectedIndex > 0 && ddlToMonth.SelectedIndex > 0)
            {
                if(Convert.ToInt32(ddlFMonth.SelectedValue)==1 || Convert.ToInt32(ddlFMonth.SelectedValue) == 2 || Convert.ToInt32(ddlFMonth.SelectedValue) == 3)
                {
                    if((Convert.ToInt32(ddlToMonth.SelectedValue)> Convert.ToInt32(ddlFMonth.SelectedIndex)) && (Convert.ToInt32(ddlToMonth.SelectedValue) >3))
                    {
                        ScriptManager.RegisterStartupScript(Page, base.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select valid Month')</script>", false);
                    }
                    else
                    {
                         getreport();
                    }
                }
                else
                {
                    if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlToMonth.SelectedValue) == 2 || Convert.ToInt32(ddlToMonth.SelectedValue) == 3)
                    {
                        getreport();
                    }
                    else   if ((Convert.ToInt32(ddlToMonth.SelectedValue) >= Convert.ToInt32(ddlFMonth.SelectedIndex)) )
                    {
                        getreport();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, base.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select valid Month')</script>", false);
                    }
                    
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, base.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select From & To month')</script>", false);
            }

         
            labelmainheading = "Learning Level Report";
        }
        catch (Exception)
        {

            throw;
        }
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
        string conditions = "";
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
                ddlDistrict_SelectedIndexChanged(ddlState, null);
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
            foreach (ListItem item in chkDistrict.Items)
            {

                item.Selected = true;

            }
            ddlDistrict_SelectedIndexChanged(chkDistrict, null);
           
        }
    }

    public void getreport()
    {
        conditions = "";
        string conditions1 = "";
        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlstate = "";

        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlstate += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlstate.Length > 0)
        {
            ddlstate = ddlstate.Substring(0, ddlstate.LastIndexOf(","));
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


        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + "  and  mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlstate.Length > 0)
        {
            conditions += "  and  mst5Village.StateCode in(" + ddlstate + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions += "  and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions += "   and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
       
        //if (ddlToMonth.SelectedIndex > 0)
        //{
        //    if (ddlFMonth.SelectedIndex > 0)
        //    {
        //        conditions1 = conditions1 + " and Month([tblChildAttendanceGKP2023].SysDate) between '" + ddlFMonth.SelectedValue + "' and '" + ddlToMonth.SelectedValue + "' ";


        //    }
        //}


        int Fyear = 0, Fyear1 = 0;
        int lastDayOfMonth = DateTime.DaysInMonth(Convert.ToInt32(DateTime.Now.Year.ToString()), Convert.ToInt32(ddlToMonth.SelectedValue));
        if (Convert.ToInt32(ddlToMonth.SelectedValue) <= 3)
        {
            Fyear = Convert.ToInt32(DateTime.Now.Year.ToString());
        }
        else
        {
            Fyear = Convert.ToInt32(DateTime.Now.Year.ToString()) - 1;
        }
        if (Convert.ToInt32(ddlFMonth.SelectedValue) <= 3)
        {
            Fyear1 = Convert.ToInt32(DateTime.Now.Year.ToString());
        }
        else
        {
            Fyear1 = Convert.ToInt32(DateTime.Now.Year.ToString()) - 1;
        }
        string Fdate = "01/" + ddlFMonth.SelectedValue + "/" + Fyear1;
        string Tdate = lastDayOfMonth + "/" + ddlToMonth.SelectedValue + "/" + Fyear;
        string Fdate1 = "01/" + "04" + "/" + ddlYear.SelectedValue;
        string conditions2 = "";

        if (ddlToMonth.SelectedIndex > 0)
        {
            if (ddlFMonth.SelectedIndex > 0)
            {
                conditions1 = conditions1 + " and convert(date, [tblChildAttendanceGKP2023].SysDate) between '" + Convert.ToDateTime(Fdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(Tdate).ToString("yyyy-MM-dd") + "' ";

                conditions2 = " and convert(date, [tblChildAttendanceGKP2023].SysDate) between '" + Convert.ToDateTime(Fdate1).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(Tdate).ToString("yyyy-MM-dd") + "' ";

            }
        }


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Cond", conditions),
            new SqlParameter("@Cond1", conditions1),
            new SqlParameter("@Fyear", ddlYear.SelectedItem.Text),
                     new SqlParameter("@Cond5", conditions2),
              new SqlParameter("@Month", ddlToMonth.SelectedValue),
                 

        };
        DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPaymentReport2024]", cmdParameters);
        lblTotalCount.Text = dataTable.Rows.Count.ToString();
        ViewState["dt"] = dataTable;
        if (dataTable.Rows.Count > 0)
        {
            ExporttoExcel(dataTable, "PaymentReport");
        }
      
    }

    private void ExporttoExcel(DataTable table, string FileName)
    {
        try
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
            //HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            //style = 'font-size:10.0pt; font-family:Calibri; background:white;'
            HttpContext.Current.Response.Write("<Table  style='border:2px solid black;' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' > <TR >");
            
            int Fyear = 0, Fyear1=0;
            int lastDayOfMonth = DateTime.DaysInMonth(Convert.ToInt32(DateTime.Now.Year.ToString()), Convert.ToInt32(ddlToMonth.SelectedValue));
            if(Convert.ToInt32(ddlToMonth.SelectedValue) <= 3)
            {
                Fyear = Convert.ToInt32(DateTime.Now.Year.ToString()) ;
            }
            else
            {
                Fyear = Convert.ToInt32(DateTime.Now.Year.ToString()) - 1;
            }
            if (Convert.ToInt32(ddlFMonth.SelectedValue) <= 3)
            {
                Fyear1 = Convert.ToInt32(DateTime.Now.Year.ToString()) ;
            }
            else
            {
                Fyear1 = Convert.ToInt32(DateTime.Now.Year.ToString()) - 1;
            }
            string Fdate = "01/" + ddlFMonth.SelectedValue + "/" + Fyear1;
            string Tdate = lastDayOfMonth +"/" + ddlToMonth.SelectedValue +"/"+ Fyear;
            decimal total = 0,  total1 = 0;
            HttpContext.Current.Response.Write("<Td colspan='13' style='text-align:center;font-family:Calibri;font-size:14px;font-weight:bold'>Foundation to Educate Girls Globally</td></TR>");
            HttpContext.Current.Response.Write("<TR ><Td style='border-bottom: 2px solid black;text-align:right;font-family:Calibri;font-size:14px;font-weight:bold'>Download Date:</td>");
            HttpContext.Current.Response.Write("<Td style='border-bottom: 2px solid black;font-family:Calibri;font-size:14px;font-weight:bold;text-align:left'> " + DateTime.Now.Date.ToString("dd/MM/yyyy") + "</td>");
            HttpContext.Current.Response.Write("<Td colspan='3' style='border-bottom: 2px solid black;'></td>");
            HttpContext.Current.Response.Write("<Td style='border-bottom: 2px solid black;text-align:right;font-family:Calibri;font-size:14px;font-weight:bold'>Data from Date:</td>");
            HttpContext.Current.Response.Write("<Td style='border-bottom: 2px solid black;font-family:Calibri;font-size:14px;font-weight:bold'>" + Fdate + " </td>");
            HttpContext.Current.Response.Write("<Td colspan='2' style='border-bottom: 2px solid black;'></td>");
            HttpContext.Current.Response.Write("<Td  style='border-bottom: 2px solid black;text-align:right;font-family:Calibri;font-size:14px;font-weight:bold'>Data to Date:</td>");
            HttpContext.Current.Response.Write("<Td style='border-bottom: 2px solid black;font-family:Calibri;font-size:14px;font-weight:bold'>" + Tdate + "</td>");
            HttpContext.Current.Response.Write("<Td colspan='2' style='border-bottom: 2px solid black;'></td></TR>");
            HttpContext.Current.Response.Write("<TR>");

            for (int i = 0; i < table.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write("<Td style='background-color:#FFE699;font-family:Calibri;font-size:14px;font-weight:bold;border-style: solid solid solid solid;border-width: thin;' > ");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(table.Columns[i].ToString());
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow dataRow in table.Rows)
            {
                HttpContext.Current.Response.Write("<TR>");
                for (int j = 0; j < table.Columns.Count; j++)
                {

                    if (j == 11)
                    {
                        total += Convert.ToDecimal(dataRow[j].ToString());
                    }
                    if (j == 12)
                    {
                        total1 += Convert.ToDecimal(dataRow[j].ToString());
                    }
                    
                    HttpContext.Current.Response.Write("<Td style='font-family:Calibri;font-size:14px;border-style: solid solid solid solid;border-width: thin;'>");
                    if (j == 6)
                    {
                        if (dataRow[j].ToString().Length>14)
                        {
                            HttpContext.Current.Response.Write("'" + dataRow[j].ToString());
                        }
                        else
                        {
                            HttpContext.Current.Response.Write(dataRow[j].ToString());
                        }
                       
                    }
                    else
                    {
                        HttpContext.Current.Response.Write(dataRow[j].ToString());
                    }

                    HttpContext.Current.Response.Write("</Td>");
                }
                HttpContext.Current.Response.Write("</TR>");
            }

            HttpContext.Current.Response.Write("<TR><Td colspan='10' Style='border-style: solid solid solid solid;border-width: thin;' ></td>");
            HttpContext.Current.Response.Write("<Td style='background-color:Yellow;font-family:Calibri;font-size:10px;font-weight:bold;border-style: solid solid solid solid;border-width: thin;'>Total :</td>");
            HttpContext.Current.Response.Write("<Td style='background-color:Yellow;font-family:Calibri;font-size:10px;font-weight:bold;border-style: solid solid solid solid;border-width: thin;'>" + total + "</td>");
            HttpContext.Current.Response.Write("<Td style='background-color:Yellow;font-family:Calibri;font-size:10px;font-weight:bold;border-style: solid solid solid solid;border-width: thin;'>" + total1 + "</td></tr>");
            HttpContext.Current.Response.Write("<TR><Td style='border-bottom: 2px solid black;' colspan='13' ></td></tr>");

            HttpContext.Current.Response.Write("<TR><Td></td>");
            HttpContext.Current.Response.Write("<Td >..........................</td>");
            HttpContext.Current.Response.Write("<Td >...........................</td>");
            HttpContext.Current.Response.Write("<Td colspan='8' ></td>");
            HttpContext.Current.Response.Write("<Td colspan='2' >................................</td></TR>");

            HttpContext.Current.Response.Write("<TR><Td></td>");
            HttpContext.Current.Response.Write("<Td  style='font-family:Calibri;font-size:10px;font-weight:bold'>Prepared by</td>");
            HttpContext.Current.Response.Write("<Td  style='font-family:Calibri;font-size:10px;font-weight:bold'>Verified by</td>");
            HttpContext.Current.Response.Write("<Td colspan='8' ></td>");
            HttpContext.Current.Response.Write("<Td colspan='2'  style='font-family:Calibri;font-size:10px;font-weight:bold'>Approved by</td></TR>");

            HttpContext.Current.Response.Write("<TR><Td  style='font-family:Calibri;font-size:10px;font-weight:bold'> Date: -</td>");
            HttpContext.Current.Response.Write("<Td style='font-family:Calibri;font-size:10px;font-weight:bold'>…./……/" + DateTime.Now.Year.ToString() + "</td>");
            HttpContext.Current.Response.Write("<Td style='font-family:Calibri;font-size:10px;font-weight:bold'>…./……/" + DateTime.Now.Year.ToString() + "</td>");
            HttpContext.Current.Response.Write("<Td colspan='8' ></td>");
            HttpContext.Current.Response.Write("<Td colspan='2' style='font-family:Calibri;font-size:10px;font-weight:bold' >…./……/" + DateTime.Now.Year.ToString() + "</td></TR>");

            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        catch (Exception)
        {

            throw;
        }
    }
}