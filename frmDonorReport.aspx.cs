using Ionic.Zip;
using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class frmDonorReport : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    string conditions = "";
    string flag = "";
    Password objPass = new Password();
    public DataTable dtUserDeatils;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblTotalCount.Text = "";
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                LoadYear();
                LoadUserLeavel();
                ViewState["1"] = "ss";
                CreateDataTable();

            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
        }
    }
    public void CreateDataTable()
    {

        DataTable dt = new DataTable();
        dt.Columns.Add("Type", System.Type.GetType("System.String"));

        dt.Columns.Add("ID", System.Type.GetType("System.Int32"));
        DataRow dr;
        dr = dt.NewRow();
        dr[0] = "5";
        dr[1] = "5";
        dt.Rows.Add(dr);
        dr = dt.NewRow();
        dr[0] = "6";
        dr[1] = "6";
        dt.Rows.Add(dr);
        dr = dt.NewRow();
        dr[0] = "7";
        dr[1] = "7";
        dt.Rows.Add(dr);
        dr = dt.NewRow();
        dr[0] = "8";
        dr[1] = "8";
        dt.Rows.Add(dr);
        dr = dt.NewRow();
        dr[0] = "9";
        dr[1] = "9";
        dt.Rows.Add(dr);
        dr = dt.NewRow();
        dr[0] = "10";
        dr[1] = "10";
        dt.Rows.Add(dr);
        dr = dt.NewRow();
        dr[0] = "11";
        dr[1] = "11";
        dt.Rows.Add(dr);
        dr = dt.NewRow();
        dr[0] = "12";
        dr[1] = "12";
        dt.Rows.Add(dr);
        dr = dt.NewRow();
        dr[0] = "13";
        dr[1] = "13";
        dt.Rows.Add(dr);
        dr = dt.NewRow();
        dr[0] = "14";
        dr[1] = "14";
        dt.Rows.Add(dr);
        chkAge.DataSource = dt;
        chkAge.DataTextField = "Type";
        chkAge.DataValueField = "ID";
        chkAge.DataBind();



    }
    public void LoadYear()
    {
        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (ViewState["Donar"].ToString() == "1")
        {

            GenerateExcelNew("EnrolmentDonorReport");
        }
        if (ViewState["Donar"].ToString() == "2")
        {

            GenerateExcelNew("EnrolmentDonorReportAge7TO14");
        }
        if (ViewState["Donar"].ToString() == "3")
        {

            GenerateExcelNewOut("EnrolmentDonorOpsReport");
        }
        if (ViewState["Donar"].ToString() == "4")
        {

            GenerateExcelNewOut("EnrolmentDonorOpsReportAge7TO14");
        }
        if (ViewState["Donar"].ToString() == "5")
        {

            GenerateExcelTeamBalik("TBRecruitmentReport");
        }
        if (ViewState["Donar"].ToString() == "6")
        {

            GenerateExcelTeamBalikTraning("TB Training Report");
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

        chkVillage.Items.Clear();
    }




    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {

        chkVillage.Items.Clear();
        chkDistrict.Items.Clear();
        chkBlock.Items.Clear();
        FillCBDist();
    }
    public void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBBock();

    }
    public void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBCluster();
    }
    public void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillageC();
    }



    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
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
        string strQry = "  SELECT ClusterCode, dbo.TitleCase(upper(ClusterName))  as ClusterName FROM mstcluster where " + conditions + "  order by ClusterName   ";
        dtDistrict = objMain.LoadData(strQry);
        chkCluster.DataSource = dtDistrict;
        chkCluster.DataTextField = "ClusterName";
        chkCluster.DataValueField = "ClusterCode";
        chkCluster.DataBind();

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
    protected void btnEnroll_Click(object sender, EventArgs e)
    {
        lnkReport.Visible = false;
        rptD2D.Visible = true;
        ViewState["Donar"] = "7";
        LoadReportEnrollmentTest();
    }
    protected void btnAnalayis_Click(object sender, EventArgs e)
    {
        lnkReport.Visible = false;
        rptD2D.Visible = true;
        ViewState["Donar"] = "9";
        LoadReportEnrollmentTest();
    }
    protected void btnEnrolllmenSummary_Click(object sender, EventArgs e)
    {
        if (ddlGrouping.SelectedIndex > 0)
        {
            lnkReport.Visible = true;
            rptD2D.Visible = false;
            ViewState["Donar"] = "1";
            LoadReportEnrollmentSummary(1);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Group ')</script>", false);
        }

    }
    protected void btnEnrolllmenSummary1_Click(object sender, EventArgs e)
    {
        if (ddlGrouping.SelectedIndex > 0)
        {
            rptD2D.Visible = false;
            lnkReport.Visible = true;
            ViewState["Donar"] = "2";
            LoadReportEnrollmentSummary(2);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Group ')</script>", false);
        }

    }
    protected void btnEnrolllmenSummaryOps_Click(object sender, EventArgs e)
    {
        if (ddlGrouping.SelectedIndex > 0)
        {
            rptD2D.Visible = false;
            lnkReport.Visible = true;
            ViewState["Donar"] = "3";
            LoadReportEnrollmentSummaryOpt(1);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Group ')</script>", false);
        }

    }
    protected void btnEnrolllmenSummaryOps1_Click(object sender, EventArgs e)
    {
        if (ddlGrouping.SelectedIndex > 0)
        {
            rptD2D.Visible = false;
            lnkReport.Visible = true;
            ViewState["Donar"] = "4";
            LoadReportEnrollmentSummaryOpt(2);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Group ')</script>", false);
        }

    }
    protected void btnTB_Click(object sender, EventArgs e)
    {
        if (ddlGrouping.SelectedIndex > 0)
        {
            rptD2D.Visible = false;
            lnkReport.Visible = false;
            ViewState["Donar"] = "5";
            LoadReportTeamBalikSummary(1);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Group ')</script>", false);
        }

    }

    protected void btnTBTraining_Click(object sender, EventArgs e)
    {
        lnkReport.Visible = false;
        ViewState["Donar"] = "6";
        LoadReportTeamBalikSummaryTtraning(1);


    }
    protected void lnk_Click(object sender, EventArgs e)
    {

        ViewState["Donar"] = "11";
        LoadReportEnrollment();
        DataTable dt = Session["Enroll123"] as DataTable;

        gvEnrollSummary.DataSource = dt;
        gvEnrollSummary.DataBind();
    }
    public void LoadReportEnrollment()
    {

        conditions = "";
        string conditionsCr = "";
        string conditionsmo = "";
        string conditionsDe = "";
        string conditionsAll = "";
        lblTotalCount.Text = "";


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

        foreach (ListItem item in chkCluster.Items)
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
        lblTotalCount.Text = "";
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions += "  and mst5Village.StateCode in(" + ddlStatecode + ") ";
            conditionsAll += "  StateCode in(" + ddlStatecode + ")";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst5Village.DistrictCode in( " + ddlDistrict + ") ";
            conditionsAll += "  and DistCode in(" + ddlDistrict + ") ";
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
            conditions += " and mst5Village.Villagecode in( " + ddlVillage + ") ";

        }
        string Age = "";
        foreach (ListItem item in chkAge.Items)
        {
            if (item.Selected)
            {

                Age += "" + item.Value + "" + ",";



            }
        }
        string AgeEnGrouopp = "";

        if (Age.Length > 0)
        {
            Age = Age.Substring(0, Age.LastIndexOf(","));

            AgeEnGrouopp = " and dbo.udfDateDiffinYrMonDay(tblEnrolment.dob,EnrolmentDate) in(" + Age + ")";
        }
        else
        {
            AgeEnGrouopp = " and dbo.udfDateDiffinYrMonDay(tblEnrolment.dob,EnrolmentDate) in(5,6,7,8,9,10,11,12,13,14)";

        }
        string D2d = "";
        if (Convert.ToInt32(ddlFlag.SelectedValue) == 1)
        {
            D2d = "2";
        }
        else if (Convert.ToInt32(ddlFlag.SelectedValue) == 2)
        {
            D2d = "1";
        }
        else
        {
            D2d = "1,2";
        }
        if (D2d.Length > 0)
        {
            conditions += " and tblEnrolment.Status  in(" + D2d + ") ";
        }
        string FristCon = conditions + conditionsCr;

        if (ViewState["Donar"].ToString() == "3")
        {
            flag = "1";

        }
        else
        {
            flag = "2";
        }

        //  DataTable dt = objMain.ReportEnrollDeatilsNew(FristCon);
        SqlParameter[] cmdParameters = new SqlParameter[]
 {
            new SqlParameter("@schoolCode", FristCon),
            new SqlParameter("@Year", ddlYear.SelectedValue),
            new SqlParameter("@Flag", flag),
             new SqlParameter("@Groupby",ddlGrouping.SelectedValue),
                 new SqlParameter("@Dd2",D2d),
                    new SqlParameter("@Fyear",ddlYear.SelectedValue),
                  new SqlParameter("@Age",Age),
                  new SqlParameter("@EnrollAge",AgeEnGrouopp),

 };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rpEnrollmentDonorRawData]", cmdParameters);

        //    DataTable dt = objMain.ReportEnrollDeatilsNew(FristCon);
        if (dt.Rows.Count > 0)
        {

            ExportToCSVFile(dt, "Enrollment");


        }
        else
        {

        }


        //19933



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
                string includeSubFolders = "File";
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

    public void LoadReportEnrollmentSummaryOpt(Int32 flag)
    {

        conditions = "";

        string conditionsAll = "";
        lblTotalCount.Text = "";


        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlStatecode = "";
        string Age = "";
        string AgeSumF = "";
        string AgeSumM = "";
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


        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }

        foreach (ListItem item in chkAge.Items)
        {
            if (item.Selected)
            {

                Age += "" + item.Value + "" + ",";

                AgeSumF += "SUM(TG" + item.Value + ")+";
                AgeSumM += "SUM(TM" + item.Value + ")+";

            }
        }
        string AgeGrouopp = "";
        string AgeEnGrouopp = "";
        if (Age.Length > 0)
        {
            Age = Age.Substring(0, Age.LastIndexOf(","));
            AgeSumF = AgeSumF.Substring(0, AgeSumF.LastIndexOf("+"));
            AgeSumM = AgeSumM.Substring(0, AgeSumM.LastIndexOf("+"));
            AgeGrouopp = " and Age in(" + Age + ")";
            AgeEnGrouopp = " and dbo.udfDateDiffinYrMonDay(tblEnrolment.dob,EnrolmentDate) in(" + Age + ")";
        }
        else
        {
            AgeSumF = "SUM(TG5)+SUM(TG6)+SUM(TG7)+SUM(TG8)+SUM(TG9)+SUM(TG10)+SUM(TG11)+SUM(TG12)+SUM(TG13)+SUM(TG14)";
            AgeSumM = "SUM(TM5)+SUM(TM6)+SUM(TM7)+SUM(TM8)+SUM(TM9)+SUM(TM10)+SUM(TM11)+SUM(TM12)+SUM(TM13)+SUM(TM14)";

            AgeEnGrouopp = " and dbo.udfDateDiffinYrMonDay(tblEnrolment.dob,EnrolmentDate) in(5,6,7,8,9,10,11,12,13,14)";


        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        lblTotalCount.Text = "";
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions += "  and mst5Village.StateCode in(" + ddlStatecode + ") ";
            conditionsAll += "  StateCode in(" + ddlStatecode + ")";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst5Village.DistrictCode in( " + ddlDistrict + ") ";
            conditionsAll += "  and DistCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {

            conditions += " and mst5Village.BlockCode in(" + ddlBlock + ") ";
            conditionsAll += " and  BlockCode in(" + ddlBlock + ") ";

        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.Villagecode in( " + ddlVillage + ") ";

        }

        string D2d = "";
        if (Convert.ToInt32(ddlFlag.SelectedValue) == 1)
        {
            D2d = "2";
        }
        else if (Convert.ToInt32(ddlFlag.SelectedValue) == 2)
        {
            D2d = "1";
        }
        else
        {
            D2d = "1,2";
        }
        string FristCon = conditions;
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');


        string CreatDate = "" + Year1[0] + "-04-01";


        //  DataTable dt = objMain.ReportEnrollDeatilsNew(FristCon);
        SqlParameter[] cmdParameters = new SqlParameter[]
     {
            new SqlParameter("@schoolCode", FristCon),
            new SqlParameter("@Year", ddlYear.SelectedValue),
            new SqlParameter("@Flag", flag),
             new SqlParameter("@Groupby",ddlGrouping.SelectedValue),
                 new SqlParameter("@Dd2",D2d),
                    new SqlParameter("@Fyear",ddlYear.SelectedValue),
                  new SqlParameter("@Age",AgeGrouopp),
                        new SqlParameter("@AgeF",AgeSumF),

                                      new SqlParameter("@AgeM",AgeSumM),
                                        new SqlParameter("@EnrollAge",AgeEnGrouopp),
                                          new SqlParameter("@CreatDate",CreatDate),

     };

        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rpEnrollmentDonorOther2020]", cmdParameters);
        if (dt.Rows.Count > 0)
        {


            gvEnrollSummary.Visible = true;
            Session["Enroll123"] = dt;
            lblTotalCount.Text = (dt.Rows.Count).ToString();
            gvEnrollSummary.DataSource = dt;
            gvEnrollSummary.DataBind();

        }
        else
        {
            gvEnrollSummary.DataSource = null;
            gvEnrollSummary.DataBind();
            Session["Enroll123"] = null;
        }





    }
    public void LoadReportEnrollmentSummary(Int32 flag)
    {

        conditions = "";

        string conditionsAll = "";
        lblTotalCount.Text = "";


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
        lblTotalCount.Text = "";
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions += "  and mst5Village.StateCode in(" + ddlStatecode + ") ";
            conditionsAll += "  StateCode in(" + ddlStatecode + ")";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst5Village.DistrictCode in( " + ddlDistrict + ") ";
            conditionsAll += "  and DistCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {

            conditions += " and mst5Village.BlockCode in(" + ddlBlock + ") ";
            conditionsAll += " and  BlockCode in(" + ddlBlock + ") ";

        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.Villagecode in( " + ddlVillage + ") ";

        }

        string D2d = "";
        if (Convert.ToInt32(ddlFlag.SelectedValue) == 1)
        {
            D2d = "2";
        }
        else if (Convert.ToInt32(ddlFlag.SelectedValue) == 2)
        {
            D2d = "1";
        }
        else
        {
            D2d = "1,2";
        }

        string Age = "";
        foreach (ListItem item in chkAge.Items)
        {
            if (item.Selected)
            {

                Age += "" + item.Value + "" + ",";



            }
        }
        string AgeGrouopp = "";
        string AgeEnGrouopp = "";
        if (Age.Length > 0)
        {
            Age = Age.Substring(0, Age.LastIndexOf(","));
            AgeGrouopp = " and Age in(" + Age + ")";
            AgeEnGrouopp = " and dbo.udfDateDiffinYrMonDay(tblEnrolment.dob,EnrolmentDate) in(" + Age + ")";
        }
        else
        {
            AgeEnGrouopp = " and dbo.udfDateDiffinYrMonDay(tblEnrolment.dob,EnrolmentDate) in(5,6,7,8,9,10,11,12,13,14)";
        }

        string FristCon = conditions;
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');


        string CreatDate = "" + Year1[0] + "-04-01";
        //  DataTable dt = objMain.ReportEnrollDeatilsNew(FristCon);
        SqlParameter[] cmdParameters = new SqlParameter[]
     {
            new SqlParameter("@schoolCode", FristCon),
            new SqlParameter("@Year", ddlYear.SelectedValue),
            new SqlParameter("@Flag", flag),
             new SqlParameter("@Groupby",ddlGrouping.SelectedValue),
                 new SqlParameter("@Dd2",D2d),
              new SqlParameter("@Age",AgeGrouopp),
              new SqlParameter("@EnrollAge",AgeEnGrouopp),
              new SqlParameter("@CreatDate",CreatDate),
     };

        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rpEnrollmentDonorNew2020]", cmdParameters);
        if (dt.Rows.Count > 0)
        {


            gvEnrollSummary.Visible = true;
            Session["Enroll123"] = dt;
            lblTotalCount.Text = (dt.Rows.Count).ToString();
            gvEnrollSummary.DataSource = dt;
            gvEnrollSummary.DataBind();

        }
        else
        {
            gvEnrollSummary.DataSource = null;
            gvEnrollSummary.DataBind();
            Session["Enroll123"] = null;
        }





    }

    public void LoadReportTeamBalikSummary(Int32 flag)
    {

        conditions = "";

        string conditionsAll = "";
        lblTotalCount.Text = "";


        string ddlBlock = "";
        string ddlDistrict = "";

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
        lblTotalCount.Text = "";
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "   V.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions += "  and V.StateCode in(" + ddlStatecode + ") ";
            conditionsAll += "  StateCode in(" + ddlStatecode + ")";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and V.DistrictCode in( " + ddlDistrict + ") ";
            conditionsAll += "  and DistCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {

            conditions += " and V.BlockCode in(" + ddlBlock + ") ";
            conditionsAll += " and  BlockCode in(" + ddlBlock + ") ";

        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and V.Villagecode in( " + ddlVillage + ") ";

        }

        string D2d = "";

        string FristCon = conditions;
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');


        string CreatDate = "" + Year1[0] + "-04-01";
        //  DataTable dt = objMain.ReportEnrollDeatilsNew(FristCon);
        SqlParameter[] cmdParameters = new SqlParameter[]
     {
            new SqlParameter("@schoolCode", FristCon),
            new SqlParameter("@Year", ddlYear.SelectedValue),
            new SqlParameter("@Flag", flag),
             new SqlParameter("@Groupby",ddlGrouping.SelectedValue),
               new SqlParameter("@CreatDate",CreatDate),


     };

        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rpTeamBalikSummary]", cmdParameters);
        if (dt.Rows.Count > 0)
        {


            gvEnrollSummary.Visible = true;
            Session["Enroll123"] = dt;
            lblTotalCount.Text = (dt.Rows.Count).ToString();
            gvEnrollSummary.DataSource = dt;
            gvEnrollSummary.DataBind();

        }
        else
        {
            gvEnrollSummary.DataSource = null;
            gvEnrollSummary.DataBind();
            Session["Enroll123"] = null;
        }





    }
    public void LoadReportTeamBalikSummaryTtraning(Int32 flag)
    {

        conditions = "";

        string conditionsAll = "";
        lblTotalCount.Text = "";


        string ddlBlock = "";
        string ddlDistrict = "";

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
        lblTotalCount.Text = "";
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "   V.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions += "  and V.StateCode in(" + ddlStatecode + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and V.DistrictCode in( " + ddlDistrict + ") ";

        }
        if (ddlBlock.Length > 0)
        {

            conditions += " and V.BlockCode in(" + ddlBlock + ") ";


        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and V.Villagecode in( " + ddlVillage + ") ";

        }
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');
        if (ddlYear.SelectedItem.Text == "2016-2017")
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "    And FromDate <= '" + Year1[1] + "-03-31'";
            }
        }
        else
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "    And FromDate >= '" + Year1[0] + "-04-01' and ToDate<='" + Year1[1] + "-03-31'";


            }
        }
        string D2d = "";

        string FristCon = conditions;

        //  DataTable dt = objMain.ReportEnrollDeatilsNew(FristCon);
        SqlParameter[] cmdParameters = new SqlParameter[]
     {
            new SqlParameter("@con", FristCon),

            new SqlParameter("@Groupby",ddlGrouping.SelectedValue),

     };

        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTempBalikTraining]", cmdParameters);
        if (dt.Rows.Count > 0)
        {


            gvEnrollSummary.Visible = true;
            Session["Enroll123"] = dt;
            lblTotalCount.Text = (dt.Rows.Count).ToString();
            gvEnrollSummary.DataSource = dt;
            gvEnrollSummary.DataBind();

        }
        else
        {
            gvEnrollSummary.DataSource = null;
            gvEnrollSummary.DataBind();
            Session["Enroll123"] = null;
        }





    }
    protected void GV_DynamicGrid1_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEnrollSummary.PageIndex = e.NewPageIndex;
        if (Session["Enroll123"] != null)
        {

            DataTable Dt = Session["Enroll123"] as DataTable;
            gvEnrollSummary.DataSource = Dt;
            gvEnrollSummary.DataBind();
        }
    }
    protected void gvReportNew_RowCreated(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.Header)
        {

            if (ViewState["Donar"].ToString() == "1" || ViewState["Donar"].ToString() == "2")
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                HeaderGridRow.CssClass = "gridnewheadercss";
                TableCell HeaderCell;

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 1)
                {
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                    HeaderCell.ColumnSpan = 1;
                    HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                    HeaderGridRow.Cells.Add(HeaderCell);
                }

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 2)
                {
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                    HeaderCell.ColumnSpan = 3;
                    HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                    HeaderGridRow.Cells.Add(HeaderCell);
                }

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 3)
                {
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                    HeaderCell.ColumnSpan = 4;
                    HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                    HeaderGridRow.Cells.Add(HeaderCell);
                }

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 4)
                {
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                    HeaderCell.ColumnSpan = 6;
                    HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                    HeaderGridRow.Cells.Add(HeaderCell);
                }
                //  HeaderCell.ColumnSpan = 5;




                HeaderCell = new TableCell();
                HeaderCell.Text = "F";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 4;

                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);



                HeaderCell = new TableCell();
                HeaderCell.Text = "M";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 4;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);


                HeaderCell = new TableCell();
                HeaderCell.Text = "Total";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 5;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);

                gvEnrollSummary.Controls[0].Controls.AddAt(0, HeaderGridRow);




            }


            if (ViewState["Donar"].ToString() == "3" || ViewState["Donar"].ToString() == "4")
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                HeaderGridRow.CssClass = "gridnewheadercss";
                TableCell HeaderCell;

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 1)
                {
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                    HeaderCell.ColumnSpan = 1;
                    HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                    HeaderGridRow.Cells.Add(HeaderCell);
                }

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 2)
                {
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                    HeaderCell.ColumnSpan = 3;
                    HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                    HeaderGridRow.Cells.Add(HeaderCell);
                }

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 3)
                {
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                    HeaderCell.ColumnSpan = 4;
                    HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                    HeaderGridRow.Cells.Add(HeaderCell);
                }

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 4)
                {
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                    HeaderCell.ColumnSpan = 6;
                    HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                    HeaderGridRow.Cells.Add(HeaderCell);
                }
                //  HeaderCell.ColumnSpan = 5;




                HeaderCell = new TableCell();
                HeaderCell.Text = "D2D Target";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 3;

                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);




                HeaderCell = new TableCell();
                HeaderCell.Text = "D2D Target-Current Year Enrolment";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 3;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "D2D Target- Last Year Enrolment";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 3;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);


                HeaderCell = new TableCell();
                HeaderCell.Text = "Current Year -Out of Target D2D Enrolment";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 3;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);


                HeaderCell = new TableCell();
                HeaderCell.Text = "Enrolment OOD2D";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 3;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);


                HeaderCell = new TableCell();
                HeaderCell.Text = "Total Enrolment";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 3;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);


                HeaderCell = new TableCell();
                HeaderCell.Text = "Ineligible";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 3;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);



                HeaderCell = new TableCell();
                HeaderCell.Text = "Remaining D2D Target";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 5;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);

                gvEnrollSummary.Controls[0].Controls.AddAt(0, HeaderGridRow);




            }
            if (ViewState["Donar"].ToString() == "5")
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                HeaderGridRow.CssClass = "gridnewheadercss";
                TableCell HeaderCell;

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 1)
                {
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                    HeaderCell.ColumnSpan = 1;
                    HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                    HeaderGridRow.Cells.Add(HeaderCell);
                }

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 2)
                {
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                    HeaderCell.ColumnSpan = 3;
                    HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                    HeaderGridRow.Cells.Add(HeaderCell);
                }

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 3)
                {
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                    HeaderCell.ColumnSpan = 4;
                    HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                    HeaderGridRow.Cells.Add(HeaderCell);
                }

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 4)
                {
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                    HeaderCell.ColumnSpan = 6;
                    HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                    HeaderGridRow.Cells.Add(HeaderCell);
                }
                //  HeaderCell.ColumnSpan = 5;




                HeaderCell = new TableCell();
                HeaderCell.Text = "Target";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 1;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);



                HeaderCell = new TableCell();
                HeaderCell.Text = "Drop Out";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 3;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);




                HeaderCell = new TableCell();
                HeaderCell.Text = "Working";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 3;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);


                HeaderCell = new TableCell();
                HeaderCell.Text = "TB Newly Joined";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 3;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);


                HeaderCell = new TableCell();
                HeaderCell.Text = "#Village Without TB";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 1;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "#Average TB Joining Days";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 1;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);

                gvEnrollSummary.Controls[0].Controls.AddAt(0, HeaderGridRow);




            }
            if (ViewState["Donar"].ToString() == "6")
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                HeaderGridRow.CssClass = "gridnewheadercss";
                TableCell HeaderCell;



                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 1)
                {
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                    HeaderCell.ColumnSpan = 1;
                    HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                    HeaderGridRow.Cells.Add(HeaderCell);
                }

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 2)
                {
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                    HeaderCell.ColumnSpan = 3;
                    HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                    HeaderGridRow.Cells.Add(HeaderCell);
                }

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 3)
                {
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                    HeaderCell.ColumnSpan = 4;
                    HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                    HeaderGridRow.Cells.Add(HeaderCell);
                }

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 4)
                {
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                    HeaderCell.ColumnSpan = 6;
                    HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                    HeaderGridRow.Cells.Add(HeaderCell);
                }
                //  HeaderCell.ColumnSpan = 5;





                HeaderCell = new TableCell();
                HeaderCell.Text = "Training Month";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                HeaderCell.ColumnSpan = 1;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Foundation day events for Team Balika";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                HeaderCell.ColumnSpan = 2;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);




                HeaderCell = new TableCell();
                HeaderCell.Text = "Meena Munch Training for Team Balika";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 2;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "TB Training on Mapping of OOSC";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 2;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);



                HeaderCell = new TableCell();
                HeaderCell.Text = "TB Training Volunteer Engagement (VE)";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 2;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);


                HeaderCell = new TableCell();
                HeaderCell.Text = "TB VE Orientation";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 2;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);



                HeaderCell = new TableCell();
                HeaderCell.Text = "TB-PRI Training";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 2;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);



                HeaderCell = new TableCell();
                HeaderCell.Text = "Team Balika One Day Orientation";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 2;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);


                HeaderCell = new TableCell();
                HeaderCell.Text = "Team Balika Residential Training - ENR + SMC";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 2;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);



                HeaderCell = new TableCell();
                HeaderCell.Text = "Team Balika review meeting on GKP";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 2;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);




                HeaderCell = new TableCell();
                HeaderCell.Text = "Team Balika Training on Balsabha & LSE";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 2;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);


                HeaderCell = new TableCell();
                HeaderCell.Text = "Team Balika Training on D2D";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 2;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Team Balika Training on GKP-L0";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 2;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);


                HeaderCell = new TableCell();
                HeaderCell.Text = "Team Balika Training on GKP-L1";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 2;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);



                HeaderCell = new TableCell();
                HeaderCell.Text = "Team Balika Training on GKP-L2";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 2;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);


                HeaderCell = new TableCell();
                HeaderCell.Text = "Team Balika Training on GKP-L3";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 2;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);


                HeaderCell = new TableCell();
                HeaderCell.Text = "Team Balika Training on Soft Skills & EDP";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 2;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 2;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);
                gvEnrollSummary.Controls[0].Controls.AddAt(0, HeaderGridRow);




            }
        }
    }
    private void GenerateExcelNewOut(string FIleName)
    {
        try
        {



            DataTable dt = Session["Enroll123"] as DataTable;
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
                HttpContext.Current.Response.Write("<td colspan='13' ' style='text-align:Center;border:.2pt solid windowtext;'>Enrolment Against Target </td>");

                HttpContext.Current.Response.Write("</tr>");
                HttpContext.Current.Response.Write("<td colspan='13' ' style='text-align:Left;border:.2pt solid windowtext;'>Date :" + DateTime.Now + " </td>");

                HttpContext.Current.Response.Write("</tr>");
                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 1)
                {
                    HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  width:2%;'> </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>D2D Target</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>D2D Target-Current Year Enrolment</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>D2D Target- Last Year Enrolment</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Current Year -Out of Target D2D Enrolment</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Enrolment OOD2D</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Total Enrolment</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Ineligible</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Remaining D2D Target</th>");
                }
                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 2)
                {
                    HttpContext.Current.Response.Write("<th class='header' colspan='3'  rowspan='1' style='" + HeaderStyle + "  width:2%;'> </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>D2D Target</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>D2D Target-Current Year Enrolment</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>D2D Target- Last Year Enrolment</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Current Year -Out of Target D2D Enrolment</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Enrolment OOD2D</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Total Enrolment</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Ineligible</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Remaining D2D Target</th>");
                }
                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 3)
                {
                    HttpContext.Current.Response.Write("<th class='header' colspan='4'  rowspan='1' style='" + HeaderStyle + "  width:2%;'> </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>D2D Target</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>D2D Target-Current Year Enrolment</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>D2D Target- Last Year Enrolment</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Current Year -Out of Target D2D Enrolment</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Enrolment OOD2D</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Total Enrolment</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Ineligible</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Remaining D2D Target</th>");
                }

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 4)
                {
                    HttpContext.Current.Response.Write("<th class='header' colspan='6'  rowspan='1' style='" + HeaderStyle + "  width:2%;'> </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>D2D Target</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>D2D Target-Current Year Enrolment</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>D2D Target- Last Year Enrolment</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Current Year -Out of Target D2D Enrolment</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Enrolment OOD2D</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Total Enrolment</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Ineligible</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Remaining D2D Target</th>"); ;
                }

                HttpContext.Current.Response.Write("</tr>");
                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 1)
                {
                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> District Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");

                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("</tr>");
                }


                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 2)
                {
                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> District Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> EG Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Admin Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");

                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("</tr>");
                }

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 3)
                {
                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> District Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> EG Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Admin Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Cluster Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");

                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");

                    HttpContext.Current.Response.Write("</tr>");
                }
                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 4)
                {
                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> District Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> EG Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Admin Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Cluster Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Village Code</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Village Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");

                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> F</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> M</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Total</th>");
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

                HttpContext.Current.Response.Write("<tr>");
                for (int J = 0; J < 1; J++)
                {
                    if (Convert.ToInt32(ddlGrouping.SelectedValue) == 1)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0)
                            {
                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                            }
                            else
                            {
                                string Col = "[" + dt.Columns[c].ColumnName + "]";
                                int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                    }
                    if (Convert.ToInt32(ddlGrouping.SelectedValue) == 2)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0 || c == 2 || c == 1)
                            {
                                if (c == 2)
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                                }
                                else
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                                }
                            }
                            else
                            {
                                string Col = "[" + dt.Columns[c].ColumnName + "]";
                                int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                    }
                    if (Convert.ToInt32(ddlGrouping.SelectedValue) == 3)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0 || c == 2 || c == 1 || c == 3)
                            {
                                if (c == 3)
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                                }
                                else
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                                }
                            }
                            else
                            {
                                string Col = "[" + dt.Columns[c].ColumnName + "]";
                                int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                    }
                    if (Convert.ToInt32(ddlGrouping.SelectedValue) == 4)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0 || c == 2 || c == 1 || c == 3 || c == 4 || c == 5)
                            {
                                if (c == 5)
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                                }
                                else
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                                }
                            }
                            else
                            {
                                string Col = "[" + dt.Columns[c].ColumnName + "]";
                                int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                    }
                }
                HttpContext.Current.Response.Write("</tr>");

                //HttpContext.Current.Response.Write("<tr>");
                //HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
                //HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
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
    private void GenerateExcelNew(string FIleName)
    {
        try
        {



            DataTable dt = Session["Enroll123"] as DataTable;
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
                HttpContext.Current.Response.Write("<td colspan='13' ' style='text-align:Center;border:.2pt solid windowtext;'>Enrolment Govt and Donor Report </td>");

                HttpContext.Current.Response.Write("</tr>");
                HttpContext.Current.Response.Write("<td colspan='13' ' style='text-align:Left;border:.2pt solid windowtext;'>Date :" + DateTime.Now + " </td>");

                HttpContext.Current.Response.Write("</tr>");
                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 1)
                {
                    HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  width:2%;'> </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>F</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>M</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>Total</th>");
                }
                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 2)
                {
                    HttpContext.Current.Response.Write("<th class='header' colspan='3'  rowspan='1' style='" + HeaderStyle + "  width:2%;'> </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>F</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>M</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>Total</th>");
                }
                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 3)
                {
                    HttpContext.Current.Response.Write("<th class='header' colspan='4'  rowspan='1' style='" + HeaderStyle + "  width:2%;'> </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>F</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>M</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>Total</th>");
                }

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 4)
                {
                    HttpContext.Current.Response.Write("<th class='header' colspan='6'  rowspan='1' style='" + HeaderStyle + "  width:2%;'> </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>F</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>M</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>Total</th>");
                }

                HttpContext.Current.Response.Write("</tr>");
                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 1)
                {
                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> District Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Class 1</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Class 2 to 5</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Class 6 to 8</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Class 1</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Class 2 to 5</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Class 6 to 8</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Class 1</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Class 2 to 5</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Class 6 to 8</th>");
                    HttpContext.Current.Response.Write("<th class='header'style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");
                    HttpContext.Current.Response.Write("</tr>");
                }


                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 2)
                {
                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> District Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> EG Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Admin Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Class 1</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Class 2 to 5</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Class 6 to 8</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Class 1</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Class 2 to 5</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Class 6 to 8</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Class 1</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Class 2 to 5</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Class 6 to 8</th>");
                    HttpContext.Current.Response.Write("<th class='header'style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");
                    HttpContext.Current.Response.Write("</tr>");
                }

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 3)
                {
                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> District Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> EG Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Admin Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Cluster Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Class 1</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Class 2 to 5</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Class 6 to 8</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Class 1</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Class 2 to 5</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Class 6 to 8</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Class 1</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Class 2 to 5</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Class 6 to 8</th>");
                    HttpContext.Current.Response.Write("<th class='header'style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");
                    HttpContext.Current.Response.Write("</tr>");
                }
                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 4)
                {
                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> District Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> EG Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Admin Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Cluster Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Village Code</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Village Name</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Class 1</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Class 2 to 5</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Class 6 to 8</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Class 1</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Class 2 to 5</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Class 6 to 8</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Class 1</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Class 2 to 5</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Class 6 to 8</th>");
                    HttpContext.Current.Response.Write("<th class='header'style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");
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

                HttpContext.Current.Response.Write("<tr>");
                for (int J = 0; J < 1; J++)
                {
                    if (Convert.ToInt32(ddlGrouping.SelectedValue) == 1)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0)
                            {
                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                            }
                            else
                            {
                                string Col = "[" + dt.Columns[c].ColumnName + "]";
                                int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                    }
                    if (Convert.ToInt32(ddlGrouping.SelectedValue) == 2)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0 || c == 2 || c == 1)
                            {
                                if (c == 2)
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                                }
                                else
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                                }
                            }
                            else
                            {
                                string Col = "[" + dt.Columns[c].ColumnName + "]";
                                int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                    }
                    if (Convert.ToInt32(ddlGrouping.SelectedValue) == 3)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0 || c == 2 || c == 1 || c == 3)
                            {
                                if (c == 3)
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                                }
                                else
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                                }
                            }
                            else
                            {
                                string Col = "[" + dt.Columns[c].ColumnName + "]";
                                int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                    }
                    if (Convert.ToInt32(ddlGrouping.SelectedValue) == 4)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0 || c == 2 || c == 1 || c == 3 || c == 4 || c == 5)
                            {
                                if (c == 5)
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                                }
                                else
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                                }
                            }
                            else
                            {
                                string Col = "[" + dt.Columns[c].ColumnName + "]";
                                int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                    }
                }
                HttpContext.Current.Response.Write("</tr>");

                //HttpContext.Current.Response.Write("<tr>");
                //HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
                //HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
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


    private void GenerateExcelTeamBalik(string FIleName)
    {
        try
        {



            DataTable dt = Session["Enroll123"] as DataTable;
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
                HttpContext.Current.Response.Write("<td colspan='15' ' style='text-align:Center;border:.2pt solid windowtext;'>TB Recruitment Report </td>");

                HttpContext.Current.Response.Write("</tr>");
                HttpContext.Current.Response.Write("<td colspan='15' ' style='text-align:Left;border:.2pt solid windowtext;'>Date :" + DateTime.Now + " </td>");

                HttpContext.Current.Response.Write("</tr>");
                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 1)
                {
                    HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  width:2%;'> </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'>Target</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Drop Out</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Working </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>TB Newly Joined </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                }
                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 2)
                {
                    HttpContext.Current.Response.Write("<th class='header' colspan='3'  rowspan='1' style='" + HeaderStyle + "  width:2%;'> </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'>Target</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Drop Out</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Working </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>TB Newly Joined </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                }
                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 3)
                {
                    HttpContext.Current.Response.Write("<th class='header' colspan='4'  rowspan='1' style='" + HeaderStyle + "  width:2%;'> </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'>Target</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Drop Out</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Working </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>TB Newly Joined </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                }

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 4)
                {
                    HttpContext.Current.Response.Write("<th class='header' colspan='6'  rowspan='1' style='" + HeaderStyle + "  width:2%;'> </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'>Target</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Drop Out</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Working </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>TB Newly Joined </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                }

                HttpContext.Current.Response.Write("</tr>");
                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 1)
                {
                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> District Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Target</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Female</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Male</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Drop Out Total</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Female</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Male</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Working Total</th>");

                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Female</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Male</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Working Total</th>");

                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Village Without TB</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Average TB Joining Days</th>");

                    HttpContext.Current.Response.Write("</tr>");
                }


                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 2)
                {
                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> District Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> EG Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Admin Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Target</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Female</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Male</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Drop Out Total</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Female</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Male</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Working Total</th>");

                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Female</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Male</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Working Total</th>");

                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Village Without TB</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Average TB Joining Days</th>");

                    HttpContext.Current.Response.Write("</tr>");
                }

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 3)
                {
                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> District Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> EG Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Admin Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Cluster Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Target</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Female</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Male</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Drop Out Total</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Female</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Male</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Working Total</th>");

                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Female</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Male</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Working Total</th>");

                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Village Without TB</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Average TB Joining Days</th>");
                    HttpContext.Current.Response.Write("</tr>");
                }
                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 4)
                {
                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> District Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> EG Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Admin Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Cluster Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Village Code</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Village Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Target</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Female</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Male</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Drop Out Total</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Female</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Male</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Working Total</th>");

                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> Female</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Male</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Working Total</th>");

                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Village Without TB</th>");
                    HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'>Average TB Joining Days</th>");
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

                HttpContext.Current.Response.Write("<tr>");
                for (int J = 0; J < 1; J++)
                {
                    if (Convert.ToInt32(ddlGrouping.SelectedValue) == 1)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0)
                            {
                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                            }
                            else
                            {
                                string Col = "[" + dt.Columns[c].ColumnName + "]";
                                int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                    }
                    if (Convert.ToInt32(ddlGrouping.SelectedValue) == 2)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0 || c == 2 || c == 1)
                            {
                                if (c == 2)
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                                }
                                else
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                                }
                            }
                            else
                            {
                                string Col = "[" + dt.Columns[c].ColumnName + "]";
                                int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                    }
                    if (Convert.ToInt32(ddlGrouping.SelectedValue) == 3)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0 || c == 2 || c == 1 || c == 3)
                            {
                                if (c == 3)
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                                }
                                else
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                                }
                            }
                            else
                            {
                                string Col = "[" + dt.Columns[c].ColumnName + "]";
                                int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                    }
                    if (Convert.ToInt32(ddlGrouping.SelectedValue) == 4)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0 || c == 2 || c == 1 || c == 3 || c == 4 || c == 5)
                            {
                                if (c == 5)
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                                }
                                else
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                                }
                            }
                            else
                            {
                                string Col = "[" + dt.Columns[c].ColumnName + "]";
                                int sum = 0;
                                if (dt.Columns[c].ColumnName == "Average TB Joining Days")
                                {

                                    sum = Convert.ToInt32(dt.Compute("Avg(" + Col + ")", string.Empty));

                                }
                                else
                                {

                                    sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));
                                }

                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                    }
                }
                HttpContext.Current.Response.Write("</tr>");

                //HttpContext.Current.Response.Write("<tr>");
                //HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
                //HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
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






    private void GenerateExcelTeamBalikTraning(string FIleName)
    {
        try
        {



            DataTable dt = Session["Enroll123"] as DataTable;
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
                HttpContext.Current.Response.Write("<td colspan='25' ' style='text-align:Center;border:.2pt solid windowtext;'>TB Training Report </td>");

                HttpContext.Current.Response.Write("</tr>");
                HttpContext.Current.Response.Write("<td colspan='25' ' style='text-align:Left;border:.2pt solid windowtext;'>Date :" + DateTime.Now + " </td>");

                HttpContext.Current.Response.Write("</tr>");
                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
                HttpContext.Current.Response.Write("<tr style='font-width:bold;height:16%;'>");

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 1)
                {
                    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                }
                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 2)
                {
                    HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'></th>");
                }

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 3)
                {
                    HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'></th>");
                }

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 4)
                {
                    HttpContext.Current.Response.Write("<th class='header' colspan='6' style='" + HeaderStyle + "  width:2%;'></th>");
                }
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90; width:2%;'>	Training Month 	</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  mso-rotate: 90; width:2%;'>	Foundation day events for Team Balika 	</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	Meena Munch Training for Team Balika 	</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	TB Training on Mapping of OOSC 	</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	TB Training Volunteer Engagement (VE) 	</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	TB VE Orientation 	</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	TB-PRI Training 	</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  mso-rotate: 90; width:2%;'>	Team Balika One Day Orientation 	</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	Team Balika Residential Training - ENR + SMC 	</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	Team Balika review meeting on GKP 	</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + " mso-rotate: 90;  width:2%;'>	Team Balika Training on Balsabha & LSE 	</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + " mso-rotate: 90;  width:2%;'>	Team Balika Training on D2D 	</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	Team Balika Training on GKP-L0 	</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	Team Balika Training on GKP-L1 	</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	Team Balika Training on GKP-L2 	</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	Team Balika Training on GKP-L3 	</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	Team Balika Training on Soft Skills & EDP 	</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + " mso-rotate: 90; width:2%;'>	#Training Days	</th>");



                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 1)
                {
                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> District Name</th>");



                }
                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 2)
                {
                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> District Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> EG Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Admin Block Name</th>");


                }

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 3)
                {
                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> District Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> EG Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Admin Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Cluster Name</th>");

                }

                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 4)
                {
                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> District Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> EG Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Admin Block Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Cluster Name</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Village Code</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Village Name</th>");
                }
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Training Month</th>");

                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Female	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Male	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Female	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Male	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Female	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Male	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Female	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Male	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Female	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Male	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Female	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Male	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Female	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Male	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Female	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Male	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Female	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Male	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Female	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Male	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Female	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Male	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Female	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Male	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Female	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Male	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Female	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Male	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Female	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Male	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Female	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	Male	</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>	#Training Days		</th>");


                HttpContext.Current.Response.Write("</tr>");




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

                HttpContext.Current.Response.Write("<tr>");
                for (int J = 0; J < 1; J++)
                {
                    if (Convert.ToInt32(ddlGrouping.SelectedValue) == 1)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0)
                            {
                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                            }
                            else
                            {
                                string Col = "[" + dt.Columns[c].ColumnName + "]";
                                int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                    }
                    if (Convert.ToInt32(ddlGrouping.SelectedValue) == 2)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0 || c == 2 || c == 1 || c == 3)
                            {
                                if (c == 3)
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                                }
                                else
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                                }
                            }
                            else
                            {
                                string Col = "[" + dt.Columns[c].ColumnName + "]";
                                int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                    }
                    if (Convert.ToInt32(ddlGrouping.SelectedValue) == 3)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0 || c == 2 || c == 1 || c == 3 || c == 3)
                            {
                                if (c == 4)
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                                }
                                else
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                                }
                            }
                            else
                            {
                                string Col = "[" + dt.Columns[c].ColumnName + "]";
                                int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                    }
                    if (Convert.ToInt32(ddlGrouping.SelectedValue) == 4)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0 || c == 2 || c == 1 || c == 3 || c == 4 || c == 5 || c == 6)
                            {
                                if (c == 6)
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                                }
                                else
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                                }
                            }
                            else
                            {
                                string Col = "[" + dt.Columns[c].ColumnName + "]";
                                int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                    }
                }
                HttpContext.Current.Response.Write("</tr>");

                //HttpContext.Current.Response.Write("<tr>");
                //HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
                //HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
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


    public void LoadReportEnrollmentTest()
    {

        conditions = "";
        string conditionsCr = "";
        string conditionsmo = "";
        string conditionsDe = "";
        string conditionsAll = "";
        lblTotalCount.Text = "";


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

        foreach (ListItem item in chkCluster.Items)
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
        lblTotalCount.Text = "";
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions += "  and mst5Village.StateCode in(" + ddlStatecode + ") ";
            conditionsAll += "  StateCode in(" + ddlStatecode + ")";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst5Village.DistrictCode in( " + ddlDistrict + ") ";
            conditionsAll += "  and DistCode in(" + ddlDistrict + ") ";
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
            conditions += " and mst5Village.Villagecode in( " + ddlVillage + ") ";

        }


        string FristCon = conditions + conditionsCr;

        if (ViewState["Donar"].ToString() == "7")
        {
            DataTable dt = objMain.AgeWiseEnrollPlan(FristCon, 0);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 1)
                {

                    rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptAgeWiseEducationsPlan.rdlc");
                    ReportDataSource datasource = new ReportDataSource("EducationsPlan", dt);
                    rptD2D.LocalReport.DataSources.Clear();
                    rptD2D.LocalReport.DataSources.Add(datasource);


                    rptD2D.Width = 600;



                    rptD2D.LocalReport.DisplayName = "Enrollplan ";
                }


                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 2)
                {

                    rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptBlockAgeWiseEducationsPlan.rdlc");
                    ReportDataSource datasource = new ReportDataSource("EducationsPlan", dt);
                    rptD2D.LocalReport.DataSources.Clear();
                    rptD2D.LocalReport.DataSources.Add(datasource);


                    rptD2D.Width = 600;



                    rptD2D.LocalReport.DisplayName = "Enrollplan ";
                }
                if (Convert.ToInt32(ddlGrouping.SelectedValue) == 3)
                {

                    rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptPhyAgeWiseEducationsPlan.rdlc");
                    ReportDataSource datasource = new ReportDataSource("EducationsPlan", dt);
                    rptD2D.LocalReport.DataSources.Clear();
                    rptD2D.LocalReport.DataSources.Add(datasource);


                    rptD2D.Width = 600;



                    rptD2D.LocalReport.DisplayName = "Enrollplan ";
                }

            }
        }

        if (ViewState["Donar"].ToString() == "9")
        {

            DataTable dt = objMain.rptEnrollmentAnalayis(FristCon, 0);
            if (dt.Rows.Count > 0)
            {


                rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rpttEnrollmentAnalayis.rdlc");
                ReportDataSource datasource = new ReportDataSource("Enrollment", dt);
                rptD2D.LocalReport.DataSources.Clear();
                rptD2D.LocalReport.DataSources.Add(datasource);
                rptD2D.Width = 600;
                rptD2D.LocalReport.DisplayName = "Enrollplan ";
            }
        }







    }
}