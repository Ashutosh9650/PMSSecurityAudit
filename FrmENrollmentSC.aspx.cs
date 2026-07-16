using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class FrmENrollmentSC : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    string conditions = "";
    Comman objComman = new Comman();
    DataTable dtMain = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {

            if (!IsPostBack)
            {
                btnApprove.Attributes.Add("onclick", "javascript:return " + "confirm('Please confirm if you want to approve? ')");

                btnReject.Attributes.Add("onclick", "javascript:return " + "confirm('Please confirm if you want to Reject? ')");
                LoadData();


            }



        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
    }

    protected void ddlBlock_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillCVillagNew();
        }
        catch (Exception)
        {

            throw;
        }
    }
    public void FillCVillagNew()
    {
        conditions = "";

        string ddlPhan = "";


        conditions = "";

        conditions = "DistrictCode in('" + ddlDistrict.SelectedValue + "')  and BlockCode in('" + ddlBlock.SelectedValue + "') ";

        //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        //objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--All-");

        string strQry = "  SELECT ClusterCode, dbo.TitleCase(upper(ClusterName))  as ClusterName FROM mstCluster where " + conditions + "  order by ClusterName   ";
        DataTable dtDistrict = objMain.LoadData(strQry);


        objComman.BindDLLMasterTable("mstSchool", "ClusterCode,ClusterName", dtDistrict, conditions, "ClusterName", "asc", ddlCluster, "ClusterName", "ClusterCode", "Select");



    }
    public void LoadDataBlock(string blockName)
    {


        conditions = "";
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
        {
            string strQry = "";

            strQry = "Select * from mst3Block  where DistrictCode='" + Session["NewDistrictCode"].ToString() + "' and BlockName='" + blockName + "' ";


            DataTable dtBlock = objMain.LoadData(strQry);

            conditions = "  DistrictCode='" + Session["NewDistrictCode"].ToString() + "'  ";



            objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");
            ddlBlock.Enabled = false;
            ddlBlock.SelectedValue = dtBlock.Rows[0]["BlockCode"].ToString();
            Session["BlockName"] = blockName;
            Session["BlockCodeAct"] = dtBlock.Rows[0]["BlockCode"].ToString();
        }
        else
        {

            conditions = conditions + "  DistrictCode='" + Session["NewDistrictCode"].ToString() + "'  and BlockCode ='" + Session["NewBlockCode"].ToString() + "'   and mst2District.FYear ='" + Session["FinYear"].ToString() + "' ";



            objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

            ddlBlock.Enabled = false;

            ddlBlock.SelectedValue = Session["NewBlockCode"].ToString();
            Session["BlockCodeAct"] = Session["NewBlockCode"].ToString();
        }




    }
    public void LoadData()
    {
        conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
        objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

        ddlState.SelectedIndex = 1;
        ddlState.Enabled = false;
        if (Session["user_level"].ToString() == "145")
        {
            conditions = "";
            conditions = " mst2District.StateCode ='" + ddlState.SelectedValue + "' and UserName='" + Session["username"].ToString() + "' ";
            string strQry1 = "       sELECT distinct mst2District.DistrictCode as DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist  ";
            strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions + "  and  Fyear='2025-2026' order by DistrictName  ";
            DataTable dtDistrict = objMain.LoadData(strQry1);

            objComman.BindDLLDatatable("mst2District", dtDistrict, "DistrictCode, dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "Desc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

        }
        else
        {
            conditions = "";
            conditions = "StateCode ='" + Session["StateCode"].ToString() + "'  and DistrictCode ='" + Session["NewDistrictCode"].ToString() + "'   ";
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");
        }
        ddlDistrict.SelectedIndex = 1;
        ddlDistrict.Enabled = false;
        conditions = "";
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "136")
        {
            conditions = "  DistrictCode='" + Session["NewDistrictCode"].ToString() + "' ";



            objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");
            ddlBlock.Enabled = true;
        }
        else
        {

            conditions = conditions + "  DistrictCode='" + Session["NewDistrictCode"].ToString() + "'  and BlockCode ='" + Session["NewBlockCode"].ToString() + "' ";



            objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

            ddlBlock.Enabled = false;

            ddlBlock.SelectedValue = Session["NewBlockCode"].ToString();
        }

        if (Session["user_level"].ToString() == "145")
        {
            ddlDistrict.SelectedIndex = 0;
            ddlDistrict.Enabled = true;
            ddlBlock.Enabled = true;
        }


    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["Backlk"] = 1;
        Response.Redirect("~/Enrollmentdashboard.aspx");
    }




    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillCBBock();
    }

    public void FillCBBock()
    {
        string conditions = "";
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
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 and BlockCode in( " + Session["BlockCode"].ToString() + " ) and FYear ='2025-2026' ";
        }
        else
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");



    }


    protected void btnReport_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/FrmReportActivityClusterSearch.aspx?ID=" + ddlBlock.SelectedValue + "");

    }


    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {

        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
        SavaData();



    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {

        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
        SavaDataRejecj();



    }
    protected void btnDOwnload_Click(object sender, EventArgs e)
    {
        string Con = " and mst5Village.DistrictCode='" + ddlDistrict.SelectedValue + "' ";
        if (ddlBlock.SelectedIndex > 0)
        {
            Con += " and mst5Village.BlockCode='" + ddlBlock.SelectedValue + "' ";
        }
        if (ddlCluster.SelectedIndex > 0)
        {
            Con += " and mst5Village.ClusterCode='" + ddlCluster.SelectedValue + "' ";
        }

        SqlParameter[] cmdParameters = new SqlParameter[]
       {
            new SqlParameter("@BlockCode",Con),

       };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptEnrollCVReport2024", cmdParameters);
        if (dt.Rows.Count > 0)
        {
            ExporttoExcel(dt, "Enrolment Course Correction");
        }

    }

    protected void btnSerach_Click(object sender, EventArgs e)
    {
        if (ddlBlock.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Block')</script>", false);

            return;
        }
        if (Convert.ToString(Session["username"]) != "")
        {
            LoadDataCV();
        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }



    }

    public void LoadDataCV()
    {
        DataTable dt = LoadActivtiyAllClusterWise();


        if (dt.Rows.Count > 0)
        {
            Gv_Profile_Search.DataSource = dt;
            Gv_Profile_Search.DataBind();
            dvMain.Visible = true;
        }
        else
        {
            Gv_Profile_Search.DataSource = null;
            Gv_Profile_Search.DataBind();
            dvMain.Visible = false;
        }
    }
    public DataTable LoadActivtiyAllClusterWise()
    {
        string Con = " and mst5Village.DistrictCode='" + ddlDistrict.SelectedValue + "' ";
        if (ddlBlock.SelectedIndex > 0)
        {
            Con += " and mst5Village.BlockCode='" + ddlBlock.SelectedValue + "' ";
        }
        if (ddlCluster.SelectedIndex > 0)
        {
            Con += " and mst5Village.ClusterCode='" + ddlCluster.SelectedValue + "' ";
        }


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@BlockCode", Con),

        };
        //  return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptEnrollCV", cmdParameters);
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptEnrollCV2024", cmdParameters);

    }

    //protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string strQry = "";
    //    if (ddlBlock.SelectedIndex > 0)
    //    {
    //        strQry = "   select Villagecode  from MstUser   where UserName='" + ddlBlock.SelectedValue + "' ";
    //        DataTable dtUserVillage = objMain.LoadData(strQry);

    //        string strVillage = dtUserVillage.Rows[0]["Villagecode"].ToString();

    //        conditions = "mst5Village.VillageCode in(" + strVillage + ") ";

    //     //   objComman.BindDLL("mst5Village", "VillageCode,VillageName ", conditions, "", "", ddlVilage, "VillageName", "VillageCode", "Select");


    //    }
    //}


    protected void TestGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //string quantity = e.Row.Cells[3].Text;

            //foreach (TableCell cell in e.Row.Cells)
            //{

            //        cell.BackColor = Color.Red;

            //}
        }
    }
    public void SavaData()
    {
        string FinalFQuesr = "";
        int Fcount = 0;
        int Chcount = 0;
        for (int i = 0; i < Gv_Profile_Search.Rows.Count; i++)
        {
            string FQuesr = " ";
            string FQuesr1 = " update tblEnrolment set ";
            CheckBox chkFormName = (CheckBox)Gv_Profile_Search.Rows[i].FindControl("chkFormName");
            Label lblUniqueChildCode = (Label)Gv_Profile_Search.Rows[i].FindControl("lblUniqueChildCode");

            Label lblGender = (Label)Gv_Profile_Search.Rows[i].FindControl("lblGender");
            Label lblDoa = (Label)Gv_Profile_Search.Rows[i].FindControl("lblDoa");
            Label lblDob = (Label)Gv_Profile_Search.Rows[i].FindControl("lblDob");
            Label lblCol_9 = (Label)Gv_Profile_Search.Rows[i].FindControl("lblCol_9");
            Label lblSCID = (Label)Gv_Profile_Search.Rows[i].FindControl("lblSCID");
            Label lblChildName = (Label)Gv_Profile_Search.Rows[i].FindControl("lblChildName");
            Label lblFatherName = (Label)Gv_Profile_Search.Rows[i].FindControl("lblFatherName");
            Label lblMotherName = (Label)Gv_Profile_Search.Rows[i].FindControl("lblMotherName");
            Label lblClassID = (Label)Gv_Profile_Search.Rows[i].FindControl("lblClassID");
            Label lblcorrect_class = (Label)Gv_Profile_Search.Rows[i].FindControl("lblcorrect_class");

            Label lblSrNo = (Label)Gv_Profile_Search.Rows[i].FindControl("lblSrNo");

            Label lblcorrect_dob = (Label)Gv_Profile_Search.Rows[i].FindControl("lblcorrect_dob");
            Label lblDOB1 = (Label)Gv_Profile_Search.Rows[i].FindControl("lblDOB1");
            Label lblcorrect_soc_cat = (Label)Gv_Profile_Search.Rows[i].FindControl("lblcorrect_soc_cat");
            Label CVUniqueID = (Label)Gv_Profile_Search.Rows[i].FindControl("CVUniqueID");
            Label lblCol_511 = (Label)Gv_Profile_Search.Rows[i].FindControl("lblCol_511");

            if (chkFormName.Checked == true)
            {
                Chcount = Chcount + 1;
                if (lblChildName.Text.Trim().Length > 2)
                {
                    FQuesr += "childname ='" + lblChildName.Text + "' ,";
                }
                if (lblFatherName.Text.Trim().Length > 2)
                {
                    FQuesr += "FatherName ='" + lblFatherName.Text + "' ,";
                }
                if (lblMotherName.Text.Trim().Length > 1)
                {
                    FQuesr += "MotherName ='" + lblMotherName.Text + "' ,";
                }
                if (lblSrNo.Text.Trim().Length > 0)
                {
                    FQuesr += "Serial ='" + lblSrNo.Text + "' ,";
                }
                if (lblcorrect_class.Text.Trim().Length > 2 && lblClassID.Text != "0")
                {
                    FQuesr += "Class ='" + lblClassID.Text + "' ,";
                }
                if (lblcorrect_soc_cat.Text.Trim().Length > 1 && lblSCID.Text.Trim().Length > 0)
                {
                    FQuesr += "Category ='" + lblSCID.Text + "' ,";
                }
                if (lblDOB1.Text.Trim().Length > 3)
                {
                    FQuesr += "[DOB] ='" + Convert.ToDateTime(lblDob.Text).ToString("yyyy-MM-dd") + "' ,";
                }
                if (lblcorrect_dob.Text.Trim().Length > 3)
                {
                    FQuesr += "[EnrolmentDate] ='" + Convert.ToDateTime(lblDoa.Text).ToString("yyyy-MM-dd") + "' ,";
                }
                if (lblCol_511.Text.Trim().Length > 2)
                {
                    FQuesr += "[Gender] ='" + lblGender.Text + "' ,";
                }

                if (FQuesr.Length > 10)
                {
                    string gg = "GETDATE()";
                    FQuesr += "[ENIsApprove] ='1' ,";
                    FQuesr += "[CVApproveby] ='" + Convert.ToString(Session["username"]) + "' ,";
                    FQuesr += "[CVApproveDate] =" + gg + " ,";
                    FQuesr += "[CVUniqueID] ='" + CVUniqueID.Text + "' ,";
                    FQuesr = FQuesr.Substring(0, FQuesr.LastIndexOf(","));
                    FQuesr += "  where UniqueChildCode ='" + lblUniqueChildCode.Text + "'";
                    string Final = FQuesr1 + FQuesr;
                    int icount = SaveENromentCV(lblUniqueChildCode.Text, CVUniqueID.Text, Convert.ToString(Session["username"]), Final, "1");
                    Fcount = icount;
                }


            }
        }
        if (Fcount > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
            LoadDataCV();
        }
        if (Chcount == 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Unique ID')</script>", false);

        }
    }


    public void SavaDataRejecj()
    {
        string FinalFQuesr = "";
        int Fcount = 0;
        int Chcount = 0;
        for (int i = 0; i < Gv_Profile_Search.Rows.Count; i++)
        {

            CheckBox chkFormName = (CheckBox)Gv_Profile_Search.Rows[i].FindControl("chkFormName");
            Label lblUniqueChildCode = (Label)Gv_Profile_Search.Rows[i].FindControl("lblUniqueChildCode");

            Label CVUniqueID = (Label)Gv_Profile_Search.Rows[i].FindControl("CVUniqueID");

            if (chkFormName.Checked == true)
            {

                Chcount = Chcount + 1;


                int icount = SaveENromentCV(lblUniqueChildCode.Text, CVUniqueID.Text, Convert.ToString(Session["username"]), "", "2");
                Fcount = icount;

            }
        }
        if (Fcount > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
            LoadDataCV();
        }
        if (Chcount == 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Unique ID')</script>", false);

        }
    }
    public int SaveENromentCV(string strMainIDNo, string CVUniqueID, string UserName, string Con, string Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqCode", strMainIDNo),
            new SqlParameter("@CVUniqueID", CVUniqueID),
            new SqlParameter("@UserName", UserName),
            new SqlParameter("@Con", Con),
            new SqlParameter("@Flag", Flag),


        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptENrollmentCVInsertUpdateNew", cmdParameters);
    }


    private void ExporttoExcel(DataTable table, string FileName)
    {
        try
        {


            if (table != null)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                string Fullfilename = "" + FileName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";

                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + " ");

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
                int columnscount = table.Columns.Count;


                for (int j = 0; j < columnscount; j++)
                {      //write in new column
                    HttpContext.Current.Response.Write("<Td>");
                    //Get column headers  and make it as bold in excel columns
                    HttpContext.Current.Response.Write("<B>");
                    HttpContext.Current.Response.Write(table.Columns[j]);
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
        catch
        {

            throw;
        }

    }

    //protected void Gv_Profile_Search_OnRowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "GVUIO")
    //    {
    //        int iIndex = Convert.ToInt32(e.CommandArgument);
    //        string VDate = Gv_Profile_Search.DataKeys[iIndex]["VDate"].ToString();
    //        Response.Redirect("./frmMobileVillageProfile.aspx?ID=" + ddlVilage.SelectedValue + "," + ddlBlock.SelectedValue + "," + VDate + "");
    //    }


}

