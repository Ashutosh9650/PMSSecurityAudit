using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class frmMobileVillageEntry : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    string conditions = "";
    Comman objComman = new Comman();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            d2dContact.Visible = false;
            LoadData();
            LoadOtherCon();
            ModalPopupExtender.Hide();
            this.ModalPopupExtender1.Hide();
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            TxtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");



            if (Request.QueryString["ID"] != null)
            {

                //if (Session["user_level"].ToString() == "19")
                //{
                //    string Strhh = Convert.ToString(Session["BlockCodeAct"]);
                //    DataTable dt = objMain.GetActivityUserWiseMaxDateNew(ddlUser.SelectedValue, Strhh);
                //    if (dt.Rows.Count > 0)
                //    {
                //        if (Convert.ToString(dt.Rows[0]["ActivityDate"].ToString()) != "")
                //        {
                //            CalendarExtenderTourdate.StartDate = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString()).AddDays(1);
                //        }
                //    }

                //}
                //if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
                //{
                //    string Strhh =Convert.ToString(Session["BlockCodeAct"]);
                //    DataTable dt = objMain.GetActivityUserWiseMaxDateNewIO(ddlUser.SelectedValue, Strhh);
                //    if (dt.Rows.Count > 0)
                //    {
                //        if (Convert.ToString(dt.Rows[0]["ActivityDate"].ToString()) != "")
                //        {
                //            CalendarExtenderTourdate.StartDate = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString()).AddDays(1);
                //        }
                //    }
                //}


                if (Session["user_level"].ToString() == "19")
                {
                    DataTable dt = objMain.GetActivityUpdateDateWiseBlockWiseNew(Convert.ToString(Session["BlockCodeAct"]), "2", "FC");
                    if (dt.Rows.Count > 0)
                    {
                    }
                    else
                    {

                        dt = objMain.GetActivityUserWiseMaxDateNew(ddlUser.SelectedValue, Convert.ToString(Session["BlockCodeAct"]));
                    }
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToString(dt.Rows[0]["ActivityDate"].ToString()) != "")
                        {

                            DateTime Activitydate1 = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString());
                            if (Activitydate1.Day == 1 && Activitydate1.Month == 4)
                            {
                                CalendarExtenderTourdate.StartDate = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString()).AddDays(0);
                            }
                            else
                            {
                                CalendarExtenderTourdate.StartDate = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString()).AddDays(1);
                            }


                        }
                    }

                }
                if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
                {
                    DataTable dt = objMain.GetActivityUpdateDateWiseBlockWiseNew(Convert.ToString(Session["BlockCodeAct"]), "2", "B");
                    if (dt.Rows.Count > 0)
                    {
                    }
                    else
                    {

                        dt = objMain.GetActivityUserWiseMaxDateNewIO(ddlUser.SelectedValue, Convert.ToString(Session["BlockCodeAct"]));
                    }

                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToString(dt.Rows[0]["ActivityDate"].ToString()) != "")
                        {
                            DateTime Activitydate1 = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString());
                            if (Activitydate1.Day == 1 && Activitydate1.Month == 4)
                            {
                                CalendarExtenderTourdate.StartDate = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString()).AddDays(0);
                            }
                            else
                            {
                                CalendarExtenderTourdate.StartDate = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString()).AddDays(1);
                            }

                        }
                    }
                }
                string QueryString = Request.QueryString["ID"];
                string[] a = QueryString.Split(',');
                txtDate.Text = a[0].ToString();
                LoadData(Session["Cluseter"].ToString());


                string ToDate = txtDate.Text;
                string[] c = ToDate.Split('/');
                string aToDate = c[2] + '-' + c[1] + '-' + c[0];

                string con = "";
                DataTable dtMain = null;
                if (Session["user_level"].ToString() == "19")
                {
                    con = "ActivityDate =('" + aToDate + "') and  UserEntry=2 and ApproveStatus='FC'   and mst5village.ClusterCode='" + Session["Cluseter"].ToString() + "' ";
                    dtMain = LoadAllActivtiyDatewise(con, 2);

                }
                if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
                {
                    con = "ActivityDate =('" + aToDate + "')  and  UserEntry=2 and ApproveStatus='B'  and mst5village.ClusterCode='" + Session["Cluseter"].ToString() + "' ";
                    dtMain = LoadAllActivtiyDatewise(con, 2);
                    // dtMain = objMain.LoadSchoolActivtiyCluseterIO(afromDate, aToDate, ddlBlock.SelectedValue, con);
                }
                if (dtMain.Rows.Count > 0)
                {
                    ddlUser.SelectedValue = dtMain.Rows[0]["UserName"].ToString();
                    ddlUser_SelectedIndexChanged(ddlUser, null);

                    if (ddlUser.SelectedIndex > 0)
                    {
                        ddlVilage.SelectedValue = dtMain.Rows[0]["Villagecode"].ToString();
                        ddlVilage_SelectedIndexChanged(ddlUser, null);
                        //    ddlVilage_SelectedIndexChanged(ddlVilage, null);
                        //  ddlSchool.SelectedValue = dtMain.Rows[0]["SchoolCode"].ToString();

                        btnSerach_Click(btnSerach, null);
                    }
                }
                else
                {
                    ViewState["GUID"] = "";
                }



            }
        }
    }

    public DataTable LoadAllActivtiyDatewise(string WhereQuery, int flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@WhereQuery", WhereQuery),
            new SqlParameter("@Flag", flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[GetAllActivityUpdateDateWise2024]", cmdParameters);
    }
    protected void btnCLT_Click(object sender, EventArgs e)
    {

        foreach (ListItem item in CBL_bookformat.Items) { item.Selected = false; }
        foreach (ListItem item in CBL_bookformatNew.Items) { item.Selected = false; }
        foreach (ListItem item in CBL_bookformatNew1.Items) { item.Selected = false; }
        //foreach (ListItem item in CBL_Muhula.Items) { item.Selected = false; }
        //foreach (ListItem item in CBL_MuhulaNew.Items) { item.Selected = false; }
        //foreach (ListItem item in CBL_MuhulaNew1.Items) { item.Selected = false; }
        txt_pbname.Text = "";
        txt_pbnameNew.Text = "";
        txt_pbnameNew1.Text = "";
        txtmainother.Text = "";
        txtV1illager.Text = "";
        TxtGSS_FeMale.Text = "";
        TxtGSS_Male.Text = "";
        txt_bookformatOther.Text = "";
        txt_bookformatOther1.Text = "";
        chkmcommmeting.Checked = false;
        chkcommmetingTB.Checked = false;
        chkcommmetingFC.Checked = false;
        rdEnrollMent.Checked = false;
        rdRetention.Checked = false;



    }

    protected void btnmm_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in CBL_Muhula.Items) { item.Selected = false; }
        foreach (ListItem item in CBL_MuhulaNew.Items) { item.Selected = false; }
        foreach (ListItem item in CBL_MuhulaNew1.Items) { item.Selected = false; }
        txtMuhala.Text = "";
        txtMuhalaNew.Text = "";
        txtMuhalaNew1.Text = "";
        chkmuhala.Checked = false;
        rblmuhulaTb.Checked = false;
        rblmuhulaFC.Checked = false;
        txtVillager2.Text = "";
        txtmOther.Text = "";
        txtmOther1.Text = "";
        TxtMM_FeMale.Text = "";
        TxtMM_Male.Text = "";
        rdEnrollment1.Checked = false;
        rdRetantion1.Checked = false;

    }


    protected void btnOther_Click(object sender, EventArgs e)
    {

        foreach (ListItem item in chk_othercom.Items) { item.Selected = false; }
        foreach (ListItem item in chk_othercom_New.Items) { item.Selected = false; }
        foreach (ListItem item in chk_othercom_New1.Items) { item.Selected = false; }
        tc1.Text = "";
        txtOtherComminuty.Text = "";
        txtOtherComminutyNew.Text = "";
        txtOtherComminutyNew1.Text = "";
        chkothercomm.Checked = false;
        rblothercommTb.Checked = false;
        rblothercommfc.Checked = false;
        txtvillager3.Text = "";
        txtOtherComm.Text = "";
        txtOtherComm1.Text = "";
        TxtCm1_FeMale.Text = "";
        TxtCm1_Male.Text = "";
        rdEnrollment2.Checked = false;
        rdRetantion2.Checked = false;
    }
    protected void btnOthe88r_Click(object sender, EventArgs e)
    {

        foreach (ListItem item in chk_comm.Items) { item.Selected = false; }
        foreach (ListItem item in chk_chkconn.Items) { item.Selected = false; }


        rblCommFC.Checked = false;
        rblcommtb.Checked = false;

        txtOtherCon.Text = "";
        txt_con_other.Text = "";

    }
    public void LoadData(string ClusterName)
    {

        string fromDate = txtDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];
        string UserName = "";

        string strQry2 = "";
        strQry2 += " select distinct UserID from tblActivityUpdate_Village  ";
        strQry2 += " inner join mst5village on mst5village.villagecode=tblActivityUpdate_Village.villagecode  ";
        strQry2 += " where ActivityDate =('" + afromDate + "')  and  ";
        strQry2 += " mst5village.ClusterCode  = '" + Session["Cluseter"].ToString() + "'";

        DataTable dtUseryyy = objMain.LoadData(strQry2);
        var kk = 0;
        if (dtUseryyy.Rows.Count > 0)
        {
            for (kk = 0; kk < dtUseryyy.Rows.Count; kk++)
            {

                UserName += "'" + dtUseryyy.Rows[kk]["UserID"].ToString() + "'" + ",";

            }

        }
        if (UserName.Length > 0)
        {
            UserName = UserName.Substring(0, UserName.LastIndexOf(","));
        }
        else
        {
            UserName = "'ggg'";
        }
        string strQry = "";
        strQry = "Select  distinct UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName]  from MstUser  where UserLevel=24 and VillageCode = '" + Session["Cluseter"].ToString() + "'  ";

        strQry += " union  ";
        strQry += " Select  distinct UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName]  from MstUser  where   UserName in(" + UserName + ") ";
        //strQry += " select UserID from tblActivityUpdate_Village  ";
        //strQry += " inner join mst5village on mst5village.villagecode=tblActivityUpdate_Village.villagecode  ";
        //strQry += " where ActivityDate =('" + afromDate + "')  and  ";
        //strQry += " mst5village.ClusterCode  = '" + Session["Cluseter"].ToString() + "')    ";


        //    conditions = "UserLevel=24 and VillageCode  in( select ClusterCode from mstCluster where ClusterName ='" + ClusterName + "') ";
        //if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "30")
        //{
        //    conditions = conditions + " and DistrictCode='" + Session["DistrictCode"].ToString() + "' ";
        //}

        //if (Session["user_level"].ToString() == "19" )
        //{
        //    conditions = conditions + " and BlockCode='" + Session["BlockCode"].ToString() + "' ";
        //}
        //if (Session["user_level"].ToString() == "24" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "61" || Session["user_level"].ToString() == "59")
        //{
        //    conditions = conditions + " and UserName='as' ";
        //}
        DataTable dtUser = objMain.LoadData(strQry);
        objComman.BindDLLMasterTable("MstUser", "UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName] ", dtUser, conditions, "", "", ddlUser, "UserName", "UserId", "Select");



    }
    protected void btnD2dSerach_Click(object sender, EventArgs e)
    {
        if (this.ddlSearch.SelectedIndex > 0)
        {
            DataTable dataTable = this.Session["D2dBind"] as DataTable;
            if (Convert.ToInt32(this.ddlSearch.SelectedValue) == 1)
            {
                string str = "UniqueId";
                DataTable dataTable2 = dataTable.Copy();
                string rowFilter = str + " like '%" + this.txtSearch.Text.Trim() + "%'   ";
                dataTable2.DefaultView.RowFilter = rowFilter;
                dataTable2.DefaultView.Sort = "UniqueId asc";
                Gv_Display.DataSource = dataTable2.DefaultView.ToTable();
                Gv_Display.DataBind();
            }
            if (Convert.ToInt32(this.ddlSearch.SelectedValue) == 2)
            {
                string str2 = "HHNo";
                DataTable dataTable3 = dataTable.Copy();
                string rowFilter = str2 + " like '%" + this.txtSearch.Text.Trim() + "%'   ";
                dataTable3.DefaultView.RowFilter = rowFilter;
                dataTable3.DefaultView.Sort = "HHNo asc";
                Gv_Display.DataSource = dataTable3.DefaultView.ToTable();
                Gv_Display.DataBind();
            }

            if (Convert.ToInt32(this.ddlSearch.SelectedValue) == 3)
            {
                string str2 = "ChildName";
                DataTable dataTable3 = dataTable.Copy();
                string rowFilter = str2 + " like '%" + this.txtSearch.Text.Trim() + "%'   ";
                dataTable3.DefaultView.RowFilter = rowFilter;
                dataTable3.DefaultView.Sort = "ChildName asc";
                Gv_Display.DataSource = dataTable3.DefaultView.ToTable();
                Gv_Display.DataBind();
            }

            if (Convert.ToInt32(this.ddlSearch.SelectedValue) == 4)
            {
                string str2 = "FathersName";
                DataTable dataTable3 = dataTable.Copy();
                string rowFilter = str2 + " like '%" + this.txtSearch.Text.Trim() + "%'   ";
                dataTable3.DefaultView.RowFilter = rowFilter;
                dataTable3.DefaultView.Sort = "FathersName asc";
                Gv_Display.DataSource = dataTable3.DefaultView.ToTable();
                Gv_Display.DataBind();
            }
        }
        this.ModalPopupExtender.Show();
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        //  btnApprove.Attributes.Add("onclick", "javascript:return " + "confirm('Please confirm if you want to approve? ')");


        Response.Redirect("~/FrmActivityDatewiseSearch.aspx?ID=" + Session["CluseterName"].ToString() + "," + Session["FromData"].ToString() + "," + Session["Todate"].ToString() + "");


    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (ddlRemark.SelectedIndex > 0)
        {
            pnlMain.Enabled = true;
            pnlMain11.Enabled = true;
            Panel1.Enabled = true;
            btnSerach_Click(btnSerach, null);
        }
        else
        {
            pnlMain.Enabled = false;
            pnlMain11.Enabled = false;
            Panel1.Enabled = false;
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        this.ModalPopupExtender1.Show();
    }

    public void LoadOtherCon()
    {
        string strQry;
        strQry = " select *  from [MSTtopicDiscuss]   where Flag=107 and [Language]=0  and  TopicDIscussID not in(9, 10) ";

        DataTable dtRole = objMain.LoadData(strQry);
        chk_othercom.DataSource = dtRole;
        chk_othercom.DataTextField = "TopicDiscussName";
        chk_othercom.DataValueField = "TopicDIscussID";
        chk_othercom.DataBind();

        string strQry1 = " select *  from [MSTtopicDiscuss]   where Flag=108 and [Language]=0 and TopicDIscussID not in(100, 101) ";

        DataTable dtNew = objMain.LoadData(strQry1);
        chk_othercom_New.DataSource = dtNew;
        chk_othercom_New.DataTextField = "TopicDiscussName";
        chk_othercom_New.DataValueField = "TopicDIscussID";
        chk_othercom_New.DataBind();

        strQry = " select *  from [MSTtopicDiscuss]   where Flag=109 and [Language]=0 and TopicDIscussID not in(109, 110)  ";
        DataTable dtNew1 = objMain.LoadData(strQry);


        chk_othercom_New1.DataSource = dtNew1;
        chk_othercom_New1.DataTextField = "TopicDiscussName";
        chk_othercom_New1.DataValueField = "TopicDIscussID";
        chk_othercom_New1.DataBind();
    }

    protected void rblothem_Click(object sender, EventArgs e)
    {
        LoadOtherConch();
    }
    public void LoadOtherConch()
    {
        if (rdEnrollment2.Checked == true || rdRetantion2.Checked == true)
        {
            string strQry;
            strQry = " select *  from [MSTtopicDiscuss]   where Flag=107 and [Language]=0  and  TopicDIscussID not in(9, 10) ";

            DataTable dtRole = objMain.LoadData(strQry);
            chk_othercom.DataSource = dtRole;
            chk_othercom.DataTextField = "TopicDiscussName";
            chk_othercom.DataValueField = "TopicDIscussID";
            chk_othercom.DataBind();

            string strQry1 = " select *  from [MSTtopicDiscuss]   where Flag=108 and [Language]=0 and TopicDIscussID not in(100, 101) ";

            DataTable dtNew = objMain.LoadData(strQry1);
            chk_othercom_New.DataSource = dtNew;
            chk_othercom_New.DataTextField = "TopicDiscussName";
            chk_othercom_New.DataValueField = "TopicDIscussID";
            chk_othercom_New.DataBind();

            strQry = " select *  from [MSTtopicDiscuss]   where Flag=109 and [Language]=0 and TopicDIscussID not in(109, 110)  ";
            DataTable dtNew1 = objMain.LoadData(strQry);


            chk_othercom_New1.DataSource = dtNew1;
            chk_othercom_New1.DataTextField = "TopicDiscussName";
            chk_othercom_New1.DataValueField = "TopicDIscussID";
            chk_othercom_New1.DataBind();
        }

        if (rpSocialMapping.Checked == true)
        {
            string strQry;
            strQry = " select *  from [MSTtopicDiscuss]   where Flag=107 and [Language]=0  and  TopicDIscussID  in(1, 3, 9, 10) ";

            DataTable dtRole = objMain.LoadData(strQry);
            chk_othercom.DataSource = dtRole;
            chk_othercom.DataTextField = "TopicDiscussName";
            chk_othercom.DataValueField = "TopicDIscussID";
            chk_othercom.DataBind();

            string strQry1 = " select *  from [MSTtopicDiscuss]   where Flag=108 and [Language]=0 and TopicDIscussID  in(90, 93, 95, 96, 100, 101) ";

            DataTable dtNew = objMain.LoadData(strQry1);
            chk_othercom_New.DataSource = dtNew;
            chk_othercom_New.DataTextField = "TopicDiscussName";
            chk_othercom_New.DataValueField = "TopicDIscussID";
            chk_othercom_New.DataBind();

            strQry = " select *  from [MSTtopicDiscuss]   where Flag=109 and [Language]=0   ";
            DataTable dtNew1 = objMain.LoadData(strQry);


            chk_othercom_New1.DataSource = dtNew1;
            chk_othercom_New1.DataTextField = "TopicDiscussName";
            chk_othercom_New1.DataValueField = "TopicDIscussID";
            chk_othercom_New1.DataBind();
        }
    }
    public void LoadData()
    {
        string strQry;
        // strQry = " select Villagecode + '-' +RIGHT('0000' +  convert(varchar,serial), 4) as UniqueId,HHNo,ChildName,FathersName  from tblDTD   where villagecode='82106996'  ";
        strQry = " select *  from [MSTtopicDiscuss]   where Flag=1 and [Language]=0  ";


        DataTable dtRole = objMain.LoadData(strQry);
        CBL_bookformat.DataSource = dtRole;
        CBL_bookformat.DataTextField = "TopicDiscussName";
        CBL_bookformat.DataValueField = "TopicDIscussID";
        CBL_bookformat.DataBind();


        CBL_Muhula.DataSource = dtRole;
        CBL_Muhula.DataTextField = "TopicDiscussName";
        CBL_Muhula.DataValueField = "TopicDIscussID";
        CBL_Muhula.DataBind();


        chk_c2.DataSource = dtRole;
        chk_c2.DataTextField = "TopicDiscussName";
        chk_c2.DataValueField = "TopicDIscussID";
        chk_c2.DataBind();

        chk_comm.DataSource = dtRole;
        chk_comm.DataTextField = "TopicDiscussName";
        chk_comm.DataValueField = "TopicDIscussID";
        chk_comm.DataBind();

        strQry = " select *  from [MSTtopicDiscuss]   where Flag=4 and [Language]=0  ";


        DataTable dtTopic = objMain.LoadData(strQry);

        chk_chkconn.DataSource = dtTopic;
        chk_chkconn.DataTextField = "TopicDiscussName";
        chk_chkconn.DataValueField = "TopicDIscussID";
        chk_chkconn.DataBind();


        strQry = " select *  from [MSTtopicDiscuss]   where Flag=5 and [Language]=0  ";

        DataTable dtTopic1 = objMain.LoadData(strQry);

        chk_Suport.DataSource = dtTopic1;
        chk_Suport.DataTextField = "TopicDiscussName";
        chk_Suport.DataValueField = "TopicDIscussID";
        chk_Suport.DataBind();

        strQry = " select *  from [MSTtopicDiscuss]   where Flag=27 and [Language]=0  ";
        DataTable dtNew = objMain.LoadData(strQry);
        CBL_bookformatNew.DataSource = dtNew;
        CBL_bookformatNew.DataTextField = "TopicDiscussName";
        CBL_bookformatNew.DataValueField = "TopicDIscussID";
        CBL_bookformatNew.DataBind();

        CBL_MuhulaNew.DataSource = dtNew;
        CBL_MuhulaNew.DataTextField = "TopicDiscussName";
        CBL_MuhulaNew.DataValueField = "TopicDIscussID";
        CBL_MuhulaNew.DataBind();

        //chk_othercom_New.DataSource = dtNew;
        //chk_othercom_New.DataTextField = "TopicDiscussName";
        //chk_othercom_New.DataValueField = "TopicDIscussID";
        //chk_othercom_New.DataBind();

        strQry = " select *  from [MSTtopicDiscuss]   where Flag=28 and [Language]=0  ";
        DataTable dtNew1 = objMain.LoadData(strQry);
        CBL_bookformatNew1.DataSource = dtNew1;
        CBL_bookformatNew1.DataTextField = "TopicDiscussName";
        CBL_bookformatNew1.DataValueField = "TopicDIscussID";
        CBL_bookformatNew1.DataBind();

        CBL_MuhulaNew1.DataSource = dtNew1;
        CBL_MuhulaNew1.DataTextField = "TopicDiscussName";
        CBL_MuhulaNew1.DataValueField = "TopicDIscussID";
        CBL_MuhulaNew1.DataBind();

        //chk_othercom_New1.DataSource = dtNew1;
        //chk_othercom_New1.DataTextField = "TopicDiscussName";
        //chk_othercom_New1.DataValueField = "TopicDIscussID";
        //chk_othercom_New1.DataBind();


        string strQry44 = " select *  from [mstLookup]   where [LookupFlag]='Doc' ";
        DataTable dtNew132 = objMain.LoadData(strQry44);
        chktBanding.DataSource = dtNew132;
        chktBanding.DataTextField = "Description";
        chktBanding.DataValueField = "LookupCode";
        chktBanding.DataBind();


        strQry = " select *  from [mstLookup]   where [LookupFlag]='STN' and LookupCode in(0,1,2)   ";

        DataTable dtSTN = objMain.LoadData(strQry);
        Session["dtstn"] = dtSTN;


        strQry = " select *  from [mstLookup]   where [LookupFlag]='ÁW'   ";
        DataTable dtAW = objMain.LoadData(strQry);

        chkAwarenessFo.DataSource = dtAW;
        chkAwarenessFo.DataTextField = "Description";
        chkAwarenessFo.DataValueField = "LookupCode";
        chkAwarenessFo.DataBind();
        chkAwarenessIo.DataSource = dtAW;
        chkAwarenessIo.DataTextField = "Description";
        chkAwarenessIo.DataValueField = "LookupCode";
        chkAwarenessIo.DataBind();

        chkAwarenessEo.DataSource = dtAW;
        chkAwarenessEo.DataTextField = "Description";
        chkAwarenessEo.DataValueField = "LookupCode";
        chkAwarenessEo.DataBind();
        objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='MFo' ", "LookupCode", "asc", ddlFo, "Description", "LookupCode", "Select");


        objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='Ine' ", "LookupCode", "asc", ddlIReasons, "Description", "LookupCode", "Select");

        objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='MFo' ", "LookupCode", "asc", ddlFo, "Description", "LookupCode", "Select");

        objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='Ens' ", "LookupCode", "asc", ddlSession, "Description", "LookupCode", "Select");


        if (Session["StateCode"].ToString() == "8")
        {
            objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='DOP' and LookupCode in(1,2,3,4,6) ", "LookupCode", "asc", ddlDOproof, "Description", "LookupCode", "Select");
        }
        else
        {
            objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='DOP' and LookupCode in(1,2,3,5,6) ", "LookupCode", "asc", ddlDOproof, "Description", "LookupCode", "Select");
        }


        objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='MCL' ", "LookupCode", "asc", ddlMonth, "Description", "LookupCode", "Select");



        objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='MST' ", "LookupCode", "asc", ddlFromStatus, "Description", "LookupCode", "Select");

        objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='ECM' ", "LookupCode", "asc", ddlCategory, "Description", "LookupCode", "Select");
        objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='CLM' ", "LookupCode", "asc", ddlClass, "Description", "LookupCode", "Select");

        objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='INT ' ", "LookupCode", "asc", ddlMigration, "Description", "LookupCode", "Select");


        objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='CFC' ", "LookupCode", "asc", ddlImplementerFo, "Description", "LookupCode", "Select");
        objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='CIT' ", "LookupCode", "asc", ddlJoinFo, "Description", "LookupCode", "Select");

        objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='ECN' ", "LookupCode", "asc", ddlEnrolmentCategory, "Description", "LookupCode", "Select");

        objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='CFC' ", "LookupCode", "asc", ddlImplementerIo, "Description", "LookupCode", "Select");
        objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='CIT' ", "LookupCode", "asc", ddlJoinIo, "Description", "LookupCode", "Select");

        objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='CFC' ", "LookupCode", "asc", ddlImplementerEo, "Description", "LookupCode", "Select");
        objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='CIT' ", "LookupCode", "asc", ddlJoinEo, "Description", "LookupCode", "Select");

        //strQry = " select UserName as UserId,[UserName]+' ('+ FristName +')' as [UserName]  from MstUser   where UserLevel=24  ";

        //DataTable dtUser = objMain.LoadData(strQry);
        //if (dtUser.Rows.Count > 0)
        //{
        //    ddlUser.DataSource = dtUser;
        //    ddlUser.DataTextField = "UserName";
        //    ddlUser.DataValueField = "UserId";
        //    ddlUser.DataBind();
        //}

        //strQry = " select *  from [MSTtopicDiscuss]   where Flag=32 and [Language]=0  ";
        //DataTable dtNew18 = objMain.LoadData(strQry);
        //ddlResoneMobileFo.DataSource = dtNew18;

        objComman.BindDLL("MSTtopicDiscuss", "TopicDIscussIDNew,TopicDiscussName", "Flag=32 and [Language]=0 ", "TopicDIscussIDNew", "asc", ddlResoneMobileFo, "TopicDiscussName", "TopicDIscussIDNew", "Select");

        objComman.BindDLL("MSTtopicDiscuss", "TopicDIscussIDNew,TopicDiscussName", "Flag=32 and [Language]=0 ", "TopicDIscussIDNew", "asc", ddlResoneMobileIN, "TopicDiscussName", "TopicDIscussIDNew", "Select");
        objComman.BindDLL("MSTtopicDiscuss", "TopicDIscussIDNew,TopicDiscussName", "Flag=32 and [Language]=0 ", "TopicDIscussIDNew", "asc", ddlResoneMobileEN, "TopicDiscussName", "TopicDIscussIDNew", "Select");


        objComman.BindDLL("MSTtopicDiscuss", "TopicDIscussIDNew,TopicDiscussName", "Flag=33 and [Language]=0 ", "TopicDIscussIDNew", "asc", ddlRelationFo, "TopicDiscussName", "TopicDIscussIDNew", "Select");

        objComman.BindDLL("MSTtopicDiscuss", "TopicDIscussIDNew,TopicDiscussName", "Flag=33 and [Language]=0 ", "TopicDIscussIDNew", "asc", ddlRelationIN, "TopicDiscussName", "TopicDIscussIDNew", "Select");
        objComman.BindDLL("MSTtopicDiscuss", "TopicDIscussIDNew,TopicDiscussName", "Flag=33 and [Language]=0 ", "TopicDIscussIDNew", "asc", ddlRelationEN, "TopicDiscussName", "TopicDIscussIDNew", "Select");






        conditions = "UserLevel=24";
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
        {
            conditions = conditions + " and DistrictCode='" + Session["DistrictCode"].ToString() + "' ";
        }

        if (Session["user_level"].ToString() == "19")
        {
            conditions = conditions + " and BlockCode='" + Session["BlockCode"].ToString() + "' ";
        }
        if (Session["user_level"].ToString() == "24" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "61" || Session["user_level"].ToString() == "59")
        {
            conditions = conditions + " and UserName='assa' ";
        }

        // objComman.BindDLL("MstUser", "UserName as UserId,FristName +' ('+ UserName +')' as [UserName] ", conditions, "", "", ddlUser, "UserName", "UserId", "Select");


        //ModalPopupExtender.Dispose();
        //  Gv_Display.DataBind();
    }

    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strQry = "";
        if (ddlUser.SelectedIndex > 0)
        {
            strQry = "   select Villagecode  from MstUser   where UserName='" + ddlUser.SelectedValue + "' ";
            DataTable dtUserVillage = objMain.LoadData(strQry);

            string strVillage = dtUserVillage.Rows[0]["Villagecode"].ToString();
            if (strVillage == "")
            {
                strVillage = "Xgh";
            }
            //conditions = "mst5Village.ClusterCode in('" + strVillage + "') ";

            ////objComman.BindDLL("mst5Village", "VillageCode,VillageName ", conditions, "VillageName", "", ddlVilage, "VillageName", "VillageCode", "Select");
            //strQry = "   select Villagecode  from MstUser   where UserName='" + ddlUser.SelectedValue + "' ";

            //DataTable dtUserVillage = objMain.LoadData(strQry);

            //string strVillage = dtUserVillage.Rows[0]["Villagecode"].ToString();

            conditions = "mst5Village.ClusterCode in('" + strVillage + "') ";

            strQry = "";
            strQry = "select VillageCode,VillageName  from mst5Village where mst5Village.ClusterCode in('" + strVillage + "')  and len(mst5Village.ClusterCode)>2    ";
            strQry += " Union select mstActivityVillage.VillageCode,mstActivityVillage.VillageName  from mstActivityVillage    inner join mst5Village on mst5Village.VillageCode=mstActivityVillage.Villagecode where UserID='" + ddlUser.SelectedValue + "' and mst5Village.Fyear='" + Session["FinYear"].ToString() + "'   ";

            strQry += " Union ";
            strQry += "  select mst5Village.VillageCode,VillageName  from mst5Village  ";
            strQry += " inner join tblActivityUpdate_Village on tblActivityUpdate_Village.VillageCode=mst5Village.VillageCode  ";
            strQry += "  where mst5Village.ClusterCode in('" + Session["Cluseter"].ToString() + "' )   and UserID='" + ddlUser.SelectedValue + "'  order by VillageName  ";
            DataTable dtVillage = objMain.LoadData(strQry);
            //objComman.BindDLLMasterTable("MstUser", "UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName] ", dtUser, conditions, "", "", ddlUser, "UserName", "UserId", "Select");

            objComman.BindDLLMasterTable("mst5Village", "VillageCode,VillageName ", dtVillage, "", "VillageName", "", ddlVilage, "VillageName", "VillageCode", "Select");

            //DataTable dt = objMain.GetActivityUserWiseMaxDate(ddlUser.SelectedValue);
            //if (dt.Rows.Count > 0)
            //{
            //    if (Convert.ToString(dt.Rows[0]["ActivityDate"].ToString()) != "")
            //    {
            //        CalendarExtenderTourdate.StartDate = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString()).AddDays(1);
            //    }
            //}
        }

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        if (ddlUser.SelectedIndex <= 0)
        {
            ModalPopupExtender.Hide();
            this.ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select User')</script>", false);

        }
        if (ddlVilage.SelectedIndex <= 0)
        {
            ModalPopupExtender.Hide();
            this.ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Village')</script>", false);

        }
        ClearData();
        pnlMain.Enabled = true;
        pnlMain11.Enabled = true;
        Panel1.Enabled = true;
    }
    protected void btnOthesrss1_Click(object sender, EventArgs e)
    {


        foreach (ListItem item in chk_Suport.Items)
        {

            item.Selected = false;

        }
        rblsupportfc.Checked = false;
        txtOtherSupport.Text = "";

    }
    protected void btnOt44hesrss1_Click(object sender, EventArgs e)
    {



        rblothertb.Checked = false;
        rblotherfc.Checked = false;
        txtmainother.Text = "";

    }
    protected void btnOtherss1_Click(object sender, EventArgs e)
    {


        foreach (ListItem item in chk_c2.Items)
        {

            item.Selected = false;

        }
        txtoC111.Text = "";
        txtAtt1.Text = "";
        txtoC1.Text = "";
        txtOtherCC1.Text = "";
        rblc1.Checked = false;
        rblc2.Checked = false;



    }
    public void ClearData()
    {

        foreach (ListItem item in CBL_bookformat.Items)
        {
            item.Selected = false;
        }
        foreach (ListItem item in CBL_bookformatNew.Items)
        {
            item.Selected = false;
        }
        foreach (ListItem item in CBL_bookformatNew1.Items)
        {
            item.Selected = false;
        }
        foreach (ListItem item in chk_c2.Items)
        {

            item.Selected = false;
        }


        txtoC111.Text = "";
        txtAtt1.Text = "";
        txtoC1.Text = "";
        txtOtherCC1.Text = "";
        rblc1.Checked = false;
        rblc2.Checked = false;
        rblTbhold.Checked = false;
        rblFcHold.Checked = false;
        txt_pbname.Text = "";
        txt_pbnameNew.Text = "";
        txt_pbnameNew1.Text = "";
        txtmOther.Text = "";
        txtmainother.Text = "";
        txt_bookformatOther.Text = "";
        txt_bookformatOther1.Text = "";
        chkmcommmeting.Checked = false;
        rdEnrollMent.Checked = false;
        rdEnrollment1.Checked = false;
        rdEnrollment2.Checked = false;
        rdRetention.Checked = false;
        rdRetantion1.Checked = false;
        rdRetantion2.Checked = false;
        rpSocialMapping.Checked = false;

        chkcommmetingTB.Checked = false;

        chkcommmetingFC.Checked = false;

        txtV1illager.Text = "";
        txt_bookformatOther.Text = "";



        foreach (ListItem item in CBL_Muhula.Items)
        {
            item.Selected = false;
        }
        foreach (ListItem item in CBL_MuhulaNew.Items)
        {
            item.Selected = false;
        }
        foreach (ListItem item in CBL_MuhulaNew1.Items)
        {
            item.Selected = false;
        }

        txtMuhala.Text = "";
        txtMuhalaNew.Text = "";
        txtMuhalaNew1.Text = "";
        chkmuhala.Checked = false;


        rblmuhulaTb.Checked = false;

        rblmuhulaFC.Checked = false;

        txtVillager2.Text = "";
        txtmOther.Text = "";
        txtmOther1.Text = "";


        foreach (ListItem item in chk_othercom.Items)
        {

            item.Selected = false;

        }
        foreach (ListItem item in chk_othercom_New.Items)
        {
            item.Selected = false;
        }
        foreach (ListItem item in chk_othercom_New1.Items)
        {
            item.Selected = false;
        }
        txtOtherComminuty.Text = "";
        txtOtherComminutyNew.Text = "";
        txtOtherComminutyNew1.Text = "";
        chkothercomm.Checked = false;

        rblothercommTb.Checked = false;

        rblothercommfc.Checked = false;

        txtvillager3.Text = "";
        txtOtherComm.Text = "";



        foreach (ListItem item in chk_comm.Items)
        {

            item.Selected = false;

        }


        txtOtherConnect.Text = "";
        chkcoom.Checked = false;

        rblcommtb.Checked = false;

        rblCommFC.Checked = false;

        txtOtherCon.Text = "";



        foreach (ListItem item in chk_chkconn.Items)
        {

            item.Selected = false;

        }


        txt_conn.Text = "";
        chkcoom.Checked = false;

        rblcommtb.Checked = false;

        rblCommFC.Checked = false;


        txt_con_other.Text = "";





        foreach (ListItem item in chk_chkconn.Items)
        {

            item.Selected = false;

        }
        foreach (ListItem item in chk_Suport.Items)
        {

            item.Selected = false;

        }

        txtSuport.Text = "";
        chkSupoort.Checked = false;

        //   rblSupporttb.Checked = false;

        rblsupportfc.Checked = false;


        txtOtherSupport.Text = "";



        rblothertb.Checked = false;

        rblotherfc.Checked = false;

        chkother.Checked = false;

        txt_con_other.Enabled = false;


        txtOtherCon.Enabled = false;
        txtOtherComm1.Text = "";
        txtOtherComm.Enabled = false;
        txtOtherComm1.Enabled = false;
        txtmOther.Enabled = false;
        txtmOther1.Enabled = false;
        txt_bookformatOther.Enabled = false;
        txt_bookformatOther1.Enabled = false;
        txtOtherSupport.Enabled = false;
        TxtGSS_FeMale.Text = "";
        TxtGSS_Male.Text = "";
        TxtMM_FeMale.Text = "";
        TxtMM_Male.Text = "";
        TxtCm1_FeMale.Text = "";
        TxtCm1_Male.Text = "";
        tc1.Text = "";
        trGssId.Visible = false;
        trmmId.Visible = false;
        txtMumaullGss.Text = "";
        txtMumaullmm.Text = "";
        ddlBo.SelectedIndex = 0;
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ViewState["GUID"].ToString().Length > 5)
        {
            int res1 = 0;
            //if (ddlRemark.SelectedIndex > 0)
            //{
            res1 = objMain.DeleteD2dDataAcctivtiyVillage(ViewState["GUID"].ToString());
            if (res1 > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Delete Sucessfully')</script>", false);
            }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Remark')</script>", false);

            //}
        }
    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        ClearData();

        if (ddlUser.SelectedIndex <= 0)
        {
            ModalPopupExtender.Hide();
            this.ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select User')</script>", false);
            return;
        }
        if (ddlVilage.SelectedIndex <= 0)
        {
            ModalPopupExtender.Hide();
            this.ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Village')</script>", false);
            return;
        }

        if (this.ddlRemark.SelectedIndex > 0)
        {
            pnlMain.Enabled = true;
            pnlMain11.Enabled = true;
            Panel1.Enabled = true;
        }
        else
        {
            pnlMain.Enabled = false;
            pnlMain11.Enabled = false;
            Panel1.Enabled = false;
        }
        string Dateof = txtDate.Text;



        rblTbhold.Enabled = true;


        string[] b = Dateof.Split('/');

        string FcDate = b[2] + '-' + b[1] + '-' + b[0];
        string strQry = "";

        if (Session["user_level"].ToString() == "19")
        {
            strQry = "   select *  from tblActivityUpdate_Village   where UserID='" + ddlUser.SelectedValue + "' and ApproveStatus='FC' and VillageCode='" + ddlVilage.SelectedValue + "' and UserEntry=3 and ActivityDate= '" + Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd") + "' ";


        }
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
        {
            strQry = "   select *  from tblActivityUpdate_Village   where UserID='" + ddlUser.SelectedValue + "' and ApproveStatus='B' and VillageCode='" + ddlVilage.SelectedValue + "' and UserEntry=3 and ActivityDate= '" + Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd") + "' ";


        }

        DataTable dtVillageActivtiy = objMain.LoadData(strQry);

        if (dtVillageActivtiy.Rows.Count > 0)
        {
            if (dtVillageActivtiy.Rows[0]["ApproveStatus"].ToString() == "B" || dtVillageActivtiy.Rows[0]["ApproveStatus"].ToString() == "FC" || dtVillageActivtiy.Rows[0]["ApproveStatus"].ToString() == "I")
            {
                if (Session["user_level"].ToString() == "19" && dtVillageActivtiy.Rows[0]["ApproveStatus"].ToString() == "FC")
                {
                    btnsave.Visible = true;
                }
                else
                {
                    btnsave.Visible = false;
                }
                if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
                {
                    if (dtVillageActivtiy.Rows[0]["ApproveStatus"].ToString() == "B")
                    {
                        btnsave.Visible = true;
                    }
                    else
                    {
                        btnsave.Visible = false;
                    }
                }
            }

            imgGSS.Visible = false;
            ImgMM.Visible = false;
            imgComm1.Visible = false;
            imgComm2.Visible = false;
            lblGG.Text = "";
            lblMM.Text = "";
            lblCom.Text = "";
            lblCom1.Text = "";
            if (dtVillageActivtiy.Rows[0]["Remarks"].ToString() != "0")
            {
                ddlRemark.SelectedValue = dtVillageActivtiy.Rows[0]["Remarks"].ToString();
            }
            if (dtVillageActivtiy.Rows[0]["PhotoGSS"].ToString().Length > 0)
            {
                lblGG.Text = dtVillageActivtiy.Rows[0]["PhotoGSS"].ToString();

                //imgMKS.ImageUrl = ResolveUrl("~/TabletImage/" + imagename);

                imgGSS.Visible = true;

            }
            if (dtVillageActivtiy.Rows[0]["PhotoMM"].ToString().Length > 0)
            {
                lblMM.Text = dtVillageActivtiy.Rows[0]["PhotoMM"].ToString();
                //imgMKS.ImageUrl = ResolveUrl("~/TabletImage/" + imagename);
                ImgMM.Visible = true;
            }
            if (dtVillageActivtiy.Rows[0]["PhotoOtherComm"].ToString().Length > 0)
            {
                lblCom.Text = dtVillageActivtiy.Rows[0]["PhotoOtherComm"].ToString();
                // imgMKS.ImageUrl = ResolveUrl("~/TabletImage/" + imagename);
                imgComm1.Visible = true;
            }
            if (dtVillageActivtiy.Rows[0]["PhotoOtherComm2"].ToString().Length > 0)
            {
                lblCom1.Text = dtVillageActivtiy.Rows[0]["PhotoOtherComm2"].ToString();
                // imgMKS.ImageUrl = ResolveUrl("~/TabletImage/" + imagename);
                imgComm2.Visible = true;

            }

            if (ddlRemark.SelectedIndex > 0)
            {
                pnlMain.Enabled = true;

                pnlMain11.Enabled = true;
                Panel1.Enabled = true;
            }
            //else
            //{
            //    pnlMain.Enabled = false;
            //}
            #region LoadDate
            ViewState["GUID"] = dtVillageActivtiy.Rows[0]["GUID_Village"].ToString();


            if (dtVillageActivtiy.Rows[0]["TBCode"].ToString().Length > 0)
            {
                ddlGssTbname.SelectedValue = dtVillageActivtiy.Rows[0]["TBCode"].ToString();
                trGssId.Visible = true;
            }


            if (dtVillageActivtiy.Rows[0]["TBCodeOtherMeet"].ToString().Length > 0)
            {
                ddltbCom1.SelectedValue = dtVillageActivtiy.Rows[0]["TBCodeOtherMeet"].ToString();
                tr1.Visible = true;
            }

            if (dtVillageActivtiy.Rows[0]["TBCodeOtherMeet2"].ToString().Length > 0)
            {
                ddltbCom2.SelectedValue = dtVillageActivtiy.Rows[0]["TBCodeOtherMeet2"].ToString();
                tr2.Visible = true;
            }

            if (dtVillageActivtiy.Rows[0]["TBCodemm"].ToString().Length > 0)
            {
                ddlMMTb.SelectedValue = dtVillageActivtiy.Rows[0]["TBCodemm"].ToString();
                trmmId.Visible = true;
            }

            if (dtVillageActivtiy.Rows[0]["Muhalla"].ToString() != "")
            {
                txtMumaullGss.Text = dtVillageActivtiy.Rows[0]["Muhalla"].ToString();

            }
            if (dtVillageActivtiy.Rows[0]["Muhallamm"].ToString() != "")
            {
                txtMumaullmm.Text = dtVillageActivtiy.Rows[0]["Muhallamm"].ToString();

            }
            if (dtVillageActivtiy.Rows[0]["BONotice"].ToString() != "0")
            {
                ddlBo.SelectedValue = dtVillageActivtiy.Rows[0]["BONotice"].ToString();

            }
            if (dtVillageActivtiy.Rows[0]["GSS_Agenda"].ToString() == "1")
            {
                chkcommmetingTB.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["GSS_Agenda"].ToString() == "2")
            {
                chkcommmetingFC.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["GSSEnrollHault"].ToString() == "1")
            {
                rdEnrollMent.Checked = true;
            }
            else if (dtVillageActivtiy.Rows[0]["GSSEnrollHault"].ToString() == "2")
            {
                rdRetention.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["MMEnrollHault"].ToString() == "1")
            {
                rdEnrollment1.Checked = true;
            }
            else if (dtVillageActivtiy.Rows[0]["MMEnrollHault"].ToString() == "2")
            {
                rdRetantion1.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["OtherEnrollHault"].ToString() == "1")
            {
                rdEnrollment2.Checked = true;
                rblothem_Click(rdRetantion2, null);
            }
            else if (dtVillageActivtiy.Rows[0]["OtherEnrollHault"].ToString() == "2")
            {
                rdRetantion2.Checked = true;
                rblothem_Click(rdRetantion2, null);
            }
            else if (dtVillageActivtiy.Rows[0]["OtherEnrollHault"].ToString() == "3")
            {
                rpSocialMapping.Checked = true;
                rblothem_Click(rdRetantion2, null);

            }


            string cmeeting = dtVillageActivtiy.Rows[0]["GSS_Agenda"].ToString();

            string[] meeting = cmeeting.Split(',');
            string TextMeeeting = "";
            foreach (string s in meeting)
            {
                foreach (ListItem item in CBL_bookformat.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        TextMeeeting += item.Text + ",";
                    }
                }
            }
            if (TextMeeeting.Length > 0)
            {
                TextMeeeting = TextMeeeting.Substring(0, TextMeeeting.LastIndexOf(","));
                txt_pbname.Text = TextMeeeting;
                chkmcommmeting.Checked = true;
            }

            string cmeeting1 = dtVillageActivtiy.Rows[0]["GSSChat"].ToString();

            string[] meeting1 = cmeeting1.Split(',');
            string TextMeeeting1 = "";
            foreach (string s in meeting1)
            {
                foreach (ListItem item in CBL_bookformatNew.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        TextMeeeting1 += item.Text + ",";
                    }
                }
            }
            if (TextMeeeting1.Length > 0)
            {
                TextMeeeting1 = TextMeeeting1.Substring(0, TextMeeeting1.LastIndexOf(","));
                txt_pbnameNew.Text = TextMeeeting1;
                chkmcommmeting.Checked = true;
            }
            string cmeeting2 = dtVillageActivtiy.Rows[0]["GSSImportantperson"].ToString();

            string[] meeting2 = cmeeting2.Split(',');
            string TextMeeeting2 = "";
            foreach (string s in meeting2)
            {
                foreach (ListItem item in CBL_bookformatNew1.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        TextMeeeting2 += item.Text + ",";
                    }
                }
            }
            if (TextMeeeting2.Length > 0)
            {
                TextMeeeting2 = TextMeeeting2.Substring(0, TextMeeeting2.LastIndexOf(","));
                txt_pbnameNew1.Text = TextMeeeting2;
                chkmcommmeting.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["GSS_TB"].ToString() == "1")
            {
                rblTb_Click(chkcommmetingFC, null);
                chkcommmetingTB.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["GSS_FC"].ToString() == "1")
            {
                rblTb_Click(chkcommmetingFC, null);
                chkcommmetingFC.Checked = true;
            }
            if (TextMeeeting.Length > 0)
            {
                txtV1illager.Text = dtVillageActivtiy.Rows[0]["GSS_Attended"].ToString();
            }
            if (dtVillageActivtiy.Rows[0]["GSSMale"].ToString() == "0")
            {
                TxtGSS_Male.Text = "";
            }
            else
            {
                TxtGSS_Male.Text = dtVillageActivtiy.Rows[0]["GSSMale"].ToString();
            }
            if (dtVillageActivtiy.Rows[0]["GSSFemale"].ToString() == "0")
            {
                TxtGSS_FeMale.Text = "";
            }
            else
            {
                TxtGSS_FeMale.Text = dtVillageActivtiy.Rows[0]["GSSFemale"].ToString();
            }
            if (dtVillageActivtiy.Rows[0]["MMFemale"].ToString() == "0")
            {
                TxtMM_FeMale.Text = "";
            }
            else
            {
                TxtMM_FeMale.Text = dtVillageActivtiy.Rows[0]["MMFemale"].ToString();
            }
            if (dtVillageActivtiy.Rows[0]["MMMale"].ToString() == "0")
            {
                TxtMM_Male.Text = "";
            }
            else
            {
                TxtMM_Male.Text = dtVillageActivtiy.Rows[0]["MMMale"].ToString();
            }
            if (dtVillageActivtiy.Rows[0]["OtherFemale"].ToString() == "0")
            {
                TxtCm1_FeMale.Text = "";
            }
            else
            {
                TxtCm1_FeMale.Text = dtVillageActivtiy.Rows[0]["OtherFemale"].ToString();
            }
            if (dtVillageActivtiy.Rows[0]["OtherMale"].ToString() == "0")
            {
                TxtCm1_Male.Text = "";
            }
            else
            {
                TxtCm1_Male.Text = dtVillageActivtiy.Rows[0]["OtherMale"].ToString();
            }
            txt_bookformatOther.Text = dtVillageActivtiy.Rows[0]["GSS_AgendaOther"].ToString();
            txt_bookformatOther1.Text = dtVillageActivtiy.Rows[0]["otherGSSChat"].ToString();
            if (txt_bookformatOther.Text.Length > 1)
            {
                txt_bookformatOther.Enabled = true;
            }
            else
            {
                txt_bookformatOther.Enabled = false;
            }
            if (txt_bookformatOther1.Text.Length > 1)
            {
                txt_bookformatOther1.Enabled = true;
            }
            else
            {
                txt_bookformatOther1.Enabled = false;
            }
            if (dtVillageActivtiy.Rows[0]["TBHandholding"].ToString() == "1")
            {
                rblFcHold.Checked = true;
            }
            else
            {
                rblFcHold.Checked = false;
            }





            string MM_Agenda = dtVillageActivtiy.Rows[0]["MM_Agenda"].ToString();
            string[] MMAgenda = MM_Agenda.Split(',');
            string MM_AgendaMeeting = "";
            foreach (string s in MMAgenda)
            {
                foreach (ListItem item in CBL_Muhula.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        MM_AgendaMeeting += item.Text + ",";
                    }
                }
            }
            if (MM_AgendaMeeting.Length > 0)
            {
                MM_AgendaMeeting = MM_AgendaMeeting.Substring(0, MM_AgendaMeeting.LastIndexOf(","));
                txtMuhala.Text = MM_AgendaMeeting;
                chkmuhala.Checked = true;
            }
            string MM_Agenda1 = dtVillageActivtiy.Rows[0]["MMChat"].ToString();
            string[] MMAgenda1 = MM_Agenda1.Split(',');
            string MM_AgendaMeeting1 = "";
            foreach (string s in MMAgenda1)
            {
                foreach (ListItem item in CBL_MuhulaNew.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        MM_AgendaMeeting1 += item.Text + ",";
                    }
                }
            }
            if (MM_AgendaMeeting1.Length > 0)
            {
                MM_AgendaMeeting1 = MM_AgendaMeeting1.Substring(0, MM_AgendaMeeting1.LastIndexOf(","));
                txtMuhalaNew.Text = MM_AgendaMeeting1;
                chkmuhala.Checked = true;
            }
            string MM_Agenda2 = dtVillageActivtiy.Rows[0]["MMImportantperson"].ToString();
            string[] MMAgenda2 = MM_Agenda2.Split(',');
            string MM_AgendaMeeting2 = "";
            foreach (string s in MMAgenda2)
            {
                foreach (ListItem item in CBL_MuhulaNew1.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        MM_AgendaMeeting2 += item.Text + ",";
                    }
                }
            }
            if (MM_AgendaMeeting2.Length > 0)
            {
                MM_AgendaMeeting2 = MM_AgendaMeeting2.Substring(0, MM_AgendaMeeting2.LastIndexOf(","));
                txtMuhalaNew1.Text = MM_AgendaMeeting2;
                chkmuhala.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["MM_TB"].ToString() == "1")
            {
                rblTbmm_Click(rblmuhulaTb, null);
                rblmuhulaTb.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["MM_FC"].ToString() == "1")
            {
                rblTbmm_Click(rblmuhulaTb, null);
                rblmuhulaFC.Checked = true;
            }

            if (MM_AgendaMeeting.Length > 0)
            {
                txtVillager2.Text = dtVillageActivtiy.Rows[0]["MM_Attended"].ToString();
            }
            txtmOther.Text = dtVillageActivtiy.Rows[0]["MM_AgendaOther"].ToString();
            txtmOther1.Text = dtVillageActivtiy.Rows[0]["othermmchat"].ToString();
            if (txtmOther.Text.Length > 1)
            {
                txtmOther.Enabled = true;
            }
            else
            {
                txtmOther.Enabled = false;

            }
            if (txtmOther1.Text.Length > 1)
            {
                txtmOther1.Enabled = true;
            }
            else
            {
                txtmOther1.Enabled = false;

            }


            string Com_Agenda = dtVillageActivtiy.Rows[0]["Com_Agenda"].ToString();

            string[] ComAgenda = Com_Agenda.Split(',');
            string Com_Agendamm = "";
            foreach (string s in ComAgenda)
            {
                foreach (ListItem item in chk_othercom.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        Com_Agendamm += item.Text + ",";
                    }
                }
            }
            if (Com_Agendamm.Length > 0)
            {
                Com_Agendamm = Com_Agendamm.Substring(0, Com_Agendamm.LastIndexOf(","));
                txtOtherComminuty.Text = Com_Agendamm;
                chkothercomm.Checked = true;
            }
            string Com_Agenda1 = dtVillageActivtiy.Rows[0]["OtherChat"].ToString();

            string[] ComAgenda1 = Com_Agenda1.Split(',');
            string Com_Agendamm1 = "";
            foreach (string s in ComAgenda1)
            {
                foreach (ListItem item in chk_othercom_New.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        Com_Agendamm1 += item.Text + ",";
                    }
                }
            }
            if (Com_Agendamm1.Length > 0)
            {
                Com_Agendamm1 = Com_Agendamm1.Substring(0, Com_Agendamm1.LastIndexOf(","));
                txtOtherComminutyNew.Text = Com_Agendamm1;
                chkothercomm.Checked = true;
            }
            string Com_Agenda3 = dtVillageActivtiy.Rows[0]["OtherImportantperson"].ToString();

            string[] ComAgenda3 = Com_Agenda3.Split(',');
            string Com_Agendamm3 = "";
            foreach (string s in ComAgenda3)
            {
                foreach (ListItem item in chk_othercom_New1.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        Com_Agendamm3 += item.Text + ",";
                    }
                }
            }
            if (Com_Agendamm3.Length > 0)
            {
                Com_Agendamm3 = Com_Agendamm3.Substring(0, Com_Agendamm3.LastIndexOf(","));
                txtOtherComminutyNew1.Text = Com_Agendamm3;
                chkothercomm.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["Com_TB"].ToString() == "1")
            {
                rblothercommTb.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["Com_FC"].ToString() == "1")
            {
                rblothercommfc.Checked = true;
            }
            if (Com_Agendamm.Length > 0)

            {
                txtvillager3.Text = dtVillageActivtiy.Rows[0]["Com_Attended"].ToString();
            }
            else
            {
                txtvillager3.Text = "";
            }
            txtOtherComm.Text = dtVillageActivtiy.Rows[0]["Com_AgendaOther"].ToString();
            txtOtherComm1.Text = dtVillageActivtiy.Rows[0]["OtherspecifyChat"].ToString();

            tc1.Text = dtVillageActivtiy.Rows[0]["Any_Other"].ToString();
            if (txtOtherComm.Text.Length > 1)
            {
                txtOtherComm.Enabled = true;
            }
            else
            {
                txtOtherComm.Enabled = false;

            }
            if (txtOtherComm1.Text.Length > 1)
            {
                txtOtherComm1.Enabled = true;
            }
            else
            {
                txtOtherComm1.Enabled = false;

            }

            //---------------------com2


            string Com_Agenda2 = dtVillageActivtiy.Rows[0]["Com_Agenda2"].ToString();

            string[] ComAgenda2 = Com_Agenda2.Split(',');
            string Com_Agendamm2 = "";
            foreach (string s in ComAgenda2)
            {
                foreach (ListItem item in chk_c2.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        Com_Agendamm2 += item.Text + ",";
                    }
                }
            }
            if (Com_Agendamm2.Length > 0)
            {
                Com_Agendamm2 = Com_Agendamm2.Substring(0, Com_Agendamm2.LastIndexOf(","));
                txtOtherCC1.Text = Com_Agendamm2;

            }
            if (dtVillageActivtiy.Rows[0]["Com_TB2"].ToString() == "1")
            {
                rblc1.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["Com_FC2"].ToString() == "1")
            {
                rblc2.Checked = true;
            }
            if (Com_Agendamm2.Length > 0)
            {
                txtAtt1.Text = dtVillageActivtiy.Rows[0]["Com_Attended2"].ToString();

            }
            else
            {
                txtAtt1.Text = "";
            }
            txtoC1.Text = dtVillageActivtiy.Rows[0]["Any_Other2"].ToString();

            txtoC111.Text = dtVillageActivtiy.Rows[0]["Com_AgendaOther2"].ToString();
            if (txtoC111.Text.Length > 1)
            {
                txtoC111.Enabled = true;
            }
            else
            {
                txtoC111.Enabled = false;

            }

            //--------------
            string ComContact_Op = dtVillageActivtiy.Rows[0]["ComContact_Agenda"].ToString();

            string[] ComContactOp = ComContact_Op.Split(',');
            string ComContact_Opmm = "";
            foreach (string s in ComContactOp)
            {
                foreach (ListItem item in chk_comm.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        ComContact_Opmm += item.Text + ",";
                    }
                }
            }
            if (ComContact_Opmm.Length > 0)
            {
                ComContact_Opmm = ComContact_Opmm.Substring(0, ComContact_Opmm.LastIndexOf(","));
                txtOtherConnect.Text = ComContact_Opmm;
                chkcoom.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["ComContact_TB"].ToString() == "1")
            {
                rblcommtb.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["ComContact_FC"].ToString() == "1")
            {
                rblCommFC.Checked = true;
            }

            txtOtherCon.Text = dtVillageActivtiy.Rows[0]["ConContact_AgendaOther"].ToString();
            if (txtOtherCon.Text.Length > 1)
            {
                txtOtherCon.Enabled = true;
            }
            else
            {
                txtOtherCon.Enabled = false;


            }
            string ComContact = dtVillageActivtiy.Rows[0]["ComContact_Op"].ToString();

            string[] ComContactOp1 = ComContact.Split(',');
            string ComContact_Opmm1 = "";
            foreach (string s in ComContactOp1)
            {
                foreach (ListItem item in chk_chkconn.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        ComContact_Opmm1 += item.Text + ",";
                    }
                }
            }
            if (ComContact_Opmm1.Length > 0)
            {
                ComContact_Opmm1 = ComContact_Opmm1.Substring(0, ComContact_Opmm1.LastIndexOf(","));
                txt_conn.Text = ComContact_Opmm1;
                chkcoom.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["ComContact_TB"].ToString() == "1")
            {
                rblcommtb.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["ComContact_FC"].ToString() == "1")
            {
                rblCommFC.Checked = true;
            }

            txt_con_other.Text = dtVillageActivtiy.Rows[0]["ComContact_Op_Other"].ToString();

            if (txt_con_other.Text.Length > 1)
            {
                txt_con_other.Enabled = true;
            }
            else
            {
                txt_con_other.Enabled = false;


            }


            string Support_Op = dtVillageActivtiy.Rows[0]["Support_Op"].ToString();

            string[] SupportOp = Support_Op.Split(',');
            string Support_Op1 = "";
            foreach (string s in SupportOp)
            {
                foreach (ListItem item in chk_Suport.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        Support_Op1 += item.Text + ",";
                    }
                }
            }
            if (Support_Op1.Length > 0)
            {
                Support_Op1 = Support_Op1.Substring(0, Support_Op1.LastIndexOf(","));
                txtSuport.Text = Support_Op1;
                chkSupoort.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["Support_FC"].ToString() == "1")
            {
                rblsupportfc.Checked = true;

            }
            //if (dtVillageActivtiy.Rows[0]["Support_TB"].ToString() == "1")
            //{
            //    rblSupporttb.Checked = true;
            //}

            txtOtherSupport.Text = dtVillageActivtiy.Rows[0]["Support_Op_Other"].ToString();
            if (txtOtherSupport.Text.Length > 1)
            {
                txtOtherSupport.Enabled = true;
            }
            else
            {
                txtOtherSupport.Enabled = false;
            }

            if (dtVillageActivtiy.Rows[0]["Others_FC"].ToString() == "1")
            {
                rblotherfc.Checked = true;

            }
            if (dtVillageActivtiy.Rows[0]["Others_TB"].ToString() == "1")
            {
                rblothertb.Checked = true;
            }
            if (dtVillageActivtiy.Rows[0]["Others_Desc"].ToString().Length > 0)
            {
                chkother.Checked = true;
            }
            txtmainother.Text = dtVillageActivtiy.Rows[0]["Others_Desc"].ToString();

            #endregion
        }
        else
        {
            imgGSS.Visible = false;
            ImgMM.Visible = false;
            imgComm1.Visible = false;
            imgComm2.Visible = false;
            lblGG.Text = "";
            lblMM.Text = "";
            lblCom.Text = "";
            lblCom1.Text = "";
            txt_con_other.Enabled = false;

            btnsave.Visible = true;
            txtOtherCon.Enabled = false;

            txtOtherComm.Enabled = false;
            txtmOther.Enabled = false;
            txt_bookformatOther.Enabled = false;
            txtOtherSupport.Enabled = false;

            ViewState["GUID"] = "";
        }
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
    protected void btnSave_Click(object sender, EventArgs e)
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
        SaveData();
    }
    protected void ddlVilage_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string strQry = "";
        //if (ddlVilage.SelectedIndex > 0)
        //{
        //    strQry = " select [mst5village].EGVillagecode + '-' +RIGHT('0000' +  convert(varchar,serial), 4) as UniqueId,UniqueCode,RIGHT('0000' +  convert(varchar,serial), 4) as  UniqueIdNew,ActivityStatus as Status,HHNo,ChildName,FathersName from  [tblDTD] inner join mst5Village on mst5village.villagecode=tblDTD.villagecode or tblDTD.villagecode=mst5village.OldUniqueCode    or tblDTD.villagecode=mst5village.RefVillageCode   where  tblDTD.Status='1' and mst5village.Villagecode= '" + ddlVilage.SelectedValue + "'    and " + DateTime.Today.Year + " - (YEAR(SurvayDate)-isnull(AgeAson,0))>=6  and (" + DateTime.Today.Year + " - (YEAR(SurvayDate)-isnull(AgeAson,0))<=14  ) and EduationStatus in(2,3,99)   and EnrollStatus=1 and DeleteFlag<>2";

        //    DataTable dtD2d = objMain.LoadData(strQry);
        //    if (dtD2d.Rows.Count > 0)
        //    {
        //        Gv_Display.DataSource = dtD2d;

        //        Gv_Display.DataBind();
        //        Session["D2dBind"] = dtD2d;
        //    }
        //    else
        //    {
        //        Gv_Display.DataSource = null;

        //        Gv_Display.DataBind();
        //    }
        //}
        // ddlRemark.SelectedIndex = 0;
        if (ddlRemark.SelectedIndex > 0)
        {
        }
        else
        {
            pnlMain.Enabled = false;
            pnlMain11.Enabled = false;
            Panel1.Enabled = false;
        }
        LoadTB();
        btnSerach_Click(btnSerach, null);

    }
    protected void rblTb_Click(object sender, EventArgs e)
    {
        if (chkcommmetingTB.Checked == true)
        {
            trGssId.Visible = true;
        }
        else
        {
            trGssId.Visible = false;
        }
    }

    protected void rblTbmm_Click(object sender, EventArgs e)
    {
        if (rblmuhulaTb.Checked == true)
        {
            trmmId.Visible = true;
        }
        else
        {
            trmmId.Visible = false;
        }
    }
    protected void rblothercom_Click(object sender, EventArgs e)
    {
        if (rblothercommTb.Checked == true)
        {
            tr1.Visible = true;
        }
        else
        {
            tr1.Visible = false;
        }
    }
    protected void rblothercom2_Click(object sender, EventArgs e)
    {
        if (rblc1.Checked == true)
        {
            tr2.Visible = true;
        }
        else
        {
            tr2.Visible = false;
        }
    }
    public void LoadTB()
    {
        string strQry = "";
        strQry = "      select TBCode,TBname from mstTeamBalika mst  with(nolock) left join mst5Village V on V.VillageCode=mst.VillageCode   	or  V.refVillage16=mst.VillageCode	or V.refVillage17=mst.VillageCode	or  V.refVillage18=mst.VillageCode or  V.refVillage19=mst.VillageCode or  V.refVillage20=mst.VillageCode or  V.refVillage21=mst.VillageCode  or  V.refVillage22=mst.VillageCode  or  V.refVillage23=mst.VillageCode or  V.refVillage24=mst.VillageCode or  V.refVillage24=mst.VillageCode  where  V.VillageCode='" + ddlVilage.SelectedValue + "'  ";
        DataTable dtVillageActivtiy = objMain.LoadData(strQry);
        Session["TBView"] = dtVillageActivtiy;

        DataTable dt = dtVillageActivtiy.Copy();
        DataTable dtC = dtVillageActivtiy.Copy();
        DataTable dtC1 = dtVillageActivtiy.Copy();
        objComman.BindDLLDatatable("mstSchool", dtVillageActivtiy, "TBCode,TBname", conditions, "TBname", "asc", ddlGssTbname, "TBname", "TBCode", "Select");
        objComman.BindDLLDatatable("mstSchool", dt, "TBCode,TBname", conditions, "TBname", "asc", ddlMMTb, "TBname", "TBCode", "Select");


        objComman.BindDLLDatatable("mstSchool", dtC, "TBCode,TBname", conditions, "TBname", "asc", ddltbCom1, "TBname", "TBCode", "Select");
        objComman.BindDLLDatatable("mstSchool", dtC1, "TBCode,TBname", conditions, "TBname", "asc", ddltbCom2, "TBname", "TBCode", "Select");


    }
    public bool BindDLLDatatable(string dtname, DataTable dt, string fieldname, string Condition, string orberbyfield, string orderby, DropDownList ddl, string textData, string valData, string ZeroIndex)
    {
        bool status = false;
        string conditions = Condition == "" ? "" : " where " + Condition;
        string orberbyfields = orberbyfield == "" ? "" : " order by " + orberbyfield;
        string orderbys = orderby == "" ? "" : orderby;



        if (ZeroIndex != "")
        {
            DataRow dr;
            dr = dt.NewRow();
            dr[textData] = "--" + ZeroIndex + "--";
            dr[valData] = "0";
            dt.Rows.InsertAt(dr, 0);
            dt.AcceptChanges();
        }
        if (dt.Rows.Count > 0)
        {
            ddl.DataTextField = textData;
            ddl.DataValueField = valData;

            ddl.DataSource = dt;
            ddl.DataBind();
            status = true;
        }
        return status;

    }
    protected void Gv_DisplayNew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblActivityStatus1 = ((Label)e.Row.FindControl("lblActivityStatus1"));
            Button btnEditEnroll = ((Button)e.Row.FindControl("btnEditUndo"));
            //if (lblActivityStatus1.Text == "4" || lblActivityStatus1.Text == "1")
            //{
            //    btnEditEnroll.Visible = false;
            //}
            //else
            //{
            //    btnEditEnroll.Visible = true;
            //}

        }
    }
    protected void Gv_Display_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlStatus = ((DropDownList)e.Row.FindControl("ddlStatus"));
            ImageButton btnEditEnroll = ((ImageButton)e.Row.FindControl("btnEditEnroll"));
            Label lblActivityDate = ((Label)e.Row.FindControl("lblActivityDate"));
            Label lbUniqueCode = ((Label)e.Row.FindControl("lbUniqueCode"));
            DataTable dt = Session["dtstn"] as DataTable;
            DataTable dtNew = Session["TBView"] as DataTable;
            DataTable dtEditTB = Session["EditTB"] as DataTable;



            DropDownList ddlContactTb = ((DropDownList)e.Row.FindControl("ddlContactTb"));
            ddlStatus.DataTextField = "Description";
            ddlStatus.DataValueField = "LookupCode";

            ddlStatus.DataSource = dt;
            ddlStatus.DataBind();


            ddlContactTb.DataTextField = "TBName";
            ddlContactTb.DataValueField = "TBCode";

            ddlContactTb.DataSource = dtNew;
            ddlContactTb.DataBind();

            Label lbStatus = ((Label)e.Row.FindControl("lbStatus"));
            ddlStatus.SelectedValue = lbStatus.Text;
            Label lblTBFC = ((Label)e.Row.FindControl("lblTBFC"));

            RadioButtonList rblTBFC = ((RadioButtonList)e.Row.FindControl("rblTBFC"));
            if (lblTBFC.Text == "1")
            {
                rblTBFC.SelectedValue = "1";

                ddlContactTb.Enabled = true;
                if (dtEditTB.Rows.Count > 0)
                {
                    DataRow[] dr = dtEditTB.Select("UniqueCode='" + lbUniqueCode.Text + "'");
                    if (dr.Length > 0)
                    {
                        ddlContactTb.SelectedValue = dr[0]["TBCode"].ToString();
                    }
                }
            }
            if (lblTBFC.Text == "2")
            {
                rblTBFC.SelectedValue = "2";
                ddlContactTb.Enabled = false;
            }


            if (lbStatus.Text == "2" || lbStatus.Text == "3")
            {
                ddlStatus.Enabled = false;
                btnEditEnroll.Visible = true;

            }
            else
            {
                ddlStatus.Enabled = true;
                btnEditEnroll.Visible = false;
            }
            if (lbStatus.Text == "1")
            {
                if (lblActivityDate.Text.Length > 5)
                {
                    if (Convert.ToDateTime(lblActivityDate.Text) == Convert.ToDateTime(DateTime.Today))
                    {
                        ddlStatus.Enabled = false;
                    }
                    else
                    {
                        ddlStatus.Enabled = true;
                    }
                }
                else
                {
                    ddlStatus.Enabled = false;
                }
            }

        }
    }
    protected void ddlDocAva_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalPopupExtender.Show();

        if (Convert.ToInt16(ddlDocAva.SelectedValue) == 2)
        {
            Div333.Visible = true;
        }
        else
        {
            Div333.Visible = false;
        }
        MpexdrFollowup.Show();
    }
    protected void ddlFo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalPopupExtender.Show();
        if (ddlFo.SelectedIndex > 0)
        {
            divF18.Visible = false;
            divF4.Visible = false;
            divF5.Visible = false;
            divF6.Visible = false;
            divF7.Visible = false;
            divF18.Visible = false;
            div24.Visible = false;

            div331.Visible = false;
            div332.Visible = false;
            Div333.Visible = false;
            if (Convert.ToInt32(ddlFo.SelectedValue) == 1)
            {
                divF4.Visible = true;
                divF5.Visible = false;
                divF6.Visible = false;
                divF7.Visible = false;
                txtOtherVillage.Text = "";
                txtOtherSchool.Text = "";
                div24.Visible = false;
                ddlOtherVillage.SelectedIndex = 0;
                ddlEnrolmentCategory.SelectedIndex = 0;

                div331.Visible = true;
                div332.Visible = true;
                //Div333.Visible = true;

            }
            if (Convert.ToInt32(ddlFo.SelectedValue) == 2)
            {
                div24.Visible = true;


            }
            else if (Convert.ToInt32(ddlFo.SelectedValue) == 4)
            {
                divF4.Visible = false;
                divF5.Visible = false;
                divF6.Visible = false;
                divF7.Visible = false;
                divF18.Visible = true;
                div24.Visible = false;
                txtOtherVillage.Text = "";
                txtOtherSchool.Text = "";
                ddlOtherVillage.SelectedIndex = 0;

            }

            MpexdrFollowup.Show();
        }
        else
        {

            divF4.Visible = false;
            divF5.Visible = false;
            divF6.Visible = false;
            divF7.Visible = false;

            MpexdrFollowup.Show();
        }
    }

    protected void ddlFOtherVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalPopupExtender.Show();
        if (ddlOtherVillage.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlOtherVillage.SelectedValue) == 1)
            {

                divF7.Visible = true;
                divF5.Visible = false;
                divF6.Visible = false;
                FillSchool();
            }
            else if (Convert.ToInt32(ddlOtherVillage.SelectedValue) == 2)
            {
                divF7.Visible = false;
                divF5.Visible = true;
                divF6.Visible = true;
            }
            else
            {
                divF7.Visible = false;
                divF5.Visible = false;
                divF6.Visible = false;
            }

            MpexdrFollowup.Show();
        }
        else
        {
            divF7.Visible = false;
            divF5.Visible = false;
            divF6.Visible = false;
            MpexdrFollowup.Show();
        }
    }




    protected void ddlEFOtherVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalPopupExtender.Show();
        if (ddlEotherVillage.SelectedIndex > 0)
        {
            DivE2.Visible = false;
            DivE3.Visible = false;
            DivE4.Visible = false;
            if (Convert.ToInt32(ddlEotherVillage.SelectedValue) == 1)
            {

                DivE2.Visible = true;

                FillSchoolE();
            }
            else if (Convert.ToInt32(ddlEotherVillage.SelectedValue) == 2)
            {
                DivE3.Visible = true;
                DivE4.Visible = true;
            }
            else
            {
                DivE2.Visible = false;
                DivE3.Visible = false;
                DivE4.Visible = false;
            }

            MpexdrFollowup.Show();
        }
        else
        {
            DivE2.Visible = false;
            DivE3.Visible = false;
            DivE4.Visible = false;
            MpexdrFollowup.Show();
        }
    }
    public void FillSchoolE()
    {
        conditions = "";
        conditions = "VillageCode ='" + ddlVilage.SelectedValue + "' and FYear ='" + Session["FinYear"].ToString() + "' ";


        objComman.BindDLL("mstSchool", "SchoolCode,Name", conditions, "Name", "asc", ddlESchool, "Name", "SchoolCode", "Select");



    }
    public void FillSchool()
    {
        conditions = "";
        conditions = "VillageCode ='" + ddlVilage.SelectedValue + "' and FYear ='" + Session["FinYear"].ToString() + "' ";


        objComman.BindDLL("mstSchool", "SchoolCode,Name", conditions, "Name", "asc", ddlSchool, "Name", "SchoolCode", "Select");



    }

    protected void ddlIReasons_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIReasons.SelectedIndex > 0)
        {
            ModalPopupExtender.Show();
            DivI3.Visible = false;
            DivI4.Visible = false;
            DivI5.Style.Add("display", "none");
            ddlMonth.SelectedIndex = 0;
            txtBDate.Text = "";
            ddlDOproof.SelectedIndex = 0;
            DivI7.Visible = false;
            if (Convert.ToInt32(ddlIReasons.SelectedValue) == 1)
            {
                DivI3.Visible = true;
                DivI7.Visible = true;
                MpexdrFollowup.Show();
            }
            else if (Convert.ToInt32(ddlIReasons.SelectedValue) == 2 || Convert.ToInt32(ddlIReasons.SelectedValue) == 3)
            {
                DivI4.Visible = true;
                MpexdrFollowup.Show();
            }
            else
            {
                MpexdrFollowup.Show();
            }
        }
        else
        {
            ddlMonth.SelectedIndex = 0;
            txtBDate.Text = "";
            ddlDOproof.SelectedIndex = 0;
            ModalPopupExtender.Show();
            DivI3.Visible = false;
            DivI4.Visible = false;
            DivI5.Style.Add("display", "none"); ;
            MpexdrFollowup.Show();
        }

    }
    protected void ddlDOproof_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlDOproof.SelectedValue) == 6)
        {
            DivI6.Visible = true;
        }
        else
        {
            DivI6.Visible = false;
            txtOther.Text = "";
        }
        DivI5.Style.Add("display", "block"); ;
        ModalPopupExtender.Show();
        MpexdrFollowup.Show();
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlCategory.SelectedValue) == 99)
        {
            DivE11.Visible = true;
        }
        else
        {
            DivE11.Visible = false;
            txtEnrommentOther.Text = "";
        }
        ModalPopupExtender.Show();
        MpexdrFollowup.Show();
    }

    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlClass.SelectedValue) == 18)
        {
            DivE10.Visible = true;
        }
        else
        {
            DivE10.Visible = false;
            txtClassOther.Text = "";
        }
        ModalPopupExtender.Show();
        MpexdrFollowup.Show();
    }
    public void txtatalter_mobile(object sender, EventArgs e)
    {
        if (txtFoAlternateMobile.Text.Length > 2)
        {
            divFoOwner.Visible = true;
            divForRelation.Visible = true;
            txtFoOwnerRelationChild.Text = "";
            txtRelation.Text = "";
        }
        else
        {
            divFoOwner.Visible = false;
            divForRelation.Visible = false;
            txtFoOwnerRelationChild.Text = "";
            txtRelation.Text = "";
        }
        ModalPopupExtender.Show();
        MpexdrFollowup.Show();
    }
    public void txtatalterIO_mobile(object sender, EventArgs e)
    {
        if (txtIoAlternateMobile.Text.Length > 2)
        {
            divIoOwner.Visible = true;
            divIorRelation.Visible = true;
            txtFoOwnerRelationChild.Text = "";
            txtRelation.Text = "";
        }
        else
        {
            divIoOwner.Visible = false;
            divIorRelation.Visible = false;
            txtIoOwnerRelationChild.Text = "";
            txtIORelation.Text = "";
        }
        ModalPopupExtender.Show();
        MpexdrFollowup.Show();
    }

    public void txtatalterIo_mobile(object sender, EventArgs e)
    {
        if (txtIoAlternateMobile.Text.Length > 2)
        {
            divIoOwner.Visible = true;
            divIorRelation.Visible = true;
            txtIoOwnerRelationChild.Text = "";
            txtIORelation.Text = "";
        }
        else
        {
            divIoOwner.Visible = false;
            divIorRelation.Visible = false;
            txtIoOwnerRelationChild.Text = "";
            txtIORelation.Text = "";
        }
        ModalPopupExtender.Show();
        MpexdrFollowup.Show();
    }

    public void txtatalterEn_mobile(object sender, EventArgs e)
    {
        if (txtEnAlternateMobile.Text.Length > 2)
        {
            divEnOwner.Visible = true;
            divEnrRelation.Visible = true;
            txtEnOwnerRelationChild.Text = "";
            txtEnRelation.Text = "";
        }
        else
        {
            divEnOwner.Visible = false;
            divEnrRelation.Visible = false;
            txtEnOwnerRelationChild.Text = "";
            txtEnRelation.Text = "";
        }
        ModalPopupExtender.Show();
        MpexdrFollowup.Show();
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        DropDownList ddlStatus = (DropDownList)row1.FindControl("ddlStatus");
        RadioButtonList rblTBFC = (RadioButtonList)row1.FindControl("rblTBFC");
        DropDownList ddlContactTb = (DropDownList)row1.FindControl("ddlContactTb");
        Label lbUniqueCode = (Label)row1.FindControl("lbUniqueCode");


        lblEditActivtive.Text = DateTime.Now.ToString();
        lblGuID.Text = "";
        Label lbStatus = (Label)row1.FindControl("lbStatus");
        lblEnrollId.Text = ddlStatus.SelectedValue;
        lblRtbFc.Text = rblTBFC.SelectedValue;
        lblD2dUniqeCode.Text = lbUniqueCode.Text;

        Label lbStatusNew = (Label)row1.FindControl("lbStatusNew");
        if (Convert.ToInt32(rblTBFC.SelectedValue) == 1)
        {
            if (Convert.ToInt32(ddlContactTb.SelectedIndex) <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select TB Name')</script>", false);
                ModalPopupExtender.Show();

                return;

            }
            else
            {
                Session["TBName"] = ddlContactTb.SelectedValue;
            }
            Session["ContactUser"] = "";

        }
        else
        {
            Session["ContactUser"] = ddlUser.SelectedValue;
            Session["TBName"] = "";
        }

        //if (lbStatus.Text == "2" && ddlStatus.SelectedValue.ToString() == "1")
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Allready Contact')</script>", false);
        //    ddlStatus.SelectedIndex = 0;
        //}
        //else if (lbStatus.Text == "0" && ddlStatus.SelectedValue.ToString() == "2")
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Contact frist then Follow up')</script>", false);
        //    ddlStatus.SelectedIndex = 0;
        //}
        //else
        //{

        divResonEN.Visible = false;
        ddlResoneMobileEN.SelectedIndex = 0;
        divResonFo.Visible = false;
        ddlResoneMobileFo.SelectedIndex = 0;
        divResonIN.Visible = false;
        ddlResoneMobileIN.SelectedIndex = 0;
        lbStatus.Text = "2";
        //}
        txtOtherResone.Text = "";
        divF18.Visible = false;
        lblEditRow.Text = "0";

        lblStst.Text = ddlStatus.SelectedItem.Text;
        ddlAvaiFO.SelectedIndex = 0;
        ddlAvialMobile.SelectedIndex = 0;
        txtMobileFO.Text = "";
        txtGovtID.Text = "";
        txtSamgraID.Text = "";
        ModalPopupExtender.Show();
        dvIngilible.Visible = false;
        dvidFollowp.Visible = false;
        dvEnrollment.Visible = false;
        DivI10.Visible = false;
        DivI11.Visible = false;
        DivI7.Visible = false;

        div331.Visible = false;
        div332.Visible = false;
        Div333.Visible = false;

        ddlAvilIO.SelectedIndex = 0;
        ddlAvialMobileIO.SelectedIndex = 0;
        txtSamgra.Text = "";
        txtIGovtID.Text = "";
        txtMobileIO.Text = "";

        ddlAvilEO.SelectedIndex = 0;
        ddlAvialMobileEO.SelectedIndex = 0;
        txtEsamgranID.Text = "";
        txtEGovtID.Text = "";
        txtMobileEO.Text = "";
        DivE12.Visible = false;
        DivE13.Visible = false;
        div17.Visible = false;

        DivI10.Visible = false;
        DivI11.Visible = false;
        div14.Visible = false;


        divF1.Visible = false;
        divF2.Visible = false;
        divMobile.Visible = false;

        foreach (ListItem item in chkAwarenessFo.Items)
        {
            item.Selected = false;
        }
        foreach (ListItem item in chktBanding.Items)
        {
            item.Selected = false;
        }
        txtAwarenessFo.Text = "";
        ddlJoinFo.SelectedIndex = 0;

        ddlSession.SelectedIndex = 0;
        ddlDocAva.SelectedIndex = 0;
        ddlImplementerFo.SelectedIndex = 0;
        ddlEnrolmentCategory.SelectedIndex = 0;
        txtCMobileFO.Text = "";


        foreach (ListItem item in chkAwarenessIo.Items)
        {
            item.Selected = false;
        }

        txtAwarenessIo.Text = "";
        ddlJoinIo.SelectedIndex = 0;
        ddlImplementerIo.SelectedIndex = 0;
        foreach (ListItem item in chkAwarenessEo.Items)
        {
            item.Selected = false;
        }
        txtCMobileIO.Text = "";

        txtAwarenessEo.Text = "";
        ddlJoinEo.SelectedIndex = 0;
        ddlImplementerEo.SelectedIndex = 0;
        ddlCategory.SelectedIndex = 0;
        txtCMobileEO.Text = "";
        div23.Visible = false;
        div26.Visible = false;

        Div27.Visible = false;
        disv23.Visible = false;
        Div31.Visible = false;
        div28.Visible = false;
        div29.Visible = false;
        Div30.Visible = false;
        div25.Visible = false;
        div21.Visible = false;
        div22.Visible = false;
        dfiv22.Visible = false;
        div24.Visible = false;
        divFoOwner.Visible = false;
        divFoOwner.Visible = false;
        divForRelation.Visible = false;


        divIoOwner.Visible = false;
        divIorRelation.Visible = false;

        divEnOwner.Visible = false;
        divEnrRelation.Visible = false;
        txtEnAlternateMobile.Text = "";
        txtIoAlternateMobile.Text = "";
        txtFoAlternateMobile.Text = "";

        if (Convert.ToInt32(ddlStatus.SelectedValue) == 1)
        {
            if (Session["StateCode"].ToString() == "8")
            {


                lblFoabali.Text = "Availability of Adhar Card";
            }
            else
            {
                lblFoabali.Text = "Availability of Samargra ID";
            }
            dvidFollowp.Visible = true;
            divF1.Visible = false;
            divF2.Visible = false;
            divF7.Visible = false;
            divF4.Visible = false;
            divF5.Visible = false;
            divF6.Visible = false;
            DivE10.Visible = false;
            DivE11.Visible = false;
            DivI10.Visible = true;
            DivI7.Visible = false;
            txtGovtID.Text = "";
            txtSamgraID.Text = "";
            ddlFo.SelectedIndex = 0;
            txtOtherVillage.Text = "";
            txtOtherSchool.Text = "";
            ddlOtherVillage.SelectedIndex = 0;
            //  div25.Visible = true;
            div21.Visible = true;
            div22.Visible = true;
            dfiv22.Visible = true;
            div24.Visible = false;



            Label lblDtdUniqid = (Label)row1.FindControl("lblDtdUniqid");

            string strQry = " select *  from [tblDTDMobileActivity]   where IsActive=0 and [GUIDDTDMobileActivity]='" + lblDtdUniqid.Text + "' and ActivityStatus=1 ";


            DataTable dtIne = objMain.LoadData(strQry);
            if (dtIne.Rows.Count > 0)
            {
                ddlFo.SelectedValue = dtIne.Rows[0]["FollowUPID"].ToString();

                ddlFo_SelectedIndexChanged(ddlIReasons, null);
                if (Session["StateCode"].ToString() == "8")
                {
                    txtGovtID.Text = dtIne.Rows[0]["GovtID"].ToString();

                }
                else
                {
                    //   txtSamgraID.Text = dtIne.Rows[0]["SamgraID"].ToString();
                }
                if (dtIne.Rows[0]["FollowUPID"].ToString() == "1" || dtIne.Rows[0]["FollowUPID"].ToString() == "2")
                {
                    ddlOtherVillage.SelectedValue = dtIne.Rows[0]["FollowUPID"].ToString();
                    ddlFOtherVillage_SelectedIndexChanged(ddlIReasons, null);
                    ddlSchool.SelectedValue = dtIne.Rows[0]["SchoolCode"].ToString();
                    txtOtherVillage.Text = dtIne.Rows[0]["SchoolOrVillageName"].ToString();

                    txtOtherSchool.Text = dtIne.Rows[0]["otherSchoolName"].ToString();
                }
                //    ddlMonth.SelectedValue = dtIne.Rows[0]["Months"].ToString();
            }
        }
        if (Convert.ToInt32(ddlStatus.SelectedValue) == 2)
        {

            div23.Visible = true;
            div26.Visible = true;

            Div27.Visible = true;
            // disv23.Visible = true;
            ddlIReasons.SelectedIndex = 0;
            ddlMigration.SelectedIndex = 0;
            txtBDate.Text = "";
            ddlMonth.SelectedIndex = 0;
            ddlDOproof.SelectedIndex = 0;
            dvIngilible.Visible = true;
            if (Session["StateCode"].ToString() == "8")
            {


                lblAvialIO.Text = "Availability of Adhar Card";
            }
            else
            {
                lblAvialIO.Text = "Availability of Samargra ID";
            }
            txtOther.Text = "";
            DivI3.Visible = false;
            DivI4.Visible = false;
            DivI5.Style.Add("display", "none");
            DivI6.Visible = false;
            DivI7.Visible = false;
        }
        if (Convert.ToInt32(ddlStatus.SelectedValue) == 3)
        {
            dvEnrollment.Visible = true;
            ddlEotherVillage.SelectedIndex = 0;
            //  ddlESchool.SelectedIndex = 0;
            ddlFromStatus.SelectedIndex = 0;
            txtErollmentDate.Text = "";
            txtEvillage.Text = "";
            txtSchoolar.Text = "";
            txtSschool.Text = "";
            ddlClass.SelectedIndex = 0;
            txtErollmentDate.Text = "";
            DivE3.Visible = false;
            DivE4.Visible = false;
            DivE2.Visible = false;
            DivE10.Visible = false;
            DivE11.Visible = false;
            DivE12.Visible = false;
            DivE13.Visible = false;

            //  Div31.Visible = true;
            div28.Visible = true;
            div29.Visible = true;
            Div30.Visible = true;
            if (Session["StateCode"].ToString() == "8")
            {


                lblAvailEO.Text = "Availability of Adhar Card";
            }
            else
            {
                lblAvailEO.Text = "Availability of Samargra ID";
            }
        }
        if (ddlStatus.SelectedIndex > 0)
        {
            LoadPreviousData(Convert.ToInt32(ddlStatus.SelectedValue), lbUniqueCode.Text);
            MpexdrFollowup.Show();
        }
    }

    public void txtState_TextChanged(object sender, EventArgs e)
    {
        bool Sbdf = false;
        foreach (ListItem item in chkAwarenessFo.Items)
        {
            if (item.Selected)
            {

                if (item.Value == "4")
                {
                    Sbdf = true;
                    txtAwarenessFo.Text = "";

                }

            }

        }
        if (Sbdf == true)
        {
            foreach (ListItem item in chkAwarenessFo.Items)
            {
                if (item.Value == "4")
                {
                    item.Selected = true;
                    txtAwarenessFo.Text = "No Of These";
                    item.Enabled = true;
                }
                else
                {
                    item.Selected = false;
                    item.Enabled = false;
                }
            }
        }
        else
        {
            foreach (ListItem item in chkAwarenessFo.Items)
            { item.Enabled = true; }
        }
        ModalPopupExtender.Show();
        MpexdrFollowup.Show();
    }

    public void txtState_TextChangedIo(object sender, EventArgs e)
    {
        bool Sbdf = false;
        foreach (ListItem item in chkAwarenessIo.Items)
        {
            if (item.Selected)
            {

                if (item.Value == "4")
                {
                    Sbdf = true;
                    txtAwarenessIo.Text = "";
                }

            }
        }
        if (Sbdf == true)
        {
            foreach (ListItem item in chkAwarenessIo.Items)
            {
                if (item.Value == "4")
                {
                    item.Selected = true;
                    txtAwarenessIo.Text = "No Of These";
                    item.Enabled = true;
                }
                else
                {
                    item.Selected = false;
                    item.Enabled = false;
                }
            }
        }
        else
        {
            foreach (ListItem item in chkAwarenessIo.Items)
            { item.Enabled = true; }
        }
        ModalPopupExtender.Show();
        MpexdrFollowup.Show();
    }
    public void txtStateEO_TextChanged(object sender, EventArgs e)
    {
        bool Sbdf = false;
        foreach (ListItem item in chkAwarenessEo.Items)
        {
            if (item.Selected)
            {

                if (item.Value == "4")
                {
                    Sbdf = true;
                    txtAwarenessEo.Text = "";
                }

            }
        }
        if (Sbdf == true)
        {
            foreach (ListItem item in chkAwarenessEo.Items)
            {
                if (item.Value == "4")
                {
                    item.Selected = true;
                    item.Enabled = true;
                    txtAwarenessEo.Text = "No Of These";
                }
                else
                {
                    item.Selected = false;
                    item.Enabled = false;
                }
            }
        }
        else
        {
            foreach (ListItem item in chkAwarenessEo.Items)
            { item.Enabled = true; }
        }
        ModalPopupExtender.Show();
        MpexdrFollowup.Show();
    }

    protected void ddlAvialMobile_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalPopupExtender.Show();
        txtMobileFO.Text = "";
        if (Convert.ToInt32(ddlAvialMobile.SelectedValue) == 1)
        {


            divMobile.Visible = true;
            div25.Visible = true;
            divResonFo.Visible = false;
        }
        else
        {
            div25.Visible = false;
            divResonFo.Visible = true;
            divMobile.Visible = false;
        }

        MpexdrFollowup.Show();
    }
    public void LoadPreviousData(Int32 Staus, string Uniq)
    {




        ModalPopupExtender.Show();
        dvIngilible.Visible = false;
        dvidFollowp.Visible = false;
        dvEnrollment.Visible = false;

        ddlAvilIO.SelectedIndex = 0;
        ddlAvialMobileIO.SelectedIndex = 0;
        txtSamgra.Text = "";
        txtIGovtID.Text = "";
        txtMobileIO.Text = "";

        ddlAvilEO.SelectedIndex = 0;
        ddlAvialMobileEO.SelectedIndex = 0;
        txtEsamgranID.Text = "";
        txtEGovtID.Text = "";
        txtMobileEO.Text = "";
        DivE12.Visible = false;
        DivE13.Visible = false;
        div17.Visible = false;

        DivI10.Visible = false;
        DivI11.Visible = false;
        div14.Visible = false;


        divF1.Visible = false;
        divF2.Visible = false;
        divMobile.Visible = false;
        if (Convert.ToInt32(Staus) == 1)
        {
            dvidFollowp.Visible = true;
            divF1.Visible = false;
            divF2.Visible = false;
            divF7.Visible = false;
            divF4.Visible = false;
            divF5.Visible = false;
            divF6.Visible = false;
            txtGovtID.Text = "";
            txtSamgraID.Text = "";
            ddlFo.SelectedIndex = 0;
            txtOtherVillage.Text = "";
            txtOtherSchool.Text = "";
            ddlOtherVillage.SelectedIndex = 0;

            string strQry = " select *  from [tblDTDMobileActivity]   where IsActive=0 and [UniqueCode]='" + Uniq + "' and ActivityStatus in(1,2,3) and ActivityDate in(select max(ActivityDate)  from [tblDTDMobileActivity]  where IsActive=0 and ActivityStatus in(1,2,3) and [UniqueCode]='" + Uniq + "') ";


            DataTable dtIne = objMain.LoadData(strQry);
            if (dtIne.Rows.Count > 0)
            {
                if (Convert.ToString(dtIne.Rows[0]["Availability"]) != "")
                {
                    ddlAvaiFO.SelectedValue = Convert.ToString(dtIne.Rows[0]["Availability"]);
                }
                ddlAvilEO_SelectedIndexChanged(ddlAvilIO, null);

                if (Convert.ToString(dtIne.Rows[0]["IsMobile"]) != "")
                {
                    ddlAvialMobile.SelectedValue = Convert.ToString(dtIne.Rows[0]["IsMobile"]);
                }
                ddlAvialMobile_SelectedIndexChanged(ddlAvilIO, null);

                if (Convert.ToString(dtIne.Rows[0]["MobileReason"]) != "")
                {
                    ddlResoneMobileFo.SelectedValue = Convert.ToString(dtIne.Rows[0]["MobileReason"]);
                }
                if (Convert.ToString(dtIne.Rows[0]["Relation"]) != "")
                {
                    ddlRelationFo.SelectedValue = Convert.ToString(dtIne.Rows[0]["Relation"]);
                }
                txtMobileFO.Text = Convert.ToString(dtIne.Rows[0]["Mobile"]);
                txtGovtID.Text = Convert.ToString(dtIne.Rows[0]["GovtID"]);
                txtSamgraID.Text = Convert.ToString(dtIne.Rows[0]["SamgraID "]);


            }

        }
        if (Convert.ToInt32(Staus) == 2)
        {




            ddlIReasons.SelectedIndex = 0;
            txtBDate.Text = "";
            ddlMonth.SelectedIndex = 0;
            ddlDOproof.SelectedIndex = 0;
            dvIngilible.Visible = true;


            DivI3.Visible = false;
            DivI4.Visible = false;
            DivI5.Style.Add("display", "none");
            DivI7.Visible = true;
            string strQry = " select *  from [tblDTDMobileActivity]   where IsActive=0 and [UniqueCode]='" + Uniq + "' and ActivityStatus in(1,2,3) and ActivityDate in(select max(ActivityDate)  from [tblDTDMobileActivity]  where IsActive=0 and ActivityStatus in(1,2,3) and [UniqueCode]='" + Uniq + "') ";


            DataTable dtIne = objMain.LoadData(strQry);
            if (dtIne.Rows.Count > 0)
            {


                if (Convert.ToString(dtIne.Rows[0]["Availability"]) != "")
                {
                    ddlAvilIO.SelectedValue = Convert.ToString(dtIne.Rows[0]["Availability"]);
                }
                ddlAvilIO_SelectedIndexChanged(ddlAvilIO, null);

                if (Convert.ToString(dtIne.Rows[0]["IsMobile"]) != "")
                {
                    ddlAvialMobileIO.SelectedValue = Convert.ToString(dtIne.Rows[0]["IsMobile"]);
                }
                ddlAvialMobileIO_SelectedIndexChanged(ddlAvilIO, null);

                if (Convert.ToString(dtIne.Rows[0]["MobileReason"]) != "")
                {
                    ddlResoneMobileIN.SelectedValue = Convert.ToString(dtIne.Rows[0]["MobileReason"]);
                }
                if (Convert.ToString(dtIne.Rows[0]["Relation"]) != "")
                {
                    ddlRelationIN.SelectedValue = Convert.ToString(dtIne.Rows[0]["Relation"]);
                }
                txtMobileIO.Text = Convert.ToString(dtIne.Rows[0]["Mobile"]);
                txtIGovtID.Text = Convert.ToString(dtIne.Rows[0]["GovtID"]);
                txtSamgra.Text = Convert.ToString(dtIne.Rows[0]["SamgraID "]);


                txtOther.Text = Convert.ToString(dtIne.Rows[0]["Other"]);
                if (Convert.ToString(dtIne.Rows[0]["SamgraID "]).Length > 0)
                {
                }


                txtIoAlternateMobile.Text = Convert.ToString(dtIne.Rows[0]["AlternateMobileNumber "]);
                txtatalterIO_mobile(txtIORelation, null);
                txtIoAlternateMobile.Text = Convert.ToString(dtIne.Rows[0]["AlternatemobileOwneName "]);
                txtIoAlternateMobile.Text = Convert.ToString(dtIne.Rows[0]["RelationChild "]);

            }

        }
        if (Convert.ToInt32(Staus) == 3)
        {
            dvEnrollment.Visible = true;
            ddlEotherVillage.SelectedIndex = 0;
            //  ddlESchool.SelectedIndex = 0;
            ddlFromStatus.SelectedIndex = 0;
            txtErollmentDate.Text = "";
            txtEvillage.Text = "";
            txtSchoolar.Text = "";
            txtSschool.Text = "";
            ddlClass.SelectedIndex = 0;
            txtErollmentDate.Text = "";
            DivE3.Visible = false;
            DivE4.Visible = false;
            DivE2.Visible = false;

            if (Session["StateCode"].ToString() == "8")
            {


                DivE12.Visible = true;
            }
            else
            {
                DivE13.Visible = true;
            }
            string strQry = " select *  from [tblDTDMobileActivity]   where IsActive=0 and [UniqueCode]='" + Uniq + "' and ActivityStatus in(1,2,3) and ActivityDate in(select max(ActivityDate)  from [tblDTDMobileActivity]  where IsActive=0 and ActivityStatus in(1,2,3) and [UniqueCode]='" + Uniq + "') ";

            DataTable dtIne = objMain.LoadData(strQry);
            if (dtIne.Rows.Count > 0)
            {





                if (Convert.ToString(dtIne.Rows[0]["Availability"]) != "")
                {
                    ddlAvilEO.SelectedValue = Convert.ToString(dtIne.Rows[0]["Availability"]);
                }
                ddlAvilEO_SelectedIndexChanged(ddlAvilIO, null);

                if (Convert.ToString(dtIne.Rows[0]["IsMobile"]) != "")
                {
                    ddlAvialMobileEO.SelectedValue = Convert.ToString(dtIne.Rows[0]["IsMobile"]);
                }
                ddlAvialMobileEO_SelectedIndexChanged(ddlAvilIO, null);
                txtMobileEO.Text = Convert.ToString(dtIne.Rows[0]["Mobile"]);
                if (Convert.ToString(dtIne.Rows[0]["MobileReason"]) != "")
                {
                    ddlResoneMobileEN.SelectedValue = Convert.ToString(dtIne.Rows[0]["MobileReason"]);
                }
                if (Convert.ToString(dtIne.Rows[0]["Relation"]) != "")
                {
                    ddlRelationEN.SelectedValue = Convert.ToString(dtIne.Rows[0]["Relation"]);
                }
                txtEGovtID.Text = Convert.ToString(dtIne.Rows[0]["GovtID"]);
                txtEsamgranID.Text = Convert.ToString(dtIne.Rows[0]["SamgraID "]);


                txtEnAlternateMobile.Text = Convert.ToString(dtIne.Rows[0]["AlternateMobileNumber "]);
                txtatalterEn_mobile(txtIORelation, null);
                txtEnAlternateMobile.Text = Convert.ToString(dtIne.Rows[0]["AlternatemobileOwneName "]);
                txtEnAlternateMobile.Text = Convert.ToString(dtIne.Rows[0]["RelationChild "]);
                if (Convert.ToString(dtIne.Rows[0]["ReadyCBL "]).Length > 0)
                {
                    rblEnCBL.SelectedValue = Convert.ToString(dtIne.Rows[0]["ReadyCBL "]);
                }

            }

        }



    }
    protected void ddlAvaiFO_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalPopupExtender.Show();
        txtSamgraID.Text = "";
        txtGovtID.Text = "";
        if (Convert.ToInt32(ddlAvaiFO.SelectedValue) == 1)
        {
            if (Session["StateCode"].ToString() == "8")
            {


                divF1.Visible = true;
            }
            else
            {
                divF2.Visible = true;
            }

        }
        else if (Convert.ToInt32(ddlAvaiFO.SelectedValue) == 2)
        {
            if (Session["StateCode"].ToString() == "8")
            {

                divF1.Visible = true;
            }
            divF2.Visible = false;
        }

        else
        {
            if (Session["StateCode"].ToString() == "8")
            {

                divF1.Visible = true;
            }
            divF2.Visible = false;
        }
        MpexdrFollowup.Show();
    }

    protected void ddlAvilIO_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalPopupExtender.Show();
        txtSamgra.Text = "";
        txtIGovtID.Text = "";

        if (Convert.ToInt32(ddlAvilIO.SelectedValue) == 1)
        {
            if (Session["StateCode"].ToString() == "8")
            {


                DivI10.Visible = true;
            }
            else
            {
                DivI11.Visible = true;
            }

        }
        else if (Convert.ToInt32(ddlAvilIO.SelectedValue) == 1)
        {
            if (Session["StateCode"].ToString() == "8")
            {

                DivI10.Visible = true;
            }

            DivI11.Visible = false;
        }
        else
        {
            if (Session["StateCode"].ToString() == "8")
            {

                DivI10.Visible = true;
            }

            DivI11.Visible = false;
        }
        MpexdrFollowup.Show();
    }
    protected void ddlAvialMobileIO_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtMobileIO.Text = "";
        ModalPopupExtender.Show();
        if (Convert.ToInt32(ddlAvialMobileIO.SelectedValue) == 1)
        {


            div14.Visible = true;
            disv23.Visible = true;
            divResonIN.Visible = false;
        }
        else
        {
            disv23.Visible = false;
            divResonIN.Visible = true;
            div14.Visible = false;
        }
        MpexdrFollowup.Show();
    }


    protected void ddlAvilEO_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalPopupExtender.Show();
        txtEsamgranID.Text = "";
        txtEGovtID.Text = "";
        if (Convert.ToInt32(ddlAvilEO.SelectedValue) == 1)
        {
            if (Session["StateCode"].ToString() == "8")
            {


                DivE12.Visible = true;
            }
            else
            {
                DivE13.Visible = true;
            }

        }
        else
        {
            if (Session["StateCode"].ToString() == "8")
            {

                DivE12.Visible = true;
            }

            DivE13.Visible = false;
        }
        MpexdrFollowup.Show();
    }
    protected void ddlAvialMobileEO_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalPopupExtender.Show();
        txtMobileEO.Text = "";
        if (Convert.ToInt32(ddlAvialMobileEO.SelectedValue) == 1)
        {


            div17.Visible = true;
            divResonEN.Visible = false;
            Div31.Visible = true;
        }
        else
        {
            div17.Visible = false;
            Div31.Visible = true;
            divResonEN.Visible = true;

        }
        MpexdrFollowup.Show();
    }
    protected void btnrblTBFC_Click(object sender, EventArgs e)
    {
        RadioButtonList ddlLabTest1 = (RadioButtonList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;
        DropDownList ddlContactTb = (DropDownList)row1.FindControl("ddlContactTb");
        RadioButtonList rblTBFC = (RadioButtonList)row1.FindControl("rblTBFC");
        if (Convert.ToInt32(rblTBFC.SelectedValue) == 1)
        {
            ddlContactTb.Enabled = true;
            ddlContactTb.SelectedIndex = 0;
        }
        else
        {
            ddlContactTb.Enabled = false;
        }
        ModalPopupExtender.Show();
    }
    protected void btnEditEnroll_Click(object sender, EventArgs e)
    {

        ImageButton ddlLabTest1 = (ImageButton)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        DropDownList ddlStatus = (DropDownList)row1.FindControl("ddlStatus");
        RadioButtonList rblTBFC = (RadioButtonList)row1.FindControl("rblTBFC");
        DropDownList ddlContactTb = (DropDownList)row1.FindControl("ddlContactTb");
        Label lbUniqueCode = (Label)row1.FindControl("lbUniqueCode");
        Label lblDtdUniqid = (Label)row1.FindControl("lblDtdUniqid");

        lblEditRow.Text = "1";

        Label lbStatus = (Label)row1.FindControl("lbStatus");
        lblEnrollId.Text = ddlStatus.SelectedValue;
        lblRtbFc.Text = rblTBFC.SelectedValue;
        lblD2dUniqeCode.Text = lbUniqueCode.Text;
        ModalPopupExtender.Show();
        dvIngilible.Visible = false;
        dvidFollowp.Visible = false;
        dvEnrollment.Visible = false;

        ddlAvilIO.SelectedIndex = 0;
        ddlAvialMobileIO.SelectedIndex = 0;
        txtSamgra.Text = "";
        txtIGovtID.Text = "";
        txtMobileIO.Text = "";

        ddlAvilEO.SelectedIndex = 0;
        ddlAvialMobileEO.SelectedIndex = 0;
        txtEsamgranID.Text = "";
        txtEGovtID.Text = "";
        txtMobileEO.Text = "";
        DivE12.Visible = false;
        DivE13.Visible = false;
        div17.Visible = false;

        DivI10.Visible = false;
        DivI11.Visible = false;
        div14.Visible = false;


        divF1.Visible = false;
        divF2.Visible = false;
        divMobile.Visible = false;

        div23.Visible = false;
        div26.Visible = false;

        Div27.Visible = false;
        disv23.Visible = false;
        Div31.Visible = false;
        div28.Visible = false;
        div29.Visible = false;
        Div30.Visible = false;
        div25.Visible = false;
        div21.Visible = false;
        div22.Visible = false;
        dfiv22.Visible = false;
        div24.Visible = false;

        if (Convert.ToInt32(ddlStatus.SelectedValue) == 1)
        {
            dvidFollowp.Visible = true;
            divF1.Visible = false;
            divF2.Visible = false;
            divF7.Visible = false;
            divF4.Visible = false;
            divF5.Visible = false;
            divF6.Visible = false;
            txtGovtID.Text = "";
            txtSamgraID.Text = "";
            ddlFo.SelectedIndex = 0;
            txtOtherVillage.Text = "";
            txtOtherSchool.Text = "";
            ddlOtherVillage.SelectedIndex = 0;



        }
        if (Convert.ToInt32(ddlStatus.SelectedValue) == 2)
        {




            ddlIReasons.SelectedIndex = 0;
            txtBDate.Text = "";
            ddlMonth.SelectedIndex = 0;
            ddlDOproof.SelectedIndex = 0;
            dvIngilible.Visible = true;


            div23.Visible = true;
            div26.Visible = true;

            Div27.Visible = true;

            DivI3.Visible = false;
            DivI4.Visible = false;
            DivI5.Style.Add("display", "none");
            DivI7.Visible = true;
            string strQry = " select *  from [tblDTDMobileActivity]   where IsActive=0 and [GUIDDTDMobileActivity]='" + lblDtdUniqid.Text + "' and ActivityStatus=" + lblEnrollId.Text + " ";


            DataTable dtIne = objMain.LoadData(strQry);
            if (dtIne.Rows.Count > 0)
            {

                ddlIReasons.SelectedValue = Convert.ToString(dtIne.Rows[0]["IneligibleID"]);
                ddlIReasons_SelectedIndexChanged(ddlFo, null);
                ddlMonth.SelectedValue = Convert.ToString(dtIne.Rows[0]["Months"]);
                lblGuID.Text = Convert.ToString(dtIne.Rows[0]["GUIDDTDMobileActivity"]);

                txtOtherResone.Text = Convert.ToString(dtIne.Rows[0]["FollowUpReason"]);
                ddlDOproof.SelectedValue = Convert.ToString(dtIne.Rows[0]["DOBproof"]);
                ddlMigration.SelectedValue = Convert.ToString(dtIne.Rows[0]["Migrationplace"]);
                if (Convert.ToString(dtIne.Rows[0]["Availability"]) != "")
                {
                    ddlAvilIO.SelectedValue = Convert.ToString(dtIne.Rows[0]["Availability"]);
                }
                ddlAvilIO_SelectedIndexChanged(ddlAvilIO, null);

                if (Convert.ToString(dtIne.Rows[0]["IsMobile"]) != "")
                {
                    ddlAvialMobileIO.SelectedValue = Convert.ToString(dtIne.Rows[0]["IsMobile"]);
                }
                ddlAvialMobileIO_SelectedIndexChanged(ddlAvilIO, null);

                if (Convert.ToString(dtIne.Rows[0]["MobileReason"]) != "")
                {
                    ddlResoneMobileIN.SelectedValue = Convert.ToString(dtIne.Rows[0]["MobileReason"]);
                }
                if (Convert.ToString(dtIne.Rows[0]["Relation"]) != "")
                {
                    ddlRelationIN.SelectedValue = Convert.ToString(dtIne.Rows[0]["Relation"]);
                }


                if (Convert.ToString(dtIne.Rows[0]["Implementer"]) != "")
                {
                    ddlImplementerIo.SelectedValue = Convert.ToString(dtIne.Rows[0]["Implementer"]);
                }
                if (Convert.ToString(dtIne.Rows[0]["JointVisit"]) != "")
                {
                    ddlJoinIo.SelectedValue = Convert.ToString(dtIne.Rows[0]["JointVisit"]);
                }

                txtCMobileIO.Text = Convert.ToString(dtIne.Rows[0]["MobileConfirm"]);
                string TextMeeeting = "";
                if (Convert.ToString(dtIne.Rows[0]["Awareness"]) != "")
                {
                    string[] meeting = Convert.ToString(dtIne.Rows[0]["Awareness"]).Split(',');

                    foreach (string s in meeting)
                    {
                        foreach (ListItem item in chkAwarenessIo.Items)
                        {
                            if (item.Value == s)
                            {
                                item.Selected = true;
                                TextMeeeting += item.Text + ",";
                            }
                        }
                    }
                }
                if (TextMeeeting.Length > 0)
                {
                    TextMeeeting = TextMeeeting.Substring(0, TextMeeeting.LastIndexOf(","));
                }
                txtAwarenessIo.Text = TextMeeeting;
                txtMobileIO.Text = Convert.ToString(dtIne.Rows[0]["Mobile"]);
                txtIGovtID.Text = Convert.ToString(dtIne.Rows[0]["GovtID"]);
                txtSamgra.Text = Convert.ToString(dtIne.Rows[0]["SamgraID "]);
                if (dtIne.Rows[0]["DOB"].ToString() != "")
                {
                    if (Convert.ToDateTime(dtIne.Rows[0]["DOB"]).ToString("dd-MM-yyyy") != "01-01-0001")
                    {
                        txtBDate.Text = Convert.ToDateTime(dtIne.Rows[0]["DOB"]).ToString("dd/MM/yyyy");
                        //txtBDate_TextChanged(txtBDate, null);
                        DivI5.Visible = true;

                    }
                }
                if (Convert.ToInt32(ddlDOproof.SelectedValue) == 6)
                {
                    ddlDOproof_SelectedIndexChanged(ddlDOproof, null);
                }
                txtOther.Text = Convert.ToString(dtIne.Rows[0]["Other"]);
                lblEditActivtive.Text = Convert.ToString(dtIne.Rows[0]["ActivityDate"]);
            }

        }
        if (Convert.ToInt32(ddlStatus.SelectedValue) == 3)
        {
            dvEnrollment.Visible = true;
            ddlEotherVillage.SelectedIndex = 0;
            //  ddlESchool.SelectedIndex = 0;
            ddlFromStatus.SelectedIndex = 0;
            txtErollmentDate.Text = "";
            txtEvillage.Text = "";
            txtSchoolar.Text = "";
            txtSschool.Text = "";
            ddlClass.SelectedIndex = 0;
            txtErollmentDate.Text = "";
            DivE3.Visible = false;
            DivE4.Visible = false;
            DivE2.Visible = false;
            div28.Visible = true;
            div29.Visible = true;
            Div30.Visible = true;
            if (Session["StateCode"].ToString() == "8")
            {


                DivE12.Visible = true;
            }
            else
            {
                DivE13.Visible = true;
            }
            string strQry = " select *  from [tblDTDMobileActivity]   where IsActive=0 and [GUIDDTDMobileActivity]='" + lblDtdUniqid.Text + "' and ActivityStatus=" + lblEnrollId.Text + " ";


            DataTable dtIne = objMain.LoadData(strQry);
            if (dtIne.Rows.Count > 0)
            {

                ddlEotherVillage.SelectedValue = Convert.ToString(dtIne.Rows[0]["VillageOptionID"]);
                ddlEFOtherVillage_SelectedIndexChanged(ddlFo, null);
                txtEvillage.Text = Convert.ToString(dtIne.Rows[0]["SchoolOrVillageName"]);
                txtSschool.Text = Convert.ToString(dtIne.Rows[0]["otherSchoolName"]);
                if (Convert.ToString(dtIne.Rows[0]["SchoolCode"]).Length > 5)
                {
                    ddlESchool.SelectedValue = Convert.ToString(dtIne.Rows[0]["SchoolCode"]);
                }
                lblGuID.Text = Convert.ToString(dtIne.Rows[0]["GUIDDTDMobileActivity"]);
                ddlFromStatus.SelectedValue = Convert.ToString(dtIne.Rows[0]["FromPanding6"]);
                ddlCategory.SelectedValue = Convert.ToString(dtIne.Rows[0]["EnrollmentCategory"]);
                if (Convert.ToString(dtIne.Rows[0]["EnrollmentCategory"]) == "99")
                {
                    ddlCategory_SelectedIndexChanged(ddlCategory, null);
                }
                txtSchoolar.Text = Convert.ToString(dtIne.Rows[0]["ScholarNo"]);
                ddlClass.SelectedValue = Convert.ToString(dtIne.Rows[0]["ClassofEnrollment"]);

                if (Convert.ToString(dtIne.Rows[0]["Availability"]) != "")
                {
                    ddlAvilEO.SelectedValue = Convert.ToString(dtIne.Rows[0]["Availability"]);
                }
                ddlAvilEO_SelectedIndexChanged(ddlAvilIO, null);

                if (Convert.ToString(dtIne.Rows[0]["IsMobile"]) != "")
                {
                    ddlAvialMobileEO.SelectedValue = Convert.ToString(dtIne.Rows[0]["IsMobile"]);
                }
                ddlAvialMobileEO_SelectedIndexChanged(ddlAvilIO, null);
                txtMobileEO.Text = Convert.ToString(dtIne.Rows[0]["Mobile"]);


                if (Convert.ToString(dtIne.Rows[0]["Relation"]) != "")
                {
                    ddlRelationIN.SelectedValue = Convert.ToString(dtIne.Rows[0]["Relation"]);
                }


                if (Convert.ToString(dtIne.Rows[0]["Implementer"]) != "")
                {
                    ddlImplementerEo.SelectedValue = Convert.ToString(dtIne.Rows[0]["Implementer"]);
                }
                if (Convert.ToString(dtIne.Rows[0]["JointVisit"]) != "")
                {
                    ddlJoinEo.SelectedValue = Convert.ToString(dtIne.Rows[0]["JointVisit"]);
                }

                txtCMobileIO.Text = Convert.ToString(dtIne.Rows[0]["MobileConfirm"]);
                string TextMeeeting = "";
                if (Convert.ToString(dtIne.Rows[0]["Awareness"]) != "")
                {
                    string[] meeting = Convert.ToString(dtIne.Rows[0]["Awareness"]).Split(',');

                    foreach (string s in meeting)
                    {
                        foreach (ListItem item in chkAwarenessEo.Items)
                        {
                            if (item.Value == s)
                            {
                                item.Selected = true;
                                TextMeeeting += item.Text + ",";
                            }
                        }
                    }
                }
                if (TextMeeeting.Length > 0)
                {
                    TextMeeeting = TextMeeeting.Substring(0, TextMeeeting.LastIndexOf(","));
                }
                txtAwarenessEo.Text = TextMeeeting;

                if (Convert.ToString(dtIne.Rows[0]["MobileReason"]) != "")
                {
                    ddlResoneMobileEN.SelectedValue = Convert.ToString(dtIne.Rows[0]["MobileReason"]);
                }
                if (Convert.ToString(dtIne.Rows[0]["Relation"]) != "")
                {
                    ddlRelationEN.SelectedValue = Convert.ToString(dtIne.Rows[0]["Relation"]);
                }
                txtEGovtID.Text = Convert.ToString(dtIne.Rows[0]["GovtID"]);
                txtEsamgranID.Text = Convert.ToString(dtIne.Rows[0]["SamgraID "]);
                if (Convert.ToString(dtIne.Rows[0]["ClassofEnrollment"]) == "18")
                {
                    ddlClass_SelectedIndexChanged(ddlClass, null);
                }
                if (Convert.ToString(dtIne.Rows[0]["DateofEnrollment"]) != "")
                {
                    if (Convert.ToDateTime(dtIne.Rows[0]["DateofEnrollment"]).ToString("dd-MM-yyyy") != "01-01-1900")
                    {
                        txtErollmentDate.Text = Convert.ToDateTime(dtIne.Rows[0]["DateofEnrollment"]).ToString("dd/MM/yyyy");

                    }

                }


            }

        }
        if (ddlStatus.SelectedIndex > 0)
        {
            MpexdrFollowup.Show();
        }


    }
    //protected void txtBDate_TextChanged(object sender, EventArgs e)
    //{
    //    ModalPopupExtender.Show();
    //    DivI5.Visible = true;
    //    MpexdrFollowup.Show();
    //}
    protected void lnkEnrool_OnClick(object sender, EventArgs e)
    {
        SqlParameter[] parm = new SqlParameter[]
            {
       new SqlParameter("@villagecode",   ddlVilage.SelectedValue ),
              new SqlParameter("@Flag","1"),

                 };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertActivityDTD2022", parm);

        SqlParameter[] parm1 = new SqlParameter[]
            {
       new SqlParameter("@villagecode",   ddlVilage.SelectedValue ),
              new SqlParameter("@Flag","2"),

                 };
        DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertActivityDTD2022", parm1);

        DataTable dataTable = ds.Tables[0];
        Session["EditTB"] = ds.Tables[1];
        //string  strQry = " select [mst5village].EGVillagecode + '-' +RIGHT('0000' +  convert(varchar,serial), 4) as UniqueId,UniqueCode,RIGHT('0000' +  convert(varchar,serial), 4) as  UniqueIdNew,ActivityStatus as Status,HHNo,ChildName,FathersName from  [tblDTD] inner join mst5Village on mst5village.villagecode=tblDTD.villagecode or tblDTD.villagecode=mst5village.OldUniqueCode    or tblDTD.villagecode=mst5village.RefVillageCode   where  tblDTD.Status='1' and mst5village.Villagecode= '" + ddlVilage.SelectedValue + "'    and " + DateTime.Today.Year + " - (YEAR(SurvayDate)-isnull(AgeAson,0))>=6  and (" + DateTime.Today.Year + " - (YEAR(SurvayDate)-isnull(AgeAson,0))<=14  ) and EduationStatus in(2,3,99)   and EnrollStatus=1 and DeleteFlag<>2";

        //  DataTable dataTable = objMain.LoadData(strQry);

        if (dataTable != null)
        {
            if (dataTable.Rows.Count > 0)
            {
                this.Gv_Display.DataSource = dataTable;
                this.Gv_Display.DataBind();
                Session["D2dBind"] = dataTable;
            }
        }
        this.txtSearch.Text = "";
        ModalPopupExtender.Show();
        ModalPopupExtender1.Hide();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {

        bool InsertD2d = false;

        Int32 Flag = 0;
        string SamgraID = "";
        string GovtID = "";
        if (txtGovtID.Text.Trim() != "")
        {
            GovtID = txtGovtID.Text;
        }
        if (txtSamgraID.Text.Trim() != "")
        {
            SamgraID = Convert.ToString(txtSamgraID.Text);
        }
        string UNICOde = objMain.Generate_RandomString(15);


        if (lblEnrollId.Text == "1")
        {
            #region InsertFollow

            if (Session["StateCode"].ToString() == "8")
            {
                if (ddlAvaiFO.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Availability of Adhar Card')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;


                }
                if (Convert.ToInt32(ddlAvaiFO.SelectedValue) == 1)
                {
                    //if (txtGovtID.Text == "")
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Govt ID')</script>", false);
                    //    ModalPopupExtender.Show();
                    //    MpexdrFollowup.Show();
                    //    return;

                    //}
                }

            }
            else
            {
                if (ddlAvaiFO.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Availability of Samargra ID')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }
                if (Convert.ToInt32(ddlAvaiFO.SelectedValue) == 1)
                {
                    if (txtSamgraID.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Samargra ID')</script>", false);
                        ModalPopupExtender.Show();
                        MpexdrFollowup.Show();
                        return;

                    }
                    if (txtSamgraID.Text.Trim().Length >= 8)
                    {

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Correct Samargra ID')</script>", false);
                        ModalPopupExtender.Show();
                        MpexdrFollowup.Show();
                        return;
                    }
                }

            }


            if (ddlAvialMobile.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Availability of Mobile No')</script>", false);
                ModalPopupExtender.Show();
                MpexdrFollowup.Show();
                return;


            }


            if (Convert.ToInt32(ddlAvialMobile.SelectedValue) == 1)
            {
                if (txtMobileFO.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Mobile No')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;

                }
                if (txtMobileFO.Text.Trim().Length == 10)
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Correct Mobile No..')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }
                if (txtCMobileFO.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Confirm Mobile No. .')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }
                if (txtMobileFO.Text != txtCMobileFO.Text)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mobile Number did not Match.')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }

            }
            if (Convert.ToInt32(ddlAvialMobile.SelectedValue) == 2)
            {
                if (ddlResoneMobileFo.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select  Reason for not sharing mobile number')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }
            }
            if (ddlFo.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Reasons')</script>", false);
                ModalPopupExtender.Show();
                MpexdrFollowup.Show();
                return;


            }
            if (ddlImplementerFo.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select implementer ')</script>", false);
                ModalPopupExtender.Show();
                MpexdrFollowup.Show();
                return;


            }

            if (txtFoAlternateMobile.Text.Length > 2)
            {

                if (txtFoAlternateMobile.Text.Trim().Length == 10)
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enterAlternate Mobile Number')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }

                if (txtFoOwnerRelationChild.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Alternate mobile Owner Name')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;

                }
                if (txtRelation.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Relation with Child')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;

                }
            }
            string ab = "";
            foreach (ListItem item in chkAwarenessFo.Items)
            {
                if (item.Selected)
                {

                    ab += "" + item.Value + "" + ",";

                }
            }
            if (ab.Length > 0)
            {
                ab = ab.Substring(0, ab.LastIndexOf(","));
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Enrolment Awareness')</script>", false);
                ModalPopupExtender.Show();
                MpexdrFollowup.Show();
                return;
            }
            if (Convert.ToInt32(ddlFo.SelectedValue) == 2)
            {
                if (ddlEnrolmentCategory.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Enrolment Category')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;


                }
            }
            if (ddlRelationFo.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Relationship of Respondent')</script>", false);
                ModalPopupExtender.Show();
                MpexdrFollowup.Show();
                return;


            }
            if (Convert.ToInt32(ddlFo.SelectedValue) == 1)
            {
                if (ddlOtherVillage.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Village')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;

                }
            }

            if (Convert.ToInt32(ddlOtherVillage.SelectedValue) == 2)
            {
                if (txtOtherVillage.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter other Village')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;

                }
            }
            if (Convert.ToInt32(ddlFo.SelectedValue) == 4)
            {
                if (txtOtherResone.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Other Reason detail')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;

                }
            }

            string abg = "";
            foreach (ListItem item in chktBanding.Items)
            {
                if (item.Selected)
                {

                    abg += "" + item.Value + "" + ",";

                }
            }
            if (abg.Length > 0)
            {
                abg = abg.Substring(0, abg.LastIndexOf(","));
            }
            if (Convert.ToInt32(ddlFo.SelectedValue) == 1)
            {
                if (ddlSession.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Enrolment Session')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;

                }
                if (Convert.ToInt32(ddlDocAva.SelectedValue) == 2)
                {
                    if (abg.Length > 0)
                    {
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select pending document')</script>", false);
                        ModalPopupExtender.Show();
                        MpexdrFollowup.Show();
                        return;

                    }
                }
            }
            SqlParameter[] cmdParameters = new SqlParameter[]
                {
                        new SqlParameter("@UniqueCode", lblD2dUniqeCode.Text ),
                        new SqlParameter("@ActivityStatus", lblEnrollId.Text),
                        new SqlParameter("@TBorFC", lblRtbFc.Text  ),
                        new SqlParameter("@ActivityDate",  DateTime.Now.ToString("yyyy-MM-dd")),
                        new SqlParameter("@GovtID", GovtID),
                        new SqlParameter("@SamgraID", SamgraID),
                        new SqlParameter("@SchoolCode", ddlSchool.SelectedValue ),

                        new SqlParameter("@SchoolOrVillageName", txtOtherVillage.Text),
                        new SqlParameter("@FollowUPID", ddlFo.SelectedValue),
                        new SqlParameter("@IneligibleID", "0" ),

                         new SqlParameter("@Months","0"),
                        new SqlParameter("@DOB", DBNull.Value),
                                new SqlParameter("@DOBproof", "0"),
                        new SqlParameter("@Other","" ),


                          new SqlParameter("@FromPanding6","0"),
                        new SqlParameter("@EnrollmentCategory","0"),
                             new SqlParameter("@ScholarNo", ""),
                        new SqlParameter("@ClassofEnrollment","0"),
                             new SqlParameter("@DateofEnrollment", DBNull.Value),
                        new SqlParameter("@CreateBy", Session["username"].ToString()),
                        new SqlParameter("@GUIDDTDMobileActivity", UNICOde),
                        new SqlParameter("@VillageOptionID", ddlOtherVillage.SelectedValue),
                          new SqlParameter("@otherSchoolName", txtOtherSchool.Text ),
                        new SqlParameter("@Fyear", Session["FinYear"].ToString() ),
                         new SqlParameter("@Flag", "1" ),
                            new SqlParameter("@Enrollmentother", "" ),
                        new SqlParameter("@Classother", ""),
                         new SqlParameter("@Migrationplace", "0" ),

                             new SqlParameter("@AvilSG", ddlAvaiFO.SelectedValue ),
                        new SqlParameter("@aviMobile", ddlAvialMobile.SelectedValue),
                         new SqlParameter("@moibleNo", txtMobileFO.Text ),
                         new SqlParameter("@FollowUpReason", txtOtherResone.Text ),
                           new SqlParameter("@MobileReason", ddlResoneMobileFo.SelectedValue),
                            new SqlParameter("@Relation",ddlRelationFo.SelectedValue ),

                             new SqlParameter("@MobileConfirm", txtCMobileFO.Text ),
                             new SqlParameter("@Implementer", ddlImplementerFo.SelectedValue),
                         new SqlParameter("@JointVisit", ddlJoinFo.Text ),

                            new SqlParameter("@Awareness",ab ),
                           new SqlParameter("@EnrollCategory",ddlEnrolmentCategory.SelectedValue ),
                              new SqlParameter("@AlternateMobileNumber",txtFoAlternateMobile.Text ),
                           new SqlParameter("@AlternatemobileOwneName",txtFoOwnerRelationChild.Text ),
                             new SqlParameter("@RelationChild",txtRelation.Text ),
                               new SqlParameter("@ReadyCBL",rblFoCBL.SelectedValue ),
                               new SqlParameter("@FCName",Session["ContactUser"].ToString() ),
                                 new SqlParameter("@TBCode",Session["TBName"].ToString()),
                                 new SqlParameter("@EnrollSession",ddlSession.SelectedValue ),
                                   new SqlParameter("@DocAvail",ddlDocAva.SelectedValue ),
                                     new SqlParameter("@DocPending",abg ),


                };
            Int32 icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateDTDMobileActivityNew", cmdParameters);
            #endregion
        }

        if (lblEnrollId.Text == "2")
        {

            #region Ineligible



            if (Session["StateCode"].ToString() == "8")
            {
                if (ddlAvilIO.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Availability of Adhar Card')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;


                }
                if (Convert.ToInt32(ddlAvilIO.SelectedValue) == 1)
                {
                    //if (txtIGovtID.Text == "")
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Govt ID')</script>", false);
                    //    ModalPopupExtender.Show();
                    //    MpexdrFollowup.Show();
                    //    return;

                    //}
                }

            }
            else
            {
                if (ddlAvilIO.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Availability of Samargra ID')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }
                if (Convert.ToInt32(ddlAvilIO.SelectedValue) == 1)
                {
                    if (txtSamgra.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Samargra ID')</script>", false);
                        ModalPopupExtender.Show();
                        MpexdrFollowup.Show();
                        return;

                    }
                    if (txtSamgra.Text.Trim().Length >= 8)
                    {

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Correct Samargra ID')</script>", false);
                        ModalPopupExtender.Show();
                        MpexdrFollowup.Show();
                        return;
                    }
                }

            }


            if (ddlAvialMobileIO.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Availability of Mobile No')</script>", false);
                ModalPopupExtender.Show();
                MpexdrFollowup.Show();
                return;


            }

            if (ddlRelationIN.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Relationship of Respondent')</script>", false);
                ModalPopupExtender.Show();
                MpexdrFollowup.Show();
                return;


            }
            if (Convert.ToInt32(ddlAvialMobileIO.SelectedValue) == 1)
            {
                if (txtMobileIO.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Mobile No')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;

                }
                if (txtMobileIO.Text.Trim().Length == 10)
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Correct Mobile No..')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }
                if (txtCMobileIO.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Confirm Mobile No. .')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }
                if (txtMobileIO.Text != txtCMobileIO.Text)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mobile Number did not Match.')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }

            }
            if (txtIoAlternateMobile.Text.Length > 2)
            {

                if (txtIoAlternateMobile.Text.Trim().Length == 10)
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enterAlternate Mobile Number')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }

                if (txtIoOwnerRelationChild.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Alternate mobile Owner Name')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;

                }
                if (txtIORelation.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Relation with Child')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;

                }
            }

            if (ddlImplementerIo.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select implementer ')</script>", false);
                ModalPopupExtender.Show();
                MpexdrFollowup.Show();
                return;


            }

            string ab = "";
            foreach (ListItem item in chkAwarenessIo.Items)
            {
                if (item.Selected)
                {

                    ab += "" + item.Value + "" + ",";

                }
            }
            if (ab.Length > 0)
            {
                ab = ab.Substring(0, ab.LastIndexOf(","));
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Enrolment Awareness')</script>", false);

                ModalPopupExtender.Show();
                MpexdrFollowup.Show();
                return;
            }
            if (ddlIReasons.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Reasons')</script>", false);
                ModalPopupExtender.Show();
                MpexdrFollowup.Show();
                return;


            }

            if (Convert.ToInt32(ddlAvialMobileIO.SelectedValue) == 2)
            {
                if (ddlResoneMobileIN.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select  Reason for not sharing mobile number')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }
            }


            if (Convert.ToInt32(ddlIReasons.SelectedValue) == 1)
            {
                if (ddlMonth.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Month')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }
                if (ddlMigration.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Migration Place')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;


                }
            }

            if (Convert.ToInt32(ddlIReasons.SelectedValue) == 2 || Convert.ToInt32(ddlIReasons.SelectedValue) == 3)
            {
                if (txtBDate.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select birthday')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }

            }
            if (txtBDate.Text != "")
            {
                string BithDate = Convert.ToDateTime(txtBDate.Text).ToString("yyyy-MM-dd");
                string ActivitDay = Convert.ToDateTime(lblEditActivtive.Text).ToString("yyyy-MM-dd");

                string[] c = txtBDate.Text.Split('/');
                string ChildDOB = c[2] + '-' + c[1] + '-' + c[0];

                Int32 Age = DateTime.Today.Year - Convert.ToInt32(c[2]);
                if (Convert.ToInt32(ddlIReasons.SelectedValue) == 2)
                {
                    if (Age > 14)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('child age always greater than 14 years')</script>", false);
                        ModalPopupExtender.Show();
                        MpexdrFollowup.Show();
                    }
                }

                if (Convert.ToInt32(ddlIReasons.SelectedValue) == 3)
                {
                    if (Age > 5)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Age must be less than 5 years.')</script>", false);
                        ModalPopupExtender.Show();
                        MpexdrFollowup.Show();
                    }
                }
                if (Convert.ToDateTime(ActivitDay) <= Convert.ToDateTime(BithDate))
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter birthday less then activity Date')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }
                if (ddlDOproof.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Dob Proof')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;


                }
            }

            if (txtIGovtID.Text.Trim() != "")
            {
                GovtID = txtIGovtID.Text;
            }
            if (txtSamgra.Text.Trim() != "")
            {
                SamgraID = Convert.ToString(txtSamgra.Text);
            }

            DateTime dob = DateTime.MinValue;
            if (txtBDate.Text != "")
            {
                dob = Convert.ToDateTime(txtBDate.Text.ToString());
            }
            if (lblEditRow.Text == "0")
            {
                Flag = 2;
            }
            else
            {
                Flag = 4;
                UNICOde = lblGuID.Text;
            }
            SqlParameter[] cmdParameters = new SqlParameter[]
                    {
                        new SqlParameter("@UniqueCode", lblD2dUniqeCode.Text ),
                        new SqlParameter("@ActivityStatus", lblEnrollId.Text),
                        new SqlParameter("@TBorFC", lblRtbFc.Text  ),
                        new SqlParameter("@ActivityDate",  DateTime.Now.ToString("yyyy-MM-dd")),
                        new SqlParameter("@GovtID", GovtID),
                        new SqlParameter("@SamgraID", SamgraID),
                        new SqlParameter("@SchoolCode",  "" ),

                        new SqlParameter("@SchoolOrVillageName",  ""),
                        new SqlParameter("@FollowUPID", "0"),
                        new SqlParameter("@IneligibleID",ddlIReasons.SelectedValue  ),

                         new SqlParameter("@Months",ddlMonth.SelectedValue),
                        new SqlParameter("@DOB",dob.ToString("yyyy-MM-dd")),
                                new SqlParameter("@DOBproof", ddlDOproof.SelectedValue),
                        new SqlParameter("@Other",txtOther.Text ),


                          new SqlParameter("@FromPanding6","0"),
                        new SqlParameter("@EnrollmentCategory","0"),
                             new SqlParameter("@ScholarNo", ""),
                        new SqlParameter("@ClassofEnrollment","0"),
                             new SqlParameter("@DateofEnrollment", DBNull.Value),
                        new SqlParameter("@CreateBy", Session["username"].ToString()),
                        new SqlParameter("@GUIDDTDMobileActivity", UNICOde),
                        new SqlParameter("@VillageOptionID", "0"),
                          new SqlParameter("@otherSchoolName", "" ),
                        new SqlParameter("@Fyear", Session["FinYear"].ToString() ),
                         new SqlParameter("@Flag", Flag),
                              new SqlParameter("@Enrollmentother", "" ),
                        new SqlParameter("@Classother", ""),
                         new SqlParameter("@Migrationplace", ddlMigration.SelectedValue ),
                              new SqlParameter("@AvilSG", ddlAvilIO.SelectedValue ),
                        new SqlParameter("@aviMobile", ddlAvialMobileIO.SelectedValue),
                         new SqlParameter("@moibleNo", txtMobileIO.Text ),
                          new SqlParameter("@FollowUpReason", "" ),
                           new SqlParameter("@MobileReason", ddlResoneMobileIN.SelectedValue),
                            new SqlParameter("@Relation",ddlRelationIN.SelectedValue ),
                                  new SqlParameter("@MobileConfirm", txtCMobileIO.Text ),
                             new SqlParameter("@Implementer", ddlImplementerIo.SelectedValue),
                         new SqlParameter("@JointVisit", ddlJoinIo.Text ),

                            new SqlParameter("@Awareness",ab ),
                           new SqlParameter("@EnrollCategory","0" ),
                             new SqlParameter("@AlternateMobileNumber",txtIoAlternateMobile.Text ),
                           new SqlParameter("@AlternatemobileOwneName",txtIoOwnerRelationChild.Text ),
                             new SqlParameter("@RelationChild",txtIORelation.Text ),
                               new SqlParameter("@ReadyCBL","0" ),
                                 new SqlParameter("@FCName",Session["ContactUser"].ToString() ),
                                 new SqlParameter("@TBCode",Session["TBName"].ToString()),
                                   new SqlParameter("@EnrollSession","0" ),
                                   new SqlParameter("@DocAvail","0"),
                                     new SqlParameter("@DocPending","" ),
                    };
            Int32 icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateDTDMobileActivityNew", cmdParameters);
            #endregion
        }

        if (lblEnrollId.Text == "3")
        {
            #region Enrollment

            if (Session["StateCode"].ToString() == "8")
            {
                if (ddlAvilEO.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Availability of Adhar Card')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;


                }
                if (Convert.ToInt32(ddlAvilEO.SelectedValue) == 1)
                {
                    //if (txtEGovtID.Text == "")
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Govt ID')</script>", false);
                    //    ModalPopupExtender.Show();
                    //    MpexdrFollowup.Show();
                    //    return;

                    //}
                }

            }
            else
            {
                if (ddlAvilEO.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Availability of Samargra ID')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }
                if (Convert.ToInt32(ddlAvilEO.SelectedValue) == 1)
                {
                    if (txtEsamgranID.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Samargra ID')</script>", false);
                        ModalPopupExtender.Show();
                        MpexdrFollowup.Show();
                        return;

                    }

                    if (txtEsamgranID.Text.Trim().Length >= 8)
                    {

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Correct Samargra ID')</script>", false);
                        ModalPopupExtender.Show();
                        MpexdrFollowup.Show();
                        return;
                    }
                }

            }


            if (ddlAvialMobileEO.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Availability of Mobile No')</script>", false);
                ModalPopupExtender.Show();
                MpexdrFollowup.Show();
                return;


            }

            if (ddlRelationEN.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Relationship of Respondent')</script>", false);
                ModalPopupExtender.Show();
                MpexdrFollowup.Show();
                return;


            }
            if (Convert.ToInt32(ddlAvialMobileEO.SelectedValue) == 1)
            {
                if (txtMobileEO.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Mobile No')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;

                }
                if (txtMobileEO.Text.Trim().Length == 10)
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Correct Mobile No..')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }
                if (txtCMobileEO.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Confirm Mobile No. .')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }
                if (txtMobileEO.Text != txtCMobileEO.Text)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mobile Number did not Match.')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }
            }

            if (txtEnAlternateMobile.Text.Length > 2)
            {

                if (txtEnAlternateMobile.Text.Trim().Length == 10)
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enterAlternate Mobile Number')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }

                if (txtEnOwnerRelationChild.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Alternate mobile Owner Name')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;

                }
                if (txtEnRelation.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Relation with Child')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;

                }
            }

            if (ddlImplementerEo.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select implementer ')</script>", false);
                ModalPopupExtender.Show();
                MpexdrFollowup.Show();
                return;


            }

            string ab = "";
            foreach (ListItem item in chkAwarenessEo.Items)
            {
                if (item.Selected)
                {

                    ab += "" + item.Value + "" + ",";

                }
            }
            if (ab.Length > 0)
            {
                ab = ab.Substring(0, ab.LastIndexOf(","));
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Enrolment Awareness')</script>", false);
                ModalPopupExtender.Show();
                MpexdrFollowup.Show();
                return;
            }
            if (Convert.ToInt32(ddlAvialMobileEO.SelectedValue) == 2)
            {
                if (ddlResoneMobileEN.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select  Reason for not sharing mobile number')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }
            }
            if (ddlFromStatus.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select From Status')</script>", false);
                ModalPopupExtender.Show();
                MpexdrFollowup.Show();
                return;


            }
            //if (ddlCategory.SelectedIndex <= 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Category')</script>", false);
            //    ModalPopupExtender.Show();
            //    MpexdrFollowup.Show();
            //    return;
            //}
            if (txtSchoolar.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Schoolar')</script>", false);
                ModalPopupExtender.Show();
                MpexdrFollowup.Show();
                return;
            }
            if (Convert.ToInt32(ddlFromStatus.SelectedValue) == 1)
            {

                if (ddlClass.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Class')</script>", false);
                    ModalPopupExtender.Show();
                    MpexdrFollowup.Show();
                    return;
                }
            }
            DateTime dob = DateTime.MinValue;
            if (txtErollmentDate.Text != "")
            {
                dob = Convert.ToDateTime(txtErollmentDate.Text);
            }
            if (lblEditRow.Text == "0")
            {
                Flag = 3;
            }
            else
            {
                Flag = 5;
                UNICOde = lblGuID.Text;
            }
            if (txtEGovtID.Text.Trim() != "")
            {
                GovtID = txtEGovtID.Text;
            }
            if (txtEsamgranID.Text.Trim() != "")
            {
                SamgraID = Convert.ToString(txtEsamgranID.Text);
            }
            SqlParameter[] cmdParameters = new SqlParameter[]
                    {
                        new SqlParameter("@UniqueCode", lblD2dUniqeCode.Text ),
                        new SqlParameter("@ActivityStatus", lblEnrollId.Text),
                        new SqlParameter("@TBorFC", lblRtbFc.Text  ),
                        new SqlParameter("@ActivityDate",  DateTime.Now.ToString("yyyy-MM-dd")),
                        new SqlParameter("@GovtID",GovtID),
                        new SqlParameter("@SamgraID",SamgraID),
                        new SqlParameter("@SchoolCode", ddlESchool.SelectedValue ),

                        new SqlParameter("@SchoolOrVillageName",  txtEvillage.Text),
                        new SqlParameter("@FollowUPID", "0"),
                        new SqlParameter("@IneligibleID", "0"  ),

                         new SqlParameter("@Months", "0"),
                        new SqlParameter("@DOB", DBNull.Value),
                                new SqlParameter("@DOBproof", ddlDOproof.SelectedValue),
                        new SqlParameter("@Other","" ),


                          new SqlParameter("@FromPanding6",ddlFromStatus.SelectedValue),
                        new SqlParameter("@EnrollmentCategory",ddlCategory.SelectedValue),
                             new SqlParameter("@ScholarNo", txtSchoolar.Text),
                        new SqlParameter("@ClassofEnrollment",ddlClass.SelectedValue),
                             new SqlParameter("@DateofEnrollment", dob.ToString("yyyy-MM-dd")),
                        new SqlParameter("@CreateBy", Session["username"].ToString()),
                        new SqlParameter("@GUIDDTDMobileActivity", UNICOde),
                        new SqlParameter("@VillageOptionID", ddlEotherVillage.SelectedValue),
                          new SqlParameter("@otherSchoolName", txtSschool.Text ),
                        new SqlParameter("@Fyear", Session["FinYear"].ToString() ),
                         new SqlParameter("@Flag", Flag ),
                              new SqlParameter("@Enrollmentother", txtEnrommentOther.Text),
                        new SqlParameter("@Classother",ddlClass.SelectedValue),
                         new SqlParameter("@Migrationplace", "0" ),
                             new SqlParameter("@AvilSG", ddlAvilEO.SelectedValue ),
                        new SqlParameter("@aviMobile", ddlAvialMobileEO.SelectedValue),
                         new SqlParameter("@moibleNo", txtMobileEO.Text ),
                           new SqlParameter("@FollowUpReason", "" ),
                           new SqlParameter("@MobileReason", ddlResoneMobileEN.SelectedValue),
                            new SqlParameter("@Relation",ddlRelationEN.SelectedValue ),
                                  new SqlParameter("@MobileConfirm", txtCMobileEO.Text ),
                             new SqlParameter("@Implementer", ddlImplementerEo.SelectedValue),
                         new SqlParameter("@JointVisit", ddlJoinEo.Text ),

                            new SqlParameter("@Awareness",ab ),
                           new SqlParameter("@EnrollCategory","0" ),
                                 new SqlParameter("@AlternateMobileNumber",txtEnAlternateMobile.Text ),
                           new SqlParameter("@AlternatemobileOwneName",txtEnOwnerRelationChild.Text ),
                             new SqlParameter("@RelationChild",txtEnRelation.Text ),
                               new SqlParameter("@ReadyCBL",rblEnCBL.SelectedValue),
                               new SqlParameter("@FCName",Session["ContactUser"].ToString() ),
                                 new SqlParameter("@TBCode",Session["TBName"].ToString()),
                                 new SqlParameter("@EnrollSession","0" ),
                                   new SqlParameter("@DocAvail","0"),
                                     new SqlParameter("@DocPending","" ),
                    };
            Int32 icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateDTDMobileActivityNew", cmdParameters);
            #endregion
        }

      
        if (InsertD2d == true)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
            ModalPopupExtender.Show();
        }
    }

    public void SaveData()
    {
        if (this.ddlRemark.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Remark')</script>", false);
            this.ddlRemark.Focus();
            return;
        }
        if (this.ddlVilage.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Village')</script>", false);
            this.ddlRemark.Focus();
            return;
        }
        if (this.ddlUser.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select FC')</script>", false);
            this.ddlRemark.Focus();
            return;
        }
        int GssEnrollRetan = 0;
        int MMEnrollRetan = 0;
        int Comm1EnrollRetan = 0;
        #region Variable
        string commmeeting = "";
        string commmeeting1 = "";
        string commmeeting2 = "";
        string commOther = "";
        string commOther1 = "";
        string GGTbCode = "";
        string Com1TBCode = "";
        string Com2TBCode = "";
        if (ddlGssTbname.SelectedIndex > 0)
        {
            GGTbCode = ddlGssTbname.SelectedValue;
        }
        if (ddltbCom1.SelectedIndex > 0)
        {
            Com1TBCode = ddltbCom1.SelectedValue;
        }
        if (ddltbCom2.SelectedIndex > 0)
        {
            Com2TBCode = ddltbCom2.SelectedValue;
        }
        #region GSS
        foreach (ListItem item in CBL_bookformat.Items)
        {
            if (item.Selected)
            {

                commmeeting += "" + item.Value + "" + ",";
                if (item.Value == "8")
                {
                    commOther = item.Value;

                    txt_bookformatOther.Enabled = true;
                }

            }
        }
        foreach (ListItem item in CBL_bookformatNew.Items)
        {
            if (item.Selected)
            {

                commmeeting1 += "" + item.Value + "" + ",";
                if (item.Value == "99")
                {
                    commOther1 = item.Value;
                    txt_bookformatOther1.Enabled = true;
                }

            }
        }
        foreach (ListItem item in CBL_bookformatNew1.Items)
        {
            if (item.Selected)
            {

                commmeeting2 += "" + item.Value + "" + ",";


            }
        }
        if (commmeeting.Length > 0) { commmeeting = commmeeting.Substring(0, commmeeting.LastIndexOf(",")); }
        if (commmeeting1.Length > 0) { commmeeting1 = commmeeting1.Substring(0, commmeeting1.LastIndexOf(",")); }
        if (commmeeting2.Length > 0) { commmeeting2 = commmeeting2.Substring(0, commmeeting2.LastIndexOf(",")); }
        if (commmeeting.Length > 0 || commmeeting1.Length > 0 || commmeeting2.Length > 0 || TxtGSS_Male.Text != "" || chkcommmetingTB.Checked == true || chkcommmetingFC.Checked == true || TxtGSS_FeMale.Text != "" || rdEnrollMent.Checked == true || rdRetention.Checked == true || txtV1illager.Text != "")
        {

            if (chkcommmetingTB.Checked == true || chkcommmetingFC.Checked == true)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select GSS-TB/FC')</script>", false);


                this.chkcommmetingTB.Focus();
                return;
            }
            if (chkcommmetingTB.Checked == true)
            {
                if (ddlGssTbname.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select GSS TB Name')</script>", false);


                    this.chkcommmetingTB.Focus();
                    return;
                }
            }
            if (rdEnrollMent.Checked == false && rdRetention.Checked == false)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select GSS Enrollment or GSS Retantion')</script>", false);
                return;
            }
            if (commmeeting.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Objective of Meeting')</script>", false);
                this.txt_pbname.Focus();
                return;
            }
            if (commOther == "8")
            {
                if (txt_bookformatOther.Text.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Objective of Meeting Other(Specify) GSS')</script>", false);


                    this.txt_bookformatOther.Focus();
                    txt_bookformatOther.Enabled = true;
                    return;
                }


            }
            else
            {
                txt_bookformatOther.Enabled = false;
            }
            if (commmeeting1.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select GSS Highlights of Discussion')</script>", false);
                this.txt_pbnameNew.Focus();
                return;
            }

            if (commOther1 == "99")
            {
                if (txt_bookformatOther1.Text.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Highlights of Discussion Other(Specify) GSS')</script>", false);


                    this.txt_bookformatOther1.Focus();
                    txt_bookformatOther1.Enabled = true;
                    return;
                }


            }
            else
            {
                txt_bookformatOther1.Enabled = false;
            }
            if (commmeeting2.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select GSS Key Participants')</script>", false);
                this.txt_pbnameNew1.Focus();
                return;
            }
            if (TxtGSS_FeMale.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select GSS Attendance-Female')</script>", false);
                this.TxtGSS_FeMale.Focus();
                return;
            }
            if (TxtGSS_Male.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select GSS Attendance-Male')</script>", false);
                this.TxtGSS_Male.Focus();
                return;
            }

            if (Convert.ToInt32(TxtGSS_FeMale.Text) > 0)
            {
            }
            else
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure GSS Attendance-Female value more than  zero')</script>", false);
                this.TxtGSS_FeMale.Focus();
                return;
            }
            if (Convert.ToInt32(TxtGSS_Male.Text) > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure GSS Attendance-Male value more than  zero')</script>", false);
                this.TxtGSS_Male.Focus();
                return;
            }



            if (txtV1illager.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure People Present OOSC/No. of parents of irregular childeren is more than or equal to zero')</script>", false);


                this.txtV1illager.Focus();
                return;
            }


            Int32 Total = Convert.ToInt32(TxtGSS_FeMale.Text) + Convert.ToInt32(TxtGSS_Male.Text);
            Int32 TotalVIllager = Convert.ToInt32(txtV1illager.Text);
            if (TotalVIllager > Total)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Attendance-Male and GSS Attendance-Female Greater then OOSC')</script> ", false);


                this.txtV1illager.Focus();
                return;
            }
        }
        #endregion

        #region MM
        string Muhula = "";
        string Muhula1 = "";
        string Muhula2 = "";
        string TempMuhulaOther = "";
        string TempMuhulaOther1 = "";
        string MMTbCode = "";
        if (ddlMMTb.SelectedIndex > 0)
        {
            MMTbCode = ddlMMTb.SelectedValue;
        }
        foreach (ListItem item in CBL_Muhula.Items)
        {
            if (item.Selected)
            {

                Muhula += "" + item.Value + "" + ",";
                if (item.Value == "8")
                {
                    TempMuhulaOther = item.Value;
                    txtmOther.Enabled = true;
                }
            }
        }
        foreach (ListItem item in CBL_MuhulaNew.Items)
        {
            if (item.Selected)
            {

                Muhula1 += "" + item.Value + "" + ",";
                if (item.Value == "99")
                {
                    TempMuhulaOther1 = item.Value;
                    txtmOther1.Enabled = true;
                }
            }
        }

        foreach (ListItem item in CBL_MuhulaNew1.Items)
        {
            if (item.Selected)
            {

                Muhula2 += "" + item.Value + "" + ",";

            }
        }
        if (Muhula.Length > 0) { Muhula = Muhula.Substring(0, Muhula.LastIndexOf(",")); }
        if (Muhula1.Length > 0) { Muhula1 = Muhula1.Substring(0, Muhula1.LastIndexOf(",")); }
        if (Muhula2.Length > 0) { Muhula2 = Muhula2.Substring(0, Muhula2.LastIndexOf(",")); }
        if (Muhula.Length > 0 || Muhula1.Length > 0 || Muhula2.Length > 0 || rdEnrollment1.Checked == true || rblmuhulaTb.Checked == true || rblmuhulaFC.Checked == true || rdRetantion1.Checked == true || TxtMM_FeMale.Text != "" || TxtMM_Male.Text != "" || txtVillager2.Text != "")
        {
            //Muhula = Muhula.Substring(0, Muhula.LastIndexOf(","));
            if (rblmuhulaTb.Checked == true || rblmuhulaFC.Checked == true)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Mauhalla Meeting  TB/FC')</script>", false);


                this.rblmuhulaTb.Focus();
                return;
            }
            if (rblmuhulaTb.Checked == true)
            {
                if (ddlMMTb.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select MM TB Name')</script>", false);


                    this.chkcommmetingTB.Focus();
                    return;
                }
            }
            if (rdEnrollment1.Checked == false && rdRetantion1.Checked == false)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select MM Enrollment or MM Retantion')</script>", false);
                return;
            }

            if (Muhula.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select MM Objective of Meeting')</script>", false);
                this.txtMuhala.Focus();
                return;
            }
            if (TempMuhulaOther == "8")
            {
                if (txtmOther.Text.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Objective of Meeting Other(Specify) Mauhalla')</script>", false);


                    this.txtmOther.Focus();
                    txtmOther.Enabled = true;
                    return;
                }


            }
            else
            {
                txtmOther.Enabled = false;
            }
            if (Muhula1.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select MM Highlights of Discussion')</script>", false);
                this.txtMuhalaNew.Focus();
                return;
            }
            if (TempMuhulaOther1 == "99")
            {
                if (txtmOther1.Text.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter MM  Highlights of Discussion  Other(Specify)')</script>", false);


                    this.txtmOther1.Focus();
                    txtmOther1.Enabled = true;
                    return;
                }


            }
            else
            {
                txtmOther1.Enabled = false;
            }
            //if (Muhula2.Length > 0)
            //{

            //}
            //else
            //{
            //    this.txtMuhalaNew1.Focus();
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select MM Key Participants')</script>", false);
            //    return;
            //}
            if (TxtMM_FeMale.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select MM Attendance-Female')</script>", false);
                this.TxtMM_FeMale.Focus();
                return;
            }
            if (TxtMM_Male.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select MM Attendance-Male')</script>", false);
                this.TxtMM_Male.Focus();
                return;
            }

            Int32 TotalMale = Convert.ToInt32(TxtMM_FeMale.Text) + Convert.ToInt32(TxtMM_Male.Text);
            if (TotalMale > 0)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure MM Attendance-Female or Male value more than  zero')</script>", false);
                this.TxtMM_FeMale.Focus();
                return;
            }

            //if (Convert.ToInt32(TxtMM_FeMale.Text) > 0)
            //{
            //}
            //else
            //{

            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure MM Attendance-Female value more than  zero')</script>", false);
            //    this.TxtMM_FeMale.Focus();
            //    return;
            //}
            //if (Convert.ToInt32(TxtMM_Male.Text) > 0)
            //{

            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure MM Attendance-Male value more than  zero')</script>", false);
            //    this.TxtMM_Male.Focus();
            //    return;
            //}

            Int32 TotalVIllager = 0;
            if (txtVillager2.Text != "")
            {
                //ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure MM Present OOSC/No. of parents of irregular childeren is more than or equal to zero')</script>", false);
                //this.txtVillager2.Focus();
                //return;
                TotalVIllager = Convert.ToInt32(txtVillager2.Text);
            }


            Int32 Total = Convert.ToInt32(TxtMM_FeMale.Text) + Convert.ToInt32(TxtMM_Male.Text);

            if (TotalVIllager > Total)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Attendance-Male and MM Attendance-Female Greater then OOSC')</script> ", false);


                this.txtV1illager.Focus();
                return;
            }

        }
        #endregion
        #region COmm1
        string othercom = "";
        string othercom1 = "";
        string othercom2 = "";
        string Tempothercom = "";
        string Tempothercom1 = "";

        foreach (ListItem item in chk_othercom.Items)
        {
            if (item.Selected)
            {

                othercom += "" + item.Value + "" + ",";

                if (item.Value == "8")
                {
                    Tempothercom = item.Value;
                    txtOtherComm.Enabled = true;
                }
            }

        }
        foreach (ListItem item in chk_othercom_New.Items)
        {
            if (item.Selected)
            {

                othercom1 += "" + item.Value + "" + ",";

                if (item.Value == "99")
                {
                    Tempothercom1 = item.Value;
                    txtOtherComm1.Enabled = true;
                }
            }
        }
        foreach (ListItem item in chk_othercom_New1.Items)
        {
            if (item.Selected)
            {
                othercom2 += "" + item.Value + "" + ",";
            }
        }
        if (othercom.Length > 0) { othercom = othercom.Substring(0, othercom.LastIndexOf(",")); }
        if (othercom1.Length > 0) { othercom1 = othercom1.Substring(0, othercom1.LastIndexOf(",")); }
        if (othercom2.Length > 0) { othercom2 = othercom2.Substring(0, othercom2.LastIndexOf(",")); }
        if (Tempothercom.Length > 0 || othercom1.Length > 0 || othercom2.Length > 0 || rdEnrollment2.Checked == true || rblothercommTb.Checked == true || rblothercommfc.Checked == true || rdRetantion2.Checked == true || rpSocialMapping.Checked == true || txtvillager3.Text != "" || TxtCm1_FeMale.Text != "" && TxtCm1_Male.Text != "" || tc1.Text != "")
        {
            //othercom = othercom.Substring(0, othercom.LastIndexOf(","));


            if (rblothercommTb.Checked == true || rblothercommfc.Checked == true)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Other Community Meeting 1-TB/FC')</script>", false);


                this.rblothercommTb.Focus();
                return;
            }
            if (rdEnrollment2.Checked == false && rdRetantion2.Checked == false && rpSocialMapping.Checked == false)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Other Community Meeting 1 Enrollment or Other Community Meeting 1 Retantion')</script>", false);
                this.rdEnrollment2.Focus();
                return;
            }
            if (tc1.Text.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Meeting Name Community Meeting 1')</script>", false);
                this.tc1.Focus();

                return;
            }
            if (rblothercommTb.Checked == true)
            {
                if (ddltbCom1.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select TB Name Community Meeting 1')</script>", false);
                    this.tc1.Focus();

                    return;
                }
            }
            if (othercom.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Community Meeting 1 Objective of Meeting')</script>", false);
                this.txtOtherComminuty.Focus();
                return;
            }
            if (Tempothercom == "8")
            {
                if (txtOtherComm.Text.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Objective of Meeting Other(specify) Community Meeting 1')</script>", false);
                    this.txtOtherComm.Focus();
                    txtOtherComm.Enabled = true;
                    return;
                }

            }
            else
            {
                txtOtherComm.Enabled = false;
            }

            if (othercom1.Length > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Community Meeting 1 Highlights of Discussion')</script>", false);
                this.txtOtherComminutyNew.Focus();
                return;
            }
            if (Tempothercom1 == "99")
            {
                if (txtOtherComm1.Text.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Highlights of Discussion Other(specify) Community Meeting 1')</script>", false);
                    this.txtOtherComm1.Focus();
                    txtOtherComm1.Enabled = true;
                    return;
                }

            }
            else
            {
                txtOtherComm1.Enabled = false;
            }
            //if (othercom2.Length > 0)
            //{

            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Community Meeting 1 Key Participants')</script>", false);
            //    this.txtOtherComminutyNew1.Focus();
            //    return;
            //}
            if (TxtCm1_FeMale.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Community Meeting 1 Attendance-Female')</script>", false);
                this.TxtCm1_FeMale.Focus();
                return;
            }
            if (TxtCm1_Male.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select  Community Meeting 1 Attendance-Male')</script>", false);
                this.TxtCm1_Male.Focus();
                return;

            }
            if (Convert.ToInt32(TxtCm1_FeMale.Text) > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Community Meeting 1 Attendance-Female value more than  zero')</script>", false);
                this.TxtCm1_FeMale.Focus();
                return;
            }
            if (Convert.ToInt32(TxtCm1_Male.Text) > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Community Meeting 1 Attendance-Male value more than  zero')</script>", false);
                this.TxtCm1_Male.Focus();
                return;
            }




            //if (txtvillager3.Text == "")
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure the number of Present OOSC/No. of parents of irregular childeren is more than or equal to zero')</script>", false);


            //    this.txtvillager3.Focus();
            //    return;
            //}

            Int32 TotalVIllager = 0;
            if (txtvillager3.Text != "")
            {
                //ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure MM Present OOSC/No. of parents of irregular childeren is more than or equal to zero')</script>", false);
                //this.txtVillager2.Focus();
                //return;
                TotalVIllager = Convert.ToInt32(txtvillager3.Text);
            }


            Int32 Total = Convert.ToInt32(TxtCm1_FeMale.Text) + Convert.ToInt32(TxtCm1_Male.Text);

            if (TotalVIllager > Total)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Attendance-Male and Community Meeting 1 Attendance-Female Greater then OOSC')</script> ", false);


                this.txtV1illager.Focus();
                return;
            }



        }
        #endregion

        //-----COm2

        #region Comm2
        string othercom11 = "";
        string Tempothercom11 = "";
        foreach (ListItem item in chk_c2.Items)
        {
            if (item.Selected)
            {

                othercom11 += "" + item.Value + "" + ",";

                if (item.Value == "8")
                {
                    Tempothercom11 = item.Value;
                    txtoC111.Enabled = true;
                }
            }

        }
        if (othercom11.Length > 0)
        {
            othercom11 = othercom11.Substring(0, othercom11.LastIndexOf(","));
        }
        if (othercom11.Length > 0 || txtAtt1.Text != "" || txtoC1.Text != "" || rblc1.Checked == true || rblc2.Checked == true)
        {


            if (rblc1.Checked == true || rblc2.Checked == true)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Other Community Meeting2-TB/FC')</script>", false);


                this.rblc1.Focus();
                return;
            }
            if (txtoC1.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Other Contact Community Meeting2 ')</script>", false);


                this.txtoC1.Focus();
                return;
            }

            if (rblc1.Checked == true)
            {
                if (ddltbCom2.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select TB Name Community Meeting 2')</script>", false);
                    this.tc1.Focus();

                    return;
                }
            }
            if (othercom11.Length > 0)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Community Meeting 2 Objective of Meeting')</script>", false);

                return;
            }
            if (txtAtt1.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure People Attended is more than or equal to zero')</script>", false);


                this.txtAtt1.Focus();
                return;
            }

            if (Tempothercom11 == "8")
            {
                if (txtoC111.Text.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Other(specify) Community Meeting2')</script>", false);


                    this.txtoC111.Focus();
                    txtoC111.Enabled = true;
                    return;
                }

            }
            else
            {
                txtoC111.Enabled = false;
            }

        }

        #endregion
        //---
        string Ambition = "";
        string AmbitionComOther = "";
        foreach (ListItem item in chk_comm.Items)
        {
            if (item.Selected)
            {

                Ambition += "" + item.Value + "" + ",";
                if (item.Value == "8")
                {
                    AmbitionComOther = item.Value;
                    txtOtherCon.Enabled = true;
                }
            }
        }




        string AmbitionComm = "";
        string OtherAmbitionComm = "";

        foreach (ListItem item in chk_chkconn.Items)
        {
            if (item.Selected)
            {

                AmbitionComm += "" + item.Value + "" + ",";
                if (item.Text == "Others")
                {
                    OtherAmbitionComm = item.Value;
                    txt_con_other.Enabled = true;
                }
            }

        }
        if (AmbitionComm.Length > 0)
        {
            AmbitionComm = AmbitionComm.Substring(0, AmbitionComm.LastIndexOf(","));
        }
        if (Ambition.Length > 0)
        {
            Ambition = Ambition.Substring(0, Ambition.LastIndexOf(","));
        }
        if (AmbitionComm.Length > 0 || Ambition.Length > 0 || rblcommtb.Checked == true || rblCommFC.Checked == true)
        {

            if (rblcommtb.Checked == true || rblCommFC.Checked == true)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select TB or FC Community Contact')</script>", false);


                this.rblcommtb.Focus();
                return;
            }
            if (AmbitionComm.Length > 0)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Reason Community Contact')</script>", false);


                this.chk_comm.Focus();

                return;
            }
            if (OtherAmbitionComm == "Others")
            {
                if (txtOtherCon.Text.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Other(specify) Community Contact')</script>", false);


                    this.txtOtherCon.Focus();
                    txtOtherCon.Enabled = true;
                    return;
                }

            }
            else
            {
                txtOtherCon.Enabled = false;
            }
            if (Ambition.Length > 0)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Community Contact')</script>", false);


                this.chk_comm.Focus();

                return;
            }


            if (AmbitionComOther == "8")
            {
                if (txt_con_other.Text.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Other(specify) Reason')</script>", false);


                    this.txt_con_other.Focus();
                    txt_con_other.Enabled = true;
                    return;
                }


            }
            else
            {
                txt_con_other.Enabled = false;
            }
        }

        string Suport = "";
        string SuportOther = "";

        foreach (ListItem item in chk_Suport.Items)
        {
            if (item.Selected)
            {

                Suport += "" + item.Value + "" + ",";
                if (item.Text == "Other")
                {
                    SuportOther = item.Value;
                    txtOtherSupport.Enabled = true;
                }
            }

        }
        if (Suport.Length > 0)
        {
            Suport = Suport.Substring(0, Suport.LastIndexOf(","));
        }
        if (Suport.Length > 0 || rblsupportfc.Checked == true)
        {
            if (rblsupportfc.Checked == true)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Support TB or FC')</script>", false);



                this.rblsupportfc.Focus();
                return;
            }
            if (Suport.Length > 0)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Support ')</script>", false);


                this.chk_Suport.Focus();
                return;
            }

            if (SuportOther == "28")
            {
                if (txtOtherSupport.Text.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Other(specify) Support')</script>", false);


                    this.txtOtherSupport.Focus();
                    txtOtherSupport.Enabled = true;
                    return;
                }


            }
            else
            {
                txtOtherSupport.Enabled = false;
            }
        }

        if (txtmainother.Text.Length > 0 || rblothertb.Checked == true || rblotherfc.Checked == true)
        {
            if (rblothertb.Checked == true || rblotherfc.Checked == true)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select TB or FC Other - Specify ')</script>", false);


                this.rblothertb.Focus();
                return;
            }
            if (txtmainother.Text.Length > 0)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Other - Specify ')</script>", false);


                this.rblothertb.Focus();
                return;
            }
        }
        Int32 TBHoldIng = 0;
        if (rblTbhold.Checked == true)
        {
            TBHoldIng = 1;
        }
        if (rblFcHold.Checked == true)
        {
            TBHoldIng = 1;
        }


        Int32 commmetingTB = 0;
        Int32 commmetingFC = 0;
        Int32 GGS = 0;
        if (chkcommmetingTB.Checked == true)
        {
            commmetingTB = 1;
            GGS = 1;
        }
        if (chkcommmetingFC.Checked == true)
        {
            commmetingFC = 1;
            GGS = 1;
        }


        Int32 c2 = 0;
        Int32 c1 = 0;
        if (rblc1.Checked == true)
        {
            c1 = 1;
        }
        if (rblc2.Checked == true)
        {
            c2 = 1;
        }

        Int32 vill1 = 0;
        if (txtV1illager.Text != "")
        {
            vill1 = Convert.ToInt32(txtV1illager.Text);
        }
        Int32 muhulaTb = 0;
        Int32 muhula55 = 0;
        Int32 muhulaFC = 0;
        if (rblmuhulaTb.Checked == true)
        {
            muhulaTb = 1;
            muhula55 = 1;
        }
        if (rblmuhulaFC.Checked == true)
        {
            muhulaFC = 1;
            muhula55 = 1;
        }


        Int32 vill2 = 0;
        if (txtVillager2.Text != "")
        {
            vill2 = Convert.ToInt32(txtVillager2.Text);
        }

        Int32 Att1 = 0;
        if (txtAtt1.Text != "")
        {
            Att1 = Convert.ToInt32(txtAtt1.Text);
        }


        Int32 othercommTb = 0;
        Int32 othercommFC = 0;

        Int32 commNew = 0;
        if (rblothercommTb.Checked == true)
        {
            othercommTb = 1;
            commNew = 1;
        }
        if (rblothercommfc.Checked == true)
        {
            othercommFC = 1;
            commNew = 1;
        }


        Int32 vill3 = 0;
        if (txtvillager3.Text != "")
        {
            vill3 = Convert.ToInt32(txtvillager3.Text);
        }

        Int32 CommFCTB = 0;
        Int32 CommFC = 0;
        Int32 Comm = 0;
        if (rblcommtb.Checked == true)
        {
            CommFCTB = 1;
            Comm = 1;
        }
        if (rblCommFC.Checked == true)
        {
            CommFC = 1;
            Comm = 1;

        }




        Int32 rolltb = 0;
        Int32 rollFC = 0;
        if (rblemrolltb.Checked == true)
        {
            rolltb = 1;
        }
        if (rblenrollFC.Checked == true)
        {
            rollFC = 1;
        }


        Int32 Supporttb = 0;
        Int32 SupportFC = 0;
        Int32 SupportCC = 0;
        //if (rblSupporttb.Checked == true)
        //{
        //    Supporttb = 1;
        //    SupportCC = 1;
        //}
        if (rblsupportfc.Checked == true)
        {
            SupportFC = 1;

            SupportCC = 1;
        }




        Int32 lotherTB = 0;
        Int32 lotherfc = 0;
        if (rblothertb.Checked == true)
        {
            lotherTB = 1;
        }
        if (rblotherfc.Checked == true)
        {
            lotherfc = 1;
        }
        #endregion
        string Dateof = txtDate.Text;
        string[] b = Dateof.Split('/');

        string FcDate = b[2] + '-' + b[1] + '-' + b[0];
        Int32 txtGSSFe = 0, txtGssMa = 0, txtMMFe = 0, txtMMMa = 0, txtComFe = 0, txtComMa = 0;
        txtGSSFe = TxtGSS_FeMale.Text == "" ? 0 : Convert.ToInt32(TxtGSS_FeMale.Text);
        txtGssMa = TxtGSS_Male.Text == "" ? 0 : Convert.ToInt32(TxtGSS_Male.Text);
        txtMMFe = TxtMM_FeMale.Text == "" ? 0 : Convert.ToInt32(TxtMM_FeMale.Text);
        txtMMMa = TxtMM_Male.Text == "" ? 0 : Convert.ToInt32(TxtMM_Male.Text);
        txtComFe = TxtCm1_FeMale.Text == "" ? 0 : Convert.ToInt32(TxtCm1_FeMale.Text);
        txtComMa = TxtCm1_Male.Text == "" ? 0 : Convert.ToInt32(TxtCm1_Male.Text);
        if (rdEnrollMent.Checked)
        {
            GssEnrollRetan = 1;
        }
        else if (rdRetention.Checked)
        {
            GssEnrollRetan = 2;
        }
        if (rdEnrollment1.Checked)
        {
            MMEnrollRetan = 1;
        }
        else if (rdRetantion1.Checked)
        { MMEnrollRetan = 2; }
        if (rdEnrollment2.Checked)
        {
            Comm1EnrollRetan = 1;
        }
        else if (rdRetantion2.Checked)
        {
            Comm1EnrollRetan = 2;
        }
        else if (rpSocialMapping.Checked)
        {
            Comm1EnrollRetan = 3;
        }
        int icount = 0;
        if (ViewState["GUID"].ToString().Length > 1)
        {
            string StudentTSInsertQuery = "";
            bool InsertTS = false;
            if (Session["user_level"].ToString() == "19")
            {
                                                SqlParameter[] cmdParameters =
                                {
                                new SqlParameter("@GUID_Village", ViewState["GUID"].ToString()),

                                new SqlParameter("@TBCodeOtherMeet", Com1TBCode),
                                new SqlParameter("@TBCodeOtherMeet2", Com2TBCode),

                                new SqlParameter("@Com_Mtg", commNew),

                                new SqlParameter(
                                "@modifyBy",
                                Session["username"].ToString()
                                ),

                                new SqlParameter(
                                "@modifyDate",
                                DateTime.Now
                                ),

                                new SqlParameter("@ComContact", Comm),

                                new SqlParameter("@Support", SupportCC),

                                new SqlParameter("@GSS_Mtg", GGS),

                                new SqlParameter("@Com_TB2", c1),

                                new SqlParameter("@MM_Mtg", muhula55),

                                new SqlParameter("@Com_FC2", c2),

                                new SqlParameter("@Com_Agenda2", othercom11),

                                new SqlParameter(
                                "@Com_AgendaOther2",
                                txtoC111.Text
                                ),

                                new SqlParameter(
                                "@Any_Other2",
                                txtoC1.Text
                                ),

                                new SqlParameter("@Com_Attended2", Att1),

                                new SqlParameter(
                                "@Any_Other",
                                tc1.Text.Trim()
                                ),

                                new SqlParameter(
                                "@TBHandholding",
                                TBHoldIng
                                ),

                                new SqlParameter("@GSS_Attended", vill1),

                                new SqlParameter(
                                "@Remarks",
                                ddlRemark.SelectedValue
                                ),
                                                                new SqlParameter("@GSS_Agenda", commmeeting),
                                new SqlParameter("@GSSChat", commmeeting1),
                                new SqlParameter("@GSSImportantperson", commmeeting2),

                                new SqlParameter(
                                "@GSS_AgendaOther",
                                txt_bookformatOther.Text
                                ),

                                new SqlParameter(
                                "@otherGSSChat",
                                txt_bookformatOther1.Text
                                ),

                                new SqlParameter("@GSS_TB", commmetingTB),
                                new SqlParameter("@GSS_FC", commmetingFC),

                                new SqlParameter("@MM_Attended", vill2),

                                new SqlParameter("@MM_Agenda", Muhula),
                                new SqlParameter("@MMChat", Muhula1),

                                new SqlParameter(
                                "@MMImportantperson",
                                Muhula2
                                ),

                                new SqlParameter(
                                "@MM_AgendaOther",
                                txtmOther.Text
                                ),

                                new SqlParameter(
                                "@othermmchat",
                                txtmOther1.Text
                                ),

                                new SqlParameter("@MM_TB", muhulaTb),
                                new SqlParameter("@MM_FC", muhulaFC),

                                new SqlParameter("@Com_Attended", vill3),

                                new SqlParameter("@Com_Agenda", othercom),

                                new SqlParameter("@OtherChat", othercom1),

                                new SqlParameter(
                                "@OtherImportantperson",
                                othercom2
                                ),

                                new SqlParameter(
                                "@Com_AgendaOther",
                                txtOtherComm.Text
                                ),

                                new SqlParameter(
                                "@OtherspecifyChat",
                                txtOtherComm1.Text
                                ),

                                new SqlParameter("@Com_TB", othercommTb),
                                new SqlParameter("@Com_FC", othercommFC),

                                new SqlParameter(
                                "@ComContact_Op",
                                AmbitionComm
                                ),

                                new SqlParameter(
                                "@ComContact_Op_Other",
                                txt_con_other.Text
                                ),

                                new SqlParameter(
                                "@ComContact_TB",
                                CommFCTB
                                ),

                                new SqlParameter(
                                "@ComContact_FC",
                                CommFC
                                ),

                                new SqlParameter(
                                "@ComContact_Agenda",
                                Ambition
                                ),

                                new SqlParameter(
                                "@ConContact_AgendaOther",
                                txtOtherCon.Text
                                ),

                                new SqlParameter("@Support_Op", Suport),

                                new SqlParameter(
                                "@Support_Op_Other",
                                txtOtherSupport.Text
                                ),

                                new SqlParameter("@Support_TB", Supporttb),
                                new SqlParameter("@Support_FC", SupportFC),

                                new SqlParameter("@Others_FC", lotherfc),
                                new SqlParameter("@Others_TB", lotherTB),

                                new SqlParameter(
                                "@Others_Desc",
                                txtmainother.Text
                                ),

                                new SqlParameter("@GSSFemale", txtGSSFe),
                                new SqlParameter("@GSSMale", txtGssMa),

                                new SqlParameter("@MMFemale", txtMMFe),
                                new SqlParameter("@MMMale", txtMMMa),

                                new SqlParameter("@OtherFemale", txtComFe),
                                new SqlParameter("@OtherMale", txtComMa),

                                new SqlParameter(
                                "@GSSEnrollHault",
                                GssEnrollRetan
                                ),

                                new SqlParameter(
                                "@MMEnrollHault",
                                MMEnrollRetan
                                ),

                                new SqlParameter(
                                "@OtherEnrollHault",
                                Comm1EnrollRetan
                                )

                                };
                // Continue remaining fields same pattern...
           

                icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Update_tblActivityUpdate_Village", cmdParameters);
               
               
            }

            if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
            {
                SqlParameter[] cmdParameters =
        {
                                new SqlParameter("@GUID_Village", ViewState["GUID"].ToString()),

                                new SqlParameter("@TBCodeOtherMeet", Com1TBCode),
                                new SqlParameter("@TBCodeOtherMeet2", Com2TBCode),

                                new SqlParameter("@Com_Mtg", commNew),

                                new SqlParameter(
                                "@modifyBy",
                                Session["username"].ToString()
                                ),

                                new SqlParameter(
                                "@modifyDate",
                                DateTime.Now
                                ),

                                new SqlParameter("@ComContact", Comm),

                                new SqlParameter("@Support", SupportCC),

                                new SqlParameter("@GSS_Mtg", GGS),

                                new SqlParameter("@Com_TB2", c1),

                                new SqlParameter("@MM_Mtg", muhula55),

                                new SqlParameter("@Com_FC2", c2),

                                new SqlParameter("@Com_Agenda2", othercom11),

                                new SqlParameter(
                                "@Com_AgendaOther2",
                                txtoC111.Text
                                ),

                                new SqlParameter(
                                "@Any_Other2",
                                txtoC1.Text
                                ),

                                new SqlParameter("@Com_Attended2", Att1),

                                new SqlParameter(
                                "@Any_Other",
                                tc1.Text.Trim()
                                ),

                                new SqlParameter(
                                "@TBHandholding",
                                TBHoldIng
                                ),

                                new SqlParameter("@GSS_Attended", vill1),

                                new SqlParameter(
                                "@Remarks",
                                ddlRemark.SelectedValue
                                ),
                                                                new SqlParameter("@GSS_Agenda", commmeeting),
                                new SqlParameter("@GSSChat", commmeeting1),
                                new SqlParameter("@GSSImportantperson", commmeeting2),

                                new SqlParameter(
                                "@GSS_AgendaOther",
                                txt_bookformatOther.Text
                                ),

                                new SqlParameter(
                                "@otherGSSChat",
                                txt_bookformatOther1.Text
                                ),

                                new SqlParameter("@GSS_TB", commmetingTB),
                                new SqlParameter("@GSS_FC", commmetingFC),

                                new SqlParameter("@MM_Attended", vill2),

                                new SqlParameter("@MM_Agenda", Muhula),
                                new SqlParameter("@MMChat", Muhula1),

                                new SqlParameter(
                                "@MMImportantperson",
                                Muhula2
                                ),

                                new SqlParameter(
                                "@MM_AgendaOther",
                                txtmOther.Text
                                ),

                                new SqlParameter(
                                "@othermmchat",
                                txtmOther1.Text
                                ),

                                new SqlParameter("@MM_TB", muhulaTb),
                                new SqlParameter("@MM_FC", muhulaFC),

                                new SqlParameter("@Com_Attended", vill3),

                                new SqlParameter("@Com_Agenda", othercom),

                                new SqlParameter("@OtherChat", othercom1),

                                new SqlParameter(
                                "@OtherImportantperson",
                                othercom2
                                ),

                                new SqlParameter(
                                "@Com_AgendaOther",
                                txtOtherComm.Text
                                ),

                                new SqlParameter(
                                "@OtherspecifyChat",
                                txtOtherComm1.Text
                                ),

                                new SqlParameter("@Com_TB", othercommTb),
                                new SqlParameter("@Com_FC", othercommFC),

                                new SqlParameter(
                                "@ComContact_Op",
                                AmbitionComm
                                ),

                                new SqlParameter(
                                "@ComContact_Op_Other",
                                txt_con_other.Text
                                ),

                                new SqlParameter(
                                "@ComContact_TB",
                                CommFCTB
                                ),

                                new SqlParameter(
                                "@ComContact_FC",
                                CommFC
                                ),

                                new SqlParameter(
                                "@ComContact_Agenda",
                                Ambition
                                ),

                                new SqlParameter(
                                "@ConContact_AgendaOther",
                                txtOtherCon.Text
                                ),

                                new SqlParameter("@Support_Op", Suport),

                                new SqlParameter(
                                "@Support_Op_Other",
                                txtOtherSupport.Text
                                ),

                                new SqlParameter("@Support_TB", Supporttb),
                                new SqlParameter("@Support_FC", SupportFC),

                                new SqlParameter("@Others_FC", lotherfc),
                                new SqlParameter("@Others_TB", lotherTB),

                                new SqlParameter(
                                "@Others_Desc",
                                txtmainother.Text
                                ),

                                new SqlParameter("@GSSFemale", txtGSSFe),
                                new SqlParameter("@GSSMale", txtGssMa),

                                new SqlParameter("@MMFemale", txtMMFe),
                                new SqlParameter("@MMMale", txtMMMa),

                                new SqlParameter("@OtherFemale", txtComFe),
                                new SqlParameter("@OtherMale", txtComMa),

                                new SqlParameter(
                                "@GSSEnrollHault",
                                GssEnrollRetan
                                ),

                                new SqlParameter(
                                "@MMEnrollHault",
                                MMEnrollRetan
                                ),

                                new SqlParameter(
                                "@OtherEnrollHault",
                                Comm1EnrollRetan
                                )

                                };
                // Continue remaining fields same pattern...


                icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Update_tblActivityUpdate_Village", cmdParameters);

            }
            if (InsertTS == true)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);

            }
        }
        else
        {
            
            string UNICOde = objMain.Generate_RandomString(8);
            string StudentTSInsertQuery = "";

            bool InsertTS = false;
            if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
            {
                if (Muhula == "")
                {
                    Muhula = "0";
                }
                SqlParameter[] cmdParameters1 =
          {
                    new SqlParameter("@VillageCode", ddlVilage.SelectedValue),
                    new SqlParameter("@UserID", ddlUser.SelectedValue),
                    new SqlParameter("@GUID_Village", UNICOde),

                    new SqlParameter("@ActivityDate", Convert.ToDateTime(FcDate)      ),

                    new SqlParameter("@TBHandholding", TBHoldIng),

                    new SqlParameter("@GSS_Mtg", GGS),
                    new SqlParameter("@GSS_Attended", vill1),
                    new SqlParameter("@GSS_Agenda", commmeeting),
                    new SqlParameter("@GSSChat", commmeeting1),
                    new SqlParameter("@GSSImportantperson", commmeeting2),

                    new SqlParameter( "@GSS_AgendaOther",txt_bookformatOther.Text   ),

                    new SqlParameter(   "@otherGSSChat", txt_bookformatOther1.Text   ),

                    new SqlParameter("@GSS_TB", commmetingTB),
                    new SqlParameter("@GSS_FC", commmetingFC),

                    new SqlParameter("@MM_Mtg", muhula55),
                    new SqlParameter("@MM_Attended", vill2),

                    new SqlParameter("@MM_Agenda", Muhula),
                    new SqlParameter("@MMChat", Muhula1),
                    new SqlParameter("@MMImportantperson", Muhula2),

                    new SqlParameter("@MM_AgendaOther",   txtmOther.Text   ),
                    new SqlParameter(  "@othermmchat",   txtmOther1.Text),

                    new SqlParameter("@MM_TB", muhulaTb),
                    new SqlParameter("@MM_FC", muhulaFC),

                    new SqlParameter("@Com_Mtg", commNew),
                    new SqlParameter("@Com_Attended", vill3),

                    new SqlParameter("@Com_Agenda", othercom),
                    new SqlParameter("@OtherChat", othercom1),

                    new SqlParameter(
                    "@OtherImportantperson",
                    othercom2
                    ),

                    new SqlParameter(
                    "@Com_AgendaOther",
                    txtOtherComm.Text
                    ),

                    new SqlParameter(
                    "@OtherspecifyChat",
                    txtOtherComm1.Text
                    ),

                    new SqlParameter("@Com_TB", othercommTb),
                    new SqlParameter("@Com_FC", othercommFC),

                    new SqlParameter("@ComContact", Comm),

                    new SqlParameter(
                    "@ComContact_Op",
                    AmbitionComm
                    ),

                    new SqlParameter(
                    "@ComContact_Op_Other",
                    txt_con_other.Text
                    ),

                    new SqlParameter("@ComContact_TB", CommFCTB),
                    new SqlParameter("@ComContact_FC", CommFC),

                    new SqlParameter(
                    "@ComContact_Agenda",
                    Ambition
                    ),

                    new SqlParameter(
                    "@ConContact_AgendaOther",
                    txtOtherCon.Text
                    ),

                    new SqlParameter("@Support", SupportCC),

                    new SqlParameter(
                    "@Support_Op",
                    Suport
                    ),

                    new SqlParameter(
                    "@Support_Op_Other",
                    txtOtherSupport.Text
                    ),

                    new SqlParameter("@Support_TB", Supporttb),
                    new SqlParameter("@Support_FC", SupportFC),

                    new SqlParameter("@Others_FC", lotherfc),
                    new SqlParameter("@Others_TB", lotherTB),

                    new SqlParameter(
                    "@Others_Desc",
                    txtmainother.Text
                    ),

                    new SqlParameter("@UserEntry", 3),

                    new SqlParameter(       "@ApproveStatus",    "B"  ),

                    new SqlParameter(
                    "@Remarks",
                    ddlRemark.SelectedValue
                    ),

                    new SqlParameter(
                    "@Any_Other",
                    tc1.Text.Trim()
                    ),

                    new SqlParameter("@Com_TB2", c1),
                    new SqlParameter("@Com_FC2", c2),

                    new SqlParameter(
                    "@Com_Agenda2",
                    othercom11
                    ),

                    new SqlParameter(
                    "@Com_AgendaOther2",
                    txtoC111.Text
                    ),

                    new SqlParameter(
                    "@Any_Other2",
                    txtoC1.Text
                    ),

                    new SqlParameter("@Com_Attended2", Att1),

                    new SqlParameter("@GSSFemale", txtGSSFe),
                    new SqlParameter("@GSSMale", txtGssMa),

                    new SqlParameter("@MMFemale", txtMMFe),
                    new SqlParameter("@MMMale", txtMMMa),

                    new SqlParameter("@OtherFemale", txtComFe),
                    new SqlParameter("@OtherMale", txtComMa),

                    new SqlParameter(
                    "@GSSEnrollHault",
                    GssEnrollRetan
                    ),

                    new SqlParameter(
                    "@MMEnrollHault",
                    MMEnrollRetan
                    ),

                    new SqlParameter(
                    "@OtherEnrollHault",
                    Comm1EnrollRetan
                    ),

                    new SqlParameter("@TBCode", GGTbCode),

                    new SqlParameter(
                    "@TBCodemm",
                    MMTbCode
                    ),

                    new SqlParameter(
                    "@Muhalla",
                    txtMumaullGss.Text
                    ),

                    new SqlParameter(
                    "@Muhallamm",
                    txtMumaullmm.Text
                    ),

                    new SqlParameter(
                    "@BONotice",
                    ddlBo.SelectedValue
                    ),

                    new SqlParameter(
                    "@TBCodeOtherMeet",
                    Com1TBCode
                    ),

                    new SqlParameter(
                    "@TBCodeOtherMeet2",
                    Com2TBCode
                    )
                    };
                icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Insert_tblActivityUpdate_Village", cmdParameters1);

            }

            if (Session["user_level"].ToString() == "19")
            {
                if (Muhula == "")
                {
                    Muhula = "0";
                }


                    SqlParameter[] cmdParameters =
                    {
                    new SqlParameter("@VillageCode", ddlVilage.SelectedValue),
                    new SqlParameter("@UserID", ddlUser.SelectedValue),
                    new SqlParameter("@GUID_Village", UNICOde),

                    new SqlParameter("@ActivityDate", Convert.ToDateTime(FcDate)      ),

                    new SqlParameter("@TBHandholding", TBHoldIng),

                    new SqlParameter("@GSS_Mtg", GGS),
                    new SqlParameter("@GSS_Attended", vill1),
                    new SqlParameter("@GSS_Agenda", commmeeting),
                    new SqlParameter("@GSSChat", commmeeting1),
                    new SqlParameter("@GSSImportantperson", commmeeting2),

                    new SqlParameter( "@GSS_AgendaOther",txt_bookformatOther.Text   ),

                    new SqlParameter(   "@otherGSSChat", txt_bookformatOther1.Text   ),

                    new SqlParameter("@GSS_TB", commmetingTB),
                    new SqlParameter("@GSS_FC", commmetingFC),

                    new SqlParameter("@MM_Mtg", muhula55),
                    new SqlParameter("@MM_Attended", vill2),

                    new SqlParameter("@MM_Agenda", Muhula),
                    new SqlParameter("@MMChat", Muhula1),
                    new SqlParameter("@MMImportantperson", Muhula2),

                    new SqlParameter("@MM_AgendaOther",   txtmOther.Text   ),
                    new SqlParameter(  "@othermmchat",   txtmOther1.Text),

                    new SqlParameter("@MM_TB", muhulaTb),
                    new SqlParameter("@MM_FC", muhulaFC),

                    new SqlParameter("@Com_Mtg", commNew),
                    new SqlParameter("@Com_Attended", vill3),

                    new SqlParameter("@Com_Agenda", othercom),
                    new SqlParameter("@OtherChat", othercom1),

                    new SqlParameter(
                    "@OtherImportantperson",
                    othercom2
                    ),

                    new SqlParameter(
                    "@Com_AgendaOther",
                    txtOtherComm.Text
                    ),

                    new SqlParameter(
                    "@OtherspecifyChat",
                    txtOtherComm1.Text
                    ),

                    new SqlParameter("@Com_TB", othercommTb),
                    new SqlParameter("@Com_FC", othercommFC),

                    new SqlParameter("@ComContact", Comm),

                    new SqlParameter(
                    "@ComContact_Op",
                    AmbitionComm
                    ),

                    new SqlParameter(
                    "@ComContact_Op_Other",
                    txt_con_other.Text
                    ),

                    new SqlParameter("@ComContact_TB", CommFCTB),
                    new SqlParameter("@ComContact_FC", CommFC),

                    new SqlParameter(
                    "@ComContact_Agenda",
                    Ambition
                    ),

                    new SqlParameter(
                    "@ConContact_AgendaOther",
                    txtOtherCon.Text
                    ),

                    new SqlParameter("@Support", SupportCC),

                    new SqlParameter(
                    "@Support_Op",
                    Suport
                    ),

                    new SqlParameter(
                    "@Support_Op_Other",
                    txtOtherSupport.Text
                    ),

                    new SqlParameter("@Support_TB", Supporttb),
                    new SqlParameter("@Support_FC", SupportFC),

                    new SqlParameter("@Others_FC", lotherfc),
                    new SqlParameter("@Others_TB", lotherTB),

                    new SqlParameter(
                    "@Others_Desc",
                    txtmainother.Text
                    ),

                    new SqlParameter("@UserEntry", 2),

                    new SqlParameter(       "@ApproveStatus",    "FC"  ),

                    new SqlParameter(
                    "@Remarks",
                    ddlRemark.SelectedValue
                    ),

                    new SqlParameter(
                    "@Any_Other",
                    tc1.Text.Trim()
                    ),

                    new SqlParameter("@Com_TB2", c1),
                    new SqlParameter("@Com_FC2", c2),

                    new SqlParameter(
                    "@Com_Agenda2",
                    othercom11
                    ),

                    new SqlParameter(
                    "@Com_AgendaOther2",
                    txtoC111.Text
                    ),

                    new SqlParameter(
                    "@Any_Other2",
                    txtoC1.Text
                    ),

                    new SqlParameter("@Com_Attended2", Att1),
                             new SqlParameter("@CreateBy",  Convert.ToString(Session["username"])),
                    new SqlParameter("@GSSFemale", txtGSSFe),
                    new SqlParameter("@GSSMale", txtGssMa),

                    new SqlParameter("@MMFemale", txtMMFe),
                    new SqlParameter("@MMMale", txtMMMa),

                    new SqlParameter("@OtherFemale", txtComFe),
                    new SqlParameter("@OtherMale", txtComMa),

                    new SqlParameter(
                    "@GSSEnrollHault",
                    GssEnrollRetan
                    ),

                    new SqlParameter(
                    "@MMEnrollHault",
                    MMEnrollRetan
                    ),

                    new SqlParameter(
                    "@OtherEnrollHault",
                    Comm1EnrollRetan
                    ),

                    new SqlParameter("@TBCode", GGTbCode),

                    new SqlParameter(
                    "@TBCodemm",
                    MMTbCode
                    ),

                    new SqlParameter(
                    "@Muhalla",
                    txtMumaullGss.Text
                    ),

                    new SqlParameter(
                    "@Muhallamm",
                    txtMumaullmm.Text
                    ),

                    new SqlParameter(
                    "@BONotice",
                    ddlBo.SelectedValue
                    ),

                    new SqlParameter(
                    "@TBCodeOtherMeet",
                    Com1TBCode
                    ),

                    new SqlParameter(
                    "@TBCodeOtherMeet2",
                    Com2TBCode
                    )
                    };
                icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Insert_tblActivityUpdate_Village", cmdParameters);

               

                SqlParameter[] cmdParameters1 =
             {
                    new SqlParameter("@VillageCode", ddlVilage.SelectedValue),
                    new SqlParameter("@UserID", ddlUser.SelectedValue),
                    new SqlParameter("@GUID_Village", UNICOde),

                    new SqlParameter("@ActivityDate", Convert.ToDateTime(FcDate)      ),

                    new SqlParameter("@TBHandholding", TBHoldIng),

                    new SqlParameter("@GSS_Mtg", GGS),
                    new SqlParameter("@GSS_Attended", vill1),
                    new SqlParameter("@GSS_Agenda", commmeeting),
                    new SqlParameter("@GSSChat", commmeeting1),
                    new SqlParameter("@GSSImportantperson", commmeeting2),

                    new SqlParameter( "@GSS_AgendaOther",txt_bookformatOther.Text   ),

                    new SqlParameter(   "@otherGSSChat", txt_bookformatOther1.Text   ),

                    new SqlParameter("@GSS_TB", commmetingTB),
                    new SqlParameter("@GSS_FC", commmetingFC),

                    new SqlParameter("@MM_Mtg", muhula55),
                    new SqlParameter("@MM_Attended", vill2),

                    new SqlParameter("@MM_Agenda", Muhula),
                    new SqlParameter("@MMChat", Muhula1),
                    new SqlParameter("@MMImportantperson", Muhula2),

                    new SqlParameter("@MM_AgendaOther",   txtmOther.Text   ),
                    new SqlParameter(  "@othermmchat",   txtmOther1.Text),

                    new SqlParameter("@MM_TB", muhulaTb),
                    new SqlParameter("@MM_FC", muhulaFC),

                    new SqlParameter("@Com_Mtg", commNew),
                    new SqlParameter("@Com_Attended", vill3),

                    new SqlParameter("@Com_Agenda", othercom),
                    new SqlParameter("@OtherChat", othercom1),

                    new SqlParameter(
                    "@OtherImportantperson",
                    othercom2
                    ),

                    new SqlParameter(
                    "@Com_AgendaOther",
                    txtOtherComm.Text
                    ),

                    new SqlParameter(
                    "@OtherspecifyChat",
                    txtOtherComm1.Text
                    ),

                    new SqlParameter("@Com_TB", othercommTb),
                    new SqlParameter("@Com_FC", othercommFC),

                    new SqlParameter("@ComContact", Comm),

                    new SqlParameter(
                    "@ComContact_Op",
                    AmbitionComm
                    ),

                    new SqlParameter(
                    "@ComContact_Op_Other",
                    txt_con_other.Text
                    ),

                    new SqlParameter("@ComContact_TB", CommFCTB),
                    new SqlParameter("@ComContact_FC", CommFC),

                    new SqlParameter(
                    "@ComContact_Agenda",
                    Ambition
                    ),

                    new SqlParameter(
                    "@ConContact_AgendaOther",
                    txtOtherCon.Text
                    ),

                    new SqlParameter("@Support", SupportCC),

                    new SqlParameter(
                    "@Support_Op",
                    Suport
                    ),

                    new SqlParameter(
                    "@Support_Op_Other",
                    txtOtherSupport.Text
                    ),

                    new SqlParameter("@Support_TB", Supporttb),
                    new SqlParameter("@Support_FC", SupportFC),

                    new SqlParameter("@Others_FC", lotherfc),
                    new SqlParameter("@Others_TB", lotherTB),

                    new SqlParameter(
                    "@Others_Desc",
                    txtmainother.Text
                    ),

                    new SqlParameter("@UserEntry", 3),

                    new SqlParameter(       "@ApproveStatus",    "FC"  ),

                    new SqlParameter(
                    "@Remarks",
                    ddlRemark.SelectedValue
                    ),

                    new SqlParameter(
                    "@Any_Other",
                    tc1.Text.Trim()
                    ),

                    new SqlParameter("@Com_TB2", c1),
                    new SqlParameter("@Com_FC2", c2),

                    new SqlParameter(
                    "@Com_Agenda2",
                    othercom11
                    ),

                    new SqlParameter(
                    "@Com_AgendaOther2",
                    txtoC111.Text
                    ),

                    new SqlParameter(
                    "@Any_Other2",
                    txtoC1.Text
                    ),

                    new SqlParameter("@Com_Attended2", Att1),
                                    new SqlParameter("@CreateBy",  Convert.ToString(Session["username"])),
                    new SqlParameter("@GSSFemale", txtGSSFe),
                    new SqlParameter("@GSSMale", txtGssMa),

                    new SqlParameter("@MMFemale", txtMMFe),
                    new SqlParameter("@MMMale", txtMMMa),

                    new SqlParameter("@OtherFemale", txtComFe),
                    new SqlParameter("@OtherMale", txtComMa),

                    new SqlParameter(
                    "@GSSEnrollHault",
                    GssEnrollRetan
                    ),

                    new SqlParameter(
                    "@MMEnrollHault",
                    MMEnrollRetan
                    ),

                    new SqlParameter(
                    "@OtherEnrollHault",
                    Comm1EnrollRetan
                    ),

                    new SqlParameter("@TBCode", GGTbCode),

                    new SqlParameter(
                    "@TBCodemm",
                    MMTbCode
                    ),

                    new SqlParameter(
                    "@Muhalla",
                    txtMumaullGss.Text
                    ),

                    new SqlParameter(
                    "@Muhallamm",
                    txtMumaullmm.Text
                    ),

                    new SqlParameter(
                    "@BONotice",
                    ddlBo.SelectedValue
                    ),

                    new SqlParameter(
                    "@TBCodeOtherMeet",
                    Com1TBCode
                    ),

                    new SqlParameter(
                    "@TBCodeOtherMeet2",
                    Com2TBCode
                    )
                    };
                icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Insert_tblActivityUpdate_Village", cmdParameters1);

            }
            if (icount >0)
            {


                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                ViewState["GUID"] = UNICOde;
            }
        }

    }


    protected void btnImgMM_Click(object sender, EventArgs e)
    {

        //  imgMKS.ImageUrl = ResolveUrl("~/TabletImage/" +  lblMM.Text);
        //imgMKS.ImageUrl = Server.MapPath(Comman.GetImagePath("TabletImagePath") + "/" + lblMM.Text);
        imgMKS.ImageUrl = "TabletImage/" + lblMM.Text;
        MpexdrDistrict.Show();

    }
    protected void btnImgGss_Click(object sender, EventArgs e)
    {


        //  imgMKS.ImageUrl = Server.MapPath(Comman.GetImagePath("TabletImagePath") + "/"" + lblGG.Text);
        imgMKS.ImageUrl = "TabletImage/" + lblGG.Text;

        //     imgMKS.ImageUrl = ResolveUrl("~/TabletImage/" + lblGG.Text);

        MpexdrDistrict.Show();
    }
    protected void btnimgComm1_Click(object sender, EventArgs e)
    {
        //imgMKS.ImageUrl = Server.MapPath(Comman.GetImagePath("TabletImagePath") + "/"" + lblCom.Text);
        imgMKS.ImageUrl = "TabletImage/" + lblCom.Text;
        //imgMKS.ImageUrl = ResolveUrl("~/TabletImage/" + lblCom.Text);
        MpexdrDistrict.Show();
    }
    protected void btnimgComm2_Click(object sender, EventArgs e)
    {
        // imgMKS.ImageUrl = Server.MapPath(Comman.GetImagePath("TabletImagePath") + "/"" + lblCom1.Text);
        imgMKS.ImageUrl = "TabletImage/" + lblCom1.Text;
        //imgMKS.ImageUrl = ResolveUrl("~/TabletImage/" +  lblCom1.Text);
        MpexdrDistrict.Show();
    }

    protected void btnD2dSerachNew_Click(object sender, EventArgs e)
    {
        DataTable dataTable = this.Session["D2dBindChild"] as DataTable;
        string rowFilter = "1=1 ";
        DataTable dataTable111 = dataTable.Copy();
        if (this.ddlStatusSearch.SelectedIndex > 0)
        {

            if (Convert.ToInt32(this.ddlStatusSearch.SelectedValue) == 1)
            {
                string str = "UniqueIdNew";
                DataTable dataTable2 = dataTable.Copy();
                rowFilter += " and " + str + " like '%" + this.txtSearchNew.Text.Trim() + "%'   ";
                dataTable2.DefaultView.RowFilter = rowFilter;
                dataTable2.DefaultView.Sort = "UniqueIdNew asc";
                //Gv_DisplayNew.DataSource = dataTable2.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
            }
            if (Convert.ToInt32(this.ddlStatusSearch.SelectedValue) == 2)
            {
                string str2 = "HHNo";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " like '%" + this.txtSearchNew.Text.Trim() + "%'   ";
                dataTable3.DefaultView.RowFilter = rowFilter;
                dataTable3.DefaultView.Sort = "HHNo asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
            }

            if (Convert.ToInt32(this.ddlStatusSearch.SelectedValue) == 3)
            {
                string str2 = "ChildName";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " like '%" + this.txtSearchNew.Text.Trim() + "%'   ";
                //dataTable3.DefaultView.RowFilter = rowFilter;
                //dataTable3.DefaultView.Sort = "ChildName asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
            }

            if (Convert.ToInt32(this.ddlStatusSearch.SelectedValue) == 4)
            {
                string str2 = "FathersName";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " like '%" + this.txtSearchNew.Text.Trim() + "%'   ";
                //dataTable3.DefaultView.RowFilter = rowFilter;
                //dataTable3.DefaultView.Sort = "FathersName asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
            }

        }
        if (txtFdate.Text != "" && TxtToDate.Text != "")
        {
            string str2 = "ActivityDate";
            DataTable dataTable3 = dataTable.Copy();
            rowFilter += " and " + str2 + " >= '" + this.txtFdate.Text.Trim() + "' and  ActivityDate<='" + this.TxtToDate.Text.Trim() + "'  ";
            //dataTable3.DefaultView.RowFilter = rowFilter;
            //dataTable3.DefaultView.Sort = "FathersName asc";
            //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
            //Gv_DisplayNew.DataBind();
        }


        if (this.ddlSearchEnroll.SelectedIndex > 0)
        {
            ddlSubContact.Visible = true;
            if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 1)
            {
                string str = "ActivityStatus";
                DataTable dataTable2 = dataTable.Copy();
                rowFilter += " and " + str + "  ='" + ddlSearchEnroll.SelectedValue + "'   ";
                //dataTable2.DefaultView.RowFilter = rowFilter;
                //dataTable2.DefaultView.Sort = "ActivityStatus asc";
                //Gv_DisplayNew.DataSource = dataTable2.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
                objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='MFo' ", "LookupCode", "asc", ddlSubContact, "Description", "LookupCode", "Select");


            }
            if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 2)
            {
                string str2 = "ActivityStatus";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " = '" + ddlSearchEnroll.SelectedValue + "'   ";
                dataTable3.DefaultView.RowFilter = rowFilter;
                dataTable3.DefaultView.Sort = "ActivityStatus asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
                ddlSubContact.Visible = false;
            }
            if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 3)
            {
                string str2 = "ActivityStatus";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " = '" + ddlSearchEnroll.SelectedValue + "'   ";
                //dataTable3.DefaultView.RowFilter = rowFilter;
                //dataTable3.DefaultView.Sort = "HHNo asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();

            }
        }
        if (this.ddlSubContact.SelectedIndex > 0)
        {
            if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 1)
            {
                if (Convert.ToInt32(this.ddlSubContact.SelectedValue) == 1)
                {
                    string str = "FollowUPID";
                    DataTable dataTable2 = dataTable.Copy();
                    rowFilter += " and " + str + "  ='" + ddlSearchEnroll.SelectedValue + "'   ";
                    //dataTable2.DefaultView.RowFilter = rowFilter;
                    //dataTable2.DefaultView.Sort = "ActivityStatus asc";
                    //Gv_DisplayNew.DataSource = dataTable2.DefaultView.ToTable();
                    //Gv_DisplayNew.DataBind();
                    objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='MFo' ", "LookupCode", "asc", ddlSubContact, "Description", "LookupCode", "Select");


                }
            }
            else
            {
                if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 2)
                {
                    string str2 = "VillageOptionID";
                    DataTable dataTable3 = dataTable.Copy();
                    rowFilter += " and " + str2 + " =1 ";
                    //dataTable3.DefaultView.RowFilter = rowFilter;
                    //dataTable3.DefaultView.Sort = "ActivityStatus asc";
                    //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                    //Gv_DisplayNew.DataBind();
                    ddlSubContact.Visible = false;
                }
                if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 2)
                {
                    string str2 = "FromPanding6";
                    DataTable dataTable3 = dataTable.Copy();
                    rowFilter += " and " + str2 + " > 0 ";
                    //dataTable3.DefaultView.RowFilter = rowFilter;
                    //dataTable3.DefaultView.Sort = "HHNo asc";
                    //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                    //Gv_DisplayNew.DataBind();
                }
            }




        }
        dataTable111.DefaultView.RowFilter = rowFilter;
        dataTable111.DefaultView.Sort = "HHNo asc";
        Gv_DisplayNew.DataSource = dataTable111.DefaultView.ToTable();
        Gv_DisplayNew.DataBind();
        this.ModalPopupExtender2.Show();
    }
    protected void ddlSearchEnroll_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dataTable = this.Session["D2dBindChild"] as DataTable;
        string rowFilter = "1=1 ";
        DataTable dataTable111 = dataTable.Copy();
        if (this.ddlStatusSearch.SelectedIndex > 0)
        {

            if (Convert.ToInt32(this.ddlStatusSearch.SelectedValue) == 1)
            {
                string str = "UniqueIdNew";
                DataTable dataTable2 = dataTable.Copy();
                rowFilter += " and " + str + " like '%" + this.txtSearchNew.Text.Trim() + "%'   ";
                dataTable2.DefaultView.RowFilter = rowFilter;
                dataTable2.DefaultView.Sort = "UniqueIdNew asc";
                //Gv_DisplayNew.DataSource = dataTable2.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
            }
            if (Convert.ToInt32(this.ddlStatusSearch.SelectedValue) == 2)
            {
                string str2 = "HHNo";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " like '%" + this.txtSearchNew.Text.Trim() + "%'   ";
                dataTable3.DefaultView.RowFilter = rowFilter;
                dataTable3.DefaultView.Sort = "HHNo asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
            }

            if (Convert.ToInt32(this.ddlStatusSearch.SelectedValue) == 3)
            {
                string str2 = "ChildName";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " like '%" + this.txtSearchNew.Text.Trim() + "%'   ";
                //dataTable3.DefaultView.RowFilter = rowFilter;
                //dataTable3.DefaultView.Sort = "ChildName asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
            }

            if (Convert.ToInt32(this.ddlStatusSearch.SelectedValue) == 4)
            {
                string str2 = "FathersName";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " like '%" + this.txtSearchNew.Text.Trim() + "%'   ";
                //dataTable3.DefaultView.RowFilter = rowFilter;
                //dataTable3.DefaultView.Sort = "FathersName asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
            }

        }
        if (txtFdate.Text != "" && TxtToDate.Text != "")
        {
            string str2 = "ActivityDate";
            DataTable dataTable3 = dataTable.Copy();
            rowFilter += " and " + str2 + " >= '" + this.txtFdate.Text.Trim() + "' and  ActivityDate<='" + this.TxtToDate.Text.Trim() + "'  ";
            //dataTable3.DefaultView.RowFilter = rowFilter;
            //dataTable3.DefaultView.Sort = "FathersName asc";
            //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
            //Gv_DisplayNew.DataBind();
        }


        if (this.ddlSearchEnroll.SelectedIndex > 0)
        {
            ddlSubContact.Visible = true;
            if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 1)
            {
                string str = "ActivityStatus";
                DataTable dataTable2 = dataTable.Copy();
                rowFilter += " and " + str + "  ='" + ddlSearchEnroll.SelectedValue + "'   ";
                //dataTable2.DefaultView.RowFilter = rowFilter;
                //dataTable2.DefaultView.Sort = "ActivityStatus asc";
                //Gv_DisplayNew.DataSource = dataTable2.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
                objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='MFo' ", "LookupCode", "asc", ddlSubContact, "Description", "LookupCode", "Select");


            }
            if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 2)
            {
                string str2 = "ActivityStatus";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " = '" + ddlSearchEnroll.SelectedValue + "'   ";
                dataTable3.DefaultView.RowFilter = rowFilter;
                dataTable3.DefaultView.Sort = "ActivityStatus asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
                ddlSubContact.Visible = false;
            }
            if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 3)
            {
                string str2 = "ActivityStatus";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " = '" + ddlSearchEnroll.SelectedValue + "'   ";
                //dataTable3.DefaultView.RowFilter = rowFilter;
                //dataTable3.DefaultView.Sort = "HHNo asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();

            }
        }
        if (this.ddlSubContact.SelectedIndex > 0)
        {
            if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 1)
            {
                if (Convert.ToInt32(this.ddlSubContact.SelectedValue) == 1)
                {
                    string str = "FollowUPID";
                    DataTable dataTable2 = dataTable.Copy();
                    rowFilter += " and " + str + "  ='" + ddlSearchEnroll.SelectedValue + "'   ";
                    //dataTable2.DefaultView.RowFilter = rowFilter;
                    //dataTable2.DefaultView.Sort = "ActivityStatus asc";
                    //Gv_DisplayNew.DataSource = dataTable2.DefaultView.ToTable();
                    //Gv_DisplayNew.DataBind();
                    objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='MFo' ", "LookupCode", "asc", ddlSubContact, "Description", "LookupCode", "Select");


                }
            }
            else
            {
                if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 2)
                {
                    string str2 = "VillageOptionID";
                    DataTable dataTable3 = dataTable.Copy();
                    rowFilter += " and " + str2 + " =1 ";
                    //dataTable3.DefaultView.RowFilter = rowFilter;
                    //dataTable3.DefaultView.Sort = "ActivityStatus asc";
                    //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                    //Gv_DisplayNew.DataBind();
                    ddlSubContact.Visible = false;
                }
                if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 2)
                {
                    string str2 = "FromPanding6";
                    DataTable dataTable3 = dataTable.Copy();
                    rowFilter += " and " + str2 + " > 0 ";
                    //dataTable3.DefaultView.RowFilter = rowFilter;
                    //dataTable3.DefaultView.Sort = "HHNo asc";
                    //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                    //Gv_DisplayNew.DataBind();
                }
            }




        }
        dataTable111.DefaultView.RowFilter = rowFilter;
        dataTable111.DefaultView.Sort = "HHNo asc";
        Gv_DisplayNew.DataSource = dataTable111.DefaultView.ToTable();
        Gv_DisplayNew.DataBind();
        this.ModalPopupExtender2.Show();

    }




    protected void ddlSubContact_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dataTable = this.Session["D2dBindChild"] as DataTable;
        string rowFilter = "1=1 ";
        DataTable dataTable111 = dataTable.Copy();
        if (this.ddlStatusSearch.SelectedIndex > 0)
        {

            if (Convert.ToInt32(this.ddlStatusSearch.SelectedValue) == 1)
            {
                string str = "UniqueIdNew";
                DataTable dataTable2 = dataTable.Copy();
                rowFilter += " and " + str + " like '%" + this.txtSearchNew.Text.Trim() + "%'   ";
                dataTable2.DefaultView.RowFilter = rowFilter;
                dataTable2.DefaultView.Sort = "UniqueIdNew asc";
                //Gv_DisplayNew.DataSource = dataTable2.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
            }
            if (Convert.ToInt32(this.ddlStatusSearch.SelectedValue) == 2)
            {
                string str2 = "HHNo";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " like '%" + this.txtSearchNew.Text.Trim() + "%'   ";
                dataTable3.DefaultView.RowFilter = rowFilter;
                dataTable3.DefaultView.Sort = "HHNo asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
            }

            if (Convert.ToInt32(this.ddlStatusSearch.SelectedValue) == 3)
            {
                string str2 = "ChildName";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " like '%" + this.txtSearchNew.Text.Trim() + "%'   ";
                //dataTable3.DefaultView.RowFilter = rowFilter;
                //dataTable3.DefaultView.Sort = "ChildName asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
            }

            if (Convert.ToInt32(this.ddlStatusSearch.SelectedValue) == 4)
            {
                string str2 = "FathersName";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " like '%" + this.txtSearchNew.Text.Trim() + "%'   ";
                //dataTable3.DefaultView.RowFilter = rowFilter;
                //dataTable3.DefaultView.Sort = "FathersName asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
            }

        }
        if (txtFdate.Text != "" && TxtToDate.Text != "")
        {
            string str2 = "ActivityDate";
            DataTable dataTable3 = dataTable.Copy();
            rowFilter += " and " + str2 + " >= '" + this.txtFdate.Text.Trim() + "' and  ActivityDate<='" + this.TxtToDate.Text.Trim() + "'  ";
            //dataTable3.DefaultView.RowFilter = rowFilter;
            //dataTable3.DefaultView.Sort = "FathersName asc";
            //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
            //Gv_DisplayNew.DataBind();
        }


        if (this.ddlSearchEnroll.SelectedIndex > 0)
        {
            ddlSubContact.Visible = true;
            if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 1)
            {
                string str = "ActivityStatus";
                DataTable dataTable2 = dataTable.Copy();
                rowFilter += " and " + str + "  ='" + ddlSearchEnroll.SelectedValue + "'   ";
                //dataTable2.DefaultView.RowFilter = rowFilter;
                //dataTable2.DefaultView.Sort = "ActivityStatus asc";
                //Gv_DisplayNew.DataSource = dataTable2.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
                objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='MFo' ", "LookupCode", "asc", ddlSubContact, "Description", "LookupCode", "Select");


            }
            if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 2)
            {
                string str2 = "ActivityStatus";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " = '" + ddlSearchEnroll.SelectedValue + "'   ";
                dataTable3.DefaultView.RowFilter = rowFilter;
                dataTable3.DefaultView.Sort = "ActivityStatus asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();
                ddlSubContact.Visible = false;
            }
            if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 3)
            {
                string str2 = "ActivityStatus";
                DataTable dataTable3 = dataTable.Copy();
                rowFilter += " and " + str2 + " = '" + ddlSearchEnroll.SelectedValue + "'   ";
                //dataTable3.DefaultView.RowFilter = rowFilter;
                //dataTable3.DefaultView.Sort = "HHNo asc";
                //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                //Gv_DisplayNew.DataBind();

            }
        }
        if (this.ddlSubContact.SelectedIndex > 0)
        {
            if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 1)
            {
                if (Convert.ToInt32(this.ddlSubContact.SelectedValue) == 1)
                {
                    string str = "FollowUPID";
                    DataTable dataTable2 = dataTable.Copy();
                    rowFilter += " and " + str + "  ='" + ddlSearchEnroll.SelectedValue + "'   ";
                    //dataTable2.DefaultView.RowFilter = rowFilter;
                    //dataTable2.DefaultView.Sort = "ActivityStatus asc";
                    //Gv_DisplayNew.DataSource = dataTable2.DefaultView.ToTable();
                    //Gv_DisplayNew.DataBind();
                    objComman.BindDLL("mstLookup", "LookupCode,Description", "[LookupFlag]='MFo' ", "LookupCode", "asc", ddlSubContact, "Description", "LookupCode", "Select");


                }
            }
            else
            {
                if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 2)
                {
                    string str2 = "VillageOptionID";
                    DataTable dataTable3 = dataTable.Copy();
                    rowFilter += " and " + str2 + " =1 ";
                    //dataTable3.DefaultView.RowFilter = rowFilter;
                    //dataTable3.DefaultView.Sort = "ActivityStatus asc";
                    //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                    //Gv_DisplayNew.DataBind();
                    ddlSubContact.Visible = false;
                }
                if (Convert.ToInt32(this.ddlSearchEnroll.SelectedValue) == 2)
                {
                    string str2 = "FromPanding6";
                    DataTable dataTable3 = dataTable.Copy();
                    rowFilter += " and " + str2 + " > 0 ";
                    //dataTable3.DefaultView.RowFilter = rowFilter;
                    //dataTable3.DefaultView.Sort = "HHNo asc";
                    //Gv_DisplayNew.DataSource = dataTable3.DefaultView.ToTable();
                    //Gv_DisplayNew.DataBind();
                }
            }




        }
        dataTable111.DefaultView.RowFilter = rowFilter;
        dataTable111.DefaultView.Sort = "HHNo asc";
        Gv_DisplayNew.DataSource = dataTable111.DefaultView.ToTable();
        Gv_DisplayNew.DataBind();
        this.ModalPopupExtender2.Show();

    }

    protected void lnkListStaus_OnClick(object sender, EventArgs e)
    {

        SqlParameter[] parm = new SqlParameter[]
             {
               new SqlParameter("@Villagecode",  ddlVilage.SelectedValue),
               new SqlParameter("@Fyear",  Session["FinYear"].ToString()),


                 };

        DataTable dtUserVillage = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptLoadBundal]", parm);
        Session["D2dBindChild"] = dtUserVillage;
        Gv_DisplayNew.DataSource = dtUserVillage;
        Gv_DisplayNew.DataBind();
        ddlSearchEnroll.SelectedIndex = 0;

        ddlStatusSearch.SelectedIndex = 0;
        txtSearchNew.Text = "";

        this.ModalPopupExtender2.Show();
    }
    public void loadDataDropdown()
    {
        DataTable dtYear = CreateDataTable();
        DataRow dr;



        dr = dtYear.NewRow();
        dr["Type"] = "School wise enrollment";
        dr["ID"] = 1;
        dtYear.Rows.Add(dr);
        dr = dtYear.NewRow();
        dr["Type"] = "From 6 Status";
        dr["ID"] = 2;
        dtYear.Rows.Add(dr);
        //get last  two digits (eg: 10 from 2010);
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlSubContact, "Type", "ID", "Select");



    }
    public DataTable CreateDataTable()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYear.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYear;
    }
    public void currencyTextBox_TextChanged(object sender, EventArgs e)
    {
        if (ddlRemark.SelectedIndex > 0)
        {
        }
        else
        {
            pnlMain.Enabled = false;
            pnlMain11.Enabled = false;
            Panel1.Enabled = false;
        }
        btnSerach_Click(btnSerach, null);
    }
    protected void btnEditUndo_Click(object sender, EventArgs e)
    {

        Button ddlLabTest1 = (Button)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        string UNICOde = objMain.Generate_RandomString(15);

        Label lbUniqueCode11 = (Label)row1.FindControl("lbUniqueCode11");
        Label lblGUIDDTDMobileActivity = (Label)row1.FindControl("lblGUIDDTDMobileActivity");
        Label lblActivityStatus1 = (Label)row1.FindControl("lblActivityStatus1");

        SqlParameter[] cmdParameters = new SqlParameter[]
                    {
                          new SqlParameter("@GUIDDTDMobileActivity", lblGUIDDTDMobileActivity.Text ),
                        new SqlParameter("@UniqueCode", lbUniqueCode11.Text ),
                        new SqlParameter("@ActivityStatus", lblActivityStatus1.Text),
                         new SqlParameter("@NewGuid", UNICOde),


                    };
        Int32 icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "UpdateContactChild", cmdParameters);

        if (icount > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
            ModalPopupExtender.Show();
        }
        lnkListStaus_OnClick(lnkEnrool, null);
    }
}