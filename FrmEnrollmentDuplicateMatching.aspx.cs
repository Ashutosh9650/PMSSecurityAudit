using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using System.Data.SqlClient;


public partial class FrmEnrollmentDuplicateMatching : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    string conditions = "";
    Comman objComman = new Comman();
    DataTable dtMain = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {

            if (!IsPostBack)
            {
            

               // LoadData();


                btnSerach_Click(btnSerach, null);
                
              
                  
            }

         
         
        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
    }
    public void ReferPage()
    {
        #region Main

    
      
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "136")
        {
            #region Impact
            ddlBlock.SelectedIndex = 1;
            ddlBlock_OnSelectedIndexChanged(ddlBlock, null);
            ddlBlock.Enabled = true;
           
            if (Convert.ToString(Session["Back"]) == "1")
            {
               
                btnSerach_Click(btnSerach, null);
                Session["Back"] = "";
            }
          
          
            #endregion
        }



        if (Session["user_level"].ToString() == "19" || Session["user_level"].ToString() == "137")
        {
            Session["BlockCodeAct"] = Session["NewBlockCode"].ToString();
         

            if (Convert.ToString(Session["Back"]) == "1")
            {
             
                btnSerach_Click(btnSerach, null);
                Session["Back"] = "";
            }
        }


        #endregion
    }
    protected void ddlBlock_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "136")
            {
                #region Impact
               
              
                Session["BlockName"] = ddlBlock.SelectedItem.Text;
                Session["BlockCodeAct"] = ddlBlock.SelectedValue;
                #endregion
            }

         
        
        }
        catch (Exception)
        {

            throw;
        }
    }
   public void LoadDataBlock(string blockName)
    {


        conditions = "";
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "136")
        {
            string strQry = "";

            strQry = "Select * from mst3Block  where DistrictCode='" + Session["NewDistrictCode"].ToString() + "' and BlockName='" + blockName + "' ";


            DataTable dtBlock = objMain.LoadData(strQry);

            conditions = "  DistrictCode='" + Session["NewDistrictCode"].ToString() + "'  ";



            objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");
            ddlBlock.Enabled = false;
            ddlBlock.SelectedValue = dtBlock.Rows[0]["BlockCode"].ToString();
            Session["BlockName"] = blockName;
            Session["BlockCodeAct"] = dtBlock.Rows[0]["BlockCode"].ToString();
        }
        else
        {

            conditions = conditions + "  DistrictCode='" + Session["NewDistrictCode"].ToString() + "'  and BlockCode ='" + Session["NewBlockCode"].ToString() + "'   and mst2District.FYear ='" + Session["FinYear"].ToString() + "' ";



            objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

            ddlBlock.Enabled = false;

            ddlBlock.SelectedValue = Session["NewBlockCode"].ToString();
            Session["BlockCodeAct"] = Session["NewBlockCode"].ToString();
        }




    }
    public void LoadData()
    {
        
       
        conditions = "";
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "136")
        {
            conditions = "  DistrictCode='" + Session["NewDistrictCode"].ToString() + "' ";

           

            objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");
            ddlBlock.Enabled = true;
        }
        else
        {

            conditions = conditions + "  DistrictCode='" + Session["NewDistrictCode"].ToString() + "'  and BlockCode ='" + Session["NewBlockCode"].ToString() + "' ";

            

            objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");
       
            ddlBlock.Enabled = false;

            ddlBlock.SelectedValue = Session["NewBlockCode"].ToString();
        }

        

        
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["Backlk"] = 1;
        Response.Redirect("~/Enrollmentdashboard.aspx");
    }

   
   
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
      
    }
 


    protected void Export_To_Excel(object sender, EventArgs e)
    {
        DataTable dt= ViewState["dtUserVillage"] as DataTable;
       // ExporttoExcel(DGV_Report, dt);
        

    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
       
        Response.Redirect("~/FrmReportActivityClusterSearch.aspx?ID=" + ddlBlock.SelectedValue + "");
      
    }

   
    
    protected void btnSerach_Click(object sender, EventArgs e)
    {
       
      LoadSerarchSchoolActivity();
     //LoadSerarchSchoolActivityGen();
     
    }
    public DataTable LoadActivtiyAllClusterWise(string userName, string WhereQuery, Int32 Flag, string WhereQuery1)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
		{
		
			new SqlParameter("@userName", userName),
			new SqlParameter("@WhereQuery", WhereQuery),
            		new SqlParameter("@WhereQuery1", WhereQuery1),
            new SqlParameter("@Flag", Flag),
         
		};
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadEnrollmentManualDuplicateMatchingBlockWise]", cmdParameters);
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


    public DataTable LoadActivtiyAllClusterWiseGen(string userName, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
		{
		
			new SqlParameter("@userName", userName),
			new SqlParameter("@WhereQuery", WhereQuery),
         
		};
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadEnrollmentSealSineGenatinClusterWise]", cmdParameters);
    }
    protected void OOD2Dtargetmet_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lblCategory") as LinkButton).Text;
        string lblBlockCode = (gvr.FindControl("lblBlockCode") as Label).Text;
        string lblClusterCode = (gvr.FindControl("lblClusterCode") as Label).Text;


        Response.Redirect("~/FrmSealSignRemoveDuplicate.aspx?ID=" + lblBlockCode + "," + lblClusterCode + "");


    }
    protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string con = "";
            string con1 = "";
            string pub_id = Gv_Profile_Search.DataKeys[e.Row.RowIndex].Value.ToString();
            if (Session["user_level"].ToString() == "19")
            {
                con = " mst5village.BlockCode='" + pub_id + "' ";
                con1 = " mst5village.BlockCode='" + Session["NewBlockCode"].ToString() + "' ";
                dtMain = LoadActivtiyAllClusterWise(pub_id, con, 2, con1);


            }
            if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "136")
            {
                con1 = " mst5village.BlockCode='" + pub_id + "' ";
                con = "mst5village.DistrictCode='" + Session["NewDistrictCode"].ToString() + "' ";
                dtMain = LoadActivtiyAllClusterWise(pub_id, con, 2, con1);


            }
            if (Session["user_level"].ToString() == "145" )
            {
                con1 = " mst5village.BlockCode='" + pub_id + "' ";
                con = "mst5village.DistrictCode in(" + Session["DistrictCodeMul"].ToString() + ")";
                dtMain = LoadActivtiyAllClusterWise(pub_id, con, 2, con1);


            }
            GridView pubTitle = (GridView)e.Row.FindControl("GridView2");
            pubTitle.DataSource = dtMain;
            pubTitle.DataBind();
        }
    }
    public void LoadSerarchSchoolActivity()
    {
        Session["dt"] = null;
     
       
        DataTable dtMain = null;
        string con = "";
        string con1 = "";
      

        if (Session["user_level"].ToString() == "19" || Session["user_level"].ToString() == "137")
        {
            con = " mst5village.BlockCode='" + Session["NewBlockCode"].ToString() + "' ";
            con1 = " BlockCode='" + Session["NewBlockCode"].ToString() + "' ";
            dtMain = LoadActivtiyAllClusterWise(ddlBlock.SelectedValue, con, 1, con1);
           

        }
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "136")
        {
            con1 = " DistrictCode='" + Session["NewDistrictCode"].ToString() + "' ";
            con = "mst5village.DistrictCode='" + Session["NewDistrictCode"].ToString() + "' ";
            dtMain = LoadActivtiyAllClusterWise(ddlBlock.SelectedValue, con, 1, con1);
      

        }
        if (Session["user_level"].ToString() == "145" )
        {
            con1 = " DistrictCode in(" + Session["DistrictCodeMul"].ToString() + ") ";
            con = "mst5village.DistrictCode in(" + Session["DistrictCodeMul"].ToString() + ") ";
            dtMain = LoadActivtiyAllClusterWise(ddlBlock.SelectedValue, con, 1, con1);


        }
        if (dtMain != null)
        {
            if (dtMain.Rows.Count > 0)
            {
                Gv_Profile_Search.DataSource = dtMain;
                Gv_Profile_Search.DataBind();
            }
            else
            {
                Gv_Profile_Search.DataSource = null;
                Gv_Profile_Search.DataBind();
            }
        }

        //    return;
    }


    public void LoadSerarchSchoolActivityGen()
    {
        Session["dt"] = null;
        // DGV_Report.Visible = false;




        //gvGenerAtion.DataSource = null;
        //gvGenerAtion.DataBind();

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
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Max 7 day can be selected')</script>", false);
        //    return;
        //}
        DataTable dtMain = null;
        string con = "";
        string con1 = "";
        //if (Session["user_level"].ToString() == "19")
        //{
        //    con = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and mst5village.BlockCode='" + Session["NewBlockCode"].ToString() + "' ";
        //    dtMain = objMain.LoadSchoolActivtiyCluseter(afromDate, aToDate, ddlBlock.SelectedValue, con);

        //}



        if (Session["user_level"].ToString() == "19" || Session["user_level"].ToString() == "137")
        {
            con = " mst5village.BlockCode='" + Session["NewBlockCode"].ToString() + "' ";

            dtMain = LoadActivtiyAllClusterWiseGen(ddlBlock.SelectedValue, con);
            // dtMain = objMain.LoadSchoolActivtiyForAllTypeReport(afromDate, aToDate, ddlBlock.SelectedValue, con, 2);

        }
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "136")
        {

            con = "mst5village.BlockCode='" + ddlBlock.SelectedValue + "' ";
            dtMain = LoadActivtiyAllClusterWiseGen(ddlBlock.SelectedValue, con);
            //dtMain = objMain.LoadSchoolActivtiyForAllTypeReport(afromDate, aToDate, ddlBlock.SelectedValue, con,3);

        }
        string condation = "";
        //if (Session["user_level"].ToString() == "19" )
        //{
        //     condation = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and UserID='" + ddlBlock.SelectedValue + "' and  UserEntry ='2'  ";
        //}
        // if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" )
        //{
        //      condation= "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and UserID='" + ddlBlock.SelectedValue + "' and  UserEntry ='3' ";
        //}

        // DataTable dtApprove = objMain.LoadSchoolActivtiyApprove(condation);

        // Session["dtApprove"] = dtApprove;

        int count = 0;
        if (dtMain.Rows.Count > 0)
        {
            //#region School


            //string strGSS = "#Children Seal-Sign";
            //DataRow[] dr = dtMain.Select("School='" + strGSS + "'");
            //if (dr.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);


            //    Item1["SRNo"] = 1;
            //    Item1["School"] = "Children Seal-Sign";
            //}

            //string strGSS3 = "#Children Pending Seal-Sign";
            //DataRow[] dr3 = dtMain.Select("School='" + strGSS3 + "'");
            //if (dr3.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);


            //    Item1["SRNo"] = 2;
            //    Item1["School"] = "#Children Pending Seal-Sign";
            //}
            //for (int i = 2; i < 23; i++)
            //{
            //    gvGenerAtion.Columns[i].Visible = false;
            //}
            //for (int i = 2; i < dtMain.Columns.Count; i++)
            //{
            //    gvGenerAtion.Columns[i].Visible = true;
            //    gvGenerAtion.Columns[i].HeaderText = dtMain.Columns[i].ColumnName;
            //}


            //DataView dataview = dtMain.DefaultView;
            //dataview.Sort = "SRNo";
            //DataTable dt = dataview.ToTable();
            //gvGenerAtion.DataSource = dt;
            //gvGenerAtion.DataBind();
            //#endregion

            //    for (int r = 0; r < dt.Rows.Count; r++)
            //    {
            //        decimal total = 0;
            //        for (int i = 2; i < dt.Columns.Count; i++)
            //        {
            //            Label lbl = ((Label)gvGenerAtion.Rows[r].Cells[i].FindControl("lblCol_" + (i + 1)));

            //            if (lbl != null)
            //            {
            //                lbl.Text = Convert.ToString(dt.Rows[r][i]);
            //                if (lbl.Text != "")
            //                {
            //                    total += Convert.ToDecimal(lbl.Text);
            //                }

            //            }
            //        }
            //    }

            //    gvGenerAtion.Rows[2].Visible = false;


            //    // gvVillageActivity.HeaderRow.Cells[count].Text = "T.B.Hand Holding";
            //    DataRow[] drApp = null;

            //    for (int Index = 1; Index < gvGenerAtion.HeaderRow.Cells.Count - 1; Index++)
            //    {
            //        #region ApproveBy
            //        var firstCell = gvGenerAtion.HeaderRow.Cells[Index];

            //        #endregion
            //        firstCell.Controls.Clear();

            //        firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmSealSign.aspx?ID=" + firstCell.Text + "", Text = firstCell.Text });

            //    }


            //}
            //else
            //{
            //    gvGenerAtion.DataSource = null;
            //    gvGenerAtion.DataBind();
            //}

            //    return;
        }


    }

}

