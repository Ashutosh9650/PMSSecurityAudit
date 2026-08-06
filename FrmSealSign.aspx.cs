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

public partial class FrmSealSign : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    public bool edit_status = false;
    public static string STRPRINTCONTENT2;

    string flag = "";
    Password objPass = new Password();
    SqlInjection sqlInjection = new SqlInjection();
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
                    if (Session["user_level"].ToString() == "145")
                    {
                        string strQry = "Select * from mst3Block  where Blockcode='" + Convert.ToString(a[0].ToString()) + "' ";


                        DataTable dtBlock = objMain.LoadData(strQry);
                        ddlDistrict.SelectedValue = dtBlock.Rows[0]["DistrictCode"].ToString();
                        ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
                    }
                    else
                    {
                        ddlDistrict.SelectedValue = Session["NewDistrictCode"].ToString();
                    }

                    ddlBlock.SelectedValue = Convert.ToString(a[0].ToString());
                    ddlBlock_SelectedIndexChanged(ddlBlock, null);

                    ddlVillage.SelectedValue = Convert.ToString(a[1].ToString());
                    ddlVillage_SelectedIndexChanged(ddlVillage, null);

                }
                LoadData();
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
        catch
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
            ddlBlock.SelectedIndex = 1;
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
    public void LoadUserLeavel()
    {
        string conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
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
            //conditions = "StateCode ='" + ddlState.SelectedValue + "'  and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
            //objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

            //ddlDistrict.SelectedIndex = 0;
            ddlState_SelectedIndexChanged(ddlDistrict, null);
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
        string strQry = "", conditions = "", conditions1 = "";
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
        string ddlVillageMain = "";
        foreach (System.Web.UI.WebControls.ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillageMain += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlVillageStr.Length > 0)
        {
            ddlVillageStr = ddlVillageStr.Substring(0, ddlVillageStr.LastIndexOf(","));
        }
        if (ddlVillageMain.Length > 0)
        {
            ddlVillageMain = ddlVillageMain.Substring(0, ddlVillageMain.LastIndexOf(","));
        }
        if (ddlVillageStr.Length > 0)
        {
            conditions = conditions + " and v.ClusterCode in (" + ddlVillageStr + ") ";
        }
        if (ddlVillageMain.Length > 0)
        {
            conditions = conditions + " and v.villagecode in (" + ddlVillageMain + ") ";
        }
        //conditions1 = conditions;
        //if (ddlFc.SelectedIndex > 0)
        //{
        //    conditions = conditions + " and tblEnrolment.CreateBy='" + ddlFc.SelectedValue.ToString() + "' ";

        //}

        SqlParameter[] parm1 = new SqlParameter[]
            {

               new SqlParameter("@Con",  conditions),
                 new SqlParameter("@Flag",  2),
            };


        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[SP_GET_Seal_Sign_New_08_06]", parm1);
        if (dt != null && dt.Rows.Count > 0)
        {
            //foreach (DataRow row in dt.Rows)
            //{
            //    string encryptionKey = row["UniqueChildCode"].ToString();

            //    if (dt.Columns.Contains("ChildName") && row["ChildName"] != DBNull.Value)
            //        row["ChildName"] = sqlInjection.DecryptMatchingWithSessionMasking(row["ChildName"].ToString(), "ChildName");

            //    if (dt.Columns.Contains("FathersName") && row["FathersName"] != DBNull.Value)
            //        row["FathersName"] = sqlInjection.DecryptMatchingWithSessionMasking(row["FathersName"].ToString(), "FathersName");

            //    if (dt.Columns.Contains("DOB") && row["DOB"] != DBNull.Value)
            //    {
            //        string encryptedDOB = row["DOB"].ToString();
            //        string decryptedDOB = sqlInjection.DecryptMatchingWithSessionMasking(encryptedDOB, "DOB", false);

            //        row["DOB"] = decryptedDOB;

            //        sqlInjection.ProcessRowAgeAndDob(row, decryptedDOB);
            //    }

            //    if (dt.Columns.Contains("DOBAvailable") && row["DOBAvailable"] != DBNull.Value)
            //    {
            //        string encryptedDOB = row["DOBAvailable"].ToString();
            //        string decryptedDOBAvailable = sqlInjection.DecryptMatchingWithSessionMasking(encryptedDOB, "DOBAvailable", false);

            //        row["DOBAvailable"] = decryptedDOBAvailable;

            //        sqlInjection.ProcessRowAgeAndDob(row, decryptedDOBAvailable);
            //    }
            //}
        }
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
            foreach (DataTable dt in ds.Tables)
            {
                if (dt.Rows.Count == 0)
                    continue;

                foreach (DataRow row in dt.Rows)
                {
                    string encryptionKey = dt.Columns.Contains("UniqueChildCode")
                        ? row["UniqueChildCode"].ToString()
                        : string.Empty;

                    if (dt.Columns.Contains("ChildName") && row["ChildName"] != DBNull.Value)
                    {
                        row["ChildName"] = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["ChildName"].ToString(),
                            "ChildName");
                    }

                    if (dt.Columns.Contains("FathersName") && row["FathersName"] != DBNull.Value)
                    {
                        row["FathersName"] = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["FathersName"].ToString(),
                            "FathersName");
                    }

                    if (dt.Columns.Contains("DOB") && row["DOB"] != DBNull.Value)
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
            foreach (DataTable table in ds.Tables)
            {
                if (table.Rows.Count == 0)
                    continue;

                foreach (DataRow row in table.Rows)
                {
                    if (table.Columns.Contains("ChildName") && row["ChildName"] != DBNull.Value)
                    {
                        row["ChildName"] = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["ChildName"].ToString(), "ChildName");
                    }

                    if (table.Columns.Contains("FathersName") && row["FathersName"] != DBNull.Value)
                    {
                        row["FathersName"] = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["FathersName"].ToString(), "FathersName");
                    }

                    if (table.Columns.Contains("DOB") && row["DOB"] != DBNull.Value)
                    {
                        string decryptedDOB = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["DOB"].ToString(), "DOB", false);

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
        sb.Append("<tr><td colspan='3' style='text-align: left'><b>Admin District:</b>" + dt.Rows[0]["DistrictName"] + ",&nbsp;&nbsp;<b>Admin Block:</b>" + dt.Rows[0]["BlockName"] + ",&nbsp;&nbsp;<b>Cluster:</b>" + dt.Rows[0]["ClusterName"] + ",&nbsp;&nbsp;<b>Panchayat:</b>" + dt.Rows[0]["PanchayatName"] + ",&nbsp;&nbsp;<b>Village:</b>" + dt.Rows[0]["VillageName"] + ",&nbsp;&nbsp;<b>School:</b>" + dt.Rows[0]["School"] + " </td></tr>");
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
            string dsssssssssssss1 = Server.MapPath(Comman.GetImagePath("TravelvouchersPath") + "\\" + filename);
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
        DataSet ds = GetDataSetNew(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[SP_GET_Seal_Sign_New_08_06]", parm1);
        if (ds != null && ds.Tables.Count > 0)
        {
            foreach (DataTable table in ds.Tables)
            {
                if (table.Rows.Count == 0)
                    continue;

                foreach (DataRow row in table.Rows)
                {
                    if (table.Columns.Contains("ChildName") && row["ChildName"] != DBNull.Value)
                    {
                        row["ChildName"] = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["ChildName"].ToString(), "ChildName");
                    }

                    if (table.Columns.Contains("FathersName") && row["FathersName"] != DBNull.Value)
                    {
                        row["FathersName"] = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["FathersName"].ToString(), "FathersName");
                    }

                    if (table.Columns.Contains("DOB") && row["DOB"] != DBNull.Value)
                    {
                        string decryptedDOB = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["DOB"].ToString(), "DOB", false);

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
            conditions = conditions + " and tblEnrolment.SchoolCode='" + SchoolCode + "' ";
        }
        conditions = conditions + " and tblEnrolment.SealSign_DiseCode='" + DiseCode + "'";

        SqlParameter[] parm1 = new SqlParameter[]
            {

               new SqlParameter("@Con",  conditions),
                 new SqlParameter("@Flag",  3),
            };

        string[] a = DiseCode.Split('_');
        DataSet ds = GetDataSetNew(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[SP_GET_Seal_Sign_New_08_06]", parm1);
        if (ds != null && ds.Tables.Count > 0)
        {
            foreach (DataTable table in ds.Tables)
            {
                if (table.Rows.Count == 0)
                    continue;

                foreach (DataRow row in table.Rows)
                {
                    if (table.Columns.Contains("ChildName") && row["ChildName"] != DBNull.Value)
                    {
                        row["ChildName"] = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["ChildName"].ToString(), "ChildName");
                    }

                    if (table.Columns.Contains("FathersName") && row["FathersName"] != DBNull.Value)
                    {
                        row["FathersName"] = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["FathersName"].ToString(), "FathersName");
                    }

                    if (table.Columns.Contains("DOB") && row["DOB"] != DBNull.Value)
                    {
                        string decryptedDOB = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["DOB"].ToString(), "DOB", false);

                        row["DOB"] = decryptedDOB;
                        sqlInjection.ProcessRowAgeAndDob(row, decryptedDOB);
                    }
                }
            }
        }

        DataTable dtFcName = objComman.Select_All_Data("MstUser", "UserName +' ('+ FristName +')' as UserName", " DistrictCode ='" + ddlDistrict.SelectedValue + "' and  VillageCode='" + ClusterCode + "' and len(VillageCode)>2 and UserLevel=24 and ActiveStatus=1", "", "");
        DataTable dt = ds.Tables[0];
        DataRow[] dr777 = dt.Select("DistrictNameNew='" + ddlDistrict.SelectedItem.Text + "' ");
        if (dr777.Length > 0)
        {
        }
        else
        {
            return "";
        }
        DataTable dt1 = ds.Tables[1];
        DataRow[] d = dt.Select("Class='1'");
        DataRow[] dr = dt.Select("Class  in(2,3,4,5,6,7,8)");
        DataRow[] dr1 = dt.Select("Class  not in('1','2','3','4','5','6','7','8')");
        int class1 = d.Length;
        int clss2T8 = dr.Length;
        int classOther = dr1.Length;
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
        sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["21"])) + "</th>");
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

        sb.Append("<tr><td style='font-weight:bold'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ह. एफ सी</td><td style='font-weight:bold'>हस्ताक्षर-प्रधानाध्यापक/प्रभारी प्रधानाध्यापक</td><td style='font-weight:bold'>ह. ब्लॉक ऑफिसर</td></tr>");
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
            foreach (DataTable table in ds.Tables)
            {
                if (table.Rows.Count == 0)
                    continue;

                foreach (DataRow row in table.Rows)
                {
                    if (table.Columns.Contains("ChildName") && row["ChildName"] != DBNull.Value)
                    {
                        row["ChildName"] = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["ChildName"].ToString(), "ChildName");
                    }

                    if (table.Columns.Contains("FathersName") && row["FathersName"] != DBNull.Value)
                    {
                        row["FathersName"] = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["FathersName"].ToString(), "FathersName");
                    }

                    if (table.Columns.Contains("DOB") && row["DOB"] != DBNull.Value)
                    {
                        string decryptedDOB = sqlInjection.DecryptMatchingWithSessionMasking(
                            row["DOB"].ToString(), "DOB", false);

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
        sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["13"])) + "</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["14"])) + "</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["15"])) + "</th> ");
        sb.Append("<th style='font-weight:bold'> ");
        sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["16"])) + "</th>");
        sb.Append("<th style='font-weight:bold'> ");
        sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["20"])) + "</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["17"])) + "</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["18"])) + "</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("Unique ID</th>");
        sb.Append("<th style='font-weight:bold'>");
        sb.Append("" + Server.HtmlDecode(Convert.ToString(dt1.Rows[0]["11"])) + "</th>");
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
        sb.Append("<tr><td style='font-weight:bold'>ह. एफ सी</td><td style='font-weight:bold'>ह. संस्था प्रधान मयसील</td><td style='font-weight:bold'>ह. ब्लॉक ऑफिसर</td></tr>");
        sb.Append("</table>");
        return sb.ToString();
    }
    protected void GVSealSign_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DropDownList ddlRe = (DropDownList)e.Row.FindControl("ddlRe");
            Label lblSchoolCode = (Label)e.Row.FindControl("lblSchoolCode");
            LinkButton lnkGenerate = (LinkButton)e.Row.FindControl("lnkGenerate");
            LinkButton lblCategory = (LinkButton)e.Row.FindControl("lblCategory");

            Int32 Icount = 0;
            if (lblCategory.Text != "")
            {
                Icount = Convert.ToInt32(lblCategory.Text);
            }


            lnkGenerate.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Seal Sign Generation? ')");
            objComman.BindDLL("tblEnrolment", " CONVERT(varchar, SealSign_DiseCode) FormNo,CONVERT(varchar, SealSign_DiseCode)+'-'+ CONVERT(varchar, SealSignGenerateDate,103) as Name", "SchoolCode='" + lblSchoolCode.Text + "' and  isnull(FormNo,0)>0", "FormNo", "asc", ddlRe, "Name", "FormNo", "Select");
            if (Icount > 0)
            {
                lnkGenerate.Visible = true;
            }
            else
            {
                lnkGenerate.Visible = false;
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
            DropDownList ddlRe = (DropDownList)GVSealSign.Rows[indx].FindControl("ddlRe");
            //GenratePDF(lblSchoolCode.Text);
            // GenratePDF_pdfTable(lblSchoolCode.Text);

            // GenrateExcel_toPDf(lblSchoolCode.Text);
            //ConvertExcelTopdf(Convert.ToString(ViewState["Filename"]));
            if (ddlRe.SelectedIndex > 0)
            {
                string st = RetStringBuilderNew(lblSchoolCode.Text, ddlRe.SelectedValue, lblClusterCode.Text);
                PrintCards(st);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Plese select seal sign Form')</script>", false);
                return;
            }

        }
        catch
        {
            throw;
        }
    }
    protected void OOD2Dtargetmet_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;
        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lblCategory") as LinkButton).Text;
        string lblSchoolCode = (gvr.FindControl("lblSchoolCode") as Label).Text;
        string conditions = "";
        string Con = "";
        conditions += "  mstschool.SchoolCOde= '" + lblSchoolCode + "' ";
        Con = " and  isnull(FormNo,0)=0 and IsComplete=1 and EnrolmentMatching=1 ";
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
                new SqlParameter("@con1",Con),
         new SqlParameter("@Flag","3"),

        };
        DataTable dt = null;
        if (Convert.ToInt32(values) > 0)
        {
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetails]", cmdParameters);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string encryptionKey = row["UniqueChildCode"].ToString();

                    if (dt.Columns.Contains("ChildName") && row["ChildName"] != DBNull.Value)
                        row["ChildName"] = sqlInjection.DecryptMatchingWithSessionMasking(row["ChildName"].ToString(), "ChildName");

                    if (dt.Columns.Contains("FathersName") && row["FathersName"] != DBNull.Value)
                        row["FathersName"] = sqlInjection.DecryptMatchingWithSessionMasking(row["FathersName"].ToString(), "FathersName");

                    if (dt.Columns.Contains("DOB") && row["DOB"] != DBNull.Value)
                    {
                        string encryptedDOB = row["DOB"].ToString();
                        string decryptedDOB = sqlInjection.DecryptMatchingWithSessionMasking(encryptedDOB, "DOB", false);

                        row["DOB"] = decryptedDOB;

                        sqlInjection.ProcessRowAgeAndDob(row, decryptedDOB);
                    }
                }
            }

            PopUpGrid.DataSource = dt;
            PopUpGrid.DataBind();
            MpexdrPopUp.Show();
        }
    }
    protected void LnkGenerate_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lblCategory") as LinkButton).Text;
        string lblSchoolCode = (gvr.FindControl("lblSchoolCode") as Label).Text;
        DropDownList ddlRe = (gvr.FindControl("ddlRe") as DropDownList);
        LinkButton lnkGenerate = gvr.FindControl("lnkGenerate") as LinkButton;
        LinkButton lblCategory = gvr.FindControl("lblCategory") as LinkButton;

        string conditions = "";
        conditions += "  mstschool.SchoolCOde= '" + lblSchoolCode + "' ";
        SqlParameter[] cmdParameters = new SqlParameter[]
{
new SqlParameter("@con",conditions),
         new SqlParameter("@Flag","1"),

};
        DataSet ds = null;
        if (Convert.ToInt32(values) > 0)
        {
            ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPMSTrackingDetails_New]", cmdParameters);


            int NoofSet = 0, NoofSet1 = 0;
            int No = 0, No1 = 0;
            NoofSet = ds.Tables[0].Rows.Count / 12;
            No = ds.Tables[0].Rows.Count % 12;
            if (No > 0)
            {
                NoofSet++;
            }
            NoofSet1 = ds.Tables[1].Rows.Count / 12;
            No1 = ds.Tables[1].Rows.Count % 12;
            if (No1 > 0)
            {
                NoofSet1++;
            }
            if (ds.Tables[2].Rows.Count > 0)
            {
                for (int i = Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + 1; i <= Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet; i++)
                {
                    if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + 1)
                    {
                        for (int j = 0; j < 12; j++)
                        {
                            if (j < ds.Tables[0].Rows.Count)
                            {
                                SqlParameter[] p = new SqlParameter[]
                            {
                            new SqlParameter("@UniqueChildCode",ds.Tables[0].Rows[j]["UniqueChildCode"]),
                                        new SqlParameter("@DiseCode", ds.Tables[0].Rows[j]["DiseCode"]),
                                     new SqlParameter("@FormNo",i),

                            };
                                DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                            }
                        }

                    }
                    else if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + 2)
                    {
                        for (int j = 12; j <= 24; j++)
                        {
                            if (j < ds.Tables[0].Rows.Count)
                            {
                                SqlParameter[] p = new SqlParameter[]
{
new SqlParameter("@UniqueChildCode",ds.Tables[0].Rows[j]["UniqueChildCode"]),
            new SqlParameter("@DiseCode", ds.Tables[0].Rows[j]["DiseCode"]),
         new SqlParameter("@FormNo",i),

};
                                DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                            }
                        }
                    }
                    else if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + 3)
                    {
                        for (int j = 24; j <= 36; j++)
                        {
                            if (j < ds.Tables[0].Rows.Count)
                            {
                                SqlParameter[] p = new SqlParameter[]
{
new SqlParameter("@UniqueChildCode",ds.Tables[0].Rows[j]["UniqueChildCode"]),
            new SqlParameter("@DiseCode", ds.Tables[0].Rows[j]["DiseCode"]),
         new SqlParameter("@FormNo",i),

};
                                DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                            }
                        }
                    }
                }
                if (NoofSet1 > 0)
                {
                    for (int i = Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet + 1; i <= Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet + NoofSet1 + 1; i++)
                    {
                        if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet + 1)
                        {
                            for (int j = 0; j < 12; j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
{
new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
            new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
         new SqlParameter("@FormNo",i),

};
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }

                        }
                        else if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet + 2)
                        {
                            for (int j = 12; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
{
new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
            new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
         new SqlParameter("@FormNo",i),

};
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }
                        else if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet + 3)
                        {
                            for (int j = 24; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
{
new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
            new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
         new SqlParameter("@FormNo",i),

};
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }
                        else if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet + 4)
                        {
                            for (int j = 36; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
{
new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
            new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
         new SqlParameter("@FormNo",i),

};
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }
                        else if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet + 5)
                        {
                            for (int j = 48; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
{
new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
            new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
         new SqlParameter("@FormNo",i),

};
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }
                        else if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet + 6)
                        {
                            for (int j = 60; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
{
                            new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
                                        new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
                                     new SqlParameter("@FormNo",i),

};
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }

                        else if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet + 7)
                        {
                            for (int j = 72; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
{
                            new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
                                        new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
                                     new SqlParameter("@FormNo",i),

};
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }
                        else if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet + 8)
                        {
                            for (int j = 84; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
                                {
                            new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
                                        new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
                                     new SqlParameter("@FormNo",i),

                                };
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }
                        else if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet + 9)
                        {
                            for (int j = 96; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
                                {
                            new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
                                        new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
                                     new SqlParameter("@FormNo",i),

                                };
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }
                        else if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet + 10)
                        {
                            for (int j = 108; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
                                {
                            new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
                                        new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
                                     new SqlParameter("@FormNo",i),

                                };
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }
                        else if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet + 11)
                        {
                            for (int j = 120; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
                                {
                            new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
                                        new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
                                     new SqlParameter("@FormNo",i),

                                };
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }
                        else if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet + 12)
                        {
                            for (int j = 132; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
                                {
                            new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
                                        new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
                                     new SqlParameter("@FormNo",i),

                                };
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }
                        else if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet + 13)
                        {
                            for (int j = 144; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
                                {
                            new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
                                        new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
                                     new SqlParameter("@FormNo",i),

                                };
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }
                        else if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet + 14)
                        {
                            for (int j = 156; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
                                {
                            new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
                                        new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
                                     new SqlParameter("@FormNo",i),

                                };
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }
                        else if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet + 15)
                        {
                            for (int j = 168; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
                                {
                            new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
                                        new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
                                     new SqlParameter("@FormNo",i),

                                };
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }
                        else if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet + 16)
                        {
                            for (int j = 180; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
                                {
                            new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
                                        new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
                                     new SqlParameter("@FormNo",i),

                                };
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }
                        else if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet + 17)
                        {
                            for (int j = 192; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
                                {
                            new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
                                        new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
                                     new SqlParameter("@FormNo",i),

                                };
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }

                        else if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet + 18)
                        {
                            for (int j = 202; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
                                {
                            new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
                                        new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
                                     new SqlParameter("@FormNo",i),

                                };
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }

                        else if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet + 19)
                        {
                            for (int j = 212; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
                                {
                            new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
                                        new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
                                     new SqlParameter("@FormNo",i),

                                };
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }

                        else if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet + 20)
                        {
                            for (int j = 222; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
                                {
                            new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
                                        new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
                                     new SqlParameter("@FormNo",i),

                                };
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }

                        else if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet + 21)
                        {
                            for (int j = 234; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
                                {
                            new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
                                        new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
                                     new SqlParameter("@FormNo",i),

                                };
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }
                        else if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet + 22)
                        {
                            for (int j = 246; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
                                {
                            new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
                                        new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
                                     new SqlParameter("@FormNo",i),

                                };
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }
                        else if (i == Convert.ToInt32(ds.Tables[2].Rows[0]["FormNo"]) + NoofSet + 23)
                        {
                            for (int j = 258; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
                                {
                            new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
                                        new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
                                     new SqlParameter("@FormNo",i),

                                };
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (NoofSet > 0)
                {
                    for (int i = 1; i <= NoofSet; i++)
                    {

                        if (i == 1)
                        {
                            for (int j = 0; j < 12; j++)
                            {
                                if (j < ds.Tables[0].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
{
new SqlParameter("@UniqueChildCode",ds.Tables[0].Rows[j]["UniqueChildCode"]),
            new SqlParameter("@DiseCode", ds.Tables[0].Rows[j]["DiseCode"]),
         new SqlParameter("@FormNo",i),

};
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }

                        }
                        else if (i == 2)
                        {
                            for (int j = 12; j <= 24; j++)
                            {
                                if (j < ds.Tables[0].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
{
new SqlParameter("@UniqueChildCode",ds.Tables[0].Rows[j]["UniqueChildCode"]),
            new SqlParameter("@DiseCode", ds.Tables[0].Rows[j]["DiseCode"]),
         new SqlParameter("@FormNo",i),

};
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }
                        else if (i == 3)
                        {
                            for (int j = 24; j <= 36; j++)
                            {
                                if (j < ds.Tables[0].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
{
new SqlParameter("@UniqueChildCode",ds.Tables[0].Rows[j]["UniqueChildCode"]),
            new SqlParameter("@DiseCode", ds.Tables[0].Rows[j]["DiseCode"]),
         new SqlParameter("@FormNo",i),

};
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }
                    }
                }

                if (NoofSet1 > 0)
                {
                    for (int i = NoofSet + 1; i <= NoofSet + NoofSet1 + 1; i++)
                    {
                        if (i == NoofSet + 1)
                        {
                            for (int j = 0; j < 12; j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
{
new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
            new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
         new SqlParameter("@FormNo",i),

};
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }

                        }
                        else if (i == NoofSet + 2)
                        {
                            for (int j = 12; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
{
new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
            new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
         new SqlParameter("@FormNo",i),

};
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }
                        else if (i == NoofSet + 3)
                        {
                            for (int j = 24; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
{
new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
            new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
         new SqlParameter("@FormNo",i),

};
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }
                        else if (i == NoofSet + 4)
                        {
                            for (int j = 36; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
{
new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
            new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
         new SqlParameter("@FormNo",i),

};
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }
                        else if (i == NoofSet + 5)
                        {
                            for (int j = 48; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
{
new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
            new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
         new SqlParameter("@FormNo",i),

};
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }
                        else if (i == NoofSet + 6)
                        {
                            for (int j = 60; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
{
new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
            new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
         new SqlParameter("@FormNo",i),

};
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }

                        else if (i == NoofSet + 7)
                        {
                            for (int j = 72; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
{
new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
            new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
         new SqlParameter("@FormNo",i),

};
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }
                        else if (i == NoofSet + 8)
                        {
                            for (int j = 84; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
{
new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
            new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
         new SqlParameter("@FormNo",i),

};
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }
                        else if (i == NoofSet + 9)
                        {
                            for (int j = 96; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
{
new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
            new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
         new SqlParameter("@FormNo",i),

};
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }

                        else if (i == NoofSet + 10)
                        {
                            for (int j = 108; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
                                {
                                new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
                                            new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
                                         new SqlParameter("@FormNo",i),

                                };
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }

                        else if (i == NoofSet + 11)
                        {
                            for (int j = 120; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
                                {
                                new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
                                            new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
                                         new SqlParameter("@FormNo",i),

                                };
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }

                        else if (i == NoofSet + 12)
                        {
                            for (int j = 132; j <= (12 * i); j++)
                            {
                                if (j < ds.Tables[1].Rows.Count)
                                {
                                    SqlParameter[] p = new SqlParameter[]
                                {
                                new SqlParameter("@UniqueChildCode",ds.Tables[1].Rows[j]["UniqueChildCode"]),
                                            new SqlParameter("@DiseCode", ds.Tables[1].Rows[j]["DiseCode"]),
                                         new SqlParameter("@FormNo",i),

                                };
                                    DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Seal_sign_Update_MaleFemale]", p);
                                }
                            }
                        }

                    }
                }

            }
        }
        objComman.BindDLL("tblEnrolment", " CONVERT(varchar, SealSign_DiseCode) as FormNo,CONVERT(varchar, SealSign_DiseCode)+'-'+ CONVERT(varchar, SealSignGenerateDate,103) as Name", "SchoolCode='" + lblSchoolCode + "' and formNo>0", "FormNo", "asc", ddlRe, "Name", "FormNo", "Select");
        lnkGenerate.Visible = false;
        LoadData();
        lblCategory.Text = "";
    }

    protected void PrintCards(string RetStringBuilder)
    {

        string a = HttpContext.Current.Server.MapPath(Comman.GetImagePath("MouPath") + "/" + "Testhtml.htm");

        string FIleName = ddlDistrict.SelectedItem.Text + "_" + DateTime.Now.ToString("dd_MM_yyyy_hhmmssfff") + "Testhtml" + ".htm";
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
        STRPRINTCONTENT2 = "";
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
}