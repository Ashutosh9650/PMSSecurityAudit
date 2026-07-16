using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SurveyTrainingProcess2026 : System.Web.UI.Page
{


    ArrayList arraylist1 = new ArrayList();
    ArrayList arraylist2 = new ArrayList();
    string conditions = "";
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    SqlConnection mycon = new SqlConnection(SqlHelper.mainConnectionString);
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                FillDropdownPre();
                LoadYear();
                LoadUserLeavel();
                FillScheduling();
                //   GVMainBind();
                ViewState["Tarining_ID"] = "";
                ViewState["dtselect"] = null;
                ViewState["dtselected"] = null;
                Session["dtParticiparticipate"] = null;
                Session["dtEntryDoneBY"] = null;
                gvRightSearch.DataSource = null;
                gvRightSearch.DataBind();
                GvQuestion.DataSource = null;
                GvQuestion.DataBind();
                linkSurvey.Visible = false;
                lblUni.Text = "";
                txtLink.Text = "";

                GvEntry.DataSource = null;
                GvEntry.DataBind();
                LinkButton1.Visible = false;
                txtLink.Visible = false;
                btnsave.Enabled = true;
                fillcategory("0");

                //CalendarExtender1.StartDate = DateTime.Today.AddDays(0);
                //CalendarExtender2.StartDate = DateTime.Today.AddDays(0);

                txtFromDate.Enabled = true;
                txtToDate.Enabled = true;
                if (Convert.ToString(Session["username"]) == "PMSAdmin" || Convert.ToString(Session["username"]) == "EGE0606" || Convert.ToString(Session["username"]) == "EGE7557" || Convert.ToString(Session["username"]) == "SuperAdmin")
                {
                    btnDelete.Visible = true;
                }
                else
                {
                    btnDelete.Visible = false;
                }
                btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }

        }
        ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "loadJSFunction();", true);
    }
    public void FillScheduling()
    {
        conditions = "";
        if (Convert.ToString(Session["username"]) == "PMSAdmin" || Convert.ToString(Session["username"]) == "EGE7557" || Convert.ToString(Session["username"]) == "SuperAdmin")
        {
            objComman.BindDLL("[tblStaffScheduling] left join mstOutcome on mstOutcome.OutcomeID=[Outcome] left join mstlearning on mstlearning.learningID=[Outcome]", "[ScheduleID] ,case when TrainingTypeFlag=1 then 'Staff Training -' + mstOutcome.OutcomeName +' ( '+ convert (varchar(10),[FromDate] ,121)  +' To '+ convert (varchar(10),[ToDate] ,121) +' )' else  'Team Balik Training -' +  mstlearning.learningName +' ( '+ convert (varchar(10),[FromDate] ,121)  +' To '+ convert (varchar(10),[ToDate] ,121) +' )' end as Schedule ", "   isnull(AssmentFlag,0)=0 and SdeleteFlag=1  and  FromDate>'2026-04-01' ", "Schedule", "asc", ddlSchedue, "Schedule", "ScheduleID", "--Select--");

        }
        else
        {
            conditions = "";
            string conditions1 = "StateCode ='" + ddlState.SelectedValue + "' ";
            if (Session["user_level_Role"].ToString() == "1")
            {

                conditions = "StateCode ='" + ddlState.SelectedValue + "' and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
            }
            else if (Session["user_level_Role"].ToString() == "2")
            {
                conditions = "StateCode ='" + ddlState.SelectedValue + "'  and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
            }
            else
            {
                conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";


            }

            if (Session["user_level_Role"].ToString() == "2")
            {
                conditions = "";
                conditions = " mst2District.StateCode ='" + ddlState.SelectedValue + "' and UserName='" + Session["username"].ToString() + "' ";
                string strQry1 = "       sELECT distinct mst2District.DistrictCode as DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist  ";
                strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions + "  and  Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName  ";
                DataTable dtDistrict = objMain.LoadData(strQry1);
                string DistrictName = "";
                foreach (DataRow row in dtDistrict.Rows)
                {


                    DistrictName += "'" + row["DistrictCode"].ToString() + "'" + ",";

                }

                if (DistrictName.Length > 0)
                {
                    DistrictName = DistrictName.Substring(0, DistrictName.LastIndexOf(","));
                }
                objComman.BindDLL("[tblStaffScheduling] left join mstOutcome on mstOutcome.OutcomeID=[Outcome] left join mstlearning on mstlearning.learningID=[Outcome]", "[ScheduleID] ,case when TrainingTypeFlag=1 then 'Staff Training -' + mstOutcome.OutcomeName +' ( '+ convert (varchar(10),[FromDate] ,121)  +' To '+ convert (varchar(10),[ToDate] ,121) +' )' else  'Team Balik Training -' +  mstlearning.learningName +' ( '+ convert (varchar(10),[FromDate] ,121)  +' To '+ convert (varchar(10),[ToDate] ,121) +' )' end as Schedule  ", " userID like '%" + Session["username"] + "%' and   isnull(AssmentFlag,0)=0    and FromDate>'2026-04-01' and DistrictCode in(" + DistrictName + ") ", "Schedule", "asc", ddlSchedue, "Schedule", "ScheduleID", "--Select--");

            }
            else
            {
                objComman.BindDLL("[tblStaffScheduling] left join mstOutcome on mstOutcome.OutcomeID=[Outcome] left join mstlearning on mstlearning.learningID=[Outcome]", "[ScheduleID] ,case when TrainingTypeFlag=1 then 'Staff Training -' + mstOutcome.OutcomeName +' ( '+ convert (varchar(10),[FromDate] ,121)  +' To '+ convert (varchar(10),[ToDate] ,121) +' )' else  'Team Balik Training -' +  mstlearning.learningName +' ( '+ convert (varchar(10),[FromDate] ,121)  +' To '+ convert (varchar(10),[ToDate] ,121) +' )' end as Schedule  ", " userID like '%" + Session["username"] + "%' and   isnull(AssmentFlag,0)=0   and  FromDate>'2026-04-01' ", "Schedule", "asc", ddlSchedue, "Schedule", "ScheduleID", "--Select--");
            }
        }


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
    protected void ddlSchedue_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSchedue.SelectedIndex > 0)
        {

            //string strQry = "SELECT StateCode, case Inducation when 0 then Other else sOutcomeName end as Other ,Location,isnull(TrainingMode,0) as TrainingMode  ,   [tblStaffScheduling].[LockRecord] , [tblStaffScheduling].[DistrictCode]   ,[FromDate]  ,[ToDate]  ,[Inducation][Outcome],mstOutcomeSpecific.OutcomeID ,[TrainingType]       ,[UserID]  ,[ScheduleID]  FROM [tblStaffScheduling]   left join mstOutcomeSpecific on mstOutcomeSpecific.sOutcomeID=[Inducation] where  [ScheduleID]=" + ddlSchedue.SelectedValue + "  ";



            DataTable dtScheduling = StaffEntryQuery(ddlSchedue.SelectedValue, "", "", "1");

            if (dtScheduling.Rows.Count > 0)
            {
                FillDropdown();

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

                ddlLevel.SelectedValue = dtScheduling.Rows[0]["TrainingTypeFlag"].ToString();
                ddlLevel_SelectedIndexChanged(ddlLevel, null);
                ViewState["DIst"] = dtScheduling.Rows[0]["DistrictCode"].ToString();
                if (ddlLevel.SelectedValue == "1")
                {
                    ddlLearning.SelectedValue = dtScheduling.Rows[0]["OutcomeM"].ToString();

                    ddlLearning_SelectedIndexChanged(ddlLevel, null);
                    ddlTraingOutcome.SelectedValue = dtScheduling.Rows[0]["Inducation"].ToString();
                }
                else
                {

                    ddlTraingOutcome.SelectedValue = dtScheduling.Rows[0]["OutcomeM"].ToString();
                }
                ddlTraining.SelectedValue = dtScheduling.Rows[0]["TrainingType"].ToString();
                ddlTraingMode.SelectedValue = dtScheduling.Rows[0]["TrainingMode"].ToString();
                //lbltr.Text = dtScheduling.Rows[0]["Other"].ToString();
                DateTime StartDate = Convert.ToDateTime(dtScheduling.Rows[0]["FromDate"].ToString());

                txtLocation.Text = dtScheduling.Rows[0]["Location"].ToString();
                txtFromDate.Text = StartDate.ToString("dd/MM/yyyy");

                DateTime EnDate = Convert.ToDateTime(dtScheduling.Rows[0]["ToDate"].ToString());

                txtToDate.Text = EnDate.ToString("dd/MM/yyyy");

                txtFromDate.Enabled = false;
                txtToDate.Enabled = false;
                ddlDistrictSearch.Enabled = false;
                ddlState.Enabled = false;
                txtLocation.Enabled = false;
                ddlTraingMode.Enabled = false;
                ddlLevel.Enabled = false;
                ddlLearning.Enabled = false;
                ddlTraingOutcome.Enabled = false;
                ddlTraining.Enabled = false;
                //txtFromDate.Enabled = false;
                //pnlMain1.Enabled = true;
                DataTable Participarticipate = new DataTable();
                Participarticipate = Get_DataFor1Filter("USP_Tarining_Participarticipate2026shul", ddlSchedue.SelectedValue.ToString());

                if (Participarticipate.Rows.Count > 0)
                {
                    Session["dtParticiparticipate"] = Participarticipate;
                }
                else
                {
                    Session["dtParticiparticipate"] = null;

                }

                DataTable EntryDoneBY = new DataTable();
                EntryDoneBY = Get_DataFor1Filter("USP_Tarining_EntryDoneBySchu", ddlSchedue.SelectedValue.ToString());

                if (EntryDoneBY.Rows.Count > 0)
                {
                    Session["dtEntryDoneBY"] = EntryDoneBY;
                }
                else
                {
                    Session["dtEntryDoneBY"] = null;

                }
                ViewState["SchedueID"] = ddlSchedue.SelectedValue;

                // Butteon2.Enabled = true;
                ViewState["Tarining_ID"] = "";


                string fdate = txtFromDate.Text;
                string[] b = fdate.Split('/');
                string FromDate = b[2] + '-' + b[1] + '-' + b[0];

                string Tdate = txtToDate.Text;
                string[] T = Tdate.Split('/');
                string Todate = T[2] + '-' + T[1] + '-' + T[0];



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
            ///  objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            //DataTable dtTb = objMain.LoadData(" SELECT StateCode,  dbo.TitleCase(upper(StateName)) as  StateName FROM [mst1State]  order by Statecode  ");



            //objComman.BindDLLMasterTableVillage("mst1State", "StateCode,StateName", dtTb, conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "Select");

            //ddlState.SelectedIndex = 0;

            //ddlState.Enabled = true;
            //ddlDistrictSearch.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            ////   objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");


            //conditions = "UserName='" + Session["username"].ToString() + "' ";
            //string strQry1 = "  SELECT distinct mst1State.StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode inner join mst1State on mst1State.StateCode=mst2District.StateCode where   " + conditions + "   order by StateName   ";
            //DataTable dtTb = objMain.LoadData(strQry1);

            //// DataTable dtTb = objMain.LoadData(" SELECT StateCode,  dbo.TitleCase(upper(StateName)) as  StateName FROM [mst1State] where " + conditions + " union select  StateCode,  dbo.TitleCase(upper(StateName))  +' ('+ 'Spine' +')'  StateName  from [mstSpineState] order by Statecode  ");



            //objComman.BindDLLMasterTableVillage("mst1State", "StateCode,StateName", dtTb, conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "Select");

            ddlState.SelectedIndex = 1;

            ddlState.Enabled = true;
            ddlDistrictSearch.Enabled = true;
        }
        else
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            ////objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            //DataTable dtTb = objMain.LoadData(" SELECT StateCode,  dbo.TitleCase(upper(StateName)) as  StateName FROM [mst1State] where " + conditions + " order by Statecode  ");



            //objComman.BindDLLMasterTableVillage("mst1State", "StateCode,StateName", dtTb, conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "Select");

            ddlState.SelectedIndex = 1;
            ddlState.Enabled = false;
            ddlDistrictSearch.Enabled = false;

        }


        if (Session["user_level_Role"].ToString() == "1")
        {
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "";
            //conditions = "StateCode ='" + ddlState.SelectedValue + "'  and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
            //// objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

            //string conditions1 = "StateCode ='" + ddlState.SelectedValue + "' ";

            //DataTable dtTb = objMain.LoadData(" SELECT DistrictCode,  dbo.TitleCase(upper(DistrictName)) as  DistrictName FROM [mst2District] where  " + conditions + "  union select  DistrictCode,  dbo.TitleCase(upper(DistrictName))  +' ('+ 'Spine' +')'  as  DistrictName from mstSpineDistrict  where  " + conditions1 + "   order by DistrictName ");



            //objComman.BindDLLMasterTableVillage("mst2District", "DistrictCode,DistrictName", dtTb, conditions, "DistrictName", "asc", ddlDistrictSearch, "DistrictName", "DistrictCode", "Select");

            conditions = "";
            conditions = " mst2District.StateCode ='" + ddlState.SelectedValue + "' and UserName='" + Session["username"].ToString() + "' ";
            string strQry1 = "       sELECT distinct mst2District.DistrictCode as DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist  ";
            strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions + "  and  Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName  ";
            DataTable dtDistrict = objMain.LoadData(strQry1);

            objComman.BindDLLDatatable("mst2District", dtDistrict, "DistrictCode, dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "Desc", ddlDistrictSearch, "DistrictName", "DistrictCode", "--Select--");

            ddlDistrictSearch.SelectedIndex = 0;

            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }

        else
        {
            conditions = "";
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and Fyear= '" + ddlYear.SelectedItem.Text + "' ";
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");


            string conditions1 = "StateCode ='" + ddlState.SelectedValue + "' ";

            // DataTable dtTb = objMain.LoadData(" SELECT DistrictCode,  dbo.TitleCase(upper(DistrictName)) as  DistrictName FROM [mst2District] where  " + conditions + "  union select  DistrictCode,  dbo.TitleCase(upper(DistrictName))  +' ('+ 'Spine' +')'  as  DistrictName from mstSpineDistrict  where  " + conditions1 + "   order by DistrictName ");

            DataTable dtTb = objMain.LoadData(" SELECT DistrictCode,  dbo.TitleCase(upper(DistrictName)) as  DistrictName FROM [mst2District] where  " + conditions + "     order by DistrictName ");



            objComman.BindDLLMasterTableVillage("mst2District", "DistrictCode,DistrictName", dtTb, conditions, "DistrictName", "asc", ddlDistrictSearch, "DistrictName", "DistrictCode", "Select");


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
            // ddlDistrict.SelectedValue=Session["DistrictCode"].ToString() ;

            ddlDistrictSearch.SelectedIndex = 1;
            ddlDist_SelectedIndexChanged(ddlDistrictSearch, null);
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }





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

    protected void btn_AddEmp(object sender, EventArgs e)
    {
    }

    public void Filllearning()
    {
        string conditions = "  ISNULL(TrainingStatus,0)=1 ";
        objComman.BindDLL("mstlearning", "learningID,dbo.TitleCase(upper(learningName)) as learningName ", conditions, "learningName", "asc", ddlTraingOutcome, "learningName", "learningID", "--Select--");

    }


    protected void btnSerach_Click(object sender, EventArgs e)
    {
        string str = "";
        //string fdate = txtFromDate.Text;
        //string[] b = fdate.Split('/');
        //string FromDate = b[2] + '-' + b[1] + '-' + b[0];

        //string Tdate = txtToDate.Text;
        //string[] T = Tdate.Split('/');
        //string Todate = T[2] + '-' + T[1] + '-' + T[0];

        //DateTime d1 = Convert.ToDateTime(FromDate);
        //DateTime d2 = Convert.ToDateTime(Todate);
        //int month = Convert.ToInt32(T[1]) - Convert.ToInt32(b[1]);
        //TimeSpan t = d2 - d1;

        //double Days = Convert.ToDouble(t.TotalDays);

        //if (Math.Sign(Days) < 0)
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select less then or equal 7 Day')</script>", false);
        //    return;
        //}
        //if (Math.Round(Days) > 7)
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select less then or equal 7 Days')</script>", false);
        //    return;
        //}

        //if (txtFromDate.Text != "" && txtToDate.Text != "")
        //{
        //    str = str + "and FromDate= '" + FromDate + "' and ToDate='" + Todate + "'";
        //}

        //if (Convert.ToString(ViewState["TBCode"]) != null)
        //{
        //    str = str + " and UniqueCode not in (" + Convert.ToString(ViewState["TBCode"]) + ")";
        //}

        //DataTable dtcheck = objComman.LoadData("Select * from tblStaffTrainingSchedue " + str + "");
        //if (dtcheck.Rows.Count > 0)
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Training not allowed')</script>", false);

        //    return;
        //}
        //else
        //{

        //    DataTable dtTb = objMain.LoadData(" SELECT  day(dateadd(d,number-1,'" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd") + "')) as TbDay ,  CONVERT(varchar,dateadd(d,number-1,'" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd") + "'),103) As TBDate from Numbers WHERE Number<=DATEDIFF(day,('" + Convert.ToDateTime(FromDate).ToString("yyyy-MM-dd") + "'),CONVERT(datetime,'" + Convert.ToDateTime(Todate).ToString("yyyy-MM-dd") + "')+1) ");

        //    Session["DateSearch"] = dtTb;
        //    GVMainBindSearch();

        //    txtFromDate.Enabled = false;
        //    txtToDate.Enabled = false;
        //}

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        FillDropdownPre();
        ddlLevel.SelectedIndex = 1;
        ddlLevel_SelectedIndexChanged(ddlLevel, null);
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtLink.Text = "";
        txtLocation.Text = "";
        txtLink.Visible = false;
        txtOthersName.Text = "";
        linkSurvey.Visible = false;
        ViewState["Tarining_ID"] = "";
        ViewState["dtselect"] = null;
        ViewState["dtselected"] = null;
        Session["dtParticiparticipate"] = null;
        Session["dtEntryDoneBY"] = null;
        gvRightSearch.DataSource = null;
        gvRightSearch.DataBind();
        ddlMainID.ClearSelection();
        GvEntry.DataSource = null;
        GvEntry.DataBind();
        GvQuestion.DataSource = null;
        GvQuestion.DataBind();
        lblUni.Text = "";
        btnsave.Enabled = true;
        lnkCopy.Visible = false;
        LnkEntry.Visible = false;
        LinkButton1.Visible = false;

        txtFromDate.Enabled = true;
        txtToDate.Enabled = true;
        ddlDistrictSearch.Enabled = true;
        ddlState.Enabled = true;
        txtLocation.Enabled = true;
        ddlTraingMode.Enabled = true;
        ddlLevel.Enabled = true;
        ddlLearning.Enabled = true;
        ddlTraingOutcome.Enabled = true;
        ddlTraining.Enabled = true;
    }
    protected void Unlock_Click(object sender, EventArgs e)
    {
        MPECopyEndline1.Show();
    }

    protected void LockSave(object sender, EventArgs e)
    {
        DateTime FromDate = Convert.ToDateTime(txtLockDate.Text);

        SqlParameter[] cmdParameters = new SqlParameter[]
        {
    new SqlParameter(
        "@Lockdate",
        Convert.ToDateTime(FromDate)
    ),

    new SqlParameter(
        "@Tarining_ID",
        Convert.ToInt32(ViewState["Tarining_ID"])
    )
        };

        int icount = SqlHelper.ExecuteNonQuery(
                 SqlHelper.mainConnectionString,
                 CommandType.StoredProcedure,
                 "USP_Update_tbl_training_question_LockDate",
                 cmdParameters
             );
        ;
        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
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
        int Tarining_ID = 0, AssessmentFor = 0, TrainingOutCome = 0, SpecificTraining = 0, AssessmentType = 0, Trainingtype = 0, QuestionCategory = 0;
        string Location = "", other = "", EntryBy = "";

        AssessmentFor = Convert.ToInt32(ddlLevel.SelectedValue);
        if (Convert.ToInt32(AssessmentFor) == 1)
        {
            if (ddlLearning.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Training OutCome')</script>", false);

                return;
            }
            if (ddlTraingOutcome.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Specific training')</script>", false);

                return;
            }

            if (ddlassement.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select  Assessment Type')</script>", false);

                return;
            }

            if (ddlTraingMode.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select  Traing Mode')</script>", false);

                return;
            }
            TrainingOutCome = Convert.ToInt32(ddlLearning.SelectedValue);
            SpecificTraining = Convert.ToInt32(ddlTraingOutcome.SelectedValue);
            AssessmentType = Convert.ToInt32(ddlassement.SelectedValue);
        }
        if (Convert.ToInt32(AssessmentFor) == 2)
        {
            TrainingOutCome = Convert.ToInt32(ddlTraingOutcome.SelectedValue);
            AssessmentType = Convert.ToInt32(ddlassement.SelectedValue);
            if (ddlTraingOutcome.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Training OutCome')</script>", false);

                return;
            }


            if (ddlassement.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select  Assessment Type')</script>", false);

                return;
            }
        }

        if (ddlMainID.SelectedValue == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Main Training or Reorientation')</script>", false);

            return;
        }

        if ((Convert.ToInt32(ddlLevel.SelectedValue) == 1 || Convert.ToInt32(ddlLevel.SelectedValue) == 2) && Convert.ToInt32(ddlMainID.SelectedValue) == 2)
        {
            if (txtFromDate.Text == "" || txtToDate.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Training Date')</script>", false);
                return;
            }
            string str = "";
            string fdate = txtFromDate.Text;
            string[] b = fdate.Split('/');
            string FromDate1 = b[2] + '-' + b[1] + '-' + b[0];

            string Tdate = txtToDate.Text;
            string[] T = Tdate.Split('/');
            string Todate = T[2] + '-' + T[1] + '-' + T[0];

            DateTime d1 = Convert.ToDateTime(FromDate1);
            DateTime d2 = Convert.ToDateTime(Todate);
            int month = Convert.ToInt32(T[1]) - Convert.ToInt32(b[1]);
            TimeSpan t = d2 - d1;

            double Days = Convert.ToDouble(t.TotalDays);
            if (Days < 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Valid  Data')</script>", false);
                return;
            }
            if (Math.Sign(Days + 1) < 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date Max 7 Day')</script>", false);
                return;
            }
            if (Math.Round(Days + 1) > 1)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date Max 1 Day')</script>", false);
                return;
            }
        }
        else
        {

            if (Convert.ToInt32(ddlLevel.SelectedValue) == 1 || Convert.ToInt32(ddlLevel.SelectedValue) == 2)
            {
                if (txtFromDate.Text == "" || txtToDate.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Training Date')</script>", false);
                    return;
                }
                string str = "";
                string fdate = txtFromDate.Text;
                string[] b = fdate.Split('/');
                string FromDate1 = b[2] + '-' + b[1] + '-' + b[0];

                string Tdate = txtToDate.Text;
                string[] T = Tdate.Split('/');
                string Todate = T[2] + '-' + T[1] + '-' + T[0];

                DateTime d1 = Convert.ToDateTime(FromDate1);
                DateTime d2 = Convert.ToDateTime(Todate);
                int month = Convert.ToInt32(T[1]) - Convert.ToInt32(b[1]);
                TimeSpan t = d2 - d1;

                double Days = Convert.ToDouble(t.TotalDays);
                if (Days < 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Valid  Data')</script>", false);
                    return;
                }
                if (Math.Sign(Days + 1) < 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date Max 7 Day')</script>", false);
                    return;
                }
                if (Math.Round(Days + 1) > 7)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date Max 7 Day')</script>", false);
                    return;
                }
                if (ddlDistrictSearch.SelectedValue != null && ddlDistrictSearch.SelectedIndex > 0)
                {
                    str = "where  DistCode='" + ddlDistrictSearch.SelectedValue.ToString() + "'";
                }


                if (txtFromDate.Text != "" && txtToDate.Text != "")
                {
                    str = str + "and FromDate= '" + FromDate1 + "' and ToDate='" + Todate + "'";
                }
                if (ddlLearning.SelectedIndex > 0)
                {
                    str = str + "and Learningtype='" + this.ddlTraingOutcome.SelectedValue.ToString() + "'";
                }
                DataTable dtPhase = null;
                if (Convert.ToInt32(ddlLevel.SelectedValue) == 2)
                {
                    dtPhase = objComman.LoadData("select  isnull(N_P1_Y1,0) as [NoOfDays]  from  Tbl_TB_Training Where  TrainingType='T' and LearningID='" + ddlTraingOutcome.SelectedValue + "' and StateCode='" + ddlState.SelectedValue + "' and DistrictCode='" + ddlDistrictSearch.SelectedValue + "'");

                }
                else
                {
                    ///    DataTable dtPhase = objComman.LoadData("select  Case when Phase=1 and  Program_Year=1 THEN N_P1_Y1 WHEN Phase=1 and Program_Year=2 THEN N_P1_Y2 WHEN Phase=1 and Program_Year>=3 THEN  N_P1_Y3 WHEN Phase=2 and  Program_Year<=4 THEN N_P2_Y1 WHEN Phase=2 and Program_Year>=5 THEN N_P2_Y2  WHEN Phase=3 and  Program_Year<=6 THEN N_P3_Y1 WHEN Phase=3 and Program_Year>=7 THEN N_P3_Y2 WHEN Phase=4 and  Program_Year=8 THEN N_P4_Y2 WHEN Phase=4 and Program_Year=9 THEN N_P4_Y3 END as [NoOfDays]  from Tbl_PhaseMapping  p inner join (select * from Tbl_TB_Training Where  TrainingType='T') TS on TS.FYear=p.Financial_Year Where TS.LearningID='" + ddlLearning.SelectedValue + "' and p.StateCode='" + ddlState.SelectedValue + "' and p.DistrictCode='" + ddlDist.SelectedValue + "'");
                    dtPhase = objComman.LoadData("select  isnull(N_P1_Y1,0) as [NoOfDays]  from Tbl_TB_Training Where  TrainingType='S' and OutComeID='" + ddlLearning.SelectedValue + "' and SoutComeID='" + ddlTraingOutcome.SelectedValue + "' and StateCode='" + ddlState.SelectedValue + "' and DistrictCode='" + ddlDistrictSearch.SelectedValue + "'");
                }

                if (dtPhase.Rows.Count > 0)
                {
                    if (Convert.ToInt32(ddlLevel.SelectedValue) == 2)
                    {
                        if (Convert.ToInt32(dtPhase.Rows[0]["NoOfDays"]) == 0)
                        {
                        }
                        else if (Math.Round(Days + 1) == Convert.ToInt32(dtPhase.Rows[0]["NoOfDays"]))
                        {
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Staff Training: Selected Training Days are either less than or greater than " + dtPhase.Rows[0]["NoOfDays"] + " Days')</script>", false);
                            return;
                        }
                    }
                    if (Convert.ToInt32(ddlLevel.SelectedValue) == 1 && Convert.ToInt32(ddlassement.SelectedValue) == 1)
                    {
                        if (Convert.ToInt32(dtPhase.Rows[0]["NoOfDays"]) == 0)
                        {
                        }
                        else if (Math.Round(Days + 1) == Convert.ToInt32(dtPhase.Rows[0]["NoOfDays"]))
                        {
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Staff Training: Selected Training Days are either less than or greater than " + dtPhase.Rows[0]["NoOfDays"] + " Days')</script>", false);
                            return;
                        }
                    }
                }
            }
        }
        if (Convert.ToInt32(AssessmentFor) == 3 || Convert.ToInt32(AssessmentFor) == 4)
        {
            if (txtOthersName.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Other')</script>", false);

                return;
            }
        }
        if (Convert.ToInt32(gvRightSearch.Rows.Count) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Add Question')</script>", false);

            return;
        }

        if (txtTotalQuestions.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Total No. of Questions')</script>", false);

            return;
        }
        if (Convert.ToInt32(gvRightSearch.Rows.Count) < Convert.ToInt32(txtTotalQuestions.Text.Trim()))
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please  Assessment Question greater than the Total no.of Questions selected ')</script>", false);

            return;
        }

        if (Session["dtParticiparticipate"] != null)
        {
            DataTable dt = Session["dtParticiparticipate"] as DataTable;
            if (dt.Rows.Count > 0)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Add Participants')</script>", false);

                return;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Add Participants')</script>", false);

            return;
        }
        if (Convert.ToInt32(AssessmentFor) == 1 || Convert.ToInt32(AssessmentFor) == 2)
        {
            if (Session["dtEntryDoneBY"] != null)
            {
                DataTable dt = Session["dtEntryDoneBY"] as DataTable;
                if (dt.Rows.Count > 0)
                {
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Add Entry Done By')</script>", false);

                    return;
                }

            }
            else
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Add Entry Done By')</script>", false);

                return;
            }
        }

        DataTable dtdin = ViewState["dtselected"] as DataTable;
        // DataTable distinctValues = dtdin.DefaultView.ToTable(true, "CategoryName");
        string CategoryName = "";
        string CategoryNameID = "";
        DataTable dtCategoryName = null;
        //foreach (GridViewRow row in gvRightSearch.Rows)
        //{

        //    CategoryName = gvRightSearch.DataKeys[row.RowIndex]["QuestionID"].ToString();
        //    CategoryNameID += "" + CategoryName + "" + ",";
        //}


        //if (CategoryNameID.Length > 0)
        //{
        //    CategoryNameID = CategoryNameID.Substring(0, CategoryNameID.LastIndexOf(","));

        //    dtCategoryName = objComman.LoadData("select distinct QCategoryID from MSTFormQuestion where formid in (select formid from MSTFormQuestion where QuestionID in ("+ CategoryNameID + ")) ");

        //}
        //if (dtCategoryName.Rows.Count== distinctValues.Rows.Count)
        //{

        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select question from all category.')</script>", false);

        //    return;
        //}
        Trainingtype = Convert.ToInt32(ddlLevel.SelectedValue);
        other = txtOthersName.Text;
        Location = txtLocation.Text;
        DateTime FromDate = Convert.ToDateTime(txtFromDate.Text);
        DateTime ToDate = Convert.ToDateTime(txtToDate.Text);
        EntryBy = Convert.ToString(Session["username"]);
        string Block = "", Dist = ""; string State = "";
        if (ddlState.SelectedIndex >= 0)
        {
            State = ddlState.SelectedValue;
        }
        if (ddlDistrictSearch.SelectedIndex >= 0)
        {
            Dist = ddlDistrictSearch.SelectedValue;
        }

        if (ddlMainBlock.SelectedIndex >= 0)
        {
            Block = ddlMainBlock.SelectedValue;
        }



        if (Convert.ToString(ViewState["Tarining_ID"]) == "")
        {
            if (Convert.ToInt32(AssessmentFor) == 1 || Convert.ToInt32(AssessmentFor) == 2)
            {
                if (ddlSchedue.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select  Scheduler')</script>", false);

                    return;
                }
            }
            int totalQ = 0;

            totalQ = Convert.ToInt32(txtTotalQuestions.Text);

            Tarining_ID = TrainingQuestionInsertUpdate(Tarining_ID, AssessmentFor, TrainingOutCome, SpecificTraining, AssessmentType, Trainingtype, other, Location, FromDate, ToDate, EntryBy, State, Dist, ddlTraingMode.SelectedValue, ddlTraining.SelectedValue, totalQ, ddlSchedue.SelectedValue);
            if (Tarining_ID > 0)
            {
                lblUni.Text = "";
                ViewState["Tarining_ID"] = Tarining_ID;

                #region question code 
                if (ddlLevel.SelectedValue != "0")
                {
                    int icount = 0, FormID = 0, QuestionID = 0, Assessment = 0, Sequence = 0;
                    string GUID = "Testxyz";

                    DataTable dtQuestion = NewgeneratedDT();
                    foreach (GridViewRow row in gvRightSearch.Rows)
                    {

                        QuestionID = Convert.ToInt32(gvRightSearch.DataKeys[row.RowIndex]["QuestionID"].ToString());
                        Assessment = Convert.ToInt32(ddlLevel.SelectedValue);
                        QuestionCategory = Convert.ToInt32(gvRightSearch.SelectedValue);
                        Sequence = Convert.ToInt32(gvRightSearch.DataKeys[row.RowIndex]["Sequence"].ToString());
                        //dtQuestion.Rows.Add(Assessment, QuestionCategory, QuestionID, Tarining_ID, Sequence);
                        //icount = icount + 1;

                        int icount4 = objMain.InsertUpdateAssment(ddlassement.SelectedValue, QuestionID.ToString(), Tarining_ID.ToString(), Sequence.ToString());
                    }
                    //DataTable dtFinal = dtQuestion;
                    //int Success = objMain.CopyFormQuestion(Assessment, QuestionCategory, QuestionID, Tarining_ID, Sequence, dtFinal);


                    DataTable dt = Session["dtParticiparticipate"] as DataTable;

                    DataTable dtparti = Session["dtParticiparticipate"] as DataTable;
                    if (dtparti.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtparti.Rows.Count; i++)
                        {
                            dt.Rows[i]["FormID"] = Tarining_ID;
                        }
                        int Parti_Success = Insert_USP_Participarticipate20252026(Tarining_ID, dtparti);
                    }




                    #endregion

                    #region entryDoneBY


                    DataTable dtentry = Session["dtEntryDoneBY"] as DataTable;
                    string UserID = "";
                    string UserName = "";
                    if (dtentry != null)
                    {



                        if (dtentry.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtentry.Rows.Count; i++)
                            {
                                dtentry.Rows[i]["FormID"] = Tarining_ID;
                                UserID += "'" + dtentry.Rows[i]["ParticiparticipateName"] + "'" + ",";
                                string jjj = dtentry.Rows[i]["ParticiparticipateName"] + "(" + dtentry.Rows[i]["EntryDoneByName"] + ")";
                                UserName += "'" + jjj + "'" + ",";


                            }
                            int Entry_Success = objMain.Insert_EntryDone(Tarining_ID, dtentry);


                        }

                        if (UserID.Length > 0)
                        {
                            UserID = UserID.Substring(0, UserID.LastIndexOf(","));
                        }
                        if (UserName.Length > 0)
                        {
                            UserName = UserName.Substring(0, UserName.LastIndexOf(","));
                        }





                        #endregion

                    }





                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                    GVMainBind();
                    GvQuestion.DataSource = null;
                    GvQuestion.DataBind();
                    FillScheduling();
                    txtFromDate.Enabled = false;
                    txtToDate.Enabled = false;
                    DataTable dtQuestion11 = new DataTable();
                    dtQuestion11 = Get_DataFor1Filter("SP_GetTrainingQuestionData", Tarining_ID.ToString());

                    if (dtQuestion11.Rows.Count > 0)
                    {

                        lblUni.Text = Convert.ToString(dtQuestion11.Rows[0]["GUIDTraining"]);
                        txtLink.Text = Convert.ToString(dtQuestion11.Rows[0]["Plink"]);
                        txtLink.Visible = true;
                        linkSurvey.Visible = true;
                        //BindgvRightSearch(ViewState["Tarining_ID"].ToString());
                        //BindGvQuestion(QuestionCategory);
                    }
                    if (Convert.ToInt32(ddlLevel.SelectedValue) == 1)
                    {
                        if (Convert.ToInt32(ddlassement.SelectedValue) == 1)
                        {

                            txtLink.Visible = true;

                        }
                        if (Convert.ToInt32(ddlassement.SelectedValue) == 2)
                        {
                            if (Convert.ToDateTime(txtToDate.Text) >= DateTime.Now)
                            {
                                txtLink.Visible = false;
                            }
                            else
                            {
                                txtLink.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        //if (Convert.ToInt32(ddlassement.SelectedValue) == 2)
                        //{

                        //    if (Convert.ToDateTime(txtToDate.Text) >= DateTime.Now)
                        //    {
                        //        txtLink.Visible = false;
                        //    }
                        //    else
                        //    {
                        //        txtLink.Visible = true;
                        //    }
                        //}
                    }
                    //BindgvRightSearch(ViewState["Tarining_ID"].ToString());
                    //BindGvQuestion(QuestionCategory);
                }
            }
        }
        else
        {
            int totalQ = 0;

            totalQ = Convert.ToInt32(txtTotalQuestions.Text);

            Tarining_ID = Convert.ToInt32(ViewState["Tarining_ID"].ToString());
            int Tarining_IDNew = TrainingQuestionInsertUpdate(Tarining_ID, AssessmentFor, TrainingOutCome, SpecificTraining, AssessmentType, Trainingtype, other, Location, FromDate, ToDate, EntryBy, State, Dist, Block, ddlTraining.SelectedValue, totalQ, "0");


            if (Tarining_ID > 0)
            {


                #region question code 
                if (ddlLevel.SelectedValue != "0")
                {
                    int icount = 0, FormID = 0, QuestionID = 0, Assessment = 0, Sequence = 0;
                    string GUID = "Testxyz";

                    int icount44 = objMain.DeleteAssment(Tarining_ID);


                    DataTable dtQuestion = NewgeneratedDT();
                    foreach (GridViewRow row in gvRightSearch.Rows)
                    {

                        QuestionID = Convert.ToInt32(gvRightSearch.DataKeys[row.RowIndex]["QuestionID"].ToString());
                        Assessment = Convert.ToInt32(ddlLevel.SelectedValue);
                        QuestionCategory = Convert.ToInt32(ddlCategory.SelectedValue);
                        Sequence = Convert.ToInt32(gvRightSearch.DataKeys[row.RowIndex]["Sequence"].ToString());
                        // dtQuestion.Rows.Add(Assessment, QuestionCategory, QuestionID, Tarining_ID, Sequence);

                        int icount4 = objMain.InsertUpdateAssment(ddlassement.SelectedValue, QuestionID.ToString(), Tarining_ID.ToString(), Sequence.ToString());

                    }
                    /// DataTable dtFinal = dtQuestion;


                    DataTable dt = Session["dtParticiparticipate"] as DataTable;

                    DataTable dtparti = Session["dtParticiparticipate"] as DataTable;
                    if (dtparti.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtparti.Rows.Count; i++)
                        {
                            dt.Rows[i]["FormID"] = Tarining_ID;
                        }
                        int Parti_Success = Insert_USP_Participarticipate20252026(Tarining_ID, dtparti);
                    }


                    DataTable dtentry = Session["dtEntryDoneBY"] as DataTable;
                    string UserID = "";
                    string UserName = "";

                    if (dtentry != null)
                    {
                        if (dtentry.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtentry.Rows.Count; i++)
                            {
                                dtentry.Rows[i]["FormID"] = Tarining_ID;
                                UserID += "'" + dtentry.Rows[i]["ParticiparticipateName"] + "'" + ",";
                                string jjj = dtentry.Rows[i]["ParticiparticipateName"] + "(" + dtentry.Rows[i]["EntryDoneByName"] + ")";
                                UserName += "'" + jjj + "'" + ",";


                            }
                        }
                        int Entry_Success = objMain.Insert_EntryDone(Tarining_ID, dtentry);
                    }
                    if (UserID.Length > 0)
                    {
                        UserID = UserID.Substring(0, UserID.LastIndexOf(","));
                    }
                    if (UserName.Length > 0)
                    {
                        UserName = UserName.Substring(0, UserName.LastIndexOf(","));
                    }


                    #endregion

                }




                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                GVMainBind();
                GvQuestion.DataSource = null;
                GvQuestion.DataBind();
                //BindgvRightSearch(ViewState["Tarining_ID"].ToString());
                //BindGvQuestion(QuestionCategory);
            }

        }


    }
    public int Insert_participate(int FormID, DataTable tbl_Tarining_Participarticipate)
    {
        DataTable dtcombo = new DataTable();

        SqlParameter[] cmdParameters = new SqlParameter[]
    {
            new SqlParameter("@FormID", FormID),
            new SqlParameter("@tbl_Tarining_Participarticipate", tbl_Tarining_Participarticipate)
    };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[USP_Participarticipate2023]", cmdParameters);

    }
    public int Insert_USP_Participarticipate20252026(int FormID, DataTable tbl_Tarining_Participarticipate)
    {
        DataTable dtcombo = new DataTable();

        SqlParameter[] cmdParameters = new SqlParameter[]
    {
            new SqlParameter("@FormID", FormID),
            new SqlParameter("@tbl_Tarining_Participarticipate", tbl_Tarining_Participarticipate)
    };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[USP_Participarticipate20252026]", cmdParameters);

    }

    public int InsertUpdateStaffScheduling2023(int Tarining_ID, int AssessmentFor, int TrainingOutCome, int SpecificTraining, int AssessmentType, int Trainingtype, string other, string Location, DateTime FromDate, DateTime ToDate, string EntryBy, string State, string Dist, string Block, string UserID, string Username, string SurveyLink)
    {
        SqlCommand dbSqlCommand;
        using (dbSqlCommand = new SqlCommand())
            dbSqlCommand.Connection = mycon;
        if (mycon.State == ConnectionState.Closed)
            mycon.Open();
        dbSqlCommand.CommandType = CommandType.StoredProcedure;
        dbSqlCommand.CommandText = "InsertUpdateStaffScheduling20262027";
        dbSqlCommand.Parameters.Add("@Tarining_ID", SqlDbType.Int).Value = Tarining_ID;
        dbSqlCommand.Parameters.Add("@AssessmentFor", SqlDbType.Int).Value = AssessmentFor;
        dbSqlCommand.Parameters.Add("@TrainingOutCome", SqlDbType.Int).Value = TrainingOutCome;
        dbSqlCommand.Parameters.Add("@SpecificTraining", SqlDbType.VarChar).Value = SpecificTraining;
        dbSqlCommand.Parameters.Add("@AssessmentType", SqlDbType.Int).Value = AssessmentType;
        dbSqlCommand.Parameters.Add("@Trainingtype", SqlDbType.Int).Value = Trainingtype;
        dbSqlCommand.Parameters.Add("@Other", SqlDbType.VarChar).Value = other;
        dbSqlCommand.Parameters.Add("@Location", SqlDbType.VarChar).Value = Location;
        dbSqlCommand.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = FromDate;
        dbSqlCommand.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = ToDate;
        dbSqlCommand.Parameters.Add("@EntryBy", SqlDbType.VarChar).Value = EntryBy;
        dbSqlCommand.Parameters.Add("@State", SqlDbType.VarChar).Value = State;
        dbSqlCommand.Parameters.Add("@Dist", SqlDbType.VarChar).Value = Dist;
        dbSqlCommand.Parameters.Add("@Block", SqlDbType.VarChar).Value = Block;
        dbSqlCommand.Parameters.Add("@UserID", SqlDbType.VarChar).Value = UserID;
        dbSqlCommand.Parameters.Add("@UserName", SqlDbType.VarChar).Value = Username;
        dbSqlCommand.Parameters.Add("@SurveyLink", SqlDbType.VarChar).Value = SurveyLink;
        dbSqlCommand.Parameters.Add("@Createby", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);


        System.Data.SqlClient.SqlParameter pRowsAffected = new SqlParameter("@output", System.Data.SqlDbType.Int);
        pRowsAffected.Direction = System.Data.ParameterDirection.Output;
        dbSqlCommand.Parameters.Add(pRowsAffected);
        try
        {
            dbSqlCommand.ExecuteNonQuery();
        }
        catch
        {
            return -1;
        }
        return Convert.ToInt32(pRowsAffected.Value);
    }
    public int InsertUpdateStaffScheduling2026(int Tarining_ID, int AssessmentFor, int TrainingOutCome, int SpecificTraining, int AssessmentType, int Trainingtype, string other, string Location, DateTime FromDate, DateTime ToDate, string EntryBy, string State, string Dist, string Block, string UserID, string Username, string SurveyLink)
    {
        SqlCommand dbSqlCommand;
        using (dbSqlCommand = new SqlCommand())
            dbSqlCommand.Connection = mycon;
        if (mycon.State == ConnectionState.Closed)
            mycon.Open();
        dbSqlCommand.CommandType = CommandType.StoredProcedure;
        dbSqlCommand.CommandText = "InsertUpdateStaffScheduling2026";
        dbSqlCommand.Parameters.Add("@Tarining_ID", SqlDbType.Int).Value = Tarining_ID;
        dbSqlCommand.Parameters.Add("@AssessmentFor", SqlDbType.Int).Value = AssessmentFor;
        dbSqlCommand.Parameters.Add("@TrainingOutCome", SqlDbType.Int).Value = TrainingOutCome;
        dbSqlCommand.Parameters.Add("@SpecificTraining", SqlDbType.VarChar).Value = SpecificTraining;
        dbSqlCommand.Parameters.Add("@AssessmentType", SqlDbType.Int).Value = AssessmentType;
        dbSqlCommand.Parameters.Add("@Trainingtype", SqlDbType.Int).Value = Trainingtype;
        dbSqlCommand.Parameters.Add("@Other", SqlDbType.VarChar).Value = other;
        dbSqlCommand.Parameters.Add("@Location", SqlDbType.VarChar).Value = Location;
        dbSqlCommand.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = FromDate;
        dbSqlCommand.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = ToDate;
        dbSqlCommand.Parameters.Add("@EntryBy", SqlDbType.VarChar).Value = EntryBy;
        dbSqlCommand.Parameters.Add("@State", SqlDbType.VarChar).Value = State;
        dbSqlCommand.Parameters.Add("@Dist", SqlDbType.VarChar).Value = Dist;
        dbSqlCommand.Parameters.Add("@Block", SqlDbType.VarChar).Value = Block;
        dbSqlCommand.Parameters.Add("@UserID", SqlDbType.VarChar).Value = UserID;
        dbSqlCommand.Parameters.Add("@UserName", SqlDbType.VarChar).Value = Username;
        dbSqlCommand.Parameters.Add("@SurveyLink", SqlDbType.VarChar).Value = SurveyLink;
        dbSqlCommand.Parameters.Add("@Createby", SqlDbType.VarChar).Value = Convert.ToString(Session["username"]);
        System.Data.SqlClient.SqlParameter pRowsAffected = new SqlParameter("@output", System.Data.SqlDbType.Int);
        pRowsAffected.Direction = System.Data.ParameterDirection.Output;
        dbSqlCommand.Parameters.Add(pRowsAffected);
        try
        {
            dbSqlCommand.ExecuteNonQuery();
        }
        catch
        {
            return -1;
        }
        return Convert.ToInt32(pRowsAffected.Value);
    }

    public int TrainingQuestionInsertUpdate(int Tarining_ID, int AssessmentFor, int TrainingOutCome, int SpecificTraining, int AssessmentType, int Trainingtype, string other, string Location, DateTime FromDate, DateTime ToDate, string EntryBy, string State, string Dist, string Block, string TrainingTypeID, int TotalTraningQuestion, string Schedulerid)
    {
        SqlCommand dbSqlCommand;
        using (dbSqlCommand = new SqlCommand())
            dbSqlCommand.Connection = mycon;
        if (mycon.State == ConnectionState.Closed)
            mycon.Open();
        dbSqlCommand.CommandType = CommandType.StoredProcedure;
        dbSqlCommand.CommandText = "USP_Training_QuestionInsert2026";
        dbSqlCommand.Parameters.Add("@Tarining_ID", SqlDbType.Int).Value = Tarining_ID;
        dbSqlCommand.Parameters.Add("@AssessmentFor", SqlDbType.Int).Value = AssessmentFor;
        dbSqlCommand.Parameters.Add("@TrainingOutCome", SqlDbType.Int).Value = TrainingOutCome;
        dbSqlCommand.Parameters.Add("@SpecificTraining", SqlDbType.VarChar).Value = SpecificTraining;
        dbSqlCommand.Parameters.Add("@AssessmentType", SqlDbType.Int).Value = AssessmentType;
        dbSqlCommand.Parameters.Add("@Trainingtype", SqlDbType.Int).Value = Trainingtype;
        dbSqlCommand.Parameters.Add("@Other", SqlDbType.VarChar).Value = other;
        dbSqlCommand.Parameters.Add("@Location", SqlDbType.VarChar).Value = Location;
        dbSqlCommand.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = FromDate;
        dbSqlCommand.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = ToDate;
        dbSqlCommand.Parameters.Add("@EntryBy", SqlDbType.VarChar).Value = EntryBy;
        dbSqlCommand.Parameters.Add("@State", SqlDbType.VarChar).Value = State;
        dbSqlCommand.Parameters.Add("@Dist", SqlDbType.VarChar).Value = Dist;
        dbSqlCommand.Parameters.Add("@Block", SqlDbType.VarChar).Value = Block;
        dbSqlCommand.Parameters.Add("@TrainingTypeID", SqlDbType.VarChar).Value = TrainingTypeID;
        dbSqlCommand.Parameters.Add("@TotalTraningQuestion", SqlDbType.Int).Value = TotalTraningQuestion;
        dbSqlCommand.Parameters.Add("@MainId", SqlDbType.Int).Value = ddlMainID.SelectedValue;
        dbSqlCommand.Parameters.Add("@Schedulerid", SqlDbType.Int).Value = Schedulerid;
        System.Data.SqlClient.SqlParameter pRowsAffected = new SqlParameter("@output", System.Data.SqlDbType.Int);
        pRowsAffected.Direction = System.Data.ParameterDirection.Output;
        dbSqlCommand.Parameters.Add(pRowsAffected);
        try
        {
            dbSqlCommand.ExecuteNonQuery();
        }
        catch
        {
            return -1;
        }
        return Convert.ToInt32(pRowsAffected.Value);
    }

    public int TrainingQuestionInsertCopy(int Tarining_ID, int AssessmentFor, int TrainingOutCome, int SpecificTraining, int AssessmentType, int Trainingtype, string other, string Location, DateTime FromDate, DateTime ToDate, string EntryBy, string State, string Dist, string Block, string TrainingTypeID, int MainID)
    {
        SqlCommand dbSqlCommand;
        using (dbSqlCommand = new SqlCommand())
            dbSqlCommand.Connection = mycon;
        if (mycon.State == ConnectionState.Closed)
            mycon.Open();
        dbSqlCommand.CommandType = CommandType.StoredProcedure;
        dbSqlCommand.CommandText = "USP_Training_QuestionCopy2025";
        dbSqlCommand.Parameters.Add("@Tarining_ID", SqlDbType.Int).Value = Tarining_ID;
        dbSqlCommand.Parameters.Add("@AssessmentFor", SqlDbType.Int).Value = AssessmentFor;
        dbSqlCommand.Parameters.Add("@TrainingOutCome", SqlDbType.Int).Value = TrainingOutCome;
        dbSqlCommand.Parameters.Add("@SpecificTraining", SqlDbType.VarChar).Value = SpecificTraining;
        dbSqlCommand.Parameters.Add("@AssessmentType", SqlDbType.Int).Value = AssessmentType;
        dbSqlCommand.Parameters.Add("@Trainingtype", SqlDbType.Int).Value = Trainingtype;
        dbSqlCommand.Parameters.Add("@Other", SqlDbType.VarChar).Value = other;
        dbSqlCommand.Parameters.Add("@Location", SqlDbType.VarChar).Value = Location;
        dbSqlCommand.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = FromDate;
        dbSqlCommand.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = ToDate;
        dbSqlCommand.Parameters.Add("@EntryBy", SqlDbType.VarChar).Value = EntryBy;
        dbSqlCommand.Parameters.Add("@State", SqlDbType.VarChar).Value = State;
        dbSqlCommand.Parameters.Add("@Dist", SqlDbType.VarChar).Value = Dist;
        dbSqlCommand.Parameters.Add("@Block", SqlDbType.VarChar).Value = Block;
        dbSqlCommand.Parameters.Add("@TotalTraningQuestion", SqlDbType.Int).Value = txtTotalQuestions.Text;
        dbSqlCommand.Parameters.Add("@TrainingTypeID", SqlDbType.VarChar).Value = TrainingTypeID;
        dbSqlCommand.Parameters.Add("@MainID", SqlDbType.Int).Value = MainID;


        System.Data.SqlClient.SqlParameter pRowsAffected = new SqlParameter("@output", System.Data.SqlDbType.Int);
        pRowsAffected.Direction = System.Data.ParameterDirection.Output;
        dbSqlCommand.Parameters.Add(pRowsAffected);
        try
        {
            dbSqlCommand.ExecuteNonQuery();
        }
        catch
        {
            return -1;
        }
        return Convert.ToInt32(pRowsAffected.Value);
    }


    private void GVMainBind()
    {


        string text = "";
        //"where AssessmentFor='" + this.ddlLevel.SelectedValue + "'";
        if (ddlDistrictSearch.SelectedIndex > 0)
        {
            text = " where DistCode ='" + ddlDistrictSearch.SelectedValue + "'";
        }

        SqlParameter[] cmdParameters = new SqlParameter[]
          {

                new SqlParameter("@Dist",ddlDistrictSearch.SelectedValue),



          };



        DataTable dtTb = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTrainngAssement]", cmdParameters);

        // DataTable dtTb = objMain.LoadData("SELECT Tarining_ID,case when [AssessmentType]= 1 then 'B' + CONVERT(varchar, Tarining_ID ) when [AssessmentType]= 2 then 'E' + CONVERT(varchar, Tarining_ID ) else ''   end[BatchID], GUIDTraining,(case when [AssessmentFor]= 1 then  mstOutcomeSpecific.sOutcomeName when [AssessmentFor]= 2 then mstlearning.learningName   when [AssessmentFor]= 3 then Other 		when [AssessmentFor]= 4 then Other else '' end)   AssessmentFor, convert(varchar(10),[FromDate], 121) as [FromDate], convert(varchar(10), todate, 121) as Todate,'https://testpms.educategirls.ngo/SurveyAns.aspx?ID='+GUIDTraining as Plink  FROM tbl_training_question left join(select LookupCode id, Description Value  From mstLookup where LookupFlag = 'Sur') a on a.id = tbl_training_question.AssessmentFor left join mstOutcomeSpecific on mstOutcomeSpecific.sOutcomeID=[SpecificTraining]   left join mstlearning on mstlearning.learningID=TrainingOutcome " + text + " and  convert(int, isnull(TdeleteFlag,0))<>2 order by Createdate desc ");

        if (dtTb.Rows.Count > 0)
        {
            ViewState["Serach"] = dtTb;
            GVMain.DataSource = dtTb;
            GVMain.DataBind();
        }
        else
        {
            GVMain.DataSource = null;
            GVMain.DataBind();
        }
    }

    private void gvRightBind()
    {


        string text = "where AssessmentFor='" + this.ddlLevel.SelectedValue + "'";
        if (this.ddlLearning.SelectedValue != null && this.ddlLearning.SelectedIndex > 0)
        {
            text = text + "and fromdate='" + this.txtFromDate.Text.ToString() + "'";
        }
        if (this.ddlTraining.SelectedValue != null && this.ddlTraining.SelectedIndex > 0)
        {
            text = text + " and Todate='" + this.txtToDate.Text.ToString() + "'";
        }

        DataTable dtTb = objMain.LoadData("SELECT Tarining_ID, GUIDTraining,a.Value AssessmentFor, convert(varchar(10),[FromDate], 121) as [FromDate], convert(varchar(10), todate, 121) as Todate  FROM tbl_training_question left join(select LookupCode id, Description Value  From mstLookup where LookupFlag = 'Sur') a on a.id = tbl_training_question.AssessmentFor  where  " + text + " order by Createdate desc ");
        if (dtTb.Rows.Count > 0)
        {
            gvRightSearch.DataSource = dtTb;
            gvRightSearch.DataBind();
        }
        else
        {
            gvRightSearch.DataSource = null;
            gvRightSearch.DataBind();
        }
    }

    protected void btnprevone_onclick(object sender, EventArgs e)
    {
        try
        {
            DataTable dtselect = (DataTable)ViewState["dtselect"];
            DataTable dtselected = (DataTable)ViewState["dtselected"];
            if (ViewState["dtselected"] != null)
            {

            }
            else
            {
                dtselected = dtselect.Clone();
            }

            DataRow dr;

            int icount = 0, FormID = 0, QuestionID = 0, QuestionCategory = 0, Assessment = 0, Sequence = 0;
            int tmp = 0;

            DataTable dtQuestion = NewgeneratedDT();
            foreach (GridViewRow row in GvQuestion.Rows)
            {
                int ind = row.DataItemIndex;

                CheckBox Chkbox = ((CheckBox)row.FindControl("chkFormName"));
                if (Chkbox.Checked == true)
                {
                    DataRow[] dr1 = dtselected.Select("QuestionID='" + GvQuestion.DataKeys[row.RowIndex]["QuestionID"].ToString() + "'");
                    if (dr1.Length > 0)
                    {
                    }
                    else
                    {
                        dr = dtselected.NewRow();
                        dr["QuestionNo"] = GvQuestion.DataKeys[row.RowIndex]["QuestionNo"].ToString();
                        dr["QuestionID"] = GvQuestion.DataKeys[row.RowIndex]["QuestionID"].ToString();
                        dr["Sequence"] = GvQuestion.DataKeys[row.RowIndex]["Sequence"].ToString();
                        dr["Question"] = GvQuestion.DataKeys[row.RowIndex]["Question"].ToString();
                        dr["FormID"] = GvQuestion.DataKeys[row.RowIndex]["FormID"].ToString();
                        dr["CategoryName"] = GvQuestion.DataKeys[row.RowIndex]["CategoryName"].ToString();
                        dtselected.Rows.Add(dr);
                    }
                    dtselect.Rows.RemoveAt(ind - tmp);

                    //QuestionID = Convert.ToInt32(GvQuestion.DataKeys[row.RowIndex]["QuestionID"].ToString());

                    ////FormID = Convert.ToInt32(ViewState["Tarining_ID"]);
                    //Assessment = Convert.ToInt32(ddlLevel.SelectedValue);
                    //QuestionCategory = Convert.ToInt32(ddlCategory.SelectedValue);
                    //Sequence = Convert.ToInt32(GvQuestion.DataKeys[row.RowIndex]["Sequence"].ToString());
                    //dtQuestion.Rows.Add(Assessment, QuestionCategory, QuestionID, FormID, Sequence);
                    icount = icount + 1;
                    tmp++;
                }
            }


            if (icount > 0)
            {
                gvRightSearch.DataSource = dtselected;
                gvRightSearch.DataBind();
                ViewState["dtselected"] = dtselected;
                lblTotal.Text = dtselected.Rows.Count.ToString();
                GvQuestion.DataSource = dtselect;
                GvQuestion.DataBind();
                ViewState["dtselect"] = dtselect;
                //showMessages("Question copied Successfully");
            }
            else
            {
                // showMessages("Please select Question");
            }


        }
        catch
        {


        }

    }


    protected void btnnextone_onclick(object sender, EventArgs e)
    {
        try
        {

            DataTable dtselect = (DataTable)ViewState["dtselect"];
            DataTable dtselected = (DataTable)ViewState["dtselected"];



            int icount = 0, deleteSuccess = 0, FormID = 0, QuestionID = 0, QuestionCategory = 0, Assessment = 0, Sequence = 0;
            DataRow dr;
            DataTable dtQuestion = NewgeneratedDT();
            int tmp = 0;
            foreach (GridViewRow row in gvRightSearch.Rows)
            {
                int ind = row.DataItemIndex;

                CheckBox Chkbox = ((CheckBox)row.FindControl("ChkHR"));
                if (Chkbox.Checked == true)
                {
                    if (dtselect != null)
                    {
                        DataRow[] dr1 = dtselect.Select("QuestionID='" + gvRightSearch.DataKeys[row.RowIndex]["QuestionID"].ToString() + "' and FormID='" + gvRightSearch.DataKeys[row.RowIndex]["FormID"].ToString() + "'");
                        if (dr1.Length > 0)
                        {
                        }
                        else
                        {
                            //if (Convert.ToInt32(ddlCategory.SelectedValue) == Convert.ToInt32(gvRightSearch.DataKeys[row.RowIndex]["FormID"].ToString()))
                            //{
                            dr = dtselect.NewRow();
                            dr["QuestionNo"] = gvRightSearch.DataKeys[row.RowIndex]["QuestionNo"].ToString();
                            dr["QuestionID"] = gvRightSearch.DataKeys[row.RowIndex]["QuestionID"].ToString();
                            dr["Sequence"] = gvRightSearch.DataKeys[row.RowIndex]["Sequence"].ToString();
                            dr["Question"] = gvRightSearch.DataKeys[row.RowIndex]["Question"].ToString();
                            dr["FormID"] = gvRightSearch.DataKeys[row.RowIndex]["FormID"].ToString();
                            dr["CategoryName"] = gvRightSearch.DataKeys[row.RowIndex]["CategoryName"].ToString();
                            dtselect.Rows.Add(dr);
                            //}
                        }

                    }
                    else
                    {
                        dtselect = dtselected.Clone();
                        dr = dtselect.NewRow();
                        dr["QuestionNo"] = gvRightSearch.DataKeys[row.RowIndex]["QuestionNo"].ToString();
                        dr["QuestionID"] = gvRightSearch.DataKeys[row.RowIndex]["QuestionID"].ToString();
                        dr["Sequence"] = gvRightSearch.DataKeys[row.RowIndex]["Sequence"].ToString();
                        dr["Question"] = gvRightSearch.DataKeys[row.RowIndex]["Question"].ToString();
                        dr["FormID"] = gvRightSearch.DataKeys[row.RowIndex]["FormID"].ToString();
                        dr["CategoryName"] = gvRightSearch.DataKeys[row.RowIndex]["CategoryName"].ToString();
                        dtselect.Rows.Add(dr);
                    }


                    dtselected.Rows.RemoveAt(ind - tmp);
                    //QuestionID = Convert.ToInt32(GvQuestion.DataKeys[row.RowIndex]["QuestionID"].ToString());

                    ////FormID = Convert.ToInt32(ViewState["Tarining_ID"]);
                    //Assessment = Convert.ToInt32(ddlLevel.SelectedValue);
                    //QuestionCategory = Convert.ToInt32(ddlCategory.SelectedValue);
                    //Sequence = Convert.ToInt32(GvQuestion.DataKeys[row.RowIndex]["Sequence"].ToString());
                    //dtQuestion.Rows.Add(Assessment, QuestionCategory, QuestionID, FormID, Sequence);
                    icount = icount + 1;
                    tmp++;
                }
            }
            if (icount > 0)
            {
                gvRightSearch.DataSource = dtselected;
                gvRightSearch.DataBind();
                ViewState["dtselected"] = dtselected;
                lblTotal.Text = dtselected.Rows.Count.ToString();
                GvQuestion.DataSource = dtselect;
                GvQuestion.DataBind();
                ViewState["dtselect"] = dtselect;
                //showMessages("Question copied Successfully");
            }
        }




        catch
        {

        }
    }
    private void showMessages(string messages)
    {
        lbl_messages.Text = "";
        lbl_messages.Text = messages;
        ModalAlert.Show();
    }

    #region anuj 

    protected void ddlLearning_SelectedIndexChanged(object sender, EventArgs e)
    {
        int FormLevel = Int32.Parse(ddlLevel.SelectedValue.ToString());
        if (FormLevel == 1)
        {
            LoadOutComeSpicify();
        }
        FillFormName(FormLevel);
    }
    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        int FormLevel = Int32.Parse(ddlLevel.SelectedValue.ToString());
        Label2.Text = "";
        d1.Visible = false;
        d2.Visible = false;
        LnkEntry.Visible = false;
        divother.Visible = true;
        div7.Visible = true;
        divother.Attributes.Remove("style");
        if (FormLevel == 1)
        {
            divother.Attributes.Add("style", "margin-left: -280px;margin-top:11px;");
            d1.Visible = true;
            d2.Visible = true;
            lblother.Visible = false;
            txtOthersName.Visible = false;
            divassemnt.Visible = true;
            //ddlassement.Enabled = true;
            ddlassement.SelectedValue = "1";
            LoadOutCome();
            Label2.Text = "Specific Training :";
            Filllearning();
            LoadOutComeSpicify();
            LnkEntry.Visible = true;

        }
        if (FormLevel == 2)
        {
            divother.Visible = true;
            Label2.Text = "Training OutCome :";

            d1.Visible = false;
            d2.Visible = true;
            lblother.Visible = false;
            txtOthersName.Visible = false;
            divassemnt.Visible = true;
            ddlassement.Enabled = false;
            ddlassement.SelectedValue = "2";
            Filllearning();
            LnkEntry.Visible = true;
        }
        if (FormLevel == 3)
        {

            lblother.Visible = true;
            txtOthersName.Visible = true;
            d1.Visible = false;
            d2.Visible = false;
            divassemnt.Visible = false;

        }
        if (FormLevel == 4)
        {
            lblother.Visible = true;
            txtOthersName.Visible = true;
            divassemnt.Visible = false;
        }



    }
    protected void ddlTraingOutcome_SelectedIndexChanged(object sender, EventArgs e)
    {
        int FormLevel = Int32.Parse(ddlLevel.SelectedValue.ToString());
        if (FormLevel == 1)
        {

        }
        if (FormLevel == 2)
        {

        }

        FillFormName(FormLevel);
    }
    public void LoadOutCome()
    {
        string conditions = "  ActiveStatus=1";

        objComman.BindDLL("mstOutcome", "OutcomeID,OutcomeName ", conditions, "OutcomeName", "asc", ddlLearning, "OutcomeName", "OutcomeID", "--Select--");

        ddlLearning.SelectedIndex = 0;


    }
    public void LoadOutComeSpicify()
    {
        string conditions = " ";

        objComman.BindDLL("mstOutcomeSpecific", "sOutcomeID,sOutcomeName ", "OutcomeID=" + ddlLearning.SelectedValue + " and ActiveStatus=1", "sOutcomeID", "asc", ddlTraingOutcome, "sOutcomeName", "sOutcomeID", "--Select--");

        ddlTraingOutcome.SelectedIndex = 0;


    }
    private void FillDropdownPre()
    {

        DataTable dt1 = Exec_Procedure("USP_GetLevel2026");
        ddlLevel.DataSource = dt1;
        ddlLevel.DataValueField = "id";
        ddlLevel.DataTextField = "Value";
        ddlLevel.DataBind();
        ddlLevel.Items.Insert(0, new System.Web.UI.WebControls.ListItem("---Select Level---", "0"));
        FillTrainingType();
    }
    private void FillDropdown()
    {

        DataTable dt1 = Exec_Procedure("USP_GetLevel");
        ddlLevel.DataSource = dt1;
        ddlLevel.DataValueField = "id";
        ddlLevel.DataTextField = "Value";
        ddlLevel.DataBind();
        ddlLevel.Items.Insert(0, new System.Web.UI.WebControls.ListItem("---Select Level---", "0"));
        FillTrainingType();

    }
    public DataTable Exec_Procedure(string ProcedureName)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
                    {

                    };
            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, ProcedureName, paramvT);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch (Exception)
        {

        }
        return dtcombo;
    }
    protected void ddlDataBound(object sender, EventArgs e)
    {
        DropDownList list = sender as DropDownList;
        if (list != null)
            list.Items.Insert(0, new ListItem("------Select-------", "0"));

    }

    public void FillFormName(int FormLevel)
    {
        DataTable dt = new DataTable();
        //int FormLevel;
        int T1 = 0;
        int T2 = 0;
        if (FormLevel == 1)
        {
            if (ddlTraingOutcome.SelectedIndex > 0)
            {
                T1 = Convert.ToInt32(ddlTraingOutcome.SelectedValue);
            }
            if (ddlLearning.SelectedIndex > 0)
            {
                T2 = Convert.ToInt32(ddlLearning.SelectedValue);
            }
        }
        if (FormLevel == 2)
        {
            if (ddlTraingOutcome.SelectedIndex > 0)
            {
                T1 = Convert.ToInt32(ddlTraingOutcome.SelectedValue);
            }
        }

        if (FormLevel != 0 || FormLevel != -1)
        {
            //dt = objBLL.Get_DataFor1Filter()
            dt = GetFormTableDetails(FormLevel, T1, T2);
        }
        else
        {
            //dt = objBLL.Select_All_Data("MSTForm", "FormLevel,FormID,FormName", "IsDeleted = 0 and FormLevel = " + FormLevel + " ", "", "");
        }



    }

    public DataTable GetFormTableDetails(int FormLevel, int T1, int T2)
    {
        DataTable dtBSL = new DataTable();
        dtBSL = null;
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                         new SqlParameter("@FormLevel",FormLevel),
                          new SqlParameter("@T1",T1),
                           new SqlParameter("@T2",T2),
                    };
            DataTable ds = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Form_Table_Deatils", paramvT);
            dtBSL = ds;
        }
        catch
        { DataTable ds = new DataTable(); ds = null; return ds; }
        return dtBSL;
    }
    protected void BindGvQuestion(int FormID)
    {

        DataTable dtQuestion = new DataTable();
        dtQuestion = Get_DataFor1Filter("USP_GetMSTtrainingQuestionLeft2024", FormID.ToString());

        if (dtQuestion.Rows.Count > 0)
        {
            GvQuestion.DataSource = dtQuestion;
            GvQuestion.DataBind();
        }
        else
        {
            GvQuestion.DataSource = null;
            GvQuestion.DataBind();
        }
        Session["Main"] = dtQuestion;
        ViewState["dtselect"] = dtQuestion;
    }


    protected void BindgvRightSearch(string FormID)
    {

        DataTable dtQuestion2 = new DataTable();
        dtQuestion2 = Get_DataFor1Filter("USP_GetMSTFormQuestionOnForm2", FormID.ToString());

        if (dtQuestion2.Rows.Count > 0)
        {
            //ddlCategory.SelectedValue = Convert.ToString(dtQuestion2.Rows[0]["QuestionCategory"]);
            gvRightSearch.DataSource = dtQuestion2;
            gvRightSearch.DataBind();

        }
        else
        {
            gvRightSearch.DataSource = null;
            gvRightSearch.DataBind();
        }
    }
    public DataTable Get_DataFor1Filter(string ProcedureName, string Filter1)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                            new SqlParameter("@Filter1",Filter1),
                    };
            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, ProcedureName, paramvT);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch (Exception)
        {

        }
        return dtcombo;
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
    public void ClearField()
    {

    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        int FormID = Int32.Parse(ddlCategory.SelectedValue);
        BindGvQuestion(FormID);

    }

    protected void GVMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVMain.PageIndex = e.NewPageIndex;
        if (ViewState["Serach"] != null)
        {
            DataTable dt = ViewState["Serach"] as DataTable;
            GVMain.DataSource = dt;
            GVMain.DataBind();
        }

    }

    protected void GVMain_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "GVMainEdit")
            {

                int iIndex = Convert.ToInt32(e.CommandArgument);
                string Tarining_ID = (GVMain.DataKeys[iIndex]["Tarining_ID"].ToString());

                string Todate = (GVMain.DataKeys[iIndex]["Todate"].ToString());
                ViewState["Tarining_ID"] = Tarining_ID;
                FillDropdown();
                EditBind(Tarining_ID, Todate);

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
        catch
        {

        }

    }
    public void EditBind(string Tarining_ID, string Modate)
    {

        DataTable dtQuestion = new DataTable();
        dtQuestion = Get_DataFor1Filter("SP_GetTrainingQuestionData", Tarining_ID);
        string strQry = "Select * from [FormEvaluation2026]  where FormID=" + Tarining_ID + "   ";
        clsMain obm = new clsMain();
        DataTable dtRole = obm.LoadData(strQry);
        if (dtRole.Rows.Count > 0)
        {
            btnsave.Enabled = false;
        }
        else
        {
            btnsave.Enabled = true;
        }


        if (dtQuestion.Rows.Count > 0)
        {
            lnkCopy.Visible = false;
            linkSurvey.Visible = true;
            txtLink.Visible = true;
            LnkEntry.Visible = false;
            LinkButton1.Visible = false;
            lblUni.Text = Convert.ToString(dtQuestion.Rows[0]["GUIDTraining"]);
            txtLink.Text = Convert.ToString(dtQuestion.Rows[0]["Plink"]);
            txtTotalQuestions.Text = Convert.ToString(dtQuestion.Rows[0]["TotalTraningQuestion"]);
            ddlLevel.SelectedValue = Convert.ToString(dtQuestion.Rows[0]["AssessmentFor"]);

            ddlLevel_SelectedIndexChanged(ddlLevel, null);
            if (Convert.ToString(dtQuestion.Rows[0]["MainId"]) != "0")
            {
                ddlMainID.SelectedValue = Convert.ToString(dtQuestion.Rows[0]["MainId"]);
            }
            else
            {
                ddlMainID.ClearSelection();
            }
            if (Convert.ToString(dtQuestion.Rows[0]["AssessmentFor"]) == "1")
            {
                if (Convert.ToString(Session["username"]) == "PMSAdmin" || Convert.ToString(Session["username"]) == "SuperAdmin")
                {
                    LinkButton1.Visible = false;
                }


                d1.Visible = true;
                d2.Visible = true;
                lblother.Visible = false;
                txtOthersName.Visible = false;
                divassemnt.Visible = true;
                LnkEntry.Visible = true;
                ddlassement.SelectedValue = "1";
                Label2.Text = "Specific Training";
                if (Convert.ToString(dtQuestion.Rows[0]["AssessmentType"]) == "1")
                {
                    string strQry1 = "Select isnull(RefTarining_ID,0) RefTarining_ID from [tbl_training_question]  where RefTarining_ID=" + Tarining_ID + "   ";

                    DataTable dtRole1 = obm.LoadData(strQry1);
                    if (dtRole1.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dtRole1.Rows[0]["RefTarining_ID"]) > 0)
                        {
                            lnkCopy.Visible = false;
                        }
                        else
                        {
                            lnkCopy.Visible = true;
                        }
                    }
                    else
                    {
                        lnkCopy.Visible = true;
                    }
                }
                else
                {
                    lnkCopy.Visible = false;

                }

                txtFromDate.Enabled = false;
                txtToDate.Enabled = false;
                ddlDistrictSearch.Enabled = false;
                ddlState.Enabled = false;
                txtLocation.Enabled = false;
                ddlTraingMode.Enabled = false;
                ddlLevel.Enabled = false;
                ddlLearning.Enabled = false;
                ddlTraingOutcome.Enabled = false;
                ddlTraining.Enabled = false;
            }
            else if (Convert.ToString(dtQuestion.Rows[0]["AssessmentFor"]) == "2")
            {
                lnkCopy.Visible = false;

                d1.Visible = false;
                d2.Visible = true;
                lblother.Visible = false;
                txtOthersName.Visible = false;
                divassemnt.Visible = true;
                ddlassement.Enabled = false;
                ddlassement.SelectedValue = "2";
                txtFromDate.Enabled = false;
                txtToDate.Enabled = false;
                ddlDistrictSearch.Enabled = false;
                ddlState.Enabled = false;
                txtLocation.Enabled = false;
                ddlTraingMode.Enabled = false;
                ddlLevel.Enabled = false;
                ddlLearning.Enabled = false;
                ddlTraingOutcome.Enabled = false;
                ddlTraining.Enabled = false;
            }
            else if (Convert.ToString(dtQuestion.Rows[0]["AssessmentFor"]) == "3")
            {
                lblother.Visible = true;
                txtOthersName.Visible = true;
                d1.Visible = false;
                d2.Visible = false;
                divassemnt.Visible = false;
                lnkCopy.Visible = false;


            }
            else if (Convert.ToString(dtQuestion.Rows[0]["AssessmentFor"]) == "4")
            {
                lblother.Visible = true;
                txtOthersName.Visible = true;
                divassemnt.Visible = false;
                lnkCopy.Visible = false;

            }

            if (dtQuestion.Rows[0]["Year"].ToString() != "")
            {
                ddlYear.SelectedValue = Convert.ToString(dtQuestion.Rows[0]["Year"]);
            }
            if (dtQuestion.Rows[0]["StateCode"].ToString() != "")
            {
                ddlState.SelectedValue = Convert.ToString(dtQuestion.Rows[0]["StateCode"]);
                ddlState_SelectedIndexChanged(ddlState, null);
            }
            if (dtQuestion.Rows[0]["DistCode"].ToString() != "")
            {
                ddlDistrictSearch.SelectedValue = Convert.ToString(dtQuestion.Rows[0]["DistCode"]);
                ddlDist_SelectedIndexChanged(ddlDistrictSearch, null);
            }
            if (dtQuestion.Rows[0]["BlockCode"].ToString() != "")
            {
                ddlTraingMode.SelectedValue = Convert.ToString(dtQuestion.Rows[0]["BlockCode"]);

            }
            DateTime fDate = Convert.ToDateTime(dtQuestion.Rows[0]["LockDate"].ToString());
            txtLockDate.Text = fDate.ToString("dd/MM/yyy");

            DateTime fDate1 = Convert.ToDateTime(dtQuestion.Rows[0]["FromDate"].ToString());
            txtFromDate.Text = fDate1.ToString("dd/MM/yyy");

            DateTime tDate = Convert.ToDateTime(dtQuestion.Rows[0]["ToDate"].ToString());
            txtToDate.Text = tDate.ToString("dd/MM/yyy");

            //txtFromDate.Text = Convert.ToString(dtQuestion.Rows[0]["FromDate"]);
            //txtToDate.Text = Convert.ToString(dtQuestion.Rows[0]["ToDate"]);
            txtFromDate.Enabled = false;
            txtToDate.Enabled = false;

            txtLocation.Text = Convert.ToString(dtQuestion.Rows[0]["Location"]);
            if (dtQuestion.Rows[0]["Other"].ToString() != "")
            {
                txtOthersName.Text = Convert.ToString(dtQuestion.Rows[0]["Other"]);
            }
            if (dtQuestion.Rows[0]["TrainingTypeID"].ToString() != "")
            {
                ddlTraining.SelectedValue = Convert.ToString(dtQuestion.Rows[0]["TrainingTypeID"]);
            }
            if (dtQuestion.Rows[0]["AssessmentFor"].ToString() != "")
            {
                ddlLevel.SelectedValue = Convert.ToString(dtQuestion.Rows[0]["AssessmentFor"]);
            }
            if (Convert.ToString(dtQuestion.Rows[0]["AssessmentFor"]) == "1")
            {
                if (dtQuestion.Rows[0]["TrainingOutCome"].ToString() != "")
                {
                    ddlLearning.SelectedValue = Convert.ToString(dtQuestion.Rows[0]["TrainingOutCome"]);
                }
            }
            if (Convert.ToString(dtQuestion.Rows[0]["AssessmentFor"]) == "2")
            {
                ddlTraingOutcome.SelectedValue = Convert.ToString(dtQuestion.Rows[0]["TrainingOutCome"]);
            }
            if (Convert.ToString(dtQuestion.Rows[0]["AssessmentFor"]) == "1")
            {
                if (dtQuestion.Rows[0]["SpecificTraining"].ToString() != "")
                {
                    ddlLearning_SelectedIndexChanged(ddlLearning, null);
                    ddlTraingOutcome.SelectedValue = Convert.ToString(dtQuestion.Rows[0]["SpecificTraining"]);
                    ddlTraingOutcome_SelectedIndexChanged(ddlTraingOutcome, null);
                }
            }
            if (dtQuestion.Rows[0]["AssessmentType"].ToString() != "")
            {
                ddlassement.SelectedValue = Convert.ToString(dtQuestion.Rows[0]["AssessmentType"]);
            }
            if (dtQuestion.Rows[0]["QuestionCategory"].ToString() != "")
            {
                ddlCategory.SelectedValue = Convert.ToString(dtQuestion.Rows[0]["QuestionCategory"]);
            }

            //int FormID = Convert.ToInt32(dtQuestion.Rows[0]["QuestionCategory"]);
            //BindGvQuestion(FormID);
            DataTable dtQuestion2 = new DataTable();
            dtQuestion2 = Get_DataFor1Filter("USP_GetMSTFormQuestionOnForm2024", Tarining_ID.ToString());

            if (dtQuestion2.Rows.Count > 0)
            {
                //ddlCategory.SelectedValue = Convert.ToString(dtQuestion2.Rows[0]["QuestionCategory"]);
                gvRightSearch.DataSource = dtQuestion2;
                gvRightSearch.DataBind();
                lblTotal.Text = dtQuestion2.Rows.Count.ToString();
                ViewState["dtselected"] = dtQuestion2;

            }
            else
            {
                ViewState["dtselected"] = null;
                gvRightSearch.DataSource = null;
                gvRightSearch.DataBind();
            }
            if (Convert.ToInt32(ddlLevel.SelectedValue) == 1)
            {

                if (Convert.ToInt32(ddlassement.SelectedValue) == 1)
                {

                    txtLink.Visible = true;

                }
                if (Convert.ToInt32(ddlassement.SelectedValue) == 2)
                {
                    if (Convert.ToDateTime(Modate) > DateTime.Now)
                    {
                        txtLink.Visible = false;
                    }
                    else
                    {
                        txtLink.Visible = true;
                    }
                }

            }
            else
            {


            }
            DataTable Participarticipate = new DataTable();
            Participarticipate = Get_DataFor1Filter("USP_Tarining_Participarticipate2026", Tarining_ID.ToString());

            if (Participarticipate.Rows.Count > 0)
            {
                Session["dtParticiparticipate"] = Participarticipate;
            }
            else
            {
                Session["dtParticiparticipate"] = null;

            }

            DataTable EntryDoneBY = new DataTable();
            EntryDoneBY = Get_DataFor1Filter("USP_Tarining_EntryDoneBy", Tarining_ID.ToString());

            if (EntryDoneBY.Rows.Count > 0)
            {
                Session["dtEntryDoneBY"] = EntryDoneBY;
            }
            else
            {
                Session["dtEntryDoneBY"] = null;

            }

        }

    }

    public void FillTrainingType()
    {
        conditions = "";
        objComman.BindDLL("mstTrainingType", "TrainingID,dbo.TitleCase(upper(TrainingName)) as TrainingName ", conditions, "TrainingName", "asc", ddlTraining, "TrainingName", "TrainingID", "--Select--");

    }

    public void fillcategory(string FormLevel)
    {
        string UserID = Session["UserID"].ToString();
        DataTable dt = new DataTable();

        dt = Get_DataFor3Filter("USP_GetSurveyOnAgencyAndLevelTr2024", "", FormLevel.ToString(), Convert.ToString(Session["FinYear"]));

        ddlCategory.DataSource = dt;
        ddlCategory.DataTextField = "FormName";
        ddlCategory.DataValueField = "FormID";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("------Select-------", "0"));

    }
    public DataTable Get_DataFor3Filter(string ProcedureName, string Filter1, string Filter2, string Filter3)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                            new SqlParameter("@Filter1",Filter1),
                            new SqlParameter("@Filter2",Filter2),
                            new SqlParameter("@Filter3",Filter3),


                    };
            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, ProcedureName, paramvT);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch (Exception)
        {

        }
        return dtcombo;
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            ddlState.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlState, null);
        }
        else
        {
            ddlState.SelectedIndex = 0;
        }

    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDistSearchNew();
    }
    protected void ddlDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        GVMainBind();
        FillCBBock();
    }
    public void FillCBBock()
    {
        conditions = "";
        string a = "";

        if (ddlDistrictSearch.SelectedValue != "0")
        {
            conditions = "DistrictCode ='" + ddlDistrictSearch.SelectedValue + "' ";
        }
        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlMainBlock, "BlockName", "BlockCode", "--Select--");




    }

    public void FillCBDistSearchNew()
    {

        //conditions = "";
        //string conditions1 = "StateCode ='" + ddlState.SelectedValue + "' ";


        //conditions = "StateCode ='" + ddlState.SelectedValue + "' and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";

        //DataTable dtTb = objMain.LoadData(" SELECT DistrictCode,  dbo.TitleCase(upper(DistrictName)) as  DistrictName FROM [mst2District] where  " + conditions + "  union select  DistrictCode,  dbo.TitleCase(upper(DistrictName))  +' ('+ 'Spine' +')'  as  DistrictName from mstSpineDistrict  where  " + conditions1 + "   order by DistrictName ");
        //objComman.BindDLLMasterTableVillage("mst2District", "DistrictCode,DistrictName", dtTb, conditions, "DistrictName", "asc", ddlDistrictSearch, "DistrictName", "DistrictCode", "Select");
        conditions = "";
        string conditions1 = "StateCode ='" + ddlState.SelectedValue + "' ";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "StateCode ='" + ddlState.SelectedValue + "' and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "StateCode ='" + ddlState.SelectedValue + "'  and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else
        {
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";


        }

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
            DataTable dtTb = objMain.LoadData(" SELECT DistrictCode,  dbo.TitleCase(upper(DistrictName)) as  DistrictName FROM [mst2District] where  " + conditions + "  union select  DistrictCode,  dbo.TitleCase(upper(DistrictName))  +' ('+ 'Spine' +')'  as  DistrictName from mstSpineDistrict  where  " + conditions1 + "   order by DistrictName ");



            objComman.BindDLLMasterTableVillage("mst2District", "DistrictCode,DistrictName", dtTb, conditions, "DistrictName", "asc", ddlDistrictSearch, "DistrictName", "DistrictCode", "Select");

        }


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

    public void FillCBState()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {
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

            objComman.BindDLLMasterTableVillage("mst1State", "StateCode,StateName", dtTb, conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "Select");

        }
        else
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            DataTable dtTb = objMain.LoadData(" SELECT StateCode,  dbo.TitleCase(upper(StateName)) as  StateName FROM [mst1State] where " + conditions + " union select  StateCode,  dbo.TitleCase(upper(StateName))  +' ('+ 'Spine' +')'  as  StateName  from [mstSpineState] order by Statecode  ");

            objComman.BindDLLMasterTableVillage("mst1State", "StateCode,StateName", dtTb, conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "Select");

        }

        ddlState.SelectedIndex = 1;
        ddlState_SelectedIndexChanged(ddlState, null);

    }
    public DataTable CreateDataTable()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYear.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYear;
    }

    public DataTable NewgeneratedDT()
    {

        DataTable dt = new DataTable();
        DataColumn QuestionCategory = new DataColumn("QuestionCategory", typeof(System.Int32));
        DataColumn Assessment = new DataColumn("Assessment", typeof(System.Int32));
        DataColumn FormID = new DataColumn("FormID", typeof(System.Int32));
        DataColumn QuestionID = new DataColumn("QuestionID", typeof(System.Int32));
        DataColumn Sequence = new DataColumn("Sequence", typeof(System.Int32));
        dt.Columns.AddRange(new DataColumn[] { Assessment, QuestionCategory, QuestionID, FormID, Sequence });
        return dt;
    }
    #endregion

    #region Ashu sir 18-02-2022

    protected void btnParticipate_Click(object sender, EventArgs e)
    {
        string RVal = SetTextBoxFocusSelect(this.Page);
        //if (!InterventionSql_Injection(RVal))
        //{
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Spurious input detected. Data rejected')</script>", false);
        //    MPEFormName1.Show();
        //    return;
        //}

        DataTable dtParticiparticipate = null;
        if (ddlLevel.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Assessment For')</script>", false);
            return;
        }
        if (Session["dtParticiparticipate"] != null)
        {
            dtParticiparticipate = ((DataTable)Session["dtParticiparticipate"]);
        }
        else
        {
            dtParticiparticipate = CreateDataDate();
        }
        if (txtParticipate.Text != "")
        {
            string[] words = txtParticipate.Text.Trim().Split(',');
            foreach (var word in words)
            {
                if (word.Length > 3)
                {
                    DataRow[] drmain = dtParticiparticipate.Select("ParticiparticipateName='" + word.Trim() + "'");
                    if (drmain.Length > 0)
                    {

                    }
                    else
                    {
                        DataTable dtP1 = new DataTable();
                        dtP1 = Get_DataFor1Filter1("LoadStaffParticiparticipate", ddlLevel.SelectedValue, word.Trim());
                        if (dtP1.Rows.Count > 0)
                        {
                            DataRow dr;
                            dr = dtParticiparticipate.NewRow();
                            dr["ParticiparticipateName"] = word.Trim();
                            dr["FormID"] = "0";
                            if (dtP1.Rows.Count > 0)
                            {
                                dr["Name"] = dtP1.Rows[0]["EMPName"].ToString();
                            }
                            else
                            {
                                dr["Name"] = string.Empty;
                            }
                            dr["UserType"] = dtP1.Rows[0]["UserType"].ToString();
                            dr["TeamBalikaUniqueCode"] = dtP1.Rows[0]["UniqueCode"].ToString();
                            dtParticiparticipate.Rows.Add(dr);
                        }
                    }
                }
            }
        }


        Session["dtParticiparticipate"] = dtParticiparticipate;
        GridView1.DataSource = dtParticiparticipate;
        GridView1.DataBind();

        if (Convert.ToString(ViewState["Tarining_ID"]) == "")
        {
        }
        else
        {

            int Tarining_ID = Convert.ToInt32(ViewState["Tarining_ID"].ToString());



            DataTable dt = Session["dtParticiparticipate"] as DataTable;

            DataTable dtparti = Session["dtParticiparticipate"] as DataTable;
            if (dtparti.Rows.Count > 0)
            {
                for (int i = 0; i < dtparti.Rows.Count; i++)
                {
                    dt.Rows[i]["FormID"] = Tarining_ID;
                }
                int Parti_Success = Insert_USP_Participarticipate20252026(Tarining_ID, dtparti);
            }
        }
        if (dtParticiparticipate.Rows.Count > 0)
        {
            lblPtotal.Text = dtParticiparticipate.Rows.Count.ToString();
        }
        else
        {
            lblPtotal.Text = "0";
        }
        MPEFormName1.Show();
    }

    protected void Delete_Question_Click2(object sender, EventArgs e)
    {
        //MPEFormName.Show();

        LinkButton Edit_Question = sender as LinkButton;
        GridViewRow row = Edit_Question.NamingContainer as GridViewRow;
        int index = row.RowIndex;


        string QuestionID = (GridView1.DataKeys[index].Values["ParticiparticipateName"].ToString());
        DataTable dtParticiparticipate = null;
        if (Convert.ToString(ViewState["Tarining_ID"]) != "")
        {
            int Tarining_ID = Convert.ToInt32(ViewState["Tarining_ID"].ToString());

            int deleteTSD1 = objMain.DeleteAssmentQuestion(Tarining_ID.ToString(), QuestionID.Trim());

        }
        dtParticiparticipate = ((DataTable)Session["dtParticiparticipate"]);
        dtParticiparticipate.Rows.Remove(dtParticiparticipate.Rows[index]);

        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Delete Sucessfully')</script>", false);

        Session["dtParticiparticipate"] = dtParticiparticipate;
        GridView1.DataSource = dtParticiparticipate;
        GridView1.DataBind();
        if (dtParticiparticipate.Rows.Count > 0)
        {
            lblPtotal.Text = dtParticiparticipate.Rows.Count.ToString();
        }
        else
        {
            lblPtotal.Text = "0";
        }
        MPEFormName1.Show();
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
        MPE_Entry.Show();
    }


    public DataTable CreateDataDate()
    {

        DataTable dtParticiparticipate = new DataTable();


        dtParticiparticipate.Columns.Add(new DataColumn("FormID", System.Type.GetType("System.String")));
        dtParticiparticipate.Columns.Add(new DataColumn("ParticiparticipateName", System.Type.GetType("System.String")));
        dtParticiparticipate.Columns.Add(new DataColumn("Name", System.Type.GetType("System.String")));
        dtParticiparticipate.Columns.Add(new DataColumn("TeamBalikaUniqueCode", System.Type.GetType("System.String")));
        dtParticiparticipate.Columns.Add(new DataColumn("UserType", System.Type.GetType("System.String")));


        Session["dtParticiparticipate"] = dtParticiparticipate;
        return dtParticiparticipate;
    }

    public DataTable CreateDataEntry()
    {

        DataTable dtEntryDoneBY = new DataTable();

        dtEntryDoneBY.Columns.Add(new DataColumn("FormID", System.Type.GetType("System.String")));
        dtEntryDoneBY.Columns.Add(new DataColumn("ParticiparticipateName", System.Type.GetType("System.String")));
        dtEntryDoneBY.Columns.Add(new DataColumn("EntryDoneByName", System.Type.GetType("System.String")));
        Session["dtEntryDoneBY"] = dtEntryDoneBY;
        return dtEntryDoneBY;
    }
    protected void LnkImport_Click(object sender, EventArgs e)
    {
        //DataTable dt = new DataTable();
        //if (ddlForm.SelectedIndex>0)
        //{
        //   DataTable dtHeader = Get_DataFor2FilterReport("rptSurvey", ddlForm.SelectedValue.ToString(), "1");
        //    exportTABLE_COMPLETE(dtHeader);


        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "Showalert", "alert('Please select Survey');", true);
        //}
        txtParticipate.Text = "";
        DataTable dtParticiparticipate = null;
        lblPtotal.Text = "0";
        dtParticiparticipate = ((DataTable)Session["dtParticiparticipate"]);
        GridView1.DataSource = dtParticiparticipate;
        GridView1.DataBind();
        if (Session["dtParticiparticipate"] != null)
        {
            if (dtParticiparticipate.Rows.Count > 0)
            {
                lblPtotal.Text = dtParticiparticipate.Rows.Count.ToString();
            }
            else
            {
                lblPtotal.Text = "0";
            }
        }
        MPEFormName1.Show();

    }
    protected void ChangePreferenceDown(object sender, EventArgs e)
    {
        LinkButton lnkDown = sender as LinkButton;
        GridViewRow row = lnkDown.NamingContainer as GridViewRow;
        int index = row.RowIndex;
        int QuetionID, Sequence, QuetionIDPrefrence, SequencePrefrence;

        QuetionID = Int32.Parse(gvRightSearch.DataKeys[index].Values["QuestionID"].ToString());
        Sequence = Int32.Parse(gvRightSearch.DataKeys[index].Values["Sequence"].ToString());

        QuetionIDPrefrence = Int32.Parse(gvRightSearch.DataKeys[index + 1].Values["QuestionID"].ToString());
        SequencePrefrence = Int32.Parse(gvRightSearch.DataKeys[index + 1].Values["Sequence"].ToString());
        DataTable dt = ViewState["dtselected"] as DataTable;

        DataRow[] dr = dt.Select("QuestionID=" + QuetionID + "");
        if (dr.Length > 0)
        {
            dr[0]["Sequence"] = SequencePrefrence;
        }
        DataRow[] dr1 = dt.Select("QuestionID=" + QuetionIDPrefrence + "");
        if (dr1.Length > 0)
        {
            dr1[0]["Sequence"] = Sequence;
        }
        dt.AcceptChanges();
        DataView dv = dt.DefaultView;
        dv.Sort = "Sequence asc";

        gvRightSearch.DataSource = dt;
        gvRightSearch.DataBind();
        ViewState["dtselected"] = dt;
        lnkUplnkDown1();
        //BindGvQuestion(Convert.ToInt32(ddlForm.SelectedValue));
    }
    public void lnkUplnkDown1()
    {
        LinkButton lnkUpChild = (gvRightSearch.Rows[0].FindControl("lnkUp") as LinkButton);
        //LinkButton lnkDownChild = (gvRightSearch.Rows[gvRightSearch.Rows.Count - 1].FindControl("lnkDownChild") as LinkButton);
        lnkUpChild.Enabled = false;
        lnkUpChild.CssClass = "buttonDisable";
        //lnkDownChild.Enabled = false;
        //lnkDownChild.CssClass = "buttonDisable";
    }
    protected void ChangePreferenceUP(object sender, EventArgs e)
    {

        LinkButton lnkUp = sender as LinkButton;
        GridViewRow row = lnkUp.NamingContainer as GridViewRow;
        int index = row.RowIndex;
        int QuetionID, Sequence, QuetionIDPrefrence, SequencePrefrence;


        QuetionID = Int32.Parse(gvRightSearch.DataKeys[index].Values["QuestionID"].ToString());
        Sequence = Int32.Parse(gvRightSearch.DataKeys[index].Values["Sequence"].ToString());

        QuetionIDPrefrence = Int32.Parse(gvRightSearch.DataKeys[index - 1].Values["QuestionID"].ToString());
        SequencePrefrence = Int32.Parse(gvRightSearch.DataKeys[index - 1].Values["Sequence"].ToString());

        DataTable dt = ViewState["dtselected"] as DataTable;
        DataRow[] dr = dt.Select("QuestionID=" + QuetionID + "");
        if (dr.Length > 0)
        {
            dr[0]["Sequence"] = SequencePrefrence;
        }
        DataRow[] dr1 = dt.Select("QuestionID=" + QuetionIDPrefrence + "");
        if (dr1.Length > 0)
        {
            dr1[0]["Sequence"] = Sequence;
        }
        dt.AcceptChanges();
        DataView dv = dt.DefaultView;
        dv.Sort = "Sequence asc";

        gvRightSearch.DataSource = dt;
        gvRightSearch.DataBind();
        ViewState["dtselected"] = dt;
        //DataTable dt = new DataTable();
        ////  dt = objBLL.UpdatePreferenceChildQuestionBank(parentquestionid, QuetionID, Sequence, QuetionIDPrefrence, SequencePrefrence, Int32.Parse(ddlForm.SelectedValue));
        //GvQuestionChild.DataSource = dt;
        //GvQuestionChild.DataBind();
        lnkUplnkDown1();

    }
    protected void LnkCopydata_Click(object sender, EventArgs e)
    {
        MPECopyEndline.Show();
        pnlcopydata.Visible = true;
        txtenddateCopy.Text = "";
        txtstartdatecopy.Text = "";
        if (txtToDate.Text != "")
        {
            CalendarExtender3.StartDate = Convert.ToDateTime(txtToDate.Text).AddDays(0);
            CalendarExtender4.StartDate = Convert.ToDateTime(txtToDate.Text).AddDays(0);
        }
        else
        {
            CalendarExtender3.StartDate = DateTime.Now.AddDays(0);
            CalendarExtender4.StartDate = DateTime.Now.AddDays(0);
        }



    }
    protected void btnCopy_Click(object sender, EventArgs e)
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
        int Tarining_ID = 0, AssessmentFor = 0, TrainingOutCome = 0, SpecificTraining = 0, AssessmentType = 0, Trainingtype = 0, QuestionCategory = 0;
        string Location = "", other = "", EntryBy = "";

        AssessmentFor = Convert.ToInt32(ddlLevel.SelectedValue);
        if (Convert.ToInt32(AssessmentFor) == 1)
        {
            if (ddlLearning.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Training OutCome')</script>", false);

                return;
            }
            if (ddlTraingOutcome.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Specific training')</script>", false);

                return;
            }

            if (ddlassement.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select  Assessment Type')</script>", false);

                return;
            }
            TrainingOutCome = Convert.ToInt32(ddlLearning.SelectedValue);
            SpecificTraining = Convert.ToInt32(ddlTraingOutcome.SelectedValue);
            AssessmentType = Convert.ToInt32(ddlassement.SelectedValue);
        }
        if (Convert.ToInt32(AssessmentFor) == 2)
        {
            TrainingOutCome = Convert.ToInt32(ddlTraingOutcome.SelectedValue);
            AssessmentType = Convert.ToInt32(ddlassement.SelectedValue);
            if (ddlTraingOutcome.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Training OutCome')</script>", false);

                return;
            }


            if (ddlassement.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select  Assessment Type')</script>", false);

                return;
            }
        }
        if (Convert.ToInt32(AssessmentFor) == 3 || Convert.ToInt32(AssessmentFor) == 4)
        {
            if (txtOthersName.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Other')</script>", false);

                return;
            }
        }
        if (Convert.ToInt32(gvRightSearch.Rows.Count) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Add Question')</script>", false);

            return;
        }

        if (Session["dtParticiparticipate"] != null)
        {

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Add Participants')</script>", false);

            return;
        }
        if (txtTotalQuestions.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Total No. of Questions')</script>", false);

            return;
        }
        if (Convert.ToInt32(gvRightSearch.Rows.Count) < Convert.ToInt32(txtTotalQuestions.Text.Trim()))
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Assessment Question greater than the Total no.of Questions selected ')</script>", false);

            return;
        }

        Trainingtype = Convert.ToInt32(ddlLevel.SelectedValue);
        other = txtOthersName.Text;
        Location = txtLocation.Text;

        DateTime FromDate = Convert.ToDateTime(txtstartdatecopy.Text);
        DateTime ToDate = Convert.ToDateTime(txtenddateCopy.Text);

        string fdate = txtstartdatecopy.Text;
        string[] b = fdate.Split('/');
        string FromDate1 = b[2] + '-' + b[1] + '-' + b[0];

        string Tdate = txtenddateCopy.Text;
        string[] T = Tdate.Split('/');
        string Todate = T[2] + '-' + T[1] + '-' + T[0];

        DateTime d1 = Convert.ToDateTime(FromDate1);
        DateTime d2 = Convert.ToDateTime(Todate);
        int month = Convert.ToInt32(T[1]) - Convert.ToInt32(b[1]);
        TimeSpan t = d2 - d1;

        double Days = Convert.ToDouble(t.TotalDays);
        if (Days < 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Valid  Data')</script>", false);
            return;
        }
        if (Math.Sign(Days + 1) < 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date Max 7 Day')</script>", false);
            return;
        }
        if (Math.Round(Days + 1) > 7)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date Max 7 Day')</script>", false);
            return;
        }

        EntryBy = Convert.ToString(Session["username"]);
        string Block = "", Dist = ""; string State = "";
        if (ddlState.SelectedIndex >= 0)
        {
            State = ddlState.SelectedValue;
        }
        if (ddlDistrictSearch.SelectedIndex >= 0)
        {
            Dist = ddlDistrictSearch.SelectedValue;
        }

        if (ddlMainBlock.SelectedIndex >= 0)
        {
            Block = ddlMainBlock.SelectedValue;
        }

        if (Convert.ToString(ViewState["Tarining_ID"]) == "")
        {

        }
        else
        {
            Tarining_ID = Convert.ToInt32(ViewState["Tarining_ID"].ToString());
            int MainID = 0;
            if (ddlMainID.SelectedValue != "")
            {
                MainID = Convert.ToInt32(ddlMainID.SelectedValue);
            }
            int Tarining_IDNew = TrainingQuestionInsertCopy(Tarining_ID, AssessmentFor, TrainingOutCome, SpecificTraining, 2, Trainingtype, other, Location, FromDate, ToDate, EntryBy, State, Dist, ddlTraingMode.SelectedValue, ddlTraining.SelectedValue, MainID);


            if (Tarining_IDNew > 0)
            {
                ViewState["Tarining_ID"] = Tarining_IDNew;

                #region question code 
                if (ddlLevel.SelectedValue != "0")
                {
                    int icount = 0, FormID = 0, QuestionID = 0, Assessment = 0, Sequence = 0;



                    DataTable dtQuestion = NewgeneratedDT();
                    foreach (GridViewRow row in gvRightSearch.Rows)
                    {

                        QuestionID = Convert.ToInt32(gvRightSearch.DataKeys[row.RowIndex]["QuestionID"].ToString());
                        Assessment = Convert.ToInt32(ddlLevel.SelectedValue);
                        QuestionCategory = Convert.ToInt32(ddlCategory.SelectedValue);
                        Sequence = Convert.ToInt32(gvRightSearch.DataKeys[row.RowIndex]["Sequence"].ToString());

                        int icount4 = objMain.InsertUpdateAssment(ddlassement.SelectedValue, QuestionID.ToString(), Tarining_IDNew.ToString(), Sequence.ToString());
                    }


                    DataTable dt = Session["dtParticiparticipate"] as DataTable;

                    DataTable dtparti = Session["dtParticiparticipate"] as DataTable;
                    if (dtparti.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtparti.Rows.Count; i++)
                        {
                            dt.Rows[i]["FormID"] = Tarining_IDNew;
                        }
                        int Parti_Success = Insert_USP_Participarticipate20252026(Tarining_IDNew, dtparti);
                    }

                    DataTable dtentry = Session["dtEntryDoneBY"] as DataTable;
                    string UserID = "";
                    string UserName = "";
                    if (dtentry != null)
                    {
                        if (dtentry.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtentry.Rows.Count; i++)
                            {
                                dtentry.Rows[i]["FormID"] = Tarining_IDNew;
                                UserID += "'" + dtentry.Rows[i]["ParticiparticipateName"] + "'" + ",";
                                string jjj = dtentry.Rows[i]["ParticiparticipateName"] + "(" + dtentry.Rows[i]["EntryDoneByName"] + ")";
                                UserName += "'" + jjj + "'" + ",";
                            }
                            int Entry_Success = objMain.Insert_EntryDone(Tarining_IDNew, dtentry);
                        }
                        if (UserID.Length > 0)
                        {
                            UserID = UserID.Substring(0, UserID.LastIndexOf(","));
                        }
                        if (UserName.Length > 0)
                        {
                            UserName = UserName.Substring(0, UserName.LastIndexOf(","));
                        }

                        #region StaffSchedul
                        //   int StaffSchedul_ID = InsertUpdateStaffScheduling2023(0, AssessmentFor, TrainingOutCome, SpecificTraining, Convert.ToInt32(ddlTraingMode.SelectedValue), Convert.ToInt32(ddlTraining.SelectedValue), other, Location, FromDate, ToDate, EntryBy, State, Dist, Block, UserID, UserName, Tarining_IDNew.ToString());
                        #endregion
                    }
                }
                #endregion
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                GVMainBind();
                GvQuestion.DataSource = null;
                GvQuestion.DataBind();
                EditBind(Tarining_IDNew.ToString(), txtToDate.Text);
                lnkCopy.Visible = false;
            }

        }


    }

    protected void LnkEntry_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        DataTable dtParticiparticipate = Session["dtEntryDoneBY"] as DataTable;
        if (dtParticiparticipate != null)
        {
            if (dtParticiparticipate.Rows.Count > 0)
            {
                GvEntry.DataSource = dtParticiparticipate;
                GvEntry.DataBind();
            }
            else
            {
                GvEntry.DataSource = null;
                GvEntry.DataBind();
            }
        }
        else
        {
            GvEntry.DataSource = null;
            GvEntry.DataBind();
        }
        MPE_Entry.Show();


    }

    protected void BtnEntry_Click(object sender, EventArgs e)
    {
        string RVal = SetTextBoxFocusSelect(this.Page);
        if (!InterventionSql_Injection(RVal))
        {
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Spurious input detected. Data rejected')</script>", false);
            MPE_Entry.Show();
            return;
        }
        DataTable dtEntryDoneBY = null;
        if (ddlLevel.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Assessment For')</script>", false);
            return;
        }

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
                        dtP1 = Get_DataFor1Filter1("LoadParticiparticipate", "0", word.Trim());
                        if (dtP1.Rows.Count > 0)
                        {
                            DataRow dr;
                            dr = dtEntryDoneBY.NewRow();
                            dr["ParticiparticipateName"] = word.Trim();
                            dr["FormID"] = "0";
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
        MPE_Entry.Show();

    }
    protected void btnExcel_Onclick(object sender, EventArgs e)
    {
        if (Session["dtParticiparticipate"] != null)
        {
            DataTable dtparticipateexcel = new DataTable();
            dtparticipateexcel = Session["dtParticiparticipate"] as DataTable;
            dtparticipateexcel.Columns.Remove("FormID");
            ExporttoExcel(dtparticipateexcel, "ParticipateReport");
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('No Records')</script>", false);
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
    public DataTable LoadEmployeeTB2025(string DistCode)
    {
        DataTable dt = null;
        try
        {

            SqlParameter[] parm = new SqlParameter[]
               {

                new SqlParameter("@DistCode",  DistCode),

               };
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadEmployeeTB2025", parm);

        }
        catch
        {

        }
        return dt;
    }
    public DataTable LoadEmployeeTB(string DistCode)
    {
        DataTable dt = null;
        try
        {

            SqlParameter[] parm = new SqlParameter[]
               {

                new SqlParameter("@DistCode",  DistCode),

               };
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadEmployeeTB", parm);

        }
        catch
        {

        }
        return dt;
    }
    protected void btDownload_Click(object sender, EventArgs e)
    {
        if (ddlState.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select State')</script>", false);
            return;
        }
        if (Convert.ToInt32(ddlLevel.SelectedValue) == 2)
        {
            string Con = "";
            DataTable dt = null;
            if (ddlState.SelectedIndex > 0)
            {
                Con = " and V.StateCode='" + ddlState.SelectedValue + "'";
            }
            if (ddlDistrictSearch.SelectedIndex > 0)
            {
                Con += " and V.DistrictCode='" + ddlDistrictSearch.SelectedValue + "'";
            }
            if (Session["user_level_Role"].ToString() == "1")
            {
                dt = LoadEmployeeTB2025(Con);
            }
            else
            {
                dt = LoadEmployeeTB(ddlDistrictSearch.SelectedValue);
            }

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ExporttoExcel(dt);
                }
            }

        }
        else
        {
            DataTable dt = objMain.LoadEmployee(ddlState.SelectedValue, ddlState.SelectedItem.Text, ddlDistrictSearch.SelectedValue, ddlDistrictSearch.SelectedItem.Text);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ExporttoExcel(dt);
                }
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
        if (ViewState["Tarining_ID"] != null)
        {
            int res1 = DeleteTBTraingAssment(ViewState["Tarining_ID"].ToString(), Session["username"].ToString());



            if (res1 > 0)
            {

                btnAdd_Click(btnAdd, null);
                GVMainBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

            }

        }


    }
    public int DeleteTBTraingAssment(string UniqueChildCode, string flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqueChildCode ", UniqueChildCode),
            new SqlParameter("@flag", flag)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteTBTraingAssment", cmdParameters);
    }
    #endregion

}