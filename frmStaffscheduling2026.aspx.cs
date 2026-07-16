using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmStaffscheduling2026 : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    string conditions = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadYear();
            LoadUserLeavel();
            LoadOutCome();
            pnlMain.Enabled = true;
            if (Convert.ToString(Session["username"]) == "PMSAdmin" || Convert.ToString(Session["username"]) == "EGE7557" || Convert.ToString(Session["username"]) == "SuperAdmin")
            {
            }
            else
            {
                CalendarExtender2.StartDate = DateTime.Now.AddDays(0);
                CalendarfffExtender1.StartDate = DateTime.Now.AddDays(0);
            }
        }
    }


    public void LoadOutCome()
    {
        conditions = "  ActiveStatus=1";

        objComman.BindDLL("mstOutcome", "OutcomeID,OutcomeName ", conditions, "OutcomeName", "asc", ddlLearning, "OutcomeName", "OutcomeID", "--Select--");

        ddlLearning.SelectedIndex = 0;


        conditions = " ";
        ddlLearning.SelectedIndex = 0;
        objComman.BindDLL("mstTrainingType", "TrainingID,dbo.TitleCase(upper(TrainingName)) as TrainingName ", "", "TrainingName", "asc", ddlTraining, "TrainingName", "TrainingID", "--Select--");

    }

    public void Filllearning()
    {


        conditions = "";
        conditions = "  ISNULL(TrainingStatus,0)=1 ";
        objComman.BindDLL("mstlearning", "learningID,dbo.TitleCase(upper(learningName)) as learningName ", conditions, "learningName", "asc", ddlLearning, "learningName", "learningID", "--Select--");




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
    protected void Delete_Question_Click1(object sender, EventArgs e)
    {
        //MPEFormName.Show();

        //LinkButton Edit_Question = sender as LinkButton;
        //GridViewRow row = Edit_Question.NamingContainer as GridViewRow;
        //int index = row.RowIndex;


        //string QuestionID = (GvEntry.DataKeys[index].Values["ParticiparticipateName"].ToString());
        //DataTable dtParticiparticipate = null;

        //dtParticiparticipate = ((DataTable)Session["dtEntryDoneBY"]);
        //dtParticiparticipate.Rows.Remove(dtParticiparticipate.Rows[index]);

        //ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Delete Sucessfully')</script>", false);

        //Session["dtEntryDoneBY"] = dtParticiparticipate;
        //GvEntry.DataSource = dtParticiparticipate;
        //GvEntry.DataBind();
        //MPE_Entry.Show();
        //MpexdrDistrict.Show();
    }
    protected void BtnEntry_Click(object sender, EventArgs e)
    {

        //DataTable dtEntryDoneBY = null;


        //if (Session["dtEntryDoneBY"] != null)
        //{
        //    dtEntryDoneBY = ((DataTable)Session["dtEntryDoneBY"]);
        //}
        //else
        //{
        //    dtEntryDoneBY = CreateDataEntry();
        //}
        //if (TextBox1.Text != "")
        //{
        //    string[] words = TextBox1.Text.Trim().Split(',');
        //    foreach (var word in words)
        //    {
        //        if (word.Length > 3)
        //        {
        //            DataRow[] drmain = dtEntryDoneBY.Select("ParticiparticipateName='" + word.Trim() + "'");
        //            if (drmain.Length > 0)
        //            {

        //            }
        //            else
        //            {
        //                DataTable dtP1 = new DataTable();
        //                dtP1 = Get_DataFor1Filter1("LoadParticiparticipate", "11", word.Trim());
        //                if (dtP1.Rows.Count > 0)
        //                {
        //                    DataRow dr;
        //                    dr = dtEntryDoneBY.NewRow();
        //                    dr["ParticiparticipateName"] = word.Trim();
        //                    dr["FormID"] = "0";
        //                    if (dtP1.Rows.Count > 0)
        //                    {
        //                        dr["EntryDoneByName"] = dtP1.Rows[0]["EMPName"].ToString();
        //                    }
        //                    else
        //                    {
        //                        dr["EntryDoneByName"] = string.Empty;
        //                    }
        //                    dtEntryDoneBY.Rows.Add(dr);
        //                }
        //            }
        //        }
        //    }
        //}

        //Session["dtEntryDoneBY"] = dtEntryDoneBY;
        //GvEntry.DataSource = dtEntryDoneBY;
        //GvEntry.DataBind();
        //MPE_Entry.Show();
        //MpexdrDistrict.Show();
    }
    protected void LnkEntry_Click(object sender, EventArgs e)
    {
        //TextBox1.Text = "";
        //DataTable dtParticiparticipate = Session["dtEntryDoneBY"] as DataTable;
        //if (dtParticiparticipate != null)
        //{
        //    if (dtParticiparticipate.Rows.Count > 0)
        //    {
        //        GvEntry.DataSource = dtParticiparticipate;
        //        GvEntry.DataBind();
        //    }
        //    else
        //    {
        //        GvEntry.DataSource = null;
        //        GvEntry.DataBind();
        //    }
        //}
        //else
        //{
        //    GvEntry.DataSource = null;
        //    GvEntry.DataBind();
        //}
        //MPE_Entry.Show();
        //MpexdrDistrict.Show();

    }
    private void GVMainBind()
    {
        if (ddlStype.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Training Type')</script>", false);



            return;
        }
        string str = "";
        string strSpain = "";
        if (ddlYear.SelectedIndex > 0)
        {
            str = "where mst2District.Fyear='" + ddlYear.SelectedItem.Text.ToString() + "'";
        }
        if (ddlState.SelectedValue != null && ddlState.SelectedIndex > 0)
        {
            str += " and mst2District.StateCode='" + ddlState.SelectedValue.ToString() + "'";
        }
        if (ddlDistrict.SelectedValue != null && ddlDistrict.SelectedIndex > 0)
        {
            str = str + "and mst2District.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "'";
        }

        if (ddlStype.SelectedIndex > 0)
        {
            str = str + " and TrainingTypeFlag='" + ddlStype.SelectedValue.ToString() + "'";
        }
        if (ddlState.SelectedValue != null && ddlState.SelectedIndex > 0)
        {
            strSpain += "  mstSpineDistrict.StateCode='" + ddlState.SelectedValue.ToString() + "'";
        }
        if (ddlDistrict.SelectedValue != null && ddlDistrict.SelectedIndex > 0)
        {
            strSpain = strSpain + "and mstSpineDistrict.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "'";
        }
        if (ddlStype.SelectedIndex > 0)
        {
            strSpain = strSpain + " and TrainingTypeFlag='" + ddlStype.SelectedValue.ToString() + "'";
        }
        conditions = "";
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');
        if (ddlYear.SelectedItem.Text == "2016-2017")
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions = "    And FromDate <= '" + Year1[1] + "-03-31'";
            }
        }
        else
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions = "    And FromDate >= '" + Year1[0] + "-04-01' and ToDate<='" + Year1[1] + "-03-31'";


            }
        }

        //  DataTable dtmstM = objMain.LoadData(" SELECT  [tblStaffScheduling].[DistrictCode] ,case when TrainingMode=1 then 'Online Training' when TrainingMode=2 then 'Offline Training'  when TrainingMode=3 then 'Refresher Training' else ''end as TrainingMode ,tblStaffScheduling.LockRecord,ScheduleID,Flag,DistrictName as District   , convert (varchar(10),[FromDate] ,121) as [FromDate]    ,convert (varchar(10),[ToDate] ,121) as [ToDate]   ,case Inducation when 0 then Other else sOutcomeName end as Other   ,mstOutcome.OutcomeName as [Outcome]   ,mstTrainingType .[TrainingName]     ,Userid as [UserName]  FROM [tblStaffScheduling]  inner join mst2District on mst2District.DistrictCode=[tblStaffScheduling].[DistrictCode] inner join mst1State on mst1State.StateCode=mst2District.StateCode   left join mstOutcome on mstOutcome.OutcomeID=[Outcome]    left join mstTrainingType on mstTrainingType.TrainingID=[TrainingType]  left join mstOutcomeSpecific on mstOutcomeSpecific.sOutcomeID=[Inducation]   " + str + " union SELECT  [tblStaffScheduling].[DistrictCode],case when TrainingMode=1 then 'Online Training' when TrainingMode=2 then 'Offline Training'  when TrainingMode=3 then 'Refresher Training' else ''end as TrainingMode  ,tblStaffScheduling.LockRecord,ScheduleID,Flag,DistrictName as District   , convert (varchar(10),[FromDate] ,121) as [FromDate]    ,convert (varchar(10),[ToDate] ,121) as [ToDate]   ,case Inducation when 0 then Other else sOutcomeName end as Other   ,mstOutcome.OutcomeName as [Outcome]   ,mstTrainingType .[TrainingName]     ,Userid as [UserName]  FROM [tblStaffScheduling]  inner join mstSpineDistrict on mstSpineDistrict.DistrictCode=[tblStaffScheduling].[DistrictCode]    left join mstOutcome on mstOutcome.OutcomeID=[Outcome]    left join mstTrainingType on mstTrainingType.TrainingID=[TrainingType]  left join mstOutcomeSpecific on mstOutcomeSpecific.sOutcomeID=[Inducation]  where  " + strSpain + "  " + conditions + "  ");
        SqlParameter[] cmdParameters = new SqlParameter[]
         {

                new SqlParameter("@strM",str),
                   new SqlParameter("@strSpain",strSpain),
                      new SqlParameter("@conditions",conditions),




         };



        DataTable dtmstM = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadStaffScheduling]", cmdParameters);

        //DataTable dt = SqlHelper.GetDataTable(strcon, CommandType.Text, "select schoolcode, Name,PrincipalName,PrincipalContact from mstSchool");
        if (dtmstM.Rows.Count > 0)
        {
            if (Convert.ToString(Session["username"]) == "PMSAdmin" || Convert.ToString(Session["username"]) == "SuperAdmin" || Convert.ToString(Session["username"]) == "EGE7557")
            {
                gvStaffScheduling.Columns[11].Visible = true;
            }
            else
            {
                gvStaffScheduling.Columns[11].Visible = false;
            }
            gvStaffScheduling.DataSource = dtmstM;
            gvStaffScheduling.DataBind();
            ViewState["Serach"] = dtmstM;

        }
        else
        {
            gvStaffScheduling.DataSource = null;
            gvStaffScheduling.DataBind();
            ViewState["Serach"] = "";
        }


    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlTrainerTyep.SelectedValue) == 2)
        {
            EV1.Visible = true;
            EV2.Visible = true;
            EV3.Visible = true;
        }
        else
        {
            EV1.Visible = false;
            EV2.Visible = false;
            EV3.Visible = false;
        }
        MpexdrDistrict.Show();
    }
    public DataTable CreateDataTable()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYear.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYear;
    }

    public DataTable CreateDataDate()
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

    protected void btnParticipate_Click(object sender, EventArgs e)
    {
        string RVal = SetTextBoxFocusSelect(this.Page);
        if (!InterventionSql_Injection(RVal))
        {
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Spurious input detected. Data rejected')</script>", false);
            MpexdrDistrict.Show();
            return;
        }

        DataTable dtParticiparticipate = null;
        if (ddlStype.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Training Type')</script>", false);
            MpexdrDistrict.Show();
            return;

        }
        if (ddlType.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select User Type')</script>", false);
            MpexdrDistrict.Show();
            return;

        }
        if (Session["dtStatffParticiparticipate"] != null)
        {
            dtParticiparticipate = ((DataTable)Session["dtStatffParticiparticipate"]);
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
                    DataRow[] drmain = dtParticiparticipate.Select("ParticipantCode='" + word.Trim() + "'");
                    if (drmain.Length > 0)
                    {

                    }
                    else
                    {
                        DataTable dtP1 = new DataTable();
                        if (ddlType.SelectedValue == "2" || ddlType.SelectedValue == "3")
                        {
                            dtP1 = Get_DataFor1Filter1("LoadStaffParticiparticipate", "1", word.Trim());
                        }
                        else
                        {
                            dtP1 = Get_DataFor1Filter1("LoadStaffParticiparticipate", ddlStype.SelectedValue, word.Trim());
                        }
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

                            dr["TeamBalikaUniqueCode"] = dtP1.Rows[0]["UniqueCode"].ToString();
                            dtParticiparticipate.Rows.Add(dr);

                        }
                    }
                }
            }
        }
        txtParticipate.Text = "";

        Session["dtStatffParticiparticipate"] = dtParticiparticipate;
        GridView1.DataSource = dtParticiparticipate;
        GridView1.DataBind();

        MpexdrDistrict.Show();
    }
    protected void btDownload_Click(object sender, EventArgs e)
    {
        if (ddlState.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select State')</script>", false);
            return;
        }
        if (Convert.ToInt32(ddlStype.SelectedValue) == 2)
        {
            string Con = "";
            DataTable dt = null;
            if (ddlState.SelectedIndex > 0)
            {
                Con = " and V.StateCode='" + ddlState.SelectedValue + "'";
            }
            if (ddlDistrict.SelectedIndex > 0)
            {
                Con += " and V.DistrictCode='" + ddlDistrict.SelectedValue + "'";
            }
            if (Session["user_level_Role"].ToString() == "1")
            {
                dt = LoadEmployeeTB2025(Con);
            }
            else
            {
                dt = LoadEmployeeTB(ddlDistrict.SelectedValue);
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
            DataTable dt = objMain.LoadEmployee(ddlState.SelectedValue, ddlState.SelectedItem.Text, ddlDistrict.SelectedValue, ddlDistrict.SelectedItem.Text);
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
    public void LoadYear()
    {
        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        ddlYear_SelectedIndexChanged(ddlYear, null);
        //}


    }


    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        AlllStateCode();
        if (ddlYear.SelectedIndex > 0)
        {
            ddlState.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlDistrict, null);
            if (Session["user_level_Role"].ToString() == "3" || Session["user_level_Role"].ToString() == "4")
            {
                ddlDistrict.SelectedIndex = 1;
            }


        }
        else
        {
            ddlState.SelectedIndex = 0;
            ddlDistrict.Items.Clear();

        }

    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillCBDist();

    }
    protected void ddlsearchState_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvStaffScheduling.DataSource = null;
        gvStaffScheduling.DataBind();
        ViewState["Serach"] = null;
        FillCBDistSearch();

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
        //    ddlRole.DataSource = dtrole;
        //    ddlRole.DataTextField = "Role";
        //    ddlRole.DataValueField = "Role_Level";
        //    ddlRole.DataBind();
        //    ddlRole.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        //}



    }
    protected void btn_AddEmp(object sender, EventArgs e)
    {
        string a = "";
        //conditions = "where 1=1 and ActiveStatus=1 and UserOnline=1 ";
        //foreach (ListItem item in lstState.Items)
        //{
        //    if (item.Selected)
        //    {

        //        a += "'" + item.Value + "'" + ",";
        //    }
        //}

        //string dist = "";
        //foreach (ListItem item in CBL_Muhula.Items)
        //{
        //    if (item.Selected)
        //    {

        //        dist += "'" + item.Value + "'" + ",";
        //    }
        //}
        //if (dist.Length > 0)
        //{
        //    dist = dist.Substring(0, dist.LastIndexOf(","));
        //}
        //conditions += " and UserLevel<>'24' ";
        //if (a.Length > 0)
        //{
        //    a = a.Substring(0, a.LastIndexOf(","));
        //    conditions += "and  StateCode in( " + a + ")";
        //}
        //if (dist.Length > 0)
        //{

        //    conditions += "and DistrictCode in( " + dist + ")";
        //}

        //if (ddlRole.SelectedIndex > 0)
        //{

        //    conditions += "and UserLevel in( '" + ddlRole.SelectedValue + "' )";
        //}

        //if (ddlType.SelectedIndex > 0)
        //{
        //    if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        //    {
        //        conditions = conditions + " and UserName like '" + txtSearchUser.Text + "%'";
        //    }
        //    if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        //    {
        //        conditions = conditions + " and FristName like '" + txtSearchUser.Text + "%'";
        //    }
        //}
        //DataTable dtUser = objMain.LoadData(" SELECT UserName as UserId, [FristName]+' ('+ UserName +')' as [UserName] from MstUser " + conditions + "");

        //lstUser.DataSource = dtUser;
        //lstUser.DataTextField = "UserName";
        //lstUser.DataValueField = "UserId";
        //lstUser.DataBind();
        //MpexdrDistrict1.Show();
        //MpexdrDistrict.Show();
    }
    protected void btnUser_Click(object sender, EventArgs e)
    {

        //string User = "";
        //string UserName = "";
        //int c = 0;
        //foreach (ListItem item in lstUser.Items)
        //{

        //    if (item.Selected)
        //    {

        //        User += "'" + item.Value + "'" + ",";
        //        UserName += "" + item.Text + "" + ",";
        //        c++;
        //    }


        //}
        //if (c > 5)
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Can not select more then 5')</script>", false);
        //    MpexdrDistrict.Show();
        //    MpexdrDistrict1.Show();
        //    return;

        //}
        //if (User.Length > 0)
        //{
        //    User = User.Substring(0, User.LastIndexOf(","));

        //    conditions = "Where UserName in(" + User + ") ";
        //}
        //DataTable dtUser = objMain.LoadData(" SELECT UserName as UserId, [FristName]+' ('+ UserName +')' as [UserName] from MstUser " + conditions + "");
        ////ddlEmployee.DataSource = dtUser;
        ////ddlEmployee.DataTextField = "UserId";
        ////ddlEmployee.DataValueField = "UserName";
        ////ddlEmployee.DataBind();
        //objComman.BindDLLMasterTable("MstUser", "Type,UserName", dtUser, conditions, "UserName", "asc", ddlEmployee, "UserName", "UserId", "Select");
        //// ddlEmployee.SelectedIndex = 1;
        //TxtEmployee.Visible = true;
        //TxtEmployee.Text = User;
        //if (UserName.Length > 0)
        //{
        //    UserName = UserName.Substring(0, UserName.LastIndexOf(","));

        //}
        //txtEmployeName.Text = UserName;
        //MpexdrDistrict.Show();
    }

    protected void lstState_TextChanged(object sender, EventArgs e)
    {

        //string a = "";

        //foreach (ListItem item in lstState.Items)
        //{
        //    if (item.Selected)
        //    {

        //        a += "'" + item.Value + "'" + ",";
        //    }
        //}
        //if (a.Length > 0)
        //{
        //    a = a.Substring(0, a.LastIndexOf(","));
        //}


        //conditions = "where StateCode in( " + a + ")";

        //DataTable dtTb = objMain.LoadData(" SELECT DistrictCode,  dbo.TitleCase(upper(DistrictName)) as DistrictName from mst2District " + conditions + " and Fyear='" + ddlYear.SelectedItem.Text + "'");


        //CBL_Muhula.DataSource = dtTb;
        //CBL_Muhula.DataTextField = "DistrictName";
        //CBL_Muhula.DataValueField = "DistrictCode";
        //CBL_Muhula.DataBind();



        //MpexdrDistrict1.Show();
        //MpexdrDistrict.Show();
    }
    protected void txtdateto_TextChanged(object sender, EventArgs e)
    {
        DataTable dt = CreateDataTableCon();
        int totalDays = 0;

        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            DateTime startDate = Convert.ToDateTime(txtFromDate.Text);
            DateTime endDate = Convert.ToDateTime(txtToDate.Text);
            DataRow dr;

            if (endDate >= startDate)
            {
                DataTable Holiday = new DataTable();
                string From = String.Format("{0:yyyy-MM-dd}", startDate);
                string To = String.Format("{0:yyyy-MM-dd}", endDate);


                while (startDate <= endDate)
                {
                    dr = dt.NewRow();
                    dr["TodayDate"] = startDate.ToString("yyyy-MM-dd");
                    dr["TodayDay"] = 0;
                    dt.Rows.Add(dr);
                    startDate = startDate.AddDays(1);
                    totalDays++;
                }

            }
        }
        if (totalDays > 7)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select less then or equal  7 Days')</script>", false);
            MpexdrDistrict.Show();
            txtFromDate.Text = "";
            txtToDate.Text = "";
            Gv_Display.DataSource = null;
            Gv_Display.DataBind();
            return;
        }
        Gv_Display.DataSource = dt;
        Gv_Display.DataBind();
        MpexdrDistrict.Show();
    }
    public DataTable CreateDataTableCon()
    {

        DataTable dtCon = new DataTable();
        dtCon.Columns.Add("TodayDate", System.Type.GetType("System.String"));
        dtCon.Columns.Add("TodayDay", System.Type.GetType("System.Int32"));

        return dtCon;
    }
    protected void txtdatefrom_TextChanged(object sender, EventArgs e)
    {

        DataTable dt = CreateDataTableCon();
        int totalDays = 0;

        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            DateTime startDate = Convert.ToDateTime(txtFromDate.Text);
            DateTime endDate = Convert.ToDateTime(txtToDate.Text);
            DataRow dr;

            if (endDate >= startDate)
            {
                DataTable Holiday = new DataTable();
                string From = String.Format("{0:yyyy-MM-dd}", startDate);
                string To = String.Format("{0:yyyy-MM-dd}", endDate);


                while (startDate <= endDate)
                {
                    dr = dt.NewRow();
                    dr["TodayDate"] = startDate.ToString("yyyy-MM-dd");
                    dr["TodayDay"] = 0;
                    dt.Rows.Add(dr);
                    startDate = startDate.AddDays(1);
                    totalDays++;
                }

            }
        }
        if (totalDays > 5)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date Max 5 Day')</script>", false);
            MpexdrDistrict.Show();
            txtFromDate.Text = "";
            txtToDate.Text = "";
            return;
        }
        Gv_Display.DataSource = dt;
        Gv_Display.DataBind();
        MpexdrDistrict.Show();
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

    protected void lnkUser_Click(object sender, EventArgs e)
    {

        FillCBStateSearch();
        ddlType.SelectedIndex = 0;
        //txtSearchUser.Text = "";
        //lstUser.Items.Clear();
        //MpexdrDistrict1.Show();
        //MpexdrDistrict.Show();
    }
    protected void ddlTraingMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlTraingMode.SelectedValue) == 1)
        {
            div2.Visible = false;
            ddlTraining.SelectedIndex = 0;
        }
        else
        {
            div2.Visible = true;
        }
        MpexdrDistrict.Show();
    }
    protected void ddlLearning_SelectedIndexChanged(object sender, EventArgs e)
    {

        divOther.Visible = false;
        divOther1.Visible = false;
        if (Convert.ToInt32(ddlStype.SelectedValue) == 1)
        {
            if (Convert.ToInt32(ddlLearning.SelectedValue) == 15)
            {
                divOther1.Visible = false;
                divOther.Visible = true;
                //   LoadOutComeSpicify();
            }
            else
            {
                divOther1.Visible = true;
                divOther.Visible = false;
                //divOther1.Visible = true;
                LoadOutComeSpicify();
            }
        }
        MpexdrDistrict.Show();
    }
    public void LoadOutComeSpicify()
    {
        conditions = " ";

        objComman.BindDLL("mstOutcomeSpecific", "sOutcomeID,sOutcomeName ", "OutcomeID=" + ddlLearning.SelectedValue + " and ActiveStatus=1", "sOutcomeID", "asc", ddlInducation, "sOutcomeName", "sOutcomeID", "--Select--");

        ddlInducation.SelectedIndex = 0;


    }
    public void FillCBDistSearch()
    {

        conditions = "";
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




    }


    public void FillCBDist()
    {

        conditions = "";
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

            objComman.BindDLLDatatable("mst2District", dtDistrict, "DistrictCode, dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "Desc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

        }
        else
        {

            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        }

    }


    protected void ddlStype_SelectedIndexChanged(object sender, EventArgs e)
    {

        GVMainBind();

    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        GVMainBind();
    }
    protected void btnCop2y_Click(object sender, EventArgs e)
    {
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
    protected void btnSaveNew_Click(object sender, EventArgs e)
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
        string cond = "";
        if (txtFromDate.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter From Date')</script>", false);



            this.txtFromDate.Focus();
            MpexdrDistrict.Show();
            return;

        }
        if (txtToDate.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter To Date')</script>", false);



            this.txtToDate.Focus();
            MpexdrDistrict.Show();
            return;
        }

        if (ddlLearning.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Outcome')</script>", false);


            MpexdrDistrict.Show();
            return;
        }
        if (ddlLearning.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Outcome')</script>", false);


            MpexdrDistrict.Show();
            return;
        }
        if (Convert.ToInt32(ddlStype.SelectedValue) == 1)
        {

            if (ddlInducation.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Specific training')</script>", false);


                MpexdrDistrict.Show();
                return;
            }
        }

        if (ddlTraining.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Training')</script>", false);


            MpexdrDistrict.Show();
            return;
        }


        string UserID = "";
        string UserName = "";
        int c = 0;


        DataTable dtentry = Session["dtStatffParticiparticipate"] as DataTable;
        int Pcount = 0;


        if (dtentry != null)
        {
            if (dtentry.Rows.Count > 0)
            {
                for (int i = 0; i < dtentry.Rows.Count; i++)
                {
                    if (Convert.ToString(dtentry.Rows[i]["ParticipantType"]) == "2")
                    {
                        UserID += "'" + dtentry.Rows[i]["ParticipantCode"] + "'" + ",";

                        string jjj = dtentry.Rows[i]["ParticipantCode"] + "(" + dtentry.Rows[i]["ParticipantName"] + ")";
                        UserName += "" + jjj + "" + ",";
                    }
                    else
                    {
                        Pcount = 1;
                    }
                }

                if (UserID.Length > 0)
                {
                    UserID = UserID.Substring(0, UserID.LastIndexOf(","));
                }
                if (UserName.Length > 0)
                {
                    UserName = UserName.Substring(0, UserName.LastIndexOf(","));
                }
            }
        }
        txtEmployeName.Text = UserName;
        TxtEmployee.Text = UserID;

        if (Pcount == 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Add Participants')</script>", false);


            MpexdrDistrict.Show();
            return;
        }
        if (txtEmployeName.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Add  Trainer')</script>", false);


            MpexdrDistrict.Show();
            return;
        }
        if (GridView1.Rows.Count > 0)
        {

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select add Participants')</script>", false);


            MpexdrDistrict.Show();
            return;

        }
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
        if (ddlLearning.SelectedIndex > 0)
        {
            cond = "Where Outcome='" + ddlLearning.SelectedValue + "'";
        }
        if (FromDate != "" && Todate != "")
        {
            cond = cond + " and FromDate='" + FromDate + "' and Todate='" + Todate + "'";
        }
        if (txtLoaction.Text != "")
        {
            cond = cond + " and Location='" + txtLoaction.Text + "'";
        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            cond = cond + " and DistrictCode='" + ddlDistrict.SelectedValue + "'";
        }
        if (ddlStype.SelectedIndex > 0)
        {
            cond = cond + " and TrainingTypeFlag='" + ddlStype.SelectedValue + "'";
        }
        if (lblShulderID.Text != "0")
        {
            cond = cond + " and ScheduleID<>'" + lblShulderID.Text + "'";
        }
        int Sout = 0;
        if (Convert.ToInt32(ddlStype.SelectedValue) == 1)
        {
            Sout = Convert.ToInt32(ddlInducation.SelectedValue);
        }
        DataTable dtValidate = objComman.LoadData("select * from tblStaffScheduling " + cond + "");

        double Days = Convert.ToDouble(t.TotalDays);
        if (Math.Sign(Days + 1) < 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date less then equal 7 Day')</script>", false);
            MpexdrDistrict.Show();
            return;
        }
        if (Math.Round(Days + 1) > 7)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date less then equal 7 Day')</script>", false);
            MpexdrDistrict.Show();
            return;
        }

        foreach (GridViewRow Itemst in Gv_Display.Rows)
        {
            int ind = Itemst.DataItemIndex;
            DropDownList ddlDay = (DropDownList)Gv_Display.Rows[ind].FindControl("ddlStatus");

            if (ddlDay.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date')</script>", false);
                MpexdrDistrict.Show();
                return;
            }




        }
        if (dtValidate.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Staff Training not allowed')</script>", false);
            MpexdrDistrict.Show();
            return;
        }
        else
        {
            DataTable dtPhase = null;
            if (Convert.ToInt32(ddlStype.SelectedValue) == 1)
            {
                // DataTable dtPhase = objComman.LoadData("select  Case when Phase=1 and  Program_Year=1 THEN N_P1_Y1 WHEN Phase=1 and Program_Year=2 THEN N_P1_Y2 WHEN Phase=1 and Program_Year>=3 THEN  N_P1_Y3 WHEN Phase=2 and  Program_Year<=4 THEN N_P2_Y1 WHEN Phase=2 and Program_Year>=5 THEN N_P2_Y2  WHEN Phase=3 and  Program_Year<=6 THEN N_P3_Y1 WHEN Phase=3 and Program_Year>=7 THEN N_P3_Y2 WHEN Phase=4 and  Program_Year=8 THEN N_P4_Y2 WHEN Phase=4 and Program_Year=9 THEN N_P4_Y3 END as [NoOfDays]  from Tbl_PhaseMapping  p inner join (select * from Tbl_TB_Training Where  TrainingType='S') TS on TS.FYear=p.Financial_Year Where TS.OutComeID='" + ddlLearning.SelectedValue + "' and SoutComeID='" + ddlInducation.SelectedValue + "' and p.StateCode='" + ddlState.SelectedValue + "' and p.DistrictCode='" + ddlDistrict.SelectedValue + "'");
                dtPhase = objComman.LoadData("select  isnull(N_P1_Y1,0) as [NoOfDays]  from Tbl_TB_Training Where  TrainingType='S' and OutComeID='" + ddlLearning.SelectedValue + "' and SoutComeID='" + ddlInducation.SelectedValue + "' and StateCode='" + ddlState.SelectedValue + "' and DistrictCode='" + ddlDistrict.SelectedValue + "'");
            }
            else
            {
                dtPhase = objComman.LoadData("select  isnull(N_P1_Y1,0) as [NoOfDays]  from Tbl_TB_Training Where  TrainingType='T' and LearningID='" + ddlLearning.SelectedValue + "'  and StateCode='" + ddlState.SelectedValue + "' and DistrictCode='" + ddlDistrict.SelectedValue + "'");

            }
            if (dtPhase.Rows.Count > 0)
            {
                if (Convert.ToInt32(dtPhase.Rows[0]["NoOfDays"]) == 0)
                {
                    DateTime fdate1 = Convert.ToDateTime(txtFromDate.Text);
                    DateTime Todate1 = Convert.ToDateTime(txtToDate.Text);
                    SqlParameter[] parm = new SqlParameter[]
                        {
                                          new SqlParameter("@Tarining_ID", lblShulderID.Text),
                        new SqlParameter("@StateCode", ddlState.SelectedValue),
                        new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
                        new SqlParameter("@FromDate", fdate1.ToString("yyyy-MM-dd")),
                        new SqlParameter("@ToDate", Todate1.ToString("yyyy-MM-dd")),
                        new SqlParameter("@Outcome", ddlLearning.SelectedValue),
                        new SqlParameter("@TrainingType", ddlTraining.SelectedValue),
                        new SqlParameter("@UserID", TxtEmployee.Text),
                         new SqlParameter("@UserName", txtEmployeName.Text),
                        new SqlParameter("@Other", txtOther.Text),
                        new SqlParameter("@Inducation", Sout),
                        new SqlParameter("@Location", txtLoaction.Text),
                       new SqlParameter("@TrainingMode", ddlTraingMode.SelectedValue),

                             new SqlParameter("@TrainingTypeFlag", ddlStype.SelectedValue),
                                new SqlParameter("@Createby", Convert.ToString(Session["username"] )),
                                   new SqlParameter("@sTrainerName", txtTrainename.Text),
                          new SqlParameter("@sEmail", txtEmail.Text),
                            new SqlParameter("@sContact", txtContact.Text),
                              new SqlParameter("@TrainerType", ddlTrainerTyep.SelectedValue),




                          };
                    int result = Convert.ToInt32(SqlHelper.ExecuteScaler(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateStaffSchedulingSave2026", parm));
                    if (dtentry.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtentry.Rows.Count; i++)
                        {
                            dtentry.Rows[i]["SchedulerID"] = result;
                        }
                        int Parti_Success = objMain.Insert_participateStatff(result, dtentry);
                    }

                    if (lblShulderID.Text == "0")
                    {
                        foreach (GridViewRow Itemst in Gv_Display.Rows)
                        {

                            int ind = Itemst.DataItemIndex;
                            Label lblUniqueCode = (Label)Gv_Display.Rows[ind].FindControl("lblUniqueCode");
                            DropDownList ddlDay = (DropDownList)Gv_Display.Rows[ind].FindControl("ddlStatus");

                            SqlParameter[] parm5 = new SqlParameter[]
                           {
                                          new SqlParameter("@ScheduleID", result),
                                        new SqlParameter("@ToDate",Convert.ToDateTime( lblUniqueCode.Text).ToString("yyyy-MM-dd")),
                                        new SqlParameter("@TrainingDay",  ddlDay.SelectedValue ),

                              };
                            int resulte = Convert.ToInt32(SqlHelper.ExecuteScaler(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertStaffScheduling", parm5));




                        }
                    }
                    if (result > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully')</script>", false);
                        GVMainBind();
                    }

                }
                else if (Math.Round(Days + 1) == Convert.ToInt32(dtPhase.Rows[0]["NoOfDays"]))
                {


                    DateTime fdate1 = Convert.ToDateTime(txtFromDate.Text);
                    DateTime Todate1 = Convert.ToDateTime(txtToDate.Text);
                    SqlParameter[] parm = new SqlParameter[]
                        {
                                  new SqlParameter("@Tarining_ID", lblShulderID.Text),
                        new SqlParameter("@StateCode", ddlState.SelectedValue),
                        new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
                        new SqlParameter("@FromDate", fdate1.ToString("yyyy-MM-dd")),
                        new SqlParameter("@ToDate", Todate1.ToString("yyyy-MM-dd")),
                        new SqlParameter("@Outcome", ddlLearning.SelectedValue),
                        new SqlParameter("@TrainingType", ddlTraining.SelectedValue),
                        new SqlParameter("@UserID", TxtEmployee.Text),
                                        new SqlParameter("@UserName", txtEmployeName.Text),
                        new SqlParameter("@Other", txtOther.Text),
                        new SqlParameter("@Inducation",Sout),
                        new SqlParameter("@Location", txtLoaction.Text),
                       new SqlParameter("@TrainingMode", ddlTraingMode.SelectedValue),

                             new SqlParameter("@TrainingTypeFlag", ddlStype.SelectedValue),
                                new SqlParameter("@Createby", Convert.ToString(Session["username"] )),
                                   new SqlParameter("@sTrainerName", txtTrainename.Text),
                          new SqlParameter("@sEmail", txtEmail.Text),
                            new SqlParameter("@sContact", txtContact.Text),
                              new SqlParameter("@TrainerType", ddlTrainerTyep.SelectedValue),



                          };
                    int result = Convert.ToInt32(SqlHelper.ExecuteScaler(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateStaffSchedulingSave2026", parm));
                    if (dtentry.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtentry.Rows.Count; i++)
                        {
                            dtentry.Rows[i]["SchedulerID"] = result;
                        }
                        int Parti_Success = objMain.Insert_participateStatff(result, dtentry);
                    }
                    if (lblShulderID.Text == "0")
                    {
                        foreach (GridViewRow Itemst in Gv_Display.Rows)
                        {

                            int ind = Itemst.DataItemIndex;
                            Label lblUniqueCode = (Label)Gv_Display.Rows[ind].FindControl("lblUniqueCode");
                            DropDownList ddlDay = (DropDownList)Gv_Display.Rows[ind].FindControl("ddlStatus");




                            SqlParameter[] parm5 = new SqlParameter[]
                           {
                                          new SqlParameter("@ScheduleID", result),
                                        new SqlParameter("@ToDate",Convert.ToDateTime( lblUniqueCode.Text).ToString("yyyy-MM-dd")),
                                        new SqlParameter("@TrainingDay",  ddlDay.SelectedValue ),

                              };
                            int resulte = Convert.ToInt32(SqlHelper.ExecuteScaler(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertStaffScheduling", parm5));



                        }
                    }
                    if (result > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully')</script>", false);
                        GVMainBind();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Staff Training: Selected Training Days are either less than or greater than " + dtPhase.Rows[0]["NoOfDays"] + " Days')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
            }
            else
            {

                DateTime fdate1 = Convert.ToDateTime(txtFromDate.Text);
                DateTime Todate1 = Convert.ToDateTime(txtToDate.Text);
                SqlParameter[] parm = new SqlParameter[]
                {

                      new SqlParameter("@Tarining_ID", lblShulderID.Text),
                        new SqlParameter("@StateCode", ddlState.SelectedValue),
                        new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
                        new SqlParameter("@FromDate", fdate1.ToString("yyyy-MM-dd")),
                        new SqlParameter("@ToDate", Todate1.ToString("yyyy-MM-dd")),
                        new SqlParameter("@Outcome", ddlLearning.SelectedValue),
                        new SqlParameter("@TrainingType", ddlTraining.SelectedValue),
                        new SqlParameter("@UserID", TxtEmployee.Text),
                         new SqlParameter("@UserName", txtEmployeName.Text),
                        new SqlParameter("@Other", txtOther.Text),
                        new SqlParameter("@Inducation", Sout),
                        new SqlParameter("@Location", txtLoaction.Text),
                       new SqlParameter("@TrainingMode", ddlTraingMode.SelectedValue),

                             new SqlParameter("@TrainingTypeFlag", ddlStype.SelectedValue),
                                new SqlParameter("@Createby", Convert.ToString(Session["username"] )),
                                   new SqlParameter("@sTrainerName", txtTrainename.Text),
                          new SqlParameter("@sEmail", txtEmail.Text),
                            new SqlParameter("@sContact", txtContact.Text),
                              new SqlParameter("@TrainerType", ddlTrainerTyep.SelectedValue),




                 };
                int result = Convert.ToInt32(SqlHelper.ExecuteScaler(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateStaffSchedulingSave2026", parm));
                if (dtentry.Rows.Count > 0)
                {
                    for (int i = 0; i < dtentry.Rows.Count; i++)
                    {
                        dtentry.Rows[i]["SchedulerID"] = result;
                    }
                    int Parti_Success = objMain.Insert_participateStatff(result, dtentry);
                }

                if (lblShulderID.Text == "0")
                {
                    foreach (GridViewRow Itemst in Gv_Display.Rows)
                    {

                        int ind = Itemst.DataItemIndex;
                        Label lblUniqueCode = (Label)Gv_Display.Rows[ind].FindControl("lblUniqueCode");
                        DropDownList ddlDay = (DropDownList)Gv_Display.Rows[ind].FindControl("ddlStatus");




                        SqlParameter[] parm5 = new SqlParameter[]
                       {
                                          new SqlParameter("@ScheduleID", result),
                                        new SqlParameter("@ToDate",Convert.ToDateTime( lblUniqueCode.Text).ToString("yyyy-MM-dd")),
                                        new SqlParameter("@TrainingDay",  ddlDay.SelectedValue ),

                          };
                        int resulte = Convert.ToInt32(SqlHelper.ExecuteScaler(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertStaffScheduling", parm5));



                    }
                }
                if (result > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully')</script>", false);
                    GVMainBind();
                }

            }

        }



    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select District')</script>", false);



            return;
        }

        if (ddlStype.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Training Type')</script>", false);



            return;
        }
        if (Convert.ToString(Session["username"]) == "PMSAdmin" || Convert.ToString(Session["username"]) == "EGE7557" || Convert.ToString(Session["username"]) == "SuperAdmin")
        {
        }
        else
        {
            CalendarExtender2.StartDate = DateTime.Now.AddDays(0);
            CalendarfffExtender1.StartDate = DateTime.Now.AddDays(0);
        }
        if (ddlStype.SelectedValue == "1")
        {
            LoadOutCome();
        }
        else
        {
            Filllearning();
        }


        txtFromDate.Enabled = true;
        txtToDate.Enabled = true;
        Session["dtStatffParticiparticipate"] = null;
        GridView1.DataSource = null;
        GridView1.DataBind();
        ddlType.SelectedIndex = 1;
        ddlTrainerTyep.SelectedIndex = 1;
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtOther.Text = "";
        txtLoaction.Text = "";
        TxtEmployee.Text = "";
        Gv_Display.DataSource = null;
        Gv_Display.DataBind();
        ddlLearning.SelectedIndex = 0;
        lblShulderID.Text = "0";
        txtEmail.Text = "";
        txtContact.Text = "";
        txtTrainename.Text = "";
        ddlTraining.SelectedIndex = 0;
        ddlEmployee.Items.Clear();
        Session["dtEntryDoneBY"] = null;
        divOther1.Visible = false;
        EV1.Visible = false;
        EV2.Visible = false;
        EV3.Visible = false;
        txtParticipate.Text = "";
        ddlTraingMode.SelectedIndex = 0;
        MpexdrDistrict.Show();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
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
            //DataTable dtTb = objMain.LoadData(" SELECT StateCode,  dbo.TitleCase(upper(StateName)) as  StateName FROM [mst1State] union select  StateCode,  dbo.TitleCase(upper(StateName))  +' ('+ 'Spine' +')'  as  StateName  from [mstSpineState] order by Statecode  ");



            //objComman.BindDLLMasterTableVillage("mst1State", "StateCode,StateName", dtTb, conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "Select");

            ddlState.SelectedIndex = 0;

            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            ////   objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");


            //conditions = "UserName='" + Session["username"].ToString() + "' ";
            //string strQry1 = "  SELECT distinct mst1State.StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode inner join mst1State on mst1State.StateCode=mst2District.StateCode where   " + conditions + " union select  StateCode,  dbo.TitleCase(upper(StateName))  +' ('+ 'Spine' +')'  as  StateName  from [mstSpineState]   order by StateName   ";
            //DataTable dtTb = objMain.LoadData(strQry1);

            //// DataTable dtTb = objMain.LoadData(" SELECT StateCode,  dbo.TitleCase(upper(StateName)) as  StateName FROM [mst1State] where " + conditions + " union select  StateCode,  dbo.TitleCase(upper(StateName))  +' ('+ 'Spine' +')'  StateName  from [mstSpineState] order by Statecode  ");



            //objComman.BindDLLMasterTableVillage("mst1State", "StateCode,StateName", dtTb, conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "Select");

            ddlState.SelectedIndex = 1;

            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            //DataTable dtTb = objMain.LoadData(" SELECT StateCode,  dbo.TitleCase(upper(StateName)) as  StateName FROM [mst1State] where " + conditions + " union select  StateCode,  dbo.TitleCase(upper(StateName))  +' ('+ 'Spine' +')'  as  StateName  from [mstSpineState] order by Statecode  ");



            //objComman.BindDLLMasterTableVillage("mst1State", "StateCode,StateName", dtTb, conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "Select");

            ddlState.SelectedIndex = 1;
            ddlState.Enabled = false;
            ddlDistrict.Enabled = false;
        }


        if (Session["user_level_Role"].ToString() == "1")
        {
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "";
            conditions = "StateCode ='" + ddlState.SelectedValue + "'  and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

            //string conditions1 = "StateCode ='" + ddlState.SelectedValue + "' ";

            //DataTable dtTb = objMain.LoadData(" SELECT DistrictCode,  dbo.TitleCase(upper(DistrictName)) as  DistrictName FROM [mst2District] where  " + conditions + "  union select  DistrictCode,  dbo.TitleCase(upper(DistrictName))  +' ('+ 'Spine' +')'  as  DistrictName from mstSpineDistrict  where  " + conditions1 + "   order by DistrictName ");



            //objComman.BindDLLMasterTableVillage("mst2District", "DistrictCode,DistrictName", dtTb, conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");


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
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");


            string conditions1 = "StateCode ='" + ddlState.SelectedValue + "' ";

            // DataTable dtTb = objMain.LoadData(" SELECT DistrictCode,  dbo.TitleCase(upper(DistrictName)) as  DistrictName FROM [mst2District] where  " + conditions + "  union select  DistrictCode,  dbo.TitleCase(upper(DistrictName))  +' ('+ 'Spine' +')'  as  DistrictName from mstSpineDistrict  where  " + conditions1 + "   order by DistrictName ");

            DataTable dtTb = objMain.LoadData(" SELECT DistrictCode,  dbo.TitleCase(upper(DistrictName)) as  DistrictName FROM [mst2District] where  " + conditions + "     order by DistrictName ");



            objComman.BindDLLMasterTableVillage("mst2District", "DistrictCode,DistrictName", dtTb, conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");


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

            ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }





    }

    protected void GridViewEmployee_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        //get the ID of the selected row

        //string id = ((Label)GridViewEmployee.Rows[e.RowIndex].Cells[3].FindControl("LabelID")).Text;

        //DeleteRecord(id); //call the method for delete



        //BindGridView(); // Rebind GridView to reflect changes made

    }

    protected void btnDeleteSelected_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();

        // Example: Get DataTable from ViewState
        if (Session["dtStatffParticiparticipate"] != null)
        {
            dt = (DataTable)Session["dtStatffParticiparticipate"];
        }
        int icount = 0;
        // Loop GridView from bottom to top
        for (int i = GridView1.Rows.Count - 1; i >= 0; i--)
        {
            CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("chkSelect");

            if (chk != null && chk.Checked)
            {
                dt.Rows[i].Delete();
                icount = icount + 1;
            }
        }

        dt.AcceptChanges();

        // Save again in ViewState
        Session["dtStatffParticiparticipate"] = dt;

        if (icount == 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select any one')</script>", false);

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Delete Successfully')</script>", false);

        }
        // Bind GridView
        GridView1.DataSource = dt;
        GridView1.DataBind();

        MpexdrDistrict.Show();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string UniqueChildCode = (gvr.FindControl("lblScheduleID") as Label).Text;
        SqlParameter[] parm = new SqlParameter[]
            {


            new SqlParameter("@ScheduleID",UniqueChildCode),
              new SqlParameter("@UserName",Session["username"].ToString()),



              };
        int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteStaffScheduling2026", parm);
        if (result > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Delete Successfully')</script>", false);
            GVMainBind();
        }
    }

    protected void Delete_Question_Click2(object sender, EventArgs e)
    {
        //MPEFormName.Show();

        LinkButton Edit_Question = sender as LinkButton;
        GridViewRow row = Edit_Question.NamingContainer as GridViewRow;
        int index = row.RowIndex;


        string QuestionID = (GridView1.DataKeys[index].Values["ParticipantCode"].ToString());
        DataTable dtParticiparticipate = null;
        //if (Convert.ToString(lblShulderID.Text) != "0")
        //{
        //    int Tarining_ID =Convert.ToInt32( lblShulderID.Text);

        //    int deleteTSD1 = DeleteAssmentQuestion(Tarining_ID.ToString(), QuestionID.Trim());

        //}
        dtParticiparticipate = ((DataTable)Session["dtStatffParticiparticipate"]);
        dtParticiparticipate.Rows.Remove(dtParticiparticipate.Rows[index]);

        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Delete Sucessfully')</script>", false);

        Session["dtStatffParticiparticipate"] = dtParticiparticipate;
        GridView1.DataSource = dtParticiparticipate;
        GridView1.DataBind();

        MpexdrDistrict.Show();
    }
    public int DeleteAssmentQuestion(string FormID, string ParticiparticipateName)
    {
        int Icount = 0;
        try
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@FormID", FormID),

              new SqlParameter("@ParticiparticipateName", ParticiparticipateName),


            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteStaffQuestion", cmdParameters);
        }
        catch
        {

        }
        return Icount;
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {

        if (ddlStype.SelectedValue == "1")
        {
            LoadOutCome();
        }
        else
        {
            Filllearning();
        }


        txtFromDate.Enabled = true;
        txtToDate.Enabled = true;
        Session["dtStatffParticiparticipate"] = null;
        GridView1.DataSource = null;
        GridView1.DataBind();
        ddlType.SelectedIndex = 1;
        ddlTrainerTyep.SelectedIndex = 1;
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtOther.Text = "";
        txtLoaction.Text = "";
        TxtEmployee.Text = "";
        Gv_Display.DataSource = null;
        Gv_Display.DataBind();
        ddlLearning.SelectedIndex = 0;

        txtEmail.Text = "";
        txtContact.Text = "";
        txtTrainename.Text = "";
        ddlTraining.SelectedIndex = 0;
        ddlEmployee.Items.Clear();
        Session["dtEntryDoneBY"] = null;
        divOther1.Visible = false;
        EV1.Visible = false;
        EV2.Visible = false;
        EV3.Visible = false;
        txtParticipate.Text = "";
        ddlTraingMode.SelectedIndex = 0;
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string UniqueChildCode = (gvr.FindControl("lblScheduleID") as Label).Text;


        lblShulderID.Text = UniqueChildCode;

        DataTable dtPhase = objComman.LoadData("select * from [tblStaffScheduling] Where   ScheduleID='" + lblShulderID.Text + "' ");
        if (dtPhase.Rows.Count > 0)
        {
            DateTime StartDate = Convert.ToDateTime(dtPhase.Rows[0]["FromDate"].ToString());
            txtFromDate.Text = StartDate.ToString("dd/MM/yyyy");
            txtFromDate.Enabled = false;
            txtToDate.Enabled = false;

            DateTime EnDate = Convert.ToDateTime(dtPhase.Rows[0]["ToDate"].ToString());

            txtToDate.Text = EnDate.ToString("dd/MM/yyyy");

            ddlLearning.SelectedValue = dtPhase.Rows[0]["Outcome"].ToString();
            ddlLearning_SelectedIndexChanged(ddlLearning, null);
            ddlInducation.SelectedValue = dtPhase.Rows[0]["Inducation"].ToString();
            txtLoaction.Text = dtPhase.Rows[0]["Location"].ToString();
            ddlTraingMode.SelectedValue = dtPhase.Rows[0]["TrainingMode"].ToString();
            ddlTraingMode_SelectedIndexChanged(ddlLearning, null);



            ddlTraining.SelectedValue = dtPhase.Rows[0]["TrainingType"].ToString();

            ddlTrainerTyep.SelectedValue = dtPhase.Rows[0]["TrainerType"].ToString();
            ddlType_SelectedIndexChanged(ddlLearning, null);
            txtTrainename.Text = dtPhase.Rows[0]["sTrainerName"].ToString();
            txtEmail.Text = dtPhase.Rows[0]["sEmail"].ToString();
            txtContact.Text = dtPhase.Rows[0]["sContact"].ToString();

        }
        DataTable dtStatffParticiparticipate = objComman.LoadData("select 		  SchedulerID	,ParticipantType,case when  ParticipantType=1 then 'Participants'  when  ParticipantType=2 then 'Trainer' when ParticipantType=3 then 'Observer' else '' end ParticipantTypeName ,	ParticipantCode	,ParticipantName,	UserType,TeamBalikaUniqueCode from [tblTrainingParticipant] Where   SchedulerID='" + lblShulderID.Text + "' ");

        Session["dtStatffParticiparticipate"] = dtStatffParticiparticipate;
        GridView1.DataSource = dtStatffParticiparticipate;
        GridView1.DataBind();
        MpexdrDistrict.Show();
        //SqlParameter[] parm = new SqlParameter[]
        //    {


        //    new SqlParameter("@ScheduleID",UniqueChildCode),
        //      new SqlParameter("@UserName",Session["username"].ToString()),



        //      };
        //int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteStaffScheduling2023", parm);
        //if (result > 0)
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Delete Successfully')</script>", false);
        //    GVMainBind();
        //}
    }
    protected void btnLnk_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string UniqueChildCode = (gvr.FindControl("lblScheduleID") as Label).Text;
        string Status = (gvr.FindControl("lnkLock") as LinkButton).Text;

        Int32 iStatus = 0;
        if (Status == "Lock")
        {
            iStatus = 1;
        }
        SqlParameter[] parm = new SqlParameter[]
            {


            new SqlParameter("@ScheduleID",UniqueChildCode),

            new SqlParameter("@Status",iStatus),




              };
        int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "UpdateStaffSchedulingFlag", parm);
        if (result > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Save Successfully')</script>", false);
            GVMainBind();
        }
    }

    protected void gvStaffScheduling_OnRowCommand(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblFlag = (Label)e.Row.FindControl("lblFlag");
            Label AssmentFlag = (Label)e.Row.FindControl("lblAssmentFlag");

            LinkButton ButtonDelete = (LinkButton)e.Row.FindControl("ButtonDelete");
            Label lblLockRecord = (Label)e.Row.FindControl("lblLockRecord");
            LinkButton lnkLock = (LinkButton)e.Row.FindControl("lnkLock");
            LinkButton ButtonEdit = (LinkButton)e.Row.FindControl("ButtonEdit");
            if (lblLockRecord.Text == "1")
            {
                lnkLock.Text = "Unlock";
            }
            else
            {
                lnkLock.Text = "Lock";
            }
            if (lblFlag.Text == "2" && AssmentFlag.Text == "1")
            {
                e.Row.BackColor = Color.LightSkyBlue;
                ButtonDelete.Enabled = false;
                ButtonEdit.Enabled = false;
                //lnkLock.ForeColor = Color.White;
            }
            else if (lblFlag.Text == "2")
            {
                e.Row.BackColor = Color.LightCyan;
                ButtonDelete.Enabled = false;
                ButtonEdit.Enabled = false;
                //lnkLock.ForeColor = Color.White;
            }
            else if (AssmentFlag.Text == "1")
            {
                e.Row.BackColor = Color.LightSkyBlue;
                ButtonDelete.Enabled = false;
                ButtonEdit.Enabled = false;
                //lnkLock.ForeColor = Color.White;
            }
            else
            {

            }
        }
    }

    protected void Gv_Display_OnRowCommand(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");

            ddlStatus.SelectedIndex = 1;
        }
    }
    public void AnnaualFCReportss(Int32 Flag)
    {
        conditions = "";



        if (ddlStype.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Training Type')</script>", false);



            return;
        }
        string str = "";
        string strSpain = "";
        if (ddlYear.SelectedIndex > 0)
        {
            str = "where mst2District.Fyear='" + ddlYear.SelectedItem.Text.ToString() + "'";
        }
        if (ddlState.SelectedValue != null && ddlState.SelectedIndex > 0)
        {
            str += " and mst2District.StateCode='" + ddlState.SelectedValue.ToString() + "'";
        }
        if (ddlDistrict.SelectedValue != null && ddlDistrict.SelectedIndex > 0)
        {
            str = str + "and mst2District.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "'";
        }

        if (ddlStype.SelectedIndex > 0)
        {
            str = str + " and TrainingTypeFlag='" + ddlStype.SelectedValue.ToString() + "'";
        }
        if (ddlState.SelectedValue != null && ddlState.SelectedIndex > 0)
        {
            strSpain += "  mstSpineDistrict.StateCode='" + ddlState.SelectedValue.ToString() + "'";
        }
        if (ddlDistrict.SelectedValue != null && ddlDistrict.SelectedIndex > 0)
        {
            strSpain = strSpain + "and mstSpineDistrict.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "'";
        }
        if (ddlStype.SelectedIndex > 0)
        {
            strSpain = strSpain + " and TrainingTypeFlag='" + ddlStype.SelectedValue.ToString() + "'";
        }
        conditions = "";
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');
        if (ddlYear.SelectedItem.Text == "2016-2017")
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions = "    And FromDate <= '" + Year1[1] + "-03-31'";
            }
        }
        else
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions = "    And FromDate >= '" + Year1[0] + "-04-01' and ToDate<='" + Year1[1] + "-03-31'";


            }
        }

        //  DataTable dtmstM = objMain.LoadData(" SELECT  [tblStaffScheduling].[DistrictCode] ,case when TrainingMode=1 then 'Online Training' when TrainingMode=2 then 'Offline Training'  when TrainingMode=3 then 'Refresher Training' else ''end as TrainingMode ,tblStaffScheduling.LockRecord,ScheduleID,Flag,DistrictName as District   , convert (varchar(10),[FromDate] ,121) as [FromDate]    ,convert (varchar(10),[ToDate] ,121) as [ToDate]   ,case Inducation when 0 then Other else sOutcomeName end as Other   ,mstOutcome.OutcomeName as [Outcome]   ,mstTrainingType .[TrainingName]     ,Userid as [UserName]  FROM [tblStaffScheduling]  inner join mst2District on mst2District.DistrictCode=[tblStaffScheduling].[DistrictCode] inner join mst1State on mst1State.StateCode=mst2District.StateCode   left join mstOutcome on mstOutcome.OutcomeID=[Outcome]    left join mstTrainingType on mstTrainingType.TrainingID=[TrainingType]  left join mstOutcomeSpecific on mstOutcomeSpecific.sOutcomeID=[Inducation]   " + str + " union SELECT  [tblStaffScheduling].[DistrictCode],case when TrainingMode=1 then 'Online Training' when TrainingMode=2 then 'Offline Training'  when TrainingMode=3 then 'Refresher Training' else ''end as TrainingMode  ,tblStaffScheduling.LockRecord,ScheduleID,Flag,DistrictName as District   , convert (varchar(10),[FromDate] ,121) as [FromDate]    ,convert (varchar(10),[ToDate] ,121) as [ToDate]   ,case Inducation when 0 then Other else sOutcomeName end as Other   ,mstOutcome.OutcomeName as [Outcome]   ,mstTrainingType .[TrainingName]     ,Userid as [UserName]  FROM [tblStaffScheduling]  inner join mstSpineDistrict on mstSpineDistrict.DistrictCode=[tblStaffScheduling].[DistrictCode]    left join mstOutcome on mstOutcome.OutcomeID=[Outcome]    left join mstTrainingType on mstTrainingType.TrainingID=[TrainingType]  left join mstOutcomeSpecific on mstOutcomeSpecific.sOutcomeID=[Inducation]  where  " + strSpain + "  " + conditions + "  ");
        SqlParameter[] cmdParameters = new SqlParameter[]
         {

                new SqlParameter("@strM",str),
                   new SqlParameter("@strSpain",strSpain),
                      new SqlParameter("@conditions",conditions),




         };



        DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadStaffSchedulingReport]", cmdParameters);
        //      conditions = "   ";
        //      conditions = " where 1 =1 ";
        //      string conditions1 = "  ";


        //      if (ddlState.SelectedIndex > 0)
        //      {
        //          conditions = conditions + "  and  D.StateCode ='" + ddlState.SelectedValue + "' ";
        //      }
        //      if (ddlDistrict.SelectedIndex > 0)
        //      {
        //          conditions = conditions + " and D.DistrictCode ='" + ddlDistrict.SelectedValue + "' ";
        //      }
        //      if (ddlState.SelectedIndex > 0)
        //      {
        //          conditions1 = conditions1 + "  and  D.StateCode ='" + ddlState.SelectedValue + "' ";
        //      }
        //      if (ddlDistrict.SelectedIndex > 0)
        //      {
        //          conditions1 = conditions1 + " and D.DistrictCode='" + ddlDistrict.SelectedValue + "' ";
        //      }

        //      string Year = ddlYear.SelectedItem.Text;
        //      string[] Year1 = Year.Split('-');
        //      if (ddlYear.SelectedIndex > 0)
        //      {
        //          conditions += "    And tblStaffScheduling.FromDate >= '" + Year1[0] + "-04-01' and tblStaffScheduling.ToDate<='" + Year1[1] + "-03-31'";
        //          conditions1 += "    And tblStaffScheduling.FromDate >= '" + Year1[0] + "-04-01' and tblStaffScheduling.ToDate<='" + Year1[1] + "-03-31'";

        //      }
        //      SqlParameter[] cmdParameters = new SqlParameter[]
        //{
        //	new SqlParameter("@Con",conditions),



        //};
        //      DataTable dataTable = null;


        //      dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptStaffTrainingScheduling2020]", cmdParameters);

        ViewState["dt"] = dataTable;
        if (dataTable.Rows.Count > 0)
        {
            ExporttoExcel(dataTable, "StafftrainingSchedularReport");


        }



    }
    protected void BtnEntry2_Click(object sender, EventArgs e)
    {

        string TrainUserdID = "";
        //foreach (GridViewRow Itemst in GvEntry.Rows)
        //{

        //    int ind = Itemst.DataItemIndex;
        //    TrainUserdID += "" + GvEntry.DataKeys[ind]["ParticiparticipateName"].ToString() + ",";



        //}
        //if (TrainUserdID.Length > 0)
        //{
        //    TrainUserdID = TrainUserdID.Substring(0, TrainUserdID.LastIndexOf(","));
        //}
        lblUsername2.Text = TrainUserdID;
        MpexdrDistrict.Show();
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        AnnaualFCReportss(1);
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
}