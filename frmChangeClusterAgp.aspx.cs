using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using iTextSharp.text.pdf;
using iTextSharp.text;

public partial class frmChangeClusterAgp : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    string conditions = string.Empty, Flag = string.Empty;
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
                GVCluster.DataSource = null;
                GVCluster.DataBind();
                ValdateUserLavel();
                LinkButton1.Visible = false;
                LinkButton2.Visible = false;
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }

            ImageButton9.Attributes.Add("onclick", "javascript:return " + "confirm('Do you really want to create “Village Name” as cluster? ')");

        }

    }
    public void ValdateUserLavel()
    {

        string strQry = "";
        string Cond = "Module='AGP Master Update Module' ";
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

            btnDelete.Visible = false;
        }
        else
        {

            btnDelete.Visible = false;
        }

        if (vADD == true)
        {
            btnAdd.Enabled = true;
            btnsave.Enabled = true;
            //lblMain.Text = "School Information Campaign";
        }
        else
        {
            btnAdd.Enabled = false;
            btnsave.Enabled = false;
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
    
        objComman.BindDLLMasterTable("mstSchoolAgp", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            ddlState.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlDistrict, null);
            ddlBlock.Items.Clear();
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

            btnsave.Enabled = true;
            LinkButton1.Enabled = true;
            LinkButton2.Enabled = true;
            string strQry;

            strQry = "Select * from mstModuleLocking  where [FromName]='Agp Cluster' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "' ";


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



                    btnsave.Enabled = false;
                    LinkButton1.Enabled = false;
                    LinkButton2.Enabled = false;



                }
            }


        }
    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {

        if (ddlType.SelectedIndex <= 0)
        {
            
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Type')</script>", false);
            return;
        }
        if (ddlDistrict.SelectedIndex <= 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select District')</script>", false);
            return;
        }
        FillGrid();
    }
    protected void btnSavedd_Click(object sender, EventArgs e)
    {
        if (ddlDeleteBlock.SelectedIndex > 0)
        {
           
        }

    }
    protected void btnSaveClick_Click(object sender, EventArgs e)
    {
        ImageButton9.Attributes.Add("onclick", "javascript:return " + "confirm('Do you really want to create “Village Name” as cluster? ')");

        if (ddlCLusterVillage.SelectedIndex > 0)
        {
           
        }
    }

    protected void btnDeleteClick_Click(object sender, EventArgs e)
    {
      
    }
    public int Update_SchoolWorkingStatus(string SchoolCode, int WorkingStatus, int MangmentType, int GKP, int GKPLevel, int SchoolType, int BalType, int SchoolCampus,string Villagecode)
    {
        SqlConnection dbSqlconnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            dbSqlconnection.Open();
            using (SqlCommand dbSqlCommand = (SqlCommand)dbSqlconnection.CreateCommand())
            {
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = "[Update_School_WorkingStatusAgp]";
                dbSqlCommand.Parameters.AddWithValue("@SchoolCode", SchoolCode);
                dbSqlCommand.Parameters.AddWithValue("@WorkingStatus", WorkingStatus);
                dbSqlCommand.Parameters.AddWithValue("@MangmentType", MangmentType);
                dbSqlCommand.Parameters.AddWithValue("@GKP", GKP);
                dbSqlCommand.Parameters.AddWithValue("@GKPLevel", GKPLevel);
                dbSqlCommand.Parameters.AddWithValue("@SchoolType", SchoolType);
                dbSqlCommand.Parameters.AddWithValue("@BalType", BalType);
                dbSqlCommand.Parameters.AddWithValue("@SchoolCampus", SchoolCampus);
                dbSqlCommand.Parameters.AddWithValue("@Villagecode", Villagecode);
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
    public int Update_VillageClusterAgp(string VillageCode, string ClusterCode, string VillageGeography, string VillageOperational, string CBlVillage, string FunctionalStatus, string AGPStatus)
    {
        SqlConnection dbSqlconnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            dbSqlconnection.Open();
            using (SqlCommand dbSqlCommand = (SqlCommand)dbSqlconnection.CreateCommand())
            {
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = "[Update_Village_Block]";
                dbSqlCommand.Parameters.AddWithValue("@VillageCode", VillageCode);
                dbSqlCommand.Parameters.AddWithValue("@ClusterCode", ClusterCode);
                dbSqlCommand.Parameters.AddWithValue("@VillageGeography", VillageGeography);
                dbSqlCommand.Parameters.AddWithValue("@CBlVillage", CBlVillage);
                dbSqlCommand.Parameters.AddWithValue("@FunctionalStatus", FunctionalStatus);
                dbSqlCommand.Parameters.AddWithValue("@VillageGeographyOperational", VillageOperational);
                dbSqlCommand.Parameters.AddWithValue("@AGPStatus", AGPStatus);
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


    public int Update_VillageCluster(string VillageCode, string ClusterCode, string VillageGeography, string VillageOperational, string CBlVillage, string FunctionalStatus, string AGPStatus)
    {
        SqlConnection dbSqlconnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            dbSqlconnection.Open();
            using (SqlCommand dbSqlCommand = (SqlCommand)dbSqlconnection.CreateCommand())
            {
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = "[Update_Village_Cluster]";
                dbSqlCommand.Parameters.AddWithValue("@VillageCode", VillageCode);
                dbSqlCommand.Parameters.AddWithValue("@ClusterCode", ClusterCode);
                dbSqlCommand.Parameters.AddWithValue("@VillageGeography", VillageGeography);
                dbSqlCommand.Parameters.AddWithValue("@CBlVillage", CBlVillage);
                dbSqlCommand.Parameters.AddWithValue("@FunctionalStatus", FunctionalStatus);
                dbSqlCommand.Parameters.AddWithValue("@VillageGeographyOperational", VillageOperational);
                dbSqlCommand.Parameters.AddWithValue("@AGPStatus", AGPStatus);
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
        if (Session["GridViewData"] != null)
        {
            UpdateData();
            int ret = 0; 
            DataTable Dt = Session["GridViewData"] as DataTable;

            // DataRow[] dr = Dt.Select(Cond);
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(ddlType.SelectedValue)== 2)
                {
                    string SchoolCode = Dt.Rows[i]["SchoolCode"].ToString();
                    Int32 WorkingStatus =Convert.ToInt32(Dt.Rows[i]["WorkingStatus"].ToString());
                    Int32 Management = Convert.ToInt32(Dt.Rows[i]["Management"].ToString());
                    Int32 GKP =  Convert.ToInt32(Dt.Rows[i]["GKP"].ToString());;
                    Int32 GKPLevel = Convert.ToInt32(Dt.Rows[i]["GKPLevel"].ToString()); 
                    Int32 SchoolType = Convert.ToInt32(Dt.Rows[i]["SchoolType"].ToString()); 
                    Int32 BAlVal = Convert.ToInt32(Dt.Rows[i]["BAlVal"].ToString()); 
                    Int32 SchoolCampus = Convert.ToInt32(Dt.Rows[i]["SchoolCampus"].ToString());
                    string TempVillageCode = Convert.ToString(Dt.Rows[i]["TempVillageCode"].ToString());
                    
                    if (SchoolCampus == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select School campus')</script>", false);
                        return;
                    }

                    ret = Update_SchoolWorkingStatus(SchoolCode, WorkingStatus, Management, GKP, GKPLevel, SchoolType, BAlVal, SchoolCampus, TempVillageCode);
                   
                }
                if (Convert.ToInt32(ddlType.SelectedValue) == 1 || Convert.ToInt32(ddlType.SelectedValue) == 3 || Convert.ToInt32(ddlType.SelectedValue) == 4)
                {
                  
                    string VillageCode = Dt.Rows[i]["TempVillageCode"].ToString();
                    string ClusterCode = Dt.Rows[i]["ClusterCode"].ToString();
                    string VillageGeography = Dt.Rows[i]["VillageGeography"].ToString();
                    string VillageOperational = Dt.Rows[i]["VillageGeographyOperational"].ToString();

                    string CBlVillage = Dt.Rows[i]["CBlVillage"].ToString();
                    string FunctionalStatus = Dt.Rows[i]["FunctionalStatus"].ToString();
                    string AGPStatus = Dt.Rows[i]["TempBlockCode"].ToString();

                    ret = Update_VillageClusterAgp(VillageCode, ClusterCode, VillageGeography, VillageOperational, CBlVillage, FunctionalStatus, AGPStatus);

                }
            }

            if (ret > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
            }
        }
    }
   
    private int Update_AnnualExamStatus(string str, string UID, string p)
    {
        int iReturnValue = 0;
        try
        {
            iReturnValue = objComman.Update_AnnualExamStatus(str, UID,Flag);
        }
        catch (Exception exp)
        {

        }
        return iReturnValue;
    }


    public void LoadUserLeavel()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", " Statecode in(select distinct statecode from mst5VillageAgp where AGPStatus=1)", "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", "Statecode in(select distinct statecode from mst5VillageAgp where AGPStatus=1)", "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            ddlState.SelectedIndex = 1;
        }
        else
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", "Statecode in(select distinct statecode from mst5VillageAgp where AGPStatus=1)", "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            ddlState.SelectedIndex = 1;
        }


        if (Session["user_level_Role"].ToString() == "1")
        {
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "";
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and Fyear= '" + ddlYear.SelectedItem.Text + "' ";
            objComman.BindDLL("mst2DistrictAgp", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", "DistrictCode in(select distinct DistrictCode from mst5VillageAgp where AGPStatus=1)", "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

            ddlDistrict.SelectedIndex = 0;

            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3BlockAgp", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");
            
        }

        else
        {


            conditions = "";
            //conditions = "StateCode ='" + ddlState.SelectedValue + "'  and Fyear= '2019-2020' ";

            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and DistrictCode in(select distinct DistrictCode from mst5VillageAgp where AGPStatus=1) and Fyear= '" + ddlYear.SelectedItem.Text + "' ";
            objComman.BindDLL("mst2DistrictAgp", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

            string strQry;
            strQry = "Select * from mst2DistrictAgp where   DistrictCode in(" + Session["DistrictCode"].ToString() + ")";
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
            //objComman.BindDLL("mst3BlockAgp", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }





    }
    protected void btnAddBlock_Click(object sender, EventArgs e)
    {
        
    }
    protected void ddlAdminBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strQry5 = " select  ISNULL( max(ISNULL(SerialNo,0)),0) +1 as SerialNo from [mst3BlockAgp]  where AdminBlock ='" + ddlAdminBlock.SelectedValue + "' ";
        DataTable dt1 = objMain.LoadData(strQry5);
        string AddBlock = "";
        if (dt1.Rows.Count > 0)
        {
            if (dt1.Rows[0]["SerialNo"].ToString() == "1")
            {
                AddBlock = "A";
            }
            if (dt1.Rows[0]["SerialNo"].ToString() == "2")
            {
                AddBlock = "B";
            }
            if (dt1.Rows[0]["SerialNo"].ToString() == "3")
            {
                AddBlock = "C";
            }
            if (dt1.Rows[0]["SerialNo"].ToString() == "4")
            {
                AddBlock = "D";
            }
            if (dt1.Rows[0]["SerialNo"].ToString() == "5")
            {
                AddBlock = "E";
            }
            if (dt1.Rows[0]["SerialNo"].ToString() == "6")
            {
                AddBlock = "F";
            }
            if (dt1.Rows[0]["SerialNo"].ToString() == "7")
            {
                AddBlock = "G";
            }
            lblSerial.Text = dt1.Rows[0]["SerialNo"].ToString();
            txtBlockCOde.Text = ddlAdminBlock.SelectedValue + "" + AddBlock;
        }
        this.ModalPopupExtender3.Show();
    }
    protected void btnAddBlock(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex <= 0)
        {

            this.ModalPopupExtender3.Show();
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select District')</script>", false);
            return;
        }
        string strQry8 = " select MainBlockCode as  AdminDistrictCode,MainBlockName as AdminDistrictName from mst5Village   where DistrictCode ='" + ddlDistrict.SelectedValue + "' group by  MainBlockCode,MainBlockName ";
        DataTable dt11 = objMain.LoadData(strQry8);

        objComman.BindDLLDatatable("mst5VillageAgp", dt11, "AdminDistrictCode, AdminDistrictName", conditions, "AdminDistrictName", "asc", ddlAdminBlock, "AdminDistrictName", "AdminDistrictCode", "--Select--");
        txtBlockCOde.Text = "";
        txtBlockName.Text = "";
        ModalPopupExtender3.Show();
    }
    protected void btnAddCluster(object sender, EventArgs e)
    {
        if (ddlBlock.SelectedIndex <= 0)
        {
          
            this.ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Block')</script>", false);
            return;
        }
        conditions = "";
        if (ddlState.SelectedIndex > 0)
        {
            conditions = "  mst5VillageAgp.StateCode='" + ddlState.SelectedValue + "'";
            
        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions = conditions + " and mst5VillageAgp.DistrictCode='" + ddlDistrict.SelectedValue + "'";
           
        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions = conditions + " and mst5VillageAgp.BlockCode='" + ddlBlock.SelectedValue + "'";
        }

        if (ddlVillage.SelectedIndex > 0)
        {
            conditions = conditions + " and mst5VillageAgp.VillageCode='" + ddlVillage.SelectedValue + "'";
        }
        conditions = conditions + " and (mstClusterAgp.ClusterCode is null or mstClusterAgp.ClusterCode='0' or mstClusterAgp.ClusterCode='') ";

        objComman.BindDLL("mst5VillageAgp left  join mstClusterAgp on mstClusterAgp.ClusterCode=mst5VillageAgp.VillageCode ", "VillageCode,VillageName ", conditions, "VillageName", "asc", ddlCLusterVillage, "VillageName", "VillageCode", "--Select--");
        ModalPopupExtender1.Show();
    }

    protected void btnDeleteBlock(object sender, EventArgs e)
    {
        objComman.BindDLL("mst3BlockAgp", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", "DistrictCode='"+ddlDistrict.SelectedValue+"'", "BlockName", "asc", ddlDeleteBlock, "BlockName", "BlockCode", "--Select--");
        ModalPopupExtender6.Show();
    }
    protected void btnDeleteCluster(object sender, EventArgs e)
    {
        if (ddlBlock.SelectedIndex <= 0)
        {

            this.ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Block')</script>", false);
            return;
        }
        conditions = "";
        if (ddlState.SelectedIndex > 0)
        {
            conditions = "  mst5VillageAgp.StateCode='" + ddlState.SelectedValue + "'";

        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions = conditions + " and mst5VillageAgp.DistrictCode='" + ddlDistrict.SelectedValue + "'";

        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions = conditions + " and mst5VillageAgp.BlockCode='" + ddlBlock.SelectedValue + "'";
        }

        if (ddlVillage.SelectedIndex > 0)
        {
            conditions = conditions + " and mst5VillageAgp.VillageCode='" + ddlVillage.SelectedValue + "'";
        }
        conditions = conditions + " and (mstClusterAgp.ClusterCode is null or mstClusterAgp.ClusterCode='' or mstClusterAgp.ClusterCode=mst5VillageAgp.VillageCode) ";

        objComman.BindDLL("mstClusterAgp   left  join mst5VillageAgp on mst5VillageAgp.ClusterCode=mstClusterAgp.ClusterCode ", "mstClusterAgp.ClusterCode as ClusterCode ,mstClusterAgp.ClusterName as ClusterName ", conditions, "ClusterName", "asc", ddlDeleteCluster, "ClusterName", "ClusterCode", "--Select--");
        ModalPopupExtender2.Show();
    }
    public void FillGrid()
    {
        try
        {
            if (Convert.ToInt32(ddlType.SelectedValue) == 2 || Convert.ToInt32(ddlType.SelectedValue) == 1)
            {
                LinkButton1.Visible = false;
                LinkButton2.Visible = false;
                LinkButton3.Visible = false;
                Button1.Visible = false;
                
            }

            if (Convert.ToInt32(ddlType.SelectedValue) == 3)
            {
                LinkButton1.Visible = true;
                LinkButton2.Visible = true;
                LinkButton3.Visible = false;
                Button1.Visible = false;
            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 4)
            {
                LinkButton1.Visible = false;
                LinkButton2.Visible = false;
                LinkButton3.Visible = true;
                Button1.Visible = true;
            }

            if (Convert.ToInt32(ddlType.SelectedValue) == 1 || Convert.ToInt32(ddlType.SelectedValue) == 3)
            {
                string strQry8 = " select * from mst5VillageAgp   where DistrictCode ='" + ddlDistrict.SelectedValue + "' and  AGPStatus=1 and (BlockCode = null or BlockCode=''  or BlockCode='0')  ";
                DataTable dt11 = objMain.LoadData(strQry8);
                if (dt11.Rows.Count > 0)
                {
                    GVCluster.DataSource = null;
                    GVCluster.DataBind();
                    GVCluster1.DataSource = null;
                    GVCluster1.DataBind();
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please complete Block Mapping')</script>", false);
                    return;
                }
            }
            conditions = "";
           string  conditionsCLuster = "";
            if (ddlState.SelectedIndex > 0)
            {
                conditions = " where V.StateCode='" + ddlState.SelectedValue + "'";
                conditionsCLuster = " where D.StateCode='" + ddlState.SelectedValue + "'";
            }
            if (ddlDistrict.SelectedIndex > 0)
            {
                conditions = conditions + " and V.DistrictCode='" + ddlDistrict.SelectedValue + "'";
                conditionsCLuster = conditionsCLuster + " and mstClusterAgp.DistrictCode='" + ddlDistrict.SelectedValue + "'";
            }

            if (ddlBlock.SelectedIndex > 0)
            {
                conditions = conditions + " and V.BlockCode='" + ddlBlock.SelectedValue + "'";
            }
            if (ddlPanchayat.SelectedIndex >1)
            {
                conditions = conditions + " and V.PanchayatCode='" + ddlPanchayat.SelectedValue + "'";
            }
            if (ddlVillage.SelectedIndex > 0)
            {
                conditions = conditions + " and V.VillageCode='" + ddlVillage.SelectedValue + "'";
            }

            if (Convert.ToInt32(ddlType.SelectedValue) == 1 || Convert.ToInt32(ddlType.SelectedValue) == 3 || Convert.ToInt32(ddlType.SelectedValue) == 4)
            {
                SqlParameter[] par1 = new SqlParameter[]
                {
                      new SqlParameter("@Condition",  conditionsCLuster),
                      new SqlParameter("@Flag", 5 ),      
                };
                DataTable DTcluster = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptReportClusterChangeAgp", par1);
                Session["DTcluster"] = DTcluster;
            }
            DataTable DT = null;
            if (ddlBlock.SelectedIndex > 0 && Convert.ToInt32(ddlType.SelectedValue) == 4)
            {
               
                SqlParameter[] par = new SqlParameter[]
            {
              new SqlParameter("@Condition",  conditions),
              new SqlParameter("@Flag",  "6"),
      
             };
                DT = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptReportClusterChangeAgp", par);
            }
            else
            {
             
                SqlParameter[] par = new SqlParameter[]
            {
              new SqlParameter("@Condition",  conditions),
              new SqlParameter("@Flag",  ddlType.SelectedValue),
      
             };
                DT = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptReportClusterChangeAgp", par);
            }
                Session["GridViewData"] = DT;
                GVCluster.Visible = true;
                if (Convert.ToInt32(ddlType.SelectedValue) == 4)
                {
                    GVCluster1.Visible = true;
                    GVCluster.Visible = false;
                if (DT.Rows.Count > 0)
                {
                  
                    GVCluster1.DataSource = DT;
                    GVCluster1.DataBind();
                }
                else
                {
                    GVCluster1.DataSource = null;
                    GVCluster1.DataBind();

                }
                }
                else
                {
                    GVCluster1.Visible = false;
                    GVCluster.Visible = true;
                    if (DT.Rows.Count > 0)
                    {
                      
                        GVCluster.DataSource = DT;
                        GVCluster.DataBind();
                    }
                    else
                    {
                        GVCluster.DataSource = null;
                        GVCluster.DataBind();

                    }
                }
                if (Convert.ToInt32(ddlType.SelectedValue) == 2)
                {
                    GVCluster.Columns[1].Visible = false;
                    GVCluster.Columns[9].Visible = true;
                    GVCluster.Columns[10].Visible = true;
                    GVCluster.Columns[11].Visible = true;
                    GVCluster.Columns[12].Visible = true;
                    GVCluster.Columns[13].Visible = false;
                    GVCluster.Columns[14].Visible = false;
                    GVCluster.Columns[15].Visible = false;
                    GVCluster.Columns[16].Visible = true;
                    GVCluster.Columns[17].Visible = true;
                    GVCluster.Columns[18].Visible = true;
                    GVCluster.Columns[19].Visible = true;
                    GVCluster.Columns[20].Visible = false;
                    GVCluster.Columns[21].Visible = false;
                    GVCluster.Columns[22].Visible = false;
                    GVCluster.Columns[23].Visible = true;
                   
                }
             if (Convert.ToInt32(ddlType.SelectedValue) == 1 || Convert.ToInt32(ddlType.SelectedValue) == 3)
                
                {
                    GVCluster.Columns[1].Visible = false;
                    GVCluster.Columns[9].Visible = false;
                    GVCluster.Columns[10].Visible = false;
                    GVCluster.Columns[11].Visible = false;
                    GVCluster.Columns[12].Visible = false;
                    GVCluster.Columns[13].Visible = true;
                    GVCluster.Columns[14].Visible = true;
                    GVCluster.Columns[15].Visible = true;
                    GVCluster.Columns[16].Visible = false;
                    GVCluster.Columns[17].Visible = false;
                    GVCluster.Columns[18].Visible = false;
                    GVCluster.Columns[19].Visible = false;
                    GVCluster.Columns[20].Visible = true;
                    GVCluster.Columns[21].Visible = true;
                    GVCluster.Columns[22].Visible = false;
                    GVCluster.Columns[23].Visible = false;
                  
                }


        }
        catch (Exception)
        {

            throw;
        }

    }
    #region Fill Master Data
    public void FillCBState()
    {
        conditions = "statecode in(select distinct statecode from mst5VillageAgp where AGPStatus=1)";
        objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");
    }
    public void FillCBDist()
    {

        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "StateCode ='" + ddlState.SelectedValue + "' and mst2DistrictAgp.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and  mst2DistrictAgp.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        //else
        //{
        //    conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCodeNew"].ToString() + ") and mst2DistrictAgp.FYear ='" + ddlYear.SelectedItem.Text + "' ";


        //}
         else
        {
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and mst2DistrictAgp.FYear  ='" + ddlYear.SelectedItem.Text + "'";


        }
        conditions = conditions + " and DistrictCode in(select distinct DistrictCode from mst5VillageAgp where AGPStatus=1)";
        objComman.BindDLL("mst2DistrictAgp", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");



    }
    public void FillCBBock()
    {
        conditions = " ";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'";
        }
        if (Session["user_level"].ToString() == "19")
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'and BlockCode in(" + Session["DistrictCodeNew"].ToString() + ")";
        }
        else
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' ";
        }

 
        objComman.BindDLL("mst3BlockAgp", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");



    }
    public void FillCVillage()
    {
        conditions = "";
        ////conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        ////objComman.BindDLL("mst5VillageAgp", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--Select--");

        if (Convert.ToString( ddlPanchayat.SelectedValue) == "1")
        {
            conditions = "mst5VillageAgp.DistrictCode ='" + ddlDistrict.SelectedValue + "'  and mst5VillageAgp.BlockCode ='" + ddlBlock.SelectedValue + "' and AGPStatus=1 ";

        }
        else
        {
            conditions = "mst5VillageAgp.DistrictCode ='" + ddlDistrict.SelectedValue + "'  and mst5VillageAgp.BlockCode ='" + ddlBlock.SelectedValue + "' and  mst5VillageAgp.PanchayatCode='" + ddlPanchayat.SelectedValue + "' and AGPStatus=1  ";

        }

        string strQry = "  SELECT mst5VillageAgp.VillageCode, dbo.TitleCase(upper((mst5VillageAgp.VillageName))) + ' (' + dbo.TitleCase(upper(mstPanchayatAgp.PanchayatName)) +')'   as VillageName FROM mst5VillageAgp INNER JOIN mstPanchayatAgp ON mst5VillageAgp.PanchayatCode = mstPanchayatAgp.PanchayatCode where " + conditions + "  order by VillageName   ";
        DataTable dtVillage = objMain.LoadData(strQry);

        objComman.BindDLLMasterTableVillage("mst5VillageAgp", "VillageName,VillageCode", dtVillage, conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "Select");


    }

    
    ////public void FillSchool()
    ////{
    ////    conditions = "";
    ////    if (ddlBlock.SelectedIndex > 0)
    ////    {
    ////        conditions = "BlockCode ='" + ddlBlock.SelectedValue + "' ";
    ////    }
    ////    if (ddlVillage.SelectedIndex > 0)
    ////    {
    ////        conditions = "VillageCode ='" + ddlVillage.SelectedValue + "' ";
    ////    }

    ////    objComman.BindDLL("mstSchoolAgp", "SchoolCode,Name", conditions, "Name", "asc", ddlSchool, "Name", "SchoolCode", "Select");

    ////}

    #endregion

    #region   SelectedIndexChanged Methods
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
         FillCVillage();
    }
  
    //protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (Convert.ToInt32(ddlType.SelectedValue) == 1)
    //    {
    //        lblShool.Visible = false;
    //        ddlSchool.Visible = false;
    //    }
    //    else
    //    {
    //        lblShool.Visible = true;
    //        ddlSchool.Visible = true;
    //    }
    //}
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
     //   Locking();
        FillCBBock();
    }

    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
       // FillCVillage();
        FillCBCluster();
      //  FillSchool();
        Locking();
    }
    public void FillCBCluster()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "'";
        objComman.BindDLLSelectAll("mstPanchayatAgp", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "Select");



    }
  
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        //FillSchool();
    }

    #endregion

    protected void GV_Cluster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        UpdateData();
        GVCluster.PageIndex = e.NewPageIndex;
        if (Session["GridViewData"] != null)
        {
            DataTable dt = Session["GridViewData"] as DataTable;
            GVCluster.DataSource = dt;
            GVCluster.DataBind();
        }


    }
    public void UpdateData()
    {

        DataTable dt = (DataTable)Session["GridViewData"];

        for (int i = 0; i < GVCluster.Rows.Count; i++)
        {
            if (Convert.ToInt32(ddlType.SelectedValue) == 2)
            {


                DropDownList ddlWorkingStatus = (DropDownList)GVCluster.Rows[i].FindControl("ddlWorkingStatus");
                DropDownList ddlManagement = (DropDownList)GVCluster.Rows[i].FindControl("ddlManagement");
                Label lblDISECode = (Label)GVCluster.Rows[i].FindControl("lblDISECode");
                DropDownList ddlGKP = (DropDownList)GVCluster.Rows[i].FindControl("ddlGKP");
                DropDownList ddlGKPLevel = (DropDownList)GVCluster.Rows[i].FindControl("ddlGKPLevel");
                DropDownList ddlSchoolType = (DropDownList)GVCluster.Rows[i].FindControl("ddlSchoolType");
                DropDownList ddlBalsabha = (DropDownList)GVCluster.Rows[i].FindControl("ddlBalsabha");
                DropDownList ddlSchoolCampus = (DropDownList)GVCluster.Rows[i].FindControl("ddlSchoolCampus");
                DropDownList ddlVillageName = (DropDownList)GVCluster.Rows[i].FindControl("ddlVillageName");


                DataRow[] dr = dt.Select("DISECode='" + Convert.ToString(lblDISECode.Text) + "'");
                if (dr.Length > 0)
                {

                    dr[0]["WorkingStatus"] = ddlWorkingStatus.SelectedValue;
                    dr[0]["Management"] = ddlManagement.SelectedValue;
                    dr[0]["GKP"] = ddlGKP.SelectedValue;

                    dr[0]["GKPLevel"] = ddlGKPLevel.SelectedValue;
                    dr[0]["SchoolType"] = ddlSchoolType.SelectedValue;
                    dr[0]["BAlVal"] = ddlBalsabha.SelectedValue;

                    dr[0]["SchoolCampus"] = ddlSchoolCampus.SelectedValue;
                    dr[0]["TempVillageCode"] = ddlVillageName.SelectedValue;





                }

            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 1 || Convert.ToInt32(ddlType.SelectedValue) == 3)
            {



                DropDownList ddlClusterCode = (DropDownList)GVCluster.Rows[i].FindControl("ddlClusterCode");
                DropDownList ddlVillageGeography = (DropDownList)GVCluster.Rows[i].FindControl("ddlVillageGeography");
                DropDownList ddlVillageOperational = (DropDownList)GVCluster.Rows[i].FindControl("ddlVillageOperational");
                DropDownList ddlCblVillage = (DropDownList)GVCluster.Rows[i].FindControl("ddlCblVillage");
                DropDownList ddlFunctionalStatus = (DropDownList)GVCluster.Rows[i].FindControl("ddlFunctionalStatus");
                DropDownList ddlAGP = (DropDownList)GVCluster.Rows[i].FindControl("ddlAGP");
                DropDownList ddlBlockName = (DropDownList)GVCluster.Rows[i].FindControl("ddlBlockName");




                Label lblVillageCode = (Label)GVCluster.Rows[i].FindControl("lblTempVillageCode");

                DataRow[] dr = dt.Select("TempVillageCode='" + Convert.ToString(lblVillageCode.Text) + "'");
                if (dr.Length > 0)
                {

                    dr[0]["ClusterCode"] = ddlClusterCode.SelectedValue;
                    dr[0]["VillageGeography"] = ddlVillageGeography.SelectedValue;
                    dr[0]["VillageGeographyOperational"] = ddlVillageOperational.SelectedValue;


                    dr[0]["CBlVillage"] = ddlCblVillage.SelectedValue;
                    dr[0]["FunctionalStatus"] = ddlFunctionalStatus.SelectedValue;
                    dr[0]["TempBlockCode"] = ddlBlockName.SelectedValue;




                }

            }
        }
          if (Convert.ToInt32(ddlType.SelectedValue) == 4)
            {

                  for (int i = 0; i < GVCluster1.Rows.Count; i++)
                  {

                DropDownList ddlClusterCode = (DropDownList)GVCluster1.Rows[i].FindControl("ddlClusterCode");
                DropDownList ddlVillageGeography = (DropDownList)GVCluster1.Rows[i].FindControl("ddlVillageGeography");
                DropDownList ddlVillageOperational = (DropDownList)GVCluster1.Rows[i].FindControl("ddlVillageOperational");
                DropDownList ddlCblVillage = (DropDownList)GVCluster1.Rows[i].FindControl("ddlCblVillage");
                DropDownList ddlFunctionalStatus = (DropDownList)GVCluster1.Rows[i].FindControl("ddlFunctionalStatus");
                DropDownList ddlAGP = (DropDownList)GVCluster1.Rows[i].FindControl("ddlAGP");
                DropDownList ddlBlockName = (DropDownList)GVCluster1.Rows[i].FindControl("ddlBlockName");




                Label lblVillageCode = (Label)GVCluster1.Rows[i].FindControl("lblTempVillageCode");

                DataRow[] dr = dt.Select("TempVillageCode='" + Convert.ToString(lblVillageCode.Text) + "'");
                if (dr.Length > 0)
                {

                    dr[0]["ClusterCode"] = ddlClusterCode.SelectedValue;
                    dr[0]["VillageGeography"] = ddlVillageGeography.SelectedValue;
                    dr[0]["VillageGeographyOperational"] = ddlVillageOperational.SelectedValue;


                    dr[0]["CBlVillage"] = ddlCblVillage.SelectedValue;
                    dr[0]["FunctionalStatus"] = ddlFunctionalStatus.SelectedValue;
                    dr[0]["TempBlockCode"] = ddlBlockName.SelectedValue;

                }


                }
           }
            
    
     
        Session["GridViewData"] = dt;

    }

    protected void ddlVillageOperational_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        DropDownList ddlVillageOperational = (DropDownList)row1.FindControl("ddlVillageOperational");
        DropDownList ddlCblVillage = (DropDownList)row1.FindControl("ddlCblVillage");
        DropDownList ddlFunctionalStatus = (DropDownList)row1.FindControl("ddlFunctionalStatus");


        Label lblTempVillageCode = (Label)row1.FindControl("lblTempVillageCode");
        if (ddlVillageOperational.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlVillageOperational.SelectedValue) == 2)
            {
                string strQry = "Select * from mstSchoolAgp  where  WorkingStatus=1  and Villagecode='" + lblTempVillageCode.Text.ToString() + "'  ";


                DataTable dtEGVillagecode = objMain.LoadData(strQry);
                if (dtEGVillagecode.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Mark Schools as Non-Operational School')</script>", false);
                    ddlVillageOperational.SelectedValue = "1";
                }

                if (Convert.ToInt32(ddlCblVillage.SelectedValue) == 1)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Mark  Non-CBL Village')</script>", false);
                    ddlVillageOperational.SelectedValue = "1";
                }
                if (Convert.ToInt32(ddlFunctionalStatus.SelectedValue) == 1)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Mark  Non Functional Village')</script>", false);
                    ddlVillageOperational.SelectedValue = "1";
                }
            }

        }
        else
        {
            ddlVillageOperational.SelectedValue = "1";
        }

    }


    protected void ddlCblVillage_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        DropDownList ddlVillageOperational = (DropDownList)row1.FindControl("ddlVillageOperational");
        DropDownList ddlCblVillage = (DropDownList)row1.FindControl("ddlCblVillage");


        if (ddlCblVillage.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlVillageOperational.SelectedValue) == 2)
            {
                if (Convert.ToInt32(ddlCblVillage.SelectedValue) == 1)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('CBL Village should be Working and Operational')</script>", false);
                    ddlCblVillage.SelectedValue = "2";
                }
            }

        }
        else
        {
            ddlCblVillage.SelectedValue = "1";
        }

    }

    protected void ddlFunctionalStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        DropDownList ddlVillageOperational = (DropDownList)row1.FindControl("ddlVillageOperational");
        DropDownList ddlFunctionalStatus = (DropDownList)row1.FindControl("ddlFunctionalStatus");


        if (ddlFunctionalStatus.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlVillageOperational.SelectedValue) == 2)
            {
                if (Convert.ToInt32(ddlFunctionalStatus.SelectedValue) == 1)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please mark village as Operational Village')</script>", false);
                    ddlFunctionalStatus.SelectedValue = "1";
                }

            }

        }
        else
        {
            ddlFunctionalStatus.SelectedValue = "1";
        }

    }
    
    protected void ddlGKP_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;
        DropDownList ddlGKP = (DropDownList)row1.FindControl("ddlGKP");
        DropDownList ddlGKPLevel = (DropDownList)row1.FindControl("ddlGKPLevel");
        if (Convert.ToInt32(ddlGKP.SelectedValue) == 2)
        {
            if (ddlGKPLevel.SelectedIndex > 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Remove GKP Level')</script>", false);
                ddlGKP.SelectedValue = "1";
                return;
            }
        }
        if (Convert.ToInt32(ddlGKP.SelectedValue) == 1)
        {
            ddlGKPLevel.Enabled = true;
        }
        else
        {
            ddlGKPLevel.Enabled = false;
        }
       
    }
    protected void ddlWorkingStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        DropDownList ddlWorkingStatus = (DropDownList)row1.FindControl("ddlWorkingStatus");

        DropDownList ddlGKP = (DropDownList)row1.FindControl("ddlGKP");

        DropDownList ddlGKPLevel = (DropDownList)row1.FindControl("ddlGKPLevel");
        DropDownList ddlBalsabha = (DropDownList)row1.FindControl("ddlBalsabha");

        Label lblTempVillageCode = (Label)row1.FindControl("lblTempVillageCode");
        if (ddlWorkingStatus.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 2 || Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 3 || Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 4 || Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 5)
            {
                string strQry = "Select * from mst5VillageAgp  where  VillageGeographyOperational=1  and Villagecode='" + lblTempVillageCode.Text.ToString() + "'  ";


                DataTable dtEGVillagecode = objMain.LoadData(strQry);
                if (dtEGVillagecode.Rows.Count > 0)
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please update VillageOperational')</script>", false);
                    ddlWorkingStatus.SelectedValue = "1";
                    return;
                }
                if (Convert.ToInt32(ddlBalsabha.SelectedValue) == 1)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Mark School As Non-Balsabha School')</script>", false);
                    ddlWorkingStatus.SelectedValue = "1";
                    return;
                }
                if (Convert.ToInt32(ddlGKP.SelectedValue) == 1)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Mark school as Non GKP School')</script>", false);
                    ddlWorkingStatus.SelectedValue = "1";
                    return;
                }
             
                ddlGKP.Enabled = false;
                ddlGKPLevel.Enabled = false;
                ddlGKPLevel.Enabled = false;
                ddlBalsabha.Enabled = false;
                //ddlGKP.SelectedIndex = 0;
                //ddlGKPLevel.SelectedIndex = 0;
                //ddlGKPLevel.SelectedIndex = 0;
            }
            else
            {
                string strQry = "Select * from mst5VillageAgp  where  VillageGeographyOperational=1  and Villagecode='" + lblTempVillageCode.Text.ToString() + "'  ";


                DataTable dtEGVillagecode = objMain.LoadData(strQry);
                if (dtEGVillagecode.Rows.Count > 0)
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please update VillageOperational')</script>", false);
                    ddlWorkingStatus.SelectedValue = "2";
                    return;
                }
                Label lblManagement = (Label)row1.FindControl("lblManagement");
                DropDownList ddlManagement = (DropDownList)row1.FindControl("ddlManagement");
                if ((Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 2 ||(Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 5)) && Convert.ToInt32(ddlWorkingStatus.SelectedValue) == 1)
                {


                    ddlBalsabha.Enabled = true;
                }
                else
                {
                    ddlBalsabha.Enabled = false;
                }
                ddlGKPLevel.Enabled = true;
                ddlGKP.Enabled = true;
                ddlGKPLevel.Enabled = true;
            }

        }
        else
        {
            ddlGKP.Enabled = true;
            ddlGKPLevel.Enabled = true;
            ddlGKPLevel.Enabled = true;
            ddlWorkingStatus.SelectedValue = "1";
        }

    }
      
    protected void GV_luster_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            if (Convert.ToInt32(ddlType.SelectedValue) == 2)
            {
                Label lblWorkingStatus = (Label)e.Row.FindControl("lblWorkingStatus");
                Label lblManagement = (Label)e.Row.FindControl("lblManagement");
                DropDownList ddlWorkingStatus = (DropDownList)e.Row.FindControl("ddlWorkingStatus");
                DropDownList ddlManagement = (DropDownList)e.Row.FindControl("ddlManagement");

                Label lblGKP = (Label)e.Row.FindControl("lblGKP");
                Label lblGKPLevel = (Label)e.Row.FindControl("lblGKPLevel");
                Label lblSchoolType = (Label)e.Row.FindControl("lblSchoolType");
                Label lblBAlVal = (Label)e.Row.FindControl("lblBAlVal");

                Label lblSchoolCampus = (Label)e.Row.FindControl("lblSchoolCampus");



                DropDownList ddlGKP = (DropDownList)e.Row.FindControl("ddlGKP");
                DropDownList ddlGKPLevel = (DropDownList)e.Row.FindControl("ddlGKPLevel");
                DropDownList ddlSchoolType = (DropDownList)e.Row.FindControl("ddlSchoolType");
                DropDownList ddlBalsabha = (DropDownList)e.Row.FindControl("ddlBalsabha");
                DropDownList ddlSchoolCampus = (DropDownList)e.Row.FindControl("ddlSchoolCampus");
                Label lblBlockName = (Label)e.Row.FindControl("lblBlockName");
                DropDownList ddlBlockName = (DropDownList)e.Row.FindControl("ddlBlockName");
                Label lblBlockCode = (Label)e.Row.FindControl("lblTempBlockCode");
             
                lblBlockName.Visible = true;
                ddlBlockName.Visible = false;

                DropDownList ddlVillageName = (DropDownList)e.Row.FindControl("ddlVillageName");
                Label lblVillageN9ame = (Label)e.Row.FindControl("lblVillageN9ame");
                Label TempVillageCode = (Label)e.Row.FindControl("lblTempVillageCode");

            
                
                lblVillageN9ame.Visible = true;
                ddlVillageName.Visible = false;
                if ((lblManagement.Text == "2" || lblManagement.Text == "5") && lblWorkingStatus.Text == "1")
                {
                    ddlBalsabha.Enabled = true;
                }
                else
                {
                    ddlBalsabha.Enabled = false;
                }
                if (lblGKP.Text == "1")
                {
                    ddlGKPLevel.Enabled = true;
                }
                else
                {
                    ddlGKPLevel.Enabled = false;
                }
                if (lblWorkingStatus.Text == "1")
                {
                   
                    ddlGKP.Enabled = true;
                   
                }
                else
                {
                   
                    ddlGKP.Enabled = false;
                  
                }
                ddlGKP.SelectedValue = lblGKP.Text;
                ddlGKPLevel.SelectedValue = lblGKPLevel.Text;
                ddlSchoolType.SelectedValue = lblSchoolType.Text;
                ddlBalsabha.SelectedValue = lblBAlVal.Text;
                ddlWorkingStatus.SelectedValue = lblWorkingStatus.Text;
                ddlManagement.SelectedValue = lblManagement.Text;
                ddlSchoolCampus.SelectedValue = lblSchoolCampus.Text;
                //objComman.BindDLL("mst5VillageAgp", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName ", "DistrictCode='" + ddlDistrict.SelectedValue + "' and BlockCode='" + lblBlockCode.Text + "' ", "VillageName", "asc", ddlVillageName, "VillageName", "VillageCode", "--Select--");
                //ddlVillageName.SelectedValue = TempVillageCode.Text;
            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 1 || Convert.ToInt32(ddlType.SelectedValue) == 3 || Convert.ToInt32(ddlType.SelectedValue) == 4)
            {
                Label lblBlockCode = (Label)e.Row.FindControl("lblTempBlockCode");
                Label lblClusterCode = (Label)e.Row.FindControl("lblTempClusterCode");
                Label lblVillageCode = (Label)e.Row.FindControl("lblTempVillageCode");

                Label lblVillageGeography = (Label)e.Row.FindControl("lblVillageGeography");
                Label lblVillageGeographyOperational = (Label)e.Row.FindControl("lblVillageGeographyOperational");

                Label lblCBlVillage = (Label)e.Row.FindControl("lblCBlVillage");
                Label lblFunctionalStatus = (Label)e.Row.FindControl("lblFunctionalStatus");
                Label lblAGPStatus = (Label)e.Row.FindControl("lblAGPStatus");

                DropDownList ddlClusterCode = (DropDownList)e.Row.FindControl("ddlClusterCode");
                DropDownList ddlVillageGeography = (DropDownList)e.Row.FindControl("ddlVillageGeography");
                DropDownList ddlVillageOperational = (DropDownList)e.Row.FindControl("ddlVillageOperational");


                DropDownList ddlCblVillage = (DropDownList)e.Row.FindControl("ddlCblVillage");

              
                DropDownList ddlFunctionalStatus = (DropDownList)e.Row.FindControl("ddlFunctionalStatus");
                DropDownList ddlAGP = (DropDownList)e.Row.FindControl("ddlAGP");
                Label lblBlockName = (Label)e.Row.FindControl("lblBlockName");
                DropDownList ddlBlockName = (DropDownList)e.Row.FindControl("ddlBlockName");


                DropDownList ddlVillageName = (DropDownList)e.Row.FindControl("ddlVillageName");
                Label lblVillageN9ame = (Label)e.Row.FindControl("lblVillageN9ame");
                lblVillageN9ame.Visible = true;
                ddlVillageName.Visible = false;

                lblBlockName.Visible = true;
                ddlBlockName.Visible = false;
                DataTable dt = Session["DTcluster"] as DataTable;
                DataTable dtAddCluster = dt.Clone();
                DataRow drNew;
                DataRow[] dr = dt.Select("BlockCode='" + lblBlockCode.Text + "'");
                if (dr.Length > 0)
                {
                    foreach (DataRow row in dr)
                    {
                      //  DtOutDoor.Rows.Remove(row);
                        drNew = dtAddCluster.NewRow();
                        drNew["ClusterCode"] = row["ClusterCode"];
                        drNew["ClusterName"] = row["ClusterName"];

                        dtAddCluster.Rows.Add(drNew);
                    }
                }

                objComman.BindDLL("mst3BlockAgp", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", "DistrictCode='"+ddlDistrict.SelectedValue +"' ", "BlockName", "asc", ddlBlockName, "BlockName", "BlockCode", "--Select--");



                    objComman.BindDLLDatatable("mst5VillageAgp", dtAddCluster, "ClusterCode, ClusterName", conditions, "ClusterName", "asc", ddlClusterCode, "ClusterName", "ClusterCode", "--Select--");
                     dtAddCluster=null;
                    if (lblClusterCode.Text.Length > 1)
                    {

                        ddlClusterCode.SelectedValue = lblClusterCode.Text;
                    }
                    if (lblVillageCode.Text == lblClusterCode.Text)
                    {
                        ddlClusterCode.Enabled = false;
                    }
                    if (lblBlockCode.Text.Length > 0)
                    {
                        ddlBlockName.SelectedValue = lblBlockCode.Text;
                    }
                    ddlVillageGeography.SelectedValue = lblVillageGeography.Text;
                    ddlVillageOperational.SelectedValue = lblVillageGeographyOperational.Text;
                    ddlCblVillage.SelectedValue = lblCBlVillage.Text;
                    ddlFunctionalStatus.SelectedValue = lblFunctionalStatus.Text;
                    ddlAGP.SelectedValue = lblAGPStatus.Text;

           }
               
               //ImgBut1.Enabled = false;
            //ImgAcc1.Enabled = false;

            //ImageButton lnk = e.Row.FindControl("ImgAccExcel") as ImageButton;
            //AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
            //trigger.ControlID = lnk.UniqueID;
            //trigger.EventName = "Click";
            //ml121.Triggers.Add(trigger);
            //mainpnl121.Triggers.Add(trigger);

        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
        }
    }

    protected void GV_luster1_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (Convert.ToInt32(ddlType.SelectedValue) == 2)
            {
                Label lblWorkingStatus = (Label)e.Row.FindControl("lblWorkingStatus");
                Label lblManagement = (Label)e.Row.FindControl("lblManagement");
                DropDownList ddlWorkingStatus = (DropDownList)e.Row.FindControl("ddlWorkingStatus");
                DropDownList ddlManagement = (DropDownList)e.Row.FindControl("ddlManagement");

                Label lblGKP = (Label)e.Row.FindControl("lblGKP");
                Label lblGKPLevel = (Label)e.Row.FindControl("lblGKPLevel");
                Label lblSchoolType = (Label)e.Row.FindControl("lblSchoolType");
                Label lblBAlVal = (Label)e.Row.FindControl("lblBAlVal");

                Label lblSchoolCampus = (Label)e.Row.FindControl("lblSchoolCampus");



                DropDownList ddlGKP = (DropDownList)e.Row.FindControl("ddlGKP");
                DropDownList ddlGKPLevel = (DropDownList)e.Row.FindControl("ddlGKPLevel");
                DropDownList ddlSchoolType = (DropDownList)e.Row.FindControl("ddlSchoolType");
                DropDownList ddlBalsabha = (DropDownList)e.Row.FindControl("ddlBalsabha");
                DropDownList ddlSchoolCampus = (DropDownList)e.Row.FindControl("ddlSchoolCampus");
                Label lblBlockName = (Label)e.Row.FindControl("lblBlockName");
                DropDownList ddlBlockName = (DropDownList)e.Row.FindControl("ddlBlockName");
                Label lblBlockCode = (Label)e.Row.FindControl("lblTempBlockCode");

                lblBlockName.Visible = true;
                ddlBlockName.Visible = false;

                DropDownList ddlVillageName = (DropDownList)e.Row.FindControl("ddlVillageName");
                Label lblVillageN9ame = (Label)e.Row.FindControl("lblVillageN9ame");
                Label TempVillageCode = (Label)e.Row.FindControl("lblTempVillageCode");



                lblVillageN9ame.Visible = true;
                ddlVillageName.Visible = false;
                if ((lblManagement.Text == "2" || lblManagement.Text == "5") && lblWorkingStatus.Text == "1")
                {
                    ddlBalsabha.Enabled = true;
                }
                else
                {
                    ddlBalsabha.Enabled = false;
                }
                if (lblGKP.Text == "1")
                {
                    ddlGKPLevel.Enabled = true;
                }
                else
                {
                    ddlGKPLevel.Enabled = false;
                }
                if (lblWorkingStatus.Text == "1")
                {

                    ddlGKP.Enabled = true;

                }
                else
                {

                    ddlGKP.Enabled = false;

                }
                ddlGKP.SelectedValue = lblGKP.Text;
                ddlGKPLevel.SelectedValue = lblGKPLevel.Text;
                ddlSchoolType.SelectedValue = lblSchoolType.Text;
                ddlBalsabha.SelectedValue = lblBAlVal.Text;
                ddlWorkingStatus.SelectedValue = lblWorkingStatus.Text;
                ddlManagement.SelectedValue = lblManagement.Text;
                ddlSchoolCampus.SelectedValue = lblSchoolCampus.Text;
                //objComman.BindDLL("mst5VillageAgp", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName ", "DistrictCode='" + ddlDistrict.SelectedValue + "' and BlockCode='" + lblBlockCode.Text + "' ", "VillageName", "asc", ddlVillageName, "VillageName", "VillageCode", "--Select--");
                //ddlVillageName.SelectedValue = TempVillageCode.Text;
            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 1 || Convert.ToInt32(ddlType.SelectedValue) == 3 || Convert.ToInt32(ddlType.SelectedValue) == 4)
            {
                Label lblBlockCode = (Label)e.Row.FindControl("lblTempBlockCode");
                Label lblClusterCode = (Label)e.Row.FindControl("lblTempClusterCode");
                Label lblVillageCode = (Label)e.Row.FindControl("lblTempVillageCode");

                Label lblVillageGeography = (Label)e.Row.FindControl("lblVillageGeography");
                Label lblVillageGeographyOperational = (Label)e.Row.FindControl("lblVillageGeographyOperational");

                Label lblCBlVillage = (Label)e.Row.FindControl("lblCBlVillage");
                Label lblFunctionalStatus = (Label)e.Row.FindControl("lblFunctionalStatus");
                Label lblAGPStatus = (Label)e.Row.FindControl("lblAGPStatus");

                DropDownList ddlClusterCode = (DropDownList)e.Row.FindControl("ddlClusterCode");
                DropDownList ddlVillageGeography = (DropDownList)e.Row.FindControl("ddlVillageGeography");
                DropDownList ddlVillageOperational = (DropDownList)e.Row.FindControl("ddlVillageOperational");


                DropDownList ddlCblVillage = (DropDownList)e.Row.FindControl("ddlCblVillage");


                DropDownList ddlFunctionalStatus = (DropDownList)e.Row.FindControl("ddlFunctionalStatus");
                DropDownList ddlAGP = (DropDownList)e.Row.FindControl("ddlAGP");
                Label lblBlockName = (Label)e.Row.FindControl("lblBlockName");
                DropDownList ddlBlockName = (DropDownList)e.Row.FindControl("ddlBlockName");


                DropDownList ddlVillageName = (DropDownList)e.Row.FindControl("ddlVillageName");
                Label lblVillageN9ame = (Label)e.Row.FindControl("lblVillageN9ame");
                lblVillageN9ame.Visible = true;
                ddlVillageName.Visible = false;

                lblBlockName.Visible = false;
                ddlBlockName.Visible = true;
                DataTable dt = Session["DTcluster"] as DataTable;
                DataTable dtAddCluster = dt.Clone();
                DataRow drNew;
                DataRow[] dr = dt.Select("BlockCode='" + lblBlockCode.Text + "'");
                if (dr.Length > 0)
                {
                    foreach (DataRow row in dr)
                    {
                        //  DtOutDoor.Rows.Remove(row);
                        drNew = dtAddCluster.NewRow();
                        drNew["ClusterCode"] = row["ClusterCode"];
                        drNew["ClusterName"] = row["ClusterName"];

                        dtAddCluster.Rows.Add(drNew);
                    }
                }

                objComman.BindDLL("mst3BlockAgp", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", "DistrictCode='" + ddlDistrict.SelectedValue + "' ", "BlockName", "asc", ddlBlockName, "BlockName", "BlockCode", "--Select--");



                objComman.BindDLLDatatable("mst5VillageAgp", dtAddCluster, "ClusterCode, ClusterName", conditions, "ClusterName", "asc", ddlClusterCode, "ClusterName", "ClusterCode", "--Select--");
                dtAddCluster = null;
                if (lblClusterCode.Text.Length > 1)
                {

                    ddlClusterCode.SelectedValue = lblClusterCode.Text;
                }
                if (lblVillageCode.Text == lblClusterCode.Text)
                {
                    ddlClusterCode.Enabled = false;
                }
                if (lblBlockCode.Text.Length > 0)
                {
                    ddlBlockName.SelectedValue = lblBlockCode.Text;
                }
                ddlVillageGeography.SelectedValue = lblVillageGeography.Text;
                ddlVillageOperational.SelectedValue = lblVillageGeographyOperational.Text;
                ddlCblVillage.SelectedValue = lblCBlVillage.Text;
                ddlFunctionalStatus.SelectedValue = lblFunctionalStatus.Text;
                ddlAGP.SelectedValue = lblAGPStatus.Text;

            }

            //ImgBut1.Enabled = false;
            //ImgAcc1.Enabled = false;

            //ImageButton lnk = e.Row.FindControl("ImgAccExcel") as ImageButton;
            //AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
            //trigger.ControlID = lnk.UniqueID;
            //trigger.EventName = "Click";
            //ml121.Triggers.Add(trigger);
            //mainpnl121.Triggers.Add(trigger);

        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
        }
    }

  
}

