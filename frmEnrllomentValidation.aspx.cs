using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class frmEnrllomentValidation : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    string conditions="";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                LoadYear();
                ///    FillCBState();
                ///    
                AlllStateCode();

                LoadData();

            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }

           

        }

    }

    public void AlllStateCode()
    {
        conditions = "";
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
            objComman.BindDLLDatatable("mst1State", dtAllState, "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

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
            objComman.BindDLLDatatable("mst1State", dtAllState, "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");


        }

    }
    public void FillCBState()
    {
        conditions = "";
        objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "desc", ddlState, "StateName", "StateCode", "--Select--");
    }

    public void FillCBDist()
    {

        conditions = "";
       
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
      


        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");



    }

    public void LoadData()
    {
        string strQry = "SELECT  [ID],[SchoolLevelName] ,case when [Mangment] =1 then 'Government' else 'private ' end [Mangment] ,EntryAllowed FROM [mstEnrollmentEntryvalidation] ";


        DataTable dt = objMain.LoadData(strQry);

        GVCluster.DataSource = dt;
        GVCluster.DataBind();
    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex <= 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select District')</script>", false);
            return;
        }
       
        LoadData();
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        int ret = 0; 
        for (int i = 0; i < GVCluster.Rows.Count; i++)
        {



         
            Label lblFromName = (Label)GVCluster.Rows[i].FindControl("lblMonth");
            TextBox txtDob = (TextBox)GVCluster.Rows[i].FindControl("txtDob");
            int Total = 0;
            if (txtDob.Text !="")
            {
                Total = Convert.ToInt32(txtDob.Text);
            }

               ret = Update_ModuelStatus(Convert.ToInt32( lblFromName.Text),Total);
            
            

        }
        if (ret > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
            LoadData();
        }
    }
    public int Update_ModuelStatus(int FromName, int DistCode)
    {
        SqlConnection dbSqlconnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            dbSqlconnection.Open();
            using (SqlCommand dbSqlCommand = (SqlCommand)dbSqlconnection.CreateCommand())
            {
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = "[Update_EnrlooVali]";
                dbSqlCommand.Parameters.AddWithValue("@FromName", FromName);
                dbSqlCommand.Parameters.AddWithValue("@Q", DistCode);
               
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
            throw exp;
        }
        finally
        {
            dbSqlconnection.Dispose();
        }
    }

    protected void GV_luster_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{


        //    Label lblMonth = (Label)e.Row.FindControl("lblMonth");
        //    TextBox txtaddate = (TextBox)e.Row.FindControl("txtaddate");
        //    TextBox txtDob = (TextBox)e.Row.FindControl("txtDob");
        //    DropDownList ddlMonth = (DropDownList)e.Row.FindControl("ddlMonth");

        //    if (lblMonth.Text != "")
        //    {
        //        ddlMonth.SelectedValue = lblMonth.Text;
        //    }


        //             txtDob.Text = Convert.ToDateTime(txtaddate.Text).ToString("dd/MM/yyyy");

        //}
    
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        AlllStateCode();
        FillCBDist();
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
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
        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


    }
}