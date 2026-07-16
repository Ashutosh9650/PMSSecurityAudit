using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class frmReportRejection : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    string conditions = ""; string RowAffect;
    string statecode = string.Empty, Clustercode = string.Empty, Distcode = string.Empty, blockcode = string.Empty, villagecode = string.Empty, dbname = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                LoadUserLeavel();
              
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
            
        }

    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        try
        {
            statecode = ddlState.SelectedValue;
            if (statecode != "")
            {
                FillRejection();

            }
            else
            {

            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    public void LoadUserLeavel()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");

        }
        else
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");

            ddlState.SelectedIndex = 1;
        }


        if (Session["user_level_Role"].ToString() == "1")
        {
        }
        else
        {
            conditions = "";
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode ='" + Session["DistrictCode"].ToString() + "'";
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

            ddlDistrict.SelectedIndex = 1;
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }
    }
    public void FillRejection()
    {
        string condition = "";
        if (ddlState.SelectedIndex > 0)
        {
            conditions = "where v.StateCode='" + ddlState.SelectedValue + "'";
        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions = conditions + "and v.DistrictCode='" + ddlDistrict.SelectedValue + "'";
        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions = conditions + "and v.BlockCode='" + ddlBlock.SelectedValue + "'";
        }
        if (ddlPanchayat.SelectedIndex > 0)
        {

            conditions = conditions + "and v.PanchayatCode='" + ddlPanchayat.SelectedValue + "'";
        }
        if (ddlVillage.SelectedIndex > 0)
        {

            conditions = conditions + "and v.VillageCode='" + ddlVillage.SelectedValue + "'";
        }
        SqlParameter[] para = new SqlParameter[]
            {
               new SqlParameter("@Cond",condition)


            };
        DataTable result = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "EG_Get_RejectionReport", para);
        GV_rejection.DataSource = result;
        GV_rejection.DataBind();
    }
    #region SelectedIndexChanged
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
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
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
    }


    #endregion

    #region Fill Master Data
    public void FillCBState()
    {
        conditions = "";
        objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");
    }
    public void FillCBDist()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "StateCode ='" + ddlState.SelectedValue + "'";
        }
        else
        {
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode=  '" + Session["DistrictCode"].ToString() + "' ";


        }

        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");



    }
    public void FillCBBock()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        if (Session["user_level"].ToString() == "19")
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 and BlockCode=  '" + Session["BlockCode"].ToString() + "'";
        }
        else
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");



    }
    public void FillCBCluster()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "'";
        objComman.BindDLL("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "--Select--");



    }
    public void FillCVillage()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--Select--");



    }
    #endregion
}