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

public partial class frmRetention : System.Web.UI.Page
{ 
    
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    string conditions = string.Empty, Flag = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                LoadYear();
                LoadUserLeavel();
          
                UserLevelFilter();
                btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
        }

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

       
       
       
    }
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSchool(); 
 
    }
    public void FillSchool()
    {
        conditions = "";
        conditions = "VillageCode ='" + ddlVillage.SelectedValue + "'  and FYear ='" + ddlYear.SelectedItem.Text + "'";
        objComman.BindDLL("mstSchool", "SchoolCode,Name", conditions, "Name", "asc", ddlSchool, "Name", "SchoolCode", "Select");


    }
 public void UserLevelFilter()
    {

        string strQry = "";
        string Cond = "Module='Retention Aggregate'";
        strQry = "Select * from MstUserRight  where " + Cond + " and Role_Id=" + Session["user_level"].ToString() + "   ";


        DataTable dtRole = objMain.LoadData(strQry);

        if (dtRole.Rows.Count > 0)
        {
            vADD = Convert.ToBoolean(dtRole.Rows[0]["AddStatus"].ToString());
            vVerify = Convert.ToBoolean(dtRole.Rows[0]["verify_Status"].ToString());
            vDelete = Convert.ToBoolean(dtRole.Rows[0]["Delete_status"].ToString());

            ViewState["vADD"] = vADD;
            ViewState["vVerify"] = vVerify;
            ViewState["vDelete"] = vDelete;
        }
        //if (vDelete == true)
        //{

        //    btnDelete.Visible = true;
        //}
        //else
        //{

        //    btnDelete.Visible = false;
        //}

        if (vADD == true)
        {
            btnsave.Enabled = true;

        }
        else
        {
            btnsave.Enabled = false;

        }
        if (vVerify == true)
        {



        }
        if (vVerify == true || vADD == true)
        {
            btnsave.Enabled = true;

        }
        else
        {
            btnsave.Enabled = false;

        }

    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        ClearData();
        LoadDataSearch("");
    }
    public void LoadDataSearch(string UnID)
    {
        string strQry = "";
        string cond = "";

        cond = "where tblRetention.Villagecode='" + ddlVillage.SelectedValue + "' and tblRetention.[Session] =" + ddlYear.SelectedValue + "";
        if (ddlSchool.SelectedIndex > 0)
        {
            cond += " and tblRetention.SchoolCode='" + ddlSchool.SelectedValue + "' ";

        }
        //strQry = "SELECT tblRetention.UniqueCode,tblRetention.VillageCode,tblRetention.Session,tblRetention.Class, tblRetention.EnrollBoys, tblRetention.EnrollGirls,tblRetention.AppearedGirls, tblRetention.AppearedBoys,tblRetention.NewEnrolledGirls, tblRetention.NewAppearedGirls, tblRetention.SchoolCode,name as SchoolName FROM tblRetention INNER JOIN mstSchool ON tblRetention.SchoolCode = mstSchool.SchoolCode " + cond + "  ";
        strQry = " SELECT  [UniqueCode]   ,tblRetention.[VillageCode]    ,tblRetention.[SchoolCode]      ,[Session]      ,[Class]      ,[EnrollBoys]      ,[EnrollGirls]      ,[AppearedGirls]      ,[AppearedBoys]      ,[NewEnrolledGirls]      ,[NewAppearedGirls]            ,tblRetention.[SysFlag]       FROM tblRetention INNER JOIN mstSchool ON tblRetention.SchoolCode = mstSchool.SchoolCode " + cond + "  ";

        //  string str = "SELECT * from tblRetention where UniqueCode ='" + UnID  + "' ";
        DataTable dtRetention = objMain.LoadData(strQry);

        if (dtRetention.Rows.Count > 0)
        {
           
            lblStudentId.Text = dtRetention.Rows[0]["UniqueCode"].ToString();
            foreach (DataRow dr in dtRetention.Rows)
            {
                #region Retention
                #region Class 1


                if (Convert.ToInt32(dr["Class"].ToString()) == 1)
                {
                    if (Convert.ToInt32(dr["EnrollBoys"].ToString()) != 0)
                    {
                        txtB1.Text = dr["EnrollBoys"].ToString();
                    }
                    if (Convert.ToInt32(dr["EnrollGirls"].ToString()) != 0)
                    {
                        txtG1.Text = dr["EnrollGirls"].ToString();
                    }
                    if (Convert.ToInt32(dr["AppearedBoys"].ToString()) != 0)
                    {
                        txtFB1.Text = dr["AppearedBoys"].ToString();
                    }
                    if (Convert.ToInt32(dr["AppearedGirls"].ToString()) != 0)
                    {
                        txtFG1.Text = dr["AppearedGirls"].ToString();
                    }
                    //if (Convert.ToInt32(dr["NewlyBoy"].ToString()) != 0)
                    //{
                    //    txtOB1.Text = dr["NewlyBoy"].ToString();
                    //}
                    if (Convert.ToInt32(dr["NewEnrolledGirls"].ToString()) != 0)
                    {
                        txtOG1.Text = dr["NewEnrolledGirls"].ToString();

                    }
                    //if (Convert.ToInt32(dr["AppearedPreBoy"].ToString()) != 0)
                    //{
                    //    txtAB1.Text = dr["AppearedPreBoy"].ToString();

                    //}

                    if (Convert.ToInt32(dr["NewAppearedGirls"].ToString()) != 0)
                    {
                        txtAG1.Text = dr["NewAppearedGirls"].ToString();

                    }

                    //txtBG1.Text = dr["EnrollBoys"].ToString() + dr["EnrollGirls"].ToString();
                    //TotalEnrollBoys += Convert.ToInt32(dr["EnrollBoys"].ToString());
                    //txtBToal.Text = TotalEnrollBoys.ToString();
                    //TotalEnrollBoys +=  Convert.ToInt32(dr["EnrollGirls"].ToString());
                    //txtTolalG.Text = TotalEnrollBoys.ToString();

                    //TotalEnrollOverAll=TotalEnrollBoys+TotalEnrollOverAll;
                    //txtToalBG.Text = TotalEnrollOverAll.ToString();

                }
                #endregion

                #region Class 2
                if (Convert.ToInt32(dr["Class"].ToString()) == 2)
                {
                    if (Convert.ToInt32(dr["EnrollBoys"].ToString()) != 0)
                    {
                        txtB2.Text = dr["EnrollBoys"].ToString();
                    }
                    if (Convert.ToInt32(dr["EnrollGirls"].ToString()) != 0)
                    {
                        txtG2.Text = dr["EnrollGirls"].ToString();
                    }
                    if (Convert.ToInt32(dr["AppearedBoys"].ToString()) != 0)
                    {
                        txtFB2.Text = dr["AppearedBoys"].ToString();
                    }
                    if (Convert.ToInt32(dr["AppearedGirls"].ToString()) != 0)
                    {
                        txtFG2.Text = dr["AppearedGirls"].ToString();
                    }
                    //if (Convert.ToInt32(dr["NewlyBoy"].ToString()) != 0)
                    //{
                    //    txtOB2.Text = dr["NewlyBoy"].ToString();
                    //}
                    if (Convert.ToInt32(dr["NewEnrolledGirls"].ToString()) != 0)
                    {
                        txtOG2.Text = dr["NewEnrolledGirls"].ToString();

                    }
                    //if (Convert.ToInt32(dr["AppearedPreBoy"].ToString()) != 0)
                    //{
                    //    txtAB2.Text = dr["AppearedPreBoy"].ToString();

                    //}

                    if (Convert.ToInt32(dr["NewAppearedGirls"].ToString()) != 0)
                    {
                        txtAG2.Text = dr["NewAppearedGirls"].ToString();

                    }

                    //txtBG2.Text = dr["EnrollBoys"].ToString() + dr["EnrollGirls"].ToString();
                    //TotalEnrollBoys += Convert.ToInt32(dr["EnrollBoys"].ToString());
                    //txtBToal.Text = TotalEnrollBoys.ToString();
                    //TotalEnrollBoys += Convert.ToInt32(dr["EnrollGirls"].ToString());
                    //txtTolalG.Text = TotalEnrollBoys.ToString();

                    //TotalEnrollOverAll = TotalEnrollBoys + TotalEnrollOverAll;
                    //txtToalBG.Text = TotalEnrollOverAll.ToString();
                }
                #endregion

                #region Class 3
                if (Convert.ToInt32(dr["Class"].ToString()) == 3)
                {
                    if (Convert.ToInt32(dr["EnrollBoys"].ToString()) != 0)
                    {
                        txtB3.Text = dr["EnrollBoys"].ToString();
                    }
                    if (Convert.ToInt32(dr["EnrollGirls"].ToString()) != 0)
                    {
                        txtG3.Text = dr["EnrollGirls"].ToString();
                    }
                    if (Convert.ToInt32(dr["AppearedBoys"].ToString()) != 0)
                    {
                        txtFB3.Text = dr["AppearedBoys"].ToString();
                    }
                    if (Convert.ToInt32(dr["AppearedGirls"].ToString()) != 0)
                    {
                        txtFG3.Text = dr["AppearedGirls"].ToString();
                    }
                    //if (Convert.ToInt32(dr["NewlyBoy"].ToString()) != 0)
                    //{
                    //    txtOB3.Text = dr["NewlyBoy"].ToString();
                    //}
                    if (Convert.ToInt32(dr["NewEnrolledGirls"].ToString()) != 0)
                    {
                        txtOG3.Text = dr["NewEnrolledGirls"].ToString();

                    }
                    //if (Convert.ToInt32(dr["AppearedPreBoy"].ToString()) != 0)
                    //{
                    //    txtAB3.Text = dr["AppearedPreBoy"].ToString();

                    //}

                    if (Convert.ToInt32(dr["NewAppearedGirls"].ToString()) != 0)
                    {
                        txtAG3.Text = dr["NewAppearedGirls"].ToString();

                    }

                }
                #endregion

                #region Class 4
                if (Convert.ToInt32(dr["Class"].ToString()) == 4)
                {
                    if (Convert.ToInt32(dr["EnrollBoys"].ToString()) != 0)
                    {
                        txtB4.Text = dr["EnrollBoys"].ToString();
                    }
                    if (Convert.ToInt32(dr["EnrollGirls"].ToString()) != 0)
                    {
                        txtG4.Text = dr["EnrollGirls"].ToString();
                    }
                    if (Convert.ToInt32(dr["AppearedBoys"].ToString()) != 0)
                    {
                        txtFB4.Text = dr["AppearedBoys"].ToString();
                    }
                    if (Convert.ToInt32(dr["AppearedGirls"].ToString()) != 0)
                    {
                        txtFG4.Text = dr["AppearedGirls"].ToString();
                    }
                    //if (Convert.ToInt32(dr["NewlyBoy"].ToString()) != 0)
                    //{
                    //    txtOB4.Text = dr["NewlyBoy"].ToString();
                    //}
                    if (Convert.ToInt32(dr["NewEnrolledGirls"].ToString()) != 0)
                    {
                        txtOG4.Text = dr["NewEnrolledGirls"].ToString();

                    }
                    //if (Convert.ToInt32(dr["AppearedPreBoy"].ToString()) != 0)
                    //{
                    //    txtAB4.Text = dr["AppearedPreBoy"].ToString();

                    //}

                    if (Convert.ToInt32(dr["NewAppearedGirls"].ToString()) != 0)
                    {
                        txtAG4.Text = dr["NewAppearedGirls"].ToString();

                    }
                }
                #endregion

                #region Class 5
                if (Convert.ToInt32(dr["Class"].ToString()) == 5)
                {
                    if (Convert.ToInt32(dr["EnrollBoys"].ToString()) != 0)
                    {
                        txtB5.Text = dr["EnrollBoys"].ToString();
                    }
                    if (Convert.ToInt32(dr["EnrollGirls"].ToString()) != 0)
                    {
                        txtG5.Text = dr["EnrollGirls"].ToString();
                    }
                    if (Convert.ToInt32(dr["AppearedBoys"].ToString()) != 0)
                    {
                        txtFB5.Text = dr["AppearedBoys"].ToString();
                    }
                    if (Convert.ToInt32(dr["AppearedGirls"].ToString()) != 0)
                    {
                        txtFG5.Text = dr["AppearedGirls"].ToString();
                    }
                    //if (Convert.ToInt32(dr["NewlyBoy"].ToString()) != 0)
                    //{
                    //    txtOB5.Text = dr["NewlyBoy"].ToString();
                    //}
                    if (Convert.ToInt32(dr["NewEnrolledGirls"].ToString()) != 0)
                    {
                        txtOG5.Text = dr["NewEnrolledGirls"].ToString();

                    }
                    //if (Convert.ToInt32(dr["AppearedPreBoy"].ToString()) != 0)
                    //{
                    //    txtAB5.Text = dr["AppearedPreBoy"].ToString();

                    //}

                    if (Convert.ToInt32(dr["NewAppearedGirls"].ToString()) != 0)
                    {
                        txtAG5.Text = dr["NewAppearedGirls"].ToString();

                    }
                }
                #endregion

                #region Class 6
                if (Convert.ToInt32(dr["Class"].ToString()) == 6)
                {
                    if (Convert.ToInt32(dr["EnrollBoys"].ToString()) != 0)
                    {
                        txtB6.Text = dr["EnrollBoys"].ToString();
                    }
                    if (Convert.ToInt32(dr["EnrollGirls"].ToString()) != 0)
                    {
                        txtG6.Text = dr["EnrollGirls"].ToString();
                    }
                    if (Convert.ToInt32(dr["AppearedBoys"].ToString()) != 0)
                    {
                        txtFB6.Text = dr["AppearedBoys"].ToString();
                    }
                    if (Convert.ToInt32(dr["AppearedGirls"].ToString()) != 0)
                    {
                        txtFG6.Text = dr["AppearedGirls"].ToString();
                    }
                    //if (Convert.ToInt32(dr["NewlyBoy"].ToString()) != 0)
                    //{
                    //    txtOB6.Text = dr["NewlyBoy"].ToString();
                    //}
                    if (Convert.ToInt32(dr["NewEnrolledGirls"].ToString()) != 0)
                    {
                        txtOG6.Text = dr["NewEnrolledGirls"].ToString();

                    }
                    //if (Convert.ToInt32(dr["AppearedPreBoy"].ToString()) != 0)
                    //{
                    //    txtAB6.Text = dr["AppearedPreBoy"].ToString();

                    //}

                    if (Convert.ToInt32(dr["NewAppearedGirls"].ToString()) != 0)
                    {
                        txtAG6.Text = dr["NewAppearedGirls"].ToString();

                    }
                }
                #endregion

                #region Class 7
                if (Convert.ToInt32(dr["Class"].ToString()) == 7)
                {
                    if (Convert.ToInt32(dr["EnrollBoys"].ToString()) != 0)
                    {
                        txtB7.Text = dr["EnrollBoys"].ToString();
                    }
                    if (Convert.ToInt32(dr["EnrollGirls"].ToString()) != 0)
                    {
                        txtG7.Text = dr["EnrollGirls"].ToString();
                    }
                    if (Convert.ToInt32(dr["AppearedBoys"].ToString()) != 0)
                    {
                        txtFB7.Text = dr["AppearedBoys"].ToString();
                    }
                    if (Convert.ToInt32(dr["AppearedGirls"].ToString()) != 0)
                    {
                        txtFG7.Text = dr["AppearedGirls"].ToString();
                    }
                    //if (Convert.ToInt32(dr["NewlyBoy"].ToString()) != 0)
                    //{
                    //    txtOB7.Text = dr["NewlyBoy"].ToString();
                    //}
                    if (Convert.ToInt32(dr["NewEnrolledGirls"].ToString()) != 0)
                    {
                        txtOG7.Text = dr["NewEnrolledGirls"].ToString();

                    }
                    //if (Convert.ToInt32(dr["AppearedPreBoy"].ToString()) != 0)
                    //{
                    //    txtAB7.Text = dr["AppearedPreBoy"].ToString();

                    //}

                    if (Convert.ToInt32(dr["NewAppearedGirls"].ToString()) != 0)
                    {
                        txtAG7.Text = dr["NewAppearedGirls"].ToString();

                    }
                }
                #endregion

                #region Class 8
                if (Convert.ToInt32(dr["Class"].ToString()) == 8)
                {
                    if (Convert.ToInt32(dr["EnrollBoys"].ToString()) != 0)
                    {
                        txtB8.Text = dr["EnrollBoys"].ToString();
                    }
                    if (Convert.ToInt32(dr["EnrollGirls"].ToString()) != 0)
                    {
                        txtG8.Text = dr["EnrollGirls"].ToString();
                    }
                    if (Convert.ToInt32(dr["AppearedBoys"].ToString()) != 0)
                    {
                        txtFB8.Text = dr["AppearedBoys"].ToString();
                    }
                    if (Convert.ToInt32(dr["AppearedGirls"].ToString()) != 0)
                    {
                        txtFG8.Text = dr["AppearedGirls"].ToString();
                    }
                    //if (Convert.ToInt32(dr["NewlyBoy"].ToString()) != 0)
                    //{
                    //    txtOB8.Text = dr["NewlyBoy"].ToString();
                    //}
                    if (Convert.ToInt32(dr["NewEnrolledGirls"].ToString()) != 0)
                    {
                        txtOG8.Text = dr["NewEnrolledGirls"].ToString();

                    }
                    //if (Convert.ToInt32(dr["AppearedPreBoy"].ToString()) != 0)
                    //{
                    //    txtAB8.Text = dr["AppearedPreBoy"].ToString();

                    //}

                    if (Convert.ToInt32(dr["NewAppearedGirls"].ToString()) != 0)
                    {
                        txtAG8.Text = dr["NewAppearedGirls"].ToString();

                    }
                }
                #endregion



                #endregion
            }
            CalculatetxtvalOnlyGirl(txtAG1, txtAG2, txtAG3, txtAG4, txtAG5, txtAG6, txtAG7, txtAG8, TxtIA2);



            CalculatetxtvalOnlyGirl(txtOG1, txtOG2, txtOG3, txtOG4, txtOG5, txtOG6, txtOG7, txtOG8, TxtIA1);



            CalculatetxtvalGirlBoy(txtFB1, txtFB2, txtFB3, txtFB4, txtFB5, txtFB6, txtFB7, txtFB8, TxtIET1, txtFG1, txtFG2, txtFG3, txtFG4, txtFG5, txtFG6, txtFG7, txtFG8, TxtIE2);





            CalculatetxtvalGirlBoy(txtB1, txtB2, txtB3, txtB4, txtB5, txtB6, txtB7, txtB8, TxtIPBET1, txtG1, txtG2, txtG3, txtG4, txtG5, txtG6, txtG7, txtG8, TxtIPBGT1);

          

            pnlMain1.Enabled = true;
        }
        else
        {
            pnlMain1.Enabled = true;
        }
    }
    public void CalculatetxtvalOnlyGirl(TextBox G1, TextBox G2, TextBox G3, TextBox G4, TextBox G5, TextBox G6, TextBox G7, TextBox G8, TextBox Gsum)
    {
        int val = 0;
        int Gval = 0;
        try
        {


            Gval = Convert.ToInt32(G1.Text.Trim() == "" ? "0" : G1.Text.Trim())
                + Convert.ToInt32(G2.Text.Trim() == "" ? "0" : G2.Text.Trim())
                + Convert.ToInt32(G3.Text.Trim() == "" ? "0" : G3.Text.Trim())
                + Convert.ToInt32(G4.Text.Trim() == "" ? "0" : G4.Text.Trim())
                + Convert.ToInt32(G5.Text.Trim() == "" ? "0" : G5.Text.Trim())
                + Convert.ToInt32(G6.Text.Trim() == "" ? "0" : G6.Text.Trim())
                + Convert.ToInt32(G7.Text.Trim() == "" ? "0" : G7.Text.Trim())
                + Convert.ToInt32(G8.Text.Trim() == "" ? "0" : G8.Text.Trim());
            Gsum.Text = Gval.ToString();
            int total = Gval + val;

        }
        catch (Exception)
        {

            throw;
        }
    }

    public void CalculatetxtvalGirlBoy(TextBox t1, TextBox t2, TextBox t3, TextBox t4, TextBox t5, TextBox t6, TextBox t7, TextBox t8, TextBox Bsum, TextBox G1, TextBox G2, TextBox G3, TextBox G4, TextBox G5, TextBox G6, TextBox G7, TextBox G8, TextBox Gsum)
    {
        int val = 0;
        int Gval = 0;
        try
        {
            val = Convert.ToInt32(t1.Text.Trim() == "" ? "0" : t1.Text.Trim())
                + Convert.ToInt32(t2.Text.Trim() == "" ? "0" : t2.Text.Trim())
                + Convert.ToInt32(t3.Text.Trim() == "" ? "0" : t3.Text.Trim())
                + Convert.ToInt32(t4.Text.Trim() == "" ? "0" : t4.Text.Trim())
                + Convert.ToInt32(t5.Text.Trim() == "" ? "0" : t5.Text.Trim())
                + Convert.ToInt32(t6.Text.Trim() == "" ? "0" : t6.Text.Trim())
                + Convert.ToInt32(t7.Text.Trim() == "" ? "0" : t7.Text.Trim())
                + Convert.ToInt32(t8.Text.Trim() == "" ? "0" : t8.Text.Trim());
            Bsum.Text = val.ToString();


            Gval = Convert.ToInt32(G1.Text.Trim() == "" ? "0" : G1.Text.Trim())
                + Convert.ToInt32(G2.Text.Trim() == "" ? "0" : G2.Text.Trim())
                + Convert.ToInt32(G3.Text.Trim() == "" ? "0" : G3.Text.Trim())
                + Convert.ToInt32(G4.Text.Trim() == "" ? "0" : G4.Text.Trim())
                + Convert.ToInt32(G5.Text.Trim() == "" ? "0" : G5.Text.Trim())
                + Convert.ToInt32(G6.Text.Trim() == "" ? "0" : G6.Text.Trim())
                + Convert.ToInt32(G7.Text.Trim() == "" ? "0" : G7.Text.Trim())
                + Convert.ToInt32(G8.Text.Trim() == "" ? "0" : G8.Text.Trim());
            Gsum.Text = Gval.ToString();
         
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void btnsave_Click(object sender, EventArgs e)
    {
        #region Enrollment Validate Boy
        Int32 pboy1 = 0;
        Int32 aboy1 = 0;
        if (txtB1.Text.Trim() != "")
        {
            pboy1 = Convert.ToInt32(txtB1.Text);
        }
        if (txtFB1.Text.Trim() != "")
        {
            aboy1 = Convert.ToInt32(txtFB1.Text);
        }

        if (aboy1 > pboy1)
        {
            

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enrollment Boy should be higher or equal to Appeared')</script>", false);
            this.txtFB1.Focus();
            txtFB1.Text = "";
            return;

        }
        Int32 pboy2 = 0;
        Int32 aboy2 = 0;
        if (txtB2.Text.Trim() != "")
        {
            pboy2 = Convert.ToInt32(txtB2.Text);
        }
        if (txtFB2.Text.Trim() != "")
        {
            aboy2 = Convert.ToInt32(txtFB2.Text);
        }

        if (aboy2 > pboy2)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Enrollment is greater or equal to Appeared')</script>", false);
            this.txtFB2.Focus();
            txtFB2.Text = "";
            return;

        }

        Int32 pboy3 = 0;
        Int32 aboy3 = 0;
        if (txtB3.Text.Trim() != "")
        {
            pboy3 = Convert.ToInt32(txtB3.Text);
        }
        if (txtFB3.Text.Trim() != "")
        {
            aboy3 = Convert.ToInt32(txtFB3.Text);
        }

        if (aboy3 > pboy3)
        {
          
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Enrollment is greater or equal to Appeared')</script>", false);
            this.txtFB3.Focus();
            txtFB3.Text = "";
            return;

        }

        Int32 pboy4 = 0;
        Int32 aboy4 = 0;
        if (txtB4.Text.Trim() != "")
        {
            pboy4 = Convert.ToInt32(txtB4.Text);
        }
        if (txtFB4.Text.Trim() != "")
        {
            aboy4 = Convert.ToInt32(txtFB4.Text);
        }

        if (aboy4 > pboy4)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Enrollment is greater or equal to Appeared')</script>", false);
            this.txtFB1.Focus();
            txtFB4.Text = "";
            return;

        }

        Int32 pboy5 = 0;
        Int32 aboy5 = 0;
        if (txtB5.Text.Trim() != "")
        {
            pboy5 = Convert.ToInt32(txtB5.Text);
        }
        if (txtFB5.Text.Trim() != "")
        {
            aboy5 = Convert.ToInt32(txtFB5.Text);
        }

        if (aboy5 > pboy5)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Enrollment is greater or equal to Appeared')</script>", false);
            this.txtFB5.Focus();

            txtFB5.Text = "";
            return;
        }


        Int32 pboy6 = 0;
        Int32 aboy6 = 0;
        if (txtB6.Text.Trim() != "")
        {
            pboy6 = Convert.ToInt32(txtB6.Text);
        }
        if (txtFB6.Text.Trim() != "")
        {
            aboy6 = Convert.ToInt32(txtFB6.Text);
        }

        if (aboy6 > pboy6)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Enrollment is greater or equal to Appeared')</script>", false);
            this.txtFB6.Focus();

            txtFB6.Text = "";
            return;
        }


        Int32 pboy7 = 0;
        Int32 aboy7 = 0;
        if (txtB7.Text.Trim() != "")
        {
            pboy7 = Convert.ToInt32(txtB7.Text);
        }
        if (txtFB7.Text.Trim() != "")
        {
            aboy7 = Convert.ToInt32(txtFB7.Text);
        }

        if (aboy7 > pboy7)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Enrollment is greater or equal to Appeared')</script>", false);
            this.txtFB7.Focus();
            txtFB7.Text = "";
            return;

        }



        Int32 pboy8 = 0;
        Int32 aboy8 = 0;
        if (txtB8.Text.Trim() != "")
        {
            pboy8 = Convert.ToInt32(txtB8.Text);
        }
        if (txtFB8.Text.Trim() != "")
        {
            aboy8 = Convert.ToInt32(txtFB8.Text);
        }

        if (aboy8 > pboy8)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Enrollment is greater or equal to Appeared')</script>", false);
            this.txtFB8.Focus();
            txtFB8.Text = "";
            return;

        }

        #endregion


        #region Enrollment Validate Gril
        Int32 egirl1 = 0;
        Int32 agirl1 = 0;
        if (txtG1.Text.Trim() != "")
        {
            egirl1 = Convert.ToInt32(txtG1.Text);
        }
        if (txtFG1.Text.Trim() != "")
        {
            agirl1 = Convert.ToInt32(txtFG1.Text);
        }

        if (agirl1 > egirl1)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enrollment Boy should be higher or equal to Appeared ')</script>", false);
            this.txtFG1.Focus();
            txtFG1.Text = "";
            return;

        }
        Int32 egirl2 = 0;
        Int32 agirl2 = 0;
        if (txtG2.Text.Trim() != "")
        {
            egirl2 = Convert.ToInt32(txtG2.Text);
        }
        if (txtFG2.Text.Trim() != "")
        {
            agirl2 = Convert.ToInt32(txtFG2.Text);
        }

        if (agirl2 > egirl2)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Enrollment is greater or equal to Appeared')</script>", false);
            this.txtFG2.Focus();
            txtFG2.Text = "";
            return;

        }

        Int32 egirl3 = 0;
        Int32 agirl3 = 0;
        if (txtG3.Text.Trim() != "")
        {
            egirl3 = Convert.ToInt32(txtG3.Text);
        }
        if (txtFG3.Text.Trim() != "")
        {
            agirl3 = Convert.ToInt32(txtFG3.Text);
        }

        if (agirl3 > egirl3)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Enrollment is greater or equal to Appeared')</script>", false);
            this.txtFG3.Focus();
            txtFG3.Text = "";
            return;

        }

        Int32 egirl4 = 0;
        Int32 agirl4 = 0;
        if (txtG4.Text.Trim() != "")
        {
            egirl4 = Convert.ToInt32(txtG4.Text);
        }
        if (txtFG4.Text.Trim() != "")
        {
            agirl4 = Convert.ToInt32(txtFG4.Text);
        }

        if (agirl4 > egirl4)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Enrollment is greater or equal to Appeared')</script>", false);
            this.txtFG1.Focus();
            txtFG4.Text = "";
            return;

        }

        Int32 egirl5 = 0;
        Int32 agirl5 = 0;
        if (txtG5.Text.Trim() != "")
        {
            egirl5 = Convert.ToInt32(txtG5.Text);
        }
        if (txtFG5.Text.Trim() != "")
        {
            agirl5 = Convert.ToInt32(txtFG5.Text);
        }

        if (agirl5 > egirl5)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Enrollment is greater or equal to Appeared')</script>", false);
            this.txtFG5.Focus();

            txtFG5.Text = "";
            return;
        }


        Int32 egirl6 = 0;
        Int32 agirl6 = 0;
        if (txtG6.Text.Trim() != "")
        {
            egirl6 = Convert.ToInt32(txtG6.Text);
        }
        if (txtFG6.Text.Trim() != "")
        {
            agirl6 = Convert.ToInt32(txtFG6.Text);
        }

        if (agirl6 > egirl6)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Enrollment is greater or equal to Appeared')</script>", false);
            this.txtFG6.Focus();

            txtFG6.Text = "";
            return;
        }


        Int32 egirl7 = 0;
        Int32 agirl7 = 0;
        if (txtG7.Text.Trim() != "")
        {
            egirl7 = Convert.ToInt32(txtG7.Text);
        }
        if (txtFG7.Text.Trim() != "")
        {
            agirl7 = Convert.ToInt32(txtFG7.Text);
        }

        if (agirl7 > egirl7)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Enrollment is greater or equal to Appeared')</script>", false);
            this.txtFG7.Focus();
            txtFG7.Text = "";
            return;

        }



        Int32 egirl8 = 0;
        Int32 agirl8 = 0;
        if (txtG8.Text.Trim() != "")
        {
            egirl8 = Convert.ToInt32(txtG8.Text);
        }
        if (txtFG8.Text.Trim() != "")
        {
            agirl8 = Convert.ToInt32(txtFG8.Text);
        }

        if (agirl8 > egirl8)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Enrollment is greater or equal to Appeared')</script>", false);
            this.txtFG8.Focus();
            txtFG8.Text = "";
            return;

        }

        #endregion


        #region Enrollment Validate Only Girl
        Int32 onlyonlyegirl1 = 0;
        Int32 onlyonlyagirl1 = 0;
        if (txtOG1.Text.Trim() != "")
        {
            onlyonlyegirl1 = Convert.ToInt32(txtOG1.Text);
        }
        if (txtAG1.Text.Trim() != "")
        {
            onlyonlyagirl1 = Convert.ToInt32(txtAG1.Text);
        }

        if (onlyonlyagirl1 > onlyonlyegirl1)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enrollment Boy should be higher or equal to Appeared')</script>", false);
            this.txtAG1.Focus();
            txtAG1.Text = "";
            return;

        }
        Int32 onlyegirl2 = 0;
        Int32 onlyagirl2 = 0;
        if (txtOG2.Text.Trim() != "")
        {
            onlyegirl2 = Convert.ToInt32(txtOG2.Text);
        }
        if (txtAG2.Text.Trim() != "")
        {
            onlyagirl2 = Convert.ToInt32(txtAG2.Text);
        }

        if (onlyagirl2 > onlyegirl2)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Enrollment is greater or equal to Appeared')</script>", false);
            this.txtAG2.Focus();
            txtAG2.Text = "";
            return;

        }

        Int32 onlyegirl3 = 0;
        Int32 onlyagirl3 = 0;
        if (txtOG3.Text.Trim() != "")
        {
            onlyegirl3 = Convert.ToInt32(txtOG3.Text);
        }
        if (txtAG3.Text.Trim() != "")
        {
            onlyagirl3 = Convert.ToInt32(txtAG3.Text);
        }

        if (onlyagirl3 > onlyegirl3)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Enrollment is greater or equal to Appeared')</script>", false);
            this.txtAG3.Focus();
            txtAG3.Text = "";
            return;

        }

        Int32 onlyegirl4 = 0;
        Int32 onlyagirl4 = 0;
        if (txtOG4.Text.Trim() != "")
        {
            onlyegirl4 = Convert.ToInt32(txtOG4.Text);
        }
        if (txtAG4.Text.Trim() != "")
        {
            onlyagirl4 = Convert.ToInt32(txtAG4.Text);
        }

        if (onlyagirl4 > onlyegirl4)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Enrollment is greater or equal to Appeared')</script>", false);
            this.txtAG1.Focus();
            txtAG4.Text = "";
            return;

        }

        Int32 onlyegirl5 = 0;
        Int32 onlyagirl5 = 0;
        if (txtOG5.Text.Trim() != "")
        {
            onlyegirl5 = Convert.ToInt32(txtOG5.Text);
        }
        if (txtAG5.Text.Trim() != "")
        {
            onlyagirl5 = Convert.ToInt32(txtAG5.Text);
        }

        if (onlyagirl5 > onlyegirl5)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Enrollment is greater or equal to Appeared')</script>", false);
            this.txtAG5.Focus();

            txtAG5.Text = "";
            return;
        }


        Int32 onlyegirl6 = 0;
        Int32 onlyagirl6 = 0;
        if (txtOG6.Text.Trim() != "")
        {
            onlyegirl6 = Convert.ToInt32(txtOG6.Text);
        }
        if (txtAG6.Text.Trim() != "")
        {
            onlyagirl6 = Convert.ToInt32(txtAG6.Text);
        }

        if (onlyagirl6 > onlyegirl6)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Enrollment is greater or equal to Appeared')</script>", false);
            this.txtAG6.Focus();

            txtAG6.Text = "";
            return;
        }


        Int32 onlyegirl7 = 0;
        Int32 onlyagirl7 = 0;
        if (txtOG7.Text.Trim() != "")
        {
            onlyegirl7 = Convert.ToInt32(txtOG7.Text);
        }
        if (txtAG7.Text.Trim() != "")
        {
            onlyagirl7 = Convert.ToInt32(txtAG7.Text);
        }

        if (onlyagirl7 > onlyegirl7)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Enrollment is greater or equal to Appeared')</script>", false);
            this.txtAG7.Focus();
            txtAG7.Text = "";
            return;

        }



        Int32 onlyegirl8 = 0;
        Int32 onlyagirl8 = 0;
        if (txtOG8.Text.Trim() != "")
        {
            onlyegirl8 = Convert.ToInt32(txtOG8.Text);
        }
        if (txtAG8.Text.Trim() != "")
        {
            onlyagirl8 = Convert.ToInt32(txtAG8.Text);
        }

        if (onlyagirl8 > onlyegirl8)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Enrollment is greater or equal to Appeared')</script>", false);
            this.txtAG8.Focus();
            txtAG8.Text = "";
            return;

        }

        #endregion


        Int32 Totol = 0;
        //if (txtToalBG.Text.Trim() != "")
        //{
        //    Totol += Convert.ToInt32(txtToalBG.Text);
        //}
        //if (txtFBGTotal10.Text.Trim() != "")
        //{
        //    Totol += Convert.ToInt32(txtFBGTotal10.Text);
        //}
        // if (txtTotalOBG.Text.Trim() !="")
        //{
        //    Totol += Convert.ToInt32(txtTotalOBG.Text);
        //}
        // if (txtTotalABG9.Text.Trim() != "")
        //{
        //    Totol += Convert.ToInt32(txtTotalABG9.Text);
        //}


        if (Totol < 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Total value Greater than zero')</script>", false);
            this.ddlYear.Focus();
            return;
        }
        if (ddlSchool.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select School')</script>", false);
            this.ddlSchool.Focus();
            return;
        }
        else if (ddlYear.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Year')</script>", false);
            this.ddlYear.Focus();
            return;
        }

     
        LoadDataSearch("");

    }


    private void btn_display_Click_Click(object sender, EventArgs e)
    {
        if (ddlSchool.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select School')</script>", false);

         
            return;
        }
        if (ddlYear.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Year')</script>", false);

          
            return;
        }
        //btnd2dSave.Enabled = true;
        //btnSumit.Enabled = true;
        //ClearData();
        //LoadDataSearch("");
    }

    public void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlSchool.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select School')</script>", false);


            return;
        }
        if (ddlYear.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Year')</script>", false);


            return;
        }
        pnlMain1.Enabled = true;
        ClearData();
    }
    public void ClearData()
    {
        lblStudentId.Text = "";

        TxtIPBGT1.Text = "";
        TxtIPBET1.Text = "";
        TxtIET1.Text = "";
        TxtIE2.Text = "";
        TxtIA1.Text = "";
        TxtIA2.Text = "";
        txtAG8.Text = "";


        txtAG7.Text = "";


        txtAG6.Text = "";


        txtAG5.Text = "";


        txtAG4.Text = "";

        txtAG3.Text = "";

        txtAG2.Text = "";
        txtAG1.Text = "";






     


        txtOG8.Text = "";


        txtOG7.Text = "";


        txtOG6.Text = "";


        txtOG5.Text = "";


        txtOG4.Text = "";

        txtOG3.Text = "";

        txtOG2.Text = "";
        txtOG1.Text = "";



       
        txtFG8.Text = "";
        txtFB8.Text = "";
      
        txtFG7.Text = "";
        txtFB7.Text = "";
       
        txtFG6.Text = "";
        txtFB6.Text = "";
        
        txtFG5.Text = "";
        txtFB5.Text = "";
      
        txtFG4.Text = "";
        txtFB4.Text = "";
        txtFG3.Text = "";
        txtFB3.Text = "";
        txtFB2.Text = "";
        txtFG2.Text = "";
        txtFG1.Text = "";


        txtFB1.Text = "";


   
   
        txtG8.Text = "";
        txtB8.Text = "";
      
        txtG7.Text = "";
        txtB7.Text = "";
      
        txtG6.Text = "";
        txtB6.Text = "";
     
        txtG5.Text = "";
        txtB5.Text = "";
 
        txtG4.Text = "";
        txtB4.Text = "";
        txtG3.Text = "";
        txtB3.Text = "";
        txtB2.Text = "";
        txtG2.Text = "";
        txtG1.Text = "";

        txtB1.Text = "";
       

    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            ddlState.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlDistrict, null);
            if (Session["user_level_Role"].ToString() == "1")
            { }
            else
            {
                ddlDistrict.SelectedIndex = 1;
            }
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
          
         
            ddlVillage.Items.Clear();
        }
        else
        {
            ddlState.SelectedIndex = 0;
            ddlDistrict.Items.Clear();
            ddlBlock.Items.Clear();
         
            ddlVillage.Items.Clear();
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
            conditions = "StateCode ='" + ddlState.SelectedValue + "'  and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
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
   
    #region Fill Master Data
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
            conditions = "StateCode ='" + ddlState.SelectedValue + "'  and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else
        {
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";


        }


        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");



    }



    public void FillCBBock()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        else if (Session["user_level_Role"].ToString() == "4")
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 and BlockCode in( " + Session["BlockCode"].ToString() + " ) and FYear ='" + ddlYear.SelectedItem.Text + "' ";
        }
        else
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");



    }
    public void FillCVillage()
    {
        conditions = "";
        //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        //objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "Select");

        if (ddlPanchayat.SelectedValue.ToString() == "1")
        {
            conditions = "mst5Village.DistrictCode ='" + ddlDistrict.SelectedValue + "'  and mst5Village.BlockCode ='" + ddlBlock.SelectedValue + "'  ";

        }
        else
        {
            conditions = "mst5Village.DistrictCode ='" + ddlDistrict.SelectedValue + "'  and mst5Village.BlockCode ='" + ddlBlock.SelectedValue + "' and  mst5Village.PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";

        }

        string strQry = "  SELECT mst5Village.VillageCode, dbo.TitleCase(upper((mst5Village.VillageName))) + ' (' + dbo.TitleCase(upper(mstPanchayat.PanchayatName)) +')'   as VillageName FROM mst5Village INNER JOIN mstPanchayat ON mst5Village.PanchayatCode = mstPanchayat.PanchayatCode where " + conditions + "  order by VillageName   ";
        DataTable dtVillage = objMain.LoadData(strQry);

        objComman.BindDLLMasterTableVillage("mst5Village", "VillageName,VillageCode", dtVillage, conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "Select");


    }


    #endregion

    #region   SelectedIndexChanged Methods
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
   
    public void FillCBCluster()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "'";
        objComman.BindDLLSelectAll("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "Select");



    }
    #endregion

  
  

   
}


