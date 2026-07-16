using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;

using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class frmTeamBalika : System.Web.UI.Page
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

                FillSocialCat();
                FillDropResone();
                ViewState["Save"] = "Save";
                FillFaimlyCat();
                FillEdu();
                FillSours();
                FillReasone();
                btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
                ValdateUserLavel();
                txtEndDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                FillSpecially();
            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }

        }
        ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "loadJSFunction();", true);

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
        AlllStateCode();
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

    public void FillSours()
    {
        conditions = "";
        conditions = "LookupFlag ='RSO' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "Description", "asc", ddlSours, "Description", "LookupCode", "Select");



    }
    public void FillDropResone()
    {
        conditions = "";
        conditions = "LookupFlag ='TMR' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description,SeqNo", conditions, "SeqNo", "asc", ddlStatusReasone, "Description", "LookupCode", "Select");

        conditions = "";
        conditions = "LookupFlag ='TJB' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description,SeqNo", conditions, "LookupCode", "asc", ddlJobOpportunity, "Description", "LookupCode", "Select");


    }

    public void FillSpecially()
    {
        conditions = "";
        conditions = "LookupFlag ='SPS' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlSpecially, "Description", "LookupCode", "Select");



    }
    public void FillReasone()
    {
        conditions = "";
        conditions = "LookupFlag ='RTB' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "Description", "asc", ddlReason, "Description", "LookupCode", "Select");



    }
    public void FillSocialCat()
    {
        conditions = "";
        conditions = "LookupFlag ='CAT' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlCategory, "Description", "LookupCode", "Select");



    }
    public void FillEdu()
    {
        conditions = "";
        conditions = "LookupFlag ='Edu' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlEducation, "Description", "LookupCode", "Select");



    }

    public void FillFaimlyCat()
    {
        conditions = "";
        conditions = "LookupFlag ='FO' and Active=1 ";
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
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
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
            //objComman.BindDLL("
            //", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");
            conditions = "StateCode ='" + ddlState.SelectedValue + "'  and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

            //string conditions1 = "StateCode ='" + ddlState.SelectedValue + "' ";

            //DataTable dtTb = objMain.LoadData(" SELECT DistrictCode,  dbo.TitleCase(upper(DistrictName)) as  DistrictName FROM [mst2District] where  " + conditions + "   order by DistrictName ");



            //objComman.BindDLLMasterTableVillage("mst2District", "DistrictCode,DistrictName", dtTb, conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");
            conditions = "";
            conditions = " mst2District.StateCode ='" + ddlState.SelectedValue + "' and UserName='" + Session["username"].ToString() + "' ";
            string strQry1 = "       sELECT distinct mst2District.DistrictCode as DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist  ";
            strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions + "  and  Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName  ";
            DataTable dtDistrict = objMain.LoadData(strQry1);

            objComman.BindDLLDatatable("mst2District", dtDistrict, "DistrictCode, dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "Desc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

            ddlDistrict.SelectedIndex = 0;
            ddlDistrict.Enabled = true;
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
            strQry = "Select DistrictCode from mst2District where   DistrictCode in(" + Session["DistrictCode"].ToString() + ")";
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
        string strQry = "  SELECT VillageGeographyOperational FROM mst5Village where villagecode ='" + ddlVillage.SelectedValue + "'     ";
        DataTable dtDistrict = objMain.LoadData(strQry);
        if (dtDistrict.Rows.Count > 0)
        {
            Session["VillageGeographyOperational"] = Convert.ToString(dtDistrict.Rows[0]["VillageGeographyOperational"]);
        }
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
            str = str + "and mst5Village.VillageCode='" + ddlVillage.SelectedValue.ToString() + "'";
        }
        DataTable dtmstM = objMain.LoadData(" SELECT TBCode,UniqueCode, TBName,mst5Village.VillageCode +'-'+ [TBCode] as UniqueId FROM [dbo].[mstTeamBalika] inner join mst5Village on mst5Village.VillageCode=mstTeamBalika.VillageCode 	or  mst5Village.refVillage16=mstTeamBalika.VillageCode	or  mst5Village.refVillage17=mstTeamBalika.VillageCode	or  mst5Village.refVillage18=mstTeamBalika.VillageCode	or  mst5Village.refVillage19=mstTeamBalika.VillageCode	or  mst5Village.refVillage20=mstTeamBalika.VillageCode	 	or  mst5Village.refVillage21=mstTeamBalika.VillageCode or  mst5Village.refVillage22=mstTeamBalika.VillageCode	or  mst5Village.refVillage23=mstTeamBalika.VillageCode or  mst5Village.refVillage24=mstTeamBalika.VillageCode  or  mst5Village.refVillage25=mstTeamBalika.VillageCode left join mst1State on mst1State.StateCode=mst5Village.StateCode left join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode   left join (select distinct blockcode,blockname from mst3Block) blk ON mst5Village.BlockCode = blk.BlockCode LEFT JOIN (select distinct PanchayatCode,PanchayatName from mstPanchayat) phy  ON mst5Village.PanchayatCode  = phy.PanchayatCode  " + str + " ");

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
    public static System.Collections.Generic.List<Control> GetAllControls(System.Collections.Generic.List<Control> controls, Type t, Control parent /* can be Page */)
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
    //public string SetTextBoxFocusSelect(Page page)
    //{
    //    string ALlTestBoxValue = "";

    //    System.Collections.Generic.List<Control> list =
    //        new System.Collections.Generic.List<Control>();

    //    // Example: Panel1 controls only
    //    list.AddRange(list.Controls.Cast<Control>());

    //    foreach (Control ctl in list)
    //    {
    //        if (ctl is TextBox)
    //        {
    //            TextBox txt = (TextBox)ctl;

    //            txt.Attributes.Add("onfocus", "this.select()");

    //            string TempVari = txt.Text;

    //            if (!string.IsNullOrEmpty(TempVari))
    //            {
    //                ALlTestBoxValue += TempVari + " ";
    //            }
    //        }
    //    }
    //    return ALlTestBoxValue;
    //}
    protected void btnsave_Click(object sender, EventArgs e)
    {

        //string RVal = SetTextBoxFocusSelect(this.Page);
        //if (!InterventionSql_Injection(RVal))
        //{
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Spurious input detected. Data rejected')</script>", false);

        //    return;
        //}
        Save_Update(0);
    }

    protected void btnSumbit_Click(object sender, EventArgs e)
    {
        Save_Update(0);
    }
    private void Save_Update(int SchoolCode)
    {

        if (Convert.ToInt32(ddlDob.SelectedValue) == 2 && txtAge.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Age')</script>", false);


            this.txtAge.Focus();
            return;
        }
        if (Convert.ToInt32(ddlWorkEx.SelectedValue) == 1 && txtDuartion.Text == "")
        {
            if (txtDuartion.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Duration')</script>", false);


                this.txtDuartion.Focus();
                return;
            }
            if (Convert.ToInt32(ddlWorkEx.SelectedValue) <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Year/Month')</script>", false);


                this.ddlWorkEx.Focus();
                return;
            }
        }
        if (Convert.ToInt32(ddlWorkingStatus.SelectedValue) <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Wroking Status')</script>", false);


            this.ddlWorkEx.Focus();
            return;
        }
        if (Convert.ToInt32(ddlSmart.SelectedValue) <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Smart phone available')</script>", false);


            this.ddlWorkEx.Focus();
            return;
        }
        if (Convert.ToInt32(ddlPhysicalStatus.SelectedValue) <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Team Balika Physical Status.')</script>", false);


            this.ddlWorkEx.Focus();
            return;
        }
        if (Convert.ToInt32(ddlPhysicalStatus.SelectedValue) > 0)
        {
            if (Convert.ToInt32(ddlPhysicalStatus.SelectedValue) == 1)
            {
                if (Convert.ToInt32(ddlSpecially.SelectedValue) <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' Please Select Type of Specially Abled')</script>", false);


                    this.ddlWorkEx.Focus();
                    return;
                }
            }
        }
        if (ddlWorkingStatus.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 1)
            {

            }
            else
            {
                if (Convert.ToInt32(ddlStatusReasone.SelectedValue) <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Dropout Reason')</script>", false);


                    this.ddlWorkEx.Focus();
                    return;
                }
                if (txtDropDate.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Dropout Date')</script>", false);


                    this.txtDuartion.Focus();
                    return;
                }

                if (Convert.ToInt32(ddlStatusReasone.SelectedValue) == 14)
                {
                    if (txtJob.Text.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter job type')</script>", false);


                        this.txtDuartion.Focus();
                        return;
                    }
                }
                if (Convert.ToInt32(ddlStatusReasone.SelectedValue) == 15)
                {
                    if (txtBus.Text.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Business Type')</script>", false);


                        this.txtDuartion.Focus();
                        return;
                    }
                }

                if (Convert.ToInt32(ddlEducation.SelectedValue) == 5 || Convert.ToInt32(ddlEducation.SelectedValue) == 7 || Convert.ToInt32(ddlEducation.SelectedValue) == 9)
                {
                    if (Convert.ToInt32(ddlSpecialization.SelectedValue) <= 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Education Specialization')</script>", false);


                        this.ddlWorkEx.Focus();
                        return;
                    }
                }
                if (Convert.ToInt32(ddlStatusReasone.SelectedValue) == 14 || Convert.ToInt32(ddlStatusReasone.SelectedValue) == 15)
                {
                    if (divJobOp.Visible == true)
                    {
                        if (Convert.ToInt32(ddlJobOpportunity.SelectedValue) <= 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Team Balika Other opportunity through')</script>", false);


                            this.ddlWorkEx.Focus();
                            return;
                        }

                        if (Convert.ToInt32(ddlJobOpportunity.SelectedValue) == 4)
                        {
                            if (txtotherjob.Text.Trim() == "")
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter the Other Detail on the Team Balika Job Opportunity Through')</script>", false);


                                this.txtDuartion.Focus();
                                return;
                            }
                        }
                    }
                }


                string DropOutData1 = txtDropDate.Text;
                string[] D1;




                if (txtDropDate.Text != "")
                {
                    D1 = DropOutData1.Split('/');
                    if (Convert.ToInt32(D1[2]) < 2016)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter valid Dropout Date')</script>", false);


                        this.txtDuartion.Focus();
                        return;
                    }
                }
            }
        }
        if (ddlWorkingStatus.SelectedIndex > 0)
        {
            //if (Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 2)
            //{
            if (Convert.ToInt32(ddlAlumni.SelectedValue) == 1)
            {
                if (txtAlumniDate.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Team Balika Alumni Date')</script>", false);


                    this.txtDuartion.Focus();
                    return;
                }
            }
            //}
        }

        if (txtFatherName.Text.Trim() == "" && txtMotherName.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Mother/Father Name')</script>", false);

            this.txtFatherName.Focus();
            return;
        }

        string Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtName.Text);
        string FatherName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtFatherName.Text);
        string MotherName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtMotherName.Text);
        string Exp = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtExp.Text);
        string Abv = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtAbv.Text);
        string EmpName = "";
        string Designation = "";
        DateTime DateJoined = DateTime.MinValue;



        if (txtEmployeeID.Text.Length > 0)
        {
            DataTable dtEmpl = objMain.LoadData(" SELECT [Employee Name] as EMP,Designation,[Date Joined] as DateJoined FROM [dbo].[mstTempCurrentUser] where EmployeeCode ='" + txtEmployeeID.Text + "'");
            if (dtEmpl.Rows.Count > 0)
            {
                EmpName = dtEmpl.Rows[0]["EMP"].ToString();
                Designation = dtEmpl.Rows[0]["Designation"].ToString();
                DateJoined = Convert.ToDateTime(dtEmpl.Rows[0]["DateJoined"]);
            }
        }
        string DateofJoining1 = txtJoingDate.Text;
        string[] b = DateofJoining1.Split('/');
        string DateofJoining = b[2] + '-' + b[1] + '-' + b[0];


        string DropOutData = txtDropDate.Text;
        string[] D;
        string AlumniDateData = txtAlumniDate.Text;
        string[] A;
        string DropOutDate;
        string AlumniDate;
        DateTime DropOuEntryDate;
        if (txtAlumniDate.Text != "")
        {
            A = AlumniDateData.Split('/');
            AlumniDate = A[2] + '-' + A[1] + '-' + A[0];
        }
        else
        {
            AlumniDate = "1900-01-01";
        }

        if (txtDropDate.Text != "")
        {
            D = DropOutData.Split('/');
            DropOutDate = D[2] + '-' + D[1] + '-' + D[0];
            DropOuEntryDate = DateTime.Now;
        }
        else
        {
            DropOutDate = "1900-01-01";
            DropOuEntryDate = DateTime.MinValue;
        }
        DateTime DOB;
        DateTime AsDob;
        Int32 Age = 0;
        Int32 mmonth = 0;
        int WrokExp = 0;

        int mainResult = 0;
        string type = "";
        string strMainIDNo = "";

        if (Convert.ToInt32(ddlWorkEx.SelectedValue) == 1)
        {
            if (txtDuartion.Text != "")
            {
                WrokExp = Convert.ToInt32(txtDuartion.Text);
            }

            if (txtMonth.Text != "")
            {
                mmonth = Convert.ToInt32(txtMonth.Text);
            }
        }
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
            Int32 Total = Convert.ToInt32(b[2]) - Convert.ToInt32(a[2]);
            if (Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 1)
            {
                if (Total < 18)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Age should be 18 years')</script>", false);


                    this.txtAge.Focus();
                    return;

                }
            }

            //    AsDob = Convert.ToDateTime(dyear + '-' + a[1] + '-' + a[0]);

            //if (Age < 3)
            //{

            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 3 and 14 years')</script>", false);


            //    this.txtAge.Focus();
            //    return;

            //}
            //if (Age > 14)
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 3 and 14 years')</script>", false);


            //    this.txtAge.Focus();
            //    return;
            //}

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

            Int32 Total = Convert.ToInt32(Convert.ToInt32(b[2]) - iyear);
            if (Total < 18)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Age should be 18 years')</script>", false);


                this.txtAge.Focus();
                return;

            }


        }

        bool PriorWorkExperience = false;
        int Duartion = 0;
        int Specialization = 0;
        if (txtDuartion.Text != "")
        {
            Duartion = Convert.ToInt32(txtDuartion.Text);
        }
        if (Convert.ToInt32(ddlWorkEx.SelectedValue) == 1)
        {
            PriorWorkExperience = true;
        }

        if (Convert.ToInt32(ddlEducation.SelectedValue) == 5 || Convert.ToInt32(ddlEducation.SelectedValue) == 7 || Convert.ToInt32(ddlEducation.SelectedValue) == 9)
        {
            Specialization = Convert.ToInt32(ddlSpecialization.SelectedValue);
        }
        if (ViewState["Save"].ToString() == "Save")
        {
            DataTable dtCheck = objMain.LoadData(" SELECT * FROM [dbo].[mstTeamBalika]  inner join mst5Village on  mst5Village.VillageCode=mstTeamBalika.VillageCode or  mst5Village.refVillage16=mstTeamBalika.VillageCode	or  mst5Village.refVillage17=mstTeamBalika.VillageCode	or  mst5Village.refVillage18=mstTeamBalika.VillageCode	or  mst5Village.refVillage19=mstTeamBalika.VillageCode	or  mst5Village.refVillage20=mstTeamBalika.VillageCode  	or  mst5Village.refVillage21=mstTeamBalika.VillageCode			  where TBName='" + Name + "' and FatherName='" + FatherName + "' and   mst5Village.VillageCode='" + ddlVillage.SelectedValue + "' ");
            if (dtCheck.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TB Name Allready Exit')</script>", false);
                return;
            }
            Unique();
            string TBCode = ViewState["TBCode"].ToString();
            string schoolod = ViewState["NumNo"].ToString();
            string Fullfilename = "";

            if (FileuploadAttach.PostedFile != null && FileuploadAttach.PostedFile.FileName != "")
            {
                string ext = System.IO.Path.GetExtension(FileuploadAttach.PostedFile.FileName).ToLower();
                if (FileuploadAttach.PostedFile.ContentLength < 102400)
                {
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Image size must be less than 100kb')</script>", false);
                    return;
                }
                if (ext != ".jpeg" && ext != ".jpg" && ext != ".png" && ext != ".gif")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Images')</script>", false);
                    return;
                }
                string exten = Path.GetExtension(FileuploadAttach.PostedFile.FileName);
                Fullfilename = "" + TBCode + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;
            }

            ViewState["Save"] = "fff";


            strMainIDNo = objMain.Generate_RandomString(8);
            ViewState["TMCode"] = strMainIDNo;
            type = "I";

            #region Attach image
            //System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(FileuploadAttach.PostedFile.InputStream);
            //System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 81);


            string sFileDir = Server.MapPath(Comman.GetImagePath("DataBackupPath")); ;

            if (FileuploadAttach.PostedFile != null && FileuploadAttach.PostedFile.FileName != "")
            {
                string exten = Path.GetExtension(FileuploadAttach.PostedFile.FileName);
                // string Imagefile1 = "LeaveDoc" + "_" + Convert.ToString(Session["EMP_ID"]) + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;

                //create directory

                if (Directory.Exists(sFileDir)) { }
                else { System.IO.Directory.CreateDirectory(sFileDir); }

                //======update the file =====\\

                if (System.IO.File.Exists(sFileDir + "\\" + Fullfilename))
                {
                    try { System.IO.File.Delete(sFileDir + "\\" + Fullfilename); }
                    catch
                    {
                        //ShowMessage.Visible = true;
                        //ShowMessage.Style.Add("background-color", "#FFBABA");
                        //MessageLBL.Style.Add("Color", "#D8000C");
                        //MessageLBL.Text = ex.ToString();

                    }
                }
                FileuploadAttach.PostedFile.SaveAs(sFileDir + Fullfilename);

            }

            #endregion
            mainResult = SaveDataTeamBalika(strMainIDNo, schoolod, TBCode, ddlVillage.SelectedValue, Name, Convert.ToInt32(ddlGender.SelectedValue), FatherName, Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlEducation.SelectedValue), Convert.ToInt32(ddloccu.SelectedValue), Convert.ToInt32(ddlDob.SelectedValue), DOB, Age, AsDob, Convert.ToInt32(ddlReason.SelectedValue), Convert.ToInt32(ddlSours.SelectedValue), PriorWorkExperience, Duartion, mmonth, txtContact.Text, type, Exp, Abv, MotherName, Fullfilename, Convert.ToDateTime(DateofJoining), Convert.ToInt32(ddlWorkingStatus.SelectedValue), Convert.ToInt32(ddlStatusReasone.SelectedValue), Convert.ToDateTime(DropOutDate), Session["username"].ToString(), Convert.ToInt32(ddltbRecruited.SelectedValue), EmpName, Designation, DateJoined, ddlAlumni.SelectedValue, AlumniDate, DropOuEntryDate, Specialization);




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

            if (FileuploadAttach.PostedFile != null && FileuploadAttach.PostedFile.FileName != "")
            {

                string ext = System.IO.Path.GetExtension(FileuploadAttach.PostedFile.FileName).ToLower();
                if (ext != ".jpeg" && ext != ".jpg" && ext != ".png" && ext != ".gif")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Images')</script>", false);
                    return;
                }
                string exten = Path.GetExtension(FileuploadAttach.PostedFile.FileName);
                Fullfilename = "" + txtIDNO.Text.Trim() + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;
            }
            string sFileDir = Server.MapPath(Comman.GetImagePath("DataBackupPath")); ;

            if (FileuploadAttach.PostedFile != null && FileuploadAttach.PostedFile.FileName != "")
            {
                string exten = Path.GetExtension(FileuploadAttach.PostedFile.FileName);
                // string Imagefile1 = "LeaveDoc" + "_" + Convert.ToString(Session["EMP_ID"]) + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;

                //create directory

                if (Directory.Exists(sFileDir)) { }
                else { System.IO.Directory.CreateDirectory(sFileDir); }

                //======update the file =====\\

                if (System.IO.File.Exists(sFileDir + "\\" + Fullfilename))
                {
                    try { System.IO.File.Delete(sFileDir + "\\" + Fullfilename); }
                    catch
                    {


                    }
                }
                FileuploadAttach.PostedFile.SaveAs(sFileDir + Fullfilename);

            }

            #endregion

            mainResult = SaveDataTeamBalika(ViewState["TMCode"].ToString(), "", "", ddlVillage.SelectedValue, Name, Convert.ToInt32(ddlGender.SelectedValue), FatherName, Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlEducation.SelectedValue), Convert.ToInt32(ddloccu.SelectedValue), Convert.ToInt32(ddlDob.SelectedValue), DOB, Age, AsDob, Convert.ToInt32(ddlReason.SelectedValue), Convert.ToInt32(ddlSours.SelectedValue), PriorWorkExperience, Duartion, mmonth, txtContact.Text, type, Exp, Abv, MotherName, Fullfilename, Convert.ToDateTime(DateofJoining), Convert.ToInt32(ddlWorkingStatus.SelectedValue), Convert.ToInt32(ddlStatusReasone.SelectedValue), Convert.ToDateTime(DropOutDate), Session["username"].ToString(), Convert.ToInt32(ddltbRecruited.SelectedValue), EmpName, Designation, DateJoined, ddlAlumni.SelectedValue, AlumniDate, DropOuEntryDate, Specialization);


            if (mainResult > 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Update sucessfully')</script>", false);
                GVMainBind();
            }

        }



    }
    public int SaveDataTeamBalika(string strMainIDNo, string TcodeSerial, string Tcode, string VillageCode, string TBName, int Gender, string strFatherName, int SocialCategory, int EducationLevel, int FamilyOccupation, int DOBAvailable, DateTime DOB, int AgeAson, DateTime AsOnDate, int ReasonForTBChoice, int RecruitmentReferalInfo, bool PriorWorkExperience, int TotalPriorWorkExperience, int PriorWorkYearMonth, string Contact, string flag, string Expectation, string Abvision, string MotherName, string ImagePath, DateTime DateofJoining, int dropOutStatus, int DroupOutRe, DateTime DropoutResone, string createby, Int32 TbRecruited, string EmpName, string Designation, DateTime DateJoined, string Alumni, string AlumniDate, DateTime DropOuEntryDate, int Specialization)
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
            new SqlParameter("@DateofJoining", DateofJoining.ToString("yyyy-MM-dd")),
            new SqlParameter("@dropOutStatus", dropOutStatus),
            new SqlParameter("@DroupOutRe", DroupOutRe),
            new SqlParameter("@DropoutResone", DropoutResone),
            new SqlParameter("@createby", createby),
                new SqlParameter("@TbRecruited", TbRecruited),

                new SqlParameter("@AlternetPhoneNo", txtxAlternate.Text),
                new SqlParameter("@IsSmartPhone", ddlSmart.SelectedValue),
                  new SqlParameter("@EmpID", txtEmployeeID.Text),
                 new SqlParameter("@EmpName", EmpName),
 new SqlParameter("@Designation", Designation),
 new SqlParameter("@DateJoined",  DateJoined.ToString("yyyy-MM-dd")),
  new SqlParameter("@Alumni", Alumni),
 new SqlParameter("@AlumniDate",Convert.ToDateTime(AlumniDate).ToString("yyyy-MM-dd")),

   new SqlParameter("@rjob", txtJob.Text),
     new SqlParameter("@rBusiness", txtBus.Text),
       new SqlParameter("@rJobOpportunity", ddlJobOpportunity.SelectedValue),
         new SqlParameter("@rjobother", txtotherjob.Text),

  new SqlParameter("@dropoutEntrydate",DropOuEntryDate.ToString("yyyy-MM-dd")),
    new SqlParameter("@PhysicalStatus", ddlPhysicalStatus.SelectedValue),
      new SqlParameter("@Specially", ddlSpecially.SelectedValue),
       new SqlParameter("@Specialization", Specialization),



        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateTeamBalikaNew2025", cmdParameters);
    }

    protected void ddlWork_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWorkEx.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlWorkEx.SelectedValue) == 1)
            {
                txtDuartion.Enabled = true;
                txtMonth.Enabled = true;
            }
            else
            {
                txtDuartion.Enabled = false;
                txtMonth.Enabled = false;
                txtDuartion.Text = "";
                txtMonth.Text = "";

            }
        }
        else
        {
            txtDuartion.Enabled = false;
            txtMonth.Enabled = false;
            txtDuartion.Text = "";
            txtMonth.Text = "";
        }
    }

    protected void ddlWorkingStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        divJob.Visible = false;
        txtJob.Text = "";

        divbus.Visible = false;
        txtBus.Text = "";
        divJobOp.Visible = false;
        ddlJobOpportunity.SelectedIndex = 0;
        divOtherJob.Visible = false;
        txtotherjob.Text = "";

        if (ddlWorkingStatus.SelectedIndex > 0)
        {


            if (Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 1 && Convert.ToString(Session["VillageGeographyOperational"]) == "2")

            {
                rdate.Visible = false;
                Resone.Visible = false;
                divAlumni.Visible = true;
                txtDropDate.Text = "";
                ddlStatusReasone.SelectedIndex = 0;
                ddlAlumni.SelectedIndex = 0;
                divAlumni1.Visible = false;

                divJob.Visible = false;
                txtJob.Text = "";
                divbus.Visible = false;
                txtBus.Text = "";

            }
            else if (Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 1)
            {
                rdate.Visible = false;
                Resone.Visible = false;
                divAlumni.Visible = false;

                ddlStatusReasone.SelectedIndex = 0;
                ddlAlumni.SelectedIndex = 0;
                divAlumni1.Visible = false;
                divJob.Visible = false;
                txtJob.Text = "";
                divbus.Visible = false;
                txtBus.Text = "";
            }
            else
            {
                divAlumni.Visible = true;
                txtEmployeeID.Text = "";
                divAlumni1.Visible = false;
                rdate.Visible = true;
                Resone.Visible = true;
                divJob.Visible = false;
                txtJob.Text = "";
                divbus.Visible = false;
                txtBus.Text = "";
            }
        }
        else
        {
            divAlumni1.Visible = false;
            divAlumni.Visible = false;
            txtEmployeeID.Text = "";
            rdate.Visible = false;
            Resone.Visible = false;
            txtDropDate.Text = "";
            divJob.Visible = false;
            txtJob.Text = "";
            divbus.Visible = false;
            txtBus.Text = "";

            ddlStatusReasone.SelectedIndex = 0;
        }
    }
    protected void ddlAlumni_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAlumni.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlAlumni.SelectedValue) == 1)
            {
                divAlumni1.Visible = true;
                txtAlumniDate.Text = "";
            }
            else
            {
                divAlumni1.Visible = false;
                txtAlumniDate.Text = "";
            }
        }
        else
        {
            divAlumni1.Visible = false;
            txtAlumniDate.Text = "";
        }
    }
    protected void ddlStatusReasone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWorkingStatus.SelectedIndex > 0)

        {
            divOtherJob.Visible = false;
            txtotherjob.Text = "";
            if (Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 2 && Convert.ToInt32(ddlStatusReasone.SelectedValue) == 3)
            {
                txtEmployeeID.Text = "";
                EmpID.Visible = true;
                divJob.Visible = false;
                txtJob.Text = "";

                divbus.Visible = false;
                txtBus.Text = "";

                divJobOp.Visible = false;
                ddlJobOpportunity.SelectedIndex = 0;
                divOtherJob.Visible = false;
                txtotherjob.Text = "";

            }
            else if (Convert.ToInt32(ddlStatusReasone.SelectedValue) == 14)
            {
                EmpID.Visible = false;
                txtEmployeeID.Text = "";
                divJob.Visible = true;
                txtJob.Text = "";
                divbus.Visible = false;
                txtBus.Text = "";
                divJobOp.Visible = false;
                ddlJobOpportunity.SelectedIndex = 0;

                if (ViewState["Save"].ToString() == "Save")
                {
                    divJobOp.Visible = true;
                    ddlJobOpportunity.SelectedIndex = 0;
                }
                else
                {
                    DataTable dtCheck = objMain.LoadData(" SELECT  *FROm [tblTraining] inner join tblTrainingDetail on tblTrainingDetail.[TBUniqueCode]=UniqueCode where  convert(int, isnull(tblTraining.DeleteFlag,0))<>2 and [Learningtype] =8 and fromdate>='2024-04-01' and TBid='" + ViewState["TMCode"].ToString() + "' ");
                    if (dtCheck.Rows.Count > 0)
                    {
                        divJobOp.Visible = false;
                        ddlJobOpportunity.SelectedIndex = 0;
                    }
                    else
                    {
                        divJobOp.Visible = true;
                        ddlJobOpportunity.SelectedIndex = 0;
                    }
                }


            }
            else if (Convert.ToInt32(ddlStatusReasone.SelectedValue) == 15)
            {
                EmpID.Visible = false;
                txtEmployeeID.Text = "";
                divJob.Visible = false;
                txtJob.Text = "";

                divbus.Visible = true;
                txtBus.Text = "";

                divJobOp.Visible = false;
                ddlJobOpportunity.SelectedIndex = 0;

                if (ViewState["Save"].ToString() == "Save")
                {
                    divJobOp.Visible = true;
                    ddlJobOpportunity.SelectedIndex = 0;
                }
                else
                {
                    DataTable dtCheck = objMain.LoadData(" SELECT  *FROm [tblTraining] inner join tblTrainingDetail on tblTrainingDetail.[TBUniqueCode]=UniqueCode where [Learningtype] =8 and fromdate>='2024-04-01' and TBid='" + ViewState["TMCode"].ToString() + "' ");
                    if (dtCheck.Rows.Count > 0)
                    {
                        divJobOp.Visible = false;
                        ddlJobOpportunity.SelectedIndex = 0;
                    }
                    else
                    {
                        divJobOp.Visible = true;
                        ddlJobOpportunity.SelectedIndex = 0;
                    }
                }
            }
            else
            {

                EmpID.Visible = false;
                txtEmployeeID.Text = "";

                divJob.Visible = false;
                txtJob.Text = "";
                divbus.Visible = false;
                txtBus.Text = "";
                divJobOp.Visible = false;
                ddlJobOpportunity.SelectedIndex = 0;
            }
        }
        else
        {
            divJob.Visible = false;
            txtJob.Text = "";
            txtEmployeeID.Text = "";
            EmpID.Visible = false;
            divbus.Visible = false;
            txtBus.Text = "";
            divJobOp.Visible = false;
            ddlJobOpportunity.SelectedIndex = 0;
            divOtherJob.Visible = false;
            txtotherjob.Text = "";
        }
    }

    protected void ddlOther_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (Convert.ToInt32(ddlJobOpportunity.SelectedValue) == 4)
        {
            divOtherJob.Visible = true;
            txtotherjob.Text = "";
        }
        else
        {
            divOtherJob.Visible = false;
            txtotherjob.Text = "";
        }

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
            DateTime ydate = new DateTime(DateTime.Now.Year - 1, 05, 01);

            txtDate.Text = ydate.ToString("dd/MM/yyyy");
            lblDob.Text = "As On";
            lblAge.Enabled = true;
            txtAge.Enabled = true;
        }
    }
    private void RefreshControl()
    {
        #region RefreshControl
        txtday.Text = "";
        ViewState["TMCode"] = null;
        ViewState["TBCode"] = null;
        ViewState["ImagePath"] = null;
        txtExp.Text = ""; txtAbv.Text = "";
        txtIDNO.Text = "Auto generated number";
        txtName.Text = string.Empty;
        txtDate.Text = string.Empty;
        ddlDob.SelectedIndex = 2;
        DateTime ydate = new DateTime(DateTime.Now.Year - 1, 05, 01);
        txtJoingDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtDate.Text = ydate.ToString("dd/MM/yyyy");


        ddlWorkEx.SelectedIndex = 0;
        txtDate.Enabled = false;
        txtFatherName.Text = string.Empty;
        txtContact.Text = string.Empty;
        txtAge.Text = string.Empty;
        txtxAlternate.Text = string.Empty;
        ddlSmart.SelectedIndex = 0;
        txtDuartion.Text = string.Empty;

        ddlGender.SelectedIndex = 0;
        ddlEducation.SelectedIndex = 0;
        ddloccu.SelectedIndex = 0;
        ddlCategory.SelectedIndex = 0;
        ddlReason.SelectedIndex = 0;
        ddlSours.SelectedIndex = 0;
        ddlWorkingStatus.SelectedIndex = 0;
        txtMonth.Text = "";
        txtMotherName.Text = "";

        ddlAlumni.SelectedIndex = 0;
        txtAlumniDate.Text = "";
        txtEmployeeID.Text = "";
        divAlumni.Visible = false;
        divAlumni1.Visible = false;
        ViewState["Save"] = "Save";
        divJob.Visible = false;
        txtJob.Text = "";

        divbus.Visible = false;
        txtBus.Text = "";
        divJobOp.Visible = false;
        ddlJobOpportunity.SelectedIndex = 0;
        divOtherJob.Visible = false;
        txtotherjob.Text = "";
        ViewState["TMCode"] = null;
        divSp.Visible = false;
        ddlSpecially.SelectedIndex = 0;
        #endregion
    }
    protected void btnAdd2_Click(object sender, EventArgs e)
    {

        // Dynamic Name
        //string folderPath = @"D:\GeneratedIDCards\";

        //if (!Directory.Exists(folderPath))
        //{
        //    Directory.CreateDirectory(folderPath);
        //}

        // Dynamic PDF Name
        //string fileName = "TB_IDCard_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
        //string pdfPath = Path.Combine(folderPath, fileName);

        //// QR Text
        //string qrText = "TB001|Ashu Kumar|Jaipur";
        //string imgname = "TB00";
        //// QR Image Path
        //string qrPath = Path.Combine(folderPath, imgname+".jpg");

        //// Generate QR Code
        //QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);

        //QRCode qrCode = new QRCode(qrCodeData);

        //using (Bitmap qrBitmap = qrCode.GetGraphic(20))
        //{
        //    qrBitmap.Save(qrPath, ImageFormat.Png);
        //}

        //// Create PDF
        //Document doc = new Document(PageSize.A4, 20, 20, 20, 20);

        //PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(pdfPath, FileMode.Create));

        //doc.Open();

        // HTML Design
        ///   System.Text.StringBuilder sb = new System.Text.StringBuilder();

        //sb.Append("<html>");
        //sb.Append("<body>");

        //sb.Append("<table border='1' width='350px' cellpadding='5' cellspacing='0' align='center'>");

        //// Header
        //sb.Append("<tr>");
        //sb.Append("<td  align='center' style='background-color:red;color:white;'>");
        //sb.Append("<h2>TEAM BALIKA ID CARD</h2>");
        //sb.Append("</td>");
        //sb.Append("</tr>");

        //// Photo + Details
        //sb.Append("<tr>");

        //sb.Append("<td width='120px' align='center'>");
        //sb.Append("<img src='https://via.placeholder.com/100' width='100' height='100' />");
        //sb.Append("</td>");

        //sb.Append("<td>");
        //sb.Append("<b>Name :</b> Ashu Kumar<br/><br/>");
        //sb.Append("<b>Village :</b> Jaipur<br/><br/>");
        //sb.Append("<b>TB Code :</b> TB001<br/><br/>");
        //sb.Append("<b>Joining :</b> 20-May-2026<br/>");
        //sb.Append("</td>");

        //sb.Append("</tr>");

        //// QR Code
        //sb.Append("<tr>");
        //sb.Append("<td colspan='2' align='center'>");
        //sb.Append("<img src='" + qrPath + "' width='120' height='120' />");
        //sb.Append("</td>");
        //sb.Append("</tr>");

        //sb.Append("</table>");

        //sb.Append("</body>");
        //sb.Append("</html>");

        //// Convert HTML to PDF
        //using (StringReader sr = new StringReader(sb.ToString()))
        //{
        //    XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, sr);
        //}

        //doc.Close();

        //Console.WriteLine("PDF Generated Successfully");
        //Console.WriteLine(pdfPath);

        //string folderPath = @"D:\GeneratedIDCards\";

        //if (!Directory.Exists(folderPath))
        //{
        //    Directory.CreateDirectory(folderPath);
        //}

        // Dynamic PDF Name
        //string fileName = "TeamBalika_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";

        //string pdfPath = Path.Combine(folderPath, fileName);

        // Image Path
        string bgImage = @"D:\GeneratedIDCards\images\backgroundsvg.png";
        string profileImage = @"D:\GeneratedIDCards\images\blank_img.png";
        string textLogo = @"D:\GeneratedIDCards\images\text.svg";
        string footerLogo = @"D:\GeneratedIDCards\images\text1.svg";
        string name = "XXX Singh";
        string village = "Gurgaon";
        string code = "TB001";
        string doj = "22-May-2026";
        string cluster = "North Cluster";

        //// Image Path
        //string bgImage =  Server.MapPath(Comman.GetImagePath("ImgPage") +"/backgroundsvg.png");
        //string profileImage = Server.MapPath(Comman.GetImagePath("ImgPage") +"/blank_img.png");
        //string textLogo = Server.MapPath(Comman.GetImagePath("ImgPage") +"/text.svg");
        //string footerLogo = Server.MapPath(Comman.GetImagePath("ImgPage") +"/text1.svg");

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.Append(@"
        <!DOCTYPE html>
        <html>
     
        <body>");

        sb.Append(@"
        <div style='max-width:480px;height:auto;margin:auto;padding:20px;'>

        <table style='font-family:Arial;border-collapse:collapse;width:100%;border:1px solid #dddddd;'>

            <tr>
                <td style='padding:8px 15px;text-align:center;'>

                    <div style='position:relative;'>

                        <img src='file:///" + bgImage.Replace("\\", "/") + @"' style='width:100%;'/>

                    </div>

                </td>
            </tr>

            <tr>
                <td style='text-align:center;padding-top:10px;'>
                    <img src='file:///" + textLogo.Replace("\\", "/") + @"' width='30%'/>
                </td>
            </tr>

            <tr>
                <td style='padding:8px 15px;'>
                    <div style='font-size:12px;'>NAME</div>
                    <div style='font-weight:bold;border-bottom:2px solid #ccc;padding:10px 0;font-size:16px;'>
                        " + name + @"
                    </div>
                </td>
            </tr>

            <tr>
                <td style='padding:8px 15px;'>
                    <div style='font-size:12px;'>VILLAGE</div>
                    <div style='font-weight:bold;border-bottom:2px solid #ccc;padding:10px 0;font-size:16px;'>
                        " + village + @"
                    </div>
                </td>
            </tr>

            <tr>
                <td style='padding:8px 15px;'>
                    <div style='font-size:12px;'>TEAM BALIKA CODE</div>
                    <div style='font-weight:bold;border-bottom:2px solid #ccc;padding:10px 0;font-size:16px;'>
                        " + code + @"
                    </div>
                </td>
            </tr>

            <tr>
                <td style='padding:8px 15px;'>
                    <div style='font-size:12px;'>DATE OF JOINING</div>
                    <div style='font-weight:bold;border-bottom:2px solid #ccc;padding:10px 0;font-size:16px;'>
                        " + doj + @"
                    </div>
                </td>
            </tr>

            <tr>
                <td style='padding:8px 15px;'>
                    <div style='font-size:12px;'>CLUSTER</div>
                    <div style='font-weight:bold;border-bottom:2px solid #ccc;padding:10px 0;font-size:16px;'>
                        " + cluster + @"
                    </div>
                </td>
            </tr>

            <tr>
                <td style='text-align:center;padding:15px;'>
                    <img src='file:///" + footerLogo.Replace("\\", "/") + @"' width='30%'/>
                </td>
            </tr>

        </table>

        </div>");

        sb.Append("</body></html>");

        // PDF Generate
        using (MemoryStream ms = new MemoryStream())
        {
            Document pdfDoc = new Document(PageSize.A4, 20f, 20f, 20f, 20f);

            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, ms);

            pdfDoc.Open();

            using (StringReader sr = new StringReader(sb.ToString()))
            {
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            }

            pdfDoc.Close();

            // Download PDF
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=TeamBalikaCard.pdf");
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.BinaryWrite(ms.ToArray());
            Response.End();
        }
    }
    //System.Text.StringBuilder sb = new System.Text.StringBuilder();

    //sb.Append(@"
    //<!DOCTYPE html>
    //<html>
    //<head>
    //    <style>

    //        body{
    //            font-family: Arial;
    //        }

    //        .page_container {
    //            max-width: 480px;
    //            margin: auto;
    //            padding: 20px;
    //        }

    //        table {
    //            border-collapse: collapse;
    //            width: 100%;
    //            border: 1px solid #dddddd;
    //        }

    //        td {
    //            padding: 8px 15px;
    //        }

    //        .imgdiv{
    //            position:relative;
    //        }

    //        .bg_img{
    //            width:100%;
    //        }

    //        .user_img{
    //            position:absolute;
    //            width:150px;
    //            height:150px;
    //            left:130px;
    //            top:120px;
    //            border-radius:50%;
    //        }

    //        .bottom-text{
    //            border-bottom:1px solid #cccccc;
    //            padding:8px 0px;
    //            font-size:14px;
    //        }

    //        .label{
    //            font-weight:bold;
    //            margin-bottom:5px;
    //        }

    //        .center{
    //            text-align:center;
    //        }

    //    </style>
    //</head>

    //<body>

    //    <div class='page_container'>

    //        <table>

    //            <tr>
    //                <td>

    //                    <div class='imgdiv'>

    //                        <img class='bg_img' src='file:///" + backgroundImg.Replace("\\", "/") + @"' />

    //                        <img class='user_img' src='file:///" + userImg.Replace("\\", "/") + @"' />

    //                    </div>

    //                </td>
    //            </tr>

    //            <tr>
    //                <td class='center'>

    //                    <img width='120'
    //                    src='file:///" + textImg.Replace("\\", "/") + @"' />

    //                </td>
    //            </tr>

    //            <tr>
    //                <td>

    //                    <div class='label'>NAME</div>

    //                    <div class='bottom-text'>
    //                        Ashu Kumar
    //                    </div>

    //                </td>
    //            </tr>

    //            <tr>
    //                <td>

    //                    <div class='label'>VILLAGE</div>

    //                    <div class='bottom-text'>
    //                        Baraura
    //                    </div>

    //                </td>
    //            </tr>

    //            <tr>
    //                <td>

    //                    <div class='label'>TEAM BALIKA CODE</div>

    //                    <div class='bottom-text'>
    //                        TB0001
    //                    </div>

    //                </td>
    //            </tr>

    //            <tr>
    //                <td>

    //                    <div class='label'>DATE OF JOINING</div>

    //                    <div class='bottom-text'>
    //                        21-May-2026
    //                    </div>

    //                </td>
    //            </tr>

    //            <tr>
    //                <td>

    //                    <div class='label'>CLUSTER</div>

    //                    <div class='bottom-text'>
    //                        Cluster A
    //                    </div>

    //                </td>
    //            </tr>

    //            <tr>
    //                <td class='center'>

    //                    <img width='120'
    //                    src='file:///" + text1Img.Replace("\\", "/") + @"' />

    //                </td>
    //            </tr>

    //        </table>

    //    </div>

    //</body>
    //</html>
    //");

    //// Create PDF
    //using (FileStream fs = new FileStream(pdfPath, FileMode.Create))
    //{
    //    Document document = new Document(PageSize.A4, 10, 10, 10, 10);

    //    PdfWriter writer = PdfWriter.GetInstance(document, fs);

    //    document.Open();

    //    using (StringReader sr = new StringReader(sb.ToString()))
    //    {
    //        XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, sr);
    //    }

    //    document.Close();
    //}

    //Console.WriteLine("PDF Generated Successfully");
    //Console.WriteLine(pdfPath);








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
        RefreshControl();
        //  Session["VillageGeographyOperational"] = "";
        Resone.Visible = false;
        rdate.Visible = false;

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

        dtmstM = objMain.LoadData(" SELECT  [UniqueCode],rjob,rBusiness,isnull(PhysicalStatus,0)PhysicalStatus,isnull(Specialization,0)Specialization,isnull(Specially,0)Specially ,isnull(rJobOpportunity,0)rJobOpportunity,rjobother,isnull(IsTeamBalikaAlumni,0) IsTeamBalikaAlumni,AlumniDate,EmpID,Status, WorkingStatus,TbRecruited,DropOutReason,DropoutDate ,ImagePath,DateofJoining,Expectation,Abvision ,mst5Village.[StateCode],mst5Village.[DistrictCode] ,mst5Village.[BlockCode] ,mst5Village.[PanchayatCode]  ,[TBCode] ,[TBName] ,[mstTeamBalika].[VillageCode] ,[Gender] ,[Active],[FatherName] ,[MotherName] ,[SocialCategory]    ,[EducationLevel] ,[FamilyOccupation]  ,[DOBAvailable]  ,[DOB]   ,[AgeAson]  ,[AsOnDate]   ,[Contact]  ,[ReasonForTBChoice]    ,[RecruitmentReferalInfo]  ,[PriorWorkExperience]    ,[TotalPriorWorkExperience]   ,[PriorWorkYearMonth],[TBCode] as UniqueId ,isnull(IsSmartPhone,0) IsSmartPhone,AlternetPhoneNo FROM [dbo].[mstTeamBalika] inner join mst5Village on mst5Village.VillageCode=mstTeamBalika.VillageCode where UniqueCode ='" + pSchoolCOde + "'");

        if (dtmstM.Rows.Count > 0)
        {

            #region School

            string strQry = "  SELECT VillageGeographyOperational FROM mst5Village where villagecode ='" + ddlVillage.SelectedValue + "'     ";
            DataTable dtDistrict = objMain.LoadData(strQry);
            if (dtDistrict.Rows.Count > 0)
            {
                Session["VillageGeographyOperational"] = Convert.ToString(dtDistrict.Rows[0]["VillageGeographyOperational"]);
            }

            //if (Session["user_level"].ToString() == "1")
            //{
            if (dtmstM.Rows[0]["Status"].ToString() == "1")
            {
                btnsave.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                btnsave.Enabled = false;
                btnDelete.Enabled = false;
            }
            DataTable dt = objMain.LoadData("SELECT  * from tblAttendance where TBID='" + pSchoolCOde + "'");
            if (dt.Rows.Count > 0)
            {
                txtday.Text = dt.Rows.Count.ToString();
            }
            else
            {
                txtday.Text = "";
            }
            //ddlState.SelectedValue = dtmstM.Rows[0]["StateCode"].ToString();
            //FillCBDist();
            //ddlDistrict.SelectedValue = dtmstM.Rows[0]["DistrictCode"].ToString().Trim();
            //FillCBBock();
            //ddlBlock.SelectedValue = dtmstM.Rows[0]["BlockCode"].ToString();
            //FillCBCluster();
            //ddlPanchayat.SelectedValue = dtmstM.Rows[0]["PanchayatCode"].ToString().Trim();
            //FillCVillage();
            //ddlVillage.SelectedValue = dtmstM.Rows[0]["VillageCode"].ToString().Trim();

            ViewState["TMCode"] = pSchoolCOde;
            txtIDNO.Text = dtmstM.Rows[0]["UniqueId"].ToString();
            txtName.Text = dtmstM.Rows[0]["TBName"].ToString().Trim();
            ddlGender.SelectedValue = dtmstM.Rows[0]["Gender"].ToString();
            ddltbRecruited.SelectedValue = dtmstM.Rows[0]["TbRecruited"].ToString();
            ddlSmart.SelectedValue = dtmstM.Rows[0]["IsSmartPhone"].ToString();

            txtxAlternate.Text = dtmstM.Rows[0]["AlternetPhoneNo"].ToString().Trim();
            ddloccu.SelectedValue = dtmstM.Rows[0]["FamilyOccupation"].ToString();
            ddlWorkingStatus.SelectedValue = dtmstM.Rows[0]["WorkingStatus"].ToString();
            ddlPhysicalStatus.SelectedValue = dtmstM.Rows[0]["PhysicalStatus"].ToString();
            ddlSp_SelectedIndexChanged(ddlPhysicalStatus, null);
            ddlSpecially.SelectedValue = dtmstM.Rows[0]["Specially"].ToString();

            EmpID.Visible = false;
            divbus.Visible = false;
            txtBus.Text = "";
            divJobOp.Visible = false;
            ddlJobOpportunity.SelectedIndex = 0;
            divOtherJob.Visible = false;
            txtotherjob.Text = "";
            if (ddlWorkingStatus.SelectedIndex > 0)
            {
                if (Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 1 && Convert.ToString(Session["VillageGeographyOperational"]) == "2")
                {
                    ddlStatusReasone.SelectedValue = dtmstM.Rows[0]["DropOutReason"].ToString();
                    ddlStatusReasone_SelectedIndexChanged(ddlBlock, null);
                    DateTime DateDrop = Convert.ToDateTime(dtmstM.Rows[0]["DropoutDate"].ToString());
                    txtDropDate.Text = DateDrop.ToString("dd/MM/yyy");

                    Resone.Visible = false;
                    rdate.Visible = false;
                    ddlAlumni.SelectedValue = dtmstM.Rows[0]["IsTeamBalikaAlumni"].ToString();
                    ddlAlumni_SelectedIndexChanged(ddlAlumni, null);
                    if (ddlAlumni.SelectedIndex > 0)
                    {
                        if (Convert.ToInt32(ddlAlumni.SelectedValue) == 1)
                        {
                            DateTime AlumniDate = Convert.ToDateTime(dtmstM.Rows[0]["AlumniDate"].ToString());
                            txtAlumniDate.Text = AlumniDate.ToString("dd/MM/yyy");
                        }
                        else
                        {
                            txtAlumniDate.Text = "";
                        }
                    }

                    divAlumni.Visible = true;
                }
                else if (Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 2)
                {
                    if (dtmstM.Rows[0]["DropOutReason"].ToString() == "4")
                    {
                        ddlStatusReasone.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlStatusReasone.SelectedValue = dtmstM.Rows[0]["DropOutReason"].ToString();
                    }
                    ddlStatusReasone_SelectedIndexChanged(ddlBlock, null);
                    DateTime DateDrop = Convert.ToDateTime(dtmstM.Rows[0]["DropoutDate"].ToString());
                    txtDropDate.Text = DateDrop.ToString("dd/MM/yyy");

                    Resone.Visible = true;
                    rdate.Visible = true;
                    ddlAlumni.SelectedValue = dtmstM.Rows[0]["IsTeamBalikaAlumni"].ToString();
                    ddlAlumni_SelectedIndexChanged(ddlAlumni, null);
                    if (ddlAlumni.SelectedIndex > 0)
                    {
                        if (Convert.ToInt32(ddlAlumni.SelectedValue) == 1)
                        {
                            DateTime AlumniDate = Convert.ToDateTime(dtmstM.Rows[0]["AlumniDate"].ToString());
                            txtAlumniDate.Text = AlumniDate.ToString("dd/MM/yyy");
                        }
                        else
                        {
                            txtAlumniDate.Text = "";
                        }
                    }

                    txtJob.Text = dtmstM.Rows[0]["rjob"].ToString().Trim();
                    txtBus.Text = dtmstM.Rows[0]["rBusiness"].ToString().Trim();
                    ddlJobOpportunity.SelectedValue = dtmstM.Rows[0]["rJobOpportunity"].ToString();
                    if (ddlJobOpportunity.SelectedIndex > 0)
                    {
                        ddlOther_SelectedIndexChanged(ddlAlumni, null);

                    }
                    txtotherjob.Text = dtmstM.Rows[0]["rjobother"].ToString().Trim();

                    divAlumni.Visible = true;
                }
                else
                {
                    Resone.Visible = false;
                    rdate.Visible = false;
                    txtDropDate.Text = "";
                    txtEmployeeID.Text = "";
                    EmpID.Visible = false;
                    ddlStatusReasone.SelectedIndex = 0;
                    ddlAlumni.SelectedIndex = 0;
                    txtAlumniDate.Text = "";
                    ddlAlumni.SelectedIndex = 0;
                    divAlumni.Visible = false;
                    divAlumni1.Visible = false;
                }
            }
            else
            {
                Resone.Visible = false;
                rdate.Visible = false;
                txtAlumniDate.Text = "";
                divAlumni1.Visible = false;
            }




            txtEmployeeID.Text = dtmstM.Rows[0]["EmpID"].ToString().Trim();
            ddlEducation.SelectedValue = dtmstM.Rows[0]["EducationLevel"].ToString();
            ddlCategory.SelectedValue = dtmstM.Rows[0]["SocialCategory"].ToString();
            ddlReason.SelectedValue = dtmstM.Rows[0]["ReasonForTBChoice"].ToString();

            ddlSpecialization_SelectedIndexChanged(ddlEducation, null);
            ddlSpecialization.SelectedValue = dtmstM.Rows[0]["Specialization"].ToString();

            ddlSours.SelectedValue = dtmstM.Rows[0]["RecruitmentReferalInfo"].ToString();
            if (Convert.ToBoolean(dtmstM.Rows[0]["PriorWorkExperience"].ToString()) == true)
            {
                ddlWorkEx.SelectedIndex = 1;
            }
            else
            {
                ddlWorkEx.SelectedIndex = 2;
            }
            txtFatherName.Text = dtmstM.Rows[0]["FatherName"].ToString().Trim();
            txtMotherName.Text = dtmstM.Rows[0]["MotherName"].ToString().Trim();
            txtContact.Text = dtmstM.Rows[0]["Contact"].ToString().Trim();
            txtDuartion.Text = "";
            txtMonth.Text = "";
            if (dtmstM.Rows[0]["TotalPriorWorkExperience"].ToString() == "0")
            {
            }
            else
            {
                txtDuartion.Text = dtmstM.Rows[0]["TotalPriorWorkExperience"].ToString().Trim();
            }
            if (dtmstM.Rows[0]["PriorWorkYearMonth"].ToString() == "0")
            {
            }
            else
            {
                txtMonth.Text = dtmstM.Rows[0]["PriorWorkYearMonth"].ToString().Trim();
            }

            if (dtmstM.Rows[0]["DateofJoining"].ToString() != "")
            {
                DateTime DateJoing = Convert.ToDateTime(dtmstM.Rows[0]["DateofJoining"].ToString());
                txtJoingDate.Text = DateJoing.ToString("dd/MM/yyy");
            }
            else
            {
                txtJoingDate.Text = "";
            }

            ddlDob.SelectedValue = dtmstM.Rows[0]["DOBAvailable"].ToString();
            txtExp.Text = dtmstM.Rows[0]["Expectation"].ToString().Trim();
            txtAbv.Text = dtmstM.Rows[0]["Abvision"].ToString().Trim();
            if (dtmstM.Rows[0]["ImagePath"].ToString() != "")
            {
                //string sFileDir = Server.MapPath(Comman.GetImagePath("ImgPage") + dtmstM.Rows[0]["ImagePath"].ToString().Trim() + "");
                //string sFileDir = Request.PhysicalApplicationPath + "images\\";
                string imagename = dtmstM.Rows[0]["ImagePath"].ToString().Trim();
                ViewState["ImagePath"] = imagename;
                imgMKS.ImageUrl = ResolveUrl("~/DataBackup/" + imagename);
            }
            else
            {
                ViewState["ImagePath"] = "";

                imgMKS.ImageUrl = null;
            }
            if (Convert.ToInt32(ddlDob.SelectedValue) == 1)
            {
                DateTime dob = Convert.ToDateTime(dtmstM.Rows[0]["DOB"].ToString());
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

                txtAge.Text = dtmstM.Rows[0]["AgeAson"].ToString();
                DateTime dob = Convert.ToDateTime(dtmstM.Rows[0]["AsOnDate"].ToString());
                txtDate.Text = dob.ToString("dd/MM/yyy");
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
        DataTable dtfilter = dt.Copy();


        strFilter = str + " like '%" + txtSearchName.Text.Trim() + "%'   ";

        //dtSoSaleOrder.Select(txtSearch.SelectedValue.ToString() + " like '" + txtSearch.Text + "%'";


        dtfilter.DefaultView.RowFilter = strFilter;
        dtfilter.DefaultView.Sort = "TBName asc";
        GVMain.DataSource = dtfilter.DefaultView.ToTable();
        GVMain.DataBind();

    }
    protected void txtJoingDate_OnTextChanged(object sender, EventArgs e)
    {
        DataTable dt = objMain.LoadData("Select StartYear from mst2District where DistrictCode ='" + ddlDistrict.SelectedValue + "'");
        if (dt.Rows.Count > 0)
        {
            HdnStartYear.Text = dt.Rows[0]["StartYear"].ToString();
        }
        // ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + txtJoingDate.Text + "')</script>", false);

        string[] Jd = (txtJoingDate.Text).Split('/');

        int JoiningYear = Convert.ToInt32(Jd[2].Trim());
        if (txtJoingDate.Text != "")
        {
            if (JoiningYear < Convert.ToInt32(HdnStartYear.Text))
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Joining year can not be Less than district start year')</script>", false);
                txtJoingDate.Text = "";
            }

        }
    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {

        GVMainBind();
        pnlMain.Enabled = false;
    }
    protected void ddlSpecialization_SelectedIndexChanged(object sender, EventArgs e)
    {
        conditions = "";
        conditions = "MainID ='" + ddlEducation.SelectedValue + "'  ";
        objComman.BindDLL("mstEducationStatusdetails", "EID,StatusName", conditions, "EID", "asc", ddlSpecialization, "StatusName", "EID", "Select");
        divSpc.Visible = false;
        if (Convert.ToInt32(ddlEducation.SelectedValue) == 5 || Convert.ToInt32(ddlEducation.SelectedValue) == 7 || Convert.ToInt32(ddlEducation.SelectedValue) == 9)
        {
            divSpc.Visible = true;
        }

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
    protected void btnDownloadPDF_Click(object sender, EventArgs e)
    {
        Document pdfDoc = new Document(PageSize.A4); MemoryStream ms = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, ms); pdfDoc.Open();
        pdfDoc.Add(new Paragraph("Team Balika Details"));
        pdfDoc.Add(new Paragraph(" "));
        pdfDoc.Add(new Paragraph("TB Code: " + txtIDNO.Text));
        pdfDoc.Add(new Paragraph("Name: " + txtName.Text));
        pdfDoc.Add(new Paragraph("Contact Number: " + txtContact.Text));
        pdfDoc.Add(new Paragraph("Alternate Number: " + txtxAlternate.Text));
        pdfDoc.Add(new Paragraph("Father Name: " + txtFatherName.Text));
        pdfDoc.Add(new Paragraph("Mother Name: " + txtMotherName.Text));
        pdfDoc.Add(new Paragraph("Gender: " + ddlGender.SelectedItem.Text));
        pdfDoc.Add(new Paragraph("DOB: " + txtDate.Text));
        pdfDoc.Add(new Paragraph("Status: " + ddlWorkingStatus.SelectedItem.Text));
        pdfDoc.Close();

        byte[] bytes = ms.ToArray();

        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=TeamBalika.pdf");
        Response.Buffer = true;
        Response.Clear();
        Response.BinaryWrite(bytes);
        Response.End();

    }
    protected void ddlSp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPhysicalStatus.SelectedIndex > 0)
        {
            if (ddlPhysicalStatus.SelectedValue == "1")
            {
                divSp.Visible = true;
            }
            else
            {
                ddlSpecially.SelectedIndex = 0;
                divSp.Visible = false;
            }
        }
        else
        {
            ddlSpecially.SelectedIndex = 0;
            divSp.Visible = false;
        }
    }
}