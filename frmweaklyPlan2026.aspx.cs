using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class frmweaklyPlan2026 : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    public bool edit_status = false;
    string conditions = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {

                LoadYear();
                LoadUserLeavel();
                //UserLevelFilter();

                //FillEduStauts();
                ViewState["1"] = "ss";


            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }

        }
        // ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "loadJSFunction();", true);
        // ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "loadJSFunction1();", true);
    }
    public void LoadYear()
    {

        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}

        objComman.BindDLL("mstLookup", "LookupCode, Description ", "LookupFlag='W1' ", "LookupCode", "asc", ddlActivity, "Description", "LookupCode", "--Select--");
        objComman.BindDLL("mstLookup", "LookupCode, Description ", "LookupFlag='LET' ", "LookupCode", "asc", ddllevelType, "Description", "LookupCode", "--Select--");

        objComman.BindDLL("mstLookup", "LookupCode, Description ", "LookupFlag='W1' ", "LookupCode", "asc", ddlAct, "Description", "LookupCode", "--Select--");

        //objComman.BindDLL("mstLookup", "LookupCode, Description ", "LookupFlag='CT' ", "LookupCode", "asc", ddlActivity, "Description", "LookupCode", "--Select--");
        objComman.BindDLL("mstLookup", "LookupCode, Description ", "LookupFlag='LE' ", "LookupCode", "asc", ddlLeave, "Description", "LookupCode", "--Select--");
        objComman.BindDLL("mstLookup", "LookupCode, Description ", "LookupFlag='HOL' ", "Description", "asc", ddlHoldday, "Description", "LookupCode", "--Select--");
        string conditions1 = "  ActiveStatus=1";
        //objComman.BindDLL("mstLookup", "LookupCode, Description ", "LookupFlag='LE' ", "Description", "asc", ddlLeavemeeting, "Description", "LookupCode", "--Select--");

        //objComman.BindDLL("mstLookup", "LookupCode, Description ", "LookupFlag='LE' ", "Description", "asc", ddlLeaveTravel, "Description", "LookupCode", "--Select--");

        objComman.BindDLL("[PMS].[dbo].mstOutcome", "OutcomeID,OutcomeName ", conditions1, "OutcomeName", "asc", ddlOutcomde, "OutcomeName", "OutcomeID", "--Select--");

    }

    public void LoadOutComeSpicify()
    {
        string conditions = " ";

        objComman.BindDLL("[PMS].[dbo].mstOutcomeSpecific", "sOutcomeID,sOutcomeName ", "OutcomeID=" + ddlOutcomde.SelectedValue + " and ActiveStatus=1", "sOutcomeID", "asc", ddlSpecific, "sOutcomeName", "sOutcomeID", "--Select--");



    }
    public void FillCBDist()
    {

        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = " mst2District.FYear ='" + Session["FinYear"].ToString() + "' and mst2District.Statecode<>'10'";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = " mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else
        {
            conditions = " DistrictCode in('" + Session["NewDistrictCode"].ToString() + "') and mst2District.FYear ='" + Session["FinYear"].ToString() + "' ";


        }
        if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "";
            conditions = " UserName='" + Session["username"].ToString() + "' ";
            string strQry1 = "       sELECT distinct mst2District.DistrictCode as DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist  ";
            strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions + "  and  Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName  ";
            DataTable dtDistrict = objMain.LoadData(strQry1);

            objComman.BindDLLDatatable("mst2District", dtDistrict, "DistrictCode, dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "Desc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

        }
        else
        {

            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        }

    }

    public void LoadUserLeavel()
    {

        if (Session["user_level_Role"].ToString() == "1")
        {
            FillCBDist();
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            FillCBDist();

        }
        else
        {
            FillCBDist();
            ddlDistrict.SelectedIndex = 1;
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
            ddlDistrict.Enabled = false;
        }


    }
    protected void ddlFC_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblTDDA.Text = "Weekly Plan";
        gvWeallyDatewise.DataSource = null;
        gvWeallyDatewise.DataBind();
        gvWeeklly.DataSource = null;
        gvWeeklly.DataBind();
        ddlMonth.SelectedIndex = 0;
        ddlMonth_SelectedIndexChanged(ddlMonth, null);
        btnAdd.Visible = false;
        Button1.Visible = false;
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
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 and BlockCode in( '" + Session["NewBlockCode"].ToString() + "' ) and FYear ='" + ddlYear.SelectedItem.Text + "' ";
        }
        else
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");


        if (Session["user_level_Role"].ToString() == "4")
        {
            ddlBlock.SelectedIndex = 1;
            ddlBlock.Enabled = false;
            ddlBlock_SelectedIndexChanged(ddlDistrict, null);
        }
        else
        {

        }
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        objComman.BindDLL("mstUser", "UserName, FristName+' (' + UserName + ')'as FristName ", "ActiveStatus=1 and  UserLevel=24 and Blockcode='" + ddlBlock.SelectedValue + "'", "FristName", "asc", ddlUser, "FristName", "UserName", "--Select--");
        objComman.BindDLL("mstUser", "UserName, FristName+' (' + UserName + ')'as FristName ", "ActiveStatus=1 and  UserLevel=19 and Blockcode='" + ddlBlock.SelectedValue + "'", "FristName", "asc", ddlBo, "FristName", "UserName", "--Select--");

    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAdd.Visible = false;
        Button1.Visible = false;
        //DataTable dt = new DataTable();
        //dt.Columns.Add("ID");
        //dt.Columns.Add("Type");
        lblTDDA.Text = "Weekly Plan";
        gvWeallyDatewise.DataSource = null;
        gvWeallyDatewise.DataBind();
        Int32 Icount = 0;
        if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        {
            Icount = Convert.ToInt32(ddlYear.SelectedValue) + 1;
        }
        else
        {
            Icount = Convert.ToInt32(ddlYear.SelectedValue);
        }
        SqlParameter[] parm1 = new SqlParameter[]
        {
              new SqlParameter("@Year",Icount),


                 new SqlParameter("@month", ddlMonth.SelectedValue),
                  new SqlParameter("@Flag", "0"),

        };


        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadWeekDropdown", parm1);

        //if (ddlMonth.SelectedIndex > 0)
        //{
        //    int Month = Convert.ToInt32(ddlMonth.SelectedValue);

        //    if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        //    {
        //        Icount = Convert.ToInt32(ddlYear.SelectedValue) + 1;
        //    }
        //    else
        //    {
        //        Icount = Convert.ToInt32(ddlYear.SelectedValue);
        //    }
        //    int pmonth = Convert.ToInt32(ddlMonth.SelectedValue);


        //    var Start1 = new DateTime(Icount, pmonth, 1);
        //    var endDate = Start1.AddMonths(1).AddDays(-1);

        //    var Start1End = Start1.AddDays(6);

        //    var Start2 = Start1.AddDays(7);

        //    var Start2End = Start2.AddDays(6);

        //    var Start3 = Start2.AddDays(7);

        //    var Start3End = Start3.AddDays(6);

        //    var Start4 = Start3.AddDays(7);
        //    DataRow dr;
        //    dr = dt.NewRow();
        //    dr[0] = "1";
        //    dr[1] = Start1.Day + "-" + Start1End.Day + " " + Start1End.ToString("MMM") + " " + Icount;
        //    dt.Rows.Add(dr);

        //    dr = dt.NewRow();
        //    dr[0] = "2";
        //    dr[1] = Start2.Day + "-" + Start2End.Day + " " + Start2End.ToString("MMM") + " " + Icount;

        //    dt.Rows.Add(dr);

        //    dr = dt.NewRow();
        //    dr[0] = "3";
        //    dr[1] = Start3.Day + "-" + Start3End.Day + " " + Start3End.ToString("MMM") + " " + Icount;


        //    dt.Rows.Add(dr);

        //    dr = dt.NewRow();
        //    dr[0] = "4";

        //    dr[1] = Start4.Day + "-" + endDate.Day + " " + endDate.ToString("MMM") + " " + Icount;

        //    dt.Rows.Add(dr);
        //    dt.AcceptChanges();
        //}
        objComman.BindDLLMasterTable("mstSchool", "Wekk,week", dt, conditions, "week", "asc", ddlWeeklly, "Wekk", "week", "Select");
    }
    protected void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
    {
        string con = "";

        btnAdd.Visible = false;
        Button1.Visible = false;
        lblTDDA.Text = "Weekly Plan: " + ddlWeeklly.SelectedItem.Text;
        if (ddlBlock.SelectedIndex > 0)
        {
            con = " and BlockCOde='" + ddlBlock.SelectedValue + "'";
        }
        if (ddlUser.SelectedIndex > 0)
        {
            con += " and Username='" + ddlUser.SelectedValue + "'";
        }
        SqlParameter[] parm1 = new SqlParameter[]
          {

               new SqlParameter("@Con",  con),
                 new SqlParameter("@month", ddlMonth.SelectedValue),
                      new SqlParameter("@Week", ddlWeeklly.SelectedValue),
          };


        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptContactWeelllyReport2026", parm1);
        int Icount;
        if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        {
            Icount = Convert.ToInt32(ddlYear.SelectedValue) + 1;
        }
        else
        {
            Icount = Convert.ToInt32(ddlYear.SelectedValue);
        }
        SqlParameter[] parm2 = new SqlParameter[]
               {
              new SqlParameter("@Year",Icount),
                 new SqlParameter("@month", ddlMonth.SelectedValue),
                  new SqlParameter("@Flag", "0"),
                   new SqlParameter("@Week", ddlWeeklly.SelectedValue),

            };


        DataTable dtmin = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LLoadWeekDropdown2026", parm2);
        if (dtmin.Rows.Count > 0)
        {

            txttdcal.StartDate = Convert.ToDateTime(dtmin.Rows[0]["minDate"].ToString());
            txttdcal.EndDate = Convert.ToDateTime(dtmin.Rows[0]["MaxDate"].ToString());
        }

        if (dt.Rows.Count > 0)
        {
            gvWeeklly.DataSource = dt;
            gvWeeklly.DataBind();


        }
        else
        {
            gvWeeklly.DataSource = null;
            gvWeeklly.DataBind();
        }

        gvWeallyDatewise.DataSource = null;
        gvWeallyDatewise.DataBind();
    }
    protected void Lnkdelete_OnClick(object sender, EventArgs e)
    {
        ImageButton bt = (ImageButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string UniqueChildCode = (gvr.FindControl("lblUniquePlanCode") as Label).Text;
        int res1 = DeleteEnrollMentData(UniqueChildCode);

        if (res1 > 0)
        {
            LoadDate(lblEditUserName.Text);
            ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

        }


    }
    public int DeleteEnrollMentData(string UniqueChildCode)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqueChildCode ", UniqueChildCode),

            new SqlParameter("@UserName",  Session["username"].ToString() )
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteWeekPlan", cmdParameters);
    }
    protected void LnkBtnBlock_OnClick(object sender, EventArgs e)
    {
        ImageButton bt = (ImageButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string UniqueChildCode = (gvr.FindControl("lblUniquePlanCode") as Label).Text;
        string lblVillagecode = (gvr.FindControl("Lvillagecode") as Label).Text;
        objComman.BindDLL("mst5Village", "Villagecode, VillageName ", "Villagecode='" + lblVillagecode + "' ", "VillageName", "asc", ddlVillage, "VillageName", "Villagecode", "--Select--");





        lblEditUniquePlanCode.Text = UniqueChildCode;
        string strQry2 = " Select * FROM [tblPlanActivity] where [UniquePlanCode]='" + UniqueChildCode + "' ";
        DataTable dtSer = objMain.LoadData(strQry2);
        if (dtSer.Rows[0]["ActivityTypeID"].ToString() == "5" || dtSer.Rows[0]["ActivityTypeID"].ToString() == "7")
        {
            DataTable dtmstM = objMain.LoadData(" SELECT TBCode, TBName FROM [mstTeamBalika] inner join mst5Village on mst5Village.VillageCode=mstTeamBalika.VillageCode 	or  mst5Village.refVillage16=mstTeamBalika.VillageCode	or  mst5Village.refVillage17=mstTeamBalika.VillageCode	or  mst5Village.refVillage18=mstTeamBalika.VillageCode	or  mst5Village.refVillage19=mstTeamBalika.VillageCode	or  mst5Village.refVillage20=mstTeamBalika.VillageCode	 	or  mst5Village.refVillage21=mstTeamBalika.VillageCode or  mst5Village.refVillage22=mstTeamBalika.VillageCode	or  mst5Village.refVillage23=mstTeamBalika.VillageCode or  mst5Village.refVillage24=mstTeamBalika.VillageCode or  mst5Village.refVillage25=mstTeamBalika.VillageCode left join mst1State on mst1State.StateCode=mst5Village.StateCode left join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode   left join (select distinct blockcode,blockname from mst3Block) blk ON mst5Village.BlockCode = blk.BlockCode LEFT JOIN (select distinct PanchayatCode,PanchayatName from mstPanchayat) phy  ON mst5Village.PanchayatCode  = phy.PanchayatCode where mst5Village.ClusterCode in(select villagecode FROM [mstUser] where Username='" + lblEditUserName.Text + "' )  ");

            //main DataTable dtmstM = objMain.LoadData(" SELECT TBCode, TBName FROM [mstTeamBalika] inner join mst5Village on mst5Village.VillageCode=mstTeamBalika.VillageCode 	or  mst5Village.refVillage16=mstTeamBalika.VillageCode	or  mst5Village.refVillage17=mstTeamBalika.VillageCode	or  mst5Village.refVillage18=mstTeamBalika.VillageCode	or  mst5Village.refVillage19=mstTeamBalika.VillageCode	or  mst5Village.refVillage20=mstTeamBalika.VillageCode	 	or  mst5Village.refVillage21=mstTeamBalika.VillageCode or  mst5Village.refVillage22=mstTeamBalika.VillageCode	or  mst5Village.refVillage23=mstTeamBalika.VillageCode or  mst5Village.refVillage24=mstTeamBalika.VillageCode or  mst5Village.refVillage25=mstTeamBalika.VillageCode left join mst1State on mst1State.StateCode=mst5Village.StateCode left join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode   left join (select distinct blockcode,blockname from mst3Block) blk ON mst5Village.BlockCode = blk.BlockCode LEFT JOIN (select distinct PanchayatCode,PanchayatName from mstPanchayat) phy  ON mst5Village.PanchayatCode  = phy.PanchayatCode where mst5Village.ClusterCode in(select villagecode FROM [mstUser] where Username='" + lblEditUserName.Text + "' )  and WorkingStatus=1");
            objComman.BindDLLDatatable("mst1State", dtmstM, "TBCode, dbo.TitleCase(upper(TBName)) as TBName", conditions, "TBName", "Desc", ddlTB, "TBName", "TBCode", "--Select--");

        }
        else

        {
            DataTable dtmstM = objMain.LoadData(" SELECT TBCode, TBName FROM [mstTeamBalika] inner join mst5Village on mst5Village.VillageCode=mstTeamBalika.VillageCode 	or  mst5Village.refVillage16=mstTeamBalika.VillageCode	or  mst5Village.refVillage17=mstTeamBalika.VillageCode	or  mst5Village.refVillage18=mstTeamBalika.VillageCode	or  mst5Village.refVillage19=mstTeamBalika.VillageCode	or  mst5Village.refVillage20=mstTeamBalika.VillageCode	 	or  mst5Village.refVillage21=mstTeamBalika.VillageCode or  mst5Village.refVillage22=mstTeamBalika.VillageCode	or  mst5Village.refVillage23=mstTeamBalika.VillageCode or  mst5Village.refVillage24=mstTeamBalika.VillageCode or  mst5Village.refVillage25=mstTeamBalika.VillageCode  left join mst1State on mst1State.StateCode=mst5Village.StateCode left join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode   left join (select distinct blockcode,blockname from mst3Block) blk ON mst5Village.BlockCode = blk.BlockCode LEFT JOIN (select distinct PanchayatCode,PanchayatName from mstPanchayat) phy  ON mst5Village.PanchayatCode  = phy.PanchayatCode where mst5Village.villagecode= '" + lblVillagecode + "'  ");

            //Mainserver    DataTable dtmstM = objMain.LoadData(" SELECT TBCode, TBName FROM [mstTeamBalika] inner join mst5Village on mst5Village.VillageCode=mstTeamBalika.VillageCode 	or  mst5Village.refVillage16=mstTeamBalika.VillageCode	or  mst5Village.refVillage17=mstTeamBalika.VillageCode	or  mst5Village.refVillage18=mstTeamBalika.VillageCode	or  mst5Village.refVillage19=mstTeamBalika.VillageCode	or  mst5Village.refVillage20=mstTeamBalika.VillageCode	 	or  mst5Village.refVillage21=mstTeamBalika.VillageCode or  mst5Village.refVillage22=mstTeamBalika.VillageCode	or  mst5Village.refVillage23=mstTeamBalika.VillageCode or  mst5Village.refVillage24=mstTeamBalika.VillageCode or  mst5Village.refVillage25=mstTeamBalika.VillageCode  left join mst1State on mst1State.StateCode=mst5Village.StateCode left join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode   left join (select distinct blockcode,blockname from mst3Block) blk ON mst5Village.BlockCode = blk.BlockCode LEFT JOIN (select distinct PanchayatCode,PanchayatName from mstPanchayat) phy  ON mst5Village.PanchayatCode  = phy.PanchayatCode where mst5Village.villagecode= '" + lblVillagecode + "'  and WorkingStatus=1");
            objComman.BindDLLDatatable("mst1State", dtmstM, "TBCode, dbo.TitleCase(upper(TBName)) as TBName", conditions, "TBName", "Desc", ddlTB, "TBName", "TBCode", "--Select--");
        }
        if (dtSer.Rows[0]["ActivityTypeID"].ToString() == "6")
        {
            objComman.BindDLL("mstLookup", "LookupCode, Description ", "LookupFlag='W1' and lookupcode in(1,2,3,4,5,6) ", "LookupCode", "asc", ddlActivity, "Description", "LookupCode", "--Select--");

        }
        else
        {
            objComman.BindDLL("mstLookup", "LookupCode, Description ", "LookupFlag='W1' and lookupcode in(1,2,3,4,5,7) ", "LookupCode", "asc", ddlActivity, "Description", "LookupCode", "--Select--");

        }

        ddlActivity.SelectedValue = dtSer.Rows[0]["ActivityTypeID"].ToString();
        if (ddlActivity.SelectedValue == "6")
        {
            ddlVillage.SelectedIndex = 1;
            divViallage.Visible = true;
        }
        else
        {
            divViallage.Visible = false;
            ddlVillage.SelectedIndex = 0;
        }
        //lblRound.Text = dtSer.Rows[0]["Round"].ToString();
        lblRound.Text = "1";
        ddlActivity_SelectedIndexChanged(ddlActivity, null);
        if (dtSer.Rows[0]["ActivityTypeID"].ToString() == "1")
        {
            ddllevelType.SelectedValue = dtSer.Rows[0]["LeaveType"].ToString();
        }
        if (dtSer.Rows[0]["ActivityID"].ToString() != "")
        {
            if (dtSer.Rows[0]["ActivityID"].ToString() != "0")
            {
                ddlActivity1.SelectedValue = dtSer.Rows[0]["ActivityID"].ToString();
                ddlActivity1_SelectedIndexChanged(ddlActivity, null);
            }
        }
        if (ddlActivity1.SelectedValue == "12" || ddlActivity1.SelectedValue == "14" | ddlActivity1.SelectedValue == "15")
        {
            ddlschool.SelectedValue = dtSer.Rows[0]["SchoolCode"].ToString();
        }
        if (ddlActivity1.SelectedValue == "5" || ddlActivity1.SelectedValue == "6" || ddlActivity1.SelectedValue == "7" || ddlActivity1.SelectedValue == "8")
        {
            string MM_Agenda1 = dtSer.Rows[0]["SchoolCode"].ToString();
            string[] MMAgenda1 = MM_Agenda1.Split(',');
            string MM_AgendaMeeting1 = "";
            foreach (string s in MMAgenda1)
            {
                foreach (ListItem item in ChkSchool.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        MM_AgendaMeeting1 += item.Text.Trim() + ",";
                    }
                }
            }
            if (MM_AgendaMeeting1.Length > 0)
            {
                MM_AgendaMeeting1 = MM_AgendaMeeting1.Substring(0, MM_AgendaMeeting1.LastIndexOf(","));
                txt_pbname.Text = MM_AgendaMeeting1.Trim();
            }

        }

        DateTime Adate = Convert.ToDateTime(dtSer.Rows[0]["PlanDate"].ToString());
        txtPlanDate.Text = Adate.ToString("dd/MM/yyy");
        if (dtSer.Rows[0]["TBCode"].ToString().Length > 2)
        {
            ddlTB.SelectedValue = dtSer.Rows[0]["TBCode"].ToString();
            chkTB.Checked = true;
            ddlTB.Enabled = true;
        }
        else
        {
            ddlTB.SelectedIndex = 0;
            chkTB.Checked = false;
            ddlTB.Enabled = false;
        }
        if (dtSer.Rows[0]["BOCode"].ToString().Length > 2)
        {
            chkBO.Checked = true;
            ddlBo.Enabled = true;
            ddlBo.SelectedValue = dtSer.Rows[0]["BOCode"].ToString();
        }
        else
        {
            chkBO.Checked = false;
            ddlBo.SelectedIndex = 0;
            ddlBo.Enabled = false;
        }

        txtoosg.Text = dtSer.Rows[0]["OOSG"].ToString();
        txtReation.Text = dtSer.Rows[0]["Retention"].ToString();
        txtEnrllment.Text = dtSer.Rows[0]["Enrollment"].ToString();
        txtsmc.Text = dtSer.Rows[0]["SMC"].ToString();
        txtGKp.Text = dtSer.Rows[0]["GKP"].ToString();
        txtBal.Text = dtSer.Rows[0]["LSE"].ToString();
        txtOther.Text = dtSer.Rows[0]["Other"].ToString();
        //if (dtSer.Rows[0]["ActivityID"].ToString() == "1")
        //{
        //    txtoosg.Enabled = true;

        //}
        //else
        //{
        //    txtoosg.Text = "";
        //    txtoosg.Enabled = false;
        //}

        if (ddlActivity.SelectedValue == "2")
        {
            ddlHoldday.SelectedValue = dtSer.Rows[0]["Holiday"].ToString();
        }

        ddlLeave.SelectedValue = dtSer.Rows[0]["IsHalfDay"].ToString();

        if (ddlActivity.SelectedValue == "3")
        {
            txtTraning.Text = dtSer.Rows[0]["Outcome"].ToString();
            //ddlOutcomde_SelectedIndexChanged(ddlOutcomde, null);

            //ddlSpecific.SelectedValue = dtSer.Rows[0]["SpecificOutcome"].ToString();

        }
        if (ddlActivity.SelectedValue == "5")
        {

            txtMeeting.Text = dtSer.Rows[0]["Meeting"].ToString();

        }
        if (ddlActivity.SelectedValue == "4")
        {

            txtTravel.Text = dtSer.Rows[0]["Travel"].ToString();

        }


        txtRemark.Text = dtSer.Rows[0]["Remarks"].ToString();
        MpexdrDistrict.Show();
    }
    protected void CBContacts_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chkTB.Checked == true)
        {
            ddlTB.Enabled = true;
        }
        else
        {
            ddlTB.SelectedIndex = 0;
            ddlTB.Enabled = false;
        }
        MpexdrDistrict.Show();
    }
    protected void CBContacts1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chkBO.Checked == true)
        {
            ddlBo.Enabled = true;
        }
        else
        {
            ddlBo.SelectedIndex = 0;
            ddlBo.Enabled = false;
        }
        MpexdrDistrict.Show();
    }
    protected void ddlOutcomde_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadOutComeSpicify();
        MpexdrDistrict.Show();
    }
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        FilSchool();
    }

    protected void ddlActivity_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_pbname.Text = "";
        divTr.Attributes.Add("style", "display:none;");
        divTr1.Attributes.Add("style", "display:none;");
        div3.Attributes.Add("style", "display:none;");
        div7.Attributes.Add("style", "display:none;");
        //divTr.Visible = false;
        //divTr1.Visible = false;

        divlev.Attributes.Add("style", "display:none;");
        divlev1.Attributes.Add("style", "display:none;");
        divH0.Attributes.Add("style", "display:none;");
        divm.Attributes.Add("style", "display:none;");
        divTravel.Attributes.Add("style", "display:none;");
        ddlLeave.SelectedIndex = 0;
        txtMeeting.Text = "";
        txtTravel.Text = "";
        divSup.Attributes.Add("style", "display:none;");
        divSup1.Attributes.Add("style", "display:none;");
        divSup2.Attributes.Add("style", "display:none;");
        divossg.Attributes.Add("style", "display:none;");

        div97.Attributes.Add("style", "display:none;");
        txtoosg.Text = "";
        txtOther.Text = "";
        txtoosg.Visible = false;
        txtReation.Visible = false;
        txtReation.Text = "";
        divaa.Attributes.Add("style", "display:none;");
        txtReation.Text = "";
        txtsmc.Text = "";
        txtBal.Text = "";
        txtGKp.Text = "";
        txtEnrllment.Text = "";
        ddllevelType.SelectedIndex = 0;
        div6.Attributes.Add("style", "display:none;");
        txtTraning.Text = "";
        if (ddlActivity1.SelectedIndex > 0)
        {
            ddlActivity1.SelectedIndex = 0;
        }
        divlev.Attributes.Add("style", "display:none;");
        ChkSchool.Items.Clear();
        if (ddlActivity.SelectedIndex > 0)
        {
            objComman.BindDLL("mstLookup", "LookupCode, Description ", "LookupFlag='LE' ", "LookupCode", "asc", ddlLeave, "Description", "LookupCode", "--Select--");
            //if (ddlActivity.SelectedValue == "1")
            //{
            //    txtoosg.Text = "";

            //    divlev.Attributes.Add("style", "display:block;"); ;
            //    lblType.Text = "Contact Type";

            //    divSup.Attributes.Add("style", "display:block;");
            //    divSup1.Attributes.Add("style", "display:block;");
            //    divSup2.Attributes.Add("style", "display:block;");
            //    divossg.Attributes.Add("style", "display:block;");
            //    lblOOSC.Text = "OOSG";
            //    txtoosg.Visible = true;

            //}
            //if (ddlActivity.SelectedValue == "9")
            //{
            //    txtReation.Text = "";
            //    lblOOSC.Text = "Retention";
            //    txtReation.Visible = true;
            //    divlev.Attributes.Add("style", "display:block;"); ;
            //    lblType.Text = "Retention Type";

            //    divSup.Attributes.Add("style", "display:block;");
            //    divSup1.Attributes.Add("style", "display:block;");
            //    divSup2.Attributes.Add("style", "display:block;");
            //    divossg.Attributes.Add("style", "display:block;");
            //}
            //if (ddlActivity.SelectedValue == "3")
            //{
            //    divlev.Attributes.Add("style", "display:block;");
            //    lblType.Text = "MM Type";
            //    divSup.Attributes.Add("style", "display:block;");
            //    divSup1.Attributes.Add("style", "display:block;");
            //    divSup2.Attributes.Add("style", "display:block;");
            //}
            //if (ddlActivity.SelectedValue == "2")
            //{

            //    divSup.Attributes.Add("style", "display:block;");
            //    divSup1.Attributes.Add("style", "display:block;");
            //    divSup2.Attributes.Add("style", "display:block;");

            //}
            if (ddlActivity.SelectedValue == "2")
            {
                divH0.Attributes.Add("style", "display:block;");
            }
            if (ddlActivity.SelectedValue == "1")
            {
                divlev.Attributes.Add("style", "display:block;");
                divlev.Attributes.Add("style", "display:block;");
                divlev1.Attributes.Add("style", "display:block;");
                lblType.Text = "Leave Period";
            }
            if (ddlActivity.SelectedValue == "3")
            {
                //divTr.Attributes.Add("style", "display:non;");
                div6.Attributes.Add("style", "display:block;");

            }
            if (ddlActivity.SelectedValue == "5")
            {
                divSup.Attributes.Add("style", "display:block;");
                divSup1.Attributes.Add("style", "display:block;");
                divSup2.Attributes.Add("style", "display:block;");
                divm.Attributes.Add("style", "display:block;");
                divlev.Attributes.Add("style", "display:block;");
                lblType.Text = "Meeting Period";
            }
            if (ddlActivity.SelectedValue == "4")
            {

                divlev.Attributes.Add("style", "display:block;");
                lblType.Text = "Travel Period";
                divTravel.Attributes.Add("style", "display:block;");
            }
            if (ddlActivity.SelectedValue == "7")
            {

                divSup.Attributes.Add("style", "display:block;");
                divSup1.Attributes.Add("style", "display:block;");
                divSup2.Attributes.Add("style", "display:block;");
                div97.Attributes.Add("style", "display:block;");
                divlev.Attributes.Add("style", "display:block;");
                lblType.Text = "Other Period";
            }
            if (ddlActivity.SelectedValue == "6")
            {
                divaa.Attributes.Add("style", "display:block;");
                if (lblRound.Text == "1")
                {
                    objComman.BindDLL("mstLookup", "LookupCode, Description ", "LookupFlag='W2' and lookupcode in(1,2,3,9,10,11,12,13,16) ", "LookupCode", "asc", ddlActivity1, "Description", "LookupCode", "--Select--");
                }
                if (lblRound.Text == "2")
                {
                    objComman.BindDLL("mstLookup", "LookupCode, Description ", "LookupFlag='W2' and lookupcode in(1,2,3,9,10,11,13,12,16) ", "LookupCode", "asc", ddlActivity1, "Description", "LookupCode", "--Select--");
                }
                if (lblRound.Text == "3")
                {
                    objComman.BindDLL("mstLookup", "LookupCode, Description ", "LookupFlag='W2' and lookupcode in(1,2,3,4,9,10,11,13,12,16) ", "LookupCode", "asc", ddlActivity1, "Description", "LookupCode", "--Select--");
                }
                if (lblRound.Text == "4")
                {
                    objComman.BindDLL("mstLookup", "LookupCode, Description ", "LookupFlag='W2'and lookupcode in(1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16) ", "LookupCode", "asc", ddlActivity1, "Description", "LookupCode", "--Select--");
                }
            }



        }

        MpexdrDistrict.Show();
    }


    protected void ddlActivity1_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_pbname.Text = "";
        divTr.Attributes.Add("style", "display:none;");
        divTr1.Attributes.Add("style", "display:none;");
        div3.Attributes.Add("style", "display:none;");
        div7.Attributes.Add("style", "display:none;");
        //divTr.Visible = false;
        //divTr1.Visible = false;

        divlev.Attributes.Add("style", "display:none;");
        divlev1.Attributes.Add("style", "display:none;");
        divH0.Attributes.Add("style", "display:none;");
        divm.Attributes.Add("style", "display:none;");
        divTravel.Attributes.Add("style", "display:none;");
        ddlLeave.SelectedIndex = 0;
        txtMeeting.Text = "";
        txtTravel.Text = "";
        divSup.Attributes.Add("style", "display:none;");
        divSup1.Attributes.Add("style", "display:none;");
        divSup2.Attributes.Add("style", "display:none;");
        divossg.Attributes.Add("style", "display:none;");
        div6.Attributes.Add("style", "display:none;");
        txtTraning.Text = "";
        txtoosg.Text = "";
        txtoosg.Visible = false;
        txtReation.Visible = false;
        txtEnrllment.Visible = false;
        txtsmc.Visible = false;
        txtBal.Visible = false;
        txtGKp.Visible = false;
        txtReation.Text = "";
        txtsmc.Text = "";
        txtBal.Text = "";
        txtGKp.Text = "";
        txtEnrllment.Text = "";

        divaa.Attributes.Add("style", "display:block;");
        objComman.BindDLL("mstLookup", "LookupCode, Description ", "LookupFlag='LE' ", "LookupCode", "asc", ddlLeave, "Description", "LookupCode", "--Select--");
        if (ddlActivity1.SelectedIndex > 0)
        {

            if (ddlActivity1.SelectedValue == "1")
            {
                txtoosg.Text = "";

                divlev.Attributes.Add("style", "display:block;"); ;
                lblType.Text = "Contact Period";

                divSup.Attributes.Add("style", "display:block;");
                divSup1.Attributes.Add("style", "display:block;");
                divSup2.Attributes.Add("style", "display:block;");
                divossg.Attributes.Add("style", "display:block;");
                lblOOSC.Text = "OOSG";
                txtoosg.Visible = true;

            }
            if (ddlActivity1.SelectedValue == "4")
            {
                txtReation.Text = "";
                lblOOSC.Text = "Retention";
                txtReation.Visible = true;
                divlev.Attributes.Add("style", "display:block;"); ;
                lblType.Text = "Retention Period";

                divSup.Attributes.Add("style", "display:block;");
                divSup1.Attributes.Add("style", "display:block;");
                divSup2.Attributes.Add("style", "display:block;");
                divossg.Attributes.Add("style", "display:block;");
            }
            if (ddlActivity1.SelectedValue == "3")
            {
                divlev.Attributes.Add("style", "display:block;");
                lblType.Text = "MM Period";
                divSup.Attributes.Add("style", "display:block;");
                divSup1.Attributes.Add("style", "display:block;");
                divSup2.Attributes.Add("style", "display:block;");
            }
            if (ddlActivity1.SelectedValue == "13")
            {
                divlev.Attributes.Add("style", "display:block;");
                lblType.Text = "Community Contact Period";
                divSup.Attributes.Add("style", "display:block;");
                divSup1.Attributes.Add("style", "display:block;");
                divSup2.Attributes.Add("style", "display:block;");
            }
            if (ddlActivity1.SelectedValue == "2")
            {

                divSup.Attributes.Add("style", "display:block;");
                divSup1.Attributes.Add("style", "display:block;");
                divSup2.Attributes.Add("style", "display:block;");

            }
            if (ddlActivity1.SelectedValue == "16")
            {

                divSup.Attributes.Add("style", "display:block;");
                divSup1.Attributes.Add("style", "display:block;");
                divSup2.Attributes.Add("style", "display:block;");

            }
            if (ddlActivity1.SelectedValue == "5")
            {
                txtEnrllment.Text = "";
                div3.Attributes.Add("style", "display:block;");
                divlev.Attributes.Add("style", "display:block;"); ;
                lblType.Text = "Enrolment Period";

                divSup.Attributes.Add("style", "display:block;");
                divSup1.Attributes.Add("style", "display:block;");
                divSup2.Attributes.Add("style", "display:block;");
                divossg.Attributes.Add("style", "display:block;");
                lblOOSC.Text = "Enrolment";
                txtEnrllment.Visible = true;

            }
            if (ddlActivity1.SelectedValue == "6")
            {
                txtsmc.Text = "";
                div3.Attributes.Add("style", "display:block;");
                divlev.Attributes.Add("style", "display:block;"); ;
                lblType.Text = "SMC Period";

                divSup.Attributes.Add("style", "display:block;");
                divSup1.Attributes.Add("style", "display:block;");
                divSup2.Attributes.Add("style", "display:block;");
                divossg.Attributes.Add("style", "display:block;");
                lblOOSC.Text = "SMC";
                txtsmc.Visible = true;

            }

            if (ddlActivity1.SelectedValue == "7")
            {
                txtGKp.Text = "";
                div3.Attributes.Add("style", "display:block;");
                divlev.Attributes.Add("style", "display:block;"); ;
                lblType.Text = "GKP Period";

                divSup.Attributes.Add("style", "display:block;");
                divSup1.Attributes.Add("style", "display:block;");
                divSup2.Attributes.Add("style", "display:block;");
                divossg.Attributes.Add("style", "display:none;");
                lblOOSC.Text = "Session";
                txtGKp.Visible = true;

            }
            if (ddlActivity1.SelectedValue == "14")
            {

                div7.Attributes.Add("style", "display:block;");
                divlev.Attributes.Add("style", "display:block;"); ;
                lblType.Text = "GKP++ Period";

                divSup.Attributes.Add("style", "display:block;");
                divSup1.Attributes.Add("style", "display:block;");
                divSup2.Attributes.Add("style", "display:block;");
                divossg.Attributes.Add("style", "display:none;");


            }
            if (ddlActivity1.SelectedValue == "15")
            {

                div7.Attributes.Add("style", "display:block;");
                divlev.Attributes.Add("style", "display:block;"); ;
                lblType.Text = "KGBV LSE Period";

                divSup.Attributes.Add("style", "display:block;");
                divSup1.Attributes.Add("style", "display:block;");
                divSup2.Attributes.Add("style", "display:block;");
                divossg.Attributes.Add("style", "display:none;");


            }
            if (ddlActivity1.SelectedValue == "12")
            {

                div7.Attributes.Add("style", "display:block;");
                divlev.Attributes.Add("style", "display:block;"); ;
                lblType.Text = "SAC Update Period";

                divSup.Attributes.Add("style", "display:block;");
                divSup1.Attributes.Add("style", "display:block;");
                divSup2.Attributes.Add("style", "display:block;");
                divossg.Attributes.Add("style", "display:none;");
                if (txtPlanDate.Text != "")
                {
                    string[] fruits = txtPlanDate.Text.Split('/');
                    if (fruits[1] == "7" || fruits[1] == "10" || fruits[1] == "1" || fruits[1] == "3")
                    { }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('SAC Update Only July allow October, January, March')</script>", false);
                        MpexdrDistrict.Show();
                        ddlActivity1.SelectedIndex = 0;
                        return;
                    }
                }

            }
            if (ddlActivity1.SelectedValue == "8")
            {
                txtBal.Text = "";
                div3.Attributes.Add("style", "display:block;");
                divlev.Attributes.Add("style", "display:block;"); ;
                lblType.Text = "Bal Sabha & LSE Period";

                divSup.Attributes.Add("style", "display:block;");
                divSup1.Attributes.Add("style", "display:block;");
                divSup2.Attributes.Add("style", "display:block;");
                divossg.Attributes.Add("style", "display:block;");
                lblOOSC.Text = "Session";
                txtBal.Visible = true;

            }

            if (ddlActivity1.SelectedValue == "9")
            {
                divlev.Attributes.Add("style", "display:block;");
                lblType.Text = "Panchayat Meeting Period";
                divSup.Attributes.Add("style", "display:block;");
                divSup1.Attributes.Add("style", "display:block;");
                divSup2.Attributes.Add("style", "display:block;");
            }

            if (ddlActivity1.SelectedValue == "10")
            {
                divlev.Attributes.Add("style", "display:block;");
                lblType.Text = "Namankan Rally Period";
                divSup.Attributes.Add("style", "display:block;");
                divSup1.Attributes.Add("style", "display:block;");
                divSup2.Attributes.Add("style", "display:block;");

                objComman.BindDLL("mstLookup", "LookupCode, Description ", "LookupFlag='LE' and LookupCode in(2,3) ", "LookupCode", "asc", ddlLeave, "Description", "LookupCode", "--Select--");

            }
            if (ddlActivity1.SelectedValue == "11")
            {
                divlev.Attributes.Add("style", "display:block;");
                lblType.Text = "Ratri Chaupal Period";
                divSup.Attributes.Add("style", "display:block;");
                divSup1.Attributes.Add("style", "display:block;");
                divSup2.Attributes.Add("style", "display:block;");
                objComman.BindDLL("mstLookup", "LookupCode, Description ", "LookupFlag='LE' and LookupCode in(3) ", "LookupCode", "asc", ddlLeave, "Description", "LookupCode", "--Select--");

            }
            FilSchool();

        }

        MpexdrDistrict.Show();
    }
    public void FilSchool()
    {
        string conditions = "";
        conditions = " Villagecode ='" + ddlVillage.SelectedValue + "' ";
        if (ddlActivity1.SelectedIndex > 0)
        {




            if (ddlActivity1.SelectedValue == "5")
            {
                conditions += "  and ManagementType=1";

            }
            if (ddlActivity1.SelectedValue == "6")
            {
                conditions += "  and ManagementType=1 and WorkingStatus=1";

            }

            if (ddlActivity1.SelectedValue == "7")
            {
                conditions += "  and GKPLevel>0";

            }
            if (ddlActivity1.SelectedValue == "8")
            {
                conditions += "  and BAlVal=1";

            }
            if (ddlActivity1.SelectedValue == "14")
            {
                conditions += "  and GKpPlus=1";

            }
            if (ddlActivity1.SelectedValue == "15")
            {
                conditions += "  and LSG=1";

            }
            if (ddlActivity1.SelectedValue == "12")
            {
                conditions += " and WorkingStatus=1";

            }
        }


        string strQry = " select distinct Schoolcode as DistrictCode, dbo.TitleCase(upper(Name))  as DistrictName from mstSchool where    " + conditions + "   order by DistrictName   ";
        DataTable dtDistrict = objMain.LoadData(strQry);


        // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");
        if (ddlActivity1.SelectedValue == "15" || (ddlActivity1.SelectedValue == "14") || (ddlActivity1.SelectedValue == "12"))
        {
            objComman.BindDLLMasterTable("mstSchool", "DistrictName,DistrictCode", dtDistrict, conditions, "Type", "asc", ddlschool, "DistrictName", "DistrictCode", "Select");


        }
        else
        {


            ChkSchool.DataSource = dtDistrict;
            ChkSchool.DataTextField = "DistrictName";
            ChkSchool.DataValueField = "DistrictCode";
            ChkSchool.DataBind();
        }

    }
    protected void btn_Un_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {

        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string UniqueChildCode = (gvr.FindControl("lblUserID") as Label).Text;
        string lblStatus1 = (gvr.FindControl("lblStatus1") as Label).Text;
        if (lblStatus1 == "1")
        {
            Button1.Visible = false;
        }
        else
        {
            Button1.Visible = false;
            btnAdd.Visible = true;
        }
        if (lblStatus1 == "1")
        {
            btnAdd.Visible = false;
        }
        if (lblStatus1 == "0")
        {
            Button1.Visible = true;
        }
        else if (lblStatus1 == "")
        {
            btnAdd.Visible = false;
            Button1.Visible = false;
        }
        else
        {
            //Button1.Visible = false;
        }

        if (lblStatus1 == "0" || lblStatus1 == "1")
        {

            lblEditUserName.Text = UniqueChildCode;
            LoadDate(UniqueChildCode);
        }
        else
        {
            lblEditUserName.Text = "";
            gvWeallyDatewise.DataSource = null;
            gvWeallyDatewise.DataBind();
        }

        //btnAdd.Visible = true;
        //Button1.Visible = true;
    }
    public void LoadDate(string username)
    {
        string con = "";

        SqlParameter[] parm1 = new SqlParameter[]
          {

               new SqlParameter("@CreateBy",  username),
                 new SqlParameter("@month", ddlMonth.SelectedValue),
                      new SqlParameter("@Week", ddlWeeklly.SelectedValue),
          };


        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptContactWeellyDatewiseReport2026", parm1);



        if (dt.Rows.Count > 0)
        {
            gvWeallyDatewise.DataSource = dt;
            gvWeallyDatewise.DataBind();
        }
        else
        {
            gvWeallyDatewise.DataSource = null;
            gvWeallyDatewise.DataBind();
        }
    }
    protected void gvnroll_OnRowCommand(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Label lblUniqueChildCode = (Label)e.Row.FindControl("lblUniqueChildCode");

            //ImageButton lbtn = (ImageButton)e.Row.FindControl("ImgAcc");
            //lbtn.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
            Label lblStatus1 = (Label)e.Row.FindControl("lblStatus1");
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");



            //e.Row.Cells[3].Attributes.Add("style", "word-break:break-all;word-wrap:break-word;");
            if (lblStatus1.Text == "0")
            {
                lblStatus.Text = "Pending";
                lblStatus.ForeColor = System.Drawing.Color.Red;

            }
            else if (lblStatus1.Text == "1")
            {
                lblStatus.Text = "Approved";
                lblStatus.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                lblStatus.Text = "";

            }
        }
    }
    protected void gvnroll1_OnRowCommand(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton LinkButton1 = (ImageButton)e.Row.FindControl("LinkButton1");
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            ImageButton LinkBut51 = (ImageButton)e.Row.FindControl("LinkBut51");
            LinkBut51.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
            Label lblGKPLevel = (Label)e.Row.FindControl("lblGKPLevel");
            Label lblBAlVal = (Label)e.Row.FindControl("lblBAlVal");
            if (lblStatus.Text == "1")
            {
                LinkButton1.Enabled = false;
                LinkBut51.Enabled = false;

            }
            else
            {
                LinkButton1.Enabled = true;
                LinkBut51.Enabled = true;
            }
            if (lblBAlVal.Text == "")
            {
            }
            else
            {
                e.Row.BackColor = System.Drawing.Color.LightBlue;
            }
            if (lblGKPLevel.Text == "")
            {
            }
            else
            {
                e.Row.BackColor = System.Drawing.Color.LightGreen;
            }
            if (lblBAlVal.Text != "" && lblGKPLevel.Text != "")
            {
                e.Row.BackColor = System.Drawing.Color.Pink;

            }
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        string Con = "";
        string HL = "", LE = "", Tr = "", Tr1 = "", meeting = "", travle = "";
        int Ishours = 0;
        if (chkTB.Checked == true)
        {
            Con = "1";
        }
        if (chkBO.Checked == true)
        {
            Con = "2";

        }
        if (chkTB.Checked == true && chkTB.Checked == true)
        {
            Con = "1,2";
        }
        if (txtPlanDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select  Date')</script>", false);
            MpexdrDistrict.Show();
            return;
        }
        if (Convert.ToInt32(ddlActivity.SelectedValue) == 12)
        {
            if (txtPlanDate.Text != "")
            {
                string[] fruits = txtPlanDate.Text.Split('/');
                if (fruits[1] == "7" || fruits[1] == "10" || fruits[1] == "1" || fruits[1] == "3")
                { }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('SAC Update Only July allow October, January, March')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
            }
        }
        DayOfWeek day = Convert.ToDateTime(txtPlanDate.Text).DayOfWeek;
        if (day == DayOfWeek.Sunday)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Other Date')</script>", false);
            MpexdrDistrict.Show();
            return;
        }
        if (ddlActivity.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Activity Planned')</script>", false);
            MpexdrDistrict.Show();
            return;
        }
        if (Convert.ToInt32(ddlActivity.SelectedValue) == 6)
        {
            if (ddlActivity1.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Sub Activity Planned')</script>", false);
                MpexdrDistrict.Show();
                return;
            }
        }

        if (ddlActivity.SelectedValue == "2")
        {
            if (ddlHoldday.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Holiday')</script>", false);
                MpexdrDistrict.Show();
                return;
            }


            DataTable dtmstM = objMain.LoadData(" SELECT * FROM [dbo].[mstLookup]  where LookupFlag = 'HOL' and lookupcode=" + ddlHoldday.SelectedValue + " and Description1= '" + Convert.ToDateTime(txtPlanDate.Text).ToString("yyyy-MM-dd") + "' ");
            if (dtmstM.Rows.Count > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Vaild Holiday')</script>", false);
                MpexdrDistrict.Show();
                return;
            }
            HL = ddlHoldday.SelectedValue;
        }
        //else
        //{
        //    DataTable dtmstM5 = objMain.LoadData(" SELECT * FROM [dbo].[mstLookup] where LookupFlag = 'HOL' and lookupcode=" + ddlHoldday.SelectedValue + " and Description1= '" + Convert.ToDateTime(txtPlanDate.Text).ToString("yyyy-MM-dd") + "' ");
        //    if (dtmstM5.Rows.Count > 0)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Today is Holiday you cannot plan any activity.')</script>", false);
        //        MpexdrDistrict.Show();
        //        return;
        //    }
        //}
        if (ddlActivity.SelectedValue == "6")
        {
            if (ddlVillage.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Village')</script>", false);
                MpexdrDistrict.Show();
                return;
            }

        }
        if (ddlActivity.SelectedValue == "1" || ddlActivity.SelectedValue == "2" || ddlActivity.SelectedValue == "4" || ddlActivity.SelectedValue == "5" || ddlActivity.SelectedValue == "7")
        {
            if (ddlActivity.SelectedValue == "1")
            {
                if (ddlLeave.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Leave Period')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
                if (ddllevelType.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Leave Type')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }


            }
            //if (ddlActivity.SelectedValue == "9")
            //{
            //    if (ddlLeave.SelectedIndex <= 0)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Retention  Type')</script>", false);
            //        MpexdrDistrict.Show();
            //        return;
            //    }

            if (ddlActivity.SelectedValue == "7")
            {
                if (txtOther.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Other Activity')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }

                if (ddlLeave.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Meeting Period')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }

            }

            if (ddlActivity.SelectedValue == "5")
            {
                if (txtMeeting.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Meeting Type')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }

                if (ddlLeave.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Meeting Period')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
                meeting = txtMeeting.Text;
            }
            if (ddlActivity.SelectedValue == "4")
            {
                if (txtTravel.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Travel ')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }

                if (ddlLeave.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Travel Period')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
                travle = txtTravel.Text;
            }
            //if (ddlActivity.SelectedValue == "1")
            //{
            //    if (ddlLeave.SelectedIndex <= 0)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Contact Type')</script>", false);
            //        MpexdrDistrict.Show();
            //        return;
            //    }

            //}
            //if (ddlActivity.SelectedValue == "3")
            //{
            //    if (ddlLeave.SelectedIndex <= 0)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select MM Type')</script>", false);
            //        MpexdrDistrict.Show();
            //        return;
            //    }

            //}
        }
        if (ddlActivity1.SelectedValue == "1" || ddlActivity1.SelectedValue == "3" || ddlActivity1.SelectedValue == "13" || ddlActivity1.SelectedValue == "4" || ddlActivity1.SelectedValue == "5" || ddlActivity1.SelectedValue == "6" || ddlActivity1.SelectedValue == "7" || ddlActivity1.SelectedValue == "8" || ddlActivity1.SelectedValue == "9" || ddlActivity1.SelectedValue == "11" || ddlActivity1.SelectedValue == "12" || ddlActivity1.SelectedValue == "14" || ddlActivity1.SelectedValue == "15")
        {

            if (ddlActivity1.SelectedValue == "1")
            {
                if (ddlLeave.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Contact Period')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }

            }
            if (ddlActivity1.SelectedValue == "9")
            {
                if (ddlLeave.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Panchayat Meeting Period')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }

            }
            if (ddlActivity1.SelectedValue == "13")
            {

                if (ddlLeave.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Community Contact Period')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }

            }
            if (ddlActivity1.SelectedValue == "12")
            {

                if (ddlLeave.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please SAC Update Period')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }

            }
            if (ddlActivity1.SelectedValue == "14")
            {

                if (ddlLeave.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please GKP++ Period')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }

            }
            if (ddlActivity1.SelectedValue == "15")
            {

                if (ddlLeave.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please KGBV LSE  Period')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }

            }
            if (ddlActivity1.SelectedValue == "10")
            {
                if (ddlLeave.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Namankan Rally Period')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }

            }
            if (ddlActivity1.SelectedValue == "11")
            {
                if (ddlLeave.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Ratri Chaupal Period')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }

            }
            if (ddlActivity1.SelectedValue == "3")
            {
                if (ddlLeave.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select MM Period')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }

            }
            if (ddlActivity1.SelectedValue == "4")
            {
                if (ddlLeave.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Retention Period')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }

            }
            if (ddlActivity1.SelectedValue == "5")
            {
                if (ddlLeave.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Enrolment  Period')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }

            }
            if (ddlActivity1.SelectedValue == "6")
            {
                if (ddlLeave.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select SMC Period')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }

            }
            if (ddlActivity1.SelectedValue == "7")
            {
                if (ddlLeave.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select GKP Period')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }

            }
            if (ddlActivity1.SelectedValue == "8")
            {
                if (ddlLeave.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Bal Sabha & LSE Period')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }

            }
        }
        if (ddlActivity.SelectedValue == "3")
        {

            //if (ddlOutcomde.SelectedIndex <= 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Training OutCome ')</script>", false);
            //    MpexdrDistrict.Show();
            //    return;
            //}


            //if (ddlSpecific.SelectedIndex <= 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Specific Training')</script>", false);
            //    MpexdrDistrict.Show();
            //    return;
            //}
            if (txtTraning.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Training Deatils ')</script>", false);
                MpexdrDistrict.Show();
                return;
            }
            Tr = txtTraning.Text;
            Tr1 = "";
        }


        if (chkTB.Checked == true)
        {
            if (ddlTB.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select TB')</script>", false);
                MpexdrDistrict.Show();
                return;
            }

        }
        if (chkBO.Checked == true)
        {
            if (ddlBo.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select BO')</script>", false);
                MpexdrDistrict.Show();
                return;
            }

        }
        if (ddlActivity1.SelectedValue == "1" && txtoosg.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter OOSC')</script>", false);
            MpexdrDistrict.Show();
            return;
        }
        if (ddlActivity1.SelectedValue == "5" && txtEnrllment.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Enrolment ')</script>", false);
            MpexdrDistrict.Show();
            return;
        }
        if (ddlActivity1.SelectedValue == "6" && txtsmc.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter SMC ')</script>", false);
            MpexdrDistrict.Show();
            return;
        }
        //if (ddlActivity1.SelectedValue == "7" && txtGKp.Text.Trim() == "")
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter GKP Session   ')</script>", false);
        //    MpexdrDistrict.Show();
        //    return;
        //}
        if (ddlActivity1.SelectedValue == "8" && txtBal.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter  Balsaba Session ')</script>", false);
            MpexdrDistrict.Show();
            return;
        }
        if (ddlActivity1.SelectedValue == "4" && txtReation.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Reation !')</script>", false);
            MpexdrDistrict.Show();
            return;
        }
        if (ddlActivity1.SelectedValue == "1" && txtoosg.Text.Trim() != "")
        {
            if (ddlLeave.SelectedValue == "1")
            {
                if (Convert.ToInt32(txtoosg.Text) > 25)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Maximum 25 Contacts can be Planned in a Single Day!')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
            }
        }
        if (ddlActivity1.SelectedValue == "4" && txtReation.Text.Trim() != "")
        {
            if (ddlLeave.SelectedValue == "1")
            {
                if (Convert.ToInt32(txtReation.Text) > 36)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Maximum 36 Retention  can be Planned in a Single Day!')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
            }
            else
            {
                if (Convert.ToInt32(txtReation.Text) > 20)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Maximum 20 Retention  can be Planned in a Half Day!')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
            }

            DataTable dtmstMGs = objMain.LoadData(" SELECT isnull(sum(convert(int, Retention)),0) Retention FROM [tblPlanActivity] 	 where  ActivityID=4 and DeleteFlag =1 and UniquePlanCode<>'" + lblEditUniquePlanCode.Text + "' and CreateBy='" + lblEditUserName.Text + "'  and Plandate='" + Convert.ToDateTime(txtPlanDate.Text).ToString("yyyy-MM-dd") + "'  and Month =" + ddlMonth.SelectedValue + " and [WeekNo] =" + ddlWeeklly.SelectedValue + " ");
            if (dtmstMGs.Rows.Count > 0)
            {
                if (Convert.ToInt32(txtReation.Text) + Convert.ToInt32(dtmstMGs.Rows[0]["Retention"]) > 36)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Maximum 36 Retention  can be Planned in a Single Day!')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
            }
        }
        if (ddlActivity1.SelectedValue == "1" && txtoosg.Text.Trim() != "")
        {
            if (ddlLeave.SelectedValue == "2" || ddlLeave.SelectedValue == "3")
            {
                if (Convert.ToInt32(txtoosg.Text) > 15)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Maximum 15 Contacts can be Planned in a Single Day!')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
            }

            DataTable dtmstMGs = objMain.LoadData(" SELECT isnull(sum(convert(int, OOSG)),0) OOSG FROM [tblPlanActivity] 	 where  ActivityID=1 and DeleteFlag =1 and UniquePlanCode<>'" + lblEditUniquePlanCode.Text + "' and isnull(PlanChanged,0) in(0,1) and CreateBy='" + lblEditUserName.Text + "'  and Plandate='" + Convert.ToDateTime(txtPlanDate.Text).ToString("yyyy-MM-dd") + "'  and Month =" + ddlMonth.SelectedValue + " and [WeekNo] =" + ddlWeeklly.SelectedValue + " ");
            if (dtmstMGs.Rows.Count > 0)
            {
                if (Convert.ToInt32(txtoosg.Text) + Convert.ToInt32(dtmstMGs.Rows[0]["OOSG"]) > 25)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Maximum 25 Contacts can be Planned in a Single Day!')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
            }
        }
        string SSchool = "";

        if (ddlActivity1.SelectedValue == "5")
        {
            if (ddlLeave.SelectedValue == "1")
            {
                if (Convert.ToInt32(txtEnrllment.Text) > 15)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Maximum 15 Enrolment  can be Planned in a Single Day!')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
            }
            else
            {
                if (Convert.ToInt32(txtEnrllment.Text) > 8)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Maximum 8 Enrolment  can be Planned in a Half Day!')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
            }

            foreach (ListItem item in ChkSchool.Items)
            {
                if (item.Selected)
                {
                    SSchool += "" + item.Value + "" + ",";

                }

            }
            if (SSchool.Length > 0)
            {
                SSchool = SSchool.Substring(0, SSchool.LastIndexOf(","));
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select School !')</script>", false);
                MpexdrDistrict.Show();
                return;
            }
            DataTable dtmstMGs = objMain.LoadData(" SELECT isnull(sum(convert(int, Enrollment)),0) Enrollment FROM [tblPlanActivity] 	 where  ActivityID=5 and DeleteFlag =1 and UniquePlanCode<>'" + lblEditUniquePlanCode.Text + "' and isnull(PlanChanged,0) in(0,1) and CreateBy='" + lblEditUserName.Text + "'  and Plandate='" + Convert.ToDateTime(txtPlanDate.Text).ToString("yyyy-MM-dd") + "'  and Month =" + ddlMonth.SelectedValue + " and [WeekNo] =" + ddlWeeklly.SelectedValue + " ");
            if (dtmstMGs.Rows.Count > 0)
            {
                if (Convert.ToInt32(txtEnrllment.Text) + Convert.ToInt32(dtmstMGs.Rows[0]["Enrollment"]) > 15)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Maximum 15 Enrolment  can be Planned in a Single Day!')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
            }
        }
        if (ddlActivity1.SelectedValue == "6")
        {
            int schoolcount = 0;
            foreach (ListItem item in ChkSchool.Items)
            {
                if (item.Selected)
                {
                    SSchool += "" + item.Value + "" + ",";
                    schoolcount = schoolcount + 1;
                }

            }
            if (SSchool.Length > 0)
            {
                SSchool = SSchool.Substring(0, SSchool.LastIndexOf(","));
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select School !')</script>", false);
                MpexdrDistrict.Show();
                return;
            }
            if (ddlLeave.SelectedValue == "1")
            {
                if (Convert.ToInt32(txtsmc.Text) > 2)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Maximum 2 SMC  can be Planned in a Single Day!')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
                if (Convert.ToInt32(txtsmc.Text) == 2)
                {
                    if (schoolcount > 2)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Only Two School!')</script>", false);
                        MpexdrDistrict.Show();
                        return;
                    }
                }
                if (Convert.ToInt32(txtsmc.Text) == 1)
                {
                    if (schoolcount > 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Only One School!')</script>", false);
                        MpexdrDistrict.Show();
                        return;
                    }
                }
            }
            else
            {
                if (Convert.ToInt32(txtsmc.Text) > 1)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Maximum 1 SMC  can be Planned in a Half Day!')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
            }

        }
        if (ddlActivity1.SelectedValue == "7")
        {
            int schoolcount = 0;
            foreach (ListItem item in ChkSchool.Items)
            {
                if (item.Selected)
                {
                    SSchool += "" + item.Value + "" + ",";
                    schoolcount = schoolcount + 1;
                }

            }
            if (SSchool.Length > 0)
            {
                SSchool = SSchool.Substring(0, SSchool.LastIndexOf(","));
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select School !')</script>", false);
                MpexdrDistrict.Show();
                return;
            }
            if (ddlLeave.SelectedValue == "1")
            {
                //if (Convert.ToInt32(txtGKp.Text) > 2)
                //{
                //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Maximum 2 GKP Session can be Planned in a Single Day!')</script>", false);
                //    MpexdrDistrict.Show();
                //    return;
                //}
                //if (Convert.ToInt32(txtGKp.Text) == 2)
                //{
                if (schoolcount > 2)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Only Two School!')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
                //}
                //if (Convert.ToInt32(txtGKp.Text) == 2)
                //{
                //    if (schoolcount >2)
                //    {
                //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Only Two School!')</script>", false);
                //        MpexdrDistrict.Show();
                //        return;
                //    }
                //}
                //if (Convert.ToInt32(txtGKp.Text) == 1)
                //{
                //    if (schoolcount > 2)
                //    {
                //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Only One School!')</script>", false);
                //        MpexdrDistrict.Show();
                //        return;
                //    }
                //}
            }
            else
            {
                //if (Convert.ToInt32(txtGKp.Text) > 1)
                //{
                //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Maximum 1 GKP Session can be Planned in a Half Day!')</script>", false);
                //    MpexdrDistrict.Show();
                //    return;
                //}
                //if (Convert.ToInt32(txtGKp.Text) == 1)
                //{
                if (schoolcount > 1)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Only One School!')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
                //}
            }

        }
        if (ddlActivity1.SelectedValue == "8")
        {
            int schoolcount = 0;
            foreach (ListItem item in ChkSchool.Items)
            {
                if (item.Selected)
                {
                    SSchool += "" + item.Value + "" + ",";
                    schoolcount = schoolcount + 1;
                }

            }
            if (SSchool.Length > 0)
            {
                SSchool = SSchool.Substring(0, SSchool.LastIndexOf(","));
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select School !')</script>", false);
                MpexdrDistrict.Show();
                return;
            }
            if (ddlLeave.SelectedValue == "1")
            {
                if (Convert.ToInt32(txtBal.Text) > 2)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Maximum 2 Bal Sabha & LSE  can be Planned in a Single Day!')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
                if (Convert.ToInt32(txtBal.Text) == 2)
                {
                    if (schoolcount < 2)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Two School!')</script>", false);
                        MpexdrDistrict.Show();
                        return;
                    }
                }
                if (Convert.ToInt32(txtBal.Text) == 2)
                {
                    if (schoolcount > 2)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Only Two School!')</script>", false);
                        MpexdrDistrict.Show();
                        return;
                    }
                }

                if (Convert.ToInt32(txtBal.Text) == 1)
                {
                    if (schoolcount > 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Only One School!')</script>", false);
                        MpexdrDistrict.Show();
                        return;
                    }
                }

            }
            else
            {
                if (Convert.ToInt32(txtBal.Text) > 1)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Maximum 1 Bal Sabha & LSE  can be Planned in a Half Day!')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
                if (schoolcount > 1)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Only One School!')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
            }

        }
        string schoolName = "";
        if (ddlActivity1.SelectedValue == "12" || ddlActivity1.SelectedValue == "14" | ddlActivity1.SelectedValue == "15")
        {
            if (ddlschool.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select School')</script>", false);
                MpexdrDistrict.Show();
                return;
            }
            SSchool = ddlschool.SelectedValue;

        }
        SqlParameter[] parm1 = new SqlParameter[]
        {

               new SqlParameter("@CreateBy",  lblEditUserName.Text),
                 new SqlParameter("@month", ddlMonth.SelectedValue),
                      new SqlParameter("@Week", ddlWeeklly.SelectedValue),
                           new SqlParameter("@Date", Convert.ToDateTime(txtPlanDate.Text).ToString("yyyy-MM-dd")),
                             new SqlParameter("@UniqCode",lblEditUniquePlanCode.Text),
        };


        DataSet dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptContactWeellyvaldation2026", parm1);

        if (dt.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToInt32(dt.Tables[0].Rows[0]["Score"]) == 8)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' " + dt.Tables[1].Rows[0]["Description"] + "  is Already Planned on  " + dt.Tables[1].Rows[0]["PlanDate"] + " ')</script>", false);
                MpexdrDistrict.Show();
                return;
            }

            if (Convert.ToInt32(dt.Tables[0].Rows[0]["Score"]) == 4)
            {
                if (ddlLeave.SelectedValue == "1")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' " + dt.Tables[1].Rows[0]["Description"] + "   is Already Planned on " + dt.Tables[1].Rows[0]["PlanDate"] + " ')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
                if (ddlActivity.SelectedValue == "3")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' " + dt.Tables[1].Rows[0]["Description"] + "   is Already Planned on " + dt.Tables[1].Rows[0]["PlanDate"] + " ')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }

                if (ddlActivity.SelectedValue == "4")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' " + dt.Tables[1].Rows[0]["Description"] + "   is Already Planned on " + dt.Tables[1].Rows[0]["PlanDate"] + " ')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
                //if (ddlActivity.SelectedValue == "7")
                //{
                //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' " + dt.Tables[1].Rows[0]["Description"] + "   is Already Planned on " + dt.Tables[1].Rows[0]["PlanDate"] + " ')</script>", false);
                //    MpexdrDistrict.Show();
                //    return;
                //}
                if (ddlActivity1.SelectedValue == "2")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' " + dt.Tables[1].Rows[0]["Description"] + "   is Already Planned on " + dt.Tables[1].Rows[0]["PlanDate"] + " ')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
                if (ddlActivity1.SelectedValue == "16")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' " + dt.Tables[1].Rows[0]["Description"] + "   is Already Planned on " + dt.Tables[1].Rows[0]["PlanDate"] + " ')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
                //if (ddlActivity.SelectedValue == "9")
                //{
                //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' " + dt.Tables[1].Rows[0]["Description"] + "   is Already Planned on " + dt.Tables[1].Rows[0]["PlanDate"] + " ')</script>", false);
                //    MpexdrDistrict.Show();
                //    return;
                //}
            }
            //DataTable dtmain = dt.Tables[1];
            //DataRow[] dr = dtmain.Select("ActivityID='" + ddlActivity.SelectedValue + "'");
            //if (ddlActivity.SelectedValue != "3")
            //{
            //    if (dr.Length > 0)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' " + dt.Tables[1].Rows[0]["Description"] + "   is Already Planned on " + dt.Tables[1].Rows[0]["PlanDate"] + " ')</script>", false);
            //        MpexdrDistrict.Show();
            //        return;
            //    }
            //}
        }

        if (ddlActivity.SelectedValue == "7" || ddlActivity.SelectedValue == "1" || ddlActivity.SelectedValue == "4" || ddlActivity.SelectedValue == "5" || ddlActivity1.SelectedValue == "1" || ddlActivity1.SelectedValue == "3" || ddlActivity1.SelectedValue == "4" || ddlActivity1.SelectedValue == "5" || ddlActivity1.SelectedValue == "6" || ddlActivity1.SelectedValue == "7" || ddlActivity1.SelectedValue == "8" || ddlActivity1.SelectedValue == "9" || ddlActivity1.SelectedValue == "10" || ddlActivity1.SelectedValue == "11" || ddlActivity1.SelectedValue == "12" || ddlActivity1.SelectedValue == "13" || ddlActivity1.SelectedValue == "14" || ddlActivity1.SelectedValue == "15")
        {
            DataTable dtmstMGs = objMain.LoadData(" SELECT case when isnull([ActivityID],0)>0then w2.Description else  mstLookup.Description end  Description, IsHours  IsHours,[ActivityID] FROM [tblPlanActivity] 	  	  left join mstLookup on mstLookup.LookupCode=[tblPlanActivity].[ActivityTypeID] and mstLookup.LookupFlag='W1'	  	  left join mstLookup w2 on w2.LookupCode=[tblPlanActivity].[ActivityID] and w2.LookupFlag='W2'  where  DeleteFlag =1 and UniquePlanCode<>'" + lblEditUniquePlanCode.Text + "' and CreateBy='" + lblEditUserName.Text + "' and isnull(PlanChanged,0) in(0,1) and Plandate='" + Convert.ToDateTime(txtPlanDate.Text).ToString("yyyy-MM-dd") + "'  and Month =" + ddlMonth.SelectedValue + " and [WeekNo] =" + ddlWeeklly.SelectedValue + " and IsHalfDay =" + ddlLeave.SelectedValue + "");
            if (dtmstMGs.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Already planned " + dtmstMGs.Rows[0]["Description"] + "')</script>", false);
                MpexdrDistrict.Show();
                return;

            }

        }



        int hhmin = Convert.ToInt32(ddlLeave.SelectedValue);
        if (ddlActivity1.SelectedValue == "2" || ddlActivity1.SelectedValue == "16" || ddlActivity.SelectedValue == "2" || ddlActivity.SelectedValue == "4" || ddlActivity.SelectedValue == "3")
        {
            hhmin = 1;
            Ishours = 8;
        }
        else
        {
            if (ddlLeave.SelectedValue == "1")
            {
                Ishours = 8;
            }
            else
            {
                Ishours = 4;
            }

        }

        foreach (ListItem item in ChkSchool.Items)
        {
            if (item.Selected)
            {
                schoolName += "" + item.Text + "" + ",";

            }

        }
        if (schoolName.Length > 0)
        {
            schoolName = schoolName.Substring(0, schoolName.LastIndexOf(","));
        }
        if (ddlActivity1.SelectedValue == "12" || ddlActivity1.SelectedValue == "14" | ddlActivity1.SelectedValue == "15")
        {

            SSchool = ddlschool.SelectedValue;
            schoolName = ddlschool.SelectedValue;
        }
        int icount = SaveDataInsertUpdate(lblEditUniquePlanCode.Text, Convert.ToDateTime(txtPlanDate.Text).ToString("yyyy-MM-dd"), ddlActivity.SelectedValue, Con, ddlBo.SelectedValue, ddlTB.SelectedValue, txtoosg.Text, txtRemark.Text, Session["username"].ToString(), meeting, travle, HL, LE, Tr, Tr1, Ishours.ToString(), SSchool, "", hhmin, schoolName);
        if (icount > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully')</script>", false);
            LoadDate(lblEditUserName.Text);
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        int icount = 0;
        int icountr = 0;
        //for (int i = 0; i < gvWeallyDatewise.Rows.Count; i++)
        //{
        //    Label lblUniquePlanCode = (Label)gvWeallyDatewise.Rows[i].FindControl("lblUniquePlanCode");
        //    CheckBox chkdel = (CheckBox)gvWeallyDatewise.Rows[i].FindControl("chkdel");
        //    if (chkdel.Checked==true)
        //    {
        //        icount = icount + 1;
        //    }
        //}
        //if (gvWeallyDatewise.Rows.Count==icount)
        //{
        if (gvWeallyDatewise.Rows.Count > 0)
        {

        }
        else
        {
            return;
        }
        Int32 Icount = 0;
        if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        {
            Icount = Convert.ToInt32(ddlYear.SelectedValue) + 1;
        }
        else
        {
            Icount = Convert.ToInt32(ddlYear.SelectedValue);
        }
        SqlParameter[] parm1 = new SqlParameter[]
        {
            new SqlParameter("@CreateBy",lblEditUserName.Text),
              new SqlParameter("@Year",Icount),


                 new SqlParameter("@month", ddlMonth.SelectedValue),
                  new SqlParameter("@Flag",ddlWeeklly.SelectedValue),

        };


        DataSet dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadWeekDropdownApprval2026", parm1);
        if (dt.Tables[1].Rows.Count > 0)
        {
            if (Convert.ToInt32(dt.Tables[1].Rows[0]["IsHours"]) != Convert.ToInt32(dt.Tables[0].Rows[0]["TotalHH"]))
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please add plans for each day of the week to approve the plan.')</script>", false);
                return;
            }
        }
        //if (dt.Tables[2].Rows.Count > 0)
        //{

        //    DataTable dth = dt.Tables[2];
        //   // DataTable dtmstGKPCheck = objMain.LoadData(" SELECT   sum(isnull(TotalGKPSchoolSeeion,0))	-sum(isnull(TotalGKPSchoolSeeionach,0)) FROM [rptweekplanSchulder] where  villagecode ='" + lblEditUserName.Text + "'  and Month =" + ddlMonth.SelectedValue + " and [WeekNo] =" + ddlWeeklly.SelectedValue + " ");
        //    for (int k = 0; k < dth.Rows.Count; k++)
        //    {
        //        DataTable dtmstGKPCheck = objMain.LoadData(" SELECT   sum(isnull(TotalGKPSchoolSeeion,0))	-sum(isnull(TotalGKPSchoolSeeionach,0)) as ddd FROM [rptweekplanSchulder] where  villagecode ='" + dth.Rows[k]["villagecode"] + "'  ");
        //        int gkpval =Convert.ToInt32(dtmstGKPCheck.Rows[0]["ddd"]);
        //        if (gkpval>0)
        //        {


        //          DataTable dtmstMGs = objMain.LoadData(" SELECT   schoolcode FROM [tblPlanActivity] where  ActivityID=7 and DeleteFlag =1 and CreateBy='" + lblEditUserName.Text + "'  and Month =" + ddlMonth.SelectedValue + " and [WeekNo] =" + ddlWeeklly.SelectedValue + " ");

        //            if (dtmstMGs.Rows.Count > 0)
        //            {
        //                int IcountM = 0;
        //                for (int i = 0; i < dtmstMGs.Rows.Count; i++)
        //                {
        //                    if (Convert.ToString(dtmstMGs.Rows[0]["schoolcode"]).Length > 30)
        //                    {
        //                        IcountM = IcountM + 2;
        //                    }
        //                    else

        //                    {
        //                        IcountM = IcountM + 1;
        //                    }
        //                }
        //                 if (IcountM < 2)
        //                {
        //                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Planning for at least 2 GKP School in a week will be mandatory..')</script>", false);
        //                    return;
        //                }
        //            }

        //            else
        //            {
        //                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Planning for at least 2 GKP session in a week will be mandatory..')</script>", false);
        //                return;
        //             }
        //         }
        //   }
        //}
        //if (dt.Tables[3].Rows.Count > 0)
        //{
        //    DataTable dth = dt.Tables[3];
        //    // DataTable dtmstGKPCheck = objMain.LoadData(" SELECT   sum(isnull(TotalGKPSchoolSeeion,0))	-sum(isnull(TotalGKPSchoolSeeionach,0)) FROM [rptweekplanSchulder] where  villagecode ='" + lblEditUserName.Text + "'  and Month =" + ddlMonth.SelectedValue + " and [WeekNo] =" + ddlWeeklly.SelectedValue + " ");
        //    for (int k = 0; k < dth.Rows.Count; k++)
        //    {
        //        DataTable dtmstGKPCheck = objMain.LoadData(" SELECT  sum(isnull(TotalBalSchoolSeeion,0))	-sum(isnull(TotalbalSchoolSeeionach,0))  as ddd FROM [rptweekplanSchulder] where  villagecode ='" + dth.Rows[k]["villagecode"] + "'  ");
        //        int Balpval = Convert.ToInt32(dtmstGKPCheck.Rows[0]["ddd"]);
        //        DataTable dtmstMGs = objMain.LoadData(" SELECT  isnull( sum(isnull(LSE,0)),0) gkp FROM [tblPlanActivity] where   ActivityID=8 and  DeleteFlag =1 and CreateBy='" + lblEditUserName.Text + "'  and Month =" + ddlMonth.SelectedValue + " and [WeekNo] =" + ddlWeeklly.SelectedValue + " ");
        //        if (Balpval > 0)
        //        {
        //            if (dtmstMGs.Rows.Count > 0)
        //            {
        //                if (Convert.ToInt32(dtmstMGs.Rows[0]["gkp"]) < 1)
        //                {
        //                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Planning for at least 1 Balsaba session in a week will be mandatory..')</script>", false);
        //                    return;
        //                }
        //            }
        //            else
        //            {

        //                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Planning for at least 1 Balsaba session in a week will be mandatory..')</script>", false);
        //                return;


        //            }
        //        }
        //    }
        //}

        for (int i = 0; i < gvWeallyDatewise.Rows.Count; i++)
        {
            Label lblUniquePlanCode = (Label)gvWeallyDatewise.Rows[i].FindControl("lblUniquePlanCode");
            CheckBox chkdel = (CheckBox)gvWeallyDatewise.Rows[i].FindControl("chkdel");
            icountr = SaveDataApprove(lblUniquePlanCode.Text, Session["username"].ToString());
        }
        if (icountr > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Approve Sucessfully')</script>", false);
            gvWeallyDatewise.DataSource = null;
            gvWeallyDatewise.DataBind();
            ddlWeek_SelectedIndexChanged(ddlActivity, null);
        }
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select All')</script>", false);

        //}
    }
    public int SaveDataApprove(string UniqueCode, string CreateBy)
    {
        int Icount = 0;
        try
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@UniqueCode", UniqueCode),

                 new SqlParameter("@Createby", CreateBy),



            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateContactWeeklyApprove", cmdParameters);
        }
        catch
        {

        }
        return Icount;
    }
    public int SaveDataInsertUpdate(string UniqueCode, string Plandate, string ActivityID, string SupportBy, string BO, string TB, string OOSC, string Remark, string CreateBy, string Meeting, string Travel, string Holiday, string Leave, string Outcome, string SpecificOutcome, string IsHours, string ssschool, string Round, int hhmin, string schoolName)
    {
        int Icount = 0;
        try
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@UniqueCode", UniqueCode),
            new SqlParameter("@Villagecode", ddlVillage.SelectedValue),
            new SqlParameter("@PlanDate", Plandate),
            new SqlParameter("@ActivityID", ddlActivity.SelectedValue),
            new SqlParameter("@SupportBy", SupportBy),
            new SqlParameter("@BOCode", BO),
            new SqlParameter("@TBCode", TB),
            new SqlParameter("@OOSG", OOSC),
            new SqlParameter("@Remark", Remark),
            //  new SqlParameter("@Createby", ddlUser.Text),
         new SqlParameter("@Createby", lblEditUserName.Text),
              new SqlParameter("@Meeting", Meeting),
               new SqlParameter("@Travel", Travel),
                new SqlParameter("@Holiday ", Holiday ),
                 new SqlParameter("@Leave", Leave),
                  new SqlParameter("@Outcome", Outcome),
                   new SqlParameter("@SpecificOutcome ", SpecificOutcome ),
                    new SqlParameter("@IsHalfDay ",hhmin ),
                       new SqlParameter("@IsHours ",IsHours),
                     new SqlParameter("@Week ", ddlWeeklly.SelectedValue ),
                       new SqlParameter("@Month ", ddlMonth.SelectedValue ),
                         new SqlParameter("@ActivityTypeID ", ddlActivity1.SelectedValue ),
                            new SqlParameter("@Enrollment ", txtEnrllment.Text ),
                               new SqlParameter("@SMC", txtsmc.Text ),
                                  new SqlParameter("@GKP", txtGKp.Text ),
                                     new SqlParameter("@LSE", txtBal.Text ),
                                        new SqlParameter("@SchoolCode",ssschool ),
                                             new SqlParameter("@Round","4" ),
                                                    new SqlParameter("@Retention",txtReation.Text ),
                                                           new SqlParameter("@levelType",ddllevelType.SelectedValue ),
                                                             new SqlParameter("@schoolName",schoolName),
                                                                new SqlParameter("@Other",txtOther.Text),



            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateContactWeekly20252026", cmdParameters);
        }
        catch
        {

        }
        return Icount;
    }
    protected void ddlPlanType_Click(object sender, EventArgs e)
    {
        DataTable dt = null;
        SqlParameter[] parm1 = new SqlParameter[]
           {
              // new SqlParameter("@CreateBy",  ddlUser.SelectedValue),
               new SqlParameter("@CreateBy",  lblEditUserName.Text),
                           //new SqlParameter("@CreateBy",  "TestEG1"),
                 new SqlParameter("@month", ddlMonth.SelectedValue),
                      new SqlParameter("@Week", ddlWeeklly.SelectedValue),
                        new SqlParameter("@Flag","4"),
           };

        if (Convert.ToInt32(ddlPlan.SelectedValue) == 4)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptWeeklyRound4Test", parm1);
            gvTopvillageround4.DataSource = dt;
            gvTopvillageround4.DataBind();
            gvTopvillageround4.Visible = true;
            gvTopvillage.Visible = false;
        }
        else
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptContactTopThreeVillage", parm1);
            gvTopvillage.DataSource = dt;
            gvTopvillage.DataBind();
            gvTopvillageround4.Visible = false;
            gvTopvillage.Visible = true;
            if (Convert.ToInt32(ddlPlan.SelectedValue) == 1)
            {
                gvTopvillage.Columns[3].Visible = true;
                gvTopvillage.Columns[4].Visible = false;
                gvTopvillage.Columns[5].Visible = false;
            }
            if (Convert.ToInt32(ddlPlan.SelectedValue) == 2)
            {

                gvTopvillage.Columns[3].Visible = false;
                gvTopvillage.Columns[4].Visible = false;
                gvTopvillage.Columns[5].Visible = true;

            }
            if (Convert.ToInt32(ddlPlan.SelectedValue) == 3)
            {

                gvTopvillage.Columns[3].Visible = false;
                gvTopvillage.Columns[4].Visible = true;
                gvTopvillage.Columns[5].Visible = true;
            }
            if (Convert.ToInt32(ddlPlan.SelectedValue) == 2)
            {

                gvTopvillage.Columns[3].Visible = false;
                gvTopvillage.Columns[4].Visible = false;
                gvTopvillage.Columns[5].Visible = true;
            }
        }
        lblRound.Text = ddlPlan.SelectedValue;
        MpexdrDistrict1.Show();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ddlPlan.SelectedIndex = 0;
        ddlAct.SelectedIndex = 0;
        gvTopvillage.DataSource = null;
        gvTopvillage.DataBind();
        gvTopvillageround4.DataSource = null;
        gvTopvillageround4.DataBind();
        gvTopvillage.Visible = false;
        gvTopvillageround4.Visible = false;

        lblTpye.Text = "Weekly Plan" + "(" + ddlWeeklly.SelectedItem.Text + " " + ")";
        MpexdrDistrict1.Show();
    }

    protected void LnkBtnBlockSc_OnClick(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {

        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string lblVillagecode = (gvr.FindControl("lblVillagecode") as Label).Text;
        objComman.BindDLL("mst5Village", "Villagecode, VillageName ", "Villagecode='" + lblVillagecode + "' ", "VillageName", "asc", ddlVillage, "VillageName", "Villagecode", "--Select--");

        ddlVillage.SelectedIndex = 1;

        lblEditUniquePlanCode.Text = "";
        ddlActivity.SelectedIndex = 0;
        ddlLeave.SelectedIndex = 0;
        ddlHoldday.SelectedIndex = 0;
        ddlOutcomde.SelectedIndex = 0;
        ddlOutcomde_SelectedIndexChanged(ddlOutcomde, null);
        txtMeeting.Text = "";
        txtoosg.Text = "";
        txtPlanDate.Text = "";
        chkBO.Checked = false;
        chkTB.Checked = false;
        txtRemark.Text = "";
        txtReation.Text = "";
        objComman.BindDLL("mstLookup", "LookupCode, Description ", "LookupFlag='W1' and lookupcode in(1,2,3,4,5,6,7) ", "LookupCode", "asc", ddlActivity, "Description", "LookupCode", "--Select--");
        ddlActivity.SelectedValue = ddlAct.SelectedValue;
        ddlActivity.Enabled = false;
        divViallage.Visible = true;

        ddlActivity_SelectedIndexChanged(ddlOutcomde, null);

        DataTable dtmstM = objMain.LoadData(" SELECT TBCode, TBName FROM [dbo].[mstTeamBalika] inner join mst5Village on mst5Village.VillageCode=mstTeamBalika.VillageCode 	or  mst5Village.refVillage16=mstTeamBalika.VillageCode	or  mst5Village.refVillage17=mstTeamBalika.VillageCode	or  mst5Village.refVillage18=mstTeamBalika.VillageCode	or  mst5Village.refVillage19=mstTeamBalika.VillageCode	or  mst5Village.refVillage20=mstTeamBalika.VillageCode	 	or  mst5Village.refVillage21=mstTeamBalika.VillageCode or  mst5Village.refVillage22=mstTeamBalika.VillageCode	or  mst5Village.refVillage23=mstTeamBalika.VillageCode or  mst5Village.refVillage24=mstTeamBalika.VillageCode or  mst5Village.refVillage25=mstTeamBalika.VillageCode  left join mst1State on mst1State.StateCode=mst5Village.StateCode left join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode   left join (select distinct blockcode,blockname from mst3Block) blk ON mst5Village.BlockCode = blk.BlockCode LEFT JOIN (select distinct PanchayatCode,PanchayatName from mstPanchayat) phy  ON mst5Village.PanchayatCode  = phy.PanchayatCode where mst5Village.villagecode= '" + ddlVillage.SelectedValue + "'  and WorkingStatus=1");
        objComman.BindDLLDatatable("mst1State", dtmstM, "TBCode, dbo.TitleCase(upper(TBName)) as TBName", conditions, "TBName", "Desc", ddlTB, "TBName", "TBCode", "--Select--");
        ddlTB.Enabled = false;
        ddlBo.Enabled = false;
        MpexdrDistrict.Show();
    }

    protected void LnkBtnBlockSc1_OnClick(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {

        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string lblVillagecode = (gvr.FindControl("lblVillagecode") as Label).Text;
        objComman.BindDLL("mst5Village", "Villagecode, VillageName ", "Villagecode='" + lblVillagecode + "' ", "VillageName", "asc", ddlVillage, "VillageName", "Villagecode", "--Select--");

        ddlVillage.SelectedIndex = 1;
        ddlActivity.Enabled = false;
        lblEditUniquePlanCode.Text = "";
        ddlActivity.SelectedIndex = 0;
        ddlLeave.SelectedIndex = 0;
        ddlHoldday.SelectedIndex = 0;
        ddlOutcomde.SelectedIndex = 0;
        ddlOutcomde_SelectedIndexChanged(ddlOutcomde, null);
        txtMeeting.Text = "";
        txtoosg.Text = "";
        txtPlanDate.Text = "";
        chkBO.Checked = false;
        chkTB.Checked = false;
        txtRemark.Text = "";
        txtReation.Text = "";
        objComman.BindDLL("mstLookup", "LookupCode, Description ", "LookupFlag='W1' and lookupcode in(1,2,3,4,5,6,7) ", "LookupCode", "asc", ddlActivity, "Description", "LookupCode", "--Select--");

        ddlActivity.SelectedValue = ddlAct.SelectedValue;
        ddlActivity_SelectedIndexChanged(ddlOutcomde, null);
        DataTable dtmstM = objMain.LoadData(" SELECT TBCode, TBName FROM [dbo].[mstTeamBalika] inner join mst5Village on mst5Village.VillageCode=mstTeamBalika.VillageCode 	or  mst5Village.refVillage16=mstTeamBalika.VillageCode	or  mst5Village.refVillage17=mstTeamBalika.VillageCode	or  mst5Village.refVillage18=mstTeamBalika.VillageCode	or  mst5Village.refVillage19=mstTeamBalika.VillageCode	or  mst5Village.refVillage20=mstTeamBalika.VillageCode	 	or  mst5Village.refVillage21=mstTeamBalika.VillageCode or  mst5Village.refVillage22=mstTeamBalika.VillageCode	or  mst5Village.refVillage23=mstTeamBalika.VillageCode or  mst5Village.refVillage24=mstTeamBalika.VillageCode or  mst5Village.refVillage25=mstTeamBalika.VillageCode left join mst1State on mst1State.StateCode=mst5Village.StateCode left join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode   left join (select distinct blockcode,blockname from mst3Block) blk ON mst5Village.BlockCode = blk.BlockCode LEFT JOIN (select distinct PanchayatCode,PanchayatName from mstPanchayat) phy  ON mst5Village.PanchayatCode  = phy.PanchayatCode where mst5Village.villagecode= '" + ddlVillage.SelectedValue + "'  and WorkingStatus=1");
        objComman.BindDLLDatatable("mst1State", dtmstM, "TBCode, dbo.TitleCase(upper(TBName)) as TBName", conditions, "TBName", "Desc", ddlTB, "TBName", "TBCode", "--Select--");
        ddlTB.Enabled = false;
        ddlBo.Enabled = false;
        divViallage.Visible = true;
        lblRound.Text = ddlPlan.SelectedValue;
        MpexdrDistrict.Show();
    }
    protected void GVMain_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "GVMainEdit")
            {

                //   int iIndex = Convert.ToInt32(e.CommandArgument);
                //string Tarining_ID = (GVMain.DataKeys[iIndex]["Tarining_ID"].ToString());

                MpexdrDistrict.Show();
            }
        }
        catch
        {

        }

    }

    protected void ddlPlanType2_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlAct.SelectedValue) == 6)
        {
            div5.Visible = false;
            ddlPlan.SelectedIndex = 0;
            gvTopvillage.DataSource = null;
            gvTopvillage.DataBind();
            gvTopvillageround4.DataSource = null;
            gvTopvillageround4.DataBind();
            gvTopvillage.Visible = false;
            gvTopvillageround4.Visible = false;
            if (ddlBo.SelectedIndex > 0)
            {
                ddlBo.SelectedIndex = 0;
            }
            if (ddlTB.SelectedIndex > 0)
            {
                ddlTB.SelectedIndex = 0;
            }
            ddlTB.Enabled = false;
            ddlBo.Enabled = false;
            MpexdrDistrict1.Show();
            ddlPlanType_Click(ddlAct, null);
        }
        else
        {
            ddlActivity.Enabled = false;
            div5.Visible = false;
            divViallage.Visible = false;
            //ddlVillage.SelectedIndex = 0;

            lblEditUniquePlanCode.Text = "";
            ddlActivity.SelectedIndex = 0;
            ddlLeave.SelectedIndex = 0;
            ddlHoldday.SelectedIndex = 0;
            ddlOutcomde.SelectedIndex = 0;
            ddlOutcomde_SelectedIndexChanged(ddlOutcomde, null);
            txtMeeting.Text = "";
            txtoosg.Text = "";
            txtPlanDate.Text = "";
            chkBO.Checked = false;
            chkTB.Checked = false;
            txtRemark.Text = "";
            txtReation.Text = "";
            if (ddlVillage.SelectedIndex > 0)
            {
                ddlVillage.SelectedIndex = 0;
            }
            if (ddlBo.SelectedIndex > 0)
            {
                ddlBo.SelectedIndex = 0;
            }
            if (ddlTB.SelectedIndex > 0)
            {
                ddlTB.SelectedIndex = 0;
            }
            ddlActivity_SelectedIndexChanged(ddlOutcomde, null);
            if (ddlAct.SelectedValue == "5" || ddlAct.SelectedValue == "7")
            {
                DataTable dtmstM = objMain.LoadData(" SELECT TBCode, TBName FROM [PMS].[dbo].[mstTeamBalika] inner join mst5Village on mst5Village.VillageCode=mstTeamBalika.VillageCode 	or  mst5Village.refVillage16=mstTeamBalika.VillageCode	or  mst5Village.refVillage17=mstTeamBalika.VillageCode	or  mst5Village.refVillage18=mstTeamBalika.VillageCode	or  mst5Village.refVillage19=mstTeamBalika.VillageCode	or  mst5Village.refVillage20=mstTeamBalika.VillageCode	 	or  mst5Village.refVillage21=mstTeamBalika.VillageCode or  mst5Village.refVillage22=mstTeamBalika.VillageCode	or  mst5Village.refVillage23=mstTeamBalika.VillageCode or  mst5Village.refVillage24=mstTeamBalika.VillageCode or  mst5Village.refVillage25=mstTeamBalika.VillageCode left join mst1State on mst1State.StateCode=mst5Village.StateCode left join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode   left join (select distinct blockcode,blockname from mst3Block) blk ON mst5Village.BlockCode = blk.BlockCode LEFT JOIN (select distinct PanchayatCode,PanchayatName from mstPanchayat) phy  ON mst5Village.PanchayatCode  = phy.PanchayatCode where mst5Village.ClusterCode in(select villagecode FROM [mstUser] where Username='" + lblEditUserName.Text + "' ) ");
                objComman.BindDLLDatatable("mst1State", dtmstM, "TBCode, dbo.TitleCase(upper(TBName)) as TBName", conditions, "TBName", "Desc", ddlTB, "TBName", "TBCode", "--Select--");

            }
            //DataTable dtmstM = objMain.LoadData(" SELECT TBCode, TBName FROM [dbo].[mstTeamBalika] inner join mst5Village on mst5Village.VillageCode=mstTeamBalika.VillageCode 	or  mst5Village.refVillage16=mstTeamBalika.VillageCode	or  mst5Village.refVillage17=mstTeamBalika.VillageCode	or  mst5Village.refVillage18=mstTeamBalika.VillageCode	or  mst5Village.refVillage19=mstTeamBalika.VillageCode	or  mst5Village.refVillage20=mstTeamBalika.VillageCode	 	or  mst5Village.refVillage21=mstTeamBalika.VillageCode or  mst5Village.refVillage22=mstTeamBalika.VillageCode	or  mst5Village.refVillage23=mstTeamBalika.VillageCode  left join mst1State on mst1State.StateCode=mst5Village.StateCode left join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode   left join (select distinct blockcode,blockname from mst3Block) blk ON mst5Village.BlockCode = blk.BlockCode LEFT JOIN (select distinct PanchayatCode,PanchayatName from mstPanchayat) phy  ON mst5Village.PanchayatCode  = phy.PanchayatCode where mst5Village.villagecode= '" + ddlVillage.SelectedValue + "' ");
            //objComman.BindDLLDatatable("mst1State", dtmstM, "TBCode, dbo.TitleCase(upper(TBName)) as TBName", conditions, "TBName", "Desc", ddlTB, "TBName", "TBCode", "--Select--");
            ddlTB.Enabled = false;
            ddlBo.Enabled = false;
            ddlActivity.SelectedValue = ddlAct.SelectedValue;
            ddlActivity_SelectedIndexChanged(ddlActivity, null);
            MpexdrDistrict.Show();

        }
    }
}