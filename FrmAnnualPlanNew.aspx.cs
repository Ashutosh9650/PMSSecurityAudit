using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmAnnualPlanNew : System.Web.UI.Page
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
                if (Convert.ToInt32(ddlYear.SelectedValue) >= 2022)
                {
                    divSub.Visible = false;
                }
                else
                {
                    divSub.Visible = true;
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

            btnDelete.Enabled = true;
            btnsave.Enabled = true;
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
                    
                    if (date2>date1)
                    {
                        btnDelete.Enabled = false;
                        btnsave.Enabled = false;
                     }

                }
                #endregion

            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 2)
            {
                strQry = "Select * from mstModuleLocking  where [FromName]='Annual Plan Village Wise' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "' ";
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


                    if (date2>date1)
                    
                    {
                        btnDelete.Enabled = false;
                        btnsave.Enabled = false;

                    }
                }
                #endregion

            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 3)
            {
                strQry = "Select * from mstModuleLocking  where [FromName]='Annual Plan School Wise' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "' ";
                #region  School Wise


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

                    }
                }
                #endregion

            }
            string strQry1 = "  SELECT * FROM [Tbl_PhaseMapping] where Phase=3   and  Financial_Year='" + ddlYear.SelectedItem.Text + "' and DistrictCode='" + ddlDistrict.SelectedValue + "'  ";
            DataTable dtPhage = objMain.LoadData(strQry1);
            if (dtPhage.Rows.Count > 0)
            {
                vPhase = true;
                ViewState["vPhase"] = "1";
            }
            else
            {
                ViewState["vPhase"] = "2";
            }
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
    public void LoadUserLeavel()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            ddlState.SelectedIndex = 1;
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

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


        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");



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
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "'";
        objComman.BindDLLSelectAll("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "--Select--");



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
                if (Convert.ToInt32(ddlYear.SelectedValue)>=2022)
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
    protected void     btnSerach_Click(object sender, EventArgs e)
    {
        Locking();
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
        if (ddlType.SelectedValue == "2" || ddlType.SelectedValue == "3")
        {
            lblMsg.Visible = false;
            if (ddlBlock.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Block')</script>", false);

                return;

            }
        }
        if (ddlType.SelectedValue == "1")
        {
            lblMsg.Visible = true;
            string SubType = "";
            if (ddlsubType.SelectedIndex > 0)
            {
                SubType = " and mstLookupAnnaulPlan.LookupType=" + ddlsubType.SelectedValue + " and isnull(mstLookupAnnaulPlan.EndMonth,0)>0 ";
            }
            else
            {
                SubType = " and isnull(mstLookupAnnaulPlan.EndMonth,0)>0 ";
            }
            string strQry = "";
            if (Convert.ToInt32(ddlYear.SelectedValue)>=2022)
            {
                strQry = " select mstLookupAnnaulPlanNew.Description,mstLookupAnnaulPlanNew.LookupCode,  [Apr], [May], [Jun], [Jul], [Aug], [Sep], [Oct], [Nov], [Dec], [Jan], [Feb], [Mar],SysFlag,StartMonth,EndMonth,MaxVal,mstLookupAnnaulPlanNew.LookupType,PhageFlag from mstLookupAnnaulPlanNew   left join (select *  from tblAnualPlanDataDetail where Districtcode='" + ddlDistrict.SelectedValue + "' and PlanType=1 )   as tblAnualPlanDataDetail on mstLookupAnnaulPlanNew.LookUpcode =tblAnualPlanDataDetail.RowNo where mstLookupAnnaulPlanNew.LookupFlag='APLD'  and mstLookupAnnaulPlanNew.LookupType=2  order by seqno ";

            }
            else
            {
                 strQry = " select mstLookupAnnaulPlan.Description,mstLookupAnnaulPlan.LookupCode,  [Apr], [May], [Jun], [Jul], [Aug], [Sep], [Oct], [Nov], [Dec], [Jan], [Feb], [Mar],SysFlag,StartMonth,EndMonth,MaxVal,mstLookupAnnaulPlan.LookupType,PhageFlag from mstLookupAnnaulPlan   left join (select *  from tblAnualPlanDataDetail where Districtcode='" + ddlDistrict.SelectedValue + "' and PlanType=1 )   as tblAnualPlanDataDetail on mstLookupAnnaulPlan.LookUpcode =tblAnualPlanDataDetail.RowNo where LookupFlag='APLD'  " + SubType + "  order by seqno ";

            }
            dtSchool = objComman.LoadData(strQry);
            //if (dtSchool.Rows.Count > 0)
            //{
            //}
            //else
            //{
               
            //    strQry = " select Description,LookupCode, 0 as [Apr],0 as [May],0 as [Jun],0 as [Jul],0 as [Aug],0 as [Sep],0 as [Oct],0 as [Nov],0 as [Dec],0 as [Jan],0 as [Feb],0 as [Mar],0 as SysFlag,StartMonth,EndMonth,MaxVal,LookupType from mstLookupAnnaulPlan where LookupFlag='APLD' " + SubType + " order by seqno ";
            //    dtSchool = objComman.LoadData(strQry);
            //}
            if (dtSchool.Rows.Count > 0)
            {
                GVMain.DataSource = null;
                GVMain.DataBind();
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
            Bindgrid();

            DataTable dtLearing = null;
            string strQry = " select  [Jun], [Jul], [Aug], [Sep]  from tblAnualPlanDataDetail where Districtcode='" + ddlDistrict.SelectedValue + "' and PlanType=1 and RowNo=7 and (Jun>0 or Jul>0 or Aug>0 or Sep>0)   ";
             dtLearing = objComman.LoadData(strQry);


             if (dtLearing.Rows.Count > 0)
             {
                 Session["dtLearing"] = dtLearing;
             }
             //else
             //{
             //    string strQry1 = " select  [Jun], [Jul], [Aug], [Sep]  from tblAnualPlanDataDetail where Districtcode='" + ddlDistrict.SelectedValue + "' and PlanType=1 and RowNo=32  ";
             //    dtLearing = objComman.LoadData(strQry1);
             //    if (dtLearing.Rows.Count > 0)
             //    {
             //        Session["dtLearing"] = dtLearing;
             //    }
             //}

         
        }

       // ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "ddlTypeOnChangeEvent()", true);
       //DVEE.Attributes.Add("style", "display:block");
      
       
    }

    public void SIPDATA()
    {
        DataTable dt = Session["dtSearchVill"] as DataTable;
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


            if (dt.Rows[i]["Description"].ToString() == "SMC Meet cum Orientation")
            {


                Int32 Apr = Convert.ToInt32(TxtApr.Text);
                Int32 May = Convert.ToInt32(TxtMay.Text);
                Int32 Jun = Convert.ToInt32(TxtJun.Text);
                Int32 Jul = Convert.ToInt32(TxtJul.Text);
                Int32 Aug = Convert.ToInt32(TxtAug.Text);
                Int32 Sep = Convert.ToInt32(TxtSep.Text);
                Int32 Oct = Convert.ToInt32(TxtOct.Text);
                Int32 Nov = Convert.ToInt32(TxtNov.Text);
                Int32 Dec = Convert.ToInt32(TxtDec.Text);
                Int32 Jan = Convert.ToInt32(TxtJan.Text);
                Int32 Feb = Convert.ToInt32(TxtFeb.Text);
                Int32 Mar = Convert.ToInt32(TxtMar.Text);


                SIP(dt,Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec, Jan, Feb, Mar);

            }


        }
    }

    public void GKPDATA()
    {
        DataTable dt = Session["dtSearchVill"] as DataTable;
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


            if (dt.Rows[i]["Description"].ToString() == "Learning Baseline for GKP")
            {


                Int32 Jun = 0;
                Int32 Jul = 0;
                Int32 Aug = 0;
                Int32 Sep = 0;
                Int32 Oct = 0;

                if (TxtJun.Text != "")
                {
                    Jun = Convert.ToInt32(TxtJun.Text);
                }
                if (TxtJul.Text != "")
                {
                    Jul = Convert.ToInt32(TxtJul.Text);
                }
                if (TxtAug.Text != "")
                {
                    Aug = Convert.ToInt32(TxtAug.Text);
                }
                if (TxtSep.Text != "")
                {

                    Sep = Convert.ToInt32(TxtSep.Text);
                }
                if (TxtOct.Text != "")
                {

                    Oct = Convert.ToInt32(TxtOct.Text);
                }
              
                LEARNINGMIDLINE(dt, Jun,Jul, Sep, Aug, Oct);

            }


        }
    }

    public void LEARNINGMIDLINE(DataTable dt, Int32 jun,Int32 jul, Int32 Sep, Int32 Aug, Int32 Oct)
    {

        dtGKPPlan = Session["dtGKPPlan"] as DataTable;
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



            if (dt.Rows[i]["Description"].ToString() == "GKP L0/L1" && Convert.ToString(ViewState["GKPLevel"]) == "1")
            {
                if (Convert.ToString(ViewState["GKP"]) == "1" && Convert.ToString(ViewState["GKPLevel"]) == "1")
                {
                    #region

                    DataRow[] dr = dtGKPPlan.Select("GKPID=" + Convert.ToString(ViewState["GKPLevel"]) + "");
                    TxtJun.Text = "0";
                    TxtJul.Text = "0";
                    TxtSep.Text = "0";
                    TxtAug.Text = "0";
                    TxtOct.Text = "0";
                    TxtNov.Text = "0";
                    TxtDec.Text = "0";
                    TxtJan.Text = "0";
                    TxtFeb.Text = "0";
                    TxtMar.Text = "0";
                    if (jun > 0)
                    {
                        if (dr.Length > 0)
                        {
                            TxtJun.Text = dr[0]["Month1"].ToString();
                            TxtJul.Text = dr[0]["Month1"].ToString();
                            TxtSep.Text = dr[0]["Month2"].ToString();
                            TxtAug.Text = dr[0]["Month3"].ToString();
                            TxtOct.Text = dr[0]["Month4"].ToString();
                            TxtNov.Text = dr[0]["Month5"].ToString();
                        
                        }



                    }
                    if (jul > 0)
                    {
                        if (dr.Length > 0)
                        {
                            TxtJul.Text = dr[0]["Month1"].ToString();
                            TxtSep.Text = dr[0]["Month2"].ToString();
                            TxtAug.Text = dr[0]["Month3"].ToString();
                            TxtOct.Text = dr[0]["Month4"].ToString();
                            TxtNov.Text = dr[0]["Month5"].ToString();
                            TxtDec.Text = dr[0]["Month6"].ToString();
                        }
                    


                    }
                    if (Sep > 0)
                    {
                        if (dr.Length > 0)
                        {
                            TxtSep.Text = dr[0]["Month1"].ToString();
                            TxtOct.Text = dr[0]["Month2"].ToString();
                            TxtNov.Text = dr[0]["Month3"].ToString();
                            TxtDec.Text = dr[0]["Month4"].ToString();
                            TxtJan.Text = dr[0]["Month5"].ToString();

                            TxtFeb.Text = dr[0]["Month6"].ToString();
                        
                        
                        }
                    }
                    if (Aug > 0)
                    {
                        if (dr.Length > 0)
                        {
                         
                            TxtSep.Text = dr[0]["Month1"].ToString();
                            TxtAug.Text = dr[0]["Month2"].ToString();
                            TxtOct.Text = dr[0]["Month3"].ToString();
                            TxtNov.Text = dr[0]["Month4"].ToString();
                            TxtDec.Text = dr[0]["Month5"].ToString();
                            TxtJan.Text = dr[0]["Month6"].ToString();
                         
                        }
                    }
                    if (Oct > 0)
                    {


                        if (dr.Length > 0)
                        {

                            TxtOct.Text = dr[0]["Month1"].ToString();
                            TxtNov.Text = dr[0]["Month2"].ToString();
                            TxtDec.Text = dr[0]["Month3"].ToString();
                            TxtJan.Text = dr[0]["Month4"].ToString();

                            TxtFeb.Text = dr[0]["Month5"].ToString();
                            TxtMar.Text = dr[0]["Month6"].ToString();

                        }
                    }
               
                    #endregion
                }
            }

            if (dt.Rows[i]["Description"].ToString() == "GKP L1/L2" && Convert.ToString(ViewState["GKPLevel"]) == "2")
            {
                if (Convert.ToString(ViewState["GKP"]) == "1" && Convert.ToString(ViewState["GKPLevel"]) == "2")
                {
                    #region

                    DataRow[] dr = dtGKPPlan.Select("GKPID=" + Convert.ToString(ViewState["GKPLevel"]) + "");
                  
                   TxtJul.Text = "0";
                    TxtSep.Text = "0";
                    TxtAug.Text = "0";
                    TxtOct.Text = "0";
                    TxtNov.Text = "0";
                    TxtDec.Text = "0";
                    TxtJan.Text = "0";
                    TxtFeb.Text = "0";
                    TxtMar.Text = "0";
                    if (jul > 0)
                    {

                        if (dr.Length > 0)
                        {
                            TxtJul.Text = dr[0]["Month1"].ToString();
                            TxtSep.Text = dr[0]["Month2"].ToString();
                            TxtAug.Text = dr[0]["Month3"].ToString();
                            TxtOct.Text = dr[0]["Month4"].ToString();
                            TxtNov.Text = dr[0]["Month5"].ToString();
                            TxtDec.Text = dr[0]["Month6"].ToString();
                        

                        }


                    }
                    if (Sep > 0)
                    {
                        if (dr.Length > 0)
                        {
                            TxtSep.Text = dr[0]["Month1"].ToString();
                            TxtOct.Text = dr[0]["Month2"].ToString();
                            TxtNov.Text = dr[0]["Month3"].ToString();
                            TxtDec.Text = dr[0]["Month4"].ToString();
                            TxtJan.Text = dr[0]["Month5"].ToString();

                            TxtFeb.Text = dr[0]["Month6"].ToString();
                        
                        }
                    }
                    if (Aug > 0)
                    {
                        if (dr.Length > 0)
                        {
                         
                            TxtSep.Text = dr[0]["Month1"].ToString();
                            TxtAug.Text = dr[0]["Month2"].ToString();
                            TxtOct.Text = dr[0]["Month3"].ToString();
                            TxtNov.Text = dr[0]["Month4"].ToString();
                            TxtDec.Text = dr[0]["Month5"].ToString();
                            TxtJan.Text = dr[0]["Month6"].ToString();
                         
                        }
                    }
                    if (Oct > 0)
                    {


                        if (dr.Length > 0)
                        {

                            TxtOct.Text = dr[0]["Month1"].ToString();
                            TxtNov.Text = dr[0]["Month2"].ToString();
                            TxtDec.Text = dr[0]["Month3"].ToString();
                            TxtJan.Text = dr[0]["Month4"].ToString();

                            TxtFeb.Text = dr[0]["Month5"].ToString();
                            TxtMar.Text = dr[0]["Month6"].ToString();
                        }
                    }
                    
                }
                    #endregion
            }
            if (dt.Rows[i]["Description"].ToString() == "GKP L2/L3" && Convert.ToString(ViewState["GKPLevel"]) == "3")
            {
                if (Convert.ToString(ViewState["GKP"]) == "1" && Convert.ToString(ViewState["GKPLevel"]) == "3")
                {
                    #region

                    DataRow[] dr = dtGKPPlan.Select("GKPID=" + Convert.ToString(ViewState["GKPLevel"]) + "");
                   TxtJul.Text = "0";
                    TxtSep.Text = "0";
                    TxtAug.Text = "0";
                    TxtOct.Text = "0";
                    TxtNov.Text = "0";
                    TxtDec.Text = "0";
                    TxtJan.Text = "0";
                    TxtFeb.Text = "0";
                    TxtMar.Text = "0";
                    if (jun > 0)
                    {

                        if (dr.Length > 0)
                        {
                            TxtJun.Text = dr[0]["Month1"].ToString();
                            TxtJul.Text = dr[0]["Month2"].ToString();
                            TxtSep.Text = dr[0]["Month4"].ToString();
                            TxtAug.Text = dr[0]["Month3"].ToString();
                            TxtOct.Text = dr[0]["Month5"].ToString();
                            TxtNov.Text = dr[0]["Month6"].ToString();
                         


                        }


                    }
                    if (jul > 0)
                    {

                        if (dr.Length > 0)
                        {
                            TxtJul.Text = dr[0]["Month1"].ToString();
                            TxtSep.Text = dr[0]["Month2"].ToString();
                            TxtAug.Text = dr[0]["Month3"].ToString();
                            TxtOct.Text = dr[0]["Month4"].ToString();
                            TxtNov.Text = dr[0]["Month5"].ToString();
                            TxtDec.Text = dr[0]["Month6"].ToString();
                        

                        }


                    }
                    if (Sep > 0)
                    {
                        if (dr.Length > 0)
                        {
                            TxtSep.Text = dr[0]["Month1"].ToString();
                            TxtOct.Text = dr[0]["Month2"].ToString();
                            TxtNov.Text = dr[0]["Month3"].ToString();
                            TxtDec.Text = dr[0]["Month4"].ToString();
                            TxtJan.Text = dr[0]["Month5"].ToString();

                            TxtFeb.Text = dr[0]["Month6"].ToString();
                        
                        
                          

                        }
                    }
                    if (Aug > 0)
                    {
                        if (dr.Length > 0)
                        {
                         
                            TxtSep.Text = dr[0]["Month1"].ToString();
                            TxtAug.Text = dr[0]["Month2"].ToString();
                            TxtOct.Text = dr[0]["Month3"].ToString();
                            TxtNov.Text = dr[0]["Month4"].ToString();
                            TxtDec.Text = dr[0]["Month5"].ToString();
                            TxtJan.Text = dr[0]["Month6"].ToString();
                         
                        }
                    }
                    if (Oct > 0)
                    {


                        if (dr.Length > 0)
                        {

                            TxtOct.Text = dr[0]["Month1"].ToString();
                            TxtNov.Text = dr[0]["Month2"].ToString();
                            TxtDec.Text = dr[0]["Month3"].ToString();
                            TxtJan.Text = dr[0]["Month4"].ToString();

                            TxtFeb.Text = dr[0]["Month5"].ToString();
                            TxtMar.Text = dr[0]["Month6"].ToString();
                        }
                    }
                }
                    #endregion
            }

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
        if (Convert.ToInt32(ddlType.SelectedValue) == 3)
        {
           // SIPDATA();
            GKPDATA();
        }
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2022)
        {
            SaveData2020();
        }
        else
        {
            SaveData();
        }


       
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
      
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        
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
          string  GKPLevel= Convert.ToString(GVMain.DataKeys[iIndex]["GKPLevel"]);
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
            TextBox TxtAug = ((TextBox)e.Row.FindControl("TxtAug"));
            TextBox TxtSep = ((TextBox)e.Row.FindControl("TxtSep"));
            TextBox TxtOct = ((TextBox)e.Row.FindControl("TxtOct"));
            TextBox TxtNov = ((TextBox)e.Row.FindControl("TxtNov"));
            TextBox TxtDec = ((TextBox)e.Row.FindControl("TxtDec"));
            TextBox TxtJan = ((TextBox)e.Row.FindControl("TxtJan"));
            TextBox TxtFeb = ((TextBox)e.Row.FindControl("TxtFeb"));
            TextBox TxtMar = ((TextBox)e.Row.FindControl("TxtMar"));
            Label lblStartMonth = ((Label)e.Row.FindControl("lblStartMonth"));
            Label lblEndMonth = ((Label)e.Row.FindControl("lblEndMonth"));
             Label lblPhageFlag = ((Label)e.Row.FindControl("lblPhageFlag"));
           
            if (ddlType.SelectedValue == "1")
            {
               
                    LoadDataEnable(TxtApr, TxtMay, TxtJun, TxtJul, TxtAug, TxtSep, TxtOct, TxtNov, TxtDec, TxtJan, TxtFeb, TxtMar, Convert.ToInt32(lblStartMonth.Text), Convert.ToInt32(lblEndMonth.Text));

                
           
            }
            else if (ddlType.SelectedValue == "2")
            {
              
                    LoadDataEnable(TxtApr, TxtMay, TxtJun, TxtJul, TxtAug, TxtSep, TxtOct, TxtNov, TxtDec, TxtJan, TxtFeb, TxtMar, Convert.ToInt32(lblStartMonth.Text), Convert.ToInt32(lblEndMonth.Text));
               
                
            }
            else if (ddlType.SelectedValue == "3")
            {
               if (Convert.ToString(ViewState["BalSacha"]) != "1" && lblPhageFlag.Text == "1")
                {
                }
                else
                {
                    LoadDataEnable(TxtApr, TxtMay, TxtJun, TxtJul, TxtAug, TxtSep, TxtOct, TxtNov, TxtDec, TxtJan, TxtFeb, TxtMar, Convert.ToInt32(lblStartMonth.Text), Convert.ToInt32(lblEndMonth.Text));
                }
              
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
    public void LoadDataEnable(TextBox TxtApr, TextBox TxtMay, TextBox TxtJun, TextBox TxtJul, TextBox TxtAug, TextBox TxtSep, TextBox TxtOct, TextBox TxtNov, TextBox TxtDec, TextBox TxtJan, TextBox TxtFeb, TextBox TxtMar,int StartMonth, int EndMonth)
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
            if (StartMonth == 4)
            {
                TxtAug.Enabled = true;
            }
            if (StartMonth == 5)
            {
                TxtSep.Enabled = true;
            }
            if (StartMonth == 6)
            {
                TxtOct.Enabled = true;
            }
            if (StartMonth == 7)
            {
                TxtNov.Enabled = true;
                }
                if (StartMonth == 8)
                {
                    TxtDec.Enabled = true;
                }

                if (StartMonth == 9)
                {
                    TxtJan.Enabled = true;
                }
                if (StartMonth == 10)
                {
                    TxtFeb.Enabled = true;
                }
                if (StartMonth == 11)
                {

                    TxtMar.Enabled = true;
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

                if (TxtSep.Text!="")
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
        FillCBBock();
        Locking();
        GVMain.DataSource = null;
        GVMain.DataBind();
        GV_AnnualPlan.DataSource = null;
        GV_AnnualPlan.DataBind();
      //  ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "ddlTypeOnChangeEvent()", true);
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
            GV_AnnualPlan.DataSource=null;
            GV_AnnualPlan.DataBind();
             GVMain.DataSource=null;
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
             GV_AnnualPlan.DataSource=null;
             GV_AnnualPlan.DataBind();
             GVMain.DataSource=null;
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
            if (Convert.ToInt32(ddlYear.SelectedValue)>=2022)
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

    public void SaveData2020()
    {
     

    }

    public void SaveData()
    {
      

    }
    #endregion
}