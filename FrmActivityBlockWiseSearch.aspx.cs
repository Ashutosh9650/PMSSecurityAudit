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

public partial class FrmActivityBlockWiseSearch : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    string conditions = "";
    Comman objComman = new Comman();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {

            if (!IsPostBack)
            {
              
               
                DataTable dtMain = objMain.GetActivityUpdateDateWiseDistWise(Session["NewDistrictCode"].ToString(), "3", "B");
                if (dtMain.Rows.Count > 0 && dtMain.Rows[0]["ActivityDate"].ToString() !="")
                {
                    #region DataSelection
                    DateTime Activitydate = Convert.ToDateTime(dtMain.Rows[0]["ActivityDate"].ToString());
                    TxtFromDate.Text = Activitydate.ToString("dd/MM/yyyy");
                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    string fromDate = TxtFromDate.Text;

                    string[] d = fromDate.Split('/');
                    string afromDate = d[2] + '-' + d[1] + '-' + d[0];
                    DateTime dttest = Activitydate.AddDays(14);
                    txtDate.Text = dttest.ToString("dd/MM/yyyy");
                    string ToDate = txtDate.Text;
                    string[] c = ToDate.Split('/');
                    string aToDate = c[2] + '-' + c[1] + '-' + c[0];
                    Int32 maxdate = Convert.ToInt32(c[0]);
                    if (d[1].ToString() != "")
                    {

                        if (Convert.ToInt32(d[2]) != 3)
                        {

                            if (Convert.ToInt32(d[0]) <= 25 && Convert.ToInt32(d[1]) != Convert.ToInt32(c[1]))
                            {
                                maxdate = (25 / Convert.ToInt32(d[1])) / Convert.ToInt32(d[2]);
                                string data = Convert.ToInt32(d[2]).ToString() + '-' + d[1] + '-' + "25";
                                //DateTime dttest1 = Activitydate.AddDays(maxdate);

                                //txtDate.Text = dttest1.ToString("dd/MM/yyyy");
                                DateTime dttest1 = Convert.ToDateTime(data);
                                txtDate.Text = dttest1.ToString("dd/MM/yyyy");

                                maxdate = 25;
                            }
                            else
                            {
                                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            }



                        }
                        else if (Convert.ToInt32(d[1]) == 3)
                        {
                            if (Convert.ToInt32(c[0]) >= 25 || Convert.ToInt32(d[1]) != Convert.ToInt32(c[1]))
                            {
                                //maxdate = 31 / Convert.ToInt32(d[1]) / Convert.ToInt32(d[2]);
                                string data = Convert.ToInt32(d[2]).ToString() + '-' + d[1] + '-' + "31";
                                //DateTime dttest1 = Activitydate.AddDays(maxdate);
                                // txtDate.Text = data;
                                DateTime dttest1 = Convert.ToDateTime(data);
                                txtDate.Text = dttest1.ToString("dd/MM/yyyy");
                                maxdate = 31;
                            }

                            else if (maxdate > DateTime.Now.Day)
                            {
                                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            }
                        }

                        else if (maxdate > DateTime.Now.Day)
                        {
                            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        }

                    }
                    #endregion

                }
                else
                {

                    TxtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    //DateTime dttest = DateTime.Now.AddDays(14);
                    //txtDate.Text = dttest.ToString("dd/MM/yyyy");
                }
             //   TxtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                if (Convert.ToString(Session["Backlk"]) == "1")
                {
                    btnSerach_Click(btnSerach, null);
                    Session["Backlk"] = "";
                }
              
               
            }
        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
    }

 
    protected void btnSave_Click(object sender, EventArgs e)
    {
      //  DGV_Report.Visible = true;
        Response.Redirect("~/FrmReportActivityBlockWise.aspx");
    }
 


    protected void Export_To_Excel(object sender, EventArgs e)
    {
        DataTable dt= ViewState["dtUserVillage"] as DataTable;
       // ExporttoExcel(DGV_Report, dt);
        

    }

    protected void btnSerach_Click(object sender, EventArgs e)
    {
        if (txtDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select To Date')</script>", false);
            return;
        }
        string fromDate = TxtFromDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        string ToDate = txtDate.Text;
        string[] c = ToDate.Split('/');
        string aToDate = c[2] + '-' + c[1] + '-' + c[0];


        DateTime d1 = Convert.ToDateTime(afromDate);
        DateTime d2 = Convert.ToDateTime(aToDate);
        int month = Convert.ToInt32(c[1]) - Convert.ToInt32(d[1]);
        TimeSpan t = d2 - d1;

        double Days = Convert.ToDouble(t.TotalDays);
        if (Math.Sign(Days) == -1)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Vaild Date')</script>", false);
            return;
        }
        if (Math.Round(Days) >= 14)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Max 14 day can be selected')</script>", false);
            return;
        }
        LoadSerarchSchoolActivity();
        LoadSearchVillageActivity();
        LoadSearchOfficeActivtiy();
    }

    public void LoadSerarchSchoolActivity()
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
        DataTable dtMain = null;
        string con = "";
        //if (Session["user_level"].ToString() == "19")
        //{
        //    con = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and mst5village.BlockCode='" + Session["BlockCode"].ToString() + "' ";
        //    dtMain = objMain.LoadSchoolActivtiyCluseter(afromDate, aToDate, ddlBlock.SelectedValue, con);

        //}
        Gv_Profile_Search.DataSource = null;
        Gv_Profile_Search.DataBind();

        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
        {
            con = " ActivityDate between('" + afromDate + "') and '" + aToDate + "'  and ApproveStatus='B'   and DistrictCode='" + Session["NewDistrictCode"].ToString() + "' ";
            //dtMain = objMain.LoadSchoolActivtiyBlockWise(afromDate, aToDate, Session["DistrictCode"].ToString(), con);
           // dtMain = objMain.LoadSchoolActivtiyForAllTypeReport(afromDate, aToDate, Session["NewDistrictCode"].ToString(), con,1);
            dtMain = objMain.LoadActivtiyAllBlockClusterWise(afromDate, aToDate, Session["NewDistrictCode"].ToString(), con, 1);
       
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
        lblSchool.Visible = false;
        int count = 0;
        if (dtMain.Rows.Count > 0)
        {
            #region School
            lblSchool.Visible = true;
            string strGSS = "SIP Annual";
            DataRow[] dr = dtMain.Select("School='" + strGSS + "'");
            if (dr.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 3;
                Item1["School"] = "SIP Annual";
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

            string strGSS4 = "Retention";
            DataRow[] dr4 = dtMain.Select("School='" + strGSS4 + "'");
            if (dr4.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 2;
                Item1["School"] = "Retention";
            }

            string strGSS5 = "GKP";
            DataRow[] dr5 = dtMain.Select("School='" + strGSS5 + "'");
            if (dr5.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 4;
                Item1["School"] = "GKP";
            }
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


            string strGSS1 = "SAC Update";
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

                Item1["School"] = "SAC Update";
            }


            string strGSS123 = "Bal Sabha";
            DataRow[] dr21 = dtMain.Select("School='" + strGSS123 + "'");
            if (dr21.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 7;
                Item1["School"] = "Bal Sabha";
            }
            string strGSS1231 = "Life Skill Game 1";
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
                Item1["School"] = "Life Skill Game 1";
            }

            string strGSS12311 = "Life Skill Game 2";
            DataRow[] dr2111 = dtMain.Select("School='" + strGSS12311 + "'");
            if (dr2111.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 9;
                Item1["School"] = "Life Skill Game 2";
            }
            string Game3 = "Life Skill Game 3";
            DataRow[] drGame3 = dtMain.Select("School='" + Game3 + "'");
            if (drGame3.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 10;
                Item1["School"] = "Life Skill Game 3";
            }
            string Game4 = "Life Skill Game 4";
            DataRow[] drGame4 = dtMain.Select("School='" + Game4 + "'");
            if (Game4.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 11;
                Item1["School"] = "Life Skill Game 4";
            }
            string Game5 = "Life Skill Game 5";
            DataRow[] drGame5 = dtMain.Select("School='" + Game5 + "'");
            if (drGame5.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 12;
                Item1["School"] = "Life Skill Game 5";
            }


            string CLt = "CLT";
            DataRow[] drCLt = dtMain.Select("School='" + CLt + "'");
            if (drCLt.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 13;
                Item1["School"] = "CLT";
            }



            string CLt1 = "Learning Baseline";
            DataRow[] drCLt1 = dtMain.Select("School='" + CLt1 + "'");
            if (drCLt1.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 14;
                Item1["School"] = "Learning Baseline";
            }

            string CLt2 = "Learning  Midline";
            DataRow[] drCLt2 = dtMain.Select("School='" + CLt2 + "'");
            if (drCLt2.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 15;
                Item1["School"] = "Learning  Midline";
            }

            string CLt3 = "Learning  Endline";
            DataRow[] drCLt3 = dtMain.Select("School='" + CLt3 + "'");
            if (drCLt3.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 16;
                Item1["School"] = "Learning  Endline";

            }

            string CLt4 = "Learning  Endline";
            DataRow[] drCLt4 = dtMain.Select("School='" + CLt4 + "'");
            if (drCLt4.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 16;
                Item1["School"] = "Learning  Endline";
            }


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
            //int sum = Convert.ToInt32(dtMain.Compute("SUM(Balsabha)", string.Empty));
            #endregion

            //Int32 col = 0;
            //foreach (DataColumn column in dtMain.Columns)
            //{
            //    if (col == 2)
            //    {
            //        Gv_Profile_Search.Columns[col].Visible = true;
            //        Gv_Profile_Search.Columns[col].HeaderText = column.ColumnName;
            //    }


            //    if (col == 3)
            //    {
            //        Gv_Profile_Search.Columns[col].Visible = true;
            //        Gv_Profile_Search.Columns[col].HeaderText = column.ColumnName;
            //    }
            //    if (col == 4)
            //    {
            //        Gv_Profile_Search.Columns[col].Visible = true;
            //        Gv_Profile_Search.Columns[col].HeaderText = column.ColumnName;
            //    }

            //    if (col == 5)
            //    {
            //        Gv_Profile_Search.Columns[col].Visible = true;
            //        Gv_Profile_Search.Columns[col].HeaderText = column.ColumnName;
            //    }
            //    col = col + 1;
            //}
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

            for (int r =0; r < dt.Rows.Count ; r++)
            {
                decimal total = 0;
                for (int i = 2; i < dt.Columns.Count; i++)
                {
                    Label lbl = ((Label)Gv_Profile_Search.Rows[r].Cells[i].FindControl("lblCol_" + (i+1)));
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

            Session["Gv_Profile_Search"] = dt;


            Gv_Profile_Search.Rows[16].Visible = false;
           
          
          //  DataRow[] drApp = null;
          ////   Gv_Profile_Search.HeaderRow.Cells[0].Text = "School Activity";
            if (Session["NewDistrictCode"].ToString() == "F665A37C5FA74831BBD93F208")
            {
                for (int Index = 2; Index < Gv_Profile_Search.HeaderRow.Cells.Count; Index++)
                {
                    #region ApproveBy
                    var firstCell = Gv_Profile_Search.HeaderRow.Cells[Index];

                    #endregion
                    firstCell.Controls.Clear();
                    firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityClusterSearchNew.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });
                    //  Gv_Profile_Search.HeaderRow.Cells[0].Visible = false;
                }
            }
            else
            {
                for (int Index = 2; Index < Gv_Profile_Search.HeaderRow.Cells.Count-1; Index++)
                {
                    #region ApproveBy
                    var firstCell = Gv_Profile_Search.HeaderRow.Cells[Index];

                    #endregion
                    firstCell.Controls.Clear();
                    firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityClusterSearchNew.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });
                    //  Gv_Profile_Search.HeaderRow.Cells[0].Visible = false;
                }
            }
            //for (int Index = 0; Index < Gv_Profile_Search.Rows.Count; Index++)
            //{
            //    Gv_Profile_Search.Rows[Index].Cells[0].Visible = false;
            //}
            //Gv_Profile_Search.Rows[1]["	OrderCount"].Visible = false;
        //    Gv_Profile_Search.Columns[0].Visible = false;
        }
        else
        {
            Gv_Profile_Search.DataSource = null;
            Gv_Profile_Search.DataBind();
        }
     

  
    }
    protected void Gv_Profile_Search_OnRowCommand(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //DataTable dt = Session["Gv_Profile_Search"] as DataTable;
            //Label lblSchool = (Label)e.Row.FindControl("lblSchool");
            //Label lblBlock1 = (Label)e.Row.FindControl("lblBlock1");
            //Label lblBlock2 = (Label)e.Row.FindControl("lblBlock2");
            //Label lblBlock3 = (Label)e.Row.FindControl("lblBlock3");
            //Label lblBlock4 = (Label)e.Row.FindControl("lblBlock4");
            //Label lblBlock5 = (Label)e.Row.FindControl("lblBlock5");
            //Label lblBlock6 = (Label)e.Row.FindControl("lblBlock6");
            //Label lblBlock7 = (Label)e.Row.FindControl("lblBlock7");
            //Label lblBlock8 = (Label)e.Row.FindControl("lblBlock8");
            //Label lblBlock9 = (Label)e.Row.FindControl("lblBlock9");
            //Label lblBlock10 = (Label)e.Row.FindControl("lblBlock10");
            //Label lblBlock11 = (Label)e.Row.FindControl("lblBlock11");
            //Label lblBlock12 = (Label)e.Row.FindControl("lblBlock12");
            //Label lblBlock13 = (Label)e.Row.FindControl("lblBlock13");
            //Label lblBlock14 = (Label)e.Row.FindControl("lblBlock14");


        }


    }

    public void LoadSearchVillageActivity()
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
        DataTable dtMain =null;

        //if (Session["user_level"].ToString() == "19")
        //{
        //    con = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and mst5village.BlockCode='" + Session["BlockCode"].ToString() + "' ";
        //    dtMain = objMain.LoadVillageActivtiyCluseter(afromDate, aToDate, ddlBlock.SelectedValue, con);

        //}
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
        {
            con = "ActivityDate between('" + afromDate + "') and '" + aToDate + "'  and mst5village.DistrictCode='" + Session["NewDistrictCode"].ToString() + "' ";
           // dtMain = objMain.LoadVillageActivtiyCluseterIO(afromDate, aToDate, ddlBlock.SelectedValue, con);
            //dtMain = objMain.LoadVillageActivtiyBlockWise(afromDate, aToDate, Session["NewDistrictCode"].ToString(), con);
            dtMain = objMain.LoadActivtiyAllBlockClusterWise(afromDate, aToDate, Session["NewDistrictCode"].ToString(), con, 2);
        }

   //     DataTable dtApprove = objMain.LoadVillageActivtiyApprove(condation);

        int count = 0;
        lblVillage.Visible = false;
        if (dtMain.Rows.Count > 0)
        {
            lblVillage.Visible = true;
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

            string strGSS5621 = "Enrollment (6 yrs)";
            DataRow[] dr61 = dtMain.Select("Village='" + strGSS5621 + "'");
            if (dr61.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Enrollment (6 yrs)";
                Item1["SRNo"] = 8;
            }
            string strGSS56211 = "Enrollment (7-14 yrs)";
            DataRow[] dr611 = dtMain.Select("Village='" + strGSS56211 + "'");
            if (dr611.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Enrollment (7-14 yrs)";
                Item1["SRNo"] = 9;
            }

            string strGSS562111 = "Ineligible";
            DataRow[] dr6111 = dtMain.Select("Village='" + strGSS562111 + "'");
            if (dr6111.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Ineligible";
                Item1["SRNo"] = 10;
            }


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

            gvVillageActivity.Rows[12].Visible = false;
            // gvVillageActivity.HeaderRow.Cells[count].Text = "T.B.Hand Holding";
            DataRow[] drApp = null;
            if (Session["NewDistrictCode"].ToString() == "F665A37C5FA74831BBD93F208")
            {
                for (int Index = 1; Index < gvVillageActivity.HeaderRow.Cells.Count; Index++)
                {
                    #region ApproveBy
                    var firstCell = gvVillageActivity.HeaderRow.Cells[Index];

                    #endregion
                    firstCell.Controls.Clear();

                    firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityClusterSearchNew.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

                }
            }
            else
            {
                for (int Index = 1; Index < gvVillageActivity.HeaderRow.Cells.Count - 1; Index++)
                {
                    #region ApproveBy
                    var firstCell = gvVillageActivity.HeaderRow.Cells[Index];

                    #endregion
                    firstCell.Controls.Clear();

                    firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityClusterSearchNew.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

                }
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

        //if (Session["user_level"].ToString() == "19")
        //{
        //    con = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and mst5village.BlockCode='" + Session["BlockCode"].ToString() + "' ";
        //    dtMain = objMain.LoadVillageActivtiyCluseter(afromDate, aToDate, ddlBlock.SelectedValue, con);

        //}
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
        {
            con = "ActivityDate between('" + afromDate + "') and '" + aToDate + "'  and mst5village.DistrictCode='" + Session["NewDistrictCode"].ToString() + "' ";
            // dtMain = objMain.LoadVillageActivtiyCluseterIO(afromDate, aToDate, ddlBlock.SelectedValue, con);
            //dtMain = objMain.LoadSchoolActivtiyOfficeBlockWise(afromDate, aToDate, Session["NewDistrictCode"].ToString(), con);
            dtMain = objMain.LoadActivtiyAllBlockClusterWise(afromDate, aToDate, Session["NewDistrictCode"].ToString(), con, 3);
        }

        //     DataTable dtApprove = objMain.LoadVillageActivtiyApprove(condation);

        int count = 0;
        lblOffice.Visible = false;
        if (dtMain.Rows.Count > 0)
        {
            lblOffice.Visible = true;


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

            gvOffice.Rows[4].Visible = false;
            // gvVillageActivity.HeaderRow.Cells[count].Text = "T.B.Hand Holding";
            DataRow[] drApp = null;
            if (Session["NewDistrictCode"].ToString() == "F665A37C5FA74831BBD93F208")
            {
                for (int Index = 1; Index < gvOffice.HeaderRow.Cells.Count ; Index++)
                {
                    #region ApproveBy
                    var firstCell = gvOffice.HeaderRow.Cells[Index];

                    #endregion
                    firstCell.Controls.Clear();

                    firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityClusterSearchNew.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

                }
            }
            else
            {
                for (int Index = 1; Index < gvOffice.HeaderRow.Cells.Count - 1; Index++)
                {
                    #region ApproveBy
                    var firstCell = gvOffice.HeaderRow.Cells[Index];

                    #endregion
                    firstCell.Controls.Clear();

                    firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityClusterSearchNew.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

                }
            }
        }
        else
        {
            gvOffice.DataSource = null;
            gvOffice.DataBind();
        }

    }
    //protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string strQry = "";
    //    if (ddlBlock.SelectedIndex > 0)
    //    {
    //        strQry = "   select Villagecode  from MstUser   where UserName='" + ddlBlock.SelectedValue + "' ";
    //        DataTable dtUserVillage = objMain.LoadData(strQry);

    //        string strVillage = dtUserVillage.Rows[0]["Villagecode"].ToString();

    //        conditions = "mst5Village.VillageCode in(" + strVillage + ") ";

    //     //   objComman.BindDLL("mst5Village", "VillageCode,VillageName ", conditions, "", "", ddlVilage, "VillageName", "VillageCode", "Select");


    //    }
    //}
  

    protected void TestGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //string quantity = e.Row.Cells[3].Text;
          
            //foreach (TableCell cell in e.Row.Cells)
            //{
               
            //        cell.BackColor = Color.Red;
               
            //}
        }
    }
    //protected void Gv_Profile_Search_OnRowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "GVUIO")
    //    {
    //        int iIndex = Convert.ToInt32(e.CommandArgument);
    //        string VDate = Gv_Profile_Search.DataKeys[iIndex]["VDate"].ToString();
    //        Response.Redirect("./frmMobileVillageProfile.aspx?ID=" + ddlVilage.SelectedValue + "," + ddlBlock.SelectedValue + "," + VDate + "");
    //    }
 
}