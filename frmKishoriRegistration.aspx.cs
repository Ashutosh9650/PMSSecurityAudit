using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class frmKishoriRegistration : System.Web.UI.Page
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
                DateTime startDate = new DateTime();

                startDate = System.DateTime.Now.AddDays(-20);
                CalendarExtender2.StartDate = startDate;
                CalendarExtender2.EndDate = System.DateTime.Now;
                CalendarExtender3.StartDate = startDate;
                CalendarExtender3.EndDate = System.DateTime.Now;
                //GVMainBind();
                LoadYear();
                BindcompletionYear();
                LoadUserLeavel();

                FillSocialCat();
                //   FillDropResone();
                ViewState["Save"] = "Save";
                FillKishoricontact();
                FillReason();
                FillClass();
                FillExamType();
                btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
                ValdateUserLavel();

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


    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlType.SelectedIndex == 1)
        {
            pnlMain.Enabled = false;
            pnlMain.Visible = true;
            pnlattendance.Visible = false;
            GVMain.DataSource = null;
            GVMain.DataBind();
            Gvattendance.DataSource = null;
            Gvattendance.DataBind();
            RefreshControl();
        }
        else
        {
            GVMain.DataSource = null;
            GVMain.DataBind();
            Gvattendance.DataSource = null;
            Gvattendance.DataBind();

            pnlMain.Visible = false;
            pnlattendance.Visible = true;
            FillSession();
        }
    }

    protected void ImageButton9_Click(object sender, EventArgs e)
    {
        string Prarakcode = "";
        Int32 mainResult = 0;
        string Prarakname = "", VillageCode = "", opertion = "";

        Prarakcode = txtMPrerakCode.Text;
        Prarakname = txtMPrerakName.Text;
        VillageCode = ddlVillage.SelectedValue;
        opertion = "Save";
        mainResult = SaveDataPrarak(Prarakcode, Prarakname, VillageCode, Convert.ToInt32(Session["UserID"]), opertion);
        if (mainResult > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
            string conditions = "VillageCode= '" + ddlVillage.SelectedValue + "'";
            FillPrarakList(conditions);
        }
    }

    public int SaveDataPrarak(string Prarakcode, string Prarakname, string VillageCode, int UserId, string opertion)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Prarakcode", Prarakcode),
            new SqlParameter("@Prarakname", Prarakname),
            new SqlParameter("@VillageCode", VillageCode),
            new SqlParameter("@UserId", UserId),
            new SqlParameter("@pOperation", opertion),

        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatePrarak", cmdParameters);
    }

    public void FillClass()
    {
        conditions = "";
        conditions = "LookupFlag ='CL' and LookupCode in(15,17) and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "Description", "asc", ddlClass, "Description", "LookupCode", "Select");



    }
    public void FillDropResone()
    {
        //conditions = "";
        //conditions = "LookupFlag ='TMR' and Active=1 ";
        //objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlStatusReasone, "Description", "LookupCode", "Select");



    }
    public void FillExamType()
    {
        conditions = "";
        conditions = "LookupFlag ='RO' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "Description", "asc", ddlExamType, "Description", "LookupCode", "Select");


        conditions = "";
        conditions = "LookupFlag ='CL' and LookupCode in(6,7,8,9,10,11,12,13,14,15,16) and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlLastClass, "Description", "LookupCode", "Select");


        string strQry = " select *  from [mstLookup]   where LookupFlag='KV' ";


        DataTable dtRole = objMain.LoadData(strQry);
        CBL_bookformat.DataSource = dtRole;
        CBL_bookformat.DataTextField = "Description";
        CBL_bookformat.DataValueField = "LookupCode";
        CBL_bookformat.DataBind();

        string strQry1 = " select *  from [mstLookup]   where LookupFlag='KS' ";


        DataTable dtRole1 = objMain.LoadData(strQry1);
        CBL_bookformatNew.DataSource = dtRole1;
        CBL_bookformatNew.DataTextField = "Description";
        CBL_bookformatNew.DataValueField = "LookupCode";
        CBL_bookformatNew.DataBind();

    }

    public void FillPrarakList(string conditions)
    {
        string strQry = " Select  upper(PrerakName) +' ('+  Prerakcode +')' PrerakName, ID,	VillageCode,	ID as Prerakcode from Tbl_PreakCode where " + conditions + "  order by PrerakName ";
        DataTable dtTbl_PreakCode = objMain.LoadData(strQry);

        objComman.BindDLLMasterTableVillage("Tbl_PreakCode", "Prerakcode,PrerakName", dtTbl_PreakCode, conditions, "PrerakName", "asc", ddlPrerakName, "PrerakName", "Prerakcode", "Select");

        objComman.BindDLLMasterTableVillage("Tbl_PreakCode", "Prerakcode,PrerakName", dtTbl_PreakCode, conditions, "PrerakName", "asc", ddlattendancePrarak, "PrerakName", "Prerakcode", "");

    }
    public void FillSession()
    {
        conditions = "";
        conditions = "Flag=50 and Language=0 ";
        objComman.BindDLL("MSTSession", "TopicDiscussName,TopicDIscussIDNew", conditions, "TopicDIscussIDNew", "asc", ddlsession, "TopicDiscussName", "TopicDIscussIDNew", "Select");

    }


    public void FillSocialCat()
    {
        conditions = "";
        conditions = "LookupFlag ='CAT' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlCategory, "Description", "LookupCode", "Select");



    }
    public void FillReason()
    {
        conditions = "";
        conditions = "LookupFlag ='KRS' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlReason, "Description", "LookupCode", "Select");



    }

    public void FillKishoricontact()
    {
        conditions = "";
        conditions = "LookupFlag ='KN' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlKishoricontact, "Description", "LookupCode", "Select");



    }


    public void ValdateUserLavel()
    {

        string strQry = "";
        string Cond = "Module='Pragati CBL'";
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
        conditions = "";
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
    protected void ddlddlReason_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlReason.SelectedValue) == 9)
        {
            Resone.Visible = true;
            txtOther.Text = "";
        }
        else
        {
            txtOther.Text = "";
            Resone.Visible = false;

        }
    }
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
        string conditions = "VillageCode= '" + ddlVillage.SelectedValue + "'";
        FillPrarakList(conditions);
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


        if (ddlVillage.SelectedValue != null && ddlVillage.SelectedIndex > 0)
        {
            str = str + "where VillageCode='" + ddlVillage.SelectedValue.ToString() + "'";
        }
        if (ddlCampID.SelectedValue != null && ddlCampID.SelectedIndex > 0)
        {
            str = str + "and CampID='" + ddlCampID.SelectedValue.ToString() + "'";
        }
        if (ddlType.SelectedValue != null && ddlType.SelectedIndex > 0)
        {
            str = str + "and Type='" + ddlType.SelectedValue.ToString() + "'";
        }
        SqlParameter[] cmdParameters = new SqlParameter[]
{
new SqlParameter("@con", str),

};
        DataTable dtmstM = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadChildRegistrationPragati]", cmdParameters);

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

    protected void ddlsession_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GVattendanceBind();
    }
    private void GVattendanceBind()
    {

        string str = " where IsDropout is NULL", str1 = "";
        if (ddlVillage.SelectedValue != null && ddlVillage.SelectedIndex > 0)
        {
            str = str + " and tblChildRegistrationPragati.VillageCode='" + ddlVillage.SelectedValue.ToString() + "'";
            str1 = str1 + "where tblChildAttendancePragati.VillageCode='" + ddlVillage.SelectedValue.ToString() + "'";
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Village')</script>", false);
            return;
        }
        if (ddlCampID.SelectedValue != null && ddlCampID.SelectedIndex > 0)
        {
            str = str + " and tblChildRegistrationPragati.CampID='" + ddlCampID.SelectedValue.ToString() + "'";
            str1 = str1 + " and tblChildAttendancePragati.CampID='" + ddlCampID.SelectedValue.ToString() + "'";
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select CampId')</script>", false);
            return;
        }
        if (ddlsession.SelectedValue != null && ddlsession.SelectedIndex > 0)
        {
            str1 = str1 + " and Session='" + ddlsession.SelectedValue.ToString() + "'";
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Session')</script>", false);
            return;
        }

        btnsave.Visible = true;
        SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@con", str1),
             new SqlParameter("@Flag", "2"),

            };
        DataTable dtmstAttendance = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadChildAttendancePragati2022]", cmdParameters);

        if (dtmstAttendance.Rows.Count > 0)
        {
            Gvattendance.DataSource = dtmstAttendance;
            Gvattendance.DataBind();
            ViewState["Gvatt"] = dtmstAttendance;
            DateTime DOB = Convert.ToDateTime(dtmstAttendance.Rows[0]["AttDate"].ToString());
            TxtAttendanceDate.Text = DOB.ToString("dd/MM/yyy");
            //TxtAttendanceDate.Text = Convert.ToString(dtmstAttendance.Rows[0]["AttDate"]);
            ddlsession.SelectedValue = Convert.ToString(dtmstAttendance.Rows[0]["Session"]);
            ddlattendancePrarak.SelectedValue = Convert.ToString(dtmstAttendance.Rows[0]["Prarakcode"]);
            if (dtmstAttendance.Rows[0]["CreateDate"].ToString() != "")
            {
                DateTime CreateDate = Convert.ToDateTime(dtmstAttendance.Rows[0]["CreateDate"].ToString());
                DateTime Todate = DateTime.Today;
                if (CreateDate.ToString("dd/MM/yyy") != Todate.ToString("dd/MM/yyy"))
                {
                    btnsave.Visible = false;
                }
                else
                {
                    btnsave.Visible = true;
                }
            }
        }
        else
        {
            SqlParameter[] cmdParameter = new SqlParameter[]
            {
            new SqlParameter("@con", str),
            new SqlParameter("@Flag", "1"),
            };
            DataTable dtmstAttend = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadChildAttendancePragati2022]", cmdParameter);


            Gvattendance.DataSource = dtmstAttend;
            Gvattendance.DataBind();
            if (dtmstAttend.Rows.Count > 0)
            {

            }

        }
    }

    public void AttClear()
    {
        TxtAttendanceDate.Text = "";

        ddlattendancePrarak.SelectedValue = "0";

    }

    protected void IsDropOut_OnCheckedChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
        int index = row.RowIndex;
        CheckBox cb1 = (CheckBox)Gvattendance.Rows[index].FindControl("IsDropOut");
        DropDownList dropout = (DropDownList)Gvattendance.Rows[index].FindControl("ddlDropoutReason");
        TextBox reson = (TextBox)Gvattendance.Rows[index].FindControl("txtOtherDropoutReason");
        if (cb1.Checked == true)
        {
            dropout.Enabled = true;
        }
        else
        {
            dropout.Enabled = false;
            dropout.SelectedValue = "0";
            reson.Text = "";
            reson.Enabled = false;
        }
    }

    protected void DropoutReason_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
        int index = row.RowIndex;
        DropDownList DropoutReason = (DropDownList)row.FindControl("ddlDropoutReason");
        TextBox reson = (TextBox)Gvattendance.Rows[index].FindControl("txtOtherDropoutReason");

        if (DropoutReason.SelectedValue == "8")
        {
            reson.Enabled = true;
        }
        else
        {
            reson.Enabled = false;
            reson.Text = "";
        }

    }

    protected void Gvattendance_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlFl = (DropDownList)e.Row.FindControl("ddlDropoutReason");
            DropDownList ddlF2 = (DropDownList)e.Row.FindControl("ddlRegistrationType");
            Label IsDropOut = ((Label)e.Row.FindControl("lblIsDropOut"));
            TextBox txtOtherDropoutReason = ((TextBox)e.Row.FindControl("txtOtherDropoutReason"));
            Label Present = ((Label)e.Row.FindControl("lblPresent"));
            Label DropoutReason = ((Label)e.Row.FindControl("lblDropoutReason"));


            conditions = "LookupFlag ='RO' and Active=1 ";
            objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "Description", "asc", ddlExamType, "Description", "LookupCode", "Select");
            string strQry = " Select TopicDiscussName, TopicDIscussIDNew from MSTSession where Flag=51 and Language=0  order by TopicDiscussName ";
            DataTable dtreason = objMain.LoadData(strQry);

            DataRow dr;
            dr = dtreason.NewRow();
            dr["TopicDiscussName"] = "--Select--";
            dr["TopicDIscussIDNew"] = "0";
            dtreason.Rows.InsertAt(dr, 0);
            dtreason.AcceptChanges();
            ddlFl.DataSource = dtreason;
            ddlFl.DataTextField = "TopicDiscussName";
            ddlFl.DataValueField = "TopicDIscussIDNew";
            ddlFl.DataBind();

            DataTable dt = new DataTable();
            dt = ViewState["Gvatt"] as DataTable;
            RadioButtonList Rbtn_present = (RadioButtonList)e.Row.FindControl("rdbtn_Present");
            CheckBox chk = (CheckBox)e.Row.FindControl("IsDropOut");

            if (IsDropOut.Text.ToString() == "1")
            {
                chk.Checked = true;
                ddlFl.Enabled = true;
            }
            else
            {
                chk.Checked = false;
            }
            if (Present.Text.ToString() == "1")
            {
                Rbtn_present.SelectedValue = "1";
            }
            if (Present.Text.ToString() == "2")
            {

                Rbtn_present.SelectedValue = "2";
            }
            if (DropoutReason.Text.ToString() != "0")
            {
                ddlFl.SelectedValue = Convert.ToString(DropoutReason.Text);
                if (ddlFl.SelectedValue == "8")
                {
                    txtOtherDropoutReason.Enabled = true;
                }
            }
            else
            {
                ddlFl.SelectedValue = "0";
            }

        }
    }

    #region Save Attendance
    public void SaveAttData()
    {
        try
        {
            int result = 0;
            for (int i = 0; i < Gvattendance.Rows.Count; i++)
            {

                string attdate = Convert.ToString(TxtAttendanceDate.Text);
                string PrerakCode = ddlattendancePrarak.SelectedValue;
                string session = ddlsession.SelectedValue;
                RadioButtonList rdbtn_Present = (RadioButtonList)Gvattendance.Rows[i].FindControl("rdbtn_Present");
                CheckBox IsDropOut = (CheckBox)Gvattendance.Rows[i].FindControl("IsDropOut");
                DropDownList ddlDropoutReason = (DropDownList)Gvattendance.Rows[i].FindControl("ddlDropoutReason");
                string DropoutReason = Convert.ToString(ddlDropoutReason.SelectedValue);
                TextBox txtOtherDropoutReason = (TextBox)Gvattendance.Rows[i].FindControl("txtOtherDropoutReason");
                string OtherDropoutReason = Convert.ToString(txtOtherDropoutReason.Text);

                if (attdate == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Attendance  date')</script>", false);
                    return;
                }

                if (session == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Session')</script>", false);
                }
                if (PrerakCode == "0")
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Prerak Name Session')</script>", false);
                    return;
                }
                if (rdbtn_Present.SelectedValue == "")
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select  Present/Absent')</script>", false);
                    return;
                }

                if (IsDropOut.Checked == true)
                {
                    if (DropoutReason == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Prerak Name Session')</script>", false);
                        return;
                    }
                    if (DropoutReason == "8")
                    {
                        if (OtherDropoutReason == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Prerak Name Session')</script>", false);
                            return;
                        }
                    }
                }
            }

            for (int i = 0; i < Gvattendance.Rows.Count; i++)
            {
                string flag = "";
                string UniqueCode = Gvattendance.DataKeys[i]["UniqueCode"].ToString(); ;
                if (Convert.ToString(UniqueCode) == "")
                {
                    UniqueCode = objComman.Generate_RandomStringAnu(8);
                    flag = "I";
                }
                else
                {
                    flag = "U";
                }

                string UniqueChildRCode = Gvattendance.DataKeys[i]["UniqueChildRCode"].ToString();
                string VillageCode = Gvattendance.DataKeys[i]["VillageCode"].ToString();
                string CampID = Gvattendance.DataKeys[i]["CampID"].ToString();
                string attdate = Convert.ToString(TxtAttendanceDate.Text);
                string PrerakCode = ddlattendancePrarak.SelectedValue;
                string session = ddlsession.SelectedValue;
                Label lblKishoriName = (Label)Gvattendance.Rows[i].FindControl("lblKishoriName");
                Label lblClass = (Label)Gvattendance.Rows[i].FindControl("lblClass");
                Label lblRegistrationType = (Label)Gvattendance.Rows[i].FindControl("lblRegistrationType");
                RadioButtonList rdbtn_Present = (RadioButtonList)Gvattendance.Rows[i].FindControl("rdbtn_Present");
                int Present = Convert.ToInt32(rdbtn_Present.SelectedValue);
                CheckBox IsDropOut = (CheckBox)Gvattendance.Rows[i].FindControl("IsDropOut");
                int DropOut = Convert.ToInt32(IsDropOut.Checked);
                DropDownList ddlDropoutReason = (DropDownList)Gvattendance.Rows[i].FindControl("ddlDropoutReason");
                TextBox txtOtherDropoutReason = (TextBox)Gvattendance.Rows[i].FindControl("txtOtherDropoutReason");


                SqlParameter[] cmdParameters = new SqlParameter[]
                {
            new SqlParameter("@UniqueCode", UniqueCode),
            new SqlParameter("@UniqueChildRCode", UniqueChildRCode),
            new SqlParameter("@VillageCode", VillageCode),
            new SqlParameter("@CampID", CampID),
            new SqlParameter("@Prarakcode", PrerakCode),
            new SqlParameter("@AttDate",  Convert.ToDateTime(attdate).ToString("yyyy-MM-dd")),
            new SqlParameter("@Session", session),
            new SqlParameter("@Present", Present),
            new SqlParameter("@DropOut",DropOut),
            new SqlParameter("@Reason", ddlDropoutReason.SelectedValue),
            new SqlParameter("@Sub_reason", txtOtherDropoutReason.Text),
            new SqlParameter("@createby", Session["username"].ToString()),
            new SqlParameter("@flag", flag)

        };
                result = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateChildattendancePragati", cmdParameters));
            }
            if (result > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                GVattendanceBind();
            }

        }
        catch (Exception)
        {

            throw;
        }

    }
    #endregion

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


    public void BindcompletionYear()
    {

        ddlCompletionYr.Items.Add(new ListItem("--Select--", "0"));
        int Currentyr = System.DateTime.Now.Year;
        for (int i = 2005; i < Currentyr; i++)
        {
            ddlCompletionYr.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }


    }


    private Boolean Validation()
    {
        try
        {
            #region Main
            string Fullfilename = "";
            //if (txtKishoriName.Text == "")
            //{

            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Kishori Name')</script>", false);
            //    return false;
            //}
            if (Convert.ToInt32(ddlReason.SelectedValue) == 9)
            {
                txtOther.Enabled = true;

            }

            if (FileuploadAttach.PostedFile != null && FileuploadAttach.PostedFile.FileName != "")
            {
                string ext = System.IO.Path.GetExtension(FileuploadAttach.PostedFile.FileName).ToLower();
                if (FileuploadAttach.PostedFile.ContentLength < 102400)
                {
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Image size must be less than 100kb')</script>", false);
                    return false;
                }
                if (ext != ".jpeg" && ext != ".jpg" && ext != ".png" && ext != ".gif")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Images')</script>", false);
                    return false;
                }
                string exten = Path.GetExtension(FileuploadAttach.PostedFile.FileName);
                Fullfilename = "" + txtKishoriName.Text + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;
            }
            else
            {
                if (ViewState["Save"].ToString() == "Save")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Image')</script>", false);
                    return false;
                }
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
                        //ShowMessage.Visible = true;
                        //ShowMessage.Style.Add("background-color", "#FFBABA");
                        //MessageLBL.Style.Add("Color", "#D8000C");
                        //MessageLBL.Text = ex.ToString();

                    }
                }
                FileuploadAttach.PostedFile.SaveAs(sFileDir + Fullfilename);

                ViewState["ImagePath"] = Fullfilename;
            }

            //if (ddlVillage.SelectedIndex <= 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Village')</script>", false);
            //    return false;
            //}
            //if (ddlCampID.SelectedIndex <= 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Camp')</script>", false);
            //    return false;
            //}
            //if (ddlType.SelectedIndex <= 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Type')</script>", false);
            //    return false;
            //}
            //if (txtPrerakName.Text=="")
            //{

            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Type Prerak Name')</script>", false);
            //    return false;
            //}

            //if (txtPrerakCode.Text == "")
            //{

            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Type Prerak Name')</script>", false);
            //    return false;
            //}

            //if (txtRegistrationDate.Text == "")
            //{

            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Date')</script>", false);
            //    return false;
            //}


            //if (txtDate.Text == "")
            //{

            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Date')</script>", false);
            //    return false;
            //}

            //int Age = 0;
            //string DateSarveyDate = txtDate.Text;
            //string[] b = DateSarveyDate.Split('/');

            //string DateB = txtRegistrationDate.Text;
            //string[] a = DateB.Split('/');

            //Age = Convert.ToInt32(a[2]) - Convert.ToInt32(b[2]);


            //if (Age < 14)
            //{

            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 14 and 24 years')</script>", false);

            //    return false;

            //}
            //if (Age > 24)
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 14 and 24 years')</script>", false);

            //    return false;
            //}

            //if (Convert.ToInt32(ddlCategory.SelectedValue) <= 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Social Category')</script>", false);


            //    return false;
            //}
            //if (Convert.ToInt32(ddlKishoricontact.SelectedValue) <= 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select-How did you make contact with Kishori')</script>", false);


            //    return false;
            //}
            //if (txtFatherName.Text == "")
            //{

            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Father Name')</script>", false);
            //    return false;
            //}
            //if (txtMotherName.Text == "")
            //{

            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Mother Name')</script>", false);
            //    return false;
            //}
            //if (txtMobile.Text == "")
            //{

            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Mobile Number')</script>", false);
            //    return false;
            //}
            //if (txtxAlternate.Text == "")
            //{

            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Mobile Number')</script>", false);
            //    return false;
            //}
            if (Convert.ToInt32(ddlSmart.SelectedValue) == 1)
            {
                if (txtKishoriMobileNumber.Text == "")
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Mobile Number')</script>", false);
                    return false;
                }
            }
            //if (Convert.ToInt32(ddlLastClass.SelectedValue) <= 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Last Class Completed')</script>", false);


            //    return false;
            //}
            //if (Convert.ToInt32(ddlReason.SelectedValue) <= 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Reason for Dropout')</script>", false);


            //    return false;
            //}

            //string Doc = "";
            //foreach (ListItem item in CBL_bookformat.Items)
            //{

            //    if (item.Selected)
            //    {

            //        Doc += "" + item.Value + "" + ",";


            //    }
            //}
            //if (Doc.Length > 0 || Doc.Length > 0)
            //{
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Document Availability')</script>", false);

            //    return false;
            //}

            //if (Convert.ToInt32(ddlExamType.SelectedValue) <= 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Exam Type')</script>", false);


            //    return false;
            //}

            //string subject = "";
            //int icountA = 0;
            //foreach (ListItem item in CBL_bookformatNew.Items)
            //{

            //    if (item.Selected)
            //    {

            //        subject += "" + item.Value + "" + ",";

            //        icountA = icountA + 1;
            //    }
            //}
            //if (subject.Length > 0 || subject.Length > 0)
            //{
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select at lease 1 Subject')</script>", false);

            //    return false;
            //}

            //if (Convert.ToInt32(ddlExamType.SelectedValue) == 2)
            //{
            //    if (icountA>=5 )
            //    {

            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Minimum 5 Subjects')</script>", false);

            //        return false;
            //    }
            //}

            //if (txtDOBReg.Text == "")
            //{

            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date')</script>", false);
            //    return false;
            //}
            //if (txtRegistration.Text == "")
            //{

            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Registration ID')</script>", false);
            //    return false;
            ////}
            //if (txtRegistration.Text.Length>=10 )
            //{
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Correct Registration ID')</script>", false);
            //    return false;
            //}
            //if (Convert.ToInt32(ddlClass.SelectedValue) <= 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Class of Admission')</script>", false);


            //    return false;
            //}
            #endregion
            return true;

        }
        catch
        {

            return false;
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

    }

    protected void btnSumbit_Click(object sender, EventArgs e)
    {


    }
    private void Save_Update(int SchoolCode)
    {
        int mainResult = 0;
        string G = "";
        string B = "";
        foreach (ListItem item in CBL_bookformat.Items)
        {
            if (item.Selected)
            {

                G += "" + item.Value + "" + ",";

            }
        }
        if (G.Length > 0) { G = G.Substring(0, G.LastIndexOf(",")); }


        foreach (ListItem item in CBL_bookformatNew.Items)
        {
            if (item.Selected)
            {

                B += "" + item.Value + "" + ",";

            }
        }
        if (B.Length > 0) { B = B.Substring(0, B.LastIndexOf(",")); }

        if (ViewState["Save"].ToString() == "Save")
        {
            //DataTable dtCheck = objMain.LoadData(" SELECT * FROM [dbo].[mstTeamBalika]  inner join mst5Village on  mst5Village.VillageCode=mstTeamBalika.VillageCode or  mst5Village.refVillage16=mstTeamBalika.VillageCode	or  mst5Village.refVillage17=mstTeamBalika.VillageCode	or  mst5Village.refVillage18=mstTeamBalika.VillageCode	or  mst5Village.refVillage19=mstTeamBalika.VillageCode	or  mst5Village.refVillage20=mstTeamBalika.VillageCode  	or  mst5Village.refVillage21=mstTeamBalika.VillageCode			  where TBName='" + Name + "' and FatherName='" + FatherName + "' and   mst5Village.VillageCode='" + ddlVillage.SelectedValue + "' ");
            //if (dtCheck.Rows.Count > 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TB Name Allready Exit')</script>", false);
            //    return;
            //}
            //Unique();
            //string TBCode = ViewState["TBCode"].ToString();
            //string schoolod = ViewState["NumNo"].ToString();
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
                Fullfilename = "" + txtKishoriName.Text + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;
            }
            else
            {
                Fullfilename = Convert.ToString(ViewState["ImagePath"]);
            }
            ViewState["Save"] = "fff";

            string strMainIDNo = objMain.Generate_RandomString(8);
            ViewState["TMCode"] = strMainIDNo;


            #region Attach image
            //System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(FileuploadAttach.PostedFile.InputStream);
            //System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 81);


            string sFileDir = Server.MapPath(Comman.GetImagePath("DataBackupPath")); ;
            if (Convert.ToString(ViewState["ImagePath"]).Length > 0)
            {

            }
            else
            {
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

                    Fullfilename = "" + txtKishoriName.Text + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;

                }
            }

            #endregion
            mainResult = SaveDataTeamBalika(strMainIDNo, G, Fullfilename, B, "I");



            if (mainResult > 0)
            {


                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                GVMainBind();
                pnlMain.Enabled = false;
            }
        }
        else
        {


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
                Fullfilename = "" + txtKishoriName.Text.Trim() + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;
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
            else
            {
                Fullfilename = Convert.ToString(ViewState["ImagePath"]);
            }
            #endregion


            mainResult = SaveDataTeamBalika(ViewState["TMCode"].ToString(), G, Fullfilename, B, "U");

            if (mainResult > 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Update sucessfully')</script>", false);
                GVMainBind();
                pnlMain.Enabled = false;
            }

        }



    }
    public int SaveDataTeamBalika(string strMainIDNo, string AvailabilityofDocument, string ImagePath, string Subject, string flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqueChildRCode", strMainIDNo),
            new SqlParameter("@VillageCode", ddlVillage.SelectedValue),
            new SqlParameter("@CampID", ddlCampID.SelectedValue),
            new SqlParameter("@Type", ddlType.SelectedValue),
            new SqlParameter("@PrerakName", ddlPrerakName.SelectedValue),
            new SqlParameter("@PrerakCode", ""),
            new SqlParameter("@Registrationdate",Convert.ToDateTime(txtRegistrationDate.Text).ToString("yyyy-MM-dd")),
            new SqlParameter("@KishoriName", txtKishoriName.Text),
            new SqlParameter("@DOB", Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd")),
            new SqlParameter("@SocialCategory", ddlCategory.SelectedValue),
            new SqlParameter("@contactKishori", ddlKishoricontact.SelectedValue),
            new SqlParameter("@FatherName", txtFatherName.Text),
            new SqlParameter("@MotherName", txtMotherName.Text),
            new SqlParameter("@ParentsMobileNo", txtMobile.Text),
            new SqlParameter("@ParentsWhatsAppNo", txtxAlternate.Text),
            new SqlParameter("@WhatsAppphoneavailable", ddlSmart.SelectedValue),
            new SqlParameter("@KishoriMobileNumber", txtKishoriMobileNumber.Text),
            new SqlParameter("@LastClass", ddlLastClass.SelectedValue),
            new SqlParameter("@LastClassCompletionYear", ddlCompletionYr.SelectedValue),

            new SqlParameter("@ReasonforDropout", ddlReason.SelectedValue),
            new SqlParameter("@OtherInfo", txtOther.Text),
            new SqlParameter("@Gender", ddlGender.SelectedValue),
            new SqlParameter("@AvailabilityofDocument", AvailabilityofDocument),
            new SqlParameter("@Image", ImagePath),
            new SqlParameter("@ExamType", ddlExamType.SelectedValue),
            new SqlParameter("@Subject", Subject),
            new SqlParameter("@DOBregistration",Convert.ToDateTime(txtDOBReg.Text).ToString("yyyy-MM-dd")),
            new SqlParameter("@RegistrationID", txtRegistration.Text),
                new SqlParameter("@Class", ddlClass.SelectedValue),
                    new SqlParameter("@createby", Session["username"].ToString()),
                new SqlParameter("@flag", flag)





        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateChildRegistrationPragati", cmdParameters);
    }


    private void RefreshControl()
    {


        txtPrerakName.Text = "";

        txtPrerakCode.Text = "";
        txtRegistrationDate.Text = "";
        txtKishoriName.Text = "";
        txtDate.Text = "";
        ddlCategory.SelectedIndex = 0;
        ddlKishoricontact.SelectedIndex = 0;
        txtFatherName.Text = "";

        txtMotherName.Text = "";
        txtMobile.Text = "";
        txtxAlternate.Text = "";
        ddlSmart.SelectedIndex = 0;
        ddlLastClass.SelectedIndex = 0;
        ddlReason.SelectedIndex = 0;
        txtOther.Text = "";
        txtDOBReg.Text = "";
        txtRegistration.Text = "";
        txtKishoriMobileNumber.Text = "";
        ddlCompletionYr.SelectedValue = "0";
        txt_pbname.Text = "";
        txt_pbnameNew.Text = "";

        foreach (ListItem item in CBL_bookformat.Items)
        {

            item.Selected = false;
        }

        foreach (ListItem item in CBL_bookformatNew.Items)
        {
            item.Selected = false;
        }
        ddlClass.SelectedIndex = 0;


        ViewState["ImagePath"] = "";
        ViewState["Save"] = "Save";

        ViewState["TMCode"] = null;

    }
    protected void btn_sapark(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
    }
    protected void btn_sapark1(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
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
        if (ddlCampID.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Camp')</script>", false);
            return;
        }
        if (ddlType.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Type')</script>", false);
            return;
        }
        Gvattendance.DataSource = null;
        Gvattendance.DataBind();
        TxtAttendanceDate.Text = "";
        ddlsession.SelectedIndex = -1;
        ddlattendancePrarak.SelectedIndex = -1;
        pnlMain.Enabled = true;
        btnsave.Visible = true;
        RefreshControl();

        // Resone.Visible = false;
        // rdate.Visible = false;

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
            string TBCode = GVMain.DataKeys[iIndex]["UniqueChildRCode"].ToString();
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
        string con = "where UniqueChildRCode='" + pSchoolCOde + "'";
        SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@con", con),

            };
        dtmstM = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadChildRegistrationPragati]", cmdParameters);


        if (dtmstM.Rows.Count > 0)
        {

            //#region School


            ////if (Session["user_level"].ToString() == "1")
            ////{
            //    if (dtmstM.Rows[0]["Status"].ToString() == "1")
            //    {
            //        btnsave.Enabled = true;
            //        btnDelete.Enabled = true;
            //    }
            //    else
            //    {
            //        btnsave.Enabled = false;
            //        btnDelete.Enabled = false;
            //    }
            //    DataTable dt = objMain.LoadData("SELECT  * from tblAttendance where TBID='" + pSchoolCOde + "'");
            //    if (dt.Rows.Count > 0)
            //    {
            //        txtday.Text = dt.Rows.Count.ToString();
            //    }
            //    else
            //    {
            //        txtday.Text = "";
            //    }
            ////ddlState.SelectedValue = dtmstM.Rows[0]["StateCode"].ToString();
            ////FillCBDist();
            ////ddlDistrict.SelectedValue = dtmstM.Rows[0]["DistrictCode"].ToString().Trim();
            ////FillCBBock();
            ////ddlBlock.SelectedValue = dtmstM.Rows[0]["BlockCode"].ToString();
            ////FillCBCluster();
            ////ddlPanchayat.SelectedValue = dtmstM.Rows[0]["PanchayatCode"].ToString().Trim();
            ////FillCVillage();
            ////ddlVillage.SelectedValue = dtmstM.Rows[0]["VillageCode"].ToString().Trim();

            ViewState["TMCode"] = pSchoolCOde;
            ddlPrerakName.SelectedValue = Convert.ToString(dtmstM.Rows[0]["PrerakName"]);
            txtPrerakCode.Text = dtmstM.Rows[0]["PrerakCode"].ToString().Trim();
            DateTime Registrationdate = Convert.ToDateTime(dtmstM.Rows[0]["Registrationdate"].ToString());
            txtRegistrationDate.Text = Registrationdate.ToString("dd/MM/yyy");
            txtKishoriName.Text = dtmstM.Rows[0]["KishoriName"].ToString().Trim();
            DateTime DOB = Convert.ToDateTime(dtmstM.Rows[0]["DOB"].ToString());
            txtDate.Text = DOB.ToString("dd/MM/yyy");
            ddlCategory.SelectedValue = dtmstM.Rows[0]["SocialCategory"].ToString();
            ddlKishoricontact.SelectedValue = dtmstM.Rows[0]["contactKishori"].ToString();
            txtFatherName.Text = dtmstM.Rows[0]["FatherName"].ToString().Trim();
            txtMotherName.Text = dtmstM.Rows[0]["MotherName"].ToString().Trim();
            txtMobile.Text = dtmstM.Rows[0]["ParentsMobileNo"].ToString().Trim();
            txtxAlternate.Text = dtmstM.Rows[0]["ParentsWhatsAppNo"].ToString().Trim();
            ddlSmart.SelectedValue = dtmstM.Rows[0]["WhatsAppphoneavailable"].ToString();
            txtKishoriMobileNumber.Text = dtmstM.Rows[0]["KishoriMobileNumber"].ToString().Trim();
            ddlLastClass.SelectedValue = dtmstM.Rows[0]["LastClass"].ToString();

            ddlCompletionYr.SelectedValue = dtmstM.Rows[0]["LastClassCompletionYear"].ToString().Trim();
            ddlReason.SelectedValue = dtmstM.Rows[0]["ReasonforDropout"].ToString();
            if (Convert.ToInt32(ddlReason.SelectedValue) == 9)
            {
                txtOther.Enabled = true;
            }
            else
            {
                txtOther.Enabled = false;
            }
            txtOther.Text = dtmstM.Rows[0]["OtherInfo"].ToString().Trim();

            ddlGender.SelectedValue = dtmstM.Rows[0]["Gender"].ToString();
            ddlExamType.SelectedValue = dtmstM.Rows[0]["ExamType"].ToString();
            string cmeeting = dtmstM.Rows[0]["ExamType"].ToString();
            DateTime DOBregistration = Convert.ToDateTime(dtmstM.Rows[0]["DOBregistration"].ToString());
            txtDOBReg.Text = DOBregistration.ToString("dd/MM/yyy");
            txtRegistration.Text = dtmstM.Rows[0]["RegistrationID"].ToString().Trim();
            ddlClass.SelectedValue = dtmstM.Rows[0]["Class"].ToString();

            DateTime CreateDate = Convert.ToDateTime(dtmstM.Rows[0]["CreateDate"].ToString());
            DateTime Todate = DateTime.Today;
            if (CreateDate.ToString("dd/MM/yyy") != Todate.ToString("dd/MM/yyy"))
            {
                btnsave.Visible = false;
            }
            else
            {
                btnsave.Visible = true;
            }
            if (dtmstM.Rows[0]["Image"].ToString() != "")
            {
                //string sFileDir = Server.MapPath(Comman.GetImagePath("ImgPage") + dtmstM.Rows[0]["ImagePath"].ToString().Trim() + "");
                //string sFileDir = Request.PhysicalApplicationPath + "images\\";
                string imagename = dtmstM.Rows[0]["Image"].ToString().Trim();
                ViewState["ImagePath"] = imagename;
                imgMKS.ImageUrl = ResolveUrl("~/DataBackup/" + imagename);
            }
            else
            {
                ViewState["ImagePath"] = "";

                imgMKS.ImageUrl = null;
            }

            string[] meeting = cmeeting.Split(',');
            string TextMeeeting = "";
            foreach (string s in meeting)
            {
                foreach (ListItem item in CBL_bookformat.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        TextMeeeting += item.Text + ",";
                    }
                }
            }
            if (TextMeeeting.Length > 0)
            {
                TextMeeeting = TextMeeeting.Substring(0, TextMeeeting.LastIndexOf(","));
                txt_pbname.Text = TextMeeeting;

            }



            string cmeeting1 = dtmstM.Rows[0]["Subject"].ToString();
            string[] meeting1 = cmeeting1.Split(',');
            string TextMeeeting1 = "";
            foreach (string s in meeting1)
            {
                foreach (ListItem item in CBL_bookformatNew.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        TextMeeeting1 += item.Text + ",";
                    }
                }
            }
            if (TextMeeeting1.Length > 0)
            {
                TextMeeeting1 = TextMeeeting1.Substring(0, TextMeeeting1.LastIndexOf(","));
                txt_pbnameNew.Text = TextMeeeting1;

            }

            //ddltbRecruited.SelectedValue = dtmstM.Rows[0]["TbRecruited"].ToString();
            //ddlSmart.SelectedValue = dtmstM.Rows[0]["IsSmartPhone"].ToString();

            //txtxAlternate.Text = dtmstM.Rows[0]["AlternetPhoneNo"].ToString().Trim();
            //ddloccu.SelectedValue = dtmstM.Rows[0]["FamilyOccupation"].ToString();
            //ddlWorkingStatus.SelectedValue = dtmstM.Rows[0]["WorkingStatus"].ToString();
            //EmpID.Visible = false;
            //if (ddlWorkingStatus.SelectedIndex > 0)
            //{
            //    if (Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 2)
            //    {
            //        ddlStatusReasone.SelectedValue = dtmstM.Rows[0]["DropOutReason"].ToString();
            //        ddlStatusReasone_SelectedIndexChanged(ddlBlock, null);
            //        DateTime DateDrop = Convert.ToDateTime(dtmstM.Rows[0]["DropoutDate"].ToString());
            //        txtDropDate.Text = DateDrop.ToString("dd/MM/yyy");

            //        Resone.Visible = true;
            //        rdate.Visible = true;
            //    }
            //    else
            //    {
            //        Resone.Visible = false;
            //        rdate.Visible = false;
            //        txtDropDate.Text = "";
            //        txtEmployeeID.Text = "";
            //        EmpID.Visible = false;
            //        ddlStatusReasone.SelectedIndex = 0;
            //    }
            //}
            //else
            //{
            //    Resone.Visible = false;
            //    rdate.Visible = false;
            //}
            //txtEmployeeID.Text = dtmstM.Rows[0]["EmpID"].ToString().Trim();
            //ddlEducation.SelectedValue = dtmstM.Rows[0]["EducationLevel"].ToString();
            //ddlCategory.SelectedValue = dtmstM.Rows[0]["SocialCategory"].ToString();
            //ddlReason.SelectedValue = dtmstM.Rows[0]["ReasonForTBChoice"].ToString();

            //ddlSours.SelectedValue = dtmstM.Rows[0]["RecruitmentReferalInfo"].ToString();
            //if (Convert.ToBoolean(dtmstM.Rows[0]["PriorWorkExperience"].ToString()) == true)
            //{
            //    ddlWorkEx.SelectedIndex = 1;
            //}
            //else
            //{
            //    ddlWorkEx.SelectedIndex = 2;
            //}
            //txtFatherName.Text = dtmstM.Rows[0]["FatherName"].ToString().Trim();
            //txtMotherName.Text = dtmstM.Rows[0]["MotherName"].ToString().Trim();
            //txtContact.Text = dtmstM.Rows[0]["Contact"].ToString().Trim();
            //txtDuartion.Text = "";
            //txtMonth.Text = ""; 
            //if (dtmstM.Rows[0]["TotalPriorWorkExperience"].ToString() == "0")
            //{
            //}
            //else
            //{
            //    txtDuartion.Text = dtmstM.Rows[0]["TotalPriorWorkExperience"].ToString().Trim();
            //}
            //if (dtmstM.Rows[0]["PriorWorkYearMonth"].ToString() == "0")
            //{
            //}
            //else
            //{
            //    txtMonth.Text = dtmstM.Rows[0]["PriorWorkYearMonth"].ToString().Trim();
            //}

            //if (dtmstM.Rows[0]["DateofJoining"].ToString() != "")
            //{
            //    DateTime DateJoing = Convert.ToDateTime(dtmstM.Rows[0]["DateofJoining"].ToString());
            //    txtJoingDate.Text = DateJoing.ToString("dd/MM/yyy");
            //}
            //else
            //{
            //    txtJoingDate.Text = "";
            //}

            //ddlDob.SelectedValue = dtmstM.Rows[0]["DOBAvailable"].ToString();
            //txtExp.Text = dtmstM.Rows[0]["Expectation"].ToString().Trim();
            //txtAbv.Text = dtmstM.Rows[0]["Abvision"].ToString().Trim();
            //if (dtmstM.Rows[0]["ImagePath"].ToString() != "")
            //{
            //    //string sFileDir = Server.MapPath(Comman.GetImagePath("ImgPage") + dtmstM.Rows[0]["ImagePath"].ToString().Trim() + "");
            //    //string sFileDir = Request.PhysicalApplicationPath + "images\\";
            //    string imagename = dtmstM.Rows[0]["ImagePath"].ToString().Trim();
            //    ViewState["ImagePath"] = imagename;
            //    imgMKS.ImageUrl = ResolveUrl("~/DataBackup/" + imagename);
            //}
            //else
            //{
            //    ViewState["ImagePath"] = "";

            //    imgMKS.ImageUrl = null;
            //}
            //if (Convert.ToInt32(ddlDob.SelectedValue) == 1)
            //{
            //     DateTime dob= Convert.ToDateTime(dtmstM.Rows[0]["DOB"].ToString());
            //     txtDate.Text = dob.ToString("dd/MM/yyy");
            //    lblDob.Text = "DOB";
            //    lblAge.Enabled = false;
            //    txtAge.Enabled = false;
            //    txtAge.Text = "";
            //    txtDate.Enabled = true;
            //}
            //else
            //{
            //    lblDob.Text = "As On";

            //    txtAge.Text = dtmstM.Rows[0]["AgeAson"].ToString();
            //    DateTime dob= Convert.ToDateTime(dtmstM.Rows[0]["AsOnDate"].ToString());
            //     txtDate.Text =dob.ToString("dd/MM/yyy");
            //     lblAge.Enabled = true;
            //    txtAge.Enabled = true;
            //    txtDate.Enabled = false;
            //}
            //#endregion
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

        GVMainBind();
        Gvattendance.DataSource = null;
        Gvattendance.DataBind();
        TxtAttendanceDate.Text = "";
        ddlsession.SelectedIndex = -1;
        ddlattendancePrarak.SelectedIndex = -1;
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
                string strQry = " Select top 1 isnull(max(Serial),0) as Serial from mstTeamBalika inner join mst5Village on  mst5Village.VillageCode=mstTeamBalika.VillageCode 	or  mst5Village.refVillage16=mstTeamBalika.VillageCode	or  mst5Village.refVillage17=mstTeamBalika.VillageCode	or  mst5Village.refVillage18=mstTeamBalika.VillageCode	or  mst5Village.refVillage19=mstTeamBalika.VillageCode	or  mst5Village.refVillage20=mstTeamBalika.VillageCode	 	or  mst5Village.refVillage21=mstTeamBalika.VillageCode  or  mst5Village.refVillage22=mstTeamBalika.VillageCode or  mst5Village.refVillage23=mstTeamBalika.VillageCode		 inner join mst3Block on  mst3Block.BlockCode=mst5Village.BlockCode where mst5Village.DistrictCode='" + ddlDistrict.SelectedValue + "'   ";
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


}