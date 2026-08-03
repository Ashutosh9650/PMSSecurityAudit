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


public partial class frmD2dSurveyExpansionRaj : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    SqlInjection sqlInjection = new SqlInjection();
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

                    LoadGroup();
                    //  objComman.BindDLL("mstlookup", "LookupCode,Description1 ", "LookupFlag='G'", "Description1", "Desc", ddlGender, "Description1", "LookupCode", "--All--");
                    LoadUserLeavel();
                    LoadUserLevel();
                    ViewState["1"] = "ss";
                    ViewState["Annual"] = "";
                    ViewState["D2dUser"] = "";
                    //if (Convert.ToString(Session["username"]) == "EGE7758" || Convert.ToString(Session["username"]) == "EGE0102" || Convert.ToString(Session["username"]) == "EGE4782" || Convert.ToString(Session["username"]) == "EGE8206" || Convert.ToString(Session["username"]) == "EGE4426" || Convert.ToString(Session["username"]) == "EGE8011" || Convert.ToString(Session["username"]) == "EGE0822" || Convert.ToString(Session["username"]) == "EGE0892" || Convert.ToString(Session["username"]) == "EGE5644" || Convert.ToString(Session["username"]) == "EGE7969" ||  Convert.ToString(Session["username"]) == "EGE3641" || Convert.ToString(Session["username"]) == "EGE8168" || Convert.ToString(Session["username"]) == "EGE4190" || Convert.ToString(Session["username"]) == "EGE2402" || Convert.ToString(Session["username"]) == "EGE7692" || Convert.ToString(Session["username"]) == "EGE4076" || Convert.ToString(Session["username"]) == "EGE2841" || Convert.ToString(Session["username"]) == "PMSAdmin" || Convert.ToString(Session["username"]) == "EGE7557" || Convert.ToString(Session["username"]) == "SuperAdmin")
                    //{
                    //    LinkButton3.Visible = true;
                    //    LinkButton52.Visible = true;
                    //    LinkButton2.Visible = true;
                    //    LinkButton45.Visible = true;
                    //    LinkButton4.Visible = true;
                    //    LinkButton5.Visible = true;
                    //}
                    //else
                    //{
                    //    LinkButton3.Visible = false;
                    //    LinkButton52.Visible = false;
                    //    LinkButton2.Visible = false;
                    //    LinkButton45.Visible = false;
                    //    LinkButton4.Visible = false;
                    //    LinkButton5.Visible = false;
                    //}
                    //if (Convert.ToString(Session["username"]) == "EGE0102" || Convert.ToString(Session["username"]) == "SuperAdmin" || Convert.ToString(Session["username"]) == "PMSAdmin" || Convert.ToString(Session["username"]) == "EGE7557")
                    //{
                    //    LinkButton6.Visible = true;
                    //}
                    //else
                    //{
                    //    LinkButton6.Visible = false;
                    //}
                    // btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");LinkButton8
                }
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
    public void LoadUserLeavel()
    {
        string conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            //string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State order by StateName   ";
            //DataTable dtState = objMain.LoadData(strQry1);
            //ChkState.DataSource = dtState;
            //ChkState.DataTextField = "StateName";
            //ChkState.DataValueField = "StateCode";
            //ChkState.DataBind();
            AlllStateCode();
            ChkState.Enabled = true;
            chkDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            AlllStateCode();
            //conditions = "UserName='" + Session["username"].ToString() + "' ";
            //string strQry1 = "  SELECT distinct mst1State.StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode inner join mst1State on mst1State.StateCode=mst2District.StateCode where   " + conditions + "  and mst1State.StateCode='99' order by StateName   ";
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
            AlllStateCode();
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            ////objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            //string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State  where   " + conditions + "   order by StateName   ";
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
            foreach (ListItem item in ChkState.Items)
            {

                item.Selected = true;

            }
            ddlState_SelectedIndexChanged(chkDistrict, null);
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
            string strQry1 = "  SELECT distinct mst2District.DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode  where   " + conditions + " and Fyear='" + ddlYear.SelectedItem.Text + "'  and mst2District.StateCode='99' order by DistrictName   ";


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
        string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State where StateCode='99'   order by StateName   ";
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

    public void FillCBDist1()
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


        conditions = "StateCode in(" + ddlState + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";

        //else if (Session["user_level_Role"].ToString() == "2")
        //{
        //    conditions = " mst2District.StateCode in(" + ddlState + ") and UserName='" + Session["username"].ToString() + "' ";
        //}
        //else
        //{
        //    conditions = "StateCode  in(" + ddlState + ") and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";


        //}
        //if (Session["user_level_Role"].ToString() == "2")
        //{
        //    //if (ddlYear.SelectedValue.ToString() == "2016")
        //    //{

        //    //    string strQry1 = "  SELECT distinct mst2District.DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where EGDistrictCode in(     SELECT distinct mst2District.EGDistrictCode  FROM MstusermultipleDist     where   " + conditions + " )  and Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName   ";
        //    //    dtDistrict = objMain.LoadData(strQry1);
        //    //}

        //    //if (ddlYear.SelectedValue.ToString() == "2017")
        //    //{

        //    //    string strQry1 = "  SELECT distinct mst2District.DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where EGDistrictCode in(     SELECT distinct mst2District.EGDistrictCode  FROM MstusermultipleDist     where   " + conditions + " )  and Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName   ";
        //    //    dtDistrict = objMain.LoadData(strQry1);
        //    //}
        //    //if (ddlYear.SelectedValue.ToString() == "2018")
        //    //{

        //    //    string strQry1 = "  SELECT distinct mst2District.DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist   inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode  where   " + conditions + " and Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName   ";
        //    //    dtDistrict = objMain.LoadData(strQry1);
        //    //}
        //    string strQry1 = "       sELECT distinct mst2District.DistrictCode as DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist  ";
        //    strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions + "  and  Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName  ";
        //    dtDistrict = objMain.LoadData(strQry1);
        //}
        //else
        //{
        string strQry = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where " + conditions + "  order by DistrictName   ";
        dtDistrict = objMain.LoadData(strQry);
        // }

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

    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
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
            //if (Session["user_level_Role"].ToString() == "2")
            //{

            //    conditions = "UserName='" + Session["username"].ToString() + "' ";
            //    string strQry1 = "  SELECT distinct mst1State.StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode inner join mst1State on mst1State.StateCode=mst2District.StateCode where   " + conditions + "  order by StateName   ";
            //    DataTable dtState = objMain.LoadData(strQry1);
            //    ChkState.DataSource = dtState;
            //    ChkState.DataTextField = "StateName";
            //    ChkState.DataValueField = "StateCode";
            //    ChkState.DataBind();
            //}
            if (Convert.ToInt32(ddlYear.SelectedValue) >= 2024)
            {
                AlllStateCode();
            }
            else
            {
                conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
                //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
                string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State  where  StateCode='99'  order by StateName   ";
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
            ddlBlock_SelectedIndexChanged(ddlDistrict, null);
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

        conditions = "DistrictCode in(" + ddlDistrict + ")  and BlockCode in(" + ddlBlock + ")";
        string strQry = "  SELECT ClusterCode, dbo.TitleCase(upper(ClusterName))  as ClusterName FROM mstcluster where " + conditions + "  order by ClusterName   ";
        dtDistrict = objMain.LoadData(strQry);



        // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        ddlPanchayat.DataSource = dtDistrict;
        ddlPanchayat.DataTextField = "ClusterName";
        ddlPanchayat.DataValueField = "ClusterCode";
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
    protected void LnkDeatild23_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 98;

        VillageVillageSubmission(1);



    }
    protected void LnkDeatild22_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 98;

        VillageSummary(1);



    }
    protected void LnkQuality_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 98;

        VillageSummaryAlter(1);



    }
    protected void LnkDeatild11_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 98;

        VillageTBStatus(1);


    }
    protected void LnkDeatild111_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 98;

        VillageTBStatus(2);


    }
    public void VillageTBStatus(int Flag)
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
             new SqlParameter("@Flag",Flag),


        };
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptBalikaAndInfluencerRaj]", cmdParameters);

        if (dt.Rows.Count > 0)
        {
            int rowCount = dt.Rows.Count;


            string[] processedProbableTeamBalikaName = new string[rowCount];
            string[] processedContactNumber = new string[rowCount];
            string[] processedUserName = new string[rowCount];
            string[] processedInfluencerName = new string[rowCount];
            string[] processedInfluencerContactNumber = new string[rowCount];


            int uniqueChildCodeIndex = dt.Columns.IndexOf("UniqueChildCode"); // Change in later

            int probableTeamBalikaNameIndex = dt.Columns.IndexOf("Probable Team Balika Name");
            int contactNumberIndex = dt.Columns.IndexOf("Contact Number");
            int userNameIndex = dt.Columns.IndexOf("User Name");
            int influencerNameIndex = dt.Columns.IndexOf("Influencer Name");
            int influencerContactNumberIndex = dt.Columns.IndexOf("Influencer Contact Number");

            // ==========================================
            // 3. PARALLEL FOR LOOP: Decryption & Masking
            // ==========================================
            System.Threading.Tasks.Parallel.For(0, rowCount, i =>
            {
                DataRow row = dt.Rows[i];

                // Unique Child Code extract karein decryption salt ke liye
                string uniqueCode = (uniqueChildCodeIndex != -1 && row[uniqueChildCodeIndex] != DBNull.Value)
                                    ? row[uniqueChildCodeIndex].ToString()
                                    : string.Empty;

                // 1. Probable Team Balika Name
                if (probableTeamBalikaNameIndex != -1 && row[probableTeamBalikaNameIndex] != DBNull.Value)
                {
                    string enc = row[probableTeamBalikaNameIndex].ToString();
                    if (!string.IsNullOrEmpty(enc))
                    {
                        processedProbableTeamBalikaName[i] = sqlInjection.DecryptMatchingWithSessionMasking(enc, "Probable Team Balika Name");
                    }
                }

                // 2. Contact Number
                if (contactNumberIndex != -1 && row[contactNumberIndex] != DBNull.Value)
                {
                    string enc = row[contactNumberIndex].ToString();
                    if (!string.IsNullOrEmpty(enc))
                    {
                        processedContactNumber[i] = sqlInjection.DecryptMatchingWithSessionMasking(enc, "Contact Number");
                    }
                }

                // 3. User Name
                if (userNameIndex != -1 && row[userNameIndex] != DBNull.Value)
                {
                    string enc = row[userNameIndex].ToString();
                    if (!string.IsNullOrEmpty(enc))
                    {
                        processedUserName[i] = sqlInjection.DecryptMatchingWithSessionMasking(enc, "User Name");
                    }
                }

                // 4. Influencer Name
                if (influencerNameIndex != -1 && row[influencerNameIndex] != DBNull.Value)
                {
                    string enc = row[influencerNameIndex].ToString();
                    if (!string.IsNullOrEmpty(enc))
                    {
                        processedInfluencerName[i] = sqlInjection.DecryptMatchingWithSessionMasking(enc, "Influencer Name");
                    }
                }

                // 5. Influencer Contact Number
                if (influencerContactNumberIndex != -1 && row[influencerContactNumberIndex] != DBNull.Value)
                {
                    string enc = row[influencerContactNumberIndex].ToString();
                    if (!string.IsNullOrEmpty(enc))
                    {
                        processedInfluencerContactNumber[i] = sqlInjection.DecryptMatchingWithSessionMasking(enc, "Influencer Contact Number");
                    }
                }
            });

            // ==========================================
            // 4. SEQUENTIAL LOOP: Update DataTable
            // ==========================================
            dt.BeginLoadData();

            for (int i = 0; i < rowCount; i++)
            {
                if (processedProbableTeamBalikaName[i] != null && probableTeamBalikaNameIndex != -1)
                    dt.Rows[i][probableTeamBalikaNameIndex] = processedProbableTeamBalikaName[i];

                if (processedContactNumber[i] != null && contactNumberIndex != -1)
                    dt.Rows[i][contactNumberIndex] = processedContactNumber[i];

                if (processedUserName[i] != null && userNameIndex != -1)
                    dt.Rows[i][userNameIndex] = processedUserName[i];

                if (processedInfluencerName[i] != null && influencerNameIndex != -1)
                    dt.Rows[i][influencerNameIndex] = processedInfluencerName[i];

                if (processedInfluencerContactNumber[i] != null && influencerContactNumberIndex != -1)
                    dt.Rows[i][influencerContactNumberIndex] = processedInfluencerContactNumber[i];
            }

            dt.EndLoadData();

            if (Flag == 1)
            {
                ExportToCSVFile(dt, "Team Balika Identification");
            }
            if (Flag == 2)
            {
                ExportToCSVFile(dt, "Village Influencer Detail");
            }
        }
    }

    public void VillageSummaryAlter(int Flag)
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
        string condition1 = string.Empty;
        string condition2 = string.Empty;


        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "  and   mstCluster.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
            condition1 += "  and   mst2District.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
            condition2 += "  where   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and mstCluster.StateCode in(" + ddlStatecode + ") ";
            condition1 += " and mst2District.StateCode in(" + ddlStatecode + ") ";
            condition2 += " and mst5Village.StateCode in(" + ddlStatecode + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mstCluster.DistrictCode in(" + ddlDistrict + ") ";

            condition1 += " and mst2District.DistrictCode in(" + ddlDistrict + ") ";
            condition2 += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }

        if (ddlBlock.Length > 0)
        {

            conditions += " and mstCluster.BlockCode in(" + ddlBlock + ") ";
            // condition1 += " and mst2District.BlockCode in(" + ddlBlock + ") ";
            condition2 += " and mst5Village.BlockCode in(" + ddlBlock + ") ";

        }




        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),
               new SqlParameter("@Con1",condition1),
                  new SqlParameter("@Con2",condition2),


        };
        DataSet dt = null;


        dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptHouseSummaryAlter]", cmdParameters);

        if (dt.Tables[0].Rows.Count > 0)
        {


            MultipuExeclTrackFinal2023(dt);


        }




    }
    public void MultipuExeclTrackFinal2023(DataSet dtMain1)
    {

        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\ExpansionD2DSurvey.xlsx");
        var ws = wb.Worksheet(1);
        var ws1 = wb.Worksheet(2);
        var ws2 = wb.Worksheet(3);
        var ws3 = wb.Worksheet(4);
        //var ws1 = wb.Worksheet(2);
        //var ws3 = wb.Worksheet(3);
        DataTable dt = dtMain1.Tables[0];
        DataTable dt1 = dtMain1.Tables[1];
        DataTable dt2 = dtMain1.Tables[2];
        DataTable dt3 = dtMain1.Tables[3];

        //DataTable dt1 = dtMain1.Tables[1];

        dt3.Columns.Remove("RowNO");
        ws.Cell(2, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 2;
        string str = "A2:AV" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        #region Cluster User
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // #Days from Last Survey Done
            int[] arcols = { 10 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToInt32(ws.Cell(x, arcols[y]).Value) >= 3 && Convert.ToInt32(ws.Cell(x, arcols[y]).Value) <= 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToInt32(ws.Cell(x, arcols[y]).Value) > 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToInt32(ws.Cell(x, arcols[y]).Value) < 3)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }

        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // #Idle Villages-Cluster
            int[] arcols = { 15 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToInt32(ws.Cell(x, arcols[y]).Value) > 0)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToInt32(ws.Cell(x, arcols[y]).Value) == 0)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //%Submissions with Audio Consent-Yes
            int[] arcols = { 17 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 60 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // #Villages with Only No Audio Consent
            int[] arcols = { 18 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToInt32(ws.Cell(x, arcols[y]).Value) > 0)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToInt32(ws.Cell(x, arcols[y]).Value) == 0)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% HH Covered in Completed Villages Against Census HH
            int[] arcols = { 19 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 75 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 90)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 75)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 90)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% % OOSG (5-14) Found in Completed Villages Against ML
            int[] arcols = { 21 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 60 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 79)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // # Days with submission is less than 50 and more than 90
            int[] arcols = { 24 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToInt32(ws.Cell(x, arcols[y]).Value) > 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // Avg. HH Surveyed / Day
            int[] arcols = { 25 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToInt32(ws.Cell(x, arcols[y]).Value) >= 50 && Convert.ToInt32(ws.Cell(x, arcols[y]).Value) <= 59)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToInt32(ws.Cell(x, arcols[y]).Value) < 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToInt32(ws.Cell(x, arcols[y]).Value) >= 60 && Convert.ToInt32(ws.Cell(x, arcols[y]).Value) <= 90)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
                else if (Convert.ToInt32(ws.Cell(x, arcols[y]).Value) > 90)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
            }

        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //Eligible HH % (P)
            int[] arcols = { 26 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 61 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 35 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 39)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 35)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // Avg. Time in Per HH (All)
            int[] arcols = { 29 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 3 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) <= 4)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 5 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) < 3)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }

        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% Family with Contact Number
            int[] arcols = { 33 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 74)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 75)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // OOSG Rate (5-14)
            int[] arcols = { 36 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToInt32(ws.Cell(x, arcols[y]).Value) >= 8 && Convert.ToInt32(ws.Cell(x, arcols[y]).Value) <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
                else if (Convert.ToInt32(ws.Cell(x, arcols[y]).Value) >= 11 && Convert.ToInt32(ws.Cell(x, arcols[y]).Value) <= 15)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.White;
                }
                else if (Convert.ToInt32(ws.Cell(x, arcols[y]).Value) >= 5 && Convert.ToInt32(ws.Cell(x, arcols[y]).Value) <= 7)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.White;
                }

                else if (Convert.ToInt32(ws.Cell(x, arcols[y]).Value) > 15)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToInt32(ws.Cell(x, arcols[y]).Value) < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }

        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% 7 To 14 OOSG Against 5-14 OOSG
            int[] arcols = { 37 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 61 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 70)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 70)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //Age Proof% - All Child
            int[] arcols = { 38 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 60 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 79)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //Age Proof% - OOSB & OOSG
            int[] arcols = { 39 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 69)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 70)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //Age Proof% - OOSB & OOSB
            int[] arcols = { 40 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 69)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 70)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        #endregion 

        ws1.Cell(2, 1).InsertData(dt1.Rows);
        Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 2;
        string str1 = "A2:AM" + ii1;
        ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        #region Cluster 

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // #Days from Last Survey Done
            int[] arcols = { 7 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToInt32(ws1.Cell(x, arcols[y]).Value) >= 3 && Convert.ToInt32(ws1.Cell(x, arcols[y]).Value) <= 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToInt32(ws1.Cell(x, arcols[y]).Value) > 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToInt32(ws1.Cell(x, arcols[y]).Value) < 3)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }

        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // #Idle Villages-Cluster
            int[] arcols = { 11 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToInt32(ws1.Cell(x, arcols[y]).Value) > 0)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToInt32(ws1.Cell(x, arcols[y]).Value) == 0)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //%Submissions with Audio Consent-Yes
            int[] arcols = { 13 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 60 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 80)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 60)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 80)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // #Villages with Only No Audio Consent
            int[] arcols = { 14 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToInt32(ws1.Cell(x, arcols[y]).Value) > 0)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToInt32(ws1.Cell(x, arcols[y]).Value) == 0)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% HH Covered in Completed Villages Against Census HH
            int[] arcols = { 15 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 75 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 90)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 75)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 90)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% % OOSG (5-14) Found in Completed Villages Against ML
            int[] arcols = { 17 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 60 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 79)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 60)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 80)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // # Days with submission is less than 50 and more than 90
            int[] arcols = { 20 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToInt32(ws1.Cell(x, arcols[y]).Value) > 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //Eligible HH % (P)
            int[] arcols = { 21 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 61 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 80)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 35 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 39)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 35)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 80)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }


        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% Family with Contact Number
            int[] arcols = { 24 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 74)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 75)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // OOSG Rate (5-14)
            int[] arcols = { 27 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToInt32(ws1.Cell(x, arcols[y]).Value) >= 8 && Convert.ToInt32(ws1.Cell(x, arcols[y]).Value) <= 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
                else if (Convert.ToInt32(ws1.Cell(x, arcols[y]).Value) >= 11 && Convert.ToInt32(ws1.Cell(x, arcols[y]).Value) <= 15)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.White;
                }
                else if (Convert.ToInt32(ws1.Cell(x, arcols[y]).Value) >= 5 && Convert.ToInt32(ws1.Cell(x, arcols[y]).Value) <= 7)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.White;
                }

                else if (Convert.ToInt32(ws1.Cell(x, arcols[y]).Value) > 15)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToInt32(ws1.Cell(x, arcols[y]).Value) < 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }

        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% 7 To 14 OOSG Against 5-14 OOSG
            int[] arcols = { 28 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 40)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 61 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 70)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }

                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 70)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //Age Proof% - All Child
            int[] arcols = { 29 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 60 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 79)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 60)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 80)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //Age Proof% - OOSB & OOSG
            int[] arcols = { 30 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 69)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 70)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //Age Proof% - OOSB & OOSB
            int[] arcols = { 31 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 69)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 70)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        #endregion




        ws2.Cell(2, 1).InsertData(dt2.Rows);
        Int32 ii11 = Convert.ToInt32(dt2.Rows.Count) + 2;
        string str11 = "A2:AI" + ii11;
        ws2.Range(str11).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws2.Range(str11).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws2.Range(str11).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws2.Range(str11).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        #region District 

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // #Days from Last Survey Done
            int[] arcols = { 3 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) >= 3 && Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) <= 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) > 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) < 3)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }

        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // #Idle Villages-Cluster
            int[] arcols = { 11 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) > 0)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) == 0)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //%Submissions with Audio Consent-Yes
            int[] arcols = { 7 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 60 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 80)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 60)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 80)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // #Villages with Only No Audio Consent
            int[] arcols = { 10 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) > 0)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) == 0)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% HH Covered in Completed Villages Against Census HH
            int[] arcols = { 11 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 75 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 90)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 75)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 90)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% % OOSG (5-14) Found in Completed Villages Against ML
            int[] arcols = { 13 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 60 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 79)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 60)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 80)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // # Days with submission is less than 50 and more than 90
            int[] arcols = { 16 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) > 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //Eligible HH % (P)
            int[] arcols = { 17 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 61 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 80)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 35 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 39)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 35)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 80)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }


        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% Family with Contact Number
            int[] arcols = { 20 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 74)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 75)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // OOSG Rate (5-14)
            int[] arcols = { 23 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) >= 8 && Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) <= 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
                else if (Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) >= 11 && Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) <= 15)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.White;
                }
                else if (Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) >= 5 && Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) <= 7)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.White;
                }

                else if (Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) > 15)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToInt32(ws2.Cell(x, arcols[y]).Value) < 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }

        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% 7 To 14 OOSG Against 5-14 OOSG
            int[] arcols = { 24 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 40)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 61 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 70)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }

                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 70)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //Age Proof% - All Child
            int[] arcols = { 25 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 60 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 79)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 60)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 80)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //Age Proof% - OOSB & OOSG
            int[] arcols = { 26 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 69)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 70)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //Age Proof% - OOSB & OOSB
            int[] arcols = { 27 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 69)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 70)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        #endregion

        ws3.Cell(2, 1).InsertData(dt3.Rows);
        Int32 ii113 = Convert.ToInt32(dt3.Rows.Count) + 2;
        string str113 = "A2:AI" + ii113;
        ws3.Range(str113).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws3.Range(str113).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws3.Range(str113).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws3.Range(str113).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        #region AdminDistrict 

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // #Days from Last Survey Done
            int[] arcols = { 3 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToInt32(ws3.Cell(x, arcols[y]).Value) >= 3 && Convert.ToInt32(ws3.Cell(x, arcols[y]).Value) <= 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToInt32(ws3.Cell(x, arcols[y]).Value) > 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToInt32(ws3.Cell(x, arcols[y]).Value) < 3)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }

        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // #Idle Villages-Cluster
            int[] arcols = { 11 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToInt32(ws3.Cell(x, arcols[y]).Value) > 0)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToInt32(ws3.Cell(x, arcols[y]).Value) == 0)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //%Submissions with Audio Consent-Yes
            int[] arcols = { 7 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 60 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 80)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 60)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 80)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // #Villages with Only No Audio Consent
            int[] arcols = { 10 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToInt32(ws3.Cell(x, arcols[y]).Value) > 0)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToInt32(ws3.Cell(x, arcols[y]).Value) == 0)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% HH Covered in Completed Villages Against Census HH
            int[] arcols = { 11 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 75 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 90)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 75)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 90)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% % OOSG (5-14) Found in Completed Villages Against ML
            int[] arcols = { 13 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 60 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 79)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 60)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 80)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // # Days with submission is less than 50 and more than 90
            int[] arcols = { 16 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToInt32(ws3.Cell(x, arcols[y]).Value) > 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //Eligible HH % (P)
            int[] arcols = { 17 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 61 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 80)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 35 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 39)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 35)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 81)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }


        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% Family with Contact Number
            int[] arcols = { 20 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 74)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 75)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // OOSG Rate (5-14)
            int[] arcols = { 23 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToInt32(ws3.Cell(x, arcols[y]).Value) >= 8 && Convert.ToInt32(ws3.Cell(x, arcols[y]).Value) <= 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
                else if (Convert.ToInt32(ws3.Cell(x, arcols[y]).Value) >= 10 && Convert.ToInt32(ws3.Cell(x, arcols[y]).Value) <= 15)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.White;
                }
                else if (Convert.ToInt32(ws3.Cell(x, arcols[y]).Value) >= 5 && Convert.ToInt32(ws3.Cell(x, arcols[y]).Value) <= 7)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.White;
                }

                else if (Convert.ToInt32(ws3.Cell(x, arcols[y]).Value) > 15)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToInt32(ws3.Cell(x, arcols[y]).Value) < 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }

        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% 7 To 14 OOSG Against 5-14 OOSG
            int[] arcols = { 24 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 40)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 61 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 70)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }

                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 70)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //Age Proof% - All Child
            int[] arcols = { 25 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 60 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 79)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 60)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 80)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //Age Proof% - OOSB & OOSG
            int[] arcols = { 26 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 69)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 70)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //Age Proof% - OOSB & OOSB
            int[] arcols = { 27 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 69)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 70)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        #endregion

        filepath = StartupPath + "\\ExpansionD2DSurveyQualityAlert " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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


        };
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptHouseHoldSummaryVillageRaj]", cmdParameters);

        if (dt.Rows.Count > 0)
        {


            ExportToCSVFile(dt, "VillageSummary ");


        }




    }

    public void VillageVillageSubmission(int Flag)
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


        };
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptHouseHoldD2dVillageSubmissionRaj]", cmdParameters);

        if (dt.Rows.Count > 0)
        {


            ExportToCSVFile(dt, "VillageSubmission");


        }




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
        };

        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptSurveyExpansionRaj]", cmdParameters);

        dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Mother Name", "Father Name", "Children guardian Name", "Mobile Number", "Grandfather Name", "Child Name", "Date of Birth", "Samagra ID", "User Name");

        if (dt.Rows.Count > 0)
        {
            ExportToCSVFile(dt, "ChildDetail");
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
        };

        DataTable dt = null;

        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptHouseHoldRaJ]", cmdParameters);

        dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Mohalla", "HH No.", "Mother Name", "Father Name", "Children guardian Name", "Mobile Number", "Grandfather Name", "LandMark", "Latitude", "Longitude");
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
    #endregion
}