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


public partial class FrmReportActivityBlockWise : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    string conditions = "";
    Comman objComman = new Comman();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
                LoadYear();
            
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["Backlk"] = "1";
        base.Response.Redirect("~/FrmActivityBlockWiseSearch.aspx");



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
                    dr = dtYear.NewRow();
                    dr["Type"] = GivenYear - 2 + "-" + Convert.ToString((GivenYear - 2 + 1));
                    dr["ID"] = y - 2;
                    dtYear.Rows.Add(dr);
                }
                else
                {
                    dr = dtYear.NewRow();
                    dr["Type"] = Convert.ToString((y - 1)) + "-" + y.ToString();
                    //y = y - 1;
                    dr["ID"] = y - 1;

                    dtYear.Rows.Add(dr);
                    dr = dtYear.NewRow();
                    dr["Type"] = GivenYear - 2 + "-" + Convert.ToString((GivenYear - 2 + 1));
                    dr["ID"] = y - 2;
                    dtYear.Rows.Add(dr);

                }

            }

        }
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


    }

    protected void btnSerach_Click(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex < 1)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Year')</script>", false);
            return;
        }

        if (ddlMonth.SelectedIndex < 1)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Month')</script>", false);
            return;
        }
        //string fromDate = TxtFromDate.Text;
        //string[] d = fromDate.Split('/');
        //string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        //string ToDate = txtDate.Text;
        //string[] c = ToDate.Split('/');
        //string aToDate = c[2] + '-' + c[1] + '-' + c[0];


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
        //if (Math.Round(Days) >= 14)
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Max 14 day can be selected')</script>", false);
        //    return;
        //}
        LoadSerarchSchoolActivity();
        LoadSearchVillageActivity();
        LoadSearchOfficeActivtiy();
    }


    public void LoadSerarchSchoolActivity()
    {
        Session["dt"] = null;
        // DGV_Report.Visible = false;
        Gv_Profile_Search.Visible = true;
        Int32 Year = 0;
        string afromDate = "";
        string aToDate = "";
        Int32 month = 0;
        if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        {
            Year = Convert.ToInt32(ddlYear.SelectedValue) + 1;
        }
        else
        {
            Year = Convert.ToInt32(ddlYear.SelectedValue);
        }
        if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
        {
            month = 12;
        }
        else
        {
            month = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
        }

        if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        {
            afromDate = Year.ToString() + '-' + ddlMonth.SelectedValue.ToString() + '-' + "01";
            aToDate = Year.ToString() + '-' + ddlMonth.SelectedValue.ToString() + '-' + "31";
        }
        else
        {
            afromDate = Year.ToString() + '-' + month.ToString() + '-' + "25";
            aToDate = Year.ToString() + '-' + ddlMonth.SelectedValue + '-' + "25";
        }

        Session["rfdate"] = afromDate;
        Session["rTodate"] = aToDate;

        DataTable dtMain = null;
        string con = "";
        //if (Session["user_level"].ToString() == "19")
        //{
        //    con = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and mst5village.BlockCode='" + Session["BlockCode"].ToString() + "' ";
        //    dtMain = objMain.LoadSchoolActivtiyCluseter(afromDate, aToDate, ddlBlock.SelectedValue, con);

        //}
        Gv_Profile_Search.DataSource = null;
        Gv_Profile_Search.DataBind();



        con = " ActivityDate between('" + afromDate + "') and '" + aToDate + "'   and UserEntry=3 and mst5village.DistrictCode='" + Session["DistrictCode"].ToString() + "' ";
        dtMain = objMain.LoadActivtiyBlockWiseReport(afromDate, aToDate, Session["DistrictCode"].ToString(), con,1);
        // dtMain = objMain.LoadSchoolActivtiyCluseterIO(afromDate, aToDate, ddlBlock.SelectedValue, con);

        string condation = "";

        lblSchool.Visible = false;
        int count = 0;
        if (dtMain.Rows.Count > 0)
        {
            #region School
            lblSchool.Visible = true;
            //    btnApprove.Visible = true;
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

            string strGSS5 = "SMC Orientation";
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
                Item1["School"] = "SMC Orientation";
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

            Session["Gv_Profile_Search"] = dt;




            //  DataRow[] drApp = null;
            ////   Gv_Profile_Search.HeaderRow.Cells[0].Text = "School Activity";
            //for (int Index = 2; Index < Gv_Profile_Search.HeaderRow.Cells.Count-1; Index++)
            //{
            //    #region ApproveBy
            //    var firstCell = Gv_Profile_Search.HeaderRow.Cells[Index];

            //    #endregion
            //    firstCell.Controls.Clear();
            //    firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityDatewiseSearch.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });
            //    //  Gv_Profile_Search.HeaderRow.Cells[0].Visible = false;
            //}
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

    public void LoadSearchVillageActivity()
    {
        Session["dt"] = null;

        string con = " ";
        DataTable dtMain = null;



        con = "   ActivityDate between('" + Session["rfdate"].ToString() + "') and '" + Session["rTodate"].ToString() + "'   and mst5village.DistrictCode='" + Session["DistrictCode"].ToString()  + "' ";
        dtMain = objMain.LoadActivtiyBlockWiseReport(Session["rfdate"].ToString(), Session["rTodate"].ToString(), Session["DistrictCode"].ToString(), con,2);
       
        int count = 0;
        lblVillage.Visible = false;
        if (dtMain.Rows.Count > 0)
        {
            // btnApprove.Visible = true;
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
            string strGSS11 = "Support";
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
            //for (int Index = 1; Index < gvVillageActivity.HeaderRow.Cells.Count-1; Index++)
            //{
            //    #region ApproveBy
            //    var firstCell = gvVillageActivity.HeaderRow.Cells[Index];

            //    #endregion
            //    firstCell.Controls.Clear();

            //    firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityDatewiseSearch.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

            //}
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


        string con = " ";
        DataTable dtMain = null;



        con = "   ActivityDate between('" + Session["rfdate"].ToString() + "') and '" + Session["rTodate"].ToString() + "'   and mst5village.DistrictCode='" + Session["DistrictCode"].ToString() + "' ";
        dtMain = objMain.LoadActivtiyBlockWiseReport(Session["rfdate"].ToString(), Session["rTodate"].ToString(), Session["DistrictCode"].ToString(), con, 3);
       
        lblOffice.Visible = false;
        if (dtMain.Rows.Count > 0)
        {

            lblOffice.Visible = true;
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
            //DataRow[] drApp = null;
            //for (int Index = 1; Index < gvOffice.HeaderRow.Cells.Count - 1; Index++)
            //{
            //    #region ApproveBy
            //    var firstCell = gvOffice.HeaderRow.Cells[Index];

            //    #endregion
            //    firstCell.Controls.Clear();

            //    firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityDatewiseSearch.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

            //}
        }
        else
        {
            gvOffice.DataSource = null;
            gvOffice.DataBind();
        }
    }
}