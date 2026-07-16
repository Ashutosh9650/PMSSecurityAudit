using Ionic.Zip;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class frmMobileTargetReportBO : System.Web.UI.Page
{

    clsMain objMain = new clsMain();
    string H1 = "", H2 = "", H3 = "", H4 = "", H5 = "";
    Comman objComman = new Comman();

    string conditions = "";
    protected void Page_Load(object sender, EventArgs e)
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
            else
            {
                base.Response.Redirect("Login.aspx", false);
            }
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
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
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
            conditions = "StateCode in(" + ddlState + ") and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
            string strQry1 = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where " + conditions + "  order by DistrictName   ";
            DataTable dtDistrict = objMain.LoadData(strQry1);
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            chkDistrict.DataSource = dtDistrict;
            chkDistrict.DataTextField = "DistrictName";
            chkDistrict.DataValueField = "DistrictCode";
            chkDistrict.DataBind();
            foreach (ListItem item in chkDistrict.Items)
            {

                item.Selected = true;
                break;
            }
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
    public DataTable CreateDataTable()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYear.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYear;
    }



    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBCluster();
        FillPanchayat();
        ddlPanchayat.Enabled = true;

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
    public void FillPanchayat()
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
    public void FillCVillageC()
    {

        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlCluserter = "";

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

        foreach (ListItem item in chkCluster.Items)
        {
            if (item.Selected)
            {

                ddlCluserter += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlCluserter.Length > 0)
        {
            ddlCluserter = ddlCluserter.Substring(0, ddlCluserter.LastIndexOf(","));
        }
        conditions = "";

        conditions = "DistrictCode in(" + ddlDistrict + ")  and BlockCode in(" + ddlBlock + ") and  ClusterCode in(" + ddlCluserter + ")";


        //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        //objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--All-");

        string strQry = "  SELECT VillageCode, dbo.TitleCase(upper(VillageName))  as VillageName FROM mst5Village where " + conditions + "  order by VillageName   ";
        DataTable dtDistrict = objMain.LoadData(strQry);

        chkVillage.DataSource = dtDistrict;
        chkVillage.DataTextField = "VillageName";
        chkVillage.DataValueField = "VillageCode";
        chkVillage.DataBind();


    }
    public void FillCVillageP()
    {

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
        string strQry = "  SELECT ClusterCode, dbo.TitleCase(upper(ClusterName))  as ClusterName FROM mstcluster where " + conditions + "  order by ClusterName   ";
        dtDistrict = objMain.LoadData(strQry);
        chkCluster.DataSource = dtDistrict;
        chkCluster.DataTextField = "ClusterName";
        chkCluster.DataValueField = "ClusterCode";
        chkCluster.DataBind();

    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillageP();

        Int32 iClusterCount = 0;
        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                iClusterCount = 1;
                break;


            }
        }
        if (iClusterCount > 0)
        {
            foreach (ListItem item in chkCluster.Items)
            {
                item.Selected = false;

            }
            chkCluster.Enabled = false;
        }
        else
        {
            chkCluster.Enabled = true;
        }

    }
    protected void ddlCluster_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillageC();
        Int32 iClusterCount = 0;
        foreach (ListItem item in chkCluster.Items)
        {
            if (item.Selected)
            {

                iClusterCount = 1;
                break;


            }
        }
        if (iClusterCount > 0)
        {
            foreach (ListItem item in ddlPanchayat.Items)
            {
                item.Selected = false;

            }
            ddlPanchayat.Enabled = false;
        }
        else
        {
            ddlPanchayat.Enabled = true;
        }


    }


    public void LoadYear()
    {
        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLYear("mstSchool", "Type,ID", conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


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
            strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions + "  and  Fyear='" + ddlYear.SelectedItem.Text + "' ";
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
        ViewState["Button"] = " ";
        FillCBDist();
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["Button"] = " ";
        FillCBBock();
    }


    protected void PMSSchool_Click(object sender, EventArgs e)
    {

        GV_DynamicGrid2.Visible = true;

        ViewState["Button"] = "2";
        btnexcel.Visible = true;
        ActivityVillageWise(2);


    }
    protected void PMSOffice_Click(object sender, EventArgs e)
    {

        GV_DynamicGrid2.Visible = true;

        ViewState["Button"] = "5";
        btnexcel.Visible = true;
        ActivityVillageWise(5);


    }

    protected void PMS_Click(object sender, EventArgs e)
    {

        GV_DynamicGrid2.Visible = true;

        ViewState["Button"] = "1";
        btnexcel.Visible = true;
        ActivityVillageWise(1);


    }



    public void ActivityVillageWise(Int32 Flag)
    {
        conditions = "";


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
            conditions = conditions + "  where   mst5village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst5village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst5village.DistrictCode in(" + ddlDistrict + ") ";
        }

        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst5village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions = conditions + " and mst5village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions = conditions + " and mst5village.Villagecode in(" + ddlVillage + ") ";
        }


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),

        };
        if (Flag == 1)
        {
            DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptBOWiseVillage]", cmdParameters);

            ViewState["dt"] = dataTable;
            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows.Count < 500)
                {
                    GV_DynamicGrid2.DataSource = dataTable;
                    GV_DynamicGrid2.DataBind();
                }
                else
                {
                    ExportToCSVFile(dataTable, "ActivityVillageWise");
                }
                return;
            }
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }

        if (Flag == 2)
        {
            DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptBOWiseSchool]", cmdParameters);

            ViewState["dt"] = dataTable;
            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows.Count < 500)
                {
                    GV_DynamicGrid2.DataSource = dataTable;
                    GV_DynamicGrid2.DataBind();
                }
                else
                {
                    ExportToCSVFile(dataTable, "ActivitySchoolWise");
                }
                return;
            }
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }
        if (Flag == 5)
        {
            DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptBOWiseOffice]", cmdParameters);

            ViewState["dt"] = dataTable;
            if (dataTable.Rows.Count > 0)
            {
                if (dataTable.Rows.Count < 1000)
                {
                    GV_DynamicGrid2.DataSource = dataTable;
                    GV_DynamicGrid2.DataBind();
                }
                else
                {
                    ExportToCSVFile(dataTable, "ActivityOfficelWise");
                }
                return;
            }
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }
    }




    private void ExportToCSVFileNew(DataTable dtTable, string filePath)
    {
        StringBuilder sbldr = new StringBuilder();
        if (dtTable != null)
        {
            if (dtTable.Columns.Count != 0)
            {
                foreach (DataColumn col in dtTable.Columns)
                {
                    if (col.ColumnName == "Q1")
                    {
                        sbldr.Append(H1 + ',');
                    }
                    else if (col.ColumnName == "Q2")
                    {
                        sbldr.Append(H2 + ',');
                    }
                    else if (col.ColumnName == "Q3")
                    {
                        sbldr.Append(H3 + ',');
                    }
                    else if (col.ColumnName == "Q4")
                    {
                        sbldr.Append(H4 + ',');
                    }
                    else if (col.ColumnName == "SRNo1")
                    {

                    }
                    else if (col.ColumnName == "Q5")
                    {
                        if (H5.Length > 0)
                        {
                            sbldr.Append(H4 + ',');
                        }
                    }
                    else
                    {
                        sbldr.Append(col.ColumnName + ',');
                    }

                }
                sbldr.Append("\r\n");
                foreach (DataRow row in dtTable.Rows)
                {
                    foreach (DataColumn column in dtTable.Columns)
                    {
                        if (column.ColumnName == "Q1")
                        {
                            sbldr.Append(Convert.ToString(row[column]).Replace(",", "  ").Replace("\r", "").Replace("\n", "") + ',');
                        }
                        else if (column.ColumnName == "Q2")
                        {
                            sbldr.Append(Convert.ToString(row[column]).Replace(",", "  ").Replace("\r", "").Replace("\n", "") + ',');
                        }
                        else if (column.ColumnName == "Q3")
                        {
                            sbldr.Append(Convert.ToString(row[column]).Replace(",", "  ").Replace("\r", "").Replace("\n", "") + ',');
                        }
                        else if (column.ColumnName == "Q4")
                        {
                            sbldr.Append(Convert.ToString(row[column]).Replace(",", "  ").Replace("\r", "").Replace("\n", "") + ',');
                        }
                        else if (column.ColumnName == "SRNo1")
                        {

                        }
                        else if (column.ColumnName == "Q5")
                        {
                            if (H5.Length > 0)
                            {
                                sbldr.Append(Convert.ToString(row[column]).Replace(",", "  ").Replace("\r", "").Replace("\n", "") + ',');
                            }
                        }
                        else
                        {
                            sbldr.Append(Convert.ToString(row[column]).Replace(",", "  ").Replace("\r", "").Replace("\n", "") + ',');
                        }

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
                Response.AddHeader("Content-disposition", "attachment; filename=" + fullPath);
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
        //str.Write(sbldr.ToString());
        //Response.ContentType = "Application/x-msexcel";
        //Response.AddHeader("content-disposition", "attachment;filename=test.csv");
        //Response.Write(sbldr.ToString());
        //Response.End();
    }




    private void ExportToCSVFile(DataTable dtTable, string filePath)
    {
        StringBuilder sbldr = new StringBuilder();
        if (dtTable != null)
        {
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

                    //zip.AddFile(foldername);
                    //string zipName = String.Format("{0}.zip", datafolder);
                    //zip.AddSelectedFiles("*.*", foldername);
                    //zip.Save(Server.MapPath(Comman.GetImagePath("DataBackupPath")) +"/" ) + zipName);

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
        //str.Write(sbldr.ToString());
        //Response.ContentType = "Application/x-msexcel";
        //Response.AddHeader("content-disposition", "attachment;filename=test.csv");
        //Response.Write(sbldr.ToString());
        //Response.End();
    }

    protected void excel(DataTable dtexcel)
    {
        string rptnm = "EG_Report_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".xls";
        StringBuilder sbb = new StringBuilder();
        ExportToExcel exportToExcel = new ExportToExcel();
        exportToExcel.ExporttoExcel(dtexcel, sbb, rptnm);
    }

    public void Export_To_Excel(object sender, EventArgs e)
    {
        if (ViewState["Button"].ToString() == "1")
        {
            DataTable dt = ViewState["dt"] as DataTable;
            ExportToCSVFile(dt, "ActivityVillageWise");
        }

        if (ViewState["Button"].ToString() == "2")
        {
            DataTable dt = ViewState["dt"] as DataTable;
            ExportToCSVFile(dt, "ActivitySchoolWise");
        }

        if (ViewState["Button"].ToString() == "5")
        {
            DataTable dt = ViewState["dt"] as DataTable;
            ExportToCSVFile(dt, "ActivityOfficeWise");
        }

    }
    private void ExportGridToExcel(GridView Gv, string FileName1)
    {
        try
        {
            //Gv.AllowPaging = false;
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = FileName1 + "_" + DateTime.Now + ".xls";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            //Gv.AllowPaging = false;
            Gv.GridLines = GridLines.Both;
            Gv.HeaderStyle.Font.Bold = true;
            Gv.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.Flush();
            Response.End();

        }
        catch (Exception)
        {

            throw;
        }

    }
    protected void ExporttoExcel(GridView Gv, DataTable table, string FileName)
    {
        Response.Clear();
        Response.Buffer = true;
        string str = "Report";
        string Fullfilename = "" + str + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";

        //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + " ");
        Response.AddHeader("content-disposition", "attachment;filename=" + Fullfilename + " ");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //To Export all pages

            //GridView gv = new GridView();
            //gv.AllowPaging = false;

            //gv.DataSource = table;
            //gv.DataBind();

            foreach (TableCell cell in Gv.HeaderRow.Cells)
            {
                cell.BackColor = Gv.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in Gv.Rows)
            {

                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = Gv.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = Gv.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            Gv.RenderControl(hw);
            //style to format numbers to string
            string style = @"<style> .textmode { } </style> <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    private void ExportToExcelGridView(GridView Gv, DataTable table, string FileName)
    {
        //Gv.DataSource = table;
        //Gv.DataBind();

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
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
        int count = Gv.HeaderRow.Cells.Count;
        for (int i = 0; i < count; i++)
        {
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(Gv.HeaderRow.Cells[i].Text);
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");
        }
        HttpContext.Current.Response.Write("</TR>");
        foreach (DataRow row in table.Rows)
        {//write in new row
            HttpContext.Current.Response.Write("<TR>");
            for (int i = 0; i < count; i++)
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

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    private void GenerateExcelNew(DataTable dt)
    {
        string abc1 = "";
        string abc2 = "";
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");
        string Fullfilename = "" + "Report" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        // Int32 EmpID = Convert.ToInt32(Contx.Request["empid"]);



        HttpContext.Current.Response.Write("<table style='border:.5pt solid windowtext;'>");





        if (dt.Rows.Count > 0)
        {





        }

        String HeaderStyle = "border:.5pt solid windowtext; font-weight:700;background:#D9D9D9;";
        HttpContext.Current.Response.Write("    <tr style='font-width:bold;'>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>#</td>");
        //   HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>TimeSheet ID</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>District Name</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>District Code</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Block Name</td>");

        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Block Code</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>ClusterCode</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>ClusterName</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Panchayat Name</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Panchayat Code</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Village Name</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Village Code</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Name</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>DISECODE</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Annual</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Apr To Jun</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Jul-Sep</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Oct-Dec</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Jan-Mar</td>");

        //HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Version No</td>");
        HttpContext.Current.Response.Write("    </tr>");



        String DataStyle = "border:.5pt solid windowtext;";
        String DateTimeStyle = "mso-number-format:\"mm/dd/yyyy hh:mm AM/PM \";";
        String DateStyle = "mso-number-format:\"mm/dd/yyyy \";";
        String TimeStyle = "mso-number-format:\"hh:mm AM/PM \";";
        String A1 = "background-color:Green;border:.5pt solid;";
        String A2 = "background-color:Orange;border:.5pt solid;";
        String A3 = "background-color:Red;border:.5pt solid;";
        String A4 = "background-color:Blue;border:.5pt solid;";





        var i = 0;
        double distance = 0;
        double Enddistance = 0;
        for (i = 0; i < dt.Rows.Count; i++)
        {

            var RowStyle = DataStyle;



            HttpContext.Current.Response.Write("<tr>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + (i + 1) + "</td>");
            //retStr += "<td style='" + RowStyle + "'>" + mData.TimeSheet_ID + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["DistrictName"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["DistrictCode"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["BlockName"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["BlockCode"].ToString() + "</td>");

            // HttpContext.Current.Response.Write("<td style='" + RowStyle + DateStyle + "'>" + Convert.ToDateTime(dt.Rows[i]["Date"].ToString()).ToString("dd/MM/yyy") + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["ClusterCode"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["ClusterName"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["PanchayatName"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["PanchayatCode"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["VillageName"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["VillageCode"].ToString() + "</td>");

            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["Name"].ToString() + "</td>");
            //   HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_StartTime"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["DiseCode"].ToString() + "</td>");

            if (dt.Rows[i]["Annual"].ToString() == "0")
            {
                HttpContext.Current.Response.Write("<td style='" + DataStyle + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Annual"].ToString() == "1")
            {
                HttpContext.Current.Response.Write("<td style='" + A1 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Annual"].ToString() == "2")
            {
                HttpContext.Current.Response.Write("<td style='" + A2 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Annual"].ToString() == "3")
            {
                HttpContext.Current.Response.Write("<td style='" + A3 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Annual"].ToString() == "4")
            {
                HttpContext.Current.Response.Write("<td style='" + A4 + "'>" + "" + "</td>");

            }


            if (dt.Rows[i]["Q1"].ToString() == "0")
            {
                HttpContext.Current.Response.Write("<td style='" + DataStyle + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q1"].ToString() == "1")
            {
                HttpContext.Current.Response.Write("<td style='" + A1 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q1"].ToString() == "2")
            {
                HttpContext.Current.Response.Write("<td style='" + A2 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q1"].ToString() == "3")
            {
                HttpContext.Current.Response.Write("<td style='" + A3 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q1"].ToString() == "4")
            {
                HttpContext.Current.Response.Write("<td style='" + A4 + "'>" + "" + "</td>");

            }


            if (dt.Rows[i]["Q2"].ToString() == "0")
            {
                HttpContext.Current.Response.Write("<td style='" + DataStyle + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q2"].ToString() == "1")
            {
                HttpContext.Current.Response.Write("<td style='" + A1 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q2"].ToString() == "2")
            {
                HttpContext.Current.Response.Write("<td style='" + A2 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q2"].ToString() == "3")
            {
                HttpContext.Current.Response.Write("<td style='" + A3 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q2"].ToString() == "4")
            {
                HttpContext.Current.Response.Write("<td style='" + A4 + "'>" + "" + "</td>");

            }


            if (dt.Rows[i]["Q3"].ToString() == "0")
            {
                HttpContext.Current.Response.Write("<td style='" + DataStyle + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q3"].ToString() == "1")
            {
                HttpContext.Current.Response.Write("<td style='" + A1 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q3"].ToString() == "2")
            {
                HttpContext.Current.Response.Write("<td style='" + A2 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q3"].ToString() == "3")
            {
                HttpContext.Current.Response.Write("<td style='" + A3 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q3"].ToString() == "4")
            {
                HttpContext.Current.Response.Write("<td style='" + A4 + "'>" + "" + "</td>");

            }

            if (dt.Rows[i]["Q4"].ToString() == "0")
            {
                HttpContext.Current.Response.Write("<td style='" + DataStyle + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q4"].ToString() == "1")
            {
                HttpContext.Current.Response.Write("<td style='" + A1 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q4"].ToString() == "2")
            {
                HttpContext.Current.Response.Write("<td style='" + A2 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q4"].ToString() == "3")
            {
                HttpContext.Current.Response.Write("<td style='" + A3 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q4"].ToString() == "4")
            {
                HttpContext.Current.Response.Write("<td style='" + A4 + "'>" + "" + "</td>");

            }

            Enddistance = 0;
            HttpContext.Current.Response.Write("</tr>");

        }

        DataStyle += "background-color:yellow;";

        HttpContext.Current.Response.Write("</table>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();


        //flushExcel(totDays.ToString());

        //var lq=dataList.Select(fld=>fld.TimeSheet_EndTime.

    }

}