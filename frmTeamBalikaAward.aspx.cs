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


public partial class frmTeamBalikaAward : System.Web.UI.Page
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
                LoadData();
                //FillSocialCat();
                //FillDropResone();
                ViewState["Save"] = "Save";
                //FillFaimlyCat();
                //FillEdu();
                //FillSours();
                //FillReasone();
                btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
                ValdateUserLavel();
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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

  
    public void ClearData()
    {

        ddlAwardLevel.SelectedIndex = 0;
        ddlAwardtype.SelectedIndex = 0;
        ddlCalltype.SelectedIndex = 0;
        txtIndividual.Text = "";
        txtAwardName.Text = "";
        IScall.Visible = false;
        IScall1.Visible = false;
        IScall2.Visible = false;
        IScall3.Visible = false;
    }
    public void LoadData()
    {
        conditions = "";
        conditions = "LookupFlag ='AWD' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlAwardtype, "Description", "LookupCode", "Select");

        conditions = "";
        conditions = "LookupFlag ='AWL' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlAwardLevel, "Description", "LookupCode", "Select");



    }
    public void LoadTB()
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
        DataTable dtmstM = objMain.LoadData(" SELECT TBCode,UniqueCode, TBName,TBCode +'-'+ [TBName] as UniqueId FROM [dbo].[mstTeamBalika] inner join mst5Village on mst5Village.VillageCode=mstTeamBalika.VillageCode 	or  mst5Village.refVillage16=mstTeamBalika.VillageCode	or  mst5Village.refVillage17=mstTeamBalika.VillageCode	or  mst5Village.refVillage18=mstTeamBalika.VillageCode	or  mst5Village.refVillage19=mstTeamBalika.VillageCode	or  mst5Village.refVillage20=mstTeamBalika.VillageCode	 	or  mst5Village.refVillage21=mstTeamBalika.VillageCode or  mst5Village.refVillage22=mstTeamBalika.VillageCode	or  mst5Village.refVillage23=mstTeamBalika.VillageCode or  mst5Village.refVillage24=mstTeamBalika.VillageCode or  mst5Village.refVillage25=mstTeamBalika.VillageCode left join mst1State on mst1State.StateCode=mst5Village.StateCode left join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode   left join (select distinct blockcode,blockname from mst3Block) blk ON mst5Village.BlockCode = blk.BlockCode LEFT JOIN (select distinct PanchayatCode,PanchayatName from mstPanchayat) phy  ON mst5Village.PanchayatCode  = phy.PanchayatCode  " + str + " and WorkingStatus=1 ");
        objComman.BindDLLMasterTable("mstSchool", "UniqueId,TBCode", dtmstM, conditions, "TBCode", "asc", ddlTBCode, "UniqueId", "TBCode", "Select");

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

    //public void FillSours()
    //{
    //    conditions = "";
    //    conditions = "LookupFlag ='RSO' and Active=1 ";
    //    objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "Description", "asc", ddlSours, "Description", "LookupCode", "Select");



    //}
    //public void FillDropResone()
    //{
    //    conditions = "";
    //    conditions = "LookupFlag ='TMR' and Active=1 ";
    //    objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlStatusReasone, "Description", "LookupCode", "Select");



    //}
    //public void FillReasone()
    //{
    //    conditions = "";
    //    conditions = "LookupFlag ='RTB' and Active=1 ";
    //    objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "Description", "asc", ddlReason, "Description", "LookupCode", "Select");



    //}
    //public void FillSocialCat()
    //{
    //    conditions = "";
    //    conditions = "LookupFlag ='CAT' and Active=1 ";
    //    objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlCategory, "Description", "LookupCode", "Select");



    //}
    //public void FillEdu()
    //{
    //    conditions = "";
    //    conditions = "LookupFlag ='Edu' and Active=1 ";
    //    objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlEducation, "Description", "LookupCode", "Select");



    //}

    //public void FillFaimlyCat()
    //{
    //    conditions = "";
    //    conditions = "LookupFlag ='FO' and Active=1 ";
    //    objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddloccu, "Description", "LookupCode", "Select");



    //}


    public void ValdateUserLavel()
    {

        string strQry = "";
        string Cond = "Module='Team Balika Award' ";
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
          //  objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
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

            string conditions1 = "StateCode ='" + ddlState.SelectedValue + "' ";

            DataTable dtTb = objMain.LoadData(" SELECT DistrictCode,  dbo.TitleCase(upper(DistrictName)) as  DistrictName FROM [mst2District] where  " + conditions + "   order by DistrictName ");



            objComman.BindDLLMasterTableVillage("mst2District", "DistrictCode,DistrictName", dtTb, conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            ddlDistrict.SelectedIndex = 0;

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
        LoadTB();
        GVMainBind();
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

        string strQry = "  SELECT mst5Village.VillageCode, dbo.TitleCase(upper((mst5Village.VillageName))) + ' (' + dbo.TitleCase(upper(mst5Village.EGvillagecode)) +')'   as VillageName FROM mst5Village INNER JOIN mstPanchayat ON mst5Village.PanchayatCode = mstPanchayat.PanchayatCode where " + conditions + "  order by VillageName   ";
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
            str = "and mst5Village.VillageCode='" + ddlVillage.SelectedValue.ToString() + "'";
        }
        DataTable dtmstM = objMain.LoadData(" SELECT  CONVERT(varchar,Award_Date,103)Calling_Date, mstTeamBalika.TBName,Unique_ID as UniqueId FROM [dbo].[mstTeamBalikaAward] inner join mstTeamBalika on mstTeamBalika.TBCode=mstTeamBalikaAward.TB_Name inner join mst5Village on mst5Village.VillageCode=mstTeamBalikaAward.VillageCode   " + str + " ");

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
        if (!Validation())
            return;

        Save_Update(0);
    }
    private Boolean Validation()
    {
        if (ddlCalltype.SelectedIndex <= 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Award Type')</script>", false);
            return false;
        }


        if (Convert.ToInt32(ddlCalltype.SelectedValue) == 1)
        {
            if (ddlAwardtype.SelectedIndex <= 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Award Type')</script>", false);
                return false;
            }
            if (Convert.ToInt32(ddlAwardtype.SelectedValue) == 7)
            {
                if (txtIndividual.Text == "")
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Individual Award Detail ')</script>", false);
                    return false;
                }
            }
        }
        if (Convert.ToInt32(ddlCalltype.SelectedValue) == 2 ||Convert.ToInt32(ddlCalltype.SelectedValue) == 3 || Convert.ToInt32(ddlCalltype.SelectedValue) == 4 || Convert.ToInt32(ddlCalltype.SelectedValue) == 5)
        {

                if (ddlAwardLevel.SelectedIndex <= 0)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Award Type')</script>", false);
                    return false;
                }
                if (txtAwardName.Text == "")
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Individual Award Detail ')</script>", false);
                    return false;
                }
            }
     
       
        return true;
    }
    protected void btnSumbit_Click(object sender, EventArgs e)
    {
        if (!Validation())
            return;
        Save_Update(0);
    }
    private void Save_Update(int SchoolCode)
    {


       
        string TBCode = ddlTBCode.SelectedValue;
        string CallingDate = txtDate.Text;
      
        

        string Awardtype = "";
        string AwardName = "";
      

        if (Convert.ToInt32(ddlCalltype.SelectedValue) == 1)
        {
            Awardtype = ddlAwardtype.SelectedValue;
            AwardName = txtIndividual.Text;
        }

        if (Convert.ToInt32(ddlCalltype.SelectedValue) == 2 || Convert.ToInt32(ddlCalltype.SelectedValue) == 4 || Convert.ToInt32(ddlCalltype.SelectedValue) == 5 || Convert.ToInt32(ddlCalltype.SelectedValue) == 3)
        {
            Awardtype = ddlAwardLevel.SelectedValue;
            AwardName = txtAwardName.Text;
        }



        

       
        int ID = 0;
     
        if (ViewState["Save"].ToString() == "Save")
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
         {

            new SqlParameter("@Unique_ID", ID),
            new SqlParameter("@Villagecode", ddlVillage.SelectedValue),
              new SqlParameter("@TB_Name", ddlTBCode.SelectedValue),
            new SqlParameter("@Calling_Date",Convert.ToDateTime( CallingDate).ToString("yyyy-MM-dd")),
                              new SqlParameter("@Type", ddlCalltype.SelectedValue),
            new SqlParameter("@AwardType", Awardtype),
            new SqlParameter("@AwardName", AwardName),
           
               new SqlParameter("@Created_By", Session["username"].ToString()),
               new SqlParameter("@flag", "I"),

      };
            int mainResult = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateTeamBalikaAward", cmdParameters);

            if (mainResult > 0)
            {


                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                GVMainBind();

                pnlMain.Enabled = false;
            }
            ViewState["Save"] = "ss";
        
        }
        else
        {

            ID = Convert.ToInt32(ViewState["TMCode"].ToString());
            SqlParameter[] cmdParameters = new SqlParameter[]
      {

            new SqlParameter("@Unique_ID", ID),
           new SqlParameter("@Villagecode", ddlVillage.SelectedValue),
              new SqlParameter("@TB_Name", ddlTBCode.SelectedValue),
            new SqlParameter("@Calling_Date",Convert.ToDateTime( CallingDate).ToString("yyyy-MM-dd")),
                  new SqlParameter("@Type", ddlCalltype.SelectedValue),
            new SqlParameter("@AwardType", Awardtype),
            new SqlParameter("@AwardName", AwardName),

               new SqlParameter("@Created_By", Session["username"].ToString()),
          

               new SqlParameter("@flag", "U"),

    };
            int mainResult = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateTeamBalikaAward", cmdParameters);

            if (mainResult > 0)
            {


                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                GVMainBind();
                pnlMain.Enabled = false;


            }
        }





    }
    public int SaveDataTeamBalika(string strMainIDNo, string TcodeSerial, string Tcode, string VillageCode, string TBName, int Gender, string strFatherName, int SocialCategory, int EducationLevel, int FamilyOccupation, int DOBAvailable, DateTime DOB, int AgeAson, DateTime AsOnDate, int ReasonForTBChoice, int RecruitmentReferalInfo, bool PriorWorkExperience, int TotalPriorWorkExperience, int PriorWorkYearMonth, string Contact, string flag, string Expectation, string Abvision, string MotherName, string ImagePath, DateTime DateofJoining, int dropOutStatus, int DroupOutRe, DateTime DropoutResone, string createby, Int32 TbRecruited)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqueCode", strMainIDNo),
            new SqlParameter("@TBCode", Tcode),
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
            new SqlParameter("@ReasonForTBChoice", ReasonForTBChoice),
            new SqlParameter("@RecruitmentReferalInfo", RecruitmentReferalInfo),
            new SqlParameter("@PriorWorkExperience", PriorWorkExperience),
            new SqlParameter("@TotalPriorWorkExperience", TotalPriorWorkExperience),
            new SqlParameter("@PriorWorkYearMonth", PriorWorkYearMonth),
            new SqlParameter("@Contact", Contact),
            new SqlParameter("@flag", flag),
            new SqlParameter("@Expectation", Expectation),
            new SqlParameter("@Abvision", Abvision),
            new SqlParameter("@MotherName", MotherName),
            new SqlParameter("@TcodeSerial", TcodeSerial),
            new SqlParameter("@ImagePath", ImagePath),
            new SqlParameter("@DateofJoining", DateofJoining),
            new SqlParameter("@dropOutStatus", dropOutStatus),
            new SqlParameter("@DroupOutRe", DroupOutRe),
            new SqlParameter("@DropoutResone", DropoutResone),
            new SqlParameter("@createby", createby),
                new SqlParameter("@TbRecruited", TbRecruited),

                //new SqlParameter("@AlternetPhoneNo", txtxAlternate.Text),
                //new SqlParameter("@IsSmartPhone", ddlSmart.SelectedValue)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateTeamBalikaNew", cmdParameters);
    }


    private void RefreshControl()
    {
        // #region RefreshControl
        // txtday.Text = "";
        // ViewState["TMCode"] = null;
        // ViewState["TBCode"] = null;
        // ViewState["ImagePath"] = null;
        //txtExp.Text="";txtAbv.Text="";
        //txtIDNO.Text = "Auto generated number";
        // txtName.Text = string.Empty;
        // txtDate.Text = string.Empty;
        // ddlDob.SelectedIndex =2;
        // DateTime ydate = new DateTime(DateTime.Now.Year, 05, 01);
        // txtJoingDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        // txtDate.Text = ydate.ToString("dd/MM/yyyy");
        // ddlWorkEx.SelectedIndex = 0;
        // txtDate.Enabled = false; 
        // txtFatherName.Text = string.Empty;
        // txtContact.Text = string.Empty;
        // txtAge.Text = string.Empty;
        // txtxAlternate.Text = string.Empty;
        // ddlSmart.SelectedIndex = 0;
        // txtDuartion.Text = string.Empty;

        // ddlGender.SelectedIndex = 0;
        // ddlEducation.SelectedIndex = 0;
        // ddloccu.SelectedIndex = 0;
        // ddlCategory.SelectedIndex = 0;
        // ddlReason.SelectedIndex = 0;
        // ddlSours.SelectedIndex = 0;
        // txtMonth.Text = "";
        // txtMotherName.Text = "";



        // ViewState["Save"] = "Save";

        // ViewState["TMCode"] = null;
        // #endregion
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
        
        ClearData();
        ddlTBCode.SelectedIndex = 0;
        txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        lblTest.Value = "0";
        //Resone.Visible = false;
        //rdate.Visible = false;
        txtDate.Enabled = true;
        ddlTBCode.Enabled = true;
        ViewState["Save"] = "Save";
        //Unique();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (ViewState["TMCode"].ToString() != null)
        {
            DeleteTM(ViewState["TMCode"].ToString());
            GVMainBind();
        }
    }
    public int DeleteTM(string Schoolcode)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Condition", Schoolcode)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_DmstTeamBalikaAward", cmdParameters);
    }

    protected void GVMain_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "GVUIO")
        {
            
            ClearData();
            ddlTBCode.Enabled = false;
            txtDate.Enabled = false;
            int iIndex = Convert.ToInt32(e.CommandArgument);
            string TBCode = GVMain.DataKeys[iIndex]["UniqueId"].ToString();
            ViewState["TMCode"] = TBCode;
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
        lblTest.Value = "1";
        DataTable dtmstM = null;
       
        dtmstM = objMain.LoadData(" SELECT * FROM [dbo].[mstTeamBalikaAward]  where Unique_ID ='" + pSchoolCOde + "'");

        if (dtmstM.Rows.Count > 0)
        {

            //ddlVillageExit.SelectedValue = dtmstM.Rows[0]["Village_Exit_Status"].ToString();
            DateTime Calling_Date = Convert.ToDateTime(dtmstM.Rows[0]["Created_Date"].ToString());
            ddlTBCode.SelectedValue = dtmstM.Rows[0]["TB_Name"].ToString();
            txtDate.Text = Calling_Date.ToString("dd/MM/yyy");
            ddlCalltype.SelectedValue = dtmstM.Rows[0]["Type"].ToString();
            ddlCalltype_SelectedIndexChanged(ddlCalltype.SelectedValue,null);
            if (Convert.ToInt32(ddlCalltype.SelectedValue) == 1)
            {
                ddlAwardtype.SelectedValue = dtmstM.Rows[0]["AwardType"].ToString();

                ddlAwardtypee_SelectedIndexChanged(ddlCalltype.SelectedValue, null);
                txtIndividual.Text = dtmstM.Rows[0]["AwardName"].ToString();
            }
            if (Convert.ToInt32(ddlCalltype.SelectedValue) == 2 || Convert.ToInt32(ddlCalltype.SelectedValue) == 3 ||Convert.ToInt32(ddlCalltype.SelectedValue) == 4 || Convert.ToInt32(ddlCalltype.SelectedValue) == 5)
            {
                ddlAwardLevel.SelectedValue = dtmstM.Rows[0]["AwardType"].ToString();
                txtAwardName.Text = dtmstM.Rows[0]["AwardName"].ToString();
            }
            //ddlCalltype.SelectedValue = dtmstM.Rows[0]["Call_Type"].ToString();

            //ddlCalltype_SelectedIndexChanged(ddlCalltype, null);
            //if (Convert.ToInt32(ddlCalltype.SelectedValue) == 1)
            //{
            //    ddlIscall.SelectedValue = dtmstM.Rows[0]["Is_Call_Connected"].ToString();

            //    ddlIscall_SelectedIndexChanged(ddlCalltype, null);

            //    string cmeeting = dtmstM.Rows[0]["Objective_Calling"].ToString();



            //    txtCallOther.Text = dtmstM.Rows[0]["Other_Objective_Detail"].ToString();




            //    txtDiscuOther.Text = dtmstM.Rows[0]["Other_Discussion_Detail"].ToString();
            //    txtFeedback.Text = dtmstM.Rows[0]["Feedback_from_TB"].ToString();
            //    txtAnyCritical.Text = dtmstM.Rows[0]["Critical_Concern"].ToString();
            //    ddlIsCrit.SelectedValue = dtmstM.Rows[0]["IsCriticalConcern"].ToString();
            //    txtRemark.Text = dtmstM.Rows[0]["Remark"].ToString();
            //    ddlCriticalStatus.SelectedValue = dtmstM.Rows[0]["Critical_Concern_Status"].ToString();
            //    txtHowCall.Text = dtmstM.Rows[0]["HowManyTimesTriedCalling"].ToString();
            //    ddlReasonNot.SelectedValue = dtmstM.Rows[0]["Reason_not_Completing_Call"].ToString();
            //    txtContact.Text = dtmstM.Rows[0]["Mobile_Number"].ToString();
            //    txtAnyAction.Text = dtmstM.Rows[0]["Any_Action_Point"].ToString();
            //}

            TimeSpan D = (DateTime.Now.Date - Convert.ToDateTime(dtmstM.Rows[0]["Created_Date"]));
            int Days = D.Days;



             if (Session["user_level"].ToString() == "60" || (Session["user_level"].ToString() == "25" || Session["user_level"].ToString() == "61" || Session["user_level"].ToString() == "101") && Days <= 7)
            {
                btnsave.Enabled = true;
                btnDelete.Enabled = true;
            }
            else if (Session["user_level"].ToString() == "1" && Days <= 90)
            {
                btnsave.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                btnsave.Enabled = false;
                btnDelete.Enabled = false;
            }

        }


    
       



    }
    protected void txtSearchName_Click(object sender, EventArgs e)
    {
        DataTable dt = ViewState["Serach"] as DataTable;
        string strFilter = "";

        string str = "TBName";
        DataTable dtfilter = dt.Copy();


        strFilter = str + " like '%" + txtSearchName.Text.Trim() + "%'   ";

        //dtSoSaleOrder.Select(txtSearch.SelectedValue.ToString() + " like '" + txtSearch.Text + "%'";


        dtfilter.DefaultView.RowFilter = strFilter;
        dtfilter.DefaultView.Sort = "TBName asc";
        GVMain.DataSource = dtfilter.DefaultView.ToTable();
        GVMain.DataBind();

    }

    protected void btnSerach_Click(object sender, EventArgs e)
    {


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
  
    protected void ddlCalltype_SelectedIndexChanged(object sender, EventArgs e)
    {
            if (ddlCalltype.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlCalltype.SelectedValue) == 1)
            {


                IScall.Visible = true;
                IScall1.Visible = false;
                IScall2.Visible = false;
                IScall3.Visible = false;

            }
            if (Convert.ToInt32(ddlCalltype.SelectedValue) == 2 || Convert.ToInt32(ddlCalltype.SelectedValue) == 3 || Convert.ToInt32(ddlCalltype.SelectedValue) == 4 || Convert.ToInt32(ddlCalltype.SelectedValue) == 5)
            {
                IScall.Visible = false;
                IScall1.Visible = false;
                IScall2.Visible = true;
                IScall3.Visible = true;


            }

        }
        else
        {
            IScall.Visible = false;
            IScall1.Visible = false;
            IScall2.Visible = false;
            IScall3.Visible = false;
         
        }

    }
    protected void ddlAwardtypee_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (ddlAwardtype.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlAwardtype.SelectedValue) == 7)
            {
                IScall1.Visible = true;
               
            }
           else 
            {
               
                IScall1.Visible = false;
                
            }
        }
        else
        {
            IScall1.Visible = false;
        }
    }
   
}