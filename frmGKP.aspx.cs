using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class frmGKP : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    string conditions="";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {

               LoadYear();
               //Bindgrid();

            
             

            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }

           

        }

    }
    protected void btn_display_Click(object sender, System.EventArgs e)
    {
        gridbindNew();
        ///ModalAlert.Show();
    }
    protected void btnAddGkp_Click(object sender, System.EventArgs e)
    {
        ddMainlLevel.SelectedIndex = 0;
        TextBox4.Text = "";
        TextBox1.Text = "";
        TextBox2.Text = "";
        DropDownList2.SelectedIndex = 0;
        Label8.Text = "";
        Label6.Text = "";
        Label7.Text = "";
        Session["DgvAdd"] = "";
        GridView1.DataSource = null;
        GridView1.DataBind();
        ModalAlert.Show();
    }
    public void LoadYear()
    {
        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
     

    }
    public void gridbindNew()
    {
        string Condtion = "  Fyear ='" +  ddlYear.SelectedItem.Text + "'";
        //string cond = "  " + Convert.ToInt32(TextBox4.Text) + "";
        //string sbujectid = DropDownList2.SelectedValue.ToString();
        if (ddls.SelectedIndex > 0)
        {
            Condtion += " and mstGKPDeatils.SubjectID ='" + ddls.SelectedValue + "'";
        }
        //string sbjectlevel = TextBox1.Text;
        SqlParameter[] parm = new SqlParameter[]
                           {


                             new SqlParameter("@Condition", Condtion),
                              new SqlParameter("@Flag", "1"),

                             };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_DataNew", parm);
        DataTable dtgrid = new DataTable();
         if (dt.Rows.Count > 0)
        {

            DGV_CLT.DataSource = dt;
            DGV_CLT.DataBind();

            Session["Dgv_LeftGriddt"] = dt;
        }
        else
        {
            DGV_CLT.DataSource = null;
            DGV_CLT.DataBind();
            Session["Dgv_LeftGriddt"] = dtgrid;

        }


    }
    protected void ddMainlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Condtion = " Fyear ='" + ddlYear.SelectedItem.Text + "' and mstGKPDeatils.SubjectID=" + DropDownList2.SelectedValue.ToString() + " and LevelID ='" + ddMainlLevel.SelectedValue + "'";
        //string cond = "  " + Convert.ToInt32(TextBox4.Text) + "";
        //string sbujectid = DropDownList2.SelectedValue.ToString();
        //string sbjectlevel = TextBox1.Text;
        SqlParameter[] parm = new SqlParameter[]
                           {


                             new SqlParameter("@Condition", Condtion),
                              new SqlParameter("@Flag", "1"),
                             };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_DataNew", parm);
        GridView1.DataSource = dt;
        GridView1.DataBind();


        Session["DgvAdd"] = dt;

        Label8.Text = "";
        Label6.Text = "";
        Label7.Text = "";
        string Condtion1 = " Fyear ='" + ddlYear.SelectedItem.Text + "' and TypeID=1 and mstGKPDeatils.SubjectID=" + DropDownList2.SelectedValue.ToString() + " and LevelID ='" + ddMainlLevel.SelectedValue + "'";
        //string cond = "  " + Convert.ToInt32(TextBox4.Text) + "";
        //string sbujectid = DropDownList2.SelectedValue.ToString();
        //string sbjectlevel = TextBox1.Text;
        SqlParameter[] parm1 = new SqlParameter[]
                           {


                             new SqlParameter("@Condition", Condtion1),
                              new SqlParameter("@Flag", "1"),
                             };
        DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_DataNew", parm1);
        if (dt1.Rows.Count > 0)
        {
            Label6.Text = "No Of Session  :" + dt1.Rows.Count.ToString();
        }

        string Condtion11 = " Fyear ='" + ddlYear.SelectedItem.Text + "' and TypeID=2 and mstGKPDeatils.SubjectID=" + DropDownList2.SelectedValue.ToString() + " and LevelID ='" + ddMainlLevel.SelectedValue + "'";

        SqlParameter[] parm2 = new SqlParameter[]
                           {


                             new SqlParameter("@Condition", Condtion11),
                              new SqlParameter("@Flag", "1"),
                             };
        DataTable dt12 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_DataNew", parm2);
        if (dt12.Rows.Count > 0)
        {

            Label7.Text = "No Of Recap :" + dt12.Rows.Count.ToString();
           
        }

        string Condtion111 = " Fyear ='" + ddlYear.SelectedItem.Text + "' and TypeID=3 and mstGKPDeatils.SubjectID=" + DropDownList2.SelectedValue.ToString() + " and LevelID ='" + ddMainlLevel.SelectedValue + "'";


        SqlParameter[] parm3 = new SqlParameter[]
                           {


                             new SqlParameter("@Condition", Condtion111),
                              new SqlParameter("@Flag", "1"),
                             };
        DataTable dt123 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_DataNew", parm3);
        if (dt123.Rows.Count > 0)
        {
            Label8.Text = "No Of Remedial :" + dt123.Rows.Count.ToString();
            
        }
        ModalAlert.Show();
    }
    protected void NoOfSession_Click(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Subject')</script>", false);
            ModalAlert.Show();
            return;

        }
        if (TextBox4.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter No. of Session')</script>", false);
            ModalAlert.Show();
            return;

        }
        if (ddMainlLevel.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Label')</script>", false);
            ModalAlert.Show();
            return;
        }
        if (Convert.ToInt32(TextBox4.Text) > 6)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Max Session No. 5')</script>", false);
            ModalAlert.Show();
            return;
        }
        string Condtion = " Fyear ='" + ddlYear.SelectedItem.Text + "' and TypeID=1 and mstGKPDeatils.SubjectID=" + DropDownList2.SelectedValue.ToString() + " and LevelID ='" + ddMainlLevel.SelectedValue + "'";
        //string cond = "  " + Convert.ToInt32(TextBox4.Text) + "";
        //string sbujectid = DropDownList2.SelectedValue.ToString();
        //string sbjectlevel = TextBox1.Text;
        SqlParameter[] parm = new SqlParameter[]
                           {


                             new SqlParameter("@Condition", Condtion),
                              new SqlParameter("@Flag", "1"),
                             };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_DataNew", parm);
        DataTable dtgrid = new DataTable();
        int NumberOfsession = Convert.ToInt32(TextBox4.Text) ;
        if (dt.Rows.Count == 0)
        {
            DataTable dtNew = Session["DgvAdd"] as DataTable;
            if (dtNew == null || dtNew.Rows.Count == 0)
            {

                dtgrid = dt.Clone();
                for (int i = 1; i <= NumberOfsession; i++)
                {
                    DataRow dr = dtgrid.NewRow();

                    dr[0] = "0";
                    dr[1] = DropDownList2.SelectedValue.ToString();
                    dr[2] = DropDownList2.SelectedItem.Text;
                    dr[3] = ddMainlLevel.SelectedItem.Text.Trim();
                    dr[4] = "Session" + " " + i;
                    dr[5] = 3;
                    dr[6] = ddMainlLevel.SelectedValue;
                    dr[7] = 1;
                    dtgrid.Rows.Add(dr);
                }

            }
            else
            {
                dtgrid = dtNew.Copy();
                DataRow [] drNew =dtNew.Select("TypeID=1");
                NumberOfsession = 5 - drNew.Length;

                for (int i = drNew.Length + 1; i <= drNew.Length + NumberOfsession; i++)
                {
                    DataRow dr = dtgrid.NewRow();

                    dr[0] = "0";
                    dr[1] = DropDownList2.SelectedValue.ToString();
                    dr[2] = DropDownList2.SelectedItem.Text;
                    dr[3] = ddMainlLevel.SelectedItem.Text.Trim();
                    dr[4] = "Session" + " " + i;
                    dr[5] = 3;
                    dr[6] = ddMainlLevel.SelectedValue;
                    dr[7] = 1;
                    dtgrid.Rows.Add(dr);
                }
            }
           


            GridView1.DataSource = dtgrid;
            GridView1.DataBind();

            Session["DgvAdd"] = dtgrid;
        }
        else
        {
          
            Int32 icount = 5 - Convert.ToInt32(dt.Rows.Count);
            DataTable dtNew = Session["DgvAdd"] as DataTable;
            if (dtNew == null || dtNew.Rows.Count == 0)
            {

                dtgrid = dt.Clone();
                for (int i = 1; i <= icount; i++)
                {
                    DataRow dr = dtgrid.NewRow();

                    dr[0] = "0";
                    dr[1] = DropDownList2.SelectedValue.ToString();
                    dr[2] = DropDownList2.SelectedItem.Text;
                    dr[3] = ddMainlLevel.SelectedItem.Text;
                    dr[4] = "Session" + " " + i;
                    dr[6] = ddMainlLevel.SelectedValue;
                    dr[7] = 1;
                    dr[5] = 3;

                    dtgrid.Rows.Add(dr);
                }

            }
            else
            {
                dtgrid = dtNew.Copy();

                DataRow[] drNew = dtNew.Select("TypeID=1");
                NumberOfsession = 5 - drNew.Length;

                for (int i = drNew.Length + 1; i <= drNew.Length + NumberOfsession; i++)
                {
                    DataRow dr = dtgrid.NewRow();

                    dr[0] = "0";
                    dr[1] = DropDownList2.SelectedValue.ToString();
                    dr[2] = DropDownList2.SelectedItem.Text;
                    dr[3] = ddMainlLevel.SelectedItem.Text.Trim();
                    dr[4] = "Session" + " " + i;
                    dr[6] = ddMainlLevel.SelectedValue;
                    dr[7] = 1;
                    dr[5] = 3;
                    dtgrid.Rows.Add(dr);
                }
            }
          
         
            GridView1.DataSource = dtgrid;
            GridView1.DataBind();
            Session["DgvAdd"] = dtgrid;

            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Already Exist')", true);

        }
        ModalAlert.Show();
    }



    protected void NoOfRecap_Click(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select  subject')</script>", false);
            ModalAlert.Show();
            return;

        }
        if (TextBox1.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter no of Session')</script>", false);
            ModalAlert.Show();
            return;

        }
        if (ddMainlLevel.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Label')</script>", false);
            ModalAlert.Show();
            return;
        }
        if (Convert.ToInt32(TextBox1.Text) > 2)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Max Recap no 2')</script>", false);
            ModalAlert.Show();
            return;
        }
        string Condtion = " Fyear ='" + ddlYear.SelectedItem.Text + "' and TypeID=2 and mstGKPDeatils.SubjectID=" + DropDownList2.SelectedValue.ToString() + " and LevelID ='" + ddMainlLevel.SelectedValue + "'";
        //string cond = "  " + Convert.ToInt32(TextBox4.Text) + "";
        //string sbujectid = DropDownList2.SelectedValue.ToString();
        //string sbjectlevel = TextBox1.Text;
        SqlParameter[] parm = new SqlParameter[]
                           {


                             new SqlParameter("@Condition", Condtion),
                              new SqlParameter("@Flag", "1"),
                             };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_DataNew", parm);
        DataTable dtgrid = new DataTable();
        int NumberOfsession = Convert.ToInt32(TextBox1.Text);
         if (dt.Rows.Count == 0)
        {
            DataTable dtNew = Session["DgvAdd"] as DataTable;
            if (dtNew == null || dtNew.Rows.Count == 0)
            {

                dtgrid = dt.Clone();
                for (int i = 1; i <= NumberOfsession; i++)
                {
                    DataRow dr = dtgrid.NewRow();

                    dr[0] = "0";
                    dr[1] = DropDownList2.SelectedValue.ToString();
                    dr[2] = DropDownList2.SelectedItem.Text;
                    dr[3] = ddMainlLevel.SelectedItem.Text.Trim();
                    dr[4] = "Recap" + " " + i;
                    dr[6] = ddMainlLevel.SelectedValue;
                    dr[7] = 2;
                    dr[5] = 3;
                    dtgrid.Rows.Add(dr);
                }
            }
            else
            {
                dtgrid = dtNew.Copy();
                DataRow[] drNew = dtNew.Select("TypeID=2");
                if (drNew.Length > 0)
                {
                    NumberOfsession = 2 - drNew.Length;

                }
               
                for (int i = drNew.Length + 1; i <= drNew.Length + NumberOfsession; i++)
                {
                    DataRow dr = dtgrid.NewRow();

                    dr[0] = "0";
                    dr[1] = DropDownList2.SelectedValue.ToString();
                    dr[2] = DropDownList2.SelectedItem.Text;
                    dr[3] = ddMainlLevel.SelectedItem.Text.Trim();
                    dr[4] = "Recap" + " " + i;
                    dr[6] = ddMainlLevel.SelectedValue;
                    dr[7] = 2;
                    dr[5] = 3;
                    dtgrid.Rows.Add(dr);
                }
            }
       


            GridView1.DataSource = dtgrid;
            GridView1.DataBind();

            Session["DgvAdd"] = dtgrid;
        }
        else
        {
        
            Int32 icount = 2 - Convert.ToInt32(dt.Rows.Count);
            DataTable dtNew = Session["DgvAdd"] as DataTable;
            if (dtNew == null || dtNew.Rows.Count == 0)
            {

                dtgrid = dt.Clone();
                for (int i = 1; i <= icount; i++)
                {
                    DataRow dr = dtgrid.NewRow();

                    dr[0] = "0";
                    dr[1] = DropDownList2.SelectedValue.ToString();
                    dr[2] = DropDownList2.SelectedItem.Text;
                    dr[3] = ddMainlLevel.SelectedItem.Text;
                    dr[4] = "Recap" + " " + i;
                    dr[6] = ddMainlLevel.SelectedValue;
                    dr[7] = 2;
                    dr[5] = 3;
                    dtgrid.Rows.Add(dr);
                }
            }
            else
            {
                dtgrid = dtNew.Copy();

                DataRow[] drNew = dtNew.Select("TypeID=2");
                if (drNew.Length > 0)
                {
                    NumberOfsession = 2 - drNew.Length;
                }

                for (int i = drNew.Length + 1; i <= drNew.Length + NumberOfsession; i++)
                {
                    DataRow dr = dtgrid.NewRow();

                    dr[0] = "0";
                    dr[1] = DropDownList2.SelectedValue.ToString();
                    dr[2] = DropDownList2.SelectedItem.Text;
                    dr[3] = ddMainlLevel.SelectedItem.Text.Trim();
                    dr[4] = "Recap" + " " + i;
                    dr[6] = ddMainlLevel.SelectedValue;
                    dr[7] = 2;
                    dr[5] = 3;
                    dtgrid.Rows.Add(dr);
                }
            }
          
          
            GridView1.DataSource = dtgrid;
            GridView1.DataBind();
            Session["DgvAdd"] = dtgrid;

            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Already Exist')", true);

        
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Already Exist')", true);

        }
        ModalAlert.Show();
    }

    protected void NoOfRemedial_Click(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select  subject')</script>", false);
            ModalAlert.Show();
            return;

        }
        if (TextBox2.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter no of Session')</script>", false);
            ModalAlert.Show();
            return;

        }
        if (ddMainlLevel.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Label')</script>", false);
            ModalAlert.Show();
            return;
        }
        if (Convert.ToInt32(TextBox2.Text) > 2)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Max Remedial no 2')</script>", false);
            ModalAlert.Show();
            return;
        }
        string Condtion = " Fyear ='" + ddlYear.SelectedItem.Text + "' and TypeID=3 and mstGKPDeatils.SubjectID=" + DropDownList2.SelectedValue.ToString() + " and LevelID ='" + ddMainlLevel.SelectedValue + "'";
        //string cond = "  " + Convert.ToInt32(TextBox4.Text) + "";
        //string sbujectid = DropDownList2.SelectedValue.ToString();
        //string sbjectlevel = TextBox1.Text;
        SqlParameter[] parm = new SqlParameter[]
                           {


                             new SqlParameter("@Condition", Condtion),
                              new SqlParameter("@Flag", "1"),
                             };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_DataNew", parm);
        DataTable dtgrid = new DataTable();
        int NumberOfsession = Convert.ToInt32(TextBox2.Text);
         if (dt.Rows.Count == 0)
        {
            DataTable dtNew = Session["DgvAdd"] as DataTable;
            if (dtNew == null || dtNew.Rows.Count == 0)
            {

                dtgrid = dt.Clone();
                for (int i = 1; i <= NumberOfsession; i++)
                {
                    DataRow dr = dtgrid.NewRow();

                    dr[0] = "0";
                    dr[1] = DropDownList2.SelectedValue.ToString();
                    dr[2] = DropDownList2.SelectedItem.Text;
                    dr[3] = ddMainlLevel.SelectedItem.Text.Trim();
                    dr[4] = "Remedial" + " " + i;
                    dr[6] = ddMainlLevel.SelectedValue;
                    dr[7] = 3;
                    dr[5] = 3;
                    dtgrid.Rows.Add(dr);
                }

            }


            else
            {
                dtgrid = dtNew.Copy();
                DataRow[] drNew = dtNew.Select("TypeID=3");
                if (drNew.Length > 0)
                {
                    NumberOfsession = 2 - drNew.Length;
                }

                for (int i = drNew.Length + 1; i <= drNew.Length + NumberOfsession; i++)
                {
                    DataRow dr = dtgrid.NewRow();

                    dr[0] = "0";
                    dr[1] = DropDownList2.SelectedValue.ToString();
                    dr[2] = DropDownList2.SelectedItem.Text;
                    dr[3] = ddMainlLevel.SelectedItem.Text.Trim();
                    dr[4] = "Remedial" + " " + i;
                    dr[6] = ddMainlLevel.SelectedValue;
                    dr[7] = 3;
                    dr[5] = 3;
                    dtgrid.Rows.Add(dr);
                }
            }
         


            GridView1.DataSource = dtgrid;
            GridView1.DataBind();

            Session["DgvAdd"] = dtgrid;
        }
        else
        {
          
            Int32 icount = 2 - Convert.ToInt32(dt.Rows.Count);
            DataTable dtNew = Session["DgvAdd"] as DataTable;
            if (dtNew == null || dtNew.Rows.Count == 0)
            {

                dtgrid = dt.Clone();
                for (int i = 1; i <= icount; i++)
                {
                    DataRow dr = dtgrid.NewRow();

                    dr[0] = "0";
                    dr[1] = DropDownList2.SelectedValue.ToString();
                    dr[2] = DropDownList2.SelectedItem.Text;
                    dr[3] = ddMainlLevel.SelectedItem.Text;
                    dr[4] = "Remedial" + " " + i;
                    dr[6] = ddMainlLevel.SelectedValue;
                    dr[7] = 3;
                    dr[5] = 3;
                    dtgrid.Rows.Add(dr);
                }
            }
            else
            {
                dtgrid = dtNew.Copy();
                DataRow[] drNew = dtNew.Select("TypeID=3");
                if (drNew.Length > 0)
                {
                    NumberOfsession = 2 - drNew.Length;
                }

                for (int i = drNew.Length + 1; i <= drNew.Length + NumberOfsession; i++)
                {
                    DataRow dr = dtgrid.NewRow();

                    dr[0] = "0";
                    dr[1] = DropDownList2.SelectedValue.ToString();
                    dr[2] = DropDownList2.SelectedItem.Text;
                    dr[3] = ddMainlLevel.SelectedItem.Text.Trim();
                    dr[4] = "Remedial" + " " + i;
                    dr[6] = ddMainlLevel.SelectedValue;
                    dr[7] = 3;
                    dr[5] = 3;
                    dtgrid.Rows.Add(dr);
                }
            }
          
         
            GridView1.DataSource = dtgrid;
            GridView1.DataBind();
            Session["DgvAdd"] = dtgrid;

            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Already Exist')", true);

        
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Already Exist')", true);

        }
        ModalAlert.Show();
    }
    public void gridbind()
    {
        if (DropDownList2.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select  subject')</script>", false);
            ModalAlert.Show();
            return;
           
        }
        if (TextBox4.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter no of Session')</script>", false);
            ModalAlert.Show();
            return;
           
        }
        if (ddMainlLevel.SelectedIndex<=0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Label')</script>", false);
            ModalAlert.Show();
            return;
        }
        if (Convert.ToInt32(TextBox4.Text)>13)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Max Session no 13')</script>", false);
            ModalAlert.Show();
            return;
        }
        string Condtion = " Fyear ='" + ddlYear.SelectedItem.Text + "' and mstGKPDeatils.SubjectID=" + DropDownList2.SelectedValue.ToString() + " and LevelID ='" + ddMainlLevel.SelectedValue + "'";
        //string cond = "  " + Convert.ToInt32(TextBox4.Text) + "";
        //string sbujectid = DropDownList2.SelectedValue.ToString();
        //string sbjectlevel = TextBox1.Text;
        SqlParameter[] parm = new SqlParameter[]
                           {


                             new SqlParameter("@Condition", Condtion),
                              new SqlParameter("@Flag", "1"),
                             };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_DataNew", parm);
        DataTable dtgrid = new DataTable();
        int NumberOfsession = Convert.ToInt32(TextBox4.Text) - 1;
        if (dt.Rows.Count == 0)
        {

            dtgrid = dt.Clone();
            for (int i = 0; i <= NumberOfsession; i++)
            {
                DataRow dr = dtgrid.NewRow();

                dr[0] = "0";
                dr[1] = DropDownList2.SelectedValue.ToString();
                dr[2] = DropDownList2.SelectedItem.Text;
                dr[3] = ddMainlLevel.SelectedItem.Text.Trim();
                dr[6] = ddMainlLevel.SelectedValue;
                dtgrid.Rows.Add(dr);
            }


            DGV_CLT.DataSource = dtgrid;
            DGV_CLT.DataBind();

            Session["Dgv_LeftGriddt"] = dtgrid;
        }
        else
        {
            
            Int32 icount = 13-Convert.ToInt32( dt.Rows.Count);

               dtgrid = dt.Copy();
               for (int i = 0; i < icount; i++)
               {
                   DataRow dr = dtgrid.NewRow();

                   dr[0] = "0";
                   dr[1] = DropDownList2.SelectedValue.ToString();
                   dr[2] = DropDownList2.SelectedItem.Text;
                   dr[3] = ddMainlLevel.SelectedItem.Text;
                   dr[6] = ddMainlLevel.SelectedValue;

                   dtgrid.Rows.Add(dr);
               }
               DGV_CLT.DataSource = dtgrid;
               DGV_CLT.DataBind();
            Session["Dgv_LeftGriddt"] = dtgrid;

        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Already Exist')", true);

        }


    }
    protected void GvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label txtMainTypeID = (Label)e.Row.FindControl("txtMainTypeID");
            Label txtMain = (Label)e.Row.FindControl("txtMain");

            Label TxtRevision = (Label)e.Row.FindControl("TxtRevision");

            if (txtMainTypeID.Text == "3")
            {
                txtMain.Text = "Main";
                TxtRevision.Text = "Revision";
            }
            else if (txtMainTypeID.Text == "1")
            {
                txtMain.Text = "Main";
                TxtRevision.Text = "";
            }
            else if (txtMainTypeID.Text == "2")
            {
                TxtRevision.Text = "Revision";
                txtMain.Text = "";
            }
 
 
        }
    }

    protected void GvReport1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label txtMainTypeID = (Label)e.Row.FindControl("txtMainTypeID");
            CheckBox chkMain = (CheckBox)e.Row.FindControl("chkMain");

            CheckBox chkRevision = (CheckBox)e.Row.FindControl("chkRevision");

            if (txtMainTypeID.Text == "3")
            {
                chkRevision.Checked = true;
                chkMain.Checked = true;
            }
            else if (txtMainTypeID.Text == "1")
            {
                chkMain.Checked = true;
                chkRevision.Checked = false;
            }
            else if (txtMainTypeID.Text == "2")
            {
                chkRevision.Checked = true;
                chkMain.Checked = false;
            }


        }
    }
    protected void Dgv_LeftGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label subject = (Label)e.Row.FindControl("SubjectID");
            if (subject.Text == "1")
            {
                subject.Text = "Hindi";
            }
            if (subject.Text == "2")
            {
                subject.Text = "English";
            }
            if (subject.Text == "3")
            {
                subject.Text = "Maths";
            }
          
        }

    }

  

    protected void btn_show_Click(object sender, System.EventArgs e)
    {
        UpdateData();
        DataTable dt = (DataTable)Session["DgvAdd"];
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {

     
            Label lblSubject = (Label)GridView1.Rows[i].FindControl("lblSubject");
            Label txtNoOfSeesion = (Label)GridView1.Rows[i].FindControl("txtNoOfSeesion");
            Label lblLevel = (Label)GridView1.Rows[i].FindControl("lblLevel");


            Label txtMainID = (Label)GridView1.Rows[i].FindControl("txtMainID");

            
            if (txtNoOfSeesion.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Session name')</script>", false);
                return;
            }
            DataRow[] dr = dt.Select("NoofLevel='" + txtNoOfSeesion.Text.Trim() + "' and  SubjectID='" + lblSubject.Text + "' and  Level='" + lblLevel.Text + "' ");
            if (dr.Length > 1)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please check Duplicate values')</script>", false);
                return;
            }

        }

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {


            Label lblSubject = (Label)GridView1.Rows[i].FindControl("lblSubject");
            Label txtNoOfSeesion = (Label)GridView1.Rows[i].FindControl("txtNoOfSeesion");
           
            Label lblLevel = (Label)GridView1.Rows[i].FindControl("lblLevel");

            Label txtMainID = (Label)GridView1.Rows[i].FindControl("txtMainID");
            Label lblLevelID = (Label)GridView1.Rows[i].FindControl("Label4");

          
          
            CheckBox chkMain = (CheckBox)GridView1.Rows[i].FindControl("chkMain");
            CheckBox chkRevision = (CheckBox)GridView1.Rows[i].FindControl("chkRevision");
            Label lblTypeID = (Label)GridView1.Rows[i].FindControl("lblTypeID");
            
            int MainID = 0;
            if (chkMain.Checked == true && chkRevision.Checked == true)
            {
                MainID = 3;
            }
            else if (chkMain.Checked == true)
            {
                MainID = 1;
            }
            else if (chkMain.Checked == true)
            {
                MainID = 2;
            }
            SqlParameter[] parm = new SqlParameter[]
            {
           
           
            new SqlParameter("@ID",txtMainID.Text),
            new SqlParameter("@sbujectid", lblSubject.Text),
            new SqlParameter("@Level", lblLevel.Text),
            new SqlParameter("@sbjectlevel", txtNoOfSeesion.Text),
            new SqlParameter("@MainID", MainID),

            new SqlParameter("@Fyear", ddlYear.SelectedItem.Text),
               new SqlParameter("@LevelID", lblLevelID.Text),
               new SqlParameter("@TypeID", lblTypeID.Text),
            
              };
            int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateGkpMaster", parm);
            if (result > 0)
            {


                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Save Sucessfully')</script>", false);

            }



        }

        gridbindNew();
    }
    public void UpdateData()
    {

        DataTable dt = (DataTable)Session["DgvAdd"];

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {

            
            Label lblSubject = (Label)GridView1.Rows[i].FindControl("lblSubject");
            Label txtNoOfSeesion = (Label)GridView1.Rows[i].FindControl("txtNoOfSeesion");
            Label Label4 = (Label)GridView1.Rows[i].FindControl("Label4");
            Label lblLevel = (Label)GridView1.Rows[i].FindControl("lblLevel");

            Label txtMainID = (Label)GridView1.Rows[i].FindControl("txtMainID");
            Label lblLevelID = (Label)GridView1.Rows[i].FindControl("Label4");

            CheckBox chkMain = (CheckBox)GridView1.Rows[i].FindControl("chkMain");
            CheckBox chkRevision = (CheckBox)GridView1.Rows[i].FindControl("chkRevision");


            dt.Rows[i]["Level"] = lblLevel.Text;
            dt.Rows[i]["SubjectID"] = lblSubject.Text;
            dt.Rows[i]["ID"] = txtMainID.Text;
            dt.Rows[i]["NoofLevel"] = txtNoOfSeesion.Text;
            if (chkMain.Checked == true && chkRevision.Checked == true)
            {
                dt.Rows[i]["MainTypeID"] = 3;
            }
            else if (chkMain.Checked == true )
            {
                dt.Rows[i]["MainTypeID"] = 1;
            }
            else if (chkMain.Checked == true)
            {
                dt.Rows[i]["MainTypeID"] = 2;
            }
            dt.Rows[i]["LevelID"] = lblLevelID.Text;

        }

        Session["DgvAdd"] = dt;

    }

}