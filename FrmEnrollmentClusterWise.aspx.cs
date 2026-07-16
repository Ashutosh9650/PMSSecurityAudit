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


public partial class FrmEnrollmentClusterWise : System.Web.UI.Page
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
            

                LoadData();



                if (Request.QueryString["rid"] != null)
                {
                    #region #Back
                    string QueryString = Request.QueryString["rid"];
                    string[] a = QueryString.Split(',');
                    //if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
                    //{
                    //    ddlBlock.SelectedIndex = 1;
                    //    ddlBlock.Enabled = true;
                    //}
                    //else
                    //{
                    //    ddlBlock.Enabled = false;
                    //}
                    //if (Session["user_level"].ToString() == "19")
                    //{
                    //    ddlBlock.SelectedValue = Session["NewBlockCode"].ToString();
                    //}
                    //Session["BlockCodeAct"] = ddlBlock.SelectedValue;

                    if (a[0].ToString() == "1" || a[0].ToString() == "2")
                    {
                        Session["RID"] = a[0];
                    }
                    if (a[0].ToString() == "1")
                    {
                        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
                        {
                            ddlBlock.SelectedIndex = 1;
                            ddlBlock.Enabled = true;
                        }
                        else
                        {
                            ddlBlock.Enabled = false;
                        }
                        if (Session["user_level"].ToString() == "19")
                        {
                            ddlBlock.SelectedValue = Session["NewBlockCode"].ToString();
                        }
                        Session["BlockCodeAct"] = ddlBlock.SelectedValue;

                        pnlMain.Visible = true;
                        pnlMain1.Visible = false;
                    }
                    else if (a[0].ToString() == "2")
                    {
                        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
                        {
                            ddlBlock.SelectedIndex = 1;
                            ddlBlock.Enabled = true;
                        }
                        else
                        {
                            ddlBlock.Enabled = false;
                        }
                        if (Session["user_level"].ToString() == "19")
                        {
                            ddlBlock.SelectedValue = Session["NewBlockCode"].ToString();
                        }
                        pnlMain.Visible = false;
                        pnlMain1.Visible = true;
                    }
                    else
                    {
                        if (Session["RID"].ToString() == "1")
                        {
                            pnlMain.Visible = true;
                            pnlMain1.Visible = false;
                            
                        }
                        if (Session["RID"].ToString() == "2")
                        {
                            pnlMain.Visible = true;
                            pnlMain1.Visible = false;
                          
                        }
                        ddlBlock.SelectedValue = Session["BlockCodeAct"].ToString();
                    }

                    btnSerach_Click(btnSerach, null);
                        #endregion
                }
                else
                {
                    #region Main


                    if (Session["RID"].ToString() == "1")
                    {
                        pnlMain.Visible = false;
                        pnlMain1.Visible = true;
                        
                    }
                    if (Session["RID"].ToString() == "2")
                    {
                        pnlMain.Visible = true;
                        pnlMain1.Visible = false;
                    }
                    if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
                    {
                        #region Impact
                        ddlBlock.SelectedIndex = 1;
                        ddlBlock_OnSelectedIndexChanged(ddlBlock, null);
                        ddlBlock.Enabled = true;
                    
                     
                          
                            btnSerach_Click(btnSerach, null);
                        
                        #endregion
                    }
                  
                 

                    //if (Session["user_level"].ToString() == "19")
                    //{
                    //    Session["BlockCodeAct"] = Session["NewBlockCode"].ToString();
                    //    btnSerach_Click(btnSerach, null);

                       
                    //}


                    #endregion
                }
                
              
                  
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

    
      
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
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



        if (Session["user_level"].ToString() == "19")
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
            if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
            {
                #region Impact
               
              
                Session["BlockName"] = ddlBlock.SelectedItem.Text;
                Session["BlockCodeAct"] = ddlBlock.SelectedValue;
                #endregion
            }

         
            btnSerach_Click(btnSerach, null);
            if (Session["RID"].ToString() == "1")
            {
                pnlMain.Visible = true;
                pnlMain1.Visible = false;
            }
            if (Session["RID"].ToString() == "2")
            {
                pnlMain.Visible = false;
                pnlMain1.Visible = true;
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
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
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
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
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
        Response.Redirect("~/FrmActivityBlockWiseSearch.aspx");
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
     LoadSerarchSchoolActivityGen();
     
    }
    public DataTable LoadActivtiyAllClusterWise( string userName, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
		{
		
			new SqlParameter("@userName", userName),
			new SqlParameter("@WhereQuery", WhereQuery),
         
		};
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadEnrollmentSealSineClusterWise]", cmdParameters);
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

    public void LoadSerarchSchoolActivity()
    {
        Session["dt"] = null;
        // DGV_Report.Visible = false;




        Gv_Profile_Search.DataSource = null;
        Gv_Profile_Search.DataBind();

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



        if (Session["user_level"].ToString() == "19")
        {
            con = " mst5village.BlockCode='" + Session["NewBlockCode"].ToString() + "' ";

            dtMain = LoadActivtiyAllClusterWise(ddlBlock.SelectedValue, con);
            // dtMain = objMain.LoadSchoolActivtiyForAllTypeReport(afromDate, aToDate, ddlBlock.SelectedValue, con, 2);

        }
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
        {

            con = "mst5village.BlockCode='" + ddlBlock.SelectedValue + "' ";
            dtMain = LoadActivtiyAllClusterWise(ddlBlock.SelectedValue, con);
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
            #region School


            string strGSS = "#Children Seal-Sign";
            DataRow[] dr = dtMain.Select("School='" + strGSS + "'");
            if (dr.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 1;
                Item1["School"] = "Children Seal-Sign";
            }

            string strGSS3 = "#Children Pending Seal-Sign";
            DataRow[] dr3 = dtMain.Select("School='" + strGSS3 + "'");
            if (dr3.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 2;
                Item1["School"] = "#Children Pending Seal-Sign";
            }
            for (int i = 2; i < 23; i++)
            {
                Gv_Profile_Search.Columns[i].Visible = false;
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

                    if (lbl != null)
                    {
                        lbl.Text = Convert.ToString(dt.Rows[r][i]);
                        if (lbl.Text != "")
                        {
                            total += Convert.ToDecimal(lbl.Text);
                        }

                    }
                }
            }

            Gv_Profile_Search.Rows[2].Visible = false;


            // gvVillageActivity.HeaderRow.Cells[count].Text = "T.B.Hand Holding";
            DataRow[] drApp = null;

            for (int Index = 1; Index < Gv_Profile_Search.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = Gv_Profile_Search.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmSealSignApproval.aspx?ID=" + firstCell.Text + "", Text = firstCell.Text });

            }


        }
        else
        {
            Gv_Profile_Search.DataSource = null;
            Gv_Profile_Search.DataBind();
        }

        //    return;
    }


    public void LoadSerarchSchoolActivityGen()
    {
        Session["dt"] = null;
        // DGV_Report.Visible = false;




        gvGenerAtion.DataSource = null;
        gvGenerAtion.DataBind();

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
     


        if (Session["user_level"].ToString() == "19")
        {
            con = " mst5village.BlockCode='" + Session["NewBlockCode"].ToString() + "' ";

            dtMain = LoadActivtiyAllClusterWiseGen(ddlBlock.SelectedValue, con);
            // dtMain = objMain.LoadSchoolActivtiyForAllTypeReport(afromDate, aToDate, ddlBlock.SelectedValue, con, 2);

        }
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
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
            #region School


            string strGSS = "#Children Seal-Sign";
            DataRow[] dr = dtMain.Select("School='" + strGSS + "'");
            if (dr.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 1;
                Item1["School"] = "Children Seal-Sign";
            }

            string strGSS3 = "#Children Pending Seal-Sign";
            DataRow[] dr3 = dtMain.Select("School='" + strGSS3 + "'");
            if (dr3.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 2;
                Item1["School"] = "#Children Pending Seal-Sign";
            }
            for (int i = 2; i < 23; i++)
            {
                gvGenerAtion.Columns[i].Visible = false;
            }
            for (int i = 2; i < dtMain.Columns.Count; i++)
            {
                gvGenerAtion.Columns[i].Visible = true;
                gvGenerAtion.Columns[i].HeaderText = dtMain.Columns[i].ColumnName;
            }


            DataView dataview = dtMain.DefaultView;
            dataview.Sort = "SRNo";
            DataTable dt = dataview.ToTable();
            gvGenerAtion.DataSource = dt;
            gvGenerAtion.DataBind();
            #endregion

            for (int r = 0; r < dt.Rows.Count; r++)
            {
                decimal total = 0;
                for (int i = 2; i < dt.Columns.Count; i++)
                {
                    Label lbl = ((Label)gvGenerAtion.Rows[r].Cells[i].FindControl("lblCol_" + (i + 1)));

                    if (lbl != null)
                    {
                        lbl.Text = Convert.ToString(dt.Rows[r][i]);
                        if (lbl.Text != "")
                        {
                            total += Convert.ToDecimal(lbl.Text);
                        }

                    }
                }
            }

            gvGenerAtion.Rows[2].Visible = false;


            // gvVillageActivity.HeaderRow.Cells[count].Text = "T.B.Hand Holding";
            DataRow[] drApp = null;

            for (int Index = 1; Index < gvGenerAtion.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = gvGenerAtion.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmSealSign.aspx?ID=" + firstCell.Text + "", Text = firstCell.Text });

            }


        }
        else
        {
            gvGenerAtion.DataSource = null;
            gvGenerAtion.DataBind();
        }

        //    return;
    }

     
    

}

