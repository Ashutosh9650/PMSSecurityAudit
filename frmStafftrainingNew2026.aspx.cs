using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class frmStafftrainingNew2026 : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    ArrayList arraylist1 = new ArrayList();
    ArrayList arraylist2 = new ArrayList();
    string conditions = "";
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {

                FillTrainingType();
                Filllearning();
                ViewState["Save"] = "Save";
                btnDelete.Visible = false;
                btnsave.Enabled = true;
                //pnlGrd.Visible = false;
                LoadYear();
                //FillCBState();
                AlllStateCode();
                LoadUser();
                ValdateUserLavel();
                fillrole();
                FillScheduling();
                Session["dtEntryDoneBY"] = null;

                Session["dtAttendation2026"] = null;

                //divDist.Visible = true;
                //divBLock.Visible = true;
                //ddlPmsType.SelectedIndex = 1;
                ViewState["TBCode"] = "";
                Session["dtSP"] = "";
                //GVMainBind();
            }
            else
            {
                Response.Redirect("Login.aspx", false);

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
    protected void ddlPMS_Spine_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (Convert.ToInt32(ddlPMS_Spine.SelectedValue) == 1)
        //{
        //    fillrole1();
        //    divDistPS.Visible = true;
        //    divState.Visible = true;
        //    MpexdrDistrict.Show();
        //}
        //if (Convert.ToInt32(ddlPMS_Spine.SelectedValue) == 2)
        //{
        //    LoadDataDist1();
        //    divDistPS.Visible = false;
        //    divState.Visible = false;
        //    MpexdrDistrict.Show();
        //}
    }
    protected void ddlPmsType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (Convert.ToInt32(ddlPmsType.SelectedValue) == 1)
        //{
        //    fillrole();
        //    divDist.Visible = true;
        //    divBLock.Visible = true;
        //}
        //if (Convert.ToInt32(ddlPmsType.SelectedValue) == 2)
        //{
        //    LoadDataDist();
        //    divDist.Visible = false;
        //    divBLock.Visible = false;
        //}

    }
    public void LoadDataDist()
    {
        SqlParameter[] par1 = new SqlParameter[]
                {

                      new SqlParameter("@Flag", "1" ),


                };
        DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadTempUserMasterDistrict", par1);

        DataTable dtDist = ds.Tables[0];

        DataTable dtDesignation = ds.Tables[1];

        if (dtDesignation.Rows.Count > 0)
        {

            //  objComman.BindDLLDatatableV("mst2District", dtDesignation, "Designation as Designation,dbo.TitleCase(upper(Designation)) as Designation", conditions, "Designation", "asc", ddllevel, "Designation", "Designation", "--Select--");


        }

    }
    public void LoadDataDist1()
    {
        //SqlParameter[] par1 = new SqlParameter[]
        //        {

        //              new SqlParameter("@Flag", "1" ),


        //        };
        //DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadTempUserMasterDistrict", par1);

        //DataTable dtDist = ds.Tables[0];

        //DataTable dtDesignation = ds.Tables[1];

        //if (dtDesignation.Rows.Count > 0)
        //{

        //    objComman.BindDLLDatatableV("mst2District", dtDesignation, "Designation as Designation,dbo.TitleCase(upper(Designation)) as Designation", conditions, "Designation", "asc", ddlRoleNew, "Designation", "Designation", "--Select--");


        //}

    }

    public void fillrole()
    {

        //string cond = "Role_Level not in(1)";

        //DataTable dtrole = Select_All_Data("mstuserrole", "*", cond, "Role_id", "");
        //if (dtrole.Rows.Count > 0)
        //{
        //    ddllevel.DataSource = dtrole;
        //    ddllevel.DataTextField = "Role";
        //    ddllevel.DataValueField = "Role_Level";
        //    ddllevel.DataBind();
        //    ddllevel.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        //}

    }
    public void fillrole1()
    {

        //string cond = "Role_Level not in(1)";

        //DataTable dtrole = Select_All_Data("mstuserrole", "*", cond, "Role_id", "");
        //if (dtrole.Rows.Count > 0)
        //{
        //    ddlRoleNew.DataSource = dtrole;
        //    ddlRoleNew.DataTextField = "Role";
        //    ddlRoleNew.DataValueField = "Role_Level";
        //    ddlRoleNew.DataBind();
        //    ddlRoleNew.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        //}

    }
    public DataTable Select_All_Data(string TableName, string TFieldName, string Condition, string OrderbyCondition, string Sortcondition)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            string WConditions = Condition.Length > 0 ? " where " + Condition : "";
            string OrderbyvalueMem = OrderbyCondition.Length > 0 ? " order by " + OrderbyCondition + "  " : "";
            string sortbycondi = Sortcondition.Length > 0 ? "" + Sortcondition : "";
            string FieldName = TFieldName.Length > 0 ? TFieldName : "";
            SqlParameter[] paramv = new SqlParameter[]
                    {
                            new SqlParameter("@TableName",TableName),
                            new SqlParameter("@Condition",WConditions),
                            new SqlParameter("@OrderbyvalueMem",OrderbyvalueMem),
                            new SqlParameter("@sortbycondi",sortbycondi),
                            new SqlParameter("@FieldName",FieldName),

                    };

            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Select_AllTableData", paramv);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch
        {
            //string mmsg = ex.Message; showMessages(mmsg);
            //showMessages("(SelectAllData)  " + mmsg);
        }
        return dtcombo;
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            txtTrainename.Enabled = false;
            // lblTrainename.Visible = false;
            txtEmail.Enabled = false;
            txtContact.Enabled = false;
            r1.Visible = false;
            r2.Visible = false;
            r3.Visible = false;
            txtTrainename.Enabled = false;
            txtEmail.Enabled = false;
            txtContact.Enabled = false;

            txtTrainename.Text = "";

            txtEmail.Text = "";
            txtContact.Text = "";
            GvEntry.DataSource = null;
            GvEntry.DataBind();
            GvEntryNew.DataSource = null;
            GvEntryNew.DataBind();
            btnAddTrain.Visible = true;
            Session["dtEntryDoneBY"] = null;
            //lblEmail.Visible = false;
            //lblContact.Visible = false;
            //lnkUser.Visible = true;
            //Fieldset1.Visible = false;
            hh1.Visible = true;
        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            btnAddTrain.Visible = false;
            txtTrainename.Enabled = true;
            txtEmail.Enabled = true;
            txtContact.Enabled = true;
            hh1.Visible = false;
            txtTrainename.Text = "";

            txtEmail.Text = "";
            txtContact.Text = "";
            txtTrainename.Enabled = true;
            txtEmail.Enabled = true;
            txtContact.Enabled = true;
            r1.Visible = true;
            r2.Visible = true;
            r3.Visible = true;

            //lblTrainename.Visible = true;
            //lblEmail.Visible = true;
            //lblContact.Visible = true;
            //lnkUser.Visible = false;
            //Fieldset1.Visible = true;
        }
    }
    protected void ddlSchedue_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSchedue.SelectedIndex > 0)
        {

            //string strQry = "SELECT StateCode, case Inducation when 0 then Other else sOutcomeName end as Other ,Location,isnull(TrainingMode,0) as TrainingMode  ,   [tblStaffScheduling].[LockRecord] , [tblStaffScheduling].[DistrictCode]   ,[FromDate]  ,[ToDate]  ,[Inducation][Outcome],mstOutcomeSpecific.OutcomeID ,[TrainingType]       ,[UserID]  ,[ScheduleID]  FROM [tblStaffScheduling]   left join mstOutcomeSpecific on mstOutcomeSpecific.sOutcomeID=[Inducation] where  [ScheduleID]=" + ddlSchedue.SelectedValue + "  ";

            Session["dtAttendation2026"] = null;

            DataTable dtScheduling = StaffEntryQuery(ddlSchedue.SelectedValue, "", "", "1");

            if (dtScheduling.Rows.Count > 0)
            {

                if (Convert.ToString(Session["username"]) == "PMSAdmin" || Convert.ToString(Session["username"]) == "EGE7557" || Convert.ToString(Session["username"]) == "SuperAdmin")
                {
                    btnsave.Enabled = true;
                    btnDelete.Enabled = true;
                }
                else
                {
                    if (dtScheduling.Rows[0]["LockRecord"].ToString() == "0")
                    {
                        btnsave.Enabled = true;
                    }
                    else
                    {
                        TimeSpan D = (DateTime.Now.Date - Convert.ToDateTime(dtScheduling.Rows[0]["ToDate"]));
                        int Days = D.Days;

                        if (Days <= 7 && Days >= 0)
                        {
                            btnsave.Enabled = true;
                            btnDelete.Enabled = true;
                            //pnlMain1.Enabled = true;
                        }
                        else
                        {
                            //  pnlMain1.Enabled = false;

                            btnDelete.Enabled = false;
                            //txtFromDate.Text = "";
                            txtToDate.Text = "";
                            //ddlLearning.SelectedIndex = 0;
                            ddlTraining.SelectedIndex = 0;
                            btnsave.Enabled = true;
                            //lbltr.Text = "";
                            //gvRightSearch.DataSource = null;
                            //gvRightSearch.DataBind();
                            //  return;
                        }
                    }
                }

                ddlState.SelectedValue = dtScheduling.Rows[0]["StateCode"].ToString();
                ddlState_SelectedIndexChanged(ddlState, null);
                ddlDistrictSearch.SelectedValue = dtScheduling.Rows[0]["DistrictCode"].ToString();

                ViewState["DIst"] = dtScheduling.Rows[0]["DistrictCode"].ToString();
                if (dtScheduling.Rows[0]["DistrictCode"].ToString() != "0")
                {
                    ddlTraingOutcome.SelectedValue = dtScheduling.Rows[0]["OutcomeID"].ToString();
                }
                LoadOutComeSpicify();
                ddlLearning.SelectedValue = dtScheduling.Rows[0]["Outcome"].ToString();

                ddlTraining.SelectedValue = dtScheduling.Rows[0]["TrainingType"].ToString();
                ddlTraingMode.SelectedValue = dtScheduling.Rows[0]["TrainingMode"].ToString();
                //lbltr.Text = dtScheduling.Rows[0]["Other"].ToString();
                DateTime StartDate = Convert.ToDateTime(dtScheduling.Rows[0]["FromDate"].ToString());
                CalendarExtender1.StartDate = Convert.ToDateTime(StartDate);
                txtLocation.Text = dtScheduling.Rows[0]["Location"].ToString();
                txtFromDate.Text = StartDate.ToString("dd/MM/yyyy");

                DateTime EnDate = Convert.ToDateTime(dtScheduling.Rows[0]["ToDate"].ToString());
                CalendarExtender2.StartDate = Convert.ToDateTime(EnDate);
                txtToDate.Text = EnDate.ToString("dd/MM/yyyy");

                txtToDate.Enabled = false;
                //txtFromDate.Enabled = false;
                //pnlMain1.Enabled = true;
                ViewState["Save"] = "Save";
                ViewState["SchedueID"] = ddlSchedue.SelectedValue;
                ddlDistrictSearch.Enabled = false;
                ddlState.Enabled = false;
                Butteon2.Enabled = true;


                string fdate = txtFromDate.Text;
                string[] b = fdate.Split('/');
                string FromDate = b[2] + '-' + b[1] + '-' + b[0];

                string Tdate = txtToDate.Text;
                string[] T = Tdate.Split('/');
                string Todate = T[2] + '-' + T[1] + '-' + T[0];

                DataTable dtTb = objMain.LoadData(" SELECT  day(dateadd(d,number-1,'" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd") + "')) as TbDay ,  CONVERT(varchar,dateadd(d,number-1,'" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd") + "'),103) As TBDate from Numbers WHERE Number<=DATEDIFF(day,('" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd") + "'),CONVERT(datetime,'" + Convert.ToDateTime(Todate).ToString("yyyy-MM-dd") + "')+1) ");

                //DataTable DateSearch = objMain.LoadData(" SELECT  day(dateadd(d,number-1,'" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd") + "')) as TbDay ,  CONVERT(varchar,dateadd(d,number-1,'" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd") + "'),103) As TBDate from Numbers WHERE Number<=DATEDIFF(day,('" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd") + "'),CONVERT(datetime,'" + Convert.ToDateTime(Todate).ToString("yyyy-MM-dd") + "')+1) ");

                Session["DateSearch"] = dtTb;

                LoadMainSP();

                //DataTable dtAttendation = StaffEntryQuery(ddlSchedue.SelectedValue, "", "", "2");
                //if (dtAttendation.Rows.Count>0)
                //{
                //    Session["dtAttendation"] = dtAttendation;
                //    gvRightSearch.DataSource = dtAttendation;
                //    gvRightSearch.DataBind();

                //    UpdateGridBlank();
                //    dtAttendation = null;
                //    dtAttendation = Session["dtAttendation"] as DataTable;

                //    gvRightSearch.DataSource = dtAttendation;
                //    gvRightSearch.DataBind();

                //}
                //else
                //{
                //    Session["dtAttendation"] = null;
                //    gvRightSearch.DataSource = null;
                //    gvRightSearch.DataBind();

                //}
                //FillScheduling();
                //RefreshControl();
                Session["dtEntryDoneBY"] = null;


                //pnltb.Visible = true;

            }
        }
        else
        {
            ddlTraingOutcome.SelectedIndex = 0;


            //ddlLearning.SelectedIndex = 0;

            ddlTraining.SelectedIndex = 0;
            ddlTraingMode.SelectedIndex = 0;
            //lbltr.Text = dtScheduling.Rows[0]["Other"].ToString();

            txtLocation.Text = "";
            txtFromDate.Text = "";

            txtToDate.Text = "";
            Butteon2.Enabled = false;

        }
    }

    public void LoadMainSP()
    {
        DataTable dtAttendation = StaffEntryQuery(ddlSchedue.SelectedValue);
        if (dtAttendation.Rows.Count > 0)
        {
            Session["dtSP"] = dtAttendation;
            gvRightSearch.DataSource = dtAttendation;
            gvRightSearch.DataBind();

        }
        else
        {
            Session["dtSP"] = null;
            gvRightSearch.DataSource = null;
            gvRightSearch.DataBind();

        }
    }

    public DataTable StaffEntryQuery(string Fliter)
    {
        DataTable dt = null;
        try
        {

            SqlParameter[] parm = new SqlParameter[]
               {
              new SqlParameter("@ScheduleID",  Fliter),

               };
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadStaffShulEntry", parm);

        }
        catch
        {

        }
        return dt;
    }
    public DataTable StaffEntryQueryEdit(string Fliter)
    {
        DataTable dt = null;
        try
        {

            SqlParameter[] parm = new SqlParameter[]
               {
              new SqlParameter("@ScheduleID",  Fliter),

               };
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadStaffShulEntryEdit", parm);

        }
        catch
        {

        }
        return dt;
    }
    public DataTable StaffEntryQuery(string Fliter, string Fliter1, string Fliter2, string Flag)
    {
        DataTable dt = null;
        try
        {

            SqlParameter[] parm = new SqlParameter[]
               {
              new SqlParameter("@Fliter",  Fliter),
               new SqlParameter("@Fliter1",  Fliter1),
                new SqlParameter("@Fliter2",  Fliter2),
                 new SqlParameter("@Flag",  Flag),
               };
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptEntryQuery2026", parm);

        }
        catch
        {

        }
        return dt;
    }
    public void LoadOutComeSpicify()
    {
        string conditions = " ";

        objComman.BindDLL("mstOutcomeSpecific", "sOutcomeID,sOutcomeName ", "OutcomeID=" + ddlTraingOutcome.SelectedValue + " and ActiveStatus=1", "sOutcomeID", "asc", ddlLearning, "sOutcomeName", "sOutcomeID", "--Select--");




    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            ddlState.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlState, null);

            //ddlBlock.Items.Clear();


        }
        else
        {
            ddlState.SelectedIndex = 0;

            //ddlBlock.Items.Clear();


        }

        Locking();
    }
    public void ValdateUserLavel()
    {

        string strQry = "";
        string Cond = "Module='Staff Training' ";
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

            btnDelete.Enabled = true;
        }
        else
        {

            btnDelete.Enabled = false;
        }

        if (vADD == true)
        {
            btnAdd.Enabled = true;
            btnsave.Enabled = true;

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

        if (vVerify == true || vADD == true)
        {
            btnsave.Enabled = true;

        }
        else
        {
            btnsave.Enabled = false;

        }
    }

    public void Locking()
    {
        if (ddlYear.SelectedIndex > 0)
        {
            btnsave.Enabled = true;
            btnAdd.Enabled = true;
            if (Session["FinYear"].ToString() != ddlYear.SelectedItem.Text)
            {

                string strQry;
                strQry = "Select * from mstModuleLocking  where [FromName]='TBTraining' and DistrictCode='" + ddlDistrictSearch.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "'";


                DataTable dtModel = objMain.LoadData(strQry);
                if (dtModel.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtModel.Rows[0]["LockMonth"].ToString()) < DateTime.Today.Month)
                    {
                        btnsave.Enabled = false;
                        btnAdd.Enabled = false;

                    }

                }
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
        DateTime GivenDate = DateTime.Now;
        int GivenYear = GivenDate.Year;
        int m = GivenDate.Month;

        DataTable dt = null;
        //ddlYear.Items.Add("--Select--","0");
        int y = GivenDate.Year;


        DateTime GivenDate1 = DateTime.Now;
        int GivenYear1 = GivenDate1.Year;
        DataTable dtYear = CreateDataTable();
        DataRow dr;
        if (ddlYear.SelectedIndex < 0)
        {

            string mYear1 = GivenYear1.ToString();
            for (int j = 0; j < 1; j++)
            {
                if (m > 3)
                {
                    dr = dtYear.NewRow();
                    dr["Type"] = GivenYear.ToString() + "-" + Convert.ToString((GivenYear + 1));
                    dr["ID"] = y;
                    dtYear.Rows.Add(dr);
                    dr = dtYear.NewRow();
                    dr["Type"] = GivenYear - 1 + "-" + Convert.ToString((GivenYear - 1 + 1));
                    dr["ID"] = y - 1;
                    dtYear.Rows.Add(dr);
                    //get last  two digits (eg: 10 from 2010);
                    dr = dtYear.NewRow();
                    dr["Type"] = GivenYear - 2 + "-" + Convert.ToString((GivenYear - 2 + 1));
                    dr["ID"] = y - 1;
                    dtYear.Rows.Add(dr);
                }
                else
                {
                    dr = dtYear.NewRow();
                    dr["Type"] = Convert.ToString((y - 1)) + "-" + y.ToString();
                    //y = y - 1;
                    dr["ID"] = y - 1;

                    dtYear.Rows.Add(dr);

                    dr = dtYear.NewRow();
                    dr["Type"] = GivenYear - 2 + "-" + Convert.ToString((GivenYear - 2 + 1));
                    dr["ID"] = y - 1;
                    dtYear.Rows.Add(dr);
                }

            }

        }
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


    }
    public void LoadUser()
    {
        //objComman.BindDLL("MstUser", "UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName] ", conditions, "", "", myDropDownlistID, "UserName", "UserId", "Select");
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillCBDistSearch();
        FillCBDistSearchNew();
    }
    protected void ddlDist_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillCBBockSearch();
        //FillCBBock();
    }
    public void FillCBBockSearch()
    {
        conditions = "";

        //conditions = "DistrictCode ='" + ddlDist.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "' and  DividedBlock=1 ";

    }
    public void FillCBDistSearchNew()
    {
        //conditions = "";
        //    if (Session["user_level_Role"].ToString() == "1")
        //    {
        //          conditions = "StateCode ='" + ddlState.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "'";
        //         objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ",conditions, "DistrictName", "asc",ddlDistrictSearch, "DistrictName", "DistrictCode", "--Select--");

        //    }
        //  else if (Session["user_level_Role"].ToString() == "2")
        //  {

        //         conditions = "StateCode ='" + ddlState.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "'";
        //          objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ",conditions, "DistrictName", "asc",ddlDistrictSearch, "DistrictName", "DistrictCode", "--Select--");

        //  }
        //  else
        //  {

        //      conditions = "DistrictCode in(" + Session["DistrictCode"].ToString() + ") and Fyear='" + ddlYear.SelectedItem.Text + "'";
        //      objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ",conditions, "DistrictName", "asc",ddlDistrictSearch, "DistrictName", "DistrictCode", "--Select--");
        //      ddlDistrictSearch.SelectedIndex = 1;
        //  }
        if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "";
            conditions = " mst2District.StateCode ='" + ddlState.SelectedValue + "' and UserName='" + Session["username"].ToString() + "' ";
            string strQry1 = "       sELECT distinct mst2District.DistrictCode as DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist  ";
            strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions + "  and  Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName  ";
            DataTable dtDistrict = objMain.LoadData(strQry1);

            objComman.BindDLLDatatable("mst2District", dtDistrict, "DistrictCode, dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "Desc", ddlDistrictSearch, "DistrictName", "DistrictCode", "--Select--");

        }
        else
        {
            conditions = "";
            string conditions1 = "StateCode ='" + ddlState.SelectedValue + "' ";


            conditions = "StateCode ='" + ddlState.SelectedValue + "' and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";

            DataTable dtTb = objMain.LoadData(" SELECT DistrictCode,  dbo.TitleCase(upper(DistrictName)) as  DistrictName FROM [mst2District] where  " + conditions + "  union select  DistrictCode,  dbo.TitleCase(upper(DistrictName))  +' ('+ 'Spine' +')'  as  DistrictName from mstSpineDistrict  where  " + conditions1 + "   order by DistrictName ");



            objComman.BindDLLMasterTableVillage("mst2District", "DistrictCode,DistrictName", dtTb, conditions, "DistrictName", "asc", ddlDistrictSearch, "DistrictName", "DistrictCode", "Select");
        }

    }

    public void FillCBDistSearch()
    {
        //conditions = "";

        //conditions = "Fyear='" + ddlYear.SelectedItem.Text + "'";

        //DataTable dtTb = objMain.LoadData(" SELECT DistrictCode,  dbo.TitleCase(upper(DistrictName)) as DistrictName from mst2District where Fyear='" + ddlYear.SelectedItem.Text + "' order by  DistrictName desc");
        //ddlDist.DataSource = dtTb;
        //ddlDist.DataTextField = "DistrictName";
        //ddlDist.DataValueField = "DistrictCode";
        //ddlDist.DataBind();


    }

    public void FillCBStateSearch()
    {
        conditions = "";
        //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
        //DataTable dtTb = objMain.LoadData(" SELECT StateCode,  dbo.TitleCase(upper(StateName)) as StateName from mst1State order by  StateCode desc");
        //lstState.DataSource = dtTb;
        //lstState.DataTextField = "StateName";
        //lstState.DataValueField = "StateCode";
        //lstState.DataBind();


        //string cond = "Role_Level not in(1)";

        //DataTable dtrole = Select_All_Data("mstuserrole", "*", cond, "Role_id", "");
        //if (dtrole.Rows.Count > 0)
        //{
        //    ddlRoleNew.DataSource = dtrole;
        //    ddlRoleNew.DataTextField = "Role";
        //    ddlRoleNew.DataValueField = "Role_Level";
        //    ddlRoleNew.DataBind();
        //    ddlRoleNew.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        //}


    }
    protected void lnkUser_Click(object sender, EventArgs e)
    {

        //FillCBStateSearch();
        //DataTable dtUser = objMain.LoadData(" SELECT UserName as UserId, [FristName]+' ('+ UserName +')' as [UserName] from MstUser where UserName in( SELECT [StaffId]  FROM [tblStaffTrainer] where StaffUniqueCode ='" + Convert.ToString(ViewState["TBCode"]) + "' ) ");
        //if (dtUser.Rows.Count > 0)
        //{
        //    lstUser.DataSource = dtUser;
        //    lstUser.DataTextField = "UserName";
        //    lstUser.DataValueField = "UserId";
        //    lstUser.DataBind();
        //}
        //ddlType.SelectedIndex = 0;
        //txtSearchUser.Text = "";
        //MpexdrDistrict.Show();
    }

    public void FillCBState()
    {



        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            ///  objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            DataTable dtTb = objMain.LoadData(" SELECT StateCode,  dbo.TitleCase(upper(StateName)) as  StateName FROM [mst1State] union select  StateCode,  dbo.TitleCase(upper(StateName))  +' ('+ 'Spine' +')'  as  StateName  from [mstSpineState] order by Statecode  ");



            objComman.BindDLLMasterTableVillage("mst1State", "StateCode,StateName", dtTb, conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "Select");



        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //   objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");


            conditions = "UserName='" + Session["username"].ToString() + "' ";
            string strQry1 = "  SELECT distinct mst1State.StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode inner join mst1State on mst1State.StateCode=mst2District.StateCode where   " + conditions + " union select  StateCode,  dbo.TitleCase(upper(StateName))  +' ('+ 'Spine' +')'  as  StateName  from [mstSpineState]   order by StateName   ";
            DataTable dtTb = objMain.LoadData(strQry1);

            // DataTable dtTb = objMain.LoadData(" SELECT StateCode,  dbo.TitleCase(upper(StateName)) as  StateName FROM [mst1State] where " + conditions + " union select  StateCode,  dbo.TitleCase(upper(StateName))  +' ('+ 'Spine' +')'  StateName  from [mstSpineState] order by Statecode  ");



            objComman.BindDLLMasterTableVillage("mst1State", "StateCode,StateName", dtTb, conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "Select");




        }
        else
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            DataTable dtTb = objMain.LoadData(" SELECT StateCode,  dbo.TitleCase(upper(StateName)) as  StateName FROM [mst1State] where " + conditions + " union select  StateCode,  dbo.TitleCase(upper(StateName))  +' ('+ 'Spine' +')'  as  StateName  from [mstSpineState] order by Statecode  ");



            objComman.BindDLLMasterTableVillage("mst1State", "StateCode,StateName", dtTb, conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "Select");




        }



        //conditions = "";

        //DataTable dtTb = objMain.LoadData(" SELECT StateCode,  dbo.TitleCase(upper(StateName)) as  StateName FROM [mst1State] union select  StateCode,  dbo.TitleCase(upper(StateName))  +' ('+ 'Spine' +')'  as  StateName  from [mstSpineState] order by Statecode  ");



        //objComman.BindDLLMasterTableVillage("mst1State", "StateCode,StateName", dtTb, conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "Select");


        ddlState.SelectedIndex = 1;


        ddlState_SelectedIndexChanged(ddlState, null);

    }
    private void GVMainBind()
    {

        string textnew = "where mstSpineDistrict.StateCode='" + this.ddlState.SelectedValue.ToString() + "'  and tblStaffScheduling.Flag=2 and TrainingTypeFlag=1";
        string text = "where mst2District.StateCode='" + this.ddlState.SelectedValue.ToString() + "' and TrainingTypeFlag=1 and mst2District.Fyear='" + ddlYear.SelectedItem.Text + "'  and tblStaffScheduling.Flag=2 ";
        //if (this.ddlLearning.SelectedValue != null && this.ddlLearning.SelectedIndex > 0)
        //{
        //    text = text + "and Learningtype='" + this.ddlLearning.SelectedValue.ToString() + "'";
        //}
        //if (this.ddlTraining.SelectedValue != null && this.ddlTraining.SelectedIndex > 0)
        //{
        //    text = text + " and TrainingType='" + this.ddlTraining.SelectedValue.ToString() + "'";
        //}
        if (this.ddlDistrictSearch.SelectedValue != null && this.ddlDistrictSearch.SelectedIndex > 0)
        {
            text = text + "and [tblStaffScheduling].DistrictCode='" + this.ddlDistrictSearch.SelectedValue.ToString() + "' and tblStaffScheduling.Flag=2";
            textnew = textnew + "and [tblStaffScheduling].DistrictCode='" + this.ddlDistrictSearch.SelectedValue.ToString() + "'  and tblStaffScheduling.Flag=2";
        }
        DataTable dtTb = objMain.LoadData(" SELECT tblStaffScheduling.ScheduleID as UniqueCode, mst2District.DistrictName as DistrictName,convert(varchar(10),[FromDate], 121) as [FromDate], convert(varchar(10), todate, 121) as todate, mstOutcome.OutcomeName   FROM[tblStaffScheduling] left  join mst2District on mst2District.DistrictCode =[tblStaffScheduling].DistrictCode   left join mstOutcome on mstOutcome.OutcomeID = Outcome " + text + "  group by tblStaffScheduling.ScheduleID, DistrictName, FromDate, todate, mstOutcome.OutcomeName  union SELECT tblStaffScheduling.ScheduleID UniqueCode,mstSpineDistrict.DistrictName as DistrictName, convert(varchar(10),[FromDate], 121) as [FromDate],convert(varchar(10), todate, 121) as todate, mstOutcome.OutcomeName   FROM[tblStaffScheduling] inner join mstSpineDistrict on mstSpineDistrict.DistrictCode = tblStaffScheduling.DistrictCode   left join mstOutcome on mstOutcome.OutcomeID = Outcome " + textnew + " and FromDate >= '2026-04-01' group by tblStaffScheduling.ScheduleID, DistrictName, FromDate, todate, mstOutcome.OutcomeName");
        if (dtTb.Rows.Count > 0)
        {
            GVMain.DataSource = dtTb;
            ViewState["Serach"] = dtTb;
            GVMain.DataBind();
        }
        else
        {
            GVMain.DataSource = null;

            GVMain.DataBind();
        }
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


    //public void FillCBDist()
    //{
    //    conditions = "";


    //    conditions = "DistrictCode ='" + Session["DistrictCode"].ToString() + "'";


    //    objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");



    //}
    public void FillTrainingType()
    {
        conditions = "";
        objComman.BindDLL("mstTrainingType", "TrainingID,dbo.TitleCase(upper(TrainingName)) as TrainingName ", conditions, "TrainingName", "asc", ddlTraining, "TrainingName", "TrainingID", "--Select--");



    }

    public void FillScheduling()
    {
        conditions = "";
        if (Convert.ToString(Session["username"]) == "PMSAdmin" || Convert.ToString(Session["username"]) == "EGE7557" || Convert.ToString(Session["username"]) == "SuperAdmin")
        {
            objComman.BindDLL("[tblStaffScheduling] inner join mstOutcome on mstOutcome.OutcomeID=[Outcome]", "[ScheduleID] ,mstOutcome.OutcomeName +' ( '+ convert (varchar(10),[FromDate] ,121)  +' To '+ convert (varchar(10),[ToDate] ,121) +' )' as Schedule ", "   Flag=1 and SdeleteFlag=1 and TrainingTypeFlag =1 and  FromDate>'2026-04-01' ", "Schedule", "asc", ddlSchedue, "Schedule", "ScheduleID", "--Select--");

        }
        else
        {
            objComman.BindDLL("[tblStaffScheduling] inner join mstOutcome on mstOutcome.OutcomeID=[Outcome]", "[ScheduleID] ,mstOutcome.OutcomeName +' ( '+ convert (varchar(10),[FromDate] ,121)  +' To '+ convert (varchar(10),[ToDate] ,121) +' )' as Schedule ", " userID like '%" + Session["username"] + "%' and  Flag=1 and SdeleteFlag=1 and TrainingTypeFlag =1 and LockRecord=1 and FromDate>'2026-04-01' ", "Schedule", "asc", ddlSchedue, "Schedule", "ScheduleID", "--Select--");
        }
        ddlSchedue.SelectedIndex = 0;

    }

    public void Filllearning()
    {
        conditions = "";
        //  objComman.BindDLL("mstlearning", "learningID,dbo.TitleCase(upper(learningName)) as learningName ", conditions, "learningName", "asc", ddlLearning, "learningName", "learningID", "--Select--");

        objComman.BindDLL("mstOutcome", "OutcomeID,OutcomeName ", conditions, "OutcomeID", "asc", ddlTraingOutcome, "OutcomeName", "OutcomeID", "--Select--");


    }

    //public void FillCVillage()
    //{
    //    conditions = "";
    //    conditions = "DistrictCode ='" + ddlDist.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "'   ";
    //    objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--Select--");



    //}
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {

        //FillCBBock();
    }

    protected void btnSerach_Click(object sender, EventArgs e)
    {
        string str = "";
        string fdate = txtFromDate.Text;
        string[] b = fdate.Split('/');
        string FromDate = b[2] + '-' + b[1] + '-' + b[0];

        string Tdate = txtToDate.Text;
        string[] T = Tdate.Split('/');
        string Todate = T[2] + '-' + T[1] + '-' + T[0];

        DateTime d1 = Convert.ToDateTime(FromDate);
        DateTime d2 = Convert.ToDateTime(Todate);
        int month = Convert.ToInt32(T[1]) - Convert.ToInt32(b[1]);
        TimeSpan t = d2 - d1;

        double Days = Convert.ToDouble(t.TotalDays);

        if (Math.Sign(Days) < 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select less then or equal 7 Day')</script>", false);
            return;
        }
        if (Math.Round(Days) > 7)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select less then or equal 7 Days')</script>", false);
            return;
        }
        if (ddlDistrictSearch.SelectedValue != null && ddlDistrictSearch.SelectedIndex > 0)
        {
            str = "where  DistCode='" + ddlDistrictSearch.SelectedValue.ToString() + "'";
        }


        if (ddlDistrictSearch.SelectedValue != null && ddlDistrictSearch.SelectedIndex > 0)
        {
            str = "and   DistCode='" + ddlDistrictSearch.SelectedValue.ToString() + "'";
        }


        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            str = str + "and FromDate= '" + FromDate + "' and ToDate='" + Todate + "'";
        }
        if (ddlLearning.SelectedIndex > 0)
        {
            str = str + "and Learningtype='" + this.ddlLearning.SelectedValue.ToString() + "'";
        }
        if (Convert.ToString(ViewState["TBCode"]) != null)
        {
            str = str + " and UniqueCode not in (" + Convert.ToString(ViewState["TBCode"]) + ")";
        }

        DataTable dtcheck = objComman.LoadData("Select * from tblStaffTrainingSchedue " + str + "");
        if (dtcheck.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Training not allowed')</script>", false);

            return;
        }
        else
        {

            DataTable dtTb = objMain.LoadData(" SELECT  day(dateadd(d,number-1,'" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd") + "')) as TbDay ,  CONVERT(varchar,dateadd(d,number-1,'" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd") + "'),103) As TBDate from Numbers WHERE Number<=DATEDIFF(day,('" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd") + "'),CONVERT(datetime,'" + Convert.ToDateTime(Todate).ToString("yyyy-MM-dd") + "')+1) ");

            Session["DateSearch"] = dtTb;
            //  GVMainBindSearch();

            txtFromDate.Enabled = false;
            txtToDate.Enabled = false;
        }
        //GvDate.DataSource = dtTb;
        //GvDate.DataBind();
    }
    protected void btnNewSerach_Click(object sender, EventArgs e)
    {
        //RefreshControl();
        GVMainBind();
        tt.Attributes.Add("style", "height:345px");
        //pnlMain1.Enabled = false;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        //pnlMain1.Enabled = true;
        //ViewState["Save"] = "Save";
        //FillScheduling();
        //RefreshControl();

        //btnsave.Enabled = true;
        //pnltb.Visible = true;
        // Session["DateSearch"] = null;


    }
    private void RefreshControl()
    {
        ddlType.SelectedIndex = 0;
        txtTrainename.Text = "";
        txtEmail.Text = "";
        txtContact.Text = "";
        ddlLearning.SelectedIndex = 0;
        ddlLearning.SelectedIndex = 0;
        ddlType.SelectedIndex = 0;
        txtFromDate.Enabled = true;
        txtToDate.Enabled = true;
        txtDesc.Text = "";
        Session["DateSearch"] = null;

        Session["TB"] = null;

        ViewState["dtselect"] = null;
        ViewState["dtAttendent"] = null;
        ViewState["TBCode"] = null;
        ViewState["dtselected"] = null;
        //txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");


        //GVTR.DataSource = null;
        //GVTR.DataBind();


    }


    public DataTable CreateDataDate()
    {

        DataTable dtAttendent = new DataTable();


        dtAttendent.Columns.Add(new DataColumn("UniqueCode", System.Type.GetType("System.String")));
        dtAttendent.Columns.Add(new DataColumn("AttDate", System.Type.GetType("System.String")));


        ViewState["dtAttendent"] = dtAttendent;
        return dtAttendent;
    }
    public int CheckTrainig()
    {
        int indcount = 0;
        DataTable dtAttendent = null;
        //foreach (GridViewRow Itemst in gvSerach.Rows)
        //{
        //    if (((CheckBox)Itemst.FindControl("Chk_allCh1")).Checked || ((CheckBox)Itemst.FindControl("Chk_allCh2")).Checked || ((CheckBox)Itemst.FindControl("Chk_allCh3")).Checked || ((CheckBox)Itemst.FindControl("Chk_allCh4")).Checked || ((CheckBox)Itemst.FindControl("Chk_allCh5")).Checked || ((CheckBox)Itemst.FindControl("Chk_allCh6")).Checked || ((CheckBox)Itemst.FindControl("Chk_allCh7")).Checked)
        //    {
        //        indcount++;
        //    }
        //}

        DataTable dtselect = (DataTable)ViewState["dtselect"];
        DataTable dtselected = (DataTable)ViewState["dtselected"];
        if (indcount > 0)
        {



            if (dtselected == null && dtselect == null) { return 0; }
            if (dtselected == null) { dtselected = dtselect.Clone(); }
            if (dtselect == null) { dtselect = dtselected.Clone(); }
            int tmp = 0;
            DataRow dr;
            DataRow drAtt;
            //foreach (GridViewRow Itemst in gvSerach.Rows)
            //{
            //    if (((CheckBox)Itemst.FindControl("Chk_allCh1")).Checked || ((CheckBox)Itemst.FindControl("Chk_allCh2")).Checked || ((CheckBox)Itemst.FindControl("Chk_allCh3")).Checked || ((CheckBox)Itemst.FindControl("Chk_allCh4")).Checked || ((CheckBox)Itemst.FindControl("Chk_allCh5")).Checked || ((CheckBox)Itemst.FindControl("Chk_allCh6")).Checked || ((CheckBox)Itemst.FindControl("Chk_allCh7")).Checked)
            //    {
            //        //dtAttendent = (DataTable)ViewState["dtAttendent"];

            //        DataTable dt = Session["DateSearch"] as DataTable;
            //        int ind = Itemst.DataItemIndex;


            //        Int32 DayCount = 0;

            //        DataRow[] dr1 = dtselected.Select("UniqueCode='" + gvSerach.DataKeys[ind]["UniqueCode"].ToString() + "'");
            //        if (dr1.Length > 0)
            //        {
            //        }
            //        else
            //        {
            //            dr = dtselected.NewRow();
            //            if (((CheckBox)Itemst.FindControl("Chk_allCh1")).Checked)
            //            {
            //                DayCount += 1;

            //                dr["Day1"] = dt.Rows[0]["TBDate"].ToString();
            //            }
            //            if (((CheckBox)Itemst.FindControl("Chk_allCh2")).Checked)
            //            {
            //                DayCount += 1;
            //                if (dt.Rows.Count > 1)
            //                {
            //                    dr["Day2"] = dt.Rows[1]["TBDate"].ToString();
            //                }
            //            }
            //            if (((CheckBox)Itemst.FindControl("Chk_allCh3")).Checked)
            //            {
            //                DayCount += 1;
            //                if (dt.Rows.Count > 2)
            //                {
            //                    dr["Day3"] = dt.Rows[2]["TBDate"].ToString();
            //                }
            //            }
            //            if (((CheckBox)Itemst.FindControl("Chk_allCh4")).Checked)
            //            {
            //                DayCount += 1;
            //                if (dt.Rows.Count > 2)
            //                {
            //                    dr["Day4"] = dt.Rows[3]["TBDate"].ToString();
            //                }
            //            }
            //            if (((CheckBox)Itemst.FindControl("Chk_allCh5")).Checked)
            //            {
            //                DayCount += 1;
            //                if (dt.Rows.Count > 2)
            //                {
            //                    dr["Day5"] = dt.Rows[4]["TBDate"].ToString();
            //                }
            //            }
            //            if (((CheckBox)Itemst.FindControl("Chk_allCh6")).Checked)
            //            {
            //                DayCount += 1;
            //                if (dt.Rows.Count > 2)
            //                {
            //                    dr["Day6"] = dt.Rows[5]["TBDate"].ToString();
            //                }
            //            }
            //            if (((CheckBox)Itemst.FindControl("Chk_allCh7")).Checked)
            //            {
            //                DayCount += 1;
            //                if (dt.Rows.Count > 2)
            //                {
            //                    dr["Day7"] = dt.Rows[6]["TBDate"].ToString();
            //                }
            //            }

            //            dr["TBCode"] = gvSerach.DataKeys[ind]["TBCode"];
            //            dr["BlockName"] = gvSerach.DataKeys[ind]["BlockName"];
            //            dr["TBName"] = gvSerach.DataKeys[ind]["TBName"];
            //            dr["UniqueCode"] = gvSerach.DataKeys[ind]["UniqueCode"];
            //            dr["VillageName"] = gvSerach.DataKeys[ind]["VillageName"];
            //            dr["UserType"] = gvSerach.DataKeys[ind]["UserType"];
            //            dr["TotalDay"] = DayCount;
            //            dtselected.Rows.Add(dr);
            //            tmp++;
            //        }
            //    }
            //}
        }
        DataTable dtData = dtselected.Copy();
        string fdate = txtFromDate.Text;
        string[] b = fdate.Split('/');
        string FromDate = b[2] + '-' + b[1] + '-' + b[0];

        string Tdate = txtToDate.Text;
        string[] T = Tdate.Split('/');
        string Todate = T[2] + '-' + T[1] + '-' + T[0];

        DataTable dtDatach = objComman.LoadData(" select [Inducation] from  tblStaffScheduling where [ScheduleID]  ='" + Convert.ToString(ViewState["SchedueID"]) + "'  ");
        if (dtDatach.Rows.Count > 0)
        {
            if (dtDatach.Rows[0]["Inducation"].ToString() == "58" || dtDatach.Rows[0]["Inducation"].ToString() == "65")
            {
                return 1;
            }
        }


        for (int i = 0; i < dtData.Rows.Count; i++)
        {

            string Day1 = Convert.ToString(dtData.Rows[i]["Day1"]), Day2 = Convert.ToString(dtData.Rows[i]["Day2"]), Day3 = Convert.ToString(dtData.Rows[i]["Day3"]), Day4 = Convert.ToString(dtData.Rows[i]["Day4"]), Day5 = Convert.ToString(dtData.Rows[i]["Day5"]), Day6 = Convert.ToString(dtData.Rows[i]["Day6"]), Day7 = Convert.ToString(dtData.Rows[i]["Day7"]);
            DataTable dtData1 = objComman.LoadData("select * from tblStaffTrainingSchedueDetail inner join tblStaffTrainingSchedue on tblStaffTrainingSchedue.UniqueCode=TBUniqueCode inner join tblStaffScheduling on tblStaffScheduling.ScheduleID=tblStaffTrainingSchedue.SchedueID  where Inducation not in(58,65) and  TBID='" + Convert.ToString(dtData.Rows[i]["TBCode"]) + "'  and  tblStaffTrainingSchedue.UniqueCode not in('" + Convert.ToString(ViewState["TBCode"]) + "' ) ");
            if (dtData1.Rows.Count > 0)
            {
                if (Day1 != "")
                {
                    DataRow[] dr = dtData1.Select("ADate1='" + Day1 + "'  ");
                    if (dr.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day1 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr1 = dtData1.Select("ADate2='" + Day1 + "'  ");
                    if (dr1.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day1 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr2 = dtData1.Select("ADate3='" + Day1 + "'  ");
                    if (dr2.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day1 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr3 = dtData1.Select("ADate4='" + Day1 + "'  ");
                    if (dr3.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day1 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr4 = dtData1.Select("ADate5='" + Day1 + "'  ");
                    if (dr4.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day1 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr5 = dtData1.Select("ADate6='" + Day1 + "'  ");
                    if (dr5.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day1 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr6 = dtData1.Select("ADate7='" + Day1 + "'  ");
                    if (dr6.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day1 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                }
                if (Day2 != "")
                {

                    DataRow[] dr = dtData1.Select("ADate1='" + Day2 + "'  ");
                    if (dr.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day2 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr1 = dtData1.Select("ADate2='" + Day2 + "'  ");
                    if (dr1.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day2 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr2 = dtData1.Select("ADate3='" + Day2 + "'  ");
                    if (dr2.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day2 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr3 = dtData1.Select("ADate4='" + Day2 + "'  ");
                    if (dr3.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day2 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr4 = dtData1.Select("ADate5='" + Day2 + "'  ");
                    if (dr4.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day2 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr5 = dtData1.Select("ADate6='" + Day2 + "'  ");
                    if (dr5.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day2 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr6 = dtData1.Select("ADate7='" + Day2 + "'  ");
                    if (dr6.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day2 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }

                }
                if (Day3 != "")
                {

                    DataRow[] dr = dtData1.Select("ADate1='" + Day3 + "'  ");
                    if (dr.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day3 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr1 = dtData1.Select("ADate2='" + Day3 + "'  ");
                    if (dr1.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day3 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr2 = dtData1.Select("ADate3='" + Day3 + "'  ");
                    if (dr2.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day3 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr3 = dtData1.Select("ADate4='" + Day3 + "'  ");
                    if (dr3.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day3 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr4 = dtData1.Select("ADate5='" + Day3 + "'  ");
                    if (dr4.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day3 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr5 = dtData1.Select("ADate6='" + Day3 + "'  ");
                    if (dr5.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day3 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr6 = dtData1.Select("ADate7='" + Day3 + "'  ");
                    if (dr6.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day3 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                }
                if (Day4 != "")
                {

                    DataRow[] dr = dtData1.Select("ADate1='" + Day4 + "'  ");
                    if (dr.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day4 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr1 = dtData1.Select("ADate2='" + Day4 + "'  ");
                    if (dr1.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day4 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr2 = dtData1.Select("ADate3='" + Day4 + "'  ");
                    if (dr2.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day4 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr3 = dtData1.Select("ADate4='" + Day4 + "'  ");
                    if (dr3.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day4 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr4 = dtData1.Select("ADate5='" + Day4 + "'  ");
                    if (dr4.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day4 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr5 = dtData1.Select("ADate6='" + Day4 + "'  ");
                    if (dr5.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day4 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr6 = dtData1.Select("ADate7='" + Day4 + "'  ");
                    if (dr6.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day4 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }

                }
                if (Day5 != "")
                {

                    DataRow[] dr = dtData1.Select("ADate1='" + Day5 + "'  ");
                    if (dr.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day5 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr1 = dtData1.Select("ADate2='" + Day5 + "'  ");
                    if (dr1.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day5 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr2 = dtData1.Select("ADate3='" + Day5 + "'  ");
                    if (dr2.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day5 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr3 = dtData1.Select("ADate4='" + Day5 + "'  ");
                    if (dr3.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day5 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr4 = dtData1.Select("ADate5='" + Day5 + "'  ");
                    if (dr4.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day5 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr5 = dtData1.Select("ADate6='" + Day5 + "'  ");
                    if (dr5.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day5 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr6 = dtData1.Select("ADate7='" + Day5 + "'  ");
                    if (dr6.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day5 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                }

                if (Day6 != "")
                {

                    DataRow[] dr = dtData1.Select("ADate1='" + Day6 + "'  ");
                    if (dr.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day6 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr1 = dtData1.Select("ADate2='" + Day6 + "'  ");
                    if (dr1.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day6 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr2 = dtData1.Select("ADate3='" + Day6 + "'  ");
                    if (dr2.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day6 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr3 = dtData1.Select("ADate4='" + Day6 + "'  ");
                    if (dr3.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day6 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr4 = dtData1.Select("ADate5='" + Day6 + "'  ");
                    if (dr4.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day6 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr5 = dtData1.Select("ADate6='" + Day6 + "'  ");
                    if (dr5.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day6 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr6 = dtData1.Select("ADate7='" + Day6 + "'  ");
                    if (dr6.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day6 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                }
                if (Day7 != "")
                {

                    DataRow[] dr = dtData1.Select("ADate1='" + Day7 + "'  ");
                    if (dr.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day7 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr1 = dtData1.Select("ADate2='" + Day7 + "'  ");
                    if (dr1.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day7 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr2 = dtData1.Select("ADate3='" + Day7 + "'  ");
                    if (dr2.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day7 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr3 = dtData1.Select("ADate4='" + Day7 + "'  ");
                    if (dr3.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day7 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr4 = dtData1.Select("ADate5='" + Day7 + "'  ");
                    if (dr4.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day7 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr5 = dtData1.Select("ADate6='" + Day7 + "'  ");
                    if (dr5.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day7 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                    DataRow[] dr6 = dtData1.Select("ADate7='" + Day7 + "'  ");
                    if (dr6.Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Employee :- " + Day7 + "," + Convert.ToString(dtData.Rows[i]["TBName"]) + "')</script>", false);
                        return 0;
                    }
                }

            }
        }

        return 1;
    }

    public void UpdateGridBlankChange()
    {
        DataTable dt = Session["DateSearch"] as DataTable;
        DataTable dtAtt = Session["dtAttendation"] as DataTable;
        foreach (GridViewRow Itemst in gvRightSearch.Rows)
        {

            //dtAttendent = (DataTable)ViewState["dtAttendent"];

            int ind = Itemst.DataItemIndex;
            Label lblTday1 = (Label)gvRightSearch.Rows[ind].FindControl("lblTday1");
            Label lblTday2 = (Label)gvRightSearch.Rows[ind].FindControl("lblTday2");
            Label lblTday3 = (Label)gvRightSearch.Rows[ind].FindControl("lblTday3");
            Label lblTday4 = (Label)gvRightSearch.Rows[ind].FindControl("lblTday4");
            Label lblTday5 = (Label)gvRightSearch.Rows[ind].FindControl("lblTday5");
            Label lblTday6 = (Label)gvRightSearch.Rows[ind].FindControl("lblTday6");
            Label lblTday7 = (Label)gvRightSearch.Rows[ind].FindControl("lblTday7");

            Int32 DayCount = 0;

            DataRow[] dr = dtAtt.Select("TBCode='" + gvRightSearch.DataKeys[ind]["TBCode"].ToString() + "' ");
            if (dr.Length > 0)
            {

                if (((CheckBox)Itemst.FindControl("Chk_final1")).Checked == true && ((CheckBox)Itemst.FindControl("Chk_final1")).Visible == true)
                {
                    DayCount += 1;
                    if (dt.Rows.Count > 1)
                    {
                        dr[0]["Day1"] = dt.Rows[0]["TBDate"].ToString();
                        lblTday1.Text = dt.Rows[0]["TBDate"].ToString();
                    }
                }
                if (((CheckBox)Itemst.FindControl("Chk_final2")).Checked == true && ((CheckBox)Itemst.FindControl("Chk_final2")).Visible == true)
                {
                    DayCount += 1;
                    if (dt.Rows.Count > 1)
                    {
                        dr[0]["Day2"] = dt.Rows[1]["TBDate"].ToString();
                        lblTday2.Text = dt.Rows[0]["TBDate"].ToString();

                    }
                }
                if (((CheckBox)Itemst.FindControl("Chk_final3")).Checked == true && ((CheckBox)Itemst.FindControl("Chk_final3")).Visible == true)
                {
                    DayCount += 1;
                    if (dt.Rows.Count > 2)
                    {
                        dr[0]["Day3"] = dt.Rows[2]["TBDate"].ToString();
                        lblTday3.Text = dt.Rows[0]["TBDate"].ToString();
                    }
                }
                if (((CheckBox)Itemst.FindControl("Chk_final4")).Checked == true && ((CheckBox)Itemst.FindControl("Chk_final4")).Visible == true)
                {
                    DayCount += 1;
                    if (dt.Rows.Count > 2)
                    {
                        dr[0]["Day4"] = dt.Rows[3]["TBDate"].ToString();
                        lblTday4.Text = dt.Rows[0]["TBDate"].ToString();
                    }
                }
                if (((CheckBox)Itemst.FindControl("Chk_final5")).Checked == true && ((CheckBox)Itemst.FindControl("Chk_final5")).Visible == true)
                {
                    DayCount += 1;
                    if (dt.Rows.Count > 2)
                    {
                        dr[0]["Day5"] = dt.Rows[4]["TBDate"].ToString();
                        lblTday5.Text = dt.Rows[0]["TBDate"].ToString();
                    }
                }
                if (((CheckBox)Itemst.FindControl("Chk_final6")).Checked == true && ((CheckBox)Itemst.FindControl("Chk_final6")).Visible == true)
                {
                    DayCount += 1;
                    if (dt.Rows.Count > 2)
                    {
                        dr[0]["Day6"] = dt.Rows[5]["TBDate"].ToString();
                        lblTday6.Text = dt.Rows[0]["TBDate"].ToString();
                    }
                }
                if (((CheckBox)Itemst.FindControl("Chk_final7")).Checked == true && ((CheckBox)Itemst.FindControl("Chk_final7")).Visible == true)
                {
                    DayCount += 1;
                    if (dt.Rows.Count > 2)
                    {
                        dr[0]["Day7"] = dt.Rows[6]["TBDate"].ToString();
                        lblTday7.Text = dt.Rows[0]["TBDate"].ToString();
                    }
                }




            }


        }
        Session["dtAttendation"] = dtAtt;

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {

        int TCount = 0;
        if (gvRightSearch.Rows.Count > 0)
        {
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Add User')</script>", false);
            return;
        }
        //if (ViewState["Save"].ToString() == "Save")
        //{
        //}
        //else
        //{
        //    UpdateGridBlankChange();
        //}
        foreach (GridViewRow Itemst in gvRightSearch.Rows)
        {
            string Tday1 = "", Tday2 = "", Tday3 = "", Tday4 = "", Tday5 = "", Tday6 = "", Tday7 = "";
            int ind = Itemst.DataItemIndex;
            string EMPCOde = "";

            Label lblTday1 = (Label)gvRightSearch.Rows[ind].FindControl("lblTday1");
            Label lblTday2 = (Label)gvRightSearch.Rows[ind].FindControl("lblTday2");
            Label lblTday3 = (Label)gvRightSearch.Rows[ind].FindControl("lblTday3");
            Label lblTday4 = (Label)gvRightSearch.Rows[ind].FindControl("lblTday4");
            Label lblTday5 = (Label)gvRightSearch.Rows[ind].FindControl("lblTday5");
            Label lblTday6 = (Label)gvRightSearch.Rows[ind].FindControl("lblTday6");
            Label lblTday7 = (Label)gvRightSearch.Rows[ind].FindControl("lblTday7");
            CheckBox ch1 = (CheckBox)gvRightSearch.Rows[ind].FindControl("Chk_final1");
            CheckBox ch2 = (CheckBox)gvRightSearch.Rows[ind].FindControl("Chk_final2");
            CheckBox ch3 = (CheckBox)gvRightSearch.Rows[ind].FindControl("Chk_final3");
            CheckBox ch4 = (CheckBox)gvRightSearch.Rows[ind].FindControl("Chk_final4");
            CheckBox ch5 = (CheckBox)gvRightSearch.Rows[ind].FindControl("Chk_final5");
            CheckBox ch6 = (CheckBox)gvRightSearch.Rows[ind].FindControl("Chk_final6");
            CheckBox ch7 = (CheckBox)gvRightSearch.Rows[ind].FindControl("Chk_final7");

            if (ch1.Checked == true)
            {
                Tday1 = lblTday1.Text;
                TCount = TCount + 1;
                EMPCOde += "'" + lblTday1.Text + "',";
            }
            if (ch2.Checked == true)
            {
                Tday2 = lblTday2.Text;
                TCount = TCount + 1;
                EMPCOde += "'" + lblTday2.Text + "',";
            }
            if (ch3.Checked == true)
            {
                Tday3 = lblTday2.Text;
                TCount = TCount + 1;
                EMPCOde += "'" + lblTday3.Text + "',";
            }
            if (ch3.Checked == true)
            {
                Tday3 = lblTday3.Text;
                EMPCOde += "'" + lblTday3.Text + "',";
            }
            if (ch4.Checked == true)
            {
                Tday4 = lblTday4.Text;
                TCount = TCount + 1;
                EMPCOde += "'" + lblTday4.Text + "',";
            }
            if (ch5.Checked == true)
            {
                Tday5 = lblTday5.Text;
                TCount = TCount + 1;
                EMPCOde += "'" + lblTday5.Text + "',";
            }
            if (ch6.Checked == true)
            {
                Tday6 = lblTday6.Text;
                TCount = TCount + 1;
                EMPCOde += "'" + lblTday6.Text + "',";
            }
            if (ch7.Checked == true)
            {
                Tday7 = lblTday7.Text;
                TCount = TCount + 1;
                EMPCOde += "'" + lblTday7.Text + "',";
            }


        }

        if (TCount > 0)
        {
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please mark Attendation')</script>", false);
            return;
        }
        Save_Update(0);
        FillScheduling();
        ddlSchedue.SelectedIndex = 0;

    }
    protected void btDownload_Click(object sender, EventArgs e)
    {
        if (ddlState.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select State')</script>", false);
            return;
        }

        DataTable dt = objMain.LoadEmployee(ddlState.SelectedValue, ddlState.SelectedItem.Text, ddlDistrictSearch.SelectedValue, ddlDistrictSearch.SelectedItem.Text);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                ExporttoExcel(dt);
            }
        }
    }
    private void ExporttoExcel(DataTable table)
    {


        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        string Fullfilename = "" + "Employee" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";

        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + " ");

        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'><TR> <TD colspan='7' style='font-size:13.0pt; text-align:center; color:blue; font-family:Calibri;' ><B>" + "" + "</B><TD></TR> <TR>");
        //am getting my grid's column headers
        int columnscount = table.Columns.Count;


        foreach (DataColumn dc in table.Columns)
        {      //write in new column
            HttpContext.Current.Response.Write("<Td>");
            //Get column headers  and make it as bold in excel columns
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(dc.ColumnName);
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


    protected void btnSumbit_Click(object sender, EventArgs e)
    {

        Save_Update(0);
    }


    private void Save_Update(int SchoolCode)
    {

        //if (Convert.ToDateTime(txttimeout.Text) < Convert.ToDateTime(txttimein.Text))
        //{


        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('End Time should be greater than start time')</script>", false);
        //    return;

        //}
        //string RVal = SetTextBoxFocusSelect(this.Page);
        //if (!InterventionSql_Injection(RVal))
        //{
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Spurious input detected. Data rejected')</script>", false);

        //    return;
        //}





        // DataTable dtAttendent = ViewState["dtAttendent"] as DataTable;
        if (ViewState["Save"].ToString() == "Save")
        {

            if (ddlSchedue.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Schedue')</script>", false);
                return;
            }


            int ICount = InsertUpdateStaffSchedueID(ddlSchedue.SelectedValue);
            foreach (GridViewRow row in gvRightSearch.Rows)
            {
                int ind = row.DataItemIndex;
                Label participant = (Label)gvRightSearch.Rows[ind].FindControl("lblTBCode1");

                Label participantname = (Label)gvRightSearch.Rows[ind].FindControl("lblName1");


                // DATE COLUMNS START FROM 5
                for (int i = 2; i < gvRightSearch.HeaderRow.Cells.Count; i++)
                {
                    CheckBox chk = row.Cells[i]
                   .Controls
                   .OfType<CheckBox>()
                   .FirstOrDefault();

                    string attendanceDate =
                        gvRightSearch.HeaderRow.Cells[i].Text.Trim();

                    string status =
                        chk.Checked ? "P" : "A";

                    int Icount = 0;
                    if (attendanceDate.Length > 6)
                    {
                        SqlParameter[] cmdParameters = new SqlParameter[]
                          {
                        new SqlParameter("@SchedulerID", ddlSchedue.SelectedValue),
                        new SqlParameter("@ParticipantCode", participant.Text),
                        new SqlParameter("@ParticipantName", participantname.Text),
                         new SqlParameter("@AttendanceDate", Convert.ToDateTime(attendanceDate)),
                        new SqlParameter("@AttendanceState", status),
                         new SqlParameter("@CreateBy", Convert.ToString(Session["username"] )),



                          };


                        Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateStaffTraningAttendence", cmdParameters);

                    }
                }




            }

            if (Session["dtAttendation2026"] != null)
            {
                DataTable dtatt = Session["dtAttendation2026"] as DataTable;
                if (dtatt.Rows.Count > 0)
                {
                    foreach (DataRow row6 in dtatt.Rows)
                    {
                        // Access by column name

                        string ParticipantType = row6["ParticipantType"].ToString();
                        string ParticipantCode = row6["ParticipantCode"].ToString();
                        string ParticipantName = row6["ParticipantName"].ToString();
                        string UserType = row6["UserType"].ToString();
                        string TeamBalikaUniqueCode = row6["TeamBalikaUniqueCode"].ToString();

                        SqlParameter[] cmdParameters = new SqlParameter[]
                         {
                        new SqlParameter("@SchedulerID", ddlSchedue.SelectedValue),
                        new SqlParameter("@ParticipantCode", ParticipantCode),
                        new SqlParameter("@ParticipantName", ParticipantName),
                         new SqlParameter("@UserType", UserType),
                        new SqlParameter("@TeamBalikaUniqueCode", TeamBalikaUniqueCode),
                         new SqlParameter("@ParticipantType",ParticipantType),



                         };


                        int Icyount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateParticipant", cmdParameters);


                    }
                }
            }

            if (ICount > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Sucessfully')</script>", false);
                GVMainBind();
                ViewState["Save"] = "hhh";
                ViewState["TBCode"] = ddlSchedue.SelectedValue;
                Session["dtAttendation2026"] = null;
            }
        }
        else
        {
            int icount5 = 0;
            foreach (GridViewRow row in gvRightSearch.Rows)
            {
                int ind = row.DataItemIndex;
                Label participant = (Label)gvRightSearch.Rows[ind].FindControl("lblTBCode1");

                Label participantname = (Label)gvRightSearch.Rows[ind].FindControl("lblName1");


                // DATE COLUMNS START FROM 5
                for (int i = 2; i < gvRightSearch.HeaderRow.Cells.Count; i++)
                {
                    CheckBox chk = row.Cells[i]
                   .Controls
                   .OfType<CheckBox>()
                   .FirstOrDefault();

                    string attendanceDate =
                        gvRightSearch.HeaderRow.Cells[i].Text.Trim();

                    string status =
                        chk.Checked ? "P" : "A";


                    if (attendanceDate.Length > 6)
                    {
                        SqlParameter[] cmdParameters = new SqlParameter[]
                          {
                        new SqlParameter("@SchedulerID",   ViewState["TBCode"].ToString()),
                        new SqlParameter("@ParticipantCode", participant.Text),
                        new SqlParameter("@ParticipantName", participantname.Text),
                         new SqlParameter("@AttendanceDate", Convert.ToDateTime(attendanceDate)),
                        new SqlParameter("@AttendanceState", status),
                         new SqlParameter("@CreateBy", Convert.ToString(Session["username"] )),



                          };


                        icount5 = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateStaffTraningAttendence", cmdParameters);

                    }
                }

            }
            if (Session["dtAttendation2026"] != null)
            {
                DataTable dtatt = Session["dtAttendation2026"] as DataTable;
                if (dtatt.Rows.Count > 0)
                {
                    foreach (DataRow row6 in dtatt.Rows)
                    {
                        // Access by column name

                        string ParticipantType = row6["ParticipantType"].ToString();
                        string ParticipantCode = row6["ParticipantCode"].ToString();
                        string ParticipantName = row6["ParticipantName"].ToString();
                        string UserType = row6["UserType"].ToString();
                        string TeamBalikaUniqueCode = row6["TeamBalikaUniqueCode"].ToString();

                        SqlParameter[] cmdParameters = new SqlParameter[]
                         {
                        new SqlParameter("@SchedulerID",  ViewState["TBCode"].ToString()),
                        new SqlParameter("@ParticipantCode", ParticipantCode),
                        new SqlParameter("@ParticipantName", ParticipantName),
                         new SqlParameter("@UserType", UserType),
                        new SqlParameter("@TeamBalikaUniqueCode", TeamBalikaUniqueCode),
                         new SqlParameter("@ParticipantType",ParticipantType),



                         };


                        int Icyount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateParticipant", cmdParameters);


                    }
                }
            }
            if (icount5 > 0)
            {


                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Sucessfully')</script>", false);
                GVMainBind();

                //}
            }
        }
    }
    public int InsertUpdateStaffSchedueID(string SchedueID)
    {
        int Icount = 0;
        try
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {

                      new SqlParameter("@SchedueID", SchedueID),
                         new SqlParameter("@Createby",    Convert.ToString(Session["username"])),

            };


            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateStaffTraningMain2026", cmdParameters);
        }
        catch
        {

        }
        return Icount;
    }

    public int InsertUpdateStaffTraningMain(string UniqueCode, string Learningtype, string TrainingMode, string TrainingType, string DistCode, string BlockCode, string FromDate, string ToDate, string Status, string Description, string Createby, string Type, string SchedueID, string TrainerName, string Email, string Contact, string InternalTrainername, string StartTime, string EndTime, string Flag)
    {
        int Icount = 0;
        try
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@UniqueCode", UniqueCode),
            new SqlParameter("@Learningtype", Learningtype),
                 new SqlParameter("@TrainingMode", TrainingMode),
                              new SqlParameter("@TrainingType", TrainingType),
            new SqlParameter("@DistCode", DistCode),
             new SqlParameter("@BlockCode", BlockCode),
             new SqlParameter("@FromDate", FromDate),
             new SqlParameter("@ToDate", ToDate),
             new SqlParameter("@Status", Status),
                            new SqlParameter("@Description", Description),
                new SqlParameter("@Createby", Createby),
                   new SqlParameter("@Type", Type),
                      new SqlParameter("@SchedueID", SchedueID),
                         new SqlParameter("@TrainerName", TrainerName),
               new SqlParameter("@Email", Email),
                new SqlParameter("@Contact", Contact),

                 new SqlParameter("@InternalTrainername", InternalTrainername),
                     new SqlParameter("@StartTime", StartTime),
                         new SqlParameter("@EndTime", EndTime),
                            new SqlParameter("@Flag", Flag),

            };


            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateStaffTraningMain2023", cmdParameters);
        }
        catch
        {

        }
        return Icount;
    }

    protected void GVMain_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "GVUIO")
        {

            int iIndex = Convert.ToInt32(e.CommandArgument);
            string TBCode = GVMain.DataKeys[iIndex]["UniqueCode"].ToString();
            ViewState["TBCode"] = TBCode;
            Session["TB"] = null;
            //gvUser.DataSource = null;
            //gvUser.DataBind();
            FillControls(TBCode);
            ViewState["Save"] = "Edit";


            ///  FillAttandent(TBCode);

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

    private void FillAttandent(string ptCOde)
    {
        //DataTable dt = objMain.LoadTB(ptCOde);



        //DataTable dtTb = objMain.LoadData(" SELECT 0 as TbDay, '--Select--' TBDate FROM tblStaffTrainingSchedue union select day(dateadd(d,number-1,FromDate)) as TbDay,CONVERT(varchar,dateadd(d,number-1,FromDate),103) as TBDate   from [tblStaffTrainingSchedue] tp,Numbers     WHERE Number<=DATEDIFF(day,FromDate,CONVERT(datetime,todate)+1) and UniqueCode= '" + ptCOde + "'   order by TbDay ");
        //if (dtTb.Rows.Count > 0)
        //{
        //    ddlDate.DataSource = dtTb;
        //    ddlDate.DataTextField = "TBDate";
        //    ddlDate.DataValueField = "TBDate";
        //    ddlDate.DataBind();

        //}

    }


    protected void gvTb_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            CheckBox Chk_final1 = (CheckBox)e.Row.FindControl("Chk_final1");
            CheckBox Chk_final2 = (CheckBox)e.Row.FindControl("Chk_final2");
            CheckBox Chk_final3 = (CheckBox)e.Row.FindControl("Chk_final3");
            CheckBox Chk_final4 = (CheckBox)e.Row.FindControl("Chk_final4");
            CheckBox Chk_final5 = (CheckBox)e.Row.FindControl("Chk_final5");
            CheckBox Chk_final6 = (CheckBox)e.Row.FindControl("Chk_final6");
            CheckBox Chk_final7 = (CheckBox)e.Row.FindControl("Chk_final7");
            Label lblFlag = (Label)e.Row.FindControl("lblFlag");
            if (lblFlag.Text == "Web")
            {
                Chk_final1.Enabled = true;
                Chk_final2.Enabled = true;
                Chk_final3.Enabled = true;
                Chk_final4.Enabled = true;
                Chk_final5.Enabled = true;
                Chk_final6.Enabled = true;
                Chk_final7.Enabled = true;

            }
            else
            {
                Chk_final1.Enabled = false;
                Chk_final2.Enabled = false;
                Chk_final3.Enabled = false;
                Chk_final4.Enabled = false;
                Chk_final5.Enabled = false;
                Chk_final6.Enabled = false;
                Chk_final7.Enabled = false;
            }
            Label lblTday1 = (Label)e.Row.FindControl("lblTday1");

            Label lblTday2 = (Label)e.Row.FindControl("lblTday2");

            Label lblTday3 = (Label)e.Row.FindControl("lblTday3");
            Label lblTday4 = (Label)e.Row.FindControl("lblTday4");
            Label lblTday5 = (Label)e.Row.FindControl("lblTday5");
            Label lblTday6 = (Label)e.Row.FindControl("lblTday6");
            Label lblTday7 = (Label)e.Row.FindControl("lblTday7");
            if (lblTday1.Text == "P")
            {
                Chk_final1.Checked = true;
            }
            if (lblTday2.Text == "P")
            {
                Chk_final2.Checked = true;
            }
            if (lblTday3.Text == "P")
            {
                Chk_final3.Checked = true;
            }
            if (lblTday4.Text == "P")
            {
                Chk_final4.Checked = true;
            }
            if (lblTday5.Text == "P")
            {
                Chk_final5.Checked = true;
            }
            if (lblTday6.Text == "P")
            {
                Chk_final6.Checked = true;
            }
            if (lblTday7.Text == "P")
            {
                Chk_final7.Checked = true;
            }

            DataTable dt = Session["DateSearch"] as DataTable;
            if (dt.Rows.Count == 1)
            {
                e.Row.Cells[2].Visible = true;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;



            }
            if (dt.Rows.Count == 2)
            {


                e.Row.Cells[2].Visible = true;
                e.Row.Cells[3].Visible = true;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;


            }
            if (dt.Rows.Count == 3)
            {


                e.Row.Cells[2].Visible = true;
                e.Row.Cells[3].Visible = true;
                e.Row.Cells[4].Visible = true;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;



            }
            if (dt.Rows.Count == 4)
            {
                e.Row.Cells[2].Visible = true;
                e.Row.Cells[3].Visible = true;
                e.Row.Cells[4].Visible = true;
                e.Row.Cells[5].Visible = true;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;

            }
            if (dt.Rows.Count == 5)
            {
                e.Row.Cells[2].Visible = true;
                e.Row.Cells[3].Visible = true;
                e.Row.Cells[4].Visible = true;
                e.Row.Cells[5].Visible = true;
                e.Row.Cells[6].Visible = true;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;


            }
            if (dt.Rows.Count == 6)
            {
                e.Row.Cells[2].Visible = true;
                e.Row.Cells[3].Visible = true;
                e.Row.Cells[4].Visible = true;
                e.Row.Cells[5].Visible = true;
                e.Row.Cells[6].Visible = true;
                e.Row.Cells[7].Visible = true;
                e.Row.Cells[8].Visible = false;
            }
            if (dt.Rows.Count == 7)
            {
                e.Row.Cells[2].Visible = true;
                e.Row.Cells[3].Visible = true;
                e.Row.Cells[4].Visible = true;
                e.Row.Cells[5].Visible = true;
                e.Row.Cells[6].Visible = true;
                e.Row.Cells[7].Visible = true;
                e.Row.Cells[8].Visible = true;
            }
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {

            DataTable dt = Session["DateSearch"] as DataTable;
            if (dt.Rows.Count == 1)
            {
                DateTime c = Convert.ToDateTime(dt.Rows[0]["TBDate"].ToString());
                string Main = c.ToString("dd/MM/yyyy");
                e.Row.Cells[2].Text = Main;
                e.Row.Cells[2].Visible = true;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
            }
            if (dt.Rows.Count == 2)
            {
                DateTime c = Convert.ToDateTime(dt.Rows[0]["TBDate"].ToString());
                string Main = c.ToString("dd/MM/yyyy");

                DateTime c1 = Convert.ToDateTime(dt.Rows[1]["TBDate"].ToString());
                string Main1 = c1.ToString("dd/MM/yyyy");
                e.Row.Cells[2].Text = Main;
                e.Row.Cells[3].Text = Main1;
                e.Row.Cells[2].Visible = true;
                e.Row.Cells[3].Visible = true;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;


            }
            if (dt.Rows.Count == 3)
            {
                DateTime c = Convert.ToDateTime(dt.Rows[0]["TBDate"].ToString());
                string Main = c.ToString("dd/MM/yyyy");

                DateTime c1 = Convert.ToDateTime(dt.Rows[1]["TBDate"].ToString());
                string Main1 = c1.ToString("dd/MM/yyyy");

                DateTime c2 = Convert.ToDateTime(dt.Rows[2]["TBDate"].ToString());
                string Main2 = c2.ToString("dd/MM/yyyy");



                e.Row.Cells[2].Text = Main;
                e.Row.Cells[3].Text = Main1;
                e.Row.Cells[4].Text = Main2;
                e.Row.Cells[2].Visible = true;
                e.Row.Cells[3].Visible = true;
                e.Row.Cells[4].Visible = true;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
            }
            if (dt.Rows.Count == 4)
            {

                DateTime c = Convert.ToDateTime(dt.Rows[0]["TBDate"].ToString());
                string Main = c.ToString("dd/MM/yyyy");

                DateTime c1 = Convert.ToDateTime(dt.Rows[1]["TBDate"].ToString());
                string Main1 = c1.ToString("dd/MM/yyyy");

                DateTime c2 = Convert.ToDateTime(dt.Rows[2]["TBDate"].ToString());
                string Main2 = c2.ToString("dd/MM/yyyy");


                DateTime c3 = Convert.ToDateTime(dt.Rows[3]["TBDate"].ToString());
                string Main3 = c3.ToString("dd/MM/yyyy");

                e.Row.Cells[2].Text = Main;
                e.Row.Cells[3].Text = Main1;
                e.Row.Cells[4].Text = Main2;
                e.Row.Cells[5].Text = Main3;
                e.Row.Cells[2].Visible = true;
                e.Row.Cells[3].Visible = true;
                e.Row.Cells[4].Visible = true;
                e.Row.Cells[5].Visible = true;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
            }

            if (dt.Rows.Count == 5)
            {
                DateTime c = Convert.ToDateTime(dt.Rows[0]["TBDate"].ToString());
                string Main = c.ToString("dd/MM/yyyy");

                DateTime c1 = Convert.ToDateTime(dt.Rows[1]["TBDate"].ToString());
                string Main1 = c1.ToString("dd/MM/yyyy");

                DateTime c2 = Convert.ToDateTime(dt.Rows[2]["TBDate"].ToString());
                string Main2 = c2.ToString("dd/MM/yyyy");


                DateTime c3 = Convert.ToDateTime(dt.Rows[3]["TBDate"].ToString());
                string Main3 = c3.ToString("dd/MM/yyyy");
                DateTime c4 = Convert.ToDateTime(dt.Rows[4]["TBDate"].ToString());
                string Main4 = c4.ToString("dd/MM/yyyy");

                e.Row.Cells[2].Text = Main;
                e.Row.Cells[3].Text = Main1;
                e.Row.Cells[4].Text = Main2;
                e.Row.Cells[5].Text = Main3;
                e.Row.Cells[6].Text = Main4;
                e.Row.Cells[2].Visible = true;
                e.Row.Cells[3].Visible = true;
                e.Row.Cells[4].Visible = true;
                e.Row.Cells[5].Visible = true;
                e.Row.Cells[6].Visible = true;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
            }
            if (dt.Rows.Count == 6)
            {
                DateTime c = Convert.ToDateTime(dt.Rows[0]["TBDate"].ToString());
                string Main = c.ToString("dd/MM/yyyy");

                DateTime c1 = Convert.ToDateTime(dt.Rows[1]["TBDate"].ToString());
                string Main1 = c1.ToString("dd/MM/yyyy");

                DateTime c2 = Convert.ToDateTime(dt.Rows[2]["TBDate"].ToString());
                string Main2 = c2.ToString("dd/MM/yyyy");


                DateTime c3 = Convert.ToDateTime(dt.Rows[3]["TBDate"].ToString());
                string Main3 = c3.ToString("dd/MM/yyyy");
                DateTime c4 = Convert.ToDateTime(dt.Rows[4]["TBDate"].ToString());
                string Main4 = c4.ToString("dd/MM/yyyy");

                DateTime c5 = Convert.ToDateTime(dt.Rows[5]["TBDate"].ToString());
                string Main5 = c5.ToString("dd/MM/yyyy");

                e.Row.Cells[2].Text = Main;
                e.Row.Cells[3].Text = Main1;
                e.Row.Cells[4].Text = Main2;
                e.Row.Cells[5].Text = Main3;
                e.Row.Cells[6].Text = Main4;
                e.Row.Cells[7].Text = Main5;
                e.Row.Cells[2].Visible = true;
                e.Row.Cells[3].Visible = true;
                e.Row.Cells[4].Visible = true;
                e.Row.Cells[5].Visible = true;
                e.Row.Cells[6].Visible = true;
                e.Row.Cells[7].Visible = true;
                e.Row.Cells[8].Visible = false;
            }
            if (dt.Rows.Count == 7)
            {
                DateTime c = Convert.ToDateTime(dt.Rows[0]["TBDate"].ToString());
                string Main = c.ToString("dd/MM/yyyy");

                DateTime c1 = Convert.ToDateTime(dt.Rows[1]["TBDate"].ToString());
                string Main1 = c1.ToString("dd/MM/yyyy");

                DateTime c2 = Convert.ToDateTime(dt.Rows[2]["TBDate"].ToString());
                string Main2 = c2.ToString("dd/MM/yyyy");


                DateTime c3 = Convert.ToDateTime(dt.Rows[3]["TBDate"].ToString());
                string Main3 = c3.ToString("dd/MM/yyyy");
                DateTime c4 = Convert.ToDateTime(dt.Rows[4]["TBDate"].ToString());
                string Main4 = c4.ToString("dd/MM/yyyy");

                DateTime c5 = Convert.ToDateTime(dt.Rows[5]["TBDate"].ToString());
                string Main5 = c5.ToString("dd/MM/yyyy");

                DateTime c6 = Convert.ToDateTime(dt.Rows[6]["TBDate"].ToString());
                string Main6 = c6.ToString("dd/MM/yyyy");
                e.Row.Cells[2].Text = Main;
                e.Row.Cells[3].Text = Main1;
                e.Row.Cells[4].Text = Main2;
                e.Row.Cells[5].Text = Main3;
                e.Row.Cells[6].Text = Main4;
                e.Row.Cells[7].Text = Main5;
                e.Row.Cells[8].Text = Main6;
                e.Row.Cells[2].Visible = true;
                e.Row.Cells[3].Visible = true;
                e.Row.Cells[4].Visible = true;
                e.Row.Cells[5].Visible = true;
                e.Row.Cells[6].Visible = true;
                e.Row.Cells[7].Visible = true;
                e.Row.Cells[8].Visible = true;
            }
        }


    }
    private void FillControls(string ptCOde)
    {
        DataTable dtmstM = null;
        DataTable dtmsLock = null;

        DataTable dtScheduling = StaffEntryQuery(ptCOde, "", "", "1");

        if (dtScheduling.Rows.Count > 0)
        {

            ddlSchedue.SelectedIndex = 0;
            ddlState.SelectedValue = dtScheduling.Rows[0]["StateCode"].ToString();
            ddlState_SelectedIndexChanged(ddlState, null);
            ddlDistrictSearch.SelectedValue = dtScheduling.Rows[0]["DistrictCode"].ToString();

            ViewState["DIst"] = dtScheduling.Rows[0]["DistrictCode"].ToString();
            if (dtScheduling.Rows[0]["DistrictCode"].ToString() != "0")
            {
                ddlTraingOutcome.SelectedValue = dtScheduling.Rows[0]["OutcomeID"].ToString();
            }
            LoadOutComeSpicify();
            ddlLearning.SelectedValue = dtScheduling.Rows[0]["Outcome"].ToString();

            ddlTraining.SelectedValue = dtScheduling.Rows[0]["TrainingType"].ToString();
            ddlTraingMode.SelectedValue = dtScheduling.Rows[0]["TrainingMode"].ToString();
            //lbltr.Text = dtScheduling.Rows[0]["Other"].ToString();
            DateTime StartDate = Convert.ToDateTime(dtScheduling.Rows[0]["FromDate"].ToString());
            CalendarExtender1.StartDate = Convert.ToDateTime(StartDate);
            txtLocation.Text = dtScheduling.Rows[0]["Location"].ToString();
            txtFromDate.Text = StartDate.ToString("dd/MM/yyyy");

            DateTime EnDate = Convert.ToDateTime(dtScheduling.Rows[0]["ToDate"].ToString());
            CalendarExtender2.StartDate = Convert.ToDateTime(EnDate);
            txtToDate.Text = EnDate.ToString("dd/MM/yyyy");

            txtToDate.Enabled = false;
            //txtFromDate.Enabled = false;
            //pnlMain1.Enabled = true;
            ViewState["Save"] = "tfytryty";
            ViewState["SchedueID"] = ddlSchedue.SelectedValue;
            ddlDistrictSearch.Enabled = false;
            ddlState.Enabled = false;
            Butteon2.Enabled = true;


            string fdate = txtFromDate.Text;
            string[] b = fdate.Split('/');
            string FromDate = b[2] + '-' + b[1] + '-' + b[0];

            string Tdate = txtToDate.Text;
            string[] T = Tdate.Split('/');
            string Todate = T[2] + '-' + T[1] + '-' + T[0];

            DataTable dtTb = objMain.LoadData(" SELECT  day(dateadd(d,number-1,'" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd") + "')) as TbDay ,  CONVERT(varchar,dateadd(d,number-1,'" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd") + "'),103) As TBDate from Numbers WHERE Number<=DATEDIFF(day,('" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd") + "'),CONVERT(datetime,'" + Convert.ToDateTime(Todate).ToString("yyyy-MM-dd") + "')+1) ");

            //DataTable DateSearch = objMain.LoadData(" SELECT  day(dateadd(d,number-1,'" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd") + "')) as TbDay ,  CONVERT(varchar,dateadd(d,number-1,'" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd") + "'),103) As TBDate from Numbers WHERE Number<=DATEDIFF(day,('" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd") + "'),CONVERT(datetime,'" + Convert.ToDateTime(Todate).ToString("yyyy-MM-dd") + "')+1) ");

            Session["DateSearch"] = dtTb;

            LoadMainSPEdit(ptCOde);


            Session["dtEntryDoneBY"] = null;

            Session["dtAttendation2026"] = null;
            //pnltb.Visible = true;



            txtFromDate.Enabled = false;
            txtToDate.Enabled = false;
            if (Convert.ToString(Session["username"]) == "PMSAdmin")
            {

            }
            else
            {
                TimeSpan D = (DateTime.Now.Date - Convert.ToDateTime(dtScheduling.Rows[0]["ToDate"]));
                int Days = D.Days;
                if (dtScheduling.Rows[0]["LockRecord"].ToString() == "5")
                {
                }
                else
                {
                    if (Days <= 7)
                    {
                        btnsave.Enabled = true;
                        btnDelete.Enabled = true;
                        //pnlMain1.Enabled = true;
                    }
                    else
                    {
                        //  pnlMain1.Enabled = false;
                        btnsave.Enabled = false;
                        btnDelete.Enabled = false;

                        return;
                    }
                }
            }



        }




    }
    public void LoadMainSPEdit(string Sid)
    {
        DataTable dtAttendation = StaffEntryQueryEdit(Sid);
        if (dtAttendation.Rows.Count > 0)
        {
            Session["dtSP"] = dtAttendation;
            gvRightSearch.DataSource = dtAttendation;
            gvRightSearch.DataBind();

        }
        else
        {
            Session["dtSP"] = null;
            gvRightSearch.DataSource = null;
            gvRightSearch.DataBind();

        }
    }
    public DataTable CreateDataEntry()
    {

        DataTable dtEntryDoneBY = new DataTable();


        dtEntryDoneBY.Columns.Add(new DataColumn("ParticiparticipateName", System.Type.GetType("System.String")));
        dtEntryDoneBY.Columns.Add(new DataColumn("EntryDoneByName", System.Type.GetType("System.String")));
        Session["dtEntryDoneBY"] = dtEntryDoneBY;
        return dtEntryDoneBY;
    }
    public DataTable CreateDataEntryAttendation()
    {

        DataTable Attendation = new DataTable();

        Attendation.Columns.Add(new DataColumn("TBCode", System.Type.GetType("System.String")));
        Attendation.Columns.Add(new DataColumn("TBName", System.Type.GetType("System.String")));
        Attendation.Columns.Add(new DataColumn("TotalDay", System.Type.GetType("System.String")));
        Attendation.Columns.Add(new DataColumn("UniqueCode", System.Type.GetType("System.String")));
        Attendation.Columns.Add(new DataColumn("UserType", System.Type.GetType("System.String")));
        Attendation.Columns.Add(new DataColumn("Day1", System.Type.GetType("System.String")));
        Attendation.Columns.Add(new DataColumn("Day2", System.Type.GetType("System.String")));
        Attendation.Columns.Add(new DataColumn("Day3", System.Type.GetType("System.String")));
        Attendation.Columns.Add(new DataColumn("Day4", System.Type.GetType("System.String")));
        Attendation.Columns.Add(new DataColumn("Day5", System.Type.GetType("System.String")));
        Attendation.Columns.Add(new DataColumn("Day6", System.Type.GetType("System.String")));
        Attendation.Columns.Add(new DataColumn("Day7", System.Type.GetType("System.String")));
        Attendation.Columns.Add(new DataColumn("iFlag", System.Type.GetType("System.String")));


        Session["dtAttendation"] = Attendation;
        return Attendation;
    }
    protected void LnkImport_Click(object sender, EventArgs e)
    {

        if (txtFromDate.Text == "" || txtToDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Training Date')</script>", false);
            return;
        }
        //string fdate = txtFromDate.Text;
        //string[] b = fdate.Split('/');
        //string FromDate = b[2] + '-' + b[1] + '-' + b[0];

        //string Tdate = txtToDate.Text;
        //string[] T = Tdate.Split('/');
        //string Todate = T[2] + '-' + T[1] + '-' + T[0];

        //DataTable dtTb = objMain.LoadData(" SELECT  day(dateadd(d,number-1,'" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd") + "')) as TbDay ,  CONVERT(varchar,dateadd(d,number-1,'" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd") + "'),103) As TBDate from Numbers WHERE Number<=DATEDIFF(day,('" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd") + "'),CONVERT(datetime,'" + Convert.ToDateTime(Todate).ToString("yyyy-MM-dd") + "')+1) ");

        ////DataTable DateSearch = objMain.LoadData(" SELECT  day(dateadd(d,number-1,'" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd") + "')) as TbDay ,  CONVERT(varchar,dateadd(d,number-1,'" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd") + "'),103) As TBDate from Numbers WHERE Number<=DATEDIFF(day,('" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd") + "'),CONVERT(datetime,'" + Convert.ToDateTime(Todate).ToString("yyyy-MM-dd") + "')+1) ");

        //Session["DateSearch"] = dtTb;
        //txtParticipate.Text = "";
        //UpdateGrid();
        //DataTable dtParticiparticipate = null;

        //dtParticiparticipate = ((DataTable)Session["dtAttendation"]);
        //GridView1.DataSource = dtParticiparticipate;
        //GridView1.DataBind();
        //gvRightSearch.DataSource = dtParticiparticipate;
        //gvRightSearch.DataBind();
        MPEFormName1.Show();

    }
    protected void btnParticipate_Click(object sender, EventArgs e)
    {

        string RVal = SetTextBoxFocusSelect(this.Page);
        if (!InterventionSql_Injection(RVal))
        {
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Spurious input detected. Data rejected')</script>", false);
            MPEFormName1.Show();
            return;
        }
        DataTable dtParticiparticipate = null;
        DataTable DateSearch = Session["DateSearch"] as DataTable;
        int iFlag = 0;
        DataTable dt = null;
        dt = ((DataTable)Session["dtSP"]);
        if (Session["dtAttendation2026"] != null)
        {
            dtParticiparticipate = ((DataTable)Session["dtAttendation2026"]);
            iFlag = 1;
        }
        else
        {
            dtParticiparticipate = CreateDataDate2026();
        }
        if (txtParticipate.Text != "")
        {
            string[] words = txtParticipate.Text.Trim().Split(',');
            foreach (var word in words)
            {
                if (word.Length > 3)
                {
                    if (word.Length > 3)
                    {
                        DataRow[] drmain = dtParticiparticipate.Select("ParticipantCode='" + word.Trim() + "'");
                        if (drmain.Length > 0)
                        {

                        }
                        else
                        {
                            DataTable dtP1 = new DataTable();

                            dtP1 = Get_DataFor1Filter1("LoadStaffParticiparticipate", "1", word.Trim());

                            if (dtP1.Rows.Count > 0)
                            {
                                DataRow dr;
                                dr = dtParticiparticipate.NewRow();
                                dr["ParticipantCode"] = word.Trim();
                                dr["SchedulerID"] = "0";
                                if (dtP1.Rows.Count > 0)
                                {
                                    dr["ParticipantName"] = dtP1.Rows[0]["EMPName"].ToString();
                                }
                                else
                                {
                                    dr["ParticipantName"] = string.Empty;
                                }

                                dr["ParticipantType"] = ddlType.SelectedValue;
                                dr["ParticipantTypeName"] = ddlType.SelectedItem.Text;
                                dr["UserType"] = dtP1.Rows[0]["UserType"].ToString();
                                dtParticiparticipate.Rows.Add(dr);

                            }
                        }
                    }
                    DataRow[] drmain1 = dt.Select("Participant='" + word.Trim() + "'");
                    if (drmain1.Length > 0)
                    {

                    }
                    else
                    {
                        DataTable dtP1 = new DataTable();
                        dtP1 = Get_DataFor1Filter1("LoadParticiparticipate2023", "1", word.Trim());
                        if (dtP1.Rows.Count > 0)
                        {
                            DataRow dr;
                            dr = dt.NewRow();
                            dr["Participant"] = word.Trim();

                            if (dtP1.Rows.Count > 0)
                            {
                                dr["ParticipantName"] = dtP1.Rows[0]["EMPName"].ToString();
                                dr["UserType"] = dtP1.Rows[0]["UserType"].ToString();
                            }
                            else
                            {
                                dr["ParticipantName"] = string.Empty;
                            }
                            dr["Flag"] = "1";
                            dr["ScheduleID"] = "1";
                            DataRow dr1 = dt.NewRow();

                            dr1["Participant"] = word.Trim();

                            if (dtP1.Rows.Count > 0)
                            {
                                dr1["ParticipantName"] = dtP1.Rows[0]["EMPName"].ToString();
                                dr1["UserType"] = dtP1.Rows[0]["UserType"].ToString();
                            }
                            else
                            {
                                dr1["ParticipantName"] = "";
                                dr1["UserType"] = "";
                            }

                            dr1["Flag"] = "Web";
                            dr1["ScheduleID"] = "1";


                            if (dt.Rows.Count == 1)
                            {
                                dr1["Day1"] = "P";
                            }

                            if (DateSearch.Rows.Count == 1)
                            {
                                dr1["Day1"] = "P";
                            }

                            if (DateSearch.Rows.Count == 2)
                            {
                                dr1["Day1"] = "P";
                                dr1["Day2"] = "P";
                            }

                            if (DateSearch.Rows.Count == 3)
                            {
                                dr1["Day1"] = "P";
                                dr1["Day2"] = "P";
                                dr1["Day3"] = "P";
                            }
                            if (DateSearch.Rows.Count == 4)
                            {
                                dr1["Day1"] = "P";
                                dr1["Day2"] = "P";
                                dr1["Day3"] = "P";
                                dr1["Day4"] = "P";
                            }
                            if (DateSearch.Rows.Count == 5)
                            {
                                dr1["Day1"] = "P";
                                dr1["Day2"] = "P";
                                dr1["Day3"] = "P";
                                dr1["Day4"] = "P";
                                dr1["Day5"] = "P";
                            }
                            if (DateSearch.Rows.Count == 6)
                            {
                                dr1["Day1"] = "P";
                                dr1["Day2"] = "P";
                                dr1["Day3"] = "P";
                                dr1["Day4"] = "P";
                                dr1["Day5"] = "P";
                                dr1["Day6"] = "P";
                            }
                            if (DateSearch.Rows.Count == 7)
                            {
                                dr1["Day1"] = "P";
                                dr1["Day2"] = "P";
                                dr1["Day3"] = "P";
                                dr1["Day4"] = "P";
                                dr1["Day5"] = "P";
                                dr1["Day7"] = "P";
                            }
                            dt.Rows.Add(dr1);
                        }
                    }
                }
            }
        }

        Session["dtSP"] = dt;
        UpdateGridBlank();
        Session["dtAttendation2026"] = dtParticiparticipate;
        gvRightSearch.DataSource = dt;
        gvRightSearch.DataBind();
        //gvRightSearch.DataSource = dtParticiparticipate;
        //gvRightSearch.DataBind();
        //UpdateGridBlank();
        //dtParticiparticipate = null;
        //dtParticiparticipate = Session["dtAttendation"] as DataTable;

        //gvRightSearch.DataSource = dtParticiparticipate;
        //gvRightSearch.DataBind();
        //GridView1.DataSource = dtParticiparticipate;
        //GridView1.DataBind();


        //if (Convert.ToString(ViewState["Tarining_ID"]) == "")
        //{
        //}
        //else
        //{

        //    //int Tarining_ID = Convert.ToInt32(ViewState["Tarining_ID"].ToString());


        //    //DataTable dt = Session["dtParticiparticipate"] as DataTable;

        //    //DataTable dtparti = Session["dtParticiparticipate"] as DataTable;
        //    //if (dtparti.Rows.Count > 0)
        //    //{
        //    //    for (int i = 0; i < dtparti.Rows.Count; i++)
        //    //    {
        //    //        dt.Rows[i]["FormID"] = Tarining_ID;
        //    //    }
        //    //    int Parti_Success = Insert_participate(Tarining_ID, dtparti);
        //    //}
        //}
        MPEFormName1.Show();
    }
    protected void Delete_Question_Click2(object sender, EventArgs e)
    {
        //MPEFormName.Show();

        LinkButton Edit_Question = sender as LinkButton;
        GridViewRow row = Edit_Question.NamingContainer as GridViewRow;
        int index = row.RowIndex;


        //  string QuestionID = (GridView1.DataKeys[index].Values["CreateDataEntryAttendation"].ToString());
        DataTable dtParticiparticipate = null;

        dtParticiparticipate = ((DataTable)Session["dtAttendation"]);
        dtParticiparticipate.Rows.Remove(dtParticiparticipate.Rows[index]);

        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Delete Sucessfully')</script>", false);

        Session["dtAttendation"] = dtParticiparticipate;
        GridView1.DataSource = dtParticiparticipate;
        GridView1.DataBind();
        gvRightSearch.DataSource = dtParticiparticipate;
        gvRightSearch.DataBind();
        MPEFormName1.Show();
    }
    public DataTable Get_DataFor1Filter1(string ProcedureName, string Filter1, string Filter2)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                            new SqlParameter("@Filter1",Filter1),
                            new SqlParameter("@Filter2",Filter2),
                    };
            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, ProcedureName, paramvT);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch (Exception)
        {

        }
        return dtcombo;
    }
    public DataTable CreateDataDate2026()
    {

        DataTable dtParticiparticipate = new DataTable();


        dtParticiparticipate.Columns.Add(new DataColumn("SchedulerID", System.Type.GetType("System.String")));
        dtParticiparticipate.Columns.Add(new DataColumn("ParticipantType", System.Type.GetType("System.String")));
        dtParticiparticipate.Columns.Add(new DataColumn("ParticipantTypeName", System.Type.GetType("System.String")));
        dtParticiparticipate.Columns.Add(new DataColumn("ParticipantCode", System.Type.GetType("System.String")));
        dtParticiparticipate.Columns.Add(new DataColumn("ParticipantName", System.Type.GetType("System.String")));
        dtParticiparticipate.Columns.Add(new DataColumn("UserType", System.Type.GetType("System.String")));

        dtParticiparticipate.Columns.Add(new DataColumn("TeamBalikaUniqueCode", System.Type.GetType("System.String")));
        Session["dtStatffParticiparticipate"] = dtParticiparticipate;
        return dtParticiparticipate;
    }
    protected void BtnEntry_Click(object sender, EventArgs e)
    {
        DataTable dtEntryDoneBY = null;


        if (Session["dtEntryDoneBY"] != null)
        {
            dtEntryDoneBY = ((DataTable)Session["dtEntryDoneBY"]);
        }
        else
        {
            dtEntryDoneBY = CreateDataEntry();
        }
        if (TextBox1.Text != "")
        {
            string[] words = TextBox1.Text.Trim().Split(',');
            foreach (var word in words)
            {
                if (word.Length > 3)
                {
                    DataRow[] drmain = dtEntryDoneBY.Select("ParticiparticipateName='" + word.Trim() + "'");
                    if (drmain.Length > 0)
                    {

                    }
                    else
                    {
                        DataTable dtP1 = new DataTable();
                        dtP1 = Get_DataFor1Filter1("LoadParticiparticipate", "1", word.Trim());
                        if (dtP1.Rows.Count > 0)
                        {
                            DataRow dr;
                            dr = dtEntryDoneBY.NewRow();
                            dr["ParticiparticipateName"] = word.Trim();

                            if (dtP1.Rows.Count > 0)
                            {
                                dr["EntryDoneByName"] = dtP1.Rows[0]["EMPName"].ToString();
                            }
                            else
                            {
                                dr["EntryDoneByName"] = string.Empty;
                            }
                            dtEntryDoneBY.Rows.Add(dr);
                        }
                    }
                }
            }
        }

        Session["dtEntryDoneBY"] = dtEntryDoneBY;
        GvEntry.DataSource = dtEntryDoneBY;
        GvEntry.DataBind();
        GvEntryNew.DataSource = dtEntryDoneBY;
        GvEntryNew.DataBind();
        MPE_Entry.Show();

    }
    protected void LnkEntry_Click(object sender, EventArgs e)
    {
        //string RVal = SetTextBoxFocusSelect(this.Page);
        //if (!InterventionSql_Injection(RVal))
        //{
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Spurious input detected. Data rejected')</script>", false);
        //    MPE_Entry.Show();
        //    return;
        //}
        TextBox1.Text = "";
        DataTable dtParticiparticipate = Session["dtEntryDoneBY"] as DataTable;
        if (dtParticiparticipate != null)
        {
            if (dtParticiparticipate.Rows.Count > 0)
            {
                GvEntry.DataSource = dtParticiparticipate;
                GvEntry.DataBind();
                GvEntryNew.DataSource = dtParticiparticipate;
                GvEntryNew.DataBind();
            }
            else
            {
                GvEntry.DataSource = null;
                GvEntry.DataBind();
                GvEntryNew.DataSource = null;
                GvEntryNew.DataBind();
            }
        }
        else
        {
            GvEntry.DataSource = null;
            GvEntry.DataBind();
            GvEntryNew.DataSource = null;
            GvEntryNew.DataBind();
        }
        MPE_Entry.Show();


    }
    protected void Delete_Question_Click1(object sender, EventArgs e)
    {
        //MPEFormName.Show();

        LinkButton Edit_Question = sender as LinkButton;
        GridViewRow row = Edit_Question.NamingContainer as GridViewRow;
        int index = row.RowIndex;


        string QuestionID = (GvEntry.DataKeys[index].Values["ParticiparticipateName"].ToString());
        DataTable dtParticiparticipate = null;

        dtParticiparticipate = ((DataTable)Session["dtEntryDoneBY"]);
        dtParticiparticipate.Rows.Remove(dtParticiparticipate.Rows[index]);

        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Delete Sucessfully')</script>", false);

        Session["dtEntryDoneBY"] = dtParticiparticipate;
        GvEntry.DataSource = dtParticiparticipate;
        GvEntry.DataBind();

        GvEntryNew.DataSource = dtParticiparticipate;
        GvEntryNew.DataBind();
        MPE_Entry.Show();
    }
    public void UpdateGrid()
    {

        foreach (GridViewRow Itemst in gvRightSearch.Rows)
        {
            if (((CheckBox)Itemst.FindControl("Chk_final1")).Checked || ((CheckBox)Itemst.FindControl("Chk_final2")).Checked || ((CheckBox)Itemst.FindControl("Chk_final3")).Checked || ((CheckBox)Itemst.FindControl("Chk_final4")).Checked || ((CheckBox)Itemst.FindControl("Chk_final5")).Checked || ((CheckBox)Itemst.FindControl("Chk_final6")).Checked || ((CheckBox)Itemst.FindControl("Chk_final7")).Checked)
            {
                //dtAttendent = (DataTable)ViewState["dtAttendent"];
                DataTable dt = Session["DateSearch"] as DataTable;
                DataTable dtAtt = Session["dtAttendation"] as DataTable;
                int ind = Itemst.DataItemIndex;


                Int32 DayCount = 0;

                DataRow[] dr = dtAtt.Select("TBCode='" + gvRightSearch.DataKeys[ind]["TBCode"].ToString() + "'");
                if (dr.Length > 0)

                {

                    if (((CheckBox)Itemst.FindControl("Chk_final1")).Checked)
                    {
                        DayCount += 1;

                        dr[0]["Day1"] = dt.Rows[0]["TBDate"].ToString();
                    }
                    else
                    {
                        dr[0]["Day1"] = "";
                    }

                    if (((CheckBox)Itemst.FindControl("Chk_final2")).Checked)
                    {
                        DayCount += 1;
                        if (dt.Rows.Count > 1)
                        {
                            dr[0]["Day2"] = dt.Rows[1]["TBDate"].ToString();
                        }
                    }
                    else
                    {
                        dr[0]["Day2"] = "";
                    }
                    if (((CheckBox)Itemst.FindControl("Chk_final3")).Checked)
                    {
                        DayCount += 1;
                        if (dt.Rows.Count > 2)
                        {
                            dr[0]["Day3"] = dt.Rows[2]["TBDate"].ToString();
                        }
                    }
                    else
                    {
                        dr[0]["Day3"] = "";
                    }
                    if (((CheckBox)Itemst.FindControl("Chk_final4")).Checked)
                    {
                        DayCount += 1;
                        if (dt.Rows.Count > 2)
                        {
                            dr[0]["Day4"] = dt.Rows[3]["TBDate"].ToString();
                        }
                    }
                    else
                    {
                        dr[0]["Day4"] = "";
                    }
                    if (((CheckBox)Itemst.FindControl("Chk_final5")).Checked)
                    {
                        DayCount += 1;
                        if (dt.Rows.Count > 2)
                        {
                            dr[0]["Day5"] = dt.Rows[4]["TBDate"].ToString();
                        }
                    }
                    else
                    {
                        dr[0]["Day5"] = "";
                    }
                    if (((CheckBox)Itemst.FindControl("Chk_final6")).Checked)
                    {
                        DayCount += 1;
                        if (dt.Rows.Count > 2)
                        {
                            dr[0]["Day6"] = dt.Rows[5]["TBDate"].ToString();
                        }
                    }
                    else
                    {
                        dr[0]["Day6"] = "";
                    }
                    if (((CheckBox)Itemst.FindControl("Chk_final7")).Checked)
                    {
                        DayCount += 1;
                        if (dt.Rows.Count > 2)
                        {
                            dr[0]["Day7"] = dt.Rows[6]["TBDate"].ToString();
                        }
                    }
                    else
                    {
                        dr[0]["Day7"] = "";
                    }

                    dr[0]["TotalDay"] = DayCount;

                }

                Session["dtAttendation"] = dtAtt;
            }
        }
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
    public bool InterventionSql_Injection(string RVal)
    {
        SqlInjection objAudit = new SqlInjection();
        bool injection = false;


        injection = objAudit.CheckInputBool(RVal);

        return injection;

    }
    protected void btnExcel_Onclick(object sender, EventArgs e)
    {
        if (Session["dtAttendation"] != null)
        {
            DataTable dt = new DataTable();
            dt = Session["dtAttendation"] as DataTable;
            DataTable dtcopy = dt.Copy();
            dtcopy.Columns.Remove("TotalDay");
            dtcopy.Columns.Remove("UniqueCode");
            dtcopy.Columns.Remove("UserType");
            dtcopy.Columns.Remove("Day1");
            dtcopy.Columns.Remove("Day2");
            dtcopy.Columns.Remove("Day3");
            dtcopy.Columns.Remove("Day4");
            dtcopy.Columns.Remove("Day5");
            dtcopy.Columns.Remove("Day6");
            dtcopy.Columns.Remove("Day7");
            dtcopy.Columns.Remove("iFlag");

            ExporttoExcel(dtcopy, "ParticipateReport");
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('No Records')</script>", false);
        }
    }





    public void UpdateGridBlank()
    {
        foreach (GridViewRow Itemst in gvRightSearch.Rows)
        {

            //dtAttendent = (DataTable)ViewState["dtAttendent"];
            DataTable dt = Session["DateSearch"] as DataTable;
            DataTable dtAtt = Session["dtSP"] as DataTable;
            int ind = Itemst.DataItemIndex;


            Int32 DayCount = 0;

            DataRow[] dr = dtAtt.Select("Participant='" + gvRightSearch.DataKeys[ind]["Participant"].ToString() + "' ");
            if (dr.Length > 0)
            {
                Label lblFlag = (Label)Itemst.FindControl("lblFlag");
                lblFlag.Text = "Web";

                if (((CheckBox)Itemst.FindControl("Chk_final1")).Checked == false && ((CheckBox)Itemst.FindControl("Chk_final1")).Visible == true)
                {
                    DayCount += 1;

                    dr[0]["Day1"] = "A";
                }
                if (((CheckBox)Itemst.FindControl("Chk_final2")).Checked == false && ((CheckBox)Itemst.FindControl("Chk_final2")).Visible == true)
                {
                    DayCount += 1;
                    if (dt.Rows.Count > 1)
                    {
                        dr[0]["Day2"] = "A";
                    }
                }
                if (((CheckBox)Itemst.FindControl("Chk_final3")).Checked == false && ((CheckBox)Itemst.FindControl("Chk_final3")).Visible == true)
                {
                    DayCount += 1;
                    if (dt.Rows.Count > 2)
                    {
                        dr[0]["Day3"] = "A";
                    }
                }
                if (((CheckBox)Itemst.FindControl("Chk_final4")).Checked == false && ((CheckBox)Itemst.FindControl("Chk_final4")).Visible == true)
                {
                    DayCount += 1;
                    if (dt.Rows.Count > 2)
                    {
                        dr[0]["Day4"] = "A";
                    }
                }
                if (((CheckBox)Itemst.FindControl("Chk_final5")).Checked == false && ((CheckBox)Itemst.FindControl("Chk_final5")).Visible == true)
                {
                    DayCount += 1;
                    if (dt.Rows.Count > 2)
                    {
                        dr[0]["Day5"] = "A";
                    }
                }
                if (((CheckBox)Itemst.FindControl("Chk_final6")).Checked == false && ((CheckBox)Itemst.FindControl("Chk_final6")).Visible == true)
                {
                    DayCount += 1;
                    if (dt.Rows.Count > 2)
                    {
                        dr[0]["Day6"] = "A";
                    }
                }
                if (((CheckBox)Itemst.FindControl("Chk_final7")).Checked == false && ((CheckBox)Itemst.FindControl("Chk_final7")).Visible == true)
                {
                    DayCount += 1;
                    if (dt.Rows.Count > 2)
                    {
                        dr[0]["Day7"] = "A";
                    }
                }


                //dr[0]["TotalDay"] = DayCount;
                // dr[0]["iFlag"] = "0";

            }

            Session["dtSP"] = dtAtt;

        }
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
                int columnscount = GridView1.HeaderRow.Cells.Count;



                for (int j = 0; j < columnscount; j++)
                {      //write in new column
                    if (j == 0)
                    {
                        HttpContext.Current.Response.Write("<Td>");
                        //Get column headers  and make it as bold in excel columns
                        HttpContext.Current.Response.Write("<B>");
                        HttpContext.Current.Response.Write("Participate Code");
                        HttpContext.Current.Response.Write("</B>");
                        HttpContext.Current.Response.Write("</Td>");
                    }
                    if (j == 0)
                    {
                        HttpContext.Current.Response.Write("<Td>");
                        //Get column headers  and make it as bold in excel columns
                        HttpContext.Current.Response.Write("<B>");
                        HttpContext.Current.Response.Write("Participate Name");
                        HttpContext.Current.Response.Write("</B>");
                        HttpContext.Current.Response.Write("</Td>");
                    }
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

}