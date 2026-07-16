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


public partial class frmReportD2dSurvey : System.Web.UI.Page
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

                    LoadGroup();
                    //  objComman.BindDLL("mstlookup", "LookupCode,Description1 ", "LookupFlag='G'", "Description1", "Desc", ddlGender, "Description1", "LookupCode", "--All--");
                    LoadUserLeavel();
                    LoadUserLevel();
                    ViewState["1"] = "ss";
                    ViewState["Annual"] = "";
                    ViewState["D2dUser"] = "";

                    LinkButton5.Visible = true;
                    //if (Convert.ToString(Session["username"]) == "PMSAdmin" || Convert.ToString(Session["username"]) == "EGE2402" || Convert.ToString(Session["username"]) == "EGE7557" || Convert.ToString(Session["username"]) == "EGE7545" || Convert.ToString(Session["username"]) == "EGE8938" || Convert.ToString(Session["username"]) == "SuperAdmin")
                    //{
                    //    LinkButton5.Visible = true;
                    //}
                    //else
                    //{
                    //    LinkButton5.Visible = false;
                    //}
                }
                // btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");LinkButton8
            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }

        }
        if (hdnbtnValue.Value == "1")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "", "<SCRIPT LANGUAGE='javascript'>fnNew(true)</script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "", "<SCRIPT LANGUAGE='javascript'>fnNew(false)</script>", false);
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

                //  }
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
    public void LoadUserLevel()
    {
        if (Session["user_level_Role"].ToString() == "4")
        {

            // ddlGroup.Enabled = false;

        }
        else if (Session["user_level_Role"].ToString() == "3")
        {

            //  ddlGroup.Enabled = true;

        }
        else
        {

            //ddlGroup.Enabled = true;
        }

    }
    public DataTable CreateDataTable()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYear.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYear;
    }

    public DataTable CreateDataTableGrroup()
    {

        DataTable dtYearGrroup = new DataTable();
        dtYearGrroup.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYearGrroup.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYearGrroup;
    }
    public void LoadGroup()
    {
        string conditions = "";
        DataRow dr;
        if (Convert.ToString(Session["user_level"]) == "19")
        {
            DataTable dtYear = CreateDataTable();

            //dr = dtYear.NewRow();
            //dr["Type"] = "--Select--";
            //dr["ID"] = 0;
            //dtYear.Rows.Add(dr);

            dr = dtYear.NewRow();
            dr["Type"] = "Block Wise";
            dr["ID"] = 2;
            dtYear.Rows.Add(dr);

            dr = dtYear.NewRow();
            dr["Type"] = "Cluster Wise";
            dr["ID"] = 3;
            dtYear.Rows.Add(dr);
            //   objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlGroup, "Type", "ID", "Select");


        }
        else
        {
            DataTable dtYear = CreateDataTable();

            //dr = dtYear.NewRow();
            //dr["Type"] = "--Select--";
            //dr["ID"] = 0;
            //dtYear.Rows.Add(dr);

            dr = dtYear.NewRow();
            dr["Type"] = "District Wise";
            dr["ID"] = 1;
            dtYear.Rows.Add(dr);
            dr = dtYear.NewRow();
            dr["Type"] = "Block Wise";
            dr["ID"] = 2;
            dtYear.Rows.Add(dr);

            dr = dtYear.NewRow();
            dr["Type"] = "Cluster Wise";
            dr["ID"] = 3;
            dtYear.Rows.Add(dr);
            // objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlGroup, "Type", "ID", "Select");

        }
    }
    public void LoadYear()
    {
        string conditions = "";
        DataTable dtYear = objComman.Generate_Financial_Year();
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
        string ddlState = "";
        DataTable dtDistrict = null;
        string conditions = "";
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
            //foreach (ListItem item in chkDistrict.Items)
            //{

            //    item.Selected = true;

            //}
            ddlDistrict_SelectedIndexChanged(chkDistrict, null);
        }

        chkVillage.Items.Clear();
    }

    protected void ddlTpye_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["Annual"] = "";
        ViewState["D2dUser"] = "";

    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        string conditions = "";
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


    protected void LnkDeatild_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 98;

        LoadChildEnrollment(1);






    }
    protected void LnkDeatild1_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 98;

        VillageCompletionStatus(1);






    }
    public void VillageCompletionStatus(int Flag)
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

        if (Session["user_level_Role"].ToString() == "3" || Session["user_level_Role"].ToString() == "4")
        {
            if (ddlDistrict.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select District')</script>", false);

                return;
            }
        }

        string condition = string.Empty;
        if (Convert.ToInt32(rblDist.SelectedValue) == 2)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "  where   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

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
            if (ddlPhan.Length > 0)
            {
                conditions += " and mst5Village.ClusterCode in(" + ddlPhan + ") ";
            }
            if (ddlVillage.Length > 0)
            {
                conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
            }
        }
        if (Convert.ToInt32(rblDist.SelectedValue) == 1)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "  where   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

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
                conditions += " and mst5Village.ClusterCode in(" + ddlPhan + ") ";
            }
            if (ddlVillage.Length > 0)
            {
                conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
            }

        }

        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),


        };
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptVillageCompletionStatus]", cmdParameters);

        if (dt.Rows.Count > 0)
        {


            ExportToCSVFile(dt, "VillageCompletionStatus");


        }




    }

    public void LoadChildEnrollment(int Flag)
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

        if (Session["user_level_Role"].ToString() == "3" || Session["user_level_Role"].ToString() == "4")
        {
            if (ddlDistrict.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select District')</script>", false);

                return;
            }
        }
        string condition = string.Empty;


        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "  where   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and mst5Village.StateCode in(" + ddlStatecode + ") ";

        }
        if (Convert.ToInt32(rblDist.SelectedValue) == 2)
        {
            if (ddlDistrict.Length > 0)
            {
                conditions += " and mst5Village.AdminDistrictCode in(" + ddlDistrict + ") ";

            }

            if (ddlBlock.Length > 0)
            {

                conditions += " and mst5Village.MainBlockCode in(" + ddlBlock + ") ";


            }
            if (ddlPhan.Length > 0)
            {
                conditions += " and mst5Village.ClusterCode in(" + ddlPhan + ") ";
            }
            if (ddlVillage.Length > 0)
            {
                conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
            }
        }
        if (Convert.ToInt32(rblDist.SelectedValue) == 1)
        {
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
                conditions += " and mst5Village.ClusterCode in(" + ddlPhan + ") ";
            }
            if (ddlVillage.Length > 0)
            {
                conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
            }
        }

        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
        {
            if (txtDate.Text != "" && txtTodate.Text != "")
            {
                if (Convert.ToDateTime(txtDate.Text) == Convert.ToDateTime(txtTodate.Text))
                {


                    if (Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd") == Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd"))
                    {


                        DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                        DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                        conditions += " and year([Createdate])=" + DateTime.Now.Year + " and  month(Createdate)=" + DateTime.Now.Month + " and  day(Createdate)=" + DateTime.Now.Day + "";
                    }
                    else
                    {
                        DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                        DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                        string[] multiArray = txtDate.Text.Split(new Char[] { '/' });
                        string[] multiArray1 = txtTodate.Text.Split(new Char[] { '/' });

                        string Fdate = Fromdate.Year + "" + multiArray[1] + "" + multiArray[0];
                        string Tdate = Todate.Year + "" + multiArray1[1] + "" + multiArray1[0];
                        conditions += " and (Year(Createdate)*10000)+(Month(Createdate)*100+Day(Createdate)) Between '" + Fdate + "' and '" + Tdate + "'";
                    }
                }
                else
                {
                    DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                    DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                    string[] multiArray = txtDate.Text.Split(new Char[] { '/' });
                    string[] multiArray1 = txtTodate.Text.Split(new Char[] { '/' });

                    string Fdate = Fromdate.Year + "" + multiArray[1] + "" + multiArray[0];
                    string Tdate = Todate.Year + "" + multiArray1[1] + "" + multiArray1[0];
                    conditions += " and (Year(Createdate)*10000)+(Month(Createdate)*100+Day(Createdate)) Between '" + Fdate + "' and '" + Tdate + "'";

                    // conditions1 += " and Date BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
                }
            }
        }

        else
        {
            if (txtDate.Text != "" && txtTodate.Text != "")
            {
                if (Convert.ToDateTime(txtDate.Text) == Convert.ToDateTime(txtTodate.Text))
                {


                    if (Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd") == Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd"))
                    {


                        DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                        DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                        conditions += " and year([tblHoushold.Createdate])=" + DateTime.Now.Year + " and  month(tblHoushold.Createdate)=" + DateTime.Now.Month + " and  day(tblHoushold.Createdate)=" + DateTime.Now.Day + "";
                    }
                    else
                    {
                        DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                        DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                        string[] multiArray = txtDate.Text.Split(new Char[] { '/' });
                        string[] multiArray1 = txtTodate.Text.Split(new Char[] { '/' });

                        string Fdate = Fromdate.Year + "" + multiArray[1] + "" + multiArray[0];
                        string Tdate = Todate.Year + "" + multiArray1[1] + "" + multiArray1[0];
                        conditions += " and (Year(tblHoushold.Createdate)*10000)+(Month(tblHoushold.Createdate)*100+Day(tblHoushold.Createdate)) Between '" + Fdate + "' and '" + Tdate + "'";
                    }
                }
                else
                {
                    DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                    DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                    string[] multiArray = txtDate.Text.Split(new Char[] { '/' });
                    string[] multiArray1 = txtTodate.Text.Split(new Char[] { '/' });

                    string Fdate = Fromdate.Year + "" + multiArray[1] + "" + multiArray[0];
                    string Tdate = Todate.Year + "" + multiArray1[1] + "" + multiArray1[0];
                    conditions += " and (Year(tblHoushold.Createdate)*10000)+(Month(tblHoushold.Createdate)*100+Day(tblHoushold.Createdate)) Between '" + Fdate + "' and '" + Tdate + "'";

                    // conditions1 += " and Date BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
                }
            }
        }

        DataTable dt = null;

        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2023)
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@Con",conditions),
            new SqlParameter("@Fyear",ddlYear.SelectedValue),


            };



            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptDtdSurvey2026]", cmdParameters);
        }
        else
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@Con",conditions),


            };



            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptDtdSurvey]", cmdParameters);
        }
        if (dt.Rows.Count > 0)
        {
            objMain.ReportDownload("Door to Door Survey", "Door to Door Survey", Convert.ToString(Session["username"]));

            ExportToCSVFile(dt, "DoortoDoorSurvey");


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


    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["Grid"] != null)
            {


                // ExportToCSVFile(Session["ExportExcel"] as DataTable, "Tracker_Report");

            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
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



    private void ExportGridToExcel(GridView Gv, string FileName)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        FileName = "" + FileName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        Gv.GridLines = GridLines.Both;
        Gv.HeaderStyle.Font.Bold = true;
        Gv.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();

    }



    private void ExporttoExcel(DataTable table, string FileName)
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
            int columnscount = table.Columns.Count;


            for (int j = 0; j < columnscount; j++)
            {      //write in new column
                HttpContext.Current.Response.Write("<Td>");
                //Get column headers  and make it as bold in excel columns
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(table.Columns[j].ColumnName);
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
    #region Amit 20200515
    private void DisplayDataOnPopup(DataTable dt, string name)
    {
        if (dt.Rows.Count > 0)
        {
            lblMsg.Text = name;
            Session["GridViewData"] = dt;
            Session["Name"] = name;
            PopUpGrid.DataSource = dt;
            PopUpGrid.DataBind();
            MpexdrPopUp.Show();
        }
        else
        {
            PopUpGrid.DataSource = null;
            PopUpGrid.DataBind();
        }
    }
    protected void PopUpGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        PopUpGrid.PageIndex = e.NewPageIndex;
        if (Session["GridViewData"] != null)
        {
            DataTable dt = Session["GridViewData"] as DataTable;
            PopUpGrid.DataSource = dt;
            PopUpGrid.DataBind();
            MpexdrPopUp.Show();
        }


    }
    protected void lnkDownload_OnClick(object sender, EventArgs e)
    {
        try
        {
            DataTable dTExcel = Session["GridViewData"] as DataTable;
            ExporttoExcel(dTExcel, Convert.ToString(Session["Name"]));
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void LnkDeatild22_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 98;

        VillageSummary(1);



    }
    protected void LnkDeatild222_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 98;
        if (txtDate.Text != "" && txtTodate.Text != "")
        {
            VillageSummaryFC(1);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date Range')</script>", false);

            return;

        }



    }

    public void VillageSummary(int Flag)
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
            conditions += "  where   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

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
            conditions += " and mst5Village.ClusterCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }



        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),
               new SqlParameter("@Fyear",ddlYear.SelectedItem.Text  ),


        };
        DataSet dt = null;


        dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptSurveySummary2026]", cmdParameters);

        if (dt.Tables[0].Rows.Count > 0)
        {


            MultipuExeclTrackFinal2023(dt);


        }




    }



    public void VillageSummaryFC(int Flag)
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

        string conditionFC = string.Empty;

        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "  where   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

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
            conditions += " and mst5Village.ClusterCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }

        if (txtDate.Text != "" && txtTodate.Text != "")
        {
            if (Convert.ToDateTime(txtDate.Text) == Convert.ToDateTime(txtTodate.Text))
            {


                if (Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd") == Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd"))
                {


                    DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                    DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                    conditions += " and year([tblHoushold.Createdate])=" + DateTime.Now.Year + " and  month(tblHoushold.Createdate)=" + DateTime.Now.Month + " and  day(tblHoushold.Createdate)=" + DateTime.Now.Day + "";

                    conditionFC += " and year([tblHoushold.Createdate])=" + DateTime.Now.Year + " and  month(tblHoushold.Createdate)=" + DateTime.Now.Month + " and  day(tblHoushold.Createdate)=" + DateTime.Now.Day + "";


                }
                else
                {
                    DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                    DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                    string[] multiArray = txtDate.Text.Split(new Char[] { '/' });
                    string[] multiArray1 = txtTodate.Text.Split(new Char[] { '/' });

                    string Fdate = Fromdate.Year + "" + multiArray[1] + "" + multiArray[0];
                    string Tdate = Todate.Year + "" + multiArray1[1] + "" + multiArray1[0];
                    conditions += " and (Year(tblHoushold.Createdate)*10000)+(Month(tblHoushold.Createdate)*100+Day(tblHoushold.Createdate)) Between '" + Fdate + "' and '" + Tdate + "'";
                    conditionFC += " and (Year(tblHoushold.Createdate)*10000)+(Month(tblHoushold.Createdate)*100+Day(tblHoushold.Createdate)) Between '" + Fdate + "' and '" + Tdate + "'";


                }
            }
            else
            {
                DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                string[] multiArray = txtDate.Text.Split(new Char[] { '/' });
                string[] multiArray1 = txtTodate.Text.Split(new Char[] { '/' });

                string Fdate = Fromdate.Year + "" + multiArray[1] + "" + multiArray[0];
                string Tdate = Todate.Year + "" + multiArray1[1] + "" + multiArray1[0];
                conditions += " and (Year(tblHoushold.Createdate)*10000)+(Month(tblHoushold.Createdate)*100+Day(tblHoushold.Createdate)) Between '" + Fdate + "' and '" + Tdate + "'";
                conditionFC += " and (Year(tblHoushold.Createdate)*10000)+(Month(tblHoushold.Createdate)*100+Day(tblHoushold.Createdate)) Between '" + Fdate + "' and '" + Tdate + "'";



                // conditions1 += " and Date BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
            }
        }


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),
               new SqlParameter("@Fyear",ddlYear.SelectedItem.Text  ),
                 new SqlParameter("@FDate",conditionFC  ),


        };
        DataSet dt = null;


        dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptSurveySummary2026FC]", cmdParameters);

        if (dt.Tables[0].Rows.Count > 0)
        {


            MultipuExeclTrackFinal2023FC(dt);


        }




    }
    public void MultipuExeclTrackFinal2023FC(DataSet dtMain1)
    {

        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\D2DSurveysummaryFC.xlsx");
        var ws = wb.Worksheet(1);

        //var ws3 = wb.Worksheet(3);
        DataTable dt = dtMain1.Tables[0];


        //DataTable dt1 = dtMain1.Tables[1];


        ws.Cell(2, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 2;
        string str = "A2:I" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);



        filepath = StartupPath + "\\FCProgressSummary  " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
    public void MultipuExeclTrackFinal2023(DataSet dtMain1)
    {

        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\D2DSurveysummary.xlsx");
        var ws = wb.Worksheet(1);
        var ws1 = wb.Worksheet(2);
        var ws2 = wb.Worksheet(3);
        var ws3 = wb.Worksheet(4);
        var ws4 = wb.Worksheet(5);
        var ws5 = wb.Worksheet(6);
        //var ws1 = wb.Worksheet(2);
        //var ws3 = wb.Worksheet(3);
        DataTable dt = dtMain1.Tables[0];
        DataTable dt1 = dtMain1.Tables[1];
        DataTable dt2 = dtMain1.Tables[2];
        DataTable dt3 = dtMain1.Tables[3];
        DataTable dt4 = dtMain1.Tables[4];
        DataTable dt5 = dtMain1.Tables[5];

        //DataTable dt1 = dtMain1.Tables[1];


        ws.Cell(2, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 2;
        string str = "A2:AF" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);



        ws1.Cell(2, 1).InsertData(dt1.Rows);
        Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 2;
        string str1 = "A2:X" + ii1;
        ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);





        ws2.Cell(2, 1).InsertData(dt2.Rows);
        Int32 ii11 = Convert.ToInt32(dt2.Rows.Count) + 2;
        string str11 = "A2:W" + ii11;
        ws2.Range(str11).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws2.Range(str11).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws2.Range(str11).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws2.Range(str11).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);



        ws3.Cell(2, 1).InsertData(dt3.Rows);
        Int32 ii113 = Convert.ToInt32(dt3.Rows.Count) + 2;
        string str113 = "A2:V" + ii113;
        ws3.Range(str113).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws3.Range(str113).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws3.Range(str113).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws3.Range(str113).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        ws4.Cell(3, 1).InsertData(dt4.Rows);
        Int32 ii1132 = Convert.ToInt32(dt4.Rows.Count) + 2;
        string str1132 = "A2:G" + ii1132;
        ws4.Range(str1132).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws4.Range(str1132).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws4.Range(str1132).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws4.Range(str1132).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);



        ws5.Cell(3, 1).InsertData(dt5.Rows);
        Int32 ii1131 = Convert.ToInt32(dt5.Rows.Count) + 2;
        string str1131 = "A2:F" + ii1131;
        ws5.Range(str1131).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws5.Range(str1131).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws5.Range(str1131).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws5.Range(str1131).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);




        filepath = StartupPath + "\\D2DSurveysummary " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
    #endregion
}