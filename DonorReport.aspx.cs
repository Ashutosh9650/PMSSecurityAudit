using Ionic.Zip;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class DonorReport : System.Web.UI.Page
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




                if (!IsPostBack)
                {
                    //   LoadYear();
                    LoadYearNew();

                    LoadUserLevel();
                    ViewState["1"] = "ss";
                    ViewState["Annual"] = "";
                    ViewState["D2dUser"] = "";
                }
                // btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }

        }
        if (hdnbtnValue.Value == "1")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "", "<SCRIPT LANGUAGE='javascript'>fnNew(true)</script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "", "<SCRIPT LANGUAGE='javascript'>fnNew(false)</script>", false);
        }
    }

    public void LoadYearNew()
    {
        DateTime GivenDate = DateTime.Now;
        int GivenYear = GivenDate.Year;
        int m = GivenDate.Month;


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

                dr = dtYear.NewRow();
                dr["Type"] = GivenYear.ToString() + "-" + Convert.ToString((GivenYear + 1));
                dr["ID"] = y;
                dtYear.Rows.Add(dr);

                //get last  two digits (eg: 10 from 2010);


            }

        }
        dtYear = Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


    }
    public DataTable CreateDataTableMonth()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYear.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYear;
    }
    public void LoadMonth()
    {
        DateTime GivenDate = DateTime.Now;
        int GivenYear = GivenDate.Year;
        int m = GivenDate.Month;


        //ddlYear.Items.Add("--Select--","0");
        int y = GivenDate.Year;


        DateTime GivenDate1 = DateTime.Now;
        int GivenYear1 = GivenDate1.Year;
        DataTable dtYear = CreateDataTableMonth();
        DataRow dr;

        if (Convert.ToInt32(ddlStartYear.SelectedValue) == 2025)
        {
            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 0 || Convert.ToInt32(ddlFrequency.SelectedValue) == 5)
            {
                dr = dtYear.NewRow();
                dr["Type"] = "Apr";
                dr["ID"] = "20250400";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "May";
                dr["ID"] = "20250500";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Jun";
                dr["ID"] = "20250600";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Jul";
                dr["ID"] = "20250700";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Aug";
                dr["ID"] = "20250800";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Sep";
                dr["ID"] = "20250900";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Oct";
                dr["ID"] = "20251000";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Nov";
                dr["ID"] = "20251100";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Dec";
                dr["ID"] = "20251200";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Jan";
                dr["ID"] = "20260100";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Feb";
                dr["ID"] = "20260200";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Mar";
                dr["ID"] = "20260300";
                dtYear.Rows.Add(dr);
            }

            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 1)
            {
                dr = dtYear.NewRow();
                dr["Type"] = "Apr";
                dr["ID"] = "20250400";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "May";
                dr["ID"] = "20250500";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Jun";
                dr["ID"] = "20250600";
                dtYear.Rows.Add(dr);


            }

            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 2)
            {


                dr = dtYear.NewRow();
                dr["Type"] = "Jul";
                dr["ID"] = "20250700";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Aug";
                dr["ID"] = "20250800";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Sep";
                dr["ID"] = "20250900";
                dtYear.Rows.Add(dr);

            }
            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 3)
            {

                dr = dtYear.NewRow();
                dr["Type"] = "Oct";
                dr["ID"] = "20251000";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Nov";
                dr["ID"] = "20251100";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Dec";
                dr["ID"] = "20251200";
                dtYear.Rows.Add(dr);

            }
            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 4)
            {


                dr = dtYear.NewRow();
                dr["Type"] = "Jan";
                dr["ID"] = "20260100";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Feb";
                dr["ID"] = "20260200";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Mar";
                dr["ID"] = "20260300";
                dtYear.Rows.Add(dr);
            }
        }
        else
        {
            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 0 || Convert.ToInt32(ddlFrequency.SelectedValue) == 5)
            {
                dr = dtYear.NewRow();
                dr["Type"] = "Apr";
                dr["ID"] = "20240400";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "May";
                dr["ID"] = "20240500";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Jun";
                dr["ID"] = "20240600";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Jul";
                dr["ID"] = "20240700";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Aug";
                dr["ID"] = "20240800";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Sep";
                dr["ID"] = "20240900";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Oct";
                dr["ID"] = "20241000";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Nov";
                dr["ID"] = "20241100";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Dec";
                dr["ID"] = "20241200";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Jan";
                dr["ID"] = "20250100";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Feb";
                dr["ID"] = "20250200";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Mar";
                dr["ID"] = "20250300";
                dtYear.Rows.Add(dr);
            }

            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 1)
            {
                dr = dtYear.NewRow();
                dr["Type"] = "Apr";
                dr["ID"] = "20240400";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "May";
                dr["ID"] = "20240500";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Jun";
                dr["ID"] = "20240600";
                dtYear.Rows.Add(dr);


            }

            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 2)
            {


                dr = dtYear.NewRow();
                dr["Type"] = "Jul";
                dr["ID"] = "20240700";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Aug";
                dr["ID"] = "20240800";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Sep";
                dr["ID"] = "20240900";
                dtYear.Rows.Add(dr);

            }
            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 3)
            {

                dr = dtYear.NewRow();
                dr["Type"] = "Oct";
                dr["ID"] = "20241000";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Nov";
                dr["ID"] = "20241100";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Dec";
                dr["ID"] = "20241200";
                dtYear.Rows.Add(dr);

            }
            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 4)
            {


                dr = dtYear.NewRow();
                dr["Type"] = "Jan";
                dr["ID"] = "20250100";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Feb";
                dr["ID"] = "20250200";
                dtYear.Rows.Add(dr);

                dr = dtYear.NewRow();
                dr["Type"] = "Mar";
                dr["ID"] = "20250300";
                dtYear.Rows.Add(dr);
            }
        }
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYearMonth, "Type", "ID", "Select");




    }
    public void LoadUserLevel()
    {


    }
    public DataTable CreateDataTable()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYear.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYear;
    }

    public DataTable CreateDataTableGrroup()
    {

        DataTable dtYearGrroup = new DataTable();
        dtYearGrroup.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYearGrroup.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYearGrroup;
    }
    public void LoadGroup(int Type)
    {
        DataRow dr;
        DataTable dtYear = CreateDataTable();



        if (Type == 1)
        {


            dr = dtYear.NewRow();
            dr["Type"] = "Q1";
            dr["ID"] = 1;
            dtYear.Rows.Add(dr);


            dr = dtYear.NewRow();
            dr["Type"] = "Q2";
            dr["ID"] = 2;
            dtYear.Rows.Add(dr);

            dr = dtYear.NewRow();
            dr["Type"] = "Q3";
            dr["ID"] = 3;
            dtYear.Rows.Add(dr);

            dr = dtYear.NewRow();
            dr["Type"] = "Q4";
            dr["ID"] = 4;
            dtYear.Rows.Add(dr);


            objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlFrequency, "Type", "ID", "ALL");


        }
        if (Type == 4)
        {


            dr = dtYear.NewRow();
            dr["Type"] = "Q1";
            dr["ID"] = 1;
            dtYear.Rows.Add(dr);


            dr = dtYear.NewRow();
            dr["Type"] = "Q2";
            dr["ID"] = 2;
            dtYear.Rows.Add(dr);

            dr = dtYear.NewRow();
            dr["Type"] = "Q3";
            dr["ID"] = 3;
            dtYear.Rows.Add(dr);

            dr = dtYear.NewRow();
            dr["Type"] = "Q4";
            dr["ID"] = 4;
            dtYear.Rows.Add(dr);

            dr = dtYear.NewRow();
            dr["Type"] = "Monthly";
            dr["ID"] = 5;
            dtYear.Rows.Add(dr);
            objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlFrequency, "Type", "ID", "ALL");


        }
        if (Type == 2)
        {


            dr = dtYear.NewRow();
            dr["Type"] = "First Half";
            dr["ID"] = 1;
            dtYear.Rows.Add(dr);


            dr = dtYear.NewRow();
            dr["Type"] = "Second Half";
            dr["ID"] = 2;
            dtYear.Rows.Add(dr);


            objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlFrequency, "Type", "ID", "ALL");


        }
        if (Type == 3)
        {


            dr = dtYear.NewRow();
            dr["Type"] = "Yearly";
            dr["ID"] = 1;
            dtYear.Rows.Add(dr);




            objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlFrequency, "Type", "ID", "ALL");


        }
    }
    public void LoadYear()
    {
        objComman.BindDLL("mstDonorDeatils", "DID,DonorName", "ActiveStatus=1 and Dyear='" + ddlStartYear.SelectedItem.Text + "'", "DonorName", "asc", ddlDonor, "DonorName", "DID", "Select");




    }
    public DataTable Generate_Financial_Year()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("Type");
        DataRow dr;
        int stYr = DateTime.Today.Month < 4 ? DateTime.Today.Year + 1 : DateTime.Today.Year + 1;
        for (int i = stYr; i > 2016; i--)
        {
            dr = dt.NewRow();
            dr[0] = (i - 1).ToString();
            dr[1] = (i - 1).ToString() + "-" + (i).ToString();
            dt.Rows.Add(dr);
        }
        dt.AcceptChanges();
        return dt;
    }




    protected void ddlTpye_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["Annual"] = "";
        ViewState["D2dUser"] = "";
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
    }
    protected void ddlStartYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadYear();

        ChkState.Items.Clear();
        chkDistrict.Items.Clear();
        chkBlock.Items.Clear();
    }
    protected void ddlFrequency_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadMonth();
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlParameter[] parm1 = new SqlParameter[]
            {

               new SqlParameter("@ID",  ddlDonor.SelectedValue),

            };
        DataTable dt = null;
        if (Convert.ToInt32(ddlStartYear.SelectedValue) >= 2025)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptDonorMasterLoadReporting2025]", parm1);

        }
        else if (Convert.ToInt32(ddlStartYear.SelectedValue) == 2024)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptDonorMasterLoadReporting2024]", parm1);

        }
        else
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptDonorMasterLoadReporting]", parm1);


        }

        if (dt.Rows.Count > 0)
        {
            lblState.Text = dt.Rows[0]["State Name"].ToString();
            lblDistrict.Text = dt.Rows[0]["District Name"].ToString();
            lblBlock.Text = dt.Rows[0]["Block Name"].ToString();
            lblFrequency.Text = dt.Rows[0]["FrequencyID"].ToString();
            lblTarget.Text = dt.Rows[0]["Fyear"].ToString();
            lblGType.Text = dt.Rows[0]["GeographyType"].ToString();
            ddInGeography.SelectedValue = dt.Rows[0]["GeographyID"].ToString();
            lblPri.Text = dt.Rows[0]["Project Period"].ToString();

            lblFyear.Text = dt.Rows[0]["NewFyear"].ToString();
            lblMonth.Text = dt.Rows[0]["Mmonth"].ToString();

            FillCBState();
            ddlFrequency.DataSource = null;
            ddlFrequency.DataBind();

            LoadGroup(Convert.ToInt32(lblFrequency.Text));
            LoadMonth();
            //   ddlFrequency.SelectedValue = lblFrequency.Text;


        }

        SqlParameter[] parm2 = new SqlParameter[]
            {

               new SqlParameter("@ID",  ddlDonor.SelectedValue),

            };


        DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptDonorMasterLoad]", parm2);

        if (dt1.Rows.Count > 0)
        {
            lblState1.Text = dt1.Rows[0]["State Name"].ToString();
            lblDistrict1.Text = dt1.Rows[0]["District Name"].ToString();
            lblBlock1.Text = dt1.Rows[0]["Block Name"].ToString();

        }
        //gvnroll.DataSource = null;
        //gvnroll.DataBind();
    }
    public void FillCBState()
    {
        if (lblState.Text.Length > 0)
        {
            string[] meeting = lblState.Text.Split(',');
            string TextMeeeting = "";
            foreach (string s in meeting)
            {
                TextMeeeting += "'" + s.Trim() + "',";

            }
            if (TextMeeeting.Length > 0)
            {
                TextMeeeting = TextMeeeting.Substring(0, TextMeeeting.LastIndexOf(","));

            }

            conditions = "";
            // objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");


            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State  where StateCode in(" + TextMeeeting + ")  order by StateName   ";
            DataTable dtState = objMain.LoadData(strQry1);
            ChkState.DataSource = dtState;
            ChkState.DataTextField = "StateName";
            ChkState.DataValueField = "StateCode";
            ChkState.DataBind();

            foreach (ListItem item in ChkState.Items)
            {

                item.Selected = true;

            }
            ddlState_SelectedIndexChanged(ChkState, null);
        }
        else
        {
            ChkState.Items.Clear();
            chkDistrict.Items.Clear();
            chkBlock.Items.Clear();
        }
    }
    public void FillCBDist()
    {
        string ddlState = "";
        DataTable dtDistrict = null;
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlState += "'" + item.Value.Trim() + "'" + ",";


            }
        }

        if (ddlState.Length > 0)
        {
            ddlState = ddlState.Substring(0, ddlState.LastIndexOf(","));
        }

        string strQry3 = "  select distinct mst2District.DistrictCode from mstDonorDistrictProfile left join mst2District on mst2District.DistrictCode=[mstDonorDistrictProfile].DistrictCode 	 	 	 	  left join mst1State on mst1State.StateCode=mst2District.StateCode where LEN(mstDonorDistrictProfile.DistrictCode)>2 and DID=" + ddlDonor.SelectedValue + " and mst2District.Fyear='" + ddlYear.SelectedItem.Text + "' ";
        DataTable dtDistrict1 = objMain.LoadData(strQry3);
        string TextMeeeting = "";


        if (lblDistrict.Text.Length > 0)
        {
            string[] meeting = lblDistrict.Text.Split(',');
            if (dtDistrict1.Rows.Count > 10)
            {
                for (int i = 0; i < dtDistrict1.Rows.Count; i++)
                {

                    TextMeeeting += "'" + dtDistrict1.Rows[i]["DistrictCode"] + "',";

                }
                if (TextMeeeting.Length > 0)
                {
                    TextMeeeting = TextMeeeting.Substring(0, TextMeeeting.LastIndexOf(","));

                }
            }
            else
            {
                foreach (string s in meeting)
                {
                    TextMeeeting += "'" + s.Trim() + "',";

                }
                if (TextMeeeting.Length > 0)
                {
                    TextMeeeting = TextMeeeting.Substring(0, TextMeeeting.LastIndexOf(","));

                }
            }
            if (lblGType.Text == "EG")
            {
                conditions = "StateCode  in(" + ddlState + ") and DistrictCode in(" + TextMeeeting + ")";
                string strQry = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where " + conditions + "  order by DistrictName   ";
                dtDistrict = objMain.LoadData(strQry);
            }
            else
            {
                conditions = " AdminDistrictCode in(" + TextMeeeting + ") and Fyear='" + Session["FinYear"].ToString() + "'";
                string strQry = "  SELECT distinct AdminDistrictCode as DistrictCode, dbo.TitleCase(upper(AdminDistrictName))  as DistrictName FROM mst5Village where " + conditions + "  order by DistrictName   ";
                dtDistrict = objMain.LoadData(strQry);
            }


            chkDistrict.DataSource = dtDistrict;
            chkDistrict.DataTextField = "DistrictName";
            chkDistrict.DataValueField = "DistrictCode";
            chkDistrict.DataBind();


            foreach (ListItem item in chkDistrict.Items)
            {

                item.Selected = true;

            }
            ddlDistrict_SelectedIndexChanged(chkDistrict, null);

        }
        else
        {
            chkDistrict.Items.Clear();
            chkBlock.Items.Clear();
        }

    }
    public void FillCBBock()
    {
        conditions = "";
        string ddlDistrict = "";

        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }


        string strQry3 = "  select distinct  mst3Block.BlockCode from mstDonorDistrictProfile left join mst3Block on mst3Block.BlockCode=[mstDonorDistrictProfile].BlockCode 	 	  left join mst1State on mst1State.StateCode=mst3Block.BlockCode where LEN(mstDonorDistrictProfile.BlockCode)>2 and DID=" + ddlDonor.SelectedValue + " and mst3Block.Fyear='" + ddlYear.SelectedItem.Text + "' ";
        DataTable dtDistrict1 = objMain.LoadData(strQry3);

        if (lblBlock.Text.Length > 0)
        {
            string[] meeting = lblBlock.Text.Split(',');
            string TextMeeeting = "";
            if (dtDistrict1.Rows.Count > 8)
            {
                for (int i = 0; i < dtDistrict1.Rows.Count; i++)
                {

                    TextMeeeting += "'" + dtDistrict1.Rows[i]["BlockCode"] + "',";

                }
                if (TextMeeeting.Length > 0)
                {
                    TextMeeeting = TextMeeeting.Substring(0, TextMeeeting.LastIndexOf(","));

                }
            }
            else
            {
                foreach (string s in meeting)
                {
                    TextMeeeting += "'" + s.Trim() + "',";

                }

                if (TextMeeeting.Length > 0)
                {
                    TextMeeeting = TextMeeeting.Substring(0, TextMeeeting.LastIndexOf(","));

                }
            }
            DataTable dtDistrict = null;
            if (lblGType.Text == "EG")
            {
                conditions = "DistrictCode in(" + ddlDistrict + ")   and BlockCode in( " + TextMeeeting + " ) ";


                //     objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

                string strQry = "  SELECT BlockCode, dbo.TitleCase(upper(BlockName))  as BlockName FROM mst3Block where " + conditions + "  order by BlockName   ";
                dtDistrict = objMain.LoadData(strQry);
            }
            else
            {
                conditions = " AdminDistrictCode in(" + ddlDistrict + ") and MainBlockCode in( " + TextMeeeting + " )  and Fyear='" + Session["FinYear"].ToString() + "'";
                string strQry = "  SELECT distinct MainBlockCode BlockCode, dbo.TitleCase(upper(MainBlockName))  as BlockName FROM mst5Village where " + conditions + "    ";
                dtDistrict = objMain.LoadData(strQry);
            }
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            chkBlock.DataSource = dtDistrict;
            chkBlock.DataTextField = "BlockName";
            chkBlock.DataValueField = "BlockCode";
            chkBlock.DataBind();

            string blockname = "";
            if (chkBlock.Items.Count > 0)
            {
                foreach (ListItem item in chkBlock.Items)
                {

                    item.Selected = true;
                    blockname += "" + item.Text + ",";

                }
                if (blockname.Length > 0)
                {
                    blockname = blockname.Substring(0, blockname.LastIndexOf(","));


                }


            }



        }
        else
        {
            chkBlock.Items.Clear();
        }


    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {

        chkDistrict.Items.Clear();
        chkBlock.Items.Clear();
        FillCBDist();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBBock();

    }


    protected void LnkDeatild_OnClick(object sender, EventArgs e)
    {
        if (ddlDonor.SelectedIndex > 0)
        {
            if (lblFrequency.Text == "4" && Convert.ToInt32(ddlFrequency.SelectedValue) == 5)
            {
                if (ddlYearMonth.SelectedIndex > 0)
                {
                    DonorReportFinal();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Month ')</script>", false);
                }
            }
            else
            {
                DonorReportFinal();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Donor Name ')</script>", false);

        }

    }

    public void DonorReportFinal()
    {
        int Quter = 0;
        string Hearder = "";
        string HearderMonth = "Ach-" + ddlYearMonth.SelectedItem.Text;
        string ACH = "";
        string Target = "";
        string TargetYDT = "";
        string Q1 = "";

        Int32 StartYear = Convert.ToInt32(lblFyear.Text);

        Int32 AQ1 = 0;
        Int32 AQ2 = 3;
        Int32 AQ3 = 6;
        Int32 AQ4 = 9;


        Int32 StartQ1 = 0;
        Int32 StartQ2 = 0;
        Int32 StartQ3 = 0;
        Int32 StartQ4 = 0;

        Int32 StartQuter1 = 0;
        Int32 StartQuter2 = 0;
        Int32 StartQuter3 = 0;
        Int32 StartQuter4 = 0;

        Int32 StartEndQuter1 = 0;
        Int32 StartEndQuter2 = 0;
        Int32 StartEndQuter3 = 0;
        Int32 StartEndQuter4 = 0;


        Int32 StartEndQ1 = 0;
        Int32 StartEndQ2 = 0;
        Int32 StartEndQ3 = 0;
        Int32 StartEndQ4 = 0;
        // Int32 MkMonth =Convert.ToInt32(lblMonth.Text);
        Int32 MkMonth = 4;

        StartQ1 = AQ1 + MkMonth;
        if (StartQ1 > 12)
        {
            StartQ1 = StartQ1 - 12;
        }
        else
        {
            StartQ1 = AQ1 + MkMonth;

            if (StartQ1 < 4)
            {
                StartQuter1 = (StartYear) * 10000 + (StartQ1) * 100;
            }
            else
            {
                StartQuter1 = (StartYear * 10000) + (StartQ1) * 100;
            }
        }

        StartQ2 = AQ2 + MkMonth;
        if (StartQ2 > 12)
        {
            StartQ2 = StartQ2 - 12;
            StartQuter2 = (StartYear + 1) * 10000 + (StartQ2) * 100;
        }
        else
        {
            StartQ2 = AQ2 + MkMonth;
            if (StartQ2 < 4)
            {
                StartQuter2 = (StartYear - 1) * 10000 + (StartQ2) * 100;
            }
            else
            {
                StartQuter2 = (StartYear * 10000) + (StartQ2) * 100;
            }

        }


        StartQ3 = AQ3 + MkMonth;
        if (StartQ3 > 12)
        {
            StartQ3 = StartQ3 - 12;
            StartQuter3 = (StartYear + 1) * 10000 + (StartQ3) * 100;
        }
        else
        {
            StartQ3 = AQ3 + MkMonth;
            if (StartQ3 < 4)
            {
                StartQuter3 = (StartYear - 1) * 10000 + (StartQ3) * 100;
            }
            else
            {
                StartQuter3 = (StartYear * 10000) + (StartQ3) * 100;
            }
        }



        StartQ4 = AQ4 + MkMonth;
        if (StartQ4 > 12)
        {
            StartQ4 = StartQ4 - 12;
            StartQuter4 = (StartYear + 1) * 10000 + (StartQ4) * 100;
        }
        else
        {
            StartQ4 = AQ4 + MkMonth;
            if (StartQ4 < 4)
            {
                StartQuter4 = (StartYear + 1) * 10000 + (StartQ4) * 100;
            }
            else
            {
                StartQuter4 = (StartYear * 10000) + (StartQ4) * 100;
            }
        }



        StartEndQ1 = StartQ1 + 2;
        if (StartEndQ1 > 12)
        {
            StartEndQ1 = StartEndQ1 - 12;
        }
        else
        {
            StartEndQ1 = StartQ1 + 2;

            if (StartQ1 < 4)
            {
                StartEndQuter1 = (StartYear) * 10000 + (StartEndQ1) * 100;
            }
            else
            {
                StartEndQuter1 = (StartYear * 10000) + (StartEndQ1) * 100;
            }
        }

        StartEndQ2 = StartQ2 + 2;
        if (StartEndQ2 > 12)
        {
            StartEndQ2 = StartEndQ2 - 12;
            StartEndQuter2 = (StartYear + 1) * 10000 + (StartEndQ2) * 100;
        }
        else
        {
            StartEndQ2 = StartQ2 + 2;
            if (StartEndQ2 < 4)
            {
                StartEndQuter2 = (StartYear - 1) * 10000 + (StartEndQ2) * 100;
            }
            else
            {
                StartEndQuter2 = (StartYear * 10000) + (StartEndQ2) * 100;
            }

        }

        StartEndQ3 = StartQ3 + 2;
        if (StartEndQ3 > 12)
        {
            StartEndQ3 = StartEndQ3 - 12;
            StartEndQuter3 = (StartYear + 1) * 10000 + (StartEndQ3) * 100;
        }
        else
        {
            StartEndQ3 = StartQ3 + 2;
            if (StartEndQ3 < 4)
            {
                if (MkMonth == 7)
                {
                    StartEndQuter3 = (StartYear + 1) * 10000 + (StartEndQ3) * 100;
                }
                else
                {
                    StartEndQuter3 = (StartYear - 1) * 10000 + (StartEndQ3) * 100;
                }
            }
            else
            {
                StartEndQuter3 = (StartYear * 10000) + (StartEndQ3) * 100;
            }

        }
        StartEndQ4 = StartQ4 + 2;
        if (StartEndQ3 > 12)
        {
            StartEndQ4 = StartEndQ4 - 12;
            StartEndQuter4 = (StartYear + 1) * 10000 + (StartEndQ4) * 100;
        }
        else
        {
            StartEndQ4 = StartQ4 + 2;
            if (StartEndQ4 < 4)
            {
                StartEndQuter4 = (StartYear + 1) * 10000 + (StartEndQ4) * 100;
            }
            else
            {
                if (MkMonth == 7)
                {
                    StartEndQuter4 = (StartYear + 1) * 10000 + (StartEndQ4) * 100;
                }
                else
                {
                    StartEndQuter4 = (StartYear * 10000) + (StartEndQ4) * 100;
                }
            }

        }




        string Querterwise = "";
        string YDTQuerterwise = "";
        string TeamAch = "";
        string TeamYtd = "";
        string STeamAch = "";
        string STeamYtd = "";

        if (lblFrequency.Text == "1")
        {
            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 0)
            {
                Quter = 4;
                Querterwise = "NewFyear between (" + StartQuter1 + ") and  (" + StartEndQuter4 + ") ";
                YDTQuerterwise = "NewFyear between (" + StartQuter1 + ") and  (" + StartEndQuter4 + ") ";


                Hearder = "Target-Quarterly";
                ACH = " Achv-Quarterly";
                Target = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
                TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";

                STeamAch = "Sum(YQ4)";
                STeamYtd = "Sum(YQ4)";

                Q1 = "quarter<=4";
            }
            else
            {
                Quter = Convert.ToInt32(ddlFrequency.SelectedValue);

                Hearder = "Target-" + ddlFrequency.SelectedItem.Text + "";
                ACH = " Achv-" + ddlFrequency.SelectedItem.Text + "";
                Q1 = "quarter=" + ddlFrequency.SelectedValue + "";
            }
            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 1)
            {
                Target = "Sum(Q1)";
                TargetYDT = "Sum(Q1)";

                STeamAch = "Sum(Q1)";
                STeamYtd = "Sum(YQ1)";
                Querterwise = "NewFyear between (" + StartQuter1 + ") and  (" + StartEndQuter1 + ") ";
                YDTQuerterwise = "NewFyear between (" + StartQuter1 + ") and  (" + StartEndQuter1 + ") ";

            }
            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 2)
            {
                Querterwise = "NewFyear between (" + StartQuter2 + ") and  (" + StartEndQuter2 + ") ";
                YDTQuerterwise = "NewFyear between (" + StartQuter1 + ") and  (" + StartEndQuter2 + ") ";
                Target = "Sum(Q2)";
                TargetYDT = "Sum(Q1)+Sum(Q2)";


                STeamAch = "Sum(Q2)";
                STeamYtd = "Sum(YQ2)";
            }
            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 3)
            {
                Querterwise = "NewFyear between (" + StartQuter3 + ") and  (" + StartEndQuter3 + ") ";
                YDTQuerterwise = "NewFyear between (" + StartQuter1 + ") and  (" + StartEndQuter3 + ") ";
                Target = "Sum(Q3)";
                TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)";


                STeamAch = "Sum(Q3)";
                STeamYtd = "Sum(YQ3)";
            }

            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 4)
            {
                Querterwise = "NewFyear between (" + StartQuter4 + ") and  (" + StartEndQuter4 + ") ";
                YDTQuerterwise = "NewFyear between (" + StartQuter1 + ") and  (" + StartEndQuter4 + ") ";
                Target = "Sum(Q4)";
                TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";

                STeamAch = "Sum(Q4)";
                STeamYtd = "Sum(YQ4)";
            }
        }
        if (lblFrequency.Text == "2")
        {
            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 0)
            {
                Querterwise = "NewFyear between (" + StartQuter1 + ") and  (" + StartEndQuter4 + ") ";
                YDTQuerterwise = "NewFyear between (" + StartQuter1 + ") and  (" + StartEndQuter4 + ") ";
                Hearder = "Target-Half Yearly";
                ACH = " Achv-Half Yearly";
                Quter = 4;
                Target = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
                TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
                Q1 = "quarter<=4";

                STeamAch = "Sum(Q4)";
                STeamYtd = "Sum(YQ4)";

            }
            else if (Convert.ToInt32(ddlFrequency.SelectedValue) == 1)
            {
                Quter = 2;
                Hearder = "Target -" + ddlFrequency.SelectedItem.Text + "";
                ACH = " Achv-" + ddlFrequency.SelectedItem.Text + "";
                Target = "Sum(Q1)";
                TargetYDT = "Sum(Q1)";
                Q1 = "quarter in(1,2)";

                STeamAch = "Sum(Q1)";
                STeamYtd = "Sum(YQ1)";
                Querterwise = "NewFyear between (" + StartQuter1 + ") and  (" + StartEndQuter2 + ") ";
                YDTQuerterwise = "NewFyear between (" + StartQuter1 + ") and  (" + StartEndQuter2 + ") ";
            }
            else
            {
                Quter = 4;
                Hearder = "Target-" + ddlFrequency.SelectedItem.Text + "";
                ACH = " Achv-" + ddlFrequency.SelectedItem.Text + "";
                Target = "Sum(Q2)";
                TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";

                STeamAch = "Sum(Q2)";
                STeamYtd = "Sum(YQ4)";
                Q1 = "quarter<=4";
                Querterwise = "NewFyear between (" + StartQuter2 + ") and  (" + StartEndQuter4 + ") ";
                YDTQuerterwise = "NewFyear between (" + StartQuter1 + ") and  (" + StartEndQuter4 + ") ";
            }
        }
        if (lblFrequency.Text == "3")
        {
            Quter = 4;
            Hearder = "Target-Yearly";
            ACH = " Achv-Yearly";
            Q1 = "quarter<=4";
            Target = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
            TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";


            STeamAch = "Sum(YQ4)";
            STeamYtd = "Sum(YQ4)";

            Querterwise = "NewFyear between (" + StartQuter1 + ") and  (" + StartEndQuter4 + ") ";
            YDTQuerterwise = "NewFyear between (" + StartQuter1 + ") and  (" + StartEndQuter4 + ") ";
        }

        if (lblFrequency.Text == "4" && Convert.ToInt32(ddlFrequency.SelectedValue) == 5)
        {

            if (ddlYearMonth.SelectedItem.Text == "Apr" || ddlYearMonth.SelectedItem.Text == "May" || ddlYearMonth.SelectedItem.Text == "Jun")
            {
                Quter = 1;
                Hearder = "Target-Yearly";
                ACH = " Achv-Q1";
                Q1 = "quarter=1";
                Target = "Sum(Q1)";
                TargetYDT = "Sum(Q1)";
                STeamAch = "Sum(Q1)";
                STeamYtd = "Sum(YQ1)";

                Querterwise = "NewFyear between (" + 20240400 + ") and  (" + 20240600 + ") ";
                YDTQuerterwise = "NewFyear between (" + 20240400 + ") and  (" + 20240600 + ") ";
            }
            if (ddlYearMonth.SelectedItem.Text == "Jul" || ddlYearMonth.SelectedItem.Text == "Aug" || ddlYearMonth.SelectedItem.Text == "Sep")
            {
                Quter = 2;
                Hearder = "Target-Yearly";
                ACH = " Achv-Q2";
                Q1 = "quarter in(1,2)";
                Target = "Sum(Q1)+Sum(Q2)";
                TargetYDT = "Sum(Q1)+Sum(Q2)";

                STeamAch = "Sum(Q2)";
                STeamYtd = "Sum(YQ2)";
                Querterwise = "NewFyear between (" + 20240400 + ") and  (" + 20240900 + ") ";
                YDTQuerterwise = "NewFyear between (" + 20240400 + ") and  (" + 20240900 + ") ";
            }

            if (ddlYearMonth.SelectedItem.Text == "Oct" || ddlYearMonth.SelectedItem.Text == "Nov" || ddlYearMonth.SelectedItem.Text == "Dec")
            {
                Quter = 3;
                Hearder = "Target-Yearly";
                ACH = " Achv-Q3";
                Q1 = "quarter in(1,2,3)";
                Target = "Sum(Q1)+Sum(Q2)+Sum(Q3)";
                TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)";

                STeamAch = "Sum(Q3)";
                STeamYtd = "Sum(YQ3)";
                Querterwise = "NewFyear between (" + 20240400 + ") and  (" + 20241200 + ") ";
                YDTQuerterwise = "NewFyear between (" + 20240400 + ") and  (" + 20241200 + ") ";
            }
            if (ddlYearMonth.SelectedItem.Text == "Jan" || ddlYearMonth.SelectedItem.Text == "Feb" || ddlYearMonth.SelectedItem.Text == "Mar")
            {
                Quter = 4;
                Hearder = "Target-Yearly";
                ACH = " Achv-Q4";
                Q1 = "quarter <=4";
                Target = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
                TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";

                STeamAch = "Sum(Q4)";
                STeamYtd = "Sum(YQ4)";
                Querterwise = "NewFyear between (" + 20240400 + ") and  (" + 20241200 + ") ";
                YDTQuerterwise = "NewFyear between (" + 20240400 + ") and  (" + 20250300 + ") ";
            }

        }
        if (lblFrequency.Text == "4" && Convert.ToInt32(ddlFrequency.SelectedValue) != 5)
        {
            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 0)
            {
                Quter = 4;
                Querterwise = "NewFyear between (" + StartQuter1 + ") and  (" + StartEndQuter4 + ") ";
                YDTQuerterwise = "NewFyear between (" + StartQuter1 + ") and  (" + StartEndQuter4 + ") ";


                Hearder = "Target-Quarterly";
                ACH = " Achv-Quarterly";
                Target = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
                TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
                STeamAch = "Sum(YQ4)";
                STeamYtd = "Sum(YQ4)";
                Q1 = "quarter<=4";
            }
            else
            {
                Quter = Convert.ToInt32(ddlFrequency.SelectedValue);

                Hearder = "Target-" + ddlFrequency.SelectedItem.Text + "";
                ACH = " Achv-" + ddlFrequency.SelectedItem.Text + "";
                Q1 = "quarter=" + ddlFrequency.SelectedValue + "";
            }
            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 1)
            {
                Target = "Sum(Q1)";
                TargetYDT = "Sum(Q1)";
                STeamAch = "Sum(Q1)";
                STeamYtd = "Sum(YQ1)";
                Querterwise = "NewFyear between (" + StartQuter1 + ") and  (" + StartEndQuter1 + ") ";
                YDTQuerterwise = "NewFyear between (" + StartQuter1 + ") and  (" + StartEndQuter1 + ") ";

            }
            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 2)
            {
                Querterwise = "NewFyear between (" + StartQuter2 + ") and  (" + StartEndQuter2 + ") ";
                YDTQuerterwise = "NewFyear between (" + StartQuter1 + ") and  (" + StartEndQuter2 + ") ";
                Target = "Sum(Q2)";
                TargetYDT = "Sum(Q1)+Sum(Q2)";
                STeamAch = "Sum(Q2)";
                STeamYtd = "Sum(YQ2)";
            }
            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 3)
            {
                Querterwise = "NewFyear between (" + StartQuter3 + ") and  (" + StartEndQuter3 + ") ";
                YDTQuerterwise = "NewFyear between (" + StartQuter1 + ") and  (" + StartEndQuter3 + ") ";
                Target = "Sum(Q3)";
                TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)";
                STeamAch = "Sum(Q3)";
                STeamYtd = "Sum(YQ3)";
            }

            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 4)
            {
                Querterwise = "NewFyear between (" + StartQuter4 + ") and  (" + StartEndQuter4 + ") ";
                YDTQuerterwise = "NewFyear between (" + StartQuter1 + ") and  (" + StartEndQuter4 + ") ";
                Target = "Sum(Q4)";
                TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
                STeamAch = "Sum(Q4)";
                STeamYtd = "Sum(YQ4)";
            }

        }

        string ddlBlock = "";
        string ddlBlockNew = "";
        string ddlDistrict = "";
        string ddlStatecode = "";
        string ddlDistrictName = "";
        string ddlStatecodeName = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";
                ddlStatecodeName += "'" + item.Text + "'" + ",";


            }
        }
        if (ddlStatecodeName.Length > 0)
        {
            ddlStatecodeName = ddlStatecodeName.Substring(0, ddlStatecodeName.LastIndexOf(","));
        }
        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";
                ddlDistrictName += "'" + item.Text + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        if (ddlDistrictName.Length > 0)
        {
            ddlDistrictName = ddlDistrictName.Substring(0, ddlDistrictName.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";
                ddlBlockNew += "'" + item.Text + "'" + ",";


            }
        }
        string conditions = "";
        string condiNew = "";
        string condiNewTeamBalik = "";
        if (lblGType.Text == "EG")
        {
            if (ddlStatecode.Length > 0)
            {
                conditions += " and NewStateCode in(" + ddlStatecode + ") ";
                condiNew += " and StateName in(" + ddlStatecodeName + ") ";
                condiNewTeamBalik += " and StateCode in(" + ddlStatecode + ") ";

            }
            if (ddlBlock.Length > 0)
            {
                ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
                ddlBlockNew = ddlBlockNew.Substring(0, ddlBlockNew.LastIndexOf(","));
            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and NewDistrictCode in(" + ddlDistrict + ") ";
                condiNew += " and DistrictName in(" + ddlDistrictName + ") ";
                condiNewTeamBalik += " and DistrictCode in(" + ddlDistrict + ") ";
            }
            if (ddlBlock.Length > 0)
            {
                conditions += " and NewBlockCode in(" + ddlBlock + ") ";
                condiNew += " and BLockName in(" + ddlBlockNew + ") ";
                condiNewTeamBalik += " and BlockCode in(" + ddlBlock + ") ";
            }
        }
        else
        {
            if (ddlBlock.Length > 0)
            {
                ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
            }
            if (ddlDistrict.Length > 0)
            {
                conditions += " and AdminDistrictCode in(" + ddlDistrict + ") ";
                condiNew += " and DistrictName in(" + ddlDistrictName + ") ";
                condiNewTeamBalik += " and AdminDistrictCode in(" + ddlDistrict + ") ";

            }
            if (ddlBlock.Length > 0)
            {
                conditions += " and AdminBlockCode in(" + ddlBlock + ") ";
                condiNewTeamBalik += " and AdminBlockCode in(" + ddlBlock + ") ";
            }
        }
        DataTable dt = null;
        if (Convert.ToInt32(ddlStartYear.SelectedValue) == 2025)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {

                new SqlParameter("@DID",ddlDonor.SelectedValue),
                    new SqlParameter("@YearID",lblFyear.Text),
             new SqlParameter("@Quter",Quter),
             new SqlParameter("@Month","4"),
                new SqlParameter("@con",conditions),
                    new SqlParameter("@Target",Target),
                        new SqlParameter("@yTarget",TargetYDT),
                            new SqlParameter("@Q1",Q1),
                            new SqlParameter("@DistTpye",lblGType.Text),
                             new SqlParameter("@conNew",condiNew),


                                 new SqlParameter("@Ach",Querterwise),
                                     new SqlParameter("@YTD",YDTQuerterwise),
                                        new SqlParameter("@STargetAch",STeamAch),
                                     new SqlParameter("@STargetYTD",STeamYtd),
            };



            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptDonorTargetandachNew2026new2025]", cmdParameters);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Achh = dt.Rows[i]["ACH"].ToString();
                    string YTD = dt.Rows[i]["YTD"].ToString();
                    string AchMonth = dt.Rows[i]["AchMonth"].ToString();
                    string TargetCH = dt.Rows[i]["Target"].ToString();
                    int Totalch = 0;
                    int totalTargetAch = 0;

                    if (dt.Rows[i]["SubID"].ToString() == "3.10" || dt.Rows[i]["SubID"].ToString() == "13.29" || dt.Rows[i]["SubID"].ToString() == "13.25" || dt.Rows[i]["SubID"].ToString() == "12.4" || dt.Rows[i]["SubID"].ToString() == "12.8" || dt.Rows[i]["SubID"].ToString() == "13.26" || dt.Rows[i]["SubID"].ToString() == "12.17" || dt.Rows[i]["SubID"].ToString() == "13.4" || dt.Rows[i]["SubID"].ToString() == "13.27" || dt.Rows[i]["SubID"].ToString() == "12.15" || dt.Rows[i]["SubID"].ToString() == "12.16" || dt.Rows[i]["SubID"].ToString() == "13.16" || dt.Rows[i]["SubID"].ToString() == "13.17" || dt.Rows[i]["SubID"].ToString() == "13.19" || dt.Rows[i]["SubID"].ToString() == "13.3" || dt.Rows[i]["SubID"].ToString() == "11.7" || dt.Rows[i]["SubID"].ToString() == "11.8" || dt.Rows[i]["SubID"].ToString() == "11.9" || dt.Rows[i]["SubID"].ToString() == "11.1" || dt.Rows[i]["SubID"].ToString() == "13.7" || dt.Rows[i]["SubID"].ToString() == "13.8" || dt.Rows[i]["SubID"].ToString() == "13.9" || dt.Rows[i]["SubID"].ToString() == "13.1" || dt.Rows[i]["SubID"].ToString() == "13.11" || dt.Rows[i]["SubID"].ToString() == "13.12" || dt.Rows[i]["SubID"].ToString() == "12.12" || dt.Rows[i]["SubID"].ToString() == "12.13" || dt.Rows[i]["SubID"].ToString() == "12.14" || dt.Rows[i]["SubID"].ToString() == "12.18")
                    {
                        if (Achh.Length > 0)
                        {
                            dt.Rows[i]["ACH"] = Convert.ToInt32(Achh) * 90 / 100;
                        }
                        if (YTD.Length > 0)
                        {
                            dt.Rows[i]["YTD"] = Convert.ToInt32(YTD) * 90 / 100;
                        }
                        if (AchMonth.Length > 0)
                        {
                            dt.Rows[i]["AchMonth"] = Convert.ToInt32(AchMonth) * 90 / 100;
                        }
                    }

                }
            }
            if (ddlYearMonth.SelectedIndex > 0)
            {
                Querterwise = "NewFyear between (" + ddlYearMonth.SelectedValue + ") and  (" + ddlYearMonth.SelectedValue + ") ";
                YDTQuerterwise = "NewFyear between (" + ddlYearMonth.SelectedValue + ") and  (" + ddlYearMonth.SelectedValue + ") ";
                SqlParameter[] cmdParameters1 = new SqlParameter[]
                    {

                        new SqlParameter("@DID",ddlDonor.SelectedValue),
                            new SqlParameter("@YearID",lblFyear.Text),
                     new SqlParameter("@Quter",Quter),
                     new SqlParameter("@Month","4"),
                        new SqlParameter("@con",conditions),
                            new SqlParameter("@Target",Target),
                                new SqlParameter("@yTarget",TargetYDT),
                                    new SqlParameter("@Q1",Q1),
                                    new SqlParameter("@DistTpye",lblGType.Text),
                                     new SqlParameter("@conNew",condiNew),


                                         new SqlParameter("@Ach",Querterwise),
                                             new SqlParameter("@YTD",YDTQuerterwise),


                    };



                DataTable dtNEw = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptDonorTargetandachNew2026New]", cmdParameters1);
                for (int i = 0; i < dtNEw.Rows.Count; i++)
                {
                    DataRow[] dr = dt.Select("SubID ='" + dtNEw.Rows[i]["SubID"].ToString() + "'");
                    if (dr.Length > 0)
                    {
                        dr[0]["ACHMonth"] = dtNEw.Rows[i]["ACH"].ToString();
                        string Achh = dt.Rows[i]["ACH"].ToString();
                        string YTD = dt.Rows[i]["YTD"].ToString();
                        string AchMonth = dt.Rows[i]["AchMonth"].ToString();
                        string TargetCH = dt.Rows[i]["Target"].ToString();
                        int Totalch = 0;
                        int totalTargetAch = 0;

                        if (dt.Rows[i]["SubID"].ToString() == "3.10" || dt.Rows[i]["SubID"].ToString() == "13.29" || dt.Rows[i]["SubID"].ToString() == "13.25" || dt.Rows[i]["SubID"].ToString() == "12.4" || dt.Rows[i]["SubID"].ToString() == "12.8" || dt.Rows[i]["SubID"].ToString() == "13.26" || dt.Rows[i]["SubID"].ToString() == "12.17" || dt.Rows[i]["SubID"].ToString() == "13.4" || dt.Rows[i]["SubID"].ToString() == "13.27" || dt.Rows[i]["SubID"].ToString() == "12.15" || dt.Rows[i]["SubID"].ToString() == "12.16" || dt.Rows[i]["SubID"].ToString() == "13.16" || dt.Rows[i]["SubID"].ToString() == "13.17" || dt.Rows[i]["SubID"].ToString() == "13.19" || dt.Rows[i]["SubID"].ToString() == "13.3" || dt.Rows[i]["SubID"].ToString() == "11.7" || dt.Rows[i]["SubID"].ToString() == "11.8" || dt.Rows[i]["SubID"].ToString() == "11.9" || dt.Rows[i]["SubID"].ToString() == "11.1" || dt.Rows[i]["SubID"].ToString() == "13.7" || dt.Rows[i]["SubID"].ToString() == "13.8" || dt.Rows[i]["SubID"].ToString() == "13.9" || dt.Rows[i]["SubID"].ToString() == "13.1" || dt.Rows[i]["SubID"].ToString() == "13.11" || dt.Rows[i]["SubID"].ToString() == "13.12" || dt.Rows[i]["SubID"].ToString() == "12.12" || dt.Rows[i]["SubID"].ToString() == "12.13" || dt.Rows[i]["SubID"].ToString() == "12.14" || dt.Rows[i]["SubID"].ToString() == "12.18")
                        {
                            //if (Achh.Length > 0)
                            //{
                            //    dt.Rows[i]["ACH"] = Convert.ToInt32(Achh) * 90 / 100;
                            //}
                            //if (YTD.Length > 0)
                            //{
                            //    dt.Rows[i]["YTD"] = Convert.ToInt32(YTD) * 90 / 100;
                            //}
                            if (AchMonth.Length > 0)
                            {
                                dt.Rows[i]["AchMonth"] = Convert.ToInt32(Achh) * 90 / 100;
                            }
                        }
                    }
                }
            }

        }
        else if (Convert.ToInt32(ddlStartYear.SelectedValue) == 2024)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {

                new SqlParameter("@DID",ddlDonor.SelectedValue),
                    new SqlParameter("@YearID",lblFyear.Text),
             new SqlParameter("@Quter",Quter),
             new SqlParameter("@Month","4"),
                new SqlParameter("@con",conditions),
                    new SqlParameter("@Target",Target),
                        new SqlParameter("@yTarget",TargetYDT),
                            new SqlParameter("@Q1",Q1),
                            new SqlParameter("@DistTpye",lblGType.Text),
                             new SqlParameter("@conNew",condiNew),


                                 new SqlParameter("@Ach",Querterwise),
                                     new SqlParameter("@YTD",YDTQuerterwise),


            };



            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptDonorTargetandachNew2025New]", cmdParameters);
            if (ddlYearMonth.SelectedIndex <= 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Achh = dt.Rows[i]["ACH"].ToString();
                    string TargetCH = dt.Rows[i]["Target"].ToString();
                    int Totalch = 0;
                    int totalTargetAch = 0;
                    if (dt.Rows[i]["SubID"].ToString() == "10.1" || dt.Rows[i]["SubID"].ToString() == "11.5" || dt.Rows[i]["SubID"].ToString() == "11.6" || dt.Rows[i]["SubID"].ToString() == "10.7" || dt.Rows[i]["SubID"].ToString() == "13.25" || dt.Rows[i]["SubID"].ToString() == "13.26" || dt.Rows[i]["SubID"].ToString() == "13.4" || dt.Rows[i]["SubID"].ToString() == "13.27" || dt.Rows[i]["SubID"].ToString() == "13.16" || dt.Rows[i]["SubID"].ToString() == "13.17" || dt.Rows[i]["SubID"].ToString() == "13.19" || dt.Rows[i]["SubID"].ToString() == "13.3" || dt.Rows[i]["SubID"].ToString() == "13.7" || dt.Rows[i]["SubID"].ToString() == "13.8" || dt.Rows[i]["SubID"].ToString() == "13.9" || dt.Rows[i]["SubID"].ToString() == "13.10" || dt.Rows[i]["SubID"].ToString() == "13.11" || dt.Rows[i]["SubID"].ToString() == "13.12")
                    {
                        if (Achh.Length > 0)
                        {
                            dt.Rows[i]["ReportableAchv"] = Convert.ToInt32(Achh) * 90 / 100;
                        }

                    }
                    else if (dt.Rows[i]["SubID"].ToString() == "4.1")
                    {
                        if (Convert.ToInt32(ddlFrequency.SelectedValue) == 1)
                        {
                            if (Achh.Length > 0)
                            {
                                int Apr = 0;
                                int May = 0;
                                int Jun = 0;
                                if (dt.Rows[i]["Apr"].ToString().Length > 0)
                                {
                                    Apr = Convert.ToInt32(dt.Rows[i]["Apr"].ToString());
                                }
                                if (dt.Rows[i]["May"].ToString().Length > 0)
                                {
                                    May = Convert.ToInt32(dt.Rows[i]["May"].ToString());
                                }
                                if (dt.Rows[i]["Jun"].ToString().Length > 0)
                                {
                                    Jun = Convert.ToInt32(dt.Rows[i]["Jun"].ToString());
                                }

                                int AprAch = Convert.ToInt32(Apr) * 30 / 100;
                                int MayAch = Convert.ToInt32(May) * 40 / 100;
                                int JunAch = Convert.ToInt32(Jun) * 30 / 100;

                                if (Achh.Length > 0)
                                {
                                    dt.Rows[i]["ReportableAchv"] = AprAch + MayAch + JunAch;
                                }
                                if (TargetCH.Length > 0)
                                {
                                    if (Convert.ToInt32(AprAch + MayAch + JunAch) < Convert.ToInt32(TargetCH))
                                    {
                                        totalTargetAch = Convert.ToInt32(TargetCH) * 100 / 100;
                                    }

                                }
                            }
                        }
                    }
                    else if (dt.Rows[i]["SubID"].ToString() == "3.10")
                    {
                        if (Achh.Length > 0)
                        {

                            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 0)
                            {
                                DataTable dtT = null;
                                if (DateTime.Now.Month == 1 || DateTime.Now.Month == 2 || DateTime.Now.Month == 3)
                                {
                                    dtT = objMain.LoadData("select  [Q1]+[Q2]+[Q3]+[Q4] as Target FROM [PMS].[dbo].[tblDonorTarget] where subid='3.10'  and DonorID=" + ddlDonor.SelectedValue + "   ");
                                    if (dtT.Rows.Count > 0)
                                    {
                                        TargetCH = dtT.Rows[0]["Target"].ToString();
                                    }
                                    else
                                    {
                                        TargetCH = "0";
                                    }
                                    if (Achh.Length > 0)
                                    {
                                        Totalch = Convert.ToInt32(Achh) * 80 / 100;
                                        dt.Rows[i]["ReportableAchv"] = Convert.ToInt32(Achh) * 80 / 100;
                                    }
                                    if (TargetCH.Length > 0)
                                    {
                                        if (Convert.ToInt32(Achh) < Convert.ToInt32(TargetCH))
                                        {
                                            totalTargetAch = Convert.ToInt32(TargetCH) * 100 / 100;
                                        }

                                    }

                                    if (totalTargetAch > 0)
                                    {
                                        if (Totalch < totalTargetAch)
                                        {

                                            dt.Rows[i]["ReportableAchv"] = totalTargetAch;

                                        }
                                    }
                                }
                                if (DateTime.Now.Month == 4 || DateTime.Now.Month == 5 || DateTime.Now.Month == 6)
                                {
                                    dtT = objMain.LoadData("select  [Q1] as Target FROM [PMS].[dbo].[tblDonorTarget] where subid='3.10'  and DonorID=" + ddlDonor.SelectedValue + "   ");
                                    if (dtT.Rows.Count > 0)
                                    {
                                        TargetCH = dtT.Rows[0]["Target"].ToString();
                                    }
                                    else
                                    {
                                        TargetCH = "0";
                                    }
                                    if (Achh.Length > 0)
                                    {
                                        Totalch = Convert.ToInt32(Achh) * 80 / 100;
                                        dt.Rows[i]["ReportableAchv"] = Convert.ToInt32(Achh) * 80 / 100;
                                    }

                                    if (TargetCH.Length > 0)
                                    {
                                        if (Convert.ToInt32(Achh) < Convert.ToInt32(TargetCH))
                                        {
                                            totalTargetAch = Convert.ToInt32(TargetCH) * 30 / 100;
                                        }

                                    }
                                    if (totalTargetAch > 0)
                                    {
                                        if (Totalch < totalTargetAch)
                                        {
                                            dt.Rows[i]["ReportableAchv"] = totalTargetAch;

                                        }
                                    }
                                }
                                if (DateTime.Now.Month == 7 || DateTime.Now.Month == 8 || DateTime.Now.Month == 9)
                                {
                                    dtT = objMain.LoadData("select  [Q1]+[Q2] as Target FROM [PMS].[dbo].[tblDonorTarget] where subid='3.10'  and DonorID=" + ddlDonor.SelectedValue + "   ");
                                    if (dtT.Rows.Count > 0)
                                    {
                                        TargetCH = dtT.Rows[0]["Target"].ToString();
                                    }
                                    else
                                    {
                                        TargetCH = "0";
                                    }
                                    if (Achh.Length > 0)
                                    {
                                        Totalch = Convert.ToInt32(Achh) * 80 / 100;
                                        dt.Rows[i]["ReportableAchv"] = Convert.ToInt32(Achh) * 80 / 100;
                                    }

                                    if (TargetCH.Length > 0)
                                    {
                                        if (Convert.ToInt32(Achh) < Convert.ToInt32(TargetCH))
                                        {
                                            totalTargetAch = Convert.ToInt32(TargetCH) * 30 / 100;
                                        }

                                    }
                                    if (totalTargetAch > 0)
                                    {
                                        if (Totalch < totalTargetAch)
                                        {
                                            dt.Rows[i]["ReportableAchv"] = totalTargetAch;

                                        }
                                    }
                                }
                                if (DateTime.Now.Month == 10 || DateTime.Now.Month == 11 || DateTime.Now.Month == 12)
                                {
                                    dtT = objMain.LoadData("select  [Q1]+[Q2]+[Q3] as Target FROM [PMS].[dbo].[tblDonorTarget] where subid='3.10'  and DonorID=" + ddlDonor.SelectedValue + "   ");
                                    if (dtT.Rows.Count > 0)
                                    {
                                        TargetCH = dtT.Rows[0]["Target"].ToString();
                                    }
                                    else
                                    {
                                        TargetCH = "0";
                                    }
                                    if (Achh.Length > 0)
                                    {
                                        Totalch = Convert.ToInt32(Achh) * 80 / 100;
                                        dt.Rows[i]["ReportableAchv"] = Convert.ToInt32(Achh) * 80 / 100;
                                    }

                                    if (TargetCH.Length > 0)
                                    {
                                        if (Convert.ToInt32(Achh) < Convert.ToInt32(TargetCH))
                                        {
                                            totalTargetAch = Convert.ToInt32(TargetCH) * 60 / 100;
                                        }

                                    }
                                    if (totalTargetAch > 0)
                                    {
                                        if (Totalch < totalTargetAch)
                                        {

                                            dt.Rows[i]["ReportableAchv"] = totalTargetAch;

                                        }
                                    }
                                }
                            }
                        }
                        if (Convert.ToInt32(ddlFrequency.SelectedValue) == 1)
                        {
                            if (Achh.Length > 1)
                            {
                                Totalch = Convert.ToInt32(Achh) * 80 / 100;
                                dt.Rows[i]["ReportableAchv"] = Convert.ToInt32(Achh) * 80 / 100;
                            }

                            if (TargetCH.Length > 0)
                            {
                                if (Convert.ToInt32(Achh) < Convert.ToInt32(TargetCH))
                                {
                                    totalTargetAch = Convert.ToInt32(TargetCH) * 30 / 100;
                                }

                            }
                            if (totalTargetAch > 0)
                            {
                                if (Totalch < totalTargetAch)
                                {

                                    dt.Rows[i]["ReportableAchv"] = totalTargetAch;

                                }
                            }

                        }

                        if (Convert.ToInt32(ddlFrequency.SelectedValue) == 2)
                        {
                            if (Achh.Length > 1)
                            {
                                Totalch = Convert.ToInt32(Achh) * 80 / 100;
                                dt.Rows[i]["ReportableAchv"] = Convert.ToInt32(Achh) * 80 / 100;
                            }

                            if (TargetCH.Length > 0)
                            {
                                if (Convert.ToInt32(Achh) < Convert.ToInt32(TargetCH))
                                {
                                    totalTargetAch = Convert.ToInt32(TargetCH) * 30 / 100;
                                }

                            }
                            if (totalTargetAch > 0)
                            {
                                if (Totalch < totalTargetAch)
                                {
                                    dt.Rows[i]["ReportableAchv"] = totalTargetAch;

                                }
                            }
                        }

                        if (Convert.ToInt32(ddlFrequency.SelectedValue) == 3)
                        {
                            if (Achh.Length > 1)
                            {
                                Totalch = Convert.ToInt32(Achh) * 80 / 100;
                                dt.Rows[i]["ReportableAchv"] = Convert.ToInt32(Achh) * 80 / 100;

                                if (TargetCH.Length > 0)
                                {
                                    if (Convert.ToInt32(Achh) < Convert.ToInt32(TargetCH))
                                    {
                                        totalTargetAch = Convert.ToInt32(TargetCH) * 60 / 100;
                                    }

                                }
                                if (totalTargetAch > 0)
                                {
                                    if (Totalch < totalTargetAch)
                                    {
                                        dt.Rows[i]["ReportableAchv"] = totalTargetAch;

                                    }
                                }
                            }


                        }

                        if (Convert.ToInt32(ddlFrequency.SelectedValue) == 4)
                        {
                            if (Achh.Length > 1)
                            {
                                Totalch = Convert.ToInt32(Achh) * 80 / 100;
                                dt.Rows[i]["ReportableAchv"] = Convert.ToInt32(Achh) * 80 / 100;

                                if (TargetCH.Length > 0)
                                {
                                    if (Convert.ToInt32(Achh) < Convert.ToInt32(TargetCH))
                                    {
                                        totalTargetAch = Convert.ToInt32(TargetCH) * 100 / 100;
                                    }

                                }
                                if (totalTargetAch > 0)
                                {
                                    if (Totalch < totalTargetAch)
                                    {

                                        dt.Rows[i]["ReportableAchv"] = totalTargetAch;

                                    }
                                }
                            }

                        }
                    }
                    else if (dt.Rows[i]["SubID"].ToString() == "1.1" || dt.Rows[i]["SubID"].ToString() == "5.1" || dt.Rows[i]["SubID"].ToString() == "5.2" || dt.Rows[i]["SubID"].ToString() == "6.1" || dt.Rows[i]["SubID"].ToString() == "6.5" || dt.Rows[i]["SubID"].ToString() == "4.2" || dt.Rows[i]["SubID"].ToString() == "15.1" || dt.Rows[i]["SubID"].ToString() == "15.3" || dt.Rows[i]["SubID"].ToString() == "15.4" || dt.Rows[i]["SubID"].ToString() == "11.7" || dt.Rows[i]["SubID"].ToString() == "11.8" || dt.Rows[i]["SubID"].ToString() == "11.9" || dt.Rows[i]["SubID"].ToString() == "11.10" || dt.Rows[i]["SubID"].ToString() == "4.3")
                    {
                        if (Achh.Length > 0)
                        {
                            dt.Rows[i]["ReportableAchv"] = Achh;
                        }
                    }
                    else if (dt.Rows[i]["SubID"].ToString() == "8.3" || dt.Rows[i]["SubID"].ToString() == "8.6" || dt.Rows[i]["SubID"].ToString() == "9.1" || dt.Rows[i]["SubID"].ToString() == "9.2" || dt.Rows[i]["SubID"].ToString() == "12.4" || dt.Rows[i]["SubID"].ToString() == "12.8" || dt.Rows[i]["SubID"].ToString() == "12.17" || dt.Rows[i]["SubID"].ToString() == "12.15" || dt.Rows[i]["SubID"].ToString() == "12.16" || dt.Rows[i]["SubID"].ToString() == "12.12" || dt.Rows[i]["SubID"].ToString() == "12.13" || dt.Rows[i]["SubID"].ToString() == "12.14" || dt.Rows[i]["SubID"].ToString() == "12.18")
                    {

                        if (Achh.Length > 0)
                        {
                            dt.Rows[i]["ReportableAchv"] = Convert.ToInt32(Achh) * 90 / 100;
                        }

                    }
                    else
                    {

                    }
                }
            }
            if (ddlYearMonth.SelectedIndex > 0)
            {
                Querterwise = "NewFyear between (" + ddlYearMonth.SelectedValue + ") and  (" + ddlYearMonth.SelectedValue + ") ";
                YDTQuerterwise = "NewFyear between (" + ddlYearMonth.SelectedValue + ") and  (" + ddlYearMonth.SelectedValue + ") ";
                SqlParameter[] cmdParameters1 = new SqlParameter[]
                    {

                        new SqlParameter("@DID",ddlDonor.SelectedValue),
                            new SqlParameter("@YearID",lblFyear.Text),
                     new SqlParameter("@Quter",Quter),
                     new SqlParameter("@Month","4"),
                        new SqlParameter("@con",conditions),
                            new SqlParameter("@Target",Target),
                                new SqlParameter("@yTarget",TargetYDT),
                                    new SqlParameter("@Q1",Q1),
                                    new SqlParameter("@DistTpye",lblGType.Text),
                                     new SqlParameter("@conNew",condiNew),


                                         new SqlParameter("@Ach",Querterwise),
                                             new SqlParameter("@YTD",YDTQuerterwise),


                    };



                DataTable dtNEw = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptDonorTargetandachNew2025New]", cmdParameters1);
                for (int i = 0; i < dtNEw.Rows.Count; i++)
                {
                    DataRow[] dr = dt.Select("SubID ='" + dtNEw.Rows[i]["SubID"].ToString() + "'");
                    if (dr.Length > 0)
                    {
                        dr[0]["ACHMonth"] = dtNEw.Rows[i]["ACH"].ToString();




                        string Achh = dtNEw.Rows[i]["ACH"].ToString();
                        if (dtNEw.Rows[i]["SubID"].ToString() == "11.7" || dtNEw.Rows[i]["SubID"].ToString() == "11.2" || dtNEw.Rows[i]["SubID"].ToString() == "11.8" || dtNEw.Rows[i]["SubID"].ToString() == "11.9" || dtNEw.Rows[i]["SubID"].ToString() == "11.10" || dtNEw.Rows[i]["SubID"].ToString() == "10.1" || dtNEw.Rows[i]["SubID"].ToString() == "10.7" || dtNEw.Rows[i]["SubID"].ToString() == "13.25" || dtNEw.Rows[i]["SubID"].ToString() == "13.26" || dtNEw.Rows[i]["SubID"].ToString() == "13.4" || dtNEw.Rows[i]["SubID"].ToString() == "13.27" || dtNEw.Rows[i]["SubID"].ToString() == "13.16" || dtNEw.Rows[i]["SubID"].ToString() == "13.17" || dtNEw.Rows[i]["SubID"].ToString() == "13.19" || dtNEw.Rows[i]["SubID"].ToString() == "13.3" || dtNEw.Rows[i]["SubID"].ToString() == "13.7" || dtNEw.Rows[i]["SubID"].ToString() == "13.8" || dtNEw.Rows[i]["SubID"].ToString() == "13.9" || dtNEw.Rows[i]["SubID"].ToString() == "13.10" || dtNEw.Rows[i]["SubID"].ToString() == "13.11" || dtNEw.Rows[i]["SubID"].ToString() == "13.12")
                        {
                            if (Achh.Length > 0)
                            {
                                dr[0]["ReportableAchv"] = Convert.ToInt32(Achh) * 90 / 100;
                            }

                        }
                        else if (dt.Rows[i]["SubID"].ToString() == "3.10")
                        {
                            if (Achh.Length > 0)
                            {
                                int TQ = 0;
                                if (Convert.ToInt32(ddlFrequency.SelectedValue) == 0)
                                {
                                    DataTable dtT = null;
                                    if (DateTime.Now.Month == 1 || DateTime.Now.Month == 2 || DateTime.Now.Month == 3)
                                    {

                                        if (Achh.Length > 0)
                                        {

                                            dr[0]["ReportableAchv"] = Convert.ToInt32(Achh) * 80 / 100;
                                        }


                                    }
                                    if (DateTime.Now.Month == 4 || DateTime.Now.Month == 5 || DateTime.Now.Month == 6)
                                    {
                                        if (Achh.Length > 0)
                                        {

                                            dr[0]["ReportableAchv"] = Convert.ToInt32(Achh) * 80 / 100;
                                        }


                                    }
                                    if (DateTime.Now.Month == 7 || DateTime.Now.Month == 8 || DateTime.Now.Month == 9)
                                    {

                                        if (Achh.Length > 0)
                                        {

                                            dr[0]["ReportableAchv"] = Convert.ToInt32(Achh) * 80 / 100;
                                        }


                                    }
                                    if (DateTime.Now.Month == 10 || DateTime.Now.Month == 11 || DateTime.Now.Month == 12)
                                    {

                                        if (Achh.Length > 0)
                                        {

                                            dr[0]["ReportableAchv"] = Convert.ToInt32(Achh) * 80 / 100;
                                        }


                                    }
                                }
                            }
                            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 1)
                            {
                                if (Achh.Length > 0)
                                {

                                    dr[0]["ReportableAchv"] = Convert.ToInt32(Achh) * 80 / 100;
                                }



                            }

                            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 2)
                            {
                                if (Achh.Length > 0)
                                {

                                    dr[0]["ReportableAchv"] = Convert.ToInt32(Achh) * 80 / 100;
                                }


                            }

                            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 3)
                            {
                                if (Achh.Length > 0)
                                {

                                    dr[0]["ReportableAchv"] = Convert.ToInt32(Achh) * 80 / 100;
                                }


                            }

                            if (Convert.ToInt32(ddlFrequency.SelectedValue) == 4)
                            {
                                if (Achh.Length > 0)
                                {

                                    dr[0]["ReportableAchv"] = Convert.ToInt32(Achh) * 80 / 100;
                                }

                            }
                        }
                        else if (dtNEw.Rows[i]["SubID"].ToString() == "5.1" || dtNEw.Rows[i]["SubID"].ToString() == "5.2" || dtNEw.Rows[i]["SubID"].ToString() == "6.1" || dtNEw.Rows[i]["SubID"].ToString() == "6.5" || dtNEw.Rows[i]["SubID"].ToString() == "4.2" || dtNEw.Rows[i]["SubID"].ToString() == "15.1" || dtNEw.Rows[i]["SubID"].ToString() == "15.3" || dtNEw.Rows[i]["SubID"].ToString() == "15.4" || dtNEw.Rows[i]["SubID"].ToString() == "11.7" || dtNEw.Rows[i]["SubID"].ToString() == "11.8" || dtNEw.Rows[i]["SubID"].ToString() == "11.9" || dtNEw.Rows[i]["SubID"].ToString() == "11.10" || dtNEw.Rows[i]["SubID"].ToString() == "4.3")
                        {
                            if (Achh.Length > 0)
                            {
                                dr[0]["ReportableAchv"] = Achh;
                            }
                        }
                        else if (dt.Rows[i]["SubID"].ToString() == "8.3" || dt.Rows[i]["SubID"].ToString() == "8.6" || dtNEw.Rows[i]["SubID"].ToString() == "1.1" || dtNEw.Rows[i]["SubID"].ToString() == "9.1" || dtNEw.Rows[i]["SubID"].ToString() == "9.2" || dtNEw.Rows[i]["SubID"].ToString() == "12.4" || dtNEw.Rows[i]["SubID"].ToString() == "12.8" || dtNEw.Rows[i]["SubID"].ToString() == "12.17" || dtNEw.Rows[i]["SubID"].ToString() == "12.15" || dtNEw.Rows[i]["SubID"].ToString() == "12.16" || dtNEw.Rows[i]["SubID"].ToString() == "12.12" || dtNEw.Rows[i]["SubID"].ToString() == "12.13" || dtNEw.Rows[i]["SubID"].ToString() == "12.14" || dtNEw.Rows[i]["SubID"].ToString() == "12.18")
                        {
                            if (Achh.Length > 0)
                            {
                                dr[0]["ReportableAchv"] = Convert.ToInt32(Achh) * 90 / 100;
                            }

                        }
                        else
                        {

                        }

                    }
                }
            }

        }
        else if (Convert.ToInt32(ddlStartYear.SelectedValue) == 2023)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {

                new SqlParameter("@DID",ddlDonor.SelectedValue),
                    new SqlParameter("@YearID",lblFyear.Text),
             new SqlParameter("@Quter",Quter),
             new SqlParameter("@Month","4"),
                new SqlParameter("@con",conditions),
                    new SqlParameter("@Target",Target),
                        new SqlParameter("@yTarget",TargetYDT),
                            new SqlParameter("@Q1",Q1),
                            new SqlParameter("@DistTpye",lblGType.Text),
                             new SqlParameter("@conNew",condiNew),


                                 new SqlParameter("@Ach",Querterwise),
                                     new SqlParameter("@YTD",YDTQuerterwise),


            };



            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptDonorTargetandachNew2024]", cmdParameters);
        }
        else
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {

                new SqlParameter("@DID",ddlDonor.SelectedValue),
                    new SqlParameter("@YearID",lblFyear.Text),
             new SqlParameter("@Quter",Quter),
             new SqlParameter("@Month","4"),
                new SqlParameter("@con",conditions),
                    new SqlParameter("@Target",Target),
                        new SqlParameter("@yTarget",TargetYDT),
                            new SqlParameter("@Q1",Q1),
                            new SqlParameter("@DistTpye",lblGType.Text),
                             new SqlParameter("@conNew",condiNew),


                                 new SqlParameter("@Ach",Querterwise),
                                     new SqlParameter("@YTD",YDTQuerterwise),


            };



            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptDonorTargetandachNew]", cmdParameters);
        }


        string a = Querterwise.Remove(0, 8);
        string b = YDTQuerterwise.Remove(0, 8);
        TeamAch = "(year([FromDate])*10000+month([FromDate])*100)  " + a + " ";
        TeamYtd = "(year([FromDate])*10000+month([FromDate])*100)   " + b + " ";
        //foreach (DataRow dr in dt.Rows)
        //{
        //    if (dr["SubID"].ToString() == "12.5"  ||  dr["SubID"].ToString() == "12.4" || dr["SubID"].ToString() == "12.6"  || dr["SubID"].ToString() == "12.7"  || dr["SubID"].ToString() == "12.8" || dr["SubID"].ToString() == "12.10"  || dr["SubID"].ToString() == "12.11"                || dr["SubID"].ToString() == "12.12"  || dr["SubID"].ToString() == "12.13"   || dr["SubID"].ToString() == "12.14"    || dr["SubID"].ToString() == "12.15"   || dr["SubID"].ToString() == "12.16"  || dr["SubID"].ToString() == "12.17")
        //    {
        //        Int32 Count1 = 0;
        //        if (dr["YTD"].ToString()!="")
        //        {
        //             Count1 = Convert.ToInt32(dr["YTD"].ToString());
        //        }

        //         if (Count1 > 0)
        //         {
        //             SqlParameter[] cmdParameters1 = new SqlParameter[]
        //          {


        //           new SqlParameter("@Con",condiNewTeamBalik),    
        //           new SqlParameter("@s",dr["SubID"].ToString()),                       
        //            new SqlParameter("@Ach",TeamAch),
        //            new SqlParameter("@YTD",TeamYtd),

        //         };
        //             DataTable dtname = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[RptLoadTeamTraing]", cmdParameters1);
        //             if (dtname.Rows.Count > 0)
        //             {
        //                 if (dtname.Rows[0]["DaysCount"].ToString().Length > 0)
        //                 {
        //                     dr["Ach"] = dr["Ach"] + "," + dtname.Rows[0]["DaysCount"];
        //                 }
        //                 if (dtname.Rows[1]["DaysCount"].ToString().Length > 0)
        //                 {
        //                     dr["YTD"] = dr["YTD"] + "," + dtname.Rows[1]["DaysCount"];
        //                 }
        //             }
        //         }
        //    }

        //    if (dr["SubID"].ToString() == "13.1" || dr["SubID"].ToString() == "13.2" || dr["SubID"].ToString() == "13.3" || dr["SubID"].ToString() == "13.4" || dr["SubID"].ToString() == "13.5" || dr["SubID"].ToString() == "13.6" || dr["SubID"].ToString() == "13.7" || dr["SubID"].ToString() == "13.8" || dr["SubID"].ToString() == "13.9" || dr["SubID"].ToString() == "13.10" || dr["SubID"].ToString() == "13.11" || dr["SubID"].ToString() == "13.12" || dr["SubID"].ToString() == "13.13" || dr["SubID"].ToString() == "13.13" || dr["SubID"].ToString() == "13.14" || dr["SubID"].ToString() == "13.15" || dr["SubID"].ToString() == "13.16" || dr["SubID"].ToString() == "13.17" || dr["SubID"].ToString() == "13.18" || dr["SubID"].ToString() == "13.19" || dr["SubID"].ToString() == "13.20")
        //    {


        //        Int32 Count = 0;
        //        if (dr["YTD"].ToString() != "")
        //        {
        //            Count= Convert.ToInt32(dr["YTD"].ToString());
        //        }


        //            if (Count > 0)
        //            {
        //                SqlParameter[] cmdParameters1 = new SqlParameter[]
        //          {


        //           new SqlParameter("@Con",condiNew),    
        //           new SqlParameter("@s",dr["SubID"].ToString()),                       
        //            new SqlParameter("@Ach",TeamAch),
        //            new SqlParameter("@YTD",TeamYtd),

        //         };
        //                DataTable dtname = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[RptStaffTraing]", cmdParameters1);
        //                if (dtname.Rows.Count > 0)
        //                {
        //                    if (dtname.Rows[0]["DaysCount"].ToString().Length > 0)
        //                    {
        //                        dr["Ach"] = dr["Ach"] + "," + dtname.Rows[0]["DaysCount"];
        //                    }
        //                    if (dtname.Rows[1]["DaysCount"].ToString().Length > 0)
        //                    {
        //                        dr["YTD"] = dr["YTD"] + "," + dtname.Rows[1]["DaysCount"];
        //                    }
        //                }
        //            }
        //        }

        //}

        dt.Columns.Remove("SubID");



        Session["Enroll123"] = dt;
        if (Convert.ToInt32(ddlStartYear.SelectedValue) >= 2025)
        {
            GV_DynamicGrid.Columns[5].Visible = false;

            GV_DynamicGrid.Visible = true;
            GridView1.Visible = false;
            dt.Columns.Remove("Apr");
            dt.Columns.Remove("May");
            dt.Columns.Remove("Jun");
            dt.Columns.Remove("GKPVal");
            if (ddlYearMonth.SelectedIndex > 0)
            {
                GV_DynamicGrid.Columns[2].HeaderText = Hearder;
                GV_DynamicGrid.Columns[4].HeaderText = HearderMonth;
                GV_DynamicGrid.Columns[5].HeaderText = "Reportable" + ACH;
                GV_DynamicGrid.Columns[6].HeaderText = ACH;
                GV_DynamicGrid.Columns[4].Visible = true;
            }
            else
            {
                GV_DynamicGrid.Columns[2].HeaderText = Hearder;
                GV_DynamicGrid.Columns[5].HeaderText = "Reportable" + ACH;
                GV_DynamicGrid.Columns[6].HeaderText = ACH;
                GV_DynamicGrid.Columns[4].Visible = false;
            }
            GV_DynamicGrid.DataSource = dt;
            GV_DynamicGrid.DataBind();

            if (ddlYearMonth.SelectedIndex > 0)
            {

            }
            else
            {
                dt.Columns.Remove("ACHMonth");
            }

        }
        else if (Convert.ToInt32(ddlStartYear.SelectedValue) == 2024)
        {
            GV_DynamicGrid.Columns[5].Visible = false;

            GV_DynamicGrid.Visible = true;
            GridView1.Visible = false;
            dt.Columns.Remove("Apr");
            dt.Columns.Remove("May");
            dt.Columns.Remove("Jun");
            dt.Columns.Remove("GKPVal");
            if (ddlYearMonth.SelectedIndex > 0)
            {
                GV_DynamicGrid.Columns[2].HeaderText = Hearder;
                GV_DynamicGrid.Columns[4].HeaderText = HearderMonth;
                GV_DynamicGrid.Columns[5].HeaderText = "Reportable" + ACH;
                GV_DynamicGrid.Columns[6].HeaderText = ACH;
                GV_DynamicGrid.Columns[4].Visible = true;
            }
            else
            {
                GV_DynamicGrid.Columns[2].HeaderText = Hearder;
                GV_DynamicGrid.Columns[5].HeaderText = "Reportable" + ACH;
                GV_DynamicGrid.Columns[6].HeaderText = ACH;
                GV_DynamicGrid.Columns[4].Visible = false;
            }
            GV_DynamicGrid.DataSource = dt;
            GV_DynamicGrid.DataBind();

            if (ddlYearMonth.SelectedIndex > 0)
            {

            }
            else
            {
                dt.Columns.Remove("ACHMonth");
            }

        }
        else
        {
            GV_DynamicGrid.Visible = false;
            GridView1.Visible = true;
            GridView1.Columns[2].HeaderText = Hearder;
            GridView1.Columns[4].HeaderText = ACH;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }


    }
    private void GenerateExcelNewOut(string FIleName)
    {
        try
        {


            string Hearderttt = "";
            string Hearderkk = "";
            string ACH = "";
            string Target = "";
            string TargetYDT = "";
            if (lblFrequency.Text == "1" || lblFrequency.Text == "2")
            {
                if (Convert.ToInt32(ddlFrequency.SelectedValue) == 0)
                {

                    Hearderttt = "Target -Quarterly";
                    Hearderkk = "Quarterly";
                    ACH = " Achv-Quarterly";
                    Target = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
                    TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
                }
                else
                {


                    Hearderttt = "Target - " + ddlFrequency.SelectedItem.Text + "";
                    Hearderkk = " " + ddlFrequency.SelectedItem.Text + "";
                    ACH = " Achv-" + ddlFrequency.SelectedItem.Text + "";
                }
                if (Convert.ToInt32(ddlFrequency.SelectedValue) == 1)
                {
                    Target = "Sum(Q1)";
                    TargetYDT = "Sum(Q1)";
                }
                if (Convert.ToInt32(ddlFrequency.SelectedValue) == 2)
                {
                    Target = "Sum(Q2)";
                    TargetYDT = "Sum(Q1)+Sum(Q2)";
                }
                if (Convert.ToInt32(ddlFrequency.SelectedValue) == 3)
                {
                    Target = "Sum(Q3)";
                    TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)";
                }

                if (Convert.ToInt32(ddlFrequency.SelectedValue) == 4)
                {
                    Target = "Sum(Q4)";
                    TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
                }
            }
            if (lblFrequency.Text == "2")
            {
                if (Convert.ToInt32(ddlFrequency.SelectedValue) == 0)
                {

                    Hearderttt = "Target-Half Yearly";
                    Hearderkk = "Target-Half Yearly";
                    ACH = " Achv-Half Yearly";

                    Target = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
                    TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
                }
                else if (Convert.ToInt32(ddlFrequency.SelectedValue) == 1)
                {

                    Hearderttt = "Target-" + ddlFrequency.SelectedItem.Text + "";
                    Hearderkk = "" + ddlFrequency.SelectedItem.Text + "";
                    ACH = " Achv-" + ddlFrequency.SelectedItem.Text + "";
                    Target = "Sum(Q1)";
                    TargetYDT = "Sum(Q1)";
                }
                else
                {

                    Hearderttt = "Target-" + ddlFrequency.SelectedItem.Text + "";
                    Hearderkk = "" + ddlFrequency.SelectedItem.Text + "";
                    ACH = " Achv -" + ddlFrequency.SelectedItem.Text + "";
                    Target = "Sum(Q2)";
                    TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
                }
            }
            if (lblFrequency.Text == "3")
            {

                Hearderttt = "Target-Yearly";
                Hearderkk = "Yearly";
                ACH = " Achv-Yearly";
                Target = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
                TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
            }
            string hhh = "";
            string TTT = "";
            if (Convert.ToInt32(ddInGeography.SelectedValue) == 1)
            {
            }
            if (Convert.ToInt32(ddInGeography.SelectedValue) == 2)
            {
                TTT = "District:" + lblDistrict1.Text + "";
            }
            if (Convert.ToInt32(ddInGeography.SelectedValue) == 3)
            {
                TTT = "Block:" + lblBlock1.Text + "";
            }
            if (lblFrequency.Text == "1")
            {
                hhh = "Quarterly";
            }
            if (lblFrequency.Text == "2")
            {
                hhh = "Half Yearly";
            }
            if (lblFrequency.Text == "3")
            {
                hhh = "Yearly";
            }
            if (lblFrequency.Text == "4" && ddlYearMonth.SelectedIndex > 0 && ddlFrequency.SelectedValue == "5")
            {
                hhh = "Monthly";
                Hearderttt = "Target -Quarterly";
                Hearderkk = "Quarterly";
                ACH = " Achv-Quarterly";
            }
            if (lblFrequency.Text == "4" && ddlYearMonth.SelectedIndex > 0 && ddlFrequency.SelectedValue != "5")
            {
                hhh = "Monthly";

                if (Convert.ToInt32(ddlFrequency.SelectedValue) == 0)
                {

                    Hearderttt = "Target -Quarterly";
                    Hearderkk = "Quarterly";
                    ACH = " Achv-Quarterly";
                    Target = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
                    TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
                }
                else
                {


                    Hearderttt = "Target - " + ddlFrequency.SelectedItem.Text + "";
                    Hearderkk = " " + ddlFrequency.SelectedItem.Text + "";
                    ACH = " Achv-" + ddlFrequency.SelectedItem.Text + "";
                }
                if (Convert.ToInt32(ddlFrequency.SelectedValue) == 1)
                {
                    Target = "Sum(Q1)";
                    TargetYDT = "Sum(Q1)";
                }
                if (Convert.ToInt32(ddlFrequency.SelectedValue) == 2)
                {
                    Target = "Sum(Q2)";
                    TargetYDT = "Sum(Q1)+Sum(Q2)";
                }
                if (Convert.ToInt32(ddlFrequency.SelectedValue) == 3)
                {
                    Target = "Sum(Q3)";
                    TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)";
                }

                if (Convert.ToInt32(ddlFrequency.SelectedValue) == 4)
                {
                    Target = "Sum(Q4)";
                    TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
                }



            }

            DataTable dt = Session["Enroll123"] as DataTable;
            if (dt.Rows.Count > 0)
            {
                if (dt.Columns.Contains("ReportableAchv"))
                {
                    dt.Columns.Remove("ReportableAchv");
                }

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                string Fullfilename = "" + FIleName + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");

                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                if (ddlYearMonth.SelectedIndex > 0)
                {
                    HttpContext.Current.Response.Write("<table  >");
                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<td colspan='7' ' style='text-align:Center;border:.3pt solid windowtext;font-weight:700; font-width:bold;'>Donor Report :" + ddlDonor.SelectedItem.Text + " </td>");

                    HttpContext.Current.Response.Write("</tr>");
                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<td colspan='3' ' style='text-align:Left;border:.3pt solid windowtext;font-width:bold;font-weight:700;'>Reporting Start Month and Year  :" + lblTarget.Text + " </td>");
                    HttpContext.Current.Response.Write("<td colspan='4' ' style='text-align:Left;border:.3pt solid windowtext;font-width:bold;font-weight:700;'> Project Period:" + lblPri.Text + " </td>");

                    HttpContext.Current.Response.Write("</tr>");

                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<td colspan='3' ' style='text-align:Left;border:.3pt solid windowtext;font-width:bold;font-weight:700;'>Reporting Frequency: " + hhh + " </td>");

                    HttpContext.Current.Response.Write("<td colspan='3' ' style='text-align:Left;border:.3pt solid windowtext;font-width:bold;font-weight:700;'>" + hhh + " : " + Hearderkk + " </td>");

                    HttpContext.Current.Response.Write("</tr>");

                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");

                    HttpContext.Current.Response.Write("<td colspan='2' ' style='text-align:Left;border:.3pt solid windowtext;font-width:bold;font-weight:700;'>Geography: " + TTT + " </td>");
                    HttpContext.Current.Response.Write("<td colspan='2' ' style='text-align:Left;border:.3pt solid windowtext;font-width:bold;font-weight:700;'>Geography Type: " + lblGType.Text + " </td>");
                    HttpContext.Current.Response.Write("<td colspan='3' ' style='text-align:Left;border:.3pt solid windowtext;font-width:bold;font-weight:700;'>Geography Level: " + ddInGeography.SelectedItem.Text + " </td>");

                    HttpContext.Current.Response.Write("</tr>");


                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
                    string m = "Ach-" + ddlYearMonth.SelectedItem.Text;

                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Reporting Outcome</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Reporting Indicator</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>" + Hearderttt + "</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Target-YTD</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> " + m + " </th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> " + "Reportable" + ACH + " </th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> " + ACH + " </th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Achv-YTD</th>");


                    HttpContext.Current.Response.Write("</tr>");

                }

                else
                {
                    HttpContext.Current.Response.Write("<table  >");
                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<td colspan='6' ' style='text-align:Center;border:.3pt solid windowtext;font-weight:700; font-width:bold;'>Donor Report :" + ddlDonor.SelectedItem.Text + " </td>");

                    HttpContext.Current.Response.Write("</tr>");
                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<td colspan='3' ' style='text-align:Left;border:.3pt solid windowtext;font-width:bold;font-weight:700;'>Reporting Start Month and Year  :" + lblTarget.Text + " </td>");
                    HttpContext.Current.Response.Write("<td colspan='3' ' style='text-align:Left;border:.3pt solid windowtext;font-width:bold;font-weight:700;'> Project Period:" + lblPri.Text + " </td>");

                    HttpContext.Current.Response.Write("</tr>");

                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<td colspan='3' ' style='text-align:Left;border:.3pt solid windowtext;font-width:bold;font-weight:700;'>Reporting Frequency: " + hhh + " </td>");

                    HttpContext.Current.Response.Write("<td colspan='3' ' style='text-align:Left;border:.3pt solid windowtext;font-width:bold;font-weight:700;'>" + hhh + " : " + Hearderkk + " </td>");

                    HttpContext.Current.Response.Write("</tr>");

                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");

                    HttpContext.Current.Response.Write("<td colspan='2' ' style='text-align:Left;border:.3pt solid windowtext;font-width:bold;font-weight:700;'>Geography: " + TTT + " </td>");
                    HttpContext.Current.Response.Write("<td colspan='2' ' style='text-align:Left;border:.3pt solid windowtext;font-width:bold;font-weight:700;'>Geography Type: " + lblGType.Text + " </td>");
                    HttpContext.Current.Response.Write("<td colspan='2' ' style='text-align:Left;border:.3pt solid windowtext;font-width:bold;font-weight:700;'>Geography Level: " + ddInGeography.SelectedItem.Text + " </td>");

                    HttpContext.Current.Response.Write("</tr>");


                    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";


                    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Reporting Outcome</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Reporting Indicator</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>" + Hearderttt + "</th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Target-YTD</th>");
                    //HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> " + "Reportable" + ACH + " </th>");
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> " + ACH + " </th>");

                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Achv-YTD</th>");


                    HttpContext.Current.Response.Write("</tr>");
                }

                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";







                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    HttpContext.Current.Response.Write("<tr>");
                    //HttpContext.Current.Response.Write("<td >Direct</td>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {


                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");


                    }
                }



                HttpContext.Current.Response.Write("</tr>");





                //HttpContext.Current.Response.Write("<tr>");
                //HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
                //HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
                //HttpContext.Current.Response.Write("</tr>");


                HttpContext.Current.Response.Write("</table>");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }
        catch
        {
            throw;
        }


    }
    private void ExporttoExcel(GridView Gv, DataTable table, string FileName)
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
            int columnscount = Gv.HeaderRow.Cells.Count;


            for (int j = 0; j < columnscount; j++)
            {      //write in new column
                HttpContext.Current.Response.Write("<Td>");
                //Get column headers  and make it as bold in excel columns
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(Gv.HeaderRow.Cells[j].Text);
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

    private void GenerateExcelNewOut2023(string FIleName)
    {
        try
        {


            string Hearderttt = "";
            string Hearderkk = "";
            string ACH = "";
            string Target = "";
            string TargetYDT = "";
            if (lblFrequency.Text == "1")
            {
                if (Convert.ToInt32(ddlFrequency.SelectedValue) == 0)
                {

                    Hearderttt = "Target -Quarterly";
                    Hearderkk = "Quarterly";
                    ACH = " Achv-Quarterly";
                    Target = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
                    TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
                }
                else
                {


                    Hearderttt = "Target - " + ddlFrequency.SelectedItem.Text + "";
                    Hearderkk = " " + ddlFrequency.SelectedItem.Text + "";
                    ACH = " Achv-" + ddlFrequency.SelectedItem.Text + "";
                }
                if (Convert.ToInt32(ddlFrequency.SelectedValue) == 1)
                {
                    Target = "Sum(Q1)";
                    TargetYDT = "Sum(Q1)";
                }
                if (Convert.ToInt32(ddlFrequency.SelectedValue) == 2)
                {
                    Target = "Sum(Q2)";
                    TargetYDT = "Sum(Q1)+Sum(Q2)";
                }
                if (Convert.ToInt32(ddlFrequency.SelectedValue) == 3)
                {
                    Target = "Sum(Q3)";
                    TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)";
                }

                if (Convert.ToInt32(ddlFrequency.SelectedValue) == 4)
                {
                    Target = "Sum(Q4)";
                    TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
                }
            }
            if (lblFrequency.Text == "2")
            {
                if (Convert.ToInt32(ddlFrequency.SelectedValue) == 0)
                {

                    Hearderttt = "Target-Half Yearly";
                    Hearderkk = "Target-Half Yearly";
                    ACH = " Achv-Half Yearly";

                    Target = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
                    TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
                }
                else if (Convert.ToInt32(ddlFrequency.SelectedValue) == 1)
                {

                    Hearderttt = "Target-" + ddlFrequency.SelectedItem.Text + "";
                    Hearderkk = "" + ddlFrequency.SelectedItem.Text + "";
                    ACH = " Achv-" + ddlFrequency.SelectedItem.Text + "";
                    Target = "Sum(Q1)";
                    TargetYDT = "Sum(Q1)";
                }
                else
                {

                    Hearderttt = "Target-" + ddlFrequency.SelectedItem.Text + "";
                    Hearderkk = "" + ddlFrequency.SelectedItem.Text + "";
                    ACH = " Achv -" + ddlFrequency.SelectedItem.Text + "";
                    Target = "Sum(Q2)";
                    TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
                }
            }
            if (lblFrequency.Text == "3")
            {

                Hearderttt = "Target-Yearly";
                Hearderkk = "Yearly";
                ACH = " Achv-Yearly";
                Target = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
                TargetYDT = "Sum(Q1)+Sum(Q2)+Sum(Q3)+Sum(Q4)";
            }
            string hhh = "";
            string TTT = "";
            if (Convert.ToInt32(ddInGeography.SelectedValue) == 1)
            {
            }
            if (Convert.ToInt32(ddInGeography.SelectedValue) == 2)
            {
                TTT = "District:" + lblDistrict1.Text + "";
            }
            if (Convert.ToInt32(ddInGeography.SelectedValue) == 3)
            {
                TTT = "Block:" + lblBlock1.Text + "";
            }
            if (lblFrequency.Text == "1")
            {
                hhh = "Quarterly";
            }
            if (lblFrequency.Text == "2")
            {
                hhh = "Half Yearly";
            }
            if (lblFrequency.Text == "3")
            {
                hhh = "Yearly";
            }
            DataTable dt = Session["Enroll123"] as DataTable;



            if (dt.Rows.Count > 0)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                string Fullfilename = "" + FIleName + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");

                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                HttpContext.Current.Response.Write("<table  >");
                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<td colspan='6' ' style='text-align:Center;border:.3pt solid windowtext;font-weight:700; font-width:bold;'>Donor Report :" + ddlDonor.SelectedItem.Text + " </td>");

                HttpContext.Current.Response.Write("</tr>");
                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<td colspan='3' ' style='text-align:Left;border:.3pt solid windowtext;font-width:bold;font-weight:700;'>Reporting Start Month and Year  :" + lblTarget.Text + " </td>");
                HttpContext.Current.Response.Write("<td colspan='3' ' style='text-align:Left;border:.3pt solid windowtext;font-width:bold;font-weight:700;'> Project Period:" + lblPri.Text + " </td>");

                HttpContext.Current.Response.Write("</tr>");

                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<td colspan='3' ' style='text-align:Left;border:.3pt solid windowtext;font-width:bold;font-weight:700;'>Reporting Frequency: " + hhh + " </td>");

                HttpContext.Current.Response.Write("<td colspan='3' ' style='text-align:Left;border:.3pt solid windowtext;font-width:bold;font-weight:700;'>" + hhh + " : " + Hearderkk + " </td>");

                HttpContext.Current.Response.Write("</tr>");

                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");

                HttpContext.Current.Response.Write("<td colspan='2' ' style='text-align:Left;border:.3pt solid windowtext;font-width:bold;font-weight:700;'>Geography: " + TTT + " </td>");
                HttpContext.Current.Response.Write("<td colspan='2' ' style='text-align:Left;border:.3pt solid windowtext;font-width:bold;font-weight:700;'>Geography Type: " + lblGType.Text + " </td>");
                HttpContext.Current.Response.Write("<td colspan='2' ' style='text-align:Left;border:.3pt solid windowtext;font-width:bold;font-weight:700;'>Geography Level: " + ddInGeography.SelectedItem.Text + " </td>");

                HttpContext.Current.Response.Write("</tr>");


                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";


                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Reporting Outcome</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Reporting Indicator</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>" + Hearderttt + "</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>Target-YTD</th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> " + ACH + " </th>");
                HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> Achv-YTD</th>");


                HttpContext.Current.Response.Write("</tr>");



                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";







                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    HttpContext.Current.Response.Write("<tr>");
                    //HttpContext.Current.Response.Write("<td >Direct</td>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {


                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");


                    }
                }



                HttpContext.Current.Response.Write("</tr>");





                //HttpContext.Current.Response.Write("<tr>");
                //HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
                //HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
                //HttpContext.Current.Response.Write("</tr>");


                HttpContext.Current.Response.Write("</table>");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }
        catch
        {
            throw;
        }


    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ddlStartYear.SelectedValue) >= 2024)
            {
                GenerateExcelNewOut("DonorReport");
            }
            else
            {
                GenerateExcelNewOut2023("DonorReport");
            }

        }
        catch (Exception)
        {

            throw;
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }
    protected void GV_DynamicGrid1_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_DynamicGrid.PageIndex = e.NewPageIndex;
        if (Session["Annual"] != null)
        {

            DataTable Dt = Session["Annual"] as DataTable;
            GV_DynamicGrid.DataSource = Dt;
            GV_DynamicGrid.DataBind();
        }
    }



    private void ExportToCSVFile(DataTable dtTable, string filePath)
    {
        if (dtTable != null)
        {
            StringBuilder sbldr = new StringBuilder();
            if (dtTable.Columns.Count != 0)
            {
                foreach (DataColumn col in dtTable.Columns)
                {
                    sbldr.Append(col.ColumnName + ',');
                }
                sbldr.Append("\r\n");
                foreach (DataRow row in dtTable.Rows)
                {
                    foreach (DataColumn column in dtTable.Columns)
                    {

                        sbldr.Append(Convert.ToString(row[column]).Replace(",", "  ").Replace("\r", "").Replace("\n", "") + ',');
                    }
                    sbldr.Append("\r\n");

                }
            }
            string sFileDir = Server.MapPath(Comman.GetImagePath("DataBackupPath"));
            string Fullfilename = "" + filePath + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".csv";
            string path = sFileDir + Fullfilename;
            File.WriteAllText(path, sbldr.ToString());

            FileStream fs = null;//, fs2=null;
            try
            {
                string path1 = Fullfilename;
                string foldername = Server.MapPath(Comman.GetImagePath("DataBackupPath") + path1 + "");
                string datafolder = path1.Substring(0, path1.Length - 4);
                //  string[] file = Directory.GetFiles(foldername);

                string fullPath = Request.MapPath(Comman.GetImagePath("DataBackupPath") + datafolder + "" + ".zip");
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(foldername, "");
                    //    zip.AddFiles(file, foldername);
                    zip.Save(Server.MapPath(Comman.GetImagePath("DataBackupPath") + datafolder + "" + ".zip"));
                }



                HttpResponse Response = HttpContext.Current.Response; Response.Clear(); Response.ClearHeaders(); Response.Charset = "UTF-8";
                fs = File.Open(fullPath, FileMode.Open);
                byte[] bytBytes = new byte[(fs.Length)];
                fs.Read(bytBytes, 0, (int)fs.Length);
                fs.Close();
                Response.AddHeader("Content-disposition", "attachment; filename=" + datafolder + "" + ".zip");
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(bytBytes);






                if (File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                if (File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                Response.Flush();
                Response.End();
            }

            catch (System.Exception ex)
            {
                Response.Clear();

                //string mmsg = ex.Message;
                //showEXPMessages("(crateZip)  " + mmsg); //showMessages(mmsg);
            }
            finally
            {
                fs.Dispose();
                Response.Clear();

            }

            //str.Write(sbldr.ToString());
            //Response.ContentType = "Application/x-msexcel";
            //Response.AddHeader("content-disposition", "attachment;filename=test.csv");
            //Response.Write(sbldr.ToString());
            //Response.End();
        }
    }




    private void ExportGridToExcel(GridView Gv, string FileName)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        FileName = "" + FileName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        Gv.GridLines = GridLines.Both;
        Gv.HeaderStyle.Font.Bold = true;
        Gv.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();

    }


    private void ExporttoExcel(DataTable table, string FileName)
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
                HttpContext.Current.Response.Write(table.Columns[j].ColumnName);
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
    #region Amit 20200515
    private void DisplayDataOnPopup(DataTable dt, string name)
    {
        if (dt.Rows.Count > 0)
        {
            lblMsg.Text = name;
            Session["GridViewData"] = dt;
            Session["Name"] = name;
            PopUpGrid.DataSource = dt;
            PopUpGrid.DataBind();
            MpexdrPopUp.Show();
        }
        else
        {
            PopUpGrid.DataSource = null;
            PopUpGrid.DataBind();
        }
    }
    protected void PopUpGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        PopUpGrid.PageIndex = e.NewPageIndex;
        if (Session["GridViewData"] != null)
        {
            DataTable dt = Session["GridViewData"] as DataTable;
            PopUpGrid.DataSource = dt;
            PopUpGrid.DataBind();
            MpexdrPopUp.Show();
        }


    }
    protected void lnkDownload_OnClick(object sender, EventArgs e)
    {
        try
        {
            DataTable dTExcel = Session["GridViewData"] as DataTable;
            ExporttoExcel(dTExcel, Convert.ToString(Session["Name"]));
        }
        catch (Exception)
        {

            throw;
        }
    }
    #endregion
}