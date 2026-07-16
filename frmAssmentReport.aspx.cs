using ClosedXML.Excel;
using Ionic.Zip;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class frmAssmentReport : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {




                if (!IsPostBack)
                {

                    FillDropdown();



                    LoadYear();
                    LoadGroup();
                    //  objComman.BindDLL("mstlookup", "LookupCode,Description1 ", "LookupFlag='G'", "Description1", "Desc", ddlGender, "Description1", "LookupCode", "--All--");
                    LoadUserLeavel();
                    LoadUserLevel();
                    ViewState["1"] = "ss";
                    ViewState["Annual"] = "";
                    ViewState["D2dUser"] = "";

                }
                // btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");LinkButton8
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
    private void FillDropdown()
    {
        DataTable dt1 = Exec_Procedure("USP_GetLevel");
        ddlLevel.DataSource = dt1;
        ddlLevel.DataValueField = "id";
        ddlLevel.DataTextField = "Value";
        ddlLevel.DataBind();
        ddlLevel.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" --Select Level-- ", "0"));


    }
    protected void ddlLearning_SelectedIndexChanged(object sender, EventArgs e)
    {
        int FormLevel = Int32.Parse(ddlLevel.SelectedValue.ToString());

        LoadOutComeSpicify();
        FillFormNameNew();


    }
    public void LoadUserLevel()
    {
        if (Session["user_level_Role"].ToString() == "4")
        {
            //  ddlGroup.SelectedValue = "3";
            // ddlGroup.Enabled = false;

        }
        else if (Session["user_level_Role"].ToString() == "3")
        {
            //  ddlGroup.SelectedValue = "2";
            //  ddlGroup.Enabled = true;

        }
        else
        {
            // ddlGroup.SelectedValue = "1";
            //ddlGroup.Enabled = true;
        }

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
    public void LoadGroup()
    {
        string conditions = "";
        DataRow dr;
        if (Convert.ToString(Session["user_level"]) == "19")
        {
            DataTable dtYear = CreateDataTable();

            //dr = dtYear.NewRow();
            //dr["Type"] = "--Select--";
            //dr["ID"] = 0;
            //dtYear.Rows.Add(dr);

            dr = dtYear.NewRow();
            dr["Type"] = "Block Wise";
            dr["ID"] = 2;
            dtYear.Rows.Add(dr);

            dr = dtYear.NewRow();
            dr["Type"] = "Cluster Wise";
            dr["ID"] = 3;
            dtYear.Rows.Add(dr);
            //      objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlGroup, "Type", "ID", "Select");


        }
        else
        {
            DataTable dtYear = CreateDataTable();

            //dr = dtYear.NewRow();
            //dr["Type"] = "--Select--";
            //dr["ID"] = 0;
            //dtYear.Rows.Add(dr);

            dr = dtYear.NewRow();
            dr["Type"] = "District Wise";
            dr["ID"] = 1;
            dtYear.Rows.Add(dr);
            dr = dtYear.NewRow();
            dr["Type"] = "Block Wise";
            dr["ID"] = 2;
            dtYear.Rows.Add(dr);

            dr = dtYear.NewRow();
            dr["Type"] = "Cluster Wise";
            dr["ID"] = 3;
            dtYear.Rows.Add(dr);
            // objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlGroup, "Type", "ID", "Select");

        }
    }
    public void LoadYear()
    {
        string conditions = "";
        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


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
    public void AlllStateCode()
    {

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
            ChkState.DataSource = dtAllState;
            ChkState.DataTextField = "StateName";
            ChkState.DataValueField = "StateCode";
            ChkState.DataBind();
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
            ChkState.DataSource = dtAllState;
            ChkState.DataTextField = "StateName";
            ChkState.DataValueField = "StateCode";
            ChkState.DataBind();
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
            ChkState.DataSource = dtAllState;
            ChkState.DataTextField = "StateName";
            ChkState.DataValueField = "StateCode";
            ChkState.DataBind();

            foreach (ListItem item in ChkState.Items)
            {

                item.Selected = true;

            }

        }

    }
    public void LoadUserLeavel()
    {
        string conditions = "";
        AlllStateCode();
        if (Session["user_level_Role"].ToString() == "1")
        {

            //string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State   order by StateName   ";
            //DataTable dtState = objMain.LoadData(strQry1);
            //ChkState.DataSource = dtState;
            //ChkState.DataTextField = "StateName";
            //ChkState.DataValueField = "StateCode";
            //ChkState.DataBind();

            ChkState.Enabled = true;
            chkDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            //conditions = "UserName='" + Session["username"].ToString() + "' ";
            //string strQry1 = "  SELECT distinct mst1State.StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode inner join mst1State on mst1State.StateCode=mst2District.StateCode where   " + conditions + "  order by StateName   ";
            //DataTable dtState = objMain.LoadData(strQry1);
            //ChkState.DataSource = dtState;
            //ChkState.DataTextField = "StateName";
            //ChkState.DataValueField = "StateCode";
            //ChkState.DataBind();
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            foreach (ListItem item in ChkState.Items)
            {

                item.Selected = true;

            }

            ChkState.Enabled = true;
            chkDistrict.Enabled = true;
        }
        else
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            ////objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            //string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State  where   " + conditions + "  order by StateName   ";
            //DataTable dtState = objMain.LoadData(strQry1);
            //ChkState.DataSource = dtState;
            //ChkState.DataTextField = "StateName";
            //ChkState.DataValueField = "StateCode";
            //ChkState.DataBind();
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            foreach (ListItem item in ChkState.Items)
            {

                item.Selected = true;

            }
            // ChkState.SelectedIndex = 1;
            ChkState.Enabled = false;
            chkDistrict.Enabled = false;
        }


        if (Session["user_level_Role"].ToString() == "1")
        {
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            string ddlState = "";

            foreach (ListItem item in ChkState.Items)
            {
                if (item.Selected)
                {

                    ddlState += "'" + item.Value + "'" + ",";


                }
            }
            conditions = "";
            //  conditions = "StateCode in(" + ddlState + ") and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
            conditions = "UserName='" + Session["username"].ToString() + "' ";
            string strQry1 = "  SELECT distinct mst2District.DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode  where   " + conditions + " and Fyear='" + ddlYear.SelectedItem.Text + "'  order by DistrictName   ";


            // string strQry1 = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where " + conditions + "  order by DistrictName   ";


            DataTable dtDistrict = objMain.LoadData(strQry1);
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            chkDistrict.DataSource = dtDistrict;
            chkDistrict.DataTextField = "DistrictName";
            chkDistrict.DataValueField = "DistrictCode";
            chkDistrict.DataBind();

            if (Session["user_level_Role"].ToString() == "2")
            {
                foreach (ListItem item in chkDistrict.Items)
                {

                    item.Selected = true;

                }
                ddlDistrict_SelectedIndexChanged(ddlState, null);
            }
            //foreach (ListItem item in chkDistrict.Items)
            //{

            //    item.Selected = true;
            //    break;
            //}
            //ddlDistrict.SelectedIndex = 0;

            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }

        else
        {

            string ddlState = "";

            foreach (ListItem item in ChkState.Items)
            {
                if (item.Selected)
                {

                    ddlState += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlState.Length > 0)
            {
                ddlState = ddlState.Substring(0, ddlState.LastIndexOf(","));
            }
            conditions = "";
            conditions = "StateCode in(" + ddlState + ") and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and Fyear= '" + ddlYear.SelectedItem.Text + "' ";
            //  objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");
            string strQry1 = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where " + conditions + "  order by DistrictName   ";
            DataTable dtDistrict = objMain.LoadData(strQry1);
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            chkDistrict.DataSource = dtDistrict;
            chkDistrict.DataTextField = "DistrictName";
            chkDistrict.DataValueField = "DistrictCode";
            chkDistrict.DataBind();
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


            //ddlDistrict.SelectedIndex = 1;
            foreach (ListItem item in chkDistrict.Items)
            {

                item.Selected = true;

            }
            ddlDistrict_SelectedIndexChanged(chkDistrict, null);
            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }





    }
    public void FillCBState()
    {
        string conditions = "";
        // objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");


        //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
        string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State   order by StateName   ";
        DataTable dtState = objMain.LoadData(strQry1);
        ChkState.DataSource = dtState;
        ChkState.DataTextField = "StateName";
        ChkState.DataValueField = "StateCode";
        ChkState.DataBind();

    }
    public void FillCBDist()
    {
        string conditions = "";
        string ddlState = "";
        DataTable dtDistrict = null;
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlState += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlState.Length > 0)
        {
            ddlState = ddlState.Substring(0, ddlState.LastIndexOf(","));
        }
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "StateCode in(" + ddlState + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = " mst2District.StateCode in(" + ddlState + ") and UserName='" + Session["username"].ToString() + "' ";
        }
        else
        {
            conditions = "StateCode  in(" + ddlState + ") and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";


        }
        string conditions1 = "StateCode in(" + ddlState + ") ";
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
            string strQry = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where " + conditions + "  union select  DistrictCode,  dbo.TitleCase(upper(DistrictName))  +' ('+ 'Spine' +')'  as  DistrictName from mstSpineDistrict  where  " + conditions1 + "   order by DistrictName   ";
            dtDistrict = objMain.LoadData(strQry);
        }

        // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        chkDistrict.DataSource = dtDistrict;
        chkDistrict.DataTextField = "DistrictName";
        chkDistrict.DataValueField = "DistrictCode";
        chkDistrict.DataBind();

        if (Session["user_level_Role"].ToString() == "2")
        {
            foreach (ListItem item in chkDistrict.Items)
            {

                item.Selected = true;

            }
        }

    }
    public void LoadOutCome()
    {
        string conditions = "  ActiveStatus=1";

        objComman.BindDLL("mstOutcome", "OutcomeID,OutcomeName ", conditions, "OutcomeName", "asc", ddlLearning, "OutcomeName", "OutcomeID", "--Select--");

        ddlLearning.SelectedIndex = 0;


    }
    public void Filllearning()
    {
        string conditions = "  ISNULL(TrainingStatus,0)=1 ";
        objComman.BindDLL("mstlearning", "learningID,dbo.TitleCase(upper(learningName)) as learningName ", conditions, "learningName", "asc", ddlTraingOutcome, "learningName", "learningID", "--Select--");

    }

    public void LoadOutComeSpicify()
    {
        string conditions = " ";

        objComman.BindDLL("mstOutcomeSpecific", "sOutcomeID,sOutcomeName ", "OutcomeID=" + ddlLearning.SelectedValue + " and ActiveStatus=1", "sOutcomeID", "asc", ddlTraingOutcome, "sOutcomeName", "sOutcomeID", "--Select--");

        ddlTraingOutcome.SelectedIndex = 0;


    }
    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        int FormLevel = Int32.Parse(ddlLevel.SelectedValue.ToString());
        //   d2.Text = "";
        d1.Visible = false;
        d2.Visible = false;
        ddlLearning.Visible = false;
        ddlTraingOutcome.Visible = false;
        //   LnkEntry.Visible = false;

        if (FormLevel == 1)
        {
            d1.Visible = true;
            d2.Visible = true;
            ddlLearning.Visible = true;
            ddlTraingOutcome.Visible = true;

            LoadOutCome();
            d2.InnerText = "Specific Training :";
            Filllearning();
            LoadOutComeSpicify();


        }
        if (FormLevel == 2)
        {
            d2.InnerText = "Training OutCome :";

            d1.Visible = false;
            d2.Visible = true;

            ddlLearning.Visible = false;
            ddlTraingOutcome.Visible = true;


            Filllearning();
        }
        if (FormLevel == 3)
        {

            d1.Visible = false;
            d2.Visible = false;
            ddlLearning.Visible = false;
            ddlTraingOutcome.Visible = false;

        }
        if (FormLevel == 4)
        {
            d1.Visible = false;
            d2.Visible = false;
            ddlLearning.Visible = false;
            ddlTraingOutcome.Visible = false;
        }
        FillFormNameNew();

    }
    protected void ddlTraingOutcome_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillFormNameNew();
    }
    public void FillFormNameNew()
    {
        string conditions = "";
        string conditions4 = "";
        string dist = "";
        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
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


            }
        }
        int FormLevel = Int32.Parse(ddlLevel.SelectedValue.ToString());
        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        if (FormLevel == 1 || FormLevel == 2)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "  and   mst2DistrictStaff.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and mst2DistrictStaff.StateCode in(" + ddlStatecode + ") ";

        }

        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst2DistrictStaff.DistrictCode in(" + ddlDistrict + ") ";

        }


        conditions += "  and  AssessmentFor =" + FormLevel + " ";

        if ((FormLevel == 1 || FormLevel == 2) && ddlLearning.SelectedIndex > 0)
        {
            conditions += "  and TrainingOutCome =" + ddlLearning.SelectedValue + " ";
        }

        if (FormLevel == 1 && ddlTraingOutcome.SelectedIndex > 0)
        {
            conditions += "  and SpecificTraining =" + ddlTraingOutcome.SelectedValue + " ";

        }
        if ((FormLevel == 1 || FormLevel == 2) && ddlAssessmentType.SelectedIndex > 0)
        {
            conditions += "  and AssessmentType =" + ddlAssessmentType.SelectedValue + " ";
        }
        if (ddlYear.SelectedIndex > 0)
        {
            string Year = ddlYear.SelectedItem.Text;
            string[] Year1 = Year.Split('-');
            conditions += "    And FromDate >= '" + Year1[0] + "-04-01' and ToDate<='" + Year1[1] + "-03-31'";

            conditions4 = "    And FromDate >= '" + Year1[0] + "-04-01' and ToDate<='" + Year1[1] + "-03-31'";
        }

        string UserID = Session["UserID"].ToString();
        DataTable dt = new DataTable();
        //int FormLevel;

        dt = Get_DataFor3Filter("USP_GetSurveyChange20232024New", conditions, FormLevel.ToString(), conditions4);
        //dt = objBLL.Select_All_Data("MSTForm", "FormID,FormName", "IsDeleted = 0 and FormLevel = " + FormLevel  + " ", "", "");



        ddlForm.DataSource = dt;
        ddlForm.DataTextField = "FormName";
        ddlForm.DataValueField = "FormID";
        ddlForm.DataBind();
        ddlForm.Items.Insert(0, new System.Web.UI.WebControls.ListItem("------Select-------", "0"));



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
        string conditions = "";
        if (ddlYear.SelectedIndex > 0)
        {
            AlllStateCode();
            if (Session["user_level_Role"].ToString() == "3" || Session["user_level_Role"].ToString() == "4")
            {

            }
            else
            {
                foreach (ListItem item in ChkState.Items)
                {

                    item.Selected = false;

                }
            }
            if (Session["user_level_Role"].ToString() == "2")
            {

                //conditions = "UserName='" + Session["username"].ToString() + "' ";
                //string strQry1 = "  SELECT distinct mst1State.StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode inner join mst1State on mst1State.StateCode=mst2District.StateCode where   " + conditions + "  order by StateName   ";
                //DataTable dtState = objMain.LoadData(strQry1);
                //ChkState.DataSource = dtState;
                //ChkState.DataTextField = "StateName";
                //ChkState.DataValueField = "StateCode";
                //ChkState.DataBind();
            }
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            foreach (ListItem item in ChkState.Items)
            {

                item.Selected = true;

            }

            ddlState_SelectedIndexChanged(chkDistrict, null);
            if (Session["user_level_Role"].ToString() == "3" || Session["user_level_Role"].ToString() == "4")
            {
                if (chkDistrict.Items.Count > 0)
                {
                    foreach (ListItem item in chkDistrict.Items)
                    {

                        item.Selected = true;

                    }
                }
            }

            ddlDistrict_SelectedIndexChanged(chkDistrict, null);


        }
        else
        {
            foreach (ListItem item in ChkState.Items)
            {

                item.Selected = false;

            }
            chkDistrict.Items.Clear();

        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {

        chkDistrict.Items.Clear();

        FillCBDist();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBBock();

    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBCluster();
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillage();
    }
    public void FillCBBock()
    {
        string conditions = "";
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

        if (Session["user_level_Role"].ToString() == "2")
        {
            if (ddlDistrict.Length > 0)
            {
            }
            else
            {
                if (chkDistrict.Items.Count > 0)
                {
                    foreach (ListItem item in chkDistrict.Items)
                    {
                        ddlDistrict += "'" + item.Value + "'" + ",";
                        item.Selected = true;
                        break;
                    }
                    if (ddlDistrict.Length > 0)
                    {
                        ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
                    }
                }
            }


        }
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "DistrictCode in(" + ddlDistrict + ") ";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "DistrictCode in(" + ddlDistrict + ")  ";
        }
        else if (Session["user_level_Role"].ToString() == "4")
        {
            conditions = "DistrictCode in(" + ddlDistrict + ")   and BlockCode in( " + Session["BlockCode"].ToString() + " ) and FYear ='" + ddlYear.SelectedItem.Text + "' ";

        }
        else
        {
            conditions = "DistrictCode in(" + ddlDistrict + ")  ";
        }
        //     objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        string strQry = "  SELECT BlockCode, dbo.TitleCase(upper(BlockName))  as BlockName FROM mst3Block where " + conditions + "  order by BlockName   ";
        DataTable dtDistrict = objMain.LoadData(strQry);
        // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        //chkBlock.DataSource = dtDistrict;
        //chkBlock.DataTextField = "BlockName";
        //chkBlock.DataValueField = "BlockCode";
        //chkBlock.DataBind();

        //if (Session["user_level_Role"].ToString() == "4")
        //{
        //    if (chkBlock.Items.Count > 0)
        //    {
        //        foreach (ListItem item in chkBlock.Items)
        //        {

        //            item.Selected = true;

        //        }
        //    }
        //    chkBlock.Enabled = false;
        //    ddlBlock_SelectedIndexChanged(ddlDistrict, null);
        //}


        //chkVillage.Items.Clear();

    }
    public void FillCBCluster()
    {
        string conditions = "";
        string ddlBlock = "";
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
        //foreach (ListItem item in chkBlock.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlBlock += "'" + item.Value + "'" + ",";


        //    }
        //}

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }
        conditions = "";
        DataTable dtDistrict = null;

        conditions = "DistrictCode in(" + ddlDistrict + ")  and BlockCode in(" + ddlBlock + ")";
        string strQry = "  SELECT ClusterCode, dbo.TitleCase(upper(ClusterName))  as ClusterName FROM mstcluster where " + conditions + "  order by ClusterName   ";
        dtDistrict = objMain.LoadData(strQry);



        // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        //ddlPanchayat.DataSource = dtDistrict;
        //ddlPanchayat.DataTextField = "ClusterName";
        //ddlPanchayat.DataValueField = "ClusterCode";
        //ddlPanchayat.DataBind();

        // objComman.BindDLL("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "--All--");


        chkVillage.Items.Clear();

    }
    public void FillCVillage()
    {
        string conditions = "";

        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";

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
        // foreach (ListItem item in chkBlock.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlBlock += "'" + item.Value + "'" + ",";


        //    }
        //}

        //if (ddlBlock.Length > 0)
        //{
        //    ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        //}

        //foreach (ListItem item in ddlPanchayat.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlPhan += "'" + item.Value + "'" + ",";


        //    }
        //}

        //if (ddlPhan.Length > 0)
        //{
        //    ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        //}
        conditions = "";

        conditions = "DistrictCode in(" + ddlDistrict + ")  and BlockCode in(" + ddlBlock + ") and  PanchayatCode in(" + ddlPhan + ")";

        //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        //objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--All-");

        string strQry = "  SELECT VillageCode, dbo.TitleCase(upper(VillageName))  as VillageName FROM mst5Village where " + conditions + "  order by VillageName   ";
        DataTable dtDistrict = objMain.LoadData(strQry);

        //chkVillage.DataSource = dtDistrict;
        //chkVillage.DataTextField = "VillageName";
        //chkVillage.DataValueField = "VillageCode";
        //chkVillage.DataBind();


    }

    protected void LnkCHild_OnClick(object sender, EventArgs e)
    {
        //ViewState["1"] = 8;
        //if (ddlGroup.SelectedIndex > 0)
        //{

        //    //LoadSchoolSummaryData(Convert.ToInt32(ddlTpye.SelectedValue));
        //    //GVChild.Visible = false;
        //    //GV_DynamicGrid.Visible = true;
        //    //GVChildTarget.Visible = false;
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Plan Type ')</script>", false);
        //}


    }
    protected void LnkChildSummary_OnClick(object sender, EventArgs e)
    {



    }
    protected void LnkDeatild_OnClick(object sender, EventArgs e)
    {
        Session["lnik"] = "1";

        LoadSummary();


    }

    protected void LnkDeatild1_OnClickAll(object sender, EventArgs e)
    {
        if (ddlLevel.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlLevel.SelectedValue) == 2)
            {
                LoadTotalAssment();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Showalert", "alert('Please select Assessment Category');", true);
        }





    }
    protected void LnkDeatild1_OnClickAll2(object sender, EventArgs e)
    {
        if (ddlLevel.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlLevel.SelectedValue) == 1 || Convert.ToInt32(ddlLevel.SelectedValue) == 3)
            {
                LoadTotalAssmentStaff();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Showalert", "alert('Please select Assessment Category');", true);
        }





    }
    public void LoadSummary()
    {
        string conditions = "";
        string conditions4 = "";
        string dist = "";
        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
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


            }
        }
        int FormLevel = Int32.Parse(ddlLevel.SelectedValue.ToString());
        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        if (FormLevel == 1 || FormLevel == 2)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "  and   mst2DistrictStaff.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and tbl_training_question.StateCode in(" + ddlStatecode + ") ";

        }
        if (Session["user_level_Role"].ToString() == "2")
        { }
        else
        {
            if (ddlDistrict.Length > 0)
            {
                conditions += " and mst2DistrictStaff.DistrictCode in(" + ddlDistrict + ") ";

            }
        }
        if (FormLevel > 0)
        {
            conditions += "  and  AssessmentFor =" + FormLevel + " ";
        }

        if ((FormLevel == 1 || FormLevel == 2) && ddlLearning.SelectedIndex > 0)
        {
            conditions += "  and TrainingOutCome =" + ddlLearning.SelectedValue + " ";
        }

        if (FormLevel == 1 && ddlTraingOutcome.SelectedIndex > 0)
        {
            conditions += "  and SpecificTraining =" + ddlTraingOutcome.SelectedValue + " ";

        }
        if ((FormLevel == 1 || FormLevel == 2) && ddlAssessmentType.SelectedIndex > 0)
        {
            conditions += "  and AssessmentType =" + ddlAssessmentType.SelectedValue + " ";
        }
        if (ddlForm.SelectedIndex > 0)
        {
            conditions += "  and Tarining_ID =" + ddlForm.SelectedValue + " ";
        }
        if (ddlYear.SelectedIndex > 0)
        {
            string Year = ddlYear.SelectedItem.Text;
            string[] Year1 = Year.Split('-');
            conditions += "    And FromDate >= '" + Year1[0] + "-04-01' and ToDate<='" + Year1[1] + "-03-31'";


        }
        DataTable dtHeader = Get_DataFor("rptSurveySummary20232024New", conditions);
        if (dtHeader.Rows.Count > 0)
        {
            GVChildTarget.DataSource = dtHeader;
            GVChildTarget.DataBind();
            Session["Summary"] = dtHeader;
        }
    }

    public void LoadTotalAssmentStaff()
    {
        string conditions = "";
        string conditions4 = "";
        string dist = "";
        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
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


            }
        }
        int FormLevel = Int32.Parse(ddlLevel.SelectedValue.ToString());
        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        if (FormLevel == 1 || FormLevel == 2)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "  and   mst2DistrictStaff.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and tbl_training_question.StateCode in(" + ddlStatecode + ") ";

        }
        if (Session["user_level_Role"].ToString() == "2")
        { }
        else
        {
            if (ddlDistrict.Length > 0)
            {
                conditions += " and mst2DistrictStaff.DistrictCode in(" + ddlDistrict + ") ";

            }
        }
        if (FormLevel > 0)
        {
            conditions += "  and  AssessmentFor =" + FormLevel + " ";
        }

        if ((FormLevel == 1 || FormLevel == 2) && ddlLearning.SelectedIndex > 0)
        {
            conditions += "  and TrainingOutCome =" + ddlLearning.SelectedValue + " ";
        }

        if (FormLevel == 1 && ddlTraingOutcome.SelectedIndex > 0)
        {
            conditions += "  and SpecificTraining =" + ddlTraingOutcome.SelectedValue + " ";

        }
        if ((FormLevel == 1 || FormLevel == 2) && ddlAssessmentType.SelectedIndex > 0)
        {
            conditions += "  and AssessmentType =" + ddlAssessmentType.SelectedValue + " ";
        }
        if (ddlForm.SelectedIndex > 0)
        {
            conditions += "  and Tarining_ID =" + ddlForm.SelectedValue + " ";
        }
        if (ddlYear.SelectedIndex > 0)
        {
            string Year = ddlYear.SelectedItem.Text;
            string[] Year1 = Year.Split('-');
            conditions += "    And FromDate >= '" + Year1[0] + "-04-01' and ToDate<='" + Year1[1] + "-03-31'";


        }
        DataTable dtHeader = null;

        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
        {

            dtHeader = Get_DataFor2FilterReport("rptSurverAssemenAllReport20262027New", conditions, "1");
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2025)
        {

            dtHeader = Get_DataFor2FilterReport("rptSurverAssemenAllReport20252026New", conditions, "1");
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2024)
        {
            dtHeader = Get_DataFor2FilterReport("rptSurverAssemenAllReport2024", conditions, "1");
        }
        else
        {
            dtHeader = Get_DataFor2FilterReport("rptSurverAssemenAllReport", conditions, "1");
        }

        if (dtHeader.Rows.Count > 0)
        {
            ExportReportAllStaff(dtHeader);
        }
    }
    public void LoadTotalAssment()
    {
        string conditions = "";
        string conditions4 = "";
        string dist = "";
        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
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


            }
        }
        int FormLevel = Int32.Parse(ddlLevel.SelectedValue.ToString());
        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        if (FormLevel == 1 || FormLevel == 2)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "  and   mst2DistrictStaff.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and tbl_training_question.StateCode in(" + ddlStatecode + ") ";

        }
        if (Session["user_level_Role"].ToString() == "2")
        { }
        else
        {
            if (ddlDistrict.Length > 0)
            {
                conditions += " and mst2DistrictStaff.DistrictCode in(" + ddlDistrict + ") ";

            }
        }
        if (FormLevel > 0)
        {
            conditions += "  and  AssessmentFor =" + FormLevel + " ";
        }

        if ((FormLevel == 1 || FormLevel == 2) && ddlLearning.SelectedIndex > 0)
        {
            conditions += "  and TrainingOutCome =" + ddlLearning.SelectedValue + " ";
        }

        if (FormLevel == 1 && ddlTraingOutcome.SelectedIndex > 0)
        {
            conditions += "  and SpecificTraining =" + ddlTraingOutcome.SelectedValue + " ";

        }
        if ((FormLevel == 1 || FormLevel == 2) && ddlAssessmentType.SelectedIndex > 0)
        {
            conditions += "  and AssessmentType =" + ddlAssessmentType.SelectedValue + " ";
        }
        if (ddlForm.SelectedIndex > 0)
        {
            conditions += "  and Tarining_ID =" + ddlForm.SelectedValue + " ";
        }
        if (ddlYear.SelectedIndex > 0)
        {
            string Year = ddlYear.SelectedItem.Text;
            string[] Year1 = Year.Split('-');
            conditions += "    And FromDate >= '" + Year1[0] + "-04-01' and ToDate<='" + Year1[1] + "-03-31'";


        }
        DataTable dtHeader = null;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
        {
            dtHeader = Get_DataFor2FilterReport("rptSurverAssemenAllReport20262027New", conditions, "2");
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2025)
        {
            dtHeader = Get_DataFor2FilterReport("rptSurverAssemenAllReport20252026New", conditions, "2");
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2024)
        {
            dtHeader = Get_DataFor2FilterReport("rptSurverAssemenAllReport2024", conditions, "2");
        }
        else
        {
            dtHeader = Get_DataFor2FilterReport("rptSurverAssemenAllReport", conditions, "2");
        }
        if (dtHeader.Rows.Count > 0)
        {
            ExportReportAll(dtHeader);
        }
    }
    public void ExportReportAllStaff(DataTable dtMain)
    {

        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\AssessmentEmp.xlsx");
        var ws = wb.Worksheet(1);


        for (int x = 0; x < dtMain.Columns.Count; x++)
        {

            ws.Cell(1, x + 1).Value = dtMain.Columns[x].ColumnName;
        }


        //dt1.Columns.Remove("rownNO");
        ws.Cell(2, 1).InsertData(dtMain.Rows);
        Int32 ii = Convert.ToInt32(dtMain.Rows.Count) + 2;
        string str = "A1:AT" + ii;

        //ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        //ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        //ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        //ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        filepath = StartupPath + "\\CompileoverallstaffAssessment  " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
        wb.SaveAs(filepath);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
        Response.WriteFile(filepath);

        Response.End();
        if (File.Exists(filepath))
        {
            System.IO.File.Delete(filepath);
        }
    }
    public void ExportReportAll(DataTable dtMain)
    {

        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\AssessmentEmp.xlsx");
        var ws = wb.Worksheet(1);


        for (int x = 0; x < dtMain.Columns.Count; x++)
        {

            ws.Cell(1, x + 1).Value = dtMain.Columns[x].ColumnName;
        }


        //dt1.Columns.Remove("rownNO");
        ws.Cell(2, 1).InsertData(dtMain.Rows);
        Int32 ii = Convert.ToInt32(dtMain.Rows.Count) + 2;
        string str = "A1:AT" + ii;

        //ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        //ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        //ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        //ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        filepath = StartupPath + "\\TeamBalikaAssessment " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
        wb.SaveAs(filepath);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
        Response.WriteFile(filepath);

        Response.End();
        if (File.Exists(filepath))
        {
            System.IO.File.Delete(filepath);
        }
    }

    protected void LnkDeatild1_OnClick(object sender, EventArgs e)
    {
        Session["lnik"] = "2";

        LoadSummaryLink();


    }
    public void LoadSummaryLink()
    {
        string conditions = "";
        string conditions4 = "";
        string dist = "";
        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
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


            }
        }
        int FormLevel = Int32.Parse(ddlLevel.SelectedValue.ToString());
        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        if (FormLevel == 1 || FormLevel == 2)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "  and   mst2DistrictStaff.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and tbl_training_question.StateCode in(" + ddlStatecode + ") ";

        }
        if (Session["user_level_Role"].ToString() == "2")
        { }
        else
        {
            if (ddlDistrict.Length > 0)
            {
                conditions += " and mst2DistrictStaff.DistrictCode in(" + ddlDistrict + ") ";

            }
        }
        if (FormLevel > 0)
        {
            conditions += "  and  AssessmentFor =" + FormLevel + " ";
        }

        if ((FormLevel == 1 || FormLevel == 2) && ddlLearning.SelectedIndex > 0)
        {
            conditions += "  and TrainingOutCome =" + ddlLearning.SelectedValue + " ";
        }

        if (FormLevel == 1 && ddlTraingOutcome.SelectedIndex > 0)
        {
            conditions += "  and SpecificTraining =" + ddlTraingOutcome.SelectedValue + " ";

        }
        if ((FormLevel == 1 || FormLevel == 2) && ddlAssessmentType.SelectedIndex > 0)
        {
            conditions += "  and AssessmentType =" + ddlAssessmentType.SelectedValue + " ";
        }
        if (ddlForm.SelectedIndex > 0)
        {
            conditions += "  and Tarining_ID =" + ddlForm.SelectedValue + " ";
        }
        if (ddlYear.SelectedIndex > 0)
        {
            string Year = ddlYear.SelectedItem.Text;
            string[] Year1 = Year.Split('-');
            conditions += "    And FromDate >= '" + Year1[0] + "-04-01' and ToDate<='" + Year1[1] + "-03-31'";


        }
        DataTable dtHeader = Get_DataFor("rptAssmentLinkRpoertNew", conditions);
        if (dtHeader.Rows.Count > 0)
        {
            GVChildTarget.DataSource = dtHeader;
            GVChildTarget.DataBind();
            Session["Summary"] = dtHeader;
        }
    }
    public DataTable Get_DataFor(string ProcedureName, string Filter1)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                            new SqlParameter("@Con",Filter1),


                    };
            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, ProcedureName, paramvT);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch (Exception)
        {

        }
        return dtcombo;
    }
    public DataTable Get_DataFor2FilterReport(string ProcedureName, string Filter1, string Filter2)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                            new SqlParameter("@FromID",Filter1),
                             new SqlParameter("@Flag",Filter2),

                    };
            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, ProcedureName, paramvT);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch
        {
            throw;
        }
        return dtcombo;
    }
    protected void LnkFillingSystem_OnClick(object sender, EventArgs e)
    {
        if (ddlForm.SelectedIndex > 0)
        {
            DataTable dtHeader = null;
            if (Convert.ToInt32(ddlYear.SelectedValue) == 2026)
            {
                dtHeader = Get_DataFor2FilterReport("rptSurvey202627", ddlForm.SelectedValue.ToString(), "1");
            }
            else if (Convert.ToInt32(ddlYear.SelectedValue) == 2025)
            {
                dtHeader = Get_DataFor2FilterReport("rptSurvey20252026", ddlForm.SelectedValue.ToString(), "1");
            }
            else if (Convert.ToInt32(ddlYear.SelectedValue) == 2024)
            {
                dtHeader = Get_DataFor2FilterReport("rptSurvey20242025", ddlForm.SelectedValue.ToString(), "1");
            }
            else
            {
                dtHeader = Get_DataFor2FilterReport("rptSurvey2024", ddlForm.SelectedValue.ToString(), "1");
            }

            GVChildTarget.DataSource = null;
            GVChildTarget.DataBind();
            Session["dtHeader1"] = dtHeader;
            ExportReportDetails();
            // exportTABLE_COMPLETE(dtHeader);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Showalert", "alert('Please select Assessment Name');", true);
        }


    }
    public void ExportReportDetails()
    {

        DataTable dtMain = Session["dtHeader1"] as DataTable;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\Assessment.xlsx");
        var ws = wb.Worksheet(1);

        for (int x = 0; x < dtMain.Columns.Count; x++)
        {

            ws.Cell(1, x + 1).Value = dtMain.Columns[x].ColumnName;
        }


        //dt1.Columns.Remove("rownNO");
        ws.Cell(2, 1).InsertData(dtMain.Rows);
        Int32 ii = Convert.ToInt32(dtMain.Rows.Count) + 2;
        string str = "A1:AT" + ii;

        //ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        //ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        //ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        //ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        filepath = StartupPath + "\\ResponseRawData" + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
        wb.SaveAs(filepath);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
        Response.WriteFile(filepath);

        Response.End();
        if (File.Exists(filepath))
        {
            System.IO.File.Delete(filepath);
        }
    }
    protected void LnkEX_OnClick(object sender, EventArgs e)
    {
        if (ddlForm.SelectedIndex > 0)
        {
            DataTable dtHeader = null;
            if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
            {
                dtHeader = Get_DataFor2FilterReport("rptSurver2026", ddlForm.SelectedValue.ToString(), "1");
            }
            else if (Convert.ToInt32(ddlYear.SelectedValue) == 2025)
            {
                dtHeader = Get_DataFor2FilterReport("rptSurver2025", ddlForm.SelectedValue.ToString(), "1");
            }
            else if (Convert.ToInt32(ddlYear.SelectedValue) == 2024)
            {
                dtHeader = Get_DataFor2FilterReport("rptSurverEMpScoreNew2024", ddlForm.SelectedValue.ToString(), "1");
            }
            else
            {
                dtHeader = Get_DataFor2FilterReport("rptSurverEMpScoreNew2023", ddlForm.SelectedValue.ToString(), "1");
            }
            Session["dtHeader"] = dtHeader;
            GVChildTarget.DataSource = null;
            GVChildTarget.DataBind();
            ExportReport();
            // exportTABLE_COMPLETESchor(dtHeader);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Showalert", "alert('Please select Assessment Name');", true);
        }



    }
    public void ExportReport()
    {

        DataTable dtMain = Session["dtHeader"] as DataTable;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\AssessmentEmp.xlsx");
        var ws = wb.Worksheet(1);

        DataTable dtSurveyQTotal = null;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2024)
        {
            dtSurveyQTotal = Get_DataFor2FilterReport("rptSurveyQTotal2024", ddlForm.SelectedValue.ToString(), "1");
        }
        else
        {
            dtSurveyQTotal = Get_DataFor2FilterReport("rptSurveyQTotal", ddlForm.SelectedValue.ToString(), "1");
        }

        int FScore = 0;
        if (dtSurveyQTotal.Rows.Count > 0)
        {
            FScore = Convert.ToInt32(dtSurveyQTotal.Rows[0]["Score"]);
        }
        int dd = 0;
        for (int j = 0; j < dtMain.Rows.Count; j++)
        {
            for (int i = 28; i < dtMain.Columns.Count; i++)
            {
                if (dtMain.Columns[i].ColumnName == "Total_Question")
                {

                }
                else
                {
                    if (Convert.ToString(dtMain.Rows[j][i].ToString()) == "")
                    {

                    }
                    else
                    {
                        dd = dd + Convert.ToInt32(dtMain.Rows[j][i].ToString());
                    }
                    //if (dtMain.Columns[i].ColumnName == "Total_Answer")
                    //{
                    //    dtMain.Rows[j].SetField("Total_Answer", Convert.ToString(dd));
                    //    dd = 0;
                    //}
                }

            }
            dtMain.Rows[j]["Total Achieved Score"] = dd.ToString();
            dd = 0;
            dtMain.AcceptChanges();
        }
        for (int hh = 0; hh < dtMain.Rows.Count; hh++)
        {
            dtMain.Rows[hh]["Total Max Score"] = FScore.ToString();
            decimal Toald = Convert.ToDecimal(dtMain.Rows[hh]["Total Achieved Score"]);
            decimal ToaldScore = (Convert.ToDecimal(dtMain.Rows[hh]["Total Achieved Score"]) / FScore);

            decimal ToaldScore1 = (Convert.ToDecimal(dtMain.Rows[hh]["Total Achieved Score"]) / FScore) * 100;
            dtMain.Rows[hh]["%Score"] = ToaldScore;
            if (Convert.ToString(dtMain.Rows[hh]["Assessment Category"]) == "Team Balika Training")
            {

                if (Math.Round(ToaldScore1) >= 85)
                {
                    dtMain.Rows[hh]["Assessment Result"] = "Expert";
                }
                if (Math.Round(ToaldScore1) >= 70 && Math.Round(ToaldScore1) < 85)
                {
                    dtMain.Rows[hh]["Assessment Result"] = "Master";
                }
                if (Math.Round(ToaldScore1) < 70)
                {
                    dtMain.Rows[hh]["Assessment Result"] = "Beginner";

                }
            }
            else
            {

                if (Convert.ToString(dtMain.Rows[hh]["Specific Training"]) == "Core group training on GKP++" | Convert.ToString(dtMain.Rows[hh]["Specific Training"]) == "CG/MT Training GKP - UTSAV" || Convert.ToString(dtMain.Rows[hh]["Specific Training"]) == "CG/MT Training GKP - PRAVAH" || Convert.ToString(dtMain.Rows[hh]["Specific Training"]) == "CG/MT Training GKP - BODH" || Convert.ToString(dtMain.Rows[hh]["Specific Training"]) == "CG/MT Training GKP SRIJAN-PRAVAH" || Convert.ToString(dtMain.Rows[hh]["Specific Training"]) == "CG/MT Training GKP SRIJAN-BODH" || Convert.ToString(dtMain.Rows[hh]["Specific Training"]) == "CG/MT Training GKP - SRIJAN" || Convert.ToString(dtMain.Rows[hh]["Specific Training"]) == "CG/MT Training on Enrollment" || Convert.ToString(dtMain.Rows[hh]["Specific Training"]) == "CG CV/SC Training" || Convert.ToString(dtMain.Rows[hh]["Specific Training"]) == "ERL -CG" || Convert.ToString(dtMain.Rows[hh]["Specific Training"]) == "CG/MT Training GKP L0-L1" || Convert.ToString(dtMain.Rows[hh]["Specific Training"]) == "CG/MT Training GKP L0-L2" || Convert.ToString(dtMain.Rows[hh]["Specific Training"]) == "CG/MT Training GKP L3" || Convert.ToString(dtMain.Rows[hh]["Specific Training"]) == "CG/MT Training GKP L2" || Convert.ToString(dtMain.Rows[hh]["Specific Training"]) == "CG/MT Training GKP L1" || Convert.ToString(dtMain.Rows[hh]["Specific Training"]) == "CG/MT training on Balsabha & LSE" || Convert.ToString(dtMain.Rows[hh]["Specific Training"]) == "Staff Training for PMS MT")
                {
                    if (Math.Round(ToaldScore1) >= 100)
                    {
                        dtMain.Rows[hh]["Assessment Result"] = "Expert";
                    }
                    if (Math.Round(ToaldScore1) >= 90 && Math.Round(ToaldScore1) <= 99)
                    {
                        dtMain.Rows[hh]["Assessment Result"] = "Master";
                    }
                    if (Math.Round(ToaldScore1) < 90)
                    {
                        dtMain.Rows[hh]["Assessment Result"] = "Beginner";

                    }
                }
                else
                {


                    if (Math.Round(ToaldScore1) >= 90)
                    {
                        dtMain.Rows[hh]["Assessment Result"] = "Expert";
                    }
                    if (Math.Round(ToaldScore1) >= 80 && Math.Round(ToaldScore1) < 90)
                    {
                        dtMain.Rows[hh]["Assessment Result"] = "Master";
                    }
                    if (Math.Round(ToaldScore1) < 80)
                    {
                        dtMain.Rows[hh]["Assessment Result"] = "Beginner";

                    }
                }
            }
        }
        for (int x = 0; x < dtMain.Columns.Count; x++)
        {

            ws.Cell(1, x + 1).Value = dtMain.Columns[x].ColumnName;
        }


        //dt1.Columns.Remove("rownNO");
        ws.Cell(2, 1).InsertData(dtMain.Rows);
        Int32 ii = Convert.ToInt32(dtMain.Rows.Count) + 2;
        string str = "A1:AT" + ii;

        //ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        //ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        //ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        //ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        filepath = StartupPath + "\\EmpResponseScore " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
        wb.SaveAs(filepath);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
        Response.WriteFile(filepath);

        Response.End();
        if (File.Exists(filepath))
        {
            System.IO.File.Delete(filepath);
        }
    }

    protected void Lnkpf_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 500;
        string conditions = "";
        string conditions4 = "";
        string dist = "";
        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
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


            }
        }
        int FormLevel = Int32.Parse(ddlLevel.SelectedValue.ToString());
        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        if (FormLevel == 1 || FormLevel == 2)
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "  and   mst2DistrictStaff.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and tbl_training_question.StateCode in(" + ddlStatecode + ") ";

        }
        if (Session["user_level_Role"].ToString() == "2")
        { }

        else
        {
            if (ddlDistrict.Length > 0)
            {
                conditions += " and mst2DistrictStaff.DistrictCode in(" + ddlDistrict + ") ";

            }
        }
        if (FormLevel > 0)
        {
            conditions += "  and  AssessmentFor =" + FormLevel + " ";
        }

        if ((FormLevel == 1 || FormLevel == 2) && ddlLearning.SelectedIndex > 0)
        {
            conditions += "  and TrainingOutCome =" + ddlLearning.SelectedValue + " ";
        }

        if (FormLevel == 1 && ddlTraingOutcome.SelectedIndex > 0)
        {
            conditions += "  and SpecificTraining =" + ddlTraingOutcome.SelectedValue + " ";

        }
        if ((FormLevel == 1 || FormLevel == 2) && ddlAssessmentType.SelectedIndex > 0)
        {
            conditions += "  and AssessmentType =" + ddlAssessmentType.SelectedValue + " ";
        }
        if (ddlForm.SelectedIndex > 0)
        {
            conditions += "  and Tarining_ID =" + ddlForm.SelectedValue + " ";
        }
        if (ddlYear.SelectedIndex > 0)
        {
            string Year = ddlYear.SelectedItem.Text;
            string[] Year1 = Year.Split('-');
            conditions += "    And FromDate >= '" + Year1[0] + "-04-01' and ToDate<='" + Year1[1] + "-03-31'";


        }
        DataTable dtHeader = null;
        if (Convert.ToInt32(ddlYear.SelectedValue) == 2026)
        {
            dtHeader = Get_DataFor2FilterReport("rptAssmentQutionwiseNew2026New", conditions.ToString(), "1");
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2025)
        {
            dtHeader = Get_DataFor2FilterReport("rptAssmentQutionwiseNew2025New", conditions.ToString(), "1");
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2024)
        {
            dtHeader = Get_DataFor2FilterReport("rptAssmentQutionwiseNew2024New", conditions.ToString(), "1");
        }
        else
        {
            dtHeader = Get_DataFor2FilterReport("rptAssmentQutionwiseNew", conditions.ToString(), "1");
        }

        Session["dtHeader"] = dtHeader;
        GVChildTarget.DataSource = null;
        GVChildTarget.DataBind();
        ExportReportQuestion();
        // exportTABLE_COMPLETESchor(dtHeader);






    }
    public void ExportReportQuestion()
    {

        DataTable dtMain = Session["dtHeader"] as DataTable;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\AssessmentQuestion.xlsx");
        var ws = wb.Worksheet(1);




        //for (int x = 0; x < dtMain.Columns.Count; x++)
        //{

        //    ws.Cell(1, x + 1).Value = dtMain.Columns[x].ColumnName;
        //}

        //dt1.Columns.Remove("rownNO");
        ws.Cell(2, 1).InsertData(dtMain.Rows);
        Int32 ii = Convert.ToInt32(dtMain.Rows.Count) + 2;
        string str = "A1:T" + ii;

        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        filepath = StartupPath + "\\QuestionWiseAnalysis " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
        wb.SaveAs(filepath);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
        Response.WriteFile(filepath);

        Response.End();
        if (File.Exists(filepath))
        {
            System.IO.File.Delete(filepath);
        }
    }

    protected void Lnkpfkj_OnddClick(object sender, EventArgs e)
    {
        ViewState["1"] = 500;

        ///  LoadPlanReportProcess(1);
        //GVChildTarget.Visible = false;
        //GVChild.Visible = false;
        //GV_DynamicGrid.Visible = false;





    }
    protected void Lnkpfkj_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 500;

        // LoadPlanReportTrakerNew2022(1);
        //GVChildTarget.Visible = false;
        //GVChild.Visible = false;
        //GV_DynamicGrid.Visible = false;





    }

    protected void Lnkpfkj33_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 500;

        // LoadPlanReportTrakerNCluster(1);
        //GVChildTarget.Visible = false;
        //GVChild.Visible = false;
        //GV_DynamicGrid.Visible = false;





    }
    protected void Lnkpfkj334_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 500;

        // LoadrptLocalVillagaePerformace(1);
        //GVChildTarget.Visible = false;
        //GVChild.Visible = false;
        //GV_DynamicGrid.Visible = false;





    }
    protected void LnkpfkjTest_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 500;

        //  LoadPlanReportTrakerNew2022(1);
        //GVChildTarget.Visible = false;
        //GVChild.Visible = false;
        //GV_DynamicGrid.Visible = false;





    }



    public void MultipuExeclProcess()
    {
        DataSet dtMain1 = ViewState["SAC"] as DataSet;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\WeeklyEnrolmentReportformat.xlsx");
        var ws = wb.Worksheet(1);
        var ws1 = wb.Worksheet(2);
        var ws2 = wb.Worksheet(3);
        var ws3 = wb.Worksheet(4);
        var ws4 = wb.Worksheet(5);
        DataTable dt = dtMain1.Tables[0];
        dt.Columns.Remove("RowNo");
        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("RowNo");
        ws.Cell(4, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 3;
        string str = "A4:G" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        DataTable dt1 = dtMain1.Tables[1];

        dt1.Columns.Remove("RowNo");

        ws1.Cell(3, 1).InsertData(dt1.Rows);
        Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 3;
        string str1 = "A4:G" + ii1;
        ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        DataTable dt2 = dtMain1.Tables[2];

        dt2.Columns.Remove("RowNo");
        ws2.Cell(4, 1).InsertData(dt2.Rows);
        Int32 ii2 = Convert.ToInt32(dt2.Rows.Count) + 3;
        string str2 = "A4:G" + ii2;
        ws2.Range(str2).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws2.Range(str2).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws2.Range(str2).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws2.Range(str2).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        DataTable dt3 = dtMain1.Tables[3];

        dt3.Columns.Remove("RowNo");
        ws3.Cell(4, 1).InsertData(dt3.Rows);
        Int32 ii3 = Convert.ToInt32(dt3.Rows.Count) + 3;
        string str3 = "A4:G" + ii2;
        ws3.Range(str3).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws3.Range(str3).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws3.Range(str3).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws3.Range(str3).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        DataTable dt4 = dtMain1.Tables[4];

        dt4.Columns.Remove("RowNo");
        ws3.Cell(20, 1).InsertData(dt4.Rows);


        DataTable dt5 = dtMain1.Tables[5];
        ws1.Cell(21, 1).InsertData(dt5.Rows);

        DataTable dt6 = dtMain1.Tables[6];
        ws2.Cell(21, 1).InsertData(dt6.Rows);

        DataTable dt7 = dtMain1.Tables[7];
        ws3.Cell(36, 1).InsertData(dt7.Rows);

        DataTable dt8 = dtMain1.Tables[8];
        ws4.Cell(4, 1).InsertData(dt8.Rows);

        //ws1.Cell(4, 1).InsertData(dt1.Rows);

        //Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 3;
        //string str1 = "A4:AL" + ii1;

        //ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        //ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        //ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        //ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        //DataTable dt2 = dtMain1.Tables[2];
        //dt2.Columns.Remove("rowno");
        //ws3.Cell(3, 1).InsertData(dt2.Rows);


        //Int32 ii11 = Convert.ToInt32(dt2.Rows.Count) + 2;
        //string str11 = "A3:O" + ii11;

        //ws3.Range(str11).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        //ws3.Range(str11).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        //ws3.Range(str11).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        //ws3.Range(str11).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
        //DataTable dt3 = dtMain1.Tables[3];
        //ws3.Cell(2, 2).Value = "Week (" + dt3.Rows[0]["Week1"].ToString() + " to  " + dt3.Rows[0]["Cumulative1"].ToString() + ")";
        //ws3.Cell(2, 3).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative1"].ToString() + ")";
        //ws3.Cell(2, 4).Value = "Week (" + dt3.Rows[0]["Week2"].ToString() + " to  " + dt3.Rows[0]["Cumulative2"].ToString() + ")";
        //ws3.Cell(2, 5).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative2"].ToString() + ")";
        //ws3.Cell(2, 6).Value = "Week (" + dt3.Rows[0]["Week3"].ToString() + " to  " + dt3.Rows[0]["Cumulative3"].ToString() + ")";
        //ws3.Cell(2, 7).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative3"].ToString() + ")";
        //ws3.Cell(2, 8).Value = "Week (" + dt3.Rows[0]["Week4"].ToString() + " to  " + dt3.Rows[0]["Cumulative4"].ToString() + ")";
        //ws3.Cell(2, 9).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative4"].ToString() + ")";
        //ws3.Cell(2, 10).Value = "Week (" + dt3.Rows[0]["Week5"].ToString() + " to  " + dt3.Rows[0]["Cumulative5"].ToString() + ")";
        //ws3.Cell(2, 11).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative5"].ToString() + ")";
        //ws3.Cell(2, 12).Value = "Week (" + dt3.Rows[0]["Week6"].ToString() + " to  " + dt3.Rows[0]["Cumulative6"].ToString() + ")";
        //ws3.Cell(2, 13).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative6"].ToString() + ")";
        //ws3.Cell(2, 14).Value = "Week (" + dt3.Rows[0]["Week7"].ToString() + " to  " + dt3.Rows[0]["Cumulative7"].ToString() + ")";
        //ws3.Cell(2, 15).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative7"].ToString() + ")";

        filepath = StartupPath + "\\PerformanceSummary " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
        wb.SaveAs(filepath);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
        Response.WriteFile(filepath);

        Response.End();
        if (File.Exists(filepath))
        {
            System.IO.File.Delete(filepath);
        }

    }

    public void MultipuExeclTrackFinal2022()
    {
        DataSet dtMain1 = ViewState["SAC"] as DataSet;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\QualityAlertNew2022.xlsx");
        var ws = wb.Worksheet(1);
        var ws1 = wb.Worksheet(4);
        var ws2 = wb.Worksheet(3);
        var ws3 = wb.Worksheet(2);
        //var ws1 = wb.Worksheet(2);
        //var ws3 = wb.Worksheet(3);
        DataTable dt = dtMain1.Tables[0];
        DataTable dt1 = dtMain1.Tables[1];
        DataTable dt2 = dtMain1.Tables[2];
        DataTable dt3 = dtMain1.Tables[3];
        //dt.Columns.Remove("rownNO");
        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("rownNO");
        ws.Cell(2, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 2;
        string str = "A2:BA" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
        #region Cluster
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            int[] arcols = { 11 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            int[] arcols = { 12 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {

            int[] arcols = { 23 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 90)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 90)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 24 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 25 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 15)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 15)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 26 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 27 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 28 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 29 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 30 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 31 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 33 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 34 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 35 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 36 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 38 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 39 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 40 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 41 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 42 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 43 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 44 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 45 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 46 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 47 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 48 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 49 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 51 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 1)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 52 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 1)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        //for (int x = 2; x < dt.Rows.Count + 2; x++)
        //{
        //    int[] arcols = { 53 };

        //    for (int y = 0; y < arcols.Length; y++)
        //    {
        //        if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
        //        {
        //        }

        //        else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 1)
        //        {
        //            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
        //        }

        //    }
        //}
        #endregion

        ws1.Cell(2, 1).InsertData(dt1.Rows);
        Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 2;
        string str1 = "A2:AS" + ii1;
        ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        #region StateWise
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            int[] arcols = { 3 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 20 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {

            int[] arcols = { 4 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 20 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }

            }
        }


        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 15 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 80 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 90)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 80)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 90)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 16 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 17 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 15)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 15)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 18 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 19 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 20 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 21 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 22 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 24 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 25 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 26 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 27 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 28 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 30 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 31 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 32 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 33 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 34 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }

            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 35 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 40)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 40)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 36 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 37 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 38 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 39 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 40 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 40)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 40)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 41 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 43 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 1)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt1.Rows.Count + 2; x++)
        {
            int[] arcols = { 44 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 1)
                {
                    ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        //for (int x = 2; x < dt1.Rows.Count + 2; x++)
        //{
        //    int[] arcols = { 45 };

        //    for (int y = 0; y < arcols.Length; y++)
        //    {
        //        if (Convert.ToString(ws1.Cell(x, arcols[y]).Value) == "")
        //        {
        //        }

        //        else if (Convert.ToDecimal(ws1.Cell(x, arcols[y]).Value) * 100 > 1)
        //        {
        //            ws1.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
        //        }

        //    }
        //}

        #endregion



        ws2.Cell(2, 1).InsertData(dt2.Rows);
        Int32 ii11 = Convert.ToInt32(dt2.Rows.Count) + 2;
        string str11 = "A2:AU" + ii11;
        ws2.Range(str11).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws2.Range(str11).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws2.Range(str11).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws2.Range(str11).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        #region Distict


        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            int[] arcols = { 5 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 20 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {

            int[] arcols = { 6 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 20 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 17 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 80 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 90)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 80)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 90)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 18 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 19 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 15)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 15)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 20 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 21 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 22 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 23 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 24 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 25 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 27 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 28 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }




        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 29 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 30 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 32 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 33 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 34 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 35 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 36 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }

            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 37 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 40)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 40)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 38 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 39 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 40 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 41 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 42 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 40)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 40)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 43 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 45 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 1)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt2.Rows.Count + 2; x++)
        {
            int[] arcols = { 46 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 1)
                {
                    ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        //for (int x = 2; x < dt2.Rows.Count + 2; x++)
        //{
        //    int[] arcols = { 47 };

        //    for (int y = 0; y < arcols.Length; y++)
        //    {
        //        if (Convert.ToString(ws2.Cell(x, arcols[y]).Value) == "")
        //        {
        //        }

        //        else if (Convert.ToDecimal(ws2.Cell(x, arcols[y]).Value) * 100 > 1)
        //        {
        //            ws2.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
        //        }

        //    }
        //}

        #endregion

        ws3.Cell(2, 1).InsertData(dt3.Rows);
        Int32 ii113 = Convert.ToInt32(dt3.Rows.Count) + 2;
        string str113 = "A2:AY" + ii113;
        ws3.Range(str113).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws3.Range(str113).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws3.Range(str113).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws3.Range(str113).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        #region Block

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            int[] arcols = { 9 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 20 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }

            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {

            int[] arcols = { 10 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 20 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 21 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 80 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 90)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 80)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 90)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 22 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 23 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 15)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 15)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 24 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 25 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 26 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 27 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 28 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 29 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 31 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 32 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 33 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 34 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 36 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 37 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 38 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 39 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 40 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }

            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 41 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 40)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 40)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 42 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 43 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 44 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 45 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 46 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 40)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 40)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 47 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 49 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        for (int x = 2; x < dt3.Rows.Count + 2; x++)
        {
            int[] arcols = { 50 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }

            }
        }
        //for (int x = 2; x < dt3.Rows.Count + 2; x++)
        //{
        //    int[] arcols = { 51 };

        //    for (int y = 0; y < arcols.Length; y++)
        //    {
        //        if (Convert.ToString(ws3.Cell(x, arcols[y]).Value) == "")
        //        {
        //        }
        //        else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 <= 50)
        //        {
        //            ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
        //        }
        //        else if (Convert.ToDecimal(ws3.Cell(x, arcols[y]).Value) * 100 > 50)
        //        {
        //            ws3.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
        //        }

        //    }
        //}

        #endregion
        filepath = StartupPath + "\\EnrolmentQualityAlert " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
        wb.SaveAs(filepath);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
        Response.WriteFile(filepath);

        Response.End();
        if (File.Exists(filepath))
        {
            System.IO.File.Delete(filepath);
        }

    }



    public void MultipuExeclClusterPerformaceReport()
    {
        DataTable dtMain1 = ViewState["SAC"] as DataTable;
        dtMain1 = ViewState["SAC"] as DataTable;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\ClusterPerformanceReport.xlsx");
        var ws = wb.Worksheet(1);

        DataTable dt = dtMain1;

        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("RowNo");
        ws.Cell(3, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 3;
        string str = "A3:AJ" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        filepath = StartupPath + "\\ClusterPerformanceReport" + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
        wb.SaveAs(filepath);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
        Response.WriteFile(filepath);

        Response.End();
        if (File.Exists(filepath))
        {
            System.IO.File.Delete(filepath);
        }

    }




    public void MultipuExeclClusterVillagaePerformace()
    {
        DataTable dtMain1 = ViewState["SAC"] as DataTable;
        dtMain1 = ViewState["SAC"] as DataTable;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\VillageExitReadyness.xlsx");
        var ws = wb.Worksheet(1);

        DataTable dt = dtMain1;

        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("RowNo");
        ws.Cell(2, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 3;
        string str = "A2:AD" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        filepath = StartupPath + "\\VillageExitReadinessReport" + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
        wb.SaveAs(filepath);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
        Response.WriteFile(filepath);

        Response.End();
        if (File.Exists(filepath))
        {
            System.IO.File.Delete(filepath);
        }

    }


    public static DataSet GetDataSet(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParameters)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(connString);
        SqlCommand cmd = new SqlCommand();

        try
        {
            PrepareCommand(cmd, conn, cmdType, cmdText, cmdParameters);
            da.SelectCommand = new SqlCommand();
            da.SelectCommand = cmd;
            da.Fill(ds);
            return ds;
        }
        catch
        {
            throw;
        }
        finally
        {
            conn.Close();
        }
    }

    private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParameters)
    {
        if (conn.State != ConnectionState.Open)
            conn.Open();
        cmd.Connection = conn;
        cmd.CommandTimeout = 0;
        cmd.CommandType = cmdType;
        cmd.CommandText = cmdText;

        if (cmdParameters != null)
        {
            foreach (SqlParameter param in cmdParameters)
            {
                cmd.Parameters.Add(param);
            }
        }
    }

    public void MultipuExecl()
    {
        DataSet dtMain1 = ViewState["SAC"] as DataSet;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\Planing.xlsx");
        var ws = wb.Worksheet(1);
        var ws1 = wb.Worksheet(2);
        var ws3 = wb.Worksheet(3);
        DataTable dt = dtMain1.Tables[0];
        dt.Columns.Remove("rownNO");
        DataTable dt1 = dtMain1.Tables[1];

        dt1.Columns.Remove("rownNO");
        ws.Cell(4, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 3;
        string str = "A4:AL" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
        ws1.Cell(4, 1).InsertData(dt1.Rows);

        Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 3;
        string str1 = "A4:AL" + ii1;

        ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        DataTable dt2 = dtMain1.Tables[2];
        dt2.Columns.Remove("rowno");
        ws3.Cell(3, 1).InsertData(dt2.Rows);


        Int32 ii11 = Convert.ToInt32(dt2.Rows.Count) + 2;
        string str11 = "A3:O" + ii11;

        ws3.Range(str11).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws3.Range(str11).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws3.Range(str11).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws3.Range(str11).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
        DataTable dt3 = dtMain1.Tables[3];
        ws3.Cell(2, 2).Value = "Week (" + dt3.Rows[0]["Week1"].ToString() + " to  " + dt3.Rows[0]["Cumulative1"].ToString() + ")";
        ws3.Cell(2, 3).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative1"].ToString() + ")";
        ws3.Cell(2, 4).Value = "Week (" + dt3.Rows[0]["Week2"].ToString() + " to  " + dt3.Rows[0]["Cumulative2"].ToString() + ")";
        ws3.Cell(2, 5).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative2"].ToString() + ")";
        ws3.Cell(2, 6).Value = "Week (" + dt3.Rows[0]["Week3"].ToString() + " to  " + dt3.Rows[0]["Cumulative3"].ToString() + ")";
        ws3.Cell(2, 7).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative3"].ToString() + ")";
        ws3.Cell(2, 8).Value = "Week (" + dt3.Rows[0]["Week4"].ToString() + " to  " + dt3.Rows[0]["Cumulative4"].ToString() + ")";
        ws3.Cell(2, 9).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative4"].ToString() + ")";
        ws3.Cell(2, 10).Value = "Week (" + dt3.Rows[0]["Week5"].ToString() + " to  " + dt3.Rows[0]["Cumulative5"].ToString() + ")";
        ws3.Cell(2, 11).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative5"].ToString() + ")";
        ws3.Cell(2, 12).Value = "Week (" + dt3.Rows[0]["Week6"].ToString() + " to  " + dt3.Rows[0]["Cumulative6"].ToString() + ")";
        ws3.Cell(2, 13).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative6"].ToString() + ")";
        ws3.Cell(2, 14).Value = "Week (" + dt3.Rows[0]["Week7"].ToString() + " to  " + dt3.Rows[0]["Cumulative7"].ToString() + ")";
        ws3.Cell(2, 15).Value = "Cumulative till date (" + dt3.Rows[0]["Cumulative7"].ToString() + ")";

        filepath = StartupPath + "\\EnrolmentSummary " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
        wb.SaveAs(filepath);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
        Response.WriteFile(filepath);

        Response.End();
        if (File.Exists(filepath))
        {
            System.IO.File.Delete(filepath);
        }

    }

    public void MultipuExeclTrackFinal()
    {
        DataSet dtMain1 = ViewState["SAC"] as DataSet;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\QualityAlert.xlsx");
        var ws = wb.Worksheet(1);
        //var ws1 = wb.Worksheet(2);
        //var ws3 = wb.Worksheet(3);
        DataTable dt = dtMain1.Tables[0];
        //dt.Columns.Remove("rownNO");
        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("rownNO");
        ws.Cell(2, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 2;
        string str = "A2:AT" + ii;
        int jj = ii + 1;
        string str2 = "A" + ii + 1 + ":AT" + jj;

        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 21 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 90)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 80)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 90)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 22 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 23 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 15)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 15)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 24 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 25 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 26 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 27 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 28 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 30 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 31 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 32 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 33 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 35 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 36 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 37 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 60)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 38 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 40 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 50)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }


        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 39 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }

                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }

            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 40 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 41 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 20 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 42 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }

        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 43 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 44 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 5 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 5)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 45 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 40)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }




        for (int x = 2; x < dt.Rows.Count + 2; x++)
        {
            int[] arcols = { 46 };

            for (int y = 0; y < arcols.Length; y++)
            {
                if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                {
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 25 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                }
                else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 25)
                {
                    ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                }
            }
        }
        // ws.Range(str2).Sty
        // ws.Range(str2).Style.Fill.BackgroundColor = XLColor.BabyBlue;
        //ws1.Cell(4, 1).InsertData(dt1.Rows);

        //Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 3;
        //string str1 = "A4:AG" + ii1;

        //ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        //ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        //ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        //ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        //DataTable dt2 = dtMain1.Tables[2];
        //dt2.Columns.Remove("rowno");
        //ws3.Cell(3, 1).InsertData(dt2.Rows);


        //Int32 ii11 = Convert.ToInt32(dt2.Rows.Count) + 2;
        //string str11 = "A3:O" + ii11;

        //ws3.Range(str11).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        //ws3.Range(str11).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        //ws3.Range(str11).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        //ws3.Range(str11).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);



        filepath = StartupPath + "\\EnrolmentQualityAlert " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
        wb.SaveAs(filepath);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
        Response.WriteFile(filepath);

        Response.End();
        if (File.Exists(filepath))
        {
            System.IO.File.Delete(filepath);
        }

    }
    public void MultipuExeclTrack()
    {
        DataSet dtMain1 = ViewState["SAC"] as DataSet;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\EnrolmentQARFormat.xlsx");
        var ws = wb.Worksheet(1);
        //var ws1 = wb.Worksheet(2);
        //var ws3 = wb.Worksheet(3);
        DataTable dt = dtMain1.Tables[0];
        //dt.Columns.Remove("rownNO");
        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("rownNO");
        ws.Cell(3, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 2;
        string str = "A2:AR" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
        //ws1.Cell(4, 1).InsertData(dt1.Rows);

        //Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 3;
        //string str1 = "A4:AG" + ii1;

        //ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        //ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        //ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        //ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        //DataTable dt2 = dtMain1.Tables[2];
        //dt2.Columns.Remove("rowno");
        //ws3.Cell(3, 1).InsertData(dt2.Rows);


        //Int32 ii11 = Convert.ToInt32(dt2.Rows.Count) + 2;
        //string str11 = "A3:O" + ii11;

        //ws3.Range(str11).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        //ws3.Range(str11).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        //ws3.Range(str11).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        //ws3.Range(str11).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);



        filepath = StartupPath + "\\EnrolmentQualityAlert " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
        wb.SaveAs(filepath);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
        Response.WriteFile(filepath);

        Response.End();
        if (File.Exists(filepath))
        {
            System.IO.File.Delete(filepath);
        }

    }




    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = Session["Summary"] as DataTable;
            if (Convert.ToString(Session["lnik"]) == "1")
            {
                ExporttoExcel(GVChildTarget, dt, "Summary");
            }
            if (Convert.ToString(Session["lnik"]) == "2")
            {
                ExporttoExcel(GVChildTarget, dt, "SurveyLinkReport");
            }
        }
        catch (Exception)
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
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }
    protected void GV_DynamicGrid1_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }



    protected void LnkChildSummaryTarget_OnClick(object sender, EventArgs e)
    {


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
            string sFileDir = Server.MapPath(Comman.GetImagePath("DataBackupPath")); ;
            string Fullfilename = "" + filePath + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".csv";
            string path = sFileDir + Fullfilename;
            File.WriteAllText(path, sbldr.ToString());

            FileStream fs = null;//, fs2=null;
            try
            {
                string path1 = Fullfilename;
                string foldername = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + path1 + "");
                string datafolder = path1.Substring(0, path1.Length - 4);
                //  string[] file = Directory.GetFiles(foldername);

                string fullPath = Request.MapPath("~/DataBackup/" + datafolder + "" + ".zip");
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(foldername, "");
                    //    zip.AddFiles(file, foldername);
                    zip.Save(Server.MapPath(Comman.GetImagePath("DataBackupPath")) + "/" + datafolder + "" + ".zip");
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
        //try
        //{
        //    DataTable dTExcel = Session["GridViewData"] as DataTable;
        //    ExporttoExcel(dTExcel, Convert.ToString(Session["Name"]));
        //}
        //catch (Exception)
        //{

        //    throw;
        //}
    }
    #endregion
}