using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
public partial class dashboard : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {

                //        BindrptNew();
                fillbirthday();
                BindrptNew1();
                div1.Visible = true;
                lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //MpexdrDistrict.Show();
                //if (Convert.ToString(Session["username"]) == "EGE3078" || Convert.ToString(Session["username"]) == "SuperAdmin")
                //{
                //    //div1.Visible = true;
                //    //lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //    //Bindrpt();
                //}
                //else
                //{
                //  // div1.Visible = false;
                //}
            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {

        GenerateExcel("ChampionSummary");

    }
    #region Birthday report  Anuj
    protected void btnBirthDay_Report_Click(object sender, EventArgs e)
    {

        GenerateBirthdayExcel("BirthdayWishesReport");

    }
    private void GenerateBirthdayExcel(string FIleName)
    {
        try
        {
            DataTable dt = ViewState["BirthAniversary"] as DataTable;
            if (dt.Rows.Count > 0)
            {
                DataColumnCollection columns = dt.Columns;
                if (columns.Contains("TB Name1"))
                {
                    dt.Columns.Remove("TB Name1");
                }



                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                string Fullfilename = "" + FIleName + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");

                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                HttpContext.Current.Response.Write("<table  >");
                HttpContext.Current.Response.Write("<tr>");
                HttpContext.Current.Response.Write("<td colspan='9'  style='text-align:center;border:.2pt solid windowtext;font-weight:700;font-size:17px;Color:#fff;background: #ed3237'>Team Balika Birthday List</ td>");

                HttpContext.Current.Response.Write("</tr>");

                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";


                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	 TB Code	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	 Name	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Gender	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Contact	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Birthday Date	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	State Name	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	District Name	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Block Name	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Village Name	</th>");


                HttpContext.Current.Response.Write("</tr>");

                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;";




                string villagecode = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HttpContext.Current.Response.Write("<tr>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {

                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");
                    }

                    HttpContext.Current.Response.Write("</tr>");


                }



                HttpContext.Current.Response.Write("</table>");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }
        catch
        {
            throw;
        }


    }

    #endregion
    private void GenerateExcel(string FIleName)
    {
        try
        {




            DataTable dt = Session["DataTable"] as DataTable;
            if (dt.Rows.Count > 0)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                string Fullfilename = "" + FIleName + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");

                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                HttpContext.Current.Response.Write("<table  >");
                HttpContext.Current.Response.Write("<tr>");
                HttpContext.Current.Response.Write("<td colspan='7'  style='text-align:center;border:.2pt solid windowtext;'>Champions of the Girls Education</td>");

                HttpContext.Current.Response.Write("</tr>");
                HttpContext.Current.Response.Write("<tr>");



                HttpContext.Current.Response.Write("<td colspan='7'  style='text-align:left;border:.2pt solid windowtext;'>Date- " + (DateTime.Now).AddDays(-1).ToString("yyyy-MM-dd") + "  </td>");

                HttpContext.Current.Response.Write("</tr>");


                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";


                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");

                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	State Name	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	District Name	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Cluster Name	</th>");

                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	FC Code	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	FC Name	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	# Quality Enrolment	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Rank	</th>");

                HttpContext.Current.Response.Write("</tr>");

                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;";




                string villagecode = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    HttpContext.Current.Response.Write("<tr>");
                    //HttpContext.Current.Response.Write("<td >Direct</td>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {

                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");
                    }

                    HttpContext.Current.Response.Write("</tr>");


                }



