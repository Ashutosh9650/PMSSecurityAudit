using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class frmCVverfication : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    string conditions = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {

                FillFormType();
                LoadYear();
                LoadUserLeavel();
                pnlMain.Visible = false;
                ViewState["Save"] = "Save";
                UserLevelFilter();


                ViewState["M"] = "";
                btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }

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
        ddlYear_SelectedIndexChanged(ddlYear, null);
        //}


    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlMain.Visible = false;
        string typevalue = ddlformatype.SelectedValue;
        switch (typevalue)
        {
            case "3":
            case "5":
            case "6":
            case "8":
                ddlschool.Visible = true;
                lblSchool.Visible = true;
                ddlschool.Items.Clear();
                ddlschool.Enabled = false;
                FillCBBock();
                ddlPanchayat.Items.Clear();
                ddlVillage.Items.Clear();
                //FillSchool();

                break;
            default:
                ddlschool.Visible = false;
                lblSchool.Visible = false;
                FillCBBock();
                ddlPanchayat.Items.Clear();
                ddlVillage.Items.Clear();
                break;

        }

    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (ddlYear.SelectedIndex > 0)
        {
            ddlState.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlDistrict, null);
            ddlDistrict.SelectedIndex = 1;
            ddlDistrict_SelectedIndexChanged(ddlBlock, null);
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;

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

    }

    public void Locking()
    {
        if (ddlYear.SelectedIndex > 0)
        {

            btnAdd.Enabled = true;
            btnsave.Enabled = true;
            if (Session["FinYear"].ToString() != ddlYear.SelectedItem.Text)
            {
                string strQry;
                strQry = "Select * from mstModuleLocking  where [FromName]='D2D' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "' ";


                DataTable dtModel = objMain.LoadData(strQry);
                if (dtModel.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtModel.Rows[0]["LockMonth"].ToString()) < DateTime.Today.Month)
                    {
                        btnAdd.Enabled = false;
                        btnsave.Enabled = false;
                        btnDelete.Enabled = false;

                        ViewState["M"] = "M";

                    }

                }

            }

        }
    }
    protected void GV_Project_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //GVMain.PageIndex = e.NewPageIndex;
        if (ViewState["Serach"] != null)
        {
            DataTable dt = ViewState["Serach"] as DataTable;
            //GVMain.DataSource = dt;
            //GVMain.DataBind();
        }

    }
    public void UserLevelFilter()
    {

        string strQry = "";
        string Cond = "Module='Cross Verfication'";
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
        if (vDelete == true)
        {

            btnDelete.Visible = true;
        }
        else
        {

            btnDelete.Visible = false;
        }

        if (vADD == true)
        {
            //   btnAdd.Enabled = true;
            btnsave.Enabled = true;
            FromRight(true);

            //"DOOR-TO-DOOR  SURVEY";
        }
        else
        {
            btnAdd.Enabled = false;

        }
        if (vVerify == true)
        {

            btnsave.Enabled = true;
            FromRight(false);


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

    public void FromRight(Boolean EnableDisable)
    {


    }
    public void LoadUserLeavel()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
            ddlState.SelectedIndex = 0;
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

        }





    }

    public void FillCBState()
    {
        conditions = "";
        objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");
    }

    public void FillFormType()
    {
        conditions = "Flag = 1 ";
        objComman.BindDLL("mstForm", "FormID,FormName ", conditions, "FormID", "asc", ddlformatype, "FormName", "FormID", "--Select--");

    }


    public void FillCBDist()
    {

        conditions = "";


        conditions = "StateCode ='" + ddlState.SelectedValue + "' and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";

        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");



    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        // pnlMain.Enabled = false;

        FillCBDist();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        Locking();
        //pnlMain.Enabled = false;

        FillCBBock();
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        //pnlMain.Enabled = false;

        FillCBCluster();
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        //pnlMain.Enabled = false;

        FillCVillage();
    }
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        //pnlMain.Enabled = false;

        Unique();

        string typevalue = ddlformatype.SelectedValue;

        switch (typevalue)
        {
            case "3":
            case "5":
            case "6":
            case "8":
                ddlschool.Enabled = true;
                FillSchool();

                break;
            default:
                ddlschool.Items.Clear();
                ddlschool.Visible = false;
                lblSchool.Visible = false;
                break;

        }


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
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "'";
        objComman.BindDLLSelectAll("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "Select");



    }
    public void FillCVillage()
    {
        conditions = "";

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

    private void GVMainBind()
    {
        //ViewState["Save"] = null;

        string str = "";
        if (ddlformatype.SelectedIndex > 0)
        {
            str = " where tbl_CVverfication.FormID='" + ddlformatype.SelectedValue.ToString() + "'";
        }
        if (ddlYear.SelectedIndex > 0)
        {
            str += "and mst5Village.Fyear='" + ddlYear.SelectedItem.Text.ToString() + "'";
        }
        if (ddlState.SelectedValue != null && ddlState.SelectedIndex > 0)
        {
            str += "and mst5Village.StateCode='" + ddlState.SelectedValue.ToString() + "'";
        }
        if (ddlDistrict.SelectedValue != null && ddlDistrict.SelectedIndex > 0)
        {
            str = str + "and mst5Village.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "'";
        }

        if (ddlBlock.SelectedValue != null && ddlBlock.SelectedIndex > 0)
        {
            str = str + "and mst5Village.BlockCode='" + ddlBlock.SelectedValue.ToString() + "'";
        }

        if (ddlPanchayat.SelectedValue != null && ddlPanchayat.SelectedIndex > 1)
        {
            str = str + "and mst5Village.PanchayatCode='" + ddlPanchayat.SelectedValue.ToString() + "'";
        }

        if (ddlVillage.SelectedValue != null && ddlVillage.SelectedIndex > 0)
        {
            str = str + "and mst5Village.VillageCode='" + ddlVillage.SelectedValue.ToString() + "'";
        }
        else
        {
            str = str + "and mst5Village.VillageCode='" + 0 + "'";

        }
        DataTable dtmstM = new DataTable();
        string typevalue = ddlformatype.SelectedValue;

        switch (typevalue)
        {
            case "3":
            case "5":
            case "6":
            case "8":
                if (ddlschool.SelectedValue != null && ddlschool.SelectedIndex > 0)
                {
                    str += " and tbl_CVverfication.SchoolCode='" + ddlschool.SelectedValue.ToString() + "'";
                    dtmstM = objMain.LoadData("select UniqueCode, mst5Village.VillageCode, Value, ComboID, tbl_CVverfication.FormID ,tbl_CVverfication.CV_UID, mstCVverfication.CV_FieldName,mstCVverfication.CV_MaxLimit,mstCVverfication.CV_Validation, mstCVverfication.CV_FieldType, mstCVverfication.CV_Flag  from tbl_CVverfication inner join mstCVverfication on mstCVverfication.CV_UID = tbl_CVverfication.CV_UID inner join mst5Village on mst5Village.VillageCode = tbl_CVverfication.VillageCode inner join mst1State on mst1State.StateCode=mst5Village.StateCode inner join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode inner join mst3Block on mst3Block.BlockCode=mst5Village.BlockCode inner join mstPanchayat on mstPanchayat.PanchayatCode=mst5Village.PanchayatCode" + str + "and" + " tbl_CVverfication.FormID = " + typevalue);

                }
                break;
            default:

                dtmstM = objMain.LoadData("select UniqueCode, mst5Village.VillageCode, Value, ComboID, tbl_CVverfication.FormID ,tbl_CVverfication.CV_UID, mstCVverfication.CV_FieldName,mstCVverfication.CV_MaxLimit,mstCVverfication.CV_Validation, mstCVverfication.CV_FieldType , mstCVverfication.CV_Flag from tbl_CVverfication inner join mstCVverfication on mstCVverfication.CV_UID = tbl_CVverfication.CV_UID inner join mst5Village on mst5Village.VillageCode = tbl_CVverfication.VillageCode inner join mst1State on mst1State.StateCode=mst5Village.StateCode inner join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode inner join mst3Block on mst3Block.BlockCode=mst5Village.BlockCode inner join mstPanchayat on mstPanchayat.PanchayatCode=mst5Village.PanchayatCode" + str + "and" + " tbl_CVverfication.FormID = " + typevalue);
                break;

        }



        if (dtmstM.Rows.Count > 0)
        {

            pnlMain.Visible = true;
            Gv_Profile_Search.Visible = true;
            Gv_Profile_Search.Enabled = true;
            //foreach (GridViewRow row in Gv_Profile_Search.Rows)
            //{
            //}
            string header = ddlformatype.SelectedItem.Text;
            SetGVHeader(header);
            ViewState["Save"] = "Update";
            ViewState["Serach"] = dtmstM;
            Gv_Profile_Search.DataSource = dtmstM;
            Gv_Profile_Search.DataBind();



        }
        else
        {
            if (ddlVillage.SelectedValue != null && ddlVillage.SelectedIndex > 0)
            {
                DataTable dtm = objMain.LoadData("select CV_UID, CV_FieldName,CV_FieldType,CV_MaxLimit,CV_Validation,CV_Flag from mstCVverfication where FormID =" + ddlformatype.SelectedValue);
                DataTable dtDD = objMain.LoadData("Select * From mstLookup");
                ViewState["DropDown"] = dtDD;

                Gv_Profile_Search.Visible = true;
                Gv_Profile_Search.Enabled = true;
                ViewState["Save"] = "Save";
                ViewState["Serach"] = dtm;
                string header = ddlformatype.SelectedItem.Text;
                SetGVHeader(header);
                Gv_Profile_Search.DataSource = dtm;
                Gv_Profile_Search.DataBind();
                Gv_Profile_Search.Dispose();
                pnlMain.Visible = true;
                pnlMain.Enabled = true;


            }
            //ViewState["Serach"] = "";
        }

    }

    protected void SetGVHeader(string GVheader)
    {
        //GridViewRowEventArgs e;
        //if (GridViewRowEventArgs row in Gv_Profile_Search.HeaderRow)
        //{
        //    if(Gv_Profile_Search.
        //foreach (GridViewRow row in Gv_Profile_Search.RowHeaderColumn)
        //{
        //}
        //if (GridViewRowEventArgs.Row.RowType == DataControlRowType.Header)
        //    {
        Gv_Profile_Search.Columns[0].HeaderText = GVheader;
        //}
        //}
    }

    protected void txtSearchName_Click(object sender, EventArgs e)
    {
        DataTable dt = ViewState["Serach"] as DataTable;
        string strFilter = "";

        string str = "ChildName";
        string str1 = "HHNo";

        DataTable dtfilter = dt.Copy();



        dtfilter.DefaultView.RowFilter = strFilter;
        dtfilter.DefaultView.Sort = "ChildName asc";


    }

    protected void Gv_Profile_Search_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (ViewState["Save"].ToString() == "Save")
        {
            //if (e.Row.RowType == DataControlRowType.Header)
            //{

            //    Gv_Profile_Search.Columns[0].HeaderText = ddlformatype.SelectedItem.Text;

            //}
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtfirst = (DataTable)ViewState["Serach"];
                DropDownList ddlist = (DropDownList)e.Row.FindControl("ddl");
                Label lbl1 = (Label)e.Row.FindControl("lblCol_2");
                Label lblName = (Label)e.Row.FindControl("Label1");
                int id = Convert.ToInt32(lbl1.Text);

                RadioButtonList rbl = (RadioButtonList)e.Row.FindControl("RadioButtonList1");
                for (int i = 0; i < dtfirst.Rows.Count; i++)
                {
                    if (dtfirst.Rows[i]["CV_FieldType"].ToString() == "L" && Convert.ToInt32(dtfirst.Rows[i]["CV_UID"]) == id)
                    {
                        lblName.Font.Bold = true;
                        lblName.Font.Size = 10;
                        e.Row.Cells[0].BackColor = System.Drawing.Color.FromArgb(120, 196, 253);
                        e.Row.Cells[1].BackColor = System.Drawing.Color.FromArgb(120, 196, 253);

                    }
                    //if (dtfirst.Rows[i]["CV_FieldType"].ToString() == "DT" && Convert.ToInt32(dtfirst.Rows[i]["CV_UID"]) == id)
                    //{
                    //}
                    if (dtfirst.Rows[i]["CV_FieldType"].ToString() == "R" && Convert.ToInt32(dtfirst.Rows[i]["CV_UID"]) == id)
                    {
                        string vl = dtfirst.Rows[i]["CV_Flag"].ToString();
                        rbl.Visible = true;
                        DataTable dtRbl = dtLbind(vl);
                        rbl.DataSource = dtRbl;

                        rbl.DataTextField = "Description1";
                        rbl.DataValueField = "LookupCode";

                        rbl.DataBind();
                    }
                    if (dtfirst.Rows[i]["CV_FieldType"].ToString() == "D" && Convert.ToInt32(dtfirst.Rows[i]["CV_UID"]) == id)
                    {
                        string vl = dtfirst.Rows[i]["CV_Flag"].ToString();
                        ddlist.Visible = true;
                        DataTable dtDD1 = dtLbind(vl);

                        ddlist.DataSource = dtDD1;


                        ddlist.DataTextField = "Description1";
                        ddlist.DataValueField = "LookupCode";

                        ddlist.DataBind();
                        ddlist.Items.Insert(0, new ListItem("-- select --", "0"));




                    }
                }
            }
        }
        if (ViewState["Save"].ToString() == "Update")
        {
            DataTable dtSecond = (DataTable)ViewState["Serach"];

            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txt11 = (TextBox)e.Row.FindControl("txt1");
                    TextBox txtdate = (TextBox)e.Row.FindControl("txtDate");
                    Label lbl = (Label)e.Row.FindControl("lblCol_2");
                    Label lblname = (Label)e.Row.FindControl("Label1");
                    int no = Convert.ToInt32(lbl.Text);
                    RadioButtonList rbl = (RadioButtonList)e.Row.FindControl("RadioButtonList1");
                    RegularExpressionValidator rev = (RegularExpressionValidator)e.Row.FindControl("RegularExpressionValidator1");

                    DropDownList ddlist = (DropDownList)e.Row.FindControl("ddl");

                    // ajax.CalendarExtender 

                    for (int i = 0; i < dtSecond.Rows.Count; i++)
                    {
                        if (dtSecond.Rows[i]["CV_FieldType"].ToString() == "DT" && Convert.ToInt32(dtSecond.Rows[i]["CV_UID"]) == no)
                        {
                            TextBox txdate = (TextBox)e.Row.FindControl("txtDate");
                            txdate.Text = dtSecond.Rows[i]["value"].ToString();
                        }

                        if (Convert.ToInt32(dtSecond.Rows[i]["CV_UID"]) == no)
                        {
                            txt11 = (TextBox)e.Row.FindControl("txt1");
                            txt11.Text = dtSecond.Rows[i]["value"].ToString();
                        }
                        if (dtSecond.Rows[i]["CV_FieldType"].ToString() == "L" && Convert.ToInt32(dtSecond.Rows[i]["CV_UID"]) == no)
                        {
                            lblname.Font.Bold = true;
                            lblname.Font.Size = 10;

                            e.Row.Cells[0].BackColor = System.Drawing.Color.FromArgb(120, 196, 253);
                            e.Row.Cells[1].BackColor = System.Drawing.Color.FromArgb(120, 196, 253);

                        }


                        if (dtSecond.Rows[i]["CV_FieldType"].ToString() == "DT" && Convert.ToInt32(dtSecond.Rows[i]["CV_UID"]) == no)
                        {
                            txt11.Visible = false;

                        }
                        if (dtSecond.Rows[i]["CV_FieldType"].ToString() == "D" && Convert.ToInt32(dtSecond.Rows[i]["CV_UID"]) == no)
                        {
                            string vl = dtSecond.Rows[i]["CV_Flag"].ToString();
                            ddlist.Visible = true;
                            DataTable dtDD1 = dtLbind(vl);
                            ddlist.DataSource = dtDD1;

                            ddlist.DataTextField = "Description1";
                            ddlist.DataValueField = "LookupCode";

                            ddlist.DataBind();
                            ddlist.SelectedValue = dtSecond.Rows[i]["value"].ToString();
                            ddlist.Items.Insert(0, new ListItem("-- select --", "0"));




                        }

                        if (dtSecond.Rows[i]["CV_FieldType"].ToString() == "R" && Convert.ToInt32(dtSecond.Rows[i]["CV_UID"]) == no)
                        {
                            string vl = dtSecond.Rows[i]["CV_Flag"].ToString();
                            rbl.Visible = true;
                            DataTable dtRbl = dtLbind(vl);
                            rbl.DataSource = dtRbl;

                            rbl.DataTextField = "Description1";
                            rbl.DataValueField = "LookupCode";
                            rbl.DataBind();
                            rbl.SelectedValue = dtSecond.Rows[i]["value"].ToString();
                        }
                    }

                }
            }
            catch
            {
                throw;
            }
        }
    }

    protected DataTable dtLbind(string Dllvl)
    {
        DataTable dtDD = objMain.LoadData("Select LookupCode, Description, Description1 From mstLookup  where LookupFlag = '" + Dllvl + "'");
        return dtDD;
    }

    public void FillSchool()
    {
        conditions = "";
        conditions = "VillageCode ='" + ddlVillage.SelectedValue + "' and FYear ='" + ddlYear.SelectedItem.Text + "' ";
        objComman.BindDLLSchool("mstSchool", "SchoolCode,Name", conditions, "Name", "asc", ddlschool, "Name", "SchoolCode", "Select");



    }
    public void FillD2dData(string VCode, int FID)
    {
        DataTable dt = new DataTable();
        try
        {

            dt = objMain.LoadData(" select UniqueCode, VillageCode, Value, ComboID, tbl_CVverfication.FormID ,tbl_CVverfication.CV_UID, mstCVverfication.CV_FieldName,mstCVverfication.CV_MaxLimit,mstCVverfication.CV_MaxLimit,mstCVverfication.CV_Validation, mstCVverfication.CV_FieldType  from  tbl_CVverfication inner join mstCVverfication on mstCVverfication.CV_UID = tbl_CVverfication.CV_UID where VillageCode ='" + VCode + "'and tbl_CVverfication.FormID = " + FID);

        }
        catch
        {
            throw;
        }
        if (dt.Rows.Count > 0)
        {
            ViewState["Dtup"] = dt;
            Gv_Profile_Search.DataSource = dt;
            Gv_Profile_Search.DataBind();


            FillSchool();

        }

    }


    protected void btnsave_Click(object sender, EventArgs e)
    {

        SaveData();
        pnlMain.Visible = false;
        Gv_Profile_Search.Visible = false;



    }
    public void SaveData()
    {
        int result = 0;
        try
        {


            string UCODE = objMain.Generate_RandomString(8);
            string vCODE = ddlVillage.SelectedValue.ToString();
            string SCode;
            string flag = ViewState["Save"].ToString();
            if (ddlschool.SelectedIndex > 0)
            {
                SCode = ddlschool.SelectedValue.ToString();
            }
            else
            {
                SCode = "";
            }


            int formID = Convert.ToInt32(ddlformatype.SelectedValue);
            string value;
            foreach (GridViewRow row in Gv_Profile_Search.Rows)
            {

                Label CVID = (Label)row.FindControl("lblCol_2");
                int CV_UID = Convert.ToInt32(CVID.Text);
                TextBox tx = (TextBox)row.FindControl("txt1");
                TextBox txdate = (TextBox)row.FindControl("txtDate");
                //txdate.Attributes.Add("readonly", "readonly");
                // string datet = txdate.UniqueID.ToString();

                if (tx.Text != "")
                {
                    value = tx.Text;
                }
                else if (txdate.Text != "")
                {
                    value = txdate.Text;

                }
                else
                {
                    value = "";
                }
                Label combo = (Label)row.FindControl("lblFlag");

                string comboid = combo.Text;

                RadioButtonList rbl = (RadioButtonList)row.FindControl("RadioButtonList1");
                if (rbl.SelectedValue != "")
                {
                    value = rbl.SelectedValue;
                }
                DropDownList dl = (DropDownList)row.FindControl("ddl");
                if (dl.SelectedIndex > 0)
                {
                    value = dl.SelectedValue;
                }

                result = objMain.InsertUpdateverification(UCODE, vCODE, SCode, CV_UID, value, comboid, formID, flag);



                //int value = 
            }
        }
        catch
        {
            throw;
        }
        if (result > 0)
        {
            if (ViewState["Save"].ToString() == "Update")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully')</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully')</script>", false);
            }

        }






    }



    protected void btnAdd_Click(object sender, EventArgs e)
    {
        pnlMain.Enabled = true;
        FillSchool();
        ClearData();

        // txtSarveyDate.Focus();
        Unique();

    }
    public void ClearData()
    {


    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
        if (ViewState["ChildId"].ToString() != null)
        {
            int res1 = objMain.DeleteD2dData(ViewState["ChildId"].ToString(), "D");



            if (res1 > 0)
            {
                GVMainBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

            }

        }
    }


    protected void btnSerach_Click(object sender, EventArgs e)
    {
        //GVMain.Enabled = true;
        if (ddlformatype.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Type')</script>", false);


            ddlformatype.Focus();
            return;
        }

        if (ddlVillage.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Village')</script>", false);


            ddlVillage.Focus();
            return;
        }
        if (Convert.ToInt32(ddlformatype.SelectedValue) == 3 || Convert.ToInt32(ddlformatype.SelectedValue) == 5 || Convert.ToInt32(ddlformatype.SelectedValue) == 6 || Convert.ToInt32(ddlformatype.SelectedValue) == 8)
        {
            if (ddlschool.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select School')</script>", false);


                ddlschool.Focus();
                return;
            }
        }
        GVMainBind();
        pnlMain.Enabled = true;

    }

    public void Unique()
    {
        if (ViewState["Save"].ToString() == "Save")
        {
            if (ddlVillage.SelectedIndex > 0)
            {

                Int32 mNewNo = 0;
                string strAlias;
                string strQry = " Select top 1 Serial from tblDTD where VillageCode='" + ddlVillage.SelectedValue + "' and EnrollStatus=1  order by Serial desc ";

                DataTable dt = objMain.LoadData(strQry);

                string strQry1 = " Select EGVillageCode as VillageCode  from mst5Village where VillageCode='" + ddlVillage.SelectedValue + "' ";
                DataTable dtVillage = objMain.LoadData(strQry1);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Serial"].ToString() == "" || dt.Rows[0]["Serial"].ToString() == "-1")
                    {
                        mNewNo += 1;
                        strAlias = mNewNo.ToString().PadLeft(3, '0');

                        ViewState["NumNo"] = strAlias;
                    }
                    else
                    {
                        mNewNo = Convert.ToInt32(dt.Rows[0]["Serial"].ToString());
                        mNewNo += 1;
                        strAlias = mNewNo.ToString().PadLeft(3, '0');

                        ViewState["NumNo"] = strAlias;


                    }

                }
                else
                {
                    mNewNo += 1;
                    strAlias = mNewNo.ToString().PadLeft(3, '0');

                    ViewState["NumNo"] = strAlias;
                }
            }
        }


    }


}