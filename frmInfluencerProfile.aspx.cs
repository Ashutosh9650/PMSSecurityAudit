using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Drawing;
using System.IO;


public partial class frmInfluencerProfile : System.Web.UI.Page
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
               
                //GVMainBind();
                LoadYear();
                LoadUserLeavel();
                FillType();
                FillSocialCat();
                FillDropResone();
                ViewState["Save"] = "Save";
                FillFaimlyCat();
                FillActive(0);
                FillEdu();
                FillDesignation();
                FillReasone();
                btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
                ValdateUserLavel();
                ddInfluencerType.SelectedIndex = 0;
            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }

        }
    }
    public DataTable CreateDataTable()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYear.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYear;
    }
    public void LoadYear()
    {
        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            AlllStateCode();
            ddlState.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlDistrict, null);
            ddlDistrict.SelectedIndex = 1;
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);

            ddlPanchayat.Items.Clear();
            ddlVillage.Items.Clear();
        }
        else
        {
            ddlState.SelectedIndex = 0;
            ddlDistrict.Items.Clear();
            ddlBlock.Items.Clear();
            ddlPanchayat.Items.Clear();
            ddlVillage.Items.Clear();
        }
    }

    public void FillDesignation()
    {
        conditions = "";
        conditions = "LookupFlag ='INP' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlDesignation, "Description", "LookupCode", "Select");



    }
    public void FillActive(Int32 Flag)
    {
        conditions = "";
        conditions = "LookupFlag ='IA' and Active=1 ";
        if (Flag == 1)
        {
            conditions += " and LookupCode=1";
        }
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlWorkEx, "Description", "LookupCode", "Select");



    }
    public void FillDropResone()
    {
        //conditions = "";
        //conditions = "LookupFlag ='TMR' and Active=1 ";
        //objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "Description", "asc", ddlStatusReasone, "Description", "LookupCode", "Select");



    }
    public void FillReasone()
    {
        //conditions = "";
        //conditions = "LookupFlag ='RTB' and Active=1 ";
        //objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "Description", "asc", ddlReason, "Description", "LookupCode", "Select");



    }
    public void FillType()
    {
        conditions = "";
        conditions = "LookupFlag ='IT' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddInfluencerType, "Description", "LookupCode", "Select");



    }
    public void FillSocialCat()
    {
        conditions = "";
        conditions = "LookupFlag ='SC' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlCategory, "Description", "LookupCode", "Select");



    }
    public void FillEdu()
    {
        conditions = "";
        conditions = "LookupFlag ='EIT' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlEducation, "Description", "LookupCode", "Select");



    }

    public void FillFaimlyCat()
    {
        conditions = "";
        conditions = "LookupFlag ='FOT' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddloccu, "Description", "LookupCode", "Select");



    }


    public void ValdateUserLavel()
    {

        string strQry = "";
        string Cond = "Module='TeamBalika' ";
        strQry = "Select * from MstUserRight  where " + Cond + " and Role_Id=" + Session["user_level"].ToString() + "   ";


        DataTable dtRole = objMain.LoadData(strQry);

        if (dtRole.Rows.Count > 0)
        {
            vADD = Convert.ToBoolean(dtRole.Rows[0]["AddStatus"].ToString());
            vVerify = Convert.ToBoolean(dtRole.Rows[0]["verify_Status"].ToString());
            vDelete = Convert.ToBoolean(dtRole.Rows[0]["Delete_status"].ToString());
            ViewState["vADD"] = vADD;
            ViewState["vVerify"] = vVerify;
            ViewState["vDelete"] = vDelete;
        }
        if (vDelete == true)
        {

            btnDelete.Visible = true;
        }
        else
        {

            btnDelete.Visible = false;
        }

        if (vADD == true)
        {
            btnAdd.Enabled = true;
            btnsave.Enabled = true;
            //lblMain.Text = "School Information Campaign";
        }
        else
        {
            btnAdd.Enabled = false;
            btnsave.Enabled = false;
        }
        //if (Session["user_level"].ToString() == "1")
        //{
        //    btnAdd.Enabled = true;
        //    btnDelete.Enabled = true;
        //    lblMain.Text = "School Information Campaign";
        //}
        if (vVerify == true)
        {

            btnsave.Enabled = true;

            //lblMain.Text = "School Information Campaign(Verify)";
            //stid.Style.Add("background-color", "#FFFFE0");
            //stmid.Style.Add("background-color", "#FFFFE0");
            //stinfid.Style.Add("background-color", "#FFFFE0");
            //stAvailability.Style.Add("background-color", "#FFFFE0");
            //stsmc.Style.Add("background-color", "#FFFFE0");
            //stdr.Style.Add("background-color", "#FFFFE0");
            //srlm.Style.Add("background-color", "#FFFFE0");
            //stbdfid.Style.Add("background-color", "#FFFFE0");
        }
        if (vVerify == true || vADD == true)
        {
            btnsave.Enabled = true;

        }
        else
        {
            btnsave.Enabled = false;

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
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
           // objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            //conditions = "UserName='" + Session["username"].ToString() + "' ";

            //string strQry1 = "  SELECT distinct mst1State.StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode inner join mst1State on mst1State.StateCode=mst2District.StateCode where   " + conditions + "   order by StateName   ";
            //DataTable dtTb = objMain.LoadData(strQry1);
            //objComman.BindDLLMasterTableVillage("mst1State", "StateCode,StateName", dtTb, conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "Select");

        //    ddlDistrict.SelectedIndex = 0;


            ddlState.SelectedIndex = 0;
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            ddlState.SelectedIndex = 1;
            ddlState.Enabled = false;
            ddlDistrict.Enabled = false;
        }


        if (Session["user_level_Role"].ToString() == "1")
        {
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            //conditions = "";
            //conditions = "StateCode ='" + ddlState.SelectedValue + "' ";
            //objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");
            conditions = "StateCode ='" + ddlState.SelectedValue + "'  and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

            //string conditions1 = "StateCode ='" + ddlState.SelectedValue + "' ";

            //DataTable dtTb = objMain.LoadData(" SELECT DistrictCode,  dbo.TitleCase(upper(DistrictName)) as  DistrictName FROM [mst2District] where  " + conditions + "   order by DistrictName ");



            //objComman.BindDLLMasterTableVillage("mst2District", "DistrictCode,DistrictName", dtTb, conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            //ddlDistrict.SelectedIndex = 0;

            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }

        else
        {
            conditions = "";
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and Fyear= '" + ddlYear.SelectedItem.Text + "' ";
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

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
   
   
    public void FillCBState()
    {
        conditions = "";
        objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");

        pnlMain.Enabled = false;

    }
    public void FillCBDist()
    {

        //conditions = "";
        //if (Session["user_level_Role"].ToString() == "1")
        //{

        //    conditions = "StateCode ='" + ddlState.SelectedValue + "' and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        //}
        //else if (Session["user_level_Role"].ToString() == "2")
        //{
        //    conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode ='" + Session["DistrictCode"].ToString() + "'  and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        //}
        //else
        //{
        //    conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";


        //}
        DataTable dtDistrict;
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "StateCode in('" + ddlState.SelectedValue + "') and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = " mst2District.StateCode in('" + ddlState.SelectedValue + "') and UserName='" + Session["username"].ToString() + "' ";
        }
        else
        {
            conditions = "StateCode  in('" + ddlState.SelectedValue + "') and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";


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

        objComman.BindDLLMasterTableVillage("mst2District", "DistrictCode,DistrictName", dtDistrict, conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

     
      //  objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");



    }
  
    //public void FillCBDist()
    //{
    //    conditions = "";
    //    conditions = "StateCode ='" + ddlState.SelectedValue + "'";
    //    objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");



    //}
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBBock();
        pnlMain.Enabled = false;
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBCluster();
        pnlMain.Enabled = false;
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillage();
        pnlMain.Enabled = false;
    }
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlMain.Enabled = false;
        //Unique();
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
    public void FillCBCluster()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "'";
        objComman.BindDLLSelectAll("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "Select");



    }
    public void FillCVillage()
    {
        conditions = "";
        //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        //objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--Select--");

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

    private void GVMainBind()
    {

        string str = "";
       
        if (ddlState.SelectedValue != null && ddlState.SelectedIndex > 0)
        {
            str = "where mst5Village.StateCode='" + ddlState.SelectedValue.ToString() + "'";
        }
        if (ddlDistrict.SelectedValue != null && ddlDistrict.SelectedIndex > 0)
        {
            str = str + "and mst5Village.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "'";
        }

        if (ddlBlock.SelectedValue != null && ddlBlock.SelectedIndex > 0)
        {
            str = str + "and mst5Village.BlockCode='" + ddlBlock.SelectedValue.ToString() + "'";
        }

        if (ddlPanchayat.SelectedValue != null && ddlPanchayat.SelectedIndex > 1)
        {
            str = str + "and mst5Village.PanchayatCode='" + ddlPanchayat.SelectedValue.ToString() + "'";
        }

        if (ddlVillage.SelectedValue != null && ddlVillage.SelectedIndex > 0)
        {
            str = str + "and mst5Village.VillageCode='" + ddlVillage.SelectedValue.ToString() + "'";
        }
        DataTable dtmstM = objMain.LoadData(" SELECT ICCode as TBCode,UniqueCode, ICName as TBName,mst5Village.VillageCode +'-'+ [ICCode] as UniqueId FROM [dbo].[mstInfluencerProfile] inner join mst5Village on mst5Village.VillageCode=mstInfluencerProfile.VillageCode 	or  mst5Village.refVillage16=mstInfluencerProfile.VillageCode	or  mst5Village.refVillage17=mstInfluencerProfile.VillageCode	or  mst5Village.refVillage18=mstInfluencerProfile.VillageCode	or  mst5Village.refVillage19=mstInfluencerProfile.VillageCode	or  mst5Village.refVillage20=mstInfluencerProfile.VillageCode	 	or  mst5Village.refVillage21=mstInfluencerProfile.VillageCode	 left join mst1State on mst1State.StateCode=mst5Village.StateCode left join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode   left join (select distinct blockcode,blockname from mst3Block) blk ON mst5Village.BlockCode = blk.BlockCode LEFT JOIN (select distinct PanchayatCode,PanchayatName from mstPanchayat) phy  ON mst5Village.PanchayatCode  = phy.PanchayatCode  " + str + " ");

        //DataTable dt = SqlHelper.GetDataTable(strcon, CommandType.Text, "select schoolcode, Name,PrincipalName,PrincipalContact from mstSchool");
        if (dtmstM.Rows.Count > 0)
        {
            GVMain.DataSource = dtmstM;
            GVMain.DataBind();
            ViewState["Serach"] = dtmstM;
        }
        else
        {
            GVMain.DataSource = null;
            GVMain.DataBind();
            ViewState["Serach"] = "";
        }
    }
    
   public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxHeight)
    {
        var ratio = (double)maxHeight / image.Height;
        var newWidth = (int)(image.Width * ratio);
        var newHeight = (int)(image.Height * ratio);
        var newImage = new Bitmap(newWidth, newHeight);
        using (var g = Graphics.FromImage(newImage))
        {
            g.DrawImage(image, 0, 0, newWidth, newHeight);
        }
        return newImage;
    }

    public bool InterventionSql_Injection(string RVal)
    {
        SqlInjection objAudit = new SqlInjection();
        bool injection = false;


        injection = objAudit.CheckInputBool(RVal);

        return injection;

    }
    public static List<Control> GetAllControls(List<Control> controls, Type t, Control parent /* can be Page */)
    {
        foreach (Control c in parent.Controls)
        {
            if (c.GetType() == t)
                controls.Add(c);
            if (c.HasControls())
                controls = GetAllControls(controls, t, c);
        }
        return controls;
    }
    public string SetTextBoxFocusSelect(Page page)
    {
        string ALlTestBoxValue = "";
        List<Control> list = new List<Control>();
        list = GetAllControls(list, typeof(TextBox), page);
        foreach (Control ctl in list)
        {
            if (ctl.GetType() == typeof(TextBox))
            {
                ((TextBox)ctl).Attributes.Add("onfocus", "this.select()");
                string TempVari = ((TextBox)ctl).Text;
                if (TempVari.Length > 0)
                {
                    ALlTestBoxValue += TempVari + "  ";
                }
            }
        }
        return ALlTestBoxValue;
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {

        string RVal = SetTextBoxFocusSelect(this.Page);
        if (!InterventionSql_Injection(RVal))
        {
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Spurious input detected. Data rejected')</script>", false);

            return;
        }
        Save_Update(0);
    }

    protected void btnSumbit_Click(object sender, EventArgs e)
    {
        string RVal = SetTextBoxFocusSelect(this.Page);
        if (!InterventionSql_Injection(RVal))
        {
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Spurious input detected. Data rejected')</script>", false);

            return;
        }
        Save_Update(0);
    }
    private void Save_Update(int SchoolCode)
    {
        if (ddlDistrict.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select District')</script>", false);
            return;
        }

        if (ddlBlock.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Block')</script>", false);
            return;
        }
        if (ddlPanchayat.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Panchayat')</script>", false);
            return;
        }
        if (ddlVillage.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Village')</script>", false);
            return;
        }
        if (Convert.ToInt32(ddlDob.SelectedValue) == 2 && txtAge.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Age!!')</script>", false);
          
         
            this.txtAge.Focus();
            return ;
        }
       if (Convert.ToInt32(ddInfluencerType.SelectedValue)==2)
       {
           if (ddlInfuName.SelectedIndex<=0)
           {
               ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Influencer Replacement Name !!')</script>", false);


               this.ddlInfuName.Focus();
               return;
           }
       }


       if (Convert.ToInt32(ddlDesignation.SelectedValue) == 99 && txtDegOther.Text == "")
       {
           ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Other Designation!!  ')</script>", false);


           this.txtDegOther.Focus();
           return;
       }
       if (Convert.ToInt32(ddloccu.SelectedValue) == 99 && txtOccOther.Text == "")
       {
           ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Other Occupation!! ')</script>", false);


           this.txtOccOther.Focus();
           return;
       }
       if (Convert.ToInt32(ddlWorkEx.SelectedValue) == 1)
       {
           if (txtActivieDate.Text == "")
           {
               ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Active Date !! ')</script>", false);


               this.txtActivieDate.Focus();
               return;
           }
       }
       if (Convert.ToInt32(ddlWorkEx.SelectedValue) == 2 )
       {

           string strQry = "SELECT * FROM [dbo].[mstInfluencerProfile]   where VillageCode='" + ddlVillage.SelectedValue + "' and Active=1  ";


           DataTable dtRole = objMain.LoadData(strQry);
           if (dtRole.Rows.Count > 0)
           {
               if (dtRole.Rows.Count == 6)
               {
                   ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('We can not inactive less than 6 members!! ')</script>", false);


                   this.txtDropDate.Focus();
                   return;
               }
           }

           if (txtDropDate.Text == "")
           {
               ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Inactive Date !! ')</script>", false);


               this.txtDropDate.Focus();
               return;
           }
           if (txtReason.Text == "")
           {
               ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Inactive Reason!!')</script>", false);


               this.txtReason.Focus();
               return;
           }
       }
        string Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtName.Text);
        string FatherName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtFatherName.Text);
      
      
     


        //string DateofJoining1;
        //string[] b = DateofJoining1.Split('/');
        //string DateofJoining = b[2] + '-' + b[1]  +'-' + b[0];


        //string DropOutData = txtDropDate.Text;
        //string[] D;
        //string DropOutDate;
        //if (txtDropDate.Text != "")
        //{
        //    D = DropOutData.Split('/');
        //    DropOutDate = D[2] + '-' + D[1] + '-' + D[0];
        //}
        //else
        //{
        //    DropOutDate = "1900-01-01";
        //}
        DateTime DOB;
        DateTime AsDob;
        Int32 Age = 0;
        Int32 mmonth = 0;
        int WrokExp = 0;
    
        int mainResult=0;
        string type = "";
        string strMainIDNo = "";
    
       
        if (Convert.ToInt32(ddlDob.SelectedValue) == 1)
        {
            string DateB = txtDate.Text;
            string[] a = DateB.Split('/');
            string BithDate = a[2] + '-' + a[1] + '-' + a[0];



            Age = DateTime.Now.Year - Convert.ToInt32(a[2]);
            DOB = Convert.ToDateTime(a[2] + '-' + a[1] + '-' + a[0]);

            Int32 iyear = Convert.ToInt32(a[2]) + Age;
            string dyear = iyear.ToString();

            AsDob = DOB;


            if (Age < 18 || Age > 80)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 18 and 80 years')</script>", false);


                    this.txtAge.Focus();
                    return;

                }
            

           

        }
        else
        {
            string DateB = txtDate.Text;
            string[] a = DateB.Split('/');
            string BithDate = a[2] + '-' + a[1] + '-' + a[0];

            Age = Convert.ToInt32(txtAge.Text);
            AsDob = Convert.ToDateTime(a[2] + '-' + a[1] + '-' + a[0]);

            Int32 iyear = Convert.ToInt32(a[2]) - Age;
            string dyear = iyear.ToString();
            DOB = Convert.ToDateTime(dyear + '-' + a[1] + '-' + a[0]);

            Int32 Total = 0;
                //Convert.ToInt32(Convert.ToInt32(b[2])-iyear);

            if (Age < 18 || Age > 80)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 18 and 80 years')</script>", false);


                this.txtAge.Focus();
                return;

            }


        }

     
            if (ViewState["Save"].ToString() == "Save")
            {
                DataTable dtCheck = objMain.LoadData(" SELECT * FROM [dbo].[mstInfluencerProfile]  inner join mst5Village on  mst5Village.VillageCode=mstInfluencerProfile.VillageCode or  mst5Village.refVillage16=mstInfluencerProfile.VillageCode	or  mst5Village.refVillage17=mstInfluencerProfile.VillageCode	or  mst5Village.refVillage18=mstInfluencerProfile.VillageCode	or  mst5Village.refVillage19=mstInfluencerProfile.VillageCode	or  mst5Village.refVillage20=mstInfluencerProfile.VillageCode  	or  mst5Village.refVillage21=mstInfluencerProfile.VillageCode			  where ICName='" + Name + "' and   mst5Village.VillageCode='" + ddlVillage.SelectedValue + "' ");
                
                
                if (dtCheck.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Influence Name Allready Exit')</script>", false);
                    return;
                }

                //DataTable dtCheckRe = objMain.LoadData(" SELECT * FROM [dbo].[mstInfluencerProfile]  inner join mst5Village on  mst5Village.VillageCode=mstInfluencerProfile.VillageCode or  mst5Village.refVillage16=mstInfluencerProfile.VillageCode	or  mst5Village.refVillage17=mstInfluencerProfile.VillageCode	or  mst5Village.refVillage18=mstInfluencerProfile.VillageCode	or  mst5Village.refVillage19=mstInfluencerProfile.VillageCode	or  mst5Village.refVillage20=mstInfluencerProfile.VillageCode  	or  mst5Village.refVillage21=mstInfluencerProfile.VillageCode			  where ICReplacmentCode='" + ddlInfuName.SelectedValue + "' and len(ICReplacmentCode)>5 and   mst5Village.VillageCode='" + ddlVillage.SelectedValue + "' ");


                //if (dtCheckRe.Rows.Count > 0)
                //{
                //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Replacement Influencer is Already Replaced.Please select Another Replacement !!')</script>", false);
                //    return;
                //}
                Unique();
                string TBCode = ViewState["TBCode"].ToString();
                string schoolod = ViewState["NumNo"].ToString();
               
                
              
               
                ViewState["Save"] = "fff";

                
                strMainIDNo = objMain.Generate_RandomString(8);
                ViewState["TMCode"] = strMainIDNo;
                type = "I";

                #region Attach image
                //System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(FileuploadAttach.PostedFile.InputStream);
                //System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 81);

                string INActiveDate;

              string ActivieDate = "";
              if (txtActivieDate.Text != "")
              {
                  string ActivieDate1 = txtActivieDate.Text;
                  string[] b = ActivieDate1.Split('/');
                   ActivieDate = b[2] + '-' + b[1] + '-' + b[0];

              }
              else
              {
                  ActivieDate = "1900-01-01";
              }
              if (txtDropDate.Text != "")
              {
                  string AINActiveDate1 = txtDropDate.Text;
                  string[] b = AINActiveDate1.Split('/');
                  INActiveDate = b[2] + '-' + b[1] + '-' + b[0];

              }
              else
              {
                  INActiveDate = "1900-01-01";
              }
               
                string ICReplacmentCode="";
                if (Convert.ToInt32(ddInfluencerType.SelectedValue)==2)
                {
                    ICReplacmentCode=ddlInfuName.SelectedValue;
                }

                #endregion
                mainResult = InfluencerProfile(strMainIDNo, schoolod, ddInfluencerType.SelectedValue, TBCode, ddlVillage.SelectedValue, Name, Convert.ToInt32(ddlGender.SelectedValue), FatherName, Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlEducation.SelectedValue), Convert.ToInt32(ddloccu.SelectedValue), Convert.ToInt32(ddlDob.SelectedValue), DOB, Age, AsDob, type, Session["username"].ToString(), Convert.ToInt32(ddlDesignation.SelectedValue), txtDegOther.Text, txtOccOther.Text, Convert.ToDateTime(ActivieDate), Convert.ToDateTime(INActiveDate), txtReason.Text, ICReplacmentCode, ddlWorkEx.SelectedValue, txtContact.Text);

         
                if (mainResult > 0)
                {
                   

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                    GVMainBind();
                    txtIDNO.Text = TBCode; 
                }
            }
            else
            {
                type = "U";
              
                  #region Attach image
                
                //  string sFileDir = Request.PhysicalApplicationPath + "ApplyLeaveDoc\\";
                string Fullfilename = Convert.ToString(ViewState["ImagePath"]);

                string INActiveDate;

                string ActivieDate = "";
                if (txtActivieDate.Text != "")
                {
                    string ActivieDate1 = txtActivieDate.Text;
                    string[] b = ActivieDate1.Split('/');
                    ActivieDate = b[2] + '-' + b[1] + '-' + b[0];

                }
                else
                {
                    ActivieDate = "1900-01-01";
                }
                if (txtDropDate.Text != "")
                {
                    string AINActiveDate1 = txtDropDate.Text;
                    string[] b = AINActiveDate1.Split('/');
                    INActiveDate = b[2] + '-' + b[1] + '-' + b[0];

                }
                else
                {
                    INActiveDate = "1900-01-01";
                }

                string ICReplacmentCode = "";
                if (Convert.ToInt32(ddInfluencerType.SelectedValue) == 2)
                {
                    ICReplacmentCode = ddlInfuName.SelectedValue;
                }
               
                #endregion
                mainResult = InfluencerProfile(ViewState["TMCode"].ToString(), "", ddInfluencerType.SelectedValue, "", ddlVillage.SelectedValue, Name, Convert.ToInt32(ddlGender.SelectedValue), FatherName, Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlEducation.SelectedValue), Convert.ToInt32(ddloccu.SelectedValue), Convert.ToInt32(ddlDob.SelectedValue), DOB, Age, AsDob, type, Session["username"].ToString(), Convert.ToInt32(ddlDesignation.SelectedValue), txtDegOther.Text, txtOccOther.Text, Convert.ToDateTime(ActivieDate), Convert.ToDateTime(INActiveDate), txtReason.Text, ICReplacmentCode, ddlWorkEx.SelectedValue, txtContact.Text);

         
              //  mainResult = objMain.SaveDataTeamBalika(ViewState["TMCode"].ToString(), "", "", ddlVillage.SelectedValue, Name, Convert.ToInt32(ddlGender.SelectedValue), FatherName, Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlEducation.SelectedValue), Convert.ToInt32(ddloccu.SelectedValue), Convert.ToInt32(ddlDob.SelectedValue), DOB, Age, AsDob, Session["username"].ToString());

            
                if (mainResult > 0)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Update sucessfully')</script>", false);
                    GVMainBind();
                }
       
            }
           
        

    }
    public int InfluencerProfile(string strMainIDNo, string TcodeSerial, string INType, string Tcode, string VillageCode, string TBName, int Gender, string strFatherName, int SocialCategory, int EducationLevel, int FamilyOccupation, int DOBAvailable, DateTime DOB, int AgeAson, DateTime AsOnDate, string flag, string createby, Int32 Designation, string DesignationOther, string FamilyOccupationOther, DateTime ActiveDate, DateTime InActiveDate, string InActiveReason, string ICReplacmentCode, string Active,string MobileNo)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
		{
			new SqlParameter("@UniqueCode", strMainIDNo),
			new SqlParameter("@TBCode", Tcode),
            new SqlParameter("@INType", INType),
			new SqlParameter("@TBName", TBName),
			new SqlParameter("@VillageCode", VillageCode),
			new SqlParameter("@Gender", Gender),
			new SqlParameter("@FatherMotherName", strFatherName),
			new SqlParameter("@SocialCategory", SocialCategory),
			new SqlParameter("@EducationLevel", EducationLevel),
			new SqlParameter("@FamilyOccupation", FamilyOccupation),
			new SqlParameter("@DOBAvailable", DOBAvailable),
			new SqlParameter("@DOB", DOB),
			new SqlParameter("@AgeAson", AgeAson),
			new SqlParameter("@AsOnDate", AsOnDate),		
			new SqlParameter("@flag", flag), 	          
			new SqlParameter("@TcodeSerial", TcodeSerial), 	          
         
			
			new SqlParameter("@createby", createby),
              new SqlParameter("@Designation", Designation),
            new SqlParameter("@DesignationOther", DesignationOther),
            new SqlParameter("@FamilyOccupationOther", FamilyOccupationOther),
               new SqlParameter("@ActiveDate", ActiveDate),
                  new SqlParameter("@InActiveDate", InActiveDate),
                   new SqlParameter("@InActiveReason", InActiveReason),
                    new SqlParameter("@ICReplacmentCode", ICReplacmentCode),
                       new SqlParameter("@Active", Active),
                         new SqlParameter("@MobileNo", MobileNo),
		};
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateInfluencerProfile", cmdParameters);
    }

    protected void ddloccu_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddloccu.SelectedValue) == 99)
        {
            divOccOther.Visible = true;
        }
        else
        {
            divOccOther.Visible = false;
        }
    }
    protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlDesignation.SelectedValue) == 99)
        {
            divOther.Visible = true;
        }
        else
        {
            divOther.Visible = false;
        }
    }
    
    protected void ddInfluencerTyp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddInfluencerType.SelectedValue) == 2)
        {
            DataTable dtCheck = objMain.LoadData(" SELECT UniqueCode,ICName FROM [dbo].[mstInfluencerProfile]  inner join mst5Village on  mst5Village.VillageCode=mstInfluencerProfile.VillageCode or  mst5Village.refVillage16=mstInfluencerProfile.VillageCode	or  mst5Village.refVillage17=mstInfluencerProfile.VillageCode	or  mst5Village.refVillage18=mstInfluencerProfile.VillageCode	or  mst5Village.refVillage19=mstInfluencerProfile.VillageCode	or  mst5Village.refVillage20=mstInfluencerProfile.VillageCode  	or  mst5Village.refVillage21=mstInfluencerProfile.VillageCode			  where Active=2 and mst5Village.VillageCode='" + ddlVillage.SelectedValue + "' ");
            objComman.BindDLLMasterTable("mstSchool", "ICName,UniqueCode", dtCheck, conditions, "ICName", "asc", ddlInfuName, "ICName", "UniqueCode", "Select");

            divType.Visible = true;
            ddlInfuName.Enabled = true;
        }
        else
        {
            ddlInfuName.Items.Clear();
            ddlInfuName.Enabled = false;
            divType.Visible = false;
        }
    }
    protected void ddlWork_SelectedIndexChanged(object sender, EventArgs e)
    {
       
            if (Convert.ToInt32(ddlWorkEx.SelectedValue) == 2)
            {
                rdate.Visible = true;
                rregion.Visible = true;
                DivActive.Visible = false;
            }
            else
            {

                rdate.Visible = false;
                rregion.Visible = false;
                DivActive.Visible = true;
            }
       
    }

    protected void ddlWorkingStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void ddlDob_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlDob.SelectedValue) == 1)
        {
            lblDob.Text = "DOB";
            lblAge.Enabled = false;
            txtAge.Enabled = false;
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDate.Enabled = true;
        }
        else
        {
            txtDate.Enabled = false;
            DateTime ydate=new DateTime(DateTime.Now.Year, 05, 01);

            txtDate.Text = ydate.ToString("dd/MM/yyyy");
            lblDob.Text = "As On";
            lblAge.Enabled = true;
            txtAge.Enabled = true;
        }
    }
    private void RefreshControl()
    {
        #region RefreshControl
     //   txtday.Text = "";
        ViewState["TMCode"] = null;
        ViewState["TBCode"] = null;
        ViewState["ImagePath"] = null;
    
       txtIDNO.Text = "Auto generated number";
        txtName.Text = string.Empty;
        txtDate.Text = string.Empty;
        ddlDob.SelectedIndex =2;
        DateTime ydate = DateTime.Today;
        //txtJoingDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtDate.Text = ydate.ToString("dd/MM/yyyy");
        ddlWorkEx.SelectedIndex = 0;
        txtDate.Enabled = false; 
        txtFatherName.Text = string.Empty;
        txtContact.Text = string.Empty;
        txtAge.Text = string.Empty;
        txtOccOther.Text = "";
        txtDropDate.Text = "";
        txtActivieDate.Text = "";
        txtDegOther.Text = "";
     
        ddlDesignation.SelectedIndex = 0;
        txtReason.Text = "";
        ddlInfuName.Items.Clear();
     //   txtDuartion.Text = string.Empty;
        divType.Visible = false;
        rregion.Visible = false;
        rdate.Visible = false;
        ddlGender.SelectedIndex = 0;
        ddlEducation.SelectedIndex = 0;
        ddloccu.SelectedIndex = 0;
        ddlCategory.SelectedIndex = 0;
        //ddlReason.SelectedIndex = 0;
        //ddlSours.SelectedIndex = 0;
        //txtMonth.Text = "";
        //txtMotherName.Text = "";
        

         
        ViewState["Save"] = "Save";
    
        ViewState["TMCode"] = null;
        #endregion
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select District')</script>", false);
            return;
        }

        if (ddlBlock.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Block')</script>", false);
            return;
        }
        if (ddlPanchayat.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Panchayat')</script>", false);
            return;
        }
        if (ddlVillage.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Village')</script>", false);
            return;
        }
        pnlMain.Enabled = true;
        FillActive(1);
        RefreshControl();
        btnsave.Visible = true;
       // ddInfluencerType.Enabled = true;
        rdate.Visible = false;
        ddlInfuName.Items.Clear();
        ViewState["Save"] = "Save";
        //Unique();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        
        if (ViewState["TMCode"].ToString() != null)
        {
            objMain.DeleteTM(ViewState["TMCode"].ToString());
            GVMainBind();
        }
    }

    protected void GVMain_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "GVUIO")
        {
            int iIndex = Convert.ToInt32(e.CommandArgument);
            string TBCode = GVMain.DataKeys[iIndex]["UniqueCode"].ToString();
            FillControls(TBCode);
            ViewState["Save"] = "Edit";
          
            pnlMain.Enabled = true;
           
            for (int i = 0; i < GVMain.Rows.Count; i++)
            {
                GridViewRow RowD = GVMain.Rows[i];
                if (i % 2 == 0)
                {
                    RowD.BackColor = Color.White;
                }
                else
                {
                    RowD.BackColor = Color.FromArgb(245, 245, 245);
                }

            }
            GridViewRow row = GVMain.Rows[iIndex];
            row.BackColor = Color.LightYellow;
        }
    }
    private void FillControls(string pSchoolCOde)
    {
        DataTable dtmstM = null;

        dtmstM = objMain.LoadData(" select *   FROM [dbo].[mstInfluencerProfile] inner join mst5Village on mst5Village.VillageCode=mstInfluencerProfile.VillageCode where UniqueCode ='" + pSchoolCOde + "'");
     
        if (dtmstM.Rows.Count > 0)
        {
            
            #region School

            ddInfluencerType.Enabled = false;
            FillActive(0);
            ViewState["TMCode"] = pSchoolCOde;
            txtIDNO.Text = dtmstM.Rows[0]["ICCode"].ToString();
            txtName.Text = dtmstM.Rows[0]["ICName"].ToString().Trim();
            ddlGender.SelectedValue = dtmstM.Rows[0]["Gender"].ToString();
            ddloccu.SelectedValue = dtmstM.Rows[0]["FamilyOccupation"].ToString();
            ddloccu_SelectedIndexChanged(ddloccu, null);
            ddInfluencerType.SelectedValue = dtmstM.Rows[0]["INType"].ToString();
            ddInfluencerTyp_SelectedIndexChanged(ddInfluencerType, null);
            ddlInfuName.Enabled = false;
            if (Convert.ToInt32(ddInfluencerType.SelectedValue) == 2)
            {
                ddlInfuName.SelectedValue = dtmstM.Rows[0]["ICReplacmentCode"].ToString();
            }
            else
            {
                ddlInfuName.Items.Clear();
            }

            ddlEducation.SelectedValue = dtmstM.Rows[0]["EducationLevel"].ToString();
            ddlDesignation.SelectedValue = dtmstM.Rows[0]["Designation"].ToString();
            ddlDesignation_SelectedIndexChanged(ddlDesignation, null);
           // ddlReason.SelectedValue = dtmstM.Rows[0]["ReasonForTBChoice"].ToString();
            //ddlSours.SelectedValue = dtmstM.Rows[0]["RecruitmentReferalInfo"].ToString();
            
            ddlCategory.SelectedValue = dtmstM.Rows[0]["SocialCategory"].ToString();
            ddlWorkEx.SelectedValue = dtmstM.Rows[0]["Active"].ToString();
            ddlWork_SelectedIndexChanged(ddlWorkEx, null);
            if (Convert.ToInt32(ddlWorkEx.SelectedValue) == 1)
            {
                btnsave.Visible = true;
            }
            else
            {
                btnsave.Visible = false;
            }
            txtFatherName.Text = dtmstM.Rows[0]["FatherName"].ToString().Trim();
         //   txtMotherName.Text = dtmstM.Rows[0]["MotherName"].ToString().Trim();
            txtContact.Text = dtmstM.Rows[0]["MobileNo"].ToString().Trim();
            txtDegOther.Text = dtmstM.Rows[0]["DesignationOther"].ToString().Trim();
            txtOccOther.Text = dtmstM.Rows[0]["FamilyOccupationOther"].ToString().Trim();
            txtReason.Text = dtmstM.Rows[0]["InActiveReason"].ToString().Trim();
          //  txtDuartion.Text = "";
          //  txtMonth.Text = ""; 

            if (dtmstM.Rows[0]["ActiveDate"].ToString() == "01/01/1900 00:00:00" || dtmstM.Rows[0]["ActiveDate"].ToString()=="")
            {
                txtActivieDate.Text = "";
            }
            else
            {
                DateTime DateJoing = Convert.ToDateTime(dtmstM.Rows[0]["ActiveDate"].ToString());
               txtActivieDate.Text = DateJoing.ToString("dd/MM/yyy");
                
            }

            if (dtmstM.Rows[0]["InActiveDate"].ToString() == "01/01/1900 00:00:00" || dtmstM.Rows[0]["InActiveDate"].ToString() == "")
            {
                txtDropDate.Text = "";
            }
            else
            {
                DateTime DateJoing = Convert.ToDateTime(dtmstM.Rows[0]["InActiveDate"].ToString());
                txtDropDate.Text = DateJoing.ToString("dd/MM/yyy");

            }

            ddlDob.SelectedValue = dtmstM.Rows[0]["DOBAvailable"].ToString();
          
           
            if (Convert.ToInt32(ddlDob.SelectedValue) == 1)
            {
                 DateTime dob= Convert.ToDateTime(dtmstM.Rows[0]["DOB"].ToString());
                 txtDate.Text = dob.ToString("dd/MM/yyy");
                lblDob.Text = "DOB";
                lblAge.Enabled = false;
                txtAge.Enabled = false;
                txtAge.Text = "";
                txtDate.Enabled = true;
            }
            else
            {
                lblDob.Text = "As On";
                DateTime ydate = DateTime.Today;
                txtDate.Text = ydate.ToString("dd/MM/yyyy");
                //txtAge.Text = dtmstM.Rows[0]["AgeAson"].ToString();
                //string DateB = dtmstM.Rows[0]["Createdate"].ToString();
                //string[] a = DateB.Split('/');

                //Int32 iyear = Convert.ToInt32(ddlYear.SelectedValue) - Convert.ToInt32(dtmstM.Rows[0]["AgeAson"].ToString());
                //string dyear = iyear.ToString();
                //DateTime DOB = Convert.ToDateTime(dyear + '-' + a[1] + '-' + a[0]);


                //txtDate.Text = DOB.ToString("dd/MM/yyy");
                 lblAge.Enabled = true;
                txtAge.Enabled = true;
                txtDate.Enabled = false;
            }
            #endregion
        }


     
        
    }
    protected void txtSearchName_Click(object sender, EventArgs e)
    {
        DataTable dt = ViewState["Serach"] as DataTable;
        string strFilter = "";

        string str = "TBName";

        string str1 = "TBCode";
        DataTable dtfilter = dt.Copy();


        strFilter = str + " like '%" + txtSearchName.Text.Trim() + "%' or   ";
        strFilter += str1 + " like '%" + txtSearchName.Text.Trim() + "%'   ";
    
        //dtSoSaleOrder.Select(txtSearch.SelectedValue.ToString() + " like '" + txtSearch.Text + "%'";


        dtfilter.DefaultView.RowFilter = strFilter;
        dtfilter.DefaultView.Sort = "TBName asc";
        GVMain.DataSource = dtfilter.DefaultView.ToTable();
        GVMain.DataBind();
       
    }
    protected void txtJoingDate_OnTextChanged(object sender, EventArgs e)
    {
        DataTable dt = objMain.LoadData("Select * from mst2District where DistrictCode ='" + ddlDistrict.SelectedValue + "'");
        if (dt.Rows.Count > 0)
        {
            HdnStartYear.Text = dt.Rows[0]["StartYear"].ToString();
        }
       
    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {

        GVMainBind();
        pnlMain.Enabled = false;
    }

    protected void btnAdd_Click1(object sender, EventArgs e)
    {

        // ddllevel_selectindexchange(sender, e);
    }

    protected void GV_Project_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVMain.PageIndex = e.NewPageIndex;
        if (ViewState["Serach"] != null)
        {
            DataTable dt = ViewState["Serach"] as DataTable;
            GVMain.DataSource = dt;
            GVMain.DataBind();
        }

    }
    public void Unique()
    {
        if (ViewState["Save"].ToString() == "Save")
        {
            if (ddlVillage.SelectedIndex > 0)
            {
                Int32 mNewNo = 0;
                string strAlias;
                string strQry = " Select top 1 isnull(max(Serial),0) as Serial from mstInfluencerProfile inner join mst5Village on  mst5Village.VillageCode=mstInfluencerProfile.VillageCode 	or  mst5Village.refVillage16=mstInfluencerProfile.VillageCode	or  mst5Village.refVillage17=mstInfluencerProfile.VillageCode	or  mst5Village.refVillage18=mstInfluencerProfile.VillageCode	or  mst5Village.refVillage19=mstInfluencerProfile.VillageCode	or  mst5Village.refVillage20=mstInfluencerProfile.VillageCode	 	or  mst5Village.refVillage21=mstInfluencerProfile.VillageCode		 inner join mst3Block on  mst3Block.BlockCode=mst5Village.BlockCode where mst5Village.VillageCode='" + ddlVillage.SelectedValue + "'   ";
                //string strQry = " Select top 1 Serial from tblDTD   order by Serial desc ";
                DataTable dt = objMain.LoadData(strQry);

                string strQry1 = " Select EGVillageCode,VillageCode  from mst5Village where VillageCode='" + ddlVillage.SelectedValue + "' ";
                DataTable dtVillage = objMain.LoadData(strQry1);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Serial"].ToString() == "" || dt.Rows[0]["Serial"].ToString() == "-1")
                    {
                        mNewNo += 1;
                        strAlias = mNewNo.ToString().PadLeft(5, '0');
                        ViewState["TBCode"] = "ACM" + "-" + dtVillage.Rows[0]["EGVillageCode"] + "-" + strAlias;
                        ViewState["NumNo"] = strAlias;
                    }
                    else
                    {
                        mNewNo = Convert.ToInt32(dt.Rows[0]["Serial"].ToString());
                        mNewNo += 1;
                        strAlias = mNewNo.ToString().PadLeft(5, '0');

                        ViewState["NumNo"] = strAlias;
                        ViewState["TBCode"] = "ACM" + "-" + dtVillage.Rows[0]["EGVillageCode"] + "-" + strAlias;

                    }

                }
                else
                {
                    mNewNo += 1;
                    strAlias = mNewNo.ToString().PadLeft(5, '0');
                    ViewState["TBCode"] = "ACM" + "-" + strAlias;
                    ViewState["NumNo"] = strAlias;
                }
            }
        }

    }


}