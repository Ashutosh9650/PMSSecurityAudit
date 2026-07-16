using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.InteropServices;

using Microsoft.Reporting.WebForms;
using System.Globalization;
public partial class frmDonorTarget : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    public bool edit_status = false;
    string conditions = "";
    string flag = "";
    Password objPass = new Password();
    public DataTable dtUserDeatils;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                
                   
                   

                    FillDonor();
                    FillSocialCat();
                    FillENrollment();
                    FillEduStauts();
                    ViewState["1"] = "ss";
                  
                
            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }

        }
    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        int Result = 0;
        for (int i = 0; i < gvnroll.Rows.Count; i++)
        {
            string ReportingOutcome = ((Label)gvnroll.Rows[i].FindControl("lblReportingOutcome")).Text;
            string lblMainID = ((Label)gvnroll.Rows[i].FindControl("lblMainID")).Text;
            string lblSubID = ((Label)gvnroll.Rows[i].FindControl("lblSubID")).Text;
            string lblFrequencyID = ((Label)gvnroll.Rows[i].FindControl("lblFrequencyID")).Text;
            string txtQ1 = ((TextBox)gvnroll.Rows[i].FindControl("txtQ1")).Text;
            string txtQ2 = ((TextBox)gvnroll.Rows[i].FindControl("txtQ2")).Text;
            string txtQ3 = ((TextBox)gvnroll.Rows[i].FindControl("txtQ3")).Text;
            string txtQ4 = ((TextBox)gvnroll.Rows[i].FindControl("txtQ4")).Text;
            int Q1 = 0, Q2 = 0, Q3 = 0, Q4 = 0;
            if (txtQ1 != "")
            {
                Q1 = Convert.ToInt32(txtQ1);
            }
            if (txtQ2 != "")
            {
                Q2 = Convert.ToInt32(txtQ2);
            }
            if (txtQ3 != "")
            {
                Q3 = Convert.ToInt32(txtQ3);
            }
            if (txtQ4 != "")
            {
                Q4 = Convert.ToInt32(txtQ4);
            }
            Result = InsertUpdateDonor(lblMainID, lblSubID, lblFrequencyID, Q1, Q2, Q3, Q4);
        }
        if (Result > 0)
        {
     
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
           
        }
    }

    public int InsertUpdateDonor(string lblMainID, string lblSubID,  string lblFrequencyID, int txtQ1, int txtQ2, int txtQ3, int txtQ4)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
                    new SqlParameter("@DonorID", ddlDonor.SelectedValue),
                    new SqlParameter("@MID", lblMainID),
                    new SqlParameter("@SID", lblSubID),
                    new SqlParameter("@FRID", lblFrequencyID),
                    new SqlParameter("@Q1", txtQ1),
                    new SqlParameter("@Q2", txtQ2),
                    new SqlParameter("@Q3", txtQ3),
                    new SqlParameter("@Q4", txtQ4),
                     new SqlParameter("@StateCode", lblState.Text),
                      new SqlParameter("@DistrictCode", lblDistrict.Text),
                       new SqlParameter("@BlockCode", lblBlock.Text),
                    
                                        
    };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateDonorTarget", cmdParameters);
    }
  

 

    public bool CheckAllphanumeric(string txtHhno)
    {


        System.Text.RegularExpressions.Regex objAlphaNumericPattern = new System.Text.RegularExpressions.Regex("^(?=.*[0-9]+.*)");
        return !objAlphaNumericPattern.IsMatch(txtHhno);
    }
 
    public void FillDonor()
    {



        objComman.BindDLL("mstDonorDeatils", "DID,DonorName", "  Dyear = '"+Convert.ToString(Session["FinYear"]) +"'", "DonorName", "asc", ddlDonor, "DonorName", "DID", "Select");



    }


    public void FillSocialCat()
    {
        conditions = "";
        conditions = "LookupFlag ='CAT' and Active=1";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlScat, "Description", "LookupCode", "Select");



    }

    public void FillENrollment()
    {
        conditions = "";
        conditions = "LookupFlag ='ES' and Active=1 and LookupCode in(1,2,3) ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlEnroll, "Description", "LookupCode", "Select");



    }

    public void FillEduStauts()
    {
        conditions = "";
        conditions = "LookupFlag ='EC' and Active=1";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlEduationStatus, "Description", "LookupCode", "Select");



    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlParameter[] parm1 = new SqlParameter[]
            {
         
               new SqlParameter("@ID",  ddlDonor.SelectedValue),
                
            };


        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptDonorMasterLoad]", parm1);

        if (dt.Rows.Count > 0)
        {
            lblState.Text = dt.Rows[0]["State Name"].ToString();
            lblDistrict.Text = dt.Rows[0]["District Name"].ToString();
            lblBlock.Text = dt.Rows[0]["Block Name"].ToString();
            lblFrequency.Text = dt.Rows[0]["Reporting Frequency"].ToString();
            lblTarget.Text = dt.Rows[0]["Fyear"].ToString();
        }
        gvnroll.DataSource = null;
        gvnroll.DataBind();
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
        //DateTime GivenDate = DateTime.Now;
        //int GivenYear = GivenDate.Year;
        //int m = GivenDate.Month;

        //DataTable dt = null;
        ////ddlYear.Items.Add("--Select--","0");
        //int y = GivenDate.Year;

      
        //DateTime GivenDate1 = DateTime.Now;
        //int GivenYear1 = GivenDate1.Year;
        //DataTable dtYear = CreateDataTable();
        //DataRow dr;
        //if (ddlYear.SelectedIndex < 0)
        //{

        //    string mYear1 = GivenYear1.ToString();
        //    for (int j = 0; j < 1; j++)
        //    {
        //        if (m > 3)
        //        {
        //            dr = dtYear.NewRow();
        //            dr["Type"] = GivenYear.ToString() + "-" + Convert.ToString((GivenYear + 1));
        //            dr["ID"] = y;
        //            dtYear.Rows.Add(dr);
        //            dr = dtYear.NewRow();
        //            dr["Type"] = GivenYear - 1 + "-" + Convert.ToString((GivenYear - 1 + 1));
        //            dr["ID"] = y - 1;
        //            dtYear.Rows.Add(dr);
        //            //get last  two digits (eg: 10 from 2010);

        //            dr = dtYear.NewRow();
        //            dr["Type"] = GivenYear - 2 + "-" + Convert.ToString((GivenYear - 2 + 1));
        //            dr["ID"] = y - 2;
        //            dtYear.Rows.Add(dr);
        //        }
        //        else
        //        {
        //            dr = dtYear.NewRow();
        //            dr["Type"] = Convert.ToString((y - 1)) + "-" + y.ToString();
        //            //y = y - 1;
        //            dr["ID"] = y - 1;

        //            dtYear.Rows.Add(dr);


        //            dr = dtYear.NewRow();
        //            dr["Type"] = GivenYear - 2 + "-" + Convert.ToString((GivenYear - 2 + 1));
        //            dr["ID"] = y - 2;
        //            dtYear.Rows.Add(dr);
        //        }

        //    }

        //}
      


    }
   

    protected void gvnroll_OnRowCommand(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           


        }


    }

    public void Puppop()
    {
        string url = "frmAddEnrollmentFrom6.aspx";

        string s = "window.open('" + url + "', 'popup_window', 'width=800,height=500,left=700,top=400,scrollbars=1,resizable=yes');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

    }
    public void LoadData()
    {
        string strQry = "";
       

        SqlParameter[] parm1 = new SqlParameter[]
            {
         
               new SqlParameter("@ID",  ddlDonor.SelectedValue),

            
            };


        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadDonoTarget]", parm1);



                if (dt.Rows.Count > 0)
                {
                   
                    if (lblFrequency.Text == "Quarterly")
                    {
                        gvnroll.Columns[2].HeaderText = "Target Q1";
                        gvnroll.Columns[3].HeaderText = "Target Q2";
                        gvnroll.Columns[4].HeaderText = "Target Q3";
                        gvnroll.Columns[5].HeaderText = "Target Q4";
                        gvnroll.Columns[2].Visible = true;
                        gvnroll.Columns[3].Visible = true;
                        gvnroll.Columns[4].Visible = true;
                        gvnroll.Columns[5].Visible = true;

                   
                    }
                    if (lblFrequency.Text == "Half Yearly")
                    {
                        gvnroll.Columns[2].HeaderText = "First Half ";
                        gvnroll.Columns[3].HeaderText = "Second Half ";

                        gvnroll.Columns[2].Visible = true;
                        gvnroll.Columns[3].Visible = true;
                      
                        gvnroll.Columns[4].Visible = false;

                        gvnroll.Columns[5].Visible = false;
                    }
                    if (lblFrequency.Text == "Yearly")
                    {
                        gvnroll.Columns[2].HeaderText = "Yearly";
                        gvnroll.Columns[2].Visible = true;
                        gvnroll.Columns[3].Visible = false;
                        gvnroll.Columns[4].Visible = false;
                        gvnroll.Columns[5].Visible = false;
                       
                    }
                    gvnroll.DataSource = dt;
                    gvnroll.DataBind();
                }
                else
                {
                    gvnroll.DataSource = null;
                    gvnroll.DataBind();
                }
        }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void btnMain_Click(object sender, EventArgs e)
    {
      

    }

    protected void LnkBtnBlock_OnClick(object sender, EventArgs e)
    {
        

    }
    public void FillD2dData()
    {
        string strQry = " Select [UniqueChildCode],VillagenameOther,SamgraID,ChildCode,mstSchool.name,tblEnrolment.[VillageCode],EnrolmentDate as SurvayDate,Class,AsOnDate,[Serial],[HouseNo],[Category],[ChildName] as ChildName,[FatherName] as FathersName,[Gender],[DOBAvailable],[DOB],[AgeAson],Type as EduationStatus,tblEnrolment.[SchoolCode],[EnrollCategory], mst5Village.PanchayatCode,mst5Village.BlockCode,mst5Village.DistrictCode,tblEnrolment.Status FROM (mst5Village INNER JOIN tblEnrolment ON mst5Village.VillageCode = tblEnrolment.VillageCode) left JOIN mstSchool ON tblEnrolment.SchoolCode = mstSchool.SchoolCode where UniqueChildCode='" + Session["UnquieId"].ToString() + "' ";
        DataTable dt = objMain.LoadData(strQry);


        if (dt.Rows.Count > 0)
        {

            //if (dt.Rows[0]["Status"].ToString() == "2")
            //{
            //    btnd2dSave.Enabled = false;
            //    btnD2Delete.Enabled = false;
            //}
            //else
            //{
            //    btnd2dSave.Enabled = true;
            //    btnD2Delete.Enabled = true;
            //}

            ddlGender.SelectedValue = dt.Rows[0]["Gender"].ToString();

            txtSrno.Text = dt.Rows[0]["Serial"].ToString();
            txtChildName.Text = dt.Rows[0]["ChildName"].ToString();
            txtFatherName.Text = dt.Rows[0]["FathersName"].ToString();

            txtSamgra.Text = dt.Rows[0]["SamgraID"].ToString();
            txtSurveyVillage.Text = dt.Rows[0]["VillagenameOther"].ToString();
            //villagecode = dt.Rows[0]["VillageCode"].ToString();




            //DTPicker_DOB.Format = DateTimePickerFormat.Custom;
            //DTPicker_DOB.CustomFormat = "dd/MM/yyyy ";

            DateTime DOB = Convert.ToDateTime(dt.Rows[0]["DOB"].ToString());

            if (dt.Rows[0]["DOB"].ToString() == "01/01/1900 00:00:00")
            {
                txtDobDate.Text = "";
            }
            else
            {
                txtDobDate.Text = DOB.ToString("dd/MM/yyyy");
            }




            DateTime SurvayDate = Convert.ToDateTime(dt.Rows[0]["SurvayDate"].ToString());
            if (dt.Rows[0]["DOB"].ToString() == "01/01/1900 00:00:00")
            {
                txtBirth.Text = "";
            }
            else
            {
                txtBirth.Text = SurvayDate.ToString("dd/MM/yyyy");
            }




            ddlScat.SelectedValue = dt.Rows[0]["Category"].ToString();
            //ddlEduationStatus.SelectedValue = Convert.ToInt32(dt.Rows[0]["EnrollCategory"].ToString()).ToString();
            //ddlEnroll.SelectedValue = dt.Rows[0]["EduationStatus"].ToString();


            lblSchool.Text = dt.Rows[0]["name"].ToString();
            txtHHNo.Text = dt.Rows[0]["HouseNo"].ToString();
            dllClass.SelectedValue = dt.Rows[0]["Class"].ToString();
            //if (dt.Rows[0]["ReasonDO_NE"].ToString() == "0")
            //{
            //    txtReason.Text = "";

            //}
            //else
            //{
            //    txtReason.Text = dt.Rows[0]["ReasonDO_NE"].ToString();
            //}


            txtHHNo.Text = dt.Rows[0]["HouseNo"].ToString();

        }
    }
    protected void btnReprot_Click(object sender, EventArgs e)
    {
        string Quter = "";
                     if (lblFrequency.Text == "Quarterly")
                    {
                        Quter = "Quarterly";                   
                    }
                    if (lblFrequency.Text == "Half Yearly")
                    {

                        Quter = "Half Yearly"; 
                        
                    }
                    if (lblFrequency.Text == "Yearly")
                    {

                        Quter = "Yearly"; 
                    }
                  
                
        SqlParameter[] cmdParameters = new SqlParameter[]
		{
			new SqlParameter("@DID",ddlDonor.SelectedValue),
            new SqlParameter("@FID",Quter),
            
		};
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptDonorTargetReport]", cmdParameters);

        if (dt.Rows.Count > 0)
        {
            // ExporttoExcel(dt, "LearningCampMaster");
            ExportToExcelNew(dt, "DonorTargetReport");
        }

    }
    public void ExportToExcelNew(DataTable dt, string FileName)
    {
        Response.ClearContent();
        Response.Buffer = true;
        string Fullfilename = "" + FileName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";

        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", Fullfilename));
        Response.ContentType = "application/ms-excel";
        Response.ContentEncoding = System.Text.Encoding.Unicode;
        Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
        DataTable dt1 = dt;
        string str = string.Empty;
        foreach (DataColumn dtcol in dt1.Columns)
        {
            Response.Write(str + dtcol.ColumnName);
            str = "\t";
        }
        Response.Write("\n");
        foreach (DataRow dr in dt1.Rows)
        {
            str = "";
            for (int j = 0; j < dt1.Columns.Count; j++)
            {
                Response.Write(str + Convert.ToString(dr[j]));
                str = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }
    
}