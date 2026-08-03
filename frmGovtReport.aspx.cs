using Ionic.Zip;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class frmGovtReport : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    string conditions = "";
    DataTableMaskingHelper dataTableMaskingHelper = new DataTableMaskingHelper();

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
                }
                // btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }

        }
    }


    //public void LoadYear()
    //{
    //    DataTable dtYear = objComman.Generate_Financial_Year();
    //    objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

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
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

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

    protected void rblBlockType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(rblDist.SelectedValue) == 2)
        {
            ChkState.Items.Clear();
            chkDistrict.Items.Clear();
            chkBlock.Items.Clear();
            chkVillage.Items.Clear();
            LoadUserLeavelAdmin();
        }
        else
        {
            ChkState.Items.Clear();
            chkDistrict.Items.Clear();
            chkBlock.Items.Clear();
            chkVillage.Items.Clear();
            LoadUserLeavel();
        }
    }
    public void LoadUserLeavelAdmin()
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

            //ChkState.Enabled = true;
            //chkDistrict.Enabled = true;
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
            //foreach (ListItem item in ChkState.Items)
            //{

            //    item.Selected = true;

            //}

            //ChkState.Enabled = true;
            //chkDistrict.Enabled = true;
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
            //foreach (ListItem item in ChkState.Items)
            //{

            //    item.Selected = true;

            //}
            //// ChkState.SelectedIndex = 1;
            //ChkState.Enabled = false;
            //chkDistrict.Enabled = false;
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
            string strQry1 = "select distinct AdminDistrictCode as DistrictCode, dbo.TitleCase(upper(AdminDistrictName))  as DistrictName from mst5Village where DistrictCode in(  SELECT distinct mst2District.DistrictCode FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode  where   " + conditions + " and Fyear='" + ddlYear.SelectedItem.Text + "' ) and  Fyear='" + ddlYear.SelectedItem.Text + "'  order by DistrictName   ";


            // string strQry1 = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where " + conditions + "  order by DistrictName   ";


            DataTable dtDistrict = objMain.LoadData(strQry1);
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            chkDistrict.DataSource = dtDistrict;
            chkDistrict.DataTextField = "DistrictName";
            chkDistrict.DataValueField = "DistrictCode";
            chkDistrict.DataBind();

            if (Session["user_level_Role"].ToString() == "2")
            {
                //foreach (ListItem item in chkDistrict.Items)
                //{

                //    item.Selected = true;

                //}
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
            string strQry1 = " select distinct AdminDistrictCode as DistrictCode, dbo.TitleCase(upper(AdminDistrictName))  as DistrictName from mst5Village where DistrictCode in( SELECT DistrictCode FROM mst2District where " + conditions + ") and Fyear= '" + ddlYear.SelectedItem.Text + "'  order by DistrictName   ";
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
            //foreach (ListItem item in chkDistrict.Items)
            //{

            //    item.Selected = true;

            //}
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
            if (Convert.ToInt32(rblDist.SelectedValue) == 2)
            {
                string strQry1 = " select distinct AdminDistrictCode as DistrictCode, dbo.TitleCase(upper(AdminDistrictName))  as DistrictName from mst5Village where DistrictCode in(      sELECT distinct mst2District.DistrictCode as DistrictCode FROM MstusermultipleDist  ";
                strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions + "  and  Fyear='" + ddlYear.SelectedItem.Text + "') and  Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName  ";
                dtDistrict = objMain.LoadData(strQry1);
            }
            else
            {
                string strQry1 = " sELECT distinct mst2District.DistrictCode as DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist  ";
                strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions + "  and  Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName  ";
                dtDistrict = objMain.LoadData(strQry1);
            }
        }
        else
        {
            if (Convert.ToInt32(rblDist.SelectedValue) == 2)
            {
                string strQry = " select distinct AdminDistrictCode as DistrictCode, dbo.TitleCase(upper(AdminDistrictName))  as DistrictName from mst5Village where DistrictCode in( SELECT DistrictCode FROM mst2District where " + conditions + ")  and  Fyear='" + ddlYear.SelectedItem.Text + "'   order by DistrictName   ";
                dtDistrict = objMain.LoadData(strQry);
            }
            else
            {
                string strQry = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where " + conditions + "  order by DistrictName   ";
                dtDistrict = objMain.LoadData(strQry);
            }
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
            ddlDistrict_SelectedIndexChanged(chkDistrict, null);
        }

        chkVillage.Items.Clear();
    }

    protected void ddlTpye_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["Annual"] = "";
        ViewState["D2dUser"] = "";
        GV_DynamicGrid.DataSource = null;
        lblMonth.Visible = false;
        ddlMonth.Visible = false;
        GV_DynamicGrid.DataBind();
        if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
        {
            lblMonth.Text = "Month";
            lblMonth.Visible = true;
            ddlMonth.Visible = true;
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
        if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
        {
            lblMonth.Text = "Quarter";
            lblMonth.Visible = true;
            ddlMonth.Visible = true;
            DataTable dtYear = CreateDataTable();
            DataRow dr;
            dr = dtYear.NewRow();
            dr["Type"] = "Q1";
            dr["ID"] = 1;
            dtYear.Rows.Add(dr);
            dr = dtYear.NewRow();
            dr["Type"] = "Q2";
            dr["ID"] = 2;
            dtYear.Rows.Add(dr);
            dr = dtYear.NewRow();
            dr["Type"] = "Q3";
            dr["ID"] = 3;
            dtYear.Rows.Add(dr);
            dr = dtYear.NewRow();
            dr["Type"] = "Q4";
            dr["ID"] = 4;
            dtYear.Rows.Add(dr);
            objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlMonth, "Type", "ID", "Select");

        }

    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            AlllStateCode();
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

            chkVillage.Items.Clear();
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {

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
        FillCVillage();
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillage();
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
        string ConAdmin = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "DistrictCode in(" + ddlDistrict + ") ";
            ConAdmin = "AdminDistrictCode in(" + ddlDistrict + ") ";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "DistrictCode in(" + ddlDistrict + ")  ";
            ConAdmin = "AdminDistrictCode in(" + ddlDistrict + ") ";
        }
        else if (Session["user_level_Role"].ToString() == "4")
        {
            conditions = "DistrictCode in(" + ddlDistrict + ")   and BlockCode in( " + Session["BlockCode"].ToString() + " ) and FYear ='" + ddlYear.SelectedItem.Text + "' ";
            ConAdmin = "AdminDistrictCode in(" + ddlDistrict + ")   and BlockCode in( " + Session["BlockCode"].ToString() + " ) and FYear ='" + ddlYear.SelectedItem.Text + "' ";

        }
        else
        {
            conditions = "DistrictCode in(" + ddlDistrict + ")  ";
            ConAdmin = "AdminDistrictCode in(" + ddlDistrict + ")  ";
        }
        //     objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        if (Convert.ToInt32(rblDist.SelectedValue) == 2)
        {
            string strQry = " SELECT  distinct MainBlockCode as BlockCode, dbo.TitleCase(upper(MainBlockName))  as BlockName FROM mst5Village where " + ConAdmin + " and FYear ='" + ddlYear.SelectedItem.Text + "' order by BlockName   ";
            DataTable dtDistrict = objMain.LoadData(strQry);
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            chkBlock.DataSource = dtDistrict;
            chkBlock.DataTextField = "BlockName";
            chkBlock.DataValueField = "BlockCode";
            chkBlock.DataBind();
        }

        else
        {
            string strQry = "  SELECT BlockCode, dbo.TitleCase(upper(BlockName))  as BlockName FROM mst3Block where " + conditions + "  order by BlockName   ";
            DataTable dtDistrict = objMain.LoadData(strQry);
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            chkBlock.DataSource = dtDistrict;
            chkBlock.DataTextField = "BlockName";
            chkBlock.DataValueField = "BlockCode";
            chkBlock.DataBind();

        }


        chkVillage.Items.Clear();

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

        conditions = "DistrictCode in(" + ddlDistrict + ")  and BlockCode in(" + ddlBlock + ") ";
        string strQry = "  SELECT PanchayatCode, dbo.TitleCase(upper(PanchayatName))  as PanchayatName FROM mstPanchayat where " + conditions + "  order by PanchayatName   ";
        dtDistrict = objMain.LoadData(strQry);



        // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        //ddlPanchayat.DataSource = dtDistrict;
        //ddlPanchayat.DataTextField = "PanchayatName";
        //ddlPanchayat.DataValueField = "PanchayatCode";
        //ddlPanchayat.DataBind();

        // objComman.BindDLL("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "--All--");


        chkVillage.Items.Clear();

    }
    public void FillCVillage()
    {
        conditions = "";
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


        conditions = "";
        if (Convert.ToInt32(rblDist.SelectedValue) == 2)
        {
            conditions = "AdminDistrictCode in(" + ddlDistrict + ")  and MainBlockCode in(" + ddlBlock + ") and FYear ='" + ddlYear.SelectedItem.Text + "'  ";

        }
        else
        {
            conditions = "DistrictCode in(" + ddlDistrict + ")  and BlockCode in(" + ddlBlock + ") ";

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


    protected void LnkDist_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 111;
        if (ddlGroup.SelectedIndex > 0)
        {

            LoadDistProfileData(1);

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Report Level ')</script>", false);
        }


    }
    protected void LnkGovernmentReport_OnClick(object sender, EventArgs e)
    {

        if (ddlGroup.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Report Level ')</script>", false);
            return;
        }
        if (ddlTpye.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Report Type ')</script>", false);
            return;
        }
        if (ddlMonth.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Month/Quarter Type ')</script>", false);
            return;
        }
        ViewState["1"] = 112;


        //     LoadGovt(1);

        LoadGovtNew(1);



    }


    protected void LnkEnrolment_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 113;


        LoadDistEnrollData(1);



    }


    public void LoadDistEnrollData(int Flag)
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



        conditions = string.Empty;
        if (Convert.ToInt32(rblDist.SelectedValue) == 2)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (ddlStatecode.Length > 0)
            {
                conditions += " and mst5Village.StateCode in(" + ddlStatecode + ") ";

            }
            if (ddlDistrict.Length > 0)
            {




                conditions += " and mst5Village.AdminDistrictCode in(" + ddlDistrict + ") ";

            }
            if (ddlBlock.Length > 0)
            {

                conditions += " and mst5Village.MainBlockCode in(" + ddlBlock + ") ";


            }
        }


        if (Convert.ToInt32(rblDist.SelectedValue) == 1)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

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

            if (ddlVillage.Length > 0)
            {
                conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
            }
        }

        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Condition", conditions),
            new SqlParameter("@Con", ddlYear.SelectedItem.Text),
            new SqlParameter("@FYear", ddlYear.SelectedValue),
        };

        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptGovTargetandAchD2dDetials]", cmdParameters);

        dataTableMaskingHelper.DecryptAndMaskDataTable(dt, true, "Child Name", "Father Name", "Samagra ID");

        if (dt.Columns.Contains("DOB"))
        {
            dt.Columns.Remove("DOB");
        }

        ViewState["Dist"] = dt;
        GV_DynamicGrid.Visible = true;
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();

        if (dt.Rows.Count > 0)
        {
            ExportToCSVFile(dt, "EnrolmentRawGovt");
        }
    }

    public void LoadDistProfileData(int Flag)
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



        conditions = string.Empty;
        if (Convert.ToInt32(rblDist.SelectedValue) == 2)

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




                conditions += " and mst5Village.AdminDistrictCode in(" + ddlDistrict + ") ";

            }
            if (ddlBlock.Length > 0)
            {

                conditions += " and mst5Village.MainBlockCode in(" + ddlBlock + ") ";


            }
        }


        if (Convert.ToInt32(rblDist.SelectedValue) == 1)
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

            if (ddlVillage.Length > 0)
            {
                conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
            }
        }




        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@schoolCode",conditions),
            new SqlParameter("@Year",ddlYear.SelectedItem.Text),
                new SqlParameter("@Flag",Flag),
                new SqlParameter("@Groupby",ddlGroup.SelectedValue),
                   new SqlParameter("@EGAdminDist",rblDist.SelectedValue),

        };
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rpDistProfile]", cmdParameters);



        ViewState["Dist"] = dt;
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


    public void LoadGovt(int Flag)
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



        conditions = string.Empty;
        if (Convert.ToInt32(rblDist.SelectedValue) == 2)
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




                conditions += " and mst5Village.AdminDistrictCode in(" + ddlDistrict + ") ";

            }
            if (ddlBlock.Length > 0)
            {

                conditions += " and mst5Village.MainBlockCode in(" + ddlBlock + ") ";


            }
        }


        if (Convert.ToInt32(rblDist.SelectedValue) == 1)
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

            if (ddlVillage.Length > 0)
            {
                conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
            }
        }




        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@schoolCode",conditions),
            new SqlParameter("@Year",ddlYear.SelectedItem.Text),
                new SqlParameter("@Flag",Flag),
                new SqlParameter("@Groupby",ddlGroup.SelectedValue),
                   new SqlParameter("@EGAdminDist",rblDist.SelectedValue),
                    new SqlParameter("@ReportType",ddlTpye.SelectedValue),
                       new SqlParameter("@Mmonth",ddlMonth.SelectedValue),
                          new SqlParameter("@MmonthName",ddlMonth.SelectedItem.Text),

        };
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rpGovReport2020]", cmdParameters);


        if (dt.Rows.Count > 0)
        {
            dt.Columns.Remove("SrNew");
        }

        ViewState["Dist"] = dt;
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


    public void LoadGovtNew(int Flag)
    {

        string ddlBlock = "";
        string ddlDistrict = "";
        string NewCon = "";
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



        conditions = string.Empty;
        if (Convert.ToInt32(rblDist.SelectedValue) == 2)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "    where mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
                NewCon += "    where mst2District.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
            }
            if (ddlStatecode.Length > 0)
            {
                conditions += " and mst5Village.StateCode in(" + ddlStatecode + ") ";
                NewCon += " and mst2District.StateCode in(" + ddlStatecode + ") ";
            }
            if (ddlDistrict.Length > 0)
            {




                conditions += " and mst5Village.AdminDistrictCode in(" + ddlDistrict + ") ";
                NewCon += " and AdminDistrictCode in(" + ddlDistrict + ") ";

            }
            if (ddlBlock.Length > 0)
            {

                conditions += " and mst5Village.MainBlockCode in(" + ddlBlock + ") ";


            }
        }


        if (Convert.ToInt32(rblDist.SelectedValue) == 1)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "    where mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
                NewCon += "    where mst2District.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (ddlStatecode.Length > 0)
            {
                conditions += " and mst5Village.StateCode in(" + ddlStatecode + ") ";

                NewCon += " and mst2District.StateCode in(" + ddlStatecode + ") ";
            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
                NewCon += " and mst2District.DistrictCode in(" + ddlDistrict + ") ";

            }

            if (ddlBlock.Length > 0)
            {

                conditions += " and mst5Village.BlockCode in(" + ddlBlock + ") ";


            }

            if (ddlVillage.Length > 0)
            {
                conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
            }
        }
        DataTable dt = null;

        if (Convert.ToInt32(ddlYear.SelectedValue) == 2020)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                 new SqlParameter("@schoolCode",conditions),
                 new SqlParameter("@Year",ddlYear.SelectedItem.Text),
                 new SqlParameter("@Flag",Flag),
                 new SqlParameter("@Groupby",ddlGroup.SelectedValue),
                 new SqlParameter("@EGAdminDist",rblDist.SelectedValue),
                 new SqlParameter("@ReportType",ddlTpye.SelectedValue),
                 new SqlParameter("@Mmonth",ddlMonth.SelectedValue),
                 new SqlParameter("@MmonthName",ddlMonth.SelectedItem.Text),
                 new SqlParameter("@schoolCodeNew",NewCon),
            };
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rpGovReport2020New]", cmdParameters);
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2021)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                    new SqlParameter("@schoolCode",conditions),
                    new SqlParameter("@Year",ddlYear.SelectedItem.Text),
                    new SqlParameter("@Flag",Flag),
                    new SqlParameter("@Groupby",ddlGroup.SelectedValue),
                    new SqlParameter("@EGAdminDist",rblDist.SelectedValue),
                    new SqlParameter("@ReportType",ddlTpye.SelectedValue),
                    new SqlParameter("@Mmonth",ddlMonth.SelectedValue),
                    new SqlParameter("@MmonthName",ddlMonth.SelectedItem.Text),
                    new SqlParameter("@schoolCodeNew",NewCon),
            };
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rpGovReport2021New]", cmdParameters);

        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2022)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                new SqlParameter("@schoolCode",conditions),
                new SqlParameter("@Year",ddlYear.SelectedItem.Text),
                new SqlParameter("@Flag",Flag),
                new SqlParameter("@Groupby",ddlGroup.SelectedValue),
                new SqlParameter("@EGAdminDist",rblDist.SelectedValue),
                new SqlParameter("@ReportType",ddlTpye.SelectedValue),
                new SqlParameter("@Mmonth",ddlMonth.SelectedValue),
                new SqlParameter("@MmonthName",ddlMonth.SelectedItem.Text),
                new SqlParameter("@schoolCodeNew",NewCon),
            };
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rpGovReport2022New]", cmdParameters);
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Remove("NewSrCode");
            }
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2023)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                new SqlParameter("@schoolCode",conditions),
                new SqlParameter("@Year",ddlYear.SelectedItem.Text),
                new SqlParameter("@Flag",Flag),
                new SqlParameter("@Groupby",ddlGroup.SelectedValue),
                new SqlParameter("@EGAdminDist",rblDist.SelectedValue),
                new SqlParameter("@ReportType",ddlTpye.SelectedValue),
                new SqlParameter("@Mmonth",ddlMonth.SelectedValue),
                new SqlParameter("@MmonthName",ddlMonth.SelectedItem.Text),
                new SqlParameter("@schoolCodeNew",NewCon),
            };
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rpGovReport2023New]", cmdParameters);
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Remove("NewSrCode");
            }
        }
        else
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                new SqlParameter("@schoolCode",conditions),
                new SqlParameter("@Year",ddlYear.SelectedItem.Text),
                new SqlParameter("@Flag",Flag),
                new SqlParameter("@Groupby",ddlGroup.SelectedValue),
                new SqlParameter("@EGAdminDist",rblDist.SelectedValue),
                new SqlParameter("@ReportType",ddlTpye.SelectedValue),
                new SqlParameter("@Mmonth",ddlMonth.SelectedValue),
                new SqlParameter("@MmonthName",ddlMonth.SelectedItem.Text),
                new SqlParameter("@schoolCodeNew",NewCon),
            };

            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rpGovReport2024New]", cmdParameters);
            if (dt.Rows.Count > 0)
            {
                dt.Columns.Remove("NewSrCode");
            }
        }



        if (dt.Rows.Count > 0)
        {
            dt.Columns.Remove("SrNew");
        }

        ViewState["Dist"] = dt;
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

    private void GenerateExcelDistProfile(string FIleName)
    {
        try
        {


            DataTable dt = ViewState["Dist"] as DataTable;
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
                if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
                {
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='13' style='text-align:Center;border:.2pt solid windowtext;'>District Profile Summary </td>");
                    HttpContext.Current.Response.Write("</tr>");
                }
                if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
                {
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='14' style='text-align:Center;border:.2pt solid windowtext;'>District Profile Summary </td>");
                    HttpContext.Current.Response.Write("</tr>");
                }
                if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
                {
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='15' ' style='text-align:Center;border:.2pt solid windowtext;'>District Profile Summary </td>");
                    HttpContext.Current.Response.Write("</tr>");
                }

                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                int columnscount = GV_DynamicGrid.HeaderRow.Cells.Count;

                for (int j = 0; j < columnscount; j++)
                {
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> " + GV_DynamicGrid.HeaderRow.Cells[j].Text + "</th>");
                }

                HttpContext.Current.Response.Write("</tr>");
                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    HttpContext.Current.Response.Write("<tr>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");
                    }
                }
                HttpContext.Current.Response.Write("</tr>");
                HttpContext.Current.Response.Write("<tr>");
                for (int J = 0; J < 1; J++)
                {
                    if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
                    {
                        #region
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0)
                            {

                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");

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
                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                        #endregion
                    }


                    if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
                    {
                        #region
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0 || c == 1)
                            {
                                if (c == 1)
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                                }
                                else
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'></td>");
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
                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                        #endregion
                    }
                    if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
                    {
                        #region
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0 || c == 1 || c == 2)
                            {
                                if (c == 2)
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                                }
                                else
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'></td>");
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
                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                        #endregion
                    }
                }

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


    private void GenerateExcelGovProfile(string FIleName)
    {
        try
        {


            DataTable dt = ViewState["Dist"] as DataTable;
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
                if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
                {
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='7' style='text-align:Center;border:.2pt solid windowtext;'>Government Report </td>");
                    HttpContext.Current.Response.Write("</tr>");
                }
                if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
                {
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='8' style='text-align:Center;border:.2pt solid windowtext;'>Government Report </td>");
                    HttpContext.Current.Response.Write("</tr>");
                }
                if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
                {
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='9' ' style='text-align:Center;border:.2pt solid windowtext;'>Government Report</td>");
                    HttpContext.Current.Response.Write("</tr>");
                }

                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                int columnscount = GV_DynamicGrid.HeaderRow.Cells.Count;

                for (int j = 0; j < columnscount; j++)
                {
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> " + GV_DynamicGrid.HeaderRow.Cells[j].Text + "</th>");
                }

                HttpContext.Current.Response.Write("</tr>");
                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    HttpContext.Current.Response.Write("<tr>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");
                    }
                }
                HttpContext.Current.Response.Write("</tr>");
                //HttpContext.Current.Response.Write("<tr>");
                //for (int J = 0; J < 1; J++)
                //{
                //    if (Convert.ToInt32(ddlGroup.SelectedValue) == 1 )
                //    {
                //        #region
                //        for (int c = 0; c < dt.Columns.Count; c++)
                //        {
                //            if (c == 0 || c == 1)
                //            {
                //                if (c == 1)
                //                {
                //                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");

                //                }
                //                else
                //                {
                //                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'> </td>");
                //                }
                //            }
                //            else
                //            {
                //                string Col = "[" + dt.Columns[c].ColumnName + "]";
                //                int sum = 0;
                //                if (Convert.ToString(dt.Rows[J][dt.Columns[c].ColumnName]) == "")
                //                {
                //                }
                //                else
                //                {
                //                    sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));
                //                }
                //                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                //            }
                //        }
                //        #endregion
                //    }
                //    if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
                //    {
                //        #region
                //        for (int c = 0; c < dt.Columns.Count; c++)
                //        {
                //            if (c == 0 || c == 1 || c == 2)
                //            {
                //                if (c == 2)
                //                {
                //                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");

                //                }
                //                else
                //                {
                //                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'> </td>");
                //                }
                //            }
                //            else
                //            {
                //                string Col = "[" + dt.Columns[c].ColumnName + "]";
                //                int sum = 0;
                //                if (Convert.ToString(dt.Rows[J][dt.Columns[c].ColumnName]) == "")
                //                {
                //                }
                //                else
                //                {
                //                    sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));
                //                }
                //                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                //            }
                //        }
                //        #endregion
                //    }

                //    if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
                //    {
                //        #region
                //        for (int c = 0; c < dt.Columns.Count; c++)
                //        {
                //            if (c == 0 || c == 1 || c == 2 || c == 3)
                //            {
                //                if (c == 3)
                //                {
                //                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                //                }
                //                else
                //                {
                //                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'></td>");
                //                }
                //            }
                //            else
                //            {
                //                string Col = "[" + dt.Columns[c].ColumnName + "]";
                //                int sum = 0;
                //                if (Convert.ToString(dt.Rows[J][dt.Columns[c].ColumnName]) == "")
                //                {
                //                }
                //                else
                //                {
                //                    sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));
                //                }
                //                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                //            }
                //        }
                //        #endregion
                //    }
                //}

                //HttpContext.Current.Response.Write("</tr>");
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
    protected void btnImport_Click(object sender, EventArgs e)
    {

        if (ViewState["1"].ToString() == "111")
        {
            DataTable dt = (DataTable)ViewState["Dist"];
            GenerateExcelDistProfile("DistrictProfileSummary");
        }
        if (ViewState["1"].ToString() == "112")
        {
            DataTable dt = (DataTable)ViewState["Dist"];
            GenerateExcelGovProfile("GovernmentReport");
        }
    }







    protected void GV_DynamicGrid1_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_DynamicGrid.PageIndex = e.NewPageIndex;
        if (Session["Dist"] != null)
        {

            DataTable Dt = Session["Dist"] as DataTable;
            GV_DynamicGrid.DataSource = Dt;
            GV_DynamicGrid.DataBind();
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


}