using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class FrmSealSignRemoveDuplicate : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    SqlHelper Sh = new SqlHelper();
    string conditions = "";
    string statecode = string.Empty, Clustercode = string.Empty, Distcode = string.Empty, blockcode = string.Empty, villagecode = string.Empty, dbname = "", FormName = string.Empty;
    int RowNumber = 0;
    SqlInjection sqlInjection = new SqlInjection();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                LoadYear();
                LoadUserLeavel();
                ddlYear.SelectedIndex = 1;
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
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

                ddlPanchayat.SelectedValue = Convert.ToString(a[1].ToString());
                ddlPanchayat_SelectedIndexChanged(ddlPanchayat, null);

            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/FrmEnrollmentDuplicateMatching.aspx");
    }
    #region Button click event
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        try
        {
            LoadReport();
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void btnSubmitToBO_Click(object sender, EventArgs e)
    {
        if (lblUniqueCode.Text.Length > 0)
        {
            int Ret = Insert_Update(lblUniqueCode.Text, Convert.ToString(Session["username"]), 5);
            if (Ret > 0)
            {
                LoadReport();
                gvD2d.DataSource = null;
                gvD2d.DataBind();
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Submit sucessfully')</script>", false);
                return;
            }
        }

    }
    protected void ImgOutDur_Click(object sender, EventArgs e)
    {
        DataTable Ds_gvReport1 = Session["OutofDoorD2d"] as DataTable;
        DataRow[] drArr1 = null;
        string StrRo = "RO";
        string StrMo = "MO";
        //drArr1 = Ds_gvReport1.Select("U ='" + StrRo + "' or K ='" + StrMo + "'  ");
        //if (drArr1.Length > 0)
        //{
        //    foreach (DataRow row in drArr1)
        //    {
        //        Ds_gvReport1.Rows.Remove(row);
        //    }

        //    Ds_gvReport1.AcceptChanges();
        //}
        //Session["OutofDoorD2d"] = Ds_gvReport1;
        if (ddl_MatchByOut.SelectedIndex > 0 && ddl_MatchByOut.SelectedValue == "1")
        {
            DataTable Ds_gvReport = Session["OutofDoorD2d"] as DataTable;
            DataView DV = Ds_gvReport.DefaultView;
            DV.RowFilter = string.Format("VillageName LIKE '%{0}%'", Txt_VillageOUT.Text);
            gvReport.DataSource = null;
            gvReport.DataBind();
            gvReport.DataSource = DV;
            gvReport.DataBind();
            Session["SearchOutOfD2d"] = DV;
        }
        else if (ddl_MatchByOut.SelectedIndex > 0 && ddl_MatchByOut.SelectedValue == "2")
        {
            DataTable Ds_gvReport = Session["OutofDoorD2d"] as DataTable;
            DataView DV = Ds_gvReport.DefaultView;
            DV.RowFilter = string.Format("House LIKE '%{0}%'", Txt_VillageOUT.Text);
            gvReport.DataSource = null;
            gvReport.DataBind();
            gvReport.DataSource = DV;
            gvReport.DataBind();
            Session["SearchOutOfD2d"] = DV;
        }
        else if (ddl_MatchByOut.SelectedIndex > 0 && ddl_MatchByOut.SelectedValue == "3")
        {
            DataTable Ds_gvReport = Session["OutofDoorD2d"] as DataTable;
            DataView DV = Ds_gvReport.DefaultView;
            DV.RowFilter = string.Format("ChildName LIKE '%{0}%'", Txt_VillageOUT.Text);
            gvReport.DataSource = null;
            gvReport.DataBind();
            gvReport.DataSource = DV;
            gvReport.DataBind();
            Session["SearchOutOfD2d"] = DV;
        }
        else if (ddl_MatchByOut.SelectedIndex > 0 && ddl_MatchByOut.SelectedValue == "4")
        {
            DataTable Ds_gvReport = Session["OutofDoorD2d"] as DataTable;
            DataView DV = Ds_gvReport.DefaultView;
            DV.RowFilter = string.Format("FathersName LIKE '%{0}%'", Txt_VillageOUT.Text);
            gvReport.DataSource = null;
            gvReport.DataBind();
            gvReport.DataSource = DV;
            gvReport.DataBind();
            Session["SearchOutOfD2d"] = DV;
        }
        else if (ddl_MatchByOut.SelectedIndex > 0 && ddl_MatchByOut.SelectedValue == "5")
        {
            DataTable Ds_gvReport = Session["OutofDoorD2d"] as DataTable;
            DataView DV = Ds_gvReport.DefaultView;
            DV.RowFilter = string.Format("UniqueId LIKE '%{0}%'", Txt_VillageOUT.Text);
            gvReport.DataSource = null;
            gvReport.DataBind();
            gvReport.DataSource = DV;
            gvReport.DataBind();
            Session["SearchOutOfD2d"] = DV;
        }
        else
        {
            DataTable Ds_gvReport = Session["OutofDoorD2d"] as DataTable;
            DataView DV = Ds_gvReport.DefaultView;
            DV.RowFilter = string.Format("FathersName LIKE '%{0}%'", Txt_VillageOUT.Text);
            gvReport.DataSource = DV;
            gvReport.DataBind();
            Session["SearchOutOfD2d"] = DV;

        }
    }
    protected void IMG_DTDSerch_Click(object sender, EventArgs e)
    {
        DataTable Ds_gvReport1 = Session["D2d"] as DataTable;
        //DataRow[] drArr1 = null;
        //string StrRo = "RD";
        //string StrMo = "MD";
        //drArr1 = Ds_gvReport1.Select("TempId ='" + StrMo + "' or K ='" + StrRo + "'  ");
        //if (drArr1.Length > 0)
        //{
        //    foreach (DataRow row in drArr1)
        //    {
        //        Ds_gvReport1.Rows.Remove(row);
        //    }

        //    Ds_gvReport1.AcceptChanges();
        //}

        //Session["D2d"] = Ds_gvReport1;
        if (ddl_MatchDTD.SelectedIndex > 0 && ddl_MatchDTD.SelectedValue == "1")
        {
            DataTable Ds_gvD2d = Session["D2d"] as DataTable;
            DataView DV = Ds_gvD2d.DefaultView;
            DV.RowFilter = string.Format("VillageName LIKE '%{0}%'", Txt_VillageDTD.Text);
            gvD2d.DataSource = null;
            gvD2d.DataBind();
            gvD2d.DataSource = DV;
            gvD2d.DataBind();
            Session["SearchD2d"] = DV;
        }
        else if (ddl_MatchDTD.SelectedIndex > 0 && ddl_MatchDTD.SelectedValue == "2")
        {
            DataTable Ds_gvD2d = Session["D2d"] as DataTable;
            DataView DV = Ds_gvD2d.DefaultView;
            DV.RowFilter = string.Format("House LIKE '%{0}%'", Txt_VillageDTD.Text);
            gvD2d.DataSource = null;
            gvD2d.DataBind();
            gvD2d.DataSource = DV;
            gvD2d.DataBind();
            Session["SearchD2d"] = DV;
        }
        else if (ddl_MatchDTD.SelectedIndex > 0 && ddl_MatchDTD.SelectedValue == "3")
        {
            DataTable Ds_gvD2d = Session["D2d"] as DataTable;
            DataView DV = Ds_gvD2d.DefaultView;
            DV.RowFilter = string.Format("ChildName LIKE '%{0}%'", Txt_VillageDTD.Text);
            gvD2d.DataSource = null;
            gvD2d.DataBind();
            gvD2d.DataSource = DV;
            gvD2d.DataBind();
            Session["SearchD2d"] = DV;
        }
        else if (ddl_MatchDTD.SelectedIndex > 0 && ddl_MatchDTD.SelectedValue == "4")
        {
            DataTable Ds_gvD2d = Session["D2d"] as DataTable;
            DataView DV = Ds_gvD2d.DefaultView;
            DV.RowFilter = string.Format("FathersName LIKE '%{0}%'", Txt_VillageDTD.Text);
            gvD2d.DataSource = null;
            gvD2d.DataBind();
            gvD2d.DataSource = DV;
            gvD2d.DataBind();
            Session["SearchD2d"] = DV;
        }
        else if (ddl_MatchDTD.SelectedIndex > 0 && ddl_MatchDTD.SelectedValue == "5")
        {
            DataTable Ds_gvD2d = Session["D2d"] as DataTable;
            DataView DV = Ds_gvD2d.DefaultView;
            DV.RowFilter = string.Format("UniqueId LIKE '%{0}%'", Txt_VillageDTD.Text);
            gvD2d.DataSource = null;
            gvD2d.DataBind();
            gvD2d.DataSource = DV;
            gvD2d.DataBind();
            Session["SearchD2d"] = DV;
        }
        else
        {
            DataTable Ds_gvD2d = Session["D2d"] as DataTable;
            DataView DV = Ds_gvD2d.DefaultView;
            DV.RowFilter = string.Format("FathersName LIKE '%{0}%'", Txt_VillageDTD.Text);
            gvD2d.DataSource = DV;
            gvD2d.DataBind();
            Session["SearchD2d"] = DV;
        }

    }
    protected void btnMatch_Click(object sender, EventArgs e)
    {
        int indcount1 = 0, indD2d = 0;
        if (lblUniqueCode.Text.Length > 0)
        {
            MatchData(indcount1, indD2d);

        }
    }
    protected void MatchData(int indcount2, int indD2d)
    {
        string UniqueIDLeft = "", UniqueIDRight = "";
        if (lblUniqueCode.Text.Length > 0)
        {
            int Ret = Insert_Update(lblUniqueCode.Text, Convert.ToString(Session["username"]), 2);
            if (Ret > 0)
            {
                LoadReport();
                gvD2d.DataSource = null;
                gvD2d.DataBind();
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Submit sucessfully')</script>", false);
                return;
            }
        }

    }
    private int Insert_Update(string UniqueIDLeft, string UniqueIDRight, int Flag)
    {
        SqlConnection dbSqlconnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            dbSqlconnection.Open();
            using (SqlCommand dbSqlCommand = (SqlCommand)dbSqlconnection.CreateCommand())
            {
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = "SP_Seal_Sign_Specification_Update";
                dbSqlCommand.Parameters.AddWithValue("@UniqueCodeE", UniqueIDLeft);
                dbSqlCommand.Parameters.AddWithValue("@UniqueCodeD", UniqueIDRight);
                dbSqlCommand.Parameters.AddWithValue("@Flag", Flag);
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
    protected void btnRest_Click(object sender, EventArgs e)
    {
    }

    protected void GVMain_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        int row1 = int.Parse(e.CommandArgument.ToString()); // commandargument is same as row index

        if (e.CommandName == "GVUIO")
        {
            int iIndex = Convert.ToInt32(e.CommandArgument);
            lblUniqueCode.Text = gvReport.DataKeys[iIndex]["EnrollCode"].ToString();
            // FillControls(TBCode);

            string FristCon = "";
            FristCon = FristCon + " ms.EnrollmentUniqueCode='" + lblUniqueCode.Text + "' ";
            DataTable dt2 = objMain.EnrollmentRemoveRightGrid(FristCon);
            if (dt2 != null && dt2.Rows.Count > 0)
            {
                foreach (DataRow rows in dt2.Rows)
                {
                    string encryptionKey = rows["UniqueChildCode"].ToString();

                    if (dt2.Columns.Contains("ChildName") && rows["ChildName"] != DBNull.Value)
                        rows["ChildName"] = sqlInjection.DecryptMatchingWithSessionMasking(rows["ChildName"].ToString(), "ChildName");

                    if (dt2.Columns.Contains("FathersName") && rows["FathersName"] != DBNull.Value)
                        rows["FathersName"] = sqlInjection.DecryptMatchingWithSessionMasking(rows["FathersName"].ToString(), "FathersName");

                }
            }
            if (dt2.Rows.Count > 0)
            {

                gvD2d.Visible = true;
                IMG_DTDSerch.Enabled = true;
                dt2.DefaultView.Sort = "ChildName asc,fathersName asc";
                gvD2d.DataSource = dt2.DefaultView.ToTable();
                gvD2d.DataBind();
                Session["D2d"] = dt2;
            }
            else
            {
                gvD2d.DataSource = null;
                gvD2d.DataBind();
            }

            for (int i = 0; i < gvReport.Rows.Count; i++)
            {
                GridViewRow RowD = gvReport.Rows[i];
                if (i % 2 == 0)
                {
                    RowD.BackColor = Color.White;
                }
                else
                {
                    RowD.BackColor = Color.FromArgb(245, 245, 245);
                }

            }
            GridViewRow row = gvReport.Rows[iIndex];
            row.BackColor = Color.FromArgb(255, 255, 0);
            gvReport.SelectedIndex = row1;
            gvReport.SelectedRow.Focus();
        }
    }
    protected void lnk_Onclick(object sender, EventArgs e)
    {

        CheckBox lnk = sender as CheckBox;
        GridViewRow row = (GridViewRow)lnk.NamingContainer;
        int indx = row.RowIndex;
        Label lblUniqueCode = (Label)gvReport.Rows[indx].FindControl("lblUniqueCode");

        int indcount1 = 0;
        foreach (GridViewRow Itemst in gvReport.Rows)
        {
            if (((CheckBox)Itemst.FindControl("Chk2")).Checked)
            {
                indcount1++;
                if (indcount1 > 1)
                {
                    ((CheckBox)Itemst.FindControl("Chk2")).Checked = false;
                }
            }
        }
        if (indcount1 > 1)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select single matching entry from OOD2D list')</script>", false);
            return;
        }
        string FristCon = "";
        FristCon = FristCon + " ms.EnrollmentUniqueCode='" + lblUniqueCode.Text + "' ";
        DataTable dt2 = objMain.EnrollmentRemoveRightGrid(FristCon);
        if (dt2 != null && dt2.Rows.Count > 0)
        {
            foreach (DataRow rows in dt2.Rows)
            {
                string encryptionKey = rows["UniqueChildCode"].ToString();

                if (dt2.Columns.Contains("ChildName") && rows["ChildName"] != DBNull.Value)
                    rows["ChildName"] = sqlInjection.DecryptMatchingWithSessionMasking(rows["ChildName"].ToString(), "ChildName");

                if (dt2.Columns.Contains("FathersName") && rows["FathersName"] != DBNull.Value)
                    rows["FathersName"] = sqlInjection.DecryptMatchingWithSessionMasking(rows["FathersName"].ToString(), "FathersName");

            }
        }
        if (dt2.Rows.Count > 0)
        {
            gvD2d.Visible = true;
            IMG_DTDSerch.Enabled = true;
            dt2.DefaultView.Sort = "ChildName asc,fathersName asc";
            gvD2d.DataSource = dt2.DefaultView.ToTable();
            gvD2d.DataBind();
            Session["D2d"] = dt2;
        }
        else
        {
            gvD2d.DataSource = null;
            gvD2d.DataBind();
        }
    }

    #endregion
    #region Fill Master Data
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
        if (Session["user_level_Role"].ToString() == "2")
        {
            ddlState_SelectedIndexChanged(ddlDistrict, null);
        }
        else
        {
            conditions = "";
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and Fyear= '" + ddlYear.SelectedItem.Text + "' ";
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");
            ddlDistrict.SelectedIndex = 1;
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
        }
    }
    public void Locking()
    {
        if (ddlYear.SelectedIndex > 0)
        {

            //ImageButton1.Enabled = true;
            //btnSumbit.Enabled = true;
            //btnMove.Enabled = true;
            if (Session["FinYear"].ToString() != ddlYear.SelectedItem.Text)
            {
                string strQry;
                strQry = "Select * from mstModuleLocking  where [FromName]='Menual Match' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "'";


                DataTable dtModel = objMain.LoadData(strQry);
                if (dtModel.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtModel.Rows[0]["LockMonth"].ToString()) < DateTime.Today.Month)
                    {
                        //ImageButton1.Enabled = false;
                        btnSumbit.Enabled = false;
                        btnMove.Enabled = false;

                    }

                }

            }
            btnSumbit.Enabled = true;
            btnMove.Enabled = true;
        }
    }
    public void FillCBState()
    {
        conditions = "";
        objComman.BindDLL(ViewState["TableName"].ToString(), "S.StateCode,dbo.TitleCase(upper(S.StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");
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
    public void FillCBBock()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        if (Session["user_level"].ToString() == "19")
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 and BlockCode in(  " + Session["BlockCode"].ToString() + " )";
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
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' ";
        objComman.BindDLL("mstcluster", "ClusterCode,dbo.TitleCase(upper(ClusterName)) as ClusterName", conditions, "ClusterName", "asc", ddlPanchayat, "ClusterName", "ClusterCode", "--Select--");
    }
    public void FillCVillage()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  ClusterCode='" + ddlPanchayat.SelectedValue + "'  ";
        string strQry = "  SELECT mst5Village.VillageCode, dbo.TitleCase(upper(VillageName)) as VillageName FROM mst5Village  where " + conditions + "  order by VillageName   ";
        DataTable dtVillage = objMain.LoadData(strQry);
        ddlVillage.DataSource = dtVillage;
        ddlVillage.DataTextField = "VillageName";
        ddlVillage.DataValueField = "VillageCode";
        ddlVillage.DataBind();

    }
    public string FilterCondition()
    {
        conditions = "";
        string Village = "";
        foreach (ListItem item in ddlVillage.Items)
        {
            if (item.Selected)
            {

                Village += "'" + item.Value + "'" + ",";
            }
        }
        if (Village.Length > 0)
        {
            Village = Village.Substring(0, Village.LastIndexOf(","));

        }
        if (ddlState.SelectedIndex > 0)
        {
            conditions += "  mst5Village.StateCode = '" + ddlState.SelectedValue + "' ";
        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " and mst5Village.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";
        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions += " and mst5Village.BlockCode = '" + ddlBlock.SelectedValue + "' ";
        }
        if (ddlPanchayat.SelectedIndex > 0)
        {
            conditions += " and mst5Village.ClusterCode= '" + ddlPanchayat.SelectedValue + "' ";
        }
        if (Village.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in( " + Village + ") ";
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += " and mst5Village.Fyear='" + ddlYear.SelectedItem.Text + "'";
        }
        return conditions;
    }
    public void LoadReport()
    {

        string FristCon = FilterCondition();
        DataTable dt = objMain.OutD2dEnrollmentRemoveLeftGrid(FristCon);
        foreach (DataRow row in dt.Rows)
        {
            string encryptionKey = row["Uniqueode"].ToString();

            if (dt.Columns.Contains("ChildName") && row["ChildName"] != DBNull.Value)
                row["ChildName"] = sqlInjection.DecryptMatchingWithSessionMasking(row["ChildName"].ToString(), "ChildName");

            if (dt.Columns.Contains("FathersName") && row["FathersName"] != DBNull.Value)
                row["FathersName"] = sqlInjection.DecryptMatchingWithSessionMasking(row["FathersName"].ToString(), "FathersName");
            //if (dt.Columns.Contains("MotherName") && row["MotherName"] != DBNull.Value)
            //    row["MotherName"] = sqlInjection.DecryptMatchingWithSessionMasking(row["MotherName"].ToString(), "MotherName");
            //if (dt.Columns.Contains("SamgraID") && row["SamgraID"] != DBNull.Value)
            //    row["SamgraID"] = sqlInjection.DecryptMatchingWithSessionMasking(row["SamgraID"].ToString(), "SamgraID");

            //if (dt.Columns.Contains("DOB") && row["DOB"] != DBNull.Value)
            //{
            //    string encryptedDOB = row["DOB"].ToString();
            //    string decryptedDOB = sqlInjection.DecryptMatchingWithSessionMasking(encryptedDOB, "DOB", false);

            //    row["DOB"] = decryptedDOB;

            //    sqlInjection.ProcessRowAgeAndDob(row, decryptedDOB);
            //}
        }
        if (dt.Rows.Count > 0)
        {
            gvReport.Visible = true;
            ImgOutDur.Enabled = true;
            dt.DefaultView.Sort = "ChildName asc,fathersName asc";
            gvReport.DataSource = dt.DefaultView.ToTable();
            gvReport.DataBind();
            gvD2d.DataSource = null;
            gvD2d.DataBind();
            Session["OutofDoorD2d"] = dt;
        }
        else
        {
            gvReport.DataSource = null;
            gvReport.DataBind();
            gvD2d.DataSource = null;
            gvD2d.DataBind();
        }

    }

    #endregion
    #region SelectedIndexChanged

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
        Locking();
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        Locking();
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
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    #endregion
}