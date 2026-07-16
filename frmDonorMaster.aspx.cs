using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class frmDonorMaster : System.Web.UI.Page
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
                LoadYear();
                ddlYear.SelectedIndex = 1;
                FillCBState();
                //  LoadData();
                ViewState["Save"] = "Save";
                FillActive(1);
                LoadDataMain();
                ViewState["DonorID"] = "";
            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }

        }

    }
    protected void ddlStartYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlYear.SelectedValue) == 2024)
        {
            btnsave.Visible = true;
        }
        else
        {
            btnsave.Visible = false;
        }
        LoadDataMain();
    }
    public void LoadYear()
    {
        //int fillYear = DateTime.Now.Year;
        //int StatYear = 2016;
        //ddlStartYear.Items.Add(new ListItem("--Select--", "0", true));
        //for (int i = StatYear; i <= fillYear; i++)
        //{
        //    string num = Convert.ToString(i);

        //    ddlStartYear.Items.Add(new ListItem(num));


        //}
    }
    public void FillActive(Int32 Flag)
    {
        conditions = "";
        conditions = "LookupFlag ='IA' and Active=1 ";
        if (Flag == 1)
        {
            conditions += " and LookupCode=1";
        }
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlStatus, "Description", "LookupCode", "Select");



    }
    public void Upload(object sender, EventArgs e)
    {
        txtMuhala.Text = "";
        FillCBDist();
        chkBlock.Items.Clear();
        txtMuhala1.Text = "";

    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        FillActive(1);
        ClearData();

        ViewState["Save"] = "Save";
        ViewState["DonorID"] = "";
    }
    public void txtDistfff_TextChanged(object sender, EventArgs e)
    {
        string Oid = "";
        bool Flag = false;
        foreach (ListItem item in chkID.Items)
        {
            if (item.Selected)
            {
                if (item.Value == "0")
                {
                    Flag = true;
                    item.Enabled = true;
                    Session["eee"] = "BCC";
                    break;


                }
                else
                {
                    Oid += "" + item.Value + "" + ",";
                    item.Enabled = true;
                }

            }
        }
        if (Flag == true)
        {
            foreach (ListItem item in chkID.Items)
            {
                if (item.Value == "0")
                {
                    item.Enabled = true;
                }
                else
                {
                    item.Enabled = false;
                }
            }

        }
        else
        {
            foreach (ListItem item in chkID.Items)
            {
                item.Enabled = true;
            }
        }

    }
    protected void btnNewSerach_Click(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void GVMain_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "GVUIO")
        {
            int iIndex = Convert.ToInt32(e.CommandArgument);
            string DID = GVMain.DataKeys[iIndex]["DID"].ToString();
            FillControls(DID);
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

    public void ClearData()
    {
        txtDonorName.Text = "";

        txtFromDate.Text = "";
        ddlMonth.SelectedIndex = 0;
        ddlStartYear.SelectedIndex = 0;
        foreach (ListItem item in chkID.Items)
        {
            item.Selected = false;
        }
        txtMuhala5.Text = "";
        txtTodate.Text = "";
        ddInGeography.SelectedIndex = 0;
        ddInGeography_SelectedIndexChanged(ddInGeography, null);
        ddlFrequency.SelectedIndex = 0;
        ddlQualitative.SelectedIndex = 0;
        ddlAGP.SelectedIndex = 0;
        ddlPhage.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        rblDist.SelectedValue = "1";
        ddlOutcome.SelectedIndex = 0;
        txtActiveDate.Text = "";
        txtDeAvtiveDate.Text = "";
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
        GvRight.DataSource = null;
        GvRight.DataBind();
        txt_pbname.Text = "";
        txtMuhala.Text = "";
        txtMuhala1.Text = "";

        ViewState["dtAttendent"] = null;
        ViewState["dtselect"] = null;
        ViewState["dtselected"] = null;


    }
    public void FillControls(string ID)
    {
        DataTable dtMain = objMain.LoadData("select * FROM [mstDonorDeatils]  where DID=" + ID + "   ");
        if (dtMain.Rows.Count > 0)
        {
            ClearData();
            FillActive(0);
            ViewState["DonorID"] = ID;
            txtDonorName.Text = dtMain.Rows[0]["DonorName"].ToString();
            DateTime fDate = Convert.ToDateTime(dtMain.Rows[0]["FromDate"].ToString());
            txtFromDate.Text = fDate.ToString("dd/MM/yyy");

            DateTime tDate = Convert.ToDateTime(dtMain.Rows[0]["ToDate"].ToString());
            txtTodate.Text = tDate.ToString("dd/MM/yyy");
            ddInGeography.SelectedValue = dtMain.Rows[0]["GeographyID"].ToString();
            ddInGeography_SelectedIndexChanged(ddInGeography, null);
            ddlFrequency.SelectedValue = dtMain.Rows[0]["FrequencyID"].ToString();
            ddlQualitative.SelectedValue = dtMain.Rows[0]["QualitativeID"].ToString();
            ddlAGP.SelectedValue = dtMain.Rows[0]["AGPID"].ToString();
            ddlPhage.SelectedValue = dtMain.Rows[0]["PhaseID"].ToString();
            ddlStatus.SelectedValue = dtMain.Rows[0]["ActiveStatus"].ToString();


            ddlStartYear.SelectedValue = dtMain.Rows[0]["Fyear"].ToString();
            ddlMonth.SelectedValue = dtMain.Rows[0]["Mmonth"].ToString();

            ddlStatus_SelectedIndexChanged(ddlStatus, null);
            rblDist.SelectedValue = dtMain.Rows[0]["DistrictType"].ToString();
            DateTime ActiveDate = Convert.ToDateTime(dtMain.Rows[0]["ActiveDate"].ToString());
            txtActiveDate.Text = ActiveDate.ToString("dd/MM/yyy");
            if (dtMain.Rows[0]["DeActiveDate"].ToString() == "01/01/1900 00:00:00" || dtMain.Rows[0]["DeActiveDate"].ToString() == "")
            {
                txtDeAvtiveDate.Text = "";
            }
            else
            {
                DateTime DateJoing = Convert.ToDateTime(dtMain.Rows[0]["DeActiveDate"].ToString());
                txtDeAvtiveDate.Text = DateJoing.ToString("dd/MM/yyy");

            }
            if (dtMain.Rows[0]["DistrictType"].ToString() == "1")
            {

                if (Convert.ToInt32(ddInGeography.SelectedValue) == 2)
                {
                    DataTable dtState = objMain.LoadData("select Distinct mst2District.StateCode from mstDonorDistrictProfile inner join mst2District on mst2District.DistrictCode=mstDonorDistrictProfile.DistrictCode  where DID=" + ID + "   ");
                    DataTable dtDistr = objMain.LoadData("select mstDonorDistrictProfile.DistrictCode,mst2District.StateCode from mstDonorDistrictProfile inner join mst2District on mst2District.DistrictCode=mstDonorDistrictProfile.DistrictCode  where DID=" + ID + "   ");
                    string State = "";
                    foreach (DataRow dr in dtState.Rows)
                    {
                        foreach (ListItem item in ChkState.Items)
                        {
                            if (item.Value == dr["StateCode"].ToString())
                            {
                                item.Selected = true;
                                State += item.Text + ",";
                            }
                        }
                    }
                    if (State.Length > 0)
                    {
                        State = State.Substring(0, State.LastIndexOf(","));
                        txt_pbname.Text = State;
                    }
                    FillCBDist();
                    string Dist = "";
                    foreach (DataRow dr in dtDistr.Rows)
                    {
                        foreach (ListItem item in chkDistrict.Items)
                        {
                            if (item.Value == dr["DistrictCode"].ToString())
                            {
                                item.Selected = true;
                                Dist += item.Text + ",";
                            }
                        }
                    }
                    if (Dist.Length > 0)
                    {
                        Dist = Dist.Substring(0, Dist.LastIndexOf(","));
                        txtMuhala.Text = Dist;
                    }
                }
                if (Convert.ToInt32(ddInGeography.SelectedValue) == 3)
                {
                    DataTable dtState = objMain.LoadData("select Distinct mst2District.StateCode from mstDonorDistrictProfile inner join mst3Block on mst3Block.BlockCOde=mstDonorDistrictProfile.BlockCOde inner join mst2District on mst2District.DistrictCode=mst3Block.DistrictCode   where DID=" + ID + "   ");
                    DataTable dtDistr = objMain.LoadData("select  Distinct mst3Block.DistrictCode from mstDonorDistrictProfile inner join mst3Block on mst3Block.BlockCOde=mstDonorDistrictProfile.BlockCOde inner join mst2District on mst2District.DistrictCode=mst3Block.DistrictCode  where DID=" + ID + "   ");
                    DataTable dtBlcok = objMain.LoadData("select mstDonorDistrictProfile.BlockCOde,mst3Block.StateCode,mst3Block.DistrictCode from mstDonorDistrictProfile inner join mst3Block on mst3Block.BlockCOde=mstDonorDistrictProfile.BlockCOde inner join mst2District on mst2District.DistrictCode=mst3Block.DistrictCode  where DID=" + ID + "   ");
                    string State = "";
                    foreach (DataRow dr in dtState.Rows)
                    {
                        foreach (ListItem item in ChkState.Items)
                        {
                            if (item.Value == dr["StateCode"].ToString())
                            {
                                item.Selected = true;
                                State += item.Text + ",";
                            }
                        }
                    }
                    if (State.Length > 0)
                    {
                        State = State.Substring(0, State.LastIndexOf(","));
                        txt_pbname.Text = State;
                    }
                    FillCBDist();
                    string Dist = "";
                    foreach (DataRow dr in dtDistr.Rows)
                    {
                        foreach (ListItem item in chkDistrict.Items)
                        {
                            if (item.Value == dr["DistrictCode"].ToString())
                            {
                                item.Selected = true;
                                Dist += item.Text + ",";
                            }
                        }
                    }
                    if (Dist.Length > 0)
                    {
                        Dist = Dist.Substring(0, Dist.LastIndexOf(","));
                        txtMuhala.Text = Dist;
                    }
                    FillCBBock();
                    string BCode = "";
                    foreach (DataRow dr in dtBlcok.Rows)
                    {
                        foreach (ListItem item in chkBlock.Items)
                        {
                            if (item.Value == dr["BlockCode"].ToString())
                            {
                                item.Selected = true;
                                BCode += item.Text + ",";
                            }
                        }
                    }
                    if (BCode.Length > 0)
                    {
                        BCode = BCode.Substring(0, BCode.LastIndexOf(","));
                        txtMuhala1.Text = BCode;
                    }
                }
            }

            if (dtMain.Rows[0]["DistrictType"].ToString() == "2")
            {

                if (Convert.ToInt32(ddInGeography.SelectedValue) == 2)
                {
                    DataTable dtState = objMain.LoadData("select Distinct mst5Village.StateCode from mstDonorDistrictProfile inner join mst5Village on mst5Village.AdminDistrictCode=mstDonorDistrictProfile.DistrictCode   where DID=" + ID + " and mst5Village.FYear ='" + Session["FinYear"].ToString() + "'  ");
                    DataTable dtDistr = objMain.LoadData("select  Distinct AdminDistrictCode from mstDonorDistrictProfile inner join mst5Village on mst5Village.AdminDistrictCode=mstDonorDistrictProfile.DistrictCode   where DID=" + ID + "  and mst5Village.FYear ='" + Session["FinYear"].ToString() + "' ");
                    string State = "";
                    foreach (DataRow dr in dtState.Rows)
                    {
                        foreach (ListItem item in ChkState.Items)
                        {
                            if (item.Value == dr["StateCode"].ToString())
                            {
                                item.Selected = true;
                                State += item.Text + ",";
                            }
                        }
                    }
                    if (State.Length > 0)
                    {
                        State = State.Substring(0, State.LastIndexOf(","));
                        txt_pbname.Text = State;
                    }
                    FillCBDist();
                    string Dist = "";
                    foreach (DataRow dr in dtDistr.Rows)
                    {
                        foreach (ListItem item in chkDistrict.Items)
                        {
                            if (item.Value == dr["AdminDistrictCode"].ToString())
                            {
                                item.Selected = true;
                                Dist += item.Text + ",";
                            }
                        }
                    }
                    if (Dist.Length > 0)
                    {
                        Dist = Dist.Substring(0, Dist.LastIndexOf(","));
                        txtMuhala.Text = Dist;
                    }
                }
                if (Convert.ToInt32(ddInGeography.SelectedValue) == 3)
                {
                    DataTable dtState = objMain.LoadData("select Distinct mst5Village.StateCode from mstDonorDistrictProfile inner join mst5Village on mst5Village.MainBlockCode=mstDonorDistrictProfile.BlockCode   where DID=" + ID + " and mst5Village.FYear ='" + Session["FinYear"].ToString() + "'  ");
                    DataTable dtDistr = objMain.LoadData("select  Distinct AdminDistrictCode from mstDonorDistrictProfile inner join mst5Village on mst5Village.MainBlockCode=mstDonorDistrictProfile.BlockCode   where DID=" + ID + "  and mst5Village.FYear ='" + Session["FinYear"].ToString() + "' ");
                    DataTable dtBlcok = objMain.LoadData("select Distinct MainBlockCode from mstDonorDistrictProfile inner join mst5Village on mst5Village.MainBlockCode=mstDonorDistrictProfile.BlockCode   where DID=" + ID + " and mst5Village.FYear ='" + Session["FinYear"].ToString() + "'  ");
                    string State = "";
                    foreach (DataRow dr in dtState.Rows)
                    {
                        foreach (ListItem item in ChkState.Items)
                        {
                            if (item.Value == dr["StateCode"].ToString())
                            {
                                item.Selected = true;
                                State += item.Text + ",";
                            }
                        }
                    }
                    if (State.Length > 0)
                    {
                        State = State.Substring(0, State.LastIndexOf(","));
                        txt_pbname.Text = State;
                    }
                    FillCBDist();
                    string Dist = "";
                    foreach (DataRow dr in dtDistr.Rows)
                    {
                        foreach (ListItem item in chkDistrict.Items)
                        {
                            if (item.Value == dr["AdminDistrictCode"].ToString())
                            {
                                item.Selected = true;
                                Dist += item.Text + ",";
                            }
                        }
                    }
                    if (Dist.Length > 0)
                    {
                        Dist = Dist.Substring(0, Dist.LastIndexOf(","));
                        txtMuhala.Text = Dist;
                    }
                    FillCBBock();
                    string BCode = "";
                    foreach (DataRow dr in dtBlcok.Rows)
                    {
                        foreach (ListItem item in chkBlock.Items)
                        {
                            if (item.Value == dr["MainBlockCode"].ToString())
                            {
                                item.Selected = true;
                                BCode += item.Text + ",";
                            }
                        }
                    }
                    if (BCode.Length > 0)
                    {
                        BCode = BCode.Substring(0, BCode.LastIndexOf(","));
                        txtMuhala1.Text = BCode;
                    }
                }
            }
            DataTable dtCheck = null;
            if (Convert.ToInt32(ddlYear.SelectedValue) >= 2024)
            {
                dtCheck = objMain.LoadData(" SELECT mstIndicatorDeatils.OID as MainID, [mstDonorSuboutcome].[SoutComeID],OutcomeName,      [mstDonorSuboutcome].[DOutcomeID]      ,[SubID]    , [SSubOutcomeName]  FROM mstIndicatorDeatils  inner join mstDonorOutcome on mstDonorOutcome.[DOutcomeID]=mstIndicatorDeatils.[OSID]   inner join [mstDonorSuboutcome] on [mstDonorSuboutcome].[DoutComeID]=mstIndicatorDeatils.[OSID]  and [mstDonorSuboutcome].SubID=mstIndicatorDeatils.OSubID where [mstIndicatorDeatils].[OID] =" + ID + "   ");
            }
            else
            {
                dtCheck = objMain.LoadData(" SELECT mstIndicatorDeatils.OID as MainID, [mstDonorSuboutcome2024].[SoutComeID],OutcomeName,      [mstDonorSuboutcome2024].[DOutcomeID]      ,[SubID]    , [SSubOutcomeName]  FROM mstIndicatorDeatils  inner join mstDonorOutcome2024 on mstDonorOutcome2024.[DOutcomeID]=mstIndicatorDeatils.[OSID]   inner join [mstDonorSuboutcome2024] on [mstDonorSuboutcome2024].[DoutComeID]=mstIndicatorDeatils.[OSID]  and [mstDonorSuboutcome2024].SubID=mstIndicatorDeatils.OSubID where [mstIndicatorDeatils].[OID] =" + ID + "   ");

            }
            GvRight.DataSource = dtCheck;
            GvRight.DataBind();
            ViewState["dtselected"] = dtCheck;

            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }

    }
    public void LoadData()
    {
        bool Flag = false;
        string Oid = "";
        foreach (ListItem item in chkID.Items)
        {
            if (item.Selected)
            {
                if (item.Value == "0")
                {
                    Flag = true;
                    Oid += "'" + item.Text + "'" + ",";
                    break;

                }
                else
                {
                    Oid += "'" + item.Value + "'" + ",";
                }

            }
        }
        if (Oid.Length > 0)
        {
            Oid = Oid.Substring(0, Oid.LastIndexOf(","));
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select any one Indicator in Indicator Box!! ')</script>", false);


            this.chkBlock.Focus();
            return;
        }

        if (ViewState["Save"].ToString() == "Save")
        {
            if (Flag == true)
            {
                DataTable dtCheck = objMain.LoadData(" SELECT 0 MainID, [mstDonorSuboutcome].[SoutComeID],OutcomeName,      [mstDonorSuboutcome].[DOutcomeID]      ,[SubID]    ,[SSubOutcomeName]  FROM [mstDonorSuboutcome]  inner join mstDonorOutcome on mstDonorOutcome.[DOutcomeID]=[mstDonorSuboutcome].[DOutcomeID] where  [mstDonorSuboutcome].[DOutComeID] ='" + ddlOutcome.SelectedValue + "'  ");
                GV_DynamicGrid.DataSource = dtCheck;
                GV_DynamicGrid.DataBind();
                ViewState["dtselect"] = dtCheck;
            }
            else
            {
                if (Oid.Length > 0)
                {
                    DataTable dtCheck = objMain.LoadData(" SELECT 0 MainID, [mstDonorSuboutcome].[SoutComeID],OutcomeName,      [mstDonorSuboutcome].[DOutcomeID]      ,[SubID]    ,[SSubOutcomeName]  FROM [mstDonorSuboutcome]  inner join mstDonorOutcome on mstDonorOutcome.[DOutcomeID]=[mstDonorSuboutcome].[DOutcomeID]    where [mstDonorSuboutcome].[SubID] in(" + Oid + ")  ");
                    GV_DynamicGrid.DataSource = dtCheck;
                    GV_DynamicGrid.DataBind();
                    ViewState["dtselect"] = dtCheck;
                }
            }
        }
        else
        {
            if (Flag == true)
            {
                DataTable dtCheck = objMain.LoadData(" SELECT 0 MainID, [mstDonorSuboutcome].[SoutComeID],OutcomeName,      [mstDonorSuboutcome].[DOutcomeID]      ,[SubID]    ,[SSubOutcomeName]  FROM [mstDonorSuboutcome]  inner join mstDonorOutcome on mstDonorOutcome.[DOutcomeID]=[mstDonorSuboutcome].[DOutcomeID]    left join mstIndicatorDeatils on mstIndicatorDeatils.[OSID]= [mstDonorSuboutcome].[DOutcomeID] and [mstDonorSuboutcome].SubID=mstIndicatorDeatils.OSubID and mstIndicatorDeatils.OID=" + ViewState["DonorID"].ToString() + "   where   mstIndicatorDeatils.[OID] is null and   [mstDonorSuboutcome].[DOutComeID] ='" + ddlOutcome.SelectedValue + "'  and  mstIndicatorDeatils.OSubID is null");
                GV_DynamicGrid.DataSource = dtCheck;
                GV_DynamicGrid.DataBind();
                ViewState["dtselect"] = dtCheck;
            }
            else
            {
                if (Oid.Length > 0)
                {
                    DataTable dtCheck = objMain.LoadData(" SELECT 0 MainID, [mstDonorSuboutcome].[SoutComeID],OutcomeName,      [mstDonorSuboutcome].[DOutcomeID]      ,[SubID]    ,[SSubOutcomeName]  FROM [mstDonorSuboutcome]  inner join mstDonorOutcome on mstDonorOutcome.[DOutcomeID]=[mstDonorSuboutcome].[DOutcomeID] left join mstIndicatorDeatils on mstIndicatorDeatils.[OSID]= [mstDonorSuboutcome].[DOutcomeID] and [mstDonorSuboutcome].SubID=mstIndicatorDeatils.OSubID and mstIndicatorDeatils.OID=" + ViewState["DonorID"].ToString() + "   where [mstDonorSuboutcome].[SubID] in(" + Oid + ")  and  mstIndicatorDeatils.[OID] is null  and  mstIndicatorDeatils.OSubID is null ");
                    GV_DynamicGrid.DataSource = dtCheck;
                    GV_DynamicGrid.DataBind();
                    ViewState["dtselect"] = dtCheck;
                }
            }
        }
    }
    protected void txtdatefrom_TextChanged(object sender, EventArgs e)
    {
        if (txtFromDate.Text != "" && txtTodate.Text != "")
        {
            DateTime startDate = Convert.ToDateTime(txtFromDate.Text);
            DateTime endDate = Convert.ToDateTime(txtTodate.Text);
            if (endDate >= startDate)
            { }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Selection')</script>", false);
                txtFromDate.Text = "";
                txtTodate.Text = "";
                return;
            }
        }
    }
    protected void txtTodate_TextChanged(object sender, EventArgs e)
    {
        if (txtFromDate.Text != "" && txtTodate.Text != "")
        {
            DateTime startDate = Convert.ToDateTime(txtFromDate.Text);
            DateTime endDate = Convert.ToDateTime(txtTodate.Text);
            if (endDate >= startDate)
            { }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Selection')</script>", false);
                txtFromDate.Text = "";
                txtTodate.Text = "";
                return;
            }
        }
    }

    public void txtState_TextChanged(object sender, EventArgs e)
    {
        FillCBDist();
    }
    public void txtDist_TextChanged(object sender, EventArgs e)
    {
        FillCBBock();
    }
    public void LoadDataMain()
    {


        DataTable dtCheck = objMain.LoadData("select DID, DonorName,convert (varchar(10),[FromDate] ,105) as [FromDate], convert (varchar(10),todate ,105) as todate  FROM [mstDonorDeatils] where  Dyear='" + ddlYear.SelectedItem.Text + "'   ");
        GVMain.DataSource = dtCheck;
        GVMain.DataBind();

    }
    protected void UploadDist(object sender, EventArgs e)
    {
        FillCBBock();
    }
    public void FillCBBock()
    {

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







        string conditions = "DistrictCode in(" + ddlDistrict + ") ";
        string ConAdmin = "AdminDistrictCode in(" + ddlDistrict + ") ";

        //     objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        if (Convert.ToInt32(rblDist.SelectedValue) == 2)
        {
            string strQry = " SELECT  distinct MainBlockCode as BlockCode, dbo.TitleCase(upper(MainBlockName))  as BlockName FROM mst5Village where " + ConAdmin + " and FYear ='" + Session["FinYear"].ToString() + "' order by BlockName   ";
            DataTable dtDistrict = objMain.LoadData(strQry);
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            chkBlock.DataSource = dtDistrict;
            chkBlock.DataTextField = "BlockName";
            chkBlock.DataValueField = "BlockCode";
            chkBlock.DataBind();
        }

        else
        {
            string strQry = "  SELECT BlockCode, dbo.TitleCase(upper(BlockName))  as BlockName FROM mst3Block where " + conditions + "  order by BlockName   ";
            DataTable dtDistrict = objMain.LoadData(strQry);
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            chkBlock.DataSource = dtDistrict;
            chkBlock.DataTextField = "BlockName";
            chkBlock.DataValueField = "BlockCode";
            chkBlock.DataBind();

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

                ddlState += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlState.Length > 0)
        {
            ddlState = ddlState.Substring(0, ddlState.LastIndexOf(","));
        }


        if (Convert.ToInt32(rblDist.SelectedValue) == 2)
        {
            conditions = "StateCode in(" + ddlState + ") and mst5Village.FYear ='" + Session["FinYear"].ToString() + "'";
            string strQry = " select distinct AdminDistrictCode as DistrictCode, dbo.TitleCase(upper(AdminDistrictName))  as DistrictName from mst5Village where    " + conditions + "   order by DistrictName   ";
            dtDistrict = objMain.LoadData(strQry);
        }
        else
        {
            conditions = "StateCode in(" + ddlState + ") and mst2District.FYear ='" + Session["FinYear"].ToString() + "'";
            string strQry = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where " + conditions + "  order by DistrictName   ";
            dtDistrict = objMain.LoadData(strQry);
        }


        // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        chkDistrict.DataSource = dtDistrict;
        chkDistrict.DataTextField = "DistrictName";
        chkDistrict.DataValueField = "DistrictCode";
        chkDistrict.DataBind();


    }
    public void FillCBState()
    {
        conditions = "";
        // objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");


        //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
        //string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State   order by StateName   ";
        //DataTable dtState = objMain.LoadData(strQry1);
        //ChkState.DataSource = dtState;
        //ChkState.DataTextField = "StateName";
        //ChkState.DataValueField = "StateCode";
        //ChkState.DataBind();

        SqlParameter[] par1 = new SqlParameter[]
              {
                      new SqlParameter("@user_level_Role",  Convert.ToString(Session["user_level_Role"])),
                      new SqlParameter("@UserName", "" ),
                    new SqlParameter("@StateCode",  ""),
                       new SqlParameter("@Year",  "2024"),
              };

        DataTable dtAllState = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptLoadAllState", par1);
        ChkState.DataSource = dtAllState;
        ChkState.DataTextField = "StateName";
        ChkState.DataValueField = "StateCode";
        ChkState.DataBind();

        string strQry2 = "  SELECT DOutComeID, OutcomeName FROM mstDonorOutcome     order by DOutComeID ";
        DataTable dtID = objMain.LoadData(strQry2);

        objComman.BindDLLMasterTable("mstSchool", "OutcomeName,DOutComeID", dtID, conditions, "Type", "asc", ddlOutcome, "OutcomeName", "DOutComeID", "Select");



    }
    protected void ddlOutcome_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strQry2 = " SELECT '0' as SubID,'ALL' as SSubOutcomeName,0 DOutComeID FROM mstDonorSuboutcome  union SELECT SubID, SSubOutcomeName,DOutComeID FROM mstDonorSuboutcome where DOutComeID='" + ddlOutcome.SelectedValue + "'    order by DOutComeID ";
        DataTable dtID = objMain.LoadData(strQry2);

        chkID.DataSource = dtID;
        chkID.DataTextField = "SSubOutcomeName";
        chkID.DataValueField = "SubID";
        chkID.DataBind();
        txtMuhala5.Text = "";

    }
    protected void rblDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(rblDist.SelectedValue) == 2 || Convert.ToInt32(rblDist.SelectedValue) == 1)
        {
            FillCBDist();
            chkBlock.Items.Clear();
            txtMuhala1.Text = "";
            txtMuhala.Text = "";
        }
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlStatus.SelectedValue) == 2)
        {
            DActive.Visible = true;
            IDActive.Visible = false;
        }
        else
        {
            DActive.Visible = false;
            IDActive.Visible = true;
        }
    }
    protected void ddInGeography_SelectedIndexChanged(object sender, EventArgs e)
    {
        divState.Visible = false;
        divDist.Visible = false;
        divBlock.Visible = false;
        divDistype.Visible = false;

        if (Convert.ToInt32(ddInGeography.SelectedValue) == 2)
        {
            divState.Visible = true;
            divDist.Visible = true;
            divDistype.Visible = true;
        }
        if (Convert.ToInt32(ddInGeography.SelectedValue) == 3)
        {
            divState.Visible = true;
            divDist.Visible = true;
            divBlock.Visible = true;
            divDistype.Visible = true;
        }
    }
    protected void btnprevone_Click(object sender, EventArgs e)
    {
        int indcount = 0;
        DataTable dtAttendent = null;
        foreach (GridViewRow Itemst in GV_DynamicGrid.Rows)
        {
            if (((CheckBox)Itemst.FindControl("rptCB")).Checked)
            {
                indcount++;
            }
        }


        if (indcount > 0)
        {


            DataTable dtselect = (DataTable)ViewState["dtselect"];
            DataTable dtselected = (DataTable)ViewState["dtselected"];
            if (dtselected == null && dtselect == null) { return; }
            if (dtselected == null) { dtselected = dtselect.Clone(); }
            if (dtselect == null) { dtselect = dtselected.Clone(); }
            int tmp = 0;
            DataRow dr;
            DataRow drAtt;
            foreach (GridViewRow Itemst in GV_DynamicGrid.Rows)
            {
                if (((CheckBox)Itemst.FindControl("rptCB")).Checked)
                {

                    int ind = Itemst.DataItemIndex;


                    Int32 DayCount = 0;

                    dr = dtselected.NewRow();

                    dr["MainID"] = GV_DynamicGrid.DataKeys[ind]["MainID"];
                    dr["SoutComeID"] = GV_DynamicGrid.DataKeys[ind]["SoutComeID"];
                    dr["OutcomeName"] = GV_DynamicGrid.DataKeys[ind]["OutcomeName"];
                    dr["SubID"] = GV_DynamicGrid.DataKeys[ind]["SubID"];
                    dr["SSubOutcomeName"] = GV_DynamicGrid.DataKeys[ind]["SSubOutcomeName"];
                    dr["DOutcomeID"] = GV_DynamicGrid.DataKeys[ind]["DOutcomeID"];
                    dtselected.Rows.Add(dr);
                    dtselect.Rows.RemoveAt(ind - tmp);

                    //drAtt = dtAttendent.NewRow();

                    //drAtt["UniqueCode"] = gvSerach.DataKeys[ind]["UniqueCode"];
                    //drAtt["Day1"] = gvSerach.Rows[ind].FindControl("lblDay1");
                    //drAtt["Day2"] = gvSerach.Rows[ind].FindControl("lblDay2");
                    //drAtt["Day3"] = gvSerach.Rows[ind].FindControl("lblDay3");

                    //dtAttendent.Rows.Add(dr);


                    tmp++;
                }

            }
            //ViewState["dtAttendent"] = dtAttendent;

            GvRight.DataSource = dtselected;
            GvRight.DataBind();

            GV_DynamicGrid.DataSource = dtselect;
            GV_DynamicGrid.DataBind();

            ViewState["dtselect"] = dtselect;
            ViewState["dtselected"] = dtselected;

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select any one Indicator in Indicator Box!! ')</script>", false);


            this.chkBlock.Focus();
            return;
        }


    }

    protected void btnnextone_Click(object sender, EventArgs e)
    {


        int indcount = 0;
        foreach (GridViewRow Itemst in GvRight.Rows)
        {
            if (((CheckBox)Itemst.FindControl("rptCB")).Checked)
            {
                indcount++;
            }
        }


        if (indcount > 0)
        {


            DataTable dtAttendent = (DataTable)ViewState["dtAttendent"];

            DataTable dtselect = (DataTable)ViewState["dtselect"];
            DataTable dtselected = (DataTable)ViewState["dtselected"];
            if (dtselected == null && dtselect == null) { return; }
            if (dtselected == null) { dtselected = dtselect.Clone(); }
            if (dtselect == null) { dtselect = dtselected.Clone(); }
            int tmp = 0;
            DataRow dr;
            foreach (GridViewRow Itemst in GvRight.Rows)
            {
                if (((CheckBox)Itemst.FindControl("rptCB")).Checked)
                {
                    int ind = Itemst.DataItemIndex;
                    dr = dtselect.NewRow();
                    dr["MainID"] = GvRight.DataKeys[ind]["MainID"];
                    dr["SoutComeID"] = GvRight.DataKeys[ind]["SoutComeID"];
                    dr["DOutcomeID"] = GvRight.DataKeys[ind]["DOutcomeID"];
                    dr["OutcomeName"] = GvRight.DataKeys[ind]["OutcomeName"];
                    dr["SubID"] = GvRight.DataKeys[ind]["SubID"];
                    dr["SSubOutcomeName"] = GvRight.DataKeys[ind]["SSubOutcomeName"];

                    dtselect.Rows.Add(dr);
                    dtselected.Rows.RemoveAt(ind - tmp);
                    //  dtAttendent.Rows.RemoveAt(ind - tmp);
                    tmp++;


                }
            }
            //ViewState["dtAttendent"] = dtAttendent;

            GvRight.DataSource = dtselected;
            GvRight.DataBind();

            GV_DynamicGrid.DataSource = dtselect;
            GV_DynamicGrid.DataBind();

            ViewState["dtselect"] = dtselect;
            ViewState["dtselected"] = dtselected;
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select any one Indicator in Indicator Box!! ')</script>", false);


            this.chkBlock.Focus();
            return;
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
    protected void btnSave_Click(object sender, EventArgs e)
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
    }
    private void Save_Update(int SchoolCode)
    {
        string Dist = "";
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                Dist += "'" + item.Value + "'" + ",";


            }
        }
        string Block = "";
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                Block += "'" + item.Value + "'" + ",";


            }
        }
        if (Convert.ToInt32(ddInGeography.SelectedValue) == 3)
        {
            if (Block.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Block !! ')</script>", false);


                this.chkBlock.Focus();
                return;
            }

        }
        if (Convert.ToInt32(ddInGeography.SelectedValue) == 2)
        {
            if (Dist.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select District !! ')</script>", false);


                this.chkDistrict.Focus();
                return;
            }

        }
        if (Convert.ToInt32(ddlStatus.SelectedValue) == 1)
        {
            if (txtActiveDate.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Active Date !! ')</script>", false);


                this.txtActiveDate.Focus();
                return;
            }
        }
        if (Convert.ToInt32(ddlStatus.SelectedValue) == 2)
        {
            if (txtDeAvtiveDate.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Deactive Date !! ')</script>", false);


                this.txtDeAvtiveDate.Focus();
                return;
            }
            if (txtDeAvtiveDate.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Deactive Reason!!')</script>", false);


                this.txtDeAvtiveDate.Focus();
                return;
            }
        }







        int mainResult = 0;



        if (ViewState["Save"].ToString() == "Save")
        {
            DataTable dtCheck = objMain.LoadData(" SELECT * FROM [dbo].[mstDonorDeatils]  where   DonorName='" + txtDonorName.Text + "' and  Dyear='" + Convert.ToString(Session["FinYear"]) + "'");


            if (dtCheck.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Donor Name Allready Exit')</script>", false);
                return;
            }



            ViewState["Save"] = "fff";




            //System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(FileuploadAttach.PostedFile.InputStream);
            //System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 81);

            string INActiveDate;

            string ActivieDate = "";

            string FromDate = "";

            string TOdate = "";


            if (txtFromDate.Text != "")
            {
                string FromDate1 = txtFromDate.Text;
                string[] b = FromDate1.Split('/');
                FromDate = b[2] + '-' + b[1] + '-' + b[0];

            }
            if (txtTodate.Text != "")
            {
                string TOdate1 = txtTodate.Text;
                string[] b = TOdate1.Split('/');
                TOdate = b[2] + '-' + b[1] + '-' + b[0];

            }
            if (txtActiveDate.Text != "")
            {
                string ActivieDate1 = txtActiveDate.Text;
                string[] b = ActivieDate1.Split('/');
                ActivieDate = b[2] + '-' + b[1] + '-' + b[0];

            }
            else
            {
                ActivieDate = "1900-01-01";
            }
            if (txtDeAvtiveDate.Text != "")
            {
                string AINActiveDate1 = txtDeAvtiveDate.Text;
                string[] b = AINActiveDate1.Split('/');
                INActiveDate = b[2] + '-' + b[1] + '-' + b[0];

            }
            else
            {
                INActiveDate = "1900-01-01";
            }

            int result = 0;
            mainResult = DonorProfile(0, txtDonorName.Text, Convert.ToDateTime(FromDate), Convert.ToDateTime(TOdate), Convert.ToInt32(ddInGeography.SelectedValue), Convert.ToInt32(rblDist.SelectedValue), Convert.ToInt32(ddlFrequency.SelectedValue), Convert.ToInt32(ddlQualitative.SelectedValue), Convert.ToInt32(ddlAGP.SelectedValue), Convert.ToInt32(ddlPhage.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToDateTime(ActivieDate), Convert.ToDateTime(INActiveDate), Session["username"].ToString(), txt_pbname.Text, txtMuhala.Text, txtMuhala1.Text);
            if (mainResult > 0)
            {
                ViewState["DonorID"] = mainResult;
                string Oid = "";
                if (Convert.ToInt32(ddInGeography.SelectedValue) == 3)
                {
                    foreach (ListItem item in chkBlock.Items)
                    {
                        if (item.Selected)
                        {

                            Oid += "" + item.Value + "" + "";


                            SqlParameter[] parm4 = new SqlParameter[]
                       {
                                  new SqlParameter("@DID",mainResult),
                                   new SqlParameter("@BlockCode",item.Value ),
                                     new SqlParameter("@Flag","B"),
                         };
                            result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeletetblDonor", parm4);

                        }

                    }
                }
                string District = "";
                if (Convert.ToInt32(ddInGeography.SelectedValue) == 2)
                {
                    foreach (ListItem item in chkDistrict.Items)
                    {
                        if (item.Selected)
                        {

                            District += "" + item.Value + "" + "";
                            SqlParameter[] parm4 = new SqlParameter[]
                       {
                                  new SqlParameter("@DID",mainResult),
                                   new SqlParameter("@BlockCode",item.Value ),
                                     new SqlParameter("@Flag","D"),
                         };
                            result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeletetblDonor", parm4);


                        }
                    }
                }
                foreach (GridViewRow Itemst in GvRight.Rows)
                {

                    int ind = Itemst.DataItemIndex;

                    string SoutComeID = GvRight.DataKeys[ind]["DOutcomeID"].ToString();
                    string SubID = GvRight.DataKeys[ind]["SubID"].ToString();

                    SqlParameter[] parm4 = new SqlParameter[]
                      {
                                  new SqlParameter("@DID",mainResult),
                                   new SqlParameter("@OSID",SoutComeID ),
                                     new SqlParameter("@OSubID",SubID),
                        };
                    result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateDonorIndicatore", parm4);



                }

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                LoadDataMain();
                //txtIDNO.Text = TBCode;
            }
        }
        else
        {

            int result = 0;
            string INActiveDate;

            string ActivieDate = "";

            string FromDate = "";

            string TOdate = "";


            if (txtFromDate.Text != "")
            {
                string FromDate1 = txtFromDate.Text;
                string[] b = FromDate1.Split('/');
                FromDate = b[2] + '-' + b[1] + '-' + b[0];

            }
            if (txtTodate.Text != "")
            {
                string TOdate1 = txtTodate.Text;
                string[] b = TOdate1.Split('/');
                TOdate = b[2] + '-' + b[1] + '-' + b[0];

            }
            if (txtActiveDate.Text != "")
            {
                string ActivieDate1 = txtActiveDate.Text;
                string[] b = ActivieDate1.Split('/');
                ActivieDate = b[2] + '-' + b[1] + '-' + b[0];

            }
            else
            {
                ActivieDate = "1900-01-01";
            }
            if (txtDeAvtiveDate.Text != "")
            {
                string AINActiveDate1 = txtDeAvtiveDate.Text;
                string[] b = AINActiveDate1.Split('/');
                INActiveDate = b[2] + '-' + b[1] + '-' + b[0];

            }
            else
            {
                INActiveDate = "1900-01-01";
            }


            SqlParameter[] parm = new SqlParameter[]
             {
                  new SqlParameter("@DID",ViewState["DonorID"].ToString()),
               };
            result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeletetblDonor", parm);

            string Oid = "";
            if (Convert.ToInt32(ddInGeography.SelectedValue) == 3)
            {
                foreach (ListItem item in chkBlock.Items)
                {
                    if (item.Selected)
                    {

                        Oid += "" + item.Value + "" + "";


                        SqlParameter[] parm4 = new SqlParameter[]
                          {
                                  new SqlParameter("@DID",ViewState["DonorID"].ToString()),
                                   new SqlParameter("@BlockCode",item.Value ),
                                     new SqlParameter("@Flag","B"),
                            };
                        result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeletetblDonor", parm4);

                    }

                }
            }
            string District = "";
            if (Convert.ToInt32(ddInGeography.SelectedValue) == 2)
            {
                foreach (ListItem item in chkDistrict.Items)
                {
                    if (item.Selected)
                    {

                        District += "" + item.Value + "" + "";
                        SqlParameter[] parm4 = new SqlParameter[]
                         {
                                  new SqlParameter("@DID",ViewState["DonorID"].ToString()),
                                   new SqlParameter("@BlockCode",item.Value ),
                                     new SqlParameter("@Flag","D"),
                           };
                        result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeletetblDonor", parm4);

                    }
                }
            }
            foreach (GridViewRow Itemst in GvRight.Rows)
            {

                int ind = Itemst.DataItemIndex;

                string SoutComeID = GvRight.DataKeys[ind]["DOutcomeID"].ToString();
                string SubID = GvRight.DataKeys[ind]["SubID"].ToString();

                SqlParameter[] parm4 = new SqlParameter[]
                        {
                                  new SqlParameter("@DID",ViewState["DonorID"].ToString()),
                                   new SqlParameter("@OSID",SoutComeID ),
                                     new SqlParameter("@OSubID",SubID),
                          };
                result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateDonorIndicatore", parm4);



            }


            mainResult = DonorProfileUpdate(Convert.ToInt32(ViewState["DonorID"].ToString()), txtDonorName.Text, Convert.ToDateTime(FromDate), Convert.ToDateTime(TOdate), Convert.ToInt32(ddInGeography.SelectedValue), Convert.ToInt32(rblDist.SelectedValue), Convert.ToInt32(ddlFrequency.SelectedValue), Convert.ToInt32(ddlQualitative.SelectedValue), Convert.ToInt32(ddlAGP.SelectedValue), Convert.ToInt32(ddlPhage.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToDateTime(ActivieDate), Convert.ToDateTime(INActiveDate), Session["username"].ToString(), txt_pbname.Text, txtMuhala.Text, txtMuhala1.Text);
            if (mainResult > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                LoadDataMain();
                //txtIDNO.Text = TBCode;
            }
        }



    }
    public int DonorProfile(Int32 DID, string DonorName, DateTime FromDate, DateTime ToDate, Int32 GeographyID, Int32 DistrictType, Int32 FrequencyID, int QualitativeID, int AGPID, int PhaseID, int ActiveStatus, DateTime ActiveDate, DateTime DeActiveDate, string createby, string State, string Dist, string Block)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@ID", DID),
            new SqlParameter("@DonorName", DonorName),
            new SqlParameter("@FromDate", FromDate),
               new SqlParameter("@ToDate", ToDate),
            new SqlParameter("@GeographyID", GeographyID),
            new SqlParameter("@DistrictType", DistrictType),
            new SqlParameter("@FrequencyID", FrequencyID),
            new SqlParameter("@QualitativeID", QualitativeID),
            new SqlParameter("@AGPID", AGPID),
            new SqlParameter("@PhaseID", PhaseID),
            new SqlParameter("@ActiveStatus", ActiveStatus),
            new SqlParameter("@ActiveDate", ActiveDate),
            new SqlParameter("@DeActiveDate", DeActiveDate),
            new SqlParameter("@createby", createby),
            new SqlParameter("@State", State),
            new SqlParameter("@District", Dist),
            new SqlParameter("@Block", Block),
               new SqlParameter("@Fyear", ddlStartYear.SelectedValue),
            new SqlParameter("@Mmonth", ddlMonth.SelectedValue),
        };
        Object Icount;

        Icount = SqlHelper.ExecuteScaler(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InserUpdateDonorNew", cmdParameters);
        return Convert.ToInt32(Icount);
    }
    public int DonorProfileUpdate(Int32 DID, string DonorName, DateTime FromDate, DateTime ToDate, Int32 GeographyID, Int32 DistrictType, Int32 FrequencyID, int QualitativeID, int AGPID, int PhaseID, int ActiveStatus, DateTime ActiveDate, DateTime DeActiveDate, string createby, string State, string Dist, string Block)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@ID", DID),
            new SqlParameter("@DonorName", DonorName),
            new SqlParameter("@FromDate", FromDate),
               new SqlParameter("@ToDate", ToDate),
            new SqlParameter("@GeographyID", GeographyID),
            new SqlParameter("@DistrictType", DistrictType),
            new SqlParameter("@FrequencyID", FrequencyID),
            new SqlParameter("@QualitativeID", QualitativeID),
            new SqlParameter("@AGPID", AGPID),
            new SqlParameter("@PhaseID", PhaseID),
            new SqlParameter("@ActiveStatus", ActiveStatus),
            new SqlParameter("@ActiveDate", ActiveDate),
            new SqlParameter("@DeActiveDate", DeActiveDate),


            new SqlParameter("@createby", createby),
               new SqlParameter("@State", State),
            new SqlParameter("@District", Dist),
            new SqlParameter("@Block", Block),
               new SqlParameter("@Fyear", ddlStartYear.SelectedValue),
            new SqlParameter("@Mmonth", ddlMonth.SelectedValue),

        };


        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InserUpdateDonorNew", cmdParameters);

    }
    protected void btnReprot_Click(object sender, EventArgs e)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@ffh",""),


        };
        DataTable dt = null;

        if (Convert.ToInt32(ddlYear.SelectedValue) == 2025)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptDonorMasterReport]", cmdParameters);
        }
        else if (Convert.ToInt32(ddlYear.SelectedValue) == 2024)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptDonorMasterReport2024]", cmdParameters);
        }
        else
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptDonorMasterReport2023]", cmdParameters);

        }
        if (dt.Rows.Count > 0)
        {
            ExporttoExcel(dt, "DonorMaster");
        }

    }
    private void ExporttoExcel(DataTable table, string FileName)
    {
        try
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
                    HttpContext.Current.Response.Write(table.Columns[j]);
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
        catch
        {

            throw;
        }
    }
}