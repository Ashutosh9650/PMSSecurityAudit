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
public partial class frmanalysisNewReport : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();

    string conditions = "";
    string flag = "";
    Password objPass = new Password();
    public DataTable dtUserDeatils;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {
            if (!IsPostBack)
            {
                LoadYear();
                LoadUserLeavel();
                ViewState["1"] = "ss";


            }
        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
    }
    public DataTable CreateDataTable()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYear.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYear;
    }
    protected void rblBlockType_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlPanchayat.Items.Clear();
        chkVillage.Items.Clear();
        ddlDistrict_SelectedIndexChanged(chkBlock, null);
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
        DataTable dtYear = objComman.Generate_Financial_Year();
    
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
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
  
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 1;
        ddlUser.SelectedIndex = 1;
        LoadReport();
    
    }
    protected void btnEnroll_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 7;
       ddlUser.SelectedIndex = 1;
        LoadReport();
    
    }

    
    protected void btnGrade_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 6;

        ddlUser.SelectedIndex = 1;
        LoadReport();
    }
    
    protected void Education_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 5;

        ddlUser.SelectedIndex = 1;
        LoadReport();
    }
    
    
    protected void btnAge_Click(object sender, EventArgs e)
    {
        
        ViewState["1"] = 4;        
        ddlUser.SelectedIndex = 1;
        LoadReport();
    }
    protected void btnReason_Click(object sender, EventArgs e)
    {
        
        ViewState["1"] = 8;        
        ddlUser.SelectedIndex = 1;
        LoadReport();
    }
    protected void btnAnalayis_Click(object sender, EventArgs e)
    {

        ViewState["1"] = 9;
        ddlUser.SelectedIndex = 1;
        LoadReport();
    }
    
    protected void btnUser_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 2;
       ddlUser.SelectedIndex = 1;
        LoadReport();
       
    }
    protected void btnD2d_Click(object sender, EventArgs e)
    {
        ViewState["1"] = 3;
       ddlUser.SelectedIndex = 1;
        LoadReport();
    }
    protected void btnMainReport_Click(object sender, EventArgs e)
    {
        
            
       
    }
    public void LoadReport()
    {

        conditions = "";
        string conditionsCr = "";
  
        string conditionsAll = "";
        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
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
            conditions += "  mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlState.Length > 0)
        {
            conditions += " and mst5Village.StateCode in(" + ddlState + ") ";
            conditionsAll += "  StateCode in(" + ddlState + "";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
            conditionsAll += "  and DistCode in(" + ddlDistrict + ")  ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions += " and mst5Village.BlockCode in(" + ddlBlock + ") ";
            conditionsAll += " and  BlockCode in(" + ddlBlock + ")  ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }

        
     

         
            if (txtDate.Text != "")
            {
                DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                conditionsCr += " and Createdate >=  '" + Fromdate.ToString("yyyy-MM-dd") + "' ";
            }
            if (txtTodate.Text != "")
            {
                DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                conditionsCr += " and Createdate <=  '" + Todate.ToString("yyyy-MM-dd") + "' ";
               
            }
            if (txtDate.Text != "" && txtTodate.Text != "")
            {
                DateTime Fromdate = Convert.ToDateTime(txtDate.Text);
                DateTime Todate = Convert.ToDateTime(txtTodate.Text);
                conditionsCr += " and Createdate BETWEEN '" + Fromdate.ToString("yyyy-MM-dd") + "' and '" + Todate.ToString("yyyy-MM-dd") + "'";
               }

            string FristCon = conditions + conditionsCr;

           
            //DataTable dt = objMain.AgeWiseSocialCategory(FristCon,0);
            //DataTable dt = objMain.AgeWisFeamilyOccupation(FristCon, 0);
            if (ViewState["1"].ToString() == "1")
            {
                DataTable dt = objMain.AgeWiseSocialCategory(FristCon, 0);
                if (dt.Rows.Count > 0)
                {





                    if (Convert.ToInt32(ddlUser.SelectedValue) == 1)
                    {

                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptDistAgeWiseSocialCategory.rdlc");
                        ReportDataSource datasource = new ReportDataSource("SocialCategory", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "AgeWiseSocialCategory";




                    }

                    if (Convert.ToInt32(ddlUser.SelectedValue) == 2)
                    {

                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptBlockAgeWiseSocialCategory.rdlc");
                        ReportDataSource datasource = new ReportDataSource("SocialCategory", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "AgeWiseSocialCategory";




                    }
                    if (Convert.ToInt32(ddlUser.SelectedValue) == 3)
                    {

                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptPanchayatAgeWiseSocialCategory.rdlc");
                        ReportDataSource datasource = new ReportDataSource("SocialCategory", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "AgeWiseSocialCategory";




                    }

                    if (Convert.ToInt32(ddlUser.SelectedValue) == 4)
                    {

                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptVillageAgeWiseSocialCategory.rdlc");
                        ReportDataSource datasource = new ReportDataSource("SocialCategory", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);
                        rptD2D.Width = 600;
                        rptD2D.LocalReport.DisplayName = "AgeWiseSocialCategory";




                    }
                }
            }

            if (ViewState["1"].ToString() == "2")
            {
                DataTable dt = objMain.AgeWisFeamilyOccupation(FristCon, 0);
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(ddlUser.SelectedValue) == 1)
                    {

                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptDistFamilyOccupation.rdlc");
                        ReportDataSource datasource = new ReportDataSource("DistFamilyOccupation", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "FamilyOccupation ";
                    }
                    if (Convert.ToInt32(ddlUser.SelectedValue) == 2)
                    {

                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptBlockFamilyOccupation.rdlc");
                        ReportDataSource datasource = new ReportDataSource("DistFamilyOccupation", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "FamilyOccupation ";
                    }
                    if (Convert.ToInt32(ddlUser.SelectedValue) == 3)
                    {

                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptPhanFamilyOccupation.rdlc");
                        ReportDataSource datasource = new ReportDataSource("DistFamilyOccupation", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "FamilyOccupation ";
                    }
                    if (Convert.ToInt32(ddlUser.SelectedValue) == 4)
                    {

                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptVillageFamilyOccupation.rdlc");
                        ReportDataSource datasource = new ReportDataSource("DistFamilyOccupation", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "FamilyOccupation ";
                    }
                }
            }


            if (ViewState["1"].ToString() == "3")
            {
                DataTable dt = objMain.ReportD2d(FristCon);
                if (dt.Rows.Count > 0)
                {

                    rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptD2dReport.rdlc");
                    ReportDataSource datasource = new ReportDataSource("D2dDataSet", dt);
                    rptD2D.LocalReport.DataSources.Clear();
                    rptD2D.LocalReport.DataSources.Add(datasource);


                    rptD2D.Width = 600;



                    rptD2D.LocalReport.DisplayName = "D2D Report";


                }
            }
            if (ViewState["1"].ToString() == "4")
            {
                if (Convert.ToInt32(ddlUser.SelectedValue) == 2)
                {
                    DataTable dt = objMain.AgeWise(FristCon, 0);
                    if (dt.Rows.Count > 0)
                    {

                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptAgeWise.rdlc");
                        ReportDataSource datasource = new ReportDataSource("AgeWise", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "Gender and Block wise Age Category  ";


                    }
                }
                if (Convert.ToInt32(ddlUser.SelectedValue) == 1)
                {
                    DataTable dt = objMain.AgeWise(FristCon, 0);
                    if (dt.Rows.Count > 0)
                    {

                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptDistAgeWise.rdlc");
                        ReportDataSource datasource = new ReportDataSource("AgeWise", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "Gender and Block wise Age Category  ";


                    }
                }
                if (Convert.ToInt32(ddlUser.SelectedValue) == 3)
                {
                    DataTable dt = objMain.AgeWise(FristCon, 0);
                    if (dt.Rows.Count > 0)
                    {

                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptPanchayatAgeWise.rdlc");
                        ReportDataSource datasource = new ReportDataSource("GenderAgeWise", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "Gender and Panchayat wise Age Category  ";


                    }
                }
                if (Convert.ToInt32(ddlUser.SelectedValue) == 4)
                {
                    DataTable dt = objMain.AgeWise(FristCon, 0);
                    if (dt.Rows.Count > 0)
                    {

                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptVillageAgeWise.rdlc");
                        ReportDataSource datasource = new ReportDataSource("GenderAgeWise", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "Gender and Village wise Age Category  ";


                    }
                }
            }

            if (ViewState["1"].ToString() == "5")
            {
                DataTable dt = objMain.AgeWiseEducationstatus(FristCon, 0);
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(ddlUser.SelectedValue) == 1)
                    {
                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptAgeWiseEducationstatus.rdlc");
                        ReportDataSource datasource = new ReportDataSource("Educationstatus", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "Educationstatus ";
                    }
                    if (Convert.ToInt32(ddlUser.SelectedValue) == 2)
                    {
                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptBlockAgeWiseEducationstatus.rdlc");
                        ReportDataSource datasource = new ReportDataSource("Educationstatus", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "Educationstatus ";
                    }
                    if (Convert.ToInt32(ddlUser.SelectedValue) ==3)
                    {
                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptPanchayatAgeWiseEducationstatus.rdlc");
                        ReportDataSource datasource = new ReportDataSource("Educationstatus", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "Educationstatus ";
                    }
                    if (Convert.ToInt32(ddlUser.SelectedValue) == 4)
                    {
                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptVillagetAgeWiseEducationstatus.rdlc");
                        ReportDataSource datasource = new ReportDataSource("Educationstatus", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "Educationstatus ";
                    }

                }
            }
            if (ViewState["1"].ToString() == "6")
            {
                DataTable dt = objMain.AgeWiseGrade(FristCon, 0);
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(ddlUser.SelectedValue) == 1)
                    {
                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptDistGradewise.rdlc");
                        ReportDataSource datasource = new ReportDataSource("Gradewise", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "Gradewise ";
                    }
                    if (Convert.ToInt32(ddlUser.SelectedValue) == 2)
                    {
                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptBlockGradewise.rdlc");
                        ReportDataSource datasource = new ReportDataSource("Gradewise", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "Gradewise ";
                    }
                    if (Convert.ToInt32(ddlUser.SelectedValue) == 3)
                    {
                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptPhanGradewise.rdlc");
                        ReportDataSource datasource = new ReportDataSource("Gradewise", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "Gradewise ";
                    }
                    if (Convert.ToInt32(ddlUser.SelectedValue) ==4)
                    {
                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptVillageGradewise.rdlc");
                        ReportDataSource datasource = new ReportDataSource("Gradewise", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "Gradewise ";
                    }
                }
            }

            if (ViewState["1"].ToString() == "7")
            {
                DataTable dt = objMain.AgeWiseEnrollPlan(FristCon, 0);
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(ddlUser.SelectedValue) == 1)
                    {

                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptAgeWiseEducationsPlan.rdlc");
                        ReportDataSource datasource = new ReportDataSource("EducationsPlan", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "Enrollplan ";
                    }

                    if (Convert.ToInt32(ddlUser.SelectedValue) == 2)
                    {

                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptBlockAgeWiseEducationsPlan.rdlc");
                        ReportDataSource datasource = new ReportDataSource("EducationsPlan", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "Enrollplan ";
                    }
                    if (Convert.ToInt32(ddlUser.SelectedValue) == 3)
                    {

                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptPhyAgeWiseEducationsPlan.rdlc");
                        ReportDataSource datasource = new ReportDataSource("EducationsPlan", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "Enrollplan ";
                    }

                }
            }


            if (ViewState["1"].ToString() == "8")
            {
                DataTable dt = objMain.AgeWiseEducationsReason(FristCon, 0);
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(ddlUser.SelectedValue) == 1)
                    {

                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptAgeWiseEducationsReason.rdlc");
                        ReportDataSource datasource = new ReportDataSource("EducationsReason", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "Enrollplan ";
                    }

                    if (Convert.ToInt32(ddlUser.SelectedValue) == 2)
                    {

                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptBlockAgeWiseEducationsReason.rdlc");
                        ReportDataSource datasource = new ReportDataSource("EducationsReason", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "Enrollplan ";
                    }
                    if (Convert.ToInt32(ddlUser.SelectedValue) == 3)
                    {

                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptPhyAgeWiseEducationsReason.rdlc");
                        ReportDataSource datasource = new ReportDataSource("EducationsReason", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "Enrollplan ";
                    }
                    if (Convert.ToInt32(ddlUser.SelectedValue) == 4)
                    {

                        rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptVillAgeWiseEducationsReason.rdlc");
                        ReportDataSource datasource = new ReportDataSource("EducationsReason", dt);
                        rptD2D.LocalReport.DataSources.Clear();
                        rptD2D.LocalReport.DataSources.Add(datasource);


                        rptD2D.Width = 600;



                        rptD2D.LocalReport.DisplayName = "Enrollplan ";
                    }
                }
            }

            if (ViewState["1"].ToString() == "9")
            {
                string cond = "";
                if (ddlState.Length > 0)
                {
                    cond += " and mst5Village.StateCode in(" + ddlState + ") ";
                   
                }
                if (ddlDistrict.Length > 0)
                {
                    cond += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
                   
                }
                if (ddlBlock.Length > 0)
                {
                    cond += " and mst5Village.BlockCode in(" + ddlBlock + ") ";
                  
                }
                DataTable dt = objMain.rptEnrollmentAnalayis(cond, 0);
                if (dt.Rows.Count > 0)
                {


                    rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rpttEnrollmentAnalayis.rdlc");
                    ReportDataSource datasource = new ReportDataSource("Enrollment", dt);
                    rptD2D.LocalReport.DataSources.Clear();
                    rptD2D.LocalReport.DataSources.Add(datasource);


                    rptD2D.Width = 600;



                    rptD2D.LocalReport.DisplayName = "Enrollplan ";
                }
            }
    }

    public void LoadUserLeavel()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State   order by StateName   ";
            DataTable dtState = objMain.LoadData(strQry1);
            ChkState.DataSource = dtState;
            ChkState.DataTextField = "StateName";
            ChkState.DataValueField = "StateCode";
            ChkState.DataBind();

            ChkState.Enabled = true;
            chkDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "UserName='" + Session["username"].ToString() + "' ";
            string strQry1 = "  SELECT distinct mst1State.StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode inner join mst1State on mst1State.StateCode=mst2District.StateCode where   " + conditions + "  order by StateName   ";
            DataTable dtState = objMain.LoadData(strQry1);
            ChkState.DataSource = dtState;
            ChkState.DataTextField = "StateName";
            ChkState.DataValueField = "StateCode";
            ChkState.DataBind();
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
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State  where   " + conditions + "  order by StateName   ";
            DataTable dtState = objMain.LoadData(strQry1);
            ChkState.DataSource = dtState;
            ChkState.DataTextField = "StateName";
            ChkState.DataValueField = "StateCode";
            ChkState.DataBind();
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
            strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions + "  and  Fyear='" + ddlYear.SelectedItem.Text + "' ";
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

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPanchayat.Items.Clear();
        chkVillage.Items.Clear();
        chkDistrict.Items.Clear();
        chkBlock.Items.Clear();
        FillCBDist();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBBock();
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBCluster();
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillage();
    }
    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadReport();
    }

    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        
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
        if (Convert.ToInt32(rblBlockType.SelectedValue) == 1)
        {
            string strQry = "  SELECT BlockCode, dbo.TitleCase(upper(BlockName))  as BlockName FROM mst3Block where " + conditions + "  order by BlockName   ";
            DataTable dtDistrict = objMain.LoadData(strQry);
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            chkBlock.DataSource = dtDistrict;
            chkBlock.DataTextField = "BlockName";
            chkBlock.DataValueField = "BlockCode";
            chkBlock.DataBind();
        }
        if (Convert.ToInt32(rblBlockType.SelectedValue) == 2)
        {
            string strQry = "  SELECT distinct MainBlockCode as BlockCode, dbo.TitleCase(upper(MainBlockName))  as BlockName FROM mst5Village where " + conditions + "  order by BlockName   ";
            DataTable dtDistrict = objMain.LoadData(strQry);
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            chkBlock.DataSource = dtDistrict;
            chkBlock.DataTextField = "BlockName";
            chkBlock.DataValueField = "BlockCode";
            chkBlock.DataBind();
        }


        ddlPanchayat.Items.Clear();
        chkVillage.Items.Clear();

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
        if (Convert.ToInt32(rblBlockType.SelectedValue) == 1)
        {
            conditions = "DistrictCode in(" + ddlDistrict + ")  and BlockCode in(" + ddlBlock + ")";
            string strQry = "  SELECT PanchayatCode, dbo.TitleCase(upper(PanchayatName))  as PanchayatName FROM mstPanchayat where " + conditions + "  order by PanchayatName   ";
            dtDistrict = objMain.LoadData(strQry);
        }
        if (Convert.ToInt32(rblBlockType.SelectedValue) == 2)
        {
            conditions = "mst5Village.DistrictCode in(" + ddlDistrict + ")  and mst5Village.MainBlockCode in(" + ddlBlock + ")";
            string strQry = "  SELECT distinct mst5Village.PanchayatCode as PanchayatCode, dbo.TitleCase(upper(PanchayatName))  as PanchayatName from mst5Village   inner join mstPanchayat on mstPanchayat.PanchayatCode=mst5Village.PanchayatCode and mst5Village.BlockCode=mstPanchayat.BlockCode where " + conditions + "  order by PanchayatName   ";
            dtDistrict = objMain.LoadData(strQry);
        }



        // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        ddlPanchayat.DataSource = dtDistrict;
        ddlPanchayat.DataTextField = "PanchayatName";
        ddlPanchayat.DataValueField = "PanchayatCode";
        ddlPanchayat.DataBind();

        // objComman.BindDLL("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "--All--");


        chkVillage.Items.Clear();

    }
    public void FillCVillage()
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
        if (Convert.ToInt32(rblBlockType.SelectedValue) == 1)
        {
            conditions = "DistrictCode in(" + ddlDistrict + ")  and BlockCode in(" + ddlBlock + ") and  PanchayatCode in(" + ddlPhan + ")";
        }
        if (Convert.ToInt32(rblBlockType.SelectedValue) == 2)
        {
            conditions = "DistrictCode in(" + ddlDistrict + ")  and MainBlockCode in(" + ddlBlock + ") and  PanchayatCode in(" + ddlPhan + ")";

        }
        //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        //objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--All-");

        string strQry = "  SELECT VillageCode, dbo.TitleCase(upper(VillageName))  as VillageName FROM mst5Village where " + conditions + "  order by VillageName   ";
        DataTable dtDistrict = objMain.LoadData(strQry);

        chkVillage.DataSource = dtDistrict;
        chkVillage.DataTextField = "VillageName";
        chkVillage.DataValueField = "VillageCode";
        chkVillage.DataBind();


    }


}