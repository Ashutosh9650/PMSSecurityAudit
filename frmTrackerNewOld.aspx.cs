using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class frmTrackerNewOld : System.Web.UI.Page
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

                FillFormType();
                LoadYear();
                LoadUserLeavel();
                pnlMain.Visible = true;
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
                    dr = dtYear.NewRow();
                    dr["Type"] = GivenYear - 2 + "-" + Convert.ToString((GivenYear - 2 + 1));
                    dr["ID"] = y - 2;
                    dtYear.Rows.Add(dr);
                }
                else
                {
                    dr = dtYear.NewRow();
                    dr["Type"] = Convert.ToString((y - 1)) + "-" + y.ToString();
                    //y = y - 1;
                    dr["ID"] = y - 1;

                    dtYear.Rows.Add(dr);

                    dr = dtYear.NewRow();
                    dr["Type"] = GivenYear - 2 + "-" + Convert.ToString((GivenYear - 2 + 1));
                    dr["ID"] = y - 2;
                    dtYear.Rows.Add(dr);
                }

            }

        }
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        ddlYear_SelectedIndexChanged(ddlYear, null);
        //}


    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlMain.Visible = false;
        string typevalue = ddlformatype.SelectedValue;
        switch (typevalue)
        {
            case "9":
            case "11":
            case "12":
            case "13":
                ddlschool.Visible = true;
                lblSchool.Visible = true;
                ddlschool.Items.Clear();
                ddlschool.Enabled = false;
                FillCBBock();
                ddlPanchayat.Items.Clear();
                ddlVillage.Items.Clear();
                //FillSchool();

                break;
            case "10":
                ddlschool.Visible = false;
                lblSchool.Visible = false;
                FillCBBock();
                ddlPanchayat.Items.Clear();
                ddlVillage.Items.Clear();
                break;

        }

    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (ddlYear.SelectedIndex > 0)
        {
            ddlState.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlDistrict, null);
            ddlDistrict.SelectedIndex = 1;
            ddlDistrict_SelectedIndexChanged(ddlBlock, null);
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;

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
            btnSumbit.Enabled = true;
            if (Session["FinYear"].ToString() != ddlYear.SelectedItem.Text)
            {
                string strQry;
                strQry = "Select * from mstModuleLocking  where [FromName]='Tracker' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "' ";


                DataTable dtModel = objMain.LoadData(strQry);
                if (dtModel.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtModel.Rows[0]["LockMonth"].ToString()) < DateTime.Today.Month)
                    {
                        btnAdd.Enabled = false;
                        btnsave.Enabled = false;
                        btnDelete.Enabled = false;
                        btnSumbit.Enabled = false;
                        ViewState["M"] = "M";

                    }

                }

            }

        }
    }
    protected void GV_Project_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //GVMain.PageIndex = e.NewPageIndex;
        if (ViewState["Serach"] != null)
        {
            DataTable dt = ViewState["Serach"] as DataTable;
            //GVMain.DataSource = dt;
            //GVMain.DataBind();
        }

    }
    public void UserLevelFilter()
    {

        string strQry = "";
        string Cond = "Module='Tracker'";
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
            btnSumbit.Enabled = true;
            lblMain.Text = "VERIFICATION";
            //"DOOR-TO-DOOR  SURVEY";
        }
        else
        {
            btnAdd.Enabled = false;

        }
        if (vVerify == true)
        {

            btnsave.Enabled = true;

            btnSumbit.Enabled = false;
        }
        if (vVerify == true || vADD == true)
        {
            btnsave.Enabled = true;
            btnSumbit.Enabled = true;
        }
        else
        {
            btnsave.Enabled = false;
            btnSumbit.Enabled = false;
        }

    }


    public void LoadUserLeavel()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

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

    public void FillCBState()
    {
        conditions = "";
        objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");
    }

    public void FillFormType()
    {
        conditions = "Flag = 2 ";
        objComman.BindDLL("mstForm", "FormID,FormName ", conditions, "FormID", "asc", ddlformatype, "FormName", "FormID", "--Select--");

    }


    public void FillCBDist()
    {

        conditions = "";


        conditions = "StateCode ='" + ddlState.SelectedValue + "' and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";

        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");



    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        // pnlMain.Enabled = false;

        FillCBDist();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        Locking();
        //pnlMain.Enabled = false;

        FillCBBock();
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        //pnlMain.Enabled = false;

        FillCBCluster();
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        //pnlMain.Enabled = false;

        FillCVillage();
    }
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        //pnlMain.Enabled = false;

        Unique();

        string typevalue = ddlformatype.SelectedValue;

        switch (typevalue)
        {
            case "9":
            case "11":
            case "12":
            case "13":
                ddlschool.Enabled = true;
                FillSchool();

                break;
            case "10":
                ddlschool.Items.Clear();
                ddlschool.Visible = false;
                lblSchool.Visible = false;
                break;

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
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "'";
        objComman.BindDLLSelectAll("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "Select");



    }
    public void FillCVillage()
    {
        conditions = "";

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
        //ViewState["Save"] = null;
        string villagestr = "";
        string str = "";
        if (ddlformatype.SelectedIndex > 0)
        {
            str = " where FormID='" + ddlformatype.SelectedValue.ToString() + "'";
        }
        if (ddlYear.SelectedIndex > 0)
        {
            str += "and mst5Village.Fyear='" + ddlYear.SelectedItem.Text.ToString() + "'";
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
            //villagestr = "Where VillageCode='" + ddlVillage.SelectedValue.ToString() + "'";
        }
        else
        {
            str = str + "and mst5Village.VillageCode='" + 0 + "'";


        }
        //cleargrids();
        DataTable dtmstM = new DataTable();
        DataTable dtschool = new DataTable();
        DataTable dtddl = new DataTable();
        string typevalue = ddlformatype.SelectedValue;

        //DataTable dt = objMain.Tracker(villagestr, typevalue);
        //Grdform6.DataSource = dt;
        //Grdform6.DataBind();

        switch (typevalue)
        {
            case "9":
            case "11":
            case "12":
            case "13":

                if (ddlschool.SelectedValue != null && ddlschool.SelectedIndex > 0)
                {
                    str += " and SchoolCode='" + ddlschool.SelectedValue.ToString() + "'";
                    //dtmstM = objMain.Tracker(str, typevalue);
                    dtmstM = objMain.LoadData("select UniqueCode, mst5Village.VillageCode, SchoolCode,ReceivedDate,CollectionDate,NoOfGirls,NoOfBoys,TBcode,EndlineReceivingDate,EndlineCollectionDate,EndlineNoofGirls,EndlineNoofBoysPresent,FormCode from tblTracker  inner join mst5Village on mst5Village.VillageCode = tblTracker.VillageCode inner join mst1State on mst1State.StateCode=mst5Village.StateCode inner join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode inner join mst3Block on mst3Block.BlockCode=mst5Village.BlockCode inner join mstPanchayat on mstPanchayat.PanchayatCode=mst5Village.PanchayatCode" + str + "and" + " tblTracker.FormID = " + typevalue);

                    dtschool = objMain.LoadData("select DISECode from mstSchool where SchoolCode = '" + ddlschool.SelectedValue + "'");
                    hdnschoolcode.Value = dtschool.Rows[0]["DISECode"].ToString();
                    if (typevalue == "13")
                    {
                        dtddl = objMain.LoadData("select TBCode,TBName from mstTeamBalika ");
                        ViewState["DDown"] = dtddl;
                    }


                }
                break;
            case "10":

                //dtmstM = objMain.Tracker(str, typevalue);
                dtmstM = objMain.LoadData("select UniqueCode, mst5Village.VillageCode, SchoolCode,ReceivedDate,CollectionDate,NoOfGirls,NoOfBoys,TBcode,EndlineReceivingDate,EndlineCollectionDate,EndlineNoofGirls,EndlineNoofBoysPresent,FormCode from tblTracker  inner join mst5Village on mst5Village.VillageCode = tblTracker.VillageCode inner join mst1State on mst1State.StateCode=mst5Village.StateCode inner join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode inner join mst3Block on mst3Block.BlockCode=mst5Village.BlockCode inner join mstPanchayat on mstPanchayat.PanchayatCode=mst5Village.PanchayatCode" + str + "and" + " tblTracker.FormID = " + typevalue);
                DataTable dtvillage = objMain.LoadData("select EGVillageCode from mst5Village where VillageCode = '" + ddlVillage.SelectedValue + "'");
                hdnvillagecode.Value = dtvillage.Rows[0]["EGVillageCode"].ToString();
                break;

        }

        if (dtmstM.Rows.Count > 0)
        {

            pnlMain.Visible = true;

            string header = ddlformatype.SelectedItem.Text;
            SetGVHeader(header);
            ViewState["Save"] = "Update";
            ViewState["Serach"] = dtmstM;

            if (typevalue == "9")
            {
                Grdform6.Visible = true;

                grdform7.Visible = false;
                grdform8.Visible = false;
                grdform9.Visible = false;
                grdform12.Visible = false;

                Grdform6.DataSource = dtmstM;
                Grdform6.DataBind();
                Grdform6.Dispose();
                Grdform6.Columns[5].Visible = true;
                pnlMain.Visible = true;
                pnlMain.Enabled = true;


            }
            if (typevalue == "10")
            {
                grdform7.Visible = true;

                Grdform6.Visible = false;

                grdform8.Visible = false;
                grdform9.Visible = false;
                grdform12.Visible = false;

                grdform7.DataSource = dtmstM;
                grdform7.DataBind();
                grdform7.Dispose();
                grdform7.Columns[3].Visible = true;
                pnlMain.Visible = true;
                pnlMain.Enabled = true;


                //  || typevalue == "11"  || typevalue == "12"
            }
            if (typevalue == "11")
            {
                grdform8.Visible = true;

                Grdform6.Visible = false;
                grdform7.Visible = false;

                grdform9.Visible = false;
                grdform12.Visible = false;

                grdform8.DataSource = dtmstM;
                grdform8.DataBind();
                grdform8.Dispose();
                grdform8.Columns[3].Visible = true;
                pnlMain.Visible = true;
                pnlMain.Enabled = true;
            }
            if (typevalue == "12")
            {

                grdform9.Visible = true;

                Grdform6.Visible = false;
                grdform7.Visible = false;
                grdform8.Visible = false;

                grdform12.Visible = false;

                grdform9.DataSource = dtmstM;
                grdform9.DataBind();
                grdform9.Dispose();
                grdform9.Columns[3].Visible = true;
                pnlMain.Visible = true;
                pnlMain.Enabled = true;
            }
            if (typevalue == "13")
            {
                Grdform6.Visible = false;
                grdform7.Visible = false;
                grdform8.Visible = false;
                grdform9.Visible = false;
                grdform12.Visible = true;


                grdform12.DataSource = dtmstM;
                grdform12.DataBind();
                grdform12.Dispose();
                //grdform9.Columns[3].Visible = true;
                pnlMain.Visible = true;
                pnlMain.Enabled = true;
            }



        }
        else
        {
            if (ddlVillage.SelectedValue != null && ddlVillage.SelectedIndex > 0)
            {

                DataRow row = dtmstM.NewRow();
                dtmstM.Rows.Add(row);
                ViewState["Save"] = "Save";
                ViewState["Serach"] = dtmstM;

                if (typevalue == "9")
                {


                    string header = ddlformatype.SelectedItem.Text;
                    Grdform6.Visible = true;

                    grdform7.Visible = false;
                    grdform8.Visible = false;
                    grdform9.Visible = false;
                    grdform12.Visible = false;
                    Grdform6.DataSource = dtmstM;
                    Grdform6.DataBind();
                    Grdform6.Dispose();


                    pnlMain.Visible = true;
                    pnlMain.Enabled = true;
                }


                if (typevalue == "10")
                {
                    Grdform6.Visible = false;

                    grdform7.Visible = true;
                    grdform8.Visible = false;
                    grdform9.Visible = false;
                    grdform12.Visible = false;
                    grdform7.DataSource = dtmstM;
                    grdform7.DataBind();
                    grdform7.Dispose();

                    pnlMain.Visible = true;
                    pnlMain.Enabled = true;
                }

                if (typevalue == "11")
                {
                    Grdform6.Visible = false;

                    grdform7.Visible = false;
                    grdform8.Visible = true;
                    grdform9.Visible = false;
                    grdform12.Visible = false;
                    grdform8.DataSource = dtmstM;
                    grdform8.DataBind();
                    grdform8.Dispose();

                    pnlMain.Visible = true;
                    pnlMain.Enabled = true;
                }
                if (typevalue == "12")
                {
                    Grdform6.Visible = false;

                    grdform7.Visible = false;
                    grdform8.Visible = false;
                    grdform9.Visible = true;
                    grdform12.Visible = false;
                    grdform9.DataSource = dtmstM;
                    grdform9.DataBind();
                    grdform9.Dispose();

                    pnlMain.Visible = true;
                    pnlMain.Enabled = true;
                }
                if (typevalue == "13")
                {
                    Grdform6.Visible = false;

                    grdform7.Visible = false;
                    grdform8.Visible = false;
                    grdform9.Visible = false;

                    grdform12.Visible = true;
                    grdform12.DataSource = dtmstM;
                    grdform12.DataBind();
                    grdform12.Dispose();

                    pnlMain.Visible = true;
                    pnlMain.Enabled = true;
                }


            }
            //ViewState["Serach"] = "";
        }

    }

    protected void btnAddForm6row_Click(object sender, EventArgs e)
    {
        EAddNewRowToGrid();
    }



    private void EAddNewRowToGrid()
    {
        int rowIndex = 0;

        if (ViewState["Serach"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["Serach"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    TextBox txtreciveddate = (TextBox)Grdform6.Rows[rowIndex].FindControl("txtrecivedate");
                    TextBox txtcollectiondate = (TextBox)Grdform6.Rows[rowIndex].FindControl("txtcollectiondate");
                    TextBox txtgirls = (TextBox)Grdform6.Rows[rowIndex].FindControl("txtnoofgirols");
                    TextBox txtboys = (TextBox)Grdform6.Rows[rowIndex].FindControl("txtNoOfBoys");

                    drCurrentRow = dtCurrentTable.NewRow();


                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);


                ViewState["ECurrentTable"] = dtCurrentTable;

                Grdform6.DataSource = dtCurrentTable;
                Grdform6.DataBind();

            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "funtions", "AddCollapsClass()", true);
        // collapseTwo.Attributes.Add("class", "panel-collapse collapse in");
        //Set Previous Data on Postbacks
        // ESetPreviousData();
    }


    protected void btnAddForm7row_Click(object sender, EventArgs e)
    {
        EAddNewRowToGrid7();
    }


    private void EAddNewRowToGrid7()
    {
        int rowIndex = 0;

        if (ViewState["Serach"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["Serach"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    TextBox txtreciveddate = (TextBox)grdform7.Rows[rowIndex].FindControl("txtrecivedate");
                    TextBox txtcollectiondate = (TextBox)grdform7.Rows[rowIndex].FindControl("txtcollectiondate");


                    drCurrentRow = dtCurrentTable.NewRow();


                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);


                ViewState["ECurrentTable"] = dtCurrentTable;

                grdform7.DataSource = dtCurrentTable;
                grdform7.DataBind();

            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "funtions", "AddCollapsClass()", true);
        // collapseTwo.Attributes.Add("class", "panel-collapse collapse in");
        //Set Previous Data on Postbacks
        // ESetPreviousData();
    }



    protected void btnAddForm8row_Click(object sender, EventArgs e)
    {
        EAddNewRowToGrid8();
    }


    private void EAddNewRowToGrid8()
    {
        int rowIndex = 0;

        if (ViewState["Serach"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["Serach"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    TextBox txtreciveddate = (TextBox)grdform8.Rows[rowIndex].FindControl("txtrecivedate");
                    TextBox txtcollectiondate = (TextBox)grdform8.Rows[rowIndex].FindControl("txtcollectiondate");


                    drCurrentRow = dtCurrentTable.NewRow();


                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);


                ViewState["ECurrentTable"] = dtCurrentTable;

                grdform8.DataSource = dtCurrentTable;
                grdform8.DataBind();

            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "funtions", "AddCollapsClass()", true);
        // collapseTwo.Attributes.Add("class", "panel-collapse collapse in");
        //Set Previous Data on Postbacks
        // ESetPreviousData();
    }


    protected void btnAddForm9row_Click(object sender, EventArgs e)
    {
        EAddNewRowToGrid9();
    }


    private void EAddNewRowToGrid9()
    {
        int rowIndex = 0;

        if (ViewState["Serach"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["Serach"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    TextBox txtreciveddate = (TextBox)grdform9.Rows[rowIndex].FindControl("txtrecivedate");
                    TextBox txtcollectiondate = (TextBox)grdform9.Rows[rowIndex].FindControl("txtcollectiondate");


                    drCurrentRow = dtCurrentTable.NewRow();


                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);


                ViewState["ECurrentTable"] = dtCurrentTable;

                grdform9.DataSource = dtCurrentTable;
                grdform9.DataBind();

            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "funtions", "AddCollapsClass()", true);
        // collapseTwo.Attributes.Add("class", "panel-collapse collapse in");
        //Set Previous Data on Postbacks
        // ESetPreviousData();
    }



    protected void btnAddForm12row_Click(object sender, EventArgs e)
    {
        EAddNewRowToGrid12();
    }


    private void EAddNewRowToGrid12()
    {
        int rowIndex = 0;

        if (ViewState["Serach"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["Serach"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList ddltb1 = (DropDownList)grdform12.Rows[rowIndex].FindControl("ddltb");

                    TextBox txtreciveddate = (TextBox)grdform12.Rows[rowIndex].FindControl("txtrecivedate");
                    TextBox txtcollectiondate = (TextBox)grdform12.Rows[rowIndex].FindControl("txtcollectiondate");

                    TextBox txtngirls = (TextBox)grdform12.Rows[rowIndex].FindControl("txtnoofgirols");
                    TextBox txtnboys = (TextBox)grdform12.Rows[rowIndex].FindControl("txtNoOfBoys");

                    TextBox txtereciveddate = (TextBox)grdform12.Rows[rowIndex].FindControl("txtendrecivedate");
                    TextBox txtecollectiondate = (TextBox)grdform12.Rows[rowIndex].FindControl("txtendcollectiondate");

                    TextBox txtengirls = (TextBox)grdform12.Rows[rowIndex].FindControl("txtenoofgirols");
                    TextBox txtenboys = (TextBox)grdform12.Rows[rowIndex].FindControl("txteNoOfBoys");






                    drCurrentRow = dtCurrentTable.NewRow();


                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);


                ViewState["ECurrentTable"] = dtCurrentTable;

                grdform9.DataSource = dtCurrentTable;
                grdform9.DataBind();

            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "funtions", "AddCollapsClass()", true);

    }


    protected void GvForm6_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (ViewState["Save"].ToString() == "Update")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label txtformvalue = (Label)e.Row.FindControl("txtcode");
                txtformvalue.Visible = true;
            }
        }
    }

    protected void GvForm7_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (ViewState["Save"].ToString() == "Update")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label txtformvalue = (Label)e.Row.FindControl("txtcode");
                txtformvalue.Visible = true;
            }
        }
    }

    protected void GvForm8_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (ViewState["Save"].ToString() == "Update")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label txtformvalue = (Label)e.Row.FindControl("txtcode");
                txtformvalue.Visible = true;
            }
        }
    }

    protected void GvForm9_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (ViewState["Save"].ToString() == "Update")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label txtformvalue = (Label)e.Row.FindControl("txtcode");
                txtformvalue.Visible = true;
            }
        }
    }


    protected void GvForm12_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (ViewState["Save"].ToString() == "Update")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtdd = (DataTable)ViewState["Serach"];
                DataTable dtdropdown = (DataTable)ViewState["DDown"];
                Label txtformvalue = (Label)e.Row.FindControl("txtcode");
                txtformvalue.Visible = true;

                DropDownList ddltb1 = (DropDownList)e.Row.FindControl("ddltb");
                ddltb1.DataSource = dtdropdown;

                ddltb1.DataTextField = "TBName";
                ddltb1.DataValueField = "TBCode";
                ddltb1.SelectedValue = dtdd.Rows[e.Row.RowIndex]["TBcode"].ToString();
                ddltb1.DataBind();
            }
        }
        if (ViewState["Save"].ToString() == "Save")
        {
            DataTable dtdropdown = (DataTable)ViewState["DDown"];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddltb1 = (DropDownList)e.Row.FindControl("ddltb");
                ddltb1.DataSource = dtdropdown;

                ddltb1.DataTextField = "TBName";
                ddltb1.DataValueField = "TBCode";
                ddltb1.DataBind();
            }
        }
    }


    //protected void GvForm7_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (ViewState["Save"].ToString() == "Update")
    //    {
    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            Label txtformvalue = (Label)e.Row.FindControl("txtcode");
    //            txtformvalue.Visible = true;
    //        }
    //    }
    //}



    protected void SetGVHeader(string GVheader)
    {
        //GridViewRowEventArgs e;
        //if (GridViewRowEventArgs row in Gv_Profile_Search.HeaderRow)
        //{
        //    if(Gv_Profile_Search.
        //foreach (GridViewRow row in Gv_Profile_Search.RowHeaderColumn)
        //{
        //}
        //if (GridViewRowEventArgs.Row.RowType == DataControlRowType.Header)
        //    {
        //Gv_Profile_Search.Columns[0].HeaderText = GVheader;
        //}
        //}
    }

    protected void txtSearchName_Click(object sender, EventArgs e)
    {
        DataTable dt = ViewState["Serach"] as DataTable;
        string strFilter = "";

        string str = "ChildName";
        string str1 = "HHNo";

        DataTable dtfilter = dt.Copy();



        dtfilter.DefaultView.RowFilter = strFilter;
        dtfilter.DefaultView.Sort = "ChildName asc";


    }



    protected DataTable dtLbind(string Dllvl)
    {
        DataTable dtDD = objMain.LoadData("Select LookupCode, Description, Description1 From mstLookup  where LookupFlag = '" + Dllvl + "'");
        return dtDD;
    }

    public void FillSchool()
    {
        conditions = "";
        conditions = "VillageCode ='" + ddlVillage.SelectedValue + "' and FYear ='" + ddlYear.SelectedItem.Text + "' ";
        objComman.BindDLLSchool("mstSchool", "SchoolCode,Name", conditions, "Name", "asc", ddlschool, "Name", "SchoolCode", "Select");



    }
    public void FillD2dData(string VCode, int FID)
    {
        DataTable dt = new DataTable();
        try
        {

            dt = objMain.LoadData(" select UniqueCode, VillageCode, Value, ComboID, tbl_CVverfication.FormID ,tbl_CVverfication.CV_UID, mstCVverfication.CV_FieldName,mstCVverfication.CV_MaxLimit,mstCVverfication.CV_MaxLimit,mstCVverfication.CV_Validation, mstCVverfication.CV_FieldType  from  tbl_CVverfication inner join mstCVverfication on mstCVverfication.CV_UID = tbl_CVverfication.CV_UID where VillageCode ='" + VCode + "'and tbl_CVverfication.FormID = " + FID);

        }
        catch
        {
        }
        if (dt.Rows.Count > 0)
        {
            ViewState["Dtup"] = dt;
            //Gv_Profile_Search.DataSource = dt;
            //Gv_Profile_Search.DataBind();


            FillSchool();

        }

    }


    protected void btnsave_Click(object sender, EventArgs e)
    {


        GVMainBind();
        pnlMain.Visible = true;
        //Gv_Profile_Search.Visible = false;



    }

    //public void cleargrids()
    //{
    //    //Grdform6.Dispose();
    //    //grdform7.Dispose();
    //    //grdform8.Dispose();
    //    //grdform9.Dispose();
    //    Grdform6.Visible = false;
    //    grdform7.Visible = false;
    //    grdform8.Visible = false;
    //    grdform9.Visible = false;
    //    grdform12.Visible = false;
    //}

    public void Unique(Int32 Type, string Code)
    {

        Int32 mNewNo = 0;
        string strAlias;
        string strQry = "";
        if (Type == 1)
        {
            strQry = " Select top 1 isnull(max(SerialNo),0) as Serial from tblTracker where VillageCode='" + ddlVillage.SelectedValue + "'   ";
        }
        if (Type == 2)
        {
            strQry = " Select top 1 isnull(max(SerialNo),0) as Serial from tblTracker where Schoolcode='" + ddlschool.SelectedValue + "'   ";
        }
        DataTable dt = objMain.LoadData(strQry);


        if (Code.Length > 0)
        {

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Serial"].ToString() == "" || dt.Rows[0]["Serial"].ToString() == "-1")
                {
                    mNewNo += 1;
                    strAlias = mNewNo.ToString().PadLeft(5, '0');
                    ViewState["TranckerCode"] = Code + "-" + strAlias;
                    ViewState["NumNo"] = strAlias;

                    lblNum.Text = mNewNo.ToString();
                }
                else
                {
                    mNewNo = Convert.ToInt32(dt.Rows[0]["Serial"].ToString());
                    mNewNo += 1;
                    strAlias = mNewNo.ToString().PadLeft(5, '0');

                    ViewState["NumNo"] = strAlias;
                    ViewState["TranckerCode"] = Code + "-" + strAlias;
                    lblNum.Text = mNewNo.ToString();
                }
            }


        }



    }

    public void SaveData(Int32 S1, string ApproveIo, string ApproveAu)
    {
        int result = 0;
        try
        {


            //string UCODE = objMain.Generate_RandomString(8);
            string vCODE = ddlVillage.SelectedValue.ToString();
            string SCode;
            string flag = "";
            //ViewState["Save"].ToString();
            string trackCode = "";
            int trackid = 0;
            string Rdate, Cdate, ERdate = "", ECdate = "", Tbcode = "";
            int NGirls = 0, NBoys = 0, ENgirls = 0, ENboys = 0;
            if (ddlschool.SelectedIndex > 0)
            {
                SCode = ddlschool.SelectedValue.ToString();
                //trackCode = 
            }
            else
            {
                SCode = "";
            }


            int formID = Convert.ToInt32(ddlformatype.SelectedValue);
            string schoolscode = hdnschoolcode.Value;
            string villagescode = hdnvillagecode.Value;
            string value = "";
            Int32 status;
            Int32 mNewNo = 0;
            if (formID == 9)
            {
                foreach (GridViewRow row in Grdform6.Rows)
                {

                    Label uidcode = (Label)row.FindControl("lblUniqueCode");

                    string UCode = uidcode.Text;
                    if (UCode == "")
                    {
                        if (lblNum.Text == "")
                        {
                            Unique(2, schoolscode);
                            status = Convert.ToInt32(lblNum.Text);
                            trackCode = ViewState["TranckerCode"].ToString();



                        }
                        else
                        {
                            mNewNo = Convert.ToInt32(lblNum.Text);
                            mNewNo += 1;
                            string strAlias = mNewNo.ToString().PadLeft(5, '0');

                            ViewState["NumNo"] = strAlias;
                            ViewState["TranckerCode"] = schoolscode + "-" + strAlias;
                            lblNum.Text = mNewNo.ToString();
                        }

                        UCode = Generate_RandomStringTemp(8);
                        flag = "I";
                    }
                    else
                    {
                        flag = "U";
                    }
                    // trackCode = schoolscode + "-" + "06" + "0" + row.RowIndex;

                    TextBox txtrdate = (TextBox)row.FindControl("txtrecivedate");
                    TextBox txtcdate = (TextBox)row.FindControl("txtcollectiondate");
                    TextBox txtgirl = (TextBox)row.FindControl("txtnoofgirols");
                    TextBox txtboy = (TextBox)row.FindControl("txtNoOfBoys");


                    Rdate = txtrdate.Text;

                    Cdate = txtcdate.Text;

                    NGirls = Convert.ToInt32(txtgirl.Text);

                    NBoys = Convert.ToInt32(txtboy.Text);

                    result = SaveDataGrid(UCode, vCODE, SCode, Rdate, Cdate, NGirls, NBoys, ERdate, ECdate, ENgirls, ENboys, flag, formID, trackCode, Tbcode, Convert.ToInt32(lblNum.Text), Session["username"].ToString(), S1, ApproveIo, ApproveAu);


                }


            }
            if (formID == 10)
            {
                foreach (GridViewRow row in grdform7.Rows)
                {

                    Label uidcode = (Label)row.FindControl("lblUniqueCode");

                    string UCode = uidcode.Text;
                    if (UCode == "")
                    {
                        if (lblNum.Text == "")
                        {
                            Unique(2, schoolscode);
                            status = Convert.ToInt32(lblNum.Text);
                            trackCode = ViewState["TranckerCode"].ToString();



                        }
                        else
                        {
                            mNewNo = Convert.ToInt32(lblNum.Text);
                            mNewNo += 1;
                            string strAlias = mNewNo.ToString().PadLeft(5, '0');

                            ViewState["NumNo"] = strAlias;
                            ViewState["TranckerCode"] = schoolscode + "-" + strAlias;
                            lblNum.Text = mNewNo.ToString();
                        }
                        UCode = Generate_RandomStringTemp(8);
                        flag = "I";
                    }
                    else
                    {
                        flag = "U";
                    }
                    // trackCode = villagescode + "-" + "07" + "0" + row.RowIndex;




                    TextBox txtrdate = (TextBox)row.FindControl("txtrecivedate");
                    TextBox txtcdate = (TextBox)row.FindControl("txtcollectiondate");



                    Rdate = txtrdate.Text;

                    // SaveDataGrid(UCode, vCODE, SCode, trackid, value, trackCode, formID, flag);


                    Cdate = txtcdate.Text;

                    result = SaveDataGrid(UCode, vCODE, SCode, Rdate, Cdate, NGirls, NBoys, ERdate, ECdate, ENgirls, ENboys, flag, formID, trackCode, Tbcode, Convert.ToInt32(lblNum.Text), Session["username"].ToString(), S1, ApproveIo, ApproveAu);




                }

            }


            if (formID == 11)
            {
                foreach (GridViewRow row in grdform8.Rows)
                {

                    Label uidcode = (Label)row.FindControl("lblUniqueCode");

                    string UCode = uidcode.Text;
                    if (UCode == "")
                    {
                        if (lblNum.Text == "")
                        {
                            Unique(2, schoolscode);
                            status = Convert.ToInt32(lblNum.Text);
                            trackCode = ViewState["TranckerCode"].ToString();



                        }
                        else
                        {
                            mNewNo = Convert.ToInt32(lblNum.Text);
                            mNewNo += 1;
                            string strAlias = mNewNo.ToString().PadLeft(5, '0');

                            ViewState["NumNo"] = strAlias;
                            ViewState["TranckerCode"] = schoolscode + "-" + strAlias;
                            lblNum.Text = mNewNo.ToString();
                        }
                        UCode = Generate_RandomStringTemp(8);
                        flag = "I";
                    }
                    else
                    {
                        flag = "U";
                    }
                    trackCode = villagescode + "-" + "08" + "0" + row.RowIndex;




                    TextBox txtrdate = (TextBox)row.FindControl("txtrecivedate");
                    TextBox txtcdate = (TextBox)row.FindControl("txtcollectiondate");



                    Rdate = txtrdate.Text;


                    Cdate = txtcdate.Text;

                    result = SaveDataGrid(UCode, vCODE, SCode, Rdate, Cdate, NGirls, NBoys, ERdate, ECdate, ENgirls, ENboys, flag, formID, trackCode, Tbcode, Convert.ToInt32(lblNum.Text), Session["username"].ToString(), S1, ApproveIo, ApproveAu);




                }

            }


            if (formID == 12)
            {
                foreach (GridViewRow row in grdform9.Rows)
                {

                    Label uidcode = (Label)row.FindControl("lblUniqueCode");

                    string UCode = uidcode.Text;
                    if (UCode == "")
                    {
                        if (lblNum.Text == "")
                        {
                            Unique(2, schoolscode);
                            status = Convert.ToInt32(lblNum.Text);
                            trackCode = ViewState["TranckerCode"].ToString();



                        }
                        else
                        {
                            mNewNo = Convert.ToInt32(lblNum.Text);
                            mNewNo += 1;
                            string strAlias = mNewNo.ToString().PadLeft(5, '0');

                            ViewState["NumNo"] = strAlias;
                            ViewState["TranckerCode"] = schoolscode + "-" + strAlias;
                            lblNum.Text = mNewNo.ToString();
                        }
                        UCode = Generate_RandomStringTemp(8);
                        flag = "I";
                    }
                    else
                    {
                        flag = "U";
                    }
                    trackCode = villagescode + "-" + "09" + "0" + row.RowIndex;


                    TextBox txtrdate = (TextBox)row.FindControl("txtrecivedate");
                    TextBox txtcdate = (TextBox)row.FindControl("txtcollectiondate");



                    Rdate = txtrdate.Text;

                    Cdate = txtcdate.Text;

                    result = SaveDataGrid(UCode, vCODE, SCode, Rdate, Cdate, NGirls, NBoys, ERdate, ECdate, ENgirls, ENboys, flag, formID, trackCode, Tbcode, Convert.ToInt32(lblNum.Text), Session["username"].ToString(), S1, ApproveIo, ApproveAu);





                }

            }


            if (formID == 13)
            {
                foreach (GridViewRow row in grdform12.Rows)
                {

                    Label uidcode = (Label)row.FindControl("lblUniqueCode");

                    string UCode = uidcode.Text;
                    if (UCode == "")
                    {
                        if (lblNum.Text == "")
                        {
                            Unique(2, schoolscode);
                            status = Convert.ToInt32(lblNum.Text);
                            trackCode = ViewState["TranckerCode"].ToString();



                        }
                        else
                        {
                            mNewNo = Convert.ToInt32(lblNum.Text);
                            mNewNo += 1;
                            string strAlias = mNewNo.ToString().PadLeft(5, '0');

                            ViewState["NumNo"] = strAlias;
                            ViewState["TranckerCode"] = schoolscode + "-" + strAlias;
                            lblNum.Text = mNewNo.ToString();
                        }
                        UCode = Generate_RandomStringTemp(8);
                        flag = "I";
                    }
                    else
                    {
                        flag = "U";
                    }
                    trackCode = villagescode + "-" + "12" + "0" + row.RowIndex;


                    //TextBox txtrdate = (TextBox)row.FindControl("txtrecivedate");
                    //TextBox txtcdate = (TextBox)row.FindControl("txtcollectiondate");

                    DropDownList ddltb = (DropDownList)row.FindControl("ddltb");

                    TextBox txtreciveddate = (TextBox)row.FindControl("txtrecivedate");
                    TextBox txtcollectiondate = (TextBox)row.FindControl("txtcollectiondate");

                    TextBox txtngirls = (TextBox)row.FindControl("txtnoofgirls");
                    TextBox txtnboys = (TextBox)row.FindControl("txtNoOfBoys");

                    TextBox txtereciveddate = (TextBox)row.FindControl("txtendrecivedate");
                    TextBox txtecollectiondate = (TextBox)row.FindControl("txtendcollectiondate");

                    TextBox txtengirls = (TextBox)row.FindControl("txtenoofgirls");
                    TextBox txtenboys = (TextBox)row.FindControl("txteNoOfBoys");

                    Tbcode = ddltb.SelectedValue;

                    Rdate = txtreciveddate.Text;

                    Cdate = txtcollectiondate.Text;

                    NGirls = Convert.ToInt32(txtngirls.Text);

                    NBoys = Convert.ToInt32(txtnboys.Text);

                    ERdate = txtereciveddate.Text;
                    ECdate = txtecollectiondate.Text;

                    ENgirls = Convert.ToInt32(txtengirls.Text);
                    ENboys = Convert.ToInt32(txtenboys.Text);

                    result = SaveDataGrid(UCode, vCODE, SCode, Rdate, Cdate, NGirls, NBoys, ERdate, ECdate, ENgirls, ENboys, flag, formID, trackCode, Tbcode, Convert.ToInt32(lblNum.Text), Session["username"].ToString(), S1, ApproveIo, ApproveAu);





                }

            }

        }
        catch
        {
        }
        if (result > 0)
        {

            ViewState["NumNo"] = "";
            ViewState["TranckerCode"] = "";
            lblNum.Text = "0";

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully')</script>", false);
            return;


        }






    }


    public int SaveDataGrid(string GUID, string VCODE, string SCODE, string Rdate, string Cdate, int Ngirls, int nboys, string ERdate, string ECdate, int ENgirls, int Enboys, string Flag, int formid, string trackCode, string Tbcode, Int32 serial, string createby, Int32 Status, string ApptoveByIO, string ApptoveByA)
    {
        int Result = 0;

        return 1;
    }

    public string Generate_RandomStringTemp(int NoChar)
    {
        Thread.Sleep(200);
        string element = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();
        string text = new string((from s in Enumerable.Repeat<string>(element, NoChar)
                                  select s[random.Next(s.Length)]).ToArray<char>()) + DateTime.Now.ToString("yyyyMMddhhmmssfff");
        return text.ToString();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        pnlMain.Enabled = true;
        FillSchool();
        ClearData();

        // txtSarveyDate.Focus();
        Unique();

    }
    public void ClearData()
    {


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
        //GVMain.Enabled = true;
        string typevalue = ddlformatype.SelectedValue;

        switch (typevalue)
        {
            case "9":
            case "11":
            case "12":
            case "13":
                if (ddlschool.SelectedIndex > 0)
                {
                    GVMainBind();
                    pnlMain.Enabled = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select School')</script>", false);
                }
                break;
            case "10":
                GVMainBind();
                pnlMain.Enabled = true;
                break;

        }

        //GVMainBind();
        //pnlMain.Enabled = true;

    }

    public void Unique()
    {
        if (ViewState["Save"].ToString() == "Save")
        {
            if (ddlVillage.SelectedIndex > 0)
            {

                Int32 mNewNo = 0;
                string strAlias;
                string strQry = " Select top 1 Serial from tblDTD where VillageCode='" + ddlVillage.SelectedValue + "' and EnrollStatus=1  order by Serial desc ";

                DataTable dt = objMain.LoadData(strQry);

                string strQry1 = " Select EGVillageCode as VillageCode  from mst5Village where VillageCode='" + ddlVillage.SelectedValue + "' ";
                DataTable dtVillage = objMain.LoadData(strQry1);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Serial"].ToString() == "" || dt.Rows[0]["Serial"].ToString() == "-1")
                    {
                        mNewNo += 1;
                        strAlias = mNewNo.ToString().PadLeft(3, '0');

                        ViewState["NumNo"] = strAlias;
                    }
                    else
                    {
                        mNewNo = Convert.ToInt32(dt.Rows[0]["Serial"].ToString());
                        mNewNo += 1;
                        strAlias = mNewNo.ToString().PadLeft(3, '0');

                        ViewState["NumNo"] = strAlias;


                    }

                }
                else
                {
                    mNewNo += 1;
                    strAlias = mNewNo.ToString().PadLeft(3, '0');

                    ViewState["NumNo"] = strAlias;
                }
            }
        }


    }
}