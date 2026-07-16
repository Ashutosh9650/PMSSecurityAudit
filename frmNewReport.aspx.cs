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
public partial class frmNewReport : System.Web.UI.Page
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
    public void LoadYear()
    {
        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            ddlState.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlDistrict, null);
            ddlDistrict.SelectedIndex = 1;
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);

            ddlPanchayat.Items.Clear();
            ddlVillage.Items.Clear();
        }
        else
        {
            ddlState.SelectedIndex = 0;
            ddlDistrict.Items.Clear();
            ddlBlock.Items.Clear();
            ddlPanchayat.Items.Clear();
            ddlVillage.Items.Clear();
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
        if (ddlState.SelectedIndex > 0)
        {
            conditions += "  mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlState.SelectedIndex > 0)
        {
            conditions += " and mst5Village.StateCode = '" + ddlState.SelectedValue + "' ";
            conditionsAll += "  StateCode = '" + ddlState.SelectedValue + "' ";
        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " and mst5Village.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";
            conditionsAll += "  and DistCode = '" + ddlDistrict.SelectedValue + "' ";
        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions += " and mst5Village.BlockCode = '" + ddlBlock.SelectedValue + "' ";
            conditionsAll += " and  BlockCode = '" + ddlBlock.SelectedValue + "' ";
        }
        if (ddlPanchayat.SelectedIndex > 0)
        {
            conditions += " and mst5Village.PanchayatCode = '" + ddlPanchayat.SelectedValue + "' ";
        }
        if (ddlVillage.SelectedIndex > 0)
        {
            conditions += " and mst5Village.VillageCode = '" + ddlVillage.SelectedValue + "' ";
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
                if (ddlState.SelectedIndex > 0)
                {
                    cond += " and mst5Village.StateCode = '" + ddlState.SelectedValue + "' ";
                   
                }
                if (ddlDistrict.SelectedIndex > 0)
                {
                    cond += " and mst5Village.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";
                   
                }
                if (ddlBlock.SelectedIndex > 0)
                {
                    cond += " and mst5Village.BlockCode = '" + ddlBlock.SelectedValue + "' ";
                  
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
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            ddlState.SelectedIndex = 1;
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            ddlState.SelectedIndex = 1;
            ddlState.Enabled = false;
            ddlDistrict.Enabled = false;
        }


        if (Session["user_level_Role"].ToString() == "1")
        {
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "";
            conditions = "StateCode ='" + ddlState.SelectedValue + "' ";
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

            ddlDistrict.SelectedIndex = 0;

            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }

        else
        {
            conditions = "";
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and Fyear= '" + ddlYear.SelectedItem.Text + "' ";
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

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
            ddlDistrict.SelectedIndex = 1;
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }





    }
    
    public void FillCBState()
    {
        conditions = "";
        objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");



    }
    public void FillCBDist()
    {

        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "StateCode ='" + ddlState.SelectedValue + "' and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode ='" + Session["DistrictCode"].ToString() + "'  and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else
        {
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";


        }


        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");



    }


    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
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
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'";
        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--All--");



    }
    public void FillCBCluster()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "'";
        objComman.BindDLL("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "--All--");



    }
    public void FillCVillage()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--All-");



    }


}