using ClosedXML.Excel;
using Ionic.Zip;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmActivitySummaryReport : System.Web.UI.Page
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
                    LoadYear();
                    LoadUserLeavel();
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
    }

    public void LoadYear()
    {
        //DateTime GivenDate = DateTime.Now;
        //int GivenYear = GivenDate.Year + 1;
        //int m = GivenDate.Month;

        //DataTable dt = null;
        ////ddlYear.Items.Add("--Select--","0");
        //int y = GivenDate.Year + 1;


        //DateTime GivenDate1 = DateTime.Now;
        //int GivenYear1 = GivenDate1.Year;
        //DataTable dtYear = CreateDataTable();
        //DataRow dr;
        //if (ddlYear.SelectedIndex < 0)
        //{

        //    string mYear1 = GivenYear1.ToString();
        //    for (int j = 0; j < 1; j++)
        //    {
        //        if (m > 3)
        //        {
        //            dr = dtYear.NewRow();
        //            dr["Type"] = GivenYear.ToString() + "-" + Convert.ToString((GivenYear + 1));
        //            dr["ID"] = y;
        //            dtYear.Rows.Add(dr);
        //            dr = dtYear.NewRow();
        //            dr["Type"] = GivenYear - 1 + "-" + Convert.ToString((GivenYear - 1 + 1));
        //            dr["ID"] = y - 1;
        //            dtYear.Rows.Add(dr);
        //            //get last  two digits (eg: 10 from 2010);

        //        }
        //        else
        //        {

        //            Int32 m7 = y + 1;
        //            dr = dtYear.NewRow();
        //            dr["Type"] = Convert.ToString((y)) + "-" + m7.ToString();
        //            //y = y - 1;
        //            dr["ID"] = y;
        //            dtYear.Rows.Add(dr);
        //            dr = dtYear.NewRow();
        //            dr["Type"] = Convert.ToString((y - 1)) + "-" + y.ToString();
        //            //y = y - 1;
        //            dr["ID"] = y - 1;

        //            dtYear.Rows.Add(dr);


        //        }

        //    }

        //}
        DataTable dtYear = objComman.Generate_Financial_Year();

        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;


    }
    //public void LoadYear()
    //{
    //    DataTable dtYear = objComman.Generate_Financial_Year();
    //    objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

    //    ddlYear.SelectedIndex = 1;
    //    //}


    //}
    public DataTable CreateDataTable()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYear.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYear;
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
    public void LoadUserLeavel()
    {
        conditions = "";
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
        conditions = "";
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
        ddlPanchayat.Items.Clear();
        chkVillage.Items.Clear();
    }

    protected void ddlTpye_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["Annual"] = "";
        ViewState["D2dUser"] = "";
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
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
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
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

            ddlPanchayat.Items.Clear();
            chkVillage.Items.Clear();
        }
        else
        {
            foreach (ListItem item in ChkState.Items)
            {

                item.Selected = false;

            }
            chkDistrict.Items.Clear();
            chkBlock.Items.Clear();
            ddlPanchayat.Items.Clear();
            chkVillage.Items.Clear();
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPanchayat.Items.Clear();
        chkVillage.Items.Clear();
        chkDistrict.Items.Clear();
        chkBlock.Items.Clear();
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

        chkBlock.DataSource = dtDistrict;
        chkBlock.DataTextField = "BlockName";
        chkBlock.DataValueField = "BlockCode";
        chkBlock.DataBind();



        ddlPanchayat.Items.Clear();
        chkVillage.Items.Clear();
        if (Session["user_level_Role"].ToString() == "4")
        {
            if (chkBlock.Items.Count > 0)
            {
                foreach (ListItem item in chkBlock.Items)
                {

                    item.Selected = true;

                }
            }
            chkBlock.Enabled = false;
            ddlBlock_SelectedIndexChanged(ddlDistrict, null);
        }
    }
    public void FillCBCluster()
    {

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
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }
        conditions = "";
        DataTable dtDistrict = null;

        conditions = "DistrictCode in(" + ddlDistrict + ")  and BlockCode in(" + ddlBlock + ")";
        string strQry = "  SELECT PanchayatCode, dbo.TitleCase(upper(PanchayatName))  as PanchayatName FROM mstPanchayat where " + conditions + "  order by PanchayatName   ";
        dtDistrict = objMain.LoadData(strQry);



        // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        ddlPanchayat.DataSource = dtDistrict;
        ddlPanchayat.DataTextField = "PanchayatName";
        ddlPanchayat.DataValueField = "PanchayatCode";
        ddlPanchayat.DataBind();

        // objComman.BindDLL("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "--All--");


        chkVillage.Items.Clear();

    }
    public void FillCVillage()
    {
        conditions = "";
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
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        conditions = "";

        conditions = "DistrictCode in(" + ddlDistrict + ")  and BlockCode in(" + ddlBlock + ") and  PanchayatCode in(" + ddlPhan + ")";

        //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        //objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--All-");

        string strQry = "  SELECT VillageCode, dbo.TitleCase(upper(VillageName))  as VillageName FROM mst5Village where " + conditions + "  order by VillageName   ";
        DataTable dtDistrict = objMain.LoadData(strQry);

        chkVillage.DataSource = dtDistrict;
        chkVillage.DataTextField = "VillageName";
        chkVillage.DataValueField = "VillageCode";
        chkVillage.DataBind();


    }

    protected void LnkAnnualPlan_OnClick(object sender, EventArgs e)
    {


        ViewState["1"] = 9;
        if (ddlGroup.SelectedIndex > 0 && ddlTpye.SelectedIndex > 0)
        {

            getSACUpdateReport(1);


        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Group and Approve Type ')</script>", false);
        }


    }

    protected void LnkAnnualPladdd_OnClick(object sender, EventArgs e)
    {


        ViewState["1"] = 9;


        getSACUpdateReportNew(3);




    }
    protected void LnkAnnualPlanFC_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 10;
        if (ddlGroup.SelectedIndex > 0 && ddlTpye.SelectedIndex > 0)
        {

            getActivitySIPTargetvsAchv(1);


        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Group and Approve Type ')</script>", false);
        }


    }

    protected void LnkAnnualPlanFCrr_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 10;
        if (ddlTpye.SelectedIndex > 0)
        {

            getActivitySAC(1);


        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Approve Type ')</script>", false);
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

    private void GenerateExcelNewSUmmary(string FIleName)
    {
        try
        {
            string aprove = "";
            if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
            {
                aprove = "FC";
            }
            if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
            {
                aprove = "BO";
            }
            if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
            {
                aprove = "IO";
            }

            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                FIleName = "SACUpdateDistrictSummary" + aprove;
            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                FIleName = "SACUpdateBlockSummary" + aprove;
            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                FIleName = "SACUpdateSchool" + aprove;
            }
            DataTable dt = ViewState["SAC"] as DataTable;
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
                if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
                {
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='7' style='text-align:Center;border:.2pt solid windowtext;'>SAC Quarter Update Status </td>");
                    HttpContext.Current.Response.Write("</tr>");
                }
                if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
                {
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='9' style='text-align:Center;border:.2pt solid windowtext;'>SAC Quarter Update Status </td>");
                    HttpContext.Current.Response.Write("</tr>");
                }
                if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
                {
                    HttpContext.Current.Response.Write("<tr>");
                    HttpContext.Current.Response.Write("<td colspan='15' ' style='text-align:Center;border:.2pt solid windowtext;'>SAC Quarter Update Status </td>");
                    HttpContext.Current.Response.Write("</tr>");
                }
                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                int columnscount = GV_DynamicGrid.HeaderRow.Cells.Count;

                for (int j = 0; j < columnscount; j++)
                {
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> " + GV_DynamicGrid.HeaderRow.Cells[j].Text + "</th>");
                }

                HttpContext.Current.Response.Write("</tr>");
                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    HttpContext.Current.Response.Write("<tr>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");
                    }
                }
                HttpContext.Current.Response.Write("</tr>");
                HttpContext.Current.Response.Write("<tr>");
                for (int J = 0; J < 1; J++)
                {
                    if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
                    {
                        #region
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0 || c == 1)
                            {
                                if (c == 1)
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                                }
                                else
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'></td>");
                                }
                            }
                            else
                            {
                                string Col = "[" + dt.Columns[c].ColumnName + "]";
                                int sum = 0;
                                if (Convert.ToString(dt.Rows[J][dt.Columns[c].ColumnName]) == "")
                                {
                                }
                                else
                                {
                                    sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));
                                }
                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                        #endregion
                    }
                    if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
                    {
                        #region
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0 || c == 1 || c == 2 || c == 3)
                            {
                                if (c == 3)
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                                }
                                else
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'></td>");
                                }
                            }
                            else
                            {
                                string Col = "[" + dt.Columns[c].ColumnName + "]";
                                int sum = 0;
                                if (Convert.ToString(dt.Rows[J][dt.Columns[c].ColumnName]) == "")
                                {
                                }
                                else
                                {
                                    sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));
                                }
                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                        #endregion
                    }

                }

                HttpContext.Current.Response.Write("</tr>");
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
        if (ViewState["1"].ToString() == "9" && Convert.ToString(ViewState["SAC"]) != "")
        {
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1 || Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                GenerateExcelNewSUmmary("StaffTrainingPlanning");
            }
        }
        if (ViewState["1"].ToString() == "10")
        {
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                DataTable dt = Session["SAC"] as DataTable;
                MultipuExeclTrack333();


            }
            else
            {
                GenerateExcelNew("StaffTrainingPlanning");
            }
        }
    }
    public void MultipuExeclTrack333()
    {
        string filepath = "";
        try
        {
            DataTable dt = Session["SAC"] as DataTable;
            string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));

            string FIleName = "";
            string aprove = "";
            if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
            {
                aprove = "FC";
            }
            if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
            {
                aprove = "BO";
            }
            if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
            {
                aprove = "IO";
            }

            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                FIleName = "SIPTargetvsAchvDistrict" + aprove;
            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                FIleName = "SIPTargetvsAchvBlock" + aprove;
            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                FIleName = "SIPTargetvsAchvSchool" + aprove;
            }

            XLWorkbook wb = new XLWorkbook();
            wb = new XLWorkbook(StartupPath + "\\SIPTargetAch.xlsx");
            var ws = wb.Worksheet(1);

            //var ws1 = wb.Worksheet(2);
            //var ws3 = wb.Worksheet(3);

            //dt.Columns.Remove("rownNO");
            //DataTable dt1 = dtMain1.Tables[1];

            //dt1.Columns.Remove("rownNO");

            ws.Cell(4, 1).InsertData(dt.Rows);
            Int32 ii = Convert.ToInt32(dt.Rows.Count) + 3;
            string str = "A4:AX" + ii;
            ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);




            filepath = StartupPath + "\\" + FIleName + " " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
        catch (Exception)
        {
            //if (File.Exists(filepath))
            //{
            //    System.IO.File.Delete(filepath);
            //}
        }

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

                string fullPath = Request.MapPath("~/DataBackup/" + datafolder + "" + ".zip");
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




    public void LoadAnnualDataSummaryNew(int Flag)
    {

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

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }



        string condition = string.Empty;
        string cond = string.Empty;

        conditions += "  and mst5Village.Fyear='" + ddlYear.SelectedItem.Text + "' ";
        cond += "  mst2District.Fyear='" + ddlYear.SelectedItem.Text + "' ";
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
            cond += " and mst2District.DistrictCode in(" + ddlDistrict + ") ";

        }
        if (ddlBlock.Length > 0)
        {
            conditions += " and mst5Village.Blockcode in(" + ddlBlock + ") ";

        }


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con", conditions),
            new SqlParameter("@ConD", cond),
                    new SqlParameter("@Group", ddlGroup.SelectedValue)
        };

        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnnualSummaryReport]", cmdParameters);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Int32 GSSEnrolment = 0;
            Int32 GSSRetention = 0;
            Int32 MMRetention = 0;
            Int32 MMEnrolment = 0;
            if (Convert.ToString(dt.Rows[i]["GSS Enrolment"]) == "")
            {

            }
            else
            {
                GSSEnrolment = Convert.ToInt32(dt.Rows[i]["GSS Enrolment"]);
            }
            if (Convert.ToString(dt.Rows[i]["GSS Retention"]) == "")
            {

            }
            else
            {
                GSSRetention = Convert.ToInt32(dt.Rows[i]["GSS Retention"]);
            }
            if (Convert.ToString(dt.Rows[i]["MM Enrolment"]) == "")
            {

            }
            else
            {
                MMEnrolment = Convert.ToInt32(dt.Rows[i]["MM Enrolment"]);
            }
            if (Convert.ToString(dt.Rows[i]["MM Retention"]) == "")
            {

            }
            else
            {
                MMRetention = Convert.ToInt32(dt.Rows[i]["MM Retention"]);
            }

            dt.Rows[i]["Total GSS"] = GSSEnrolment + GSSRetention;
            dt.Rows[i]["Total MM"] = MMEnrolment + MMRetention;
        }
        dt.Columns.Remove("mon");
        ViewState["D2dUser"] = dt;

        GV_DynamicGrid.Visible = true;
        GV_DynamicGrid.DataSource = dt;
        GV_DynamicGrid.DataBind();

        ViewState["1"] = 100;




    }
    public void getSACUpdateReportNew(Int32 Flag)
    {


        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";
        string schoolCodeAprove = "";

        if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
        {
            Con += "  and UserEntry=3  ";
            schoolCodeAprove = "   UserEntry=3  ";
        }
        else if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
        {
            Con += "  and UserEntry=3   ";
            schoolCodeAprove = "   UserEntry=3  ";
        }
        else
        {
            Con += "  and UserEntry=3   ";
            schoolCodeAprove = "   UserEntry=3   ";
        }
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');
        string CreatDate = "" + Year1[0] + "-04-01";
        string CreatDate1 = "" + Year1[1] + "-03-31";



        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
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

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }




        //dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);

        SqlParameter[] cmdParameters = new SqlParameter[]
        {

            new SqlParameter("@schoolCode", conditions1 + Con),
        new SqlParameter("@Con", conditions1),
        new SqlParameter("@schoolCodeAprove", schoolCodeAprove + Con),
            new SqlParameter("@Fyear", ddlYear.SelectedItem.Text),
               new SqlParameter("@Groupby", ddlGroup.SelectedValue),
        };
        dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivitySACUpdateSummaryReportNew]", cmdParameters);
        ViewState["SAC"] = dtMain;


        if (dtMain.Rows.Count > 0)
        {
            DataTable newDataTable = dtMain.Clone();
            DataTable dtn = dtMain.Clone();
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2 || Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                for (int i = 0; i < 20; i++)
                {
                    dtn.ImportRow(dtMain.Rows[i]);

                }
                GV_DynamicGrid.Visible = true;
                GV_DynamicGrid.DataSource = dtn;
                GV_DynamicGrid.DataBind();
            }
            else
            {
                GV_DynamicGrid.Visible = true;
                GV_DynamicGrid.DataSource = dtMain;
                GV_DynamicGrid.DataBind();
            }


            string FIleName = "SACUpdateSchool";

            ExportToCSVFile(dtMain, FIleName);


            //  GenerateExcelSIP(dtMain, aprove);

        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }

    }
    public void getSACUpdateReport(Int32 Flag)
    {


        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";
        string schoolCodeAprove = "";

        if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
        {
            Con += "  and UserEntry=3  and ApproveStatus='FC' ";
            schoolCodeAprove = "   UserEntry=3  and ApproveStatus='FC' ";
        }
        else if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
        {
            Con += "  and UserEntry=3  and ApproveStatus='B' ";
            schoolCodeAprove = "   UserEntry=3  and ApproveStatus='B' ";
        }
        else
        {
            Con += "  and UserEntry=3  and ApproveStatus='I' ";
            schoolCodeAprove = "   UserEntry=3  and ApproveStatus='I' ";
        }
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');
        string CreatDate = "" + Year1[0] + "-04-01";
        string CreatDate1 = "" + Year1[1] + "-03-31";



        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
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

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }




        //dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);

        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@schoolCode", conditions1 + Con),
            new SqlParameter("@Con", conditions1),
            new SqlParameter("@schoolCodeAprove", schoolCodeAprove + Con),
            new SqlParameter("@Fyear", ddlYear.SelectedItem.Text),
            new SqlParameter("@Groupby", ddlGroup.SelectedValue),
        };
        dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivitySACUpdateSummaryReport]", cmdParameters);


        ViewState["SAC"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {
            DataTable newDataTable = dtMain.Clone();
            DataTable dtn = dtMain.Clone();
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2 || Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                for (int i = 0; i < 20; i++)
                {
                    dtn.ImportRow(dtMain.Rows[i]);

                }
                GV_DynamicGrid.Visible = true;
                GV_DynamicGrid.DataSource = dtn;
                GV_DynamicGrid.DataBind();
            }
            else
            {
                GV_DynamicGrid.Visible = true;
                GV_DynamicGrid.DataSource = dtMain;
                GV_DynamicGrid.DataBind();
            }

            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                string FIleName = "";
                string aprove = "";
                if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
                {
                    aprove = "FC";
                }
                if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
                {
                    aprove = "BO";
                }
                if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
                {
                    aprove = "IO";
                }

                if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
                {
                    FIleName = "SACUpdateDistrictSummary" + aprove;
                }
                if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
                {
                    FIleName = "SACUpdateBlockSummary" + aprove;
                }
                if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
                {
                    FIleName = "SACUpdateSchool" + aprove;
                }
                ExportToCSVFile(dtMain, FIleName);
            }

            //  GenerateExcelSIP(dtMain, aprove);

        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }

    }
    public void getActivitySAC(Int32 Flag)
    {


        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";
        string schoolCodeAprove = "";

        if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
        {
            Con += "  and UserEntry=3  and ApproveStatus='FC' ";
            schoolCodeAprove = "   UserEntry=3  and ApproveStatus='FC' ";
        }
        else if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
        {
            Con += "  and UserEntry=3  and ApproveStatus='B' ";
            schoolCodeAprove = "   UserEntry=3  and ApproveStatus='B' ";
        }
        else
        {
            Con += "  and UserEntry=3  and ApproveStatus='I' ";
            // schoolCodeAprove = "   UserEntry=3  and ApproveStatus='I' ";
        }
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');
        string CreatDate = "" + Year1[0] + "-04-01";
        string CreatDate1 = "" + Year1[1] + "-03-31";



        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
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

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions1 = conditions1 + " where   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }




        //dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);

        SqlParameter[] cmdParameters = new SqlParameter[]
        {

            new SqlParameter("@Con", conditions1 + Con),



        };
        dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptSchoolActivityandSAC]", cmdParameters);
        Session["SAC"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {
            objMain.ReportDownload("SAC Report", "Activity Summary Report", Convert.ToString(Session["username"]));
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();

            MultipuExeclProcess(dtMain);

            //  GenerateExcelSIP(dtMain, aprove);

        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }

    }
    public void MultipuExeclProcess(DataTable table)
    {

        string StartupPath = Server.MapPath(Comman.GetImagePath("MouPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\Sacupdatereport.xlsx");
        var ws = wb.Worksheet(1);



        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("RowNo");
        ws.Cell(2, 1).InsertData(table.Rows);
        Int32 ii = Convert.ToInt32(table.Rows.Count) + 1;
        string str = "A3:Q" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);






        filepath = StartupPath + "\\SACUpdate " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
    public void getActivitySIPTargetvsAchv(Int32 Flag)
    {


        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";
        string schoolCodeAprove = "";

        if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
        {
            Con += "  and UserEntry=3  and ApproveStatus='FC' ";
            schoolCodeAprove = "   UserEntry=3  and ApproveStatus='FC' ";
        }
        else if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
        {
            Con += "  and UserEntry=3  and ApproveStatus='B' ";
            schoolCodeAprove = "   UserEntry=3  and ApproveStatus='B' ";
        }
        else
        {
            // Con += "  and UserEntry=3  and ApproveStatus='I' ";
            // schoolCodeAprove = "   UserEntry=3  and ApproveStatus='I' ";
        }
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');
        string CreatDate = "" + Year1[0] + "-04-01";
        string CreatDate1 = "" + Year1[1] + "-03-31";



        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
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

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }




        //dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2024)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
           {

            new SqlParameter("@schoolCode", conditions1 + Con),
        new SqlParameter("@Con", conditions1),
        new SqlParameter("@schoolCodeAprove", schoolCodeAprove + Con),
            new SqlParameter("@Fyear", ddlYear.SelectedItem.Text),
               new SqlParameter("@Groupby", ddlGroup.SelectedValue),
               new SqlParameter("@Approve", ddlTpye.SelectedValue),


           };
            dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivitySIPTargetvsAchvNew2024]", cmdParameters);

        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2023)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
           {

            new SqlParameter("@schoolCode", conditions1 + Con),
        new SqlParameter("@Con", conditions1),
        new SqlParameter("@schoolCodeAprove", schoolCodeAprove + Con),
            new SqlParameter("@Fyear", ddlYear.SelectedItem.Text),
               new SqlParameter("@Groupby", ddlGroup.SelectedValue),
               new SqlParameter("@Approve", ddlTpye.SelectedValue),


           };
            dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivitySIPTargetvsAchvNew2023]", cmdParameters);

        }
        else
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {

            new SqlParameter("@schoolCode", conditions1 + Con),
        new SqlParameter("@Con", conditions1),
        new SqlParameter("@schoolCodeAprove", schoolCodeAprove + Con),
            new SqlParameter("@Fyear", ddlYear.SelectedItem.Text),
               new SqlParameter("@Groupby", ddlGroup.SelectedValue),
               new SqlParameter("@Approve", ddlTpye.SelectedValue),


            };
            dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivitySIPTargetvsAchvNew]", cmdParameters);
        }
        Session["SAC"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {
            objMain.ReportDownload("SIP Target vs Achv", "Activity Summary Report", Convert.ToString(Session["username"]));


            DataTable newDataTable = dtMain.Clone();
            DataTable dtn = dtMain.Clone();
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                for (int i = 0; i < 50; i++)
                {
                    dtn.ImportRow(dtMain.Rows[i]);
                }
                GV_DynamicGrid.Visible = true;
                GV_DynamicGrid.DataSource = dtn;
                GV_DynamicGrid.DataBind();
            }
            else
            {
                GV_DynamicGrid.Visible = true;
                GV_DynamicGrid.DataSource = dtMain;
                GV_DynamicGrid.DataBind();
            }


            //  GenerateExcelSIP(dtMain, aprove);

        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }

    }
    private void GenerateExcelNew(string FIleName)
    {
        try
        {


            string aprove = "";
            if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
            {
                aprove = "FC";
            }
            if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
            {
                aprove = "BO";
            }
            if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
            {
                aprove = "IO";
            }

            if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
            {
                FIleName = "SIPTargetvsAchvDistrict" + aprove;
            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
            {
                FIleName = "SIPTargetvsAchvBlock" + aprove;
            }
            if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
            {
                FIleName = "SIPTargetvsAchvDistrictSchool" + aprove;
            }

            DataTable dt = Session["SAC"] as DataTable;
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
                HttpContext.Current.Response.Write("<tr>");
                HttpContext.Current.Response.Write("<td colspan='30' ' style='text-align:Center;border:.2pt solid windowtext;'>SIP Target vs Achv </td>");

                HttpContext.Current.Response.Write("</tr>");

                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");

                if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
                {
                    HttpContext.Current.Response.Write("<th class='header' colspan='2'  rowspan='1' style='" + HeaderStyle + "  width:2%;'> </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Drinking Water</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Separate Toilet</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Kitchen Shed</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Boundary Wall</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Electricity</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Playground</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Swings  Slides</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Total</th>");

                }
                if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
                {
                    HttpContext.Current.Response.Write("<th class='header' colspan='4'  rowspan='1' style='" + HeaderStyle + "  width:2%;'> </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Drinking Water</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Separate Toilet</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Kitchen Shed</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Boundary Wall</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Electricity</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Playground</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Swings  Slides</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Total</th>");
                }
                if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
                {
                    HttpContext.Current.Response.Write("<th class='header' colspan='10'  rowspan='1' style='" + HeaderStyle + "  width:2%;'> </th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Drinking Water</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Separate Toilet</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Kitchen Shed</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Boundary Wall</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Electricity</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Playground</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Swings  Slides</th>");
                    HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Total</th>");
                }



                HttpContext.Current.Response.Write("</tr>");
                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                int columnscount = GV_DynamicGrid.HeaderRow.Cells.Count;

                for (int j = 0; j < columnscount; j++)
                {
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> " + GV_DynamicGrid.HeaderRow.Cells[j].Text + "</th>");
                }

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
                #region Row1



                #endregion


                HttpContext.Current.Response.Write("</tr>");

                HttpContext.Current.Response.Write("<tr>");
                for (int J = 0; J < 1; J++)
                {
                    if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0 || c == 1)
                            {
                                if (c == 1)
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                                }
                                else
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                                }
                            }
                            else
                            {
                                string Col = "[" + dt.Columns[c].ColumnName + "]";
                                int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                    }
                    if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0 || c == 2 || c == 1 || c == 3)
                            {
                                if (c == 4)
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                                }
                                else
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                                }
                            }
                            else
                            {
                                string Col = "[" + dt.Columns[c].ColumnName + "]";
                                int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                    }
                    if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0 || c == 2 || c == 1 || c == 3 || c == 4 || c == 5 || c == 6 || c == 7 || c == 8 || c == 9)
                            {
                                if (c == 9)
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                                }
                                else
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                                }
                            }
                            else
                            {
                                string Col = "[" + dt.Columns[c].ColumnName + "]";
                                int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
                    }
                    if (Convert.ToInt32(ddlGroup.SelectedValue) == 4)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c == 0 || c == 2 || c == 1 || c == 3 || c == 4 || c == 5)
                            {
                                if (c == 5)
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                                }
                                else
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                                }
                            }
                            else
                            {
                                string Col = "[" + dt.Columns[c].ColumnName + "]";
                                int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                            }
                        }
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

    protected void gvReportNew_RowCreated(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.Header)
        {


            if (ViewState["1"].ToString() == "10")
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                HeaderGridRow.CssClass = "gridnewheadercss";
                TableCell HeaderCell;



                if (Convert.ToInt32(ddlGroup.SelectedValue) == 1)
                {
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                    HeaderCell.ColumnSpan = 2;
                    HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                    HeaderGridRow.Cells.Add(HeaderCell);
                }

                if (Convert.ToInt32(ddlGroup.SelectedValue) == 2)
                {
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                    HeaderCell.ColumnSpan = 4;
                    HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                    HeaderGridRow.Cells.Add(HeaderCell);
                }

                if (Convert.ToInt32(ddlGroup.SelectedValue) == 3)
                {
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                    HeaderCell.ColumnSpan = 10;
                    HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                    HeaderGridRow.Cells.Add(HeaderCell);
                }







                HeaderCell = new TableCell();
                HeaderCell.Text = "Drinking Water";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                HeaderCell.ColumnSpan = 5;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Separate Toilet";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                HeaderCell.ColumnSpan = 5;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);




                HeaderCell = new TableCell();
                HeaderCell.Text = "Kitchen Shed";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 5;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Boundary Wall";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 5;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);



                HeaderCell = new TableCell();
                HeaderCell.Text = "Electricity";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 5;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);


                HeaderCell = new TableCell();
                HeaderCell.Text = "Playground";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 5;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);



                HeaderCell = new TableCell();
                HeaderCell.Text = "Swings  Slides";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 5;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);



                HeaderCell = new TableCell();
                HeaderCell.Text = "Total";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 5;
                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);


                GV_DynamicGrid.Controls[0].Controls.AddAt(0, HeaderGridRow);




            }
        }
    }

}