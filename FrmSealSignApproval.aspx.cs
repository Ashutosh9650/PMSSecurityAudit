using PMS.Crypto.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmSealSignApproval : System.Web.UI.Page
{

    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    public bool edit_status = false;
    string conditions = "";
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

                    ddlVillage.SelectedValue = a[1].ToString();
                    ddlVillage_SelectedIndexChanged(ddlVillage, null);
                    LoadData();
                    FillClass();
                    FillSocialCat();
                    FillENrollment();
                    FillEduStauts();
                }

                btnMain.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to submit validation results? ')");
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
    #region ************* Button Click Event
    protected void lnkPrev_OnClick(object sender, EventArgs e)
    {
        try
        {
            DataRow[] dr1 = Session["dr"] as DataRow[];
            int Sequence = Convert.ToInt32(Session["Sequence"]);
            if (Sequence >= 0)
            {
                if (Sequence <= dr1.Length && Sequence != 0)
                {
                    Imag.ImageUrl = "TabletImage/" + Convert.ToString(dr1[Sequence - 1]["SealFormImage"]);
                    Session["Sequence"] = Sequence - 1;
                    lblDisplay.Text = Convert.ToString(dr1[Sequence - 1]["SealFormImage"]);
                    lblSchoolName.Text = Convert.ToString(dr1[Sequence - 1]["SchoolName"]);
                    DataTable dt = Session["GridViewData"] as DataTable;
                    string strFilter = "";

                    string str = "SealFormImage";

                    DataTable dtfilter = dt.Copy();
                    strFilter = str + " = '" + lblDisplay.Text + "'    ";
                    dtfilter.DefaultView.RowFilter = strFilter;
                    dtfilter.DefaultView.Sort = "EnrolmentDateNew,Serial ";
                    lblDiscode.Text = Convert.ToString(dtfilter.DefaultView.ToTable().Rows[0]["SealSign_DiseCode"]);
                    GVSealSign.DataSource = dtfilter.DefaultView.ToTable();
                    GVSealSign.DataBind();
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void lnkDownload_OnClick(object sender, EventArgs e)
    {
        try
        {


            DataRow[] dr1 = Session["dr"] as DataRow[];
            int Sequence = Convert.ToInt32(Session["Sequence"]);
            FileInfo file = new FileInfo((Server.MapPath(Comman.GetImagePath("TabletImagePath") + "/" + Convert.ToString(dr1[Sequence]["SealFormImage"]))));
            if (file.Exists)
            {

                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + Convert.ToString(dr1[Sequence]["SealFormImage"]));
                string aaa = Server.MapPath(Comman.GetImagePath("TabletImagePath") + "/" + Convert.ToString(dr1[Sequence]["SealFormImage"]));
                Response.TransmitFile(Server.MapPath(Comman.GetImagePath("TabletImagePath") + "/" + Convert.ToString(dr1[Sequence]["SealFormImage"])));
            }

        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void lnkNext_OnClick(object sender, EventArgs e)
    {
        try
        {
            DataRow[] dr1 = Session["dr"] as DataRow[];
            int Sequence = Convert.ToInt32(Session["Sequence"]);
            if (Sequence >= 0)
            {
                if (Sequence < dr1.Length - 1)
                {
                    Imag.ImageUrl = "TabletImage/" + Convert.ToString(dr1[Sequence + 1]["SealFormImage"]);


                    Session["Sequence"] = Sequence + 1;
                    lblDisplay.Text = Convert.ToString(dr1[Sequence + 1]["SealFormImage"]);
                    lblSchoolName.Text = Convert.ToString(dr1[Sequence + 1]["SchoolName"]);
                    DataTable dt = Session["GridViewData"] as DataTable;
                    string strFilter = "";

                    string str = "SealFormImage";

                    DataTable dtfilter = dt.Copy();
                    strFilter = str + " = '" + lblDisplay.Text + "'    ";
                    dtfilter.DefaultView.RowFilter = strFilter;
                    dtfilter.DefaultView.Sort = "EnrolmentDateNew,Serial ";
                    lblDiscode.Text = Convert.ToString(dtfilter.DefaultView.ToTable().Rows[0]["SealSign_DiseCode"]);
                    GVSealSign.DataSource = dtfilter.DefaultView.ToTable();
                    GVSealSign.DataBind();
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
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



            if (!Validation())
                return;
            int ret = 0;
            Boolean Flag = false;
            //  UpdateData();
            ///     DataTable dt = (DataTable)Session["GridViewData"];
            Button btn = sender as Button;
            for (int i = 0; i < GVSealSign.Rows.Count; i++)
            {
                string C_ID = GVSealSign.DataKeys[i]["UniqueChildCode"].ToString();
                Int32 Approve = 0;
                Int32 Reject = 0;
                Int32 Resone1 = 0;
                Int32 Resone2 = 0;
                CheckBox chkApprove = ((CheckBox)GVSealSign.Rows[i].FindControl("chkApprove"));
                CheckBox chkReject = ((CheckBox)GVSealSign.Rows[i].FindControl("chkReject"));
                DropDownList ddLRejectReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddLRejectReasion"));
                DropDownList ddlSubReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddlSubReasion"));

                if (chkApprove.Checked == true || chkReject.Checked == true || ddLRejectReasion.SelectedIndex > 0)
                {
                    if (chkApprove.Checked == true)
                    {
                        Approve = 1;
                    }
                    if (chkReject.Checked == true)
                    {
                        Approve = 2;
                    }
                    if (ddLRejectReasion.SelectedIndex > 0)
                    {
                        Resone1 = Convert.ToInt32(ddLRejectReasion.SelectedValue);

                    }
                    if (ddlSubReasion.SelectedIndex > 0)
                    {
                        Resone2 = Convert.ToInt32(ddlSubReasion.SelectedValue);
                    }
                }
                if (chkApprove.Checked == true || chkReject.Checked == true)
                {
                    ret = Insert_Update(C_ID, Approve, Resone1.ToString(), Resone2.ToString());
                    if (ret > 0)
                    {
                        Flag = true;
                    }
                }
            }
            //foreach (DataRow row in dt.Rows)
            //{
            //    string lblCUniqueChildCode = Convert.ToString(row["UniqueChildCode"]);
            //    int chkApprove = Convert.ToInt32(row["ApprovalStatus"]);

            //    string ddLRejectReasion = Convert.ToString(row["RejectReason"]);
            //    string RejectSubReason = Convert.ToString(row["RejectSubReason"]);
            //    int IsAr = 0;


            //    ret = Insert_Update(lblCUniqueChildCode, chkApprove, ddLRejectReasion, RejectSubReason);

            //}
            if (Flag == true)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Submit sucessfully')</script>", false);
                //ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Save sucessfully')</script>", false);
                btnSerach_Click(btnSerach, null);
                chkApproveAll.Checked = false;
                chkRejectAll.Checked = false;
                return;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void LnkBtnBlock_OnClick(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnPreview_OnClick(object sender, EventArgs e)
    {
        try
        {
            DataRow[] dr1 = Session["dr"] as DataRow[];
            int Sequence = Convert.ToInt32(Session["Sequence"]);
            if (Sequence >= 0)
            {
                if (Sequence <= dr1.Length)
                {
                    imgMKS.ImageUrl = "TabletImage/" + Convert.ToString(dr1[Sequence]["SealFormImage"]);
                    MpexdrDistrict.Show();
                }
            }
        }
        catch
        {

            throw;
        }
    }
    #endregion-//////
    #region  ************ Fill Method
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
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode in( " + Session["BlockCode"].ToString() + " ) and FYear ='" + ddlYear.SelectedItem.Text + "' ";
        }
        else
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' ";
        }
        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

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
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");
        ddlYear.SelectedIndex = 1;
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
            //conditions = "";
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


        if (vADD == true)
        {
            btnMain.Enabled = true;

        }
        else
        {
            btnMain.Enabled = false;

        }
        if (vVerify == true)
        {



        }
        if (vVerify == true || vADD == true)
        {
            btnMain.Enabled = true;

        }
        else
        {
            btnMain.Enabled = false;

        }

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
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' ";
        objComman.BindDLL("mstcluster", "ClusterCode,dbo.TitleCase(upper(ClusterName)) as ClusterName", conditions, "ClusterName", "asc", ddlVillage, "ClusterName", "ClusterCode", "--Select--");
    }
    public void FillFC()
    {
        conditions = "ActiveStatus =1 And UserLevel=24 ";
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions += " and BlockCode ='" + ddlBlock.SelectedValue + "'  ";
        }
        if (ddlVillage.SelectedIndex > 0)
        {
            conditions += " and VillageCode ='" + ddlVillage.SelectedValue + "' ";
        }

        objComman.BindDLL("mstuser", " UserName as UserID,UserName +' ('+ FristName +')' as UserName ", conditions, "UserName", "asc", ddlFc, "UserName", "UserID", "Select");

    }
    public void LoadData()
    {
        objComman.BindDLL("mstLookup", "LookupCode,Description", "LookupFlag='MR'", "LookupCode", "asc", ddlAllResone, "Description", "LookupCode", "Select");

        string strQry = "";
        conditions = "";
        conditions = " v.StateCode='" + ddlState.SelectedValue.ToString() + "'";

        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions = conditions + " and v.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "' ";
        }

        if (ddlBlock.SelectedIndex > 0)
        {
            conditions = conditions + " and v.BlockCode='" + ddlBlock.SelectedValue.ToString() + "' ";
        }
        if (ddlVillage.SelectedIndex > 0)
        {
            conditions = conditions + " and v.ClusterCode='" + ddlVillage.SelectedValue.ToString() + "' ";
        }
        if (ddlSchool.SelectedIndex > 0)
        {
            conditions = conditions + " and tblEnrolment.SchoolCode='" + ddlSchool.SelectedValue.ToString() + "' ";
        }
        if (ddlFc.SelectedIndex > 0)
        {
            conditions = conditions + " and tblEnrolment.CreateBy='" + ddlFc.SelectedValue.ToString() + "' ";
        }
        if (ddlVillageNew.SelectedIndex > 0)
        {
            conditions = conditions + " and v.Villagecode='" + ddlVillageNew.SelectedValue.ToString() + "' ";
        }
        SqlParameter[] parm1 = new SqlParameter[]
            {

               new SqlParameter("@Con",  conditions),
                 new SqlParameter("@Flag",  1),
            };


        DataTable dt = GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[SP_GET_Seal_Sign]", parm1);
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

        String[] arColoumn = { "SchoolName", "SealFormImage", "SealSign_DiseCode" };
        DataTable dtNew = dt.DefaultView.ToTable(true, arColoumn);
        DataRow[] dr = dtNew.Select("len(SealFormImage)>2");
        Session["dr"] = dr;
        Session["Sequence"] = "0";

        if (dr.Length > 0)
        {

            string path = Comman.GetImagePath("TabletImagePath") + "/";
            if (Convert.ToString(dr[0]["SealFormImage"]) != "" && Convert.ToString(dr[0]["SealFormImage"]).Length > 2)
            {
                DivImage.Visible = true;
                Imag.ImageUrl = "TabletImage/" + Convert.ToString(dr[0]["SealFormImage"]);
                lblDisplay.Visible = true;
                lblSchoolName.Text = Convert.ToString(dr[0]["SchoolName"]);
                lblDisplay.Text = Convert.ToString(dr[0]["SealFormImage"]);
                lblDiscode.Text = Convert.ToString(dr[0]["SealSign_DiseCode"]);

            }
            else
            {
                DivImage.Visible = false;
            }
        }


        if (dt.Rows.Count > 0)
        {

            Session["GridViewData"] = dt;
            string strFilter = "";

            string str = "SealFormImage";

            DataTable dtfilter = dt.Copy();
            strFilter = str + " = '" + lblDisplay.Text + "'    ";
            dtfilter.DefaultView.RowFilter = strFilter;
            dtfilter.DefaultView.Sort = "EnrolmentDateNew,Serial ";
            GVSealSign.DataSource = dtfilter.DefaultView.ToTable();
            GVSealSign.DataBind();

        }
        else
        {
            GVSealSign.DataSource = null;
            GVSealSign.DataBind();
            lblDisplay.Text = "";
            Imag.ImageUrl = "";
            lblDiscode.Text = "";
        }
    }
    public static DataTable GetDataTable(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParameters)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection(connString);
        SqlCommand cmd = new SqlCommand();
        try
        {
            PrepareCommand(cmd, conn, cmdType, cmdText, cmdParameters);
            da.SelectCommand = new SqlCommand();
            cmd.CommandTimeout = 0;
            da.SelectCommand = cmd;
            da.Fill(dt);
            return dt;
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
        FillFC();
        FillCVillagNew();
        FillSchool();
    }
    protected void ddlVillageNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSchoolVill();
    }
    public void FillSchool()
    {
        string strQry = "Select SchoolCode,Name from mstSchool  where VillageCode in(select VillageCode from mst5Village where ClusterCode ='" + ddlVillage.SelectedValue + "')  and FYear ='" + ddlYear.SelectedItem.Text + "'  ";

        DataTable dtSchool = objMain.LoadData(strQry);

        objComman.BindDLLMasterTable("mstSchool", "SchoolCode,Name", dtSchool, conditions, "Name", "asc", ddlSchool, "Name", "SchoolCode", "Select");




        //conditions = "";
        //conditions = "VillageCode ='" + ddlVillage.SelectedValue + "'  and FYear ='" + ddlYear.SelectedItem.Text + "'";
        //objComman.BindDLL("mstSchool", "SchoolCode,Name", conditions, "Name", "asc", ddlSchool, "Name", "SchoolCode", "Select");


    }
    public void FillSchoolVill()
    {
        string strQry = "Select SchoolCode,Name from mstSchool  where VillageCode ='" + ddlVillageNew.SelectedValue + "'  and FYear ='" + ddlYear.SelectedItem.Text + "'  ";

        DataTable dtSchool = objMain.LoadData(strQry);

        objComman.BindDLLMasterTable("mstSchool", "SchoolCode,Name", dtSchool, conditions, "Name", "asc", ddlSchool, "Name", "SchoolCode", "Select");




        //conditions = "";
        //conditions = "VillageCode ='" + ddlVillage.SelectedValue + "'  and FYear ='" + ddlYear.SelectedItem.Text + "'";
        //objComman.BindDLL("mstSchool", "SchoolCode,Name", conditions, "Name", "asc", ddlSchool, "Name", "SchoolCode", "Select");


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
    public void FillCVillagNew()
    {
        conditions = "";

        string ddlPhan = "";


        conditions = "";

        conditions = "DistrictCode in('" + ddlDistrict.SelectedValue + "')  and BlockCode in('" + ddlBlock.SelectedValue + "') and  ClusterCode in('" + ddlVillage.SelectedValue + "')";

        //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        //objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--All-");

        string strQry = "  SELECT VillageCode, dbo.TitleCase(upper(VillageName))  as VillageName FROM mst5Village where " + conditions + "  order by VillageName   ";
        DataTable dtDistrict = objMain.LoadData(strQry);


        objComman.BindDLLMasterTable("mstSchool", "VillageCode,VillageName", dtDistrict, conditions, "VillageName", "asc", ddlVillageNew, "VillageName", "VillageCode", "Select");



    }
    protected void ddLRejectReasion_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList drp = (DropDownList)sender;
            GridViewRow gv = (GridViewRow)drp.NamingContainer;
            int index = gv.RowIndex;
            DropDownList ddLRejectReasion = (DropDownList)GVSealSign.Rows[index].FindControl("ddLRejectReasion");
            DropDownList ddlSubReasion = (DropDownList)GVSealSign.Rows[index].FindControl("ddlSubReasion");


            if (ddLRejectReasion.SelectedValue == "1")
            {
                ddlSubReasion.Visible = true;
                objComman.BindDLL("mstLookup", "LookupCode,Description", "LookupFlag='SR1'", "LookupCode", "asc", ddlSubReasion, "Description", "LookupCode", "Select");
            }
            else if (ddLRejectReasion.SelectedValue == "2")
            {
                ddlSubReasion.Visible = true;
                objComman.BindDLL("mstLookup", "LookupCode,Description", "LookupFlag='SR2'", "LookupCode", "asc", ddlSubReasion, "Description", "LookupCode", "Select");
            }
            else
            {
                ddlSubReasion.Visible = false;
            }

        }
        catch
        {

            throw;
        }
    }
    protected void chkRejectAll_OnCheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < GVSealSign.Rows.Count; i++)
        {
            string C_ID = GVSealSign.DataKeys[i]["UniqueChildCode"].ToString();

            CheckBox chkApprove = ((CheckBox)GVSealSign.Rows[i].FindControl("chkApprove"));
            CheckBox chkReject = ((CheckBox)GVSealSign.Rows[i].FindControl("chkReject"));
            DropDownList ddLRejectReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddLRejectReasion"));
            DropDownList ddlSubReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddlSubReasion"));

        }
    }
    protected void chkApproveAll_OnCheckedChanged(object sender, EventArgs e)
    {
        chkRejectAll.Checked = false;
        for (int i = 0; i < GVSealSign.Rows.Count; i++)
        {
            if (chkApproveAll.Checked == true)
            {

                string C_ID = GVSealSign.DataKeys[i]["UniqueChildCode"].ToString();

                CheckBox chkApprove = ((CheckBox)GVSealSign.Rows[i].FindControl("chkApprove"));
                CheckBox chkReject = ((CheckBox)GVSealSign.Rows[i].FindControl("chkReject"));
                DropDownList ddLRejectReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddLRejectReasion"));
                DropDownList ddlSubReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddlSubReasion"));
                chkApprove.Checked = true;
                chkReject.Checked = false;
                ddLRejectReasion.Visible = false;
                ddlSubReasion.Visible = false;
            }
            else
            {
                string C_ID = GVSealSign.DataKeys[i]["UniqueChildCode"].ToString();

                CheckBox chkApprove = ((CheckBox)GVSealSign.Rows[i].FindControl("chkApprove"));
                CheckBox chkReject = ((CheckBox)GVSealSign.Rows[i].FindControl("chkReject"));
                DropDownList ddLRejectReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddLRejectReasion"));
                DropDownList ddlSubReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddlSubReasion"));
                HiddenField hdnApprov = (HiddenField)GVSealSign.Rows[i].FindControl("hdnApprov");
                if (hdnApprov.Value == "1")
                {
                    chkApprove.Checked = true;
                }
                else
                {
                    chkApprove.Checked = false;
                }
                chkReject.Checked = false;
                ddLRejectReasion.Visible = false;
                ddlSubReasion.Visible = false;
            }
        }
    }
    protected void chkRejectAl444l_OnCheckedChanged(object sender, EventArgs e)
    {
        chkApproveAll.Checked = false;


        for (int i = 0; i < GVSealSign.Rows.Count; i++)
        {
            if (chkRejectAll.Checked == true)
            {
                string C_ID = GVSealSign.DataKeys[i]["UniqueChildCode"].ToString();

                CheckBox chkApprove = ((CheckBox)GVSealSign.Rows[i].FindControl("chkApprove"));
                CheckBox chkReject = ((CheckBox)GVSealSign.Rows[i].FindControl("chkReject"));
                DropDownList ddLRejectReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddLRejectReasion"));
                DropDownList ddlSubReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddlSubReasion"));
                HiddenField hdnApprov = (HiddenField)GVSealSign.Rows[i].FindControl("hdnApprov");
                if (hdnApprov.Value == "1")
                {
                    chkApprove.Checked = true;
                }
                else
                {
                    chkApprove.Checked = false;
                    chkReject.Checked = true;

                    ddLRejectReasion.Visible = true;
                }


                MpexdrPopUp.Show();
            }
            else
            {
                string C_ID = GVSealSign.DataKeys[i]["UniqueChildCode"].ToString();

                CheckBox chkApprove = ((CheckBox)GVSealSign.Rows[i].FindControl("chkApprove"));
                CheckBox chkReject = ((CheckBox)GVSealSign.Rows[i].FindControl("chkReject"));
                DropDownList ddLRejectReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddLRejectReasion"));
                DropDownList ddlSubReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddlSubReasion"));
                HiddenField hdnApprov = (HiddenField)GVSealSign.Rows[i].FindControl("hdnApprov");
                if (hdnApprov.Value == "1")
                {
                    chkApprove.Checked = true;
                }
                else
                {
                    chkApprove.Checked = false;
                }
                chkReject.Checked = false;
                ddLRejectReasion.Visible = false;
                ddlSubReasion.Visible = false;
                ddlSubReasion.Items.Clear();
            }

        }
    }
    protected void chkReject_OnCheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox ch = (CheckBox)sender;
            GridViewRow gv = (GridViewRow)ch.NamingContainer;
            int indx = gv.RowIndex;
            CheckBox chk = (CheckBox)GVSealSign.Rows[indx].FindControl("chkReject");
            DropDownList DropDownList1 = (DropDownList)GVSealSign.Rows[indx].FindControl("ddLRejectReasion");
            DropDownList ddlSubReasion = ((DropDownList)GVSealSign.Rows[indx].FindControl("ddlSubReasion"));
            if (chk.Checked)
            {

                DropDownList1.Visible = true;
            }
            else
            {
                DropDownList1.SelectedIndex = 0;
                //GVSealSign.Columns[10].Visible = false;
                //GVSealSign.Columns[10].Visible = false;
                DropDownList1.Visible = false;
                ddlSubReasion.Visible = false;
                if (ddlSubReasion.SelectedIndex > 0)
                {
                    ddlSubReasion.SelectedIndex = 0;
                }


            }


        }
        catch
        {

            throw;
        }
    }
    #endregion
    #region ******* Insert Update
    public int Insert_Update(string UniqueChildCode, int Status, string RejectReason, string subResion)
    {
        SqlConnection dbSqlconnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            dbSqlconnection.Open();
            using (SqlCommand dbSqlCommand = (SqlCommand)dbSqlconnection.CreateCommand())
            {
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = "SP_Seal_Sign_Update2020";
                dbSqlCommand.Parameters.AddWithValue("@UniqueChildCode", UniqueChildCode);
                dbSqlCommand.Parameters.AddWithValue("@ApproveReject", Status);
                dbSqlCommand.Parameters.AddWithValue("@RejectReason", RejectReason);
                dbSqlCommand.Parameters.AddWithValue("@subResion", subResion == "" ? "0" : subResion);
                SqlParameter ReturnAffectedRows = new SqlParameter("@RowAffected", System.Data.SqlDbType.Int);
                ReturnAffectedRows.Direction = ParameterDirection.Output;
                dbSqlCommand.Parameters.Add(ReturnAffectedRows);
                dbSqlCommand.ExecuteNonQuery();
                int _returnRow = Convert.ToInt32(ReturnAffectedRows.Value);
                return _returnRow;
            }
        }
        catch (SqlException exp)
        {
            throw;
        }
        finally
        {
            dbSqlconnection.Dispose();
        }
    }

    public void UpdateData()
    {

        DataTable dt = (DataTable)Session["GridViewData"];

        for (int i = 0; i < GVSealSign.Rows.Count; i++)
        {
            string C_ID = GVSealSign.DataKeys[i]["UniqueChildCode"].ToString();

            CheckBox chkApprove = ((CheckBox)GVSealSign.Rows[i].FindControl("chkApprove"));
            CheckBox chkReject = ((CheckBox)GVSealSign.Rows[i].FindControl("chkReject"));
            DropDownList ddLRejectReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddLRejectReasion"));
            DropDownList ddlSubReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddlSubReasion"));
            if (chkApprove.Checked == true || chkReject.Checked == true || ddLRejectReasion.SelectedIndex > 0)
            {
                DataRow[] dr = dt.Select("UniqueChildCode='" + Convert.ToString(C_ID) + "'");
                if (dr.Length > 0)
                {
                    if (chkApprove.Checked == true)
                    {
                        dr[0]["ApprovalStatus"] = 1;


                    }
                    if (chkReject.Checked == true)
                    {
                        dr[0]["ApprovalStatus"] = 2;
                    }
                    if (ddLRejectReasion.SelectedIndex > 0)
                    {
                        dr[0]["RejectReason"] = ddLRejectReasion.SelectedValue;
                        dr[0]["RejectSubReason"] = ddlSubReasion.SelectedValue;
                    }
                    else
                    {
                        dr[0]["RejectReason"] = "0";
                        dr[0]["RejectSubReason"] = "0";
                    }

                }
            }

        }
        Session["GridViewData"] = dt;

    }
    #endregion

    #region
    private Boolean Validation()
    {
        bool temp = false;
        bool tempName = false;
        int Icount = 0;
        int IcountNew = 0;
        int FIcountNew = 0;

        for (int i = 0; i < GVSealSign.Rows.Count; i++)
        {
            CheckBox chkReject = ((CheckBox)GVSealSign.Rows[i].FindControl("chkReject"));
            CheckBox chkApprove = ((CheckBox)GVSealSign.Rows[i].FindControl("chkApprove"));

            DropDownList ddLRejectReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddLRejectReasion"));
            DropDownList ddlSubReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddlSubReasion"));
            if (Convert.ToInt32(ddLRejectReasion.SelectedValue) == 3)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Other reason ')</script>", false);
                return false;
            }
        }
        for (int i = 0; i < GVSealSign.Rows.Count; i++)
        {
            CheckBox chkReject = ((CheckBox)GVSealSign.Rows[i].FindControl("chkReject"));
            CheckBox chkApprove = ((CheckBox)GVSealSign.Rows[i].FindControl("chkApprove"));

            DropDownList ddLRejectReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddLRejectReasion"));
            DropDownList ddlSubReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddlSubReasion"));
            if (chkReject.Checked == true || chkApprove.Checked == true)
            {
                FIcountNew += 1;
            }
        }

        if (GVSealSign.Rows.Count != FIcountNew)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Approve or rejection  for all children')</script>", false);
            return false;
        }
        for (int i = 0; i < GVSealSign.Rows.Count; i++)
        {
            CheckBox chkReject = ((CheckBox)GVSealSign.Rows[i].FindControl("chkReject"));
            CheckBox chkApprove = ((CheckBox)GVSealSign.Rows[i].FindControl("chkApprove"));

            DropDownList ddLRejectReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddLRejectReasion"));
            DropDownList ddlSubReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddlSubReasion"));
            if (Convert.ToInt32(ddLRejectReasion.SelectedValue) == 1)
            {
                temp = true;
                Icount += 1;
            }
        }

        if (temp == true)
        {
            if (GVSealSign.Rows.Count != Icount)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Seal-Sign not Received rejection reason for all children')</script>", false);
                return false;
            }
        }

        for (int i = 0; i < GVSealSign.Rows.Count; i++)
        {
            CheckBox chkReject = ((CheckBox)GVSealSign.Rows[i].FindControl("chkReject"));
            CheckBox chkApprove = ((CheckBox)GVSealSign.Rows[i].FindControl("chkApprove"));

            DropDownList ddLRejectReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddLRejectReasion"));
            DropDownList ddlSubReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddlSubReasion"));
            if (Convert.ToInt32(ddLRejectReasion.SelectedValue) == 4)
            {
                tempName = true;
                IcountNew += 1;
            }
        }

        if (tempName == true)
        {
            if (GVSealSign.Rows.Count != IcountNew)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Photo Not Clear/Wrong Photo rejection reason for all children')</script>", false);
                return false;
            }
        }
        for (int i = 0; i < GVSealSign.Rows.Count; i++)
        {
            CheckBox chkReject = ((CheckBox)GVSealSign.Rows[i].FindControl("chkReject"));
            CheckBox chkApprove = ((CheckBox)GVSealSign.Rows[i].FindControl("chkApprove"));

            DropDownList ddLRejectReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddLRejectReasion"));
            DropDownList ddlSubReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddlSubReasion"));
            if (chkReject.Checked)
            {
                if (ddLRejectReasion.SelectedIndex > 0)
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Reject Reason')</script>", false);
                    return false;
                }
                if (Convert.ToInt32(ddLRejectReasion.SelectedValue) == 1 || Convert.ToInt32(ddLRejectReasion.SelectedValue) == 2)
                {
                    if (ddlSubReasion.SelectedIndex > 0)
                    {

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Sub Reject Reason')</script>", false);
                        return false;
                    }
                }
            }
            //if (chkApprove.Checked == true || chkReject.Checked == true)
            //{
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Approve or Reject')</script>", false);
            //    return false;
            //}

        }
        return true;
    }
    #endregion
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        //  btnApprove.Attributes.Add("onclick", "javascript:return " + "confirm('Please confirm if you want to approve? ')");


        Response.Redirect("~/FrmEnrollmentBlockWise.aspx");


    }
    protected void GVSealSign_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnApprov = (HiddenField)e.Row.FindControl("hdnApprov");
            HiddenField hdnRejectReasion = (HiddenField)e.Row.FindControl("hdnRejectReasion");
            HiddenField hdnSubReasion = (HiddenField)e.Row.FindControl("hdnSubReasion");
            DropDownList ddLRejectReasion = (DropDownList)e.Row.FindControl("ddLRejectReasion");
            DropDownList ddlSubReasion = (DropDownList)e.Row.FindControl("ddlSubReasion");
            CheckBox chkApprove = (CheckBox)e.Row.FindControl("chkApprove");
            CheckBox chkReject = (CheckBox)e.Row.FindControl("chkReject");
            LinkButton lblChildName = ((LinkButton)e.Row.FindControl("lblChildName"));
            Label lblRejectFlag = ((Label)e.Row.FindControl("lblRejectFlag"));
            Label lblSealSign = ((Label)e.Row.FindControl("lblSealSign"));


            if (lblRejectFlag.Text == "2")
            {
                lblChildName.ForeColor = System.Drawing.Color.Red;
            }
            if (hdnApprov.Value == "1")
            {
                chkApprove.Checked = true;
            }
            else if (hdnApprov.Value == "2")
            {
                chkReject.Checked = true;
                //ddLRejectReasion.Attributes.Add("style", "display:block");
                ddLRejectReasion.Visible = true;
                //ddlSubReasion.Visible = false;
            }
            else
            {
                ddLRejectReasion.Visible = false;
                chkApprove.Checked = false;
                chkReject.Checked = false;
            }
            if (hdnApprov.Value == "1")
            {
                chkReject.Enabled = false;
            }
            else if (hdnApprov.Value == "2")
            {
                chkReject.Enabled = true;
            }
            else
            {
            }
            if (lblSealSign.Text == "2")
            {
            }
            objComman.BindDLL("mstLookup", "LookupCode,Description", "LookupFlag='MR'", "LookupCode", "asc", ddLRejectReasion, "Description", "LookupCode", "Select");
            ddLRejectReasion.SelectedValue = hdnRejectReasion.Value;
            if (hdnRejectReasion.Value == "1")
            {
                ddlSubReasion.Visible = true;
                objComman.BindDLL("mstLookup", "LookupCode,Description", "LookupFlag='SR1'", "LookupCode", "asc", ddlSubReasion, "Description", "LookupCode", "Select");
                ddlSubReasion.SelectedValue = hdnSubReasion.Value;
            }
            else if (hdnRejectReasion.Value == "2")
            {
                ddlSubReasion.Visible = true;
                objComman.BindDLL("mstLookup", "LookupCode,Description", "LookupFlag='SR2'", "LookupCode", "asc", ddlSubReasion, "Description", "LookupCode", "Select");
                ddlSubReasion.SelectedValue = hdnSubReasion.Value;
            }
            else
            {
                ddlSubReasion.Visible = false;
            }

        }
    }

    protected void LnkBtnBlock_OnClickNew(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string UniqueChildCode = (gvr.FindControl("lblCUniqueChildCode") as Label).Text;
        //Label lblStatus = (Label)gvr.FindControl("lblStatus");
        //Label lblSchool = (Label)gvr.FindControl("lblSchool");
        Label lblSchoolCode = (Label)gvr.FindControl("lblSchoolCode");
        Label lblDisecode = (Label)gvr.FindControl("lblDisecode");
        DropDownList ll = (DropDownList)gvr.FindControl("ddLRejectReasion");
        if (Convert.ToInt32(ll.SelectedValue) == 3)
        {
            Session["UnquieId"] = UniqueChildCode;
            Session["Disecode"] = lblDisecode.Text;
            //Session["SchoolName"] = lblSchool.Text;


            //if (ddlVillage.SelectedIndex > 0)
            //{
            //    Session["Villageame"] = ddlVillage.SelectedItem.Text;
            //}
            Session["mYear"] = ddlYear.SelectedValue;
            //Session["EnStatus"] = lblStatus.Text;

            string strQry = "select ManagementType,WorkingStatus,SchoolLevel,SchoolCodeID from mstSchool where SchoolCode='" + lblSchoolCode.Text + "'   ";


            DataTable dtMangment = objMain.LoadData(strQry);

            if (dtMangment.Rows.Count > 0)
            {
                Session["ManagementType"] = dtMangment.Rows[0]["ManagementType"].ToString();
                Session["SchoolLevel"] = dtMangment.Rows[0]["SchoolLevel"].ToString();
                Session["WorkingStatus"] = dtMangment.Rows[0]["WorkingStatus"].ToString();
                Session["SchoolCodeID"] = dtMangment.Rows[0]["SchoolCodeID"].ToString();

            }
            FillD2dData();
            MpexdrDistrictAdd.Show();
        }


    }
    public void FillClass()
    {

        conditions = "LookupFlag ='ECL'  and lookupcode not in(1,2,3,4,5)";

        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", dllClass, "Description", "LookupCode", "Select");



    }


    public void FillSocialCat()
    {
        conditions = "";
        conditions = "LookupFlag ='CAT' and Active=1";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlScat, "Description", "LookupCode", "Select");



    }

    public void FillENrollment()
    {
        conditions = "";
        conditions = "LookupFlag ='ES' and Active=1 and LookupCode in(1,2,3) ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlEnroll, "Description", "LookupCode", "Select");



    }

    public void FillEduStauts()
    {
        conditions = "";
        conditions = "LookupFlag ='EC' and Active=1";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlEduationStatus, "Description", "LookupCode", "Select");



    }
    public void FillD2dData()
    {
        string strQry = " Select [UniqueChildCode],VillagenameOther,mothername,SamgraID,ChildCode,mstSchool.name,tblEnrolment.[VillageCode],EnrolmentDate as SurvayDate,Class,AsOnDate,[Serial],[HouseNo],[Category],[ChildName] as ChildName,[FatherName] as FathersName,[Gender],[DOBAvailable],[DOB],[AgeAson],Type as EduationStatus,tblEnrolment.[SchoolCode],[EnrollCategory], mst5Village.PanchayatCode,mst5Village.BlockCode,mst5Village.DistrictCode,tblEnrolment.Status FROM (mst5Village INNER JOIN tblEnrolment ON mst5Village.VillageCode = tblEnrolment.VillageCode) left JOIN mstSchool ON tblEnrolment.SchoolCode = mstSchool.SchoolCode where UniqueChildCode='" + Session["UnquieId"].ToString() + "' ";
        DataTable dt = objMain.LoadData(strQry);
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                string encryptionKey = row["UniqueChildCode"].ToString();

                if (dt.Columns.Contains("ChildName") && row["ChildName"] != DBNull.Value)
                    row["ChildName"] = sqlInjection.DecryptMatchingWithSessionMasking(row["ChildName"].ToString(), "ChildName");

                if (dt.Columns.Contains("FathersName") && row["FathersName"] != DBNull.Value)
                    row["FathersName"] = sqlInjection.DecryptMatchingWithSessionMasking(row["FathersName"].ToString(), "FathersName");
                if (dt.Columns.Contains("mothername") && row["mothername"] != DBNull.Value)
                    row["mothername"] = sqlInjection.DecryptMatchingWithSessionMasking(row["mothername"].ToString(), "mothername");

                if (dt.Columns.Contains("DOB") && row["DOB"] != DBNull.Value)
                {
                    string encryptedDOB = row["DOB"].ToString();
                    string decryptedDOB = sqlInjection.DecryptMatchingWithSessionMasking(encryptedDOB, "DOB", false);

                    row["DOB"] = decryptedDOB;

                    sqlInjection.ProcessRowAgeAndDob(row, decryptedDOB);
                }
                if (dt.Columns.Contains("SamgraID") && row["SamgraID"] != DBNull.Value)
                    row["SamgraID"] = sqlInjection.DecryptMatchingWithSessionMasking(row["SamgraID"].ToString(), "SamgraID");
            }
        }

        if (dt.Rows.Count > 0)
        {
            if (Convert.ToString(Session["StateCode"]) == "23")
            {
                Div9.Visible = true;
            }
            else
            {
                Div9.Visible = false;
            }

            ddlGender.SelectedValue = dt.Rows[0]["Gender"].ToString();

            txtSrno.Text = dt.Rows[0]["Serial"].ToString();
            txtChildName.Text = dt.Rows[0]["ChildName"].ToString();
            txtFatherName.Text = dt.Rows[0]["FathersName"].ToString();
            txtMonthName.Text = dt.Rows[0]["mothername"].ToString();
            txtSamgra.Text = dt.Rows[0]["SamgraID"].ToString();

            //villagecode = dt.Rows[0]["VillageCode"].ToString();




            //DTPicker_DOB.Format = DateTimePickerFormat.Custom;
            //DTPicker_DOB.CustomFormat = "dd/MM/yyyy ";

            DateTime DOB = Convert.ToDateTime(dt.Rows[0]["DOB"].ToString());

            if (dt.Rows[0]["DOB"].ToString() == "01/01/1900 00:00:00")
            {
                txtDobDate.Text = "";
            }
            else
            {
                txtDobDate.Text = DOB.ToString("dd/MM/yyyy");
            }




            DateTime SurvayDate = Convert.ToDateTime(dt.Rows[0]["SurvayDate"].ToString());
            if (dt.Rows[0]["DOB"].ToString() == "01/01/1900 00:00:00")
            {
                txtBirth.Text = "";
            }
            else
            {
                txtBirth.Text = SurvayDate.ToString("dd/MM/yyyy");
            }




            ddlScat.SelectedValue = dt.Rows[0]["Category"].ToString();
            //ddlEduationStatus.SelectedValue = Convert.ToInt32(dt.Rows[0]["EnrollCategory"].ToString()).ToString();
            //ddlEnroll.SelectedValue = dt.Rows[0]["EduationStatus"].ToString();



            txtHHNo.Text = dt.Rows[0]["HouseNo"].ToString();
            dllClass.SelectedValue = dt.Rows[0]["Class"].ToString();
            //if (dt.Rows[0]["ReasonDO_NE"].ToString() == "0")
            //{
            //    txtReason.Text = "";

            //}
            //else
            //{
            //    txtReason.Text = dt.Rows[0]["ReasonDO_NE"].ToString();
            //}


            txtHHNo.Text = dt.Rows[0]["HouseNo"].ToString();

        }
    }
    protected void ddlAllResone_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlAllResone.SelectedValue == "1")
            {
                ddlAllResoneSub.Visible = true;
                objComman.BindDLL("mstLookup", "LookupCode,Description", "LookupFlag='SR1'", "LookupCode", "asc", ddlAllResoneSub, "Description", "LookupCode", "Select");
            }
            else if (ddlAllResone.SelectedValue == "2")
            {
                ddlAllResoneSub.Visible = true;
                objComman.BindDLL("mstLookup", "LookupCode,Description", "LookupFlag='SR2'", "LookupCode", "asc", ddlAllResoneSub, "Description", "LookupCode", "Select");
            }
            else
            {
                ddlAllResoneSub.Visible = false;
            }
            MpexdrPopUp.Show();
        }
        catch
        {

            throw;
        }
    }
    protected void btnAll_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < GVSealSign.Rows.Count; i++)
        {
            CheckBox chkReject = ((CheckBox)GVSealSign.Rows[i].FindControl("chkReject"));
            if (chkRejectAll.Checked == true)
            {
                DropDownList ddLRejectReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddLRejectReasion"));
                DropDownList ddlSubReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddlSubReasion"));
                if (ddlAllResone.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Reason ')</script>", false);
                    MpexdrPopUp.Show();
                    return;
                }
                if (ddlAllResone.SelectedValue == "1")
                {
                    if (ddlAllResoneSub.SelectedIndex <= 0)

                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Sub reason')</script>", false);
                        MpexdrPopUp.Show();
                        return;
                    }
                }
                if (ddlAllResone.SelectedValue == "2")
                {
                    if (ddlAllResoneSub.SelectedIndex <= 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Sub reason')</script>", false);
                        MpexdrPopUp.Show();
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < GVSealSign.Rows.Count; i++)
        {
            if (chkRejectAll.Checked == true)
            {
                string C_ID = GVSealSign.DataKeys[i]["UniqueChildCode"].ToString();

                CheckBox chkApprove = ((CheckBox)GVSealSign.Rows[i].FindControl("chkApprove"));
                CheckBox chkReject = ((CheckBox)GVSealSign.Rows[i].FindControl("chkReject"));
                DropDownList ddLRejectReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddLRejectReasion"));
                DropDownList ddlSubReasion = ((DropDownList)GVSealSign.Rows[i].FindControl("ddlSubReasion"));
                HiddenField hdnApprov = (HiddenField)GVSealSign.Rows[i].FindControl("hdnApprov");
                if (hdnApprov.Value == "1")
                {
                }
                else
                {
                    ddLRejectReasion.SelectedValue = ddlAllResone.SelectedValue;

                    if (ddLRejectReasion.SelectedValue == "1")
                    {
                        ddlSubReasion.Visible = true;
                        objComman.BindDLL("mstLookup", "LookupCode,Description", "LookupFlag='SR1'", "LookupCode", "asc", ddlSubReasion, "Description", "LookupCode", "Select");

                    }
                    else if (ddLRejectReasion.SelectedValue == "2")
                    {
                        ddlSubReasion.Visible = true;
                        objComman.BindDLL("mstLookup", "LookupCode,Description", "LookupFlag='SR2'", "LookupCode", "asc", ddlSubReasion, "Description", "LookupCode", "Select");

                    }

                    if (ddlAllResoneSub.SelectedIndex > 0)
                    {
                        ddlSubReasion.SelectedValue = ddlAllResoneSub.SelectedValue;
                    }
                    ddLRejectReasion.Visible = true;
                    ddlSubReasion.Visible = true;
                }
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
    protected void btSave_Click(object sender, EventArgs e)
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
        if (!ValidationAdd())
            return;
        SaveData();

    }


    public void SaveData()
    {


        string strUnique = "0";
        string HHNo = txtHHNo.Text.Trim();
        string ChildName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtChildName.Text.Trim());
        string FathersName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtFatherName.Text.Trim());
        string unquieKey = Session["UnquieId"].ToString();
        string encChildName = String.Empty, encFathersName = String.Empty, encChildDOB = String.Empty, encSamgra = String.Empty, encmothername = String.Empty; ;
        if (Session["UnquieId"].ToString().Length > 6)
        {
            encChildName = CryptoService.Encrypt(ChildName);
            encFathersName = CryptoService.Encrypt(FathersName);
            encSamgra = CryptoService.Encrypt(txtSamgra.Text);
            encmothername = CryptoService.Encrypt(txtMonthName.Text);

        }
        string strSerial = txtSrno.Text.Trim();

        string dllClasss = dllClass.SelectedValue;
        string Scat = ddlScat.SelectedValue.ToString();


        Int32 Gender = Convert.ToInt32(ddlGender.SelectedValue);


        Int32 DoAv = 1;

        //if (cmbGender == "2")
        //{
        //    Gender = 2;
        //}
        //else
        //{
        //    Gender = 1;
        //}

        DateTime DOB;
        DateTime AsDob;
        Int32 Age = 0;



        Int32 ymyear = Convert.ToInt32(Session["mYear"].ToString());
        string Adminision = txtBirth.Text;

        string[] b = Adminision.Split('/');
        string DateAdminision = b[2] + '-' + b[1] + '-' + b[0];

        DateTime DOBStudent = Convert.ToDateTime(txtDobDate.Text);
        DateTime dtason = DOBStudent;
        Age = ymyear - dtason.Year;

        DOB = DOBStudent;

        string[] c = txtDobDate.Text.Split('/');
        string ChildDOB = c[2] + '-' + c[1] + '-' + c[0];
        encChildDOB = CryptoService.Encrypt(ChildDOB);

        string DOB1 = DOBStudent.ToString();
        string[] words = DOB1.Split('/');
        Int32 iyear = Convert.ToInt32(dtason.Year) + Age;
        string lYear = iyear.ToString();
        AsDob = Convert.ToDateTime(DateTime.Today); ;
        // AsDob = words[2] + '-' + words[1] + '-' + iyear.ToString();
        string StudentTSInsertQuery = "";


        string Fullfilename = "";

        //if (FileuploadAttach.PostedFile != null && FileuploadAttach.PostedFile.FileName != "")
        //{
        //    string ext = System.IO.Path.GetExtension(FileuploadAttach.PostedFile.FileName).ToLower();
        //    if (FileuploadAttach.PostedFile.ContentLength < 102400)
        //    {
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Image size must be less than 100kb')</script>", false);
        //        return;
        //    }
        //    if (ext != ".jpeg" && ext != ".jpg" && ext != ".png" && ext != ".gif")
        //    {
        //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Images')</script>", false);
        //        return;
        //    }
        //    string exten = Path.GetExtension(FileuploadAttach.PostedFile.FileName);
        //    Fullfilename = "" + "IMG" + "_" + Convert.ToString(Session["Disecode"]) + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;
        //}

        //string sFileDir = Comman.GetImagePath("TabletImagePath") +"/"

        //if (FileuploadAttach.PostedFile != null && FileuploadAttach.PostedFile.FileName != "")
        //{
        //    string exten = Path.GetExtension(FileuploadAttach.PostedFile.FileName);
        //    // string Imagefile1 = "LeaveDoc" + "_" + Convert.ToString(Session["EMP_ID"]) + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;

        //    //create directory

        //    if (Directory.Exists(sFileDir)) { }
        //    else { System.IO.Directory.CreateDirectory(sFileDir); }

        //    //======update the file =====\\

        //    if (System.IO.File.Exists(sFileDir + "\\" + Fullfilename))
        //    {
        //        try { System.IO.File.Delete(sFileDir + "\\" + Fullfilename); }
        //        catch
        //        {

        //        }
        //    }
        //    FileuploadAttach.PostedFile.SaveAs(sFileDir + Fullfilename);

        //  }
        int icount = 0;
        if (Session["UnquieId"].ToString().Length > 6)
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
   {
                            //new SqlParameter("@MotherName", txtMonthName.Text),

                            //new SqlParameter("@SamgraID", txtSamgra.Text),
                              new SqlParameter("@MotherName", encmothername),

                            new SqlParameter("@SamgraID", encSamgra),
                            new SqlParameter("@Category",
                                Convert.ToInt32(ddlScat.SelectedValue)),

                            new SqlParameter("@Class",
                                Convert.ToInt32(dllClass.SelectedValue)),

                            new SqlParameter("@Serial", strSerial),

                            //new SqlParameter("@ChildName", ChildName),

                            //new SqlParameter("@FatherName", FathersName),
                              new SqlParameter("@ChildName", encChildName),

                            new SqlParameter("@FatherName", encFathersName),

                            new SqlParameter("@Gender",
                                Convert.ToInt32(Gender)),

                            new SqlParameter("@EnrolmentDate",
                                Convert.ToDateTime(DateAdminision)),

                            new SqlParameter("@DOBAvailable",
                                Convert.ToBoolean(DoAv)),

                            //new SqlParameter("@DOB",
                            //    Convert.ToDateTime(ChildDOB)),
                           new SqlParameter("@DOB",
                                Convert.ToDateTime(encChildDOB)),

                            new SqlParameter("@AgeAson",
                                Convert.ToInt32(Age)),

                            new SqlParameter("@AsOnDate",
                                Convert.ToDateTime(AsDob)),

                            new SqlParameter("@ModifyDate",
                                DateTime.Now),

                            new SqlParameter("@ModifyBy",
                                Convert.ToString(Session["username"])),

                            new SqlParameter("@HouseNo",
                                txtHHNo.Text.Trim()),

                            new SqlParameter("@UniqueChildCode",
                                Convert.ToString(Session["UnquieId"]))
   };

            icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "USP_Update_tblEnrolment", cmdParameters);

            string D2StudentTSInsertQuery = "";
            if (Convert.ToString(Session["EnStatus"]) == "1")
            {
                SqlParameter[] cmdParameters1 = new SqlParameter[]
  {
    new SqlParameter("@HHNo",
        txtHHNo.Text.Trim()),

    new SqlParameter("@SurvayDate",
        Convert.ToDateTime(DateAdminision)),

    new SqlParameter("@SocialCategory",
        Convert.ToInt32(ddlScat.SelectedValue)),

    //new SqlParameter("@ChildName",
    //    ChildName),

    //new SqlParameter("@FathersName",
    //    FathersName),
     new SqlParameter("@ChildName",
        encChildName),

    new SqlParameter("@FathersName",
        encFathersName),

    new SqlParameter("@Gender",
        Convert.ToInt32(Gender)),

    //new SqlParameter("@DOB",
    //    Convert.ToDateTime(ChildDOB)),
     new SqlParameter("@DOB",
        Convert.ToDateTime(encChildDOB)),

    new SqlParameter("@AgeAson",
        Convert.ToInt32(Age)),

    new SqlParameter("@DoChild",
        Convert.ToInt32(dllClass.SelectedValue)),

    new SqlParameter("@ModifyDate",
        DateTime.Now),

    new SqlParameter("@ModifyBy",
        Convert.ToString(Session["username"])),

    new SqlParameter("@UniqueCode",
        Convert.ToString(Session["UnquieId"]))
  };

                icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "USP_Update_tblDTD", cmdParameters1);



                if (icount > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);

                    txtChildName.Text = "";
                    txtFatherName.Text = "";
                    txtHHNo.Text = "";
                    txtSrno.Text = "";
                    txtSamgra.Text = "";


                    txtHHNo.Focus();
                    txtBirth.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    ddlEduationStatus.SelectedIndex = 0;
                    //  Response.Write("<script>window.close();</" + "script>");
                    string strQry = "";
                    conditions = "";
                    conditions = " v.StateCode='" + ddlState.SelectedValue.ToString() + "'";

                    if (ddlDistrict.SelectedIndex > 0)
                    {
                        conditions = conditions + " and v.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "' ";
                    }

                    if (ddlBlock.SelectedIndex > 0)
                    {
                        conditions = conditions + " and v.BlockCode='" + ddlBlock.SelectedValue.ToString() + "' ";
                    }
                    if (ddlVillage.SelectedIndex > 0)
                    {
                        conditions = conditions + " and v.ClusterCode='" + ddlVillage.SelectedValue.ToString() + "' ";
                    }
                    if (ddlSchool.SelectedIndex > 0)
                    {
                        conditions = conditions + " and tblEnrolment.SchoolCode='" + ddlSchool.SelectedValue.ToString() + "' ";
                    }
                    if (ddlFc.SelectedIndex > 0)
                    {
                        conditions = conditions + " and tblEnrolment.CreateBy='" + ddlFc.SelectedValue.ToString() + "' ";
                    }
                    if (ddlVillageNew.SelectedIndex > 0)
                    {
                        conditions = conditions + " and v.Villagecode='" + ddlVillageNew.SelectedValue.ToString() + "' ";
                    }
                    conditions = conditions + " and SealFormImage='" + lblDisplay.Text + "' ";

                    SqlParameter[] parm1 = new SqlParameter[]
                {

               new SqlParameter("@Con",  conditions),
                 new SqlParameter("@Flag",  1),
                };


                    DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[SP_GET_Seal_Sign]", parm1);
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
                    GVSealSign.DataSource = dt;
                    GVSealSign.DataBind();
                    //  LoadData();

                }
                else
                {

                }
            }

        }
    }
    public bool CheckAllphanumeric(string txtHhno)
    {


        System.Text.RegularExpressions.Regex objAlphaNumericPattern = new System.Text.RegularExpressions.Regex("^(?=.*[0-9]+.*)");
        return !objAlphaNumericPattern.IsMatch(txtHhno);
    }
    private Boolean ValidationAdd()
    {
        try
        {



            if (Session["UnquieId"].ToString().Length > 6)
            { }
            else
            {
                string strQry = " Select [Serial] FROM tblEnrolment where [Serial]='" + txtSrno.Text.ToString() + "' and  SchoolCode ='" + Session["Schoolid"].ToString() + "'";
                DataTable dt = objMain.LoadData(strQry);

                if (dt.Rows.Count > 0)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('This  Serial No already exists in Database')</script>", false);
                    MpexdrDistrictAdd.Show();
                    return false;
                }

            }

            if (txtSrno.Text.Trim() == "")
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Serial No')</script>", false);
                MpexdrDistrictAdd.Show();
                return false;
            }
            bool Alf = CheckAllphanumeric(txtSrno.Text);
            if (Alf == true)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter at least one number in SR')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }
            if (txtChildName.Text.Trim() == "")
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Child name')</script>", false);
                MpexdrDistrictAdd.Show();
                return false;
            }

            else if (txtFatherName.Text.Trim() == "")
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Father name')</script>", false);
                MpexdrDistrictAdd.Show();
                return false;
            }
            else if (txtMonthName.Text.Trim() == "")
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Mother name')</script>", false);
                MpexdrDistrictAdd.Show();
                return false;
            }
            else if (dllClass.SelectedIndex <= 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Class')</script>", false);
                MpexdrDistrictAdd.Show();
                return false;
            }

            else if (ddlScat.SelectedIndex <= 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select SocialCategory')</script>", false);
                MpexdrDistrictAdd.Show();
                return false;
            }
            else if (ddlGender.SelectedIndex <= 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Gender')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }
            if (ddlState.SelectedValue == "23")
            {
                if (txtSamgra.Text.Trim() == "")
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter sangram ID')</script>", false);
                    MpexdrDistrictAdd.Show();
                    return false;
                }
            }
            if (ddlState.SelectedValue == "23")
            {
                if (txtSamgra.Text.Trim().Length < 8)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('sangram ID should be 8 or 9 digits')</script>", false);
                    MpexdrDistrictAdd.Show();
                    return false;
                }
            }
            if (txtBirth.Text.Trim() == "")
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Admission Date')</script>", false);
                MpexdrDistrictAdd.Show();
                return false;
            }

            if (txtDobDate.Text.Trim() == "")
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter DOB')</script>", false);
                MpexdrDistrictAdd.Show();
                return false;
            }
            DateTime AdmissionDate = Convert.ToDateTime(txtBirth.Text);
            Int32 fDate = ((AdmissionDate.Year) * 10000 + (AdmissionDate.Month) * 100 + (AdmissionDate.Day));

            Int32 cFyear = Convert.ToInt32(ddlYear.SelectedValue);

            Int32 cYear = ((cFyear) * 10000 + (04) * 100 + (01));

            DateTime DOB;
            DateTime AsDob;
            Int32 Age = 0;
            DateTime DobDateQ1 = Convert.ToDateTime(txtDobDate.Text);

            string DateSarveyDate = txtBirth.Text;
            string[] b = DateSarveyDate.Split('/');

            string DateB = txtDobDate.Text;
            string[] a = txtDobDate.Text.Split('/');
            string BithDate = a[2] + '-' + a[1] + '-' + a[0];



            // Age = Convert.ToInt32(b[2]) - Convert.ToInt32(a[2]);
            DOB = Convert.ToDateTime(a[2] + '-' + a[1] + '-' + a[0]);

            string strQry66 = "select dbo.udfDateDiffinYrMonDay('" + DobDateQ1.ToString("yyyy-MM-dd") + "','" + AdmissionDate.ToString("yyyy-MM-dd") + "') as age ";
            DataTable dtDate = objMain.LoadData(strQry66);
            if (dtDate.Rows.Count > 0)
            {
                Age = Convert.ToInt32(dtDate.Rows[0]["age"]);
            }

            Int32 iyear = Convert.ToInt32(a[2]) + Age;
            string dyear = iyear.ToString();

            string strQry5 = "select * from mstLookup where LookupFlag='AV' and Description='" + ddlDistrict.SelectedValue + "' ";
            DataTable dtAge = objMain.LoadData(strQry5);
            int FAge = 0;
            int ToAge = 0;
            if (dtAge.Rows.Count > 0)
            {
                FAge = Convert.ToInt32(dtAge.Rows[0]["LookupCode"]);

                ToAge = Convert.ToInt32(dtAge.Rows[0]["SeqNo"]);
            }
            if (Age < FAge)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between " + FAge + " and  " + ToAge + "  years')</script>", false);
                MpexdrDistrict.Show();
                return false;

            }
            if (Age > ToAge)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between  " + FAge + " and  " + ToAge + "  years')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }
            //if (Convert.ToDateTime(txtBirth.Text.ToString()) <= Convert.ToDateTime(txtDobDate.Text))
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Date of admission is subsequent to DOB')</script>", false);
            //    MpexdrDistrict.Show();
            //    return false;
            //}

            //if (Age < 5)
            //{

            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 5 and 16 years')</script>", false);
            //    MpexdrDistrictAdd.Show();
            //    return false;

            //}
            //if (Age > 14)
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 5 and 14 years')</script>", false);
            //    MpexdrDistrictAdd.Show();
            //    return false;
            //}

            //if (Convert.ToInt32(ddlYear.SelectedValue) > Convert.ToInt32(AdmissionDate.Year))
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure enrollment date should be in current year')</script>", false);
            //    MpexdrDistrictAdd.Show();
            //    //dDOB.Style.BackColor = Color.Red;
            //    return false;
            //}
            string strQr1y5 = " Select SchoolCodeID from mstSchool where schoolcode in(select schoolcode from tblEnrolment where UniqueChildCode ='" + Session["UnquieId"].ToString() + "')";
            DataTable dtschool = objMain.LoadData(strQr1y5);
            if (dtschool.Rows[0]["SchoolCodeID"].ToString() == "0")
            {
            }
            else
            {

                string strQr1y = " Select mstClassValdation.[Operator], mstClassValdation.[Class], mstLookup.SeqNo AS SeqNoCode FROM mstClassValdation, mstLookup where LookupFlag ='ECL' and LookupCode=" + dllClass.SelectedValue + " and  [Age]=" + Age + " ";
                DataTable dtNew = objMain.LoadData(strQr1y);


                if (Convert.ToInt32(dllClass.SelectedValue) <= 5)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select valid class')</script>", false);

                    MpexdrDistrictAdd.Show();
                    //dDOB.Style.BackColor = Color.Red;
                    return false;

                }
                else
                {
                    if (dtNew.Rows.Count > 0)
                    {
                        Int32 Iclass = Convert.ToInt32(dtNew.Rows[0]["Class"].ToString());
                        Int32 SeqNoCode = Convert.ToInt32(dtNew.Rows[0]["SeqNoCode"].ToString());
                        string Op = dtNew.Rows[0]["Operator"].ToString();
                        if (Convert.ToInt32(Iclass) < SeqNoCode)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('You can  select max " + Iclass + " class')</script>", false);
                            MpexdrDistrictAdd.Show();
                            return false;
                        }
                    }
                }

                if (Session["SchoolLevel"].ToString() == "5")
                {
                    if (Convert.ToInt32(ddlGender.SelectedValue) == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('You can add only female child in KGBV School')</script>", false);
                        MpexdrDistrictAdd.Show();
                        return false;
                    }
                }


                if (Convert.ToInt32(dllClass.SelectedValue) <= 5)
                {
                }
                else
                {

                    string strQr1yC = " Select  mstLookup.SeqNo AS SeqNoCode FROM mstLookup where LookupFlag ='ECL' and LookupCode=" + dllClass.SelectedValue + " ";
                    DataTable dtNewC = objMain.LoadData(strQr1yC);
                    Int32 MainClass = Convert.ToInt32(dtNewC.Rows[0]["SeqNoCode"].ToString());
                    if (Session["SchoolLevel"].ToString() == "5")
                    {
                        string strQr1y1 = " Select MaxClass FROM mstClassValdation where  SchoolType=" + Session["SchoolLevel"].ToString() + " ";
                        DataTable dtNew1 = objMain.LoadData(strQr1y1);
                        if (MainClass < 6)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Class 6 to 12 School')</script>", false);
                            MpexdrDistrictAdd.Show();
                            return false;
                        }
                        else
                        {
                            Int32 MaxClass = Convert.ToInt32(dtNew1.Rows[0]["MaxClass"].ToString());
                            if (MainClass > MaxClass)
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Class 6 to 12 School')</script>", false);
                                MpexdrDistrictAdd.Show();
                                return false;
                            }
                        }
                    }
                    else
                    {
                        string strQr1y1 = " Select MaxClass FROM mstClassValdation where  SchoolType=" + Session["SchoolLevel"].ToString() + " ";
                        DataTable dtNew1 = objMain.LoadData(strQr1y1);
                        if (dtNew1.Rows.Count > 0)
                        {
                            Int32 MaxClass = Convert.ToInt32(dtNew1.Rows[0]["MaxClass"].ToString());
                            if (MainClass > MaxClass)
                            {
                                if (Session["SchoolLevel"].ToString() == "1")
                                {
                                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Class 1 to 5')</script>", false);
                                    MpexdrDistrictAdd.Show();
                                    return false;
                                }
                                else if (Session["SchoolLevel"].ToString() == "2" || Session["SchoolLevel"].ToString() == "7")
                                {
                                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Class 1 to 8')</script>", false);
                                    MpexdrDistrictAdd.Show();
                                    return false;
                                }
                                else if (Session["SchoolLevel"].ToString() == "3")
                                {
                                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Class 1 to 10')</script>", false);

                                    MpexdrDistrictAdd.Show();
                                    return false;
                                }
                                else if (Session["SchoolLevel"].ToString() == "7")
                                {
                                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Class 1 to 8')</script>", false);

                                    MpexdrDistrict.Show();
                                    return false;
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Class 1 to 12 ')</script>", false);
                                    MpexdrDistrictAdd.Show();
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select valid School')</script>", false);
                            MpexdrDistrictAdd.Show();

                            //dDOB.Style.BackColor = Color.Red;
                            return false;
                        }
                    }
                }
            }
            DateTime date1 = Convert.ToDateTime(txtDobDate.Text);
            DateTime date2 = Convert.ToDateTime(txtBirth.Text);
            // int daysDiff = ((TimeSpan)(date2 - date1)).Days;
            TimeSpan timeSpan = date2 - date1;

            decimal finalResult = 0;
            finalResult = Convert.ToDecimal(timeSpan.TotalDays / 365);
            if (finalResult < 3)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Date of Birth and Date of Enrollment difference of 3 years')</script>", false);
                MpexdrDistrictAdd.Show();
                //dDOB.Style.BackColor = Color.Red;
                return false;
            }


            return true;

        }
        catch
        {
            // MessageBox.Show(ex.Message, "EG", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }
    }

}