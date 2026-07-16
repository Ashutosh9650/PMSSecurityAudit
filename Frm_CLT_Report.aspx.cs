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
using System.IO;
using System.Drawing;

public partial class Frm_CLT_Report : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    string conditions = "";
    string labelmainheading = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {

                LoadYear();
                LoadUserLeavel();
                ViewState["Button"] = "AA";
               
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
        }

    }
    public void LoadUserLeavel()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlstate, "StateName", "StateCode", "--Select--");
            ddlstate.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlstate, "StateName", "StateCode", "--Select--");

            ddlstate.SelectedIndex = 1;
            ddlstate.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlstate, "StateName", "StateCode", "--Select--");

            ddlstate.SelectedIndex = 1;
            ddlstate.Enabled = false;
            ddlDistrict.Enabled = false;
        }


        if (Session["user_level_Role"].ToString() == "1")
        {
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "";
            conditions = "StateCode ='" + ddlstate.SelectedValue + "' ";
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
            conditions = "StateCode ='" + ddlstate.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and Fyear= '" + ddlYear.SelectedItem.Text + "' ";
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

    public void FillCBState()
    {
        conditions = "";
        objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlstate, "StateName", "StateCode", "--Select--");



    }

    public void FillCBBock()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'";
        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--All--");



    }
    public void FillCBDist()
    {

        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "StateCode ='" + ddlstate.SelectedValue + "' and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "StateCode ='" + ddlstate.SelectedValue + "' and DistrictCode ='" + Session["DistrictCode"].ToString() + "'  and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else
        {
            conditions = "StateCode ='" + ddlstate.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";


        }


        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");



    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["Button"] = " ";
       FillCBDist();
        //LoadSchool();
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSchool();
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            ddlstate.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlDistrict, null);
            ddlDistrict.SelectedIndex = 1;
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
          
           
        }
        else
        {
            ddlstate.SelectedIndex = 0;
            ddlDistrict.Items.Clear();
            ddlBlock.Items.Clear();
           
           
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["Button"] = " ";
        FillCBBock();
       //LoadSchool();
      
    }

    public void getreportBaseline()
    {
        conditions = "";
        string conditionsGroupby = "";
        string conditionsSelect = "";
        string conditionsJoin = "";
        string subject = "";
        if (ddlType.SelectedIndex > 0 )
        {
            if (ddlstate.SelectedIndex > 0)
            {
                conditions += "  where  mst5Village.StateCode = '" + ddlstate.SelectedValue + "' ";

            }
            if (ddlDistrict.SelectedIndex > 0)
            {
                conditions += " and mst5Village.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";

            }
            if (ddlBlock.SelectedIndex > 0)
            {
                conditions += " and mst5Village.BlockCode = '" + ddlBlock.SelectedValue + "' ";


            }

            if (Convert.ToInt32(ddlType.SelectedValue) == 1)
            {
                // conditionsJoin = " inner join (select distinct blockcode,blockname from mst3Block) blk ON mst5Village.BlockCode = blk.BlockCode";
                conditionsSelect = " , count(distinct mst5Village.Blockcode ) as [Number of Block#]  ,count(distinct mst5Village.VillageName) as [Number of Village#],count(distinct s.Name) as [ Number of School#] ";


                // conditionsJoin += " inner join (select distinct PanchayatCode,PanchayatName from mstPanchayat) phy  ON mst5Village.PanchayatCode =phy.PanchayatCode ";
                //conditionsSelect += " ,phy.PanchayatName,mst5Village.VillageName,s.Name as SchoolName ";
                //conditionsGroupby += " ,phy.PanchayatName,mst5Village.VillageName,s.Name  ";
            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 2)
            {
                conditionsSelect = " , blk.BlockName  ,count(distinct mst5Village.VillageName) as [Number of Village#],count(distinct s.Name) as [ Number of School#] ";
                conditionsGroupby = " ,blk.BlockName ";
                conditionsJoin = " inner join (select distinct blockcode,blockname from mst3Block) blk ON mst5Village.BlockCode = blk.BlockCode";
            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 3)
            {
                conditionsJoin = " inner join (select distinct blockcode,blockname from mst3Block) blk ON mst5Village.BlockCode = blk.BlockCode";
                conditionsSelect = " ,blk.BlockName ";
                conditionsGroupby = " ,blk.BlockName ";

                conditionsJoin += " inner join (select distinct PanchayatCode,PanchayatName from mstPanchayat) phy  ON mst5Village.PanchayatCode =phy.PanchayatCode ";
                conditionsSelect += " ,phy.PanchayatName,mst5Village.VillageName,s.Name as SchoolName ";
                conditionsGroupby += " ,phy.PanchayatName,mst5Village.VillageName,s.Name  ";
            }
            if (ddlSchool.SelectedIndex > 0)
            {
                conditions += " and s.SchoolCode = '" + ddlSchool.SelectedValue + "' ";
            }
            if (ddlsubject.SelectedIndex > 0)
            {
                subject = ddlsubject.SelectedItem.Text;
            }
        }
        else
        {
            if (ddlstate.SelectedIndex > 0)
            {
                conditions += "  where  mst5Village.StateCode = '" + ddlstate.SelectedValue + "' ";

            }
            if (ddlDistrict.SelectedIndex > 0)
            {
                conditions += " and mst5Village.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";

                conditionsJoin = " inner join (select distinct blockcode,blockname from mst3Block) blk ON mst5Village.BlockCode = blk.BlockCode";
                conditionsSelect = " ,blk.BlockName ";
                conditionsGroupby = " ,blk.BlockName ";
            }
            if (ddlBlock.SelectedIndex > 0)
            {
                conditions += " and mst5Village.BlockCode = '" + ddlBlock.SelectedValue + "' ";

                conditionsJoin += " inner join (select distinct PanchayatCode,PanchayatName from mstPanchayat) phy  ON mst5Village.PanchayatCode =phy.PanchayatCode ";
                conditionsSelect += " ,phy.PanchayatName,mst5Village.VillageName,s.Name as SchoolName ";
                conditionsGroupby += " ,phy.PanchayatName,mst5Village.VillageName,s.Name  ";
            }
            if (ddlSchool.SelectedIndex > 0)
            {
                conditions += " and s.SchoolCode = '" + ddlSchool.SelectedValue + "' ";
            }
            if (ddlsubject.SelectedIndex > 0)
            {
                subject = ddlsubject.SelectedItem.Text;
            }
        }
        SqlParameter[] parm = new SqlParameter[]
            {
       new SqlParameter("@condition",  conditions),
        new SqlParameter("@conditionsSelect",  conditionsSelect),
        new SqlParameter("@conditionsJoin",  conditionsJoin),
         new SqlParameter("@conditionsGroupby",  conditionsGroupby),
       new SqlParameter("@subject",  subject),
        new SqlParameter("@flag",ddlsubject.SelectedValue),
      
                 };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Get_CLT_Report_DataBasLine]", parm);
        lblTotalCount.Text = dt.Rows.Count.ToString();
        ViewState["dt"] = (DataTable)dt;
        if (dt.Rows.Count > 0)
        {
            DGV_Report.DataSource = dt;
            DGV_Report.DataBind();

        }

        else
        {
            DGV_Report.DataSource = null;
            DGV_Report.DataBind();
        }

    }
    public void getreport()
    { 
          conditions = "";
        string  conditionsGroupby = "";
        string conditionsSelect = "";
        string conditionsJoin = "";
          string subject = "";
          if (ddlType.SelectedIndex > 0 && ddlDistrict.SelectedIndex >= 0)
          {
              if (ddlYear.SelectedIndex > 0)
              {
                  conditions += "  where  mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

              }
              if (ddlstate.SelectedIndex > 0)
              {
                  conditions += "  and  mst5Village.StateCode = '" + ddlstate.SelectedValue + "' ";

              }
              if (ddlDistrict.SelectedIndex > 0)
              {
                  conditions += " and mst5Village.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";

              }
              if (ddlBlock.SelectedIndex > 0)
              {
                  conditions += " and mst5Village.BlockCode = '" + ddlBlock.SelectedValue + "' ";


              }

              if (Convert.ToInt32(ddlType.SelectedValue) == 1)
              {
                 // conditionsJoin = " inner join (select distinct blockcode,blockname from mst3Block) blk ON mst5Village.BlockCode = blk.BlockCode";
                  conditionsSelect = " , count(distinct mst5Village.Blockcode ) as [Number of Block#]  ,count(distinct mst5Village.VillageName) as [Number of Village#],count(distinct s.Name) as [ Number of School#] ";
               

                 // conditionsJoin += " inner join (select distinct PanchayatCode,PanchayatName from mstPanchayat) phy  ON mst5Village.PanchayatCode =phy.PanchayatCode ";
                  //conditionsSelect += " ,phy.PanchayatName,mst5Village.VillageName,s.Name as SchoolName ";
                  //conditionsGroupby += " ,phy.PanchayatName,mst5Village.VillageName,s.Name  ";
              }
              if (Convert.ToInt32(ddlType.SelectedValue) == 2)
              {
                  conditionsSelect = " , blk.BlockName  ,count(distinct mst5Village.VillageName) as [Number of Village#],count(distinct s.Name) as [ Number of School#] ";
                  conditionsGroupby = " ,blk.BlockName ";
                  conditionsJoin = " inner join  mst3Block as blk ON mst5Village.BlockCode = blk.BlockCode";
              }
              if (Convert.ToInt32(ddlType.SelectedValue) == 3)
              {
                  conditionsJoin = " inner join mst3Block as blk ON mst5Village.BlockCode = blk.BlockCode";
                  conditionsSelect = " ,blk.BlockName ";
                  conditionsGroupby = " ,blk.BlockName ";

                  conditionsJoin += " inner join  mstPanchayat as phy  ON mst5Village.PanchayatCode =phy.PanchayatCode ";
                  conditionsSelect += " ,phy.PanchayatName,mst5Village.VillageName,s.Name as SchoolName ";
                  conditionsGroupby += " ,phy.PanchayatName,mst5Village.VillageName,s.Name  ";
              }
           
          }
          else
          {
              if (ddlstate.SelectedIndex > 0)
              {
                  conditions += "  where  mst5Village.StateCode = '" + ddlstate.SelectedValue + "' ";

              }
              if (ddlDistrict.SelectedIndex > 0)
              {
                  conditions += " and mst5Village.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";

                  conditionsJoin = " inner join (select distinct blockcode,blockname from mst3Block) blk ON mst5Village.BlockCode = blk.BlockCode";
                  conditionsSelect = " ,blk.BlockName ";
                  conditionsGroupby = " ,blk.BlockName ";
              }
              if (ddlBlock.SelectedIndex > 0)
              {
                  conditions += " and mst5Village.BlockCode = '" + ddlBlock.SelectedValue + "' ";

                  conditionsJoin += " inner join (select distinct PanchayatCode,PanchayatName from mstPanchayat) phy  ON mst5Village.PanchayatCode =phy.PanchayatCode ";
                  conditionsSelect += " ,phy.PanchayatName,mst5Village.VillageName,s.Name as SchoolName ";
                  conditionsGroupby += " ,phy.PanchayatName,mst5Village.VillageName,s.Name  ";
              }
          }

        if (ddlSchool.SelectedIndex > 0)
        {
            conditions += " and s.SchoolCode = '" + ddlSchool.SelectedValue + "' ";
        }
        if (ddlsubject.SelectedIndex > 0)
        {
            subject = ddlsubject.SelectedItem.Text;
        }
        SqlParameter[] parm = new SqlParameter[]
            {
       new SqlParameter("@condition",  conditions),
        new SqlParameter("@conditionsSelect",  conditionsSelect),
        new SqlParameter("@conditionsJoin",  conditionsJoin),
         new SqlParameter("@conditionsGroupby",  conditionsGroupby),
        new SqlParameter("@subject",  subject),
        new SqlParameter("@flag",ddlsubject.SelectedValue),
      
                 };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Get_CLT_Report_DataNew]", parm);
        lblTotalCount.Text = dt.Rows.Count.ToString();
        ViewState["dt"] = (DataTable)dt;
        if (dt.Rows.Count > 0)
        {
            DGV_Report.DataSource = dt;
            DGV_Report.DataBind();

        }

        else

        {
            DGV_Report.DataSource = null;
            DGV_Report.DataBind();
        }
    
    }

    public void getreport2()
    {
        conditions = "";
        string subject = "";
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "  where  mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlstate.SelectedIndex > 0)
        {
            conditions += "  and  mst5Village.StateCode = '" + ddlstate.SelectedValue + "' ";

        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " and v.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";

        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions += " and v.BlockCode = '" + ddlBlock.SelectedValue + "' ";

        }

        if (ddlsubject.SelectedIndex > 0)
        {
            subject = ddlsubject.SelectedItem.Text;
        }
        SqlParameter[] parm = new SqlParameter[]
            {
       new SqlParameter("@condition",  conditions),
       new SqlParameter("@subject",  subject),
        new SqlParameter("@flag",2),
      
                 };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Get_CLT_Report_Data]", parm);
        ViewState["dt"] =dt;
        lblTotalCount.Text = dt.Rows.Count.ToString();
        if (dt.Rows.Count > 0)
        {


            GridView1.DataSource = dt;
            GridView1.DataBind();

        }

        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

        }

    }


    protected void PMSBaseline_Click(object sender, EventArgs e)
    {
        gvRetaion.Visible = false;
            DGV_Report.Visible = true;
            rptD2D.Visible = false;
        //if (ddlsubject.SelectedIndex != 0)
        //{
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView1.Visible = false;

            ViewState["Button"] = "Baseline";
            btnexcel.Visible = true;
            if (ddlType.SelectedIndex > 0)
            {
                getreportBaseline();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Type')</script>", false);
            }
          
         
            labelmainheading = "Baseline Level Report";
        //}

        //else

        //{
        //    DGV_Report.DataSource = null;
        //    DGV_Report.DataBind();
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Subject')</script>", false);
        //}
    }
    protected void PMS_Click(object sender, EventArgs e)
    {
      
        //if (ddlsubject.SelectedIndex != 0)
        //{
            gvRetaion.Visible = false;
           DGV_Report.Visible = true;
            rptD2D.Visible = false;
             ViewState["Button"] = "PMS";
            btnexcel.Visible = true;

            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView1.Visible = false;
            if (ddlType.SelectedIndex > 0)
            {
                getreport();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Type')</script>", false);
            }
           
            labelmainheading = "Learning Level Report";
        //}

        //else

        //{
        //    DGV_Report.DataSource = null;
        //    DGV_Report.DataBind();
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Subject')</script>", false);
        //}
    }
    public void LoadSchool()
    {

        string conditions = "";
      
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += "  mst5Village.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";


        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions += " and mst5Village.BlockCode = '" + ddlBlock.SelectedValue + "' ";


        }
        

        objComman.BindDLL("mstSchool  inner join mst5Village on mst5Village.VillageCode=mstSchool.VillageCode ", "SchoolCode,Name", conditions, "Name", "asc", ddlSchool, "Name", "SchoolCode", "Select");

    }

    protected void Report2_Click(object sender, EventArgs e)
    {
        gvRetaion.Visible = false;
        DGV_Report.Visible = false;
        rptD2D.Visible = false;

        GridView1.DataSource = null;
        GridView1.DataBind();
        GridView1.Visible = true;
        btnexcel.Visible = true;
        ViewState["Button"] = "Report2";
        getreport2();
        labelmainheading = "";
    
    }
    protected void Report3_Click(object sender, EventArgs e)
    {
            gvRetaion.Visible = false;
            DGV_Report.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView1.Visible = false;
            string subject = "";
      
       
            rptD2D.Visible = true;
            if (ddlDistrict.SelectedIndex > 0)
            {
                conditions += " where mst5Village.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";

         
            }
            if (ddlBlock.SelectedIndex > 0)
            {
                conditions += " and mst5Village.BlockCode = '" + ddlBlock.SelectedValue + "' ";

               
            }
            if (ddlSchool.SelectedIndex > 0)
            {
                conditions += " and mstSchool.SchoolCode = '" + ddlSchool.SelectedValue + "' ";
            }

            if (ddlsubject.SelectedIndex > 0)
            {
                subject = ddlsubject.SelectedItem.Text;
            }
            DataTable dt = objMain.Baseline(conditions, subject,Convert.ToInt32(ddlsubject.SelectedValue));
            rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptBaselineReport.rdlc");
            ReportDataSource datasource = new ReportDataSource("Baseline", dt);
            //rptD2D.LocalReport.ReportPath = Server.MapPath("~/Report/rptSchoolBaseline.rdlc");
            //ReportDataSource datasource = new ReportDataSource("Baseline", dt);
            rptD2D.LocalReport.DataSources.Clear();
            rptD2D.LocalReport.DataSources.Add(datasource);


            rptD2D.Width = 600;

       

            rptD2D.LocalReport.DisplayName = "Baseline ";


      
       
    }


    protected void Retention_Click(object sender, EventArgs e)
    {
        gvRetaion.Visible = true;
        DGV_Report.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        rptD2D.Visible = false;
        GridView1.Visible = false;
        string subject = "";

        ViewState["Button"] = "LoadRetion";
     
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " where mst5Village.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";


        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions += " and mst5Village.BlockCode = '" + ddlBlock.SelectedValue + "' ";


        }

        DataTable dt = objMain.rptRetention(conditions);
        gvRetaion.DataSource = dt;
        gvRetaion.DataBind();
    }

    //protected void Export_To_Excel(object sender, EventArgs e)
    //{
    //    DataTable dtexcel = new DataTable();
    //    if (ViewState["dt"] != null)
    //    {
    //        dtexcel = (DataTable)ViewState["dt"];
    //    }
    //    excel(dtexcel);
    //}

    protected void DGV_Reports_RowCreated(object sender, GridViewRowEventArgs e)
    {

        //        Baseline
        #region Basline Endline
        if (ViewState["Button"].ToString() == "PMS")
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (ddlsubject.SelectedIndex > 0)
                    {

                        GridView HeaderGrid = (GridView)sender;
                        GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                        HeaderGridRow.CssClass = "gridnewheadercss";
                        TableCell HeaderCell;
                        string[] headerrow = { "DistrictName", "BlockName", "PanchayatName", "VillageName", "School Name", "Total Children", "% Attendance", "# Hindi A", "# Hindi B", "# Hindi C", "# Hindi D", "# Hindi E", "# Hindi X", "Hindi Average", "Hindi Possible Updgrade", "% Hindi Possible", "Total Children", "%Attendance", "# Hindi A", "# Hindi B", "# Hindi C", "# Hindi D", "# Hindi E", "# Hindi X", "Hindi Average", "Hindi Possible Updgrade", "% Hindi Possible Post" };

                        HeaderCell = new TableCell();
                        HeaderCell.Text = "Block Profile";
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                        if (ddlType.SelectedIndex > 0 && ddlDistrict.SelectedIndex >= 0)
                        {
                            if (Convert.ToInt32(ddlType.SelectedValue) == 1)
                            {
                                HeaderCell.ColumnSpan = 4;
                            }
                            if (Convert.ToInt32(ddlType.SelectedValue) == 2)
                            {
                                HeaderCell.ColumnSpan = 4;
                            }
                            if (Convert.ToInt32(ddlType.SelectedValue) == 3)
                            {
                                HeaderCell.ColumnSpan = 5;
                            }
                          
                        }
                        else
                        {
                            if (ddlDistrict.SelectedIndex <= 0)
                            {
                                HeaderCell.ColumnSpan = 1;
                            }
                            if (ddlDistrict.SelectedIndex > 0)
                            {
                                HeaderCell.ColumnSpan = 2;
                            }
                            if (ddlBlock.SelectedIndex > 0)
                            {
                                HeaderCell.ColumnSpan = 5;
                            }
                        }
                        //  HeaderCell.ColumnSpan = 5;
                        HeaderCell.CssClass = "gridnewheadercss";
                        HeaderGridRow.Cells.Add(HeaderCell);


                        HeaderCell = new TableCell();
                        HeaderCell.Text = "Learning Baseline ";
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                        if (ddlDistrict.SelectedIndex <= 0)
                        {
                            HeaderCell.ColumnSpan = 11;
                        }

                        if (ddlDistrict.SelectedIndex > 0)
                        {
                            HeaderCell.ColumnSpan = 11;
                        }
                        //  HeaderCell.ColumnSpan = 11;
                        HeaderCell.CssClass = "gridnewheadercss";
                        HeaderGridRow.Cells.Add(HeaderCell);


                        HeaderCell = new TableCell();
                        HeaderCell.Text = "LEARNING ENDLINE ";
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                        if (ddlDistrict.SelectedIndex <= 0)
                        {
                            HeaderCell.ColumnSpan = 9;
                        }

                        if (ddlDistrict.SelectedIndex > 0)
                        {
                            HeaderCell.ColumnSpan = 9;
                        }
                        //  HeaderCell.ColumnSpan = 10;

                        HeaderCell.CssClass = "gridnewheadercss";
                        HeaderGridRow.Cells.Add(HeaderCell);

                        HeaderCell = new TableCell();
                        HeaderCell.Text = "ANALYSIS";
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                        if (ddlDistrict.SelectedIndex <= 0)
                        {
                            HeaderCell.ColumnSpan = 3;
                        }
                        if (ddlDistrict.SelectedIndex > 0)
                        {
                            HeaderCell.ColumnSpan = 3;
                        }
                        //  HeaderCell.ColumnSpan = 3;
                        HeaderCell.CssClass = "gridnewheadercss";
                        HeaderGridRow.Cells.Add(HeaderCell);


                        //
                        DGV_Report.Controls[0].Controls.AddAt(0, HeaderGridRow);
                      
             
                    }
                    else
                    {

                        GridView HeaderGrid = (GridView)sender;
                        GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                        HeaderGridRow.CssClass = "gridnewheadercss";
                        TableCell HeaderCell;
                        string[] headerrow = { "DistrictName", "BlockName", "PanchayatName", "VillageName", "School Name", "Total Children", "% Attendance", "# Hindi A", "# Hindi B", "# Hindi C", "# Hindi D", "# Hindi E", "# Hindi X", "Hindi Average", "Hindi Possible Updgrade", "% Hindi Possible", "Total Children", "%Attendance", "# Hindi A", "# Hindi B", "# Hindi C", "# Hindi D", "# Hindi E", "# Hindi X", "Hindi Average", "Hindi Possible Updgrade", "% Hindi Possible Post" };

                        HeaderCell = new TableCell();
                        HeaderCell.Text = "Block Profile";
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                        if (ddlType.SelectedIndex > 0 && ddlDistrict.SelectedIndex >= 0)
                        {
                            if (Convert.ToInt32(ddlType.SelectedValue) == 1)
                            {
                                HeaderCell.ColumnSpan = 4;
                            }
                            if (Convert.ToInt32(ddlType.SelectedValue) == 2)
                            {
                                HeaderCell.ColumnSpan = 4;
                            }
                            if (Convert.ToInt32(ddlType.SelectedValue) == 3)
                            {
                                HeaderCell.ColumnSpan = 5;
                            }
                        }
                        else
                        {
                            if (ddlDistrict.SelectedIndex <= 0)
                            {
                                HeaderCell.ColumnSpan = 1;
                            }
                            if (ddlDistrict.SelectedIndex > 0)
                            {
                                HeaderCell.ColumnSpan = 2;
                            }
                            if (ddlBlock.SelectedIndex > 0)
                            {
                                HeaderCell.ColumnSpan = 5;
                            }
                        }
                      //  HeaderCell.ColumnSpan = 5;
                        HeaderCell.CssClass = "gridnewheadercss";
                        HeaderGridRow.Cells.Add(HeaderCell);


                        HeaderCell = new TableCell();
                        HeaderCell.Text = "Learning Baseline Hindi";
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                        if (ddlDistrict.SelectedIndex <= 0)
                        {
                            HeaderCell.ColumnSpan =11;
                        }

                        if (ddlDistrict.SelectedIndex > 0)
                        {
                            HeaderCell.ColumnSpan = 11;
                        }
                      //  HeaderCell.ColumnSpan = 11;
                        HeaderCell.CssClass = "gridnewheadercss";
                        HeaderGridRow.Cells.Add(HeaderCell);


                        HeaderCell = new TableCell();
                        HeaderCell.Text = "LEARNING ENDLINE HINDI";
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                        if (ddlDistrict.SelectedIndex <= 0)
                        {
                            HeaderCell.ColumnSpan = 9;
                        }

                        if (ddlDistrict.SelectedIndex > 0)
                        {
                            HeaderCell.ColumnSpan = 9;
                        }
                      //  HeaderCell.ColumnSpan = 10;

                        HeaderCell.CssClass = "gridnewheadercss";
                        HeaderGridRow.Cells.Add(HeaderCell);

                        HeaderCell = new TableCell();
                        HeaderCell.Text = "ANALYSIS";
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                        if (ddlDistrict.SelectedIndex <= 0)
                        {
                            HeaderCell.ColumnSpan =3;
                        }
                        if (ddlDistrict.SelectedIndex > 0)
                        {
                            HeaderCell.ColumnSpan = 3;
                        }
                      //  HeaderCell.ColumnSpan = 3;
                        HeaderCell.CssClass = "gridnewheadercss";
                        HeaderGridRow.Cells.Add(HeaderCell);


                        HeaderCell = new TableCell();
                        HeaderCell.Text = "Learning Baseline English";
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                        if (ddlDistrict.SelectedIndex <= 0)
                        {
                            HeaderCell.ColumnSpan = 9;
                        }

                        if (ddlDistrict.SelectedIndex > 0)
                        {
                            HeaderCell.ColumnSpan = 9;
                        }
                        //  HeaderCell.ColumnSpan = 11;
                        HeaderCell.CssClass = "gridnewheadercss";
                        HeaderGridRow.Cells.Add(HeaderCell);



                        HeaderCell = new TableCell();
                        HeaderCell.Text = "LEARNING ENDLINE ENGLISH";
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                        if (ddlDistrict.SelectedIndex <= 0)
                        {
                            HeaderCell.ColumnSpan = 9;
                        }

                        if (ddlDistrict.SelectedIndex > 0)
                        {
                            HeaderCell.ColumnSpan = 9;
                        }
                        HeaderCell.CssClass = "gridnewheadercss";
                        HeaderGridRow.Cells.Add(HeaderCell);


                        HeaderCell = new TableCell();
                        HeaderCell.Text = "ANALYSIS";
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                        if (ddlDistrict.SelectedIndex <= 0)
                        {
                            HeaderCell.ColumnSpan = 3;
                        }
                        if (ddlDistrict.SelectedIndex > 0)
                        {
                            HeaderCell.ColumnSpan = 3;
                        }
                        HeaderCell.CssClass = "gridnewheadercss";
                        HeaderGridRow.Cells.Add(HeaderCell);


                        HeaderCell = new TableCell();
                        HeaderCell.Text = "Learning Baseline Math";
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                        if (ddlDistrict.SelectedIndex <= 0)
                        {
                            HeaderCell.ColumnSpan = 9;
                        }

                        if (ddlDistrict.SelectedIndex > 0)
                        {
                            HeaderCell.ColumnSpan = 9;
                        }
                        //  HeaderCell.ColumnSpan = 11;
                        HeaderCell.CssClass = "gridnewheadercss";
                        HeaderGridRow.Cells.Add(HeaderCell);



                        HeaderCell = new TableCell();
                        HeaderCell.Text = "LEARNING ENDLINE MATH";
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                        if (ddlDistrict.SelectedIndex <= 0)
                        {
                            HeaderCell.ColumnSpan = 9;
                        }

                        if (ddlDistrict.SelectedIndex > 0)
                        {
                            HeaderCell.ColumnSpan = 9;
                        }
                        HeaderCell.CssClass = "gridnewheadercss";
                        HeaderGridRow.Cells.Add(HeaderCell);


                        HeaderCell = new TableCell();
                        HeaderCell.Text = "ANALYSIS";
                        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                        if (ddlDistrict.SelectedIndex <= 0)
                        {
                            HeaderCell.ColumnSpan = 3;
                        }
                        if (ddlDistrict.SelectedIndex > 0)
                        {
                            HeaderCell.ColumnSpan = 3;
                        }
                        HeaderCell.CssClass = "gridnewheadercss";
                        HeaderGridRow.Cells.Add(HeaderCell);

                   
                        //
                        DGV_Report.Controls[0].Controls.AddAt(0, HeaderGridRow);
                    }
                }
            }
        #endregion

        #region Basline

        if (ViewState["Button"].ToString() == "Baseline")
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (ddlsubject.SelectedIndex > 0)
                {

                    GridView HeaderGrid = (GridView)sender;
                    GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    HeaderGridRow.CssClass = "gridnewheadercss";
                    TableCell HeaderCell;
                    string[] headerrow = { "DistrictName", "BlockName", "PanchayatName", "VillageName", "School Name", "Total Children", "% Attendance", "# Hindi A", "# Hindi B", "# Hindi C", "# Hindi D", "# Hindi E", "# Hindi X", "Hindi Average", "Hindi Possible Updgrade", "% Hindi Possible", "Total Children", "%Attendance", "# Hindi A", "# Hindi B", "# Hindi C", "# Hindi D", "# Hindi E", "# Hindi X", "Hindi Average", "Hindi Possible Updgrade", "% Hindi Possible Post" };

                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Block Profile";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    if (ddlType.SelectedIndex > 0 && ddlDistrict.SelectedIndex >= 0)
                    {
                        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
                        {
                            HeaderCell.ColumnSpan = 4;
                        }
                        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
                        {
                            HeaderCell.ColumnSpan = 4;
                        }
                        if (Convert.ToInt32(ddlType.SelectedValue) == 3)
                        {
                            HeaderCell.ColumnSpan = 5;
                        }
                    }
                    else
                    {
                        if (ddlDistrict.SelectedIndex <= 0)
                        {
                            HeaderCell.ColumnSpan = 1;
                        }
                        if (ddlDistrict.SelectedIndex > 0)
                        {
                            HeaderCell.ColumnSpan = 2;
                        }
                        if (ddlBlock.SelectedIndex > 0)
                        {
                            HeaderCell.ColumnSpan = 5;
                        }
                    }
                    //  HeaderCell.ColumnSpan = 5;
                    HeaderCell.CssClass = "gridnewheadercss";
                    HeaderGridRow.Cells.Add(HeaderCell);


                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Learning Baseline ";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    if (ddlDistrict.SelectedIndex <= 0)
                    {
                        HeaderCell.ColumnSpan = 11;
                    }

                    if (ddlDistrict.SelectedIndex > 0)
                    {
                        HeaderCell.ColumnSpan = 11;
                    }
                    //  HeaderCell.ColumnSpan = 11;
                    HeaderCell.CssClass = "gridnewheadercss";
                    HeaderGridRow.Cells.Add(HeaderCell);


                 

                    ////HeaderCell = new TableCell();
                    ////HeaderCell.Text = "ANALYSIS";
                    ////HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    ////if (ddlDistrict.SelectedIndex <= 0)
                    ////{
                    ////    HeaderCell.ColumnSpan = 3;
                    ////}
                    ////if (ddlDistrict.SelectedIndex > 0)
                    ////{
                    ////    HeaderCell.ColumnSpan = 3;
                    ////}
                    ////  HeaderCell.ColumnSpan = 3;
                    //HeaderCell.CssClass = "gridnewheadercss";
                    //HeaderGridRow.Cells.Add(HeaderCell);


                    //
                    DGV_Report.Controls[0].Controls.AddAt(0, HeaderGridRow);


                }
                else
                {

                    GridView HeaderGrid = (GridView)sender;
                    GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    HeaderGridRow.CssClass = "gridnewheadercss";
                    TableCell HeaderCell;
                    string[] headerrow = { "DistrictName", "BlockName", "PanchayatName", "VillageName", "School Name", "Total Children", "% Attendance", "# Hindi A", "# Hindi B", "# Hindi C", "# Hindi D", "# Hindi E", "# Hindi X", "Hindi Average", "Hindi Possible Updgrade", "% Hindi Possible", "Total Children", "%Attendance", "# Hindi A", "# Hindi B", "# Hindi C", "# Hindi D", "# Hindi E", "# Hindi X", "Hindi Average", "Hindi Possible Updgrade", "% Hindi Possible Post" };

                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Block Profile";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    if (ddlType.SelectedIndex > 0 && ddlDistrict.SelectedIndex >= 0)
                    {
                        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
                        {
                            HeaderCell.ColumnSpan = 4;
                        }
                        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
                        {
                            HeaderCell.ColumnSpan = 4;
                        }
                        if (Convert.ToInt32(ddlType.SelectedValue) == 3)
                        {
                            HeaderCell.ColumnSpan = 5;
                        }
                    }
                    else
                    {
                        if (ddlDistrict.SelectedIndex <= 0)
                        {
                            HeaderCell.ColumnSpan = 1;
                        }
                        if (ddlDistrict.SelectedIndex >= 0)
                        {
                            HeaderCell.ColumnSpan = 2;
                        }
                        if (ddlBlock.SelectedIndex > 0)
                        {
                            HeaderCell.ColumnSpan = 5;
                        }
                    }
                    //  HeaderCell.ColumnSpan = 5;
                    HeaderCell.CssClass = "gridnewheadercss";
                    HeaderGridRow.Cells.Add(HeaderCell);


                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Learning Baseline Hindi";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    if (ddlDistrict.SelectedIndex <= 0)
                    {
                        HeaderCell.ColumnSpan = 11;
                    }

                    if (ddlDistrict.SelectedIndex > 0)
                    {
                        HeaderCell.ColumnSpan = 11;
                    }
                    //  HeaderCell.ColumnSpan = 11;
                    HeaderCell.CssClass = "gridnewheadercss";
                    HeaderGridRow.Cells.Add(HeaderCell);


                   

                    //HeaderCell = new TableCell();
                    //HeaderCell.Text = "ANALYSIS";
                    //HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    //if (ddlDistrict.SelectedIndex <= 0)
                    //{
                    //    HeaderCell.ColumnSpan = 3;
                    //}
                    //if (ddlDistrict.SelectedIndex > 0)
                    //{
                    //    HeaderCell.ColumnSpan = 3;
                    //}
                    ////  HeaderCell.ColumnSpan = 3;
                    //HeaderCell.CssClass = "gridnewheadercss";
                    //HeaderGridRow.Cells.Add(HeaderCell);


                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Learning Baseline English";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    if (ddlDistrict.SelectedIndex <= 0)
                    {
                        HeaderCell.ColumnSpan = 9;
                    }

                    if (ddlDistrict.SelectedIndex > 0)
                    {
                        HeaderCell.ColumnSpan = 9;
                    }
                    //  HeaderCell.ColumnSpan = 11;
                    HeaderCell.CssClass = "gridnewheadercss";
                    HeaderGridRow.Cells.Add(HeaderCell);





                    //HeaderCell = new TableCell();
                    //HeaderCell.Text = "ANALYSIS";
                    //HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    //if (ddlDistrict.SelectedIndex <= 0)
                    //{
                    //    HeaderCell.ColumnSpan = 3;
                    //}
                    //if (ddlDistrict.SelectedIndex > 0)
                    //{
                    //    HeaderCell.ColumnSpan = 3;
                    //}
                    //HeaderCell.CssClass = "gridnewheadercss";
                    //HeaderGridRow.Cells.Add(HeaderCell);


                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Learning Baseline Math";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    if (ddlDistrict.SelectedIndex <= 0)
                    {
                        HeaderCell.ColumnSpan = 9;
                    }

                    if (ddlDistrict.SelectedIndex > 0)
                    {
                        HeaderCell.ColumnSpan = 9;
                    }
                    //  HeaderCell.ColumnSpan = 11;
                    HeaderCell.CssClass = "gridnewheadercss";
                    HeaderGridRow.Cells.Add(HeaderCell);





                    //HeaderCell = new TableCell();
                    //HeaderCell.Text = "ANALYSIS";
                    //HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    //if (ddlDistrict.SelectedIndex <= 0)
                    //{
                    //    HeaderCell.ColumnSpan = 3;
                    //}
                    //if (ddlDistrict.SelectedIndex > 0)
                    //{
                    //    HeaderCell.ColumnSpan = 3;
                    //}
                    //HeaderCell.CssClass = "gridnewheadercss";
                    //HeaderGridRow.Cells.Add(HeaderCell);


                    //
                    DGV_Report.Controls[0].Controls.AddAt(0, HeaderGridRow);
                }
            }
        }
        #endregion
    }

    protected void gvRetaion_RowCreated(object sender, GridViewRowEventArgs e)
    {

        //        Baseline
     

        #region Basline

       
            if (e.Row.RowType == DataControlRowType.Header)
            {
               

                    GridView HeaderGrid = (GridView)sender;
                    GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    HeaderGridRow.CssClass = "gridnewheadercss";
                    TableCell HeaderCell;
                
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Dist Profile";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                   
                            HeaderCell.ColumnSpan = 10;
                        HeaderCell.ColumnSpan = 10;
                    HeaderCell.CssClass = "gridnewheadercss";
                    HeaderGridRow.Cells.Add(HeaderCell);


                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Enrolment Boys ";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                                     
                        HeaderCell.ColumnSpan = 9;
                    
                    HeaderCell.CssClass = "gridnewheadercss";
                    HeaderGridRow.Cells.Add(HeaderCell);


                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Enrolment Girl ";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                    HeaderCell.ColumnSpan = 9;

                    HeaderCell.CssClass = "gridnewheadercss";
                    HeaderGridRow.Cells.Add(HeaderCell);

                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Appear in Final Exam Boys";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                    HeaderCell.ColumnSpan = 9;

                    HeaderCell.CssClass = "gridnewheadercss";
                    HeaderGridRow.Cells.Add(HeaderCell);


                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Appear in Final Exam Girls";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                    HeaderCell.ColumnSpan = 9;

                    HeaderCell.CssClass = "gridnewheadercss";
                    HeaderGridRow.Cells.Add(HeaderCell);



                    HeaderCell = new TableCell();
                    HeaderCell.Text = "No. of Newly Enrolled Girls";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                    HeaderCell.ColumnSpan = 9;

                    HeaderCell.CssClass = "gridnewheadercss";
                    HeaderGridRow.Cells.Add(HeaderCell);




                    HeaderCell = new TableCell();
                    HeaderCell.Text = "No. of Girls appeared in Final Exam from Newly enrolled girls";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                    HeaderCell.ColumnSpan = 9;

                    HeaderCell.CssClass = "gridnewheadercss";
                    HeaderGridRow.Cells.Add(HeaderCell);
                    ////HeaderCell = new TableCell();
                    ////HeaderCell.Text = "ANALYSIS";
                    ////HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    ////if (ddlDistrict.SelectedIndex <= 0)
                    ////{
                    ////    HeaderCell.ColumnSpan = 3;
                    ////}
                    ////if (ddlDistrict.SelectedIndex > 0)
                    ////{
                    ////    HeaderCell.ColumnSpan = 3;
                    ////}
                    ////  HeaderCell.ColumnSpan = 3;
                    //HeaderCell.CssClass = "gridnewheadercss";
                    //HeaderGridRow.Cells.Add(HeaderCell);


                    //
                    gvRetaion.Controls[0].Controls.AddAt(0, HeaderGridRow);


               
            
        }
        #endregion
    }

    protected void excel(DataTable dtexcel)
    {
        string Rptnm = "";
        Rptnm = "EG_Report_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".xls";
        
        System.Text.StringBuilder sbb = new System.Text.StringBuilder();
        //sbb.Append("<table style='border=1;BACKGROUND-COLOR: #EAF1DD;'>");
        //if (dtexcel.Columns.Count > 0)
        //{
        //    sbb.Append("<tr style='BACKGROUND-COLOR: #0e7a9e;'><h3><td colspan=21 align=left height=100px  style='font-size:14pt;font-weight:bold;FONT-FAMILY: Arial'> " + labelmainheading + " </td></h3></tr>");
        //}

        //else
        //{
        //    sbb.Append("<tr style='BACKGROUND-COLOR: #81AB81;'><h3><td colspan=10 align=left height=100px  style='font-size:14pt;font-weight:bold;FONT-FAMILY: Arial'> " + labelmainheading + " </td></h3></tr>");

        //}
        //sbb.Append("<tr/> ");
       
        //    sbb.Append("<tr> <td>State : </td> <td>" + (ddlstate.SelectedItem.Text.Length == 0 ? " " : ddlstate.SelectedItem.Text) + "</td> <td>District : </td> <td>" + (ddlDistrict.SelectedItem.Text.Length == 0 ? " " : ddlDistrict.SelectedItem.Text) + "</td><td>Block : </td> <td>" + (ddlBlock.SelectedItem.Text.Length == 0 || ddlBlock.SelectedItem.Text == "--Select--" ? " " : ddlBlock.SelectedItem.Text) + "</td><td>Subject : </td> <td>" + ddlsubject.SelectedItem.Text + "</td>");

        //sbb.Append("<tr/> ");
        //sbb.Append("</table>");
       
        ExportToExcel _objExl = new ExportToExcel();

        _objExl.ExporttoExcel(dtexcel, sbb, Rptnm);
    }


    public void Export_To_Excel(object sender, EventArgs e)
    {

        if (ViewState["Button"].ToString() == "Report2")
        {
            DataTable dt = ViewState["dt"] as DataTable;
           
              ExporttoExcel(GridView1, dt);
        }
        else
        {
            LoadExecel();
        }

        if (ViewState["Button"].ToString() == "LoadRetion")
          {
              LoadRetion();
          }
    }
    
    public void LoadExecel()
    {



        if (DGV_Report.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=EG_Report_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                DGV_Report.AllowPaging = false;


                if (ViewState["Button"].ToString() == "PMS")
                {
                    getreport();
                }

                else
                {

                    getreportBaseline();
                }

                DGV_Report.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in DGV_Report.HeaderRow.Cells)
                {
                    cell.BackColor = DGV_Report.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in DGV_Report.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = DGV_Report.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = DGV_Report.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                DGV_Report.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }


    public void LoadRetion()
    {



        if (gvRetaion.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=EG_Retaion_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvRetaion.AllowPaging = false;
                Retention_Click(btnexcel,null);
                gvRetaion.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvRetaion.HeaderRow.Cells)
                {
                    cell.BackColor = gvRetaion.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvRetaion.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvRetaion.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvRetaion.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvRetaion.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //if (Page != null)
        //{
        //    Page.VerifyRenderingInServerForm(this);
        //}
       
        /* Verifies that the control is rendered */
    }
    private void ExporttoExcel(GridView Gv, DataTable table)
    {


        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");

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
        int columnscount = Gv.HeaderRow.Cells.Count;


        for (int j = 0; j < columnscount; j++)
        {      //write in new column
            HttpContext.Current.Response.Write("<Td>");
            //Get column headers  and make it as bold in excel columns
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(Gv.HeaderRow.Cells[j].Text);
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