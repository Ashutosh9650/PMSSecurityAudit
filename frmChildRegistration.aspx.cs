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



public partial class frmChildRegistration : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    string conditions = "";
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

                    LinkButton6.Visible = true;

                    LinkButton7.Visible = false;



                }
                // btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }

        }
    }


    public void LoadYear()
    {
        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


    }
    public DataTable CreateDataTable()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYear.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYear;
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

            string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State   order by StateName   ";
            DataTable dtState = objMain.LoadData(strQry1);
            ChkState.DataSource = dtState;
            ChkState.DataTextField = "StateName";
            ChkState.DataValueField = "StateCode";
            ChkState.DataBind();

            ChkState.Enabled = true;
            chkDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "UserName='" + Session["username"].ToString() + "' ";
            string strQry1 = "  SELECT distinct mst1State.StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode inner join mst1State on mst1State.StateCode=mst2District.StateCode where   " + conditions + "  order by StateName   ";
            DataTable dtState = objMain.LoadData(strQry1);
            ChkState.DataSource = dtState;
            ChkState.DataTextField = "StateName";
            ChkState.DataValueField = "StateCode";
            ChkState.DataBind();
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
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State  where   " + conditions + "  order by StateName   ";
            DataTable dtState = objMain.LoadData(strQry1);
            ChkState.DataSource = dtState;
            ChkState.DataTextField = "StateName";
            ChkState.DataValueField = "StateCode";
            ChkState.DataBind();
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



        ddlPanchayat.Items.Clear();
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

    protected void LnkAnnualPlan_OnClick(object sender, EventArgs e)
    {


        ViewState["1"] = 9;

        getSACUpdateReport(1);




    }

    protected void LnkSBLChild(object sender, EventArgs e)
    {


        ViewState["1"] = 500;

        getChildRegistrationSchool(1);




    }
    protected void LnkAnnualPlanFC_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 10;

        getActivitySIPTargetvsAchv(1);




    }
    protected void LnkSBLAtten(object sender, EventArgs e)
    {
        ViewState["1"] = 501;

        ChildAttendanceSchool(1);




    }

    protected void LnkAnnualPl_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 15;

        getActivitySIPTargetvsSummary(1);




    }

    protected void LnkAnnualPlGrad_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 21;

        getActivitySIPTargetvsSummaryGrade(1);




    }
    protected void LnkAVisitors(object sender, EventArgs e)
    {
        ViewState["1"] = 11;

        getActivityVisitors(1);




    }
    protected void LnkAVisitorsSbL(object sender, EventArgs e)
    {
        ViewState["1"] = 511;

        getActivityVisitorsSchool(1);




    }
    protected void LnkVillageLevel(object sender, EventArgs e)
    {
        ViewState["1"] = 16;

        if (ddlGroup.SelectedIndex > 0)
        {
            if (ddlLevel.SelectedIndex > 0)
            {
                getActivityVillagelevel(1);

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Group ')</script>", false);
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Camp ')</script>", false);
        }




    }

    protected void LnkCPLLevel21(object sender, EventArgs e)
    {
        ViewState["1"] = 22;
        if (ddlGroup.SelectedIndex > 0)
        {

            getActivityVillagelevelMulti(1);

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Camp ')</script>", false);
        }


    }
    protected void LnkCPLLevel(object sender, EventArgs e)
    {
        ViewState["1"] = 18;
        if (ddlGroup.SelectedIndex > 0)
        {

            getActivityCPLlevel(1);
            //getActivityVillagelevelMulti(1);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Camp ')</script>", false);
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
        if (ViewState["1"].ToString() == "9" && Convert.ToString(ViewState["SAC"]) != "")
        {
            DataTable st = ViewState["SAC"] as DataTable;

            ExportToCSVFile(st, "ChildRegistration");

        }
        if (ViewState["1"].ToString() == "10" && Convert.ToString(ViewState["SAC"]) != "")
        {
            DataTable st = ViewState["SAC"] as DataTable;
            ExportToCSVFile(st, "ChildAttendance");


        }
        if (ViewState["1"].ToString() == "500" && Convert.ToString(ViewState["SAC"]) != "")
        {
            DataTable st = ViewState["SAC"] as DataTable;

            ExportToCSVFile(st, "ChildRegistrationSchool");

        }
        if (ViewState["1"].ToString() == "501" && Convert.ToString(ViewState["SAC"]) != "")
        {
            DataTable st = ViewState["SAC"] as DataTable;
            ExportToCSVFile(st, "ChildAttendanceSchool");


        }

        if (ViewState["1"].ToString() == "15" && Convert.ToString(ViewState["SAC"]) != "")
        {
            DataTable st = ViewState["SAC"] as DataTable;
            ExportToCSVFile(st, "ChildAttendanceSummary");


        }
        if (ViewState["1"].ToString() == "11")
        {
            DataTable st = ViewState["SAC"] as DataTable;
            ExportToCSVFile(st, "Visitors");


        }
        if (ViewState["1"].ToString() == "511")
        {
            DataTable st = ViewState["SAC"] as DataTable;
            ExportToCSVFile(st, "SBLVisitors");


        }
        if (ViewState["1"].ToString() == "16")
        {
            if (Convert.ToInt32(ddlLevel.SelectedValue) == 2)
            {
                DataTable st = ViewState["SAC"] as DataTable;
                ExportToCSVFile(st, "CBLProgressTrackingvillagewise");
            }
            if (Convert.ToInt32(ddlLevel.SelectedValue) == 1)

            {
                DataTable st = ViewState["SAC"] as DataTable;
                GenerateExcelNew2021("CBLProgressTrackingdistrinctwise");
            }
            if (Convert.ToInt32(ddlLevel.SelectedValue) == 4)
            {
                DataTable st = ViewState["SAC"] as DataTable;
                GenerateExcelNew2021New("CBLProgressTrackingdistrinctwise");
            }

        }

        if (ViewState["1"].ToString() == "18")
        {

            DataTable st = ViewState["SAC"] as DataTable;
            ExportToCSVFile(st, "CBLQualityAlertReport");

        }

        if (ViewState["1"].ToString() == "22")
        {

            DataTable st = ViewState["SAC"] as DataTable;
            MultipuExecl();

        }
    }
    public void MultipuExecl()
    {
        DataSet dtMain1 = ViewState["SAC"] as DataSet;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\test.xlsx");
        var ws = wb.Worksheet(1);
        var ws1 = wb.Worksheet(2);
        var ws2 = wb.Worksheet(3);
        var ws3 = wb.Worksheet(4);
        DataTable dt = dtMain1.Tables[0];
        DataTable dt1 = dtMain1.Tables[1];
        DataTable dt2 = dtMain1.Tables[2];
        DataTable dt3 = dtMain1.Tables[3];
        dt1.Columns.Remove("rowno");
        dt2.Columns.Remove("rowno");
        dt3.Columns.Remove("rowno");
        ws.Cell(2, 1).InsertData(dt.Rows);


        ws1.Cell(3, 1).InsertData(dt1.Rows);
        ws2.Cell(2, 1).InsertData(dt2.Rows);
        ws3.Cell(2, 1).InsertData(dt3.Rows);

        filepath = StartupPath + "\\Quality Alert Report_Summary " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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


    private void GenerateExcelNew2021Village(string FIleName)
    {
        try
        {





            DataTable dt = ViewState["SAC"] as DataTable;
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

                HttpContext.Current.Response.Write("<tr>");
                HttpContext.Current.Response.Write("<td colspan='60'  style='text-align:Center;border:.2pt solid windowtext;'></td>");

                HttpContext.Current.Response.Write("</tr>");

                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

                //HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                ////HttpContext.Current.Response.Write("<th class='header' rowspan='3'  style='" + HeaderStyle + "  width:2%;'>Master</th>");
                ////HttpContext.Current.Response.Write("<th class='header'  rowspan='3' style='" + HeaderStyle + "  width:2%;'>Planned Villages</th>");
                ////HttpContext.Current.Response.Write("<th class='header'  rowspan='3' style='" + HeaderStyle + "  width:2%;'> TB Leading Session having Smart Phone (Plan)</th>");

                ////  HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Camp Start Status</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Master</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>   Planned Villages</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> TB Leading Session having Smart Phone (Plan)</th>");

                //HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> Camp Start Status</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Completion Status</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Camp Regularity (PMS)</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='7' style='" + HeaderStyle + "  width:2%;'>#Children Registered</th>");

                //HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Attrition rate</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='18' style='" + HeaderStyle + "  width:2%;'># Villages where Sessions Not Conducted</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='18' style='" + HeaderStyle + "  width:2%;'>% Children Attendance by Session</th>");

                //HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>Average Attendance</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Registered Group</th>");
                //HttpContext.Current.Response.Write("</tr>");
                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";

                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                int columnscount = GV_DynamicGrid.HeaderRow.Cells.Count;

                for (int j = 0; j < columnscount; j++)
                {
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> " + GV_DynamicGrid.HeaderRow.Cells[j].Text + "</th>");
                }

                HttpContext.Current.Response.Write("</tr>");





                for (int i = 0; i < dt.Rows.Count; i++)
                {



                    HttpContext.Current.Response.Write("<tr>");


                    for (int c = 0; c < dt.Columns.Count; c++)
                    {


                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");


                    }

                }
                #region Row1



                #endregion


                HttpContext.Current.Response.Write("</tr>");
                //HttpContext.Current.Response.Write("<tr>");
                //DataTable dtSumm= ViewState["SACK"] as DataTable;

                //for (int i = 0; i < dtSumm.Rows.Count; i++)
                //{




                //    for (int c = 0; c < dtSumm.Columns.Count; c++)
                //    {
                //        if (c == 0 || c == 1 || c == 2)
                //        {
                //            if (c == 2)
                //            {
                //                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                //            }
                //            else
                //            {
                //                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'></td>");
                //            }
                //        }
                //        else
                //        {

                //            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dtSumm.Rows[i][c] + "</td>");

                //        }
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
    private void GenerateExcelNew2021(string FIleName)
    {
        try
        {





            DataTable dt = ViewState["SAC"] as DataTable;
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

                HttpContext.Current.Response.Write("<tr>");
                HttpContext.Current.Response.Write("<td colspan='60'  style='text-align:Center;border:.2pt solid windowtext;'></td>");

                HttpContext.Current.Response.Write("</tr>");

                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                //HttpContext.Current.Response.Write("<th class='header' rowspan='3'  style='" + HeaderStyle + "  width:2%;'>Master</th>");
                //HttpContext.Current.Response.Write("<th class='header'  rowspan='3' style='" + HeaderStyle + "  width:2%;'>Planned Villages</th>");
                //HttpContext.Current.Response.Write("<th class='header'  rowspan='3' style='" + HeaderStyle + "  width:2%;'> TB Leading Session having Smart Phone (Plan)</th>");

                //  HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Camp Start Status</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Master</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>   Planned Villages</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> TB Leading Session having Smart Phone (Plan)</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> Camp Start Status</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Completion Status</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Camp Regularity (PMS)</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='7' style='" + HeaderStyle + "  width:2%;'>#Children Registered</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Attrition rate</th>");
                if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
                {
                    if (Convert.ToInt32(ddlYear.SelectedValue) == 2022)
                    {
                        HttpContext.Current.Response.Write("<th class='header' colspan='20' style='" + HeaderStyle + "  width:2%;'># Villages where Sessions Not Conducted</th>");
                        HttpContext.Current.Response.Write("<th class='header' colspan='20' style='" + HeaderStyle + "  width:2%;'>% Children Attendance by Session</th>");

                    }
                    else
                    {
                        HttpContext.Current.Response.Write("<th class='header' colspan='18' style='" + HeaderStyle + "  width:2%;'># Villages where Sessions Not Conducted</th>");
                        HttpContext.Current.Response.Write("<th class='header' colspan='18' style='" + HeaderStyle + "  width:2%;'>% Children Attendance by Session</th>");

                    }

                }
                if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
                {
                    HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'># Villages where Sessions Not Conducted</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>% Children Attendance by Session</th>");

                }

                HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>Average Attendance</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Registered Group</th>");
                HttpContext.Current.Response.Write("</tr>");
                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";

                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                int columnscount = GV_DynamicGrid.HeaderRow.Cells.Count;

                for (int j = 0; j < columnscount; j++)
                {
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> " + GV_DynamicGrid.HeaderRow.Cells[j].Text + "</th>");
                }

                HttpContext.Current.Response.Write("</tr>");





                for (int i = 0; i < dt.Rows.Count; i++)
                {



                    HttpContext.Current.Response.Write("<tr>");


                    for (int c = 0; c < dt.Columns.Count; c++)
                    {


                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");


                    }

                }
                #region Row1



                #endregion


                HttpContext.Current.Response.Write("</tr>");
                //HttpContext.Current.Response.Write("<tr>");
                //DataTable dtSumm= ViewState["SACK"] as DataTable;

                //for (int i = 0; i < dtSumm.Rows.Count; i++)
                //{




                //    for (int c = 0; c < dtSumm.Columns.Count; c++)
                //    {
                //        if (c == 0 || c == 1 || c == 2)
                //        {
                //            if (c == 2)
                //            {
                //                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                //            }
                //            else
                //            {
                //                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'></td>");
                //            }
                //        }
                //        else
                //        {

                //            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dtSumm.Rows[i][c] + "</td>");

                //        }
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

    private void GenerateExcelNew2021New(string FIleName)
    {
        try
        {





            DataTable dt = ViewState["SAC"] as DataTable;
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

                HttpContext.Current.Response.Write("<tr>");
                HttpContext.Current.Response.Write("<td colspan='60'  style='text-align:Center;border:.2pt solid windowtext;'></td>");

                HttpContext.Current.Response.Write("</tr>");

                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                //HttpContext.Current.Response.Write("<th class='header' rowspan='3'  style='" + HeaderStyle + "  width:2%;'>Master</th>");
                //HttpContext.Current.Response.Write("<th class='header'  rowspan='3' style='" + HeaderStyle + "  width:2%;'>Planned Villages</th>");
                //HttpContext.Current.Response.Write("<th class='header'  rowspan='3' style='" + HeaderStyle + "  width:2%;'> TB Leading Session having Smart Phone (Plan)</th>");

                //  HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Camp Start Status</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='7' style='" + HeaderStyle + "  width:2%;'> Master</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>   Planned Villages</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> TB Leading Session having Smart Phone (Plan)</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> Camp Start Status</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Completion Status</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Camp Regularity (PMS)</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='7' style='" + HeaderStyle + "  width:2%;'>#Children Registered</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Attrition rate</th>");
                if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
                {
                    if (Convert.ToInt32(ddlYear.SelectedValue) == 2022)
                    {
                        HttpContext.Current.Response.Write("<th class='header' colspan='20' style='" + HeaderStyle + "  width:2%;'># Villages where Sessions Not Conducted</th>");
                        HttpContext.Current.Response.Write("<th class='header' colspan='20' style='" + HeaderStyle + "  width:2%;'>% Children Attendance by Session</th>");

                    }
                    else
                    {
                        HttpContext.Current.Response.Write("<th class='header' colspan='18' style='" + HeaderStyle + "  width:2%;'># Villages where Sessions Not Conducted</th>");
                        HttpContext.Current.Response.Write("<th class='header' colspan='18' style='" + HeaderStyle + "  width:2%;'>% Children Attendance by Session</th>");
                    }
                }
                if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
                {
                    HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'># Villages where Sessions Not Conducted</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>% Children Attendance by Session</th>");

                }

                HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>Average Attendance</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Registered Group</th>");
                HttpContext.Current.Response.Write("</tr>");
                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";

                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                int columnscount = GV_DynamicGrid.HeaderRow.Cells.Count;

                for (int j = 0; j < columnscount; j++)
                {
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> " + GV_DynamicGrid.HeaderRow.Cells[j].Text + "</th>");
                }

                HttpContext.Current.Response.Write("</tr>");





                for (int i = 0; i < dt.Rows.Count; i++)
                {



                    HttpContext.Current.Response.Write("<tr>");


                    for (int c = 0; c < dt.Columns.Count; c++)
                    {


                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");


                    }

                }
                #region Row1



                #endregion


                HttpContext.Current.Response.Write("</tr>");
                //HttpContext.Current.Response.Write("<tr>");
                //DataTable dtSumm= ViewState["SACK"] as DataTable;

                //for (int i = 0; i < dtSumm.Rows.Count; i++)
                //{




                //    for (int c = 0; c < dtSumm.Columns.Count; c++)
                //    {
                //        if (c == 0 || c == 1 || c == 2)
                //        {
                //            if (c == 2)
                //            {
                //                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                //            }
                //            else
                //            {
                //                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'></td>");
                //            }
                //        }
                //        else
                //        {

                //            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dtSumm.Rows[i][c] + "</td>");

                //        }
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






    public void getSACUpdateReport(Int32 Flag)
    {


        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";
        string schoolCodeAprove = "";



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
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }
        if (ddlGroup.SelectedIndex > 0)
        {

            conditions1 = conditions1 + " and  tblChildRegistration.CampID='" + ddlGroup.SelectedValue + "' ";
        }



        //dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);

        SqlParameter[] cmdParameters = new SqlParameter[]
        {


        new SqlParameter("@con", conditions1),
              new SqlParameter("@Year", ddlYear.SelectedValue),

        };
        dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptChildRegistration]", cmdParameters);
        ViewState["SAC"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {

            //DataTable newDataTable = dtMain.Clone();
            //DataTable dtn = dtMain.Clone();
            //for (int i = 0; i < 3; i++)
            //{
            //    dtn.ImportRow(dtMain.Rows[i]);
            //}
            if (dtMain.Rows.Count > 100)
            {
                ExportToCSVFile(dtMain, "ChildRegistration");
            }
            else
            {
                GV_DynamicGrid.Visible = true;
                GV_DynamicGrid.DataSource = dtMain;
                GV_DynamicGrid.DataBind();
            }


            //  GenerateExcelSIP(dtMain, aprove);

        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }

    }

    public void getChildRegistrationSchool(Int32 Flag)
    {


        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";
        string schoolCodeAprove = "";



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
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }

        //dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);

        SqlParameter[] cmdParameters = new SqlParameter[]
        {


        new SqlParameter("@con", conditions1),
              new SqlParameter("@Year", ddlYear.SelectedValue),

        };
        dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptChildRegistrationSchool]", cmdParameters);
        ViewState["SAC"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {

            //DataTable newDataTable = dtMain.Clone();
            //DataTable dtn = dtMain.Clone();
            //for (int i = 0; i < 3; i++)
            //{
            //    dtn.ImportRow(dtMain.Rows[i]);
            //}
            if (dtMain.Rows.Count > 100)
            {
                ExportToCSVFile(dtMain, "ChildRegistrationSchool");
            }
            else
            {
                GV_DynamicGrid.Visible = true;
                GV_DynamicGrid.DataSource = dtMain;
                GV_DynamicGrid.DataBind();
            }


            //  GenerateExcelSIP(dtMain, aprove);

        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }

    }

    public void getActivitySIPTargetvsSummaryGrade(Int32 Flag)
    {


        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";
        string schoolCodeAprove = "";





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
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }



        //dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);

        SqlParameter[] cmdParameters = new SqlParameter[]
        {


        new SqlParameter("@con", conditions1),

        };
        dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAttendensSummarGradWise]", cmdParameters);
        ViewState["SAC"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {
            //DataTable newDataTable = dtMain.Clone();
            //DataTable dtn = dtMain.Clone();
            //for (int i = 0; i < 3; i++)
            //{
            //    dtn.ImportRow(dtMain.Rows[i]);
            //}
            if (dtMain.Rows.Count > 0)
            {

                ExportToCSVFile(dtMain, "CBLLearningGrade");

            }




            //  GenerateExcelSIP(dtMain, aprove);

        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }

    }
    public void getActivitySIPTargetvsSummary(Int32 Flag)
    {


        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";
        string schoolCodeAprove = "";





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
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }

        if (ddlGroup.SelectedIndex > 0)
        {

            conditions1 = conditions1 + " and  CampID='" + ddlGroup.SelectedValue + "' ";
        }



        //dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);

        SqlParameter[] cmdParameters = new SqlParameter[]
        {


        new SqlParameter("@con", conditions1),
        new SqlParameter("@Year", ddlYear.SelectedValue),

        };
        dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAttendentSummary]", cmdParameters);
        ViewState["SAC"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {
            //DataTable newDataTable = dtMain.Clone();
            //DataTable dtn = dtMain.Clone();
            //for (int i = 0; i < 3; i++)
            //{
            //    dtn.ImportRow(dtMain.Rows[i]);
            //}
            if (dtMain.Rows.Count > 0)
            {
                if (dtMain.Rows.Count > 300)
                {
                    ExportToCSVFile(dtMain, "ChildAttendance");
                }
                else
                {
                    GV_DynamicGrid.Visible = true;
                    GV_DynamicGrid.DataSource = dtMain;
                    GV_DynamicGrid.DataBind();
                }
            }




            //  GenerateExcelSIP(dtMain, aprove);

        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }

    }

    public void getActivitySIPTargetvsAchv(Int32 Flag)
    {


        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";
        string schoolCodeAprove = "";





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
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }
        if (ddlGroup.SelectedIndex > 0)
        {

            conditions1 = conditions1 + " and  CampID='" + ddlGroup.SelectedValue + "' ";
        }




        //dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);

        SqlParameter[] cmdParameters = new SqlParameter[]
        {


        new SqlParameter("@con", conditions1),
        new SqlParameter("@Year", ddlYear.SelectedValue),

        };
        dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptChildAttendence]", cmdParameters);
        ViewState["SAC"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {
            //DataTable newDataTable = dtMain.Clone();
            //DataTable dtn = dtMain.Clone();
            //for (int i = 0; i < 3; i++)
            //{
            //    dtn.ImportRow(dtMain.Rows[i]);
            //}
            if (dtMain.Rows.Count > 0)
            {
                if (dtMain.Rows.Count > 300)
                {
                    ExportToCSVFile(dtMain, "ChildAttendance");
                }
                else
                {
                    GV_DynamicGrid.Visible = true;
                    GV_DynamicGrid.DataSource = dtMain;
                    GV_DynamicGrid.DataBind();
                }
            }




            //  GenerateExcelSIP(dtMain, aprove);

        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }

    }

    public void ChildAttendanceSchool(Int32 Flag)
    {


        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";
        string schoolCodeAprove = "";





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
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }





        //dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);

        SqlParameter[] cmdParameters = new SqlParameter[]
        {


        new SqlParameter("@con", conditions1),
        new SqlParameter("@Year", ddlYear.SelectedValue),

        };
        dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptChildAttendenceSchool]", cmdParameters);
        ViewState["SAC"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {
            //DataTable newDataTable = dtMain.Clone();
            //DataTable dtn = dtMain.Clone();
            //for (int i = 0; i < 3; i++)
            //{
            //    dtn.ImportRow(dtMain.Rows[i]);
            //}
            if (dtMain.Rows.Count > 0)
            {
                if (dtMain.Rows.Count > 300)
                {
                    ExportToCSVFile(dtMain, "ChildAttendanceSchool");
                }
                else
                {
                    GV_DynamicGrid.Visible = true;
                    GV_DynamicGrid.DataSource = dtMain;
                    GV_DynamicGrid.DataBind();
                }
            }




            //  GenerateExcelSIP(dtMain, aprove);

        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }

    }
    public void getActivityVisitorsSchool(Int32 Flag)
    {


        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";
        string schoolCodeAprove = "";





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
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }


        if (ddlGroup.SelectedIndex > 0)
        {

            conditions1 = conditions1 + " and  tblChildAttendance.CampID='" + ddlGroup.SelectedValue + "' ";
        }



        //dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);

        SqlParameter[] cmdParameters = new SqlParameter[]
        {


        new SqlParameter("@con", conditions1),

        };
        dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptVisitorsSchool]", cmdParameters);
        ViewState["SAC"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {
            //DataTable newDataTable = dtMain.Clone();
            //DataTable dtn = dtMain.Clone();
            //for (int i = 0; i < 3; i++)
            //{
            //    dtn.ImportRow(dtMain.Rows[i]);
            //}
            if (dtMain.Rows.Count > 500)
            {
                ExportToCSVFile(dtMain, "SBLVisitors");
            }
            else
            {
                GV_DynamicGrid.Visible = true;
                GV_DynamicGrid.DataSource = dtMain;
                GV_DynamicGrid.DataBind();

            }

            //  GenerateExcelSIP(dtMain, aprove);

        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }

    }


    public void getActivityVisitors(Int32 Flag)
    {


        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";
        string schoolCodeAprove = "";





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
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }


        if (ddlGroup.SelectedIndex > 0)
        {

            conditions1 = conditions1 + " and  tblChildAttendance.CampID='" + ddlGroup.SelectedValue + "' ";
        }



        //dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);

        SqlParameter[] cmdParameters = new SqlParameter[]
        {


        new SqlParameter("@con", conditions1),

        };
        dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptVisitors]", cmdParameters);
        ViewState["SAC"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {
            //DataTable newDataTable = dtMain.Clone();
            //DataTable dtn = dtMain.Clone();
            //for (int i = 0; i < 3; i++)
            //{
            //    dtn.ImportRow(dtMain.Rows[i]);
            //}
            if (dtMain.Rows.Count > 500)
            {
                ExportToCSVFile(dtMain, "Visitors");
            }
            else
            {
                GV_DynamicGrid.Visible = true;
                GV_DynamicGrid.DataSource = dtMain;
                GV_DynamicGrid.DataBind();

            }

            //  GenerateExcelSIP(dtMain, aprove);

        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }

    }


    public void getActivityVillagelevel(Int32 Flag)
    {


        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";
        string schoolCodeAprove = "";





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

        if (Convert.ToInt32(ddlLevel.SelectedValue) == 2)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
            }
            if (ddlStatecode.Length > 0)
            {
                conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
            }
            if (ddlDistrict.Length > 0)
            {
                conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
            }
            if (ddlBlock.Length > 0)
            {
                conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
            }
            if (ddlPhan.Length > 0)
            {
                conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
            }
            if (ddlVillage.Length > 0)
            {
                conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
            }


            if (ddlGroup.SelectedIndex > 0)
            {

                conditions1 = conditions1 + " and  TempCampwiseSumayReport.CampID='" + ddlGroup.SelectedValue + "' ";
            }
        }
        if (Convert.ToInt32(ddlLevel.SelectedValue) == 1)

        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions1 = conditions1 + "    D.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
            }
            if (ddlStatecode.Length > 0)
            {
                conditions1 = conditions1 + " and    D.StateCode in(" + ddlStatecode + ") ";
            }
            if (ddlDistrict.Length > 0)
            {
                conditions1 = conditions1 + " and D.DistrictCode in(" + ddlDistrict + ") ";
            }

            if (ddlGroup.SelectedIndex > 0)
            {

                conditions1 = conditions1 + " and  TempCampwiseDistrictSumayReport.Camp='" + ddlGroup.SelectedValue + "' ";
            }
        }

        if (Convert.ToInt32(ddlLevel.SelectedValue) == 4)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions1 = conditions1 + "   mstCluster.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
            }
            if (ddlStatecode.Length > 0)
            {
                conditions1 = conditions1 + " and    mstCluster.StateCode in(" + ddlStatecode + ") ";
            }
            if (ddlDistrict.Length > 0)
            {
                conditions1 = conditions1 + " and mstCluster.DistrictCode in(" + ddlDistrict + ") ";
            }

            if (ddlGroup.SelectedIndex > 0)
            {

                conditions1 = conditions1 + " and  TempCampwiseClusterSumayReport.Camp='" + ddlGroup.SelectedValue + "' ";
            }
        }

        //dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);
        if (Convert.ToInt32(ddlLevel.SelectedValue) == 2)
        {

            if (Convert.ToInt32(ddlYear.SelectedValue) == 2022)
            {
                SqlParameter[] cmdParameters = new SqlParameter[]
                {


                new SqlParameter("@con", conditions1),
                    new SqlParameter("@Flag", ddlLevel.SelectedValue),
                          new SqlParameter("@Camp", ddlGroup.SelectedValue),

                };
                dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptvillageLevelCampSummarNew202123]", cmdParameters);
            }
            else
            {
                SqlParameter[] cmdParameters = new SqlParameter[]
                 {


                new SqlParameter("@con", conditions1),
                    new SqlParameter("@Flag", ddlLevel.SelectedValue),
                          new SqlParameter("@Camp", ddlGroup.SelectedValue),

                 };
                dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptvillageLevelCampSummarNew202122]", cmdParameters);


            }
            ViewState["SAC"] = dtMain;

            if (dtMain.Rows.Count > 0)
            {
                //DataTable newDataTable = dtMain.Clone();
                //DataTable dtn = dtMain.Clone();
                //for (int i = 0; i < 3; i++)
                //{
                //    dtn.ImportRow(dtMain.Rows[i]);
                //}
                if (dtMain.Rows.Count > 500)
                {
                    btnImport_Click(Button3, null);
                }
                else
                {
                    GV_DynamicGrid.Visible = true;
                    GV_DynamicGrid.DataSource = dtMain;
                    GV_DynamicGrid.DataBind();

                }
            }
            else
            {
                GV_DynamicGrid.Visible = true;
                GV_DynamicGrid.DataSource = null;
                GV_DynamicGrid.DataBind();

            }
        }


        if (Convert.ToInt32(ddlLevel.SelectedValue) == 1)
        {
            DataSet dts = null;
            if (Convert.ToInt32(ddlYear.SelectedValue) == 2022)
            {
                SqlParameter[] cmdParameters = new SqlParameter[]
        {


        new SqlParameter("@con", conditions1),
            new SqlParameter("@Flag", ddlLevel.SelectedValue),

            new SqlParameter("@Camp", ddlGroup.SelectedValue),
        };
                dts = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptvillageLevelCampSummarNew202123]", cmdParameters);
            }
            else
            {
                SqlParameter[] cmdParameters = new SqlParameter[]
        {


        new SqlParameter("@con", conditions1),
            new SqlParameter("@Flag", ddlLevel.SelectedValue),

            new SqlParameter("@Camp", ddlGroup.SelectedValue),
        };
                dts = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptvillageLevelCampSummarNew202122]", cmdParameters);
            }

            ViewState["SAC"] = dts.Tables[0];
            ViewState["SACK"] = dts.Tables[1];

            if (dts.Tables[0].Rows.Count > 0)
            {
                //DataTable newDataTable = dtMain.Clone();
                //DataTable dtn = dtMain.Clone();
                //for (int i = 0; i < 3; i++)
                //{
                //    dtn.ImportRow(dtMain.Rows[i]);
                //}
                if (dts.Tables[0].Rows.Count > 500)
                {
                    btnImport_Click(Button3, null);
                }
                else
                {
                    GV_DynamicGrid.Visible = true;
                    GV_DynamicGrid.DataSource = dts.Tables[0];
                    GV_DynamicGrid.DataBind();

                }
            }
            else
            {
                GV_DynamicGrid.Visible = true;
                GV_DynamicGrid.DataSource = null;
                GV_DynamicGrid.DataBind();

            }
        }


        if (Convert.ToInt32(ddlLevel.SelectedValue) == 4)
        {
            DataSet dts = null;
            if (Convert.ToInt32(ddlYear.SelectedValue) == 2022)
            {

                SqlParameter[] cmdParameters = new SqlParameter[]
                   {


                new SqlParameter("@con", conditions1),
                    new SqlParameter("@Flag", ddlLevel.SelectedValue),

                    new SqlParameter("@Camp", ddlGroup.SelectedValue),
                   };

                dts = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptvillageLevelCampSummarNew202123]", cmdParameters);
            }
            else
            {
                SqlParameter[] cmdParameters = new SqlParameter[]
                {


                new SqlParameter("@con", conditions1),
                    new SqlParameter("@Flag", ddlLevel.SelectedValue),

                    new SqlParameter("@Camp", ddlGroup.SelectedValue),
                };

                dts = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptvillageLevelCampSummarNew202122]", cmdParameters);

            }
            ViewState["SAC"] = dts.Tables[0];
            ViewState["SACK"] = dts.Tables[1];

            if (dts.Tables[0].Rows.Count > 0)
            {
                //DataTable newDataTable = dtMain.Clone();
                //DataTable dtn = dtMain.Clone();
                //for (int i = 0; i < 3; i++)
                //{
                //    dtn.ImportRow(dtMain.Rows[i]);
                //}
                if (dts.Tables[0].Rows.Count > 50000)
                {
                    btnImport_Click(Button3, null);
                }
                else
                {
                    GV_DynamicGrid.Visible = true;
                    GV_DynamicGrid.DataSource = dts.Tables[0];
                    GV_DynamicGrid.DataBind();

                }
            }
            else
            {
                GV_DynamicGrid.Visible = true;
                GV_DynamicGrid.DataSource = null;
                GV_DynamicGrid.DataBind();

            }
        }

        //  GenerateExcelSIP(dtMain, aprove);


    }


    public void getActivityVillagelevelMulti(Int32 Flag)
    {


        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";
        string schoolCodeAprove = "";





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
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }




        if (ddlGroup.SelectedIndex > 0)
        {

            conditions1 = conditions1 + " and   TempCampwiseSumayReport.CampID='" + ddlGroup.SelectedValue + "' ";
        }



        //dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);
        DataSet dtMain1 = null;
        if (Convert.ToInt32(ddlYear.SelectedValue) == 2021)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
        {


        new SqlParameter("@con", conditions1),
            new SqlParameter("@ComNo", ddlGroup.SelectedValue),


        };
            dtMain1 = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptSummarReportNew2020]", cmdParameters);
        }
        else
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
       {


        new SqlParameter("@con", conditions1),
            new SqlParameter("@ComNo", ddlGroup.SelectedValue),


       };
            dtMain1 = GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptSummarReportNew]", cmdParameters);
        }
        ViewState["SAC"] = dtMain1;


        if (dtMain1.Tables[0].Rows.Count > 0)
        {
            //DataTable newDataTable = dtMain.Clone();
            //DataTable dtn = dtMain.Clone();
            //for (int i = 0; i < 3; i++)
            //{
            //    dtn.ImportRow(dtMain.Rows[i]);
            //}
            ReportDownload("CBL Quality Alert Report");
            if (dtMain1.Tables[0].Rows.Count > 100)
            {
                btnImport_Click(Button3, null);
            }
            else
            {
                GV_DynamicGrid.Visible = true;
                GV_DynamicGrid.DataSource = dtMain1.Tables[0];
                GV_DynamicGrid.DataBind();

            }

        }
        else
        {
            GV_DynamicGrid.Visible = true;
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();

        }


        //  GenerateExcelSIP(dtMain, aprove);


    }
    private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParameters)
    {
        if (conn.State != ConnectionState.Open)
            conn.Open();
        cmd.Connection = conn;

        cmd.CommandType = cmdType;
        cmd.CommandText = cmdText;
        cmd.CommandTimeout = 0;
        if (cmdParameters != null)
        {
            foreach (SqlParameter param in cmdParameters)
            {
                cmd.Parameters.Add(param);
            }
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
    public void ReportDownload(string Rname)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
         {
        new SqlParameter("@fname", Rname),
            new SqlParameter("@Username", Convert.ToString(Session["username"])),


       };
        int icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[InsertDownloadReport]", cmdParameters);
    }
    public void getActivityCPLlevel(Int32 Flag)
    {


        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";
        string schoolCodeAprove = "";





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
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }


        if (ddlGroup.SelectedIndex > 0)
        {

            conditions1 = conditions1 + " and  TempCampwiseSumayReport.CampID='" + ddlGroup.SelectedValue + "' ";
        }



        //dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);

        SqlParameter[] cmdParameters = new SqlParameter[]
        {


        new SqlParameter("@con", conditions1),
            new SqlParameter("@Flag", "3"),


        };
        dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptvillageLevelCampSummar]", cmdParameters);
        ViewState["SAC"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {
            //DataTable newDataTable = dtMain.Clone();
            //DataTable dtn = dtMain.Clone();
            //for (int i = 0; i < 3; i++)
            //{
            //    dtn.ImportRow(dtMain.Rows[i]);
            //}
            if (dtMain.Rows.Count > 500)
            {
                btnImport_Click(Button3, null);
            }
            else
            {
                GV_DynamicGrid.Visible = true;
                GV_DynamicGrid.DataSource = dtMain;
                GV_DynamicGrid.DataBind();

            }

            //  GenerateExcelSIP(dtMain, aprove);

        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }

    }

}