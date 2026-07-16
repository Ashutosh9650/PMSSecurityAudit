using AjaxControlToolkit;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class FrmActivityDatewiseSearch : System.Web.UI.Page
{
  

     clsMain objMain = new clsMain();

     string conditions = "";

     Comman objComman = new Comman();

   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString( Session["username"]) != "")
        {

            if (!IsPostBack)
            {
                Session["asdsa"] = "";
                Session["Record"] = "";
                ModalPopupExtender1.Hide();
              
                MpexdrDistrict.Hide();
                FillCBState();
                if (Request.QueryString["ID"] != null)
                {
                  
                    string QueryString = Request.QueryString["ID"];
                    string[] array = QueryString.Split(',');
                    TxtFromDate.Text = array[1].ToString();
                    txtDate.Text = array[2].ToString();
                
                    Session["FromData"] = array[1].ToString();
                    Session["Todate"] = array[2].ToString();
                    LoadData(array[0]);
                     Session["CluseterName"] = array[0];
                    btnSerach_Click(btnSerach, null);
                 
                
                }
            }
        }
        else
        {
            base.Response.Redirect("Login.aspx", false);
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        string strQry = "select UserName from mstuser where villagecode='" + ddlCulster.SelectedValue + "' and ActiveStatus=1";

        DataTable dtUser = objMain.LoadData(strQry);
        string Username = dtUser.Rows[0]["UserName"].ToString();
        pnlGridView.Visible = true;
        pnlView.Visible = false;
        DataTable dtMain1 = objMain.mstActivityVillageCheck(Username, ddlAddVillage.SelectedValue, 3);
        if (dtMain1.Rows.Count > 0)
        {
            gvVillage.DataSource = dtMain1;
            gvVillage.DataBind();
        }
        MpexdrDistrict.Show();
    }
    protected void btnNewUserSave_Click(object sender, EventArgs e)
    {
        string strQry = "select UserName from mstuser where villagecode='" + ddlCulster.SelectedValue + "' and ActiveStatus=1";

        DataTable dtUser = objMain.LoadData(strQry);
        string Username = dtUser.Rows[0]["UserName"].ToString();
        DataTable dtMain = objMain.mstActivityVillageCheck(Username, ddlAddVillage.SelectedValue, 1);
        if (dtMain.Rows.Count > 0)
        {
            if (dtMain.Rows.Count >= 8)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('You Add 8 village Allready')</script>", false);
                MpexdrDistrict.Show();
                return;

            }
        }
        DataTable dtMain1 = objMain.mstActivityVillageCheck(Username, ddlAddVillage.SelectedValue, 2);
        if (dtMain1.Rows.Count > 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('You are Allreday Add this village')</script>", false);
            MpexdrDistrict.Show();
            return;


        }
        Int32 iCount = objMain.mstActivityVillageMaster(Username, ddlAddVillage.SelectedValue, ddlAddVillage.SelectedItem.Text);
        if (iCount > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
            
        }
        LoadAllGrid();
    }
    protected void btnRest_Click(object sender, EventArgs e)
    {
        LoadAllGrid();
     }
    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        ImageButton bt = (ImageButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string UniqueChildCode = (gvr.FindControl("lblCUniqueChildCode") as Label).Text;
        string lblUserId = (gvr.FindControl("lblUserId") as Label).Text;


        int res1 = objMain.DeleteActivityVillage(lblUserId, UniqueChildCode);

        if (res1 > 0)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);
        }


    }
    protected void btnAddVillage_Click(object sender, EventArgs e)
    {
        if (ddlCulster.SelectedIndex <= 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select User')</script>", false);
            return;
        }
        ddlState.SelectedIndex = 0;
        ddlDistrict.Items.Clear();
        ddlBlock.Items.Clear();
        ddlPanchayat.Items.Clear();
        ddlAddVillage.Items.Clear();

        pnlGridView.Visible = false;
        pnlView.Visible = true;
        MpexdrDistrict.Show();
        LoadAllGrid();
    }
    public void LoadAllGrid()
    {
        if (Gv_Profile_Search.Rows.Count > 0)
        {

            for (int Index = 1; Index < Gv_Profile_Search.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = Gv_Profile_Search.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmNewSchoolActivity.aspx?ID=" + firstCell.Text + "," + ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        if (gvVillageActivity.Rows.Count > 0)
        {
            for (int Index = 1; Index < gvVillageActivity.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = gvVillageActivity.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmMobileVillageEntry.aspx?ID=" + firstCell.Text + "," + ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        if (gvOffice.Rows.Count > 0)
        {
            for (int Index = 1; Index < gvOffice.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = gvOffice.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmOfficeReport.aspx?ID=" + firstCell.Text + "," + ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }

        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
        MpexdrDistrict.Show();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBBock();
        MpexdrDistrict.Show();
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBCluster();
        MpexdrDistrict.Show();
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillage();
        MpexdrDistrict.Show();
    }
    public void FillCVillage()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlAddVillage, "VillageName", "VillageCode", "--Select--");



    }
    public void FillCBCluster()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "'";
        objComman.BindDLL("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "--Select--");



    }
    public void FillCBBock()
    {
        conditions = "";

        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and FYear ='" + Session["FinYear"].ToString() + "'";


        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");



    }
    public void FillCBDist()
    {

        conditions = "";

        conditions = "StateCode ='" + ddlState.SelectedValue + "' and mst2District.FYear ='" + Session["FinYear"].ToString() + "'";
        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");



    }

    public void FillCBState()
    {
        conditions = "";

        objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");





    }
    protected void ddlCulster_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCulster.SelectedIndex > 0)
        {
            gvVillageOffice.DataSource = null;
            gvVillageOffice.DataBind();

            gvVillageDeatial.DataSource = null;
            gvVillageDeatial.DataBind();
            gvVillageWise.DataSource = null;
            gvVillageWise.DataBind();
            Session["Cluseter"] = ddlCulster.SelectedValue;
            Session["CluseterName"] = ddlCulster.SelectedItem.Text;
            LoadSchoolProfile();
            LoadVIllageProfile();
            LoadSearchOfficeActivtiy();
        }

    }
    public void LoadData(string ClusterName)
    {

        string fromDate = TxtFromDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        string ToDate = txtDate.Text;
        string[] c = ToDate.Split('/');
        string aToDate = c[2] + '-' + c[1] + '-' + c[0];





        string strQry = "";
        if (Session["user_level"].ToString() == "145")
        {
            strQry += " select  ClusterCode,blockCode from mstCluster where BlockCode ='" + Convert.ToString(Session["BlockCodeAct"]) + "' and   ClusterName ='" + ClusterName + "' and  DistrictCode in(" + Session["DistrictCodeMul"].ToString() + ") and  FYear ='" + Session["FinYear"].ToString() + "'   ";

        }
        else
        {
            strQry += " select  ClusterCode,blockCode from mstCluster where BlockCode ='" + Convert.ToString(Session["BlockCodeAct"]) + "' and   ClusterName ='" + ClusterName + "' and  DistrictCode='" + Session["NewDistrictCode"].ToString() + "' and  FYear ='" + Session["FinYear"].ToString() + "'   ";
        }
        DataTable dtCluser = objMain.LoadData(strQry);


        DataTable dtUser = null;
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
        {

            dtUser = objMain.GetActivityClusterLoad(dtCluser.Rows[0]["BlockCode"].ToString(), afromDate, aToDate);

        }
        else
        {
             dtUser = objMain.GetActivityClusterLoad(Session["NewBlockCode"].ToString(), afromDate, aToDate);

        }
        DataView dataview = dtUser.DefaultView;
        dataview.Sort = "BlockName";
        DataTable dt = dataview.ToTable();
        objComman.BindDLLMasterTable("MstUser", "BlockCode ,BlockName ", dt, conditions, "", "", ddlCulster, "BlockName", "BlockCode", "Select");
        ddlCulster.SelectedValue = dtCluser.Rows[0]["ClusterCode"].ToString();
         Session["Cluseter"] =dtCluser.Rows[0]["ClusterCode"].ToString();
      
    }
    public DataTable CreateDataTable()
    {

        DataTable dtRecord = new DataTable();
        dtRecord.Columns.Add("ActivityDate", System.Type.GetType("System.String"));

        dtRecord.Columns.Add("Status", System.Type.GetType("System.Int32"));
        Session["Record"] = dtRecord;
        return dtRecord;
    }

    public void SaveDataDate()
    {
        string fromDate = TxtFromDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        string ToDate = txtDate.Text;
        string[] c = ToDate.Split('/');
        string aToDate = c[2] + '-' + c[1] + '-' + c[0];

        DataTable dtblackData = objMain.GetActivityDistinctAllVillage(afromDate, aToDate,ddlCulster.SelectedValue);
        if (dtblackData.Rows.Count > 0)
        {
            for (int r = 0; r < dtblackData.Rows.Count; r++)
            {
                for (int i = 0; i < Gv_Display.Rows.Count; i++)
                {
                    DropDownList ddlStatus = ((DropDownList)Gv_Display.Rows[i].FindControl("ddlStatus"));
                    Label lbUniqueCode = ((Label)Gv_Display.Rows[i].FindControl("lblUniqueCode"));

                    if (Convert.ToInt32(ddlStatus.SelectedValue) > 0)
                    {
                        DateTime toDate = Convert.ToDateTime(lbUniqueCode.Text);
                        SqlParameter[] parm = new SqlParameter[]
                     {
                   new SqlParameter("@fDate",  toDate.ToString("yyyy-MM-dd")),
                   new SqlParameter("@RemarksActivity",  ddlStatus.SelectedValue),
                   new SqlParameter("@villagecode",  dtblackData.Rows[r]["villagecode"].ToString()),
              
      
                 };
                        int result = Convert.ToInt32(SqlHelper.ExecuteScaler(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "UpdateActivityBlankRecord", parm));



                    }
                }
            }
        }
        else
        {

            string QueryCluseter = "select top 1 mst5Village.Villagecode  from mst5Village ";
            QueryCluseter += "  where mst5Village.ClusterCode='" + ddlCulster.SelectedValue + "'  ";
            DataTable dtBlackCluseter = objMain.LoadData(QueryCluseter);
            string vVillagecode = dtBlackCluseter.Rows[0]["Villagecode"].ToString();

            for (int i = 0; i < Gv_Display.Rows.Count; i++)
            {
                DropDownList ddlStatus = ((DropDownList)Gv_Display.Rows[i].FindControl("ddlStatus"));
                Label lbUniqueCode = ((Label)Gv_Display.Rows[i].FindControl("lblUniqueCode"));

                if (Convert.ToInt32(ddlStatus.SelectedValue) > 0)
                {
                    DateTime toDate = Convert.ToDateTime(lbUniqueCode.Text);
                    SqlParameter[] parm = new SqlParameter[]
                     {
                   new SqlParameter("@fDate",  toDate.ToString("yyyy-MM-dd")),
                   new SqlParameter("@RemarksActivity",  ddlStatus.SelectedValue),
                   new SqlParameter("@villagecode",  vVillagecode),
              
      
                 };
                    int result = Convert.ToInt32(SqlHelper.ExecuteScaler(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "UpdateActivityBlankRecord", parm));



                }
            }
        }



        if (Gv_Profile_Search.Rows.Count > 0)
        {
            for (int Index = 1; Index < Gv_Profile_Search.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = Gv_Profile_Search.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmNewSchoolActivity.aspx?ID=" + firstCell.Text + "," +ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        if (gvVillageActivity.Rows.Count > 0)
        {
            for (int Index = 1; Index < gvVillageActivity.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = gvVillageActivity.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmMobileVillageEntry.aspx?ID=" + firstCell.Text + "," +ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        if (gvOffice.Rows.Count > 0)
        {
            for (int Index = 1; Index < gvOffice.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = gvOffice.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmOfficeReport.aspx?ID=" + firstCell.Text + "," +ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
    }
    protected void btnSaveData_Click(object sender, EventArgs e)
    {

        for (int i = 0; i < Gv_Display.Rows.Count; i++)
        {
            DropDownList ddlStatus = ((DropDownList)Gv_Display.Rows[i].FindControl("ddlStatus"));
            Label lbUniqueCode = ((Label)Gv_Display.Rows[i].FindControl("lblUniqueCode"));

            if (Convert.ToInt32(ddlStatus.SelectedValue) > 0)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select  reason')</script>", false);
                ModalPopupExtender1.Show();
                return;
            }
        }

        SaveDataDate();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        
        if (Gv_Profile_Search.Rows.Count > 0)
        {
            for (int Index = 1; Index < Gv_Profile_Search.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = Gv_Profile_Search.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmNewSchoolActivity.aspx?ID=" + firstCell.Text + "," +ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        if (gvVillageActivity.Rows.Count > 0)
        {
            for (int Index = 1; Index < gvVillageActivity.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = gvVillageActivity.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmMobileVillageEntry.aspx?ID=" + firstCell.Text + "," +ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        if (gvOffice.Rows.Count > 0)
        {
            for (int Index = 1; Index < gvOffice.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = gvOffice.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmOfficeReport.aspx?ID=" + firstCell.Text + "," +ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        ModalPopupExtender1.Hide();
    }
    protected void Gv_Display_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlStatus = ((DropDownList)e.Row.FindControl("ddlStatus"));
            Label lbStatus = ((Label)e.Row.FindControl("lbStatus"));
            ddlStatus.SelectedValue = lbStatus.Text;

         }
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
     
        string fromDate = TxtFromDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        string ToDate = txtDate.Text;
        string[] c = ToDate.Split('/');
        string aToDate = c[2] + '-' + c[1] + '-' + c[0];

        string condation = "";

    
        //DataTable dtEditblackData = objMain.GetActivityDateWiseBlankRecord(afromDate, aToDate,ddlCulster.SelectedValue, 2);
        //if (dtEditblackData.Rows.Count > 0)
        //{
        //    if (dtEditblackData.Rows.Count > 0)
        //    {
        //        Gv_Display.DataSource = dtEditblackData;
        //        Gv_Display.DataBind();
        //        Session["Record"] = dtEditblackData;
        //    }
        //    else
        //    {
        //        Gv_Display.DataSource = null;
        //        Gv_Display.DataBind();
        //    }

        //}
        //else
        //{
            DataTable dtblackData = objMain.GetActivityDateWiseBlankRecord(afromDate, aToDate,ddlCulster.SelectedValue, 7);

            string Query = " SELECT   CONVERT(varchar,dateadd(d,number-1,'" + afromDate + "'),103) as ActivityDate from Numbers WHERE Number<=DATEDIFF(day,('" + afromDate + "'),CONVERT(datetime,'" + aToDate + "')+1)";
            DataTable dtBlackAll = objMain.LoadData(Query);

            if (Session["Record"].ToString() == null || Session["Record"].ToString() == "")
            {
                DataTable dtRecord = CreateDataTable();
                DataRow[] dr3 = null;
               
                for (int r = 0; r < dtBlackAll.Rows.Count; r++)
                {

                    DateTime dateValue = Convert.ToDateTime(dtBlackAll.Rows[r]["ActivityDate"].ToString());
                    string str = dateValue.ToString("ddd");
                    if (str == "Sun")
                    {

                    }
                    else
                    {
                        dr3 = dtblackData.Select("ActivityDate='" + dtBlackAll.Rows[r]["ActivityDate"].ToString() + "'");

                        if (dr3.Length > 0)
                        {


                        }
                        else
                        {
                            DataRow Item1;
                            Item1 = dtRecord.NewRow();
                            dtRecord.Rows.Add(Item1);



                            Item1["ActivityDate"] = dtBlackAll.Rows[r]["ActivityDate"].ToString();
                            Item1["Status"] = 0;
                        }
                    }
                }

                if (dtRecord.Rows.Count > 0)
                {
                    Gv_Display.DataSource = dtRecord;
                    Gv_Display.DataBind();
                    Session["Record"] = dtRecord;
                }
                else
                {
                    Gv_Display.DataSource = null;
                    Gv_Display.DataBind();
                }

            }
            else
            {
                Gv_Display.DataSource = Session["Record"];
                Gv_Display.DataBind();
               
            }
        //}
        if (Gv_Profile_Search.Rows.Count > 0)
        {
            for (int Index = 1; Index < Gv_Profile_Search.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = Gv_Profile_Search.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmNewSchoolActivity.aspx?ID=" + firstCell.Text + "," +ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        if (gvVillageActivity.Rows.Count > 0)
        {
            for (int Index = 1; Index < gvVillageActivity.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = gvVillageActivity.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmMobileVillageEntry.aspx?ID=" + firstCell.Text + "," +ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        if (gvOffice.Rows.Count > 0)
        {
            for (int Index = 1; Index < gvOffice.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = gvOffice.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmOfficeReport.aspx?ID=" + firstCell.Text + "," +ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
    }
    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ( Session["user_level"].ToString() == "24")
        {
             btnApprove.Visible = false;
        }
    }

    

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/FrmActivityDatewiseReport.aspx");
    }

    protected void Gv_Profile_Search_OnRowCommand(object sender, GridViewRowEventArgs e)
    {
    }

    protected void Export_To_Excel(object sender, EventArgs e)
    {
        DataTable table =  ViewState["dtUserVillage"] as DataTable;
         ExporttoExcel( DGV_Report, table);
    }

    private void ExporttoExcel(GridView Gv, DataTable table)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("windows-1250");
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
        int count = Gv.HeaderRow.Cells.Count;
        for (int i = 0; i < count; i++)
        {
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(Gv.HeaderRow.Cells[i].Text);
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");
        }
        HttpContext.Current.Response.Write("</TR>");
        foreach (DataRow dataRow in table.Rows)
        {
            HttpContext.Current.Response.Write("<TR>");
            for (int j = 0; j < table.Columns.Count; j++)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write(dataRow[j].ToString());
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");
        }
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }

   

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if ( Session["user_level"].ToString() == "19")
        {
            Session["Back"] = "1";
            base.Response.Redirect("~/FrmActivityClusterSearchNew.aspx?ID=" + Session["BlockCodeAct"].ToString() + "," + TxtFromDate.Text + "," + txtDate.Text + " ");

            
        }
        if ( Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
        {
            Session["Back"] = "1";
            Response.Redirect("~/FrmActivityClusterSearchNew.aspx?ID=" + Session["BlockCodeAct"].ToString() + "," + TxtFromDate.Text + "," + txtDate.Text + " ");
          
        }
    }

    protected void btnSerach_Click(object sender, EventArgs e)
    {
       
         LoadSchoolProfile();
         LoadVIllageProfile();
         LoadSearchOfficeActivtiy();
    }
    public DataTable LoadActivtiyAllDateNewWise(string fdate, string toDate, string userName, string WhereQuery, string WhereQuery1, Int32 Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fdate", fdate),
            new SqlParameter("@toDate ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery),
                new SqlParameter("@WhereQueryD2d", WhereQuery1),
            new SqlParameter("@Flag", Flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadActivtiyAllDateNewWiseNew20222024]", cmdParameters);
    }
    public void LoadSchoolProfile()
    {
         Session["dt"] = null;
      
         Gv_Profile_Search.Visible = true;
         string fromDate = TxtFromDate.Text;
         string[] d = fromDate.Split('/');
         string afromDate = d[2] + '-' + d[1] + '-' + d[0];

         string ToDate = txtDate.Text;
         string[] c = ToDate.Split('/');
         string aToDate = c[2] + '-' + c[1] + '-' + c[0];

         Gv_Profile_Search.DataSource = null;
         Gv_Profile_Search.DataBind();
         DataTable dtMain = null;
        if ( Session["user_level"].ToString() == "19")
        {
            string whereQuery = "ActivityDate between('" + afromDate + "') and '" + aToDate + "'  and UserEntry=2 and ApproveStatus='FC'  and v.ClusterCode='" + ddlCulster.SelectedValue + "'";

            string whereQuery1 = "ActivityDate between('" + afromDate + "') and '" + aToDate + "'  and ApproveStatus='FC'  and v.ClusterCode='" + ddlCulster.SelectedValue + "'";

            //dtMain = objMain.LoadSchoolActivtiyNew(afromDate, aToDate,ddlCulster.SelectedValue, whereQuery);
            dtMain = LoadActivtiyAllDateNewWise(afromDate, aToDate, ddlCulster.SelectedValue, whereQuery, whereQuery1, 1);
        }
        if ( Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
        {
            string whereQuery = "ActivityDate between('" + afromDate + "') and '" + aToDate + "'  and UserEntry=3 and ApproveStatus='B'  and v.ClusterCode='" + ddlCulster.SelectedValue + "'";
            string whereQuery1 = "ActivityDate between('" + afromDate + "') and '" + aToDate + "'  and ApproveStatus='B'  and v.ClusterCode='" + ddlCulster.SelectedValue + "'";


            //dtMain = objMain.LoadSchoolActivtiyNew(afromDate, aToDate,ddlCulster.SelectedValue, whereQuery);
            //dtMain = objMain.LoadSchoolActivtiyForAllTypeReport(afromDate, aToDate,ddlCulster.SelectedValue, whereQuery,5);
            dtMain = LoadActivtiyAllDateNewWise(afromDate, aToDate, ddlCulster.SelectedValue, whereQuery, whereQuery1, 1);
        }
       if (dtMain.Rows.Count > 0)
        {
            #region School
           
            btnApprove.Visible = true;
            string strGSS = "TB Handholding";
            DataRow[] dr = dtMain.Select("School='" + strGSS + "'");
            if (dr.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 2;
                Item1["School"] = "TB Handholding";
            }

            string strGSS3 = "School Count";
            DataRow[] dr3 = dtMain.Select("School='" + strGSS3 + "'");
            if (dr3.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 1;
                Item1["School"] = "School Count";
            }

            //string strGSS4 = "Retention";
            //DataRow[] dr4 = dtMain.Select("School='" + strGSS4 + "'");
            //if (dr4.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);


            //    Item1["SRNo"] = 3;
            //    Item1["School"] = "Retention";
            //}

            //string strGSS5 = "GKP";
            //DataRow[] dr5 = dtMain.Select("School='" + strGSS5 + "'");
            //if (dr5.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);


            //    Item1["SRNo"] = 4;
            //    Item1["School"] = "GKP";
            //}
            string strGSS56 = "SMC Meeting";
            DataRow[] dr6 = dtMain.Select("School='" + strGSS56 + "'");
            if (dr6.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);

                Item1["SRNo"] = 5;
                Item1["School"] = "SMC Meeting";
            }


            string strGSS1 = "SAC Quarter Update";
            DataRow[] dr1 = dtMain.Select("School='" + strGSS1 + "'");
            if (dr1.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);

                Item1["SRNo"] = 6;

                Item1["School"] = "SAC Quarter Update";
            }
            string strGSS11 = "School infra update";
            DataRow[] dr111 = dtMain.Select("School='" + strGSS11 + "'");
            if (dr111.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);

                Item1["SRNo"] = 6;

                Item1["School"] = "School infra update";
            }



            //string strGSS123 = "Bal Sabha";
            //DataRow[] dr21 = dtMain.Select("School='" + strGSS123 + "'");
            //if (dr21.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);


            //    Item1["SRNo"] = 7;
            //    Item1["School"] = "Bal Sabha";
            //}
            string strGSS1231 = "School Contact";
            DataRow[] dr211 = dtMain.Select("School='" + strGSS1231 + "'");
            if (dr211.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 8;
                Item1["School"] = "School Contact";
            }


            //string strGSS12311 = "Life Skill Game 2";
            //DataRow[] dr2111 = dtMain.Select("School='" + strGSS12311 + "'");
            //if (dr2111.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);


            //    Item1["SRNo"] = 9;
            //    Item1["School"] = "Life Skill Game 2";
            //}
            //string Game3 = "Life Skill Game 3";
            //DataRow[] drGame3 = dtMain.Select("School='" + Game3 + "'");
            //if (drGame3.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);


            //    Item1["SRNo"] = 10;
            //    Item1["School"] = "Life Skill Game 3";
            //}
            //string Game4 = "Life Skill Game 4";
            //DataRow[] drGame4 = dtMain.Select("School='" + Game4 + "'");
            //if (drGame4.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);


            //    Item1["SRNo"] = 11;
            //    Item1["School"] = "Life Skill Game 4";
            //}
            //string Game5 = "Life Skill Game 5";
            //DataRow[] drGame5 = dtMain.Select("School='" + Game5 + "'");
            //if (drGame5.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);


            //    Item1["SRNo"] = 12;
            //    Item1["School"] = "Life Skill Game 5";
            //}


            //string CLt = "CLT";
            //DataRow[] drCLt = dtMain.Select("School='" + CLt + "'");
            //if (drCLt.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);


            //    Item1["SRNo"] = 13;
            //    Item1["School"] = "CLT";
            //}



            //string CLt1 = "Learning Baseline";
            //DataRow[] drCLt1 = dtMain.Select("School='" + CLt1 + "'");
            //if (drCLt1.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);


            //    Item1["SRNo"] = 14;
            //    Item1["School"] = "Learning Baseline";
            //}

            //string CLt2 = "Learning  Midline";
            //DataRow[] drCLt2 = dtMain.Select("School='" + CLt2 + "'");
            //if (drCLt2.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);


            //    Item1["SRNo"] = 15;
            //    Item1["School"] = "Learning  Midline";
            //}

            //string CLt3 = "Learning  Endline";
            //DataRow[] drCLt3 = dtMain.Select("School='" + CLt3 + "'");
            //if (drCLt3.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);


            //    Item1["SRNo"] = 16;
            //    Item1["School"] = "Learning  Endline";

            //}

            //string CLt4 = "Learning  Endline";
            //DataRow[] drCLt4 = dtMain.Select("School='" + CLt4 + "'");
            //if (drCLt4.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);


            //    Item1["SRNo"] = 16;
            //    Item1["School"] = "Learning  Endline";
            //}


            string CLt5 = "Other Activity";
            DataRow[] drCLt5 = dtMain.Select("School='" + CLt5 + "'");
            if (drCLt5.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 17;
                Item1["School"] = "Other Activity";
            }

            for (int i = 2; i < dtMain.Columns.Count; i++)
            {
                Gv_Profile_Search.Columns[i].Visible = true;
                Gv_Profile_Search.Columns[i].HeaderText = dtMain.Columns[i].ColumnName;
            }


            DataView dataview = dtMain.DefaultView;
            dataview.Sort = "SRNo";
            DataTable dt = dataview.ToTable();
            Gv_Profile_Search.DataSource = dt;
            Gv_Profile_Search.DataBind();
            #endregion

            for (int r = 0; r < dt.Rows.Count; r++)
            {
                decimal total = 0;
                for (int i = 2; i < dt.Columns.Count; i++)
                {
                    Label lbl = ((Label)Gv_Profile_Search.Rows[r].Cells[i].FindControl("lblCol_" + (i + 1)));
                    Label TxtTotla = ((Label)Gv_Profile_Search.Rows[r].Cells[i].FindControl("TxtTotla"));
                    if (lbl != null)
                    {
                        lbl.Text = Convert.ToString(dt.Rows[r][i]);
                        if (lbl.Text != "")
                        {
                            total += Convert.ToDecimal(lbl.Text);
                        }
                        if (total == 0)
                        {
                        }
                        else
                        {
                            TxtTotla.Text = total.ToString();
                        }
                    }
                }
            }

            Gv_Profile_Search.Rows[7].Visible = false;
           
            // gvVillageActivity.HeaderRow.Cells[count].Text = "T.B.Hand Holding";
            DataRow[] drApp = null;
            for (int Index = 1; Index < Gv_Profile_Search.HeaderRow.Cells.Count ; Index++)
            {
                #region ApproveBy
                var firstCell = Gv_Profile_Search.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmNewSchoolActivity.aspx?ID=" + firstCell.Text + "," +ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        else
        {
            Gv_Profile_Search.DataSource = null;
            Gv_Profile_Search.DataBind();
        }
       
    }

    public void LoadVIllageProfile()
    {
        string fromDate = TxtFromDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        string ToDate = txtDate.Text;
        string[] c = ToDate.Split('/');
        string aToDate = c[2] + '-' + c[1] + '-' + c[0];


        //DateTime d1 = Convert.ToDateTime(afromDate);
        //DateTime d2 = Convert.ToDateTime(aToDate);
        //int month = Convert.ToInt32(c[1]) - Convert.ToInt32(d[1]);
        //TimeSpan t = d2 - d1;

        //double Days = Convert.ToDouble(t.TotalDays);
        //if (Math.Sign(Days) == -1)
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Vaild Date')</script>", false);
        //    return;
        //}
        //if (Math.Round(Days) >= 7)
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date rang 7 Day')</script>", false);
        //    return;
        //}
        string con = " ";
        DataTable dtMain = null;
        string con1 = " ";
        if (Session["user_level"].ToString() == "19")
        {
            con = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and UserEntry=3 and ApproveStatus='FC'  and mstCluster.ClusterCode='" + ddlCulster.SelectedValue + "' ";
            con1 = "ActivityDate between('" + afromDate + "') and '" + aToDate + "'  and mstCluster.ClusterName='" +ddlCulster.SelectedValue + "' ";

            //dtMain = objMain.LoadVIllageActivtiyNew(afromDate, aToDate,ddlCulster.SelectedValue, con, con1);
            dtMain = objMain.LoadActivtiyAllDateNewWise(afromDate, aToDate, ddlCulster.SelectedValue, con, con1, 2);
        }
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
        {
            con = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and UserEntry=3 and ApproveStatus='B'  and mstCluster.ClusterCode='" + ddlCulster.SelectedValue + "' ";
            con1 = "ActivityDate between('" + afromDate + "') and '" + aToDate + "'  and mstCluster.ClusterCode='" + ddlCulster.SelectedValue + "' ";
          
            // dtMain = objMain.LoadVillageActivtiyCluseterIO(afromDate, aToDate, ddlBlock.SelectedValue, con);
            //dtMain = objMain.LoadVIllageActivtiyNew(afromDate, aToDate,ddlCulster.SelectedValue, con, con1);
            dtMain = objMain.LoadActivtiyAllDateNewWise(afromDate, aToDate, ddlCulster.SelectedValue, con, con1, 2);
        }


        int count = 0;
     
        if (dtMain.Rows.Count > 0)
        {
            btnApprove.Visible = true;
         
            string strGSS = "Village Count";
            DataRow[] dr = dtMain.Select("Village='" + strGSS + "'");
            if (dr.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();

                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Village Count";
                Item1["SRNo"] = 1;
            }

            string strGSS3 = "TB Handholding";
            DataRow[] dr3 = dtMain.Select("Village='" + strGSS3 + "'");
            if (dr3.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "TB Handholding";
                Item1["SRNo"] = 2;
            }

            string strGSS4 = "GSS";
            DataRow[] dr4 = dtMain.Select("Village='" + strGSS4 + "'");
            if (dr4.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "GSS";
                Item1["SRNo"] = 3;
            }
            string strGSS41 = "MM";
            DataRow[] dr41 = dtMain.Select("Village='" + strGSS41 + "'");
            if (dr41.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "MM";
                Item1["SRNo"] = 4;
            }

            string strGSS5 = "Other Community Meeting 1";
            DataRow[] dr5 = dtMain.Select("Village='" + strGSS5 + "'");
            if (dr5.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Other Community Meeting 1";
                Item1["SRNo"] = 5;
            }

            string strGSS56 = "Other Community Meeting 2";
            DataRow[] dr56 = dtMain.Select("Village='" + strGSS56 + "'");
            if (dr56.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Other Community Meeting 2";
                Item1["SRNo"] = 6;
            }
            string strGSS562 = "Community Contact";
            DataRow[] dr6 = dtMain.Select("Village='" + strGSS562 + "'");
            if (dr6.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Community Contact";
                Item1["SRNo"] = 7;
            }

            //string strGSS5621 = "Enrollment (6 yrs)";
            //DataRow[] dr61 = dtMain.Select("Village='" + strGSS5621 + "'");
            //if (dr61.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);



            //    Item1["Village"] = "Enrollment (6 yrs)";
            //    Item1["SRNo"] = 8;
            //}
            //string strGSS56211 = "Enrollment (7-14 yrs)";
            //DataRow[] dr611 = dtMain.Select("Village='" + strGSS56211 + "'");
            //if (dr611.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);



            //    Item1["Village"] = "Enrollment (7-14 yrs)";
            //    Item1["SRNo"] = 9;
            //}

            //string strGSS562111 = "Ineligible";
            //DataRow[] dr6111 = dtMain.Select("Village='" + strGSS562111 + "'");
            //if (dr6111.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);



            //    Item1["Village"] = "Ineligible";
            //    Item1["SRNo"] = 10;
            //}


            string strGSS1 = "Support";
            DataRow[] dr1 = dtMain.Select("Village='" + strGSS1 + "'");
            if (dr1.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Support";
                Item1["SRNo"] = 11;
            }
            string strGSS11 = "Other Activity";
            DataRow[] dr11 = dtMain.Select("Village='" + strGSS11 + "'");
            if (dr11.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Other Activity";
                Item1["SRNo"] = 12;
            }


            for (int i = 2; i < dtMain.Columns.Count; i++)
            {
                gvVillageActivity.Columns[i].Visible = true;
                gvVillageActivity.Columns[i].HeaderText = dtMain.Columns[i].ColumnName;
            }


            DataView dataview = dtMain.DefaultView;
            dataview.Sort = "SRNo";
            DataTable dt = dataview.ToTable();
            gvVillageActivity.DataSource = dt;
            gvVillageActivity.DataBind();

            for (int r = 0; r < dt.Rows.Count; r++)
            {
                decimal total = 0;
                for (int i = 2; i < dt.Columns.Count; i++)
                {
                    Label lbl = ((Label)gvVillageActivity.Rows[r].Cells[i].FindControl("lblColV_" + (i + 1)));
                    Label TxtTotla = ((Label)gvVillageActivity.Rows[r].Cells[i].FindControl("TxtTotlaV"));
                    if (lbl != null)
                    {
                        lbl.Text = Convert.ToString(dt.Rows[r][i]);
                        if (lbl.Text != "")
                        {
                            total += Convert.ToDecimal(lbl.Text);
                        }
                        if (total == 0)
                        {
                        }
                        else
                        {
                            TxtTotla.Text = total.ToString();
                        }
                    }
                }
            }


            // gvVillageActivity.HeaderRow.Cells[count].Text = "T.B.Hand Holding";
            DataRow[] drApp = null;

            gvVillageActivity.Rows[9].Visible = false;
            for (int Index = 1; Index < gvVillageActivity.HeaderRow.Cells.Count; Index++)
            {
                #region ApproveBy
                var firstCell = gvVillageActivity.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmMobileVillageEntry.aspx?ID=" + firstCell.Text + "," +ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }

          
        }
        else
        {
            gvVillageActivity.DataSource = null;
            gvVillageActivity.DataBind();
        }

    }


    public void LoadSearchOfficeActivtiy()
    {
        Session["dt"] = null;

        //if (ddlBlock.SelectedIndex <= 0)
        //{

        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select User')</script>", false);
        //    return;
        //}


        string fromDate = TxtFromDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        string ToDate = txtDate.Text;
        string[] c = ToDate.Split('/');
        string aToDate = c[2] + '-' + c[1] + '-' + c[0];


        //DateTime d1 = Convert.ToDateTime(afromDate);
        //DateTime d2 = Convert.ToDateTime(aToDate);
        //int month = Convert.ToInt32(c[1]) - Convert.ToInt32(d[1]);
        //TimeSpan t = d2 - d1;

        //double Days = Convert.ToDouble(t.TotalDays);
        //if (Math.Sign(Days) == -1)
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Vaild Date')</script>", false);
        //    return;
        //}
        //if (Math.Round(Days) >= 7)
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date rang 7 Day')</script>", false);
        //    return;
        //}
        string con = " ";
        DataTable dtMain = null;

        if (Session["user_level"].ToString() == "19")
        {
            con = "ActivityDate between('" + afromDate + "') and '" + aToDate + "'  and ApproveStatus='FC'  and mstCluster.ClusterCode='" +ddlCulster.SelectedValue + "' ";
            //dtMain = objMain.LoadOfficeActivtiyNew(afromDate, aToDate,ddlCulster.SelectedValue, con);
            dtMain = objMain.LoadActivtiyAllDateNewWise(afromDate, aToDate, ddlCulster.SelectedValue, con, "", 3);
        }
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
        {
            con = "ActivityDate between('" + afromDate + "') and '" + aToDate + "'  and ApproveStatus='B'  and mstCluster.ClusterCode='" + ddlCulster.SelectedValue + "' ";
            // dtMain = objMain.LoadVillageActivtiyCluseterIO(afromDate, aToDate, ddlBlock.SelectedValue, con);
            //dtMain = objMain.LoadOfficeActivtiyNew(afromDate, aToDate,ddlCulster.SelectedValue, con);
            dtMain = objMain.LoadActivtiyAllDateNewWise(afromDate, aToDate, ddlCulster.SelectedValue, con, "", 3);
        }
        int count = 0;
      
        if (dtMain.Rows.Count > 0)
        {
            btnApprove.Visible = true;
          
            string strGSSVillage = "Village Count";
            DataRow[] drGSSVillage = dtMain.Select("Village='" + strGSSVillage + "'");
            if (drGSSVillage.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Village Count";
                Item1["SRNo"] = "1";

            }

            string strGSS = "Meeting";
            DataRow[] dr = dtMain.Select("Village='" + strGSS + "'");
            if (dr.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Meeting";
                Item1["SRNo"] = "2";

            }

            string strGSS3 = "Other_specify";
            DataRow[] dr3 = dtMain.Select("Village='" + strGSS3 + "'");
            if (dr3.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Other_specify";
                Item1["SRNo"] = "4";
            }

            string strGSS4 = "Training";
            DataRow[] dr4 = dtMain.Select("Village='" + strGSS4 + "'");
            if (dr4.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Training";
                Item1["SRNo"] = "3";
            }

            //string strGSS5 = "Other Community Meeting";


            for (int i = 2; i < dtMain.Columns.Count; i++)
            {
                gvOffice.Columns[i].Visible = true;
                gvOffice.Columns[i].HeaderText = dtMain.Columns[i].ColumnName;
            }

            DataView dataview = dtMain.DefaultView;
            dataview.Sort = "SRNo";
            DataTable dt = dataview.ToTable();
            gvOffice.DataSource = dt;
            gvOffice.DataBind();

            for (int r = 0; r < dt.Rows.Count; r++)
            {
                decimal total = 0;
                for (int i = 2; i < dt.Columns.Count; i++)
                {
                    Label lbl = ((Label)gvOffice.Rows[r].Cells[i].FindControl("lblColO_" + (i + 1)));
                    Label TxtTotla = ((Label)gvOffice.Rows[r].Cells[i].FindControl("TxtTotlaO"));
                    if (lbl != null)
                    {
                        lbl.Text = Convert.ToString(dt.Rows[r][i]);
                        if (lbl.Text != "")
                        {
                            total += Convert.ToDecimal(lbl.Text);
                        }
                        if (total == 0)
                        {
                        }
                        else
                        {
                            TxtTotla.Text = total.ToString();
                        }
                    }
                }
            }


            // gvVillageActivity.HeaderRow.Cells[count].Text = "T.B.Hand Holding";
            DataRow[] drApp = null;
          
            for (int Index = 1; Index < gvOffice.HeaderRow.Cells.Count ; Index++)
            {
                #region ApproveBy
                var firstCell = gvOffice.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmOfficeReport.aspx?ID=" + firstCell.Text + "," +ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }
       
        }
        else
        {
            gvOffice.DataSource = null;
            gvOffice.DataBind();
        }

    }



    protected void LnkSchool_OnClick(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string con1 = "";
        string con2 = "";
        string UniqueCode = (gvr.FindControl("lblUn1") as Label).Text;

        if (UniqueCode == "GKP")
        {
            con2 = " and LEN(LevelID) >0   ";
        }
        if (UniqueCode == "SIP Annual")
        {
            con1 = "and tblActivityUpdate_School.SIP_Annual>0 ";
        }
        else if (UniqueCode == "Retention")
        {
            con1 = "and tblActivityUpdate_School.Retention_Annual>0 ";
        }
        else if (UniqueCode == "SMC Orientation")
        {
            con1 = " and  tblActivityUpdate_School.SMC >0 ";
        }
        else if (UniqueCode == "SMC Meeting")
        {
            con1 = " and  tblActivityUpdate_School.SMC_Meeting >0 ";
        }
        else if (UniqueCode == "School infra update")
        {
            con1 = " and  tblActivityUpdate_School.Infrastructure >0  ";
        }
        else if (UniqueCode == "SAC Quarter Update")
        {
            con1 = " and  tblActivityUpdate_School.SACUpdate >0  ";
        }
        else if (UniqueCode == "Bal Sabha")
        {
            con1 = " and  tblActivityUpdate_School.BalSabha >0  ";
        }
        else if (UniqueCode == "School Contact")
        {
            con1 = " and    len(SchoolContactOption)>0   ";
        }
        //else if (UniqueCode == "Life Skill Game 1")
        //{
        //    con1 = "and   LifeSkillGameEntry like '%1%' and  Lifeskill_Games>0  ";
        //}
        //else if (UniqueCode == "Life Skill Game 2")
        //{
        //    con1 = " and   LifeSkillGameEntry like '%2%' and  Lifeskill_Games>0  ";
        //}
        //else if (UniqueCode == "Life Skill Game 3")
        //{
        //    con1 = " and   LifeSkillGameEntry like '%3%' and  Lifeskill_Games>0  ";
        //}
        //else if (UniqueCode == "Life Skill Game 4")
        //{
        //    con1 = " and   LifeSkillGameEntry like '%4%' and  Lifeskill_Games>0  ";
        //}
        //else if (UniqueCode == "Life Skill Game 5")
        //{
        //    con1 = " and   LifeSkillGameEntry like '%5%' and  Lifeskill_Games>0  ";
        //}
        else if (UniqueCode == "CLT")
        {
            con1 = "and  tblActivityUpdate_School.CLT>0  ";
        }
        else if (UniqueCode == "Learning Baseline")
        {
            con1 = " and  CLT_Pretest>0 ";
        }
        else if (UniqueCode == "Learning Midline" || UniqueCode == "Learning  Midline")
        {
            con1 = "  and    CTL_Midtest>0 ";
        }
        else if (UniqueCode == "Learning Endline" || UniqueCode == "Learning  Endline")
        {
            con1 = " and  CLT_Posttest>0";
        }
        else if (UniqueCode == "Other Activity")
        {
            con1 = "  and    len(Others_Description)>0 ";
        }
        string fromDate = TxtFromDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        string ToDate = txtDate.Text;
        string[] c = ToDate.Split('/');
        string aToDate = c[2] + '-' + c[1] + '-' + c[0];
        string con;


        DataTable dtMain = null;
        if (Session["user_level"].ToString() == "19")
        {
            if (con2.Length > 0)
            {
                con = " where ActivityDate between('" + afromDate + "') and '" + aToDate + "' and ApproveStatus='FC'  and mst5village.ClusterCode='" + ddlCulster.SelectedValue + "'  ";
                dtMain = objMain.GetGKPWiseActivity(con + con2);

            }
            else
            {
                con = " where ActivityDate between('" + afromDate + "') and '" + aToDate + "' and UserEntry=2 and ApproveStatus='FC'  and mst5village.ClusterCode='" + ddlCulster.SelectedValue + "' ";
                dtMain = objMain.GetSchoolActivtiy(con + con1);
            }

        }
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
        {
            if (con2.Length > 0)
            {
                con = " where ActivityDate between('" + afromDate + "') and '" + aToDate + "' and ApproveStatus='B'   and mst5village.ClusterCode='" + ddlCulster.SelectedValue + "' ";
                dtMain = objMain.GetGKPWiseActivity(con + con2);
            }
            else
            {
                con = "where ActivityDate between('" + afromDate + "') and '" + aToDate + "' and UserEntry=3 and ApproveStatus='B' and mst5village.ClusterCode='" + ddlCulster.SelectedValue + "' ";
                dtMain = objMain.GetSchoolActivtiy(con + con1);
            }
            // dtMain = objMain.LoadSchoolActivtiyCluseterIO(afromDate, aToDate, ddlBlock.SelectedValue, con);
        }

        if (dtMain.Rows.Count > 0)
        {
            gvVillageWise.DataSource = dtMain;
            gvVillageWise.DataBind();
        }
        else
        {
            gvVillageWise.DataSource = null;
            gvVillageWise.DataBind();
        }


        gvVillageDeatial.Visible = false;
        gvVillageWise.Visible = true;
        gvVillageOffice.Visible = false;
       
        if (Gv_Profile_Search.Rows.Count > 0)
        {

            for (int Index = 1; Index < Gv_Profile_Search.HeaderRow.Cells.Count; Index++)
            {
                #region ApproveBy
                var firstCell = Gv_Profile_Search.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmNewSchoolActivity.aspx?ID=" + firstCell.Text + "," + ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        if (gvVillageActivity.Rows.Count > 0)
        {
            for (int Index = 1; Index < gvVillageActivity.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = gvVillageActivity.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmMobileVillageEntry.aspx?ID=" + firstCell.Text + "," +ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        if (gvOffice.Rows.Count > 0)
        {
            for (int Index = 1; Index < gvOffice.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = gvOffice.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmOfficeReport.aspx?ID=" + firstCell.Text + "," +ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }
      
        }
        ModalPopupExtender43.Show();
    }
    protected void hhd_click(object sender, EventArgs e)
    {
        if (Gv_Profile_Search.Rows.Count > 0)
        {

            for (int Index = 1; Index < Gv_Profile_Search.HeaderRow.Cells.Count; Index++)
            {
                #region ApproveBy
                var firstCell = Gv_Profile_Search.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmNewSchoolActivity.aspx?ID=" + firstCell.Text + "," + ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        if (gvVillageActivity.Rows.Count > 0)
        {
            for (int Index = 1; Index < gvVillageActivity.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = gvVillageActivity.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmMobileVillageEntry.aspx?ID=" + firstCell.Text + "," + ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        if (gvOffice.Rows.Count > 0)
        {
            for (int Index = 1; Index < gvOffice.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = gvOffice.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmOfficeReport.aspx?ID=" + firstCell.Text + "," + ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }

        }
    }
        protected void LnkVillage_OnClick(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string con1 = "";
        Int32 Flag = 1;
        string UniqueCode = (gvr.FindControl("lblvllV_2") as Label).Text;
        if (UniqueCode == "TB Handholding")
        {
            con1 = " and TBHandholding >0 ";
        }
        else if (UniqueCode == "GSS")
        {
            con1 = "and  len(GSS_Agenda)>0   and GSSEnrollHault=1    ";
        }
        else if (UniqueCode == "MM")
        {
            con1 = " and  MM_Mtg>0   ";
        }
        else if (UniqueCode == "Other Community Meeting 1")
        {
            con1 = " and  Com_mtg>0  ";
        }
        else if (UniqueCode == "Other Community Meeting 2")
        {
            con1 = " and  Com_mtg2>0   ";
        }
        else if (UniqueCode == "Community Contact")
        {
            con1 = " and  ComContact>0  ";
        }



        else if (UniqueCode == "Enrollment (6 yrs)")
        {
            con1 = " and  ActivityStatus=5  and AgeAson =6";
            Flag = 2;
        }
        else if (UniqueCode == "Enrollment (7-14 yrs)")
        {
            con1 = "and    ActivityStatus=5  and AgeAson >=7 and AgeAson <=14   ";
            Flag = 2;
        }
        else if (UniqueCode == "Ineligible")
        {
            con1 = "and    ActivityStatus=3    ";
            Flag = 3;
        }

        else if (UniqueCode == "Support")
        {
            con1 = " and    Support>0  ";
        }
        else if (UniqueCode == "Other Activity")
        {
            con1 = " and    len(Others_Desc)>1   ";
        }

        string fromDate = TxtFromDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        string ToDate = txtDate.Text;
        string[] c = ToDate.Split('/');
        string aToDate = c[2] + '-' + c[1] + '-' + c[0];
        string con;


        DataTable dtMain = null;
        if (Session["user_level"].ToString() == "19")
        {
            con = " where ActivityDate between('" + afromDate + "') and '" + aToDate + "' and ApproveStatus='FC' and UserEntry=2 and mst5village.ClusterCode='" + ddlCulster.SelectedValue + "' ";
            string d2d = "where ActivityDate between('" + afromDate + "') and '" + aToDate + "' and mst5village.ClusterCode='" + ddlCulster.SelectedValue + "'";
            if (Flag == 1)
            {
                dtMain = objMain.GeVillageActivtiy(con + con1, Flag);
            }
            else
            {
                dtMain = objMain.GeVillageActivtiy(d2d + con1, Flag);
            }

        }
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
        {
            con = "where ActivityDate between('" + afromDate + "') and '" + aToDate + "' and ApproveStatus='B' and UserEntry=3 and mst5village.ClusterCode='" + ddlCulster.SelectedValue + "' ";
            string d2d = "where ActivityDate between('" + afromDate + "') and '" + aToDate + "' and mst5village.ClusterCode='" + ddlCulster.SelectedValue + "'";
            if (Flag == 1)
            {
                dtMain = objMain.GeVillageActivtiy(con + con1, Flag);
            }
            else
            {
                dtMain = objMain.GeVillageActivtiy(d2d + con1, Flag);
            }
        }

        if (dtMain.Rows.Count > 0)
        {
            gvVillageDeatial.DataSource = dtMain;
            gvVillageDeatial.DataBind();
        }
        else
        {
            gvVillageDeatial.DataSource = null;
            gvVillageDeatial.DataBind();

        }
        gvVillageOffice.Visible = false;
        gvVillageDeatial.Visible = true;
        gvVillageWise.Visible = false;
      
        if (Gv_Profile_Search.Rows.Count > 0)
        {

            for (int Index = 1; Index < Gv_Profile_Search.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = Gv_Profile_Search.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmNewSchoolActivity.aspx?ID=" + firstCell.Text + "," +ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        if (gvVillageActivity.Rows.Count > 0)
        {
            for (int Index = 1; Index < gvVillageActivity.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = gvVillageActivity.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmMobileVillageEntry.aspx?ID=" + firstCell.Text + "," +ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        if (gvOffice.Rows.Count > 0)
        {
            for (int Index = 1; Index < gvOffice.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = gvOffice.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmOfficeReport.aspx?ID=" + firstCell.Text + "," +ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }

        }
        ModalPopupExtender43.Show();
    }
    protected void LnkOffice_OnClick(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string UniqueCode = (gvr.FindControl("lbooff") as Label).Text;
        string con1 = "";
        string con = "";
        DataTable dtMain = null;
        if (UniqueCode == "Meeting")
        {
            con1 = " and Meeting>0  ";
        }
        else if (UniqueCode == "Training")
        {
            con1 = "and Training>0  ";
        }
        else if (UniqueCode == "Other Activity")
        {
            con1 = " and   Other_FC>0  ";
        }
        string fromDate = TxtFromDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        string ToDate = txtDate.Text;
        string[] c = ToDate.Split('/');
        string aToDate = c[2] + '-' + c[1] + '-' + c[0];
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
        {
            con = "where ActivityDate between('" + afromDate + "') and '" + aToDate + "' and ApproveStatus='B' and mst5village.ClusterCode='" + ddlCulster.SelectedValue + "' ";

            dtMain = objMain.GetOfficeWiseActivity(con + con1);

        }
        if (Session["user_level"].ToString() == "19")
        {
            con = "where ActivityDate between('" + afromDate + "') and '" + aToDate + "' and ApproveStatus='FC' and mst5village.ClusterCode='" + ddlCulster.SelectedValue + "' ";

            dtMain = objMain.GetOfficeWiseActivity(con + con1);

        }
        if (dtMain.Rows.Count > 0)
        {
            gvVillageOffice.DataSource = dtMain;
            gvVillageOffice.DataBind();
        }
        else
        {
            gvVillageOffice.DataSource = null;
            gvVillageOffice.DataBind();


        }
        if (Gv_Profile_Search.Rows.Count > 0)
        {

            for (int Index = 1; Index < Gv_Profile_Search.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = Gv_Profile_Search.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmNewSchoolActivity.aspx?ID=" + firstCell.Text + "," +ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        if (gvVillageActivity.Rows.Count > 0)
        {
            for (int Index = 1; Index < gvVillageActivity.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = gvVillageActivity.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmMobileVillageEntry.aspx?ID=" + firstCell.Text + "," +ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        if (gvOffice.Rows.Count > 0)
        {
            for (int Index = 1; Index < gvOffice.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = gvOffice.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./frmOfficeReport.aspx?ID=" + firstCell.Text + "," +ddlCulster.SelectedValue + "," + txtDate.Text + "", Text = firstCell.Text });

            }

        }
        gvVillageOffice.Visible = true;
        gvVillageDeatial.Visible = false;
        gvVillageWise.Visible = false;
        ModalPopupExtender43.Show();
    }
}
