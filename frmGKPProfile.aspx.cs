using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Drawing;
using System.Data.SqlClient;


public partial class frmGKPProfile : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    string conditions = "";
    Comman objComman = new Comman();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          
          
          
            this.ModalPopupExtender1.Hide();
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            LoadData("");
            //if (Request.QueryString["ID"] != null)
            //{

            //    if (Session["user_level"].ToString() == "19")
            //    {
            //        string Strhh = Convert.ToString(Session["BlockCodeAct"]);
            //        DataTable dt = objMain.GetActivityUserWiseMaxDateNew(ddlUser.SelectedValue, Strhh);
            //        if (dt.Rows.Count > 0)
            //        {
            //            if (Convert.ToString(dt.Rows[0]["ActivityDate"].ToString()) != "")
            //            {
            //                CalendarExtenderTourdate.StartDate = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString()).AddDays(1);
            //            }
            //        }

            //    }
            //    if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
            //    {
            //        string Strhh =Convert.ToString(Session["BlockCodeAct"]);
            //        DataTable dt = objMain.GetActivityUserWiseMaxDateNewIO(ddlUser.SelectedValue, Strhh);
            //        if (dt.Rows.Count > 0)
            //        {
            //            if (Convert.ToString(dt.Rows[0]["ActivityDate"].ToString()) != "")
            //            {
            //                CalendarExtenderTourdate.StartDate = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString()).AddDays(1);
            //            }
            //        }
            //    }
            //    string QueryString = Request.QueryString["ID"];
            //    string[] a = QueryString.Split(',');
            //    txtDate.Text = a[0].ToString();
            //    LoadData(Session["Cluseter"].ToString());
              

            //    string ToDate = txtDate.Text;
            //    string[] c = ToDate.Split('/');
            //    string aToDate = c[2] + '-' + c[1] + '-' + c[0];

            //    string con = "";
            //    DataTable dtMain = null;
            //    if (Session["user_level"].ToString() == "19")
            //    {
            //        con = "ActivityDate =('" + aToDate + "') and  UserEntry=2 and ApproveStatus='FC'   and mstCluster.ClusterCode='" + Session["Cluseter"].ToString() + "' ";
            //        dtMain = objMain.LoadAllActivtiyDatewise(con, 2);

            //    }
            //    if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
            //    {
            //        con = "ActivityDate =('" + aToDate + "')  and  UserEntry=2 and ApproveStatus='B'  and mstCluster.ClusterCode='" + Session["Cluseter"].ToString() + "' ";
            //        dtMain = objMain.LoadAllActivtiyDatewise(con, 2);
            //        // dtMain = objMain.LoadSchoolActivtiyCluseterIO(afromDate, aToDate, ddlBlock.SelectedValue, con);
            //    }
            //    if (dtMain.Rows.Count > 0)
            //    {
            //        ddlUser.SelectedValue = dtMain.Rows[0]["UserName"].ToString();
            //        ddlUser_SelectedIndexChanged(ddlUser, null);
            //        if (ddlUser.SelectedIndex > 0)
            //        {
            //            ddlVilage.SelectedValue = dtMain.Rows[0]["Villagecode"].ToString();
            //            ddlVilage_SelectedIndexChanged(ddlVilage, null);
            //            //  ddlSchool.SelectedValue = dtMain.Rows[0]["SchoolCode"].ToString();

            //            btnSerach_Click(btnSerach, null);
            //        }
            //    }
            //    else
            //    {
            //        ViewState["GUID"] = "";
            //    }
            //    pnlMain.Enabled = false;
               
               
            //}
        }
    }

    public void LoadData(string ClusterName)
    {

        string fromDate = txtDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];




        string strQry = "";
        //strQry = "Select  distinct UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName]  from MstUser  where UserLevel=24 and VillageCode   = '" + Session["Cluseter"].ToString() + "'   ";

        //strQry += "union  ";
        strQry += " Select  distinct UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName]  from MstUser  where UserLevel=24 and UserName in(  ";
        strQry += " select UserID from Tbl_GKP  ";
        strQry += " inner join mst5village on mst5village.villagecode=Tbl_GKP.villagecode  ";
        strQry += " where ActivityDate =('" + afromDate + "') )   ";
        //strQry += " and mst5village.ClusterCode   = '" + Session["Cluseter"].ToString() + "' )    ";


        DataTable dtUser = objMain.LoadData(strQry);
        objComman.BindDLLMasterTable("MstUser", "UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName] ", dtUser, conditions, "", "", ddlUser, "UserName", "UserId", "Select");


        objComman.BindDLL("mstSubject", "SubjectID, SubjectName", conditions, "SubjectID", "asc", ddlSubject, "SubjectName", "SubjectID", "Select");


    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        //  btnApprove.Attributes.Add("onclick", "javascript:return " + "confirm('Please confirm if you want to approve? ')");


       // Response.Redirect("~/FrmActivityDatewiseSearch.aspx?ID=" + Session["CluseterName"].ToString() + "," + Session["FromData"].ToString() + "," + Session["Todate"].ToString() + "");


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
        this.ModalPopupExtender1.Show();
    }
  

    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strQry = "";
        if (ddlUser.SelectedIndex > 0)
        {
            strQry = "   select Villagecode  from MstUser   where UserName='" + ddlUser.SelectedValue + "' ";
            DataTable dtUserVillage = objMain.LoadData(strQry);

            string strVillage = dtUserVillage.Rows[0]["Villagecode"].ToString();

            //conditions = "mst5Village.ClusterCode in('" + strVillage + "') ";

            ////objComman.BindDLL("mst5Village", "VillageCode,VillageName ", conditions, "VillageName", "", ddlVilage, "VillageName", "VillageCode", "Select");
            //strQry = "   select Villagecode  from MstUser   where UserName='" + ddlUser.SelectedValue + "' ";

            //DataTable dtUserVillage = objMain.LoadData(strQry);

            //string strVillage = dtUserVillage.Rows[0]["Villagecode"].ToString();

            conditions = "mst5Village.ClusterCode in('" + strVillage + "') ";

            strQry = "";
            strQry = "select VillageCode,VillageName  from mst5Village where mst5Village.ClusterCode in('" + strVillage + "')  and len(mst5Village.ClusterCode)>2    ";
            strQry += " Union select VillageCode,VillageName  from mstActivityVillage where UserID='" + ddlUser.SelectedValue + "'   ";
            strQry += " Union ";
            strQry += "  select mst5Village.VillageCode,VillageName  from mst5Village  ";
            strQry += " inner join tblActivityUpdate_Village on tblActivityUpdate_Village.VillageCode=mst5Village.VillageCode  ";
            //strQry += "  where mst5Village.ClusterCode in('" + Session["Cluseter"].ToString() + "' )   and UserID='" + ddlUser.SelectedValue + "'   ";
            strQry += "  where UserID='" + ddlUser.SelectedValue + "'   order by VillageName ";
            DataTable dtVillage = objMain.LoadData(strQry);
            //objComman.BindDLLMasterTable("MstUser", "UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName] ", dtUser, conditions, "", "", ddlUser, "UserName", "UserId", "Select");

            objComman.BindDLLMasterTable("mst5Village", "VillageCode,VillageName ", dtVillage, "", "VillageName", "VillageName", ddlVilage, "VillageName", "VillageCode", "Select");

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
          
            this.ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select User')</script>", false);

        }
        if (ddlVilage.SelectedIndex <= 0)
        {
         
            this.ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Village')</script>", false);

        }
        ClearData();
        pnlMain.Enabled = true;
    }
  
    public void ClearData()
    {
        
          
         
    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        ClearData();
      
        if (ddlUser.SelectedIndex <= 0)
        {
           
            this.ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select User')</script>", false);
            return;
        }
        if (ddlVilage.SelectedIndex <= 0)
        {
          
            this.ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Village')</script>", false);
            return;
        }

        if (this.ddlRemark.SelectedIndex > 0)
        {
            this.pnlMain.Enabled = true;
            btnAdd.Visible = true;
        }
        else
        {
            btnAdd.Visible = false;
            this.pnlMain.Enabled = false;
        }
        string Dateof = txtDate.Text;






        string[] b = Dateof.Split('/');

        string FcDate = b[2] + '-' + b[1] + '-' + b[0];

      //string  con = "ActivityDate =('" + FcDate + "')  and  UserEntry=2 and ApproveStatus='B'  and mstCluster.ClusterCode='" + Session["Cluseter"].ToString() + "' ";
        string con = "ActivityDate =('" + FcDate + "')    and Schoolcode='" + ddlSchool.SelectedValue + "' ";


        DataTable dtGKP = objMain.LoadGKPDeatils(con);
        if (dtGKP.Rows.Count > 0)
        {
            if (dtGKP.Rows[0]["ApproveStatus"].ToString() == "B" || dtGKP.Rows[0]["ApproveStatus"].ToString() == "FC" || dtGKP.Rows[0]["ApproveStatus"].ToString() == "I")
            {
                if (Session["user_level"].ToString() == "19" && dtGKP.Rows[0]["ApproveStatus"].ToString() == "FC")
                {
                   // pnlMain.Visible = true;
                }
                else
                {
                   // pnlMain.Visible = false;
                }
                if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
                {
                    if (dtGKP.Rows[0]["ApproveStatus"].ToString() == "B")
                    {
                     //   pnlMain.Visible = true;
                    }
                    else
                    {
                      //  pnlMain.Visible = false;
                    }
                }
            }

        
            if (dtGKP.Rows[0]["Remarks"].ToString().Length > 0)
            {
                ddlRemark.SelectedValue = dtGKP.Rows[0]["Remarks"].ToString();
            }

            gvGkp.DataSource = dtGKP;
            gvGkp.DataBind();
            
        }
        else
        {
            gvGkp.DataSource = null;
            gvGkp.DataBind();
            ViewState["GUID"] = "";
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveData();
    }
    protected void btnAddGkp_Click(object sender, EventArgs e)
    {
        ddlSubject.SelectedIndex = 0;
        ddlLevel.Items.Clear();
        ddlSSession.Items.Clear();
        lblGuId.Text = "";
        MpexdrDistrict8.Show();
    }
    protected void ddlVilage_SelectedIndexChanged(object sender, EventArgs e)
    {

        LoadSchool();
    }
    protected void Gv_Display_RowDataBound(object sender, GridViewRowEventArgs e)
    {
     
    }
    public void LoadSchool()
    {
        conditions = "Villagecode='" + ddlVilage.SelectedValue + "'  ";

        objComman.BindDLL("Mstschool", "SchoolCode ,Name", conditions, "", "", ddlSchool, "Name", "SchoolCode", "Select");

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        LoadSchool();
    }

    public void SaveData()
    {
        string con = "";
         string Dateof = txtDate.Text;

        string[] b = Dateof.Split('/');

        string FcDate = b[2] + '-' + b[1] + '-' + b[0];

        if (ddlSubject.SelectedIndex <= 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Subject')</script>", false);
            MpexdrDistrict8.Show();
            return;
        }
        if (ddlLevel.SelectedIndex <= 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Level')</script>", false);
            MpexdrDistrict8.Show();
            return;
        }
        if (ddlSubject.SelectedIndex <= 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Session')</script>", false);
            MpexdrDistrict8.Show();
            return;
        }

        if (lblGuId.Text.Length > 2)
        {
            con = "where ActivityDate =('" + FcDate + "') and GUID_GKP not in('" + ddlSchool.SelectedValue + "')     and Schoolcode='" + ddlSchool.SelectedValue + "' and  SubjectID='" + ddlSubject.SelectedValue + "'  and  LevelID='" + ddlLevel.SelectedValue + "'  and  Session='" + ddlSSession.SelectedItem.Text + "'  ";

            DataTable dt = objMain.LoadCheckGkp(con);
            if (dt.Rows.Count > 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('This Activty Allreday Exit')</script>", false);
                MpexdrDistrict8.Show();
                return;
            }
        }
        else
        {
            con = "where ActivityDate =('" + FcDate + "')    and Schoolcode='" + ddlSchool.SelectedValue + "' and  SubjectID='" + ddlSubject.SelectedValue + "'  and  LevelID='" + ddlLevel.SelectedValue + "'  and  Session='" + ddlSSession.SelectedItem.Text + "'  ";
            DataTable dt = objMain.LoadCheckGkp(con);
            if (dt.Rows.Count > 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('This Activty Allreday Exit')</script>", false);
                MpexdrDistrict8.Show();
                return;
            }
        }

        string GUId = "";
        string Flag = "";
        Int32 TB=0;
        Int32 FC = 0;
        if (lblGuId.Text.Length > 2)
        {
            GUId = lblGuId.Text;
            Flag = "P";
        }
        else
        {
            GUId = objMain.Generate_RandomString(8);
            Flag = "I";
        }
        if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
        {
            FC=1;
        }
         if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
        {
            TB = 1;
        }

         SqlParameter[] parm = new SqlParameter[]
            {
           
           
            new SqlParameter("@UserID", ddlUser.SelectedValue),
            new SqlParameter("@GUID_GKP", GUId),
            new SqlParameter("@SubjectID", ddlSubject.SelectedValue),
            new SqlParameter("@LevelID", ddlLevel.SelectedItem.Text),
            new SqlParameter("@Session", ddlSSession.SelectedItem.Text),
            new SqlParameter("@GKP_fc", FC),
            new SqlParameter("@GKP_tb", TB),
            new SqlParameter("@SchoolCode", ddlSchool.SelectedValue),
             new SqlParameter("@VillageCode", ddlVilage.SelectedValue),
             new SqlParameter("@ActivityDate",Convert.ToDateTime(FcDate)),
              new SqlParameter("@Flag", Flag),
            
              };
         int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateGkp", parm);

         if (result > 0)
         {
             ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Save Sucessfully')</script>", false);
             btnSerach_Click(btnSerach, null);
         }
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        objComman.BindDLLNew("mstGKPDeatils", "Level", "SubjectID='" + ddlSubject.SelectedValue + "' ", "Level", "asc", ddlLevel, "Level", "Level", "Select");
        MpexdrDistrict8.Show();
    }
    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        objComman.BindDLL("mstGKPDeatils", "'Session'+' '+ CONVERT(varchar,NoofLevel) as Session,NoofLevel", "SubjectID='" + ddlSubject.SelectedValue + "' and  Level='" + ddlLevel.SelectedValue + "' ", "'Session'+' '+ CONVERT(varchar,NoofLevel) ", "asc", ddlSSession, "Session", "NoofLevel", "Select");
        MpexdrDistrict8.Show();
    }
    protected void LnkBtnBlock_OnClick(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string UniqueCode = (gvr.FindControl("lblCUniqueChildCode") as Label).Text;
        string lblsubjectid = (gvr.FindControl("lblsubjectid") as Label).Text;
        string lblLevelID = (gvr.FindControl("lblLevelID") as Label).Text;
        string lblSession = (gvr.FindControl("lblSession") as Label).Text;
        string lblgkp_fc = (gvr.FindControl("lblgkp_fc") as Label).Text;
        string lblgkp_tb = (gvr.FindControl("lblgkp_tb") as Label).Text;
        lblGuId.Text = UniqueCode;
        ddlSubject.SelectedValue = lblsubjectid;
        ddlSubject_SelectedIndexChanged(ddlSubject, null);
        int index = ddlLevel.Items.IndexOf(ddlLevel.Items.FindByText(lblLevelID.Trim()));
        if (index != -1)
        {
            ddlLevel.SelectedIndex = index;
        }
        ddlLevel_SelectedIndexChanged(ddlLevel, null);

        int index1 = ddlSSession.Items.IndexOf(ddlSSession.Items.FindByText(lblSession.Trim()));
        if (index1 != -1)
        {
            ddlSSession.SelectedIndex = index1;
        }
        if (lblgkp_fc == "1")
        {
            rblApprove.SelectedValue = "1";
        }
        if (lblgkp_tb == "1")
        {
            rblApprove.SelectedValue = "2";
        }
        MpexdrDistrict8.Show();
        //Label lblStatus = (Label)gvr.FindControl("lblStatus");
        //Session["UnquieId"] = UniqueChildCode;
        //Session["StateCode"] = ddlState.SelectedValue;
        //Session["DistCode"] = ddlDistrict.SelectedValue;
        //Session["BlockCode"] = ddlBlock.SelectedValue;
        //Session["PhanyCode"] = ddlPanchayat.SelectedValue;
        //Session["VillCode"] = ddlVillage.SelectedValue;


    }
 
    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        ImageButton bt = (ImageButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string UniqueChildCode = (gvr.FindControl("lblCUniqueChildCode") as Label).Text;

      
       

    }
}