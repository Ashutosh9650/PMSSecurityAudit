using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmTravelMatrix2024HR : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    public bool edit_status = false;
    int sumFooterValue = 0;
    int TravelCostWithincluster = 0;
    int TravelCostWithinclusterOut = 0;
    int PerDiem = 0;
    int Accommodation = 0;
    int Conveyance = 0;
    int Expanses = 0;
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

                LoadYear();
                LoadUserLeavel();
                UserLevelFilter();


                ViewState["1"] = "ss";
                if (Convert.ToString(Session["user_level"]) == "128" || Convert.ToString(Session["user_level"]) == "130" || Convert.ToString(Session["user_level"]) == "148")
                {
                    lblmsg.Text = "Travel Matrix- HR Payment Confirmation";
                    btnAdd.Text = "Submit to DOL";
                }
                if (Convert.ToString(Session["user_level"]) == "91")
                {
                    lblmsg.Text = "Travel Matrix- DOL Payment Summary Approval";
                    btnAdd.Text = "Approve";
                    btnDownload.Visible = true;
                }
                if (Convert.ToString(Session["user_level"]) == "124" || Convert.ToString(Session["user_level"]) == "149")
                {
                    lblmsg.Text = "Travel Matrix- Finance Payment Approval";
                    btnAdd.Text = "Paid";
                }
                //if (Request.QueryString["ID"] != null)
                //{
                //     ddlState.SelectedValue=Convert.ToString(Session["Scode"] );
                //    ddlState_SelectedIndexChanged(ddlState, null);
                //   ddlDistrict.SelectedValue = Convert.ToString(Session["Dcode"]);
                //    ddlDistrict_SelectedIndexChanged(ddlDistrict, null);

                //    ddlBlock.SelectedValue = Convert.ToString(Session["Bcode"]);
                //    ddlBlock_SelectedIndexChanged(ddlDistrict, null);
                //    ddlCluster.SelectedValue = Convert.ToString(Session["Ccode"]);
                //    ddlCluster_SelectedIndexChanged(ddlDistrict, null);
                //    ddlFC.SelectedValue= Convert.ToString(Session["FCcode"]);
                //    ddlMonth.SelectedValue = Convert.ToString(Session["MMmonth"]);
                //    btnSearch_Click(btnAdd, null);

                //}
            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }

        }
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            AlllStateCode();
            ddlState.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlDistrict, null);
            if (Session["user_level_Role"].ToString() == "3" || Session["user_level_Role"].ToString() == "4")
            {
                ddlDistrict.SelectedIndex = 1;
                ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
            }

            ddlCluster.Items.Clear();

        }
        else
        {
            ddlState.SelectedIndex = 0;
            ddlDistrict.Items.Clear();
            ddlBlock.Items.Clear();
            ddlCluster.Items.Clear();

        }

    }

    public void UserLevelFilter()
    {





        string strQry = "";
        string Cond = "Module='Travel Matrix'";
        strQry = "Select * from MstUserRight  where " + Cond + " and Role_Id=" + Session["user_level"].ToString() + "   ";


        DataTable dtTravelMatrix = objMain.LoadData(strQry);

        if (dtTravelMatrix.Rows.Count > 0)
        {
            vADD = Convert.ToBoolean(dtTravelMatrix.Rows[0]["AddStatus"].ToString());
            vVerify = Convert.ToBoolean(dtTravelMatrix.Rows[0]["verify_Status"].ToString());
            vDelete = Convert.ToBoolean(dtTravelMatrix.Rows[0]["Delete_status"].ToString());

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

        //if (vADD == true)
        //{
        //    btnsave.Enabled = true;

        //}
        //else
        //{
        //    btnsave.Enabled = false;

        //}
        //if (vVerify == true)
        //{



        //}
        //if (vVerify == true || vADD == true)
        //{
        //    btnsave.Enabled = true;

        //}
        //else
        //{
        //    btnsave.Enabled = false;

        //}

    }
    public void LoadYear()
    {

        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


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
            objComman.BindDLLDatatable("mst1State", dtAllState, "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

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
            objComman.BindDLLDatatable("mst1State", dtAllState, "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

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
            objComman.BindDLLDatatable("mst1State", dtAllState, "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");


        }

    }
    public void LoadUserLeavel()
    {
        AlllStateCode();
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            ddlState.SelectedIndex = 0;
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

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
            //conditions = "StateCode ='" + ddlState.SelectedValue + "'  and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
            //objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

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
    public void FillCBCluster()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' ";
        objComman.BindDLL("mstcluster", "ClusterCode,dbo.TitleCase(upper(ClusterName)) as ClusterName", conditions, "ClusterName", "asc", ddlCluster, "ClusterName", "ClusterCode", "--Select--");


    }
    protected void ddlCluster_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillFC();
    }
    public void FillFC()
    {
        conditions = "ActiveStatus =1 And UserLevel=24 ";
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions += " and BlockCode ='" + ddlBlock.SelectedValue + "'  ";
        }
        if (ddlCluster.SelectedIndex > 0)
        {
            conditions += " and VillageCode ='" + ddlCluster.SelectedValue + "' ";
        }
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
        {
            objComman.BindDLLSelectAll("mstuser", "UserName  ,UserName +' ('+ FristName +')' as UserID ", conditions, "UserName", "asc", ddlFC, "UserID", "UserName", "Select");
        }
        else
        {
            objComman.BindDLLSelectAll("mstuser2026", "UserName  ,UserName +' ('+ FristName +')' as UserID ", conditions, "UserName", "asc", ddlFC, "UserID", "UserName", "Select");

        }

        //objComman.BindDLL("mstuser", "UserName  ,UserName +' ('+ FristName +')' as UserID ", conditions, "UserName", "asc", ddlFC, "UserID", "UserName", "Select");

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
        if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "";
            conditions = " mst2District.StateCode ='" + ddlState.SelectedValue + "' and UserName='" + Session["username"].ToString() + "' ";
            string strQry1 = "       sELECT distinct mst2District.DistrictCode as DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist  ";
            strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions + "  and  Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName  ";
            DataTable dtDistrict = objMain.LoadData(strQry1);

            objComman.BindDLLDatatable("mst2District", dtDistrict, "DistrictCode, dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "Desc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

        }
        else
        {

            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        }

    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        TravelCostWithincluster = 0;
        TravelCostWithinclusterOut = 0;
        PerDiem = 0;
        Accommodation = 0;
        Conveyance = 0;
        Expanses = 0;
        sumFooterValue = 0;
        if (Convert.ToString(Session["username"]) != "")
        {
        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
        LoadData();
    }
    public void LoadData()
    {

        string con = "";
        if (ddlDistrict.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select District')</script>", false);
            return;
        }
        if (ddlMonth.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select month')</script>", false);
            return;
        }
        int mYear = 0;

        if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        {
            mYear = Convert.ToInt32(ddlYear.SelectedValue) + 1;
        }
        else
        {
            mYear = Convert.ToInt32(ddlYear.SelectedValue);
        }

        if (ddlDistrict.SelectedIndex > 0)
        {
            con += " and mst3Block.DistrictCode ='" + ddlDistrict.SelectedValue + "'";
        }
        if (ddlBlock.SelectedIndex > 0)
        {
            con += " and mst3Block.BlockCode ='" + ddlBlock.SelectedValue + "'";
        }
        if (ddlCluster.SelectedIndex > 0)
        {
            con += " and mstCluster.ClusterCode ='" + ddlCluster.SelectedValue + "'";
        }
        if (ddlFC.SelectedIndex > 0)
        {
            con += "and tblTravelMatrixDeatils2024.UserId ='" + ddlFC.SelectedValue + "'";
        }
        con += "  and [mMonth]='" + ddlMonth.SelectedValue + "'  and [mYear]='" + mYear + "'";


        SqlParameter[] parm1 = new SqlParameter[]
      {
             new SqlParameter("@Con",con),
           new SqlParameter("@Month",ddlMonth.SelectedValue),
            new SqlParameter("@Myear",mYear),
              new SqlParameter("@Fyear",ddlYear.SelectedItem.Text),
                   new SqlParameter("@user_level", Convert.ToString(Session["user_level"])),

      };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptTravelDeatil2024ViewForHR2026", parm1);

        // DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptTravelDeatil2024ViewForHR", parm1);
        btnAdd.Visible = false;
        if (dt.Rows.Count > 0)
        {
            gvTravekDatewise.Columns[0].Visible = false;
            gvTravekDatewise.Columns[16].Visible = false;
            if (Convert.ToString(Session["user_level"]) == "128" || Convert.ToString(Session["user_level"]) == "130" || Convert.ToString(Session["user_level"]) == "148")
            {
                btnAdd.Visible = true;
            }
            if (Convert.ToString(Session["user_level"]) == "91")
            {
                btnAdd.Visible = true;
            }
            if ((Convert.ToString(Session["user_level"]) == "124" || Convert.ToString(Session["user_level"]) == "149" || Convert.ToString(Session["user_level"]) == "149"))
            {
                btnAdd.Visible = true;
                gvTravekDatewise.Columns[0].Visible = true;
                gvTravekDatewise.Columns[17].Visible = true;
            }
            gvTravekDatewise.DataSource = dt;
            gvTravekDatewise.DataBind();

            if ((Convert.ToString(Session["user_level"]) == "124" || Convert.ToString(Session["user_level"]) == "91" || Convert.ToString(Session["user_level"]) == "149"))
            {

                for (int i = 0; i < gvTravekDatewise.Rows.Count; i++)
                {
                    GridViewRow RowD = gvTravekDatewise.Rows[i];

                    System.Web.UI.WebControls.Label lblHoldStatus = (System.Web.UI.WebControls.Label)RowD.FindControl("lblHoldStatus");
                    if (lblHoldStatus.Text == "1")
                    {
                        RowD.BackColor = System.Drawing.Color.Red;
                    }

                }
            }
        }
        else
        {
            gvTravekDatewise.DataSource = null;
            gvTravekDatewise.DataBind();
        }
    }
    protected void gvnroll_OnRowCommand(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblTotalPay = (Label)e.Row.FindControl("lblTotalPay");
            Label lbltotalExpens = (Label)e.Row.FindControl("TotalExpensBO");
            Label lblvehicle = (Label)e.Row.FindControl("lblvehicle");
            Label lblAccommodation = (Label)e.Row.FindControl("lblAccommodation");
            Label lblPerDim = (Label)e.Row.FindControl("lblPerDim");
            Label lblClusteroutTotalAmountKM = (Label)e.Row.FindControl("lblClusteroutTotalAmountKM");
            Label lblClusterTotalAmountKM = (Label)e.Row.FindControl("lblClusterTotalAmountKM");
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            LinkButton LinkButton1 = (LinkButton)e.Row.FindControl("LinkButton1");

            if (Convert.ToString(Session["user_level"]) == "128" || Convert.ToString(Session["user_level"]) == "130" || Convert.ToString(Session["user_level"]) == "148")
            {
                if (lblStatus.Text == "3")
                {
                    LinkButton1.Text = "Unhold";
                    LinkButton1.ForeColor = Color.Blue;
                }
                if (lblStatus.Text == "5")
                {
                    LinkButton1.Text = "Hold";
                    LinkButton1.ForeColor = Color.Red;
                }
            }
            if (Convert.ToString(Session["user_level"]) == "91")
            {
                if (lblStatus.Text == "4")
                {
                    LinkButton1.Text = "Reject Form";
                }

            }
            if ((Convert.ToString(Session["user_level"]) == "124" || Convert.ToString(Session["user_level"]) == "149"))
            {
                if (lblStatus.Text == "6")
                {
                    LinkButton1.Text = "Reject Form";
                    LinkButton1.Enabled = true;
                }
                else
                {
                    LinkButton1.Text = "Approved";
                    LinkButton1.Enabled = false;
                }

            }
            if (lblTotalPay.Text != "")
            {
                sumFooterValue += Convert.ToInt32(lblTotalPay.Text);
            }
            if (lbltotalExpens.Text != "")
            {
                Expanses += Convert.ToInt32(lbltotalExpens.Text);
            }
            if (lblvehicle.Text != "")
            {
                Conveyance += Convert.ToInt32(lblvehicle.Text);
            }
            if (lblAccommodation.Text != "")
            {
                Accommodation += Convert.ToInt32(lblAccommodation.Text);
            }
            if (lblPerDim.Text != "")
            {
                PerDiem += Convert.ToInt32(lblPerDim.Text);
            }
            if (lblClusteroutTotalAmountKM.Text != "")
            {
                TravelCostWithinclusterOut += Convert.ToInt32(lblClusteroutTotalAmountKM.Text);
            }
            if (lblClusterTotalAmountKM.Text != "")
            {
                TravelCostWithincluster += Convert.ToInt32(lblClusterTotalAmountKM.Text);
            }



        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("lblTotal");
            lbl.Text = sumFooterValue.ToString();
            Label lbltotalExpens = (Label)e.Row.FindControl("lbltotalExpens");
            lbltotalExpens.Text = Expanses.ToString();
            Label lbltotalvehicle = (Label)e.Row.FindControl("lbltotalvehicle");
            lbltotalvehicle.Text = Conveyance.ToString();
            Label lbltotalAccommodation = (Label)e.Row.FindControl("lbltotalAccommodation");
            lbltotalAccommodation.Text = Accommodation.ToString();
            Label lbltotalPerDim = (Label)e.Row.FindControl("lbltotalPerDim");
            lbltotalPerDim.Text = PerDiem.ToString();
            Label lbltotalClusteroutTotalAmountKM = (Label)e.Row.FindControl("lbltotalClusteroutTotalAmountKM");
            lbltotalClusteroutTotalAmountKM.Text = TravelCostWithinclusterOut.ToString();

            Label lbltotalClusterTotalAmountKM = (Label)e.Row.FindControl("lbltotalClusterTotalAmountKM");
            lbltotalClusterTotalAmountKM.Text = TravelCostWithincluster.ToString();
        }
    }


    public void LoadDataDeatils(string Fdate, string Todate)
    {


        if (ddlFC.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select FC')</script>", false);
            return;
        }
        if (ddlMonth.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select month')</script>", false);
            return;
        }
        int mYear = 0;

        if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        {
            mYear = Convert.ToInt32(ddlYear.SelectedValue) + 1;
        }
        else
        {
            mYear = Convert.ToInt32(ddlYear.SelectedValue);
        }
        SqlParameter[] parm1 = new SqlParameter[]
      {
           new SqlParameter("@Fromdate", Convert.ToDateTime(Fdate).ToString("yyyy-MM-dd")),
            new SqlParameter("@Todate", Convert.ToDateTime(Todate).ToString("yyyy-MM-dd")),
             new SqlParameter("@UserName", ddlFC.SelectedValue),
             new SqlParameter("@month", ddlMonth.SelectedValue),
              new SqlParameter("@Myear",mYear),
                 new SqlParameter("@UserRole",Convert.ToString(Session["user_level_Role"])),


      };


        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptTravelDeatil2024", parm1);
        if (dt.Rows.Count > 0)
        {
            gvTravekDatewise.DataSource = dt;
            gvTravekDatewise.DataBind();
        }
        else
        {
            gvTravekDatewise.DataSource = null;
            gvTravekDatewise.DataBind();
        }

    }
    protected void LnkBtnBlock_OnClick(object sender, EventArgs e)
    {
        Session["Scode"] = ddlState.SelectedValue;
        Session["Dcode"] = ddlDistrict.SelectedValue;
        Session["Bcode"] = ddlBlock.SelectedValue;

        Session["Ccode"] = ddlCluster.SelectedValue;
        Session["FCcode"] = ddlFC.SelectedValue;
        Session["MMmonth"] = ddlMonth.SelectedValue;

        ImageButton bt = (ImageButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string UniqueChildCode = (gvr.FindControl("lblPlanUniqueCode") as Label).Text;
        string FFlag = "2";
        Response.Redirect("~/frmTravelMatrixWithClusters.aspx?ID=" + ddlCluster.SelectedValue + "," + ddlMonth.SelectedValue + "," + ddlFC.SelectedValue + "," + FFlag + "," + UniqueChildCode + "");

    }

    protected void lnl_Action(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {
        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
        LinkButton ddlLabTest1 = (LinkButton)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        Label lblFromNo = (Label)row1.FindControl("lblFromNo");
        Label lblMyear = (Label)row1.FindControl("lblMyear");
        Label lblUserID = (Label)row1.FindControl("lblUserID");
        LinkButton LinkButton1 = (LinkButton)row1.FindControl("LinkButton1");
        int Icount = 0;
        int Status = 0;
        int FStatus = 0;
        string Flag = "";
        if (Convert.ToString(Session["user_level"]) == "128" || Convert.ToString(Session["user_level"]) == "130" || Convert.ToString(Session["user_level"]) == "148")
        {
            string hh = "";
            if ((Convert.ToString(Session["user_level"]) == "128" || Convert.ToString(Session["user_level"]) == "130" || Convert.ToString(Session["user_level"]) == "148") && LinkButton1.Text == "Unhold")
            {
                Status = 5;
                FStatus = 5;
                Flag = "1";
                hh = "Hold";
            }
            if ((Convert.ToString(Session["user_level"]) == "128" || Convert.ToString(Session["user_level"]) == "130" || Convert.ToString(Session["user_level"]) == "148") && LinkButton1.Text == "Hold")
            {
                Status = 3;
                Flag = "2";
                hh = "UnHold";
                FStatus = 6;
            }
            int mYear = 0;

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                mYear = Convert.ToInt32(ddlYear.SelectedValue) + 1;
            }
            else
            {
                mYear = Convert.ToInt32(ddlYear.SelectedValue);
            }

            SqlParameter[] cmdParameters1 = new SqlParameter[]
                              {
                        new SqlParameter("@FromNo", lblFromNo.Text),
                          new SqlParameter("@mYear",""+mYear +" "),
                         new SqlParameter("@mMonth",""+ddlMonth.SelectedValue +" "),

                        new SqlParameter("@UserID", ""+ lblUserID.Text +" "),
                          new SqlParameter("@Status", Status),
                           new SqlParameter("@Ftatus", FStatus),

                                new SqlParameter("@CreateBy", Convert.ToString(Session["username"])),
                                  new SqlParameter("@user_level", Convert.ToString(Session["user_level"])),
                                    new SqlParameter("@Flag",Flag),

                              };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatetravelMatrixApproveHold", cmdParameters1);

            if (Icount > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + hh + " sucessfully')</script>", false);

                if ((Convert.ToString(Session["user_level"]) == "128" || Convert.ToString(Session["user_level"]) == "130" || Convert.ToString(Session["user_level"]) == "148") && LinkButton1.Text == "Unhold")
                {
                    LinkButton1.Text = "Hold";
                    LinkButton1.ForeColor = Color.Red;


                }
                else if ((Convert.ToString(Session["user_level"]) == "128" || Convert.ToString(Session["user_level"]) == "130" || Convert.ToString(Session["user_level"]) == "148") && LinkButton1.Text == "Hold")
                {
                    LinkButton1.Text = "Unhold";
                    LinkButton1.ForeColor = Color.Blue;
                }
            }
        }
        if (Convert.ToString(Session["user_level"]) == "91")
        {
            lblFromNoEdit.Text = lblFromNo.Text;
            lblUserIDEdit.Text = lblUserID.Text;
            txtResone.Text = "";
            MPE_Entry.Show();
        }
        if ((Convert.ToString(Session["user_level"]) == "124" || Convert.ToString(Session["user_level"]) == "149"))
        {
            lblFromNoEdit.Text = lblFromNo.Text;
            lblUserIDEdit.Text = lblUserID.Text;
            txtResone.Text = "";
            MPE_Entry.Show();
        }
    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        int mYear = 0;
        int Status = 0;
        if (Convert.ToString(Session["user_level"]) == "91")
        {
            Status = 7;
        }
        if ((Convert.ToString(Session["user_level"]) == "124" || Convert.ToString(Session["user_level"]) == "149"))
        {
            Status = 9;
        }
        if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        {
            mYear = Convert.ToInt32(ddlYear.SelectedValue) + 1;
        }
        else
        {
            mYear = Convert.ToInt32(ddlYear.SelectedValue);
        }
        int Icount = 0;
        SqlParameter[] cmdParameters1 = new SqlParameter[]
                            {
                        new SqlParameter("@FromNo", lblFromNoEdit.Text),
                          new SqlParameter("@mYear",""+mYear +" "),
                         new SqlParameter("@mMonth",""+ddlMonth.SelectedValue +" "),

                        new SqlParameter("@UserID", ""+ lblUserIDEdit.Text +" "),
                          new SqlParameter("@Status", Status),

                                new SqlParameter("@CreateBy", Convert.ToString(Session["username"])),
                                  new SqlParameter("@user_level", Convert.ToString(Session["user_level"])),
                                     new SqlParameter("@Remark", txtResone.Text),


                            };
        Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatetravelMatrixReject", cmdParameters1);

        if (Icount > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reject sucessfully')</script>", false);
            LoadData();
        }

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {
        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
        int Status = 0;
        int Icount = 0;
        if (Convert.ToString(Session["user_level"]) == "128" || Convert.ToString(Session["user_level"]) == "130" || Convert.ToString(Session["user_level"]) == "148")
        {

            for (int i = 0; i < gvTravekDatewise.Rows.Count; i++)
            {
                Label lblFromNo = (Label)gvTravekDatewise.Rows[i].FindControl("lblFromNo");
                LinkButton LinkButton1 = (LinkButton)gvTravekDatewise.Rows[i].FindControl("LinkButton1");
                Label lblMyear = (Label)gvTravekDatewise.Rows[i].FindControl("lblMyear");
                Label lblUserID = (Label)gvTravekDatewise.Rows[i].FindControl("lblUserID");
                Status = 4;
                //if (LinkButton1.Text=="Unhold")
                //{
                SqlParameter[] cmdParameters1 = new SqlParameter[]
                                        {
                        new SqlParameter("@FromNo", lblFromNo.Text),
                          new SqlParameter("@mYear",""+lblMyear.Text +" "),
                         new SqlParameter("@mMonth",""+ddlMonth.SelectedValue +" "),

                        new SqlParameter("@UserID", ""+ lblUserID.Text +" "),
                          new SqlParameter("@Status", Status),

                                new SqlParameter("@CreateBy", Convert.ToString(Session["username"])),
                                  new SqlParameter("@user_level", Convert.ToString(Session["user_level"])),


                                        };
                Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatetravelMatrixApprove", cmdParameters1);

                //}

            }
            if (Icount > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Submitted Successfully')</script>", false);
                LoadData();

            }
        }
        if (Convert.ToString(Session["user_level"]) == "91")
        {

            for (int i = 0; i < gvTravekDatewise.Rows.Count; i++)
            {
                Label lblFromNo = (Label)gvTravekDatewise.Rows[i].FindControl("lblFromNo");
                LinkButton LinkButton1 = (LinkButton)gvTravekDatewise.Rows[i].FindControl("LinkButton1");
                Label lblMyear = (Label)gvTravekDatewise.Rows[i].FindControl("lblMyear");
                Label lblUserID = (Label)gvTravekDatewise.Rows[i].FindControl("lblUserID");
                Status = 6;
                if (LinkButton1.Text == "Reject Form")
                {
                    SqlParameter[] cmdParameters1 = new SqlParameter[]
                                            {
                        new SqlParameter("@FromNo", lblFromNo.Text),
                          new SqlParameter("@mYear",""+lblMyear.Text +" "),
                         new SqlParameter("@mMonth",""+ddlMonth.SelectedValue +" "),

                        new SqlParameter("@UserID", ""+ lblUserID.Text +" "),
                          new SqlParameter("@Status", Status),

                                new SqlParameter("@CreateBy", Convert.ToString(Session["username"])),
                                  new SqlParameter("@user_level", Convert.ToString(Session["user_level"])),


                                            };
                    Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatetravelMatrixApprove", cmdParameters1);

                }

            }
            if (Icount > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Approve sucessfully')</script>", false);
                LoadData();

            }
        }
        if ((Convert.ToString(Session["user_level"]) == "124" || Convert.ToString(Session["user_level"]) == "149"))
        {
            int icount = 0;
            for (int i = 0; i < gvTravekDatewise.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)gvTravekDatewise.Rows[i].FindControl("chkSelect");
                if (chkSelect.Checked == true)
                {
                    icount = 1;
                }
            }
            if (icount == 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select checkbox')</script>", false);
                return;
            }
            for (int i = 0; i < gvTravekDatewise.Rows.Count; i++)
            {
                Label lblFromNo = (Label)gvTravekDatewise.Rows[i].FindControl("lblFromNo");
                LinkButton LinkButton1 = (LinkButton)gvTravekDatewise.Rows[i].FindControl("LinkButton1");
                Label lblMyear = (Label)gvTravekDatewise.Rows[i].FindControl("lblMyear");
                Label lblUserID = (Label)gvTravekDatewise.Rows[i].FindControl("lblUserID");
                Status = 8;
                CheckBox chkSelect = (CheckBox)gvTravekDatewise.Rows[i].FindControl("chkSelect");
                if (chkSelect.Checked == true)
                {
                    SqlParameter[] cmdParameters1 = new SqlParameter[]
                                            {
                        new SqlParameter("@FromNo", lblFromNo.Text),
                          new SqlParameter("@mYear",""+lblMyear.Text +" "),
                         new SqlParameter("@mMonth",""+ddlMonth.SelectedValue +" "),

                        new SqlParameter("@UserID", ""+ lblUserID.Text +" "),
                          new SqlParameter("@Status", Status),

                                new SqlParameter("@CreateBy", Convert.ToString(Session["username"])),
                                  new SqlParameter("@user_level", Convert.ToString(Session["user_level"])),


                                            };
                    Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatetravelMatrixApprove", cmdParameters1);

                }

            }
            if (Icount > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Approve sucessfully')</script>", false);
                LoadData();

            }
        }
    }
    protected void View_Action(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {
        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
        ImageButton ddlLabTest1 = (ImageButton)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        Label lblFromNo = (Label)row1.FindControl("lblFromNo");
        Label lblMyear = (Label)row1.FindControl("lblMyear");
        Label lblUserID = (Label)row1.FindControl("lblUserID");
        LinkButton LinkButton1 = (LinkButton)row1.FindControl("LinkButton1");
        GeneraatePDFMain(lblFromNo.Text, lblUserID.Text);
    }
    protected void btnDown_Click(object sender, EventArgs e)
    {

        if (Convert.ToString(Session["username"]) != "")
        {
        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
        LoadDatadownload();
    }
    public void LoadDatadownload()
    {

        string con = "";
        if (ddlDistrict.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select District')</script>", false);
            return;
        }
        if (ddlMonth.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select month')</script>", false);
            return;
        }
        int mYear = 0;

        if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        {
            mYear = Convert.ToInt32(ddlYear.SelectedValue) + 1;
        }
        else
        {
            mYear = Convert.ToInt32(ddlYear.SelectedValue);
        }

        if (ddlDistrict.SelectedIndex > 0)
        {
            con += " and mst3Block.DistrictCode ='" + ddlDistrict.SelectedValue + "'";
        }
        if (ddlBlock.SelectedIndex > 0)
        {
            con += " and mst3Block.BlockCode ='" + ddlBlock.SelectedValue + "'";
        }
        if (ddlCluster.SelectedIndex > 0)
        {
            con += " and mstCluster.ClusterCode ='" + ddlCluster.SelectedValue + "'";
        }
        if (ddlFC.SelectedIndex > 0)
        {
            con += "and tblTravelMatrixDeatils2024.UserId ='" + ddlFC.SelectedValue + "'";
        }
        con += "  and [mMonth]='" + ddlMonth.SelectedValue + "'  and [mYear]='" + mYear + "'";


        SqlParameter[] parm1 = new SqlParameter[]
      {
             new SqlParameter("@Con",con),
           new SqlParameter("@Month",ddlMonth.SelectedValue),
            new SqlParameter("@Myear",mYear),
                   new SqlParameter("@user_level", Convert.ToString(Session["user_level"])),

      };


        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptTravelDeatil2024DownloadSummary", parm1);

        if (dt.Rows.Count > 0)
        {
            dt.Columns.Remove("rn");
            MultipuExeclTrack(dt);
        }
        else
        {

        }
    }
    public void MultipuExeclTrack(DataTable dt)
    {

        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\TravelMatrixSummary.xlsx");
        var ws = wb.Worksheet(1);
        //var ws1 = wb.Worksheet(2);
        //var ws3 = wb.Worksheet(3);

        //dt.Columns.Remove("rownNO");
        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("rownNO");
        ws.Cell(2, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 2;
        string str = "A2:R" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
        //ws1.Cell(4, 1).InsertData(dt1.Rows);

        //Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 3;
        //string str1 = "A4:AG" + ii1;

        //ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        //ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        //ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        //ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        //DataTable dt2 = dtMain1.Tables[2];
        //dt2.Columns.Remove("rowno");
        //ws3.Cell(3, 1).InsertData(dt2.Rows);


        //Int32 ii11 = Convert.ToInt32(dt2.Rows.Count) + 2;
        //string str11 = "A3:O" + ii11;

        //ws3.Range(str11).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        //ws3.Range(str11).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        //ws3.Range(str11).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        //ws3.Range(str11).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);



        filepath = StartupPath + "\\TravelMatrixSummary " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
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
    protected string GeneraatePDFMain(string FromNo, string Username)
    {
        string sb = "";
        try
        {
            string Fdate = "";
            string Tdate = "";
            int mMonth = 0;
            if (ddlMonth.SelectedValue == "1")
            {
                mMonth = 12;
            }
            else
            {
                mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            }
            if (ddlMonth.SelectedValue == "2" || ddlMonth.SelectedValue == "3")
            {
                Fdate = DateTime.Now.Year + 1 + "-" + mMonth + "-" + "21";
                Tdate = DateTime.Now.Year + 1 + "-" + ddlMonth.SelectedValue + "-" + "20";
            }
            else if (ddlMonth.SelectedValue == "4")
            {
                Fdate = DateTime.Now.Year + "-" + mMonth + "-" + "01";
                Tdate = DateTime.Now.Year + "-" + ddlMonth.SelectedValue + "-" + "20";
            }
            else if (ddlMonth.SelectedValue == "1")
            {
                Fdate = DateTime.Now.Year + "-" + mMonth + "-" + "21";
                Tdate = DateTime.Now.Year + 1 + "-" + ddlMonth.SelectedValue + "-" + "20";
            }
            else
            {
                Fdate = DateTime.Now.Year + "-" + mMonth + "-" + "21";
                Tdate = DateTime.Now.Year + "-" + ddlMonth.SelectedValue + "-" + "20";
            }

            int mYear = 0;

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                mYear = Convert.ToInt32(ddlYear.SelectedValue) + 1;
            }
            else
            {
                mYear = Convert.ToInt32(ddlYear.SelectedValue);
            }
            string empname = "", empcode = "", designation = "", district = "", Block = "", cluster = "", depatment = "", Reporting = "";
            //  DataTable dtemployee = objMain.Select_All_Data("MstUser inner join tblTravelMatrixDeatils2024 on  MstUser.UserName=tblTravelMatrixDeatils2024.UserID inner join  Mstuserrole on  MstUser.UserLevel= Mstuserrole.Role_Level inner join tblemployeedetails on tblemployeedetails.EmployeeID=MstUser.UserName inner join mst2District on mst2District.districtcode=MstUser.DistrictCode inner join mst3Block on mst3Block.blockcode=MstUser.blockcode inner join mst5Village on mst5Village.villagecode=MstUser.villagecode    inner join MstUser u on u.blockcode=MstUser.blockcode and u.UserLevel=19 and U.ActiveStatus=1", "distinct mstuser.FristName as name,mstuser.UserName as code,Mstuserrole.Role as desg,Department ,mst2District.districtname,BlockName,VillageName as cluster,U.userName +'-'+ u.FristName  as [Reporting Manager]", "MstUser.UserName='" + ddlFC.SelectedValue + "' and mYear=" + mYear + " and mMonth=" + Convert.ToInt32(ddlMonth.SelectedValue) + "", "", "");
            //      DataTable dtemployee = objMain.Select_All_Data("MstUser inner join tblTravelMatrixDeatils2024 on  MstUser.UserName=tblTravelMatrixDeatils2024.UserID inner join  Mstuserrole on  MstUser.UserLevel= Mstuserrole.Role_Level  inner join mst2District on mst2District.districtcode=MstUser.DistrictCode inner join mst3Block on mst3Block.blockcode=MstUser.blockcode inner join mst5Village on mst5Village.villagecode=MstUser.villagecode    inner join MstUser u on u.blockcode=MstUser.blockcode and u.UserLevel=19 and U.ActiveStatus=1", "distinct mstuser.FristName as name,mstuser.UserName as code,Mstuserrole.Role as desg,'' Department ,mst2District.districtname,BlockName,VillageName as cluster,U.userName +'-'+ u.FristName  as [Reporting Manager]", "MstUser.UserName='" + Username + "' and mYear=" + mYear + " and mMonth=" + Convert.ToInt32(ddlMonth.SelectedValue) + "", "", "");



            DataTable dtemployee = null;
            if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
            {
                SqlParameter[] parm2 = new SqlParameter[]

            {

                     new SqlParameter("@UserName", Username),
                     new SqlParameter("@mMonth", ddlMonth.SelectedValue),
                      new SqlParameter("@mYear",mYear),




            };


                dtemployee = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadEMpDetailsTravel", parm2);

            }
            else
            {
                SqlParameter[] parm2 = new SqlParameter[]

                 {

                     new SqlParameter("@UserName", Username),
                     new SqlParameter("@mMonth", ddlMonth.SelectedValue),
                      new SqlParameter("@mYear",mYear),




                 };


                dtemployee = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadEMpDetailsTravel2025", parm2);

            }

            // SqlParameter[] parm2 = new SqlParameter[]
            //{

            //  new SqlParameter("@UserName", Username),
            //  new SqlParameter("@mMonth", ddlMonth.SelectedValue),
            //   new SqlParameter("@mYear",mYear),


            //};


            // DataTable dtemployee = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadEMpDetailsTravel", parm2);


            if (dtemployee.Rows.Count > 0)
            {

                empname = dtemployee.Rows[0]["name"].ToString();
                empcode = dtemployee.Rows[0]["code"].ToString();
                designation = dtemployee.Rows[0]["desg"].ToString();
                district = dtemployee.Rows[0]["districtname"].ToString();
                Block = dtemployee.Rows[0]["BlockName"].ToString();
                cluster = dtemployee.Rows[0]["cluster"].ToString();
                depatment = dtemployee.Rows[0]["Department"].ToString();
                Reporting = dtemployee.Rows[0]["Reporting Manager"].ToString();

            }


            string imageURLLogo = Server.MapPath(".") + "/images/logo-new1.png";

            sb += @"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">";
            sb += "<html>";
            sb += "<body>";
            // sb += "<table width='100%' cellspacing='0' cellpadding='2'>";
            DataSet dstravle = null;
            if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
            {

                SqlParameter[] parm1 = new SqlParameter[]
              {

                       new SqlParameter("@UserName", Username),
                        new SqlParameter("@month", ddlMonth.SelectedValue),
                         new SqlParameter("@Myear",mYear),
                            new SqlParameter("@UserRole",Convert.ToString(Session["user_level_Role"])),
                    new SqlParameter("@FromNo",FromNo),



              };


                dstravle = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptTravelDeatil2024View", parm1);
            }
            else
            {
                SqlParameter[] parm1 = new SqlParameter[]
           {

                               new SqlParameter("@UserName", Username),
                                new SqlParameter("@month", ddlMonth.SelectedValue),
                                 new SqlParameter("@Myear",mYear),
                                    new SqlParameter("@UserRole",Convert.ToString(Session["user_level_Role"])),
                            new SqlParameter("@FromNo",FromNo),



           };
                dstravle = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptTravelDeatil2024View2025", parm1);
            }
            //       SqlParameter[] parm1 = new SqlParameter[]
            //{

            //        new SqlParameter("@UserName", Username),
            //        new SqlParameter("@month", ddlMonth.SelectedValue),
            //         new SqlParameter("@Myear",mYear),
            //            new SqlParameter("@UserRole",Convert.ToString(Session["user_level_Role"])),
            //    new SqlParameter("@FromNo",FromNo),


            //};


            //       DataSet dstravle = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptTravelDeatil2024View", parm1);


            DataTable dttravelmatrixdetails = dstravle.Tables[0];
            DataTable dttraveDate = dstravle.Tables[4];
            DataTable dttravex = dstravle.Tables[1];
            DataTable dttravexIMg = dstravle.Tables[2];

            // DataTable dttravelmatrixdetails = objMain.Select_All_Data("tblTravelMatrixDeatils2024", "convert(varchar,TravelDate,103) as Fromdate,convert(varchar,TravelDate,103) as Todate,LoginTime as TimeIn,logouttime as Timeout, [FromVillagename] as [FromVillagename],[ToVillagename] ,isnull(RevisedFare,0) as LC,isnull(RevisedDAAdmin,0) as DA", "userid='" + ddlFC.SelectedValue + "' and mYear='" + ddlYear.SelectedValue + "' and deleteflag=1  ", "TravelDate", "ASC";
            int Acount = dttravelmatrixdetails.Rows.Count;
            int MainCount = 0;
            int icount = 0;
            if (Acount > 12)
            {
                MainCount = 12;

            }
            else
            {
                MainCount = Acount;

            }
            int tot = 0;
            int DA = 0;
            //if (pageindex <= 15)
            //{


            //sb += "<tr style='font-size:20px;'>";
            //sb += "<td style='font-size:20px;text-align:center'>";


            empname = dtemployee.Rows[0]["name"].ToString();
            empcode = dtemployee.Rows[0]["code"].ToString();
            designation = dtemployee.Rows[0]["desg"].ToString();
            district = dtemployee.Rows[0]["districtname"].ToString();
            Block = dtemployee.Rows[0]["BlockName"].ToString();
            cluster = dtemployee.Rows[0]["cluster"].ToString();
            depatment = dtemployee.Rows[0]["Department"].ToString();
            Reporting = dtemployee.Rows[0]["Reporting Manager"].ToString();

            DataTable sqldtTourPlan = new DataTable();
            if (Acount > 12)
            {
                sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; font-family:Calibri (Body)' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                sb += " <tr style='background: #fff2cc;'>";
                sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px;font-family:Calibri (Body)'> Employee Name: <b>" + empname + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> Employee Code:<b>" + empcode + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> Designation:<b>Field Coordinator</b>  ";
                sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager: <b>" + Reporting + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> District: <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004<b></b>";
                sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                sb += " < td>Form No: <b>" + FromNo + " </b></td> </tr> </tbody> </table> </td> </tr>";
                sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                              "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                              "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                              " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                              "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                              " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                          " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                           "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                           "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                           " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                           "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                             " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                              " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                              " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                              "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                                "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                              " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                              "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                              "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                              "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";


                for (int i = 0; i < MainCount; i++)
                {

                    sb += "<tr  style='border: 0'>";
                    sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                    sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                    sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                    sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                    sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                    sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                    sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                    sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                    sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                    //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                    sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                    sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                    sb += "</tr>";

                    tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                    //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];
                }
                if (Acount > 12)
                {
                    sb += " </tbody> </table>";
                }

                if (Acount > 12)
                {
                    int Ig = 0;
                    if (Acount > 24)
                    {
                        Ig = 24;
                    }
                    else
                    {
                        Ig = Acount - 12;
                        Ig = 12 + Ig;
                    }

                    sb += "<div style='page-break-before : always;'> </div>";
                    sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                    sb += " <tr style='background: #fff2cc;'>";
                    sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                    sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Employee Code: <b>" + empcode + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                    sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager: <b>" + Reporting + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> District: <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                    sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                    sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                    sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                    sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                    sb += " < td>Form No: <b>" + FromNo + " </b></td> </tr> </tbody> </table> </td> </tr>";
                    sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                                  "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                                  "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                                  " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                                  "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                                  " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                              " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                               "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                               "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                               " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                               "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                                 " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                                  " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                                  " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                                  "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                                    "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                                  " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                                  "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                                  "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                                  "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";



                    for (int i = 12; i < Ig; i++)
                    {


                        sb += "<tr  style='border: 0'>";
                        sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                        sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                        sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                        sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                        sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                        sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                        //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                        sb += "</tr>";
                        tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);


                        //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];



                    }

                }
                if (Acount > 24)
                {
                    sb += " </tbody> </table>";
                }
                if (Acount > 24)
                {
                    int Ig = 0;

                    if (Acount > 36)
                    {
                        Ig = 36;
                    }
                    else
                    {
                        Ig = Acount - 24;
                        Ig = 24 + Ig;
                    }


                    sb += "<div style='page-break-before : always;'> </div>";
                    sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                    sb += " <tr style='background: #fff2cc;'>";
                    sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                    sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Employee Code : <b>" + empcode + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                    sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager : <b>" + Reporting + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> District : <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                    sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                    sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                    sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                    sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                    sb += " < td>Form No: <b>" + FromNo + " </b></td> </tr> </tbody> </table> </td> </tr>";
                    sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                          "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                          " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                          " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                      " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                       "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                       "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                       " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                       "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                         " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                            "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                          " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                          "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";

                    for (int i = 24; i < Ig; i++)
                    {

                        sb += "<tr  style='border: 0'>";
                        sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                        sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                        sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                        sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                        sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                        sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                        //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                        sb += "</tr>";
                        tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];



                    }

                }

                if (Acount > 36)
                {
                    sb += " </tbody> </table>";
                }
                if (Acount > 36)
                {
                    int Ig = 0;

                    if (Acount > 48)
                    {
                        Ig = 48;
                    }
                    else
                    {
                        Ig = Acount - 48;
                        Ig = 48 + Ig;
                    }


                    sb += "<div style='page-break-before : always;'> </div>";
                    sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                    sb += " <tr style='background: #fff2cc;'>";
                    sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                    sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Employee Code : <b>" + empcode + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                    sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager : <b>" + Reporting + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> District : <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                    sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                    sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                    sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                    sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                    sb += " < td>Form No: <b>" + FromNo + " </b></td> </tr> </tbody> </table> </td> </tr>";
                    sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                          "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                          " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                          " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                      " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                       "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                       "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                       " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                       "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                         " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                            "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                          " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                          "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";

                    for (int i = 36; i < Ig; i++)
                    {

                        sb += "<tr  style='border: 0'>";
                        sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                        sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                        sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                        sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                        sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                        sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                        //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                        sb += "</tr>";
                        tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];



                    }

                }

                if (Acount > 48)
                {
                    sb += " </tbody> </table>";
                }

                if (Acount > 48)
                {
                    int Ig = 0;

                    if (Acount > 60)
                    {
                        Ig = 60;
                    }
                    else
                    {
                        Ig = Acount - 60;
                        Ig = 60 + Ig;
                    }


                    sb += "<div style='page-break-before : always;'> </div>";
                    sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                    sb += " <tr style='background: #fff2cc;'>";
                    sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                    sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Employee Code : <b>" + empcode + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                    sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager : <b>" + Reporting + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> District : <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                    sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                    sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                    sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                    sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                    sb += " < td>Form No: <b>" + FromNo + " </b></td> </tr> </tbody> </table> </td> </tr>";
                    sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                          "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                          " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                          " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                      " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                       "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                       "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                       " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                       "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                         " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                            "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                          " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                          "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";

                    for (int i = 48; i < Ig; i++)
                    {

                        sb += "<tr  style='border: 0'>";
                        sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                        sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                        sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                        sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                        sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                        sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                        //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                        sb += "</tr>";
                        tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];



                    }

                }



                if (Acount > 60)
                {
                    sb += " </tbody> </table>";
                }

                if (Acount > 60)
                {
                    int Ig = 0;

                    if (Acount > 72)
                    {
                        Ig = 72;
                    }
                    else
                    {
                        Ig = Acount - 72;
                        Ig = 72 + Ig;
                    }


                    sb += "<div style='page-break-before : always;'> </div>";
                    sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                    sb += " <tr style='background: #fff2cc;'>";
                    sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                    sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Employee Code : <b>" + empcode + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                    sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager : <b>" + Reporting + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> District : <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                    sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                    sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                    sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                    sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                    sb += " < td>Form No: <b>" + FromNo + " </b></td> </tr> </tbody> </table> </td> </tr>";
                    sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                          "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                          " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                          " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                      " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                       "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                       "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                       " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                       "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                         " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                            "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                          " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                          "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";

                    for (int i = 60; i < Ig; i++)
                    {

                        sb += "<tr  style='border: 0'>";
                        sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                        sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                        sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                        sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                        sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                        sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                        //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                        sb += "</tr>";
                        tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];



                    }

                }

                if (Acount > 72)
                {
                    sb += " </tbody> </table>";
                }

                if (Acount > 72)
                {
                    int Ig = 0;

                    if (Acount > 84)
                    {
                        Ig = 84;
                    }
                    else
                    {
                        Ig = Acount - 84;
                        Ig = 84 + Ig;
                    }


                    sb += "<div style='page-break-before : always;'> </div>";
                    sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                    sb += " <tr style='background: #fff2cc;'>";
                    sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                    sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Employee Code : <b>" + empcode + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                    sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager : <b>" + Reporting + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> District : <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                    sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                    sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                    sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                    sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                    sb += " < td>Form No: <b>" + FromNo + " </b></td> </tr> </tbody> </table> </td> </tr>";
                    sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                          "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                          " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                          " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                      " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                       "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                       "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                       " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                       "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                         " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                            "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                          " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                          "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";

                    for (int i = 72; i < Ig; i++)
                    {

                        sb += "<tr  style='border: 0'>";
                        sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                        sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                        sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                        sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                        sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                        sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                        //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                        sb += "</tr>";
                        tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];



                    }

                }

                if (Acount > 84)
                {
                    sb += " </tbody> </table>";
                }

                if (Acount > 84)
                {
                    int Ig = 0;

                    if (Acount > 96)
                    {
                        Ig = 96;
                    }
                    else
                    {
                        Ig = Acount - 96;
                        Ig = 96 + Ig;
                    }


                    sb += "<div style='page-break-before : always;'> </div>";
                    sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                    sb += " <tr style='background: #fff2cc;'>";
                    sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                    sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Employee Code : <b>" + empcode + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                    sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager : <b>" + Reporting + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> District : <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                    sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                    sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                    sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                    sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                    sb += " < td>Form No: <b>" + FromNo + " </b></td> </tr> </tbody> </table> </td> </tr>";
                    sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                          "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                          " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                          " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                      " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                       "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                       "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                       " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                       "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                         " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                            "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                          " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                          "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";

                    for (int i = 84; i < Ig; i++)
                    {

                        sb += "<tr  style='border: 0'>";
                        sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                        sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                        sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                        sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                        sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                        sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                        //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                        sb += "</tr>";
                        tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];



                    }

                }

                if (Acount > 96)
                {
                    sb += " </tbody> </table>";
                }

                if (Acount > 96)
                {
                    int Ig = 0;

                    if (Acount > 108)
                    {
                        Ig = 108;
                    }
                    else
                    {
                        Ig = Acount - 108;
                        Ig = 108 + Ig;
                    }


                    sb += "<div style='page-break-before : always;'> </div>";
                    sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                    sb += " <tr style='background: #fff2cc;'>";
                    sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                    sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Employee Code : <b>" + empcode + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                    sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager : <b>" + Reporting + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> District : <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                    sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                    sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                    sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                    sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                    sb += " < td>Form No: <b>" + FromNo + " </b></td> </tr> </tbody> </table> </td> </tr>";
                    sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                          "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                          " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                          " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                      " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                       "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                       "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                       " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                       "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                         " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                            "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                          " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                          "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";

                    for (int i = 96; i < Ig; i++)
                    {

                        sb += "<tr  style='border: 0'>";
                        sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                        sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                        sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                        sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                        sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                        sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                        //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                        sb += "</tr>";
                        tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];



                    }

                }

                sb += "<tr style='border: 0'> <td colspan='18' style=' border-bottom: 1px solid #000000; text-align: right; padding-right: 15px; padding: 9px; font-weight: 900; font-size: 14px; border-left:1px solid #000; ' > TOTAL REIMBURSEMENT </td> <td style='border-bottom: 1px solid #000000; text-align:right ;border-right:1px solid #000;'>" + tot + "</td> </tr> </tbody> </table> ";

            }
            else
            {
                sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                sb += " <tr style='background: #fff2cc;'>";
                sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> Employee Code: <b>" + empcode + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager: <b>" + Reporting + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> District: <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                sb += " < td>Form No: <b>" + FromNo + " </b></td> </tr> </tbody> </table> </td> </tr>";
                sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                   "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                   "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                   " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                   "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                   " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
               " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                  " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                   " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                   " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                   "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                     "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                   " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                   "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                   "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                   "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";


                for (int i = 0; i < dttravelmatrixdetails.Rows.Count; i++)
                {

                    sb += "<tr  style='border: 0'>";
                    sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                    sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                    sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                    sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                    sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                    sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                    sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                    sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                    sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                    //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                    sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                    sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                    sb += "</tr>";

                    tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                    //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];
                }


                sb += "<tr style='border: 0'> <td colspan='18' style=' border-bottom: 1px solid #000000; text-align: right; padding-right: 15px; padding: 9px; font-weight: 900; font-size: 14px; border-left:1px solid #000; ' > TOTAL REIMBURSEMENT </td> <td style='border-bottom: 1px solid #000000; text-align:right ;border-right:1px solid #000;'>" + tot + "</td> </tr> </tbody> </table> ";

            }

            sb += "<div style='page-break-before : always;'> </div>";
            sb += "<table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody> <tr> <td colspan='1' style='border:1px solid #000; border-bottom:0; border-right:0; '></td> <td colspan='4' style=' font-size: 26px; text-align: center; font-weight: 900; padding: 15px; border:0; border-top:1px solid #000;' > Foundation to Educate Girls Globally </td> <td colspan='1' style=' text-align: right; border:1px solid #000; border-left: 0; border-bottom:0; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt='' /> </td> </tr>";

            sb += "<tr style='background: #fff2cc; border: 0'> <td colspan='6' style='padding: 15px; border:1px solid #000;'>";
            sb += " <table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' >";
            sb += " <tbody> <tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td> ";
            sb += "<td style='padding-bottom: 15px'> Employee Code: <b>" + empcode + "</b> </td> ";
            sb += "<td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b> </td> ";
            sb += "<td style='padding-bottom: 15px'> Reporting Manager: <b>" + Reporting + "</b> </td> ";
            sb += "<td style='padding-bottom: 15px'> District: <b>" + district + "</b> </td> </tr> </tbody> </table> ";
            sb += "<table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody> <tr style='font-size: 11px'> ";
            sb += "<td style='font-size: 11px'>Block: <b>" + Block + "</b></td> <td>Cluster: <b>" + cluster + " </b></td>";
            sb += " <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b></td> ";
            sb += "<td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
            sb += " <td>Form No: <b>" + FromNo + "</b></td> </tr> </tbody> </table> </td> </tr>";
            sb += " <tr> <th colspan='6' style=' font-weight: 900; font-size: 18px; padding: 9px; text-align: center; border:1px solid #000; ' > Other Expens </th> </tr>";
            sb += " <tr style='font-size: 10px; 'background: #ececec;'>  ";
            sb += "<th  width='9%' style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;  padding-top:15px; padding-bottom:15px;'>Date</th><th width='10%'  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;  padding-top:15px; padding-bottom:15px;'>Local Travel in KM</th> <th  width='30%' style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;  padding-top:15px; padding-bottom:15px;'>Description</th>";
            sb += " <th  width='10%' style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;  padding-top:15px; padding-bottom:15px;'>Conveyance</th> <th  width='10%' style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;  padding-top:15px; padding-bottom:15px;'>Others</th> ";
            sb += "<th  width='15%' style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;border-right:1px solid #000;  padding-top:15px; padding-bottom:15px;'>Remark</th> </tr> ";

            if (dttravex.Rows.Count > 0)
            {


                for (int i = 0; i < dttravex.Rows.Count; i++)
                {

                    sb += "<tr style='font-size:11px'>";

                    sb += "<td width='14%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravex.Rows[i]["Date"] + "</td>";

                    sb += "<td width='15%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;text-align:right;'>" + dttravex.Rows[i]["KM"] + "</td>";
                    sb += "<td width='20%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravex.Rows[i]["Desc"] + "</td>";
                    sb += "<td width='10%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;text-align:right;'>" + dttravex.Rows[i]["Conveyance"] + "</td>";
                    sb += "<td width='10%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;text-align:right;'>" + dttravex.Rows[i]["Other"] + "</td>";
                    //sb+="<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["DA"] + "</td>";
                    sb += "<td width='15%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000; border-right:1px solid #000;'>" + dttravex.Rows[i]["Remark"] + "</td>";
                    //sb+="<td width='14%' valign='top'></td>";


                    sb += "</tr>";


                }


            }
            else
            {
                sb += "<tr style='font-size:11px'>";

                sb += "<td width='14%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;'></td>";

                sb += "<td width='15%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;'></td>";
                sb += "<td width='20%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;'></td>";
                sb += "<td width='10%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;'></td>";
                sb += "<td width='10%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;'></td>";
                //sb+="<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["DA"] + "</td>";
                sb += "<td width='15%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000; border-right:1px solid #000;'></td>";
                //sb+="<td width='14%' valign='top'></td>";


                sb += "</tr>";

            }
            DataTable dttravelApprove = dstravle.Tables[3];
            sb += "</tbody> </table>";
            sb += "< table style='width: 100%; border-spacing: 0; border-collapse: 0; margin-bottom: 15px; border: 0; font-size: 11px; ' border='1' > ";

            sb += "<tr>";
            //sb += "<tr> <td>01/11/2024</td> <td>01/11/2024</td> <td>01/11/2024</td> <td>01/11/2024</td> ";
            //sb += "<td>01/11/2024</td> <td>01/11/2024</td> </tr> <tr>";
            sb += " <td colspan='6' style=' text-align: center; background: #ececec; font-weight: 900; font-size: 18px; padding: 9px; border:0 ; border-bottom:1px solid #000;' > Approval Status </td> </tr> ";
            sb += "<tr> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;' >Submission: " + dttravelApprove.Rows[0]["SubmittedStatus"].ToString() + " </td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Submitted By: " + dttravelApprove.Rows[0]["SubmittedBy"].ToString() + "  </td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Submitted Date: " + dttravelApprove.Rows[0]["SubmittedDate"].ToString() + "</td> </tr> ";
            sb += "<tr> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>BO Approval: " + dttravelApprove.Rows[0]["BOApprovalStatus"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Approved By: " + dttravelApprove.Rows[0]["BOApprovalBy"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Approval Date: " + dttravelApprove.Rows[0]["BOApprovalDate"].ToString() + "</td> </tr> ";
            sb += "<tr> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Admin Verification: " + dttravelApprove.Rows[0]["AdminApprovalStatus"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Verified By: " + dttravelApprove.Rows[0]["AdminApprovalBy"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Verified Date: " + dttravelApprove.Rows[0]["AdminApprovalDate"].ToString() + "</td> </tr> ";
            sb += "<tr> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>HR Verification: " + dttravelApprove.Rows[0]["HRApprovalStatus"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Verified By: " + dttravelApprove.Rows[0]["HRApprovalBy"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Verified Date: " + dttravelApprove.Rows[0]["HRApprovalDate"].ToString() + "</td> </tr> ";
            sb += "<tr> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>DOL Approval: " + dttravelApprove.Rows[0]["DOLApprovalStatus"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Approved By: " + dttravelApprove.Rows[0]["DOLApprovalBy"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Approval Date: " + dttravelApprove.Rows[0]["DOLApprovalDate"].ToString() + "</td> </tr> ";
            sb += "<tr> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Payment Status: " + dttravelApprove.Rows[0]["FinanceApprovalStatus"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Payment Processed by: " + dttravelApprove.Rows[0]["FinanceApprovalBy"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Payment Process Date:" + dttravelApprove.Rows[0]["FinanceApprovalDate"].ToString() + "</td> </tr> ";

            sb += " </table>";

            if (dttravexIMg.Rows.Count > 0)
            {
                sb += "<div style='page-break-before : always;'> </div>";

                sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                sb += " <tr style='background: #fff2cc;'>";
                sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> Employee Code: <b>" + empcode + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager: <b>" + Reporting + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> District: <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                sb += " < td>Form No: <b>" + FromNo + " </b></td> </tr> </tbody> </table></td> </tr>";


                sb += "</tbody> </table>";

                sb += "<table border=1 width='100%' cellspacing='2' cellpadding='2' style='font-size:10px;page-break-after: always; border-color:#dddddd;font-weight:normal'> ";
                int kcount = 0;
                for (int i = 0; i < dttravexIMg.Rows.Count; i++)
                {
                    string Imh = dttravexIMg.Rows[i]["ImagePath"].ToString();
                    string imageURLLogo1 = Server.MapPath(".") + "/Travel/" + Imh;
                    if (System.IO.File.Exists(imageURLLogo1))
                    {
                        kcount = kcount + 1;
                        sb += "<tr>";

                        sb += "<td valign='top'><img      src='" + imageURLLogo1 + "'  height='600px' width='960px'  alt='Bird' /></td>";
                        //sb+="<td width='14%' valign='top'>dfgfdg</td>";

                        sb += "</tr>";
                    }
                }
                if (kcount == 0)
                {
                    sb += "<tr>";

                    sb += "<td valign='top'></td>";

                    sb += "</tr>";
                }
                sb += "</table>";
            }


            StringReader sr = new StringReader(sb.ToString());
            Document pdfDoc = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
            // Document pdfDoc = new Document(PageSize.A4, 36, 36, 36, 72;
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

            string FC = Username;
            //var cssText = File.ReadAllText(MapPath("~/StyleSheet.css");


            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);

                pdfDoc.Open();
                pdfDoc.NewPage();

                using (TextReader reader = new StringReader(sb))
                {
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, reader);
                }

                pdfDoc.Close();
                byte[] bytes = memoryStream.ToArray();


                memoryStream.Close();

                File.WriteAllBytes(Request.PhysicalApplicationPath + "/Travel vouchers/TravelVoucher_" + ddlMonth.SelectedItem.Text + "_" + FC.Substring(0, 7) + DateTime.Now.ToString("ddMMyyyy_hhmmss") + ".pdf", bytes);
            }



            string filename = "Travel vouchers" + "_" + ddlMonth.SelectedItem.Text + "_" + FC.Substring(0, 7) + DateTime.Now.ToString("ddMMyyyy_hhmmss") + ".pdf";
            //  string dsssssssssssss = Request.PhysicalApplicationPath + "Travel vouchers\\TravelVoucher_" + ddlMonth.SelectedItem.Text + "_ " + ddlFc.SelectedItem.Text + ".pdf";
            WebClient req = new WebClient();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            string dsssssssssssss1 = Server.MapPath(Comman.GetImagePath("TravelvouchersPath") + "/TravelVoucher_" + ddlMonth.SelectedItem.Text + "_" + FC.Substring(0, 7) + DateTime.Now.ToString("ddMMyyyy_hhmmss") + ".pdf");
            byte[] data = req.DownloadData(dsssssssssssss1);
            response.BinaryWrite(data);
            //   Response.TransmitFile(Server.MapPath(Comman.GetImagePath("TravelvouchersPath") + "/"" + filename);
            response.End();




            //string filename ="TravelVoucher"+"_" +ddlFc.SelectedValue +".pdf";
            //FileInfo file = new FileInfo((Server.MapPath(Comman.GetImagePath("TravelvouchersPath") + "/"" + filename));
            //if (file.Exists)
            //{

            //    Response.ContentType = "application/octet-stream";
            //    Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename;
            //    string aaa = Server.MapPath(Comman.GetImagePath("TravelvouchersPath") + "/"" + filename;
            //    Response.TransmitFile(Server.MapPath(Comman.GetImagePath("TravelvouchersPath") + "/" + filename);


            //}


            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('BTOR Not Available.')</script>", false;

            //}


        }
        catch (System.Exception ex)
        {

            //   Response.Clear(;

            //string mmsg = ex.Message;
            //showEXPMessages("(crateZip)  " + mmsg; //showMessages(mmsg;
        }
        finally
        {

            //Response.Clear(;

        }

        return sb.ToString();

    }
    protected string GeneraatePDF(string FromNo, string Username)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        try
        {
            string Fdate = "";
            string Tdate = "";
            int mMonth = 0;
            if (ddlMonth.SelectedValue == "1")
            {
                mMonth = 12;
            }
            else
            {
                mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
            }
            if (ddlMonth.SelectedValue == "2" || ddlMonth.SelectedValue == "3")
            {
                Fdate = DateTime.Now.Year + 1 + "-" + mMonth + "-" + "21";
                Tdate = DateTime.Now.Year + 1 + "-" + ddlMonth.SelectedValue + "-" + "20";
            }
            else if (ddlMonth.SelectedValue == "4")
            {
                Fdate = DateTime.Now.Year + "-" + mMonth + "-" + "01";
                Tdate = DateTime.Now.Year + "-" + ddlMonth.SelectedValue + "-" + "20";
            }
            else if (ddlMonth.SelectedValue == "1")
            {
                Fdate = DateTime.Now.Year + "-" + mMonth + "-" + "21";
                Tdate = DateTime.Now.Year + 1 + "-" + ddlMonth.SelectedValue + "-" + "20";
            }
            else
            {
                Fdate = DateTime.Now.Year + "-" + mMonth + "-" + "21";
                Tdate = DateTime.Now.Year + "-" + ddlMonth.SelectedValue + "-" + "20";
            }
            int mYear = 0;

            if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
            {
                mYear = Convert.ToInt32(ddlYear.SelectedValue) + 1;
            }
            else
            {
                mYear = Convert.ToInt32(ddlYear.SelectedValue);
            }
            string empname = "", empcode = "", designation = "", district = "", Block = "", cluster = "", depatment = "", Reporting = "";
            DataTable dtemployee = objMain.Select_All_Data("MstUser inner join tblTravelMatrixDeatils2024 on  MstUser.UserName=tblTravelMatrixDeatils2024.UserID inner join  Mstuserrole on  MstUser.UserLevel= Mstuserrole.Role_Level inner join tblemployeedetails on tblemployeedetails.EmployeeID=MstUser.UserName inner join mst2District on mst2District.districtcode=MstUser.DistrictCode inner join mst3Block on mst3Block.blockcode=MstUser.blockcode inner join mst5Village on mst5Village.villagecode=MstUser.villagecode    inner join MstUser u on u.blockcode=MstUser.blockcode and u.UserLevel=19 and U.ActiveStatus=1", "distinct mstuser.FristName as name,mstuser.UserName as code,Mstuserrole.Role as desg,Department ,mst2District.districtname,BlockName,VillageName as cluster,U.userName +'-'+ u.FristName  as [Reporting Manager]", "MstUser.UserName='" + Username + "' and mYear=" + mYear + " and mMonth=" + Convert.ToInt32(ddlMonth.SelectedValue) + "", "", "");

            if (dtemployee.Rows.Count > 0)
            {

                empname = dtemployee.Rows[0]["name"].ToString();
                empcode = dtemployee.Rows[0]["code"].ToString();
                designation = dtemployee.Rows[0]["desg"].ToString();
                district = dtemployee.Rows[0]["districtname"].ToString();
                Block = dtemployee.Rows[0]["BlockName"].ToString();
                cluster = dtemployee.Rows[0]["cluster"].ToString();
                depatment = dtemployee.Rows[0]["Department"].ToString();
                Reporting = dtemployee.Rows[0]["Reporting Manager"].ToString();

            }


            string imageURLLogo = Server.MapPath(".") + "/images/logo-new1.png";


            sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");

            SqlParameter[] parm1 = new SqlParameter[]
     {

             new SqlParameter("@UserName", Username),
             new SqlParameter("@month", ddlMonth.SelectedValue),
              new SqlParameter("@Myear",mYear),
                 new SqlParameter("@UserRole",Convert.ToString(Session["user_level_Role"])),
         new SqlParameter("@FromNo",FromNo),



     };


            DataSet dstravle = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptTravelDeatil2024View", parm1);

            DataTable dttravelmatrixdetails = dstravle.Tables[0];
            // DataTable dttravelmatrixdetails = objMain.Select_All_Data("tblTravelMatrixDeatils2024", "convert(varchar,TravelDate,103) as Fromdate,convert(varchar,TravelDate,103) as Todate,LoginTime as TimeIn,logouttime as Timeout, [FromVillagename] as [FromVillagename],[ToVillagename] ,isnull(RevisedFare,0) as LC,isnull(RevisedDAAdmin,0) as DA", "userid='" + ddlFC.SelectedValue + "' and mYear='" + ddlYear.SelectedValue + "' and deleteflag=1  ", "TravelDate", "ASC");

            int tot = 0;
            int DA = 0;
            //if (pageindex <= 15)
            //{
            sb.Append("<tr style='font-size:20px;'>");
            sb.Append("<td style='font-size:20px;text-align:center'>");


            sb.Append("<table width='100%' border=1 cellspacing='2' cellpadding='2' style=' font-size:17px; border-color:#dddddd;BACKGROUND-COLOR:Red'> ");

            sb.Append("<tr style='font-size:20px;font-weight:bold'>");
            sb.Append("<td style='font-size:20px;text-align:center' colspan='10'>Foundation to Educate Girls Globally</td><td   style='text-align:right;border:none;' > <img width='50%' height='40%' src='" + imageURLLogo + "' alt='Bird' /> </td>");

            sb.Append("</tr>");
            sb.Append("</table>");


            sb.Append("<table  width='100%' border=1 cellspacing='2' cellpadding='2' style=' font-size:17px; border-color:#dddddd;BACKGROUND-COLOR:Red'> ");

            sb.Append("<tr style='font-size:20px;font-weight:bold'>");
            sb.Append("<td style='font-size:20px;text-align:center'>Travel Settlement form</td>");
            sb.Append("</tr>");

            sb.Append("</table>");



            sb.Append("<table bgColor='#BDD7EE' width='100%' border=1 cellspacing='2' cellpadding='2' style=' font-size:17px; border-color:#dddddd;BACKGROUND-COLOR:Red'> ");
            sb.Append("<tr style='font-size:12px;font-weight:bold;background-color='Red''><td width='14%' >Name of Employee:</td><td width='14%'>Employee Code</td><td width='15%'>Designation</td><td width='15%'>Reporting Manager</td><td width='14%' valign='top'>District / Office Name</td><td width='14%'>Block Name</td><td width='14%'>Cluster Name</td></tr>");
            sb.Append("</table>");

            DataTable sqldtTourPlan = new DataTable();

            sb.Append("<table border=1 width='100%' cellspacing='2' cellpadding='2' style=' font-size:17px; border-color:#dddddd'> ");
            sb.Append("<tr style='font-size:10px'>");

            sb.Append("<td width='14%' valign='top'>" + empname + "</td>");
            sb.Append("<td width='14%' valign='top'>" + empcode + "</td>");
            sb.Append("<td width='15%' valign='top'>" + designation + "</td>");
            sb.Append("<td width='15%' valign='top'>" + Reporting + "</td>");
            sb.Append("<td width='14%' valign='top'>" + district + "</td>");
            sb.Append("<td width='14%' valign='top'>" + Block + "</td>");
            sb.Append("<td width='14%' valign='top'>" + cluster + "</td></tr>");
            sb.Append("</table>");
            sb.Append("<table   background-color='#F1F1F1' width='100%' cellspacing='0' cellpadding='2'>");

            sb.Append("</table>");

            sb.Append("<table bgColor='#BDD7EE' width='100%' border=1 cellspacing='2' cellpadding='2' style=' font-size:17px; border-color:#dddddd'> ");
            sb.Append("<tr style='font-size:12px;font-weight:bold'><td width='14%'>Department:</td><td width='14%'>Department Code</td><td width='15%'>Work Level</td><td width='15%' colspan='4'>Settlement Period</td></tr>");

            sb.Append("</table>");

            sb.Append("<table border=1 width='100%' cellspacing='2' cellpadding='2' style=' font-size:17px; border-color:#dddddd'> ");
            sb.Append("<tr style='font-size:10px'>");

            sb.Append("<td width='14%' valign='top'>" + depatment + "</td>");
            sb.Append("<td width='14%' valign='top'></td>");
            sb.Append("<td width='15%' valign='top'></td>");
            sb.Append("<td bgColor='#BDD7EE' width='15%' valign='top'>From:</td>");
            sb.Append("<td width='14%' valign='top'>" + Convert.ToDateTime(Fdate).ToString("dd-MM-yyyy") + "</td>");
            sb.Append("<td bgColor='#BDD7EE' width='14%' valign='top'>To:</td>");
            sb.Append("<td width='14%' valign='top'>" + Convert.ToDateTime(Tdate).ToString("dd-MM-yyyy") + "</td></tr>");
            sb.Append("</table>");
            sb.Append("<table>");
            sb.Append("<tr>");
            sb.Append("<td>");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<table>");
            sb.Append("<tr>");
            sb.Append("<td>");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<table>");
            sb.Append("<tr>");
            sb.Append("<td>");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<table>");
            sb.Append("<tr>");
            sb.Append("<td>");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<table border=1 width='100%' cellspacing='2' cellpadding='2' style='border-color:#dddddd'>");

            // sb.Append("<tr  bgColor='#BDD7EE' style='font-size:12px;font-weight:bold'><td width='14%' colspan='10'></td><td width='14%'>Cost Centre 013</td><td width='14%'>Cost Centre 012</td><td width='14%'>Cost Centre 011</td><td width='14%'>Cost Centre 010</td><td width='14%' colspan='2'></td></tr>");



            // sb.Append("<tr  bgColor='#A9D08E' style='font-size:12px;font-weight:bold'><td  style='display:none' width='14%'>Date from:</td><td width='14%'>Time In:</td><td width='15%'>Date To</td><td width='15%'>Time Out</td><td width='15%'>Travelling from</td><td width='15%'>Travelling to</td><td width='15%'>Purpose of Visit</td><td width='15%'>Lodging (Guest House/Hotel)</td><td width='15%'>Self Paid / BTC (applicable for hotel booking)</td><td width='15%'>Mode of Travel</td><td width='15%'>Local conveyance</td><td width='15%'>Lodging</td><td width='15%'>DA</td><td width='15%'>Travel Expenses</td><td width='15%'>Others</td><td width='15%'>Total</td></tr>");
            sb.Append("<tr  bgColor='#A9D08E' style='font-size:12px;font-weight:bold'><td width='15%'>Date from</td><td width='15%'>Time In</td><td width='15%'>Date To</td><td width='15%'>Time Out</td><td width='15%'>Travelling from</td><td width='15%'>Travelling to</td><td width='15%'>Purpose of Visit</td><td width='15%'>KM- Within Cluster</td><td width='15%'>KM- Outside Cluster</td><td width='15%'>Place of Accommodation</td><td width='15%'>Accommodation Payment Type</td><td width='15%'>Accommodation Occupancy</td><td width='15%'>Mode of Travel</td><td width='15%'> Local Conveyance</td><td width='15%'>Accommodation Cost</td><td width='15%'>Per Diem</td><td width='15%'>Travel Expenses</td><td width='15%'>Others</td><td width='15%'>Total</td></tr>");
            //sb.Append("<tr  bgColor='#A9D08E' style='font-size:12px;font-weight:bold'><td width='14%'>Date from</td><td width='14%'>Time In</td><td width='15%'>Date To</td><td width='15%'>Time Out</td><td width='15%'>Travelling from</td><td width='15%'>Travelling to</td><td width='15%'>Purpose of Visit</td><td width='15%'>Lodging (Guest House/Hotel)</td><td width='15%'>Self Paid / BTC (applicable for hotel booking)</td><td width='15%'>Mode of Travel</td><td width='15%'>Local conveyance</td><td width='15%'>Lodging</td><td width='15%'>DA</td><td width='15%'>Travel Expenses</td><td width='15%'>Others</td><td width='15%'>Total</td></tr>");

            sb.Append("</table>");





            if (dttravelmatrixdetails.Rows.Count > 0)
            {
                //int rownum = 5;
                //int p = 5;
                for (int i = 0; i < dttravelmatrixdetails.Rows.Count; i++)
                {

                    sb.Append("<table border=1 width='100%' cellspacing='2' cellpadding='2' style=' font-size:10px; border-color:#dddddd;font-weight:normal'> ");
                    sb.Append("<tr>");
                    sb.Append("<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>");
                    sb.Append("<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>");
                    sb.Append("<td width='15%' valign='top'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>");
                    sb.Append("<td width='15%' valign='top'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>");
                    sb.Append("<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>");
                    sb.Append("<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>");
                    sb.Append("<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>");
                    sb.Append("<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>");
                    sb.Append("<td width='15%' valign='top'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>");
                    sb.Append("<td width='15%' valign='top'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>");
                    sb.Append("<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>");
                    sb.Append("<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>");
                    sb.Append("<td width='15%' valign='top'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>");
                    sb.Append("<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>");
                    sb.Append("<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>");
                    Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);
                    sb.Append("<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>");
                    sb.Append("<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>");
                    sb.Append("<td width='15%' valign='top'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>");
                    sb.Append("<td width='15%' valign='top'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>");
                    sb.Append("</table>");



                    tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                    //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"]);
                }


            }
            else
            {

            }



            sb.Append("</td>");
            sb.Append("</tr>");
            //    }

            //add table here
            sb.Append("<tr style='font-size:9px;'>");
            sb.Append("<td style='font-size:9px;'>");
            sb.Append("<table  width='100%' border=1 cellspacing='2' cellpadding='2' style=' font-size:9px; border-color:#dddddd'>");
            //sb.Append("<tr>");
            //sb.Append("<td colspan='10'></td>");

            //sb.Append("<td style='text-align:center'>" + tot + "</td>");

            //sb.Append("<td style='text-align:center' > " + DA + "</td>");

            //sb.Append("<td style='text-align:center' >  0.00</td>");

            //sb.Append("<td style='text-align:center' >  0.00</td>");

            //sb.Append("<td style='text-align:center'>  0.00</td>");
            //int TOalDA = tot + DA;
            //sb.Append("<td style='text-align:center'>  " + TOalDA + "</td>");


            //sb.Append("</tr>");

            DataTable dttravelApprove = dstravle.Tables[3];

            sb.Append("<tr>");
            sb.Append("<td colspan='10'>  </td>");

            sb.Append("<td> </td>");

            sb.Append("<td>  </td>");

            sb.Append("<td colspan='3'> TOTAL REIMBURSEMENT:</td>");



            sb.Append("<td style='text-align:center'> " + tot + "</td>");


            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td colspan='16' style='text-align:center'> Approve Status :</td>");

            //sb.Append("<td> </td>");

            //sb.Append("<td>  </td>");

            //sb.Append("<td> Advances:</td>");

            //sb.Append("<td> </td>");

            //sb.Append("<td>  </td>");

            //sb.Append("<td style='text-align:center'> </td>");


            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td colspan='6'>Submission: " + dttravelApprove.Rows[0]["SubmittedStatus"].ToString() + " </td>");

            sb.Append("<td colspan='5'>Submitted By: " + dttravelApprove.Rows[0]["SubmittedBy"].ToString() + "  </td>");

            sb.Append("<td colspan='5'> Submitted Date:   " + dttravelApprove.Rows[0]["SubmittedDate"].ToString() + "  </td>");

            //sb.Append("<td colspan='3'>HR Verification</td>");
            //sb.Append("<td colspan='3'>DOL Approval</td>");
            //sb.Append("<td colspan='3'>Payment Status</td>");


            sb.Append("</tr>");
            sb.Append("<tr>");

            sb.Append("<td colspan='6'>BO Approval: " + dttravelApprove.Rows[0]["BOApprovalStatus"].ToString() + "</td>");

            sb.Append("<td colspan='5'> Approved By: " + dttravelApprove.Rows[0]["BOApprovalBy"].ToString() + "</td>");

            sb.Append("<td colspan='5'> Approval Date: " + dttravelApprove.Rows[0]["BOApprovalDate"].ToString() + "</td>");



            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td colspan='6'>Admin Approval: " + dttravelApprove.Rows[0]["AdminApprovalStatus"].ToString() + "</td>");

            sb.Append("<td colspan='5'> Approved By: " + dttravelApprove.Rows[0]["AdminApprovalBy"].ToString() + "</td>");

            sb.Append("<td colspan='5'> Approval Date: " + dttravelApprove.Rows[0]["AdminApprovalDate"].ToString() + "</td>");



            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td colspan='6'>HR Verification: " + dttravelApprove.Rows[0]["HRApprovalStatus"].ToString() + "</td>");

            sb.Append("<td colspan='5'> Verified By " + dttravelApprove.Rows[0]["HRApprovalBy"].ToString() + "</td>");

            sb.Append("<td colspan='5'> Verified Date: " + dttravelApprove.Rows[0]["HRApprovalDate"].ToString() + "</td>");



            sb.Append("</tr>");

            sb.Append("<tr>");
            sb.Append("<td colspan='6'>DOL Verification:" + dttravelApprove.Rows[0]["DOLApprovalStatus"].ToString() + "</td>");

            sb.Append("<td colspan='5'> Verified By:" + dttravelApprove.Rows[0]["DOLApprovalBy"].ToString() + "</td>");

            sb.Append("<td colspan='5'> Verified Date:" + dttravelApprove.Rows[0]["DOLApprovalDate"].ToString() + "</td>");



            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td colspan='6'>Payment Status:" + dttravelApprove.Rows[0]["FinanceApprovalStatus"].ToString() + "</td>");

            sb.Append("<td colspan='5'> Payment Processed by:" + dttravelApprove.Rows[0]["FinanceApprovalBy"].ToString() + "</td>");

            sb.Append("<td colspan='5'> Payment Process Date:" + dttravelApprove.Rows[0]["FinanceApprovalDate"].ToString() + "</td>");



            sb.Append("</tr>");

            sb.Append("</table>");
            sb.Append("</td>");
            sb.Append("</tr>");


            sb.Append("<tr style='font-size:20px;font-weight:bold'>");
            sb.Append("<td style='font-size:20px;text-align:center'>&nbsp;&nbsp;</td>");
            sb.Append("</tr>");

            sb.Append("<tr style='font-size:20px;font-weight:bold'>");
            sb.Append("<td style='font-size:20px;text-align:center'>&nbsp;&nbsp;</td>");
            sb.Append("</tr>");

            sb.Append("<tr style='font-size:20px;font-weight:bold'>");
            sb.Append("<td style='font-size:20px;text-align:center'>&nbsp;&nbsp;</td>");
            sb.Append("</tr>");

            sb.Append("<tr style='font-size:20px;font-weight:bold'>");
            sb.Append("<td style='font-size:20px;text-align:center'>&nbsp;&nbsp;</td>");
            sb.Append("</tr>");





            sb.Append("<tr style='font-size:20px;font-weight:bold'>");
            sb.Append("<td style='font-size:20px;text-align:center'>");
            // sb.Append(" < p style = 'page -break-after: always;' > &nbsp;</ p >");
            sb.Append("<table bgColor='#BDD7EE' width='100%' border=1 cellspacing='2' cellpadding='2' style=' font-size:17px; border-color:#dddddd;BACKGROUND-COLOR:Red'> ");
            sb.Append("<tr style='font-size:12px;font-weight:bold;background-color='Red''><td width='14%' >Name of Employee:</td><td width='14%'>Employee Code</td><td width='15%'>Designation</td><td width='15%'>Reporting Manager</td><td width='14%' valign='top'>District / Office Name</td><td width='14%'>Block Name</td><td width='14%'>Cluster Name</td></tr>");
            sb.Append("</table>");
            // sb.Append(" < p &nbsp;</ p >");



            sb.Append("<table border=1 width='100%' cellspacing='2' cellpadding='2' style=' font-size:17px; border-color:#dddddd'> ");
            sb.Append("<tr style='font-size:10px'>");

            sb.Append("<td width='14%' valign='top'>" + empname + "</td>");
            sb.Append("<td width='14%' valign='top'>" + empcode + "</td>");
            sb.Append("<td width='15%' valign='top'>" + designation + "</td>");
            sb.Append("<td width='15%' valign='top'>" + Reporting + "</td>");
            sb.Append("<td width='14%' valign='top'>" + district + "</td>");
            sb.Append("<td width='14%' valign='top'>" + Block + "</td>");
            sb.Append("<td width='14%' valign='top'>" + cluster + "</td></tr>");
            sb.Append("</table>");
            sb.Append("<table   background-color='#F1F1F1' width='100%' cellspacing='0' cellpadding='2'>");

            sb.Append("</table>");
            sb.Append("<table bgColor='#BDD7EE' width='100%' border=1 cellspacing='2' cellpadding='2' style=' font-size:17px; border-color:#dddddd'> ");
            sb.Append("<tr style='font-size:12px;font-weight:bold'><td width='14%'>Department:</td><td width='14%'>Department Code</td><td width='15%'>Work Level</td><td width='15%' colspan='4'>Settlement Period</td></tr>");

            sb.Append("</table>");

            sb.Append("<table border=1 width='100%' cellspacing='2' cellpadding='2' style=' font-size:17px; border-color:#dddddd'> ");
            sb.Append("<tr style='font-size:10px'>");

            sb.Append("<td width='14%' valign='top'>" + depatment + "</td>");
            sb.Append("<td width='14%' valign='top'></td>");
            sb.Append("<td width='15%' valign='top'></td>");
            sb.Append("<td bgColor='#BDD7EE' width='15%' valign='top'>From:</td>");
            sb.Append("<td width='14%' valign='top'>" + Convert.ToDateTime(Fdate).ToString("dd-MM-yyyy") + "</td>");
            sb.Append("<td bgColor='#BDD7EE' width='14%' valign='top'>To:</td>");
            sb.Append("<td width='14%' valign='top'>" + Convert.ToDateTime(Tdate).ToString("dd-MM-yyyy") + "</td></tr>");
            sb.Append("</table>");
            sb.Append("<table>");
            sb.Append("<tr>");
            sb.Append("<td>");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<table>");
            sb.Append("<tr>");
            sb.Append("<td>");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<table>");
            sb.Append("<tr>");
            sb.Append("<td>");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<table>");
            sb.Append("<tr>");
            sb.Append("<td>");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            //sb.Append("<table border=1 width='100%' cellspacing='2' cellpadding='2' ");

            //sb.Append("<td colspan='6' width='14%' valign='top'>");

            //sb.Append("<table border=1 width='100%' cellspacing='2' cellpadding='2' style='border-color:#dddddd'>");

            //  // sb.Append("<tr  bgColor='#BDD7EE' style='font-size:12px;font-weight:bold'><td width='14%' colspan='10'></td><td width='14%'>Cost Centre 013</td><td width='14%'>Cost Centre 012</td><td width='14%'>Cost Centre 011</td><td width='14%'>Cost Centre 010</td><td width='14%' colspan='2'></td></tr>");



            //  // sb.Append("<tr  bgColor='#A9D08E' style='font-size:12px;font-weight:bold'><td  style='display:none' width='14%'>Date from:</td><td width='14%'>Time In:</td><td width='15%'>Date To</td><td width='15%'>Time Out</td><td width='15%'>Travelling from</td><td width='15%'>Travelling to</td><td width='15%'>Purpose of Visit</td><td width='15%'>Lodging (Guest House/Hotel)</td><td width='15%'>Self Paid / BTC (applicable for hotel booking)</td><td width='15%'>Mode of Travel</td><td width='15%'>Local conveyance</td><td width='15%'>Lodging</td><td width='15%'>DA</td><td width='15%'>Travel Expenses</td><td width='15%'>Others</td><td width='15%'>Total</td></tr>");
            //  sb.Append("<tr  bgColor='#A9D08E' style='font-size:12px;font-weight:bold'><td width='15%'>Date</td><td width='15%'>Description</td><td width='15%'>Local Travel in KM</td><td width='15%'>Conveyance</td><td width='15%'>Others</td><td width='15%'>Remark</td></tr>");
            //  //sb.Append("<tr  bgColor='#A9D08E' style='font-size:12px;font-weight:bold'><td width='14%'>Date from</td><td width='14%'>Time In</td><td width='15%'>Date To</td><td width='15%'>Time Out</td><td width='15%'>Travelling from</td><td width='15%'>Travelling to</td><td width='15%'>Purpose of Visit</td><td width='15%'>Lodging (Guest House/Hotel)</td><td width='15%'>Self Paid / BTC (applicable for hotel booking)</td><td width='15%'>Mode of Travel</td><td width='15%'>Local conveyance</td><td width='15%'>Lodging</td><td width='15%'>DA</td><td width='15%'>Travel Expenses</td><td width='15%'>Others</td><td width='15%'>Total</td></tr>");

            //  sb.Append("</table>");



            sb.Append("<table border=1 width='100%' cellspacing='2' cellpadding='2' >");


            //sb.Append("<tr  bgColor='#A9D08E' style='font-size:12px;font-weight:bold'><td width='15%'>Date</td><td width='15%'>Description</td><td width='15%'>Local Travel in KM</td><td width='15%'>Conveyance</td><td width='15%'>Others</td><td width='15%'>Remark</td></tr>");
            //sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td  colspan='6' >");

            sb.Append("<table border=1 width='100%' valign='top' cellspacing='2' cellpadding='2' style=' font-size:10px;vertical-align:top; border-color:#dddddd;font-weight:normal'> ");
            sb.Append("<tr>");

            sb.Append("<td width='14%' valign='top'>Date</td>");
            sb.Append("<td width='14%' valign='top'>Description</td>");
            sb.Append("<td width='15%' valign='top'>Local Travel in KM</td>");
            sb.Append("<td width='15%' valign='top'>Conveyance</td>");
            sb.Append("<td width='14%' valign='top'>Others</td>");
            //sb.Append("<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["DA"] + "</td>");
            sb.Append("<td width='15%' valign='top'>Remark</td>");
            //sb.Append("<td width='14%' valign='top'></td>");

            sb.Append("</table>");
            DataTable dttravex = dstravle.Tables[1];
            DataTable dttravexIMg = dstravle.Tables[2];
            if (dttravex.Rows.Count > 0)
            {
                //int rownum = 5;
                //int p = 5;
                for (int i = 0; i < dttravex.Rows.Count; i++)
                {

                    sb.Append("<table border=1 width='100%' cellspacing='2' cellpadding='2' style=' font-size:10px;vertical-align:top; border-color:#dddddd;font-weight:normal'> ");
                    sb.Append("<tr>");

                    sb.Append("<td width='14%' valign='top'>" + dttravex.Rows[i]["Date"] + "</td>");
                    sb.Append("<td width='14%' valign='top'>" + dttravex.Rows[i]["Desc"] + "</td>");
                    sb.Append("<td width='15%' valign='top'>" + dttravex.Rows[i]["KM"] + "</td>");
                    sb.Append("<td width='15%' valign='top'>" + dttravex.Rows[i]["Conveyance"] + "</td>");
                    sb.Append("<td width='14%' valign='top'>" + dttravex.Rows[i]["Other"] + "</td>");
                    //sb.Append("<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["DA"] + "</td>");
                    sb.Append("<td width='15%' valign='top'>" + dttravex.Rows[i]["Remark"] + "</td>");
                    //sb.Append("<td width='14%' valign='top'></td>");

                    sb.Append("</table>");


                }


            }
            sb.Append("</td>");


            sb.Append("<table  width='100%'  style=' font-size:10px;'> ");
            sb.Append("<tr>");

            sb.Append("<td colspan='6' valign='top'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;...</ td>");

            sb.Append("</tr>");


            sb.Append("</table>");
            sb.Append("<table border=0 width='100%' cellspacing='2' cellpadding='2' style=' font-size:10px;margin-top:120px; border-color:#dddddd;font-weight:normal'> ");

            //sb.Append("<tr style='font-size:9px'>");

            //sb.Append("<td  valign='top'>	</ td>");
            //sb.Append("<td  valign='top'></td>");

            //sb.Append("</tr>");
            //sb.Append("<tr style='font-size:9px'>");

            //sb.Append("<td  valign='top'></ td>");
            //sb.Append("<td  valign='top'></td>");

            //sb.Append("</tr>");

            sb.Append("<tr style='font-size:9px'>");

            sb.Append("<td  valign='top'>Department Code	</ td>");
            sb.Append("<td  valign='top'>Department Name</td>");

            sb.Append("</tr>");

            sb.Append("<tr style='font-size:9px'>");

            sb.Append("<td  valign='top'>130007</td>");
            sb.Append("<td  valign='top'>Government Liaison</td>");

            sb.Append("</tr>");

            sb.Append("<tr style='font-size:9px'>");

            sb.Append("<td  valign='top'>130016</td>");
            sb.Append("<td  valign='top'>Volunteer Engagement</td>");

            sb.Append("</tr>");


            sb.Append("<tr style='font-size:9px'>");

            sb.Append("<td  valign='top'>130009</td>");
            sb.Append("<td  valign='top'>Finance & Accounts</td>");

            sb.Append("</tr>");

            sb.Append("<tr style='font-size:9px'>");

            sb.Append("<td  valign='top'>130010</td>");
            sb.Append("<td  valign='top'>HR & Administration</td>");

            sb.Append("</tr>");
            sb.Append("<tr style='font-size:9px'>");

            sb.Append("<td  valign='top'>130011</td>");
            sb.Append("<td  valign='top'>IT </td>");

            sb.Append("</tr>");

            sb.Append("<tr style='font-size:9px'>");

            sb.Append("<td  valign='top'>130012</td>");
            sb.Append("<td  valign='top'>ED Office </td>");

            sb.Append("</tr>");

            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");





            sb.Append("</table>");



            sb.Append("</td>");
            sb.Append("</td>");
            sb.Append("</tr>");

            //sb.Append("</table>");


            sb.Append("<tr style='font-size:20px;font-weight:bold'>");
            sb.Append("<td style='font-size:20px;text-align:center'>&nbsp;&nbsp;</td>");
            sb.Append("</tr>");

            sb.Append("<tr style='font-size:20px;font-weight:bold'>");
            sb.Append("<td style='font-size:20px;text-align:center'>&nbsp;&nbsp;</td>");
            sb.Append("</tr>");
            sb.Append("<tr style='font-size:20px;font-weight:bold'>");
            sb.Append("<td style='font-size:20px;text-align:center'>&nbsp;&nbsp;</td>");
            sb.Append("</tr>");

            sb.Append("<tr style='font-size:20px;font-weight:bold'>");
            sb.Append("<td style='font-size:20px;text-align:center'>&nbsp;&nbsp;</td>");
            sb.Append("</tr>");

            sb.Append("<tr style='font-size:20px;font-weight:bold'>");
            sb.Append("<td style='font-size:20px;text-align:center'>");
            sb.Append("<table width='100%' cellspacing='2' cellpadding='2' style=' font-size:17px; border-color:#dddddd'> ");
            sb.Append("<tr style='font-size:10px'>");

            sb.Append("<td width='14%' valign='top'>");

            sb.Append("</td>");
            sb.Append("<td width='100%' valign='top'>");
            sb.Append("</td>");



            sb.Append("</table>");

            sb.Append("</td>");
            sb.Append("</tr>");



            sb.Append("</table>");




            // sb.Append("<div class='page-break'>");

            sb.Append("<table border=1 width='100%' cellspacing='2' cellpadding='2' style=' font-size:10px; border-color:#dddddd;font-weight:normal'> ");
            for (int i = 0; i < dttravexIMg.Rows.Count; i++)
            {
                string Imh = dttravexIMg.Rows[i]["ImagePath"].ToString();
                string imageURLLogo1 = Server.MapPath(".") + "/Travel/" + Imh;
                if (System.IO.File.Exists(imageURLLogo1))
                {

                    sb.Append("<tr>");

                    sb.Append("<td valign='top'><img    height='600px' width='960px' src='" + imageURLLogo1 + "' alt='Bird' /></td>");
                    //sb.Append("<td width='14%' valign='top'>dfgfdg</td>");

                    sb.Append("</tr>");
                }
            }
            sb.Append("</table>");



            StringReader sr = new StringReader(sb.ToString());
            Document pdfDoc = new Document(PageSize.A2, 70f, 70f, 20f, 10f);
            // Document pdfDoc = new Document(PageSize.A4, 36, 36, 36, 72);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

            string FC = ddlFC.SelectedItem.Text;
            //var cssText = File.ReadAllText(MapPath("~/StyleSheet.css"));


            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);

                pdfDoc.Open();
                pdfDoc.NewPage();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();

                File.WriteAllBytes(Request.PhysicalApplicationPath + "/Travel vouchers/TravelVoucher_" + ddlMonth.SelectedItem.Text + "_" + Username.Substring(0, 7) + DateTime.Now.ToString("ddMMyyyy_hhmmss") + ".pdf", bytes);
            }


            string filename = "Travel vouchers" + "_" + ddlMonth.SelectedItem.Text + "_" + Username.Substring(0, 7) + DateTime.Now.ToString("ddMMyyyy_hhmmss") + ".pdf";
            //  string dsssssssssssss = Request.PhysicalApplicationPath + "Travel vouchers\\TravelVoucher_" + ddlMonth.SelectedItem.Text + "_ " + ddlFc.SelectedItem.Text + ".pdf";
            WebClient req = new WebClient();
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            string dsssssssssssss1 = Server.MapPath(Comman.GetImagePath("TravelvouchersPath") + "/TravelVoucher_" + ddlMonth.SelectedItem.Text + "_" + Username.Substring(0, 7) + DateTime.Now.ToString("ddMMyyyy_hhmmss") + ".pdf");
            byte[] data = req.DownloadData(dsssssssssssss1);
            response.BinaryWrite(data);
            //   Response.TransmitFile(Server.MapPath(Comman.GetImagePath("TravelvouchersPath") + "/"" + filename));
            response.End();




            //string filename ="TravelVoucher"+"_" +ddlFc.SelectedValue +".pdf";
            //FileInfo file = new FileInfo((Server.MapPath(Comman.GetImagePath("TravelvouchersPath") + "/"" + filename)));
            //if (file.Exists)
            //{

            //    Response.ContentType = "application/octet-stream";
            //    Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);
            //    string aaa = Server.MapPath(Comman.GetImagePath("TravelvouchersPath") + "/"" + filename);
            //    Response.TransmitFile(Server.MapPath(Comman.GetImagePath("TravelvouchersPath") + "/"" + filename));


            //}


            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('BTOR Not Available.')</script>", false);

            //}


        }
        catch (System.Exception ex)
        {

            //   Response.Clear();

            //string mmsg = ex.Message;
            //showEXPMessages("(crateZip)  " + mmsg); //showMessages(mmsg);
        }
        finally
        {

            //Response.Clear();

        }

        return sb.ToString();

    }

}