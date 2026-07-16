using Ionic.Zip;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;



public partial class frmLogSummary : System.Web.UI.Page
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
                    txtFromDate.Text = (DateTime.Now).AddDays(-6).ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");




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
    public void AlllStateCode()
    {
        conditions = "";
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
        conditions = "";
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

        //ddlPanchayat.DataSource = dtDistrict;
        //ddlPanchayat.DataTextField = "PanchayatName";
        //ddlPanchayat.DataValueField = "PanchayatCode";
        //ddlPanchayat.DataBind();

        //// objComman.BindDLL("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "--All--");


        //chkVillage.Items.Clear();

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

        //foreach (ListItem item in ddlPanchayat.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlPhan += "'" + item.Value + "'" + ",";


        //    }
        //}

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

        //chkVillage.DataSource = dtDistrict;
        //chkVillage.DataTextField = "VillageName";
        //chkVillage.DataValueField = "VillageCode";
        //chkVillage.DataBind();


    }

    protected void LnkAnnualPlan_OnClick(object sender, EventArgs e)
    {


        ViewState["1"] = 9;


        getLogReport(1);


    }
    protected void LnkAnnualPlan_OnClick2(object sender, EventArgs e)
    {


        ViewState["1"] = 9;


        getLogReportMobbile(1);


    }
    protected void LnkAnnualPlan_OnClick3(object sender, EventArgs e)
    {


        ViewState["1"] = 9;


        getLogReportContact(1);


    }
    protected void LnkAnnualPlan_OnClick4(object sender, EventArgs e)
    {


        ViewState["1"] = 9;


        getLogReportMobbileBO(1);


    }
    public void getLogReport(Int32 Flag)
    {


        string conditions1 = "";
        DataTable dtMain;




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


        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    MstUser.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and MstUser.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and MstUser.BlockCode in(" + ddlBlock + ") ";
        }


        if (txtUserID.Text != "")
        {

            conditions1 = conditions1 + " and  MstUser.Username ='" + txtUserID.Text + "' ";
        }
        if (txtUserName.Text != "")
        {

            conditions1 = conditions1 + " and  Firstname ='" + txtUserName.Text + "' ";
        }

        conditions1 = conditions1 + " and  [LogDate] >'" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + "' and [LogDate] <='" + Convert.ToDateTime(txtToDate.Text).AddDays(1).ToString("yyyy-MM-dd") + "' ";

        //dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);

        SqlParameter[] cmdParameters = new SqlParameter[]
        {


        new SqlParameter("@Con", conditions1),

        };
        dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptLoginHistory]", cmdParameters);
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
                ExportToCSVFile(dtMain, "Logsummary");
            }
            else
            {

            }


            //  GenerateExcelSIP(dtMain, aprove);

        }
        else
        {

        }

    }

    protected void Lnkfc_OnClick(object sender, EventArgs e)
    {
        getLogReportFCPlanner(1);
    }
    public void getLogReportFCPlanner(Int32 Flag)
    {


        string conditions1 = "";
        DataTable dtMain;




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


        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    MstUser.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and MstUser.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and MstUser.BlockCode in(" + ddlBlock + ") ";
        }


        if (txtUserID.Text != "")
        {

            conditions1 = conditions1 + " and  MstUser.Username ='" + txtUserID.Text + "' ";
        }
        if (txtUserName.Text != "")
        {

            conditions1 = conditions1 + " and  Firstname ='" + txtUserName.Text + "' ";
        }

        conditions1 = conditions1 + " and  [CreatedOn] >'" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + "' and [CreatedOn] <='" + Convert.ToDateTime(txtToDate.Text).AddDays(1).ToString("yyyy-MM-dd") + "' ";

        //dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);

        SqlParameter[] cmdParameters = new SqlParameter[]
        {


        new SqlParameter("@Con", conditions1),

        };
        dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptMoibleReportHistoryFCPlanner]", cmdParameters);
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
                ExportToCSVFile(dtMain, "FCWeeklyPlan");
            }
            else
            {

            }


            //  GenerateExcelSIP(dtMain, aprove);

        }
        else
        {

        }

    }
    public void getLogReportContact(Int32 Flag)
    {


        string conditions1 = "";
        DataTable dtMain;




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


        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    MstUser.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and MstUser.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and MstUser.BlockCode in(" + ddlBlock + ") ";
        }


        if (txtUserID.Text != "")
        {

            conditions1 = conditions1 + " and  MstUser.Username ='" + txtUserID.Text + "' ";
        }
        if (txtUserName.Text != "")
        {

            conditions1 = conditions1 + " and  Firstname ='" + txtUserName.Text + "' ";
        }

        conditions1 = conditions1 + " and  [CreatedOn] >'" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + "' and [CreatedOn] <='" + Convert.ToDateTime(txtToDate.Text).AddDays(1).ToString("yyyy-MM-dd") + "' ";

        //dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);

        SqlParameter[] cmdParameters = new SqlParameter[]
        {


        new SqlParameter("@Con", conditions1),

        };
        dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptMoibleReportHistoryContact]", cmdParameters);
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
                ExportToCSVFile(dtMain, "Contactsummarylog");
            }
            else
            {

            }


            //  GenerateExcelSIP(dtMain, aprove);

        }
        else
        {

        }

    }
    public void getLogReportMobbile(Int32 Flag)
    {


        string conditions1 = "";
        DataTable dtMain;




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


        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    MstUser.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and MstUser.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and MstUser.BlockCode in(" + ddlBlock + ") ";
        }


        if (txtUserID.Text != "")
        {

            conditions1 = conditions1 + " and  MstUser.Username ='" + txtUserID.Text + "' ";
        }
        if (txtUserName.Text != "")
        {

            conditions1 = conditions1 + " and  Firstname ='" + txtUserName.Text + "' ";
        }

        conditions1 = conditions1 + " and  [CreatedOn] >'" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + "' and [CreatedOn] <='" + Convert.ToDateTime(txtToDate.Text).AddDays(1).ToString("yyyy-MM-dd") + "' ";

        //dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);

        SqlParameter[] cmdParameters = new SqlParameter[]
        {


        new SqlParameter("@Con", conditions1),

        };
        dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptMoibleReportHistory]", cmdParameters);
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
                ExportToCSVFile(dtMain, "Enrolment&CVsummarylog");
            }
            else
            {

            }


            //  GenerateExcelSIP(dtMain, aprove);

        }
        else
        {

        }

    }

    public void getLogReportMobbileBO(Int32 Flag)
    {


        string conditions1 = "";
        DataTable dtMain;




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


        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    MstUser.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and MstUser.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and MstUser.BlockCode in(" + ddlBlock + ") ";
        }


        if (txtUserID.Text != "")
        {

            conditions1 = conditions1 + " and  MstUser.Username ='" + txtUserID.Text + "' ";
        }
        if (txtUserName.Text != "")
        {

            conditions1 = conditions1 + " and  Firstname ='" + txtUserName.Text + "' ";
        }

        conditions1 = conditions1 + " and  [CreatedOn] >'" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + "' and [CreatedOn] <='" + Convert.ToDateTime(txtToDate.Text).AddDays(1).ToString("yyyy-MM-dd") + "' ";

        //dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);

        SqlParameter[] cmdParameters = new SqlParameter[]
        {


        new SqlParameter("@Con", conditions1),

        };
        dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptMoibleReportHistoryBO]", cmdParameters);
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
                ExportToCSVFile(dtMain, "Enrolment&CVsummarylogBO");
            }
            else
            {

            }


            //  GenerateExcelSIP(dtMain, aprove);

        }
        else
        {

        }

    }


    protected void LnkSBLChild(object sender, EventArgs e)
    {


        ViewState["1"] = 500;



    }
    protected void LnkAnnualPlanFC_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 10;


        getLogRegReport(1);




    }
    public void getLogRegReport(Int32 Flag)
    {


        string conditions1 = "";
        string conditions2 = "";
        DataTable dtMain;




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


        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    MstUser.StateCode in(" + ddlStatecode + ") ";
            conditions2 = conditions2 + " and    MstUserHistory.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and MstUser.DistrictCode in(" + ddlDistrict + ") ";
            conditions2 = conditions2 + " and MstUserHistory.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and MstUser.BlockCode in(" + ddlBlock + ") ";
            conditions2 = conditions2 + " and MstUserHistory.BlockCode in(" + ddlBlock + ") ";
        }


        if (txtUserID.Text != "")
        {

            conditions1 = conditions1 + " and  MstUser.Username ='" + txtUserID.Text + "' ";
            conditions2 = conditions2 + " and  MstUserHistory.Username ='" + txtUserID.Text + "' ";
        }
        if (txtUserName.Text != "")
        {

            conditions1 = conditions1 + " and  Firstname ='" + txtUserName.Text + "' ";
            conditions2 = conditions2 + " and  Firstname ='" + txtUserName.Text + "' ";
        }

        conditions1 = conditions1 + " and (  [UserCreateDate] >'" + Convert.ToDateTime(txtFromDate.Text).AddDays(-1).ToString("yyyy-MM-dd") + "' and [UserCreateDate] <='" + Convert.ToDateTime(txtToDate.Text).AddDays(1).ToString("yyyy-MM-dd") + "' or [UserModifyDate] >'" + Convert.ToDateTime(txtFromDate.Text).AddDays(-1).ToString("yyyy-MM-dd") + "' and [UserModifyDate] <='" + Convert.ToDateTime(txtToDate.Text).AddDays(1).ToString("yyyy-MM-dd") + "' )";

        conditions2 = conditions2 + " and  [CopyDate] >'" + Convert.ToDateTime(txtFromDate.Text).AddDays(-1).ToString("yyyy-MM-dd") + "' and [CopyDate] <='" + Convert.ToDateTime(txtToDate.Text).AddDays(1).ToString("yyyy-MM-dd") + "' ";

        //dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);

        SqlParameter[] cmdParameters = new SqlParameter[]
        {


        new SqlParameter("@Con", conditions1),
           new SqlParameter("@Con2", conditions2),

        };
        dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptUserRegHistory]", cmdParameters);
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
                ExportToCSVFile(dtMain, "UserRegistrationTracker");
            }
            else
            {

            }


            //  GenerateExcelSIP(dtMain, aprove);

        }
        else
        {

        }

    }
    protected void LnkSBLAtten(object sender, EventArgs e)
    {
        ViewState["1"] = 501;




    }

    protected void LnkAnnualPl_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 15;

        getContactsummary(1, "Contact Summary");


    }
    protected void LnkAnnualPl1_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 15;

        getContactsummary(2, "Contact Quality Alert");


    }
    protected void LnkAgis_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 15;

        string conditions1 = "";
        string conditions2 = "";
        DataTable dtMain;
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


        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    MstUser.StateCode in(" + ddlStatecode + ") ";
            conditions2 = conditions2 + " and    MstUser.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and MstUser.DistrictCode in(" + ddlDistrict + ") ";
            conditions2 = conditions2 + " and MstUser.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and MstUser.BlockCode in(" + ddlBlock + ") ";
            conditions2 = conditions2 + " and MstUser.BlockCode in(" + ddlBlock + ") ";
        }


        if (txtUserID.Text != "")
        {

            conditions1 = conditions1 + " and  MstUser.Username ='" + txtUserID.Text + "' ";
            conditions2 = conditions2 + " and  MstUser.Username ='" + txtUserID.Text + "' ";
        }
        if (txtUserName.Text != "")
        {

            conditions1 = conditions1 + " and  Firstname ='" + txtUserName.Text + "' ";
            conditions2 = conditions2 + " and  Firstname ='" + txtUserName.Text + "' ";
        }

        conditions1 = conditions1 + " and  [LogDate] >'" + Convert.ToDateTime(txtFromDate.Text).AddDays(-1).ToString("yyyy-MM-dd") + "' and [LogDate] <='" + Convert.ToDateTime(txtToDate.Text).AddDays(1).ToString("yyyy-MM-dd") + "' ";

        conditions2 = conditions2 + " and  [Ddate] >'" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + "' and [Ddate] <='" + Convert.ToDateTime(txtToDate.Text).AddDays(1).ToString("yyyy-MM-dd") + "' ";
        conditions2 = conditions2 + " and ModuleName in('Heat Map','Coverage','Cluster')";
        //dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);

        SqlParameter[] cmdParameters = new SqlParameter[]
        {
           new SqlParameter("@Con", conditions1),
           new SqlParameter("@Con1", conditions2),
        };
        dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptDownloadHistory]", cmdParameters);
        ViewState["SAC"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {
            if (dtMain.Rows.Count > 0)
            {

                ExportToCSVFile(dtMain, "GISSummary");

            }
        }
    }
    protected void LnkAnnualPl2_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 15;

        getContactsummary(3, "Contact Detail Report");


    }
    protected void LnkAnnualPl3_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 15;

        getContactsummary(4, "Enrollment Target Raw Data");


    }
    protected void LnkAnnualPl4_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 15;

        getContactsummary(5, "Contact Status Report");


    }
    protected void LnkAnnualPl5_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 15;

        getContactsummary(6, "Activity-School Raw Data");


    }
    protected void LnkAnnualPl6_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 15;

        getContactsummary(7, "Activity-Village Raw Data");

    }
    protected void LnkAnnualPl7_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 15;

        getContactsummary(8, "Activity-Village GSS Raw Data");

    }
    protected void LnkAnnualPl8_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 15;

        getContactsummary(9, "Activity-Village MM Raw Data");

    }
    protected void LnkAnnualPl9_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 15;

        getContactsummary(10, "Approve Status");

    }
    protected void LnkAnnualPl10_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 15;

        getContactsummary(11, "Balsabha- Detail");

    }
    protected void LnkAnnualPl11_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 15;

        getContactsummary(12, "Balsabha- Child Registration");

    }
    protected void LnkAnnualPl12_OnClick(object sender, EventArgs e)
    {


        getContactsummary(13, "LSE Attendance Detail");

    }
    protected void LnkAnnualPl13_OnClick(object sender, EventArgs e)
    {

        getContactsummary(14, "SIP Target vs Achv");

    }
    protected void LnkAnnualPl14_OnClick(object sender, EventArgs e)
    {


        getContactsummary(15, "SAC Report");

    }
    protected void LnkAnnualPl15_OnClick(object sender, EventArgs e)
    {

        getContactsummary(16, "GKP Child Registration");

    }
    protected void LnkAnnualPl16_OnClick(object sender, EventArgs e)
    {

        getContactsummary(17, "GKP Child Registration Class 2");

    }
    protected void LnkAnnualPl17_OnClick(object sender, EventArgs e)
    {

        getContactsummary(18, "GKP Child Attendence");

    }
    protected void LnkAnnualPl18_OnClick(object sender, EventArgs e)
    {

        getContactsummary(19, "GKP Assessment");

    }
    protected void LnkAnnualPl19_OnClick(object sender, EventArgs e)
    {

        getContactsummary(20, "GKP Assessment Class 2");

    }
    protected void LnkAnnualPl120_OnClick(object sender, EventArgs e)
    {

        getContactsummary(21, "GKP Summary");

    }
    protected void LnkAnnualPl121_OnClick(object sender, EventArgs e)
    {

        getContactsummary(22, "GKP Assessment Summary");

    }
    protected void LnkAnnualPl122_OnClick(object sender, EventArgs e)
    {

        getContactsummary(23, "GKP Quality Alert");

    }
    protected void LnkAnnualPl123_OnClick(object sender, EventArgs e)
    {

        getContactsummary(24, "Door to Door Survey");

    }
    protected void LnkAnnualPl124_OnClick(object sender, EventArgs e)
    {

        getContactsummary(25, "Annual Plan Target Sheet");

    }
    protected void LnkAnnualPl125_OnClick(object sender, EventArgs e)
    {

        getContactsummary(26, "Annual Plan Summary");

    }
    protected void LnkAnnualPl126_OnClick(object sender, EventArgs e)
    {

        getContactsummary(27, "Enrollment Target Raw Data");

    }
    protected void LnkAnnualPl127_OnClick(object sender, EventArgs e)
    {

        getContactsummary(28, "Approval Process Report");

    }
    protected void LnkAnnualPl128_OnClick(object sender, EventArgs e)
    {

        getContactsummary(29, "Annual Plan Quality Alert");

    }
    protected void LnkAnnualPl74_OnClick(object sender, EventArgs e)
    {

        getContactsummary(30, "Enrolment Details");

    }
    protected void LnkAnnualP84_OnClick(object sender, EventArgs e)
    {

        getContactsummary(31, "Enrolment Quality Alert");

    }
    protected void LnkAnnualP94_OnClick(object sender, EventArgs e)
    {
        getContactsummary(32, "Enrolment Summary");
    }

    protected void LnkEnrollementHistory_OnClick(object sender, EventArgs e)
    {
        getEnrollenmentHistory(32, "Enrolment Tracker");
    }
    public void getEnrollenmentHistory(Int32 Flag, string Name)
    {
        string conditions1 = "";
        string conditions2 = "";
        DataTable dtMain;
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
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    S.StateCode in(" + ddlStatecode + ") ";
            conditions2 = conditions2 + " and    S.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and D.DistrictCode in(" + ddlDistrict + ") ";
            conditions2 = conditions2 + " and D.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and B.BlockCode in(" + ddlBlock + ") ";
            conditions2 = conditions2 + " and B.BlockCode in(" + ddlBlock + ") ";
        }

        conditions2 = conditions2 + " and  [UpdatedOn] >'" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + "' and [UpdatedOn] <='" + Convert.ToDateTime(txtToDate.Text).AddDays(1).ToString("yyyy-MM-dd") + "' ";

        SqlParameter[] cmdParameters = new SqlParameter[]
        {
           new SqlParameter("@Con", conditions1),
           new SqlParameter("@Con1", conditions2),
        };
        dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptDownloadEnrollementTracker]", cmdParameters);
        ViewState["SAC"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {
            if (dtMain.Rows.Count > 0)
            {
                ExportToCSVFile(dtMain, "Enrolment Tracker");
            }
        }
        else
        {

        }
    }
    public void getContactsummary(Int32 Flag, string Name)
    {
        string conditions1 = "";
        string conditions2 = "";
        DataTable dtMain;
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


        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    MstUser.StateCode in(" + ddlStatecode + ") ";
            conditions2 = conditions2 + " and    MstUser.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and MstUser.DistrictCode in(" + ddlDistrict + ") ";
            conditions2 = conditions2 + " and MstUser.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and MstUser.BlockCode in(" + ddlBlock + ") ";
            conditions2 = conditions2 + " and MstUser.BlockCode in(" + ddlBlock + ") ";
        }


        if (txtUserID.Text != "")
        {

            conditions1 = conditions1 + " and  MstUser.Username ='" + txtUserID.Text + "' ";
            conditions2 = conditions2 + " and  MstUser.Username ='" + txtUserID.Text + "' ";
        }
        if (txtUserName.Text != "")
        {

            conditions1 = conditions1 + " and  Firstname ='" + txtUserName.Text + "' ";
            conditions2 = conditions2 + " and  Firstname ='" + txtUserName.Text + "' ";
        }

        conditions1 = conditions1 + " and  [LogDate] >'" + Convert.ToDateTime(txtFromDate.Text).AddDays(-1).ToString("yyyy-MM-dd") + "' and [LogDate] <='" + Convert.ToDateTime(txtToDate.Text).AddDays(1).ToString("yyyy-MM-dd") + "' ";

        conditions2 = conditions2 + " and  [Ddate] >'" + Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd") + "' and [Ddate] <='" + Convert.ToDateTime(txtToDate.Text).AddDays(1).ToString("yyyy-MM-dd") + "' ";
        conditions2 = conditions2 + " and ReportName ='" + Name + " '";
        //dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);

        SqlParameter[] cmdParameters = new SqlParameter[]
        {
           new SqlParameter("@Con", conditions1),
           new SqlParameter("@Con1", conditions2),
        };
        dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptDownloadHistory]", cmdParameters);
        ViewState["SAC"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {
            if (dtMain.Rows.Count > 0)
            {
                if (Flag == 1)
                {
                    ExportToCSVFile(dtMain, "ContactSummaryDownloadHistroy");
                }
                if (Flag == 2)
                {
                    ExportToCSVFile(dtMain, "ContactQualityAlertDownloadHistroy");
                }
                if (Flag == 3)
                {
                    ExportToCSVFile(dtMain, "ContactDetailReportDownloadHistroy");
                }
                if (Flag == 4)
                {
                    ExportToCSVFile(dtMain, "EnrollmentTargetRawDataDownloadHistroy");
                }
                if (Flag == 5)
                {
                    ExportToCSVFile(dtMain, "ContactStatusReportDownloadHistroy");
                }
                if (Flag == 6)
                {
                    ExportToCSVFile(dtMain, "ActivitySchoolRawDataDownloadHistroy");
                }
                if (Flag == 7)
                {
                    ExportToCSVFile(dtMain, "ActivityVillageRawDataDownloadHistroy");
                }
                if (Flag == 8)
                {
                    ExportToCSVFile(dtMain, "ActivityVillageGSSRawDataDownloadHistroy");
                }
                if (Flag == 9)
                {
                    ExportToCSVFile(dtMain, "ActivityVillageMMRawDataDownloadHistroy");
                }
                if (Flag == 10)
                {
                    ExportToCSVFile(dtMain, "ApproveStatusDownloadHistroy");
                }
                if (Flag == 11)
                {
                    ExportToCSVFile(dtMain, "BalsabhaDetailDownloadHistroy");
                }
                if (Flag == 12)
                {
                    ExportToCSVFile(dtMain, "Balsabha-ChildRegistrationDownloadHistroy");
                }
                if (Flag == 13)
                {
                    ExportToCSVFile(dtMain, "LSEAttendanceDetailDownloadHistroy");
                }
                if (Flag == 14)
                {
                    ExportToCSVFile(dtMain, "SIPTargetvsAchvDownloadHistroy");
                }
                if (Flag == 15)
                {
                    ExportToCSVFile(dtMain, "SACReportDownloadHistroy");
                }
                if (Flag == 16)
                {
                    ExportToCSVFile(dtMain, "GKPChildRegistrationDownloadHistroy");
                }
                if (Flag == 17)
                {
                    ExportToCSVFile(dtMain, "GKPChildRegistrationClass2DownloadHistroy");
                }
                if (Flag == 18)
                {
                    ExportToCSVFile(dtMain, "GKPAttendence");
                }
                if (Flag == 19)
                {
                    ExportToCSVFile(dtMain, "GKPAssessment");
                }
                if (Flag == 20)
                {
                    ExportToCSVFile(dtMain, "GKPAssessmentClass2");
                }
                if (Flag == 21)
                {
                    ExportToCSVFile(dtMain, "GKPSummary");
                }
                if (Flag == 22)
                {
                    ExportToCSVFile(dtMain, "GKPAssessmentSummary");
                }
                if (Flag == 23)
                {
                    ExportToCSVFile(dtMain, "GKPQualityAlert");
                }
                if (Flag == 24)
                {
                    ExportToCSVFile(dtMain, "DoortoDoorSurvey");
                }
                if (Flag == 25)
                {
                    ExportToCSVFile(dtMain, "AnnualPlanTargetSheet");
                }
                if (Flag == 26)
                {
                    ExportToCSVFile(dtMain, "AnnualPlanSummary");
                }
                if (Flag == 27)
                {
                    ExportToCSVFile(dtMain, "EnrollmentTargetRawData");
                }
                if (Flag == 28)
                {
                    ExportToCSVFile(dtMain, "ApprovalProcessReport");
                }
                if (Flag == 29)
                {
                    ExportToCSVFile(dtMain, "AnnualPlanQualityAlert");
                }
                if (Flag == 30)
                {
                    ExportToCSVFile(dtMain, "Enrolment Details");
                }
                if (Flag == 31)
                {
                    ExportToCSVFile(dtMain, "Enrolment Quality Alert");
                }
                if (Flag == 32)
                {
                    ExportToCSVFile(dtMain, "Enrolment Summary");
                }

            }



            //  GenerateExcelSIP(dtMain, aprove);

        }
        else
        {

        }

    }

    protected void LnkAnnualPlGrad_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 21;





    }
    protected void LnkAVisitors(object sender, EventArgs e)
    {
        ViewState["1"] = 11;





    }
    protected void LnkAVisitorsSbL(object sender, EventArgs e)
    {
        ViewState["1"] = 511;





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


        if (ViewState["1"].ToString() == "18")
        {

            DataTable st = ViewState["SAC"] as DataTable;
            ExportToCSVFile(st, "CBLQualityAlertReport");

        }

        if (ViewState["1"].ToString() == "22")
        {

            DataTable st = ViewState["SAC"] as DataTable;


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


}