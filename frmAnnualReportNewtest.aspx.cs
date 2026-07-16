using ClosedXML.Excel;
using Ionic.Zip;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class frmAnnualReportNewtest : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {




                if (!IsPostBack)
                {
                    LoadYear();
                    LoadUserLeavel();
                    ViewState["1"] = "ss";
                    ViewState["Annual"] = "";
                    ViewState["D2dUser"] = "";

                    lblPlan.Visible = false;
                    ddlTpye.Visible = false;
                    lblGrop.Visible = false;
                    ddlGroup.Visible = false;
                    A1.Visible = false;
                    A2.Visible = false;
                    A3.Visible = false;


                    // btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
                }
                else
                {
                    Response.Redirect("Login.aspx", false);

                }

            }
        }

    }
    //public void LoadYear()
    //{
    //    DataTable dtYear = objComman.Generate_Financial_Year();
    //    objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, "", "Type", "asc", ddlYear, "Type", "ID", "Select");

    //    ddlYear.SelectedIndex = 1;
    //    //}


    //}
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
        string conditions = "";
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
            //if (Convert.ToString(Session["NewDistrictCode"]) == "D73724BDE7734E8B9BAD6C7A1" || Convert.ToString(Session["NewDistrictCode"]) == "2EB646C9A3BA423EB9C8D49E8" || Convert.ToString(Session["NewDistrictCode"]) == "A7A3FE989537496C866994976" || Convert.ToString(Session["NewDistrictCode"]) == "05271CFB65FB47F4A40F09E0F")
            //{

            //    conditions = "StateCode='9A' ";
            //    //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            //    string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State  where   " + conditions + "  order by StateName   ";
            //    DataTable dtState = objMain.LoadData(strQry1);
            //    ChkState.DataSource = dtState;
            //    ChkState.DataTextField = "StateName";
            //    ChkState.DataValueField = "StateCode";
            //    ChkState.DataBind();
            //}
            //else if (Convert.ToString(Session["NewDistrictCode"]) == "232ADC6360FD409EA75CAA999" || Convert.ToString(Session["NewDistrictCode"]) == "E53EE7DDC0B4406894B833429" || Convert.ToString(Session["NewDistrictCode"]) == "36BA800E6C8C428DBCFDED216" || Convert.ToString(Session["NewDistrictCode"]) == "1A38DEEF72904FDFB101083C1")

            //{
            //    conditions = "StateCode='9C' ";
            //    //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            //    string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State  where   " + conditions + "  order by StateName   ";
            //    DataTable dtState = objMain.LoadData(strQry1);
            //    ChkState.DataSource = dtState;
            //    ChkState.DataTextField = "StateName";
            //    ChkState.DataValueField = "StateCode";
            //    ChkState.DataBind();
            //}
            //else if (Convert.ToString(Session["NewDistrictCode"]) == "3735E3B1B5B941A885EA90956" || Convert.ToString(Session["NewDistrictCode"]) == "A80CFE7B2E874932B057FACDB" || Convert.ToString(Session["NewDistrictCode"]) == "5C018B3D24E44511918C2CDB3")

            //{
            //    conditions = "StateCode='9B' ";
            //    //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            //    string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State  where   " + conditions + "  order by StateName   ";
            //    DataTable dtState = objMain.LoadData(strQry1);
            //    ChkState.DataSource = dtState;
            //    ChkState.DataTextField = "StateName";
            //    ChkState.DataValueField = "StateCode";
            //    ChkState.DataBind();
            //}
            //else
            //{
            //    conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //    //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            //    string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State  where   " + conditions + "  order by StateName   ";
            //    DataTable dtState = objMain.LoadData(strQry1);
            //    ChkState.DataSource = dtState;
            //    ChkState.DataTextField = "StateName";
            //    ChkState.DataValueField = "StateCode";
            //    ChkState.DataBind();
            //}
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

            if (Session["user_level_Role"].ToString() == "2")
            {
                foreach (ListItem item in chkDistrict.Items)
                {

                    item.Selected = true;

                }
                ddlDistrict_SelectedIndexChanged(ddlState, null);
            }
            //foreach (ListItem item in chkDistrict.Items)
            //{

            //    item.Selected = true;
            //    break;
            //}
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
        string conditions = "";

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
        ddlPanchayat.Items.Clear();
        chkVillage.Items.Clear();
    }

    protected void ddlTpye_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["Annual"] = "";
        ViewState["D2dUser"] = "";
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        string conditions = "";
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
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2023)
        {
            lblPlan.Visible = false;
            ddlTpye.Visible = false;
            lblGrop.Visible = false;
            ddlGroup.Visible = false;
            A1.Visible = false;
            A2.Visible = false;
            A3.Visible = false;
        }
        else
        {
            lblPlan.Visible = true;
            ddlTpye.Visible = true;
            lblGrop.Visible = true;
            ddlGroup.Visible = true;
            A1.Visible = true;
            A2.Visible = true;
            A3.Visible = true;
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPanchayat.Items.Clear();
        chkVillage.Items.Clear();
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
    public void FillCBBock()
    {
        string conditions = "";
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



        ddlPanchayat.Items.Clear();
        chkVillage.Items.Clear();

    }
    public void FillCBCluster()
    {
        string conditions = "";
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

        conditions = "DistrictCode in(" + ddlDistrict + ")  and BlockCode in(" + ddlBlock + ")";
        string strQry = "  SELECT PanchayatCode, dbo.TitleCase(upper(PanchayatName))  as PanchayatName FROM mstPanchayat where " + conditions + "  order by PanchayatName   ";
        dtDistrict = objMain.LoadData(strQry);



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
        string conditions = "";
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

        conditions = "DistrictCode in(" + ddlDistrict + ")  and BlockCode in(" + ddlBlock + ") and  PanchayatCode in(" + ddlPhan + ")";

        //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        //objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--All-");

        string strQry = "  SELECT VillageCode, dbo.TitleCase(upper(VillageName))  as VillageName FROM mst5Village where " + conditions + "  order by VillageName   ";
        DataTable dtDistrict = objMain.LoadData(strQry);

        chkVillage.DataSource = dtDistrict;
        chkVillage.DataTextField = "VillageName";
        chkVillage.DataValueField = "VillageCode";
        chkVillage.DataBind();


    }

    public void MultipuExeclTrack2025()
    {
        DataSet dt5 = ViewState["SAC"] as DataSet;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\AnnalPlanExcel2025.xlsx");
        var ws = wb.Worksheet(1);
        var ws1 = wb.Worksheet(2);
        //var ws1 = wb.Worksheet(2);
        //var ws3 = wb.Worksheet(3);

        //dt.Columns.Remove("rownNO");
        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("rownNO");
        DataTable dt = dt5.Tables[0];
        ws.Cell(5, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 4;
        string str = "A5:BF" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        DataTable dt1 = dt5.Tables[1];
        ws1.Cell(5, 1).InsertData(dt1.Rows);
        Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 4;
        string str1 = "A5:H" + ii1;
        ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);




        filepath = StartupPath + "\\AnnualPlanExcel " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
    public void MultipuExeclTrack2024()
    {
        DataSet dt5 = ViewState["SAC"] as DataSet;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\AnnalPlanExcel2024.xlsx");
        var ws = wb.Worksheet(1);
        var ws1 = wb.Worksheet(2);
        //var ws1 = wb.Worksheet(2);
        //var ws3 = wb.Worksheet(3);

        //dt.Columns.Remove("rownNO");
        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("rownNO");
        DataTable dt = dt5.Tables[0];
        ws.Cell(5, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 4;
        string str = "A5:BQ" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        DataTable dt1 = dt5.Tables[1];
        ws1.Cell(5, 1).InsertData(dt1.Rows);
        Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 4;
        string str1 = "A5:H" + ii1;
        ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);




        filepath = StartupPath + "\\AnnualPlanExcel " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
    public void MultipuExeclTrack333()
    {
        DataSet dt5 = ViewState["SAC"] as DataSet;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\AnnalPlanExcel.xlsx");
        var ws = wb.Worksheet(1);
        var ws1 = wb.Worksheet(2);
        //var ws1 = wb.Worksheet(2);
        //var ws3 = wb.Worksheet(3);

        //dt.Columns.Remove("rownNO");
        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("rownNO");
        DataTable dt = dt5.Tables[0];
        ws.Cell(5, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 4;
        string str = "A5:BV" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        DataTable dt1 = dt5.Tables[1];
        ws1.Cell(5, 1).InsertData(dt1.Rows);
        Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 4;
        string str1 = "A5:H" + ii1;
        ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);




        filepath = StartupPath + "\\AnnualPlanExcel " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
    public void LoadAnnualDataDeatilsHotSpot444(int Flag)
    {
        string conditions = "";

        string conditions1 = "";

        string ddlDistrict = "";
        string ddlDistrictNew = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
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

                ddlDistrictNew = item.Value;
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
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + "    mstCluster.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
            conditions1 = conditions1 + "    mst2District.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mstCluster.StateCode in(" + ddlStatecode + ") ";
            conditions1 = conditions1 + "  and  mst2District.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mstCluster.DistrictCode in(" + ddlDistrict + ") ";
            conditions1 = conditions1 + " and mst2District.DistrictCode in(" + ddlDistrict + ") ";
        }

        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mstCluster.BlockCode in(" + ddlBlock + ") ";
        }





        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con", conditions),
             new SqlParameter("@Con1", conditions1),
         new SqlParameter("@Dist",ddlDistrictNew ),



        };
        DataSet dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnnaualPlanDataSummryDownloadFinal]", cmdParameters);
        // DataTable dt = objMain.LoadAnnaulPlanRowData(conditions, Flag);







        ViewState["SAC"] = dt;
        if (dt.Tables[0].Rows.Count > 0)
        {
            objMain.ReportDownload("Annual Plan Target Sheet", "Annual Plan", Convert.ToString(Session["username"]));
            MultipuExeclTrack333();
        }





    }

    public void LoadAnnualDataDeatilsHotSpot2024(int Flag)
    {
        string conditions = "";

        string conditions1 = "";

        string ddlDistrict = "";
        string ddlDistrictNew = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
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

                ddlDistrictNew = item.Value;
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
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + "    mstCluster.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
            conditions1 = conditions1 + "    mst2District.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mstCluster.StateCode in(" + ddlStatecode + ") ";
            conditions1 = conditions1 + "  and  mst2District.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mstCluster.DistrictCode in(" + ddlDistrict + ") ";
            conditions1 = conditions1 + " and mst2District.DistrictCode in(" + ddlDistrict + ") ";
        }

        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mstCluster.BlockCode in(" + ddlBlock + ") ";
        }





        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con", conditions),
             new SqlParameter("@Con1", conditions1),
         new SqlParameter("@Dist",ddlDistrictNew ),



        };

        DataSet dt = null;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
        {
            dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnnaualPlanDataSummryDownloadFinal2025]", cmdParameters);
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) >= 2025)
        {
            dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnnaualPlanDataSummryDownloadFinal20252026]", cmdParameters);
        }
        else

        {
            dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnnaualPlanDataSummryDownloadFinal2024]", cmdParameters);
        }
        // DataTable dt = objMain.LoadAnnaulPlanRowData(conditions, Flag);







        ViewState["SAC"] = dt;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2025)
        {
            if (dt.Tables[0].Rows.Count > 0)
            {
                objMain.ReportDownload("Annual Plan Target Sheet", "Annual Plan", Convert.ToString(Session["username"]));
                MultipuExeclTrack2025();
            }
        }
        else
        {
            if (dt.Tables[0].Rows.Count > 0)
            {
                objMain.ReportDownload("Annual Plan Target Sheet", "Annual Plan", Convert.ToString(Session["username"]));
                MultipuExeclTrack2024();
            }

        }



    }
    protected void LnkAnnualPlan_OnClick(object sender, EventArgs e)
    {
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');
        string ddlDistrict = "";


        if (Convert.ToInt32(Year1[0]) >= 2024)
        {
            int icount = 0;
            foreach (ListItem item in chkDistrict.Items)
            {
                if (item.Selected)
                {

                    ddlDistrict += "'" + item.Value + "'" + ",";

                    icount = icount + 1;
                }
            }

            if (ddlDistrict.Length > 0)
            {
                ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
                if (icount > 1)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select one district ')</script>", false);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  district ')</script>", false);
                return;
            }

            LoadAnnualDataDeatilsHotSpot2024(1);
        }
        else if (Convert.ToInt32(Year1[0]) == 2023)
        {
            int icount = 0;
            foreach (ListItem item in chkDistrict.Items)
            {
                if (item.Selected)
                {

                    ddlDistrict += "'" + item.Value + "'" + ",";

                    icount = icount + 1;
                }
            }

            if (ddlDistrict.Length > 0)
            {
                ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
                if (icount > 1)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select one district ')</script>", false);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  district ')</script>", false);
                return;
            }

            LoadAnnualDataDeatilsHotSpot444(1);
        }

        else if (ddlTpye.SelectedIndex > 0)
        {

            if (Convert.ToInt32(Year1[0]) >= 2021)
            {
                ViewState["1"] = 518;
                LoadAnnualData2021(Convert.ToInt32(ddlTpye.SelectedValue));

            }
            else
            {
                ViewState["1"] = 8;
                LoadAnnualData(Convert.ToInt32(ddlTpye.SelectedValue));

            }

        }


    }
    protected void LnkAnnualPlanFC_OnClick(object sender, EventArgs e)
    {

        if (ddlTpye.SelectedIndex > 0)
        {
            ViewState["1"] = 9;
            string Year = ddlYear.SelectedItem.Text;
            string[] Year1 = Year.Split('-');
            if (Convert.ToInt32(Year1[0]) >= 2021)
            {
                ViewState["1"] = 714;
                LoadAnnualSheetData2021(Convert.ToInt32(ddlTpye.SelectedValue));
            }
            else if (Convert.ToInt32(Year1[0]) >= 2020)
            {
                ViewState["1"] = 9;
                LoadAnnualDataFC(Convert.ToInt32(ddlTpye.SelectedValue));
            }
            else
            {

                LoadAnnualDataDetailOD(1);
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Plan Type ')</script>", false);
        }


    }

    public void AnnaualFCReportOld(Int32 Flag)
    {
        string conditions = "";

        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
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
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }

        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }




        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Villagecode",conditions),


        };
        DataTable dataTable = null;


        dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[GetAnualPlanFCWiseReportNew]", cmdParameters);

        ViewState["dt"] = dataTable;
        if (dataTable.Rows.Count > 0)
        {

            ExportToCSVFile(dataTable, "AnualPlanFC");


            return;
        }
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();


    }
    public void LoadAnnualSheetData2021(int Flag)
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



        string condition = string.Empty;
        if (Flag == 1)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "    where mst2District.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (ddlStatecode.Length > 0)
            {
                conditions += " and mst2District.StateCode in(" + ddlStatecode + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and mst2District.DistrictCode in(" + ddlDistrict + ") ";

            }


        }


        if (Flag == 2 || Flag == 3)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "    where mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (ddlStatecode.Length > 0)
            {
                conditions += " and mst5Village.StateCode in(" + ddlStatecode + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";

            }

            if (ddlBlock.Length > 0)
            {

                conditions += " and mst5Village.BlockCode in(" + ddlBlock + ") ";


            }
            if (ddlPhan.Length > 0)
            {
                conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
            }
            if (ddlVillage.Length > 0)
            {
                conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
            }
        }




        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),
                new SqlParameter("@Flag",Flag),
                new SqlParameter("@FYear",ddlYear.SelectedValue),


        };
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnnaulplanSheetFCshhet2023]", cmdParameters);

        //  GenerateExcelNewFCString("");

        ViewState["Annual"] = dt;
        GV_DynamicGrid.Visible = true;
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();




        if (dt.Rows.Count > 500)
        {
            DataTable dtn = dt.Clone();
            int i = 0;
            int count = 100;
            foreach (DataRow row in dt.Rows)
            {
                if (i < count)
                {
                    dtn.ImportRow(row);
                    i++;
                }
                if (i > count)
                    break;
            }
            GV_DynamicGrid.DataSource = dtn;
            GV_DynamicGrid.DataBind();
        }
        else
        {
            GV_DynamicGrid.DataSource = dt;
            GV_DynamicGrid.DataBind();
        }




    }
    public void LoadAnnualDataFC(int Flag)
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



        string condition = string.Empty;
        if (Flag == 1)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "    where mst2District.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (ddlStatecode.Length > 0)
            {
                conditions += " and mst2District.StateCode in(" + ddlStatecode + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and mst2District.DistrictCode in(" + ddlDistrict + ") ";

            }


        }


        if (Flag == 2 || Flag == 3)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "    where mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (ddlStatecode.Length > 0)
            {
                conditions += " and mst5Village.StateCode in(" + ddlStatecode + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";

            }

            if (ddlBlock.Length > 0)
            {

                conditions += " and mst5Village.BlockCode in(" + ddlBlock + ") ";


            }
            if (ddlPhan.Length > 0)
            {
                conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
            }
            if (ddlVillage.Length > 0)
            {
                conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
            }
        }




        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),
                new SqlParameter("@Flag",Flag),


        };
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnnaulplanSheetFCshhet]", cmdParameters);

        //  GenerateExcelNewFCString("");

        ViewState["Annual"] = dt;
        GV_DynamicGrid.Visible = true;
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();




        if (dt.Rows.Count > 1500)
        {
            GV_DynamicGrid.DataSource = dt;
            GV_DynamicGrid.DataBind();
        }
        else
        {
            GV_DynamicGrid.DataSource = dt;
            GV_DynamicGrid.DataBind();
        }




    }
    public void LoadAnnualData2021(int Flag)
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



        string condition = string.Empty;
        if (Flag == 1)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "    where mst2District.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (ddlStatecode.Length > 0)
            {
                conditions += " and mst2District.StateCode in(" + ddlStatecode + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and mst2District.DistrictCode in(" + ddlDistrict + ") ";

            }


        }


        if (Flag == 2 || Flag == 3)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "    where mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (ddlStatecode.Length > 0)
            {
                conditions += " and mst5Village.StateCode in(" + ddlStatecode + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";

            }

            if (ddlBlock.Length > 0)
            {

                conditions += " and mst5Village.BlockCode in(" + ddlBlock + ") ";


            }
            if (ddlPhan.Length > 0)
            {
                conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
            }
            if (ddlVillage.Length > 0)
            {
                conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
            }
        }




        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),
                new SqlParameter("@Flag",Flag),
                    new SqlParameter("@FYear",ddlYear.SelectedValue),

        };
        DataTable dt = null;

        DataTable dt1 = null;

        DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnnaulplaSheet2023]", cmdParameters);


        dt = ds.Tables[0];
        dt1 = ds.Tables[1];
        ViewState["Annual"] = dt1;
        GV_DynamicGrid.Visible = true;
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();




        if (dt.Rows.Count > 100)
        {
            DataTable dtn = dt.Clone();
            int i = 0;
            int count = 100;
            foreach (DataRow row in dt.Rows)
            {
                if (i < count)
                {
                    dtn.ImportRow(row);
                    i++;
                }
                if (i > count)
                    break;
            }
            GV_DynamicGrid.DataSource = dtn;
            GV_DynamicGrid.DataBind();
        }
        else
        {
            GV_DynamicGrid.DataSource = dt;
            GV_DynamicGrid.DataBind();
        }




    }
    public void LoadAnnualData(int Flag)
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



        string condition = string.Empty;
        if (Flag == 1)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "    where mst2District.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (ddlStatecode.Length > 0)
            {
                conditions += " and mst2District.StateCode in(" + ddlStatecode + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and mst2District.DistrictCode in(" + ddlDistrict + ") ";

            }


        }


        if (Flag == 2 || Flag == 3)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "    where mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (ddlStatecode.Length > 0)
            {
                conditions += " and mst5Village.StateCode in(" + ddlStatecode + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";

            }

            if (ddlBlock.Length > 0)
            {

                conditions += " and mst5Village.BlockCode in(" + ddlBlock + ") ";


            }
            if (ddlPhan.Length > 0)
            {
                conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
            }
            if (ddlVillage.Length > 0)
            {
                conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
            }
        }




        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),
                new SqlParameter("@Flag",Flag),

        };
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnnaulplaSheetDC]", cmdParameters);



        ViewState["Annual"] = dt;
        GV_DynamicGrid.Visible = true;
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();




        if (dt.Rows.Count > 1500)
        {
            GV_DynamicGrid.DataSource = dt;
            GV_DynamicGrid.DataBind();
        }
        else
        {
            GV_DynamicGrid.DataSource = dt;
            GV_DynamicGrid.DataBind();
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

    private void ExporttoExcel2022(GridView Gv, DataTable table, string FileName)
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
              "style='font-size:10.0pt; font-family:Calibri; background:white;'> ");
            HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
            String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
            HttpContext.Current.Response.Write("<th class='header' colspan='7' style='" + HeaderStyle + "  width:2%;'></th>");
            HttpContext.Current.Response.Write("<th class='header'  colspan='3' style='" + HeaderStyle + "  width:2%;'> Univers OOSG</th>");
            HttpContext.Current.Response.Write("<th class='header'  colspan='3' style='" + HeaderStyle + "  width:2%;'> OPS Target-OOSG</th>");

            HttpContext.Current.Response.Write("</tr>");

            HttpContext.Current.Response.Write("<tr>");
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


    private void GenerateExcelNewSUmmary(string FIleName)
    {
        try
        {

            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                FIleName = "AnnualPlanDistrictSummary";
            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                FIleName = "AnnualPlanBlockSummary";
            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                FIleName = "AnnualPlanClusterSummary";
            }
            DataTable dt = Session["D2dUser"] as DataTable;
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
                if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
                {
                    sw.Write("<tr>");
                    sw.Write("<td colspan='35' ' style='text-align:Center;border:.2pt solid windowtext;'>Annual Plan District Summary </td>");
                    sw.Write("</tr>");
                }
                if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
                {
                    sw.Write("<tr>");
                    sw.Write("<td colspan='35' ' style='text-align:Center;border:.2pt solid windowtext;'>Annual Plan Block Summary </td>");
                    sw.Write("</tr>");
                }
                if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
                {
                    sw.Write("<tr>");
                    sw.Write("<td colspan='35' ' style='text-align:Center;border:.2pt solid windowtext;'>Annual Plan Cluster Summary </td>");
                    sw.Write("</tr>");
                }
                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
                sw.Write("<tr style='font-width:bold;'>");
                int columnscount = GV_DynamicGrid.HeaderRow.Cells.Count;

                for (int j = 0; j < columnscount; j++)
                {
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> " + GV_DynamicGrid.HeaderRow.Cells[j].Text + "</th>");
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
                sw.Write("<tr>");
                for (int J = 0; J < 1; J++)
                {
                    if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
                    {
                        #region
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0 || c == 1 || c == 2)
                            {
                                if (c == 2)
                                {
                                    sw.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                                }
                                else
                                {
                                    sw.Write("<td style='" + RowStyle + "'></td>");
                                }
                            }
                            else
                            {
                                string Col = "[" + dt.Columns[c].ColumnName + "]";
                                int sum = 0;
                                if (Convert.ToString(dt.Rows[J][dt.Columns[c].ColumnName]) == "")
                                {
                                }
                                else
                                {
                                    sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));
                                }
                                sw.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                        #endregion
                    }
                    if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
                    {
                        #region
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0 || c == 1 || c == 2 || c == 3 || c == 4 || c == 5 || c == 6)
                            {
                                if (c == 6)
                                {
                                    sw.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                                }
                                else
                                {
                                    sw.Write("<td style='" + RowStyle + "'></td>");
                                }
                            }
                            else
                            {
                                string Col = "[" + dt.Columns[c].ColumnName + "]";
                                int sum = 0;
                                if (Convert.ToString(dt.Rows[J][dt.Columns[c].ColumnName]) == "")
                                {
                                }
                                else
                                {
                                    sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));
                                }
                                sw.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                        #endregion
                    }
                    if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
                    {
                        #region
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0 || c == 1 || c == 2 || c == 3 || c == 4 || c == 5 || c == 6 || c == 7 || c == 8)
                            {
                                if (c == 8)
                                {
                                    sw.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                                }
                                else
                                {
                                    sw.Write("<td style='" + RowStyle + "'></td>");
                                }
                            }
                            else
                            {
                                string Col = "[" + dt.Columns[c].ColumnName + "]";
                                int sum = 0;
                                if (Convert.ToString(dt.Rows[J][dt.Columns[c].ColumnName]) == "")
                                {
                                }
                                else
                                {
                                    sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));
                                }
                                sw.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                        #endregion
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
    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (ViewState["1"].ToString() == "8" && Convert.ToString(ViewState["Annual"]) != "")
        {

            GenerateExcelNew("StaffTrainingPlanning");
        }
        if (ViewState["1"].ToString() == "518" && Convert.ToString(ViewState["Annual"]) != "")
        {

            GenerateExcelNew2021("StaffTrainingPlanning");
        }

        if (ViewState["1"].ToString() == "9" && Convert.ToString(ViewState["Annual"]) != "")
        {

            GenerateExcelNewFC("StaffTrainingPlanning");
            // GenerateExcelNewFCString("StaffTrainingPlanning"); ViewState["D2dUser"] 
        }
        if (ViewState["1"].ToString() == "714")
        {
            if (Convert.ToInt32(ddlYear.SelectedValue) >= 2022)
            {
                GenerateExcelNewFC2023("StaffTrainingPlanning");
            }
            else
            {
                GenerateExcelNewFC2021("StaffTrainingPlanning");
            }

            // GenerateExcelNewFCString("StaffTrainingPlanning"); ViewState["D2dUser"] 
        }

        if (ViewState["1"].ToString() == "100")
        {
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                DataTable dt = (DataTable)Session["D2dUser"];
                ExportToCSVFileNew(dt, "AnnualPlanClusterSummary");
                // MultipuExeclGKPGKPAssessmentSummary();
            }
            else
            {
                DataTable dt = (DataTable)Session["D2dUser"];
                GenerateExcelNewSUmmary("DistrictWise");
            }
        }

        if (ViewState["1"].ToString() == "101")
        {
            DataTable dt = (DataTable)ViewState["D2dUser"];
            ExporttoExcel(GV_DynamicGrid, dt, "BlockWise");
        }
        if (ViewState["1"].ToString() == "102")
        {
            DataTable dt = (DataTable)ViewState["D2dUser"];
            ExporttoExcel(GV_DynamicGrid, dt, "ClusterWise");
        }
        if (ViewState["1"].ToString() == "710" && Convert.ToString(ViewState["D2dUser"]) != "")
        {
            DataTable dt = (DataTable)ViewState["D2dUser"];
            ExporttoExcel2022(GV_DynamicGrid, dt, "EnrollmentOPsTarget");
        }
    }
    public void MultipuExeclGKPGKPAssessmentSummary()
    {
        DataTable dtMain1 = ViewState["D2dUser"] as DataTable;
        dtMain1 = ViewState["D2dUser"] as DataTable;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\AnnualPlanClusterSummary.xlsx");
        var ws = wb.Worksheet(1);

        DataTable dt = dtMain1;

        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("RowNo");
        ws.Cell(3, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 3;
        string str = "A3:BH" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);



        filepath = StartupPath + "\\AnnualPlanClusterSummary " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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

    private void GenerateExcelNew(string FIleName)
    {
        try
        {


            if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
            {
                FIleName = "StaffTrainingTraget";
            }
            if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
            {
                FIleName = "VillageLevelPlan";
            }
            if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
            {
                FIleName = "SchoolLevelPlan";
            }
            DataTable dt = ViewState["Annual"] as DataTable;
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
                if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
                {
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='15' ' style='text-align:Center;border:.2pt solid windowtext;'>Annual Plan- Staff Training Planning </td>");

                    HttpContext.Current.Response.Write("</tr>");
                    HttpContext.Current.Response.Write("<td colspan='4'  style='text-align:Left;border:.2pt solid windowtext;'>Entry Date: …..............</td>");
                    HttpContext.Current.Response.Write("<td colspan='11' style='text-align:Left;border:.2pt solid windowtext;'>Entry Done by:…............</td>");
                    HttpContext.Current.Response.Write("</tr>");
                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header' rowspan='2'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Activity</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'> Month</th>");
                    HttpContext.Current.Response.Write("</tr>");

                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    //HttpContext.Current.Response.Write("<th class='header' rowspan='2'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    //HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    //HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Activity</th>");

                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Apr</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>May</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jun</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jul</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Aug</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Sep</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Oct</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Nov</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Dec</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jan</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Feb</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Mar</th>");

                    HttpContext.Current.Response.Write("</tr>");
                }


                if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
                {

                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='23' ' style='text-align:Center;border:.2pt solid windowtext;'>Annual Plan - Village Level Planning</td>");

                    HttpContext.Current.Response.Write("</tr>");
                    HttpContext.Current.Response.Write("<td colspan='7'  style='text-align:Left;border:.2pt solid windowtext;'>Entry Date: …..............</td>");
                    HttpContext.Current.Response.Write("<td colspan='8' style='text-align:Left;border:.2pt solid windowtext;'>Planing Date:…............</td>");
                    HttpContext.Current.Response.Write("<td colspan='8' style='text-align:Left;border:.2pt solid windowtext;'>Entry Done by:…............</td>");
                    HttpContext.Current.Response.Write("</tr>");
                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header' rowspan='2'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> EG Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>  EG Block Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Admin Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>  Admin Block Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Cluster Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>  Cluster  Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Village Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>  Village  Code</th>");

                    HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Activity</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'> Month</th>");
                    HttpContext.Current.Response.Write("</tr>");

                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    //HttpContext.Current.Response.Write("<th class='header' rowspan='2'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    //HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    //HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Activity</th>");

                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Apr</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>May</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jun</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jul</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Aug</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Sep</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Oct</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Nov</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Dec</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jan</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Feb</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Mar</th>");

                    HttpContext.Current.Response.Write("</tr>");
                }


                if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
                {

                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='25' ' style='text-align:Center;border:.2pt solid windowtext;'>Annual Plan -School Level Planning</td>");

                    HttpContext.Current.Response.Write("</tr>");
                    HttpContext.Current.Response.Write("<td colspan='7'  style='text-align:Left;border:.2pt solid windowtext;'>Entry Date: …..............</td>");
                    HttpContext.Current.Response.Write("<td colspan='8' style='text-align:Left;border:.2pt solid windowtext;'>Planing Date:…............</td>");

                    HttpContext.Current.Response.Write("<td colspan='10' style='text-align:Left;border:.2pt solid windowtext;'>Entry Done by:…............</td>");
                    HttpContext.Current.Response.Write("</tr>");
                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header' rowspan='2'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> EG Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>  EG Block Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Admin Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>  Admin Block Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Cluster Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>  Cluster  Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Village Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>  Village  Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> School Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Dise Code</th>");

                    HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Activity</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'> Month</th>");
                    HttpContext.Current.Response.Write("</tr>");

                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    //HttpContext.Current.Response.Write("<th class='header' rowspan='2'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    //HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    //HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Activity</th>");

                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Apr</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>May</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jun</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jul</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Aug</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Sep</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Oct</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Nov</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Dec</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jan</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Feb</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Mar</th>");

                    HttpContext.Current.Response.Write("</tr>");
                }
                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";






                for (int i = 0; i < dt.Rows.Count; i++)
                {



                    HttpContext.Current.Response.Write("<tr>");

                    if (dt.Rows[i]["Activity"].ToString() == "PMS Generated Targets")
                    {
                        HttpContext.Current.Response.Write("<td colspan='15' ' style='text-align:Center;border:.2pt solid windowtext;'>" + dt.Rows[i]["Activity"].ToString() + " </td>");
                    }
                    else
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {


                            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");


                        }
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


    private void GenerateExcelNew2021(string FIleName)
    {
        try
        {


            if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
            {
                FIleName = "StaffTrainingTraget";
            }
            if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
            {
                FIleName = "VillageLevelPlan";
            }
            if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
            {
                FIleName = "SchoolLevelPlan";
            }
            DataTable dt = ViewState["Annual"] as DataTable;
            if (dt.Rows.Count > 0)
            {

                string Fullfilename = "" + FIleName + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";
                string fileName = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + Fullfilename + "");
                StreamWriter sw = new StreamWriter(fileName, false);
                sw.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                sw.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");

                sw.Write("<table  >");
                if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
                {
                    sw.Write("<tr>");
                    sw.Write("<td colspan='15' ' style='text-align:Center;border:.2pt solid windowtext;'>Annual Plan-  District Level Planning </td>");

                    sw.Write("</tr>");
                    sw.Write("<td colspan='4'  style='text-align:Left;border:.3pt solid windowtext;font-weight:700; '>Entry Date: …..............</td>");
                    sw.Write("<td colspan='11' style='text-align:Left;border:.3pt solid windowtext;font-weight:700; '>Entry Done by:…............</td>");
                    sw.Write("</tr>");
                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

                    sw.Write("<tr style='font-width:bold;'>");
                    sw.Write("<th class='header' rowspan='2'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Activity</th>");
                    sw.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'> Month</th>");
                    sw.Write("</tr>");

                    sw.Write("<tr style='font-width:bold;'>");
                    //sw.Write("<th class='header' rowspan='2'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    //sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    //sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Activity</th>");

                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Apr</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>May</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jun</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jul</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Aug</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Sep</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Oct</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Nov</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Dec</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jan</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Feb</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Mar</th>");

                    sw.Write("</tr>");
                }


                if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
                {

                    sw.Write("<tr>");
                    sw.Write("<td colspan='23' ' style='text-align:Center;border:.2pt solid windowtext;'>Annual Plan - Village Level Planning</td>");

                    sw.Write("</tr>");
                    sw.Write("<td colspan='7'  style='text-align:Left;border:.2pt solid windowtext;'>Entry Date: …..............</td>");
                    sw.Write("<td colspan='8' style='text-align:Left;border:.2pt solid windowtext;'>Planing Date:…............</td>");
                    sw.Write("<td colspan='8' style='text-align:Left;border:.2pt solid windowtext;'>Entry Done by:…............</td>");
                    sw.Write("</tr>");
                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

                    sw.Write("<tr style='font-width:bold;'>");
                    sw.Write("<th class='header' rowspan='2'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> EG Block Name</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>  EG Block Code</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Admin Block Name</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>  Admin Block Code</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Cluster Name</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>  Cluster  Code</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Village Name</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>  Village  Code</th>");

                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Activity</th>");
                    sw.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'> Month</th>");
                    sw.Write("</tr>");

                    sw.Write("<tr style='font-width:bold;'>");
                    //sw.Write("<th class='header' rowspan='2'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    //sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    //sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Activity</th>");

                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Apr</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>May</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jun</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jul</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Aug</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Sep</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Oct</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Nov</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Dec</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jan</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Feb</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Mar</th>");

                    sw.Write("</tr>");
                }


                if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
                {

                    sw.Write("<tr>");
                    sw.Write("<td colspan='25' ' style='text-align:Center;border:.2pt solid windowtext;'>Annual Plan -School Level Planning</td>");

                    sw.Write("</tr>");
                    sw.Write("<td colspan='7'  style='text-align:Left;border:.2pt solid windowtext;'>Entry Date: …..............</td>");
                    sw.Write("<td colspan='8' style='text-align:Left;border:.2pt solid windowtext;'>Planing Date:…............</td>");

                    sw.Write("<td colspan='10' style='text-align:Left;border:.2pt solid windowtext;'>Entry Done by:…............</td>");
                    sw.Write("</tr>");
                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

                    sw.Write("<tr style='font-width:bold;'>");
                    sw.Write("<th class='header' rowspan='2'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> EG Block Name</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>  EG Block Code</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Admin Block Name</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>  Admin Block Code</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Cluster Name</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>  Cluster  Code</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Village Name</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>  Village  Code</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> School Name</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Dise Code</th>");

                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Activity</th>");
                    sw.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'> Month</th>");
                    sw.Write("</tr>");

                    sw.Write("<tr style='font-width:bold;'>");
                    //sw.Write("<th class='header' rowspan='2'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    //sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    //sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Activity</th>");

                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Apr</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>May</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jun</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jul</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Aug</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Sep</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Oct</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Nov</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Dec</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jan</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Feb</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Mar</th>");

                    sw.Write("</tr>");
                }
                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";






                for (int i = 0; i < dt.Rows.Count; i++)
                {



                    sw.Write("<tr>");

                    if (Convert.ToInt32(dt.Rows[i]["LookCode"].ToString()) > 200)
                    {
                        if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
                        {
                            sw.Write("<td colspan='15' ' style='text-align:Center;border:.3pt solid windowtext;font-weight:700;'>" + dt.Rows[i]["Activity"].ToString() + " </td>");
                        }
                        if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
                        {
                            sw.Write("<td colspan='23' ' style='text-align:Center;border:.3pt solid windowtext;font-weight:700;'>" + dt.Rows[i]["Activity"].ToString() + " </td>");
                        }
                        if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
                        {
                            sw.Write("<td colspan='25' ' style='text-align:Center;border:.3pt solid windowtext;font-weight:700;'>" + dt.Rows[i]["Activity"].ToString() + " </td>");
                        }
                    }
                    else
                    {
                        for (int c = 0; c < dt.Columns.Count - 1; c++)
                        {


                            sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");


                        }
                    }
                }
                #region Row1



                #endregion


                sw.Write("</tr>");




                sw.Write("</table>");
                sw.Close();



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



    private void GenerateExcelNewAnnaulDetails(string FIleName)
    {
        try
        {


            if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
            {
                FIleName = "AnnualPlanDistrictDetail";
            }
            if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
            {
                FIleName = "AnnualPlanVillageDetail";
            }
            if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
            {
                FIleName = "AnnualPlanSchoolDetail";
            }
            DataTable dt = ViewState["D2dUser"] as DataTable;
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
                if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
                {
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='17' ' style='text-align:Center;border:.2pt solid windowtext;'>Annual Plan -  District Level Detail</td>");

                    HttpContext.Current.Response.Write("</tr>");
                    HttpContext.Current.Response.Write("<td colspan='17'  style='text-align:Left;border:.2pt solid windowtext;'> Date:" + DateTime.Now + "</td>");

                    HttpContext.Current.Response.Write("</tr>");
                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    //HttpContext.Current.Response.Write("<th class='header' rowspan='2'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    //HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    //HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Activity</th>");
                    HttpContext.Current.Response.Write("<th class='header'   style='" + HeaderStyle + "  width:2%;'> State Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>State Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>District Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'   style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'   style='" + HeaderStyle + "  width:2%;'>Description</th>");

                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Apr</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>May</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jun</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jul</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Aug</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Sep</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Oct</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Nov</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Dec</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jan</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Feb</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Mar</th>");

                    HttpContext.Current.Response.Write("</tr>");
                }


                if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
                {

                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='27'  style='text-align:Center;border:.2pt solid windowtext;'>Annual Plan - Village Level Detail</td>");

                    HttpContext.Current.Response.Write("</tr>");
                    HttpContext.Current.Response.Write("<td colspan='27'  style='text-align:Left;border:.2pt solid windowtext;'> Date :  " + DateTime.Now + "</td>");
                    HttpContext.Current.Response.Write("</tr>");
                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    //HttpContext.Current.Response.Write("<th class='header' rowspan='2'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    //HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    //HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Activity</th>");
                    HttpContext.Current.Response.Write("<th class='header'   style='" + HeaderStyle + "  width:2%;'> State Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>State Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  EG Block Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> EG Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  Admin Block Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Admin Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  Cluster  Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Cluster Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  Panchayat Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Panchayat Name</th>");

                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  Village  Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Village Name</th>");



                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Description</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Apr</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>May</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jun</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jul</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Aug</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Sep</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Oct</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Nov</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Dec</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jan</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Feb</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Mar</th>");

                    HttpContext.Current.Response.Write("</tr>");
                }


                if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
                {

                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='29' ' style='text-align:Center;border:.2pt solid windowtext;'>Annual Plan - School Level Detail</td>");

                    HttpContext.Current.Response.Write("</tr>");
                    HttpContext.Current.Response.Write("<td colspan='29'  style='text-align:Left;border:.2pt solid windowtext;'> Date :  " + DateTime.Now + "</td>");
                    HttpContext.Current.Response.Write("</tr>");
                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";


                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    //HttpContext.Current.Response.Write("<th class='header' rowspan='2'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    //HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    //HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Activity</th>");
                    HttpContext.Current.Response.Write("<th class='header'   style='" + HeaderStyle + "  width:2%;'> State Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>State Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  EG Block Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> EG Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  Admin Block Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Admin Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  Cluster  Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Cluster Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  Panchayat Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Panchayat Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  Village  Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Village Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  School Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'   style='" + HeaderStyle + "  width:2%;'> Dise Code</th>");



                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Description</th>");

                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Apr</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>May</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jun</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jul</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Aug</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Sep</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Oct</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Nov</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Dec</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jan</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Feb</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Mar</th>");

                    HttpContext.Current.Response.Write("</tr>");
                }
                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";






                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    HttpContext.Current.Response.Write("<tr>");
                    //HttpContext.Current.Response.Write("<td >Direct</td>");
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

    private void GenerateExcelNewAnnaulTBNeed(string FIleName)
    {
        try
        {



            FIleName = "TeamBalikaTargetAcheivement";

            DataTable dt = ViewState["D2dUser"] as DataTable;
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



                if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
                {

                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='24'  style='text-align:Center;border:.2pt solid windowtext;'>Annual Plan - Village Level Detail</td>");

                    //HttpContext.Current.Response.Write("</tr>");
                    //HttpContext.Current.Response.Write("<td colspan='27'  style='text-align:Left;border:.2pt solid windowtext;'> Date :  " + DateTime.Now + "</td>");
                    //HttpContext.Current.Response.Write("</tr>");
                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    //HttpContext.Current.Response.Write("<th class='header' rowspan='2'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    //HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    //HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Activity</th>");
                    HttpContext.Current.Response.Write("<th class='header'   style='" + HeaderStyle + "  width:2%;'> State Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>State Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  EG Block Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> EG Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  Admin Block Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Admin Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  Cluster  Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Cluster Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  Panchayat Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Panchayat Name</th>");

                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  Village  Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Village Name</th>");



                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> TB Need- Enrolment</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> TB Need- Learning</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>TB Need Enrolment +Learning</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Total TB Need</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>[TB for Enrollment]</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>[TB for Learning]</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>[TB for Nrolment+Learning]</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>[Total Working TB]</th>");


                    HttpContext.Current.Response.Write("</tr>");
                }



                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";






                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    HttpContext.Current.Response.Write("<tr>");
                    //HttpContext.Current.Response.Write("<td >Direct</td>");
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
    private void GenerateExcelNewAnnaulDetailsString(string FIleName)
    {
        try
        {


            if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
            {
                FIleName = "AnnualPlanDistrictDetail";
            }
            if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
            {
                FIleName = "AnnualPlanVillageDetail";
            }
            if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
            {
                FIleName = "AnnualPlanSchoolDetail";
            }
            string Fullfilename1 = "" + FIleName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";
            string fileName = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + Fullfilename1 + "");

            DataTable dt = ViewState["D2dUser"] as DataTable;
            if (dt.Rows.Count > 0)
            {
                StreamWriter sw = new StreamWriter(fileName, false);
                //HttpContext.Current.Response.Clear();
                //HttpContext.Current.Response.ClearContent();
                //HttpContext.Current.Response.ClearHeaders();
                //HttpContext.Current.Response.Buffer = true;
                //HttpContext.Current.Response.ContentType = "application/ms-excel";
                //HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                //string Fullfilename = "" + FIleName + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


                //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");
                sw.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                //HttpContext.Current.Response.Charset = "utf-8";
                //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                sw.Write("<table  >");
                if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
                {
                    sw.Write("<tr>");
                    sw.Write("<td colspan='17' ' style='text-align:Center;border:.2pt solid windowtext;'>Annual Plan - Staff Training Detail</td>");

                    sw.Write("</tr>");
                    sw.Write("<td colspan='17'  style='text-align:Left;border:.2pt solid windowtext;'> Date:" + DateTime.Now + "</td>");

                    sw.Write("</tr>");
                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

                    sw.Write("<tr style='font-width:bold;'>");
                    //sw.Write("<th class='header' rowspan='2'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    //sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    //sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Activity</th>");
                    sw.Write("<th class='header'   style='" + HeaderStyle + "  width:2%;'> State Code</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>State Name</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>District Code</th>");
                    sw.Write("<th class='header'   style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    sw.Write("<th class='header'   style='" + HeaderStyle + "  width:2%;'>Description</th>");

                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Apr</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>May</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jun</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jul</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Aug</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Sep</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Oct</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Nov</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Dec</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jan</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Feb</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Mar</th>");

                    sw.Write("</tr>");
                }


                if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
                {

                    sw.Write("<tr>");
                    sw.Write("<td colspan='27'  style='text-align:Center;border:.2pt solid windowtext;'>Annual Plan - Village Level Detail</td>");

                    sw.Write("</tr>");
                    sw.Write("<td colspan='27'  style='text-align:Left;border:.2pt solid windowtext;'> Date :  " + DateTime.Now + "</td>");
                    sw.Write("</tr>");
                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

                    sw.Write("<tr style='font-width:bold;'>");
                    //sw.Write("<th class='header' rowspan='2'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    //sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    //sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Activity</th>");
                    sw.Write("<th class='header'   style='" + HeaderStyle + "  width:2%;'> State Code</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>State Name</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  EG Block Code</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> EG Block Name</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  Admin Block Code</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Admin Block Name</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  Cluster  Code</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Cluster Name</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  Panchayat Code</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Panchayat Name</th>");

                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  Village  Code</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Village Name</th>");



                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Description</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Apr</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>May</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jun</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jul</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Aug</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Sep</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Oct</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Nov</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Dec</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jan</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Feb</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Mar</th>");

                    sw.Write("</tr>");
                }


                if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
                {

                    sw.Write("<tr>");
                    sw.Write("<td colspan='29' ' style='text-align:Center;border:.2pt solid windowtext;'>Annual Plan - School Level Detail</td>");

                    sw.Write("</tr>");
                    sw.Write("<td colspan='29'  style='text-align:Left;border:.2pt solid windowtext;'> Date :  " + DateTime.Now + "</td>");
                    sw.Write("</tr>");
                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";


                    sw.Write("<tr style='font-width:bold;'>");
                    //sw.Write("<th class='header' rowspan='2'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    //sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    //sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Activity</th>");
                    sw.Write("<th class='header'   style='" + HeaderStyle + "  width:2%;'> State Code</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>State Name</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  EG Block Code</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> EG Block Name</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  Admin Block Code</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Admin Block Name</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  Cluster  Code</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Cluster Name</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  Panchayat Code</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Panchayat Name</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  Village  Code</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Village Name</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>  School Code</th>");
                    sw.Write("<th class='header'   style='" + HeaderStyle + "  width:2%;'> School Name</th>");



                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Description</th>");

                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Apr</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>May</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jun</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jul</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Aug</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Sep</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Oct</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Nov</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Dec</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jan</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Feb</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Mar</th>");

                    sw.Write("</tr>");
                }
                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";






                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    sw.Write("<tr>");
                    //sw.Write("<td >Direct</td>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {


                        sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");


                    }
                }
                #region Row1



                #endregion


                sw.Write("</tr>");
                sw.Write("</table>");

                sw.Close();
                FileStream fs = null;//, fs2=null;
                try
                {
                    string path1 = Fullfilename1;
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
    protected void GV_DynamicGrid1_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_DynamicGrid.PageIndex = e.NewPageIndex;
        if (Session["Annual"] != null)
        {

            DataTable Dt = Session["Annual"] as DataTable;
            GV_DynamicGrid.DataSource = Dt;
            GV_DynamicGrid.DataBind();
        }
    }
    private void GenerateExcelNewFC(string FIleName)
    {
        try
        {



            DataTable dt = ViewState["Annual"] as DataTable;
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
                {
                    FIleName = "StaffTrainingTarget";
                }
                if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
                {
                    FIleName = "FCVillageLevelPlan";
                }
                if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
                {
                    FIleName = "FCSchoolLevelPlan";
                }

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
                if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
                {
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='18'  style='text-align:Center;border:.2pt solid windowtext;'>Staff Training Target </td>");

                    HttpContext.Current.Response.Write("</tr>");

                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";



                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> District Code</th>");

                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on Enrolment and SMC	</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on D2D	</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on CMM	</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on CV and SC	</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on PMS	</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on FC- PN(L0)	</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on FC- L1	</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on FC- L2	</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on FC- L3	</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on Learning Baseline	</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on Learning Endline	</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on Soft Skills	</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Foundation Day Event for Staff	</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on Bal Sabha and LSG	</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on Phase III Activities	</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on SMC Meeting	</th>");


                    HttpContext.Current.Response.Write("</tr>");
                }


                if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
                {

                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='38' ' style='text-align:Center;border:.2pt solid windowtext;'>Annual Plan - Village Level Planning</td>");

                    HttpContext.Current.Response.Write("</tr>");

                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='10'  style='" + HeaderStyle + "  width:2%;'>Location </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>Target - OOSG</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>Target - OOSB</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'>Target - GSS </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'>Target - MM </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'>Target - Team Balika</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Phase III Activities</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='11' style='" + HeaderStyle + "  width:2%;'>Target - Team Balika Training</th>");

                    HttpContext.Current.Response.Write("</tr>");
                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "   mso-rotate: 90;  width:2%;'>District Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "   mso-rotate: 90;  width:2%;'> District Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "   mso-rotate: 90;  width:2%;'> EG Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'   style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'>  EG Block Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'   style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> Admin Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "   mso-rotate: 90;  width:2%;'>  Admin Block Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'   style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> Cluster Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "   mso-rotate: 90;  width:2%;'>  Cluster  Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> Village Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'>  Village  Code</th>");


                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[5 Years]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[6 Years]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[7 - 14 Years]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Total OOSG]	</th>");

                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[5 Years]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[6 Years]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[7 - 14 Years]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Total OOSB]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Enrolment]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Retention]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Enrolment]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Retention]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[#TBNeed]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[#TB Avail.]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[BO-TB Meeting]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[TB-PRI Meeting]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Community Ownership]	</th>");

                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[TB Training (Enr+SMC)]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[TB Training Bal Sabha and LSG]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[TB Training GKP L0]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[TB Training GKP L1]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[TB Training GKP L2]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[TB Training GKP L3]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[TB Orientation on Soft Skills]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Foundation Day Event for TB]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[TB PRI Training]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[TB One Day Orientation]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[TB Skill Course]	</th>");




                    HttpContext.Current.Response.Write("</tr>");
                }


                if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
                {

                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='28' ' style='text-align:Center;border:.2pt solid windowtext;'>School Level Annual Plan Target Sheet</td>");


                    HttpContext.Current.Response.Write("</tr>");
                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='12'  style='" + HeaderStyle + "  width:2%;'>Location </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Target - SMC</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'>Target - Retention Data Collection</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Target - SIP </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'>Target - Learning Baseline </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Target - GKP</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'>Target - Balsabha</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'>Target - LSG</th>");

                    HttpContext.Current.Response.Write("</tr>");
                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "   mso-rotate: 90;  width:2%;'>District Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "   mso-rotate: 90;  width:2%;'> District Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "   mso-rotate: 90;  width:2%;'> EG Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'   style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'>  EG Block Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'   style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> Admin Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "   mso-rotate: 90;  width:2%;'>  Admin Block Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'   style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> Cluster Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "   mso-rotate: 90;  width:2%;'>  Cluster  Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> Village Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'>  Village  Code</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> School Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'>  School  Code</th>");


                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Is SMC Active]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[#SMC Meeting]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[SMC Meet + ORENT]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Aggregate Retention ]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Retention Data Collection]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Critical Infra Target]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Other Critical Infra Target]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Total SIP Target]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Learning Baseline]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[GKP Need(Yes/No)]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[TB GKP CAPABLE (YES/NO)]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[GKP L0/L1]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[GKP L1/L2]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[GKP L2/L3]	</th>");

                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Bal Sabha]	</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[LSG]	</th>");


                    HttpContext.Current.Response.Write("</tr>");
                }
                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";


                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    HttpContext.Current.Response.Write("<tr>");
                    //HttpContext.Current.Response.Write("<td >Direct</td>");
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
    private void GenerateExcelNewFC2023(string FIleName)
    {
        try
        {



            DataTable dt = ViewState["Annual"] as DataTable;
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
                {
                    FIleName = "StaffTrainingTarget";
                }
                if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
                {
                    FIleName = "FCVillageLevelPlan";
                }
                if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
                {
                    FIleName = "FCSchoolLevelPlan";
                }

                string Fullfilename = "" + FIleName + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";

                string fileName = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + Fullfilename + "");
                StreamWriter sw = new StreamWriter(fileName, false);
                sw.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                sw.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");


                sw.Write("<table  >");
                if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
                {
                    sw.Write("<tr>");
                    sw.Write("<td colspan='21'  style='text-align:Center;border:.2pt solid windowtext;'>Staff Training Target </td>");

                    sw.Write("</tr>");

                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";


                    sw.Write("<tr style='font-width:bold;'>");

                    sw.Write("<th class='header' colspan='2'  style='" + HeaderStyle + "  width:2%;'>Location </th>");
                    sw.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>PMS Generated Targets</th>");
                    sw.Write("<th class='header' colspan='11' style='" + HeaderStyle + "  width:2%;'>User Entry- Online Trainings(#Participants)</th>");
                    sw.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Other Target Entry</th>");


                    sw.Write("</tr>");

                    sw.Write("<tr style='font-width:bold;'>");
                    int columnscount = dt.Columns.Count;


                    for (int j = 0; j < columnscount; j++)
                    {      //write in new column

                        sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>" + dt.Columns[j].ColumnName + "</th>");
                    }
                    sw.Write("</tr>");
                    //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> District Code</th>");

                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on Enrolment and SMC	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on D2D	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on CMM	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on CV and SC	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on PMS	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on FC- PN(L0)	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on FC- L1	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on FC- L2	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on FC- L3	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on Learning Baseline	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on Learning Endline	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on Soft Skills	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Foundation Day Event for Staff	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on Bal Sabha and LSG	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on Phase III Activities	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on SMC Meeting	</th>");



                }


                if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
                {

                    sw.Write("<tr>");
                    sw.Write("<td colspan='48' ' style='text-align:Center;border:.2pt solid windowtext;'>Annual Plan - Village Level Planning</td>");

                    sw.Write("</tr>");

                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
                    sw.Write("<tr style='font-width:bold;'>");
                    sw.Write("<th class='header' colspan='10'  style='" + HeaderStyle + "  width:2%;'>Location </th>");
                    sw.Write("<th class='header' colspan='13' style='" + HeaderStyle + "  width:2%;'>PMS Generated Target</th>");
                    sw.Write("<th class='header' colspan='19' style='" + HeaderStyle + "  width:2%;'>Village Level Targets</th>");

                    sw.Write("<th class='header' colspan='6' style='" + HeaderStyle + "  width:2%;'>User Entry- TB Training Entry</th>");


                    sw.Write("</tr>");
                    sw.Write("<tr style='font-width:bold;'>");
                    int columnscount = dt.Columns.Count;

                    for (int j = 0; j < columnscount; j++)
                    {      //write in new column

                        sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>" + dt.Columns[j].ColumnName + "</th>");
                    }



                    sw.Write("</tr>");
                }


                if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
                {

                    sw.Write("<tr>");
                    sw.Write("<td colspan='28' ' style='text-align:Center;border:.2pt solid windowtext;'>School Level Annual Plan Target Sheet</td>");


                    sw.Write("</tr>");
                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

                    sw.Write("<tr style='font-width:bold;'>");
                    sw.Write("<th class='header' colspan='12'  style='" + HeaderStyle + "  width:2%;'>Location </th>");
                    sw.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>SMC Meeting Targets</th>");
                    sw.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>SIP and SAC Update Targets</th>");
                    sw.Write("<th class='header' colspan='6' style='" + HeaderStyle + "  width:2%;'>Learning Targets </th>");
                    sw.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'>BalSabha and LSG Targets </th>");
                    sw.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'>Other Activity Targets</th>");


                    sw.Write("</tr>");
                    int columnscount = dt.Columns.Count;


                    for (int j = 0; j < columnscount; j++)
                    {      //write in new column

                        sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>" + dt.Columns[j].ColumnName + "</th>");
                    }

                    sw.Write("</tr>");
                }
                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";


                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    sw.Write("<tr>");
                    //HttpContext.Current.Response.Write("<td >Direct</td>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {

                        sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");

                    }
                }
                #region Row1



                #endregion

                sw.Write("</tr>");
                sw.Write("</table>");
                sw.Close();
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

    private void GenerateExcelNewFC2021(string FIleName)
    {
        try
        {



            DataTable dt = ViewState["Annual"] as DataTable;
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
                {
                    FIleName = "StaffTrainingTarget";
                }
                if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
                {
                    FIleName = "FCVillageLevelPlan";
                }
                if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
                {
                    FIleName = "FCSchoolLevelPlan";
                }

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
                if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
                {
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='61'  style='text-align:Center;border:.2pt solid windowtext;'>Staff Training Target </td>");

                    HttpContext.Current.Response.Write("</tr>");

                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";


                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");

                    HttpContext.Current.Response.Write("<th class='header' colspan='2'  style='" + HeaderStyle + "  width:2%;'>Location </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>PMS Generated Targets</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='18' style='" + HeaderStyle + "  width:2%;'>User Entry- Online Trainings(#Participants)</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='18' style='" + HeaderStyle + "  width:2%;'>User Entry- Offline Trainings(#Participants) </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='18' style='" + HeaderStyle + "  width:2%;'>User Entry- Refresher Trainings(#Participants)</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'>Other Target Entry</th>");


                    HttpContext.Current.Response.Write("</tr>");

                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    int columnscount = dt.Columns.Count;


                    for (int j = 0; j < columnscount; j++)
                    {      //write in new column

                        HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>" + dt.Columns[j].ColumnName + "</th>");
                    }
                    HttpContext.Current.Response.Write("</tr>");
                    //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> District Code</th>");

                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on Enrolment and SMC	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on D2D	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on CMM	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on CV and SC	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on PMS	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on FC- PN(L0)	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on FC- L1	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on FC- L2	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on FC- L3	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on Learning Baseline	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on Learning Endline	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on Soft Skills	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Foundation Day Event for Staff	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on Bal Sabha and LSG	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on Phase III Activities	</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on SMC Meeting	</th>");



                }


                if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
                {

                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='38' ' style='text-align:Center;border:.2pt solid windowtext;'>Annual Plan - Village Level Planning</td>");

                    HttpContext.Current.Response.Write("</tr>");

                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='11'  style='" + HeaderStyle + "  width:2%;'>Location </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='10' style='" + HeaderStyle + "  width:2%;'>Enrolment Targets</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='18' style='" + HeaderStyle + "  width:2%;'>Village Level Targets</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='10' style='" + HeaderStyle + "  width:2%;'>Online TB Training Targets</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='10' style='" + HeaderStyle + "  width:2%;'>Offline TB Training Taregts</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='10' style='" + HeaderStyle + "  width:2%;'>Refresher TB Training  Targets</th>");


                    HttpContext.Current.Response.Write("</tr>");
                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    int columnscount = dt.Columns.Count;

                    for (int j = 0; j < columnscount; j++)
                    {      //write in new column

                        HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>" + dt.Columns[j].ColumnName + "</th>");
                    }



                    HttpContext.Current.Response.Write("</tr>");
                }


                if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
                {

                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='29' ' style='text-align:Center;border:.2pt solid windowtext;'>School Level Annual Plan Target Sheet</td>");


                    HttpContext.Current.Response.Write("</tr>");
                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='12'  style='" + HeaderStyle + "  width:2%;'>Location </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>SMC Meeting Targets</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>SIP and SAC Update Targets</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='6' style='" + HeaderStyle + "  width:2%;'>Learning Targets </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'>BalSabha and LSG Targets </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'>Other Activity Targets</th>");


                    HttpContext.Current.Response.Write("</tr>");
                    int columnscount = dt.Columns.Count;


                    for (int j = 0; j < columnscount; j++)
                    {      //write in new column

                        HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>" + dt.Columns[j].ColumnName + "</th>");
                    }

                    HttpContext.Current.Response.Write("</tr>");
                }
                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";


                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    HttpContext.Current.Response.Write("<tr>");
                    //HttpContext.Current.Response.Write("<td >Direct</td>");
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


    private void GenerateExcelNewFCString(string FIleName)
    {
        try
        {



            DataTable dt = ViewState["Annual"] as DataTable;
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
                {
                    FIleName = "StaffTrainingTarget";
                }
                if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
                {
                    FIleName = "FCVillageLevelPlan";
                }
                if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
                {
                    FIleName = "FCSchoolLevelPlan";
                }

                string Fullfilename1 = "" + FIleName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";
                string fileName = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + Fullfilename1 + "");
                StreamWriter sw = new StreamWriter(fileName, false);
                sw.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");

                //HttpContext.Current.Response.Clear();
                //HttpContext.Current.Response.ClearContent();
                //HttpContext.Current.Response.ClearHeaders();
                //HttpContext.Current.Response.Buffer = true;
                //HttpContext.Current.Response.ContentType = "application/ms-excel";
                //HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                //string Fullfilename = "" + FIleName + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


                //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");

                //HttpContext.Current.Response.Charset = "utf-8";
                //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                sw.Write("<table  >");
                if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
                {
                    sw.Write("<tr>");
                    sw.Write("<td colspan='17' ' style='text-align:Center;border:.2pt solid windowtext;'>Staff Training Target </td>");

                    sw.Write("</tr>");

                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";



                    sw.Write("<tr style='font-width:bold;'>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> District Code</th>");

                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on Enrolment and SMC	</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on D2D	</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on CMM	</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on CV and SC	</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on PMS	</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on FC- PN(L0)	</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on FC- L1	</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on FC- L2	</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on FC- L3	</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on Learning Baseline	</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on Learning Endline	</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on Soft Skills	</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Foundation Day Event for Staff	</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on Bal Sabha and LSG	</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Staff Training on Phase III Activities	</th>");


                    sw.Write("</tr>");
                }


                if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
                {

                    sw.Write("<tr>");
                    sw.Write("<td colspan='36' ' style='text-align:Center;border:.2pt solid windowtext;'>Annual Plan - Village Level Planning</td>");

                    sw.Write("</tr>");

                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
                    sw.Write("<tr style='font-width:bold;'>");
                    sw.Write("<th class='header' colspan='10'  style='" + HeaderStyle + "  width:2%;'>Location </th>");
                    sw.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>Target - OOSG</th>");
                    sw.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>Target - OOSB</th>");
                    sw.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'>Target - GSS </th>");
                    sw.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'>Target - MM </th>");
                    sw.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'>Target - Team Balika</th>");
                    sw.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>Target - Team Balika Training</th>");

                    sw.Write("</tr>");
                    sw.Write("<tr style='font-width:bold;'>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "   mso-rotate: 90;  width:2%;'>District Name</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "   mso-rotate: 90;  width:2%;'> District Code</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "   mso-rotate: 90;  width:2%;'> EG Block Name</th>");
                    sw.Write("<th class='header'   style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'>  EG Block Code</th>");
                    sw.Write("<th class='header'   style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> Admin Block Name</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "   mso-rotate: 90;  width:2%;'>  Admin Block Code</th>");
                    sw.Write("<th class='header'   style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> Cluster Name</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "   mso-rotate: 90;  width:2%;'>  Cluster  Code</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> Village Name</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'>  Village  Code</th>");


                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[5 Years]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[6 Years]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[7 - 14 Years]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Total OOSG]	</th>");

                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[5 Years]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[6 Years]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[7 - 14 Years]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Total OOSB]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Enrolment]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Retention]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Enrolment]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Retention]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[#TBNeed]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[#TB Avail.]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[TB Training (Enr+SMC)]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[TB Training Bal Sabha and LSG]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[TB Training GKP L0]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[TB Training GKP L1]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[TB Training GKP L2]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[TB Training GKP L3]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[TB VE Training]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Foundation Day Event for TB]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[TB VE Orientation]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[TB PRI Training]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[TB One Day Orientation]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[TB Training on Soft Skills and EDP]	</th>");


                    sw.Write("</tr>");
                }


                if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
                {

                    sw.Write("<tr>");
                    sw.Write("<td colspan='29' ' style='text-align:Center;border:.2pt solid windowtext;'>School Level Annual Plan Target Sheet</td>");


                    sw.Write("</tr>");
                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

                    sw.Write("<tr style='font-width:bold;'>");
                    sw.Write("<th class='header' colspan='12'  style='" + HeaderStyle + "  width:2%;'>Location </th>");
                    sw.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Target - SMC</th>");
                    sw.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'>Target - Retention Data Collection</th>");
                    sw.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Target - SIP </th>");
                    sw.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'>Target - Learning Baseline </th>");
                    sw.Write("<th class='header' colspan='6' style='" + HeaderStyle + "  width:2%;'>Target - GKP</th>");
                    sw.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'>Target - Balsabha</th>");
                    sw.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'>Target - LSG</th>");

                    sw.Write("</tr>");
                    sw.Write("<tr style='font-width:bold;'>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "   mso-rotate: 90;  width:2%;'>District Name</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "   mso-rotate: 90;  width:2%;'> District Code</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "   mso-rotate: 90;  width:2%;'> EG Block Name</th>");
                    sw.Write("<th class='header'   style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'>  EG Block Code</th>");
                    sw.Write("<th class='header'   style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> Admin Block Name</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "   mso-rotate: 90;  width:2%;'>  Admin Block Code</th>");
                    sw.Write("<th class='header'   style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> Cluster Name</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "   mso-rotate: 90;  width:2%;'>  Cluster  Code</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> Village Name</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'>  Village  Code</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> School Name</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'>  School  Code</th>");


                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Is SMC Active]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[#SMC Meeting]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[SMC Meet + ORENT]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Aggregate Retention ]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Retention Data Collection]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Critical Infra Target]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Other Critical Infra Target]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Total SIP Target]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Learning Baseline]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[GKP Need(Yes/No)]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[TB GKP CAPABLE (YES/NO)]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[GKP L0/L1]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[GKP L1/L2]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[GKP L2/L3]	</th>");

                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[Bal Sabha]	</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	[LSG]	</th>");


                    sw.Write("</tr>");
                }
                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";






                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    sw.Write("<tr>");
                    //sw.Write("<td >Direct</td>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {


                        sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");


                    }
                }
                #region Row1

                #endregion

                sw.Write("</tr>");
                sw.Write("</table>");
                sw.Close();
                //HttpContext.Current.Response.Flush();
                //HttpContext.Current.Response.End();
                FileStream fs = null;//, fs2=null;
                try
                {
                    string path1 = Fullfilename1;
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

    private void GenerateExcelNewString(string FIleName)
    {
        try
        {


            if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
            {
                FIleName = "StaffTrainingTraget";
            }
            if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
            {
                FIleName = "VillageLevelPlan";
            }
            if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
            {
                FIleName = "SchoolLevelPlan";
            }
            DataTable dt = ViewState["Annual"] as DataTable;
            if (dt.Rows.Count > 0)
            {

                string Fullfilename1 = "" + FIleName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";
                string fileName = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + Fullfilename1 + "");
                StreamWriter sw = new StreamWriter(fileName, false);
                sw.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");

                if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
                {
                    sw.Write("<tr>");
                    sw.Write("<td colspan='15' ' style='text-align:Center;border:.2pt solid windowtext;'>Annual Plan- Staff Training Planning </td>");

                    sw.Write("</tr>");
                    sw.Write("<td colspan='4'  style='text-align:Left;border:.2pt solid windowtext;'>Entry Date: …..............</td>");
                    sw.Write("<td colspan='11' style='text-align:Left;border:.2pt solid windowtext;'>Entry Done by:…............</td>");
                    sw.Write("</tr>");
                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

                    sw.Write("<tr style='font-width:bold;'>");
                    sw.Write("<th class='header' rowspan='2'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Activity</th>");
                    sw.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'> Month</th>");
                    sw.Write("</tr>");

                    sw.Write("<tr style='font-width:bold;'>");
                    //sw.Write("<th class='header' rowspan='2'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    //sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    //sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Activity</th>");

                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Apr</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>May</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jun</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jul</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Aug</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Sep</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Oct</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Nov</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Dec</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jan</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Feb</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Mar</th>");

                    sw.Write("</tr>");
                }


                if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
                {

                    sw.Write("<tr>");
                    sw.Write("<td colspan='23' ' style='text-align:Center;border:.2pt solid windowtext;'>Annual Plan - Village Level Planning</td>");

                    sw.Write("</tr>");
                    sw.Write("<td colspan='7'  style='text-align:Left;border:.2pt solid windowtext;'>Entry Date: …..............</td>");
                    sw.Write("<td colspan='8' style='text-align:Left;border:.2pt solid windowtext;'>Planing Date:…............</td>");
                    sw.Write("<td colspan='8' style='text-align:Left;border:.2pt solid windowtext;'>Entry Done by:…............</td>");
                    sw.Write("</tr>");
                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

                    sw.Write("<tr style='font-width:bold;'>");
                    sw.Write("<th class='header' rowspan='2'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> EG Block Name</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>  EG Block Code</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Admin Block Name</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>  Admin Block Code</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Cluster Name</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>  Cluster  Code</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Village Name</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>  Village  Code</th>");

                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Activity</th>");
                    sw.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'> Month</th>");
                    sw.Write("</tr>");

                    sw.Write("<tr style='font-width:bold;'>");
                    //sw.Write("<th class='header' rowspan='2'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    //sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    //sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Activity</th>");

                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Apr</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>May</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jun</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jul</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Aug</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Sep</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Oct</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Nov</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Dec</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jan</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Feb</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Mar</th>");

                    sw.Write("</tr>");
                }


                if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
                {

                    sw.Write("<tr>");
                    sw.Write("<td colspan='25' ' style='text-align:Center;border:.2pt solid windowtext;'>Annual Plan -School Level Planning</td>");

                    sw.Write("</tr>");
                    sw.Write("<td colspan='7'  style='text-align:Left;border:.2pt solid windowtext;'>Entry Date: …..............</td>");
                    sw.Write("<td colspan='8' style='text-align:Left;border:.2pt solid windowtext;'>Planing Date:…............</td>");

                    sw.Write("<td colspan='10' style='text-align:Left;border:.2pt solid windowtext;'>Entry Done by:…............</td>");
                    sw.Write("</tr>");
                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

                    sw.Write("<tr style='font-width:bold;'>");
                    sw.Write("<th class='header' rowspan='2'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> EG Block Name</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>  EG Block Code</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Admin Block Name</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>  Admin Block Code</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Cluster Name</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>  Cluster  Code</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Village Name</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>  Village  Code</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> School Name</th>");
                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Dise Code</th>");

                    sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Activity</th>");
                    sw.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'> Month</th>");
                    sw.Write("</tr>");

                    sw.Write("<tr style='font-width:bold;'>");
                    //sw.Write("<th class='header' rowspan='2'  style='" + HeaderStyle + "  width:2%;'>District Name</th>");
                    //sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> District Code</th>");
                    //sw.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'> Activity</th>");

                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Apr</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>May</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jun</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jul</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Aug</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Sep</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Oct</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Nov</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Dec</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Jan</th>");
                    sw.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Feb</th>");
                    sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Mar</th>");

                    sw.Write("</tr>");
                }
                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";






                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    sw.Write("<tr>");
                    //sw.Write("<td >Direct</td>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {


                        sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");


                    }
                }
                #region Row1



                #endregion


                sw.Write("</tr>");




                sw.Write("</table>");

                sw.Close();
                //HttpContext.Current.Response.Flush();
                //HttpContext.Current.Response.End();
                FileStream fs = null;//, fs2=null;
                try
                {
                    string path1 = Fullfilename1;
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

    private static string getWorkbookTemplate()
    {
        var sb = new StringBuilder(818);
        sb.AppendFormat(@"<?xml version=""1.0""?>{0}", Environment.NewLine);
        sb.AppendFormat(@"<?mso-application progid=""Excel.Sheet""?>{0}", Environment.NewLine);
        sb.AppendFormat(@"<Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet""{0}", Environment.NewLine);
        sb.AppendFormat(@" xmlns:o=""urn:schemas-microsoft-com:office:office""{0}", Environment.NewLine);
        sb.AppendFormat(@" xmlns:x=""urn:schemas-microsoft-com:office:excel""{0}", Environment.NewLine);
        sb.AppendFormat(@" xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet""{0}", Environment.NewLine);
        sb.AppendFormat(@" xmlns:html=""http://www.w3.org/TR/REC-html40"">{0}", Environment.NewLine);
        sb.AppendFormat(@" <Styles>{0}", Environment.NewLine);
        sb.AppendFormat(@"  <Style ss:ID=""Default"" ss:Name=""Normal"">{0}", Environment.NewLine);
        sb.AppendFormat(@"   <Alignment ss:Vertical=""Bottom""/>{0}", Environment.NewLine);
        sb.AppendFormat(@"   <Borders/>{0}", Environment.NewLine);
        sb.AppendFormat(@"   <Font ss:FontName=""Calibri"" x:Family=""Swiss"" ss:Size=""11"" ss:Color=""#000000""/>{0}", Environment.NewLine);
        sb.AppendFormat(@"   <Interior/>{0}", Environment.NewLine);
        sb.AppendFormat(@"   <NumberFormat/>{0}", Environment.NewLine);
        sb.AppendFormat(@"   <Protection/>{0}", Environment.NewLine);
        sb.AppendFormat(@"  </Style>{0}", Environment.NewLine);
        sb.AppendFormat(@"  <Style ss:ID=""s62"">{0}", Environment.NewLine);
        sb.AppendFormat(@"   <Font ss:FontName=""Calibri"" x:Family=""Swiss"" ss:Size=""11"" ss:Color=""#000000""{0}", Environment.NewLine);
        sb.AppendFormat(@"    ss:Bold=""1""/>{0}", Environment.NewLine);
        sb.AppendFormat(@"  </Style>{0}", Environment.NewLine);
        sb.AppendFormat(@"  <Style ss:ID=""s63"">{0}", Environment.NewLine);
        sb.AppendFormat(@"   <NumberFormat ss:Format=""Short Date""/>{0}", Environment.NewLine);
        sb.AppendFormat(@"  </Style>{0}", Environment.NewLine);
        sb.AppendFormat(@" </Styles>{0}", Environment.NewLine);
        sb.Append(@"{0}\r\n</Workbook>");
        return sb.ToString();
    }
    protected void LnkAnnualPlanDe_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 10;
        if (ddlTpye.SelectedIndex > 0)
        {
            string Year = ddlYear.SelectedItem.Text;
            string[] Year1 = Year.Split('-');
            if (Convert.ToInt32(Year1[0]) >= 2021)
            {
                LoadAnnualDataDeatils2021(1);
            }
            else if (Convert.ToInt32(Year1[0]) == 2020)
            {
                LoadAnnualDataDeatils(1);
            }
            else
            {
                LoadAnnualDataDetailOD(1);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Plan Type ')</script>", false);
        }


    }

    protected void LnkEnrolment_TbNeed(object sender, EventArgs e)
    {

        LoadAnnualDataDeatilsTbNeed(1);



    }
    protected void LnkEnrolment_Hotspot(object sender, EventArgs e)
    {
        ViewState["1"] = 710;
        LoadAnnualDataDeatilsHotSpot(1);



    }

    protected void AnnalPlanExcel_Hotspottest(object sender, EventArgs e)
    {
        ViewState["1"] = 710;
        LoadAnnualDataDeatilsHotSpot55(1);



    }
    protected void AnnalPlanExcel_Hotspot(object sender, EventArgs e)
    {
        ViewState["1"] = 710;
        LoadAnnualDataDeatilsHotSpot(1);



    }
    public void LoadAnnualDataDeatilsHotSpot55(int Flag)
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



        string condition = string.Empty;

        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "    where mst2District.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and mst2District.StateCode in(" + ddlStatecode + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst2District.DistrictCode in(" + ddlDistrict + ") ";

        }




        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con", conditions),


        };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptApproveStausMasterPlan]", cmdParameters);
        // DataTable dt = objMain.LoadAnnaulPlanRowData(conditions, Flag);


        ViewState["SAC"] = dt;
        if (dt.Rows.Count > 0)
        {
            //  objMain.ReportDownload("Approval Process Report", "Annual Plan", Convert.ToString(Session["username"]));
            ExportToCSVFile(dt, "Master Approval Process Report");

        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }




    }
    public void LoadAnnualDataDetailOD(int Flag)
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



        string condition = string.Empty;
        if (Flag == 2)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "    where mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (ddlStatecode.Length > 0)
            {
                conditions += " and mst5Village.StateCode in(" + ddlStatecode + ") ";

            }
        }
        else
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "    where mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (ddlStatecode.Length > 0)
            {
                conditions += " and mst5Village.StateCode in(" + ddlStatecode + ") ";

            }
        }

        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";

        }

        if (ddlBlock.Length > 0)
        {

            conditions += " and mst5Village.BlockCode in(" + ddlBlock + ") ";


        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }

        DataTable dt = objMain.LoadAnnaulPlanRowData(conditions, Flag);
        ViewState["D2dUser"] = dt;

        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();


        if (Flag == 1)
        {

            if (dt.Rows.Count > 0)
            {
                ExportToCSVFile(dt, "AnnualPlanDetail");
            }
            else
            {
                GV_DynamicGrid.DataSource = null;
                GV_DynamicGrid.DataBind();
            }


        }

        if (Flag == 2)
        {

            if (dt.Rows.Count > 0)
            {
                ExportToCSVFile(dt, "AnnualPlanDetail");
            }
            else
            {
                GV_DynamicGrid.DataSource = null;
                GV_DynamicGrid.DataBind();
            }


        }


    }
    public void LoadAnnualDataDeatils2021(int Flag)
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



        string condition = string.Empty;
        if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "    where mst2District.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (ddlStatecode.Length > 0)
            {
                conditions += " and mst2District.StateCode in(" + ddlStatecode + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and mst2District.DistrictCode in(" + ddlDistrict + ") ";

            }


        }


        if (Convert.ToInt32(ddlTpye.SelectedValue) == 2 || Convert.ToInt32(ddlTpye.SelectedValue) == 3)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "    where mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (ddlStatecode.Length > 0)
            {
                conditions += " and mst5Village.StateCode in(" + ddlStatecode + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";

            }

            if (ddlBlock.Length > 0)
            {

                conditions += " and mst5Village.BlockCode in(" + ddlBlock + ") ";


            }
            if (ddlPhan.Length > 0)
            {
                conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
            }
            if (ddlVillage.Length > 0)
            {
                conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
            }
        }

        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con", conditions),
            new SqlParameter("@Flag", Flag),
            new SqlParameter("@Tpye", ddlTpye.SelectedValue)
        };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadAnnaulPlanRowDataNew2022]", cmdParameters);
        // DataTable dt = objMain.LoadAnnaulPlanRowData(conditions, Flag);
        dt.Columns.Remove("Rowno");
        ViewState["D2dUser"] = dt;

        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();


        if (Flag == 1)
        {

            if (dt.Rows.Count > 0)
            {
                GenerateExcelNewAnnaulDetails("");
                //if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
                //{
                //    ExportToCSVFile(dt, "AnnualPlanDetailDistrictLevel");
                //}
                //if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
                //{
                //    ExportToCSVFile(dt, "AnnualPlanDetailVillageLevel");
                //}
                //if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
                //{
                //    ExportToCSVFile(dt, "AnnualPlanDetailSchoolLevel");
                //}
            }
            else
            {
                GV_DynamicGrid.DataSource = null;
                GV_DynamicGrid.DataBind();
            }

        }

        if (Flag == 2)
        {

            if (dt.Rows.Count > 1500)
            {
                ExportToCSVFile(dt, "AnnualPlanDetail");
            }
            else
            {
                GV_DynamicGrid.DataSource = null;
                GV_DynamicGrid.DataBind();
            }

        }


    }


    public void LoadAnnualDataDeatilsTbNeed(int Flag)
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



        string condition = string.Empty;

        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "    where mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and mst5Village.StateCode in(" + ddlStatecode + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";

        }

        if (ddlBlock.Length > 0)
        {

            conditions += " and mst5Village.BlockCode in(" + ddlBlock + ") ";


        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con", conditions),

        };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTbTargetAndAch]", cmdParameters);
        // DataTable dt = objMain.LoadAnnaulPlanRowData(conditions, Flag);

        ViewState["D2dUser"] = dt;

        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();





        if (dt.Rows.Count > 0)
        {
            ExportToCSVFile(dt, "TeamBalikaTargetAcheivement");
        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }




    }



    public void LoadAnnualDataDeatilsHotSpot(int Flag)
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



        string condition = string.Empty;

        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "    where mst2District.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and mst2District.StateCode in(" + ddlStatecode + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst2District.DistrictCode in(" + ddlDistrict + ") ";

        }




        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con", conditions),


        };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptApproveStausPlan]", cmdParameters);
        // DataTable dt = objMain.LoadAnnaulPlanRowData(conditions, Flag);


        ViewState["SAC"] = dt;
        if (dt.Rows.Count > 0)
        {
            objMain.ReportDownload("Approval Process Report", "Annual Plan", Convert.ToString(Session["username"]));
            ExportToCSVFile(dt, "Approval Process Report");

        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }




    }
    public void LoadAnnualSummary2024(int Flag)
    {

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



        string conditions = string.Empty;
        string conditions1 = string.Empty;

        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "    where mst2District.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
            conditions1 += "    where mstCluster.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and mst2District.StateCode in(" + ddlStatecode + ") ";
            conditions1 += " and mstCluster.StateCode in(" + ddlStatecode + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst2District.DistrictCode in(" + ddlDistrict + ") ";
            conditions1 += " and mstCluster.StateCode in(" + ddlStatecode + ") ";

        }
        if (ddlBlock.Length > 0)
        {
            conditions1 += " and mstCluster.Blockcode in(" + ddlBlock + ") ";
        }




        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con", conditions1),
               new SqlParameter("@Con1", conditions),

        };
        DataSet dt = null;

        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
        {
            dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnnalualPlan20252026Summary]", cmdParameters);
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2025)
        {
            dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnnalualPlan2025Summary]", cmdParameters);
        }
        else

        {
            dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnnalualPlan2024Summary]", cmdParameters);
        }







        ViewState["SAC"] = dt;

        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2025)
        {
            if (dt.Tables[0].Rows.Count > 0 || dt.Tables[1].Rows.Count > 0)
            {
                objMain.ReportDownload("Annual Plan Summary", "Annual Plan", Convert.ToString(Session["username"]));
                MultipuExeclTrack20252026();
            }
            else
            {
                GV_DynamicGrid.DataSource = null;
                GV_DynamicGrid.DataBind();
            }
        }
        else

        {
            if (dt.Tables[0].Rows.Count > 0 || dt.Tables[1].Rows.Count > 0)
            {
                objMain.ReportDownload("Annual Plan Summary", "Annual Plan", Convert.ToString(Session["username"]));
                MultipuExeclTrack2024205();
            }
            else
            {
                GV_DynamicGrid.DataSource = null;
                GV_DynamicGrid.DataBind();
            }
        }





    }

    public void LoadAnnualSummary2023(int Flag)
    {

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



        string conditions = string.Empty;
        string conditions1 = string.Empty;

        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "    where mst2District.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
            conditions1 += "    where mstCluster.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and mst2District.StateCode in(" + ddlStatecode + ") ";
            conditions1 += " and mstCluster.StateCode in(" + ddlStatecode + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst2District.DistrictCode in(" + ddlDistrict + ") ";
            conditions1 += " and mstCluster.StateCode in(" + ddlStatecode + ") ";

        }
        if (ddlBlock.Length > 0)
        {
            conditions1 += " and mstCluster.Blockcode in(" + ddlBlock + ") ";
        }




        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con", conditions1),
               new SqlParameter("@Con1", conditions),

        };
        DataSet dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnnalualPlan2023Summary]", cmdParameters);
        // DataTable dt = objMain.LoadAnnaulPlanRowData(conditions, Flag);







        ViewState["SAC"] = dt;
        if (dt.Tables[0].Rows.Count > 0 || dt.Tables[1].Rows.Count > 0)
        {
            objMain.ReportDownload("Annual Plan Summary", "Annual Plan", Convert.ToString(Session["username"]));
            MultipuExeclTrack();
        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }




    }

    public void MultipuExeclTrack20252026()
    {
        DataSet dt = ViewState["SAC"] as DataSet;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\AnualPlanSummary2025.xlsx");
        var ws = wb.Worksheet(1);
        var ws2 = wb.Worksheet(2);
        var ws3 = wb.Worksheet(4);
        var ws4 = wb.Worksheet(5);
        DataTable dt4 = dt.Tables[3];
        dt4.Columns.Remove("EGDistrictCode1");
        dt4.Columns.Remove("T");
        if (dt.Tables[0].Rows.Count > 0)
        {
            ws.Cell(3, 1).InsertData(dt.Tables[0].Rows);
            Int32 ii = Convert.ToInt32(dt.Tables[0].Rows.Count) + 2;
            string str = "A3:I" + ii;
            ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        }
        if (dt.Tables[0].Rows.Count > 0)
        {
            ws2.Cell(2, 1).InsertData(dt.Tables[1].Rows);
            Int32 ii2 = Convert.ToInt32(dt.Tables[1].Rows.Count) + 1;
            string str2 = "A2:AS" + ii2;
            ws2.Range(str2).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws2.Range(str2).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws2.Range(str2).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws2.Range(str2).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

            ws3.Cell(3, 1).InsertData(dt.Tables[2].Rows);
            Int32 ii3 = Convert.ToInt32(dt.Tables[2].Rows.Count) + 2;
            string str3 = "A2:AR" + ii3;
            ws3.Range(str3).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws3.Range(str3).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws3.Range(str3).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws3.Range(str3).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

            ws4.Cell(2, 1).InsertData(dt4.Rows);
            Int32 ii4 = Convert.ToInt32(dt4.Rows.Count) + 1;
            string str4 = "A2:BN" + ii4;
            ws4.Range(str4).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws4.Range(str4).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws4.Range(str4).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws4.Range(str4).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
        }




        filepath = StartupPath + "\\AnualPlanSummary " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
    public void MultipuExeclTrack2024205()
    {
        DataSet dt = ViewState["SAC"] as DataSet;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\AnualPlanSummary2024.xlsx");
        var ws = wb.Worksheet(1);
        var ws2 = wb.Worksheet(2);
        var ws3 = wb.Worksheet(4);
        var ws4 = wb.Worksheet(5);
        DataTable dt4 = dt.Tables[3];
        dt4.Columns.Remove("EGDistrictCode1");
        dt4.Columns.Remove("T");
        if (dt.Tables[0].Rows.Count > 0)
        {
            ws.Cell(3, 1).InsertData(dt.Tables[0].Rows);
            Int32 ii = Convert.ToInt32(dt.Tables[0].Rows.Count) + 2;
            string str = "A3:I" + ii;
            ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        }
        if (dt.Tables[0].Rows.Count > 0)
        {
            ws2.Cell(2, 1).InsertData(dt.Tables[1].Rows);
            Int32 ii2 = Convert.ToInt32(dt.Tables[1].Rows.Count) + 1;
            string str2 = "A2:AY" + ii2;
            ws2.Range(str2).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws2.Range(str2).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws2.Range(str2).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws2.Range(str2).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

            ws3.Cell(3, 1).InsertData(dt.Tables[2].Rows);
            Int32 ii3 = Convert.ToInt32(dt.Tables[2].Rows.Count) + 2;
            string str3 = "A2:AX" + ii3;
            ws3.Range(str3).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws3.Range(str3).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws3.Range(str3).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws3.Range(str3).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

            ws4.Cell(2, 1).InsertData(dt4.Rows);
            Int32 ii4 = Convert.ToInt32(dt4.Rows.Count) + 1;
            string str4 = "A2:BX" + ii4;
            ws4.Range(str4).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws4.Range(str4).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws4.Range(str4).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws4.Range(str4).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
        }




        filepath = StartupPath + "\\AnualPlanSummary " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
    public void MultipuExeclTrack()
    {
        DataSet dt = ViewState["SAC"] as DataSet;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\AnualPlanSummary.xlsx");
        var ws = wb.Worksheet(1);
        var ws2 = wb.Worksheet(2);
        var ws3 = wb.Worksheet(4);
        var ws4 = wb.Worksheet(5);
        DataTable dt4 = dt.Tables[3];
        dt4.Columns.Remove("EGDistrictCode1");
        dt4.Columns.Remove("T");
        if (dt.Tables[0].Rows.Count > 0)
        {
            ws.Cell(3, 1).InsertData(dt.Tables[0].Rows);
            Int32 ii = Convert.ToInt32(dt.Tables[0].Rows.Count) + 2;
            string str = "A3:I" + ii;
            ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        }
        if (dt.Tables[0].Rows.Count > 0)
        {
            ws2.Cell(2, 1).InsertData(dt.Tables[1].Rows);
            Int32 ii2 = Convert.ToInt32(dt.Tables[1].Rows.Count) + 1;
            string str2 = "A2:BE" + ii2;
            ws2.Range(str2).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws2.Range(str2).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws2.Range(str2).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws2.Range(str2).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

            ws3.Cell(3, 1).InsertData(dt.Tables[2].Rows);
            Int32 ii3 = Convert.ToInt32(dt.Tables[2].Rows.Count) + 2;
            string str3 = "A2:BD" + ii3;
            ws3.Range(str3).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws3.Range(str3).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws3.Range(str3).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws3.Range(str3).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

            ws4.Cell(2, 1).InsertData(dt4.Rows);
            Int32 ii4 = Convert.ToInt32(dt4.Rows.Count) + 1;
            string str4 = "A2:CD" + ii4;
            ws4.Range(str4).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws4.Range(str4).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws4.Range(str4).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws4.Range(str4).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
        }




        filepath = StartupPath + "\\AnualPlanSummary " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
    public void LoadAnnualDataDeatils(int Flag)
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



        string condition = string.Empty;
        if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "    where mst2District.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (ddlStatecode.Length > 0)
            {
                conditions += " and mst2District.StateCode in(" + ddlStatecode + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and mst2District.DistrictCode in(" + ddlDistrict + ") ";

            }


        }


        if (Convert.ToInt32(ddlTpye.SelectedValue) == 2 || Convert.ToInt32(ddlTpye.SelectedValue) == 3)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "    where mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (ddlStatecode.Length > 0)
            {
                conditions += " and mst5Village.StateCode in(" + ddlStatecode + ") ";

            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";

            }

            if (ddlBlock.Length > 0)
            {

                conditions += " and mst5Village.BlockCode in(" + ddlBlock + ") ";


            }
            if (ddlPhan.Length > 0)
            {
                conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
            }
            if (ddlVillage.Length > 0)
            {
                conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
            }
        }

        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con", conditions),
            new SqlParameter("@Flag", Flag),
            new SqlParameter("@Tpye", ddlTpye.SelectedValue)
        };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadAnnaulPlanRowDataNew]", cmdParameters);
        // DataTable dt = objMain.LoadAnnaulPlanRowData(conditions, Flag);
        dt.Columns.Remove("Rowno");
        ViewState["D2dUser"] = dt;

        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();


        if (Flag == 1)
        {

            if (dt.Rows.Count > 0)
            {
                GenerateExcelNewAnnaulDetails("");
                //if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
                //{
                //    ExportToCSVFile(dt, "AnnualPlanDetailDistrictLevel");
                //}
                //if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
                //{
                //    ExportToCSVFile(dt, "AnnualPlanDetailVillageLevel");
                //}
                //if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
                //{
                //    ExportToCSVFile(dt, "AnnualPlanDetailSchoolLevel");
                //}
            }
            else
            {
                GV_DynamicGrid.DataSource = null;
                GV_DynamicGrid.DataBind();
            }

        }

        if (Flag == 2)
        {

            if (dt.Rows.Count > 1500)
            {
                ExportToCSVFile(dt, "AnnualPlanDetail");
            }
            else
            {
                GV_DynamicGrid.DataSource = null;
                GV_DynamicGrid.DataBind();
            }

        }


    }
    private void ExportToCSVFileNew(DataTable dtTable, string filePath)
    {
        if (dtTable != null)
        {
            StringBuilder sbldr = new StringBuilder();
            if (dtTable.Columns.Count != 0)
            {
                int columnscount = GV_DynamicGrid.HeaderRow.Cells.Count;

                for (int j = 0; j < columnscount; j++)
                {
                    sbldr.Append(Convert.ToString(GV_DynamicGrid.HeaderRow.Cells[j].Text) + ',');
                }


                //foreach (DataColumn col in dtTable.Columns)
                //{
                //    sbldr.Append(col.ColumnName + ',');
                //}
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
    protected void LnkAnSummary_OnClick(object sender, EventArgs e)
    {
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');
        if (Convert.ToInt32(Year1[0]) >= 2024)
        {
            LoadAnnualSummary2024(1);
        }
        else if (Convert.ToInt32(Year1[0]) == 2023)
        {
            LoadAnnualSummary2023(1);
        }
        else if (ddlGroup.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Group Type')</script>", false);
        }
        else
        {

            if (Convert.ToInt32(Year1[0]) >= 2022)
            {
                LoadAnnualDataSummaryNew2021(1);
            }
            else if (Convert.ToInt32(Year1[0]) == 2021)
            {
                LoadAnnualDataSummaryNew2021HHH(1);
            }
            else if (Convert.ToInt32(Year1[0]) == 2020)
            {
                LoadAnnualDataSummaryNew(1);
            }
            else
            {

                LoadAnnualDataSummary(1);
            }
        }


    }


    protected void LnkAnSummaryAlter_OnClick(object sender, EventArgs e)
    {
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');
        if (Convert.ToInt32(Year1[0]) >= 2024)
        {
            LoadAnnualDataSummaryAlter2024(1);
        }
        if (Convert.ToInt32(Year1[0]) == 2023)
        {
            LoadAnnualDataSummaryAlter(1);
        }


    }
    public void LoadAnnualDataSummaryAlter2024(int Flag)
    {

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



        string conditions = string.Empty;

        conditions += " and mstCluster.Fyear='" + ddlYear.SelectedItem.Text + "' ";
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mstCluster.DistrictCode in(" + ddlDistrict + ") ";

        }
        if (ddlBlock.Length > 0)
        {
            conditions += " and mstCluster.Blockcode in(" + ddlBlock + ") ";

        }


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@DistCode", conditions),

        };

        DataSet dt = null;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2025)
        {
            dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptClusterAnaulSummaryNewAlte2025]", cmdParameters);
            Session["SACAlter"] = dt;
            if (dt.Tables[0].Rows.Count > 0)
            {
                objMain.ReportDownload("Annual Plan Quality Alert", "Annual Plan", Convert.ToString(Session["username"]));
                MultipuExeclTrackAlterAlter();
            }
            GV_DynamicGrid.Visible = true;
            GV_DynamicGrid.DataSource = dt;
            GV_DynamicGrid.DataBind();
        }
        else

        {
            dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptClusterAnaulSummaryNewAlte2024]", cmdParameters);
            Session["SACAlter"] = dt;
            if (dt.Tables[0].Rows.Count > 0)
            {
                objMain.ReportDownload("Annual Plan Quality Alert", "Annual Plan", Convert.ToString(Session["username"]));
                MultipuExeclTrackAlterAlter();
            }
            GV_DynamicGrid.Visible = true;
            GV_DynamicGrid.DataSource = dt;
            GV_DynamicGrid.DataBind();
        }


        ViewState["1"] = 100;




    }

    public void LoadAnnualDataSummaryAlter(int Flag)
    {

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



        string conditions = string.Empty;

        conditions += " and mstCluster.Fyear='" + ddlYear.SelectedItem.Text + "' ";
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mstCluster.DistrictCode in(" + ddlDistrict + ") ";

        }
        if (ddlBlock.Length > 0)
        {
            conditions += " and mstCluster.Blockcode in(" + ddlBlock + ") ";

        }


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@DistCode", conditions),

        };

        DataSet dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptClusterAnaulSummaryNewAlte]", cmdParameters);

        Session["SACAlter"] = dt;
        if (dt.Tables[0].Rows.Count > 0)
        {
            objMain.ReportDownload("Annual Plan Quality Alert", "Annual Plan", Convert.ToString(Session["username"]));
            MultipuExeclTrackAlter();
        }
        GV_DynamicGrid.Visible = true;
        GV_DynamicGrid.DataSource = dt;
        GV_DynamicGrid.DataBind();

        ViewState["1"] = 100;




    }

    public void MultipuExeclTrackAlterAlter()
    {
        DataSet dt = Session["SACAlter"] as DataSet;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\AnnualPlanQualityAlert2024.xlsx");
        var ws = wb.Worksheet(1);
        var ws2 = wb.Worksheet(2);


        if (dt.Tables[0].Rows.Count > 0)
        {
            ws.Cell(3, 1).InsertData(dt.Tables[0].Rows);
            Int32 ii = Convert.ToInt32(dt.Tables[0].Rows.Count) + 2;
            string str = "A3:AF" + ii;

            ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

            //for (int x = 3; x < dt.Tables[0].Rows.Count + 3; x++)
            //{
            //    int[] arcols = { 19 };
            //    int[] arcols1 = { 18 };
            //    string dhhd = Convert.ToString(ws.Cell(x, arcols1[0]).Value);
            //    for (int y = 0; y < arcols.Length; y++)
            //    {
            //        if (dhhd == "")
            //        {

            //        }
            //        else
            //        {
            //            if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
            //            {

            //            }
            //            else if (Convert.ToInt32(dhhd) == 0 && Convert.ToInt32(ws.Cell(x, arcols[y]).Value) == 0)
            //            {

            //            }
            //            else if (Convert.ToInt32(dhhd) <= Convert.ToInt32(ws.Cell(x, arcols[y]).Value))
            //            {
            //                ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
            //            }

            //            else if (Convert.ToInt32(dhhd) >= Convert.ToInt32(ws.Cell(x, arcols[y]).Value))
            //            {
            //                ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
            //            }

            //        }

            //        //else if (Convert.ToDecimal(ws.Cell(x, arcols[y-1]).Value)  >=  Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) )
            //        //{
            //        //    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
            //        //}

            //    }
            //}

            for (int x = 3; x < dt.Tables[0].Rows.Count + 3; x++)
            {
                ///# Balsabha Schools in Master	# Balsabha School Planned

                int[] arcols = { 23 };
                int[] arcols1 = { 22 };
                string dhhd = Convert.ToString(ws.Cell(x, arcols1[0]).Value);
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (dhhd == "")
                    {

                    }
                    else
                    {
                        if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                        {

                        }
                        else if (Convert.ToInt32(dhhd) == 0 && Convert.ToInt32(ws.Cell(x, arcols[y]).Value) == 0)
                        {

                        }
                        else if (Convert.ToInt32(dhhd) <= Convert.ToInt32(ws.Cell(x, arcols[y]).Value))
                        {
                            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
                        }

                        else if (Convert.ToInt32(dhhd) >= Convert.ToInt32(ws.Cell(x, arcols[y]).Value))
                        {
                            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                        }

                    }

                    //else if (Convert.ToDecimal(ws.Cell(x, arcols[y-1]).Value)  >=  Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) )
                    //{
                    //    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    //}

                }
            }

            for (int x = 3; x < dt.Tables[0].Rows.Count + 3; x++)
            {
                int[] arcols = { 24 };

                // Low Benificiary Balsabha Schools
                for (int y = 0; y < arcols.Length; y++)
                {

                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {

                    }
                    else if (Convert.ToInt32(ws.Cell(x, arcols[y]).Value) > 0)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }


                }
            }


            for (int x = 3; x < dt.Tables[0].Rows.Count + 3; x++)
            {
                //# GKP Schools in Master	# GKP Schools Planned 

                int[] arcols = { 26 };
                int[] arcols1 = { 25 };
                string dhhd = Convert.ToString(ws.Cell(x, arcols1[0]).Value);
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (dhhd == "")
                    {

                    }
                    else
                    {
                        if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                        {

                        }
                        else if (Convert.ToInt32(dhhd) == 0 && Convert.ToInt32(ws.Cell(x, arcols[y]).Value) == 0)
                        {

                        }
                        else if (Convert.ToInt32(dhhd) <= Convert.ToInt32(ws.Cell(x, arcols[y]).Value))
                        {
                            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
                        }
                        else if (Convert.ToInt32(dhhd) >= Convert.ToInt32(ws.Cell(x, arcols[y]).Value))
                        {
                            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                        }
                    }

                    //else if (Convert.ToDecimal(ws.Cell(x, arcols[y-1]).Value)  >=  Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) )
                    //{
                    //    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    //}

                }
            }

            for (int x = 3; x < dt.Tables[0].Rows.Count + 3; x++)
            {
                int[] arcols = { 27 };

                //# Low Beneficiary GKP Schools Planned

                for (int y = 0; y < arcols.Length; y++)
                {

                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {

                    }
                    else if (Convert.ToInt32(ws.Cell(x, arcols[y]).Value) > 0)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }


                }
            }
        }
        if (dt.Tables[0].Rows.Count > 0)
        {
            ws2.Cell(3, 1).InsertData(dt.Tables[1].Rows);
            Int32 ii2 = Convert.ToInt32(dt.Tables[1].Rows.Count) + 2;
            string str2 = "A3:AB" + ii2;
            ws2.Range(str2).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws2.Range(str2).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws2.Range(str2).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws2.Range(str2).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
            //for (int x = 3; x < dt.Tables[1].Rows.Count + 3; x++)
            //{
            //    int[] arcols = { 14 };
            //    int[] arcols1 = { 13 };
            //    string dhhd = Convert.ToString(ws2.Cell(x, arcols1[0]).Value);
            //    for (int y = 0; y < arcols.Length; y++)
            //    {
            //        if (dhhd == "")
            //        {

            //        }
            //        else
            //        {
            //            if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
            //            {

            //            }
            //            else if (Convert.ToInt32(dhhd) == 0 && Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) == 0)
            //            {

            //            }
            //            else if (Convert.ToInt32(dhhd) <= Convert.ToInt32(ws2.Cell(x, arcols[y]).Value))
            //            {
            //                ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
            //            }
            //            else if (Convert.ToInt32(dhhd) >= Convert.ToInt32(ws2.Cell(x, arcols[y]).Value))
            //            {
            //                ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
            //            }
            //        }

            //        //else if (Convert.ToDecimal(ws.Cell(x, arcols[y-1]).Value)  >=  Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) )
            //        //{
            //        //    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
            //        //}

            //    }
            //}
            for (int x = 3; x < dt.Tables[1].Rows.Count + 3; x++)
            {
                int[] arcols = { 18 };
                int[] arcols1 = { 17 };
                string dhhd = Convert.ToString(ws2.Cell(x, arcols1[0]).Value);
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (dhhd == "")
                    {

                    }
                    else
                    {
                        if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                        {

                        }
                        else if (Convert.ToInt32(dhhd) == 0 && Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) == 0)
                        {

                        }
                        else if (Convert.ToInt32(dhhd) <= Convert.ToInt32(ws2.Cell(x, arcols[y]).Value))
                        {
                            ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
                        }
                        else if (Convert.ToInt32(dhhd) >= Convert.ToInt32(ws2.Cell(x, arcols[y]).Value))
                        {
                            ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                        }
                    }

                    //else if (Convert.ToDecimal(ws.Cell(x, arcols[y-1]).Value)  >=  Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) )
                    //{
                    //    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    //}

                }
            }

            for (int x = 3; x < dt.Tables[0].Rows.Count + 3; x++)
            {
                int[] arcols = { 19 };


                for (int y = 0; y < arcols.Length; y++)
                {

                    if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                    {

                    }
                    else if (Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) > 0)
                    {
                        ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }


                }
            }


            for (int x = 3; x < dt.Tables[1].Rows.Count + 3; x++)
            {
                int[] arcols = { 21 };
                int[] arcols1 = { 20 };
                string dhhd = Convert.ToString(ws2.Cell(x, arcols1[0]).Value);
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (dhhd == "")
                    {

                    }
                    else
                    {
                        if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                        {

                        }
                        else if (Convert.ToInt32(dhhd) == 0 && Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) == 0)
                        {

                        }
                        else if (Convert.ToInt32(dhhd) <= Convert.ToInt32(ws2.Cell(x, arcols[y]).Value))
                        {
                            ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
                        }
                        else if (Convert.ToInt32(dhhd) >= Convert.ToInt32(ws2.Cell(x, arcols[y]).Value))
                        {
                            ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                        }
                    }

                    //else if (Convert.ToDecimal(ws.Cell(x, arcols[y-1]).Value)  >=  Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) )
                    //{
                    //    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    //}

                }
            }

            for (int x = 3; x < dt.Tables[0].Rows.Count + 3; x++)
            {
                int[] arcols = { 22 };


                for (int y = 0; y < arcols.Length; y++)
                {

                    if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                    {

                    }
                    else if (Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) > 0)
                    {
                        ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }


                }
            }

        }




        filepath = StartupPath + "\\AnnualPlanQualityAlert " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
    public void MultipuExeclTrackAlter()
    {
        DataSet dt = Session["SACAlter"] as DataSet;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\AnnualPlanQualityAlert.xlsx");
        var ws = wb.Worksheet(1);
        var ws2 = wb.Worksheet(2);


        if (dt.Tables[0].Rows.Count > 0)
        {
            ws.Cell(3, 1).InsertData(dt.Tables[0].Rows);
            Int32 ii = Convert.ToInt32(dt.Tables[0].Rows.Count) + 2;
            string str = "A3:AH" + ii;

            ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

            for (int x = 3; x < dt.Tables[0].Rows.Count + 3; x++)
            {
                int[] arcols = { 19 };
                int[] arcols1 = { 18 };
                string dhhd = Convert.ToString(ws.Cell(x, arcols1[0]).Value);
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (dhhd == "")
                    {

                    }
                    else
                    {
                        if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                        {

                        }
                        else if (Convert.ToInt32(dhhd) == 0 && Convert.ToInt32(ws.Cell(x, arcols[y]).Value) == 0)
                        {

                        }
                        else if (Convert.ToInt32(dhhd) <= Convert.ToInt32(ws.Cell(x, arcols[y]).Value))
                        {
                            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
                        }

                        else if (Convert.ToInt32(dhhd) >= Convert.ToInt32(ws.Cell(x, arcols[y]).Value))
                        {
                            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                        }

                    }

                    //else if (Convert.ToDecimal(ws.Cell(x, arcols[y-1]).Value)  >=  Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) )
                    //{
                    //    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    //}

                }
            }

            for (int x = 3; x < dt.Tables[0].Rows.Count + 3; x++)
            {
                int[] arcols = { 25 };
                int[] arcols1 = { 24 };
                string dhhd = Convert.ToString(ws.Cell(x, arcols1[0]).Value);
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (dhhd == "")
                    {

                    }
                    else
                    {
                        if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                        {

                        }
                        else if (Convert.ToInt32(dhhd) == 0 && Convert.ToInt32(ws.Cell(x, arcols[y]).Value) == 0)
                        {

                        }
                        else if (Convert.ToInt32(dhhd) <= Convert.ToInt32(ws.Cell(x, arcols[y]).Value))
                        {
                            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
                        }

                        else if (Convert.ToInt32(dhhd) >= Convert.ToInt32(ws.Cell(x, arcols[y]).Value))
                        {
                            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                        }

                    }

                    //else if (Convert.ToDecimal(ws.Cell(x, arcols[y-1]).Value)  >=  Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) )
                    //{
                    //    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    //}

                }
            }

            for (int x = 3; x < dt.Tables[0].Rows.Count + 3; x++)
            {
                int[] arcols = { 26 };


                for (int y = 0; y < arcols.Length; y++)
                {

                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {

                    }
                    else if (Convert.ToInt32(ws.Cell(x, arcols[y]).Value) > 0)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }


                }
            }


            for (int x = 3; x < dt.Tables[0].Rows.Count + 3; x++)
            {
                int[] arcols = { 28 };
                int[] arcols1 = { 27 };
                string dhhd = Convert.ToString(ws.Cell(x, arcols1[0]).Value);
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (dhhd == "")
                    {

                    }
                    else
                    {
                        if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                        {

                        }
                        else if (Convert.ToInt32(dhhd) == 0 && Convert.ToInt32(ws.Cell(x, arcols[y]).Value) == 0)
                        {

                        }
                        else if (Convert.ToInt32(dhhd) <= Convert.ToInt32(ws.Cell(x, arcols[y]).Value))
                        {
                            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
                        }
                        else if (Convert.ToInt32(dhhd) >= Convert.ToInt32(ws.Cell(x, arcols[y]).Value))
                        {
                            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                        }
                    }

                    //else if (Convert.ToDecimal(ws.Cell(x, arcols[y-1]).Value)  >=  Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) )
                    //{
                    //    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    //}

                }
            }

            for (int x = 3; x < dt.Tables[0].Rows.Count + 3; x++)
            {
                int[] arcols = { 29 };


                for (int y = 0; y < arcols.Length; y++)
                {

                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {

                    }
                    else if (Convert.ToInt32(ws.Cell(x, arcols[y]).Value) > 0)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }


                }
            }
        }
        if (dt.Tables[0].Rows.Count > 0)
        {
            ws2.Cell(3, 1).InsertData(dt.Tables[1].Rows);
            Int32 ii2 = Convert.ToInt32(dt.Tables[1].Rows.Count) + 2;
            string str2 = "A3:AD" + ii2;
            ws2.Range(str2).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws2.Range(str2).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws2.Range(str2).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws2.Range(str2).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
            for (int x = 3; x < dt.Tables[1].Rows.Count + 3; x++)
            {
                int[] arcols = { 14 };
                int[] arcols1 = { 13 };
                string dhhd = Convert.ToString(ws2.Cell(x, arcols1[0]).Value);
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (dhhd == "")
                    {

                    }
                    else
                    {
                        if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                        {

                        }
                        else if (Convert.ToInt32(dhhd) == 0 && Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) == 0)
                        {

                        }
                        else if (Convert.ToInt32(dhhd) <= Convert.ToInt32(ws2.Cell(x, arcols[y]).Value))
                        {
                            ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
                        }
                        else if (Convert.ToInt32(dhhd) >= Convert.ToInt32(ws2.Cell(x, arcols[y]).Value))
                        {
                            ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                        }
                    }

                    //else if (Convert.ToDecimal(ws.Cell(x, arcols[y-1]).Value)  >=  Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) )
                    //{
                    //    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    //}

                }
            }
            for (int x = 3; x < dt.Tables[1].Rows.Count + 3; x++)
            {
                int[] arcols = { 20 };
                int[] arcols1 = { 19 };
                string dhhd = Convert.ToString(ws2.Cell(x, arcols1[0]).Value);
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (dhhd == "")
                    {

                    }
                    else
                    {
                        if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                        {

                        }
                        else if (Convert.ToInt32(dhhd) == 0 && Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) == 0)
                        {

                        }
                        else if (Convert.ToInt32(dhhd) <= Convert.ToInt32(ws2.Cell(x, arcols[y]).Value))
                        {
                            ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
                        }
                        else if (Convert.ToInt32(dhhd) >= Convert.ToInt32(ws2.Cell(x, arcols[y]).Value))
                        {
                            ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                        }
                    }

                    //else if (Convert.ToDecimal(ws.Cell(x, arcols[y-1]).Value)  >=  Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) )
                    //{
                    //    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    //}

                }
            }

            for (int x = 3; x < dt.Tables[0].Rows.Count + 3; x++)
            {
                int[] arcols = { 21 };


                for (int y = 0; y < arcols.Length; y++)
                {

                    if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                    {

                    }
                    else if (Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) > 0)
                    {
                        ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }


                }
            }


            for (int x = 3; x < dt.Tables[1].Rows.Count + 3; x++)
            {
                int[] arcols = { 23 };
                int[] arcols1 = { 22 };
                string dhhd = Convert.ToString(ws2.Cell(x, arcols1[0]).Value);
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (dhhd == "")
                    {

                    }
                    else
                    {
                        if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                        {

                        }
                        else if (Convert.ToInt32(dhhd) == 0 && Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) == 0)
                        {

                        }
                        else if (Convert.ToInt32(dhhd) <= Convert.ToInt32(ws2.Cell(x, arcols[y]).Value))
                        {
                            ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
                        }
                        else if (Convert.ToInt32(dhhd) >= Convert.ToInt32(ws2.Cell(x, arcols[y]).Value))
                        {
                            ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                        }
                    }

                    //else if (Convert.ToDecimal(ws.Cell(x, arcols[y-1]).Value)  >=  Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) )
                    //{
                    //    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    //}

                }
            }

            for (int x = 3; x < dt.Tables[0].Rows.Count + 3; x++)
            {
                int[] arcols = { 24 };


                for (int y = 0; y < arcols.Length; y++)
                {

                    if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                    {

                    }
                    else if (Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) > 0)
                    {
                        ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }


                }
            }

        }




        filepath = StartupPath + "\\AnnualPlanQualityAlert " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
    public void LoadAnnualDataSummary(int Flag)
    {

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



        string conditions = string.Empty;

        conditions += " and mst5Village.Fyear='" + ddlYear.SelectedItem.Text + "' ";
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";

        }
        if (ddlBlock.Length > 0)
        {
            conditions += " and mst5Village.Blockcode in(" + ddlBlock + ") ";

        }


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@DistCode", conditions),
                    new SqlParameter("@Flag", ddlGroup.SelectedValue)
        };

        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnnualSummaryCluserWise]", cmdParameters);



        ViewState["D2dUser"] = dt;

        GV_DynamicGrid.Visible = true;
        GV_DynamicGrid.DataSource = dt;
        GV_DynamicGrid.DataBind();

        ViewState["1"] = 100;




    }
    public void LoadAnnualDataSummaryNew2021HHH(int Flag)
    {

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



        string conditions = string.Empty;
        string cond = string.Empty;

        conditions += "  and mst5Village.Fyear='" + ddlYear.SelectedItem.Text + "' ";
        cond += "  mst2District.Fyear='" + ddlYear.SelectedItem.Text + "' ";
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
            cond += " and mst2District.DistrictCode in(" + ddlDistrict + ") ";

        }
        if (ddlBlock.Length > 0)
        {
            conditions += " and mst5Village.Blockcode in(" + ddlBlock + ") ";

        }


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con", conditions),
            new SqlParameter("@ConD", cond),
                    new SqlParameter("@Group", ddlGroup.SelectedValue)
        };

        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnnualSummaryReport2021]", cmdParameters);

        dt.Columns.Remove("mon");
        ViewState["D2dUser"] = dt;

        GV_DynamicGrid.Visible = true;

        ViewState["1"] = 100;
        if (dt.Rows.Count > 30)
        {
            DataTable dtn = dt.Clone();
            int i = 0;
            int count = 20;
            foreach (DataRow row in dt.Rows)
            {
                if (i < count)
                {
                    dtn.ImportRow(row);
                    i++;
                }
                if (i > count)
                    break;
            }
            GV_DynamicGrid.DataSource = dtn;
            GV_DynamicGrid.DataBind();
            //   btnImport_Click(LinkButton3, null);
        }
        else
        {
            GV_DynamicGrid.DataSource = dt;
            GV_DynamicGrid.DataBind();
        }



    }
    public void LoadAnnualDataSummaryNew2021(int Flag)
    {

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



        string conditions = string.Empty;
        string cond = string.Empty;

        conditions += "  and mst5Village.Fyear='" + ddlYear.SelectedItem.Text + "' ";
        cond += "  mst2District.Fyear='" + ddlYear.SelectedItem.Text + "' ";
        if (ddlStatecode.Length > 0)
        {
            conditions += " and mst5Village.Statecode in(" + ddlStatecode + ") ";
            cond += " and mst2District.Statecode in(" + ddlStatecode + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
            cond += " and mst2District.DistrictCode in(" + ddlDistrict + ") ";

        }
        if (ddlBlock.Length > 0)
        {
            conditions += " and mst5Village.Blockcode in(" + ddlBlock + ") ";

        }


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con", conditions),
            new SqlParameter("@ConD", cond),
                    new SqlParameter("@Group", ddlGroup.SelectedValue),
                        new SqlParameter("@Fyear", ddlYear.SelectedValue)
        };

        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnnualSummaryReport2023]", cmdParameters);

        dt.Columns.Remove("mon");
        Session["D2dUser"] = dt;

        GV_DynamicGrid.Visible = true;

        ViewState["1"] = 100;
        if (dt.Rows.Count > 30)
        {
            DataTable dtn = dt.Clone();
            int i = 0;
            int count = 20;
            foreach (DataRow row in dt.Rows)
            {
                if (i < count)
                {
                    dtn.ImportRow(row);
                    i++;
                }
                if (i > count)
                    break;
            }
            GV_DynamicGrid.DataSource = dtn;
            GV_DynamicGrid.DataBind();
            //   btnImport_Click(LinkButton3, null);
        }
        else
        {
            GV_DynamicGrid.DataSource = dt;
            GV_DynamicGrid.DataBind();
        }



    }
    public void LoadAnnualDataSummaryNew(int Flag)
    {

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



        string conditions = string.Empty;
        string cond = string.Empty;

        conditions += "  and mst5Village.Fyear='" + ddlYear.SelectedItem.Text + "' ";
        cond += "  mst2District.Fyear='" + ddlYear.SelectedItem.Text + "' ";
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
            cond += " and mst2District.DistrictCode in(" + ddlDistrict + ") ";

        }
        if (ddlBlock.Length > 0)
        {
            conditions += " and mst5Village.Blockcode in(" + ddlBlock + ") ";

        }


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con", conditions),
            new SqlParameter("@ConD", cond),
                    new SqlParameter("@Group", ddlGroup.SelectedValue)
        };

        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnnualSummaryReport]", cmdParameters);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Int32 GSSEnrolment = 0;
            Int32 GSSRetention = 0;
            Int32 MMRetention = 0;
            Int32 MMEnrolment = 0;
            if (Convert.ToString(dt.Rows[i]["GSS Enrolment"]) == "")
            {

            }
            else
            {
                GSSEnrolment = Convert.ToInt32(dt.Rows[i]["GSS Enrolment"]);
            }
            if (Convert.ToString(dt.Rows[i]["GSS Retention"]) == "")
            {

            }
            else
            {
                GSSRetention = Convert.ToInt32(dt.Rows[i]["GSS Retention"]);
            }
            if (Convert.ToString(dt.Rows[i]["MM Enrolment"]) == "")
            {

            }
            else
            {
                MMEnrolment = Convert.ToInt32(dt.Rows[i]["MM Enrolment"]);
            }
            if (Convert.ToString(dt.Rows[i]["MM Retention"]) == "")
            {

            }
            else
            {
                MMRetention = Convert.ToInt32(dt.Rows[i]["MM Retention"]);
            }

            dt.Rows[i]["Total GSS"] = GSSEnrolment + GSSRetention;
            dt.Rows[i]["Total MM"] = MMEnrolment + MMRetention;
        }
        dt.Columns.Remove("mon");
        ViewState["D2dUser"] = dt;

        GV_DynamicGrid.Visible = true;

        ViewState["1"] = 100;
        if (dt.Rows.Count > 30)
        {
            DataTable dtn = dt.Clone();
            int i = 0;
            int count = 20;
            foreach (DataRow row in dt.Rows)
            {
                if (i < count)
                {
                    dtn.ImportRow(row);
                    i++;
                }
                if (i > count)
                    break;
            }
            GV_DynamicGrid.DataSource = dtn;
            GV_DynamicGrid.DataBind();
            //   btnImport_Click(LinkButton3, null);
        }
        else
        {
            GV_DynamicGrid.DataSource = dt;
            GV_DynamicGrid.DataBind();
        }



    }

    public void AnnaualFCReport(Int32 Flag)
    {
        string conditions = "";


        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
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
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }

        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }




        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Villagecode",conditions),


        };
        DataTable dataTable = null;


        dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[GetAnualPlanFCWiseReportNew]", cmdParameters);

        ViewState["dt"] = dataTable;
        if (dataTable.Rows.Count > 0)
        {

            ExportToCSVFile(dataTable, "AnualPlanFC");

        }
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();


    }
    protected void LnkMasterData_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 117;

        LoadMasterData(0);

    }
    protected void LnkEnrolment_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 118;

        LoadEnrollData(0);

    }
    public void LoadMasterData(int Flag)
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
        DataTable dt = null;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
        {
            dt = objMain.LoadMasterDataNew2025(conditions, Convert.ToInt32(ddlYear.SelectedValue));
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2024 | Convert.ToInt32(ddlYear.SelectedValue) == 2025)
        {
            dt = objMain.LoadMasterDataNew(conditions, 12);
        }
        else
        {
            dt = objMain.LoadMasterDataNew(conditions, 6);
        }

        ViewState["D2dUser"] = dt;




        GV_DynamicGrid.Visible = true;
        if (dt.Rows.Count > 0)
        {
            ExportToCSVFile(dt, "MasterData");
        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }




    }

    public void LoadEnrollData(int Flag)
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



        string condition = string.Empty;
        if (Flag == 2)
        {
            if (ddlStatecode.Length > 0)
            {
                conditions += " and mst5Village.StateCode in(" + ddlStatecode + ") ";

            }
        }
        else
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "    and mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (ddlStatecode.Length > 0)
            {
                conditions += " and mst5Village.StateCode in(" + ddlStatecode + ") ";

            }
        }

        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";

        }

        if (ddlBlock.Length > 0)
        {

            conditions += " and mst5Village.BlockCode in(" + ddlBlock + ") ";

        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }




        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Condition",conditions),
         new SqlParameter("@Fyear",ddlYear.SelectedValue),

        };
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptEnrollTargetD2dDetials]", cmdParameters);
        ViewState["D2dUser"] = dt;

        GV_DynamicGrid.Visible = true;
        if (dt.Rows.Count > 0)
        {
            objMain.ReportDownload("Enrollment Target Raw Data", "Annual Plan", Convert.ToString(Session["username"]));
            ExportToCSVFile(dt, "EnrollmentTargetRawData");
        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }




    }
}