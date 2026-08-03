//using DocumentFormat.OpenXml.Spreadsheet;
using ClosedXML.Excel;
using Ionic.Zip;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class frmMobileTargetReport : System.Web.UI.Page
{

    clsMain objMain = new clsMain();
    string H1 = "", H2 = "", H3 = "", H4 = "", H5 = "";
    Comman objComman = new Comman();
    DataTableMaskingHelper dataTableMaskingHelper = new DataTableMaskingHelper();

    string conditions = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                ViewState["Button"] = "AA";
                LoadYear();
                LoadUserLeavel();
                divDate.Visible = true;
                divDate1.Visible = true;
                divMonth.Visible = false;
                divToMonth.Visible = false;
                Session["ABC"] = "";
                LinkButton43.Visible = true;
                //if (Convert.ToString(Session["username"]) == "PMSAdmin" || Convert.ToString(Session["username"]) == "EGE0606"  || Convert.ToString(Session["username"]) == "EGE3031"  || Convert.ToString(Session["username"]) == "EGE7557" || Convert.ToString(Session["username"]) == "SuperAdmin")
                //{
                //    LinkButton43.Visible = true;
                //}
                //else
                //{
                //    LinkButton43.Visible = false;
                //}
                return;
            }
            else
            {
                base.Response.Redirect("Login.aspx", false);
            }
        }
    }

    private void GenerateExcelOutReachNew(string FIleName)
    {
        try
        {



            DataTable dt = Session["ClusteTrargetCNew"] as DataTable;
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
                HttpContext.Current.Response.Write("<td colspan='25' ' style='text-align:left;border:.2pt solid windowtext;'></td>");

                HttpContext.Current.Response.Write("</tr>");
                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  width:2%;'></th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='18' style='" + HeaderStyle + "  width:2%;'> Status of Contact</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='24' style='" + HeaderStyle + "  width:2%;'>Enrolled</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='24' style='" + HeaderStyle + "  width:2%;'> FollowUp	</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='36' style='" + HeaderStyle + "  width:2%;'>  Ineligible</th>");
                HttpContext.Current.Response.Write("</tr>");


                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  width:2%;'></th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='9'  style='" + HeaderStyle + "  width:2%;'>Female</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='9' style='" + HeaderStyle + "  width:2%;'> Male</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='12'  style='" + HeaderStyle + "  width:2%;'>Female</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'> Male</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>Female</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'> Male	</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='15' style='" + HeaderStyle + "  width:2%;'>Female</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='15' style='" + HeaderStyle + "  width:2%;'> Male	</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>  Female</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>  Male</th>");


                //HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>Female</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'> Male	</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>  Female</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>  Male</th>");

                HttpContext.Current.Response.Write("</tr>");


                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Enrolled With SR</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Follow Up</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Ineligible</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Enrolled With SR</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Follow Up</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Ineligible</th>");

                //HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Enrolled With SR</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Follow Up</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Ineligible</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> NRSTC</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>KGBV</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Aanganwadi</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Mainstream</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> NRSTC</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>KGBV</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Aanganwadi</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Mainstream</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Enrolled Info by Parents</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Ready to be Enrolled</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Not Ready-School Distance</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Not Ready-Other Reason</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Enrolled Info by Parents</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Ready to be Enrolled</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Not Ready-School Distance</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Not Ready-Other Reason</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Migration</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Overage</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Underage</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Typing Error</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Death</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Migration</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Overage</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Underage</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Typing Error</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Death</th>");


                HttpContext.Current.Response.Write("</tr>");


                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'>Block</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  so-rotate: 90; width:2%;'>Cluster Name</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");


                HttpContext.Current.Response.Write("</tr>");

                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";
                String ToallRowStyle = "border:.2pt solid windowtext; font-weight:100; font-size:11pt;rowspan=2;border:.2pt solid windowtext;";

                String RowStyeYellow = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#FFFF00;";
                String RowStyeRed = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#FF0000;";
                String RowStyeGreen = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#008000;";





                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    HttpContext.Current.Response.Write("<tr>");
                    //HttpContext.Current.Response.Write("<td >Direct</td>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {


                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");


                    }
                    #region Row1



                    #endregion


                    HttpContext.Current.Response.Write("</tr>");


                }
                HttpContext.Current.Response.Write("<tr>");
                for (int J = 0; J < 1; J++)
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (c == 1 || c == 0)
                        {
                            if (c == 1)
                            {
                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                            }
                            else
                            {
                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                            }

                        }
                        else
                        {
                            string Col = dt.Columns[c].ColumnName;
                            int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                        }
                    }
                }
                HttpContext.Current.Response.Write("</tr>");


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
    private void GenerateExcelOutReach(string FIleName)
    {
        try
        {



            DataTable dt = Session["ClusteTrargetC"] as DataTable;
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
                //HttpContext.Current.Response.Write("<tr>");
                //HttpContext.Current.Response.Write("<td colspan='25' ' style='text-align:left;border:.2pt solid windowtext;'></td>");

                //HttpContext.Current.Response.Write("</tr>");
                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  width:2%;'></th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='18' style='" + HeaderStyle + "  width:2%;'> Status of Contact</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='24' style='" + HeaderStyle + "  width:2%;'>Enrolled</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='24' style='" + HeaderStyle + "  width:2%;'> FollowUp	</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='30' style='" + HeaderStyle + "  width:2%;'>  Ineligible</th>");
                HttpContext.Current.Response.Write("</tr>");


                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  width:2%;'></th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='9'  style='" + HeaderStyle + "  width:2%;'>Female</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='9' style='" + HeaderStyle + "  width:2%;'> Male</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='12'  style='" + HeaderStyle + "  width:2%;'>Female</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'> Male</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>Female</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'> Male	</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='15' style='" + HeaderStyle + "  width:2%;'>Female</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='15' style='" + HeaderStyle + "  width:2%;'> Male	</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>  Female</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>  Male</th>");


                //HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>Female</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'> Male	</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>  Female</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>  Male</th>");

                HttpContext.Current.Response.Write("</tr>");


                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  width:2%;'></th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Enrolled With SR</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Follow Up</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Ineligible</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Enrolled With SR</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Follow Up</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Ineligible</th>");

                //HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Enrolled With SR</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Follow Up</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Ineligible</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> NRSTC</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>KGBV</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Aanganwadi</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Mainstream</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> NRSTC</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>KGBV</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Aanganwadi</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Mainstream</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Enrolled Info by Parents</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Ready to be Enrolled</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Not Ready-School Distance</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Not Ready-Other Reason</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Enrolled Info by Parents</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Ready to be Enrolled</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Not Ready-School Distance</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Not Ready-Other Reason</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Migration</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Overage</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Underage</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Typing Error</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Death</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Migration</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Overage</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Underage</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Typing Error</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Death</th>");


                HttpContext.Current.Response.Write("</tr>");


                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'>Block</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");


                HttpContext.Current.Response.Write("</tr>");

                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";
                String ToallRowStyle = "border:.2pt solid windowtext; font-weight:100; font-size:11pt;rowspan=2;border:.2pt solid windowtext;";

                String RowStyeYellow = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#FFFF00;";
                String RowStyeRed = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#FF0000;";
                String RowStyeGreen = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#008000;";





                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    HttpContext.Current.Response.Write("<tr>");
                    //HttpContext.Current.Response.Write("<td >Direct</td>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {


                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");


                    }
                    #region Row1



                    #endregion


                    HttpContext.Current.Response.Write("</tr>");


                }
                HttpContext.Current.Response.Write("<tr>");
                for (int J = 0; J < 1; J++)
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (c == 0)
                        {

                            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");


                        }
                        else
                        {
                            string Col = dt.Columns[c].ColumnName;
                            int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                        }
                    }
                }
                HttpContext.Current.Response.Write("</tr>");

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



            DataTable dt = Session["DtTrarget"] as DataTable;
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
                HttpContext.Current.Response.Write("<td colspan='25' ' style='text-align:left;border:.2pt solid windowtext;'></td>");

                HttpContext.Current.Response.Write("</tr>");
                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  width:2%;'>Block</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='8' style='" + HeaderStyle + "  width:2%;'> Target vs Contact Status of OOSG</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='8' style='" + HeaderStyle + "  width:2%;'> Target vs Contact Status of OOSB</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'> Remaning OOSG</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'> Remaning OOSB</th>");
                HttpContext.Current.Response.Write("</tr>");

                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> 	10 to 14</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> 	10 to 14</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");


                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");


                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");






                HttpContext.Current.Response.Write("</tr>");



                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");

                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	BlockName	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");


                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 	10 to 14</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 	10 to 14</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");


                HttpContext.Current.Response.Write("</tr>");

                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";
                String ToallRowStyle = "border:.2pt solid windowtext; font-weight:100; font-size:11pt;rowspan=2;border:.2pt solid windowtext;";

                String RowStyeYellow = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#FFFF00;";
                String RowStyeRed = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#FF0000;";
                String RowStyeGreen = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#008000;";





                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    HttpContext.Current.Response.Write("<tr>");
                    //HttpContext.Current.Response.Write("<td >Direct</td>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {


                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");


                    }
                }
                #region Row1



                #endregion


                HttpContext.Current.Response.Write("</tr>");

                HttpContext.Current.Response.Write("<tr>");
                for (int J = 0; J < 1; J++)
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (c == 0)
                        {
                            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                        }
                        else
                        {
                            string Col = dt.Columns[c].ColumnName;
                            int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                        }
                    }
                }
                HttpContext.Current.Response.Write("</tr>");

                //HttpContext.Current.Response.Write("<tr>");
                //HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
                //HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
                //HttpContext.Current.Response.Write("</tr>");


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


    private void GenerateExcelNewCluster(string FIleName)
    {
        try
        {



            DataTable dt = Session["DtTrargetC"] as DataTable;
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
                HttpContext.Current.Response.Write("<td colspan='25' ' style='text-align:left;border:.2pt solid windowtext;'></td>");

                HttpContext.Current.Response.Write("</tr>");
                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>Block</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='8' style='" + HeaderStyle + "  width:2%;'> Target vs Contact Status of OOSG</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='8' style='" + HeaderStyle + "  width:2%;'> Target vs Contact Status of OOSB</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'> Remaning OOSG</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'> Remaning OOSB</th>");
                HttpContext.Current.Response.Write("</tr>");

                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> 	10 to 14</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> 	10 to 14</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");


                //HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 5 to 6 Yrs</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 7 to 9</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 	10 to 14</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");

                //HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 5 to 6 Yrs</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 7 to 9</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 	10 to 14</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");
                HttpContext.Current.Response.Write("</tr>");



                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");

                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	BlockName	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	ClusterName	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");


                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 	10 to 14</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 	10 to 14</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");
                //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");
                //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");
                //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");
                //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");
                //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");


                //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");
                //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");
                //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");

                HttpContext.Current.Response.Write("</tr>");

                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";
                String ToallRowStyle = "border:.2pt solid windowtext; font-weight:100; font-size:11pt;rowspan=2;border:.2pt solid windowtext;";

                String RowStyeYellow = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#FFFF00;";
                String RowStyeRed = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#FF0000;";
                String RowStyeGreen = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#008000;";





                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    HttpContext.Current.Response.Write("<tr>");
                    //HttpContext.Current.Response.Write("<td >Direct</td>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {


                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");


                    }
                    #region Row1



                    #endregion


                    HttpContext.Current.Response.Write("</tr>");


                }

                HttpContext.Current.Response.Write("<tr>");
                for (int J = 0; J < 1; J++)
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (c == 1 || c == 0)
                        {
                            if (c == 1)
                            {
                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                            }
                            else
                            {
                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                            }
                        }
                        else
                        {
                            string Col = dt.Columns[c].ColumnName;
                            int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                        }
                    }
                }
                HttpContext.Current.Response.Write("</tr>");

                HttpContext.Current.Response.Write("<tr>");
                HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
                HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
                HttpContext.Current.Response.Write("</tr>");


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

    protected void gvReportCluster_RowCreated(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.Header)
        {


            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow.CssClass = "gridnewheadercss";
            TableCell HeaderCell;

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;


            HeaderCell.ColumnSpan = 4;

            //  HeaderCell.ColumnSpan = 5;

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 1;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);




            HeaderCell = new TableCell();
            HeaderCell.Text = "Status of Contact";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 18;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);



            HeaderCell = new TableCell();
            HeaderCell.Text = "Enrolled";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 24;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);



            HeaderCell = new TableCell();
            HeaderCell.Text = "FollowUp";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 24;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "Ineligible";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 30;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);
            gvReportCluster.Controls[0].Controls.AddAt(0, HeaderGridRow);





            GridView HeaderGrid11 = (GridView)sender;
            GridViewRow HeaderGridRow11 = new GridViewRow(1, 1, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow11.CssClass = "gridnewheadercss";
            TableCell HeaderCell11;

            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;


            HeaderCell11.ColumnSpan = 4;

            //  HeaderCell11.ColumnSpan = 5;

            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 1;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);




            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Female";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 9;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);



            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Male";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 9;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);



            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Female";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 12;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);



            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Male";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 12;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);


            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Female";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 12;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);



            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Male";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 12;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);

            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Female";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 15;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);


            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Male";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 15;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);


            gvReportCluster.Controls[0].Controls.AddAt(1, HeaderGridRow11);






            GridView HeaderGrid1 = (GridView)sender;
            GridViewRow HeaderGridRow1 = new GridViewRow(1, 2, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow1.CssClass = "gridnewheadercss";
            TableCell HeaderCell1;

            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;


            HeaderCell1.ColumnSpan = 1;

            //  HeaderCell1.ColumnSpan = 5;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Enrolled With SR";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Follow Up";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Ineligible";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Enrolled With SR";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Follow Up";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Ineligible";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "NRSTC";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "KGBV";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;



            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Aanganwadi";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Mainstream";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "NRSTC";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "KGBV";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;



            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Aanganwadi";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Mainstream";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Enrolled Info by Parents";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Ready to be Enrolled";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Not Ready-School Distance";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Ready to be Enrolled";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);

            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Not Ready-Other Reason";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Ready to be Enrolled";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Not Ready-School Distance";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Not Ready-Other Reason";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Migration";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Overage";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Underage";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Typing Error";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Death";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Migration";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Overage";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Underage";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Typing Error";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Death";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);

            gvReportCluster.Controls[0].Controls.AddAt(2, HeaderGridRow1);

        }

    }


    protected void gvReportClusterOutrich_RowCreated(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.Header)
        {


            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow.CssClass = "gridnewheadercss";
            TableCell HeaderCell;

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;


            HeaderCell.ColumnSpan = 4;

            //  HeaderCell.ColumnSpan = 5;

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 2;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);




            HeaderCell = new TableCell();
            HeaderCell.Text = "Status of Contact";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 18;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);



            HeaderCell = new TableCell();
            HeaderCell.Text = "Enrolled";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 24;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);



            HeaderCell = new TableCell();
            HeaderCell.Text = "FollowUp";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 24;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "Ineligible";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 31;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);
            gvReportClusterOutrich.Controls[0].Controls.AddAt(0, HeaderGridRow);





            GridView HeaderGrid11 = (GridView)sender;
            GridViewRow HeaderGridRow11 = new GridViewRow(1, 1, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow11.CssClass = "gridnewheadercss";
            TableCell HeaderCell11;

            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;


            HeaderCell11.ColumnSpan = 4;

            //  HeaderCell11.ColumnSpan = 5;

            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 2;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);




            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Female";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 9;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);



            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Male";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 9;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);



            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Female";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 12;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);



            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Male";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 12;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);


            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Female";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 12;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);



            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Male";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 12;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);

            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Female";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 15;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);


            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Male";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 15;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);


            gvReportClusterOutrich.Controls[0].Controls.AddAt(1, HeaderGridRow11);






            GridView HeaderGrid1 = (GridView)sender;
            GridViewRow HeaderGridRow1 = new GridViewRow(1, 2, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow1.CssClass = "gridnewheadercss";
            TableCell HeaderCell1;

            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;


            HeaderCell1.ColumnSpan = 2;

            //  HeaderCell1.ColumnSpan = 5;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Enrolled With SR";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Follow Up";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Ineligible";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Enrolled With SR";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Follow Up";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Ineligible";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "NRSTC";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "KGBV";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;



            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Aanganwadi";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Mainstream";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "NRSTC";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "KGBV";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;



            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Aanganwadi";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Mainstream";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Enrolled Info by Parents";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Ready to be Enrolled";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Not Ready-School Distance";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Ready to be Enrolled";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);

            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Not Ready-Other Reason";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Ready to be Enrolled";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Not Ready-School Distance";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Not Ready-Other Reason";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Migration";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Overage";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Underage";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Typing Error";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Death";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Migration";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Overage";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Underage";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Typing Error";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Death";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);

            gvReportClusterOutrich.Controls[0].Controls.AddAt(2, HeaderGridRow1);

        }

    }

    protected void gvReport_RowCreated(object sender, GridViewRowEventArgs e)
    {

        if (Session["ABC"].ToString() == "B")
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {


                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                HeaderGridRow.CssClass = "gridnewheadercss";
                TableCell HeaderCell;

                HeaderCell = new TableCell();
                HeaderCell.Text = "";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                HeaderCell.ColumnSpan = 4;

                //  HeaderCell.ColumnSpan = 5;

                HeaderCell = new TableCell();
                HeaderCell.Text = "";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 1;

                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);




                HeaderCell = new TableCell();
                HeaderCell.Text = "Target vs Contact Status of OOSG";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 8;

                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);



                HeaderCell = new TableCell();
                HeaderCell.Text = "Target vs Contact Status of OOSB";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 8;

                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);



                HeaderCell = new TableCell();
                HeaderCell.Text = "Remaning OOSG";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 4;

                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);


                HeaderCell = new TableCell();
                HeaderCell.Text = "Remaning OOSB";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 25;

                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);
                gvReport.Controls[0].Controls.AddAt(0, HeaderGridRow);








                GridView HeaderGrid1 = (GridView)sender;
                GridViewRow HeaderGridRow1 = new GridViewRow(1, 1, DataControlRowType.Header, DataControlRowState.Insert);
                HeaderGridRow1.CssClass = "gridnewheadercss";
                TableCell HeaderCell1;

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;


                HeaderCell1.ColumnSpan = 1;

                //  HeaderCell1.ColumnSpan = 5;
                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);


                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "5 to 6 Yrs";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);


                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "7 to 9";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 2;

                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);




                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "10 to 14";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);


                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "TOTAL";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 2;

                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);


                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "5 to 6 Yrs";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);


                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "7 to 9";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 2;

                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);




                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "10 to 14";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);


                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "TOTAL";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 2;



                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);



                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "5 to 6 Yrs";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 1;
                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);


                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "7 to 9";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 1;

                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);




                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "10 to 14";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 1;
                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);



                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Total";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 1;
                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);


                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "5 to 6 Yrs";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 1;
                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);


                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "7 to 9";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 1;

                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);




                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "10 to 14";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 1;
                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Total";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 1;
                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);

                gvReport.Controls[0].Controls.AddAt(1, HeaderGridRow1);

            }
        }
    }

    protected void gvReportNew_RowCreated(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.Header)
        {


            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow.CssClass = "gridnewheadercss";
            TableCell HeaderCell;

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;


            HeaderCell.ColumnSpan = 4;

            //  HeaderCell.ColumnSpan = 5;

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 2;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);




            HeaderCell = new TableCell();
            HeaderCell.Text = "Target vs Contact Status of OOSG";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 8;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);



            HeaderCell = new TableCell();
            HeaderCell.Text = "Target vs Contact Status of OOSB";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 8;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);



            HeaderCell = new TableCell();
            HeaderCell.Text = "Remaning OOSG";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 4;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "Remaning OOSB";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 25;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);
            gvReportNew.Controls[0].Controls.AddAt(0, HeaderGridRow);








            GridView HeaderGrid1 = (GridView)sender;
            GridViewRow HeaderGridRow1 = new GridViewRow(1, 1, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow1.CssClass = "gridnewheadercss";
            TableCell HeaderCell1;

            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;


            HeaderCell1.ColumnSpan = 2;

            //  HeaderCell1.ColumnSpan = 5;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "5 to 6 Yrs";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 2;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "7 to 9";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 2;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "10 to 14";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 2;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "TOTAL";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 2;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "5 to 6 Yrs";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 2;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "7 to 9";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 2;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "10 to 14";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 2;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "TOTAL";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 2;



            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "5 to 6 Yrs";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 1;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "7 to 9";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 1;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "10 to 14";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 1;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Total";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 1;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "5 to 6 Yrs";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 1;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "7 to 9";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 1;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "10 to 14";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 1;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);

            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Total";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 1;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);

            gvReportNew.Controls[0].Controls.AddAt(1, HeaderGridRow1);


        }
    }

    public void LoadContactSummary(string Flag)
    {
        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";
        GvSip.Visible = true;

        Session["ABC"] = "B";

        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');

        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            Con += " and tblDTDMobileActivity.ActivityDate between ('" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + "') ";
        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            Int32 ih = 0;
            Int32 iK = 0;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                iK = Convert.ToInt32(Year1[1]);
                ih = Convert.ToInt32(Year1[1]);
            }
            else
            {
                iK = Convert.ToInt32(Year1[0]);
                ih = Convert.ToInt32(Year1[0]); ;
            }

            int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                ih = 2019;
                mMonth = 12;
            }

            string fDate = "";
            string tate = "";
            DateTime trmDate;
            DateTime frmDate;
            fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
            frmDate = Convert.ToDateTime(fDate);


            tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
            trmDate = Convert.ToDateTime(tate);

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
                trmDate = Convert.ToDateTime(tate);
            }

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
            {

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
                frmDate = Convert.ToDateTime(fDate);

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
                frmDate = Convert.ToDateTime(fDate);
            }

            Con += " and tblDTDMobileActivity.ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";

        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 3)
        {
            string fDate = "";
            string tate = "";
            DateTime trmDate;
            DateTime frmDate;
            Int32 ih = 0;
            Int32 iK = 0;


            if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                iK = Convert.ToInt32(Year1[1]);
            }
            else
            {
                iK = Convert.ToInt32(Year1[0]);
            }

            if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlToMonth.SelectedValue) == 2 || Convert.ToInt32(ddlToMonth.SelectedValue) == 3)
            {
                ih = Convert.ToInt32(Year1[1]);
            }
            else
            {
                ih = Convert.ToInt32(Year1[0]);
            }
            int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                ih = 2019;
                mMonth = 12;
            }

            fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
            frmDate = Convert.ToDateTime(fDate);

            tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlToMonth.SelectedValue)) + "" + "-" + "25";
            trmDate = Convert.ToDateTime(tate);

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
                trmDate = Convert.ToDateTime(tate);
            }

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
            {

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
                frmDate = Convert.ToDateTime(fDate);

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
                frmDate = Convert.ToDateTime(fDate);
            }
            Con += " and tblDTDMobileActivity.ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";


        }


        //if (ddlYear.SelectedIndex > 0)
        //{
        //    conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        //}
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }




        dtMain = rptContactSummary(conditions1 + Con, conditions1, Flag, ddlYear.SelectedItem.Text, Convert.ToInt32(ddlYear.SelectedValue));
        ViewState["dt"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {
            Session["DtTrarget"] = dtMain;
            gvReport.DataSource = dtMain;
            gvReport.DataBind();

            // GenerateExcelNew("dddddsf");
        }
        else
        {
            gvReport.DataSource = null;
            gvReport.DataBind();
        }




    }


    public void LoadContactClusterSummary(string Flag)
    {
        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";
        GvSip.Visible = true;

        Session["ABC"] = "B";

        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');

        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            Con += " and tblDTDMobileActivity.ActivityDate between ('" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + "') ";
        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            Int32 ih = 0;
            Int32 iK = 0;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                iK = Convert.ToInt32(Year1[1]);
                ih = Convert.ToInt32(Year1[1]);
            }
            else
            {
                iK = Convert.ToInt32(Year1[0]);
                ih = Convert.ToInt32(Year1[0]); ;
            }

            int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                ih = 2019;
                mMonth = 12;
            }

            string fDate = "";
            string tate = "";
            DateTime trmDate;
            DateTime frmDate;
            fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
            frmDate = Convert.ToDateTime(fDate);


            tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
            trmDate = Convert.ToDateTime(tate);

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
                trmDate = Convert.ToDateTime(tate);
            }

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
            {

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
                frmDate = Convert.ToDateTime(fDate);

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
                frmDate = Convert.ToDateTime(fDate);
            }

            Con += " and tblDTDMobileActivity.ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";

        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 3)
        {
            string fDate = "";
            string tate = "";
            DateTime trmDate;
            DateTime frmDate;
            Int32 ih = 0;
            Int32 iK = 0;


            if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                iK = Convert.ToInt32(Year1[1]);
            }
            else
            {
                iK = Convert.ToInt32(Year1[0]);
            }

            if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlToMonth.SelectedValue) == 2 || Convert.ToInt32(ddlToMonth.SelectedValue) == 3)
            {
                ih = Convert.ToInt32(Year1[1]);
            }
            else
            {
                ih = Convert.ToInt32(Year1[0]);
            }
            int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                ih = 2019;
                mMonth = 12;
            }

            fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
            frmDate = Convert.ToDateTime(fDate);

            tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlToMonth.SelectedValue)) + "" + "-" + "25";
            trmDate = Convert.ToDateTime(tate);

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
                trmDate = Convert.ToDateTime(tate);
            }

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
            {

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
                frmDate = Convert.ToDateTime(fDate);

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
                frmDate = Convert.ToDateTime(fDate);
            }
            Con += " and tblDTDMobileActivity.ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";


        }


        //if (ddlYear.SelectedIndex > 0)
        //{
        //    conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        //}
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }




        dtMain = rptContactSummary(conditions1 + Con, conditions1, "2", ddlYear.SelectedItem.Text, Convert.ToInt32(ddlYear.SelectedValue));
        ViewState["dt"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {
            Session["DtTrargetC"] = dtMain;
            gvReportNew.DataSource = dtMain;
            gvReportNew.DataBind();

            // GenerateExcelNew("dddddsf");
        }
        else
        {
            gvReportNew.DataSource = null;
            gvReportNew.DataBind();
        }




    }
    public DataTable rptContactSummary(string WhereQuery, string conditions1, string Flag, string Fyear, Int32 yYear)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {

            new SqlParameter("@schoolCode", WhereQuery),
            new SqlParameter("@Con", conditions1),
                new SqlParameter("@Flag", Flag),
            new SqlParameter("@Fyear", Fyear),
            new SqlParameter("@yYear", yYear),

        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptD2d2ContactBlockWiseSummary]", cmdParameters);
    }
    public void LoadContactClusterOutReachNew(string Flag)
    {
        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";
        GvSip.Visible = true;

        Session["ABC"] = "B";

        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');

        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            Con += " and tblDTDMobileActivity.ActivityDate between ('" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + "') ";
        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            Int32 ih = 0;
            Int32 iK = 0;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                iK = Convert.ToInt32(Year1[1]);
                ih = Convert.ToInt32(Year1[1]);
            }
            else
            {
                iK = Convert.ToInt32(Year1[0]);
                ih = Convert.ToInt32(Year1[0]); ;
            }

            int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                ih = 2019;
                mMonth = 12;
            }

            string fDate = "";
            string tate = "";
            DateTime trmDate;
            DateTime frmDate;
            fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
            frmDate = Convert.ToDateTime(fDate);


            tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
            trmDate = Convert.ToDateTime(tate);

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
                trmDate = Convert.ToDateTime(tate);
            }

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
            {

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
                frmDate = Convert.ToDateTime(fDate);

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
                frmDate = Convert.ToDateTime(fDate);
            }

            Con += " and tblDTDMobileActivity.ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";

        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 3)
        {
            string fDate = "";
            string tate = "";
            DateTime trmDate;
            DateTime frmDate;
            Int32 ih = 0;
            Int32 iK = 0;


            if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                iK = Convert.ToInt32(Year1[1]);
            }
            else
            {
                iK = Convert.ToInt32(Year1[0]);
            }

            if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlToMonth.SelectedValue) == 2 || Convert.ToInt32(ddlToMonth.SelectedValue) == 3)
            {
                ih = Convert.ToInt32(Year1[1]);
            }
            else
            {
                ih = Convert.ToInt32(Year1[0]);
            }
            int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                ih = 2019;
                mMonth = 12;
            }

            fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
            frmDate = Convert.ToDateTime(fDate);

            tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlToMonth.SelectedValue)) + "" + "-" + "25";
            trmDate = Convert.ToDateTime(tate);

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
                trmDate = Convert.ToDateTime(tate);
            }

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
            {

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
                frmDate = Convert.ToDateTime(fDate);

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
                frmDate = Convert.ToDateTime(fDate);
            }
            Con += " and tblDTDMobileActivity.ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";


        }


        //if (ddlYear.SelectedIndex > 0)
        //{
        //    conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        //}
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }




        dtMain = rptContactSummaryOutReach(conditions1 + Con, conditions1, "2", ddlYear.SelectedItem.Text, Convert.ToInt32(ddlYear.SelectedValue));
        ViewState["dt"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {
            Session["ClusteTrargetCNew"] = dtMain;
            gvReportClusterOutrich.DataSource = dtMain;
            gvReportClusterOutrich.DataBind();

            // GenerateExcelNew("dddddsf");
        }
        else
        {
            gvReportClusterOutrich.DataSource = null;
            gvReportClusterOutrich.DataBind();
        }




    }

    public void LoadContactClusterOutReach(string Flag)
    {
        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";
        GvSip.Visible = true;

        Session["ABC"] = "B";

        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');

        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            Con += " and tblDTDMobileActivity.ActivityDate between ('" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + "') ";
        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            Int32 ih = 0;
            Int32 iK = 0;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                iK = Convert.ToInt32(Year1[1]);
                ih = Convert.ToInt32(Year1[1]);
            }
            else
            {
                iK = Convert.ToInt32(Year1[0]);
                ih = Convert.ToInt32(Year1[0]); ;
            }

            int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                ih = 2019;
                mMonth = 12;
            }

            string fDate = "";
            string tate = "";
            DateTime trmDate;
            DateTime frmDate;
            fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
            frmDate = Convert.ToDateTime(fDate);


            tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
            trmDate = Convert.ToDateTime(tate);

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
                trmDate = Convert.ToDateTime(tate);
            }

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
            {

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
                frmDate = Convert.ToDateTime(fDate);

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
                frmDate = Convert.ToDateTime(fDate);
            }

            Con += " and tblDTDMobileActivity.ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";

        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 3)
        {
            string fDate = "";
            string tate = "";
            DateTime trmDate;
            DateTime frmDate;
            Int32 ih = 0;
            Int32 iK = 0;


            if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                iK = Convert.ToInt32(Year1[1]);
            }
            else
            {
                iK = Convert.ToInt32(Year1[0]);
            }

            if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlToMonth.SelectedValue) == 2 || Convert.ToInt32(ddlToMonth.SelectedValue) == 3)
            {
                ih = Convert.ToInt32(Year1[1]);
            }
            else
            {
                ih = Convert.ToInt32(Year1[0]);
            }
            int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                ih = 2019;
                mMonth = 12;
            }

            fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
            frmDate = Convert.ToDateTime(fDate);

            tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlToMonth.SelectedValue)) + "" + "-" + "25";
            trmDate = Convert.ToDateTime(tate);

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
                trmDate = Convert.ToDateTime(tate);
            }

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
            {

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
                frmDate = Convert.ToDateTime(fDate);

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
                frmDate = Convert.ToDateTime(fDate);
            }
            Con += " and tblDTDMobileActivity.ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";


        }


        //if (ddlYear.SelectedIndex > 0)
        //{
        //    conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        //}
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }




        dtMain = rptContactSummaryOutReach(conditions1 + Con, conditions1, "1", ddlYear.SelectedItem.Text, Convert.ToInt32(ddlYear.SelectedValue));
        ViewState["dt"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {
            Session["ClusteTrargetC"] = dtMain;
            gvReportCluster.DataSource = dtMain;
            gvReportCluster.DataBind();

            // GenerateExcelNew("dddddsf");
        }
        else
        {
            gvReportCluster.DataSource = null;
            gvReportCluster.DataBind();
        }




    }
    public DataTable rptContactSummaryOutReach(string WhereQuery, string conditions1, string Flag, string Fyear, Int32 Yyear)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {

            new SqlParameter("@schoolCode", WhereQuery),
            new SqlParameter("@Flag", Flag),
              new SqlParameter("@Fyear",Fyear),
                new SqlParameter("@Yyear",Yyear),
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptD2d2ContactBlockWiseSummaryOutReach]", cmdParameters);
    }
    protected void ContactSummary_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = false;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "9000";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        GV_DynamicGrid2.Visible = false;
        gvReport.Visible = true;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;

        if (ddlType.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlType.SelectedValue) == 1)
            {

                LoadContactSummary("1");


            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 2)
            {
                if (ddlMonth.SelectedIndex > 0)
                {
                    LoadContactSummary("1");

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  month ')</script>", false);
                }
            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 3)
            {

                if (ddlMonth.SelectedIndex > 0 && ddlToMonth.SelectedIndex > 0)
                {
                    LoadContactSummary("1");

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select From month and To month')</script>", false);
                }
            }

            if (Convert.ToInt32(ddlType.SelectedValue) == 4)
            {


                LoadContactSummary("1");



            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Type')</script>", false);
        }
    }
    protected void ClusterWise_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = false;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "9001";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        GV_DynamicGrid2.Visible = false;
        gvReport.Visible = false;
        gvReportNew.Visible = true;
        gvReportClusterOutrich.Visible = false;
        gvReportCluster.Visible = false;
        if (ddlType.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlType.SelectedValue) == 1)
            {

                LoadContactClusterSummary("2");

            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 2)
            {
                if (ddlMonth.SelectedIndex > 0)
                {
                    LoadContactClusterSummary("2");

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  month ')</script>", false);
                }
            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 3)
            {

                if (ddlMonth.SelectedIndex > 0 && ddlToMonth.SelectedIndex > 0)
                {
                    LoadContactClusterSummary("2");

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select From month and To month')</script>", false);
                }
            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 4)
            {


                LoadContactClusterSummary("2");


            }



        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Type')</script>", false);
        }
    }



    protected void Outreach_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = false;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "9005";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        GV_DynamicGrid2.Visible = false;
        gvReport.Visible = false;
        gvReportNew.Visible = false;
        gvReportCluster.Visible = true;
        gvReportClusterOutrich.Visible = false;
        gvReportNew.Visible = false;
        if (ddlType.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlType.SelectedValue) == 1)
            {

                LoadContactClusterOutReach("1");

            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 2)
            {
                if (ddlMonth.SelectedIndex > 0)
                {
                    LoadContactClusterOutReach("1");

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  month ')</script>", false);
                }
            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 3)
            {

                if (ddlMonth.SelectedIndex > 0 && ddlToMonth.SelectedIndex > 0)
                {
                    LoadContactClusterOutReach("1");

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select From month and To month')</script>", false);
                }
            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 4)
            {


                LoadContactClusterOutReach("1");


            }



        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Type')</script>", false);
        }
    }


    protected void OutreachCluster_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = false;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "9007";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        GV_DynamicGrid2.Visible = false;
        gvReport.Visible = false;
        gvReportNew.Visible = false;
        gvReportCluster.Visible = false;
        gvReportClusterOutrich.Visible = true;

        if (ddlType.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlType.SelectedValue) == 1)
            {

                LoadContactClusterOutReachNew("2");

            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 2)
            {
                if (ddlMonth.SelectedIndex > 0)
                {
                    LoadContactClusterOutReachNew("2");

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  month ')</script>", false);
                }
            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 3)
            {

                if (ddlMonth.SelectedIndex > 0 && ddlToMonth.SelectedIndex > 0)
                {
                    LoadContactClusterOutReachNew("2");

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select From month and To month')</script>", false);
                }
            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 4)
            {

                LoadContactClusterOutReachNew("2");

            }


        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Type')</script>", false);
        }
    }
    public void LoadUserLeavel()
    {
        conditions = "";
        AlllStateCode();
        if (Session["user_level_Role"].ToString() == "1")
        {

            //string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State   order by StateName   ";
            //DataTable dtState = objMain.LoadData(strQry1);
            //ChkState.DataSource = dtState;
            //ChkState.DataTextField = "StateName";
            //ChkState.DataValueField = "StateCode";
            //ChkState.DataBind();

            ChkState.Enabled = true;
            chkDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            //conditions = "UserName='" + Session["username"].ToString() + "' ";
            //string strQry1 = "  SELECT distinct mst1State.StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode inner join mst1State on mst1State.StateCode=mst2District.StateCode where   " + conditions + "  order by StateName   ";
            //DataTable dtState = objMain.LoadData(strQry1);
            //ChkState.DataSource = dtState;
            //ChkState.DataTextField = "StateName";
            //ChkState.DataValueField = "StateCode";
            //ChkState.DataBind();
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            foreach (ListItem item in ChkState.Items)
            {

                item.Selected = true;

            }

            ChkState.Enabled = true;
            chkDistrict.Enabled = true;
        }
        else
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            ////objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            //string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State  where   " + conditions + "  order by StateName   ";
            //DataTable dtState = objMain.LoadData(strQry1);
            //ChkState.DataSource = dtState;
            //ChkState.DataTextField = "StateName";
            //ChkState.DataValueField = "StateCode";
            //ChkState.DataBind();
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            foreach (ListItem item in ChkState.Items)
            {

                item.Selected = true;

            }
            // ChkState.SelectedIndex = 1;
            ChkState.Enabled = false;
            chkDistrict.Enabled = false;
        }


        if (Session["user_level_Role"].ToString() == "1")
        {
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            string ddlState = "";

            foreach (ListItem item in ChkState.Items)
            {
                if (item.Selected)
                {

                    ddlState += "'" + item.Value + "'" + ",";


                }
            }
            conditions = "";
            //  conditions = "StateCode in(" + ddlState + ") and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
            conditions = "UserName='" + Session["username"].ToString() + "' ";
            string strQry1 = "  SELECT distinct mst2District.DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode  where   " + conditions + " and Fyear='" + ddlYear.SelectedItem.Text + "'  order by DistrictName   ";


            // string strQry1 = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where " + conditions + "  order by DistrictName   ";


            DataTable dtDistrict = objMain.LoadData(strQry1);
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            chkDistrict.DataSource = dtDistrict;
            chkDistrict.DataTextField = "DistrictName";
            chkDistrict.DataValueField = "DistrictCode";
            chkDistrict.DataBind();


            foreach (ListItem item in chkDistrict.Items)
            {

                item.Selected = true;
                break;
            }
            //ddlDistrict.SelectedIndex = 0;

            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }

        else
        {

            string ddlState = "";

            foreach (ListItem item in ChkState.Items)
            {
                if (item.Selected)
                {

                    ddlState += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlState.Length > 0)
            {
                ddlState = ddlState.Substring(0, ddlState.LastIndexOf(","));
            }
            conditions = "";
            conditions = "StateCode in(" + ddlState + ") and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and Fyear= '" + ddlYear.SelectedItem.Text + "' ";
            //  objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");
            string strQry1 = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where " + conditions + "  order by DistrictName   ";
            DataTable dtDistrict = objMain.LoadData(strQry1);
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            chkDistrict.DataSource = dtDistrict;
            chkDistrict.DataTextField = "DistrictName";
            chkDistrict.DataValueField = "DistrictCode";
            chkDistrict.DataBind();
            string strQry;
            strQry = "Select * from mst2District where   DistrictCode in(" + Session["DistrictCode"].ToString() + ")";
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


            //ddlDistrict.SelectedIndex = 1;
            foreach (ListItem item in chkDistrict.Items)
            {

                item.Selected = true;

            }
            ddlDistrict_SelectedIndexChanged(chkDistrict, null);
            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }





    }
    public DataTable CreateDataTable()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYear.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYear;
    }



    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBCluster();
        FillPanchayat();
        ddlPanchayat.Enabled = true;
        SchoolLoad();
    }
    public void SchoolLoad()
    {
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        string conditions1 = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }
        conditions1 += "group by mstSchool.SchoolCode,Name ";
        //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
        objComman.BindDLL("tblActivityUpdate_School inner join mst5Village on mst5Village.VillageCode= tblActivityUpdate_School.VillageCode inner join mstSchool on mstSchool.SchoolCode= tblActivityUpdate_School.SchoolCode", " mstSchool.SchoolCode,Name  ", conditions1, "Name", "asc", ddlScholl, "Name", "SchoolCode", "--All--");

    }
    public void FillCBBock()
    {
        conditions = "";
        string ddlDistrict = "";

        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        if (Session["user_level_Role"].ToString() == "2")
        {
            if (ddlDistrict.Length > 0)
            {
            }
            else
            {
                if (chkDistrict.Items.Count > 0)
                {
                    foreach (ListItem item in chkDistrict.Items)
                    {
                        ddlDistrict += "'" + item.Value + "'" + ",";
                        item.Selected = true;
                        break;
                    }
                    if (ddlDistrict.Length > 0)
                    {
                        ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
                    }
                }
            }


        }
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "DistrictCode in(" + ddlDistrict + ") ";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "DistrictCode in(" + ddlDistrict + ")  ";
        }
        else if (Session["user_level_Role"].ToString() == "4")
        {
            conditions = "DistrictCode in(" + ddlDistrict + ")   and BlockCode in( " + Session["BlockCode"].ToString() + " ) and FYear ='" + ddlYear.SelectedItem.Text + "' ";
        }
        else
        {
            conditions = "DistrictCode in(" + ddlDistrict + ")  ";
        }
        //     objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        string strQry = "  SELECT BlockCode, dbo.TitleCase(upper(BlockName))  as BlockName FROM mst3Block where " + conditions + "  order by BlockName   ";
        DataTable dtDistrict = objMain.LoadData(strQry);
        // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        chkBlock.DataSource = dtDistrict;
        chkBlock.DataTextField = "BlockName";
        chkBlock.DataValueField = "BlockCode";
        chkBlock.DataBind();



        ddlPanchayat.Items.Clear();
        chkVillage.Items.Clear();

    }
    public void FillPanchayat()
    {

        string ddlBlock = "";
        string ddlDistrict = "";

        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }
        conditions = "";
        DataTable dtDistrict = null;
        conditions = "DistrictCode in(" + ddlDistrict + ")  and BlockCode in(" + ddlBlock + ")";
        string strQry = "  SELECT PanchayatCode, dbo.TitleCase(upper(PanchayatName))  as PanchayatName FROM mstPanchayat where " + conditions + "  order by PanchayatName   ";
        dtDistrict = objMain.LoadData(strQry);





        // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        ddlPanchayat.DataSource = dtDistrict;
        ddlPanchayat.DataTextField = "PanchayatName";
        ddlPanchayat.DataValueField = "PanchayatCode";
        ddlPanchayat.DataBind();

        // objComman.BindDLL("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "--All--");


        chkVillage.Items.Clear();

    }
    public void FillCVillageC()
    {

        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlCluserter = "";

        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in chkCluster.Items)
        {
            if (item.Selected)
            {

                ddlCluserter += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlCluserter.Length > 0)
        {
            ddlCluserter = ddlCluserter.Substring(0, ddlCluserter.LastIndexOf(","));
        }
        conditions = "";

        conditions = "DistrictCode in(" + ddlDistrict + ")  and BlockCode in(" + ddlBlock + ") and  ClusterCode in(" + ddlCluserter + ")";


        //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        //objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--All-");

        string strQry = "  SELECT VillageCode, dbo.TitleCase(upper(VillageName))  as VillageName FROM mst5Village where " + conditions + "  order by VillageName   ";
        DataTable dtDistrict = objMain.LoadData(strQry);

        chkVillage.DataSource = dtDistrict;
        chkVillage.DataTextField = "VillageName";
        chkVillage.DataValueField = "VillageCode";
        chkVillage.DataBind();


    }
    public void FillCVillageP()
    {

        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";

        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        conditions = "";

        conditions = "DistrictCode in(" + ddlDistrict + ")  and BlockCode in(" + ddlBlock + ") and  PanchayatCode in(" + ddlPhan + ")";


        //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        //objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--All-");

        string strQry = "  SELECT VillageCode, dbo.TitleCase(upper(VillageName))  as VillageName FROM mst5Village where " + conditions + "  order by VillageName   ";
        DataTable dtDistrict = objMain.LoadData(strQry);

        chkVillage.DataSource = dtDistrict;
        chkVillage.DataTextField = "VillageName";
        chkVillage.DataValueField = "VillageCode";
        chkVillage.DataBind();


    }
    public void FillCBCluster()
    {


        string ddlBlock = "";
        string ddlDistrict = "";

        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }
        conditions = "";
        DataTable dtDistrict = null;
        conditions = "DistrictCode in(" + ddlDistrict + ")  and BlockCode in(" + ddlBlock + ")";
        string strQry = "  SELECT ClusterCode, dbo.TitleCase(upper(ClusterName))  as ClusterName FROM mstcluster where " + conditions + "  order by ClusterName   ";
        dtDistrict = objMain.LoadData(strQry);
        chkCluster.DataSource = dtDistrict;
        chkCluster.DataTextField = "ClusterName";
        chkCluster.DataValueField = "ClusterCode";
        chkCluster.DataBind();

    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillageP();

        Int32 iClusterCount = 0;
        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                iClusterCount = 1;
                break;


            }
        }
        if (iClusterCount > 0)
        {
            foreach (ListItem item in chkCluster.Items)
            {
                item.Selected = false;

            }
            chkCluster.Enabled = false;
        }
        else
        {
            chkCluster.Enabled = true;
        }
        SchoolLoad();
    }
    protected void ddlCluster_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillageC();
        Int32 iClusterCount = 0;
        foreach (ListItem item in chkCluster.Items)
        {
            if (item.Selected)
            {

                iClusterCount = 1;
                break;


            }
        }
        if (iClusterCount > 0)
        {
            foreach (ListItem item in ddlPanchayat.Items)
            {
                item.Selected = false;

            }
            ddlPanchayat.Enabled = false;
        }
        else
        {
            ddlPanchayat.Enabled = true;
        }
        SchoolLoad();

    }
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        SchoolLoad();
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            ddlToMonth.SelectedIndex = 0;
            ddlMonth.SelectedIndex = 0;
            divDate.Visible = true;
            divDate1.Visible = true;
            divMonth.Visible = false;
            divToMonth.Visible = false;
        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            ddlToMonth.SelectedIndex = 0;
            ddlMonth.SelectedIndex = 0;
            divDate.Visible = true;
            divDate1.Visible = true;
            //divDate.Visible = false;
            //divDate1.Visible = false;
            divMonth.Visible = true;
            divToMonth.Visible = false;
            txtDate.Text = "";
            txtTodate.Text = "";
        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 3)
        {
            ddlToMonth.SelectedIndex = 0;
            ddlMonth.SelectedIndex = 0;
            divDate.Visible = true;
            divDate1.Visible = true;
            divMonth.Visible = true;
            divToMonth.Visible = true;
            txtDate.Text = "";
            txtTodate.Text = "";
        }

        if (Convert.ToInt32(ddlType.SelectedValue) == 4)
        {
            divDate.Visible = true;
            divDate1.Visible = true;
            divMonth.Visible = false;
            divToMonth.Visible = false;
            ddlToMonth.SelectedIndex = 0;
            ddlMonth.SelectedIndex = 0;
            txtDate.Text = "";
            txtTodate.Text = "";
        }

    }
    public void LoadYear()
    {
        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


    }
    public void FillCBState()
    {
        conditions = "";
        // objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");


        //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
        string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State   order by StateName   ";
        DataTable dtState = objMain.LoadData(strQry1);
        ChkState.DataSource = dtState;
        ChkState.DataTextField = "StateName";
        ChkState.DataValueField = "StateCode";
        ChkState.DataBind();

    }
    public void FillCBDist()
    {
        string ddlState = "";
        DataTable dtDistrict = null;
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlState += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlState.Length > 0)
        {
            ddlState = ddlState.Substring(0, ddlState.LastIndexOf(","));
        }
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "StateCode in(" + ddlState + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = " mst2District.StateCode in(" + ddlState + ") and UserName='" + Session["username"].ToString() + "' ";
        }
        else
        {
            conditions = "StateCode  in(" + ddlState + ") and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";


        }
        if (Session["user_level_Role"].ToString() == "2")
        {
            //if (ddlYear.SelectedValue.ToString() == "2016")
            //{

            //    string strQry1 = "  SELECT distinct mst2District.DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where EGDistrictCode in(     SELECT distinct mst2District.EGDistrictCode  FROM MstusermultipleDist     where   " + conditions + " )  and Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName   ";
            //    dtDistrict = objMain.LoadData(strQry1);
            //}

            //if (ddlYear.SelectedValue.ToString() == "2017")
            //{

            //    string strQry1 = "  SELECT distinct mst2District.DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where EGDistrictCode in(     SELECT distinct mst2District.EGDistrictCode  FROM MstusermultipleDist     where   " + conditions + " )  and Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName   ";
            //    dtDistrict = objMain.LoadData(strQry1);
            //}
            //if (ddlYear.SelectedValue.ToString() == "2018")
            //{

            //    string strQry1 = "  SELECT distinct mst2District.DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist   inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode  where   " + conditions + " and Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName   ";
            //    dtDistrict = objMain.LoadData(strQry1);
            //}
            string strQry1 = "       sELECT distinct mst2District.DistrictCode as DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist  ";
            strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions + "  and  Fyear='" + ddlYear.SelectedItem.Text + "'  order by DistrictName  ";
            dtDistrict = objMain.LoadData(strQry1);
        }
        else
        {
            string strQry = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where " + conditions + "  order by DistrictName   ";
            dtDistrict = objMain.LoadData(strQry);
        }

        // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        chkDistrict.DataSource = dtDistrict;
        chkDistrict.DataTextField = "DistrictName";
        chkDistrict.DataValueField = "DistrictCode";
        chkDistrict.DataBind();


        ddlPanchayat.Items.Clear();
        chkVillage.Items.Clear();
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
            ChkState.DataSource = dtAllState;
            ChkState.DataTextField = "StateName";
            ChkState.DataValueField = "StateCode";
            ChkState.DataBind();
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
            ChkState.DataSource = dtAllState;
            ChkState.DataTextField = "StateName";
            ChkState.DataValueField = "StateCode";
            ChkState.DataBind();
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
            ChkState.DataSource = dtAllState;
            ChkState.DataTextField = "StateName";
            ChkState.DataValueField = "StateCode";
            ChkState.DataBind();

            foreach (ListItem item in ChkState.Items)
            {

                item.Selected = true;

            }

        }

    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            AlllStateCode();
            if (Session["user_level_Role"].ToString() == "3" || Session["user_level_Role"].ToString() == "4")
            {

            }
            else
            {
                foreach (ListItem item in ChkState.Items)
                {

                    item.Selected = false;

                }
            }

            if (Session["user_level_Role"].ToString() == "2")
            {

                //conditions = "UserName='" + Session["username"].ToString() + "' ";
                //string strQry1 = "  SELECT distinct mst1State.StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode inner join mst1State on mst1State.StateCode=mst2District.StateCode where   " + conditions + "  order by StateName   ";
                //DataTable dtState = objMain.LoadData(strQry1);
                //ChkState.DataSource = dtState;
                //ChkState.DataTextField = "StateName";
                //ChkState.DataValueField = "StateCode";
                //ChkState.DataBind();
                foreach (ListItem item in ChkState.Items)
                {

                    item.Selected = true;

                }
            }
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");



            ddlState_SelectedIndexChanged(chkDistrict, null);
            if (Session["user_level_Role"].ToString() == "3" || Session["user_level_Role"].ToString() == "4")
            {
                if (chkDistrict.Items.Count > 0)
                {
                    foreach (ListItem item in chkDistrict.Items)
                    {

                        item.Selected = true;

                    }
                }
            }

            ddlDistrict_SelectedIndexChanged(chkDistrict, null);

            ddlPanchayat.Items.Clear();
            chkVillage.Items.Clear();
        }
        else
        {
            foreach (ListItem item in ChkState.Items)
            {

                item.Selected = false;

            }
            chkDistrict.Items.Clear();
            chkBlock.Items.Clear();
            ddlPanchayat.Items.Clear();
            chkVillage.Items.Clear();
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["Button"] = " ";
        FillCBDist();
        chkBlock.Items.Clear();
        chkCluster.Items.Clear();
        chkVillage.Items.Clear();
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["Button"] = " ";
        FillCBBock();


        chkCluster.Items.Clear();
        chkVillage.Items.Clear();
    }
    public void getAnnaulReport(Int32 Flag)
    {
        conditions = "";


        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }

        if (ddlScholl.SelectedIndex > 0)
        {
            conditions += " and mstSchool.SchoolCode ='" + ddlScholl.SelectedValue + "' ";
        }


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@schoolCode",conditions),

        };
        DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivityQuerltyAnnualNew]", cmdParameters);

        ViewState["dt"] = dataTable;
        if (dataTable.Rows.Count > 0)
        {

            gvQuerltyAnnual.DataSource = dataTable;
            gvQuerltyAnnual.DataBind();

            return;
        }
        gvQuerltyAnnual.DataSource = null;
        gvQuerltyAnnual.DataBind();


    }
    public void getreport(Int32 Flag)
    {
        conditions = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + "  where  mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }
        if (Flag == 2)
        {
            if (ddlScholl.SelectedIndex > 0)
            {
                conditions += " and mstSchool.SchoolCode ='" + ddlScholl.SelectedValue + "' ";
            }
        }

        if (Flag == 3)
        {
            if (ddlContact.SelectedIndex > 0)
            {
                conditions += " and mstActivityLookup.Lookupcode='" + ddlContact.SelectedValue + "' ";
            }
        }
        Int32 iApp = 0;
        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
        {
            iApp = 1;
        }
        else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
        {
            iApp = 2;
        }
        else
        {
            iApp = 3;
        }
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Cond", conditions),
                new SqlParameter("@Flag", Flag),
                       new SqlParameter("@iApp", iApp),
        };
        DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptVillageReportCard]", cmdParameters);
        if (Flag == 2)
        {

            // lblTotalCount.Text = dataTable.Rows.Count.ToString();

            DataView DV = dataTable.DefaultView;

            DV.RowFilter = string.Format("GroupName LIKE '%{0}%'", "School");
            ViewState["dt"] = dataTable;
            if (dataTable.Rows.Count > 0)
            {

                DGV_Report.DataSource = DV;
                DGV_Report.DataBind();

                return;
            }
            DGV_Report.DataSource = null;
            DGV_Report.DataBind();
        }
        if (Flag == 1)
        {

            lblTotalCount.Text = dataTable.Rows.Count.ToString();
            ViewState["dt"] = dataTable;

            if (dataTable.Rows.Count > 0)
            {

                DGV_Report.DataSource = dataTable;
                DGV_Report.DataBind();

                return;
            }
            DGV_Report.DataSource = null;
            DGV_Report.DataBind();
        }
        if (Flag == 3)
        {

            lblTotalCount.Text = dataTable.Rows.Count.ToString();
            ViewState["dt"] = dataTable;
            if (dataTable.Rows.Count > 500)
            {
                ExportToCSVFile(dataTable, "ContactReport");
            }
            else

            {

                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();

                return;
            }
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }

        if (Flag == 8)
        {

            lblTotalCount.Text = dataTable.Rows.Count.ToString();
            ViewState["dt"] = dataTable;
            if (dataTable.Rows.Count > 0)
            {

                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();

                return;
            }
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }
        if (Flag == 9)
        {

            lblTotalCount.Text = dataTable.Rows.Count.ToString();
            ViewState["dt"] = dataTable;
            if (dataTable.Rows.Count > 0)
            {

                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();

                return;
            }
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }
        if (Flag == 10)
        {

            lblTotalCount.Text = dataTable.Rows.Count.ToString();
            ViewState["dt"] = dataTable;
            if (dataTable.Rows.Count > 0)
            {

                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();

                return;
            }
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }
        if (Flag == 11)
        {

            lblTotalCount.Text = dataTable.Rows.Count.ToString();
            ViewState["dt"] = dataTable;
            if (dataTable.Rows.Count > 0)
            {

                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();

                return;
            }
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }

        if (Flag == 12)
        {

            lblTotalCount.Text = dataTable.Rows.Count.ToString();
            ViewState["dt"] = dataTable;
            if (dataTable.Rows.Count > 0)
            {

                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();

                return;
            }
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }
    }


    protected void PMS_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = true;
        GV_DynamicGrid2.Visible = false;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "2";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReport.Visible = false;
        gvReportClusterOutrich.Visible = false;
        getreport(1);

    }
    protected void Age_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "1";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getreport(8);

    }
    protected void Annaul_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = false;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "14";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = true;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getAnnaulReport(0);


    }
    protected void Approve_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "316";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getApproveReport(0);


    }
    protected void GKP_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "5216";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getGKP(0);


    }
    protected void SACCurren_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "216";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getSACReport(0);


    }
    protected void SSACCluster_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "216";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getrptActivitySACReportSummary(0);


    }

    public void getrptActivitySACReportSummary(Int32 Flag)
    {
        conditions = "";


        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }

        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }

        if (ddlScholl.SelectedIndex > 0)
        {
            conditions += " and mstSchool.SchoolCode ='" + ddlScholl.SelectedValue + "' ";
        }


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),

        };
        DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivitySACReportSummary]", cmdParameters);

        ViewState["dt"] = dataTable;
        if (dataTable.Rows.Count > 0)
        {

            GV_DynamicGrid2.DataSource = dataTable;
            GV_DynamicGrid2.DataBind();

            return;
        }
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();


    }
    protected void SACLastCurren_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "216";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getSACLastMonthReport(0);


    }
    protected void Balsaba_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "999";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getBalsabhaCLusterWise(1);


    }
    protected void BalsabaDistrict_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "9991";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getBalsabhaCLusterWise(3);


    }
    protected void BalsabaBlock_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "9992";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getBalsabhaCLusterWise(2);


    }
    protected void BalsabaRawData_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "9998";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2021)
        {
            getBalsabhaCLusterWiseNewReport(1);
        }
        else
        {
            getBalsabhaCLusterWise(4);
        }
    }
    protected void BalsabaRawDataKG_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "9998";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2021)
        {
            getBalsabhaCLusterWiseNewReportKGBV(1);
        }
    }
    protected void BalsabaRawDataLifftt_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "9998";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2021)
        {
            getBalsabhaCLusterWiseNewReportKGBV(2);
        }
    }

    protected void BalsabaRawDataLifftt_Click1(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "9998";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2021)
        {
            getBalsabhaCLusterWiseNewReportKGBV(4);
        }
    }
    public void getBalsabhaCLusterWiseNewReportKGBV(Int32 Flag)
    {
        conditions = "";


        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }

        string Con = "";
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');


        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + " and   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }

        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }
        //if (Flag == 1)
        //{
        //    if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
        //    {
        //        conditions += "   and ApproveStatus='FC' ";
        //    }
        //    else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
        //    {
        //        conditions += "   and ApproveStatus='FC' ";
        //    }
        //    else
        //    {
        //        conditions += "  and ApproveStatus='FC' ";
        //    }
        //}
        //if (Flag == 2)
        //{
        //    if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
        //    {
        //        conditions += "   and tblChildAttendanceLifeskill.ApproveStatus='FC' ";
        //    }
        //    else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
        //    {
        //        conditions += "   and tblChildAttendanceLifeskill.ApproveStatus='FC' ";
        //    }
        //    else
        //    {
        //        conditions += "  and tblChildAttendanceLifeskill.ApproveStatus='FC' ";
        //    }
        //}
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions ),
            new SqlParameter("@Flag",Flag),

        };
        DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptBalsabaNewKKGBV", cmdParameters);

        ViewState["dt"] = dataTable;
        if (dataTable.Rows.Count > 0)
        {
            if (Flag == 1)
            {
                // objMain.ReportDownload("Balsabha Report", "Balsabha- Child Registration", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, true, "Child Name", "Father Name", "Contact No", "DOB");

                ExportToCSVFile(dataTable, "KGBVChildRegistration");
            }
            if (Flag == 2)
            {
                // objMain.ReportDownload("Balsabha Report", "LSE Attendance Detail", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, true, "Child Name", "Father Name", "Contact No", "DOB");

                ExportToCSVFile(dataTable, "KGBVAttendanceDetail");
            }
            if (Flag == 4)
            {
                //  objMain.ReportDownload("Balsabha Report", "LSE Attendance Detail", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, true, "Child Name", "Father Name", "Contact No", "DOB");

                ExportToCSVFile(dataTable, "KGBVAssessmentDetail");
            }
        }
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();
    }
    protected void Balsabasumll_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "9ww998";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;

        getBalsabhaCLusterWiseNewReportLiffSummry(1);



    }

    protected void BalsabaRawDatahf_Click2(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "9ww998";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;

        getBalsabhaLSESummry(1);



    }
    protected void BalsabaRawDataLiff_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "9998";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2021)
        {
            getBalsabhaCLusterWiseNewReport(2);
        }
    }
    protected void BalsabaRawDataLiff_Click1(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "9998";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2024)
        {
            getBalsabhaCLusterWiseNewReport2024(3);
        }
        else
        {
            getBalsabhaCLusterWiseNewReport(3);
        }




    }
    protected void BalsabaRawDataLiff_Click2(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "9998";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2024)
        {
            getBalsabhaCLusterWiseNewReport2024(4);
        }



    }
    public void getBalsabhaCLusterWiseRawData(Int32 Flag)
    {
        conditions = "";


        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }

        string Con = "";
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');

        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            Con += " and tblActivityUpdate_School.ActivityDate between ('" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + "') ";
        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            Int32 ih = 0;
            Int32 iK = 0;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                iK = Convert.ToInt32(Year1[1]);
                ih = Convert.ToInt32(Year1[1]);
            }
            else
            {
                iK = Convert.ToInt32(Year1[0]);
                ih = Convert.ToInt32(Year1[0]); ;
            }

            int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                ih = 2019;
                mMonth = 12;
            }

            string fDate = "";
            string tate = "";
            DateTime trmDate;
            DateTime frmDate;
            fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
            frmDate = Convert.ToDateTime(fDate);


            tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
            trmDate = Convert.ToDateTime(tate);

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
                trmDate = Convert.ToDateTime(tate);
            }

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
            {

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
                frmDate = Convert.ToDateTime(fDate);

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
                frmDate = Convert.ToDateTime(fDate);
            }

            Con += " and tblActivityUpdate_School.ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";

        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 3)
        {
            string fDate = "";
            string tate = "";
            DateTime trmDate;
            DateTime frmDate;
            Int32 ih = 0;
            Int32 iK = 0;


            if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                iK = Convert.ToInt32(Year1[1]);
            }
            else
            {
                iK = Convert.ToInt32(Year1[0]);
            }

            if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlToMonth.SelectedValue) == 2 || Convert.ToInt32(ddlToMonth.SelectedValue) == 3)
            {
                ih = Convert.ToInt32(Year1[1]);
            }
            else
            {
                ih = Convert.ToInt32(Year1[0]);
            }
            int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                ih = 2019;
                mMonth = 12;
            }

            fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
            frmDate = Convert.ToDateTime(fDate);

            tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlToMonth.SelectedValue)) + "" + "-" + "25";
            trmDate = Convert.ToDateTime(tate);

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
                trmDate = Convert.ToDateTime(tate);
            }

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
            {

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
                frmDate = Convert.ToDateTime(fDate);

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
                frmDate = Convert.ToDateTime(fDate);
            }
            Con += " and tblActivityUpdate_School.ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";


        }

        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }

        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }

        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
        {
            conditions += "  and UserEntry=3  and ApproveStatus='FC' ";
        }
        else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
        {
            conditions += "  and UserEntry=3  and ApproveStatus='B' ";
        }
        else
        {
            conditions += "  and UserEntry=3  and ApproveStatus='I' ";
        }

        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@schoolCode",conditions +Con),
            new SqlParameter("@Flag",Flag),

        };
        DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptBalsaba", cmdParameters);

        ViewState["dt"] = dataTable;
        if (dataTable.Rows.Count > 0)
        {
            if (dataTable.Rows.Count > 1000)
            {
                ExportToCSVFile(dataTable, "BalsabhaRawData");
            }
            else
            {
                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();
            }

            return;
        }
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();
    }

    public void getBalsabhaCLusterWiseNewReportLiffSummry(Int32 Flag)
    {
        conditions = "";
        string cons = "";
        string conL = "";
        string conB = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }

        string Con = "";
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');


        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + " and   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }

        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }

        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
        {
            conL += "   and ApproveStatus='FC' ";
        }
        else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
        {
            conL += "   and ApproveStatus='B' ";
        }
        else
        {
            conL += "  and ApproveStatus='I' ";
        }

        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
        {
            conB += "   and tblChildAttendanceLifeskill.ApproveStatus='FC' ";
        }
        else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
        {
            conB += "   and tblChildAttendanceLifeskill.ApproveStatus='B' ";
        }
        else
        {
            conB += "  and tblChildAttendanceLifeskill.ApproveStatus='I' ";
        }
        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
        {
            cons += "  and UserEntry=3   ";
        }
        else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
        {
            cons += "  and UserEntry=3 ";
        }
        else
        {
            cons += "  and UserEntry=3  ";
        }
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions +cons),
            new SqlParameter("@Con1",conditions),
             new SqlParameter("@Con2",conditions),

        };
        DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptBalsbaSummaryData", cmdParameters);

        ViewState["dt"] = dataTable;
        if (dataTable.Rows.Count > 0)
        {
            objMain.ReportDownload("Balsabha Report", "Balsabha- Detail", Convert.ToString(Session["username"]));
            ExportToCSVFile(dataTable, "BalsabhaDetail");

        }
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();


    }


    public void getBalsabhaLSESummry(Int32 Flag)
    {
        conditions = "";
        string cons = "";
        string conL = "";
        string conB = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }

        string Con = "";
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');


        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + " and   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }

        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }

        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
        {
            conL += "   and ApproveStatus='FC' ";
        }
        else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
        {
            conL += "   and ApproveStatus='B' ";
        }
        else
        {
            conL += "  and ApproveStatus='I' ";
        }

        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
        {
            conB += "   and tblChildAttendanceLifeskill.ApproveStatus='FC' ";
        }
        else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
        {
            conB += "   and tblChildAttendanceLifeskill.ApproveStatus='B' ";
        }
        else
        {
            conB += "  and tblChildAttendanceLifeskill.ApproveStatus='I' ";
        }
        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
        {
            cons += "  and UserEntry=3   ";
        }
        else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
        {
            cons += "  and UserEntry=3 ";
        }
        else
        {
            cons += "  and UserEntry=3  ";
        }
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions +cons),
            new SqlParameter("@Con1",conditions),
             new SqlParameter("@Con2",conditions),

        };
        DataSet dataTable = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptBalsbaLSSummaryData", cmdParameters);

        ViewState["dt"] = dataTable;
        if (dataTable.Tables[0].Rows.Count > 0)
        {
            MultipuExeclProcess1();

        }
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();


    }

    public void MultipuExeclProcess1()
    {
        DataSet dtMain1 = ViewState["dt"] as DataSet;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\LSESummary.xlsx");
        var ws = wb.Worksheet(1);
        var ws1 = wb.Worksheet(2);
        var ws2 = wb.Worksheet(3);


        DataTable dt = dtMain1.Tables[0];

        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("RowNo");
        ws.Cell(2, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 1;
        string str = "A2:AD" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        DataTable dt1 = dtMain1.Tables[1];



        ws1.Cell(2, 1).InsertData(dt1.Rows);
        Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 1;
        string str1 = "A2:AE" + ii1;
        ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        DataTable dt2 = dtMain1.Tables[2];


        ws2.Cell(2, 1).InsertData(dt2.Rows);
        Int32 ii2 = Convert.ToInt32(dt2.Rows.Count) + 1;
        string str2 = "A2:AA" + ii2;
        ws2.Range(str2).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws2.Range(str2).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws2.Range(str2).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws2.Range(str2).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);




        filepath = StartupPath + "\\LSESummary " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
        wb.SaveAs(filepath);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
        Response.WriteFile(filepath);

        Response.End();
        if (File.Exists(filepath))
        {
            System.IO.File.Delete(filepath);
        }

    }
    public void getBalsabhaCLusterWiseNewReport2024(Int32 Flag)
    {
        conditions = "";


        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }

        string Con = "";
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');


        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + " and   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }

        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }
        if (Flag == 1)
        {
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                conditions += "   and ApproveStatus='FC' ";
            }
            else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                conditions += "   and ApproveStatus='FC' ";
            }
            else
            {
                conditions += "  and ApproveStatus='FC' ";
            }
        }
        if (Flag == 2)
        {
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                conditions += "   and tblChildAttendanceLifeskill.ApproveStatus='FC' ";
            }
            else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                conditions += "   and tblChildAttendanceLifeskill.ApproveStatus='FC' ";
            }
            else
            {
                conditions += "  and tblChildAttendanceLifeskill.ApproveStatus='FC' ";
            }
        }
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions ),
            new SqlParameter("@Flag",Flag),

        };
        DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptBalsabaNew2024", cmdParameters);

        ViewState["dt"] = dataTable;
        if (dataTable.Rows.Count > 0)
        {
            if (Flag == 1)
            {
                objMain.ReportDownload("Balsabha Report", "Balsabha- Child Registration", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, true, "Child Name", "Father Name", "Contact No", "DOB");

                ExportToCSVFile(dataTable, "BalsabhaChildRegistration");
            }
            if (Flag == 2)
            {
                objMain.ReportDownload("Balsabha Report", "LSE Attendance Detail", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, true, "Child Name", "Father Name", "Contact No", "DOB");

                ExportToCSVFile(dataTable, "LSEAttendanceDetail");
            }
            if (Flag == 3)
            {
                objMain.ReportDownload("Balsabha Report", "LSE Attendance Detail", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, true, "Child Name", "Father Name", "Contact No", "DOB");

                ExportToCSVFile(dataTable, "LSEAssessmentDetail");
            }
            if (Flag == 4)
            {
                objMain.ReportDownload("Balsabha Report", "LSE Attendance Detail", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, true, "Child Name", "Father Name", "Contact No", "DOB");

                ExportToCSVFile(dataTable, "LSEAssessmentDetailScore");
            }
        }
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();
    }
    public void getBalsabhaCLusterWiseNewReport(Int32 Flag)
    {
        conditions = "";


        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }

        string Con = "";
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');


        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + " and   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }

        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }
        if (Flag == 1)
        {
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                conditions += "   and ApproveStatus='FC' ";
            }
            else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                conditions += "   and ApproveStatus='FC' ";
            }
            else
            {
                conditions += "  and ApproveStatus='FC' ";
            }
        }
        if (Flag == 2)
        {
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                conditions += "   and tblChildAttendanceLifeskill.ApproveStatus='FC' ";
            }
            else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                conditions += "   and tblChildAttendanceLifeskill.ApproveStatus='FC' ";
            }
            else
            {
                conditions += "  and tblChildAttendanceLifeskill.ApproveStatus='FC' ";
            }
        }
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions ),
            new SqlParameter("@Flag",Flag),
        };
        DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptBalsabaNew", cmdParameters);

        ViewState["dt"] = dataTable;
        if (dataTable.Rows.Count > 0)
        {
            if (Flag == 1)
            {
                objMain.ReportDownload("Balsabha Report", "Balsabha- Child Registration", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, true, "Child Name", "Father Name", "Contact No", "DOB");

                ExportToCSVFile(dataTable, "BalsabhaChildRegistration");
            }
            if (Flag == 2)
            {
                objMain.ReportDownload("Balsabha Report", "LSE Attendance Detail", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, true, "Child Name", "Father Name", "Contact No", "DOB");

                ExportToCSVFile(dataTable, "LSEAttendanceDetail");
            }
            if (Flag == 3)
            {
                objMain.ReportDownload("Balsabha Report", "LSE Attendance Detail", Convert.ToString(Session["username"]));

                dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, true, "Child Name", "Father Name", "Contact No", "DOB");

                ExportToCSVFile(dataTable, "LSEAssessmentDetail");
            }
        }
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();


    }
    public void getBalsabhaCLusterWise(Int32 Flag)
    {
        conditions = "";


        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }

        string Con = "";
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');

        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            Con += " and tblActivityUpdate_School.ActivityDate between ('" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + "') ";
        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            Int32 ih = 0;
            Int32 iK = 0;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                iK = Convert.ToInt32(Year1[1]);
                ih = Convert.ToInt32(Year1[1]);
            }
            else
            {
                iK = Convert.ToInt32(Year1[0]);
                ih = Convert.ToInt32(Year1[0]); ;
            }

            int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                ih = 2019;
                mMonth = 12;
            }

            string fDate = "";
            string tate = "";
            DateTime trmDate;
            DateTime frmDate;
            fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
            frmDate = Convert.ToDateTime(fDate);


            tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
            trmDate = Convert.ToDateTime(tate);

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
                trmDate = Convert.ToDateTime(tate);
            }

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
            {

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
                frmDate = Convert.ToDateTime(fDate);

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
                frmDate = Convert.ToDateTime(fDate);
            }

            Con += " and tblActivityUpdate_School.ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";

        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 3)
        {
            string fDate = "";
            string tate = "";
            DateTime trmDate;
            DateTime frmDate;
            Int32 ih = 0;
            Int32 iK = 0;


            if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                iK = Convert.ToInt32(Year1[1]);
            }
            else
            {
                iK = Convert.ToInt32(Year1[0]);
            }

            if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlToMonth.SelectedValue) == 2 || Convert.ToInt32(ddlToMonth.SelectedValue) == 3)
            {
                ih = Convert.ToInt32(Year1[1]);
            }
            else
            {
                ih = Convert.ToInt32(Year1[0]);
            }
            int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                ih = 2019;
                mMonth = 12;
            }

            fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
            frmDate = Convert.ToDateTime(fDate);

            tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlToMonth.SelectedValue)) + "" + "-" + "25";
            trmDate = Convert.ToDateTime(tate);

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
                trmDate = Convert.ToDateTime(tate);
            }

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
            {

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
                frmDate = Convert.ToDateTime(fDate);

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
                frmDate = Convert.ToDateTime(fDate);
            }
            Con += " and tblActivityUpdate_School.ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";


        }

        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }

        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }

        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
        {
            conditions += "  and UserEntry=3  and ApproveStatus='FC' ";
        }
        else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
        {
            conditions += "  and UserEntry=3 and ApproveStatus='B' ";
        }
        else
        {
            conditions += "  and UserEntry=3  and ApproveStatus='I' ";
        }

        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@schoolCode",conditions +Con),
            new SqlParameter("@Flag",Flag),

        };
        DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptBalsaba", cmdParameters);

        if (Flag == 4)
            dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "User Name");

        ViewState["dt"] = dataTable;
        if (dataTable.Rows.Count > 0)
        {
            if (dataTable.Rows.Count > 1000)
            {
                ExportToCSVFile(dataTable, "BalsabhaClusterSummary");
            }
            else
            {
                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();
            }

            return;
        }
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();
    }
    public void getSACLastMonthReport(Int32 Flag)
    {
        //conditions = "";


        //string ddlDistrict = "";
        //string ddlPhan = "";
        //string ddlVillage = "";
        //string ddlBlock = "";
        //string ddlStatecode = "";
        //foreach (ListItem item in ChkState.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlStatecode += "'" + item.Value + "'" + ",";


        //    }
        //}

        //if (ddlStatecode.Length > 0)
        //{
        //    ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        //}
        //foreach (ListItem item in chkDistrict.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlDistrict += "'" + item.Value + "'" + ",";


        //    }
        //}

        //if (ddlDistrict.Length > 0)
        //{
        //    ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        //}
        //foreach (ListItem item in chkBlock.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlBlock += "'" + item.Value + "'" + ",";


        //    }
        //}

        //if (ddlBlock.Length > 0)
        //{
        //    ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        //}

        //foreach (ListItem item in ddlPanchayat.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlPhan += "'" + item.Value + "'" + ",";


        //    }
        //}

        //if (ddlPhan.Length > 0)
        //{
        //    ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        //}
        //foreach (ListItem item in chkVillage.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlVillage += "'" + item.Value + "'" + ",";


        //    }
        //}
        //if (ddlVillage.Length > 0)
        //{
        //    ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        //}
        //if (ddlYear.SelectedIndex > 0)
        //{
        //    conditions = conditions + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        //}
        //if (ddlStatecode.Length > 0)
        //{
        //    conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        //}
        //if (ddlDistrict.Length > 0)
        //{
        //    conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        //}

        //if (ddlBlock.Length > 0)
        //{
        //    conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        //}
        //if (ddlPhan.Length > 0)
        //{
        //    conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        //}
        //if (ddlVillage.Length > 0)
        //{
        //    conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        //}

        //if (ddlScholl.SelectedIndex > 0)
        //{
        //    conditions += " and mstSchool.SchoolCode ='" + ddlScholl.SelectedValue + "' ";
        //}


        //SqlParameter[] cmdParameters = new SqlParameter[]
        //{
        //    new SqlParameter("@con",conditions),

        //};
        //DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivitySACReportLastMonthAndCurrent]", cmdParameters);

        //ViewState["dt"] = dataTable;
        //if (dataTable.Rows.Count > 0)
        //{
        //    if (dataTable.Rows.Count >1000)
        //    {
        //        ExportToCSVFile(dataTable, "SACcurrentLastmonthStatus");
        //    }
        //    else
        //    {
        //        GV_DynamicGrid2.DataSource = dataTable;
        //        GV_DynamicGrid2.DataBind();
        //    }

        //    return;
        //}
        //GV_DynamicGrid2.DataSource = null;
        //GV_DynamicGrid2.DataBind();

        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";
        string schoolCodeAprove = "";
        GvSip.Visible = true;
        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
        {
            Con += "  and UserEntry=3  and ApproveStatus='FC' ";
            schoolCodeAprove = "   UserEntry=3  and ApproveStatus='FC' ";
        }
        else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
        {
            Con += "  and UserEntry=3  and ApproveStatus='B' ";
            schoolCodeAprove = "   UserEntry=3  and ApproveStatus='B' ";
        }
        else
        {
            Con += "  and UserEntry=3  and ApproveStatus='I' ";
            schoolCodeAprove = "   UserEntry=3  and ApproveStatus='I' ";
        }
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');
        string CreatDate = "" + Year1[0] + "-04-01";
        string CreatDate1 = "" + Year1[1] + "-03-31";

        if (Convert.ToInt32(ddlType.SelectedValue) == 0)
        {
            Con += " and ActivityDate between ('" + CreatDate + "') and ('" + CreatDate1 + "') ";

        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            Con += " and ActivityDate between ('" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + "') ";

        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            Int32 ih = 0;
            Int32 iK = 0;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                iK = Convert.ToInt32(Year1[1]);
                ih = Convert.ToInt32(Year1[1]);
            }
            else
            {
                iK = Convert.ToInt32(Year1[0]);
                ih = Convert.ToInt32(Year1[0]); ;
            }

            int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                ih = 2019;
                mMonth = 12;
            }

            string fDate = "";
            string tate = "";
            DateTime trmDate;
            DateTime frmDate;
            fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
            frmDate = Convert.ToDateTime(fDate);


            tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
            trmDate = Convert.ToDateTime(tate);

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
                trmDate = Convert.ToDateTime(tate);
            }

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
            {

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
                frmDate = Convert.ToDateTime(fDate);

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
                frmDate = Convert.ToDateTime(fDate);
            }

            Con += " and ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";

        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 3)
        {
            string fDate = "";
            string tate = "";
            DateTime trmDate;
            DateTime frmDate;
            Int32 ih = 0;
            Int32 iK = 0;


            if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                iK = Convert.ToInt32(Year1[1]);
            }
            else
            {
                iK = Convert.ToInt32(Year1[0]);
            }

            if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlToMonth.SelectedValue) == 2 || Convert.ToInt32(ddlToMonth.SelectedValue) == 3)
            {
                ih = Convert.ToInt32(Year1[1]);
            }
            else
            {
                ih = Convert.ToInt32(Year1[0]);
            }
            int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                ih = 2019;
                mMonth = 12;
            }

            fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
            frmDate = Convert.ToDateTime(fDate);

            tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlToMonth.SelectedValue)) + "" + "-" + "25";
            trmDate = Convert.ToDateTime(tate);

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
                trmDate = Convert.ToDateTime(tate);
            }

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
            {

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
                frmDate = Convert.ToDateTime(fDate);

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
                frmDate = Convert.ToDateTime(fDate);
            }
            Con += " and ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";


        }


        //if (ddlYear.SelectedIndex > 0)
        //{
        //    conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        //}
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }




        //dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);

        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@schoolCode", conditions1 + Con),
            new SqlParameter("@Con", conditions1),
            new SqlParameter("@schoolCodeAprove", schoolCodeAprove + Con),
            new SqlParameter("@Fyear", ddlYear.SelectedItem.Text),
        };
        dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivitySACReportLastMonthAndCurrent2019]", cmdParameters);
        ViewState["dt"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {
            //DataTable newDataTable = dtMain.Clone();
            //DataTable dtn = dtMain.Clone();
            //for (int i = 0; i < 3; i++)
            //{
            //    dtn.ImportRow(dtMain.Rows[i]);
            //}

            //GvSip.DataSource = dtMain;
            //GvSip.DataBind();
            GvSip.Visible = false;
            string aprove = "";
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                aprove = "FC";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                aprove = "BO";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
            {
                aprove = "IO";
            }
            string n = "SIPlastYearandCurrentStatus" + '_' + aprove;
            ExportToCSVFile(dtMain, n);
            //  GenerateExcelSIP(dtMain, aprove);

        }
        else
        {
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }

    }
    public void getSACReport(Int32 Flag)
    {
        conditions = "";


        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }

        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }

        if (ddlScholl.SelectedIndex > 0)
        {
            conditions += " and mstSchool.SchoolCode ='" + ddlScholl.SelectedValue + "' ";
        }
        string schoolCodeAprove = "";
        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
        {
            conditions += "  and UserEntry=3  and ApproveStatus='FC' ";
            schoolCodeAprove = "   UserEntry=3  and ApproveStatus='FC' ";
        }
        else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
        {
            conditions += "  and UserEntry=3  and ApproveStatus='B' ";
            schoolCodeAprove = "   UserEntry=3  and ApproveStatus='B' ";
        }
        else
        {
            conditions += "  and UserEntry=3  and ApproveStatus='I' ";
            schoolCodeAprove = "   UserEntry=3  and ApproveStatus='I' ";
        }
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
            new SqlParameter("@schoolCodeAprove",schoolCodeAprove),
             new SqlParameter("@Fyear",ddlYear.SelectedItem.Text),


        };


        //  DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivitySACReport]", cmdParameters);
        DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivitySACReport2020]", cmdParameters);

        ViewState["dt"] = dataTable;
        if (dataTable.Rows.Count > 0)
        {
            if (dataTable.Rows.Count > 100)
            {
                ExportToCSVFile(dataTable, "SAC_CurrentStatus");
            }
            else
            {
                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();
            }

            return;
        }
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();


    }
    public void getGKP(Int32 Flag)
    {
        conditions = "";


        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + " where    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }

        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }

        if (ddlScholl.SelectedIndex > 0)
        {
            conditions += " and mstSchool.SchoolCode ='" + ddlScholl.SelectedValue + "' ";
        }

        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');

        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "    And ActivityDate >= '" + Year1[0] + "-04-01' and ActivityDate<='" + Year1[1] + "-03-31'";


        }
        string GKP = "";
        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
        {
            conditions += "  and ApproveStatus='FC' ";
            GKP = "GKPRawDataFCWise";
        }
        else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
        {
            conditions += "   and ApproveStatus='B' ";
            GKP = "GKPRawDataBOWise";
        }
        else
        {
            conditions += "  and ApproveStatus='I' ";
            GKP = "GKPRawDataIOWise";
        }

        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),


        };
        DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptGKPDeatils]", cmdParameters);

        ViewState["dt"] = dataTable;
        if (dataTable.Rows.Count > 0)
        {
            if (dataTable.Rows.Count > 1000)
            {
                ExportToCSVFile(dataTable, GKP);
            }
            else
            {
                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();
            }

            return;
        }
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();


    }
    public void getApproveReport(Int32 Flag)
    {
        conditions = "";


        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + "    mst3Block.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst3Block.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst3Block.DistrictCode in(" + ddlDistrict + ") ";
        }

        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst3Block.BlockCode in(" + ddlBlock + ") ";
        }


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@WhereQuery",conditions),

        };
        DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivityAprroveStaus]", cmdParameters);

        ViewState["dt"] = dataTable;
        if (dataTable.Rows.Count > 0)
        {
            objMain.ReportDownload("Approve Status", "Activity Report", Convert.ToString(Session["username"]));


            GV_DynamicGrid2.DataSource = dataTable;
            GV_DynamicGrid2.DataBind();

            return;
        }
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();


    }
    protected void Social_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "1";
        btnexcel.Visible = true;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        gvQuerltyAnnual.Visible = false;
        getreport(9);

    }
    protected void Family_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "1";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getreport(10);

    }
    protected void Achievemen_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "1";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getreport(11);

    }
    protected void Class_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "1";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getreport(12);

    }
    protected void btn_Life_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string StrFlag = (gvr.FindControl("lblTarOutCome1") as Label).Text;
        if (StrFlag == "School Infrastructure (M) ")
        {
            A1.Text = "";
            A2.Text = "";
            A3.Text = "";
            A4.Text = "";
            A5.Text = "";
            A6.Text = "";
            A7.Text = "";
            A8.Text = "";
            A9.Text = "";

            string ddlDistrict = "";
            string ddlPhan = "";
            string ddlVillage = "";
            string ddlBlock = "";
            string ddlStatecode = "";
            foreach (ListItem item in ChkState.Items)
            {
                if (item.Selected)
                {

                    ddlStatecode += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlStatecode.Length > 0)
            {
                ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
            }
            foreach (ListItem item in chkDistrict.Items)
            {
                if (item.Selected)
                {

                    ddlDistrict += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlDistrict.Length > 0)
            {
                ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
            }
            foreach (ListItem item in chkBlock.Items)
            {
                if (item.Selected)
                {

                    ddlBlock += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlBlock.Length > 0)
            {
                ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
            }

            foreach (ListItem item in ddlPanchayat.Items)
            {
                if (item.Selected)
                {

                    ddlPhan += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlPhan.Length > 0)
            {
                ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
            }
            foreach (ListItem item in chkVillage.Items)
            {
                if (item.Selected)
                {

                    ddlVillage += "'" + item.Value + "'" + ",";


                }
            }
            if (ddlVillage.Length > 0)
            {
                ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
            }
            if (ddlYear.SelectedIndex > 0)
            {
                conditions = conditions + "  where  mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
            }
            if (ddlStatecode.Length > 0)
            {
                conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
            }
            if (ddlDistrict.Length > 0)
            {
                conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
            }
            if (ddlBlock.Length > 0)
            {
                conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
            }
            if (ddlPhan.Length > 0)
            {
                conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
            }
            if (ddlVillage.Length > 0)
            {
                conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
            }
            if (ddlScholl.SelectedIndex > 0)
            {
                conditions += " and mstSchool.SchoolCode ='" + ddlScholl.SelectedValue + "' ";
            }
            Int32 iApp = 0;
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                iApp = 1;
            }
            else
            {
                iApp = 2;
            }
            Int32 iFlag = 0;
            if (ddlScholl.SelectedIndex > 0)
            {
                iFlag = 7;
            }
            else
            {
                iFlag = 5;
            }
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Cond", conditions),
                new SqlParameter("@Flag", iFlag),
                       new SqlParameter("@iApp", iApp),
        };
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptVillageReportCard]", cmdParameters);
            if (dt.Rows.Count > 0)
            {
                A1.Text = dt.Rows[0]["M_DrinkingWater"].ToString();
                A2.Text = dt.Rows[0]["M_GirlsToilet"].ToString();
                A3.Text = dt.Rows[0]["M_Kitchen"].ToString();
                A4.Text = dt.Rows[0]["M_Electricity"].ToString();
                A5.Text = dt.Rows[0]["M_Playground"].ToString();
                A6.Text = dt.Rows[0]["M_Slide"].ToString();
                A7.Text = dt.Rows[0]["M_BoundaryWall"].ToString();
                A8.Text = dt.Rows[0]["M_Books"].ToString();
                A9.Text = dt.Rows[0]["M_CLT_Kit"].ToString();

            }
            MpexdrDistrict1.Show();
        }
        if (StrFlag == "School Infrastructure ")
        {

            A1.Text = "";
            A2.Text = "";
            A3.Text = "";
            A4.Text = "";
            A5.Text = "";
            A6.Text = "";
            A7.Text = "";
            A8.Text = "";
            A9.Text = "";


            string ddlDistrict = "";
            string ddlPhan = "";
            string ddlVillage = "";
            string ddlBlock = "";
            string ddlStatecode = "";
            foreach (ListItem item in ChkState.Items)
            {
                if (item.Selected)
                {

                    ddlStatecode += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlStatecode.Length > 0)
            {
                ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
            }
            foreach (ListItem item in chkDistrict.Items)
            {
                if (item.Selected)
                {

                    ddlDistrict += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlDistrict.Length > 0)
            {
                ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
            }
            foreach (ListItem item in chkBlock.Items)
            {
                if (item.Selected)
                {

                    ddlBlock += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlBlock.Length > 0)
            {
                ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
            }

            foreach (ListItem item in ddlPanchayat.Items)
            {
                if (item.Selected)
                {

                    ddlPhan += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlPhan.Length > 0)
            {
                ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
            }
            foreach (ListItem item in chkVillage.Items)
            {
                if (item.Selected)
                {

                    ddlVillage += "'" + item.Value + "'" + ",";


                }
            }
            if (ddlVillage.Length > 0)
            {
                ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
            }
            if (ddlYear.SelectedIndex > 0)
            {
                conditions = conditions + "  where  mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
            }
            if (ddlStatecode.Length > 0)
            {
                conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
            }
            if (ddlDistrict.Length > 0)
            {
                conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
            }
            if (ddlBlock.Length > 0)
            {
                conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
            }
            if (ddlPhan.Length > 0)
            {
                conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
            }
            if (ddlVillage.Length > 0)
            {
                conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
            }
            if (ddlScholl.SelectedIndex > 0)
            {
                conditions += " and mstSchool.SchoolCode ='" + ddlScholl.SelectedValue + "' ";
            }
            Int32 iApp = 0;

            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                iApp = 1;
            }
            else
            {
                iApp = 2;
            }
            Int32 iFlag = 0;
            if (ddlScholl.SelectedIndex > 0)
            {
                iFlag = 4;
            }
            else
            {
                iFlag = 4;
            }
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Cond", conditions),
                new SqlParameter("@Flag", iFlag),
                       new SqlParameter("@iApp", iApp),
        };
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptVillageReportCard]", cmdParameters);
            if (dt.Rows.Count > 0)
            {
                A1.Text = dt.Rows[0]["Cur_DrinkingWater"].ToString();
                A2.Text = dt.Rows[0]["Cur_GirlsToilet"].ToString();
                A3.Text = dt.Rows[0]["Cur_Kitchen"].ToString();
                A4.Text = dt.Rows[0]["Cur_Electricity"].ToString();
                A5.Text = dt.Rows[0]["Cur_Playground"].ToString();
                A6.Text = dt.Rows[0]["Cur_Slide"].ToString();
                A7.Text = dt.Rows[0]["Cur_BoundaryWall"].ToString();
                A8.Text = dt.Rows[0]["Cur_Books"].ToString();
                A9.Text = dt.Rows[0]["Cur_CLT_Kit"].ToString();

            }
            MpexdrDistrict1.Show();
        }

        if (StrFlag == "Life Skill Game")
        {
            MpexdrDistrict.Show();

            DataTable dt = ViewState["dt"] as DataTable;

            DataRow[] drNew1 = dt.Select("OutCome='Life Skill Game 1'");
            if (drNew1.Length > 0)
            {
                l1.Text = drNew1[0]["AchievementTillDate"].ToString();
            }
            DataRow[] drNew2 = dt.Select("OutCome='Life Skill Game 2'");
            if (drNew2.Length > 0)
            {
                l2.Text = drNew2[0]["AchievementTillDate"].ToString();
            }
            DataRow[] drNew3 = dt.Select("OutCome='Life Skill Game 3'");
            if (drNew3.Length > 0)
            {
                l3.Text = drNew3[0]["AchievementTillDate"].ToString();
            }
            DataRow[] drNew4 = dt.Select("OutCome='Life Skill Game 4'");
            if (drNew4.Length > 0)
            {
                l4.Text = drNew4[0]["AchievementTillDate"].ToString();
            }
            DataRow[] drNew5 = dt.Select("OutCome='Life Skill Game 5'");
            if (drNew5.Length > 0)
            {
                l5.Text = drNew5[0]["AchievementTillDate"].ToString();
            }
        }
    }


    protected void School_Click(object sender, EventArgs e)
    {



        DGV_Report.Visible = true;
        gvWeaklly.Visible = false;
        GV_DynamicGrid2.Visible = false;
        ViewState["Button"] = "12";
        btnexcel.Visible = true;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getreport(2);

        gvQuerltyAnnual.Visible = false;
    }
    protected void ContactReport_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        gvWeaklly.Visible = false;
        GV_DynamicGrid2.Visible = true;
        ViewState["Button"] = "560";
        btnexcel.Visible = true;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getreportContactDeatlis(3);
        gvQuerltyAnnual.Visible = false;
    }
    public void getreportContactDeatlis(Int32 Flag)
    {
        conditions = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }


        string Con = "";
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');

        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            conditions += " and tblDTDMobileActivity.ActivityDate between ('" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + "') ";
        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            Int32 ih = 0;
            Int32 iK = 0;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                iK = Convert.ToInt32(Year1[1]);
                ih = Convert.ToInt32(Year1[1]);
            }
            else
            {
                iK = Convert.ToInt32(Year1[0]);
                ih = Convert.ToInt32(Year1[0]); ;
            }

            int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                ih = 2019;
                mMonth = 12;
            }

            string fDate = "";
            string tate = "";
            DateTime trmDate;
            DateTime frmDate;
            fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
            frmDate = Convert.ToDateTime(fDate);


            tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
            trmDate = Convert.ToDateTime(tate);

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
                trmDate = Convert.ToDateTime(tate);
            }

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
            {

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
                frmDate = Convert.ToDateTime(fDate);

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
                frmDate = Convert.ToDateTime(fDate);
            }

            conditions += " and tblDTDMobileActivity.ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";

        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 3)
        {
            string fDate = "";
            string tate = "";
            DateTime trmDate;
            DateTime frmDate;
            Int32 ih = 0;
            Int32 iK = 0;


            if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                iK = Convert.ToInt32(Year1[1]);
            }
            else
            {
                iK = Convert.ToInt32(Year1[0]);
            }

            if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlToMonth.SelectedValue) == 2 || Convert.ToInt32(ddlToMonth.SelectedValue) == 3)
            {
                ih = Convert.ToInt32(Year1[1]);
            }
            else
            {
                ih = Convert.ToInt32(Year1[0]);
            }
            int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                ih = 2019;
                mMonth = 12;
            }

            fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
            frmDate = Convert.ToDateTime(fDate);

            tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlToMonth.SelectedValue)) + "" + "-" + "25";
            trmDate = Convert.ToDateTime(tate);

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
                trmDate = Convert.ToDateTime(tate);
            }

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
            {

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
                frmDate = Convert.ToDateTime(fDate);

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
                frmDate = Convert.ToDateTime(fDate);
            }
            conditions += " and tblDTDMobileActivity.ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";


        }

        DataTable dataTable = null;
        Int32 Contact = Convert.ToInt32(ddlContact.SelectedValue);
        if (Contact == 1)
        {
            conditions += " and tblDTDMobileActivity.IneligibleID>0";
        }
        if (Contact == 2)
        {
            conditions += " and tblDTDMobileActivity.FollowUPID=2";
        }
        if (Contact == 3)
        {
            conditions += " and tblDTDMobileActivity.ActivityStatus=3";
        }
        if (Contact == 4)
        {

            conditions += " and tblDTDMobileActivity.FollowUPID=1";
        }

        if (Contact == 0)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Condition", conditions),
               new SqlParameter("@Con", ddlYear.SelectedItem.Text),
            new SqlParameter("@FYear", ddlYear.SelectedValue),

        };
            dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptContactMobileTargetD2dDetials]", cmdParameters);
        }
        else
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", conditions),

            new SqlParameter("@Flag", Contact)
        };
            dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportMobileActivityStatusType]", cmdParameters);
        }
        string FileName = "";

        if (Contact == 0)
        {
            FileName = "ContactReport";

        }
        if (Contact == 1)
        {
            FileName = "IneligibleContactStatus";

        }
        if (Contact == 2)
        {
            FileName = "ReadyForEnrolledStatus";

        }
        if (Contact == 3)
        {
            FileName = "EnrolledContactStatus";

        }
        if (Contact == 4)
        {
            FileName = "EnrolledInfoByParentStatus";

        }
        lblTotalCount.Text = dataTable.Rows.Count.ToString();
        ViewState["dt"] = dataTable;
        if (dataTable.Rows.Count > 10)
        {
            ExportToCSVFile(dataTable, FileName);
        }
        else
        {

            GV_DynamicGrid2.DataSource = dataTable;
            GV_DynamicGrid2.DataBind();

            return;
        }
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();




    }

    protected void ActivitySchoolRaw_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        gvWeaklly.Visible = false;
        GV_DynamicGrid2.Visible = true;
        ViewState["Button"] = "556";
        btnexcel.Visible = true;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getreportRowData(1);
        gvQuerltyAnnual.Visible = false;
    }
    protected void ActivitySchoolRaw3_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        gvWeaklly.Visible = false;
        GV_DynamicGrid2.Visible = true;
        ViewState["Button"] = "776";
        btnexcel.Visible = true;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getreportRowData(5);
        gvQuerltyAnnual.Visible = false;
    }
    protected void ActivityVillage_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        gvWeaklly.Visible = false;
        GV_DynamicGrid2.Visible = true;
        ViewState["Button"] = "558";
        btnexcel.Visible = true;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getreportRowData(2);
        gvQuerltyAnnual.Visible = false;
    }
    protected void ActivityVillageGSS_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        gvWeaklly.Visible = false;
        GV_DynamicGrid2.Visible = true;
        ViewState["Button"] = "5588";
        btnexcel.Visible = true;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getreportRowData(6);
        gvQuerltyAnnual.Visible = false;
    }
    protected void ActivityVillageMM_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        gvWeaklly.Visible = false;
        GV_DynamicGrid2.Visible = true;
        ViewState["Button"] = "5589";
        btnexcel.Visible = true;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getreportRowData(7);
        gvQuerltyAnnual.Visible = false;
    }
    protected void ActivityBaselineVillage_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        gvWeaklly.Visible = false;
        GV_DynamicGrid2.Visible = true;
        ViewState["Button"] = "572";
        btnexcel.Visible = true;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getreportRowDataBaseline(4);
        gvQuerltyAnnual.Visible = false;
    }
    protected void Reason_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        gvWeaklly.Visible = false;
        GV_DynamicGrid2.Visible = true;
        ViewState["Button"] = "778";
        btnexcel.Visible = true;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getReasonReasonRowData(2);
        gvQuerltyAnnual.Visible = false;
    }
    protected void SMCe_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        gvWeaklly.Visible = false;
        GV_DynamicGrid2.Visible = true;
        ViewState["Button"] = "559";
        btnexcel.Visible = true;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getreportRowData(3);
        gvQuerltyAnnual.Visible = false;
    }
    protected void SMCeMeeting_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        gvWeaklly.Visible = false;
        GV_DynamicGrid2.Visible = true;
        ViewState["Button"] = "972";
        btnexcel.Visible = true;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getreportRowData(8);
        gvQuerltyAnnual.Visible = false;
    }
    protected void SMCeMeetin99g1_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        gvWeaklly.Visible = false;
        GV_DynamicGrid2.Visible = true;
        ViewState["Button"] = "972";
        btnexcel.Visible = true;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getreportRowData(9);
        gvQuerltyAnnual.Visible = false;
    }
    public void getReasonReasonRowData(Int32 Flag)
    {
        conditions = "";
        string ddlDistrict = "";
        string con = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";

        if (txtDate.Text == "" || txtTodate.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Date')</script>", false);
            return;
        }


        string fromDate = txtDate.Text;

        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        string ToDate = txtTodate.Text;
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
        if (Math.Round(Days) >= 31)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date rang 30 Day')</script>", false);
            return;
        }


        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + "  where  mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }

        con += conditions;

        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
        {
            conditions += " and ActivityDate between('" + afromDate + "') and ('" + aToDate + "') and UserEntry=3  ";
            con += " and ActivityDate between('" + afromDate + "') and ('" + aToDate + "')   ";
        }
        //and ApproveStatus='FC'
        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
        {
            conditions += " and ActivityDate between('" + afromDate + "') and ('" + aToDate + "') and UserEntry=3  ";
            con += " and ActivityDate between('" + afromDate + "') and ('" + aToDate + "')   ";
        }
        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
        {
            conditions += " and ActivityDate between('" + afromDate + "') and '" + aToDate + "'  ";
            con += " and ActivityDate between('" + afromDate + "') and ('" + aToDate + "')  ";
        }
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Condition", conditions),
            new SqlParameter("@con", con),


        };
        DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptReasonEditReport]", cmdParameters);


        if (Flag == 2)
        {
            dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Employee Name");

            lblTotalCount.Text = dataTable.Rows.Count.ToString();
            ViewState["dt"] = dataTable;
            if (dataTable.Rows.Count > 1000)
            {
                ExportToCSVFile(dataTable, "ReasonReport");
            }
            else
            {
                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();

                return;
            }
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }
    }
    public void getreportRowDataBaseline(Int32 Flag)
    {
        conditions = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";

        if (txtDate.Text == "" || txtTodate.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Date')</script>", false);
            return;
        }


        string fromDate = txtDate.Text;

        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        string ToDate = txtTodate.Text;
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
        //if (Math.Round(Days) >= 31)
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date rang 30 Day')</script>", false);
        //    return;
        //}


        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + "  where  mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }



        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
        {
            conditions += " and ActivityDate between('" + afromDate + "') and ('" + aToDate + "')  ";
        }

        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
        {
            conditions += " and ActivityDate between('" + afromDate + "') and ('" + aToDate + "') ";
        }
        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
        {
            conditions += " and ActivityDate between('" + afromDate + "') and '" + aToDate + "'  ";
        }

        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con", conditions),
            new SqlParameter("@Flag", Flag),
            new SqlParameter("@mYear",  ddlYear.SelectedValue),
        };
        DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivitySchoolRawData]", cmdParameters);

        if (Flag == 1)
        {
            dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Name of SMC President", "Classrooms");

            lblTotalCount.Text = dataTable.Rows.Count.ToString();
            ViewState["dt"] = dataTable;
            if (dataTable.Rows.Count > 1000)
            {
                ExportToCSVFile(dataTable, "SchoolActivityRawData");
            }
            else
            {
                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();
                return;
            }
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }

        if (Flag == 2)
        {
            dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Muhalla GSS", "Muhalla MM");

            lblTotalCount.Text = dataTable.Rows.Count.ToString();
            ViewState["dt"] = dataTable;
            if (dataTable.Rows.Count > 1000)
            {
                ExportToCSVFile(dataTable, "VillageActivityRawData");
            }
            else
            {

                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();

                return;
            }
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }

        if (Flag == 4)
        {
            lblTotalCount.Text = dataTable.Rows.Count.ToString();
            ViewState["dt"] = dataTable;
            if (dataTable.Rows.Count > 100)
            {
                ExportToCSVFile(dataTable, "BaselineActivityRawData");
            }
            else
            {
                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();

                return;
            }
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }
    }

    public void getreportRowData(Int32 Flag)
    {
        conditions = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        string afromDate = "";
        string aToDate = "";
        if (txtDate.Text != "" && txtTodate.Text != "")
        {
            string fromDate = txtDate.Text;

            string[] d = fromDate.Split('/');
            afromDate = d[2] + '-' + d[1] + '-' + d[0];

            string ToDate = txtTodate.Text;
            string[] c = ToDate.Split('/');
            aToDate = c[2] + '-' + c[1] + '-' + c[0];


            DateTime d1 = Convert.ToDateTime(afromDate);
            DateTime d2 = Convert.ToDateTime(aToDate);
            int month = Convert.ToInt32(c[1]) - Convert.ToInt32(d[1]);
            TimeSpan t = d2 - d1;
        }




        //double Days = Convert.ToDouble(t.TotalDays);
        //if (Math.Sign(Days) == -1)
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Vaild Date')</script>", false);
        //    return;
        //}
        //if (Math.Round(Days) >= 31)
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date rang 30 Day')</script>", false);
        //    return;
        //}


        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + "  where  mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }


        if (Flag == 8)
        {
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                conditions += " and UserEntry=2  and ApproveStatus='FC' ";
            }

            if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                conditions += " and UserEntry=3  and ApproveStatus='B' ";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
            {
                conditions += " and  UserEntry=3  and ApproveStatus='I' ";
            }
        }
        else
        {
            if (txtDate.Text != "" && txtTodate.Text != "")
            {
                if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                {
                    conditions += " and ActivityDate between('" + afromDate + "') and ('" + aToDate + "') and UserEntry=2  and ApproveStatus='FC' ";
                }

                if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                {
                    conditions += " and ActivityDate between('" + afromDate + "') and ('" + aToDate + "') and UserEntry=3  and ApproveStatus='B' ";
                }
                if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                {
                    conditions += " and ActivityDate between('" + afromDate + "') and '" + aToDate + "' and UserEntry=3  and ApproveStatus='I' ";
                }
            }
            else
            {

                if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                {
                    conditions += " and UserEntry=2  and ApproveStatus='FC' ";
                }

                if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                {
                    conditions += "  and UserEntry=3  and ApproveStatus='B' ";
                }
                if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                {
                    conditions += "  and UserEntry=3  and ApproveStatus='I' ";
                }
            }
        }
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con", conditions),
            new SqlParameter("@Flag", Flag),
            new SqlParameter("@mYear",  ddlYear.SelectedValue),
        };
        DataTable dataTable = GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivitySchoolRawData2023]", cmdParameters);

        if (Flag == 1)
        {
            dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Name of SMC President", "Classrooms");

            lblTotalCount.Text = dataTable.Rows.Count.ToString();
            ViewState["dt"] = dataTable;
            if (dataTable.Rows.Count > 0)
            {

                objMain.ReportDownload("Activity-School Raw Data", "Activity Report", Convert.ToString(Session["username"]));

                ExportToCSVFile(dataTable, "SchoolActivityRawData");
            }
            else
            {

                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();

                return;
            }
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }

        if (Flag == 2)
        {
            dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Muhalla GSS", "Muhalla MM");

            lblTotalCount.Text = dataTable.Rows.Count.ToString();
            ViewState["dt"] = dataTable;
            if (dataTable.Rows.Count > 0)
            {
                 ExportToCSVFile(dataTable, "VillageActivityRawData");
                objMain.ReportDownload("Activity-Village Raw Data", "Activity Report", Convert.ToString(Session["username"]));
            }
            else
            {
                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();
                return;
            }
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }

        if (Flag == 3)
        {
            lblTotalCount.Text = dataTable.Rows.Count.ToString();
            ViewState["dt"] = dataTable;
            if (dataTable.Rows.Count > 0)
            {
                ExportToCSVFile(dataTable, "SMCRawData");
            }
            else
            {

                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();

                return;
            }
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }
        if (Flag == 5)
        {
            dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Latitude", "LatLong");

            lblTotalCount.Text = dataTable.Rows.Count.ToString();
            ViewState["dt"] = dataTable;
            if (dataTable.Rows.Count > 0)
            {
                ExportToCSVFile(dataTable, "ScoolContactRawData");
            }
            else
            {

                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();

                return;
            }
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }
        if (Flag == 6)
        {
            dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Muhalla GSS");

            lblTotalCount.Text = dataTable.Rows.Count.ToString();
            ViewState["dt"] = dataTable;
            if (dataTable.Rows.Count > 0)
            {
                objMain.ReportDownload("Activity-Village GSS Raw Data", "Activity Report", Convert.ToString(Session["username"]));

                ExportToCSVFile(dataTable, "VillageActivityGSSRawData");
            }
            else
            {

                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();

                return;
            }
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }
        if (Flag == 7)
        {
            dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Muhalla MM");
            lblTotalCount.Text = dataTable.Rows.Count.ToString();
            ViewState["dt"] = dataTable;
            if (dataTable.Rows.Count > 0)
            {
                objMain.ReportDownload("Activity-Village MM Raw Data", "Activity Report", Convert.ToString(Session["username"]));


                ExportToCSVFile(dataTable, "VillageActivityMMRawData");
            }
            else
            {

                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();

                return;
            }
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }
        if (Flag == 8)
        {
            dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Member Name", "Mobile");

            lblTotalCount.Text = dataTable.Rows.Count.ToString();
            ViewState["dt"] = dataTable;
            if (dataTable.Rows.Count > 0)
            {
                ExportToCSVFile(dataTable, "SMCMeetingRowData");
            }
            else
            {
                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();

                return;
            }
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }
        if (Flag == 9)
        {
            dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Member Name", "Mobile");

            lblTotalCount.Text = dataTable.Rows.Count.ToString();
            ViewState["dt"] = dataTable;
            if (dataTable.Rows.Count > 0)
            {
                ExportToCSVFile(dataTable, "SMCMeetingAttendance");
            }
            else
            {
                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();

                return;
            }
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }

    }
    public static DataTable GetDataTable(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParameters)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection(connString);
        SqlCommand cmd = new SqlCommand();
        try
        {
            PrepareCommand(cmd, conn, cmdType, cmdText, cmdParameters);
            da.SelectCommand = new SqlCommand();
            cmd.CommandTimeout = 0;
            da.SelectCommand = cmd;
            da.Fill(dt);
            return dt;
        }
        catch
        {
            throw;
        }
        finally
        {
            conn.Close();
        }
    }
    /// <param name="cmdParameters"></param>
    private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParameters)
    {
        if (conn.State != ConnectionState.Open)
            conn.Open();
        cmd.Connection = conn;

        cmd.CommandType = cmdType;
        cmd.CommandText = cmdText;
        cmd.CommandTimeout = 0;
        if (cmdParameters != null)
        {
            foreach (SqlParameter param in cmdParameters)
            {
                cmd.Parameters.Add(param);
            }
        }
    }
    protected void SMSIP_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = false;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "890";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        GV_DynamicGrid2.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        LoadSip();
        //if (ddlType.SelectedIndex > 0)
        //{
        //    if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        //    {

        //        LoadSip();

        //    }
        //    if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        //    {
        //        if (ddlMonth.SelectedIndex > 0)
        //        {
        //            LoadSip();
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  month ')</script>", false);
        //        }
        //    }
        //    if (Convert.ToInt32(ddlType.SelectedValue) == 3)
        //    {

        //        if (ddlMonth.SelectedIndex > 0 && ddlToMonth.SelectedIndex > 0)
        //        {
        //            LoadSip();
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select From month and To month')</script>", false);
        //        }
        //    }
        //    if (Convert.ToInt32(ddlType.SelectedValue) == 4)
        //    {
        //        LoadSip();
        //    }


        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Type')</script>", false);
        //}
    }

    protected void SMSIP_Click1(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = false;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "890";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        GV_DynamicGrid2.Visible = false;

        if (ddlType.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlType.SelectedValue) == 1)
            {

                LoadSip1();

            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 2)
            {
                if (ddlMonth.SelectedIndex > 0)
                {
                    LoadSip1();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  month ')</script>", false);
                }
            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 3)
            {

                if (ddlMonth.SelectedIndex > 0 && ddlToMonth.SelectedIndex > 0)
                {
                    LoadSip1();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select From month and To month')</script>", false);
                }
            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 4)
            {
                LoadSip1();
            }


        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Type')</script>", false);
        }
    }
    public void LoadSip1()
    {
        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";
        GvSip.Visible = true;
        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
        {
            Con += "  and UserEntry=3  and ApproveStatus='FC' ";
        }
        else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
        {
            Con += "  and UserEntry=3  and ApproveStatus='B' ";
        }
        else
        {
            Con += "  and UserEntry=3  and ApproveStatus='I' ";
        }

        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            Con += " and ActivityDate between ('" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + "') ";
        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            Int32 ih = 0;
            Int32 iK = 0;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                iK = DateTime.Today.Year;
                ih = DateTime.Today.Year;
            }
            else
            {
                iK = Convert.ToInt32(ddlYear.SelectedValue);
                ih = Convert.ToInt32(ddlYear.SelectedValue);
            }

            int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                ih = 2019;
                mMonth = 12;
            }


            string fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
            DateTime frmDate = Convert.ToDateTime(fDate);

            string tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
            DateTime trmDate = Convert.ToDateTime(tate);


            Con += " and ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";

        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 3)
        {

            Int32 ih = 0;
            Int32 iK = 0;
            if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                iK = DateTime.Today.Year;
            }
            else
            {
                iK = Convert.ToInt32(ddlYear.SelectedValue);
            }

            if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlToMonth.SelectedValue) == 2 || Convert.ToInt32(ddlToMonth.SelectedValue) == 3)
            {
                ih = DateTime.Today.Year;
            }
            else
            {
                ih = Convert.ToInt32(ddlYear.SelectedValue);
            }
            int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                ih = 2019;
                mMonth = 12;
            }

            string fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
            DateTime frmDate = Convert.ToDateTime(fDate);

            string tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlToMonth.SelectedValue)) + "" + "-" + "25";
            DateTime trmDate = Convert.ToDateTime(tate);


            Con += " and ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";


        }


        //if (ddlYear.SelectedIndex > 0)
        //{
        //    conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        //}
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }

        dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);



        ViewState["dt"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {
            DataTable newDataTable = dtMain.Clone();
            DataTable dtn = dtMain.Clone();
            for (int i = 0; i < 3; i++)
            {
                dtn.ImportRow(dtMain.Rows[i]);
            }

            GvSip.DataSource = dtMain;
            GvSip.DataBind();
            GvSip.Visible = false;
            SIPDetailsNew(dtMain, "");
            string aprove = "";
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                aprove = "FC";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                aprove = "BO";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
            {
                aprove = "IO";
            }
            if (dtMain.Rows.Count > 0)
            {
                // ExportToCSVFileApprove(dtMain, "ActivityMonthWise", aprove);
            }
            //else
            //{
            //    GV_DynamicGrid2.DataSource = dtMain;
            //    GV_DynamicGrid2.DataBind();
            //}
        }
        else
        {
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }




    }
    protected void SMSSummary_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "578";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        GV_DynamicGrid2.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        if (ddlType.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlType.SelectedValue) == 1)
            {

                LoadLiffskill();

            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 2)
            {
                if (ddlMonth.SelectedIndex > 0)
                {
                    LoadLiffskill();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  month ')</script>", false);
                }
            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 3)
            {

                if (ddlMonth.SelectedIndex > 0 && ddlToMonth.SelectedIndex > 0)
                {
                    LoadLiffskill();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select From month and To month')</script>", false);
                }
            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 4)
            {
                LoadLiffskill();
            }


        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Type')</script>", false);
        }
    }

    public void LoadSip()
    {
        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";
        string schoolCodeAprove = "";
        GvSip.Visible = true;
        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
        {
            Con += "  and UserEntry=3  and ApproveStatus='FC' ";
            schoolCodeAprove = "   UserEntry=3  and ApproveStatus='FC' ";
        }
        else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
        {
            Con += "  and UserEntry=3  and ApproveStatus='B' ";
            schoolCodeAprove = "   UserEntry=3  and ApproveStatus='B' ";
        }
        else
        {
            Con += "  and UserEntry=3  and ApproveStatus='I' ";
            schoolCodeAprove = "   UserEntry=3  and ApproveStatus='I' ";
        }
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');
        string CreatDate = "" + Year1[0] + "-04-01";
        string CreatDate1 = "" + Year1[1] + "-03-31";

        if (Convert.ToInt32(ddlType.SelectedValue) == 0)
        {
            Con += " and ActivityDate between ('" + CreatDate + "') and ('" + CreatDate1 + "') ";

        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            Con += " and ActivityDate between ('" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + "') ";

        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            Int32 ih = 0;
            Int32 iK = 0;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                iK = Convert.ToInt32(Year1[1]);
                ih = Convert.ToInt32(Year1[1]);
            }
            else
            {
                iK = Convert.ToInt32(Year1[0]);
                ih = Convert.ToInt32(Year1[0]); ;
            }

            int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                ih = 2019;
                mMonth = 12;
            }

            string fDate = "";
            string tate = "";
            DateTime trmDate;
            DateTime frmDate;
            fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
            frmDate = Convert.ToDateTime(fDate);


            tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
            trmDate = Convert.ToDateTime(tate);

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
                trmDate = Convert.ToDateTime(tate);
            }

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
            {

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
                frmDate = Convert.ToDateTime(fDate);

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
                frmDate = Convert.ToDateTime(fDate);
            }

            Con += " and ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";

        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 3)
        {
            string fDate = "";
            string tate = "";
            DateTime trmDate;
            DateTime frmDate;
            Int32 ih = 0;
            Int32 iK = 0;


            if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                iK = Convert.ToInt32(Year1[1]);
            }
            else
            {
                iK = Convert.ToInt32(Year1[0]);
            }

            if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlToMonth.SelectedValue) == 2 || Convert.ToInt32(ddlToMonth.SelectedValue) == 3)
            {
                ih = Convert.ToInt32(Year1[1]);
            }
            else
            {
                ih = Convert.ToInt32(Year1[0]);
            }
            int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                ih = 2019;
                mMonth = 12;
            }

            fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
            frmDate = Convert.ToDateTime(fDate);

            tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlToMonth.SelectedValue)) + "" + "-" + "25";
            trmDate = Convert.ToDateTime(tate);

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
                trmDate = Convert.ToDateTime(tate);
            }

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
            {

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
                frmDate = Convert.ToDateTime(fDate);

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
                frmDate = Convert.ToDateTime(fDate);
            }
            Con += " and ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";


        }


        //if (ddlYear.SelectedIndex > 0)
        //{
        //    conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        //}
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }




        //dtMain = objMain.rptActivitySIPSummaryReport(conditions1 + Con, conditions1);

        SqlParameter[] cmdParameters = new SqlParameter[]
    {

            new SqlParameter("@schoolCode", conditions1 + Con),
        new SqlParameter("@Con", conditions1),
        new SqlParameter("@schoolCodeAprove", schoolCodeAprove + Con),
          new SqlParameter("@Fyear", ddlYear.SelectedItem.Text),

    };
        dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivitySIPSummaryReportNew]", cmdParameters);
        ViewState["dt"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {
            //DataTable newDataTable = dtMain.Clone();
            //DataTable dtn = dtMain.Clone();
            //for (int i = 0; i < 3; i++)
            //{
            //    dtn.ImportRow(dtMain.Rows[i]);
            //}

            //GvSip.DataSource = dtMain;
            //GvSip.DataBind();
            GvSip.Visible = false;
            string aprove = "";
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                aprove = "FC";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                aprove = "BO";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
            {
                aprove = "IO";
            }
            SIPDetailsDaaa(dtMain, aprove);

        }
        else
        {
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }




    }


    public void LoadSMCSummary(int Flag)
    {
        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";
        string schoolCodeAprove = "";
        GvSip.Visible = true;
        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
        {
            Con += "  and UserEntry=3  and ApproveStatus='FC' ";
            schoolCodeAprove = "   UserEntry=3  and ApproveStatus='FC' ";
        }
        else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
        {
            Con += "  and UserEntry=3  and ApproveStatus='B' ";
            schoolCodeAprove = "   UserEntry=3  and ApproveStatus='B' ";
        }
        else
        {
            Con += "  and UserEntry=3  and ApproveStatus='I' ";
            schoolCodeAprove = "   UserEntry=3  and ApproveStatus='I' ";
        }
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');



        string CreatDate = "" + Year1[0] + "-04-01";
        string CreatDate1 = "" + Year1[1] + "-03-31";

        if (Convert.ToInt32(ddlType.SelectedValue) == 0)
        {
            Con += " and ActivityDate between ('" + CreatDate + "') and ('" + CreatDate1 + "') ";

        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            Con += " and ActivityDate between ('" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + "') ";

        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            Int32 ih = 0;
            Int32 iK = 0;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                iK = Convert.ToInt32(Year1[1]);
                ih = Convert.ToInt32(Year1[1]);
            }
            else
            {
                iK = Convert.ToInt32(Year1[0]);
                ih = Convert.ToInt32(Year1[0]); ;
            }

            int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                ih = 2019;
                mMonth = 12;
            }

            string fDate = "";
            string tate = "";
            DateTime trmDate;
            DateTime frmDate;
            fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
            frmDate = Convert.ToDateTime(fDate);


            tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
            trmDate = Convert.ToDateTime(tate);

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
                trmDate = Convert.ToDateTime(tate);
            }

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
            {

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
                frmDate = Convert.ToDateTime(fDate);

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
                frmDate = Convert.ToDateTime(fDate);
            }

            Con += " and ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";

        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 3)
        {
            string fDate = "";
            string tate = "";
            DateTime trmDate;
            DateTime frmDate;
            Int32 ih = 0;
            Int32 iK = 0;


            if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                iK = Convert.ToInt32(Year1[1]);
            }
            else
            {
                iK = Convert.ToInt32(Year1[0]);
            }

            if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlToMonth.SelectedValue) == 2 || Convert.ToInt32(ddlToMonth.SelectedValue) == 3)
            {
                ih = Convert.ToInt32(Year1[1]);
            }
            else
            {
                ih = Convert.ToInt32(Year1[0]);
            }
            int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                ih = 2019;
                mMonth = 12;
            }

            fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
            frmDate = Convert.ToDateTime(fDate);

            tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlToMonth.SelectedValue)) + "" + "-" + "25";
            trmDate = Convert.ToDateTime(tate);

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
                trmDate = Convert.ToDateTime(tate);
            }

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
            {

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
                frmDate = Convert.ToDateTime(fDate);

                fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
                frmDate = Convert.ToDateTime(fDate);
            }
            Con += " and ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";


        }


        //if (ddlYear.SelectedIndex > 0)
        //{
        //    conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        //}
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con", conditions1 + Con),
            new SqlParameter("@Flag", Flag),
        };
        dtMain = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[RptSMCMeetingSummary]", cmdParameters);
        ViewState["dt"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {
            GvSip.Visible = false;
            string aprove = "";
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                aprove = "FC";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                aprove = "BO";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
            {
                aprove = "IO";
            }
            GenerateExcelSAC(dtMain, aprove, Flag);
        }
        else
        {
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }
    }



    public void LoadLiffskill()
    {
        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";
        GV_DynamicGrid2.Visible = true;
        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
        {
            Con += "  and UserEntry=3  and ApproveStatus='FC' ";
        }
        else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
        {
            Con += "  and UserEntry=3  and ApproveStatus='FC' ";
        }
        else
        {
            Con += "  and UserEntry=3  and ApproveStatus='FC' ";
        }

        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            Con += " and ActivityDate between ('" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + "') ";
        }
        //if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        //{
        //    string fDate = (DateTime.Today.Year) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue) - 1) + "" + "-" + "26";
        //    DateTime frmDate = Convert.ToDateTime(fDate);

        //    string tate = (DateTime.Today.Year) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
        //    DateTime trmDate = Convert.ToDateTime(tate);


        //    Con += " and ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";

        //}
        //if (Convert.ToInt32(ddlType.SelectedValue) == 3)
        //{

        //    string fDate = (DateTime.Today.Year) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue) - 1) + "" + "-" + "26";
        //    DateTime frmDate = Convert.ToDateTime(fDate);

        //    string tate = (DateTime.Today.Year) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlToMonth.SelectedValue)) + "" + "-" + "25";
        //    DateTime trmDate = Convert.ToDateTime(tate);


        //    Con += " and ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";


        //}
        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            Int32 ih = 0;
            Int32 iK = 0;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                iK = DateTime.Today.Year;
                ih = DateTime.Today.Year;
            }
            else
            {
                iK = Convert.ToInt32(ddlYear.SelectedValue);
                ih = Convert.ToInt32(ddlYear.SelectedValue);
            }

            int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                ih = 2019;
                mMonth = 12;
            }


            string fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
            DateTime frmDate = Convert.ToDateTime(fDate);

            string tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
            DateTime trmDate = Convert.ToDateTime(tate);


            Con += " and ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";

        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 3)
        {

            Int32 ih = 0;
            Int32 iK = 0;
            if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                iK = DateTime.Today.Year;
            }
            else
            {
                iK = Convert.ToInt32(ddlYear.SelectedValue);
            }

            if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlToMonth.SelectedValue) == 2 || Convert.ToInt32(ddlToMonth.SelectedValue) == 3)
            {
                ih = DateTime.Today.Year;
            }
            else
            {
                ih = Convert.ToInt32(ddlYear.SelectedValue);
            }
            int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                ih = 2019;
                mMonth = 12;
            }

            string fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
            DateTime frmDate = Convert.ToDateTime(fDate);

            string tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlToMonth.SelectedValue)) + "" + "-" + "25";
            DateTime trmDate = Convert.ToDateTime(tate);


            Con += " and ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";
        }

        //if (ddlYear.SelectedIndex > 0)
        //{
        //    conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        //}
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }




        dtMain = objMain.rptActivityLifeSkillReport(conditions1 + Con, conditions1);
        ViewState["dt"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {

            GV_DynamicGrid2.Visible = true;
            GV_DynamicGrid2.DataSource = dtMain;
            GV_DynamicGrid2.DataBind();


            string aprove = "";
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                aprove = "FC";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                aprove = "BO";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
            {
                aprove = "IO";
            }
            if (dtMain.Rows.Count > 0)
            {
                // ExportToCSVFileApprove(dtMain, "ActivityMonthWise", aprove);
            }
            //else
            //{
            //    GV_DynamicGrid2.DataSource = dtMain;
            //    GV_DynamicGrid2.DataBind();
            //}
        }
        else
        {
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }




    }


    protected void UpdateSchool_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = false;
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();
        gvWeaklly.Visible = true;
        ViewState["Button"] = "3";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        if (ddlType.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlType.SelectedValue) == 1)
            {
                ViewState["Button"] = "4";
                LoadSchoolProfile();
                gvWeaklly.Visible = false;
            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 2)
            {

                if (ddlMonth.SelectedIndex > 0)
                {
                    ViewState["Button"] = "3";
                    gvWeaklly.Visible = true;
                    LoadWeaklly();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  month ')</script>", false);
                }
            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 3)
            {

                if (ddlMonth.SelectedIndex > 0 && ddlToMonth.SelectedIndex > 0)
                {
                    ViewState["Button"] = "11";
                    gvWeaklly.Visible = false;
                    GV_DynamicGrid2.Visible = true;
                    LoadMonthly();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select From month and To month')</script>", false);
                }
            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 4)
            {
                ViewState["Button"] = "11";
                gvWeaklly.Visible = false;
                GV_DynamicGrid2.Visible = true;
                LoadQuter();
            }


        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Type')</script>", false);
        }
    }
    protected void DGV_Report_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTarOutCome = (Label)e.Row.FindControl("lblTarOutCome1");
            LinkButton LinkButton3 = (LinkButton)e.Row.FindControl("LinkButton4");

            if (lblTarOutCome.Text == "Life Skill Game")
            {
                lblTarOutCome.Visible = false;
                LinkButton3.Visible = true;
            }
            else if (lblTarOutCome.Text == "School Infrastructure (M) " || lblTarOutCome.Text == "School Infrastructure ")
            {
                lblTarOutCome.Visible = false;
                LinkButton3.Visible = true;
            }
            else
            {
                lblTarOutCome.Visible = true;
                LinkButton3.Visible = false;
            }
        }

    }

    protected void gvQuerltyAnnual_Report_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblAnnual = (Label)e.Row.FindControl("lblAnnual");
            TextBox txtAnnual = (TextBox)e.Row.FindControl("txtAnnual");

            Label lblQ2 = (Label)e.Row.FindControl("lblQ2");
            TextBox txtQ2 = (TextBox)e.Row.FindControl("txtQ2");


            Label lblQ3 = (Label)e.Row.FindControl("lblQ3");
            TextBox txtQ3 = (TextBox)e.Row.FindControl("txtQ3");

            Label lblQ4 = (Label)e.Row.FindControl("lblQ4");
            TextBox txtQ4 = (TextBox)e.Row.FindControl("txtQ4");

            if (lblAnnual.Text == "0")
            {
                txtAnnual.BackColor = Color.White;
            }
            if (lblAnnual.Text == "1")
            {
                txtAnnual.BackColor = Color.Green;
            }
            if (lblAnnual.Text == "2")
            {
                txtAnnual.BackColor = Color.Orange;
            }
            if (lblAnnual.Text == "3")
            {
                txtAnnual.BackColor = Color.Red;
            }
            if (lblAnnual.Text == "4")
            {
                txtAnnual.BackColor = Color.Blue;
            }



            if (lblQ2.Text == "0")
            {
                txtQ2.BackColor = Color.White;
            }
            if (lblQ2.Text == "1")
            {
                txtQ2.BackColor = Color.Green;
            }
            if (lblQ2.Text == "2")
            {
                txtQ2.BackColor = Color.Orange;
            }
            if (lblQ2.Text == "3")
            {
                txtQ2.BackColor = Color.Red;
            }
            if (lblQ2.Text == "4")
            {
                txtQ2.BackColor = Color.Blue;
            }



            if (lblQ3.Text == "0")
            {
                txtQ3.BackColor = Color.White;
            }
            if (lblQ3.Text == "1")
            {
                txtQ3.BackColor = Color.Green;
            }
            if (lblQ3.Text == "2")
            {
                txtQ3.BackColor = Color.Orange;
            }
            if (lblQ3.Text == "3")
            {
                txtQ3.BackColor = Color.Red;
            }
            if (lblQ3.Text == "4")
            {
                txtQ3.BackColor = Color.Blue;
            }


            if (lblQ4.Text == "0")
            {
                lblQ4.BackColor = Color.White;
            }
            if (lblQ4.Text == "1")
            {
                txtQ4.BackColor = Color.Green;
            }
            if (lblQ4.Text == "2")
            {
                txtQ4.BackColor = Color.Orange;
            }
            if (lblQ4.Text == "3")
            {
                txtQ4.BackColor = Color.Red;
            }
            if (lblQ4.Text == "4")
            {
                txtQ4.BackColor = Color.Blue;
            }
        }

    }
    protected void grdSearchResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTarOutCome = (Label)e.Row.FindControl("lblTarOutCome");
            LinkButton LinkButton3 = (LinkButton)e.Row.FindControl("LinkButton3");

            if (lblTarOutCome.Text == "Life Skill Game")
            {
                lblTarOutCome.Visible = false;
                LinkButton3.Visible = true;
            }


            else
            {
                lblTarOutCome.Visible = true;
                LinkButton3.Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (i == 12)
                {
                    e.Row.Cells[i].Text = H1;
                }
                if (i == 13)
                {
                    e.Row.Cells[i].Text = H2;
                }
                if (i == 14)
                {
                    e.Row.Cells[i].Text = H3;
                }
                if (i == 15)
                {
                    e.Row.Cells[i].Text = H4;
                }
                if (i == 16)
                {
                    e.Row.Cells[i].Text = H5;
                }
            }
        }
    }
    public void LoadWeaklly()
    {
        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 year = 0;

        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');

        if (Convert.ToInt32(ddlMonth.SelectedValue) > 3)
        {
            year = Convert.ToInt32(Year1[0]);
        }
        else
        {
            year = Convert.ToInt32(Year1[1]);
        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            #region Month From to TO month
            string con1 = "", con2 = "", con3 = "", con4 = "", con5 = "";

            string ddlDistrict = "";
            string ddlPhan = "";
            string ddlVillage = "";
            string ddlBlock = "";
            string ddlStatecode = "";
            foreach (ListItem item in ChkState.Items)
            {
                if (item.Selected)
                {

                    ddlStatecode += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlStatecode.Length > 0)
            {
                ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
            }
            foreach (ListItem item in chkDistrict.Items)
            {
                if (item.Selected)
                {

                    ddlDistrict += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlDistrict.Length > 0)
            {
                ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
            }
            foreach (ListItem item in chkBlock.Items)
            {
                if (item.Selected)
                {

                    ddlBlock += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlBlock.Length > 0)
            {
                ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
            }

            foreach (ListItem item in ddlPanchayat.Items)
            {
                if (item.Selected)
                {

                    ddlPhan += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlPhan.Length > 0)
            {
                ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
            }
            foreach (ListItem item in chkVillage.Items)
            {
                if (item.Selected)
                {

                    ddlVillage += "'" + item.Value + "'" + ",";


                }
            }
            if (ddlVillage.Length > 0)
            {
                ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
            }
            if (ddlYear.SelectedIndex > 0)
            {
                conditions1 = conditions1 + "   mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
            }
            if (ddlStatecode.Length > 0)
            {
                conditions1 = conditions1 + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
            }
            if (ddlDistrict.Length > 0)
            {
                conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
            }
            if (ddlBlock.Length > 0)
            {
                conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
            }
            if (ddlPhan.Length > 0)
            {
                conditions1 += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
            }
            if (ddlVillage.Length > 0)
            {
                conditions1 += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                conditions += "  and UserEntry=3  and ApproveStatus='FC' ";
            }
            else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                conditions += "  and UserEntry=3  and ApproveStatus='B' ";
            }
            else
            {
                conditions += "  and UserEntry=3  and ApproveStatus='I' ";
            }
            //  dtMain = objMain.rptActivityUpdateReportsMonthly( conditions1  + conditions);
            #endregion
            #region weakFilter
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
            {
                //Int32 year = DateTime.Today.Year ;
                string fDate = (year - 1).ToString() + "-" + "12" + "-" + "26";
                DateTime frmDate = Convert.ToDateTime(fDate);
                DateTime Q1 = Convert.ToDateTime(frmDate).AddDays(7);
                DateTime Q2 = Convert.ToDateTime(Q1).AddDays(7);
                DateTime Q3 = Convert.ToDateTime(Q2).AddDays(7);
                DateTime Q4 = Convert.ToDateTime(Q3).AddDays(7);



                string fDate4 = (year.ToString()) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
                DateTime Q5 = Convert.ToDateTime(fDate4);


                con1 = "  ActivityDate between('" + frmDate.ToString("yyyy-MM-dd") + "') and '" + Q1.ToString("yyyy-MM-dd") + "' ";
                con2 = "  ActivityDate between('" + Q1.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q2.ToString("yyyy-MM-dd") + "' ";
                con3 = " ActivityDate between('" + Q2.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q3.ToString("yyyy-MM-dd") + "' ";
                con4 = " ActivityDate between('" + Q3.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q4.ToString("yyyy-MM-dd") + "' ";
                con5 = " ActivityDate between('" + Q4.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q5.ToString("yyyy-MM-dd") + "' ";


                H1 = frmDate.ToString("ddMMMyyyy") + " To " + Q1.ToString("ddMMMyyyy");
                H2 = Q1.AddDays(1).ToString("ddMMMyyyy") + " To " + Q2.ToString("ddMMMyyyy");
                H3 = Q2.AddDays(1).ToString("ddMMMyyyy") + " To " + Q3.ToString("ddMMMyyyy");
                H4 = Q3.AddDays(1).ToString("ddMMMyyyy") + " To " + Q4.ToString("ddMMMyyyy");
                H5 = Q4.AddDays(1).ToString("ddMMMyyyy") + " To " + Q5.ToString("ddMMMyyyy");




            }
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 2)
            {
                //Int32 year = DateTime.Today.Year;
                string fDate = (year.ToString()) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue) - 1) + "" + "-" + "26";
                DateTime frmDate = Convert.ToDateTime(fDate);
                DateTime Q1 = Convert.ToDateTime(frmDate).AddDays(7);
                DateTime Q2 = Convert.ToDateTime(Q1).AddDays(7);
                DateTime Q3 = Convert.ToDateTime(Q2).AddDays(7);
                DateTime Q4 = Convert.ToDateTime(Q3).AddDays(7);





                string fDate4 = (DateTime.Today.Year) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
                DateTime Q5 = Convert.ToDateTime(fDate4);


                con1 = "  ActivityDate between('" + frmDate.ToString("yyyy-MM-dd") + "') and '" + Q1.ToString("yyyy-MM-dd") + "' ";
                con2 = "  ActivityDate between('" + Q1.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q2.ToString("yyyy-MM-dd") + "' ";
                con3 = " ActivityDate between('" + Q2.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q3.ToString("yyyy-MM-dd") + "' ";
                con4 = " ActivityDate between('" + Q3.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q4.ToString("yyyy-MM-dd") + "' ";
                con5 = " ActivityDate between('" + Q4.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q5.ToString("yyyy-MM-dd") + "' ";


                H1 = frmDate.ToString("ddMMMyyyy") + " To " + Q1.ToString("ddMMMyyyy");
                H2 = Q1.AddDays(1).ToString("ddMMMyyyy") + " To " + Q2.ToString("ddMMMyyyy");
                H3 = Q2.AddDays(1).ToString("ddMMMyyyy") + " To " + Q3.ToString("ddMMMyyyy");
                H4 = Q3.AddDays(1).ToString("ddMMMyyyy") + " To " + Q4.ToString("ddMMMyyyy");
                H5 = Q4.AddDays(1).ToString("ddMMMyyyy") + " To " + Q5.ToString("ddMMMyyyy");

            }
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                //Int32 year = DateTime.Today.Year;
                string fDate = (year.ToString()) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue) - 1) + "" + "-" + "26";
                DateTime frmDate = Convert.ToDateTime(fDate);
                DateTime Q1 = Convert.ToDateTime(frmDate).AddDays(7);
                DateTime Q2 = Convert.ToDateTime(Q1).AddDays(7);
                DateTime Q3 = Convert.ToDateTime(Q2).AddDays(7);
                DateTime Q4 = Convert.ToDateTime(Q3).AddDays(7);




                string fDate4 = (year.ToString()) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "30";
                DateTime Q5 = Convert.ToDateTime(fDate4);


                con1 = "  ActivityDate between('" + frmDate.ToString("yyyy-MM-dd") + "') and '" + Q1.ToString("yyyy-MM-dd") + "' ";
                con2 = "  ActivityDate between('" + Q1.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q2.ToString("yyyy-MM-dd") + "' ";
                con3 = " ActivityDate between('" + Q2.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q3.ToString("yyyy-MM-dd") + "' ";
                con4 = " ActivityDate between('" + Q3.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q4.ToString("yyyy-MM-dd") + "' ";
                con5 = " ActivityDate between('" + Q4.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q5.ToString("yyyy-MM-dd") + "' ";


                H1 = frmDate.ToString("ddMMMyyyy") + " To " + Q1.ToString("ddMMMyyyy");
                H2 = Q1.AddDays(1).ToString("ddMMMyyyy") + " To " + Q2.ToString("ddMMMyyyy");
                H3 = Q2.AddDays(1).ToString("ddMMMyyyy") + " To " + Q3.ToString("ddMMMyyyy");
                H4 = Q3.AddDays(1).ToString("ddMMMyyyy") + " To " + Q4.ToString("ddMMMyyyy");
                H5 = Q4.AddDays(1).ToString("ddMMMyyyy") + " To " + Q5.ToString("ddMMMyyyy");

            }
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
            {
                //Int32 year = DateTime.Today.Year;
                string fDate = year.ToString() + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
                DateTime frmDate = Convert.ToDateTime(fDate);
                DateTime Q1 = Convert.ToDateTime(frmDate).AddDays(6);
                DateTime Q2 = Convert.ToDateTime(Q1).AddDays(7);
                DateTime Q3 = Convert.ToDateTime(Q2).AddDays(7);
                //DateTime Q4 = Convert.ToDateTime(Q3).AddDays(7);



                string fDate4 = year + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
                DateTime Q4 = Convert.ToDateTime(fDate4);


                con1 = "  ActivityDate between('" + frmDate.ToString("yyyy-MM-dd") + "') and '" + Q1.ToString("yyyy-MM-dd") + "' ";
                con2 = "  ActivityDate between('" + Q1.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q2.ToString("yyyy-MM-dd") + "' ";
                con3 = " ActivityDate between('" + Q2.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q3.ToString("yyyy-MM-dd") + "' ";
                con4 = " ActivityDate between('" + Q3.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q4.ToString("yyyy-MM-dd") + "' ";
                //   con5 = " ActivityDate between('" + Q4.ToString("yyyy-MM-dd") + "') and '" + Q5.ToString("yyyy-MM-dd") + "' ";


                H1 = frmDate.ToString("ddMMMyyyy") + " To " + Q1.ToString("ddMMMyyyy");
                H2 = Q1.AddDays(1).ToString("ddMMMyyyy") + " To " + Q2.ToString("ddMMMyyyy");
                H3 = Q2.AddDays(1).ToString("ddMMMyyyy") + " To " + Q3.ToString("ddMMMyyyy");
                H4 = Q3.AddDays(1).ToString("ddMMMyyyy") + " To " + Q4.ToString("ddMMMyyyy");
                //  H5 = Q4.ToString("ddMMMyyyy") + " To " + Q5.ToString("ddMMMyyyy");
                Icount = 4;



            }
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 5)
            {
                //Int32 year = DateTime.Today.Year;
                string fDate = year.ToString() + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue) - 1) + "" + "-" + "26";
                DateTime frmDate = Convert.ToDateTime(fDate);
                DateTime Q1 = Convert.ToDateTime(frmDate).AddDays(6);
                DateTime Q2 = Convert.ToDateTime(Q1).AddDays(7);
                DateTime Q3 = Convert.ToDateTime(Q2).AddDays(7);
                DateTime Q4 = Convert.ToDateTime(Q3).AddDays(7);


                string fDate4 = year + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
                DateTime Q5 = Convert.ToDateTime(fDate4);


                con1 = "  ActivityDate between('" + frmDate.ToString("yyyy-MM-dd") + "') and '" + Q1.ToString("yyyy-MM-dd") + "' ";
                con2 = "  ActivityDate between('" + Q1.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q2.ToString("yyyy-MM-dd") + "' ";
                con3 = " ActivityDate between('" + Q2.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q3.ToString("yyyy-MM-dd") + "' ";
                con4 = " ActivityDate between('" + Q3.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q4.ToString("yyyy-MM-dd") + "' ";
                con5 = " ActivityDate between('" + Q4.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q5.ToString("yyyy-MM-dd") + "' ";


                H1 = frmDate.ToString("ddMMMyyyy") + " To " + Q1.ToString("ddMMMyyyy");
                H2 = Q1.AddDays(1).ToString("ddMMMyyyy") + " To " + Q2.ToString("ddMMMyyyy");
                H3 = Q2.AddDays(1).ToString("ddMMMyyyy") + " To " + Q3.ToString("ddMMMyyyy");
                H4 = Q3.AddDays(1).ToString("ddMMMyyyy") + " To " + Q4.ToString("ddMMMyyyy");
                H5 = Q4.AddDays(1).ToString("ddMMMyyyy") + " To " + Q5.ToString("ddMMMyyyy");

            }
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 6)
            {
                //Int32 year = DateTime.Today.Year;
                string fDate = year.ToString() + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue) - 1) + "" + "-" + "26";
                DateTime frmDate = Convert.ToDateTime(fDate);
                DateTime Q1 = Convert.ToDateTime(frmDate).AddDays(6);
                DateTime Q2 = Convert.ToDateTime(Q1).AddDays(7);
                DateTime Q3 = Convert.ToDateTime(Q2).AddDays(7);
                DateTime Q4 = Convert.ToDateTime(Q3).AddDays(7);


                string fDate4 = year + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
                DateTime Q5 = Convert.ToDateTime(fDate4);


                con1 = "  ActivityDate between('" + frmDate.ToString("yyyy-MM-dd") + "') and '" + Q1.ToString("yyyy-MM-dd") + "' ";
                con2 = "  ActivityDate between('" + Q1.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q2.ToString("yyyy-MM-dd") + "' ";
                con3 = " ActivityDate between('" + Q2.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q3.ToString("yyyy-MM-dd") + "' ";
                con4 = " ActivityDate between('" + Q3.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q4.ToString("yyyy-MM-dd") + "' ";
                con5 = " ActivityDate between('" + Q4.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q5.ToString("yyyy-MM-dd") + "' ";


                H1 = frmDate.ToString("ddMMMyyyy") + " To " + Q1.ToString("ddMMMyyyy");
                H2 = Q1.AddDays(1).ToString("ddMMMyyyy") + " To " + Q2.ToString("ddMMMyyyy");
                H3 = Q2.AddDays(1).ToString("ddMMMyyyy") + " To " + Q3.ToString("ddMMMyyyy");
                H4 = Q3.AddDays(1).ToString("ddMMMyyyy") + " To " + Q4.ToString("ddMMMyyyy");
                H5 = Q4.AddDays(1).ToString("ddMMMyyyy") + " To " + Q5.ToString("ddMMMyyyy");



            }
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 7)
            {
                //Int32 year = DateTime.Today.Year;
                string fDate = year.ToString() + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue) - 1) + "" + "-" + "26";
                DateTime frmDate = Convert.ToDateTime(fDate);
                DateTime Q1 = Convert.ToDateTime(frmDate).AddDays(6);
                DateTime Q2 = Convert.ToDateTime(Q1).AddDays(7);
                DateTime Q3 = Convert.ToDateTime(Q2).AddDays(7);
                DateTime Q4 = Convert.ToDateTime(Q3).AddDays(7);


                string fDate4 = year + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
                DateTime Q5 = Convert.ToDateTime(fDate4);


                con1 = "  ActivityDate between('" + frmDate.ToString("yyyy-MM-dd") + "') and '" + Q1.ToString("yyyy-MM-dd") + "' ";
                con2 = "  ActivityDate between('" + Q1.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q2.ToString("yyyy-MM-dd") + "' ";
                con3 = " ActivityDate between('" + Q2.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q3.ToString("yyyy-MM-dd") + "' ";
                con4 = " ActivityDate between('" + Q3.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q4.ToString("yyyy-MM-dd") + "' ";
                con5 = " ActivityDate between('" + Q4.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q5.ToString("yyyy-MM-dd") + "' ";



                H1 = frmDate.ToString("ddMMMyyyy") + " To " + Q1.ToString("ddMMMyyyy");
                H2 = Q1.AddDays(1).ToString("ddMMMyyyy") + " To " + Q2.ToString("ddMMMyyyy");
                H3 = Q2.AddDays(1).ToString("ddMMMyyyy") + " To " + Q3.ToString("ddMMMyyyy");
                H4 = Q3.AddDays(1).ToString("ddMMMyyyy") + " To " + Q4.ToString("ddMMMyyyy");
                H5 = Q4.AddDays(1).ToString("ddMMMyyyy") + " To " + Q5.ToString("ddMMMyyyy");



            }
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 8)
            {
                //Int32 year = DateTime.Today.Year;
                string fDate = year.ToString() + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue) - 1) + "" + "-" + "26";
                DateTime frmDate = Convert.ToDateTime(fDate);
                DateTime Q1 = Convert.ToDateTime(frmDate).AddDays(6);
                DateTime Q2 = Convert.ToDateTime(Q1).AddDays(7);
                DateTime Q3 = Convert.ToDateTime(Q2).AddDays(7);
                DateTime Q4 = Convert.ToDateTime(Q3).AddDays(7);


                string fDate4 = year + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
                DateTime Q5 = Convert.ToDateTime(fDate4);

                con1 = "  ActivityDate between('" + frmDate.ToString("yyyy-MM-dd") + "') and '" + Q1.ToString("yyyy-MM-dd") + "' ";
                con2 = "  ActivityDate between('" + Q1.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q2.ToString("yyyy-MM-dd") + "' ";
                con3 = " ActivityDate between('" + Q2.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q3.ToString("yyyy-MM-dd") + "' ";
                con4 = " ActivityDate between('" + Q3.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q4.ToString("yyyy-MM-dd") + "' ";
                con5 = " ActivityDate between('" + Q4.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q5.ToString("yyyy-MM-dd") + "' ";



                H1 = frmDate.ToString("ddMMMyyyy") + " To " + Q1.ToString("ddMMMyyyy");
                H2 = Q1.AddDays(1).ToString("ddMMMyyyy") + " To " + Q2.ToString("ddMMMyyyy");
                H3 = Q2.AddDays(1).ToString("ddMMMyyyy") + " To " + Q3.ToString("ddMMMyyyy");
                H4 = Q3.AddDays(1).ToString("ddMMMyyyy") + " To " + Q4.ToString("ddMMMyyyy");
                H5 = Q4.AddDays(1).ToString("ddMMMyyyy") + " To " + Q5.ToString("ddMMMyyyy");


            }
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 9)
            {
                ////Int32 year = DateTime.Today.Year;
                string fDate = year.ToString() + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue) - 1) + "" + "-" + "26";
                DateTime frmDate = Convert.ToDateTime(fDate);
                DateTime Q1 = Convert.ToDateTime(frmDate).AddDays(6);
                DateTime Q2 = Convert.ToDateTime(Q1).AddDays(7);
                DateTime Q3 = Convert.ToDateTime(Q2).AddDays(7);
                DateTime Q4 = Convert.ToDateTime(Q3).AddDays(7);


                string fDate4 = year + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
                DateTime Q5 = Convert.ToDateTime(fDate4);


                con1 = "  ActivityDate between('" + frmDate.ToString("yyyy-MM-dd") + "') and '" + Q1.ToString("yyyy-MM-dd") + "' ";
                con2 = "  ActivityDate between('" + Q1.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q2.ToString("yyyy-MM-dd") + "' ";
                con3 = " ActivityDate between('" + Q2.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q3.ToString("yyyy-MM-dd") + "' ";
                con4 = " ActivityDate between('" + Q3.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q4.ToString("yyyy-MM-dd") + "' ";
                con5 = " ActivityDate between('" + Q4.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q5.ToString("yyyy-MM-dd") + "' ";



                H1 = frmDate.ToString("ddMMMyyyy") + " To " + Q1.ToString("ddMMMyyyy");
                H2 = Q1.AddDays(1).ToString("ddMMMyyyy") + " To " + Q2.ToString("ddMMMyyyy");
                H3 = Q2.AddDays(1).ToString("ddMMMyyyy") + " To " + Q3.ToString("ddMMMyyyy");
                H4 = Q3.AddDays(1).ToString("ddMMMyyyy") + " To " + Q4.ToString("ddMMMyyyy");
                H5 = Q4.AddDays(1).ToString("ddMMMyyyy") + " To " + Q5.ToString("ddMMMyyyy");


            }
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 10)
            {
                //Int32 year = DateTime.Today.Year;
                string fDate = year.ToString() + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue) - 1) + "" + "-" + "26";
                DateTime frmDate = Convert.ToDateTime(fDate);
                DateTime Q1 = Convert.ToDateTime(frmDate).AddDays(6);
                DateTime Q2 = Convert.ToDateTime(Q1).AddDays(7);
                DateTime Q3 = Convert.ToDateTime(Q2).AddDays(7);
                DateTime Q4 = Convert.ToDateTime(Q3).AddDays(7);


                string fDate4 = year + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
                DateTime Q5 = Convert.ToDateTime(fDate4);


                con1 = "  ActivityDate between('" + frmDate.ToString("yyyy-MM-dd") + "') and '" + Q1.ToString("yyyy-MM-dd") + "' ";
                con2 = "  ActivityDate between('" + Q1.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q2.ToString("yyyy-MM-dd") + "' ";
                con3 = " ActivityDate between('" + Q2.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q3.ToString("yyyy-MM-dd") + "' ";
                con4 = " ActivityDate between('" + Q3.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q4.ToString("yyyy-MM-dd") + "' ";
                con5 = " ActivityDate between('" + Q4.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q5.ToString("yyyy-MM-dd") + "' ";


                H1 = frmDate.ToString("ddMMMyyyy") + " To " + Q1.ToString("ddMMMyyyy");
                H2 = Q1.AddDays(1).ToString("ddMMMyyyy") + " To " + Q2.ToString("ddMMMyyyy");
                H3 = Q2.AddDays(1).ToString("ddMMMyyyy") + " To " + Q3.ToString("ddMMMyyyy");
                H4 = Q3.AddDays(1).ToString("ddMMMyyyy") + " To " + Q4.ToString("ddMMMyyyy");
                H5 = Q4.AddDays(1).ToString("ddMMMyyyy") + " To " + Q5.ToString("ddMMMyyyy");


            }
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 11)
            {
                //Int32 year = DateTime.Today.Year;
                string fDate = year.ToString() + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue) - 1) + "" + "-" + "26";
                DateTime frmDate = Convert.ToDateTime(fDate);
                DateTime Q1 = Convert.ToDateTime(frmDate).AddDays(6);
                DateTime Q2 = Convert.ToDateTime(Q1).AddDays(7);
                DateTime Q3 = Convert.ToDateTime(Q2).AddDays(7);
                DateTime Q4 = Convert.ToDateTime(Q3).AddDays(7);


                string fDate4 = year + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
                DateTime Q5 = Convert.ToDateTime(fDate4);


                con1 = "  ActivityDate between('" + frmDate.ToString("yyyy-MM-dd") + "') and '" + Q1.ToString("yyyy-MM-dd") + "' ";
                con2 = "  ActivityDate between('" + Q1.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q2.ToString("yyyy-MM-dd") + "' ";
                con3 = " ActivityDate between('" + Q2.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q3.ToString("yyyy-MM-dd") + "' ";
                con4 = " ActivityDate between('" + Q3.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q4.ToString("yyyy-MM-dd") + "' ";
                con5 = " ActivityDate between('" + Q4.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q5.ToString("yyyy-MM-dd") + "' ";



                H1 = frmDate.ToString("ddMMMyyyy") + " To " + Q1.ToString("ddMMMyyyy");
                H2 = Q1.AddDays(1).ToString("ddMMMyyyy") + " To " + Q2.ToString("ddMMMyyyy");
                H3 = Q2.AddDays(1).ToString("ddMMMyyyy") + " To " + Q3.ToString("ddMMMyyyy");
                H4 = Q3.AddDays(1).ToString("ddMMMyyyy") + " To " + Q4.ToString("ddMMMyyyy");
                H5 = Q4.AddDays(1).ToString("ddMMMyyyy") + " To " + Q5.ToString("ddMMMyyyy");


            }
            if (Convert.ToInt32(ddlMonth.SelectedValue) == 12)
            {
                //Int32 year = DateTime.Today.Year;
                string fDate = year.ToString() + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue) - 1) + "" + "-" + "26";
                DateTime frmDate = Convert.ToDateTime(fDate);
                DateTime Q1 = Convert.ToDateTime(frmDate).AddDays(6);
                DateTime Q2 = Convert.ToDateTime(Q1).AddDays(7);
                DateTime Q3 = Convert.ToDateTime(Q2).AddDays(7);
                DateTime Q4 = Convert.ToDateTime(Q3).AddDays(7);


                string fDate4 = year + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
                DateTime Q5 = Convert.ToDateTime(fDate4);

                con1 = "  ActivityDate between('" + frmDate.ToString("yyyy-MM-dd") + "') and '" + Q1.ToString("yyyy-MM-dd") + "' ";
                con2 = "  ActivityDate between('" + Q1.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q2.ToString("yyyy-MM-dd") + "' ";
                con3 = " ActivityDate between('" + Q2.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q3.ToString("yyyy-MM-dd") + "' ";
                con4 = " ActivityDate between('" + Q3.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q4.ToString("yyyy-MM-dd") + "' ";
                con5 = " ActivityDate between('" + Q4.AddDays(1).ToString("yyyy-MM-dd") + "') and '" + Q5.ToString("yyyy-MM-dd") + "' ";


                H1 = frmDate.ToString("ddMMMyyyy") + " To " + Q1.ToString("ddMMMyyyy");
                H2 = Q1.AddDays(1).ToString("ddMMMyyyy") + " To " + Q2.ToString("ddMMMyyyy");
                H3 = Q2.AddDays(1).ToString("ddMMMyyyy") + " To " + Q3.ToString("ddMMMyyyy");
                H4 = Q3.AddDays(1).ToString("ddMMMyyyy") + " To " + Q4.ToString("ddMMMyyyy");
                H5 = Q4.AddDays(1).ToString("ddMMMyyyy") + " To " + Q5.ToString("ddMMMyyyy");


            }

            dtMain = rptActivityWeaklyReport(conditions1 + conditions, con1, con2, con3, con4, con5, Icount, Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue));
            ViewState["dt"] = dtMain;
            if (dtMain.Rows.Count > 0)
            {
                if (dtMain.Rows.Count > 100)
                {
                    string aprove = "";
                    if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                    {
                        aprove = "FC";
                    }
                    if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                    {
                        aprove = "BO";
                    }
                    if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                    {
                        aprove = "IO";
                    }
                    //if (dtMain.Rows.Count > 1000)
                    //{

                    if (dtMain.Rows.Count > 100)
                    {
                        ExportToCSVFileNewWeaklly(dtMain, "ActivityWeekWise", aprove);

                        ViewState["dt"] = dtMain;
                    }
                    //  ExportToCSVFileNew(dtMain, "Weekly");
                }
                else
                {

                    gvWeaklly.DataSource = dtMain;
                    gvWeaklly.DataBind();

                    if (Icount == 4)
                    {
                        gvWeaklly.Columns[16].Visible = false;
                    }
                    else
                    {
                        gvWeaklly.Columns[16].Visible = true;
                    }
                }
            }
            else
            {
                gvWeaklly.DataSource = null;
                gvWeaklly.DataBind();
            }

            #endregion
        }
    }
    public DataTable rptActivityWeaklyReport(string WhereQuery, string Q1, string Q2, string Q3, string Q4, string Q5, Int32 flag, Int32 @Month, Int32 mYear)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {

            new SqlParameter("@WhereQuery", WhereQuery),
            new SqlParameter("@Q1", Q1),
            new SqlParameter("@Q2", Q2),
          new SqlParameter("@Q3", Q3),
          new SqlParameter("@Q4", Q4),
          new SqlParameter("@Q5", Q5),
          new SqlParameter("@flag", flag),
           new SqlParameter("@Month", Month),
             new SqlParameter("@mYear", mYear),
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivityWeaklyReportNew]", cmdParameters);
    }
    private void ExportToCSVFileNewWeaklly(DataTable dtTable, string filePath, string Approve)
    {
        StringBuilder sbldr = new StringBuilder();
        if (dtTable != null)
        {
            if (dtTable.Columns.Count != 0)
            {
                foreach (DataColumn col in dtTable.Columns)
                {
                    if (col.ColumnName == "Q1")
                    {
                        sbldr.Append(H1 + ',');
                    }
                    else if (col.ColumnName == "Q2")
                    {
                        sbldr.Append(H2 + ',');
                    }
                    else if (col.ColumnName == "Q3")
                    {
                        sbldr.Append(H3 + ',');
                    }
                    else if (col.ColumnName == "Q4")
                    {
                        sbldr.Append(H4 + ',');
                    }
                    else if (col.ColumnName == "Q5")
                    {
                        sbldr.Append(H5 + ',');
                    }
                    else if (col.ColumnName == "SRNo1")
                    {

                    }
                    else if (col.ColumnName == "Q5")
                    {
                        if (H5.Length > 0)
                        {
                            sbldr.Append(H4 + ',');
                        }
                    }
                    else
                    {
                        sbldr.Append(col.ColumnName + ',');
                    }

                }
                sbldr.Append("\r\n");
                foreach (DataRow row in dtTable.Rows)
                {
                    foreach (DataColumn column in dtTable.Columns)
                    {
                        if (column.ColumnName == "Q1")
                        {
                            sbldr.Append(Convert.ToString(row[column]).Replace(",", "  ").Replace("\r", "").Replace("\n", "") + ',');
                        }
                        else if (column.ColumnName == "Q2")
                        {
                            sbldr.Append(Convert.ToString(row[column]).Replace(",", "  ").Replace("\r", "").Replace("\n", "") + ',');
                        }
                        else if (column.ColumnName == "Q3")
                        {
                            sbldr.Append(Convert.ToString(row[column]).Replace(",", "  ").Replace("\r", "").Replace("\n", "") + ',');
                        }
                        else if (column.ColumnName == "Q4")
                        {
                            sbldr.Append(Convert.ToString(row[column]).Replace(",", "  ").Replace("\r", "").Replace("\n", "") + ',');
                        }
                        else if (column.ColumnName == "SRNo1")
                        {

                        }
                        else if (column.ColumnName == "Q5")
                        {
                            if (H5.Length > 0)
                            {
                                sbldr.Append(Convert.ToString(row[column]).Replace(",", "  ").Replace("\r", "").Replace("\n", "") + ',');
                            }
                        }
                        else
                        {
                            sbldr.Append(Convert.ToString(row[column]).Replace(",", "  ").Replace("\r", "").Replace("\n", "") + ',');
                        }

                    }
                    sbldr.Append("\r\n");

                }
            }
            string sFileDir = Server.MapPath(Comman.GetImagePath("DataBackupPath")); ;
            string Fullfilename = "" + filePath + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + "_" + Approve + ".csv";

            string path = sFileDir + Fullfilename;
            File.WriteAllText(path, sbldr.ToString());

            FileStream fs = null;//, fs2=null;
            try
            {
                string path1 = Fullfilename;
                string foldername = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + path1 + "");
                string datafolder = path1.Substring(0, path1.Length - 4);
                //  string[] file = Directory.GetFiles(foldername);

                string fullPath = Request.MapPath("~/DataBackup/" + datafolder + "" + ".zip");
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(foldername, "");
                    //    zip.AddFiles(file, foldername);
                    zip.Save(Server.MapPath(Comman.GetImagePath("DataBackupPath")) + "/" + datafolder + "" + ".zip");
                }



                HttpResponse Response = HttpContext.Current.Response; Response.Clear(); Response.ClearHeaders(); Response.Charset = "UTF-8";
                fs = File.Open(fullPath, FileMode.Open);
                byte[] bytBytes = new byte[(fs.Length)];
                fs.Read(bytBytes, 0, (int)fs.Length);
                fs.Close();
                Response.AddHeader("Content-disposition", "attachment; filename=" + datafolder + "" + ".zip");
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(bytBytes);






                if (File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                if (File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.End();
            }

            catch (System.Exception ex)
            {
                //  Server.Transfer("default.aspx", false);
                Response.Clear();

                //string mmsg = ex.Message;
                //showEXPMessages("(crateZip)  " + mmsg); //showMessages(mmsg);
            }
            finally
            {
                fs.Dispose();
                Response.Clear();

            }
        }
        //str.Write(sbldr.ToString());
        //Response.ContentType = "Application/x-msexcel";
        //Response.AddHeader("content-disposition", "attachment;filename=test.csv");
        //Response.Write(sbldr.ToString());
        //Response.End();
    }
    private void ExportToCSVFileNew(DataTable dtTable, string filePath)
    {
        StringBuilder sbldr = new StringBuilder();
        if (dtTable != null)
        {
            if (dtTable.Columns.Count != 0)
            {
                foreach (DataColumn col in dtTable.Columns)
                {
                    if (col.ColumnName == "Q1")
                    {
                        sbldr.Append(H1 + ',');
                    }
                    else if (col.ColumnName == "Q2")
                    {
                        sbldr.Append(H2 + ',');
                    }
                    else if (col.ColumnName == "Q3")
                    {
                        sbldr.Append(H3 + ',');
                    }
                    else if (col.ColumnName == "Q4")
                    {
                        sbldr.Append(H4 + ',');
                    }
                    else if (col.ColumnName == "Q5")
                    {
                        sbldr.Append(H5 + ',');
                    }
                    else if (col.ColumnName == "SRNo1")
                    {

                    }
                    else if (col.ColumnName == "Q5")
                    {
                        if (H5.Length > 0)
                        {
                            sbldr.Append(H4 + ',');
                        }
                    }
                    else
                    {
                        sbldr.Append(col.ColumnName + ',');
                    }

                }
                sbldr.Append("\r\n");
                foreach (DataRow row in dtTable.Rows)
                {
                    foreach (DataColumn column in dtTable.Columns)
                    {
                        if (column.ColumnName == "Q1")
                        {
                            sbldr.Append(Convert.ToString(row[column]).Replace(",", "  ").Replace("\r", "").Replace("\n", "") + ',');
                        }
                        else if (column.ColumnName == "Q2")
                        {
                            sbldr.Append(Convert.ToString(row[column]).Replace(",", "  ").Replace("\r", "").Replace("\n", "") + ',');
                        }
                        else if (column.ColumnName == "Q3")
                        {
                            sbldr.Append(Convert.ToString(row[column]).Replace(",", "  ").Replace("\r", "").Replace("\n", "") + ',');
                        }
                        else if (column.ColumnName == "Q4")
                        {
                            sbldr.Append(Convert.ToString(row[column]).Replace(",", "  ").Replace("\r", "").Replace("\n", "") + ',');
                        }
                        else if (column.ColumnName == "SRNo1")
                        {

                        }
                        else if (column.ColumnName == "Q5")
                        {
                            if (H5.Length > 0)
                            {
                                sbldr.Append(Convert.ToString(row[column]).Replace(",", "  ").Replace("\r", "").Replace("\n", "") + ',');
                            }
                        }
                        else
                        {
                            sbldr.Append(Convert.ToString(row[column]).Replace(",", "  ").Replace("\r", "").Replace("\n", "") + ',');
                        }

                    }
                    sbldr.Append("\r\n");

                }
            }
            string sFileDir = Server.MapPath(Comman.GetImagePath("DataBackupPath")); ;
            string Fullfilename = "" + filePath + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".csv";
            string path = sFileDir + Fullfilename;
            File.WriteAllText(path, sbldr.ToString());

            FileStream fs = null;//, fs2=null;
            try
            {
                string path1 = Fullfilename;
                string foldername = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + path1 + "");
                string datafolder = path1.Substring(0, path1.Length - 4);
                //  string[] file = Directory.GetFiles(foldername);

                string fullPath = Request.MapPath("~/DataBackup/" + datafolder + "" + ".zip");
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(foldername, "");
                    //    zip.AddFiles(file, foldername);
                    zip.Save(Server.MapPath(Comman.GetImagePath("DataBackupPath")) + "/" + datafolder + "" + ".zip");
                }



                HttpResponse Response = HttpContext.Current.Response; Response.Clear(); Response.ClearHeaders(); Response.Charset = "UTF-8";
                fs = File.Open(fullPath, FileMode.Open);
                byte[] bytBytes = new byte[(fs.Length)];
                fs.Read(bytBytes, 0, (int)fs.Length);
                fs.Close();
                Response.AddHeader("Content-disposition", "attachment; filename=" + datafolder + "" + ".zip");
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(bytBytes);






                if (File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                if (File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.End();
            }

            catch (System.Exception ex)
            {
                //  Server.Transfer("default.aspx", false);
                Response.Clear();

                //string mmsg = ex.Message;
                //showEXPMessages("(crateZip)  " + mmsg); //showMessages(mmsg);
            }
            finally
            {
                fs.Dispose();
                Response.Clear();

            }
        }
        //str.Write(sbldr.ToString());
        //Response.ContentType = "Application/x-msexcel";
        //Response.AddHeader("content-disposition", "attachment;filename=test.csv");
        //Response.Write(sbldr.ToString());
        //Response.End();
    }
    public void LoadMonthly()
    {
        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 YYearID = 0;
        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');

        if (Convert.ToInt32(ddlMonth.SelectedValue) > 3)
        {
            YYearID = Convert.ToInt32(Year1[0]);
        }
        else
        {
            YYearID = Convert.ToInt32(Year1[1]);
        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 3)
        {

            string GK1 = "", GK2 = "", GK3 = "", GK4 = "";
            string Group1 = "", Group2 = "", Group3 = "", Group4 = "";
            #region Month From to TO month
            string con1 = "", con2 = "", con3 = "", con4 = "", con5 = "";
            //if (ddlYear.SelectedIndex > 0)
            //{
            //    conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
            //}
            string ddlDistrict = "";
            string ddlPhan = "";
            string ddlVillage = "";
            string ddlBlock = "";
            string ddlStatecode = "";
            foreach (ListItem item in ChkState.Items)
            {
                if (item.Selected)
                {

                    ddlStatecode += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlStatecode.Length > 0)
            {
                ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
            }
            foreach (ListItem item in chkDistrict.Items)
            {
                if (item.Selected)
                {

                    ddlDistrict += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlDistrict.Length > 0)
            {
                ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
            }
            foreach (ListItem item in chkBlock.Items)
            {
                if (item.Selected)
                {

                    ddlBlock += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlBlock.Length > 0)
            {
                ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
            }

            foreach (ListItem item in ddlPanchayat.Items)
            {
                if (item.Selected)
                {

                    ddlPhan += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlPhan.Length > 0)
            {
                ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
            }
            foreach (ListItem item in chkVillage.Items)
            {
                if (item.Selected)
                {

                    ddlVillage += "'" + item.Value + "'" + ",";


                }
            }
            if (ddlVillage.Length > 0)
            {
                ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
            }
            if (ddlYear.SelectedIndex > 0)
            {
                conditions1 = conditions1 + "    V.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
            }
            if (ddlStatecode.Length > 0)
            {
                conditions1 = conditions1 + " and     V.StateCode in(" + ddlStatecode + ") ";
            }
            if (ddlDistrict.Length > 0)
            {
                conditions1 = conditions1 + " and V.DistrictCode in(" + ddlDistrict + ") ";
            }
            if (ddlBlock.Length > 0)
            {
                conditions1 = conditions1 + " and  V.BlockCode in(" + ddlBlock + ") ";
            }
            if (ddlPhan.Length > 0)
            {
                conditions1 = conditions1 + " and  V.PanchayatCode in(" + ddlPhan + ") ";
            }
            if (ddlVillage.Length > 0)
            {
                conditions1 = conditions1 + " and  V.VillageCode in(" + ddlVillage + ") ";
            }

            //  dtMain = objMain.rptActivityUpdateReportsMonthly(conditions + conditions1);
            #endregion
            #region weakFilter


            Int32 fromMonth = Convert.ToInt32(ddlMonth.SelectedValue);
            Int32 ToMonth = Convert.ToInt32(ddlToMonth.SelectedValue);
            TotalMonth = ToMonth - fromMonth;

            if (TotalMonth + 1 == 1 || TotalMonth + 1 == 2 || TotalMonth + 1 == 3 || TotalMonth + 1 == 4)
            {


            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Max 4 Month')</script>", false);
                return;
            }
            Int32 iRowCount = 1;
            for (int i = fromMonth; i < ToMonth + 1; i++)
            {
                if (i == 1)
                {

                    string fDate = (YYearID - 1).ToString() + "-" + "12" + "-" + "26";
                    DateTime frmDate = Convert.ToDateTime(fDate);

                    string fDate4 = (DateTime.Today.Year) + "-" + "" + Convert.ToInt32(i) + "" + "-" + "25";
                    DateTime Q5 = Convert.ToDateTime(fDate4);


                    con1 = "  ActivityDate between('" + frmDate.ToString("yyyy-MM-dd") + "') and '" + Q5.ToString("yyyy-MM-dd") + "' ";
                    if (iRowCount == 1)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H1 = "isnull(tv.Jan,0)  as   [Ach-Jan] ";
                            GK1 = "sum(isnull(an.Jan,0)) as  [Target -Jan]";
                            Group1 = " tv.Jan ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H1 = "isnull(tv.JanB,0)  as   [Ach-Jan]";
                            GK1 = "sum(isnull(an.Jan,0)) as  [Target -Jan] ";
                            Group1 = " tv.JanB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H1 = "isnull(tv.JanI,0) as   [Ach-Jan]";
                            GK1 = "sum(isnull(an.Jan,0)) as  [Target -Jan]";
                            Group1 = " tv.JanI ";
                        }
                    }
                    if (iRowCount == 2)
                    {

                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H2 = "isnull(tv.Jan,0)  as   [Ach-Jan] ";
                            GK2 = "sum(isnull(an.Jan,0)) as  [Target -Jan]";
                            Group2 = " tv.Jan ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H2 = "isnull(an.JanB,0)  as   [Ach-Jan]";
                            GK2 = "isnull(tv.Jan,0) as  [Target -Jan] ";
                            Group2 = " tv.JanB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H2 = "isnull(tv.JanI,0) as   [Ach-Jan]";
                            GK2 = "sum(isnull(an.Jan,0)) as  [Target -Jan]";
                            Group2 = " tv.JanI ";
                        }
                    }
                    if (iRowCount == 3)
                    {

                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H3 = "isnull(tv.Jan,0)  as   [Ach-Jan] ";
                            GK3 = "sum(isnull(an.Jan,0)) as  [Target -Jan]";
                            Group3 = " tv.Jan ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H3 = "isnull(tv.JanB,0)  as   [Ach-Jan]";
                            GK3 = "sum(isnull(an.Jan,0)) as  [Target -Jan] ";
                            Group3 = " tv.JanB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H3 = "isnull(tv.JanI,0) as   [Ach-Jan]";
                            GK3 = "sum(isnull(an.Jan,0)) as  [Target -Jan]";
                            Group3 = " tv.JanI ";
                        }
                    }
                    if (iRowCount == 4)
                    {

                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H4 = "isnull(tv.Jan,0)  as   [Ach-Jan] ";
                            GK4 = "isnull(an.Jan,0) as  [Target -Jan]";
                            Group4 = " tv.Jan ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H4 = "isnull(tv.JanB,0)  as   [Ach-Jan]";
                            GK4 = "isnull(an.Jan,0) as  [Target -Jan] ";
                            Group4 = " tv.JanB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H4 = "isnull(tv.JanI,0) as   [Ach-Jan]";
                            GK4 = "isnull(an.Jan,0) as  [Target -Jan]";
                            Group4 = " tv.JanI ";
                        }
                    }



                }
                if (i == 2)
                {

                    string fDate = (YYearID) + "-" + "" + Convert.ToString(Convert.ToInt32(i) - 1) + "" + "-" + "26";
                    DateTime frmDate = Convert.ToDateTime(fDate);





                    string fDate4 = (DateTime.Today.Year) + "-" + "" + Convert.ToString(i) + "" + "-" + "25";
                    DateTime Q5 = Convert.ToDateTime(fDate4);

                    if (iRowCount == 1)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H1 = "isnull(tv.Feb,0) as [Ach-Feb]";
                            GK1 = "sum(isnull(an.Feb,0)) as [Target -Feb]";
                            Group1 = " tv.Feb ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H1 = "isnull(tv.FebB,0) as [Ach-Feb]";
                            GK1 = "sum(isnull(an.Feb,0))  as [Target -Feb]";
                            Group1 = " tv.FebB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H1 = "isnull(tv.FebI,0) as [Ach-Feb]";
                            GK1 = "sum(isnull(an.Feb,0)) as [Target -Feb]";
                            Group1 = " tv.FebI ";
                        }
                    }
                    if (iRowCount == 2)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H2 = "isnull(tv.Feb,0) as [Ach-Feb]";
                            GK2 = "sum(isnull(an.Feb,0)) as [Target -Feb]";
                            Group2 = " tv.Feb ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H2 = "isnull(tv.FebB,0) as [Ach-Feb]";
                            GK2 = "sum(isnull(an.Feb,0))  as [Target -Feb]";
                            Group2 = " tv.FebB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H2 = "isnull(tv.FebI,0) as [Ach-Feb]";
                            GK2 = "sum(isnull(an.Feb,0)) as [Target -Feb]";
                            Group2 = " tv.FebI ";
                        }
                    }
                    if (iRowCount == 3)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H3 = "isnull(tv.Feb,0) as [Ach-Feb]";
                            GK3 = "sum(isnull(an.Feb,0)) as [Target -Feb]";
                            Group3 = " tv.Feb ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H3 = "isnull(tv.FebB,0) as [Ach-Feb]";
                            GK3 = "sum(isnull(an.Feb,0))  as [Target -Feb]";
                            Group3 = " tv.FebB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H3 = "isnull(tv.FebI,0) as [Ach-Feb]";
                            GK3 = "sum(isnull(an.Feb,0)) as [Target -Feb]";
                            Group3 = " tv.FebI ";
                        }
                    }
                    if (iRowCount == 4)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H4 = "isnull(tv.Feb,0) as [Ach-Feb]";
                            GK4 = "sum(isnull(an.Feb,0)) as [Target -Feb]";
                            Group4 = " tv.Feb ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H4 = "isnull(tv.FebB,0) as [Ach-Feb]";
                            GK4 = "sum(isnull(an.Feb,0))  as [Target -Feb]";
                            Group4 = " tv.FebB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H4 = "isnull(tv.FebI,0) as [Ach-Feb]";
                            GK4 = "sum(isnull(an.Feb,0)) as [Target -Feb]";
                            Group4 = " tv.FebI ";
                        }
                    }


                }



                if (i == 3)
                {

                    string fDate = (YYearID) + "-" + "" + Convert.ToString(Convert.ToInt32(i) - 1) + "" + "-" + "26";
                    DateTime frmDate = Convert.ToDateTime(fDate);

                    string fDate4 = (DateTime.Today.Year) + "-" + "" + Convert.ToString(i) + "" + "-" + "31";
                    DateTime Q5 = Convert.ToDateTime(fDate4);

                    if (iRowCount == 1)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H1 = "isnull(tv.Mar,0) as [Ach-Mar]";
                            GK1 = "sum(isnull(an.Mar,0)) as  [Target -Mar]";
                            Group1 = " tv.Mar ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H1 = "isnull(tv.MarB,0) as [Ach-Mar]";
                            GK1 = "sum(isnull(an.Mar,0)) as  [Target -Mar]";
                            Group1 = " tv.MarB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H1 = "isnull(tv.MarI,0) as [Ach-Mar]";
                            GK1 = "sum(isnull(an.Mar,0)) as  [Target -Mar]";
                            Group1 = " tv.MarI ";
                        }
                    }
                    if (iRowCount == 2)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H2 = "isnull(tv.Mar,0) as [Ach-Mar]";
                            GK2 = "sum(isnull(an.Mar,0)) as  [Target -Mar]";
                            Group2 = " tv.Mar ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H2 = "isnull(tv.MarB,0) as [Ach-Mar]";
                            GK2 = "sum(isnull(an.Mar,0)) as  [Target -Mar]";
                            Group2 = " tv.MarB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H2 = "isnull(tv.MarI,0) as [Ach-Mar]";
                            GK2 = "sum(isnull(an.Mar,0)) as  [Target -Mar]";
                            Group2 = " tv.MarI ";
                        }
                    }
                    if (iRowCount == 3)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H3 = "isnull(tv.Mar,0) as [Ach-Mar]";
                            GK3 = "sum(isnull(an.Mar,0)) as  [Target -Mar]";
                            Group3 = " tv.Mar ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H3 = "isnull(tv.MarB,0) as [Ach-Mar]";
                            GK3 = "sum(isnull(an.Mar,0)) as  [Target -Mar]";
                            Group3 = " tv.MarB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H3 = "isnull(tv.MarI,0) as [Ach-Mar]";
                            GK3 = "sum(isnull(an.Mar,0)) as  [Target -Mar]";
                            Group3 = " tv.MarI ";
                        }
                    }
                    if (iRowCount == 4)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H4 = "isnull(tv.Mar,0) as [Ach-Mar]";
                            GK4 = "sum(isnull(an.Mar,0)) as  [Target -Mar]";
                            Group4 = " tv.Mar ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H4 = "isnull(tv.MarB,0) as [Ach-Mar]";
                            GK4 = "sum(isnull(an.Mar,0)) as  [Target -Mar]";
                            Group4 = " tv.Mar ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H4 = "isnull(tv.MarI,0) as [Ach-Mar]";
                            GK4 = "sum(isnull(an.Mar,0)) as  [Target -Mar]";
                            Group4 = " tv.Mar ";
                        }
                    }


                }


                if (i == 4)
                {

                    string fDate = (YYearID) + "-" + "" + Convert.ToString(Convert.ToInt32(i)) + "" + "-" + "01";
                    DateTime frmDate = Convert.ToDateTime(fDate);





                    string fDate4 = (YYearID) + "-" + "" + Convert.ToString(i) + "" + "-" + "25";
                    DateTime Q5 = Convert.ToDateTime(fDate4);

                    if (iRowCount == 1)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H1 = "isnull(tv.Apr,0) as  [Ach-Apr] ";
                            GK1 = "sum(isnull(an.Apr,0)) as  [Target -Apr]";
                            Group1 = " tv.Apr ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H1 = "isnull(tv.AprB,0) as  [Ach-Apr] ";
                            GK1 = "sum(isnull(an.Apr,0)) as  [Target -Apr] ";
                            Group1 = " tv.AprB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H1 = "isnull(tv.AprI,0) as  [Ach-Apr]";
                            GK1 = "sum(isnull(an.Apr,0)) as  [Target -Apr]";
                            Group1 = " tv.AprI ";
                        }
                    }
                    if (iRowCount == 2)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H2 = "isnull(tv.Apr,0) as  [Ach-Apr] ";
                            GK2 = "sum(isnull(an.Apr,0)) as  [Target -Apr]";
                            Group2 = " tv.Apr ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H2 = "isnull(tv.AprB,0) as  [Ach-Apr] ";
                            GK2 = "sum(isnull(an.Apr,0)) as  [Target -Apr] ";
                            Group2 = " tv.AprB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H2 = "isnull(tv.AprI,0) as  [Ach-Apr]";
                            GK2 = "sum(isnull(an.Apr,0)) as  [Target -Apr]";
                            Group2 = " tv.AprI ";
                        }
                    }
                    if (iRowCount == 3)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H3 = "isnull(tv.Apr,0) as  [Ach-Apr] ";
                            GK3 = "sum(isnull(an.Apr,0)) as  [Target -Apr]";

                            Group3 = " tv.Apr ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H3 = "isnull(tv.AprB,0) as [Ach-Apr] ";
                            GK3 = "sum(isnull(an.Apr,0)) as  [Target -Apr]";
                            Group3 = " tv.AprB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H3 = "isnull(tv.AprI,0) as  [Ach-Apr] ";
                            GK3 = "sum(isnull(an.Apr,0)) as  [Target -Apr]";
                            Group3 = " tv.AprI ";
                        }
                    }
                    if (iRowCount == 4)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H4 = "isnull(tv.Apr,0) as  [Ach-Apr]";
                            GK4 = "sum(isnull(an.Apr,0)) as  [Target -Apr]";
                            Group4 = " tv.Apr ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H4 = "isnull(tv.AprB,0) as  [Ach-Apr]";
                            GK4 = "sum(isnull(an.Apr,0)) as  [Target -Apr]";
                            Group4 = " tv.AprB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H4 = "isnull(tv.AprI,0) as [Ach-Apr]";
                            GK4 = "sum(isnull(an.Apr,0)) as  [Target -Apr]";
                            Group4 = " tv.AprI ";
                        }
                    }


                }
                if (i == 5)
                {

                    string fDate = (YYearID) + "-" + "" + Convert.ToString(Convert.ToInt32(i) - 1) + "" + "-" + "26";
                    DateTime frmDate = Convert.ToDateTime(fDate);


                    string fDate4 = (YYearID) + "-" + "" + Convert.ToString(i) + "" + "-" + "25";
                    DateTime Q5 = Convert.ToDateTime(fDate4);

                    if (iRowCount == 1)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H1 = "isnull(tv.May,0) as [Ach-May]";
                            GK1 = "sum(isnull(an.May,0)) as [Target -May]";
                            Group1 = " tv.May ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H1 = "isnull(tv.MayB,0) as [Ach-May] ";
                            GK1 = "sum(isnull(an.May,0)) as [Target -May]";
                            Group1 = " tv.MayB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H1 = "isnull(tv.MayI,0) as [Ach-May] ";
                            GK1 = "sum(isnull(an.May,0)) as [Target -May]";
                            Group1 = " tv.MayI ";
                        }
                    }
                    if (iRowCount == 2)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H2 = "isnull(tv.May,0) as [Ach-May]";
                            GK2 = "sum(isnull(an.May,0)) as [Target -May]";
                            Group2 = " tv.May ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H2 = "isnull(tv.MayB,0) as [Ach-May] ";
                            GK2 = "sum(isnull(an.May,0)) as [Target -May]";
                            Group2 = " tv.MayB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H2 = "isnull(tv.MayI,0) as [Ach-May] ";
                            GK2 = "sum(isnull(an.May,0)) as [Target -May]";
                            Group2 = " tv.MayI ";
                        }
                    }
                    if (iRowCount == 3)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H3 = "isnull(tv.May,0) as [Ach-May]";
                            GK3 = "sum(isnull(an.May,0)) as [Target -May]";
                            Group3 = " tv.May ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H3 = "isnull(tv.MayB,0) as [Ach-May] ";
                            GK3 = "sum(isnull(an.May,0)) as [Target -May]";
                            Group3 = " tv.MayB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H3 = "isnull(tv.MayI,0) as [Ach-May] ";
                            GK3 = "sum(isnull(an.May,0)) as [Target -May]";
                            Group3 = " tv.MayI ";
                        }
                    }
                    if (iRowCount == 4)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H4 = "isnull(tv.May,0) as [Ach-May]";
                            GK4 = "sum(isnull(an.May,0)) as [Target -May]";
                            Group4 = " tv.May ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H4 = "isnull(tv.MayB,0) as [Ach-May] ";
                            GK4 = "sum(isnull(an.May,0)) as [Target -May]";
                            Group4 = " tv.MayB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H4 = "isnull(tv.MayI,0) as [Ach-May] ";
                            GK4 = "sum(isnull(an.May,0)) as [Target -May]";
                            Group4 = " tv.MayI ";
                        }
                    }


                }
                if (i == 6)
                {

                    string fDate = (YYearID) + "-" + "" + Convert.ToString(Convert.ToInt32(i) - 1) + "" + "-" + "26";
                    DateTime frmDate = Convert.ToDateTime(fDate);


                    string fDate4 = (YYearID) + "-" + "" + Convert.ToString(i) + "" + "-" + "25";
                    DateTime Q5 = Convert.ToDateTime(fDate4);

                    if (iRowCount == 1)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H1 = "isnull(tv.Jun,0) as [Ach-Jun] ";
                            GK1 = "sum(isnull(an.Jun,0)) as [Target -Jun]";
                            Group1 = " tv.Jun ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H1 = "isnull(tv.JunB,0)  as [Ach-Jun]";
                            GK1 = "sum(isnull(an.Jun,0)) as [Target -Jun] ";
                            Group1 = " tv.JunB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H1 = "isnull(tv.JunI,0)  as [Ach-Jun]";
                            GK1 = "sum(isnull(an.Jun,0)) as [Target -Jun]";
                            Group1 = " tv.JunI ";
                        }
                    }
                    if (iRowCount == 2)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H2 = "isnull(tv.Jun,0) as [Ach-Jun] ";
                            GK2 = "sum(isnull(an.Jun,0)) as [Target -Jun]";
                            Group2 = " tv.Jun ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H2 = "isnull(tv.JunB,0)  as [Ach-Jun]";
                            GK2 = "sum(isnull(an.Jun,0)) as [Target -Jun] ";
                            Group2 = " tv.JunB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H2 = "isnull(tv.JunI,0)  as [Ach-Jun]";
                            GK2 = "sum(isnull(an.Jun,0)) as [Target -Jun]";
                            Group2 = " tv.JunI ";
                        }
                    }
                    if (iRowCount == 3)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H3 = "isnull(tv.Jun,0) as [Ach-Jun] ";
                            GK3 = "sum(isnull(an.Jun,0)) as [Target -Jun]";
                            Group3 = " tv.Jun ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H3 = "isnull(tv.JunB,0)  as [Ach-Jun]";
                            GK3 = "sum(isnull(an.Jun,0)) as [Target -Jun] ";
                            Group3 = " tv.JunB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H3 = "isnull(tv.JunI,0)  as [Ach-Jun]";
                            GK3 = "sum(isnull(an.Jun,0)) as [Target -Jun]";
                            Group3 = " tv.JunI ";
                        }
                    }
                    if (iRowCount == 4)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H4 = "isnull(tv.Jun,0) as [Ach-Jun] ";
                            GK4 = "sum(isnull(an.Jun,0)) as [Target -Jun]";
                            Group4 = " tv.Jun ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H4 = "isnull(tv.JunB,0)  as [Ach-Jun]";
                            GK4 = "sum(isnull(an.Jun,0)) as [Target -Jun] ";
                            Group4 = " tv.JunB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H4 = "isnull(tv.JunI,0)  as [Ach-Jun]";
                            GK4 = "sum(isnull(an.Jun,0)) as [Target -Jun]";
                            Group4 = " tv.JunI ";
                        }
                    }


                }

                if (i == 7)
                {

                    string fDate = (YYearID) + "-" + "" + Convert.ToString(Convert.ToInt32(i) - 1) + "" + "-" + "26";
                    DateTime frmDate = Convert.ToDateTime(fDate);





                    string fDate4 = (YYearID) + "-" + "" + Convert.ToString(i) + "" + "-" + "25";
                    DateTime Q5 = Convert.ToDateTime(fDate4);

                    if (iRowCount == 1)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H1 = "isnull(tv.Jul,0) as [Ach-Jul] ";
                            GK1 = "sum(isnull(an.Jul,0)) as [Target -Jul]";
                            Group1 = " tv.Jul ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H1 = "isnull(tv.JulB,0)  as [Ach-Jul]";
                            GK1 = "sum(isnull(an.Jul,0)) as [Target -Jul] ";
                            Group1 = " tv.JulB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H1 = "isnull(tv.JulI,0)  as [Ach-Jul]";
                            GK1 = "sum(isnull(an.Jul,0)) as [Target -Jul]";
                            Group1 = " tv.JulI ";
                        }
                    }
                    if (iRowCount == 2)
                    {

                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H2 = "isnull(tv.Jul,0) as [Ach-Jul] ";
                            GK2 = "isnull(tv.Jul,0) as [Target -Jul]";
                            Group2 = " tv.Jul ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H2 = "isnull(tv.JulB,0)  as [Ach-Jul]";
                            GK2 = "sum(isnull(an.Jul,0)) as [Target -Jul] ";
                            Group2 = " tv.JulB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H2 = "isnull(tv.JulI,0)  as [Ach-Jul]";
                            GK2 = "sum(isnull(an.Jul,0)) as [Target -Jul]";
                            Group2 = " tv.JulI ";
                        }
                    }
                    if (iRowCount == 3)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H3 = "isnull(tv.Jul,0) as [Ach-Jul] ";
                            GK3 = "sum(isnull(an.Jul,0)) as [Target -Jul]";
                            Group3 = " tv.Jul ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H3 = "isnull(tv.JulB,0)  as [Ach-Jul]";
                            GK3 = "sum(isnull(an.Jul,0)) as [Target -Jul] ";
                            Group3 = " tv.JulB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H3 = "isnull(tv.JulI,0)  as [Ach-Jul]";
                            GK3 = "sum(isnull(an.Jul,0)) as [Target -Jul]";
                            Group3 = " tv.JulI ";
                        }
                    }
                    if (iRowCount == 4)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H4 = "isnull(tv.Jul,0) as [Ach-Jul] ";
                            GK4 = "sum(isnull(an.Jul,0)) as [Target -Jul]";
                            Group4 = " tv.Jul ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H4 = "isnull(tv.JulB,0)  as [Ach-Jul]";
                            GK4 = "sum(isnull(an.Jul,0)) as [Target -Jul] ";
                            Group4 = " tv.JulB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H4 = "isnull(tv.JulI,0)  as [Ach-Jul]";
                            GK4 = "sum(isnull(an.Jul,0)) as [Target -Jul]";
                            Group4 = " tv.JulI ";
                        }
                    }


                }
                if (i == 8)
                {

                    string fDate = (YYearID) + "-" + "" + Convert.ToString(Convert.ToInt32(i) - 1) + "" + "-" + "26";
                    DateTime frmDate = Convert.ToDateTime(fDate);





                    string fDate4 = (YYearID) + "-" + "" + Convert.ToString(i) + "" + "-" + "25";
                    DateTime Q5 = Convert.ToDateTime(fDate4);

                    if (iRowCount == 1)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H1 = "isnull(tv.Aug,0) as [Ach-Aug] ";
                            GK1 = "sum(isnull(an.Aug,0)) as [Target -Aug]";
                            Group1 = " tv.Aug ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H1 = "isnull(tv.AugB,0)  as [Ach-Aug]";
                            GK1 = "sum(isnull(an.Aug,0)) as [Target -Aug] ";
                            Group1 = " tv.AugB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H1 = "isnull(tv.AugI,0)  as [Ach-Aug]";
                            GK1 = "sum(isnull(an.Aug,0)) as [Target -Aug]";
                            Group1 = " tv.AugI ";
                        }
                    }
                    if (iRowCount == 2)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H2 = "isnull(tv.Aug,0) as [Ach-Aug] ";
                            GK2 = "sum(isnull(an.Aug,0)) as [Target -Aug]";
                            Group2 = " tv.Aug ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H2 = "isnull(tv.AugB,0)  as [Ach-Aug]";
                            GK2 = "sum(isnull(an.Aug,0)) as [Target -Aug] ";
                            Group2 = " tv.AugB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H2 = "isnull(tv.AugI,0)  as [Ach-Aug]";
                            GK2 = "sum(isnull(an.Aug,0)) as [Target -Aug]";
                            Group2 = " tv.AugI ";
                        }
                    }
                    if (iRowCount == 3)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H3 = "isnull(tv.Aug,0) as [Ach-Aug] ";
                            GK3 = "sum(isnull(an.Aug,0)) as [Target -Aug]";
                            Group3 = " tv.Aug ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H3 = "isnull(tv.AugB,0)  as [Ach-Aug]";
                            GK3 = "sum(isnull(an.Aug,0)) as [Target -Aug] ";
                            Group3 = " tv.AugB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H3 = "isnull(tv.AugI,0)  as [Ach-Aug]";
                            GK3 = "sum(isnull(an.Aug,0)) as [Target -Aug]";
                            Group3 = " tv.AugI ";
                        }
                    }
                    if (iRowCount == 4)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H4 = "isnull(tv.Aug,0) as [Ach-Aug] ";
                            GK4 = "sum(isnull(an.Aug,0)) as [Target -Aug]";
                            Group4 = " tv.Aug ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H4 = "isnull(tv.AugB,0)  as [Ach-Aug]";
                            GK4 = "sum(isnull(an.Aug,0)) as [Target -Aug] ";
                            Group4 = " tv.AugB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H4 = "isnull(tv.AugI,0)  as [Ach-Aug]";
                            GK4 = "sum(isnull(an.Aug,0)) as [Target -Aug]";
                            Group4 = " tv.AugI ";
                        }
                    }


                }
                if (i == 9)
                {

                    string fDate = (YYearID) + "-" + "" + Convert.ToString(Convert.ToInt32(i) - 1) + "" + "-" + "26";
                    DateTime frmDate = Convert.ToDateTime(fDate);





                    string fDate4 = (YYearID) + "-" + "" + Convert.ToString(i) + "" + "-" + "25";
                    DateTime Q5 = Convert.ToDateTime(fDate4);

                    if (iRowCount == 1)
                    {
                        con1 = "  ActivityDate between('" + frmDate.ToString("yyyy-MM-dd") + "') and '" + Q5.ToString("yyyy-MM-dd") + "' ";
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H1 = "isnull(tv.Sep,0) as [Ach-Sep] ";
                            GK1 = "sum(isnull(an.Sep,0)) as [Target -Sep]";
                            Group1 = " tv.Sep ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H1 = "isnull(tv.SepB,0)  as [Ach-Sep]";
                            GK1 = "sum(isnull(an.Sep,0)) as [Target -Sep] ";
                            Group1 = " tv.SepB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H1 = "isnull(tv.SepI,0)  as [Ach-Sep]";
                            GK1 = "sum(isnull(an.Sep,0)) as [Target -Sep]";
                            Group1 = " tv.SepI ";
                        }
                    }
                    if (iRowCount == 2)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H2 = "isnull(tv.Sep,0) as [Ach-Sep] ";
                            GK2 = "sum(isnull(an.Sep,0)) as [Target -Sep]";
                            Group2 = " tv.Sep ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H2 = "isnull(tv.SepB,0)  as [Ach-Sep]";
                            GK2 = "sum(isnull(an.Sep,0)) as [Target -Sep] ";
                            Group2 = " tv.SepB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H2 = "isnull(tv.SepI,0)  as [Ach-Sep]";
                            GK2 = "sum(isnull(an.Sep,0)) as [Target -Sep]";
                            Group2 = " tv.SepI ";
                        }
                    }
                    if (iRowCount == 3)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H3 = "isnull(tv.Sep,0) as [Ach-Sep] ";
                            GK3 = "sum(isnull(an.Sep,0)) as [Target -Sep]";
                            Group3 = " tv.Sep ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H3 = "isnull(tv.SepB,0)  as [Ach-Sep]";
                            GK3 = "sum(isnull(an.Sep,0)) as [Target -Sep] ";
                            Group3 = " tv.SepB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H3 = "isnull(tv.SepI,0)  as [Ach-Sep]";
                            GK3 = "sum(isnull(an.Sep,0)) as [Target -Sep]";
                            Group3 = " tv.SepI ";
                        }
                    }
                    if (iRowCount == 4)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H4 = "isnull(tv.Sep,0) as [Ach-Sep] ";
                            GK4 = "sum(isnull(an.Sep,0)) as [Target -Sep]";
                            Group4 = " tv.Sep ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H4 = "isnull(tv.SepB,0)  as [Ach-Sep]";
                            GK4 = "sum(isnull(an.Sep,0)) as [Target -Sep] ";
                            Group4 = " tv.SepB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H4 = "isnull(tv.SepI,0)  as [Ach-Sep]";
                            GK4 = "sum(isnull(an.Sep,0)) as [Target -Sep]";
                            Group4 = " tv.SepI ";
                        }
                    }


                }
                if (i == 10)
                {

                    string fDate = (YYearID) + "-" + "" + Convert.ToString(Convert.ToInt32(i) - 1) + "" + "-" + "26";
                    DateTime frmDate = Convert.ToDateTime(fDate);





                    string fDate4 = (YYearID) + "-" + "" + Convert.ToString(i) + "" + "-" + "25";
                    DateTime Q5 = Convert.ToDateTime(fDate4);

                    if (iRowCount == 1)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H1 = "isnull(tv.Oct,0) as [Ach-Oct] ";
                            GK1 = "sum(isnull(an.Oct,0)) as [Target -Oct]";
                            Group1 = " tv.Oct ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H1 = "isnull(tv.OctB,0)  as [Ach-Oct]";
                            GK1 = "sum(isnull(an.Oct,0)) as [Target -Oct] ";
                            Group1 = " tv.OctB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H1 = "isnull(tv.OctI,0)  as [Ach-Oct]";
                            GK1 = "sum(isnull(an.Oct,0)) as [Target -Oct]";
                            Group1 = " tv.OctI ";
                        }
                    }
                    if (iRowCount == 2)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H2 = "isnull(tv.Oct,0) as [Ach-Oct] ";
                            GK2 = "sum(isnull(an.Oct,0)) as [Target -Oct]";
                            Group2 = " tv.Oct ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H2 = "isnull(tv.OctB,0)  as [Ach-Oct]";
                            GK2 = "sum(isnull(an.Oct,0)) as [Target -Oct] ";
                            Group2 = " tv.OctB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H2 = "isnull(tv.OctI,0)  as [Ach-Oct]";
                            GK2 = "sum(isnull(an.Oct,0)) as [Target -Oct]";
                            Group2 = " tv.OctI ";
                        }
                    }
                    if (iRowCount == 3)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H3 = "isnull(tv.Oct,0) as [Ach-Oct] ";
                            GK3 = "sum(isnull(an.Oct,0)) as [Target -Oct]";
                            Group3 = " tv.Oct ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H3 = "isnull(tv.OctB,0)  as [Ach-Oct]";
                            GK3 = "sum(isnull(an.Oct,0)) as [Target -Oct] ";
                            Group3 = " tv.OctB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H3 = "isnull(tv.OctI,0)  as [Ach-Oct]";
                            GK3 = "sum(isnull(an.Oct,0)) as [Target -Oct]";
                            Group3 = " tv.OctI ";
                        }
                    }
                    if (iRowCount == 4)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H4 = "isnull(tv.Oct,0) as [Ach-Oct] ";
                            GK4 = "sum(isnull(an.Oct,0)) as [Target -Oct]";
                            Group4 = " tv.Oct ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H4 = "isnull(tv.OctB,0)  as [Ach-Oct]";
                            GK4 = "sum(isnull(an.Oct,0)) as [Target -Oct] ";
                            Group4 = " tv.OctB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H4 = "isnull(tv.OctI,0)  as [Ach-Oct]";
                            GK4 = "sum(isnull(an.Oct,0)) as [Target -Oct]";
                            Group4 = " tv.OctI ";
                        }
                    }


                }
                if (i == 11)
                {

                    string fDate = (YYearID) + "-" + "" + Convert.ToString(Convert.ToInt32(i) - 1) + "" + "-" + "26";
                    DateTime frmDate = Convert.ToDateTime(fDate);





                    string fDate4 = (YYearID) + "-" + "" + Convert.ToString(i) + "" + "-" + "25";
                    DateTime Q5 = Convert.ToDateTime(fDate4);

                    if (iRowCount == 1)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H1 = "isnull(tv.Nov,0) as [Ach-Nov] ";
                            GK1 = "sum(isnull(an.Nov,0)) as [Target -Nov]";
                            Group1 = " tv.Nov ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H1 = "isnull(tv.NovB,0)  as [Ach-Nov]";
                            GK1 = "sum(isnull(an.Nov,0)) as [Target -Nov] ";
                            Group1 = " tv.NovB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H1 = "isnull(tv.NovI,0)  as [Ach-Nov]";
                            GK1 = "sum(isnull(an.Nov,0)) as [Target -Nov]";
                            Group1 = " tv.NovI ";
                        }
                    }
                    if (iRowCount == 2)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H2 = "isnull(tv.Nov,0) as [Ach-Nov] ";
                            GK2 = "sum(isnull(an.Nov,0)) as [Target -Nov]";
                            Group2 = " tv.Nov ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H2 = "isnull(tv.NovB,0)  as [Ach-Nov]";
                            GK2 = "sum(isnull(an.Nov,0)) as [Target -Nov] ";
                            Group2 = " tv.NovB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H2 = "isnull(tv.NovI,0)  as [Ach-Nov]";
                            GK2 = "sum(isnull(an.Nov,0)) as [Target -Nov]";
                            Group2 = " tv.NovI ";
                        }
                    }
                    if (iRowCount == 3)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H3 = "isnull(tv.Nov,0) as [Ach-Nov] ";
                            GK3 = "sum(isnull(an.Nov,0)) as [Target -Nov]";
                            Group3 = " tv.Nov ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H3 = "isnull(tv.NovB,0)  as [Ach-Nov]";
                            GK3 = "sum(isnull(an.Nov,0)) as [Target -Nov] ";
                            Group3 = " tv.NovB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H3 = "isnull(tv.NovI,0)  as [Ach-Nov]";
                            GK3 = "sum(isnull(an.Nov,0)) as [Target -Nov]";
                            Group3 = " tv.NovI ";
                        }
                    }
                    if (iRowCount == 4)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H4 = "isnull(tv.Nov,0) as [Ach-Nov] ";
                            GK4 = "sum(isnull(an.Nov,0)) as [Target -Nov]";
                            Group4 = " tv.Nov ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H4 = "isnull(tv.NovB,0)  as [Ach-Nov]";
                            GK4 = "sum(isnull(an.Nov,0)) as [Target -Nov] ";
                            Group4 = " tv.NovB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H4 = "isnull(tv.NovI,0)  as [Ach-Nov]";
                            GK4 = "sum(isnull(an.Nov,0)) as [Target -Nov]";
                            Group4 = " tv.NovI ";
                        }
                    }


                }
                if (i == 12)
                {

                    string fDate = (YYearID) + "-" + "" + Convert.ToString(Convert.ToInt32(i) - 1) + "" + "-" + "26";
                    DateTime frmDate = Convert.ToDateTime(fDate);





                    string fDate4 = (YYearID) + "-" + "" + Convert.ToString(i) + "" + "-" + "25";
                    DateTime Q5 = Convert.ToDateTime(fDate4);

                    if (iRowCount == 1)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H1 = "isnull(tv.Dec,0) as [Ach-Dec] ";
                            GK1 = "sum(isnull(an.Dec,0)) as [Target -Dec]";
                            Group1 = " tv.Dec ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H1 = "isnull(tv.DecB,0)  as [Ach-Dec]";
                            GK1 = "sum(isnull(an.Dec,0)) as [Target -Dec] ";
                            Group1 = " tv.DecB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H1 = "isnull(tv.DecI,0)  as [Ach-Dec]";
                            GK1 = "sum(isnull(an.Dec,0)) as [Target -Dec]";
                            Group1 = " tv.DecI ";
                        }
                    }
                    if (iRowCount == 2)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H2 = "isnull(tv.Dec,0) as [Ach-Dec] ";
                            GK2 = "sum(isnull(an.Dec,0)) as [Target -Dec]";
                            Group2 = " tv.Dec ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H2 = "isnull(tv.DecB,0)  as [Ach-Dec]";
                            GK2 = "sum(isnull(an.Dec,0)) as [Target -Dec] ";
                            Group2 = " tv.DecB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H2 = "isnull(tv.DecI,0)  as [Ach-Dec]";
                            GK2 = "sum(isnull(an.Dec,0)) as [Target -Dec]";
                            Group2 = " tv.DecI ";
                        }
                    }
                    if (iRowCount == 3)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H3 = "isnull(tv.Dec,0) as [Ach-Dec] ";
                            GK3 = "sum(isnull(an.Dec,0)) as [Target -Dec]";
                            Group3 = " tv.Dec ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H3 = "isnull(tv.DecB,0)  as [Ach-Dec]";
                            GK3 = "sum(isnull(an.Dec,0)) as [Target -Dec] ";
                            Group3 = " tv.DecB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H3 = "isnull(tv.DecI,0)  as [Ach-Dec]";
                            GK3 = "sum(isnull(an.Dec,0)) as [Target -Dec]";
                            Group3 = " tv.DecI ";
                        }
                    }
                    if (iRowCount == 4)
                    {
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                        {
                            H4 = "isnull(tv.Dec,0) as [Ach-Dec] ";
                            GK4 = "sum(isnull(an.Dec,0)) as [Target -Dec]";
                            Group4 = " tv.Dec ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                        {
                            H4 = "isnull(tv.DecB,0)  as [Ach-Dec]";
                            GK4 = "sum(isnull(an.Dec,0)) as [Target -Dec] ";
                            Group4 = " tv.DecB ";
                        }
                        if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                        {
                            H4 = "isnull(tv.DecI,0)  as [Ach-Dec]";
                            GK4 = "sum(isnull(an.Dec,0)) as [Target -Dec]";
                            Group4 = " tv.DecI ";
                        }
                    }


                }
                iRowCount = iRowCount + 1;
            }
            string con = "";
            string GroupyBy = "";
            if (H1.Length > 0)
            {

                con = GK1 + ",";
                GroupyBy += Group1 + ",";
                con += H1 + ",";
            }
            if (H2.Length > 0)
            {
                con += GK2 + ",";
                con += H2 + ",";
                GroupyBy += Group2 + ",";
            }
            if (H3.Length > 0)
            {
                con += GK3 + ",";
                con += H3 + ",";
                GroupyBy += Group3 + ",";
            }
            if (H4.Length > 0)
            {
                con += GK4 + ",";
                con += H4 + ",";
                GroupyBy += Group4 + ",";
            }
            if (con.Length > 0)
            {
                con = con.Substring(0, con.LastIndexOf(","));
            }
            if (GroupyBy.Length > 0)
            {
                GroupyBy = GroupyBy.Substring(0, GroupyBy.LastIndexOf(","));
            }
            dtMain = rptActivityMonthReportNew(conditions1 + conditions, con, GroupyBy);
            ViewState["dt"] = dtMain;
            if (dtMain.Rows.Count > 0)
            {
                string aprove = "";
                if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                {
                    aprove = "FC";
                }
                if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                {
                    aprove = "BO";
                }
                if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                {
                    aprove = "IO";
                }
                if (dtMain.Rows.Count > 0)
                {
                    ExportToCSVFileApprove(dtMain, "ActivityMonthWise", aprove);
                }
                //else
                //{
                //    GV_DynamicGrid2.DataSource = dtMain;
                //    GV_DynamicGrid2.DataBind();
                //}
            }
            else
            {
                GV_DynamicGrid2.DataSource = null;
                GV_DynamicGrid2.DataBind();
            }


            #endregion
        }
    }

    public DataTable rptActivityMonthReportNew(string WhereQuery, string Q1, string G)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {

            new SqlParameter("@WhereQuery", WhereQuery),
            new SqlParameter("@SelectQuery", Q1),
            new SqlParameter("@Groupby", G),

        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivityMontlyNew]", cmdParameters);
    }
    public void LoadQuter()
    {
        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        if (Convert.ToInt32(ddlType.SelectedValue) == 4)
        {


            #region Month From to TO month
            string con1 = "", con2 = "", con3 = "", con4 = "", con5 = "";

            string ddlDistrict = "";
            string ddlPhan = "";
            string ddlVillage = "";
            string ddlBlock = "";
            string ddlStatecode = "";
            foreach (ListItem item in ChkState.Items)
            {
                if (item.Selected)
                {

                    ddlStatecode += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlStatecode.Length > 0)
            {
                ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
            }
            foreach (ListItem item in chkDistrict.Items)
            {
                if (item.Selected)
                {

                    ddlDistrict += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlDistrict.Length > 0)
            {
                ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
            }
            foreach (ListItem item in chkBlock.Items)
            {
                if (item.Selected)
                {

                    ddlBlock += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlBlock.Length > 0)
            {
                ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
            }

            foreach (ListItem item in ddlPanchayat.Items)
            {
                if (item.Selected)
                {

                    ddlPhan += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlPhan.Length > 0)
            {
                ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
            }
            foreach (ListItem item in chkVillage.Items)
            {
                if (item.Selected)
                {

                    ddlVillage += "'" + item.Value + "'" + ",";


                }
            }
            if (ddlVillage.Length > 0)
            {
                ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
            }
            if (ddlYear.SelectedIndex > 0)
            {
                conditions1 = "    V.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
            }
            if (ddlStatecode.Length > 0)
            {
                conditions1 += "  and  V.StateCode in(" + ddlStatecode + ") ";
            }
            if (ddlDistrict.Length > 0)
            {
                conditions1 += " and  V.DistrictCode in(" + ddlDistrict + ") ";
            }
            if (ddlBlock.Length > 0)
            {
                conditions1 += "  and V.BlockCode in(" + ddlBlock + ") ";
            }
            if (ddlPhan.Length > 0)
            {
                conditions1 = "  and V.PanchayatCode in(" + ddlPhan + ") ";
            }
            if (ddlVillage.Length > 0)
            {
                conditions1 += " and  V.VillageCode in(" + ddlVillage + ") ";
            }
            int flag = 0;
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                flag = 1;
            }
            else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                flag = 2;
            }
            else
            {
                flag = 3;
            }
            //  dtMain = objMain.rptActivityUpdateReportsMonthly(conditions + conditions1);
            #endregion
            #region weakFilter



            dtMain = objMain.rptActivityQuerltyNew(conditions1 + conditions, flag);
            if (dtMain.Rows.Count > 0)
            {
                string aprove = "";
                if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                {
                    aprove = "FC";
                }
                if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                {
                    aprove = "BO";
                }
                if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                {
                    aprove = "IO";
                }
                //if (dtMain.Rows.Count > 1000)
                //{

                if (dtMain.Rows.Count > 0)
                {
                    ExportToCSVFileApprove(dtMain, "ActivityQuarterWise", aprove);

                    ViewState["dt"] = dtMain;
                }
                else
                {
                    GV_DynamicGrid2.DataSource = dtMain;
                    GV_DynamicGrid2.DataBind();

                    ViewState["dt"] = dtMain;
                }
            }
            else
            {
                GV_DynamicGrid2.DataSource = null;
                GV_DynamicGrid2.DataBind();

                ViewState["dt"] = "";
            }




            #endregion
        }
    }


    private void ExportToCSVFileApprove(DataTable dtTable, string filePath, string Approve)
    {
        StringBuilder sbldr = new StringBuilder();
        if (dtTable != null)
        {
            if (dtTable.Columns.Count != 0)
            {
                foreach (DataColumn col in dtTable.Columns)
                {
                    sbldr.Append(col.ColumnName + ',');
                }
                sbldr.Append("\r\n");
                foreach (DataRow row in dtTable.Rows)
                {
                    foreach (DataColumn column in dtTable.Columns)
                    {

                        sbldr.Append(Convert.ToString(row[column]).Replace(",", "  ").Replace("\r", "").Replace("\n", "") + ',');
                    }
                    sbldr.Append("\r\n");

                }
            }
            string sFileDir = Server.MapPath(Comman.GetImagePath("DataBackupPath")); ;
            string Fullfilename = "" + filePath + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + "_" + Approve + ".csv";

            string path = sFileDir + Fullfilename;
            File.WriteAllText(path, sbldr.ToString());

            FileStream fs = null;//, fs2=null;
            try
            {
                string path1 = Fullfilename;
                string foldername = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + path1 + "");
                string datafolder = path1.Substring(0, path1.Length - 4);
                //  string[] file = Directory.GetFiles(foldername);

                string fullPath = Request.MapPath("~/DataBackup/" + datafolder + "" + ".zip");
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(foldername, "");
                    //    zip.AddFiles(file, foldername);
                    zip.Save(Server.MapPath(Comman.GetImagePath("DataBackupPath")) + "/" + datafolder + "" + ".zip");

                }



                HttpResponse Response = HttpContext.Current.Response; Response.Clear(); Response.ClearHeaders(); Response.Charset = "UTF-8";
                fs = File.Open(fullPath, FileMode.Open);
                byte[] bytBytes = new byte[(fs.Length)];
                fs.Read(bytBytes, 0, (int)fs.Length);
                fs.Close();
                Response.AddHeader("Content-disposition", "attachment; filename=" + datafolder + "" + ".zip");
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(bytBytes);






                if (File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                if (File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.End();
            }

            catch (System.Exception ex)
            {
                //  Server.Transfer("default.aspx", false);
                Response.Clear();

                //string mmsg = ex.Message;
                //showEXPMessages("(crateZip)  " + mmsg); //showMessages(mmsg);
            }
            finally
            {
                fs.Dispose();
                Response.Clear();

            }
        }
        //str.Write(sbldr.ToString());
        //Response.ContentType = "Application/x-msexcel";
        //Response.AddHeader("content-disposition", "attachment;filename=test.csv");
        //Response.Write(sbldr.ToString());
        //Response.End();
    }
    private void ExportToCSVFileDateWise(DataTable dtTable, string filePath)
    {
        StringBuilder sbldr = new StringBuilder();
        if (dtTable != null)
        {
            if (dtTable.Columns.Count != 0)
            {
                foreach (DataColumn col in dtTable.Columns)
                {
                    sbldr.Append(col.ColumnName + ',');
                }
                sbldr.Append("\r\n");
                foreach (DataRow row in dtTable.Rows)
                {
                    foreach (DataColumn column in dtTable.Columns)
                    {

                        sbldr.Append(Convert.ToString(row[column]).Replace(",", "  ").Replace("\r", "").Replace("\n", "") + ',');
                    }
                    sbldr.Append("\r\n");

                }
            }
            string sFileDir = Server.MapPath(Comman.GetImagePath("DataBackupPath")); ;
            string Fullfilename = filePath;
            string path = sFileDir + Fullfilename;
            File.WriteAllText(path, sbldr.ToString());

            FileStream fs = null;//, fs2=null;
            try
            {
                string path1 = Fullfilename;
                string foldername = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + path1 + "");
                string datafolder = path1.Substring(0, path1.Length - 4);
                //  string[] file = Directory.GetFiles(foldername);

                string fullPath = Request.MapPath("~/DataBackup/" + datafolder + "" + ".zip");
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(foldername, "");
                    //    zip.AddFiles(file, foldername);
                    zip.Save(Server.MapPath(Comman.GetImagePath("DataBackupPath")) + "/" + datafolder + "" + ".zip");

                }



                HttpResponse Response = HttpContext.Current.Response; Response.Clear(); Response.ClearHeaders(); Response.Charset = "UTF-8";
                fs = File.Open(fullPath, FileMode.Open);
                byte[] bytBytes = new byte[(fs.Length)];
                fs.Read(bytBytes, 0, (int)fs.Length);
                fs.Close();
                Response.AddHeader("Content-disposition", "attachment; filename=" + datafolder + "" + ".zip");
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(bytBytes);






                if (File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                if (File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.End();
            }

            catch (System.Exception ex)
            {
                //  Server.Transfer("default.aspx", false);
                Response.Clear();

                //string mmsg = ex.Message;
                //showEXPMessages("(crateZip)  " + mmsg); //showMessages(mmsg);
            }
            finally
            {
                fs.Dispose();
                Response.Clear();

            }
        }
        //str.Write(sbldr.ToString());
        //Response.ContentType = "Application/x-msexcel";
        //Response.AddHeader("content-disposition", "attachment;filename=test.csv");
        //Response.Write(sbldr.ToString());
        //Response.End();
    }
    private void ExporttoExcelNew(DataTable table, string FileName)
    {
        if (table != null)
        {

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            string Fullfilename = "" + FileName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";

            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + " ");

            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
              "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
            //am getting my grid's column headers
            int columnscount = table.Columns.Count;


            for (int j = 0; j < columnscount; j++)
            {      //write in new column
                HttpContext.Current.Response.Write("<Td>");
                //Get column headers  and make it as bold in excel columns
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(table.Columns[j].ColumnName);
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in table.Rows)
            {//write in new row
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }

    public void ExportReportQuestion()
    {

        DataTable dtMain = Session["dtHeader"] as DataTable;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\AssessmentQuestion.xlsx");
        var ws = wb.Worksheet(1);




        //for (int x = 0; x < dtMain.Columns.Count; x++)
        //{

        //    ws.Cell(1, x + 1).Value = dtMain.Columns[x].ColumnName;
        //}

        //dt1.Columns.Remove("rownNO");
        ws.Cell(2, 1).InsertData(dtMain.Rows);
        Int32 ii = Convert.ToInt32(dtMain.Rows.Count) + 2;
        string str = "A1:S" + ii;

        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        filepath = StartupPath + "\\QuestionWiseAnalysis " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
        wb.SaveAs(filepath);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
        Response.WriteFile(filepath);

        Response.End();
        if (File.Exists(filepath))
        {
            System.IO.File.Delete(filepath);
        }
    }
    private void ExportToCSVFile(DataTable dtTable, string filePath)
    {

        StringBuilder sbldr = new StringBuilder();
        if (dtTable != null)
        {

            if (dtTable.Columns.Count != 0)
            {
                int icount = 0;
                foreach (DataColumn col in dtTable.Columns)
                {
                    sbldr.Append(col.ColumnName + ',');
                }
                sbldr.Append("\r\n");

                foreach (DataRow row in dtTable.Rows)
                {


                    foreach (DataColumn column in dtTable.Columns)
                    {

                        sbldr.Append(Convert.ToString(row[column]).Replace(",", "  ").Replace("\r", "").Replace("\n", "") + ',');
                    }
                    sbldr.Append("\r\n");

                    icount = icount + 1;
                }


            }
            string sFileDir = Server.MapPath(Comman.GetImagePath("DataBackupPath")); ;
            string Fullfilename = "" + filePath + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".csv";
            string path = sFileDir + Fullfilename;
            File.WriteAllText(path, sbldr.ToString());

            FileStream fs = null;//, fs2=null;
            try
            {
                string path1 = Fullfilename;
                string foldername = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + path1 + "");
                string datafolder = path1.Substring(0, path1.Length - 4);
                //  string[] file = Directory.GetFiles(foldername);

                string fullPath = Request.MapPath("~/DataBackup/" + datafolder + "" + ".zip");
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(foldername, "");
                    //    zip.AddFiles(file, foldername);
                    zip.Save(Server.MapPath(Comman.GetImagePath("DataBackupPath")) + "/" + datafolder + "" + ".zip");

                }



                HttpResponse Response = HttpContext.Current.Response; Response.Clear(); Response.ClearHeaders(); Response.Charset = "UTF-8";
                fs = File.Open(fullPath, FileMode.Open);
                byte[] bytBytes = new byte[(fs.Length)];
                fs.Read(bytBytes, 0, (int)fs.Length);
                fs.Close();
                Response.AddHeader("Content-disposition", "attachment; filename=" + datafolder + "" + ".zip");
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(bytBytes);






                if (File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                if (File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.End();
            }

            catch (System.Exception ex)
            {
                //  Server.Transfer("default.aspx", false);
                Response.Clear();

                //string mmsg = ex.Message;
                //showEXPMessages("(crateZip)  " + mmsg); //showMessages(mmsg);
            }
            finally
            {
                fs.Dispose();
                Response.Clear();

            }
        }
        //str.Write(sbldr.ToString());
        //Response.ContentType = "Application/x-msexcel";
        //Response.AddHeader("content-disposition", "attachment;filename=test.csv");
        //Response.Write(sbldr.ToString());
        //Response.End();
    }
    public void LoadSchoolProfile()
    {
        Session["dt"] = null;
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();
        string conditions1 = "";
        DataTable dtMain = null;
        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            #region DateWise
            if (txtDate.Text == "" || txtTodate.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Date')</script>", false);
                return;
            }


            string fromDate = txtDate.Text;

            string[] d = fromDate.Split('/');
            string afromDate = d[2] + '-' + d[1] + '-' + d[0];

            string ToDate = txtTodate.Text;
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
            if (Math.Round(Days) >= 7)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date rang 7 Day')</script>", false);
                return;
            }
            string ddlDistrict = "";
            string ddlPhan = "";
            string ddlVillage = "";
            string ddlBlock = "";
            string ddlStatecode = "";
            foreach (ListItem item in ChkState.Items)
            {
                if (item.Selected)
                {

                    ddlStatecode += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlStatecode.Length > 0)
            {
                ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
            }
            foreach (ListItem item in chkDistrict.Items)
            {
                if (item.Selected)
                {

                    ddlDistrict += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlDistrict.Length > 0)
            {
                ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
            }
            foreach (ListItem item in chkBlock.Items)
            {
                if (item.Selected)
                {

                    ddlBlock += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlBlock.Length > 0)
            {
                ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
            }

            foreach (ListItem item in ddlPanchayat.Items)
            {
                if (item.Selected)
                {

                    ddlPhan += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlPhan.Length > 0)
            {
                ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
            }
            foreach (ListItem item in chkVillage.Items)
            {
                if (item.Selected)
                {

                    ddlVillage += "'" + item.Value + "'" + ",";


                }
            }
            string ss = "";
            if (ddlYear.SelectedIndex > 0)
            {
                ss = "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
            }
            if (ddlStatecode.Length > 0)
            {
                conditions1 = "     and  mst5Village.StateCode in(" + ddlStatecode + ") ";
            }
            if (ddlDistrict.Length > 0)
            {
                conditions1 = "   and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
            }
            if (ddlBlock.Length > 0)
            {
                conditions1 = "  and mst5Village.BlockCode in(" + ddlBlock + ") ";
            }
            if (ddlPhan.Length > 0)
            {
                conditions1 = " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
            }
            if (ddlVillage.Length > 0)
            {
                conditions1 = " and mst5Village.VillageCode in(" + ddlVillage + ") ";
            }

            conditions1 = ss + conditions1;

            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                conditions += " and ActivityDate between('" + afromDate + "') and ('" + aToDate + "') and UserEntry=3  and ApproveStatus='FC' ";
            }

            if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                conditions += " and ActivityDate between('" + afromDate + "') and ('" + aToDate + "') and UserEntry=3  and ApproveStatus='B' ";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 3)

            {
                conditions += " and ActivityDate between('" + afromDate + "') and '" + aToDate + "' and UserEntry=3  and ApproveStatus='I' ";
            }
            dtMain = objMain.rptActivityUpdateReports(afromDate, aToDate, conditions1 + conditions);
            #endregion
        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 3)
        {
            #region Month From to TO month
            string ddlDistrict = "";
            string ddlPhan = "";
            string ddlVillage = "";
            string ddlBlock = "";
            string ddlStatecode = "";
            foreach (ListItem item in ChkState.Items)
            {
                if (item.Selected)
                {

                    ddlStatecode += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlStatecode.Length > 0)
            {
                ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
            }
            foreach (ListItem item in chkDistrict.Items)
            {
                if (item.Selected)
                {

                    ddlDistrict += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlDistrict.Length > 0)
            {
                ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
            }
            foreach (ListItem item in chkBlock.Items)
            {
                if (item.Selected)
                {

                    ddlBlock += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlBlock.Length > 0)
            {
                ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
            }

            foreach (ListItem item in ddlPanchayat.Items)
            {
                if (item.Selected)
                {

                    ddlPhan += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlPhan.Length > 0)
            {
                ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
            }
            foreach (ListItem item in chkVillage.Items)
            {
                if (item.Selected)
                {

                    ddlVillage += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlYear.SelectedIndex > 0)
            {
                conditions1 = conditions1 + "  where  mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
            }
            if (ddlStatecode.Length > 0)
            {
                conditions1 = conditions1 + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
            }
            if (ddlDistrict.Length > 0)
            {
                conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
            }
            if (ddlBlock.Length > 0)
            {
                conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
            }
            if (ddlPhan.Length > 0)
            {
                conditions1 += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
            }
            if (ddlVillage.Length > 0)
            {
                conditions1 += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
            }

            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                conditions += "  and UserEntry=3  and ApproveStatus='FC' ";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                conditions += "  and UserEntry=3  and ApproveStatus='B' ";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
            {
                conditions += "  and UserEntry=3  and ApproveStatus='I' ";
            }
            dtMain = objMain.rptActivityUpdateReportsMonthly(conditions + conditions1);
            #endregion
        }

        if (dtMain.Rows.Count > 0)
        {
            #region School
            //for (int i = 0; i < dtMain.Rows.Count; i++)
            //{
            //    string Vill;
            //    string strGSS = "GSS";
            //    DataRow[] dr = dtMain.Select("School='" + strGSS + "'");
            //    if (dr.Length > 0)
            //    {


            //    }
            //    else
            //    {
            //        DataRow Item1;
            //        Item1 = dtMain.NewRow();
            //        dtMain.Rows.Add(Item1);


            //        Item1["SRNo"] = 1;
            //        Item1["School"] = "GSS";
            //    }

            //    string strGSS3 = "MM";
            //    DataRow[] dr3 = dtMain.Select("School='" + strGSS3 + "'");
            //    if (dr3.Length > 0)
            //    {


            //    }
            //    else
            //    {
            //        DataRow Item1;
            //        Item1 = dtMain.NewRow();
            //        dtMain.Rows.Add(Item1);


            //        Item1["SRNo"] = 2;
            //        Item1["School"] = "MM";
            //    }

            //    string strGSS4 = "Other Community Meeting 1";
            //    DataRow[] dr4 = dtMain.Select("School='" + strGSS4 + "'");
            //    if (dr4.Length > 0)
            //    {


            //    }
            //    else
            //    {
            //        DataRow Item1;
            //        Item1 = dtMain.NewRow();
            //        dtMain.Rows.Add(Item1);


            //        Item1["SRNo"] = 3;
            //        Item1["School"] = "Other Community Meeting 1";
            //    }

            //    string strGSS5 = "Other Community Meeting 2";
            //    DataRow[] dr5 = dtMain.Select("School='" + strGSS5 + "'");
            //    if (dr5.Length > 0)
            //    {


            //    }
            //    else
            //    {
            //        DataRow Item1;
            //        Item1 = dtMain.NewRow();
            //        dtMain.Rows.Add(Item1);


            //        Item1["SRNo"] = 4;
            //        Item1["School"] = "Other Community Meeting 2";
            //    }
            //    string strGSS56 = "Community Contact";
            //    DataRow[] dr6 = dtMain.Select("School='" + strGSS56 + "'");
            //    if (dr6.Length > 0)
            //    {


            //    }
            //    else
            //    {
            //        DataRow Item1;
            //        Item1 = dtMain.NewRow();
            //        dtMain.Rows.Add(Item1);

            //        Item1["SRNo"] = 5;
            //        Item1["School"] = "Community Contact";
            //    }


            //    string strGSS1 = "SIP Annual Data";
            //    DataRow[] dr1 = dtMain.Select("School='" + strGSS1 + "'");
            //    if (dr1.Length > 0)
            //    {


            //    }
            //    else
            //    {
            //        DataRow Item1;
            //        Item1 = dtMain.NewRow();
            //        dtMain.Rows.Add(Item1);

            //        Item1["SRNo"] = 6;

            //        Item1["School"] = "SIP Annual Data";
            //    }


            //    string strGSS123 = "Retention Annual Data";
            //    DataRow[] dr21 = dtMain.Select("School='" + strGSS123 + "'");
            //    if (dr21.Length > 0)
            //    {


            //    }
            //    else
            //    {
            //        DataRow Item1;
            //        Item1 = dtMain.NewRow();
            //        dtMain.Rows.Add(Item1);


            //        Item1["SRNo"] = 7;
            //        Item1["School"] = "Retention Annual Data";
            //    }
            //    string strGSS1231 = "SMC Meeting";
            //    DataRow[] dr211 = dtMain.Select("School='" + strGSS1231 + "'");
            //    if (dr211.Length > 0)
            //    {


            //    }
            //    else
            //    {
            //        DataRow Item1;
            //        Item1 = dtMain.NewRow();
            //        dtMain.Rows.Add(Item1);


            //        Item1["SRNo"] = 8;
            //        Item1["School"] = "SMC Meeting";
            //    }

            //    string strGSS12311 = "SAC Update";
            //    DataRow[] dr2111 = dtMain.Select("School='" + strGSS12311 + "'");
            //    if (dr2111.Length > 0)
            //    {


            //    }
            //    else
            //    {
            //        DataRow Item1;
            //        Item1 = dtMain.NewRow();
            //        dtMain.Rows.Add(Item1);


            //        Item1["SRNo"] = 9;
            //        Item1["School"] = "SAC Update";
            //    }
            //    string Game3 = "BalSabha";
            //    DataRow[] drGame3 = dtMain.Select("School='" + Game3 + "'");
            //    if (drGame3.Length > 0)
            //    {


            //    }
            //    else
            //    {
            //        DataRow Item1;
            //        Item1 = dtMain.NewRow();
            //        dtMain.Rows.Add(Item1);


            //        Item1["SRNo"] = 10;
            //        Item1["School"] = "BalSabha";
            //    }
            //    string Game41 = "LifeSkillGame";
            //    DataRow[] drGame41 = dtMain.Select("School='" + Game41 + "'");
            //    if (drGame41.Length > 0)
            //    {


            //    }
            //    else
            //    {
            //        DataRow Item1;
            //        Item1 = dtMain.NewRow();
            //        dtMain.Rows.Add(Item1);


            //        Item1["SRNo"] = 11;
            //        Item1["School"] = "LifeSkillGame";
            //    }
            //    string Game5 = "Learning Baseline";
            //    DataRow[] drGame5 = dtMain.Select("School='" + Game5 + "'");
            //    if (drGame5.Length > 0)
            //    {


            //    }
            //    else
            //    {
            //        DataRow Item1;
            //        Item1 = dtMain.NewRow();
            //        dtMain.Rows.Add(Item1);


            //        Item1["SRNo"] = 12;
            //        Item1["School"] = "Learning Baseline";
            //    }


            //    string CLt = "Learning Midline";
            //    DataRow[] drCLt = dtMain.Select("School='" + CLt + "'");
            //    if (drCLt.Length > 0)
            //    {


            //    }
            //    else
            //    {
            //        DataRow Item1;
            //        Item1 = dtMain.NewRow();
            //        dtMain.Rows.Add(Item1);


            //        Item1["SRNo"] = 13;
            //        Item1["School"] = "Learning Midline";
            //    }



            //    string CLt1 = "Learning Endline";
            //    DataRow[] drCLt1 = dtMain.Select("School='" + CLt1 + "'");
            //    if (drCLt1.Length > 0)
            //    {


            //    }
            //    else
            //    {
            //        DataRow Item1;
            //        Item1 = dtMain.NewRow();
            //        dtMain.Rows.Add(Item1);


            //        Item1["SRNo"] = 14;
            //        Item1["School"] = "Learning Endline";
            //    }


            //}

            if (dtMain.Rows.Count > 100)
            {
                GV_DynamicGrid2.DataSource = null;
                GV_DynamicGrid2.DataBind();
                ViewState["dt"] = dtMain;
                string aprove = "";
                if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
                {
                    aprove = "FC";
                }
                if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
                {
                    aprove = "BO";
                }
                if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
                {
                    aprove = "IO";
                }
                string str = "" + "ActivityDateWise" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + "_" + aprove + ".csv";

                ExportToCSVFileDateWise(dtMain, str);
            }
            else
            {

                ViewState["dt"] = dtMain;
                DataView dataview = dtMain.DefaultView;
                //dataview.Sort = "SRNo";
                //DataTable dt = dataview.ToTable();
                GV_DynamicGrid2.DataSource = dtMain;
                GV_DynamicGrid2.DataBind();



            }
            #endregion


        }
        else
        {
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }

    }

    protected void excel(DataTable dtexcel)
    {
        string rptnm = "EG_Report_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".xls";
        StringBuilder sbb = new StringBuilder();
        ExportToExcel exportToExcel = new ExportToExcel();
        exportToExcel.ExporttoExcel(dtexcel, sbb, rptnm);
    }
    private void ExportToExcelGridViewApprove(GridView Gv, DataTable table, string FileName, string Approve)
    {
        //Gv.DataSource = table;
        //Gv.DataBind();
        if (table.Rows.Count > 0)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">");
            string str = FileName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + "_" + Approve + ".xls";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + str + " ");
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
            foreach (DataRow row in table.Rows)
            {//write in new row
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }

    private void ExportToExcelGridViewApproveBasline(GridView Gv, DataTable table, string FileName, string Approve)
    {
        //Gv.DataSource = table;
        //Gv.DataBind();
        if (table.Rows.Count > 0)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">");
            string str = FileName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + str + " ");
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
            foreach (DataRow row in table.Rows)
            {//write in new row
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
    public void Export_To_Excel(object sender, EventArgs e)
    {
        if (ViewState["Button"].ToString() == "1")
        {
            DataTable dt = Session["DtTrarget"] as DataTable;
            ExporttoExcel(GV_DynamicGrid2, dt, "Report");
        }

        if (ViewState["Button"].ToString() == "9000")
        {
            DataTable dt = Session["DtTrargetC"] as DataTable;
            GenerateExcelNew("ContactBlockwisesummary");
        }
        if (ViewState["Button"].ToString() == "9005")
        {
            DataTable dt = Session["ClusteTrargetC"] as DataTable;
            GenerateExcelOutReach("BlockwiseOutreach");
        }
        if (ViewState["Button"].ToString() == "9007")
        {
            DataTable dt = Session["ClusteTrargetCNew"] as DataTable;
            GenerateExcelOutReachNew("ClusterwiseOutreach");
        }

        if (ViewState["Button"].ToString() == "9001")
        {
            DataTable dt = ViewState["dt"] as DataTable;
            GenerateExcelNewCluster("ContactClusterwisesummary");
        }
        if (ViewState["Button"].ToString() == "890")
        {
            DataTable dt = ViewState["dt"] as DataTable;
            GenerateExcel(dt, "Report");
        }

        if (ViewState["Button"].ToString() == "11")
        {
            DataTable dt = ViewState["dt"] as DataTable;
            ExportToExcelGridView(GV_DynamicGrid2, dt, "Report");
        }
        if (ViewState["Button"].ToString() == "560")
        {
            string FileName = "";
            Int32 Contact = Convert.ToInt32(ddlContact.SelectedValue);
            if (Contact == 0)
            {
                FileName = "EnrDailyStatus";

            }
            if (Contact == 1)
            {
                FileName = "IneligibleContactStatus";

            }
            if (Contact == 2)
            {
                FileName = "ReadyForEnrolledStatus";

            }
            if (Contact == 3)
            {
                FileName = "EnrolledContactStatus";

            }
            if (Contact == 4)
            {
                FileName = "EnrolledInfoByParentStatus";

            }
            DataTable dt = ViewState["dt"] as DataTable;
            ExportToCSVFile(dt, FileName);
        }
        if (ViewState["Button"].ToString() == "556")
        {
            DataTable dt = ViewState["dt"] as DataTable;
            string aprove = "";
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                aprove = "FC";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                aprove = "BO";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
            {
                aprove = "IO";
            }

            ExportToExcelGridViewApprove(GV_DynamicGrid2, dt, "SchoolActivityRawData", aprove);
        }

        if (ViewState["Button"].ToString() == "776")
        {
            DataTable dt = ViewState["dt"] as DataTable;
            string aprove = "";
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                aprove = "FC";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                aprove = "BO";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
            {
                aprove = "IO";
            }

            ExportToExcelGridViewApprove(GV_DynamicGrid2, dt, "SchoolContactRawData", aprove);
        }
        if (ViewState["Button"].ToString() == "558")
        {
            string aprove = "";
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                aprove = "FC";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                aprove = "BO";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
            {
                aprove = "IO";
            }
            DataTable dt = ViewState["dt"] as DataTable;
            ExportToExcelGridViewApprove(GV_DynamicGrid2, dt, "VillageActivityRawData", aprove);
        }

        if (ViewState["Button"].ToString() == "5588")
        {
            string aprove = "";
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                aprove = "FC";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                aprove = "BO";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
            {
                aprove = "IO";
            }
            DataTable dt = ViewState["dt"] as DataTable;
            ExportToExcelGridViewApprove(GV_DynamicGrid2, dt, "VillageActivityGSSRawData", aprove);
        }
        if (ViewState["Button"].ToString() == "5589")
        {
            string aprove = "";
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                aprove = "FC";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                aprove = "BO";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
            {
                aprove = "IO";
            }
            DataTable dt = ViewState["dt"] as DataTable;
            ExportToExcelGridViewApprove(GV_DynamicGrid2, dt, "VillageActivityMMRawData", aprove);
        }
        if (ViewState["Button"].ToString() == "572")
        {
            string aprove = "";
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                aprove = "FC";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                aprove = "BO";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
            {
                aprove = "IO";
            }
            DataTable dt = ViewState["dt"] as DataTable;
            ExportToExcelGridViewApproveBasline(GV_DynamicGrid2, dt, "BaselineActivityRawData", aprove);
        }

        if (ViewState["Button"].ToString() == "559")
        {
            string aprove = "";
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                aprove = "FC";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                aprove = "BO";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
            {
                aprove = "IO";
            }
            DataTable dt = ViewState["dt"] as DataTable;
            ExportToExcelGridViewApprove(GV_DynamicGrid2, dt, "SMCRawData", aprove);
        }
        if (ViewState["Button"].ToString() == "972")
        {
            string aprove = "";
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                aprove = "FC";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                aprove = "BO";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
            {
                aprove = "IO";
            }
            DataTable dt = ViewState["dt"] as DataTable;
            ExportToExcelGridViewApprove(GV_DynamicGrid2, dt, "SMCMeetingRawData", aprove);
        }
        if (ViewState["Button"].ToString() == "2")
        {
            DataTable dt = ViewState["dt"] as DataTable;
            ExporttoExcel(DGV_Report, dt, "Report");
        }
        if (ViewState["Button"].ToString() == "778")
        {
            DataTable dt = ViewState["dt"] as DataTable;
            ExportToExcelGridView(GV_DynamicGrid2, dt, "ReasonReport");
        }

        if (ViewState["Button"].ToString() == "12")
        {
            DataTable dt = ViewState["dt"] as DataTable;
            ExportToExcelGridView(DGV_Report, dt, "Report");
        }


        if (ViewState["Button"].ToString() == "3")
        {
            DataTable dt = ViewState["dt"] as DataTable;

            string aprove = "";
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                aprove = "FC";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                aprove = "BO";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
            {
                aprove = "IO";
            }
            string str = "" + "ActivityWeekWise" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + "_" + aprove + ".xls";
            ExportToExcelGridViewFIname(gvWeaklly, dt, str);
        }
        if (ViewState["Button"].ToString() == "4")
        {
            string aprove = "";
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                aprove = "FC";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                aprove = "BO";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
            {
                aprove = "IO";
            }

            DataTable dt = ViewState["dt"] as DataTable;
            ExportToExcelGridViewDateWise(GV_DynamicGrid2, dt, "ActivityDateWise", aprove);

        }
        if (ViewState["Button"].ToString() == "16")
        {
            DataTable dt = ViewState["dt"] as DataTable;
            ExporttoExcel(GV_DynamicGrid2, dt, "Report");
        }
        if (ViewState["Button"].ToString() == "316")
        {
            DataTable dt = ViewState["dt"] as DataTable;
            ExportToExcelGridView(GV_DynamicGrid2, dt, "Report");
        }

        if (ViewState["Button"].ToString() == "216")
        {
            DataTable dt = ViewState["dt"] as DataTable;
            ExportToExcelGridView(GV_DynamicGrid2, dt, "Report");
        }
        if (ViewState["Button"].ToString() == "5216")
        {
            string GKP = "";
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {

                GKP = "GKPRawDataFCWise";
            }
            else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {

                GKP = "GKPRawDataBOWise";
            }
            else
            {

                GKP = "GKPRawDataIOWise";
            }
            DataTable dt = ViewState["dt"] as DataTable;
            ExportToExcelGridView(GV_DynamicGrid2, dt, GKP);
        }


        if (ViewState["Button"].ToString() == "999")
        {
            DataTable dt = ViewState["dt"] as DataTable;
            ExportToExcelGridView(GV_DynamicGrid2, dt, "BalsabhaSummary");
        }
        if (ViewState["Button"].ToString() == "9991")
        {
            DataTable dt = ViewState["dt"] as DataTable;
            ExportToExcelGridView(GV_DynamicGrid2, dt, "BalsabhaSummary");
        }
        if (ViewState["Button"].ToString() == "9992")
        {
            DataTable dt = ViewState["dt"] as DataTable;
            ExportToExcelGridView(GV_DynamicGrid2, dt, "BalsabhaSummary");
        }
        if (ViewState["Button"].ToString() == "9998")
        {
            DataTable dt = ViewState["dt"] as DataTable;
            ExportToExcelGridView(GV_DynamicGrid2, dt, "BalsabhaRawdata");
        }


        if (ViewState["Button"].ToString() == "14")
        {
            DataTable dt = ViewState["dt"] as DataTable;
            GenerateExcelNew(dt);
        }

        if (ViewState["Button"].ToString() == "578")
        {
            DataTable dt = ViewState["dt"] as DataTable;
            string aprove = "";
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                aprove = "FC";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                aprove = "BO";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
            {
                aprove = "IO";
            }

            ExportToExcelGridViewApprove(GV_DynamicGrid2, dt, "ActivityLifeSkill", aprove);
        }
    }
    private void ExportGridToExcel(GridView Gv, string FileName1)
    {
        try
        {
            //Gv.AllowPaging = false;
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = FileName1 + "_" + DateTime.Now + ".xls";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            //Gv.AllowPaging = false;
            Gv.GridLines = GridLines.Both;
            Gv.HeaderStyle.Font.Bold = true;
            Gv.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.Flush();
            Response.End();

        }
        catch (Exception)
        {

            throw;
        }

    }
    protected void ExporttoExcel(GridView Gv, DataTable table, string FileName)
    {
        Response.Clear();
        Response.Buffer = true;
        string str = "Report";
        string Fullfilename = "" + str + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";

        //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + " ");
        Response.AddHeader("content-disposition", "attachment;filename=" + Fullfilename + " ");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //To Export all pages

            //GridView gv = new GridView();
            //gv.AllowPaging = false;

            //gv.DataSource = table;
            //gv.DataBind();

            foreach (TableCell cell in Gv.HeaderRow.Cells)
            {
                cell.BackColor = Gv.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in Gv.Rows)
            {

                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = Gv.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = Gv.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            Gv.RenderControl(hw);
            //style to format numbers to string
            string style = @"<style> .textmode { } </style> <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    private void ExportToExcelGridViewFIname(GridView Gv, DataTable table, string str)
    {
        //Gv.DataSource = table;
        //Gv.DataBind();
        if (table.Rows.Count > 0)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">");

            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + str + " ");
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
            foreach (DataRow row in table.Rows)
            {//write in new row
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }

    protected void ExporttoExcelSip(GridView Gv, DataTable table, string FileName)
    {
        Response.Clear();
        Response.Buffer = true;
        string str = "Report";
        string Fullfilename = "" + str + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";

        //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + " ");
        Response.AddHeader("content-disposition", "attachment;filename=" + Fullfilename + " ");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //To Export all pages

            //GridView gv = new GridView();
            //   Gv.AllowPaging = false;

            //gv.DataSource = table;
            //gv.DataBind();

            foreach (TableCell cell in Gv.HeaderRow.Cells)
            {
                cell.BackColor = Gv.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in Gv.Rows)
            {

                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = Gv.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = Gv.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            Gv.RenderControl(hw);
            //style to format numbers to string
            string style = @"<style> .textmode { } </style> <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    private void ExportToExcelGridView(GridView Gv, DataTable table, string FileName)
    {
        //Gv.DataSource = table;
        //Gv.DataBind();
        if (table.Rows.Count > 0)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">");
            string str = FileName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + str + " ");
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
            foreach (DataRow row in table.Rows)
            {//write in new row
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }


    private void ExportToExcelGridViewDateWise(GridView Gv, DataTable table, string FileName, string Approve)
    {
        //Gv.DataSource = table;
        //Gv.DataBind();
        if (table.Rows.Count > 0)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">");

            string str = "" + FileName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + "_" + Approve + ".xls";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + str + " ");
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
            foreach (DataRow row in table.Rows)
            {//write in new row
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    private void GenerateExcelNew(DataTable dt)
    {
        string abc1 = "";
        string abc2 = "";
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");
        string Fullfilename = "" + "Report" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        // Int32 EmpID = Convert.ToInt32(Contx.Request["empid"]);



        HttpContext.Current.Response.Write("<table style='border:.5pt solid windowtext;'>");





        if (dt.Rows.Count > 0)
        {





        }

        String HeaderStyle = "border:.5pt solid windowtext; font-weight:700;background:#D9D9D9;";
        HttpContext.Current.Response.Write("    <tr style='font-width:bold;'>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>#</td>");
        //   HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>TimeSheet ID</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>District Name</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>District Code</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Block Name</td>");

        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Block Code</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Cluster Code</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Cluster Name</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Panchayat Name</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Panchayat Code</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Village Name</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Village Code</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Name</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>DISECODE</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>OutCome</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Annual</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Apr To Jun</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Jul-Sep</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Oct-Dec</td>");
        HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Jan-Mar</td>");

        //HttpContext.Current.Response.Write("         <td style='" + HeaderStyle + "'>Version No</td>");
        HttpContext.Current.Response.Write("    </tr>");



        String DataStyle = "border:.5pt solid windowtext;";
        String DateTimeStyle = "mso-number-format:\"mm/dd/yyyy hh:mm AM/PM \";";
        String DateStyle = "mso-number-format:\"mm/dd/yyyy \";";
        String TimeStyle = "mso-number-format:\"hh:mm AM/PM \";";
        String A1 = "background-color:Green;border:.5pt solid;";
        String A2 = "background-color:Orange;border:.5pt solid;";
        String A3 = "background-color:Red;border:.5pt solid;";
        String A4 = "background-color:Blue;border:.5pt solid;";





        var i = 0;
        double distance = 0;
        double Enddistance = 0;
        for (i = 0; i < dt.Rows.Count; i++)
        {

            var RowStyle = DataStyle;



            HttpContext.Current.Response.Write("<tr>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + (i + 1) + "</td>");
            //retStr += "<td style='" + RowStyle + "'>" + mData.TimeSheet_ID + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["DistrictName"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["DistrictCode"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["BlockName"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["BlockCode"].ToString() + "</td>");

            // HttpContext.Current.Response.Write("<td style='" + RowStyle + DateStyle + "'>" + Convert.ToDateTime(dt.Rows[i]["Date"].ToString()).ToString("dd/MM/yyy") + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["ClusterCode"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["ClusterName"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["PanchayatName"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["PanchayatCode"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["VillageName"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["VillageCode"].ToString() + "</td>");

            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["Name"].ToString() + "</td>");
            //   HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["TimeSheet_StartTime"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["DiseCode"].ToString() + "</td>");
            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i]["DocName"].ToString() + "</td>");



            if (dt.Rows[i]["Annual"].ToString() == "0")
            {
                HttpContext.Current.Response.Write("<td style='" + DataStyle + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Annual"].ToString() == "1")
            {
                HttpContext.Current.Response.Write("<td style='" + A1 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Annual"].ToString() == "2")
            {
                HttpContext.Current.Response.Write("<td style='" + A2 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Annual"].ToString() == "3")
            {
                HttpContext.Current.Response.Write("<td style='" + A3 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Annual"].ToString() == "4")
            {
                HttpContext.Current.Response.Write("<td style='" + A4 + "'>" + "" + "</td>");

            }


            if (dt.Rows[i]["Q1"].ToString() == "0")
            {
                HttpContext.Current.Response.Write("<td style='" + DataStyle + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q1"].ToString() == "1")
            {
                HttpContext.Current.Response.Write("<td style='" + A1 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q1"].ToString() == "2")
            {
                HttpContext.Current.Response.Write("<td style='" + A2 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q1"].ToString() == "3")
            {
                HttpContext.Current.Response.Write("<td style='" + A3 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q1"].ToString() == "4")
            {
                HttpContext.Current.Response.Write("<td style='" + A4 + "'>" + "" + "</td>");

            }


            if (dt.Rows[i]["Q2"].ToString() == "0")
            {
                HttpContext.Current.Response.Write("<td style='" + DataStyle + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q2"].ToString() == "1")
            {
                HttpContext.Current.Response.Write("<td style='" + A1 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q2"].ToString() == "2")
            {
                HttpContext.Current.Response.Write("<td style='" + A2 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q2"].ToString() == "3")
            {
                HttpContext.Current.Response.Write("<td style='" + A3 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q2"].ToString() == "4")
            {
                HttpContext.Current.Response.Write("<td style='" + A4 + "'>" + "" + "</td>");

            }


            if (dt.Rows[i]["Q3"].ToString() == "0")
            {
                HttpContext.Current.Response.Write("<td style='" + DataStyle + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q3"].ToString() == "1")
            {
                HttpContext.Current.Response.Write("<td style='" + A1 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q3"].ToString() == "2")
            {
                HttpContext.Current.Response.Write("<td style='" + A2 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q3"].ToString() == "3")
            {
                HttpContext.Current.Response.Write("<td style='" + A3 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q3"].ToString() == "4")
            {
                HttpContext.Current.Response.Write("<td style='" + A4 + "'>" + "" + "</td>");

            }

            if (dt.Rows[i]["Q4"].ToString() == "0")
            {
                HttpContext.Current.Response.Write("<td style='" + DataStyle + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q4"].ToString() == "1")
            {
                HttpContext.Current.Response.Write("<td style='" + A1 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q4"].ToString() == "2")
            {
                HttpContext.Current.Response.Write("<td style='" + A2 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q4"].ToString() == "3")
            {
                HttpContext.Current.Response.Write("<td style='" + A3 + "'>" + "" + "</td>");

            }
            if (dt.Rows[i]["Q4"].ToString() == "4")
            {
                HttpContext.Current.Response.Write("<td style='" + A4 + "'>" + "" + "</td>");

            }

            Enddistance = 0;
            HttpContext.Current.Response.Write("</tr>");

        }

        DataStyle += "background-color:yellow;";

        HttpContext.Current.Response.Write("</table>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();


        //flushExcel(totDays.ToString());

        //var lq=dataList.Select(fld=>fld.TimeSheet_EndTime.

    }
    protected void SACquarterWise_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "216";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getSACReportQuter(1);


    }
    protected void SACquarter_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "216";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getSACReportQuter(2);


    }
    protected void AnnaualFCReport_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "216";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        LoadSMCSummary(2);


    }

    protected void AnnaualSMCMeetingSchoolSummary(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        GV_DynamicGrid2.Visible = true;
        gvWeaklly.Visible = false;
        ViewState["Button"] = "216";
        btnexcel.Visible = true;
        gvQuerltyAnnual.Visible = false;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        LoadSMCSummary(1);


    }
    public void getSACReportQuter(Int32 Flag)
    {
        conditions = "";


        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }

        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }

        if (ddlScholl.SelectedIndex > 0)
        {
            conditions += " and mstSchool.SchoolCode ='" + ddlScholl.SelectedValue + "' ";
        }

        string schoolCodeAprove = "";
        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
        {

            schoolCodeAprove = "   UserEntry=3  and ApproveStatus='FC' ";
        }
        else if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
        {

            schoolCodeAprove = "   UserEntry=3  and ApproveStatus='B' ";
        }
        else
        {

            schoolCodeAprove = "   UserEntry=3  and ApproveStatus='I' ";
        }
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con",conditions),
            new SqlParameter("@Flag",Flag),
             new SqlParameter("@conAprove",schoolCodeAprove),

        };
        DataTable dataTable = null;


        //dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivitySACReportQuterWiseNew]", cmdParameters);
        dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivitySACReportQuterWise2019]", cmdParameters);

        ViewState["dt"] = dataTable;
        if (dataTable.Rows.Count > 0)
        {
            if (dataTable.Rows.Count > 100)
            {
                ExportToCSVFile(dataTable, "SACquarterStatus");
            }
            else
            {
                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();
            }

            return;
        }
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();
    }

    public void AnnaualFCReport(Int32 Flag)
    {
        conditions = "";


        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }

        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }

        if (ddlScholl.SelectedIndex > 0)
        {
            conditions += " and mstSchool.SchoolCode ='" + ddlScholl.SelectedValue + "' ";
        }


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Villagecode",conditions),


        };
        DataTable dataTable = null;


        dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[GetAnualPlanFCWiseReport]", cmdParameters);

        ViewState["dt"] = dataTable;
        if (dataTable.Rows.Count > 0)
        {
            if (dataTable.Rows.Count > 1000)
            {

                ExportToCSVFile(dataTable, "SAquarterStatus");
            }
            else
            {
                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();
            }

            return;
        }
        GV_DynamicGrid2.DataSource = null;
        GV_DynamicGrid2.DataBind();


    }

    private void SIPDetailsNew(DataTable dt, string FIleName)
    {
        try
        {




            string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
            string filepath = "";
            XLWorkbook wb = new XLWorkbook();
            wb = new XLWorkbook(StartupPath + "\\SIPDetailsNew1.xlsx");
            var ws = wb.Worksheet(1);

            ws.Cell(5, 1).InsertData(dt.Rows);
            Int32 ii = Convert.ToInt32(dt.Rows.Count) + 2;
            string str = "A5:AV" + ii;

            ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);



            filepath = StartupPath + "\\SIPDetails" + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
            wb.SaveAs(filepath);
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
            Response.WriteFile(filepath);

            Response.End();
            if (File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }

        }
        catch
        {

            throw;
        }


    }

    private void SIPDetailsDaaa(DataTable dt, string FIleName)
    {
        try
        {




            string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
            string filepath = "";
            XLWorkbook wb = new XLWorkbook();
            wb = new XLWorkbook(StartupPath + "\\SIPstatus.xlsx");
            var ws = wb.Worksheet(1);

            ws.Cell(4, 1).InsertData(dt.Rows);
            Int32 ii = Convert.ToInt32(dt.Rows.Count) + 2;
            string str = "A5:AV" + ii;

            ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);



            filepath = StartupPath + "\\SIPstatusDetails" + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
            wb.SaveAs(filepath);
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
            Response.WriteFile(filepath);

            Response.End();
            if (File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }

        }
        catch
        {

            throw;
        }


    }

    private void GenerateExcel(DataTable dt, string FIleName)
    {
        try
        {





            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.ClearContent();
            //HttpContext.Current.Response.ClearHeaders();
            //HttpContext.Current.Response.Buffer = true;
            //HttpContext.Current.Response.ContentType = "application/ms-excel";
            //HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            //string Fullfilename = "" + FIleName + "_" + "" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


            //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");

            //HttpContext.Current.Response.Charset = "utf-8";
            //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");


            string Fullfilename1 = "" + "SIPDetails" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";
            string fileName = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + Fullfilename1 + "");
            StreamWriter sw = new StreamWriter(fileName, false);
            sw.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            sw.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");


            sw.Write("<table  >");

            sw.Write("<tr>");


            sw.Write("<td colspan='48' style='text-align:center;font:bold;border:.5pt solid windowtext;'>" + "SIP" + "   </td>");
            sw.Write("</tr>");
            sw.Write("<tr>");
            sw.Write("<td  colspan='12' style='text-align:center;font:bold;border:.5pt solid windowtext;'>" + "" + "" + "" + "</td>");
            sw.Write("<td  colspan='12' style='text-align:center;font:bold;border:.5pt solid windowtext;'>" + "#Critical SIP" + "" + "" + "</td>");
            sw.Write("<td  colspan='18' style='text-align:center;font:bold;border:.5pt solid windowtext;'>" + "#Other Critical SIP" + "" + "" + "</td>");
            sw.Write("<td  colspan='3' style='text-align:center;font:bold;border:.5pt solid windowtext;'>" + "#Other SIP" + "" + "" + "</td>");
            sw.Write("<td  colspan='3' style='text-align:center;font:bold;border:.5pt solid windowtext;'>" + "#GKP" + "" + "" + "</td>");
            sw.Write("</tr>");

            sw.Write("<tr>");


            sw.Write("<td  colspan='12' style='text-align:center;font:bold;border:.5pt solid windowtext;'>" + "District Profile" + "" + "" + "</td>");
            //sw.Write("<td colspan='13'> </td>");
            sw.Write("<td  colspan='3' style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Girl's Toilet</td>");
            sw.Write("<td  colspan='3' style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Drinking Water</td>");
            sw.Write("<td  colspan='3' style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Kitchen Shed</td>");
            sw.Write("<td  colspan='3' style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>PTR (Pupil Teacher Ratio)</td>");
            sw.Write("<td  colspan='3' style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Playground	</td>");
            sw.Write("<td  colspan='3' style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Electricity</td>");
            sw.Write("<td  colspan='3' style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Health Check UP</td>");
            sw.Write("<td  colspan='3' style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>PCR (Pupil Children Ratio)	</td>");

            sw.Write("<td  colspan='3' style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Swings and Sliders	</td>");
            sw.Write("<td  colspan='3' style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Boundary Wall	</td>");

            sw.Write("<td  colspan='3' style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Maintenance</td>");
            sw.Write("<td  colspan='3' style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>GKP	</td>");

            sw.Write("</tr>");



            String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

            sw.Write("<tr style='font-width:bold;'>");
            // sw.Write("<td></td>");
            Int32 iCount = 47;


            for (int Index = 0; Index <= iCount; Index++)
            {
                var firstCell = GvSip.HeaderRow.Cells[Index];


                sw.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'>" + firstCell.Text.Trim() + "</th>");


            }
            sw.Write("</tr>");



            String DataStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;";
            String DataGrey = "border:.1pt dotted windowtext; background:#dddddd; font-weight:100; font-size:9pt;";
            String dataBl = "border:.1pt dotted windowtext; font-weight:700; font-size:9pt;";
            int intMonth = DateTime.Now.Month;
            int intYear = DateTime.Now.Year;

            int i = 0; String day = "";
            Int32 i12 = 0; Int32 i13 = 0; Int32 i14 = 0; Int32 i15 = 0; Int32 i16 = 0; Int32 i17 = 0; Int32 i18 = 0; Int32 i19 = 0; Int32 i20 = 0; Int32 i21 = 0;
            Int32 i22 = 0; Int32 i23 = 0; Int32 i24 = 0; Int32 i25 = 0; Int32 i26 = 0; Int32 i27 = 0; Int32 i28 = 0; Int32 i29 = 0; Int32 i30 = 0; Int32 i31 = 0;
            Int32 i32 = 0; Int32 i33 = 0; Int32 i34 = 0; Int32 i35 = 0; Int32 i36 = 0; Int32 i37 = 0; Int32 i38 = 0; Int32 i39 = 0; Int32 i40 = 0;
            Int32 i41 = 0; Int32 i42 = 0; Int32 i43 = 0; Int32 i44 = 0; Int32 i45 = 0; Int32 i46 = 0; Int32 i47 = 0; Int32 i48 = 0; Int32 i49 = 0;
            for (i = 0; i < dt.Rows.Count; i++)
            {
                var RowStyle = DataStyle;

                sw.Write("<tr>");
                //sw.Write("<td >Direct</td>");
                for (int c = 0; c < dt.Columns.Count; c++)
                {

                    sw.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c].ToString() + "</td>");

                }

                i12 += Convert.ToInt32(dt.Rows[i]["Annual"].ToString());
                i13 += Convert.ToInt32(dt.Rows[i]["GPrepared"].ToString());
                i14 += Convert.ToInt32(dt.Rows[i]["CPrepared"].ToString());
                i15 += Convert.ToInt32(dt.Rows[i]["Annual1"].ToString());
                i16 += Convert.ToInt32(dt.Rows[i]["PDriking"].ToString());
                i17 += Convert.ToInt32(dt.Rows[i]["CDriking"].ToString());
                i18 += Convert.ToInt32(dt.Rows[i]["Annual2"].ToString());
                i19 += Convert.ToInt32(dt.Rows[i]["PKitchen"].ToString());
                i20 += Convert.ToInt32(dt.Rows[i]["CKitchen"].ToString());
                i21 += Convert.ToInt32(dt.Rows[i]["Annual26"].ToString());
                i22 += Convert.ToInt32(dt.Rows[i]["PPtrDriking"].ToString());
                i23 += Convert.ToInt32(dt.Rows[i]["CPtrDriking"].ToString());

                i24 += Convert.ToInt32(dt.Rows[i]["Annual3"].ToString());

                i25 += Convert.ToInt32(dt.Rows[i]["PPlayground"].ToString());

                i26 += Convert.ToInt32(dt.Rows[i]["CPlayground"].ToString());

                i27 += Convert.ToInt32(dt.Rows[i]["Annual4"].ToString());

                i28 += Convert.ToInt32(dt.Rows[i]["PElectricity"].ToString());

                i29 += Convert.ToInt32(dt.Rows[i]["CElectricity"].ToString());

                i30 += Convert.ToInt32(dt.Rows[i]["Annual5"].ToString());

                i31 += Convert.ToInt32(dt.Rows[i]["PHealthCheckUP"].ToString());


                i32 += Convert.ToInt32(dt.Rows[i]["CHealthCheckUP"].ToString());
                i33 += Convert.ToInt32(dt.Rows[i]["Annual6"].ToString());


                i34 += Convert.ToInt32(dt.Rows[i]["PClassroom"].ToString());

                i35 += Convert.ToInt32(dt.Rows[i]["CClassroom"].ToString());
                //   i36 += Convert.ToInt32(dt.Rows[i]["Annual7"].ToString());

                //i37 += Convert.ToInt32(dt.Rows[i]["PGkp"].ToString());
                //i38 += Convert.ToInt32(dt.Rows[i]["CGkp"].ToString());
                i39 += Convert.ToInt32(dt.Rows[i]["Annual8"].ToString());
                i40 += Convert.ToInt32(dt.Rows[i]["PSwingsandSliders"].ToString());
                i41 += Convert.ToInt32(dt.Rows[i]["CSwingsandSliders"].ToString());
                i42 += Convert.ToInt32(dt.Rows[i]["Annual9"].ToString());
                i43 += Convert.ToInt32(dt.Rows[i]["PBoundaryWall"].ToString());
                i44 += Convert.ToInt32(dt.Rows[i]["CBoundaryWall"].ToString());
                i45 += Convert.ToInt32(dt.Rows[i]["Annual0"].ToString());
                i46 += Convert.ToInt32(dt.Rows[i]["POthersSip"].ToString());
                i47 += Convert.ToInt32(dt.Rows[i]["COthersSip"].ToString());



                sw.Write("</tr>");
            }

            sw.Write("<tr>");
            sw.Write("<td style='" + DataStyle + "'>" + "" + "</td>");

            sw.Write("</tr>");
            for (i = 0; i < 1; i++)
            {
                var RowStyle = DataStyle;

                sw.Write("<tr>");
                //sw.Write("<td >Direct</td>");

                sw.Write("<td style='" + RowStyle + "'>" + "" + "</td>");

                sw.Write("<td style='" + RowStyle + "'>" + "" + "</td>");


                sw.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + "" + "</td>");

                sw.Write("<td style='" + RowStyle + "'>" + "Total" + "</td>");

                sw.Write("<td style='" + RowStyle + "'>" + i12 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i13 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i14 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i15 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i16 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i17 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i18 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i19 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i20 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i21 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i22 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i23 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i24 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i25 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i26 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i27 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i28 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i29 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i30 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i31 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i32 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i33 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i34 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i35 + "</td>");
                //   sw.Write("<td style='" + RowStyle + "'>" + i36 + "</td>");
                //sw.Write("<td style='" + RowStyle + "'>" + i37 + "</td>");
                //sw.Write("<td style='" + RowStyle + "'>" + i38 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i39 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i40 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i41 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i42 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i43 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i44 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i45 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i46 + "</td>");
                sw.Write("<td style='" + RowStyle + "'>" + i47 + "</td>");

                sw.Write("<td style='" + RowStyle + "'>" + 0 + "</td>");

                sw.Write("<td style='" + RowStyle + "'>" + 0 + "</td>");

                sw.Write("<td style='" + RowStyle + "'>" + 0 + "</td>");



                sw.Write("</tr>");
            }
            DataStyle = "border:.3pt solid windowtext; font-size:9pt;";

            //     sw.Write("</tr>");


            //  DataStyle += "background-color:yellow;";

            int SipP = i13 + i16 + i19 + i22;
            int SipC = i14 + i17 + i20 + i23;

            int OtherSipP = i25 + i28 + i34 + i37 + i40 + i43 + i46;
            int OtherSipC = i26 + i29 + i32 + i35 + i38 + i41 + i44;
            sw.Write("</table>");
            sw.Write("<table  style='text-align:center;font:bold;border:.5pt solid windowtext;'>");

            sw.Write("<tr>");
            sw.Write("<td  style='text-align:center;font:bold;border:.5pt solid windowtext;'>" + "Critical SIP Prepared " + "</td>");
            sw.Write("<td  style='text-align:center;font:bold;border:.5pt solid windowtext;'>" + SipP + "</td>");
            sw.Write("</tr>");

            sw.Write("<tr>");
            sw.Write("<td  style='text-align:center;font:bold;border:.5pt solid windowtext;'>" + "Critical SIP Completed " + "</td>");
            sw.Write("<td  style='text-align:center;font:bold;border:.5pt solid windowtext;'>" + SipC + "</td>");
            sw.Write("</tr>");


            sw.Write("<tr>");
            sw.Write("<td  style='text-align:center;font:bold;border:.5pt solid windowtext;'>" + "Other SIP Prepared " + "</td>");
            sw.Write("<td  style='text-align:center;font:bold;border:.5pt solid windowtext;'>" + OtherSipP + "</td>");
            sw.Write("</tr>");

            sw.Write("<tr>");
            sw.Write("<td  style='text-align:center;font:bold;border:.5pt solid windowtext;'>" + "Other SIP Completed " + "</td>");
            sw.Write("<td  style='text-align:center;font:bold;border:.5pt solid windowtext;'>" + OtherSipC + "</td>");
            sw.Write("</tr>");


            sw.Write("</table>");
            sw.Flush();
            sw.Close();


            FileStream fs = null;//, fs2=null;
            try
            {
                string path1 = Fullfilename1;
                string foldername = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + path1 + "");
                string datafolder = path1.Substring(0, path1.Length - 4);
                //  string[] file = Directory.GetFiles(foldername);
                string path = foldername;
                string fullPath = Request.MapPath("~/DataBackup/" + datafolder + "" + ".zip");
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(foldername, "");
                    //    zip.AddFiles(file, foldername);
                    zip.Save(Server.MapPath(Comman.GetImagePath("DataBackupPath")) + "/" + datafolder + "" + ".zip");
                }



                HttpResponse Response = HttpContext.Current.Response; Response.Clear(); Response.ClearHeaders(); Response.Charset = "UTF-8";
                fs = File.Open(fullPath, FileMode.Open);
                byte[] bytBytes = new byte[(fs.Length)];
                fs.Read(bytBytes, 0, (int)fs.Length);
                fs.Close();
                Response.AddHeader("Content-disposition", "attachment; filename=" + datafolder + "" + ".zip");
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(bytBytes);






                if (File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                if (File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.End();
            }

            catch (System.Exception ex)
            {
                //  Server.Transfer("default.aspx", false);
                Response.Clear();

                //string mmsg = ex.Message;
                //showEXPMessages("(crateZip)  " + mmsg); //showMessages(mmsg);
            }
            finally
            {
                fs.Dispose();
                Response.Clear();

            }


        }
        catch
        {

            throw;
        }


    }

    private void GenerateExcelSAC(DataTable dt, string FIleName, int Flag)
    {
        try
        {





            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.ClearContent();
            //HttpContext.Current.Response.ClearHeaders();
            //HttpContext.Current.Response.Buffer = true;
            //HttpContext.Current.Response.ContentType = "application/ms-excel";
            //HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            //string Fullfilename = "" + FIleName + "_" + "" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


            //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");

            //HttpContext.Current.Response.Charset = "utf-8";
            //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            string str = "";
            if (Flag == 2)
            {
                str = "SMCMeetingBlockSummary";
            }
            else
            {
                str = "SMCMeetingSchoolSummary";
            }

            string Fullfilename1 = "" + str + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + "_" + FIleName + ".xls";
            string fileName = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + Fullfilename1 + "");
            StreamWriter sw = new StreamWriter(fileName, false);
            sw.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            sw.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");


            sw.Write("<table  >");

            sw.Write("<tr>");

            if (Flag == 2)
            {
                sw.Write("<td colspan='8' style='text-align:center;font:bold;border:.5pt solid windowtext;'>" + "SMC Meeting Block Summary" + "   </td>");
            }
            else
            {
                sw.Write("<td colspan='14' style='text-align:center;font:bold;border:.5pt solid windowtext;'>" + "SMC Meeting School Summary" + "   </td>");

            }
            sw.Write("</tr>");
            sw.Write("<tr>");
            if (Flag == 2)
            {
                sw.Write("<td colspan='8' style='text-align:left;font:bold;border:.5pt solid windowtext;'>" + "Current Date : " + DateTime.Now + "" + "   </td>");
            }
            else
            {
                sw.Write("<td colspan='14' style='text-align:left;font:bold;border:.5pt solid windowtext;'>" + "Current Date : " + DateTime.Now + "" + "   </td>");

            }
            sw.Write("</tr>");



            sw.Write("<tr>");



            //sw.Write("<td colspan='13'> </td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>District Name</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>District Code</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Block Name</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Block Code</td>");
            if (Flag == 1)
            {
                sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Cluster Name</td>");
                sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Cluster Code</td>");


                sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Village Name</td>");
                sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Village Code</td>");

                sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>School Name</td>");
                sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>DISECODE</td>");
            }
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'># SMC Meeting</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Total Trained Female Member</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Total Traind Male Member</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Total Member</td>");



            sw.Write("</tr>");



            String DataStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;";
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                sw.Write("<tr style='font-width:bold;'>");
                //HttpContext.Current.Response.Write("<td >Direct</td>");
                for (int c = 0; c < dt.Columns.Count; c++)
                {

                    sw.Write("<td style='" + DataStyle + "'>" + dt.Rows[i][c] + "</td>");
                }
            }
            sw.Write("</tr>");
            sw.Write("<tr>");
            HttpContext.Current.Response.Write("<tr>");
            if (Flag == 2)
            {
                for (int J = 0; J < 1; J++)
                {

                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (c <= 3)
                        {
                            if (c == 3)
                            {
                                sw.Write("<td class='header' style='" + DataStyle + "  width:2%;'> Total</td>");
                            }
                            else
                            {
                                sw.Write("<td class='header' style='" + DataStyle + "  width:2%;'></td>");
                            }

                        }
                        else
                        {
                            string Col = "[" + dt.Columns[c].ColumnName + "]";
                            int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));
                            sw.Write("<td class='header' style='" + DataStyle + "  width:2%;'>" + sum + "</td>");

                        }
                    }
                }


            }
            if (Flag == 1)
            {
                for (int J = 0; J < 1; J++)
                {

                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (c <= 9)
                        {
                            if (c == 9)
                            {
                                sw.Write("<td class='header' style='" + DataStyle + "  width:2%;'> Total</td>");
                            }
                            else
                            {
                                sw.Write("<td class='header' style='" + DataStyle + "  width:2%;'></td>");
                            }

                        }
                        else
                        {
                            string Col = "[" + dt.Columns[c].ColumnName + "]";
                            int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));
                            sw.Write("<td class='header' style='" + DataStyle + "  width:2%;'>" + sum + "</td>");

                        }
                    }
                }


            }
            sw.Write("</tr>");


            sw.Write("</table>");
            sw.Flush();
            sw.Close();


            FileStream fs = null;//, fs2=null;
            try
            {
                string path1 = Fullfilename1;
                string foldername = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + path1 + "");
                string datafolder = path1.Substring(0, path1.Length - 4);
                //  string[] file = Directory.GetFiles(foldername);
                string path = foldername;
                string fullPath = Request.MapPath("~/DataBackup/" + datafolder + "" + ".zip");
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(foldername, "");
                    //    zip.AddFiles(file, foldername);
                    zip.Save(Server.MapPath(Comman.GetImagePath("DataBackupPath")) + "/" + datafolder + "" + ".zip");
                }



                HttpResponse Response = HttpContext.Current.Response; Response.Clear(); Response.ClearHeaders(); Response.Charset = "UTF-8";
                fs = File.Open(fullPath, FileMode.Open);
                byte[] bytBytes = new byte[(fs.Length)];
                fs.Read(bytBytes, 0, (int)fs.Length);
                fs.Close();
                Response.AddHeader("Content-disposition", "attachment; filename=" + datafolder + "" + ".zip");
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(bytBytes);






                if (File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                if (File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.End();
            }

            catch (System.Exception ex)
            {
                //  Server.Transfer("default.aspx", false);
                Response.Clear();

                //string mmsg = ex.Message;
                //showEXPMessages("(crateZip)  " + mmsg); //showMessages(mmsg);
            }
            finally
            {
                fs.Dispose();
                Response.Clear();

            }


        }
        catch
        {

            throw;
        }


    }

    private void GenerateExcelSIP(DataTable dt, string FIleName)
    {
        try
        {





            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.ClearContent();
            //HttpContext.Current.Response.ClearHeaders();
            //HttpContext.Current.Response.Buffer = true;
            //HttpContext.Current.Response.ContentType = "application/ms-excel";
            //HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            //string Fullfilename = "" + FIleName + "_" + "" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


            //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");

            //HttpContext.Current.Response.Charset = "utf-8";
            //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");


            string Fullfilename1 = "" + "SIPstatusReport" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + "_" + FIleName + ".xls";
            string fileName = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + Fullfilename1 + "");
            StreamWriter sw = new StreamWriter(fileName, false);
            sw.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            sw.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");


            sw.Write("<table  >");

            sw.Write("<tr>");


            sw.Write("<td colspan='15' style='text-align:center;font:bold;border:.5pt solid windowtext;'>" + "SIP Status Report" + "   </td>");
            sw.Write("</tr>");
            sw.Write("<tr>");
            sw.Write("<td colspan='15' style='text-align:left;font:bold;border:.5pt solid windowtext;'>" + "Current Date : " + DateTime.Now + "" + "   </td>");
            sw.Write("</tr>");

            sw.Write("<tr>");
            sw.Write("<td  colspan='12' style='text-align:center;font:bold;border:.5pt solid windowtext;'></td>");
            sw.Write("<td  colspan='2' style='text-align:center;font:bold;border:.5pt solid windowtext;'> Drinking Water</td>");
            sw.Write("<td  colspan='2' style='text-align:center;font:bold;border:.5pt solid windowtext;'>Girls Toilet</td>");
            sw.Write("<td  colspan='2' style='text-align:center;font:bold;border:.5pt solid windowtext;'>Playground</td>");
            sw.Write("<td  colspan='2' style='text-align:center;font:bold;border:.5pt solid windowtext;'>Electricity</td>");
            sw.Write("<td  colspan='2' style='text-align:center;font:bold;border:.5pt solid windowtext;'>Boundary Wall</td>");

            sw.Write("<td  colspan='2' style='text-align:center;font:bold;border:.5pt solid windowtext;'>Swings and Sliders</td>");
            sw.Write("<td  colspan='2' style='text-align:center;font:bold;border:.5pt solid windowtext;'>Kitchen</td>");
            sw.Write("<td  colspan='2' style='text-align:center;font:bold;border:.5pt solid windowtext;'>PTR</td>");
            sw.Write("<td  colspan='2' style='text-align:center;font:bold;border:.5pt solid windowtext;'>PCR</td>");
            sw.Write("<td  colspan='2' style='text-align:center;font:bold;border:.5pt solid windowtext;'>Health Checkup</td>");

            sw.Write("</tr>");

            sw.Write("<tr>");



            //sw.Write("<td colspan='13'> </td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>District Name</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>District Code</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Block Name</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Block Code</td>");

            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Cluster Name</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Cluster Code</td>");

            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Panchayat Name</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Panchayat Code</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Village Name</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Village Code</td>");

            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'> School Name</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>DISECODE</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Status as on 31st March</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Current Status</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Status as on 31st March</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Current Status</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Status as on 31st March</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Current Status</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Status as on 31st March</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Current Status</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Status as on 31st March</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Current Status</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Status as on 31st March</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Current Status</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Status as on 31st March</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Current Status</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Status as on 31st March</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Current Status</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Status as on 31st March</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Current Status</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Status as on 31st March</td>");
            sw.Write("<td   style='text-align:center;font:bold;border:.5pt solid windowtext;text-align: center'>Current Status</td>");



            sw.Write("</tr>");



            String DataStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;";
            for (int i = 0; i < dt.Rows.Count; i++)
            {




                sw.Write("<tr style='font-width:bold;'>");
                //HttpContext.Current.Response.Write("<td >Direct</td>");
                for (int c = 0; c < dt.Columns.Count; c++)
                {


                    sw.Write("<td style='" + DataStyle + "'>" + dt.Rows[i][c] + "</td>");


                }
            }
            sw.Write("</tr>");
            //sw.Write("<tr>");
            //HttpContext.Current.Response.Write("<tr>");
            //for (int J = 0; J < 1; J++)
            //{

            //        for (int c = 0; c < dt.Columns.Count; c++)
            //        {
            //            if (c <=12)
            //            {
            //                if (c == 12)
            //                {
            //                    sw.Write("<td class='header' style='" + HeaderStyle + "  width:2%;'> Total</td>");
            //                }
            //                else
            //                {
            //                    sw.Write("<td class='header' style='" + HeaderStyle + "  width:2%;'></td>");
            //                }

            //            }
            //            else
            //            {
            //                string Col = "[" + dt.Columns[c].ColumnName + "]";
            //                int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));
            //                sw.Write("<td class='header' style='" + HeaderStyle + "  width:2%;'>" + sum + "</td>");

            //            }
            //        }

            //}
            //sw.Write("</tr>");


            sw.Write("</table>");
            sw.Flush();
            sw.Close();


            FileStream fs = null;//, fs2=null;
            try
            {
                string path1 = Fullfilename1;
                string foldername = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + path1 + "");
                string datafolder = path1.Substring(0, path1.Length - 4);
                //  string[] file = Directory.GetFiles(foldername);
                string path = foldername;
                string fullPath = Request.MapPath("~/DataBackup/" + datafolder + "" + ".zip");
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(foldername, "");
                    //    zip.AddFiles(file, foldername);
                    zip.Save(Server.MapPath(Comman.GetImagePath("DataBackupPath")) + "/" + datafolder + "" + ".zip");
                }



                HttpResponse Response = HttpContext.Current.Response; Response.Clear(); Response.ClearHeaders(); Response.Charset = "UTF-8";
                fs = File.Open(fullPath, FileMode.Open);
                byte[] bytBytes = new byte[(fs.Length)];
                fs.Read(bytBytes, 0, (int)fs.Length);
                fs.Close();
                Response.AddHeader("Content-disposition", "attachment; filename=" + datafolder + "" + ".zip");
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(bytBytes);






                if (File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                if (File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.End();
            }

            catch (System.Exception ex)
            {
                //  Server.Transfer("default.aspx", false);
                Response.Clear();

                //string mmsg = ex.Message;
                //showEXPMessages("(crateZip)  " + mmsg); //showMessages(mmsg);
            }
            finally
            {
                fs.Dispose();
                Response.Clear();

            }


        }
        catch
        {

            throw;
        }


    }


    protected void PanchayatMeeting_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        gvWeaklly.Visible = false;
        GV_DynamicGrid2.Visible = true;
        ViewState["Button"] = "701";
        btnexcel.Visible = true;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getreporAllVillage(1);
        gvQuerltyAnnual.Visible = false;
    }
    protected void ActivityVillageRatri_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        gvWeaklly.Visible = false;
        GV_DynamicGrid2.Visible = true;
        ViewState["Button"] = "704";
        btnexcel.Visible = true;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getreporAllVillage(2);
        gvQuerltyAnnual.Visible = false;
    }
    protected void ActivityVillageNamankan_Click(object sender, EventArgs e)
    {
        DGV_Report.Visible = false;
        gvWeaklly.Visible = false;
        GV_DynamicGrid2.Visible = true;
        ViewState["Button"] = "704";
        btnexcel.Visible = true;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReport.Visible = false;
        getreporAllVillage(3);
        gvQuerltyAnnual.Visible = false;
    }
    public void getreporAllVillage(Int32 Flag)
    {
        conditions = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        string afromDate = "";
        string aToDate = "";
        if (txtDate.Text != "" && txtTodate.Text != "")
        {
            string fromDate = txtDate.Text;

            string[] d = fromDate.Split('/');
            afromDate = d[2] + '-' + d[1] + '-' + d[0];

            string ToDate = txtTodate.Text;
            string[] c = ToDate.Split('/');
            aToDate = c[2] + '-' + c[1] + '-' + c[0];


            DateTime d1 = Convert.ToDateTime(afromDate);
            DateTime d2 = Convert.ToDateTime(aToDate);
            int month = Convert.ToInt32(c[1]) - Convert.ToInt32(d[1]);
            TimeSpan t = d2 - d1;
        }




        //double Days = Convert.ToDouble(t.TotalDays);
        //if (Math.Sign(Days) == -1)
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Vaild Date')</script>", false);
        //    return;
        //}
        //if (Math.Round(Days) >= 31)
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date rang 30 Day')</script>", false);
        //    return;
        //}


        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        foreach (ListItem item in ddlPanchayat.Items)
        {
            if (item.Selected)
            {

                ddlPhan += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        foreach (ListItem item in chkVillage.Items)
        {
            if (item.Selected)
            {

                ddlVillage += "'" + item.Value + "'" + ",";


            }
        }
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + "  where  mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }



        if (txtDate.Text != "" && txtTodate.Text != "")
        {
            if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
            {
                conditions += " and ActivityDate between('" + afromDate + "') and ('" + aToDate + "') and UserEntry=2  and ApproveStatus='FC' ";
            }

            if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
            {
                conditions += " and ActivityDate between('" + afromDate + "') and ('" + aToDate + "') and UserEntry=3  and ApproveStatus='B' ";
            }
            if (Convert.ToInt32(rblApprove.SelectedValue) == 3)
            {
                conditions += " and ActivityDate between('" + afromDate + "') and '" + aToDate + "' and UserEntry=3  and ApproveStatus='I' ";
            }

        }
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con", conditions),
            new SqlParameter("@Flag", Flag),
            new SqlParameter("@mYear",  ddlYear.SelectedValue),
        };
        DataTable dataTable = GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivityVIllageRawDataPanchayat]", cmdParameters);

        if (Flag == 1)
        {
            dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Sarpanch/Pradhan Name", "Sarpanch/Pradhan Mobile Number", "Entry Done By Name");

            lblTotalCount.Text = dataTable.Rows.Count.ToString();
            ViewState["dt"] = dataTable;
            if (dataTable.Rows.Count > 0)
            {
                ExportToCSVFile(dataTable, "PanchayatMeetingReport");
            }
            else
            {
                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();
                return;
            }
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }
        if (Flag == 2)
        {
            dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Mohalla", "Entry Done By Name");

            lblTotalCount.Text = dataTable.Rows.Count.ToString();
            ViewState["dt"] = dataTable;
            if (dataTable.Rows.Count > 0)
            {
                ExportToCSVFile(dataTable, "RatriChaupalReport");
            }
            else
            {
                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();

                return;
            }
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }

        if (Flag == 3)
        {
            dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Entry Done By Name");

            lblTotalCount.Text = dataTable.Rows.Count.ToString();
            ViewState["dt"] = dataTable;
            if (dataTable.Rows.Count > 0)
            {
                ExportToCSVFile(dataTable, "NamankanRallyReport");
            }
            else
            {
                GV_DynamicGrid2.DataSource = dataTable;
                GV_DynamicGrid2.DataBind();

                return;
            }
            GV_DynamicGrid2.DataSource = null;
            GV_DynamicGrid2.DataBind();
        }
    }
}