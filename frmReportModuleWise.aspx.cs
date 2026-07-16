using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class frmReportModuleWise : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    string conditions = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // FillDropDown();
            LoadUserLeavel();
           
            divYear.Visible = false;
        }
    }
    public void LoadUserLeavel()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState1, "StateName", "StateCode", "--Select--");
            ddlState1.Enabled = true;
            ddlDistrict1.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState1, "StateName", "StateCode", "--Select--");

            ddlState1.SelectedIndex = 1;
            
        }
        else
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState1, "StateName", "StateCode", "--Select--");

            ddlState1.SelectedIndex = 1;
            
        }


       




    }
    public void FillDropDown()
    {
        conditions = "";
        if (Session["user_level"].ToString() == "99" || Session["user_level"].ToString() == "79" || Session["user_level"].ToString() == "89")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "asc", ddlState1, "StateName", "StateCode", "--Select--");

        }
        else
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "asc", ddlState1, "StateName", "StateCode", "--Select--");

            ddlState1.SelectedIndex = 1;
        }
        if (Session["user_level"].ToString() == "99" || Session["user_level"].ToString() == "79" || Session["user_level"].ToString() == "89")
        {
        }
        else
        {
            conditions = "";
            conditions = "StateCode ='" + ddlState1.SelectedValue + "' and DistrictCode ='" + Session["DistrictCode"].ToString() + "'";
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict1, "DistrictName", "DistrictCode", "--Select--");
            ddlDistrict1.SelectedIndex = 1;
            ddlDistrict_SelectedIndexChanged(ddlDistrict1, null);
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }
    }
    #region ***** Excel Export Click Events ****************
    protected void btnCSV_Click(object sender, EventArgs e)
    {
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
    }
    #endregion
    #region ******** Report Button Click Events ***************
    //protected void BtnDateWiseAll_Click(object sender, EventArgs e)
    //{
    //}
    //protected void LnkAllUsers_Click(object sender, EventArgs e)
    //{
    //}
    //protected void LnkAllModuleTimePeriod_Click(object sender, EventArgs e)
    //{
    //}
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        if (ddlType.SelectedValue == "1")
        {
        }
        else if (ddlType.SelectedValue == "2")
        {
        }
        Fill();

    }
    #endregion
    #region ******** FillGrid *******
    public void Fill()
    {

        string Condition = " where 1=1", Condition4=" where 1=1";
        string Condition1 = " where 1=1 ", Condition2 = "where 1=1", Condition3=" where 1=1";
        
        int Flag = 0;
        string Y1 = string.Empty;
        string Y2 = string.Empty;
        string StateCode = string.Empty, DistrictCode = string.Empty, BlockCode = string.Empty,EmployeeCode=string.Empty;
        if (ddlType.SelectedValue == "0" && ddlType.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('please select type')</script>", false);
            return;
        }
        if (ddlYear.SelectedValue!="")
        {
            string Year = ddlYear.SelectedValue;
            string[] ar = Year.Split('-');
            Y2 = ar[0];
        }
       

        if (ddlState1.SelectedValue != null && ddlState1.SelectedIndex > 0)
        {
            Condition = Condition + " and mst1State.StateCode='" + ddlState1.SelectedValue.ToString() + "'";
            StateCode = ddlState1.SelectedValue.ToString();
        }
        if (ddlDistrict1.SelectedValue != null && ddlDistrict1.SelectedIndex > 0)
        {
            Condition = Condition + " and mst2District.DistrictCode='" + ddlDistrict1.SelectedValue.ToString() + "'";
            DistrictCode = ddlDistrict1.SelectedValue.ToString();
        }
        if (ddlBlock1.SelectedValue != null && ddlBlock1.SelectedIndex > 0)
        {
            Condition = Condition + " and mst3Block.BlockCode='" + ddlBlock1.SelectedValue.ToString() + "'";
            BlockCode = ddlBlock1.SelectedValue.ToString();
        }
        if (ddlType.SelectedValue == "1")
        {
            if (ddlPeriod.SelectedIndex > 0 && ddlPeriod.SelectedValue != null)
            {
                if (ddlPeriod.SelectedValue == "1")
                {
                    if (ddlMonth.SelectedValue != null && ddlMonth.SelectedIndex >= 0)
                    {
                        Flag = 1;
                        Condition1 =  " Where month(Createdate)='" + ddlMonth.SelectedValue + "' and Year(Createdate)='" + Y2 + "'";
                        Condition2 = " Where month(Createddate)='" + ddlMonth.SelectedValue + "' and Year(Createddate)='" + Y2 + "'";
                        Condition3 = " Where month([tblTraining].Createdate)='" + ddlMonth.SelectedValue + "' and Year([tblTraining].Createdate)='" + Y2 + "'";
                    }

                }
                else if (ddlPeriod.SelectedValue == "2")
                {
                    if (ddlMonth.SelectedValue != null)
                    {
                        Flag = 2;
                        if (ddlState1.SelectedIndex > 0)
                        {

                            Condition4 = Condition + " and Year(Createdate)='" + Y2 + "'";
                            Condition1 = Condition + " and Year(Createddate)='" + Y2 + "'";
                            Condition2 = Condition + " and Year([tblTraining].Createdate)='" + Y2 + "'";
                            Condition = Condition4;
                        }
                        else
                        {
                            Condition = Condition + " and Year(Createdate)='" + Y2 + "'";
                            Condition1 = Condition1 + " and Year(Createddate)='" + Y2 + "'";
                            Condition2 = Condition2 + " and Year([tblTraining].Createdate)='" + Y2 + "'";
                        }
                        if (ddlEmployee1.SelectedIndex > 0)
                        {
                            Condition = Condition + " and Createby='" + ddlEmployee1.SelectedValue + "'";
                            Condition1 = Condition1 + " and Createdby='" + ddlEmployee1.SelectedValue + "'";
                            Condition3 = Condition2 + " and tblTraining.CreateBY='" + ddlEmployee1.SelectedValue + "'";

                        }
                    }
                }
                else if (ddlPeriod.SelectedValue == "3")
                {
                    if (TxtFromDate.Text != "")
                    {
                        Flag = 1;
                        DateTime Fromdate = Convert.ToDateTime(TxtFromDate.Text);
                        Condition1 =Condition1+ " and Createdate>='" + Fromdate.ToString("yyyy-MM-dd") + "'";
                        Condition2 =  Condition2+" and Createddate>='" + Fromdate.ToString("yyyy-MM-dd") + "'";
                        Condition3 = Condition3+ " and [tblTraining].Createdate>='" + Fromdate.ToString("yyyy-MM-dd") + "'";
                    }
                    if (TxtD.Text != "")
                    {
                        Flag = 1;
                        DateTime ToDate = Convert.ToDateTime(TxtD.Text);
                        Condition1 = Condition1 +" and Createdate<='" + ToDate.ToString("yyyy-MM-dd") + "'";
                        Condition2 = Condition2 +" and Createddate<='" + ToDate.ToString("yyyy-MM-dd") + "'";
                        Condition3 =  Condition3+" and [tblTraining].Createdate<='" + ToDate.ToString("yyyy-MM-dd") + "'";
                    }

                }

            }
            if (ddlPeriod.SelectedValue == "1")
            {
                if (ddlEmployee1.SelectedValue != null && ddlEmployee1.SelectedIndex > 0)
                {
                    Flag = 3;
                    Condition4 = Condition + " and CreateBY='" + ddlEmployee1.SelectedValue.ToString() + "'";
                    Condition2 = Condition + " and tblTraining.CreateBY='" + ddlEmployee1.SelectedValue.ToString() + "'";
                    Condition3 = Condition + " and Createdby='" + ddlEmployee1.SelectedValue.ToString() + "'";
                    Condition = Condition4;
                }
            }
        }
        else if (ddlType.SelectedValue == "2")
        {

        }
        if (ddlType.SelectedIndex > 0)
        {
            if (ddlType.SelectedValue == "1")
            {
                DataTable dt = objMain.GetGridDataEntryStatusReprt(Condition, Condition1, Condition2, Condition3, Flag);
                ViewState["DataStatus"] = dt;

                if (dt.Rows.Count > 0)
                {
                    GV_Report.DataSource = dt;
                    GV_Report.DataBind();
                }
                else
                {
                    GV_Report.DataSource = null;
                    GV_Report.DataBind();
                }
            }
            else if(ddlType.SelectedValue == "2")
            {
                if (ddlEmployee1.SelectedValue != null && ddlEmployee1.SelectedIndex > 0)
                {
                    EmployeeCode = ddlEmployee1.SelectedValue.ToString();
                  
                }
                if (TxtFromDate.Text != "")
                {
                    Flag = 1;
                    DateTime Fromdate = Convert.ToDateTime(TxtFromDate.Text);
                    Condition1 = Condition + " and Createdate>='" + Fromdate.ToString("yyyy-MM-dd") + "'";
                    Condition2 = Condition + " and Createddate>='" + Fromdate.ToString("yyyy-MM-dd") + "'";
                    Condition3 = Condition + " and [tblTraining].Createdate>='" + Fromdate.ToString("yyyy-MM-dd") + "'";
                }
                if (TxtD.Text != "")
                {
                    Flag = 1;
                    DateTime ToDate = Convert.ToDateTime(TxtD.Text);
                    Condition1 = Condition1 + " and Createdate<='" + ToDate.ToString("yyyy-MM-dd") + "'";
                    Condition2 = Condition2 + " and Createddate<='" + ToDate.ToString("yyyy-MM-dd") + "'";
                    Condition3 = Condition3 + " and [tblTraining].Createdate<='" + ToDate.ToString("yyyy-MM-dd") + "'";
                }
                DataTable dt = objMain.GetUserWise(Condition, Condition1, Condition2, Condition3, StateCode, DistrictCode,BlockCode,EmployeeCode, Flag);
                ViewState["DataStatus"] = dt;
                if (dt.Rows.Count > 0)
                {
                    GV_Report.DataSource = dt;
                    GV_Report.DataBind();
                }
                else
                {
                    GV_Report.DataSource = null;
                    GV_Report.DataBind();
                }
            }
        }
    }
    #endregion
    #region ****** Fill Masters ***************
    public void FillCBDist()
    {
        conditions = "";
        conditions = "StateCode ='" + ddlState1.SelectedValue + "' and Fyear ='" + ddlYear.SelectedValue + "'";
        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict1, "DistrictName", "DistrictCode", "--All--");
    }
    public void FillCBBock()
    {
        conditions = "";
        conditions = "districtcode ='" + ddlDistrict1.SelectedValue + "' and Fyear ='" + ddlYear.SelectedValue + "'";
        objComman.BindDLL("mst3block", "blockcode,dbo.titlecase(upper(blockname)) as blockname ", conditions, "blockname", "asc", ddlBlock1, "blockname", "blockcode", "--all--");
    }
    public void FillUser()
    {
        if (ddlDistrict1.SelectedIndex > 0)
        {
            conditions = "StaffID  <>'' and DistrictCode ='" + ddlDistrict1.SelectedValue + "'";
            objComman.BindDLL("MstUser", "UserName,[UserName]+' ('+ FristName +')' as UserName1 ", conditions, "UserName1", "asc", ddlEmployee1, "UserName1", "UserName", "--Select--");
        }
        else
        {
            conditions = "StaffID  <>'' and DistrictCode ='" + Session["DistrictCode"].ToString() + "'";
            objComman.BindDLL("MstUser", "UserName,[UserName]+' ('+ FristName +')' as UserName1 ", conditions, "UserName1", "asc", ddlEmployee1, "UserName1", "UserName", "--Select--");
        }
    }
    #endregion
    #region ******* Selected Index Changed Eevents ********************************
    protected void ddlType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlType.SelectedValue == "1")
        {
        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
        if (ddlState1.SelectedValue == "0")
        {
            ddlDistrict1.Items.Clear();
            ddlBlock1.Items.Clear();
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBBock();
        FillUser();
        if (ddlDistrict1.SelectedValue == "0")
        {
            ddlBlock1.Items.Clear();
            ddlEmployee1.Items.Clear();
        }
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] ar = null;
        if (ddlYear.SelectedValue != "")
        {
            string Year = ddlYear.SelectedValue;
            ar = Year.Split('-');

        }


        string startDate = "" + ar[0] + "-04-01";
        CalendarExtender3.StartDate = Convert.ToDateTime(startDate);
        string EndDate = "" + ar[1] + "-03-31";
        CalendarExtender3.EndDate = Convert.ToDateTime(EndDate);


        string startToDate = "" + ar[0] + "-04-01";
        CalendarExtender2.StartDate = Convert.ToDateTime(startToDate);
        string EndToDate = "" + ar[1] + "-03-31";
        CalendarExtender2.EndDate = Convert.ToDateTime(EndToDate);
    }
    protected void ddlPeriod_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPeriod.SelectedValue == "0")
        {
            LblMonth.Visible = false;
            ddlMonth.Visible = false;
            LblToDate.Visible = false;
            TxtFromDate.Visible = false;
            ddlYear.Visible = false;
            TxtD.Visible = false;
            ddlYear1.Visible = false;
        }
        else if (ddlPeriod.SelectedValue == "1")
        {
            LblMonth.Visible = true;
            LblMonth.Text = "Year";
            ddlMonth.Visible = true;
            ddlYear1.Visible = false;
            TxtFromDate.Visible = false;
            LblToDate.Visible = true;
            LblToDate.Text = "Month";
            TxtD.Visible = false;
            LblToDate.Visible = false;
            ddlYear.Visible = true;
            LblToDate.Visible = true;
            divfDate.Visible = false;

            FillMonth();
            FillYear();
        }
        else if (ddlPeriod.SelectedValue == "2")
        {
            LblMonth.Visible = true;
            LblMonth.Text = "Year";
            ddlMonth.Visible = false;
            ddlYear1.Visible = false;
            LblToDate.Visible = false;
            TxtFromDate.Visible = false;
            TxtD.Visible = false;
            ddlYear.Visible = true;
            divfDate.Visible = false;
            divYear.Visible = true;
            FillYear();
        }
        else if (ddlPeriod.SelectedValue == "3")
        {
            LblMonth.Visible = true;
            LblMonth.Text = "From :";
            LblToDate.Text = "To :";
            ddlMonth.Visible = false;
            TxtFromDate.Visible = true;
            LblToDate.Visible = true;
            TxtD.Visible = true;
            ddlYear.Visible = true;
            ddlYear1.Visible = false;
            divYear.Visible = true;
            divfDate.Visible = true;
            FillYear();
            string[] ar = null;
            if (ddlYear.SelectedValue != "")
            {
                string Year = ddlYear.SelectedValue;
                 ar = Year.Split('-');
             
            }


            string startDate = "" + ar[0] + "-04-01";
            CalendarExtender3.StartDate = Convert.ToDateTime(startDate);
            string EndDate = "" + ar[1] + "-03-31";
            CalendarExtender3.EndDate = Convert.ToDateTime(EndDate);


            string startToDate = "" + ar[0] + "-04-01";
            CalendarExtender2.StartDate = Convert.ToDateTime(startToDate);
            string EndToDate = "" + ar[1] + "-03-31";
            CalendarExtender2.EndDate = Convert.ToDateTime(EndToDate);
        }

    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedValue == "2")
        {
            Lbl1.Visible = false;
            LblState1.Text = "Region :";
            Lbl2.Visible = false;
            ddlRegion.Visible = true;
            ddlDistrict1.Visible = false;
            ddlBlock1.Visible = false;
            ddlEmployee1.Visible = false;
            LblState1.Visible = true;
            ddlState1.Visible = false;

        }
        else if (ddlCountry.SelectedValue == "3")
        {
            Lbl1.Text = "";
            Lbl1.Text = "District :";
            Lbl2.Visible = true;
            Lbl1.Visible = true;
            Lbl2.Text = "Block :";
            ddlRegion.Visible = false;
            ddlDistrict1.Visible = true;
            ddlBlock1.Visible = true;
            ddlEmployee1.Visible = false;
            LblState1.Visible = true;
            LblState1.Text = "";
            LblState1.Text = "State :";
            ddlState1.Visible = true;
        }
        else if (ddlCountry.SelectedValue == "4")
        {

            Lbl2.Text = "";
            Lbl2.Text = "Employee :";
            Lbl1.Text = "";
            Lbl1.Text = "District :";
            Lbl1.Visible = true;
            Lbl2.Visible = true;
            ddlDistrict1.Visible = true;
            ddlBlock1.Visible = false;
            ddlRegion.Visible = false;
            ddlEmployee1.Visible = true;
            LblState1.Text = "";
            LblState1.Text = "State :";
            LblState1.Visible = true;
            ddlState1.Visible = true;

        }
        else
        {
            ddlDistrict1.Visible = false;
            ddlBlock1.Visible = false;
            ddlRegion.Visible = false;
            ddlEmployee1.Visible = false;
            Lbl2.Visible = false;
            Lbl1.Visible = false;
            LblState1.Text = "";
            LblState1.Visible = false;
            ddlState1.Visible = false;
        }
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    private void FillMonth()
    {
        if (ddlMonth.Items.Count > 0)
        {
            ddlMonth.Items.Clear();
        }
        for (int month = 1; month <= 12; month++)
        {
            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            ddlMonth.Items.Add(new ListItem(monthName, month.ToString().PadLeft(2, '0')));
        }
        if (ddlYear1.Items.Count > 0)
        {
            ddlYear1.Items.Clear();
        }
        for (int month = 1; month <= 12; month++)
        {
            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            ddlYear1.Items.Add(new ListItem(monthName, month.ToString().PadLeft(2, '0')));
        }
        //if (ddlPeriod.SelectedValue == "1")
        //{
        //    if (ddlMonth.Items.Count > 0)
        //    {
        //        ddlYear.Items.Clear();
        //    }
        //    for (int i = DateTime.Now.Year; i > 2010; i--)
        //    {
        //        ddlYear.Items.Add((i-1)+"-"+   i.ToString());
        //    }
        //}
        //for (int month = 1; month <= 12; month++)
        //{
        //    ddlMonth.Items.Add(new ListItem(month.ToString().PadLeft(2, '0'), month.ToString().PadLeft(2, '0')));
        //}
    }
    private void FillYear()
    {
        if (ddlPeriod.SelectedValue == "2")
        {
            if (ddlMonth.Items.Count > 0)
            {
                ddlMonth.Items.Clear();
            }
            for (int i = DateTime.Now.Year; i > 2010; i--)
            {
                ddlMonth.Items.Add((i - 1) + "-" + i.ToString());
            }
        }

        if (ddlYear.Items.Count > 0)
        {
            ddlYear.Items.Clear();
        }
        for (int i = DateTime.Now.Year +1; i > 2015; i--)
        {
            ddlYear.Items.Add((i - 1) + "-" + i.ToString());
        }

    }
    #endregion
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    #region ****** Gridview Events ********
    protected void GV_Report_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_Report.PageIndex = e.NewPageIndex;
        if (ViewState["DataStatus"] != null)
        {
            DataTable Dt = ViewState["DataStatus"] as DataTable;
            GV_Report.DataSource = Dt;
            GV_Report.DataBind();
        }
    }
    protected void GV_Report1_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }
    protected void GV_Report3_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }
    protected void GV_Report4_pageindexchanging(object sender, GridViewPageEventArgs e)
    {
    }
    protected void GV_Report5_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }

    #endregion
}