using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class frmD2dSurvey : System.Web.UI.Page
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
                //SqlConnection conn = new SqlConnection("Data Source=EducateGirls.db.3975866.hostedresource.com;Initial Catalog=EducateGirls;User=educategirls;Password=mw2Master1EG0!");
                //conn.Open();

                //GVMainBind();
                LoadYear();
                LoadUserLeavel();
                //FillSocialCat();

                //ViewState["Save"] = "Save";
                //FillFaimlyCat();
                //FillEdu();
                //FillSours();
                //FillReasone();
                //btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
                FillAgeProof();
                FillSocialCat();
                FillFaimlyCat();
                FillEnrollStatus();
                FillEnrollCat();
                FillClass();
                FillReasone();
                ViewState["Save"] = "Save";
                UserLevelFilter();


                ViewState["M"] = "";
                btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }

        }
    }
    public DataTable CreateDataTable()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYear.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYear;
    }
    public void LoadYear()
    {
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
        //            //get last  two digits (eg: 10 from 2010);
        //            dr = dtYear.NewRow();
        //            dr["Type"] = GivenYear - 2 + "-" + Convert.ToString((GivenYear - 2 + 1));
        //            dr["ID"] = y - 2;
        //            dtYear.Rows.Add(dr);
        //        }
        //        else
        //        {
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
        DataTable dtYear = objComman.Generate_Financial_Year();

        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        ddlYear_SelectedIndexChanged(ddlYear, null);
        //}


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

    public void Locking()
    {
        if (ddlYear.SelectedIndex > 0)
        {

            btnAdd.Enabled = true;
            btnsave.Enabled = true;

            string strQry;
            if (Session["FinYear"].ToString() != ddlYear.SelectedItem.Text)
            {

                strQry = "Select * from mstModuleLocking  where [FromName]='D2D' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "' ";


                DataTable dtModel = objMain.LoadData(strQry);
                if (dtModel.Rows.Count > 0)
                {

                    if (Convert.ToInt32(dtModel.Rows[0]["LockMonth"].ToString()) < DateTime.Today.Month)
                    {
                        btnAdd.Enabled = false;
                        btnsave.Enabled = false;
                        btnDelete.Enabled = false;

                        ViewState["M"] = "M";

                    }

                }

            }
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
    public void UserLevelFilter()
    {

        string strQry = "";
        string Cond = "Module='D2D'";
        strQry = "Select * from MstUserRight  where " + Cond + " and Role_Id=" + Session["user_level"].ToString() + "   ";


        DataTable dtRole = objMain.LoadData(strQry);

        if (dtRole.Rows.Count > 0)
        {
            vADD = Convert.ToBoolean(dtRole.Rows[0]["AddStatus"].ToString());
            vVerify = Convert.ToBoolean(dtRole.Rows[0]["verify_Status"].ToString());
            vDelete = Convert.ToBoolean(dtRole.Rows[0]["Delete_status"].ToString());

            ViewState["vADD"] = vADD;
            ViewState["vVerify"] = vVerify;
            ViewState["vDelete"] = vDelete;
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
            btnAdd.Enabled = true;
            btnsave.Enabled = true;
            FromRight(true);
            lblMain.Text = "DOOR-TO-DOOR  SURVEY";
        }
        else
        {
            btnAdd.Enabled = false;

        }
        if (vVerify == true)
        {

            btnsave.Enabled = true;
            // FromRight(false);
            txtChildName.Enabled = true;
            txtFatherName.Enabled = true;
            txtAge.Enabled = true;
            ddlGender.Enabled = true;
            txtDate.Enabled = true;
            cmbAgeproof.Enabled = true;
            txtHouse.Enabled = true;
            cmbReason.Enabled = true;
            //txtChildName.BackColor = Color.LightYellow;
            //txtFatherName.BackColor = Color.LightYellow;
            //txtAge.BackColor = Color.LightYellow;

            //ddlGender.BackColor = Color.LightYellow;
            //txtSarveyDate.BackColor = Color.LightYellow;
            //cmbAgeproof.BackColor = Color.LightYellow;
            //txtHouse.BackColor = Color.LightYellow;
            //cmbReason.BackColor = Color.LightYellow;
            //ddlDob.BackColor = Color.LightYellow;
            lblMain.Text = "DOOR-TO-DOOR  SURVEY(VERIFY)";

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

    public void FromRight(Boolean EnableDisable)
    {


        txtSarveyDate.Enabled = EnableDisable;
        txtChildName.Enabled = EnableDisable;
        txtFatherName.Enabled = EnableDisable;
        txtAge.Enabled = EnableDisable;
        cmbSchool.Enabled = EnableDisable;
        ddlClass.Enabled = EnableDisable;
        cmbReason.Enabled = EnableDisable;
        ddlGender.Enabled = EnableDisable;


        txtDate.Enabled = EnableDisable;
        txtAge.Enabled = EnableDisable;
        cmbSchool.Enabled = EnableDisable;
        cmbAgeproof.Enabled = EnableDisable;
        ddloccu.Enabled = EnableDisable;
        ddlCategory.Enabled = EnableDisable;

        ddlEducation.Enabled = EnableDisable;

        cmbReason.Enabled = EnableDisable;

        cmbReason.Enabled = EnableDisable;
        txtHouse.Enabled = EnableDisable;
        txtMauhalla.Enabled = EnableDisable;


    }
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
            conditions = "StateCode ='" + ddlState.SelectedValue + "'  and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
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
            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }





    }

    public void FillAgeProof()
    {
        conditions = "";
        conditions = "LookupFlag ='AGE' and Active=1";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", cmbAgeproof, "Description", "LookupCode", "Select");



    }
    public void FillSocialCat()
    {
        conditions = "";
        conditions = "LookupFlag ='CAT' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlCategory, "Description", "LookupCode", "Select");
    }
    public void FillFaimlyCat()
    {
        conditions = "";
        conditions = "LookupFlag ='FO' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddloccu, "Description", "LookupCode", "Select");
    }
    public void FillEnrollStatus()
    {
        conditions = "";
        conditions = "LookupFlag ='ES' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlEducation, "Description", "LookupCode", "Select");

    }

    public void FillEnrollCat()
    {
        conditions = "";
        conditions = "LookupFlag ='EC' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "Description", "asc", ddlEnrollCat, "Description", "LookupCode", "Select");
    }

    public void FillClass()
    {
        conditions = "";
        conditions = "LookupFlag ='CL' and Active=1";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlClass, "Description", "LookupCode", "Select");

    }

    public void FillReasone()
    {
        conditions = "";
        conditions = "LookupFlag ='RE' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", cmbReason, "Description", "LookupCode", "Select");
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


        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");



    }



    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlMain.Enabled = false;
        GVMain.Enabled = false;
        FillCBDist();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        Locking();
        pnlMain.Enabled = false;
        GVMain.Enabled = false;
        FillCBBock();
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlMain.Enabled = false;
        GVMain.Enabled = false;
        FillCBCluster();
        Locking();
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlMain.Enabled = false;
        GVMain.Enabled = false;
        FillCVillage();
    }
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlMain.Enabled = false;
        GVMain.Enabled = false;
        Session["Serial"] = "";
        Unique();
        FillSchool();
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
        objComman.BindDLLSelectAll("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "Select");



    }
    public void FillCVillage()
    {
        conditions = "";
        ////conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        ////objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--Select--");

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

    private void GVMainBind()
    {

        string str = "";
        if (ddlYear.SelectedIndex > 0)
        {
            str = "where mst5Village.Fyear='" + ddlYear.SelectedItem.Text.ToString() + "'";
        }
        if (ddlState.SelectedValue != null && ddlState.SelectedIndex > 0)
        {
            str += "and mst5Village.StateCode='" + ddlState.SelectedValue.ToString() + "'";
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
        else
        {
            str = str + "and mst5Village.VillageCode='" + 0 + "'";
        }

        DataTable dtmstM = objMain.LoadData(" Select tblDTD.UniqueCode,HHNo as HHNo,ChildName from tblDTD  inner join mst5Village on mst5Village.VillageCode=tblDTD.VillageCode  left join mst1State on mst1State.StateCode=mst5Village.StateCode left join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode left join mst3Block on mst3Block.BlockCode=mst5Village.BlockCode left join mstPanchayat on mstPanchayat.PanchayatCode=mst5Village.PanchayatCode " + str + " and DeleteFlag=1 and Status=1 order by ChildName ");

        //DataTable dt = SqlHelper.GetDataTable(strcon, CommandType.Text, "select schoolcode, Name,PrincipalName,PrincipalContact from mstSchool");
        if (dtmstM.Rows.Count > 0)
        {
            GVMain.DataSource = dtmstM;
            GVMain.DataBind();
            ViewState["Serach"] = dtmstM;
        }
        else
        {
            GVMain.DataSource = null;
            GVMain.DataBind();
            ViewState["Serach"] = "";
        }

    }

    protected void txtSearchName_Click(object sender, EventArgs e)
    {
        DataTable dt = ViewState["Serach"] as DataTable;
        string strFilter = "";

        string str = "ChildName";
        string str1 = "HHNo";

        DataTable dtfilter = dt.Copy();


        strFilter = str + " like '%" + txtSearchName.Text.Trim() + "%'   ";
        strFilter += " or " + str1 + " like '%" + txtSearchName.Text.Trim() + "%'   ";

        //dtSoSaleOrder.Select(txtSearch.SelectedValue.ToString() + " like or '" + txtSearch.Text + "%'";


        dtfilter.DefaultView.RowFilter = strFilter;
        dtfilter.DefaultView.Sort = "ChildName asc";
        GVMain.DataSource = dtfilter.DefaultView.ToTable();
        GVMain.DataBind();

    }

    protected void GVMain_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "GVUIO")
        {
            int iIndex = Convert.ToInt32(e.CommandArgument);
            string TBCode = GVMain.DataKeys[iIndex]["UniqueCode"].ToString();
            FillD2dData(TBCode);
            ViewState["Save"] = "Update";


            if (ViewState["M"].ToString() == "M")
            {
                pnlMain.Enabled = true;
                btnAdd.Enabled = false;
                btnsave.Enabled = false;
                btnDelete.Enabled = false;

            }
            if (Convert.ToString(Session["UNI"]) == "UNI")
            {
                pnlMain.Enabled = false;
                btnAdd.Enabled = false;
                btnsave.Enabled = false;
                btnDelete.Enabled = false;

            }
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
    public void FillSchoolEdit(string SchoolCode)
    {
        conditions = "";
        string strQry = " select SchoolCode,Name from mstSchool where VillageCode ='" + ddlVillage.SelectedValue + "' and FYear ='" + ddlYear.SelectedItem.Text + "' Union select SchoolCode,Name from mstSchool where SchoolCode ='" + SchoolCode + "' ";
        // objComman.BindDLLSchool("mstSchool", "SchoolCode,Name", conditions, "Name", "asc", cmbSchool, "Name", "SchoolCode", "Select");
        //  string strQry = "  SELECT mst5Village.VillageCode, dbo.TitleCase(upper((mst5Village.VillageName))) + ' (' + dbo.TitleCase(upper(mstPanchayat.PanchayatName)) +')'   as VillageName FROM mst5Village INNER JOIN mstPanchayat ON mst5Village.PanchayatCode = mstPanchayat.PanchayatCode where " + conditions + "  order by VillageName   ";
        DataTable dtSchool = objMain.LoadData(strQry);

        BindDLLMasterTableVillage("mstSchool", "Name,SchoolCode", dtSchool, conditions, "Name", "asc", cmbSchool, "Name", "SchoolCode", "Select");



    }
    public bool BindDLLMasterTableVillage(string dtname, string fieldname, DataTable dt, string Condition, string orberbyfield, string orderby, DropDownList ddl, string textData, string valData, string ZeroIndex)
    {
        bool status = false;
        string conditions = Condition == "" ? "" : " where " + Condition;
        string orberbyfields = orberbyfield == "" ? "" : " order by " + orberbyfield;
        string orderbys = orderby == "" ? "" : orderby;


        //string strQry = "Select  distinct " + fieldname + " from " + dtname + " " + conditions + " " + orberbyfields + " " + orderbys + "";
        //DataTable dt = dbt.VGridFill(strQry);
        if (ZeroIndex != "")
        {
            DataRow dr;
            dr = dt.NewRow();
            dr[textData] = "--" + ZeroIndex + "--";
            dr[valData] = "0";
            dt.Rows.InsertAt(dr, 0);
            dt.AcceptChanges();
        }
        if (dt.Rows.Count > 0)
        {
            if (ZeroIndex != "")
            {
                DataRow dr;
                dr = dt.NewRow();
                dr[textData] = "--" + "Other" + "--";
                dr[valData] = "99";
                dt.Rows.InsertAt(dr, dt.Rows.Count + 1);
                dt.AcceptChanges();
            }
        }
        else
        {
            DataRow dr;
            dr = dt.NewRow();
            dr[textData] = "--" + "Other" + "--";
            dr[valData] = "99";
            dt.Rows.InsertAt(dr, 1);
            dt.AcceptChanges();
        }
        if (dt.Rows.Count > 0)
        {
            ddl.DataTextField = textData;
            ddl.DataValueField = valData;

            ddl.DataSource = dt;
            ddl.DataBind();
            status = true;
        }
        return status;

    }

    public void FillSchool()
    {
        conditions = "";
        conditions = "VillageCode ='" + ddlVillage.SelectedValue + "' and FYear ='" + ddlYear.SelectedItem.Text + "' ";


        objComman.BindDLLSchool("mstSchool", "SchoolCode,Name", conditions, "Name", "asc", cmbSchool, "Name", "SchoolCode", "Select");



    }
    public void FillD2dData(string SerialNo)
    {
        DataTable dt = new DataTable();
        //if (Convert.ToBoolean(ViewState["vVerify"].ToString()) == true)
        //{
        //    dt = objMain.LoadData("  Select tblDTD.[UniqueCode],EnrollStatus,mstSchool.DISECode,FamilyOccupationOther,SchoolOther,mst5Village.[EGVillageCode] as VillageCode,AgeProofOther, EnrollStatus, Migration,SurvayDate,DoChild1 as DoChild,AsOnDate1 as AsOnDate,Mauhalla1 as Mauhalla,[Serial],HHNo1 as [HHNo],SocialCategory1 as [SocialCategory],FamilyOccupation1 as [FamilyOccupation],[ChildName1] as ChildName,[FathersName1] as FathersName,Gender1 as [Gender],DOBAvailable1 as [DOBAvailable],DOB1 as [DOB],AgeAson1 as [AgeAson],AgeProof1 as [AgeProof],EduationStatus1 as [EduationStatus],School1 as [School],ReasonDO_NE1 as [ReasonDO_NE],[MigrationDuration],[EnrolmentCategory], mst5Village.PanchayatCode,mst5Village.BlockCode,mst5Village.DistrictCode,mstSchool.Name,tblDTD.Status FROM (mst5Village INNER JOIN tblDTD ON mst5Village.VillageCode = tblDTD.VillageCode) left JOIN mstSchool ON tblDTD.School = mstSchool.SchoolCode where tblDTD.UniqueCode='" + SerialNo + "'  ");

        //}
        //else
        //{
        dt = objMain.LoadData("  Select tblDTD.[UniqueCode],EnrollStatus,mstSchool.DISECode,FamilyOccupationOther,SchoolOther,mst5Village.[EGVillageCode] as VillageCode,AgeProofOther,EnrollStatus,Migration,SurvayDate,DoChild,AsOnDate,Mauhalla,[Serial],[HHNo],[SocialCategory],[FamilyOccupation],[ChildName] as ChildName,[FathersName] as FathersName,[Gender],[DOBAvailable],[DOB],[AgeAson],[AgeProof],[EduationStatus],[School],[ReasonDO_NE],[MigrationDuration],[EnrolmentCategory], mst5Village.PanchayatCode,mst5Village.BlockCode,mst5Village.DistrictCode,mstSchool.Name,tblDTD.Status FROM (mst5Village INNER JOIN tblDTD ON mst5Village.VillageCode = tblDTD.VillageCode) left JOIN mstSchool ON tblDTD.School = mstSchool.SchoolCode where tblDTD.UniqueCode='" + SerialNo + "'  ");

        //}
        if (dt.Rows.Count > 0)
        {


            ViewState["ChildId"] = SerialNo;
            if (dt.Rows[0]["Status"].ToString() == "2" || dt.Rows[0]["EnrollStatus"].ToString() == "2" || dt.Rows[0]["EnrollStatus"].ToString() == "4" || dt.Rows[0]["EnrollStatus"].ToString() == "3")
            {

                btnsave.Enabled = false;
                btnDelete.Enabled = false;


            }
            else
            {
                if (ViewState["vDelete"].ToString() == "True")
                {
                    btnDelete.Enabled = true;
                }
                else
                {
                    btnDelete.Enabled = false;
                }
                btnsave.Enabled = true;
            }

            //CBDistrictSearch.SelectedValue = dt.Rows[0]["DistrictCode"].ToString();
            //CBBlock.SelectedValue = dt.Rows[0]["BlockCode"].ToString();
            //CBCluster.SelectedValue = dt.Rows[0]["PanchayatCode"].ToString();
            //CBVillage.SelectedValue = dt.Rows[0]["VillageCode"].ToString();
            FillSchoolEdit(dt.Rows[0]["School"].ToString());
            txtNewSchool.Text = dt.Rows[0]["SchoolOther"].ToString();
            if (dt.Rows[0]["Serial"].ToString().Length.ToString() == "1")
            {
                txtUnique.Text = dt.Rows[0]["VillageCode"].ToString() + "-" + 0 + 0 + dt.Rows[0]["Serial"].ToString();
            }
            else
            {
                txtUnique.Text = dt.Rows[0]["VillageCode"].ToString() + "-" + 0 + dt.Rows[0]["Serial"].ToString();
            }
            txtChildName.Text = dt.Rows[0]["ChildName"].ToString();
            txtFatherName.Text = dt.Rows[0]["FathersName"].ToString();

            txtAge.Text = dt.Rows[0]["AgeAson"].ToString();

            ddlGender.SelectedValue = dt.Rows[0]["Gender"].ToString();

            //DTPicker_DOB.Format = DateTimePickerFormat.Custom;
            //DTPicker_DOB.CustomFormat = "dd/MM/yyyy ";
            if (dt.Rows[0]["DOBAvailable"].ToString() == "True")
            {
                lblDob.Text = "DOB";
                lblAge.Enabled = false;
                txtAge.Enabled = false;

                txtDate.Enabled = true;

                ddlDob.SelectedValue = "1";
                txtAge.Text = dt.Rows[0]["AgeAson"].ToString();
                DateTime DOB = Convert.ToDateTime(dt.Rows[0]["DOB"].ToString());


                txtDate.Text = DOB.ToString("dd/MM/yyy");

            }
            else
            {
                txtDate.Enabled = false;

                lblDob.Text = "As On";
                lblAge.Enabled = true;
                txtAge.Enabled = true;

                ddlDob.SelectedValue = "2";
                txtAge.Text = dt.Rows[0]["AgeAson"].ToString();
                DateTime DOB = Convert.ToDateTime(dt.Rows[0]["AsOnDate"].ToString());
                txtDate.Text = DOB.ToString("dd/MM/yyy");

            }
            ViewState["Save"] = "Update";
            DateTime SurvayDate = Convert.ToDateTime(dt.Rows[0]["SurvayDate"].ToString());

            txtSarveyDate.Text = SurvayDate.ToString("dd/MM/yyy");
            txtOtherAge.Text = dt.Rows[0]["AgeProofOther"].ToString();
            cmbAgeproof.SelectedValue = dt.Rows[0]["AgeProof"].ToString();
            ddloccu.SelectedValue = dt.Rows[0]["FamilyOccupation"].ToString();
            if (ddloccu.SelectedValue == "6")
            {
                txtFOther.Enabled = true;
                txtFOther.Text = dt.Rows[0]["FamilyOccupationOther"].ToString();
            }
            else
            {
                txtFOther.Enabled = false;
                txtFOther.Text = "";
            }
            ddlCategory.SelectedValue = dt.Rows[0]["SocialCategory"].ToString();
            txtMigration.Text = dt.Rows[0]["MigrationDuration"].ToString();
            ddlEducation.SelectedValue = dt.Rows[0]["EduationStatus"].ToString();
            ddlEnrollCat.SelectedValue = dt.Rows[0]["EnrolmentCategory"].ToString();
            cmbReason.SelectedValue = dt.Rows[0]["ReasonDO_NE"].ToString();

            if (dt.Rows[0]["DISECode"].ToString() != "0" && dt.Rows[0]["DISECode"].ToString() != "")
            {
                cmbSchool.SelectedValue = dt.Rows[0]["School"].ToString();
            }
            if (dt.Rows[0]["School"].ToString() == "99")
            {
                cmbSchool.SelectedValue = dt.Rows[0]["School"].ToString();
            }
            //lblschoolId.Text = dt.Rows[0]["School"].ToString();
            txtHouse.Text = dt.Rows[0]["HHNo"].ToString();
            txtMauhalla.Text = dt.Rows[0]["Mauhalla"].ToString();
            ddlClass.SelectedValue = dt.Rows[0]["DoChild"].ToString();
            //if (dt.Rows[0]["ReasonDO_NE"].ToString() == "0")
            //{
            //    txtReason.Text = "";

            //}
            //else
            //{
            //    txtReason.Text = dt.Rows[0]["ReasonDO_NE"].ToString();
            //}
            if (Convert.ToInt32(ddlEducation.SelectedValue) > 0)
            {
                if (Convert.ToInt32(ddlEducation.SelectedValue) == 1 || Convert.ToInt32(ddlEducation.SelectedValue) == 2)
                {

                    cmbSchool.Enabled = true;
                    ddlClass.Enabled = true;


                }
                else
                {


                    cmbSchool.Enabled = false;
                    ddlClass.Enabled = false;




                }

                if (Convert.ToInt32(ddlEducation.SelectedValue) == 1)
                {
                    cmbReason.Enabled = false;

                    ddlEnrollCat.Enabled = false;

                }
                else
                {


                    ddlEnrollCat.Enabled = true;

                    cmbReason.Enabled = true;
                }

            }

            else
            {
                cmbSchool.Enabled = false;
                ddlClass.Enabled = false;
                cmbReason.Enabled = false;
            }


            if (dt.Rows[0]["HHNo"].ToString() == "0")
            {
                txtHouse.Text = "";
            }
            else
            {
                txtHouse.Text = dt.Rows[0]["HHNo"].ToString();
            }

            if (Convert.ToInt32(cmbReason.SelectedValue) == 1)
            {
                txtMigration.Enabled = true;

            }
            else
            {
                txtMigration.Enabled = false;

            }

            if (cmbSchool.SelectedIndex > 0)
            {
                //lblschoolId.Text = cmbSchool.SelectedValue.ToString();

                if (cmbSchool.SelectedValue == "99")
                {
                    txtNewSchool.Enabled = true;
                }
                else
                {
                    txtNewSchool.Enabled = false;
                }
            }
            else
            {
                txtNewSchool.Enabled = false;
            }

            if (Convert.ToInt32(cmbAgeproof.SelectedValue) == 5)
            {
                txtOtherAge.Enabled = true;
            }
            else
            {
                txtOtherAge.Enabled = false;
            }
        }
    }

    protected void cmbAgeproof_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(cmbAgeproof.SelectedValue) == 5)
        {
            txtOtherAge.Enabled = true;
        }
        else
        {
            txtOtherAge.Enabled = false;
        }
    }
    protected void cmbSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbSchool.SelectedIndex > 0)
        {
            //lblschoolId.Text = cmbSchool.SelectedValue.ToString();

            if (cmbSchool.SelectedValue == "99")
            {
                txtNewSchool.Enabled = true;
            }
            else
            {
                txtNewSchool.Enabled = false;
                txtNewSchool.Text = "";
            }
        }
        else
        {
            txtNewSchool.Enabled = false;
        }
    }
    protected void ddlDob_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDob.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlDob.SelectedValue) == 1)
            {
                lblDob.Text = "DOB";
                lblAge.Enabled = false;
                txtAge.Enabled = false;
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDate.Enabled = true;
                txtAge.Text = "";
            }
            else
            {
                txtDate.Enabled = false;
                DateTime ydate = new DateTime(DateTime.Now.Year, 05, 01);

                txtDate.Text = ydate.ToString("dd/MM/yyyy");
                lblDob.Text = "As On";
                lblAge.Enabled = true;
                txtAge.Enabled = true;
            }
        }
        else
        {
            txtDate.Enabled = false;
            txtAge.Enabled = false;
        }
    }
    protected void cmbReason_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(cmbReason.SelectedValue) == 1)
        {
            txtMigration.Enabled = true;

        }
        else
        {
            txtMigration.Enabled = false;

        }
    }
    protected void ddloccu_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddloccu.SelectedValue.ToString() == "6")
        {
            txtFOther.Enabled = true;
            txtFOther.Text = "";
        }
        else
        {
            txtFOther.Enabled = false;
            txtFOther.Text = "";
        }
    }
    protected void ddlEducation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlEducation.SelectedValue) > 0)
        {
            if (Convert.ToInt32(ddlEducation.SelectedValue) == 1 || Convert.ToInt32(ddlEducation.SelectedValue) == 2)
            {

                cmbSchool.Enabled = true;
                ddlClass.Enabled = true;


            }

            else
            {

                cmbSchool.SelectedIndex = 0;
                ddlClass.SelectedIndex = 0;
                txtNewSchool.Text = "";
                cmbSchool.Enabled = false;
                ddlClass.Enabled = false;




            }

            if (Convert.ToInt32(ddlEducation.SelectedValue) == 1)
            {
                cmbReason.Enabled = false;
                cmbReason.SelectedIndex = 0;

                ddlEnrollCat.SelectedIndex = 0;
                ddlEnrollCat.Enabled = false;

            }
            else
            {


                ddlEnrollCat.Enabled = true;

                cmbReason.Enabled = true;
            }

        }

        else
        {
            cmbSchool.Enabled = false;
            ddlClass.Enabled = false;
            cmbReason.Enabled = false;
        }


    }

    private Boolean Validation()
    {
        try
        {


            if (txtMauhalla.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mauhalla/Dhani/Fali')</script>", false);



                this.txtMauhalla.Focus();
                return false;
            }
            else if (txtHouse.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter House/Family No')</script>", false);



                this.txtHouse.Focus();
                return false;
            }




            else if (ddlDob.SelectedIndex < 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select DOB Available')</script>", false);


                this.txtAge.Focus();
                return false;
            }
            else if (Convert.ToInt32(ddlDob.SelectedValue) == 2 && txtAge.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Age')</script>", false);


                this.txtAge.Focus();
                return false;
            }





            else if (ddlEducation.SelectedIndex >= 0)
            {
                if (Convert.ToInt32(ddlEducation.SelectedValue) == 1 || Convert.ToInt32(ddlEducation.SelectedValue) == 2)
                {

                    if (cmbSchool.Visible == true)
                    {
                        if (cmbSchool.SelectedIndex <= 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select School Name')</script>", false);


                            this.cmbSchool.Focus();
                            return false;
                        }
                        if (cmbSchool.SelectedValue.ToString() == "99")
                        {
                            if (txtNewSchool.Text == "")
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter School Name')</script>", false);



                                this.txtNewSchool.Focus();
                                return false;
                            }
                        }
                        if (ddlClass.SelectedIndex <= 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Class')</script>", false);


                            this.ddlClass.Focus();
                            return false;
                        }
                    }
                }
                if (Convert.ToInt32(ddlEducation.SelectedValue) == 2 || Convert.ToInt32(ddlEducation.SelectedValue) == 3)
                {
                    if (cmbReason.SelectedIndex <= 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Reason')</script>", false);


                        this.cmbReason.Focus();
                        return false;
                    }
                }
            }
            // if (DTPicker_DOB.Checked == false)
            //{
            //    MessageBox.Show("Select Date of Birth");
            //    this.DTPicker_DOB.Focus();
            //    return false;
            //}
            if (Convert.ToInt32(ddlEducation.SelectedValue) == 3 || Convert.ToInt32(ddlEducation.SelectedValue) == 2)
            {
                if (ddlEnrollCat.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Enrollment Category')</script>", false);



                    this.ddlEnrollCat.Focus();
                    return false;
                }
            }







            DateTime DOB;
            DateTime AsDob;
            Int32 Age = 0;


            if (Convert.ToInt32(ddlDob.SelectedValue) == 1)
            {

                string DateSarveyDate = txtSarveyDate.Text;
                string[] b = DateSarveyDate.Split('/');

                string DateB = txtDate.Text;
                string[] a = DateB.Split('/');
                string BithDate = a[2] + '-' + a[1] + '-' + a[0];



                Age = Convert.ToInt32(b[2]) - Convert.ToInt32(a[2]);
                DOB = Convert.ToDateTime(a[2] + '-' + a[1] + '-' + a[0]);

                Int32 iyear = Convert.ToInt32(a[2]) + Age;
                string dyear = iyear.ToString();
                //  AsDob = Convert.ToDateTime(dyear + '-' + a[1] + '-' + a[0]);

                if (Age < 0)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 0 and 18 years')</script>", false);


                    this.txtAge.Focus();
                    return false;

                }
                if (Age > 18)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 0 and 18 years')</script>", false);


                    this.txtAge.Focus();
                    return false;
                }
                if (Convert.ToInt32(ddlEducation.SelectedValue) == 3 || Convert.ToInt32(ddlEducation.SelectedValue) == 2)
                {
                    if (Convert.ToInt32(cmbReason.SelectedValue) == 2)
                    {
                        if (Age > 6)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Reason Selection')</script>", false);


                            cmbReason.Focus();
                            return false;

                        }
                    }
                }
            }
            else
            {



                if (Convert.ToInt32(txtAge.Text) < 0)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 0 and 18 years')</script>", false);


                    this.txtAge.Focus();
                    return false;

                }
                if (Convert.ToInt32(txtAge.Text) > 18)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 0 and 18 years')</script>", false);


                    this.txtAge.Focus();
                    return false;
                }
                if (Convert.ToInt32(ddlEducation.SelectedValue) == 3 || Convert.ToInt32(ddlEducation.SelectedValue) == 2)
                {
                    if (Convert.ToInt32(cmbReason.SelectedValue) == 2)
                    {
                        if (txtAge.Text != "")
                        {
                            if (Convert.ToInt32(txtAge.Text) > 6)
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Reason Selection')</script>", false);


                                cmbReason.Focus();
                                return false;

                            }
                        }
                    }
                }

            }


            return true;

        }
        catch
        {

            return false;
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        if (!Validation())
            return;
        SaveData();
        GVMainBind();


    }
    public void SaveData()
    {

        string StudentTSInsertQuery;
        Int32 DoAv = 0;
        Int32 Gender = 0;
        DateTime DOB;
        DateTime AsDob;
        Int32 Age = 0;
        Int32 mmonth = 0;

        if (Convert.ToInt32(ddlGender.SelectedValue) == 1)
        {
            Gender = 1;
        }
        else
        {
            Gender = 2;
        }

        string childName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtChildName.Text.Trim());
        string FatherName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtFatherName.Text.Trim());



        string MainSarveyDate = txtSarveyDate.Text;
        string[] d = MainSarveyDate.Split('/');


        string SarveyDate = d[2] + '-' + d[1] + '-' + d[0];

        if (Convert.ToInt32(ddlDob.SelectedValue) == 1)
        {

            string DateSarveyDate = txtSarveyDate.Text;
            string[] b = DateSarveyDate.Split('/');

            string DateB = txtDate.Text;
            string[] a = DateB.Split('/');
            string BithDate = a[2] + '-' + a[1] + '-' + a[0];



            Age = Convert.ToInt32(b[2]) - Convert.ToInt32(a[2]);
            DOB = Convert.ToDateTime(a[2] + '-' + a[1] + '-' + a[0]);

            Int32 iyear = Convert.ToInt32(a[2]) + Age;
            string dyear = iyear.ToString();
            AsDob = Convert.ToDateTime(DateTime.Today);
            DoAv = 1;
        }
        else
        {
            string DateB = txtDate.Text;
            string[] a = DateB.Split('/');
            string BithDate = a[2] + '-' + a[1] + '-' + a[0];

            Age = Convert.ToInt32(txtAge.Text);
            AsDob = Convert.ToDateTime(a[2] + '-' + a[1] + '-' + a[0]);

            Int32 iyear = Convert.ToInt32(a[2]) - Age;
            string dyear = iyear.ToString();

            string month = "04";
            string sDay = "01";
            Int32 Iyear = DateTime.Today.Year - Age;
            DOB = Convert.ToDateTime(Iyear.ToString() + '-' + month + '-' + sDay);



            DoAv = 0;
        }

        string val_House = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtHouse.Text.Trim());

        string val_AgeOther = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtOtherAge.Text.Trim());

        string val_SchoolOther = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtNewSchool.Text.Trim());
        if (Convert.ToBoolean(ViewState["vADD"].ToString()) == true)
        {

        }

        if (Convert.ToBoolean(ViewState["vVerify"].ToString()) == true)
        {

        }
        if (Convert.ToBoolean(ViewState["vADD"].ToString()) == true)
        {

        }
    }


    public void LoadUnique()
    {
        string strQry = " Select isnull(max(Serial),0) as Serial from [tblDTD]   left join  mst5Village  on mst5Village.VillageCode=[tblDTD].VillageCode  or  mst5Village.refVillage16=tblDTD.VillageCode	or  mst5Village.refVillage17=tblDTD.VillageCode	or  mst5Village.refVillage18=tblDTD.VillageCode	or  mst5Village.refVillage19=tblDTD.VillageCode	or  mst5Village.refVillage20=tblDTD.VillageCode  	or  mst5Village.refVillage21=tblDTD.VillageCode	  where mst5Village.villagecode='" + ddlVillage.SelectedValue + "'   ";
        //string strQry = " Select top 1 Serial from tblDTD   order by Serial desc ";
        DataTable dt = objMain.LoadData(strQry);
        Session["Serial"] = dt.Rows[0]["Serial"].ToString();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        pnlMain.Enabled = true;
        FillSchool();
        ClearData();

        txtSarveyDate.Focus();
        Unique();

    }
    public void ClearData()
    {
        txtFOther.Enabled = false;
        txtFOther.Text = "";
        ddlDob.SelectedIndex = 2;

        cmbSchool.Visible = true;

        txtNewSchool.Text = "";

        txtUnique.Text = "";
        txtChildName.Text = "";
        txtNewSchool.Text = "";
        txtOtherAge.Text = "";
        //txtFatherName.Text = "";
        txtAge.Text = "";
        cmbSchool.Enabled = false;
        ddlClass.Enabled = false;
        cmbReason.Enabled = false;
        ddlGender.SelectedIndex = 1;
        ViewState["Save"] = "Save";
        //DTPicker_DOB.Format = DateTimePickerFormat.Custom;
        //DTPicker_DOB.CustomFormat = "dd/MM/yyyy ";
        //  DTPicker_DOB.Value = DateTime.Today.Date;

        DateTime ydate = new DateTime(DateTime.Now.Year, 05, 01);
        txtDate.Text = ydate.ToString("dd/MM/yyyy");
        lblDob.Text = "As On";
        txtDate.Enabled = false;
        txtSarveyDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtAge.Text = "";
        cmbSchool.SelectedIndex = 0;
        cmbAgeproof.SelectedIndex = 0;
        ddloccu.SelectedIndex = 0;
        ddlCategory.SelectedIndex = 0;
        txtMigration.Text = "";
        ddlEducation.SelectedIndex = 0;
        ddlEnrollCat.SelectedIndex = 0;
        cmbReason.SelectedIndex = 0;
        ddlClass.SelectedIndex = 0;

        txtHouse.Text = "";
        //txtMauhalla.Text = "";

        lblNumNo.Text = "";
        txtUnique.Text = "";


    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
        if (ViewState["ChildId"].ToString() != null)
        {
            int res1 = objMain.DeleteD2dData(ViewState["ChildId"].ToString(), "D");



            if (res1 > 0)
            {
                GVMainBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

            }

        }
    }


    protected void btnSerach_Click(object sender, EventArgs e)
    {
        GVMain.Enabled = true;
        string strQry1 = " Select NameLocalLng as VillageCode  from mst2District where DistrictCode='" + ddlDistrict.SelectedValue + "' ";
        DataTable dt = objMain.LoadData(strQry1);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["VillageCode"].ToString() == "UNI")
            {
                Session["UNI"] = "UNI";
            }
            else
            {
                Session["UNI"] = "";
            }
        }
        else
        {
            Session["UNI"] = "";
        }

        GVMainBind();
        pnlMain.Enabled = false;
    }

    public void Unique()
    {
        if (ViewState["Save"].ToString() == "Save")
        {
            if (ddlVillage.SelectedIndex > 0)
            {
                txtUnique.Text = "";
                Int32 mNewNo = 0;
                string strAlias;
                string strQry = " Select isnull(max(Serial),0) as Serial from [tblDTD]   left join  mst5Village  on mst5Village.VillageCode=[tblDTD].VillageCode  or  mst5Village.refVillage16=tblDTD.VillageCode	or  mst5Village.refVillage17=tblDTD.VillageCode	or  mst5Village.refVillage18=tblDTD.VillageCode	or  mst5Village.refVillage19=tblDTD.VillageCode	or  mst5Village.refVillage20=tblDTD.VillageCode  	or  mst5Village.refVillage21=tblDTD.VillageCode	  where mst5Village.villagecode='" + ddlVillage.SelectedValue + "' ";
                //string strQry = " Select top 1 isnull(TBCode,0) as Serial from mstTeamBalika where VillageCode='" + ddlVillage.SelectedValue + "'  order by TBCode desc ";
                //string strQry = " Select top 1 Serial from tblDTD   order by Serial desc ";
                DataTable dt = objMain.LoadData(strQry);

                string strQry1 = " Select EGVillageCode as VillageCode  from mst5Village where VillageCode='" + ddlVillage.SelectedValue + "' ";
                DataTable dtVillage = objMain.LoadData(strQry1);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Serial"].ToString() == "" || dt.Rows[0]["Serial"].ToString() == "-1")
                    {
                        mNewNo += 1;
                        strAlias = mNewNo.ToString().PadLeft(3, '0');
                        txtUnique.Text = dtVillage.Rows[0]["VillageCode"].ToString() + "-" + strAlias;
                        ViewState["NumNo"] = strAlias;
                        Session["Serial"] = mNewNo;
                    }
                    else
                    {
                        mNewNo = Convert.ToInt32(dt.Rows[0]["Serial"].ToString());
                        mNewNo += 1;
                        strAlias = mNewNo.ToString().PadLeft(3, '0');

                        ViewState["NumNo"] = strAlias;
                        txtUnique.Text = dtVillage.Rows[0]["VillageCode"].ToString() + "-" + strAlias;
                        Session["Serial"] = mNewNo;

                    }

                }
                else
                {
                    mNewNo += 1;
                    Session["Serial"] = mNewNo;
                    strAlias = mNewNo.ToString().PadLeft(3, '0');
                    txtUnique.Text = dtVillage.Rows[0]["VillageCode"].ToString() + "-" + strAlias;
                    ViewState["NumNo"] = strAlias;
                }
            }
        }


    }


}