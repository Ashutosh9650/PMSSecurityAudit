using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmMoibileUserRights : System.Web.UI.Page
{
    Comman objComman = new Comman();
    clsMain objMain = new clsMain();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] != null)
        {
            Label lblTitle = (Label)Master.FindControl("lblUser");
            lblTitle.Text = Session["username"].ToString();
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
        if (!IsPostBack)
        {
            fill_userlistbox(sender);
            btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");


        }

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        DataTable sqldt = new DataTable();
        string level = userlist.SelectedValue;
        DataTable dtUserLevel = objComman.Select_All_Data("MstUser", "UserName,FristName + ' (' + UserName +')' as [Name]", "UserLevel =" + userlist.SelectedValue + "", "FristName", "");
        if (dtUserLevel.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('You can not  Deleted because UserRole link in UserMaster');", true);
        }
        else
        {
            int icount = objMain.DeleteUserRole(level);
            if (icount > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Delete Successfully')</script>", false);
                fill_userlistbox(sender);
            }
        }

    }
    public void fill_userlistbox(object sender)
    {
        userlist.Items.Clear();
        DataTable sqldt = new DataTable();
        string condition = " RID in(5) ";
        string fieldname = " * ";
        string orderby = "";
        string sortby = "";
        sqldt = objComman.Select_All_Data("mstuserrole", fieldname, condition, orderby, sortby);
        //sqldt = objBLL.BindFillClusteruser();
        userlist.DataSource = sqldt;
        userlist.DataTextField = "Role";
        userlist.DataValueField = "Role_Level";
        userlist.DataBind();
        if (userlist.Items.Count > 0)
        {
            userlist.SelectedIndex = 0;
            EventArgs ee = new EventArgs();
            userlist_SelectedIndexChanged(sender, ee);
        }
        //Listlevel.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Cluster--", ""));

    }
    public void Filluserpermission()
    {

        DataTable sqldt = new DataTable();
        string condition = "";
        string fieldname = " * ";
        string orderby = "";
        string sortby = " menu_id ";
        sqldt = objComman.Select_All_Data("mstMobilemenu", fieldname, condition, orderby, sortby);

        GV_UserPermission.DataSource = sqldt;
        GV_UserPermission.DataBind();

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < GV_UserPermission.Rows.Count; i++)
            {

                string per_idt = ((Label)GV_UserPermission.Rows[i].FindControl("lbl_perid")).Text;
                bool view = ((CheckBox)GV_UserPermission.Rows[i].FindControl("view_check")).Checked;
                bool edit = ((CheckBox)GV_UserPermission.Rows[i].FindControl("edit_check")).Checked;
                bool delete = ((CheckBox)GV_UserPermission.Rows[i].FindControl("delete_check")).Checked;
                bool Add = ((CheckBox)GV_UserPermission.Rows[i].FindControl("Add_check")).Checked;




                DataSet ds = new DataSet();
                SqlParameter[] paramsToStore = new SqlParameter[6];
                paramsToStore[0] = new SqlParameter("@Role_Id", SqlDbType.VarChar);
                paramsToStore[0].Value = userlist.SelectedValue;
                paramsToStore[1] = new SqlParameter("@PermissionID", SqlDbType.VarChar);
                paramsToStore[1].Value = per_idt;
                paramsToStore[2] = new SqlParameter("@view", SqlDbType.VarChar);
                paramsToStore[2].Value = view;
                paramsToStore[3] = new SqlParameter("@edit", SqlDbType.VarChar);
                paramsToStore[3].Value = edit;
                paramsToStore[4] = new SqlParameter("@delete", SqlDbType.VarChar);
                paramsToStore[4].Value = delete;
                paramsToStore[5] = new SqlParameter("@AddStatus", SqlDbType.VarChar);
                paramsToStore[5].Value = Add;




                int res = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "sp_addrightsmolie", paramsToStore);


                if (res < 0)
                {
                    string script = @"alert('Add Successfully');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", script, true);
                }
                else
                {
                    //lbl_addclust.Text = "Add Not Sucessfully"; 
                }
            }
        }

        catch
        {
        }
    }
    protected void GVUserPermission_RowCreated(object sender, GridViewRowEventArgs e)
    {

        //        Baseline


        #region Basline


        if (e.Row.RowType == DataControlRowType.Header)
        {


            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow.CssClass = "gridnewheadercss";
            TableCell HeaderCell;

            HeaderCell = new TableCell();
            HeaderCell.Text = " ";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 1;

            HeaderCell.CssClass = "gridnewheadercss";
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "Online";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 4;

            HeaderCell.CssClass = "gridnewheadercss";
            HeaderGridRow.Cells.Add(HeaderCell);


            //HeaderCell = new TableCell();
            //HeaderCell.Text = "Offline ";
            //HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            //HeaderCell.ColumnSpan = 4;
            //HeaderGridRow.Cells.Add(HeaderCell);


            GV_UserPermission.Controls[0].Controls.AddAt(0, HeaderGridRow);




        }
        #endregion
    }
    protected void userlist_SelectedIndexChanged(object sender, EventArgs e)
    {
        string level = userlist.SelectedValue;
        DataTable dtreport = new DataTable();
        SqlParameter[] paramsToStore = new SqlParameter[1];
        paramsToStore[0] = new SqlParameter("@level", SqlDbType.VarChar);
        paramsToStore[0].Value = level;


        DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Sp__GetUseRrightMobile]", paramsToStore);

        dtreport = ds.Tables[0] as DataTable; ;
        if (dtreport.Rows.Count > 0)
        {
            GV_UserPermission.DataSource = dtreport;

            GV_UserPermission.DataBind();

            for (int i = 0; i < dtreport.Rows.Count; i++)
            {

                CheckBox V = (CheckBox)GV_UserPermission.Rows[i].FindControl("view_check");
                CheckBox E = (CheckBox)GV_UserPermission.Rows[i].FindControl("edit_check");
                CheckBox A = (CheckBox)GV_UserPermission.Rows[i].FindControl("Add_check");
                CheckBox D = (CheckBox)GV_UserPermission.Rows[i].FindControl("delete_check");



                //CheckBox G = (CheckBox)GV_UserPermission.Rows[i].FindControl("offlineview_check");
                //CheckBox H = (CheckBox)GV_UserPermission.Rows[i].FindControl("offlineedit_check");
                //CheckBox K = (CheckBox)GV_UserPermission.Rows[i].FindControl("offlineAdd_check");
                //CheckBox L = (CheckBox)GV_UserPermission.Rows[i].FindControl("offlinedelete_check");

                if (dtreport.Rows[i]["view_status"].ToString() == "False")
                {

                    V.Checked = false;
                }
                else
                {
                    V.Checked = true;
                }
                if (dtreport.Rows[i]["AddStatus"].ToString() == "False")
                {

                    A.Checked = false;
                }
                else
                {
                    A.Checked = true;
                }
                if (dtreport.Rows[i]["edit_status"].ToString() == "False")
                {

                    E.Checked = false;
                }
                else
                {
                    E.Checked = true;
                }

                if (dtreport.Rows[i]["delete_status"].ToString() == "False")
                {

                    D.Checked = false;
                }
                else
                {
                    D.Checked = true;
                }


                //if (dtreport.Rows[i]["Offlineview_status"].ToString() == "False")
                //{

                //    G.Checked = false;
                //}
                //else
                //{
                //   G.Checked = true;
                //}
                //if (dtreport.Rows[i]["OfflineAddStatus"].ToString() == "False")
                //{

                //    K.Checked = false;
                //}
                //else
                //{
                //    K.Checked = true;
                //}
                //if (dtreport.Rows[i]["Offlineedit_status"].ToString() == "False")
                //{

                //    H.Checked = false;
                //}
                //else
                //{
                //    H.Checked = true;
                //}

                //if (dtreport.Rows[i]["Offlinedelete_status"].ToString() == "False")
                //{

                //    L.Checked = false;
                //}
                //else
                //{
                //    L.Checked = true;
                //}
                if (dtreport.Rows[i]["link"].ToString() == "#")
                {
                    GV_UserPermission.Rows[i].BackColor = System.Drawing.Color.Gray;
                    GV_UserPermission.Rows[i].ForeColor = System.Drawing.Color.White;

                    V.Visible = true;
                    E.Visible = false;
                    A.Visible = false;
                    D.Visible = false;
                    //   G.Visible = false;
                    //H.Visible = false;
                    //K.Visible = false;
                    //L.Visible = false;

                }
                if (dtreport.Rows[i]["ReportId"].ToString() == "1")
                {


                    V.Visible = true;
                    E.Visible = false;
                    A.Visible = false;
                    D.Visible = false;
                    //G.Visible = false;
                    //H.Visible = false;
                    //K.Visible = false;
                    //L.Visible = false;

                }
            }

        }
        else
        {
            Filluserpermission();
        }

    }
    protected void btn_Add_click(object sender, EventArgs e)
    {
        TxtRole.Text = "";
        ddlRoleLevel.SelectedValue = "0";
        ModalPopupExtender1.Show();
    }
    protected void ImgSave_Click(object sender, EventArgs e)
    {
        string RoleName = "";
        string RoleLevel = "";
        if (TxtRole.Text != "")
        {
            RoleName = TxtRole.Text;

        }
        if (ddlRoleLevel.SelectedIndex > 0)
        {
            RoleLevel = Convert.ToString(ddlRoleLevel.SelectedValue);
        }
        DataSet ds = InsertRole(RoleName, RoleLevel);
        if (ds.Tables[0].Rows[0]["RetValue"].ToString() == "Already Exists.")
        {
            string script = @"alert('Role Already Exists.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", script, true);
        }
        else
        {
            string script = @"alert('Add Successfully.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", script, true);
            fill_userlistbox(userlist);
        }
    }
    public DataSet InsertRole(string RoleName, string RoleLevel)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[Sp__Add_UserRolemobile]";
            sqlcmd.Parameters.AddWithValue("@RoleName", RoleName);
            sqlcmd.Parameters.AddWithValue("@RoleLevel", RoleLevel);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw e;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
}