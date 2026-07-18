using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frm_UserRegistration : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Password objPass = new Password();
    Comman objComman = new Comman();
    public DataTable dtUserDeatils;
    string conditions = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {
            if (!IsPostBack)
            {
                fillleftgrid();
                //  showcontrols();
                b.Visible = false;
                c.Visible = false;
                d.Visible = false;

                ec.Visible = false;
                M1.Visible = false;
                M2.Visible = false;
                fillstate();
                fillrole();
                txtuname.Text = "";
                //fillemployee();
                ddlstate.Enabled = false;

                ddldistrict.Enabled = false;
                ddlemployee.Enabled = false;
                ddlblbock.Enabled = false;

                txtFristName.Enabled = false;

                rblExternal.Enabled = false;
                rblInternal.Enabled = false;
                txtuname.Enabled = true;

                txtuname.Enabled = false;
                txtpw.Enabled = false;
                txtcpassword.Enabled = false;
                ddllevel.Enabled = false;

            }
        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        int icount = objMain.DeleteUserActivity(txtuname.Text, 2);
        if (icount > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Delete Successfully')</script>", false);

        }


    }
    protected void OOD2Dtargetmet_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;
        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string values = (gvr.FindControl("lblCategory") as LinkButton).Text;


        //}


    }
    protected void btnlnk_Click(object sender, EventArgs e)
    {
        string Msg = "";
        Int32 iActivity = 0;
        if (lnkActivate.Text == "Active User")
        {
            iActivity = 2;
            Msg = "DeActivate";

        }
        else
        {
            iActivity = 1;
            Msg = "Activate";

        }



        Int32 Icoutn = 0;
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@level", txtuname.Text),
            new SqlParameter("@ActiveStatus ", iActivity),
           new SqlParameter("@ActivemodifyBy ", Session["username"].ToString())

        };
        Icoutn = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Sp__GetUseMaterDelete", cmdParameters);

        if (Icoutn > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('User " + Msg + " Successfully')</script>", false);

        }


    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        FillGridNew();
    }
    public void FillGridNew()
    {
        try
        {
            conditions = "where 1=1 ";
            string conditionsCLuster = "";
            if (Session["user_level"].ToString() == "1")
            {

            }

            else if (Session["user_level"].ToString() == "79")
            {
                if (Convert.ToString(Session["StateCode"]) == "8")
                {
                    conditions += " and Statecode in('" + Session["Statecode"].ToString() + "') ";
                }
                else
                {
                    conditions += " and DistrictCode in( sELECT distinct mst2District.DistrictCode  FROM MstusermultipleDist  inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where  UserName='" + Session["username"].ToString() + "'  and  Fyear='" + Session["FinYear"] + "' ) ";

                }

            }
            else if (Session["user_level"].ToString() == "94")
            {

            }
            else
            {
                conditions += " and DistrictCode='" + Session["DistrictCode"].ToString() + "' ";


            }
            if (ddlType.SelectedIndex > 0)
            {
                if (Convert.ToInt32(ddlType.SelectedValue) == 1)
                {
                    conditions = conditions + " and UserName like '" + txtSearchUser.Text + "%'";
                }
                if (Convert.ToInt32(ddlType.SelectedValue) == 2)
                {
                    conditions = conditions + " and FristName like '" + txtSearchUser.Text + "%'";
                }
            }


            DataTable dtuser = null;



            dtuser = Select_All_DataNew("mstuser", "*", conditions, "UserID", "ASC");




            if (dtuser.Rows.Count > 0)
            {
                dgvleftgrid.DataSource = dtuser;
                dgvleftgrid.DataBind();
                ViewState["Serach"] = dtuser;
            }
            else
            {
                dgvleftgrid.DataSource = null;
                dgvleftgrid.DataBind();
                ViewState["Serach"] = null;
            }
        }
        catch (Exception)
        {

            throw;
        }

    }


    protected void ddlstate_selectindexchnaged(object sender, EventArgs e)
    {
        filldistrict();

    }
    protected void lstState_selectindexchnaged(object sender, EventArgs e)
    {
        filldistrict();

    }

    protected void ddldistrict_selectindexchnaged(object sender, EventArgs e)
    {
        fillblock();
    }

    protected void ddlblock_selectindexchnaged(object sender, EventArgs e)
    {
        getvillagedata();
    }

    public void getvillagedata()
    {

        string vill = "";
        if (ddlstate.SelectedIndex > 0)
        {
            vill = "Statecode='" + ddlstate.SelectedValue.ToString() + "'";
        }

        if (ddldistrict.SelectedIndex > 0)
        {
            vill = "Statecode='" + ddlstate.SelectedValue.ToString() + "' and Districtcode='" + ddldistrict.SelectedValue + "'";
        }

        if (ddlblbock.SelectedIndex > 0)
        {
            vill = "Statecode='" + ddlstate.SelectedValue.ToString() + "' and Districtcode='" + ddldistrict.SelectedValue + "' and BlockCode='" + ddlblbock.SelectedValue.ToString() + "'";
        }
        DataTable dtvillage = Select_All_Data("mstCluster", "ClusterCode,ClusterName", vill, "ClusterName", "ASC");
        if (dtvillage.Rows.Count > 0)
        {
            ddlCluster.DataSource = dtvillage;
            ddlCluster.DataTextField = "ClusterName";
            ddlCluster.DataValueField = "ClusterCode";
            ddlCluster.DataBind();
            ddlCluster.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        else
        {
            ddlCluster.Items.Clear();
            ddlCluster.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }



    }
    //ddlemployee

    public void fillstate()
    {
        SqlParameter[] par1 = new SqlParameter[]
              {
                      new SqlParameter("@user_level_Role",  "1"),
                      new SqlParameter("@UserName", "" ),
                    new SqlParameter("@StateCode",  ""),
                       new SqlParameter("@Year", "2024"),
              };
        DataTable dtstate = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptLoadAllState", par1);
        // DataTable dtstate = Select_All_Data("mst1State", "*", "", "", "");
        if (dtstate.Rows.Count > 0)
        {
            ddlstate.DataSource = dtstate;
            ddlstate.DataTextField = "Statename";
            ddlstate.DataValueField = "Statecode";
            ddlstate.DataBind();
            ddlstate.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

            lstState.DataSource = dtstate;
            lstState.DataTextField = "Statename";
            lstState.DataValueField = "Statecode";
            lstState.DataBind();
        }
    }

    public void filldistrict()
    {
        try
        {


            string dist = "";

            if (ddlstate.SelectedIndex > 0)
            {



                dist = "Statecode='" + ddlstate.SelectedValue.ToString() + "' and FYear ='" + Session["FinYear"] + "'";

            }



            if (lstState.Visible == true)
            {
                string Statecode = "";
                foreach (ListItem item in lstState.Items)
                {
                    if (item.Selected)
                    {

                        Statecode += "'" + item.Value + "'" + ",";


                    }
                }
                //if (Session["user_level"].ToString() == "1")
                //{
                //    if (Statecode.Length > 0)
                //    {
                //        Statecode = Statecode.Substring(0, Statecode.LastIndexOf(","));

                //        dist = "Statecode in(" + Statecode + ") and FYear in('2017-2018','2018-2019')";
                //    }
                //}
                //else
                //{

                if (Statecode.Length > 0)
                {
                    Statecode = Statecode.Substring(0, Statecode.LastIndexOf(","));

                    dist = "Statecode in(" + Statecode + ") and FYear ='" + Session["FinYear"] + "'";
                }
                //}
            }
            DataTable dtdist = null;



            //if (Session["user_level"].ToString() == "1")
            //{
            //    dtdist = Select_All_Data("mst2District", " DistrictName +' ('+ Fyear +')'  as   DistrictName ,DistrictCode", dist, "DistrictName", "ASC");
            //}
            //else
            //{

            dtdist = Select_All_Data("mst2District", "DistrictCode,DistrictName,Statecode", dist, "DistrictName", "ASC");
            //}
            if (dtdist.Rows.Count > 0)
            {
                ddldistrict.DataSource = dtdist;
                ddldistrict.DataTextField = "DistrictName";
                ddldistrict.DataValueField = "DistrictCode";
                ddldistrict.DataBind();
                ddldistrict.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

                ddlBaseDist.DataSource = dtdist;
                ddlBaseDist.DataTextField = "DistrictName";
                ddlBaseDist.DataValueField = "DistrictCode";
                ddlBaseDist.DataBind();
                ddlBaseDist.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));

                lstDistrict.DataSource = dtdist;
                lstDistrict.DataTextField = "DistrictName";
                lstDistrict.DataValueField = "DistrictCode";
                lstDistrict.DataBind();

                ViewState["Dist"] = dtdist;

            }
        }
        catch (Exception)
        {
            //throw;
            throw;
        }

    }

    public void fillblock()
    {
        string block = "";
        if (ddlstate.SelectedIndex > 0)
        {
            block = "Statecode='" + ddlstate.SelectedValue.ToString() + "' and  DividedBlock=1";
        }

        if (ddldistrict.SelectedIndex > 0)
        {
            block = "Statecode='" + ddlstate.SelectedValue.ToString() + "' and  DividedBlock=1 and Districtcode='" + ddldistrict.SelectedValue + "'";
        }
        DataTable dtblock = Select_All_Data("mst3Block", "distinct BlockCode,Blockname", block, "Blockname", "ASC");
        if (dtblock.Rows.Count > 0)
        {
            ddlblbock.DataSource = dtblock;
            ddlblbock.DataTextField = "BlockName";
            ddlblbock.DataValueField = "BlockCode";
            ddlblbock.DataBind();
            ddlblbock.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
    }

    public void fillrole()
    {

        string cond = "";




        if (Session["user_level"].ToString() == "1")
        {


        }
        else
        {
            cond = "Role_Level not in(1)";
        }






        DataTable dtrole = Select_All_Data("mstuserrole", "*", cond, "Role_id", "");
        if (dtrole.Rows.Count > 0)
        {
            ddllevel.DataSource = dtrole;
            ddllevel.DataTextField = "Role";
            ddllevel.DataValueField = "Role_Level";
            ddllevel.DataBind();
            ddllevel.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }



    }
    public DataTable Select_All_DataNew(string TableName, string TFieldName, string Condition, string OrderbyCondition, string Sortcondition)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            string WConditions = Condition.Length > 0 ? "  " + Condition : "";
            string OrderbyvalueMem = OrderbyCondition.Length > 0 ? " order by " + OrderbyCondition + "  " : "";
            string sortbycondi = Sortcondition.Length > 0 ? "" + Sortcondition : "";
            string FieldName = TFieldName.Length > 0 ? TFieldName : "";
            SqlParameter[] paramv = new SqlParameter[]
                    {
                            new SqlParameter("@TableName",TableName),
                            new SqlParameter("@Condition",WConditions),
                            new SqlParameter("@OrderbyvalueMem",OrderbyvalueMem),
                            new SqlParameter("@sortbycondi",sortbycondi),
                            new SqlParameter("@FieldName",FieldName),

                    };

            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Select_AllTableData", paramv);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch (Exception)
        {
            //string mmsg = ex.Message; showMessages(mmsg);
            //showMessages("(SelectAllData)  " + mmsg);
            throw;
        }
        return dtcombo;
    }

    public DataTable Select_All_Data(string TableName, string TFieldName, string Condition, string OrderbyCondition, string Sortcondition)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            string WConditions = Condition.Length > 0 ? " where " + Condition : "";
            string OrderbyvalueMem = OrderbyCondition.Length > 0 ? " order by " + OrderbyCondition + "  " : "";
            string sortbycondi = Sortcondition.Length > 0 ? "" + Sortcondition : "";
            string FieldName = TFieldName.Length > 0 ? TFieldName : "";
            SqlParameter[] paramv = new SqlParameter[]
                    {
                            new SqlParameter("@TableName",TableName),
                            new SqlParameter("@Condition",WConditions),
                            new SqlParameter("@OrderbyvalueMem",OrderbyvalueMem),
                            new SqlParameter("@sortbycondi",sortbycondi),
                            new SqlParameter("@FieldName",FieldName),

                    };

            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Select_AllTableData", paramv);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch (Exception)
        {
            //string mmsg = ex.Message; showMessages(mmsg);
            //showMessages("(SelectAllData)  " + mmsg);
            throw;
        }
        return dtcombo;
    }


    public void showcontrols()
    {

        if (ddllevel.SelectedIndex > 0)

        {
            string Cond2 = "Role_Level ='" + ddllevel.SelectedValue + "'";
            DataTable dtstate = Select_All_Data("Mstuserrole", "RID", Cond2, "RID", "");
            Int32 RoleLevel = Convert.ToInt32(dtstate.Rows[0]["RID"].ToString());
            M1.Visible = false;
            M2.Visible = false;
            divBas.Visible = false;
            if (RoleLevel == 1)
            {

                b.Visible = false;
                c.Visible = false;
                d.Visible = false;
                ec.Visible = false;


            }
            if (RoleLevel == 2)
            {

                b.Visible = true;
                c.Visible = false;
                d.Visible = false;
                ec.Visible = false;


            }
            if (RoleLevel == 3)
            {

                b.Visible = true;
                c.Visible = true;
                d.Visible = false;
                ec.Visible = false;


            }

            if (RoleLevel == 4)
            {

                b.Visible = true;
                c.Visible = true;
                d.Visible = true;
                ec.Visible = false;


            }
            if (RoleLevel == 5)
            {

                b.Visible = true;
                c.Visible = true;
                d.Visible = true;
                ec.Visible = true;


            }


            DataTable dtRoleLevel = Select_All_Data("Mstusermultiplerole", "RID", Cond2, "RID", "");
            if (dtRoleLevel.Rows.Count > 0)
            {
                Int32 RoleLevelNee = Convert.ToInt32(dtRoleLevel.Rows[0]["RID"].ToString());

                b.Visible = false;
                c.Visible = false;
                d.Visible = false;
                ec.Visible = false;
                divBas.Visible = false;
                if (RoleLevelNee == 1)
                {

                    M1.Visible = true;



                }
                if (RoleLevelNee == 2)
                {

                    M1.Visible = true;
                    M2.Visible = true;
                    divBas.Visible = true;
                    foreach (ListItem item in lstState.Items)
                    {
                        item.Selected = false;

                    }
                    lstDistrict.Items.Clear();
                }
                if (RoleLevelNee == 3)
                {


                }

                if (RoleLevelNee == 4)
                {



                }
                if (RoleLevel == 5)
                {




                }

            }

            //if (ddllevel.SelectedValue == "19")
            //{


            //    b.Visible = true;
            //    c.Visible = true;
            //    d.Visible = true;
            //    ec.Visible = false;

            //}
            //else if (ddllevel.SelectedValue == "39" || ddllevel.SelectedValue == "25"  || ddllevel.SelectedValue == "61" || ddllevel.SelectedValue == "60" || ddllevel.SelectedValue == "30" || ddllevel.SelectedValue == "29")
            //{
            //    //b.Visible = true;
            //    //c.Visible = false;
            //    //d.Visible = false;
            //    //ec.Visible = false;

            //    b.Visible = true;
            //    c.Visible = true;
            //    d.Visible = false;
            //    ec.Visible = false;


            //}
            //else if (ddllevel.SelectedValue == "59")
            //{

            //    b.Visible = true;
            //    c.Visible = false;
            //    d.Visible = false;
            //    ec.Visible = false;
            //}

            //if (ddllevel.SelectedValue == "24")
            //{


            //    b.Visible = true;
            //    c.Visible = true;
            //    d.Visible = true;
            //    e.Visible = true;

            //}
        }
    }
    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlemployee.SelectedIndex > 0)
        {
            string Cond = " LTRIM(EmployeeID) ='" + ddlemployee.SelectedValue + "'";
            DataTable dtEmp = Select_All_Data("tblemployeedetails", "EmployeeID,firstname , lastname", Cond, "firstname", "");
            if (dtEmp.Rows.Count > 0)
            {
                SqlParameter[] pa = new SqlParameter[]
                 {
                 new SqlParameter("@UserName", dtEmp.Rows[0]["EmployeeID"].ToString() ),

                 };
                DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "checkUserAvailability", pa);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('User Already Exist!.')</script>", false);
                    txtFristName.Text = "";

                    txtuname.Text = "";
                    ddlemployee.SelectedIndex = 0;
                }

                else
                {
                    txtFristName.Text = dtEmp.Rows[0]["firstname"].ToString();

                    txtuname.Text = dtEmp.Rows[0]["EmployeeID"].ToString();
                }
            }
            else
            {
                txtFristName.Text = "";

                txtuname.Text = "";
            }
        }
        else
        {
            txtFristName.Text = "";

            txtuname.Text = "";
        }
    }


    protected void ddllevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        showcontrols();
        fillemployee();

        //if (Convert.ToInt32(ddllevel.SelectedValue) == 24)
        //{
        //    IM.Visible = true;
        //}
        //else
        //{
        //    IM.Visible = false;
        //}
    }
    public void fillemployee()
    {


        string Cond = "EmployeeType ='" + ddllevel.SelectedValue + "' and UserReg=1 ";
        DataTable dtstate = Select_All_Data("tblemployeedetails", " LTRIM(EmployeeID) as EmployeeID,firstname + ' (' + EmployeeID +')' as [Name]", Cond, "firstname", "");
        if (dtstate.Rows.Count > 0)
        {
            ddlemployee.DataSource = dtstate;
            ddlemployee.DataTextField = "Name";
            ddlemployee.DataValueField = "EmployeeID";
            ddlemployee.DataBind();
            ddlemployee.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
    }

    public void fillemployeeNew()
    {


        string Cond = "EmployeeType ='" + ddllevel.SelectedValue + "' and UserReg=2 ";
        DataTable dtstate = Select_All_Data("tblemployeedetails", "  LTRIM(EmployeeID) as EmployeeID,firstname + ' (' + EmployeeID +')' as [Name]", Cond, "firstname", "");
        if (dtstate.Rows.Count > 0)
        {
            ddlemployee.DataSource = dtstate;
            ddlemployee.DataTextField = "Name";
            ddlemployee.DataValueField = "EmployeeID";
            ddlemployee.DataBind();
            ddlemployee.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        Session["dtstate"] = dtstate;
    }
    public void fillleftgrid()
    {
        string cond = "";

        DataTable dtuser = null;
        if (Session["user_level"].ToString() == "1")
        {
            dtuser = Select_All_Data("mstuser", "top 50 *", "", "UserID", "desc");
        }

        else if (Session["user_level"].ToString() == "79")
        {
            if (Convert.ToString(Session["StateCode"]) == "8")
            {
                cond = " Statecode in('" + Session["Statecode"].ToString() + "') ";
            }
            else
            {
                cond = " DistrictCode in(" + Session["DistrictCode"].ToString() + ") ";
            }
            dtuser = Select_All_Data("mstuser", "top 100*", cond, "UserID", "desc");
        }
        else if (Session["user_level"].ToString() == "94")
        {
            dtuser = Select_All_Data("mstuser", "top 50 *", "", "UserID", "desc");
        }
        else
        {

            cond = " DistrictCode='" + Session["DistrictCode"].ToString() + "' ";

            dtuser = Select_All_Data("mstuser", "top 50 *", "", "UserID", "desc");

        }


        if (dtuser.Rows.Count > 0)
        {
            dgvleftgrid.DataSource = dtuser;
            dgvleftgrid.DataBind();
            ViewState["Serach"] = dtuser;
        }
        else
        {
            dgvleftgrid.DataSource = null;
            dgvleftgrid.DataBind();
            ViewState["Serach"] = null;
        }
    }

    protected void dgvleftgrid_rowcommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            if (e.CommandName == "Show")
            {
                ViewState["Flag"] = "U";
                int iIndex = Convert.ToInt32(e.CommandArgument);
                int id = Convert.ToInt32(dgvleftgrid.DataKeys[iIndex]["UserID"].ToString());
                ViewState["id"] = id;

                int ActiveStatus = Convert.ToInt32(dgvleftgrid.DataKeys[iIndex]["ActiveStatus"].ToString());
                if (ActiveStatus == 2)
                {
                    btnSave.Enabled = false;
                }
                else
                {
                    btnSave.Enabled = true;
                }

                ViewState["Save"] = "Update";
                fillcontrols(id);
                // showcontrols();
                ddlstate.Enabled = true;
                rblExternal.Enabled = false;
                rblInternal.Enabled = false;
                ddldistrict.Enabled = true;

                ddlblbock.Enabled = true;
                //txtFristName.Enabled = true;
                //txtLastName.Enabled = true;
                //rblExternal.Enabled = true;
                //rblInternal.Enabled = true;
                //txtuname.Enabled = true;
                txtpw.Enabled = true;
                txtcpassword.Enabled = true;
                ddllevel.Enabled = true;
                //hdnGranteeStatus.Value = "Update";

            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void fillcontrols(int UserID)
    {
        string condition = "";
        condition = "UserID=" + UserID + "";
        DataTable dt = Select_All_Data("mstuser", "*,case  when ActiveStatus=2 then  isnull(DATEDIFF(month, ActivemodifyDate,getdate()),0) else 0 end as fDay", condition, "", "");


        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["UserType"].ToString() == "2")
            {
                demp.Visible = false;

                rblExternal.Checked = true;
                rblInternal.Checked = false;
            }
            else
            {
                rblExternal.Checked = false;
                rblInternal.Checked = true;

                demp.Visible = true;
            }
            if (dt.Rows[0]["UserLevel"].ToString() != "")
            {
                ddllevel.SelectedValue = dt.Rows[0]["UserLevel"].ToString();
                ddllevel_SelectedIndexChanged(ddllevel, null);
            }

            else
            {
                ddllevel.SelectedIndex = -1;
            }
            txtuname.Text = dt.Rows[0]["UserName"].ToString();
            txtFristName.Text = dt.Rows[0]["FristName"].ToString();

            fillemployeeNew();
            string condition1 = "UserName='" + dt.Rows[0]["UserName"].ToString().Trim() + " '";
            DataTable dtMultipul = Select_All_Data("MstusermultipleDist inner join mst2District on mst2District.DistrictCode=[MstusermultipleDist].DistrictCode ", "mst2District.DistrictCode,mst2District.StateCode ", condition1, "", "");


            txtImi.Text = dt.Rows[0]["IMEINo"].ToString();
            txtAndroidID.Text = dt.Rows[0]["AndroidID"].ToString();

            if (dt.Rows[0]["ActiveStatus"].ToString() == "2")
            {
                lnkActivate.Visible = true;
                lnkActivate.Text = "DeActive User";
            }
            else
            {

                lnkActivate.Visible = true;
                lnkActivate.Text = "Active User";
            }

            if (Convert.ToString(Session["username"]) == "PMSAdmin" || Convert.ToString(Session["username"]) == "SuperAdmin" || Convert.ToString(Session["username"]) == "EGE7557")
            {
            }
            else
            {
                if (Convert.ToInt32(dt.Rows[0]["fDay"]) > 3)
                {
                    lnkActivate.Visible = false;
                }
            }
            //if (Convert.ToInt32(ddllevel.SelectedValue) == 24)
            //{
            //    IM.Visible = true;
            //}
            //else
            //{
            //    IM.Visible = false;
            //}
            if (dt.Rows[0]["UserOnline"].ToString() == "True")
            {
                chkOnline.Checked = true;
            }
            else
            {
                chkOnline.Checked = false;
            }
            if (dt.Rows[0]["UserOffline"].ToString() == "True")
            {
                chkOffline.Checked = true;
            }
            else
            {
                chkOffline.Checked = false;
            }
            if (dt.Rows[0]["UserType"].ToString() == "2")
            {
            }
            else
            {
                if (dt.Rows[0]["UserName"].ToString() != "")
                {
                    DataTable dt55 = Session["dtstate"] as DataTable;
                    DataRow[] dr = dt55.Select("EmployeeID='" + dt.Rows[0]["UserName"].ToString() + "'");
                    if (dr.Length > 0)
                    {
                        ddlemployee.SelectedValue = dt.Rows[0]["UserName"].ToString();
                    }
                }

                else
                {
                    ddlemployee.SelectedIndex = -1;
                }
            }
            if (dt.Rows[0]["StateCode"].ToString() != "")
            {
                ddlstate.SelectedValue = dt.Rows[0]["StateCode"].ToString();
            }

            else
            {
                ddlstate.SelectedIndex = -1;
            }


            filldistrict();
            if (dt.Rows[0]["Districtcode"].ToString() != "")
            {
                if (dtMultipul.Rows.Count > 0)
                {
                    ddldistrict.SelectedIndex = -1;
                }
                else
                {
                    ddldistrict.SelectedValue = dt.Rows[0]["Districtcode"].ToString();
                }
            }

            else
            {
                ddldistrict.SelectedIndex = -1;
            }


            fillblock();
            if (dtMultipul.Rows.Count > 0)
            {
                for (int r = 0; r < dtMultipul.Rows.Count; r++)
                {

                    foreach (ListItem item in lstState.Items)
                    {
                        if (item.Value.ToString() == dtMultipul.Rows[r]["StateCode"].ToString())
                        {

                            item.Selected = true;


                        }
                    }
                }
                lstState_selectindexchnaged(lstState, null);

                for (int r = 0; r < dtMultipul.Rows.Count; r++)
                {

                    foreach (ListItem item in lstDistrict.Items)
                    {
                        if (item.Value.ToString() == dtMultipul.Rows[r]["DistrictCode"].ToString())
                        {

                            item.Selected = true;


                        }
                    }
                }
                if (dt.Rows[0]["BaseDist"].ToString() != "")
                {
                    ddlBaseDist.SelectedValue = dt.Rows[0]["BaseDist"].ToString();
                }

                else
                {
                    ddlBaseDist.SelectedIndex = -1;
                }
            }
            if (dt.Rows[0]["BlockCode"].ToString() != "")
            {
                ddlblbock.SelectedValue = dt.Rows[0]["BlockCode"].ToString();
                getvillagedata();
            }

            else
            {
                ddlblbock.SelectedIndex = -1;
            }
            if ((Convert.ToString(dt.Rows[0]["VillageCode"]) == "") || (dt.Rows[0]["VillageCode"].ToString() == null))
            {
                ddlCluster.SelectedIndex = -1;
            }
            else
            {
                ddlCluster.SelectedValue = dt.Rows[0]["VillageCode"].ToString();

            }

            //if ((dt.Rows[0]["VillageCode"].ToString() != "") || (dt.Rows[0]["VillageCode"].ToString() != null))
            //{
            //    //getvillagedata();
            //    //string vilage = Convert.ToString(dt.Rows[0]["VillageCode"].ToString());

            //    //  vilage = vilage.Replace("'", "");

            //    //string[] str = vilage.Split(',');

            //    //for (int i = 0; i <= chkvillage.Items.Count - 1; i++)
            //    //{
            //    //    for (int j = 0; j <= str.Length-1 ; j++)
            //    //    {
            //    //        if (chkvillage.Items[i].Value == str[j].ToString().Trim())
            //    //        {
            //    //            chkvillage.Items[i].Selected = true;
            //    //            break;
            //    //        }
            //    //        else
            //    //        {
            //    //            chkvillage.Items[i].Selected = false;
            //    //        }
            //    //    }
            //    //}
            //}

            txtuname.Text = dt.Rows[0]["UserName"].ToString();
            txtpw.Text = dt.Rows[0]["Password"].ToString();
        }

    }
    protected void rblInternal_CheckedChanged(object sender, EventArgs e)
    {
        demp.Visible = true;
        txtFristName.Enabled = false;

        ddllevel.SelectedIndex = -1;
        ddldistrict.SelectedIndex = -1;
        ddlblbock.SelectedIndex = -1;
        ddlemployee.SelectedIndex = -1;
        txtFristName.Text = "";

        txtuname.Text = "";
    }
    protected void rblExternal_CheckedChanged(object sender, EventArgs e)
    {
        demp.Visible = false;
        txtFristName.Enabled = true;

        ddllevel.SelectedIndex = -1;
        ddldistrict.SelectedIndex = -1;
        ddlblbock.SelectedIndex = -1;
        ddlemployee.SelectedIndex = -1;
        txtFristName.Text = "";
        txtuname.Text = "Auto generated number";

    }
    protected void btn_Add_click(object sender, EventArgs e)
    {
        lnkActivate.Visible = false;
        txtFristName.Enabled = false;
        chkOnline.Checked = false;
        chkOffline.Checked = false;
        btnSave.Enabled = true;
        ddllevel.SelectedIndex = -1;
        ddldistrict.SelectedIndex = -1;
        ddlblbock.SelectedIndex = -1;
        // chkvillage.SelectedIndex = -1;
        b.Visible = false;
        c.Visible = false;
        d.Visible = false;

        ec.Visible = false;
        M1.Visible = false;
        M2.Visible = false;
        divBas.Visible = false;
        ViewState["Save"] = "Save";
        demp.Visible = true;
        txtuname.Text = "";
        txtpw.Text = "";
        txtcpassword.Text = "";
        ddlemployee.SelectedIndex = -1;
        ddlstate.SelectedIndex = -1;
        ViewState["Flag"] = "I";
        ViewState["id"] = DBNull.Value;
        ddlstate.Enabled = true;

        txtFristName.Text = "";
        txtImi.Text = "";
        txtAndroidID.Text = "";
        rblInternal.Checked = true;
        rblInternal.Enabled = true;
        rblExternal.Enabled = true;
        ddldistrict.Enabled = true;
        ddlemployee.Enabled = true;
        ddlblbock.Enabled = true;

        // chkvillage.Enabled = true;
        txtuname.Enabled = false;
        txtpw.Enabled = true;
        txtcpassword.Enabled = true;
        ddllevel.Enabled = true;

        if (rblExternal.Checked == true)
        {
            demp.Visible = false;
            txtFristName.Enabled = true;
            rblInternal.Checked = false;
            ddllevel.SelectedIndex = -1;
            ddldistrict.SelectedIndex = -1;
            ddlblbock.SelectedIndex = -1;
            ddlemployee.SelectedIndex = -1;
            txtFristName.Text = "";
            txtuname.Text = "Auto generated number";
        }

    }

    private string GetCheckBoxListSelection(CheckBoxList chbx)
    {

        ArrayList cblSelections = new ArrayList();
        string a = "";

        foreach (ListItem item in chbx.Items)
        {
            if (item.Selected)
            {
                cblSelections.Add(item.Value);
                a += "'" + item.Value + "'" + ",";
            }
        }
        return a;

    }

    public static byte[] HashPassword(string password)
    {
        var provider = new SHA1CryptoServiceProvider();
        var encoding = new UnicodeEncoding();
        return provider.ComputeHash(encoding.GetBytes(password));
    }

    public void CreateDataTableUserDetails()
    {

        dtUserDeatils = new DataTable();

        dtUserDeatils.Columns.Add(new DataColumn("UserName", System.Type.GetType("System.String")));
        dtUserDeatils.Columns.Add(new DataColumn("DistrictCode", System.Type.GetType("System.String")));


    }
    public bool InterventionSql_Injection(string RVal)
    {
        SqlInjection objAudit = new SqlInjection();
        bool injection = false;


        injection = objAudit.CheckInputBool(RVal);

        return injection;

    }
    public static List<Control> GetAllControls(List<Control> controls, Type t, Control parent /* can be Page */)
    {
        foreach (Control c in parent.Controls)
        {
            if (c.GetType() == t)
                controls.Add(c);
            if (c.HasControls())
                controls = GetAllControls(controls, t, c);
        }
        return controls;
    }
    public string SetTextBoxFocusSelect(Page page)
    {
        string ALlTestBoxValue = "";
        List<Control> list = new List<Control>();
        list = GetAllControls(list, typeof(TextBox), page);
        foreach (Control ctl in list)
        {
            if (ctl.GetType() == typeof(TextBox))
            {
                ((TextBox)ctl).Attributes.Add("onfocus", "this.select()");
                string TempVari = ((TextBox)ctl).Text;
                if (TempVari.Length > 0)
                {
                    ALlTestBoxValue += TempVari + "  ";
                }
            }
        }
        return ALlTestBoxValue;
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        string RVal = SetTextBoxFocusSelect(this.Page);
        if (!InterventionSql_Injection(RVal))
        {
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Spurious input detected. Data rejected')</script>", false);

            return;
        }
        if (ViewState["Save"].ToString() == "Save")
        {
            if (txtpw.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Password')</script>", false);
                return;

            }
        }
        if (rblExternal.Checked == false)
        {
            if (txtuname.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter UserName')</script>", false);
                return;

            }
        }

        if (chkOnline.Checked == false && chkOffline.Checked == false)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Offline or Online')</script>", false);
            return;


        }
        if (lstDistrict.Visible == true)
        {
            Boolean sSate = false;
            Boolean sDist = false;
            foreach (ListItem item in lstState.Items)
            {
                if (item.Selected)
                {
                    sSate = true;

                }
            }



            foreach (ListItem item in lstDistrict.Items)
            {
                if (item.Selected)
                {

                    sDist = true;
                }
            }
            if (sSate == false)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select State')</script>", false);
                return;


            }
            if (sDist == false)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select District')</script>", false);
                return;


            }
        }
        string userlevel = "", FristName = "", LastName = "", statecode = "", districtcode = "", blockcode = "", villagecode = "", uname = "", pw = "", cpw = "", staffid = "";

        if (lstDistrict.Visible == true)
        {
            CreateDataTableUserDetails();




            foreach (ListItem item in lstDistrict.Items)
            {
                if (item.Selected)
                {
                    DataRow Item1;
                    Item1 = dtUserDeatils.NewRow();
                    dtUserDeatils.Rows.Add(Item1);
                    Item1["UserName"] = txtuname.Text.Trim();

                    Item1["DistrictCode"] = item.Value;

                }
            }

            DataTable dt = ViewState["Dist"] as DataTable;
            DataRow[] dr = dt.Select("DistrictCode='" + dtUserDeatils.Rows[0]["districtcode"].ToString() + "'");
            if (dr.Length > 0)
            {
                //statecode = dr[0]["statecode"].ToString();
                districtcode = dr[0]["DistrictCode"].ToString();
            }

        }
        Int32 UserType = 1;
        Int32 SerialNo = 0;
        if (ddllevel.SelectedIndex > 0)
        {
            userlevel = ddllevel.SelectedValue.ToString();
        }
        if (txtuname.Text == "TestAdmin")
        {

        }
        else
        {
            if (ddlstate.SelectedIndex > 0)
            {
                statecode = ddlstate.SelectedValue.ToString();
            }
        }
        if (ddldistrict.SelectedIndex > 0)
        {
            districtcode = ddldistrict.SelectedValue.ToString();
        }
        if (ddlblbock.SelectedIndex > 0)
        {
            blockcode = ddlblbock.SelectedValue.ToString();
        }
        if (ddlCluster.SelectedIndex > 0)
        {
            villagecode = ddlCluster.SelectedValue.ToString();
        }
        if (ddlemployee.SelectedIndex > 0)
        {
            staffid = ddlemployee.SelectedValue.ToString();
        }
        if ((txtuname.Text != null) || (txtuname.Text != ""))
        {
            uname = txtuname.Text.Trim();
        }
        if ((txtpw.Text != null) || (txtpw.Text != ""))
        {
            pw = txtpw.Text;

        }
        if ((txtcpassword.Text != null) || (txtcpassword.Text != ""))
        {
            cpw = txtcpassword.Text;
        }

        //if (ddlBaseDist.SelectedIndex > 0)
        //{

        //    districtcode = ddlBaseDist.SelectedValue.ToString();
        //}

        if (rblExternal.Checked == true)
        {
            if (ViewState["Save"].ToString() == "Save")
            {
                Unique();
                txtuname.Text = ViewState["UCode"].ToString();
                uname = txtuname.Text;
                UserType = 2;
                SerialNo = Convert.ToInt32(ViewState["NumNo"].ToString());
            }
            staffid = txtuname.Text;
        }

        if ((txtFristName.Text != null) || (txtFristName.Text != ""))
        {
            FristName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtFristName.Text);

        }



        bool checkpsw = false;
        string msgg = "";

        msgg = Password.checkpasswordCharacters(txtpw.Text.Trim());
        if (txtpw.Text.Trim() != "")
        {
            checkpsw = Password.CheckPasswordAgainstPolicy(uname, pw);
        }
        if (checkpsw)
        {
            if (txtpw.Text.Trim() != "")
            {
                pw = Password.CreatePasswordHash(txtpw.Text);
            }
        }
        if (ViewState["Save"].ToString() == "Save")
        {

        }


        //if (chkvillage.SelectedIndex != -1)
        //{
        //    villagecode = GetCheckBoxListSelection(chkvillage);
        //}


        SqlParameter[] parm = new SqlParameter[]
    {


            new SqlParameter("@userlevel", userlevel),
            new SqlParameter("@statecode", statecode),
            new SqlParameter("@district", districtcode),
            new SqlParameter("@block", blockcode),
            new SqlParameter("@village", villagecode),
            new SqlParameter("@uname", uname),
            new SqlParameter("@pw", pw),
            new SqlParameter("@staffid", staffid),
             new SqlParameter("@flag", ViewState["Flag"].ToString()),
             new SqlParameter("@uid",ViewState["id"].ToString()),
              new SqlParameter("@UserType", UserType),
               new SqlParameter("@SerialNo", SerialNo),
                new SqlParameter("@fristName",FristName ),
                 new SqlParameter("@LastName", LastName),
                   new SqlParameter("@UserOnline", chkOnline.Checked),
                     new SqlParameter("@UserOffline", chkOffline.Checked),
                         new SqlParameter("@IMEINo", txtImi.Text),
                           new SqlParameter("@CreateBy",  Session["username"].ToString()),
                             new SqlParameter("@BaseDist",  ddlBaseDist.SelectedValue),
                                new SqlParameter("@AndroidID", txtAndroidID.Text.Trim()),

      };
        int result = Convert.ToInt32(SqlHelper.ExecuteScaler(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "sp_insert_update_usermaster", parm));
        if (ViewState["id"].ToString() == null || ViewState["id"].ToString() == "")
        {
            ViewState["id"] = result.ToString();
        }
        if (dtUserDeatils != null)
        {
            if (dtUserDeatils.Rows.Count > 0)
            {
                SqlParameter[] parm1 = new SqlParameter[]
                 {


                            new SqlParameter("@UserName",  dtUserDeatils.Rows[0]["UserName"].ToString()),
                            new SqlParameter("@DistrictCode",  dtUserDeatils.Rows[0]["DistrictCode"].ToString()),
                              new SqlParameter("@Flag", 1),

                   };
                int result1 = Convert.ToInt32(SqlHelper.ExecuteScaler(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUsermultipleDist", parm1));
                for (int r = 0; r < dtUserDeatils.Rows.Count; r++)
                {
                    SqlParameter[] parm2 = new SqlParameter[]
                         {


                                    new SqlParameter("@UserName", dtUserDeatils.Rows[r]["UserName"].ToString()),
                                    new SqlParameter("@DistrictCode", dtUserDeatils.Rows[r]["DistrictCode"].ToString()),
                                      new SqlParameter("@Flag", 2),

                           };
                    int result4 = Convert.ToInt32(SqlHelper.ExecuteScaler(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUsermultipleDist", parm2));

                }


            }
        }
        //if (result > 0)
        //{
        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully')</script>", false);

        fillleftgrid();
        //txtpw.Text = "";
        //txtuname.Text = "";
        //ddlblbock.SelectedIndex = -1;
        //ddlstate.SelectedIndex = -1;

        //}
        if (ViewState["Save"].ToString() == "Save")
        {
            ViewState["Save"] = "Update";
        }


    }
    protected void Txtuser_TextChanged(object sender, EventArgs e)
    {
        UniquUserName(txtuname.Text);
    }
    private void UniquUserName(string username)
    {
        try
        {
            SqlParameter[] pa = new SqlParameter[]
     {
     new SqlParameter("@UserName", username ),

     };
            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "checkUserAvailability", pa);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('User Already Exist!.')</script>", false);
                txtuname.Text = "";
            }

        }
        catch (Exception)
        {
            throw;
        }


    }
    public void Unique()
    {

        Int32 mNewNo = 0;
        string strAlias;
        string strQry = " Select isnull(max(SerialNo),0) as Serial from MstUser where  UserType=2 ";
        //string strQry = " Select top 1 Serial from tblDTD   order by Serial desc ";
        DataTable dt = objMain.LoadData(strQry);

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["Serial"].ToString() == "" || dt.Rows[0]["Serial"].ToString() == "-1")
            {
                mNewNo += 1;
                strAlias = mNewNo.ToString().PadLeft(4, '0');
                ViewState["UCode"] = "EGEX" + "" + strAlias;
                ViewState["NumNo"] = strAlias;
            }
            else
            {
                mNewNo = Convert.ToInt32(dt.Rows[0]["Serial"].ToString());
                mNewNo += 1;
                strAlias = mNewNo.ToString().PadLeft(4, '0');

                ViewState["NumNo"] = strAlias;
                ViewState["UCode"] = "EGEX" + "" + strAlias;

            }

        }
        else
        {
            mNewNo += 1;
            strAlias = mNewNo.ToString().PadLeft(4, '0');
            ViewState["UCode"] = "EGEX" + "" + strAlias;
            ViewState["NumNo"] = strAlias;
        }


    }

    protected void GV_Project_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgvleftgrid.PageIndex = e.NewPageIndex;
        if (ViewState["Serach"] != null)
        {
            DataTable dt = ViewState["Serach"] as DataTable;
            dgvleftgrid.DataSource = dt;
            dgvleftgrid.DataBind();
        }

    }
}