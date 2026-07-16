using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class FrmRetentionCV : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    string conditions = "";
    Comman objComman = new Comman();
    DataTable dtMain = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {

            if (!IsPostBack)
            {
                LoadYear();
                //     ddlYear_SelectedIndexChanged(ddlYear, null);
                btnApprove.Attributes.Add("onclick", "javascript:return " + "confirm('Please confirm if you want to approve? ')");

                btnReject.Attributes.Add("onclick", "javascript:return " + "confirm('Please confirm if you want to Reject? ')");
                LoadUserLeavel();


                btnReject.Visible = true;
                btnApprove.Visible = true;



            }



        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
    }
    public void LoadYear()
    {
        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 2;
        //}


    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBBock();

    }
    public void FillCBBock()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        else if (Session["user_level_Role"].ToString() == "4")
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 and BlockCode in( " + Session["BlockCode"].ToString() + " ) and FYear ='" + ddlYear.SelectedItem.Text + "' ";
        }
        else
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");



    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
    }
    public void FillCBDist()
    {
        DataTable dtDistrict = null;
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
            if (ddlYear.SelectedValue.ToString() == "2024")
            {

                string strQry1 = "   sELECT distinct mst2District.RefDistrictCode FROM MstusermultipleDist  ";
                strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions + "  and  Fyear='2025-2026'   ";
                DataTable dt = objMain.LoadData(strQry1);
                string gg = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    gg += dt.Rows[i]["RefDistrictCode"].ToString();
                }

                string strQry9 = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where DistrictCode in(" + gg + " ) order by DistrictName   ";
                dtDistrict = objMain.LoadData(strQry9);
                objComman.BindDLLMasterTable("mstSchool", "DistrictName,DistrictCode", dtDistrict, conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            }
            else
            {

                objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            }
        }
        else if (Session["user_level_Role"].ToString() == "3" || Session["user_level_Role"].ToString() == "4")
        {
            if (ddlYear.SelectedValue.ToString() == "2024")
            {
                conditions = " where DistrictCode in('" + Session["NewDistrictCode"].ToString() + "') and mst2District.FYear ='2025-2026' ";
                string strQryq = "  select RefDistrictCode from mst2District " + conditions + "  order by DistrictName   ";
                DataTable dtDistrictw = objMain.LoadData(strQryq);
                string mdis = dtDistrictw.Rows[0]["RefDistrictCode"].ToString();

                string strQry = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where DistrictCode in(" + mdis + " ) order by DistrictName   ";
                dtDistrict = objMain.LoadData(strQry);
                objComman.BindDLLMasterTable("mstSchool", "DistrictName,DistrictCode", dtDistrict, conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            }
            else
            {

                objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            }
        }
        else
        {

            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        }




    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            AlllStateCode();
            ddlState.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlDistrict, null);
            if (Session["user_level_Role"].ToString() == "1")
            {
            }
            else
            {
                ddlDistrict.SelectedIndex = 1;
            }
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);

            ddlVillage.Items.Clear();
        }
        else
        {
            ddlState.SelectedIndex = 0;
            ddlDistrict.Items.Clear();
            ddlBlock.Items.Clear();

            ddlVillage.Items.Clear();
        }
    }

    protected void ddlBlock_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillCBCluster();
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillage();
    }
    public void FillCVillage()
    {
        conditions = "";
        //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        //objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "Select");

        if (ddlPanchayat.SelectedValue.ToString() == "1")
        {
            conditions = "mst5Village.DistrictCode ='" + ddlDistrict.SelectedValue + "'  and mst5Village.BlockCode ='" + ddlBlock.SelectedValue + "'  ";

        }
        else
        {
            conditions = "mst5Village.DistrictCode ='" + ddlDistrict.SelectedValue + "'  and mst5Village.BlockCode ='" + ddlBlock.SelectedValue + "' and  mst5Village.PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";

        }

        string strQry = "  SELECT mst5Village.VillageCode, dbo.TitleCase(upper((mst5Village.VillageName))) + ' (' + dbo.TitleCase(upper(mstPanchayat.PanchayatName)) +')'   as VillageName FROM mst5Village INNER JOIN mstPanchayat ON mst5Village.PanchayatCode = mstPanchayat.PanchayatCode where " + conditions + "  order by VillageName   ";
        DataTable dtVillage = objMain.LoadData(strQry);

        objComman.BindDLLMasterTableVillage("mst5Village", "VillageName,VillageCode", dtVillage, conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "Select");


    }

    public void FillCBCluster()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "'";
        objComman.BindDLLSelectAll("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "Select");



    }
    public void FillCVillagNew()
    {
        conditions = "";

        //string ddlPhan = "";


        //conditions = "";

        //conditions = "DistrictCode in('" + ddlDistrict.SelectedValue + "')  and BlockCode in('" + ddlBlock.SelectedValue + "') ";

        ////conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        ////objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--All-");

        //string strQry = "  SELECT ClusterCode, dbo.TitleCase(upper(ClusterName))  as ClusterName FROM mstCluster where " + conditions + "  order by ClusterName   ";
        //DataTable dtDistrict = objMain.LoadData(strQry);


        //objComman.BindDLLMasterTable("mstSchool", "ClusterCode,ClusterName", dtDistrict, conditions, "ClusterName", "asc", ddlCluster, "ClusterName", "ClusterCode", "Select");



    }
    public void LoadDataBlock(string blockName)
    {


        conditions = "";
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
        {
            string strQry = "";

            strQry = "Select * from mst3Block  where DistrictCode='" + Session["NewDistrictCode"].ToString() + "' and BlockName='" + blockName + "' ";


            DataTable dtBlock = objMain.LoadData(strQry);

            conditions = "  DistrictCode='" + Session["NewDistrictCode"].ToString() + "'  ";



            objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");
            ddlBlock.Enabled = false;
            ddlBlock.SelectedValue = dtBlock.Rows[0]["BlockCode"].ToString();
            Session["BlockName"] = blockName;
            Session["BlockCodeAct"] = dtBlock.Rows[0]["BlockCode"].ToString();
        }
        else
        {

            conditions = conditions + "  DistrictCode='" + Session["NewDistrictCode"].ToString() + "'  and BlockCode ='" + Session["NewBlockCode"].ToString() + "'   and mst2District.FYear ='" + Session["FinYear"].ToString() + "' ";



            objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

            ddlBlock.Enabled = false;

            ddlBlock.SelectedValue = Session["NewBlockCode"].ToString();
            Session["BlockCodeAct"] = Session["NewBlockCode"].ToString();
        }




    }

    public void LoadUserState()
    {
        if (ddlYear.SelectedValue == "2024")
        {
            conditions = "";
            if (Session["user_level_Role"].ToString() == "1")
            {
                conditions = " StateCode in('10','23','6','8','9A','9B','9C') ";
                objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
                ddlState.Enabled = true;
                ddlDistrict.Enabled = true;
            }
            else if (Session["user_level_Role"].ToString() == "2")
            {
                conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
                objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

                ddlState.SelectedIndex = 1;
                ddlState.Enabled = true;
                ddlDistrict.Enabled = true;
            }
            else
            {
                conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
                objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

                ddlState.SelectedIndex = 1;
                ddlState.Enabled = false;
                ddlDistrict.Enabled = false;
            }
        }
        else
        {
            if (Session["user_level_Role"].ToString() == "1")
            {
                conditions = " StateCode in('10','23','6','8','9','99') ";
                objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
                ddlState.Enabled = true;
                ddlDistrict.Enabled = true;
            }
            else if (Session["user_level_Role"].ToString() == "2")
            {
                conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
                objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

                ddlState.SelectedIndex = 1;
                ddlState.Enabled = true;
                ddlDistrict.Enabled = true;
            }
            else
            {
                conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
                objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

                ddlState.SelectedIndex = 1;
                ddlState.Enabled = false;
                ddlDistrict.Enabled = false;
            }

        }






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
            objComman.BindDLLDatatable("mst1State", dtAllState, "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

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
            objComman.BindDLLDatatable("mst1State", dtAllState, "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

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
            objComman.BindDLLDatatable("mst1State", dtAllState, "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");


        }

    }

    public void LoadUserLeavel()
    {
        AlllStateCode();
        if (Session["user_level_Role"].ToString() == "1")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            ddlState.SelectedIndex = 1;
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            ddlState.SelectedIndex = 1;
            ddlState.Enabled = false;
            ddlDistrict.Enabled = true;
        }

        //if (ddlYear.SelectedValue == "2024")
        //{
        //    conditions = "";
        //    if (Session["user_level_Role"].ToString() == "1")
        //    {
        //        conditions = " StateCode in('10','23','6','8','9A','9B','9C') ";
        //        objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
        //        ddlState.Enabled = true;
        //        ddlDistrict.Enabled = true;
        //    }
        //    else if (Session["user_level_Role"].ToString() == "2")
        //    {
        //        conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
        //        objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

        //        ddlState.SelectedIndex = 1;
        //        ddlState.Enabled = true;
        //        ddlDistrict.Enabled = true;
        //    }
        //    else
        //    {
        //        conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
        //        objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

        //        ddlState.SelectedIndex = 1;
        //        ddlState.Enabled = false;
        //        ddlDistrict.Enabled = false;
        //    }
        //}
        //else
        //{
        //    if (Session["user_level_Role"].ToString() == "1")
        //    {
        //        conditions = " StateCode in('10','23','6','8','9','99') ";
        //        objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
        //        ddlState.Enabled = true;
        //        ddlDistrict.Enabled = true;
        //    }
        //    else if (Session["user_level_Role"].ToString() == "2")
        //    {
        //        conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
        //        objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

        //        ddlState.SelectedIndex = 1;
        //        ddlState.Enabled = true;
        //        ddlDistrict.Enabled = true;
        //    }
        //    else
        //    {
        //        conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
        //        objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

        //        ddlState.SelectedIndex = 1;
        //        ddlState.Enabled = false;
        //        ddlDistrict.Enabled = false;
        //    }

        //}


        if (Session["user_level_Role"].ToString() == "1")
        {
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "";
            conditions = "StateCode ='" + ddlState.SelectedValue + "'  and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

            ddlDistrict.SelectedIndex = 0;

            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }

        else
        {

            FillCBDist();
            //conditions = "";
            //conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and Fyear= '" + ddlYear.SelectedItem.Text + "' ";
            //objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");
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
            ddlDistrict.SelectedIndex = 1;
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }





    }


    public void LoadData()
    {
        conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
        objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

        ddlState.SelectedIndex = 1;
        ddlState.Enabled = false;

        conditions = "";
        conditions = "StateCode ='" + Session["StateCode"].ToString() + "'  and DistrictCode ='" + Session["NewDistrictCode"].ToString() + "'   ";
        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

        ddlDistrict.SelectedIndex = 1;
        ddlDistrict.Enabled = false;
        conditions = "";
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "136")
        {
            conditions = "  DistrictCode='" + Session["NewDistrictCode"].ToString() + "' ";



            objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");
            ddlBlock.Enabled = true;
        }
        else
        {

            conditions = conditions + "  DistrictCode='" + Session["NewDistrictCode"].ToString() + "'  and BlockCode ='" + Session["NewBlockCode"].ToString() + "' ";



            objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

            ddlBlock.Enabled = false;

            ddlBlock.SelectedValue = Session["NewBlockCode"].ToString();
        }




    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["Backlk"] = 1;
        Response.Redirect("~/Enrollmentdashboard.aspx");
    }









    protected void btnReport_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/FrmReportActivityClusterSearch.aspx?ID=" + ddlBlock.SelectedValue + "");

    }


    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {

        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
        SavaData();



    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {

        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
        SavaDataRejecj();



    }
    protected void btnDOwnload_Click(object sender, EventArgs e)
    {

        string Con = " and mst5Village.DistrictCode='" + ddlDistrict.SelectedValue + "' ";
        if (ddlBlock.SelectedIndex > 0)
        {
            Con += " and mst5Village.BlockCode='" + ddlBlock.SelectedValue + "' ";
        }


        if (ddlPanchayat.SelectedValue.ToString() == "1")
        {
        }
        else
        {
            if (ddlPanchayat.SelectedIndex > 0)
            {
                Con += " and mst5Village.PanchayatCode='" + ddlPanchayat.SelectedValue + "' ";
            }
        }
        //if (ddlPanchayat.SelectedIndex > 0)
        //{
        //    Con += " and mst5Village.PanchayatCode='" + ddlPanchayat.SelectedValue + "' ";
        //}
        if (ddlVillage.SelectedIndex > 0)
        {
            Con += " and mst5Village.VillageCode='" + ddlVillage.SelectedValue + "' ";
        }

        SqlParameter[] cmdParameters = new SqlParameter[]
       {
            new SqlParameter("@Con",Con),

       };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadReationDataCVReport", cmdParameters);
        if (dt.Rows.Count > 0)
        {
            ExporttoExcel(dt, "Retention Course Correction");
        }

    }

    protected void btnSerach_Click(object sender, EventArgs e)
    {
        if (ddlBlock.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Block')</script>", false);

            return;
        }
        if (Convert.ToString(Session["username"]) != "")
        {
            LoadDataCV();
        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }



    }

    public void LoadDataCV()
    {
        DataTable dt = LoadActivtiyAllClusterWise();


        if (dt.Rows.Count > 0)
        {
            Gv_Profile_Search.DataSource = dt;
            Gv_Profile_Search.DataBind();
            dvMain.Visible = true;
        }
        else
        {
            Gv_Profile_Search.DataSource = null;
            Gv_Profile_Search.DataBind();
            dvMain.Visible = false;
        }
    }
    public DataTable LoadActivtiyAllClusterWise()
    {
        string Con = " and mst5Village.DistrictCode='" + ddlDistrict.SelectedValue + "' ";
        if (ddlBlock.SelectedIndex > 0)
        {
            Con += " and mst5Village.BlockCode='" + ddlBlock.SelectedValue + "' ";
        }
        if (ddlPanchayat.SelectedValue.ToString() == "1")
        {
        }
        else
        {
            if (ddlPanchayat.SelectedIndex > 0)
            {
                Con += " and mst5Village.PanchayatCode='" + ddlPanchayat.SelectedValue + "' ";
            }
        }
        if (ddlVillage.SelectedIndex > 0)
        {
            Con += " and mst5Village.VillageCode='" + ddlVillage.SelectedValue + "' ";
        }

        //if (ddlCluster.SelectedIndex > 0)
        //{
        //    Con += " and mst5Village.ClusterCode='" + ddlCluster.SelectedValue + "' ";
        //}


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con", Con),

        };
        //  return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptEnrollCV", cmdParameters);
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadReationDataCV", cmdParameters);

    }

    //protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string strQry = "";
    //    if (ddlBlock.SelectedIndex > 0)
    //    {
    //        strQry = "   select Villagecode  from MstUser   where UserName='" + ddlBlock.SelectedValue + "' ";
    //        DataTable dtUserVillage = objMain.LoadData(strQry);

    //        string strVillage = dtUserVillage.Rows[0]["Villagecode"].ToString();

    //        conditions = "mst5Village.VillageCode in(" + strVillage + ") ";

    //     //   objComman.BindDLL("mst5Village", "VillageCode,VillageName ", conditions, "", "", ddlVilage, "VillageName", "VillageCode", "Select");


    //    }
    //}


    protected void TestGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //string quantity = e.Row.Cells[3].Text;

            //foreach (TableCell cell in e.Row.Cells)
            //{

            //        cell.BackColor = Color.Red;

            //}
        }
    }
    public void SavaData()
    {
        int Fcount = 0;
        int Flag = 1; int Chcount = 0;
        for (int i = 0; i < Gv_Profile_Search.Rows.Count; i++)
        {
            string FQuesr = " ";
            Label lblUniqueChildCode = (Label)Gv_Profile_Search.Rows[i].FindControl("lblUniqueChildCode");

            CheckBox chkFormName = (CheckBox)Gv_Profile_Search.Rows[i].FindControl("chkFormName");

            Label lblFlag = (Label)Gv_Profile_Search.Rows[i].FindControl("lblFlag");
            Label lblClass = (Label)Gv_Profile_Search.Rows[i].FindControl("lblClass");
            Label lblRe = (Label)Gv_Profile_Search.Rows[i].FindControl("lblRe");
            Label lblAtt = (Label)Gv_Profile_Search.Rows[i].FindControl("lblAtt");

            Label lblCVClassError = (Label)Gv_Profile_Search.Rows[i].FindControl("lblCVClassError");
            Label lblCVLastAttendanceError = (Label)Gv_Profile_Search.Rows[i].FindControl("lblCVLastAttendanceError");
            Label lblregularattendance = (Label)Gv_Profile_Search.Rows[i].FindControl("lblregularattendance");
            Label lblchild_key = (Label)Gv_Profile_Search.Rows[i].FindControl("lblchild_key");


            int CFlag = 0;
            int RFlag = 0;
            if (chkFormName.Checked == true)
            {
                RFlag = Convert.ToInt32(lblFlag.Text);
                if (RFlag == 8)
                {
                    if (lblCVClassError.Text == "Error")
                    {
                        CFlag = 1;
                    }
                    if (lblCVLastAttendanceError.Text == "Error")
                    {
                        CFlag = 2;
                    }
                    if (lblCVClassError.Text == "Error" && lblCVLastAttendanceError.Text == "Error")
                    {
                        CFlag = 3;
                    }

                }
                if (RFlag == 11)
                {
                    CFlag = 1;
                    if (lblregularattendance.Text == "Yes")
                    {
                        CFlag = 2;
                    }


                }
                string Att = "";
                if (lblAtt.Text.Length > 0)
                {
                    Att = Convert.ToDateTime(lblAtt.Text).ToString("yyyy-MM-dd");

                }



                Chcount = Chcount + 1;

                int icount = SaveENromentCV(lblUniqueChildCode.Text, lblchild_key.Text, Convert.ToString(Session["username"]), Flag, CFlag, RFlag, lblClass.Text, Att, Convert.ToInt32(lblRe.Text));
                Fcount = icount;



            }
        }
        if (Fcount > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
            LoadDataCV();
        }
        if (Chcount == 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Unique ID')</script>", false);

        }
    }


    public void SavaDataRejecj()
    {
        string FinalFQuesr = "";
        int Fcount = 0;
        int Chcount = 0;
        for (int i = 0; i < Gv_Profile_Search.Rows.Count; i++)
        {

            CheckBox chkFormName = (CheckBox)Gv_Profile_Search.Rows[i].FindControl("chkFormName");
            Label lblUniqueChildCode = (Label)Gv_Profile_Search.Rows[i].FindControl("lblUniqueChildCode");

            Label lblchild_key = (Label)Gv_Profile_Search.Rows[i].FindControl("lblchild_key");

            if (chkFormName.Checked == true)
            {

                Chcount = Chcount + 1;


                int icount = SaveENromentCV(lblUniqueChildCode.Text, lblchild_key.Text, Convert.ToString(Session["username"]), 2, 0, 0, "", "", 0);

                Fcount = icount;

            }
        }
        if (Fcount > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
            LoadDataCV();
        }
        if (Chcount == 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Unique ID')</script>", false);

        }
    }
    public int SaveENromentCV(string strMainIDNo, string CVUniqueID, string UserName, int Flag, int CFlag, int RFlag, string PresentClass, string LastPresentDate, int RE)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqCode", strMainIDNo),
            new SqlParameter("@CVUniqueID", CVUniqueID),
            new SqlParameter("@UserName", UserName),
               new SqlParameter("@Flag", Flag),
                    new SqlParameter("@CFlag", CFlag),
    new SqlParameter("@RFlag", RFlag),
    new SqlParameter("@PresentClass", PresentClass),
      new SqlParameter("@LastPresentDate", LastPresentDate),
  new SqlParameter("@RE",RE),




        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptRetention2024SVInsertUpdateNew", cmdParameters);
    }


    private void ExporttoExcel(DataTable table, string FileName)
    {
        try
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
                    HttpContext.Current.Response.Write(table.Columns[j]);
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
        catch
        {

            throw;
        }

    }

    //protected void Gv_Profile_Search_OnRowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "GVUIO")
    //    {
    //        int iIndex = Convert.ToInt32(e.CommandArgument);
    //        string VDate = Gv_Profile_Search.DataKeys[iIndex]["VDate"].ToString();
    //        Response.Redirect("./frmMobileVillageProfile.aspx?ID=" + ddlVilage.SelectedValue + "," + ddlBlock.SelectedValue + "," + VDate + "");
    //    }


}

