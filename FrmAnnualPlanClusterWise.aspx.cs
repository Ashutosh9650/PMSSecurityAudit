using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class FrmAnnualPlanClusterWise : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    string conditions = "";
    DataTable dtSearchVill = null;
    DataTable dtGKPPlan = null;
    public string RowNo = "", SchoolLeavel = "", BalSacha = "", GKP = "";
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    public bool vPhase = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                LoadYear();
                LoadUserLeavel();
                LoadGKPDetails();
                txtRemark.Attributes.Add("maxlength", "150");
                if (Convert.ToInt32(ddlYear.SelectedValue) >= 2022)
                {
                    divSub.Visible = false;
                }
                else
                {
                    divSub.Visible = true;
                }
                if (Convert.ToString(Session["user_level"]) == "39" || Convert.ToString(Session["user_level"]) == "145")
                {
                    btnSubmitted.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to submit the data to DOL? After Submitted, data will not be edited!!')");

                }
                if (Convert.ToString(Session["user_level"]) == "91")

                {
                    LinkButton1.Visible = false;
                    FileUpload1.Visible = false;
                    btnsave.Visible = false;
                    btnDelete.Visible = false;
                    btnSubmitted.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to submit the data to SOL ?')");

                    ImageButton1.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to reject annual plan?')");

                }
                if (Convert.ToString(Session["user_level"]) == "92")
                {
                    LinkButton1.Visible = false;
                    FileUpload1.Visible = false;
                    btnsave.Visible = false;
                    btnDelete.Visible = false;

                    btnSubmitted.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Approve Data? ')");
                    ImageButton1.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to reject annual plan?')");

                }

            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }

        }
    }
    public void LoadGKPDetails()
    {
        string strQry = "Select * from mstGKPPlan ";
        dtGKPPlan = objMain.LoadData(strQry);
        Session["dtGKPPlan"] = dtGKPPlan;
    }
    public void Locking()
    {
        if (ddlYear.SelectedIndex > 0)
        {

            // btnDelete.Enabled = true;
            // btnsave.Enabled = true;

            string strQry;
            if (Convert.ToInt32(ddlType.SelectedValue) == 1)
            {
                strQry = "Select * from mstModuleLocking  where [FromName]='Annual Plan District Wise' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "' ";
                #region  District Wise


                string Year = ddlYear.SelectedItem.Text;
                string[] Year1 = Year.Split('-');



                DateTime date1;
                DateTime date2;
                DataTable dtModel = objMain.LoadData(strQry);
                if (dtModel.Rows.Count > 0)
                {

                    date1 = Convert.ToDateTime(dtModel.Rows[0]["lockdate"].ToString());
                    date2 = DateTime.Now.Date;

                    if (date2 > date1)
                    {
                        FileUpload1.Enabled = false;
                        LinkButton1.Enabled = false;

                        btnSubmitted.Enabled = false;
                        btnReject.Enabled = false;
                        btnUnlock.Enabled = false;


                        btnDelete.Enabled = false;
                        btnsave.Enabled = false;
                    }

                }
                #endregion

            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 2)
            {
                strQry = "Select * from mstModuleLocking  where [FromName]='Annual Plan Cluster Wise' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "' ";
                #region  Village Wise


                string Year = ddlYear.SelectedItem.Text;
                string[] Year1 = Year.Split('-');



                DateTime date1;
                DateTime date2;
                DataTable dtModel = objMain.LoadData(strQry);
                if (dtModel.Rows.Count > 0)
                {


                    date1 = Convert.ToDateTime(dtModel.Rows[0]["lockdate"].ToString());
                    date2 = DateTime.Now.Date;


                    if (date2 > date1)

                    {
                        btnDelete.Enabled = false;
                        btnsave.Enabled = false;
                        FileUpload1.Enabled = false;
                        LinkButton1.Enabled = false;

                        btnSubmitted.Enabled = false;
                        btnReject.Enabled = false;
                        btnUnlock.Enabled = false;
                    }
                }
                #endregion

            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 0)
            {
                strQry = "Select * from mstModuleLocking  where [FromName]='Annual Plan District Wise' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "' ";
                #region  District Wise


                string Year = ddlYear.SelectedItem.Text;
                string[] Year1 = Year.Split('-');



                DateTime date1;
                DateTime date2;
                DataTable dtModel = objMain.LoadData(strQry);
                if (dtModel.Rows.Count > 0)
                {

                    date1 = Convert.ToDateTime(dtModel.Rows[0]["lockdate"].ToString());
                    date2 = DateTime.Now.Date;

                    if (date2 > date1)
                    {
                        FileUpload1.Enabled = false;
                        LinkButton1.Enabled = false;

                        btnSubmitted.Enabled = false;
                        btnReject.Enabled = false;
                        btnUnlock.Enabled = false;


                        btnDelete.Enabled = false;
                        btnsave.Enabled = false;
                    }

                }
                #endregion

            }
            //if (Convert.ToInt32(ddlType.SelectedValue) == 3)
            //{
            //    strQry = "Select * from mstModuleLocking  where [FromName]='Annual Plan School Wise' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "' ";
            //    #region  School Wise


            //    string Year = ddlYear.SelectedItem.Text;
            //    string[] Year1 = Year.Split('-');



            //    DateTime date1;
            //    DateTime date2;
            //    DataTable dtModel = objMain.LoadData(strQry);
            //    if (dtModel.Rows.Count > 0)
            //    {

            //        date1 = Convert.ToDateTime(dtModel.Rows[0]["lockdate"].ToString());
            //        date2 = DateTime.Now.Date;


            //        if (date2 > date1)
            //        {
            //            btnDelete.Enabled = false;
            //            btnsave.Enabled = false;

            //        }
            //    }
            //    #endregion

            //}
            //string strQry1 = "  SELECT * FROM [Tbl_PhaseMapping] where Phase=3   and  Financial_Year='" + ddlYear.SelectedItem.Text + "' and DistrictCode='" + ddlDistrict.SelectedValue + "'  ";
            //DataTable dtPhage = objMain.LoadData(strQry1);
            //if (dtPhage.Rows.Count > 0)
            //{
            //    vPhase = true;
            //    ViewState["vPhase"] = "1";
            //}
            //else
            //{
            //    ViewState["vPhase"] = "2";
            //}
        }
    }
    public void UserLevelFilter()
    {

        string strQry = "";
        string Cond = "Module='Annual Plan Entry'";
        strQry = "Select * from MstUserRight  where " + Cond + " and Role_Id=" + Session["user_level"].ToString() + "   ";


        DataTable dtRole = objMain.LoadData(strQry);

        if (dtRole.Rows.Count > 0)
        {
            vADD = Convert.ToBoolean(dtRole.Rows[0]["AddStatus"].ToString());
            vVerify = Convert.ToBoolean(dtRole.Rows[0]["verify_Status"].ToString());
            vDelete = Convert.ToBoolean(dtRole.Rows[0]["Delete_status"].ToString());


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

            //  btnsave.Enabled = true;

        }
        else
        {
            //  btnAdd.Enabled = false;

        }
        if (vVerify == true)
        {

            btnsave.Enabled = true;


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
    #region Fill Method

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
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            ddlState.SelectedIndex = 0;
            ddlState_SelectedIndexChanged(ddlDistrict, null);
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
            conditions = "";
            conditions = "StateCode ='" + ddlState.SelectedValue + "'  and Fyear= '" + ddlYear.SelectedItem.Text + "' ";
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

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

                }
                else
                {

                    Int32 m7 = y + 1;
                    dr = dtYear.NewRow();
                    dr["Type"] = Convert.ToString((y)) + "-" + m7.ToString();
                    //y = y - 1;
                    dr["ID"] = y;
                    dtYear.Rows.Add(dr);
                    dr = dtYear.NewRow();
                    dr["Type"] = Convert.ToString((y - 1)) + "-" + y.ToString();
                    //y = y - 1;
                    dr["ID"] = y - 1;

                    dtYear.Rows.Add(dr);


                }

            }

        }
        //DataTable dtYear = objComman.Generate_Financial_Year();

        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //DateTime GivenDate = DateTime.Now;
        //int GivenYear = GivenDate.Year;
        //int m = GivenDate.Month;

        //DataTable dt = null;
        ////ddlYear.Items.Add("--Select--","0");
        //int y = GivenDate.Year;


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

        //            dr = dtYear.NewRow();
        //            dr["Type"] = GivenYear - 2 + "-" + Convert.ToString((GivenYear - 2 + 1));
        //            dr["ID"] = y - 2;
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

        //            dr = dtYear.NewRow();
        //            dr["Type"] = GivenYear - 2 + "-" + Convert.ToString((GivenYear - 2 + 1));
        //            dr["ID"] = y - 2;
        //            dtYear.Rows.Add(dr);
        //        }


        //    }

        //}
        //DataTable dtYear = objComman.Generate_Financial_Year();

        //objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        //ddlYear.SelectedIndex = 1;
        //}


    }

    public DataTable CreateDataTable()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYear.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYear;
    }
    public void FillCBState()
    {
        conditions = "";
        objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");
    }
    public void FillCBDist()
    {

        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "StateCode ='" + ddlState.SelectedValue + "'  and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "StateCode ='" + ddlState.SelectedValue + "'  and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";
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
        conditions = "mstCluster.DistrictCode ='" + ddlDistrict.SelectedValue + "'  and mstCluster.BlockCode ='" + ddlBlock.SelectedValue + "' and  VillageGeographyOperational=1";
        objComman.BindDLL("mstCluster inner join mst5Village on mst5Village.ClusterCode=mstCluster.ClusterCode", "mstCluster.ClusterCode,dbo.TitleCase(upper(mstCluster.ClusterName)) as ClusterName", conditions, "ClusterName", "asc", ddlPanchayat, "ClusterName", "ClusterCode", "--Select--");

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

        string strQry = "  SELECT mst5Village.VillageCode, dbo.TitleCase(upper((mst5Village.VillageName))) + ' (' + dbo.TitleCase(upper(mstPanchayat.PanchayatName)) +')'   as VillageName FROM mst5Village INNER JOIN mstPanchayat ON mst5Village.PanchayatCode = mstPanchayat.PanchayatCode where " + conditions + "  order by VillageName   ";
        DataTable dtVillage = objMain.LoadData(strQry);

        objComman.BindDLLMasterTableVillage("mst5Village", "VillageName,VillageCode", dtVillage, conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "Select");



    }
    public void Bindgrid()
    {
        string str = string.Empty;
        if (ddlState.SelectedValue != null && ddlState.SelectedIndex > 0)
        {
            str = "where mst5Village.StateCode='" + ddlState.SelectedValue.ToString() + "'";
        }
        if (ddlDistrict.SelectedValue != null && ddlDistrict.SelectedIndex > 0)
        {
            str = str + "and mst5Village.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "'";
        }

        if (ddlBlock.SelectedValue != null && ddlBlock.SelectedIndex > 0)
        {
            str = str + "and mst5Village.BlockCode='" + ddlBlock.SelectedValue.ToString() + "'";
        }

        if (ddlPanchayat.SelectedValue != null && ddlPanchayat.SelectedIndex > 1)
        {
            str = str + "and mst5Village.PanchayatCode='" + ddlPanchayat.SelectedValue.ToString() + "'";
        }

        if (ddlVillage.SelectedValue != null && ddlVillage.SelectedIndex > 0)
        {
            str = str + "and mst5Village.VillageCode='" + ddlVillage.SelectedValue.ToString() + "'";
        }
        string strQry = "";
        if (ddlType.SelectedValue == "2")
        {

            GVMain.Columns[1].Visible = false;
            strQry = "select  VillageName +' ('+ EGVillagecode +')' as VillageName, Villagecode,'' SchoolName,'' as DISECode ,'' RowNo, '' SchoolLevel,'' BAlVal,'' GKP,'' GKPLevel,'' as ManagementType FROM mst5Village " + str + " and FunctionalStatus=1";
        }
        else if (ddlType.SelectedValue == "3")
        {
            GVMain.Columns[1].Visible = true;
            strQry = "SELECT VillageName   AS VillageName,Name +' ('+ DISECode +')'  AS SchoolName,SchoolCode as DISECode,SchoolLevel,mst5Village.Villagecode,'' RowNo,BAlVal,GKP,GKPLevel,ManagementType FROM mst5Village INNER JOIN mstSchool ON mst5Village.VillageCode = mstSchool.VillageCode " + str + " and WorkingStatus=1 and ManagementType=1 ";
        }
        DataTable dtSchool = objComman.LoadData(strQry);
        if (dtSchool.Rows.Count > 0)
        {

            GVMain.DataSource = dtSchool;
            GVMain.DataBind();
            GV_AnnualPlan.DataSource = null;
            GV_AnnualPlan.DataBind();
        }
        else
        {
            GVMain.DataSource = null;
            GVMain.DataBind();
        }
    }
    public void FillControls()
    {
    }
    public void LoadData()
    {
        string strQry = "";
        string Condtion = "";
        Int32 iCount = 0;
        Condtion = "where  mst5Village.StateCode='" + ddlState.SelectedValue.ToString() + "'";
        if (ddlBlock.SelectedValue != null && ddlBlock.SelectedIndex > 0)
        {
            Condtion = Condtion + " and mst5Village.BlockCode='" + ddlBlock.SelectedValue.ToString() + "'";
        }

        if (ddlDistrict.SelectedValue != null && ddlDistrict.SelectedIndex >= 0)
        {
            Condtion = Condtion + " and mst5Village.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "'";
        }

        if (ddlVillage.SelectedValue != null && ddlVillage.SelectedIndex > 0)
        {
            Condtion = Condtion + " and mst5Village.VillageCode='" + ddlVillage.SelectedValue.ToString() + "'";
        }
        //if (ddlType.SelectedValue == "2")
        //{
        //    strQry = " select Description,RowNo as LookupCode,  [Apr], [May], [Jun], [Jul], [Aug], [Sep], [Oct], [Nov], [Dec], [Jan], [Feb], [Mar],[RowNo] from tblAnualPlanDataDetail where VillageCode='" + ViewState["VillageCode"].ToString() + "' and PlanType=2 order by RowNo ";
        //}
        //else if (ddlType.SelectedValue == "3")
        //{

        //    strQry = " select Description,RowNo as LookupCode,  [Apr], [May], [Jun], [Jul], [Aug], [Sep], [Oct], [Nov], [Dec], [Jan], [Feb], [Mar],[RowNo] from tblAnualPlanDataDetail where SchoolCode='" + ViewState["SchoolId"].ToString() + "' and PlanType=3 order by RowNo ";
        //}

        DataTable dtPreLoad;
        //if (dtPreLoad.Rows.Count > 0)
        //{
        if (ddlType.SelectedValue == "2")
        {
            string SubType = "";
            if (ddlsubType.SelectedIndex > 0)
            {
                SubType = " and mstLookupAnnaulPlan.LookupType=" + ddlsubType.SelectedValue + " and isnull(mstLookupAnnaulPlan.EndMonth,0)>0 ";
            }
            else
            {
                SubType = " and isnull(mstLookupAnnaulPlan.EndMonth,0)>0 ";
            }
            string strQry4 = "";
            if (Convert.ToInt32(ddlYear.SelectedValue) >= 2022)
            {
                strQry4 = " select mstLookupAnnaulPlanNew.Description,mstLookupAnnaulPlanNew.LookupCode,  [Apr], [May], [Jun], [Jul], [Aug], [Sep], [Oct], [Nov], [Dec], [Jan], [Feb], [Mar],SysFlag,StartMonth,EndMonth,MaxVal,mstLookupAnnaulPlanNew.LookupType,PhageFlag from mstLookupAnnaulPlanNew   left join (select *  from tblAnualPlanDataDetail where Villagecode='" + Convert.ToString(ViewState["VillageCode"]) + "' and PlanType=2 )   as tblAnualPlanDataDetail on mstLookupAnnaulPlanNew.LookUpcode =tblAnualPlanDataDetail.RowNo where LookupFlag='APLV' and isnull(mstLookupAnnaulPlanNew.EndMonth,0)>0   order by seqno ";


            }
            else
            {
                strQry4 = " select mstLookupAnnaulPlan.Description,mstLookupAnnaulPlan.LookupCode,  [Apr], [May], [Jun], [Jul], [Aug], [Sep], [Oct], [Nov], [Dec], [Jan], [Feb], [Mar],SysFlag,StartMonth,EndMonth,MaxVal,mstLookupAnnaulPlan.LookupType,PhageFlag from mstLookupAnnaulPlan   left join (select *  from tblAnualPlanDataDetail where Villagecode='" + Convert.ToString(ViewState["VillageCode"]) + "' and PlanType=2 )   as tblAnualPlanDataDetail on mstLookupAnnaulPlan.LookUpcode =tblAnualPlanDataDetail.RowNo where LookupFlag='APLV'  " + SubType + "  order by seqno ";


            }
            dtSearchVill = objComman.LoadData(strQry4);
        }

        if (ddlType.SelectedValue == "3")
        {
            string SubType = "";

            if (ddlsubType.SelectedIndex > 0)
            {
                SubType = " and mstLookupAnnaulPlan.LookupType=" + ddlsubType.SelectedValue + " and isnull(mstLookupAnnaulPlan.EndMonth,0)>0 ";
            }
            else
            {
                SubType = " and isnull(mstLookupAnnaulPlan.EndMonth,0)>0 ";
            }
            string strQry4 = "";
            if (Convert.ToInt32(ddlYear.SelectedValue) >= 2022)
            {
                strQry4 = " select mstLookupAnnaulPlanNew.Description,mstLookupAnnaulPlanNew.LookupCode,  [Apr], [May], [Jun], [Jul], [Aug], [Sep], [Oct], [Nov], [Dec], [Jan], [Feb], [Mar],SysFlag,StartMonth,EndMonth,MaxVal,mstLookupAnnaulPlanNew.LookupType,PhageFlag from mstLookupAnnaulPlanNew   left join (select *  from tblAnualPlanDataDetail where schoolcode='" + Convert.ToString(ViewState["SchoolId"]) + "' and PlanType=3 )   as tblAnualPlanDataDetail on mstLookupAnnaulPlanNew.LookUpcode =tblAnualPlanDataDetail.RowNo where LookupFlag='APLS' and  isnull(mstLookupAnnaulPlanNew.EndMonth,0)>0  order by seqno ";

            }
            else
            {
                strQry4 = " select mstLookupAnnaulPlan.Description,mstLookupAnnaulPlan.LookupCode,  [Apr], [May], [Jun], [Jul], [Aug], [Sep], [Oct], [Nov], [Dec], [Jan], [Feb], [Mar],SysFlag,StartMonth,EndMonth,MaxVal,mstLookupAnnaulPlan.LookupType,PhageFlag from mstLookupAnnaulPlan   left join (select *  from tblAnualPlanDataDetail where schoolcode='" + Convert.ToString(ViewState["SchoolId"]) + "' and PlanType=3 )   as tblAnualPlanDataDetail on mstLookupAnnaulPlan.LookUpcode =tblAnualPlanDataDetail.RowNo where LookupFlag='APLS'  " + SubType + "  order by seqno ";

            }
            dtSearchVill = objComman.LoadData(strQry4);
        }

        if (dtSearchVill.Rows.Count > 0)
        {
            GV_AnnualPlan.DataSource = dtSearchVill;
            GV_AnnualPlan.DataBind();
        }
        Session["dtSearchVill"] = dtSearchVill;
        if (ddlType.SelectedValue == "2")
        {

            TbNeed();
        }
        if (ddlType.SelectedValue == "3")
        {
            DataTable dt = Session["dtLearing"] as DataTable;
            if (dt != null)
            {
                if (dt.Rows.Count > 0 && Convert.ToString(ViewState["GKP"]) == "1" && (Convert.ToString(ViewState["GKPLevel"]) == "1" || Convert.ToString(ViewState["GKPLevel"]) == "2" || Convert.ToString(ViewState["GKPLevel"]) == "3"))
                {
                    LEARNIOpenMonth(dt, Convert.ToInt32(dt.Rows[0]["Jun"]), Convert.ToInt32(dt.Rows[0]["Jul"]), Convert.ToInt32(dt.Rows[0]["Aug"]), Convert.ToInt32(dt.Rows[0]["Sep"]));
                }
            }
            BalEnableDisableMonth();
            for (int i = 0; i < GV_AnnualPlan.Rows.Count; i++)
            {
                if (dtSearchVill.Rows[i]["Description"].ToString() == "SAC Update" && Convert.ToString(ViewState["ManagementType"]) == "1")
                {


                    TextBox TxtApr = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtApr");
                    TextBox TxtMay = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMay");
                    TextBox TxtJun = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJun");
                    TextBox TxtJul = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJul");
                    TextBox TxtAug = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtAug");
                    TextBox TxtSep = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtSep");
                    TextBox TxtOct = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtOct");
                    TextBox TxtNov = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtNov");
                    TextBox TxtDec = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtDec");
                    TextBox TxtJan = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJan");
                    TextBox TxtFeb = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtFeb");
                    TextBox TxtMar = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMar");

                    TxtJul.Text = "1";
                    TxtOct.Text = "1";
                    TxtJan.Text = "1";
                    TxtMar.Text = "1";


                }
            }
        }

        //if (ddlType.SelectedValue == "2")
        //{
        //    string st = "select * from tblTempAnnualPlanFC where VillageCode='" + Convert.ToString(ViewState["VillageCode"]) + "'";
        //    DataTable dtSchoolData = objComman.LoadData(st);
        //    strQry = "";
        //    Int32 A1 = 0; Int32 A2 = 0; Int32 A3 = 0; Int32 A4 = 0; Int32 A5 = 0; Int32 A6 = 0; Int32 A7 = 0; Int32 A8 = 0;
        //    DataRow[] dr1 = dtSchoolData.Select("Villagecode='" + Convert.ToString(ViewState["VillageCode"]) + "' AND RowNo=2");
        //    if (dr1.Length > 0)
        //    {
        //        A1 = Convert.ToInt32(dr1[0]["FiveYrsOOSG"].ToString());
        //        A2 = Convert.ToInt32(dr1[0]["6 YRS OOSG TGT"].ToString());
        //        A3 = Convert.ToInt32(dr1[0]["7 - 14 YRS OOSG TGT"].ToString());
        //        A4 = Convert.ToInt32(dr1[0]["TOT OOSG TGT"].ToString());
        //        A5 = Convert.ToInt32(dr1[0]["FiveYrsOOSB"].ToString());
        //        A6 = Convert.ToInt32(dr1[0]["SIXYrsOOSB"].ToString());
        //        A7 = Convert.ToInt32(dr1[0]["7 - 14 YRS OOSB TGT"].ToString());
        //        A8 = A5 + A6 + A7;
        //    }
        //    for (int i = 0; i < GV_AnnualPlan.Rows.Count; i++)
        //    {
        //        TextBox TxtApr = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtApr");
        //        TextBox TxtMay = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMay");
        //        TextBox TxtJun = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJun");
        //        TextBox TxtJul = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJul");
        //        TextBox TxtAug = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtAug");
        //        TextBox TxtSep = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtSep");
        //        TextBox TxtOct = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtOct");
        //        TextBox TxtNov = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtNov");
        //        TextBox TxtDec = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtDec");
        //        TextBox TxtJan = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJan");
        //        TextBox TxtFeb = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtFeb");
        //        TextBox TxtMar = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMar");
        //        if (dtSearchVill.Rows[i]["Description"].ToString() == "5- Yrs OOSG")
        //        {
        //            TxtApr.Text = Convert.ToString(A1);
        //        }
        //        if (dtSearchVill.Rows[i]["Description"].ToString() == "6 Yrs OOSG")
        //        {
        //            TxtApr.Text = Convert.ToString(A2);
        //        }
        //        if (dtSearchVill.Rows[i]["Description"].ToString() == "7-14 Yrs OOSG")
        //        {
        //            TxtApr.Text = Convert.ToString(A3);
        //        }
        //        if (dtSearchVill.Rows[i]["Description"].ToString() == "Total OOSG")
        //        {
        //            TxtApr.Text = Convert.ToString(A4);
        //        }
        //        if (dtSearchVill.Rows[i]["Description"].ToString() == "5 Yrs OOSB")
        //        {
        //            TxtApr.Text = Convert.ToString(A5);
        //        }
        //        if (dtSearchVill.Rows[i]["Description"].ToString() == "6 Yrs OOSB")
        //        {
        //            TxtApr.Text = Convert.ToString(A6);
        //        }
        //        if (dtSearchVill.Rows[i]["Description"].ToString() == "7-14 Yrs OOSB")
        //        {
        //            TxtApr.Text = Convert.ToString(A7);
        //        }
        //        if (dtSearchVill.Rows[i]["Description"].ToString() == "Total OOSB")
        //        {
        //            TxtApr.Text = Convert.ToString(A8);
        //        }
        //    }

        //}
        //if (ddlType.SelectedValue == "3")
        //{
        //    string st = "select * from tblTempAnnualPlanFC where SchoolCode='" + Convert.ToString(ViewState["SchoolId"]) + "'";
        //    DataTable dtSchoolData = objComman.LoadData(st);
        //    strQry = "";
        //    DataRow[] dr1 = dtSchoolData.Select("SchoolCode='" + Convert.ToString(ViewState["SchoolId"]) + "' AND RowNo=3");
        //    Int32 CRITICALSIP = 0; Int32 OTHERSIP = 0; Int32 TOTALSIP = 0;
        //    if (dr1.Length > 0)
        //    {
        //        CRITICALSIP = Convert.ToInt32(dr1[0]["Critical InfraTgt (TOT)"].ToString());
        //        OTHERSIP = Convert.ToInt32(dr1[0]["Other Critical Infra Tgt)"].ToString());
        //        TOTALSIP = Convert.ToInt32(dr1[0]["TOTALSIP"].ToString());
        //        Session["CRITICALSIP"] = CRITICALSIP;
        //        Session["OTHERSIP"] = OTHERSIP;
        //        Session["TOTALSIP"] = TOTALSIP;
        //    }
        //    else
        //    {
        //        Session["CRITICALSIP"] = "0";
        //        Session["OTHERSIP"] = "0";
        //        Session["TOTALSIP"] = "0";
        //    }

        //    for (int i = 0; i < GV_AnnualPlan.Rows.Count; i++)
        //    {


        //        TextBox TxtApr = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtApr");
        //        TextBox TxtMay = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMay");
        //        TextBox TxtJun = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJun");
        //        TextBox TxtJul = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJul");
        //        TextBox TxtAug = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtAug");
        //        TextBox TxtSep = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtSep");
        //        TextBox TxtOct = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtOct");
        //        TextBox TxtNov = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtNov");
        //        TextBox TxtDec = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtDec");
        //        TextBox TxtJan = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJan");
        //        TextBox TxtFeb = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtFeb");
        //        TextBox TxtMar = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMar");

        //        if (dtSearchVill.Rows[i]["Description"].ToString() == "Critical SIP")
        //        {
        //            TxtMay.Text = Convert.ToString(CRITICALSIP);

        //        }
        //        if (dtSearchVill.Rows[i]["Description"].ToString() == "Other SIP")
        //        {
        //            TxtMay.Text = Convert.ToString(OTHERSIP);

        //        }
        //        if (dtSearchVill.Rows[i]["Description"].ToString() == "Total SIP TGT")
        //        {
        //            TxtMay.Text = Convert.ToString(TOTALSIP);

        //        }
        //        if (dtSearchVill.Rows[i]["Description"].ToString() == "SAC Update")
        //        {
        //            TxtJul.Text = "1";
        //            TxtOct.Text = "1";
        //            TxtJan.Text = "1";
        //            TxtMar.Text = "1";
        //        }
        //        if (dtSearchVill.Rows[i]["Description"].ToString() == "SMC Meet cum Orientation")
        //        {


        //            Int32 Apr = Convert.ToInt32(TxtApr.Text);
        //            Int32 May = Convert.ToInt32(TxtMay.Text);
        //            Int32 Jun = Convert.ToInt32(TxtJun.Text);
        //            Int32 Jul = Convert.ToInt32(TxtJul.Text);
        //            Int32 Aug = Convert.ToInt32(TxtAug.Text);
        //            Int32 Sep = Convert.ToInt32(TxtSep.Text);
        //            Int32 Oct = Convert.ToInt32(TxtOct.Text);
        //            Int32 Nov = Convert.ToInt32(TxtNov.Text);
        //            Int32 Dec = Convert.ToInt32(TxtDec.Text);
        //            Int32 Jan = Convert.ToInt32(TxtJan.Text);
        //            Int32 Feb = Convert.ToInt32(TxtFeb.Text);
        //            Int32 Mar = Convert.ToInt32(TxtMar.Text);


        //            SIP(dtSearchVill,Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec, Jan, Feb, Mar);

        //        }


        //    }
        //    //if (Convert.ToString(ViewState["GKP"]) == "1")
        //    //{
        //    //    GKPEnableDisableMonth();
        //    //}
        //    if (Convert.ToString(ViewState["BalSacha"]) == "1")
        //    {
        //        BalEnableDisableMonth();
        //    }
        //}

    }
    public void TbNeed()
    {
        Int32 TbEn = 0;
        Int32 TbEn1 = 0;
        Int32 TbEn2 = 0;
        for (int i = 0; i < GV_AnnualPlan.Rows.Count; i++)
        {


            TextBox TxtApr = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtApr");
            TextBox TxtMay = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMay");
            TextBox TxtJun = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJun");
            TextBox TxtJul = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJul");
            TextBox TxtAug = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtAug");
            TextBox TxtSep = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtSep");
            TextBox TxtOct = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtOct");
            TextBox TxtNov = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtNov");
            TextBox TxtDec = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtDec");
            TextBox TxtJan = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJan");
            TextBox TxtFeb = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtFeb");
            TextBox TxtMar = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMar");

            if (dtSearchVill.Rows[i]["Description"].ToString() == "TB Need- Enrolment")
            {
                if (TxtApr.Text != "")
                {
                    TbEn = Convert.ToInt32(TxtApr.Text);
                }
            }
            if (dtSearchVill.Rows[i]["Description"].ToString() == "TB Need- Learning")
            {
                if (TxtApr.Text != "")
                {
                    TbEn1 = Convert.ToInt32(TxtApr.Text);
                }
            }
            if (dtSearchVill.Rows[i]["Description"].ToString() == "TB Need- Enrolment+Learning")
            {
                if (TxtApr.Text != "")
                {
                    TbEn2 = Convert.ToInt32(TxtApr.Text);
                }
            }

            if (dtSearchVill.Rows[i]["Description"].ToString() == "TB Handhold- Enrolment" && TbEn > 0)
            {

                TxtApr.Enabled = true;

                TxtMay.Enabled = true;

                TxtJun.Enabled = true;

                TxtJul.Enabled = true;

                TxtAug.Enabled = true;

                TxtSep.Enabled = true;

                TxtOct.Enabled = true;

                TxtNov.Enabled = true;

                TxtDec.Enabled = true;

                TxtJan.Enabled = true;

                TxtFeb.Enabled = true;


                TxtMar.Enabled = true;

            }
            if (dtSearchVill.Rows[i]["Description"].ToString() == "TB Handhold- Learning" && TbEn1 > 0)
            {

                TxtApr.Enabled = true;

                TxtMay.Enabled = true;

                TxtJun.Enabled = true;

                TxtJul.Enabled = true;

                TxtAug.Enabled = true;

                TxtSep.Enabled = true;

                TxtOct.Enabled = true;

                TxtNov.Enabled = true;

                TxtDec.Enabled = true;

                TxtJan.Enabled = true;

                TxtFeb.Enabled = true;


                TxtMar.Enabled = true;

            }
            if (dtSearchVill.Rows[i]["Description"].ToString() == "TB Handhold- Enrolment + Learning" && TbEn1 > 0)
            {

                TxtApr.Enabled = true;

                TxtMay.Enabled = true;

                TxtJun.Enabled = true;

                TxtJul.Enabled = true;

                TxtAug.Enabled = true;

                TxtSep.Enabled = true;

                TxtOct.Enabled = true;

                TxtNov.Enabled = true;

                TxtDec.Enabled = true;

                TxtJan.Enabled = true;

                TxtFeb.Enabled = true;


                TxtMar.Enabled = true;

            }

        }
    }
    public void SIP(DataTable dt, Int32 Apr, Int32 May, Int32 Jun, Int32 Jul, Int32 Aug, Int32 Sep, Int32 Oct, Int32 Nov, Int32 Dec, Int32 Jan, Int32 Feb, Int32 Mar)
    {
        Int32 Total = Apr + May + Jun + Jul + Aug + Sep + Oct + Nov + Dec + Jan + Feb + Mar;
        for (int i = 0; i < GV_AnnualPlan.Rows.Count; i++)
        {



            if (dt.Rows[i]["Description"].ToString() == "Critical SIP")
            {

                TextBox TxtApr = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtApr");
                TextBox TxtMay = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMay");
                TextBox TxtJun = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJun");
                TextBox TxtJul = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJul");
                TextBox TxtAug = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtAug");
                TextBox TxtSep = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtSep");
                TextBox TxtOct = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtOct");
                TextBox TxtNov = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtNov");
                TextBox TxtDec = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtDec");
                TextBox TxtJan = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJan");
                TextBox TxtFeb = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtFeb");
                TextBox TxtMar = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMar");

                TxtApr.Text = "0";
                TxtMay.Text = "0";
                TxtJun.Text = "0";
                TxtJul.Text = "0";
                TxtAug.Text = "0";
                TxtSep.Text = "0";

                TxtOct.Text = "0";

                TxtNov.Text = "0";
                TxtDec.Text = "0";
                TxtJan.Text = "0";
                TxtFeb.Text = "0";
                TxtMar.Text = "0";


                if (Total > 0)
                {

                    if (Apr == 1)
                    {
                        TxtApr.Text = Session["CRITICALSIP"].ToString();

                    }
                    else if (May == 1)
                    {
                        TxtMay.Text = Session["CRITICALSIP"].ToString();
                    }
                    else if (Jun == 1)
                    {
                        TxtJun.Text = Session["CRITICALSIP"].ToString();
                    }
                    else if (Jul == 1)
                    {
                        TxtJul.Text = Session["CRITICALSIP"].ToString();
                    }
                    else if (Aug == 1)
                    {
                        TxtAug.Text = Session["CRITICALSIP"].ToString();
                    }
                    else if (Sep == 1)
                    {
                        TxtSep.Text = Session["CRITICALSIP"].ToString();
                    }
                    else if (Oct == 1)
                    {
                        TxtOct.Text = Session["CRITICALSIP"].ToString();
                    }
                    else if (Nov == 1)
                    {
                        TxtNov.Text = Session["CRITICALSIP"].ToString();
                    }
                    else if (Dec == 1)
                    {
                        TxtDec.Text = Session["CRITICALSIP"].ToString();
                    }
                    else if (Jan == 1)
                    {
                        TxtJan.Text = Session["CRITICALSIP"].ToString();
                    }
                    else if (Feb == 1)
                    {
                        TxtFeb.Text = Session["CRITICALSIP"].ToString();
                    }
                    else if (Mar == 1)
                    {
                        TxtMar.Text = Session["CRITICALSIP"].ToString();
                    }
                }
                else
                {
                    TxtMay.Text = Session["CRITICALSIP"].ToString();
                }
            }


            if (dt.Rows[i]["Description"].ToString() == "Other SIP")
            {

                TextBox TxtApr = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtApr");
                TextBox TxtMay = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMay");
                TextBox TxtJun = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJun");
                TextBox TxtJul = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJul");
                TextBox TxtAug = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtAug");
                TextBox TxtSep = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtSep");
                TextBox TxtOct = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtOct");
                TextBox TxtNov = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtNov");
                TextBox TxtDec = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtDec");
                TextBox TxtJan = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJan");
                TextBox TxtFeb = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtFeb");
                TextBox TxtMar = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMar");

                TxtApr.Text = "0";
                TxtMay.Text = "0";
                TxtJun.Text = "0";
                TxtJul.Text = "0";
                TxtAug.Text = "0";
                TxtSep.Text = "0";

                TxtOct.Text = "0";

                TxtNov.Text = "0";
                TxtDec.Text = "0";
                TxtJan.Text = "0";
                TxtFeb.Text = "0";
                TxtMar.Text = "0";



                if (Total > 0)
                {

                    if (Apr == 1)
                    {
                        TxtApr.Text = Session["OTHERSIP"].ToString();

                    }
                    else if (May == 1)
                    {
                        TxtMay.Text = Session["OTHERSIP"].ToString();
                    }
                    else if (Jun == 1)
                    {
                        TxtJun.Text = Session["OTHERSIP"].ToString();
                    }
                    else if (Jul == 1)
                    {
                        TxtJul.Text = Session["OTHERSIP"].ToString();
                    }
                    else if (Aug == 1)
                    {
                        TxtAug.Text = Session["OTHERSIP"].ToString();
                    }
                    else if (Sep == 1)
                    {
                        TxtSep.Text = Session["CRITICALSIP"].ToString();
                    }
                    else if (Oct == 1)
                    {
                        TxtOct.Text = Session["OTHERSIP"].ToString();
                    }
                    else if (Nov == 1)
                    {
                        TxtNov.Text = Session["OTHERSIP"].ToString();
                    }
                    else if (Dec == 1)
                    {
                        TxtDec.Text = Session["OTHERSIP"].ToString();
                    }
                    else if (Jan == 1)
                    {
                        TxtJan.Text = Session["OTHERSIP"].ToString();
                    }
                    else if (Feb == 1)
                    {
                        TxtFeb.Text = Session["OTHERSIP"].ToString();
                    }
                    else if (Mar == 1)
                    {
                        TxtMar.Text = Session["OTHERSIP"].ToString();
                    }
                }
                else
                {
                    TxtMay.Text = Session["OTHERSIP"].ToString();
                }
            }

            if (dt.Rows[i]["Description"].ToString() == "Total SIP TGT")
            {

                TextBox TxtApr = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtApr");
                TextBox TxtMay = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMay");
                TextBox TxtJun = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJun");
                TextBox TxtJul = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJul");
                TextBox TxtAug = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtAug");
                TextBox TxtSep = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtSep");
                TextBox TxtOct = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtOct");
                TextBox TxtNov = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtNov");
                TextBox TxtDec = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtDec");
                TextBox TxtJan = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJan");
                TextBox TxtFeb = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtFeb");
                TextBox TxtMar = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMar");

                TxtApr.Text = "0";
                TxtMay.Text = "0";
                TxtJun.Text = "0";
                TxtJul.Text = "0";
                TxtAug.Text = "0";
                TxtSep.Text = "0";

                TxtOct.Text = "0";

                TxtNov.Text = "0";
                TxtDec.Text = "0";
                TxtJan.Text = "0";
                TxtFeb.Text = "0";
                TxtMar.Text = "0";



                if (Total > 0)
                {

                    if (Apr == 1)
                    {
                        TxtApr.Text = Session["TOTALSIP"].ToString();

                    }
                    else if (May == 1)
                    {
                        TxtMay.Text = Session["TOTALSIP"].ToString();
                    }
                    else if (Jun == 1)
                    {
                        TxtJun.Text = Session["TOTALSIP"].ToString();
                    }
                    else if (Jul == 1)
                    {
                        TxtJul.Text = Session["TOTALSIP"].ToString();
                    }
                    else if (Aug == 1)
                    {
                        TxtAug.Text = Session["TOTALSIP"].ToString();
                    }
                    else if (Sep == 1)
                    {
                        TxtSep.Text = Session["TOTALSIP"].ToString();
                    }
                    else if (Oct == 1)
                    {
                        TxtOct.Text = Session["TOTALSIP"].ToString();
                    }
                    else if (Nov == 1)
                    {
                        TxtNov.Text = Session["TOTALSIP"].ToString();
                    }
                    else if (Dec == 1)
                    {
                        TxtDec.Text = Session["TOTALSIP"].ToString();
                    }
                    else if (Jan == 1)
                    {
                        TxtJan.Text = Session["TOTALSIP"].ToString();
                    }
                    else if (Feb == 1)
                    {
                        TxtFeb.Text = Session["TOTALSIP"].ToString();
                    }
                    else if (Mar == 1)
                    {
                        TxtMar.Text = Session["TOTALSIP"].ToString();
                    }
                }
                else
                {
                    TxtMay.Text = Session["TOTALSIP"].ToString();
                }
            }

        }
    }

    #endregion
    #region Button Click Events
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        // Locking();
        DataTable dtSchool = new DataTable();
        pnlMain.Enabled = true;


        if (ddlType.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Plan Type')</script>", false);

            return;

        }
        if (ddlDistrict.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select District')</script>", false);

            return;

        }
        if (ddlType.SelectedValue == "2")
        {

            if (ddlBlock.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Block')</script>", false);

                return;

            }
            if (ddlPanchayat.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Cluster')</script>", false);

                return;

            }
        }
        lblMsg.Visible = false;
        if (ddlType.SelectedValue == "1")
        {
            lblMsg.Visible = true;
            string SubType = "";
            if (Convert.ToInt32(ddlYear.SelectedValue) == 2026)
            {
                SqlParameter[] cmdParameters = new SqlParameter[]
            {

              new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
               new SqlParameter("@Flag", "1"),

            };
                // loadAnnaulPlanClusterWise
                dtSchool = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[loadAnnaulPlanClusterWise2025]", cmdParameters);


            }
            else if (Convert.ToInt32(ddlYear.SelectedValue) == 2025)
            {
                SqlParameter[] cmdParameters = new SqlParameter[]
            {

              new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
               new SqlParameter("@Flag", "1"),

            };
                // loadAnnaulPlanClusterWise
                dtSchool = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[loadAnnaulPlanClusterWise2025]", cmdParameters);


            }
            else if (Convert.ToInt32(ddlYear.SelectedValue) == 2024)
            {
                SqlParameter[] cmdParameters = new SqlParameter[]
            {

              new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
               new SqlParameter("@Flag", "1"),

            };
                // loadAnnaulPlanClusterWise
                dtSchool = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[loadAnnaulPlanClusterWise2023]", cmdParameters);


            }
            else
            {

                SqlParameter[] cmdParameters = new SqlParameter[]
            {

              new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
               new SqlParameter("@Flag", "1"),

            };
                // loadAnnaulPlanClusterWise
                dtSchool = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[loadAnnaulPlanClusterWise]", cmdParameters);

            }
            if (dtSchool.Rows.Count > 0)
            {
                Session["GridViewData"] = dtSchool;
                GVMain.DataSource = null;
                GVMain.DataBind();
                GV_AnnualPlan.Columns[1].Visible = true;
                GV_AnnualPlan.DataSource = dtSchool;
                GV_AnnualPlan.DataBind();

            }
            else
            {

                GV_AnnualPlan.DataSource = null;
                GV_AnnualPlan.DataBind();

            }
        }
        else
        {
            if (Convert.ToInt32(ddlYear.SelectedValue) == 2026)
            {
                SqlParameter[] cmdParameters = new SqlParameter[]
            {

              new SqlParameter("@DistrictCode", ddlPanchayat.SelectedValue),

               new SqlParameter("@Flag", "2"),

            };
                DataSet dtCL = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[loadAnnaulPlanClusterWise2025]", cmdParameters);

                DataTable dt = dtCL.Tables[0];

                DataTable dtCluster = dtCL.Tables[1];
                DataTable dttarget = dtCL.Tables[2];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string FiveYearsOOSG = Convert.ToString(dttarget.Rows[0]["i14"]);
                    //string sixYearsOOSG = Convert.ToString(dttarget.Rows[0]["i6"]);
                    //string SeventofourteenYearsOOSG = Convert.ToString(dttarget.Rows[0]["i14"]);
                    //string FifteentoeighteenYearsOOSG = Convert.ToString(dttarget.Rows[0]["iB6"]);
                    //string SeventofourteenOOSB = Convert.ToString(dttarget.Rows[0]["iB18"]);
                    if (i == 0)
                    {
                        dt.Rows[i]["Q5"] = FiveYearsOOSG;
                    }
                    //if (i == 1)
                    //{
                    //    dt.Rows[i]["Q5"] = sixYearsOOSG;
                    //}
                    //if (i == 2)
                    //{
                    //    dt.Rows[i]["Q5"] = SeventofourteenYearsOOSG;
                    //}
                    //if (i == 3)
                    //{
                    //    dt.Rows[i]["Q5"] = FifteentoeighteenYearsOOSG;
                    //}
                    //if (i == 4)
                    //{
                    //    dt.Rows[i]["Q5"] = SeventofourteenOOSB;
                    //}
                }
                if (dtCluster.Rows.Count > 0)
                {
                    string FiveYearsOOSG = Convert.ToString(dtCluster.Rows[0]["7-14 Years OOSG GoalQ1"]);
                    string SixYearsOOSG = Convert.ToString(dtCluster.Rows[0]["7-14 Years OOSG GoalQ2"]);
                    string SeventofourteenYearsOOSG = Convert.ToString(dtCluster.Rows[0]["SeventofourteenYearsOOSG"]);
                    string FifteentoeighteenYearsOOSG = Convert.ToString(dtCluster.Rows[0]["FifteentoeighteenYearsOOSG"]);
                    //string SeventofourteenOOSB = Convert.ToString(dtCluster.Rows[0]["SeventofourteenOOSB"]);
                    string Q1GSS = Convert.ToString(dtCluster.Rows[0]["Q1GSS"]); string Q2GSS = Convert.ToString(dtCluster.Rows[0]["Q2GSS"]); string Q3GSS = Convert.ToString(dtCluster.Rows[0]["Q3GSS"]);
                    string Q4GSS = Convert.ToString(dtCluster.Rows[0]["Q4GSS"]); string Q1MM = Convert.ToString(dtCluster.Rows[0]["Q1MM"]); string Q2MM = Convert.ToString(dtCluster.Rows[0]["Q2MM"]);
                    string Q3MM = Convert.ToString(dtCluster.Rows[0]["Q3MM"]); string Q4MM = Convert.ToString(dtCluster.Rows[0]["Q4MM"]); string Balsaba = Convert.ToString(dtCluster.Rows[0]["Balsaba"]);
                    string GkpSchool = Convert.ToString(dtCluster.Rows[0]["GkpSchool"]); string Gkp = Convert.ToString(dtCluster.Rows[0]["Gkp"]); string Sac1 = Convert.ToString(dtCluster.Rows[0]["Sac1"]);
                    string Sac2 = Convert.ToString(dtCluster.Rows[0]["Sac2"]); string Sac3 = Convert.ToString(dtCluster.Rows[0]["Sac3"]); string Sac4 = Convert.ToString(dtCluster.Rows[0]["Sac4"]);
                    string AGPCampQ1 = Convert.ToString(dtCluster.Rows[0]["PanchayatMeetingQ1"]); string AGPCampQ2 = Convert.ToString(dtCluster.Rows[0]["PanchayatMeetingQ2"]);
                    string AGPBeneficiariesQ1 = Convert.ToString(dtCluster.Rows[0]["RatriChaupalQ1"]);
                    string AGPBeneficiariesQ2 = Convert.ToString(dtCluster.Rows[0]["RatriChaupalQ2"]); string AGPBeneficiariesQ3 = Convert.ToString(dtCluster.Rows[0]["RatriChaupalQ3"]);
                    string NamankanRailyQ1 = Convert.ToString(dtCluster.Rows[0]["NamankanRailyQ1"]);
                    string GKPPlus = Convert.ToString(dtCluster.Rows[0]["#GKP Plus Schools"]);
                    string GKPPlusb = Convert.ToString(dtCluster.Rows[0]["#GKP Plus Beneficiaries"]);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            dt.Rows[i]["Q2"] = FiveYearsOOSG;
                            dt.Rows[i]["Q3"] = SixYearsOOSG;
                        }

                        //if (i == 4)
                        //{
                        //    dt.Rows[i]["Q1"] = SeventofourteenOOSB;
                        //}
                        if (i == 1)
                        {
                            dt.Rows[i]["Q1"] = Q1GSS;
                            dt.Rows[i]["Q2"] = Q2GSS;
                            dt.Rows[i]["Q3"] = Q3GSS;
                            dt.Rows[i]["Q4"] = Q4GSS;
                        }
                        if (i == 2)
                        {
                            dt.Rows[i]["Q1"] = Q1MM;
                            dt.Rows[i]["Q2"] = Q2MM;
                            dt.Rows[i]["Q3"] = Q3MM;
                            dt.Rows[i]["Q4"] = Q4MM;
                        }
                        if (i == 3)
                        {
                            dt.Rows[i]["Q1"] = AGPCampQ1;
                            dt.Rows[i]["Q2"] = AGPCampQ2;

                        }
                        if (i == 4)
                        {
                            dt.Rows[i]["Q1"] = AGPBeneficiariesQ1;
                            dt.Rows[i]["Q2"] = AGPBeneficiariesQ2;
                            dt.Rows[i]["Q3"] = AGPBeneficiariesQ3;

                        }
                        if (i == 5)
                        {
                            dt.Rows[i]["Q1"] = NamankanRailyQ1;


                        }
                        if (i == 6)
                        {
                            dt.Rows[i]["Q1"] = Balsaba;

                        }
                        if (i == 7)
                        {
                            dt.Rows[i]["Q1"] = GkpSchool;

                        }
                        if (i == 8)
                        {
                            dt.Rows[i]["Q1"] = Gkp;

                        }
                        if (i == 9)
                        {
                            dt.Rows[i]["Q1"] = GKPPlus;

                        }
                        if (i == 10)
                        {
                            dt.Rows[i]["Q1"] = GKPPlusb;

                        }
                        if (i == 11)
                        {
                            dt.Rows[i]["Q1"] = Sac1;
                            dt.Rows[i]["Q2"] = Sac2;
                            dt.Rows[i]["Q3"] = Sac3;
                            dt.Rows[i]["Q4"] = Sac4;
                        }



                    }
                }
                if (dt.Rows.Count > 0)
                {
                    GV_AnnualPlan.Columns[1].Visible = false;
                    GVMain.DataSource = null;
                    GVMain.DataBind();
                    GV_AnnualPlan.DataSource = dt;
                    GV_AnnualPlan.DataBind();

                }
                else
                {

                    GV_AnnualPlan.DataSource = null;
                    GV_AnnualPlan.DataBind();

                }

            }
            else if (Convert.ToInt32(ddlYear.SelectedValue) == 2025)
            {
                SqlParameter[] cmdParameters = new SqlParameter[]
            {

              new SqlParameter("@DistrictCode", ddlPanchayat.SelectedValue),

               new SqlParameter("@Flag", "2"),

            };
                DataSet dtCL = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[loadAnnaulPlanClusterWise2025]", cmdParameters);

                DataTable dt = dtCL.Tables[0];

                DataTable dtCluster = dtCL.Tables[1];
                DataTable dttarget = dtCL.Tables[2];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string FiveYearsOOSG = Convert.ToString(dttarget.Rows[0]["i14"]);
                    //string sixYearsOOSG = Convert.ToString(dttarget.Rows[0]["i6"]);
                    //string SeventofourteenYearsOOSG = Convert.ToString(dttarget.Rows[0]["i14"]);
                    //string FifteentoeighteenYearsOOSG = Convert.ToString(dttarget.Rows[0]["iB6"]);
                    //string SeventofourteenOOSB = Convert.ToString(dttarget.Rows[0]["iB18"]);
                    if (i == 0)
                    {
                        dt.Rows[i]["Q5"] = FiveYearsOOSG;
                    }
                    //if (i == 1)
                    //{
                    //    dt.Rows[i]["Q5"] = sixYearsOOSG;
                    //}
                    //if (i == 2)
                    //{
                    //    dt.Rows[i]["Q5"] = SeventofourteenYearsOOSG;
                    //}
                    //if (i == 3)
                    //{
                    //    dt.Rows[i]["Q5"] = FifteentoeighteenYearsOOSG;
                    //}
                    //if (i == 4)
                    //{
                    //    dt.Rows[i]["Q5"] = SeventofourteenOOSB;
                    //}
                }
                if (dtCluster.Rows.Count > 0)
                {
                    string FiveYearsOOSG = Convert.ToString(dtCluster.Rows[0]["7-14 Years OOSG GoalQ1"]);
                    string SixYearsOOSG = Convert.ToString(dtCluster.Rows[0]["7-14 Years OOSG GoalQ2"]);
                    string SeventofourteenYearsOOSG = Convert.ToString(dtCluster.Rows[0]["SeventofourteenYearsOOSG"]);
                    string FifteentoeighteenYearsOOSG = Convert.ToString(dtCluster.Rows[0]["FifteentoeighteenYearsOOSG"]);
                    //string SeventofourteenOOSB = Convert.ToString(dtCluster.Rows[0]["SeventofourteenOOSB"]);
                    string Q1GSS = Convert.ToString(dtCluster.Rows[0]["Q1GSS"]); string Q2GSS = Convert.ToString(dtCluster.Rows[0]["Q2GSS"]); string Q3GSS = Convert.ToString(dtCluster.Rows[0]["Q3GSS"]);
                    string Q4GSS = Convert.ToString(dtCluster.Rows[0]["Q4GSS"]); string Q1MM = Convert.ToString(dtCluster.Rows[0]["Q1MM"]); string Q2MM = Convert.ToString(dtCluster.Rows[0]["Q2MM"]);
                    string Q3MM = Convert.ToString(dtCluster.Rows[0]["Q3MM"]); string Q4MM = Convert.ToString(dtCluster.Rows[0]["Q4MM"]); string Balsaba = Convert.ToString(dtCluster.Rows[0]["Balsaba"]);
                    string GkpSchool = Convert.ToString(dtCluster.Rows[0]["GkpSchool"]); string Gkp = Convert.ToString(dtCluster.Rows[0]["Gkp"]); string Sac1 = Convert.ToString(dtCluster.Rows[0]["Sac1"]);
                    string Sac2 = Convert.ToString(dtCluster.Rows[0]["Sac2"]); string Sac3 = Convert.ToString(dtCluster.Rows[0]["Sac3"]); string Sac4 = Convert.ToString(dtCluster.Rows[0]["Sac4"]);
                    string AGPCampQ1 = Convert.ToString(dtCluster.Rows[0]["PanchayatMeetingQ1"]); string AGPCampQ2 = Convert.ToString(dtCluster.Rows[0]["PanchayatMeetingQ2"]);
                    string AGPBeneficiariesQ1 = Convert.ToString(dtCluster.Rows[0]["RatriChaupalQ1"]);
                    string AGPBeneficiariesQ2 = Convert.ToString(dtCluster.Rows[0]["RatriChaupalQ2"]); string AGPBeneficiariesQ3 = Convert.ToString(dtCluster.Rows[0]["RatriChaupalQ3"]);
                    string NamankanRailyQ1 = Convert.ToString(dtCluster.Rows[0]["NamankanRailyQ1"]);
                    string GKPPlus = Convert.ToString(dtCluster.Rows[0]["#GKP Plus Schools"]);
                    string GKPPlusb = Convert.ToString(dtCluster.Rows[0]["#GKP Plus Beneficiaries"]);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            dt.Rows[i]["Q2"] = FiveYearsOOSG;
                            dt.Rows[i]["Q3"] = SixYearsOOSG;
                        }

                        //if (i == 4)
                        //{
                        //    dt.Rows[i]["Q1"] = SeventofourteenOOSB;
                        //}
                        if (i == 1)
                        {
                            dt.Rows[i]["Q1"] = Q1GSS;
                            dt.Rows[i]["Q2"] = Q2GSS;
                            dt.Rows[i]["Q3"] = Q3GSS;
                            dt.Rows[i]["Q4"] = Q4GSS;
                        }
                        if (i == 2)
                        {
                            dt.Rows[i]["Q1"] = Q1MM;
                            dt.Rows[i]["Q2"] = Q2MM;
                            dt.Rows[i]["Q3"] = Q3MM;
                            dt.Rows[i]["Q4"] = Q4MM;
                        }
                        if (i == 3)
                        {
                            dt.Rows[i]["Q1"] = AGPCampQ1;
                            dt.Rows[i]["Q2"] = AGPCampQ2;

                        }
                        if (i == 4)
                        {
                            dt.Rows[i]["Q1"] = AGPBeneficiariesQ1;
                            dt.Rows[i]["Q2"] = AGPBeneficiariesQ2;
                            dt.Rows[i]["Q3"] = AGPBeneficiariesQ3;

                        }
                        if (i == 5)
                        {
                            dt.Rows[i]["Q1"] = NamankanRailyQ1;


                        }
                        if (i == 6)
                        {
                            dt.Rows[i]["Q1"] = Balsaba;

                        }
                        if (i == 7)
                        {
                            dt.Rows[i]["Q1"] = GkpSchool;

                        }
                        if (i == 8)
                        {
                            dt.Rows[i]["Q1"] = Gkp;

                        }
                        if (i == 9)
                        {
                            dt.Rows[i]["Q1"] = GKPPlus;

                        }
                        if (i == 10)
                        {
                            dt.Rows[i]["Q1"] = GKPPlusb;

                        }
                        if (i == 11)
                        {
                            dt.Rows[i]["Q1"] = Sac1;
                            dt.Rows[i]["Q2"] = Sac2;
                            dt.Rows[i]["Q3"] = Sac3;
                            dt.Rows[i]["Q4"] = Sac4;
                        }



                    }
                }
                if (dt.Rows.Count > 0)
                {
                    GV_AnnualPlan.Columns[1].Visible = false;
                    GVMain.DataSource = null;
                    GVMain.DataBind();
                    GV_AnnualPlan.DataSource = dt;
                    GV_AnnualPlan.DataBind();

                }
                else
                {

                    GV_AnnualPlan.DataSource = null;
                    GV_AnnualPlan.DataBind();

                }

            }
            else if (Convert.ToInt32(ddlYear.SelectedValue) == 2024)
            {
                SqlParameter[] cmdParameters = new SqlParameter[]
            {

              new SqlParameter("@DistrictCode", ddlPanchayat.SelectedValue),

               new SqlParameter("@Flag", "2"),

            };
                DataSet dtCL = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[loadAnnaulPlanClusterWise2023]", cmdParameters);

                DataTable dt = dtCL.Tables[0];

                DataTable dtCluster = dtCL.Tables[1];
                DataTable dttarget = dtCL.Tables[2];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string FiveYearsOOSG = Convert.ToString(dttarget.Rows[0]["i5"]);
                    string sixYearsOOSG = Convert.ToString(dttarget.Rows[0]["i6"]);
                    string SeventofourteenYearsOOSG = Convert.ToString(dttarget.Rows[0]["i14"]);
                    string FifteentoeighteenYearsOOSG = Convert.ToString(dttarget.Rows[0]["iB6"]);
                    string SeventofourteenOOSB = Convert.ToString(dttarget.Rows[0]["iB18"]);
                    if (i == 0)
                    {
                        dt.Rows[i]["Q5"] = FiveYearsOOSG;
                    }
                    if (i == 1)
                    {
                        dt.Rows[i]["Q5"] = sixYearsOOSG;
                    }
                    if (i == 2)
                    {
                        dt.Rows[i]["Q5"] = SeventofourteenYearsOOSG;
                    }
                    if (i == 3)
                    {
                        dt.Rows[i]["Q5"] = FifteentoeighteenYearsOOSG;
                    }
                    //if (i == 4)
                    //{
                    //    dt.Rows[i]["Q5"] = SeventofourteenOOSB;
                    //}
                }
                if (dtCluster.Rows.Count > 0)
                {
                    string FiveYearsOOSG = Convert.ToString(dtCluster.Rows[0]["FiveYearsOOSG"]);
                    string SixYearsOOSG = Convert.ToString(dtCluster.Rows[0]["SixYearsOOSG"]);
                    string SeventofourteenYearsOOSG = Convert.ToString(dtCluster.Rows[0]["SeventofourteenYearsOOSG"]);
                    string FifteentoeighteenYearsOOSG = Convert.ToString(dtCluster.Rows[0]["FifteentoeighteenYearsOOSG"]);
                    //string SeventofourteenOOSB = Convert.ToString(dtCluster.Rows[0]["SeventofourteenOOSB"]);
                    string Q1GSS = Convert.ToString(dtCluster.Rows[0]["Q1GSS"]); string Q2GSS = Convert.ToString(dtCluster.Rows[0]["Q2GSS"]); string Q3GSS = Convert.ToString(dtCluster.Rows[0]["Q3GSS"]);
                    string Q4GSS = Convert.ToString(dtCluster.Rows[0]["Q4GSS"]); string Q1MM = Convert.ToString(dtCluster.Rows[0]["Q1MM"]); string Q2MM = Convert.ToString(dtCluster.Rows[0]["Q2MM"]);
                    string Q3MM = Convert.ToString(dtCluster.Rows[0]["Q3MM"]); string Q4MM = Convert.ToString(dtCluster.Rows[0]["Q4MM"]); string Balsaba = Convert.ToString(dtCluster.Rows[0]["Balsaba"]);
                    string GkpSchool = Convert.ToString(dtCluster.Rows[0]["GkpSchool"]); string Gkp = Convert.ToString(dtCluster.Rows[0]["Gkp"]); string Sac1 = Convert.ToString(dtCluster.Rows[0]["Sac1"]);
                    string Sac2 = Convert.ToString(dtCluster.Rows[0]["Sac2"]); string Sac3 = Convert.ToString(dtCluster.Rows[0]["Sac3"]); string Sac4 = Convert.ToString(dtCluster.Rows[0]["Sac4"]);
                    string AGPCampQ1 = Convert.ToString(dtCluster.Rows[0]["AGPCampQ1"]); string AGPCampQ2 = Convert.ToString(dtCluster.Rows[0]["AGPCampQ2"]); string AGPCampQ3 = Convert.ToString(dtCluster.Rows[0]["AGPCampQ3"]);
                    string AGPCampQ4 = Convert.ToString(dtCluster.Rows[0]["AGPCampQ4"]); string AGPBeneficiariesQ1 = Convert.ToString(dtCluster.Rows[0]["AGPBeneficiariesQ1"]);
                    string AGPBeneficiariesQ2 = Convert.ToString(dtCluster.Rows[0]["AGPBeneficiariesQ2"]); string AGPBeneficiariesQ3 = Convert.ToString(dtCluster.Rows[0]["AGPBeneficiariesQ3"]);
                    string AGPBeneficiariesQ4 = Convert.ToString(dtCluster.Rows[0]["AGPBeneficiariesQ4"]);
                    string AGPPrerakQ1 = Convert.ToString(dtCluster.Rows[0]["AGPPrerakQ1"]); string AGPPrerakQ2 = Convert.ToString(dtCluster.Rows[0]["AGPPrerakQ2"]);
                    string AGPPrerakQ3 = Convert.ToString(dtCluster.Rows[0]["AGPPrerakQ3"]); string AGPPrerakQ4 = Convert.ToString(dtCluster.Rows[0]["AGPPrerakQ4"]);
                    string CBLVillages = Convert.ToString(dtCluster.Rows[0]["CBLVillages"]);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            dt.Rows[i]["Q1"] = FiveYearsOOSG;
                        }
                        if (i == 1)
                        {
                            dt.Rows[i]["Q1"] = SixYearsOOSG;
                        }
                        if (i == 2)
                        {
                            dt.Rows[i]["Q1"] = SeventofourteenYearsOOSG;
                        }
                        if (i == 3)
                        {
                            dt.Rows[i]["Q1"] = FifteentoeighteenYearsOOSG;
                        }
                        //if (i == 4)
                        //{
                        //    dt.Rows[i]["Q1"] = SeventofourteenOOSB;
                        //}
                        if (i == 4)
                        {
                            dt.Rows[i]["Q1"] = Q1GSS;
                            dt.Rows[i]["Q2"] = Q2GSS;
                            dt.Rows[i]["Q3"] = Q3GSS;
                            dt.Rows[i]["Q4"] = Q4GSS;
                        }
                        if (i == 5)
                        {
                            dt.Rows[i]["Q1"] = Q1MM;
                            dt.Rows[i]["Q2"] = Q2MM;
                            dt.Rows[i]["Q3"] = Q3MM;
                            dt.Rows[i]["Q4"] = Q4MM;
                        }
                        //if (i == 6)
                        //{
                        //    dt.Rows[i]["Q1"] = CBLVillages;

                        //}
                        if (i == 6)
                        {
                            dt.Rows[i]["Q1"] = Balsaba;

                        }
                        if (i == 7)
                        {
                            dt.Rows[i]["Q1"] = GkpSchool;

                        }
                        if (i == 8)
                        {
                            dt.Rows[i]["Q1"] = Gkp;

                        }
                        if (i == 9)
                        {
                            dt.Rows[i]["Q1"] = Sac1;
                            dt.Rows[i]["Q2"] = Sac2;
                            dt.Rows[i]["Q3"] = Sac3;
                            dt.Rows[i]["Q4"] = Sac4;
                        }
                        if (i == 10)
                        {
                            dt.Rows[i]["Q1"] = AGPCampQ1;
                            dt.Rows[i]["Q2"] = AGPCampQ2;
                            dt.Rows[i]["Q3"] = AGPCampQ3;
                            dt.Rows[i]["Q4"] = AGPCampQ4;
                        }
                        if (i == 11)
                        {
                            dt.Rows[i]["Q1"] = AGPBeneficiariesQ1;
                            dt.Rows[i]["Q2"] = AGPBeneficiariesQ2;
                            dt.Rows[i]["Q3"] = AGPBeneficiariesQ3;
                            dt.Rows[i]["Q4"] = AGPBeneficiariesQ4;
                        }
                        if (i == 12)
                        {
                            dt.Rows[i]["Q1"] = AGPPrerakQ1;
                            dt.Rows[i]["Q2"] = AGPPrerakQ2;
                            dt.Rows[i]["Q3"] = AGPPrerakQ3;
                            dt.Rows[i]["Q4"] = AGPPrerakQ4;
                        }


                    }
                }
                if (dt.Rows.Count > 0)
                {
                    GV_AnnualPlan.Columns[1].Visible = false;
                    GVMain.DataSource = null;
                    GVMain.DataBind();
                    GV_AnnualPlan.DataSource = dt;
                    GV_AnnualPlan.DataBind();

                }
                else
                {

                    GV_AnnualPlan.DataSource = null;
                    GV_AnnualPlan.DataBind();

                }

            }
            else
            {

                SqlParameter[] cmdParameters = new SqlParameter[]
                {

              new SqlParameter("@DistrictCode", ddlPanchayat.SelectedValue),

               new SqlParameter("@Flag", "2"),

                };
                DataSet dtCL = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[loadAnnaulPlanClusterWise]", cmdParameters);

                DataTable dt = dtCL.Tables[0];

                DataTable dtCluster = dtCL.Tables[1];
                DataTable dttarget = dtCL.Tables[2];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string FiveYearsOOSG = Convert.ToString(dttarget.Rows[0]["i6"]);
                    string SeventofourteenYearsOOSG = Convert.ToString(dttarget.Rows[0]["i14"]);
                    string FifteentoeighteenYearsOOSG = Convert.ToString(dttarget.Rows[0]["iB6"]);
                    string SeventofourteenOOSB = Convert.ToString(dttarget.Rows[0]["iB18"]);
                    if (i == 0)
                    {
                        dt.Rows[i]["Q5"] = FiveYearsOOSG;
                    }
                    if (i == 1)
                    {
                        dt.Rows[i]["Q5"] = SeventofourteenYearsOOSG;
                    }
                    if (i == 2)
                    {
                        dt.Rows[i]["Q5"] = FifteentoeighteenYearsOOSG;
                    }
                    if (i == 3)
                    {
                        dt.Rows[i]["Q5"] = SeventofourteenOOSB;
                    }
                }
                if (dtCluster.Rows.Count > 0)
                {

                    string FiveYearsOOSG = Convert.ToString(dtCluster.Rows[0]["FiveYearsOOSG"]);
                    string SeventofourteenYearsOOSG = Convert.ToString(dtCluster.Rows[0]["SeventofourteenYearsOOSG"]);
                    string FifteentoeighteenYearsOOSG = Convert.ToString(dtCluster.Rows[0]["FifteentoeighteenYearsOOSG"]);
                    string SeventofourteenOOSB = Convert.ToString(dtCluster.Rows[0]["SeventofourteenOOSB"]);
                    string Q1GSS = Convert.ToString(dtCluster.Rows[0]["Q1GSS"]); string Q2GSS = Convert.ToString(dtCluster.Rows[0]["Q2GSS"]); string Q3GSS = Convert.ToString(dtCluster.Rows[0]["Q3GSS"]);
                    string Q4GSS = Convert.ToString(dtCluster.Rows[0]["Q4GSS"]); string Q1MM = Convert.ToString(dtCluster.Rows[0]["Q1MM"]); string Q2MM = Convert.ToString(dtCluster.Rows[0]["Q2MM"]);
                    string Q3MM = Convert.ToString(dtCluster.Rows[0]["Q3MM"]); string Q4MM = Convert.ToString(dtCluster.Rows[0]["Q4MM"]); string Balsaba = Convert.ToString(dtCluster.Rows[0]["Balsaba"]);
                    string GkpSchool = Convert.ToString(dtCluster.Rows[0]["GkpSchool"]); string Gkp = Convert.ToString(dtCluster.Rows[0]["Gkp"]); string Sac1 = Convert.ToString(dtCluster.Rows[0]["Sac1"]);
                    string Sac2 = Convert.ToString(dtCluster.Rows[0]["Sac2"]); string Sac3 = Convert.ToString(dtCluster.Rows[0]["Sac3"]); string Sac4 = Convert.ToString(dtCluster.Rows[0]["Sac4"]);
                    string AGPCampQ1 = Convert.ToString(dtCluster.Rows[0]["AGPCampQ1"]); string AGPCampQ2 = Convert.ToString(dtCluster.Rows[0]["AGPCampQ2"]); string AGPCampQ3 = Convert.ToString(dtCluster.Rows[0]["AGPCampQ3"]);
                    string AGPCampQ4 = Convert.ToString(dtCluster.Rows[0]["AGPCampQ4"]); string AGPBeneficiariesQ1 = Convert.ToString(dtCluster.Rows[0]["AGPBeneficiariesQ1"]);
                    string AGPBeneficiariesQ2 = Convert.ToString(dtCluster.Rows[0]["AGPBeneficiariesQ2"]); string AGPBeneficiariesQ3 = Convert.ToString(dtCluster.Rows[0]["AGPBeneficiariesQ3"]);
                    string AGPBeneficiariesQ4 = Convert.ToString(dtCluster.Rows[0]["AGPBeneficiariesQ4"]);
                    string AGPPrerakQ1 = Convert.ToString(dtCluster.Rows[0]["AGPPrerakQ1"]); string AGPPrerakQ2 = Convert.ToString(dtCluster.Rows[0]["AGPPrerakQ2"]);
                    string AGPPrerakQ3 = Convert.ToString(dtCluster.Rows[0]["AGPPrerakQ3"]); string AGPPrerakQ4 = Convert.ToString(dtCluster.Rows[0]["AGPPrerakQ4"]);
                    string CBLVillages = Convert.ToString(dtCluster.Rows[0]["CBLVillages"]);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            dt.Rows[i]["Q1"] = FiveYearsOOSG;
                        }
                        if (i == 1)
                        {
                            dt.Rows[i]["Q1"] = SeventofourteenYearsOOSG;
                        }
                        if (i == 2)
                        {
                            dt.Rows[i]["Q1"] = FifteentoeighteenYearsOOSG;
                        }
                        if (i == 3)
                        {
                            dt.Rows[i]["Q1"] = SeventofourteenOOSB;
                        }
                        if (i == 4)
                        {
                            dt.Rows[i]["Q1"] = Q1GSS;
                            dt.Rows[i]["Q2"] = Q2GSS;
                            dt.Rows[i]["Q3"] = Q3GSS;
                            dt.Rows[i]["Q4"] = Q4GSS;
                        }
                        if (i == 5)
                        {
                            dt.Rows[i]["Q1"] = Q1MM;
                            dt.Rows[i]["Q2"] = Q2MM;
                            dt.Rows[i]["Q3"] = Q3MM;
                            dt.Rows[i]["Q4"] = Q4MM;
                        }
                        if (i == 6)
                        {
                            dt.Rows[i]["Q1"] = CBLVillages;

                        }
                        if (i == 7)
                        {
                            dt.Rows[i]["Q1"] = Balsaba;

                        }
                        if (i == 8)
                        {
                            dt.Rows[i]["Q1"] = GkpSchool;

                        }
                        if (i == 9)
                        {
                            dt.Rows[i]["Q1"] = Gkp;

                        }
                        if (i == 10)
                        {
                            dt.Rows[i]["Q1"] = Sac1;
                            dt.Rows[i]["Q2"] = Sac2;
                            dt.Rows[i]["Q3"] = Sac3;
                            dt.Rows[i]["Q4"] = Sac4;
                        }
                        if (i == 11)
                        {
                            dt.Rows[i]["Q1"] = AGPCampQ1;
                            dt.Rows[i]["Q2"] = AGPCampQ2;
                            dt.Rows[i]["Q3"] = AGPCampQ3;
                            dt.Rows[i]["Q4"] = AGPCampQ4;
                        }
                        if (i == 12)
                        {
                            dt.Rows[i]["Q1"] = AGPBeneficiariesQ1;
                            dt.Rows[i]["Q2"] = AGPBeneficiariesQ2;
                            dt.Rows[i]["Q3"] = AGPBeneficiariesQ3;
                            dt.Rows[i]["Q4"] = AGPBeneficiariesQ4;
                        }
                        if (i == 13)
                        {
                            dt.Rows[i]["Q1"] = AGPPrerakQ1;
                            dt.Rows[i]["Q2"] = AGPPrerakQ2;
                            dt.Rows[i]["Q3"] = AGPPrerakQ3;
                            dt.Rows[i]["Q4"] = AGPPrerakQ4;
                        }


                    }
                }
                if (dt.Rows.Count > 0)
                {
                    GV_AnnualPlan.Columns[1].Visible = false;
                    GVMain.DataSource = null;
                    GVMain.DataBind();
                    GV_AnnualPlan.DataSource = dt;
                    GV_AnnualPlan.DataBind();

                }
                else
                {

                    GV_AnnualPlan.DataSource = null;
                    GV_AnnualPlan.DataBind();

                }
            }
        }




    }


    public void UpdateData()
    {

        DataTable dt = (DataTable)Session["GridViewData"];

        for (int i = 0; i < GV_AnnualPlan.Rows.Count; i++)
        {



            Label LblDesc = (Label)GV_AnnualPlan.Rows[i].FindControl("LblDesc");
            TextBox TxtApr = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtApr");
            TextBox TxtMay = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMay");
            TextBox TxtJun = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJun");
            TextBox TxtJul = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJul");
            TextBox TxtTrainingLevel = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtTrainingLevel");
            DataRow[] dr = dt.Select("Activity='" + Convert.ToString(LblDesc.Text) + "'");
            if (dr.Length > 0)
            {
                if (TxtTrainingLevel.Text != "")
                {
                    dr[0]["TrainingLevel"] = TxtTrainingLevel.Text;
                }
                if (TxtApr.Text != "")
                {
                    dr[0]["Q1"] = TxtApr.Text;
                }
                if (TxtMay.Text != "")
                {
                    dr[0]["Q2"] = TxtMay.Text;
                }
                if (TxtJun.Text != "")
                {
                    dr[0]["Q3"] = TxtJun.Text;
                }
                if (TxtJul.Text != "")
                {
                    dr[0]["Q4"] = TxtJul.Text;
                }
                dr[0]["Createby"] = Convert.ToString(Session["username"]);
            }

        }
        Session["GridViewData"] = dt;

    }
    public void SaveTraing()
    {
        UpdateData();
        DataTable dt = (DataTable)Session["GridViewData"];
        dt.Columns.Remove("StartMonth");
        dt.Columns.Remove("EndMonth");
        dt.Columns.Remove("MaxVal");
        dt.Columns.Remove("Q5");
        DataSet dsResult = Insert_Update_tblAnualPlanClusterWiseDetailPage(dt);
        if (dsResult.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved successfully')</script>", false);
            btnSerach_Click(btnSerach, null);

        }
    }
    public void SaveClusterWiseData()
    {
        string FiveYearsOOSG = "";
        string SixYearsOOSG = "";
        string SeventofourteenYearsOOSG = "";
        string FifteentoeighteenYearsOOSG = "";
        string SeventofourteenOOSB = "";
        string Q1GSS = ""; string Q2GSS = ""; string Q3GSS = "";
        string Q4GSS = ""; string Q1MM = ""; string Q2MM = "";
        string Q3MM = ""; string Q4MM = ""; string Balsaba = "";
        string GkpSchool = ""; string Gkp = ""; string Sac1 = "";
        string Sac2 = ""; string Sac3 = ""; string Sac4 = "";
        string RatriChaupal1 = ""; string RatriChaupal2 = ""; string RatriChaupal3 = "";
        string NamankanRaily = ""; string GKPSchools = "";
        string AGPBeneficiariesQ2 = ""; string AGPBeneficiariesQ3 = "";
        string AGPBeneficiariesQ4 = "";
        string PanchayatMeeting1 = ""; string PanchayatMeeting2 = "";
        string AGPPrerakQ3 = ""; string AGPPrerakQ4 = ""; string GKPLpusSchools = "";
        string GKPLben = "";

        for (int i = 0; i < GV_AnnualPlan.Rows.Count; i++)
        {

            #region SavData
            Label LblDesc = (Label)GV_AnnualPlan.Rows[i].FindControl("LblDesc");
            TextBox TxtApr = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtApr");
            TextBox TxtMay = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMay");
            TextBox TxtJun = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJun");
            TextBox TxtJul = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJul");

            if (i == 0)
            {
                FiveYearsOOSG = TxtMay.Text;
                SixYearsOOSG = TxtJun.Text;
            }
            //if (i == 1)
            //{
            //    SixYearsOOSG = TxtApr.Text;
            //}
            //if (i == 2)
            //{
            //    SeventofourteenYearsOOSG = TxtApr.Text;
            //}
            //if (i == 3)
            //{
            //    FifteentoeighteenYearsOOSG = TxtApr.Text;
            //}
            //if (i == 4)
            //{
            //    SeventofourteenOOSB = TxtApr.Text;
            //}
            if (i == 1)
            {
                Q1GSS = TxtApr.Text;
                Q2GSS = TxtMay.Text;
                Q3GSS = TxtJun.Text;
                Q4GSS = TxtJul.Text;
            }
            if (i == 2)
            {
                Q1MM = TxtApr.Text;
                Q2MM = TxtMay.Text;
                Q3MM = TxtJun.Text;
                Q4MM = TxtJul.Text;

            }
            if (i == 3)
            {
                PanchayatMeeting1 = TxtApr.Text;
                PanchayatMeeting2 = TxtMay.Text;


            }
            if (i == 4)
            {
                RatriChaupal1 = TxtApr.Text;
                RatriChaupal2 = TxtMay.Text;
                RatriChaupal3 = TxtJun.Text;


            }
            if (i == 5)
            {
                NamankanRaily = TxtApr.Text;



            }
            if (i == 6)
            {
                Balsaba = TxtApr.Text;

            }
            if (i == 7)
            {
                GkpSchool = TxtApr.Text;

            }
            if (i == 8)
            {
                Gkp = TxtApr.Text;

            }
            if (i == 9)
            {
                GKPLpusSchools = TxtApr.Text;

            }
            if (i == 10)
            {
                GKPLben = TxtApr.Text;

            }

            if (i == 11)
            {
                Sac1 = TxtApr.Text;
                Sac2 = TxtMay.Text;
                Sac3 = TxtJun.Text;
                Sac4 = TxtJul.Text;

            }


            #endregion
        }
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@ClusterCode", ddlPanchayat.SelectedValue),
               new SqlParameter("@FiveYearsOOSG", FiveYearsOOSG),
            new SqlParameter("@SixYearsOOSG", SixYearsOOSG),
            //new SqlParameter("@SeventofourteenYearsOOSG", SeventofourteenYearsOOSG),
            //new SqlParameter("@FifteentoeighteenYearsOOSG", FifteentoeighteenYearsOOSG),
          //  new SqlParameter("@SeventofourteenOOSB", SeventofourteenOOSB),
            new SqlParameter("@Q1GSS", Q1GSS),
            new SqlParameter("@Q2GSS", Q2GSS),
            new SqlParameter("@Q3GSS", Q3GSS),
            new SqlParameter("@Q4GSS", Q4GSS),
            new SqlParameter("@Q1MM", Q1MM),
            new SqlParameter("@Q2MM", Q2MM),
            new SqlParameter("@Q3MM", Q3MM),
            new SqlParameter("@Q4MM", Q4MM),
            new SqlParameter("@Balsaba", Balsaba),
            new SqlParameter("@GkpSchool", GkpSchool),
            new SqlParameter("@Gkp", Gkp),
            new SqlParameter("@Sac1", Sac1),
            new SqlParameter("@Sac2", Sac2),
            new SqlParameter("@Sac3", Sac3),
            new SqlParameter("@Sac4", Sac4),
            new SqlParameter("@PanchayatMeetingQ1", PanchayatMeeting1),
            new SqlParameter("@PanchayatMeetingQ2", PanchayatMeeting2),
            new SqlParameter("@RatriChaupalQ1", RatriChaupal1),
            new SqlParameter("@RatriChaupalQ2", RatriChaupal2),
            new SqlParameter("@RatriChaupalQ3", RatriChaupal3),
           new SqlParameter("@NamankanRailyQ1", NamankanRaily),

            new SqlParameter("@GkpSchoolPlus", GKPLpusSchools),

        new SqlParameter("@GkpSchoolPlusBe", GKPLben),
       
   

            //    new SqlParameter("@CBLVillages", CBLVillages),
                new SqlParameter("@Createby", Convert.ToString(Session["username"])),



        };
        int icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateAnualPlanClusterWiseDetail2025New", cmdParameters);

        if (icount > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved successfully')</script>", false);
            btnSerach_Click(btnSerach, null);

        }
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
        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            // SIPDATA();
            SaveTraing();
        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            // SIPDATA();
            SaveClusterWiseData();
        }
        //if (Convert.ToInt32(ddlYear.SelectedValue) >= 2022)
        //{
        //    SaveData2020();
        //}
        //else
        //{
        //    SaveData();
        //}



    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            bool InsertTS = false;
            if (ddlType.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Plan Type')</script>", false);

                return;

            }
            if (ddlDistrict.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select District')</script>", false);

                return;

            }

            if (Convert.ToInt32(ddlType.SelectedValue) == 2)
            {
                if (ddlPanchayat.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Cluster')</script>", false);

                    return;

                }
                SqlParameter[] cmdParameters = new SqlParameter[]
                          {
                        new SqlParameter("@clustercode", ddlPanchayat.SelectedValue),
                        new SqlParameter("@Flag", "1"),

                          };


                int Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeletetblAnualPlanClusterWiseDetail", cmdParameters);


            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 1)
            {
                SqlParameter[] cmdParameters = new SqlParameter[]
                          {
                        new SqlParameter("@clustercode", ddlPanchayat.SelectedValue),
                        new SqlParameter("@Flag", "1"),

                          };


                int Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeletetblAnualPlanClusterWiseDetail", cmdParameters);

            }
            if (InsertTS == true)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Delete successfully')</script>", false);
                GV_AnnualPlan.DataSource = null;
                GV_AnnualPlan.DataBind();
            }

        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnSumbit_Click(object sender, EventArgs e)
    {
    }
    protected void txtSearchName_Click(object sender, EventArgs e)
    {
    }

    #endregion
    #region Gridview Events
    protected void GVMain_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "GVUIO")
        {
            int iIndex = Convert.ToInt32(e.CommandArgument);
            string SchoolId = Convert.ToString(GVMain.DataKeys[iIndex]["DISECode"]);
            string VillageCode = Convert.ToString(GVMain.DataKeys[iIndex]["VillageCode"]);
            RowNo = Convert.ToString(GVMain.DataKeys[iIndex]["RowNo"]);
            SchoolLeavel = Convert.ToString(GVMain.DataKeys[iIndex]["SchoolLevel"]);
            BalSacha = Convert.ToString(GVMain.DataKeys[iIndex]["BAlVal"]);
            GKP = Convert.ToString(GVMain.DataKeys[iIndex]["GKP"]);
            string GKPLevel = Convert.ToString(GVMain.DataKeys[iIndex]["GKPLevel"]);
            string ManagementType = Convert.ToString(GVMain.DataKeys[iIndex]["ManagementType"]);
            ViewState["SchoolId"] = SchoolId;
            ViewState["VillageCode"] = VillageCode;
            ViewState["RowNo"] = RowNo;
            ViewState["SchoolLeavel"] = SchoolLeavel;
            ViewState["BalSacha"] = BalSacha;

            ViewState["GKP"] = GKP;
            ViewState["GKPLevel"] = GKPLevel;
            ViewState["ManagementType"] = ManagementType;

            LoadData();
            ViewState["Save"] = "Edit";

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
        //  ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "ddlTypeOnChangeEvent()", true);
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
    protected void GV_AnnualPlan_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lb = ((Label)e.Row.FindControl("LblDesc"));
            TextBox TxtApr = ((TextBox)e.Row.FindControl("TxtApr"));
            TextBox TxtMay = ((TextBox)e.Row.FindControl("TxtMay"));
            TextBox TxtJun = ((TextBox)e.Row.FindControl("TxtJun"));
            TextBox TxtJul = ((TextBox)e.Row.FindControl("TxtJul"));

            Label lblStartMonth = ((Label)e.Row.FindControl("lblStartMonth"));
            Label lblEndMonth = ((Label)e.Row.FindControl("lblEndMonth"));


            if (ddlType.SelectedValue == "1")
            {
                if (lb.Text == "Staff Training on Enrolment and SMC")
                {
                    LoadDataEnableTest(TxtApr, TxtMay, TxtJun, TxtJul, 0, Convert.ToInt32(lblEndMonth.Text));
                }
                else
                {
                    LoadDataEnable(TxtApr, TxtMay, TxtJun, TxtJul, Convert.ToInt32(lblStartMonth.Text), Convert.ToInt32(lblEndMonth.Text));
                }


            }
            if (ddlType.SelectedValue == "2")
            {

                LoadDataEnable(TxtApr, TxtMay, TxtJun, TxtJul, Convert.ToInt32(lblStartMonth.Text), Convert.ToInt32(lblEndMonth.Text));



            }
            //else if (ddlType.SelectedValue == "2")
            //{

            //        LoadDataEnable(TxtApr, TxtMay, TxtJun, TxtJul, TxtAug, TxtSep, TxtOct, TxtNov, TxtDec, TxtJan, TxtFeb, TxtMar, Convert.ToInt32(lblStartMonth.Text), Convert.ToInt32(lblEndMonth.Text));


            //}
            //else if (ddlType.SelectedValue == "3")
            //{
            //   if (Convert.ToString(ViewState["BalSacha"]) != "1" && lblPhageFlag.Text == "1")
            //    {
            //    }
            //    else
            //    {
            //        LoadDataEnable(TxtApr, TxtMay, TxtJun, TxtJul, TxtAug, TxtSep, TxtOct, TxtNov, TxtDec, TxtJan, TxtFeb, TxtMar, Convert.ToInt32(lblStartMonth.Text), Convert.ToInt32(lblEndMonth.Text));
            //    }

            //}

        }
    }
    public void LoadDataEnableTest(TextBox TxtApr, TextBox TxtMay, TextBox TxtJun, TextBox TxtJul, int StartMonth, int EndMonth)
    {
        int i = StartMonth;
        for (StartMonth = i; StartMonth <= EndMonth - 1; StartMonth++)
        {

            if (StartMonth == 0)
            {
                TxtApr.Enabled = true;
            }
            if (StartMonth == 1)
            {

                TxtMay.Enabled = false;
            }
            if (StartMonth == 2)
            {
                TxtJun.Enabled = false;
            }
            if (StartMonth == 3)
            {
                TxtJul.Enabled = true;
            }
        }
    }
    public void LEARNIOpenMonth(DataTable dt, Int32 Jun, Int32 Jul, Int32 Aug, Int32 sep)
    {


        for (int i = 0; i < GV_AnnualPlan.Rows.Count; i++)
        {



            Label LblDesc = (Label)GV_AnnualPlan.Rows[i].FindControl("LblDesc");
            TextBox TxtJun = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJun");
            TextBox TxtJul = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJul");
            TextBox TxtAug = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtAug");
            TextBox TxtSep = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtSep");
            TextBox TxtOct = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtOct");
            TextBox TxtNov = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtNov");
            TextBox TxtDec = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtDec");
            TextBox TxtJan = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJan");
            TextBox TxtFeb = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtFeb");
            if (LblDesc.Text == "Learning Baseline for GKP")
            {
                if (Jun > 0)
                {
                    TxtJun.Enabled = true;
                    TxtJul.Enabled = true;
                }
                if (Jul > 0)
                {
                    TxtJul.Enabled = true;
                    TxtAug.Enabled = true;
                }
                if (Aug > 0)
                {
                    TxtAug.Enabled = true;
                    TxtSep.Enabled = true;
                }
                if (sep > 0)
                {
                    TxtOct.Enabled = true;
                    TxtSep.Enabled = true;
                }
            }
        }
    }
    public void LoadDataEnable(TextBox TxtApr, TextBox TxtMay, TextBox TxtJun, TextBox TxtJul, int StartMonth, int EndMonth)
    {
        int i = StartMonth;
        for (StartMonth = i; StartMonth <= EndMonth - 1; StartMonth++)
        {

            if (StartMonth == 0)
            {
                TxtApr.Enabled = true;
            }
            if (StartMonth == 1)
            {

                TxtMay.Enabled = true;
            }
            if (StartMonth == 2)
            {
                TxtJun.Enabled = true;
            }
            if (StartMonth == 3)
            {
                TxtJul.Enabled = true;
            }
        }
    }
    protected void EnableDisableMonth(TextBox TxtApr, TextBox TxtMay, TextBox TxtJun, TextBox TxtJul, TextBox TxtAug, TextBox TxtSep, TextBox TxtOct, TextBox TxtNov, TextBox TxtDec, TextBox TxtJan, TextBox TxtFeb, TextBox TxtMar, bool Apr, bool May, bool Jun, bool Jul, bool Aug, bool Sep, bool Oct, bool Nov, bool Dec, bool Jan, bool Feb, bool Mar)
    {
        TxtApr.Enabled = Apr;
        TxtMay.Enabled = May;
        TxtJun.Enabled = Jun;
        TxtJul.Enabled = Jul;
        TxtAug.Enabled = Aug;
        TxtSep.Enabled = Sep;
        TxtOct.Enabled = Oct;
        TxtNov.Enabled = Nov;
        TxtDec.Enabled = Dec;
        TxtJan.Enabled = Jan;
        TxtFeb.Enabled = Feb;
        TxtMar.Enabled = Mar;
    }
    protected void BalEnableDisableMonth()
    {
        Int32 Aug = 0, Sep = 0, Oct = 0;
        for (int i = 0; i < dtSearchVill.Rows.Count; i++)
        {
            TextBox TxtApr = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtApr");
            TextBox TxtMay = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMay");
            TextBox TxtJun = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJun");
            TextBox TxtJul = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJul");
            TextBox TxtAug = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtAug");
            TextBox TxtSep = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtSep");
            TextBox TxtOct = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtOct");
            TextBox TxtNov = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtNov");
            TextBox TxtDec = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtDec");
            TextBox TxtJan = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJan");
            TextBox TxtFeb = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtFeb");
            TextBox TxtMar = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMar");
            if (dtSearchVill.Rows[i]["Description"].ToString() == "Bal Sabha")
            {

                if (TxtSep.Text != "")
                {
                    Sep = Convert.ToInt32(TxtSep.Text);
                }
                if (TxtAug.Text != "")
                {
                    Aug = Convert.ToInt32(TxtAug.Text);
                }
                if (TxtOct.Text != "")
                {
                    Oct = Convert.ToInt32(TxtOct.Text);
                }
            }

            if (dtSearchVill.Rows[i]["Description"].ToString() == "LSE Sessions")
            {

                if (Sep > 0)
                {

                    TxtSep.Enabled = true;
                    TxtOct.Enabled = true;
                    TxtNov.Enabled = true;
                    TxtDec.Enabled = true;
                    TxtJan.Enabled = true;
                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;


                }
                if (Aug > 0)
                {
                    TxtAug.Enabled = true;
                    TxtSep.Enabled = true;
                    TxtOct.Enabled = true;
                    TxtNov.Enabled = true;
                    TxtDec.Enabled = true;
                    TxtJan.Enabled = true;
                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;
                }
                if (Oct > 0)
                {

                    TxtOct.Enabled = true;
                    TxtNov.Enabled = true;
                    TxtDec.Enabled = true;
                    TxtJan.Enabled = true;
                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;
                }
            }
        }
    }
    protected void GKPEnableDisableMonth()
    {
        Int32 Jul = 0, Aug = 0, Sep = 0, Oct = 0;
        for (int i = 0; i < dtSearchVill.Rows.Count; i++)
        {
            TextBox TxtApr = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtApr");
            TextBox TxtMay = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMay");
            TextBox TxtJun = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJun");
            TextBox TxtJul = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJul");
            TextBox TxtAug = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtAug");
            TextBox TxtSep = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtSep");
            TextBox TxtOct = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtOct");
            TextBox TxtNov = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtNov");
            TextBox TxtDec = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtDec");
            TextBox TxtJan = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtJan");
            TextBox TxtFeb = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtFeb");
            TextBox TxtMar = (TextBox)GV_AnnualPlan.Rows[i].FindControl("TxtMar");
            if (dtSearchVill.Rows[i]["Description"].ToString() == "Learning Baseline")
            {
                if (Convert.ToInt32(TxtJul.Text) > 0)
                {
                    Jul = Convert.ToInt32(TxtJul.Text);
                }
                if (Convert.ToInt32(TxtSep.Text) > 0)
                {
                    Sep = Convert.ToInt32(TxtSep.Text);
                }
                if (Convert.ToInt32(TxtAug.Text) > 0)
                {
                    Aug = Convert.ToInt32(TxtAug.Text);
                }
                if (Convert.ToInt32(TxtOct.Text) > 0)
                {
                    Oct = Convert.ToInt32(TxtOct.Text);
                }
            }

            if (dtSearchVill.Rows[i]["Description"].ToString() == "GKP L0" || dtSearchVill.Rows[i]["Description"].ToString() == "GKP L1" || dtSearchVill.Rows[i]["Description"].ToString() == "GKP L2" || dtSearchVill.Rows[i]["Description"].ToString() == "GKP L3")

            {
                if (Jul > 0)
                {



                    TxtJul.Enabled = true;
                    TxtAug.Enabled = true;
                    TxtSep.Enabled = true;
                    TxtOct.Enabled = true;
                    TxtNov.Enabled = true;
                    TxtDec.Enabled = true;
                    TxtJan.Enabled = true;
                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;
                }
                if (Sep > 0)
                {

                    TxtSep.Enabled = true;
                    TxtOct.Enabled = true;
                    TxtNov.Enabled = true;
                    TxtDec.Enabled = true;
                    TxtJan.Enabled = true;
                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;


                }
                if (Aug > 0)
                {
                    TxtAug.Enabled = true;
                    TxtSep.Enabled = true;
                    TxtOct.Enabled = true;
                    TxtNov.Enabled = true;
                    TxtDec.Enabled = true;
                    TxtJan.Enabled = true;
                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;
                }
                if (Oct > 0)
                {

                    TxtOct.Enabled = true;
                    TxtNov.Enabled = true;
                    TxtDec.Enabled = true;
                    TxtJan.Enabled = true;
                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;
                }
            }
        }
    }

    protected void BalEnableDisableMonth(TextBox TxtApr, TextBox TxtMay, TextBox TxtJun, TextBox TxtJul, TextBox TxtAug, TextBox TxtSep, TextBox TxtOct, TextBox TxtNov, TextBox TxtDec, TextBox TxtJan, TextBox TxtFeb, TextBox TxtMar, bool Apr, bool May, bool Jun, bool Jul, bool Aug, bool Sep, bool Oct, bool Nov, bool Dec, bool Jan, bool Feb, bool Mar)
    {

        if (Convert.ToInt32(TxtJul.Text) > 0)
        {



            TxtJul.Enabled = true;
            TxtAug.Enabled = true;
            TxtSep.Enabled = true;
            TxtOct.Enabled = true;
            TxtNov.Enabled = true;
            TxtDec.Enabled = true;
            TxtJan.Enabled = true;
            TxtFeb.Enabled = true;
            TxtMar.Enabled = true;
        }
        if (Convert.ToInt32(TxtSep.Text) > 0)
        {

            TxtSep.Enabled = true;
            TxtOct.Enabled = true;
            TxtNov.Enabled = true;
            TxtDec.Enabled = true;
            TxtJan.Enabled = true;
            TxtFeb.Enabled = true;
            TxtMar.Enabled = true;

            TxtApr.Enabled = Apr;
            TxtMay.Enabled = May;
            TxtJun.Enabled = Jun;
            TxtJul.Enabled = Jul;
            TxtAug.Enabled = Aug;
            TxtSep.Enabled = Sep;
            TxtOct.Enabled = Oct;
            TxtNov.Enabled = Nov;
            TxtDec.Enabled = Dec;
            TxtJan.Enabled = Jan;
            TxtFeb.Enabled = Feb;
            TxtMar.Enabled = Mar;

        }
        if (Convert.ToInt32(TxtAug.Text) > 0)
        {
            TxtAug.Enabled = true;
            TxtSep.Enabled = true;
            TxtOct.Enabled = true;
            TxtNov.Enabled = true;
            TxtDec.Enabled = true;
            TxtJan.Enabled = true;
            TxtFeb.Enabled = true;
            TxtMar.Enabled = true;
        }


    }
    #endregion
    #region Selected Index Changed Events
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        // pnlMain.Enabled = false;
        // GVMain.Enabled = false;
        FillCBDist();
        GVMain.DataSource = null;
        GVMain.DataBind();
        GV_AnnualPlan.DataSource = null;
        GV_AnnualPlan.DataBind();
        //  ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "ddlTypeOnChangeEvent()", true);

    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {
        }
        else
        {
            Response.Redirect("Login.aspx", false);
        }
        FillCBBock();

        GVMain.DataSource = null;
        GVMain.DataBind();
        GV_AnnualPlan.DataSource = null;
        GV_AnnualPlan.DataBind();
        //  ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "ddlTypeOnChangeEvent()", true);


        SqlParameter[] cmdParameters = new SqlParameter[]
          {

              new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
               new SqlParameter("@Flag", "1"),

          };
        DataSet dtSchool = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadAnnaulPlanApprovalStatus]", cmdParameters);
        DataTable dtCluster = dtSchool.Tables[0];
        DataTable dtTraing = dtSchool.Tables[1];
        btnReject.Visible = false;
        btnsave.Visible = true;
        btnDelete.Visible = true;
        btnSubmitted.Enabled = false;
        btnSubmitted.Visible = true;
        btnUnlock.Visible = false;
        if (dtCluster.Rows.Count > 0 && dtTraing.Rows.Count > 0)
        {
            if (Convert.ToInt32(dtTraing.Rows[0]["ApproveStatus"]) == 3)
            {
                if (Convert.ToString(Session["username"]) == "PMSAdmin" || Convert.ToString(Session["username"]) == "SuperAdmin")
                {
                    btnUnlock.Visible = true;
                }
            }
            if (Convert.ToString(Session["user_level"]) == "39" || Convert.ToString(Session["user_level"]) == "145")
            {
                if (Convert.ToInt32(dtTraing.Rows[0]["ApproveStatus"]) == 0)
                {
                    // btnSubmitted.Text = "Submitted to DOL";
                    btnSubmitted.Enabled = true;

                    LinkButton1.Visible = true;
                    FileUpload1.Visible = true;
                    btnsave.Visible = true;
                    btnDelete.Visible = true;
                }
                else if (Convert.ToInt32(dtTraing.Rows[0]["ApproveStatus"]) > 0)
                {
                    btnSubmitted.Enabled = false;

                    LinkButton1.Visible = false;
                    FileUpload1.Visible = false;
                    btnsave.Visible = false;
                    btnDelete.Visible = false;
                }

                if (Convert.ToInt32(dtTraing.Rows[0]["ApproveStatus"]) == 1)
                {

                    btnSubmitted.Text = " Submitted to DOL";


                }
                if (Convert.ToInt32(dtTraing.Rows[0]["ApproveStatus"]) == 2)
                {
                    btnSubmitted.Text = "Submitted to SOL";
                }
                if (Convert.ToInt32(dtTraing.Rows[0]["ApproveStatus"]) == 3)
                {
                    btnSubmitted.Text = "Approved";
                }

            }
            if (Convert.ToString(Session["user_level"]) == "91")
            {

                LinkButton1.Visible = false;
                FileUpload1.Visible = false;
                btnsave.Visible = false;
                btnDelete.Visible = false;
                if (Convert.ToInt32(dtTraing.Rows[0]["ApproveStatus"]) == 0)
                {
                    btnSubmitted.Visible = false;
                }
                if (Convert.ToInt32(dtTraing.Rows[0]["ApproveStatus"]) == 1)
                {
                    btnSubmitted.Enabled = true;
                    btnSubmitted.Text = "Submit to SOL";

                    btnReject.Visible = true;
                }
                if (Convert.ToInt32(dtTraing.Rows[0]["ApproveStatus"]) == 2)
                {
                    btnSubmitted.Text = "Submitted to SOL";
                }
                if (Convert.ToInt32(dtTraing.Rows[0]["ApproveStatus"]) == 3)
                {
                    btnSubmitted.Text = "Approved";
                }
            }
            if (Convert.ToString(Session["user_level"]) == "92")
            {
                if (Convert.ToInt32(dtTraing.Rows[0]["ApproveStatus"]) == 0)
                {
                    btnSubmitted.Visible = false;
                }
                LinkButton1.Visible = false;
                FileUpload1.Visible = false;
                btnsave.Visible = false;
                btnDelete.Visible = false;
                if (Convert.ToInt32(dtTraing.Rows[0]["ApproveStatus"]) == 2)
                {
                    btnSubmitted.Enabled = true;

                    btnReject.Visible = true;
                }
                if (Convert.ToInt32(dtTraing.Rows[0]["ApproveStatus"]) == 1)
                {
                    btnSubmitted.Text = "Submitted to DOL";
                }
                if (Convert.ToInt32(dtTraing.Rows[0]["ApproveStatus"]) == 2)
                {
                    btnSubmitted.Text = "Approve";
                }
                if (Convert.ToInt32(dtTraing.Rows[0]["ApproveStatus"]) == 3)
                {
                    btnSubmitted.Text = "Approved";
                }
            }
            if (Convert.ToInt32(dtTraing.Rows[0]["ApproveStatus"]) > 0)
            {
                btnsave.Visible = false;
                btnDelete.Visible = false;

                LinkButton1.Visible = false;
                FileUpload1.Visible = false;
            }


        }
        //if (ddlType.SelectedValue == "1")
        //{

        //    int ff = DateTime.Today.Month;
        //    if (ff == 7 || ff == 8 || ff == 9)
        //    {
        //        btnReject.Visible = false;
        //        btnsave.Visible = true;
        //        btnDelete.Visible = true;
        //        btnSubmitted.Enabled = false;
        //        btnSubmitted.Visible = true;
        //        btnUnlock.Visible = false;

        //        if (Convert.ToString(Session["user_level"]) == "39")
        //        {
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q2Approval"]) == 0)
        //            {
        //                // btnSubmitted.Text = "Submitted to DOL";
        //                btnSubmitted.Enabled = true;

        //                LinkButton1.Visible = false;
        //                FileUpload1.Visible = false;
        //                btnsave.Visible = true;
        //                btnDelete.Visible = true;
        //            }
        //            else if (Convert.ToInt32(dtTraing.Rows[0]["Q2Approval"]) > 0)
        //            {
        //                btnSubmitted.Enabled = false;

        //                LinkButton1.Visible = false;
        //                FileUpload1.Visible = false;
        //                btnsave.Visible = false;
        //                btnDelete.Visible = false;
        //            }

        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q2Approval"]) == 1)
        //            {

        //                btnSubmitted.Text = " Submitted to DOL";


        //            }
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q2Approval"]) == 2)
        //            {
        //                btnSubmitted.Text = "Submitted to SOL";
        //            }
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q2Approval"]) == 3)
        //            {
        //                btnSubmitted.Text = "Approved";
        //            }

        //        }
        //        if (Convert.ToString(Session["user_level"]) == "91")
        //        {

        //            LinkButton1.Visible = false;
        //            FileUpload1.Visible = false;
        //            btnsave.Visible = false;
        //            btnDelete.Visible = false;
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q2Approval"]) == 0)
        //            {
        //                btnSubmitted.Visible = false;
        //            }
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q2Approval"]) == 1)
        //            {
        //                btnSubmitted.Enabled = true;
        //                btnSubmitted.Text = "Submit to SOL";

        //                btnReject.Visible = true;
        //            }
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q2Approval"]) == 2)
        //            {
        //                btnSubmitted.Text = "Submitted to SOL";
        //            }
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q2Approval"]) == 3)
        //            {
        //                btnSubmitted.Text = "Approved";
        //            }
        //        }
        //        if (Convert.ToString(Session["user_level"]) == "92")
        //        {
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q2Approval"]) == 0)
        //            {
        //                btnSubmitted.Visible = false;
        //            }
        //            LinkButton1.Visible = false;
        //            FileUpload1.Visible = false;
        //            btnsave.Visible = false;
        //            btnDelete.Visible = false;
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q2Approval"]) == 2)
        //            {
        //                btnSubmitted.Enabled = true;

        //                btnReject.Visible = true;
        //            }
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q2Approval"]) == 1)
        //            {
        //                btnSubmitted.Text = "Submitted to DOL";
        //            }
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q2Approval"]) == 2)
        //            {
        //                btnSubmitted.Text = "Approve";
        //            }
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q2Approval"]) == 3)
        //            {
        //                btnSubmitted.Text = "Approved";
        //            }
        //        }
        //    }

        //    if (ff == 10 || ff == 11 || ff == 12)
        //    {
        //        btnReject.Visible = false;
        //        btnsave.Visible = true;
        //        btnDelete.Visible = true;
        //        btnSubmitted.Enabled = false;
        //        btnSubmitted.Visible = true;
        //        btnUnlock.Visible = false;

        //        if (Convert.ToString(Session["user_level"]) == "39")
        //        {
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q3Approval"]) == 0)
        //            {
        //                // btnSubmitted.Text = "Submitted to DOL";
        //                btnSubmitted.Enabled = true;

        //                LinkButton1.Visible = false;
        //                FileUpload1.Visible = false;
        //                btnsave.Visible = true;
        //                btnDelete.Visible = true;
        //            }
        //            else if (Convert.ToInt32(dtTraing.Rows[0]["Q3Approval"]) > 0)
        //            {
        //                btnSubmitted.Enabled = false;

        //                LinkButton1.Visible = false;
        //                FileUpload1.Visible = false;
        //                btnsave.Visible = false;
        //                btnDelete.Visible = false;
        //            }

        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q3Approval"]) == 1)
        //            {

        //                btnSubmitted.Text = " Submitted to DOL";


        //            }
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q3Approval"]) == 2)
        //            {
        //                btnSubmitted.Text = "Submitted to SOL";
        //            }
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q3Approval"]) == 3)
        //            {
        //                btnSubmitted.Text = "Approved";
        //            }

        //        }
        //        if (Convert.ToString(Session["user_level"]) == "91")
        //        {

        //            LinkButton1.Visible = false;
        //            FileUpload1.Visible = false;
        //            btnsave.Visible = false;
        //            btnDelete.Visible = false;
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q3Approval"]) == 0)
        //            {
        //                btnSubmitted.Visible = false;
        //            }
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q3Approval"]) == 1)
        //            {
        //                btnSubmitted.Enabled = true;
        //                btnSubmitted.Text = "Submit to SOL";

        //                btnReject.Visible = true;
        //            }
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q3Approval"]) == 2)
        //            {
        //                btnSubmitted.Text = "Submitted to SOL";
        //            }
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q3Approval"]) == 3)
        //            {
        //                btnSubmitted.Text = "Approved";
        //            }
        //        }
        //        if (Convert.ToString(Session["user_level"]) == "92")
        //        {
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q3Approval"]) == 0)
        //            {
        //                btnSubmitted.Visible = false;
        //            }
        //            LinkButton1.Visible = false;
        //            FileUpload1.Visible = false;
        //            btnsave.Visible = false;
        //            btnDelete.Visible = false;
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q3Approval"]) == 2)
        //            {
        //                btnSubmitted.Enabled = true;

        //                btnReject.Visible = true;
        //            }
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q3Approval"]) == 1)
        //            {
        //                btnSubmitted.Text = "Submitted to DOL";
        //            }
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q3Approval"]) == 2)
        //            {
        //                btnSubmitted.Text = "Approve";
        //            }
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q3Approval"]) == 3)
        //            {
        //                btnSubmitted.Text = "Approved";
        //            }
        //        }
        //    }
        //    if (ff == 1 || ff == 2 || ff == 3)
        //    {
        //        btnReject.Visible = false;
        //        btnsave.Visible = true;
        //        btnDelete.Visible = true;
        //        btnSubmitted.Enabled = false;
        //        btnSubmitted.Visible = true;
        //        btnUnlock.Visible = false;

        //        if (Convert.ToString(Session["user_level"]) == "39")
        //        {
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q4Approval"]) == 0)
        //            {
        //                // btnSubmitted.Text = "Submitted to DOL";
        //                btnSubmitted.Enabled = true;

        //                LinkButton1.Visible = false;
        //                FileUpload1.Visible = false;
        //                btnsave.Visible = true;
        //                btnDelete.Visible = true;
        //            }
        //            else if (Convert.ToInt32(dtTraing.Rows[0]["Q4Approval"]) > 0)
        //            {
        //                btnSubmitted.Enabled = false;

        //                LinkButton1.Visible = false;
        //                FileUpload1.Visible = false;
        //                btnsave.Visible = false;
        //                btnDelete.Visible = false;
        //            }

        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q4Approval"]) == 1)
        //            {

        //                btnSubmitted.Text = " Submitted to DOL";


        //            }
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q4Approval"]) == 2)
        //            {
        //                btnSubmitted.Text = "Submitted to SOL";
        //            }
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q4Approval"]) == 3)
        //            {
        //                btnSubmitted.Text = "Approved";
        //            }

        //        }
        //        if (Convert.ToString(Session["user_level"]) == "91")
        //        {

        //            LinkButton1.Visible = false;
        //            FileUpload1.Visible = false;
        //            btnsave.Visible = false;
        //            btnDelete.Visible = false;
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q4Approval"]) == 0)
        //            {
        //                btnSubmitted.Visible = false;
        //            }
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q4Approval"]) == 1)
        //            {
        //                btnSubmitted.Enabled = true;
        //                btnSubmitted.Text = "Submit to SOL";

        //                btnReject.Visible = true;
        //            }
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q4Approval"]) == 2)
        //            {
        //                btnSubmitted.Text = "Submitted to SOL";
        //            }
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q4Approval"]) == 3)
        //            {
        //                btnSubmitted.Text = "Approved";
        //            }
        //        }
        //        if (Convert.ToString(Session["user_level"]) == "92")
        //        {
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q4Approval"]) == 0)
        //            {
        //                btnSubmitted.Visible = false;
        //            }
        //            LinkButton1.Visible = false;
        //            FileUpload1.Visible = false;
        //            btnsave.Visible = false;
        //            btnDelete.Visible = false;
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q4Approval"]) == 2)
        //            {
        //                btnSubmitted.Enabled = true;

        //                btnReject.Visible = true;
        //            }
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q4Approval"]) == 1)
        //            {
        //                btnSubmitted.Text = "Submitted to DOL";
        //            }
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q4Approval"]) == 2)
        //            {
        //                btnSubmitted.Text = "Approve";
        //            }
        //            if (Convert.ToInt32(dtTraing.Rows[0]["Q4Approval"]) == 3)
        //            {
        //                btnSubmitted.Text = "Approved";
        //            }
        //        }
        //    }
        //}
        Locking();
    }
    protected void ddlSubType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GV_AnnualPlan.DataSource = null;
        GV_AnnualPlan.DataBind();
        GVMain.DataSource = null;
        GVMain.DataBind();
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            divBlock.Attributes.Add("style", "display:none");
            divPhy.Attributes.Add("style", "display:none");
            divVill.Attributes.Add("style", "display:none");
            lblMsg.Visible = false;
            GV_AnnualPlan.DataSource = null;
            GV_AnnualPlan.DataBind();
            GVMain.DataSource = null;
            GVMain.DataBind();

            objComman.BindDLL("mstMasterAnnaulPlan", "LookupType,Description  as Description ", "LookupFlag='APLD' and ActiveStatus=1 ", "LookupType", "asc", ddlsubType, "Description", "LookupType", "--All--");

            divSub.Visible = true;
        }
        else if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            lblMsg.Visible = false;
            divBlock.Attributes.Add("style", "display:block");
            divPhy.Attributes.Add("style", "display:block");
            divVill.Attributes.Add("style", "display:none");
            GV_AnnualPlan.DataSource = null;
            GV_AnnualPlan.DataBind();
            GVMain.DataSource = null;
            GVMain.DataBind();
            objComman.BindDLL("mstMasterAnnaulPlan", "LookupType,Description  as Description ", "LookupFlag='APLV' and ActiveStatus=1 ", "LookupType", "asc", ddlsubType, "Description", "LookupType", "--All--");
            divSub.Visible = true;
        }
        else if (Convert.ToInt32(ddlType.SelectedValue) == 3)
        {
            lblMsg.Visible = false;
            divBlock.Attributes.Add("style", "display:block");
            divPhy.Attributes.Add("style", "display:block");
            divVill.Attributes.Add("style", "display:block");
            GV_AnnualPlan.DataSource = null;
            GV_AnnualPlan.DataBind();
            GVMain.DataSource = null;
            GVMain.DataBind();
            objComman.BindDLL("mstMasterAnnaulPlan", "LookupType,Description  as Description ", "LookupFlag='APLeS' and ActiveStatus=1 ", "LookupType", "asc", ddlsubType, "Description", "LookupType", "--All--");
            divSub.Visible = false;
        }

        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2022)
        {
            divSub.Visible = false;
        }
        else
        {
            divSub.Visible = true;
        }

    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBCluster();
        GVMain.DataSource = null;
        GVMain.DataBind();
        GV_AnnualPlan.DataSource = null;
        GV_AnnualPlan.DataBind();
        // ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "ddlTypeOnChangeEvent()", true);
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillage();
        GVMain.DataSource = null;
        GVMain.DataBind();
        GV_AnnualPlan.DataSource = null;
        GV_AnnualPlan.DataBind();
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "ddlTypeOnChangeEvent()", true);
    }
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "ddlTypeOnChangeEvent()", true);
        // FillSchool();
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            ddlState.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlDistrict, null);
            if (Session["user_level_Role"].ToString() == "3" || Session["user_level_Role"].ToString() == "4")
            {
                ddlDistrict.SelectedIndex = 1;
                ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
            }
            if (Convert.ToInt32(ddlYear.SelectedValue) >= 2022)
            {
                divSub.Visible = false;
            }
            else
            {
                divSub.Visible = true;
            }

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
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "ddlTypeOnChangeEvent()", true);
    }

    #endregion
    #region Save




    #endregion

    protected void AnnalPlanExcel_Hotspot(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {
        }
        else
        {
            Response.Redirect("Login.aspx", false);
        }
        ViewState["1"] = 710;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2024)
        {
            LoadAnnualDataDeatilsHotSpot2024(1);
        }
        else
        {
            LoadAnnualDataDeatilsHotSpot(1);
        }



    }
    public void LoadAnnualDataDeatilsHotSpot2024(int Flag)
    {
        string conditions = "";

        if (ddlDistrict.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select District')</script>", false);

            return;

        }

        string condition = string.Empty;

        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "    where mstCluster.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlState.SelectedIndex > 0)
        {
            conditions += " and mstCluster.StateCode in('" + ddlState.SelectedValue + "') ";

        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " and mstCluster.DistrictCode in('" + ddlDistrict.SelectedValue + "') ";

        }

        //if (ddlBlock.SelectedIndex > 0)
        //{

        //    conditions += " and mstCluster.BlockCode in('" + ddlBlock.SelectedValue + "') ";


        //}


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con", conditions),
             new SqlParameter("@DistictName", ddlDistrict.SelectedItem.Text),
              new SqlParameter("@DistictCode", ddlDistrict.SelectedValue),


        };
        DataSet dt = null;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
        {
            dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnnaualPlanDataSummry20252026Deatail]", cmdParameters);
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2025)
        {
            dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnnaualPlanDataSummry2025Deatail]", cmdParameters);
        }
        else
        {
            dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnnaualPlanDataSummry20242025]", cmdParameters);


        }
        // DataTable dt = objMain.LoadAnnaulPlanRowData(conditions, Flag);


        ViewState["SAC"] = dt;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2025)
        {
            if (dt.Tables[0].Rows.Count > 0)
            {
                MultipuExeclTrack2025();
            }
        }
        else
        {
            if (dt.Tables[0].Rows.Count > 0)
            {
                MultipuExeclTrack2024();
            }
        }

    }
    public void LoadAnnualDataDeatilsHotSpot(int Flag)
    {
        string conditions = "";

        if (ddlDistrict.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select District')</script>", false);

            return;

        }

        string condition = string.Empty;

        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "    where mstCluster.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlState.SelectedIndex > 0)
        {
            conditions += " and mstCluster.StateCode in('" + ddlState.SelectedValue + "') ";

        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " and mstCluster.DistrictCode in('" + ddlDistrict.SelectedValue + "') ";

        }

        //if (ddlBlock.SelectedIndex > 0)
        //{

        //    conditions += " and mstCluster.BlockCode in('" + ddlBlock.SelectedValue + "') ";


        //}


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con", conditions),
             new SqlParameter("@DistictName", ddlDistrict.SelectedItem.Text),
              new SqlParameter("@DistictCode", ddlDistrict.SelectedValue),


        };

        DataSet dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnnaualPlanDataSummryDownload]", cmdParameters);
        // DataTable dt = objMain.LoadAnnaulPlanRowData(conditions, Flag);







        ViewState["SAC"] = dt;
        if (dt.Tables[0].Rows.Count > 0)
        {
            MultipuExeclTrack();
        }





    }
    public void MultipuExeclTrack2025()
    {
        DataSet dt5 = ViewState["SAC"] as DataSet;
        string StartupPath = Server.MapPath("~/Export");
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\AnnalPlanExcel2025.xlsx");
        var ws = wb.Worksheet(1);
        var ws1 = wb.Worksheet(2);
        //var ws1 = wb.Worksheet(2);
        //var ws3 = wb.Worksheet(3);

        //dt.Columns.Remove("rownNO");
        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("rownNO");
        DataTable dt = dt5.Tables[0];
        ws.Cell(5, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 4;
        string str = "A5:BF" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        DataTable dt1 = dt5.Tables[1];
        ws1.Cell(5, 1).InsertData(dt1.Rows);
        Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 4;
        string str1 = "A5:H" + ii1;
        ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);




        filepath = StartupPath + "\\AnnualPlanExcel " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
    public void MultipuExeclTrack2024()
    {
        DataSet dt5 = ViewState["SAC"] as DataSet;
        string StartupPath = Server.MapPath("~/Export");
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\AnnalPlanExcel2024.xlsx");
        var ws = wb.Worksheet(1);
        var ws1 = wb.Worksheet(2);
        //var ws1 = wb.Worksheet(2);
        //var ws3 = wb.Worksheet(3);

        //dt.Columns.Remove("rownNO");
        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("rownNO");
        DataTable dt = dt5.Tables[0];
        ws.Cell(5, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 4;
        string str = "A5:BQ" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        DataTable dt1 = dt5.Tables[1];
        ws1.Cell(5, 1).InsertData(dt1.Rows);
        Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 4;
        string str1 = "A5:H" + ii1;
        ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);




        filepath = StartupPath + "\\AnnualPlanExcel " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
        DataSet dt5 = ViewState["SAC"] as DataSet;
        string StartupPath = Server.MapPath("~/Export");
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\AnnalPlanExcel.xlsx");
        var ws = wb.Worksheet(1);
        var ws1 = wb.Worksheet(2);
        //var ws1 = wb.Worksheet(2);
        //var ws3 = wb.Worksheet(3);

        //dt.Columns.Remove("rownNO");
        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("rownNO");
        DataTable dt = dt5.Tables[0];
        ws.Cell(5, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 4;
        string str = "A5:BV" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        DataTable dt1 = dt5.Tables[1];
        ws1.Cell(5, 1).InsertData(dt1.Rows);
        Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 4;
        string str1 = "A5:H" + ii1;
        ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);




        filepath = StartupPath + "\\AnnualPlanExcel " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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

        if (ddlDistrict.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select District')</script>", false);

            return;

        }
        if (Convert.ToString(Session["username"]) != "")
        {
        }
        else
        {
            Response.Redirect("Login.aspx", false);
        }
        GenerateExcelData2024();
    }
    public void MultipuExeclTrackError(DataTable dt, DataTable dtTraing)
    {
        try
        {
            string StartupPath = Server.MapPath("~/Export");
            string filepath = "";
            XLWorkbook wb = new XLWorkbook();
            wb = new XLWorkbook(StartupPath + "\\AnnalPlanExcel2025.xlsx");
            var ws = wb.Worksheet(1);
            var ws1 = wb.Worksheet(2);
            //var ws1 = wb.Worksheet(2);
            //var ws3 = wb.Worksheet(3);

            //dt.Columns.Remove("rownNO");
            //DataTable dt1 = dtMain1.Tables[1];

            //dt1.Columns.Remove("rownNO");
            if (dt.Rows.Count > 0)
            {
                ws.Cell(5, 1).InsertData(dt.Rows);
                Int32 ii = Convert.ToInt32(dt.Rows.Count) + 4;
                string str = "A5:BE" + ii;
                ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
                ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

                ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
                ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
                DataTable dtSchool = Session["CheckTarget"] as DataTable;
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    int I6 = 0;
                    int I5 = 0;
                    int I7 = 0;
                    int IB6 = 0;
                    int IB18 = 0;
                    int TotalI7TO14 = 0;
                    int I7TO14 = 0;
                    for (int i = 15; i < dt.Columns.Count; i++)
                    {
                        string clusterCode = Convert.ToString(dt.Rows[r][9]);
                        string GPB = Convert.ToString(dt.Rows[r][48]);
                        int GKPPlus = 0;
                        DataRow[] dr = dtSchool.Select("EGClusterCode='" + clusterCode + "'");
                        if (dr.Length > 0)
                        {
                            if (ddlDistrict.SelectedItem.Text == "F96DE2E6E3A14373BD68848F4" && ddlDistrict.SelectedItem.Text == "5D6D65B0CF1B488B87D2AE872")
                            {
                                I7TO14 = Convert.ToInt32(dr[0]["i14"]) + Convert.ToInt32(dr[0]["i6"]);
                            }
                            else
                            {
                                I7TO14 = Convert.ToInt32(dr[0]["i14"]);
                            }

                        }



                        string icoff = Convert.ToString(dt.Rows[r][i]);
                        //if (i == 15 || i == 16)
                        //{
                        //    if (icoff != "")
                        //    {
                        //        I6 += Convert.ToInt32(icoff);
                        //    }

                        //}
                        //if (i == 17 || i == 18)
                        //{
                        //    if (icoff != "")
                        //    {
                        //        I7 += Convert.ToInt32(icoff);
                        //    }

                        //}
                        //if (i == 20 || i == 21)
                        //{
                        //    if (icoff != "")
                        //    {
                        //        IB6 += Convert.ToInt32(icoff);
                        //    }

                        //}
                        //if (i == 22)
                        //{
                        //    if (icoff != "")
                        //    {
                        //        IB18 += Convert.ToInt32(icoff);
                        //    }

                        //}

                        if (i == 16 || i == 17 || i == 18 || i == 19 || i == 20 || i == 49 || i == 21 || i == 22 || i == 23 || i == 24 || i == 25 || i == 26 || i == 27 || i == 28 || i == 29 || i == 30 || i == 31 || i == 45 || i == 46 || i == 47 || i == 48 || i == 52 || i == 53 || i == 54 || i == 55 || i == 56 || i == 57)
                        {
                            string stValue = Convert.ToString(dt.Rows[r][i]);
                            #region All Cluster
                            string st = stValue.Trim();
                            if (st.Length > 0)
                            {
                                if (st.All(char.IsDigit))
                                {
                                }
                                else
                                {
                                    ws.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                                }
                            }

                            if (i == 16)
                            {
                                if (st != "")
                                {
                                    if (st.All(char.IsDigit))
                                    {
                                        TotalI7TO14 = Convert.ToInt32(st);
                                        //if (I5 < Convert.ToInt32(st))
                                        //{
                                        //    ws.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                                        //}
                                    }
                                }
                            }
                            if (i == 17)
                            {
                                if (st != "")
                                {
                                    if (st.All(char.IsDigit))
                                    {
                                        TotalI7TO14 += Convert.ToInt32(st);
                                        if (I7TO14 < Convert.ToInt32(TotalI7TO14))
                                        {
                                            ws.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                                        }
                                    }
                                }
                            }



                            ///Target
                            if (i == 16 || i == 17)

                            {
                                if (st.Length > 0)
                                {
                                    if (st.Length > 3)
                                    {
                                        ws.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                                    }
                                }


                            }
                            ///GSS
                            if (i == 18 || i == 19 || i == 20 || i == 21)

                            {
                                if (st.Length > 0)
                                {
                                    if (st.Length > 1)
                                    {
                                        ws.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                                    }
                                }
                            }
                            //MM
                            if (i == 22 || i == 23 || i == 24 || i == 25)
                            {
                                if (st.Length > 0)
                                {
                                    if (st.Length > 2)
                                    {
                                        ws.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                                    }
                                }
                            }

                            ///#Panchayat Meeting#Ratri Chaupal#Namankan Raily
                            if (i == 26 || i == 27 || i == 28 || i == 29 || i == 30 || i == 31)

                            {
                                if (st.Length > 0)
                                {
                                    if (st.Length > 1)
                                    {
                                        ws.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                                    }
                                }
                            }


                            //GKP
                            if (i == 45 || i == 46)
                            {
                                if (st.Length > 0)
                                {
                                    if (st.Length > 2)
                                    {
                                        ws.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                                    }
                                }
                            }
                            if (i == 47)
                            {
                                if (st.Length > 0)
                                {
                                    if (st.Length > 3)
                                    {
                                        ws.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                                    }
                                }
                            }
                            //GKP plus
                            if (i == 48)
                            {
                                if (st.Length > 0)
                                {
                                    if (Convert.ToInt32(st) > 6)
                                    {
                                        ws.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                                    }
                                }
                            }

                            if (i == 49)
                            {
                                if (st.Length > 0)
                                {
                                    if (GPB.Length > 0)
                                    {
                                        int gk = Convert.ToInt32(GPB) * 100;
                                        if (gk <= Convert.ToInt32(st))
                                        {
                                            ws.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                                        }
                                        else
                                        {

                                        }
                                    }
                                    else
                                    {
                                        ws.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;

                                    }
                                }
                            }
                            //SMC
                            if (i == 54 || i == 55 || i == 56 || i == 57)
                            {
                                if (st.Length > 0)
                                {
                                    if (st.Length > 2)
                                    {
                                        ws.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                                    }
                                }
                            }


                            //if (i == 57 || i == 58 || i == 59 || i == 60 || i == 61 || i == 62 || i == 63 || i == 64)
                            //{
                            //    if (st.Length > 0)
                            //    {
                            //        if (st.Length > 2)
                            //        {
                            //            ws.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                            //        }
                            //    }
                            //}
                            //if (i == 65 || i == 66 || i == 67 || i == 68)
                            //{
                            //    if (st.Length > 0)
                            //    {
                            //        if (st.Length > 4)
                            //        {
                            //            ws.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                            //        }
                            //    }
                            //}
                            #endregion

                        }

                    }
                }
            }
            if (dtTraing.Rows.Count > 0)
            {
                ws1.Cell(5, 1).InsertData(dtTraing.Rows);
                Int32 ii1 = Convert.ToInt32(dtTraing.Rows.Count) + 4;
                string str1 = "A5:H" + ii1;
                ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
                ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

                ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
                ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
                for (int r = 0; r < dtTraing.Rows.Count; r++)
                {
                    string Activity = Convert.ToString(dtTraing.Rows[r][2]);
                    for (int i = 3; i < dtTraing.Columns.Count; i++)
                    {
                        string stValue = Convert.ToString(dtTraing.Rows[r][i]);
                        #region All block
                        string st = stValue.Trim();
                        if (st.Length > 0)
                        {
                            if (st.All(char.IsDigit))
                            {
                            }
                            else
                            {
                                ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                            }
                        }
                        if (i == 3)
                        {
                            if (st == "1" || st == "2" || st == "3" || st == "" || st == "0")
                            {

                            }
                            else
                            {
                                ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                            }
                        }
                        if (Activity == "Staff Training on Enrolment and SMC" && (i == 5 || i == 6))
                        {
                            if (st.Length > 0 && st != "0")
                            {
                                ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                            }
                        }
                        if (Activity == "Staff Training on Enrolment and SMC" && (i == 7 || i == 4))
                        {
                            if (st.Length > 3)
                            {
                                ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                            }
                        }
                        if (Activity == "Staff Training on CV and SC" && (i == 7 || i == 5 || i == 6))
                        {
                            if (st.Length > 0 && st != "0")
                            {
                                ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                            }
                        }
                        if (Activity == "Staff Training on CV and SC" && i == 4)
                        {
                            if (st.Length > 3)
                            {
                                ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                            }
                        }
                        if ((Activity == "Staff Training on CV and SC" || Activity == "Staff Training on PMS" || Activity == "Staff Training on Learning Baseline" || Activity == "Staff Training on GKP-L0/L1" || Activity == "Staff Training on GKP-L1/L2" || Activity == "Staff Training on GKP-L2/L3" || Activity == "Staff Training on Bal Sabha and LSE" || Activity == "Staff Training on SMC Meeting" || Activity == "Staff training on D2D refresher" || Activity == "TB Alumni one day orientation" || Activity == "CG/MT Training on Enrollment & SMC" || Activity == "CG/Master Trainers Training on L0/L1" || Activity == "CG/Master Trainers Training on L1/L2" || Activity == "CG/Master Trainers Training on L2/L3" || Activity == "CG/MT Training on Bal Sabha & LSE") && (i == 4 || i == 5 || i == 6 || i == 7))
                        {
                            if (st.Length > 3)
                            {
                                ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                            }
                        }
                        if (Activity == "Staff Training on Learning Baseline" && (i == 7 || i == 6))
                        {
                            if (st.Length > 0 && st != "0")
                            {
                                ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                            }
                        }
                        if (Activity == "Staff Training on Learning Baseline" && (i == 4 || i == 5))
                        {
                            if (st.Length > 3)
                            {
                                ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                            }
                        }
                        //if (Activity == "Staff Training on Learning Endline" && (i == 7 || i == 6 || i == 4 || i == 5))
                        //{
                        //    if (st.Length > 0 && st != "0")
                        //    {
                        //        ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                        //    }
                        //}
                        if (Activity == "Staff Training on Bal Sabha and LSE" && (i == 7))
                        {
                            if (st.Length > 0 && st != "0")
                            {
                                ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                            }
                        }
                        if (Activity == "Staff Training on Bal Sabha and LSE" && (i == 4 || i == 5 || i == 6))
                        {
                            if (st.Length > 3)
                            {
                                ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                            }
                        }
                        if ((Activity == "TB Training on Enrolment & SMC" || Activity == "TB Training on GKP-L0/L1" || Activity == "TB Training on GKP-L1/L2" || Activity == "TB Training on GKP-L2/L3" || Activity == "TB One Day Orientation" || Activity == "TB Skill Training Volunteer Engagement (VE)" || Activity == "Staff training on GKP Plus part-I" || Activity == "Staff training on GKP Plus part-II" || Activity == "Any Additional Training" || Activity == "KGBV Teachers training on LSE") && (i == 4 || i == 5 || i == 6 || i == 7))
                        {
                            if (st.Length > 4)
                            {
                                ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                            }
                        }


                        if (Activity == "TB Training on Enrolment & SMC" && (i == 7 || i == 6 || i == 5))
                        {
                            if (st.Length > 0 && st != "0")
                            {
                                ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                            }
                        }
                        if (Activity == "TB Training on Enrolment & SMC" && (i == 4))
                        {
                            if (st.Length > 4)
                            {
                                ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                            }
                        }
                        if ((Activity == "TB Training on Bal Sabha and LSE" || Activity == "TB Training on Camp Vidya") && (i == 7))
                        {
                            if (st.Length > 0 && st != "0")
                            {
                                ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                            }
                        }
                        if ((Activity == "TB Training on Bal Sabha and LSE" || Activity == "TB Training on Camp Vidya") && (i == 4 || i == 6 || i == 5))
                        {
                            if (st.Length > 4)
                            {
                                ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                            }
                        }
                        if ((Activity == "TB orientation on EG annual Plan") && (i == 7 || i == 6 || i == 5))
                        {
                            if (st.Length > 0 && st != "0")
                            {
                                ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                            }
                        }
                        if (Activity == "TB orientation on EG annual Plan" && (i == 4))
                        {
                            if (st.Length > 3)
                            {
                                ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                            }
                        }
                        if ((Activity == "TB Alumni one day orientation" || Activity == "CG/MT Training on Enrollment & SMC" || Activity == "CG/Master Trainers Training on L0/L1" || Activity == "CG/Master Trainers Training on L1/L2" || Activity == "CG/Master Trainers Training on L2/L3" || Activity == "CG/MT Training on Bal Sabha & LSE") && (i == 4 || i == 5 || i == 6 || i == 7))
                        {
                            if (st.Length > 3)
                            {
                                ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                            }
                        }


                        #endregion
                    }
                    //for (int i = 3; i < dtTraing.Columns.Count; i++)
                    //{
                    //    string stValue = Convert.ToString(dtTraing.Rows[r][i]);
                    //    #region All blck
                    //    string st = stValue.Trim();
                    //    if (st.Length > 0)
                    //    {
                    //        if (st.All(char.IsDigit))
                    //        {
                    //        }
                    //        else
                    //        {
                    //            ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                    //        }
                    //    }
                    //    if (i == 3)
                    //    {
                    //        if (st == "1" || st == "2" || st == "3" || st == "" || st == "0")
                    //        {

                    //        }else
                    //        {
                    //            ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                    //        }
                    //    }
                    //    if (r == 0 && (i == 4 || i == 5 || i == 6))
                    //    {
                    //        if (st.Length > 0 && st != "0")
                    //        {
                    //            ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                    //        }
                    //    }
                    //    if (r == 0 && i == 7)
                    //    {
                    //        if (st.Length > 3)
                    //        {
                    //            ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                    //        }
                    //    }
                    //    if (r == 1 && (i == 7 || i == 5 || i == 6))
                    //    {
                    //        if (st.Length > 0 && st != "0")
                    //        {
                    //            ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                    //        }
                    //    }
                    //    if (r == 1 && i == 4)
                    //    {
                    //        if (st.Length > 3)
                    //        {
                    //            ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                    //        }
                    //    }
                    //    if ((r == 2 || r == 4 || r == 5 || r == 6 || r == 8 || r == 10 || r == 11 || r == 12 || r == 24) && (i == 4 || i == 5 || i == 6 || i == 7))
                    //    {
                    //        if (st.Length > 3)
                    //        {
                    //            ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                    //        }
                    //    }
                    //    if (r == 3 && (i == 7 || i == 6))
                    //    {
                    //        if (st.Length > 0 && st != "0")
                    //        {
                    //            ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                    //        }
                    //    }
                    //    if (r == 3 && (i == 4 || i == 5))
                    //    {
                    //        if (st.Length > 3)
                    //        {
                    //            ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                    //        }
                    //    }
                    //    if (r == 9 && (i == 7 || i == 6))
                    //    {
                    //        if (st.Length > 0 && st != "0")
                    //        {
                    //            ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                    //        }
                    //    }
                    //    if (r == 7 && (i == 7 || i == 6 || i == 4 || i == 5))
                    //    {
                    //        if (st.Length > 0 && st != "0")
                    //        {
                    //            ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                    //        }
                    //    }

                    //    if (r == 9 && (i == 4 || i == 5))
                    //    {
                    //        if (st.Length > 3)
                    //        {

                    //        }
                    //    }

                    //    if ((r == 13 || r == 14 || r == 15 || r == 16 || r == 17 || r == 18 || r == 19 || r == 20) && (i == 4 || i == 5 || i == 6 || i == 7))
                    //    {
                    //        if (st.Length > 4)
                    //        {
                    //            ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                    //        }
                    //    }


                    //    if (r == 13 && (i == 7 || i == 6 || i == 5))
                    //    {
                    //        if (st.Length > 0 && st != "0")
                    //        {
                    //            ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                    //        }
                    //    }
                    //    if (r == 13 && (i == 4))
                    //    {
                    //        if (st.Length > 4)
                    //        {
                    //            ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                    //        }
                    //    }
                    //    if ((r == 21 || r == 22) && (i == 7))
                    //    {
                    //        if (st.Length > 0 && st != "0")
                    //        {
                    //            ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                    //        }
                    //    }
                    //    if ((r == 21 || r == 22) && (i == 4 || i == 6 || i == 5))
                    //    {
                    //        if (st.Length > 4)
                    //        {
                    //            ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                    //        }
                    //    }
                    //    if ((r == 23) && (i == 7 || i == 6 || i == 5))
                    //    {
                    //        if (st.Length > 0 && st != "0")
                    //        {
                    //            ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                    //        }
                    //    }
                    //    if (r == 23 && (i == 4))
                    //    {
                    //        if (st.Length > 3)
                    //        {
                    //            ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                    //        }
                    //    }
                    //    if ((r == 24 || r == 25 || r == 26 || r == 27 || r == 28) && (i == 4 || i == 5 || i == 6 || i == 7))
                    //    {
                    //        if (st.Length > 3)
                    //        {
                    //            ws1.Cell(r + 5, i + 1).Style.Fill.BackgroundColor = XLColor.Red;
                    //        }
                    //    }
                    //    #endregion
                    //}
                }
            }
            filepath = StartupPath + "\\AnnalPlanExcel " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
        catch
        {
            throw;
        }
        // need to catch possible exceptions

    }
    private void GenerateExcelData2024()
    {
        OleDbConnection oledbConn = new OleDbConnection();
        try
        {
            // need to pass relative path after deploying on server
            string path = System.IO.Path.GetFullPath(Server.MapPath(FileUpload1.FileName));
            /* connection string  to work with excel file. HDR=Yes - indicates 
               that the first row contains columnnames, not data. HDR=No - indicates 
               the opposite. "IMEX=1;" tells the driver to always read "intermixed" 
               (numbers, dates, strings etc) data columns as text. 
            Note that this option might affect excel sheet write access negative. */
            string sDirectory = Server.MapPath("~/Mou//");

            bool res = false;
            string FilePath = sDirectory + FileUpload1.FileName;
            FileUpload1.PostedFile.SaveAs(FilePath);
            ViewState["FileName"] = FileUpload1.FileName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");

            // instance a memory stream and pass the

            if (Path.GetExtension(path) == ".xls")
            {

                oledbConn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=Excel 4.0;Persist Security Info=False;");
            }
            else if (Path.GetExtension(path) == ".xlsx")
            {

                oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=Excel 8.0;Persist Security Info=False;");
            }
            else
            {

            }

            oledbConn.Open();
            OleDbCommand cmd = new OleDbCommand(); ;

            DataTable dtCluster = new DataTable();
            DataTable dtTraing = new DataTable();
            //string conString = ConfigurationManager.ConnectionStrings["Exl07Con"].ConnectionString; ;
            //DataTable dtExcelDataCheck = null;
            // conString = string.Format(conString, FilePath);
            //using (OleDbConnection ex_con = new OleDbConnection(conString))
            //{
            //    ex_con.Open();
            //    for (int n = 0; n < ex_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows.Count; n++)
            //    {ter

            //        string sheet1 = ex_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[n]["TABLE_NAME"].ToString();
            //        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] ", ex_con))
            //        {
            //            oda.Fill(dtExcelDataCheck);
            //        }

            //    }
            //}
            // string Q = "SELECT Sno,StateName,StateCode,DistrictName,DistrictCode,BlockName,BlockCode,EGBlock,EGBlockCode,GramPanchyat,GP_CODE,ClusterName,ClusterCode,VillageName,VillageCode,SchoolName,GOVTDISECODE,DISECODE,Operational_NON_Operational,Management,SchoolType  FROM [JHALAWAR DATA$]";
            try
            {
                string Q = "SELECT * FROM [Cluster Level Planning$]";
                OleDbDataAdapter oleda = new OleDbDataAdapter(Q, oledbConn);
                oleda.Fill(dtCluster);

                string Q1 = "SELECT * FROM [Training Planning$]";
                OleDbDataAdapter oleda1 = new OleDbDataAdapter(Q1, oledbConn);
                oleda1.Fill(dtTraing);

            }
            catch
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invaild Format')</script>", false);

                return;
            }

            SqlParameter[] cmdParameters = new SqlParameter[]
            {

              new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),


            };
            DataTable dtSchool = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[loadDIstrinctAnnaulPlanClusterWise2025]", cmdParameters);

            Session["CheckTarget"] = dtSchool;

            int ik = 0;
            foreach (DataRow recRow in dtCluster.Rows)
            {
                if (ik == 0)
                {
                    recRow[0] = string.Empty;
                    recRow.Delete();
                }
                if (ik == 1)
                {
                    recRow[1] = string.Empty;
                    recRow.Delete();
                }
                if (ik == 2)
                {
                    recRow[2] = string.Empty;
                    recRow.Delete();
                }
                if (ik == 3)
                {
                    break;
                }

                ik = ik + 1;


            }

            int ik1 = 0;
            foreach (DataRow recRow in dtTraing.Rows)
            {
                if (ik1 == 0)
                {
                    recRow[0] = string.Empty;
                    recRow.Delete();
                }
                if (ik1 == 1)
                {
                    recRow[1] = string.Empty;
                    recRow.Delete();
                }
                if (ik1 == 2)
                {
                    recRow[2] = string.Empty;
                    recRow.Delete();
                }
                if (ik1 == 3)
                {
                    break;
                }

                ik1 = ik1 + 1;
            }
            dtCluster.AcceptChanges();

            dtTraing.AcceptChanges();
            int ClusterCount = dtCluster.Columns.Count;
            int trainincoutn = dtTraing.Columns.Count;
            if (ClusterCount == 58)
            {

            }
            else
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invaild Format')</script>", false);

                return;

            }
            if (trainincoutn == 8)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invaild Format')</script>", false);

                return;

            }

            //DataTable dt = ds.Tables[0];
            bool TempFlag = false;
            bool TempFlagC = false;
            bool TempFlagT = false;
            dtCluster.Columns.Add("TempID", System.Type.GetType("System.String"));
            dtTraing.Columns.Add("TempID", System.Type.GetType("System.String"));

            if (Convert.ToString(dtTraing.Rows[0][0]).Trim().ToLower() != ddlDistrict.SelectedItem.Text.Trim().ToLower())
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select vaild District ')</script>", false);

                return;
            }

            for (int r = 0; r < dtCluster.Rows.Count; r++)
            {
                TempFlagC = false;
                int I5 = 0;
                int I6 = 0;
                int I7 = 0;
                int IB6 = 0;
                int IB18 = 0;
                int I7TO14 = 0;
                int TotalI7TO14 = 0;
                string clusterCode = Convert.ToString(dtCluster.Rows[r][9]);
                string GPB = Convert.ToString(dtCluster.Rows[r][48]);

                int GKPPlus = 0;
                DataRow[] dr = dtSchool.Select("EGClusterCode='" + clusterCode + "'");
                if (dr.Length > 0)
                {
                    //I5 = Convert.ToInt32(dr[0]["i5"]);
                    //I6 = Convert.ToInt32(dr[0]["i6"]);
                    //I7 = Convert.ToInt32(dr[0]["i14"]);
                    if (ddlDistrict.SelectedValue == "F96DE2E6E3A14373BD68848F4" || ddlDistrict.SelectedValue == "5D6D65B0CF1B488B87D2AE872")
                    {
                        I7TO14 = Convert.ToInt32(dr[0]["i14"]) + Convert.ToInt32(dr[0]["i6"]);
                    }
                    else
                    {
                        I7TO14 = Convert.ToInt32(dr[0]["i14"]);
                    }
                    //IB18 = Convert.ToInt32(dr[0]["iB18"]);
                }
                for (int i = 9; i < dtCluster.Columns.Count; i++)
                {
                    string icoff = Convert.ToString(dtCluster.Rows[r][i]);

                    if (i == 16 || i == 17 || i == 18 || i == 19 || i == 20 || i == 21 || i == 22 || i == 23 || i == 24 || i == 25 || i == 26 || i == 27 || i == 28 || i == 29 || i == 30 || i == 31 || i == 45 || i == 46 || i == 47 || i == 48 || i == 49 || i == 52 || i == 53 || i == 54 || i == 55 || i == 56 || i == 57)

                    //if (i == 23 || i == 24 || i == 25 || i == 26 || i == 27 || i == 28 || i == 29 || i == 30 || i == 31 || i == 32 || i == 33 || i == 34 || i == 35 || i == 36 || i == 48 || i == 49 || i == 50 || i == 51 || i == 58 || i == 59 || i == 60 || i == 61 || i == 62 || i == 63 || i == 64 || i == 65 || i == 66 || i == 67 || i == 68 || i == 69 || i == 70 || i == 71 || i == 72 || i == 73 || i == 74)
                    {
                        string stValue = Convert.ToString(dtCluster.Rows[r][i]);
                        #region All Cluster
                        string st = stValue.Trim();
                        if (st.Length > 0)
                        {
                            if (st.All(char.IsDigit))
                            {
                            }
                            else
                            {
                                TempFlag = true;
                                TempFlagC = true;
                            }
                        }
                        if (i == 16)
                        {
                            if (st != "")
                            {
                                if (st.All(char.IsDigit))
                                {
                                    TotalI7TO14 = Convert.ToInt32(st);
                                    //if (I5 < Convert.ToInt32(st))
                                    //{
                                    //    TempFlag = true;
                                    //    TempFlagC = true;
                                    //}
                                }
                            }
                        }


                        if (i == 17)
                        {
                            if (st != "")
                            {
                                if (st.All(char.IsDigit))
                                {
                                    TotalI7TO14 += Convert.ToInt32(st);
                                    if (I7TO14 < Convert.ToInt32(TotalI7TO14))
                                    {
                                        TempFlag = true;
                                        TempFlagC = true;
                                    }
                                }
                            }
                        }

                        if (i == 16 || i == 17)
                        {
                            if (st.Length > 0)
                            {
                                if (st.Length > 3)
                                {
                                    TempFlag = true;
                                    TempFlagC = true;
                                }
                            }


                        }
                        ///GSS
                        if (i == 18 || i == 19 || i == 20 || i == 21)

                        {
                            if (st.Length > 0)
                            {
                                if (st.Length > 1)
                                {
                                    TempFlag = true;
                                    TempFlagC = true;
                                }
                            }
                        }
                        //MM
                        if (i == 22 || i == 23 || i == 24 || i == 25)
                        {
                            if (st.Length > 0)
                            {
                                if (st.Length > 2)
                                {
                                    TempFlag = true;
                                    TempFlagC = true;
                                }
                            }
                        }
                        //GKP
                        if (i == 45 || i == 43 || i == 44)
                        {
                            if (st.Length > 0)
                            {
                                if (st.Length > 3)
                                {
                                    TempFlag = true;
                                    TempFlagC = true;
                                }
                            }
                        }

                        if (i == 26 || i == 27 || i == 28 || i == 29 || i == 30 || i == 31)

                        {
                            if (st.Length > 0)
                            {
                                if (st.Length > 1)
                                {
                                    TempFlag = true;
                                    TempFlagC = true;
                                }
                            }
                        }


                        //GKP
                        if (i == 45 || i == 46)
                        {
                            if (st.Length > 0)
                            {
                                if (st.Length > 2)
                                {
                                    TempFlag = true;
                                    TempFlagC = true;
                                }
                            }
                        }
                        if (i == 47)
                        {
                            if (st.Length > 0)
                            {
                                if (st.Length > 3)
                                {
                                    TempFlag = true;
                                    TempFlagC = true;
                                }
                            }
                        }
                        //GKP plus
                        if (i == 48)
                        {
                            if (st.Length > 0)
                            {
                                if (Convert.ToInt32(st) > 6)
                                {
                                    GKPPlus = Convert.ToInt32(st);
                                    TempFlag = true;
                                    TempFlagC = true;
                                }
                            }
                        }
                        if (i == 49)
                        {
                            if (st.Length > 0)
                            {
                                if (GPB.Length > 0)
                                {
                                    int gk = Convert.ToInt32(GPB) * 100;
                                    if (gk < Convert.ToInt32(st))
                                    {
                                        TempFlag = true;
                                        TempFlagC = true;
                                    }
                                    else
                                    {

                                    }
                                }
                                else
                                {

                                    TempFlag = true;
                                    TempFlagC = true;
                                }
                            }
                        }
                        //  SMC

                        if (i == 54 || i == 55 || i == 56 || i == 57)
                        {
                            if (st.Length > 0)
                            {
                                if (st.Length > 2)
                                {
                                    TempFlag = true;
                                    TempFlagC = true;
                                }
                            }
                        }
                        //if (i == 57 || i == 58 || i == 59 || i == 60 || i == 61 || i == 62 || i == 63 || i == 64)
                        //{
                        //    if (st.Length > 0)
                        //    {
                        //        if (st.Length > 2)
                        //        {
                        //            TempFlag = true;
                        //            TempFlagC = true;
                        //        }
                        //    }
                        //}

                        //if (i == 65 || i == 66 || i == 67 || i == 68)
                        //{
                        //    if (st.Length > 0)
                        //    {
                        //        if (st.Length > 4)
                        //        {
                        //            TempFlag = true;
                        //            TempFlagC = true;
                        //        }
                        //    }
                        //}
                        #endregion

                    }

                }
                if (TempFlagC == true)
                {
                    dtCluster.Rows[r]["TempID"] = "1";
                }
            }
            for (int r = 0; r < dtTraing.Rows.Count; r++)
            {
                TempFlagT = false;
                string Activity = Convert.ToString(dtTraing.Rows[r][2]);
                for (int i = 3; i < dtTraing.Columns.Count; i++)
                {
                    string stValue = Convert.ToString(dtTraing.Rows[r][i]);
                    #region All block
                    string st = stValue.Trim();
                    if (st.Length > 0)
                    {
                        if (st.All(char.IsDigit))
                        {
                        }
                        else
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if (i == 3)
                    {
                        if (st == "1" || st == "2" || st == "3" || st == "" || st == "0")
                        {

                        }
                        else
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if (Activity == "Staff Training on Enrolment and SMC" && (i == 5 || i == 6))
                    {
                        if (st.Length > 0 && st != "0")
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if (Activity == "Staff Training on Enrolment and SMC" && (i == 7 || i == 4))
                    {
                        if (st.Length > 3)
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if (Activity == "Staff Training on CV and SC" && (i == 7 || i == 5 || i == 6))
                    {
                        if (st.Length > 0 && st != "0")
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if (Activity == "Staff Training on CV and SC" && i == 4)
                    {
                        if (st.Length > 3)
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if ((Activity == "Staff Training on CV and SC" || Activity == "Staff Training on PMS" || Activity == "Staff Training on Learning Baseline" || Activity == "Staff Training on GKP-L0/L1" || Activity == "Staff Training on GKP-L1/L2" || Activity == "Staff Training on GKP-L2/L3" || Activity == "Staff Training on Bal Sabha and LSE" || Activity == "Staff Training on SMC Meeting" || Activity == "Staff training on D2D refresher" || Activity == "TB Alumni one day orientation" || Activity == "CG/MT Training on Enrollment & SMC" || Activity == "CG/Master Trainers Training on L0/L1" || Activity == "CG/Master Trainers Training on L1/L2" || Activity == "CG/Master Trainers Training on L2/L3" || Activity == "CG/MT Training on Bal Sabha & LSE") && (i == 4 || i == 5 || i == 6 || i == 7))
                    {
                        if (st.Length > 3)
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if (Activity == "Staff Training on Learning Baseline" && (i == 7 || i == 6))
                    {
                        if (st.Length > 0 && st != "0")
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if (Activity == "Staff Training on Learning Baseline" && (i == 4 || i == 5))
                    {
                        if (st.Length > 3)
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    //if (Activity == "Staff Training on Learning Endline" && (i == 7 || i == 6 || i == 4 || i == 5))
                    //{
                    //    if (st.Length > 0 && st != "0")
                    //    {
                    //        TempFlag = true;
                    //        TempFlagT = true;
                    //    }
                    //}
                    if (Activity == "Staff Training on Bal Sabha and LSE" && (i == 7))
                    {
                        if (st.Length > 0 && st != "0")
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if (Activity == "Staff Training on Bal Sabha and LSE" && (i == 4 || i == 5 || i == 6))
                    {
                        if (st.Length > 3)
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if ((Activity == "TB Training on Enrolment & SMC" || Activity == "TB Training on GKP-L0/L1" || Activity == "TB Training on GKP-L1/L2" || Activity == "TB Training on GKP-L2/L3" || Activity == "TB One Day Orientation" || Activity == "TB Skill Training Volunteer Engagement (VE)" || Activity == "Staff training on GKP Plus part-I" || Activity == "Staff training on GKP Plus part-II" || Activity == "Any Additional Training" || Activity == "KGBV Teachers training on LSE") && (i == 4 || i == 5 || i == 6 || i == 7))
                    {
                        if (st.Length > 4)
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }


                    if (Activity == "TB Training on Enrolment & SMC" && (i == 7 || i == 6 || i == 5))
                    {
                        if (st.Length > 0 && st != "0")
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if (Activity == "TB Training on Enrolment & SMC" && (i == 4))
                    {
                        if (st.Length > 4)
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if ((Activity == "TB Training on Bal Sabha and LSE" || Activity == "TB Training on Camp Vidya") && (i == 7))
                    {
                        if (st.Length > 0 && st != "0")
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if ((Activity == "TB Training on Bal Sabha and LSE" || Activity == "TB Training on Camp Vidya") && (i == 4 || i == 6 || i == 5))
                    {
                        if (st.Length > 4)
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if ((Activity == "TB orientation on EG annual Plan") && (i == 7 || i == 6 || i == 5))
                    {
                        if (st.Length > 0 && st != "0")
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if (Activity == "TB orientation on EG annual Plan" && (i == 4))
                    {
                        if (st.Length > 3)
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if ((Activity == "TB Alumni one day orientation" || Activity == "CG/MT Training on Enrollment & SMC" || Activity == "CG/Master Trainers Training on L0/L1" || Activity == "CG/Master Trainers Training on L1/L2" || Activity == "CG/Master Trainers Training on L2/L3" || Activity == "CG/MT Training on Bal Sabha & LSE") && (i == 4 || i == 5 || i == 6 || i == 7))
                    {
                        if (st.Length > 3)
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }


                    #endregion
                }
                if (TempFlagT == true)
                {
                    dtTraing.Rows[r]["TempID"] = "1";
                }
            }
            if (TempFlag == true)
            {
                string name = "";
                foreach (DataRow tableRow in dtCluster.Rows)
                {
                    if (tableRow["TempID"].ToString().Equals(name))
                        tableRow.Delete();
                }
                dtCluster.AcceptChanges();

                //foreach (DataRow tableRow in dtTraing.Rows)
                //{
                //    if (tableRow["TempID"].ToString().Equals(name))
                //        tableRow.Delete();
                //}

                //dtTraing.AcceptChanges();

                dtCluster.Columns.Remove("TempID");
                dtTraing.Columns.Remove("TempID");
                MultipuExeclTrackError(dtCluster, dtTraing);
                return;
            }
            else
            {
                dtCluster.Columns.Remove("TempID");
                dtTraing.Columns.Remove("TempID");
                if (dtCluster.Rows.Count > 0 || dtTraing.Rows.Count > 0)
                {
                    SavaDataCluster24(dtCluster, dtTraing);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('No Record Found')</script>", false);

                }
            }


        }

        // need to catch possible exceptions
        catch
        {


        }
        finally
        {
            oledbConn.Close();
        }
    }
    private void GenerateExcelData()
    {
        OleDbConnection oledbConn = new OleDbConnection();
        try
        {
            // need to pass relative path after deploying on server
            string path = System.IO.Path.GetFullPath(Server.MapPath(FileUpload1.FileName));
            /* connection string  to work with excel file. HDR=Yes - indicates 
               that the first row contains columnnames, not data. HDR=No - indicates 
               the opposite. "IMEX=1;" tells the driver to always read "intermixed" 
               (numbers, dates, strings etc) data columns as text. 
            Note that this option might affect excel sheet write access negative. */
            string sDirectory = Server.MapPath("~/Mou//");

            bool res = false;
            string FilePath = sDirectory + FileUpload1.FileName;
            FileUpload1.PostedFile.SaveAs(FilePath);
            ViewState["FileName"] = FileUpload1.FileName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");

            // instance a memory stream and pass the

            if (Path.GetExtension(path) == ".xls")
            {

                oledbConn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=Excel 4.0;Persist Security Info=False;");
            }
            else if (Path.GetExtension(path) == ".xlsx")
            {

                oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=Excel 8.0;Persist Security Info=False;");
            }
            else
            {

            }

            oledbConn.Open();
            OleDbCommand cmd = new OleDbCommand(); ;

            DataTable dtCluster = new DataTable();
            DataTable dtTraing = new DataTable();
            //string conString = ConfigurationManager.ConnectionStrings["Exl07Con"].ConnectionString; ;
            //DataTable dtExcelDataCheck = null;
            // conString = string.Format(conString, FilePath);
            //using (OleDbConnection ex_con = new OleDbConnection(conString))
            //{
            //    ex_con.Open();
            //    for (int n = 0; n < ex_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows.Count; n++)
            //    {ter

            //        string sheet1 = ex_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[n]["TABLE_NAME"].ToString();
            //        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] ", ex_con))
            //        {
            //            oda.Fill(dtExcelDataCheck);
            //        }

            //    }
            //}
            // string Q = "SELECT Sno,StateName,StateCode,DistrictName,DistrictCode,BlockName,BlockCode,EGBlock,EGBlockCode,GramPanchyat,GP_CODE,ClusterName,ClusterCode,VillageName,VillageCode,SchoolName,GOVTDISECODE,DISECODE,Operational_NON_Operational,Management,SchoolType  FROM [JHALAWAR DATA$]";
            try
            {
                string Q = "SELECT * FROM [Cluster Level Planning$]";
                OleDbDataAdapter oleda = new OleDbDataAdapter(Q, oledbConn);
                oleda.Fill(dtCluster);

                string Q1 = "SELECT * FROM [Training Planning$]";
                OleDbDataAdapter oleda1 = new OleDbDataAdapter(Q1, oledbConn);
                oleda1.Fill(dtTraing);

            }
            catch
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invaild Format')</script>", false);

                return;
            }

            SqlParameter[] cmdParameters = new SqlParameter[]
            {

              new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),


            };
            DataTable dtSchool = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadDIstrinctAnnaulPlanClusterWise]", cmdParameters);

            Session["CheckTarget"] = dtSchool;

            int ik = 0;
            foreach (DataRow recRow in dtCluster.Rows)
            {
                if (ik == 0)
                {
                    recRow[0] = string.Empty;
                    recRow.Delete();
                }
                if (ik == 1)
                {
                    recRow[1] = string.Empty;
                    recRow.Delete();
                }
                if (ik == 2)
                {
                    recRow[2] = string.Empty;
                    recRow.Delete();
                }
                if (ik == 3)
                {
                    break;
                }

                ik = ik + 1;


            }

            int ik1 = 0;
            foreach (DataRow recRow in dtTraing.Rows)
            {
                if (ik1 == 0)
                {
                    recRow[0] = string.Empty;
                    recRow.Delete();
                }
                if (ik1 == 1)
                {
                    recRow[1] = string.Empty;
                    recRow.Delete();
                }
                if (ik1 == 2)
                {
                    recRow[2] = string.Empty;
                    recRow.Delete();
                }
                if (ik1 == 3)
                {
                    break;
                }

                ik1 = ik1 + 1;
            }
            dtCluster.AcceptChanges();

            dtTraing.AcceptChanges();
            int ClusterCount = dtCluster.Columns.Count;
            int trainincoutn = dtTraing.Columns.Count;
            if (ClusterCount == 54)
            {

            }
            else
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invaild Format')</script>", false);

                return;

            }
            if (trainincoutn == 8)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invaild Format')</script>", false);

                return;

            }

            //DataTable dt = ds.Tables[0];
            bool TempFlag = false;
            bool TempFlagC = false;
            bool TempFlagT = false;
            dtCluster.Columns.Add("TempID", System.Type.GetType("System.String"));
            dtTraing.Columns.Add("TempID", System.Type.GetType("System.String"));

            if (Convert.ToString(dtTraing.Rows[0][0]).Trim().ToLower() != ddlDistrict.SelectedItem.Text.Trim().ToLower())
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select vaild District ')</script>", false);

                return;
            }

            for (int r = 0; r < dtCluster.Rows.Count; r++)
            {
                TempFlagC = false;
                int I6 = 0;
                int I7 = 0;
                int IB6 = 0;
                int IB18 = 0;
                string clusterCode = Convert.ToString(dtCluster.Rows[r][7]);

                DataRow[] dr = dtSchool.Select("EGClusterCode='" + clusterCode + "'");
                if (dr.Length > 0)
                {
                    I6 = Convert.ToInt32(dr[0]["i6"]);
                    I7 = Convert.ToInt32(dr[0]["i14"]);
                    IB6 = Convert.ToInt32(dr[0]["iB6"]);
                    IB18 = Convert.ToInt32(dr[0]["iB18"]);
                }
                for (int i = 15; i < dtCluster.Columns.Count; i++)
                {
                    string icoff = Convert.ToString(dtCluster.Rows[r][i]);
                    //if (i == 15 || i == 16)
                    //{
                    //    if (icoff != "")
                    //    {
                    //        I6 += Convert.ToInt32(icoff);
                    //    }

                    //}
                    //if (i == 17 || i == 18)
                    //{
                    //    if (icoff != "")
                    //    {
                    //        I7 += Convert.ToInt32(icoff);
                    //    }

                    //}
                    //if (i == 20 || i == 21)
                    //{
                    //    if (icoff != "")
                    //    {
                    //        IB6 += Convert.ToInt32(icoff);
                    //    }

                    //}
                    //if (i == 22 )
                    //{
                    //    if (icoff != "")
                    //    {
                    //        IB18 += Convert.ToInt32(icoff);
                    //    }

                    //}
                    if (i == 23 || i == 24 || i == 25 || i == 26 || i == 27 || i == 28 || i == 29 || i == 30 || i == 31 || i == 32 || i == 33 || i == 34 || i == 35 || i == 36 || i == 48 || i == 49 || i == 50 || i == 51 || i == 58 || i == 59 || i == 60 || i == 61 || i == 62 || i == 63 || i == 64 || i == 65 || i == 66 || i == 67 || i == 68 || i == 69 || i == 70 || i == 71 || i == 72 || i == 73 || i == 74)
                    {
                        string stValue = Convert.ToString(dtCluster.Rows[r][i]);
                        #region All Cluster
                        string st = stValue.Trim();
                        if (st.Length > 0)
                        {
                            if (st.All(char.IsDigit))
                            {
                            }
                            else
                            {
                                TempFlag = true;
                                TempFlagC = true;
                            }
                        }
                        if (i == 23)
                        {
                            if (st != "")
                            {
                                if (st.All(char.IsDigit))
                                {
                                    if (I6 < Convert.ToInt32(st))
                                    {
                                        TempFlag = true;
                                        TempFlagC = true;
                                    }
                                }
                            }
                        }


                        if (i == 24)
                        {
                            if (st != "")
                            {
                                if (st.All(char.IsDigit))
                                {
                                    if (I7 < Convert.ToInt32(st))
                                    {
                                        TempFlag = true;
                                        TempFlagC = true;
                                    }
                                }
                            }
                        }
                        if (i == 25)
                        {
                            if (st != "")
                            {
                                if (st.All(char.IsDigit))
                                {
                                    if (IB18 < Convert.ToInt32(st))
                                    {
                                        TempFlag = true;
                                        TempFlagC = true;
                                    }
                                }
                            }
                        }
                        if (i == 26)
                        {
                            if (st != "")
                            {
                                if (st.All(char.IsDigit))
                                {
                                    if (IB6 < Convert.ToInt32(st))
                                    {
                                        TempFlag = true;
                                        TempFlagC = true;
                                    }
                                }
                            }
                        }
                        ///Target
                        if (i == 23 || i == 24 || i == 25 || i == 26)
                        {
                            if (st.Length > 0)
                            {
                                if (st.Length > 3)
                                {
                                    TempFlag = true;
                                    TempFlagC = true;
                                }
                            }


                        }
                        ///GSS
                        if (i == 27 || i == 28 || i == 29 || i == 30)
                        {
                            if (st.Length > 0)
                            {
                                if (st.Length > 1)
                                {
                                    TempFlag = true;
                                    TempFlagC = true;
                                }
                            }
                        }
                        //MM
                        if (i == 31 || i == 32 || i == 33 || i == 34 || i == 35)
                        {
                            if (st.Length > 0)
                            {
                                if (st.Length > 2)
                                {
                                    TempFlag = true;
                                    TempFlagC = true;
                                }
                            }
                        }
                        //GKP
                        if (i == 48 || i == 49 || i == 50)
                        {
                            if (st.Length > 0)
                            {
                                if (st.Length > 3)
                                {
                                    TempFlag = true;
                                    TempFlagC = true;
                                }
                            }
                        }
                        //SMC
                        if (i == 58 || i == 59 || i == 60 || i == 61)
                        {
                            if (st.Length > 0)
                            {
                                if (st.Length > 2)
                                {
                                    TempFlag = true;
                                    TempFlagC = true;
                                }
                            }
                        }
                        if (i == 62 || i == 63 || i == 64 || i == 65 || i == 70 || i == 71 || i == 72 || i == 73)
                        {
                            if (st.Length > 0)
                            {
                                if (st.Length > 2)
                                {
                                    TempFlag = true;
                                    TempFlagC = true;
                                }
                            }
                        }
                        if (i == 66 || i == 67 || i == 68 || i == 69)
                        {
                            if (st.Length > 0)
                            {
                                if (st.Length > 4)
                                {
                                    TempFlag = true;
                                    TempFlagC = true;
                                }
                            }
                        }
                        #endregion

                    }

                }
                if (TempFlagC == true)
                {
                    dtCluster.Rows[r]["TempID"] = "1";
                }
            }
            for (int r = 0; r < dtTraing.Rows.Count; r++)
            {
                TempFlagT = false;
                string Activity = Convert.ToString(dtTraing.Rows[r][2]);
                for (int i = 3; i < dtTraing.Columns.Count; i++)
                {
                    string stValue = Convert.ToString(dtTraing.Rows[r][i]);
                    #region All block
                    string st = stValue.Trim();
                    if (st.Length > 0)
                    {
                        if (st.All(char.IsDigit))
                        {
                        }
                        else
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if (i == 3)
                    {
                        if (st == "1" || st == "2" || st == "3" || st == "" || st == "0")
                        {

                        }
                        else
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if (Activity == "Staff Training on Enrolment and SMC" && (i == 4 || i == 5 || i == 6))
                    {
                        if (st.Length > 0 && st != "0")
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if (Activity == "Staff Training on Enrolment and SMC" && i == 7)
                    {
                        if (st.Length > 3)
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if (Activity == "Staff Training on CV and SC" && (i == 7 || i == 5 || i == 6))
                    {
                        if (st.Length > 0 && st != "0")
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if (Activity == "Staff Training on CV and SC" && i == 4)
                    {
                        if (st.Length > 3)
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if ((Activity == "Staff Training on PMS" || Activity == "Staff Training on GKP-L0/L1" || Activity == "Staff Training on GKP-L1/L2" || Activity == "Staff Training on GKP-L2/L3" || Activity == "Staff Training on Soft Skills" || Activity == "Staff Training on SMC Meeting" || Activity == "Staff training on D2D refresher" || Activity == "AGP Prerak Training on AGP Camp" || Activity == "TB Alumni one day orientation") && (i == 4 || i == 5 || i == 6 || i == 7))
                    {
                        if (st.Length > 3)
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if (Activity == "Staff Training on Learning Baseline" && (i == 7 || i == 6))
                    {
                        if (st.Length > 0 && st != "0")
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if (Activity == "Staff Training on Learning Baseline" && (i == 4 || i == 5))
                    {
                        if (st.Length > 3)
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    //if (Activity == "Staff Training on Learning Endline" && (i == 7 || i == 6 || i == 4 || i == 5))
                    //{
                    //    if (st.Length > 0 && st != "0")
                    //    {
                    //        TempFlag = true;
                    //        TempFlagT = true;
                    //    }
                    //}
                    if (Activity == "Staff Training on Bal Sabha and LSE" && (i == 7))
                    {
                        if (st.Length > 0 && st != "0")
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if (Activity == "Staff Training on Bal Sabha and LSE" && (i == 4 || i == 5 || i == 6))
                    {
                        if (st.Length > 3)
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if ((Activity == "TB Training on Enrolment & SMC" || Activity == "TB PRI Training" || Activity == "TB Training on GKP-L1/L2" || Activity == "TB Training on GKP-L2/L3" || Activity == "TB Training on GKP-L0/L1" || Activity == "TB Skills training orientation" || Activity == "TB One Day Orientation") && (i == 4 || i == 5 || i == 6 || i == 7))
                    {
                        if (st.Length > 4)
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }


                    if (Activity == "TB Training on Enrolment & SMC" && (i == 7 || i == 6 || i == 5))
                    {
                        if (st.Length > 0 && st != "0")
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if (Activity == "TB Training on Enrolment & SMC" && (i == 4))
                    {
                        if (st.Length > 4)
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if ((Activity == "TB Training on Bal Sabha and LSE" || Activity == "TB Training on Camp Vidya") && (i == 7))
                    {
                        if (st.Length > 0 && st != "0")
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if ((Activity == "TB Training on Bal Sabha and LSE" || Activity == "TB Training on Camp Vidya") && (i == 4 || i == 6 || i == 5))
                    {
                        if (st.Length > 4)
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if ((Activity == "TB orientation on EG annual Plan") && (i == 7 || i == 6 || i == 5))
                    {
                        if (st.Length > 0 && st != "0")
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if (Activity == "TB orientation on EG annual Plan" && (i == 4))
                    {
                        if (st.Length > 3)
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }
                    if ((Activity == "TB Alumni one day orientation" || Activity == "CG/MT Training on Enrollment & SMC" || Activity == "CG/Master Trainers Training on L0/L1" || Activity == "CG/Master Trainers Training on L1/L2" || Activity == "CG/Master Trainers Training on L2/L3" || Activity == "CG/MT Training on Bal Sabha & LSE") && (i == 4 || i == 5 || i == 6 || i == 7))
                    {
                        if (st.Length > 3)
                        {
                            TempFlag = true;
                            TempFlagT = true;
                        }
                    }


                    #endregion
                }
                if (TempFlagT == true)
                {
                    dtTraing.Rows[r]["TempID"] = "1";
                }
            }
            if (TempFlag == true)
            {
                string name = "";
                foreach (DataRow tableRow in dtCluster.Rows)
                {
                    if (tableRow["TempID"].ToString().Equals(name))
                        tableRow.Delete();
                }
                dtCluster.AcceptChanges();

                //foreach (DataRow tableRow in dtTraing.Rows)
                //{
                //    if (tableRow["TempID"].ToString().Equals(name))
                //        tableRow.Delete();
                //}

                //dtTraing.AcceptChanges();

                dtCluster.Columns.Remove("TempID");
                dtTraing.Columns.Remove("TempID");
                MultipuExeclTrackError(dtCluster, dtTraing);
                return;
            }
            else
            {
                dtCluster.Columns.Remove("TempID");
                dtTraing.Columns.Remove("TempID");
                if (dtCluster.Rows.Count > 0 || dtTraing.Rows.Count > 0)
                {
                    SavaDataCluster(dtCluster, dtTraing);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('No Record Found')</script>", false);

                }
            }


        }

        // need to catch possible exceptions
        catch
        {


        }
        finally
        {
            oledbConn.Close();
        }
    }
    public void SavaDataCluster24(DataTable dtCluster, DataTable dtTraing)
    {
        try
        {
            DataTable dtmain = CreateDataTableClusert();
            for (int r = 0; r < dtCluster.Rows.Count; r++)
            {
                dtmain.Rows.Add();
                for (int i = 6; i < dtCluster.Columns.Count; i++)
                {
                    string stValue = Convert.ToString(dtCluster.Rows[r][i]);
                    if (i == 9)
                    {
                        dtmain.Rows[r]["ClusterCode"] = stValue;
                    }
                    if (i == 16)
                    {
                        dtmain.Rows[r]["7-14YearsOOSGGoalQ1"] = stValue;
                    }
                    if (i == 17)
                    {
                        dtmain.Rows[r]["7-14YearsOOSGGoalQ2"] = stValue;
                    }
                    //if (i == 21)
                    //{
                    //    dtmain.Rows[r]["SeventofourteenYearsOOSG"] = stValue;
                    //}
                    //if (i == 22)
                    //{
                    //    dtmain.Rows[r]["FifteentoeighteenYearsOOSG"] = stValue;
                    //}
                    //if (i == 22)
                    //{
                    //    dtmain.Rows[r]["SeventofourteensOOSB"] = stValue;
                    //}
                    if (i == 18)
                    {
                        dtmain.Rows[r]["Q1GSS"] = stValue;
                    }
                    if (i == 19)
                    {
                        dtmain.Rows[r]["Q2GSS"] = stValue;
                    }
                    if (i == 20)
                    {
                        dtmain.Rows[r]["Q3GSS"] = stValue;
                    }
                    if (i == 21)
                    {
                        dtmain.Rows[r]["Q4GSS"] = stValue;
                    }



                    if (i == 26)
                    {
                        dtmain.Rows[r]["PanchayatMeetingQ1"] = stValue;
                    }
                    if (i == 27)
                    {
                        dtmain.Rows[r]["PanchayatMeetingQ2"] = stValue;
                    }
                    if (i == 28)
                    {
                        dtmain.Rows[r]["RatriChaupalQ1"] = stValue;
                    }
                    if (i == 29)
                    {
                        dtmain.Rows[r]["RatriChaupalQ2"] = stValue;
                    }
                    if (i == 30)
                    {
                        dtmain.Rows[r]["RatriChaupalQ3"] = stValue;
                    }
                    if (i == 31)
                    {
                        dtmain.Rows[r]["NamankanRailyQ1"] = stValue;
                    }





                    if (i == 22)
                    {
                        dtmain.Rows[r]["Q1MM"] = stValue;
                    }
                    if (i == 23)
                    {
                        dtmain.Rows[r]["Q2MM"] = stValue;
                    }
                    if (i == 24)
                    {
                        dtmain.Rows[r]["Q3MM"] = stValue;
                    }
                    if (i == 25)
                    {
                        dtmain.Rows[r]["Q4MM"] = stValue;
                    }


                    if (i == 45)
                    {
                        dtmain.Rows[r]["Balsaba"] = stValue;
                    }
                    if (i == 46)
                    {
                        dtmain.Rows[r]["GkpSchool"] = stValue;
                    }
                    if (i == 47)
                    {
                        dtmain.Rows[r]["Gkp"] = stValue;
                    }

                    if (i == 48)
                    {
                        dtmain.Rows[r]["GKPPlusSchools"] = stValue;
                    }
                    if (i == 49)
                    {
                        dtmain.Rows[r]["GKPPlusBeneficiaries"] = stValue;
                    }



                    if (i == 54)
                    {
                        dtmain.Rows[r]["Sac1"] = stValue;
                    }
                    if (i == 55)
                    {
                        dtmain.Rows[r]["Sac2"] = stValue;
                    }
                    if (i == 56)
                    {
                        dtmain.Rows[r]["Sac3"] = stValue;
                    }
                    if (i == 57)
                    {
                        dtmain.Rows[r]["Sac4"] = stValue;
                    }
                    //if (i == 57)
                    //{
                    //    dtmain.Rows[r]["AGPCampQ1"] = stValue;
                    //}
                    //if (i == 58)
                    //{
                    //    dtmain.Rows[r]["AGPCampQ2"] = stValue;
                    //}
                    //if (i == 59)
                    //{
                    //    dtmain.Rows[r]["AGPCampQ3"] = stValue;
                    //}
                    //if (i == 60)
                    //{
                    //    dtmain.Rows[r]["AGPCampQ4"] = stValue;
                    //}
                    //if (i == 61)
                    //{
                    //    dtmain.Rows[r]["AGPBeneficiariesQ1"] = stValue;
                    //}
                    //if (i == 62)
                    //{
                    //    dtmain.Rows[r]["AGPBeneficiariesQ2"] = stValue;
                    //}
                    //if (i == 63)
                    //{
                    //    dtmain.Rows[r]["AGPBeneficiariesQ3"] = stValue;
                    //}
                    //if (i == 64)
                    //{
                    //    dtmain.Rows[r]["AGPBeneficiariesQ4"] = stValue;
                    //}

                    //if (i == 65)
                    //{
                    //    dtmain.Rows[r]["AGPPrerakQ1"] = stValue;
                    //}
                    //if (i == 66)
                    //{
                    //    dtmain.Rows[r]["AGPPrerakQ2"] = stValue;
                    //}
                    //if (i == 67)
                    //{
                    //    dtmain.Rows[r]["AGPPrerakQ3"] = stValue;
                    //}
                    //if (i == 68)
                    //{
                    //    dtmain.Rows[r]["AGPPrerakQ4"] = stValue;
                    //}
                    dtmain.Rows[r]["Createby"] = Convert.ToString(Session["username"]);

                }

            }



            DataTable dtmainMainTraing = CreateDataTableTraining();
            int LQ = 0;
            for (int r = 0; r < dtTraing.Rows.Count; r++)
            {

                dtmainMainTraing.Rows.Add();
                for (int i = 0; i < dtTraing.Columns.Count; i++)
                {
                    string stValue = Convert.ToString(dtTraing.Rows[r][i]).Trim();
                    if (i == 1)
                    {
                        dtmainMainTraing.Rows[r]["DistrictCode"] = stValue;
                    }
                    if (i == 2)
                    {
                        dtmainMainTraing.Rows[r]["Activity"] = stValue.Trim();
                    }
                    if (i == 3)
                    {
                        dtmainMainTraing.Rows[r]["TrainingLevel"] = stValue;
                    }
                    if (i == 4)
                    {

                        dtmainMainTraing.Rows[r]["Q1"] = stValue;
                    }
                    if (i == 5)
                    {

                        dtmainMainTraing.Rows[r]["Q2"] = stValue;
                    }
                    if (i == 6)
                    {

                        dtmainMainTraing.Rows[r]["Q3"] = stValue;
                    }
                    if (i == 7)
                    {


                        dtmainMainTraing.Rows[r]["Q4"] = stValue;

                    }
                    dtmainMainTraing.Rows[r]["Createby"] = Convert.ToString(Session["username"]);
                }

            }
            DataRow[] dr = dtmainMainTraing.Select("Activity='Staff Training on Learning Endline'");
            if (dr.Length > 0)
            {
                dr[0]["Q1"] = "0";
                dr[0]["Q2"] = "0";
                dr[0]["Q3"] = "0";
                dr[0]["Q4"] = "0";
            }

            DataSet dsResult = Insert_Update_tblAnualPlanClusterWiseDetail(dtmain, dtmainMainTraing);
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved successfully')</script>", false);
                ddlDistrict_SelectedIndexChanged(ddlDistrict, null);

            }
        }
        catch
        {
            throw;
        }
    }
    public void SavaDataCluster(DataTable dtCluster, DataTable dtTraing)
    {
        try
        {
            DataTable dtmain = CreateDataTableClusert();
            for (int r = 0; r < dtCluster.Rows.Count; r++)
            {
                dtmain.Rows.Add();
                for (int i = 6; i < dtCluster.Columns.Count; i++)
                {
                    string stValue = Convert.ToString(dtCluster.Rows[r][i]);
                    if (i == 7)
                    {
                        dtmain.Rows[r]["ClusterCode"] = stValue;
                    }
                    if (i == 23)
                    {
                        dtmain.Rows[r]["FiveYearsOOSG"] = stValue;
                    }
                    if (i == 24)
                    {
                        dtmain.Rows[r]["SeventofourteenYearsOOSG"] = stValue;
                    }
                    if (i == 25)
                    {
                        dtmain.Rows[r]["FifteentoeighteenYearsOOSG"] = stValue;
                    }
                    if (i == 26)
                    {
                        dtmain.Rows[r]["SeventofourteensOOSB"] = stValue;
                    }
                    if (i == 27)
                    {
                        dtmain.Rows[r]["Q1GSS"] = stValue;
                    }
                    if (i == 28)
                    {
                        dtmain.Rows[r]["Q2GSS"] = stValue;
                    }
                    if (i == 29)
                    {
                        dtmain.Rows[r]["Q3GSS"] = stValue;
                    }
                    if (i == 30)
                    {
                        dtmain.Rows[r]["Q4GSS"] = stValue;
                    }
                    if (i == 31)
                    {
                        dtmain.Rows[r]["Q1MM"] = stValue;
                    }
                    if (i == 32)
                    {
                        dtmain.Rows[r]["Q2MM"] = stValue;
                    }
                    if (i == 33)
                    {
                        dtmain.Rows[r]["Q3MM"] = stValue;
                    }
                    if (i == 34)
                    {
                        dtmain.Rows[r]["Q4MM"] = stValue;
                    }
                    if (i == 35)
                    {
                        dtmain.Rows[r]["CBLVillages"] = stValue;
                    }

                    if (i == 48)
                    {
                        dtmain.Rows[r]["Balsaba"] = stValue;
                    }
                    if (i == 49)
                    {
                        dtmain.Rows[r]["GkpSchool"] = stValue;
                    }
                    if (i == 50)
                    {
                        dtmain.Rows[r]["Gkp"] = stValue;
                    }
                    if (i == 58)
                    {
                        dtmain.Rows[r]["Sac1"] = stValue;
                    }
                    if (i == 59)
                    {
                        dtmain.Rows[r]["Sac2"] = stValue;
                    }
                    if (i == 60)
                    {
                        dtmain.Rows[r]["Sac3"] = stValue;
                    }
                    if (i == 61)
                    {
                        dtmain.Rows[r]["Sac4"] = stValue;
                    }
                    if (i == 62)
                    {
                        dtmain.Rows[r]["AGPCampQ1"] = stValue;
                    }
                    if (i == 63)
                    {
                        dtmain.Rows[r]["AGPCampQ2"] = stValue;
                    }
                    if (i == 64)
                    {
                        dtmain.Rows[r]["AGPCampQ3"] = stValue;
                    }
                    if (i == 65)
                    {
                        dtmain.Rows[r]["AGPCampQ4"] = stValue;
                    }
                    if (i == 66)
                    {
                        dtmain.Rows[r]["AGPBeneficiariesQ1"] = stValue;
                    }
                    if (i == 67)
                    {
                        dtmain.Rows[r]["AGPBeneficiariesQ2"] = stValue;
                    }
                    if (i == 68)
                    {
                        dtmain.Rows[r]["AGPBeneficiariesQ3"] = stValue;
                    }
                    if (i == 69)
                    {
                        dtmain.Rows[r]["AGPBeneficiariesQ4"] = stValue;
                    }

                    if (i == 70)
                    {
                        dtmain.Rows[r]["AGPPrerakQ1"] = stValue;
                    }
                    if (i == 71)
                    {
                        dtmain.Rows[r]["AGPPrerakQ2"] = stValue;
                    }
                    if (i == 72)
                    {
                        dtmain.Rows[r]["AGPPrerakQ3"] = stValue;
                    }
                    if (i == 73)
                    {
                        dtmain.Rows[r]["AGPPrerakQ4"] = stValue;
                    }
                    dtmain.Rows[r]["Createby"] = Convert.ToString(Session["username"]);

                }

            }


            //DataTable dtClusterCodeMain = CreateDataTableMain();
            //for (int r = 0; r < dtmain.Rows.Count; r++)
            //{
            //    string stValue1 = Convert.ToString(dtmain.Rows[r]["ClusterCode"]);
            //    for (int i = 0; i < dtmain.Columns.Count; i++)
            //    {
            //        dtClusterCodeMain.Rows.Add();
            //        string stValue = Convert.ToString(dtmain.Rows[r][i]);

            //        dtClusterCodeMain.Rows[r]["ClusterCode"] = stValue1;
            //        if (i == 1)
            //        {
            //            dtClusterCodeMain.Rows[r]["Activity"] = "5-6 Years OOSG Goal";
            //            dtClusterCodeMain.Rows[r]["Q1"] = stValue;
            //        }

            //    }
            //}

            DataTable dtmainMainTraing = CreateDataTableTraining();
            int LQ = 0;
            for (int r = 0; r < dtTraing.Rows.Count; r++)
            {

                dtmainMainTraing.Rows.Add();
                for (int i = 0; i < dtTraing.Columns.Count; i++)
                {
                    string stValue = Convert.ToString(dtTraing.Rows[r][i]).Trim();
                    if (i == 1)
                    {
                        dtmainMainTraing.Rows[r]["DistrictCode"] = stValue;
                    }
                    if (i == 2)
                    {
                        dtmainMainTraing.Rows[r]["Activity"] = stValue.Trim();
                    }
                    if (i == 3)
                    {
                        dtmainMainTraing.Rows[r]["TrainingLevel"] = stValue;
                    }
                    if (i == 4)
                    {

                        dtmainMainTraing.Rows[r]["Q1"] = stValue;
                    }
                    if (i == 5)
                    {

                        dtmainMainTraing.Rows[r]["Q2"] = stValue;
                    }
                    if (i == 6)
                    {

                        dtmainMainTraing.Rows[r]["Q3"] = stValue;
                    }
                    if (i == 7)
                    {


                        dtmainMainTraing.Rows[r]["Q4"] = stValue;

                    }
                    dtmainMainTraing.Rows[r]["Createby"] = Convert.ToString(Session["username"]);
                }

            }
            DataRow[] dr = dtmainMainTraing.Select("Activity='Staff Training on Learning Endline'");
            if (dr.Length > 0)
            {
                dr[0]["Q1"] = "0";
                dr[0]["Q2"] = "0";
                dr[0]["Q3"] = "0";
                dr[0]["Q4"] = "0";
            }

            DataSet dsResult = Insert_Update_tblAnualPlanClusterWiseDetail(dtmain, dtmainMainTraing);
            if (dsResult.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved successfully')</script>", false);
                ddlDistrict_SelectedIndexChanged(ddlDistrict, null);

            }
        }
        catch
        {
            throw;
        }
    }
    public DataTable CreateDataTableTraining()
    {

        DataTable dtMaintraing = new DataTable();
        dtMaintraing.Columns.Add("DistrictCode", System.Type.GetType("System.String"));

        dtMaintraing.Columns.Add("Activity", System.Type.GetType("System.String"));
        dtMaintraing.Columns.Add("TrainingLevel", System.Type.GetType("System.String"));
        dtMaintraing.Columns.Add("Q1", System.Type.GetType("System.String"));
        dtMaintraing.Columns.Add("Q2", System.Type.GetType("System.String"));
        dtMaintraing.Columns.Add("Q3", System.Type.GetType("System.String"));
        dtMaintraing.Columns.Add("Q4", System.Type.GetType("System.String"));
        dtMaintraing.Columns.Add("Createby", System.Type.GetType("System.String"));
        return dtMaintraing;
    }
    public DataTable CreateDataTableMain()
    {

        DataTable dtClusterCodeMain = new DataTable();
        dtClusterCodeMain.Columns.Add("ClusterCode", System.Type.GetType("System.String"));

        dtClusterCodeMain.Columns.Add("Activity", System.Type.GetType("System.String"));

        dtClusterCodeMain.Columns.Add("Q1", System.Type.GetType("System.String"));
        dtClusterCodeMain.Columns.Add("Q2", System.Type.GetType("System.String"));
        dtClusterCodeMain.Columns.Add("Q3", System.Type.GetType("System.String"));
        dtClusterCodeMain.Columns.Add("Q4", System.Type.GetType("System.String"));
        dtClusterCodeMain.Columns.Add("Createby", System.Type.GetType("System.String"));
        return dtClusterCodeMain;
    }
    public DataTable CreateDataTableClusert()
    {

        DataTable dtMainCluster = new DataTable();


        dtMainCluster.Columns.Add("ClusterCode", System.Type.GetType("System.String"));

        dtMainCluster.Columns.Add("7-14YearsOOSGGoalQ1", System.Type.GetType("System.String"));
        dtMainCluster.Columns.Add("7-14YearsOOSGGoalQ2", System.Type.GetType("System.String"));

        dtMainCluster.Columns.Add("PanchayatMeetingQ1", System.Type.GetType("System.String"));
        dtMainCluster.Columns.Add("PanchayatMeetingQ2", System.Type.GetType("System.String"));
        dtMainCluster.Columns.Add("RatriChaupalQ1", System.Type.GetType("System.String"));
        dtMainCluster.Columns.Add("RatriChaupalQ2", System.Type.GetType("System.String"));
        dtMainCluster.Columns.Add("RatriChaupalQ3", System.Type.GetType("System.String"));
        dtMainCluster.Columns.Add("NamankanRailyQ1", System.Type.GetType("System.String"));
        dtMainCluster.Columns.Add("Q1GSS", System.Type.GetType("System.String"));
        dtMainCluster.Columns.Add("Q2GSS", System.Type.GetType("System.String"));
        dtMainCluster.Columns.Add("Q3GSS", System.Type.GetType("System.String"));
        dtMainCluster.Columns.Add("Q4GSS", System.Type.GetType("System.String"));
        dtMainCluster.Columns.Add("Q1MM", System.Type.GetType("System.String"));
        dtMainCluster.Columns.Add("Q2MM", System.Type.GetType("System.String"));
        dtMainCluster.Columns.Add("Q3MM", System.Type.GetType("System.String"));
        dtMainCluster.Columns.Add("Q4MM", System.Type.GetType("System.String"));
        dtMainCluster.Columns.Add("Balsaba", System.Type.GetType("System.String"));
        dtMainCluster.Columns.Add("GkpSchool", System.Type.GetType("System.String"));
        dtMainCluster.Columns.Add("Gkp", System.Type.GetType("System.String"));
        dtMainCluster.Columns.Add("GKPPlusSchools", System.Type.GetType("System.String"));
        dtMainCluster.Columns.Add("GKPPlusBeneficiaries", System.Type.GetType("System.String"));

        dtMainCluster.Columns.Add("Sac1", System.Type.GetType("System.String"));
        dtMainCluster.Columns.Add("Sac2", System.Type.GetType("System.String"));
        dtMainCluster.Columns.Add("Sac3", System.Type.GetType("System.String"));
        dtMainCluster.Columns.Add("Sac4", System.Type.GetType("System.String"));

        //dtMainCluster.Columns.Add("AGPCampQ1", System.Type.GetType("System.String"));
        //dtMainCluster.Columns.Add("AGPCampQ2", System.Type.GetType("System.String"));
        //dtMainCluster.Columns.Add("AGPCampQ3", System.Type.GetType("System.String"));
        //dtMainCluster.Columns.Add("AGPCampQ4", System.Type.GetType("System.String"));

        //dtMainCluster.Columns.Add("AGPBeneficiariesQ1", System.Type.GetType("System.String"));
        //dtMainCluster.Columns.Add("AGPBeneficiariesQ2", System.Type.GetType("System.String"));
        //dtMainCluster.Columns.Add("AGPBeneficiariesQ3", System.Type.GetType("System.String"));
        //dtMainCluster.Columns.Add("AGPBeneficiariesQ4", System.Type.GetType("System.String"));


        //dtMainCluster.Columns.Add("AGPPrerakQ1", System.Type.GetType("System.String"));
        //dtMainCluster.Columns.Add("AGPPrerakQ2", System.Type.GetType("System.String"));
        //dtMainCluster.Columns.Add("AGPPrerakQ3", System.Type.GetType("System.String"));
        //dtMainCluster.Columns.Add("AGPPrerakQ4", System.Type.GetType("System.String"));
        //dtMainCluster.Columns.Add("CBLVillages", System.Type.GetType("System.String"));

        dtMainCluster.Columns.Add("Createby", System.Type.GetType("System.String"));

        return dtMainCluster;
    }
    public DataSet Insert_Update_tblAnualPlanClusterWiseDetail(DataTable dtMainCluster, DataTable dtMaintraing)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Insert_Update_tblAnualPlanClusterWiseDetail2025";
            sqlcmd.Parameters.AddWithValue("@tblAnualPlanClusterWiseDetail", dtMainCluster);
            sqlcmd.Parameters.AddWithValue("@tblAnualPlanTraingWiseDetail", dtMaintraing);

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw e;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Insert_Update_tblAnualPlanClusterWiseDetailPage(DataTable dtMaintraing)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Insert_Update_tblAnualPlanClusterWiseDetailPage";

            sqlcmd.Parameters.AddWithValue("@tblAnualPlanTraingWiseDetail", dtMaintraing);

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw e;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    protected void btnSubmitted_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {
        }
        else
        {
            Response.Redirect("Login.aspx", false);
        }
        if (ddlDistrict.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select District')</script>", false);

            return;

        }
        int approveStataus = 0;
        if (Convert.ToString(Session["user_level"]) == "39" || Convert.ToString(Session["user_level"]) == "145")
        {
            approveStataus = 1;
        }
        if (Convert.ToString(Session["user_level"]) == "91")
        {
            approveStataus = 2;
        }
        if (Convert.ToString(Session["user_level"]) == "92")
        {
            approveStataus = 3;
        }
        int icount = 0;
        SqlParameter[] cmdParameters = new SqlParameter[]
       {
            new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
            new SqlParameter("@approveStataus", approveStataus),
            new SqlParameter("@Remark", ""),
             new SqlParameter("@UserName", Convert.ToString(Session["username"])),
               new SqlParameter("@Flag", "1"),



       };
        icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateAnualPlanFinalApprove", cmdParameters);


        //int ff = DateTime.Today.Month;
        //if (ff == 7 || ff == 8 || ff == 9)
        //{
        //    SqlParameter[] cmdParameters1 = new SqlParameter[]
        //  {
        //    new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
        //    new SqlParameter("@approveStataus", approveStataus),
        //    new SqlParameter("@Remark", ""),
        //     new SqlParameter("@UserName", Convert.ToString(Session["username"])),
        //       new SqlParameter("@Flag", "1"),
        //         new SqlParameter("@Q1", "2"),

        //                   };
        //     icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateAnualPlanFinalApproveQ", cmdParameters1);

        //}
        //if (ff == 10 || ff == 11 || ff == 12)
        //{
        //    SqlParameter[] cmdParameters1 = new SqlParameter[]
        // {
        //    new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
        //    new SqlParameter("@approveStataus", approveStataus),
        //    new SqlParameter("@Remark", ""),
        //     new SqlParameter("@UserName", Convert.ToString(Session["username"])),
        //       new SqlParameter("@Flag", "1"),
        //         new SqlParameter("@Q1", "3"),

        //                  };
        //    icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateAnualPlanFinalApproveQ", cmdParameters1);

        //}
        //if (ff == 1 || ff == 2 || ff == 3)
        //{
        //    SqlParameter[] cmdParameters1 = new SqlParameter[]
        //{
        //    new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
        //    new SqlParameter("@approveStataus", approveStataus),
        //    new SqlParameter("@Remark", ""),
        //     new SqlParameter("@UserName", Convert.ToString(Session["username"])),
        //       new SqlParameter("@Flag", "1"),
        //         new SqlParameter("@Q1", "4"),

        //                 };
        //    icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateAnualPlanFinalApproveQ", cmdParameters1);

        //}


        if (icount > 0)
        {
            if (Convert.ToString(Session["user_level"]) == "39" || Convert.ToString(Session["user_level"]) == "145")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Data Successfully Submitted to DOL!!')</script>", false);

            }
            if (Convert.ToString(Session["user_level"]) == "91")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Data Successfully Submitted to SOL!!')</script>", false);

            }
            if (Convert.ToString(Session["user_level"]) == "92")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Congratulations your Annual Plan has been successfully approved!')</script>", false);
            }

            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);

        }


    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        txtRemark.Text = "";
        ModalPopupExtender1.Show();
    }
    protected void btnUnlock_Click(object sender, EventArgs e)
    {

        SqlParameter[] parm = new SqlParameter[]
            {

           new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
               new SqlParameter("@Createby", Convert.ToString(Session["username"])),


              };
        int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateAnualPlanFinalApproveAdmin", parm);
        if (result > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unlocked Successfully')</script>", false);
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
        }
    }
    protected void btnsaveReject_Click(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select District')</script>", false);

            return;

        }
        int approveStataus = 0;

        if (Convert.ToString(Session["user_level"]) == "91")
        {
            approveStataus = 4;
        }
        if (Convert.ToString(Session["user_level"]) == "92")
        {
            approveStataus = 5;
        }
        SqlParameter[] cmdParameters = new SqlParameter[]
       {
            new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
            new SqlParameter("@approveStataus", approveStataus),
            new SqlParameter("@Remark", txtRemark.Text),
             new SqlParameter("@UserName", Convert.ToString(Session["username"])),
               new SqlParameter("@Flag", "2"),



       };
        int icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateAnualPlanFinalApprove", cmdParameters);
        //int ff = DateTime.Today.Month;
        //if (ff == 7 || ff == 8 || ff == 9)
        //{
        //    SqlParameter[] cmdParameters1 = new SqlParameter[]
        //  {
        //    new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
        //    new SqlParameter("@approveStataus", approveStataus),
        //    new SqlParameter("@Remark", ""),
        //     new SqlParameter("@UserName", Convert.ToString(Session["username"])),
        //       new SqlParameter("@Flag", "2"),
        //         new SqlParameter("@Q1", "2"),

        //                   };
        //     icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateAnualPlanFinalApproveQ", cmdParameters1);

        //}
        //if (ff == 10 || ff == 11 || ff == 12)
        //{
        //    SqlParameter[] cmdParameters1 = new SqlParameter[]
        // {
        //    new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
        //    new SqlParameter("@approveStataus", approveStataus),
        //    new SqlParameter("@Remark", ""),
        //     new SqlParameter("@UserName", Convert.ToString(Session["username"])),
        //       new SqlParameter("@Flag", "2"),
        //         new SqlParameter("@Q1", "3"),

        //                  };
        //    icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateAnualPlanFinalApproveQ", cmdParameters1);

        //}
        //if (ff == 1 || ff == 2 || ff == 3)
        //{
        //    SqlParameter[] cmdParameters1 = new SqlParameter[]
        //{
        //    new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue),
        //    new SqlParameter("@approveStataus", approveStataus),
        //    new SqlParameter("@Remark", ""),
        //     new SqlParameter("@UserName", Convert.ToString(Session["username"])),
        //       new SqlParameter("@Flag", "2"),
        //         new SqlParameter("@Q1", "4"),

        //                 };
        //    icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateAnualPlanFinalApproveQ", cmdParameters1);

        //}
        if (icount > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Annual plan rejected successfully!!')</script>", false);
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);

        }

    }
}