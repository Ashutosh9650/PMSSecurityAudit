using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmTravelMatrixApprove : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    string conditions = string.Empty, Flag = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {

                if (Request.QueryString["ID"] != null)
                {
                    LoadYear();

                    LoadUserLeavel();
                    GV_TravelMatrix.DataSource = null;
                    GV_TravelMatrix.DataBind();
                    UserLevelFilter();
                    string QueryString = Request.QueryString["ID"];
                    string[] array = QueryString.Split(',');
                    ddlBlock.SelectedValue = array[0].ToString();
                    ddlMonth.SelectedValue = array[1].ToString();
                    ddlmonthselectindex(ddlState, null);
                    btnsubmit.Visible = false;
                    btnSerach_Click(btnSerach, null);

                    if (Session["user_level"].ToString() == "124")
                    {

                        btnsubmit.Text = "Process For Payment";
                        btnsubmit.ToolTip = "Process For Payment";
                    }
                    else if (Session["user_level"].ToString() == "123")
                    {

                        btnsubmit.Text = "Verify";
                        btnsubmit.ToolTip = "Verify";
                    }
                    btnsubmit.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Approve TD/DA for whole District ? ')");
                }
                else
                {
                    btnsubmit.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Approve TD/DA for whole District ? ')");
                    LoadYear();

                    LoadUserLeavel();
                    GV_TravelMatrix.DataSource = null;
                    GV_TravelMatrix.DataBind();
                    UserLevelFilter();
                    //FillMonths();
                    Int32 p = Convert.ToInt32(DateTime.Now.Month);
                    ddlMonth.SelectedValue = p.ToString();
                    ddlmonthselectindex(ddlState, null);
                    btnsubmit.Visible = false;
                    if (Session["user_level"].ToString() == "124")
                    {

                        btnsubmit.Text = "Process For Payment";
                        btnsubmit.ToolTip = "Process For Payment";
                    }
                    else if (Session["user_level"].ToString() == "123")
                    {

                        btnsubmit.Text = "Verify";
                        btnsubmit.ToolTip = "Verify";
                    }
                }

            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
        }

    }

    protected void ddlmonthselectindex(object sender, EventArgs e)
    {
        string startdate = "";
        string enddate = "";
        string startdate1 = "";
        string enddate1 = "";
        int pmonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
        Int32 Icount = Convert.ToInt32(ddlYear.SelectedValue) + 1;
        if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
        {
            string strQry1 = "Select * from mstTravelDateRange  where mMonth=0  ";


            DataTable dtTravelRang = objMain.LoadData(strQry1);
            startdate = dtTravelRang.Rows[0]["FromDay"].ToString() + "/" + 12 + "/" + ddlYear.SelectedValue + "";
            enddate = dtTravelRang.Rows[0]["ToDay"].ToString() + "/" + Convert.ToInt32(ddlMonth.SelectedValue) + "/" + Icount + "";

            startdate1 = ddlYear.SelectedValue + "-" + 12 + "-" + dtTravelRang.Rows[0]["FromDay"].ToString();
            enddate1 = Icount + "-" + Convert.ToInt32(ddlMonth.SelectedValue) + "-" + dtTravelRang.Rows[0]["ToDay"].ToString();
        }
        else if (Convert.ToInt32(ddlMonth.SelectedValue) == 2)
        {
            string strQry1 = "Select * from mstTravelDateRange  where mMonth=0  ";


            DataTable dtTravelRang = objMain.LoadData(strQry1);

            startdate = dtTravelRang.Rows[0]["FromDay"].ToString() + "/" + pmonth + "/" + Icount + "";
            enddate = dtTravelRang.Rows[0]["ToDay"].ToString() + "/" + Convert.ToInt32(ddlMonth.SelectedValue) + "/" + Icount + "";

            startdate1 = Icount + "-" + pmonth + "-" + dtTravelRang.Rows[0]["FromDay"].ToString();
            enddate1 = Icount + "-" + Convert.ToInt32(ddlMonth.SelectedValue) + "-" + dtTravelRang.Rows[0]["ToDay"].ToString(); ;

        }
        else if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        {
            string strQry1 = "Select * from mstTravelDateRange  where mMonth=" + Convert.ToInt32(ddlMonth.SelectedValue) + "  ";


            DataTable dtTravelRang = objMain.LoadData(strQry1);

            startdate = dtTravelRang.Rows[0]["FromDay"].ToString() + "/" + pmonth + "/" + Icount + "";
            enddate = dtTravelRang.Rows[0]["ToDay"].ToString() + "/" + Convert.ToInt32(ddlMonth.SelectedValue) + "/" + Icount + "";

            startdate1 = Icount + "-" + pmonth + "-" + dtTravelRang.Rows[0]["FromDay"].ToString();
            enddate1 = Icount + "-" + Convert.ToInt32(ddlMonth.SelectedValue) + "-" + dtTravelRang.Rows[0]["ToDay"].ToString(); ;

        }
        else if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
        {
            string strQry1 = "Select * from mstTravelDateRange  where mMonth=" + Convert.ToInt32(ddlMonth.SelectedValue) + "  ";


            DataTable dtTravelRang = objMain.LoadData(strQry1);

            startdate = dtTravelRang.Rows[0]["FromDay"].ToString() + "/" + 4 + "/" + Icount + "";
            enddate = dtTravelRang.Rows[0]["ToDay"].ToString() + "/" + Convert.ToInt32(ddlMonth.SelectedValue) + "/" + Icount + "";

            startdate1 = Icount + "-" + 04 + "-" + dtTravelRang.Rows[0]["FromDay"].ToString();
            enddate1 = Icount + "-" + Convert.ToInt32(ddlMonth.SelectedValue) + "-" + dtTravelRang.Rows[0]["ToDay"].ToString();
        }
        else
        {
            string strQry1 = "Select * from mstTravelDateRange  where mMonth=0 ";


            DataTable dtTravelRang = objMain.LoadData(strQry1);

            startdate = dtTravelRang.Rows[0]["FromDay"].ToString() + "/" + pmonth + "/" + ddlYear.SelectedValue + "";
            enddate = dtTravelRang.Rows[0]["ToDay"].ToString() + "/" + Convert.ToInt32(ddlMonth.SelectedValue) + "/" + ddlYear.SelectedValue + "";


            startdate1 = Convert.ToInt32(ddlYear.SelectedValue) + "-" + pmonth + "-" + dtTravelRang.Rows[0]["FromDay"].ToString();
            enddate1 = Convert.ToInt32(ddlYear.SelectedValue) + "-" + Convert.ToInt32(ddlMonth.SelectedValue) + "-" + dtTravelRang.Rows[0]["ToDay"].ToString();
        }


        txtfd.Text = startdate;
        txttd.Text = enddate;
        txtfdcal.StartDate = Convert.ToDateTime(startdate1);
        txtfdcal.EndDate = Convert.ToDateTime(enddate1);

        txttdcal.StartDate = Convert.ToDateTime(startdate1);
        txttdcal.EndDate = Convert.ToDateTime(enddate1);

    }

    protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlMonth.SelectedValue) > 0)
        {
            GV_TravelMatrix.DataSource = null;
            GV_TravelMatrix.DataBind();
        }
    }
    public void FillMonths()
    {

        ddlMonth.Items.Clear();
        string[] M = { "ff", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        int Month = DateTime.Now.Month;

        string Year = ddlYear.SelectedValue;
        string[] a = Year.Split('-');

        if ((a[0] != "0") && (a[0] == DateTime.Now.Year.ToString()))
        {

            ddlMonth.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", "0", true));

            for (int i = 0; i < Month; i++)
            {

                ddlMonth.Items.Add(M[i].ToString());
            }

        }
        else
        {
            ddlMonth.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", "0", true));
            for (int i = 1; i < 12; i++)
            {

                ddlMonth.Items.Add(M[i].ToString());
            }
        }


    }

    public void UserLevelFilter()
    {


        string strQry1 = "Select * from mstDAAmount  where RoleID=" + Session["user_level"].ToString() + "   ";


        DataTable dtTravelDA = objMain.LoadData(strQry1);
        if (dtTravelDA.Rows.Count > 0)
        {
            lblB1DA.Text = dtTravelDA.Rows[0]["DAAmount"].ToString();
        }

        string strQry = "";
        string Cond = "Module='Enroll'";
        strQry = "Select * from MstUserRight  where " + Cond + " and Role_Id=" + Session["user_level"].ToString() + "   ";


        DataTable dtTravelMatrix = objMain.LoadData(strQry);

        if (dtTravelMatrix.Rows.Count > 0)
        {
            vADD = Convert.ToBoolean(dtTravelMatrix.Rows[0]["AddStatus"].ToString());
            vVerify = Convert.ToBoolean(dtTravelMatrix.Rows[0]["verify_Status"].ToString());
            vDelete = Convert.ToBoolean(dtTravelMatrix.Rows[0]["Delete_status"].ToString());

            ViewState["vADD"] = vADD;
            ViewState["vVerify"] = vVerify;
            ViewState["vDelete"] = vDelete;
        }
        //if (vDelete == true)
        //{

        //    btnDelete.Visible = true;
        //}
        //else
        //{

        //    btnDelete.Visible = false;
        //}



    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {



        if (Convert.ToInt32(ddlMonth.SelectedValue) <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Month ')</script>", false);
            return;
        }

        if (Session["user_level"].ToString() == "19")
        {
            LoadData();
        }

        else if (Session["user_level"].ToString() == "124")
        {
            LoadData();
        }
        else if (Session["user_level"].ToString() == "123")
        {
            LoadData();
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
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            ddlState.SelectedIndex = 1;
            ddlState1_SelectedIndexChanged(ddlDistrict, null);
            ddlDistrict.SelectedIndex = 1;
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);



        }
        else
        {
            ddlState.SelectedIndex = 0;
            ddlDistrict.Items.Clear();
            ddlBlock.Items.Clear();


        }

        ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
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
                    dr = dtYear.NewRow();
                    dr["Type"] = GivenYear - 2 + "-" + Convert.ToString((GivenYear - 2 + 1));
                    dr["ID"] = y - 2;
                    dtYear.Rows.Add(dr);
                }
                else
                {
                    dr = dtYear.NewRow();
                    dr["Type"] = Convert.ToString((y - 1)) + "-" + y.ToString();
                    //y = y - 1;
                    dr["ID"] = y - 1;

                    dtYear.Rows.Add(dr);

                    dr = dtYear.NewRow();
                    dr["Type"] = GivenYear - 2 + "-" + Convert.ToString((GivenYear - 2 + 1));
                    dr["ID"] = y - 2;
                    dtYear.Rows.Add(dr);
                }

            }

        }
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


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
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

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
                    ddlYear.Enabled = true;
                }
            }
            else
            {
                ddlYear.Enabled = true;
            }
            ddlDistrict.SelectedIndex = 1;
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }





    }

    #region Fill Master Data
    public void FillCBState(DropDownList ddl)
    {
        conditions = "";
        objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddl, "StateName", "StateCode", "--Select--");
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


        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");



    }
    public void FillCBBock()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        else if (Session["user_level_Role"].ToString() == "4")
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 and BlockCode in( " + Session["BlockCode"].ToString() + " ) and FYear ='" + ddlYear.SelectedItem.Text + "' ";
        }
        else
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--ALL--");



    }



    #endregion

    #region   SelectedIndexChanged Methods
    protected void ddlState1_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBBock();
    }



    #endregion

    protected void GV_Retention_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        UpdateData();
        GV_TravelMatrix.PageIndex = e.NewPageIndex;
        if (Session["GridViewData"] != null)
        {
            DataTable dt = Session["GridViewData"] as DataTable;
            GV_TravelMatrix.DataSource = dt;
            GV_TravelMatrix.DataBind();
        }


    }
    public void UpdateData()
    {

        DataTable dt = (DataTable)Session["GridViewData"];

        for (int i = 0; i < GV_TravelMatrix.Rows.Count; i++)
        {
            string C_ID = GV_TravelMatrix.DataKeys[i]["UniqueChildCode"].ToString();

            CheckBox chkPresent = ((CheckBox)GV_TravelMatrix.Rows[i].FindControl("RbPresent"));
            CheckBox chkAbsent = ((CheckBox)GV_TravelMatrix.Rows[i].FindControl("RbAbsent"));
            CheckBox RbNone = ((CheckBox)GV_TravelMatrix.Rows[i].FindControl("RbNone"));
            if (chkPresent.Checked == true || chkAbsent.Checked == true || RbNone.Checked == true)
            {
                DataRow[] dr = dt.Select("UniqueChildCode='" + Convert.ToString(C_ID) + "'");
                if (dr.Length > 0)
                {
                    if (chkPresent.Checked == true)
                    {
                        dr[0]["TempId"] = 1;


                    }
                    if (chkAbsent.Checked == true)
                    {
                        dr[0]["TempId"] = 2;
                    }
                    if (RbNone.Checked == true)
                    {
                        dr[0]["TempId"] = 0;
                    }
                }
            }

        }
        Session["GridViewData"] = dt;

    }

    public DataTable CreateDataTableFare()
    {

        DataTable dt = new DataTable();
        dt.Columns.Add("LogDate", System.Type.GetType("System.DateTime"));
        dt.Columns.Add("BaseFare", System.Type.GetType("System.Int32"));
        dt.Columns.Add("cBaseFare", System.Type.GetType("System.Int32"));
        dt.Columns.Add("VillageName", System.Type.GetType("System.String"));
        dt.Columns.Add("Remark", System.Type.GetType("System.String"));
        dt.Columns.Add("submissionstatus");
        dt.Columns.Add("BOVillage");
        dt.Columns.Add("Logintime");
        dt.Columns.Add("LogoutTime");
        dt.Columns.Add("Remarks");
        dt.Columns.Add("AID");
        return dt;

    }

    public string changedatetimetype(string strdt)
    {
        string dtstr = "";
        string[] dtyp = strdt.Split('/');
        dtstr = dtyp[2] + "-" + dtyp[1] + "-" + dtyp[0];
        return dtstr;
    }






    public void LoadData()
    {

        conditions = "where 1 =1 ";
        if (ddlState.SelectedIndex > 0)
        {
            conditions += "  and mstCluster.StateCode ='" + ddlState.SelectedValue + "' ";

        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " and mstCluster.DistrictCode ='" + ddlDistrict.SelectedValue + "' ";

        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions += " and mstCluster.BlockCode ='" + ddlBlock.SelectedValue + "' ";

        }

        //string st = "";
        //if (Session["user_level"].ToString() == "19")
        //{
        //    st = "";
        //}

        //else if (Session["user_level"].ToString() == "124")
        //{
        //    st = " and SubmissionStatus in ('A','F') and deleteflag=0";
        //}
        //else if (Session["user_level"].ToString() == "123")
        //{
        //    st = " and SubmissionStatus in ('A','P','F') and deleteflag=0";
        //}


        if (Session["user_level"].ToString() == "123")
        {
            //    string strQry = "select DATE as LogDate,'' as  FromVillageCode,'' as FromVillagename, mst5Village.VillageCode,VillageName,LoginTime,0 as BaseFare from Tbl_User_Login inner join mst5Village on mst5Village.VillageCode=Tbl_User_Login.Villagecode where userid=2465 and date>='2018-06-16' and   date<='2018-06-27' order by date, LoginTime ";

            conditions = conditions + " and TravelDate between ( '" + Convert.ToDateTime(txtfd.Text).ToString("yyyy-MM-dd") + " ') and  ( '" + Convert.ToDateTime(txttd.Text).ToString("yyyy-MM-dd") + "')";


            SqlParameter[] cmdParameters1 = new SqlParameter[]
                {
                new SqlParameter("@Con", conditions),
                new SqlParameter("@month", ddlMonth.SelectedValue),
                new SqlParameter("@Year", ddlYear.SelectedValue),
                new SqlParameter("@flag", "6")
                };
            DataTable dataTableNew = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTADAStatusReportMail]", cmdParameters1);

            if (dataTableNew.Rows.Count > 0)
            {

                if (dataTableNew.Rows[0]["Status"].ToString() == "Approved(BO)")
                {
                    btnsubmit.Visible = true;
                }
                else
                {
                    btnsubmit.Visible = false;
                }
                GV_TravelMatrix.DataSource = dataTableNew;
                GV_TravelMatrix.DataBind();



            }
            else
            {
                GV_TravelMatrix.DataSource = null;
                GV_TravelMatrix.DataBind();
            }

        }
        else if (Session["user_level"].ToString() == "124")
        {
            //    string strQry = "select DATE as LogDate,'' as  FromVillageCode,'' as FromVillagename, mst5Village.VillageCode,VillageName,LoginTime,0 as BaseFare from Tbl_User_Login inner join mst5Village on mst5Village.VillageCode=Tbl_User_Login.Villagecode where userid=2465 and date>='2018-06-16' and   date<='2018-06-27' order by date, LoginTime ";

            conditions = conditions + " and TravelDate between ( '" + Convert.ToDateTime(txtfd.Text).ToString("yyyy-MM-dd") + " ') and  ( '" + Convert.ToDateTime(txttd.Text).ToString("yyyy-MM-dd") + "')";




            SqlParameter[] cmdParameters = new SqlParameter[]
                {
                    new SqlParameter("@Con", conditions),
                    new SqlParameter("@month", ddlMonth.SelectedValue),
                    new SqlParameter("@Year", ddlYear.SelectedValue),
                    new SqlParameter("@flag", "3")
                };
            DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTADAStatusReport]", cmdParameters);
            if (dataTable.Rows.Count > 0)
            {

                if (dataTable.Rows[0]["Status"].ToString() == "Verified(Admin)")
                {
                    btnsubmit.Visible = true;
                }
                else
                {
                    btnsubmit.Visible = false;
                }
                GvAccount.DataSource = dataTable;
                GvAccount.DataBind();



            }
            else
            {
                GvAccount.DataSource = null;
                GvAccount.DataBind();
            }
        }

    }
    protected void LnkStatus_OnClick(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string lblBlockCOde = (gvr.FindControl("lblBlockCOde") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;
        string lblUserID = (gvr.FindControl("lblUserID") as Label).Text;

        Session["Fblock"] = ddlBlock.SelectedValue;
        Session["FMonth"] = ddlMonth.SelectedValue;
        base.Response.Redirect("~/FrmTravelmatrix.aspx?ID=" + lblBlockCOde + "," + ddlMonth.SelectedValue + " ," + ddlDistrict.SelectedValue + "," + lblClusterCode + "," + lblUserID + " ");


    }

    protected void LnkStatus_OnClick1(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string lblBlockCOde = (gvr.FindControl("lblBlockCOde") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;
        string lblUserID = (gvr.FindControl("lblUserID1") as Label).Text;

        Session["Fblock"] = ddlBlock.SelectedValue;
        Session["FMonth"] = ddlMonth.SelectedValue;
        base.Response.Redirect("~/FrmTravelmatrix.aspx?ID=" + lblBlockCOde + "," + ddlMonth.SelectedValue + " ," + ddlDistrict.SelectedValue + "," + lblClusterCode + "," + lblUserID + " ");


    }
    protected void lnk1_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;
        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string LBLTempId = (gvr.FindControl("LBLTempId") as Label).Text;

        DataTable dt = Session["travelMatrixDetails"] as DataTable;
        DataView dv1 = dt.DefaultView;
        dv1.RowFilter = "LogDate ='" + LBLTempId + "' ";
        DataTable dtNew = dv1.ToTable();


    }


    #region Add Village

    protected void btnPayment_Click(object sender, EventArgs e)
    {
        if (GvAccount.Rows.Count > 0)
        {

            string st = "";

            if ((Session["user_level"].ToString() == "19"))
            {
                st = "P";

            }

            else if ((Session["user_level"].ToString() == "123"))
            {
                st = "A";
            }
            else
            {
                st = "F";
            }
            SqlParameter[] parm9 = new SqlParameter[]
                {
                 new SqlParameter("@Status",st),

                  new SqlParameter("@Tdate",changedatetimetype(txtfd.Text)),
                       new SqlParameter("@Todate",  changedatetimetype(txttd.Text)),

                  new SqlParameter("@BlockCOde",ddlBlock.SelectedValue),
                   new SqlParameter("@DistCOde",ddlDistrict.SelectedValue),
                    new SqlParameter("@SBY", Convert.ToString(Session["username"])),
                     new SqlParameter("@ID", "0"),
                       new SqlParameter("@RoleID", Session["user_level"].ToString()),
                };
            int r = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Travel_SubmissionNew", parm9);

            LoadData();
            string ApproveStatus = "";
            if (Session["user_level"].ToString() == "19")
            {


                ApproveStatus = "Approved";
            }

            else if (Session["user_level"].ToString() == "124")
            {

                ApproveStatus = "Processed For Payment";
            }
            else if (Session["user_level"].ToString() == "123")
            {

                ApproveStatus = "Verified";
            }
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ApproveStatus + " Successfully')</script>", false);
            return;

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        if ((Session["user_level"].ToString() == "123"))
        {
            int bCount = 0;
            int bTCount = 0;
            string strQry1 = "Select count(*) as icount from mst3block  where DistrictCode='" + ddlDistrict.SelectedValue + "'  ";
            DataTable dtUser = objMain.LoadData(strQry1);
            if (dtUser.Rows.Count > 0)
            {
                bCount = Convert.ToInt32(dtUser.Rows[0]["icount"]);
            }

            string conditions = " where mst3Block.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";
            conditions = conditions + " and TravelDate between ( '" + Convert.ToDateTime(txtfd.Text).ToString("yyyy-MM-dd") + " ') and  ( '" + Convert.ToDateTime(txttd.Text).ToString("yyyy-MM-dd") + "')";






            SqlParameter[] cmdParameters = new SqlParameter[]
                {
                    new SqlParameter("@Con", conditions),
                    new SqlParameter("@month", ddlMonth.SelectedValue),
                    new SqlParameter("@Year", ddlYear.SelectedValue),
                    new SqlParameter("@flag", "10")
                };
            DataTable dataTable11 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTADAStatusReportMail]", cmdParameters);

            if (dataTable11.Rows.Count > 0)
            {
                bTCount = Convert.ToInt32(dataTable11.Rows[0]["icount"]);
            }
            if (bTCount == bCount)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please approve all BO Data')</script>", false);

                return;
            }
        }
        if (Convert.ToInt32(ddlMonth.SelectedValue) <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Month ')</script>", false);

            return;
        }
        if ((Session["user_level"].ToString() == "124"))
        {
            CalendarExtender8rdate.StartDate = DateTime.Today;
            ModalPopupExtender1.Show();
        }
        else
        {
            if (GV_TravelMatrix.Rows.Count > 0)
            {

                string st = "";

                if ((Session["user_level"].ToString() == "19"))
                {
                    st = "P";

                }

                else if ((Session["user_level"].ToString() == "123"))
                {
                    st = "A";
                }
                else
                {
                    st = "F";
                }
                SqlParameter[] parm9 = new SqlParameter[]
                {
                 new SqlParameter("@Status",st),

                  new SqlParameter("@Tdate",changedatetimetype(txtfd.Text)),
                       new SqlParameter("@Todate",  changedatetimetype(txttd.Text)),

                  new SqlParameter("@BlockCOde",ddlBlock.SelectedValue),
                   new SqlParameter("@DistCOde",ddlDistrict.SelectedValue),
                    new SqlParameter("@SBY", Convert.ToString(Session["username"])),
                     new SqlParameter("@ID", "0"),
                       new SqlParameter("@RoleID", Session["user_level"].ToString()),
                };
                int r = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Travel_SubmissionNew", parm9);

                string ApproveStatus = "";
                if (Session["user_level"].ToString() == "19")
                {


                    ApproveStatus = "Approved";
                }

                else if (Session["user_level"].ToString() == "124")
                {

                    ApproveStatus = "Processed For Payment";
                }
                else if (Session["user_level"].ToString() == "123")
                {

                    ApproveStatus = "Verified";
                }


                LoadData();
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ApproveStatus + " Successfully')</script>", false);
                return;


            }
            //if (Session["user_level"].ToString() == "19")
            //{

            //    GenerateExcelApprove("");
            //}
        }
    }
    private void GenerateExcelApprove(string FIleName)
    {
        try
        {

            string conditions = " where MstUser.BlockCode = '" + ddlBlock.SelectedValue + "' ";
            conditions = conditions + " and TravelDate between ( '" + Convert.ToDateTime(txtfd.Text).ToString("yyyy-MM-dd") + " ') and  ( '" + Convert.ToDateTime(txttd.Text).ToString("yyyy-MM-dd") + "')";

            SqlParameter[] cmdParameters = new SqlParameter[]
                {
                    new SqlParameter("@Con", conditions),
                    new SqlParameter("@month", ddlMonth.SelectedValue),
                    new SqlParameter("@Year", ddlYear.SelectedValue),
                    new SqlParameter("@flag", "8")
                };
            DataTable dataTableCheck = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTADAStatusReportMail]", cmdParameters);



            if (dataTableCheck.Rows.Count > 0)
            {
                for (int k = 0; k < dataTableCheck.Rows.Count; k++)
                {


                    string email = dataTableCheck.Rows[k]["BlockCode"].ToString();
                    conditions = "";
                    conditions = " where mst3Block.BlockCode = '" + dataTableCheck.Rows[k]["BlockCode"].ToString() + "' ";
                    conditions = conditions + " and TravelDate between ( '" + Convert.ToDateTime(txtfd.Text).ToString("yyyy-MM-dd") + " ') and  ( '" + Convert.ToDateTime(txttd.Text).ToString("yyyy-MM-dd") + "')";



                    SqlParameter[] cmdParameters1 = new SqlParameter[]
                {
                    new SqlParameter("@Con", conditions),
                    new SqlParameter("@month", ddlMonth.SelectedValue),
                    new SqlParameter("@Year", ddlYear.SelectedValue),
                    new SqlParameter("@flag", "4")
                };
                    DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTADAStatusReportMail]", cmdParameters1);



                    DataTable dt = dataTable.Copy();
                    if (dt.Rows.Count > 0)
                    {
                        #region Excel Download
                        string Fullfilename1 = "" + dataTableCheck.Rows[0]["UserName"].ToString() + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";
                        string fileName = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + Fullfilename1 + "");
                        StreamWriter sw = new StreamWriter(fileName, false);

                        sw.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");



                        HttpContext.Current.Response.Charset = "utf-8";
                        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");


                        String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

                        sw.Write("<table>");

                        sw.Write("<tr style='font-width:bold;'>");


                        sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Block Name	</th>");
                        sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Cluster Name	</th>");
                        sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Emp ID	</th>");
                        sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Emp Name	</th>");
                        sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	TravelDate	</th>");
                        sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Local conveyance	</th>");
                        sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	DA	</th>");


                        sw.Write("</tr>");

                        String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";

                        String RowStyle1 = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#FFFF00;";


                        String HeaderStyle1 = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all;text-align:center; ";

                        string villagecode = string.Empty;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            sw.Write("<tr>");

                            for (int c = 0; c < dt.Columns.Count; c++)
                            {


                                sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");



                            }

                            sw.Write("</tr>");


                        }
                        Int32 TOtal = 0;
                        Int32 Local = 0;
                        Int32 DA = 0;
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {



                            if (dt.Rows[j]["DA"].ToString() != "")
                            {
                                DA += Convert.ToInt32(dt.Rows[j]["DA"]);
                            }

                            if (dt.Rows[j]["Local Conveyance"].ToString() != "")
                            {

                                Local += Convert.ToInt32(dt.Rows[j]["Local Conveyance"]);
                            }
                        }
                        sw.Write("<tr>");
                        sw.Write("<td colspan='5'  style='" + HeaderStyle1 + "'>Total</td>");
                        sw.Write("<td style='" + HeaderStyle + "'>" + Local + "</td>");
                        sw.Write("<td  style='" + HeaderStyle + "'>" + DA + "</td>");

                        //sw.Write("<td  style='" + HeaderStyle + "'></td>");
                        //sw.Write("<td  style='" + HeaderStyle + "'></td>");
                        //sw.Write("<td  style='" + HeaderStyle + "'></td>");
                        //sw.Write("<td  style='" + HeaderStyle + "'></td>");
                        sw.Write("</tr>");

                        //sw.Write("</table>");
                        sw.Write("</table>");



                        sw.Close();
                        SqlParameter[] cmdParameter5 = new SqlParameter[]
                        {
                          //  new SqlParameter("@Con", "where MstUser.UserID = '" + ddlFc.SelectedValue + "' "),
                            new SqlParameter("@Con1", "where MstUser.BlockCode = '" + ddlBlock.SelectedValue + "'"),

                        };
                        DataSet dtEmail = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTADAMail]", cmdParameter5);
                        if (dtEmail.Tables[0].Rows.Count > 0)
                        {
                            email = dtEmail.Tables[1].Rows[0]["EmaillID"].ToString();
                            //  email = "mukta.arora@educategirls.ngo";
                            //   email = "aksingh06mca@gmail.com";
                            //  email = "upendramani.kushwaha@educategirls.ngo";
                            if (email.Length > 0)
                            {
                                MailMessage mail = new MailMessage();
                                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                                mail.From = new MailAddress("PMS.Team@educategirls.ngo");
                                mail.To.Add("" + email + "");//
                                mail.Subject = "TA/DA updates for the " + ddlMonth.SelectedItem.Text + "/ " + ddlYear.SelectedValue + " ";
                                //ViewState["Body"] = "Dear  " + ViewState["EmployeeName"].ToString() + ",<br/><br/> Your Leave  for  " + ViewState["Noofdays"].ToString() + " days from " + HiddenField1.Value + " to " + HiddenField2.Value + " has been rejected,<br/> Reason:" + cmb_Reason.SelectedItem.Text + " <br/>Remarks:" + TxtRemark.InnerText + "<br/><br/> Regards,<br/> Name:" + ViewState["Supempname"].ToString() + "<br/> Post:" + ViewState["superdesg"].ToString() + "<br/> ";
                                string body = "Dear Sir/Madam,<br/><br/> Please find attached the updates for TA/DA for the " + ddlMonth.SelectedItem.Text + "/ " + ddlYear.SelectedValue + ". For any queries, kindly contact your reporting manager.<br/><br/> Regards,<br/> PMS Team ";
                                mail.IsBodyHtml = true;
                                mail.Body = body;
                                System.Net.Mail.Attachment attachment;
                                if ((File.Exists(fileName)))
                                {
                                    attachment = new System.Net.Mail.Attachment(fileName);
                                    mail.Attachments.Add(attachment);
                                }
                                System.Net.Mail.Attachment attachment1;
                                SmtpServer.Port = 587;
                                SmtpServer.Credentials = new System.Net.NetworkCredential("PMS.Team@educategirls.ngo", "PMSTeam2018");
                                SmtpServer.EnableSsl = true;



                                SmtpServer.Send(mail);


                            }
                        }

                        #endregion
                    }
                }
            }

        }
        catch
        {

            throw;
        }

    }

    #endregion



}


