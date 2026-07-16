using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class frmOfficeReport : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    string conditions = "";
    Comman objComman = new Comman();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            FillCBState();
            if (Request.QueryString["ID"] != null)
            {

               
                string QueryString = Request.QueryString["ID"];
                string[] a = QueryString.Split(',');
                txtDate.Text = a[0].ToString();
                LoadData(Session["Cluseter"].ToString());

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
                //    string Strhh = Convert.ToString(Session["BlockCodeAct"]);
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
                            CalendarExtenderTourdate.StartDate = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString()).AddDays(1);
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
                            CalendarExtenderTourdate.StartDate = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString()).AddDays(1);
                        }
                    }
                }
                string ToDate = txtDate.Text;
                string[] c = ToDate.Split('/');
                string aToDate = c[2] + '-' + c[1] + '-' + c[0];

                string con = "";
                DataTable dtMain = null;
                pnlMain.Enabled = false;
                if (Session["user_level"].ToString() == "19")
                {
                    con = "ActivityDate =('" + aToDate + "')   and ApproveStatus='FC'  and mstCluster.ClusterCode='" + Session["Cluseter"].ToString() + "' ";
                    dtMain = objMain.LoadAllActivtiyDatewise(con, 3);

                }
                if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
                {
                    con = "ActivityDate =('" + aToDate + "')  and ApproveStatus='B' and mstCluster.ClusterCode='" + Session["Cluseter"].ToString() + "' ";
                    dtMain = objMain.LoadAllActivtiyDatewise(con, 3);
                    // dtMain = objMain.LoadSchoolActivtiyCluseterIO(afromDate, aToDate, ddlBlock.SelectedValue, con);
                }
                if (dtMain.Rows.Count > 0)
                {
                    ddlUser.SelectedValue = dtMain.Rows[0]["UserName"].ToString();
                    ddlUser_SelectedIndexChanged(ddlUser, null);
                    if (ddlUser.SelectedIndex > 0)
                    {
                        ddlVilage.SelectedValue = dtMain.Rows[0]["Villagecode"].ToString();
                        ddlVilage_SelectedIndexChanged(ddlVilage, null);
                        //  ddlSchool.SelectedValue = dtMain.Rows[0]["SchoolCode"].ToString();

                        btnSerach_Click(btnSerach, null);
                    }
                }
              
                // ddlUser_SelectedIndexChanged(ddlUser, null);

                //ddlUser.SelectedValue = a[1];


                ////DateTime ActivityDate = Convert.ToDateTime(a[0]);
                ////txtDate.Text = ActivityDate.ToString("dd/MM/yyy");
                //txtDate.Text = a[0].ToString();
                //   ddlUser_SelectedIndexChanged(ddlUser, null);
                //ddlUser.SelectedValue = a[1];
                //txtDate.Text = a[0].ToString();
                //ddlUser_SelectedIndexChanged(ddlUser, null);

                ////DateTime ActivityDate = Convert.ToDateTime(a[0]);
                ////txtDate.Text = ActivityDate.ToString("dd/MM/yyy");

                //if (ddlUser.SelectedIndex > 0)
                //{
                //    string ToDate = txtDate.Text;
                //    string[] c = ToDate.Split('/');
                //    string aToDate = c[2] + '-' + c[1] + '-' + c[0];

                //    string strQry = "Select * from TblActivityUpdate_Office where  ActivityDate = '" + aToDate + "' and UserID='" + ddlUser.SelectedValue + "'  ";


                //    DataTable dtRole = objMain.LoadData(strQry);
                //    if (dtRole.Rows.Count > 0)
                //    {
                //        ddlVilage.SelectedValue = dtRole.Rows[0]["Villagecode"].ToString();
                //        ddlVilage_SelectedIndexChanged(ddlVilage, null);

                //        btnSerach_Click(btnSerach, null);
                //    }

                //}

                //btnSerach_Click(btnSerach, null);
            }

        }

    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
        MpexdrDistrict.Show();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBBock();
        MpexdrDistrict.Show();
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBCluster();
        MpexdrDistrict.Show();
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillage();
        MpexdrDistrict.Show();
    }
    public void FillCVillage()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlAddVillage, "VillageName", "VillageCode", "--Select--");



    }
    public void FillCBCluster()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "'";
        objComman.BindDLL("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "--Select--");



    }
    public void FillCBBock()
    {
        conditions = "";

        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and FYear ='" + Session["FinYear"].ToString() + "'";
       
        
        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");



    }
    public void FillCBDist()
    {

        conditions = "";

        conditions = "StateCode ='" + ddlState.SelectedValue + "' and mst2District.FYear ='" + Session["FinYear"].ToString() + "'";
       objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");



    }

    public void FillCBState()
    {
        conditions = "";
        
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");

      

       

    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        //  btnApprove.Attributes.Add("onclick", "javascript:return " + "confirm('Please confirm if you want to approve? ')");


        Response.Redirect("~/FrmActivityDatewiseSearch.aspx?ID=" + Session["CluseterName"].ToString() + "," + Session["FromData"].ToString() + "," + Session["Todate"].ToString() + "");


    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        pnlGridView.Visible = true;
        pnlView.Visible = false;
          DataTable dtMain1 = objMain.mstActivityVillageCheck(ddlUser.SelectedValue, ddlAddVillage.SelectedValue, 3);
          if (dtMain1.Rows.Count > 0)
          {
              gvVillage.DataSource = dtMain1;
              gvVillage.DataBind();
          }
        MpexdrDistrict.Show();
    }
    protected void btnNewUserSave_Click(object sender, EventArgs e)
    {
  
        DataTable dtMain = objMain.mstActivityVillageCheck(ddlUser.SelectedValue, ddlAddVillage.SelectedValue,1);
        if (dtMain.Rows.Count > 0)
        {
            if (dtMain.Rows.Count >= 8)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('You Add 8 village Allready')</script>", false);
                MpexdrDistrict.Show();
                return;
               
            }
        }
        DataTable dtMain1 = objMain.mstActivityVillageCheck(ddlUser.SelectedValue, ddlAddVillage.SelectedValue, 2);
        if (dtMain1.Rows.Count > 0)
        {
           
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('You are Allreday Add this village')</script>", false);
                MpexdrDistrict.Show();
                return;

           
        }
        Int32 iCount = objMain.mstActivityVillageMaster(ddlUser.SelectedValue, ddlAddVillage.SelectedValue, ddlAddVillage.SelectedItem.Text);
        if (iCount > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
            ddlUser_SelectedIndexChanged(ddlUser, null);
        }
    }
    
    protected void btnAddVillage_Click(object sender, EventArgs e)
    {
        if (ddlUser.SelectedIndex <= 0)
        {
          
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select User')</script>", false);
            return;
        }
        pnlGridView.Visible = false;
        pnlView.Visible = true;
        MpexdrDistrict.Show();
    }
    public void LoadData(string ClusterName)
    {

        string fromDate = txtDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];




        string strQry = "";
        strQry = "Select  distinct UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName]  from MstUser  where UserLevel=24 and   VillageCode = '" + Session["Cluseter"].ToString() + "' ";

        strQry += "union  ";
        strQry += " Select  distinct UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName]  from MstUser  where UserLevel=24 and UserName in(  ";
        strQry += " select UserID from TblActivityUpdate_Office  ";
        strQry += " inner join mst5village on mst5village.villagecode=TblActivityUpdate_Office.villagecode  ";
        strQry += " where ActivityDate =('" + afromDate + "')  and  ";
        strQry += " mst5village.ClusterCode    = '" + Session["Cluseter"].ToString() + "'  )    ";


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

    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (ddlRemark.SelectedIndex > 0)
        {
            pnlMain.Enabled = true;
            btnSerach_Click(btnSerach, null);
        }
        else
        {
            pnlMain.Enabled = false;
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();

    }
    #region ************ Button Click Events *****************
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        if (ddlUser.SelectedIndex <= 0)
        {
            ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select User')</script>", false);
            return;
        }
        if (ddlVilage.SelectedIndex <= 0)
        {
            ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Village')</script>", false);
            return;
        }
        if (txtDate.Text == "")
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date')</script>", false);
            return;
        }

        if (ddlRemark.SelectedIndex > 0)
        {
            pnlMain.Enabled = true;
        }
        else
        {
            pnlMain.Enabled = false;
        }
        // ClearData();

        LoadOfficeActvity();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ViewState["GUID"].ToString().Length > 5)
        {
            int res1 = 0;
            
        SqlParameter[] cmdParameters = new SqlParameter[]
		{
			new SqlParameter("@UniqueChildCode ", ViewState["GUID"].ToString()),
			
		};
        res1 = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteAcctivtiyOffice", cmdParameters);
    
            
                if (res1 > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Delete Sucessfully')</script>", false);
                }
           
        }
    }
    public void LoadOfficeActvity()
    {
        ClearData();


        string Dateof = txtDate.Text;
        string[] b = Dateof.Split('/');

        string FcDate = b[2] + '-' + b[1] + '-' + b[0];
        string strQry = "   select *  from TblActivityUpdate_Office   where UserID='" + ddlUser.SelectedValue + "' and VillageCode='" + ddlVilage.SelectedValue + "' and ActivityDate= '" + Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd") + "' ";
        DataTable dtVillageActivtiy = objMain.LoadData(strQry);

        if (dtVillageActivtiy.Rows.Count > 0)
        {
            if (dtVillageActivtiy.Rows[0]["ApproveStatus"].ToString() == "B" || dtVillageActivtiy.Rows[0]["ApproveStatus"].ToString() == "FC" || dtVillageActivtiy.Rows[0]["ApproveStatus"].ToString() == "I")
            {
                if (Session["user_level"].ToString() == "19" && dtVillageActivtiy.Rows[0]["ApproveStatus"].ToString() == "FC")
                {
                    Btnsave.Visible = true;
                }
                else
                {
                    Btnsave.Visible = false;
                }
                if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
                {
                    if (dtVillageActivtiy.Rows[0]["ApproveStatus"].ToString() == "B")
                    {
                        Btnsave.Visible = true;
                    }
                    else
                    {
                        Btnsave.Visible = false;
                    }
                }
            }

            #region LoadDate
            ViewState["GUID"] = dtVillageActivtiy.Rows[0]["GUID_Office"].ToString();

            if (dtVillageActivtiy.Rows[0]["Remarks"].ToString().Length > 0)
            {
                ddlRemark.SelectedValue = dtVillageActivtiy.Rows[0]["Remarks"].ToString();
            }
            if (dtVillageActivtiy.Rows[0]["Meeting"].ToString() == "1")
            {
                chkMeetings.Checked = true;
            }
            else
            {
                chkMeetings.Checked = false;
            }

            if (dtVillageActivtiy.Rows[0]["Meeting_FC"].ToString() == "1")
            {
                rblMeetingsFC.Checked = true;
            }
            else
            {
                rblMeetingsFC.Checked = false;
            }

            string cmeeting = dtVillageActivtiy.Rows[0]["MeetingType"].ToString();

            string[] meeting = cmeeting.Split(',');
            string TextMeeeting = "";
            foreach (string s in meeting)
            {
                if (s == "58")
                {
                    chk_FC_For.Checked = true;
                }
                if (s == "59")
                {
                    chk_BO.Checked = true;
                }

                if (s == "60")
                {
                    chk_Goverment.Checked = true;
                }

                if (s == "61")
                {
                    chk_Other.Checked = true;
                }

            }




            if (dtVillageActivtiy.Rows[0]["Training"].ToString() == "1")
            {
                Chk_Training.Checked = true;
            }
            else
            {
                Chk_Training.Checked = false;
            }

            if (dtVillageActivtiy.Rows[0]["Training_FC"].ToString() == "1")
            {
                rdTrainingFC.Checked = true;
            }
            else
            {
                rdTrainingFC.Checked = false;
            }

            string cTrainingType = dtVillageActivtiy.Rows[0]["TrainingType"].ToString();

            string[] TrainingType = cTrainingType.Split(',');

            foreach (string s in TrainingType)
            {
                if (s == "62")
                {
                    CHkTB.Checked = true;
                }
                if (s == "63")
                {
                    ChkStaffTraining.Checked = true;
                }

                if (s == "64")
                {
                    Chk_Other_Training.Checked = true;
                }


            }
            if (dtVillageActivtiy.Rows[0]["Other_FC"].ToString() == "1")
            {
                chk_Other_Desc.Checked = true;
            }
            Txt_OtherDesc.Text = dtVillageActivtiy.Rows[0]["Other_specify"].ToString();
            txtTraingOther.Text = dtVillageActivtiy.Rows[0]["TrainingType_Other"].ToString();

            #endregion
        }
        else
        {
            chkMeetings.Checked = false;
            rblMeetingsFC.Checked = false;
            chk_FC_For.Checked = false;
            chk_BO.Checked = false;
            chk_Goverment.Checked = false;
            chk_Other.Checked = false;
            Chk_Training.Checked = false;
            rdTrainingFC.Checked = false;
            CHkTB.Checked = false;
            ChkStaffTraining.Checked = false;
            Chk_Other_Training.Checked = false;
            chk_Other_Desc.Checked = false;
            Txt_OtherDesc.Text = "";
            txtTraingOther.Text = "";
            txtTraingOtherDec.Text = "";
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
    protected void BtnSave_Click(object sender, EventArgs e)
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
    public void ClearData()
    {
        chkMeetings.Checked = false;
        rblMeetingsFC.Checked = false;
        chk_FC_For.Checked = false;
        chk_BO.Checked = false;
        chk_Goverment.Checked = false;
        chk_Other.Checked = false;
        Chk_Training.Checked = false;
        rdTrainingFC.Checked = false;
        CHkTB.Checked = false;
        ChkStaffTraining.Checked = false;
        Chk_Other_Training.Checked = false;
        chk_Other_Desc.Checked = false;
        Txt_OtherDesc.Text = "";
        txtTraingOther.Text = "";
        txtTraingOtherDec.Text = "";
    }
    protected void SaveData()
    {
        Int32 MeetingCount = 0;
        Int32 TrainingCount = 0;
        if (txtDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date')</script>", false);


            this.rblMeetingsFC.Focus();
            return;
        }
        if (chkMeetings.Checked == true)
        {
            if (rblMeetingsFC.Checked == false)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Meeting FC')</script>", false);


                this.rblMeetingsFC.Focus();
                return;
            }

            if (chk_FC_For.Checked == true)
            {
                MeetingCount = 1;
            }
            if (chk_BO.Checked == true)
            {
                MeetingCount = 1;
            }
            if (chk_Goverment.Checked == true)
            {
                MeetingCount = 1;
            }
            if (chk_Other.Checked == true)
            {
                MeetingCount = 1;
            }
            if (MeetingCount == 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Any Meeting Type')</script>", false);


                this.rblMeetingsFC.Focus();
                return;
            }
        }
        if (chkMeetings.Checked == false)
        {


            if (chk_FC_For.Checked == true)
            {
                MeetingCount = 1;
            }
            if (chk_BO.Checked == true)
            {
                MeetingCount = 1;
            }
            if (chk_Goverment.Checked == true)
            {
                MeetingCount = 1;
            }
            if (chk_Other.Checked == true)
            {
                MeetingCount = 1;
                if (txtTraingOther.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Other Meeting')</script>", false);


                    this.txtTraingOther.Focus();
                    return;
                }
            }
            if (MeetingCount == 1)
            {
                if (chkMeetings.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Meeting ')</script>", false);


                    this.rblMeetingsFC.Focus();
                    return;
                }
            }

        }

        if (Chk_Training.Checked == true)
        {
            if (rdTrainingFC.Checked == false)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Training FC ')</script>", false);


                this.rblMeetingsFC.Focus();
                return;
            }
            if (CHkTB.Checked == true)
            {
                TrainingCount = 1;
            }
            if (ChkStaffTraining.Checked == true)
            {
                TrainingCount = 1;
            }
            if (Chk_Other_Training.Checked == true)
            {
                TrainingCount = 1;

                if (txtTraingOther.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Other Training ')</script>", false);


                    this.txtTraingOther.Focus();
                    return;
                }
            }
            if (TrainingCount == 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Any Meeting Type')</script>", false);


                this.rblMeetingsFC.Focus();
                return;
            }
        }
        if (chk_Other_Desc.Checked == true)
        {
            if (Txt_OtherDesc.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Other(Description) ')</script>", false);


                this.txtTraingOther.Focus();
                return;
            }
        }



        if (Chk_Training.Checked == false)
        {

            if (CHkTB.Checked == true)
            {
                TrainingCount = 1;
            }
            if (ChkStaffTraining.Checked == true)
            {
                TrainingCount = 1;
            }
            if (Chk_Other_Training.Checked == true)
            {
                TrainingCount = 1;

                if (txtTraingOther.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Other Training ')</script>", false);


                    this.txtTraingOther.Focus();
                    return;
                }
            }

            if (TrainingCount == 1)
            {
                if (Chk_Training.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Training ')</script>", false);


                    this.rblMeetingsFC.Focus();
                    return;
                }
            }
        }
        if (chk_Other_Desc.Checked == true)
        {
            if (Txt_OtherDesc.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Other(Description) ')</script>", false);


                this.txtTraingOther.Focus();
                return;
            }
        }
        if (Txt_OtherDesc.Text != "")
        {
            if (chk_Other_Desc.Checked == false)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Other(Description) ')</script>", false);


                this.txtTraingOther.Focus();
                return;
            }
        }
        string Meeting = "", MeetingType = "", Trainging = "", TraingingType = "", TxtDesc = "", UNICOde = "";
        int MeetingFC = 0, TraingingFC = 0, OtherDesc = 0;
        if (chkMeetings.Checked == true)
        {
            Meeting = "1";
            if (rblMeetingsFC.Checked == true)
            {
                MeetingFC = 1;
                if (chk_FC_For.Checked == true)
                {
                    MeetingType = "58,";
                }
                if (chk_BO.Checked == true)
                {
                    MeetingType += "59,";
                }
                if (chk_Goverment.Checked == true)
                {
                    MeetingType += "60,";
                }
                if (chk_Other.Checked == true)
                {
                    MeetingType += "61,";
                }
                if (MeetingType.Length > 0)
                {
                    MeetingType = MeetingType.Substring(0, MeetingType.LastIndexOf(","));
                }

            }
        }
        if (Chk_Training.Checked == true)
        {
            Trainging = "1";
            if (rdTrainingFC.Checked == true)
            {
                TraingingFC = 1;
                if (CHkTB.Checked == true)
                {
                    TraingingType = "62,";
                }
                if (ChkStaffTraining.Checked == true)
                {
                    TraingingType += "63,";
                }
                if (Chk_Other_Training.Checked == true)
                {
                    TraingingType += "64,";

                }
                if (Trainging.Length > 0)
                {
                    TraingingType = TraingingType.Substring(0, TraingingType.LastIndexOf(","));
                }
            }

        }
        if (chk_Other_Desc.Checked == true)
        {
            OtherDesc = 1;
            if (Txt_OtherDesc.Text == "")
            {
                TxtDesc = Txt_OtherDesc.Text;
            }

        }
        if (ViewState["GUID"].ToString().Length > 5)
        {
            UNICOde = ViewState["GUID"].ToString();
        }
        else
        {
            UNICOde = objMain.Generate_RandomString(8);
        }
        int result = 0;
        string Dateof = txtDate.Text;
        string[] b = Dateof.Split('/');

        string FcDate = b[2] + '-' + b[1] + '-' + b[0];
        Boolean InsertTS = false;
        string SQL = "";
        if (ViewState["GUID"].ToString().Length > 1)
        {
            if (Session["user_level"].ToString() == "19")
            {

                SqlParameter[] parm = new SqlParameter[]
                        {
                     new SqlParameter("@GUID_Office",ViewState["GUID"].ToString()),
                        new SqlParameter("@Meeting", Meeting),
                        new SqlParameter("@Meeting_FC", MeetingFC),
                        new SqlParameter("@MeetingType",MeetingType),
                        new SqlParameter("@MeetingType_Other", MeetingFC),
                                          new SqlParameter("@Other_FC",OtherDesc),
                        new SqlParameter("@Training", Trainging),
                        new SqlParameter("@Training_FC", TraingingFC),
                        new SqlParameter("@TrainingType", TraingingType),
                         new SqlParameter("@TrainingType_Other",txtTraingOther.Text),
  
                        new SqlParameter("@Other_specify", Txt_OtherDesc.Text),
                        new SqlParameter("@ActivityDate", Convert.ToDateTime(FcDate).ToString("yyy/MM/dd")),
                       new SqlParameter("@UserID",ddlUser.SelectedValue ),

                             new SqlParameter("@VillageCode", ddlVilage.SelectedValue),
                                 new SqlParameter("@ApproveStatus","FC"),
                       new SqlParameter("@UserEntry", "2"),
                         new SqlParameter("@Remarks", ddlRemark.SelectedValue),
                                new SqlParameter("@CreateBy", Convert.ToString(Session["username"] )),
                                  new SqlParameter("@Flag", "U"),

                          };
                result = Convert.ToInt32(SqlHelper.ExecuteScaler(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateOffice", parm));


            }
            if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
            {
                SqlParameter[] parm = new SqlParameter[]
                        {
                     new SqlParameter("@GUID_Office",ViewState["GUID"].ToString()),
                        new SqlParameter("@Meeting", Meeting),
                        new SqlParameter("@Meeting_FC", MeetingFC),
                        new SqlParameter("@MeetingType",MeetingType),
                        new SqlParameter("@MeetingType_Other", MeetingFC),
                                          new SqlParameter("@Other_FC",OtherDesc),
                        new SqlParameter("@Training", Trainging),
                        new SqlParameter("@Training_FC", TraingingFC),
                        new SqlParameter("@TrainingType", TraingingType),
                         new SqlParameter("@TrainingType_Other",txtTraingOther.Text),

                        new SqlParameter("@Other_specify", Txt_OtherDesc.Text),
                        new SqlParameter("@ActivityDate", Convert.ToDateTime(FcDate).ToString("yyy/MM/dd")),
                       new SqlParameter("@UserID",ddlUser.SelectedValue ),

                             new SqlParameter("@VillageCode", ddlVilage.SelectedValue),
                                 new SqlParameter("@ApproveStatus","B"),
                       new SqlParameter("@UserEntry", "3"),
                         new SqlParameter("@Remarks", ddlRemark.SelectedValue),
                                new SqlParameter("@CreateBy", Convert.ToString(Session["username"] )),
                                  new SqlParameter("@Flag", "U"),

                          };
                result = Convert.ToInt32(SqlHelper.ExecuteScaler(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateOffice", parm));

            }
        }
        else
        {
            if (Session["user_level"].ToString() == "19")
            {

                SqlParameter[] parm = new SqlParameter[]
                          {
                                          new SqlParameter("@GUID_Office",UNICOde),
                        new SqlParameter("@Meeting", Meeting),
                        new SqlParameter("@Meeting_FC", MeetingFC),
                        new SqlParameter("@MeetingType",MeetingType),
                        new SqlParameter("@MeetingType_Other", MeetingFC),
                        new SqlParameter("@Training", Trainging),
                        new SqlParameter("@Training_FC", TraingingFC),
                        new SqlParameter("@TrainingType", TraingingType),
       
                           new SqlParameter("@TrainingType_Other",txtTraingOther.Text),
                                             new SqlParameter("@Other_FC",OtherDesc),

                        new SqlParameter("@Other_specify", Txt_OtherDesc.Text),
                        new SqlParameter("@ActivityDate", Convert.ToDateTime(FcDate).ToString("yyy/MM/dd")),
                       new SqlParameter("@UserID",ddlUser.SelectedValue ),

                             new SqlParameter("@VillageCode", ddlVilage.SelectedValue),
                                 new SqlParameter("@ApproveStatus","FC"),
                       new SqlParameter("@UserEntry", "2"),
                         new SqlParameter("@Remarks", ddlRemark.SelectedValue),
                                new SqlParameter("@CreateBy", Convert.ToString(Session["username"] )),
                                  new SqlParameter("@Flag", "I"),

                            };
                result = Convert.ToInt32(SqlHelper.ExecuteScaler(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateOffice", parm));

            }
            if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
            {
                SqlParameter[] parm = new SqlParameter[]
                          {
                                          new SqlParameter("@GUID_Office",UNICOde),
                        new SqlParameter("@Meeting", Meeting),
                        new SqlParameter("@Meeting_FC", MeetingFC),
                        new SqlParameter("@MeetingType",MeetingType),
                        new SqlParameter("@MeetingType_Other", MeetingFC),
                        new SqlParameter("@Training", Trainging),
                        new SqlParameter("@Training_FC", TraingingFC),
                        new SqlParameter("@TrainingType", TraingingType),
                         new SqlParameter("@TrainingType_Other",txtTraingOther.Text),
                                           new SqlParameter("@Other_FC",OtherDesc),

                        new SqlParameter("@Other_specify", Txt_OtherDesc.Text),
                        new SqlParameter("@ActivityDate", Convert.ToDateTime(FcDate).ToString("yyy/MM/dd")),
                       new SqlParameter("@UserID",ddlUser.SelectedValue ),

                             new SqlParameter("@VillageCode", ddlVilage.SelectedValue),
                                 new SqlParameter("@ApproveStatus","FC"),
                       new SqlParameter("@UserEntry", "3"),
                         new SqlParameter("@Remarks", ddlRemark.SelectedValue),
                                new SqlParameter("@CreateBy", Convert.ToString(Session["username"] )),
                                  new SqlParameter("@Flag", "B"),

                            };
                result = Convert.ToInt32(SqlHelper.ExecuteScaler(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateOffice", parm));

            }
        }
        if (result>0)
        {


            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
            ViewState["GUID"] = UNICOde;
        }
    }
    #endregion
    #region *********** SelectedIndexChanged*****************
    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strQry = "";
        if (ddlUser.SelectedIndex > 0)
        {
            strQry = "   select Villagecode  from MstUser   where UserName='" + ddlUser.SelectedValue + "' ";

            DataTable dtUserVillage = objMain.LoadData(strQry);

            string strVillage = dtUserVillage.Rows[0]["Villagecode"].ToString();

            conditions = "mst5Village.ClusterCode in('" + strVillage + "') ";

            strQry = "";
            strQry = "select VillageCode,VillageName  from mst5Village where mst5Village.ClusterCode in('" + strVillage + "')     ";
            strQry += " Union select VillageCode,VillageName  from mstActivityVillage where UserID='" + ddlUser.SelectedValue + "'   ";
            strQry += " Union ";
            strQry += "  select mst5Village.VillageCode,VillageName  from mst5Village  ";
            strQry += " inner join TblActivityUpdate_Office on TblActivityUpdate_Office.VillageCode=mst5Village.VillageCode  ";
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
    protected void ddlVilage_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    public void UserData()
    {
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
        objComman.BindDLL("MstUser", "UserName as UserId,FristName +' ('+ UserName +')' as [UserName] ", conditions, "", "", ddlUser, "UserName", "UserId", "Select");
    }
    protected void ccCritica_CheckedChanged(object sender, EventArgs e)
    {
        if (Chk_Other_Training.Checked == true)
        {
            txtTraingOther.Text = "";
            txtTraingOther.Visible = true;
        }
        else
        {
            txtTraingOther.Text = "";
            txtTraingOther.Visible = false;

        }
    }
    protected void chk_Other_Click(object sender, EventArgs e)
    {
        if (chk_Other.Checked == true)
        {
            txtTraingOtherDec.Text = "";
            txtTraingOtherDec.Visible = true;
        }
        else
        {
            txtTraingOtherDec.Text = "";
            txtTraingOtherDec.Visible = false;

        }
    }

    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        ImageButton bt = (ImageButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string UniqueChildCode = (gvr.FindControl("lblCUniqueChildCode") as Label).Text;
        string lblUserId = (gvr.FindControl("lblUserId") as Label).Text;


        int res1 = objMain.DeleteActivityVillage(lblUserId, UniqueChildCode);

        if (res1 > 0)
        {
           
            ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);
            btnView_Click(btnEdit, null);
            ddlUser_SelectedIndexChanged(ddlUser, null);
        }
        

    }
    #endregion
}