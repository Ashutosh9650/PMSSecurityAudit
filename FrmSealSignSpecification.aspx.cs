using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrmSealSignSpecification : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    SqlHelper Sh = new SqlHelper();
    string conditions = "";
    string statecode = string.Empty, Clustercode = string.Empty, Distcode = string.Empty, blockcode = string.Empty, villagecode = string.Empty, dbname = "", FormName = string.Empty;
    int RowNumber = 0;
    SqlInjection sqlInjection = new SqlInjection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                LoadYear();
                LoadUserLeavel();
                ddlYear.SelectedIndex = 1;
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
            if (Request.QueryString["ID"] != null)
            {
                string QueryString = Request.QueryString["ID"];
                string[] a = QueryString.Split(',');
                if (Session["user_level"].ToString() == "145")
                {
                    string strQry = "Select * from mst3Block  where Blockcode='" + Convert.ToString(a[0].ToString()) + "' ";


                    DataTable dtBlock = objMain.LoadData(strQry);
                    ddlDistrict.SelectedValue = dtBlock.Rows[0]["DistrictCode"].ToString();
                    ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
                }
                else
                {
                    ddlDistrict.SelectedValue = Session["NewDistrictCode"].ToString();
                }

                ddlBlock.SelectedValue = Convert.ToString(a[0].ToString());
                ddlBlock_SelectedIndexChanged(ddlBlock, null);
                ddlserachblock.SelectedValue = Convert.ToString(a[0].ToString());
                ddlBlockserachblock_SelectedIndexChanged(ddlBlock, null);
                ddlPanchayat.SelectedValue = Convert.ToString(a[1].ToString());
                ddlCluster.SelectedValue = Convert.ToString(a[1].ToString());
                ddlPanchayat_SelectedIndexChanged(ddlPanchayat, null);
                ddlCluster_SelectedIndexChanged(ddlPanchayat, null);
                string s = a[2];
                foreach (ListItem item in ddlVillage.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;

                    }
                }
                //foreach (ListItem item in ddlVillageD2d.Items)
                //{
                //    if (item.Value == s)
                //    {
                //        item.Selected = true;

                //    }
                //}

            }
            TabContainer1.ActiveTabIndex = 0;

            if (Convert.ToString(Session["user_level"]) == "39" || Convert.ToString(Session["user_level"]) == "30" || Convert.ToString(Session["user_level"]) == "136" || Convert.ToString(Session["user_level"]) == "145")
            {
                btnSumbit.Visible = true;
                //TabPanel2.Visible = false;
            }
            else if (Convert.ToString(Session["user_level"]) == "19" || Convert.ToString(Session["user_level"]) == "137")
            {
                btnSumbit.Visible = false;

            }
            /// BtnBoSubmit.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
        }

    }
    #region Button click event
    protected void btnSdh_Click(object sender, EventArgs e)
    {
        if (pnlMainddd.Visible == true)
        {
            pnlMainddd.Visible = false;
        }
        else
        {
            pnlMainddd.Visible = true;
        }
    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        try
        {
            LoadReport();
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnSumbit_Click(object sender, EventArgs e)
    {
        try
        {
            int Ret = 0;
            //for (int i = 0; i < gvReport.Rows.Count; i++)
            //{
            //Label lblUniqueCode = (Label)gvReport.Rows[i].FindControl("lblUniqueCode");

            Ret = Insert_Update(lblUniqueCode.Text, "", 6);
            // Ret++;
            // }
            if (Ret > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Submit sucessfully')</script>", false);
                LoadReport();
                return;
            }
        }
        catch
        {

            throw;
        }
    }
    protected void ImgOutDur_Click(object sender, EventArgs e)
    {
        DataTable Ds_gvReport1 = Session["OutofDoorD2d"] as DataTable;
        DataRow[] drArr1 = null;
        string StrRo = "RO";
        string StrMo = "MO";

        Session["OutofDoorD2d"] = Ds_gvReport1;
        if (ddl_MatchByOut.SelectedIndex > 0 && ddl_MatchByOut.SelectedValue == "1")
        {
            DataTable Ds_gvReport = Session["OutofDoorD2d"] as DataTable;
            DataView DV = Ds_gvReport.DefaultView;
            DV.RowFilter = string.Format("VillageName LIKE '%{0}%'", Txt_VillageOUT.Text);
            gvReport.DataSource = null;
            gvReport.DataBind();
            gvReport.DataSource = DV;
            gvReport.DataBind();
            Session["SearchOutOfD2d"] = DV;
        }
        else if (ddl_MatchByOut.SelectedIndex > 0 && ddl_MatchByOut.SelectedValue == "2")
        {
            DataTable Ds_gvReport = Session["OutofDoorD2d"] as DataTable;
            DataView DV = Ds_gvReport.DefaultView;
            DV.RowFilter = string.Format("House LIKE '%{0}%'", Txt_VillageOUT.Text);
            gvReport.DataSource = null;
            gvReport.DataBind();
            gvReport.DataSource = DV;
            gvReport.DataBind();
            Session["SearchOutOfD2d"] = DV;
        }
        else if (ddl_MatchByOut.SelectedIndex > 0 && ddl_MatchByOut.SelectedValue == "3")
        {
            DataTable Ds_gvReport = Session["OutofDoorD2d"] as DataTable;
            DataView DV = Ds_gvReport.DefaultView;
            DV.RowFilter = string.Format("ChildName LIKE '%{0}%'", Txt_VillageOUT.Text);
            gvReport.DataSource = null;
            gvReport.DataBind();
            gvReport.DataSource = DV;
            gvReport.DataBind();
            Session["SearchOutOfD2d"] = DV;
        }
        else if (ddl_MatchByOut.SelectedIndex > 0 && ddl_MatchByOut.SelectedValue == "4")
        {
            DataTable Ds_gvReport = Session["OutofDoorD2d"] as DataTable;
            DataView DV = Ds_gvReport.DefaultView;
            DV.RowFilter = string.Format("FathersName LIKE '%{0}%'", Txt_VillageOUT.Text);
            gvReport.DataSource = null;
            gvReport.DataBind();
            gvReport.DataSource = DV;
            gvReport.DataBind();
            Session["SearchOutOfD2d"] = DV;
        }
        else if (ddl_MatchByOut.SelectedIndex > 0 && ddl_MatchByOut.SelectedValue == "5")
        {
            DataTable Ds_gvReport = Session["OutofDoorD2d"] as DataTable;
            DataView DV = Ds_gvReport.DefaultView;
            DV.RowFilter = string.Format("UniqueId LIKE '%{0}%'", Txt_VillageOUT.Text);
            gvReport.DataSource = null;
            gvReport.DataBind();
            gvReport.DataSource = DV;
            gvReport.DataBind();
            Session["SearchOutOfD2d"] = DV;
        }
        else
        {
            DataTable Ds_gvReport = Session["OutofDoorD2d"] as DataTable;
            DataView DV = Ds_gvReport.DefaultView;
            DV.RowFilter = string.Format("FathersName LIKE '%{0}%'", Txt_VillageOUT.Text);
            gvReport.DataSource = DV;
            gvReport.DataBind();
            Session["SearchOutOfD2d"] = DV;

        }
    }
    protected void IMG_DTDSerch_Click(object sender, EventArgs e)
    {
        DataTable Ds_gvReport1 = Session["D2d"] as DataTable;
        DataRow[] drArr1 = null;
        string StrRo = "RD";
        string StrMo = "MD";
        drArr1 = Ds_gvReport1.Select("TempId ='" + StrMo + "' or K ='" + StrRo + "'  ");
        if (drArr1.Length > 0)
        {
            foreach (DataRow row in drArr1)
            {
                Ds_gvReport1.Rows.Remove(row);
            }

            Ds_gvReport1.AcceptChanges();
        }

        Session["D2d"] = Ds_gvReport1;
        if (ddl_MatchDTD.SelectedIndex > 0 && ddl_MatchDTD.SelectedValue == "1")
        {
            DataTable Ds_gvD2d = Session["D2d"] as DataTable;
            DataView DV = Ds_gvD2d.DefaultView;
            DV.RowFilter = string.Format("VillageName LIKE '%{0}%'", Txt_VillageDTD.Text);
            gvD2d.DataSource = null;
            gvD2d.DataBind();
            gvD2d.DataSource = DV;
            gvD2d.DataBind();
            Session["SearchD2d"] = DV;
        }
        else if (ddl_MatchDTD.SelectedIndex > 0 && ddl_MatchDTD.SelectedValue == "2")
        {
            DataTable Ds_gvD2d = Session["D2d"] as DataTable;
            DataView DV = Ds_gvD2d.DefaultView;
            DV.RowFilter = string.Format("House LIKE '%{0}%'", Txt_VillageDTD.Text);
            gvD2d.DataSource = null;
            gvD2d.DataBind();
            gvD2d.DataSource = DV;
            gvD2d.DataBind();
            Session["SearchD2d"] = DV;
        }
        else if (ddl_MatchDTD.SelectedIndex > 0 && ddl_MatchDTD.SelectedValue == "3")
        {
            DataTable Ds_gvD2d = Session["D2d"] as DataTable;
            DataView DV = Ds_gvD2d.DefaultView;
            DV.RowFilter = string.Format("ChildName LIKE '%{0}%'", Txt_VillageDTD.Text);
            gvD2d.DataSource = null;
            gvD2d.DataBind();
            gvD2d.DataSource = DV;
            gvD2d.DataBind();
            Session["SearchD2d"] = DV;
        }
        else if (ddl_MatchDTD.SelectedIndex > 0 && ddl_MatchDTD.SelectedValue == "4")
        {
            DataTable Ds_gvD2d = Session["D2d"] as DataTable;
            DataView DV = Ds_gvD2d.DefaultView;
            DV.RowFilter = string.Format("FathersName LIKE '%{0}%'", Txt_VillageDTD.Text);
            gvD2d.DataSource = null;
            gvD2d.DataBind();
            gvD2d.DataSource = DV;
            gvD2d.DataBind();
            Session["SearchD2d"] = DV;
        }
        else if (ddl_MatchDTD.SelectedIndex > 0 && ddl_MatchDTD.SelectedValue == "5")
        {
            DataTable Ds_gvD2d = Session["D2d"] as DataTable;
            DataView DV = Ds_gvD2d.DefaultView;
            DV.RowFilter = string.Format("UniqueId LIKE '%{0}%'", Txt_VillageDTD.Text);
            gvD2d.DataSource = null;
            gvD2d.DataBind();
            gvD2d.DataSource = DV;
            gvD2d.DataBind();
            Session["SearchD2d"] = DV;
        }
        else
        {
            DataTable Ds_gvD2d = Session["D2d"] as DataTable;
            DataView DV = Ds_gvD2d.DefaultView;
            DV.RowFilter = string.Format("FathersName LIKE '%{0}%'", Txt_VillageDTD.Text);
            gvD2d.DataSource = DV;
            gvD2d.DataBind();
            Session["SearchD2d"] = DV;
        }

    }
    protected void ImgOutDur1_Click(object sender, EventArgs e)
    {
        DataTable Ds_gvReport1 = Session["OutofDoorD2d1"] as DataTable;
        DataRow[] drArr1 = null;
        string StrRo = "RO";
        string StrMo = "MO";

        Session["OutofDoorD2d1"] = Ds_gvReport1;
        if (ddlSearch1.SelectedIndex > 0 && ddlSearch1.SelectedValue == "1")
        {
            DataTable Ds_gvReport = Session["OutofDoorD2d1"] as DataTable;
            DataView DV = Ds_gvReport.DefaultView;
            DV.RowFilter = string.Format("VillageName LIKE '%{0}%'", TxtSearch1.Text);
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView1.DataSource = DV;
            GridView1.DataBind();
            Session["SearchOutOfD2d1"] = DV.ToTable();
            ImageButton5.Enabled = true;
            txtSearchHHNO2.Enabled = true;
        }
        else if (ddlSearch1.SelectedIndex > 0 && ddlSearch1.SelectedValue == "2")
        {
            DataTable Ds_gvReport = Session["OutofDoorD2d1"] as DataTable;
            DataView DV = Ds_gvReport.DefaultView;
            DV.RowFilter = string.Format("House LIKE '%{0}%'", TxtSearch1.Text);
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView1.DataSource = DV;
            GridView1.DataBind();
            Session["SearchOutOfD2d1"] = DV.ToTable();
            ImageButton5.Enabled = true;
            txtSearchHHNO2.Enabled = true;
        }
        else if (ddlSearch1.SelectedIndex > 0 && ddlSearch1.SelectedValue == "3")
        {
            DataTable Ds_gvReport = Session["OutofDoorD2d1"] as DataTable;
            DataView DV = Ds_gvReport.DefaultView;
            DV.RowFilter = string.Format("ChildName LIKE '%{0}%'", TxtSearch1.Text);
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView1.DataSource = DV;
            GridView1.DataBind();
            Session["SearchOutOfD2d1"] = DV.ToTable();
            ImageButton5.Enabled = true;
            txtSearchHHNO2.Enabled = true;
        }
        else if (ddlSearch1.SelectedIndex > 0 && ddlSearch1.SelectedValue == "4")
        {
            DataTable Ds_gvReport = Session["OutofDoorD2d1"] as DataTable;
            DataView DV = Ds_gvReport.DefaultView;
            DV.RowFilter = string.Format("FathersName LIKE '%{0}%'", TxtSearch1.Text);
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView1.DataSource = DV;
            GridView1.DataBind();
            Session["SearchOutOfD2d1"] = DV.ToTable();
            ImageButton5.Enabled = true;
            txtSearchHHNO2.Enabled = true;
        }
        else if (ddlSearch1.SelectedIndex > 0 && ddlSearch1.SelectedValue == "5")
        {
            DataTable Ds_gvReport = Session["OutofDoorD2d1"] as DataTable;
            DataView DV = Ds_gvReport.DefaultView;
            DV.RowFilter = string.Format("UniqueId LIKE '%{0}%'", TxtSearch1.Text);
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView1.DataSource = DV;
            gvReport.DataBind();
            Session["SearchOutOfD2d1"] = DV.ToTable();
            ImageButton5.Enabled = true;
            txtSearchHHNO2.Enabled = true;
        }
        else
        {
            DataTable Ds_gvReport = Session["OutofDoorD2d1"] as DataTable;
            DataView DV = Ds_gvReport.DefaultView;
            // DV.RowFilter = string.Format("FathersName LIKE '%{0}%'", TxtSearch1.Text);
            DV.RowFilter = string.Format("UniqueId LIKE '%{0}%' OR House LIKE '%{0}%' OR VillageName LIKE '%{0}%' OR ChildName LIKE '%{0}%' OR FathersName LIKE '%{0}%'", TxtSearch1.Text);
            GridView1.DataSource = null;
            GridView1.DataSource = DV;
            GridView1.DataBind();
            Session["SearchOutOfD2d1"] = DV.ToTable();
            ImageButton5.Enabled = true;
            txtSearchHHNO2.Enabled = true;
        }
    }

    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        DataTable Ds_gvReport = Session["OutofDoorD2d1"] as DataTable;
        DataView DV = Ds_gvReport.DefaultView;
        DV.RowFilter = string.Format("UniqueId LIKE '%{0}%' OR House LIKE '%{0}%' OR VillageName LIKE '%{0}%' OR ChildName LIKE '%{0}%' OR FathersName LIKE '%{0}%'", txtSearchHHNO2.Text);
        GridView1.DataSource = DV;
        GridView1.DataBind();
    }

    protected void IMG_DTDSerch1_Click(object sender, EventArgs e)
    {
        DataTable Ds_gvReport1 = Session["D2d1"] as DataTable;
        //DataRow[] drArr1 = null;
        //string StrRo = "RD";
        //string StrMo = "MD";
        //drArr1 = Ds_gvReport1.Select("TempId ='" + StrMo + "' or K ='" + StrRo + "'  ");
        //if (drArr1.Length > 0)
        //{
        //    foreach (DataRow row in drArr1)
        //    {
        //        Ds_gvReport1.Rows.Remove(row);
        //    }

        //    Ds_gvReport1.AcceptChanges();
        //}

        Session["D2d1"] = Ds_gvReport1;

        DataTable dtk = new DataTable();

        if (ddlS2.SelectedIndex > 0 && ddlS2.SelectedValue == "1")
        {
            DataTable Ds_GridView2 = Session["D2d1"] as DataTable;
            DataView DV = Ds_GridView2.DefaultView;
            DV.RowFilter = string.Format("VillageName LIKE '%{0}%'", TxtSearch2.Text);
            //GridView2.DataSource = null;
            //GridView2.DataBind();
            GridView2.DataSource = DV;
            GridView2.DataBind();
            Session["SearchD2d1"] = DV.ToTable();
            ImageButton4.Enabled = true;
            txtSearchHHNo.Enabled = true;
        }
        else if (ddlS2.SelectedIndex > 0 && ddlS2.SelectedValue == "2")
        {
            DataTable Ds_GridView2 = Session["D2d1"] as DataTable;
            DataView DV = Ds_GridView2.DefaultView;
            DV.RowFilter = string.Format("House LIKE '%{0}%'", TxtSearch2.Text);
            //GridView2.DataSource = null;
            //GridView2.DataBind();
            GridView2.DataSource = DV;
            GridView2.DataBind();
            Session["SearchD2d1"] = DV.ToTable();
            ImageButton4.Enabled = true;
            txtSearchHHNo.Enabled = true;
        }
        else if (ddlS2.SelectedIndex > 0 && ddlS2.SelectedValue == "3")
        {
            DataTable Ds_GridView2 = Session["D2d1"] as DataTable;
            DataView DV = Ds_GridView2.DefaultView;
            DV.RowFilter = string.Format("ChildName LIKE '%{0}%'", TxtSearch2.Text);
            //GridView2.DataSource = null;
            //GridView2.DataBind();
            GridView2.DataSource = DV;
            GridView2.DataBind();
            Session["SearchD2d1"] = DV.ToTable();
            ImageButton4.Enabled = true;
            txtSearchHHNo.Enabled = true;
        }
        else if (ddlS2.SelectedIndex > 0 && ddlS2.SelectedValue == "4")
        {
            DataTable Ds_GridView2 = Session["D2d1"] as DataTable;
            DataView DV = Ds_GridView2.DefaultView;
            DV.RowFilter = string.Format("FathersName LIKE '%{0}%'", TxtSearch2.Text);
            //GridView2.DataSource = null;
            //GridView2.DataBind();
            GridView2.DataSource = DV;
            GridView2.DataBind();
            Session["SearchD2d1"] = DV.ToTable();
            ImageButton4.Enabled = true;
            txtSearchHHNo.Enabled = true;
        }
        else if (ddlS2.SelectedIndex > 0 && ddlS2.SelectedValue == "5")
        {
            DataTable Ds_GridView2 = Session["D2d1"] as DataTable;
            DataView DV = Ds_GridView2.DefaultView;
            DV.RowFilter = string.Format("UniqueId LIKE '%{0}%'", TxtSearch2.Text);
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView2.DataSource = DV;
            GridView2.DataBind();
            Session["SearchD2d1"] = DV.ToTable();
            ImageButton4.Enabled = true;
            txtSearchHHNo.Enabled = true;
        }
        else
        {
            DataTable Ds_GridView2 = Session["D2d1"] as DataTable;
            DataView DV = Ds_GridView2.DefaultView;
            DV.RowFilter = string.Format("UniqueId LIKE '%{0}%' OR House LIKE '%{0}%' OR VillageName LIKE '%{0}%' OR ChildName LIKE '%{0}%' OR FathersName LIKE '%{0}%'", TxtSearch2.Text);
            GridView2.DataSource = DV;
            GridView2.DataBind();
            Session["SearchD2d1"] = DV.ToTable();
            ImageButton4.Enabled = true;
            txtSearchHHNo.Enabled = true;
        }

    }
    public void ImageBn4_Click(object sender, EventArgs e)
    {
        DataTable Ds_gvReport2 = Session["SearchD2d1"] as DataTable;
        DataRow[] drArr1 = null;
        string StrRo = "RD";
        string StrMo = "MD";
        //drArr1 = Ds_gvReport2.Select("TempId ='" + StrMo + "' or K ='" + StrRo + "'  ");
        //if (drArr1.Length > 0)
        //{
        //    foreach (DataRow row in drArr1)
        //    {
        //        Ds_gvReport2.Rows.Remove(row);
        //    }

        //    Ds_gvReport2.AcceptChanges();
        //}
        DataTable Ds_GridView2 = Session["SearchD2d1"] as DataTable;
        DataView DV = Ds_GridView2.DefaultView;
        DV.RowFilter = string.Format("UniqueId LIKE '%{0}%' OR House LIKE '%{0}%' OR VillageName LIKE '%{0}%' OR ChildName LIKE '%{0}%' OR FathersName LIKE '%{0}%'", txtSearchHHNo.Text);
        GridView2.DataSource = DV;
        GridView2.DataBind();

    }
    protected void btnMatch1_Click(object sender, EventArgs e)
    {
        int indcount1 = 0, indD2d = 0;


        foreach (GridViewRow Itemst in gvD2d.Rows)
        {
            if (((CheckBox)Itemst.FindControl("ChkD2d")).Checked)
            {
                indD2d++;
            }

        }

        if (indD2d > 1)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select single matching entry from D2d list')</script>", false);
            return;
        }
        if (indD2d == 1)
        {
            MatchData(indcount1, indD2d);

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select matching entry from D2d list')</script>", false);
            return;
        }
    }
    protected void btnMatch_Click(object sender, EventArgs e)
    {
        int indcount1 = 0, indD2d = 0;


        foreach (GridViewRow Itemst in gvD2d.Rows)
        {
            if (((CheckBox)Itemst.FindControl("ChkD2d")).Checked)
            {
                indD2d++;
            }

        }

        if (indD2d > 1)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select single matching entry from D2d list')</script>", false);
            return;
        }
        string UniqueIDLeft = lblUniqueCode.Text;
        string UniqueIDRight = "";
        foreach (GridViewRow Itemst in gvD2d.Rows)
        {
            if (((CheckBox)Itemst.FindControl("ChkD2d")).Checked)
            {
                Label lblD2dUniqueCode = (Label)Itemst.FindControl("lblD2dUniqueCode");
                UniqueIDRight = lblD2dUniqueCode.Text;
            }
        }



        string strQry = "     select UniqueChildCode as de,DOBNew,	CChildName,	FatherName, IsD2dContact from rptTblOSSCDeatils with(nolock) where UniqueChildCode='" + UniqueIDRight + "' and  IsD2dContact =1  ";
        DataTable dtV = objMain.LoadData(strQry);
        if (dtV != null && dtV.Rows.Count > 0)
        {
            foreach (DataRow row in dtV.Rows)
            {
                string encryptionKey = row["UniqueChildCode"].ToString();

                if (dtV.Columns.Contains("ChildName") && row["ChildName"] != DBNull.Value)
                    row["ChildName"] = sqlInjection.DecryptMatchingWithSessionMasking(row["ChildName"].ToString(), "ChildName");

                if (dtV.Columns.Contains("FathersName") && row["FathersName"] != DBNull.Value)
                    row["FathersName"] = sqlInjection.DecryptMatchingWithSessionMasking(row["FathersName"].ToString(), "FathersName");

                if (dtV.Columns.Contains("DOB") && row["DOB"] != DBNull.Value)
                {
                    string encryptedDOB = row["DOB"].ToString();
                    string decryptedDOB = sqlInjection.DecryptMatchingWithSessionMasking(encryptedDOB, "DOB", false);

                    row["DOB"] = decryptedDOB;

                    sqlInjection.ProcessRowAgeAndDob(row, decryptedDOB);
                }
            }
        }
        if (dtV.Rows.Count > 0)
        {
            string strQry1 = "     select DOB,	ChildName,	FatherName from tblENrolment with(nolock) where UniqueChildCode='" + UniqueIDLeft + "'   ";
            DataTable dtV1 = objMain.LoadData(strQry1);
            if (dtV1 != null && dtV1.Rows.Count > 0)
            {
                foreach (DataRow row in dtV1.Rows)
                {
                    string encryptionKey = row["UniqueChildCode"].ToString();

                    if (dtV1.Columns.Contains("ChildName") && row["ChildName"] != DBNull.Value)
                        row["ChildName"] = sqlInjection.DecryptMatchingWithSessionMasking(row["ChildName"].ToString(), "ChildName");

                    if (dtV1.Columns.Contains("FathersName") && row["FathersName"] != DBNull.Value)
                        row["FathersName"] = sqlInjection.DecryptMatchingWithSessionMasking(row["FathersName"].ToString(), "FathersName");

                    if (dtV1.Columns.Contains("DOB") && row["DOB"] != DBNull.Value)
                    {
                        string encryptedDOB = row["DOB"].ToString();
                        string decryptedDOB = sqlInjection.DecryptMatchingWithSessionMasking(encryptedDOB, "DOB", false);

                        row["DOB"] = decryptedDOB;

                        sqlInjection.ProcessRowAgeAndDob(row, decryptedDOB);
                    }
                }
            }
            string msg = "";
            Label5.Text = "";
            Label4.Text = "";
            Label3.Text = "";
            if (dtV.Rows[0]["DOBNew"].ToString().Length > 0)
            {
                msg = "Contact DOB =" + Convert.ToDateTime(dtV.Rows[0]["DOBNew"].ToString()).ToString("dd/MM/yyy") + " and Enrolment DOB =" + Convert.ToDateTime(dtV1.Rows[0]["DOB"].ToString()).ToString("dd/MM/yyy") + " Mismatch ";
                Label3.Text = msg;
            }
            if (dtV.Rows[0]["CChildName"].ToString().Length > 0)
            {
                msg = "\r\nContact ChildName =" + dtV.Rows[0]["CChildName"].ToString().ToUpper() + " and Enrolment ChildName =" + dtV1.Rows[0]["ChildName"].ToString().ToUpper() + " Mismatch ";
                Label4.Text = msg;
            }
            if (dtV.Rows[0]["FatherName"].ToString().Length > 0)
            {
                msg = "\r\nContact FatherName =" + dtV.Rows[0]["FatherName"].ToString().ToUpper() + " and Enrolment FatherName =" + dtV1.Rows[0]["FatherName"].ToString().ToUpper() + " Mismatch ";
                Label5.Text = msg;
            }

            Button2.Visible = false;
            Button4.Visible = true;
            MpexdrDistrict.Show();
        }
        else
        {


            if (indD2d == 1)
            {
                MatchData(indcount1, indD2d);

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select matching entry from D2d list')</script>", false);
                return;
            }
        }

    }
    protected void BtnBoSubmit1_Click(object sender, EventArgs e)
    {
        try
        {
            int indcount1 = 0, indD2d = 0;
            string UniqueIDLeft = "";
            string EDob = "";
            string EChildName = "";
            string EFatherName = "";
            string DDob = "";
            string DChildName = "";
            string DFatherName = "";
            string D2dContact = "";
            foreach (GridViewRow Itemst in GridView1.Rows)
            {
                if (((CheckBox)Itemst.FindControl("Chk1")).Checked)
                {
                    Label lblDOB = (Label)Itemst.FindControl("lblDOB");
                    Label lblChildName = (Label)Itemst.FindControl("lblChildName");
                    Label lblFathersName = (Label)Itemst.FindControl("lblFathersName");
                    EDob = lblDOB.Text;
                    EChildName = lblChildName.Text;
                    EFatherName = lblFathersName.Text;
                    indD2d++;

                }

            }
            foreach (GridViewRow Itemst in GridView2.Rows)
            {
                if (((CheckBox)Itemst.FindControl("ChkD2d")).Checked)
                {
                    Label lblDOB = (Label)Itemst.FindControl("lblDOBNew");
                    Label lblChildName = (Label)Itemst.FindControl("lblCChildName");
                    Label lblFathersName = (Label)Itemst.FindControl("lblFatherName");
                    Label lblIsD2dContact = (Label)Itemst.FindControl("lblIsD2dContact");
                    DDob = lblDOB.Text;
                    DChildName = lblChildName.Text;
                    DFatherName = lblFathersName.Text;
                    D2dContact = lblIsD2dContact.Text;
                    indcount1++;
                }

            }


            if (indD2d == 1 && indcount1 == 1)
            {
                string msg = "";

                MatchData(indcount1, indD2d);

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select single matching entry')</script>", false);
                return;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void BtnBoSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int indcount1 = 0, indD2d = 0;
            string UniqueIDLeft = "";
            string EDob = "";
            string EChildName = "";
            string EFatherName = "";
            string DDob = "";
            string DChildName = "";
            string DFatherName = "";
            string D2dContact = "";
            foreach (GridViewRow Itemst in GridView1.Rows)
            {
                if (((CheckBox)Itemst.FindControl("Chk1")).Checked)
                {
                    Label lblDOB = (Label)Itemst.FindControl("lblDOB");
                    Label lblChildName = (Label)Itemst.FindControl("lblChildName");
                    Label lblFathersName = (Label)Itemst.FindControl("lblFathersName");
                    EDob = lblDOB.Text;
                    EChildName = lblChildName.Text;
                    EFatherName = lblFathersName.Text;
                    indD2d++;

                }

            }
            foreach (GridViewRow Itemst in GridView2.Rows)
            {
                if (((CheckBox)Itemst.FindControl("ChkD2d")).Checked)
                {
                    Label lblDOB = (Label)Itemst.FindControl("lblDOBNew");
                    Label lblChildName = (Label)Itemst.FindControl("lblCChildName");
                    Label lblFathersName = (Label)Itemst.FindControl("lblFatherName");
                    Label lblIsD2dContact = (Label)Itemst.FindControl("lblIsD2dContact");
                    DDob = lblDOB.Text;
                    DChildName = lblChildName.Text;
                    DFatherName = lblFathersName.Text;
                    D2dContact = lblIsD2dContact.Text;
                    indcount1++;
                }

            }


            if (indD2d == 1 && indcount1 == 1)
            {
                string msg = "";
                Label5.Text = "";
                Label4.Text = "";
                Label3.Text = "";
                if (DDob.Length > 0)
                {
                    msg = "Contact DOB =" + Convert.ToDateTime(DDob).ToString("dd/MM/yyy") + " and Enrolment DOB =" + Convert.ToDateTime(EDob).ToString("dd/MM/yyy") + " Mismatch ";
                    Label3.Text = msg;
                }
                if (DChildName.Length > 0)
                {
                    msg = "Contact ChildName =" + DChildName.ToUpper() + " and Enrolment ChildName =" + EChildName.ToUpper() + " Mismatch ";
                    Label4.Text = msg;
                }
                if (DFatherName.Length > 0)
                {
                    msg = "Contact Father Name =" + DFatherName.ToUpper() + " and Enrolment Father Name =" + EChildName.ToUpper() + " Mismatch ";
                    Label5.Text = msg;
                }
                if (D2dContact == "1")
                {
                    Label3.Text = msg;
                    Button2.Visible = true;
                    Button4.Visible = false;
                    MpexdrDistrict.Show();

                    //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    //sb.Append("<script type = 'text/javascript'>");
                    //sb.Append("window.onload=function(){");
                    //sb.Append("confirm('");
                    //sb.Append("Hi");
                    //sb.Append("')};");
                    //sb.Append("</script>");
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "confirm", sb.ToString());

                }
                else
                {

                    MatchData(indcount1, indD2d);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select single matching entry')</script>", false);
                return;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    public void BOSave()
    {
        try
        {
            int indcount1 = 0, indD2d = 0;
            string UniqueIDLeft = "";
            string EDob = "";
            string EChildName = "";
            string EFatherName = "";
            string DDob = "";
            string DChildName = "";
            string DFatherName = "";
            string D2dContact = "";
            foreach (GridViewRow Itemst in GridView1.Rows)
            {
                if (((CheckBox)Itemst.FindControl("Chk1")).Checked)
                {
                    Label lblDOB = (Label)Itemst.FindControl("lblDOB");
                    Label lblChildName = (Label)Itemst.FindControl("lblChildName");
                    Label lblFathersName = (Label)Itemst.FindControl("lblFathersName");
                    EDob = lblDOB.Text;
                    EChildName = lblChildName.Text;
                    EFatherName = lblFathersName.Text;
                    indD2d++;

                }

            }
            foreach (GridViewRow Itemst in GridView2.Rows)
            {
                if (((CheckBox)Itemst.FindControl("ChkD2d")).Checked)
                {
                    Label lblDOB = (Label)Itemst.FindControl("lblDOBNew");
                    Label lblChildName = (Label)Itemst.FindControl("lblCChildName");
                    Label lblFathersName = (Label)Itemst.FindControl("lblFatherName");
                    Label lblIsD2dContact = (Label)Itemst.FindControl("lblIsD2dContact");
                    DDob = lblDOB.Text;
                    DChildName = lblChildName.Text;
                    DFatherName = lblFathersName.Text;
                    D2dContact = lblIsD2dContact.Text;
                    indcount1++;
                }

            }


            if (indD2d == 1 && indcount1 == 1)
            {
                string msg = "";
                if (DDob.Length > 0)
                {
                    msg = "Contact DOB =" + Convert.ToDateTime(DDob).ToString("dd/MM/yyy") + " and Enrolment DOB =" + Convert.ToDateTime(EDob).ToString("dd/MM/yyy") + " Mismatch ";
                }
                if (DChildName.Length > 0)
                {
                    msg += "Contact ChildName =" + DChildName.ToUpper() + " and Enrolment ChildName =" + EChildName.ToUpper() + " Mismatch ";
                }
                if (DFatherName.Length > 0)
                {
                    msg += "Contact ChildName =" + DFatherName.ToUpper() + " and Enrolment ChildName =" + EChildName.ToUpper() + " Mismatch ";
                }
                if (D2dContact == "1")
                {
                    BtnBoSubmit.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
                    //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    //sb.Append("<script type = 'text/javascript'>");
                    //sb.Append("window.onload=function(){");
                    //sb.Append("confirm('");
                    //sb.Append("Hi");
                    //sb.Append("')};");
                    //sb.Append("</script>");
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "confirm", sb.ToString());

                }

                // MatchData(indcount1, indD2d);

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select single matching entry')</script>", false);
                return;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void MatchData(int indcount2, int indD2d)
    {
        string UniqueIDLeft = "", UniqueIDRight = "";
        if (Convert.ToString(Session["user_level"]) == "39" || Convert.ToString(Session["user_level"]) == "30" || Convert.ToString(Session["user_level"]) == "136" || Convert.ToString(Session["user_level"]) == "145")
        {

            if (TabContainer1.ActiveTabIndex == 0)
            {
                UniqueIDLeft = lblUniqueCode.Text;
                foreach (GridViewRow Itemst in gvD2d.Rows)
                {
                    if (((CheckBox)Itemst.FindControl("ChkD2d")).Checked)
                    {
                        Label lblD2dUniqueCode = (Label)Itemst.FindControl("lblD2dUniqueCode");
                        UniqueIDRight = lblD2dUniqueCode.Text;
                    }
                }
                if (UniqueIDRight != "" && UniqueIDLeft != "")
                {
                    int Ret = Insert_Update(UniqueIDLeft, UniqueIDRight, 1);
                    if (Ret > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Submit sucessfully')</script>", false);

                        gvD2d.DataSource = null;
                        gvD2d.DataBind();
                        //LoadReport();

                        DataRow[] drArr1 = null;
                        DataTable dt = Session["OutofDoorD2d"] as DataTable;

                        drArr1 = dt.Select("UniqueCode ='" + UniqueIDLeft + "'   ");
                        if (drArr1.Length > 0)
                        {
                            foreach (DataRow row in drArr1)
                            {
                                dt.Rows.Remove(row);
                            }

                            dt.AcceptChanges();
                        }
                        Session["OutofDoorD2d"] = dt;
                        gvReport.DataSource = dt;
                        gvReport.DataBind();

                        DataRow[] drArr5 = null;
                        DataTable dt5 = Session["OutofDoorD2d1"] as DataTable;

                        drArr5 = dt5.Select("UniqueCode ='" + UniqueIDLeft + "'   ");
                        if (drArr5.Length > 0)
                        {
                            foreach (DataRow row in drArr5)
                            {
                                dt5.Rows.Remove(row);
                            }

                            dt5.AcceptChanges();
                        }
                        Session["OutofDoorD2d1"] = dt5;
                        GridView1.DataSource = dt5;
                        GridView1.DataBind();

                        return;
                    }
                }
            }
            else
            {
                foreach (GridViewRow Itemst in GridView1.Rows)
                {
                    if (((CheckBox)Itemst.FindControl("Chk1")).Checked)
                    {
                        Label lblUniqueCodechk1 = (Label)Itemst.FindControl("lblUniqueCode");
                        UniqueIDLeft = lblUniqueCodechk1.Text;
                    }
                }
                foreach (GridViewRow Itemst in GridView2.Rows)
                {
                    if (((CheckBox)Itemst.FindControl("ChkD2d")).Checked)
                    {
                        Label lblD2dUniqueCode = (Label)Itemst.FindControl("lblD2dUniqueCode");
                        UniqueIDRight = lblD2dUniqueCode.Text;
                    }
                }
                if (UniqueIDRight != "" && UniqueIDLeft != "")
                {
                    int Ret = Insert_Update(UniqueIDLeft, UniqueIDRight, 4);
                    if (Ret > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Submit sucessfully')</script>", false);
                        //LoadReport();


                        DataRow[] drArr1 = null;
                        DataTable dt = Session["OutofDoorD2d1"] as DataTable;

                        drArr1 = dt.Select("UniqueCode ='" + UniqueIDLeft + "'   ");
                        if (drArr1.Length > 0)
                        {
                            foreach (DataRow row in drArr1)
                            {
                                dt.Rows.Remove(row);
                            }

                            dt.AcceptChanges();
                        }
                        Session["OutofDoorD2d1"] = dt;
                        GridView1.DataSource = dt;
                        GridView1.DataBind();



                        DataRow[] drArr2 = null;
                        DataTable dt1 = Session["D2d1"] as DataTable;

                        drArr2 = dt1.Select("UniqueCode ='" + UniqueIDRight + "'   ");
                        if (drArr2.Length > 0)
                        {
                            foreach (DataRow row in drArr2)
                            {
                                dt1.Rows.Remove(row);
                            }

                            dt1.AcceptChanges();
                        }
                        Session["D2d1"] = dt1;
                        GridView2.DataSource = dt1;
                        GridView2.DataBind();

                        DataRow[] drArr3 = null;
                        DataTable dt3 = Session["OutofDoorD2d"] as DataTable;

                        drArr3 = dt3.Select("UniqueCode ='" + UniqueIDLeft + "'   ");
                        if (drArr3.Length > 0)
                        {
                            foreach (DataRow row in drArr3)
                            {
                                dt3.Rows.Remove(row);
                            }

                            dt3.AcceptChanges();
                        }
                        Session["OutofDoorD2d"] = dt3;
                        gvReport.DataSource = dt3;
                        gvReport.DataBind();

                        return;
                    }
                }
            }

        }
        else if (Convert.ToString(Session["user_level"]) == "19" || Convert.ToString(Session["user_level"]) == "137")
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                UniqueIDLeft = lblUniqueCode.Text;
                foreach (GridViewRow Itemst in gvD2d.Rows)
                {
                    if (((CheckBox)Itemst.FindControl("ChkD2d")).Checked)
                    {
                        Label lblD2dUniqueCode = (Label)Itemst.FindControl("lblD2dUniqueCode");
                        UniqueIDRight = lblD2dUniqueCode.Text;
                    }
                }
                if (UniqueIDRight != "" && UniqueIDLeft != "")
                {
                    int Ret = Insert_Update(UniqueIDLeft, UniqueIDRight, 8);
                    if (Ret > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Submit sucessfully')</script>", false);

                        gvD2d.DataSource = null;
                        gvD2d.DataBind();
                        //LoadReport();
                        DataRow[] drArr1 = null;
                        DataTable dt = Session["OutofDoorD2d"] as DataTable;

                        drArr1 = dt.Select("UniqueCode ='" + UniqueIDLeft + "'   ");
                        if (drArr1.Length > 0)
                        {
                            foreach (DataRow row in drArr1)
                            {
                                dt.Rows.Remove(row);
                            }

                            dt.AcceptChanges();
                        }
                        Session["OutofDoorD2d"] = dt;
                        gvReport.DataSource = dt;
                        gvReport.DataBind();
                        DataRow[] drArr5 = null;
                        DataTable dt5 = Session["OutofDoorD2d1"] as DataTable;

                        drArr5 = dt5.Select("UniqueCode ='" + UniqueIDLeft + "'   ");
                        if (drArr5.Length > 0)
                        {
                            foreach (DataRow row in drArr5)
                            {
                                dt5.Rows.Remove(row);
                            }

                            dt5.AcceptChanges();
                        }
                        Session["OutofDoorD2d1"] = dt5;
                        GridView1.DataSource = dt5;
                        GridView1.DataBind();
                        return;
                    }
                }
            }
            else
            {
                foreach (GridViewRow Itemst in GridView1.Rows)
                {
                    if (((CheckBox)Itemst.FindControl("Chk1")).Checked)
                    {
                        Label lblUniqueCodechk1 = (Label)Itemst.FindControl("lblUniqueCode");
                        UniqueIDLeft = lblUniqueCodechk1.Text;
                    }
                }
                foreach (GridViewRow Itemst in GridView2.Rows)
                {
                    if (((CheckBox)Itemst.FindControl("ChkD2d")).Checked)
                    {
                        Label lblD2dUniqueCode = (Label)Itemst.FindControl("lblD2dUniqueCode");
                        UniqueIDRight = lblD2dUniqueCode.Text;
                    }
                }
                if (UniqueIDRight != "" && UniqueIDLeft != "")
                {

                    int Ret = Insert_Update(UniqueIDLeft, UniqueIDRight, 10);
                    if (Ret > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Submit sucessfully')</script>", false);
                        // LoadReport();

                        DataRow[] drArr1 = null;
                        DataTable dt = Session["OutofDoorD2d1"] as DataTable;

                        drArr1 = dt.Select("UniqueCode ='" + UniqueIDLeft + "'   ");
                        if (drArr1.Length > 0)
                        {
                            foreach (DataRow row in drArr1)
                            {
                                dt.Rows.Remove(row);
                            }

                            dt.AcceptChanges();
                        }
                        Session["OutofDoorD2d1"] = dt;
                        GridView1.DataSource = dt;
                        GridView1.DataBind();



                        DataRow[] drArr2 = null;
                        DataTable dt1 = Session["D2d1"] as DataTable;

                        drArr2 = dt1.Select("UniqueCode ='" + UniqueIDRight + "'   ");
                        if (drArr2.Length > 0)
                        {
                            foreach (DataRow row in drArr2)
                            {
                                dt1.Rows.Remove(row);
                            }

                            dt1.AcceptChanges();
                        }
                        Session["D2d1"] = dt1;
                        GridView2.DataSource = dt1;
                        GridView2.DataBind();

                        DataRow[] drArr3 = null;
                        DataTable dt3 = Session["OutofDoorD2d"] as DataTable;

                        drArr3 = dt3.Select("UniqueCode ='" + UniqueIDLeft + "'   ");
                        if (drArr3.Length > 0)
                        {
                            foreach (DataRow row in drArr3)
                            {
                                dt3.Rows.Remove(row);
                            }

                            dt3.AcceptChanges();
                        }
                        Session["OutofDoorD2d"] = dt3;
                        gvReport.DataSource = dt3;
                        gvReport.DataBind();
                        return;
                    }
                }
            }


        }



    }
    private int Insert_Update(string UniqueIDLeft, string UniqueIDRight, int Flag)
    {
        SqlConnection dbSqlconnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            dbSqlconnection.Open();
            using (SqlCommand dbSqlCommand = (SqlCommand)dbSqlconnection.CreateCommand())
            {
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = "SP_Seal_Sign_Specification_Update";
                dbSqlCommand.Parameters.AddWithValue("@UniqueCodeE", UniqueIDLeft);
                dbSqlCommand.Parameters.AddWithValue("@UniqueCodeD", UniqueIDRight);
                dbSqlCommand.Parameters.AddWithValue("@Flag", Flag);
                SqlParameter ReturnAffectedRows = new SqlParameter("@RowAffected", System.Data.SqlDbType.Int);
                ReturnAffectedRows.Direction = ParameterDirection.Output;
                dbSqlCommand.Parameters.Add(ReturnAffectedRows);
                dbSqlCommand.ExecuteNonQuery();
                int _returnRow = Convert.ToInt32(ReturnAffectedRows.Value);
                return _returnRow;
            }
        }
        catch (SqlException exp)
        {
            throw;
        }
        finally
        {
            dbSqlconnection.Dispose();
        }
    }
    protected void btnRest_Click(object sender, EventArgs e)
    {
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/FrmEnrollmentBlockWiseMatching.aspx");
    }

    //[System.Web.Services.WebMethod]
    //protected void WebMethodCall(string UniqueCode)
    //{
    //    try
    //    {
    //        string FristCon = "";
    //        FristCon = FristCon + " m.EnrollmentUniqueCode='" + lblUniqueCode.Text + "' and mst5Village.Fyear='" + ddlYear.SelectedItem.Text + "'";
    //        DataTable dt2 = EnrollmentRightGrid(FristCon);
    //        if (dt2.Rows.Count > 0)
    //        {

    //            gvD2d.Visible = true;
    //            IMG_DTDSerch.Enabled = true;
    //            dt2.DefaultView.Sort = "ChildName asc,fathersName asc";
    //            gvD2d.DataSource = dt2.DefaultView.ToTable();
    //            gvD2d.DataBind();
    //            UpdGrdLeftID.Focus();
    //            Session["D2d"] = dt2;
    //        }
    //        else
    //        {
    //            gvD2d.DataSource = null;
    //            gvD2d.DataBind();
    //        }
    //    }
    //    catch
    //    {

    //        throw;
    //    }
    //}
    protected void GVMain_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        int row1 = int.Parse(e.CommandArgument.ToString());
        if (e.CommandName == "GVUIO")
        {
            int iIndex = Convert.ToInt32(e.CommandArgument);
            lblUniqueCode.Text = gvReport.DataKeys[iIndex]["UniqueCode"].ToString();
            // FillControls(TBCode);
            Int32 Flag = 0;
            if (Convert.ToString(Session["user_level"]) == "39" || Convert.ToString(Session["user_level"]) == "30" || Convert.ToString(Session["user_level"]) == "136" || Convert.ToString(Session["user_level"]) == "145")
            {

                Flag = 1;
            }
            else if (Convert.ToString(Session["user_level"]) == "19" || Convert.ToString(Session["user_level"]) == "137")
            {


                Flag = 2;
            }
            string FristCon = "";
            FristCon = FristCon + " m.EnrollmentUniqueCode='" + lblUniqueCode.Text + "' and mst5Village.Fyear='" + ddlYear.SelectedItem.Text + "'";
            DataTable dt2 = EnrollmentRightGrid(FristCon, Flag);
            if (dt2 != null && dt2.Rows.Count > 0)
            {
                foreach (DataRow rows in dt2.Rows)
                {
                    string encryptionKey = rows["UniqueChildCode"].ToString();

                    if (dt2.Columns.Contains("ChildName") && rows["ChildName"] != DBNull.Value)
                        rows["ChildName"] = sqlInjection.DecryptMatchingWithSessionMasking(rows["ChildName"].ToString(), "ChildName");

                    if (dt2.Columns.Contains("FathersName") && rows["FathersName"] != DBNull.Value)
                        rows["FathersName"] = sqlInjection.DecryptMatchingWithSessionMasking(rows["FathersName"].ToString(), "FathersName");

                    if (dt2.Columns.Contains("DOB") && rows["DOB"] != DBNull.Value)
                    {
                        string encryptedDOB = rows["DOB"].ToString();
                        string decryptedDOB = sqlInjection.DecryptMatchingWithSessionMasking(encryptedDOB, "DOB", false);

                        rows["DOB"] = decryptedDOB;

                        sqlInjection.ProcessRowAgeAndDob(rows, decryptedDOB);
                    }
                }
            }
            if (dt2.Rows.Count > 0)
            {

                gvD2d.Visible = true;
                IMG_DTDSerch.Enabled = true;
                dt2.DefaultView.Sort = "ChildName asc,fathersName asc";
                gvD2d.DataSource = dt2.DefaultView.ToTable();
                gvD2d.DataBind();
                UpdGrdLeftID.Focus();
                Session["D2d"] = dt2;
            }
            else
            {
                gvD2d.DataSource = null;
                gvD2d.DataBind();
            }

            for (int i = 0; i < gvReport.Rows.Count; i++)
            {
                GridViewRow RowD = gvReport.Rows[i];
                if (i % 2 == 0)
                {
                    RowD.BackColor = Color.White;
                }
                else
                {
                    RowD.BackColor = Color.FromArgb(245, 245, 245);
                }

            }
            GridViewRow row = gvReport.Rows[iIndex];
            row.BackColor = Color.FromArgb(255, 255, 0);
            Session["D2d"] = dt2;
            gvReport.SelectedIndex = row1;
            gvReport.SelectedRow.Focus();
            //if (Convert.ToString(row.RowState) == "Selected" || Convert.ToString(row.RowState) == "Alternate, Selected")
            //{               
            //    if (iIndex % 2 == 0)
            //    {
            //        row.BackColor = Color.White;
            //    }
            //    else
            //    {
            //         row.BackColor = Color.FromArgb(245, 245, 245);
            //    }

            //    Session["D2d"] = dt2;
            //    gvReport.SelectedIndex = row1;
            //    gvReport.SelectedRow.Focus();
            //}
            //else
            //{


            //}
        }
    }

    //protected void lnk_Onclick(object sender, EventArgs e)
    //{
    //    LinkButton lnk = sender as LinkButton;
    //    GridViewRow row = (GridViewRow)lnk.NamingContainer;
    //    int indx = row.RowIndex;
    //    Label lblUniqueCode = (Label)gvReport.Rows[indx].FindControl("lblUniqueCode");
    //    string FristCon = "";
    //    FristCon = FristCon + " m.EnrollmentUniqueCode='" + lblUniqueCode.Text + "' and mst5Village.Fyear='" + ddlYear.SelectedItem.Text + "'";
    //    DataTable dt2 = EnrollmentRightGrid(FristCon);
    //    if (dt2.Rows.Count > 0)
    //    {

    //        gvD2d.Visible = true;
    //        IMG_DTDSerch.Enabled = true;
    //        dt2.DefaultView.Sort = "ChildName asc,fathersName asc";
    //        gvD2d.DataSource = dt2.DefaultView.ToTable();
    //        gvD2d.DataBind();
    //        Session["D2d"] = dt2;
    //    }
    //    else
    //    {
    //        gvD2d.DataSource = null;
    //        gvD2d.DataBind();
    //    }

    //    for (int i = 0; i < gvReport.Rows.Count; i++)
    //    {
    //        GridViewRow RowD = gvReport.Rows[i];
    //        if (i % 2 == 0)
    //        {
    //            RowD.BackColor = Color.White;
    //        }
    //        else
    //        {
    //            RowD.BackColor = Color.FromArgb(245, 245, 245);
    //        }

    //    }
    //    //GridViewRow row = gvReport.Rows[indx];
    //    //row.BackColor = Color.LightYellow;
    //}

    public DataTable OutD2dEnrollmentLeftGrid(string Frist, Int32 Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
{
new SqlParameter("@condtion", Frist),
new SqlParameter("@Flag", Flag),
};
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptOutOfD2dandSealSignSpecificationLeftNew]", cmdParameters);
    }
    public DataTable OutD2dEnrollmentLeftGridManualMatching(string Frist, Int32 Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
{
new SqlParameter("@condtion", Frist),
new SqlParameter("@Flag", Flag),
};
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptOutOfD2dandSealSignSpecificationLeftNew_Manual_matching]", cmdParameters);
    }
    public DataTable EnrollmentRightGrid(string Frist, Int32 Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
{
new SqlParameter("@condtion", Frist),
new SqlParameter("@Flag", Flag)
};
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPtentialMatchesSealSignSpecificationRight]", cmdParameters);
    }

    public DataTable EnrollmentRightGrid_20200613(string Frist)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
{
new SqlParameter("@condtion", Frist)
};
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptD2dandEnrollmentVerfiy_20200613]", cmdParameters);
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
    }
    #endregion
    #region Fill Master Data
    public void LoadYear()
    {
        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");
        ddlYear.SelectedIndex = 1;
    }
    public void LoadUserLeavel()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
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
        if (Session["user_level_Role"].ToString() == "2")
        {
            //conditions = "";
            //conditions = "StateCode ='" + ddlState.SelectedValue + "' ";
            //objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");
            //ddlDistrict.SelectedIndex = 0;
            ddlState_SelectedIndexChanged(ddlDistrict, null);

        }
        else
        {
            conditions = "";
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and Fyear= '" + ddlYear.SelectedItem.Text + "' ";
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");
            ddlDistrict.SelectedIndex = 1;
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
        }
    }
    public void Locking()
    {
        if (ddlYear.SelectedIndex > 0)
        {

            //ImageButton1.Enabled = true;
            //btnSumbit.Enabled = true;
            //btnMove.Enabled = true;
            if (Session["FinYear"].ToString() != ddlYear.SelectedItem.Text)
            {
                string strQry;
                strQry = "Select * from mstModuleLocking  where [FromName]='Menual Match' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "'";


                DataTable dtModel = objMain.LoadData(strQry);
                if (dtModel.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtModel.Rows[0]["LockMonth"].ToString()) < DateTime.Today.Month)
                    {
                        //ImageButton1.Enabled = false;
                        btnSumbit.Enabled = false;
                        btnMove.Enabled = false;

                    }

                }

            }
            btnSumbit.Enabled = true;
            btnMove.Enabled = true;
        }
    }
    public void FillCBState()
    {
        conditions = "";
        objComman.BindDLL(ViewState["TableName"].ToString(), "S.StateCode,dbo.TitleCase(upper(S.StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");
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
    public void FillCBBock()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        if (Session["user_level"].ToString() == "19")
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 and BlockCode in(  " + Session["BlockCode"].ToString() + " )";
        }
        else
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlserachblock, "BlockName", "BlockCode", "--Select--");

    }
    public void FillCBCluster()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "'";

        string strQry = "  SELECT ClusterCode, dbo.TitleCase(upper(ClusterName)) as ClusterName FROM mstcluster  where " + conditions + "  union   SELECT '0' ClusterCode,'--All--' as ClusterName FROM mstcluster   union   SELECT '1' ClusterCode,'--Blank Cluster---' as ClusterName order by ClusterName   ";
        DataTable dtVillage = objMain.LoadData(strQry);
        ddlPanchayat.DataSource = dtVillage;
        ddlPanchayat.DataTextField = "ClusterName";
        ddlPanchayat.DataValueField = "ClusterCode";
        ddlPanchayat.DataBind();

        // objComman.BindDLL("mstcluster", "ClusterCode,dbo.TitleCase(upper(ClusterName)) as ClusterName", conditions, "ClusterName", "asc", ddlPanchayat, "ClusterName", "ClusterCode", "--Select--");
        //objComman.BindDLL("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "--Select--");
    }
    public void FillCBClusterNew()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlserachblock.SelectedValue + "'";
        objComman.BindDLL("mstcluster", "ClusterCode,dbo.TitleCase(upper(ClusterName)) as ClusterName", conditions, "ClusterName", "asc", ddlCluster, "ClusterName", "ClusterCode", "--Select--");
        //objComman.BindDLL("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "--Select--");
    }
    public void FillCVillage()
    {
        conditions = "";
        if (ddlPanchayat.SelectedValue == "1")
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and (ClusterCode='' or  ClusterCode is null or ClusterCode='0') ";

        }
        else
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  ClusterCode='" + ddlPanchayat.SelectedValue + "'  ";
        }
        string strQry = "  SELECT mst5Village.VillageCode, dbo.TitleCase(upper(VillageName)) as VillageName FROM mst5Village  where " + conditions + "  order by VillageName   ";
        DataTable dtVillage = objMain.LoadData(strQry);
        ddlVillage.DataSource = dtVillage;
        ddlVillage.DataTextField = "VillageName";
        ddlVillage.DataValueField = "VillageCode";
        ddlVillage.DataBind();

    }
    public void FillCVillageNew()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlserachblock.SelectedValue + "' and  ClusterCode='" + ddlCluster.SelectedValue + "'  ";
        string strQry = "  SELECT mst5Village.VillageCode, dbo.TitleCase(upper(VillageName)) as VillageName FROM mst5Village  where " + conditions + "  order by VillageName   ";
        DataTable dtVillage = objMain.LoadData(strQry);
        ddlVillageD2d.DataSource = dtVillage;
        ddlVillageD2d.DataTextField = "VillageName";
        ddlVillageD2d.DataValueField = "VillageCode";
        ddlVillageD2d.DataBind();

    }

    public string FilterCondition()
    {
        conditions = "";
        string Village = "";
        foreach (ListItem item in ddlVillage.Items)
        {
            if (item.Selected)
            {

                Village += "'" + item.Value + "'" + ",";
            }
        }
        if (Village.Length > 0)
        {
            Village = Village.Substring(0, Village.LastIndexOf(","));

        }
        if (ddlState.SelectedIndex > 0)
        {
            conditions += "  mst5Village.StateCode = '" + ddlState.SelectedValue + "' ";
        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " and mst5Village.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";
        }
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions += " and mst5Village.BlockCode = '" + ddlBlock.SelectedValue + "' ";
        }


        if (ddlPanchayat.SelectedValue == "1")
        {

        }
        else
        {
            if (ddlPanchayat.SelectedIndex > 0)
            {
                conditions += " and mst5Village.ClusterCode = '" + ddlPanchayat.SelectedValue + "' ";
            }
        }

        if (Village.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in( " + Village + ") ";
        }

        return conditions;
    }

    public string FilterConditionNew()
    {
        conditions = "";
        string Village = "";
        string VillageNe2 = "";
        foreach (ListItem item in ddlVillage.Items)
        {
            if (item.Selected)
            {

                Village += "'" + item.Value + "'" + ",";
            }
        }
        foreach (ListItem item in ddlVillageD2d.Items)
        {
            if (item.Selected)
            {

                VillageNe2 += "'" + item.Value + "'" + ",";
            }
        }
        if (Village.Length > 0)
        {
            Village = Village.Substring(0, Village.LastIndexOf(","));

        }
        if (VillageNe2.Length > 0)
        {
            VillageNe2 = VillageNe2.Substring(0, VillageNe2.LastIndexOf(","));

        }
        if (ddlState.SelectedIndex > 0)
        {
            conditions += "  mst5Village.StateCode = '" + ddlState.SelectedValue + "' ";
        }
        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions += " and mst5Village.DistrictCode = '" + ddlDistrict.SelectedValue + "' ";
        }

        if (pnlMainddd.Visible == true)
        {
            if (ddlserachblock.SelectedIndex > 0)
            {
                conditions += " and mst5Village.BlockCode = '" + ddlserachblock.SelectedValue + "' ";
            }
            if (ddlCluster.SelectedIndex > 0)
            {
                conditions += " and mst5Village.ClusterCode = '" + ddlCluster.SelectedValue + "' ";
            }
            if (VillageNe2.Length > 0)
            {
                conditions += " and mst5Village.VillageCode in( " + VillageNe2 + ") ";
            }

        }
        else
        {
            if (ddlBlock.SelectedIndex > 0)
            {
                conditions += " and mst5Village.BlockCode = '" + ddlBlock.SelectedValue + "' ";
            }
            if (ddlCluster.SelectedIndex > 0)
            {
                conditions += " and mst5Village.ClusterCode = '" + ddlPanchayat.SelectedValue + "' ";
            }
            if (Village.Length > 0)
            {
                conditions += " and mst5Village.VillageCode in( " + Village + ") ";
            }
        }

        return conditions;
    }
    public void LoadReport()
    {

        string FristCon = FilterCondition();
        string FristConNew = FilterConditionNew();
        string FristCon1 = FristCon;
        Int32 Flag = 0;
        if (Convert.ToString(Session["user_level"]) == "39" || Convert.ToString(Session["user_level"]) == "30" || Convert.ToString(Session["user_level"]) == "136" || Convert.ToString(Session["user_level"]) == "145")
        {
            FristCon1 += " and IsDoBoFlag =1 ";
            Flag = 1;
        }
        else if (Convert.ToString(Session["user_level"]) == "19" || Convert.ToString(Session["user_level"]) == "137")
        {

            FristCon1 += " and IsDoBoFlag =2 ";
            Flag = 2;
        }
        DataTable dt = OutD2dEnrollmentLeftGrid(FristCon1, Flag);
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                string encryptionKey = row["UniqueCode"].ToString();

                if (dt.Columns.Contains("ChildName") && row["ChildName"] != DBNull.Value)
                    row["ChildName"] = sqlInjection.DecryptMatchingWithSessionMasking(row["ChildName"].ToString(), "ChildName");

                if (dt.Columns.Contains("FathersName") && row["FathersName"] != DBNull.Value)
                    row["FathersName"] = sqlInjection.DecryptMatchingWithSessionMasking(row["FathersName"].ToString(), "FathersName");

                if (dt.Columns.Contains("DOB") && row["DOB"] != DBNull.Value)
                {
                    string encryptedDOB = row["DOB"].ToString();
                    string decryptedDOB = sqlInjection.DecryptMatchingWithSessionMasking(encryptedDOB, "DOB", false);

                    row["DOB"] = decryptedDOB;

                    sqlInjection.ProcessRowAgeAndDob(row, decryptedDOB);
                }
            }
        }
        DataTable dtManual = OutD2dEnrollmentLeftGridManualMatching(FristCon1, Flag);
        if (dtManual != null && dtManual.Rows.Count > 0)
        {
            foreach (DataRow row in dtManual.Rows)
            {
                string encryptionKey = row["UniqueCode"].ToString();

                if (dtManual.Columns.Contains("ChildName") && row["ChildName"] != DBNull.Value)
                    row["ChildName"] = sqlInjection.DecryptMatchingWithSessionMasking(row["ChildName"].ToString(), "ChildName");

                if (dtManual.Columns.Contains("FathersName") && row["FathersName"] != DBNull.Value)
                    row["FathersName"] = sqlInjection.DecryptMatchingWithSessionMasking(row["FathersName"].ToString(), "FathersName");

                if (dtManual.Columns.Contains("DOB") && row["DOB"] != DBNull.Value)
                {
                    string encryptedDOB = row["DOB"].ToString();
                    string decryptedDOB = sqlInjection.DecryptMatchingWithSessionMasking(encryptedDOB, "DOB", false);

                    row["DOB"] = decryptedDOB;

                    sqlInjection.ProcessRowAgeAndDob(row, decryptedDOB);
                }
            }
        }
        DataTable dt2 = EnrollmentRightGrid_20200613(FristConNew);
        if (dt2 != null && dt2.Rows.Count > 0)
        {
            foreach (DataRow row in dt2.Rows)
            {
                string encryptionKey = row["UniqueCode"].ToString();

                if (dt2.Columns.Contains("ChildName") && row["ChildName"] != DBNull.Value)
                    row["ChildName"] = sqlInjection.DecryptMatchingWithSessionMasking(row["ChildName"].ToString(), "ChildName");

                if (dt2.Columns.Contains("FathersName") && row["FathersName"] != DBNull.Value)
                    row["FathersName"] = sqlInjection.DecryptMatchingWithSessionMasking(row["FathersName"].ToString(), "FathersName");
                if (dt2.Columns.Contains("Mauhalla") && row["Mauhalla"] != DBNull.Value)
                    row["Mauhalla"] = sqlInjection.DecryptMatchingWithSessionMasking(row["Mauhalla"].ToString(), "Mauhalla");


            }
        }
        if (dt.Rows.Count > 0 || dtManual.Rows.Count > 0)
        {
            gvReport.Visible = true;
            ImgOutDur.Enabled = true;
            ImageButton2.Enabled = true;
            ImageButton3.Enabled = true;

            dt.DefaultView.Sort = "MatchingCount desc,ChildName asc,fathersName asc";
            dtManual.DefaultView.Sort = "MatchingCount desc,ChildName asc,fathersName asc";
            if (Convert.ToString(Session["user_level"]) == "39" || Convert.ToString(Session["user_level"]) == "30" || Convert.ToString(Session["user_level"]) == "136" || Convert.ToString(Session["user_level"]) == "145")
            {
                gvReport.DataSource = dt.DefaultView.ToTable();
                gvReport.DataBind();
                GridView1.DataSource = dtManual.DefaultView.ToTable();
                GridView1.DataBind();
                if (dt2.Rows.Count > 0)
                {
                    dt2.DefaultView.Sort = "ChildName asc,fathersName asc";
                    GridView2.DataSource = dt2.DefaultView.ToTable();
                    GridView2.DataBind();
                }
            }
            else if (Convert.ToString(Session["user_level"]) == "19")
            {
                gvReport.DataSource = dt.DefaultView.ToTable();
                gvReport.DataBind();
                GridView1.DataSource = dtManual.DefaultView.ToTable();
                GridView1.DataBind();
                if (dt2.Rows.Count > 0)
                {
                    dt2.DefaultView.Sort = "ChildName asc,fathersName asc";
                    GridView2.DataSource = dt2.DefaultView.ToTable();
                    GridView2.DataBind();
                }
                else
                {
                    GridView2.DataSource = null;
                    GridView2.DataBind();
                }
            }

            Session["OutofDoorD2d"] = dt;
            Session["OutofDoorD2d1"] = dtManual;
            Session["D2d1"] = dt2;
        }

        else
        {
            gvReport.DataSource = null;
            gvReport.DataBind();
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

    }

    #endregion
    #region SelectedIndexChanged

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            ddlState.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlDistrict, null);
            if (Session["user_level_Role"].ToString() == "3" || Session["user_level_Role"].ToString() == "4")
            {
                ddlDistrict.SelectedIndex = 1;
                ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
            }
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
        Locking();
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        Locking();
        FillCBBock();
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBCluster();
        FillCBClusterNew();
    }
    protected void ddlBlockserachblock_SelectedIndexChanged(object sender, EventArgs e)
    {
        // FillCBCluster();
        FillCBClusterNew();
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillage();
    }
    protected void ddlCluster_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillageNew();
    }


    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void Chk1_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox activeCheckBox = sender as CheckBox;

        foreach (GridViewRow rw in GridView1.Rows)
        {
            CheckBox chkBx = (CheckBox)rw.FindControl("Chk1");
            if (chkBx != activeCheckBox)
            {
                chkBx.Checked = false;
            }
            else
            {
                chkBx.Checked = true;
            }
        }
    }

    #endregion
    #region ****** Grid view Event *************
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        LoadReport();
    }
    #endregion

    protected void ddlS2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlS2.SelectedIndex > 0 && ddlS2.SelectedValue == "1")
        {
            txtSearchHHNo.Enabled = true;

        }
        else if (ddlS2.SelectedIndex > 0 && ddlS2.SelectedValue == "2")
        {
            txtSearchHHNo.Enabled = false;

        }
        else if (ddlS2.SelectedIndex > 0 && ddlS2.SelectedValue == "3")
        {
            txtSearchHHNo.Enabled = true;

        }
        else if (ddlS2.SelectedIndex > 0 && ddlS2.SelectedValue == "4")
        {
            txtSearchHHNo.Enabled = true;
        }
        else
        {
            txtSearchHHNo.Enabled = true;
        }
    }


    protected void BtnHIde_Click(object sender, EventArgs e)
    {
        DataTable Ds_gvReport1 = Session["OutofDoorD2d1"] as DataTable;

        foreach (GridViewRow Itemst in GridView1.Rows)
        {
            if (((CheckBox)Itemst.FindControl("ChkOutD2d")).Checked)
            {
                Label gg = (Label)Itemst.FindControl("lblUniqueCode");
                DataRow[] dr = Ds_gvReport1.Select("UniqueCode='" + gg.Text + "'");
                if (dr.Length > 0)
                {
                    dr[0]["TempId"] = "2";
                }
            }

        }

        DataRow[] drArr1 = null;
        string StrRo = "RD";

        drArr1 = Ds_gvReport1.Select("TempId =2 ");
        if (drArr1.Length > 0)
        {
            foreach (DataRow row in drArr1)
            {
                Ds_gvReport1.Rows.Remove(row);
            }

            Ds_gvReport1.AcceptChanges();
        }

        Session["OutofDoorD2d1"] = Ds_gvReport1;
        GridView1.DataSource = Ds_gvReport1;
        GridView1.DataBind();
    }

}