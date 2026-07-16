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
using System.Collections;


public partial class frmTeamBalikaTbtraining : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    ArrayList arraylist1 = new ArrayList();
    ArrayList arraylist2 = new ArrayList();
    string conditions = "";
    Boolean Flag = false;
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

                FillCBState();
                //LoadData();
                //GVMainBind();
            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }

        }
       
     

     
    }

    public void LoadData()
    {
       
            DataTable dt = objMain.LoadTeamBalikTraining(ViewState["TBCode"].ToString());
            if (dt.Rows.Count > 0)
            {

                Session["Search"] = dt;
                gvnroll.DataSource = dt;
                gvnroll.DataBind();
                //gvnroll.Columns[0].Visible = false;
                //gvnroll.Columns[1].Visible = false;

                //Save_Update(0);

            }
        
    }



    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillCBDistSearch();
    }
    protected void ddlDist_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillCBBockSearch();
        FillCBBock();
    }
    public void FillCBBockSearch()
    {
        conditions = "";

        //conditions = "DistrictCode ='" + ddlDist.SelectedValue + "' and  DividedBlock=1 ";

        //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlMainBlock, "BlockName", "BlockCode", "--Select--");



    }

    public void FillCBDistSearch()
    {
        conditions = "";


       // conditions = "StateCode ='" + ddlState.SelectedValue + "'";


        //  objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDist, "DistrictName", "DistrictCode", "--Select--");



    }
    public void FillCBState()
    {
        //conditions = "";
        //objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");
        //ddlState.SelectedIndex = 1;
        //ddlState_SelectedIndexChanged(ddlState, null);

    }

    public void FillTrainingType()
    {
        conditions = "";
        objComman.BindDLL("mstTrainingType", "TrainingID,dbo.TitleCase(upper(TrainingName)) as TrainingName ", conditions, "TrainingName", "asc", ddlTraining, "TrainingName", "TrainingID", "--Select--");



    }

    public void Filllearning()
    {
        conditions = "";
        objComman.BindDLL("mstlearning", "learningID,dbo.TitleCase(upper(learningName)) as learningName ", conditions, "learningName", "asc", ddlLearning, "learningName", "learningID", "--Select--");



    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillCVillage();
    }
    public void FillCVillage()
    {
        conditions = "";
        //conditions = "DistrictCode ='" + ddlDist.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "'   ";
        //objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--Select--");



    }


    protected void gvnroll_RowDataBound(object sender, GridViewRowEventArgs e)
    {



        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblTbid = (Label)e.Row.FindControl("lblTBID");
            Label lblUniqueCode = (Label)e.Row.FindControl("lblUniqueCode");

            CheckBox ChkClose = (CheckBox)e.Row.FindControl("ChkClose");

            Label lblTBDate = (Label)e.Row.FindControl("lblTBDate");

            ChkClose.Checked = true;


            string Dateof = lblTBDate.Text;
            string[] b = Dateof.Split('/');
            string tDate = b[2] + '-' + b[1] + '-' + b[0];
            DataTable dtTb = objMain.LoadData(" SELECT * from [tblAttendance] where [AttUniqueCode] ='" + lblUniqueCode.Text + "' and [TBId] ='" + lblTbid.Text + "' and [AttDate] ='" + tDate + "' and [Status]=0 ");
            if (dtTb.Rows.Count > 0)
            {
                ChkClose.Checked = false;

            }
            //int index = e.Row.RowIndex;
            //DataTable dt = Session["Search"] as DataTable;
            //foreach (DataRow row_ in dt.Rows)
            //{
            //    DataRow row = row_;
            //    Int32 icount = 0;
            //    foreach (DataColumn col_ in dt.Columns)
            //    {
            //        DataColumn col = col_;

            //        if (icount > 5)
            //        {
            //            int rcount = icount;
            //            string name = "txtAtt" + rcount;
            //            CheckBox gg = new CheckBox();
            //            gg.ID = name;
            //            gg.EnableViewState = true;
            //            e.Row.Cells[rcount].Controls.Add(gg);
            //            gg.Checked = true;

            //        }
            //        icount = icount + 1;
            //    }

            //    break;
            //}


            //e.Row.Cells[0].Visible = false;

            //e.Row.Cells[1].Visible = false;

        }
        
    }


    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillCBBock();
    }
    public void FillCBBock()
    {
        conditions = "";

        //  conditions = "DistrictCode ='" + ddlDist.SelectedValue + "' and  DividedBlock=1 ";

        //  objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");



    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        GVMainBindSearch();

    }
    protected void btnNewSerach_Click(object sender, EventArgs e)
    {
        //gvnroll.Visible = true;
        //LoadData();

     //   RefreshControl();
        GVMainBind();
        
    }
    private void GVMainBind()
    {


        string str = "where Learningtype='" + ddlLearning.SelectedValue.ToString() + "'";


        if (ddlTraining.SelectedValue != null && ddlTraining.SelectedIndex > 0)
        {
            str = "and TrainingType='" + ddlTraining.SelectedValue.ToString() + "'";
        }
        DataTable dtTb = objMain.LoadData(" SELECT UniqueCode,  BlockName as DistrictName, convert (varchar(10),[FromDate] ,121) as [FromDate], convert (varchar(10),todate ,121) as todate  ,learningName   FROM [tblTraining]    inner join mst2District on mst2District.DistrictCode=[DistCode]  inner join mst3Block on mst3Block.BlockCode=tblTraining.[BlockCode] inner join mstlearning on mstlearning.learningID=Learningtype " + str + " group by UniqueCode,  BlockName , DistrictName,  FromDate, todate  ,learningName ");
        if (dtTb.Rows.Count > 0)
        {
            GVMain.DataSource = dtTb;

            GVMain.DataBind();
        }
        else
        {
            GVMain.DataSource = null;

            GVMain.DataBind();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {


        //ViewState["Save"] = "Save";
        //RefreshControl();

        //btnsave.Enabled = true;


    }
    private void RefreshControl()
    {


        Session["TB"] = null;

        ViewState["dtselect"] = null;
        ViewState["TBCode"] = null;
        ViewState["dtselected"] = null;

        ddlTraining.SelectedIndex = 0;
        ddlLearning.SelectedIndex = 0;




    }
    private void GVMainBindSearch()
    {

        string str = "";


        //if (ddlDist.SelectedValue != null && ddlDist.SelectedIndex > 0)
        //{
        //    str = "where  mst5Village.DistrictCode='" + ddlDist.SelectedValue.ToString() + "'";
        //}

        //if (ddlBlock.SelectedValue != null && ddlBlock.SelectedIndex > 0)
        //{
        //    str = str + "and mst5Village.BlockCode='" + ddlBlock.SelectedValue.ToString() + "'";
        //}


        //if (ddlVillage.SelectedValue != null && ddlVillage.SelectedIndex > 0)
        //{
        //    str = str + "and mst5Village.VillageCode='" + ddlVillage.SelectedValue.ToString() + "'";
        //}
        DataTable dtTb = new DataTable();
        if (ViewState["Save"].ToString() == "Edit")
        {

            dtTb = objMain.LoadData(" SELECT  mst3Block.BlockName  , TBCode, TBName,UniqueCode,VillageName FROM [dbo].[mstTeamBalika] inner join mst5Village on mst5Village.VillageCode=mstTeamBalika.VillageCode inner join mst3Block on mst3Block.BlockCode=mst5Village.BlockCode " + str + "  and UniqueCode not in(select TBID from [tblTrainingDetail] where TBUniqueCode='" + ViewState["TBCode"].ToString() + "') group by  mst3Block.BlockName  , TBCode, TBName,UniqueCode,VillageName  ");

        }
        else
        {
            dtTb = objMain.LoadData(" SELECT  mst3Block.BlockName  , TBCode, TBName,UniqueCode,VillageName FROM [dbo].[mstTeamBalika] inner join mst5Village on mst5Village.VillageCode=mstTeamBalika.VillageCode inner join mst3Block on mst3Block.BlockCode=mst5Village.BlockCode " + str + " group by  mst3Block.BlockName  , TBCode, TBName,UniqueCode,VillageName");


        }

        //gvSerach.DataSource = dtTb;


        //gvSerach.DataBind();
        ViewState["dtselect"] = dtTb;
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
        LoadData();
        gvnroll.Visible = true;

    }
    private void Save_Update(int SchoolCode)
    {
        string TSDInsertQuery = "";
        bool InsertTSD1=false;

        for (int intIndex = 0; intIndex <= gvnroll.Rows.Count - 1; intIndex++)
        {

            Label lblTBDate = (Label)gvnroll.Rows[intIndex].Cells[0].FindControl("lblTBDate");

            CheckBox chk = (CheckBox)gvnroll.Rows[intIndex].Cells[0].FindControl("ChkClose");
            Label lblTbid = (Label)gvnroll.Rows[intIndex].Cells[0].FindControl("lblTBID");
            int attcha = 0;
            if (chk.Checked == true)
            {
                attcha = 1;
            }
            string Dateof = lblTBDate.Text;
            string[] b = Dateof.Split('/');
            string tDate = b[2] + '-' + b[1] + '-' + b[0];

            int mainResult = objMain.insert_Attendeace(ViewState["TBCode"].ToString(), lblTbid.Text, Convert.ToDateTime(tDate), attcha);

           
        }
        if (InsertTSD1 == true)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
        }

    }
    protected void GVMain_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "GVUIO")
        {
            Flag = true;
            int iIndex = Convert.ToInt32(e.CommandArgument);
            string TBCode = GVMain.DataKeys[iIndex]["UniqueCode"].ToString();
            ViewState["TBCode"] = TBCode;
            gvnroll.Visible = true;
           
            LoadData();
            btnAdd_Click(btnAdd, null);
        }
    }

    protected void gvTb_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chk = (CheckBox)e.Row.FindControl("ChkClose");
            Label lblTbid = (Label)e.Row.FindControl("lblTbid");
            DataRow[] drItem;
            if (Session["TB"] != null)
            {
                DataTable dtCurrentTable = (DataTable)Session["TB"];

                drItem = dtCurrentTable.Select("TBId ='" + lblTbid.Text + "' and AttUniqueCode='" + ViewState["TBCode"].ToString() + "' ");

                if (drItem.Length > 0)
                {
                    if (drItem[0]["Status"].ToString() == "True")
                    {
                        chk.Checked = true;
                    }
                    else
                    {
                        chk.Checked = false;
                    }
                }
            }



        }

    }
    private void FillControls(string ptCOde)
    {
        DataTable dtmstM = null;
        //if (Session["user_level"].ToString() == "1")
        //{
        dtmstM = objMain.LoadData(" SELECT  [UniqueCode] ,Status   ,mst2District.StateCode  ,[Learningtype]  ,[TrainingType]   ,[DistCode] ,BlockCode  ,[FromDate]   ,[ToDate]  FROM [tblTraining]  inner join mst2District on mst2District.DistrictCode=[DistCode] where UniqueCode ='" + ptCOde + "'");
        //}
        //if (Session["user_level"].ToString() == "2")
        //{
        //    dtmstM = objMain.LoadData(" SELECT mstSchool.[VillageCode],Status,mst5Village.[StateCode],mst5Village.[DistrictCode] ,mst5Village.[BlockCode] ,mst5Village.[PanchayatCode] ,mst5Village.VillageCode +'-'+ [SchoolCodeId] as UniqueId,[SchoolCode] ,[SchoolCodeId]  ,[DISECode1] as DISECode ,[Name1] as Name ,mstSchool.[NameLocalLng] ,[Address] ,[SchoolLevel1] as SchoolLevel ,[PrincipalName1] as PrincipalName  ,[PrincipalContact1] as PrincipalContact ,[BhamashahName] ,[TeacherContactNo1] as TeacherContactNo ,[TeacherName1] as TeacherName FROM [dbo].[mstSchool] inner join mst5Village on mst5Village.VillageCode=mstSchool.VillageCode where schoolcode ='" + pSchoolCOde + "'");
        //}
        if (dtmstM.Rows.Count > 0)
        {
         
            FillCBDistSearch();
            //   ddlDist.SelectedValue = dtmstM.Rows[0]["DistCode"].ToString();
            FillCBBockSearch();
            FillCBBock();
            //   ddlMainBlock.SelectedValue = dtmstM.Rows[0]["BlockCode"].ToString();
            ddlLearning.SelectedValue = dtmstM.Rows[0]["Learningtype"].ToString();

            ddlTraining.SelectedValue = dtmstM.Rows[0]["TrainingType"].ToString().Trim();



            if (dtmstM.Rows[0]["Status"].ToString().Trim() == "2")
            {
                btnsave.Enabled = false;
            }
            else
            {
                btnsave.Enabled = true;
            }
            DateTime fDate = Convert.ToDateTime(dtmstM.Rows[0]["FromDate"].ToString());
            //txtFromDate.Text = fDate.ToString("dd/MM/yyy");

            //DateTime tDate = Convert.ToDateTime(dtmstM.Rows[0]["ToDate"].ToString());
            //txtToDate.Text = tDate.ToString("dd/MM/yyy");

            //DataTable dtTmDeatil = objMain.LoadData("SELECT mstTeamBalika.TBName  ,mst3Block.BlockName  , TBCode, TBName,[TBID]  as UniqueCode,VillageName FROM [tblTrainingDetail]  inner join mstTeamBalika on mstTeamBalika.UniqueCode=TBID inner join mst5Village on mst5Village.VillageCode=mstTeamBalika.VillageCode inner join mst3Block on mst3Block.BlockCode=mst5Village.BlockCode where TBUniqueCode ='" + ptCOde + "'");
            //if (dtTmDeatil.Rows.Count > 0)
            //{
            //    ViewState["dtselected"] = dtTmDeatil;

            //    gvRightSearch.DataSource = dtTmDeatil;
            //    //lbright.DataTextField = "TBName";
            //    //lbright.DataValueField = "UniqueCode";
            //    gvRightSearch.DataBind();

            //    foreach (GridViewRow Itemst in gvRightSearch.Rows)
            //  {
            //        CheckBox chk=(CheckBox)Itemst.FindControl("Chk_allCh1");
            //        chk.Checked = true;
            //    }
            //}  
        }




    }

}