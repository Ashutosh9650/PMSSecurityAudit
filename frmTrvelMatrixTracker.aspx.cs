using ClosedXML.Excel;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class frmTrvelMatrixTracker : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    DataTableMaskingHelper dataTableMaskingHelper = new DataTableMaskingHelper();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {




                if (!IsPostBack)
                {


                    if (Convert.ToString(Session["user_level"]) == "124" || Convert.ToString(Session["user_level"]) == "149" || Convert.ToString(Session["user_level"]) == "117" || Convert.ToString(Session["user_level"]) == "SuperAdmin")
                    {
                        lnkR.Visible = true;
                        Li1.Visible = true;
                    }

                    //if (Convert.ToString(Session["username"]) == "PMSAdmin" || Convert.ToString(Session["username"]) == "EGE7557" || Convert.ToString(Session["username"]) == "SuperAdmin")
                    //{
                    //    LinkButton13.Visible = true;
                    //    LinkButton14.Visible = true;

                    //}
                    //else
                    //{
                    //    LinkButton13.Visible = false;
                    //    LinkButton14.Visible = false;

                    //}

                    LoadYear();
                    LoadGroup();
                    //  objComman.BindDLL("mstlookup", "LookupCode,Description1 ", "LookupFlag='G'", "Description1", "Desc", ddlGender, "Description1", "LookupCode", "--All--");
                    LoadUserLeavel();
                    LoadUserLevel();
                    ViewState["1"] = "ss";
                    ViewState["Annual"] = "";
                    ViewState["D2dUser"] = "";

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


    public void LoadUserLevel()
    {
        if (Session["user_level_Role"].ToString() == "4")
        {
            ddlGroup.SelectedValue = "3";
            // ddlGroup.Enabled = false;

        }
        else if (Session["user_level_Role"].ToString() == "3")
        {
            ddlGroup.SelectedValue = "2";
            //  ddlGroup.Enabled = true;

        }
        else
        {
            ddlGroup.SelectedValue = "1";
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
        //string conditions = "";
        //DataRow dr;
        //if (Convert.ToString(Session["user_level"]) == "19")
        //{
        //    DataTable dtYear = CreateDataTable();

        //    //dr = dtYear.NewRow();
        //    //dr["Type"] = "--Select--";
        //    //dr["ID"] = 0;
        //    //dtYear.Rows.Add(dr);

        //    dr = dtYear.NewRow();
        //    dr["Type"] = "Block Wise";
        //    dr["ID"] = 2;
        //    dtYear.Rows.Add(dr);

        //    dr = dtYear.NewRow();
        //    dr["Type"] = "Cluster Wise";
        //    dr["ID"] = 3;
        //    dtYear.Rows.Add(dr);
        //    objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlGroup, "Type", "ID", "Select");


        //}
        //else
        //{
        //    DataTable dtYear = CreateDataTable();

        //    //dr = dtYear.NewRow();
        //    //dr["Type"] = "--Select--";
        //    //dr["ID"] = 0;
        //    //dtYear.Rows.Add(dr);

        //    dr = dtYear.NewRow();
        //    dr["Type"] = "District Wise";
        //    dr["ID"] = 1;
        //    dtYear.Rows.Add(dr);
        //    dr = dtYear.NewRow();
        //    dr["Type"] = "Block Wise";  
        //    dr["ID"] = 2;
        //    dtYear.Rows.Add(dr);

        //    dr = dtYear.NewRow();
        //    dr["Type"] = "Cluster Wise";
        //    dr["ID"] = 3;
        //    dtYear.Rows.Add(dr);
        //    objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlGroup, "Type", "ID", "Select");

        //}
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

            //string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State   order by StateName   ";
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
            AlllStateCode();
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
        LoadData();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBBock();
        LoadData();
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBCluster();
        LoadData();
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillage();
        LoadData();
    }
    protected void ddlGender_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }
    public void LoadData()
    {


        //if (ddlGroup.SelectedIndex <= 0)
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select FC')</script>", false);
        //    return;
        //}
        if (ddlGender.SelectedIndex > 0)
        {

            int mYear = 0;

            if (Convert.ToInt32(ddlGender.SelectedValue) == 1 || Convert.ToInt32(ddlGender.SelectedValue) == 2 || Convert.ToInt32(ddlGender.SelectedValue) == 3)
            {
                mYear = Convert.ToInt32(ddlYear.SelectedValue) + 1;
            }
            else
            {
                mYear = Convert.ToInt32(ddlYear.SelectedValue);
            }
            string ddlCluster = "";
            string ddldist = "";
            string ddlBlock = "";
            string ddlState = "";
            foreach (ListItem item in ChkState.Items)
            {
                if (item.Selected)
                {

                    ddlState += "'" + item.Value + "'" + ",";


                }
            }
            foreach (ListItem item in chkDistrict.Items)
            {
                if (item.Selected)
                {

                    ddldist += "'" + item.Value + "'" + ",";


                }
            }

            foreach (ListItem item in ddlPanchayat.Items)
            {
                if (item.Selected)
                {

                    ddlCluster += "'" + item.Value + "'" + ",";


                }
            }

            foreach (ListItem item in chkBlock.Items)
            {
                if (item.Selected)
                {

                    ddlBlock += "'" + item.Value + "'" + ",";


                }
            }

            string Con = "";
            if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
            {
                if (ddlCluster.Length > 0)
                {
                    ddlCluster = ddlCluster.Substring(0, ddlCluster.LastIndexOf(","));

                    Con += " and mstuser.Villagecode in(" + ddlCluster + ")";
                }
                //if (ddlCluster.Length > 0)
                //{
                //    ddlCluster = ddlCluster.Substring(0, ddlCluster.LastIndexOf(","));

                //    Con = " and mstuser.Villagecode in(" + ddlCluster + ")";
                //}

                if (ddldist.Length > 0)
                {
                    ddldist = ddldist.Substring(0, ddldist.LastIndexOf(","));

                    Con += " and mstuser.districtcode in(" + ddldist + ")";
                }
                if (ddlState.Length > 0)
                {
                    ddlState = ddlState.Substring(0, ddlState.LastIndexOf(","));

                    Con += " and mstuser.Statecode in(" + ddlState + ")";
                }
                if (ddlBlock.Length > 0)
                {
                    ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));

                    Con += " and mstuser.BlockCOde in(" + ddlBlock + ")";
                }
            }
            else
            {
                if (ddlCluster.Length > 0)
                {
                    ddlCluster = ddlCluster.Substring(0, ddlCluster.LastIndexOf(","));

                    Con += " and mstuser2026.Villagecode in(" + ddlCluster + ")";
                }
                //if (ddlCluster.Length > 0)
                //{
                //    ddlCluster = ddlCluster.Substring(0, ddlCluster.LastIndexOf(","));

                //    Con = " and mstuser.Villagecode in(" + ddlCluster + ")";
                //}

                if (ddldist.Length > 0)
                {
                    ddldist = ddldist.Substring(0, ddldist.LastIndexOf(","));

                    Con += " and mstuser2026.districtcode in(" + ddldist + ")";
                }
                if (ddlState.Length > 0)
                {
                    ddlState = ddlState.Substring(0, ddlState.LastIndexOf(","));

                    Con += " and mstuser2026.Statecode in(" + ddlState + ")";
                }
                if (ddlBlock.Length > 0)
                {
                    ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));

                    Con += " and mstuser2026.BlockCOde in(" + ddlBlock + ")";
                }
            }

            SqlParameter[] parm1 = new SqlParameter[]
      {
             new SqlParameter("@Con", Con),
             new SqlParameter("@month", ddlGender.SelectedValue),
              new SqlParameter("@Year",mYear),
                   new SqlParameter("@user_level", Convert.ToString(Session["user_level"])),

      };
            DataTable dt = null;
            if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
            {

                dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptTravelWeelllyReportMain", parm1);
            }
            else

            {

                dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptTravelWeelllyReportMain2025", parm1);
            }

            BindDLLMasterTable("mstSchool", "FromNo,FromNo", dt, "", "Type", "asc", ddlGroup, "FromNo", "FromNo", "Select");

            Session["RFNo"] = dt;
        }


    }
    public bool BindDLLMasterTable(string dtname, string fieldname, DataTable dt, string Condition, string orberbyfield, string orderby, DropDownList ddl, string textData, string valData, string ZeroIndex)
    {
        bool status = false;
        string conditions = Condition == "" ? "" : " where " + Condition;
        string orberbyfields = orberbyfield == "" ? "" : " order by " + orberbyfield;
        string orderbys = orderby == "" ? "" : orderby;


        //string strQry = "Select  distinct " + fieldname + " from " + dtname + " " + conditions + " " + orberbyfields + " " + orderbys + "";
        //DataTable dt = dbt.VGridFill(strQry);
        if (ZeroIndex != "")
        {
            DataRow dr;
            dr = dt.NewRow();
            dr[textData] = "--" + ZeroIndex + "--";
            dr[valData] = "--" + ZeroIndex + "--";
            dt.Rows.InsertAt(dr, 0);
            dt.AcceptChanges();
        }
        if (dt.Rows.Count > 0)
        {
            ddl.DataTextField = textData;
            ddl.DataValueField = valData;

            ddl.DataSource = dt;
            ddl.DataBind();
            status = true;
        }
        return status;

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

    protected void LnkCHild_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 8;
        if (ddlGroup.SelectedIndex > 0)
        {

            LoadSchoolSummaryData(Convert.ToInt32(ddlTpye.SelectedValue));
            GVChild.Visible = false;
            GV_DynamicGrid.Visible = true;
            GVChildTarget.Visible = false;
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Plan Type ')</script>", false);
        }


    }
    protected void LnkChildSummary_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 8;
        if (ddlGender.SelectedIndex > 0)
        {

            LoadChildSummaryData(Convert.ToInt32(ddlTpye.SelectedValue));
            GVChildTarget.Visible = false;
            GVChild.Visible = true;
            GV_DynamicGrid.Visible = false;
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Plan Month ')</script>", false);
        }




    }
    protected void LnkDeatild_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 98;

        LoadChildSummaryDataTarget(2);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;





    }

    protected void LnkDeatild1_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 98;
        LoadChildSummaryDataTarget(4);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;





    }

    protected void LnkDeatildddd_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 988;
        if (ddlGroup.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Form No')</script>", false);
            return;
        }
        GeneraatePDFMain();
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;


    }
    protected string GeneraatePDFMain()
    {
        string sb = "";
        string UserName = "";
        try
        {
            string Fdate = "";
            string Tdate = "";
            int mMonth = 0;
            if (ddlGender.SelectedValue == "1")
            {
                mMonth = 12;
            }
            else
            {
                mMonth = Convert.ToInt32(ddlGender.SelectedValue) - 1;
            }
            if (ddlGender.SelectedValue == "2" || ddlGender.SelectedValue == "3")
            {
                Fdate = DateTime.Now.Year + 1 + "-" + mMonth + "-" + "21";
                Tdate = DateTime.Now.Year + 1 + "-" + ddlGender.SelectedValue + "-" + "20";
            }
            else if (ddlGender.SelectedValue == "4")
            {
                Fdate = DateTime.Now.Year + "-" + mMonth + "-" + "01";
                Tdate = DateTime.Now.Year + "-" + ddlGender.SelectedValue + "-" + "20";
            }
            else if (ddlGender.SelectedValue == "1")
            {
                Fdate = DateTime.Now.Year + "-" + mMonth + "-" + "21";
                Tdate = DateTime.Now.Year + 1 + "-" + ddlGender.SelectedValue + "-" + "20";
            }
            else
            {
                Fdate = DateTime.Now.Year + "-" + mMonth + "-" + "21";
                Tdate = DateTime.Now.Year + "-" + ddlGender.SelectedValue + "-" + "20";
            }

            int mYear = 0;

            if (Convert.ToInt32(ddlGender.SelectedValue) == 1 || Convert.ToInt32(ddlGender.SelectedValue) == 2 || Convert.ToInt32(ddlGender.SelectedValue) == 3)
            {
                mYear = Convert.ToInt32(ddlYear.SelectedValue) + 1;
            }
            else
            {
                mYear = Convert.ToInt32(ddlYear.SelectedValue);
            }
            DataTable dt = Session["RFNo"] as DataTable;
            DataRow[] dr = dt.Select("FromNo='" + ddlGroup.SelectedItem.Text + "'");
            string fno = dr[0]["FromNo1"].ToString();
            UserName = dr[0]["UserID"].ToString();
            string empname = "", empcode = "", designation = "", district = "", Block = "", cluster = "", depatment = "", Reporting = "";
            //  DataTable dtemployee = objMain.Select_All_Data("MstUser inner join tblTravelMatrixDeatils2024 on  MstUser.UserName=tblTravelMatrixDeatils2024.UserID inner join  Mstuserrole on  MstUser.UserLevel= Mstuserrole.Role_Level  inner join mst2District on mst2District.districtcode=MstUser.DistrictCode inner join mst3Block on mst3Block.blockcode=MstUser.blockcode inner join mst5Village on mst5Village.villagecode=MstUser.villagecode    inner join MstUser u on u.blockcode=MstUser.blockcode and u.UserLevel=19 and U.ActiveStatus=1", "distinct mstuser.FristName as name,mstuser.UserName as code,Mstuserrole.Role as desg,'' Department ,mst2District.districtname,BlockName,VillageName as cluster,U.userName +'-'+ u.FristName  as [Reporting Manager]", "MstUser.UserName='" + UserName + "' and mYear=" + mYear + " and mMonth=" + Convert.ToInt32(ddlGender.SelectedValue) + "", "", "");
            DataTable dtemployee = null;
            if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
            {
                SqlParameter[] parm2 = new SqlParameter[]
                {
                    new SqlParameter("@UserName", UserName),
                    new SqlParameter("@mMonth",  ddlGender.SelectedValue),
                    new SqlParameter("@mYear",mYear),
                };

                dtemployee = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadEMpDetailsTravel", parm2);

                dataTableMaskingHelper.DecryptAndMaskDataTable(dtemployee, "FristName", "Reporting Manager");
            }
            else
            {
                SqlParameter[] parm2 = new SqlParameter[]
                {
                     new SqlParameter("@UserName", UserName),
                     new SqlParameter("@mMonth",  ddlGender.SelectedValue),
                     new SqlParameter("@mYear",mYear),
                };

                dtemployee = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadEMpDetailsTravel2025", parm2);
            }


            if (dtemployee.Rows.Count > 0)
            {

                empname = dtemployee.Rows[0]["name"].ToString();
                empcode = dtemployee.Rows[0]["code"].ToString();
                designation = dtemployee.Rows[0]["desg"].ToString();
                district = dtemployee.Rows[0]["districtname"].ToString();
                Block = dtemployee.Rows[0]["BlockName"].ToString();
                cluster = dtemployee.Rows[0]["cluster"].ToString();
                depatment = dtemployee.Rows[0]["Department"].ToString();
                Reporting = dtemployee.Rows[0]["Reporting Manager"].ToString();

            }


            string imageURLLogo = Server.MapPath(".") + "/images/logo-new1.png";
            sb += @"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">";
            sb += "<html>";
            sb += "<body>";
            // sb += "<table width='100%' cellspacing='0' cellpadding='2'>";
            //Session["FromNo"] = "nov_1";
            DataSet dstravle = null;
            if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
            {
                SqlParameter[] parm1 = new SqlParameter[]
       {

                         new SqlParameter("@UserName",UserName),
                         new SqlParameter("@month", ddlGender.SelectedValue),
                          new SqlParameter("@Myear",mYear),
                             new SqlParameter("@UserRole",Convert.ToString(Session["user_level_Role"])),
                     new SqlParameter("@FromNo",fno),



       };


                dstravle = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptTravelDeatil2024View", parm1);
            }
            else
            {
                SqlParameter[] parm1 = new SqlParameter[]
            {

                                 new SqlParameter("@UserName",UserName),
                                 new SqlParameter("@month", ddlGender.SelectedValue),
                                  new SqlParameter("@Myear",mYear),
                                     new SqlParameter("@UserRole",Convert.ToString(Session["user_level_Role"])),
                             new SqlParameter("@FromNo",fno),



            };


                dstravle = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptTravelDeatil2024View2025", parm1);

            }
            DataTable dttravelmatrixdetails = dstravle.Tables[0];
            DataTable dttraveDate = dstravle.Tables[4];
            DataTable dttravex = dstravle.Tables[1];
            DataTable dttravexIMg = dstravle.Tables[2];

            /// DataTable dttravelmatrixdetails = objMain.Select_All_Data("tblTravelMatrixDeatils2024", "convert(varchar,TravelDate,103) as Fromdate,convert(varchar,TravelDate,103) as Todate,LoginTime as TimeIn,logouttime as Timeout, [FromVillagename] as [FromVillagename],[ToVillagename] ,isnull(RevisedFare,0) as LC,isnull(RevisedDAAdmin,0) as DA", "userid='" + ddlFC.SelectedValue + "' and mYear='" + ddlYear.SelectedValue + "' and deleteflag=1  ", "TravelDate", "ASC";
            int Acount = dttravelmatrixdetails.Rows.Count;
            int MainCount = 0;
            int icount = 0;
            if (Acount > 12)
            {
                MainCount = 12;

            }
            else
            {
                MainCount = Acount;

            }
            int tot = 0;
            int DA = 0;
            //if (pageindex <= 15)
            //{


            //sb += "<tr style='font-size:20px;'>";
            //sb += "<td style='font-size:20px;text-align:center'>";


            empname = dtemployee.Rows[0]["name"].ToString();
            empcode = dtemployee.Rows[0]["code"].ToString();
            designation = dtemployee.Rows[0]["desg"].ToString();
            district = dtemployee.Rows[0]["districtname"].ToString();
            Block = dtemployee.Rows[0]["BlockName"].ToString();
            cluster = dtemployee.Rows[0]["cluster"].ToString();
            depatment = dtemployee.Rows[0]["Department"].ToString();
            Reporting = dtemployee.Rows[0]["Reporting Manager"].ToString();

            DataTable sqldtTourPlan = new DataTable();
            if (Acount > 12)
            {
                sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; font-family:Calibri (Body)' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                sb += " <tr style='background: #fff2cc;'>";
                sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px;font-family:Calibri (Body)'> Employee Name: <b>" + empname + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> Employee Code:<b>" + empcode + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> Designation:<b>Field Coordinator</b>  ";
                sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager: <b>" + Reporting + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> District: <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004<b></b>";
                sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                sb += " < td>Form No: <b>" + fno + " </b></td> </tr> </tbody> </table> </td> </tr>";
                sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                              "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                              "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                              " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                              "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                              " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                          " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                           "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                           "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                           " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                           "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                             " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                              " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                              " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                              "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                                "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                              " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                              "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                              "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                              "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";


                for (int i = 0; i < MainCount; i++)
                {

                    sb += "<tr  style='border: 0'>";
                    sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                    sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                    sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                    sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                    sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                    sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                    sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                    sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                    sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                    //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                    sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                    sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                    sb += "</tr>";

                    tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                    //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];
                }
                if (Acount > 12)
                {
                    sb += " </tbody> </table>";
                }

                if (Acount > 12)
                {
                    int Ig = 0;
                    if (Acount > 24)
                    {
                        Ig = 24;
                    }
                    else
                    {
                        Ig = Acount - 12;
                        Ig = 12 + Ig;
                    }

                    sb += "<div style='page-break-before : always;'> </div>";
                    sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                    sb += " <tr style='background: #fff2cc;'>";
                    sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                    sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Employee Code: <b>" + empcode + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                    sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager: <b>" + Reporting + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> District: <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                    sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                    sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                    sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                    sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                    sb += " < td>Form No: <b>" + fno + " </b></td> </tr> </tbody> </table> </td> </tr>";
                    sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                                  "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                                  "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                                  " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                                  "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                                  " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                              " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                               "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                               "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                               " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                               "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                                 " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                                  " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                                  " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                                  "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                                    "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                                  " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                                  "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                                  "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                                  "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";



                    for (int i = 12; i < Ig; i++)
                    {


                        sb += "<tr  style='border: 0'>";
                        sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                        sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                        sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                        sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                        sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                        sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                        //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                        sb += "</tr>";
                        tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);


                        //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];



                    }

                }
                if (Acount > 24)
                {
                    sb += " </tbody> </table>";
                }
                if (Acount > 24)
                {
                    int Ig = 0;

                    if (Acount > 36)
                    {
                        Ig = 36;
                    }
                    else
                    {
                        Ig = Acount - 24;
                        Ig = 24 + Ig;
                    }


                    sb += "<div style='page-break-before : always;'> </div>";
                    sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                    sb += " <tr style='background: #fff2cc;'>";
                    sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                    sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Employee Code : <b>" + empcode + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                    sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager : <b>" + Reporting + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> District : <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                    sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                    sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                    sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                    sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                    sb += " < td>Form No: <b>" + fno + " </b></td> </tr> </tbody> </table> </td> </tr>";
                    sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                          "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                          " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                          " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                      " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                       "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                       "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                       " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                       "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                         " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                            "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                          " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                          "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";

                    for (int i = 24; i < Ig; i++)
                    {

                        sb += "<tr  style='border: 0'>";
                        sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                        sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                        sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                        sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                        sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                        sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                        //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                        sb += "</tr>";
                        tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];



                    }

                }

                if (Acount > 36)
                {
                    sb += " </tbody> </table>";
                }
                if (Acount > 36)
                {
                    int Ig = 0;

                    if (Acount > 48)
                    {
                        Ig = 48;
                    }
                    else
                    {
                        Ig = Acount - 48;
                        Ig = 48 + Ig;
                    }


                    sb += "<div style='page-break-before : always;'> </div>";
                    sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                    sb += " <tr style='background: #fff2cc;'>";
                    sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                    sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Employee Code : <b>" + empcode + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                    sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager : <b>" + Reporting + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> District : <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                    sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                    sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                    sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                    sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                    sb += " < td>Form No: <b>" + fno + " </b></td> </tr> </tbody> </table> </td> </tr>";
                    sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                          "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                          " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                          " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                      " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                       "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                       "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                       " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                       "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                         " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                            "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                          " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                          "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";

                    for (int i = 36; i < Ig; i++)
                    {

                        sb += "<tr  style='border: 0'>";
                        sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                        sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                        sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                        sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                        sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                        sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                        //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                        sb += "</tr>";
                        tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];



                    }

                }

                if (Acount > 48)
                {
                    sb += " </tbody> </table>";
                }

                if (Acount > 48)
                {
                    int Ig = 0;

                    if (Acount > 60)
                    {
                        Ig = 60;
                    }
                    else
                    {
                        Ig = Acount - 60;
                        Ig = 60 + Ig;
                    }


                    sb += "<div style='page-break-before : always;'> </div>";
                    sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                    sb += " <tr style='background: #fff2cc;'>";
                    sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                    sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Employee Code : <b>" + empcode + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                    sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager : <b>" + Reporting + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> District : <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                    sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                    sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                    sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                    sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                    sb += " < td>Form No: <b>" + fno + " </b></td> </tr> </tbody> </table> </td> </tr>";
                    sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                          "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                          " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                          " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                      " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                       "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                       "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                       " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                       "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                         " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                            "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                          " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                          "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";

                    for (int i = 48; i < Ig; i++)
                    {

                        sb += "<tr  style='border: 0'>";
                        sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                        sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                        sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                        sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                        sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                        sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                        //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                        sb += "</tr>";
                        tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];



                    }

                }



                if (Acount > 60)
                {
                    sb += " </tbody> </table>";
                }

                if (Acount > 60)
                {
                    int Ig = 0;

                    if (Acount > 72)
                    {
                        Ig = 72;
                    }
                    else
                    {
                        Ig = Acount - 72;
                        Ig = 72 + Ig;
                    }


                    sb += "<div style='page-break-before : always;'> </div>";
                    sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                    sb += " <tr style='background: #fff2cc;'>";
                    sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                    sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Employee Code : <b>" + empcode + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                    sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager : <b>" + Reporting + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> District : <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                    sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                    sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                    sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                    sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                    sb += " < td>Form No: <b>" + fno + " </b></td> </tr> </tbody> </table> </td> </tr>";
                    sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                          "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                          " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                          " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                      " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                       "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                       "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                       " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                       "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                         " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                            "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                          " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                          "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";

                    for (int i = 60; i < Ig; i++)
                    {

                        sb += "<tr  style='border: 0'>";
                        sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                        sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                        sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                        sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                        sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                        sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                        //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                        sb += "</tr>";
                        tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];



                    }

                }

                if (Acount > 72)
                {
                    sb += " </tbody> </table>";
                }

                if (Acount > 72)
                {
                    int Ig = 0;

                    if (Acount > 84)
                    {
                        Ig = 84;
                    }
                    else
                    {
                        Ig = Acount - 84;
                        Ig = 84 + Ig;
                    }


                    sb += "<div style='page-break-before : always;'> </div>";
                    sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                    sb += " <tr style='background: #fff2cc;'>";
                    sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                    sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Employee Code : <b>" + empcode + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                    sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager : <b>" + Reporting + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> District : <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                    sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                    sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                    sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                    sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                    sb += " < td>Form No: <b>" + fno + " </b></td> </tr> </tbody> </table> </td> </tr>";
                    sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                          "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                          " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                          " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                      " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                       "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                       "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                       " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                       "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                         " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                            "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                          " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                          "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";

                    for (int i = 72; i < Ig; i++)
                    {

                        sb += "<tr  style='border: 0'>";
                        sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                        sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                        sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                        sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                        sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                        sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                        //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                        sb += "</tr>";
                        tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];



                    }

                }

                if (Acount > 84)
                {
                    sb += " </tbody> </table>";
                }

                if (Acount > 84)
                {
                    int Ig = 0;

                    if (Acount > 96)
                    {
                        Ig = 96;
                    }
                    else
                    {
                        Ig = Acount - 96;
                        Ig = 96 + Ig;
                    }


                    sb += "<div style='page-break-before : always;'> </div>";
                    sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                    sb += " <tr style='background: #fff2cc;'>";
                    sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                    sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Employee Code : <b>" + empcode + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                    sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager : <b>" + Reporting + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> District : <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                    sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                    sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                    sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                    sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                    sb += " < td>Form No: <b>" + fno + " </b></td> </tr> </tbody> </table> </td> </tr>";
                    sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                          "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                          " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                          " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                      " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                       "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                       "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                       " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                       "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                         " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                            "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                          " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                          "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";

                    for (int i = 84; i < Ig; i++)
                    {

                        sb += "<tr  style='border: 0'>";
                        sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                        sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                        sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                        sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                        sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                        sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                        //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                        sb += "</tr>";
                        tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];



                    }

                }

                if (Acount > 96)
                {
                    sb += " </tbody> </table>";
                }

                if (Acount > 96)
                {
                    int Ig = 0;

                    if (Acount > 108)
                    {
                        Ig = 108;
                    }
                    else
                    {
                        Ig = Acount - 108;
                        Ig = 108 + Ig;
                    }


                    sb += "<div style='page-break-before : always;'> </div>";
                    sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                    sb += " <tr style='background: #fff2cc;'>";
                    sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                    sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Employee Code : <b>" + empcode + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                    sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager : <b>" + Reporting + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> District : <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                    sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                    sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                    sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                    sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                    sb += " < td>Form No: <b>" + fno + " </b></td> </tr> </tbody> </table> </td> </tr>";
                    sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                          "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                          " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                          " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                      " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                       "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                       "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                       " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                       "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                         " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                            "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                          " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                          "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";

                    for (int i = 96; i < Ig; i++)
                    {

                        sb += "<tr  style='border: 0'>";
                        sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                        sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                        sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                        sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                        sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                        sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                        //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                        sb += "</tr>";
                        tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];



                    }

                }

                sb += "<tr style='border: 0'> <td colspan='18' style=' border-bottom: 1px solid #000000; text-align: right; padding-right: 15px; padding: 9px; font-weight: 900; font-size: 14px; border-left:1px solid #000; ' > TOTAL REIMBURSEMENT </td> <td style='border-bottom: 1px solid #000000; text-align:right ;border-right:1px solid #000;'>" + tot + "</td> </tr> </tbody> </table> ";

            }
            else
            {
                sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                sb += " <tr style='background: #fff2cc;'>";
                sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> Employee Code: <b>" + empcode + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager: <b>" + Reporting + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> District: <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                sb += " < td>Form No: <b>" + fno + " </b></td> </tr> </tbody> </table> </td> </tr>";
                sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                   "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                   "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                   " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                   "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                   " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
               " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                  " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                   " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                   " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                   "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                     "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                   " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                   "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                   "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                   "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";


                for (int i = 0; i < dttravelmatrixdetails.Rows.Count; i++)
                {

                    sb += "<tr  style='border: 0'>";
                    sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                    sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                    sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                    sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                    sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                    sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                    sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                    sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                    sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                    //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                    sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                    sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                    sb += "</tr>";

                    tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                    //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];
                }


                sb += "<tr style='border: 0'> <td colspan='18' style=' border-bottom: 1px solid #000000; text-align: right; padding-right: 15px; padding: 9px; font-weight: 900; font-size: 14px; border-left:1px solid #000; ' > TOTAL REIMBURSEMENT </td> <td style='border-bottom: 1px solid #000000; text-align:right ;border-right:1px solid #000;'>" + tot + "</td> </tr> </tbody> </table> ";

            }

            sb += "<div style='page-break-before : always;'> </div>";
            sb += "<table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody> <tr> <td colspan='1' style='border:1px solid #000; border-bottom:0; border-right:0; '></td> <td colspan='4' style=' font-size: 26px; text-align: center; font-weight: 900; padding: 15px; border:0; border-top:1px solid #000;' > Foundation to Educate Girls Globally </td> <td colspan='1' style=' text-align: right; border:1px solid #000; border-left: 0; border-bottom:0; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt='' /> </td> </tr>";

            sb += "<tr style='background: #fff2cc; border: 0'> <td colspan='6' style='padding: 15px; border:1px solid #000;'>";
            sb += " <table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' >";
            sb += " <tbody> <tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td> ";
            sb += "<td style='padding-bottom: 15px'> Employee Code: <b>" + empcode + "</b> </td> ";
            sb += "<td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b> </td> ";
            sb += "<td style='padding-bottom: 15px'> Reporting Manager: <b>" + Reporting + "</b> </td> ";
            sb += "<td style='padding-bottom: 15px'> District: <b>" + district + "</b> </td> </tr> </tbody> </table> ";
            sb += "<table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody> <tr style='font-size: 11px'> ";
            sb += "<td style='font-size: 11px'>Block: <b>" + Block + "</b></td> <td>Cluster: <b>" + cluster + " </b></td>";
            sb += " <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b></td> ";
            sb += "<td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
            sb += " <td>Form No: <b>" + fno + "</b></td> </tr> </tbody> </table> </td> </tr>";
            sb += " <tr> <th colspan='6' style=' font-weight: 900; font-size: 18px; padding: 9px; text-align: center; border:1px solid #000; ' > Other Expens </th> </tr>";
            sb += " <tr style='font-size: 10px; 'background: #ececec;'>  ";
            sb += "<th  width='9%' style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;  padding-top:15px; padding-bottom:15px;'>Date</th><th width='10%'  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;  padding-top:15px; padding-bottom:15px;'>Local Travel in KM</th> <th  width='30%' style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;  padding-top:15px; padding-bottom:15px;'>Description</th>";
            sb += " <th  width='10%' style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;  padding-top:15px; padding-bottom:15px;'>Conveyance</th> <th  width='10%' style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;  padding-top:15px; padding-bottom:15px;'>Others</th> ";
            sb += "<th  width='15%' style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;border-right:1px solid #000;  padding-top:15px; padding-bottom:15px;'>Remark</th> </tr> ";

            if (dttravex.Rows.Count > 0)
            {


                for (int i = 0; i < dttravex.Rows.Count; i++)
                {

                    sb += "<tr style='font-size:11px'>";

                    sb += "<td width='14%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravex.Rows[i]["Date"] + "</td>";

                    sb += "<td width='15%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;text-align:right;'>" + dttravex.Rows[i]["KM"] + "</td>";
                    sb += "<td width='20%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravex.Rows[i]["Desc"] + "</td>";
                    sb += "<td width='10%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;text-align:right;'>" + dttravex.Rows[i]["Conveyance"] + "</td>";
                    sb += "<td width='10%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;text-align:right;'>" + dttravex.Rows[i]["Other"] + "</td>";
                    //sb+="<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["DA"] + "</td>";
                    sb += "<td width='15%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000; border-right:1px solid #000;'>" + dttravex.Rows[i]["Remark"] + "</td>";
                    //sb+="<td width='14%' valign='top'></td>";


                    sb += "</tr>";


                }


            }
            else
            {
                sb += "<tr style='font-size:11px'>";

                sb += "<td width='14%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;'></td>";

                sb += "<td width='15%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;'></td>";
                sb += "<td width='20%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;'></td>";
                sb += "<td width='10%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;'></td>";
                sb += "<td width='10%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;'></td>";
                //sb+="<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["DA"] + "</td>";
                sb += "<td width='15%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000; border-right:1px solid #000;'></td>";
                //sb+="<td width='14%' valign='top'></td>";


                sb += "</tr>";

            }
            DataTable dttravelApprove = dstravle.Tables[3];
            sb += "</tbody> </table>";
            sb += "< table style='width: 100%; border-spacing: 0; border-collapse: 0; margin-bottom: 15px; border: 0; font-size: 11px; ' border='1' > ";

            sb += "<tr>";
            //sb += "<tr> <td>01/11/2024</td> <td>01/11/2024</td> <td>01/11/2024</td> <td>01/11/2024</td> ";
            //sb += "<td>01/11/2024</td> <td>01/11/2024</td> </tr> <tr>";
            sb += " <td colspan='6' style=' text-align: center; background: #ececec; font-weight: 900; font-size: 18px; padding: 9px; border:0 ; border-bottom:1px solid #000;' > Approval Status </td> </tr> ";
            sb += "<tr> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;' >Submission: " + dttravelApprove.Rows[0]["SubmittedStatus"].ToString() + " </td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Submitted By: " + dttravelApprove.Rows[0]["SubmittedBy"].ToString() + "  </td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Submitted Date: " + dttravelApprove.Rows[0]["SubmittedDate"].ToString() + "</td> </tr> ";
            sb += "<tr> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>BO Approval: " + dttravelApprove.Rows[0]["BOApprovalStatus"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Approved By: " + dttravelApprove.Rows[0]["BOApprovalBy"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Approval Date: " + dttravelApprove.Rows[0]["BOApprovalDate"].ToString() + "</td> </tr> ";
            sb += "<tr> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Admin Verification: " + dttravelApprove.Rows[0]["AdminApprovalStatus"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Verified By: " + dttravelApprove.Rows[0]["AdminApprovalBy"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Verified Date: " + dttravelApprove.Rows[0]["AdminApprovalDate"].ToString() + "</td> </tr> ";
            sb += "<tr> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>HR Verification: " + dttravelApprove.Rows[0]["HRApprovalStatus"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Verified By: " + dttravelApprove.Rows[0]["HRApprovalBy"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Verified Date: " + dttravelApprove.Rows[0]["HRApprovalDate"].ToString() + "</td> </tr> ";
            sb += "<tr> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>DOL Approval: " + dttravelApprove.Rows[0]["DOLApprovalStatus"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Approved By: " + dttravelApprove.Rows[0]["DOLApprovalBy"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Approval Date: " + dttravelApprove.Rows[0]["DOLApprovalDate"].ToString() + "</td> </tr> ";
            sb += "<tr> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Payment Status: " + dttravelApprove.Rows[0]["FinanceApprovalStatus"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Payment Processed by: " + dttravelApprove.Rows[0]["FinanceApprovalBy"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Payment Process Date:" + dttravelApprove.Rows[0]["FinanceApprovalDate"].ToString() + "</td> </tr> ";

            sb += " </table>";

            if (dttravexIMg.Rows.Count > 0)
            {
                sb += "<div style='page-break-before : always;'> </div>";

                sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                sb += " <tr style='background: #fff2cc;'>";
                sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> Employee Code: <b>" + empcode + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager: <b>" + Reporting + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> District: <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                sb += " < td>Form No: <b>" + fno + " </b></td> </tr> </tbody> </table></td> </tr>";


                sb += "</tbody> </table>";

                sb += "<table border=1 width='100%' cellspacing='2' cellpadding='2' style='font-size:10px;page-break-after: always; border-color:#dddddd;font-weight:normal'> ";
                int kcount = 0;
                for (int i = 0; i < dttravexIMg.Rows.Count; i++)
                {
                    string Imh = dttravexIMg.Rows[i]["ImagePath"].ToString();
                    string imageURLLogo1 = Server.MapPath(".") + "/Travel/" + Imh;
                    if (System.IO.File.Exists(imageURLLogo1))
                    {
                        kcount = kcount + 1;
                        sb += "<tr>";

                        sb += "<td valign='top'><img      src='" + imageURLLogo1 + "'  height='600px' width='960px'  alt='Bird' /></td>";
                        //sb+="<td width='14%' valign='top'>dfgfdg</td>";

                        sb += "</tr>";
                    }
                }
                if (kcount == 0)
                {
                    sb += "<tr>";

                    sb += "<td valign='top'></td>";

                    sb += "</tr>";
                }
                sb += "</table>";
            }
            //sb += @"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">";
            //sb += "<html>";
            //sb += "<body>";
            // sb += "<table width='100%' cellspacing='0' cellpadding='2'>";
            StringReader sr = new StringReader(sb.ToString());
            iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 10, 10, 10, 10);

            // Document pdfDoc = new Document(PageSize.A4, 36, 36, 36, 72;
            iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);

            string FC = UserName;
            //var cssText = File.ReadAllText(MapPath("~/StyleSheet.css");


            using (MemoryStream memoryStream = new MemoryStream())
            {
                iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memoryStream);

                pdfDoc.Open();
                pdfDoc.NewPage();

                using (TextReader reader = new StringReader(sb))
                {
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, reader);
                }

                pdfDoc.Close();
                byte[] bytes = memoryStream.ToArray();


                memoryStream.Close();

                File.WriteAllBytes(Request.PhysicalApplicationPath + "/Travel vouchers/TravelVoucher_" + ddlGender.SelectedItem.Text + "_" + FC.Substring(0, 7) + DateTime.Now.ToString("ddMMyyyy_hhmmss") + ".pdf", bytes);
            }



            string filename = "Travel vouchers" + "_" + ddlGender.SelectedItem.Text + "_" + FC.Substring(0, 7) + DateTime.Now.ToString("ddMMyyyy_hhmmss") + ".pdf";
            //  string dsssssssssssss = Request.PhysicalApplicationPath + "Travel vouchers\\TravelVoucher_" + ddlMonth.SelectedItem.Text + "_ " + ddlFc.SelectedItem.Text + ".pdf";
            System.Net.WebClient req = new System.Net.WebClient();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            string dsssssssssssss1 = Server.MapPath(Comman.GetImagePath("TravelvouchersPath") + "/TravelVoucher_" + ddlGender.SelectedItem.Text + "_" + FC.Substring(0, 7) + DateTime.Now.ToString("ddMMyyyy_hhmmss") + ".pdf");
            byte[] data = req.DownloadData(dsssssssssssss1);
            response.BinaryWrite(data);
            //   Response.TransmitFile(Server.MapPath(Comman.GetImagePath("TravelvouchersPath") + "/"" + filename);
            response.End();




            //string filename ="TravelVoucher"+"_" +ddlFc.SelectedValue +".pdf";
            //FileInfo file = new FileInfo((Server.MapPath(Comman.GetImagePath("TravelvouchersPath") + "/"" + filename));
            //if (file.Exists)
            //{

            //    Response.ContentType = "application/octet-stream";
            //    Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename;
            //    string aaa = Server.MapPath(Comman.GetImagePath("TravelvouchersPath") + "/"" + filename;
            //    Response.TransmitFile(Server.MapPath(Comman.GetImagePath("TravelvouchersPath") + "/"" + filename);


            //}


            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('BTOR Not Available.')</script>", false;

            //}


        }
        catch (System.Exception ex)
        {

            //   Response.Clear(;

            //string mmsg = ex.Message;
            //showEXPMessages("(crateZip)  " + mmsg; //showMessages(mmsg;
        }
        finally
        {

            //Response.Clear(;

        }

        return sb.ToString();

    }
    protected void LnkDeatildddd2_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 988;

        LoadChildSummaryDataTarget(5);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;


    }
    protected void LnkReject_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 988;

        LoadChildSummaryDataTarget(6);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;


    }
    protected void LnkFar_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 988;

        rpttravelMatrix(6);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;


    }

    protected void LnkFillingSystem_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 99;

        LoadChildSummaryDataTarget(3);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;





    }
    protected void LnkEX_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 100;

        LoadExceptionReport(1);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;





    }
    protected void LnkEnrolmentcv(object sender, EventArgs e)
    {
        ViewState["1"] = 1010;

        LoadEnrolmentcv(1);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;





    }
    public void LoadEnrolmentcv(int Flag)
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

        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2024)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptEnrollCVReportAprove2024", cmdParameters);
        }
        else
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptEnrollCVReportAprove", cmdParameters);
        }
        if (dt.Rows.Count > 0)
        {
            ExportToCSVFile(dt, "CourseCorrectionDetail");
        }




    }


    protected void Lnkpf_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 500;

        LoadPlanReport(1);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;





    }

    protected void Lnkpfkj_OnddClick(object sender, EventArgs e)
    {
        ViewState["1"] = 500;

        LoadPlanReportProcess(1);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;





    }
    protected void Lnkpfkj_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 500;

        LoadPlanReportTrakerNew2022(1);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;





    }

    protected void Lnkpfkj33_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 500;

        LoadPlanReportTrakerNCluster(1);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;





    }
    protected void Lnkpfkj334_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 500;

        //  LoadrptLocalVillagaePerformace(1);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;





    }
    protected void LnkpfkjTest_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 500;

        LoadPlanReportTrakerNew2022(1);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;





    }

    public void LoadChildSummaryDataTarget(int Flag)
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


        //if (ddlStatecode.Length > 0)
        //{
        //    conditions += " and mstuser.StateCode in(" + ddlStatecode + ") ";

        //}

        string conditions6 = "";
        if (Flag != 5)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "  and   mst2District.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
        }
        if (Convert.ToInt32(ddlYear.SelectedValue) == 2026)
        {
            if (ddlStatecode.Length > 0)
            {
                conditions += " and mstuser.Statecode in(" + ddlStatecode + ") ";
                conditions6 += " and mstuser.Statecode in(" + ddlStatecode + ") ";
            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and mstuser.DistrictCode in(" + ddlDistrict + ") ";
                conditions6 += " and mstuser.DistrictCode in(" + ddlDistrict + ") ";
            }
            if (ddlBlock.Length > 0)
            {
                conditions += " and mstuser.BlockCode in(" + ddlBlock + ") ";
                conditions6 += " and mstuser.BlockCode in(" + ddlBlock + ") ";
            }
            if (ddlPhan.Length > 0)
            {
                conditions += " and mstuser.villagecode in(" + ddlPhan + ") ";
                conditions6 += " and mstuser.villagecode in(" + ddlPhan + ") ";

            }

        }
        else
        {
            if (ddlStatecode.Length > 0)
            {
                conditions += " and mstuser2026.Statecode in(" + ddlStatecode + ") ";
                conditions6 += " and mstuser2026.Statecode in(" + ddlStatecode + ") ";
            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and mstuser2026.DistrictCode in(" + ddlDistrict + ") ";
                conditions6 += " and mstuser2026.DistrictCode in(" + ddlDistrict + ") ";
            }
            if (ddlBlock.Length > 0)
            {
                conditions += " and mstuser2026.BlockCode in(" + ddlBlock + ") ";
                conditions6 += " and mstuser2026.BlockCode in(" + ddlBlock + ") ";
            }
            if (ddlPhan.Length > 0)
            {
                conditions += " and mstuser2026.villagecode in(" + ddlPhan + ") ";
                conditions6 += " and mstuser2026.villagecode in(" + ddlPhan + ") ";

            }
        }

        int mYear = 0;

        if (Convert.ToInt32(ddlGender.SelectedValue) == 1 || Convert.ToInt32(ddlGender.SelectedValue) == 2 || Convert.ToInt32(ddlGender.SelectedValue) == 3)
        {
            mYear = Convert.ToInt32(ddlYear.SelectedValue) + 1;
        }
        else
        {
            mYear = Convert.ToInt32(ddlYear.SelectedValue);
        }
        if (Flag == 6)
        {
            if (Convert.ToInt32(ddlGender.SelectedValue) > 0)
            {
                conditions += " and tblTravelMatrixDeatils2024AuditTrailRejection.mmonth =" + ddlGender.SelectedValue + " ";
            }
            // conditions += " and tblTravelMatrixDeatils2024AuditTrailRejection.mYear =" + mYear + " ";
            //if (ddlGroup.SelectedIndex > 0)
            //{
            //    conditions += " and ( tblTravelMatrixDeatils2024AuditTrailRejection.UserID +' ('+ FromNo +')') ='" + ddlGroup.SelectedValue + "' ";
            //}

        }

        else if (Flag == 8)
        {

        }
        else if (Flag == 3)
        {
            if (Convert.ToInt32(ddlGender.SelectedValue) > 0)
            {
                conditions += " and tblTravelMatrixApproveStatus.mmonth =" + ddlGender.SelectedValue + " ";
                conditions6 += " and tblTravelMatrixDeatils2024.mmonth =" + ddlGender.SelectedValue + " ";
            }
            //conditions += " and tblTravelMatrixApproveStatus.mYear =" + mYear + " ";
            if (ddlGroup.SelectedIndex > 0)
            {
                //conditions += " and ( tblTravelMatrixApproveStatus.UserID +' ('+ FromNo +')') ='" + ddlGroup.SelectedValue + "' ";
                //conditions6 += " and ( tblTravelMatrixDeatils2024.UserID +' ('+ FromNo +')') ='" + ddlGroup.SelectedValue + " '";
            }
        }
        else if (Flag == 5)
        {
            if (Convert.ToInt32(ddlGender.SelectedValue) > 0)
            {
                conditions += " and tblTravelMatrixDeatils2024.mmonth =" + ddlGender.SelectedValue + " ";

            }
        }
        else if (Flag == 7)
        {
            if (Convert.ToInt32(ddlGender.SelectedValue) > 0)
            {
                conditions += " and tblTravelMatrixDeatils2024.mmonth =" + ddlGender.SelectedValue + " ";

            }
        }
        else
        {

            if (ddlGroup.SelectedIndex > 0)
            {
                conditions += " and ( trim(tblTravelMatrixDeatils2024.UserID) +'('+ tblTravelMatrixDeatils2024.FromNo +')') ='" + ddlGroup.SelectedValue + "' ";
            }
            if (Convert.ToInt32(ddlGender.SelectedValue) > 0)
            {
                conditions += " and tblTravelMatrixDeatils2024.mmonth =" + ddlGender.SelectedValue + " ";
            }
            //  conditions += " and tblTravelMatrixDeatils2024.mYear =" + mYear + " ";
        }


        if (Convert.ToInt32(ddlYear.SelectedValue) == 2025)
        {
            conditions = conditions.Replace("tblTravelMatrixDeatils2024", "tblTravelMatrixDeatils2025");
        }

        DataTable dt = null;

        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2024)
        {
            if (Flag == 1)
            {
                SqlParameter[] cmdParameters = new SqlParameter[]
                {
                    new SqlParameter("@Con",conditions),
                    new SqlParameter("@FYear",ddlYear.SelectedItem.Text),
                    new SqlParameter("@MYear",ddlYear.SelectedValue),
                };
                dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTravelDeatil2024ViewReport2026]", cmdParameters);

                if (dt.Rows.Count > 0)
                {
                    dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Emp Name");

                    ExportToCSVFile(dt, "Travel Matrix Detail");
                }
            }

            if (Flag == 2)
            {
                SqlParameter[] cmdParameters = new SqlParameter[]
                {
                    new SqlParameter("@Con",conditions),
                    new SqlParameter("@FYear",ddlYear.SelectedItem.Text),
                    new SqlParameter("@MYear",ddlYear.SelectedValue),
                };
                dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTravelDeatil2024PaymentHold2026]", cmdParameters);

                if (dt.Rows.Count > 0)
                {
                    dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Emp Name");

                    ExportToCSVFile(dt, "Payment Hold");
                }
            }
            if (Flag == 3)
            {
                SqlParameter[] cmdParameters = new SqlParameter[]
                {
                    new SqlParameter("@Con",conditions),
                    new SqlParameter("@Con6",conditions6),
                    new SqlParameter("@FYear",ddlYear.SelectedItem.Text),
                    new SqlParameter("@MYear",ddlYear.SelectedValue),
                };
                dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTravelDeatil2024ApprvalStaus2026]", cmdParameters);

                if (dt.Rows.Count > 0)
                {
                    dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "FC Name", "Employee Name");
                    ExportToCSVFile(dt, "Approval Status");
                }
            }
            if (Flag == 4)
            {
                SqlParameter[] cmdParameters = new SqlParameter[]
                {
                    new SqlParameter("@Con",conditions),
                    new SqlParameter("@FYear",ddlYear.SelectedItem.Text),
                    new SqlParameter("@MYear",ddlYear.SelectedValue),
                };

                dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTravelDeatil2024TADAclaim2026]", cmdParameters);
                if (dt.Rows.Count > 0)
                {
                    dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Emp Name");

                    ExportToCSVFile(dt, "TADAClaimSummary");
                }
            }
            if (Flag == 5)
            {
                if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
                {
                    SqlParameter[] cmdParameters = new SqlParameter[]
                        {
                    new SqlParameter("@Con",conditions),

                     new SqlParameter("@MYear",ddlYear.SelectedValue),
                        };
                    dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTADACorrection2024]", cmdParameters);
                    if (dt.Rows.Count > 0)
                    {
                        dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "FC Name");
                        ExportToCSVFile(dt, "TADACorrectionlog");
                    }
                }

                else
                {
                    SqlParameter[] cmdParameters = new SqlParameter[]
                        {
                    new SqlParameter("@Con",conditions),

                     new SqlParameter("@MYear",ddlYear.SelectedValue),
                        };
                    dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTADACorrection2025]", cmdParameters);
                    if (dt.Rows.Count > 0)
                    {
                        dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "FC Name");
                        ExportToCSVFile(dt, "TADACorrectionlog");
                    }
                }

            }
            if (Flag == 6)
            {
                SqlParameter[] cmdParameters = new SqlParameter[]
                {
                    new SqlParameter("@Con",conditions),
                    new SqlParameter("@FYear",ddlYear.SelectedItem.Text),
                    new SqlParameter("@MYear",ddlYear.SelectedValue),
                };

                dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTravelDeatil2024PaymentReject2026]", cmdParameters);

                if (dt.Rows.Count > 0)
                {
                    dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Emp Name");

                    ExportToCSVFile(dt, "RejectionDeatils");
                }
            }
            if (Flag == 7)
            {
                SqlParameter[] cmdParameters = new SqlParameter[]
                {
                    new SqlParameter("@Con",conditions),
                    new SqlParameter("@FYear",ddlYear.SelectedItem.Text),
                    new SqlParameter("@MYear",ddlYear.SelectedValue),
                };
                dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTravelDeatilFinSummery2026]", cmdParameters);


                if (dt.Rows.Count > 0)
                {
                    dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Employee Name");

                    ExportToCSVFile(dt, "FinanceDataReport");
                }
            }

            if (Flag == 8)
            {
                SqlParameter[] cmdParameters = new SqlParameter[]
                {
                    new SqlParameter("@Con",conditions),
                    new SqlParameter("@FYear",ddlYear.SelectedItem.Text),
                    new SqlParameter("@MYear",ddlYear.SelectedValue),
                };

                dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTravelannaulFinSummery2026]", cmdParameters);
                if (dt.Rows.Count > 0)
                {
                    dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Employee Name");
                    ExportToCSVFile(dt, "FinanceTrendReport");
                }
            }
        }



    }
    public void LoadChildEnrollmenttracker(int Flag)
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
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.ClusterCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }
        //if (ddlGender.SelectedIndex > 0)
        //{
        //    conditions += " and e.Gender= '" + ddlGender.SelectedValue + "' ";
        //}
        //if (Convert.ToInt32(ddlYear.SelectedValue) >= 2023)
        //{
        //    if (ddlGender.SelectedIndex > 0)
        //    {
        //        conditions += " and EnrollmentGnder= '" + ddlGender.SelectedValue + "' ";
        //    }
        //}
        //else
        //{
        //    if (ddlGender.SelectedIndex > 0)
        //    {
        //        conditions += " and e.Gender= '" + ddlGender.SelectedValue + "' ";
        //    }
        //}


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion",conditions),
                new SqlParameter("@Year",ddlYear.SelectedValue),

        };
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportEnrollDeatilsTracker]", cmdParameters);

        if (dt.Rows.Count > 0)
        {



            ExportToCSVFile(dt, "EnrolmentdataeditReport");

        }




    }

    public void LoadChildEnrollmentContact(int Flag)
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
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.ClusterCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }
        if (ddlGender.SelectedIndex > 0)
        {
            conditions += " and e.Gender= '" + ddlGender.SelectedValue + "' ";
        }
        //if (Convert.ToInt32(ddlYear.SelectedValue) >= 2023)
        //{
        //    if (ddlGender.SelectedIndex > 0)
        //    {
        //        conditions += " and EnrollmentGnder= '" + ddlGender.SelectedValue + "' ";
        //    }
        //}
        //else
        //{
        //    if (ddlGender.SelectedIndex > 0)
        //    {
        //        conditions += " and e.Gender= '" + ddlGender.SelectedValue + "' ";
        //    }
        //}


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion",conditions),
            new SqlParameter("@Year",ddlYear.SelectedValue),
        };
        DataTable dt = null;

        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportEnrollDeatilsmatachingContact]", cmdParameters);

        dataTableMaskingHelper.DecryptAndMaskDataTable(dt, true, "HHNo", "Enrolment Child Name", "MotherName", "Father Name", "DOB", "Contact DOB", "Contact Child Name", "Contact Father Name", "Samagra ID", "D2D Child Name", "D2D MotherName", "D2D Father Name", "Latitude", "Longitude");

        if (dt.Rows.Count > 0)
        {
            if (ddlDistrict.Length > 0)
            {
                string Dname = "";
                foreach (ListItem item in chkDistrict.Items)
                {
                    if (item.Selected)
                    {

                        Dname += "'" + item.Text + "'" + ",";


                    }
                }

                if (Dname.Length > 0)
                {
                    Dname = Dname.Substring(0, Dname.LastIndexOf(","));
                }
                DataRow[] dr = dt.Select("DistrictName in(" + Dname + ") ");
                if (dr.Length > 0)
                {
                    ExportToCSVFile(dt, "DiscrepanciesfromContacttoEnrolment");
                }
            }
            else
            {
                ExportToCSVFile(dt, "DiscrepanciesfromContacttoEnrolment");
            }
        }




    }

    public void LoadChildEnrollmentFuzzy(int Flag)
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
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.ClusterCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }
        if (ddlGender.SelectedIndex > 0)
        {
            conditions += " and e.Gender= '" + ddlGender.SelectedValue + "' ";
        }
        //if (Convert.ToInt32(ddlYear.SelectedValue) >= 2023)
        //{
        //    if (ddlGender.SelectedIndex > 0)
        //    {
        //        conditions += " and EnrollmentGnder= '" + ddlGender.SelectedValue + "' ";
        //    }
        //}
        //else
        //{
        //    if (ddlGender.SelectedIndex > 0)
        //    {
        //        conditions += " and e.Gender= '" + ddlGender.SelectedValue + "' ";
        //    }
        //}


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion",conditions),
            new SqlParameter("@Year",ddlYear.SelectedValue),
        };

        DataTable dt = null;

        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportEnrollDeatilsNewFrom2021Fuzzmataching]", cmdParameters);

        dataTableMaskingHelper.DecryptAndMaskDataTable(dt, true, "HHNo", "Enrolment Child Name", "D2D Child Name", "MotherName", "D2D MotherName", "Father Name", "D2D Father Name", "DOB", "Contact DOB", "Contact Child Name", "Contact Father Name", "Samagra ID", "Latitude", "Longitude");

        if (dt.Rows.Count > 0)
        {
            if (ddlDistrict.Length > 0)
            {
                string Dname = "";
                foreach (ListItem item in chkDistrict.Items)
                {
                    if (item.Selected)
                    {
                        Dname += "'" + item.Text + "'" + ",";
                    }
                }

                if (Dname.Length > 0)
                {
                    Dname = Dname.Substring(0, Dname.LastIndexOf(","));
                }
                DataRow[] dr = dt.Select("DistrictName in(" + Dname + ") ");
                if (dr.Length > 0)
                {
                    ExportToCSVFile(dt, "EnrollmentFuzzyMatching");
                }
            }
            else
            {
                ExportToCSVFile(dt, "EnrollmentFuzzyMatching");
            }
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
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.ClusterCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }
        //if (Convert.ToInt32(ddlYear.SelectedValue) >= 2023)
        //{
        //    if (ddlGend        //if (Convert.ToInt32(ddlYear.SelectedValue) >= 2023)
        //{
        //    if (ddlGender.SelectedIndex > 0)
        //    {
        //        conditions += " and EnrollmentGnder= '" + ddlGender.SelectedValue + "' ";
        //    }
        //}
        //else
        //{
        //    if (ddlGender.SelectedIndex > 0)
        //    {
        //        conditions += " and e.Gender= '" + ddlGender.SelectedValue + "' ";
        //    }
        //}
        if (ddlGender.SelectedIndex > 0)
        {
            conditions += " and e.Gender= '" + ddlGender.SelectedValue + "' ";
        }

        //if (Convert.ToInt32(ddlYear.SelectedValue) >= 2023)
        //{
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion",conditions),
            new SqlParameter("@Year",ddlYear.SelectedValue),
        };

        DataTable dt = null;

        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportEnrollDeatilsNewFrom2021]", cmdParameters);

        if (Convert.ToInt32(ddlYear.SelectedValue) == 2026)
            dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Enrolment Child Name", "MotherName", "Father Name", "DOB", "Contact DOB", "Contact Child Name", "Contact Father Name", "Samagra ID", "HHNo", "D2D Child Name", "D2D Mother Name", "D2D Father Name", "Latitude", "Longitude", "AgeAson");
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2025)
            dataTableMaskingHelper.DecryptAndMaskDataTable(dt, true, "Enrolment Child Name", "MotherName", "Father Name", "DOB", "Contact DOB", "Contact Child Name", "Contact Father Name", "Samagra ID", "HHNo", "D2D Child Name", "D2D Mother Name", "D2D Father Name", "Latitude", "Longitude");
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2024)
            dataTableMaskingHelper.DecryptAndMaskDataTable(dt, true, "Enrolment Child Name", "MotherName", "Father Name", "DOB", "Contact DOB", "Contact Child Name", "Contact Father Name", "Samagra ID", "HHNo", "D2D Child Name", "D2D Mother Name", "D2D Father Name", "Latitude", "Longitude");
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2023)
            dataTableMaskingHelper.DecryptAndMaskDataTable(dt, true, "Enrolment Child Name", "MotherName", "Father Name", "DOB", "Contact Child Name", "Contact Father Name", "Samagra ID", "HHNo", "D2D Child Name", "D2D Mother Name", "D2D Father Name", "Latitude", "Longitude");
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2022)
            dataTableMaskingHelper.DecryptAndMaskDataTable(dt, true, "Enrolment Child Name", "MotherName", "Father Name", "DOB", "Contact Child Name", "Contact Father Name", "Samagra ID", "HHNo", "D2D Child Name", "D2D Mother Name", "D2D Father Name", "Latitude", "Longitude");
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2021)
            dataTableMaskingHelper.DecryptAndMaskDataTable(dt, true, "Enrolment Child Name", "MotherName", "Father Name", "DOB", "Contact Child Name", "Contact Father Name", "Samagra ID", "HHNo", "D2D Child Name", "D2D Mother Name", "D2D Father Name");
        else
            dataTableMaskingHelper.DecryptAndMaskDataTable(dt, true, "Enrolment Child Name", "MotherName", "Father Name", "DOB", "Contact Child Name", "Contact Father Name", "Samagra ID", "HHNo", "D2D Child Name", "D2D Mother Name", "D2D Father Name");

        if (dt.Rows.Count > 0)
        {
            objMain.ReportDownload("Enrolment Details", "Enrolment trackers", Convert.ToString(Session["username"]));
            if (ddlDistrict.Length > 0)
            {
                string Dname = "";
                foreach (ListItem item in chkDistrict.Items)
                {
                    if (item.Selected)
                    {

                        Dname += "'" + item.Text + "'" + ",";


                    }
                }

                if (Dname.Length > 0)
                {
                    Dname = Dname.Substring(0, Dname.LastIndexOf(","));
                }
                DataRow[] dr = dt.Select("DistrictName in(" + Dname + ") ");
                if (dr.Length > 0)
                {
                    ExportToCSVFile(dt, "EnrollmentDetails");
                }
            }
            else
            {
                ExportToCSVFile(dt, "EnrollmentDetails");
            }
        }
        //}
        //if (Convert.ToInt32(ddlYear.SelectedValue) >= 2023)
        //{
        //    // dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportEnrollDeatilsNewFrom2021]", cmdParameters);

        //    dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportEnrollDeatilsNewFrom2023]", cmdParameters);
        //}
        //else
        //{
        //    dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportEnrollDeatilsNewFrom2021]", cmdParameters);
        //}

        //else
        //{
        //     conditions = "";
        //     ddlBlock = "";
        //     ddlDistrict = "";
        //     ddlPhan = "";
        //     ddlVillage = "";
        //     ddlStatecode = "";
        //    foreach (ListItem item in ChkState.Items)
        //    {
        //        if (item.Selected)
        //        {

        //            ddlStatecode += "'" + item.Text.ToUpper() + "'" + ",";
        //        }
        //    }

        //    if (ddlStatecode.Length > 0)
        //    {
        //        ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        //    }
        //    foreach (ListItem item in chkDistrict.Items)
        //    {
        //        if (item.Selected)
        //        {

        //            ddlDistrict += "'" + item.Text.ToUpper() + "'" + ",";


        //        }
        //    }

        //    if (ddlDistrict.Length > 0)
        //    {
        //        ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        //    }
        //    foreach (ListItem item in chkBlock.Items)
        //    {
        //        if (item.Selected)
        //        {

        //            ddlBlock += "'" + item.Text.ToUpper() + "'" + ",";


        //        }
        //    }

        //    if (ddlBlock.Length > 0)
        //    {
        //        ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        //    }

        //    foreach (ListItem item in ddlPanchayat.Items)
        //    {
        //        if (item.Selected)
        //        {

        //            ddlPhan += "'" + item.Text.ToUpper() + "'" + ",";


        //        }
        //    }

        //    if (ddlPhan.Length > 0)
        //    {
        //        ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        //    }
        //    foreach (ListItem item in chkVillage.Items)
        //    {
        //        if (item.Selected)
        //        {

        //            ddlVillage += "'" + item.Value + "'" + ",";


        //        }
        //    }

        //    if (ddlVillage.Length > 0)
        //    {
        //        ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        //    }


        //    string sFileDir = Server.MapPath(Comman.GetImagePath("EnrolmentDetailsPath") + "/";
        //    string path = sFileDir + "EnrolmentDetails" + ddlYear.SelectedItem.Text + ".csv";
        //    string FileName = "EnrolmentDetails" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
        //    string path1 = sFileDir + FileName;
        //    string CSVFNAME = "EnrolmentDetails" + DateTime.Now.ToString("yyyyMMddHHmmss");

        //    string inputFilePath = path;

        //    #region Filter and Position
        //    string filterValue1 = ddlYear.SelectedItem.Text; // Replace with your filter value
        //    string filterValue2 = ddlStatecode; // Replace with another filter value
        //    string filterValue3 = ddlDistrict;
        //    string filterValue4 = ddlBlock;
        //    string filterValue5 = ddlPhan;
        //    string filterValue6 = "";
        //    if (ddlGender.SelectedItem.Text.ToString() == "----All----")
        //    {
        //        filterValue6 = "";
        //    }
        //    else
        //    {
        //        filterValue6 = ddlGender.SelectedItem.Text;
        //    }
        //    int columnToFilter1 = 0; // Replace with the index of the column you want to filter
        //    int columnToFilter2 = 0;
        //    int columnToFilter3 = 2;
        //    int columnToFilter4 = 6;
        //    int columnToFilter5 = 10;
        //    int columnToFilter6 = 29;
        //    #endregion Filter and Position
        //    FilterAndCreateCSV(inputFilePath, filterValue1, filterValue2, filterValue3, filterValue4, filterValue5, filterValue6, columnToFilter1, columnToFilter2, columnToFilter3, columnToFilter4, columnToFilter5, columnToFilter6, path1);

        //    FileStream fs = null;
        //    string foldername = Server.MapPath(Comman.GetImagePath("EnrolmentDetailsPath") + "/" + FileName + "");
        //    string fullPath = Request.MapPath("~/EnrolmentDetails/" + FileName + "" + ".zip");
        //    using (ZipFile zip = new ZipFile())
        //    {
        //        zip.UseZip64WhenSaving = Zip64Option.Always;
        //        zip.AddFile(foldername, "");
        //        zip.Save(Server.MapPath(Comman.GetImagePath("EnrolmentDetailsPath") + "/" + FileName + "" + ".zip"));
        //    }

        //    HttpResponse Response = HttpContext.Current.Response; Response.Clear(); Response.ClearHeaders(); Response.Charset = "UTF-8";
        //    fs = File.Open(fullPath, FileMode.Open);
        //    byte[] bytBytes = new byte[(fs.Length)];
        //    fs.Read(bytBytes, 0, (int)fs.Length);
        //    fs.Close();
        //    Response.AddHeader("Content-disposition", "attachment; filename=" + FileName + ".zip");
        //    Response.ContentType = "application/octet-stream";
        //    Response.BinaryWrite(bytBytes);
        //    if (File.Exists(path1))
        //    {
        //        System.IO.File.Delete(path1);
        //    }
        //    if (File.Exists(fullPath))
        //    {
        //        System.IO.File.Delete(fullPath);
        //    }
        //    Response.Flush();
        //    Response.End();
        //}






    }
    static void FilterAndCreateCSV(string inputFilePath, string filterValue1, string filterValue2, string filterValue3, string filterValue4, string filterValue5, string filterValue6, int columnToFilter1, int columnToFilter2, int columnToFilter3, int columnToFilter4, int columnToFilter5, int columnToFilter6, string outputFilePath)
    {
        try
        {
            using (var reader = new StreamReader(inputFilePath))
            using (var writer = new StreamWriter(outputFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var columns = line.Split(','); // Assuming CSV is comma-separated
                    if (filterValue6 != "")
                    {
                        if (filterValue2 != "")
                        {
                            if (filterValue3 != "")
                            {
                                if (filterValue4 != "")
                                {
                                    if (filterValue5 != "")
                                    {
                                        if ((filterValue2.Contains(columns[columnToFilter2]) && filterValue3.Contains(columns[columnToFilter3]) && filterValue4.Contains(columns[columnToFilter4]) && filterValue5.Contains(columns[columnToFilter5]) && filterValue6.Contains(columns[columnToFilter6])))
                                        {
                                            writer.WriteLine(line);
                                        }
                                        else if (columns[0].ToString() == "State Name")
                                        {
                                            writer.WriteLine(line);
                                        }
                                    }
                                    else
                                    {
                                        if ((filterValue2.Contains(columns[columnToFilter2]) && filterValue3.Contains(columns[columnToFilter3]) && filterValue4.Contains(columns[columnToFilter4]) && filterValue6.Contains(columns[columnToFilter6])))
                                        {
                                            writer.WriteLine(line);
                                        }
                                        else if (columns[0].ToString() == "State Name")
                                        {
                                            writer.WriteLine(line);
                                        }
                                    }
                                }
                                else
                                {
                                    if ((filterValue2.Contains(columns[columnToFilter2]) && filterValue3.Contains(columns[columnToFilter3]) && filterValue6.Contains(columns[columnToFilter6])))
                                    {
                                        writer.WriteLine(line);
                                    }
                                    else if (columns[0].ToString() == "State Name")
                                    {
                                        writer.WriteLine(line);
                                    }
                                }
                            }
                            else
                            {
                                if ((filterValue2.Contains(columns[columnToFilter2]) && filterValue6.Contains(columns[columnToFilter6])))
                                {
                                    writer.WriteLine(line);
                                }
                                else if (columns[0].ToString() == "State Name")
                                {
                                    writer.WriteLine(line);
                                }
                            }

                        }
                        else
                        {
                            if ((filterValue6.Contains(columns[columnToFilter6])))
                            {
                                writer.WriteLine(line);
                            }
                            else if (columns[0].ToString() == "State Name")
                            {
                                writer.WriteLine(line);
                            }
                        }

                    }

                    else
                    {
                        if (filterValue2 != "")
                        {
                            if (filterValue3 != "")
                            {
                                if (filterValue4 != "")
                                {
                                    if (filterValue5 != "")
                                    {
                                        if ((filterValue2.Contains(columns[columnToFilter2]) && filterValue3.Contains(columns[columnToFilter3]) && filterValue4.Contains(columns[columnToFilter4]) && filterValue5.Contains(columns[columnToFilter5])))
                                        {
                                            writer.WriteLine(line);
                                        }
                                        else if (columns[0].ToString() == "State Name")
                                        {
                                            writer.WriteLine(line);
                                        }
                                    }
                                    else
                                    {
                                        if ((filterValue2.Contains(columns[columnToFilter2]) && filterValue3.Contains(columns[columnToFilter3]) && filterValue4.Contains(columns[columnToFilter4])))
                                        {
                                            writer.WriteLine(line);
                                        }
                                        else if (columns[0].ToString() == "State Name")
                                        {
                                            writer.WriteLine(line);
                                        }
                                    }
                                }
                                else
                                {
                                    if ((filterValue2.Contains(columns[columnToFilter2]) && filterValue3.Contains(columns[columnToFilter3])))
                                    {
                                        writer.WriteLine(line);
                                    }
                                    else if (columns[0].ToString() == "State Name")
                                    {
                                        writer.WriteLine(line);
                                    }
                                }
                            }
                            else
                            {
                                if ((filterValue2.Contains(columns[columnToFilter2])))
                                {
                                    writer.WriteLine(line);
                                }
                                else if (columns[0].ToString() == "State Name")
                                {
                                    writer.WriteLine(line);
                                }
                            }

                        }
                        else
                        {

                            if (columns[0].ToString() == "State Name")
                            {
                                writer.WriteLine(line);
                            }
                            else
                            {
                                writer.WriteLine(line);
                            }
                        }

                    }

                }
            }
            string str;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }



    public void LoadExceptionReport(int Flag)
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
            conditions += "  where   v.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and v.StateCode in(" + ddlStatecode + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and v.DistrictCode in(" + ddlDistrict + ") ";

        }

        if (ddlBlock.Length > 0)
        {

            conditions += " and v.BlockCode in(" + ddlBlock + ") ";


        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and v.ClusterCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and v.VillageCode in(" + ddlVillage + ") ";
        }



        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion",conditions),
        };
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "ReportEnrollDeatilsNewFromException", cmdParameters);

        if (dt.Rows.Count > 0)
        {
            dataTableMaskingHelper.DecryptAndMaskDataTable(dt, true, "HHNo", "ChildName", "Father Name", "DOB");

            ExportToCSVFile(dt, "ExceptionReport");
        }
    }


    public void LoadPlanReport(int Flag)
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




        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),
                    new SqlParameter("@Conb",ConNe),

                        new SqlParameter("@Fyear",ddlYear.SelectedItem.Text),
                             new SqlParameter("@Gender",ddlGender.SelectedValue),

        };
        DataSet dt = null;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2023)
        {
            dt = GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptEnrollandcontactSummaryNewFinal2023", cmdParameters);

            if (dt.Tables[0].Rows.Count > 0)
            {
                objMain.ReportDownload("Enrolment Summary", "Enrolment trackers", Convert.ToString(Session["username"]));
                ViewState["SAC"] = dt;
                MultipuExecl();
            }
        }
        else
        {

            dt = GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptEnrollandcontactSummaryNewFinal2021", cmdParameters);

            if (dt.Tables[0].Rows.Count > 0)
            {
                objMain.ReportDownload("Enrolment Summary", "Enrolment trackers", Convert.ToString(Session["username"]));
                ViewState["SAC"] = dt;
                MultipuExecl();
            }
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




        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Fyear","2022"),
              new SqlParameter("@mMonth","1"),
             new SqlParameter("@WeekType","3"),
        };
        DataSet dt = null;


        dt = GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptperfomancereort", cmdParameters);

        if (dt.Tables[0].Rows.Count > 0)
        {
            ViewState["SAC"] = dt;
            MultipuExeclProcess();
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

        ws1.Cell(3, 1).InsertData(dt1.Rows);
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
        ws3.Cell(20, 1).InsertData(dt4.Rows);


        DataTable dt5 = dtMain1.Tables[5];
        ws1.Cell(21, 1).InsertData(dt5.Rows);

        DataTable dt6 = dtMain1.Tables[6];
        ws2.Cell(21, 1).InsertData(dt6.Rows);

        DataTable dt7 = dtMain1.Tables[7];
        ws3.Cell(36, 1).InsertData(dt7.Rows);

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

        filepath = StartupPath + "\\PerformanceSummary " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
    public void LoadPlanReportTrakerNew2022(int Flag)
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
        string ConNe = string.Empty;
        string Con1 = string.Empty;
        string ConState = string.Empty;
        string ConDistict = string.Empty;
        string ConBlock = string.Empty;
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "  where    mstCluster.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            ConDistict += "  where    mst2District.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
            ConBlock += "  where    mst3Block.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and mstCluster.StateCode in(" + ddlStatecode + ") ";

            ConState = " and [State Code] in(" + ddlStatecode + ") ";

            ConDistict += " and mst2District.StateCode in(" + ddlStatecode + ") ";
            ConBlock += " and mst3Block.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mstCluster.DistrictCode in(" + ddlDistrict + ") ";
            ConDistict += " and mst2District.DistrictCode in(" + ddlDistrict + ") ";
            ConBlock += " and mst3Block.DistrictCode in(" + ddlDistrict + ") ";

            ConState += " and [DistrictCode] in(" + ddlDistrict + ") ";

        }

        if (ddlBlock.Length > 0)
        {

            conditions += " and mstCluster.BlockCode in(" + ddlBlock + ") ";

            ConBlock += " and mst3Block.BlockCode in(" + ddlBlock + ") ";
            ConState += " and [BlockCode] in(" + ddlBlock + ") ";
        }




        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),


                        new SqlParameter("@Fyear",ddlYear.SelectedItem.Text),
                             new SqlParameter("@Gender",ddlGender.SelectedValue),
                                 new SqlParameter("@ConState",ConState),
                                     new SqlParameter("@ConDistict",ConDistict),
                                         new SqlParameter("@ConBlock",ConBlock),
        };
        DataSet dt = null;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2024)
        {
            dt = GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptEnrolmentQualityAlertFinal2024202517Total", cmdParameters);
            //dt = GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptEnrolmentQualityAlertNew2021", cmdParameters);

            if (dt.Tables[0].Rows.Count > 0)
            {
                objMain.ReportDownload("Enrolment Quality Alert", "Enrolment trackers", Convert.ToString(Session["username"]));

                ViewState["SAC"] = dt;
                MultipuExeclTrackFinal2023();
            }

        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2023)
        {
            dt = GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptEnrolmentQualityAlertFinal2023272023Total", cmdParameters);
            //dt = GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptEnrolmentQualityAlertNew2021", cmdParameters);

            if (dt.Tables[0].Rows.Count > 0)
            {
                objMain.ReportDownload("Enrolment Quality Alert", "Enrolment trackers", Convert.ToString(Session["username"]));

                ViewState["SAC"] = dt;
                MultipuExeclTrackFinal2023();
            }

        }
        else
        {
            dt = GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptEnrolmentQualityAlertFinal202327", cmdParameters);
            //dt = GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptEnrolmentQualityAlertNew2021", cmdParameters);
            objMain.ReportDownload("Enrolment Quality Alert", "Enrolment trackers", Convert.ToString(Session["username"]));

            if (dt.Tables[0].Rows.Count > 0)
            {
                ViewState["SAC"] = dt;
                MultipuExeclTrackFinal2022();
            }

        }


    }
    public void MultipuExeclTrackFinal2023()
    {
        DataSet dtMain1 = ViewState["SAC"] as DataSet;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\QualityAlertNew2023.xlsx");
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
        dt3.Columns.Remove("rowno");
        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("rownNO");
        ws.Cell(2, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 2;
        string str = "A2:AQ" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        #region Cluster
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // % indicators in Red
            int[] arcols = { 8 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% Critical indicators in Red
            int[] arcols = { 9 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% Operational Village with no Enrolment
            int[] arcols = { 10 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% Operational Schools with no Enrolment
            int[] arcols = { 11 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% Total Schools with no Enrolment
            int[] arcols = { 12 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% Confirmed OOD2D
            int[] arcols = { 20 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% D2D Enrolment with no Contact(5-14Yr)
            int[] arcols = { 21 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% Ineligible Conversion in Quality Enrolment(QE)
            int[] arcols = { 22 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }


        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% EIBP (After 1st April) Conversion in QE
            int[] arcols = { 23 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 == 0)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }



            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% Ready to Enrol Conversion in QE

            int[] arcols = { 24 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% Enrolment from CIOOSG verification later than DOA

            int[] arcols = { 25 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% Class-6 Enrolment

            int[] arcols = { 26 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% 5 to 6 Years D2D Conversion

            int[] arcols = { 27 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 70 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 70)
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

            //% Class-6 Enrolment - Same Campus


            int[] arcols = { 28 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }

            }
        }


        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% TB Supported Enrolment


            int[] arcols = { 29 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 35 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 35)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% Incomplete SR Enrolment


            int[] arcols = { 30 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% Pending matching by District/Block


            int[] arcols = { 31 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% of enrolment with age difference (>=3 years)


            int[] arcols = { 32 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //# Operational Villages with no Effort Enrolment


            int[] arcols = { 33 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 1)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //%Enrolment from School Enrolment (Before 1st April)


            int[] arcols = { 34 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 3 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 3)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //%Enrolment from School Enrolment (After 1st April)


            int[] arcols = { 35 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 90)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% Fuzzy Matching


            int[] arcols = { 36 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //% RTE with High Document conversion


            int[] arcols = { 37 };

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

            //% RTE with High Document conversion


            int[] arcols = { 38 };

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

            //% High Risk Enrolment Error


            int[] arcols = { 39 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 8 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 8)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }


        #endregion
        ws1.Cell(2, 1).InsertData(dt1.Rows);
        Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 2;
        string str1 = "A2:AN" + ii1;
        ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        #region block
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            // % indicators in Red
            int[] arcols = { 5 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            //% Critical indicators in Red
            int[] arcols = { 6 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            //% Operational Village with no Enrolment
            int[] arcols = { 7 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            //% Operational Schools with no Enrolment
            int[] arcols = { 8 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            //% Total Schools with no Enrolment
            int[] arcols = { 9 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            //% Confirmed OOD2D
            int[] arcols = { 17 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            //% D2D Enrolment with no Contact(5-14Yr)
            int[] arcols = { 18 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            //% Ineligible Conversion in Quality Enrolment(QE)
            int[] arcols = { 19 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }


        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            //% EIBP (After 1st April) Conversion in QE
            int[] arcols = { 20 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 == 0)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }



            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            //% Ready to Enrol Conversion in QE

            int[] arcols = { 21 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            //% Enrolment from CIOOSG verification later than DOA

            int[] arcols = { 22 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            //% Class-6 Enrolment

            int[] arcols = { 23 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            //% 5 to 6 Years D2D Conversion

            int[] arcols = { 24 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 70 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 80)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 70)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 80)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            //% Class-6 Enrolment - Same Campus


            int[] arcols = { 25 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }

            }
        }


        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            //% TB Supported Enrolment


            int[] arcols = { 26 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 35 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 35)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            //% Incomplete SR Enrolment


            int[] arcols = { 27 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            //% Pending matching by District/Block


            int[] arcols = { 28 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            //% of enrolment with age difference (>=3 years)


            int[] arcols = { 29 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            //# Operational Villages with no Effort Enrolment


            int[] arcols = { 30 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) > 1)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            //%Enrolment from School Enrolment (Before 1st April)


            int[] arcols = { 31 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 3 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 3)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            //%Enrolment from School Enrolment (After 1st April)


            int[] arcols = { 32 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 90)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            //% Fuzzy Matching


            int[] arcols = { 33 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            //% RTE with High Document conversion


            int[] arcols = { 34 };

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


        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            //% RTE with High Document conversion


            int[] arcols = { 35 };

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

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            //% High Risk Enrolment Error


            int[] arcols = { 36 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 8 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 8)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        #endregion


        ws2.Cell(2, 1).InsertData(dt2.Rows);
        Int32 ii11 = Convert.ToInt32(dt2.Rows.Count) + 2;
        string str11 = "A2:AL" + ii11;
        ws2.Range(str11).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws2.Range(str11).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws2.Range(str11).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws2.Range(str11).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        #region Dist
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            // % indicators in Red
            int[] arcols = { 3 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            //% Critical indicators in Red
            int[] arcols = { 4 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            //% Operational Village with no Enrolment
            int[] arcols = { 5 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            //% Operational Schools with no Enrolment
            int[] arcols = { 6 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            //% Total Schools with no Enrolment
            int[] arcols = { 7 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            //% Confirmed OOD2D
            int[] arcols = { 15 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            //% D2D Enrolment with no Contact(5-14Yr)
            int[] arcols = { 16 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            //% Ineligible Conversion in Quality Enrolment(QE)
            int[] arcols = { 17 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }


        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            //% EIBP (After 1st April) Conversion in QE
            int[] arcols = { 18 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 == 0)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            //% Ready to Enrol Conversion in QE

            int[] arcols = { 19 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            //% Enrolment from CIOOSG verification later than DOA

            int[] arcols = { 20 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            //% Class-6 Enrolment

            int[] arcols = { 21 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            //% 5 to 6 Years D2D Conversion

            int[] arcols = { 22 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 70 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 80)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 70)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 80)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            //% Class-6 Enrolment - Same Campus


            int[] arcols = { 23 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }

            }
        }


        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            //% TB Supported Enrolment


            int[] arcols = { 24 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 35 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 35)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            //% Incomplete SR Enrolment


            int[] arcols = { 25 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            //% Pending matching by District/Block


            int[] arcols = { 26 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            //% of enrolment with age difference (>=3 years)


            int[] arcols = { 27 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            //# Operational Villages with no Effort Enrolment


            int[] arcols = { 28 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) > 1)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            //%Enrolment from School Enrolment (Before 1st April)


            int[] arcols = { 29 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 3 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 3)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            //%Enrolment from School Enrolment (After 1st April)


            int[] arcols = { 30 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 90)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            //% Fuzzy Matching


            int[] arcols = { 31 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            //% RTE with High Document conversion


            int[] arcols = { 32 };

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


        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            //% RTE with High Document conversion


            int[] arcols = { 33 };

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

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            //% % High Risk Enrolment Error


            int[] arcols = { 34 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 8 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 8)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        #endregion

        ws3.Cell(2, 1).InsertData(dt3.Rows);
        Int32 ii113 = Convert.ToInt32(dt3.Rows.Count) + 2;
        string str113 = "A2:AK" + ii113;
        ws3.Range(str113).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws3.Range(str113).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws3.Range(str113).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws3.Range(str113).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        #region state
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            // % indicators in Red
            int[] arcols = { 2 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            //% Critical indicators in Red
            int[] arcols = { 3 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            //% Operational Village with no Enrolment
            int[] arcols = { 4 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            //% Operational Schools with no Enrolment
            int[] arcols = { 5 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            //% Total Schools with no Enrolment
            int[] arcols = { 6 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            //% Confirmed OOD2D
            int[] arcols = { 14 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            //% D2D Enrolment with no Contact(5-14Yr)
            int[] arcols = { 15 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            //% Ineligible Conversion in Quality Enrolment(QE)
            int[] arcols = { 16 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }


        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            //% EIBP (After 1st April) Conversion in QE
            int[] arcols = { 17 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 == 0)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            //% Ready to Enrol Conversion in QE

            int[] arcols = { 18 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            //% Enrolment from CIOOSG verification later than DOA

            int[] arcols = { 19 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            //% Class-6 Enrolment

            int[] arcols = { 20 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            //% 5 to 6 Years D2D Conversion

            int[] arcols = { 21 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 70 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 80)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 70)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 80)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            //% Class-6 Enrolment - Same Campus


            int[] arcols = { 22 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }

            }
        }


        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            //% TB Supported Enrolment


            int[] arcols = { 23 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 35 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 40)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 35)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            //% Incomplete SR Enrolment


            int[] arcols = { 24 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            //% Pending matching by District/Block


            int[] arcols = { 25 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            //% of enrolment with age difference (>=3 years)


            int[] arcols = { 26 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            //# Operational Villages with no Effort Enrolment


            int[] arcols = { 27 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) > 1)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            //%Enrolment from School Enrolment (Before 1st April)


            int[] arcols = { 28 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 3 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 3)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            //%Enrolment from School Enrolment (After 1st April)


            int[] arcols = { 29 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 90)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            //% Fuzzy Matching


            int[] arcols = { 30 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            //% RTE with High Document conversion


            int[] arcols = { 31 };

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


        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            //% RTE with High Document conversion


            int[] arcols = { 32 };

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

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            //% % High Risk Enrolment Error


            int[] arcols = { 33 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 8 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 8)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }


            }
        }

        #endregion

        filepath = StartupPath + "\\EnrolmentQualityAlert " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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

    public void MultipuExeclTrackFinal2022()
    {
        DataSet dtMain1 = ViewState["SAC"] as DataSet;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\QualityAlertNew2022.xlsx");
        var ws = wb.Worksheet(1);
        var ws1 = wb.Worksheet(4);
        var ws2 = wb.Worksheet(3);
        var ws3 = wb.Worksheet(2);
        //var ws1 = wb.Worksheet(2);
        //var ws3 = wb.Worksheet(3);
        DataTable dt = dtMain1.Tables[0];
        DataTable dt1 = dtMain1.Tables[1];
        DataTable dt2 = dtMain1.Tables[2];
        DataTable dt3 = dtMain1.Tables[3];
        //dt.Columns.Remove("rownNO");
        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("rownNO");
        ws.Cell(2, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 2;
        string str = "A2:BA" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
        #region Cluster
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            int[] arcols = { 11 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            int[] arcols = { 12 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            int[] arcols = { 23 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 90)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 80)
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
            int[] arcols = { 24 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 25 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 15)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 15)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 26 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 27 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 28 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 29 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 30 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 31 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 33 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 34 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 35 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 36 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 38 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 39 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 40 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 41 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 42 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 43 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 44 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 45 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 46 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 47 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 48 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 49 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 51 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 1)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 52 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 1)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        //for (int x = 2; x < dt.Rows.Count + 2; x++)
        //{
        //    int[] arcols = { 53 };

        //    for (int y = 0; y < arcols.Length; y++)
        //    {
        //        if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
        //        {
        //        }

        //        else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 1)
        //        {
        //            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
        //        }

        //    }
        //}
        #endregion

        ws1.Cell(2, 1).InsertData(dt1.Rows);
        Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 2;
        string str1 = "A2:AS" + ii1;
        ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        #region StateWise
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            int[] arcols = { 3 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 20 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            int[] arcols = { 4 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 20 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }

            }
        }


        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 15 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 80 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 90)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 80)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 90)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 16 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 17 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 15)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 15)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 18 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 19 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 20 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 21 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 22 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 24 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 25 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 26 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 27 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 28 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 30 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 31 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 32 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 33 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 34 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }

            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 35 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 40)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 40)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 36 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 37 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 38 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 39 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 40 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 40)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 40)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 41 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 43 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 1)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 44 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 1)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        //for (int x = 2; x < dt1.Rows.Count + 2; x++)
        //{
        //    int[] arcols = { 45 };

        //    for (int y = 0; y < arcols.Length; y++)
        //    {
        //        if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
        //        {
        //        }

        //        else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 1)
        //        {
        //            ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
        //        }

        //    }
        //}

        #endregion



        ws2.Cell(2, 1).InsertData(dt2.Rows);
        Int32 ii11 = Convert.ToInt32(dt2.Rows.Count) + 2;
        string str11 = "A2:AU" + ii11;
        ws2.Range(str11).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws2.Range(str11).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws2.Range(str11).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws2.Range(str11).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        #region Distict


        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            int[] arcols = { 5 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 20 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            int[] arcols = { 6 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 20 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 17 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 80 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 90)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 80)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 90)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 18 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 19 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 15)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 15)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 20 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 21 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 22 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 23 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 24 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 25 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 27 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 28 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }




        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 29 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 30 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 32 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 33 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 34 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 35 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 36 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }

            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 37 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 40)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 40)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 38 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 39 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 40 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 41 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 42 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 40)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 40)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 43 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 45 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 1)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 46 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 1)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        //for (int x = 2; x < dt2.Rows.Count + 2; x++)
        //{
        //    int[] arcols = { 47 };

        //    for (int y = 0; y < arcols.Length; y++)
        //    {
        //        if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
        //        {
        //        }

        //        else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 1)
        //        {
        //            ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
        //        }

        //    }
        //}

        #endregion

        ws3.Cell(2, 1).InsertData(dt3.Rows);
        Int32 ii113 = Convert.ToInt32(dt3.Rows.Count) + 2;
        string str113 = "A2:AY" + ii113;
        ws3.Range(str113).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws3.Range(str113).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws3.Range(str113).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws3.Range(str113).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        #region Block

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            int[] arcols = { 9 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 20 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            int[] arcols = { 10 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 20 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 21 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 80 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 90)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 80)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 90)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 22 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 23 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 15)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 15)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 24 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 25 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 26 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 27 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 28 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 29 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 31 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 32 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 33 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 34 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 36 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 37 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 38 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 39 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 40 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }

            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 41 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 40)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 40)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 42 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 43 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 44 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 45 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 46 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 40)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 40)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 47 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 49 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 50 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        //for (int x = 2; x < dt3.Rows.Count + 2; x++)
        //{
        //    int[] arcols = { 51 };

        //    for (int y = 0; y < arcols.Length; y++)
        //    {
        //        if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
        //        {
        //        }
        //        else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
        //        {
        //            ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
        //        }
        //        else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
        //        {
        //            ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
        //        }

        //    }
        //}

        #endregion
        filepath = StartupPath + "\\EnrolmentQualityAlert " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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


    public void LoadPlanReportTrakerNCluster(int Flag)
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
        string ConNe = string.Empty;
        string Con1 = string.Empty;
        string ConState = string.Empty;
        string ConDistict = string.Empty;
        string ConBlock = string.Empty;
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "  where    mstCluster.Fyear = '" + ddlYear.SelectedItem.Text + "' ";



        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and mstCluster.StateCode in(" + ddlStatecode + ") ";


        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mstCluster.DistrictCode in(" + ddlDistrict + ") ";


        }

        if (ddlBlock.Length > 0)
        {

            conditions += " and mstCluster.BlockCode in(" + ddlBlock + ") ";


        }




        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),
              new SqlParameter("@Year",ddlYear.SelectedValue),



        };
        DataSet dt = null;

        dt = GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptAllClusterPerformaceReport2024", cmdParameters);
        //dt = GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptEnrolmentQualityAlertNew2021", cmdParameters);
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2023)
        {
            if (dt.Tables[0].Rows.Count > 0)
            {
                ViewState["SAC"] = dt.Tables[0];
                MultipuExeclClusterPerformaceReport2023();
            }
        }
        else
        {
            if (dt.Tables[0].Rows.Count > 0)
            {
                ViewState["SAC"] = dt.Tables[0];
                MultipuExeclClusterPerformaceReport();
            }
        }




    }

    public void MultipuExeclClusterPerformaceReport2023()
    {
        DataTable dtMain1 = ViewState["SAC"] as DataTable;
        dtMain1 = ViewState["SAC"] as DataTable;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\ClusterPerformanceReport2023.xlsx");
        var ws = wb.Worksheet(1);

        DataTable dt = dtMain1;

        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("RowNo");
        ws.Cell(3, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 3;
        string str = "A3:AJ" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        filepath = StartupPath + "\\clusterperformanceReport" + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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

    public void MultipuExeclClusterPerformaceReport()
    {
        DataTable dtMain1 = ViewState["SAC"] as DataTable;
        dtMain1 = ViewState["SAC"] as DataTable;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\ClusterPerformanceReport.xlsx");
        var ws = wb.Worksheet(1);

        DataTable dt = dtMain1;

        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("RowNo");
        ws.Cell(3, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 3;
        string str = "A3:AJ" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        filepath = StartupPath + "\\ClusterPerformanceReport" + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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



    public void rpttravelMatrix(int Flag)
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
        string ConNe = string.Empty;
        string Con1 = string.Empty;
        string ConState = string.Empty;
        string ConDistict = string.Empty;
        string ConBlock = string.Empty;
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "  where    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";



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

            conditions += " and mst5Village.clustercode in(" + ddlPhan + ") ";


        }


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),
                 new SqlParameter("@Fyear",ddlYear.SelectedItem.Text),



        };
        DataSet dt = null;

        dt = GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptTravelMatrixFare", cmdParameters);
        //dt = GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptEnrolmentQualityAlertNew2021", cmdParameters);

        if (dt.Tables[0].Rows.Count > 0)
        {
            ExportToCSVFile(dt.Tables[0], "TravelFare");
        }




    }
    public void MultipuExeclClusterVillagaePerformace()
    {
        DataTable dtMain1 = ViewState["SAC"] as DataTable;
        dtMain1 = ViewState["SAC"] as DataTable;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\VillageExitReadyness.xlsx");
        var ws = wb.Worksheet(1);

        DataTable dt = dtMain1;

        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("RowNo");
        ws.Cell(2, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 3;
        string str = "A2:AD" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        filepath = StartupPath + "\\VillageExitReadinessReport" + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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

    public void LoadPlanReportTraker(int Flag)
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
        string ConNe = string.Empty;
        string Con1 = string.Empty;
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "  where    mstCluster.Fyear = '" + ddlYear.SelectedItem.Text + "' ";


        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and mstCluster.StateCode in(" + ddlStatecode + ") ";


        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mstCluster.DistrictCode in(" + ddlDistrict + ") ";

        }

        if (ddlBlock.Length > 0)
        {

            ConNe += " and mstCluster.BlockCode in(" + ddlBlock + ") ";


        }




        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),


                        new SqlParameter("@Fyear",ddlYear.SelectedItem.Text),
                             new SqlParameter("@Gender",ddlGender.SelectedValue),

        };
        DataSet dt = null;

        dt = GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptEnrolmentQualityAlertFinal", cmdParameters);
        //dt = GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptEnrolmentQualityAlertNew2021", cmdParameters);

        if (dt.Tables[0].Rows.Count > 0)
        {
            ViewState["SAC"] = dt;
            MultipuExeclTrackFinal();
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

    public void MultipuExecl()
    {
        DataSet dtMain1 = ViewState["SAC"] as DataSet;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\Planing.xlsx");
        var ws = wb.Worksheet(1);
        var ws1 = wb.Worksheet(2);
        var ws3 = wb.Worksheet(3);
        DataTable dt = dtMain1.Tables[0];
        dt.Columns.Remove("rownNO");
        DataTable dt1 = dtMain1.Tables[1];

        dt1.Columns.Remove("rownNO");
        ws.Cell(4, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 3;
        string str = "A4:AL" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
        ws1.Cell(4, 1).InsertData(dt1.Rows);

        Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 3;
        string str1 = "A4:AL" + ii1;

        ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        DataTable dt2 = dtMain1.Tables[2];
        dt2.Columns.Remove("rowno");
        ws3.Cell(3, 1).InsertData(dt2.Rows);


        Int32 ii11 = Convert.ToInt32(dt2.Rows.Count) + 2;
        string str11 = "A3:O" + ii11;

        ws3.Range(str11).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws3.Range(str11).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws3.Range(str11).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws3.Range(str11).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
        DataTable dt3 = dtMain1.Tables[3];
        ws3.Cell(2, 2).Value = "Week (" + dt3.Rows[0]["Week1"].ToString() + " to  " + dt3.Rows[0]["Cumulative1"].ToString() + ")";
        ws3.Cell(2, 3).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative1"].ToString() + ")";
        ws3.Cell(2, 4).Value = "Week (" + dt3.Rows[0]["Week2"].ToString() + " to  " + dt3.Rows[0]["Cumulative2"].ToString() + ")";
        ws3.Cell(2, 5).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative2"].ToString() + ")";
        ws3.Cell(2, 6).Value = "Week (" + dt3.Rows[0]["Week3"].ToString() + " to  " + dt3.Rows[0]["Cumulative3"].ToString() + ")";
        ws3.Cell(2, 7).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative3"].ToString() + ")";
        ws3.Cell(2, 8).Value = "Week (" + dt3.Rows[0]["Week4"].ToString() + " to  " + dt3.Rows[0]["Cumulative4"].ToString() + ")";
        ws3.Cell(2, 9).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative4"].ToString() + ")";
        ws3.Cell(2, 10).Value = "Week (" + dt3.Rows[0]["Week5"].ToString() + " to  " + dt3.Rows[0]["Cumulative5"].ToString() + ")";
        ws3.Cell(2, 11).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative5"].ToString() + ")";
        ws3.Cell(2, 12).Value = "Week (" + dt3.Rows[0]["Week6"].ToString() + " to  " + dt3.Rows[0]["Cumulative6"].ToString() + ")";
        ws3.Cell(2, 13).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative6"].ToString() + ")";
        ws3.Cell(2, 14).Value = "Week (" + dt3.Rows[0]["Week7"].ToString() + " to  " + dt3.Rows[0]["Cumulative7"].ToString() + ")";
        ws3.Cell(2, 15).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative7"].ToString() + ")";

        filepath = StartupPath + "\\EnrolmentSummary " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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

    public void MultipuExeclTrackFinal()
    {
        DataSet dtMain1 = ViewState["SAC"] as DataSet;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\QualityAlert.xlsx");
        var ws = wb.Worksheet(1);
        //var ws1 = wb.Worksheet(2);
        //var ws3 = wb.Worksheet(3);
        DataTable dt = dtMain1.Tables[0];
        //dt.Columns.Remove("rownNO");
        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("rownNO");
        ws.Cell(2, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 2;
        string str = "A2:AT" + ii;
        int jj = ii + 1;
        string str2 = "A" + ii + 1 + ":AT" + jj;

        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 21 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 90)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 80)
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
            int[] arcols = { 22 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 23 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 15)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 15)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 24 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 25 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 26 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 27 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 28 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 30 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 31 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 32 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 33 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 35 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 36 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 37 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 38 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 39 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 40 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 41 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 42 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 43 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 44 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 45 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }




        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 46 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        // ws.Range(str2).Sty
        // ws.Range(str2).Style.Fill.BackgroundColor = XLColor.BabyBlue;
        //ws1.Cell(4, 1).InsertData(dt1.Rows);

        //Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 3;
        //string str1 = "A4:AG" + ii1;

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



        filepath = StartupPath + "\\EnrolmentQualityAlert " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
        DataSet dtMain1 = ViewState["SAC"] as DataSet;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\EnrolmentQARFormat.xlsx");
        var ws = wb.Worksheet(1);
        //var ws1 = wb.Worksheet(2);
        //var ws3 = wb.Worksheet(3);
        DataTable dt = dtMain1.Tables[0];
        //dt.Columns.Remove("rownNO");
        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("rownNO");
        ws.Cell(3, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 2;
        string str = "A2:AR" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
        //ws1.Cell(4, 1).InsertData(dt1.Rows);

        //Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 3;
        //string str1 = "A4:AG" + ii1;

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



        filepath = StartupPath + "\\EnrolmentQualityAlert " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
    public void LoadFillSystem(int Flag)
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
            conditions += "  where   v.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and v.StateCode in(" + ddlStatecode + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and v.DistrictCode in(" + ddlDistrict + ") ";

        }

        if (ddlBlock.Length > 0)
        {

            conditions += " and v.BlockCode in(" + ddlBlock + ") ";


        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and v.ClusterCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and v.VillageCode in(" + ddlVillage + ") ";
        }



        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),

        };
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptFillSystemReport", cmdParameters);

        if (dt.Rows.Count > 0)
        {
            ExportToCSVFile(dt, "FilingSystem");
        }




    }

    public void LoadChildSummaryData(int Flag)
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

        string condition5 = string.Empty;

        //if (ddlYear.SelectedIndex > 0)
        //{
        //    conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        //}
        if (ddlStatecode.Length > 0)
        {
            conditions += " and StateCode in(" + ddlStatecode + ") ";
            condition5 += " and mst5Village.StateCode in(" + ddlStatecode + ") ";


        }



        if (ddlDistrict.Length > 0)
        {
            conditions += " and DistrictCode in(" + ddlDistrict + ") ";
            condition5 += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {

            conditions += " and BlockCode in(" + ddlBlock + ") ";
            condition5 += " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and villagecode in(" + ddlPhan + ") ";
            condition5 += " and mst5Village.clustercode in(" + ddlPhan + ") ";
        }

        string conditions1 = "";

        int mYear = 0;

        if (Convert.ToInt32(ddlGender.SelectedValue) == 1 || Convert.ToInt32(ddlGender.SelectedValue) == 2 || Convert.ToInt32(ddlGender.SelectedValue) == 3)
        {
            mYear = Convert.ToInt32(ddlYear.SelectedValue) + 1;
        }
        else
        {
            mYear = Convert.ToInt32(ddlYear.SelectedValue);
        }
        string Fdate = mYear + "" + ddlGender.SelectedValue + "" + "01";
        string Tdate = mYear + "" + ddlGender.SelectedValue + "" + "31";
        int mMonth = 0;

        if (ddlGender.SelectedValue == "1")
        {
            mMonth = 12;
        }
        else
        {
            mMonth = Convert.ToInt32(ddlGender.SelectedValue) - 1;
        }

        if (ddlGender.SelectedValue == "2" || ddlGender.SelectedValue == "3")
        {
            Fdate = DateTime.Now.Year + 1 + "0" + mMonth + "" + "21";
            Tdate = DateTime.Now.Year + "0" + ddlGender.SelectedValue + "-" + "20";
        }
        else if (ddlGender.SelectedValue == "4")
        {
            Fdate = DateTime.Now.Year + "0" + mMonth + "" + "01";
            Tdate = DateTime.Now.Year + "0" + ddlGender.SelectedValue + "" + "20";
        }
        else if (ddlGender.SelectedValue == "1")
        {
            Fdate = DateTime.Now.Year - 1 + "0" + mMonth + "" + "21";
            Tdate = DateTime.Now.Year + "0" + ddlGender.SelectedValue + "" + "20";
        }
        else
        {
            if (mMonth > 9)
            {
                Fdate = DateTime.Now.Year + "" + mMonth + "" + "21";
                Tdate = DateTime.Now.Year + "" + ddlGender.SelectedValue + "" + "20";
            }
            else
            {
                Fdate = DateTime.Now.Year + "0" + mMonth + "" + "21";
                Tdate = DateTime.Now.Year + "0" + ddlGender.SelectedValue + "" + "20";
            }

        }
        //conditions1 += " and (Year([Date])*10000)+(Month([Date])*100+Day([Date])) Between '" + Fdate + "' and '" + Tdate + "'";


        // conditions1 += " and Date BETWEEN '" + Convert.ToDateTime(Fdate).ToString("yyyy-MM-dd") + "' and '" + Convert.ToDateTime(Tdate).ToString("yyyy-MM-dd") + "'";
        conditions1 += " and (Year([Date])*10000)+(Month([Date])*100+Day([Date])) Between '" + Fdate + "' and '" + Tdate + "'";

        //    else
        //    {
        //        DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
        //        DateTime Todate = Convert.ToDateTime(txtTodate.Text);
        //        conditions1 += " and Date BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
        //    }
        //}

        if (ddlYear.SelectedIndex > 0)
        {
            string Year = ddlYear.SelectedItem.Text;
            string[] Year1 = Year.Split('-');
            // conditions1 += "    And Date >= '" + Year1[0] + "-04-01' and Date<='" + Year1[1] + "-03-31'";


        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions1 += " and mst5Village.Fyear= '" + ddlYear.SelectedItem.Text + "' ";
        }

        DataTable dt = null;
        if (Session["FinYear"].ToString() == ddlYear.SelectedItem.Text)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                new SqlParameter("@condtion", conditions),
                new SqlParameter("@condtion1", conditions1),
                new SqlParameter("@condtion5", condition5),
            };

            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTblUserLoginTravelMatrix]", cmdParameters);
        }
        else
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                new SqlParameter("@condtion", conditions),
                new SqlParameter("@condtion1", conditions1),
            };

            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTblUserLoginNew2025]", cmdParameters);
        }


        if (dt.Rows.Count > 0)
        {
            dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "FristName", "Frist Name");
            LoadSV(dt);
        }
    }
    public void LoadSV(DataTable dt)
    {
        //UpdateLatLong();
        //if (ViewState["1"].ToString() == "2")
        //{

        string color = string.Empty;
        var i = 0;
        double distance = 0;
        double Enddistance = 0;
        string abc1 = "";
        string abc2 = "";
        String DataStyle = "border:.5pt solid windowtext;";
        String InvalidGeoLocationStype = "Red";
        String ValidGeoLocationStype = "Green";

        DataTable dt1 = new DataTable();

        #region Add Datatable


        dt1.Columns.Add("State");
        dt1.Columns.Add("District");
        dt1.Columns.Add("Block");
        dt1.Columns.Add("Cluster Name");
        dt1.Columns.Add("Cluster Code");
        dt1.Columns.Add("Login Village Code");
        dt1.Columns.Add("Login Village name");

        dt1.Columns.Add("FC Code");
        dt1.Columns.Add("FC Name");
        dt1.Columns.Add("Visit Date");
        dt1.Columns.Add("Login Time");
        dt1.Columns.Add("Logout Time");
        dt1.Columns.Add("Hours");

        dt1.Columns.Add("Login Location");
        dt1.Columns.Add("Logout Location");
        dt1.Columns.Add("Login Distance");
        dt1.Columns.Add("Logout Distance");
        dt1.Columns.Add("Location Accuracy");
        dt1.Columns.Add("KM As per travel Matrix");

        if (dt.Rows.Count > 0)
        {
            for (i = 0; i < dt.Rows.Count; i++)
            {
                var RowStyle = DataStyle;
                color = "";
                Int32 dHours = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Hours;
                Int32 dMins = (Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"]) - Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"])).Minutes;

                TimeSpan dayJob = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[i]["TimeSheet_EndTime"])).Subtract(Convert.ToDateTime(dt.Rows[i]["TimeSheet_StartTime"]));

                String villageGeoData = dt.Rows[i]["Village_GeoLocation"].ToString();
                abc1 = null;
                abc2 = null;


                if (villageGeoData != null && villageGeoData != "" && villageGeoData != "[]")
                {

                    if (dt.Rows[i]["StarttimeLocation"].ToString() == "" || villageGeoData.Length < 100 || dt.Rows[i]["StarttimeLocation"].ToString() == null || dt.Rows[i]["StarttimeLocation"].ToString() == "Unsupported browser" || dt.Rows[i]["StarttimeLocation"].ToString() == "User denied" || dt.Rows[i]["StarttimeLocation"].ToString() == "Location unavailable" || dt.Rows[i]["StarttimeLocation"].ToString() == "Request timed out" || dt.Rows[i]["StarttimeLocation"].ToString() == "Unknown error" || dt.Rows[i]["StarttimeLocation"].ToString() == "GPS turned off")
                    {
                    }
                    else
                    {

                        //GeoUtils.GeoPointChecker geoChecker = new GeoUtils.GeoPointChecker(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData);

                        if (dt.Rows[i]["StarttimeLocation"].ToString().Length > 4 && dt.Rows[i]["EndtimeLocation"].ToString().ToString().Length > 4)
                        {

                            //if (geoChecker.isValid() == false)
                            //{
                            //distance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData), 2);
                            distance = Math.Round(Convert.ToDouble(dt.Rows[i]["Distance1"].ToString()), 2);

                            if (distance > 4)
                            {
                                color = "Red";
                                abc1 = InvalidGeoLocationStype;
                            }
                            else
                            {
                                color = "Green";
                                abc1 = ValidGeoLocationStype;
                            }
                            //}
                            //else
                            //{
                            //    color = "Green";
                            //    distance = Math.Round(GeoUtils.GeoPointChecker.abcd(dt.Rows[i]["StarttimeLocation"].ToString(), villageGeoData), 2);
                            //    abc1 = ValidGeoLocationStype;
                            //}
                        }
                        else
                        {
                            RowStyle += DataStyle;
                            abc1 = DataStyle;
                            color = "";
                        }

                        //GeoUtils.GeoPointChecker geoChecker1 = new GeoUtils.GeoPointChecker(dt.Rows[i]["EndtimeLocation"].ToString(), villageGeoData);
                        if (dt.Rows[i]["StarttimeLocation"].ToString().Length > 4 && dt.Rows[i]["EndtimeLocation"].ToString().ToString().Length > 4)
                        {
                            Enddistance = Math.Round(Convert.ToDouble(dt.Rows[i]["Distance2"].ToString()), 2);
                            abc2 = ValidGeoLocationStype;
                        }
                        else
                        {
                            abc2 = InvalidGeoLocationStype;
                        }
                    }
                }


                DataRow _dr = dt1.NewRow();
                _dr["State"] = dt.Rows[i]["StateName"].ToString();
                _dr["District"] = dt.Rows[i]["DistrictName"].ToString();
                _dr["Block"] = dt.Rows[i]["BlockName"].ToString();
                _dr["Cluster Name"] = dt.Rows[i]["ClusterName"].ToString();
                _dr["Cluster Code"] = dt.Rows[i]["ClusterCode"].ToString();
                _dr["Login Village Code"] = dt.Rows[i]["VillageCode"].ToString();
                _dr["Login Village name"] = dt.Rows[i]["VillageName"].ToString();

                _dr["FC Code"] = dt.Rows[i]["UserName"].ToString();
                _dr["FC Name"] = dt.Rows[i]["FristName"].ToString();
                _dr["Visit Date"] = Convert.ToDateTime(dt.Rows[i]["Date"].ToString()).ToString("dd/MM/yyy");

                //_dr["Type"] = dt.Rows[i]["Role"].ToString();


                //    _dr["Grampanchayat"] = dt.Rows[i]["PanchayatName"].ToString();

                _dr["Login Time"] = dt.Rows[i]["TimeSheet_StartTime"].ToString();
                _dr["Login Location"] = dt.Rows[i]["StarttimeLocation"].ToString();
                _dr["KM As per travel Matrix"] = dt.Rows[i]["KM"].ToString();

                //dt1.Columns.Add("FC Code");
                //dt1.Columns.Add("FC Name");
                //dt1.Columns.Add("Visit Date");
                //dt1.Columns.Add("Login Time");
                //dt1.Columns.Add("Logout Time");
                //dt1.Columns.Add("Hours");

                //dt1.Columns.Add("Login Location");
                //dt1.Columns.Add("Logout Location");
                //dt1.Columns.Add("Login Distance");
                //dt1.Columns.Add("Logout Distance");
                //dt1.Columns.Add("Location Accuracy");
                //dt1.Columns.Add("KM As per travel Matrix");


                if (abc1 == "red);")
                {

                }
                else if (abc1 == "Green")
                {
                    if (dt.Rows[i]["EndtimeLocation"].ToString() == "GPS turned off")
                    {
                        _dr["Logout Location"] = "";

                    }
                    else
                    {
                        _dr["Logout Location"] = dt.Rows[i]["EndtimeLocation"].ToString();
                    }

                }

                else
                {
                    _dr["Logout Location"] = dt.Rows[i]["EndtimeLocation"].ToString();
                }
                _dr["Logout Time"] = dt.Rows[i]["TimeSheet_EndTime"].ToString();
                _dr["Hours"] = dayJob.Hours + ":" + dayJob.Minutes;

                if (abc1 == "Green")
                {
                    _dr["Login Distance"] = distance + "KM";

                }
                else if (abc1 == "Red")
                {
                    _dr["Login Distance"] = distance + "KM";

                }
                else if (abc1 == "border:.5pt solid windowtext;")
                {
                    _dr["Login Distance"] = "NA";

                }
                else
                {
                    _dr["Login Distance"] = "NA";
                }

                if (abc2 == "Green")
                {
                    _dr["Logout Distance"] = Enddistance + "KM";
                }
                else if (abc2 == "Red")
                {
                    _dr["Logout Distance"] = "NA";
                }
                else if (abc2 == "border:.5pt solid windowtext;")
                {
                    _dr["Logout Distance"] = "NA";
                }
                else
                {
                    _dr["Logout Distance"] = "NA";
                }


                _dr["Location Accuracy"] = color;
                dt1.Rows.Add(_dr);
            }
        }

        #endregion



        ExporttoCSV(dt1, "FCLoginLogoutReport");

        //}
    }


    private void ExporttoCSV(DataTable table, string filename1)
    {
        string filePath = filename1;

        var dataTable = table;
        StringBuilder sbldr = new StringBuilder();
        List<string> columnNames = new List<string>();
        List<string> rows = new List<string>();


        if (dataTable.Columns.Count != 0)
        {
            foreach (DataColumn col in dataTable.Columns)
            {
                sbldr.Append(col.ColumnName + ',');
            }
            sbldr.Append("\r\n");
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (DataColumn column in dataTable.Columns)
                {

                    if (column.ColumnName == "Login Location" || column.ColumnName == "Logout Location")
                    {
                        sbldr.Append(Convert.ToString(row[column]).Replace(",", "- ").Replace("\r", "").Replace("\n", "") + ',');
                    }
                    else
                    {
                        sbldr.Append(Convert.ToString(row[column]).Replace(",", "- ").Replace("\r", "").Replace("\n", "") + ',');
                    }
                }
                sbldr.Append("\r\n");

            }
        }
        //    foreach (DataColumn col in dataTable.Columns)
        //{
        //    builder.Append(col.ColumnName + ',');
        //}
        //builder.Append("\r\n");
        //foreach (DataRow row in dataTable.Rows)
        //{
        //    List<string> currentRow = new List<string>();

        //    foreach (DataColumn column in dataTable.Columns)
        //    {
        //        if (column.ColumnName == "Start Entry Location" || column.ColumnName == "End Entry Location")
        //        {
        //            builder.Append(row[column].ToString().Replace(",", "-"));
        //        }
        //        else
        //        {
        //            builder.Append(row[column].ToString().Replace("\r", "").Replace("\n", ""));
        //        }




        //    }
        //    builder.Append("\r\n");
        //    // rows.Add(string.Join(",", currentRow.ToArray()));

        //}



        //   builder.Append(string.Join("\n", rows.ToArray()));

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
    }

    public void LoadSchoolSummaryData(int Flag)
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
                new SqlParameter("@Group",ddlGroup.SelectedValue),
                    new SqlParameter("@Flag","4"),
                    new SqlParameter("@MYear",ddlYear.SelectedValue),

        };
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTracking]", cmdParameters);



        ViewState["Annual"] = dt;
        Session["ExportExcel"] = dt;
        Session["Grid"] = "GV_DynamicGrid";
        GV_DynamicGrid.Visible = true;
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();

        if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
        {

            GV_DynamicGrid.Columns[0].Visible = true;
            GV_DynamicGrid.Columns[1].Visible = false;
            GV_DynamicGrid.Columns[2].Visible = false;
            //GV_DynamicGrid.Columns[3].Visible = false;
        }
        if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
        {

            GV_DynamicGrid.Columns[0].Visible = false;
            GV_DynamicGrid.Columns[1].Visible = false;
            GV_DynamicGrid.Columns[2].Visible = true;
        }
        if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
        {

            GV_DynamicGrid.Columns[0].Visible = false;
            GV_DynamicGrid.Columns[1].Visible = true;
            GV_DynamicGrid.Columns[2].Visible = false;
        }
        if (dt.Rows.Count > 0)
        {
            GV_DynamicGrid.DataSource = dt;
            GV_DynamicGrid.DataBind();
        }
        else
        {
            GV_DynamicGrid.DataSource = null;
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


    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["Grid"] != null)
            {
                if (Convert.ToString(Session["Grid"]) == "GV_DynamicGrid")
                {
                    ExportGridToExcel(GV_DynamicGrid, "SchoolSummaries ");
                }
                if (Convert.ToString(Session["Grid"]) == "GVChild")
                {
                    ExportGridToExcel(GVChild, "Processmonitoring");
                }
                if (Convert.ToString(Session["Grid"]) == "GVChildTarget")
                {
                    ExportGridToExcel(GVChildTarget, "Targetmonitoring");
                }

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



    protected void LnkChildSummaryTarget_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 8;
        //if (ddlGender.SelectedIndex > 0)
        //{

        LoadChildSummaryDataTarget(1);
        GVChildTarget.Visible = true;
        GV_DynamicGrid.Visible = false;
        GVChild.Visible = false;


    }
    protected void LnkFinr_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 8;
        //if (ddlGender.SelectedIndex > 0)
        //{

        LoadChildSummaryDataTarget(7);
        GVChildTarget.Visible = true;
        GV_DynamicGrid.Visible = false;
        GVChild.Visible = false;


    }

    protected void LnkFinr2_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 8;
        //if (ddlGender.SelectedIndex > 0)
        //{

        LoadChildSummaryDataTarget(8);
        GVChildTarget.Visible = true;
        GV_DynamicGrid.Visible = false;
        GVChild.Visible = false;


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


    protected void lblvisited_Click(object sender, EventArgs e)
    {
        string conditions = "";
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lblvisitedSchools") as LinkButton).Text;
        string lblDistrictCode = (gvr.FindControl("lblDistrictCode") as Label).Text;
        string lblBlockCode = (gvr.FindControl("lblBlockCode") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;

        if (Convert.ToInt32(values) > 0)
        {
            conditions = "";
            string Con = "";
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                conditions += " and mst5Village.DistrictCode= '" + lblDistrictCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                conditions += " and mst5Village.BlockCOde= '" + lblBlockCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                conditions += " and mst5Village.ClusterCode ='" + lblClusterCode + "' ";

            }
            // Con = " and isnull(Sr,0)>0";
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
                new SqlParameter("@con1",Con),
         new SqlParameter("@Flag","11"),


        };
            DataTable dt = null;


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetails]", cmdParameters);
            DisplayDataOnPopup(dt, "Schoolvisited");
            //ExporttoExcel(dt, "notwillingtoshareSRdata");

        }

    }


    protected void SR_Click(object sender, EventArgs e)
    {
        string conditions = "";
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lblSRdata") as LinkButton).Text;
        string lblDistrictCode = (gvr.FindControl("lblDistrictCode") as Label).Text;
        string lblBlockCode = (gvr.FindControl("lblBlockCode") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;

        if (Convert.ToInt32(values) > 0)
        {
            conditions = "";
            string Con = "";
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                conditions += " and mst5Village.DistrictCode= '" + lblDistrictCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                conditions += " and mst5Village.BlockCOde= '" + lblBlockCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                conditions += " and mst5Village.ClusterCode ='" + lblClusterCode + "' ";

            }
            Con = " and Sr=2";
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
                new SqlParameter("@con1",Con),
         new SqlParameter("@Flag","12"),


        };
            DataTable dt = null;


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetails]", cmdParameters);
            DisplayDataOnPopup(dt, "notwillingtoshareSRdata");
            //ExporttoExcel(dt, "notwillingtoshareSRdata");

        }

    }


    protected void visited_Click(object sender, EventArgs e)
    {
        string conditions = "";
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lblvisited") as LinkButton).Text;
        string lblDistrictCode = (gvr.FindControl("lblDistrictCode") as Label).Text;
        string lblBlockCode = (gvr.FindControl("lblBlockCode") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;

        if (Convert.ToInt32(values) > 0)
        {
            conditions = "";
            string Con = "";
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                conditions += " and mst5Village.DistrictCode= '" + lblDistrictCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                conditions += " and mst5Village.BlockCOde= '" + lblBlockCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                conditions += " and mst5Village.ClusterCode= '" + lblClusterCode + "' ";

            }

            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
                new SqlParameter("@con1",Con),
         new SqlParameter("@Flag","2"),

        };
            DataTable dt = null;


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetails]", cmdParameters);
            DisplayDataOnPopup(dt, "Schoolsyettobevisited");
            //ExporttoExcel(dt, "notwillingsealsign");

        }
    }

    protected void NotcompleteSR_Click(object sender, EventArgs e)
    {
        string conditions = "";
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lblChildrenwithincompleteSR") as LinkButton).Text;
        string lblDistrictCode = (gvr.FindControl("lblDistrictCode") as Label).Text;
        string lblBlockCode = (gvr.FindControl("lblBlockCode") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;

        if (Convert.ToInt32(values) > 0)
        {
            conditions = "";
            string Con = "";
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                conditions += " and mst5Village.DistrictCode= '" + lblDistrictCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                conditions += " and mst5Village.BlockCOde= '" + lblBlockCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                conditions += " and mst5Village.ClusterCode= '" + lblClusterCode + "' ";

            }
            if (ddlGender.SelectedIndex > 0)
            {
                conditions += " and Gender= '" + ddlGender.SelectedValue + "' ";

            }
            Con = "  and IsComplete=2  and tblEnrolment.DeleteFlag<>2 ";
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
                new SqlParameter("@con1",Con),
         new SqlParameter("@Flag","3"),

        };
            DataTable dt = null;


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetails]", cmdParameters);
            DisplayDataOnPopup(dt, "InCompleteSR");
            //ExporttoExcel(dt, "IsCompleteSR");

        }
    }

    protected void completeSR_Click(object sender, EventArgs e)
    {
        string conditions = "";
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lblcompleteSR") as LinkButton).Text;
        string lblDistrictCode = (gvr.FindControl("lblDistrictCode") as Label).Text;
        string lblBlockCode = (gvr.FindControl("lblBlockCode") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;

        if (Convert.ToInt32(values) > 0)
        {
            conditions = "";
            string Con = "";
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                conditions += " and mst5Village.DistrictCode= '" + lblDistrictCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                conditions += " and mst5Village.BlockCOde= '" + lblBlockCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                conditions += " and mst5Village.ClusterCode= '" + lblClusterCode + "' ";

            }
            if (ddlGender.SelectedIndex > 0)
            {
                conditions += " and Gender= '" + ddlGender.SelectedValue + "' ";

            }
            Con = " and IsComplete=1 and EnrolmentMatching=1 and Status=1";
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
                new SqlParameter("@con1",Con),
         new SqlParameter("@Flag","3"),

        };
            DataTable dt = null;


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetails]", cmdParameters);
            DisplayDataOnPopup(dt, "IsCompleteSR");
            //ExporttoExcel(dt, "IsCompleteSR");

        }
    }


    protected void matchedatdistrict_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;
        string conditions = "";
        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lblmatchedatdistrict") as LinkButton).Text;
        string lblDistrictCode = (gvr.FindControl("lblDistrictCode") as Label).Text;
        string lblBlockCode = (gvr.FindControl("lblBlockCode") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;

        if (Convert.ToInt32(values) > 0)
        {
            conditions = "";
            string Con = "";
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                conditions += " and mst5Village.DistrictCode= '" + lblDistrictCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                conditions += " and mst5Village.BlockCOde= '" + lblBlockCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                conditions += " and mst5Village.ClusterCode= '" + lblClusterCode + "' ";

            }
            if (ddlGender.SelectedIndex > 0)
            {
                conditions += " and Gender= '" + ddlGender.SelectedValue + "' ";

            }
            Con = " and   IsDoBoFlag in(1,0)  and IsComplete=1 ";
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
                new SqlParameter("@con1",Con),
         new SqlParameter("@Flag","3"),

        };
            DataTable dt = null;


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetails]", cmdParameters);
            DisplayDataOnPopup(dt, "Pendingdistrictmatching");
            //ExporttoExcel(dt, "matchedatdistrict");

        }
    }

    protected void matchedatdistrictFC_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;
        string conditions = "";
        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lblv3is3ited") as LinkButton).Text;
        string lblDistrictCode = (gvr.FindControl("lblDistrictCode") as Label).Text;
        string lblBlockCode = (gvr.FindControl("lblBlockCode") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;

        if (Convert.ToInt32(values) > 0)
        {
            conditions = "";
            string Con = "";
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                conditions += " and mst5Village.DistrictCode= '" + lblDistrictCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                conditions += " and mst5Village.BlockCOde= '" + lblBlockCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                conditions += " and mst5Village.ClusterCode= '" + lblClusterCode + "' ";

            }
            if (ddlGender.SelectedIndex > 0)
            {
                conditions += " and Gender= '" + ddlGender.SelectedValue + "' ";

            }
            Con = " and  IsDoBoFlag=2  and IsComplete=1";
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
                new SqlParameter("@con1",Con),
         new SqlParameter("@Flag","3"),

        };
            DataTable dt = null;


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetails]", cmdParameters);
            DisplayDataOnPopup(dt, "PendingFCBOmatching");
            //ExporttoExcel(dt, "matchedatFCBO");

        }
    }

    protected void Childrenready_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lblChildrenready") as LinkButton).Text;
        string lblDistrictCode = (gvr.FindControl("lblDistrictCode") as Label).Text;
        string lblBlockCode = (gvr.FindControl("lblBlockCode") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;
        string conditions = "";
        if (Convert.ToInt32(values) > 0)
        {
            conditions = "";
            string Con = "";
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                conditions += " and mst5Village.DistrictCode= '" + lblDistrictCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                conditions += " and mst5Village.BlockCOde= '" + lblBlockCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                conditions += " and mst5Village.ClusterCode= '" + lblClusterCode + "' ";

            }
            if (ddlGender.SelectedIndex > 0)
            {
                conditions += " and Gender= '" + ddlGender.SelectedValue + "' ";

            }
            Con = " and  IsComplete=1 and isnull(FormNo,0)>0 and EnrolmentMatching=1   and (SealFormImage=null or SealFormImage='')  ";
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
                new SqlParameter("@con1",Con),
         new SqlParameter("@Flag","3"),

        };
            DataTable dt = null;


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetails]", cmdParameters);
            DisplayDataOnPopup(dt, "Pendingsealsigncollection");
            //ExporttoExcel(dt, "Childrenreadysealsign");

        }
    }

    protected void Childrenreadyforsealsignreceived_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;
        string conditions = "";
        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lblChildrenreadyforsealsignreceived") as LinkButton).Text;
        string lblDistrictCode = (gvr.FindControl("lblDistrictCode") as Label).Text;
        string lblBlockCode = (gvr.FindControl("lblBlockCode") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;

        if (Convert.ToInt32(values) > 0)
        {
            conditions = "";
            string Con = "";
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                conditions += " and mst5Village.DistrictCode= '" + lblDistrictCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                conditions += " and mst5Village.BlockCOde= '" + lblBlockCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                conditions += " and mst5Village.ClusterCode= '" + lblClusterCode + "' ";

            }
            if (ddlGender.SelectedIndex > 0)
            {
                conditions += " and Gender= '" + ddlGender.SelectedValue + "' ";

            }
            Con = "  and  EnrolmentMatching=1  and isnull(FormNo,0)=0 and IsComplete=1    ";
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
                new SqlParameter("@con1",Con),
         new SqlParameter("@Flag","3"),

        };
            DataTable dt = null;


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetails]", cmdParameters);
            DisplayDataOnPopup(dt, "PendingSealsignGeneration");
            //ExporttoExcel(dt, "receivedsealsign");

        }
    }


    protected void ChildrenreadyforsealsignNotreceived_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;
        string conditions = "";
        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lblChildrenreadyforsealsignNotreceived") as LinkButton).Text;
        string lblDistrictCode = (gvr.FindControl("lblDistrictCode") as Label).Text;
        string lblBlockCode = (gvr.FindControl("lblBlockCode") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;

        if (Convert.ToInt32(values) > 0)
        {
            conditions = "";
            string Con = "";
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                conditions += " and mst5Village.DistrictCode= '" + lblDistrictCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                conditions += " and mst5Village.BlockCOde= '" + lblBlockCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                conditions += " and mst5Village.ClusterCode= '" + lblClusterCode + "' ";

            }
            if (ddlGender.SelectedIndex > 0)
            {
                conditions += " and Gender= '" + ddlGender.SelectedValue + "' ";

            }
            Con = " and SealSign<>1  and EnrolmentMatching=1 and isnull(FormNo,0)>0 ";
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
                new SqlParameter("@con1",Con),
         new SqlParameter("@Flag","3"),

        };
            DataTable dt = null;


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetails]", cmdParameters);
            DisplayDataOnPopup(dt, "Notreceivedsealsign");
            //ExporttoExcel(dt, "Notreceivedsealsign");

        }
    }

    protected void Childdatarecollected_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;
        string conditions = "";
        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lb1Childdatarecollected") as LinkButton).Text;
        string lblDistrictCode = (gvr.FindControl("lblDistrictCode") as Label).Text;
        string lblBlockCode = (gvr.FindControl("lblBlockCode") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;

        if (Convert.ToInt32(values) > 0)
        {
            conditions = "";
            string Con = "";
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                conditions += " and mst5Village.DistrictCode= '" + lblDistrictCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                conditions += " and mst5Village.BlockCOde= '" + lblBlockCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                conditions += " and mst5Village.ClusterCode= '" + lblClusterCode + "' ";

            }
            if (ddlGender.SelectedIndex > 0)
            {
                conditions += " and Gender= '" + ddlGender.SelectedValue + "' ";

            }
            Con = " and  isnull(ApprovalStatus,0)=2   ";
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
                new SqlParameter("@con1",Con),
         new SqlParameter("@Flag","5"),

        };
            DataTable dt = null;


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetails]", cmdParameters);
            DisplayDataOnPopup(dt, "Rejectedvalidation");
            // ExporttoExcel(dt, "Recollect");

        }
    }

    protected void Authenticatedenrolment_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;
        string conditions = "";
        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lblAuthenticatedenrolment") as LinkButton).Text;
        string lblDistrictCode = (gvr.FindControl("lblDistrictCode") as Label).Text;
        string lblBlockCode = (gvr.FindControl("lblBlockCode") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;

        if (Convert.ToInt32(values) > 0)
        {
            conditions = "";
            string Con = "";
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                conditions += " and mst5Village.DistrictCode= '" + lblDistrictCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                conditions += " and mst5Village.BlockCOde= '" + lblBlockCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                conditions += " and mst5Village.ClusterCode= '" + lblClusterCode + "' ";

            }
            if (ddlGender.SelectedIndex > 0)
            {
                conditions += " and Gender= '" + ddlGender.SelectedValue + "' ";

            }
            Con = "  and isnull(ApprovalStatus,0)=1 and  EnrolmentMatching=1 and isnull(FormNo,0)>0";
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
                new SqlParameter("@con1",Con),
         new SqlParameter("@Flag","3"),

        };
            DataTable dt = null;


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetails]", cmdParameters);
            DisplayDataOnPopup(dt, "Validatedachievements");
            // ExporttoExcel(dt, "enrolmentachievements");

        }
    }



    protected void AuthenticatedenrolmenYet_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;
        string conditions = "";
        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lbAuthenticatedenrolmentYet") as LinkButton).Text;
        string lblDistrictCode = (gvr.FindControl("lblDistrictCode") as Label).Text;
        string lblBlockCode = (gvr.FindControl("lblBlockCode") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;

        if (Convert.ToInt32(values) > 0)
        {
            conditions = "";
            string Con = "";
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                conditions += " and mst5Village.DistrictCode= '" + lblDistrictCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                conditions += " and mst5Village.BlockCOde= '" + lblBlockCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                conditions += " and mst5Village.ClusterCode= '" + lblClusterCode + "' ";

            }
            if (ddlGender.SelectedIndex > 0)
            {
                conditions += " and Gender= '" + ddlGender.SelectedValue + "' ";

            }
            Con = "  and isnull(ApprovalStatus,0)=0  and  EnrolmentMatching=1  and isnull(FormNo,0)>0 and    len(SealFormImage)>3";
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
                new SqlParameter("@con1",Con),
         new SqlParameter("@Flag","3"),

        };
            DataTable dt = null;


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetails]", cmdParameters);
            DisplayDataOnPopup(dt, "Pendingvalidation");
            //ExporttoExcel(dt, "yettobeauthenticated");

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
    protected void D2Dtargetmet_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lblChildrenwithincompleteSR") as LinkButton).Text;
        string lblDistrictCode = (gvr.FindControl("lblDistrictCode") as Label).Text;
        string lblBlockCode = (gvr.FindControl("lblBlockCode") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;
        string conditions = "";
        if (Convert.ToInt32(values) > 0)
        {
            conditions = "";
            string Con = "";
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                conditions += " and mst5Village.DistrictCode= '" + lblDistrictCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                conditions += " and mst5Village.BlockCOde= '" + lblBlockCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                conditions += " and mst5Village.ClusterCode= '" + lblClusterCode + "' ";

            }
            if (ddlGender.SelectedIndex > 0)
            {
                conditions += " and e.Gender= '" + ddlGender.SelectedValue + "' ";
            }
            Con = "  and e.Status=2 and EnrolmentMatching=1 and IsComplete=1  and TblDTD.DeleteFlag<>2 and ISNULL(TblDTD.Verified,0)=0";
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
                new SqlParameter("@con1",Con),
         new SqlParameter("@Flag","5"),
              new SqlParameter("@MYear",ddlYear.SelectedValue),
        };
            DataTable dt = null;


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetailsTarget]", cmdParameters);
            DisplayDataOnPopup(dt, "D2Dchildren");
            // ExporttoExcel(dt, "D2Dchildren");

        }
    }

    protected void D2DtargetmetGSA_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;
        string conditions = "";
        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lblChildrenwffithincompleteSR") as LinkButton).Text;
        string lblDistrictCode = (gvr.FindControl("lblDistrictCode") as Label).Text;
        string lblBlockCode = (gvr.FindControl("lblBlockCode") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;

        if (Convert.ToInt32(values) > 0)
        {
            conditions = "";
            string Con = "";
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                conditions += " and mst5Village.DistrictCode= '" + lblDistrictCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                conditions += " and mst5Village.BlockCOde= '" + lblBlockCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                conditions += " and mst5Village.ClusterCode= '" + lblClusterCode + "' ";

            }
            if (ddlGender.SelectedIndex > 0)
            {
                conditions += " and e.Gender= '" + ddlGender.SelectedValue + "' ";
            }
            Con = "  and e.Status=2 and EnrolmentMatching=1 and IsComplete=1  and TblDTD.DeleteFlag<>2 and ISNULL(TblDTD.Verified,0)=2";
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
                new SqlParameter("@con1",Con),
         new SqlParameter("@Flag","5"),
              new SqlParameter("@MYear",ddlYear.SelectedValue),
        };
            DataTable dt = null;


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetailsTarget]", cmdParameters);
            DisplayDataOnPopup(dt, "D2DchildrenGSA");
            // ExporttoExcel(dt, "D2Dchildren");

        }
    }
    protected void D2DtargetmetCIOOSG_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;
        string conditions = "";
        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lblChildrenwithsincompleteSR") as LinkButton).Text;
        string lblDistrictCode = (gvr.FindControl("lblDistrictCode") as Label).Text;
        string lblBlockCode = (gvr.FindControl("lblBlockCode") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;

        if (Convert.ToInt32(values) > 0)
        {
            conditions = "";
            string Con = "";
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                conditions += " and mst5Village.DistrictCode= '" + lblDistrictCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                conditions += " and mst5Village.BlockCOde= '" + lblBlockCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                conditions += " and mst5Village.ClusterCode= '" + lblClusterCode + "' ";

            }
            if (ddlGender.SelectedIndex > 0)
            {
                conditions += " and e.Gender= '" + ddlGender.SelectedValue + "' ";
            }
            Con = "  and e.Status=2 and EnrolmentMatching=1 and IsComplete=1  and TblDTD.DeleteFlag<>2 and ISNULL(TblDTD.Verified,0)=1";
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
                new SqlParameter("@con1",Con),
         new SqlParameter("@Flag","5"),
              new SqlParameter("@MYear",ddlYear.SelectedValue),
        };
            DataTable dt = null;


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetailsTarget]", cmdParameters);
            DisplayDataOnPopup(dt, "D2DchildrenCIOOSG");
            // ExporttoExcel(dt, "D2Dchildren");

        }
    }

    protected void OOD2Dtargetmet_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lbOOD2Dchildren") as LinkButton).Text;
        string lblDistrictCode = (gvr.FindControl("lblDistrictCode") as Label).Text;
        string lblBlockCode = (gvr.FindControl("lblBlockCode") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;
        string conditions = "";
        if (Convert.ToInt32(values) > 0)
        {
            conditions = "";
            string Con = "";
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                conditions += " and mst5Village.DistrictCode= '" + lblDistrictCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                conditions += " and mst5Village.BlockCOde= '" + lblBlockCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                conditions += " and mst5Village.ClusterCode= '" + lblClusterCode + "' ";

            }
            if (ddlGender.SelectedIndex > 0)
            {
                conditions += " and e.Gender= '" + ddlGender.SelectedValue + "' ";
            }
            Con = "  and e.Status=1 and EnrolmentMatching=1 and IsComplete=1";
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
                new SqlParameter("@con1",Con),
         new SqlParameter("@Flag","3"),
              new SqlParameter("@MYear",ddlYear.SelectedValue),
        };
            DataTable dt = null;


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetailsTarget]", cmdParameters);
            DisplayDataOnPopup(dt, "OOD2Dchildren");
            //ExporttoExcel(dt, "OOD2Dchildren");

        }
    }

    protected void OOD2Droppedout_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lblDroppedoutchildren") as LinkButton).Text;
        string lblDistrictCode = (gvr.FindControl("lblDistrictCode") as Label).Text;
        string lblBlockCode = (gvr.FindControl("lblBlockCode") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;
        string conditions = "";
        if (Convert.ToInt32(values) > 0)
        {
            conditions = "";
            string Con = "";
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                conditions += " and mst5Village.DistrictCode= '" + lblDistrictCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                conditions += " and mst5Village.BlockCOde= '" + lblBlockCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                conditions += " and mst5Village.ClusterCode= '" + lblClusterCode + "' ";

            }
            if (ddlGender.SelectedIndex > 0)
            {
                conditions += " and e.Gender= '" + ddlGender.SelectedValue + "' ";
            }
            Con = "  and  e.SealSign=3 and EnrolmentMatching=1 and e.DeleteFlag<>2 and IsComplete=1";
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
                new SqlParameter("@con1",Con),
         new SqlParameter("@Flag","4"),
           new SqlParameter("@MYear",ddlYear.SelectedValue),

        };
            DataTable dt = null;


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetailsTarget]", cmdParameters);
            DisplayDataOnPopup(dt, "childrenNotinSR");
            //ExporttoExcel(dt, "Droppedoutchildren");

        }
    }


    protected void Childrenlessthan5yearsoldD2D_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lblChildrenlessthan5yearsoldD2D") as LinkButton).Text;
        string lblDistrictCode = (gvr.FindControl("lblDistrictCode") as Label).Text;
        string lblBlockCode = (gvr.FindControl("lblBlockCode") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;
        string conditions = "";
        if (Convert.ToInt32(values) > 0)
        {
            conditions = "";
            string Con = "";
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                conditions += " and mst5Village.DistrictCode= '" + lblDistrictCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                conditions += " and mst5Village.BlockCOde= '" + lblBlockCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                conditions += " and mst5Village.ClusterCode= '" + lblClusterCode + "' ";

            }
            if (ddlGender.SelectedIndex > 0)
            {
                conditions += " and e.Gender= '" + ddlGender.SelectedValue + "' ";
            }
            Con = "    and dbo.udfDateDiffinYrMonDay(e.dob,e.EnrolmentDate)<5  and e.Status=2 and e.DeleteFlag<>2 and IsComplete=1  and EnrolmentMatching=1";
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
                new SqlParameter("@con1",Con),
         new SqlParameter("@Flag","3"),
           new SqlParameter("@MYear",ddlYear.SelectedValue),

        };
            DataTable dt = null;


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetailsTarget]", cmdParameters);
            DisplayDataOnPopup(dt, "D2dChildrenlessthan5");
            //ExporttoExcel(dt, "Childrenlessthan");

        }
    }

    protected void Childrenlessthan14yearsoldD2D_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lbAuthentChildrenover14yearsoldOOD2D") as LinkButton).Text;
        string lblDistrictCode = (gvr.FindControl("lblDistrictCode") as Label).Text;
        string lblBlockCode = (gvr.FindControl("lblBlockCode") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;
        string conditions = "";
        if (Convert.ToInt32(values) > 0)
        {
            conditions = "";
            string Con = "";
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                conditions += " and mst5Village.DistrictCode= '" + lblDistrictCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                conditions += " and mst5Village.BlockCOde= '" + lblBlockCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                conditions += " and mst5Village.ClusterCode= '" + lblClusterCode + "' ";

            }
            if (ddlGender.SelectedIndex > 0)
            {
                conditions += " and e.Gender= '" + ddlGender.SelectedValue + "' ";
            }
            Con = "    and dbo.udfDateDiffinYrMonDay(e.dob,e.EnrolmentDate)>14  and e.Status=2 and IsComplete=1 and e.DeleteFlag<>2 and EnrolmentMatching=1 ";
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
                new SqlParameter("@con1",Con),
         new SqlParameter("@Flag","3"),
           new SqlParameter("@MYear",ddlYear.SelectedValue),

        };
            DataTable dt = null;


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetailsTarget]", cmdParameters);

            DisplayDataOnPopup(dt, "Childrenover14D2d");
            // ExporttoExcel(dt, "Childrenover14D2d");

        }
    }

    protected void Total_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;
        string conditions = "";
        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lblTotalechiddldren") as LinkButton).Text;
        string lblDistrictCode = (gvr.FindControl("lblDistrictCode") as Label).Text;
        string lblBlockCode = (gvr.FindControl("lblBlockCode") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;

        if (Convert.ToInt32(values) > 0)
        {
            conditions = "";
            string Con = "";
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                conditions += " and mst5Village.DistrictCode= '" + lblDistrictCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                conditions += " and mst5Village.BlockCOde= '" + lblBlockCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                conditions += " and mst5Village.ClusterCode= '" + lblClusterCode + "' ";

            }
            if (ddlGender.SelectedIndex > 0)
            {
                conditions += " and e.Gender= '" + ddlGender.SelectedValue + "' ";
            }
            Con = "    and  e.Status in(1,2) and EnrolmentMatching=1 and IsComplete=1 and e.DeleteFlag<>2 ";
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
                new SqlParameter("@con1",Con),
         new SqlParameter("@Flag","3"),
           new SqlParameter("@MYear",ddlYear.SelectedValue),

        };
            DataTable dt = null;


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetailsTarget]", cmdParameters);

            DisplayDataOnPopup(dt, "TotalD2DandOOD2Dchildren");
            //ExporttoExcel(dt, "Childrenlessthan5OOD2d ");

        }
    }
    protected void Childrenlessthan5yearsoldOOD2D_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lb1Childrenlessthan5yearsoldOOD2D") as LinkButton).Text;
        string lblDistrictCode = (gvr.FindControl("lblDistrictCode") as Label).Text;
        string lblBlockCode = (gvr.FindControl("lblBlockCode") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;
        string conditions = "";
        if (Convert.ToInt32(values) > 0)
        {
            conditions = "";
            string Con = "";
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                conditions += " and mst5Village.DistrictCode= '" + lblDistrictCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                conditions += " and mst5Village.BlockCOde= '" + lblBlockCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                conditions += " and mst5Village.ClusterCode= '" + lblClusterCode + "' ";

            }
            if (ddlGender.SelectedIndex > 0)
            {
                conditions += " and e.Gender= '" + ddlGender.SelectedValue + "' ";
            }
            Con = "    and   e.Status=1  and  dbo.udfDateDiffinYrMonDay(e.dob,e.EnrolmentDate)<5  and IsComplete=1 and EnrolmentMatching=1 ";
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
                new SqlParameter("@con1",Con),
         new SqlParameter("@Flag","3"),
           new SqlParameter("@MYear",ddlYear.SelectedValue),

        };
            DataTable dt = null;


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetailsTarget]", cmdParameters);

            DisplayDataOnPopup(dt, "OOD2DChildrenlessthan5");
            //ExporttoExcel(dt, "Childrenlessthan5OOD2d ");

        }
    }
    //protected void Childrenlessthan14yearsoldD2D_Click(object sender, EventArgs e)
    //{
    //    LinkButton bt = (LinkButton)sender;

    //    GridViewRow gvr = (GridViewRow)bt.NamingContainer;
    //    string values = (gvr.FindControl("lblChildrenover14yearsoldD2D") as LinkButton).Text;
    //    string lblDistrictCode = (gvr.FindControl("lblDistrictCode") as Label).Text;
    //    string lblBlockCode = (gvr.FindControl("lblBlockCode") as Label).Text;
    //    string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;

    //    if (Convert.ToInt32(values) > 0)
    //    {
    //        conditions = "";
    //        string Con = "";
    //        if (ddlYear.SelectedIndex > 0)
    //        {
    //            conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

    //        }
    //        if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
    //        {
    //            conditions += " and mst5Village.DistrictCode= '" + lblDistrictCode + "' ";

    //        }
    //        if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
    //        {
    //            conditions += " and mst5Village.BlockCOde= '" + lblBlockCode + "' ";

    //        }
    //        if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
    //        {
    //            conditions += " and mst5Village.ClusterCode= '" + lblClusterCode + "' ";

    //        }
    //        if (ddlGender.SelectedIndex > 0)
    //        {
    //            conditions += " and e.Gender= '" + ddlGender.SelectedValue + "' ";
    //        }
    //        Con = "    and   e.Status=2  and dbo.udfDateDiffinYrMonDay(e.dob,e.EnrolmentDate)>14 and EnrolmentMatching=1";
    //        SqlParameter[] cmdParameters = new SqlParameter[]
    //    {
    //        new SqlParameter("@con",conditions),
    //            new SqlParameter("@con1",Con),
    //     new SqlParameter("@Flag","3"),
    //       new SqlParameter("@MYear",ddlYear.SelectedValue),

    //    };
    //        DataTable dt = null;


    //        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetailsTarget]", cmdParameters);
    //        DisplayDataOnPopup(dt, "D2dChildrenOver14");
    //        //ExporttoExcel(dt, "ChildrenOver14OOD2d ");

    //    }
    //}


    protected void Childrenlessthan14yearsoldOOD2D_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lblChildrenover14yearsoldD2D") as LinkButton).Text;
        string lblDistrictCode = (gvr.FindControl("lblDistrictCode") as Label).Text;
        string lblBlockCode = (gvr.FindControl("lblBlockCode") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;
        string conditions = "";
        if (Convert.ToInt32(values) > 0)
        {
            conditions = "";
            string Con = "";
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                conditions += " and mst5Village.DistrictCode= '" + lblDistrictCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                conditions += " and mst5Village.BlockCOde= '" + lblBlockCode + "' ";

            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                conditions += " and mst5Village.ClusterCode= '" + lblClusterCode + "' ";

            }
            if (ddlGender.SelectedIndex > 0)
            {
                conditions += " and e.Gender= '" + ddlGender.SelectedValue + "' ";
            }
            Con = "    and   e.Status=1  and dbo.udfDateDiffinYrMonDay(e.dob,e.EnrolmentDate)>14 and IsComplete=1 and EnrolmentMatching=1";
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
                new SqlParameter("@con1",Con),
         new SqlParameter("@Flag","3"),
           new SqlParameter("@MYear",ddlYear.SelectedValue),

        };
            DataTable dt = null;


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetailsTarget]", cmdParameters);
            DisplayDataOnPopup(dt, "OOD2DChildrenOver14");
            //ExporttoExcel(dt, "ChildrenOver14OOD2d ");

        }
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