                HttpContext.Current.Response.Write("</table>");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }
        catch
        {
            throw;
        }


    }

    private void GenerateExcelNew(string FIleName)
    {
        try
        {



            DataTable dt = Session["DataTable"] as DataTable;
            if (dt.Rows.Count > 0)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                string Fullfilename = "" + FIleName + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");

                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                HttpContext.Current.Response.Write("<table  >");
                HttpContext.Current.Response.Write("<tr>");


                HttpContext.Current.Response.Write("<td colspan='2'  style='text-align:left;border:.2pt solid windowtext;'>PMS Data Date- " + DateTime.Now.ToString("yyyy-MM-dd") + "  </td>");
                HttpContext.Current.Response.Write("<td colspan='2' ' style='text-align:left;border:.2pt solid windowtext;'>Data Source: PMS </td>");
                HttpContext.Current.Response.Write("<td colspan='2' ' style='text-align:left;border:.2pt solid windowtext;background:#FFFF00;'>UnderAchievemnt</td>");
                HttpContext.Current.Response.Write("<td colspan='2' ' style='text-align:text-color: red:left;border:.2pt solid windowtext;background:#FF0000;'>OverAchievement</td>");

                HttpContext.Current.Response.Write("<td colspan='14' ' style='text-align:left;border:.2pt solid windowtext;background:#008000;'>Within +10% and -10% Range</td>");
                HttpContext.Current.Response.Write("</tr>");

                HttpContext.Current.Response.Write("<tr>");
                HttpContext.Current.Response.Write("<td colspan='16' ' style='text-align:left;border:.2pt solid windowtext;'>Pplan - Participants Planned, Pach - Participants Achieved, Mplan - Mandays Planned, Mach - Mandays Achieved, MeetPlan - Meeting Planned, MeetAch - Meeting Achieved</td>");
                HttpContext.Current.Response.Write("</tr>");

                HttpContext.Current.Response.Write("<tr>");
                HttpContext.Current.Response.Write("<td colspan='16' ' style='text-align:left;border:.2pt solid windowtext;'></td>");

                HttpContext.Current.Response.Write("</tr>");
                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2'  rowspan='2' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>6 Yrs OOSG</th>");
                HttpContext.Current.Response.Write("<th class='header'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>7-14 Yrs OOSG</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='6' style='" + HeaderStyle + "  width:2%;'> GSS</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='6' style='" + HeaderStyle + "  width:2%;'> MM</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='16' style='" + HeaderStyle + "  width:2%;'> SMC Meet Cum Orientation</th>");

                HttpContext.Current.Response.Write("</tr>");

                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> CM</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> YTD</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> CM</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> YTD</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='8' style='" + HeaderStyle + "  width:2%;'> CM</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='8' style='" + HeaderStyle + "  width:2%;'> YTD</th>");
                HttpContext.Current.Response.Write("</tr>");

                HttpContext.Current.Response.Write("</tr>");

                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");

                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Region	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Districts	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Tgt	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Tgt	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Plan	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Ach	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	% Ach	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Plan	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Ach	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	% Ach	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Plan	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Ach	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	%Ach	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Plan	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Ach	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	%Ach	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	MeetPlan	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	MeetAch	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	%MeetAch	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	PPlan	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Pach - All	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Pach - Female	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	%Ach - ALL	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	%Ach - Female	</th>");
                //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	MPlan	</th>");
                //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	MAch	</th>");
                //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	%MAch	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	MeetPlan	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	MeetAch	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	%MeetAch	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	PPlan	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Pach - All	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Pach - Female	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	%Ach - All	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	%Ach - Female	</th>");
                //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	MPlan	</th>");
                //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	MAch	</th>");
                //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	%MAch	</th>");

                HttpContext.Current.Response.Write("</tr>");

                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";
                String ToallRowStyle = "border:.2pt solid windowtext; font-weight:100; font-size:11pt;rowspan=2;border:.2pt solid windowtext;";

                String RowStyeYellow = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#FFFF00;";
                String RowStyeRed = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#FF0000;";
                String RowStyeGreen = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#008000;";




                DataSet ds = Session["dsDataTable"] as DataSet;

                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    HttpContext.Current.Response.Write("<tr>");
                    //HttpContext.Current.Response.Write("<td >Direct</td>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {

                        if (dt.Columns[c].ToString() == "GSSmainAch" || dt.Columns[c].ToString() == "yGSSmainAch" || dt.Columns[c].ToString() == "MMmainAch" || dt.Columns[c].ToString() == "yMmainAch" || dt.Columns[c].ToString() == "MeetMainplanAch" || dt.Columns[c].ToString() == "MainMeetAllplanAch" || dt.Columns[c].ToString() == "MainMAch" || dt.Columns[c].ToString() == "MainMeetAllFemalplanAch" || dt.Columns[c].ToString() == "MainyMeetPlanAll" || dt.Columns[c].ToString() == "yMainMeetAllplanAch" || dt.Columns[c].ToString() == "yMainMeetAllFemalplanAch")
                        {
                            if (Convert.ToString(dt.Rows[i][c]) == "")
                            {
                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'></td>");
                            }
                            else
                            {
                                if (Convert.ToDecimal(dt.Rows[i][c]) < 90)
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyeYellow + "'>" + dt.Rows[i][c] + "</td>");
                                }
                                if (Convert.ToDecimal(dt.Rows[i][c]) >= 90 && Convert.ToDecimal(dt.Rows[i][c]) <= 110)
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyeGreen + "'>" + dt.Rows[i][c] + "</td>");
                                }
                                if (Convert.ToDecimal(dt.Rows[i][c]) > 110)
                                {
                                    HttpContext.Current.Response.Write("<td style='" + RowStyeRed + "'>" + dt.Rows[i][c] + "</td>");
                                }
                            }
                        }
                        else
                        {

                            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");
                        }


                    }
                    #region Row1

                    //for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    //{
                    //    HttpContext.Current.Response.Write("<tr>");
                    //    for (int c = 0; c < dt.Columns.Count; c++)
                    //    {
                    //        if (c == 0)
                    //        {
                    //            HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'>Total</td>");
                    //        }
                    //        else if (c == 1)
                    //        {
                    //            HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
                    //        }
                    //        else
                    //        {
                    //            HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'>" + ds.Tables[0].Rows[j][c] + "</td>");
                    //        }
                    //    }
                    //    HttpContext.Current.Response.Write("</tr>");

                    //}

                    #endregion


                    HttpContext.Current.Response.Write("</tr>");


                }
                HttpContext.Current.Response.Write("<tr>");
                HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
                HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
                HttpContext.Current.Response.Write("</tr>");

                for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                {
                    HttpContext.Current.Response.Write("<tr>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (c == 0)
                        {
                            HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'>Total</td>");
                        }
                        else if (c == 1)
                        {
                            HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
                        }
                        else
                        {
                            HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'>" + ds.Tables[1].Rows[j][c] + "</td>");
                        }
                    }
                    HttpContext.Current.Response.Write("</tr>");

                }



                HttpContext.Current.Response.Write("</table>");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }
        catch
        {
            throw;
        }


    }
    private void BindrptNew1()
    {
        DataTable dt;
        SqlParameter[] parm2 = new SqlParameter[]
                {
                          new SqlParameter("@Con",  ""),


                     };

        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptGoogleLink]", parm2);
        //GridView1.DataSource = dt;  Anuj
        //GridView1.DataBind();
        ViewState["Dtreport"] = dt;
        lnkenrollment.HRef = dt.Rows[0]["WebLink"].ToString();
        lnkPrimary.HRef = dt.Rows[1]["WebLink"].ToString();
        lnkCBL.HRef = dt.Rows[2]["WebLink"].ToString();
        lnkquality.HRef = dt.Rows[3]["WebLink"].ToString();
        lnkBalance.HRef = dt.Rows[4]["WebLink"].ToString();
        lnlCIOOSHG.HRef = dt.Rows[5]["WebLink"].ToString();
        lnkTrainingDshboard.HRef = dt.Rows[6]["WebLink"].ToString();

        A1.HRef = dt.Rows[7]["WebLink"].ToString();
        A2.HRef = dt.Rows[8]["WebLink"].ToString();

        A3.HRef = dt.Rows[9]["WebLink"].ToString();
        A4.HRef = dt.Rows[10]["WebLink"].ToString();
        A5.HRef = dt.Rows[11]["WebLink"].ToString();
        A6.HRef = dt.Rows[12]["WebLink"].ToString();
        A7.HRef = dt.Rows[13]["WebLink"].ToString();
    }
    protected void onclick_enrolment(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        dt = ViewState["Dtreport"] as DataTable;
        if (dt.Rows[0]["Name"].ToString() == "Enrolment")
        {

            Response.Redirect("" + dt.Rows[0]["WebLink"].ToString() + "");
        }
    }
    protected void onclick_lnkPrimary(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        dt = ViewState["Dtreport"] as DataTable;
        if (dt.Rows[1]["Name"].ToString() == "Primary D2D")
        {
            Response.Redirect("" + dt.Rows[0]["WebLink"].ToString() + "");
        }
    }
    protected void onclick_lnkCBL(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        dt = ViewState["Dtreport"] as DataTable;
        if (dt.Rows[2]["Name"].ToString() == "CBL")
        {
            Response.Redirect("" + dt.Rows[0]["WebLink"].ToString() + "");
        }
    }
    protected void onclick_lnkquality(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        dt = ViewState["Dtreport"] as DataTable;
        if (dt.Rows[3]["Name"].ToString() == "Quality impact dashboard")
        {
            Response.Redirect("" + dt.Rows[0]["WebLink"].ToString() + "");
        }
    }
    protected void onclick_lnkBalance(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        dt = ViewState["Dtreport"] as DataTable;
        if (dt.Rows[4]["Name"].ToString() == "Balance Scorecard")
        {
            Response.Redirect("" + dt.Rows[0]["WebLink"].ToString() + "");
        }
    }
    protected void onclick_lnlCIOOSHG(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        dt = ViewState["Dtreport"] as DataTable;
        if (dt.Rows[5]["Name"].ToString() == "CIOOSG Survey")
        {
            Response.Redirect("" + dt.Rows[0]["WebLink"].ToString() + "");
        }
    }
    protected void onclick_lnkTrainingDshboard(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        dt = ViewState["Dtreport"] as DataTable;
        if (dt.Rows[6]["Name"].ToString() == "Training Dashboard")
        {
            Response.Redirect("" + dt.Rows[0]["WebLink"].ToString() + "");
        }
    }



    private void BindrptNew()
    {
        string con = " ";
        DataSet ds;
        DataTable dt;
        try
        {




            SqlParameter[] parm2 = new SqlParameter[]
            {
                          new SqlParameter("@Con",  con),


                 };

            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptDownloadReportNew]", parm2);

            int i1 = 0;
            int i2 = 0;

            int i3 = 0;

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        i1 = Convert.ToInt32(dt.Rows[i]["Icount"]);
                    }
                    if (i == 1)
                    {
                        i2 = Convert.ToInt32(dt.Rows[i]["Icount"]);
                    }
                    if (i == 2)
                    {
                        i3 = Convert.ToInt32(dt.Rows[i]["Icount"]);
                    }
                }

                DataRow[] dr2;
                dr2 = dt.Select("Icount=" + i1 + "");
                if (dr2.Length > 0)
                {
                    for (int i = 0; i < dr2.Length; i++)
                    {
                        dr2[i]["CHam"] = "	EG Champion of the day";
                    }


                }
                DataRow[] dr1;
                dr1 = dt.Select("Icount=" + i2 + "");
                if (dr1.Length > 0)
                {
                    for (int i = 0; i < dr1.Length; i++)
                    {
                        dr1[i]["CHam"] = "	EG Champion of the day";
                    }


                }

                DataRow[] dr;
                dr = dt.Select("Icount=" + i3 + "");
                if (dr.Length > 0)
                {
                    for (int i = 0; i < dr.Length; i++)
                    {
                        dr[i]["CHam"] = "	EG Champion of the day";
                    }


                }

                Session["DataTable"] = dt;
                //gvReport.DataSource = dt; Anuj
                //gvReport.DataBind();
            }
            else
            {

                Session["DataTable"] = null;
                //gvReport.DataSource = null; Anuj
                //gvReport.DataBind();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    private void Bindrpt()
    {
        string con = " DistrictCode in (" + Session["DistrictCode"] + ")";
        DataSet ds;
        DataTable dt;
        try
        {


            if (Session["user_level_Role"].ToString() == "1")
            {
                MainDiv.Style.Add("height", "700px");

                SqlParameter[] parm = new SqlParameter[]
                {
                   new SqlParameter("@Con",  "E421AA06278E498DB71B2008D"),
                new SqlParameter("@Flag",  1),

                     };
                dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnuualSacReportNewDoc]", parm);



                SqlParameter[] parm2 = new SqlParameter[]
                {
                   new SqlParameter("@Con",  "E421AA06278E498DB71B2008D"),
                new SqlParameter("@Flag", 2),

                     };

                ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnuualSacReportNewDoc]", parm2);
            }
            else if (Session["user_level_Role"].ToString() == "2")
            {
                MainDiv.Style.Add("height", "700px");
                string strQry1 = "  SELECT distinct mst2District.DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist   inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode  where    UserName='" + Session["username"].ToString() + "' and Fyear='" + Session["FinYear"] + "' order by DistrictName   ";
                DataTable dtDistrict = objMain.LoadData(strQry1);
                string ddlStatecode = "";
                for (int j = 0; j < dtDistrict.Rows.Count; j++)
                {


                    ddlStatecode += "'" + dtDistrict.Rows[j]["DistrictCode"] + "'" + ",";



                }
                if (ddlStatecode.Length > 0)
                {
                    ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
                    con = " DistrictCode in (" + ddlStatecode + ")";
                }
                else
                {
                    return;
                }




                SqlParameter[] parm = new SqlParameter[]
                {
           new SqlParameter("@Con", con),
        new SqlParameter("@Flag",  3),

                     };
                dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnuualSacReportNewDoc]", parm);



                SqlParameter[] parm2 = new SqlParameter[]
                {
           new SqlParameter("@Con",  con),
        new SqlParameter("@Flag", 5),

                     };

                ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnuualSacReportNewDoc]", parm2);
            }

            else
            {

                MainDiv.Style.Add("height", "400px");
                SqlParameter[] parm = new SqlParameter[]
                {
                       new SqlParameter("@Con", con),
                    new SqlParameter("@Flag",  3),

                     };
                dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnuualSacReportNewDoc]", parm);



                SqlParameter[] parm2 = new SqlParameter[]
                {
                          new SqlParameter("@Con",  con),
                          new SqlParameter("@Flag", 4),

                     };

                ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnuualSacReportNewDoc]", parm2);
            }


            Session["dsDataTable"] = ds;
            Session["DataTable"] = dt;
            //gvReport.DataSource = dt;
            //gvReport.DataBind(); Anuj24072023
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void gvReport_RowCreated(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.Header)
        {


            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow.CssClass = "gridnewheadercss";
            TableCell HeaderCell;

            HeaderCell = new TableCell();

            HeaderCell.Text = "Champions of the Girls Education";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;


            HeaderCell.ColumnSpan = 7;

            //  HeaderCell.ColumnSpan = 5;
            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);



            //gvReport.Controls[0].Controls.AddAt(0, HeaderGridRow); Anuj 24072023


            GridView HeaderGrid1 = (GridView)sender;
            GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow1.CssClass = "gridnewheadercss";
            TableCell HeaderCell1;

            HeaderCell1 = new TableCell();
            string str = (DateTime.Now).AddDays(-1).ToString("dd/MM/yyyy");
            HeaderCell1.Text = "Date :" + str;
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderCell1.ColumnSpan = 7;

            //  HeaderCell1.ColumnSpan = 5;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);

            //gvReport.Controls[0].Controls.AddAt(1, HeaderGridRow1); Anuj24072023

        }
    }


    #region Anuj birthday
    protected void birthAnni_Click(object sender, EventArgs e)
    {
        DataTable BirtAnni = ViewState["BirthAniversary"] as DataTable;
        if (BirtAnni.Rows.Count > 0 && BirtAnni != null)
        {
            GV_birhday.DataSource = BirtAnni;
            GV_birhday.DataBind();
            ModalPopupExtender1.Show();
        }

    }
    protected void birthAnni_Click1(object sender, EventArgs e)
    {

        fillbirthday();
        //fillAnniversary();
        ModalPopupExtender1.Show();
        //MpexdrDistrict.Show();
    }
    public void fillbirthday()
    {
        DataTable dt = new DataTable();
        string conditions = "";
        if (Session["user_level_Role"].ToString() == "3")
        {
            conditions += " and v.DistrictCode='" + Session["NewDistrictCode"].ToString() + "'";

        }

        else if (Session["user_level_Role"].ToString() == "4")
        {
            conditions += " and v.BlockCOde='" + Session["NewBlockCode"].ToString() + "'";

        }
        else if (Session["user_level_Role"].ToString() == "2")

        {

            string conditions1 = "UserName='" + Session["username"].ToString() + "' ";
            string strQry1 = "  SELECT distinct mst2District.DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode  where   " + conditions1 + " and Fyear='2023-2024'  order by DistrictName   ";

            DataTable dtDistrict = objMain.LoadData(strQry1);

            string ddlDistrictCode = "";

            foreach (DataRow row in dtDistrict.Rows)
            {

                ddlDistrictCode += "'" + row["DistrictCode"].ToString() + "'" + ",";

            }
            if (ddlDistrictCode.Length > 0)
            {
                ddlDistrictCode = ddlDistrictCode.Substring(0, ddlDistrictCode.LastIndexOf(","));
                conditions += " and v.DistrictCode in(" + ddlDistrictCode + ")";
            }


        }

        else
        {

        }
        //string year = Convert.ToString(System.DateTime.Now.Year);
        //string year1 = year + '-' + (Convert.ToInt32 (year) + 1);
        SqlParameter[] parm = new SqlParameter[]
            {

                       new SqlParameter("@Year", conditions)


                 };
        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Sp_BirthdayDetails]", parm);


        //dt = objMain.Select_All_Data("tblemployeedetails", "Firstname,Convert(nvarchar(10),DateofBirth,105) as Date ,EmployeeID", " Status=1 and day(DateofBirth) = day(getdate())-4 and month(DateofBirth) = month(getdate())", "Firstname", "ASC");
        for (int i = 0; i < dt.Rows.Count; i++)
        {

            if (i == 0)
            {
                Id_0.InnerText = dt.Rows[i]["TB Name1"].ToString();
            }
            else if (i == 1)
            {
                Id_1.InnerText = dt.Rows[i]["TB Name1"].ToString();
            }
            else if (i == 2)
            {
                Id_2.InnerText = dt.Rows[i]["TB Name1"].ToString();
            }
            else if (i == 3)
            {
                Id_3.InnerText = dt.Rows[i]["TB Name1"].ToString();
            }
            else if (i == 4)
            {
                Id_4.InnerText = dt.Rows[i]["TB Name1"].ToString();
            }
            else if (i == 5)
            {
                Id_5.InnerText = dt.Rows[i]["TB Name1"].ToString();
            }

            else if (i == 6)
            {
                Id_6.InnerText = dt.Rows[i]["TB Name1"].ToString();
            }
            else if (i == 7)
            {
                Id_7.InnerText = dt.Rows[i]["TB Name1"].ToString();
            }
            else if (i == 8)
            {
                Id_8.InnerText = dt.Rows[i]["TB Name1"].ToString();
            }
            else if (i == 9)
            {
                Id_9.InnerText = dt.Rows[i]["TB Name1"].ToString();
            }

            else if (i == 10)
            {
                Id_10.InnerText = dt.Rows[i]["TB Name1"].ToString();
            }

            else if (i == 11)
            {
                Id_11.InnerText = dt.Rows[i]["TB Name1"].ToString();
            }
            else if (i == 12)
            {
                Id_12.InnerText = dt.Rows[i]["TB Name1"].ToString();
            }
            else if (i == 13)
            {
                Id_13.InnerText = dt.Rows[i]["TB Name1"].ToString();
            }

            else if (i == 14)
            {
                Id_14.InnerText = dt.Rows[i]["TB Name1"].ToString();
            }
            else if (i == 15)
            {
                Id_15.InnerText = dt.Rows[i]["TB Name1"].ToString();
            }

            else if (i == 16)
            {
                Id_16.InnerText = dt.Rows[i]["TB Name1"].ToString();
            }
            else if (i == 17)
            {
                Id_17.InnerText = dt.Rows[i]["TB Name1"].ToString();
            }
            else if (i == 18)
            {
                Id_18.InnerText = dt.Rows[i]["TB Name1"].ToString();
            }
            else if (i == 19)
            {
                Id_19.InnerText = dt.Rows[i]["TB Name1"].ToString();
            }

        }
        if (dt.Rows.Count > 20)
        {
            Hdncount.Value = "20";
        }
        else
        {
            Hdncount.Value = Convert.ToString(dt.Rows.Count);
        }
        if (dt.Rows.Count > 0)
        {
            ImgBirthday.Visible = true;
            ViewState["BirthAniversary"] = dt;
            GV_birhday.DataSource = dt;
            GV_birhday.DataBind();
        }


        //ModalPopupExtender1.Show();

        //if (dt.Rows.Count > 0)
        //{
        //    spbirth.Visible = true;
        //    spbirth.InnerHtml = Convert.ToString(dt.Rows.Count);
        //    Huuu.Style.Add("color", "OrangeRed");
        //}
        //else
        //{
        //    spbirth.Visible = false;
        //    Huuu.Style.Add("color", "#333");
        //}

    }
    #endregion 
}