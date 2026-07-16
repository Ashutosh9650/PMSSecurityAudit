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


public partial class frmTeamBalikaCalling : System.Web.UI.Page
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

    public void MainClear()
    {
        txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        ddlTBCode.SelectedIndex = 0;
        ddlVillageExit.SelectedIndex = 0;
        ddlCalltype.SelectedIndex = 0;
        IScall.Visible = false;
        pnlNo.Visible = false;
        pnlCall.Visible = false;
        pnlCallNo.Visible = false;
        pnlNo1.Visible = false;
        pnlCall1.Visible = false;

        pnlYCritical.Visible = false;
        pnlNCritical.Visible = false;
        ddlVillageExit.Enabled = true;
        ddlTBCode.Enabled = true;
        ddlCalltype.Enabled = true;
        ddlIscall.Enabled = true;
        ViewState["TMCode"] = "";
    }
    public void ClearData()
    {
        foreach (ListItem item in CBL_bookformat1.Items) { item.Selected = false; }
        foreach (ListItem item in chkOtherDicu.Items) { item.Selected = false; }
        foreach (ListItem item in chkobjCall.Items) { item.Selected = false; }
        foreach (ListItem item in chkobjdiuOther.Items) { item.Selected = false; }
        ddlReasonNot.SelectedIndex = 0;
        txtCallOther.Text = "";
        txtHowCall.Text = "";
        txtContact.Text = "";
        txtAnyAction.Text = "";
        txtIssue.Text = "";
        txtSupport.Text = "";
        txtNoOther.Text = "";
        txtDiscuOther.Text = "";
        txtFeedback.Text = "";
        txtAnyCritical.Text = "";
        txtRemark.Text = "";
        ddlIsCrit.SelectedIndex = 0;
        ddlCriticalStatus.SelectedIndex = 0;
        txtOther1.Text = "";
        txtCritical.Text = "";
        ddlDPO.SelectedIndex = 0;
        txtDBORepark.Text = "";
        ddlCriticalConcern.SelectedIndex = 0;
    }
    public void LoadData()
    {
        string strQry1 = " select LookupCode,Description  from [mstLookup]   where LookupFlag='TMC'  ";
        DataTable dtOther1 = objMain.LoadData(strQry1);
        CBL_bookformat1.DataSource = dtOther1;
        CBL_bookformat1.DataTextField = "Description";
        CBL_bookformat1.DataValueField = "LookupCode";
        CBL_bookformat1.DataBind();
        string strQry2 = " select LookupCode,Description  from [mstLookup]   where LookupFlag='DP'  ";
        DataTable dtOther2 = objMain.LoadData(strQry2);
        chkOtherDicu.DataSource = dtOther2;
        chkOtherDicu.DataTextField = "Description";
        chkOtherDicu.DataValueField = "LookupCode";
        chkOtherDicu.DataBind();
        string strQry3 = " select LookupCode,Description  from [mstLookup]   where LookupFlag='RC'  ";
        DataTable dtOther3 = objMain.LoadData(strQry3);

        objComman.BindDLLMasterTable("mstSchool", "Description,LookupCode", dtOther3, conditions, "LookupCode", "asc", ddlReasonNot, "Description", "LookupCode", "Select");

        string strQry4 = " select LookupCode,Description  from [mstLookup]   where LookupFlag='OOC'  ";
        DataTable dtOther24 = objMain.LoadData(strQry4);
        chkobjCall.DataSource = dtOther24;
        chkobjCall.DataTextField = "Description";
        chkobjCall.DataValueField = "LookupCode";
        chkobjCall.DataBind();


        string strQry5 = " select LookupCode,Description  from [mstLookup]   where LookupFlag='ODP'  ";
        DataTable dtOther5 = objMain.LoadData(strQry5);
        chkobjdiuOther.DataSource = dtOther5;
        chkobjdiuOther.DataTextField = "Description";
        chkobjdiuOther.DataValueField = "LookupCode";
        chkobjdiuOther.DataBind();

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
        string Cond = "Module='Team Balika Calling' ";
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
        DataTable dtmstM = objMain.LoadData(" SELECT  CONVERT(varchar,Calling_Date,103)Calling_Date, mstTeamBalika.TBName,Unique_ID as UniqueId FROM [dbo].[mstTBCalling] inner join mstTeamBalika on mstTeamBalika.TBCode=mstTBCalling.TB_Name inner join mst5Village on mst5Village.VillageCode=mstTBCalling.VillageCode   " + str + " ");

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


        if (Convert.ToInt32(ddlCalltype.SelectedValue) == 1)
        {
            int V1 = 0;
            int V2 = 0;
            foreach (ListItem item in CBL_bookformat1.Items)
            {
                if (item.Selected)
                {
                    if (item.Text == "Other")
                    {

                        txtCallOther.Enabled = true;
                        V1 = 9;
                    }

                }
            }
            if (V1 == 0)
            {
                txtCallOther.Enabled = false;
            }
            foreach (ListItem item in chkOtherDicu.Items)
            {
                if (item.Selected)
                {
                    if (item.Text == "Other")
                    {

                        txtDiscuOther.Enabled = true;
                        V2 = 9;
                    }

                }
            }
            if (V2 == 0)
            {
                txtDiscuOther.Enabled = false;
            }
        }

        if (Convert.ToInt32(ddlCalltype.SelectedValue) == 2)
        {

            int V1 = 0;
            int V2 = 0;
            int V3 = 0;
            int V4 = 0;
            foreach (ListItem item in chkobjCall.Items)
            {
                if (item.Selected)
                {
                    if (item.Value == "9")
                    {

                        txtIssue.Enabled = true;
                        V1 = Convert.ToInt32(item.Value);
                    }
                    if (item.Value == "10")
                    {

                        txtSupport.Enabled = true;
                        V2 = Convert.ToInt32(item.Value);
                    }
                    if (item.Value == "11")
                    {

                        txtNoOther.Enabled = true;
                        V3 = Convert.ToInt32(item.Value);
                    }

                }
                if (V1 == 0)
                {
                    txtIssue.Enabled = false;
                }
                if (V2 == 0)
                {
                    txtSupport.Enabled = false;
                }
                if (V3 == 0)
                {
                    txtNoOther.Enabled = false;
                }
            }
            foreach (ListItem item in chkobjdiuOther.Items)
            {
                if (item.Selected)
                {
                    if (item.Text == "Other")
                    {

                        txtOther1.Enabled = true;
                        V4 = 8;
                    }

                }
            }
            if (V4 == 0)
            {
                txtOther1.Enabled = false;
            }
        }
        //if (ViewState["Save"].ToString() == "Save")
        //{
        //    DataTable dtmstM = objMain.LoadData(" SELECT  CONVERT(varchar,Calling_Date,103)Calling_Date, mstTeamBalika.TBName,Unique_ID as UniqueId FROM [dbo].[mstTBCalling] inner join mstTeamBalika on mstTeamBalika.TBCode=mstTBCalling.TB_Name inner join mst5Village on mst5Village.VillageCode=mstTBCalling.VillageCode  where TB_Name= '" + ddlTBCode.SelectedValue + "' and Calling_Date ='" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "' ");
        //    if (dtmstM.Rows.Count > 0)
        //    {

        //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Record All ready Exit')</script>", false);
        //        return false;
        //    }
        //}
        if (ddlVillageExit.SelectedIndex <= 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select  Village Exit Readiness Status')</script>", false);
            return false;
        }

        if (ddlTBCode.SelectedIndex <= 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select TB Name')</script>", false);
            return false;
        }
        if (ddlCalltype.SelectedIndex <= 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Call Type')</script>", false);
            return false;
        }
        if (Convert.ToInt32(ddlCalltype.SelectedValue) == 1)
        {
            if (ddlIscall.SelectedIndex <= 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Is Call Connected')</script>", false);
                return false;
            }
            if (Convert.ToInt32(ddlIscall.SelectedValue) == 1)
            {
                #region "Yes"
                string commmeeting = "";
                foreach (ListItem item in CBL_bookformat1.Items)
                {
                    if (item.Selected)
                    {

                        commmeeting += "" + item.Value + "" + ",";

                    }
                }
                if (commmeeting.Length > 0)
                {

                }
                else
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Objective of Calling')</script>", false);
                    return false;
                }

                if (txtCallOther.Text == "" && txtCallOther.Enabled == true)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Other')</script>", false);
                    return false;
                }


                string commmeeting1 = "";
                foreach (ListItem item in chkOtherDicu.Items)
                {
                    if (item.Selected)
                    {

                        commmeeting1 += "" + item.Value + "" + ",";

                    }
                }

               

                if (chkY1.Checked ==false && chkY2.Checked == false && chkY3.Checked == false && chkY4.Checked == false && chkY5.Checked == false && chkY6.Checked == false && chkY7.Checked == false && chkY8.Checked == false && chkY9.Checked == false && chkY10.Checked == false && chkY11.Checked == false && chkY12.Checked == false && chkY13.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Other Discussion Points')</script>", false);
                    return false;
                }
               


                if (txtDiscuOther.Text == "" && txtDiscuOther.Enabled == true)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Other')</script>", false);
                    return false;
                }
                if (ddlIsCrit.SelectedIndex <= 0)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select whether the Critical Concern needs to be shared with DPO')</script>", false);
                    return false;
                }
                #endregion
            }

            if (Convert.ToInt32(ddlIscall.SelectedValue) == 2)
            {
                if (txtHowCall.Text == "")
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select How Many Times Tried for Calling')</script>", false);
                    return false;
                }
                if (ddlReasonNot.SelectedIndex <= 0)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Reason for not Completing Call')</script>", false);
                    return false;
                }
            }
        }
        if (Convert.ToInt32(ddlCalltype.SelectedValue) == 2)
        {
            string commmeeting = "";
            foreach (ListItem item in chkobjCall.Items)
            {
                if (item.Selected)
                {

                    commmeeting += "" + item.Value + "" + ",";

                }
            }
            if (commmeeting.Length > 0)
            {

            }
            else
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Objective of Calling')</script>", false);
                return false;
            }

            if (txtIssue.Text == "" && txtIssue.Enabled == true)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Issues Sharing')</script>", false);
                return false;
            }
            if (txtSupport.Text == "" && txtSupport.Enabled == true)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Support Required')</script>", false);
                return false;
            }
            if (txtNoOther.Text == "" && txtNoOther.Enabled == true)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Other')</script>", false);
                return false;
            }

            //string commmeeting4 = "";
            //foreach (ListItem item in chkobjdiuOther.Items)
            //{
            //    if (item.Selected)
            //    {

            //        commmeeting4 += "" + item.Value + "" + ",";

            //    }
            //}
            //if (commmeeting4.Length > 0)
            //{

            //}
            //else
            //{

            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Other Discussion Points')</script>", false);
            //    return false;
            //}
            if (chkN1.Checked == false && chkN2.Checked == false && chkN3.Checked == false && chkN4.Checked == false && chkN5.Checked == false && chkN6.Checked == false && chkN7.Checked == false && chkN8.Checked == false && chkN9.Checked == false && chkN10.Checked == false && chkN11.Checked == false && chkN12.Checked == false && chkN13.Checked == false)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Other Discussion Points')</script>", false);
                return false;
            }
            if (txtOther1.Text == "" && txtOther1.Enabled == true)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Other')</script>", false);
                return false;
            }
            if (ddlDPO.SelectedIndex <= 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select whether the Critical Concern needs to be shared with DPO')</script>", false);
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


        string VillageExitReadinessStatus = ddlVillageExit.SelectedValue;
        string TBCode = ddlTBCode.SelectedValue;
        string CallingDate = txtDate.Text;
        string CallType = ddlCalltype.SelectedValue;
        string IScall = ddlIscall.SelectedValue;

        string ObjectiveofCalling = "";
        string ObjectiveofCallingOther = "";
        string OtherDiscussionPointsOther = "";
        string Feedback = "";
        string AnyCritical = "";
        string CriticalConcern = "";
        string Remark = "";
        string CriticalStatus = "";


        if (Convert.ToInt32(ddlIscall.SelectedValue) == 1)
        {
            foreach (ListItem item in CBL_bookformat1.Items)
            {
                if (item.Selected)
                {

                    ObjectiveofCalling += "" + item.Value + "" + ",";


                }
            }
            if (ObjectiveofCalling.Length > 0)
            {
                ObjectiveofCalling = ObjectiveofCalling.Substring(0, ObjectiveofCalling.LastIndexOf(","));
            }
            ObjectiveofCallingOther = txtCallOther.Text;
        }




        string OtherDiscussionPoints = "";


        if (Convert.ToInt32(ddlIscall.SelectedValue) == 1)
        {
            if (chkY1.Checked == true)
            {
                OtherDiscussionPoints += "1" + ",";
            }
            if (chkY2.Checked == true)
            {
                OtherDiscussionPoints += "2" + ",";
            }
            if (chkY3.Checked == true)
            {
                OtherDiscussionPoints += "3" + ",";
            }
            if (chkY4.Checked == true)
            {
                OtherDiscussionPoints += "4" + ",";
            }
            if (chkY5.Checked == true)
            {
                OtherDiscussionPoints += "5" + ",";
            }
            if (chkY6.Checked == true)
            {
                OtherDiscussionPoints += "6" + ",";
            }
            if (chkY7.Checked == true)
            {
                OtherDiscussionPoints += "7" + ",";
            }
            if (chkY8.Checked == true)
            {
                OtherDiscussionPoints += "8" + ",";
            }
            if (chkY9.Checked == true)
            {
                OtherDiscussionPoints += "9" + ",";
            }
            if (chkY10.Checked == true)
            {
                OtherDiscussionPoints += "10" + ",";
            }
            if (chkY11.Checked == true)
            {
                OtherDiscussionPoints += "11" + ",";
            }
            if (chkY12.Checked == true)
            {
                OtherDiscussionPoints += "12" + ",";
            }
            if (chkY13.Checked == true)
            {
                OtherDiscussionPoints += "13" + ",";
            }
          
            //foreach (ListItem item in chkOtherDicu.Items)
            //{
            //    if (item.Selected)
            //    {

            //        OtherDiscussionPoints += "" + item.Value + "" + ",";


            //    }
            //}
            if (OtherDiscussionPoints.Length > 0)
            {
                OtherDiscussionPoints = OtherDiscussionPoints.Substring(0, OtherDiscussionPoints.LastIndexOf(","));
            }
            OtherDiscussionPointsOther = txtDiscuOther.Text;


            Feedback = txtFeedback.Text;
            AnyCritical = txtAnyCritical.Text;
            CriticalConcern = ddlIsCrit.SelectedValue;
            Remark = txtRemark.Text;
            CriticalStatus = ddlCriticalStatus.SelectedValue;

        }


        //--------------------------------------------------------------------
        string HowManyTimesTriedCalling = txtHowCall.Text;
        string Contact = txtContact.Text;

        string ReasonnotCompletingCall = ddlReasonNot.SelectedValue;
        string AnyAction = txtAnyAction.Text;

        //--------------------------------------------------------------------


        if (Convert.ToInt32(ddlCalltype.SelectedValue) == 2)
        {

            foreach (ListItem item in chkobjCall.Items)
            {
                if (item.Selected)
                {

                    ObjectiveofCalling += "" + item.Value + "" + ",";


                }
            }
            if (ObjectiveofCalling.Length > 0)
            {
                ObjectiveofCalling = ObjectiveofCalling.Substring(0, ObjectiveofCalling.LastIndexOf(","));
            }
            //foreach (ListItem item in chkobjdiuOther.Items)
            //{
            //    if (item.Selected)
            //    {

            //        OtherDiscussionPoints += "" + item.Value + "" + ",";


            //    }
            //}
            if (chkN1.Checked == true)
            {
                OtherDiscussionPoints += "1" + ",";
            }
            if (chkN2.Checked == true)
            {
                OtherDiscussionPoints += "2" + ",";
            }
            if (chkN3.Checked == true)
            {
                OtherDiscussionPoints += "3" + ",";
            }
            if (chkN4.Checked == true)
            {
                OtherDiscussionPoints += "4" + ",";
            }
            if (chkN5.Checked == true)
            {
                OtherDiscussionPoints += "5" + ",";
            }
            if (chkN6.Checked == true)
            {
                OtherDiscussionPoints += "6" + ",";
            }
            if (chkN7.Checked == true)
            {
                OtherDiscussionPoints += "7" + ",";
            }
            if (chkN8.Checked == true)
            {
                OtherDiscussionPoints += "8" + ",";
            }
            if (chkN9.Checked == true)
            {
                OtherDiscussionPoints += "9" + ",";
            }
            if (chkN10.Checked == true)
            {
                OtherDiscussionPoints += "10" + ",";
            }
            if (chkN11.Checked == true)
            {
                OtherDiscussionPoints += "11" + ",";
            }
            if (chkN12.Checked == true)
            {
                OtherDiscussionPoints += "12" + ",";
            }
            if (chkN13.Checked == true)
            {
                OtherDiscussionPoints += "13" + ",";
            }

            if (OtherDiscussionPoints.Length > 0)
            {
                OtherDiscussionPoints = OtherDiscussionPoints.Substring(0, OtherDiscussionPoints.LastIndexOf(","));
            }
            ObjectiveofCallingOther = txtNoOther.Text;
            OtherDiscussionPointsOther = txtOther1.Text;

            Feedback = "";
            AnyCritical = txtCritical.Text;
            CriticalConcern = ddlDPO.SelectedValue;
            Remark = txtDBORepark.Text;
            CriticalStatus = ddlCriticalConcern.SelectedValue;

        }
        int ID = 0;
        string Issue = txtIssue.Text;
        string Support = txtSupport.Text;
        if (ViewState["Save"].ToString() == "Save")
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
         {

            new SqlParameter("@Unique_ID", ID),
            new SqlParameter("@Villagecode", ddlVillage.SelectedValue),
            new SqlParameter("@Village_Exit_Status", ddlVillageExit.SelectedValue),
            new SqlParameter("@TB_Name", ddlTBCode.SelectedValue),
            new SqlParameter("@Calling_Date",Convert.ToDateTime( CallingDate).ToString("yyyy-MM-dd")),
            new SqlParameter("@Call_Type", CallType),
            new SqlParameter("@Is_Call_Connected", IScall),
            new SqlParameter("@Objective_Calling", ObjectiveofCalling),
            new SqlParameter("@Other_Objective_Detail", ObjectiveofCallingOther),
            new SqlParameter("@Other_Discussion_Points", OtherDiscussionPoints),
            new SqlParameter("@Other_Discussion_Detail", OtherDiscussionPointsOther),
            new SqlParameter("@Feedback_from_TB", Feedback),
            new SqlParameter("@Critical_Concern", AnyCritical),
            new SqlParameter("@IsCriticalConcern", CriticalConcern),
            new SqlParameter("@Remark", Remark),
            new SqlParameter("@Critical_Concern_Status", CriticalStatus),
             new SqlParameter("@HowManyTimesTriedCalling", HowManyTimesTriedCalling),


            new SqlParameter("@Reason_not_Completing_Call", ReasonnotCompletingCall),
            new SqlParameter("@Mobile_Number", Contact),

            new SqlParameter("@Any_Action_Point", AnyAction),
            new SqlParameter("@Issue_Detail", Issue),
               new SqlParameter("@Support_Detail", Support),
               new SqlParameter("@Created_By", Session["username"].ToString()),
               new SqlParameter("@flag", "I"),

      };
            int mainResult = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdate_mstTBCalling", cmdParameters);

            if (mainResult > 0)
            {


                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                GVMainBind();
                pnlNo.Enabled = false;
                pnlCall.Enabled = false;
                pnlCallNo.Enabled = false;
                pnlNo1.Enabled = false;
                pnlCall1.Enabled = false;
                ddlCriticalConcern.Enabled = false;
                ddlCriticalStatus.Enabled = false;

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
            new SqlParameter("@Village_Exit_Status", ddlVillageExit.SelectedValue),
            new SqlParameter("@TB_Name", ddlTBCode.SelectedValue),
            new SqlParameter("@Calling_Date",Convert.ToDateTime( CallingDate).ToString("yyyy-MM-dd")),
            new SqlParameter("@Call_Type", CallType),
            new SqlParameter("@Is_Call_Connected", IScall),
            new SqlParameter("@Objective_Calling", ObjectiveofCalling),
            new SqlParameter("@Other_Objective_Detail", ObjectiveofCallingOther),
            new SqlParameter("@Other_Discussion_Points", OtherDiscussionPoints),
            new SqlParameter("@Other_Discussion_Detail", OtherDiscussionPointsOther),
            new SqlParameter("@Feedback_from_TB", Feedback),
            new SqlParameter("@Critical_Concern", AnyCritical),
            new SqlParameter("@IsCriticalConcern", CriticalConcern),
            new SqlParameter("@Remark", Remark),
            new SqlParameter("@Critical_Concern_Status", CriticalStatus),
             new SqlParameter("@HowManyTimesTriedCalling", HowManyTimesTriedCalling),


            new SqlParameter("@Reason_not_Completing_Call", ReasonnotCompletingCall),
            new SqlParameter("@Mobile_Number", Contact),

            new SqlParameter("@Any_Action_Point", AnyAction),
            new SqlParameter("@Issue_Detail", Issue),
               new SqlParameter("@Support_Detail", Support),
               new SqlParameter("@Created_By", Session["username"].ToString()),
               new SqlParameter("@flag", "U"),

    };
            int mainResult = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdate_mstTBCalling", cmdParameters);

            if (mainResult > 0)
            {


                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                GVMainBind();
                pnlNo.Enabled = false;
                pnlCall.Enabled = false;
                pnlCallNo.Enabled = false;
                pnlNo1.Enabled = false;
                pnlCall1.Enabled = false;
                ddlCriticalConcern.Enabled = false;
                ddlCriticalStatus.Enabled = false;


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
        chkY1.Checked = false;
        chkY2.Checked = false;
        chkY3.Checked = false;
        chkY4.Checked = false;
        chkY5.Checked = false;
        chkY6.Checked = false;
        chkY7.Checked = false;
        chkY8.Checked = false;
        chkY9.Checked = false;
        chkY10.Checked = false;
        chkY11.Checked = false;
        chkY12.Checked = false;
        chkY13.Checked = false;

        chkY1.Enabled = true;
        chkY2.Enabled = true;
        chkY3.Enabled = true;
        chkY4.Enabled = true;
        chkY5.Enabled = true;
        chkY6.Enabled = true;
        chkY7.Enabled = true;
        chkY8.Enabled = true;
        chkY9.Enabled = true;
        chkY10.Enabled = true;
        chkY11.Enabled = true;
        chkY13.Enabled = true;
        chkY12.Enabled = true;


        chkN1.Checked = false;
        chkN2.Checked = false;
        chkN3.Checked = false;
        chkN4.Checked = false;
        chkN5.Checked = false;
        chkN6.Checked = false;
        chkN7.Checked = false;
        chkN8.Checked = false;
        chkN9.Checked = false;
        chkN10.Checked = false;
        chkN11.Checked = false;
        chkN12.Checked = false;
        chkN13.Checked = false;

        chkN1.Enabled = true;
        chkN2.Enabled = true;
        chkN3.Enabled = true;
        chkN4.Enabled = true;
        chkN5.Enabled = true;
        chkN6.Enabled = true;
        chkN7.Enabled = true;
        chkN8.Enabled = true;
        chkN9.Enabled = true;
        chkN10.Enabled = true;
        chkN11.Enabled = true;
        chkN13.Enabled = true;
        chkN12.Enabled = true;

        pnlC881.Visible = false;
        pnlNo.Enabled = true;
        pnlCall.Enabled = true;
        pnlCallNo.Enabled = true;
        pnlNo1.Enabled = true;
        pnlCall1.Enabled = true;
        ddlCriticalConcern.Enabled = false;
        ddlCriticalStatus.Enabled = false;
        pnlMain.Enabled = true;
        MainClear();
        ClearData();
        lblTest.Value = "0";
        //Resone.Visible = false;
        //rdate.Visible = false;

        ViewState["Save"] = "Save";
        //Unique();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (ViewState["TMCode"].ToString() != null)
        {
            // objMain.DeleteTM(ViewState["TMCode"].ToString());
            GVMainBind();
        }
    }

    protected void GVMain_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "GVUIO")
        {
            MainClear();
            ClearData();
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
        chkobjCall.Enabled = false;
        dtmstM = objMain.LoadData(" SELECT * FROM [dbo].[mstTBCalling]  where Unique_ID ='" + pSchoolCOde + "'");

        if (dtmstM.Rows.Count > 0)
        {

            ddlVillageExit.SelectedValue = dtmstM.Rows[0]["Village_Exit_Status"].ToString();
            DateTime Calling_Date = Convert.ToDateTime(dtmstM.Rows[0]["Calling_Date"].ToString());
            ddlTBCode.SelectedValue = dtmstM.Rows[0]["TB_Name"].ToString();
            txtDate.Text = Calling_Date.ToString("dd/MM/yyy");
            ddlCalltype.SelectedValue = dtmstM.Rows[0]["Call_Type"].ToString();

            ddlCalltype_SelectedIndexChanged(ddlCalltype, null);
            if (Convert.ToInt32(ddlCalltype.SelectedValue) == 1)
            {
                ddlIscall.SelectedValue = dtmstM.Rows[0]["Is_Call_Connected"].ToString();

                ddlIscall_SelectedIndexChanged(ddlCalltype, null);

                string cmeeting = dtmstM.Rows[0]["Objective_Calling"].ToString();

                string[] meeting = cmeeting.Split(',');

                foreach (string s in meeting)
                {

                   

                    foreach (ListItem item in CBL_bookformat1.Items)
                    {
                        if (item.Value == s)
                        {
                            item.Selected = true;

                        }
                        if (item.Text == "Other")
                        {

                            //  txtCallOther.Enabled = true;
                        }
                    }
                }
                txtCallOther.Text = dtmstM.Rows[0]["Other_Objective_Detail"].ToString();


                string cmeeting1 = dtmstM.Rows[0]["Other_Discussion_Points"].ToString();

                string[] meeting1 = cmeeting1.Split(',');

                foreach (string s in meeting1)
                {
                    if (s == "1")
                    {
                        chkY1.Checked = true;
                    }
                    if (s == "2")
                    {
                        chkY2.Checked = true;
                    }
                    if (s == "3")
                    {
                        chkY3.Checked = true;
                    }
                    if (s == "4")
                    {
                        chkY4.Checked = true;
                    }
                    if (s == "5")
                    {
                        chkY5.Checked = true;
                    }
                    if (s == "6")
                    {
                        chkY6.Checked = true;
                    }
                    if (s == "7")
                    {
                        chkY7.Checked = true;
                    }
                    if (s == "8")
                    {
                        chkY8.Checked = true;
                    }
                    if (s == "9")
                    {
                        chkY9.Checked = true;
                    }
                    if (s == "10")
                    {
                        chkY10.Checked = true;
                    }
                    if (s == "11")
                    {
                        chkY11.Checked = true;
                    }
                    if (s == "12")
                    {
                        chkY12.Checked = true;
                    }
                    if (s == "13")
                    {
                        chkY13.Checked = true;
                    }
                    //foreach (ListItem item in chkOtherDicu.Items)
                    //{
                    //    if (item.Value == s)
                    //    {
                    //        item.Selected = true;

                    //    }
                    //    if (item.Text == "Other")
                    //    {

                    //        // txtCallOther.Enabled = true;
                    //    }
                    //}
                }
                txtDiscuOther.Text = dtmstM.Rows[0]["Other_Discussion_Detail"].ToString();
                txtFeedback.Text = dtmstM.Rows[0]["Feedback_from_TB"].ToString();
                txtAnyCritical.Text = dtmstM.Rows[0]["Critical_Concern"].ToString();
                ddlIsCrit.SelectedValue = dtmstM.Rows[0]["IsCriticalConcern"].ToString();
                txtRemark.Text = dtmstM.Rows[0]["Remark"].ToString();
                ddlCriticalStatus.SelectedValue = dtmstM.Rows[0]["Critical_Concern_Status"].ToString();
                txtHowCall.Text = dtmstM.Rows[0]["HowManyTimesTriedCalling"].ToString();
                ddlReasonNot.SelectedValue = dtmstM.Rows[0]["Reason_not_Completing_Call"].ToString();
                txtContact.Text = dtmstM.Rows[0]["Mobile_Number"].ToString();
                txtAnyAction.Text = dtmstM.Rows[0]["Any_Action_Point"].ToString();
            }
            if (Convert.ToInt32(ddlCalltype.SelectedValue) == 2)
            {

                string cmeeting = dtmstM.Rows[0]["Objective_Calling"].ToString();

                string[] meeting = cmeeting.Split(',');

                foreach (string s in meeting)
                {
                    foreach (ListItem item in chkobjCall.Items)
                    {
                        if (item.Value == s)
                        {
                            item.Selected = true;

                        }
                        if (item.Value == "9")
                        {

                            //  txtIssue.Enabled = true;
                        }
                        if (item.Value == "10")
                        {

                            //  txtSupport.Enabled = true;
                        }
                        if (item.Value == "11")
                        {

                            //txtNoOther.Enabled = true;
                        }

                    }
                }
                txtNoOther.Text = dtmstM.Rows[0]["Other_Objective_Detail"].ToString();
                txtSupport.Text = dtmstM.Rows[0]["Support_Detail"].ToString();
                txtIssue.Text = dtmstM.Rows[0]["Issue_Detail"].ToString();


                string cmeeting1 = dtmstM.Rows[0]["Other_Discussion_Points"].ToString();

                string[] meeting1 = cmeeting1.Split(',');

                foreach (string s in meeting1)
                {
                    if (s == "1")
                    {
                        chkN1.Checked = true;
                    }
                    if (s == "2")
                    {
                        chkN2.Checked = true;
                    }
                    if (s == "3")
                    {
                        chkN3.Checked = true;
                    }
                    if (s == "4")
                    {
                        chkN4.Checked = true;
                    }
                    if (s == "5")
                    {
                        chkN5.Checked = true;
                    }
                    if (s == "6")
                    {
                        chkN6.Checked = true;
                    }
                    if (s == "7")
                    {
                        chkN7.Checked = true;
                    }
                    if (s == "8")
                    {
                        chkN8.Checked = true;
                    }
                    if (s == "9")
                    {
                        chkN9.Checked = true;
                    }
                    if (s == "10")
                    {
                        chkN10.Checked = true;
                    }
                    if (s == "11")
                    {
                        chkN11.Checked = true;
                    }
                    if (s == "12")
                    {
                        chkN12.Checked = true;
                    }
                    if (s == "13")
                    {
                        chkN13.Checked = true;
                    }
                    //foreach (ListItem item in chkobjdiuOther.Items)
                    //{
                    //    if (item.Value == s)
                    //    {
                    //        item.Selected = true;

                    //    }
                    //    if (item.Text == "Other")
                    //    {

                    //        txtCallOther.Enabled = true;
                    //    }
                    //}
                }
                txtOther1.Text = dtmstM.Rows[0]["Other_Discussion_Detail"].ToString();

                txtCritical.Text = dtmstM.Rows[0]["Critical_Concern"].ToString();
                ddlDPO.SelectedValue = dtmstM.Rows[0]["IsCriticalConcern"].ToString();
                txtDBORepark.Text = dtmstM.Rows[0]["Remark"].ToString();
                ddlCriticalConcern.SelectedValue = dtmstM.Rows[0]["Critical_Concern_Status"].ToString();
            }


        }


        pnlNo.Enabled = false;
        pnlCall.Enabled = false;
        pnlCallNo.Enabled = false;
        pnlNo1.Enabled = false;
        pnlCall1.Enabled = false;
        if (Convert.ToInt32(ddlCalltype.SelectedValue) == 2)
        {
            pnlNCritical.Visible = true;
            DateTime Calling_Date = Convert.ToDateTime(dtmstM.Rows[0]["Calling_Date"].ToString());
            if (Calling_Date.ToString("dd/MM/yyy") == DateTime.Now.ToString("dd/MM/yyy"))
            {
                ddlCriticalConcern.Enabled = false;

            }
            else
            {
                ddlCriticalConcern.Enabled = true;

            }


        }
        if (Convert.ToInt32(ddlCalltype.SelectedValue) == 1)
        {
            if (Convert.ToInt32(ddlIscall.SelectedValue) == 1)
            {
                pnlYCritical.Visible = true;
                DateTime Calling_Date = Convert.ToDateTime(dtmstM.Rows[0]["Calling_Date"].ToString());
                if (Calling_Date.ToString("dd/MM/yyy") == DateTime.Now.ToString("dd/MM/yyy"))
                {
                    ddlCriticalStatus.Enabled = false;
                }
                else
                {

                    ddlCriticalStatus.Enabled = true;
                }
            }
            else
            {
                pnlYCritical.Visible = false;
                pnlNCritical.Visible = false;
            }
        }
        ddlVillageExit.Enabled = false;
        ddlTBCode.Enabled = false;
        ddlCalltype.Enabled = false;
        ddlIscall.Enabled = false;



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
    public void Unique()
    {
        if (ViewState["Save"].ToString() == "Save")
        {
            if (ddlVillage.SelectedIndex > 0)
            {
                Int32 mNewNo = 0;
                string strAlias;
                string strQry = " Select top 1 isnull(max(Serial),0) as Serial from mstTeamBalika inner join mst5Village on  mst5Village.VillageCode=mstTeamBalika.VillageCode 	or  mst5Village.refVillage16=mstTeamBalika.VillageCode	or  mst5Village.refVillage17=mstTeamBalika.VillageCode	or  mst5Village.refVillage18=mstTeamBalika.VillageCode	or  mst5Village.refVillage19=mstTeamBalika.VillageCode	or  mst5Village.refVillage20=mstTeamBalika.VillageCode	 	or  mst5Village.refVillage21=mstTeamBalika.VillageCode  or  mst5Village.refVillage22=mstTeamBalika.VillageCode or  mst5Village.refVillage23=mstTeamBalika.VillageCode	or  mst5Village.refVillage24=mstTeamBalika.VillageCode or  mst5Village.refVillage25=mstTeamBalika.VillageCode	 inner join mst3Block on  mst3Block.BlockCode=mst5Village.BlockCode where mst5Village.DistrictCode='" + ddlDistrict.SelectedValue + "'   ";
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
                        ViewState["TBCode"] = "TB" + "-" + dtVillage.Rows[0]["EGVillageCode"] + "-" + strAlias;
                        ViewState["NumNo"] = strAlias;
                    }
                    else
                    {
                        mNewNo = Convert.ToInt32(dt.Rows[0]["Serial"].ToString());
                        mNewNo += 1;
                        strAlias = mNewNo.ToString().PadLeft(5, '0');

                        ViewState["NumNo"] = strAlias;
                        ViewState["TBCode"] = "TB" + "-" + dtVillage.Rows[0]["EGVillageCode"] + "-" + strAlias;

                    }

                }
                else
                {
                    mNewNo += 1;
                    strAlias = mNewNo.ToString().PadLeft(5, '0');
                    ViewState["TBCode"] = "TB" + "-" + strAlias;
                    ViewState["NumNo"] = strAlias;
                }
            }
        }

    }
    protected void ddlCalltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlIscall.SelectedIndex = 0;
        ClearData();
        if (ddlCalltype.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlCalltype.SelectedValue) == 1)
            {
               
                pnlNo.Visible = false;
                pnlCall.Visible = false;
                pnlCallNo.Visible = false;
                IScall.Visible = true;
                pnlNo1.Visible = false;
                pnlCall1.Visible = false;
                pnlYCritical.Visible = false;
                pnlNCritical.Visible = false;
            }
            if (Convert.ToInt32(ddlCalltype.SelectedValue) == 2)
            {
                pnlCall.Visible = false;
                pnlCallNo.Visible = false;
                IScall.Visible = false;
                pnlNo.Visible = true;
                pnlNo1.Visible = true;
                IScall.Visible = false;
                pnlCall1.Visible = false;
                pnlYCritical.Visible = false;
                pnlNCritical.Visible = true;

                chkN1.Checked = false;
                chkN2.Checked = false;
                chkN3.Checked = false;
                chkN4.Checked = false;
                chkN5.Checked = false;
                chkN6.Checked = false;
                chkN7.Checked = false;
                chkN8.Checked = false;
                chkN9.Checked = false;
                chkN10.Checked = false;
                chkN11.Checked = false;
                chkN12.Checked = false;
                chkN13.Checked = false;

                chkN1.Enabled = true;
                chkN2.Enabled = true;
                chkN3.Enabled = true;
                chkN4.Enabled = true;
                chkN5.Enabled = true;
                chkN6.Enabled = true;
                chkN7.Enabled = true;
                chkN8.Enabled = true;
                chkN9.Enabled = true;
                chkN10.Enabled = true;
                chkN11.Enabled = true;
                chkN13.Enabled = true;
                chkN12.Enabled = true;

            }

        }
        else
        {
            IScall.Visible = false;
            pnlNo.Visible = false;
            pnlCall.Visible = false;
            pnlCallNo.Visible = false;
            pnlNo1.Visible = false;
            pnlCall1.Visible = false;

            pnlYCritical.Visible = false;
            pnlNCritical.Visible = false;
        }

    }
    protected void ddlIscall_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearData();
        if (ddlIscall.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlIscall.SelectedValue) == 1)
            {
                pnlCall.Visible = true;
                pnlCall1.Visible = true;
                pnlCallNo.Visible = false;
                pnlYCritical.Visible = true;
                chkY1.Enabled = true;
                chkY2.Enabled = true;
                chkY3.Enabled = true;
                chkY4.Enabled = true;
                chkY5.Enabled = true;
                chkY6.Enabled = true;
                chkY7.Enabled = true;
                chkY8.Enabled = true;
                chkY9.Enabled = true;
                chkY10.Enabled = true;
                chkY11.Enabled = true;
                chkY13.Enabled = true;
                chkY12.Enabled = true;
                chkY1.Checked = false;
                chkY2.Checked = false;
                chkY3.Checked = false;
                chkY4.Checked = false;
                chkY5.Checked = false;
                chkY6.Checked = false;
                chkY7.Checked = false;
                chkY8.Checked = false;
                chkY9.Checked = false;
                chkY10.Checked = false;
                chkY11.Checked = false;
                chkY12.Checked = false;
                chkY13.Checked = false;
            }
            if (Convert.ToInt32(ddlIscall.SelectedValue) == 2)
            {
                pnlCallNo.Visible = true;
                pnlCall.Visible = false;
                pnlCall1.Visible = false;
                pnlYCritical.Visible = false;
            }
        }
        else
        {
            pnlCall1.Visible = false;
            pnlCallNo.Visible = false;
            pnlCall.Visible = false;
        }
    }
    protected void ddlContact_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlReasonNot.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlReasonNot.SelectedValue) == 6)
            {
                txtContact.Enabled = true;
            }
            else
            {
                txtContact.Enabled = false;
            }

        }
        else
        {
            txtContact.Enabled = false;
        }
    }

    protected void chkred_CheckedChanged(object sender, EventArgs e)
    {
        pnlCall.Visible = false;

    }
    protected void chkY12_CheckedChanged(Object sender, EventArgs e)
    {
        if (chkY12.Checked == true)
        {

            chkY1.Enabled = false;
            chkY2.Enabled = false;
            chkY3.Enabled = false;
            chkY4.Enabled = false;
            chkY5.Enabled = false;
            chkY6.Enabled = false;
            chkY7.Enabled = false;
            chkY8.Enabled = false;
            chkY9.Enabled = false;
            chkY10.Enabled = false;
            chkY11.Enabled = false;
            chkY13.Enabled = false;
            txtDiscuOther.Text = "";

            chkY1.Checked = false;
            chkY2.Checked = false;
            chkY3.Checked = false;
            chkY4.Checked = false;
            chkY5.Checked = false;
            chkY6.Checked = false;
            chkY7.Checked = false;
            chkY8.Checked = false;
            chkY9.Checked = false;
            chkY10.Checked = false;
            chkY11.Checked = false;
            chkY13.Checked = false;
        }
        else
        {
            chkY1.Enabled = true;
            chkY2.Enabled = true;
            chkY3.Enabled = true;
            chkY4.Enabled = true;
            chkY5.Enabled = true;
            chkY6.Enabled = true;
            chkY7.Enabled = true;
            chkY8.Enabled = true;
            chkY9.Enabled = true;
            chkY10.Enabled = true;
            chkY11.Enabled = true;
            chkY13.Enabled = true;
            txtDiscuOther.Text = "";
        }
        int V1 = 0;
      
        foreach (ListItem item in CBL_bookformat1.Items)
        {
            if (item.Selected)
            {
                if (item.Text == "Other")
                {

                    txtCallOther.Enabled = true;
                    V1 = 9;
                }

            }
        }
        if (V1 == 0)
        {
            txtCallOther.Enabled = false;
        }
    }
    protected void chkY13_CheckedChanged(Object sender, EventArgs e)
    {
        if (chkY13.Checked==true)
        {
            txtDiscuOther.Text = "";
            txtDiscuOther.Enabled = true;
        }
        else
        {
            txtDiscuOther.Text = "";
            txtDiscuOther.Enabled = false;
        }
        int V1 = 0;
      
        foreach (ListItem item in CBL_bookformat1.Items)
        {
            if (item.Selected)
            {
                if (item.Text == "Other")
                {

                    txtCallOther.Enabled = true;
                    V1 = 9;
                }

            }
        }
        if (V1 == 0)
        {
            txtCallOther.Enabled = false;
        }

    }


    protected void chkN12_CheckedChanged(Object sender, EventArgs e)
    {
        if (chkN12.Checked == true)
        {

            chkN1.Enabled = false;
            chkN2.Enabled = false;
            chkN3.Enabled = false;
            chkN4.Enabled = false;
            chkN5.Enabled = false;
            chkN6.Enabled = false;
            chkN7.Enabled = false;
            chkN8.Enabled = false;
            chkN9.Enabled = false;
            chkN10.Enabled = false;
            chkN11.Enabled = false;
            chkN13.Enabled = false;
            txtOther1.Text = "";

            chkN1.Checked = false;
            chkN2.Checked = false;
            chkN3.Checked = false;
            chkN4.Checked = false;
            chkN5.Checked = false;
            chkN6.Checked = false;
            chkN7.Checked = false;
            chkN8.Checked = false;
            chkN9.Checked = false;
            chkN10.Checked = false;
            chkN11.Checked = false;
            chkN13.Checked = false;
        }
        else
        {
            chkN1.Enabled = true;
            chkN2.Enabled = true;
            chkN3.Enabled = true;
            chkN4.Enabled = true;
            chkN5.Enabled = true;
            chkN6.Enabled = true;
            chkN7.Enabled = true;
            chkN8.Enabled = true;
            chkN9.Enabled = true;
            chkN10.Enabled = true;
            chkN11.Enabled = true;
            chkN13.Enabled = true;
            txtOther1.Text = "";
        }
        int V1 = 0;
        int V2 = 0;
        int V3 = 0;
        int V4 = 0;
        foreach (ListItem item in chkobjCall.Items)
        {
            if (item.Selected)
            {
                if (item.Value == "9")
                {

                    txtIssue.Enabled = true;
                    V1 = Convert.ToInt32(item.Value);
                }
                if (item.Value == "10")
                {

                    txtSupport.Enabled = true;
                    V2 = Convert.ToInt32(item.Value);
                }
                if (item.Value == "11")
                {

                    txtNoOther.Enabled = true;
                    V3 = Convert.ToInt32(item.Value);
                }

            }
            if (V1 == 0)
            {
                txtIssue.Enabled = false;
            }
            if (V2 == 0)
            {
                txtSupport.Enabled = false;
            }
            if (V3 == 0)
            {
                txtNoOther.Enabled = false;
            }
        }
    }
    protected void chkN13_CheckedChanged(Object sender, EventArgs e)
    {
        if (chkN13.Checked == true)
        {
            txtOther1.Text = "";
            txtOther1.Enabled = true;
        }
        else
        {
            txtOther1.Text = "";
            txtOther1.Enabled = false;
        }
        int V1 = 0;
        int V2 = 0;
        int V3 = 0;
       
        foreach (ListItem item in chkobjCall.Items)
        {
            if (item.Selected)
            {
                if (item.Value == "9")
                {

                    txtIssue.Enabled = true;
                    V1 = Convert.ToInt32(item.Value);
                }
                if (item.Value == "10")
                {

                    txtSupport.Enabled = true;
                    V2 = Convert.ToInt32(item.Value);
                }
                if (item.Value == "11")
                {

                    txtNoOther.Enabled = true;
                    V3 = Convert.ToInt32(item.Value);
                }

            }
            if (V1 == 0)
            {
                txtIssue.Enabled = false;
            }
            if (V2 == 0)
            {
                txtSupport.Enabled = false;
            }
            if (V3 == 0)
            {
                txtNoOther.Enabled = false;
            }
        }
    }
}