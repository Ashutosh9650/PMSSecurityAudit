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


public partial class frmGKpChildReg : System.Web.UI.Page
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

                    //if (Convert.ToString(Session["username"]) == "PMSAdmin" || Convert.ToString(Session["username"]) == "SuperAdmin")
                    //{

                    //    LinkButton14.Visible = true;GKPAssessmentSummary_OnClick

                    //}
                    //else
                    //{

                    //    LinkButton14.Visible = false;
                    //}

                    LoadYear();
                    LoadGroup();
                    objComman.BindDLL("mstlookup", "LookupCode,Description1 ", "LookupFlag='G'", "Description1", "Desc", ddlGender, "Description1", "LookupCode", "--All--");
                    LoadUserLeavel();
                    LoadUserLevel();
                    ViewState["1"] = "ss";
                    ViewState["Annual"] = "";
                    ViewState["D2dUser"] = "";

                    //LinkButton7.Visible = true;


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
            objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlGroup, "Type", "ID", "Select");


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
            objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlGroup, "Type", "ID", "Select");

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
        string conditions = "";
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
        if (ddlGroup.SelectedIndex > 0)
        {

            LoadChildSummaryData(Convert.ToInt32(ddlTpye.SelectedValue));
            GVChildTarget.Visible = false;
            GVChild.Visible = true;
            GV_DynamicGrid.Visible = false;
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Plan Type ')</script>", false);
        }




    }
    protected void LnkDeatild_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 498;
        LoadChildEnrollment(1);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;
    }

    protected void LnkDeatildGyanodaya_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 4944;

        LoadChildEnrollmentGyanodaya(1);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;





    }
    protected void LnkDeatild2_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 498;

        LoadChildEnrollment2(1);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;
    }
    protected void LnkFillingSystem_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 9669;
        LoadFillSystem(1);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;
    }

    protected void LnkFillingSystem111_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 9669;

        LoadFillSystemG(1);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;





    }
    protected void LnkFillingSystemGyanodaya_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 9669;

        LoadFillSystemGyanodaya(1);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;





    }
    protected void LnkEX_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 10770;

        LoadExceptionReport(1);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;





    }
    protected void LnkEXGyanodaya_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 10770;

        LoadExceptionReportGyanodaya(1);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;





    }
    protected void LnkEX55_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 10770;

        LoadrptGKPassmentVlass2(1);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;





    }

    protected void LnkgkSummary_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 10770;

        LoadGKSUmmaryReport(1);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;





    }
    protected void GKPAssessmentSummary_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 107870;

        LoadGKPAssessmentSummary(1);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;





    }
    protected void GKPAsses_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 107870;

        LoadSessionMonitoringReport(1);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;





    }

    public void LoadSessionMonitoringReport(int Flag)
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
            new SqlParameter("@con",conditions),
        };

        DataTable dt = null;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2025)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptSessionMonitoringReport", cmdParameters);

            if (dt.Rows.Count > 0)
            {
                dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "FC/TB Name");

                ViewState["SAC"] = dt;
                ExportToCSVFile(dt, "SessionMonitoringReport");
            }
        }
    }

    protected void GKPAssessmentAlert1_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 107870;

        LoadGKPAssessmentSummaryAlterDist(1);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;
    }

    protected void GKPAsImage_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 107870;

        LoadGKPAssessmentSummaryImag(1);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;





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

        LoadPlanReportTraker(1);
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


        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and mst5Village.StateCode in(" + ddlStatecode + ") ";

        }
        //if (ddlDistrict.Length > 0)
        //{
        //    conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";

        //}

        //if (ddlBlock.Length > 0)
        //{

        //    conditions += " and mst5Village.BlockCode in(" + ddlBlock + ") ";


        //}
        //if (ddlPhan.Length > 0)
        //{
        //    conditions += " and mst5Village.ClusterCode in(" + ddlPhan + ") ";
        //}
        //if (ddlVillage.Length > 0)
        //{
        //    conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        //}

        if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
        {
            if (ddlDistrict.Length > 0)
            {
                conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
            }

        }
        if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
        {
            if (ddlDistrict.Length > 0)
            {
                conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
            }
            if (ddlBlock.Length > 0)
            {
                conditions += " and mst5Village.BlockCode in(" + ddlBlock + ") ";
            }

        }
        if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
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
        }



        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),
                new SqlParameter("@Group",ddlGroup.SelectedValue),
                    new SqlParameter("@Flag","4"),
             new SqlParameter("@MYear",ddlYear.SelectedValue),
             new SqlParameter("@Gender",ddlGender.SelectedValue)
        };
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingNew]", cmdParameters);



        ViewState["Annual"] = dt;
        Session["ExportExcel"] = dt;
        Session["Grid"] = "GVChildTarget";
        GVChildTarget.Visible = true;
        GVChildTarget.DataSource = null;
        GVChildTarget.DataBind();

        if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
        {

            GVChildTarget.Columns[0].Visible = true;
            GVChildTarget.Columns[1].Visible = false;
            GVChildTarget.Columns[2].Visible = false;
            //GV_DynamicGrid.Columns[3].Visible = false;
        }
        if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
        {

            GVChildTarget.Columns[0].Visible = false;
            GVChildTarget.Columns[1].Visible = false;
            GVChildTarget.Columns[2].Visible = true;
        }
        if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
        {

            GVChildTarget.Columns[0].Visible = false;
            GVChildTarget.Columns[1].Visible = true;
            GVChildTarget.Columns[2].Visible = false;
        }
        if (dt.Rows.Count > 0)
        {
            GVChildTarget.DataSource = dt;
            GVChildTarget.DataBind();
        }
        else
        {
            GVChildTarget.DataSource = null;
            GVChildTarget.DataBind();
        }




    }
    public void LoadChildEnrollment2(int Flag)
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
        if (Convert.ToInt32(ddlYear.SelectedValue) == 2025)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptGKPchildRestraionClass22026]", cmdParameters);

            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Child Registration Class 2", "GKP Report", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Child Name", "Father Name");

                ExportToCSVFile(dt, "GKPChildRegistration");
            }
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2024)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptGKPchildRestraionClass22024]", cmdParameters);

            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Child Registration Class 2", "GKP Report", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Child Name", "Father Name");

                ExportToCSVFile(dt, "GKPChildRegistration");
            }
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2023)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptGKPchildRestraionClass22023]", cmdParameters);

            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Child Registration Class 2", "GKP Report", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Child Name", "Father Name");

                ExportToCSVFile(dt, "GKPChildRegistration");
            }
        }
        else
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptGKPchildRestraionClass2]", cmdParameters);

            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Child Registration Class 2", "GKP Report", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Child Name", "Father Name");

                ExportToCSVFile(dt, "GKPChildRegistration");
            }
        }
    }

    public void LoadChildEnrollmentGyanodaya(int Flag)
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
        DataTable dt = null;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2024)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                new SqlParameter("@Con",conditions),
            };
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptGKPGyanodayachildRestraion2024]", cmdParameters);
        }
        else
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                new SqlParameter("@Con",conditions),
            };

            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptGKPGyanodayachildRestraion2023]", cmdParameters);
        }


        if (dt.Rows.Count > 0)
        {
            objMain.ReportDownload("GKP Gyanodaya Child Registration", "GKP Report", Convert.ToString(Session["username"]));

            dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Child Name", "Father Name");

            ExportToCSVFile(dt, "GyanodayaGKPChildRegistration");
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
            ddlStatecode += "'99'" + ",";
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
        DataTable dt = null;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2025)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                new SqlParameter("@Con",conditions),
            };

            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptGKPchildRestraion2025]", cmdParameters);

        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2024)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                new SqlParameter("@Con",conditions),
            };


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptGKPchildRestraion2024]", cmdParameters);

        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2023)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                new SqlParameter("@Con",conditions),
            };

            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptGKPchildRestraion2023]", cmdParameters);

        }
        else
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                new SqlParameter("@Con",conditions),
            };

            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptGKPchildRestraion]", cmdParameters);

        }

        if (dt.Rows.Count > 0)
        {
            objMain.ReportDownload("GKP Child Registration", "GKP Report", Convert.ToString(Session["username"]));

            dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Child Name", "Father Name");

            ExportToCSVFile(dt, "GKPChildRegistration");
        }
    }
    public void LoadGKSUmmaryReport(int Flag)
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
            ddlStatecode += "'99'" + ",";
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
            new SqlParameter("@con",conditions),

        };
        DataTable dt = null;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2025)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptgkpsummaryNew202307New2026", cmdParameters);

            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Summary", "GKP Report", Convert.ToString(Session["username"]));
                ViewState["SAC"] = dt;

                dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Name of Sarpanch", "Mobile No. Sarpanch");

                MultipuExeclGKPProcess2024();
            }
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2024)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptgkpsummaryNew202307New2024", cmdParameters);

            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Summary", "GKP Report", Convert.ToString(Session["username"]));

                ViewState["SAC"] = dt;
                MultipuExeclGKPProcess2024();
            }
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2023)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptgkpsummaryNew202307New", cmdParameters);

            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Summary", "GKP Report", Convert.ToString(Session["username"]));

                ViewState["SAC"] = dt;
                MultipuExeclGKPProcess2023();
            }
        }

        else
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptgkpsummary", cmdParameters);

            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Summary", "GKP Report", Convert.ToString(Session["username"]));

                ViewState["SAC"] = dt;
                MultipuExeclGKPProcess();
            }
        }



    }


    public void LoadGKPAssessmentSummary(int Flag)
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
            ddlStatecode += "'99'" + ",";
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
            new SqlParameter("@con",conditions),

        };
        DataTable dt = null;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2025)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptGKPAssessmentSummary2026", cmdParameters);

            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Assessment Summary", "GKP Report", Convert.ToString(Session["username"]));

                ViewState["SAC"] = dt;
                MultipuExeclGKPGKPAssessmentSummary();
            }
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2024)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptGKPAssessmentSummary2024", cmdParameters);

            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Assessment Summary", "GKP Report", Convert.ToString(Session["username"]));

                ViewState["SAC"] = dt;
                MultipuExeclGKPGKPAssessmentSummary();
            }
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2023)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptGKPAssessmentSummary2023", cmdParameters);

            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Assessment Summary", "GKP Report", Convert.ToString(Session["username"]));

                ViewState["SAC"] = dt;
                MultipuExeclGKPGKPAssessmentSummary();
            }
        }
        else
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptGKPAssessmentSummary", cmdParameters);

            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Assessment Summary", "GKP Report", Convert.ToString(Session["username"]));

                ViewState["SAC"] = dt;
                MultipuExeclGKPGKPAssessmentSummary();
            }
        }



    }
    public void MultipuExeclGKPGKPAssessmentSummary()
    {
        string filepath = "";
        try
        {
            DataTable dtMain1 = ViewState["SAC"] as DataTable;
            dtMain1 = ViewState["SAC"] as DataTable;
            string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));

            XLWorkbook wb = new XLWorkbook();
            wb = new XLWorkbook(StartupPath + "\\GKPAssessmentSummary.xlsx");
            var ws = wb.Worksheet(1);

            DataTable dt = dtMain1;

            //DataTable dt1 = dtMain1.Tables[1];

            //dt1.Columns.Remove("RowNo");
            ws.Cell(3, 1).InsertData(dt.Rows);
            Int32 ii = Convert.ToInt32(dt.Rows.Count) + 3;
            string str = "A3:CO" + ii;
            ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);



            filepath = StartupPath + "\\GKPAssessmentSummary " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
        catch
        {
            //if (File.Exists(filepath))
            //{
            //    System.IO.File.Delete(filepath);
            //}
        }

    }


    public void LoadGKPAssessmentSummaryAlter(int Flag)
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
            new SqlParameter("@con",conditions),

        };
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptGKPAssessmentSummaryAlter2022", cmdParameters);

        if (dt.Rows.Count > 0)
        {
            ViewState["SAC"] = dt;
            MultipuExeclGKPGKPAssessmentSummaryALter();
        }




    }

    public void MultipuExeclGKPGKPAssessmentSummaryALter()
    {
        DataTable dtMain1 = ViewState["SAC"] as DataTable;
        dtMain1 = ViewState["SAC"] as DataTable;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\GKPQualityAlert.xlsx");
        var ws = wb.Worksheet(1);

        DataTable dt = dtMain1;

        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("RowNo");
        ws.Cell(2, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 3;
        string str = "A2:BB" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //  int[] arcols = { 24 };
            int[] arcols = { 23 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 100)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //  int[] arcols = { 24 };
            int[] arcols = { 24 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 100)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //  int[] arcols = { 24 };
            int[] arcols = { 25 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 100)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //  int[] arcols = { 24 };
            int[] arcols = { 26 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 80 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 90)
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

            //  int[] arcols = { 27 };
            int[] arcols = { 27 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
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
            //int[] arcols = { 26 };
            int[] arcols = { 28 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
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

            int[] arcols = { 29 };
            //int[] arcols = { 27 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 11 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // int[] arcols = { 28 };
            int[] arcols = { 30 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 35 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) <= 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) < 35)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //  int[] arcols = { 29 };
            int[] arcols = { 31 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 35 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) <= 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) < 35)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //int[] arcols = { 32 };
            int[] arcols = { 34 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 31 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 70)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 70 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 99)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 100 || Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // int[] arcols = { 33 };
            int[] arcols = { 35 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 11 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //     int[] arcols = { 34 };
            int[] arcols = { 36 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 80 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 90)
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

            // int[] arcols = { 36 };
            int[] arcols = { 38 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // int[] arcols = { 37 };
            int[] arcols = { 39 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // int[] arcols = { 38 };
            int[] arcols = { 40 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //  int[] arcols = { 39 };
            int[] arcols = { 41 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //int[] arcols = { 40 };
            int[] arcols = { 42 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // int[] arcols = { 41 };
            int[] arcols = { 43 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // int[] arcols = { 42 };
            int[] arcols = { 44 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //    int[] arcols = { 43 };
            int[] arcols = { 45 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //  int[] arcols = { 44 };
            int[] arcols = { 46 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //   int[] arcols = { 45 };
            int[] arcols = { 47 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 5 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //  int[] arcols = { 46 };
            int[] arcols = { 48 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 5 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            int[] arcols = { 49 };
            // int[] arcols = { 47 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }


            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 50 };
            //  int[] arcols = { 48 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }


            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //int[] arcols = { 51 };
            int[] arcols = { 52 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 1)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //   int[] arcols = { 52 };
            int[] arcols = { 54 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 1)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }



            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //   int[] arcols = { 53 };
            int[] arcols = { 55 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 1)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }



            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // int[] arcols = { 54 };
            int[] arcols = { 56 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 1)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }



            }
        }
        filepath = StartupPath + "\\GKPQualityAlert" + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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


    public void LoadGKPAssessmentSummaryImag(int Flag)
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


        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "  where   mst3Block.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and mst3Block.StateCode in(" + ddlStatecode + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst3Block.DistrictCode in(" + ddlDistrict + ") ";

        }

        if (ddlBlock.Length > 0)
        {

            conditions += " and mst3Block.BlockCode in(" + ddlBlock + ") ";


        }


        if (ddlYear.SelectedIndex > 0)
        {
            condition1 += "  where   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlStatecode.Length > 0)
        {
            condition1 += " and mst5Village.StateCode in(" + ddlStatecode + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            condition1 += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";

        }

        if (ddlBlock.Length > 0)
        {

            condition1 += " and mst5Village.BlockCode in(" + ddlBlock + ") ";


        }
        if (ddlPhan.Length > 0)
        {
            condition1 += " and mst5Village.ClusterCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            condition1 += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }



        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",condition1),


        };
        DataSet dt = null;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2024)
        {
            dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptGKPchildAttentionImage2024", cmdParameters);

            if (dt.Tables[0].Rows.Count > 0)
            {
                //objMain.ReportDownload("GKP Quality Alert", "GKP Report", Convert.ToString(Session["username"]));

                ViewState["SAC"] = dt;
                ExportToCSVFile(dt.Tables[0], "GKPImage");
            }

        }
        else
        {
            dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptGKPchildAttentionImage", cmdParameters);

            if (dt.Tables[0].Rows.Count > 0)
            {
                //objMain.ReportDownload("GKP Quality Alert", "GKP Report", Convert.ToString(Session["username"]));

                ViewState["SAC"] = dt;
                ExportToCSVFile(dt.Tables[0], "GKPImage");
            }

        }



    }

    public void LoadGKPAssessmentSummaryAlterDist(int Flag)
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
            ddlStatecode += "'99'" + ",";
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


        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "  where   mst3Block.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and mst3Block.StateCode in(" + ddlStatecode + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst3Block.DistrictCode in(" + ddlDistrict + ") ";

        }

        if (ddlBlock.Length > 0)
        {

            conditions += " and mst3Block.BlockCode in(" + ddlBlock + ") ";


        }


        if (ddlYear.SelectedIndex > 0)
        {
            condition1 += "  where   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlStatecode.Length > 0)
        {
            condition1 += " and mst5Village.StateCode in(" + ddlStatecode + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            condition1 += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";

        }

        if (ddlBlock.Length > 0)
        {

            condition1 += " and mst5Village.BlockCode in(" + ddlBlock + ") ";


        }
        if (ddlPhan.Length > 0)
        {
            condition1 += " and mst5Village.ClusterCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            condition1 += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }



        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
            new SqlParameter("@con1",condition1),
        };

        DataSet dt = null;
        if (Convert.ToInt32(ddlYear.SelectedValue) == 2025)
        {
            dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GKPAlterAssessmentSummaryBlock20252026", cmdParameters);

            if (dt.Tables[0].Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Quality Alert", "GKP Report", Convert.ToString(Session["username"]));

                ViewState["SAC"] = dt;
                MultipuExeclGKPGKPAssessmentSummaryALterDIstrict2023();
            }
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2024)
        {
            dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GKPAlterAssessmentSummaryBlock202320242025", cmdParameters);

            if (dt.Tables[0].Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Quality Alert", "GKP Report", Convert.ToString(Session["username"]));

                ViewState["SAC"] = dt;
                MultipuExeclGKPGKPAssessmentSummaryALterDIstrict2023();
            }
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2023)
        {
            dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GKPAlterAssessmentSummaryBlock20232024", cmdParameters);

            if (dt.Tables[0].Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Quality Alert", "GKP Report", Convert.ToString(Session["username"]));

                ViewState["SAC"] = dt;
                MultipuExeclGKPGKPAssessmentSummaryALterDIstrict2023();
            }
        }
        else
        {
            dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GKPAlterAssessmentSummaryBlock", cmdParameters);

            if (dt.Tables[0].Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Quality Alert", "GKP Report", Convert.ToString(Session["username"]));

                ViewState["SAC"] = dt;
                MultipuExeclGKPGKPAssessmentSummaryALterDIstrict();
            }
        }
    }
    public void MultipuExeclGKPGKPAssessmentSummaryALterDIstrict2023()
    {
        DataSet dtMain1 = ViewState["SAC"] as DataSet;

        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\GKPQualityAlertDist2023.xlsx");
        var ws = wb.Worksheet(1);
        var ws11 = wb.Worksheet(2);
        var ws2 = wb.Worksheet(3);
        var ws3 = wb.Worksheet(4);
        DataTable dt = dtMain1.Tables[0];
        DataTable dt11 = dtMain1.Tables[1];
        DataTable dt2 = dtMain1.Tables[2];
        DataTable dt3 = dtMain1.Tables[3];


        #region school
        ws.Cell(2, 1).InsertData(dt.Rows);
        Int32 ii55 = Convert.ToInt32(dt.Rows.Count) + 3;
        string str55 = "A2:BB" + ii55;
        ws.Range(str55).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str55).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str55).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str55).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //  int[] arcols = { 24 };
            int[] arcols = { 22 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 100)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //  int[] arcols = { 24 };
            int[] arcols = { 23 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 100)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //  int[] arcols = { 24 };
            int[] arcols = { 24 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 100)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //  int[] arcols = { 24 };
            int[] arcols = { 25 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 80 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 90)
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

            //  int[] arcols = { 27 };
            int[] arcols = { 26 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
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
            //int[] arcols = { 26 };
            int[] arcols = { 27 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
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

            int[] arcols = { 28 };
            //int[] arcols = { 27 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 11 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // int[] arcols = { 28 };
            int[] arcols = { 29 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 35 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) <= 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) < 35)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //  int[] arcols = { 29 };
            int[] arcols = { 30 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 35 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) <= 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) < 35)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //int[] arcols = { 32 };
            int[] arcols = { 33 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 31 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 70)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 70 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 99)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 100 || Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // int[] arcols = { 33 };
            int[] arcols = { 34 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 11 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //     int[] arcols = { 34 };
            int[] arcols = { 35 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 80 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 90)
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

            // int[] arcols = { 36 };
            int[] arcols = { 36 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // int[] arcols = { 37 };
            int[] arcols = { 37 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // int[] arcols = { 38 };
            int[] arcols = { 38 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //  int[] arcols = { 39 };
            int[] arcols = { 39 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //int[] arcols = { 40 };
            int[] arcols = { 40 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // int[] arcols = { 41 };
            int[] arcols = { 41 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // int[] arcols = { 42 };
            int[] arcols = { 42 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //    int[] arcols = { 43 };
            int[] arcols = { 43 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //  int[] arcols = { 44 };
            int[] arcols = { 44 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //   int[] arcols = { 45 };
            int[] arcols = { 45 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 5 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //  int[] arcols = { 46 };
            int[] arcols = { 46 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 5 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            int[] arcols = { 47 };
            // int[] arcols = { 47 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }


            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 48 };
            //  int[] arcols = { 48 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }


            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 49 };
            //  int[] arcols = { 48 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }


            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //int[] arcols = { 51 };
            int[] arcols = { 52 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 1)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //   int[] arcols = { 52 };
            int[] arcols = { 53 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 1)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }



            }
        }
        //for (int x = 2; x < dt.Rows.Count + 2; x++)
        //{

        //    //   int[] arcols = { 53 };
        //    int[] arcols = { 54 };
        //    for (int y = 0; y < arcols.Length; y++)
        //    {
        //        if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
        //        {
        //        }
        //        else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 1)
        //        {
        //            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
        //        }



        //    }
        //}
        //for (int x = 2; x < dt.Rows.Count + 2; x++)
        //{

        //    // int[] arcols = { 54 };
        //    int[] arcols = { 55 };

        //    for (int y = 0; y < arcols.Length; y++)
        //    {
        //        if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
        //        {
        //        }
        //        else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 1)
        //        {
        //            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
        //        }



        //    }
        //}
        #endregion
        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("RowNo");
        ws11.Cell(2, 1).InsertData(dt11.Rows);
        Int32 ii = Convert.ToInt32(dt11.Rows.Count) + 1;
        string str = "A2:AO" + ii;
        ws11.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws11.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws11.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws11.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
        #region block


        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 18 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 19 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 20 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 21 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 22 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 23 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 24 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 25 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 26 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 27 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 28 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 29 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 30 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 31 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 32 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 33 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 34 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 35 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 36 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 37 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        #endregion
        ws2.Cell(2, 1).InsertData(dt2.Rows);
        Int32 ii1 = Convert.ToInt32(dt2.Rows.Count) + 1;
        string str1 = "A2:AK" + ii1;
        ws2.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws2.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws2.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws2.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        #region distrct

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {


            int[] arcols = { 14 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {


            int[] arcols = { 15 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {


            int[] arcols = { 16 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {


            int[] arcols = { 26 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {


            int[] arcols = { 31 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        #endregion


        ws3.Cell(2, 1).InsertData(dt3.Rows);
        Int32 ii2 = Convert.ToInt32(dt3.Rows.Count) + 1;
        string str2 = "A2:AI" + ii2;
        ws3.Range(str2).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws3.Range(str2).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws3.Range(str2).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws3.Range(str2).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        #region #state

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {


            int[] arcols = { 12 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {


            int[] arcols = { 13 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {


            int[] arcols = { 14 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {


            int[] arcols = { 15 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {


            int[] arcols = { 16 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {


            int[] arcols = { 17 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {


            int[] arcols = { 18 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {


            int[] arcols = { 19 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {


            int[] arcols = { 20 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {


            int[] arcols = { 30 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        #endregion


        filepath = StartupPath + "\\GKPQualityAlert" + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
    public void MultipuExeclGKPGKPAssessmentSummaryALterDIstrict()
    {
        DataSet dtMain1 = ViewState["SAC"] as DataSet;

        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\GKPQualityAlertDist.xlsx");
        var ws = wb.Worksheet(1);
        var ws11 = wb.Worksheet(2);
        var ws2 = wb.Worksheet(3);
        var ws3 = wb.Worksheet(4);
        DataTable dt = dtMain1.Tables[0];
        DataTable dt11 = dtMain1.Tables[1];
        DataTable dt2 = dtMain1.Tables[2];
        DataTable dt3 = dtMain1.Tables[3];


        #region school
        ws.Cell(2, 1).InsertData(dt.Rows);
        Int32 ii55 = Convert.ToInt32(dt.Rows.Count) + 3;
        string str55 = "A2:BB" + ii55;
        ws.Range(str55).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str55).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str55).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str55).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //  int[] arcols = { 24 };
            int[] arcols = { 22 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 100)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //  int[] arcols = { 24 };
            int[] arcols = { 23 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 100)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //  int[] arcols = { 24 };
            int[] arcols = { 24 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 100)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //  int[] arcols = { 24 };
            int[] arcols = { 25 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 80 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 90)
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

            //  int[] arcols = { 27 };
            int[] arcols = { 26 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
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
            //int[] arcols = { 26 };
            int[] arcols = { 27 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
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

            int[] arcols = { 28 };
            //int[] arcols = { 27 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 11 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // int[] arcols = { 28 };
            int[] arcols = { 29 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 35 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) <= 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) < 35)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //  int[] arcols = { 29 };
            int[] arcols = { 30 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 35 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) <= 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) < 35)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //int[] arcols = { 32 };
            int[] arcols = { 33 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 31 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 70)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 70 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 99)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 100 || Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // int[] arcols = { 33 };
            int[] arcols = { 34 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 11 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //     int[] arcols = { 34 };
            int[] arcols = { 35 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 80 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 90)
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

            // int[] arcols = { 36 };
            int[] arcols = { 36 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // int[] arcols = { 37 };
            int[] arcols = { 37 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // int[] arcols = { 38 };
            int[] arcols = { 38 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //  int[] arcols = { 39 };
            int[] arcols = { 39 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //int[] arcols = { 40 };
            int[] arcols = { 40 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // int[] arcols = { 41 };
            int[] arcols = { 41 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // int[] arcols = { 42 };
            int[] arcols = { 42 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //    int[] arcols = { 43 };
            int[] arcols = { 43 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //  int[] arcols = { 44 };
            int[] arcols = { 44 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //   int[] arcols = { 45 };
            int[] arcols = { 45 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 5 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //  int[] arcols = { 46 };
            int[] arcols = { 46 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 5 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            int[] arcols = { 47 };
            // int[] arcols = { 47 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }


            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 48 };
            //  int[] arcols = { 48 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }


            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 49 };
            //  int[] arcols = { 48 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                    ws.Cell(x, arcols[y]).Value = "";
                }


            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //int[] arcols = { 51 };
            int[] arcols = { 52 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 1)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }


            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //   int[] arcols = { 52 };
            int[] arcols = { 53 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 1)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }



            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            //   int[] arcols = { 53 };
            int[] arcols = { 54 };
            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 1)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }



            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            // int[] arcols = { 54 };
            int[] arcols = { 55 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 1)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }



            }
        }
        #endregion
        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("RowNo");
        ws11.Cell(2, 1).InsertData(dt11.Rows);
        Int32 ii = Convert.ToInt32(dt11.Rows.Count) + 1;
        string str = "A2:AP" + ii;
        ws11.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws11.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws11.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws11.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
        #region block


        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 18 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 19 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 20 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 21 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 22 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 23 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 24 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 25 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 26 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 27 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 28 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 29 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 30 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 31 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 32 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 33 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 34 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 35 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 36 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt11.Rows.Count + 2; x++)
        {


            int[] arcols = { 37 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws11.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws11.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws11.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        #endregion
        ws2.Cell(2, 1).InsertData(dt2.Rows);
        Int32 ii1 = Convert.ToInt32(dt2.Rows.Count) + 1;
        string str1 = "A2:AL" + ii1;
        ws2.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws2.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws2.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws2.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        #region distrct

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {


            int[] arcols = { 14 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {


            int[] arcols = { 15 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {


            int[] arcols = { 16 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {


            int[] arcols = { 26 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {


            int[] arcols = { 31 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        #endregion


        ws3.Cell(2, 1).InsertData(dt3.Rows);
        Int32 ii2 = Convert.ToInt32(dt3.Rows.Count) + 1;
        string str2 = "A2:AJ" + ii2;
        ws3.Range(str2).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws3.Range(str2).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws3.Range(str2).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws3.Range(str2).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        #region #state

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {


            int[] arcols = { 12 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {


            int[] arcols = { 13 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {


            int[] arcols = { 14 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {


            int[] arcols = { 15 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {


            int[] arcols = { 16 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {


            int[] arcols = { 17 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {


            int[] arcols = { 18 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {


            int[] arcols = { 19 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {


            int[] arcols = { 20 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {


            int[] arcols = { 30 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
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
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0" || Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "0.00")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        #endregion


        filepath = StartupPath + "\\GKPQualityAlert" + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
    public void MultipuExeclGKPProcess2023()
    {
        DataTable dtMain1 = ViewState["SAC"] as DataTable;
        dtMain1 = ViewState["SAC"] as DataTable;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\GKPReport2023.xlsx");
        var ws = wb.Worksheet(1);

        DataTable dt = dtMain1;

        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("RowNo");
        ws.Cell(3, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 3;
        string str = "A4:AY" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);



        filepath = StartupPath + "\\GKPSummary " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
    public void MultipuExeclGKPProcess2024()
    {
        DataTable dtMain1 = ViewState["SAC"] as DataTable;
        dtMain1 = ViewState["SAC"] as DataTable;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\GKPReport2024.xlsx");
        var ws = wb.Worksheet(1);

        DataTable dt = dtMain1;

        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("RowNo");
        ws.Cell(3, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 3;
        string str = "A4:AY" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);



        filepath = StartupPath + "\\GKPSummary " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
    public void MultipuExeclGKPProcess()
    {
        DataTable dtMain1 = ViewState["SAC"] as DataTable;
        dtMain1 = ViewState["SAC"] as DataTable;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\GKPReport.xlsx");
        var ws = wb.Worksheet(1);

        DataTable dt = dtMain1;

        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("RowNo");
        ws.Cell(3, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 3;
        string str = "A4:AP" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);



        filepath = StartupPath + "\\GKPSummary " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
    public void LoadrptGKPassmentVlass2(int Flag)
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

        DataTable dt = null;
        if (Convert.ToInt32(ddlYear.SelectedValue) == 2025)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                new SqlParameter("@con",conditions),
            };

            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptGKPassmentVlass22026", cmdParameters);
            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Assessment Class 2", "GKP Report", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Child Name", "Father Name");

                ExportToCSVFile(dt, "GKPAssessment");
            }
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2024)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                new SqlParameter("@con",conditions),
            };

            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptGKPassmentVlass22024", cmdParameters);

            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Assessment Class 2", "GKP Report", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Child Name", "Father Name");

                ExportToCSVFile(dt, "GKPAssessment");
            }
        }

        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2023)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                new SqlParameter("@con",conditions),
            };


            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptGKPassmentVlass22023", cmdParameters);
            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Assessment Class 2", "GKP Report", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Child Name", "Father Name");

                ExportToCSVFile(dt, "GKPAssessment");
            }
        }
        else
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                new SqlParameter("@con",conditions),
            };

            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptGKPassmentVlass2", cmdParameters);
            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Assessment Class 2", "GKP Report", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Child Name", "Father Name");

                ExportToCSVFile(dt, "GKPAssessment");
            }
        }
    }

    public void LoadExceptionReportGyanodaya(int Flag)
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
            new SqlParameter("@con",conditions),

        };
        DataTable dt = null;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2024)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptGyanodayaAssessmentassment2024", cmdParameters);

            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("Gyanodaya Assessment", "GKP Report", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Child Name", "Father Name");

                ExportToCSVFile(dt, "GyanodayaAssessment");
            }
        }
        else
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptGyanodayaAssessmentassment2023", cmdParameters);

            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("Gyanodaya Assessment", "GKP Report", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Child Name", "Father Name");

                ExportToCSVFile(dt, "GyanodayaAssessment");
            }
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
            ddlStatecode += "'99'" + ",";
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
            new SqlParameter("@con",conditions),

        };
        DataTable dt = null;
        if (Convert.ToInt32(ddlYear.SelectedValue) == 2025)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptGKPassment2025", cmdParameters);

            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Assessment", "GKP Report", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Child Name", "Father Name");

                ExportToCSVFile(dt, "GKPAssessment");
            }
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2024)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptGKPassment2024", cmdParameters);

            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Assessment", "GKP Report", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Child Name", "Father Name");

                ExportToCSVFile(dt, "GKPAssessment");
            }
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2023)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptGKPassment2023", cmdParameters);

            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Assessment", "GKP Report", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Child Name", "Father Name");

                ExportToCSVFile(dt, "GKPAssessment");
            }
        }
        else
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptGKPassment", cmdParameters);

            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Assessment", "GKP Report", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Child Name", "Father Name");

                ExportToCSVFile(dt, "GKPAssessment");
            }
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


        dt = GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptEnrollandcontactSummaryNewFinal2021", cmdParameters);

        if (dt.Tables[0].Rows.Count > 0)
        {
            ViewState["SAC"] = dt;
            MultipuExecl();
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


        dt = GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptEnrolmentQualityAlertNew2021", cmdParameters);

        if (dt.Tables[0].Rows.Count > 0)
        {
            ViewState["SAC"] = dt;
            MultipuExeclTrack();
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
    public void LoadFillSystemGyanodaya(int Flag)
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
            new SqlParameter("@con",conditions),
        };

        DataTable dt = null;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2024)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptGKPchildAttention2023Gyanodaya2024", cmdParameters);

            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Gyanodaya Child Attendence", "GKP Report", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Child Name", "Father Name");

                ExportToCSVFile(dt, "GKPGyanodayaChildAttendance");
            }
        }
        else
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptGKPchildAttention2023Gyanodaya", cmdParameters);

            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Gyanodaya Child Attendence", "GKP Report", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Child Name", "Father Name");

                ExportToCSVFile(dt, "GKPGyanodayaChildAttendance");
            }
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
            ddlStatecode += "'99'" + ",";

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
            new SqlParameter("@con",conditions),

        };
        DataTable dt = null;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2025)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptGKPchildAttention2025New", cmdParameters);

            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Child Attendence", "GKP Report", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Child Name", "Father Name");

                ExportToCSVFile(dt, "GKPChildAttendance");
            }
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2024)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptGKPchildAttention2024New", cmdParameters);
 
            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Child Attendence", "GKP Report", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Child Name", "Father Name");

                ExportToCSVFile(dt, "GKPChildAttendance");
            }
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2023)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptGKPchildAttention2023New", cmdParameters);

            dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Child Name", "Father Name");
        }
        else
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptGKPchildAttention", cmdParameters);

            if (dt.Rows.Count > 0)
            {
                objMain.ReportDownload("GKP Child Attendence", "GKP Report", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Child Name", "Father Name");

                ExportToCSVFile(dt, "GKPChildAttendance");
            }
        }
    }

    public void LoadFillSystemG(int Flag)
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
            ddlStatecode += "'99'" + ",";

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
            new SqlParameter("@con",conditions),
        };

        DataTable dt = null;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2025)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptGKPchildAttentionGyanod2025New", cmdParameters);

            if (dt.Rows.Count > 0)
            {
                dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Child Name", "Father Name");

                ExportToCSVFile(dt, "GKPGyanodayaChildAttendance");
            }
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


        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "     mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and mst5Village.StateCode in(" + ddlStatecode + ") ";

        }
        //if (ddlDistrict.Length > 0)
        //{
        //    conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";

        //}

        //if (ddlBlock.Length > 0)
        //{

        //    conditions += " and mst5Village.BlockCode in(" + ddlBlock + ") ";


        //}
        //if (ddlPhan.Length > 0)
        //{
        //    conditions += " and mst5Village.ClusterCode in(" + ddlPhan + ") ";
        //}
        //if (ddlVillage.Length > 0)
        //{
        //    conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        //}
        if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
        {
            if (ddlDistrict.Length > 0)
            {
                conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
            }

        }
        if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
        {
            if (ddlDistrict.Length > 0)
            {
                conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
            }
            if (ddlBlock.Length > 0)
            {
                conditions += " and mst5Village.BlockCode in(" + ddlBlock + ") ";
            }

        }
        if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
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
        }




        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),
                new SqlParameter("@Group",ddlGroup.SelectedValue),
                    new SqlParameter("@Flag","2"),
             new SqlParameter("@MYear",ddlYear.SelectedValue),
               new SqlParameter("@Gender",ddlGender.SelectedValue),
        };
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingNew]", cmdParameters);



        ViewState["Annual"] = dt;
        Session["ExportExcel"] = dt;
        Session["Grid"] = "GVChild";
        GVChild.Visible = true;
        GVChild.DataSource = null;
        GVChild.DataBind();

        if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
        {

            GVChild.Columns[0].Visible = true;
            GVChild.Columns[1].Visible = false;
            GVChild.Columns[2].Visible = false;
            //GV_DynamicGrid.Columns[3].Visible = false;
        }
        if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
        {

            GVChild.Columns[0].Visible = false;
            GVChild.Columns[1].Visible = false;
            GVChild.Columns[2].Visible = true;
        }
        if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
        {

            GVChild.Columns[0].Visible = false;
            GVChild.Columns[1].Visible = true;
            GVChild.Columns[2].Visible = false;
        }
        if (dt.Rows.Count > 0)
        {
            GVChild.DataSource = dt;
            GVChild.DataBind();
        }
        else
        {
            GVChild.DataSource = null;
            GVChild.DataBind();
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
        if (ddlGroup.SelectedIndex > 0)
        {

            LoadChildSummaryDataTarget(Convert.ToInt32(ddlTpye.SelectedValue));
            GVChildTarget.Visible = true;
            GV_DynamicGrid.Visible = false;
            GVChild.Visible = false;
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Plan Type ')</script>", false);
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
    protected void LnkGKPAttendanceTracker_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 498;
        LoadGKPAttendanceTracker(1);
        GVChildTarget.Visible = false;
        GVChild.Visible = false;
        GV_DynamicGrid.Visible = false;

    }
    public void LoadGKPAttendanceTracker(int Flag)
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
            conditions += " and  mst5Village.StateCode in(" + ddlStatecode + ") ";

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
            conditions += " and VillageCode in(" + ddlVillage + ") ";
        }
        DataTable dt = null;


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),


        };
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2024)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptGKPGyanodayaAlterUser2024]", cmdParameters);
        }
        else
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptGKPGyanodayaAlterUser]", cmdParameters);
        }
        if (dt.Rows.Count > 0)
        {
            objMain.ReportDownload("GKP Attendance Tracker", "GKP Report", Convert.ToString(Session["username"]));
            ViewState["dt"] = dt;
            ExeclGKPGKPttendanceTracker();
        }
    }

    public void ExeclGKPGKPttendanceTracker()
    {
        DataTable dtMain1 = ViewState["dt"] as DataTable;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\GKPAttendanceTracker.xlsx");
        var ws = wb.Worksheet(1);
        DataTable dt = dtMain1;


        ws.Cell(3, 1).InsertData(dt.Rows);
        Int32 ii54 = Convert.ToInt32(dt.Rows.Count) + 3;
        string str55 = "A3:BB" + ii54;
        ws.Range(str55).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str55).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str55).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str55).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        //for (int x = 3; x <= dt.Rows.Count; x++)
        //{

        //    for (int y = 1; y <= 54; y++)
        //    {
        //        ws.Cell(x, y).Value = dt.Rows[x - 1][y - 1].ToString();
        //    }
        //}
        filepath = StartupPath + "\\GyanodayaSummary" + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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