using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;

public partial class FrmAnnualPlan : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    string conditions = "";
    DataTable dtSearchVill = null;
    public string RowNo = "", SchoolLeavel = "";
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                LoadYear();
                LoadUserLeavel();

            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }

        }
    }

    public void Locking()
    {
        if (ddlYear.SelectedIndex > 0)
        {

            btnDelete.Enabled = true;
            btnsave.Enabled = true;
            string strQry;
            strQry = "Select * from mstModuleLocking  where [FromName]='Annual Plan Entry' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "' ";


            DataTable dtModel = objMain.LoadData(strQry);
            if (dtModel.Rows.Count > 0)
            {
                if (Convert.ToInt32(dtModel.Rows[0]["LockMonth"].ToString()) < DateTime.Today.Month)
                {

                    btnsave.Enabled = false;
                    btnDelete.Enabled = false;



                }

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

        //string strQry = "select  VillageName as VillageName,  Name as   SchoolName,SchoolCode as DISECode,mstSchool.Villagecode from mstSchool inner join mst5Village on mst5Village.VillageCode=mstSchool.VillageCode " + str + "";
        string strQry = "select  VillageName as VillageName,  Name as   SchoolName,SchoolCode as DISECode,tblSummarySchoolAnualData.SchoolCode as DISECode,tblSummarySchoolAnualData.Villagecode,RowNo, SchooLevel FROM (tblSummarySchoolAnualData INNER JOIN mst5Village ON tblSummarySchoolAnualData.VillageCode = mst5Village.VillageCode)" + str + "";

        DataTable dtSchool = objComman.LoadData(strQry);
        if (dtSchool.Rows.Count > 0)
        {
            GVMain.DataSource = dtSchool;
            GVMain.DataBind();
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
            Condtion = Condtion + "and mst5Village.BlockCode='" + ddlBlock.SelectedValue.ToString() + "'";
        }

        if (ddlDistrict.SelectedValue != null && ddlDistrict.SelectedIndex >= 0)
        {
            Condtion = Condtion + "and mst5Village.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "'";
        }

        if (ddlVillage.SelectedValue != null && ddlVillage.SelectedIndex > 0)
        {
            Condtion = Condtion + "and mst5Village.VillageCode='" + ddlVillage.SelectedValue.ToString() + "'";
        }
        if (ViewState["SchoolId"].ToString().Length > 2)
        {
            strQry = " select Description,RowNo as LookupCode,  [Apr], [May], [Jun], [Jul], [Aug], [Sep], [Oct], [Nov], [Dec], [Jan], [Feb], [Mar],[RowNo] from tblAnualPlanDataDetail where SchoolCode='" + ViewState["SchoolId"].ToString() + "'  order by RowNo ";
        }
        else
        {
            strQry = " select Description,RowNo as LookupCode,  [Apr], [May], [Jun], [Jul], [Aug], [Sep], [Oct], [Nov], [Dec], [Jan], [Feb], [Mar],[RowNo] from tblAnualPlanDataDetail where VillageCode='" + ViewState["VillageCode"].ToString() + "'  order by RowNo ";
        }

        DataTable dtPreLoad = objComman.LoadData(strQry);
        if (dtPreLoad.Rows.Count > 0)
        {
            dtSearchVill = dtPreLoad.Copy();
            iCount = 1;
        }
        else
        {

            strQry = " select Description, LookupCode, 0 as [Apr],0 as [May],0 as [Jun],0 as [Jul],0 as [Aug],0 as [Sep],0 as [Oct],0 as [Nov],0 as [Dec],0 as [Jan],0 as [Feb],0 as [Mar] from mstLookup where LookupFlag='Anu' order by LookupCode ";

            dtSearchVill = objComman.LoadData(strQry);


        }
        if (dtSearchVill.Rows.Count > 0)
        {
            GV_AnnualPlan.DataSource = dtSearchVill;
            GV_AnnualPlan.DataBind();
        }
        string st = "select * from tblSummarySchoolAnualData where VillageCode='" + ViewState["VillageCode"].ToString() + "'";
        DataTable dtSchoolData = objComman.LoadData(st);
        strQry = "";
        Int32 A1 = 0; Int32 A2 = 0; Int32 A3 = 0; Int32 A4 = 0; Int32 CRITICALSIP = 0; Int32 OTHERSIP = 0; Int32 TOTALSIP = 0; Int32 SACQ1 = 0; Int32 SACQ2 = 0; Int32 SACQ3 = 0; Int32 SACQ4 = 0;
        if (ViewState["SchoolId"] != null || ViewState["SchoolId"] != "")
        {
            DataRow[] dr1 = dtSchoolData.Select("SchoolCode='" + ViewState["SchoolId"].ToString() + "'");

            DataRow[] dr4 = null;
            if (dr1.Length > 0 && Convert.ToInt32(dr1[0]["RowNo"]) == 1)
            {
                A1 = Convert.ToInt32(dr1[0]["SixOOSG"].ToString());
                A2 = Convert.ToInt32(dr1[0]["SevenOOSG"].ToString());
                A3 = Convert.ToInt32(dr1[0]["TotalOOSG"].ToString());
                A4 = Convert.ToInt32(dr1[0]["SevenOOSB"].ToString());

            }
            if (dr1.Length > 0)
            {
                CRITICALSIP = Convert.ToInt32(dr1[0]["CRITICALSIP"].ToString());
                OTHERSIP = Convert.ToInt32(dr1[0]["OTHERSIP"].ToString());
                TOTALSIP = Convert.ToInt32(dr1[0]["TOTALSIP"].ToString());
                ////SACQ1 = Convert.ToInt32(dr1[0]["SACQ1"].ToString());
                ////SACQ2 = Convert.ToInt32(dr1[0]["SACQ2"].ToString());
                ////SACQ3 = Convert.ToInt32(dr1[0]["SACQ3"].ToString());
                ////SACQ4 = Convert.ToInt32(dr1[0]["SACQ4"].ToString());
            }
        }
        if (ViewState["SchoolId"].ToString().Length < 2)
        {
            DataRow[] dr1 = dtSchoolData.Select("Villagecode='" + Convert.ToString(ViewState["VillageCode"]) + "'");

            DataRow[] dr4 = null;
            if (dr1.Length > 0 && Convert.ToInt32(dr1[0]["RowNo"]) == 1)
            {
                A1 = Convert.ToInt32(dr1[0]["SixOOSG"].ToString());
                A2 = Convert.ToInt32(dr1[0]["SevenOOSG"].ToString());
                A3 = Convert.ToInt32(dr1[0]["TotalOOSG"].ToString());
                A4 = Convert.ToInt32(dr1[0]["SevenOOSB"].ToString());
                RowNo = dr1[0]["RowNo"].ToString();
                SchoolLeavel = dr1[0]["SchooLevel"].ToString();

            }

        }
        if (dtSearchVill.Rows.Count > 0)
        {


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


                if (dtSearchVill.Rows[i]["Description"].ToString() == "TB Need" && RowNo == "1")
                {
                    TxtApr.Enabled = true;
                }
                if (dtSearchVill.Rows[i]["Description"].ToString() == "SMC Meet cum Orient")
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
                if ((dtSearchVill.Rows[i]["Description"].ToString() == "TB VE TRAINING" && RowNo == "1") || (dtSearchVill.Rows[i]["Description"].ToString() == "TB Train GKP" && RowNo == "1"))
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
                if (dtSearchVill.Rows[i]["Description"].ToString() == "6 yrs OOSG")
                {

                    TxtApr.Text = A1.ToString();



                }
                if (dtSearchVill.Rows[i]["Description"].ToString() == "7-14 yrs OOSG")
                {


                    TxtApr.Text = A2.ToString();


                }

                if (dtSearchVill.Rows[i]["Description"].ToString() == "Total OOSG")
                {


                    TxtApr.Text = A3.ToString();


                }

                if (dtSearchVill.Rows[i]["Description"].ToString() == "7-14 yrs OOSB")
                {


                    TxtApr.Text = A4.ToString();


                }
                if (dtSearchVill.Rows[i]["Description"].ToString() == "CRITICAL SIP")
                {
                    TxtMay.Text = CRITICALSIP.ToString();
                    //GV_AnnualPlan.Rows[i].Cells["May"].Value = CRITICALSIP;
                }
                if (dtSearchVill.Rows[i]["Description"].ToString() == "OTHER SIP")
                {
                    TxtMay.Text = OTHERSIP.ToString();
                    //  GV_AnnualPlan.Rows[i].Cells["May"].Value = OTHERSIP;
                }
                if (dtSearchVill.Rows[i]["Description"].ToString() == "TOTAL SIP TGT")
                {
                    TxtMay.Text = TOTALSIP.ToString();
                    //GV_AnnualPlan.Rows[i].Cells["May"].Value = TOTALSIP;
                }

                if (ViewState["SchoolId"].ToString().Length > 2 || ViewState["SchoolId"].ToString().Length > 2)
                {
                    if (dtSearchVill.Rows[i]["Description"].ToString() == "SAC UPDATE")
                    {


                        TxtJul.Text = "1";
                        TxtOct.Text = "1";

                        TxtJan.Text = "1";

                        TxtMar.Text = "1";


                    }
                    if (dtSearchVill.Rows[i]["Description"].ToString() == "Retention/SIP Data")
                    {
                        TxtApr.Enabled = true;
                        TxtMay.Enabled = true;
                        TxtJun.Enabled = true;
                        TxtJul.Enabled = true;
                    }

                }

                if (dtSearchVill.Rows[i]["Description"].ToString() == "TB TRAIN ENR+SMC" && RowNo == "1")
                {
                    TxtApr.Enabled = true;
                    TxtMay.Enabled = true;
                    TxtJun.Enabled = true;
                    TxtJul.Enabled = true;
                }
                if (dtSearchVill.Rows[i]["Description"].ToString() == "GSS ENR" && RowNo == "1")
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



                }
                if (dtSearchVill.Rows[i]["Description"].ToString() == "TB Train BS+LSG" )
                {
                    if (dtSearchVill.Rows[i]["Description"].ToString() == "TB Train BS+LSG" && RowNo == "1")
                    {
                        TxtMay.Enabled = true;
                        TxtJun.Enabled = true;
                        TxtJul.Enabled = true;
                        TxtAug.Enabled = true;
                        TxtSep.Enabled = true;
                        TxtOct.Enabled = true;
                        TxtNov.Enabled = true;
                        TxtDec.Enabled = true;
                        TxtJan.Enabled = true;
                    }

                    else
                    {
                        TxtMay.Enabled = false   ;
                        TxtJun.Enabled = false;
                        TxtJul.Enabled = false;
                        TxtAug.Enabled = false;
                        TxtSep.Enabled = false;
                        TxtOct.Enabled = false;
                        TxtNov.Enabled = false;
                        TxtDec.Enabled = false;
                        TxtJan.Enabled = false;

                    }

                }
              

                if (dtSearchVill.Rows[i]["Description"].ToString() == "MM ENR" && RowNo == "1")
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


                }
                if (dtSearchVill.Rows[i]["Description"].ToString() == "GSS RETENTION" || dtSearchVill.Rows[i]["Description"].ToString() == "MM RETENTION" && RowNo == "1")
                {
                    TxtOct.Enabled = true;
                    TxtNov.Enabled = true;
                    TxtDec.Enabled = true;
                    TxtJan.Enabled = true;
                    TxtFeb.Enabled = true;
                    TxtMar.Enabled = true;


                }
                if (dtSearchVill.Rows[i]["Description"].ToString() == "LEARNING BASELINE")
                {

                    #region LEARNING
                    TxtJul.Enabled = true;
                    TxtAug.Enabled = true;
                    //GV_AnnualPlan.Rows[i].Cells["Aug"].ReadOnly = false;
                    TxtSep.Enabled = true;


                    #endregion

                }
                if (dtSearchVill.Rows[i]["Description"].ToString() == "Bal Sabha" && SchoolLeavel == "2" || SchoolLeavel == "5")
                {

                    #region LEARNING
                    TxtJul.Enabled = true;
                    TxtAug.Enabled = true;
                    //GV_AnnualPlan.Rows[i].Cells["Aug"].ReadOnly = false;
                    TxtSep.Enabled = true;

                    #endregion

                }






                //if (dtSearchVill.Rows[i]["Description"].ToString() == "LEARNING ENDLINE")
                //{
                //    TxtMar.Text = "1";
                //    //GV_AnnualPlan.Rows[i].Cells["Mar"].Value = 1;
                //}





            }

        }

    }
    #endregion
    #region Button Click Events
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        Bindgrid();
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        SaveData();
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
            string SchoolId = GVMain.DataKeys[iIndex]["DISECode"].ToString();
            string VillageCode = GVMain.DataKeys[iIndex]["VillageCode"].ToString();
            RowNo = GVMain.DataKeys[iIndex]["RowNo"].ToString();
            SchoolLeavel = GVMain.DataKeys[iIndex]["SchooLevel"].ToString();
            ViewState["SchoolId"] = SchoolId;
            ViewState["VillageCode"] = VillageCode;
            ViewState["RowNo"] = RowNo;
            ViewState["SchoolLeavel"] = SchoolLeavel;
            LoadData();
            ViewState["Save"] = "Edit";

            pnlMain.Enabled = true;

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
            if (e.Row.Cells[0].Text == "6 yrs OOSG")
            {
                e.Row.Enabled = false;
            }
            else if (e.Row.Cells[0].Text == "7-14 yrs OOSG")
            {
                e.Row.Enabled = false;
            }
            else if (e.Row.Cells[0].Text == "Total OOSG")
            {
                e.Row.Enabled = false;
            }
            else if (e.Row.Cells[0].Text == "7-14 yrs OOSB")
            {
                e.Row.Enabled = false;
            }
            else if (e.Row.Cells[0].Text == "CRITICAL SIP")
            {
                e.Row.Enabled = false;
            }
            else if (e.Row.Cells[0].Text == "OTHER SIP")
            {
                e.Row.Enabled = false;
            }
            else if (e.Row.Cells[0].Text == "TOTAL SIP TGT")
            {
                e.Row.Enabled = false;
            }
            else if (e.Row.Cells[0].Text == "SAC UPDATE")
            {
                e.Row.Enabled = false;
            }
            else if (e.Row.Cells[0].Text == "TB TRAIN")
            {
                e.Row.Enabled = false;
            }
            else if (e.Row.Cells[0].Text == "ENR+SMC")
            {
                e.Row.Enabled = false;
            }
            else if (e.Row.Cells[0].Text == "GSS ENR")
            {
                e.Row.Enabled = false;
            }
            else if (e.Row.Cells[0].Text == "MM ENR")
            {
                e.Row.Enabled = false;
            }
        }
    }
    #endregion
    #region Selected Index Changed Events
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        // pnlMain.Enabled = false;
        // GVMain.Enabled = false;
        FillCBDist();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBBock();
        Locking();
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBCluster();
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillage();
    }
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
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

    }
    #endregion
    #region Save
    public void SaveData()
    {
     

    }
    #endregion


    public void LEARNINGMIDLINE(Int32 jul, Int32 Sep, Int32 Aug)
    {

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
            if (dtSearchVill.Rows[i]["Description"].ToString() == "LEARNING MIDLINE")
            {
                TxtNov.Text = Convert.ToString(jul);
                TxtDec.Text = Convert.ToString(Aug);
                TxtJan.Text = Convert.ToString(Sep);
            }
            if (dtSearchVill.Rows[i]["Description"].ToString() == "GKP Sessions")
            {
                TxtJul.Text = "0";
                TxtSep.Text = "0";
                TxtAug.Text = "0";
                TxtNov.Text = "0";
                TxtDec.Text = "0";
                TxtJan.Text = "0";
                TxtFeb.Text = "0";
                TxtMar.Text = "0";
                if (jul > 0)
                {
                    TxtJul.Enabled = false;
                    TxtAug.Enabled = false;
                    TxtSep.Enabled = false;
                    TxtOct.Enabled = false;
                    TxtNov.Enabled = false;
                    TxtDec.Enabled = false;
                    TxtJan.Enabled = false;
                    TxtFeb.Enabled = false;
                    TxtMar.Enabled = false;
                }
                if (Sep > 0)
                {
                    TxtJul.Enabled = true;
                    TxtAug.Enabled = true;
                    TxtSep.Enabled = false;
                    TxtOct.Enabled = false;
                    TxtNov.Enabled = false;
                    TxtDec.Enabled = false;
                    TxtJan.Enabled = false;
                    TxtFeb.Enabled = false;
                    TxtMar.Enabled = false;
                }
                if (Aug > 0)
                {
                    TxtJul.Enabled = true;
                    TxtAug.Enabled = false;
                    TxtSep.Enabled = false;
                    TxtOct.Enabled = false;
                    TxtNov.Enabled = false;
                    TxtDec.Enabled = false;
                    TxtJan.Enabled = false;
                    TxtFeb.Enabled = false;
                    TxtMar.Enabled = false;
                }
            }


        }
    }
    public void BALSab(Int32 jul, Int32 Sep, int Aug)
    {

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

            if (dtSearchVill.Rows[i]["Description"].ToString() == "LSG")
            {
                TxtJul.Text = "0";
                TxtAug.Text = "0";
                TxtSep.Text = "0";
                TxtOct.Text = "0";
                TxtNov.Text = "0";
                TxtDec.Text = "0";
                TxtJan.Text = "0";
                TxtFeb.Text = "0";
                TxtMar.Text = "0";
                TxtApr.Text = "0";
                TxtMay.Text = "0";
                TxtJun.Text = "0";
                if (jul > 0)
                {
                    TxtJul.Enabled = false;
                    TxtAug.Enabled = false;
                    TxtSep.Enabled = false;
                    TxtOct.Enabled = false;
                    TxtNov.Enabled = false;
                    TxtDec.Enabled = false;
                    TxtJan.Enabled = false;
                    TxtFeb.Enabled = false;
                    TxtMar.Enabled = false;
                    TxtApr.Enabled = true;
                    TxtMay.Enabled = true;
                    TxtJun.Enabled = true;
                }
                if (Sep > 0)
                {
                    TxtJul.Enabled = true;
                    TxtAug.Enabled = true;
                    TxtSep.Enabled = false;
                    TxtOct.Enabled = false;
                    TxtNov.Enabled = false;
                    TxtDec.Enabled = false;
                    TxtJan.Enabled = false;
                    TxtFeb.Enabled = false;
                    TxtMar.Enabled = false;
                    TxtApr.Enabled = true;
                    TxtMay.Enabled = true;
                    TxtJun.Enabled = true;
                }
                if (Aug > 0)
                {

                    TxtJul.Enabled = true;
                    TxtAug.Enabled = false;
                    TxtSep.Enabled = false;
                    TxtOct.Enabled = false;
                    TxtNov.Enabled = false;
                    TxtDec.Enabled = false;
                    TxtJan.Enabled = false;
                    TxtFeb.Enabled = false;
                    TxtMar.Enabled = false;
                    TxtApr.Enabled = true;
                    TxtMay.Enabled = true;
                    TxtJun.Enabled = true;
                }
            }


        }
    }
    public void TbNeed(Int32 TBNeed)
    {

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
            if (dtSearchVill.Rows[i]["Description"].ToString() == "TB HH")
            {
                TxtJul.Text = Convert.ToString(TBNeed * 2);
                TxtAug.Text = Convert.ToString(TBNeed * 2);
                TxtSep.Text = Convert.ToString(TBNeed * 2);
                TxtOct.Text = Convert.ToString(TBNeed * 2);
                TxtNov.Text = Convert.ToString(TBNeed * 2);
                TxtDec.Text = Convert.ToString(TBNeed * 2);
                TxtJan.Text = Convert.ToString(TBNeed * 2);
                TxtFeb.Text = Convert.ToString(TBNeed * 2);
                TxtMar.Text = Convert.ToString(TBNeed * 2);
                TxtApr.Text = Convert.ToString(TBNeed * 2);
                TxtMay.Text = Convert.ToString(TBNeed * 2);
                TxtJun.Text = Convert.ToString(TBNeed * 2);
            }



        }
    }
}