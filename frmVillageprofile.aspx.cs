using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmVillageprofile : System.Web.UI.Page
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

                LoadYear();
                LoadUserLeavel();


                fillcombos("C", ddlMainCaste1);
                fillcombos("C", ddlMainCastes2);
                fillcombos("C", ddlMainCastes3);
                fillcombos("O", ddlPrimaryOccupation);
                fillcombos("O", ddlSecondaryOccupation);
                fillcombos("O", ddlOtherOccupation);
                fillcombos("S", ddlConectivityfromMainRoad);
                fillcombos("S", ddlElect);
                fillcombos("S", ddlAvailablity);
                fillcombos("WS", ddlSourceofdrinkingwater);
                fillcombos("TM", ddlModeoftrans);

                ValdateUserLavel();



            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }
        }

    }
    public void SaveImage()
    {
        #region UploadImage 
        string Fullfilename = "";
        string Fullfilename1 = "";
        if (FileuploadAttach.PostedFile != null && FileuploadAttach.PostedFile.FileName != "")
        {
            string ext = System.IO.Path.GetExtension(FileuploadAttach.PostedFile.FileName).ToLower();
            if (FileuploadAttach.PostedFile.ContentLength < 202400)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Image size must be less than 100kb')</script>", false);
                return;
            }
            if (ext != ".jpeg" && ext != ".jpg" && ext != ".png" && ext != ".gif")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Images')</script>", false);
                return;
            }
            string exten = Path.GetExtension(FileuploadAttach.PostedFile.FileName);
            Fullfilename = "" + txtVillageCode.Text + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;

            string sFileDir = Server.MapPath(Comman.GetImagePath("DataBackupPath")); ;
            string fullpathh = sFileDir + Fullfilename;
            if (FileuploadAttach.PostedFile != null && FileuploadAttach.PostedFile.FileName != "")
            {

                // string Imagefile1 = "LeaveDoc" + "_" + Convert.ToString(Session["EMP_ID"]) + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;

                //create directory

                if (Directory.Exists(sFileDir)) { }
                else { System.IO.Directory.CreateDirectory(sFileDir); }

                //======update the file =====\\

                if (System.IO.File.Exists(sFileDir + "\\" + Fullfilename))
                {
                    try { System.IO.File.Delete(sFileDir + "\\" + Fullfilename); }
                    catch
                    {
                        //ShowMessage.Visible = true;
                        //ShowMessage.Style.Add("background-color", "#FFBABA");
                        //MessageLBL.Style.Add("Color", "#D8000C");
                        //MessageLBL.Text = ex.ToString();

                    }
                }
                FileuploadAttach.PostedFile.SaveAs(sFileDir + Fullfilename);

            }

            string sFileDir1 = Server.MapPath(Comman.GetImagePath("ImportExcelPath") + "/");
            Fullfilename1 = sFileDir1 + Fullfilename;
            Bitmap bmp1 = new Bitmap(fullpathh);


            ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);


            System.Drawing.Imaging.Encoder myEncoder =
            System.Drawing.Imaging.Encoder.Quality;

            EncoderParameters myEncoderParameters = new EncoderParameters(1);

            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);

            myEncoderParameter = new EncoderParameter(myEncoder, 0L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            bmp1.Save(Fullfilename1, jgpEncoder, myEncoderParameters);

        }



        #endregion


    }
    private ImageCodecInfo GetEncoder(ImageFormat format)
    {

        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

        foreach (ImageCodecInfo codec in codecs)
        {
            if (codec.FormatID == format.Guid)
            {
                return codec;
            }
        }
        return null;
    }
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


        }
        else
        {
            ddlState.SelectedIndex = 0;
            ddlDistrict.Items.Clear();
            ddlBlock.Items.Clear();
            ddlPanchayat.Items.Clear();

        }



    }
    public void Locking()
    {
        if (ddlYear.SelectedIndex > 0)
        {

            btnsave.Enabled = true;
            btnSUmbit.Enabled = true;
            string strQry;
            strQry = "Select * from mstModuleLocking  where [FromName]='VIP' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "'";

            if (Session["FinYear"].ToString() != ddlYear.SelectedItem.Text)
            {
                DataTable dtModel = objMain.LoadData(strQry);
                if (dtModel.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtModel.Rows[0]["LockMonth"].ToString()) < DateTime.Today.Month)
                    {
                        btnsave.Enabled = false;
                        btnSUmbit.Enabled = false;
                        btnDelete.Enabled = false;

                    }

                }

            }
        }
    }
    public void ValdateUserLavel()
    {

        string strQry = "";
        string Cond = "Module='VIllageProfile' ";
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

            btnDelete.Visible = true;
        }

        if (vADD == true)
        {
            btnAdd.Enabled = true;
            btnsave.Enabled = true;
            lblMain.Text = "Village Profile";
        }
        else
        {
            btnAdd.Enabled = false;
            btnsave.Enabled = false;
        }

        if (vVerify == true)
        {

            btnsave.Enabled = true;

            lblMain.Text = "Village Information(Verify)";

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
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
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

            ddlDistrict.SelectedIndex = 1;
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

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
        DateTime GivenDate = DateTime.Now;
        int GivenYear = GivenDate.Year;
        int m = GivenDate.Month;

        DataTable dt = null;
        //ddlYear.Items.Add("--Select--","0");
        int y = GivenDate.Year;
        string strQry = "";
        if (Session["NewDistrictCode"].ToString() == "7E673ED1107241C696C6954C2" || Session["NewDistrictCode"].ToString() == "B2CDC6AC58C741749E866A3BA" || Session["NewDistrictCode"].ToString() == "51A6384B637749D399E599219")
        {
            strQry = "Select Min(Fyear) as Type,0 as ID from mst2District  where [DistrictCode]='" + Session["NewDistrictCode"].ToString() + "'   ";
        }
        else
        {
            strQry = "Select Min(Fyear) as Type,0 as ID from mst2District  where [DistrictCode] in(" + Session["DistrictCode"].ToString() + ")   ";
        }

        DataTable dtFyear = objMain.LoadData(strQry);
        //DateTime GivenDate1 = DateTime.Now;
        //int GivenYear1 = GivenDate1.Year;
        DataTable dtYear = CreateDataTable();
        //    DataTable dtYear=null;
        DataRow dr;
        if (dtFyear.Rows.Count > 0)
        {
            if (ddlYear.SelectedIndex < 0)
            {

                //string mYear1 = GivenYear1.ToString();
                for (int i = 0; i < dtFyear.Rows.Count; i++)
                {
                    string[] LineData;
                    string MfYear = dtFyear.Rows[i]["Type"].ToString();
                    char Seperator = '-';
                    LineData = MfYear.Split(Seperator);
                    int idd = 0;

                    dr = dtYear.NewRow();
                    dr["Type"] = dtFyear.Rows[i]["Type"].ToString();
                    if (LineData[0].ToString() == "")
                    {
                    }
                    else
                    {
                        dr["ID"] = LineData[0];
                    }

                    dtYear.Rows.Add(dr);
                    //if (m > 3)
                    //{
                    //    dr = dtYear.NewRow();
                    //    dr["Type"] = GivenYear.ToString() + "-" + Convert.ToString((GivenYear + 1));
                    //    dr["ID"] = y;
                    //    dtYear.Rows.Add(dr);
                    //    dr = dtYear.NewRow();
                    //    dr["Type"] = GivenYear - 1 + "-" + Convert.ToString((GivenYear - 1 + 1));
                    //    dr["ID"] = y - 1;
                    //    dtYear.Rows.Add(dr);
                    //    //get last  two digits (eg: 10 from 2010);

                    //}
                    //else
                    //{
                    //    dr = dtYear.NewRow();
                    //    dr["Type"] = Convert.ToString((y - 1)) + "-" + y.ToString();
                    //    //y = y - 1;
                    //    dr["ID"] = y - 1;

                    //    dtYear.Rows.Add(dr);


                    //}

                }

            }
        }
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");
        if (dtYear.Rows.Count > 0)
        {
            ddlYear.SelectedIndex = 1;
            //}

            ddlYear_SelectedIndexChanged(ddlYear, null);
        }
    }
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
            conditions = "StateCode ='" + ddlState.SelectedValue + "'   and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
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
    public void FillCBCluster()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "'";
        objComman.BindDLL("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "--Select--");
    }
    private void GVMainBind()
    {

        string str = "";

        if (ddlState.SelectedValue != null && ddlState.SelectedIndex > 0)
        {
            str = "where mst5Village.StateCode='" + ddlState.SelectedValue.ToString() + "'";
        }
        if (ddlDistrict.SelectedValue != null && ddlDistrict.SelectedIndex > 0)
        {
            str = str + " and mst5Village.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "'";
        }

        if (ddlBlock.SelectedValue != null && ddlBlock.SelectedIndex > 0)
        {
            str = str + " and mst5Village.BlockCode='" + ddlBlock.SelectedValue.ToString() + "'";
        }

        if (ddlPanchayat.SelectedValue != null && ddlPanchayat.SelectedIndex > 0)
        {
            str = str + " and mst5Village.PanchayatCode='" + ddlPanchayat.SelectedValue.ToString() + "'";
        }


        DataTable dtVilllage = null;
        dtVilllage = objMain.LoadData("Select EGVillageCode as VillageCode, VillageCode as NewVillageCode,VillageName  from mst5Village " + str + " ");
        GvVillage.DataSource = dtVilllage;
        GvVillage.DataBind();
        ViewState["Serach"] = dtVilllage;

    }
    private void RefreshControl()
    {
        txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtSarpanch.Text = "";
        TxtDistance.Text = ""; ddlMainCaste1.SelectedIndex = 0; ddlMainCastes2.SelectedIndex = 0; ddlMainCastes3.SelectedIndex = 0; ddlPrimaryOccupation.SelectedIndex = 0;
        ddlSecondaryOccupation.SelectedIndex = 0; ddlOtherOccupation.SelectedIndex = 0; txtTotalHouseholds.Text = ""; txtNoofAnganwari.Text = ""; ddlConectivityfromMainRoad.SelectedIndex = 0;
        TxtGovt1.Text = ""; TxtGovt2.Text = ""; TxtGovt3.Text = ""; TxtGovt4.Text = ""; TxtGovt5.Text = ""; TxtPvt1.Text = ""; TxtPvt2.Text = ""; TxtPvt3.Text = ""; TxtPvt4.Text = "";
        TxtPvt5.Text = ""; TxtPvt6.Text = ""; TxtCont.Text = ""; TxtHall.Text = ""; TxtHospital.Text = ""; TxtMarket.Text = ""; ddlElect.SelectedIndex = 0; ddlSourceofdrinkingwater.SelectedIndex = 0;
        TxtYouth.Text = ""; ddlAvailablity.SelectedIndex = 0; TxtBank.Text = ""; TextBox1.Text = "";
        ddlModeoftrans.SelectedIndex = 0;
        TxtDhani.Text = "";
        txtTotalpopulation.Text = "";
        ViewState["ImagePath"] = null;

    }
    public void SavaData()
    {


    }
    #region -------- Button Click Event  ---------
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        RefreshControl();
        pnlMain.Enabled = true;
        GVMainBind();
        ViewState["hdnFlag"] = "I";

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        RefreshControl();
        ViewState["hdnFlag"] = "I";
        txtSarpanch.Focus();
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        SavaData();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

    }
    protected void btnSumbit_Click(object sender, EventArgs e)
    {
        btnsave_Click(sender, e);
    }
    protected void btnYearAdd_Click(object sender, EventArgs e)
    {
    }
    #endregion
    #region  -------- SelectedIndexChangedEvent  ----------
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlMain.Enabled = false;
        pnlMain1.Enabled = false;
        FillCBDist();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        Locking();
        pnlMain.Enabled = false;
        pnlMain1.Enabled = false;
        FillCBBock();
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlMain.Enabled = false;
        pnlMain1.Enabled = false;
        FillCBCluster();
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlMain.Enabled = false;
        pnlMain1.Enabled = false;

    }
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlMain.Enabled = false;
        pnlMain1.Enabled = false;
        // Unique();
    }

    #endregion
    #region GvVillage Events
    protected void GvVillage_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "GV_VIO")
        {
            GV_name_Add.DataSource = null;
            GV_name_Add.DataBind();
            ViewState["ECurrentTable"] = null;
            int iIndex = Convert.ToInt32(e.CommandArgument);
            string VillageCode = GvVillage.DataKeys[iIndex]["NewVillageCode"].ToString();
            ViewState["hdnFlag"] = "U";
            ViewState["VillageCode"] = VillageCode;

            fillGridName();
            FillControls(VillageCode);

        }
    }

    private void FillControls(string VillageCode)
    {

        string strQry = string.Empty;
        if (vVerify == true)
        {
            strQry = " Select mst5Village.VillageName,ImagePath,mst5Village.DistrictCode,TotalFali,Totalpopulation,mst5Village.BlockCode,mst5Village.PanchayatCode,ClusterCode, mstVillageTS.VillageCode,Year,SarpanchName1 as SarpanchName,SarpanchContact1 as SarpanchContact,MainCaste11 as MainCaste1,MainCaste22 as MainCaste2,MainCaste33 as MainCaste3,Occupation11 as Occupation1,Occupation22 as Occupation2,Occupation33 as Occupation3,DistanceDistrictHQ1 as DistanceDistrictHQ,NoAnganwadi1 as NoAnganwadi ,TotalHH1 as TotalHH,ConnectivityMainRoad1 as ConnectivityMainRoad,ModeTransport1 as ModeTransport,DistanceSchool1 as DistanceSchool,Govt_PS1 as Govt_PS,Govt_UPS1 as Govt_UPS,Govt_SS1 as Govt_SS,Govt_USS1 as Govt_USS,Govt_Total1 as Govt_Total,Pvt_PS1 as Pvt_PS,Pvt_UPS1 as Pvt_UPS,Pvt_SS1 as Pvt_SS,Pvt_USS1 as Pvt_USS,Pvt_Total1 as Pvt_Total,Electricity1 as Electricity,DrinkingWaterSource1 as DrinkingWaterSource,NoCommunityCentre1 as NoCommunityCentre,NoYouthGroup1 as NoYouthGroup,AvailabilityFemaleGroup1 as AvailabilityFemaleGroup,DistanceHospital1 as DistanceHospital,NearestBank1 as NearestBank,NearestMarket1 as NearestMarket, Status from mstVillageTS inner join mst5Village on mst5Village.villagecode=mstVillageTS.Villagecode where mstVillageTS.VillageCode='" + VillageCode + "' ";
        }
        else
        {
            strQry = " Select mst5Village.VillageName,ImagePath,mst5Village.DistrictCode,TotalFali,Totalpopulation,mst5Village.BlockCode,mst5Village.PanchayatCode,ClusterCode, mstVillageTS.VillageCode,Year,SarpanchName,SarpanchContact,MainCaste1,MainCaste2,MainCaste3,Occupation1,Occupation2,Occupation3,DistanceDistrictHQ,NoAnganwadi,TotalHH,ConnectivityMainRoad,ModeTransport,DistanceSchool,Govt_PS,Govt_UPS,Govt_SS,Govt_USS,Govt_Total,Pvt_PS,Pvt_UPS,Pvt_SS,Pvt_USS,Pvt_Total,Electricity,DrinkingWaterSource,NoCommunityCentre,NoYouthGroup,AvailabilityFemaleGroup,DistanceHospital,NearestBank,NearestMarket,Status from mstVillageTS inner join mst5Village on mst5Village.villagecode=mstVillageTS.Villagecode where mstVillageTS.VillageCode='" + VillageCode + "' ";

        }
        DataTable dtvillageTS = objMain.LoadData(strQry);
        string strQryMain = " Select EGVillageCode as VillageCode,VillageName,DistrictCode,BlockCode,PanchayatCode,ClusterCode from mst5Village  where VillageCode='" + VillageCode + "' ";
        DataTable dtvillageMain = objMain.LoadData(strQryMain);

        strQry = "";
        strQry = " Select VillageCode,DhaniName,VillageGUID  from mstVillageDhani where mstVillageDhani.VillageCode='" + VillageCode + "' ";

        DataTable dtFali = objMain.LoadData(strQry);
        if (dtFali.Rows.Count > 0)
        {
            GV_name_Add.DataSource = dtFali;
            GV_name_Add.DataBind();
            ViewState["ECurrentTable"] = dtFali;
        }
        if (dtvillageMain.Rows.Count > 0)
        {
            txtVillageCode.Text = dtvillageMain.Rows[0]["Villagecode"].ToString();
            TxtVillageName.Text = dtvillageMain.Rows[0]["VillageName"].ToString();
        }
        if (dtvillageTS.Rows.Count > 0)
        {
            try
            {


                //if (Convert.ToBoolean(ViewState["vADD"].ToString()) == true || Convert.ToBoolean(ViewState["vVerify"].ToString()) == true)
                //{
                //    if (Convert.ToBoolean(ViewState["vADD"].ToString()) == true)
                //    {

                //        btnsave.Enabled = true;

                //    }
                //    if (Convert.ToBoolean(ViewState["vVerify"].ToString()) == true)
                //    {
                //        btnsave.Enabled = true;
                //    }
                //}
                //else
                //{
                //    btnsave.Enabled = false;
                //}

            }
            catch (Exception e)
            {
            }


            if (dtvillageMain.Rows[0]["ClusterCode"].ToString() != "")
            {
                // ddlPanchayat.SelectedValue = dtvillageMain.Rows[0]["ClusterCode"].ToString();
            }
            else
            {
                // ddlPanchayat.SelectedIndex = -1;
            }
        }
        if (dtvillageTS.Rows.Count > 0)
        {
            if (dtvillageTS.Rows[0]["Year"].ToString() != "0")
            {
                ddlYear.Text = dtvillageTS.Rows[0]["Year"].ToString();
            }
            else
            {
                ddlYear.SelectedIndex = 0;
            }
            if (dtvillageTS.Rows[0]["ImagePath"].ToString() != "")
            {
                //string sFileDir = Server.MapPath(Comman.GetImagePath("ImgPage") + dtmstM.Rows[0]["ImagePath"].ToString().Trim() + "");
                //string sFileDir = Request.PhysicalApplicationPath + "images\\";
                string imagename = dtvillageTS.Rows[0]["ImagePath"].ToString().Trim();
                ViewState["ImagePath"] = imagename;
                imgMKS.ImageUrl = ResolveUrl("~/ImportExcel/" + imagename);
            }
            else
            {
                ViewState["ImagePath"] = "";

                imgMKS.ImageUrl = null;
            }
            ViewState["hdnFlag"] = "U";
            txtSarpanch.Text = dtvillageTS.Rows[0]["SarpanchName"].ToString();
            TxtCont.Text = dtvillageTS.Rows[0]["SarpanchContact"].ToString();
            TextBox1.Text = dtvillageTS.Rows[0]["distanceSchool"].ToString();
            TxtDhani.Text = dtvillageTS.Rows[0]["TotalFali"].ToString();
            txtTotalpopulation.Text = dtvillageTS.Rows[0]["Totalpopulation"].ToString();


            if (dtvillageTS.Rows[0]["MainCaste1"].ToString() != "0")
            {
                ddlMainCaste1.SelectedValue = dtvillageTS.Rows[0]["MainCaste1"].ToString();
            }
            else
            {
                ddlMainCaste1.SelectedIndex = 0;
            }

            if (dtvillageTS.Rows[0]["MainCaste2"].ToString() != "0")
            {
                ddlMainCastes2.SelectedValue = dtvillageTS.Rows[0]["MainCaste2"].ToString();
            }
            else
            {
                ddlMainCastes2.SelectedIndex = 0;
            }
            if (dtvillageTS.Rows[0]["MainCaste3"].ToString() != "0")
            {
                ddlMainCastes3.SelectedValue = dtvillageTS.Rows[0]["MainCaste3"].ToString();
            }
            else
            {
                ddlMainCastes3.SelectedIndex = 0;
            }
            if (dtvillageTS.Rows[0]["Occupation1"].ToString() != "0")
            {
                ddlPrimaryOccupation.SelectedValue = dtvillageTS.Rows[0]["Occupation1"].ToString();
            }
            else
            {
                ddlPrimaryOccupation.SelectedIndex = 0;
            }
            if (dtvillageTS.Rows[0]["Occupation2"].ToString() != "0")
            {
                ddlSecondaryOccupation.SelectedValue = dtvillageTS.Rows[0]["Occupation2"].ToString();
            }
            else
            {
                ddlSecondaryOccupation.SelectedIndex = 0;
            }
            if (dtvillageTS.Rows[0]["Occupation3"].ToString() != "0")
            {
                ddlOtherOccupation.SelectedValue = dtvillageTS.Rows[0]["Occupation3"].ToString();
            }
            else
            {
                ddlOtherOccupation.SelectedIndex = 0;
            }
            TxtDistance.Text = dtvillageTS.Rows[0]["DistanceDistrictHQ"].ToString();
            txtNoofAnganwari.Text = dtvillageTS.Rows[0]["NoAnganwadi"].ToString();
            txtTotalHouseholds.Text = dtvillageTS.Rows[0]["TotalHH"].ToString();
            if (dtvillageTS.Rows[0]["ConnectivityMainRoad"].ToString() != "0")
            {
                ddlConectivityfromMainRoad.SelectedValue = dtvillageTS.Rows[0]["ConnectivityMainRoad"].ToString();
            }
            else
            {
                ddlConectivityfromMainRoad.SelectedIndex = 0;
            }
            if (dtvillageTS.Rows[0]["ModeTransport"].ToString() != "0")
            {
                ddlModeoftrans.SelectedValue = dtvillageTS.Rows[0]["ModeTransport"].ToString();
            }
            else
            {
                ddlModeoftrans.SelectedIndex = 0;
            }
            // txtschooldist.Text = dtvillageTS.Rows[0]["DistanceSchool"].ToString();



            TxtGovt1.Text = dtvillageTS.Rows[0]["Govt_PS"].ToString();
            TxtPvt1.Text = dtvillageTS.Rows[0]["Pvt_PS"].ToString();
            TxtGovt2.Text = dtvillageTS.Rows[0]["Govt_UPS"].ToString();
            TxtPvt2.Text = dtvillageTS.Rows[0]["Pvt_UPS"].ToString();
            TxtGovt3.Text = dtvillageTS.Rows[0]["Govt_SS"].ToString();

            TxtPvt3.Text = dtvillageTS.Rows[0]["Pvt_SS"].ToString();

            TxtGovt4.Text = dtvillageTS.Rows[0]["Govt_USS"].ToString();
            TxtPvt4.Text = dtvillageTS.Rows[0]["Pvt_USS"].ToString();

            TxtGovt5.Text = dtvillageTS.Rows[0]["Govt_Total"].ToString();
            TxtPvt5.Text = dtvillageTS.Rows[0]["Pvt_Total"].ToString();


            if (dtvillageTS.Rows[0]["Electricity"].ToString() != "0")
            {
                ddlElect.SelectedValue = dtvillageTS.Rows[0]["Electricity"].ToString();
            }
            else
            {
                ddlElect.SelectedIndex = 0;
            }
            if (dtvillageTS.Rows[0]["DrinkingWaterSource"].ToString() != "0")
            {
                ddlSourceofdrinkingwater.SelectedValue = dtvillageTS.Rows[0]["DrinkingWaterSource"].ToString();
            }
            else
            {
                ddlSourceofdrinkingwater.SelectedIndex = 0;
            }
            if (dtvillageTS.Rows[0]["AvailabilityFemaleGroup"].ToString() != "0")
            {
                ddlAvailablity.SelectedValue = dtvillageTS.Rows[0]["AvailabilityFemaleGroup"].ToString();
            }
            else
            {
                ddlAvailablity.SelectedIndex = 0;
            }
            SumTotal();

            TxtHall.Text = dtvillageTS.Rows[0]["NoCommunityCentre"].ToString();
            TxtYouth.Text = dtvillageTS.Rows[0]["NoYouthGroup"].ToString();

            TxtHospital.Text = dtvillageTS.Rows[0]["DistanceHospital"].ToString();
            TxtBank.Text = dtvillageTS.Rows[0]["NearestBank"].ToString();
            TxtMarket.Text = dtvillageTS.Rows[0]["NearestMarket"].ToString();

        }
        else
        {
            RefreshControl();
            //  btnsave.Enabled = true;

            ViewState["hdnFlag"] = "I";

        }
    }
    public void SumTotal()
    {

        int govprimaryschool = 0, pvtprimaryschool = 0, govupperprimary = 0, pvtupperprimary = 0, govsec = 0, pvtsec = 0, govsensec = 0, pvtsensec = 0, govtot = 0, pvttot = 0, totalschool = 0;

        if (TxtGovt1.Text != "")
        {
            govprimaryschool = Convert.ToInt32(TxtGovt1.Text);
        }
        if (TxtPvt1.Text != "")
        {
            pvtprimaryschool = Convert.ToInt32(TxtPvt1.Text);
        }
        if (TxtGovt2.Text != "")
        {
            govupperprimary = Convert.ToInt32(TxtGovt2.Text);
        }
        if (TxtPvt2.Text != "")
        {
            pvtupperprimary = Convert.ToInt32(TxtPvt2.Text);
        }
        if (TxtGovt3.Text != "")
        {
            govsec = Convert.ToInt32(TxtGovt3.Text);
        }

        if (TxtPvt3.Text != "")
        {
            pvtsec = Convert.ToInt32(TxtPvt3.Text);
        }
        if (TxtGovt4.Text != "")
        {
            govsensec = Convert.ToInt32(TxtGovt4.Text);
        }
        if (TxtPvt4.Text != "")
        {
            pvtsensec = Convert.ToInt32(TxtPvt4.Text);
        }

        govtot = govprimaryschool + govupperprimary + govsec + govsensec;
        if (govtot > 0)
        {
            TxtGovt5.Text = govtot.ToString();
        }
        pvttot = pvtprimaryschool + pvtupperprimary + pvtsec + pvtsensec;
        if (pvttot > 0)
        {
            TxtPvt5.Text = pvttot.ToString();
        }

        totalschool = govtot + pvttot;
        if (totalschool > 0)
        {
            TxtPvt6.Text = totalschool.ToString();
        }

    }
    protected void GvVillage_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvVillage.PageIndex = e.NewPageIndex;
        if (ViewState["Serach"] != null)
        {
            DataTable dt = ViewState["Serach"] as DataTable;
            GvVillage.DataSource = dt;
            GvVillage.DataBind();
        }
    }
    #endregion
    public void fillcombos(string LookupFlag, DropDownList dropdown)
    {
        conditions = "";
        conditions = "LookupFlag ='" + LookupFlag + "'";
        objComman.BindDLL("mstLookup", "LookupCode,Description,SeqNo", conditions, "SeqNo", "asc", dropdown, "Description", "LookupCode", "--Select--");



    }

    public void fillGridName()
    {

        DataTable dtType = new DataTable();
        DataRow dr;
        dtType.Columns.Add("VillageCode", System.Type.GetType("System.String"));
        dtType.Columns.Add("DhaniName", System.Type.GetType("System.String"));

        dtType.Columns.Add("VillageGUID", System.Type.GetType("System.String"));



        dr = dtType.NewRow();
        dr["VillageCode"] = "0";
        dr["DhaniName"] = "";
        dr["VillageGUID"] = "";

        dtType.Rows.Add(dr);


        GV_name_Add.DataSource = dtType;
        GV_name_Add.DataBind();
        ViewState["ECurrentTable"] = dtType;
    }
    protected void btnAdd_Click1(object sender, EventArgs e)
    {

        EAddNewRowToGrid();

        // ddllevel_selectindexchange(sender, e);
    }
    private void EAddNewRowToGrid()
    {
        int rowIndex = 0;

        if (ViewState["ECurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["ECurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values

                    TextBox box1 = (TextBox)GV_name_Add.Rows[rowIndex].Cells[0].FindControl("lblName");

                    drCurrentRow = dtCurrentTable.NewRow();
                    //drCurrentRow["RowNumber"] = i + 1;txtno




                    if (box1.Text == "")
                    { dtCurrentTable.Rows[i - 1]["DhaniName"] = DBNull.Value; }
                    else
                    { dtCurrentTable.Rows[i - 1]["DhaniName"] = box1.Text; }





                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);



                ViewState["ECurrentTable"] = dtCurrentTable;

                GV_name_Add.DataSource = dtCurrentTable;
                GV_name_Add.DataBind();
            }
        }
        else
        {
            // Response.Write("ViewState is null");
        }


        ESetPreviousData();
    }
    private void ESetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["ECurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["ECurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    TextBox box1 = (TextBox)GV_name_Add.Rows[rowIndex].Cells[0].FindControl("lblName");


                    box1.Text = dt.Rows[i]["DhaniName"].ToString();

                    rowIndex++;
                }
            }
        }
    }
    protected void Img_btn_delete_Click(object sender, EventArgs e)
    {
        //EAddNewRowToGrid();
        DataTable dt_AfterDelete = (DataTable)ViewState["ECurrentTable"];
        ImageButton Img_btn_delete = sender as ImageButton;
        GridViewRow row = Img_btn_delete.NamingContainer as GridViewRow;
        int index = row.RowIndex;
        DataRow dr = dt_AfterDelete.Rows[index];

        dr.Delete();
        dt_AfterDelete.AcceptChanges();
        string Id;


        // fillGridName();
        if (dt_AfterDelete.Rows.Count == 0)
        {
            fillGridName();
            GV_name_Add.DataSource = (DataTable)ViewState["ECurrentTable"];
            GV_name_Add.DataBind();
        }
        else
        {
            GV_name_Add.DataSource = dt_AfterDelete;
            GV_name_Add.DataBind();
        }
    }
}