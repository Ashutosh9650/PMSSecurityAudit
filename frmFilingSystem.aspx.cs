using DocumentFormat.OpenXml.Spreadsheet;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class frmFilingSystem : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    public bool edit_status = false;
    public static string STRPRINTCONTENT2;
    SqlInjection sqlInjection = new SqlInjection();


    string flag = "";
    Password objPass = new Password();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                LoadYear();
                LoadUserLeavel();


                ViewState["1"] = "ss";
                UserLevelFilter();
                if (Request.QueryString["ID"] != null)
                {
                    string QueryString = Request.QueryString["ID"];
                    string[] a = QueryString.Split(',');
                    ddlDistrict.SelectedValue = Session["NewDistrictCode"].ToString();
                    ddlBlock.SelectedValue = Convert.ToString(a[0].ToString());
                    ddlBlock_SelectedIndexChanged(ddlBlock, null);

                    ddlVillage.SelectedValue = Convert.ToString(a[1].ToString());
                    ddlVillage_SelectedIndexChanged(ddlVillage, null);

                }

                // LoadData();
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

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        //  btnApprove.Attributes.Add("onclick", "javascript:return " + "confirm('Please confirm if you want to approve? ')");


        Response.Redirect("~/FrmEnrollmentBlockWiseGenration.aspx");


    }
    #region ************* Button Click Event
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        try
        {
            LoadData();
            //  GVSealSign.Enabled = false;
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnMain_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;
            int indx = row.RowIndex;
            Label lblSchoolCode = (Label)GVSealSign.Rows[indx].FindControl("lblSchoolCode");

            //GenratePDF(lblSchoolCode.Text);
            // GenratePDF_pdfTable(lblSchoolCode.Text);

            // GenrateExcel_toPDf(lblSchoolCode.Text);
            //ConvertExcelTopdf(Convert.ToString(ViewState["Filename"]));
            string st = RetStringBuilder(lblSchoolCode.Text);
            PrintCards(st);
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion
    #region  ************ Fill Method
    public void FillCBBock()
    {
        string conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  ";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' ";
        }
        else if (Session["user_level_Role"].ToString() == "4")
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode in( " + Session["BlockCode"].ToString() + " ) and FYear ='" + ddlYear.SelectedItem.Text + "' ";
        }
        else
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' ";
        }

        //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        string strQry = "  SELECT BlockCode, dbo.TitleCase(upper(BlockName))  as BlockName FROM mst3Block where " + conditions + "  order by BlockName   ";
        DataTable dtDistrict = objMain.LoadData(strQry);
        ddlBlock.DataSource = dtDistrict;
        ddlBlock.DataTextField = "BlockName";
        ddlBlock.DataValueField = "BlockCode";
        ddlBlock.DataBind();
        if (Session["user_level_Role"].ToString() == "4")
        {
            foreach (System.Web.UI.WebControls.ListItem item in ddlBlock.Items)
            {


                item.Selected = true;

            }

            ddlBlock_SelectedIndexChanged(ddlVillage, null);
            ddlBlock.Enabled = false;
        }
    }
    public void LoadYear()
    {
        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, "", "Type", "asc", ddlYear, "Type", "ID", "Select");
        ddlYear.SelectedIndex = 1;
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
            objComman.BindDLLDatatable("mst1State", dtAllState, "StateCode, dbo.TitleCase(upper(StateName)) as StateName", "", "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

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
            objComman.BindDLLDatatable("mst1State", dtAllState, "StateCode, dbo.TitleCase(upper(StateName)) as StateName", "", "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

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
            objComman.BindDLLDatatable("mst1State", dtAllState, "StateCode, dbo.TitleCase(upper(StateName)) as StateName", "", "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");


        }

    }
    public void LoadUserLeavel()
    {
        AlllStateCode();
        string conditions = "";
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

            conditions = "StateCode ='" + ddlState.SelectedValue + "'  and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

            ddlDistrict.SelectedIndex = 0;

            //ddlDistrict.SelectedIndex = 1;
            //string conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }

        else
        {

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

        if (Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "136") // Role DEO
        {
            //lnkbtnsend.visible = false;
            foreach (GridViewRow row in GVSealSign.Rows)
            {
                LinkButton lnkbtnsend = (LinkButton)row.FindControl("lnkbtnsend");
                LinkButton lnkbtnRecieve = (LinkButton)row.FindControl("lnkbtnRecieve");

                lnkbtnsend.Visible = true;
                lnkbtnRecieve.Visible = true;
            }

        }
        else if (Session["user_level"].ToString() == "19" || Session["user_level"].ToString() == "137") // Role
        {
            foreach (GridViewRow row in GVSealSign.Rows)
            {
                LinkButton lnkbtnsend = (LinkButton)row.FindControl("lnkbtnsend");
                LinkButton lnkbtnRecieve = (LinkButton)row.FindControl("lnkbtnRecieve");
                lnkbtnsend.Visible = true;
                lnkbtnRecieve.Visible = true;
            }
        }





    }
    public void UserLevelFilter()
    {

        string strQry = "";
        string Cond = "Module='Enroll'";
        strQry = "Select * from MstUserRight  where " + Cond + " and Role_Id=" + Session["user_level"].ToString() + "   ";


        DataTable dtRole = objMain.LoadData(strQry);

        if (dtRole.Rows.Count > 0)
        {
            vADD = Convert.ToBoolean(dtRole.Rows[0]["AddStatus"].ToString());
            vVerify = Convert.ToBoolean(dtRole.Rows[0]["verify_Status"].ToString());
            vDelete = Convert.ToBoolean(dtRole.Rows[0]["Delete_status"].ToString());
            edit_status = Convert.ToBoolean(dtRole.Rows[0]["edit_status"].ToString());
            ViewState["vADD"] = vADD;
            ViewState["vVerify"] = vVerify;
            ViewState["vDelete"] = vDelete;
            ViewState["edit_status"] = edit_status;
        }
    }
    public void FillCBState()
    {
        string conditions = "";
        objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");
    }
    public void FillCBDist()
    {
        string conditions = "";
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
    public void FillCVillage()
    {
        string conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'";
        string ddlBlockStr = "";
        foreach (System.Web.UI.WebControls.ListItem item in ddlBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlockStr += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlockStr.Length > 0)
        {
            ddlBlockStr = ddlBlockStr.Substring(0, ddlBlockStr.LastIndexOf(","));
        }
        conditions += " and BlockCode in (" + ddlBlockStr + ") ";
        string strQry = "  SELECT ClusterCode,dbo.TitleCase(upper(ClusterName)) as ClusterName FROM mstcluster where " + conditions + "  order by ClusterName   ";
        DataTable dtCluster = objMain.LoadData(strQry);
        ddlVillage.DataSource = dtCluster;
        ddlVillage.DataTextField = "ClusterName";
        ddlVillage.DataValueField = "ClusterCode";
        ddlVillage.DataBind();

    }
    public void FillFC()
    {
        string conditions = "ActiveStatus =1 And UserLevel=24 ";
        string ddlBlockStr = ""; string ddlVillageStr = "";
        foreach (System.Web.UI.WebControls.ListItem item in ddlBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlockStr += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlockStr.Length > 0)
        {
            ddlBlockStr = ddlBlockStr.Substring(0, ddlBlockStr.LastIndexOf(","));
        }
        if (ddlBlockStr.Length > 0)
        {
            conditions = conditions + " and BlockCode in (" + ddlBlockStr + ")";
        }


        foreach (System.Web.UI.WebControls.ListItem item in ddlVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillageStr += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlVillageStr.Length > 0)
        {
            ddlVillageStr = ddlVillageStr.Substring(0, ddlVillageStr.LastIndexOf(","));
        }
        if (ddlVillageStr.Length > 0)
        {
            conditions = conditions + " and VillageCode in (" + ddlVillageStr + ") ";
        }

        objComman.BindDLL("mstuser", " UserName as UserID,UserName +' ('+ FristName +')' as UserName ", conditions, "UserName", "asc", ddlFc, "UserName", "UserID", "Select");

    }
    public void LoadData()
    {
        string strQry = "";
        string conditions = "";
        conditions = " v.StateCode='" + ddlState.SelectedValue.ToString() + "'";

        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions = conditions + " and v.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "' ";
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select District ')</script>", false);
            return;
        }
        string ddlBlockStr = "";
        foreach (System.Web.UI.WebControls.ListItem item in ddlBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlockStr += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlockStr.Length > 0)
        {
            ddlBlockStr = ddlBlockStr.Substring(0, ddlBlockStr.LastIndexOf(","));
        }
        if (ddlBlockStr.Length > 0)
        {
            conditions = conditions + " and v.BlockCode in (" + ddlBlockStr + ")";
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Block ')</script>", false);
            return;
        }
        string ddlVillageStr = "";
        foreach (System.Web.UI.WebControls.ListItem item in ddlVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillageStr += "'" + item.Value + "'" + ",";


            }
        }
        string ddlVillageStrOther = "";
        foreach (System.Web.UI.WebControls.ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillageStrOther += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillageStr.Length > 0)
        {
            ddlVillageStr = ddlVillageStr.Substring(0, ddlVillageStr.LastIndexOf(","));
        }
        if (ddlVillageStrOther.Length > 0)
        {
            ddlVillageStrOther = ddlVillageStrOther.Substring(0, ddlVillageStrOther.LastIndexOf(","));
        }
        if (ddlVillageStr.Length > 0)
        {
            conditions = conditions + " and v.ClusterCode in (" + ddlVillageStr + ") ";
        }

        if (ddlFc.SelectedIndex > 0)
        {
            conditions = conditions + " and a.CreateBy='" + ddlFc.SelectedValue.ToString() + "' ";
        }
        if (ddlVillageStrOther.Length > 0)
        {
            conditions = conditions + " and v.villagecode in (" + ddlVillageStrOther + ") ";
        }
        SqlParameter[] parm1 = new SqlParameter[]
            {

               new SqlParameter("@Con",  conditions)

            };


        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadFillSystem]", parm1);
        if (dt.Rows.Count > 0)
        {
            GVSealSign.DataSource = dt;
            GVSealSign.DataBind();
            Session["GridViewData"] = dt;
        }
        else
        {
            GVSealSign.DataSource = null;
            GVSealSign.DataBind();
        }
    }
    #endregion
    #region  ************ OnSelectedIndexChanged Event
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Locking();
        //LockingEdit();
        FillCBBock();
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillage();
        //Locking();
        //LockingEdit();
    }
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillagNew();
        FillFC();
    }
    public void FillCVillagNew()
    {
        string conditions = "";

        string ddlPhan = "";



        foreach (System.Web.UI.WebControls.ListItem item in ddlVillage.Items)
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


        conditions = "DistrictCode in('" + ddlDistrict.SelectedValue + "')  and BlockCode in('" + ddlBlock.SelectedValue + "') and  ClusterCode in(" + ddlPhan + ")";

        //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        //objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--All-");

        string strQry = "  SELECT VillageCode, dbo.TitleCase(upper(VillageName))  as VillageName FROM mst5Village where " + conditions + "  order by VillageName   ";
        DataTable dtDistrict = objMain.LoadData(strQry);

        chkVillage.DataSource = dtDistrict;
        chkVillage.DataTextField = "VillageName";
        chkVillage.DataValueField = "VillageCode";
        chkVillage.DataBind();


    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        AlllStateCode();
        if (ddlYear.SelectedIndex > 0)
        {
            ddlState.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlDistrict, null);
            if (Session["user_level_Role"].ToString() == "3" || Session["user_level_Role"].ToString() == "4")
            {
                ddlDistrict.SelectedIndex = 1;
                ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
            }

            ddlVillage.Items.Clear();
        }
        else
        {
            ddlState.SelectedIndex = 0;
            ddlDistrict.Items.Clear();
            ddlBlock.Items.Clear();
            ddlVillage.Items.Clear();
        }
        //Locking();
        //LockingEdit();
    }

    #endregion
    #region ***************** Generate PDF *************
    public void GenratePDF_pdfTable(string SchoolCode)
    {
        string FC = ddlFc.SelectedItem.Text;
        string path = Server.MapPath("Travel vouchers");
        string conditions = "";
        conditions = " v.StateCode='" + ddlState.SelectedValue.ToString() + "'";

        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions = conditions + " and v.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "' ";
        }

        if (ddlBlock.SelectedIndex > 0)
        {
            conditions = conditions + " and v.BlockCode='" + ddlBlock.SelectedValue.ToString() + "' ";
        }
        if (ddlVillage.SelectedIndex > 1)
        {
            conditions = conditions + " and v.ClusterCode='" + ddlVillage.SelectedValue.ToString() + "' ";
        }
        if (ddlFc.SelectedIndex > 0)
        {
            conditions = conditions + " and tblEnrolment.CreateBy='" + ddlFc.SelectedValue.ToString() + "' ";
        }
        if (ddlFc.SelectedIndex > 0)
        {
            conditions = conditions + " and tblEnrolment.SchoolCode='" + SchoolCode + "' ";
        }
        SqlParameter[] parm1 = new SqlParameter[]
            {

               new SqlParameter("@Con",  conditions),
                 new SqlParameter("@Flag",  3),
            };


        DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[SP_GET_Seal_Sign_New_08_06]", parm1);
        if (ds != null && ds.Tables.Count > 0)
        {
            foreach (DataTable dtable in ds.Tables)
            {
                if (dtable.Rows.Count == 0)
                    continue;

                foreach (DataRow row in dtable.Rows)
                {
                    string encryptionKey = dtable.Columns.Contains("UniqueChildCode")
                        ? row["UniqueChildCode"].ToString()
                        : string.Empty;

                    if (dtable.Columns.Contains("ChildName") && row["ChildName"] != DBNull.Value)
                    {
                        row["ChildName"] = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["ChildName"].ToString(),
                            "ChildName");
                    }

                    if (dtable.Columns.Contains("FathersName") && row["FathersName"] != DBNull.Value)
                    {
                        row["FathersName"] = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["FathersName"].ToString(),
                            "FathersName");
                    }
                    if (dtable.Columns.Contains("SamgraID") && row["SamgraID"] != DBNull.Value)
                    {
                        row["SamgraID"] = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["SamgraID"].ToString(),
                            "SamgraID");
                    }

                    if (dtable.Columns.Contains("DOB") && row["DOB"] != DBNull.Value)
                    {
                        string decryptedDOB = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["DOB"].ToString(),
                            "DOB",
                            false);

                        row["DOB"] = decryptedDOB;

                        sqlInjection.ProcessRowAgeAndDob(row, decryptedDOB);
                    }
                }
            }
        }
        string filename = path + "SealSign" + "_" + FC.Substring(0, 7) + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".pdf";
        //string filename = "SealSign" + "_" + FC.Substring(0, 7) + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".pdf";
        BaseFont bf = BaseFont.CreateFont(Environment.GetEnvironmentVariable("windir") + @"\fonts\Kruti Dev 010 Regular.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        //BaseFont bf = BaseFont.CreateFont(Server.MapPath("fonts") + "\\Kruti Dev 010 Regular.ttf", BaseFont.IDENTITY_H, true);
        Document document = new Document(PageSize.A3, 10f, 10f, 10f, 10f);
        try
        {
            PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));
            FontFactory.Register(Server.MapPath("fonts") + "\\Kruti Dev 010 Regular.ttf", "arial unicode ms");

            PdfPTable table = new PdfPTable(3);
            iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
            table.AddCell(new Paragraph(Convert.ToString(ds.Tables[1].Rows[0]["1"]), font));
            table.AddCell(" ");
            table.AddCell(new Paragraph(Convert.ToString(ds.Tables[1].Rows[0]["2"]), font));
            document.Open();
            document.Add(table);

            document.Close();
            ShowPdf(filename);

        }
        catch (Exception)
        {

            throw;

        }
        finally
        {



        }

    }
    public void ShowPdf(string filename)
    {

        //Clears all content output from Buffer Stream

        Response.ClearContent();

        //Clears all headers from Buffer Stream

        Response.ClearHeaders();

        //Adds an HTTP header to the output stream

        Response.AddHeader("Content-Disposition", "inline;filename=" + filename);

        //Gets or Sets the HTTP MIME type of the output stream

        Response.ContentType = "application/pdf";

        //Writes the content of the specified file directory to an HTTP response output stream as a file block

        Response.WriteFile(filename);

        //sends all currently buffered output to the client

        Response.Flush();

        //Clears all content output from Buffer Stream

        Response.Clear();

    }


    private void GenratePDF(string SchoolCode)
    {

        string imageURLLogo = Server.MapPath(".") + "/images/logo-new.png";
        StringBuilder sb = new StringBuilder();
        string conditions = "";
        conditions = " v.StateCode='" + ddlState.SelectedValue.ToString() + "'";

        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions = conditions + " and v.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "' ";
        }

        if (ddlBlock.SelectedIndex > 0)
        {
            conditions = conditions + " and v.BlockCode='" + ddlBlock.SelectedValue.ToString() + "' ";
        }
        if (ddlVillage.SelectedIndex > 1)
        {
            conditions = conditions + " and v.ClusterCode='" + ddlVillage.SelectedValue.ToString() + "' ";
        }
        if (ddlFc.SelectedIndex > 0)
        {
            conditions = conditions + " and tblEnrolment.CreateBy='" + ddlFc.SelectedValue.ToString() + "' ";
        }
        if (ddlFc.SelectedIndex > 0)
        {
            conditions = conditions + " and tblEnrolment.SchoolCode='" + SchoolCode + "' ";
        }
        SqlParameter[] parm1 = new SqlParameter[]
            {

               new SqlParameter("@Con",  conditions),
                 new SqlParameter("@Flag",  3),
            };


        DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[SP_GET_Seal_Sign_New_08_06]", parm1);
        if (ds != null && ds.Tables.Count > 0)
        {
            foreach (DataTable dtable in ds.Tables)
            {
                if (dtable.Rows.Count == 0)
                    continue;

                foreach (DataRow row in dtable.Rows)
                {
                    string encryptionKey = dtable.Columns.Contains("UniqueChildCode")
                        ? row["UniqueChildCode"].ToString()
                        : string.Empty;

                    if (dtable.Columns.Contains("ChildName") && row["ChildName"] != DBNull.Value)
                    {
                        row["ChildName"] = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["ChildName"].ToString(),
                            "ChildName");
                    }

                    if (dtable.Columns.Contains("FathersName") && row["FathersName"] != DBNull.Value)
                    {
                        row["FathersName"] = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["FathersName"].ToString(),
                            "FathersName");
                    }
                    if (dtable.Columns.Contains("SamgraID") && row["SamgraID"] != DBNull.Value)
                    {
                        row["SamgraID"] = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["SamgraID"].ToString(),
                            "SamgraID");
                    }

                    if (dtable.Columns.Contains("DOB") && row["DOB"] != DBNull.Value)
                    {
                        string decryptedDOB = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["DOB"].ToString(),
                            "DOB",
                            false);

                        row["DOB"] = decryptedDOB;

                        sqlInjection.ProcessRowAgeAndDob(row, decryptedDOB);
                    }
                }
            }
        }
        DataTable dt = ds.Tables[0];
        DataTable dt1 = ds.Tables[1];
        DataRow[] d = dt.Select("Class=1");
        DataRow[] dr = dt.Select("Class>=2 and Class<=8");
        DataRow[] dr1 = dt.Select("(len(Class)>2 or Class>8)");
        int class1 = d.Length;
        int clss2T8 = dr.Length;
        int classOther = 0;
        sb.Append("<table width='80%'>");
        sb.Append("<tr>");
        sb.Append("<td>Sr. NO: Auto-generated <img  width='50%' height='50%' src='" + imageURLLogo + "' alt='Bird' /> </td>");
        sb.Append("<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>");
        sb.Append("<td>Date:Auto-generated</i></span></td>");
        sb.Append("</tr>");
        sb.Append("<tr><td colspan='3' style='text-align: center; font-family:mangal;'><b> " + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["1"])) + " (" + Convert.ToString(ddlYear.SelectedItem.Text) + ")</b></td></tr>");
        sb.Append("<tr><td colspan='3' style='text-align: left'><b>Field coordinator:</b>" + dt.Rows[0]["UserName"] + "</td></tr>");
        sb.Append("<tr><td colspan='3' style='text-align: left'><b>District:</b>" + dt.Rows[0]["DistrictName"] + ",&nbsp;&nbsp;<b>Block:</b>" + dt.Rows[0]["BlockName"] + ",&nbsp;&nbsp;<b>Cluster:</b>" + dt.Rows[0]["ClusterName"] + ",&nbsp;&nbsp;<b>Panchayat:</b>" + dt.Rows[0]["PanchayatName"] + ",&nbsp;&nbsp;<b>Village:</b>" + dt.Rows[0]["VillageName"] + ",&nbsp;&nbsp;<b>School:</b>" + dt.Rows[0]["School"] + " </td></tr>");
        sb.Append("<tr><td colspan='3' style='text-align: left'><b>Dies Code:</b>" + dt.Rows[0]["DISECode"] + "&nbsp;&nbsp;<b>Division Code:</b>" + dt.Rows[0]["Division"] + " </td></tr>");
        sb.Append("<tr>");
        sb.Append("<td  colspan='3' >");
        sb.Append("<table class='style1' border='1'>");
        sb.Append("<tr>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("Sr. No</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("SR No</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("Child Name</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("DOB</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("Social Category</th> ");
        sb.Append("<th style='font-weight:bold'> ");
        sb.Append("Father Name</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("DOE</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("Class</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("ID</th>");
        sb.Append("</tr>");
        try
        {
            dt.Columns.Remove("DistrictName");
            dt.Columns.Remove("BlockName");
            dt.Columns.Remove("ClusterName");
            dt.Columns.Remove("PanchayatName");
            dt.Columns.Remove("VillageName");
            dt.Columns.Remove("School");
            dt.Columns.Remove("DISECode");
            dt.Columns.Remove("Division");
            dt.AcceptChanges();
        }
        catch (Exception)
        {

            throw;
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sb.Append("<tr>");
            for (int j = 0; j < dt.Columns.Count; j++)
            {

                sb.Append("<td>" + dt.Rows[i][j] + " </td>");

            }
            sb.Append("</tr>");
        }

        sb.Append("</table>");
        sb.Append("</td></tr>");
        classOther = dt.Rows.Count - (class1 + clss2T8);
        sb.Append("<tr><td><b>Total Students:</>" + dt.Rows.Count + "</td><td><b>Class 1:</b> " + class1 + ",<b>Class 2-8:</b>" + clss2T8 + " </td><td><b>Other Class:</b>" + classOther + "</td></tr>");
        sb.Append("<tr><td style='font-weight:bold'>Signature S C</td><td style='font-weight:bold'>Signature S C</td><td style='font-weight:bold'>Signature Block officer</td></tr>");
        sb.Append("</table>");
        StringReader sr = new StringReader(sb.ToString());

        Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 20f, 10f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        string FC = ddlFc.SelectedItem.Text;
        string filename = "SealSign" + "_" + FC.Substring(0, 7) + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".pdf";
        using (MemoryStream memoryStream = new MemoryStream())
        {
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
            pdfDoc.Open();
            pdfDoc.NewPage();
            htmlparser.Parse(sr);
            pdfDoc.Close();

            byte[] bytes = memoryStream.ToArray();

            memoryStream.Close();

            File.WriteAllBytes(Request.PhysicalApplicationPath + "/Travel vouchers/" + filename, bytes);
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            Response.ContentType = "application/ms-excel";
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            string dsssssssssssss1 = Server.MapPath(Comman.GetImagePath("TravelvouchersPath") + "//" + filename);
            Response.ContentEncoding = Encoding.UTF8;
            Response.Charset = "";

            // byte[] data = req.DownloadData(dsssssssssssss1);
            //Response.BinaryWrite(data);
            Response.BinaryWrite(bytes);
        }



        //  string dsssssssssssss = Request.PhysicalApplicationPath + "Travel vouchers\\TravelVoucher_" + ddlMonth.SelectedItem.Text + "_ " + ddlFc.SelectedItem.Text + ".pdf";
        WebClient req = new WebClient();
        //HttpResponse response = HttpContext.Current.Response;
        //response.Clear();
        //response.ClearContent();
        //response.ClearHeaders();
        //Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);      
        //Response.ContentType = "application/octet-stream";
        //Response.Buffer = true;

        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Response.Charset = "utf-8";
        //Response.ContentEncoding = Encoding.UTF7;
        //string dsssssssssssss1 = Server.MapPath(Comman.GetImagePath("TravelvouchersPath")\\" + filename;
        //// byte[] data = req.DownloadData(dsssssssssssss1);
        ////Response.BinaryWrite(data);
        //Response.BinaryWrite(dsssssssssssss1);
        // Response.End();

    }


    #endregion
    #region ***************** Excel to pdf ************
    private void GenrateExcel_toPDf(string SchoolCode)
    {

        string imageURLLogo = Server.MapPath(".") + "/images/logo-new.png";
        StringBuilder sb = new StringBuilder();
        string conditions = "";
        conditions = " v.StateCode='" + ddlState.SelectedValue.ToString() + "'";

        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions = conditions + " and v.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "' ";
        }

        if (ddlBlock.SelectedIndex > 0)
        {
            conditions = conditions + " and v.BlockCode='" + ddlBlock.SelectedValue.ToString() + "' ";
        }
        if (ddlVillage.SelectedIndex > 1)
        {
            conditions = conditions + " and v.ClusterCode='" + ddlVillage.SelectedValue.ToString() + "' ";
        }
        if (ddlFc.SelectedIndex > 0)
        {
            conditions = conditions + " and tblEnrolment.CreateBy='" + ddlFc.SelectedValue.ToString() + "' ";
        }
        if (ddlFc.SelectedIndex > 0)
        {
            conditions = conditions + " and tblEnrolment.SchoolCode='" + SchoolCode + "' ";
        }
        SqlParameter[] parm1 = new SqlParameter[]
            {

               new SqlParameter("@Con",  conditions),
                 new SqlParameter("@Flag",  3),
            };
        DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[SP_GET_Seal_Sign_New_08_06]", parm1);
        if (ds != null && ds.Tables.Count > 0)
        {
            foreach (DataTable dtable in ds.Tables)
            {
                if (dtable.Rows.Count == 0)
                    continue;

                foreach (DataRow row in dtable.Rows)
                {
                    string encryptionKey = dtable.Columns.Contains("UniqueChildCode")
                        ? row["UniqueChildCode"].ToString()
                        : string.Empty;

                    if (dtable.Columns.Contains("ChildName") && row["ChildName"] != DBNull.Value)
                    {
                        row["ChildName"] = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["ChildName"].ToString(),
                            "ChildName");
                    }

                    if (dtable.Columns.Contains("FathersName") && row["FathersName"] != DBNull.Value)
                    {
                        row["FathersName"] = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["FathersName"].ToString(),
                            "FathersName");
                    }
                    if (dtable.Columns.Contains("SamgraID") && row["SamgraID"] != DBNull.Value)
                    {
                        row["SamgraID"] = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["SamgraID"].ToString(),
                            "SamgraID");
                    }

                    if (dtable.Columns.Contains("DOB") && row["DOB"] != DBNull.Value)
                    {
                        string decryptedDOB = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["DOB"].ToString(),
                            "DOB",
                            false);

                        row["DOB"] = decryptedDOB;

                        sqlInjection.ProcessRowAgeAndDob(row, decryptedDOB);
                    }
                }
            }
        }
        DataTable dt = ds.Tables[0];
        DataTable dt1 = ds.Tables[1];
        DataRow[] d = dt.Select("Class=1");
        DataRow[] dr = dt.Select("Class>=2 and Class<=8");
        DataRow[] dr1 = dt.Select("(len(Class)>2 or Class>8)");
        int class1 = d.Length;
        int clss2T8 = dr.Length;
        int classOther = 0;
        sb.Append("<table width='80%'>");
        sb.Append("<tr>");
        sb.Append("<td>Sr. NO: Auto-generated <img  width='5%' height='5%' src='" + imageURLLogo + "' alt='Bird' /> </td>");
        sb.Append("<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>");
        sb.Append("<td>Date:Auto-generated</i></span></td>");
        sb.Append("</tr>");
        sb.Append("<tr border='1'><td colspan='3' style='text-align: center; font-family:mangal;'><b> " + Convert.ToString(dt1.Rows[0]["1"]) + " (" + Convert.ToString(ddlYear.SelectedItem.Text) + ")</b></td></tr>");
        sb.Append("<tr><td colspan='3' style='text-align: left'><b>FC Name:</b>" + dt.Rows[0]["UserName"] + "</td></tr>");
        sb.Append("<tr><td colspan='3' style='text-align: left'><b>" + Convert.ToString(dt1.Rows[0]["2"]) + ":</b>" + dt.Rows[0]["DistrictName"] + ",&nbsp;&nbsp;<b>Block:</b>" + dt.Rows[0]["BlockName"] + ",&nbsp;&nbsp;<b>Cluster:</b>" + dt.Rows[0]["ClusterName"] + ",&nbsp;&nbsp;<b>Panchayat:</b>" + dt.Rows[0]["PanchayatName"] + ",&nbsp;&nbsp;<b>Village:</b>" + dt.Rows[0]["VillageName"] + ",&nbsp;&nbsp;<b>School:</b>" + dt.Rows[0]["School"] + " </td></tr>");
        sb.Append("<tr><td colspan='3' style='text-align: left'><b>Dies Code:</b>" + dt.Rows[0]["DISECode"] + "&nbsp;&nbsp;<b>Division Code:</b>" + dt.Rows[0]["Division"] + " </td></tr>");
        sb.Append("<tr>");
        sb.Append("<td  colspan='3' >");
        sb.Append("<table class='style1' border='1'>");
        sb.Append("<tr>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("Sr. No</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("SR No</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("Child Name</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("DOB</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("Social Category</th> ");
        sb.Append("<th style='font-weight:bold'> ");
        sb.Append("Father Name</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("DOE</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("Class</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("ID</th>");
        sb.Append("</tr>");
        try
        {
            dt.Columns.Remove("DistrictName");
            dt.Columns.Remove("BlockName");
            dt.Columns.Remove("ClusterName");
            dt.Columns.Remove("PanchayatName");
            dt.Columns.Remove("VillageName");
            dt.Columns.Remove("School");
            dt.Columns.Remove("DISECode");
            dt.Columns.Remove("Division");
            dt.AcceptChanges();
        }
        catch (Exception)
        {

            throw;
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sb.Append("<tr>");
            for (int j = 0; j < dt.Columns.Count; j++)
            {

                sb.Append("<td>" + dt.Rows[i][j] + " </td>");

            }
            sb.Append("</tr>");
        }

        sb.Append("</table>");
        sb.Append("</td></tr>");
        classOther = dt.Rows.Count - (class1 + clss2T8);
        sb.Append("<tr><td><b>Total Students:</>" + dt.Rows.Count + "</td><td><b>Class 1:</b> " + class1 + ",<b>Class 2-8:</b>" + clss2T8 + " </td><td><b>Other Class:</b>" + classOther + "</td></tr>");
        sb.Append("<tr><td style='font-weight:bold'>Signature S C</td><td style='font-weight:bold'>Signature S C</td><td style='font-weight:bold'>Signature Block officer</td></tr>");
        sb.Append("</table>");
        ViewState["Filename"] = "";
        string FC = ddlFc.SelectedItem.Text;
        string filename = Server.MapPath(Comman.GetImagePath("TravelvouchersPath") + "\\" + "SealSign" + "_" + FC.Substring(0, 7) + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls");
        StreamWriter sw = new StreamWriter(filename, false);
        sw.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Tr
ansitional//EN"">");
        sw.Write(sb.ToString());
        sw.Close();
        ViewState["Filename"] = filename;


        //HttpContext.Current.Response.Clear();
        //HttpContext.Current.Response.ClearContent();
        //HttpContext.Current.Response.ClearHeaders();
        //HttpContext.Current.Response.Buffer = true;
        //HttpContext.Current.Response.ContentType = "application/ms-excel";
        //HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");

        //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + "");
        //ViewState["Filename"] = filename;
        //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        //HttpContext.Current.Response.Charset = "";
        //HttpContext.Current.Response.Write(sb.ToString()); 
        //HttpContext.Current.Response.Flush();
        //HttpContext.Current.Response.End(); 
    }
    private void ConvertExcelTopdf(string filename)
    {
        //Workbook workbook = new Workbook();
        //workbook.LoadFromFile(@"..\..\Sample.xlsx", ExcelVersion.Version2010);
        //HttpApplication app = new HttpApplication();
        //Workbook wkb = app.Workbooks.Open("d:\\x.xlsx");
        //XlsxToPdfConverter converter = new XlsxToPdfConverter();
        //using (Stream stream = File.OpenRead(filename))
        //{
        //    converter.Load(stream);
        //}
        ////Convert active worksheet to pdf
        //converter.ContentPart = PdfContentPart.FromActiveSheet;
        ////If Print Area is set in the workbook, the output pdf will only show the Print Area
        ////So dislpay whole worksheet need to disable this property
        //converter.DisplayAsPrintArea = false;
        ////Convert Excel to pdf, and save it to file stream
        //using (var stream = File.OpenWrite("convert.pdf"))
        //{
        //    converter.Save(stream);
        //}
    }



    #endregion
    #region
    public static DataSet GetDataSetNew(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParameters)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(connString);
        SqlCommand cmd = new SqlCommand();

        try
        {
            PrepareCommandNew(cmd, conn, cmdType, cmdText, cmdParameters);
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

    public static void PrepareCommandNew(SqlCommand cmd, SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParameters)
    {
        if (conn.State != ConnectionState.Open)
            conn.Open();
        cmd.Connection = conn;

        cmd.CommandType = cmdType;
        cmd.CommandText = cmdText;
        cmd.CommandTimeout = 0;
        if (cmdParameters != null)
        {
            foreach (SqlParameter param in cmdParameters)
            {
                cmd.Parameters.Add(param);
            }
        }
    }

    private string RetStringBuilderNew(string SchoolCode, string DiseCode, string ClusterCode)
    {
        // string imageURLLogo =  "/images/logo-new.png";
        StringBuilder sb = new StringBuilder();
        string conditionsNew = "";
        conditionsNew = " v.StateCode='" + ddlState.SelectedValue.ToString() + "'";

        if (ddlDistrict.SelectedIndex > 0)
        {
            conditionsNew = conditionsNew + " and v.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "' ";
        }

        if (ddlBlock.SelectedIndex > 0)
        {
            conditionsNew = conditionsNew + " and v.BlockCode='" + ddlBlock.SelectedValue.ToString() + "' ";
        }
        if (ddlVillage.SelectedIndex > 1)
        {
            conditionsNew = conditionsNew + " and v.ClusterCode='" + ddlVillage.SelectedValue.ToString() + "' ";
        }
        //if (ddlFc.SelectedIndex > 0)
        //{
        //    conditions = conditions + " and tblEnrolment.CreateBy='" + ddlFc.SelectedValue.ToString() + "' ";
        //}
        if (ddlFc.SelectedIndex > 0)
        {
            conditionsNew = conditionsNew + " and tblEnrolment.SchoolCode='" + SchoolCode + "' ";
        }
        conditionsNew = conditionsNew + " and tblEnrolment.SealSign_DiseCode='" + DiseCode + "'";

        SqlParameter[] parm1 = new SqlParameter[]
            {

               new SqlParameter("@Con",  conditionsNew),
                 new SqlParameter("@Flag",  3),
            };

        string[] a = DiseCode.Split('_');
        DataSet ds = GetDataSetNew(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[SP_GET_Seal_Sign_New_08_06]", parm1);
        if (ds != null && ds.Tables.Count > 0)
        {
            foreach (DataTable dtable in ds.Tables)
            {
                if (dtable.Rows.Count == 0)
                    continue;

                foreach (DataRow row in dtable.Rows)
                {
                    string encryptionKey = dtable.Columns.Contains("UniqueChildCode")
                        ? row["UniqueChildCode"].ToString()
                        : string.Empty;

                    if (dtable.Columns.Contains("ChildName") && row["ChildName"] != DBNull.Value)
                    {
                        row["ChildName"] = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["ChildName"].ToString(),
                            "ChildName");
                    }

                    if (dtable.Columns.Contains("FathersName") && row["FathersName"] != DBNull.Value)
                    {
                        row["FathersName"] = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["FathersName"].ToString(),
                            "FathersName");
                    }
                    if (dtable.Columns.Contains("SamgraID") && row["SamgraID"] != DBNull.Value)
                    {
                        row["SamgraID"] = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["SamgraID"].ToString(),
                            "SamgraID");
                    }

                    if (dtable.Columns.Contains("DOB") && row["DOB"] != DBNull.Value)
                    {
                        string decryptedDOB = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["DOB"].ToString(),
                            "DOB",
                            false);

                        row["DOB"] = decryptedDOB;

                        sqlInjection.ProcessRowAgeAndDob(row, decryptedDOB);
                    }
                }
            }
        }
        DataTable dtFcName = objComman.Select_All_Data("MstUser", "UserName +' ('+ FristName +')' as UserName", " VillageCode='" + ClusterCode + "' and ActiveStatus=1", "", "");
        DataTable dt = ds.Tables[0];
        DataTable dt1 = ds.Tables[1];
        DataRow[] dr777 = dt.Select("DistrictNameNew='" + ddlDistrict.SelectedItem.Text + "' ");
        if (dr777.Length > 0)
        {
        }
        else
        {
            return "";
        }

        DataRow[] d = dt.Select("Class='1'");
        DataRow[] dr = dt.Select("Class in(2,3,4,5,6,7,8)");
        DataRow[] dr1 = dt.Select("(len(Class)>2 or Class>8)");
        int class1 = d.Length;
        int clss2T8 = dr.Length;
        int classOther = 0;
        //DataRow[] d = dt.Select("Class=1");
        //DataRow[] dr = dt.Select("Class>=2 and Class<=8");
        //DataRow[] dr1 = dt.Select("(len(Class)>2 or Class>8)");
        //int class1 = d.Length;
        //int clss2T8 = dr.Length;
        //int classOther = 0;
        sb.Append("<table width='100%'>");
        sb.Append("<tr>");
        // sb.Append("<td>Sr. NO: Auto-generated <img  width='50%' height='50%' src='" + imageURLLogo + "' alt='Bird' /> </td>");
        sb.Append("<td  style='padding-left:35px'>क्रमांक:" + a[2] + " </td>");
        sb.Append("<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>");
        sb.Append("<td>Date:</i></span></td>");
        sb.Append("</tr>");
        string FcName = "";
        if (ddlFc.SelectedIndex > 0)
        {
            FcName = ddlFc.SelectedItem.Text;
        }
        else
        {
            FcName = (dtFcName.Rows.Count > 0 ? Convert.ToString(dtFcName.Rows[0]["UserName"]) : "");
        }
        if (a[1] == "M")
        {
            sb.Append("<tr><td colspan='3' style='text-align: center; font-family:mangal;'><b> नामांकित छात्रों की सूची (" + Convert.ToString(ddlYear.SelectedItem.Text) + ")</b></td></tr>");
        }
        else
        {
            sb.Append("<tr><td colspan='3' style='text-align: center; font-family:mangal;'><b> " + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["1"])) + " (" + Convert.ToString(ddlYear.SelectedItem.Text) + ")</b></td></tr>");
        }
        sb.Append("<tr ><td  colspan='3' style='text-align: left;padding-left:35px'><b>Field coordinator: </b>" + FcName + "</td></tr>");
        sb.Append("<tr><td colspan='3' style='text-align: left;padding-left:35px'></td></tr>");
        sb.Append("<tr><td colspan='3' style='text-align: left;padding-left:35px'><b>" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["2"])) + " :</b>" + dt.Rows[0]["DistrictName"] + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b>" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["3"])) + " :</b>" + dt.Rows[0]["BlockName"] + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["4"])) + " :</b>" + dt.Rows[0]["ClusterName"] + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["5"])) + " :</b>" + dt.Rows[0]["PanchayatName"] + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["6"])) + " :</b>" + dt.Rows[0]["VillageName"] + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["7"])) + " :</b>" + dt.Rows[0]["School"] + " </td></tr>");
        sb.Append("<tr><td colspan='3' style='text-align: left'></td></tr>");

        sb.Append("<tr><td colspan='3' style='text-align: left;padding-left:35px'><b>" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["8"])) + " :</b>" + dt.Rows[0]["DISECode"] + "&nbsp;&nbsp;<b>" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["9"])) + " :</b>" + dt.Rows[0]["Division"] + " </td></tr>");
        sb.Append("<tr><td colspan='3' style='text-align: left'></td></tr>");
        sb.Append("<tr>");
        sb.Append("<td  colspan='3' >");
        sb.Append("<table  style='margin-left:35px' border='1' margin-left='35px' Width='90%'>");
        sb.Append("<tr>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["10"])) + "</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["11"])) + "</th>");
        sb.Append("<th style='font-weight:bold'>");
        if (a[1] == "M")
        {
            sb.Append("छात्र का नाम</th>");
        }
        else
        {
            sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["13"])) + "</th>");
        }
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["14"])) + "</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("आयु</th>");
        sb.Append("<th style='font-weight:bold'> ");
        sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["16"])) + "</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["15"])) + "</th> ");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["17"])) + "</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["18"])) + "</th>");


        sb.Append("<th style='font-weight:bold'> ");
        sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["20"])) + "</th>");



        sb.Append("<th style='font-weight:bold'>");
        sb.Append("Unique ID</th>");
        if (Convert.ToString(Session["StateCode"]) == "23")
        {
            sb.Append("<th style='font-weight:bold'> ");
            sb.Append("" + Server.HtmlDecode("समग्र ID") + "</th>");

        }





        sb.Append("</tr>");
        //sb.Append("<tr>");
        //sb.Append("<th style='font-weight:bold'>");
        //sb.Append("Sr. No</th>");
        //sb.Append("<th style='font-weight:bold'>");
        //sb.Append("SR No</th>");
        //sb.Append("<th style='font-weight:bold'>");
        //sb.Append("Child Name</th>");
        //sb.Append("<th style='font-weight:bold'>");
        //sb.Append("DOB</th>");
        //sb.Append("<th style='font-weight:bold'>");
        //sb.Append("Social Category</th> ");
        //sb.Append("<th style='font-weight:bold'> ");
        //sb.Append("Father Name</th>");
        //sb.Append("<th style='font-weight:bold'>");
        //sb.Append("DOE</th>");
        //sb.Append("<th style='font-weight:bold'>");
        //sb.Append("Class</th>");
        //sb.Append("<th style='font-weight:bold'>");
        //sb.Append("ID</th>");
        //sb.Append("</tr>");
        try
        {
            dt.Columns.Remove("DistrictNameNew");
            dt.Columns.Remove("DistrictName");
            dt.Columns.Remove("BlockName");
            dt.Columns.Remove("ClusterName");
            dt.Columns.Remove("PanchayatName");
            dt.Columns.Remove("VillageName");
            dt.Columns.Remove("School");
            dt.Columns.Remove("DISECode");
            dt.Columns.Remove("Division");

            dt.Columns.Remove("EnrolmentDate");
            if (Convert.ToString(Session["StateCode"]) != "23")
            {

                dt.Columns.Remove("SamgraID");
            }
            dt.AcceptChanges();
        }
        catch (Exception)
        {

            throw;
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sb.Append("<tr>");
            for (int j = 0; j < dt.Columns.Count; j++)
            {

                sb.Append("<td style='text-align:center;'>" + dt.Rows[i][j] + " </td>");

            }
            sb.Append("</tr>");
        }

        sb.Append("</table>");
        sb.Append("</td></tr>");
        classOther = dt.Rows.Count - (class1 + clss2T8);
        sb.Append("<tr><td colspan='3' style='text-align: left'></td></tr>");
        if (a[1] == "M")
        {
            sb.Append("<tr><td  style='padding-left:35px'><b>प्रपत्र में कुल छात्रों की संख्या:</>" + dt.Rows.Count + "</td><td><b>कक्षा 1:</b> " + class1 + ",<b>कक्षा 2 से 8:</b>" + clss2T8 + " </td><td><b> अन्य कक्षा:</b>" + classOther + "</td></tr>");
        }
        else
        {
            sb.Append("<tr><td style='padding-left:35px'><b>प्रपत्र में कुल छात्राओं की संख्या:</>" + dt.Rows.Count + "</td><td><b>कक्षा 1:</b> " + class1 + ",<b>कक्षा 2 से 8:</b>" + clss2T8 + " </td><td><b> अन्य कक्षा:</b>" + classOther + "</td></tr>");
        }

        sb.Append("<tr><td style='font-weight:bold'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ह. एफ सी</td><td style='font-weight:bold'>ह. संस्था प्रधान मयसील</td><td style='font-weight:bold'>ह. ब्लॉक ऑफिसर</td></tr>");
        sb.Append("</table>");
        return sb.ToString();
    }
    private string RetStringBuilder(string SchoolCode)
    {
        // string imageURLLogo =  "/images/logo-new.png";
        StringBuilder sb = new StringBuilder();
        string conditions = "";
        conditions = " v.StateCode='" + ddlState.SelectedValue.ToString() + "'";

        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions = conditions + " and v.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "' ";
        }

        if (ddlBlock.SelectedIndex > 0)
        {
            conditions = conditions + " and v.BlockCode='" + ddlBlock.SelectedValue.ToString() + "' ";
        }
        if (ddlVillage.SelectedIndex > 1)
        {
            conditions = conditions + " and v.ClusterCode='" + ddlVillage.SelectedValue.ToString() + "' ";
        }
        if (ddlFc.SelectedIndex > 0)
        {
            conditions = conditions + " and tblEnrolment.CreateBy='" + ddlFc.SelectedValue.ToString() + "' ";
        }
        if (ddlFc.SelectedIndex > 0)
        {
            conditions = conditions + " and tblEnrolment.SchoolCode='" + SchoolCode + "' ";
        }
        SqlParameter[] parm1 = new SqlParameter[]
            {

               new SqlParameter("@Con",  conditions),
                 new SqlParameter("@Flag",  3),
            };


        DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[SP_GET_Seal_Sign_New_08_06]", parm1);
        if (ds != null && ds.Tables.Count > 0)
        {
            foreach (DataTable dtable in ds.Tables)
            {
                if (dtable.Rows.Count == 0)
                    continue;

                foreach (DataRow row in dtable.Rows)
                {
                    string encryptionKey = dtable.Columns.Contains("UniqueChildCode")
                        ? row["UniqueChildCode"].ToString()
                        : string.Empty;

                    if (dtable.Columns.Contains("ChildName") && row["ChildName"] != DBNull.Value)
                    {
                        row["ChildName"] = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["ChildName"].ToString(),
                            "ChildName");
                    }

                    if (dtable.Columns.Contains("FathersName") && row["FathersName"] != DBNull.Value)
                    {
                        row["FathersName"] = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["FathersName"].ToString(),
                            "FathersName");
                    }
                    if (dtable.Columns.Contains("SamgraID") && row["SamgraID"] != DBNull.Value)
                    {
                        row["SamgraID"] = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["SamgraID"].ToString(),
                            "SamgraID");
                    }

                    if (dtable.Columns.Contains("DOB") && row["DOB"] != DBNull.Value)
                    {
                        string decryptedDOB = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["DOB"].ToString(),
                            "DOB",
                            false);

                        row["DOB"] = decryptedDOB;

                        sqlInjection.ProcessRowAgeAndDob(row, decryptedDOB);
                    }
                }
            }
        }
        DataTable dt = ds.Tables[0];
        DataTable dt1 = ds.Tables[1];
        DataRow[] d = dt.Select("Class=1");
        DataRow[] dr = dt.Select("Class>=2 and Class<=8");
        DataRow[] dr1 = dt.Select("(len(Class)>2 or Class>8)");
        int class1 = d.Length;
        int clss2T8 = dr.Length;
        int classOther = 0;
        sb.Append("<table width='80%'>");
        sb.Append("<tr>");
        // sb.Append("<td>Sr. NO: Auto-generated <img  width='50%' height='50%' src='" + imageURLLogo + "' alt='Bird' /> </td>");
        sb.Append("<td>क्रमांक: 1 </td>");
        sb.Append("<td>&nbsp;&nbsp;&nbsp;&nbsp;</td>");
        sb.Append("<td>Date:" + (DateTime.Now).ToString("yyyy-MM-dd") + "</i></span></td>");
        sb.Append("</tr>");
        sb.Append("<tr><td colspan='3' style='text-align: center; font-family:mangal;'><b> " + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["1"])) + " (" + Convert.ToString(ddlYear.SelectedItem.Text) + ")</b></td></tr>");
        sb.Append("<tr><td colspan='3' style='text-align: left'><b>Field coordinator:</b>" + dt.Rows[0]["UserName"] + "</td></tr>");
        sb.Append("<tr><td colspan='3' style='text-align: left'><b>" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["2"])) + ":</b>" + dt.Rows[0]["DistrictName"] + ",&nbsp;&nbsp;<b>" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["3"])) + ":</b>" + dt.Rows[0]["BlockName"] + ",&nbsp;&nbsp;<b>" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["4"])) + ":</b>" + dt.Rows[0]["ClusterName"] + ",&nbsp;&nbsp;<b>" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["5"])) + ":</b>" + dt.Rows[0]["PanchayatName"] + ",&nbsp;&nbsp;<b>" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["6"])) + ":</b>" + dt.Rows[0]["VillageName"] + ",&nbsp;&nbsp;<b>" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["7"])) + ":</b>" + dt.Rows[0]["School"] + " </td></tr>");
        sb.Append("<tr><td colspan='3' style='text-align: left'><b>" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["8"])) + ":</b>" + dt.Rows[0]["DISECode"] + "&nbsp;&nbsp;<b>" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["9"])) + ":</b>" + dt.Rows[0]["Division"] + " </td></tr>");
        sb.Append("<tr>");
        sb.Append("<td  colspan='3' >");
        sb.Append("<table class='style1' border='0'>");
        sb.Append("<tr>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["10"])) + "</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["11"])) + "</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["13"])) + "</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["14"])) + "</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["15"])) + "</th> ");
        sb.Append("<th style='font-weight:bold'> ");
        sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["16"])) + "</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["17"])) + "</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["18"])) + "</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("ID</th>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("Sr. No</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("SR No</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("Child Name</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("DOB</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("Social Category</th> ");
        sb.Append("<th style='font-weight:bold'> ");
        sb.Append("Father Name</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("DOE</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("Class</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("ID</th>");
        sb.Append("</tr>");
        try
        {
            dt.Columns.Remove("DistrictName");
            dt.Columns.Remove("BlockName");
            dt.Columns.Remove("ClusterName");
            dt.Columns.Remove("PanchayatName");
            dt.Columns.Remove("VillageName");
            dt.Columns.Remove("School");
            dt.Columns.Remove("DISECode");
            dt.Columns.Remove("Division");
            dt.AcceptChanges();
        }
        catch (Exception)
        {

            throw;
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sb.Append("<tr>");
            for (int j = 0; j < dt.Columns.Count; j++)
            {

                sb.Append("<td>" + dt.Rows[i][j] + " </td>");

            }
            sb.Append("</tr>");
        }

        sb.Append("</table>");
        sb.Append("</td></tr>");
        classOther = dt.Rows.Count - (class1 + clss2T8);
        sb.Append("<tr><td><b>प्रपत्र में कुल छात्राओं की संख्य:</>" + dt.Rows.Count + "</td><td><b>कक्षा 1:</b> " + class1 + ",<b>कक्षा 2 से 8:</b>" + clss2T8 + " </td><td><b> अन्य कक्षा:</b>" + classOther + "</td></tr>");
        sb.Append("<tr><td style='font-weight:bold'>ह. एफ सी</td><td style='font-weight:bold'>हस्ताक्षर-प्रधानाध्यापक/प्रभारी प्रधानाध्यापक</td><td style='font-weight:bold'>ह. ब्लॉक ऑफिसर</td></tr>");
        sb.Append("</table>");
        return sb.ToString();
    }
    protected void GVSealSign_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DropDownList ddlRe = (DropDownList)e.Row.FindControl("ddlRe");
            Label lblSchoolCode = (Label)e.Row.FindControl("lblSchoolCode");
            Label lblSealSign = (Label)e.Row.FindControl("lblSealSign");
            Label lblAprrove = (Label)e.Row.FindControl("lblAprrove");

            //LinkButton lnkGenerate = (LinkButton)e.Row.FindControl("lnkGenerate");
            //LinkButton lblCategory = (LinkButton)e.Row.FindControl("lblCategory");
            LinkButton lnkbtnsend = (LinkButton)e.Row.FindControl("lnkbtnsend");
            LinkButton lnkbtnRecieve = (LinkButton)e.Row.FindControl("lnkbtnRecieve");

            string sendval = Convert.ToString(GVSealSign.DataKeys[e.Row.RowIndex]["SendFile"]);
            string recieveval = Convert.ToString(GVSealSign.DataKeys[e.Row.RowIndex]["ReceiveFile"]);



            Int32 Icount = 0;


            if (Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "136") // Role DEO
            {
                if (sendval == "" || sendval == "0")
                {
                    lnkbtnsend.Text = "Submit to BO";
                    //lnkbtnsend.Enabled = true;

                }
                else
                {
                    lnkbtnsend.Visible = false;
                }

                if (recieveval == "2")
                {
                    lnkbtnRecieve.Text = "Received from BO";
                    lnkbtnRecieve.Visible = true;
                }
                else if (recieveval == "3" && lblAprrove.Text == "1")
                {
                    lnkbtnRecieve.Text = "Filing Completed";
                    lnkbtnRecieve.Visible = true;
                }

                else
                {
                    lnkbtnRecieve.Visible = false;
                }

            }
            if (Session["user_level"].ToString() == "19" || Session["user_level"].ToString() == "137") // Role DEO
            {
                if (sendval == "1")
                {
                    lnkbtnsend.Visible = true;
                    lnkbtnsend.Text = "Submit to FC";
                }
                else if (sendval == "1")
                {
                    lnkbtnsend.Visible = false;
                }
                else
                {
                    lnkbtnsend.Visible = false;
                }
                if (recieveval == "1" && lblSealSign.Text == "1")
                {
                    lnkbtnRecieve.Visible = true;
                    lnkbtnRecieve.Text = "Received from FC";
                }
                else
                {
                    lnkbtnRecieve.Visible = false;
                }
            }

        }
    }
    protected void btnMain1_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            GridViewRow row = (GridViewRow)lnk.NamingContainer;
            int indx = row.RowIndex;
            Label lblSchoolCode = (Label)GVSealSign.Rows[indx].FindControl("lblSchoolCode");
            Label lblClusterCode = (Label)GVSealSign.Rows[indx].FindControl("lblClusterCode");
            //  DropDownList ddlRe = (DropDownList)GVSealSign.Rows[indx].FindControl("ddlRe");
            LinkButton lnkPdf1 = (LinkButton)GVSealSign.Rows[indx].FindControl("lnkPdf1");
            //GenratePDF(lblSchoolCode.Text);
            // GenratePDF_pdfTable(lblSchoolCode.Text);

            // GenrateExcel_toPDf(lblSchoolCode.Text);
            //ConvertExcelTopdf(Convert.ToString(ViewState["Filename"]));

            string st = RetStringBuilderNew(lblSchoolCode.Text, lnkPdf1.Text, lblClusterCode.Text);
            PrintCards(st);


        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void PrintCards(string RetStringBuilder)
    {

        string a = HttpContext.Current.Server.MapPath(Comman.GetImagePath("MouPath") + "/" + "Testhtml.htm");
        string FIleName = ddlDistrict.SelectedItem.Text + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + "Testhtml" + ".htm";
        string b = HttpContext.Current.Server.MapPath(Comman.GetImagePath("MouPath") + "/" + FIleName + "");


        File.Copy(a, b, true);
        StreamReader s = File.OpenText(b.ToString());
        string strFinalHtml = "";
        string read = null;
        while ((read = s.ReadLine()) != null)
        {
            strFinalHtml += read;
        }
        s.Close();
        strFinalHtml = strFinalHtml.Replace("{MainContent}", RetStringBuilder);
        STRPRINTCONTENT2 = strFinalHtml;
        Page.ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:PrintPanel2(); ", true);


    }
    protected void GVSealSign_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GVSealSign.PageIndex = e.NewPageIndex;
        if (Session["GridViewData"] != null)
        {
            DataTable dt = Session["GridViewData"] as DataTable;
            GVSealSign.DataSource = dt;
            GVSealSign.DataBind();
        }


    }
    #endregion


    protected void GVSealSign_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            int rowIndex = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "LnkSend")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int RemoveAt = gvr.RowIndex;
                rowIndex = RemoveAt;
                if (Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "136") // Role DEO
                {
                    string SchoolCode = this.GVSealSign.DataKeys[rowIndex]["SchoolCode"].ToString();
                    LinkButton lnkbtnsend = (LinkButton)GVSealSign.Rows[rowIndex].FindControl("lnkbtnsend");
                    LinkButton lnkbtnRecieve = (LinkButton)GVSealSign.Rows[rowIndex].FindControl("lnkbtnRecieve");
                    string SendFile = Convert.ToString(this.GVSealSign.DataKeys[rowIndex]["SendFile"]);
                    string ReceiveFile = Convert.ToString(this.GVSealSign.DataKeys[rowIndex]["ReceiveFile"]);
                    string SealSign_DiseCode = Convert.ToString(this.GVSealSign.DataKeys[rowIndex]["SealSign_DiseCode"]);


                    hdnsend.Value = "1";
                    hdnrecieve.Value = "0";
                    string SendFiles = hdnsend.Value;
                    string RecieveFiles = hdnrecieve.Value;
                    string RoleDEO = "30";
                    int Result = UpdateFilingSystemSend(SchoolCode, SendFiles, RecieveFiles, RoleDEO, SealSign_DiseCode);

                    if (Result > 0)
                    {
                        lnkbtnsend.Visible = false;
                        lnkbtnRecieve.Visible = false;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                        LoadData();
                    }
                    else
                    {
                        lnkbtnsend.Visible = true;
                        lnkbtnRecieve.Visible = false;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved  UnSucessful')</script>", false);
                    }
                    //Session["SendUserLevel"] = hdnsend.Value;
                }
                else if (Session["user_level"].ToString() == "19" || Session["user_level"].ToString() == "137") // Role BEO
                {
                    string SchoolCode = this.GVSealSign.DataKeys[rowIndex]["SchoolCode"].ToString();
                    string SendFile = Convert.ToString(this.GVSealSign.DataKeys[rowIndex]["SendFile"]);
                    string ReceiveFile = Convert.ToString(this.GVSealSign.DataKeys[rowIndex]["ReceiveFile"]);
                    LinkButton lnkbtnsend = (LinkButton)GVSealSign.Rows[rowIndex].FindControl("lnkbtnsend");
                    LinkButton lnkbtnRecieve = (LinkButton)GVSealSign.Rows[rowIndex].FindControl("lnkbtnRecieve");
                    string SealSign_DiseCode = Convert.ToString(this.GVSealSign.DataKeys[rowIndex]["SealSign_DiseCode"]);


                    hdnsend.Value = "2";
                    hdnrecieve.Value = "1";
                    string SendFiles = hdnsend.Value;
                    string RecieveFiles = hdnrecieve.Value;
                    string RoleBEO = "19";
                    int Result = UpdateFilingSystemSend(SchoolCode, SendFiles, RecieveFiles, RoleBEO, SealSign_DiseCode);

                    if (Result > 0)
                    {
                        lnkbtnsend.Visible = false;
                        lnkbtnRecieve.Visible = false;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                        LoadData();
                    }
                    else
                    {
                        lnkbtnsend.Visible = true;
                        lnkbtnRecieve.Visible = false;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved  UnSucessful')</script>", false);
                    }
                }
                else
                {

                }
            }
            if (e.CommandName == "LnkRecieve")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int RemoveAt = gvr.RowIndex;
                rowIndex = RemoveAt;
                if (Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "136") // Role DEO
                {
                    string SchoolCode = this.GVSealSign.DataKeys[rowIndex]["SchoolCode"].ToString();
                    LinkButton lnkbtnsend = (LinkButton)GVSealSign.Rows[rowIndex].FindControl("lnkbtnsend");
                    LinkButton lnkbtnRecieve = (LinkButton)GVSealSign.Rows[rowIndex].FindControl("lnkbtnRecieve");
                    string SendFile = Convert.ToString(this.GVSealSign.DataKeys[rowIndex]["SendFile"]);
                    string ReceiveFile = Convert.ToString(this.GVSealSign.DataKeys[rowIndex]["ReceiveFile"]);
                    string SealSign_DiseCode = Convert.ToString(this.GVSealSign.DataKeys[rowIndex]["SealSign_DiseCode"]);

                    // hdnsend.Value = "2";
                    hdnrecieve.Value = "3";
                    if (ReceiveFile == "3")
                    {
                        hdnrecieve.Value = "4";
                    }
                    string RoleDEO = "30";
                    //string SendFiles = hdnsend.Value;
                    string RecieveFiles = hdnrecieve.Value;
                    int Result = UpdateFilingSystemRecieved(SchoolCode, RecieveFiles, RoleDEO, SealSign_DiseCode);

                    if (Result > 0)
                    {
                        lnkbtnsend.Visible = false;
                        lnkbtnRecieve.Visible = false;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                        LoadData();
                    }
                    else
                    {
                        lnkbtnsend.Visible = true;
                        lnkbtnRecieve.Visible = false;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved  UnSucessful')</script>", false);
                    }

                }
                else if (Session["user_level"].ToString() == "19" || Session["user_level"].ToString() == "137") // Role BEO
                {
                    string SchoolCode = this.GVSealSign.DataKeys[rowIndex]["SchoolCode"].ToString();
                    string SendFile = Convert.ToString(this.GVSealSign.DataKeys[rowIndex]["SendFile"]);
                    string ReceiveFile = Convert.ToString(this.GVSealSign.DataKeys[rowIndex]["ReceiveFile"]);
                    LinkButton lnkbtnsend = (LinkButton)GVSealSign.Rows[rowIndex].FindControl("lnkbtnsend");
                    LinkButton lnkbtnRecieve = (LinkButton)GVSealSign.Rows[rowIndex].FindControl("lnkbtnRecieve");
                    string SealSign_DiseCode = Convert.ToString(this.GVSealSign.DataKeys[rowIndex]["SealSign_DiseCode"]);

                    hdnrecieve.Value = string.Empty;
                    string RoleDEO = "19";
                    string RecieveFiles = "2";
                    int Result = UpdateFilingSystemRecieved(SchoolCode, RecieveFiles, RoleDEO, SealSign_DiseCode);
                    if (Result > 0)
                    {
                        lnkbtnsend.Visible = false;
                        lnkbtnRecieve.Visible = true;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                        LoadData();
                    }
                    else
                    {
                        lnkbtnsend.Visible = true;
                        lnkbtnRecieve.Visible = false;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved  UnSucessful')</script>", false);
                    }

                }
                else
                {

                }
            }

        }
        catch (Exception)
        {

            throw;
        }
    }

    public int UpdateFilingSystemSend(string SchoolCode, string sendFile, string RecieveFiles, string Role, string SealSign_DiseCode)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
       {
            new SqlParameter("@SchoolCode", SchoolCode),
            new SqlParameter("@SendFlag", sendFile),
            new SqlParameter("@RecieveFlag", RecieveFiles),
            new SqlParameter("@Role", Role),
            new SqlParameter("@SealSign_DiseCode", SealSign_DiseCode)

       };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "usp_FilingSendUpdate", cmdParameters);
    }


    public int UpdateFilingSystemRecieved(string schoolCode, string recieveFiles, string Role, string SealSign_DiseCode)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
      {
            new SqlParameter("@SchoolCode", schoolCode),
            new SqlParameter("@RecieveFlag", recieveFiles),
            new SqlParameter("@Role", Role),
               new SqlParameter("@SealSign_DiseCode", SealSign_DiseCode)
      };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "usp_FilingRecieveUpdate", cmdParameters);
    }


}